#include "link.h"
#include "gamesys.h"
#include "il2cpp_api.h"
#include "inventory.h"
#include "shmem.h"
// winsock2 必须先于 windows.h
#include <winsock2.h>
#include <ws2tcpip.h>
#include <windows.h>
#include <atomic>
#include <cmath>
#include <cstdio>
#include <cstring>
#include <fstream>
#include <string>
#include <thread>

#pragma comment(lib, "ws2_32.lib")

namespace ecl {

namespace {

void* Invoke(void* methodInfo, void* obj, void** params);  // 前置声明
void LogLink(const std::string& msg);                     // 前置声明(定义在下方; 自动回退日志要用)

#pragma pack(push, 1)
struct UnityVector3 { float x, y, z; };
struct UnityQuaternion { float x, y, z, w; };
#pragma pack(pop)

LinkConfig g_cfg;
UnityHandles g_handles;
std::atomic<uint64_t> g_lastRecvMs{0};
std::atomic<bool> g_linkStop{false};
std::thread g_udpThread;
std::atomic<bool> g_udpRunning{false};

PoseFrame g_latest;  // UDP 线程写; Unity 线程经 g_lastRecvMs 同步后读
std::atomic<int> g_pendingCmd{0};      // 运行时指令(12字节 ECMD 包)
std::atomic<float> g_pendingCmdArg{0.f};
// v2: 20 字节 ECM2 包携带的 4 个浮点参数(镜头参数/模拟输入/多参指令)
std::atomic<float> g_cmdA0{0.f}, g_cmdA1{0.f}, g_cmdA2{0.f}, g_cmdA3{0.f};
std::atomic<int> g_poseModeOverride{-1};  // >=0 时覆盖 ini 的 pose_mode
std::atomic<bool> g_baselineLogged{false};
// v16: 运行时覆盖 ini 的朝向映射模式。v1.0.0at 起语义扩成 4 值:
//   -1 = 用 ini, 0 = 相对(按 ini 的 pose_rot_increment_frame), 1 = 绝对,
//    2 = 相对·世界系增量, 3 = 相对·本地帧增量(1.0.0z 旧语义)
std::atomic<int> g_rotAbsOverride{-1};

// v16: 朝向是否用"绝对"映射。修正"Blender 里绕本地 xyz 旋转在游戏里不是标准直角坐标系"。
// 相对模式把 Blender 的旋转增量叠加到游戏相机自己的基线朝向上, 而两者基线朝向天然相差
// 一个常量角 Δ(实测可达几十度), 于是 Blender 的本地轴被 Δ 转过去 —— 俯仰会带出滚转、
// 三轴互相串。绝对模式令 Δ=0: 游戏相机朝向 = 映射后的 Blender 朝向, 本地轴一一对应。
//
// v1.0.0at: 相对模式的旋转也**默认按标准语义**(kRotRelAligned = 与绝对一致)。原因(实测):
//   相对模式的零点是"游戏相机自己的基线朝向", 与映射后的 Blender 基线相差 Δ(现场 53.3°/89.0°)。
//   增量只能左乘(世界系)或右乘(本地帧), 所以 Δ≠0 时**必然有一个系是歪的** ——
//   世界系增量: 世界轴 0.00° / 本地轴偏 52.6~63.1°;
//   本地帧增量: 本地轴 0.00° / 世界轴偏 38.8~76.9°(换姿态最大漂移 87°);
//   Δ=0 时四种写法全部 0.000°(见 source/tools/verify_rotation_mapping.py 第 6 节)。
//   因此"把增量旋转修正为标准坐标系"的唯一自洽做法 = 让旋转与常规(绝对)一致,
//   增量只作用在位置上(与 v16 的结论"位置用增量是对的, 旋转不能"一致)。
enum RotMapMode {
    kRotAbsolute = 0,    // 绝对: 朝向 = 映射后的 Blender 朝向
    kRotRelAligned = 1,  // 相对·标准: 同上(等价武装时对齐基线, Δ=0), 两系同时 1:1
    kRotRelWorld = 2,    // 相对·世界系增量: 世界轴 1:1, 本地轴偏 Δ
    kRotRelLocal = 3     // 相对·本地帧增量: 本地轴 1:1, 世界轴偏 Δ(1.0.0z 语义)
};

static const char* RotMapName(int m) {
    switch (m) {
        case kRotRelAligned: return "相对·标准(与绝对一致, 增量只作用于位置)";
        case kRotRelWorld:   return "相对·世界系增量(世界轴一一对应; 绕相机本地轴会带出滚转)";
        case kRotRelLocal:   return "相对·本地帧增量(本地轴一一对应; 绕世界轴会偏 Δ)";
        default:             return "绝对(Blender 全权决定, 两系一一对应)";
    }
}

int RotMapModeEff() {
    const int ov = g_rotAbsOverride.load();
    if (ov == 1) return kRotAbsolute;
    if (ov == 2) return kRotRelWorld;
    if (ov == 3) return kRotRelLocal;
    if (ov == 0) {
        // 相对: 按 ini 选增量所在系(0 标准/1 世界系/2 本地帧)
        if (g_cfg.pose_rot_increment_frame == 1) return kRotRelWorld;
        if (g_cfg.pose_rot_increment_frame == 2) return kRotRelLocal;
        return kRotRelAligned;
    }
    if (g_cfg.pose_rot_absolute) return kRotAbsolute;
    if (g_cfg.pose_rot_increment_frame == 1) return kRotRelWorld;
    if (g_cfg.pose_rot_increment_frame == 2) return kRotRelLocal;
    return kRotRelAligned;
}

bool RotAbsolute() {
    const int m = RotMapModeEff();
    return m == kRotAbsolute || m == kRotRelAligned;
}

// v1.1: 位置是否以 Blender 的"参考物件"(初始方块)为原点做绝对映射
std::atomic<int> g_anchorOverride{-1};
bool AnchorReference() {
    const int ov = g_anchorOverride.load();
    if (ov == 0) return false;
    if (ov == 1) return true;
    return g_cfg.pose_anchor_reference;
}

// v1.2: 游戏侧原点的来源(角色锚点)与原点修正量
std::atomic<int> g_anchorSrcOverride{-1};
std::atomic<float> g_originDx{0.f}, g_originDy{0.f}, g_originDz{0.f};

// v1.0.0ap: 锚点来源"自动回退"开关(ini pose_anchor_auto_fallback, 默认开; 指令 72 可切)。
//   语义: 首选来源(通常是 vcamFollow)只有在**读到新鲜值**时才用; 一旦连续失效 >0.5 秒,
//   就自动改用引擎相机(游戏机位, 永远可用); 恢复新鲜 >0.3 秒再切回。
//   滞回是为了不让单帧读取失败造成来回跳。实测背景: 干员/角色界面里 vcamFollow 读不到,
//   而旧代码会把 211 秒前、709 米外的粘性缓存当成有效原点 → 相机"找不到界面里的角色"。
std::atomic<int> g_anchorAutoFallback{1};
std::atomic<bool> g_anchorAutoUsingEngine{false};
uint64_t g_anchorAutoFailSinceMs = 0;   // 连续失效的起点(0 = 当前没在失效)
uint64_t g_anchorAutoOkSinceMs = 0;     // 连续新鲜的起点(0 = 当前没在新鲜)
bool AutoFallbackWanted() { return g_anchorAutoFallback.load() == 1; }
bool AnchorAutoUsingEngine() { return g_anchorAutoUsingEngine.load(); }
void AnchorAutoNoteLive(bool live) {
    if (!AutoFallbackWanted()) {
        g_anchorAutoUsingEngine.store(false);
        g_anchorAutoFailSinceMs = 0;
        g_anchorAutoOkSinceMs = 0;
        return;
    }
    const uint64_t now = GetTickCount64();
    if (live) {
        g_anchorAutoFailSinceMs = 0;
        if (g_anchorAutoOkSinceMs == 0) g_anchorAutoOkSinceMs = now;
        if (g_anchorAutoUsingEngine.load() && now - g_anchorAutoOkSinceMs >= 300) {
            // 恢复: 已连续新鲜 0.3 秒 → 切回首选来源
            g_anchorAutoUsingEngine.store(false);
            g_anchorAutoOkSinceMs = 0;
            LogLink("[anchor] 自动恢复: 首选来源重新可读 → 切回该来源(不再用引擎相机)");
        }
    } else {
        g_anchorAutoOkSinceMs = 0;
        if (g_anchorAutoFailSinceMs == 0) g_anchorAutoFailSinceMs = now;
        if (!g_anchorAutoUsingEngine.load() && now - g_anchorAutoFailSinceMs >= 500) {
            // 失效: 已连续失败 0.5 秒 → 改用引擎相机
            g_anchorAutoUsingEngine.store(true);
            g_anchorAutoFailSinceMs = 0;
            LogLink("[anchor] 自动回退: 首选来源连续 0.5 秒读不到新鲜值 → 改用**引擎相机**"
                    "(原点=游戏机位); 它恢复可读后会自动切回");
        }
    }
}

int AnchorSourceSel() {
    const int ov = g_anchorSrcOverride.load();
    if (ov >= 0 && ov < kAnchorSourceCount) return ov;
    return g_cfg.pose_anchor_source;
}

// 最近一帧的位姿样本(供诊断指令 39 打印, 仅 Unity 主线程访问)
BridgePose g_lastSample;
std::atomic<bool> g_lastSampleValid{false};

// v1.3: 探测期间(指令 45/46)强制关闭位姿写入。
// 理由: 测量的是"引擎自己的相机摆位", 虽然钩子读到的是我们写入之前的值, 但让两套写入
// 同时存在没有意义, 也容易在排查时把问题混淆。探测结束自动恢复(见 ProbeFrameTick)。
std::atomic<bool> g_probeLock{false};

// v1.4: 偏移驱动(pose_mode=4)需要的"期望相机世界位置 − 机架零点 P0"。
//   P0 = 武装那一刻的相机位置; 指令 45 实测: SetCameraOffset(0,0,0) 时相机正好落在 P0,
//   且 相机世界位置 = P0 + R·偏移(R 为偏移坐标系, 实测绕世界 X 转约 11°, 见 pose_offset_basis)。
//   于是"位置"可以交给游戏摆(它自己带着机架跟角色走), 我们只负责偏移量。
UnityVector3 g_v4World{};
std::atomic<bool> g_v4Valid{false};
std::atomic<uint64_t> g_v4LastSendMs{0};

// v1.6: 插件面板的"镜头模式"(随角色) —— 由共享内存包的 kFlagFollow 位携带。
//   0 = 定镜头(pose_mode 3), 1 = 随角色(pose_mode 4)。
// 用 -1 表示"本帧还没收到包", 避免首帧就误切。
std::atomic<int> g_followReq{-1};
// v1.0.0ak: 插件面板的"隐藏游戏 UI(拍摄用)" —— 由共享内存包的 kFlagHideUi 位携带。
//   -1 = 本帧还没收到包(首包只记录状态, 不误动作), 0 = 显示, 1 = 请求隐藏。
std::atomic<int> g_hideUiReq{-1};
// 运行时锚点微调量(游戏世界轴), 初值取 ini, 可用指令 48 热改
std::atomic<float> g_anchorOx{0.f}, g_anchorOy{1.4f}, g_anchorOz{0.f};

// v1.0.0m: 锚点**冻结**。
// 用户的思路(也是"定镜头"的正确语义): 武装那一瞬间取一次角色坐标, 之后**不再**用角色坐标
// 干预相机 —— 相机就钉在世界上那一点, 只有 Blender 自己的关键帧会让它动。
// 为什么必须冻结(实测踩到): vcamFollow 在自由相机开着时读不到(读到的是自由相机自己的 vcam,
// Follow 是 null), 可游戏一旦进入它自己的相机模式, 这个值就变得**可读且随角色移动** ——
// 于是每帧实时读取会让"定机位"悄悄变成"跟拍"。
// 随角色模式同样使用冻结值: 偏移量保持常量, "跟随"由游戏自己的机架完成(pose_mode=4)。
std::atomic<bool> g_latchValid{false};
float g_latch[3] = {0.f, 0.f, 0.f};
std::atomic<uint64_t> g_latchMs{0};
int g_latchSrc = 0;

// 定义放在 LogLink / g_enginePos 之后(见下)
bool LatchAnchor(const char* why);

// v9: 「解析目标位姿」与「写入」拆开, 写入时机可选。
//   pose_mode = 1 -> 锚点 CameraManager.TailLateTick 里写(可能被 Cinemachine 覆盖)
//   pose_mode = 3 -> 挂在 CinemachineBrain.LateUpdate 之后写(最后时机, 不会被覆盖)
struct PoseTarget {
    UnityVector3 pos{};
    UnityQuaternion rot{};
    bool rotValid = false;
    float fov = 0.f;
    bool fovValid = false;
    bool posValid = true;  // v1.4: 偏移驱动模式只写朝向, 位置交给游戏(不碰 position)
};
PoseTarget g_target;
std::atomic<bool> g_targetValid{false};
std::atomic<int> g_poseWriteCount{0};

// v11: 相对基线。Blender 场景坐标与游戏世界坐标无关, 绝对套用会把相机搬到空区(虚空)。
// 记录"两侧基线"后按差值搬运: 目标 = 游戏基线 + (Blender当前 - Blender基线)
std::atomic<bool> g_relNeedReset{true};
// 最近一次收到有效帧的时刻。用于区分"偶发丢帧"与"真的断流":
// 只有长时间(>3s)没有新帧才重建基线, 否则会把用户刚做的位移当成新基线吸收掉,
// 表现为"怎么动都不跟随"(实测踩过这个坑)。
std::atomic<uint64_t> g_lastFrameMs{0};

// v14: 由 Cinemachine Brain 钩子在"写入之前"读到的引擎位姿。
// 为什么需要它: 帧内顺序是 TailLateTick(OnGameTick) -> ... -> Brain.LateUpdate(我们写入) -> 渲染。
// 所以在 OnGameTick 里直接读 transform 读到的是"我们自己上一帧写进去的值", 不是引擎位姿,
// 用它建立基线会被污染(实测基线被读成 0.00,0.23,6.91 这种我们自己写的值)。
UnityVector3 g_enginePos{};
UnityQuaternion g_engineRot{};
std::atomic<bool> g_engineValid{false};
// v1.0.0ab-fix: 引擎位姿的"新鲜度"(毫秒时间戳, GetTickCount64)。
// 为什么需要: g_enginePos 只在钩子里、且 pose_mode 为 3/4 时才被刷新。pose_mode 还是 0 的
// 时候它一直是 (0,0,0) —— 此时若允许"引擎相机来源", 就会拿 (0,0,0) 当锚点(实测踩到:
// 武装那一刻把锚点冻结成了 (0,0,0), 来源显示 engineCam)。所以该来源必须过这一关。
std::atomic<uint64_t> g_enginePosMs{0};
bool EngineCamFresh() {
    const uint64_t t = g_enginePosMs.load();
    return t != 0 && (GetTickCount64() - t) < 200;
}

// v1.0.0ad: 逐帧追踪(只读诊断)。背景: 随角色模式"角色走动/跳跃时小幅抖动(幅度 ∝ 速度)"换了
// 锚点来源(引擎相机)依旧存在 → 抖动不在"原点的平滑层", 而在**逐帧位置信号本身**。
// 光靠 60 帧一次的 [diag] 看不到逐帧细节, 所以这里加一个按帧输出的只读追踪:
//   指令 60 a0=帧数(默认 1200) 开始; 指令 61 停止; 帧数用尽自动停。
// 每行记录: 帧序号/距上帧毫秒 + 引擎位姿 + 本帧原点(锚点)与来源 + vcamFollow 读数
//           + 写入目标 + 写入后回读 + 朝向 + 包序号/Blender 帧号。
// 用它可以把"抖动来自锚点 / 引擎相机 / 采样相位 / 写入"一次分开。
UnityVector3 g_diagOrg{};        // 本帧实际使用的原点
int g_diagOrgSrc = -1;           // 原点来源
float g_diagAncUsed[3] = {0.f, 0.f, 0.f};  // 原点实际用到的锚点(扣掉微调量前的那个值)
bool g_diagAncUsedLive = false;  // 那个锚点是实时读到, 还是退回缓存
std::atomic<int> g_traceRemain{0};
int g_traceCount = 0;
uint64_t g_tracePrevMs = 0;
// v1.0.0ae: "写入前补读锚点" 开关(指令 62 a0=1/0, 默认关)。
// 起因(2026-09-14 逐帧追踪实测): 本帧原点用到的锚点 == 上一行追踪记录的锚点(2410/2410 行),
// 即**相机用的角色坐标稳定落后一次调用**, 落后量 = 角色这一帧的位移;
// 而调用间隔本身在抖(实测 0~47ms) → 相机与角色的相位差 = v × 抖动时间 → 幅度 ∝ 速度的抖动。
// 本开关在**写之前**再读一次同一来源的锚点, 用差值把目标位姿补到最新:
//     v += (锚点_晚读 − 锚点_原点所用)
// 打开后若抖动消失, 就证实"锚点晚一帧"就是根因(并顺便成为修法)。
std::atomic<int> g_lateAnchor{0};

// v1.0.0af: 低开销逐帧追踪 —— 循环内**零 I/O**, 结束时一次性落盘。
// 为什么要改: 1.0.0ad/ae 的探针每帧都 snprintf + 写日志, 这段 I/O 恰好夹在"原点用的锚点读取"
// 与"追踪末尾的锚点读取"之间。实测两者之差 = 每次调用位移的**整一倍**(err/step 中位 1.000),
// 而游戏的角色更新是并发的 → 这个"落后一整拍"极可能是**探针自身开销造成的伪影**。
// 改法: 每帧只往内存数组里写一条记录(纯赋值), 帧数用尽/发指令 61 时再批量输出。
constexpr int kTraceCap = 2048;
struct TraceRec {
    uint64_t tUs;                 // QueryPerformanceCounter 微秒
    uint32_t dtUs;                // 距上一条的微秒
    int32_t seqLo;                // 包序号(低 32 位)
    int32_t bf;                   // 包里的 Blender 帧号
    int32_t orgSrc;
    uint8_t liveA1, liveA2, liveA3, lateOn;
    uint8_t pad[4];
    float eng[3], a1[3], a2[3], aU[3], aL[3], a3[3], tgt[3], post[3];
};
TraceRec g_tr[kTraceCap];
int g_trCount = 0;
float g_trA1[3] = {0.f, 0.f, 0.f};   // 调用一开始连读两次(两者之间几乎无工作)
float g_trA2[3] = {0.f, 0.f, 0.f};
bool g_trA1Live = false, g_trA2Live = false;
float g_trAL[3] = {0.f, 0.f, 0.f};   // 写之前的"补读"值(指令 62 打开时才有)
bool g_trALLive = false;
uint64_t g_trPrevUs = 0;
bool TraceWanted() { return g_traceRemain.load() > 0 || g_trCount > 0; }
uint64_t QpcUs() {
    LARGE_INTEGER f, c;
    if (!QueryPerformanceFrequency(&f) || f.QuadPart == 0) return GetTickCount64() * 1000ULL;
    QueryPerformanceCounter(&c);
    return static_cast<uint64_t>(static_cast<double>(c.QuadPart) * 1e6 /
                                 static_cast<double>(f.QuadPart));
}
void TraceDump();
UnityVector3 g_gameBasePos{};
UnityQuaternion g_gameBaseRot{};
UnityVector3 g_blenderBasePos{};
UnityQuaternion g_blenderBaseQuat{};
std::atomic<bool> g_relValid{false};

// ---- 轻量日志(写模块目录 EndfieldCamLink.log + 调试输出) ----
std::string ModuleDir() {
    char path[MAX_PATH];
    if (GetModuleFileNameA(GetModuleHandleA("EndfieldCamLink.dll"), path, MAX_PATH) == 0)
        return std::string();
    std::string dir = path;
    const size_t slash = dir.find_last_of('\\');
    if (slash != std::string::npos) dir.resize(slash);
    return dir;
}

void LogLink(const std::string& msg) {
    OutputDebugStringA(("[EndfieldCamLink] " + msg + "\n").c_str());
    const std::string dir = ModuleDir();
    if (dir.empty()) return;
    std::ofstream f(dir + "\\EndfieldCamLink.log", std::ios::app);
    if (f.is_open()) f << msg << std::endl;
}

// v1.0.0m: 冻结角色锚点 —— 武装那一瞬间取一次, 之后不再用实时角色坐标干预相机。
// (定义放在这里是因为需要 LogLink 与 g_enginePos; 声明在文件上方, 供指令/基线回调调用)
bool LatchAnchor(const char* why) {
    const float eng[3] = {g_enginePos.x, g_enginePos.y, g_enginePos.z};
    int sel = AnchorSourceSel();
    float a[3] = {0.f, 0.f, 0.f};
    int used = -1;
    // v1.0.0ap/aq: 自动回退 —— 配置的来源(通常是 vcamFollow, 或"扫描模式"=武装点 0)如果"读不到
    // 新鲜值"(拿到的是粘性缓存), 本次冻结改用**引擎相机**(游戏自己的机位, 永远可用)。
    // 配置本身不改, 于是回到世界后下一次冻结/每帧读又会用回首选来源。
    // 判据看 GameSysAnchorLastWasLive(), 不看返回值 —— 实时读失败时 GameSysAnchorPosition()
    // 也会返回 true(附缓存值), 正是这次的坑。1.0.0aq 补: **扫描模式(0) 也要覆盖**。
    if (AutoFallbackWanted() && sel != kAnchorEngineCam) {
        bool live = false;
        if (sel == kAnchorArmedPoint) {
            for (int c = kAnchorVcamFollow; c <= kAnchorCamMinusOffset; ++c) {
                float probe[3] = {0.f, 0.f, 0.f};
                if (GameSysAnchorPosition(c, eng, probe) && GameSysAnchorLastWasLive()) {
                    live = true;
                    break;
                }
            }
        } else {
            float probe[3] = {0.f, 0.f, 0.f};
            live = GameSysAnchorPosition(sel, eng, probe) && GameSysAnchorLastWasLive();
        }
        if (!live) {
            if (EngineCamFresh()) {
                char b[288];
                snprintf(b, sizeof(b),
                         "[anchor] 自动回退: %s读不到新鲜值(%s) → 本次冻结改用**引擎相机**"
                         "(游戏机位; 角色锚点恢复后可读时, 下次冻结/每帧读会自动用回来)",
                         sel == kAnchorArmedPoint ? "扫描模式的角色候选" : "首选来源",
                         GameSysAnchorSourceName(sel));
                LogLink(b);
                sel = kAnchorEngineCam;
            } else {
                LogLink("[anchor] 自动回退: 角色候选不新鲜, 且本帧还没有有效引擎位姿 → 退回原逻辑");
            }
        }
    }
    if (sel != kAnchorArmedPoint) {
        if (sel == kAnchorEngineCam && !EngineCamFresh()) {
            // v1.0.0ab-fix: 本帧还没有有效的引擎位姿(pose_mode 还是 0 / 钩子尚未跑过)时,
            // g_enginePos 还是 (0,0,0) —— 用它冻结锚点会得到一个假锚点。显式选了它也不许用。
            static uint64_t warnMs = 0;
            const uint64_t nowMs = GetTickCount64();
            if (nowMs - warnMs > 2000) {
                warnMs = nowMs;
                LogLink("[anchor] 引擎相机来源暂不可用: 本帧还没有有效的引擎位姿"
                        "(pose_mode 需要 3/4) → 本次冻结退回角色候选");
            }
        } else if (GameSysAnchorPosition(sel, eng, a)) {
            used = sel;
        }
    } else {
        // 来源=武装点时, 冻结优先取"角色相关候选"(vcamFollow → ... → camMinusOffset)
        // v1.0.0ab-fix: 扫描范围**不含** engineCam —— 它永远能返回值(哪怕值是未初始化的 0),
        // 一旦被兜底扫描选中, 就会把锚点冻结成 (0,0,0)(实测踩到)。引擎相机只允许显式选择。
        for (int c = kAnchorVcamFollow; c <= kAnchorCamMinusOffset; ++c) {
            if (GameSysAnchorPosition(c, eng, a)) {
                used = c;
                break;
            }
        }
    }
    char b[352];
    if (used < 0) {
        snprintf(b, sizeof(b),
                 "[anchor] 锚点冻结失败(%s): 所有角色候选都取不到值 → 继续沿用上一次的冻结值%s",
                 why, g_latchValid.load() ? "" : "(当前没有可用的冻结值, 退回武装点+修正量)");
        LogLink(b);
        return false;
    }
    g_latch[0] = a[0];
    g_latch[1] = a[1];
    g_latch[2] = a[2];
    g_latchSrc = used;
    g_latchMs.store(GetTickCount64());
    g_latchValid.store(true);
    snprintf(b, sizeof(b),
             "[anchor] *** 锚点已冻结(%s): 来源=%s 值=(%.3f, %.3f, %.3f) | 此后不再用实时角色坐标"
             "干预相机: 定镜头=钉在这点上, 随角色=作为常量偏移交给游戏跟随",
             why, GameSysAnchorSourceName(used), a[0], a[1], a[2]);
    LogLink(b);
    return true;
}

// 读 Vector3/Quaternion 属性(runtime_invoke 返回值类型装箱, 需 unbox)
bool ReadVec3(void* mi, void* tr, float& x, float& y, float& z) {
    void* boxed = Invoke(mi, tr, nullptr);    if (!boxed) return false;
    void* raw = Il2Cpp().object_unbox(boxed);
    if (!raw) return false;
    x = static_cast<float*>(raw)[0];
    y = static_cast<float*>(raw)[1];
    z = static_cast<float*>(raw)[2];
    return true;
}
bool ReadQuat(void* mi, void* tr, float& x, float& y, float& z, float& w) {
    void* boxed = Invoke(mi, tr, nullptr);
    if (!boxed) return false;
    void* raw = Il2Cpp().object_unbox(boxed);
    if (!raw) return false;
    x = static_cast<float*>(raw)[0];
    y = static_cast<float*>(raw)[1];
    z = static_cast<float*>(raw)[2];
    w = static_cast<float*>(raw)[3];
    return true;
}

// v1.5: 四元数 -> 旋转基矩阵(行主序 9 个数)。列 = 本地 X/Y/Z 在世界里的方向。
// 用途: 偏移驱动(pose_mode=4)要把"世界位移"转成"相机本地偏移"(实测偏移就在相机本地系里)。
void QuatToBasis(const UnityQuaternion& q, float R[9]) {
    const float x = q.x, y = q.y, z = q.z, w = q.w;
    const float xx = x * x, yy = y * y, zz = z * z;
    const float xy = x * y, xz = x * z, yz = y * z;
    const float wx = w * x, wy = w * y, wz = w * z;
    R[0] = 1.f - 2.f * (yy + zz); R[1] = 2.f * (xy - wz);       R[2] = 2.f * (xz + wy);
    R[3] = 2.f * (xy + wz);       R[4] = 1.f - 2.f * (xx + zz); R[5] = 2.f * (yz - wx);
    R[6] = 2.f * (xz - wy);       R[7] = 2.f * (yz + wx);       R[8] = 1.f - 2.f * (xx + yy);
}

void* Invoke(void* methodInfo, void* obj, void** params) {
    if (!methodInfo) return nullptr;
    void* exc = nullptr;
    void* result = Il2Cpp().runtime_invoke(methodInfo, obj, params, &exc);
    return exc ? nullptr : result;
}

// ---- Unity Camera 标量属性读/写(装箱返回值需 unbox) ----
float ReadCamFloat(void* mi, void* cam, float def = 0.f) {
    if (!mi || !cam) return def;
    void* boxed = Invoke(mi, cam, nullptr);
    if (!boxed) return def;
    void* raw = Il2Cpp().object_unbox(boxed);
    if (!raw) return def;
    return *static_cast<float*>(raw);
}
bool ReadCamBool(void* mi, void* cam, bool def = false) {
    if (!mi || !cam) return def;
    void* boxed = Invoke(mi, cam, nullptr);
    if (!boxed) return def;
    void* raw = Il2Cpp().object_unbox(boxed);
    if (!raw) return def;
    return *static_cast<uint8_t*>(raw) != 0;
}
bool WriteCamFloat(void* mi, void* cam, float v) {
    if (!mi || !cam) return false;
    float val = v;
    void* p[1] = {&val};
    Invoke(mi, cam, p);
    return true;
}
bool ReadCamVec2(void* mi, void* cam, float& x, float& y) {
    if (!mi || !cam) return false;
    void* boxed = Invoke(mi, cam, nullptr);
    if (!boxed) return false;
    void* raw = Il2Cpp().object_unbox(boxed);
    if (!raw) return false;
    x = static_cast<float*>(raw)[0];
    y = static_cast<float*>(raw)[1];
    return true;
}
bool WriteCamVec2(void* mi, void* cam, float x, float y) {
    if (!mi || !cam) return false;
    struct V2 { float x, y; } v{x, y};
    void* p[1] = {&v};
    Invoke(mi, cam, p);
    return true;
}

// 读取并记录 Unity 相机完整镜头状态(用于"测量而非猜测")
void LogLens(const char* tag) {
    if (!g_handles.ok) return;
    void* cam = Invoke(g_handles.camera_main, nullptr, nullptr);
    if (!cam) {
        LogLink(std::string("[lens] ") + tag + " Camera.main = null");
        return;
    }
    const float fov = ReadCamFloat(g_handles.cam_fov_get, cam, -999.f);
    const bool phys = ReadCamBool(g_handles.cam_phys_get, cam, false);
    const float focal = ReadCamFloat(g_handles.cam_focal_get, cam, -999.f);
    const float ap = ReadCamFloat(g_handles.cam_aperture_get, cam, -999.f);
    const float focus = ReadCamFloat(g_handles.cam_focusdist_get, cam, -999.f);
    float sw = -999.f, sh = -999.f;
    ReadCamVec2(g_handles.cam_sensor_get, cam, sw, sh);
    const float nearC = ReadCamFloat(g_handles.cam_near_get, cam, -999.f);
    const float farC = ReadCamFloat(g_handles.cam_far_get, cam, -999.f);
    char b[384];
    snprintf(b, sizeof(b),
             "[lens] %s fov=%.3f physical=%d focal=%.3f aperture=%.3f focus=%.3f "
             "sensor=(%.2f,%.2f) clip=(%.3f,%.3f)",
             tag, fov, phys ? 1 : 0, focal, ap, focus, sw, sh, nearC, farC);
    LogLink(b);
}

void UdpLoop(uint16_t port) {
    WSADATA wsa;
    if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0) return;
    SOCKET s = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
    if (s == INVALID_SOCKET) {
        WSACleanup();
        return;
    }
    DWORD tv = 200;
    setsockopt(s, SOL_SOCKET, SO_RCVTIMEO, reinterpret_cast<const char*>(&tv),
               sizeof(tv));
    sockaddr_in addr{};
    addr.sin_family = AF_INET;
    addr.sin_port = htons(port);
    addr.sin_addr.s_addr = htonl(INADDR_LOOPBACK);  // 仅本机
    if (bind(s, reinterpret_cast<sockaddr*>(&addr), sizeof(addr)) != 0) {
        closesocket(s);
        WSACleanup();
        return;
    }
    char buf[128];
    while (!g_linkStop.load()) {
        const int n = recv(s, buf, sizeof(buf), 0);
        if (n == 12 && memcmp(buf, "ECMD", 4) == 0) {
            // 运行时指令包: magic(4) + u32 id(4) + float arg(4)
            uint32_t id = 0;
            float arg = 0.f;
            memcpy(&id, buf + 4, 4);
            memcpy(&arg, buf + 8, 4);
            g_pendingCmdArg.store(arg);
            g_cmdA0.store(arg);
            g_cmdA1.store(0.f);
            g_cmdA2.store(0.f);
            g_cmdA3.store(0.f);
            g_pendingCmd.store(static_cast<int>(id));
            continue;
        }
        if (n == 24 && memcmp(buf, "ECM2", 4) == 0) {
            // v2 扩展指令包: magic(4) + u32 id(4) + float a0,a1,a2,a3(16) = 24 字节
            uint32_t id = 0;
            float a0 = 0.f, a1 = 0.f, a2 = 0.f, a3 = 0.f;
            memcpy(&id, buf + 4, 4);
            memcpy(&a0, buf + 8, 4);
            memcpy(&a1, buf + 12, 4);
            memcpy(&a2, buf + 16, 4);
            memcpy(&a3, buf + 20, 4);
            g_cmdA0.store(a0);
            g_cmdA1.store(a1);
            g_cmdA2.store(a2);
            g_cmdA3.store(a3);
            g_pendingCmdArg.store(a0);
            g_pendingCmd.store(static_cast<int>(id));
            continue;
        }
        if (n < static_cast<int>(sizeof(PoseFrame))) continue;
        PoseFrame f;
        memcpy(&f, buf, sizeof(PoseFrame));
        g_latest = f;
        g_lastRecvMs.store(GetTickCount64());
    }
    closesocket(s);
    WSACleanup();
}

// v1.0.0as: 记住最后一次下发的镜头参数 —— **重取快照实例后必须用它们把新实例喂一遍**
// (否则新实例是默认值, 而插件看数值没变就不会再发 = 看起来"参数丢了")。
// 定义必须放在指令处理体之前(指令 78 要用它)。
struct LastLensState {
    float ap = -1.f;
    float focus = -1.f;
    float focal = -1.f;
};
LastLensState g_lastLens;

// 运行时指令执行体(必须在 Unity 主线程调用 -> 由 OnGameTick 触发)
void ExecutePendingCommand() {
    const int id = g_pendingCmd.exchange(0);
    if (id == 0) return;
    const float arg = g_pendingCmdArg.load();
    const float a0 = g_cmdA0.load(), a1 = g_cmdA1.load();
    const float a2 = g_cmdA2.load(), a3 = g_cmdA3.load();
    GameSysResolve();  // 幂等
    switch (id) {
        case 1:  // 置位门控 enableMarketingCamera
            GameSysSetMarketingGate(true);
            break;
        case 2:  // 打开营销相机
            GameSysSetMarketingGate(true);
            GameSysOpenMarketingCamera();
            break;
        case 3:  // 关闭营销相机
            GameSysCloseMarketingCamera();
            break;
        case 4:  // 设置当前相机 FOV (绝对; 负值会被夹到 0, 避免投影矩阵失效变灰)
            GameSysSetOverrideFov(arg < 0.1f ? 0.f : (arg > 179.f ? 179.f : arg));
            break;
        case 5:  // 营销相机 FOV 速率(注意: 该接口是"持续变化速率", 非增量)
            GameSysMarketingChangeFov(arg);
            break;
        case 6:  // 快照相机: 激活
            GameSysSnapshotActivate();
            break;
        case 7:  // 快照相机: 光圈
            GameSysSnapshotSetAperture(arg > 0.f ? arg : 2.8f);
            break;
        case 8:  // 快照相机: 对焦距离
            GameSysSnapshotSetFocusDistance(arg > 0.f ? arg : 10.f);
            break;
        case 9:  // 打印契约状态
            GameSysReport();
            break;
        case 10:  // 刷新实例并打印(只读, 不获取快照相机)
            GameSysRefreshAndReport();
            break;
        case 11:  // 快照相机: FOV 增量
            GameSysSnapshotSetAdditiveFov(arg);
            break;

        // ================= v2 新增: 测量与控制原语 =================
        case 12:  // Unity 相机镜头状态读回(测量, 不改动)
            LogLens("读回");
            break;
        case 13: {  // 设置 Unity 物理相机镜头: a0=usePhysical(>=0 生效) a1=焦距 a2=光圈 a3=对焦距离
            void* cam = Invoke(g_handles.camera_main, nullptr, nullptr);
            if (!cam) {
                LogLink("[lens] 设置失败: Camera.main = null");
                break;
            }
            if (a0 >= 0.f) {
                const uint8_t on = a0 > 0.5f ? 1 : 0;
                uint8_t v = on;
                void* p[1] = {&v};
                Invoke(g_handles.cam_phys_set, cam, p);
                LogLink(std::string("[lens] usePhysicalProperties = ") + (on ? "true" : "false"));
            }
            if (a1 > 0.f) WriteCamFloat(g_handles.cam_focal_set, cam, a1);
            if (a2 > 0.f) WriteCamFloat(g_handles.cam_aperture_set, cam, a2);
            if (a3 > 0.f) WriteCamFloat(g_handles.cam_focusdist_set, cam, a3);
            if (a0 >= 0.f && a0 <= 0.5f && a1 > 0.f) {
                // 关闭物理属性时用"焦距→FOV"换算保证视觉一致(传感器高 24mm 近似)
                const float fovDeg = 2.f * std::atan(24.f / (2.f * a1)) * 57.29578f;
                WriteCamFloat(g_handles.cam_fov_set, cam, fovDeg);
                char b[128];
                snprintf(b, sizeof(b), "[lens] 换算 FOV = %.2f (焦距 %.1fmm)", fovDeg, a1);
                LogLink(b);
            }
            LogLens("写入后");
            break;
        }
        case 14:  // 取快照(拍照)相机实例: CameraManager.CreateOrGetTemporaryController(Type)
            GameSysResolveSnapshotInstance();
            GameSysSnapshotProbe();
            break;
        case 15:  // 快照相机状态探测(只调无参 getter)
            GameSysSnapshotProbe();
            break;
        case 16:  // 景深总开关: a0 = f-stop 或 1
            GameSysEnableDof(a0 > 0.f ? a0 : 1.f);
            break;
        case 17:  // 营销相机模拟移动输入 (a0=x, a1=y)
            GameSysMarketingMoveInput(a0, a1);
            break;
        case 18:  // 营销相机模拟旋转输入 (a0=x, a1=y)
            GameSysMarketingRotateInput(a0, a1);
            break;
        case 19:  // 营销相机 Zoom(delta)
            GameSysMarketingZoom(a0);
            break;
        case 20:  // 安全阀: 回正相机
            GameSysRecenterCamera();
            break;
        case 21:  // 安全阀: 复位相机位置
            GameSysResetCameraPosition();
            break;
        case 22:  // 运行时切换位姿模式: 0=关 1=直接写 transform 2=伺服(未实现)
            g_poseModeOverride.store(static_cast<int>(a0));
            {
                char b[96];
                snprintf(b, sizeof(b), "[cfg] pose_mode 运行时切为 %d", static_cast<int>(a0));
                LogLink(b);
            }
            break;
        case 23:  // 打印 Camera.main transform + 镜头(用于伺服诊断)
        {
            void* cam = Invoke(g_handles.camera_main, nullptr, nullptr);
            if (!cam) {
                LogLink("[state] Camera.main = null");
                break;
            }
            void* tr = Invoke(g_handles.comp_transform, cam, nullptr);
            float px = 0, py = 0, pz = 0, rx = 0, ry = 0, rz = 0, rw = 0;
            if (tr) {
                ReadVec3(g_handles.tr_position_get, tr, px, py, pz);
                ReadQuat(g_handles.tr_rotation_get, tr, rx, ry, rz, rw);
            }
            char b[320];
            snprintf(b, sizeof(b),
                     "[state] cam pos=(%.3f,%.3f,%.3f) quat=(%.4f,%.4f,%.4f,%.4f)",
                     px, py, pz, rx, ry, rz, rw);
            LogLink(b);
            LogLens("state");
            break;
        }
        case 24:  // 直接写 Camera.main.fieldOfView(与游戏 override 路径对照)
        {
            void* cam = Invoke(g_handles.camera_main, nullptr, nullptr);
            if (cam && a0 > 0.1f && a0 < 179.f) {
                WriteCamFloat(g_handles.cam_fov_set, cam, a0);
                char b[112];
                snprintf(b, sizeof(b), "[lens] 直接写 fieldOfView = %.2f", a0);
                LogLink(b);
                LogLens("直写后");
            }
            break;
        }
        case 25:  // 快照相机实例状态 + 尝试一次光圈(若实例可用)
            GameSysRefreshAndReport();
            if (a0 > 0.f) GameSysSnapshotSetAperture(a0);
            break;
        // ================= v3 新增 =================
        case 26:  // 枚举类成员: a0=目标索引 a1=关键字索引 (先发 26 + a0=-1 看索引表)
            if (static_cast<int>(a0) < 0) {
                GameSysEnumListTargets();
            } else {
                GameSysEnumerateTarget(static_cast<int>(a0), static_cast<int>(a1));
            }
            break;
        case 30:  // 打印关键托管方法的签名(参数类型/返回类型)
            GameSysReportSignatures();
            break;
        case 31:  // 一步到位的光圈测试:
                  // 进入拍照模式 -> 拿快照相机实例 -> 激活 -> 设光圈(a0)/对焦(a1)/附加FOV(a2)
            GameSysToggleSnapshotCamera(1.f, 0.f);
            if (a0 > 0.f) GameSysSnapshotSetAperture(a0);
            if (a1 > 0.f) GameSysSnapshotSetFocusDistance(a1);
            if (a2 != 0.f) GameSysSnapshotSetAdditiveFov(a2);
            GameSysSnapshotProbe();
            break;
        case 32:  // 退出拍照模式
            GameSysToggleSnapshotCamera(0.f, 0.f);
            break;
        case 33:  // 快照相机: 变焦倍率 a0 + 读回
            GameSysResolveSnapshotInstance();
            if (a0 > 0.f) GameSysSnapshotSetZoomScale(a0);
            GameSysSnapshotProbe();
            break;
        case 34:  // 重置位姿相对基线(把当前游戏位姿与 Blender 位姿重新对齐为原点)
            g_relNeedReset.store(true);
            g_relValid.store(false);
            LogLink("[baseline] 已请求重置相对基线(下一帧重新建立)");
            break;
        case 35:  // 打印相对基线状态
        {
            char b[320];
            snprintf(b, sizeof(b),
                     "[baseline] valid=%d 游戏 pos=(%.2f,%.2f,%.2f) Blender pos=(%.2f,%.2f,%.2f) "
                     "relative=%d max_offset=%.0f",
                     g_relValid.load() ? 1 : 0, g_gameBasePos.x, g_gameBasePos.y,
                     g_gameBasePos.z, g_blenderBasePos.x, g_blenderBasePos.y,
                     g_blenderBasePos.z, g_cfg.pose_relative ? 1 : 0,
                     g_cfg.pose_max_offset);
            LogLink(b);
            break;
        }
        case 27:  // CameraManager.ToggleSnapshotCamera(a0, a1) —— 参数语义未知的探测
            GameSysToggleSnapshotCamera(a0, a1);
            break;
        case 28:  // 快照相机变焦: SetZoomScale(a0) 与读回
            if (a0 > 0.f) GameSysSnapshotSetZoomScale(a0);
            GameSysSnapshotProbe();
            break;
        case 29:  // 快照相机实例 + FOV 控制组合: a0=光圈(>0 时设置) a1=对焦距离(>0 时)
            GameSysResolveSnapshotInstance();
            if (a0 > 0.f) GameSysSnapshotSetAperture(a0);
            if (a1 > 0.f) GameSysSnapshotSetFocusDistance(a1);
            GameSysSnapshotProbe();
            break;
        case 38: {  // 朝向映射模式 a0: 0=相对(按 ini 的增量系) 1=绝对 2=相对·世界系 3=相对·本地帧
            const int a = (a0 >= 2.5f) ? 3 : (a0 >= 1.5f) ? 2 : (a0 >= 0.5f) ? 1 : 0;
            g_rotAbsOverride.store(a);
            const int m = RotMapModeEff();
            char b[384];
            snprintf(b, sizeof(b),
                     "[rot] 朝向映射已切为 %s | 建议重跑 tools\\arm_lens.ps1 重建基线"
                     "(绝对/标准不依赖基线; 世界系/本地帧两种增量以游戏基线为零点)",
                     RotMapName(m));
            LogLink(b);
            break;
        }
        case 39: {  // v1.1: 打印位置基线诊断(锚点/参考物件/相机与角色的相对位置)
            char b[512];
            char r[224];
            r[0] = 0;
            if (!g_lastSampleValid.load()) {
                snprintf(b, sizeof(b), "[anchor] 还没有收到 Blender 数据");
                LogLink(b);
                break;
            }
            const BridgePose& s = g_lastSample;
            const char* src = g_cfg.shmem ? "shmem" : "udp";
            const bool useRef = AnchorReference() && s.hasRef;
            void* cam = Invoke(g_handles.camera_main, nullptr, nullptr);
            void* tr = cam ? Invoke(g_handles.comp_transform, cam, nullptr) : nullptr;
            float cx = 0, cy = 0, cz = 0, rx = 0, ry = 0, rz = 0, rw = 0;
            if (tr) {
                ReadVec3(g_handles.tr_position_get, tr, cx, cy, cz);
                ReadQuat(g_handles.tr_rotation_get, tr, rx, ry, rz, rw);
            }
            snprintf(b, sizeof(b),
                     "[anchor] 源=%s 锚点=%s 原点来源=%s 原点修正量=(%.2f,%.2f,%.2f) | "
                     "游戏基线点 pos=(%.2f,%.2f,%.2f) | "
                     "Blender相机 pos=(%.2f,%.2f,%.2f) 参考物件 pos=(%.2f,%.2f,%.2f)"
                     " hasRef=%d | 本次位移=(%.2f,%.2f,%.2f) | 当前相机 pos=(%.2f,%.2f,%.2f)",
                     src, useRef ? "参考物件(绝对)" : "Blender相机基线(增量)",
                     GameSysAnchorSourceName(AnchorSourceSel()), g_originDx.load(),
                     g_originDy.load(), g_originDz.load(), g_gameBasePos.x,
                     g_gameBasePos.y, g_gameBasePos.z, s.pos[0], s.pos[1], s.pos[2],
                     s.refPos[0], s.refPos[1], s.refPos[2], s.hasRef ? 1 : 0,
                     useRef ? (s.pos[0] - s.refPos[0]) * g_cfg.pos_scale
                            : (s.pos[0] - g_blenderBasePos.x) * g_cfg.pos_scale,
                     useRef ? (s.pos[1] - s.refPos[1]) * g_cfg.pos_scale
                            : (s.pos[1] - g_blenderBasePos.y) * g_cfg.pos_scale,
                     useRef ? (s.pos[2] - s.refPos[2]) * g_cfg.pos_scale
                            : (s.pos[2] - g_blenderBasePos.z) * g_cfg.pos_scale,
                     cx, cy, cz);
            LogLink(b);
            // 游戏原生"相机相对角色的偏移": 角色位置 = 相机位置 − 偏移(拍照相机就是"角色+偏移"模型)
            float off[3] = {0.f, 0.f, 0.f};
            if (GameSysSnapshotCameraOffset(off)) {
                snprintf(r, sizeof(r),
                         "[anchor] 原生相机偏移=(%.3f,%.3f,%.3f) -> 推导角色位置="
                         "(%.2f,%.2f,%.2f)",
                         off[0], off[1], off[2], cx - off[0], cy - off[1], cz - off[2]);
                LogLink(r);
            } else {
                LogLink("[anchor] 原生相机偏移不可用(接口未解析或快照相机实例未就绪)");
            }
            break;
        }
        case 40: {  // v1.1: 位置锚点切换 a0: 1=参考物件(绝对) 0=Blender相机基线(增量)
            const bool ref = a0 >= 0.5f;
            g_anchorOverride.store(ref ? 1 : 0);
            char b[288];
            snprintf(b, sizeof(b),
                     "[anchor] 位置锚点已切为 %s | 建议重新武装(指令 22 或 arm_lens.ps1)重建游戏基线点",
                     ref ? "参考物件(初始方块): 目标 = 游戏基线点 + (Blender相机 − 参考物件)"
                         : "Blender相机基线(旧): 目标 = 游戏基线点 + (Blender相机 − 相机基线)");
            LogLink(b);
            break;
        }
        case 41: {  // v1.2: 校准原点 —— 把"当前位姿"吸收成原点修正量
            // 手动用法: 武装后把 Blender 相机放到 (0,0,0), 用 Blender 把游戏相机挪到正好与角色
            // 重合, 然后发本指令(a0 任意)。之后 (0,0,0) 就正好落在该位置(角色)上。
            // v1.3 自动用法: a1=1 —— 用当前选定的锚点来源(指令 42)直接算出修正量, 不需要肉眼对齐:
            //           原点修正量 = 锚点(角色) − 武装点
            if (a1 > 0.5f) {
                float a[3] = {0.f, 0.f, 0.f};
                const float eng[3] = {g_enginePos.x, g_enginePos.y, g_enginePos.z};
                int src = AnchorSourceSel();
                if (src == kAnchorArmedPoint) {
                    // v1.0.0l: 原点来源=武装点时, 自动校准**也要能用** ——
                    // 直接挑"角色相关候选"里第一个可用的(vcamFollow → vcamLookAt),
                    // 读不到实时值就吃粘性缓存。理由: source=0 + 原点修正量是最稳的运行时配置
                    // (不依赖运行时能否读到锚点), 而修正量恰恰需要由锚点算出来。
                    for (int c = kAnchorVcamFollow; c <= kAnchorVcamLookAt; ++c) {
                        if (GameSysAnchorPosition(c, eng, a)) {
                            src = c;
                            break;
                        }
                    }
                    if (src == kAnchorArmedPoint) {
                        LogLink("[origin] 自动校准失败: 两个 vcam 候选都取不到值"
                                "(vcamFollow/vcamLookAt); 可先用指令 43 看候选, 或改用手动校准");
                        break;
                    }
                    char h[192];
                    snprintf(h, sizeof(h),
                             "[origin] 原点来源=武装点(修正量模式), 自动校准改用候选 %s 的读数",
                             GameSysAnchorSourceName(src));
                    LogLink(h);
                } else if (!GameSysAnchorPosition(src, eng, a)) {
                    LogLink(std::string("[origin] 自动校准失败: 候选 ") +
                            GameSysAnchorSourceName(src) + " 当前不可用");
                    break;
                }
                // 注意: 这里**不加**锚点微调量 —— 微调量在 org 处统一叠加一次
                // (org = 原点 + 修正量 + 微调量), 加到这里会重复计入。
                const float dx = a[0] - g_gameBasePos.x;
                const float dy = a[1] - g_gameBasePos.y;
                const float dz = a[2] - g_gameBasePos.z;
                g_originDx.store(dx);
                g_originDy.store(dy);
                g_originDz.store(dz);
                char b[448];
                snprintf(b, sizeof(b),
                         "[origin] 已自动校准(来源=%s, 锚点微调=(%.2f, %.2f, %.2f)): 锚点=(%.3f, "
                         "%.3f, %.3f) 武装点=(%.3f, %.3f, %.3f) → 原点修正量=(%.3f, %.3f, %.3f) | "
                         "持久化: pose_origin_dx=%.3f / dy=%.3f / dz=%.3f",
                         GameSysAnchorSourceName(src), g_anchorOx.load(), g_anchorOy.load(),
                         g_anchorOz.load(), a[0], a[1], a[2], g_gameBasePos.x,
                         g_gameBasePos.y, g_gameBasePos.z, dx, dy, dz, dx, dy, dz);
                LogLink(b);
                break;
            }
            if (!g_lastSampleValid.load()) {
                LogLink("[origin] 还没有收到 Blender 数据, 无法校准");
                break;
            }
            const BridgePose& s = g_lastSample;
            const bool useRef = AnchorReference() && s.hasRef;
            float dx = 0.f, dy = 0.f, dz = 0.f;
            if (useRef) {
                dx = (s.pos[0] - s.refPos[0]) * g_cfg.pos_scale;
                dy = (s.pos[1] - s.refPos[1]) * g_cfg.pos_scale;
                dz = (s.pos[2] - s.refPos[2]) * g_cfg.pos_scale;
            } else {
                dx = (s.pos[0] - g_blenderBasePos.x) * g_cfg.pos_scale;
                dy = (s.pos[1] - g_blenderBasePos.y) * g_cfg.pos_scale;
                dz = (s.pos[2] - g_blenderBasePos.z) * g_cfg.pos_scale;
            }
            // v1.6: 手动校准要减掉锚点微调量, 才能与自动校准得到同一语义
            // (目标位置 = 原点 + 修正量 + 微调量)。
            dx -= g_anchorOx.load();
            dy -= g_anchorOy.load();
            dz -= g_anchorOz.load();
            g_originDx.store(dx);
            g_originDy.store(dy);
            g_originDz.store(dz);
            {
                char b[384];
                snprintf(b, sizeof(b),
                         "[origin] 已校准: 原点修正量=(%.3f, %.3f, %.3f) 游戏单位 | "
                         "把 Blender 相机放回 (0,0,0) 后, 游戏相机就落在你刚才对准的位置 | "
                         "持久化: ini 里写 pose_origin_dx=%.3f / dy=%.3f / dz=%.3f",
                         dx, dy, dz, dx, dy, dz);
                LogLink(b);
            }
            break;
        }
        case 42: {  // v1.2: 选择"游戏侧原点"的来源(角色锚点) a0: 0..5 (5=引擎相机, v1.0.0ab)
            int src = static_cast<int>(a0 + 0.5f);
            if (src < 0 || src >= kAnchorSourceCount) src = 0;
            g_anchorSrcOverride.store(src);
            char b[320];
            snprintf(b, sizeof(b),
                     "[anchor] 游戏侧原点来源已切为 %s (%s) | 建议发指令 43 看候选坐标, "
                     "并重新武装一次让基线干净",
                     GameSysAnchorSourceName(src), GameSysAnchorSourceLabel(src));
            LogLink(b);
            break;
        }
        case 49:  // v1.0.0m: 重新冻结角色锚点(武装脚本在"自由相机还没开"时调用, 拿到最新鲜的值)
            LatchAnchor("指令 49");
            break;
        case 60: {  // v1.0.0ad/af: 低开销逐帧追踪开始 a0=帧数(默认 1200, 上限 2048)
            int n = static_cast<int>(a0 + 0.5f);
            if (n <= 0 || n > kTraceCap) n = 1200;
            g_trCount = 0;
            g_trPrevUs = 0;
            g_traceCount = 0;
            g_tracePrevMs = 0;
            g_traceRemain.store(n);
            char b[288];
            snprintf(b, sizeof(b),
                     "[tr] 逐帧追踪开始: %d 帧(内存缓冲版, 循环内零 I/O; 约 %.1f 秒 @120fps)。"
                     "现在让角色走动/跳跃, 结束后发指令 61(或缓冲写满自动落盘)。",
                     n, n / 120.0);
            LogLink(b);
            break;
        }
        case 61: {  // v1.0.0ad: 逐帧追踪停止(并把缓冲落盘)
            const int left = g_traceRemain.exchange(0);
            char b[192];
            snprintf(b, sizeof(b), "[tr] 逐帧追踪停止, 输出缓冲(剩余 %d 帧未采集)", left);
            LogLink(b);
            TraceDump();
            break;
        }
        case 62: {  // v1.0.0ae: "写入前补读锚点"开关 a0=1 开 / 0 关(默认关)
            const int on = (a0 > 0.5f) ? 1 : 0;
            g_lateAnchor.store(on);
            char b[256];
            snprintf(b, sizeof(b),
                     "[anchor] 写入前补读锚点: %s | 目的: 消掉\"相机用的角色坐标落后一次调用\""
                     "造成的相位抖动(逐帧追踪实测 2410/2410 行都是落后的)。",
                     on ? "开" : "关");
            LogLink(b);
            break;
        }
        case 48: {  // v1.6: 锚点微调量(游戏世界轴) a0=x a1=y a2=z
            g_anchorOx.store(a0);
            g_anchorOy.store(a1);
            g_anchorOz.store(a2);
            char b[224];
            snprintf(b, sizeof(b),
                     "[anchor] 锚点微调量已设为 (%.3f, %.3f, %.3f) | 建议重新自动校准"
                     "(指令 41 a0=0 a1=1)让它生效到原点修正量里",
                     a0, a1, a2);
            LogLink(b);
            break;
        }
        case 43: {  // v1.2: 打印全部锚点候选(找角色位置)
            {
                char b[416];
                snprintf(b, sizeof(b),
                         "[anchor] 当前来源=%s (%s) | 原点修正量=(%.3f, %.3f, %.3f) | "
                         "武装位置=(%.2f, %.2f, %.2f)",
                         GameSysAnchorSourceName(AnchorSourceSel()),
                         GameSysAnchorSourceLabel(AnchorSourceSel()), g_originDx.load(),
                         g_originDy.load(), g_originDz.load(), g_gameBasePos.x, g_gameBasePos.y,
                         g_gameBasePos.z);
                LogLink(b);
            }
            {
                char b[352];
                if (g_latchValid.load()) {
                    snprintf(b, sizeof(b),
                             "[anchor] 冻结锚点(v1.0.0m, 相机原点用它)= (%.3f, %.3f, %.3f) "
                             "来源=%s 冻结于 %.1f 秒前 | 想要更新请发指令 49",
                             g_latch[0], g_latch[1], g_latch[2],
                             GameSysAnchorSourceName(g_latchSrc),
                             (GetTickCount64() - g_latchMs.load()) / 1000.0);
                } else {
                    snprintf(b, sizeof(b),
                             "[anchor] 冻结锚点= 无(退回 来源/修正量 那条路) | 发指令 49 可冻结");
                }
                LogLink(b);
            }
            {
                const float eng[3] = {g_enginePos.x, g_enginePos.y, g_enginePos.z};
                GameSysReportAnchorCandidates(eng);
            }
            break;
        }
        case 44:  // v1.3: 类型体检 —— 关键 getter 的声明类型 vs 实际对象类(含父类链)
            GameSysTypeAudit();
            break;
        case 45:  // v1.3: SetCameraOffset 写入-测量标定
                  //   a0: 0=两种调用约定都跑(默认) 1=只跑装箱 2=只跑裸值指针
                  //   a1: 每步稳定帧数(默认 45)
            {
                const int cm = static_cast<int>(a0 + 0.5f);
                const int st = static_cast<int>(a1 + 0.5f);
                // 快照相机实例是 SetCameraOffset 的载体; 未就绪时会顺手取一次(会进拍照模式, 有日志)
                GameSysResolveSnapshotInstance();
                GameSysOffsetCalibBegin(cm, st);
            }
            break;
        case 46:  // v1.3: 锚点候选采样器 a0=采样帧数(默认 600)
            GameSysAnchorSampleBegin(static_cast<int>(a0 + 0.5f));
            break;
        case 47:  // v1.3: 打印 SetCameraOffset 签名 + 一次调用约定干跑(不写任何值)
            GameSysReportSetOffsetSignature();
            GameSysSnapshotProbe();
            break;
        // ================= v1.0.0ai: 隐藏游戏 UI 探测 =================
        // 约束: C1 不进原生相机模式 / C2 不改键位 / C3 攻击可用 / C4 不碰 UID 水印
        // => 只动渲染层(相机 cullingMask、图层), 不动 UI 的逻辑状态。
        case 63: {  // UI 符号清单(只读元数据): 导出到 模块目录\EndfieldCamLink_ui_inventory.log
            const std::string dir = ModuleDir();
            if (dir.empty()) {
                LogLink("[ui] 指令63 失败: 取不到模块目录");
                break;
            }
            const std::string p = dir + "\\EndfieldCamLink_ui_inventory.log";
            LogLink("[ui] 指令63: 开始导出 UI 符号清单 -> " + p);
            RunInventorySet(p, kInvUi);
            LogLink("[ui] 指令63: UI 符号清单导出完成(文件里以 '=== 枚举结束 ===' 收尾)");
            break;
        }
        case 64:  // UI 体检(只读: 相机清单 + UI/图层成员签名 + 相机 getter 候选)
            GameSysUiAudit();
            break;
        case 65:  // 渲染层写入-测量
                  //   a0: 1=写入 0=立即还原(默认)
                  //   a1: 目标 0=自动找 UI 相机(默认) 1=Camera.main(对照) 2=非 main 的第一台
                  //   a2: 自动还原等待秒数(默认 3, 范围 0.5~60)
                  //   a3: v1.0.0ar 要写入的遮罩值(0 = 旧行为: 写 0 = 该相机彻底不渲染)。
                  //       用途: 验证"ESC 界面里那幅世界画面在哪个位" —— 例如写 1024(位10 UIPP)。
                  //       注意浮点精度: 位掩码请用"少数几个高位"(如 1024/1056), 32 位满掩码会被浮点截断。
            GameSysUiTryCullMask(static_cast<int>(a0 + 0.5f),
                                 static_cast<int>(a1 + 0.5f), a2,
                                 static_cast<int>(a3 + 0.5f));
            break;
        case 66:  // v1.0.0ak: 隐藏 UI 的策略 —— a0=1 严格保持(连 ESC 菜单也藏) / 0 尊重游戏(默认)
            GameSysUiSetStrict(static_cast<int>(a0 + 0.5f));
            GameSysUiReportState();
            break;
        case 67:  // v1.0.0ak: 打印隐藏 UI 的当前状态(只读)
            GameSysUiReportState();
            break;
        // ================= v1.0.0am: "完全去掉 UI"探测轮 =================
        case 70:  // 图层与相机体检(只读): 图层名表 + 各相机遮罩(逐位带名字) + 默认值 + UI模型/卡池层
            GameSysUiReportLayers();
            break;
        case 71:  // 主(世界)相机图层写-测
                  //   a0 = 要清掉的层位掩码(0 = 取消该设置)
                  //   a1 = >0 一次性测试(秒, 到期自动还原); <=0 设为持续(勾选隐藏 UI 时一并生效)
            GameSysUiWorldClearSet(static_cast<int>(a0 < 0 ? a0 - 0.5f : a0 + 0.5f),
                                   static_cast<float>(a1));
            break;
        case 72:  // v1.0.0ap: 锚点来源"自动回退"开关(a0=1 开(默认) / 0 关) —— 只读诊断见指令 43
        {
            const int on = static_cast<int>(a0 + 0.5f) != 0 ? 1 : 0;
            g_anchorAutoFallback.store(on);
            g_anchorAutoUsingEngine.store(false);
            g_anchorAutoFailSinceMs = 0;
            g_anchorAutoOkSinceMs = 0;
            if (on) {
                LogLink("[anchor] 自动回退已开启: 首选来源读不到新鲜值时自动改用引擎相机, 恢复后自动切回");
            } else {
                LogLink("[anchor] 自动回退已关闭: 首选来源读不到时沿用粘性缓存值(旧行为)");
            }
            break;
        }
        case 73:  // v1.0.0ar: 主(世界)相机保活 —— 不让游戏在 ESC/时停时把世界相机彻底关掉
                  //   a0 = 1 开(持久) / 0 关
                  //   a1 > 0 一次性测试秒数(到期自动还原)
                  //   a2 != 0 目标遮罩覆盖(诊断)
                  //   a3 = 1 激进(缺世界位就按回) / 0 保守(仅主相机=0 时按回)
            GameSysUiWorldKeepSet(static_cast<int>(a0 + 0.5f), static_cast<float>(a1),
                                  static_cast<int>(a2 + 0.5f), static_cast<int>(a3 + 0.5f));
            break;
        case 74:  // v1.0.0ar: UI 相机"清屏 / 启用"写-测 —— 把"世界没渲染"和"被清屏擦掉"分开
                  //   a0 = 0 还原 / 1 写 clearFlags / 2 写 enabled=false / 3 两个都写
                  //   a1 = >=1 一次性测试秒数(到期自动还原); <1 持续
                  //   a2 = clearFlags 目标值(0/缺省 = 3 Depth: 只清深度不擦颜色)
                  // 场景: 严格隐藏(UI 相机遮罩 0) + 保活(主相机强制渲染) 时若仍全黑, 发本条写 Depth;
                  //       世界画面若露出来 -> 说明全黑是**清屏**造成的, 而"屏蔽 ESC 后强制渲染世界"可行。
            GameSysUiCamPropSet(static_cast<int>(a0 + 0.5f), static_cast<float>(a1),
                                static_cast<int>(a2 + 0.5f));
            break;
        case 75:  // v1.0.0as: 景深(DOF)链路体检(**只读**) —— 光圈为什么只在官方相机里生效
                  //   a0 = 0(默认) 只跑已验证安全的路径(方法枚举/类链/是否值类型)
                  //   a0 = 1 额外列出字段**名字**(仍不读值; 第一版在字段取值处崩过, 现单独隔离)
            GameSysDofAudit(static_cast<int>(a0 + 0.5f));
            break;
        case 76:  // v1.0.0as: 景深"通道验证" —— 在自由相机里开一次景深(一次性, 到期自动还原)
                  //   a0 = 1 无参构造(a1=保持秒数) / 3 六float全设成 a1(a2=保持秒数)
                  //        4 槽位试探(a1=该槽位值, a2=槽位0..5, a3=其余值) / 0 还原 / 2 兜底关
            GameSysDofChanTest(static_cast<int>(a0 + 0.5f), static_cast<float>(a1),
                               static_cast<float>(a2), static_cast<float>(a3));
            break;
        case 77:  // v1.0.0as: 光圈"落地"测试 —— SetAperture/Focus 之后**主动 Apply**(关键假设)
                  //   a0 = fStop(>0 时设置) / a1 = 对焦距离(>0 时设置) / a2 = 1 调 Apply / a3 = 1 先 Reset
            GameSysDofApplyTest(static_cast<float>(a0), static_cast<float>(a1),
                                static_cast<int>(a2 + 0.5f), static_cast<int>(a3 + 0.5f));
            break;
        case 78:  // v1.0.0as: **一键修复虚化** —— 重取快照相机实例(旧实例会陈旧) + 喂回参数 + Apply
        {
            // 用户 2026-09-15 实测: 进一次官方相机再退出后, 我们缓存的那个快照相机实例就"陈旧"了
            //   (SetAperture 不报错但毫无视觉效果); 重新取一次实例(= 武装的第一步)立刻恢复 ✓
            LogLink("[lens] 修复虚化: 重新取快照相机实例 + 把当前镜头参数喂回 + Apply ...");
            GameSysToggleSnapshotCamera(1.f, 0.f);  // 内部已做 Activate + Apply
            char b[208];
            if (g_lastLens.ap > 0.f) {
                GameSysSnapshotSetAperture(g_lastLens.ap);
                if (g_lastLens.focus > 0.f) GameSysSnapshotSetFocusDistance(g_lastLens.focus);
                GameSysSnapshotApplyDof();
                snprintf(b, sizeof(b),
                         "[lens] 修复虚化: 已把 f/%.2f + 对焦 %.2fm 喂给新实例并 Apply",
                         g_lastLens.ap, g_lastLens.focus);
            } else {
                snprintf(b, sizeof(b),
                         "[lens] 修复虚化: 新实例已就绪; 但还没有从 Blender 收到过镜头参数"
                         "(动一下光圈即可)");
            }
            LogLink(b);
            break;
        }
        default:
            break;
    }
    char b[96];
    snprintf(b, sizeof(b), "[cmd] 已执行指令 id=%d arg=%.3f [%.2f %.2f %.2f]", id, arg,
             a0, a1, a2, a3);
    LogLink(b);
}

}  // namespace

void StartLink(const LinkConfig& cfg) {
    g_cfg = cfg;
    g_originDx.store(cfg.pose_origin_dx);
    g_originDy.store(cfg.pose_origin_dy);
    g_originDz.store(cfg.pose_origin_dz);
    g_anchorOx.store(cfg.pose_anchor_ox);
    g_anchorOy.store(cfg.pose_anchor_oy);
    g_anchorOz.store(cfg.pose_anchor_oz);
    // v1.0.0ah: 写入时刻补读锚点 —— **默认开启**(ini: pose_late_anchor, 默认 true)。
    // 依据: 目标在帧内早期(TailLateTick)读锚点、帧末(Brain 之后)才写相机, 相位差 = v × 帧时间,
    // 而帧时间在抖 → 幅度 ∝ 速度的抖; 写入前补读后实测 |aL−a3| = 0.0000 m。运行时可用指令 62 切换。
    g_lateAnchor.store(cfg.pose_late_anchor ? 1 : 0);
    // v1.0.0ap: 锚点来源自动回退(默认开) —— 详见 g_anchorAutoFallback 处的说明。
    g_anchorAutoFallback.store(cfg.pose_anchor_auto_fallback ? 1 : 0);
    if (!cfg.pose_anchor_auto_fallback) {
        LogLink("[anchor] 自动回退已关闭(ini pose_anchor_auto_fallback=false): 首选来源读不到时"
                "仍会沿用粘性缓存值");
    }
    // v1.0.0am: "隐藏游戏 UI"两档默认值 —— 策略(全藏/尊重游戏)与"世界相机要清掉的层"。
    // 策略默认 true(严格保持): 战斗中游戏频繁往 UI 相机配置栈加层, 尊重游戏会让隐藏悄悄失效
    // (用户反馈的现象: 体力条/怪条又出现)。世界清位默认 0(不动主相机), 用指令 70/71 定出层号再填。
    GameSysUiSetStrict(cfg.ui_hide_strict ? 1 : 0);
    if (cfg.ui_hide_maincam_clear_bits != 0) {
        char b[192];
        snprintf(b, sizeof(b), "[ui] ini: ui_hide_maincam_clear_bits=0x%08X -> 隐藏时一并清掉这些层",
                 static_cast<unsigned>(cfg.ui_hide_maincam_clear_bits));
        LogLink(b);
        GameSysUiWorldClearSet(cfg.ui_hide_maincam_clear_bits, 0.f);
    }
    // v1.0.0ar: 主(世界)相机保活 —— 按 ESC 时游戏会把主相机遮罩写成 0(整个世界不画), 那时
    // "隐藏 UI"就等于把屏幕唯一的产出源也藏了(黑屏)。保活开着时, 帧末发现世界被关掉就按回去。
    if (cfg.ui_hide_world_keepalive) {
        LogLink("[ui] ini: ui_hide_world_keepalive=true -> 游戏关掉世界相机时会被按回"
                "(ESC 时也能看到时停的画面; 指令 73 可关)");
        GameSysUiWorldKeepSet(1, 0.f, 0, 0);
    }
    // v1.0.0ar: 隐藏 UI 时禁用 UI 相机 —— 只压遮罩不够: **遮罩=0 的相机照样清屏**,
    // 按 ESC 时它会把主相机刚画好的世界擦黑(用户实测: 加这一步后世界画面立刻露出来)。
    GameSysUiSetDisableUiCam(cfg.ui_hide_disable_uicam ? 1 : 0);
    if (g_udpRunning.load()) return;
    g_linkStop.store(false);
    g_udpThread = std::thread([cfg] { UdpLoop(cfg.port); });
    g_udpThread.detach();
    g_udpRunning.store(true);
}

bool HandlesReady() { return g_handles.ok; }

// v6: 把 Blender 镜头参数完整应用到游戏相机(全部走已验证的接口)
//   焦距  -> CameraManager.SetOverrideFOVForCurrCamera(绝对 FOV)   [实测生效]
//   光圈  -> SnapshotCameraController.SetAperture(fStop)          [实测 f/1.4 虚化 / f/16 清晰]
//   对焦距 -> SnapshotCameraController.SetFocusDistance(m)        [实测调用无异常]
// 快照相机实例: 优先 Unity 查找, 其次 ToggleSnapshotCamera 的返回值。
void ApplyShmemLens(const BridgePose& bp) {
    // ---- v1.0.0as: **镜头写入节流**(修用户实测的"动光圈/动参数时画面抽动") ----
    // 现象(2026-09-15 用户反馈): 拖动光圈/焦距时画面会抽。日志实证: 一秒内写了 6~7 次
    //   `SetAperture` + `ApplySnapshotDofSettings`, 每次都让景深重设一下 → 看着就是抽。
    // 做法: 镜头参数不是逐帧数据, 统一 **400ms 一次**(最多 2.5 次/秒); 并把"算变化"的门槛抬高到
    //   肉眼无感的量级(焦距 0.3mm / 光圈 0.05 / 对焦 0.02m), 免得拖动时每个 tick 都算"变了"。
    static uint64_t lastLensMs = 0;
    const uint64_t nowLens = GetTickCount64();
    if (lastLensMs != 0 && nowLens - lastLensMs < 400) return;
    lastLensMs = nowLens;

    // --- 焦距 -> FOV ---
    if (bp.focalLength > 0.5f && fabsf(bp.focalLength - g_lastLens.focal) > 0.3f) {
        g_lastLens.focal = bp.focalLength;
        const float sensorH = bp.sensorH > 0.5f ? bp.sensorH : 24.f;
        const float fovDeg = 2.f * static_cast<float>(std::atan(sensorH / (2.f * bp.focalLength))) *
                             57.29578f;
        if (fovDeg > 0.5f && fovDeg < 179.f) {
            GameSysSetOverrideFov(fovDeg);
            char b[176];
            snprintf(b, sizeof(b),
                     "[lens] 焦距 %.2fmm + 传感器高 %.2fmm -> FOV %.2f (已应用)", bp.focalLength,
                     sensorH, fovDeg);
            LogLink(b);
        }
    }

    // --- 光圈 / 对焦距离 -> 快照相机景深 ---
    const bool wantAperture = bp.fStop > 0.f && fabsf(bp.fStop - g_lastLens.ap) > 0.05f;
    const bool wantFocus =
        bp.focusDistance > 0.f && fabsf(bp.focusDistance - g_lastLens.focus) > 0.02f;
    // ---- v1.0.0as: **景深保活**(必须放在下面的早退之前!) ----
    // 实测根因: SetAperture/SetFocusDistance 只写数值, 必须 ApplySnapshotDofSettings() 才把景深
    //   激活并挂到主相机; 而"进一次官方相机再退出"会把这份激活冲掉(用户现象)。
    //   保活 = 每 3 秒重施加一次, 把冲掉的激活救回来 —— 与"数值有没有变化"无关, 所以**不能放在早退之后**
    //   (第一版就是放在早退之后, 于是数值不变时根本走不到, 日志里一条保活都没有)。
    if (g_cfg.lens_dof_keepalive && GameSysSnapshotInstanceReady()) {
        static uint64_t lastDofApply = 0;
        static int dofApplyCount = 0;
        const uint64_t nowMs = GetTickCount64();
        if (lastDofApply == 0 || nowMs - lastDofApply >= 3000) {
            lastDofApply = nowMs;
            if (GameSysSnapshotApplyDof()) {
                ++dofApplyCount;
                if (dofApplyCount <= 3 || (dofApplyCount % 100) == 0) {
                    char b[208];
                    snprintf(b, sizeof(b),
                             "[lens] 景深保活: 第 %d 次重施加(每 3 秒一次; 关掉用 ini "
                             "lens_dof_keepalive=false)",
                             dofApplyCount);
                    LogLink(b);
                }
            }
        }
    }
    if (!wantAperture && !wantFocus) return;
    // 先记录"已见到该值", 避免实例未就绪时每帧重复尝试并刷屏日志
    if (wantAperture) g_lastLens.ap = bp.fStop;
    if (wantFocus) g_lastLens.focus = bp.focusDistance;

    // 重要(修复): 这里绝不主动去"获取"快照相机实例。
    // 早期版本在此调用实例解析, 结果每帧都去枚举场景对象, 内存耗尽导致引擎崩溃
    // (Player.log: [Critical] Commit memory failed)。
    // 数据流只读缓存; 获取实例只能由显式指令(27 / arm_lens.ps1)触发。
    if (!GameSysSnapshotInstanceReady()) {
        LogLink("[lens] 光圈/对焦本次未应用: 快照相机实例未就绪"
                " (进入游戏世界后运行 tools\\arm_lens.ps1 或发送指令 27)");
        return;
    }
    if (wantAperture) {
        GameSysSnapshotSetAperture(bp.fStop);
        char b[144];
        snprintf(b, sizeof(b), "[lens] 光圈 f/%.2f (来自 Blender) 已应用", bp.fStop);
        LogLink(b);
    }
    if (wantFocus) {
        GameSysSnapshotSetFocusDistance(bp.focusDistance);
        char b[144];
        snprintf(b, sizeof(b), "[lens] 对焦距离 %.3f m (来自 Blender) 已应用", bp.focusDistance);
        LogLink(b);
    }
    // ---- v1.0.0as: **景深落地 + 保活**(用户 2026-09-15 的"光圈只在官方相机里生效"的根因修复) ----
    // 实测(指令 77 双向对照): SetAperture/SetFocusDistance 只写数值, 必须 ApplySnapshotDofSettings()
    //   才把景深**激活并挂到主相机**; 激活一旦被游戏冲掉(典型: 进一次官方相机再退出), 我们的数值
    //   更新就再也看不见效果 —— 这就是用户看到的现象。所以: 改数值时补一次 Apply, 并周期性重施加。
    if (wantAperture || wantFocus) {
        if (GameSysSnapshotApplyDof()) LogLink("[lens] 景深已落地(ApplySnapshotDofSettings)");
    }
}

// v11: 采集相对基线。必须在"我们本帧还没写入"时调用, 读到的才是引擎给出的位姿。
// (实测每帧 Brains 覆盖后引擎值都会恢复, 因此只要在写入之前读就是干净的。)
static bool CapturePoseBaseline(const UnityVector3& blenderPos,
                                const UnityQuaternion& blenderQuat) {
    if (g_engineValid.load()) {
        // 首选: Brain 钩子在写入前读到的引擎位姿(干净)
        g_gameBasePos = g_enginePos;
        g_gameBaseRot = g_engineRot;
    } else {
        // 退化: 直接读 transform。注意此时读到的可能是我们自己的旧写入, 仅作兜底。
        void* cam = Invoke(g_handles.camera_main, nullptr, nullptr);
        void* tr = cam ? Invoke(g_handles.comp_transform, cam, nullptr) : nullptr;
        if (!tr) return false;  // 下一帧再试
        ReadVec3(g_handles.tr_position_get, tr, g_gameBasePos.x, g_gameBasePos.y,
                 g_gameBasePos.z);
        ReadQuat(g_handles.tr_rotation_get, tr, g_gameBaseRot.x, g_gameBaseRot.y,
                 g_gameBaseRot.z, g_gameBaseRot.w);
    }
    g_blenderBasePos = blenderPos;
    g_blenderBaseQuat = blenderQuat;
    g_relValid.store(true);
    g_relNeedReset.store(false);
    // v1.0.0m: 武装瞬间(重新建立基线时)冻结角色锚点 —— 之后不再用实时角色坐标干预相机。
    LatchAnchor("武装建立基线");
    char b[384];
    snprintf(b, sizeof(b),
             "[baseline] 已建立相对基线(引擎位姿来源=%s) | 游戏 pos=(%.2f,%.2f,%.2f) "
             "rot(%.3f,%.3f,%.3f,%.3f) | Blender pos=(%.2f,%.2f,%.2f)",
             g_engineValid.load() ? "Brain钩子" : "直接读取(可能含旧写入)",
             g_gameBasePos.x, g_gameBasePos.y, g_gameBasePos.z, g_gameBaseRot.x,
             g_gameBaseRot.y, g_gameBaseRot.z, g_gameBaseRot.w, g_blenderBasePos.x,
             g_blenderBasePos.y, g_blenderBasePos.z);
    LogLink(b);
    // v16: 打印"游戏基线朝向"与"映射后的 Blender 基线朝向"之间的夹角。
    // 相对模式的"世界系/本地帧"两种增量以游戏基线为零点, 这个角 Δ 就是造成
    // "绕 xyz 旋转不像标准直角坐标系"的常量错位(有一个系必被它转过);
    // 绝对/相对·标准(默认)不受它影响(令 Δ=0), 因此这里也顺带说明当前模式。
    {
        const float dot = fabsf(g_gameBaseRot.w * blenderQuat.w + g_gameBaseRot.x * blenderQuat.x +
                                g_gameBaseRot.y * blenderQuat.y + g_gameBaseRot.z * blenderQuat.z);
        const float cl = dot > 1.f ? 1.f : dot;
        const float offDeg = 2.f * static_cast<float>(std::acos(cl)) * 57.29578f;
        const BridgePose& s = g_lastSample;
        const bool useRef = AnchorReference() && s.hasRef;
        char c[560];
        snprintf(c, sizeof(c),
                 "[map] 朝向=%s(与游戏基线朝向相差 %.1f°) | 位置锚点=%s | "
                 "参考物件 pos=(%.2f,%.2f,%.2f) hasRef=%d | 游戏基线点 pos=(%.2f,%.2f,%.2f)",
                 RotMapName(RotMapModeEff()),
                 offDeg,
                 useRef ? "参考物件(初始方块, 绝对)"
                        : "Blender相机基线(增量)",
                 s.refPos[0], s.refPos[1], s.refPos[2], s.hasRef ? 1 : 0, g_gameBasePos.x,
                 g_gameBasePos.y, g_gameBasePos.z);
        LogLink(c);
        if (!useRef && AnchorReference())
            LogLink("[map] 提示: 插件未携带参考物件位置(老版本插件?), 已退回增量锚点。"
                    "在 Blender 插件面板设置/保留“Cube”即可启用绝对锚点。");
    }
    return true;
}

// v9: 把当前目标位姿写进 Camera.main。由 OnGameTick(pose_mode=1) 或
// Cinemachine Brain 钩子(pose_mode=3) 调用。
static void ApplyPoseTarget(const char* who) {
    if (!g_handles.ok) return;
    void* cam = Invoke(g_handles.camera_main, nullptr, nullptr);
    if (!cam) return;
    void* tr = Invoke(g_handles.comp_transform, cam, nullptr);
    if (!tr) return;

    const PoseTarget t = g_target;  // 结构体很小, 直接拷贝取一致快照
    static int diagCounter = 0;
    const bool diagThis = ((diagCounter++ % 60) == 59);
    float bx = 0, by = 0, bz = 0, brx = 0, bry = 0, brz = 0, brw = 0;
    // 每帧都读: 这里是"写入之前", 读到的是引擎(Brain)刚给出的位姿, 是最干净的来源。
    // 既用于诊断, 也作为相对基线的引擎位姿来源(见 CapturePoseBaseline)。
    ReadVec3(g_handles.tr_position_get, tr, bx, by, bz);
    ReadQuat(g_handles.tr_rotation_get, tr, brx, bry, brz, brw);
    g_enginePos.x = bx; g_enginePos.y = by; g_enginePos.z = bz;
    g_engineRot.x = brx; g_engineRot.y = bry; g_engineRot.z = brz; g_engineRot.w = brw;
    g_engineValid.store(true);
    g_enginePosMs.store(GetTickCount64());  // v1.0.0ab-fix: 标记"本帧引擎位姿有效"
    // v1.0.0af: 追踪期间, 在**没有任何其它工作**的间隙里连读两次锚点 —— 用来判断
    // "锚点在一次调用内前进"到底是并发更新(两次紧邻读就不同)还是探针自身开销(两次相同)。
    if (g_traceRemain.load() > 0) {
        const float e3[3] = {bx, by, bz};
        if (GameSysAnchorPosition(kAnchorVcamFollow, e3, g_trA1)) g_trA1Live = GameSysAnchorLastWasLive();
        if (GameSysAnchorPosition(kAnchorVcamFollow, e3, g_trA2)) g_trA2Live = GameSysAnchorLastWasLive();
    }

    UnityVector3 p = t.pos;
    // ---------- v1.0.0ag: 写入时刻补读锚点(指令 62) ----------
    // 依据(1.0.0af 零 I/O 追踪, 走动段 1314 行):
    //   · |a1 − a2| = 0        → 锚点在一次调用内不会变(无竞态); 之前测到的"落后"不是探针伪影的性质
    //   · |aU − a3| / 位移 = 1.000 → 目标用的锚点比"写入时刻读到的"整整旧一个相位
    //   原因: 目标在 OnGameTick(TailLateTick 组, 帧内早)算出, 而写入在 Brain.LateUpdate 之后(帧末),
    //         两者差大半个帧 → 相机用的角色坐标老了大半个帧; 帧时间又在抖(实测走动时中位 23ms、
    //         最大 49ms、41% 的帧偏离中位 >20%) → 相位差 = v × 帧时间 → **幅度 ∝ 速度的抖动**。
    //   修法: 写入前重读同一来源的锚点(此时角色已按本帧更新完), 用差值把位置补到"写入时刻"。
    //   基准是 g_diagAncUsed(算目标时用的那个锚点, 不含锚点微调量), late 同样不含 → 差值是纯增量。
    if (g_lateAnchor.load() == 1 && g_followReq.load() == 1 && t.posValid &&
        g_diagOrgSrc >= kAnchorVcamFollow && g_diagOrgSrc <= kAnchorCamMinusOffset) {
        float late[3] = {0.f, 0.f, 0.f};
        const float e3[3] = {bx, by, bz};
        if (GameSysAnchorPosition(g_diagOrgSrc, e3, late) && GameSysAnchorLastWasLive()) {
            p.x += late[0] - g_diagAncUsed[0];
            p.y += late[1] - g_diagAncUsed[1];
            p.z += late[2] - g_diagAncUsed[2];
        }
        g_trAL[0] = late[0];
        g_trAL[1] = late[1];
        g_trAL[2] = late[2];
        g_trALLive = GameSysAnchorLastWasLive();
    }
    if (t.posValid) {
        void* pPos[1] = {&p};
        Invoke(g_handles.tr_position_set, tr, pPos);
    }
    if (t.rotValid) {
        UnityQuaternion q = t.rot;
        void* pRot[1] = {&q};
        Invoke(g_handles.tr_rotation_set, tr, pRot);
    }
    if (t.fovValid) {
        float fv = t.fov;
        void* pFov[1] = {&fv};
        Invoke(g_handles.cam_fov_set, cam, pFov);
    }
    g_poseWriteCount.fetch_add(1);

    if (diagThis) {
        float ax = 0, ay = 0, az = 0, arx = 0, ary = 0, arz = 0, arw = 0;
        ReadVec3(g_handles.tr_position_get, tr, ax, ay, az);
        ReadQuat(g_handles.tr_rotation_get, tr, arx, ary, arz, arw);
        char buf[448];
        snprintf(buf, sizeof(buf),
                 "[diag:%s] 写入前(引擎给出的值)=pos(%.2f,%.2f,%.2f) "
                 "rot(%.3f,%.3f,%.3f,%.3f) | 目标=pos(%.2f,%.2f,%.2f) | "
                 "写入后=pos(%.2f,%.2f,%.2f) rot(%.3f,%.3f,%.3f,%.3f)",
                 who, bx, by, bz, brx, bry, brz, brw, t.pos.x, t.pos.y, t.pos.z, ax,
                 ay, az, arx, ary, arz, arw);
        LogLink(buf);
    }
    // v1.4: 偏移驱动的自检 —— 位置是游戏自己摆的, 所以这里能看到"偏移→位置"换算对不对。
    // 残差若恒定不为 0, 说明 pose_offset_basis(偏移坐标系)与实际不符, 可用这些数拟合。
    if (diagThis && !t.posValid) {
        const float dx = g_gameBasePos.x + g_v4World.x;
        const float dy = g_gameBasePos.y + g_v4World.y;
        const float dz = g_gameBasePos.z + g_v4World.z;
        char buf[384];
        snprintf(buf, sizeof(buf),
                 "[diag:偏移驱动] 期望相机 pos=(%.3f,%.3f,%.3f) | 实际(游戏摆的)="
                 "pos=(%.3f,%.3f,%.3f) | 残差=(%+.3f,%+.3f,%+.3f)",
                 dx, dy, dz, bx, by, bz, dx - bx, dy - by, dz - bz);
        LogLink(buf);
    }

    // ---------- v1.0.0af: 低开销逐帧追踪(内存缓冲, 循环内零 I/O) ----------
    if (g_traceRemain.load() > 0 && g_trCount < kTraceCap) {
        float ax = 0, ay = 0, az = 0, arx = 0, ary = 0, arz = 0, arw = 0;
        ReadVec3(g_handles.tr_position_get, tr, ax, ay, az);
        ReadQuat(g_handles.tr_rotation_get, tr, arx, ary, arz, arw);
        const float eng3[3] = {bx, by, bz};
        float a3[3] = {0.f, 0.f, 0.f};
        const bool a3ok = GameSysAnchorPosition(kAnchorVcamFollow, eng3, a3);
        TraceRec& r = g_tr[g_trCount];
        r.tUs = QpcUs();
        r.dtUs = (g_trPrevUs == 0) ? 0u : static_cast<uint32_t>(r.tUs - g_trPrevUs);
        g_trPrevUs = r.tUs;
        r.seqLo = static_cast<int32_t>(g_lastSample.sequence & 0x7fffffffULL);
        r.bf = g_lastSample.frame;
        r.orgSrc = g_diagOrgSrc;
        r.liveA1 = g_trA1Live ? 1 : 0;
        r.liveA2 = g_trA2Live ? 1 : 0;
        r.liveA3 = a3ok && GameSysAnchorLastWasLive() ? 1 : 0;
        r.lateOn = static_cast<uint8_t>(g_lateAnchor.load() > 0 ? 1 : 0);
        for (int k = 0; k < 3; ++k) {
            r.eng[k] = eng3[k];
            r.a1[k] = g_trA1[k];
            r.a2[k] = g_trA2[k];
            r.aU[k] = g_diagAncUsed[k];
            r.aL[k] = g_trAL[k];
            r.a3[k] = a3[k];
            r.tgt[k] = (k == 0) ? t.pos.x : (k == 1) ? t.pos.y : t.pos.z;
            r.post[k] = (k == 0) ? ax : (k == 1) ? ay : az;
        }
        ++g_trCount;
        if (g_traceRemain.fetch_sub(1) <= 1 || g_trCount >= kTraceCap) {
            g_traceRemain.store(0);
            TraceDump();
        }
    }
}

// v1.0.0af: 把内存里的追踪记录一次性落盘(只在追踪结束/缓冲写满时调用, 不在测量循环里)。
// 为什么要这样: 循环里做 snprintf+写文件, 会把"两次锚点读取"之间的间隔拉长到毫秒级,
// 而游戏的角色更新是并发的 → 测出来的"锚点落后一整拍"可能是**探针自身开销造成的伪影**。
// 格式(固定字段, 便于脚本解析):
//   [tr2] n dtUs seq bf src late | eng=(x,y,z) | a1=L/C(x,y,z) | a2=L/C(x,y,z)
//         | aU=(x,y,z) | aL=(x,y,z) | a3=L/C(x,y,z) | tgt=(x,y,z) | post=(x,y,z)
namespace {
void TraceDump() {
    if (g_trCount <= 0) return;
    char b[640];
    snprintf(b, sizeof(b), "[tr2] === dump %d 帧 (循环内零 I/O 版) ===", g_trCount);
    LogLink(b);
    for (int i = 0; i < g_trCount; ++i) {
        const TraceRec& r = g_tr[i];
        snprintf(b, sizeof(b),
                 "[tr2] %d %u %d %d %d %d | eng=(%.3f,%.3f,%.3f) | a1=%s(%.3f,%.3f,%.3f) | "
                 "a2=%s(%.3f,%.3f,%.3f) | aU=(%.3f,%.3f,%.3f) | aL=(%.3f,%.3f,%.3f) | "
                 "a3=%s(%.3f,%.3f,%.3f) | tgt=(%.3f,%.3f,%.3f) | post=(%.3f,%.3f,%.3f)",
                 i, r.dtUs, r.seqLo, r.bf, r.orgSrc, r.lateOn,
                 r.eng[0], r.eng[1], r.eng[2],
                 r.liveA1 ? "L" : "C", r.a1[0], r.a1[1], r.a1[2],
                 r.liveA2 ? "L" : "C", r.a2[0], r.a2[1], r.a2[2],
                 r.aU[0], r.aU[1], r.aU[2], r.aL[0], r.aL[1], r.aL[2],
                 r.liveA3 ? "L" : "C", r.a3[0], r.a3[1], r.a3[2],
                 r.tgt[0], r.tgt[1], r.tgt[2], r.post[0], r.post[1], r.post[2]);
        LogLink(b);
    }
    LogLink("[tr2] === dump 结束 ===");
    g_trCount = 0;
    g_trPrevUs = 0;
}
}  // namespace

void QueueCommand(int id, float arg) {    g_pendingCmdArg.store(arg);
    g_cmdA0.store(arg);
    g_cmdA1.store(0.f);
    g_cmdA2.store(0.f);
    g_cmdA3.store(0.f);
    g_pendingCmd.store(id);
}

void QueueCommand4(int id, float a0, float a1, float a2, float a3) {
    g_cmdA0.store(a0);
    g_cmdA1.store(a1);
    g_cmdA2.store(a2);
    g_cmdA3.store(a3);
    g_pendingCmdArg.store(a0);
    g_pendingCmd.store(id);
}

void SetHandles(const UnityHandles& h) { g_handles = h; }

// v1.3: 探测每帧推进。放在 OnGameTick 最前面, 且**不依赖** Blender 数据流
// (探测期间本来就不该有位姿写入)。探测本身要读"引擎自己的相机世界坐标",
// 所以先读 Camera.main.transform.position, 再交给 gamesys 的状态机。
static void ProbeFrameTick() {
    const bool active = GameSysOffsetCalibActive() || GameSysAnchorSampleActive();
    if (g_probeLock.load() != active) {
        g_probeLock.store(active);
        if (active) {
            LogLink("[probe] 探测开始: 位姿写入已临时关闭(探测结束自动恢复)");
        } else {
            LogLink("[probe] 探测结束: 位姿写入已恢复");
        }
    }
    if (!active || !g_handles.ok) return;
    void* cam = Invoke(g_handles.camera_main, nullptr, nullptr);
    if (!cam) return;
    void* tr = Invoke(g_handles.comp_transform, cam, nullptr);
    if (!tr) return;
    float x = 0.f, y = 0.f, z = 0.f;
    if (!ReadVec3(g_handles.tr_position_get, tr, x, y, z)) return;
    const float p[3] = {x, y, z};
    GameSysProbeTick(p);
}

void OnGameTick() {
    ExecutePendingCommand();  // 运行时指令优先处理(与位姿数据无关)
    GameSysUiTick();          // v1.0.0ai: 指令65 的到期自动还原(每帧无条件跑, 安全通道)
    ProbeFrameTick();         // v1.3: 探测(指令 45/46)每帧推进, 与 Blender 数据流无关
    if (!g_cfg.enabled || !g_handles.ok) return;

    // ---------- 1) 取位姿数据源 (共享内存 优先; 否则 UDP) ----------
    bool hasData = false;
    bool isCalibration = false;
    PoseFrame f{};
    BridgePose bp{};
    if (g_cfg.shmem) {
        if (ShmemLatest(bp) && (bp.flags & kFlagEnabled) != 0) {
            g_lastRecvMs.store(GetTickCount64());
            g_lastFrameMs.store(GetTickCount64());
            hasData = true;
            // v1.6: 插件面板的镜头模式(位 5) —— 随角色=1 / 定镜头=0。
            // 只在变化时动作: 切到随角色要关掉自由相机(偏移只对快照/关卡相机有效),
            // 切回定镜头要把自由相机开回来。
            const int req = (bp.flags & kFlagFollow) != 0 ? 1 : 0;
            if (g_followReq.load() != req) {
                const int prev = g_followReq.exchange(req);
                if (prev < 0) {
                    // 首次收到包: 只记录, 不擅自开关自由相机(那由武装脚本负责)
                    LogLink(req == 1
                                ? "[mode] 插件面板当前为「随角色镜头」→ 每帧读实时角色坐标 + 绝对写"
                                  " transform(pose_mode=3)"
                                : "[mode] 插件面板当前为「定镜头」→ 冻结角色锚点 + 绝对写 "
                                  "transform(pose_mode=3)");
                } else if (req == 1) {
                    // 随角色: 关掉自由相机(这样实时角色坐标读得到) —— 位置仍由我们每帧绝对写,
                    // 不用"发偏移让游戏摆"(那条路会被游戏自己的相机状态/鼠标干涉)。
                    LogLink("[mode] 插件面板: 切换到「随角色镜头」→ 关闭自由相机 + 每帧读实时角色"
                            "坐标 + 绝对写 transform(pose_mode=3)");
                    GameSysCloseMarketingCamera();
                    const float zero[3] = {0.f, 0.f, 0.f};
                    GameSysSnapshotSetCameraOffset(zero, 1);
                    g_v4LastSendMs.store(0);
                    LatchAnchor("面板切换(随角色, 作为兜底值)");
                } else {
                    LogLink("[mode] 插件面板: 切换到「定镜头」→ 打开自由相机 + 直接写 "
                            "transform(pose_mode=3)");
                    // 先冻结锚点(此时自由相机还开着 → 用粘性缓存值), 再把自由相机开回来
                    LatchAnchor("面板切换(定镜头)");
                    GameSysOpenMarketingCamera();
                    const float zero[3] = {0.f, 0.f, 0.f};
                    GameSysSnapshotSetCameraOffset(zero, 1);
                    g_v4LastSendMs.store(0);
                }
            }
            // v1.0.0ak: 插件面板的"隐藏游戏 UI"(位 6) —— 边沿触发。
            //   勾选 = 请求隐藏(记录常规遮罩 + 往游戏自己的配置栈加条目);
            //   取消 = 请求显示(撤销条目, 让游戏重算)。持续保持由帧末的 GameSysUiEnforce 负责。
            const int hideReq = (bp.flags & kFlagHideUi) != 0 ? 1 : 0;
            if (g_hideUiReq.load() != hideReq) {
                const int prevHide = g_hideUiReq.exchange(hideReq);
                if (prevHide < 0) {
                    // 首个包: 只在面板本来就勾着时执行一次隐藏(不打印"变化"日志)
                    if (hideReq == 1) {
                        GameSysUiHideBegin("首个数据包(面板勾选项已开启)");
                    } else {
                        LogLink("[ui] 面板: 隐藏游戏 UI = 关(未请求隐藏)");
                    }
                } else if (hideReq == 1) {
                    GameSysUiHideBegin("面板勾选");
                } else {
                    GameSysUiHideEnd("面板取消勾选");
                }
            }
        } else {
            // 只有真的断流(>3s 无新帧)才重建基线; 偶发丢帧不动基线
            const uint64_t nowMs = GetTickCount64();
            const uint64_t lastMs = g_lastFrameMs.load();
            if (lastMs != 0 && nowMs - lastMs > 3000) {
                if (g_baselineLogged.load()) g_baselineLogged.store(false);
                g_relNeedReset.store(true);
                // v1.0.0ak: 断流 3 秒, 隐藏状态也别留着(帧末的 500ms 判定通常已经先放行了)
                if (g_hideUiReq.load() == 1) {
                    g_hideUiReq.store(0);
                    GameSysUiHideEnd("数据流中断");
                }
            }
            return;
        }
    } else {
        const uint64_t now = GetTickCount64();
        const uint64_t last = g_lastRecvMs.load();
        if (last == 0 || now - last > g_cfg.timeout_ms) {
            if (g_baselineLogged.load()) g_baselineLogged.store(false);
            g_relNeedReset.store(true);
            return;
        }
        f = g_latest;
        hasData = true;
        g_lastFrameMs.store(now);
        isCalibration = (f.fov < 0.0f);  // 协议约定: fov<0 = 只记录基线
    }
    if (!hasData) return;
    g_lastSample = bp;  // 供诊断指令 39
    g_lastSampleValid.store(true);

    // ---------- v2: 镜头参数应用(独立于位姿模式) ----------
    if (g_cfg.lens_enabled && g_cfg.shmem && (bp.flags & kFlagLens) != 0) {
        ApplyShmemLens(bp);
    }

    // ---------- 3) 组织目标位姿 (统一为 Unity 坐标/四元数) ----------
    UnityVector3 v{};
    UnityQuaternion q{};
    bool rotApplied = false;
    float fovToSet = 0.f;
    bool doFov = false;
    if (g_cfg.shmem) {
        v.x = bp.pos[0]; v.y = bp.pos[1]; v.z = bp.pos[2];
        q.x = bp.quat[0]; q.y = bp.quat[1]; q.z = bp.quat[2]; q.w = bp.quat[3];
        rotApplied = g_cfg.rotation_enabled;
        if (g_cfg.fov_enabled && (bp.flags & kFlagLens) != 0) {
            fovToSet = bp.fovDeg;
            doFov = (fovToSet > 0.5f && fovToSet < 179.0f);
        }
    } else {
        if (g_cfg.unix_space) {
            v.x = f.px; v.y = f.py; v.z = f.pz;
        } else {
            BlenderToUnityPos(f.px, f.py, f.pz, v.x, v.y, v.z);
        }
        if (g_cfg.rotation_enabled) {
            if (g_cfg.unix_space) {
                q.x = f.qx; q.y = f.qy; q.z = f.qz; q.w = f.qw;
            } else {
                BlenderToUnityQuat(f.qw, f.qx, f.qy, f.qz, q.w, q.x, q.y, q.z);
            }
            rotApplied = true;
        }
        if (g_cfg.fov_enabled && f.fov > 0.5f && f.fov < 179.0f) {
            fovToSet = f.fov;
            doFov = true;
        }
    }
    // ---------- 3.5) 绝对 / 相对(基线) 换算 ----------
    if (g_cfg.pose_relative) {
        // 建立基线(需要读到引擎当前位姿, 因此在写入之前)
        if (g_relNeedReset.load() && !CapturePoseBaseline(v, q)) return;
        // 位置: 目标 = 游戏基线点 + 位移 × 位置比例
        //   位移有两种取法(v1.1):
        //     参考物件锚点(推荐) = Blender相机当前位置 − 参考物件位置  —— 绝对, 可复现
        //     相机基线(旧)       = Blender相机当前 − Blender相机基线    —— 增量, 依赖武装时刻
        UnityVector3 d{};
        const bool useRef = AnchorReference() && bp.hasRef;
        if (useRef) {
            d.x = (v.x - bp.refPos[0]) * g_cfg.pos_scale;
            d.y = (v.y - bp.refPos[1]) * g_cfg.pos_scale;
            d.z = (v.z - bp.refPos[2]) * g_cfg.pos_scale;
        } else {
            d.x = (v.x - g_blenderBasePos.x) * g_cfg.pos_scale;
            d.y = (v.y - g_blenderBasePos.y) * g_cfg.pos_scale;
            d.z = (v.z - g_blenderBasePos.z) * g_cfg.pos_scale;
        }
        const float len = sqrtf(d.x * d.x + d.y * d.y + d.z * d.z);
        if (g_cfg.pose_max_offset > 0.f && len > g_cfg.pose_max_offset) {
            const float k = g_cfg.pose_max_offset / len;
            d.x *= k;
            d.y *= k;
            d.z *= k;
        }
        // 游戏侧原点(v1.0.0n 起按模式分流):
        //   定镜头(followReq!=1): 用**冻结**的锚点 —— 相机钉在世界上那一点, 鼠标/角色都牵不动它。
        //   随角色(followReq==1): 每帧读**实时**角色坐标 —— 相机跟着角色走。
        //     这条路必须是"我们每帧绝对写 transform"(pose_mode=3), 因为它对游戏自己的相机
        //     状态免疫(定镜头实测: 进入游戏自带相机模式也不动)。反过来, 用"发偏移让游戏摆"
        //     (pose_mode=4)时基准 P0/坐标系 R 都在游戏手里, 鼠标一动基准就漂 —— 实测被
        //     鼠标干涉, 因此随角色不再走那条路(4 仅保留给实验, 用指令 22 a0=4 手动进入)。
        UnityVector3 org = g_gameBasePos;
        const bool followMode = (g_followReq.load() == 1);
        bool orgSet = false;
        // v1.0.0ab: 记录"实际生效的原点来源", 用于两件事:
        //   ① 引擎相机来源(5)不叠加锚点微调量(语义不同, 见下);
        //   ② 首次切到某个来源时打一行日志, 便于对着日志判断当前链路。
        int orgSrc = -1;
        const float eng[3] = {g_enginePos.x, g_enginePos.y, g_enginePos.z};
        float cand[3] = {0.f, 0.f, 0.f};
        if (followMode) {
            const int srcCfg = AnchorSourceSel();
            // v1.0.0ap/aq: 自动回退 —— 首选来源"读到的不是新鲜值"(拿到粘性缓存)时, 不采用那个旧值,
            // 改用引擎相机。注意两点:
            //   ① 必须看 GameSysAnchorLastWasLive(), 不能只看返回值 —— 实时读失败时
            //      GameSysAnchorPosition() 也会返回 true(附缓存值), 这正是"进干员界面后相机还停在
            //      三分钟前的位置"的根因;
            //   ② **必须覆盖"扫描模式"(来源=0)** —— 面板默认用它(武装点/修正量模式), 1.0.0ap 只覆盖了
            //      显式来源 1..4, 结果用户在扫描模式下完全没触发(实测 0 行"自动回退")。
            int src = srcCfg;
            if (AutoFallbackWanted()) {
                bool live = true;
                if (srcCfg == kAnchorArmedPoint) {
                    // 扫描模式: 只要**任一个候选是实时可读的**就算"有新鲜锚点"
                    live = false;
                    for (int c = kAnchorVcamFollow; c <= kAnchorCamMinusOffset; ++c) {
                        float probe[3] = {0.f, 0.f, 0.f};
                        if (GameSysAnchorPosition(c, eng, probe) && GameSysAnchorLastWasLive()) {
                            live = true;
                            break;
                        }
                    }
                } else if (srcCfg >= kAnchorVcamFollow && srcCfg <= kAnchorCamMinusOffset) {
                    float probe[3] = {0.f, 0.f, 0.f};
                    live = GameSysAnchorPosition(srcCfg, eng, probe) && GameSysAnchorLastWasLive();
                }
                // 显式选了引擎相机(5)/武装点以外的情况一律视为"新鲜"
                AnchorAutoNoteLive(live);
                if (!live && AnchorAutoUsingEngine() && EngineCamFresh()) src = kAnchorEngineCam;
            } else {
                AnchorAutoNoteLive(true);
            }
            // v1.0.0ab-fix: 引擎相机来源必须"本帧引擎位姿有效"才允许用(否则会是 (0,0,0));
            // 不满足时按原逻辑退回角色候选, 而不是拿一个假原点。
            const bool srcOk = (src != kAnchorArmedPoint) &&
                               (src != kAnchorEngineCam || EngineCamFresh());
            if (src != kAnchorArmedPoint && !srcOk) {
                static uint64_t warnMs = 0;
                const uint64_t nowMs = GetTickCount64();
                if (nowMs - warnMs > 2000) {
                    warnMs = nowMs;
                    LogLink("[anchor] 引擎相机来源暂不可用(本帧引擎位姿无效, pose_mode 需为 3/4)"
                            " → 临时退回角色候选");
                }
            }
            if (srcOk && GameSysAnchorPosition(src, eng, cand)) {
                org.x = cand[0];
                org.y = cand[1];
                org.z = cand[2];
                orgSet = true;
                orgSrc = src;
                g_diagAncUsedLive = GameSysAnchorLastWasLive();
            }
            if (!orgSet) {
                // 兜底扫描同样**不含** engineCam(它只允许显式选择, 见 LatchAnchor 的说明)。
                // v1.0.0aq: 自动回退开着时, 扫描只认**实时可读**的候选 —— 否则它会再次把
                // "粘性缓存里的旧值"捡回来, 把我们刚做出的引擎相机回退又顶掉(实测: 干员界面里
                // 那 66 秒前的值被当成有效原点, 相机被写到 700 米外)。
                const bool onlyLive = AutoFallbackWanted();
                for (int c = kAnchorVcamFollow; c <= kAnchorCamMinusOffset; ++c) {
                    if (GameSysAnchorPosition(c, eng, cand) &&
                        (!onlyLive || GameSysAnchorLastWasLive())) {
                        org.x = cand[0];
                        org.y = cand[1];
                        org.z = cand[2];
                        orgSet = true;
                        orgSrc = c;
                        g_diagAncUsedLive = GameSysAnchorLastWasLive();
                        break;
                    }
                }
                // v1.0.0aq: 一个新鲜的都没有 → 自动回退到引擎相机(游戏机位)
                if (!orgSet && onlyLive && AnchorAutoUsingEngine() && EngineCamFresh()) {
                    if (GameSysAnchorPosition(kAnchorEngineCam, eng, cand)) {
                        org.x = cand[0];
                        org.y = cand[1];
                        org.z = cand[2];
                        orgSet = true;
                        orgSrc = kAnchorEngineCam;
                        g_diagAncUsedLive = true;
                        static uint64_t logMs = 0;
                        const uint64_t now2 = GetTickCount64();
                        if (now2 - logMs > 3000) {
                            logMs = now2;
                            LogLink("[anchor] 自动回退生效: 角色候选全部不是新鲜值 → 本帧原点用**引擎相机**"
                                    "(游戏机位)");
                        }
                    }
                }
            }
        } else if (g_latchValid.load()) {
            org.x = g_latch[0];
            org.y = g_latch[1];
            org.z = g_latch[2];
            orgSet = true;
            orgSrc = g_latchSrc;
        }
        if (!orgSet) {
            if (g_latchValid.load()) {
                // 实时取不到(例如游戏离开了拍照模式) → 退回冻结值, 至少不会乱跳
                org.x = g_latch[0];
                org.y = g_latch[1];
                org.z = g_latch[2];
                orgSrc = g_latchSrc;
            } else {
                const int anchorSrc = AnchorSourceSel();
                if (anchorSrc != kAnchorArmedPoint) {
                    if (GameSysAnchorPosition(anchorSrc, eng, cand)) {
                        org.x = cand[0];
                        org.y = cand[1];
                        org.z = cand[2];
                        orgSrc = anchorSrc;
                    }
                } else {
                    org.x += g_originDx.load();
                    org.y += g_originDy.load();
                    org.z += g_originDz.load();
                    orgSrc = kAnchorArmedPoint;
                }
            }
        }
        // v1.6: 锚点微调量 —— 实测锚点(vcamFollow)落在**角色脚底**, 而 Blender 侧约定
        // (0,0,0) = 角色头部中心, 所以默认往上抬 1.4(游戏世界 Y 向上)。
        // 放在这里(而不是塞进原点修正量)是为了: 手动校准与自动校准都走同一套语义,
        // 改一个数就同时作用于两种校准方式。
        // v1.0.0ab: **引擎相机来源不叠加它** —— 那 1.4 是"脚底→头部"的语义, 对"游戏机位"
        // 没有意义(叠加只会把相机整体抬高 1.4m)。要微调该来源, 直接在 Blender 里移动相机。
        if (orgSrc != kAnchorEngineCam) {
            org.x += g_anchorOx.load();
            org.y += g_anchorOy.load();
            org.z += g_anchorOz.load();
        } else {
            static bool loggedEngineCam = false;
            if (!loggedEngineCam) {
                loggedEngineCam = true;
                LogLink("[anchor] 原点来源=引擎相机(游戏自己的机位): 只叠加 Blender 偏移, "
                        "不叠加锚点微调量。若画面不跟随角色, 说明当前状态下游戏相机是静止的 —— "
                        "用指令 42 切回 vcamFollow(-A0 1) 或武装点(-A0 0)。");
            }
        }
        v.x = org.x + d.x;
        v.y = org.y + d.y;
        v.z = org.z + d.z;
        // v1.0.0ag: 旧的"补读"放在这里(算目标的阶段)是**错的**:
        //   它与原始读处在同一个帧内相位(TailLateTick 组), 补不到任何东西; 而且用的是
        //   上一次调用的 g_diagAncUsed 当基准 → 每帧多加一个整步(实测用户反馈"抖动更严重")。
        //   正确位置是**写入时刻** —— 见 ApplyPoseTarget 里"写入前补读"那段(那里角色已按本帧更新完)。
        // v1.0.0ad: 记下本帧真正用的原点(逐帧追踪用)
        g_diagOrg = org;
        g_diagOrgSrc = orgSrc;
        g_diagAncUsed[0] = org.x - g_anchorOx.load();
        g_diagAncUsed[1] = org.y - g_anchorOy.load();
        g_diagAncUsed[2] = org.z - g_anchorOz.load();
        // v1.4: 偏移驱动要用"期望相机世界位置 − 机架零点 P0"(= 期望位置 − 武装点)。
        //   这里 v 就是期望的相机世界位置, 因此直接减 g_gameBasePos 即可,
        //   它自动包含了 Δ(原点修正量)与锚点来源的选择, 无需另立一套换算。
        g_v4World.x = v.x - g_gameBasePos.x;
        g_v4World.y = v.y - g_gameBasePos.y;
        g_v4World.z = v.z - g_gameBasePos.z;
        g_v4Valid.store(true);
        // 旋转: 绝对/相对·标准 = 映射后的 Blender 姿态; 相对·世界系/本地帧 = 叠加游戏基线
        if (rotApplied) {
            const int rm = RotMapModeEff();
            if (rm == kRotAbsolute || rm == kRotRelAligned) {
                // v16 绝对模式 / v1.0.0at 相对·标准: q 保持为"映射后的 Blender 姿态", 不做基线换算。
                // 这样游戏相机朝向 = Blender 相机朝向(同一物理方向), 世界轴与本地轴**同时**一一对应,
                // 俯仰不再带出滚转。位置仍然是上面的相对增量。
                //   v1.0.0at 说明: 相对管线保留这个语义时, 旋转与绝对模式完全等价(等价于武装时把基线
                //   对齐, Δ=0), 增量只作用在位置 —— 这正是"把增量旋转修正为标准坐标系"的实现:
                //   Δ≠0 时不可能两系同时 1:1(见 config.h 与该模式的数值表), 只能取标准语义作为默认。
            } else if (rm == kRotRelWorld) {
                // v1.0.0at: 相对·世界系增量 = delta 左乘游戏基线朝向。
                //   delta = q_now × conj(q_base) 是**世界系**增量; 实测: 绕 Blender 世界轴转 →
                //   游戏绕对应世界轴(0.00°), 且换姿态不漂移; 代价是绕相机本地轴转时,
                //   游戏里的轴被 Δ 转过(实测偏 52.6~63.1°) → 俯仰会带出滚转。
                //   (数值来源: source/tools/verify_rotation_mapping.py 第 6 节 "相对·世界系" 列)
                const float invW = g_blenderBaseQuat.w;
                const float invX = -g_blenderBaseQuat.x;
                const float invY = -g_blenderBaseQuat.y;
                const float invZ = -g_blenderBaseQuat.z;
                float dw, dx, dy, dz;
                QuatMul(q.w, q.x, q.y, q.z, invW, invX, invY, invZ, dw, dx, dy, dz);
                float fw, fx, fy, fz;
                QuatMul(dw, dx, dy, dz,
                        g_gameBaseRot.w, g_gameBaseRot.x, g_gameBaseRot.y, g_gameBaseRot.z,
                        fw, fx, fy, fz);
                q.w = fw;
                q.x = fx;
                q.y = fy;
                q.z = fz;
            } else {
                // v1.0.0z: 相对·本地帧增量 —— delta = conj(q_base) × q_now, 再**右乘**游戏基线朝向。
                //   "游戏相机的本地增量 == 映射后 Blender 相机的本地增量"(轴偏差 0.000°, 角大小一致),
                //   且 Blender 回到基线姿态时游戏相机正好回到自己的基线姿态。
                //   注意: 这样 Δ 是**自动抵消**的 —— 不需要把 Δ 当常量写进代码;
                //     Δ 随每次武装时两侧的朝向而变, 硬编码必然在下次会话就失效。
                //   代价: 绕 Blender **世界轴**转时, 游戏里的轴被 Δ 转过(实测偏 38.8~76.9°,
                //     换姿态最大漂移 87°) → 世界轴不是标准坐标系。仅作兼容保留。
                const float invW = g_blenderBaseQuat.w;
                const float invX = -g_blenderBaseQuat.x;
                const float invY = -g_blenderBaseQuat.y;
                const float invZ = -g_blenderBaseQuat.z;
                float dw, dx, dy, dz;
                QuatMul(invW, invX, invY, invZ, q.w, q.x, q.y, q.z, dw, dx, dy, dz);
                float fw, fx, fy, fz;
                QuatMul(g_gameBaseRot.w, g_gameBaseRot.x, g_gameBaseRot.y, g_gameBaseRot.z,
                        dw, dx, dy, dz, fw, fx, fy, fz);
                q.w = fw;
                q.x = fx;
                q.y = fy;
                q.z = fz;
            }
        }
    } else {
        v.x *= g_cfg.pos_scale;
        v.y *= g_cfg.pos_scale;
        v.z *= g_cfg.pos_scale;
    }

    // ---------- 4) 写入 ----------
    // 位姿写入闸门(默认关闭: 直接写 transform 与 Cinemachine 冲突)
    int poseMode = g_poseModeOverride.load();
    if (poseMode < 0) poseMode = g_cfg.pose_mode;
    if (g_probeLock.load()) poseMode = 0;  // v1.3: 探测期间不写位姿(测量要干净)
    // v1.6 起插件面板的「定镜头 / 随角色」**不再切换 pose_mode**: 两者都走 pose_mode=3
    // (每帧绝对写 transform, 对游戏自身相机状态与鼠标免疫), 区别只在游戏侧原点取
    // "冻结值(定镜头)" 还是 "实时角色坐标(随角色)" —— 见下面 org 的计算。
    // pose_mode=4(发偏移让游戏自己摆)只保留给实验, 用指令 22 a0=4 手动进入。
    if (poseMode == 0) {
        g_targetValid.store(false);  // 关闭时清掉目标, 防止残留旧目标被钩子继续写
        // v1.4: 若刚从偏移驱动模式退出来, 把偏移归零, 免得上一次的偏移一直挂着
        if (g_v4LastSendMs.load() != 0) {
            const float zero[3] = {0.f, 0.f, 0.f};
            GameSysSnapshotSetCameraOffset(zero, 1);
            g_v4LastSendMs.store(0);
            LogLink("[offset] 已退出偏移驱动, 偏移归零");
        }
        return;
    }

    // ---------- v1.4: 偏移驱动(pose_mode=4) ----------
    // 与模式 3 的区别: 位置不写 transform, 改成发"相机偏移", 由游戏自己摆相机:
    //     相机世界位置 = P0 + R · 偏移   →   偏移 = R⁻¹ · (期望世界位置 − P0)
    //   R = pose_offset_basis(指令 45 实测; 旋转矩阵 → R⁻¹ = Rᵀ)。
    //   好处: 只要 P0 跟着角色走, "随角色移动"就是游戏原生行为, 不需要我们每帧追。
    //   朝向仍由我们每帧写(绝对映射), 位置与朝向互不干扰(两个独立属性)。
    if (poseMode == 4) {
        g_targetValid.store(false);
        if (isCalibration) return;  // 校准帧: 不动偏移, 也不写朝向
        const uint64_t nowMs = GetTickCount64();
        const uint64_t lastMs = g_lastFrameMs.load();
        if (!g_v4Valid.load()) {
            static bool loggedNoRel = false;
            if (!loggedNoRel) {
                loggedNoRel = true;
                LogLink("[offset] 偏移驱动不可用: 需要 pose_relative=true(要的是相对量, 不是绝对坐标)");
            }
            return;
        }
        if (lastMs == 0 || nowMs - lastMs > 500) {
            // 安全阀: 数据流断了就把偏移归零, 让游戏自己接管机位
            if (g_v4LastSendMs.load() != 0) {
                const float zero[3] = {0.f, 0.f, 0.f};
                GameSysSnapshotSetCameraOffset(zero, 1);
                g_v4LastSendMs.store(0);
                LogLink("[safety] 数据流中断, 偏移已归零, 相机交还游戏");
            }
            return;
        }
        if (!GameSysSnapshotInstanceReady()) {
            static bool loggedNoInst = false;
            if (!loggedNoInst) {
                loggedNoInst = true;
                LogLink("[offset] 偏移驱动不可用: 快照相机实例未就绪(先发指令 27 进入拍照模式)");
            }
            return;
        }
        // v1.5: 偏移坐标系 = **相机自己的本地系**。
        // 实测依据(指令 45): 写偏移 (1,0,0)/(0,1,0) 得到的相机位移正是相机的"右/上"方向
        // (两个响应严格正交, 且随相机朝向变化); 沿视线方向的那一轴会被机架的距离限制卡住。
        // 所以把世界向量转进相机本地系即可: local = Rᵀ · world, R 由本帧要写入的朝向构造。
        // pose_offset_basis 保留为**可选的残差修正**(默认单位阵)。
        float R[9];
        QuatToBasis(q, R);
        const float* B = g_cfg.pose_offset_basis;
        const float w[3] = {g_v4World.x, g_v4World.y, g_v4World.z};
        const float loc[3] = {R[0] * w[0] + R[3] * w[1] + R[6] * w[2],
                              R[1] * w[0] + R[4] * w[1] + R[7] * w[2],
                              R[2] * w[0] + R[5] * w[1] + R[8] * w[2]};
        float off[3];
        off[0] = B[0] * loc[0] + B[1] * loc[1] + B[2] * loc[2];
        off[1] = B[3] * loc[0] + B[4] * loc[1] + B[5] * loc[2];
        off[2] = B[6] * loc[0] + B[7] * loc[1] + B[8] * loc[2];
        GameSysSnapshotSetCameraOffset(off, 1);  // 1 = 裸值指针(指令 45 实测唯一生效的约定)
        g_v4LastSendMs.store(nowMs);

        // 朝向仍然按绝对映射写(位置不写)
        g_target.posValid = false;
        g_target.pos = v;
        g_target.rot = q;
        g_target.rotValid = rotApplied;
        g_target.fov = fovToSet;
        g_target.fovValid = doFov;
        g_targetValid.store(true);
        return;
    }

    // 存为目标, 供对应时机的写入函数使用
    g_target.posValid = true;
    g_target.pos = v;
    g_target.rot = q;
    g_target.rotValid = rotApplied;
    g_target.fov = fovToSet;
    g_target.fovValid = doFov;
    g_targetValid.store(true);

    // 首帧记录当前基线(校准坐标换算用), 每轮激活记录一次
    if (!g_baselineLogged.exchange(true)) {
        void* camB = Invoke(g_handles.camera_main, nullptr, nullptr);
        void* trB = camB ? Invoke(g_handles.comp_transform, camB, nullptr) : nullptr;
        float bx = 0, by = 0, bz = 0, rx = 0, ry = 0, rz = 0, rw = 0;
        if (trB) {
            ReadVec3(g_handles.tr_position_get, trB, bx, by, bz);
            ReadQuat(g_handles.tr_rotation_get, trB, rx, ry, rz, rw);
        }
        char buf[288];
        snprintf(buf, sizeof(buf),
                 "[M0/baseline] camera pos=(%.3f, %.3f, %.3f) quat=(%.4f, %.4f, "
                 "%.4f, %.4f) src=%s pose_mode=%d",
                 bx, by, bz, rx, ry, rz, rw, g_cfg.shmem ? "shmem" : "udp", poseMode);
        LogLink(buf);
    }
    if (isCalibration) return;  // 校准帧不写入任何位姿

    // pose_mode=1: 就在本锚点写(可能被 Cinemachine 覆盖, 用于对照实验)
    // pose_mode=3: 交给 CinemachineBrain 之后的钩子写
    if (poseMode == 1) ApplyPoseTarget("TailLateTick");
}

// 由 CinemachineBrain.LateUpdate 之后的钩子调用(帧末最后时机)
void OnCameraBrainTick() {
    // v1.0.0ak: 隐藏游戏 UI 的"最后一句话"就在这个位置 —— 帧末、渲染之前。
    // 游戏自己的 HUD 更新发生在这个钩子之前(Unity 的 LateUpdate 是每帧最后的回调),
    // 所以这里重申遮罩能盖住游戏的重建; 与位姿模式/目标是否有效无关, 单独走自己的判定。
    {
        const uint64_t nowTick = GetTickCount64();
        const uint64_t lastTick = g_lastFrameMs.load();
        const uint64_t ageMs =
            (lastTick == 0) ? 0xFFFFFFFFull : (nowTick - lastTick);
        GameSysUiEnforce(ageMs);
        // v1.0.0ar: 主(世界)相机保活 —— 同一时机(帧末、渲染之前)。
        // 为什么必须在这里: 游戏把主相机遮罩写成 0 发生在此钩子之前, 这里写回去才赶得上本帧渲染。
        // 它不接 ageMs: 保活是用户显式设置, 不因断流自我关闭(详见 gamesys.h 的说明)。
        GameSysUiWorldKeepTick();
    }
    int poseMode = g_poseModeOverride.load();
    if (poseMode < 0) poseMode = g_cfg.pose_mode;
    // v1.0.0n: 面板的两种模式都用 pose_mode=3(绝对写), 所以这里不再按 followReq 改 poseMode
    if (poseMode != 3 && poseMode != 4) return;  // 4 = 偏移驱动(实验): 只写朝向, 位置交给游戏
    if (g_probeLock.load()) return;  // v1.3: 探测期间不写位姿
    if (!g_targetValid.load()) return;
    // 安全阀: 数据流停了就让游戏自己的相机接管, 否则相机会永久卡在我们最后写的位置
    // (若那个位置在关卡之外, 玩家会一直看虚空且无法恢复)。
    const uint64_t now = GetTickCount64();
    const uint64_t last = g_lastFrameMs.load();
    if (last == 0 || now - last > 500) {
        g_targetValid.store(false);
        LogLink("[safety] 数据流中断 >500ms, 停止位姿写入, 交还游戏相机");
        return;
    }
    ApplyPoseTarget("BrainLateUpdate");
}

bool PoseWriteActive() {
    if (g_probeLock.load()) return false;  // v1.3: 探测期间不写位姿
    int poseMode = g_poseModeOverride.load();
    if (poseMode < 0) poseMode = g_cfg.pose_mode;
    return poseMode == 3 || poseMode == 4;
}

}  // namespace ecl
