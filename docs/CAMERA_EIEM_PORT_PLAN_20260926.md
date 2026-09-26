# 相机增强与 EIEM 功能移植计划（2026-09-26）

目的：把 Endfield_Camera_Relink（MIT）和 EIEM（AGPL-3.0）的功能并入 Better Endfield，做成一个全功能 mod。
本文用于中断后续做：先看“进度”，再看对应阶段。

## 已定决策

- **范围**：直接移植 EIEM 的功能，不考虑与 EIEM 共存。
  - 不做：音乐同步（对接太麻烦）、MUS4 肌肉模式、ImGui 界面、代理加载器、更新检查。
  - Relink 的 Blender 实时联动暂不做；改为在游戏内实现运镜，并支持导入 VMD。
- **协议**：本仓库和 EIEM 都是 AGPL-3.0，可以直接移植代码。
  - 文件头保留出处："Portions derived from EIEM (https://github.com/Sasye/EIEM), AGPL-3.0"；
  - 在第三方声明中加一条。
  - Relink 是 MIT，只参考思路，不复制代码。
- **资料位置**：
  - EIEM 源码：`F:/Better Endfield/tmp_analysis/eiem/src/`（仓库最后推送 2026-09-11，共约 2.9 万行）；
  - Relink：`F:/Better Endfield/tmp_analysis/camera_relink/`；分析见 `docs/CAMERA_RELINK_PORTING_ANALYSIS_20260926.md`。
  - 两者都只读源码，不运行任何 exe/dll/脚本。
- **测试**：由我保证编译通过，实机效果由用户测试并提供 `BetterEndfield.log`。

## 进度

- [x] 阶段 1a：修复冻结时间后自由视角动不了（代码完成，待实机测试）
- [x] 阶段 1b：鼠标转向由我们自己计算（系统时钟）（同上）
- [x] 阶段 1c：高级运镜（环绕、推拉/变焦、升降、平移、滚转、平滑）（同上）
- [x] 阶段 1d：关键帧录制与回放（同上；保存/读取文件未做）
- [x] 阶段 1e：VMD 镜头导入（同上；扣除“センター”位移留到阶段 3）
- [ ] 阶段 2：VMD 解析器独立成共享代码
- [ ] 阶段 3：VMD 身体动作播放（大头，需多轮实机测试）
- [ ] 阶段 4：面部表情、眼睛、裙子布料

（每完成一项就勾选，并在“工作记录”末尾追加一行：日期、改了哪些文件、测试状态。）

## 阶段 1：相机（`native/modules/camera/module.cpp`）

### 现状（1843 行）

- **自由视角**：
  - `EnterFreeCamera` 在 631 行，`ApplyFreeCamera` 在 657 行，`PumpFreeCameraControl` 在 701 行。
  - 位置由我们用 `GetTickCount64` 计算，写入 `transform.position` 和 FOV。
  - 朝向来自游戏自己的相机，也就是游戏自带的鼠标环绕。
- **冻结**：把 `Time.timeScale` 设为 0（`PumpFreeCameraControl`），恢复在 `RestoreWorldPause`（598 行）。
- **钩子**：
  - `DetourTailLateTick`（约 1399 行）：调用 Pump，再调用 `ApplyFreeCamera`。
  - `DetourPushState`（约 1389 行，`cinemachine.push_state`）：只有第一人称用它，调用 `ApplyFirstPersonState`（1164 行）。
  - `DetourTimeUnscaledDelta`（约 1379 行）：冻结后仍然运行，但只处理热键。

### 1a/1b 冻结修复（已确认的原因）

- **原因**：
  - `timeScale=0` 后，`CameraManager.TailLateTick` 不再执行，`ApplyFreeCamera` 就不会被调用。用户实测：冻结后完全动不了。
  - 游戏相机的鼠标环绕也会随冻结停下。
- **做法**：
  - 自由视角维护自己的完整相机状态：`g_free_position`，加上 `g_free_yaw/pitch/roll` 和 FOV。
  - 进入自由视角时，从当前相机读取初始朝向。
  - 鼠标转向在输入线程里累计原始增量（`GetCursorPos` 差值，或 `WM_INPUT`），主线程按系统时钟消费。
  - 写入点改为 `DetourPushState`，照 `ApplyFirstPersonState` 的方式写 CameraState：
    - raw_position 和 raw_orientation；
    - 清零 correction；
    - 设置 lens FOV。
  - `TailLateTick` 只保留非冻结时的逻辑。
- **待实机确认**：冻结时 Cinemachine 是否仍在每帧调用 `PushStateToUnityCamera`。
  - Relink 在 ESC 暂停下能驱动相机，旁证它仍在调用。
  - 如果不调用，备选方案是在 `DetourTimeUnscaledDelta` 里直接写 `Camera.main` 的 transform。
  - 实现时在日志里记录冻结期间 `g_push_state_calls` 的增长量，用来判断。
- **退出和解冻**：恢复 timeScale，并把相机交还给游戏。现有的 `ExitFreeCamera` 流程不变。

### 1c 高级运镜（游戏内生成，使用系统时钟）

- **环绕**：以当前角色为中心，参数为半径、角速度、高度和注视点高度。
- **推拉/希区柯克变焦**：沿视线方向移动，同时按 `tan(fov/2)·距离 = 常数` 反向调整 FOV。
- **升降和平移轨道**：按速度匀速移动，支持缓入缓出。
- **滚转**：可用热键调整，有复位键。
- **FOV 缩放**：可用滚轮或热键。
- **平滑**：位置和角度都做指数平滑，有惯性，平滑系数可配置。
- 所有参数写进 `betterendfield.camera.module.ini`，界面接入管理器的相机设置页。

### 1d 关键帧录制与回放

- 用热键把当前机位记为关键帧，内容包括位置、朝向四元数、FOV 和时间间隔。
- 回放时：
  - 位置用 Catmull-Rom 或贝塞尔曲线插值；
  - 朝向用 slerp；
  - FOV 线性插值。
- 支持循环播放；可以保存为文件，也可以从文件读取（简单文本格式）。

### 1e VMD 镜头（参考 EIEM `camera_player.h` 和 `camera_control.h`）

- **VMD 镜头帧**：注视点、欧拉角（弧度）、距离、FOV，以及 24 字节插值参数，帧率 30fps。
  - 插值参数分 6 条曲线：x、y、z、旋转、距离、FOV；
  - 取下一帧的 `interp`，贝塞尔求值用牛顿迭代 12 次（`CamBezierEval`）。
- **相机位置**：`interest + rotate(euler, (0,0,distance))`，再乘缩放系数 0.07（`CAM_SCALE`）。
  - 欧拉角三个分量都取反，按 Y·X·Z 的顺序组合；FOV 加 5°。
- **对齐**：
  - 以角色的位置和朝向为原点，偏航再加 180°；
  - 按角色身高相对参考身高 1.245 的比例缩放；
  - 身体动作播放时，还要扣掉“センター”骨骼的位移（`SampleCharDisplacement`）。
- **写入**：EIEM 的做法是禁用 CinemachineBrain，再写 transform；我们统一走 1a 的 PushState 写入点。
- **时钟**：系统高精度计时（QPC），支持暂停、拖动和循环。

## 阶段 2：VMD 解析（EIEM `vmd_parser.h`，1228 行）

- 基本原样移植，放到共享位置（例如 `native/common/vmd_parser.h`）。
- 字符串是 Shift-JIS 编码。
- 需要骨骼帧、表情帧和相机帧，以及 IK 开关帧。
- 要加上文件大小和帧数上限，防止恶意文件。

## 阶段 3：身体动作（大头，约 1.5 万行）

- **来源**：`ghost_rig.h`（9226 行）、`direct_vmd_pose.h`（3149 行）、`direct_vmd_runtime.h`、`animation.h`、`leg_ik_player.h`、`bone_map.h`，以及主线程调度 `trojan.h`/`init.h`（约 5500 行）。
- **主要工作**：
  - 把直接调用 il2cpp 的地方改成我们的 `Contract(...)` 体系；
  - 与 actions 模块（`pose_overlay.inl`）协调骨骼所有权，避免两边同时写骨骼。
- **建议子步骤**：
  1. 骨骼映射和躯干 FK；
  2. 手臂、手指和扭转骨；
  3. 腿部 FK/IK（FinalIK）；
  4. 地形吸附。
  - 每步都实机测试后再做下一步。

## 阶段 4：面部与布料

- **面部**：`smc_face.h`（1614 行）负责表情 Morph、眨眼、口型和眼睛。
  - 它依赖动态解析的 SMC 字段偏移，游戏更新后最容易失效。
- **布料**：`cloth.h`（350 行）负责裙子碰撞体缩放。

## 工作记录

- 2026-09-26：分析 Relink 和 EIEM，确定本计划；尚未改代码。
- 2026-09-26：阶段 1a–1e 写完代码，原生模块和管理器均编译通过（0 错误），未实机测试。
  - **新文件**：`native/modules/camera/free_camera_runtime.inc`，包括自由视角位姿、鼠标钩子、运镜、关键帧和 VMD 镜头；另有 `ui/BetterEndfield.UI/Models/FreeCameraExtras.cs` 和 `THIRD_PARTY_NOTICES.md`。
  - **修改**：
    - `camera/module.cpp`：配置项、热键、`PushState` 写入、心跳回退、新增 `rotation.get/set` 契约，版本改为 1.6.0；
    - 管理器的 `ModConfiguration.cs`、`ConfigurationService.cs`、`MainWindow.xaml(.cs)`：新增“运镜与镜头导入”区块，配置可以往返保存。
  - **实机需确认**：
    1. 冻结后日志里是否出现 “Cinemachine is not pushing; writing the camera transform”，以及冻结后能否移动和转向；
    2. 鼠标钩子在游戏锁定光标时增量是否正常；
    3. VMD 的朝向、缩放和视野是否正确（参数可在界面调整）。
  - 编译方法：用 VS 自带的 cmake，构建目录为 `F:/zmd_bem/tmp/cam/build`（仓库里的 `build/` 是其他机器生成的缓存，不能用）。管理器编译时需把 `global.json` 临时改成 9.0.308，编译后已改回。
