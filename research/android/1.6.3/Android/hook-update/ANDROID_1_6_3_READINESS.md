# Android 1.6.3 挂点核查边界

2026-10-15，工作区 `G:\Better Endfield`。本次只读核查源码构建关系、现有 Android 输入及 ADB 设备，不安装、不重启、不修改共享 native 代码，不生成全量 dump / IDA 数据库。

后续更新：设备已连接，实际新版为 **Android 1.6.4 / versionCode 61**，已取得同一安装的 ARM64 库与 metadata，并完成定向静态核查及 BE 4.0.3 的一轮初始化日志检查。当前结果见 [Android 1.6.4 核查](../../../1.6.4/Android/hook-update/ANDROID_1_6_4_HOOK_CHECK.md)。下文无设备/无新版输入描述仅保留为早先时点记录。MMD 编译分支结论已按实际源文件纠正。

## 当前结论

Windows 本轮的持续冲刺改动会编译进 Android；MMD 的 Android 分支使用独立托管查询适配器，Windows 地面结果原生布局校验不会编译进入该分支。**Windows 1.6.3 的挂点核验不能直接作为 Android 新版的核验结果**。Android 使用 ARM64 独立生成的 IL2CPP 本机代码；方法存在、方法仍被实际调用、字段合同正确是不同条件。

| 本轮修改 | Android 构建关系 | 现在可确认 | 新版 Android 尚需确认 |
| --- | --- | --- | --- |
| Actions 持续冲刺 | `android/app/src/main/cpp/CMakeLists.txt:100` 直接编译共享 `native/modules/actions/module.cpp` | 新挂点 `_TryGetCurInterruptCommand` / `_DoExit` 会进入 Android；按方法描述符解析 ARM64 本机入口，未复制 Windows RVA | Android `_TickMainFlow` 的实际调用链和内联情况；两个挂点是否在对应路径被调用；一次冲刺开始/保持/退出 |
| MMD 地面检测 | `eiem_slot.inc` 的 Android 条件分支包含 `compat/android_slot.inc`，再使用 `android_terrain.inc` | 按签名解析 `ComputeFloorDist` / `FindFloor`，通过 `runtime_invoke`、装箱并 pin 的值对象及字段元数据查询；Windows 原生 typedef / 布局校验不在 Android 路径中 | Android 方法参数、命名字段/值类型方法合同及实际采样结果 |
| CombatStats Proto、108 属性范围和修正类型 | 当前 Android CMake 未编译 `native/modules/combat_stats/module.cpp`，Android native 模块中也未注册 CombatStats | 这部分 Windows 更新不是当前 Android 包中的功能改动 | 若以后移植，应单独核查 Android 协议字段、枚举与集合布局；本轮无需为未编译模块扩大验证 |

## 已有适配与保护

- Android `DesktopModule` 通过当前进程的 IL2CPP 元数据按程序集、类、方法和签名查找本机入口，再交给 Android hook broker。共享的是描述符和逻辑，不是 Windows 地址。
- Actions 的 `InterruptCommandData.interruptType` / `reason` 通过字段解析和 `field_get_value_object` 读取；`SpDashEnd`、`ForceExit`、`Sprint` 等枚举通过当前元数据取值。未将 Windows 枚举数字或字段偏移直接用于 Android。
- Actions 必需方法/字段解析失败返回 `BE_Result_ContractMismatch`，Android adapter 结束该模块初始化并释放对应挂钩。此保护能发现缺失接口，不能发现“方法存在但已被 ARM64 编译器内联，挂钩没有触发”。
- Windows MMD `VerifyFloorResultLayout` 检查 `FindFloorResult` 和嵌入 `RaycastHit` 的原生布局；Android 不调用该函数。Android `PrepareTerrain` 验证值类型、完整方法签名和命名字段，`FloorSample` 通过托管调用与 pin/unbox 读取结果。
- Android 当前仅构建 `arm64-v8a`。ARM64 与 Windows x64 都是 64 位不意味着优化、值类型传参和字段合同可以免检；运行时合同保护也不等于功能已验证成功。

## 本机可用资料

- `adb devices -l` 当前没有任何连接设备，无法确认设备上的新版客户端，也无法进行一轮运行时挂钩验证。
- `inputs/1.6.3/` 只有 `Windows/`，没有 Android 1.6.3 输入。
- 当前已知游戏 APK 是 `inputs/1.5.3/Android/vfs-transfer/com.hypergryph.endfield/base.apk`。用本地 `aapt dump badging` 读到 `com.hypergryph.endfield`、`versionName=1.5.3`、`versionCode=50`、`arm64-v8a`。
- 现有 `research/android/1.5.3/Windows/android-153/global-metadata.dat` 和 `android-refactor/engine/libil2cpp.so` 都属于旧版资料目录。本次不拿这些旧输入重复扫描后冒称新版结果。
- 当前工作区与用户 Downloads 顶层未发现其他新版游戏 APK。未作跨磁盘全量搜索。

## 最小后续资料与验证

优先使用**同一份 Android 1.6.3 安装**的 `arm64-v8a/libil2cpp.so` 和已可读取的 `global-metadata.dat`，附该安装的版本信息；或者连接已更新到 1.6.3 的设备，走既有工具定向读取这些内容。无需先建立全量反编译库。

定向核查只需覆盖：

1. 持续冲刺涉及的 `_TickMainFlow`、`_TryGetCurInterruptCommand`、`_DoExit`、`ShouldInterruptSpDash` 的签名和 ARM64 调用关系，以及 `InterruptCommandData` 字段/相关枚举。
2. `ComputeFloorDist`、`FindFloorResult`、`UnityEngine.RaycastHit` 的参数和嵌套字段布局。
3. 在设备上查看共享模块初始化结果，做一次持续冲刺进入/保持/退出及一次 MMD 地面采样。这一步确认挂钩实际触发和调用 ABI，而非重复扫描全部资源。

如果还要给 Android 自定义模型、Unity 原生网格路径作新版保证，需要该安装的对应 Unity 原生库/设备探针资料，不能用 Windows 模型采集或旧 Android 资源代替。当前没有出现足以支持直接改写 Android 偏移或加入平台分支的新证据，因此本轮未作猜测性修复。
