# 安卓版重构方案

> 2026-09-21 更新：本文保留上一轮调查记录。MeshData 数组失败已定位为调用约定错误；安卓提交、回读及资源事务的新进展见 [ANDROID_MESH_SUBMISSION_20260921.md](ANDROID_MESH_SUBMISSION_20260921.md)。关于“必须新增托管桥”“replace 始终关闭”的旧结论已被后续验证取代。


**日期：** 2026-09-20
**范围：** LSPosed 注入、安卓版控制界面、悬浮控制窗、模型替换模块
**目标：** 在保持现有功能可回退的前提下，把安卓版从“桌面模块的移植集合”重构为可分层演进的运行时。

## 0. 执行状态（2026-09-20）

已进入目标模式执行代码改造。实际边界和逐项验证见 [PC 复用审计与执行记录](ANDROID_CUSTOM_MODEL_REUSE_AUDIT_20260920.md)。

**最新协作方式：** 优先打通模型导入链路。用户负责游戏内登录、点击继续、切换角色和观察效果；助手负责修改、构建、更新模块及读取日志。测试流程发生变化时简要告知用户，不再自行操作游戏或进行界面巡检。用户正在测试时不自动关闭游戏；此前“助手启动的无人值守测试结束后关闭游戏”的要求仍适用于无人值守测试。

**最新字段进展：** 已加载 Unity 代码中找到两个独立的具名路径（描述器与回调读取器），均动态导出同一字段偏移；适配器已实现仅对新建、未发布 Mesh 的受限写入和回读。正式资源交付已观察到 `chr_0003_endminf_postmodel`。管理员包测试等待用户进入主游戏后读取日志；新 Mesh 写入测试及实际外观替换尚未验证完成。下列旧阶段记录中的“只找到一条归档路径/没有写入实现”已由此进展取代，不能据此认定完整替换已经完成。

**最近一次人工游戏测试结果：** 用户进入主游戏后，`endmin_in_casualwear.bem` 的 `resource=auto` 命中了 `chr_0003_endminf_postmodel` 和随后加载的 `chr_0003_endminf_uimodel`。`mesh_probe=1` 对正式角色的第一个 Mesh 完成“创建独立 Mesh → 写入字段 → 回读 → 销毁”，日志为 `detached Mesh field initialize=PASS source_value=4`；原资源和渲染器未修改。游戏进程保持运行且未见 AndroidRuntime/FATAL。下一步才是复用 PC 构建器发布替换 Mesh。

- [x] 区分登录模型模块 `model` 与 BEM 自定义模型模块 `custom_model`，完成首轮代码边界审计。
- [x] 建立 PC/Android 共用的 BEM/注册表构建目标；PC 事务测试和模拟器 ARM64 模型包解析通过。
- [x] 实现 legacy/modern 两个独立构建变体、共享启动层和 modern 配置快照同步，两种 APK 构建通过。
- [x] 现代入口与首帧 native 加载验证：NeoZygisk 2.4 + Vector 2.2/API 102 成功注入；不兼容的 ReZygisk 已停用。
- [x] Android 资源交付探针命中 `_FinishWithAsset`，已观察到 `SK_actor_female` 等资源；实际 Mesh 观察和替换仍待实现。
- [x] Android 只读动态扫描探针在真实 Mesh 上命中唯一候选并观察到 1/4；与 getter 语义差异已记录。
- [ ] 扫描证据生产化、可写字段适配与单目标模型替换；PC `custom_model/module.cpp` 已进入 Android 编译目标，BEM 包校验和 Endminf 的 `SK_actor_female` 资源匹配已在模拟器通过；扫描器已升级到函数边界、邻近字段和输入 `+0x14` 到候选 `+0x1C0` 的同函数复制链校验，当前静态/动态均只得到一个 `libunity.so+0x87AD48` 复制候选，并支持显式 `copy_hook=1` 与 `create_hook=1` 的只读参数追踪；archive 方向仍不独立，几何发布继续禁止候选写入。
- [x] 悬浮控制服务、响应式内容壳和 modern remote-file 命令通道已接入并构建通过；命令消费已进入 Unity 线程队列，尚未绑定模型写入动作。
- [x] 响应式内容壳、悬浮控制窗骨架和独立诊断页已接入；功能页仍以内嵌页面区块实现，模型动作绑定和完整多分辨率巡检仍未完成。

构建已升级至 API/service 102 所要求的 compileSdk 37、AGP 9.1.1、Gradle 9.3.1；targetSdk 保持 35。以下设计按实测证据继续修订，不把待研究方案当成已实现功能。

**Android 兼容性审计结论：** 见 [Android 模型替换静态兼容性审计](ANDROID_CUSTOM_MODEL_STATIC_AUDIT_20260920.md)。当前 Android `replace=1` 明确拒绝发布。PC 八项 raw Mesh icall 中七项未解析成功，UploadMeshDataImpl 可用；部分名称编码隐藏，不能用明文搜索断言接口不存在。MeshData 分配/访问/应用注册链已静态定位，`api_audit=1` 已核实签名；`mesh_data_probe=1` 已在 Unity 资源线程完成 Mesh/ MeshDataArray/`m_Ptr` 契约验证，但 descriptor 数组安全构造仍缺少托管桥，因此探针在几何写入前拒绝。上次桥接中的两参数回调被强转成一参数函数是确定的源码错误，已移除；完整替换仍待验证。

## 1. 结论

### 1.1 API 采用新版 libxposed/LSPosed API，但保留过渡兼容层

新版 API 可以使用，建议将它作为长期入口。官方现代 API 使用 `XposedModule` 作为 Java 入口，入口清单从 `assets/xposed_init` 迁移到 `META-INF/xposed/java_init.list`；原生入口使用 `META-INF/xposed/native_init.list`，模块配置和作用域也迁移到 `META-INF/xposed/module.prop`、`META-INF/xposed/scope.list`。参考：

- [libxposed API](https://github.com/libxposed/api)
- [libxposed Javadoc](https://libxposed.github.io/api/io/github/libxposed/api/package-summary.html)
- [LSPosed Modern API 迁移说明](https://github.com/LSPosed/LSPosed/wiki/Develop-Xposed-Modules-Using-Modern-API)

当前采用 `io.github.libxposed:api:102.0.0` 与 `service:102.0.0`。过渡兼容采用 **独立 product flavors**：`legacy` 保留原入口，`modern` 仅打包现代入口，两者使用相同 applicationId，可覆盖安装回退。不要在同一 APK 混用两代框架 API。当前现代框架验证目标为 Vector 2.2/API 102，手机上的框架版本仍需单独验证。

新版 API 只负责“模块被加载、作用域被授予、进程被识别”。真正的 hook、资源关联、模型替换和控制命令都放到与框架无关的 runtime 层，这样以后更换注入框架时不用重写功能模块。

### 1.2 界面改为响应式的功能页

现有 `activity_main.xml` 是一个大型单页滚动布局，适合功能少、分辨率固定的早期版本，但不适合手机横屏、平板、折叠屏和系统字体放大。重构为以下结构：

```text
MainActivity
└── ResponsiveShell
    ├── ModelFragment
    ├── VoiceFragment
    ├── EnhancementFragment
    └── DiagnosticsFragment
```

手机使用底部导航或顶部标签；宽度达到平板阈值时切换为左侧导航加右侧详情的双栏布局。所有尺寸进入 `values/dimens.xml`、`values-sw600dp/`、`values-land/`，不再在 Java 或单个 XML 中散落固定像素值。

### 1.3 悬浮窗采用 Android Overlay Service，不把它当成 PC 热键的直接替代

手机没有可靠的全局 PC 键盘热键。悬浮窗应提供等价的“一键动作”和状态切换：折叠态是一个小型控制点，展开后显示模型替换、语音、增强功能和日志状态。需要 `SYSTEM_ALERT_WINDOW` 授权，并由前台服务管理生命周期。

悬浮窗不能直接从 UI 线程调用 Unity 或 native hook。控制链应为：

```text
OverlayService
  → ModuleControlRepository
  → 原子配置/命令通道
  → NativeBridge
  → 游戏线程上的 RuntimeCommandPump
```

默认只在游戏进程处于前台时显示，游戏退出或注入失败时自动收起。权限拒绝、服务被系统回收、游戏重启都必须能恢复，不得因为悬浮窗异常影响游戏加载。

### 1.4 模型替换是第一业务迁移目标

模型替换应先于悬浮窗完成，因为悬浮窗只是控制面，必须先有稳定的模型替换运行时和命令接口。第一步不是直接改 Android 代码，而是先审计 PC 端实现：逐个标记可直接复用、需要抽象、必须重写的部分。现有 Android 工程把桌面登录展示模块 `native/modules/model/module.cpp` 编译进 `login_model`，并已有 Android Host 适配；BEM 自定义模型替换实际位于 `native/modules/custom_model/`，此前没有接入 Android。后者的 Windows 指针访问、扫描和生命周期必须单独适配。

安卓版第一版模型替换应拆成“平台适配层 + 共享替换策略 + Android 资源接入层”。字段写入路线明确采用 PC 端动态扫描方案的复刻，而不是重新猜一个 Android 固定偏移：

```text
Android Hook Bootstrap
  → ResourceDeliveryAdapter
  → AssetIdentity / ModelReplacementSession
  → Shared Model Replacement Policy
  → MeshSkinMetadataAdapter(Android)
  → Unity native object
```

`Mesh.InternalSetBoneWeights` 已经由实机观察确认不能作为 `m_BonesPerVertex` 的 setter，因此不再作为目标 hook。`Mesh.Internal_Create` 只适合作为创建时机观察点；真正的字段写入要复刻 PC 端的动态扫描、候选验证和运行时适配流程，在 Android 的资源上下文和 native 对象布局确认后进行。

当前 Android ELF 证据还显示 `0x87AD48`（结构复制路径）、`0xD97A70`（骨骼计数/索引消费者）和 `0x6E1D04`（槽位分配路径）共同触及候选位移，但它们尚未映射回稳定的 IL2CPP 方法元数据；因此不能把这些版本地址当作方法级 hook 目标。

真实游戏进程的方法审计进一步确认，`Internal_Create`、`ClearImpl`、`UploadMeshDataImpl`、`GetBonesPerVertexValue` 的托管解析结果属于 `libil2cpp.so` 包装层；通过 icall 解析得到的 Unity 原生入口位于 `libunity.so`，`InternalSetBoneWeights` 为 `libunity.so+0x5190F0`，但这些入口均未对应上述三个字段访问候选。显式调试 hook 已捕获 16 次 `Internal_Create` 调用，但仅一个候选值为 1，其余为 0 或大数；这尚未证明新 Mesh 的字段路径有效，必须先核实 native 对象来源。这个结果排除了“把 `InternalSetBoneWeights` 当作字段 setter”的路线，也说明仅凭方法名不能替代 PC 动态扫描的结构证据。

## 2. 当前实现基线

以下事实作为重构起点，后续以代码和实机日志为准：

- Android 工程位于 `android/`，目前是 LSPosed 模块，native hook 在 `UnityPlayer.nativeRender()` 首帧后启动。
- Java 入口按构建变体分开：legacy 保留 `assets/xposed_init`/`assets/xposed_scope`，modern 使用 `META-INF/xposed/java_init.list`、`module.prop` 和 `scope.list`。
- native 层已经有 `core/hook_broker`、`core/runtime`、日志和多个功能模块。
- `login_model` 当前复用桌面模型源文件；Android 专属的资源投递、对象解析和字段适配尚未独立出来。
- UI 当前由 `activity_main.xml` 中的响应式页面区块承载模型、语音、增强和诊断页；逻辑仍集中在 `MainActivity`，后续可再拆成 Fragment。
- 配置由 UI 和注入进程共同使用，必须明确跨进程的读写协议，不能依赖某个进程的内存状态。

## 3. 建议的目录和模块边界

```text
android/app/src/main/
├── java/.../framework/
│   ├── ModernXposedEntry.java
│   ├── LegacyXposedEntry.java
│   └── ModuleScope.java
├── java/.../ui/
│   ├── MainActivity.java
│   ├── ResponsiveShell.java
│   ├── ModelFragment.java
│   ├── VoiceFragment.java
│   └── DiagnosticsFragment.java
├── java/.../overlay/
│   ├── OverlayService.java
│   └── OverlayCommandRouter.java
└── cpp/
    ├── core/
    ├── framework_adapter/
    ├── resource_delivery/
    ├── model_replacement/
    │   ├── model_replacement_policy.cpp
    │   ├── asset_identity.cpp
    │   ├── mesh_skin_metadata_adapter.h
    │   └── android_mesh_skin_metadata_adapter.cpp
    └── command_pump/
```

功能模块通过统一能力开关管理：`model_replacement`、`voice`、`enhancement`、`overlay_control`。每个模块都能单独报告 `disabled`、`waiting_for_game`、`ready`、`failed`，单个模块失败不应阻止其它模块启动。

## 4. 新版 API 迁移设计

### 4.1 入口适配

新增 `ModernXposedEntry`，以 API 102 的 `onModuleLoaded`/`onPackageReady` 回调执行以下工作：

1. 校验当前进程和模块作用域是否为目标游戏。
2. 初始化共享配置读取器和日志上下文。
3. 启动 native bootstrap 或注册 native init 入口。
4. 将框架提供的错误和生命周期事件转换为 `FrameworkEvent`。

保留 `LegacyXposedEntry` 一段过渡期。两者不得同时重复安装 hook；由 `HookBroker` 使用进程级一次性状态保证幂等。现代入口验证完成前，分别产出 legacy/modern APK；单个 APK 只包含对应框架入口。验证完成后再决定是否停止旧版构建。

### 4.2 配置与跨进程

UI 进程和游戏进程不共享 Java 内存。modern 使用官方 libxposed service 的 remote preferences 作为跨进程配置快照；UI 本地设置为持久化来源，服务连接时发布，断连后保留本地设置。legacy 保留 XSharedPreferences 路径。

快照包含 `schemaVersion`、`generation` 和功能配置；配置仍在启动时消费，动作命令通过独立的 `command.next`/`command.status` remote file 通道传递和回执。不能把应用私有目录中的“原子文件”直接当成跨 UID 可读通道；如果使用文件交换 BEM/日志，须通过框架 remote files 或受控授权传递。

待实现的动作协议须区分 `accepted/applied/rejected/unsupported`，带唯一命令 ID、进程会话 ID 和配置代数，在游戏线程消费并回报结果。

## 5. 响应式界面重构

### 5.1 分阶段替换

第一步不重写全部控件，只把现有大 XML 拆成三个功能 Fragment，并抽出共享的状态模型。第二步加入响应式 shell 和资源限定符。第三步再处理主题、动画、无障碍和诊断页。

每个功能页只负责展示和提交意图：

- `ModelFragment`：模型选择、预览、启用/停用、当前运行状态。
- `VoiceFragment`：语言、角色和语音包选择。
- `EnhancementFragment`：画面和其它增强开关。
- `DiagnosticsFragment`：框架版本、hook 状态、模型扫描结果、最近错误。

### 5.2 分辨率和字体验收

至少验证以下组合：

- 320dp 宽手机竖屏；
- 600dp 以上平板；
- 手机横屏；
- 系统字体 1.3x 和 2.0x；
- 深色模式和高对比度模式。

控件最小触摸区域为 48dp；长文本不得通过裁切解决；列表项状态和错误信息必须能在放大字体后完整显示。

## 6. 悬浮窗设计

### 6.1 生命周期

使用 `OverlayService` 管理 `WindowManager` 视图。Manifest 声明悬浮窗权限和前台服务能力，运行时引导用户到系统设置授权。服务只在用户明确开启悬浮控制且目标游戏存在时运行。

### 6.2 交互

- 折叠态：拖动位置、点击展开、长按隐藏。
- 展开态：显示四个核心动作和模块状态，不承载复杂设置。
- 设置和模型列表仍在主界面完成。
- 游戏截图或录屏场景下允许一键隐藏；服务重启后恢复上次位置但不自动执行动作。

当前实现：`OverlayService` 使用 `TYPE_APPLICATION_OVERLAY` 和前台服务；面板提供模型切换占位命令与隐藏按钮。modern 构建通过 libxposed service 的 `command.next` remote file 发送到注入进程，注入进程 Java 轮询后通过 JNI 进入 native 待消费队列；legacy 构建保留本地命令文件回退。native 在 Unity 资源交付回调上消费并通过 `command.status` 回报 `accepted`、`applied`、`rejected` 或 `unsupported`；模型切换在模型发布尚未开放前明确回报 `unsupported`，不伪造成功状态。

### 6.3 与 native 的同步

悬浮窗写入命令后只等待确认状态，不直接假定动作成功。native runtime 在 Unity 线程安全点消费命令并回报：`accepted`、`applied`、`rejected`、`unsupported`。命令超时后界面显示诊断入口，不循环重试。

## 7. 模型替换迁移路线

### 阶段 A：PC 端复用性审计

先建立 PC 端模型替换代码清单，至少分为三类：

- **直接复用：** 预设匹配、替换决策、缓存键、恢复原模型、失败降级、协议和测试数据。
- **抽象后复用：** 资源身份、资源交付、native 对象访问、日志、线程调度和 hook broker。
- **Android 重写：** ELF/AArch64 动态扫描、Android 资源链路、JNI/LSPosed bootstrap、Mesh 字段适配和 Android 生命周期。

审计完成前不复制更多桌面实现到 Android；每个复制的函数都要在清单中注明它属于哪一类以及对应的替代接口。

### 阶段 B：抽离共享策略

从桌面模型模块中抽出与平台无关的部分：预设匹配、替换决策、缓存键、恢复原模型、失败降级。Windows Host API、指针获取、日志和资源生命周期全部改为接口。

### 阶段 C：确认 Android 资源接入点

Android 不走 `I18NAssetLoader`。优先在真实资源链路中观察并关联：

1. `BundleLoader.AssetProxy.LoadAsync/LoadSync`；
2. `BundleLoader.BundleProxy.LoadAsset/LoadAssetAsync`；
3. `BundleResourceManager._LoadAssetInternal`；
4. `_FinishWithAsset` 作为资源交付边界观察点。

第一版只记录资源身份、Mesh 指针、线程和调用上下文，不写字段。没有唯一身份或上下文不完整时直接放弃该次替换。

### 阶段 D：复刻 PC 动态扫描和字段适配

在保留 PC 端扫描判定逻辑语义的前提下，实现 Android ELF/AArch64 只读探针：

- 从唯一字符串、交叉引用和 A64 `ADRP + ADD/LDR` 组合定位候选函数；
- 识别两个相互独立的归档/读取路径；
- 只有两个路径对同一偏移达成一致，且运行时值范围合理时，才确认字段布局；
- `m_BonesPerVertex` 的来源值只接受已观察的有效范围；
- 任何不确定、版本不匹配或扫描歧义都保持原 Mesh，不进行裸写。

这里的目标是把字段访问封装在 `MeshSkinMetadataAdapter` 中，而不是把某个 Unity 版本的偏移散落到模型替换逻辑里。

### 阶段 E：单目标实机替换

先支持一个已知角色和一个已知资源版本，覆盖：资源加载、Mesh 创建、替换、缓存命中、场景切换、角色销毁和恢复原模型。稳定后再扩大到角色索引和多模型预设。

## 8. 实施顺序

| 阶段 | 内容 | 退出条件 |
| --- | --- | --- |
| 0 | 冻结现有配置和 hook 行为，加入能力状态日志 | 旧版本行为可回退 |
| 1 | 新版 API 入口适配，旧构建变体保留 | 现代入口能在目标 LSPosed 上注入且不重复 hook |
| 2 | PC 端复用性审计 | 每个模型替换函数均标记复用、抽象或重写 |
| 3 | 抽离共享模型策略和 Android 资源适配接口 | 桌面构建不回归，Android 可只读观察资源 |
| 4 | 复刻 PC 动态扫描和 `MeshSkinMetadataAdapter` 探针 | 能对已知版本给出确定/不确定结果 |
| 5 | 单目标模型替换 | 替换失败时原模型完整保留 |
| 6 | UI Fragment 化和多分辨率 shell | 仅在界面改动涉及的范围内做定向验证 |
| 7 | Overlay Service 和命令通道 | 可切换模型状态，游戏重启后可恢复 |
| 8 | 扩展模型范围，最后移除旧 API 入口 | 现代 API 覆盖目标设备，旧入口无使用者 |

虽然界面可以在阶段 3 后并行开发，但模型替换必须先于悬浮窗的完整功能接入。悬浮窗第一版只显示状态和发送经过验证的模型命令。

## 9. 测试约束

### 9.1 模拟器作为传输完成后的首要验证环境

资源传输完成后，优先在已启动的 Android 模拟器上验证本次改动。测试前先确认 APK、VFS 和模块配置已经就位，但不重新搬运私有数据。手机只作为后续 ABI、性能和真实资源差异的补充验证设备。

### 9.2 只测改动部分

每次提交只执行与本次改动直接相关的定向测试。游戏测试必须在 `finally` 中关闭游戏进程并复位调试开关，避免持续占用模拟器资源：

- PC 代码审计或共享策略改动：运行对应桌面单元测试和构建目标；
- Android hook 或动态扫描改动：安装新 APK，查看 native bootstrap、资源身份和扫描结果；
- 模型替换改动：只验证目标角色、目标资源和恢复路径；
- UI 改动：只验证受影响页面的启动、保存和状态回显，不开展全量多分辨率巡检；
- 悬浮窗改动：只验证权限、显示、命令发送和服务停止。

在模型替换和 API 入口尚未稳定前，暂不安排大范围界面检查或完整回归测试。

## 10. 风险和回退

- **LSPosed 版本差异：** 新版 API 以现代入口为主，旧入口保留到实机矩阵通过；不能假定 API 102 在所有管理器上都可用。
- **Android ABI/Unity 版本差异：** 扫描失败、字段歧义或函数签名不符时关闭模型替换，不尝试猜偏移。
- **资源链路差异：** 不把 `I18NAssetLoader` 作为默认入口；通过资源身份和交付边界逐层确认。
- **悬浮窗权限和系统限制：** 权限未授予或服务被回收时只禁用 overlay，不影响游戏注入和模型模块。
- **跨进程配置损坏：** 发现 schema 或 generation 不一致时回退到上一个有效快照。

## 11. 第一批可执行任务

1. 建立 PC 端模型替换复用性审计表，标记共享、抽象和 Android 重写边界。
2. 新增框架适配，分别生成现代和旧版 APK，完成幂等 hook bootstrap 与配置同步。
3. 从桌面模型模块抽出 `ModelReplacementPolicy` 和平台接口，保证桌面构建继续通过。
4. 在 Android 资源路径安装只读观察 hook，记录资源身份、Mesh 指针和线程上下文。
5. 复刻 PC 端动态扫描判定流程，先只输出确定/不确定结果，不写字段。
6. 将 `activity_main.xml` 拆为 `ModelFragment`、`VoiceFragment`、`EnhancementFragment`，先保持原有功能不变。
7. 建立 `schemaVersion=1` 的配置快照和动作确认协议；悬浮窗命令通道已完成，下一步补 `accepted/applied/rejected/unsupported` 回执与模型策略绑定。

这七项完成后再决定字段写入点和悬浮窗的具体视觉形态，避免把尚未稳定的 native 接入点固化到 UI。传输完成后，后续每次只在模拟器执行与改动对应的测试。
