# Android PC UI 鼠标边缘限位排查与相对输入接入（2026-10-06）

反馈来自 [issue #23](https://github.com/Dr-hydra/Better-Endfield/issues/23)：切到 PC UI 后鼠标只能转动一定视角，无法连续转满 360°。症状对应绝对光标到达屏幕边缘后的移动限位；本轮尚无连接的 Android 设备，不能把该判断写成当前真机日志已经验证的结论。

## 已核验的游戏契约

- 原 UI 模块只将 `DeviceInfo.inputType` 切到 `Keyboard`，没有相对鼠标事件桥。Android 的 `touch_input_android.cpp` 也是不做转换的 stand-in。此前 2026-10-01 的修复解决输入类型漂移，不包含鼠标捕获。
- 本地 Android 1.5.3 `global-metadata.dat` 确认 `Input.Beyond.dll / Beyond.Input.RealCursorManager._ToggleCursorInternal(bool enable, bool forceUpdate)`。ARM64 方法体调用 Unity 的 `Cursor.get_visible/set_visible/set_lockState`，隐藏光标时向引擎请求 `lockState = 1`。这个请求不能单独证明 Android 正在提供无屏幕边界的相对事件。
- 同一 Android metadata/方法体确认 `InputManager.GetAxis(string name) -> float`，其正常路径是 `Rewired.ReInput.players.GetPlayer(0).GetAxis(name)`。确认 `InputManager.m_realCursorManager` 引用字段及 `RealCursorManager.CalcState(bool forceUpdate)`。
- 当前 1.5.3 Lua `UI/Panels/LevelCamera/LevelCameraCtrl.lua:208`、`:209` 直接消费 `GetAxis("Mouse X")`、`GetAxis("Mouse Y")`，保留原 `0.9`/`0.63` 乘数后，经 `_MoveCamera` 和 `UIUtils.getNormalizedScreenX/Y` 进入 `CameraManager.OnInput`。`_MoveCamera` 和 `UIManager` 还会重复读取同一轴，因此不能在每次 getter 调用时清空该轴。

这里的 Lua 证据来自仓库 Windows 1.5.3 的脚本快照，Android 自身的 C++ 方法签名/调用链来自 Android metadata 与 ELF。代码按同名游戏轴接入；没有伪造不同平台的相机倍率。

## Native 实施

1. Android-only 可选钩子观察原 `RealCursorManager` 隐藏/显示光标的最终请求，始终调用原方法。只有 PC UI 开启、两个 native 钩子就绪、前台且游戏请求隐藏光标时，Java 才能收到捕获请求；菜单显示光标时立即使相对输入失效。
2. 模块晚于原 Cursor 初始化附着时，在第一次真实 `GetAxis` 鼠标轴调用上，通过反射字段取真实 `m_realCursorManager` 并调用一次原 `CalcState(true)`。该路径遵循游戏原有的请求优先级，不由模块猜测当前菜单状态；字段尚未可读时最多每 250 ms 重试，取得意图后停止。
3. 三个 JNI 入口为 `pcMouseCaptureRequested(): boolean`、`pcMouseCaptured(boolean)`、`pcMouseMotion(float,float)`。捕获请求与系统实际授予捕获分开，未授予捕获时全部回退游戏原 `GetAxis`。捕获确认幂等，Java 可重复确认真实状态而不清空移动；这样短于轮询间隔的菜单/配置往返也能重新同步 native 捕获状态。
4. `AndroidPcMouseState` 用互斥锁接收 Java UI 线程的 float 相对移动，在每个 nativeRender 帧代内保留一份快照；同帧多次 X/Y 查询得到相同值，晚到事件保留给下一帧。不会把输入限制到 Screen 范围；菜单、失焦、关闭配置、捕获丢失及模块 Shutdown 清空待处理和快照移动。
5. 仅在实际捕获成立时覆盖 `Mouse X`/`Mouse Y`，Android Y 向下的坐标转成游戏轴的 Y 向上；其他轴与未捕获状态调用原函数。下游 Lua/相机倍率和控制流程保持原路径。

直接在 named-axis 边界提供相对像素，未复刻 Rewired 的设备级校准或定制轴映射；不能宣称 Rewired 的任意自定义 sensitivity/invert/calibration 都等价保留。真机需同时验收转速与方向。此修复没有猜测一个补偿倍率。

Java 的 Unity 目标 View 选择、系统 Pointer Capture 生命周期和按钮/滚轮向原 Unity 输入入口转送由并行输入审计实现，完整交付报告需结合该实现与整包构建结果。

## 诊断与验证

`AndroidUiFrame` 仅在 PC UI、diagnostics 及前台同时有效时每 250 ms 读取一次可选引擎状态；稳定状态每 5 秒最多记录一个摘要，光标 lock/visible 改变时记录新状态。摘要含实际 Cursor lock/visible、mousePosition、Screen 尺寸、边缘采样比例、legacy `Mouse X/Y` 的采样幅度，以及真实桥的 `relative_requested/captured/relative_events/axis_reads/relative_snapshot`。桥状态的观察不会消费待处理移动。legacy 轴不可用时只报告一次并停止重试该轴，继续光标诊断。

不能用引擎 `lock=0` 单独判断桥失效，也不能把四次每秒的 legacy 采样解释成总输入量。`captured=true`、事件计数增长与 `axis_reads` 增长可分别说明系统捕获、相对数据到达以及 named-axis 消费。

通过的检查：

- MSVC 宿主 fixture，真实包含生产 `module.cpp`：原布局漂移/恢复、诊断 gating/限频/边缘统计/缺合约/异常降级、原 Cursor 生命周期/首次 CalcState 懒刷新/实际捕获回退/named-axis 消费/重复读取/其他轴透传/Shutdown。
- `AndroidPcMouseState`：亚像素累计、同帧 X/Y 快照、超过屏幕尺寸的连续移动、不活动帧不漂移、异常 float 拒绝、菜单/失焦/关闭/捕获丢失清理。
- NDK `clang++ --target=aarch64-linux-android26`：生产 UI 模块、`android_frame.cpp` 和全部新增 fixture 的 ARM64 语法检查。

日志：`build/pcui-layout-audit/android/host-build.log`、`*-test.log`、`arm64-syntax.log`。Android 完整 APK/签名与 Windows native 兼容构建由主任务负责，不在这里重复声明结果。

没有运行真实 Android 游戏验收，没有重置 Mod/用户配置，没有修改正式 Release，没有计算产物哈希。真机优先检查主世界连续水平转 360°、上下方向及原设置速度、菜单打开/关闭、鼠标按钮/滚轮、切后台/恢复以及 PC UI 关闭后的正常原输入。

## 完整交付检查

Java `PcUiMouseBridge` 接入系统 Pointer Capture，由 Xposed 对目标 Unity View 的 captured-event 分派做拦截，不替换游戏已有 listener。仅已就绪的 native 消费者请求捕获时启用；相对运动交给 Mouse X/Y 桥，鼠标按钮与滚轮保留事件元数据、转换为普通鼠标来源和视图中心坐标，经原 `UnityPlayer.injectEvent` 转送。未被本桥持有的游戏自有捕获保留原处理。

独立复核修复了两项回退缺口：Unity 注入拒绝或输入通道硬错误后，不在下一轮 poll 重新抢占捕获；未获授的请求取消时立即清 ownership，不依赖可能不会到来的 false 回调。已实际获授的捕获释放期间仍抑制尾部相对事件，避免送入绝对坐标路径。原生捕获确认幂等；短于 poll 周期的菜单切换可重新确认状态而不清掉同帧有效移动。

最终验证：

- Java 实际生产 helper 的宿主 fixture 39 项通过，包含失败熔断、历史相对事件、按钮/滚轮转送、菜单/前后台、未获授取消和游戏自有捕获；Android 37 API jar 编译通过。
- Windows `BetterEndfield.UiModule` Release 构建通过，日志 `build/pcui-layout-audit/windows-ui-build.log`。
- Android 最终 `assembleRelease` 成功，日志 `build/pcui-layout-audit/android-release.log`。原正式签名政策验证通过；APK Signature Scheme v2 校验通过，日志 `apk-verify.log`。
- 测试 APK：`build/pcui-layout-audit/BetterEndfield-3.5.1-PCUI-test-Android-arm64.apk`，应用版本仍为 3.5.1。APK 未安装到手机，正式 releases 未替换。
- 本机 BetterEndfield.ini、ui-settings.json 与 custom-model/runtime.ini 对检查前副本逐字节一致，没有恢复或重置配置。

手柄问题、Android 方法来源及 Rewired 原始轴单位边界见 [输入契约审计](ANDROID_INPUT_CONTRACT_AUDIT_20261006.md)。此次没有增加手柄模式选择或手柄到键鼠映射。
