# Windows 来源游戏的输入兼容性：实施与验证

日期：2026-10-12。用户授权开发本机其他游戏的投影操控，并要求操控期间屏蔽终末地输入。

## 实施结果

已为显式选择的 Windows x64 Unity／Unreal 来源加入进程内输入桥。
仅在开始窗口操控时启用；窗口类 `UnityWndClass`／`UnrealWindow` 用于选择后端，
不能作为该游戏已经兼容的证据。普通应用继续使用原窗口消息路径，保留 Qt 重复字符修复。
当前默认热键：`-` 投屏启停、`Ctrl+-` 视频暂停／继续、`Alt+-` 窗口操控启停。
Esc 转给来源，不再用于退出操控；自定义热键仍按配置读取。

桥文件为 `runtime/BetterEndfieldNext.GameInputBridge.dll`，由采集辅助进程加载进选中的来源。
通过按来源 PID 命名的共享内存和注册窗口消息传送事件，不使用全局 SendInput，
不把来源变为真实系统前台。来源中的局部焦点与设备状态只在操控期间生效。
退出、来源消失、终末地失焦、发送失败或两秒心跳过期会释放输入；DLL 保持加载，
非操控状态下钩子透传原接口。重连从来源已加载模块的 PE 导出表定位初始化函数，
不使用本地新版 DLL 的 RVA 去调用仍在运行的旧版模块。

## 来源桥覆盖

| 路径 | 当前实现 |
| --- | --- |
| Raw Input | 向来源原有键鼠接收窗口派发 WM_INPUT，GetRawInputData 返回合成包；操控时过滤来源的真实硬件键鼠包 |
| 原生键状态 | GetAsyncKeyState、GetKeyState、GetKeyboardState 返回来源虚拟状态 |
| 鼠标位置 | GetCursorPos 返回映射位置；SetCursorPos／ClipCursor／ShowCursor 隔离来源对实体鼠标的修改 |
| 焦点 | 来源局部焦点查询及通知；避免默认窗口处理把来源变成真实前台 |
| DirectInput8 | 已存在及新建的标准键盘、鼠标设备状态与 buffered events；其他设备及非标准 state 大小透传 |
| 退出 | 发送键鼠松开、恢复焦点通知；失联租约同样释放 |

鼠标相对量来自前后映射坐标差值，首次定位为零；仍使用用户接受的原系统鼠标方案，
可能显示两个鼠标或存在空间偏移。本次没有重新启用此前失败的虚拟指针方案。

## Muse Dash 实机证据

安装位置：`E:\SteamLibrary\steamapps\common\Muse Dash`，Steam build ID 25297493。
本机为 Unity 2019.4.41f1、IL2CPP，包含 Rewired 和 Unity 新旧输入模块。
只读解析确认发布配置 `runInBackground=true`，旧输入未被禁用。
这些静态信息不能单独证明游戏接受投影输入。

用户保持 Muse Dash 主菜单运行后，已在来源后台完成内部状态验证。
只发送保留键 F15 与客户区左上角 (8,8) 的鼠标移动、按下和松开，未进入对局。

| 来源读取 | 初始 | 按键按下 | 鼠标按下 | 全部松开 |
| --- | --- | --- | --- | --- |
| Unity GetKey(F15) | 0 | 1 | 1 | 0 |
| Unity GetMouseButton(0) | 0 | 0 | 1 | 0 |
| Rewired Keyboard.GetKey(F15) | 0 | 1 | 1 | 0 |
| 来源 frameCount | 65410 | 65436 | 65462 | 65488 |

Unity 与 Rewired 返回的位置均为 (8,1069)，与客户区 (8,8) 的 Y 轴换算一致。
来源共读取 Raw Input 15 次、原生键状态 242 次、鼠标位置 119 次、局部焦点 11 次；
原有键盘／鼠标 Raw Input 接收目标各一个。测试期间实际系统前台保持不变。
Rewired Mouse.GetButton 诊断接口未解析到，未把它的默认零值当作鼠标读取失败。

结果：`build/next/media-screen/muse-input-probe-20261012-004627/result.json`。
来源在运行中加载的是该目录的独立 DLL 副本，部署无需关闭 Muse Dash。
本项证明后台来源引擎读取到了按键、鼠标状态与位置；
会客室内菜单操作、实际玩法和音游输入延迟仍需另行验收。

## 终末地输入屏蔽

操控期间收集并消费终末地 WndProc 的普通键鼠消息，同时使用 Host 组合钩子中和
GetAsyncKeyState／GetKeyState／GetKeyboardState、GetRawInputData／GetRawInputBuffer，
过滤 Rewired Player／Keyboard／Controller 以及 Unity legacy 的键、按钮、轴等查询。
Raw Input 的松开包保留，用于清理旧设备缓存；游戏读取在整个操控与退出屏障中保持中性。
媒体热键使用线程局部的真实键态读取，因此屏蔽期间仍可用操控热键退出。
进入与退出调用 Unity ResetInputAxes；退出后等实体键鼠全部松开并经过两帧，再恢复输入。
Alt 的系统菜单命令同样屏蔽。

四项隔离 CTest 已通过，覆盖 Esc 转给来源、原生键状态和 Raw Input 屏蔽、
Unity 查询屏蔽、退出组合键松开屏障、跨进程来源状态、现有 DirectInput 设备、
字符唯一性、相对鼠标、释放、实际前台／ClipCursor 保持及已加载模块重连。
新增接口与本机终末地 1.5.3 静态 dump 核对：130 个方法、3 个字段、0 缺失。
Windows 模块／采集辅助进程与 Android 共享原生编译通过；未安装 Android APK。
部署后用户在会客室实测并确认“效果可以”，本版投影操控获得用户验收。
该反馈不扩展为所有按键、来源游戏或玩法的逐项兼容保证。

## 已知边界

- UE 来源已加入后端选择，尚无 UE 游戏实机验证；不保证任意 Unity／UE 游戏通用。
- GetRawInputBuffer 当前过滤真实硬件包，尚未提供合成 buffer 事件。仅使用该接口消费输入的来源需补齐后端。
- 非标准 DirectInput 数据格式、独占设备、其他引擎后端与来源权限限制需单独验证；当前来源桥只支持 Windows x64。
- 来源必须继续后台更新；局部焦点桥不保证所有游戏自有的后台暂停策略均能工作。
- 尚未实现无限相对鼠标，第一人称游戏的视角和持续转向未验收。
- 不提供游戏操控到画面的延迟保证；音游需分别验证捕获帧率与玩法手感。

运行日志为 `%LOCALAPPDATA%\BetterEndfieldNext\logs\MediaInputBridge.log`，
只记录 PID、钩子覆盖、计数和读取次数，不记录具体键值或文字。未计算产物哈希。

## 原问题的隔离诊断

最初 WindowInput 只发送定向窗口消息。隔离 Win32 窗口收到普通按键和鼠标消息各一次，
但 F24 Raw Input 为零、GetAsyncKeyState／GetKeyState 按下位为零，前台保持。
这解释了应用测试通过仍不能证明来源游戏输入可用。
原诊断结果保留于 `build/next/media-screen/raw-input-probe/result.json`，
静态资产检查位于 `build/next/media-screen/muse-dash-input-assets.json`。

## 官方依据

- [Microsoft Raw Input](https://learn.microsoft.com/en-us/windows/win32/inputdev/raw-input)：WM_INPUT 与读取接口。
- [Rewired 输入设置](https://guavaman.com/projects/rewired/docs/RewiredEditor.html)：原生键鼠、Raw Input／DirectInput 与失焦过滤。
- [Rewired 已知问题](https://guavaman.com/projects/rewired/docs/KnownIssues.html)：普通键消息与 Raw Input 差异。
- [Unity Input System 后台行为](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.14/manual/Settings.html)：后台更新和设备失焦处理；不作为 Muse Dash 内置版本的证据。
