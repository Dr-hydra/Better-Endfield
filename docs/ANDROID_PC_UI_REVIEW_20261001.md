# Android PC 布局输入切换复核与修复（2026-10-01）

本次复核能确定：安卓以前确实存在调用游戏桌面布局切换的路径，3.3.0 用户报告与旧代码一致。当前显式开关也已接到 `DeviceInfo.ChangeInputType`，但现有手机日志不足以确定本次为何没有执行。一次性 generation 判定、初始字段读取失败、游戏进程的旧启动快照都是需要区分的情况；provider 拒绝 Keyboard 与 Init 改写字段目前没有当前手机二进制/时序证据，不能作为已确定根因。

调查阶段检查生产源码、历史提交和既有测试。手机 prefs/log 状态由主调查提供：`pc_ui_enabled=true`，Keyboard 枚举解析成功，相关 hook 成功，无 `Input type pushed` 记录。没有将这些状态扩展成游戏内部输入字段已经为何种值的结论。下文缺口及行号描述修复前代码，实际落地与验证见文末。

## 以前的明确调用路径

历史提交 `e547bae0`、`86618656`、`3b285742` 的 `native/modules/ui/module.cpp::PumpInputType` 均包含同一逻辑：

```cpp
const bool active = g_mobile_ui_enabled.load(...);
int32_t target = kInputTypeTouch;
if (!active) {
    const int32_t restore = g_restore_input_type.load(...);
    target = restore >= 0 ? restore : 0;
}
// current != target 时：
g_host->runtime_invoke(..., g_change_input_type_method, nullptr, parameters, &exception);
g_applied_generation.store(desired, ...);
```

所以当安卓加载 UI 模块，例如开启 HUD/UID 功能，且 `mobile_ui_enabled=false`、没有恢复值，target 就是 `0`（Keyboard）。如果手机已经进入 Touch，代码确实调用 `DeviceInfo.ChangeInputType(0)`；该路径不需要显式可见的 PC 布局设置。用户关于 3.3.0 隐藏入口触发的报告有源码支持，不能回答“安卓从来没有调用点”。

旧代码还在调用后无条件标记 generation 已应用，游戏拒绝/异常也不会再次尝试。文档 `docs/ANDROID_CAMERA_MMD_20261001.md:19` 已记录此隐式 Keyboard 行为以及本轮取消原因。

## 当前显式开关调用链

| 步骤 | 位置 | 作用 |
| --- | --- | --- |
| 保存开关 | `android/app/src/main/java/dev/betterendfield/android/ModuleSettings.java:123` | 保存 `PC_UI_ENABLED` 并重写 UI 配置。 |
| 配置内容 | 同文件 `:131` | 写 `enabled=true`、`diagnostics=true`、`pc_ui_enabled`，明确 `mobile_ui_enabled=false`、`platform_spoof_enabled=false`。 |
| 游戏启动读取 | `android/app/src/main/java/dev/betterendfield/android/XposedEntry.java:36` | 读取 `ModuleConfigurations`；此处是游戏进程启动快照。 |
| native 传参 | `android/app/src/main/java/dev/betterendfield/android/RuntimeBootstrap.java:94` | 将快照 `configs.ui()` 设进 `BETTER_ENDFIELD_UI_CONFIG`。 |
| 共享模块配置 | `android/app/src/main/cpp/modules/desktop/desktop_module.cpp:51`、`:85` | 从环境变量保存 configuration，调用共享 UI `configuration_changed`。 |
| Keyboard 枚举解析 | `native/modules/ui/module.cpp:1240` | 使用真实元数据读取 `DeviceInfo/InputType.Keyboard`，失败时不启用 PC 模式。 |
| 显式生效条件 | 同文件 `:1471` | `pc_active = config.enabled && config.pc_ui_enabled && g_keyboard_input_type >= 0`。 |
| 通知 pump | 同文件 `:1501` | `g_desired_generation.fetch_add(1)`，交给 UI/Unity 线程处理。 |
| pump | 同文件 `:981`、`:1017`、`:1028` | target 选择真实 Keyboard 值，调用游戏 `ChangeInputType`，读回正确才标记本次成功。 |
| 执行入口 | 同文件 `:1058`、`:1066`、`:1078`、`:1417` | UIStyleByState Awake/UpdateStyle、EventSystem.Update、Android frame client 均可调用 pump。 |
| 游戏自身切换 detour | 同文件 `:896` | PC 模式启用时，将游戏随后经过此 detour 的切换请求重定向到 Keyboard。 |

`617b1973` 新增显式 `pc_ui_enabled` 路径，并把默认无恢复值的隐式 Keyboard 改成不切换：`!active && restore < 0` 时直接返回。UI 模块是 PC 同源 native 代码，Android adapter 真正调用的桌面样式入口就是上述游戏 `DeviceInfo.ChangeInputType`，不是启动 Windows 界面程序。

## 已确定的代码缺口

### 1. generation 相同后不会检查游戏实际输入字段

`PumpInputType:982` 第一条有效判断为 `applied == desired` 则返回，甚至不读当前 inputType。`:1020` 在字段已经等于 target 时也标记成功。

因此以下情形由源码即可确认：

1. 显式 PC 模式已开启，初次读到 inputType=Keyboard（可能是初始值，也可能是游戏已选 Keyboard）；pump 标记 applied。
2. 后来游戏实际字段变成 Touch，但模块配置没有再变化。
3. pump 始终由相同 generation 提前返回，无法发现或修正实际状态漂移。

这不等于已经证明游戏 Init 就是该字段写入者。`DetourChangeInputType` 可以约束经过该钩子的游戏调用，但不能据此证明所有初始化/内联/直接字段写入都经过该钩子。需要日志记录真实字段前后变化；没有手机 MethodInfo dump/地址依据时不要照搬 PC RVA。

现有 `native/tests/android_rebuild/ui_layout_test.cpp` 直接包含生产 `module.cpp`，但每次切换场景前均递增 generation。它覆盖默认保留 Touch/Controller、显式 PC 切换和恢复，没有覆盖“同 generation、真实字段漂移”。

### 2. 初始字段读取失败会静默阻挡

`:1016`：`active && !have_current && restore < 0` 直接返回，不 invoke、不 mark、不记录日志。保留恢复能力的条件合理，但当前无法从日志区分未取得字段、取到默认值或流程压根没有运行。

`:997` 若 host/runtime_invoke/method 不可用会直接标记 applied，同样没有解释。但当前提供的契约和 hook 成功日志不足以单独证明每次 pump 都拿到可靠的静态字段盒装结果。

### 3. 当前日志未记实际 PC 配置快照

`:1486` 的 `UI Configuration applied` 只列 enabled、mobile、HUD/UID、platform 与整体 ACTIVE 状态，未记录 `pc_ui_enabled`、`pc_active` 或 Keyboard 实际值。HUD/UID 也可以使 ACTIVE=true，因此不能由 ACTIVE 日志推定 PC 模式启用。

prefs 中开关为 true 只证明设置保存结果。XposedEntry/RuntimeBootstrap/DesktopModule 读取的是游戏启动时快照，目前没有 UI 配置动态重取通道；如果游戏先启动再改设置，游戏里的 `g_pc_ui_enabled` 可能仍是 false。必须在 native 配置应用点记录真实值，才可排除旧快照。

### 4. “无 push 日志”不能优先归因 provider 拒绝

正常 UI 配置写 `diagnostics=true`，默认 diagnostics 也是 true。只要到达 invoke 分支，`:1043` 就记录 target、before、after，拒绝时带 `[REJECTED]`，异常带 `[managed exception]`。

因此在确认 diagnostics、日志完整性后，无 push 更指向 invoke 前的早退，包括 inactive/no-restore、已 applied、字段不可读、current==target。provider 拒绝只应在已观察到实际调用但 readback 不等于 target 时继续调查；当前没有足够证据确定 Android provider 是否拒绝 Keyboard。

PC 文档 `docs/MOBILE_UI_REVERSING.md` 的 `ChangeInputType→onInputTypeChanged→UIStyleByState` 与 provider 条件可以说明已有工作思路，但其地址及 Local provider 的 Touch 支持结论属于 PC 反编译，不能当作当前 Android Keyboard 支持的证据。

## 优先处理判断

建议先补有限、精确的状态日志，同时以生产 fixture 验证上述 generation 漂移缺陷，然后针对已证实缺陷修复。平台伪装没有目前所需证据，也不是优先手段。

- 配置应用时记录 PC 请求值、有效值、Keyboard 值、desired generation。
- pump 状态变更时记录 generation、当前字段可读性/current、target、restore，以及早退原因。每个 generation/原因只记一次，字段发生变化再记，禁止按帧重复刷日志。
- 将同 generation 下 Keyboard→Touch 的 fixture 场景补进去，区分初次“字段已经等于 target”和 invoke 成功后的漂移；再覆盖字段初始不可读后恢复、invoke 拒绝/异常、禁用恢复原输入、HUD/UID 单独开启不切布局。
- 活跃布局请求可用有限频率核对真实 inputType，并在漂移时重新应用；配置不活跃且没有恢复请求时仍保持无操作。
- 原始恢复输入不能因重试/漂移不断覆盖。初次读到默认 Keyboard 后尚未进入真实设备状态也会使 restore 误记为 Keyboard，这需要测出的初始化时序决定策略，不能仅靠重复调用解决。

此顺序能尽快分清是启动快照、pump 前置条件或游戏拒绝，并修复确定的一次性状态缺陷。当前不把未观察到的 Init/provider 行为写成故障根因。

## 本轮已落地与验证

- 生产 `PumpInputType` 在显式布局启用时，每 250 ms 核对真实输入字段。即使配置 generation 不变，字段偏离目标仍重新执行游戏自己的 `ChangeInputType`。禁用且没有恢复请求时不检查、不强制选择布局；新配置会清检查计时，下一次 pump 可直接执行。
- 初次字段已经等于目标时不记录恢复值，因为尚未发生覆盖。如果随后观察到游戏选择 Touch，再覆盖为 Keyboard，就保存这个真实 Touch 值供关闭时恢复，避免保存早期 Keyboard 默认值。
- 配置日志新增 `pc_ui_enabled`、`pc_ui_effective`、`keyboard_input_type`；pump 有限记录“字段已经匹配”及“等待可读字段”等提前返回原因。同一 generation 的同一调用结果不反复打印；拒绝重试受 250 ms 上限约束。
- 已用直接包含生产 `module.cpp` 的 `ui_layout_test.cpp` 在手机上运行独立 ARM64 测试：修复前，同 generation 下把字段从 Keyboard 改成 Touch，断言失败；修复后，漂移纠正、恢复 Touch/Controller、初次无需覆盖时不误存恢复值、禁用时保留游戏选择、检查限频、PC→Touch 反向漂移均通过。测试没有启动游戏、注入游戏指针或安装 APK。
- Windows `BetterEndfield.UiModule` 和 Android `betterendfield_android` ARM64 构建通过；本轮未生成、安装 APK，也未替换 PC 安装目录。

构建记录：`artifacts/android-model-diagnostics/ui-layout-build.log`、`ui-switch-windows-build.log`、`ui-switch-android-build.log`。这修复了可复现的模块状态缺陷；当前手机现场是否还存在旧启动快照、不可读字段或游戏拒绝，需包含新增日志的 APK 冷启动后确认，不宣称游戏内 PC 布局已经验收成功。
