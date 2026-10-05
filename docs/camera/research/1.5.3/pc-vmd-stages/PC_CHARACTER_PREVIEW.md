# PC 阶段 3/4：角色动作预览与写入所有权（2026-09-27）

## 本轮交付边界

基于 main `9b1e89599b1d3fbdbf1bcccd2d1b3c48340125c0`，包含上一轮阶段 2 / 机位文件改动。
远端目标分支 `dev/pc-camera-motion-20260927`。没有包含安卓/BEM 补丁，也没有新增 CI。
这是阶段 3a/3b 和阶段 4 的**可测试预览切片**，不是完整 MMD 播放器，更不是游戏内验收结果。

参考 GitHub `Sasye/EIEM` 的 `8b46b76b33e3f61825b6b8c55d983bcf2c1bc94c`：
`src/bone_map.h`、`src/smc_face.h`、`src/cloth.h`。通过 GitHub 连接器读取实际源码；
本地网络不能解析 github.com，未完成 git clone。未运行参考项目的 DLL/EXE/构建脚本。
保留了语义名称映射的 AGPL-3.0 出处，没有复制 HumanBone 数字常量、SMC 哈希或内存偏移。

## 实际实现

### 阶段 3：相对 FK 预览

- 沿当前角色模型的 Transform 层级查找语义名称；重复叶名称拒绝自动选择。
  一次绑定最多遍历 4096 个节点、深度 64；仅允许当前模型下恰好一个 Animator。
- 46 个候选映射：躯干/头颈/肩臂/手腕、30 个手指和两只眼睛；原始 CP932 轨道名保持无损。
  身体模式至少找到 4 个身体轨道才启用。缺失轨道不猜地址、不使用枚举序号碰运气。
- **相对预览，不是真正的 bind-pose 重定向**：把源文件第 0 帧校准到当前捕获的角色姿态，
  后续旋转经局部轴基变换应用。每帧使用固定参考姿态，不在上一帧输出上累加。
  当前姿态不是 T-pose；源动作和目标参考姿态不同，结果可能不符合原舞蹈，必须实测校准。
- 不写角色控制器、不替换 Animator、不写世界根节点或根位移。不包含腿部 FK/IK、Twist 分配、
  地形吸附或准标准骨完整语义。中心位置补偿和身体/镜头共用时钟尚未实现。
- F8 异步加载/播放，再按停止；F9 暂停/继续；F10 停止。热键可配置。
  停止、切角色、模型层级变化、配置更新和失去前台都会取消会话/迟到文件结果。
- 使用系统单调时钟和帧计数去重。正常入口为 CameraManager.TailLateTick；
  unscaledDeltaTime 仅在**已由 TailLateTick 确认的同一线程**上提供补充泵。
  不从任意 timer/input 线程调用 Unity。若游戏停止所有入口，只能等安全入口恢复；
  不声称已验证所有冻结/暂停场景。

### 与 Actions 的写入协调

Host 新增命名能力 `BetterEndfield_GetPoseLeaseApiV1`，不扩展 `BE_HostApiV1` 的布局。
注册表只存在于 Host 中，以 Animator.transform 为键，用递增 token 拒绝重复、外来和过期释放。
Actions 的骨骼覆盖在取得租约后才允许运行；Camera 检查 Actions 的版本握手，遇到旧 DLL 拒绝
启动身体预览。先占有者直到结束前不会被另一个写入者抢占。冲刺骨骼覆盖不可用时保留原生冲刺逻辑。

**Host、Actions、Camera 三个 DLL 必须一起更新**。旧 Host 没有该能力时，新的预览拒绝启动，
PC 的可选冲刺骨骼覆盖也拒绝启动，不默默退回两个模块同时写骨骼的行为。
该机制只协调参与能力协议的两个模块，不保证与任意第三方 mod 或全部游戏动画后处理共存。

### 阶段 4：眼睛与通用 BlendShape 适配

- `両目` 与左右眼轨道可组合到 `eyeLfJoint` / `eyeRtJoint`；仍属于上述相对旋转预览。
- 按名称查找标准 SkinnedMeshRenderer BlendShape，支持已映射的五元音、眨眼和笑眼通道。
  多个眨眼来源采用 max 合成而非叠加到超过上限；VMD 权重钳制到 0..1 后映射到 0..100。
- 保存 renderer/mesh 身份、原权重和最后写入值；换 Mesh、销毁、重挂层级时停止。
  恢复时只恢复仍等于本会话最后输出的值，不覆盖别的写入者已改变的数据。
- **游戏专用 SMC 面部还没实现。** 如果模型没有对应标准 BlendShape，会明确记录不支持；
  不能据此认为口型/眨眼已经在原版角色上生效。参考项目的 SMC 私有数组、固定哈希路径未复制。
- **裙子/布料适配还没实现。** 不把普通物理 Collider 当成布料碰撞体去缩放，不猜私有布局。

## 使用配置

在现有 `[betterendfield.camera]` 节中添加，配置路径使用本地普通文件，重启或重新应用配置：

```ini
enabled=true
vmd_motion_file=C:/Users/YourName/Documents/dance.vmd
vmd_body_enabled=true
vmd_eyes_enabled=true
vmd_face_enabled=false
vmd_motion_weight=0.5
vmd_motion_loop=false
vmd_motion_hotkey=F8
vmd_motion_pause_hotkey=F9
vmd_motion_stop_hotkey=F10
```

这些新增开关默认全关。初测在角色静止、没有持续冲刺、没有操作其他骨骼写入插件时按 F8。
`0.5` 是建议初测的混合权重，不是动作的物理参数。按 F10 退出。
只看眼睛/标准表情时可关闭 body，单独启用 eyes/face；无任何匹配通道会拒绝启动。
管理器配置模型已保留这些键，但没有新增完整身体播放面板；本轮入口仍是 INI + 热键。

## 本地验证

`native/tests/camera_playback` 现在有 7 个独立目标：原 VMD、机位文件、相机回调，以及新增的
角色数学/生命周期、生产角色适配器、生产 Host 租约 API、生产 Actions 租约守卫。

测试使用人工对象和主机平台替身，不加载/调用游戏：销毁、重挂层级、Mesh 更换、配置作废、
外来租约、过期恢复、回调重入、参考帧校准、非累积输出、混合权重、时钟暂停和结束边界。
骨骼/表情写入前先验证读取完整输出；中途失败只回滚仍归当前会话持有的通道。

```powershell
cmake -S native/tests/camera_playback -B build/camera-playback-tests -A x64
cmake --build build/camera-playback-tests --config Release
ctest --test-dir build/camera-playback-tests -C Release --output-on-failure

cmake -S native -B build/pc-camera -A x64
cmake --build build/pc-camera --config Release --target BetterEndfield.Host BetterEndfield.Actions BetterEndfield.Camera
```

此工作环境为 Linux：没有宣称 MSVC/Windows SDK 链接、WinUI 编译或游戏内通过。
Actions 的非 Windows 测试仅用其已有 Android Win32 替身和测试宏打开新增的 PC 租约守卫；
不是 Android 功能移植，也不是 Windows DLL 加载测试。GC pin、真实 Metadata 生命周期、
晚帧写入顺序、实际相机/人体轴向、SMC 面部和衣服需要在客户端验证。

进程动态卸载不在本轮保证范围；Host 仍为既有进程生命周期设计。退出程序由系统回收；
正常功能停用/场景变化在下一次安全游戏回调中恢复。网络磁盘 I/O 无强制超时保证。
