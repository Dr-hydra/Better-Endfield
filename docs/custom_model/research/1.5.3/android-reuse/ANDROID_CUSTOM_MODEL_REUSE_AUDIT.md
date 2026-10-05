# Android 自定义模型替换：复用审计与执行记录

本文件记录实际代码边界和验证结果。任务仍在执行，不能将编译通过理解为模型替换已通过游戏测试。

## 已确定的模块边界

`native/modules/model/module.cpp` 是登录展示、动画及装饰控制；Android 的 `LoginModelModule` 已经使用其 Host 适配器。用户要移植的 BEM 自定义模型替换位于 **`native/modules/custom_model/`**，此前没有进入 Android 构建。两个模块须保留独立配置和状态，不能以登录模型 hook 数量代替 BEM 功能验收。

| 代码及函数族 | 结论 | 实施方式 |
| --- | --- | --- |
| `bem.cpp`: `ParseBem`、`LoadBem`、`ReadBemPackageInfo`，容器/网格/纹理解码校验 | 直接共享 | 已建立 `BetterEndfield.CustomModelCore`；PC/Android 编译同一源文件及 Zstd 解码器 |
| `mod_registry.cpp`: `ParseModRegistry`、`ModRegistry::Match`、配置和冲突判定 | 直接共享 | 同一静态库；将 `.bem` 扩展名比较改为可移植字符串 |
| `resource_policy.h`: `EffectiveLodEnabled`、`CommitResource` | 直接共享 | 保留失败接收者也参与逆序恢复、恢复失败不销毁已绑定对象的语义 |
| `module.cpp`: `DecodeComponentSkin`、`DeclarationsEqual`、BEM/renderer 兼容性判定 | 抽象后共享 | 区分纯数据校验与依赖 Unity 调用的步骤；不重写包协议 |
| `PreparePalette`、`PrepareDrawMaterials`、`PrepareResource`、`ProcessResource` | 抽象后共享 | 保留完整准备后提交的事务顺序；对象分配、托管数组及引擎调用由 Host/平台接口提供 |
| `ConstructionScope`、强/弱 GC 句柄、`RememberResource`、完成资源和 payload 缓存 | 抽象后共享 | 接入 Android IL2CPP exports；销毁在游戏线程执行，不能只移植容器逻辑 |
| `BuildMeshFromComponent`、纹理/材质构造、`ApplyPreparedBinding`、`RestorePreparedBinding` | 策略共享，调用层适配 | 保留创建前校验、字段初始化、上传后回读和恢复；重查 AArch64 icall ABI |
| `ResolveEngineBindings`、`SafeResolveIcall`、`SafeSet*`、`SafeQuery*` | Android 重写调用适配 | `libil2cpp.so`/`libunity.so`；不可用 C++ `catch` 冒充 Windows SEH 的非法访存保护 |
| `MappedImageSize`、`ResolveNativeMeshLayout` | Android 扫描实现重写 | PC 保留 PE/x64 解码；Android 用 ELF/AArch64 字符串交叉引用和函数边界，保留证据不足拒绝写入 |
| `GetNativeObjectPointer`、`TryReadNativeUInt32`、`TryWriteNativeUInt32` | Android 内存适配 | `m_CachedPtr` 从元数据解析；禁止照搬 PC 固定位移或假设 `/proc/maps` 校验即可保证对象存活 |
| `ResourceFinish`、`ResourcePump` | 交付策略共享，接入层适配 | Android 按完整方法签名解析真实链路；已加入只读 `_FinishWithAsset` 探针，尚未证明目标 Mesh 交付完整覆盖 |
| `LodField`/LOD 强制与恢复、`GetTickCount64`、`GetCurrentThreadId` | 平台适配 | 元数据字段/静态字段访问，单调时钟和游戏线程身份；必须保留停止时恢复 |
| `DllMain`、Windows 模块固定/卸载和 Host hook 退休接口 | Android 重写生命周期 | JNI/bootstrap、Dobby hook 所有权及进程生命周期；首版不声明支持 native 热卸载 |
| PC creator 工具、BEM 生成、manifest | 继续在 PC 复用 | Android 消费已验证的 BEM 包；不将桌面制作界面移植进注入进程 |

## 已落地代码

- 共享核心 CMake 目标同时用于 PC 自定义模型模块、PC 绑定测试、Android 模块和跨平台 `BemValidate`。
- `legacy`/`modern` 两个 APK 构建变体，分别依赖旧 API 82 与新版 API/service 102.0.0。相同包名保留设置，可覆盖安装切换；不能同时安装。
- 共用 `RuntimeBootstrap`：目录准备、首帧加载、最多三次加载尝试。框架相关 hook 留在各自入口中。
- modern 设置通过官方 service 发布配置快照；UI 本地设置保留，框架不可用时不丢配置。当前运行时仍在启动时读取，实时命令尚未接入。
- Android IL2CPP 增加嵌套类型元数据解析，供完整签名资源方法解析使用。
- 调试只读资源探针：`debug.betterendfield.resource_probe=1` 启用，生产包不因该属性启动；原资源链继续调用一次，限制日志频率，不写 Mesh 字段。

## 已完成验证

- PC Release `BetterEndfield.CustomModel` 和 `CustomModelBindingTests` 构建通过。
- 使用 `tools/CustomModel/test_bem_v1.py:fixture` 的专用测试包，PC 绑定测试通过：解析、精确 donor 身份、材质隔离、回滚、所有权、外观/LOD 路由。
- Android ARM64 解析器在模拟器执行通过：合成包的 default/hidden 两个外观；实际 `gilberta-two-appearances.bem` 的 default/outfit-b 两个外观（4 个组件，分别 1/7 张纹理）。这验证 BEM 消费，不代表移动端纹理格式或渲染兼容性已验证。
- `assembleLegacyDebug`、`assembleModernDebug` 编译通过。SDK 37.0、Build Tools 36.0.0、AGP 9.1.1、Gradle 9.3.1 已安装/接入，保留 NDK 27.2.12479018 和 targetSdk 35。
- 模拟器最终使用 NeoZygisk 2.4 + Vector 2.2 (3080)，CLI 返回 API 102。ReZygisk 1.0.0 无法识别该 root 环境，已停用。仅启用本模块并作用于 `com.hypergryph.endfield/0`。
- 现代入口在游戏内实际加载成功，`UnityPlayer.nativeRender` 首帧后进入 native runtime。
- 资源探针已命中 `_FinishWithAsset`，观察到 `LoginController`、`LoginRootPanel`、`SK_actor_female` 等真实资源交付，原链路继续运行。
- 修正了模拟器 ARM64 转译代码的误判：宿主 `/proc/self/maps` 缺少 `x` 时，进一步核对已加载 ELF 的 `PT_LOAD/PF_X`，仍拒绝任意可读地址；Dobby 在此环境安装并命中成功。
- 已提供 `android/Test-ResourceProbe.ps1`：显式指定设备，安装 debug APK、定时采样、finally 关闭游戏并复位属性；日志采集不自动判定测试通过。
- 已加入响应式内容壳：平板/宽屏将设置内容限制为 840dp 并居中，手机维持全宽；尚未做全量分辨率巡检。
- 主界面已补充独立诊断页，显示 modern/legacy 框架入口、悬浮窗权限与开关、模型配置状态以及 Mesh 只读扫描边界；模拟器启动、UIAutomator 页面切换和诊断文本可见性验证通过，未启动游戏。
- 已加入 `OverlayService` 和命令泵：modern 使用 Vector remote file `command.next`，注入进程 Java 转 JNI，native 在 Unity 线程队列消费；当前仅记录接受，不改变模型状态。
- 命令泵已补齐回执协议：native 接收时写入 `accepted`，Unity 线程按当前处理能力写入 `applied` 或 `unsupported`，modern 入口通过 `command.status` remote file 回传，悬浮面板显示状态。`model_toggle` 目前明确回报 `unsupported`，不会伪装成模型替换成功；回执通道已编译验证，尚未做点击悬浮按钮的完整端到端实机测试。
- 最新命令/UI 定向检查：modern/legacy APK 均构建通过；modern MainActivity 和 OverlayService 在模拟器启动/停止无崩溃，未启动游戏。Endminf 资源链复测仍命中 11 个 Mesh，游戏结束后 PID 为空。
- 已加入 `CustomModelModule`：从 debug 配置读取 BEM 包，使用共用解析器加载 `gilberta-two-appearances.bem`，在模拟器真实 `_FinishWithAsset` 链路匹配 `SK_actor_female`，确认 4 个组件；当前明确停在 `publication withheld until MeshSkinMetadataAdapter is authorized`，没有改动渲染器。
- `native/modules/custom_model/module.cpp` 现在进入 Android CMake 编译目标，复用 PC 端的准备、校验、事务提交和恢复代码；Windows PE/SEH/模块生命周期通过 `platform_compat.h` 和 Android ELF resolver 适配。Android 编译目标把桌面导出符号改为私有名字，避免和现有登录模型导出冲突。该接入目前是策略代码与平台适配的编译集成，实际 Android 运行仍由 `CustomModelModule` 的证据门控路径负责。
- 最新定向结果：双 APK 构建通过；PC `CustomModelBindingTests` 通过；Android 静态扫描工具仍报告 1 个具名归档引用、唯一候选 `0x1C0`、`writable_layout_confirmed=false`。使用 `endmin_in_casualwear.bem`（default 外观）在模拟器真实 `_FinishWithAsset` 链路匹配 `SK_actor_female`，确认 11 个组件；日志仍为 `publication withheld until MeshSkinMetadataAdapter is authorized`，没有 Mesh/renderer 写入。测试结束后游戏 PID 为空、调试属性和临时 BEM 文件已清理。
- 在同一次目标测试中，`CustomModelModule` 的匹配路径枚举出 Endminf 的 11 个真实 Mesh；候选值可读并出现 `1`/`4`，例如 `S_actor_endminf_cloth_03_lod0=1`、`S_actor_endminf_body_01_lod0=4`，每条均标记 `evidence=candidate only; writes disabled`。这确认 Android 资源身份、Mesh 对象读取和字段观察已串起来，但不改变可写证据门槛。
- 扫描器现在读取 Android `.eh_frame_hdr` 的函数边界，并在同一函数/同一基址寄存器上检查 Mesh 邻近字段，避免把所有 `+0x1C0` 位移直接算成目标字段。最终模拟器报告 `archive_functions=1`、`field_read_functions=80`、`field_write_functions=93`、`clamp_sites=2`；静态报告为唯一 archive 函数、多个同位移消费者。由于 archive 方向仍只有一条具名引用，且这些消费者还需要类型/寄存器来源归属，`MeshSkinMetadataAdapter::Write` 额外要求至少两个 archive/read/write 函数并继续无条件返回 false。
- 扫描器进一步识别了“同一函数从输入结构 `+0x14` 读取、在短指令窗口内写入候选 `+0x1C0`，并同时触及至少三个 Mesh 邻近字段”的复制链。静态 `mesh-report-v5.json` 只得到一个函数 `0x87AD48`；模拟器动态证据对应 `copy_functions=1`、`copy_offsets=0x87ad48`。这确认了一个可继续做方法级参数/上下文审计的候选复制函数，但仍没有第二条独立归档方向，因此默认路径不安装地址 hook，也没有开启写入。
- 进一步加入了显式 `copy_hook=1` 的调试开关：运行时按扫描结果动态安装 `libunity.so+0x87ad48` 的只读参数追踪 hook，并成功在模拟器报告 `copy probe hook installed`；20 秒目标资源测试无崩溃，但没有观察到该函数在当前 `_FinishWithAsset` 启动窗口内被调用。这说明候选地址可被动态解析和安全安装，尚不能证明它覆盖新 Mesh 构建路径，默认配置仍不安装该 hook。
- 又加入了显式 `create_hook=1` 的 `Mesh.Internal_Create` icall 只读探针。模拟器实测动态 hook 安装成功并捕获 16 次新 Mesh 创建，原始日志中仅 1/16 个候选值为 `1`，其余 15 个为 0 或大数，没有 `4`；此前把资源 Mesh 的 1/4 与创建日志混淆，现更正。该结果仅确认 hook 被调用，不能证明新 Mesh 字段路径有效，但没有把它误判为字段 setter，字段写入仍关闭。
- 静态反汇编补出了两条更具体的候选链：`0x87AD48` 在复制多个 Mesh 邻近字段时把输入结构的 `+0x14` 写入目标 `+0x1C0`；`0xD97A70` 在处理每顶点骨骼数据时读取、累加并回写 `+0x1C0`，随后使用 `+0x1C4` 的索引区。`0x6E1D04` 还展示了按标志确保该槽位已分配的生命周期路径。它们比单纯的 clamp 计数更接近 PC 的字段语义，但尚未完成 IL2CPP 方法元数据映射和参数方向证明，所以仍不能直接安装地址 hook 或打开写入。
- 曾对 `Object.Instantiate(Object)` 做过仅限 debug 配置的 ABI 试探；当前 `ResolveMethodExact` 签名不足以证明这个重载，调用触发了游戏 native 崩溃路径。该探针已撤掉并用正常 Endminf 资源测试复验通过；后续若走克隆路线，必须先完成精确重载解析和 Unity 线程/生命周期验证，不能把它当作现成 setter。
- 最终定向回归（2026-09-20 08:10）：安装 modern debug APK，使用 `resource=SK_actor_female` 和 `endmin_in_casualwear.bem` 启动游戏 20 秒；方法审计输出 14 条，BEM 校验和 11 个组件匹配成功，`FATAL`、AndroidRuntime、JNI bridge 未实现错误均为 0。测试结束后 PID、调试属性和临时 BEM 文件均为空/已清理。
- 后续证据回归（08:42–08:59）：静态复制链报告保持唯一 `0x87ad48`；默认资源测试报告 `copy_offsets=0x87ad48`；`create_hook=1` 测试安装 `Internal_Create` hook 并捕获 16 次创建，无崩溃；诊断页 UIAutomator 启动、切页和 Mesh 只读文案可见性验证通过，游戏进程始终未被 UI 测试启动。
- 已在真实游戏进程中记录 Mesh 方法和 icall 的加载地址，作为“方法级入口是否覆盖字段写入”的排除证据：托管 `Internal_Create`、`ClearImpl`、`UploadMeshDataImpl`、`GetBonesPerVertexValue` 解析到 `libil2cpp.so` 包装方法；对应 icall 分别解析到 `libunity.so+0x516904`、`+0x51A204`、`+0x51A714`、`+0x519510`，`InternalSetBoneWeights` 解析到 `libunity.so+0x5190F0`，`SetBoneWeights` 未解析。它们均未映射到静态候选 `0x87AD48`、`0xD97A70`、`0x6E1D04`，因此目前不能据此安装 setter hook；`InternalSetBoneWeights` 仍只能作为观察/排除点。
- 方法审计还确认：候选字段访问存在于多个 native 函数，但现有 Unity 方法元数据无法把这些函数归并为稳定的公开方法级写入链。继续复刻 PC 动态扫描时，必须同时保留函数边界、归档读写方向、对象来源和参数方向证据；未满足条件时适配器保持只读。modern 入口的 JNI 状态轮询已改为只有存在待处理命令时才探测，失败后退避 10 秒，避免 native bridge 尚未加载时重复调用 `status()`。

## Android 字段扫描证据

从已转移 APK 的 ARM64 `libunity.so` 提取静态证据，当前找到了一个名称引用窗口：

```text
归档函数入口（ELF VA）0x167F92C：x20 <- x0 (Mesh)，x19 <- x1 (archive)
0x16809A0  ADRP x1, 名称页
0x16809A4  ADD  x3, x20, #0x1C0
0x16809A8  ADD  x1, x1, 名称页内偏移   // m_BonesPerVertex
0x16809AC  MOV  x0, x19
0x16809B0  MOV  w4, wzr
0x16809B4  BL   0x969EF8
```

可复现入口：`python tools/CustomModel/inspect_android_mesh_layout.py <libunity.so> --output <report.json>`（需要 capstone）。该工具只输出候选和反汇编证据，永远不将候选标为可写布局。

这些地址只是当前包的研究证据，不是生产配置。当前局部 ADRP+ADD 搜索只得到一个候选引用，尚不符合 PC 的两个独立序列化路径一致条件。**不得将候选 `0x1C0` 直接写入 Android 代码。** 下一步扩大引用分析并追踪归档辅助函数的读写方向；若 Android 合并了读写序列化代码，需要证明等价的独立验证链，不能直接降低证据门槛。

## 下一步及未完成项

1. 新版入口/首帧/资源交付/实际 Mesh 观察已验证；继续验证配置修改与快照同步。
2. Android 运行时扫描探针已实现并实测；下一步补齐归档参数来源/独立消费者的结构验证、拒绝路径测试和可写适配器，目前无 Android 字段写入实现。
3. 抽离共享引擎操作接口，完成 BEM 模型替换事务，验证一个目标角色的替换和恢复；当前已完成包校验和资源匹配，几何发布仍未开放。
4. 响应式内容壳和悬浮窗命令通道已实现；功能页拆分、命令回执和模型策略绑定尚未实现。
5. 每次只测涉及模块，游戏测试使用 `try/finally` 关闭进程并复位调试探针；不传输手机私有数据。

官方依赖依据：[API/service Maven 发布](https://repo.maven.apache.org/maven2/io/github/libxposed/)、[Vector 2.2](https://github.com/JingMatrix/Vector/releases/tag/v2.2)、[AGP 9.1.1 与 API 37 兼容表](https://developer.android.com/build/releases/agp-9-1-0-release-notes)。

## 运行时扫描进展（06:25 定向测试）

`mesh_layout_probe.cpp` 已在已加载 `libunity.so` 上执行扫描，入口通过 `dl_iterate_phdr` 获取 ELF 段，使用 `process_vm_readv` 只读复制，按 AArch64 指令匹配名称引用和字段地址计算。无固定字段偏移。实际结果为：

- 唯一候选 448（十六进制 `0x1C0`）；1 个具名归档引用；2 个非栈字段读取后 clamp-to-4 的指令窗口。
- 从 `SK_actor_female` 取得女管理员 body/cloth/face/hair/iris 等真实 Mesh，候选位置可读，观察值包括 1 和 4。
- `GetBonesPerVertexValue` 对这些 Mesh 均返回 4，但 cloth_03、cloth_05、iris 的候选字段实际为 1。**该 getter 与私有字段语义不同，不能用其返回值初始化新 Mesh。** 后续仍须沿用 PC 的“从原 Mesh 原生字段读取并复制”策略。
- 当前运行时扫描结果仍是 `candidate only; writes disabled`。两个 clamp 窗口目前只计数指令地址，尚未证明来自两个独立函数，也未完成全函数寄存器来源验证，不能直接升级为可写布局。
- runtime 字段读使用内核自进程读取接口；候选不唯一、无法读取或未知指令时不写任何对象。
- 本次仅做 native 资源与扫描测试，没有进行界面巡检；游戏已在 finally 中关闭，调试属性归零。

本次证据位于本地 `artifacts/android-refactor/probe-20260920-062532/`；这些为调试产物，不纳入发布包。生产化时应缩减/移除 getter 代码字节采样，只保留解析状态和必要诊断。

## 当前优先级（用户恢复后）

优先打通 BEM 导入到游戏内模型替换；暂停扩大 UI 和悬浮窗改动。先核实运行时 Mesh 对象路径，再完成共享构建/提交/恢复链路。创建探针的异常值不能视为布局验证通过。

## 最新进展与人工游戏测试交接

- APK 磁盘代码与已加载的 Unity 可执行代码存在差异。对已加载可执行段分析后，发现描述器函数 `0x167F92C` 与读取器函数 `0x1681778` 均通过 `m_BonesPerVertex` 名称关联到同一动态字段偏移。运行时记录为 `archive_functions=2 descriptor_paths=1 reader_paths=1`；这些地址仅为本次证据，不是平台配置。
- 动态扫描增加读取器模式，验证函数参数来源、Mesh 寄存器、归档寄存器以及回调槽位；字段适配器要求两种路径和两个函数同时成立。`mesh_probe=1` 仅创建独立 Mesh、复制原 Mesh 字段、回读并销毁，不绑定渲染器，不改原资源。
- `resource=auto` 使用 BEM 的 world/ui 资源身份。点击登录页继续后，实际观察到正式资源 `chr_0003_endminf_postmodel`；仅观察 `SK_actor_female` 或开屏后固定等待 20 秒不足以覆盖正式角色加载。
- 管理员测试包：`endmin_in_casualwear.bem`；当前 debug 配置 `mesh_probe=1;resource=auto;package=/data/local/tmp/e.bem`。记录 `detached Mesh field initialize=PASS` 才能确认本轮字段写入实验成功；该记录仍不代表完整几何或外观替换成功。
- 用户接管所有游戏内操作。助手只更新模块、说明有变化的测试步骤并读取日志，不再自动点击、切角色或关闭用户正在测试的游戏。安装新 APK 后，已运行游戏仍使用原先加载的 native 库，需要用户重启游戏才能测试新版。
- 待完成：正式角色匹配与独立 Mesh 写入结果、共享构建/提交/恢复链路、单目标实际外观替换。现代 debug APK 最新构建通过；没有扩大 UI 测试范围。

### 人工游戏测试回读（最新）

用户进入主游戏后，`resource=auto` 命中 `chr_0003_endminf_postmodel` 与 `chr_0003_endminf_uimodel`。对正式角色 Mesh 的独立对象实验记录 `detached Mesh field initialize=PASS source_value=4`，随后独立对象销毁成功，原渲染器保持不变；本轮没有 AndroidRuntime/FATAL。该结果确认字段适配器的受限写入与回读可工作，但不等于完整模型替换。
