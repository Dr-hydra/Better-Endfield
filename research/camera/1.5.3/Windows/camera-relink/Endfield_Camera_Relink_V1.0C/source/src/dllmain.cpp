// EndfieldCamLink —— DllMain + 自举 + 游戏每帧回调
#include "config.h"
#include "gamesys.h"
#include "hook.h"
#include "il2cpp_api.h"
#include "inventory.h"
#include "link.h"
#include "resolver.h"
#include "shmem.h"

#include <windows.h>
#include <algorithm>
#include <atomic>
#include <chrono>
#include <fstream>
#include <string>
#include <thread>

using namespace std::chrono_literals;

namespace {

std::string g_moduleDir;
ecl::LinkConfig g_cfg;

void Log(const std::string& msg) {
    OutputDebugStringA(("[EndfieldCamLink] " + msg + "\n").c_str());
    std::ofstream log(g_moduleDir + "\\EndfieldCamLink.log", std::ios::app);
    if (log.is_open()) log << msg << std::endl;
}

void GameSysLogBridge(const char* msg) { Log(msg ? msg : ""); }

// ---- 目标方法描述(名称/程序集来自社区公开逆向资料, 版本敏感) ----
// 主锚点: CameraManager.TailLateTick(float) —— 任何相机模式下都每帧运行, 且位于帧末
constexpr char kAnchorAsm[] = "Gameplay.Beyond.dll";
constexpr char kAnchorNs[]  = "Beyond.Gameplay.View";
constexpr char kAnchorCls[] = "CameraManager";
constexpr char kAnchorFn[]  = "TailLateTick";  // 1 参数(float dt)
// 备用锚点: CameraMono._ProcessDitherByPitch (仅角色相机模式运行时有效)
constexpr char kAnchorAltCls[] = "CameraMono";
constexpr char kAnchorAltFn[]  = "_ProcessDitherByPitch";

// 钩子回调(Unity 主线程): 先放行原逻辑, 再执行我们的每帧工作
void(__fastcall* g_origAnchor)(void* self, void* method, float dt) = nullptr;
void __fastcall AnchorDetour(void* self, void* method, float dt) {
    if (g_origAnchor) g_origAnchor(self, method, dt);
    ecl::OnGameTick();
}
// 备用锚点(0 参数)回调
void(__fastcall* g_origAnchorAlt)(void* self, void* method) = nullptr;
void __fastcall AnchorDetourAlt(void* self, void* method) {
    if (g_origAnchorAlt) g_origAnchorAlt(self, method);
    ecl::OnGameTick();
}

// ---- 位姿写入的可选时机: Cinemachine Brain 之后 ----
// 设计动机: CameraManager.TailLateTick 早于 Cinemachine Brain 的 LateUpdate,
// 在那里写 Camera.main.transform 会被 Brain 每帧覆盖(表现为"位置完全不动")。
// 挂在 Brain.LateUpdate 之后写, 是渲染前最后的时机, 不会被覆盖。
void(__fastcall* g_origBrain)(void* self, void* method) = nullptr;
void __fastcall BrainDetour(void* self, void* method) {
    if (g_origBrain) g_origBrain(self, method);
    ecl::OnCameraBrainTick();
}

// Cinemachine 在 IL2CPP 里可能被打包进任意程序集, 且 v2/v3 命名空间不同 -> 逐个试
bool TryHookBrain() {
    struct Cand {
        const char* ns;
        const char* cls;
    };
    const Cand cands[] = {
        {"Cinemachine", "CinemachineBrain"},        // Cinemachine 2.x
        {"Unity.Cinemachine", "CinemachineBrain"},  // Cinemachine 3.x
        {"Beyond.Gameplay.View", "CinemachineBrain"},
    };
    for (const Cand& c : cands) {
        auto m = ecl::ResolveMethodAnywhere(c.ns, c.cls, "LateUpdate", 0);
        if (!m.ok) continue;
        const char* asmName = ecl::LastResolvedAssembly();
        char b[320];
        snprintf(b, sizeof(b), "找到 %s.%s.LateUpdate @ %p (程序集 %s), 尝试装钩...",
                 c.ns, c.cls, m.pointer, asmName ? asmName : "?");
        Log(b);
        if (!ecl::InstallHook(m.pointer, reinterpret_cast<void*>(&BrainDetour),
                              reinterpret_cast<void**>(&g_origBrain))) {
            snprintf(b, sizeof(b), "Brain 钩子安装失败, MinHook 状态码=%d",
                     ecl::LastHookStatus());
            Log(b);
            return false;
        }
        snprintf(b, sizeof(b), "成功挂上 %s.%s.LateUpdate -> pose_mode=3 可用", c.ns,
                 c.cls);
        Log(b);
        return true;
    }
    Log("未找到 CinemachineBrain.LateUpdate -> pose_mode=3 不可用(改用 1 或模拟输入伺服)");
    return false;
}

bool ResolveUnityHandles(ecl::UnityHandles& out) {
    using ecl::ResolveMethod;
    auto m = ResolveMethod("UnityEngine.CoreModule.dll", "UnityEngine", "Camera",
                           "get_main", 0);
    if (!m.ok) return false;
    out.camera_main = m.methodInfo;

    m = ResolveMethod("UnityEngine.CoreModule.dll", "UnityEngine", "Component",
                      "get_transform", 0);
    if (!m.ok) return false;
    out.comp_transform = m.methodInfo;

    m = ResolveMethod("UnityEngine.CoreModule.dll", "UnityEngine", "Transform",
                      "get_position", 0);
    if (!m.ok) return false;
    out.tr_position_get = m.methodInfo;

    m = ResolveMethod("UnityEngine.CoreModule.dll", "UnityEngine", "Transform",
                      "set_position", 1);
    if (!m.ok) return false;
    out.tr_position_set = m.methodInfo;

    m = ResolveMethod("UnityEngine.CoreModule.dll", "UnityEngine", "Transform",
                      "get_rotation", 0);
    if (!m.ok) return false;
    out.tr_rotation_get = m.methodInfo;

    m = ResolveMethod("UnityEngine.CoreModule.dll", "UnityEngine", "Transform",
                      "set_rotation", 1);
    if (!m.ok) return false;
    out.tr_rotation_set = m.methodInfo;

    m = ResolveMethod("UnityEngine.CoreModule.dll", "UnityEngine", "Camera",
                      "set_fieldOfView", 1);
    if (!m.ok) return false;
    out.cam_fov_set = m.methodInfo;

    m = ResolveMethod("UnityEngine.CoreModule.dll", "UnityEngine", "Camera",
                      "get_fieldOfView", 0);
    if (m.ok) out.cam_fov_get = m.methodInfo;

    // ---- Unity 物理相机(真实镜头参数)。缺失不致命, 只置 lens_ok=false ----
    std::string missingLens;
    auto tryPair = [&missingLens](const char* getter, const char* setter, void*& g,
                                  void*& s) {
        auto mg = ResolveMethod("UnityEngine.CoreModule.dll", "UnityEngine", "Camera",
                                getter, 0);
        auto ms = ResolveMethod("UnityEngine.CoreModule.dll", "UnityEngine", "Camera",
                                setter, 1);
        g = mg.ok ? mg.methodInfo : nullptr;
        s = ms.ok ? ms.methodInfo : nullptr;
        if (!mg.ok) missingLens += std::string(" get:") + getter;
        if (!ms.ok) missingLens += std::string(" set:") + setter;
        return mg.ok && ms.ok;
    };
    bool lens = true;
    lens &= tryPair("get_focalLength", "set_focalLength", out.cam_focal_get,
                    out.cam_focal_set);
    lens &= tryPair("get_aperture", "set_aperture", out.cam_aperture_get,
                    out.cam_aperture_set);
    lens &= tryPair("get_focusDistance", "set_focusDistance", out.cam_focusdist_get,
                    out.cam_focusdist_set);
    lens &= tryPair("get_usePhysicalProperties", "set_usePhysicalProperties",
                    out.cam_phys_get, out.cam_phys_set);
    lens &= tryPair("get_sensorSize", "set_sensorSize", out.cam_sensor_get,
                    out.cam_sensor_set);
    lens &= tryPair("get_nearClipPlane", "set_nearClipPlane", out.cam_near_get,
                    out.cam_near_set);
    lens &= tryPair("get_farClipPlane", "set_farClipPlane", out.cam_far_get,
                    out.cam_far_set);
    out.lens_ok = lens;
    Log(std::string("Unity 物理镜头句柄: ") + (lens ? "齐全" : "部分缺失") +
        (lens ? "" : ";" + missingLens));

    out.ok = true;
    return true;
}

// ---- IL2CPP VM 就绪检测: 钩住导出的 il2cpp_init ----
// 为什么必须这么做(三次"偶发"崩溃的根因):
//   旧判据是"GameAssembly.dll 模块已加载 + 导出可解析"。但导出表一加载就能用,
//   而 VM 可能仍在 il2cpp_init 内部初始化。我们的自举线程此时去调
//   il2cpp_class_from_name 等元数据接口, 与 il2cpp_init 并发操作未初始化完的 VM,
//   偶发踩坏内存 -> 0xC0000005。崩溃栈实测为 il2cpp_init -> il2cpp_class_from_name,
//   且不含本模块帧, 所以之前一直误判为"游戏自身不稳定"。
// 正确判据: il2cpp_init 返回之后 VM 才算就绪。il2cpp_init 是 GameAssembly.dll 的
//   导出函数, 用 GetProcAddress 即可拿到, 整个过程不需要碰 VM。
std::atomic<bool> g_vmReady{false};
std::atomic<bool> g_vmInitHookInstalled{false};

using Il2cppInitFn = int(__fastcall*)(const char*);
Il2cppInitFn g_origIl2cppInit = nullptr;

int __fastcall Il2cppInitDetour(const char* domainName) {
    const int r = g_origIl2cppInit ? g_origIl2cppInit(domainName) : -1;
    g_vmReady.store(true);
    Log(std::string("il2cpp_init 已返回 (ret=") + std::to_string(r) +
        ") -> VM 就绪, 现在才允许碰 IL2CPP 接口");
    return r;
}

// 轮询等待 GameAssembly 加载并挂上 il2cpp_init 钩子(全程只调 Win32 API)。
// 轮询间隔必须是 1ms 级: 实测 GameAssembly 加载后 UnityPlayer 几乎立刻调用初始化函数,
// 50ms 轮询会错过它(钩子装上时 init 已经跑完)。
// 注意: Windows 上 UnityPlayer 实际调用的是 il2cpp_init_utf16, il2cpp_init 虽导出但可能未被使用
//       (实测提前装好 il2cpp_init 钩子却从未被触发), 因此两个都挂。
bool TryHookIl2cppInit(int waitMs) {
    const char* names[] = {"il2cpp_init_utf16", "il2cpp_init"};
    const int steps = waitMs;
    for (int i = 0; i <= steps; ++i) {
        HMODULE ga = GetModuleHandleA("GameAssembly.dll");
        if (ga) {
            bool anyInstalled = false;
            for (const char* nm : names) {
                void* p = reinterpret_cast<void*>(GetProcAddress(ga, nm));
                if (!p) continue;
                if (ecl::InstallHook(p, reinterpret_cast<void*>(&Il2cppInitDetour),
                                     reinterpret_cast<void**>(&g_origIl2cppInit))) {
                    anyInstalled = true;
                    Log(std::string("已挂上 ") + nm + " 钩子(用于精确判定 VM 就绪)");
                } else {
                    Log(std::string(nm) + " 钩子安装失败, 状态码=" +
                        std::to_string(ecl::LastHookStatus()));
                }
            }
            if (anyInstalled) {
                g_vmInitHookInstalled.store(true);
                return true;
            }
            return false;
        }
        std::this_thread::sleep_for(1ms);
    }
    Log("等待 GameAssembly 初始化函数超时");
    return false;
}

void Bootstrap() {
    Log("Bootstrap 开始 (等待 GameAssembly/IL2CPP...)");
    // 第一步: 只做 Win32 操作, 挂 il2cpp_init 钩子以精确判定 VM 就绪
    const bool hooked = TryHookIl2cppInit(60000);

    bool attached = false;
    bool fallbackUsed = false;
    // 最多等待 180s 让游戏完成 IL2CPP 初始化
    for (int i = 0; i < 360; ++i) {
        // VM 就绪判据:
        //   首选 il2cpp_init 钩子已触发(精确)。
        //   兜底(仅在等待超时或钩子没装上时启用, 避免正常启动时过早动手):
        //     "导出可用 + 静置", 覆盖"附加注入到已运行进程"这类 init 早已结束的场景。
        bool ready = g_vmReady.load();
        if (!ready && !fallbackUsed) {
            const bool hookMissing = !g_vmInitHookInstalled.load();
            const bool waitedLong = (i >= 50);  // 50 * 500ms = 25s
            if (hookMissing || waitedLong) {
                if (ecl::Il2Cpp().loaded) {
                    fallbackUsed = true;
                    Log(hookMissing
                            ? "il2cpp_init 钩子未装上 -> 退化为'导出可用'判据, 额外静置 10s"
                            : "等待 il2cpp_init 超时(可能 VM 早已初始化, 如附加注入) -> 退化为'导出可用'判据, 额外静置 3s");
                    std::this_thread::sleep_for(hookMissing ? 10000ms : 3000ms);
                    ready = true;
                }
            }
        }
        if (!ready) {
            if (i % 40 == 0) {
                const char* miss = ecl::Il2Cpp().firstMissing;
                Log(std::string("等待 IL2CPP VM 就绪(il2cpp_init 未返回)... 缺失: ") +
                    (miss ? miss : "(未知)"));
            }
            std::this_thread::sleep_for(500ms);
            continue;
        }
        if (!ecl::Il2Cpp().loaded) {
            std::this_thread::sleep_for(200ms);
            continue;
        }
        // 触发一次自检, 确保导出齐全
        const ecl::Il2CppApi& api = ecl::Il2Cpp();
        // 关键: 把本线程注册进 GC 域再做任何 VM 调用。
        // 未注册线程碰 VM 是 "Collecting from unknown thread" 的经典成因。
        if (!attached && api.thread_attach && api.domain_get) {
            void* domain = api.domain_get();
            if (!domain) {
                std::this_thread::sleep_for(500ms);
                continue;
            }
            api.thread_attach(domain);
            attached = true;
            Log("自举线程已附加到 IL2CPP 域 (thread_attach)");
            // 附加后仍再等一拍, 让 VM 把初始化跑完
            std::this_thread::sleep_for(1500ms);
        }
        // 解析钩子锚点: 优先 CameraManager.TailLateTick(任何相机模式都运行, 帧末)
        auto anchor = ecl::ResolveMethod(kAnchorAsm, kAnchorNs, kAnchorCls,
                                         kAnchorFn, 1);
        bool useAltAnchor = false;
        if (!anchor.ok) {
            anchor = ecl::ResolveMethod(kAnchorAsm, kAnchorNs, kAnchorAltCls,
                                        kAnchorAltFn, 0);
            useAltAnchor = anchor.ok;
        }
        if (!anchor.ok) {
            Log("锚点 CameraManager.TailLateTick / CameraMono._ProcessDitherByPitch 均未就绪, 重试...");
            std::this_thread::sleep_for(500ms);
            continue;
        }
        Log(useAltAnchor ? "使用备用锚点 CameraMono._ProcessDitherByPitch"
                         : "使用主锚点 CameraManager.TailLateTick");
        // 解析 Unity 相机操作句柄
        ecl::UnityHandles handles;
        if (!ResolveUnityHandles(handles)) {
            Log("Unity 相机方法解析失败, 重试...");
            std::this_thread::sleep_for(500ms);
            continue;
        }
        ecl::SetHandles(handles);

        // 符号清单(可选): 一次性枚举相机相关类/方法/字段
        if (g_cfg.inventory) {
            Log("开始输出符号清单 -> " + g_moduleDir + "\\inventory.log");
            ecl::RunInventory(g_moduleDir + "\\inventory.log");
            Log("符号清单输出完成");
        }

        // 解析就绪后短暂延迟再装钩 (可配置, 默认5s)
        const uint32_t delayMs = std::min<uint32_t>(g_cfg.hook_delay_ms, 60000u);
        Log("解析完成, " + std::to_string(delayMs) + "ms 后装钩...");
        std::this_thread::sleep_for(std::chrono::milliseconds(delayMs));

        // 安装 inline hook (复用 MinHook), 保留原函数调用
        void* detour = useAltAnchor ? reinterpret_cast<void*>(&AnchorDetourAlt)
                                    : reinterpret_cast<void*>(&AnchorDetour);
        void** origSlot = useAltAnchor
                              ? reinterpret_cast<void**>(&g_origAnchorAlt)
                              : reinterpret_cast<void**>(&g_origAnchor);
        if (!ecl::InstallHook(anchor.pointer, detour, origSlot)) {
            Log("MinHook 安装失败!");
            return;
        }
        // 启动 UDP 链路
        ecl::StartLink(g_cfg);
        Log("Ready: 相机链路已挂载 (hook ok, UDP 端口 " +
            std::to_string(g_cfg.port) + ")");

        // 位姿写入的可选时机: Cinemachine Brain 之后(失败不致命)
        TryHookBrain();

        // 共享内存位姿源(Phase-1 协议)
        if (g_cfg.shmem) {
            Log(ecl::ShmemStart() ? "共享内存已连接 (EndfieldCameraBridgeV1)"
                                  : "共享内存未找到 (Blender 未开始发送?)");
        }

        // 游戏相机系统探测(只解析元数据; 所有托管调用排队到 Unity 线程执行)
        if (g_cfg.gamesys_probe) {
            ecl::GameSysSetLog(&GameSysLogBridge);
            ecl::GameSysResolve();
            ecl::GameSysReport();
            if (g_cfg.probe_open_marketing) ecl::QueueCommand(2, 0.f);
            if (g_cfg.probe_fov > 0.0f) ecl::QueueCommand(4, g_cfg.probe_fov);
            if (g_cfg.probe_aperture > 0.0f)
                ecl::QueueCommand(7, g_cfg.probe_aperture);
            ecl::QueueCommand(10, 0.f);  // 刷新实例并报告
        }
        return;
    }
    Log("Bootstrap 超时: 60s 内未完成解析");
}

}  // namespace

BOOL APIENTRY DllMain(HMODULE module, DWORD reason, LPVOID) {
    if (reason == DLL_PROCESS_ATTACH) {
        DisableThreadLibraryCalls(module);
        char buf[MAX_PATH];
        GetModuleFileNameA(module, buf, MAX_PATH);
        g_moduleDir = buf;
        const size_t slash = g_moduleDir.find_last_of('\\');
        if (slash != std::string::npos) g_moduleDir.resize(slash);

        g_cfg = ecl::LoadConfig(g_moduleDir + "\\EndfieldCamLink.ini");
        if (!g_cfg.enabled) return TRUE;

        Log("模块已注入, 启动自举线程");
        std::thread(Bootstrap).detach();
    }
    return TRUE;
}
