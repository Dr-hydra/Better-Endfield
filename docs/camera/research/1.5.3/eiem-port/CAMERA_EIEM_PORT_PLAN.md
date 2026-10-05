# 相机增强与 EIEM 功能移植计划（2026-09-26）

目的：把 Endfield_Camera_Relink（MIT）和 EIEM（AGPL-3.0）的功能并入 Better Endfield，做成一个全功能 mod。
本文用于中断后续做：先看“进度”，再看对应阶段。

## 已定决策

- **范围**：直接移植 EIEM 的功能，不考虑与 EIEM 共存。
  - 不做：MUS4 肌肉模式、ImGui 界面、代理加载器、更新检查。
  - 音乐同步 2026-09-29 改为要做，见文末“2026-09-29 决策”。
  - Relink 的 Blender 实时联动暂不做；改为在游戏内实现运镜，并支持导入 VMD。
- **协议**：本仓库和 EIEM 都是 AGPL-3.0，可以直接移植代码。
  - 文件头保留出处："Portions derived from EIEM (https://github.com/Sasye/EIEM), AGPL-3.0"；
  - 在第三方声明中加一条。
  - Relink 是 MIT，只参考思路，不复制代码。
- **资料位置**：
  - EIEM 源码：`F:/Better Endfield/tmp_analysis/eiem/src/`（仓库最后推送 2026-09-11，共约 2.9 万行）；
  - Relink：`F:/Better Endfield/tmp_analysis/camera_relink/`；分析见 `docs/camera/research/1.5.3/relink-port/CAMERA_RELINK_PORTING_ANALYSIS.md`。
  - 两者都只读源码，不运行任何 exe/dll/脚本。
- **测试**：由我保证编译通过，实机效果由用户测试并提供 `BetterEndfield.log`。

## 进度

- [x] 阶段 1a：修复冻结时间后自由视角动不了（代码完成，待实机测试）
- [x] 阶段 1b：鼠标转向由我们自己计算（系统时钟）（同上）
- [x] 阶段 1c：高级运镜（环绕、推拉/变焦、升降、平移、滚转、平滑）（同上）
- [x] 阶段 1d：关键帧录制与回放（2026-09-27 已补保存/读取；入口为 INI+F6/F7；管理器文件选择 UI、Windows 构建与实机待验）
- [x] 阶段 1e：VMD 镜头导入（同上；扣除“センター”位移留到阶段 3）
- [x] 阶段 2：VMD 解析器独立成共享代码（2026-09-27 代码及主机测试完成；Windows 构建待验）
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

- 2026-09-27：以 main 9b1e895 为基线继续 PC 相机，实际读取 EIEM 447600b9 的解析/相机源码。
  - 新增 `native/shared/motion/vmd.h`、`camera_path.h`、`camera_file_worker.h`，共享完整 VMD 数据段与采样；文件 I/O 从游戏回调移到有界、可 join 的工作线程。
  - 机位路径实现版本化保存/读取；修复播放时清空关键帧和 VMD 最后一帧未呈现的问题，加入结果版本与有限值校验。
  - 管理器配置模型保留新路径和热键字段；未增加文件选择 UI；未修改安卓/BEM/CI。
  - `native/tests/camera_playback`：Clang ASan/UBSan 和 GCC 的 3 组测试均通过；Windows DLL/管理器构建、实机尚未执行。
  - 阶段 3/4 仍未实现，不能把共享解析器中的骨骼/Morph 数据读取当作游戏身体或面部播放完成。
  - 详细接口、使用方法和验收边界见 `docs/camera/research/1.5.3/pc-vmd-stages/PC_CAMERA_PROGRESS.md`。

## 2026-09-27 阶段 3/4 预览切片（不是完整阶段验收）

- [x] Host 命名 pose-lease 能力、Actions 协作、旧版本 DLL 握手与拒绝。
- [x] 躯干/肩臂/手指的相对 FK 预览、文件线程、角色/层级/配置生命周期。
- [x] 眼睛轨道以及标准 BlendShape 名称适配与可恢复权重写入。
- [ ] 真正 bind-pose/准标准骨完整重定向、Twist、腿部 FK/FinalIK、地形。
- [ ] SMC 专用面部、裙子/布料、身体/镜头同步及中心位移补偿。

参考与使用见 `../pc-vmd-stages/PC_CHARACTER_PREVIEW.md`。预览以当前姿态校准到源第 0 帧，
不应称为完整 MMD 身体播放。阶段 3、4 的总复选框仍保持未完成。
主机 7 项回归目标执行结果随交付验证记录；Windows DLL / WinUI / 游戏内验证待做。

## 2026-09-29 决策（第二轮，按此顺序施工）

Windows 检查：`dev/pc-camera-motion-20260927` 用 MSVC 编译 Host/Actions/Camera 与管理器通过；
7 个测试在把 `near()` 改名为 `nearly()`（与 Windows 宏冲突）后全过。

1. **小键盘键位**（MMD 相关全部移到小键盘；自由视角 `9`、冻结 `8`、第一人称 `-` 留在主键盘）
   - 用 WH_KEYBOARD_LL 按扫描码识别小键盘，不依赖 NumLock，并区分小键盘回车。
   - Enter 播放/暂停（动作+镜头+音乐）；+ 停止；4/6 后退/前进 5 秒；5 切镜头模式（VMD/自由/游戏）；
     7/9 滚转、8 复位；1/3 视野；2 运镜；0 记关键帧；. 播放关键帧；- 悬浮窗显示/隐藏；* / 空。
   - 关键帧清空/保存/读取、选文件只放悬浮窗/管理器。F6–F10 默认键取消。
   - 以后整体改为可录入、可组合的自定义快捷键，MMD 做完再做。
2. **统一播放控制与音乐**
   - 动作、镜头、音乐共用一个时钟：播放、暂停、跳转、循环、结束。
   - 音乐走音乐模块：新增“本地文件”数据源（Media Foundation 解码 WAV/MP3，不需要 OmniMix），
     通过 Wwise Audio Input 播放；导出命名能力（同 PoseLease 形式）给相机模块调用。
   - MMD 播放时暂停 OmniMix 流、保持原生 BGM 暂停，结束后恢复。开关与相机模块联动。
   - 原因：Host 禁止两个模块钩同一目标（hook_broker），相机模块不能自己再接一套 Wwise。
3. **悬浮窗**：复用战斗数据悬浮窗（独立 exe + 共享内存 + GDI+ 分层窗口）的基础，
   改成可点击（不激活窗口）、双向共享内存命令；游戏锁鼠标时需按 Alt。
4. **文件**：管理器里选文件并复制到 `<安装目录>\mmd\<作品>\`（每个作品一个 `set.ini`），相机模块和悬浮窗只读。
5. 之后：重定向（ghost rig）与腿部 IK → SMC 面部 → 布料 → 地形跟随、身高适配。

## 2026-09-29 第二轮施工结果（步骤 1–4 代码完成，待联调）

- [x] 小键盘键位：WH_KEYBOARD_LL 按扫描码识别，不依赖 NumLock，区分小键盘回车和独立方向键；钩子不可用时退回 GetAsyncKeyState（此时小键盘回车不可用）。
  - `hotkey_layout=2` 表示新布局；旧配置（layout 1）里的 F6–F10 默认键会被替换成小键盘默认值。
- [x] 统一播放（`mmd_director_runtime.inc`）：动作、镜头、表情、音乐共用一个 QPC 时钟，支持播放/暂停/停止/跳转/循环；
  镜头模式 VMD / 自由 / 游戏可切换；没有单独镜头文件时用动作 VMD 里的镜头段。
- [x] 音乐：音乐模块新增 `local_track.inc`（Media Foundation 解码 WAV/MP3/M4A/AAC/FLAC/WMA），
  导出 `BetterEndfield_GetLocalMusicApiV1`（`native/shared/include/BetterEndfield/LocalMusic.h`）。
  本地曲目优先于 OmniMix，播放时让出 OmniMix、保持原生 BGM 暂停，结束后恢复。相机模块用 `mmd_music_enabled` 联动。
- [x] 悬浮窗 `BetterEndfield.MmdOverlay.exe`：由相机模块在 MMD 开启时拉起；可点击但不激活窗口；
  共享内存双向协议见 `mmd_overlay_protocol.h`；小键盘 `-` 显示/隐藏；已加入打包脚本。
- [x] 作品库：`mmd_library.h`（原生）与 `Services/MmdLibraryService.cs`（管理器）读写同一格式；
  管理器相机页新增 MMD 区块：导入作品（选动作/镜头/表情/音乐，复制进库）、音乐偏移、循环、悬浮窗开关、键位。
- 构建：Host/Actions/Camera/Music/MmdOverlay 用 MSVC 编译 0 错误；`camera_playback` 7 个测试全过；
  管理器 0 错误（`global.json` 临时改 9.0.308 后已恢复）。构建目录 `F:/zmd_bem/tmp/pcmotion/native`、`tests`。
- 未提交 git，未实机测试。

### 联调清单

1. 四个文件一起替换：`runtime\BetterEndfield.Host.dll`、`modules\BetterEndfield.Actions.dll`、`BetterEndfield.Camera.dll`、`BetterEndfield.Music.dll`，
   再把 `modules\BetterEndfield.MmdOverlay.exe` 放在 Camera DLL 旁边。
2. 在管理器里导入一个作品，确认 `<安装目录>\mmd\` 下生成文件夹和 `set.ini`。
3. 进游戏后按小键盘 `-` 打开悬浮窗；按 Alt 放出光标后点作品，确认游戏窗口不失焦。
4. 小键盘回车播放：动作、镜头、音乐是否同时开始；4/6 跳转后三者是否对齐；NumLock 关着时是否仍然有效。
5. 播放音乐时 OmniMix 是否让出、结束后是否恢复；日志搜 `MMD`、`Local music`。
6. 身体动作仍是预览版（相对叠加、无腿部 IK/SMC 面部/布料），效果不对属于已知范围。

## 2026-09-29 决策（第三轮）：身体动作改以 Endfield-Poser 为参考

来源：https://github.com/OedoSoldier/Endfield-Poser（adeedaf，AGPL-3.0；上游 honxi1/Endfield-Poser，部分思路源自 EIEM）。
源码只读克隆在 `tmp_analysis/poser/`，未运行其中任何程序。

- 理由：重定向核心约 2500 行纯数学头文件（`math/mmd_rig.h`、`mmd_retarget.h`、`mmd_motion.h`、`ik_two_bone.h`、
  `mmd_amplitude.h`、`mmd_avatar.h`、`mmd_squad.h`、`mmd_terrain.h`），不碰游戏接口，可单测；
  Avatar 元数据自动适配（无需 T 姿）；两骨腿部 IK；多人同台；仍在维护。
- 做法：数学头文件移植到 `native/shared/motion/poser/`（保留署名，包进 `mmd` 命名空间）；
  游戏侧用我们的 `resolve_method/resolve_field` 重写，替换现有“相对叠加预览”。
- 播放期间停用写骨骼的组件（Animator、BipedIK、GrounderBipedIK、LookAtComponent、TransformFollowDamper、AnimatorMono），
  每帧写整套局部旋转和根骨骼世界位姿；停止时恢复全部骨骼、根和组件开关。头发/衣物/尾巴模拟保持开启。
- 多人同台：读游戏小队（`GameInstance.get_player` → `GamePlayer.squadManager` → `SquadManager.GetMemberBySlot`），
  只驱动已在场的 1–4 名队员，不生成角色；原点是开始播放时的操控角色；共用时间轴、镜头、音乐。
- 不搬：Poser 的衣物增强（数万行生成数据）、ImGui 面板、手动 T 姿校准的保存文件、网页服务。

施工顺序：
- [ ] P1 移植数学头文件 + 单测
- [ ] P2 单人身体：Avatar 适配、停用写入组件、FK + 腿部 IK、根位移、动作幅度、IK 模式
- [ ] P3 多人同台：小队读取、站位、共用时间轴；作品 `set.ini` 支持 `motion2..motion4`
- [ ] P4 地形跟随、镜头按身高适配
- [ ] P5 SMC 面部（Poser `smc_morph.h`）、布料冻结开关

## 2026-09-29 决策（第四轮）：身体动作按方案 A，以 EIEM 为主

- 用户更认可 EIEM 的身体动作效果，改为完整移植 EIEM DirectVmd（`tmp_analysis/eiem_git`，1bc9baa，2026-09-29）：
  源骨架/準標準骨/PMX 参考、FK、扭转骨分配、FinalIK 腿部驱动、膝盖弯曲混合、地形跟随。
- 更正：EIEM 仍在维护（6 月至今 45 提交）；按名字挂的钩子约 16 个，不是 60 个。
- 移植规则：删除全部写死 RVA / 字段偏移兜底（`0x035BD200`、`0x0326A380`、`0x032759E0`、`currentFloor 0x2e8` 等），
  名字解析失败就关闭该功能并写日志；所有钩子经 Host `create_hook`。
- 多人同台：把 EIEM 的单角色全局状态改为每角色一份（钩子按 `self` 实例分派到角色）；
  读取小队、站位、共用原点参考 Poser `game/squad.h`、`math/mmd_squad.h`。
- 第三轮的 Poser 施工清单（P1–P5）作废，Poser 只作为多人部分的参考。

### 第四轮进度（2026-09-29）

- [x] A1 单人 EIEM DirectVmd 接入相机模块
  - `native/modules/camera/eiem/upstream/`：EIEM 1bc9baa 源码快照；本地补丁见 `eiem/UPSTREAM.md`（`BE-PATCH` 标记）。
  - `eiem/eiem_body.cpp`：替代 EIEM 的 init/trojan/gui。按名字解析 IL2CPP（无 RVA、无偏移兜底），
    钩子经 Host：`SolverManager.LateUpdate`、`BipedIK.UpdateSolver`、`IKSolverTrigonometric.OnUpdate`（必需），
    `GrounderBipedIK.OnSolverUpdate/OnPostSolverUpdate`（地形用，可缺）。
    采样线程跟随 MMD 导演时钟：时间轴跳变→EIEM seek，循环回绕→loopCycle+1。
  - 单独的 OBJECT 库 `BetterEndfield.CameraEiem`（`/Zc:char8_t-`、无 UNICODE/NOMINMAX，按 EIEM 原编译方式），链接 winmm。
  - `character_motion_runtime.inc` 改为 EIEM 适配层：选操控角色、取唯一 Animator、`Entity.get_movementComponent`，
    持有姿态租约（与 Actions 互斥），把导演或本地时钟喂给 EIEM；导演接口（phase/duration/load/stop）不变。
  - 管理器：“眼睛”开关改为“地形贴合（实验）”（`vmd_terrain_enabled`）；“动作强度”改为“位移幅度（%）”
    （`vmd_motion_scale`，0.05–5，乘在 EIEM 按腿长自动缩放之上）。眼睛随身体动作一起由 EIEM 驱动。
  - 验证：原生全量构建通过；相机测试 7 套全部通过（角色适配层测试重写为 EIEM 替身，72 项）；管理器构建通过。
- [x] A2 表情：`SkeletalMorphCore.Update` + 两个 MorphToBone job 钩子，只在 SMC 全部所需字段按名字解析成功时安装。
  作品的 face VMD 通过 `DirectVmdRuntime_RequestMorphOverride` 覆盖动作文件里的表情。
- [x] A3 多人同台
  - EIEM 状态按角色复制：`eiem_slot.inc` 编译 4 份（各自命名空间），每份有自己的鬼影骨架、采样线程、动作、表情缓存；
    `eiem_body.cpp` 每个函数只挂一次，回调分给各份，EIEM 自己的归属检查保证只有驱动该角色的那份动手。
  - 表情：SMC 按第一根面部骨是否在该角色骨架下归属（替代 EIEM“第一个见到的 SMC”规则，避免锁到别人脸上）。
  - 小队读取参考 Poser：`GameInstance.get_player` → `GamePlayer.squadManager` → `SquadManager.GetMemberBySlot`；
    第 1 人为操控角色，第 2–4 人按小队顺序取场上队员（跳过被其他模块占用或骨架不合格的）。
  - 站位：多人时所有人以第 1 人开播时的根骨骼位置/朝向为舞台原点，不做首帧对齐（保留 VMD 里的队形）；单人仍用 EIEM 首帧对齐。
  - 队员中途离场只停该队员；操控角色切换则全部停止。
  - 作品 `set.ini` 支持 `motion2..motion4`、`face2..face4`；管理器导入对话框新增“多人同台（可选）”折叠区，列表显示“N 人动作”。
  - 验证：原生全量构建通过；相机测试 7 套全部通过（角色适配层 101 项含小队场景；作品库解析 8 项）；管理器构建通过。
- [x] 镜头按身高适配（照 EIEM `camera_control.h`）：MMD 播放且身体由 EIEM 驱动时，VMD 镜头跟随 EIEM 的舞台锚点
  （含首帧对齐与地形偏移）；镜头取自动作文件时用身体的动作缩放，单独的镜头 VMD 用 0.07×身高/1.245 m；
  `vmd_camera_scale` 变为相对 0.07 的倍率。测试 8 项。
- [ ] 未做：EIEM 布料增强（数万行，游戏自身的头发/衣物物理照常运行）；PMX 参考骨架与 A/T 预设的界面入口。
- [ ] 联调项（多人）
  - 队员被 AI 跟随/移动组件拉回的情况（Poser 会停用 TransformFollowDamper 等写入组件，我们暂未停用）。
  - 多个鬼影骨架同时存在时的性能；`BetterEndfield.EiemBody.2.log`…`.4.log` 分别对应第 2–4 人。
- [ ] 联调项（新增）
  - 进游戏后播放，看 `<安装目录>\BetterEndfield.EiemBody.log`（有 `logs\` 目录时在其中）的 `[BE-RESOLVE]` 一行：
    `core=1 finalIkSolver=1` 是身体动作的前提；`grounder/floorLayout` 为 0 时地形贴合不可用。
  - 冻结（8 键，timeScale=0）时 FinalIK 的 `SolverManager.LateUpdate` 是否仍执行——决定暂停画面时姿势是否保持。
  - 与 Actions 冲刺姿态互斥：冲刺中按播放应提示等待。
