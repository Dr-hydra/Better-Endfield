# Endfield Camera Relink V1.0C 移植可行性分析（2026-09-26）

> 分析对象：`F:\Better Endfield\tmp_analysis\camera_relink\Endfield_Camera_Relink_V1.0C`（MIT，作者"一块铅矾"）
> 对照对象：`native/modules/camera/module.cpp`（我方相机模块 1.5.0）
> 方法：纯静态阅读源码与文档，未运行任何 exe/dll/ps1/py。标注【未验证】的是推断或只在对方文档里声称、我方未实测的结论。
> 下文路径 `src/…` 指 `…\V1.0C\source\src\…`，`addon` 指 `…\V1.0C\blender_addon\endfield_camera_bridge\__init__.py`。

---

## 0. 先说结论（先纠正一个认识偏差）

1. **对方没有自己写"键鼠自由相机"。** 所谓"更丰富的自由相机"其实是两样东西：
   - 游戏**内置的官方"营销相机"**：`CameraUtils.OpenMarketingCamera()` 打开，控制器是 `Beyond.Gameplay.View.MarketingCameraController`。对方只负责把它打开或关掉。这台相机本身带移动/旋转/滚转模式/升降/变速/FOV/**时间缩放**/镜头抖动/吸附目标等能力（符号见 §1.2）；
   - **Blender 驱动的 6 自由度位姿**：位置、朝向（含滚转）、焦距对应 FOV、光圈、对焦距离。
2. **对方没有写 `Time.timeScale` 的"时停"代码。** 用户感觉"时停更好"，根源是下面两条之一，或两条都有：
   - 它借用**游戏原生 ESC 暂停**，再修好暂停时的画面：禁用 UI 相机、强制保住世界相机的 cullingMask、清掉 WorldUI 层。结果是"时停的干净世界画面，没有任何 UI"；
   - 官方营销相机自带时间缩放（`ChangeTimeScale` / `_UpdateTimeScale` / `m_timeScaleHandle`），走的是游戏自己的时间控制系统。对方代码**没有调用**它。它在正式版里是否绑了按键，【未验证】。
3. **"转场导入"不在游戏侧。** 运镜轨迹全部在 Blender 里生成和插值：预设脚本写关键帧，Blender 播放动画。游戏侧只按帧读取 128 字节的共享内存包，把**最新一包**写进相机，**不做任何插值**。
4. 值得移植、而且能同时照顾 Android 的，按性价比排序：**隐藏 UI + ESC 时停取景** > **把我方自由相机的写入点挪到 PushState 并补上旋转、滚转和 FOV** > **原生运镜预设（C++ 路径播放器）** > **打开官方营销相机** > **Blender 联动（仅 Windows）**。

---

## 1. 第三方功能清单与实现方式

### 1.1 总体架构与钩子

| 项 | 实现 | 位置 |
|---|---|---|
| 注入 | 自带 `injector.exe`：`CreateProcess(CREATE_SUSPENDED)` → `VirtualAllocEx`/`CreateRemoteThread(LoadLibraryA)` → `ResumeThread`。文档明说是为了"抢在 ACE 武装之前" | `source/tools/injector/main.cpp:31-99`；`docs/04 §三` |
| VM 就绪 | 用 MinHook 挂 `il2cpp_init_utf16`/`il2cpp_init` 导出；超时退化为"导出可用 + 静置 3s/10s"，然后 `il2cpp_thread_attach` | `src/dllmain.cpp:194-236, 239-300` |
| 解析 | 直接调 il2cpp 导出，按"程序集/命名空间/类/方法/**参数个数**"解析，不校验参数类型（签名只由调试指令 30/44 打印） | `src/resolver.cpp`、`src/gamesys.cpp:199-219` |
| 钩子 1（主锚点） | `Beyond.Gameplay.View.CameraManager.TailLateTick(float)`；备用 `CameraMono._ProcessDitherByPitch()`。每帧执行指令队列、读共享内存、组织目标位姿 | `src/dllmain.cpp:34-55`；`src/link.cpp:1616 OnGameTick` |
| 钩子 2（位姿写入） | `Cinemachine.CinemachineBrain.LateUpdate()`，**原函数返回之后**写 `Camera.main.transform`（position/rotation）和 `fieldOfView`；隐藏 UI 的重申、世界相机保活也放在这里 | `src/dllmain.cpp:58-98`；`src/link.cpp:2137 OnCameraBrainTick`、`1409 ApplyPoseTarget` |
| 调用方式 | 只用 `il2cpp_runtime_invoke`，**明令禁止读写托管字段**（文档记录因此崩过 3 次） | `src/gamesys.cpp:1-13`；`docs/04 §七` |
| IPC | ① 命名共享内存 `EndfieldCameraBridgeV1`，只读映射（`OpenFileMappingA`）；② UDP 指令通道，只绑 `127.0.0.1:9601`（`ECMD` 12 字节 / `ECM2` 24 字节） | `src/shmem.cpp:73-135`；`src/link.cpp:521-582` |

> 代码瑕疵（不要照抄）：`AnchorDetour` 声明为 `(void* self, void* method, float dt)`（`src/dllmain.cpp:45-49`），而 IL2CPP 的真实签名是 `(self, float dt, MethodInfo*)`。x64 下它只是把寄存器原样转发给原函数，所以"碰巧能跑"【推断】。我方 `DetourTailLateTick(void*, float, void*)`（`module.cpp:1399`）的签名是对的。

### 1.2 官方自由相机（营销相机）

- 打开/关闭：`CameraUtils.OpenMarketingCamera()` / `CloseMarketingCamera()`（`src/gamesys.cpp:535-547`）。实例取法：`CameraManager.GetMainMarketingCameraController()`。
- 对方封装了模拟输入：`SetMoveInput(float,float)`、`SetRotateInput(float,float)`、`Zoom(float)`、`ChangeFov(**Int32**)`（`src/gamesys.cpp:562-621`）。注意 `ChangeFov` 的参数是 Int32，而且是**速率**语义：传 float 会得到巨大负速率，画面变灰（`docs/reference/CAMERA_SYMBOLS.md` §一）。
- 符号清单里还有：`EnterRollMode/LeaveRollMode`、`SetRollInput(1)`、`ChangeHeight(1)`、`ChangeMoveSpeed(1)`、`ChangeRotationSpeed(1)`、`ChangeTimeScale(1)`、`ChangeShakeNoise(1)`、`ToggleAttachMode/Attach/AttachToPoint/AttachToGroup/FeedPointMove(3)`、`ToggleMoveSpaceMode`；字段 `m_timeScale / m_timeScaleHandle / minTimeScale / maxTimeScale`（`docs/reference/inventory_camera_symbols.log:6358-6455`）。**对方只调用了前 4 个。**
- 官方相机的键鼠绑定由游戏自己处理。对方文档没写具体按键，零售版是否可用全部功能，【未验证】。
- 已知副作用：打开后 `get_curActiveController` 变成 `MarketingCameraController`，快照相机的偏移不再生效，`vcamFollow` 也读不到角色（`docs/04 §五`、`docs/03 §378`）。

### 1.3 Blender 实时联动（位姿 + 镜头）

- **数据包**（addon `PACKET = "<4sIQdIi3f3f3f8f5fQ"`，共 128 字节，addon:56-57；读取侧在 `src/shmem.cpp:92-135`）：magic `ECB1`、version 1、seq（偶数表示稳定，写入前先写 seq|1）、timestamp、flags、frame、pos[3]、forward[3]、up[3]、lens/sensorW/sensorH/clipStart/clipEnd/focusDist/fStop/fps、refPos[3]+2 个保留、seq 副本（用于撕裂检测）。
  - flags 各位：0 启用、1 位姿、2 镜头、3 抑制（未消费）、4 携带参考物件、5 随角色模式、6 隐藏 UI（`src/include/shmem.h`）。
- **坐标**：Blender 端先做 `_to_unity(v)=(x,z,y)`（addon:119）；模块用 `LookRotation(forward, up)` 重建四元数（`src/shmem.cpp:21-70`）。FOV = `2·atan(sensorH/(2·focal))`。
- **位置映射**（`src/link.cpp:1760-1800`）：`目标 = 游戏原点 + (Blender相机 − Cube参考物件) × pos_scale`，并用 `pose_max_offset=800` 限幅。
  - 游戏原点有 6 种来源（`src/gamesys.cpp:757-850`）：武装时刻的机位（冻结）、`CameraManager.get_curVirtualCam()→CinemachineVirtualCamera.get_Follow/get_LookAt()→Transform.get_position`、`get_curActiveController()→CameraControllerBase.get_cameraTrans()`、引擎相机减 `SnapshotCameraController.GetCameraOffset()`、引擎相机本帧位置；
  - 另有自动回退：首选来源连续 0.5s 读不到时改用引擎相机，恢复 0.3s 后切回（`src/link.cpp:126-154`）；
  - 另有"写入时刻补读锚点"：写入前重读锚点并补差，用来消除角色移动时的相位抖动（`src/link.cpp:1438-1461`）。
- **朝向**：默认"绝对"模式，直接采用映射后的 Blender 朝向；可选世界系或本地帧增量（`src/link.cpp:1958-2010`）。
- **写入**：在 `CinemachineBrain.LateUpdate` 之后调 `Transform.set_position/set_rotation` + `Camera.set_fieldOfView`（`src/link.cpp:1409-1500`）。实验性的 `pose_mode=4` 改走 `SnapshotCameraController.SetCameraOffset(Vector3)`（值类型必须传**裸值指针**，传装箱对象无效），文档结论是不适合正式使用。
- **安全阀**：数据流中断超过 500ms 就停止写位姿、交还游戏；中断超过 3s 后恢复时重建基线（`src/link.cpp:2152-2159, 1685-1698`）。
- **镜头**（`src/link.cpp:1255-1345`，400ms 节流）：焦距 → `CameraManager.SetOverrideFOVForCurrCamera(float)`；光圈/对焦 → `SnapshotCameraController.SetAperture / SetFocusDistance`，之后必须调 `ApplySnapshotDofSettings()` 才会挂到主相机上；可选每 3 秒保活一次。快照相机实例**只能**从 `CameraManager.ToggleSnapshotCamera(true,false)` 的返回值取到（会进入拍照模式），而且只允许由用户显式触发。文档记录：在标题画面自动调用会让游戏卡死在 loading（`CAMERA_SYMBOLS.md §二`）。

### 1.4 运镜预设 / "转场导入"

- 预设完全在 Blender 里：`presets/*.py` 文件头用 `# EFPRESET: {json}` 声明元数据，`build(ef, params)` 调用 `ef.*` 写关键帧（`docs/07_运镜预设.md`）。
- 数学部分：机位用以 Cube（角色头部）为原点的球坐标 `pose(theta, radius, height, aim_height, dutch)`（addon:980）。`orbit_linear / dolly_linear / crane_linear`（addon:1272-1310）用 LINEAR 插值保证匀速，关键帧数由四条判据**数值搜索**得到：弦偏差 ≤1cm、折角 ≤3°、瞄准偏差 ≤0.5°、转动台阶比 ≤1.25。缓动版 `sweep`（addon:1071）按弧长和偏差自适应加密，配 BEZIER + AUTO_CLAMPED（addon:1313-1330）。时间一律按归一化时间/秒，改"运镜时间"就整段等比缩放。
- 播放：Blender 常驻 timer 以 1–1000Hz 调 `send_camera`（addon:328-347）。对方文档要求发包率 ≥ 2× 游戏帧率，否则漏帧抽动，因为游戏侧"没有新包的帧就不写"。
- 自带的三个预设是环绕 180°、推拉、升降（`presets/01~03`）。**没有"导入外部相机路径文件"的功能**，"导入"指的是在 Blender 里做好动画后实时播放过去。

### 1.5 隐藏游戏 UI（可直接移植的亮点）

| 层 | 手段（全部是游戏自己的接口） | 位置 |
|---|---|---|
| 屏幕 HUD | UI 相机（`CameraManager.get_uiCamera()`，名 UICamera，只渲染层 5，depth 2）：`AddUICamCullingMaskConfig("ECLHideUI", 0)` + `_UpdateUICamCullingMask()`；恢复时 `RemoveUICamCullingMaskConfig` 并让游戏重算；兜底 `_SetUICameraCullingMask(int)` / `Camera.set_cullingMask` | `src/gamesys.cpp:2801-2848, 3679-3755` |
| 清屏问题 | 遮罩为 0 的相机**仍会清屏**，而且本作自定义管线（HG.Rendering）不采纳 `clearFlags`。所以隐藏时要把 UI 相机 `Behaviour.set_enabled(false)`，并每帧重申 | `src/gamesys.cpp:3603-3651, 3850-3866` |
| 世界空间 UI | 怪物血条/体力条在**层 16 WorldUI**，由 MainCamera 渲染：`AddMainCamCullingMaskConfig("ECLHideWorldUI", cur & ~0x10000)` + `_UpdateMainCamCullingMask()` | `src/gamesys.cpp:3095-3158` |
| 策略 | 严格模式（每帧压回）或"尊重游戏"（游戏加层时放行）；断流 >500ms 自动恢复显示 | `src/gamesys.cpp:3812-3900` |

### 1.6 ESC 时停取景（用户说的"更好的时停"）

- 实测机制（`docs/10 §6`）：按 ESC 时游戏调 `_SetMainCameraCullingMask(0)` 关掉世界相机，同时 UI 相机遮罩变成 `0x420`（层 5 UI + 层 10 UIPP）。ESC 界面里那幅"冻结/模糊的世界画面"和菜单是**同一张层 10 合成图**，用遮罩拆不开。
- 对方做法：勾选隐藏 UI 时 ① 禁用 UI 相机（不画也不擦）；② 世界相机保活：帧末发现主相机遮罩被写成 0，就写回 `GetMainCamDefaultCullingMask() & ~清位`（读不到时退到勾选时记下的值，或实测常量 `0xAFFFDBDF`），**绝不写 0**；结束时用 `_UpdateMainCamCullingMask()` 交还游戏（`src/gamesys.cpp:3132-3227, 4181-4228`）。日志显示游戏只在切换状态时写一次 0，不是每帧对抗。
- 结果："ESC 时停的世界、画面里没有 UI"，这是游戏**原生暂停**，冻结是全局且一致的。
- 【未验证】ESC 暂停期间相机还能不能动。对方的数据读取在 `TailLateTick`（由 `TimeManager.TailLateTick` 驱动，见 `CAMERA_SYMBOLS.md` 崩溃栈），暂停时是否仍然运行未知；位姿写入在 `CinemachineBrain.LateUpdate` 之后，属于 Unity 生命周期，理论上不受暂停影响。文档只声称"能看到时停画面"，没声称"时停中能运镜"。

### 1.7 其他

- 大量只读诊断指令：签名体检、类成员枚举、锚点采样、逐帧追踪、图层名表等（`src/link.cpp:596-1200`，完整表见 `docs/04 §六`）；`inventory.cpp` 导出符号清单。
- 未采纳的思路：用营销相机的 `SetMoveInput/SetRotateInput` 做闭环伺服（未实现）。

---

## 2. 时停对比

### 2.1 我方现状（module.cpp）

- 热键 `pause_key`（默认 `'8'`）只在**自由相机激活**且 `pause_enabled` 时有效（`module.cpp:701-739`）。实现是读 `UnityEngine.Time.get_timeScale()` 保存旧值 → `set_timeScale(0)`；恢复时写回旧值（`RestoreWorldPause`，`module.cpp:598-608`；退出自由相机时也恢复，`611-629`）。
- 为了让 timeScale=0 时热键仍能响应，额外 hook 了 `Time.get_unscaledDeltaTime` 作为心跳，**只泵控制逻辑**（`DetourTimeUnscaledDelta`，`module.cpp:1379-1387`）。
- 自由相机的位移 `ApplyFreeCamera()` 只在 `CameraManager.TailLateTick` 的 detour 里执行（`module.cpp:1399-1411`），用 `GetTickCount64` 算 dt，写 `Transform.set_position` 和 `Camera.set_fieldOfView`（`module.cpp:657-699`）。

### 2.2 对方为什么"更好"（事实加推断）

| 维度 | 我方 `Time.timeScale=0` | 对方（ESC 原生暂停 + 画面修复） |
|---|---|---|
| 冻结范围 | 只冻结依赖 scaled time 的系统；走 `unscaledTime` 的 UI/部分特效/游戏自有时钟不受影响【推断】 | 游戏自己决定暂停哪些系统，全局一致 |
| 与游戏时间系统冲突 | 游戏有自己的时间缩放句柄体系：`CameraManager._InitTimeControllers / SetAffectedByTimeScale / m_affectedByTimeScaleIds`、`SnapshotActionPlayState.timeScaleModifierHandles`、营销相机的 `m_timeScaleHandle`。游戏可能覆写我们的 0，或者我们保存的"原值"正好是顿帧期间的 0.x，恢复后卡住【推断，未验证】 | 不写 timeScale，没有冲突 |
| 相机能否继续动 | 如果 timeScale=0 时 `TailLateTick` 停跑（它由 `TimeManager` 驱动），`ApplyFreeCamera` 也跟着停，暂停中相机就动不了【未验证；心跳 hook 的存在说明作者担心过 tick 停跑】 | 同样【未验证】；但位姿写在 Brain.LateUpdate 之后 |
| UI | 暂停时 HUD 仍在画面里 | 禁用 UI 相机、保住世界相机、清 WorldUI → 干净的静帧 |
| 服务端一致性 | 本地时间停了，服务端计时照常跑，风险不明【推断】 | 走游戏的正规暂停路径 |

**结论**：对方的优势不在"冻结算法"，而在于**借用原生暂停，并解决了暂停时的渲染问题**（UI 相机清屏、世界相机被关）。更进一步的原生方案（【未验证】，需要实机探测）：
1. `MarketingCameraController.ChangeTimeScale(1)`：营销相机自带时间缩放，相机本身按理不受影响；
2. `CameraManager.SetAffectedByTimeScale(0)/RemoveAffectedByTimeScale(1)`、`StopTick(1)/ResumeTick(1)`：可能把相机 tick 和世界时间解耦。

### 2.3 附带发现：我方自由相机可能有写入时机问题【需实机确认】

对方实测帧序是 `CameraManager.TailLateTick → … → CinemachineBrain.LateUpdate → 渲染`：**在 TailLateTick 里写 transform 会被 Brain 每帧覆盖**（`docs/04 §四`、`src/dllmain.cpp:52-57`）。我方自由相机恰好在 TailLateTick 里写 `Transform.set_position`。如果 Brain 当时在驱动相机，表现会是"不动"或"抖动"。我方第一人称已经改用 `PushStateToUnityCamera` 覆写 CameraState，自由相机没有跟着改。

另外，我方 `FindSnapshotCameraController()` 用的是 `Object.FindObjectOfType(Type,bool)`（`module.cpp:831-858`）。对方记录本作里这个调用"解析得到但调用抛托管异常"（`CAMERA_SYMBOLS.md §二`）。建议核对我方日志，确认第一人称 photo_mode_exit 路径是否受影响。

---

## 3. 自由相机实现对比

| 项 | 我方 | 对方 |
|---|---|---|
| 相机来源 | 自己控制 `Camera.main` | 官方营销相机（游戏输入）+ Blender 位姿覆盖 |
| 写入点 | `TailLateTick` 后 `Transform.set_position`（可能被 Brain 覆盖，见 §2.3） | `CinemachineBrain.LateUpdate` 之后写 transform（渲染前最后时机） |
| 避免 Cinemachine 冲突 | 第一人称：`PushStateToUnityCamera(CameraState&)` 直接改 RawPosition/校正量/Lens；自由相机：无 | 在 Brain 之后覆盖；位姿的唯一来源是"写入前读到的引擎值"（避免读回自己写的值） |
| 平移 | 方向键 + PgUp/PgDn，沿相机 forward/right/up，固定速度 `movement_speed` | Blender 任意轨迹；官方相机自带变速/升降 |
| 旋转 | **无**（沿用游戏相机朝向） | Blender 绝对朝向；官方相机 `SetRotateInput` |
| 滚转 | 无（已解析 `LensSettings.Dutch` 偏移但没用） | Blender 的 up 向量 → LookRotation；官方相机 `EnterRollMode/SetRollInput` |
| FOV | 配置里的固定值 | 焦距换算后走 `SetOverrideFOVForCurrCamera` 和 `Camera.fieldOfView` |
| 景深 | 无 | 快照相机 `SetAperture/SetFocusDistance/ApplySnapshotDofSettings` |
| 碰撞 | 无 | 无（只限幅 800） |
| 平滑 | 无（dt 夹到 ≤50ms） | 游戏侧无插值，平滑来自 Blender 曲线；"引擎相机原点"可继承游戏的跟随阻尼 |
| 安全阀 | 相机对象变化时退出 | 断流 >500ms 交还游戏；隐藏 UI 失败时一律恢复显示 |
| 平台 | Windows + Android（Dobby） | 仅 Windows（MinHook、共享内存、Winsock） |

**建议的我方做法**：把自由相机的写入迁到已有的 `DetourPushState`（`module.cpp:1389`），直接写 `RawPosition`、`RawOrientation`，把 `PositionCorrection/OrientationCorrection` 清零，并写 `Lens.FieldOfView` 和 `Lens.Dutch`（滚转）。偏移表 `g_state_layout` 已经解析好了（`module.cpp:790-818`）。这等价于、甚至优于对方的"Brain 之后写 transform"：不用新钩子、镜头参数一致、Android 已经验证能用。

---

## 4. 转场 / 运镜路径 / 预设

- **数据格式**：实时通道是 §1.3 的 128 字节共享内存包，seqlock 语义；遗留 UDP 包是 36 字节（`src/include/pose.h`、`source/protocol.md`）。预设是带 `EFPRESET` JSON 头的 Python 脚本。**没有独立的相机路径文件格式。**
- **插值**：全部在 Blender 里完成（LINEAR 或 BEZIER AUTO_CLAMPED，关键帧数自动求解）。游戏侧不插值，只写最新包。
- **驱动游戏相机**：TailLateTick 读包 → 算目标 → Brain.LateUpdate 之后写 transform/FOV；镜头参数走游戏 API。
- **外部依赖**：Blender ≥3.6（实测 5.2.1）+ 对方插件（MIT）；命名共享内存 `EndfieldCameraBridgeV1`；UDP 127.0.0.1:9601（只用于"修复虚化"等一次性指令）；`%TEMP%` 下的若干令牌文件（`EndfieldCameraLink_mode.txt / _settings.txt / _arm.txt`）。
- **可原生化的部分**：球坐标机位 `pose()` 和三种匀速运镜（环绕/推拉/升降）的数学都很小。可以在我方模块里实现"锚点（角色）+ 球坐标关键帧 + LINEAR/缓动插值 + 按 unscaled 时间播放"，直接喂给 PushState，**不依赖 Blender，Android 也能用**。

---

## 5. 逐项移植评估

| 功能 | 工作量 | 需要的契约/改动 | 风险 | Android |
|---|---|---|---|---|
| A. 隐藏 UI（UI 相机配置栈 + 禁用 UI 相机 + WorldUI 层 16） | **小–中**（约 250–400 行） | MethodContract 新增：`CameraManager.get_uiCamera / AddUICamCullingMaskConfig(string,int) / RemoveUICamCullingMaskConfig(string) / _UpdateUICamCullingMask / _SetUICameraCullingMask(int) / AddMainCamCullingMaskConfig / RemoveMainCamCullingMaskConfig / _UpdateMainCamCullingMask / _SetMainCameraCullingMask / GetMainCamDefaultCullingMask`、`CameraUtils.get_cameraManager`、`Camera.get/set_cullingMask`、`Behaviour.get/set_enabled`；字符串用 `g_host->string_new`。每帧重申可放在 PushState 或 TailLateTick | 低：只动渲染遮罩，失败时一律恢复。更新脆弱性为中（`_` 前缀的私有方法名）；注意 cullingMask 是含位 31 的负 int，只能用 0 当无效哨兵 | 可移植；层号和 UI 相机结构需在 Android 包上复测 |
| B. ESC 时停取景（世界相机保活） | **小**（A 完成后约 80 行） | 同 A；只在隐藏 UI 期间生效，绝不写 0 | 低 | 可移植（Android 的暂停入口不同，需复测） |
| C. 用原生时间系统替换 `Time.timeScale=0` | **中**（需要先探测） | 候选：`MarketingCameraController.ChangeTimeScale`、`CameraManager.SetAffectedByTimeScale/StopTick`。先做只读签名体检，再做小步写测 | 中：语义未知，可能影响战斗/服务端计时 | 同 IL2CPP，理论可移植 |
| D. 自由相机升级（PushState 写入 + 鼠标/按键旋转 + 滚转 + FOV 缩放 + 变速 + 指数平滑 + unscaled dt） | **中** | 复用现有 CameraState 偏移；输入走现有 `KeyDown`（Android 走虚拟键） | 低：纯表现层 | 可移植（Dobby 已在挂 PushState） |
| E. 开关官方营销相机（外加 SetMoveInput/SetRotateInput/SetRollInput 映射） | 开关**小**；完整映射**中** | `CameraUtils.OpenMarketingCamera/CloseMarketingCamera`、`CameraManager.GetMainMarketingCameraController`、`MarketingCameraController.*`（注意 `ChangeFov(Int32)`） | 中：这是开发/营销用的隐藏功能，会切换 curActiveController，与我方第一人称和快照相机互相影响；进出后快照实例会失效 | 【未验证】营销相机在 Android 包里是否存在、输入是否绑定 |
| F. 景深（光圈/对焦） | **中** | `CameraManager.ToggleSnapshotCamera(bool,bool)`（返回实例，会进入拍照模式）、`SnapshotCameraController.SetAperture/SetFocusDistance/ApplySnapshotDofSettings/ResetSnapshotDofSettings`；400ms 节流；进出官方相机后要重新取实例 | 中：标题/加载阶段调用会卡 loading；只能由用户触发 | 可移植 |
| G. 原生运镜预设（环绕/推拉/升降，C++ 路径播放器） | **中** | 锚点用 `PlayerController.GetMainCharacter`（我方已有）或 vcam Follow；数学参考 addon:980、1272-1330 | 低 | 可移植 |
| H. Blender 联动（兼容 ECB1 协议，复用对方插件） | **中–大**（锚点/基线/安全阀约 600–900 行，可简化） | 共享内存读取器 + LookRotation + 参考物件锚点 + 500ms 安全阀；写入走 PushState | 低–中：本地 IPC 无鉴权；需在我方分发中附对方 MIT 声明 | **不可移植**（命名共享内存/Blender 仅桌面；局域网转发不建议） |
| I. 注入器 / il2cpp_init 钩子 / MinHook 封装 | **不建议移植** | 我方已有宿主（MinHook/Dobby 的 hook broker + BE_* 解析） | 高：挂起注入属于"绕开 ACE 时序"，性质敏感 | — |

**通用改动说明**
- 对方所有调用都是 `runtime_invoke`，可以一一改写成我方 `MethodContract`（完整签名 + 返回类型）+ `Invoke`，**比对方按参数个数解析更严格**。值类型参数（Vector3）按对方实测传裸值指针，这与我方 `SetValue` 的现有做法一致。
- 不需要额外的 MinHook。若 A/B 的重申需要"Brain 之后"的时机，可以复用 PushState 钩子（它就在 Brain 管线末端），不必再挂 `CinemachineBrain.LateUpdate`。
- 游戏大量使用 iFix 热修代理（符号中的 `<>iFixBaseProxy_*`），被热修的方法行为可能被服务端替换，这对任何钩子/调用都是更新脆弱点。

**许可证义务（MIT）**
- 如果复制或改写对方代码（包括 Blender 插件、预设数学），要在分发物里保留 `Copyright (c) 2026 一块铅矾 (Endfield Camera Relink)` 和 MIT 全文。建议加进我方第三方声明文件，并在移植文件头注明来源。
- 只借用思路和游戏符号名（事实性信息）不构成复制，但仍建议致谢。
- MinHook（BSD-2）我方已有，无新增义务。Blender 插件要求"随包保留原作者信息（一块铅矾 / Codex）"。

---

## 6. 可疑行为与安全关注

| 项 | 评估 |
|---|---|
| 网络 | 源码中只有 `127.0.0.1:9601` UDP 的收（DLL）和发（Blender 插件），**没有外网访问**。install/tools 脚本里没发现下载或 http 调用（只 grep 了关键词）。 |
| 预编译二进制 | `game_mod/EndfieldCamLink.dll`（3.2MB）和 `injector.exe` 是预编译的，**无法确认与源码一致**（包里有 `diff_dll_sections.py`，但我们没运行）。移植只用源码，不要分发或运行它们。 |
| 反作弊 | 挂起注入，刻意赶在 ACE 就绪前（对方文档自认"技术手段侵入性高"）。**这是最大风险**，我方不应引入。 |
| 游戏状态 | 默认只动渲染层：transform、FOV、cullingMask、相机 enabled、快照相机的 DOF 数值。会**切换游戏相机模式**（`ToggleSnapshotCamera` 进拍照模式、`OpenMarketingCamera`），文档记录自动调用曾卡死 loading。没有数值/背包/网络协议相关调用。 |
| 本地攻击面 | UDP 指令和共享内存都没有鉴权，本机任意进程都能让 DLL 开关官方相机或改遮罩（影响有限）。 |
| Blender 插件 | 会 `exec()` 预设目录里任意 `.py`（addon:1449），不可信的预设等于在 Blender 里执行任意代码。安装脚本会后台启动 Blender 改偏好设置；另会在 `%TEMP%` 写令牌文件。 |
| 未发现 | 键盘记录、上传、持久化（注册表/计划任务）、改游戏目录文件等行为（基于关键词 grep 与通读核心源码；未逆向二进制）。 |

---

## 7. 建议的落地顺序

1. **A + B**：隐藏 UI 和 ESC 时停取景，收益大、风险低、跨平台。
2. **D**：自由相机改走 PushState，补旋转/滚转/FOV/平滑。顺手解决 §2.3 的写入时机隐患，并实测"ESC 暂停/timeScale=0 时 PushState 是否仍被调用"。如果仍被调用，就能在原生暂停中自由运镜，比对方的效果更好。
3. **C**：探测原生时间缩放句柄，替换 `Time.timeScale`。
4. **G / F / E**：原生运镜预设、景深、官方营销相机开关。
5. **H**：只在明确需要 Blender 工作流时做，仅限 Windows，并附 MIT 声明。
