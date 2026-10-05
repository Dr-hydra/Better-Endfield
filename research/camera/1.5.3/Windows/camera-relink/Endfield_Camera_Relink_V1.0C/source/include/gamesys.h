#pragma once
// 游戏相机系统(原生接口)封装: 营销相机 / 快照(拍照)相机 / 景深 / 镜头
//
// 设计原则(v4, 血泪教训):
//   1) 只通过「已解析的托管方法 + runtime_invoke」操作游戏对象。
//      这条路径有异常返回、有 thread_attach 保护, 全程稳定。
//   2) 绝不直接读写托管对象的字段内存。
//      教训: v2/v3 用 il2cpp_field_get_value / il2cpp_class_get_field_from_name
//      做字段反射, 两次把游戏打崩(访问违例, 崩溃栈落在本模块内)。
//      字段反射对 IL2CPP 加固版本过于脆弱, 已整体移除。
//   3) 元数据枚举(类名/方法名/字段名)只读元数据, 安全, 保留。
#include <cstdint>
#include <string>

namespace ecl {

using GameSysLogFn = void (*)(const char*);
void GameSysSetLog(GameSysLogFn fn);

// 解析全部相机系统契约; 返回 true 表示核心(营销相机)可用
bool GameSysResolve();

// 打印解析结果(哪些契约可用/缺失)
void GameSysReport();

// 置位/清除 InputManager.enableMarketingCamera 门控
bool GameSysSetMarketingGate(bool on);

// 官方自由相机开关
bool GameSysOpenMarketingCamera();
bool GameSysCloseMarketingCamera();

// 当前相机 FOV 覆盖(CameraManager.SetOverrideFOVForCurrCamera) —— 已验证可用的焦距路径
bool GameSysSetOverrideFov(float fov);

// 营销相机 FOV 速率(注意: 是"持续变化速率"而非增量; 负值会把 FOV 拖到负数导致灰屏)
bool GameSysMarketingChangeFov(float rate);

// 营销相机模拟输入(游戏官方自由相机的驱动方式)
bool GameSysMarketingMoveInput(float x, float y);
bool GameSysMarketingRotateInput(float x, float y);
bool GameSysMarketingZoom(float delta);

// 安全阀: 回正 / 复位相机
bool GameSysRecenterCamera();
bool GameSysResetCameraPosition();

// ---- 快照(拍照)相机: 光圈/对焦/FOV/变焦 的唯一入口 ----
// 实例获取: 先 CreateOrGetTemporaryController(SnapshotCameraController 类型),
//           再退回 CameraManager.GetMainMarketingCameraController 之外的已知 getter。
bool GameSysResolveSnapshotInstance();
bool GameSysSnapshotInstanceReady();
bool GameSysSnapshotSetAperture(float fStop);
bool GameSysSnapshotSetFocusDistance(float distance);
bool GameSysSnapshotSetAdditiveFov(float delta);
bool GameSysSnapshotSetZoomScale(float scale);
float GameSysSnapshotGetZoomScale();
bool GameSysSnapshotActivate();
bool GameSysSnapshotDeactivate();
bool GameSysSnapshotUpdateSensorSize();

// ---- 景深(CameraUtils 静态接口) ----
// 游戏自身的 DOF 总开关; v>0 时按 f-stop 传参, 否则传 1 表示开启
bool GameSysEnableDof(float v);
// CameraManager.ToggleSnapshotCamera(a, b) —— 参数语义未知, 仅用于探测
bool GameSysToggleSnapshotCamera(float a, float b);

// 刷新实例并打印状态(供运行时指令使用)
void GameSysRefreshAndReport();
// 快照相机状态探测(只调用无参 getter, 不触碰内存)
void GameSysSnapshotProbe();
// v1.1: 读游戏原生的"相机相对角色偏移" SnapshotCameraController.GetCameraOffset()。
// 拍照相机就是「角色位置 + 相机偏移」模型, 因此 角色位置 = Camera.main.position − 偏移。
// 返回 false 表示接口未解析或快照相机实例未就绪(此时不做任何事, 不报错)。
bool GameSysSnapshotCameraOffset(float out[3]);

// ---- v1.2 位置锚点候选 ----
// 背景: 位置映射的"游戏侧原点"应当落在**角色**身上, 而不是"武装那一刻相机在哪"。
// 实测 GetCameraOffset() 在未手调时为 (0,0,0), 所以它不能用来反推角色位置;
// 于是把几个可能的"角色锚点"来源全部列出, 支持运行时切换, 由使用者对着画面挑。
// 所有候选都走"已解析的托管 getter", 并在调用前做实例类型校验(不符就不调用)。
enum AnchorSource {
    kAnchorArmedPoint = 0,        // 武装那一刻的相机位置(旧行为, 不调用游戏接口)
    kAnchorVcamFollow = 1,        // CameraManager.get_curVirtualCam().get_Follow()
    kAnchorVcamLookAt = 2,        // CameraManager.get_curVirtualCam().get_LookAt()
    kAnchorControllerTrans = 3,   // CameraManager.get_curActiveController().get_cameraTrans()
    kAnchorCamMinusOffset = 4,    // 引擎相机位置 − SnapshotCameraController.GetCameraOffset()
    // v1.0.0ab(3 号方案): 直接用**引擎相机自己的位置**当原点。
    // 动机: 随角色模式实测"角色走动/跳跃时相机相对角色小幅抖动(幅度 ∝ 角色速度, 与步频无关、
    //       静止时不抖、定镜头(冻结锚点)也不抖)" → 说明抖动来自"实时锚点的时间粒度/相位":
    //       我们贴的是角色侧节点的原始位置, 而游戏自己的相机是**逐帧平滑+带跟随阻尼**的。
    // 语义: 目标位置 = 引擎相机位置(本帧 Brain 刚算出的值) + Blender 位移。
    //       也就是"以游戏当前机位为原点, 只叠加 Blender 的偏移" —— 把游戏那层平滑继承过来。
    // 注意: ① 该来源**不叠加锚点微调量**(pose_anchor_ox/oy/oz, 那是"脚底→头部"用的, 语义不同);
    //       ② 只在"游戏相机确实在跟随角色"时才有效(若游戏相机是静止的, 效果等同定镜头)。
    //       ③ 默认不启用, 用指令 42 a0=5 切换, 一条指令即可切回。
    kAnchorEngineCam = 5,         // 引擎相机位置(游戏自己的机位, 带游戏的跟随阻尼)
    kAnchorSourceCount = 6,
};
const char* GameSysAnchorSourceName(int source);
// 解析中文名(供日志)
const char* GameSysAnchorSourceLabel(int source);
// 取候选位置; false = 该来源当前不可用(接口未解析/实例为空/类型不符)
bool GameSysAnchorPosition(int source, const float enginePos[3], float out[3]);
// v1.0.0ae: 最近一次 GameSysAnchorPosition 是否真的"实时读到"(false = 退回了粘性缓存)。
// 逐帧追踪(指令 60)用它区分"锚点滞后"的成因: 实时读失败用了旧缓存 / 值本身就晚一帧。
bool GameSysAnchorLastWasLive();
// 打印全部候选(诊断): 逐个尝试并输出可用性与坐标
void GameSysReportAnchorCandidates(const float enginePos[3]);

// ---- v1.3 探测轮(方案 B 前置): 角色锚点自动标定 ----
// 目标: 拿到"角色世界坐标", 让位置映射的游戏侧原点可以每次武装自动校准(不再肉眼对齐),
//       并且为"随角色移动"链路提供每帧角色位置。
// 已知障碍: GetCameraOffset() 恒为 (0,0,0), 所以"读接口"这条路不通。
//          但拍照相机就是「角色位置 + 相机偏移」模型, 而 SetCameraOffset 可写 ——
//          于是改用「写入-测量」反推: 写完偏移后读引擎相机世界坐标, 就能解出
//          锚点(角色)位置, 以及偏移所在的空间与增益。
//
// 指令 44: 类型体检 —— 打印关键 getter 的"声明返回类型"与"实际对象类(含父类链)",
//          用来判断实例类型校验为什么拦下 get_curVirtualCam(指针相等判定可能被泛型/
//          程序集链接的重复 Il2CppClass 拦掉)。
void GameSysTypeAudit();

// 指令 45: SetCameraOffset 写入-测量标定。
//   convMode: 0=装箱对象与裸值指针两种约定都跑(默认) 1=只跑装箱 2=只跑裸值
//   settle  : 每次写入后等待的稳定帧数(默认 12)
//   返回 false = 接口未解析或快照相机实例未就绪(需先发指令 27 进入拍照模式)。
bool GameSysOffsetCalibBegin(int convMode, int settle);
bool GameSysOffsetCalibActive();

// 指令 46: 锚点候选采样器 —— 连续 N 帧采样所有候选, 输出中位数与极差。
//   判据: 极差很小(≤2cm)且与引擎相机位置明显不同 = 可用的静态锚点。
bool GameSysAnchorSampleBegin(int frames);
bool GameSysAnchorSampleActive();

// ---- v1.0.0ai/aj 探测轮(隐藏游戏 UI 的前置): 只读体检 + 渲染层写入-测量 ----
// 目标: 在"不进原生相机模式(C1) / 不影响键位(C2) / 攻击可用(C3) / 不碰 UID 水印(C4)"下隐藏 HUD。
// 1.0.0ai 体检实测(定案): 世界由 MainCamera(遮罩 0xAFFFDBDF, 不含位 5)渲染,
//   HUD 由独立 UICamera(CameraManager.get_uiCamera(), 遮罩 0x20 = **只渲染层 5**, depth=2)渲染
//   → 两者遮罩零重叠: 动 UICamera 的遮罩碰不到世界、也不涉及任何输入状态 ⇒ 方案 A 成立。
//   游戏自己的接口: _SetUICameraCullingMask / Add·RemoveUICamCullingMaskConfig + _UpdateUICamCullingMask
//   / GetUICamDefaultCullingMask(=32) / _CurrCameraNeedHideHUD(原生 X 那套的判断)。
//
// 指令 64: UI 体检(只读)。输出:
//   ① UnityEngine.Camera.allCameras 全量相机清单(本 build 缺数组导出 → 自动降级);
//   ② CameraManager / CameraUtils 中 UI/图层/遮罩 相关成员的完整签名(元数据);
//   ③ 全部 0 参且返回 UnityEngine.Camera 的 getter 候选, 逐个调用并打印其实例属性;
//   ④ 全部 0 参且返回 int 且名字像 层/遮罩 的 getter, 逐个调用(直接给出 UI 图层号)。
void GameSysUiAudit();

// 指令 65: 渲染层写入-测量(a0=1 关上 / a0=0 立即还原; a1=目标选择; a2=保持秒数)。
//   目标选择: 0=自动(首选 CameraManager.get_uiCamera(); 取不到才退回 allCameras 枚举按名字挑;
//                都失败 → 拒绝执行, 不写任何东西),
//             1=Camera.main(对照实验, 会黑屏, 慎用), 2=非 main 的相机(get_uiCamera 优先)。
//   写入通道(自动依次尝试, 日志能看出哪条生效):
//     通道1 = 游戏自己的命名配置栈 AddUICamCullingMaskConfig("ECLHideUI", 0) + _UpdateUICamCullingMask()
//             —— 原生 X 隐藏 UI 走的就是这条, 最不容易被覆写;
//     通道2 = _SetUICameraCullingMask(0)(没有则退回 UnityEngine 的 set_cullingMask)。
//   读回: 写入前 → 写入后立刻 → 保持中(约一半时长)各读一次; 保持中那次判断"游戏会不会自己改回去"。
//   还原: 到期自动还原(每帧 tick 驱动), 先用过的通道先撤销, 再按需补一次直接写(双保险)。
// v1.0.0ar: 第 4 个参数 maskOverride —— 要写进目标相机的**任意遮罩值**(0 = 旧行为"彻底不渲染")。
// 为什么加: 实测 ESC(时停)界面里, 那幅"冻结/模糊的世界画面"是 **UI 相机**画的(主相机此刻遮罩=0),
// 而 UI 相机在那一瞬的遮罩是 0x420 = 位5(UI, 菜单) + 位10(UIPP, 那幅世界画面)。
// 于是"只藏菜单、留下世界"= 把 UI 相机遮罩写成 0x400 —— 用本参数就能一次性试出来(自动还原)。
bool GameSysUiTryCullMask(int mode, int target, float holdSeconds, int maskOverride);
// 指令 65 的还原阶段是否还在等(供日志/诊断)
bool GameSysUiCullMaskPending();

// ---- v1.0.0ak: 面板"隐藏游戏 UI(拍摄用)"的正式通路(共享内存位 6 驱动) ----
// 实测(1.0.0aj 指令 65): UICamera 只渲染层 5, MainCamera 的遮罩不含位 5 → 只动 UICamera 遮罩
// 即可隐藏 HUD, 且不涉及输入(移动/普通攻击/技能照常)、可逆。
//
// 隐藏: 记录"常规遮罩"→ AddUICamCullingMaskConfig("ECLHideUI", 0) + _UpdateUICamCullingMask()
// 显示: RemoveUICamCullingMaskConfig("ECLHideUI") + _UpdateUICamCullingMask(), 再读回校验;
//       若仍为 0(异常)则补一次直接写回记录的常规遮罩(双保险: 绝不能把 UI 永久藏起来)。
bool GameSysUiHideBegin(const char* why);
void GameSysUiHideEnd(const char* why);
// 每帧在**帧末钩子**(CinemachineBrain 之后、渲染之前)调用 —— 那里的写入才是"最后一句话"。
// streamAgeMs = 距上一个有效包的时间; >500ms 视为断流 → 立刻放行(失败要开, 不能把 UI 关死)。
void GameSysUiEnforce(uint64_t streamAgeMs);
// 策略: 0=尊重游戏(游戏呼出界面/加层时放行, 回到常规后自动重新隐藏)
//       1=严格保持(**默认**, 用户 2026-09-14 选定"全藏, 连菜单也不显示": 每帧压回 0)
void GameSysUiSetStrict(int strict01);
// 只读诊断(日志用)
void GameSysUiReportState();

// ---- v1.0.0am: "完全去掉 UI"探测轮(怪物血条/角色体力条 这类还没被盖住的元素) ----
// 背景: 1.0.0ak 只压了 UI 相机(层 5)的遮罩 —— 屏幕 HUD 消失, 但
//   · 怪物血条/状态条 多半是**世界空间**的(画在怪物头顶, 由 MainCamera 渲染), UI 相机管不到;
//   · 角色体力条 若在战斗中让游戏往 UI 相机配置栈里加了新层, 会被"放行"策略整块放回来
//     (所以策略已默认改为严格保持)。
// 因此需要: ① 看清每一层叫什么名字(名字比位号好认得多), ② 能对 MainCamera 的图层写-测(带自动还原)。
//
// 指令 70: 图层与相机体检(只读)。
//   打印 LayerMask.LayerToName(0..31) 的**图层名表**, 以及 MainCamera / UICamera 当前遮罩
//   (逐位带名字)、游戏自报的默认遮罩、GetUIModelLayerMask(i) / GetGachaLayerMask(i)。
void GameSysUiReportLayers();

// 指令 71: 主(世界)相机图层写-测。
//   clearBits : 要从 MainCamera 遮罩里清掉的位(位掩码; 0 = 取消该设置)
//   holdSec   : >0 = 一次性测试, 到期自动还原(每帧 tick 驱动);
//               <=0 = 设为**持续**(此后勾选"隐藏游戏 UI"时一并生效, 直到取消勾选或再发一次 0)
//   通道优先走游戏自己的配置栈: AddMainCamCullingMaskConfig("ECLHideWorldUI", 新遮罩)
//   + _UpdateMainCamCullingMask(); 还原 = RemoveMainCamCullingMaskConfig + _UpdateMainCamCullingMask。
void GameSysUiWorldClearSet(int clearBits, float holdSec);
// 当前持续清位设置(供日志/诊断)
int GameSysUiWorldClearBits();

// ---- v1.0.0ar: 主(世界)相机"保活"(指令 73) ----
// 问题(2026-09-15 实测, 用户按 ESC 黑屏): 游戏进入"时停/界面"状态时会
//   _SetMainCameraCullingMask(0) —— 世界完全不画; 同一时刻 UI 相机的遮罩从 0x20 扩到 0x420
//   (位 5 UI + 位 10 UIPP) —— 也就是说那一刻**整幅画面只由 UI 相机产出**。
//   于是"严格隐藏"把 UI 相机压到 0 就等于把屏幕唯一的产出源按掉 → 全黑(不是清位写坏了:
//   清位只写 cur & ~clearBits(非零), 且撤销配置条目后主相机仍是 0 = 游戏自有值)。
// 做法: 帧末(渲染前最后时机)若发现主相机被游戏关掉, 就把它按回期望遮罩
//   (= GetMainCamDefaultCullingMask() & ~持续清位)。绝不写 0; 目标推导不出来就不动手(失败要开);
//   数据流中断 >500ms 自动关闭。与"隐藏 UI"配合 = ESC 时也能看到时停的世界画面且无任何 UI。
//
// GameSysUiWorldKeepSet: 开关 / 一次性测试
//   on=1 开(持久), on=0 关(默认; 同时结束一次性测试并让游戏自己重算遮罩)
//   holdSec>0 一次性测试秒数(到期自动还原)
//   maskOverride!=0 目标遮罩覆盖(诊断用)
//   mode=1 激进(缺世界位就按回) / 0 保守(仅当主相机被彻底关掉(=0)时按回)
void GameSysUiWorldKeepSet(int on, float holdSec, int maskOverride, int mode);
// 每帧推进(帧末钩子调用)。
// **闸门(用户 2026-09-15 要求): 只有勾选了"隐藏 UI"(GameSysUiHideBegin 生效)时才会触发**;
//   没隐藏 UI 时一次都不碰主相机 —— 那时画面是游戏自己的事(包括它按 ESC 把世界相机写成 0)。
// 注意: 这条通道也**不挂"数据流中断 >500ms 就撒手"**那条判据(隐藏 UI 有, 因为它会把你困在
//   没有 UI 的画面里)。保活是用户显式打开的设置, Blender 没连上时也该照常工作。
void GameSysUiWorldKeepTick();
// 隐藏 UI 会话开始/结束时调用: 清零计数(让状态行的"按回 N 次"反映本次会话) / 把主相机交还游戏重算
void GameSysUiWorldKeepResetSession();
bool GameSysUiWorldKeepHandBack(const char* why);

// ---- v1.0.0ar: 指令 74 —— UI 相机"清屏 / 启用"写-测 ----
// 目的(用户 2026-09-15 的诉求): **屏蔽 ESC 界面之后, 强制渲染它后面的游戏画面**。
//   严格隐藏把 UI 相机遮罩压成 0 时, 画面仍全黑 —— 两种可能: ① 世界根本没被渲染;
//   ② 世界被渲染了, 但**遮罩=0 的 UI 相机照样清屏**(它 depth=2, 在主相机 depth=0 之后)把颜色缓冲擦掉。
//   实测 ESC 界面态 UICamera depth=2 / clearFlags=1 -> ②很可疑。本条用于把两者分开:
//   把 clearFlags 改成 Depth(3) 或 Nothing(4)(或干脆 enabled=false), 世界画面若露出来就是②。
// what: 0=还原 / 1=写 clearFlags / 2=写 enabled(false) / 3=两个都写
// clearValue: clearFlags 目标值(<=0 取默认 3=Depth)
// holdSec: >=1 一次性测试(到期自动还原); <1 持续(直到 what=0)
void GameSysUiCamPropSet(int what, float holdSec, int clearValue);
// v1.0.0ar: 隐藏 UI 时是否**禁用 UI 相机**(ini ui_hide_disable_uicam, 默认开)。
// 为什么必须: 只压遮罩不够 —— **遮罩=0 的相机照样清屏**, ESC 界面态下它把主相机画好的世界擦黑;
//   实测只写 clearFlags=Depth 无效(自定义渲染管线不采纳), 而 enabled=false 立刻让世界露出来。
void GameSysUiSetDisableUiCam(int on);
// 只读诊断: 1 = 保活开着
int GameSysUiWorldKeepState(int* reapplies, int* fails);

// v1.0.0ai: 指令 65 的每帧推进(到期自动还原)。**必须每帧无条件调用**(与探测无关),
// 因为它是"写坏渲染层也要能自动恢复"的安全通道。
void GameSysUiTick();

// 每帧推进(由尾帧钩子调用; enginePos = 此时刻 Camera.main 的世界坐标, 探测期间不写位姿)
void GameSysProbeTick(const float enginePos[3]);

// ---- v1.0.0as: 指令 75 —— 景深(DOF)链路体检(**只读**) ----
// 为什么要它: 用户反馈"光圈只在进入官方相机后才生效, 我们的正常自由相机里完全无效"。
//   已查明: 我们现在只把光圈送给 SnapshotCameraController.SetAperture, 而那个实例只在官方相机态存在;
//   游戏自己的主相机景深通道是 CameraUtils.GetDOFData() -> HGDepthOfFieldData
//   与 CameraUtils.EnableDOF(CameraDOFDescriptor)(作用于 CameraManager.mainCamDOFComp)。
//   本命令打印: DOF 相关的全部重载签名 / HGDepthOfFieldData 字段(名·类型·偏移·当前值) /
//   CameraDOFDescriptor 结构 / mainCamDOFComp 此刻在不在。全部走元数据 API, 不碰裸内存偏移。
// 参数: a0 = 0(默认) 只跑安全路径(方法枚举/类链); a0 = 1 额外列出字段**名字**(仍不读值)。
// ⚠ 纪律: 只允许「已解析方法 + runtime_invoke」与「只读元数据枚举」。**绝不读写字段内存**
//   (v2/v3 与 2026-09-15 指令 75 第一版, 共三次把游戏打崩 —— 详见 gamesys.cpp 文件头)。
void GameSysDofAudit(int includeFieldNames);

// ---- v1.0.0as: 指令 76 —— 主相机景深"通道验证"(一次性, 到期自动还原) ----
// 用户 2026-09-15 决定验证的第一件事: **在我们的自由相机里到底能不能开景深**。
// 做法只用两类允许的操作: ① 已解析方法 + runtime_invoke  ② 只读元数据枚举。**绝不读写字段**。
//   GetDOFData() 取游戏原数据(只为拿它的类) -> object_new -> 调它的**无参构造** ->
//   挑出 CameraUtils 上参数类型为 HGDepthOfFieldData 的那个 EnableDOF 重载 -> 调用。
// 还原: 重新 GetDOFData() 再 EnableDOF 一次(交还游戏), 不跨帧缓存托管指针。
//   a0 = 1 无参构造测试(p1 = 保持秒数, 默认 20)
//       3 带数值构造: (HGDepthOfFieldType, float×6) 里 6 个 float **全设成 p1**(p2 = 保持秒数)
//       4 槽位试探: 只把 f[p2] 设成 p1、其余设成 p3(固定保持 30 秒) —— 用来逐槽位找"光圈/对焦"
//       0 还原(交还游戏原数据) / 2 兜底 DisableDOF(0, null)
void GameSysDofChanTest(int mode, float p1, float p2, float p3);
// 每帧推进(到期自动还原; 由 GameSysUiTick 无条件驱动)
void GameSysDofTick();

// ---- v1.0.0as: 景深"落地"(Apply/Reset) + 指令 77 ----
// 用户 2026-09-15 实测推导: 武装时会顺手调一次 `ApplySnapshotDofSettings()`, 所以"武装后自由镜头有虚化";
//   进一次官方相机再退出后那份施加被覆盖, 而 SetAperture 只改"控制器里的数值"、没人再 Apply,
//   于是表现成"光圈只在进官方相机后才生效"。**修法 = 每次改完光圈/对焦后主动 Apply。**
bool GameSysSnapshotApplyDof();
bool GameSysSnapshotResetDof();
// 指令 77: fStop>0 -> SetAperture; focus>0 -> SetFocusDistance; doApply -> Apply; doReset -> 先 Reset
void GameSysDofApplyTest(float fStop, float focus, int doApply, int doReset);

// 写入 SnapshotCameraController.SetCameraOffset(Vector3)。
//   convention: 0=传装箱对象(多数 il2cpp 版本要求) 1=传裸值指针(按值拷贝 12 字节)
//   void 方法无法用返回值判成败, 只以"是否抛托管异常"为判据。参数不是值类型时拒绝调用。
bool GameSysSnapshotSetCameraOffset(const float v[3], int convention);
// 打印 SetCameraOffset 的参数类型/尺寸/返回类型(只读元数据, 不做任何调用)
void GameSysReportSetOffsetSignature();

// 只读元数据: 对象的类名链(自身 -> 父类 ...)  /  按类名(含父类链)判定
std::string GameSysClassChainOf(void* obj);
bool GameSysObjectIsNamed(void* obj, const char* name);

// ---- 元数据枚举(只读元数据, 安全) ----
// 按目标索引 + 关键字索引枚举类成员; a0=-1 时打印索引表
void GameSysEnumerateTarget(int targetIndex, int keywordIndex);
void GameSysEnumListTargets();
// 打印关键托管方法的参数类型与返回类型(不靠猜参数语义)
void GameSysReportSignatures();

}  // namespace ecl
