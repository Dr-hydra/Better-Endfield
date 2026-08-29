# Better Endfield 移动端 UI / 云游戏与触控交互逆向接口文档

本文档汇总整理了对《明日方舟：终末地》（Arknights: Endfield）PC 客户端中**移动端 UI、触控轮盘、云游戏模式及平台判定**的逆向分析成果、关键类/方法/RVA 签名清单、两种实现路径的尝试与现存问题，供后续接手人员深入分析。

---

## 1. 核心机制概述

在游戏客户端中，UI 的形态（PC 键位布局 vs 手机触控轮盘）受以下几个层次的控制：

1. **底层平台判定 (`UnityEngine.Application` / `Beyond.DeviceInfo`)**：
   - 区分当前运行环境（Windows、Android、iOS）以及是否处于云游戏环境（Cloud Game）。
2. **输入设备与交互模式 (`Beyond.DeviceInfo.inputType` / `UIStyleByState`)**：
   - 游戏内部定义了 `InputType` 枚举：`Keyboard = 0`、`Touch = 1`、`Controller = 2`。
   - `UIStyleByState` 组件根据 `DeviceInfo.get_inputType()` 返回值切换各 UI 控件的样式与可见性。
3. **HUD 显隐与触控按键 (`Beyond.UI.UIHudFadeTouchSkill` / `Beyond.UI.UIJoystick`)**：
   - 战斗界面的移动端技能轮盘由 `UIHudFadeTouchSkill` 控制；
   - 虚拟移动摇杆由 `UIJoystick` 与 `UITouchPanel` 驱动；
   - 它们会在 `Awake` / `_CheckNeedShow` 时检查全局输入状态（如 `HudFadeManager.GetNeedShow`）。
4. **上层 Lua 业务逻辑 (XLua)**：
   - 大量 HUD、主界面与菜单通过 Lua 脚本编写，通过 `CS.UnityEngine.Application` 与 `CS.Beyond.DeviceInfo` 的 XLua Wrap 导出读取平台状态。

---

## 2. 观察到的完整接口与 RVA 签名清单

> **注**：以下 RVA 基准对应当前 IL2CPP Dump（基于 `GameAssembly.dll`，Unity 2021）。实际运行时通过 `BetterEndfield.Host` 的 `il2cpp_class_get_methods` 动态解析，无需硬编码偏移。

### 2.1 平台与设备状态 (`Common.Beyond.dll` -> `Beyond.DeviceInfo`)

| 接口 / 方法名 | 类型 | RVA | 描述 / 返回值 |
| :--- | :--- | :--- | :--- |
| `get_isMobile` | `bool ()` | `0x034DFB40` | 是否为移动端平台 |
| `get_isMobileUser` | `bool ()` | `0x04513960` | 用户视角是否为移动端 |
| `get_isAndroid` | `bool ()` | `0x034DFC60` | 是否为 Android |
| `get_isAndroidUser` | `bool ()` | `0x045139F0` | 用户是否为 Android 客户端 |
| `get_isIOS` | `bool ()` | `0x034DFBE0` | 是否为 iOS |
| `get_isIOSUser` | `bool ()` | `0x04513A60` | 用户是否为 iOS 客户端 |
| `get_isPC` | `bool ()` | `0x04502CD0` | 是否为 PC 端 |
| `get_isPCUser` | `bool ()` | `0x03C00570` | 用户视角是否为 PC 端 |
| `get_isPCorConsole` | `bool ()` | `0x04502C80` | 是否为 PC 或主机 |
| `get_isPCorConsoleUser` | `bool ()` | `0x03C00520` | 用户是否为 PC 或主机 |
| `get_isConsole` | `bool ()` | `0x067CC350` | 是否为主机平台 |
| `get_isConsoleUser` | `bool ()` | `0x067CC2DC` | 用户是否为主机平台 |
| `get_platform` | `RuntimePlatform ()` | `0x067CC3C0` | 平台枚举 (Android=11, Windows=2) |
| `get_userPlatform` | `RuntimePlatform ()` | `0x03C005C0` | 用户平台枚举 |
| `get_inputType` | `InputType ()` | `0x047A8C00` / `0x033FDF80` | **当前输入类型** (0=Keyboard, 1=Touch, 2=Controller) |
| `set_inputType` | `void (InputType)` | `0x049CFC80` / `0x067CC5C0` | 设置当前输入类型 |
| `get_usingTouch` | `bool ()` | `0x033FFC40` | 是否正在使用触控 |
| `get_usingKeyboard` | `bool ()` | `0x034DFAC0` | 是否正在使用键鼠 |
| `get_usingController` | `bool ()` | `0x033FDED0` | 是否正在使用手柄 |
| `get_supportsTouch` | `bool ()` | `0x034DED20` | 设备是否支持触控 |
| `get_supportsKeyboard`| `bool ()` | `0x067CC490` | 设备是否支持键盘 |
| `get_supportsController`| `bool ()` | `0x067CC410` | 设备是否支持手柄 |
| `ChangeInputType` | `void (InputType)` | `0x067CC08C` | 触发输入类型切换事件并通知全系统 |
| `Init` | `void (IDeviceInfoProvider)`| `0x049CE960` | 设备信息提供者初始化 |

---

### 2.2 Unity 引擎层平台接口 (`UnityEngine.CoreModule.dll` -> `UnityEngine.Application`)

| 接口 / 方法名 | 类型 | RVA | 描述 |
| :--- | :--- | :--- | :--- |
| `get_isMobilePlatform` | `bool ()` | `0x0366DCC0` | 引擎级移动平台判定 |
| `get_platform` | `RuntimePlatform ()`| `0x037978C0` | 引擎级平台枚举 (`Android = 11`) |
| `get_isConsolePlatform`| `bool ()` | `0x0B3A5D64` | 引擎级主机平台判定 |
| `get_isCloudGame` | `bool ()` | `0x03C00660` | 引擎级云游戏标志 |

---

### 2.3 云游戏架构接口 (`Common.Beyond.dll` -> `Beyond.CloudGame` / `CloudGameUtility`)

| 接口 / 方法名 | 类型 | RVA | 描述 |
| :--- | :--- | :--- | :--- |
| `CloudGameUtility.IsCloudGame` | `bool ()` | `0x041A5420` | 检查 `-cloud` 启动参数及哈希校验状态 |
| `CloudGame.get_enabled` | `bool ()` | `0x03C00630` | 云游戏是否全局启用 |
| `CloudGame.get_isMobilePlatform` | `bool ()` | `0x067CAD20` | 云游戏是否运行于移动端模式 |
| `CloudGame.get_isPCPlatform` | `bool ()` | `0x067CAD9C` | 云游戏是否运行于 PC 模式 |
| `CloudGame.get_platform` | `RuntimePlatform ()`| `0x067CAF74` | 云游戏当前上报的平台 |
| `CloudGame.get_clientType` | `CloudClientType ()`| `0x067CABC0` | 云游戏客户端类型 (App / Web) |

---

### 2.4 UI 与触控核心控制器

#### 1) UI 状态驱动 (`UI.Beyond.dll` -> `Beyond.UI.UIStyleByState`)
- **`UpdateStyle`** (`0x03CEBEB0`)：遍历自身所有 `StyleConfig` 并应用符合当前输入类型的样式。
- **`_IsValidStyle`** (`0x03CEC740`)：内部调用 `0x033FDF80`（`DeviceInfo.get_inputType`），匹配当前样式是否生效。

#### 2) HUD 触控技能轮盘 (`UI.Gameplay.Beyond.dll` -> `Beyond.UI.UIHudFadeTouchSkill`)
- **`Awake`** (`0x0B122330`)
- **`OnShow`** (`0x0B122778`)
- **`_CheckNeedShow`** (`0x0B12299C`)：调用 `UIHudFadeBase.get_globalNeedShow` (`0x03968D40`) 决定是否淡入技能轮盘。
- **`InformShow`** (`0x0B1224D0`)：响应输入切换事件主动显示。

#### 3) 全局 HUD 淡入淡出管理器 (`Gameplay.Beyond.dll` -> `Beyond.UI.HudFadeManager`)
- **`GetNeedShow`** (`0x03968DA0`)：根据 `UIStyleInputValidType` 判定当前 HUD 元素是否需要展示。
- **`InformAllFadingHud`** (`0x036737B0`)：广播输入类型掩码并通知所有注册的 HUD。

#### 4) 触控摇杆与触控面板 (`UI.Beyond.dll`)
- **`Beyond.UI.UIJoystick`** (`UI.Beyond.dll.cs:15350`):
  - `CheckShouldActive`
  - `_OnHudLayoutLoaded`
  - `_Activate`
  - `OnPointerDown` / `OnDrag` / `OnPointerUp`
- **`Beyond.UI.UITouchPanel`** (`UI.Beyond.dll.cs:15880`):
  - 核心手势与触控事件分发（拖拽、长按、缩放）。

#### 5) XLua 平台导出 Wrap (`Assembly-CSharp.dll`)
- **`UnityEngineApplicationWrap`** (`Assembly-CSharp.dll.cs:153512`):
  - `_g_get_isMobilePlatform` (`0x05D66E34`)
  - `_g_get_platform` (`0x049C8BD0`)
  - `_g_get_isCloudGame` (`0x05D66AD0`)
- **`BeyondDeviceInfoInputTypeWrap`** (`Assembly-CSharp.dll.cs:126775`)

---

## 3. 两种实现方案分析与实验总结

### 方案 A：原生 `-cloud` / `-cloudkey` 参数法
- **机制**：
  游戏主程序在入口 `CloudGameUtility._Init()` 会检查命令行参数 `-cloud` 和 `-cloudkey`。通过 MD5 哈希校验后激活云游戏管道。
- **实验结果**：
  由于云游戏模式下，游戏会等待专用 Named Pipe（如 `\\.\pipe\hypergryph_cloud_game_*`）由云端推流宿主完成 handshake 并下发鉴权 Token，导致 PC 端常规本地登录逻辑被绕过，游戏停留在无登录框的黑屏/等待状态。

### 方案 B：纯内存 Hook 平台与输入判定 (`BetterEndfield.UiModule`)
- **机制**：
  在不使用 `-cloud` 参数的情况下正常进行 PC 登录。通过在 `GameAssembly.dll` 中 Detour 拦截 `Beyond.DeviceInfo` 和 `UnityEngine.Application` 的各 getter，强制向游戏系统汇报当前为 `Touch` 输入与移动端环境。
- **失效根因（已通过反编译定位，2026-08-29）**：
  `get_inputType` 在 `GameAssembly.dll` 中存在**两份等价副本**，而 IL2CPP 元数据 `methodPointer` 指向的是几乎无人调用的那一份：

  | 副本 | 调用者 |
  | :--- | :--- |
  | `0x033FDF80` | `UIWidget.IsValidInput`、`IsShowTypeValid`、`get_usingTouch`、`ChangeInputType`(×3)、`DeviceInfo.Init` |
  | `0x047A8C00`（元数据） | 仅 1 处 XLua/invoker 桩 |

  模块经 `il2cpp_class_get_methods` 解析得到 `0x047A8C00` 并挂钩，因此对整个 UI 样式系统完全无效。
  `set_inputType` 同理：实际生效的是 `0x049CFC80`，元数据给出的 `0x067CC5C0` **零调用者**。
  这类访问器**不能靠 Hook 元数据地址生效**，且失败是静默的。

---

## 4. 已验证的正确启用路径

无需云游戏模式。完整链路（均经 Hex-Rays 反编译确认）：

```
DeviceInfo.ChangeInputType(Touch)   0x067CC08C
  └─ set_inputType 0x049CFC80        写静态字段 [静态区+0x28]
  └─ onInputTypeChanged.Invoke()     读 [静态区+0x30] 后 call
        └─ UIStyleByState.UpdateStyle()
              └─ _IsValidStyle → IsValidInput(mask)
                    └─ get_inputType 0x033FDF80 → mask&4(Touch)
```

要点：

1. **写状态而非改返回值**：`ChangeInputType` 会写真实静态字段，因此**所有**副本、以及派生的 `usingTouch` 全部随之改变，不需要挂任何 getter。
2. **事件是刷新已有 UI 的唯一手段**：`UIStyleByState.Awake`（`0x03CECC10`）会把自身注册进 `onInputTypeChanged`（静态偏移 `+0x30`）再调 `UpdateStyle`，所以事件能覆盖已实例化的面板。
3. **必须在游戏主线程调用**：Host 配置轮询线程不得直接调进托管代码。模块通过 Hook `UIStyleByState.Awake` / `UpdateStyle` 取得主线程执行点，再用 `runtime_invoke` 推送切换。
4. **`UIStyleInputValidTypeMask` 取值**：`Keyboard=2`、`Touch=4`、`Controller=8`。
5. **Local provider 已足够**：`LocalDeviceInfoProvider.SupportsInputType(Touch)` 的实现就是 `return DeviceInfo.isMobile`，而 `isMobile = isIOS || isAndroid` 派生自 `platform`。伪装 `platform` 即可让本地 provider 报告支持触控，无需 `CloudDeviceInfoProvider`、云管道或 handshake，因而不会触发方案 A 的黑屏问题。
6. **Lua 层不缓存**（推翻原假设）：`MainHudCtrl.lua` 中 `DeviceInfo.usingTouch` 为实时读取，而 `get_usingTouch` 正是走 `0x033FDF80`，故字段写入会同步传播到 Lua 层。

### 仍待确认

触控 HUD 的**美术资源是否随 PC 包分发**尚未证实。VFS 索引中不存在 `.prefab` 条目（prefab 封装在 Unity bundle 内部），因此无法用文件名枚举证伪。有利证据：Lua 侧保留了触控分支，且 `UIStyleByState` 的设计是「同一 Prefab 内按输入类型切换样式」而非分发独立的移动端 Prefab。若资源确实缺失，预期表现为「控件按触控样式重排，但缺少摇杆与技能轮盘」。

---

## 5. 触控输入注入（独立于显示）

切换 `inputType` 只改变 UI 形态，**不产生任何输入**。这是两条从头到尾没有交汇点的链路：显示链读 `DeviceInfo.inputType`，输入链读 `EnhancedTouch.Touch.activeTouches`。

### 5.1 输入链的唯一入口

三个读触控的函数全部遍历同一个访问器：

| 调用者 | RVA |
| :--- | :--- |
| `UIJoystick.CheckShouldActive` | `0x042EEA54` |
| `UIUtils.TryGetTouch` | `0x08B090234` |
| `InputManager.GetPressPosByTouchId` | `0x042EE6D1` |

被调用的 `0x042EEF60` 即 `UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches`（与 `Unity.InputSystem.dll.cs:12196` 的 RVA 一致）。该数组只由 `Touchscreen` 这个 InputDevice 填充，PC 上无触摸屏时恒为空，三处循环体一次都不进。

**摇杆的 WASD 兜底**：`UIJoystick` 自带 `_keyboardUp/_keyboardDown/_keyboardLeft/_keyboardRight`（`KeyboardKeyCode`，偏移 `0xb0`–`0xbc`），直接读键码，不经过 `activeTouches`。这是切到 Touch 后移动仍然可用、而其余操作全失效的原因。

### 5.2 已验证：合成触控注入可行（2026-08-29）

Windows 的 `CreateSyntheticPointerDevice` + `InjectSyntheticPointerInput` 注入的触点，会被 Unity 的 Windows 后端识别为 `Touchscreen` 设备并填入 `activeTouches`，游戏内触控 UI 实际响应。**因此不需要在 Input System 托管层伪造设备**（即不需要 `AddDevice` / `QueueEvent` 那条路）。

验证工具：`native/research/touch_probe/`（`EXCLUDE_FROM_ALL`，不进发布包）。

两个必须注意的实现约束，都是踩过的坑：

1. **必须与游戏同完整性级别**。UIPI 会静默丢弃低完整性进程投向高完整性窗口的输入——不报错，只是没反应。游戏以管理员运行，所以探针由 `launcher_module.cpp` 从游戏进程内 `CreateProcess` 拉起，靠继承令牌对齐。
2. **配置不能放 `BetterEndfield.ini`**。`ConfigurationService.SaveModConfigurationAsync` 是整文件覆盖写，只序列化 UI 建模过的节，手工添加的节会在下次保存时被删除。研究模块的参数应放在 DLL 旁的独立文件里。

### 5.3 已实现：`native/modules/ui/touch_input.cpp`

左键即手指，随触控 UI 一同启停，`Ctrl+Alt+T` 运行时开关。用 `WH_MOUSE_LL` 而非轮询，以便吞掉原始鼠标事件。三处容易写错的地方：

1. **只按触控签名过滤，不按 `LLMHF_INJECTED` 过滤**。Windows 从触控反向合成的鼠标消息带 `dwExtraInfo == 0xFF515700`（掩码 `0xFFFFFF00`），必须滤掉，否则自己注入的触点会被再转换一次，手指按下去抬不起来。但**不能**顺手滤掉 `LLMHF_INJECTED`——串流客户端正是用 `SendInput` 送鼠标的，那恰恰是最该转换的输入。
2. **必须限定游戏窗口有焦点**（比对前台窗口的进程 ID 与自身）。否则触控布局一开，整个桌面的左键都会被吞掉，包括本 mod 自己的战斗浮层。
3. **低级钩子的回调在安装线程上派发**，该线程必须自带消息循环；热键也注册在同一线程，靠 `WM_HOTKEY` 收，配置中途关闭时用自定义消息把"松手"投递回该线程，避免跨线程操作设备。

### 5.4 已知但不修：切到 Touch 后键盘绑定被禁

现象已确认：切到 Touch 后除 WASD 外键盘全部失效（WASD 幸存的原因见 5.1 的摇杆兜底字段）。

结构上确认了一段：强制重建函数边界后（元数据 RVA 落在 iFix 热更新桩上，IDA 默认只认出 5 字节），`_UpdateInputBindingMask`（`0x09E2FAD4`，176 字节）在 `0x09E2FB4E` 处确实调用 `_SetInputBindingMask`（`0x09E2F04C`，531 字节）。

但 `_UpdateInputBindingMask` **零调用者**——又是元数据死副本的形态。所以"切换输入类型 → 禁用键盘绑定组"这条因果链**仍未证实**，活副本没找到。

**不修**：目标场景是串流到移动端，那边本来就没有键盘。若将来要做 PC 上的触控/键盘混合模式，从这里接着挖：先按字节特征找 `_UpdateInputBindingMask` 的其余副本，逐个查 xref 定位活的那份，再决定是挂钩空转还是强制传 Keyboard。

### 5.5 尚未解决

- 无（触控 HUD 美术资源已确认随 PC 包分发，实测可用）：疑为 `InputManager._UpdateInputBindingMask(InputType)` → `_SetInputBindingMask(String group)` 切换 Rewired 绑定组所致。**仅有签名与行为吻合，无反编译证据**——这两个函数在 IDA 中被 iFix 热更新桩打断了边界。
- 技能轮盘等美术资源是否随 PC 包分发，仍未证实。
