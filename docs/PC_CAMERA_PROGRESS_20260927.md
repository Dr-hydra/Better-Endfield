# PC 相机继续开发：共享 VMD 与机位文件（2026-09-27）

> 历史记录：本文记录阶段 2 / 机位文件交付时的范围和测试数量。后续阶段 3/4 预览、
> 7 项测试及当前 PC 分支请以 `PC_CHARACTER_PREVIEW_20260927.md` 为准。

## 基线与范围

基于 `main@9b1e89599b1d3fbdbf1bcccd2d1b3c48340125c0`；本地工作分支
`dev/pc-camera-paths-vmd-20260927`。本轮不修改 Android 文件、BEM 导入或 CI，
也不包含之前的安卓补丁。相机的共享源文件有改动，和安卓分支合并时仍需处理冲突。
交付为针对上述 main 的独立补丁，不代表已经推送到 GitHub。

当前 main 的计划只记录阶段 1 已实现，阶段 2–4 未完成。之前对话所提的 PC
开发分支未在本次远端分支列表中出现，因此不能将旧对话的完成声明视为当前代码。
本轮补齐阶段 2 和阶段 1d 的文件功能，不把身体/FinalIK/布料标记为已完成。

## 参考源码

已通过 GitHub 连接器读取实际源码，而非依据记忆重建接口：

- 仓库：https://github.com/Sasye/EIEM
- 本轮参考提交：`447600b9de959d8b709ad84b19e605eeb8a2a0a9`
- `src/vmd_parser.h`：结构、旧/新版本头、骨骼/表情/相机/灯光/阴影/IK 段。
- `src/camera_player.h`：相机六条曲线、Euler 符号、机位转换。

容器直接 git clone 因网络 DNS 不可用未完成；采用连接器读取参考文件，未运行
参考项目的程序或构建脚本，未将整套参考实现或资源包复制进本仓库。
`vmd.h` 保留 AGPL-3.0 来源声明，第三方声明已更新。

注意：旧 VmdBezier 的参数顺序是 x1,x2,y1,y2；EIEM 的函数签名是 x1,y1,x2,y2。
两者调用点与签名配套，原实现不因此构成字节顺序错误。本轮增加非对称曲线测试，
不是以这个误判为修复依据。

## 已实现

### 共享解析与采样

`native/shared/motion/vmd.h` 不依赖游戏、Win32、文件系统或结构体打包布局。
解析骨骼、Morph、相机、灯光、自阴影、可见性和 IK 开关；骨骼/表情/IK 名称
保留 CP932 原始字节，不因有损转码将不同轨道合并。此层尚不负责 UTF-8 显示或
游戏骨骼名称映射。

解析支持旧/新 VMD 头、完整段边界处省略尾段，拒绝半截计数、截断记录、非法
浮点数、超量轨道、非法布尔/插值/FOV 及未知尾部数据。重复键稳定排序并采用
源文件最后一条；零四元数替换成单位四元数并计数。失败不覆盖调用方现有数据。

默认策略：文件 64 MiB；骨骼/Morph 各 50 万键；相机 25 万键；辅助段各 10 万键；
每组最多 4096 条命名轨道；IK 每帧 256、总计 50 万；最长 24 小时（30 fps）。
这些是可调整的解析策略，不是声称 VMD 格式本身只有这些上限。

采样提供骨骼 XYZ 分通道 Bezier + 四元数 slerp、Morph 线性、IK/可见性阶跃，
相机六曲线与相邻帧切镜。相机适配器仍只支持透视：文件含正交键时明确拒绝，
不把正交状态静默丢掉。身体数据可被共享解析器读取，不代表已能在游戏中跳舞。

### 关键帧保存/读取

新增版本化 `.becamera` 文本，记录位置、四元数、FOV、统一片段时长与循环标志。
支持最多 64 个关键帧，文件上限 64 KiB；读取完整校验后才替换当前路径。
保存使用同目录独占临时文件，写完后原子替换目标文件；父目录需要事先存在。

```ini
; 加到现有 [betterendfield.camera] 配置节，修改后重新应用配置/重启游戏。
keyframe_file=C:/Users/YourName/Documents/shot.becamera
keyframe_save_hotkey=F6
keyframe_load_hotkey=F7
```

沿用 Numpad0 录制、Numpad2 回放、Numpad4 清空、Numpad6 VMD 播放/停止。
F6 保存当前已录制路径；F7 读取，不自动开始回放。已有键位均可继续配置。
路径留空时明确提示配置路径，不会默认往游戏目录写文件。

管理器 `FreeCameraExtras` 增加了路径及两项热键的读取/序列化字段；本轮未增加
MainWindow 文件选择按钮，也未构建管理器。新路径入口目前为上述 INI 设置。
读取文件的片段时长与循环选项在运行时生效；之后再应用管理器配置会覆盖它们。

### 回调、文件任务与故障行为

- 单个可停止并 join 的工作线程承担 VMD/路径磁盘 I/O 和解析，不调用 Unity API。
- 队列只容纳一项任务/结果；忙时明确拒绝新请求，不无限积压文件或偷偷丢保存。
- 路径、配置版本一起快照；结果经过会话和配置版本检查，退出、清空、切换播放
  或配置更新之后的迟到加载结果不会覆盖当前状态。
- 加载失败保留已有路径与播放；成功后才切换。VMD 加载中再次按播放键可取消
  结果采用；取消不会强行终止 OS 文件调用。
- 回放使用不可变路径快照；清空先停止，录制期间编辑也停止原路径回放。
- 修复 VMD 跨越结束时间时没采样最后一帧的问题；单键 VMD 正确呈现后停止。
- 文件数据转换成世界坐标后再次检查有限值，拒绝将溢出机位写给引擎。
- 功能关闭时清空待处理热键请求，避免之后重新启用时意外保存或载入。

## 测试与构建

独立测试项目（不需要创建 CI）：

```powershell
cmake -S native/tests/camera_playback -B build/camera-playback-tests -A x64
cmake --build build/camera-playback-tests --config Release
ctest --test-dir build/camera-playback-tests -C Release --output-on-failure

cmake -S native -B build/pc-camera -A x64
cmake --build build/pc-camera --config Release --target BetterEndfield.Camera
```

本环境实际完成的是 Linux 主机验证：

- Clang Debug + AddressSanitizer/UndefinedBehaviorSanitizer：3/3 测试通过。
- GCC Release：3/3 测试通过。
- VMD：1118 项断言以及 3000 份固定种子的畸形数据变异输入。
- 路径/真实主机文件系统/工作线程：49 项断言。
- 包含生产 `module.cpp` 的回调集成测试：28 项断言。

非 Windows 的集成测试使用专用 Win32 编译替身，不链接/调用游戏。替身目录只给
测试目标，不进入生产模块。以上不代表 MSVC/Windows SDK DLL 构建、管理器编译、
真实鼠标 Hook、Unity/IL2CPP 或游戏内验收已完成。Windows 原子替换路径也需要在
Windows 测试；不能用 Linux rename 测试代替它。

## 剩余事项

1. Windows 编译、机位录制/保存/读取、冻结回放和场景切换实测；管理器文件选择 UI。
2. 阶段 3：动态骨骼映射、动作所有权与 actions 协调、躯干 FK、手臂/手指/Twist、
   FinalIK、地形吸附；身体驱动与相机同一时钟/中心位移补偿。
3. 阶段 4：动态面部表情、眼睛、裙子/布料生命周期。
4. VMD 视角的身高适配、暂停/拖动 UI、正交相机支持尚未在本轮实现。

现有 PC 相机的游戏线程亲和性、卸载时机、所有 Hook 的实际触发频率并未在本轮
全面重构。文件工作线程在退出时等待已经开始的 OS I/O 结束；文件大小上限不是
网络磁盘超时保证。测试优先使用本地普通文件。
