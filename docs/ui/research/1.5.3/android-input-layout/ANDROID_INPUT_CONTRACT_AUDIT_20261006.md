# Android PCUI 输入契约审计（2026-10-06）

Android 切换 PCUI 原先只选择 Keyboard 输入类型，没有接通系统捕获式相对鼠标输入。对于“只能转有限角度、无法 360°”的反馈，代码缺口与屏幕边缘限位相符。现已实现捕获、游戏鼠标轴消费与退出恢复；当前完成静态取证、宿主回归和编译验证，尚无真机 360° 或灵敏度验收。

## Android 样本及契约

本轮读取的是仓库既有 Android 1.5.3 样本，路径中的 `Windows` 表示研究工作环境，文件本身为 Android 元数据/ARM64 ELF：

- `research/android/1.5.3/Windows/android-153/global-metadata.dat`、`strings.txt`。
- `research/android/1.5.3/Windows/android-refactor/engine/libil2cpp.so`。
- 同目录 `libunity.so`，包含版本字符串 `2021.3.34f5`。

下表 RVA 只用于复核样本，不进入生产实现。生产代码按程序集、类型、方法和参数解析；没有移植 PC RVA。

| Android 托管契约 | 样本证据 |
| --- | --- |
| `Input.Beyond.dll / Beyond.Input.RealCursorManager._ToggleCursorInternal(enable, forceUpdate)` | 方法索引 420934、token `0x06000286`、Android RVA `0x1581F2E0`。原生正常分支比较可见状态，调用 `Cursor.set_visible(enable)`，并依既有虚拟鼠标条件调用 `Cursor.set_lockState(!enable)`。 |
| `RealCursorManager.CalcState(forceUpdate)` | 方法索引 420932、token `0x06000284`，可通过游戏自己的请求优先级重新计算显示/隐藏意图。 |
| `Beyond.Input.InputManager.m_realCursorManager` | 字段索引 272783，引用类型索引 142723。初始化已错过光标事件时，可从 `GetAxis` 的实例反射取此字段，调用 `CalcState(true)`。 |
| `InputManager.GetAxis(string name) → float` | 方法索引 420704、token `0x060001A0`、Android RVA `0x157E8EE4`，正常分支调用 `Rewired.ReInput.players.GetPlayer(0).GetAxis(name)`。 |

Android `Rewired_Core.dll` 中嵌套 `Mouse.Update`（token `0x06000267`、RVA `0x17D9D5F8`）循环读取前三个轴，调用 `UnityEngine.Input::GetAxisRaw(System.String)`，把返回的 float 写入缓存。`Mouse.GetAxisRaw(int)`（token `0x0600026A`、RVA `0x17D9E1F8`）读取该缓存。由此可确认上游含 Unity legacy 轴值和 Rewired 映射，不能把原 `InputManager.GetAxis` 直接等同于鼠标像素位移。

当前捕获实现把相对 float 位移供给游戏 `Mouse X / Mouse Y` 消费，保留下游现有 Lua/相机增益；没有证据证明 Unity 输入设置和 Rewired 映射的灵敏度/校准系数都为 1，因此不能声称所有原始灵敏度被完整保留。实际比例、转向方向和游戏手感仍需真机测量。既有 `LevelCameraCtrl.lua` 的消费链是本机 Windows Lua 样本；Android 实际热更新分支仍需运行日志核实。

详细只读取证保存在 `build/pcui-layout-audit/input-contract/contract-evidence.txt` 和 `rewired-mouse-update.asm`，未计算产物哈希。

## Java 捕获与恢复

[XposedEntry.java](../../../../../android/app/src/main/java/dev/betterendfield/android/XposedEntry.java) 在 `View`、`ViewGroup` 的捕获事件分发上挂定向钩子，同时观察捕获与窗口焦点回调；没有替换游戏事件 listener。[PcUiMouseBridge.java](../../../../../android/app/src/main/java/dev/betterendfield/android/PcUiMouseBridge.java) 仅在 native 消费契约就绪、PCUI 请求捕获、Unity 持有焦点且有鼠标设备时请求 capture，已有游戏自有 capture 继续原流程。

捕获后的 `getX/getY` 及历史样本是相对位移，只送入 native float 队列。按键和滚轮事件复制后保留原元数据，以普通 `SOURCE_MOUSE` 和 Unity 视图中心坐标调用运行时查得的 `UnityPlayer.injectEvent(InputEvent)`；不会把相对坐标原样送入 Unity 绝对坐标路径。缺少该入口或拒绝输入时恢复原输入流程。

菜单、配置关闭、失焦和后台都会释放捕获及游戏中仍按住的鼠标键。硬错误设置当前 Unity 实例的 `transportFailed`，避免每 50 ms 重新抢占；同 Activity 暂停/恢复不会清除此状态，仅新的 Unity 实例或新的 Activity 会话重试。尚未实际获授的请求取消时立即清 ownership，不依赖可能没有的 `capture=false` 回调；已获授后异步释放期间继续抑制尾部相对事件，直到实际捕获结束。实际 capture 采用幂等重确认，覆盖 native 已看到短菜单切换、Java 尚未轮询的时序窗口。

[Android 官方捕获文档](https://developer.android.com/develop/ui/views/touch-and-input/gestures/movement#use-pointer-capture) 说明相对坐标及焦点丢失行为。[AOSP View](https://github.com/aosp-mirror/platform_frameworks_base/blob/master/core/java/android/view/View.java) 和 [ViewGroup](https://github.com/aosp-mirror/platform_frameworks_base/blob/master/core/java/android/view/ViewGroup.java) 支持上述分发入口选择。[Unity 的 Android 边缘鼠标问题](https://issuetracker.unity.com/issues/13143) 标题与症状相符，但当前页面正文不可读，不能把其搜索索引中的历史解释当成此游戏当前客户端的实测事实。

## Controller 的只读结论

Android 元数据的 `InputType` 枚举包含 `Keyboard / Touch / Controller / Max`，字段默认数据为紧邻的压缩 int 字节 `00 / 02 / 04 / 06`，解码得到 `0 / 1 / 2 / 3`；[Il2CppDumper 压缩 int 实现](https://github.com/Perfare/Il2CppDumper/blob/master/Il2CppDumper/Extensions/BinaryReaderExtensions.cs) 可用于复核。因此 Android 的 Controller 值确为 2。

`Common.Beyond.dll / Beyond.LocalDeviceInfoProvider.SupportsInputType` 在该 Android 样本的方法索引为 244221、token `0x06000160`、RVA `0xADCB4D8`。正常分支于 `0xADCB590` 比较输入类型 2，于 `0xADCB598` 直接返回 true；初始输入类型仍为 Touch=1。这里有接受 Controller 请求的客户端代码证据，但 IFix 可以替换方法，连接设备、Rewired joystick 映射、按钮提示和实际操作尚未验收。本轮没有新增手柄映射或修改输入模式配置；现有 PCUI 的 Keyboard 覆盖行为仍需独立设计。

## 验证

- 真实主代码 `PcUiMouseBridge` 的 **39 项宿主检查通过**，覆盖捕获授予/拒绝、历史与小数位移、按钮/滚轮元数据、菜单/失焦/后台恢复、短菜单重确认、硬失败熔断、未授 ownership 清理及新 Unity 实例重试。
- `PcUiMouseBridge.java` 和 JNI 声明已通过 Android 37.0 SDK 的独立 `javac` 编译。
- 回归入口：`python android/app/src/testHost/pcui_mouse_fixture.py --output build/pcui-layout-audit/input-contract/host-fixture`。
- 完整 APK、原签名策略和 Windows 构建结果见本轮集成报告；本报告不代替真机视觉和输入验收。
