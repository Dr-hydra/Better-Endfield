// 游戏相机系统封装 (v4)
//
// 血泪教训: v2/v3 用 il2cpp_field_get_value / il2cpp_class_get_field_from_name
// 直接读托管对象字段, 两次把游戏打崩(访问违例, 崩溃栈落在本模块内)。
// **第三次(2026-09-15 11:24, 指令 75 的第一版)**: 在字段循环里用了
//   `field_get_type` + `class_value_size(k,nullptr)` + `field_get_value`(想顺便打出字段**当前值**),
//   指令发出那一刻游戏当场崩溃(崩溃转储 Crash_2026-09-15_032417127), 模块命令线程同时卡死。
//   结论不变且更严: **连"按类型解析 + 按声明尺寸读值"都算字段内存读写**, 一律禁止。
// 本文件已彻底移除字段内存读写, 只保留两类操作:
//   A) 已解析托管方法 + runtime_invoke (有异常返回 + thread_attach 保护)
//   B) 只读元数据枚举(类名/方法名/字段名), 不读任何对象内存
// 需要某个类的"数据布局"时, 唯一允许的做法是: 找**方法**(setter/属性/工厂)去设置它,
//   或者让游戏自己的 API 把数据交给我们(A/B 两类之内), 绝不自己去摸字段。
#include "gamesys.h"
#include "il2cpp_api.h"
#include "resolver.h"

#include <windows.h>
#include <algorithm>
#include <cmath>
#include <cstdio>
#include <cstring>
#include <string>
#include <vector>

namespace ecl {

namespace {

GameSysLogFn g_log = nullptr;
void LogG(const std::string& s) { if (g_log) g_log(s.c_str()); }

constexpr char kAsm[] = "Gameplay.Beyond.dll";

struct Sys {
    // CameraUtils (静态)
    bool has_getCameraManager = false;
    void* mi_getCameraManager = nullptr;
    bool has_openMarketing = false;
    void* mi_openMarketing = nullptr;
    bool has_closeMarketing = false;
    void* mi_closeMarketing = nullptr;
    bool has_enableDof = false;
    void* mi_enableDof = nullptr;
    bool has_getDofData = false;
    void* mi_getDofData = nullptr;
    bool has_recenter = false;
    void* mi_recenter = nullptr;
    bool has_resetCamPos = false;
    void* mi_resetCamPos = nullptr;
    // CameraManager (实例)
    bool has_getMainMarketing = false;
    void* mi_getMainMarketing = nullptr;
    bool has_getMainLevel = false;
    void* mi_getMainLevel = nullptr;
    bool has_setOverrideFov = false;
    void* mi_setOverrideFov = nullptr;
    bool has_toggleSnapshot = false;
    void* mi_toggleSnapshot = nullptr;
    bool has_createTemp = false;
    void* mi_createTemp = nullptr;
    // MarketingCameraController (实例)
    bool has_marketingChangeFov = false;
    void* mi_marketingChangeFov = nullptr;
    bool has_marketingMoveInput = false;
    void* mi_marketingMoveInput = nullptr;
    bool has_marketingRotateInput = false;
    void* mi_marketingRotateInput = nullptr;
    bool has_marketingZoom = false;
    void* mi_marketingZoom = nullptr;
    // SnapshotCameraController (实例)
    bool has_snapSetAperture = false;
    void* mi_snapSetAperture = nullptr;
    bool has_snapSetFocus = false;
    void* mi_snapSetFocus = nullptr;
    bool has_snapAdditiveFov = false;
    void* mi_snapAdditiveFov = nullptr;
    bool has_snapSetZoom = false;
    void* mi_snapSetZoom = nullptr;
    bool has_snapGetZoom = false;
    void* mi_snapGetZoom = nullptr;
    bool has_snapActivate = false;
    void* mi_snapActivate = nullptr;
    bool has_snapDeactivate = false;
    void* mi_snapDeactivate = nullptr;
    bool has_snapUpdateSensor = false;
    void* mi_snapUpdateSensor = nullptr;
    bool has_snapIsFirstPerson = false;
    void* mi_snapIsFirstPerson = nullptr;
    bool has_snapApplyDof = false;
    void* mi_snapApplyDof = nullptr;
    bool has_snapResetDof = false;   // v1.0.0as: 与 Apply 配对的 Reset(恢复游戏默认)
    void* mi_snapResetDof = nullptr;
    // v1.1: 拍照相机的"相机相对角色偏移"
    bool has_snapGetOffset = false;
    void* mi_snapGetOffset = nullptr;
    // v1.3: 写入"相机相对角色偏移"(方案 B 随角色的关键通道; Get 恒为 0, 所以只走写-测)
    bool has_snapSetOffset = false;
    void* mi_snapSetOffset = nullptr;
    // v1.2: 锚点候选(角色位置来源)
    bool has_curVirtualCam = false;
    void* mi_curVirtualCam = nullptr;
    bool has_curActiveController = false;
    void* mi_curActiveController = nullptr;
    bool has_controllerCameraTrans = false;
    void* mi_controllerCameraTrans = nullptr;
    bool has_vcamFollow = false;
    void* mi_vcamFollow = nullptr;
    bool has_vcamLookAt = false;
    void* mi_vcamLookAt = nullptr;
    bool has_trPosition = false;
    void* mi_trPosition = nullptr;
    // v1.0.0ai: UI 探测(隐藏 HUD) —— UnityEngine 侧的相机/渲染层读写, 全部可缺省
    bool has_allCamerasCount = false;
    void* mi_allCamerasCount = nullptr;
    bool has_allCameras = false;
    void* mi_allCameras = nullptr;
    bool has_camMainGet = false;
    void* mi_camMainGet = nullptr;
    bool has_camMaskGet = false;
    void* mi_camMaskGet = nullptr;
    bool has_camMaskSet = false;
    void* mi_camMaskSet = nullptr;
    bool has_camNameGet = false;
    void* mi_camNameGet = nullptr;
    bool has_camEnabledGet = false;
    void* mi_camEnabledGet = nullptr;
    bool has_camDepthGet = false;
    void* mi_camDepthGet = nullptr;
    bool has_camClearFlagsGet = false;
    void* mi_camClearFlagsGet = nullptr;
    bool has_camTargetTexGet = false;
    void* mi_camTargetTexGet = nullptr;
    // v1.0.0ar: 相机"清屏 / 启用 / 深度"的**写入**通道(指令 74 用)。
    // 为什么要: 遮罩=0 的相机**照样会清屏** —— 实测 ESC 界面态 UICamera depth=2、clearFlags=1,
    //   它在主相机(depth=0)之后渲染; 若它每帧擦掉颜色缓冲, 那"严格模式全黑"就不是"世界没渲染"
    //   而是**被清屏擦掉**。写 clearFlags(Depth=3 / Nothing=4)就能把这两种情况分开。
    bool has_camClearFlagsSet = false;
    void* mi_camClearFlagsSet = nullptr;
    bool has_camEnabledSet = false;
    void* mi_camEnabledSet = nullptr;
    bool has_camDepthSet = false;
    void* mi_camDepthSet = nullptr;
    // v1.0.0aj: 游戏自己的 UI 相机通道(由 1.0.0ai 体检实测存在)
    //   get_uiCamera()             -> Camera  名='UICamera' 遮罩=0x20(只渲染层5) depth=2
    //   _SetUICameraCullingMask    -> void    直接写 UI 相机遮罩
    //   Add/RemoveUICamCullingMaskConfig(string,int)->bool + _UpdateUICamCullingMask->void
    //                             游戏自己的"命名配置栈"(原生 X 隐藏 UI 走的就是这条)
    //   GetUICamDefaultCullingMask -> int     默认遮罩(实测 32)
    //   _CurrCameraNeedHideHUD()   -> bool    游戏自认为"当前该不该隐藏 HUD"(只读诊断)
    bool has_getUiCamera = false;
    void* mi_getUiCamera = nullptr;
    bool has_setUiCamMask = false;
    void* mi_setUiCamMask = nullptr;
    bool has_addUiCamCfg = false;
    void* mi_addUiCamCfg = nullptr;
    bool has_rmUiCamCfg = false;
    void* mi_rmUiCamCfg = nullptr;
    bool has_updUiCamCfg = false;
    void* mi_updUiCamCfg = nullptr;
    bool has_uiCamDefaultMask = false;
    void* mi_uiCamDefaultMask = nullptr;
    bool has_needHideHud = false;
    void* mi_needHideHud = nullptr;
    // v1.0.0am: "完全去掉 UI" —— 图层名表 + 主(世界)相机遮罩通道
    bool has_layerToName = false;
    void* mi_layerToName = nullptr;
    bool has_nameToLayer = false;
    void* mi_nameToLayer = nullptr;
    bool has_addMainCamCfg = false;
    void* mi_addMainCamCfg = nullptr;
    bool has_rmMainCamCfg = false;
    void* mi_rmMainCamCfg = nullptr;
    bool has_updMainCamCfg = false;
    void* mi_updMainCamCfg = nullptr;
    bool has_setMainCamMask = false;
    void* mi_setMainCamMask = nullptr;
    bool has_mainCamDefaultMask = false;
    void* mi_mainCamDefaultMask = nullptr;
    bool has_mainCamMaskGet = false;
    void* mi_mainCamMaskGet = nullptr;
    bool has_uiModelMask = false;
    void* mi_uiModelMask = nullptr;
    bool has_gachaMask = false;
    void* mi_gachaMask = nullptr;
    // 实例缓存
    void* cameraManager = nullptr;
    void* marketingController = nullptr;
    void* snapshotController = nullptr;
    bool resolved = false;
};

Sys g_sys;

void TryMethod(bool& flag, void*& slot, const char* ns, const char* cls,
               const char* method, int argc, const char* label) {
    auto m = ResolveMethod(kAsm, ns, cls, method, argc);
    if (m.ok) {
        flag = true;
        slot = m.methodInfo;
        LogG(std::string("[gamesys] OK  ") + label);
    } else {
        LogG(std::string("[gamesys] MISS ") + label);
    }
}

// 跨程序集解析(UnityEngine / Cinemachine 等不在 Gameplay.Beyond.dll 里)
void TryMethodAnywhere(bool& flag, void*& slot, const char* ns, const char* cls,
                       const char* method, int argc, const char* label) {
    auto m = ResolveMethodAnywhere(ns, cls, method, argc);
    if (m.ok) {
        flag = true;
        slot = m.methodInfo;
        LogG(std::string("[gamesys] OK  ") + label);
    } else {
        LogG(std::string("[gamesys] MISS ") + label);
    }
}

// 带异常诊断的托管调用: 调用前把当前线程附加到 IL2CPP 域(防 GC 致命错误)
void* InvokeLogged(void* methodInfo, void* obj, void** params, const char* label) {
    if (!methodInfo) return nullptr;
    const Il2CppApi& a = Il2Cpp();
    if (!a.loaded) return nullptr;
    if (a.thread_attach && a.domain_get) {
        void* d = a.domain_get();
        if (d) a.thread_attach(d);
    }
    void* exc = nullptr;
    void* r = a.runtime_invoke(methodInfo, obj, params, &exc);
    if (exc) {
        LogG(std::string("[gamesys] 调用 ") + label + " 抛出托管异常");
        return nullptr;
    }
    return r;
}

// 取某个托管类的 Il2CppClass*(纯元数据)
void* ClassOf(const char* ns, const char* cls) {
    return ResolveClass(kAsm, ns, cls);
}

void* TypeOfClass(const char* ns, const char* cls) {
    void* k = ClassOf(ns, cls);
    const Il2CppApi& a = Il2Cpp();
    if (!k || !a.class_get_type) return nullptr;
    return a.class_get_type(k);
}

// 元数据类型名(只读元数据)
std::string TypeName(void* type) {
    const Il2CppApi& a = Il2Cpp();
    if (!type || !a.class_from_type || !a.class_get_name) return "?";
    void* k = a.class_from_type(type);
    if (!k) return "?";
    const char* n = a.class_get_name(k);
    const char* ns = a.class_get_namespace ? a.class_get_namespace(k) : nullptr;
    std::string s;
    if (ns && *ns) {
        s += ns;
        s += ".";
    }
    s += (n ? n : "?");
    return s;
}

// 读托管数组的首个可用元素 —— 已废弃(v8 删除调用方), 保留说明避免后人重蹈覆辙:
// 该路径每帧枚举场景对象会导致内存耗尽崩溃, 见下方 ResolveSnapshotByCreateTemp 的注释。

// CameraManager 实例: 静态 getter
void* CameraManagerInstance() {
    if (g_sys.cameraManager) return g_sys.cameraManager;
    if (!g_sys.has_getCameraManager) return nullptr;
    void* obj = InvokeLogged(g_sys.mi_getCameraManager, nullptr, nullptr,
                             "CameraUtils.get_cameraManager");
    if (obj) {
        g_sys.cameraManager = obj;
        LogG("[gamesys] CameraManager 实例: OK");
    }
    return obj;
}

// 注意(重要修复 v8): 这里曾用 Object.FindObjectsOfType(Type) /
// Resources.FindObjectsOfTypeAll(Type) 兜底找快照相机。两条都删掉了, 原因是实测危害极大:
//   1) 本作里 FindObjectsOfType 实际返回 null(不可用), FindObjectOfType(Type,bool) 抛异常;
//   2) Unity 侧反复报 "FindAllObjectsOfType: The type has to be derived from
//      UnityEngine.Object. Type is Boolean.";
//   3) 若把这条路径放进每帧数据流(Blender 60fps 推送), 每帧都会枚举场景全部对象,
//      内存被迅速吃光, 最终 [Critical] Commit memory failed 导致引擎崩溃。
// 结论: 获取快照相机实例只保留 ToggleSnapshotCamera 返回值这一条路, 且只由显式指令触发。
bool ResolveSnapshotByCreateTemp() {
    if (g_sys.snapshotController) return true;
    if (!g_sys.has_toggleSnapshot) {
        LogG("[gamesys] ToggleSnapshotCamera 不可用, 无法获取快照相机实例");
        return false;
    }
    LogG("[gamesys] ToggleSnapshotCamera(true,false) 取快照相机实例(会进入拍照模式)");
    GameSysToggleSnapshotCamera(1.f, 0.f);
    return g_sys.snapshotController != nullptr;
}

}  // namespace

void GameSysSetLog(GameSysLogFn fn) { g_log = fn; }

bool GameSysResolve() {
    if (g_sys.resolved) return true;
    LogG("[gamesys] 解析游戏相机系统契约(v4: 只走托管调用)...");
    TryMethod(g_sys.has_getCameraManager, g_sys.mi_getCameraManager, "Beyond.Gameplay.View",
              "CameraUtils", "get_cameraManager", 0, "CameraUtils.get_cameraManager");
    TryMethod(g_sys.has_openMarketing, g_sys.mi_openMarketing, "Beyond.Gameplay.View",
              "CameraUtils", "OpenMarketingCamera", 0, "CameraUtils.OpenMarketingCamera");
    TryMethod(g_sys.has_closeMarketing, g_sys.mi_closeMarketing, "Beyond.Gameplay.View",
              "CameraUtils", "CloseMarketingCamera", 0, "CameraUtils.CloseMarketingCamera");
    TryMethod(g_sys.has_getDofData, g_sys.mi_getDofData, "Beyond.Gameplay.View",
              "CameraUtils", "GetDOFData", 0, "CameraUtils.GetDOFData");
    TryMethod(g_sys.has_recenter, g_sys.mi_recenter, "Beyond.Gameplay.View",
              "CameraUtils", "RecenterCamera", 0, "CameraUtils.RecenterCamera");
    TryMethod(g_sys.has_resetCamPos, g_sys.mi_resetCamPos, "Beyond.Gameplay.View",
              "CameraUtils", "ResetCameraPosition", 0, "CameraUtils.ResetCameraPosition");
    TryMethod(g_sys.has_getMainMarketing, g_sys.mi_getMainMarketing, "Beyond.Gameplay.View",
              "CameraManager", "GetMainMarketingCameraController", 0,
              "CameraManager.GetMainMarketingCameraController");
    TryMethod(g_sys.has_getMainLevel, g_sys.mi_getMainLevel, "Beyond.Gameplay.View",
              "CameraManager", "GetMainLevelCameraController", 0,
              "CameraManager.GetMainLevelCameraController");
    TryMethod(g_sys.has_setOverrideFov, g_sys.mi_setOverrideFov, "Beyond.Gameplay.View",
              "CameraManager", "SetOverrideFOVForCurrCamera", 1,
              "CameraManager.SetOverrideFOVForCurrCamera");
    TryMethod(g_sys.has_toggleSnapshot, g_sys.mi_toggleSnapshot, "Beyond.Gameplay.View",
              "CameraManager", "ToggleSnapshotCamera", 2,
              "CameraManager.ToggleSnapshotCamera");
    TryMethod(g_sys.has_createTemp, g_sys.mi_createTemp, "Beyond.Gameplay.View",
              "CameraManager", "CreateOrGetTemporaryController", 1,
              "CameraManager.CreateOrGetTemporaryController");
    TryMethod(g_sys.has_enableDof, g_sys.mi_enableDof, "Beyond.Gameplay.View",
              "CameraUtils", "EnableDOF", 1, "CameraUtils.EnableDOF");
    TryMethod(g_sys.has_marketingChangeFov, g_sys.mi_marketingChangeFov,
              "Beyond.Gameplay.View", "MarketingCameraController", "ChangeFov", 1,
              "MarketingCameraController.ChangeFov");
    TryMethod(g_sys.has_marketingMoveInput, g_sys.mi_marketingMoveInput,
              "Beyond.Gameplay.View", "MarketingCameraController", "SetMoveInput", 2,
              "MarketingCameraController.SetMoveInput(2)");
    TryMethod(g_sys.has_marketingRotateInput, g_sys.mi_marketingRotateInput,
              "Beyond.Gameplay.View", "MarketingCameraController", "SetRotateInput", 2,
              "MarketingCameraController.SetRotateInput(2)");
    TryMethod(g_sys.has_marketingZoom, g_sys.mi_marketingZoom, "Beyond.Gameplay.View",
              "MarketingCameraController", "Zoom", 1,
              "MarketingCameraController.Zoom(1)");
    TryMethod(g_sys.has_snapSetAperture, g_sys.mi_snapSetAperture, "Beyond.Gameplay.View",
              "SnapshotCameraController", "SetAperture", 1,
              "SnapshotCameraController.SetAperture");
    TryMethod(g_sys.has_snapSetFocus, g_sys.mi_snapSetFocus, "Beyond.Gameplay.View",
              "SnapshotCameraController", "SetFocusDistance", 1,
              "SnapshotCameraController.SetFocusDistance");
    TryMethod(g_sys.has_snapAdditiveFov, g_sys.mi_snapAdditiveFov, "Beyond.Gameplay.View",
              "SnapshotCameraController", "SetAdditiveFOV", 1,
              "SnapshotCameraController.SetAdditiveFOV");
    TryMethod(g_sys.has_snapSetZoom, g_sys.mi_snapSetZoom, "Beyond.Gameplay.View",
              "SnapshotCameraController", "SetZoomScale", 1,
              "SnapshotCameraController.SetZoomScale");
    TryMethod(g_sys.has_snapGetZoom, g_sys.mi_snapGetZoom, "Beyond.Gameplay.View",
              "SnapshotCameraController", "GetZoomScale", 0,
              "SnapshotCameraController.GetZoomScale");
    TryMethod(g_sys.has_snapActivate, g_sys.mi_snapActivate, "Beyond.Gameplay.View",
              "SnapshotCameraController", "ActivateSnapshotCamera", 0,
              "SnapshotCameraController.ActivateSnapshotCamera");
    TryMethod(g_sys.has_snapDeactivate, g_sys.mi_snapDeactivate, "Beyond.Gameplay.View",
              "SnapshotCameraController", "DeactivateSnapshotCamera", 0,
              "SnapshotCameraController.DeactivateSnapshotCamera");
    TryMethod(g_sys.has_snapUpdateSensor, g_sys.mi_snapUpdateSensor,
              "Beyond.Gameplay.View", "SnapshotCameraController", "UpdateSensorSize", 0,
              "SnapshotCameraController.UpdateSensorSize");
    TryMethod(g_sys.has_snapIsFirstPerson, g_sys.mi_snapIsFirstPerson,
              "Beyond.Gameplay.View", "SnapshotCameraController", "get_isFirstPerson", 0,
              "SnapshotCameraController.get_isFirstPerson");
    // 元数据枚举(只读元数据, 安全): 类成员清单
    TryMethod(g_sys.has_snapApplyDof, g_sys.mi_snapApplyDof, "Beyond.Gameplay.View",
              "SnapshotCameraController", "ApplySnapshotDofSettings", 0,
              "SnapshotCameraController.ApplySnapshotDofSettings");
    // v1.0.0as: 与 Apply 配对的 Reset(把快照相机的景深设置恢复成游戏默认)
    TryMethod(g_sys.has_snapResetDof, g_sys.mi_snapResetDof, "Beyond.Gameplay.View",
              "SnapshotCameraController", "ResetSnapshotDofSettings", 0,
              "SnapshotCameraController.ResetSnapshotDofSettings");
    // v1.1: 拍照相机的相机偏移(只读诊断用: 角色位置 = 相机位置 − 偏移)
    TryMethod(g_sys.has_snapGetOffset, g_sys.mi_snapGetOffset, "Beyond.Gameplay.View",
              "SnapshotCameraController", "GetCameraOffset", 0,
              "SnapshotCameraController.GetCameraOffset");
    // v1.3: 写入侧(方案 B 的着力点)。参数是 Vector3(值类型), 调用约定由指令 45 现场标定。
    TryMethod(g_sys.has_snapSetOffset, g_sys.mi_snapSetOffset, "Beyond.Gameplay.View",
              "SnapshotCameraController", "SetCameraOffset", 1,
              "SnapshotCameraController.SetCameraOffset");
    // v1.2: 锚点候选(找"角色位置"的几条路; 失败不致命)
    TryMethod(g_sys.has_curVirtualCam, g_sys.mi_curVirtualCam, "Beyond.Gameplay.View",
              "CameraManager", "get_curVirtualCam", 0, "CameraManager.get_curVirtualCam");
    TryMethod(g_sys.has_curActiveController, g_sys.mi_curActiveController,
              "Beyond.Gameplay.View", "CameraManager", "get_curActiveController", 0,
              "CameraManager.get_curActiveController");
    TryMethod(g_sys.has_controllerCameraTrans, g_sys.mi_controllerCameraTrans,
              "Beyond.Gameplay.View", "CameraControllerBase", "get_cameraTrans", 0,
              "CameraControllerBase.get_cameraTrans");
    TryMethodAnywhere(g_sys.has_vcamFollow, g_sys.mi_vcamFollow, "Cinemachine",
                      "CinemachineVirtualCamera", "get_Follow", 0,
                      "CinemachineVirtualCamera.get_Follow(跨程序集)");
    TryMethodAnywhere(g_sys.has_vcamLookAt, g_sys.mi_vcamLookAt, "Cinemachine",
                      "CinemachineVirtualCamera", "get_LookAt", 0,
                      "CinemachineVirtualCamera.get_LookAt(跨程序集)");
    TryMethodAnywhere(g_sys.has_trPosition, g_sys.mi_trPosition, "UnityEngine", "Transform",
                      "get_position", 0, "UnityEngine.Transform.get_position(跨程序集)");

    // v1.0.0ai: UI 探测用的 UnityEngine.Camera 能力(全部可缺省; 缺了只影响体检/写测)。
    // 注意: 故意先试 Camera 再试父类 —— IL2CPP 的 methods 表通常含继承方法, 但版本间有差异。
    TryMethodAnywhere(g_sys.has_camMainGet, g_sys.mi_camMainGet, "UnityEngine", "Camera",
                      "get_main", 0, "Camera.get_main");
    TryMethodAnywhere(g_sys.has_allCamerasCount, g_sys.mi_allCamerasCount, "UnityEngine",
                      "Camera", "get_allCamerasCount", 0, "Camera.get_allCamerasCount");
    TryMethodAnywhere(g_sys.has_allCameras, g_sys.mi_allCameras, "UnityEngine", "Camera",
                      "get_allCameras", 0, "Camera.get_allCameras");
    TryMethodAnywhere(g_sys.has_camMaskGet, g_sys.mi_camMaskGet, "UnityEngine", "Camera",
                      "get_cullingMask", 0, "Camera.get_cullingMask");
    TryMethodAnywhere(g_sys.has_camMaskSet, g_sys.mi_camMaskSet, "UnityEngine", "Camera",
                      "set_cullingMask", 1, "Camera.set_cullingMask");
    TryMethodAnywhere(g_sys.has_camDepthGet, g_sys.mi_camDepthGet, "UnityEngine", "Camera",
                      "get_depth", 0, "Camera.get_depth");
    TryMethodAnywhere(g_sys.has_camClearFlagsGet, g_sys.mi_camClearFlagsGet,
                      "UnityEngine", "Camera", "get_clearFlags", 0,
                      "Camera.get_clearFlags");
    // v1.0.0ar: 清屏/启用/深度的写入通道(可缺省; 缺了只是指令 74 不可用)
    TryMethodAnywhere(g_sys.has_camClearFlagsSet, g_sys.mi_camClearFlagsSet, "UnityEngine",
                      "Camera", "set_clearFlags", 1, "Camera.set_clearFlags");
    TryMethodAnywhere(g_sys.has_camEnabledSet, g_sys.mi_camEnabledSet, "UnityEngine", "Camera",
                      "set_enabled", 1, "Camera.set_enabled");
    if (!g_sys.has_camEnabledSet)
        TryMethodAnywhere(g_sys.has_camEnabledSet, g_sys.mi_camEnabledSet, "UnityEngine",
                          "Behaviour", "set_enabled", 1, "Behaviour.set_enabled");
    TryMethodAnywhere(g_sys.has_camDepthSet, g_sys.mi_camDepthSet, "UnityEngine", "Camera",
                      "set_depth", 1, "Camera.set_depth");
    TryMethodAnywhere(g_sys.has_camTargetTexGet, g_sys.mi_camTargetTexGet, "UnityEngine",
                      "Camera", "get_targetTexture", 0, "Camera.get_targetTexture");
    TryMethodAnywhere(g_sys.has_camNameGet, g_sys.mi_camNameGet, "UnityEngine", "Camera",
                      "get_name", 0, "Camera.get_name");
    if (!g_sys.has_camNameGet)
        TryMethodAnywhere(g_sys.has_camNameGet, g_sys.mi_camNameGet, "UnityEngine",
                          "Object", "get_name", 0, "Object.get_name");
    TryMethodAnywhere(g_sys.has_camEnabledGet, g_sys.mi_camEnabledGet, "UnityEngine",
                      "Camera", "get_enabled", 0, "Camera.get_enabled");
    if (!g_sys.has_camEnabledGet)
        TryMethodAnywhere(g_sys.has_camEnabledGet, g_sys.mi_camEnabledGet, "UnityEngine",
                          "Behaviour", "get_enabled", 0, "Behaviour.get_enabled");
    // v1.0.0aj: 游戏自己的 UI 相机通道(1.0.0ai 体检在 CameraManager 上实测到这些成员)
    TryMethod(g_sys.has_getUiCamera, g_sys.mi_getUiCamera, "Beyond.Gameplay.View",
              "CameraManager", "get_uiCamera", 0, "CameraManager.get_uiCamera");
    TryMethod(g_sys.has_setUiCamMask, g_sys.mi_setUiCamMask, "Beyond.Gameplay.View",
              "CameraManager", "_SetUICameraCullingMask", 1,
              "CameraManager._SetUICameraCullingMask(int)");
    TryMethod(g_sys.has_addUiCamCfg, g_sys.mi_addUiCamCfg, "Beyond.Gameplay.View",
              "CameraManager", "AddUICamCullingMaskConfig", 2,
              "CameraManager.AddUICamCullingMaskConfig(string,int)");
    TryMethod(g_sys.has_rmUiCamCfg, g_sys.mi_rmUiCamCfg, "Beyond.Gameplay.View",
              "CameraManager", "RemoveUICamCullingMaskConfig", 1,
              "CameraManager.RemoveUICamCullingMaskConfig(string)");
    TryMethod(g_sys.has_updUiCamCfg, g_sys.mi_updUiCamCfg, "Beyond.Gameplay.View",
              "CameraManager", "_UpdateUICamCullingMask", 0,
              "CameraManager._UpdateUICamCullingMask()");
    TryMethod(g_sys.has_uiCamDefaultMask, g_sys.mi_uiCamDefaultMask, "Beyond.Gameplay.View",
              "CameraManager", "GetUICamDefaultCullingMask", 0,
              "CameraManager.GetUICamDefaultCullingMask()");
    TryMethod(g_sys.has_needHideHud, g_sys.mi_needHideHud, "Beyond.Gameplay.View",
              "CameraManager", "_CurrCameraNeedHideHUD", 0,
              "CameraManager._CurrCameraNeedHideHUD()");
    // v1.0.0am: 图层名表 + 主(世界)相机遮罩的写入通道(全部可缺省, 缺了只影响"完全去掉 UI"那一档)
    TryMethodAnywhere(g_sys.has_layerToName, g_sys.mi_layerToName, "UnityEngine", "LayerMask",
                      "LayerToName", 1, "LayerMask.LayerToName(int)");
    TryMethodAnywhere(g_sys.has_nameToLayer, g_sys.mi_nameToLayer, "UnityEngine", "LayerMask",
                      "NameToLayer", 1, "LayerMask.NameToLayer(string)");
    TryMethod(g_sys.has_addMainCamCfg, g_sys.mi_addMainCamCfg, "Beyond.Gameplay.View",
              "CameraManager", "AddMainCamCullingMaskConfig", 2,
              "CameraManager.AddMainCamCullingMaskConfig(string,int)");
    TryMethod(g_sys.has_rmMainCamCfg, g_sys.mi_rmMainCamCfg, "Beyond.Gameplay.View",
              "CameraManager", "RemoveMainCamCullingMaskConfig", 1,
              "CameraManager.RemoveMainCamCullingMaskConfig(string)");
    TryMethod(g_sys.has_updMainCamCfg, g_sys.mi_updMainCamCfg, "Beyond.Gameplay.View",
              "CameraManager", "_UpdateMainCamCullingMask", 0,
              "CameraManager._UpdateMainCamCullingMask()");
    TryMethod(g_sys.has_setMainCamMask, g_sys.mi_setMainCamMask, "Beyond.Gameplay.View",
              "CameraManager", "_SetMainCameraCullingMask", 1,
              "CameraManager._SetMainCameraCullingMask(int)");
    TryMethod(g_sys.has_mainCamDefaultMask, g_sys.mi_mainCamDefaultMask, "Beyond.Gameplay.View",
              "CameraManager", "GetMainCamDefaultCullingMask", 0,
              "CameraManager.GetMainCamDefaultCullingMask()");
    TryMethod(g_sys.has_mainCamMaskGet, g_sys.mi_mainCamMaskGet, "Beyond.Gameplay.View",
              "CameraManager", "GetMainCameraCullingMask", 0,
              "CameraManager.GetMainCameraCullingMask()");
    TryMethod(g_sys.has_uiModelMask, g_sys.mi_uiModelMask, "Beyond.Gameplay.View",
              "CameraManager", "GetUIModelLayerMask", 1,
              "CameraManager.GetUIModelLayerMask(int)");
    TryMethod(g_sys.has_gachaMask, g_sys.mi_gachaMask, "Beyond.Gameplay.View",
              "CameraManager", "GetGachaLayerMask", 1,
              "CameraManager.GetGachaLayerMask(int)");

    // 注意: 这里刻意不解析 Unity 的 FindObjectsOfType / FindObjectOfType / FindObjectsOfTypeAll。
    // 三者在本作的实测结果与危害见 ResolveSnapshotByCreateTemp 上方注释(v8 已彻底移除)。

    // 注意: 解析阶段只做元数据解析, 绝不调用托管方法
    g_sys.resolved = g_sys.has_openMarketing || g_sys.has_getMainMarketing;
    return g_sys.resolved;
}

void GameSysReport() {
    char buf[640];
    snprintf(buf, sizeof(buf),
             "[gamesys] 契约: openMarketing=%d closeMarketing=%d getMainMarketing=%d "
             "setOverrideFov=%d marketingChangeFov=%d moveInput=%d rotateInput=%d "
             "zoom=%d snapAperture=%d snapFocus=%d snapZoom=%d snapActivate=%d "
             "createTemp=%d toggleSnapshot=%d enableDof=%d recenter=%d "
             "cameraManager=%p marketing=%p snapshot=%p",
             g_sys.has_openMarketing, g_sys.has_closeMarketing,
             g_sys.has_getMainMarketing, g_sys.has_setOverrideFov,
             g_sys.has_marketingChangeFov, g_sys.has_marketingMoveInput,
             g_sys.has_marketingRotateInput, g_sys.has_marketingZoom,
             g_sys.has_snapSetAperture, g_sys.has_snapSetFocus, g_sys.has_snapSetZoom,
             g_sys.has_snapActivate, g_sys.has_createTemp, g_sys.has_toggleSnapshot,
             g_sys.has_enableDof, g_sys.has_recenter, g_sys.cameraManager,
             g_sys.marketingController, g_sys.snapshotController);
    LogG(buf);
}

bool GameSysSetMarketingGate(bool on) {
    // 实测: OpenMarketingCamera 无需该门控即可生效(自由相机成功开启),
    // 而该门控字段/属性在加固后的元数据里都不存在, 故不再尝试。
    LogG(std::string("[gamesys] 门控跳过(实测不需要) on=") + (on ? "1" : "0"));
    return false;
}

bool GameSysOpenMarketingCamera() {
    if (!g_sys.has_openMarketing) return false;
    InvokeLogged(g_sys.mi_openMarketing, nullptr, nullptr, "OpenMarketingCamera");
    LogG("[gamesys] OpenMarketingCamera() 已调用");
    return true;
}

bool GameSysCloseMarketingCamera() {
    if (!g_sys.has_closeMarketing) return false;
    InvokeLogged(g_sys.mi_closeMarketing, nullptr, nullptr, "CloseMarketingCamera");
    LogG("[gamesys] CloseMarketingCamera() 已调用");
    return true;
}

bool GameSysSetOverrideFov(float fov) {
    if (!g_sys.has_setOverrideFov) return false;
    void* mgr = CameraManagerInstance();
    if (!mgr) return false;
    float v = fov;
    void* p[1] = {&v};
    InvokeLogged(g_sys.mi_setOverrideFov, mgr, p, "SetOverrideFOVForCurrCamera");
    char b[128];
    snprintf(b, sizeof(b), "[gamesys] SetOverrideFOVForCurrCamera(%.2f)", fov);
    LogG(b);
    return true;
}

bool GameSysMarketingChangeFov(float rate) {
    if (!g_sys.has_marketingChangeFov || !g_sys.marketingController) return false;
    // 签名自省结果: ChangeFov 的参数是 System.Int32 而不是 Single!
    // (v2 曾误传 -15.0f, 其位模式 0xC1700000 被当作 -1047527424 的巨大负速率,
    //  把 FOV 一路拖成负数 -> 投影矩阵失效 -> 灰屏。类型必须严格匹配。)
    int32_t v = static_cast<int32_t>(rate);
    if (v < -10) v = -10;
    if (v > 10) v = 10;
    void* p[1] = {&v};
    InvokeLogged(g_sys.mi_marketingChangeFov, g_sys.marketingController, p,
                 "MarketingCameraController.ChangeFov");
    char b[128];
    snprintf(b, sizeof(b), "[gamesys] MarketingCameraController.ChangeFov(int %d)", v);
    LogG(b);
    return true;
}

bool GameSysMarketingMoveInput(float x, float y) {
    if (!g_sys.has_marketingMoveInput || !g_sys.marketingController) {
        LogG("[gamesys] SetMoveInput 不可用或营销相机未就绪");
        return false;
    }
    float p[2] = {x, y};
    void* args[1] = {p};
    InvokeLogged(g_sys.mi_marketingMoveInput, g_sys.marketingController, args,
                 "SetMoveInput");
    char b[128];
    snprintf(b, sizeof(b), "[gamesys] SetMoveInput(%.3f, %.3f)", x, y);
    LogG(b);
    return true;
}

bool GameSysMarketingRotateInput(float x, float y) {
    if (!g_sys.has_marketingRotateInput || !g_sys.marketingController) {
        LogG("[gamesys] SetRotateInput 不可用或营销相机未就绪");
        return false;
    }
    float p[2] = {x, y};
    void* args[1] = {p};
    InvokeLogged(g_sys.mi_marketingRotateInput, g_sys.marketingController, args,
                 "SetRotateInput");
    char b[128];
    snprintf(b, sizeof(b), "[gamesys] SetRotateInput(%.3f, %.3f)", x, y);
    LogG(b);
    return true;
}

bool GameSysMarketingZoom(float delta) {
    if (!g_sys.has_marketingZoom || !g_sys.marketingController) {
        LogG("[gamesys] Zoom 不可用或营销相机未就绪");
        return false;
    }
    float v = delta;
    void* args[1] = {&v};
    InvokeLogged(g_sys.mi_marketingZoom, g_sys.marketingController, args, "Zoom");
    char b[128];
    snprintf(b, sizeof(b), "[gamesys] MarketingCameraController.Zoom(%.3f)", delta);
    LogG(b);
    return true;
}

bool GameSysRecenterCamera() {
    if (!g_sys.has_recenter) return false;
    InvokeLogged(g_sys.mi_recenter, nullptr, nullptr, "RecenterCamera");
    LogG("[gamesys] CameraUtils.RecenterCamera()");
    return true;
}

bool GameSysResetCameraPosition() {
    if (!g_sys.has_resetCamPos) return false;
    InvokeLogged(g_sys.mi_resetCamPos, nullptr, nullptr, "ResetCameraPosition");
    LogG("[gamesys] CameraUtils.ResetCameraPosition()");
    return true;
}

bool GameSysResolveSnapshotInstance() { return ResolveSnapshotByCreateTemp(); }
bool GameSysSnapshotInstanceReady() { return g_sys.snapshotController != nullptr; }

// v1.2: 实例类型校验 —— 只有当实例的类确实是"声明类或其子类"时才调用,
// 避免把方法用错对象(项目铁律: 不符就不调用)。
// v1.3: 判定从"Il2CppClass 指针相等"放宽为"类名出现在对象的父类链里"。
//   原因: IL2CPP 里同一个类可能存在多份 Il2CppClass 对象(泛型实例化 / 程序集链接 /
//   类型转发), 指针相等会把**合法实例**拦掉 —— 实测 get_curVirtualCam 就是这样被拦的。
//   类名比对仍然是真正的类型比对(只是换了比较键), 且只在名字完全相等时放行。
bool InstanceIsA(void* declaringMethod, void* obj) {
    if (!obj) return false;
    const Il2CppApi& a = Il2Cpp();
    if (!a.object_get_class || !a.method_get_return_type || !a.class_from_type ||
        !a.class_get_name)
        return true;  // 判不出则放行
    void* retType = a.method_get_return_type(declaringMethod);
    if (!retType) return true;
    void* declared = a.class_from_type(retType);
    if (!declared) return true;
    const char* declaredName = a.class_get_name(declared);
    if (!declaredName) return true;
    void* k = a.object_get_class(obj);
    int guard = 0;
    while (k && guard++ < 64) {
        if (k == declared) return true;
        const char* n = a.class_get_name(k);
        if (n && std::strcmp(n, declaredName) == 0) return true;
        if (!a.class_get_parent) return true;
        k = a.class_get_parent(k);
    }
    return false;
}

// v1.4: vcam 对象的可用性判定。
// 血泪: get_curVirtualCam 的**声明返回类型是接口** Cinemachine.ICinemachineCamera,
// 而接口不在类继承链上 —— 所以"按声明类比对父类链"必然失败(实测一直因此被拦下,
// 实际对象却是正牌虚拟相机: Beyond.Gameplay.View.LevelVirtualCamera /
// MarketingVirtualCamera < Cinemachine.CinemachineVirtualCamera < ...Base)。
// 正确做法: 按**将要调用的方法所属的类**校验 —— get_Follow/get_LookAt 声明在
// Cinemachine.CinemachineVirtualCamera(Base) 上, 于是沿父链能查到该类名即可调用。
bool VcamObjectUsable(void* obj) {
    return GameSysObjectIsNamed(obj, "CinemachineVirtualCamera") ||
           GameSysObjectIsNamed(obj, "CinemachineVirtualCameraBase");
}

// 无参 getter 调用
void* CallGetter(void* methodInfo, void* self, const char* label) {
    if (!methodInfo || !self) return nullptr;
    return InvokeLogged(methodInfo, self, nullptr, label);
}

// 限流日志: 锚点候选在采样期间(指令 46)会**每帧**被调用, 不可用时若每次都打日志,
// 一次 600 帧采样会刷出上千行(既噪声大也拖慢)。这里每 300 次只留一条。
void LogThrottled(const char* msg) {
    static unsigned n = 0;
    if ((n++ % 300u) == 0u) LogG(msg);
}

// 取 Transform 的世界位置(装箱 Vector3 需 unbox)
bool TransformPos(void* transform, float out[3]) {
    if (!transform || !g_sys.has_trPosition) return false;
    const Il2CppApi& a = Il2Cpp();
    if (!a.loaded || !a.object_unbox) return false;
    void* boxed = InvokeLogged(g_sys.mi_trPosition, transform, nullptr, "Transform.get_position");
    if (!boxed) return false;
    void* raw = a.object_unbox(boxed);
    if (!raw) return false;
    const float* f = static_cast<const float*>(raw);
    out[0] = f[0];
    out[1] = f[1];
    out[2] = f[2];
    return true;
}

const char* GameSysAnchorSourceName(int source) {
    switch (source) {
        case kAnchorVcamFollow: return "vcamFollow";
        case kAnchorVcamLookAt: return "vcamLookAt";
        case kAnchorControllerTrans: return "controllerTrans";
        case kAnchorCamMinusOffset: return "camMinusOffset";
        case kAnchorEngineCam: return "engineCam";
        default: return "armedPoint";
    }
}

const char* GameSysAnchorSourceLabel(int source) {
    switch (source) {
        case kAnchorVcamFollow: return "vcam 的 Follow 目标";
        case kAnchorVcamLookAt: return "vcam 的 LookAt 目标";
        case kAnchorControllerTrans: return "当前控制器的 cameraTrans";
        case kAnchorCamMinusOffset: return "引擎相机位置 − 原生偏移";
        case kAnchorEngineCam: return "引擎相机位置(游戏自己的机位, 带游戏平滑/跟随阻尼)";
        default: return "武装那一刻的相机位置";
    }
}

bool GameSysAnchorPosition(int source, const float enginePos[3], float out[3]);

namespace {

// v1.5: 锚点粘性缓存。
// 背景: 自由相机打开时(也就是"拍片工作状态"), get_curVirtualCam 返回的是自由相机自己的
// MarketingVirtualCamera, 它的 Follow 是 null —— 实测自动校准因此报"候选不可用"。
// 但角色锚点本身是(近似)静止的, 而"武装那一刻(自由相机还没开)读到的值"完全够用。
// 所以: 缓存最近一次成功读到的值, 后续读取失败时返回缓存(并限流提示其年龄)。
struct StickyAnchor {
    bool valid[kAnchorSourceCount] = {};
    float v[kAnchorSourceCount][3] = {};
    uint64_t ms[kAnchorSourceCount] = {};
};
StickyAnchor g_sticky;

// v1.0.0ae: 最近一次 GameSysAnchorPosition 的返回值是"实时读到"还是"退回了缓存"。
// 逐帧追踪要靠它区分: 原点滞后一帧, 到底是"实时读失败→用旧缓存", 还是"值本身就晚一帧"。
bool g_anchorLastLive = false;

bool AnchorPositionLive(int source, const float enginePos[3], float out[3]);

}  // namespace

bool GameSysAnchorPosition(int source, const float enginePos[3], float out[3]) {
    const bool ok = AnchorPositionLive(source, enginePos, out);
    g_anchorLastLive = ok;                       // v1.0.0ae: 供逐帧追踪区分"实时值/缓存值"
    if (ok) {
        if (source > 0 && source < kAnchorSourceCount) {
            g_sticky.valid[source] = true;
            g_sticky.v[source][0] = out[0];
            g_sticky.v[source][1] = out[1];
            g_sticky.v[source][2] = out[2];
            g_sticky.ms[source] = GetTickCount64();
        }
        return true;
    }
    if (source > 0 && source < kAnchorSourceCount && g_sticky.valid[source]) {
        out[0] = g_sticky.v[source][0];
        out[1] = g_sticky.v[source][1];
        out[2] = g_sticky.v[source][2];
        char b[256];
        const uint64_t age = GetTickCount64() - g_sticky.ms[source];
        snprintf(b, sizeof(b),
                 "[anchor] %s 实时读取失败, 改用缓存值(%.3f, %.3f, %.3f), 缓存年龄 %.1f 秒",
                 GameSysAnchorSourceName(source), out[0], out[1], out[2], age / 1000.0);
        LogThrottled(b);
        return true;
    }
    return false;
}

bool GameSysAnchorLastWasLive() { return g_anchorLastLive; }

namespace {

bool AnchorPositionLive(int source, const float enginePos[3], float out[3]) {
    switch (source) {
        case kAnchorVcamFollow:
        case kAnchorVcamLookAt: {
            if (!g_sys.has_curVirtualCam || !g_sys.has_trPosition) return false;
            void* mgr = CameraManagerInstance();
            if (!mgr) return false;
            void* vcam = CallGetter(g_sys.mi_curVirtualCam, mgr, "get_curVirtualCam");
            if (!vcam) return false;
            if (!VcamObjectUsable(vcam)) {
                LogThrottled("[anchor] get_curVirtualCam 返回的对象不是 Cinemachine 虚拟相机, 跳过");
                return false;
            }
            void* mi = (source == kAnchorVcamFollow) ? g_sys.mi_vcamFollow : g_sys.mi_vcamLookAt;
            if (!mi) return false;
            void* tr = CallGetter(mi, vcam, "vcam 目标");
            if (!tr) return false;
            return TransformPos(tr, out);
        }
        case kAnchorControllerTrans: {
            if (!g_sys.has_curActiveController || !g_sys.has_controllerCameraTrans ||
                !g_sys.has_trPosition)
                return false;
            void* mgr = CameraManagerInstance();
            if (!mgr) return false;
            void* ctl = CallGetter(g_sys.mi_curActiveController, mgr, "get_curActiveController");
            if (!ctl) return false;
            if (!InstanceIsA(g_sys.mi_curActiveController, ctl)) {
                LogThrottled("[anchor] get_curActiveController 返回的对象类型与声明不符, 跳过");
                return false;
            }
            void* tr = CallGetter(g_sys.mi_controllerCameraTrans, ctl, "get_cameraTrans");
            if (!tr) return false;
            return TransformPos(tr, out);
        }
        case kAnchorCamMinusOffset: {
            float off[3] = {0.f, 0.f, 0.f};
            if (!GameSysSnapshotCameraOffset(off)) return false;
            out[0] = enginePos[0] - off[0];
            out[1] = enginePos[1] - off[1];
            out[2] = enginePos[2] - off[2];
            return true;
        }
        case kAnchorEngineCam: {
            // v1.0.0ab(3 号方案): 原点 = 引擎相机自己这一帧的位置。
            // enginePos 是钩子在"写入之前"读到的值(Brain 本帧刚算出的游戏机位), 因此:
            //   · 它天然带游戏的跟随阻尼与逐帧平滑(这正是随角色模式缺的那一层);
            //   · 它不是反馈量: 我们每帧覆盖 Camera.main, 下一帧 Brain 又按游戏逻辑重新算一遍,
            //     所以不会累积漂移。
            // 若游戏相机本身是静止的(例如拍照模式下的快照相机), 本来源等价于定镜头 —— 这是
            // 该方案唯一的硬前提, 用指令 43/46 看引擎相机极差即可判定。
            out[0] = enginePos[0];
            out[1] = enginePos[1];
            out[2] = enginePos[2];
            return true;
        }
        default:
            return false;
    }
}

}  // namespace

void GameSysReportAnchorCandidates(const float enginePos[3]) {
    char b[320];
    snprintf(b, sizeof(b),
             "[anchor] 引擎相机 pos=(%.3f, %.3f, %.3f) | 解析情况: curVirtualCam=%d "
             "curActiveController=%d controllerCameraTrans=%d vcamFollow=%d vcamLookAt=%d "
             "transformPos=%d cameraOffset=%d",
             enginePos[0], enginePos[1], enginePos[2], g_sys.has_curVirtualCam ? 1 : 0,
             g_sys.has_curActiveController ? 1 : 0, g_sys.has_controllerCameraTrans ? 1 : 0,
             g_sys.has_vcamFollow ? 1 : 0, g_sys.has_vcamLookAt ? 1 : 0,
             g_sys.has_trPosition ? 1 : 0, g_sys.has_snapGetOffset ? 1 : 0);
    LogG(b);
    for (int s = 1; s < kAnchorSourceCount; ++s) {
        float p[3] = {0.f, 0.f, 0.f};
        const bool ok = GameSysAnchorPosition(s, enginePos, p);
        snprintf(b, sizeof(b), "[anchor] %-16s %s%s", GameSysAnchorSourceName(s),
                 ok ? "=" : "不可用", ok ? "" : "");
        std::string line = b;
        if (ok) {
            snprintf(b, sizeof(b), " (%.3f, %.3f, %.3f)  相对引擎相机 (%.2f, %.2f, %.2f)",
                     p[0], p[1], p[2], p[0] - enginePos[0], p[1] - enginePos[1],
                     p[2] - enginePos[2]);
            line += b;
        }
        LogG(line);
    }
}

// v1.1: 读 SnapshotCameraController.GetCameraOffset() -> Vector3(装箱, 需 unbox)。
// 只读诊断用; 任何一步不满足就返回 false, 不抛异常、不写内存。
bool GameSysSnapshotCameraOffset(float out[3]) {
    if (!g_sys.has_snapGetOffset || !g_sys.snapshotController) return false;
    const Il2CppApi& a = Il2Cpp();
    if (!a.loaded || !a.object_unbox) return false;
    void* boxed = InvokeLogged(g_sys.mi_snapGetOffset, g_sys.snapshotController, nullptr,
                               "GetCameraOffset");
    if (!boxed) return false;
    void* raw = a.object_unbox(boxed);
    if (!raw) return false;
    const float* f = static_cast<const float*>(raw);
    out[0] = f[0];
    out[1] = f[1];
    out[2] = f[2];
    return true;
}

bool GameSysSnapshotSetAperture(float fStop) {
    if (!g_sys.has_snapSetAperture) return false;
    if (!g_sys.snapshotController) {
        LogG("[gamesys] 光圈失败: 快照相机实例未就绪");
        return false;
    }
    float v = fStop;
    void* p[1] = {&v};
    InvokeLogged(g_sys.mi_snapSetAperture, g_sys.snapshotController, p, "SetAperture");
    char b[128];
    snprintf(b, sizeof(b), "[gamesys] SnapshotCameraController.SetAperture(%.2f)", fStop);
    LogG(b);
    return true;
}

bool GameSysSnapshotSetFocusDistance(float d) {
    if (!g_sys.has_snapSetFocus) return false;
    if (!g_sys.snapshotController) {
        LogG("[gamesys] 对焦失败: 快照相机实例未就绪");
        return false;
    }
    float v = d;
    void* p[1] = {&v};
    InvokeLogged(g_sys.mi_snapSetFocus, g_sys.snapshotController, p, "SetFocusDistance");
    char b[128];
    snprintf(b, sizeof(b), "[gamesys] SnapshotCameraController.SetFocusDistance(%.3f)", d);
    LogG(b);
    return true;
}

bool GameSysSnapshotSetAdditiveFov(float delta) {
    if (!g_sys.has_snapAdditiveFov) return false;
    if (!g_sys.snapshotController) return false;
    float v = delta;
    void* p[1] = {&v};
    InvokeLogged(g_sys.mi_snapAdditiveFov, g_sys.snapshotController, p, "SetAdditiveFOV");
    LogG("[gamesys] SnapshotCameraController.SetAdditiveFOV 已调用");
    return true;
}

bool GameSysSnapshotSetZoomScale(float scale) {
    if (!g_sys.has_snapSetZoom) return false;
    if (!g_sys.snapshotController) return false;
    float v = scale;
    void* p[1] = {&v};
    InvokeLogged(g_sys.mi_snapSetZoom, g_sys.snapshotController, p, "SetZoomScale");
    char b[128];
    snprintf(b, sizeof(b), "[gamesys] SnapshotCameraController.SetZoomScale(%.3f)", scale);
    LogG(b);
    return true;
}

float GameSysSnapshotGetZoomScale() {
    if (!g_sys.has_snapGetZoom || !g_sys.snapshotController) return -1.f;
    void* boxed = InvokeLogged(g_sys.mi_snapGetZoom, g_sys.snapshotController, nullptr,
                               "GetZoomScale");
    const Il2CppApi& a = Il2Cpp();
    if (!boxed || !a.object_unbox) return -1.f;
    void* raw = a.object_unbox(boxed);
    if (!raw) return -1.f;
    return *static_cast<float*>(raw);
}

bool GameSysSnapshotActivate() {
    if (!g_sys.has_snapActivate) return false;
    if (!g_sys.snapshotController) return false;
    InvokeLogged(g_sys.mi_snapActivate, g_sys.snapshotController, nullptr,
                 "ActivateSnapshotCamera");
    LogG("[gamesys] SnapshotCameraController.ActivateSnapshotCamera()");
    return true;
}

bool GameSysSnapshotDeactivate() {
    if (!g_sys.has_snapDeactivate) return false;
    if (!g_sys.snapshotController) return false;
    InvokeLogged(g_sys.mi_snapDeactivate, g_sys.snapshotController, nullptr,
                 "DeactivateSnapshotCamera");
    LogG("[gamesys] SnapshotCameraController.DeactivateSnapshotCamera()");
    return true;
}

// ---- v1.0.0as: 景深"落地"——把快照相机的景深设置施加到主相机(Apply) / 恢复默认(Reset) ----
// 为什么关键(用户 2026-09-15 实测推导): 武装(cmd 27)时会顺手调一次 **ApplySnapshotDofSettings()**,
//   所以"武装后自由镜头**有**虚化"; 而进一次官方相机再退出后那份施加被覆盖, 我们的 SetAperture
//   只改"快照控制器里的数值"、**没人再 Apply**, 于是表现成"光圈只在进官方相机后才生效"。
bool GameSysSnapshotApplyDof() {
    if (!g_sys.has_snapApplyDof) {
        LogG("[gamesys] ApplySnapshotDofSettings 未解析 -> 无法施加景深");
        return false;
    }
    if (!g_sys.snapshotController) {
        LogG("[gamesys] 施加景深失败: 快照相机实例未就绪(先武装一次: tools\\arm_lens.ps1 或 -Id 27)");
        return false;
    }
    InvokeLogged(g_sys.mi_snapApplyDof, g_sys.snapshotController, nullptr,
                 "ApplySnapshotDofSettings");
    LogG("[gamesys] SnapshotCameraController.ApplySnapshotDofSettings() 已调用");
    return true;
}

bool GameSysSnapshotResetDof() {
    if (!g_sys.has_snapResetDof) {
        LogG("[gamesys] ResetSnapshotDofSettings 未解析 -> 无法恢复默认景深");
        return false;
    }
    if (!g_sys.snapshotController) {
        LogG("[gamesys] 恢复默认景深失败: 快照相机实例未就绪");
        return false;
    }
    InvokeLogged(g_sys.mi_snapResetDof, g_sys.snapshotController, nullptr,
                 "ResetSnapshotDofSettings");
    LogG("[gamesys] SnapshotCameraController.ResetSnapshotDofSettings() 已调用");
    return true;
}

// 指令 77: 一次性验证"光圈能不能在自由相机里生效"= SetAperture(+Focus) 之后**主动 Apply**
//   fStop>0 -> SetAperture; focus>0 -> SetFocusDistance; doApply -> ApplySnapshotDofSettings()
//   doReset -> 先 ResetSnapshotDofSettings()(对照: 恢复游戏默认)
void GameSysDofApplyTest(float fStop, float focus, int doApply, int doReset) {
    GameSysResolve();
    LogG("[dof] === 指令 77: 光圈/对焦 + Apply(落地) 测试 ===");
    if (!g_sys.snapshotController) {
        LogG("[dof]   快照相机实例未就绪 -> 先武装一次(tools\\arm_lens.ps1)再来");
        return;
    }
    if (doReset) GameSysSnapshotResetDof();
    if (fStop > 0.f) {
        GameSysSnapshotSetAperture(fStop);
        char b[160];
        snprintf(b, sizeof(b), "[dof]   SetAperture(%.2f)", fStop);
        LogG(b);
    }
    if (focus > 0.f) {
        GameSysSnapshotSetFocusDistance(focus);
        char b[160];
        snprintf(b, sizeof(b), "[dof]   SetFocusDistance(%.2f)", focus);
        LogG(b);
    }
    if (doApply) {
        GameSysSnapshotApplyDof();
        LogG("[dof]   **已 Apply** —— 请看画面: 虚化出现了吗? (若出现, 就说明光圈终于落到自由相机上了)");
    } else {
        LogG("[dof]   只设置了数值, 未 Apply(对照组: 预期画面无变化)");
    }
}

bool GameSysSnapshotUpdateSensorSize() {
    if (!g_sys.has_snapUpdateSensor || !g_sys.snapshotController) return false;
    InvokeLogged(g_sys.mi_snapUpdateSensor, g_sys.snapshotController, nullptr,
                 "UpdateSensorSize");
    LogG("[gamesys] SnapshotCameraController.UpdateSensorSize()");
    return true;
}

bool GameSysEnableDof(float v) {
    if (!g_sys.has_enableDof) return false;
    float val = v;
    void* p[1] = {&val};
    InvokeLogged(g_sys.mi_enableDof, nullptr, p, "CameraUtils.EnableDOF");
    char b[128];
    snprintf(b, sizeof(b), "[gamesys] CameraUtils.EnableDOF(%.3f) 已调用", v);
    LogG(b);
    return true;
}

// 签名自省结果: ToggleSnapshotCamera(System.Boolean, System.Boolean) 的返回值
// **就是** SnapshotCameraController —— 这是拿快照(拍照)相机实例的正路。
// 拿到实例后光圈/对焦距离/变焦全部可用(SetAperture/SetFocusDistance/SetZoomScale 均为 Single)。
bool GameSysToggleSnapshotCamera(float a, float b) {
    if (!g_sys.has_toggleSnapshot) return false;
    void* mgr = CameraManagerInstance();
    if (!mgr) return false;
    uint8_t pa = a > 0.5f ? 1 : 0;
    uint8_t pb = b > 0.5f ? 1 : 0;
    void* p[2] = {&pa, &pb};
    void* ret = InvokeLogged(g_sys.mi_toggleSnapshot, mgr, p, "ToggleSnapshotCamera");
    char buf[192];
    snprintf(buf, sizeof(buf), "[gamesys] ToggleSnapshotCamera(%d, %d) 已调用 ret=%p",
             pa, pb, ret);
    LogG(buf);
    if (ret) {
        g_sys.snapshotController = ret;
        LogG("[gamesys] *** 快照相机实例: 来自 ToggleSnapshotCamera 返回值");
        // 拿到实例后立刻探一次: 激活 + 应用 DOF 设置 + 读回变焦
        GameSysSnapshotActivate();
        if (g_sys.has_snapApplyDof) {
            InvokeLogged(g_sys.mi_snapApplyDof, ret, nullptr, "ApplySnapshotDofSettings");
            LogG("[gamesys] ApplySnapshotDofSettings() 已调用");
        }
        GameSysSnapshotProbe();
    }
    return true;
}

void GameSysSnapshotProbe() {
    // 只读探测: 不在这里尝试获取实例(获取只能由显式指令触发)
    if (!g_sys.snapshotController) {
        LogG("[probe] 快照相机实例未就绪 (运行 tools\\arm_lens.ps1 或发送指令 27)");
        return;
    }
    const float z = GameSysSnapshotGetZoomScale();
    char b[160];
    snprintf(b, sizeof(b), "[probe] 快照相机实例=%p zoomScale=%.4f",
             g_sys.snapshotController, z);
    LogG(b);
    GameSysSnapshotUpdateSensorSize();
}

void GameSysRefreshAndReport() {
    void* mgr = CameraManagerInstance();
    if (mgr && g_sys.has_getMainMarketing) {
        void* mc = InvokeLogged(g_sys.mi_getMainMarketing, mgr, nullptr,
                                "GetMainMarketingCameraController");
        if (mc) {
            g_sys.marketingController = mc;
            LogG("[gamesys] MarketingCameraController 实例: OK");
        } else {
            LogG("[gamesys] MarketingCameraController 实例: 未获取(可能未创建)");
        }
    }
    // 注意: 这里刻意不做快照相机实例解析 —— 自举探测阶段必须保持"只读",
    // 任何改动相机管理器状态的动作都留给用户显式指令(14/27/31)。
    GameSysReport();
}

// ---------------- 方法签名自省(只读元数据, 安全) ----------------
// 目的: 不靠猜参数语义。打印每个关键托管方法的参数类型与返回类型。
void GameSysReportSignatures() {
    const Il2CppApi& a = Il2Cpp();
    if (!a.method_get_param || !a.method_get_return_type || !a.class_from_type) {
        LogG("[sig] 签名自省 API 不可用(method_get_param/return_type/class_from_type)");
        return;
    }
    struct Item {
        const char* label;
        void* mi;
    };
    const Item items[] = {
        {"CameraUtils.get_cameraManager", g_sys.mi_getCameraManager},
        {"CameraUtils.OpenMarketingCamera", g_sys.mi_openMarketing},
        {"CameraUtils.CloseMarketingCamera", g_sys.mi_closeMarketing},
        {"CameraUtils.EnableDOF", g_sys.mi_enableDof},
        {"CameraUtils.GetDOFData", g_sys.mi_getDofData},
        {"CameraUtils.RecenterCamera", g_sys.mi_recenter},
        {"CameraUtils.ResetCameraPosition", g_sys.mi_resetCamPos},
        {"CameraManager.GetMainMarketingCameraController", g_sys.mi_getMainMarketing},
        {"CameraManager.GetMainLevelCameraController", g_sys.mi_getMainLevel},
        {"CameraManager.SetOverrideFOVForCurrCamera", g_sys.mi_setOverrideFov},
        {"CameraManager.ToggleSnapshotCamera", g_sys.mi_toggleSnapshot},
        {"CameraManager.CreateOrGetTemporaryController", g_sys.mi_createTemp},
        {"MarketingCameraController.ChangeFov", g_sys.mi_marketingChangeFov},
        {"MarketingCameraController.SetMoveInput", g_sys.mi_marketingMoveInput},
        {"MarketingCameraController.SetRotateInput", g_sys.mi_marketingRotateInput},
        {"MarketingCameraController.Zoom", g_sys.mi_marketingZoom},
        {"SnapshotCameraController.SetAperture", g_sys.mi_snapSetAperture},
        {"SnapshotCameraController.SetFocusDistance", g_sys.mi_snapSetFocus},
        {"SnapshotCameraController.SetAdditiveFOV", g_sys.mi_snapAdditiveFov},
        {"SnapshotCameraController.SetZoomScale", g_sys.mi_snapSetZoom},
        {"SnapshotCameraController.GetZoomScale", g_sys.mi_snapGetZoom},
        {"SnapshotCameraController.UpdateSensorSize", g_sys.mi_snapUpdateSensor},
        {"SnapshotCameraController.ActivateSnapshotCamera", g_sys.mi_snapActivate},
        {"SnapshotCameraController.DeactivateSnapshotCamera", g_sys.mi_snapDeactivate},
        {"SnapshotCameraController.ApplySnapshotDofSettings", g_sys.mi_snapApplyDof},
    };
    for (const Item& it : items) {
        if (!it.mi) {
            LogG(std::string("[sig] (未解析) ") + it.label);
            continue;
        }
        const uint32_t pc =
            a.method_get_param_count ? a.method_get_param_count(it.mi) : 0;
        std::string s = std::string("[sig] ") + it.label + "(";
        for (uint32_t i = 0; i < pc; ++i) {
            if (i) s += ", ";
            s += TypeName(a.method_get_param(it.mi, i));
        }
        s += ") -> ";
        s += TypeName(a.method_get_return_type(it.mi));
        LogG(s);
    }
}

// ---- v1.0.0as: 指令 75 —— 景深(DOF)链路体检(**只读**) ----
// 背景(用户 2026-09-15): 光圈只在**进入官方相机(拍照/营销)**后才生效, 在我们的正常自由相机里完全无效。
// 已查明(指令 30 的 live 签名):
//   · 我们现在只把光圈送给 `SnapshotCameraController.SetAperture` —— 那个实例**只在官方相机态存在**
//     (日志实证: `[lens] 光圈/对焦本次未应用: 快照相机实例未就绪`);
//   · 游戏自己的"主相机景深"通道有**两个** EnableDOF 重载:
//       CameraUtils.EnableDOF(Beyond.Gameplay.CameraDOFDescriptor) -> void
//       CameraUtils.EnableDOF(HG.Rendering.Runtime.HGDepthOfFieldData) -> void   ← 直接吃数据本体
//     配对: `GetDOFData() -> HGDepthOfFieldData`(实测是**类**, 不是值类型) 与
//     `DisableDOF(float, AnimationCurve)` / `CameraManager._TryGenDOFTween(float, AnimationCurve)`;
//   · URP / PPSv2 的 `DepthOfField` 枚举都找不到 → 景深是本作自家的 `HG.Rendering`。
//
// ⚠ **事故记录(2026-09-15 11:24, 我的错)**: 第一版体检在字段循环里用了
//   `field_get_type` / `class_value_size` / `field_get_value`(想顺便把字段**当前值**也打出来),
//   指令发出去的**那一刻游戏就崩了**(崩溃转储 `Crash_2026-09-15_032417127`, 11:24:17;
//   模块命令线程同时卡死, 连只读的 67 都不再响应)。
//   教训: 对"外部程序集 + 加固"的类, **凡是"按类型解析 + 按声明尺寸读字段值"的动作都不可信** ——
//   本项目真正验证过安全的字段用法**只有一种**: `class_get_fields` + `field_get_name`(只列名字,
//   见 DumpUiMembers); 类型/尺寸/取值/写值一律视为危险, 必须单独、可中止地验证。
//   因此本命令默认路径**只用已验证的 API**(方法枚举 + 类链 + 方法签名), 字段名列表放在**最后**执行,
//   并且要显式 `a0=1` 才会跑。
//
// 参数: a0 = 0(默认) 只跑安全路径; a0 = 1 额外列出字段**名字**(仍不读值, 且放在最后)
namespace {
// 只列字段名字 —— 与 DumpUiMembers 里同一套已被验证安全的调用(不做类型解析、不读值)
void DumpDofFieldNames(void* klass, const char* label, int cap) {
    const Il2CppApi& a = Il2Cpp();
    if (!klass || !a.class_get_fields || !a.field_get_name) {
        LogG(std::string("[dof]   ") + label + ": 字段枚举 API 不可用 -> 跳过");
        return;
    }
    void* it = nullptr;
    void* f = nullptr;
    int c = 0, n = 0;
    while ((f = a.class_get_fields(klass, &it)) != nullptr && c < cap) {
        ++c;
        const char* fn = a.field_get_name(f);
        if (!fn) continue;
        ++n;
        LogG(std::string("[dof]   F ") + label + "." + fn);
    }
    if (n == 0) LogG(std::string("[dof]   ") + label + " 没有字段(或被加固剥离)");
}

// 打印某个类上名字含 kw 的全部方法(含参数类型/返回类型; kw 为空 = 全部)。
// 这套(TypeName/method_get_param)已被指令 30 在生产里验证过。
void DumpDofMethods(void* klass, const char* label, const char* kw) {
    const Il2CppApi& a = Il2Cpp();
    if (!klass || !a.class_get_methods || !a.method_get_name) return;
    void* it = nullptr;
    void* m = nullptr;
    int c = 0, hits = 0;
    while ((m = a.class_get_methods(klass, &it)) != nullptr && c < 6000) {
        ++c;
        const char* mn = a.method_get_name(m);
        if (!mn) continue;
        if (kw && *kw && !strstr(mn, kw)) continue;
        ++hits;
        std::string s = std::string("[dof]   ") + label + "." + mn + "(";
        const uint32_t pc = a.method_get_param_count ? a.method_get_param_count(m) : 0;
        for (uint32_t i = 0; i < pc; ++i) {
            if (i) s += ", ";
            s += TypeName(a.method_get_param(m, i));
        }
        s += ") -> ";
        s += TypeName(a.method_get_return_type(m));
        LogG(s);
    }
    if (hits == 0) {
        LogG(std::string("[dof]   ") + label + " 上没有匹配 '" + (kw ? kw : "") + "' 的方法");
    }
}
}  // namespace

void GameSysDofAudit(int includeFieldNames) {
    const Il2CppApi& a = Il2Cpp();
    LogG("[dof] ===== 景深(DOF)链路体检(只读; 只用已验证 API) =====");
    {
        char b[256];
        snprintf(b, sizeof(b),
                 "[dof] 参数: 列字段名=%d | 能力 enableDof=%d getDofData=%d | 字段枚举API=%d",
                 includeFieldNames ? 1 : 0, g_sys.has_enableDof ? 1 : 0,
                 g_sys.has_getDofData ? 1 : 0,
                 (a.class_get_fields && a.field_get_name) ? 1 : 0);
        LogG(b);
    }
    void* cu = ResolveClass("Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraUtils");
    void* cmClass = ResolveClass("Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraManager");
    LogG("[dof] ① DOF 相关的全部重载签名:");
    DumpDofMethods(cu, "CameraUtils", "DOF");
    DumpDofMethods(cmClass, "CameraManager", "DOF");

    LogG("[dof] ② CameraUtils.GetDOFData() 的返回对象(景深数据本体):");
    void* data = nullptr;
    if (g_sys.has_getDofData && g_sys.mi_getDofData) {
        data = InvokeLogged(g_sys.mi_getDofData, nullptr, nullptr, "CameraUtils.GetDOFData");
    }
    void* dk = nullptr;
    if (!data) {
        LogG("[dof]   GetDOFData() 返回 null 或未解析 -> 后续跳过");
    } else {
        LogG("[dof]   类链: " + GameSysClassChainOf(data));
        dk = a.object_get_class ? a.object_get_class(data) : nullptr;
        if (dk) {
            const int isVt = a.class_is_valuetype ? (a.class_is_valuetype(dk) ? 1 : 0) : -1;
            char b[192];
            snprintf(b, sizeof(b), "[dof]   是否值类型=%d (0=类 1=值类型)", isVt);
            LogG(b);
            LogG("[dof]   它自己带的方法(找 setter/属性):");
            DumpDofMethods(dk, "HGDepthOfFieldData", "");
        }
    }

    LogG("[dof] ③ CameraDOFDescriptor(EnableDOF 的参数类型):");
    void* dd = ResolveClass("Gameplay.Beyond.dll", "Beyond.Gameplay", "CameraDOFDescriptor");
    if (!dd) {
        LogG("[dof]   类未找到");
    } else {
        const int isVt = a.class_is_valuetype ? (a.class_is_valuetype(dd) ? 1 : 0) : -1;
        char b[192];
        snprintf(b, sizeof(b), "[dof]   是否值类型=%d", isVt);
        LogG(b);
        DumpDofMethods(dd, "CameraDOFDescriptor", "");
    }

    // ---- 危险区(需显式开启): 只列字段名字, 不解析类型、不读值 ----
    if (includeFieldNames) {
        LogG("[dof] ④ 字段名列表(仅名字; 这是项目里唯一验证过安全的字段用法):");
        if (dk) DumpDofFieldNames(dk, "HGDepthOfFieldData", 200);
        if (dd) DumpDofFieldNames(dd, "CameraDOFDescriptor", 100);
        if (cmClass) DumpDofFieldNames(cmClass, "CameraManager", 400);
    } else {
        LogG("[dof] ④ (字段名列表未开启: 加 -A0 1 才跑 —— 第一版在这里崩过, 现在单独隔离)");
    }
    LogG("[dof] ===== 体检结束 =====");
}

// ---- v1.0.0as: 指令 76 —— 主相机景深"通道验证"(一次性, 到期自动还原) ----
// 用户 2026-09-15 决定要验证的第一件事: **在我们的自由相机里到底能不能开景深**。
// 做法只允许两类操作(与 gamesys.cpp 文件头纪律一致): ① 已解析方法 + runtime_invoke
//   ② 只读元数据枚举(类名/方法名/参数类型)。**绝不读写字段**。
// 步骤: GetDOFData() 取游戏原数据(只为拿它的**类**) → `object_new` 造一个实例 →
//       在它的方法里找**无参构造**并调用 → 在 CameraUtils 上找**参数类型为 HGDepthOfFieldData 的
//       EnableDOF 重载**并调用它(注意: 我们已解析的 mi_enableDof 是"哪个重载"并不确定, 所以要按类型挑)。
// 还原: 重新 GetDOFData() 再 EnableDOF 一次(把游戏自己的数据交回去) —— **不跨帧缓存托管指针**。
// 参数: a0=1 开测(a1=保持秒数, 默认 20) / a0=0 还原 / a0=2 强制 DisableDOF(0, null) 兜底
namespace {
// 景深通道验证的挂起状态 —— **只存时间戳, 不缓存任何托管指针**(项目纪律: 托管指针不跨帧持有)
struct DofTestState {
    bool pending = false;
    uint64_t until = 0;
};
DofTestState g_dof;

// 在 klass 上找参数个数为 pc 的 ".ctor"(只读元数据)
void* FindCtor(void* klass, int pc) {
    const Il2CppApi& a = Il2Cpp();
    if (!klass || !a.class_get_methods || !a.method_get_name) return nullptr;
    void* it = nullptr;
    void* m = nullptr;
    int c = 0;
    while ((m = a.class_get_methods(klass, &it)) != nullptr && c < 6000) {
        ++c;
        const char* mn = a.method_get_name(m);
        if (!mn || strcmp(mn, ".ctor") != 0) continue;
        const int got = a.method_get_param_count
                            ? static_cast<int>(a.method_get_param_count(m))
                            : -1;
        if (got == pc) return m;
    }
    return nullptr;
}

// 在 klass 上找"名字 == name 且第 0 个参数类型匹配 paramTypeName"的重载。
// v1.0.0as 修正: `class_get_name` 只返回**短名**(如 `Single`), 而命名空间要另外问
//   `class_get_namespace` —— 原来直接拿短名去比 "System.Single" 永远不中(于是"兜底关景深"从未执行)。
//   现在同时接受"命名空间.短名"与"短名"两种写法。
void* FindOverloadByParamType(void* klass, const char* name, const char* paramTypeName) {
    const Il2CppApi& a = Il2Cpp();
    if (!klass || !a.class_get_methods || !a.method_get_name || !a.method_get_param) return nullptr;
    void* it = nullptr;
    void* m = nullptr;
    int c = 0;
    while ((m = a.class_get_methods(klass, &it)) != nullptr && c < 6000) {
        ++c;
        const char* mn = a.method_get_name(m);
        if (!mn || strcmp(mn, name) != 0) continue;
        const uint32_t pc = a.method_get_param_count ? a.method_get_param_count(m) : 0;
        if (pc != 1) continue;
        void* pt = a.method_get_param(m, 0);
        if (!pt || !a.class_from_type || !a.class_get_name) continue;
        void* pk = a.class_from_type(pt);
        const char* cn = pk ? a.class_get_name(pk) : nullptr;
        if (!cn) continue;
        const char* nsp = (pk && a.class_get_namespace) ? a.class_get_namespace(pk) : nullptr;
        std::string full;
        if (nsp && *nsp) {
            full = std::string(nsp) + "." + cn;
        } else {
            full = cn;
        }
        // v1.0.0as 再修: **基元类型的 class_get_namespace 是空的**(如 System.Single),
        // 于是"命名空间.短名"拼不出来 —— 因此还要拿"请求名里的最后一段"跟短名比一次
        // (否则 `DisableDOF(System.Single, ...)` 永远挑不中, "兜底关景深"就一直失败)。
        const char* wantShort = strrchr(paramTypeName, '.');
        wantShort = wantShort ? (wantShort + 1) : paramTypeName;
        if (full == paramTypeName || strcmp(cn, paramTypeName) == 0 ||
            strcmp(cn, wantShort) == 0)
            return m;
    }
    return nullptr;
}

// 名字 == name 且**参数个数 == pc** 的第 1 个参数类型名(诊断用; 找不到返回空串)
std::string FirstParamTypeNameOfOverload(void* klass, const char* name, int pc) {
    const Il2CppApi& a = Il2Cpp();
    if (!klass || !a.class_get_methods || !a.method_get_name || !a.method_get_param) return "";
    void* it = nullptr;
    void* m = nullptr;
    int c = 0;
    while ((m = a.class_get_methods(klass, &it)) != nullptr && c < 6000) {
        ++c;
        const char* mn = a.method_get_name(m);
        if (!mn || strcmp(mn, name) != 0) continue;
        const int got = a.method_get_param_count ? static_cast<int>(a.method_get_param_count(m)) : -1;
        if (got != pc) continue;
        if (pc < 1) return "(无参数)";
        if (!a.class_from_type || !a.class_get_name) return "?";
        void* pk = a.class_from_type(a.method_get_param(m, 0));
        const char* cn = pk ? a.class_get_name(pk) : nullptr;
        const char* nsp = (pk && a.class_get_namespace) ? a.class_get_namespace(pk) : nullptr;
        if (!cn) return "?";
        std::string full = (nsp && *nsp) ? (std::string(nsp) + "." + cn) : std::string(cn);
        return full;
    }
    return "";
}

// 在 klass 上找"名字 == name 且参数个数 == pc"的第一个重载(只读元数据)
void* FindOverloadByCount(void* klass, const char* name, int pc) {
    const Il2CppApi& a = Il2Cpp();
    if (!klass || !a.class_get_methods || !a.method_get_name) return nullptr;
    void* it = nullptr;
    void* m = nullptr;
    int c = 0;
    while ((m = a.class_get_methods(klass, &it)) != nullptr && c < 6000) {
        ++c;
        const char* mn = a.method_get_name(m);
        if (!mn || strcmp(mn, name) != 0) continue;
        const int got = a.method_get_param_count ? static_cast<int>(a.method_get_param_count(m)) : -1;
        if (got == pc) return m;
    }
    return nullptr;
}

void* CameraUtilsClass() {
    return ResolveClass("Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraUtils");
}

// 用"游戏原数据"还原(重新取一次, 不缓存)
bool DofRestoreFromGame(const char* why) {
    if (!g_sys.has_getDofData || !g_sys.mi_getDofData) return false;
    void* orig = InvokeLogged(g_sys.mi_getDofData, nullptr, nullptr, "GetDOFData(还原)");
    if (!orig) {
        LogG("[dof] 还原失败: GetDOFData() 返回 null");
        return false;
    }
    void* mi = FindOverloadByParamType(CameraUtilsClass(), "EnableDOF", "HGDepthOfFieldData");
    if (!mi) {
        LogG("[dof] 还原失败: 找不到 EnableDOF(HGDepthOfFieldData) 重载");
        return false;
    }
    void* p[1] = {orig};
    InvokeLogged(mi, nullptr, p, "CameraUtils.EnableDOF(游戏原数据)");
    LogG(std::string("[dof] 已还原(") + why + "): 把游戏自己的景深数据交回去了");
    return true;
}
}  // namespace

void GameSysDofChanTest(int mode, float p1, float p2, float p3) {
    const Il2CppApi& a = Il2Cpp();
    GameSysResolve();
    if (mode == 0) {
        if (g_dof.pending) {
            g_dof.pending = false;
            DofRestoreFromGame("手动还原");
        } else {
            LogG("[dof] 还原请求: 当前没有挂起的景深测试(仍执行一次交还, 保证干净)");
            DofRestoreFromGame("手动还原");
        }
        return;
    }
    if (mode == 2) {  // 兜底: 强制关掉
        void* cu = CameraUtilsClass();
        // 只读诊断: 先把 DisableDOF 的全部重载签名打出来(方法名 + 参数类型; 不碰字段)
        LogG("[dof] DisableDOF 重载清单(只读):");
        if (cu && a.class_get_methods && a.method_get_name) {
            void* it = nullptr;
            void* m = nullptr;
            int c = 0, n = 0;
            while ((m = a.class_get_methods(cu, &it)) != nullptr && c < 6000) {
                ++c;
                const char* mn = a.method_get_name(m);
                if (!mn || strcmp(mn, "DisableDOF") != 0) continue;
                ++n;
                std::string s = "[dof]   CameraUtils.DisableDOF(";
                const uint32_t pc2 = a.method_get_param_count ? a.method_get_param_count(m) : 0;
                for (uint32_t i = 0; i < pc2; ++i) {
                    if (i) s += ", ";
                    s += TypeName(a.method_get_param(m, i));
                }
                s += ") -> ";
                s += TypeName(a.method_get_return_type(m));
                LogG(s);
            }
            if (n == 0) LogG("[dof]   (没有找到 DisableDOF)");
        }
        void* mi = FindOverloadByParamType(cu, "DisableDOF", "System.Single");
        if (!mi) {
            LogG("[dof] 兜底关闭失败: 没挑到 DisableDOF(System.Single, AnimationCurve) 重载"
                 " —— 上面清单里若有该签名, 说明类型名比对还有问题, 请把它贴给我");
            return;
        }
        float zero = 0.f;
        void* p[2] = {&zero, nullptr};
        InvokeLogged(mi, nullptr, p, "CameraUtils.DisableDOF(0, null)");
        g_dof.pending = false;
        LogG("[dof] 已调用 DisableDOF(0, null) 兜底关闭(若它抛托管异常, 上面会有一行提示)");
        return;
    }
    if (mode == 5 || mode == 6) {
        // 用户 2026-09-15 的关键实测: "武装后自由镜头**有**虚化, 但进一次官方相机再退出后,
        //   自由相机的虚化就**失效**了" ⇒ 景深状态被那次切换清掉, 退出时没还原。
        // 这两档走 **CameraManager 实例**那条路(与静态的 CameraUtils.EnableDOF 可能是两套逻辑):
        //   mode 5 = CameraManager._ClearDOFTween()  —— 清掉挂着的景深过渡状态
        //   mode 6 = CameraManager.EnableDOF(<GetDOFData() 拿到的数据>)  —— 用实例通道重开
        void* cmClass = ResolveClass("Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraManager");
        void* mgr = CameraManagerInstance();
        if (!cmClass || !mgr) {
            LogG("[dof] mode 5/6 不可用: 拿不到 CameraManager 类或实例");
            return;
        }
        if (mode == 5) {
            void* mi = FindOverloadByCount(cmClass, "_ClearDOFTween", 0);
            if (!mi) {
                LogG("[dof] 找不到 CameraManager._ClearDOFTween() -> 放弃");
                return;
            }
            InvokeLogged(mi, mgr, nullptr, "CameraManager._ClearDOFTween()");
            LogG("[dof] 已调用 CameraManager._ClearDOFTween()(清过渡状态) —— 看画面有变化吗?");
            return;
        }
        void* orig = InvokeLogged(g_sys.mi_getDofData, nullptr, nullptr, "GetDOFData");
        if (!orig) {
            LogG("[dof] mode 6: GetDOFData() 返回 null -> 放弃");
            return;
        }
        void* mi = FindOverloadByParamType(cmClass, "EnableDOF", "HGDepthOfFieldData");
        if (!mi) {
            LogG("[dof] mode 6: 找不到 CameraManager.EnableDOF(HGDepthOfFieldData) 重载 -> 放弃");
            return;
        }
        void* p[1] = {orig};
        InvokeLogged(mi, mgr, p, "CameraManager.EnableDOF(HGDepthOfFieldData)");
        LogG("[dof] 已调用 **CameraManager 实例** 的 EnableDOF(游戏数据) —— 看画面: 虚化回来了吗?");
        return;
    }
    if (mode != 1 && mode != 3 && mode != 4) {
        LogG("[dof] 参数无效: a0=1 无参构造 / 3 六float全设A1(a2=秒) / 4 槽位试探(a1值,a2槽位,a3其余) / "
             "5 CameraManager._ClearDOFTween() / 6 CameraManager.EnableDOF(游戏数据) / 0 还原 / 2 强制关");
        return;
    }
    if (!g_sys.has_getDofData || !a.object_new || !a.object_get_class) {
        LogG("[dof] 通道验证不可用: 缺少 GetDOFData / object_new / object_get_class");
        return;
    }
    // ① 拿到"类"(只为拿类; 对象本身不跨帧缓存)
    void* orig = InvokeLogged(g_sys.mi_getDofData, nullptr, nullptr, "GetDOFData");
    if (!orig) {
        LogG("[dof] 通道验证失败: GetDOFData() 返回 null");
        return;
    }
    void* klass = a.object_get_class(orig);
    if (!klass) {
        LogG("[dof] 通道验证失败: 取不到 HGDepthOfFieldData 的类");
        return;
    }
    LogG("[dof] === 通道验证: 在我们的自由相机里开一次景深(一次性, 到期自动还原) ===");
    LogG("[dof]   类: " + GameSysClassChainOf(orig));
    // ③ 选构造:
    //    mode 1 = 无参构造(可能全零 -> 等于没虚化, 用户实测"没变化"符合这一解释)
    //    mode 3 = 带数值构造 (HGDepthOfFieldType, float×6), 6 个 float **全设成 A1**
    //    mode 4 = 带数值构造, 只把某个槽位设成 A1、其余设成 A3(逐槽位定位光圈/对焦)
    int32_t type0 = 0;   // HGDepthOfFieldType 的第一个枚举值(具体语义靠观察, 不读字段猜)
    float fv[6] = {0, 0, 0, 0, 0, 0};
    void* obj = nullptr;
    if (mode == 1) {
        void* ctor = FindCtor(klass, 0);
        if (!ctor) {
            LogG("[dof]   找不到无参构造 -> 放弃");
            return;
        }
        obj = a.object_new(klass);
        if (!obj) {
            LogG("[dof]   object_new 失败 -> 放弃");
            return;
        }
        InvokeLogged(ctor, obj, nullptr, "HGDepthOfFieldData..ctor()");
        LogG("[dof]   已构造 HGDepthOfFieldData() 实例(无参; 若全零则等于无虚化)");
    } else {
        void* ctor = FindCtor(klass, 7);  // (HGDepthOfFieldType, float×6)
        if (!ctor) {
            LogG("[dof]   找不到 7 参数构造 (HGDepthOfFieldType, float×6) -> 放弃");
            return;
        }
        if (mode == 3) {
            for (int i = 0; i < 6; ++i) fv[i] = p1;
        } else {
            int slot = static_cast<int>(p2 + 0.5f);
            if (slot < 0) slot = 0;
            if (slot > 5) slot = 5;
            for (int i = 0; i < 6; ++i) fv[i] = p3;
            fv[slot] = p1;
            char sb[160];
            snprintf(sb, sizeof(sb), "[dof]   槽位试探: f[%d]=%.4f, 其余=%.4f", slot,
                     fv[slot], p3);
            LogG(sb);
        }
        obj = a.object_new(klass);
        if (!obj) {
            LogG("[dof]   object_new 失败 -> 放弃");
            return;
        }
        void* p7[7] = {&type0, &fv[0], &fv[1], &fv[2], &fv[3], &fv[4], &fv[5]};
        InvokeLogged(ctor, obj, p7, "HGDepthOfFieldData..ctor(Type, float×6)");
        char bb[256];
        snprintf(bb, sizeof(bb),
                 "[dof]   已构造 HGDepthOfFieldData(Type=%d, %.3f, %.3f, %.3f, %.3f, %.3f, %.3f)",
                 type0, fv[0], fv[1], fv[2], fv[3], fv[4], fv[5]);
        LogG(bb);
    }
    // ④ 挑出吃 HGDepthOfFieldData 的那个 EnableDOF 重载并调用
    void* mi = FindOverloadByParamType(CameraUtilsClass(), "EnableDOF", "HGDepthOfFieldData");
    if (!mi) {
        LogG("[dof]   找不到 EnableDOF(HGDepthOfFieldData) 重载 -> 放弃");
        return;
    }
    void* p[1] = {obj};
    InvokeLogged(mi, nullptr, p, "CameraUtils.EnableDOF(HGDepthOfFieldData)");
    // 保持秒数: mode1/3 用参数给(p1 / p2), mode4 固定 30 秒(参数槽位不够用)
    const float holdIn = (mode == 3) ? p2 : ((mode == 1) ? p1 : 30.f);
    const int hold = (holdIn >= 2.f) ? ((holdIn > 300.f) ? 300 : static_cast<int>(holdIn + 0.5f)) : 20;
    g_dof.pending = true;
    g_dof.until = GetTickCount64() + static_cast<uint64_t>(hold) * 1000ull;
    char b[320];
    snprintf(b, sizeof(b),
             "[dof] 已调用 CameraUtils.EnableDOF(HGDepthOfFieldData) —— **请看画面: 是否出现景深/虚化?**"
             " (%d 秒后自动还原; 全程只用方法调用, 没碰任何字段)",
             hold);
    LogG(b);
}

void GameSysDofTick() {
    if (!g_dof.pending) return;
    if (GetTickCount64() >= g_dof.until) {
        g_dof.pending = false;
        DofRestoreFromGame("到期自动还原");
    }
}

// ---------------- 元数据枚举(只读元数据, 不读对象内存) ----------------

namespace {
struct EnumTarget {
    const char* asmName;
    const char* ns;
    const char* cls;
};
const EnumTarget kEnumTargets[] = {
    {"UnityEngine.CoreModule.dll", "UnityEngine", "Camera"},
    {"Gameplay.Beyond.dll", "Beyond.Gameplay.View", "SnapshotCameraController"},
    {"Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraManager"},
    {"Gameplay.Beyond.dll", "Beyond.Gameplay.View", "MarketingCameraController"},
    {"Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraUtils"},
    {"Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraMono"},
    {"Gameplay.Beyond.dll", "Beyond.Gameplay", "SnapshotSystem"},
    {"Gameplay.Beyond.dll", "Beyond.Gameplay", "CameraDOFDescriptor"},
    {"UnityEngine.Rendering.Universal.dll", "UnityEngine.Rendering.Universal",
     "DepthOfField"},
    {"UnityEngine.Rendering.PostProcessing.dll",
     "UnityEngine.Rendering.PostProcessing", "DepthOfField"},
};
const char* kEnumKeywords[] = {"",       "aperture", "focal", "focus", "physic",
                               "lens",   "sensor",   "clip",  "depth", "dof",
                               "blur",   "zoom",     "camera", "fov",  "snapshot"};
constexpr int kEnumTargetCount =
    static_cast<int>(sizeof(kEnumTargets) / sizeof(kEnumTargets[0]));
constexpr int kEnumKeywordCount =
    static_cast<int>(sizeof(kEnumKeywords) / sizeof(kEnumKeywords[0]));
}  // namespace

void GameSysEnumListTargets() {
    LogG("[enum] 可选目标索引:");
    for (int i = 0; i < kEnumTargetCount; ++i) {
        char b[256];
        snprintf(b, sizeof(b), "  [%d] %s :: %s.%s", i, kEnumTargets[i].asmName,
                 kEnumTargets[i].ns, kEnumTargets[i].cls);
        LogG(b);
    }
    LogG("[enum] 可选关键字索引:");
    for (int i = 0; i < kEnumKeywordCount; ++i) {
        char b[128];
        snprintf(b, sizeof(b), "  [%d] '%s'", i, kEnumKeywords[i]);
        LogG(b);
    }
}

void GameSysEnumerateTarget(int targetIndex, int keywordIndex) {
    char b[160];
    if (targetIndex < 0 || targetIndex >= kEnumTargetCount) {
        snprintf(b, sizeof(b), "[enum] 目标索引越界: %d (0..%d)", targetIndex,
                 kEnumTargetCount - 1);
        LogG(b);
        return;
    }
    if (keywordIndex < 0 || keywordIndex >= kEnumKeywordCount) keywordIndex = 0;
    const EnumTarget& t = kEnumTargets[targetIndex];
    const char* kw = kEnumKeywords[keywordIndex];

    const Il2CppApi& a = Il2Cpp();
    void* klass = ResolveClass(t.asmName, t.ns, t.cls);
    if (!klass) {
        snprintf(b, sizeof(b), "[enum] 类未找到: %s :: %s.%s", t.asmName, t.ns, t.cls);
        LogG(b);
        return;
    }
    snprintf(b, sizeof(b), "[enum] %s.%s 关键字='%s'", t.ns, t.cls, kw);
    LogG(b);
    int nm = 0, nf = 0;
    if (a.class_get_methods && a.method_get_name) {
        void* it = nullptr;
        void* m = nullptr;
        int c = 0;
        while ((m = a.class_get_methods(klass, &it)) != nullptr && c < 4000) {
            ++c;
            const char* mn = a.method_get_name(m);
            if (!mn) continue;
            if (*kw && !strstr(mn, kw)) continue;
            ++nm;
            const int pc = a.method_get_param_count
                               ? static_cast<int>(a.method_get_param_count(m))
                               : -1;
            char l[256];
            snprintf(l, sizeof(l), "  M %s(%d)", mn, pc);
            LogG(l);
        }
    }
    if (a.class_get_fields && a.field_get_name) {
        void* it = nullptr;
        void* f = nullptr;
        int c = 0;
        while ((f = a.class_get_fields(klass, &it)) != nullptr && c < 4000) {
            ++c;
            const char* fn = a.field_get_name(f);
            if (!fn) continue;
            if (*kw && !strstr(fn, kw)) continue;
            ++nf;
            char l[224];
            snprintf(l, sizeof(l), "  F %s", fn);
            LogG(l);
        }
    }
    snprintf(b, sizeof(b), "[enum] %s.%s 命中: 方法 %d / 字段 %d", t.ns, t.cls, nm, nf);
    LogG(b);
}

// ============================================================================
// v1.3 探测轮: 类型体检(44) / SetCameraOffset 写入-测量标定(45) / 锚点采样器(46)
//
// 为什么要这套东西:
//   位置映射的"游戏侧原点"应当落在角色身上, 但目前只能靠人工肉眼对齐(指令 41),
//   而且武装时原点一旦漂移(实测新时序里的复位让原点挪了 1.84m), 旧校准量立刻失效。
//   若「角色位置 + 相机偏移」这条通道成立, 就能每次武装**自动**算出原点修正量,
//   并顺带得到"随角色移动"所需的每帧角色位置(方案 B)。
//   已知 GetCameraOffset() 恒为 (0,0,0) —— 所以不靠"读", 靠"写 + 读引擎相机世界坐标"反推。
// ============================================================================

namespace {

// 只读元数据: 方法声明的返回类型名
std::string DeclaredReturnName(void* methodInfo) {
    const Il2CppApi& a = Il2Cpp();
    if (!methodInfo || !a.method_get_return_type) return "?";
    return TypeName(a.method_get_return_type(methodInfo));
}

double Dist3(const float a[3], const float b[3]) {
    const double dx = a[0] - b[0], dy = a[1] - b[1], dz = a[2] - b[2];
    return std::sqrt(dx * dx + dy * dy + dz * dz);
}

}  // namespace

std::string GameSysClassChainOf(void* obj) {
    const Il2CppApi& a = Il2Cpp();
    if (!obj || !a.object_get_class || !a.class_get_name) return "?";
    std::string s;
    void* k = a.object_get_class(obj);
    int guard = 0;
    while (k && guard++ < 32) {
        const char* n = a.class_get_name(k);
        const char* ns = a.class_get_namespace ? a.class_get_namespace(k) : nullptr;
        if (!s.empty()) s += " < ";
        if (ns && *ns) {
            s += ns;
            s += ".";
        }
        s += (n ? n : "?");
        if (!a.class_get_parent) break;
        k = a.class_get_parent(k);
    }
    return s;
}

bool GameSysObjectIsNamed(void* obj, const char* name) {
    const Il2CppApi& a = Il2Cpp();
    if (!obj || !name || !a.object_get_class || !a.class_get_name) return false;
    void* k = a.object_get_class(obj);
    int guard = 0;
    while (k && guard++ < 32) {
        const char* n = a.class_get_name(k);
        if (n && std::strcmp(n, name) == 0) return true;
        if (!a.class_get_parent) break;
        k = a.class_get_parent(k);
    }
    return false;
}

// ---- 指令 44: 类型体检 ----
void GameSysTypeAudit() {
    const Il2CppApi& a = Il2Cpp();
    LogG("[audit] ===== 关键 getter: 声明返回类型 vs 实际对象类 =====");
    {
        char b[224];
        snprintf(b, sizeof(b),
                 "[audit] 反射可用性: object_get_class=%d class_get_parent=%d "
                 "method_get_return_type=%d class_from_type=%d class_get_name=%d",
                 a.object_get_class ? 1 : 0, a.class_get_parent ? 1 : 0,
                 a.method_get_return_type ? 1 : 0, a.class_from_type ? 1 : 0,
                 a.class_get_name ? 1 : 0);
        LogG(b);
    }
    void* mgr = CameraManagerInstance();
    if (!mgr) {
        LogG("[audit] CameraManager 实例不可用(先发指令 2 打开自由相机再体检)");
        return;
    }
    LogG("[audit] CameraManager 实际类: " + GameSysClassChainOf(mgr));

    struct Item {
        void* mi;
        bool has;
        const char* label;
    };
    const Item items[] = {
        {g_sys.mi_curVirtualCam, g_sys.has_curVirtualCam, "CameraManager.get_curVirtualCam"},
        {g_sys.mi_curActiveController, g_sys.has_curActiveController,
         "CameraManager.get_curActiveController"},
        {g_sys.mi_getMainMarketing, g_sys.has_getMainMarketing,
         "CameraManager.GetMainMarketingCameraController"},
        {g_sys.mi_getMainLevel, g_sys.has_getMainLevel,
         "CameraManager.GetMainLevelCameraController"},
    };
    for (const Item& it : items) {
        if (!it.has || !it.mi) {
            LogG(std::string("[audit] ") + it.label + " : 未解析(签名不匹配或类里没有)");
            continue;
        }
        const std::string declared = DeclaredReturnName(it.mi);
        void* obj = CallGetter(it.mi, mgr, it.label);
        std::string line = std::string("[audit] ") + it.label + " | 声明=" + declared;
        if (!obj) {
            line += " | 返回 null";
            LogG(line);
            continue;
        }
        line += " | 实际=" + GameSysClassChainOf(obj);
        line += std::string(" | 类型校验=") + (InstanceIsA(it.mi, obj) ? "通过" : "不通过");
        LogG(line);
    }

    // vcam 的 Follow/LookAt: 按"方法所属类"校验(声明返回类型是接口, 父链比对不适用)
    if (g_sys.has_curVirtualCam && g_sys.mi_curVirtualCam) {
        void* vcam = CallGetter(g_sys.mi_curVirtualCam, mgr, "audit.vcam");
        if (!vcam) {
            LogG("[audit] vcam 实例为 null → 候选 1/2 当前不可用");
        } else if (!VcamObjectUsable(vcam)) {
            LogG("[audit] get_curVirtualCam 的实际类不是 Cinemachine 虚拟相机 → 候选 1/2 不可用"
                 "(此时绝不能硬调 Follow/LookAt)");
        } else {
            LogG("[audit] vcam 可用(声明类型是接口 ICinemachineCamera, 接口不在父类链上; "
                 "已改为按 Follow/LookAt 所属类 CinemachineVirtualCamera 校验) → 读取 Follow/LookAt");
            struct Target {
                void* mi;
                bool has;
                const char* label;
            };
            const Target ts[] = {
                {g_sys.mi_vcamFollow, g_sys.has_vcamFollow, "Follow"},
                {g_sys.mi_vcamLookAt, g_sys.has_vcamLookAt, "LookAt"},
            };
            for (const Target& t : ts) {
                if (!t.has || !t.mi) {
                    LogG(std::string("[audit] vcam.") + t.label + " : 未解析");
                    continue;
                }
                void* tr = CallGetter(t.mi, vcam, t.label);
                if (!tr) {
                    LogG(std::string("[audit] vcam.") + t.label + " = null");
                    continue;
                }
                float p[3] = {0.f, 0.f, 0.f};
                const bool ok = TransformPos(tr, p);
                char b[320];
                if (ok) {
                    snprintf(b, sizeof(b), "[audit] vcam.%s | 实际=%s | 世界位置=(%.3f, %.3f, %.3f)",
                             t.label, GameSysClassChainOf(tr).c_str(), p[0], p[1], p[2]);
                } else {
                    snprintf(b, sizeof(b), "[audit] vcam.%s | 实际=%s | 世界位置读取失败", t.label,
                             GameSysClassChainOf(tr).c_str());
                }
                LogG(b);
            }
        }
    } else {
        LogG("[audit] get_curVirtualCam 未解析");
    }

    if (g_sys.snapshotController) {
        LogG("[audit] 快照相机实例实际类: " + GameSysClassChainOf(g_sys.snapshotController));
    } else {
        LogG("[audit] 快照相机实例未就绪(先发指令 27 进入拍照模式)");
    }
    LogG("[audit] ===== 体检结束 =====");
}

// ---- 指令 45: SetCameraOffset 写入 ----
bool GameSysSnapshotSetCameraOffset(const float v[3], int convention) {
    if (!g_sys.has_snapSetOffset || !g_sys.mi_snapSetOffset) {
        LogG("[probe45] SetCameraOffset 未解析, 无法写入");
        return false;
    }
    if (!g_sys.snapshotController) {
        LogG("[probe45] 快照相机实例未就绪, 无法写入(先发指令 27)");
        return false;
    }
    const Il2CppApi& a = Il2Cpp();
    if (!a.loaded || !a.runtime_invoke) return false;

    void* arg = nullptr;
    void* boxed = nullptr;
    if (convention == 1) {
        arg = const_cast<float*>(v);  // 裸值指针: 部分版本按值拷 12 字节
    } else {
        // 装箱对象: Vector3 装箱后把值写进 unbox 数据区(等价于 il2cpp_value_box)
        if (!a.method_get_param || !a.class_from_type || !a.class_is_valuetype ||
            !a.object_new || !a.object_unbox) {
            LogG("[probe45] 缺少装箱所需的反射导出, 跳过装箱约定");
            return false;
        }
        void* pt = a.method_get_param(g_sys.mi_snapSetOffset, 0);
        if (!pt) return false;
        void* k = a.class_from_type(pt);
        if (!k) return false;
        if (!a.class_is_valuetype(k)) {
            LogG("[probe45] 参数不是值类型 → 装箱约定不适用(已拒绝调用, 避免用错对象)");
            return false;
        }
        uint32_t align = 0;
        const int size = a.class_value_size ? a.class_value_size(k, &align) : -1;
        if (size >= 0 && size < static_cast<int>(sizeof(float) * 3)) {
            LogG("[probe45] 参数尺寸小于 Vector3(12 字节) → 拒绝按 3 个 float 写入");
            return false;
        }
        boxed = a.object_new(k);
        if (!boxed) return false;
        void* raw = a.object_unbox(boxed);
        if (!raw) return false;
        std::memcpy(raw, v, sizeof(float) * 3);
        arg = boxed;
    }

    if (a.thread_attach && a.domain_get) {
        void* d = a.domain_get();
        if (d) a.thread_attach(d);
    }
    void* params[1] = {arg};
    void* exc = nullptr;
    a.runtime_invoke(g_sys.mi_snapSetOffset, g_sys.snapshotController, params, &exc);
    if (exc) {
        LogG("[probe45] SetCameraOffset 抛出托管异常(该约定可能不适用)");
        return false;
    }
    return true;
}

void GameSysReportSetOffsetSignature() {
    const Il2CppApi& a = Il2Cpp();
    if (!g_sys.has_snapSetOffset || !g_sys.mi_snapSetOffset) {
        LogG("[probe45] SetCameraOffset 未解析(契约缺失) → 方案 B 需要换路");
        return;
    }
    char b[256];
    const int pc = a.method_get_param_count
                       ? static_cast<int>(a.method_get_param_count(g_sys.mi_snapSetOffset))
                       : -1;
    snprintf(b, sizeof(b), "[probe45] 签名: SetCameraOffset 参数个数=%d", pc);
    LogG(b);
    for (int i = 0; i < pc && i < 4; ++i) {
        void* pt = a.method_get_param ? a.method_get_param(g_sys.mi_snapSetOffset,
                                                           static_cast<uint32_t>(i))
                                      : nullptr;
        if (!pt) break;
        void* k = a.class_from_type ? a.class_from_type(pt) : nullptr;
        uint32_t align = 0;
        const int size = (k && a.class_value_size) ? a.class_value_size(k, &align) : -1;
        const int vt = (k && a.class_is_valuetype) ? (a.class_is_valuetype(k) ? 1 : 0) : -1;
        snprintf(b, sizeof(b), "[probe45]   参数[%d] 类型=%s 值类型=%d 尺寸=%d", i,
                 TypeName(pt).c_str(), vt, size);
        LogG(b);
    }
    LogG("[probe45]   返回类型=" + DeclaredReturnName(g_sys.mi_snapSetOffset));
    LogG("[probe45] 说明: 值类型参数在 il2cpp runtime_invoke 有两种约定 —— 装箱对象(默认) "
         "或裸值指针; 指令 45 两种都试, 由「写入后相机是否按比例移动」判定哪种生效。");
}

// ---- 指令 45: 状态机 ----
namespace {

constexpr int kOffStepCount = 6;      // 0=基线 1..5=写入
constexpr int kOffSampleFrames = 6;   // 每个测量点平均的帧数

struct OffsetCalib {
    bool active = false;
    bool entered = false;
    int convList[2] = {0, 1};
    int convCount = 2;
    int convIdx = 0;
    int settle = 45;
    int settleLeft = 0;
    int sampleLeft = 0;
    int step = 0;
    double acc[3] = {0.0, 0.0, 0.0};
    int accN = 0;
    float pts[kOffStepCount][3] = {};
    bool lastWriteOk = true;
    int goodConv = -1;
    float anchor[3] = {};
    bool anchorOk = false;
};

OffsetCalib g_oc;

const float kOffCmdVal[kOffStepCount][3] = {
    {0.f, 0.f, 0.f},  // 0: 基线(不写)
    {0.f, 0.f, 0.f},  // 1: 偏移 (0,0,0)
    {1.f, 0.f, 0.f},  // 2: 偏移 (1,0,0)
    {2.f, 0.f, 0.f},  // 3: 偏移 (2,0,0) —— 用于验证线性(应恰好是 2 倍)
    {0.f, 1.f, 0.f},  // 4: 偏移 (0,1,0)
    {0.f, 0.f, 1.f},  // 5: 偏移 (0,0,1)
};
const char* kOffStepName[kOffStepCount] = {"基线(不写)", "偏移(0,0,0)", "偏移(1,0,0)",
                                           "偏移(2,0,0)", "偏移(0,1,0)", "偏移(0,0,1)"};

const char* ConvName(int c) { return c == 1 ? "裸值指针" : "装箱对象"; }
int CurConv() { return g_oc.convList[g_oc.convIdx]; }

void OffsetCalibEnterStep() {
    const int s = g_oc.step;
    g_oc.lastWriteOk = true;
    if (s > 0) {
        g_oc.lastWriteOk = GameSysSnapshotSetCameraOffset(
            kOffCmdVal[s], CurConv());
    }
    g_oc.settleLeft = g_oc.settle;
    g_oc.sampleLeft = kOffSampleFrames;
    g_oc.acc[0] = g_oc.acc[1] = g_oc.acc[2] = 0.0;
    g_oc.accN = 0;
}

void OffsetCalibFinishConv() {
    const float* base = g_oc.pts[0];
    const float* p0 = g_oc.pts[1];
    const float* p1 = g_oc.pts[2];
    const float* p2 = g_oc.pts[3];
    const float* pY = g_oc.pts[4];
    const float* pZ = g_oc.pts[5];
    const int c = CurConv();
    char b[448];

    float rx[3], rx2[3], ry[3], rz[3];
    for (int i = 0; i < 3; ++i) {
        rx[i] = p1[i] - p0[i];
        rx2[i] = p2[i] - p0[i];
        ry[i] = pY[i] - p0[i];
        rz[i] = pZ[i] - p0[i];
    }
    const double dBase = Dist3(base, p0);
    const double lx = Dist3(p0, p1), lx2 = Dist3(p0, p2), ly = Dist3(p0, pY),
                 lz = Dist3(p0, pZ);
    const bool effective = (lx > 0.05 || ly > 0.05 || lz > 0.05);
    const bool linear = (lx > 0.05) && (std::fabs(lx2 - 2.0 * lx) <= 0.2 * lx2);

    snprintf(b, sizeof(b),
             "[probe45] 约定=%s | 基线 pos=(%.3f, %.3f, %.3f) | 写(0,0,0)后 pos=(%.3f, %.3f, "
             "%.3f) 位移=%.3f",
             ConvName(c), base[0], base[1], base[2], p0[0], p0[1], p0[2], dBase);
    LogG(b);
    snprintf(b, sizeof(b),
             "[probe45] 约定=%s | 响应 X=(%.3f,%.3f,%.3f) |X|=%.3f  2X=(%.3f,%.3f,%.3f) "
             "|2X|=%.3f  Y=(%.3f,%.3f,%.3f) |Y|=%.3f  Z=(%.3f,%.3f,%.3f) |Z|=%.3f",
             ConvName(c), rx[0], rx[1], rx[2], lx, rx2[0], rx2[1], rx2[2], lx2, ry[0], ry[1],
             ry[2], ly, rz[0], rz[1], rz[2], lz);
    LogG(b);
    {
        float got[3] = {0.f, 0.f, 0.f};
        const bool gotOk = GameSysSnapshotCameraOffset(got);
        snprintf(b, sizeof(b), "[probe45] 约定=%s | 判定: 参数%s 线性=%s | GetCameraOffset 读回=%s",
                 ConvName(c), effective ? "生效" : "未生效", linear ? "是" : "否",
                 gotOk ? "" : "不可用");
        std::string line = b;
        if (gotOk) {
            snprintf(b, sizeof(b), "(%.3f, %.3f, %.3f)", got[0], got[1], got[2]);
            line += b;
        }
        LogG(line);
    }
    if (effective && g_oc.goodConv < 0) {
        g_oc.goodConv = c;
        g_oc.anchor[0] = p0[0];
        g_oc.anchor[1] = p0[1];
        g_oc.anchor[2] = p0[2];
        g_oc.anchorOk = true;
        snprintf(b, sizeof(b),
                 "[probe45] *** 结论: 约定「%s」可用。偏移=(0,0,0) 时相机落在 "
                 "锚点=(%.3f, %.3f, %.3f) —— 这就是「角色位置 + 偏移」里的机架零点 P0。",
                 ConvName(c), p0[0], p0[1], p0[2]);
        LogG(b);
        // 偏移坐标系: 实测 世界位移 = R · 偏移。把 R 归一化成"绕世界 X 转 θ"并打印可直接
        // 粘进 ini 的 pose_offset_basis(行主序 9 个数), 供 pose_mode=4(偏移驱动) 使用。
        {
            const double ylen = std::sqrt(ry[1] * ry[1] + ry[2] * ry[2]);
            const double zlen = std::sqrt(rz[1] * rz[1] + rz[2] * rz[2]);
            if (ylen > 1e-4 && zlen > 1e-4) {
                const double cs = ry[1] / ylen, sn = ry[2] / ylen;
                const double theta = std::atan2(sn, cs) * 57.2957795;
                snprintf(b, sizeof(b),
                         "[probe45] 偏移坐标系: X 响应=%+.4f/%+.4f/%+.4f | Y 响应归一="
                         "(0, %+.5f, %+.5f) | Z 响应归一=(0, %+.5f, %+.5f) → 绕世界 X 转 "
                         "%+.2f°(cos=%.5f sin=%.5f)",
                         rx[0], rx[1], rx[2], cs, sn, rz[1] / zlen, rz[2] / zlen, theta, cs, sn);
                LogG(b);
                snprintf(b, sizeof(b),
                         "[probe45] 若要启用偏移驱动(pose_mode=4), 把下面这行写进 ini: "
                         "pose_offset_basis=%.5f,%.5f,%.5f,%.5f,%.5f,%.5f,%.5f,%.5f,%.5f",
                         rx[0], rx[1], rx[2], 0.0, cs, -sn, 0.0, sn, cs);
                LogG(b);
            }
        }
    }
}

void OffsetCalibNextConvOrFinish() {
    const int justTested = CurConv();
    // 收尾/换约定前都把偏移退回 (0,0,0): 0 表示"相机正好在锚点上", 是最安全的位置,
    // 也避免上一种约定的残留(可能是被误读的垃圾值)污染下一种约定的基线。
    GameSysSnapshotSetCameraOffset(kOffCmdVal[1], justTested);
    ++g_oc.convIdx;
    if (g_oc.convIdx >= g_oc.convCount) {
        g_oc.active = false;
        char b[384];
        if (g_oc.goodConv >= 0) {
            snprintf(b, sizeof(b),
                     "[probe45] 全部结束 | 可用约定=%s | 锚点=(%.3f, %.3f, %.3f)",
                     ConvName(g_oc.goodConv), g_oc.anchor[0], g_oc.anchor[1], g_oc.anchor[2]);
            LogG(b);
            LogG("[probe45] 下一步: 指令 46 采样锚点候选(看哪个候选等于这个锚点) → 指令 42 "
                 "选定该候选 → 指令 41 a0=1 自动校准(无需肉眼对齐)");
            GameSysReportAnchorCandidates(g_oc.anchor);
        } else {
            LogG("[probe45] 全部结束 | 两种约定都没让相机按偏移移动 → SetCameraOffset 这条路"
                 "不通(可能: 当前激活相机不是快照相机 / 偏移只影响编辑态 / 参数不是 Vector3)。"
                 "先看上面的签名与 [audit] 输出。");
        }
        return;
    }
    g_oc.step = 0;
    g_oc.entered = false;
    g_oc.settleLeft = 0;
    g_oc.sampleLeft = 0;
    LogG(std::string("[probe45] 换约定: ") + ConvName(CurConv()));
}

void OffsetCalibTick(const float enginePos[3]) {
    if (!g_oc.active) return;
    if (!g_oc.entered) {
        g_oc.entered = true;
        OffsetCalibEnterStep();
        return;
    }
    if (g_oc.settleLeft > 0) {
        --g_oc.settleLeft;
        return;
    }
    if (g_oc.sampleLeft > 0) {
        g_oc.acc[0] += enginePos[0];
        g_oc.acc[1] += enginePos[1];
        g_oc.acc[2] += enginePos[2];
        ++g_oc.accN;
        if (--g_oc.sampleLeft == 0) {
            const int s = g_oc.step;
            const int n = g_oc.accN > 0 ? g_oc.accN : 1;
            g_oc.pts[s][0] = static_cast<float>(g_oc.acc[0] / n);
            g_oc.pts[s][1] = static_cast<float>(g_oc.acc[1] / n);
            g_oc.pts[s][2] = static_cast<float>(g_oc.acc[2] / n);
            char b[288];
            if (s == 0) {
                snprintf(b, sizeof(b), "[probe45] 约定=%s | %s → pos=(%.3f, %.3f, %.3f)",
                         ConvName(CurConv()), kOffStepName[s], g_oc.pts[s][0], g_oc.pts[s][1],
                         g_oc.pts[s][2]);
            } else {
                snprintf(b, sizeof(b),
                         "[probe45] 约定=%s | 写%s%s → pos=(%.3f, %.3f, %.3f)  相对基线 "
                         "(%.3f, %.3f, %.3f)",
                         ConvName(CurConv()), kOffStepName[s],
                         g_oc.lastWriteOk ? "" : "(写入失败/被拒绝)", g_oc.pts[s][0],
                         g_oc.pts[s][1], g_oc.pts[s][2],
                         g_oc.pts[s][0] - g_oc.pts[0][0], g_oc.pts[s][1] - g_oc.pts[0][1],
                         g_oc.pts[s][2] - g_oc.pts[0][2]);
            }
            LogG(b);
            ++g_oc.step;
            if (g_oc.step >= kOffStepCount) {
                OffsetCalibFinishConv();
                OffsetCalibNextConvOrFinish();
            } else {
                OffsetCalibEnterStep();
            }
        }
        return;
    }
    OffsetCalibEnterStep();
}

// ---- 指令 46: 锚点候选采样器 ----
constexpr int kSampleMax = 900;
constexpr int kEngineRow = 0;  // 行 0 = 引擎相机自身

struct AnchorSampler {
    bool active = false;
    int cap = 0;
    int n = 0;
    int cnt[kAnchorSourceCount] = {};
    float data[kAnchorSourceCount][3][kSampleMax] = {};
};

AnchorSampler g_as;

float MedianOf(std::vector<float>& v) {
    if (v.empty()) return 0.f;
    const size_t mid = v.size() / 2;
    std::nth_element(v.begin(), v.begin() + mid, v.end());
    return v[mid];
}

void AnchorSampleFinish() {
    char b[384];
    snprintf(b, sizeof(b), "[probe46] 采样结束: 有效帧=%d (每点平均 %.1f s @120fps)", g_as.n,
             g_as.n / 120.0);
    LogG(b);

    float med[kAnchorSourceCount][3] = {};
    float rng[kAnchorSourceCount][3] = {};
    for (int row = 0; row < kAnchorSourceCount; ++row) {
        if (g_as.cnt[row] == 0) continue;
        for (int ax = 0; ax < 3; ++ax) {
            std::vector<float> v;
            v.reserve(g_as.cnt[row]);
            for (int i = 0; i < g_as.cnt[row]; ++i) v.push_back(g_as.data[row][ax][i]);
            med[row][ax] = MedianOf(v);
            float lo = v[0], hi = v[0];
            for (float x : v) {
                if (x < lo) lo = x;
                if (x > hi) hi = x;
            }
            rng[row][ax] = hi - lo;
        }
    }
    // 行 0 = 引擎相机
    if (g_as.cnt[kEngineRow] > 0) {
        snprintf(b, sizeof(b),
                 "[probe46] %-16s 中位数=(%.4f, %.4f, %.4f) 极差=(%.5f, %.5f, %.5f)",
                 "引擎相机", med[kEngineRow][0], med[kEngineRow][1], med[kEngineRow][2],
                 rng[kEngineRow][0], rng[kEngineRow][1], rng[kEngineRow][2]);
        LogG(b);
    }
    for (int s = 1; s < kAnchorSourceCount; ++s) {
        if (g_as.cnt[s] == 0) {
            snprintf(b, sizeof(b), "[probe46] %-16s 不可用(该候选当前取不到值)",
                     GameSysAnchorSourceName(s));
            LogG(b);
            continue;
        }
        const double d = Dist3(med[s], med[kEngineRow]);
        snprintf(b, sizeof(b),
                 "[probe46] %-16s 中位数=(%.4f, %.4f, %.4f) 极差=(%.5f, %.5f, %.5f) 距引擎相机=%.4f",
                 GameSysAnchorSourceName(s), med[s][0], med[s][1], med[s][2], rng[s][0],
                 rng[s][1], rng[s][2], d);
        LogG(b);
    }
    LogG("[probe46] 判据: 极差很小(≤0.02)且距引擎相机明显(>0.1) = 可用的静止锚点; "
         "若恰好等于 [probe45] 的锚点, 就是「角色位置」本身。");
    std::string hint = "[probe46] 可用候选:";
    int nOk = 0;
    for (int s = 1; s < kAnchorSourceCount; ++s) {
        if (g_as.cnt[s] == 0) continue;
        const float spread = rng[s][0] + rng[s][1] + rng[s][2];
        if (spread <= 0.06f) {
            hint += " ";
            hint += GameSysAnchorSourceName(s);
            ++nOk;
        }
    }
    if (nOk == 0) hint += " (无: 所有候选都在动或取不到值)";
    LogG(hint);
}

void AnchorSampleTick(const float enginePos[3]) {
    if (!g_as.active) return;
    for (int ax = 0; ax < 3; ++ax) g_as.data[kEngineRow][ax][g_as.cnt[kEngineRow]] = enginePos[ax];
    ++g_as.cnt[kEngineRow];
    for (int s = 1; s < kAnchorSourceCount; ++s) {
        float p[3] = {0.f, 0.f, 0.f};
        if (!GameSysAnchorPosition(s, enginePos, p)) continue;
        for (int ax = 0; ax < 3; ++ax) g_as.data[s][ax][g_as.cnt[s]] = p[ax];
        ++g_as.cnt[s];
    }
    ++g_as.n;
    if (g_as.cnt[kEngineRow] >= g_as.cap || g_as.n >= g_as.cap) {
        g_as.active = false;
        AnchorSampleFinish();
    }
}

}  // namespace

bool GameSysOffsetCalibBegin(int convMode, int settle) {
    if (!g_sys.has_snapSetOffset || !g_sys.mi_snapSetOffset) {
        LogG("[probe45] 无法开始: SetCameraOffset 未解析");
        return false;
    }
    if (!g_sys.snapshotController) {
        LogG("[probe45] 无法开始: 快照相机实例未就绪(先发指令 27 进入拍照模式)");
        return false;
    }
    GameSysReportSetOffsetSignature();
    g_oc = OffsetCalib();
    if (convMode == 1) {
        g_oc.convList[0] = 0;
        g_oc.convCount = 1;
    } else if (convMode == 2) {
        g_oc.convList[0] = 1;
        g_oc.convCount = 1;
    } else {
        g_oc.convList[0] = 0;
        g_oc.convList[1] = 1;
        g_oc.convCount = 2;
    }
    g_oc.settle = settle > 0 ? settle : 45;
    g_oc.active = true;
    g_oc.entered = false;
    g_oc.step = 0;
    {
        const double per = (g_oc.settle + kOffSampleFrames) * kOffStepCount * g_oc.convCount / 120.0;
        char b[224];
        snprintf(b, sizeof(b),
                 "[probe45] 开始写入-测量标定: 约定数=%d 每步稳定帧=%d 预计 %.1f 秒 "
                 "(期间关闭位姿写入, 结束自动恢复)",
                 g_oc.convCount, g_oc.settle, per);
        LogG(b);
    }
    return true;
}

bool GameSysOffsetCalibActive() { return g_oc.active; }

bool GameSysAnchorSampleBegin(int frames) {
    if (frames <= 0) frames = 600;
    if (frames > kSampleMax) frames = kSampleMax;
    // 避免 54KB 的临时对象: 逐字段清零
    g_as.active = false;
    g_as.cap = frames;
    g_as.n = 0;
    for (int s = 0; s < kAnchorSourceCount; ++s) g_as.cnt[s] = 0;
    g_as.active = true;
    {
        char b[192];
        snprintf(b, sizeof(b), "[probe46] 开始采样锚点候选: %d 帧 (约 %.1f 秒 @120fps), "
                               "期间关闭位姿写入",
                 frames, frames / 120.0);
        LogG(b);
    }
    return true;
}

bool GameSysAnchorSampleActive() { return g_as.active; }

// ================= v1.0.0ai: 隐藏 UI 探测 =================
//
// 约束(用户给的硬条件): C1 不进原生相机模式; C2 不改键位(原生 X 会让很多键位失效);
// C3 攻击仍可用; C4 不碰账号 UID 水印。=> 只能动**渲染层**, 不能动 UI 的逻辑状态
// (SetActive(false)/canvas.enabled=false/CanvasGroup.blocksRaycasts=false 都改逻辑, 已排除)。
// 因此探测顺序: ① UI 相机的 cullingMask ② HUD 根节点的图层 ③ 复用原生 X(最后手段)。
namespace {

bool ContainsCI(const std::string& hay, const char* needle) {
    if (needle[0] == '\0') return false;
    const size_t n = strlen(needle);
    for (size_t i = 0; i + n <= hay.size(); ++i) {
        size_t k = 0;
        while (k < n && tolower(static_cast<unsigned char>(hay[i + k])) ==
                           tolower(static_cast<unsigned char>(needle[k])))
            ++k;
        if (k == n) return true;
    }
    return false;
}

// ---- 读回原语(全部走 runtime_invoke + object_unbox, 不碰对象内存) ----
bool ReadInt32(void* mi, void* obj, int* out, const char* label) {
    if (!mi) return false;
    const Il2CppApi& a = Il2Cpp();
    void* boxed = InvokeLogged(mi, obj, nullptr, label);
    if (!boxed || !a.object_unbox) return false;
    void* raw = a.object_unbox(boxed);
    if (!raw) return false;
    *out = *static_cast<int32_t*>(raw);
    return true;
}

bool ReadBool(void* mi, void* obj, int* out, const char* label) {
    if (!mi) return false;
    const Il2CppApi& a = Il2Cpp();
    void* boxed = InvokeLogged(mi, obj, nullptr, label);
    if (!boxed || !a.object_unbox) return false;
    void* raw = a.object_unbox(boxed);
    if (!raw) return false;
    *out = (*static_cast<uint8_t*>(raw)) ? 1 : 0;
    return true;
}

bool ReadFloat(void* mi, void* obj, float* out, const char* label) {
    if (!mi) return false;
    const Il2CppApi& a = Il2Cpp();
    void* boxed = InvokeLogged(mi, obj, nullptr, label);
    if (!boxed || !a.object_unbox) return false;
    void* raw = a.object_unbox(boxed);
    if (!raw) return false;
    *out = *static_cast<float*>(raw);
    return true;
}

// 托管字符串读取(ASCII 相机名足够; 高位字节丢弃)。缺 string API 时返回占位符。
std::string ReadStr(void* mi, void* obj, const char* label) {
    if (!mi) return "?";
    const Il2CppApi& a = Il2Cpp();
    void* s = InvokeLogged(mi, obj, nullptr, label);
    if (!s) return "(null)";
    if (!a.string_length || !a.string_chars) return "(无 string API)";
    const int32_t n = a.string_length(s);
    if (n < 0) return "?";
    if (n > 256) return "(过长)";
    uint16_t* c = a.string_chars(s);
    if (!c) return "?";
    std::string o;
    o.reserve(static_cast<size_t>(n));
    for (int32_t i = 0; i < n; ++i) {
        const uint16_t ch = c[i];
        o += (ch < 0x80) ? static_cast<char>(ch) : '?';
    }
    return o;
}

struct CamProbe {
    void* obj = nullptr;
    std::string cls;
    std::string name;
    bool hasMask = false;   // 遮罩读取是否成功(遮罩可能是负数, 不能用 -1 当判据)
    int mask = -1;
    int enabled = -1;
    int clear = -1;
    int hasTargetTex = -1;
    float depth = -999.f;
};

void ProbeCamera(void* cam, CamProbe* p) {
    if (!cam) return;
    p->obj = cam;
    p->cls = GameSysClassChainOf(cam);
    if (g_sys.has_camNameGet)
        p->name = ReadStr(g_sys.mi_camNameGet, cam, "Camera.get_name");
    if (g_sys.has_camMaskGet) {
        int mv = 0;
        if (ReadInt32(g_sys.mi_camMaskGet, cam, &mv, "Camera.get_cullingMask")) {
            p->mask = mv;
            p->hasMask = true;
        }
    }
    if (g_sys.has_camEnabledGet) ReadBool(g_sys.mi_camEnabledGet, cam, &p->enabled, "Camera.get_enabled");
    if (g_sys.has_camClearFlagsGet) ReadInt32(g_sys.mi_camClearFlagsGet, cam, &p->clear, "Camera.get_clearFlags");
    if (g_sys.has_camDepthGet) ReadFloat(g_sys.mi_camDepthGet, cam, &p->depth, "Camera.get_depth");
    if (g_sys.has_camTargetTexGet) {
        void* t = InvokeLogged(g_sys.mi_camTargetTexGet, cam, nullptr, "Camera.get_targetTexture");
        p->hasTargetTex = t ? 1 : 0;
    }
}

std::string CamLine(const CamProbe& p) {
    char b[512];
    snprintf(b, sizeof(b),
             "名='%s' 遮罩=0x%08X(%d) 启用=%d 深度=%.1f clearFlags=%d 渲染到纹理=%d 类=%s",
             p.name.c_str(), static_cast<unsigned>(p.mask), p.mask, p.enabled,
             p.depth, p.clear, p.hasTargetTex, p.cls.c_str());
    std::string s(b);
    if (p.mask >= 0) {  // 该相机渲染哪些层(找 UI 图层时最有用的一列)
        s += " 位=[";
        bool first = true;
        for (int i = 0; i < 32; ++i) {
            if (!(static_cast<unsigned>(p.mask) & (1u << i))) continue;
            if (!first) s += ",";
            s += std::to_string(i);
            first = false;
        }
        s += "]";
    }
    return s;
}

// Camera.allCameras 枚举(需要可选导出 array_length + array_addr; 缺则返回 -1 = 不可用)
// 安全前置: ① 返回值声明类型必须含 "Camera["(否则不碰内存)
//           ② 长度必须与 get_allCamerasCount 一致(否则说明我按错的类型解释了这个数组)
//           ③ 长度必须 ≤ cap。这三条任意一条不满足 -> 返回负值, 不读任何元素。
int CollectAllCameras(std::vector<void*>& out, int cap) {
    const Il2CppApi& a = Il2Cpp();
    if (!g_sys.has_allCameras) return -1;
    if (!a.array_length || !a.array_addr) return -1;
    {
        const std::string rt = a.method_get_return_type
                                   ? TypeName(a.method_get_return_type(g_sys.mi_allCameras))
                                   : "?";
        if (rt.find("Camera[") == std::string::npos) {
            LogG("[ui] allCameras 返回类型不是 Camera[] (" + rt + ") -> 放弃枚举");
            return -3;
        }
    }
    void* arr = InvokeLogged(g_sys.mi_allCameras, nullptr, nullptr, "Camera.get_allCameras");
    if (!arr) return -2;
    const uint32_t n = a.array_length(arr);
    if (n > static_cast<uint32_t>(cap)) {
        char b[160];
        snprintf(b, sizeof(b), "[ui] allCameras 长度异常(%u > %d) -> 放弃枚举", n, cap);
        LogG(b);
        return -3;
    }
    if (g_sys.has_allCamerasCount) {
        int c = -1;
        if (ReadInt32(g_sys.mi_allCamerasCount, nullptr, &c, "Camera.get_allCamerasCount")) {
            if (c >= 0 && static_cast<uint32_t>(c) != n) {
                char b[160];
                snprintf(b, sizeof(b),
                         "[ui] allCameras 长度(%u)与 allCamerasCount(%d) 不一致 -> 放弃枚举",
                         n, c);
                LogG(b);
                return -3;
            }
        }
    }
    for (uint32_t i = 0; i < n; ++i) {
        char* slot = a.array_addr(arr, static_cast<int32_t>(sizeof(void*)), i);
        if (!slot) continue;
        void* cam = *reinterpret_cast<void**>(slot);
        if (cam) out.push_back(cam);
    }
    return static_cast<int>(n);
}

void* MainCamera() {
    if (!g_sys.has_camMainGet) return nullptr;
    return InvokeLogged(g_sys.mi_camMainGet, nullptr, nullptr, "Camera.get_main");
}

// UI 体检里要看的成员名关键词(只读元数据, 只打印签名)
const char* kUiKw[] = {"ui",      "hud",     "canvas",  "widget", "overlay",
                       "mask",    "layer",   "visib",   "hide",   "show",
                       "panel",   "render",  "screen",  "photo",  "snapshot",
                       "graphic", "display", "hidden",  "enable"};

bool NameHasUiKw(const char* name) {
    if (!name) return false;
    for (const char* k : kUiKw) {
        if (ContainsCI(name, k)) return true;
    }
    return false;
}

std::string MethodSig(void* m) {
    const Il2CppApi& a = Il2Cpp();
    const int pc = a.method_get_param_count
                       ? static_cast<int>(a.method_get_param_count(m)) : -1;
    std::string s = a.method_get_name ? a.method_get_name(m) : "?";
    s += "(";
    for (int i = 0; i < pc; ++i) {
        if (i) s += ", ";
        s += (a.method_get_param ? TypeName(a.method_get_param(m, i)) : "?");
    }
    s += ") -> ";
    s += (a.method_get_return_type ? TypeName(a.method_get_return_type(m)) : "?");
    return s;
}

// 打印某类里"名称含 UI 关键词"的成员签名(含字段名) + 收集:
//   camGetters = 0 参 -> UnityEngine.Camera 的方法
//   intGetters = 0 参 -> System.Int32 且名字像"层/遮罩"的方法(能直接给出 UI 图层号)
void DumpUiMembers(const char* asmName, const char* ns, const char* cls,
                   std::vector<void*>* camGetters, std::vector<void*>* intGetters,
                   int* budget) {
    const Il2CppApi& a = Il2Cpp();
    void* klass = ResolveClass(asmName, ns, cls);
    char b[320];
    if (!klass) {
        snprintf(b, sizeof(b), "[ui]   %s.%s: 类未找到", ns, cls);
        LogG(b);
        return;
    }
    snprintf(b, sizeof(b), "[ui]   %s.%s 名称含 UI/图层 关键词的成员:", ns, cls);
    LogG(b);
    int shown = 0;
    if (a.class_get_methods && a.method_get_name) {
        void* it = nullptr;
        void* m = nullptr;
        int c = 0;
        while ((m = a.class_get_methods(klass, &it)) != nullptr && c < 6000) {
            ++c;
            const char* mn = a.method_get_name(m);
            if (!mn) continue;
            const int pc = a.method_get_param_count
                               ? static_cast<int>(a.method_get_param_count(m)) : -1;
            if (pc == 0) {
                const std::string rt = a.method_get_return_type
                                           ? TypeName(a.method_get_return_type(m))
                                           : "?";
                // 收集"0 参且返回 UnityEngine.Camera"的 getter(候选 UI 相机入口)
                if (camGetters && rt == "UnityEngine.Camera") camGetters->push_back(m);
                // 收集"0 参且返回 int"的"层/遮罩"getter(直接给出 UI 图层号)
                if (intGetters && rt == "System.Int32" &&
                    (ContainsCI(mn, "layer") || ContainsCI(mn, "mask") ||
                     ContainsCI(mn, "ui")))
                    intGetters->push_back(m);
            }
            if (!NameHasUiKw(mn)) continue;
            if (*budget <= 0) continue;
            LogG("       M " + MethodSig(m));
            --*budget;
            ++shown;
        }
    }
    if (a.class_get_fields && a.field_get_name) {
        void* it = nullptr;
        void* f = nullptr;
        int c = 0;
        while ((f = a.class_get_fields(klass, &it)) != nullptr && c < 3000) {
            ++c;
            const char* fn = a.field_get_name(f);
            if (!fn || !NameHasUiKw(fn)) continue;
            if (*budget <= 0) continue;
            LogG(std::string("       F ") + fn);
            --*budget;
            ++shown;
        }
    }
    if (shown == 0) LogG("       (无)");
}

// ---- 指令 65 的写入-测量状态 ----
struct UiMaskTest {
    bool pending = false;
    uint64_t until = 0;
    uint64_t midAt = 0;
    bool midLogged = false;
    int target = 0;
    int oldMask = 0;
    int want = 0;    // v1.0.0ar: 本次要求写入的值(保持中读回用它做判据, 不再写死 0)
    int chMode = 0;  // 0=直接写遮罩 1=游戏配置栈(AddUICamCullingMaskConfig)
    int tries = 0;
};
UiMaskTest g_uim;

// ---- v1.0.0ar: 指令 74 —— UI 相机"清屏 / 启用"写-测 ----
// 关键怀疑(2026-09-15): 遮罩=0 的相机**照样会清屏**。ESC 界面态实测 UICamera depth=2、clearFlags=1,
//   它在主相机(depth=0)之后渲染 —— 若它每帧把颜色缓冲擦掉, 那么"严格模式全黑"就不是"世界没渲染",
//   而是**被清屏擦掉了**。写 clearFlags(Depth=3 / Nothing=4) 或 enabled=false 就能把这两条分开:
//   写完之后如果主相机的世界画面露出来 -> 清屏是真凶, 同时也就拿到了"ESC 时无 UI 的世界画面"。
struct UiCamWrite {
    bool pending = false;   // 有写入挂着(到期或手动还原)
    uint64_t until = 0;     // 到期时刻(0 = 持续, 不自动还原)
    bool haveOld = false;
    int oldClear = -1;
    int oldEnabled = -1;
    bool wroteClear = false;
    bool wroteEnabled = false;
    int reap = 0;           // 重申次数
    bool notedNoEnableSet = false;  // "set_enabled 不可用"只提示一次
};
UiCamWrite g_ucw;
// 前置声明(UiCamera 的实现在本文件更靠后的位置)
void* UiCamera();

bool UiCamSetClearFlags(void* cam, int v) {
    if (!g_sys.has_camClearFlagsSet || !cam) return false;
    int32_t x = v;
    void* p[1] = {&x};
    InvokeLogged(g_sys.mi_camClearFlagsSet, cam, p, "Camera.set_clearFlags");
    return true;
}
bool UiCamSetEnabled(void* cam, int v) {
    if (!g_sys.has_camEnabledSet || !cam) return false;
    int32_t x = v ? 1 : 0;  // IL2CPP 的 runtime_invoke 按参数类型拷字节, bool 取低字节 -> 0/1 安全
    void* p[1] = {&x};
    InvokeLogged(g_sys.mi_camEnabledSet, cam, p, "Camera.set_enabled");
    return true;
}

void UiCamWriteRestore(const char* why) {
    void* cam = UiCamera();
    char b[320];
    if (!cam) {
        g_ucw = UiCamWrite{};
        return;
    }
    CamProbe p;
    ProbeCamera(cam, &p);
    if (g_ucw.wroteClear && g_ucw.oldClear >= 0) UiCamSetClearFlags(cam, g_ucw.oldClear);
    if (g_ucw.wroteEnabled && g_ucw.oldEnabled >= 0) UiCamSetEnabled(cam, g_ucw.oldEnabled);
    CamProbe q;
    ProbeCamera(cam, &q);
    snprintf(b, sizeof(b),
             "[ui] 相机状态测试还原(%s): clearFlags %d -> %d, enabled %d -> %d (重申 %d 次)",
             why, p.clear, q.clear, p.enabled, q.enabled, g_ucw.reap);
    LogG(b);
    g_ucw = UiCamWrite{};
}

// 每帧推进(到期自动还原)
void UiCamWriteTick() {
    if (!g_ucw.pending) return;
    if (g_ucw.until != 0 && GetTickCount64() >= g_ucw.until) UiCamWriteRestore("到期自动还原");
}

// 直接写遮罩: 优先用游戏自己的 _SetUICameraCullingMask(它内部会维护自己的状态),
// 没有再退回 UnityEngine 的 set_cullingMask。
bool WriteCamMask(void* cam, int mask, const char* label) {
    int32_t v = mask;
    char h[160];
    if (g_sys.has_setUiCamMask) {
        void* cm = CameraManagerInstance();
        if (cm) {
            void* params[1] = {&v};
            InvokeLogged(g_sys.mi_setUiCamMask, cm, params, "_SetUICameraCullingMask");
            snprintf(h, sizeof(h), "[ui]   %s -> _SetUICameraCullingMask(0x%08X)", label,
                     static_cast<unsigned>(mask));
            LogG(h);
            return true;
        }
    }
    if (!cam || !g_sys.has_camMaskSet) return false;
    void* params[1] = {&v};
    InvokeLogged(g_sys.mi_camMaskSet, cam, params, "Camera.set_cullingMask");
    snprintf(h, sizeof(h), "[ui]   %s -> Camera.set_cullingMask(0x%08X)", label,
             static_cast<unsigned>(mask));
    LogG(h);
    return true;
}

// 读相机遮罩。**注意: 遮罩是位掩码, 带位 31 的合法值(例如 MainCamera 的 0xAFFFDBDF)
// 作为有符号 int 是负数 —— 所以绝不能用 `< 0` 当"读取失败"的判据**(1.0.0am 就栽在这里:
// 世界清位测试因此直接拒绝执行)。失败与否一律看 g_maskOk。
bool g_maskOk = false;

int MaskOf(void* cam) {
    g_maskOk = false;
    if (!cam || !g_sys.has_camMaskGet) return -1;
    int v = -1;
    if (!ReadInt32(g_sys.mi_camMaskGet, cam, &v, "Camera.get_cullingMask")) return -1;
    g_maskOk = true;
    return v;
}

// 游戏自己的 UI 相机(体检实测: 名 UICamera, 遮罩 0x20=只渲染层5, depth=2)
void* UiCamera() {
    if (!g_sys.has_getUiCamera) return nullptr;
    void* cm = CameraManagerInstance();
    if (!cm) return nullptr;
    return InvokeLogged(g_sys.mi_getUiCamera, cm, nullptr, "CameraManager.get_uiCamera");
}

bool UiCfgChannelReady() {
    const Il2CppApi& a = Il2Cpp();
    return g_sys.has_addUiCamCfg && g_sys.has_rmUiCamCfg && g_sys.has_updUiCamCfg &&
           a.string_new != nullptr;
}

// 往游戏的"命名配置栈"里加/删一条(原生 X 隐藏 UI 走的就是这条), 然后让游戏自己应用
bool UiCfgAdd(const char* name, int mask) {
    if (!UiCfgChannelReady()) return false;
    const Il2CppApi& a = Il2Cpp();
    void* s = a.string_new(name);
    if (!s) return false;
    int32_t m = mask;
    void* params[2] = {s, &m};
    void* cm = CameraManagerInstance();
    if (!cm) return false;
    void* r = InvokeLogged(g_sys.mi_addUiCamCfg, cm, params, "AddUICamCullingMaskConfig");
    int ok = -1;
    if (r && a.object_unbox) {
        void* raw = a.object_unbox(r);
        if (raw) ok = (*static_cast<uint8_t*>(raw)) ? 1 : 0;
    }
    {
        char b[256];
        snprintf(b, sizeof(b), "[ui]   AddUICamCullingMaskConfig(\"%s\", 0x%08X) -> %s",
                 name, static_cast<unsigned>(mask), ok < 0 ? "无返回" : (ok ? "true" : "false"));
        LogG(b);
    }
    InvokeLogged(g_sys.mi_updUiCamCfg, cm, nullptr, "_UpdateUICamCullingMask");
    return ok != 0;
}

bool UiCfgRemove(const char* name) {
    if (!UiCfgChannelReady()) return false;
    const Il2CppApi& a = Il2Cpp();
    void* s = a.string_new(name);
    if (!s) return false;
    void* params[1] = {s};
    void* cm = CameraManagerInstance();
    if (!cm) return false;
    void* r = InvokeLogged(g_sys.mi_rmUiCamCfg, cm, params, "RemoveUICamCullingMaskConfig");
    int ok = -1;
    if (r && a.object_unbox) {
        void* raw = a.object_unbox(r);
        if (raw) ok = (*static_cast<uint8_t*>(raw)) ? 1 : 0;
    }
    {
        char b[224];
        snprintf(b, sizeof(b), "[ui]   RemoveUICamCullingMaskConfig(\"%s\") -> %s", name,
                 ok < 0 ? "无返回" : (ok ? "true" : "false"));
        LogG(b);
    }
    InvokeLogged(g_sys.mi_updUiCamCfg, cm, nullptr, "_UpdateUICamCullingMask");
    return ok != 0;
}

bool PickTargetCamera(int target, void** out, std::string* desc) {
    *out = nullptr;
    void* main = MainCamera();
    char b[640];  // 提示行含中文(UTF-8 3 字节/字) + 相机名, 给足余量避免截断
    if (target == 1) {
        if (!main) { LogG("[ui] 目标=1(Camera.main) 但 get_main 不可用"); return false; }
        *out = main;
        *desc = "Camera.main(对照实验: 会整屏黑, 谨慎)";
        return true;
    }
    std::vector<void*> cams;
    const int n = CollectAllCameras(cams, 32);

    // target 0/2 的首选通道: 游戏自己的 get_uiCamera()
    //   —— 本 build 没导出 il2cpp_array_length(实测 allCameras 枚举不可用), 所以这条是主路径。
    void* ui = UiCamera();
    if (ui) {
        CamProbe p;
        ProbeCamera(ui, &p);
        const bool sameAsMain = (ui == main) ||
                                (!p.name.empty() && p.name == ReadStr(g_sys.mi_camNameGet, main, "Camera.get_name"));
        if (sameAsMain) {
            LogG("[ui] get_uiCamera() 返回的就是 Camera.main -> 不作为 UI 相机目标");
        } else if (target == 2) {
            *out = ui;
            *desc = std::string("get_uiCamera()(非 main): ") + CamLine(p);
            return true;
        } else {
            *out = ui;
            *desc = std::string("get_uiCamera(): ") + CamLine(p);
            return true;
        }
    }

    if (n < 0) {
        if (target == 2) { LogG("[ui] 需要相机枚举(allCameras)但不可用"); return false; }
        snprintf(b, sizeof(b),
                 "[ui] 找不到 UI 相机: get_uiCamera 未解析/返回 null, 且 allCameras 枚举不可用(%d) "
                 "-> 拒绝执行, 不做任何写入", n);
        LogG(b);
        return false;
    }
    if (target == 2) {
        for (void* c : cams) {
            if (c == main) continue;
            *out = c;
            *desc = "非 main 的第一台相机";
            return true;
        }
        LogG("[ui] 目标=2 但除 Camera.main 外没有别的相机");
        return false;
    }
    // target == 0 兜底: 枚举里按"名称/类名像 UI"挑
    // 注意: 同一个原生相机可能对应不同的托管包装对象, 所以除了指针相等, 还要比名字。
    std::string mainName;
    if (main) mainName = ReadStr(g_sys.mi_camNameGet, main, "Camera.get_name");
    for (void* c : cams) {
        if (c == main) continue;
        CamProbe p;
        ProbeCamera(c, &p);
        if (!mainName.empty() && p.name == mainName) continue;  // 与 main 同名 = 同一台
        const bool isUi = ContainsCI(p.name, "ui") || ContainsCI(p.name, "hud") ||
                          ContainsCI(p.cls, "ui") || ContainsCI(p.cls, "canvas");
        if (!isUi) continue;
        *out = c;
        snprintf(b, sizeof(b), "枚举识别为 UI 相机(共 %d 台, main='%s'): %s", n,
                 mainName.c_str(), CamLine(p).c_str());
        *desc = b;
        return true;
    }
    snprintf(b, sizeof(b),
             "[ui] 自动识别失败: 枚举到 %d 台相机(main='%s'), 没有一台名称/类名像 UI 相机 "
             "(说明 HUD 很可能由 Camera.main 以 ScreenSpaceOverlay 直接绘制) -> 拒绝执行, "
             "不做任何写入", n, mainName.c_str());
    LogG(b);
    return false;
}

// 还原: 重新取一次相机(不跨帧缓存托管指针 —— 包装对象可能被 GC)再恢复原遮罩。
// 先用过的通道要先撤销, 再按需补一次直接写(双保险: 不许把 UI 留在隐藏态)。
void UiMaskRestore(const char* why) {
    void* cam = nullptr;
    std::string desc;
    if (!PickTargetCamera(g_uim.target, &cam, &desc)) {
        ++g_uim.tries;
        char b[224];
        snprintf(b, sizeof(b), "[ui] 还原受阻(%s): 找不到目标相机, 第 %d 次重试(每帧重试)",
                 why, g_uim.tries);
        LogG(b);
        if (g_uim.tries > 600) {  // ~10 秒
            LogG("[ui] !! 还原连续失败超过 10 秒: 请手动切一次场景/进出拍照模式让游戏重配遮罩");
            g_uim.pending = false;
            g_uim.chMode = 0;
        }
        return;
    }
    const int now = MaskOf(cam);
    if (g_uim.chMode == 1) UiCfgRemove("ECLHideUI");
    int after = MaskOf(cam);
    if (after != g_uim.oldMask) {
        WriteCamMask(cam, g_uim.oldMask, "还原: 直接写回原遮罩");
        after = MaskOf(cam);
    }
    char b[768];
    snprintf(b, sizeof(b),
             "[ui] 还原(%s): 目标=%s 还原前=0x%08X 写回=0x%08X 还原后=0x%08X %s",
             why, desc.c_str(), static_cast<unsigned>(now),
             static_cast<unsigned>(g_uim.oldMask), static_cast<unsigned>(after),
             (after == g_uim.oldMask) ? "OK" : "!! 不一致(游戏可能每帧覆写)");
    LogG(b);
    g_uim.pending = false;
    g_uim.chMode = 0;
    g_uim.tries = 0;
    g_uim.midLogged = false;
}

// ---- v1.0.0am: "隐藏游戏 UI"的持续状态(结构体必须放在这里: 下面的 tick 要用) ----
// 隐藏/重申/放行的判定都基于它; worldClearBits = 隐藏时要从主相机遮罩里清掉的层(0 = 不动)。
struct UiHide {
    bool active = false;
    // v1.0.0ar: 隐藏时是否**禁用 UI 相机**(默认开, ini ui_hide_disable_uicam)。
    // 实测定案(2026-09-15): 遮罩=0 的相机**照样清屏** —— ESC 界面态下 UICamera(depth 2, 在主相机之后)
    //   每帧把主相机画好的世界擦黑; 只写 clearFlags=Depth 无效(本作自定义渲染管线不采纳 clearFlags),
    //   而 enabled=false 立刻见效: 世界画面(时停/静止)露出来。
    bool disableUiCam = true;
    bool uiCamDisabled = false;   // 我们是否已经把它禁用了
    int uiCamWasEnabled = -1;     // 原值(还原用)
    int uiCamReapplies = 0;       // 重申"禁用"次数
    bool strict = true;  // v1.0.0am: 默认严格保持(用户选定"全藏, 连菜单也不显示"); ini 可改
    // v1.0.0ar 第三档策略(指令 66 a0=2): **只清"常规 UI 位", 保留游戏额外加的位**。
    // 由来(2026-09-15 实测): 按 ESC(时停)时, 游戏把主相机遮罩写成 0(世界相机不画), 同时把
    //   UI 相机遮罩从常规 0x20 扩到 0x420 = 位5(UI, 菜单) + 位10(UIPP, **那幅冻结/模糊的世界画面**)。
    //   用户确认"原生 ESC 界面背景就是游戏世界画面" → 也就是说那一刻屏幕上的世界是 UI 相机画的。
    //   于是"藏菜单、留世界"= UI 相机遮罩 = 当前值 & ~常规值 = 0x400 ✓(全藏成 0 就是黑屏)。
    bool keepExtra = false;
    bool released = false;
    int normalMask = -1;
    int reapplies = 0;
    int releases = 0;
    int worldClearBits = 0;
    int worldReapplies = 0;
    int mainNormalMask = -1;
};
UiHide g_uih;

// ---- v1.0.0am: 主(世界)相机清位的"一次性测试"状态 + 前置声明 ----
struct UiWorldTest {
    bool pending = false;
    uint64_t until = 0;
    int bits = 0;
};
UiWorldTest g_uiwt;
int MainMaskOf();
bool WorldCfgApply(int clearBits);
bool WorldCfgRemove();

void UiMaskTick() {
    UiCamWriteTick();  // v1.0.0ar: 指令 74 的"清屏/启用"写-测到期还原
    // v1.0.0am: 主(世界)相机清位的"一次性测试"到期还原(每帧检查)
    if (g_uiwt.pending && GetTickCount64() >= g_uiwt.until) {
        g_uiwt.pending = false;
        WorldCfgRemove();
        int back = MainMaskOf();
        if ((back & g_uiwt.bits) != 0) {  // 撤销条目没还原 -> 直接补写
            void* main = MainCamera();
            if (main && g_sys.has_camMaskSet) {
                int32_t v = back | g_uiwt.bits;
                void* p[1] = {&v};
                InvokeLogged(g_sys.mi_camMaskSet, main, p, "Camera.set_cullingMask(还原)");
                back = MainMaskOf();
            }
        }
        char b[256];
        snprintf(b, sizeof(b),
                 "[ui] 世界清位测试到期还原: 清掉过 0x%08X, 主相机现在=0x%08X %s",
                 static_cast<unsigned>(g_uiwt.bits), static_cast<unsigned>(back),
                 (back & g_uiwt.bits) == g_uiwt.bits ? "OK(位已恢复)" : "!! 仍未恢复");
        LogG(b);
        GameSysUiReportState();
        // 若还挂着"持续清位", 把它重新施加回来
        if (g_uih.worldClearBits != 0) WorldCfgApply(g_uih.worldClearBits);
    }
    if (!g_uim.pending) return;
    const uint64_t now = GetTickCount64();
    if (!g_uim.midLogged && now >= g_uim.midAt) {
        g_uim.midLogged = true;  // 保持中读回一次: 判断"游戏会不会自己把遮罩改回去"
        void* cam = nullptr;
        std::string desc;
        if (PickTargetCamera(g_uim.target, &cam, &desc)) {
            const int m = MaskOf(cam);
            char b[320];
            // v1.0.0ar 修正: 判据是"是否等于我们要求的值"(原来写死 m == 0, 于是写 0x400 这类
            // 非零目标时也会误报"已被游戏改回")。
            snprintf(b, sizeof(b), "[ui] 保持中读回: 遮罩=0x%08X %s (目标 0x%08X, 通道=%s)",
                     static_cast<unsigned>(m),
                     (m == g_uim.want) ? "写入粘住了"
                                       : "!! 已被游戏改回 -> 该通道不粘, 需要每帧重写或用配置栈",
                     static_cast<unsigned>(g_uim.want),
                     g_uim.chMode == 1 ? "游戏配置栈" : "直接写");
            LogG(b);
        }
    }
    if (now < g_uim.until) return;
    UiMaskRestore("到期自动还原");
}

// ---- v1.0.0am: 主(世界)相机遮罩通道 + 图层名表(全部是"先读再写", 带读回校验) ----

int MainMaskOf() {
    void* main = MainCamera();
    if (!main) return -1;
    return MaskOf(main);
}

int GameSysUiMainDefaultMask() {
    int v = -1;
    if (g_sys.has_mainCamDefaultMask) {
        if (!ReadInt32(g_sys.mi_mainCamDefaultMask, CameraManagerInstance(), &v,
                       "GetMainCamDefaultCullingMask")) {
            v = -1;
        }
    }
    if (v < 0) {  // 该 getter 不可用/读失败时才退回"当前遮罩"
        const int cur = MainMaskOf();
        if (g_maskOk) return cur;
    }
    return v;
}

int GameSysUiUiDefaultMask() {
    int v = -1;
    if (g_sys.has_uiCamDefaultMask) {
        ReadInt32(g_sys.mi_uiCamDefaultMask, CameraManagerInstance(), &v,
                  "GetUICamDefaultCullingMask");
    }
    return v;
}

bool MainCfgReady() {
    const Il2CppApi& a = Il2Cpp();
    return g_sys.has_addMainCamCfg && g_sys.has_rmMainCamCfg && g_sys.has_updMainCamCfg &&
           a.string_new != nullptr;
}

// 把 clearBits 从主相机遮罩里清掉(优先游戏配置栈, 兜底直接写)。返回是否已达成。
bool WorldCfgApply(int clearBits) {
    if (clearBits == 0) return true;
    const int cur = MainMaskOf();
    if (!g_maskOk) {
        LogG("[ui] 世界清位: 读不到主相机遮罩 -> 放弃写入");
        return false;
    }
    if ((cur & clearBits) == 0) return true;  // 已经清干净
    const int32_t want = cur & ~clearBits;
    if (MainCfgReady()) {
        const Il2CppApi& a = Il2Cpp();
        void* s = a.string_new("ECLHideWorldUI");
        void* cm = CameraManagerInstance();
        if (s && cm) {
            void* params[2] = {s, const_cast<int32_t*>(&want)};
            InvokeLogged(g_sys.mi_addMainCamCfg, cm, params, "AddMainCamCullingMaskConfig");
            InvokeLogged(g_sys.mi_updMainCamCfg, cm, nullptr, "_UpdateMainCamCullingMask");
            if ((MainMaskOf() & clearBits) == 0) return true;
        }
    }
    void* cm = CameraManagerInstance();
    if (g_sys.has_setMainCamMask && cm) {
        int32_t v = want;
        void* p[1] = {&v};
        InvokeLogged(g_sys.mi_setMainCamMask, cm, p, "_SetMainCameraCullingMask");
    }
    void* main = MainCamera();
    if (main && g_sys.has_camMaskSet) {
        int32_t v = want;
        void* p[1] = {&v};
        InvokeLogged(g_sys.mi_camMaskSet, main, p, "Camera.set_cullingMask(main)");
    }
    return (MainMaskOf() & clearBits) == 0;
}

// ---- v1.0.0ar: 主(世界)相机"保活" —— 不允许游戏把它彻底关掉 ----
// 实测背景(2026-09-15, 用户按 ESC 黑屏, 隔离实验定案):
//   游戏进入"时停/界面"状态时会 _SetMainCameraCullingMask(0) —— 世界**完全不画**,
//   整幅画面改由 UI 相机产出(同一时刻它的遮罩从常规 0x20 扩到 0x420 = 位5 UI + 位10 UIPP)。
//   所以"把 UI 相机压到 0"就等于把屏幕唯一的产出源也按掉了 → 全黑(不是我们写坏了遮罩:
//   层位清位只会写 cur & ~clearBits(非零), 且撤销我们的配置条目后主相机仍然是 0 = 游戏自有值)。
// 做法: 帧末(渲染之前最后一个时机)检查主相机遮罩; 世界被游戏关掉时, 按回我们期望的遮罩
//   (= 游戏自报默认遮罩 & ~持续清位)。**绝不写 0**(那就是黑屏), 目标推导不出来就什么都不做(失败要开)。
// 用途: 藏 UI 相机 + 保住世界相机 = "ESC 时也能看到时停的正常世界画面, 且没有任何 UI"。
// **闸门(用户 2026-09-15 明确要求): 这条链路只服务于"隐藏 UI"** —— 只有勾选了隐藏 UI
//   (g_uih.active) 时才会触发; 没勾选时一次都不写主相机(那时主相机完全交给游戏, 包括它按 ESC 关掉)。
struct WorldKeep {
    bool on = false;       // 持久开关(ini ui_hide_world_keepalive / 指令 73 a0=1)
    bool pending = false;  // 一次性测试在跑(指令 73 a1=秒, 到期自动还原)
    int mode = 0;          // 0 = 只在"主相机被彻底关掉(0)"时按回(保守; ESC 实测正是这种)
                           // 1 = 只要缺世界位就按回(激进: 能覆盖"只关掉一部分"的变体)
    int maskOverride = 0;  // 指令 73 a2: 非 0 = 直接拿这个当目标(诊断用), 0 = 自动推导
    int reapplies = 0;     // 按回次数(每次"隐藏 UI"会话开始时清零)
    int fails = 0;         // 按回失败/目标不可用次数
    bool wrote = false;    // 本次会话是否真的写过主相机(隐藏 UI 结束时据此交还游戏重算)
    bool noHideNoted = false;  // "没隐藏 UI 所以不触发"只提示一次, 免得刷屏
    uint64_t until = 0;
};
WorldKeep g_wk;
// 兜底常量: 游戏自报默认遮罩读不到、且还没有缓存值时用的实测值(见 1.0.0am 体检)
const int kMainWorldMaskFallback = 0xAFFFDBDF;

// 期望的世界遮罩。返回 **0 = 推导不出来**(0 永远不是合法目标: 写 0 就是黑屏)。
// **注意符号**: 0xAFFFDBDF 当 int32 是**负数**(位 31 也在用), 所以这里**只能拿 0 当"无效"哨兵**,
// 一律不许写 `> 0` / `<= 0` / `-1` —— 这正是 v1.0.0ar 第一版的实际 bug(保活因此一次都没动手)。
int WorldKeepTarget() {
    int def = 0;
    if (g_wk.maskOverride != 0) {
        def = g_wk.maskOverride;
    } else {
        def = GameSysUiMainDefaultMask();  // GetMainCamDefaultCullingMask(); ESC 界面态下它自报 0
        if (def == 0) {
            def = (g_uih.mainNormalMask != 0) ? g_uih.mainNormalMask  // 勾选隐藏 UI 那一刻的主相机遮罩
                                              : kMainWorldMaskFallback;
        }
    }
    if (def == 0) return 0;
    return def & ~g_uih.worldClearBits;  // 藏 UI 时连"持续清位"一起保持; 结果为 0 也算推导不出来
}

// 把主相机遮罩强写成 want(读回校验; 成功判据 = want 的位**都**在, 游戏自己多给的层不干涉)。
// 只用"直接写"这条通道: 每帧可能都要按回, 而配置栈通道每次都要 string_new(托管分配), 不适合逐帧。
bool WriteMainMaskForce(int want) {
    if (want == 0) return false;
    void* cm = CameraManagerInstance();
    if (g_sys.has_setMainCamMask && cm) {
        int32_t v = want;
        void* p[1] = {&v};
        InvokeLogged(g_sys.mi_setMainCamMask, cm, p, "保活: _SetMainCameraCullingMask");
    }
    int after = MainMaskOf();
    if (g_maskOk && (after & want) == want) return true;
    void* main = MainCamera();
    if (main && g_sys.has_camMaskSet) {
        int32_t v = want;
        void* p[1] = {&v};
        InvokeLogged(g_sys.mi_camMaskSet, main, p, "保活: Camera.set_cullingMask(main)");
        after = MainMaskOf();
        if (g_maskOk && (after & want) == want) return true;
    }
    return false;
}

// 还原: 让游戏自己按它的配置栈重算(不写死值, 免得跟游戏状态打架)
void WorldKeepRestoreGame(const char* why) {
    const int before = MainMaskOf();
    void* cm = CameraManagerInstance();
    if (g_sys.has_updMainCamCfg && cm) {
        InvokeLogged(g_sys.mi_updMainCamCfg, cm, nullptr, "_UpdateMainCamCullingMask(保活还原)");
    }
    char b[256];
    snprintf(b, sizeof(b), "[ui] 世界保活还原(%s): 让游戏自己重算主相机遮罩 0x%08X -> 0x%08X",
             why, static_cast<unsigned>(before), static_cast<unsigned>(MainMaskOf()));
    LogG(b);
}

// 注意: GameSysUiWorldKeepSet / GameSysUiWorldKeepTick 这两个"对外入口"定义在匿名命名空间**之外**
// (见文件末尾 指令 73 段), 否则它们会拿内部链接, link.cpp 那边链不到。

// 撤销我们的主相机配置条目, 让游戏自己重算(不留残渣)
bool WorldCfgRemove() {
    if (!MainCfgReady()) return false;
    const Il2CppApi& a = Il2Cpp();
    void* s = a.string_new("ECLHideWorldUI");
    if (!s) return false;
    void* cm = CameraManagerInstance();
    if (!cm) return false;
    void* params[1] = {s};
    InvokeLogged(g_sys.mi_rmMainCamCfg, cm, params, "RemoveMainCamCullingMaskConfig");
    InvokeLogged(g_sys.mi_updMainCamCfg, cm, nullptr, "_UpdateMainCamCullingMask");
    return true;
}

// 带 1 个 int 参数的托管调用
void* InvokeLogged1Int(void* mi, int arg, const char* label) {
    if (!mi) return nullptr;
    int32_t v = arg;
    void* params[1] = {&v};
    return InvokeLogged(mi, nullptr, params, label);
}

bool ReadInt32Args1(void* mi, int arg, int* out, const char* label) {
    const Il2CppApi& a = Il2Cpp();
    void* boxed = InvokeLogged1Int(mi, arg, label);
    if (!boxed || !a.object_unbox) return false;
    void* raw = a.object_unbox(boxed);
    if (!raw) return false;
    *out = *static_cast<int32_t*>(raw);
    return true;
}

}  // namespace

void GameSysUiAudit() {
    const Il2CppApi& a = Il2Cpp();
    LogG("[ui] ===== 探测1: 隐藏 UI 可行性体检(只读, 不写任何状态) =====");
    {
        char b[320];
        snprintf(b, sizeof(b),
                 "[ui] 能力: get_main=%d allCamerasCount=%d allCameras=%d "
                 "cullingMask读=%d cullingMask写=%d 名称=%d 启用=%d 深度=%d clearFlags=%d "
                 "渲染到纹理=%d | 数组API=%d(string=%d)",
                 g_sys.has_camMainGet, g_sys.has_allCamerasCount, g_sys.has_allCameras,
                 g_sys.has_camMaskGet, g_sys.has_camMaskSet, g_sys.has_camNameGet,
                 g_sys.has_camEnabledGet, g_sys.has_camDepthGet,
                 g_sys.has_camClearFlagsGet, g_sys.has_camTargetTexGet,
                 (a.array_length && a.array_addr) ? 1 : 0,
                 (a.string_length && a.string_chars) ? 1 : 0);
        LogG(b);
    }

    // ---- ① 全量相机清单 ----
    std::vector<void*> cams;
    const int n = CollectAllCameras(cams, 32);
    void* main = MainCamera();
    {
        char b[192];
        snprintf(b, sizeof(b), "[ui] ① UnityEngine.Camera.allCameras: 枚举结果=%d 台",
                 n < 0 ? 0 : n);
        LogG(b);
        if (n == -1) LogG("[ui]    (数组 API 缺失: il2cpp_array_length / il2cpp_array_addr_with_size 未导出)");
        if (n == -2) LogG("[ui]    (get_allCameras 调用失败)");
    }
    for (size_t i = 0; i < cams.size(); ++i) {
        CamProbe p;
        ProbeCamera(cams[i], &p);
        char b[256];
        snprintf(b, sizeof(b), "[ui]    相机[%zu] %s %s", i, CamLine(p).c_str(),
                 (cams[i] == main) ? "<== 这就是 Camera.main" : "");
        LogG(b);
    }
    if (cams.empty()) {
        LogG("[ui]    没有枚举到相机(或枚举不可用) —— 不影响下面的 getter 候选探测");
    }

    // ---- ② UI 相关成员签名(只读元数据) ----
    LogG("[ui] ② CameraManager / CameraUtils 里 UI·图层·遮罩 相关成员的完整签名:");
    std::vector<void*> getters;
    std::vector<void*> intGetters;
    int budget = 260;
    DumpUiMembers("Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraManager",
                  &getters, &intGetters, &budget);
    DumpUiMembers("Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraUtils",
                  &getters, &intGetters, &budget);
    if (budget <= 0) LogG("[ui]    (签名打印已到上限 260 行)");

    // ---- ③ 0 参 -> Camera 的 getter 候选, 逐个调用 ----
    LogG("[ui] ③ CameraManager/CameraUtils 中 '0 参 -> UnityEngine.Camera' 的 getter 候选:");
    void* cm = CameraManagerInstance();
    void* ownerCm = ResolveClass("Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraManager");
    for (size_t i = 0; i < getters.size(); ++i) {
        void* m = getters[i];
        std::string sig = MethodSig(m);
        // 归属判定: 该方法是否出现在 CameraManager 的方法表里(在 = 实例方法, 不在 = 视作静态)
        bool fromCm = false;
        {
            void* it = nullptr;
            void* mm = nullptr;
            int c = 0;
            while (ownerCm &&
                   (mm = a.class_get_methods(ownerCm, &it)) != nullptr && c < 6000) {
                ++c;
                if (mm == m) { fromCm = true; break; }
            }
        }
        if (fromCm && !cm) {
            char b[384];
            snprintf(b, sizeof(b),
                     "[ui]    getter %s (CameraManager 实例方法) —— 实例未就绪, 跳过调用",
                     sig.c_str());
            LogG(b);
            continue;
        }
        void* obj = fromCm ? cm : nullptr;  // CameraUtils 侧是静态, 按约定传 null this
        {
            char b[384];
            snprintf(b, sizeof(b), "[ui]    getter %s (%s)", sig.c_str(),
                     fromCm ? "CameraManager 实例" : "静态(null this)");
            LogG(b);
        }
        void* cam = InvokeLogged(m, obj, nullptr, "UI相机候选 getter");
        if (!cam) {
            LogG("[ui]      -> 返回 null(当前没有这类相机 / 需要别的实例)");
            continue;
        }
        CamProbe p;
        ProbeCamera(cam, &p);
        LogG(std::string("[ui]      -> ") + CamLine(p) +
             (cam == main ? " <== 就是 Camera.main" : ""));
    }
    if (getters.empty()) LogG("[ui]    (没有解析到任何 '0 参 -> Camera' 的 getter)");

    // ---- ④ 0 参 -> int 的"层/遮罩"getter, 逐个调用(直接给出 UI 图层号) ----
    LogG("[ui] ④ CameraManager/CameraUtils 中 '0 参 -> int' 且名字像 层/遮罩 的 getter 候选:");
    if (intGetters.empty()) LogG("[ui]    (没有这类 getter)");
    for (size_t i = 0; i < intGetters.size(); ++i) {
        void* m = intGetters[i];
        const std::string sig = MethodSig(m);
        bool fromCm = false;
        {
            void* it = nullptr;
            void* mm = nullptr;
            int c = 0;
            while (ownerCm &&
                   (mm = a.class_get_methods(ownerCm, &it)) != nullptr && c < 6000) {
                ++c;
                if (mm == m) { fromCm = true; break; }
            }
        }
        if (fromCm && !cm) {
            LogG("[ui]    int-getter " + sig + " (CameraManager 实例方法) —— 实例未就绪, 跳过");
            continue;
        }
        void* obj = fromCm ? cm : nullptr;
        int v = -1;
        if (!ReadInt32(m, obj, &v, "UI层/遮罩 int getter")) {
            LogG("[ui]    int-getter " + sig + " -> 调用失败/无返回值");
            continue;
        }
        char b[512];
        snprintf(b, sizeof(b), "[ui]    int-getter %s (%s) -> %d (0x%08X)",
                 sig.c_str(), fromCm ? "CameraManager 实例" : "静态", v,
                 static_cast<unsigned>(v));
        LogG(b);
    }

    LogG("[ui] ===== 体检结束。若此处看不到 UI 相机, 请发指令 63 导出 UI 符号清单精确定位 =====");
}

bool GameSysUiTryCullMask(int mode, int target, float holdSeconds, int maskOverride) {
    if (!g_sys.has_camMaskGet || !g_sys.has_camMaskSet) {
        LogG("[ui] 指令65 不可用: Camera.get/set_cullingMask 未解析");
        return false;
    }
    GameSysResolve();  // 幂等: 保证 CameraManager 实例/Camera 侧接口都已解析
    if (mode == 0) {  // 还原
        if (!g_uim.pending) {
            LogG("[ui] 还原请求: 当前没有待还原的隐藏动作(没有任何写入被挂起, 不做任何事)");
            return false;
        }
        g_uim.until = 0;
        UiMaskRestore("手动还原");
        return true;
    }
    if (g_uim.pending) {
        LogG("[ui] 已有挂起的隐藏动作, 先还原再执行新的写入");
        g_uim.until = 0;
        UiMaskRestore("重入前还原");
    }
    void* cam = nullptr;
    std::string desc;
    if (!PickTargetCamera(target, &cam, &desc)) return false;

    CamProbe before;
    ProbeCamera(cam, &before);
    LogG("[ui] 写入前: " + CamLine(before));
    if (!before.hasMask) {
        LogG("[ui] 读不到当前遮罩(读回失败) -> 为安全起见放弃本次写入");
        return false;
    }
    g_uim.target = target;
    g_uim.oldMask = before.mask;
    LogG("[ui] 目标相机: " + desc);

    // 游戏自己的判断(只读): "当前相机该不该隐藏 HUD" —— 我们假装它就是原生 X 那套的输出
    if (g_sys.has_needHideHud) {
        int nh = -1;
        if (ReadBool(g_sys.mi_needHideHud, CameraManagerInstance(), &nh,
                     "_CurrCameraNeedHideHUD")) {
            char b[192];
            snprintf(b, sizeof(b), "[ui] 游戏自认为'该不该隐藏 HUD'(_CurrCameraNeedHideHUD)=%d%s",
                     nh, nh ? " (游戏此刻也想藏 HUD)" : " (游戏此刻不想藏)");
            LogG(b);
        }
    }
    if (g_sys.has_uiCamDefaultMask) {
        int dm = -1;
        if (ReadInt32(g_sys.mi_uiCamDefaultMask, CameraManagerInstance(), &dm,
                      "GetUICamDefaultCullingMask")) {
            char b[192];
            snprintf(b, sizeof(b), "[ui] GetUICamDefaultCullingMask()=0x%08X(位=",
                     static_cast<unsigned>(dm));
            std::string s(b);
            bool first = true;
            for (int i = 0; i < 32; ++i) {
                if (!(static_cast<unsigned>(dm) & (1u << i))) continue;
                if (!first) s += ",";
                s += std::to_string(i);
                first = false;
            }
            s += ")";
            LogG(s);
        }
    }
    LogG(std::string("[ui] 通道可用性: 游戏配置栈=") +
         (UiCfgChannelReady() ? "有(Add/RemoveUICamCullingMaskConfig + _Update)"
                              : "无(需要 il2cpp_string_new / 接口未解析)") +
         " | _SetUICameraCullingMask=" + (g_sys.has_setUiCamMask ? "有" : "无"));

    // 掩码默认写 0(该相机彻底不渲染): UI 相机只渲染层 5(实测遮罩 0x20),
    // 而 MainCamera 的遮罩不含位 5 -> 动它碰不到世界, 也不涉及任何输入状态。
    // v1.0.0ar: 允许传任意值(maskOverride) —— 用来验证"ESC 界面里那幅世界画面在哪个位"。
    const int want = maskOverride;
    g_uim.want = want;
    g_uim.chMode = 0;

    // 通道 1(首选): 游戏自己的"命名配置栈" —— 原生 X 隐藏 UI 走的就是这条, 最不容易被覆写
    if (UiCfgChannelReady()) {
        char b0[192];
        snprintf(b0, sizeof(b0), "[ui] 通道1(游戏配置栈): 加条目 ECLHideUI=0x%08X 并让游戏自己应用",
                 static_cast<unsigned>(want));
        LogG(b0);
        UiCfgAdd("ECLHideUI", want);
        const int m1 = MaskOf(cam);
        {
            char b[320];
            snprintf(b, sizeof(b), "[ui]   配置栈通道读回: 0x%08X %s", static_cast<unsigned>(m1),
                     (m1 == want) ? "OK(配置栈通道有效)"
                                  : "!! 没生效(配置栈语义可能不是取交集) -> 改用直接写");
            LogG(b);
        }
        if (m1 == want) g_uim.chMode = 1;
    }

    // 通道 2(兜底): _SetUICameraCullingMask / set_cullingMask 直接写
    if (g_uim.chMode == 0) {
        LogG("[ui] 通道2(直接写): _SetUICameraCullingMask / Camera.set_cullingMask");
        if (!WriteCamMask(cam, want, "隐藏: 写目标遮罩")) {
            LogG("[ui] 写入失败(没有可用的写入接口)");
            return false;
        }
        const int m2 = MaskOf(cam);
        char b[320];
        snprintf(b, sizeof(b), "[ui]   直接写读回: 0x%08X %s", static_cast<unsigned>(m2),
                 (m2 == want) ? "OK(写入生效)"
                              : "!! 立刻就被改回去了 -> 游戏每帧在维护该遮罩");
        LogG(b);
    }

    const int hold = (holdSeconds >= 0.5f && holdSeconds <= 60.f)
                         ? static_cast<int>(holdSeconds + 0.5f) : 3;
    const uint64_t nowMs = GetTickCount64();
    g_uim.pending = true;
    g_uim.tries = 0;
    g_uim.midLogged = false;
    g_uim.until = nowMs + static_cast<uint64_t>(hold) * 1000ull;
    g_uim.midAt = nowMs + static_cast<uint64_t>(hold) * 1000ull / 2;
    {
        char b[768];
        snprintf(b, sizeof(b),
                 "[ui] 已隐藏 UI 相机渲染(遮罩 0x%08X -> 0x%08X, 通道=%s), 将在 %d 秒后自动还原。"
                 "现在请看游戏画面: UI 是否消失? 键位/攻击是否正常?",
                 static_cast<unsigned>(g_uim.oldMask), static_cast<unsigned>(want),
                 g_uim.chMode == 1 ? "游戏配置栈" : "直接写", hold);
        LogG(b);
    }
    return true;
}

bool GameSysUiCullMaskPending() { return g_uim.pending; }

// ---- v1.0.0ar: 指令 74 —— UI 相机"清屏 / 启用"写-测的对外入口 ----
// what: 0 = 还原, 1 = 写 clearFlags, 2 = 写 enabled(false), 3 = 两个都写
// clearValue: clearFlags 目标值(<=0 时取默认 3 = Depth: 只清深度、不擦颜色 -> 主相机画面能留住)
// holdSec: >=1 一次性测试(到期自动还原); <1 持续(直到发 what=0)
// 目的: 把"严格模式全黑"的两种可能分开 —— 世界没被渲染 vs 被 UI 相机的清屏擦掉。
void GameSysUiCamPropSet(int what, float holdSec, int clearValue) {
    GameSysResolve();
    if (what == 0) {
        if (!g_ucw.pending) {
            LogG("[ui] 相机状态测试还原请求: 当前没有挂起的写入(不做任何事)");
            return;
        }
        UiCamWriteRestore("手动还原");
        return;
    }
    void* cam = UiCamera();
    if (!cam) {
        LogG("[ui] 相机状态测试: 读不到 UI 相机 -> 放弃");
        return;
    }
    if (g_ucw.pending) UiCamWriteRestore("重入前还原");
    CamProbe p;
    ProbeCamera(cam, &p);
    LogG("[ui] 相机状态测试(写前): " + CamLine(p));
    g_ucw.oldClear = p.clear;
    g_ucw.oldEnabled = p.enabled;
    g_ucw.haveOld = true;
    const int cv = (clearValue > 0) ? clearValue : 3;  // 3 = Depth
    bool any = false;
    if (what == 1 || what == 3) {
        if (!g_sys.has_camClearFlagsSet) {
            LogG("[ui] 相机状态测试: Camera.set_clearFlags 未解析 -> 写不了 clearFlags");
        } else {
            UiCamSetClearFlags(cam, cv);
            g_ucw.wroteClear = true;
            any = true;
        }
    }
    if (what == 2 || what == 3) {
        if (!g_sys.has_camEnabledSet) {
            LogG("[ui] 相机状态测试: Camera.set_enabled 未解析 -> 写不了 enabled");
        } else {
            UiCamSetEnabled(cam, 0);
            g_ucw.wroteEnabled = true;
            any = true;
        }
    }
    if (!any) return;
    CamProbe q;
    ProbeCamera(cam, &q);
    const int hold = (holdSec >= 1.f)
                         ? ((holdSec > 600.f) ? 600 : static_cast<int>(holdSec + 0.5f))
                         : 0;
    g_ucw.pending = true;
    g_ucw.until = hold ? (GetTickCount64() + static_cast<uint64_t>(hold) * 1000ull) : 0;
    char b[512];
    snprintf(b, sizeof(b),
             "[ui] 相机状态测试: clearFlags %d -> %d, enabled %d -> %d (%s) | %s"
             " —— 请看画面: 主相机渲染的世界画面露出来了吗?",
             p.clear, q.clear, p.enabled, q.enabled,
             hold ? "到期自动还原" : "持续(发 -Id 74 -A0 0 还原)", 
             (q.clear == p.clear && q.enabled == p.enabled) ? "!! 读回没变化(写没生效)" : "写入已生效");
    LogG(b);
}

// ================= v1.0.0ak: 面板"隐藏游戏 UI"的正式通路(共享内存位 6) =================
//
// 与指令 65 的区别: 65 是"一次性测量"(写→读回→自动还原), 这里是"持续状态"(勾选期间一直保持),
// 并且要处理"游戏自己会重建配置栈"这件事:
//   实测(1.0.0aj 第二次测试): 用户按 ESC 呼出界面时, 遮罩从 0 变成 0x00000420(位5+位10)
//   —— 游戏重建了整个 uiCameraCullingMaskConfigs 栈, 我们那条 ECLHideUI 被冲掉。
// 策略(用户选定: 尊重游戏, ESC 菜单可见):
//   · 遮罩 = 0            → 我们仍处于隐藏态, 什么都不做;
//   · 遮罩 = 常规值       → 游戏回到了常规 HUD 状态(界面关了/栈被重建) → **重新隐藏**;
//   · 遮罩 含常规值以外的位 → 游戏主动显示了额外图层(呼出界面) → **放行**, 只记一次日志;
//   严格模式(指令 66 a0=1)则无论什么值都压回 0。
// 失败要开: 断流 >500ms 立刻放行 —— 绝不把 UI 永久藏起来。
// v1.0.0ak/am: "隐藏游戏 UI"的持续状态(定义见上方匿名命名空间 —— 需要被 tick 先看到)
// (原结构体与变量已上移到匿名命名空间)

// ---- v1.0.0ar: 隐藏 UI 时"禁用 UI 相机"的执行体 ----
// 由来(2026-09-15 实测定案): 隐藏 UI 只把 UICamera 遮罩压成 0 是不够的 —— **遮罩=0 的相机照样清屏**。
//   按 ESC 时游戏把画面交给 UICamera(depth=2, 在主相机之后)渲染, 它每帧把主相机刚画好的世界擦成黑的,
//   造成"世界没渲染"的假象(实际主相机一直在画, 用指令 73 保活可证)。对照实验:
//     · 写 clearFlags=3(Depth) -> 仍然全黑(本作是自定义渲染管线, clearFlags 不被采纳)
//     · 写 enabled=false       -> 世界画面(时停/静止)立刻露出来 ✓
// 所以隐藏时把 UI 相机整体禁用(既不画也不擦), 还原时写回原值。
bool UiCamHideDisable(const char* why) {
    if (!g_uih.disableUiCam) return false;
    if (!g_sys.has_camEnabledSet) {
        if (!g_ucw.notedNoEnableSet) {
            g_ucw.notedNoEnableSet = true;
            LogG("[ui] 禁用 UI 相机: Camera.set_enabled 未解析 -> 这一档不可用(只能压遮罩)");
        }
        return false;
    }
    void* cam = UiCamera();
    if (!cam) return false;
    int en = -1;
    if (!ReadBool(g_sys.mi_camEnabledGet, cam, &en, "Camera.get_enabled")) return false;
    if (en == 0) {  // 已经关着(游戏自己关的也算达成)
        g_uih.uiCamDisabled = true;
        return true;
    }
    if (g_uih.uiCamWasEnabled < 0) g_uih.uiCamWasEnabled = en;
    UiCamSetEnabled(cam, 0);
    int after = -1;
    ReadBool(g_sys.mi_camEnabledGet, cam, &after, "Camera.get_enabled");
    g_uih.uiCamDisabled = true;
    char b[320];
    snprintf(b, sizeof(b),
             "[ui] 已禁用 UI 相机(%s): enabled %d -> %d%s —— 遮罩=0 的相机照样清屏, 不关掉它"
             "主相机画好的世界会被擦黑",
             why, en, after, (after == 0) ? "" : "(!! 立刻被改回)");
    LogG(b);
    return after == 0;
}

bool UiCamHideRestore(const char* why) {
    if (!g_uih.uiCamDisabled) return false;
    g_uih.uiCamDisabled = false;
    void* cam = UiCamera();
    if (!cam || !g_sys.has_camEnabledSet) return false;
    const int want = (g_uih.uiCamWasEnabled >= 0) ? g_uih.uiCamWasEnabled : 1;
    int en = -1;
    ReadBool(g_sys.mi_camEnabledGet, cam, &en, "Camera.get_enabled");
    if (en != want) UiCamSetEnabled(cam, want ? 1 : 0);
    int after = -1;
    ReadBool(g_sys.mi_camEnabledGet, cam, &after, "Camera.get_enabled");
    char b[256];
    snprintf(b, sizeof(b), "[ui] UI 相机已还原(%s): enabled %d -> %d (原值 %d)",
             why, en, after, want);
    LogG(b);
    g_uih.uiCamWasEnabled = -1;
    return true;
}

void GameSysUiSetDisableUiCam(int on) {
    g_uih.disableUiCam = (on != 0);
    LogG(g_uih.disableUiCam
             ? "[ui] ini: 隐藏 UI 时禁用 UI 相机 = 开(遮罩=0 的相机照样清屏 -> 必须一并关掉)"
             : "[ui] ini: 隐藏 UI 时禁用 UI 相机 = 关(只压遮罩; ESC 时会看到全黑)");
    if (!g_uih.disableUiCam) UiCamHideRestore("开关关闭");
}

void GameSysUiSetStrict(int policy) {
    // v1.0.0ar: 参数从"0/1"扩充为三档 —— 0=尊重游戏, 1=严格保持(全藏), 2=保留额外位(藏菜单留世界)
    if (policy == 2) {
        g_uih.strict = false;
        g_uih.keepExtra = true;
    } else {
        g_uih.strict = (policy != 0);
        g_uih.keepExtra = false;
    }
    char b[224];
    snprintf(b, sizeof(b), "[ui] 隐藏策略改为: %s",
             g_uih.keepExtra
                 ? "保留额外位(只藏常规 UI 位, 游戏额外加的层照常渲染 —— ESC 时留下那幅世界画面)"
                 : (g_uih.strict ? "严格保持(连 ESC 菜单也藏)"
                                 : "尊重游戏(游戏呼出界面时放行, 回到常规后自动重新隐藏)"));
    LogG(b);
}

bool GameSysUiHideBegin(const char* why) {
    if (!g_sys.has_getUiCamera || !g_sys.has_camMaskGet) {
        LogG("[ui] 隐藏失败: UI 相机接口未解析(get_uiCamera / get_cullingMask)");
        return false;
    }
    void* cam = UiCamera();
    if (!cam) {
        LogG("[ui] 隐藏失败: get_uiCamera() 返回 null");
        return false;
    }
    const int now = MaskOf(cam);
    if (!g_maskOk) {
        LogG("[ui] 隐藏失败: 读不到 UICamera 当前遮罩(这是真的读失败, 不是负数遮罩)");
        return false;
    }
    if (g_uih.active && now == 0) {
        LogG("[ui] 隐藏请求: 已经处于隐藏态, 不重复写入");
        return true;
    }
    // 用户勾选那一刻的遮罩 = 本局的"常规值"(用于判断游戏是否回到了常规状态)
    g_uih.normalMask = now;
    g_uih.active = true;
    g_uih.released = false;
    g_uih.reapplies = 0;
    g_uih.releases = 0;
    // v1.0.0ar: 保活只服务于"隐藏 UI", 每开一次新会话就把它的计数清零(状态行里的"按回 N 次"= 本次)
    GameSysUiWorldKeepResetSession();
    g_uih.uiCamReapplies = 0;  // 禁用 UI 相机的"重申"计数也按会话清零

    bool ok = false;
    if (UiCfgChannelReady()) {
        UiCfgAdd("ECLHideUI", 0);
        ok = (MaskOf(cam) == 0);
    }
    if (!ok) {
        LogG("[ui] 配置栈通道未生效 -> 退回直接写 _SetUICameraCullingMask(0)");
        WriteCamMask(cam, 0, "隐藏: 写 0");
        ok = (MaskOf(cam) == 0);
    }
    // v1.0.0am: 世界(主)相机清位 —— 若配置了, 隐藏时一并施加
    bool worldOk = true;
    if (g_uih.worldClearBits != 0) {
        g_uih.mainNormalMask = MainMaskOf();
        worldOk = WorldCfgApply(g_uih.worldClearBits);
        char w[320];
        snprintf(w, sizeof(w),
                 "[ui] 世界清位: 主相机 0x%08X 清掉 0x%08X -> 0x%08X %s",
                 static_cast<unsigned>(g_uih.mainNormalMask),
                 static_cast<unsigned>(g_uih.worldClearBits),
                 static_cast<unsigned>(MainMaskOf()), worldOk ? "OK" : "!! 没写成");
        LogG(w);
    }
    char b[512];
    snprintf(b, sizeof(b),
             "[ui] 隐藏游戏 UI: %s | 常规遮罩=0x%08X(位=", why,
             static_cast<unsigned>(g_uih.normalMask));
    std::string s(b);
    bool first = true;
    for (int i = 0; i < 32; ++i) {
        if (!(static_cast<unsigned>(g_uih.normalMask) & (1u << i))) continue;
        if (!first) s += ",";
        s += std::to_string(i);
        first = false;
    }
    s += ") 当前=0x";
    char h[16];
    snprintf(h, sizeof(h), "%08X", static_cast<unsigned>(MaskOf(cam)));
    s += h;
    s += ok ? " -> 已隐藏(通道=游戏配置栈)"
            : " -> !! 没藏住(两条通道都无效)";
    LogG(s);
    // v1.0.0ar: 只压遮罩不够 —— **遮罩=0 的相机照样清屏**, 会把主相机画好的世界擦黑。
    // 所以隐藏开始就把 UI 相机整体禁用(还原见 GameSysUiHideEnd)。
    if (ok) UiCamHideDisable("隐藏开始");
    if (!ok) g_uih.active = false;
    return ok;
}

void GameSysUiHideEnd(const char* why) {
    if (!g_uih.active) return;
    void* cam = UiCamera();
    // v1.0.0ar: 先把被禁用的 UI 相机还原(它是"隐藏"的一部分, 必须比遮罩更早交还游戏)
    UiCamHideRestore(why);
    const int now = cam ? MaskOf(cam) : -1;
    if (UiCfgChannelReady()) UiCfgRemove("ECLHideUI");
    int after = cam ? MaskOf(cam) : -1;
    if (after == 0 && g_uih.normalMask >= 0) {
        // 撤销条目后仍然全关(异常/覆写) -> 直接写回常规遮罩(双保险)
        WriteCamMask(cam, g_uih.normalMask, "显示: 兜底写回常规遮罩");
        after = MaskOf(cam);
    }
    // v1.0.0am: 主(世界)相机也要还原 —— 撤销条目 + 让游戏自己重算; 仍缺位就直接补写
    const int mainBefore = MainMaskOf();
    if (g_uih.worldClearBits != 0) {
        WorldCfgRemove();
        int mAfter = MainMaskOf();
        if ((mAfter & g_uih.worldClearBits) != g_uih.worldClearBits &&
            g_uih.mainNormalMask != 0) {
            // v1.0.0ar 修正: 这里原本写的是 `mainNormalMask >= 0` —— 但遮罩 0xAFFFDBDF 当 int32 是
            // **负数**, 于是这条兜底**从来没执行过**(一直靠游戏自己重算兜住, 所以一直没暴露)。
            // 现在改成 `!= 0`(0 = 从未记录), 与保活通道同一套"只有 0 才算无效"的约定。
            void* main = MainCamera();
            if (main && g_sys.has_camMaskSet) {
                int32_t v = mAfter | g_uih.worldClearBits;
                void* p[1] = {&v};
                InvokeLogged(g_sys.mi_camMaskSet, main, p, "显示: 补写主相机遮罩");
                mAfter = MainMaskOf();
            }
        }
    }
    // v1.0.0ar: 保活期间可能**直接写过**主相机(不走配置栈) -> 结束前交还游戏, 让它按自己的
    // 配置栈重算(在 ESC 界面态下算出来仍是 0 = 游戏想让世界相机停着, 那是它的选择)。
    GameSysUiWorldKeepHandBack("隐藏 UI 结束");
    const int mainAfter = MainMaskOf();
    char b[768];
    snprintf(b, sizeof(b),
             "[ui] 显示游戏 UI: %s | 撤销前=0x%08X 撤销后=0x%08X 常规值=0x%08X %s"
             " (本次共重申 %d 次, 放行 %d 次) | 主相机 还原前=0x%08X 还原后=0x%08X%s",
             why, static_cast<unsigned>(now), static_cast<unsigned>(after),
             static_cast<unsigned>(g_uih.normalMask),
             (after < 0 || after == g_uih.normalMask || after != 0) ? "OK" : "!! 仍为 0",
             g_uih.reapplies, g_uih.releases, static_cast<unsigned>(mainBefore),
             static_cast<unsigned>(mainAfter),
             (g_uih.worldClearBits == 0) ? "(未启用世界清位)"
                                         : ((mainAfter & g_uih.worldClearBits) == g_uih.worldClearBits
                                                ? " 清位已恢复"
                                                : " !! 清位未恢复"));
    LogG(b);
    g_uih.active = false;
    g_uih.released = false;
    g_uih.worldReapplies = 0;
}

void GameSysUiEnforce(uint64_t streamAgeMs) {
    if (!g_uih.active) return;
    if (streamAgeMs > 500) {
        GameSysUiHideEnd("数据流中断 >500ms(失败要开)");
        return;
    }
    // v1.0.0am: 主(世界)相机清位 —— 先做, 与 UI 相机各自独立判定
    if (g_uih.worldClearBits != 0) {
        const int mm = MainMaskOf();
        if (g_maskOk && (mm & g_uih.worldClearBits) != 0) {
            ++g_uih.worldReapplies;
            const bool wok = WorldCfgApply(g_uih.worldClearBits);
            if (g_uih.worldReapplies <= 3 || !wok) {
                char b[256];
                snprintf(b, sizeof(b),
                         "[ui] 重申世界清位: 主相机遮罩 0x%08X -> %s (清掉 0x%08X, 第 %d 次)",
                         static_cast<unsigned>(mm), wok ? "已压回" : "!! 压不动",
                         static_cast<unsigned>(g_uih.worldClearBits), g_uih.worldReapplies);
                LogG(b);
            }
        }
    }
    void* cam = UiCamera();
    if (!cam) return;
    const int m = MaskOf(cam);
    if (!g_maskOk) return;
    // v1.0.0ar: 重申"禁用 UI 相机" —— 游戏可能自己把它打开, 而**遮罩=0 也照样清屏**,
    // 所以禁用态必须和遮罩一样每帧看着(便宜: 一次 bool 读 + 必要时一次 bool 写, 无托管分配)。
    if (g_uih.disableUiCam && g_uih.uiCamDisabled && g_sys.has_camEnabledGet) {
        int en = 1;
        if (ReadBool(g_sys.mi_camEnabledGet, cam, &en, "Camera.get_enabled") && en != 0) {
            ++g_uih.uiCamReapplies;
            UiCamSetEnabled(cam, 0);
            if (g_uih.uiCamReapplies <= 3 || (g_uih.uiCamReapplies % 300) == 0) {
                int a = -1;
                ReadBool(g_sys.mi_camEnabledGet, cam, &a, "Camera.get_enabled");
                char b[224];
                snprintf(b, sizeof(b),
                         "[ui] 重申禁用 UI 相机: 游戏又把它打开了 -> 已再关(第 %d 次, 结果 enabled=%d)",
                         g_uih.uiCamReapplies, a);
                LogG(b);
            }
        }
    }
    if (m == 0) {  // 还在隐藏态
        g_uih.released = false;
        return;
    }
    const bool extraBits =
        (g_uih.normalMask >= 0) && ((m & ~g_uih.normalMask) != 0);
    // v1.0.0ar: 策略"保留额外位"的目标遮罩 = 当前值去掉"常规 UI 位" —— 常规态(0x20)算出来是 0
    // (与全藏等价), ESC 界面态(0x420)算出来是 0x400(游戏那幅世界画面) ✓
    const int want = g_uih.keepExtra && (g_uih.normalMask >= 0)
                         ? (m & ~g_uih.normalMask)
                         : 0;
    if (g_uih.strict || g_uih.keepExtra || !extraBits) {
        // 需要重申: 游戏回到了常规状态(或它又重建了配置栈), 或者当前是严格/保留额外位模式
        ++g_uih.reapplies;
        bool ok = false;
        if (UiCfgChannelReady()) {
            UiCfgAdd("ECLHideUI", want);
            ok = (MaskOf(cam) == want);
        }
        if (!ok) {
            WriteCamMask(cam, want, "重申: 写目标遮罩");
            ok = (MaskOf(cam) == want);
        }
        if (!g_uih.released) {
            char b[256];
            snprintf(b, sizeof(b),
                     "[ui] 重申隐藏: 遮罩回到 0x%08X -> %s(目标 0x%08X%s, 第 %d 次)",
                     static_cast<unsigned>(m), ok ? "已压回" : "!! 压不动",
                     static_cast<unsigned>(want),
                     g_uih.keepExtra ? " = 保留游戏额外加的位" : "", g_uih.reapplies);
            LogG(b);
        }
        g_uih.released = false;
    } else if (!g_uih.released) {
        // 尊重游戏: 它主动显示了额外图层(典型: 按 ESC 呼出界面) -> 放行, 等它回到常规值
        g_uih.released = true;
        ++g_uih.releases;
        char b[256];
        snprintf(b, sizeof(b),
                 "[ui] 放行(尊重游戏): 游戏把遮罩改成 0x%08X(常规值 0x%08X 之外还多了 0x%08X) "
                 "-> 界面交回游戏显示; 等它回到常规值后会自动重新隐藏",
                 static_cast<unsigned>(m), static_cast<unsigned>(g_uih.normalMask),
                 static_cast<unsigned>(m & ~g_uih.normalMask));
        LogG(b);
    }
}

void GameSysUiReportState() {
    int wkRe = 0, wkFa = 0;
    const int wk = GameSysUiWorldKeepState(&wkRe, &wkFa);
    char b[640];
    snprintf(b, sizeof(b),
             "[ui] 状态: 请求隐藏=%d 策略=%s 常规遮罩=0x%08X 重申=%d 放行=%d 当前=%s | "
             "世界清位=0x%08X(重申 %d 次) 主相机=0x%08X | 保活=%s(按回 %d 次, 失败 %d 次) | "
             "UI相机=%s(重申 %d 次)",
             g_uih.active ? 1 : 0,
             g_uih.keepExtra ? "保留额外位"
                             : (g_uih.strict ? "严格保持" : "尊重游戏"),
             static_cast<unsigned>(g_uih.normalMask), g_uih.reapplies, g_uih.releases,
             g_uih.active ? (g_uih.released ? "放行中(游戏在显示界面)" : "隐藏中")
                          : "未启用",
             static_cast<unsigned>(g_uih.worldClearBits), g_uih.worldReapplies,
             static_cast<unsigned>(MainMaskOf()),
             wk ? "开" : "关", wkRe, wkFa,
             g_uih.uiCamDisabled ? "禁用" : "正常", g_uih.uiCamReapplies);
    LogG(b);
}

// ---- v1.0.0am: 图层名表 + 主(世界)相机清位 ----

// LayerMask.LayerToName(i) —— 静态方法, 1 个 int 参数, 返回托管字符串
std::string LayerName(int i) {
    if (!g_sys.has_layerToName) return "?";
    void* s = InvokeLogged1Int(g_sys.mi_layerToName, i, "LayerMask.LayerToName");
    if (!s) return "(null)";
    const Il2CppApi& a = Il2Cpp();
    if (!a.string_length || !a.string_chars) return "(无 string API)";
    const int32_t n = a.string_length(s);
    if (n <= 0 || n > 64) return "";
    uint16_t* c = a.string_chars(s);
    if (!c) return "?";
    std::string o;
    for (int32_t k = 0; k < n; ++k) {
        const uint16_t ch = c[k];
        o += (ch < 0x80) ? static_cast<char>(ch) : '?';
    }
    return o;
}

void GameSysUiReportLayers() {
    LogG("[ui] ===== 图层与相机体检(只读, 不写任何状态) =====");
    {
        char b[256];
        snprintf(b, sizeof(b),
                 "[ui] 能力: LayerToName=%d NameToLayer=%d addMainCamCfg=%d rmMainCamCfg=%d "
                 "updMainCamCfg=%d setMainCamMask=%d mainCamDefault=%d mainCamMaskGet=%d "
                 "uiModelMask=%d gachaMask=%d",
                 g_sys.has_layerToName, g_sys.has_nameToLayer, g_sys.has_addMainCamCfg,
                 g_sys.has_rmMainCamCfg, g_sys.has_updMainCamCfg, g_sys.has_setMainCamMask,
                 g_sys.has_mainCamDefaultMask, g_sys.has_mainCamMaskGet, g_sys.has_uiModelMask,
                 g_sys.has_gachaMask);
        LogG(b);
    }
    MainMaskOf();  // 预读一次, 让日志顺序更好看
    LogG("[ui] ① 图层名表 LayerMask.LayerToName(0..31):");
    for (int i = 0; i < 32; ++i) {
        const std::string nm = LayerName(i);
        if (nm.empty()) continue;
        char b[128];
        snprintf(b, sizeof(b), "[ui]   位 %2d = %s", i, nm.c_str());
        LogG(b);
    }
    LogG("[ui] ② 相机遮罩(逐位带名字):");
    auto dumpMask = [](const char* who, int mask) {
        char b[512];
        snprintf(b, sizeof(b), "[ui]   %s 遮罩=0x%08X 渲染的层=[", who,
                 static_cast<unsigned>(mask));
        std::string s(b);
        bool first = true;
        for (int i = 0; i < 32; ++i) {
            if (!(static_cast<unsigned>(mask) & (1u << i))) continue;
            if (!first) s += ", ";
            s += std::to_string(i);
            const std::string nm = LayerName(i);
            if (!nm.empty()) { s += ":"; s += nm; }
            first = false;
        }
        s += "]";
        LogG(s);
        // 未渲染的位(找"这不属于我"的层很有用)
        std::string t = "[ui]     未渲染的位=[";
        bool f2 = true;
        for (int i = 0; i < 32; ++i) {
            if (static_cast<unsigned>(mask) & (1u << i)) continue;
            if (!f2) t += ", ";
            t += std::to_string(i);
            const std::string nm = LayerName(i);
            if (!nm.empty()) { t += ":"; t += nm; }
            f2 = false;
        }
        t += "]";
        LogG(t);
    };
    void* ui = UiCamera();
    const int uiMask = MaskOf(ui);
    const int mainNow = MainMaskOf();
    dumpMask("MainCamera(世界)", mainNow);
    dumpMask("UICamera(HUD)", uiMask);
    {
        char b[256];
        snprintf(b, sizeof(b),
                 "[ui] ③ 游戏自报默认遮罩: main=0x%08X ui=0x%08X | 当前持续清位设置=0x%08X",
                 static_cast<unsigned>(GameSysUiMainDefaultMask()),
                 static_cast<unsigned>(GameSysUiUiDefaultMask()),
                 static_cast<unsigned>(g_uih.worldClearBits));
        LogG(b);
    }
    if (g_sys.has_uiModelMask) {
        LogG("[ui] ④ GetUIModelLayerMask(i) / GetGachaLayerMask(i) (i=0..3):");
        for (int i = 0; i < 4; ++i) {
            int v = -1;
            if (ReadInt32Args1(g_sys.mi_uiModelMask, i, &v, "GetUIModelLayerMask")) {
                char b[192];
                snprintf(b, sizeof(b), "[ui]   UIModelLayerMask(%d)=0x%08X 位=0x%08X", i,
                         static_cast<unsigned>(v), static_cast<unsigned>(1 << (v & 31)));
                LogG(b);
            }
            int g = -1;
            if (g_sys.has_gachaMask &&
                ReadInt32Args1(g_sys.mi_gachaMask, i, &g, "GetGachaLayerMask")) {
                char b[192];
                snprintf(b, sizeof(b), "[ui]   GachaLayerMask(%d)=0x%08X", i,
                         static_cast<unsigned>(g));
                LogG(b);
            }
        }
    }
    LogG("[ui] ===== 体检结束。要看某一层到底管什么: 发指令 71 清掉它试试"
         "( -A0 <位掩码> -A1 <秒>, 自动还原 ) =====");
}

// 指令 71 的执行体
void GameSysUiWorldClearSet(int clearBits, float holdSec) {
    GameSysResolve();
    const int mainNow = MainMaskOf();
    const bool mainOk = g_maskOk;
    char b[512];
    if (clearBits == 0) {
        // 取消持续清位; 若有一次性测试在跑, 立即结束它
        g_uih.worldClearBits = 0;
        if (g_uiwt.pending) {
            g_uiwt.pending = false;
            WorldCfgRemove();
            int back = MainMaskOf();
            if ((back & g_uiwt.bits) != 0) {
                void* main = MainCamera();
                if (main && g_sys.has_camMaskSet) {
                    int32_t v = back | g_uiwt.bits;
                    void* p[1] = {&v};
                    InvokeLogged(g_sys.mi_camMaskSet, main, p, "Camera.set_cullingMask(取消测试)");
                    back = MainMaskOf();
                }
            }
            snprintf(b, sizeof(b), "[ui] 世界清位测试已取消并还原: 主相机 0x%08X -> 0x%08X",
                     static_cast<unsigned>(mainNow), static_cast<unsigned>(back));
            LogG(b);
        } else {
            snprintf(b, sizeof(b),
                     "[ui] 世界清位已关闭(主相机保持=0x%08X)。要一次性试某几层: "
                     "-Id 71 -A0 <位掩码> -A1 3", static_cast<unsigned>(mainNow));
            LogG(b);
        }
        GameSysUiReportState();
        return;
    }
    if (!mainOk) {
        LogG("[ui] 世界清位失败: 读不到 MainCamera 或它的遮罩(get_main 或 get_cullingMask 失败)");
        return;
    }
    if (holdSec > 0.f) {
        // 一次性测试: 到期自动还原(每帧 tick 驱动)
        const int hold = (holdSec > 60.f) ? 60 : static_cast<int>(holdSec + 0.5f);
        if (hold < 1) {
            LogG("[ui] 世界清位测试: 保持秒数太小(至少 1 秒)");
            return;
        }
        g_uiwt.pending = true;
        g_uiwt.bits = clearBits;
        g_uiwt.until = GetTickCount64() + static_cast<uint64_t>(hold) * 1000ull;
        const bool ok = WorldCfgApply(clearBits);
        const int after = MainMaskOf();
        snprintf(b, sizeof(b),
                 "[ui] 世界清位测试(%d 秒后自动还原): 清掉 0x%08X | 主相机 0x%08X -> 0x%08X %s"
                 " —— 请看画面: 想隐藏的东西消失了吗? 世界有没有缺东西?",
                 hold, static_cast<unsigned>(clearBits), static_cast<unsigned>(mainNow),
                 static_cast<unsigned>(after), ok ? "OK" : "!! 没写成");
        LogG(b);
        return;
    }
    // 持续模式: 勾选"隐藏游戏 UI"时一并生效
    g_uih.worldClearBits = clearBits;
    snprintf(b, sizeof(b),
             "[ui] 世界清位已设为**持续** 0x%08X(勾选隐藏 UI 时一并清掉; 发 -A0 0 取消)", 
             static_cast<unsigned>(clearBits));
    LogG(b);
    if (g_uih.active) {
        const bool ok = WorldCfgApply(clearBits);
        snprintf(b, sizeof(b), "[ui]   当前正在隐藏 -> 立刻施加: 主相机 0x%08X -> 0x%08X %s",
                 static_cast<unsigned>(mainNow), static_cast<unsigned>(MainMaskOf()),
                 ok ? "OK" : "!! 没写成");
        LogG(b);
    }
    GameSysUiReportState();
}

int GameSysUiWorldClearBits() { return g_uih.worldClearBits; }


void GameSysProbeTick(const float enginePos[3]) {
    if (g_oc.active) OffsetCalibTick(enginePos);
    if (g_as.active) AnchorSampleTick(enginePos);
}

// 每帧无条件推进(由 OnGameTick 直接调用, 不走 ProbeFrameTick —— 后者只在探测期间跑)
void GameSysUiTick() {
    UiMaskTick();
    GameSysDofTick();  // v1.0.0as: 指令 76 的景深通道验证到期还原
}

// ---- v1.0.0ar: 指令 73 —— 主(世界)相机"保活"的对外入口 ----
// **必须在匿名命名空间之外定义**(否则拿内部链接, link.cpp 的指令分派链不到)。
//   a0 = 1 开(持久) / 0 关(默认; 同时结束一次性测试, 并让游戏自己重算遮罩)
//   a1 > 0  一次性测试秒数(到期自动还原; 适合"先试一下再看画面")
//   a2 != 0 目标遮罩覆盖(诊断用; 仍会自动扣掉"持续清位")
//   a3 = 1  激进模式(缺世界位就按回) / 0 保守模式(仅当主相机被彻底关掉(=0)时按回)
// 判据: 只认"主相机被游戏关掉"这一种情况才动手 —— 实测 ESC 时游戏写的正是 0。
// 安全: 绝不写 0(那是黑屏); 目标推导不出来就什么都不做; 数据流中断 >500ms 自动关闭(失败要开)。
void GameSysUiWorldKeepSet(int on, float holdSec, int maskOverride, int mode) {
    GameSysResolve();
    const int cur = MainMaskOf();
    char b[512];
    if (maskOverride != 0) g_wk.maskOverride = maskOverride;
    g_wk.mode = mode ? 1 : 0;
    if (holdSec > 0.f) {
        const float cap = (holdSec > 600.f) ? 600.f : holdSec;
        g_wk.pending = true;
        g_wk.until = GetTickCount64() + static_cast<uint64_t>(cap * 1000.f);
        g_wk.reapplies = 0;
        g_wk.fails = 0;
        snprintf(b, sizeof(b),
                 "[ui] 世界保活测试(%.0f 秒后自动还原): 目标遮罩 0x%08X(%s) | 主相机当前=0x%08X"
                 " —— **只在勾选隐藏 UI 时生效**; 请看画面: 按 ESC 后世界还在吗?",
                 cap, static_cast<unsigned>(WorldKeepTarget()),
                 g_wk.mode ? "缺世界位就按回" : "仅在完全关闭时按回", static_cast<unsigned>(cur));
        LogG(b);
    } else if (on) {
        g_wk.on = true;
        g_wk.reapplies = 0;
        g_wk.fails = 0;
        snprintf(b, sizeof(b),
                 "[ui] 世界保活已开启(持久): 主相机被游戏关掉时每帧按回 0x%08X(%s)"
                 " —— **只在勾选隐藏 UI 时生效**(没隐藏 UI 时一次都不碰主相机)",
                 static_cast<unsigned>(WorldKeepTarget()),
                 g_wk.mode ? "缺世界位就按回" : "仅在完全关闭时按回");
        LogG(b);
    } else {
        const bool wasPending = g_wk.pending;
        const int re = g_wk.reapplies, fa = g_wk.fails;
        g_wk.on = false;
        g_wk.pending = false;
        g_wk.maskOverride = 0;  // v1.0.0ar: 关掉时一并清掉诊断用的目标覆盖(免得残留到下次会话)
        snprintf(b, sizeof(b), "[ui] 世界保活已关闭: 共按回 %d 次, 失败 %d 次%s",
                 re, fa, wasPending ? "(一次性测试也一并结束)" : "");
        LogG(b);
        WorldKeepRestoreGame("手动关闭");
    }
    // 立刻执行一次, 不用等下一帧(方便"发完指令就看画面")
    if (g_wk.on || g_wk.pending) GameSysUiWorldKeepTick();
    GameSysUiReportState();
}

// 每帧推进(帧末钩子调用)。
// 这里**不挂"数据流中断就撒手"那条判据**(隐藏 UI 有那条, 因为它会把你困在没有 UI 的画面里):
// 保活是用户显式打开的设置, Blender 没连上时也该照常工作; 出问题的代价只是"界面时世界还在渲染",
// 想停就 -Id 73 -A0 0(或改 ini)。一次性测试另有"到期自动还原"兜底。
void GameSysUiWorldKeepTick() {
    if (!g_wk.on && !g_wk.pending) return;
    // ---- 闸门(用户 2026-09-15 要求): 没在隐藏 UI 就**一次都不许碰主相机** ----
    // 理由: 这个按回动作只是为了救"隐藏 UI 撞上游戏关掉世界相机"造成的黑屏; 不隐藏 UI 时
    // 画面是游戏自己的事(包括它按 ESC 主动把世界相机写成 0), 我们不该插手。
    if (!g_uih.active) {
        if (!g_wk.noHideNoted) {
            g_wk.noHideNoted = true;
            LogG("[ui] 世界保活: 当前**没有隐藏 UI** -> 不触发(按你的要求, 这条链路只在勾选隐藏 UI 时生效)"
                 "; 想现在试: 先勾选「隐藏游戏 UI」");
        }
        return;
    }
    g_wk.noHideNoted = false;
    if (g_wk.pending && GetTickCount64() >= g_wk.until) {
        g_wk.pending = false;
        char b[256];
        snprintf(b, sizeof(b), "[ui] 世界保活测试到期: 共按回 %d 次, 失败 %d 次",
                 g_wk.reapplies, g_wk.fails);
        LogG(b);
        WorldKeepRestoreGame("一次性测试到期");
        g_wk.wrote = false;
        if (!g_wk.on) {
            GameSysUiReportState();
            return;
        }
    }
    const int want = WorldKeepTarget();
    if (want == 0) {  // 推导不出来 -> 绝不动主相机(写 0 就是黑屏)
        if (++g_wk.fails <= 3) LogG("[ui] 世界保活: 目标遮罩推导不出来 -> 本轮不动主相机");
        return;
    }
    const int cur = MainMaskOf();
    if (!g_maskOk) return;
    const bool missing = (g_wk.mode == 0) ? (cur == 0) : ((cur & want) != want);
    if (!missing) return;
    ++g_wk.reapplies;
    const bool ok = WriteMainMaskForce(want);
    if (ok) g_wk.wrote = true;
    if (!ok) ++g_wk.fails;
    if (g_wk.reapplies <= 3 || (g_wk.reapplies % 300) == 0 || !ok) {
        char b[320];
        snprintf(b, sizeof(b), "[ui] 世界保活: 主相机 0x%08X -> %s(按回 0x%08X, 第 %d 次, 失败 %d 次)",
                 static_cast<unsigned>(cur), ok ? "已按回" : "!! 按不动",
                 static_cast<unsigned>(want), g_wk.reapplies, g_wk.fails);
        LogG(b);
    }
}

// 隐藏 UI 开始时清零(让状态行里的"按回 N 次"反映**本次**会话)
void GameSysUiWorldKeepResetSession() {
    g_wk.reapplies = 0;
    g_wk.fails = 0;
    g_wk.wrote = false;
    g_wk.noHideNoted = false;
}

// 隐藏 UI 结束时(或开始新的会话前)把主相机交还游戏: 让游戏按自己的配置栈重算。
// 为什么需要: 保活是**直接写**相机遮罩(不走配置栈), 不还的话那个值会一直留着。
bool GameSysUiWorldKeepHandBack(const char* why) {
    if (!g_wk.wrote) return false;
    g_wk.wrote = false;
    WorldKeepRestoreGame(why);
    return true;
}

// 只读诊断: 保活是否开着(供指令 67 的状态行)
int GameSysUiWorldKeepState(int* reapplies, int* fails) {
    if (reapplies) *reapplies = g_wk.reapplies;
    if (fails) *fails = g_wk.fails;
    return (g_wk.on || g_wk.pending) ? 1 : 0;
}
}  // namespace ecl
