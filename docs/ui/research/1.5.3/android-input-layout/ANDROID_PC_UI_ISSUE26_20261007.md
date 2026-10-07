# Android PC 布局 issue #26：退出绑定、可见光标与菜单坐标（2026-10-07）

对应反馈：[安卓端 PC 布局的一些 BUG，issue #26](https://github.com/Dr-hydra/Better-Endfield/issues/26)。反馈包含 Esc 触发退出确认、工业模式按 Alt 后光标不可见，以及制造、寻访、简易制作、退出确认、理智药等菜单鼠标点击不稳定。

本次完成生产输入桥修正及宿主验证，未连接手机运行游戏。Esc 的 Android 专属绑定与原路径之间存在明确冲突条件；菜单坐标与 Android 指针图标补全符合反馈症状，但尚不能认定已经解释、消除了全部菜单故障。

## 已确认的契约

- 本机 1.5.3 Lua `MainHudCtrl.lua` 在 `DeviceInfo.isAndroid` 时额外注册 `common_quit_game`，回调显示退出确认。PC 布局只改变输入类型，保留 Android 平台身份，因此这条额外绑定依然存在。Lua 样本属于 Windows 工作环境导出，Android 热更新后的 Lua 是否一致需要现场确认。
- Android 1.5.3 元数据存在 `Input.Beyond.dll / Beyond.Input.InputBindingInfo.get_enabled()`，token `0x060000D1`、方法索引 420497；同类型含 `playerActionId` 字符串字段。生产代码按完整方法及字段类型解析，不使用样本地址或字段偏移。
- Android `RealCursorManager._ToggleCursorInternal(enable, forceUpdate)` 的 `enable` 是游戏计算后的真实显示意图；隐藏时请求锁定，显示时解除锁定。此前 Java 桥只据此处理捕获，未恢复 Unity 子视图的 Android `PointerIcon`。
- Android `InputManager.GetMousePos()` 的样本正常分支直接解析并调用 `UnityEngine.Input::get_mousePosition_Injected(UnityEngine.Vector3&)`，传入 `Vector3*`；不能仅挂钩托管 `Input.get_mousePosition_Injected` 包装方法并假设所有调用都会经过它。生产代码动态解析真实 icall，按 `void(Vector3*)` 接入共享 hook broker。
- 普通鼠标的 Android `MotionEvent` 是视图本地绝对坐标；捕获鼠标的 `SOURCE_MOUSE_RELATIVE` 是相对位移。Unity 子视图可能在分发中改变事件偏移，因此本次在 **Unity 根视图分发之前** 观察普通事件，按根视图实时宽高归一化；不从子视图或 `injectEvent` 推测根坐标。

本轮只读样本记录：`build/android-issues-25-26/issue26-contract-evidence.txt`。其中部分磁盘代码/元数据条目仍经过客户端变换，不能把未还原的方法体作为完整调用链证据。

### 退出绑定 getter 的真实消费者核验

为了排除“挂到的属性包装已经被键盘 checker 内联绕过”，本轮另扫描 `Input.Beyond.dll` 已注册方法中的 ARM64 直接调用，并核对调用后控制流。`InputBindingInfo.get_enabled` 的样本实际地址为 `0x158035D4`，找到 13 个直接 B/BL 消费点，包含以下键盘资格与优先级选择分支：

| 消费方法 | 样本地址与动作 | 返回 false 后行为 |
| --- | --- | --- |
| `KeyboardOnClickChecker.CheckKeyboardInput()`，token `0x0600022D` | `0x158152C4: bl 0x158035D4` | `0x158152C8: tbz w0, #0, 0x158152A0`，跳回候选遍历；只有通过资格检查才继续读取 binding 的 actionPriority 并比较。 |
| `KeyboardOnLongPressChecker.CheckKeyboardInput()`，token `0x06000234` | `0x158175EC: bl 0x158035D4` | `0x158175F0: tbz w0, #0, 0x158175C8`，同样跳过当前候选，再进行优先级选择。 |
| `InputManager.IsBindingEnabled(...)`，token `0x06000184` | `0x157E5E90: bl 0x158035D4` | 使用该 getter 的布尔结果返回。 |

这证明在当前 Android 样本已核验的点击与长按 checker 资格选择中，所挂 getter 是真实执行边界；返回 false 会在 action 竞争之前排除退出 binding，而不只是压掉已经选中的 callback。没有发现这两处被内联绕过的反证。其他时序类型、IFix 的现场替换以及未来游戏版本仍不能由本轮磁盘样本保证。

只读核验脚本与对应调用周围汇编：`build/android-issues-25-26/issue26_eligibility_consumers.py`、`issue26-eligibility-consumers.txt`。这些地址只作为样本证据，不进入生产配置或代码。

## 修正行为

1. PC 布局启用时，`InputBindingInfo.get_enabled()` 对 `playerActionId == common_quit_game` 返回 false。其余 action、触屏模式及关闭 PC 布局后的 Android 退出行为保留游戏原结果。不会猜测 Esc 键码、替换协议终端 action，或伪装整个客户端平台。
2. 游戏请求显示光标时，Java 在 Unity 根视图及其子视图恢复系统箭头。捕获、关闭功能、失焦、暂停、实例更换或传输失败时，恢复每个视图原有 `PointerIcon`。
3. `SOURCE_MOUSE` 的普通移动、按钮按下和释放继续原样分发，同时发布真实绝对位置。只有 PC 布局、光标可见、native 契约就绪、前台且没有真实触屏接触时，native 的 mouse-position icall 才使用这个位置；以实时 `Screen.width/height` 缩放，并把 Android 向下的 Y 转成 Unity 向上的 Y。
4. 捕获桥合成的视图中心按钮事件不参与绝对位置观测；相对样本也不进入绝对路径。真实触屏按下后恢复 Unity 原位置来源，最后一指 `ACTION_UP` 或 `ACTION_CANCEL` 结束接触；`ACTION_POINTER_UP` 不释放尚在按住的其他手指。触屏结束后等待新的真实鼠标样本，不复用旧鼠标坐标。
5. 稳定菜单中的配置发布、相同光标请求和跨帧读取保持幂等，按下期间不会因重复发布清空位置。切换隐藏光标、失焦、关闭功能或模块停止时清理旧位置。
6. icall 或可选字段无法解析/安装时保留原游戏输入。读取 binding 的 action 不保存托管指针缓存，避免对象销毁、GC 或地址复用产生错误匹配。

## 日志与验收

沿用每 250 ms 采样、稳定状态每 5 秒一次的摘要，新增 `absolute_hook_ready`、`absolute_events`、`position_reads`、`absolute_valid`。它们分别区分真实位置 hook 是否成功、菜单鼠标输入是否到达、游戏是否消费新位置及当前样本是否有效；不按每个事件打印日志。

宿主验证包括真实 Java 桥的捕获、释放、菜单图标、普通坐标与原事件保留，以及真实 C++ 状态/模块的布局恢复、相对轴、绝对坐标缩放、跨帧保持、触屏优先、退出 action 资格和诊断限频。测试使用模型化平台及托管对象，不能代替 Android 游戏现场验收。

需要冷启动新 APK 后，依次检查主世界 Esc 打开协议终端、工业模式 Alt 显示/隐藏、制造/寻访/简易制作/理智药菜单中连续按下释放及拖动、真实触屏与鼠标交替、窗口失焦恢复、关闭 PC 布局恢复 Android 行为。若菜单仍失败，应结合上面的绝对输入计数继续核对 Rewired 的触屏/鼠标事件路由，不能仅凭本轮 fixture 断言游戏内点击已修好。

平台资料：[Android 指针捕获与坐标](https://developer.android.com/develop/ui/views/touch-and-input/gestures/movement#use-pointer-capture)、[Android PointerIcon](https://developer.android.com/reference/android/view/PointerIcon)、[View 事件分发 API](https://developer.android.com/reference/android/view/View)。
