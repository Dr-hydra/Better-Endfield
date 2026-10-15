# Android 1.6.4 BEM 链路定向核查

2026-10-15；设备 `78572d34`，游戏 `com.hypergryph.endfield` **1.6.4 / 61 / arm64-v8a**，BE 4.0.3。用户确认语言资源尚未下载，手机无法进入游戏；本轮停止手机开屏/启动/配置试验，只使用已取得的新版代码与 metadata、只读设备现有包和设置做静态检查。

## 当前结论

本轮所查 BEM 实现没有发现需要更换函数或原生网格布局的新版差异：**69 项方法合同、10 项 Unity 字段合同全部匹配**；同一安装的 `libunity.so` 中，生产探针要求的两种独立具名序列一致确认 `m_BonesPerVertex=0x1C0`。

这不是 Android BEM 运行时验收。当前包与 Android UI LOD0→world LOD1 对应表仍引用旧资源 snapshot；没有加载 1.6.4 的实际 UI/world donor 骨骼、材质与网格，也没有执行绑定 commit 或恢复。新代码合同通过不能证明这些资源身份未变化。

## 当前现有包与只读导出

设备登记了一项启用包：

| 项目 | 值 |
| --- | --- |
| package_id | `native.hiragara.tohsaka-rin.aglina.prototype` |
| package version | `1.2.6` |
| BEM 容器 | `1.0`，magic `BEM\0PKG\0` |
| 目标 | 洁尔佩塔 `chr_0013_aglina` |
| 当前选项 | `enabled=true`、appearance `rin` |
| 字节数 | `10,077,562` |
| generation | `6800d797-bf69-4b5f-ba23-3c967104ea7e` |

设备原文件：

```text
/data/user/0/com.hypergryph.endfield/files/betterendfieldnext/installed-models/6800d797-bf69-4b5f-ba23-3c967104ea7e.bem
```

已仅导出该文件到：

```text
inputs/1.6.4/Android/bem-validation/tohsaka-rin-aglina-1.2.6-device.bem
```

来源、文件大小、登记的目标与选择保存在同目录 `source.json`，未计算 hash。通过现有 `bem_v1.read_package(..., decode=False)` 读取包结构和 manifest，不展开贴图或整包资源。

**实际 manifest 的 `target.platform` 是 `windows-x64`**，profile=`chr_0013_aglina-pc`，revision=`20260920-identities1`，snapshot=`2954fa80-23c1-1579-2b22-4ecfd6d70418`，world/UI 目标分别为 `chr_0013_aglina_postmodel` / `chr_0013_aglina_uimodel`。这是 Android 现有 legacy 适配器支持的 PC 目标 BEM，不是含四份独立资源的 BEM 1.4 包。安装登记的 Windows/Android 四个 `resource_keys` 是 native 旧格式适配登记生成的，不代表包内含四份显式平台资源。

原 manifest 已保存为本目录 `device-package-manifest.json`。它可用于 Windows 同包代表性验证；Windows 的结果须单独报告，不能外推手机 donor 身份已验证。

## GUI 到 Host 的生产路径

1. `BemInstallPage` / `BemInstaller` 处理包检查、启用和外观选择，将 generation 与选项写入 `installed_bem_packages`，通过既有 framework 发布文件及设置。修改选项走 `OverlayWritePolicy` 与提交流程，防止覆盖其他包。
2. `BemInstalledResources.prepare` 按 generation / remote 名称检查归属和大小，复制到游戏私有 `installed-models`，临时文件写完并同步后原子改名；只纳入启用包，生成包含 packages、appearances、options、parameters 的 custom-model 配置。
3. `RuntimeBootstrap.customModelConfig` 在 Release 读取 `BemInstalledResources.configuration`，随后通过 `BETTER_ENDFIELD_NEXT_CUSTOM_MODEL_CONFIG` 交给 native。Release 不使用 debug 系统属性覆盖。
4. Android `CustomModelModule` 配置 MeshData builder 后，调用共享 custom-model module 初始化，按元数据解析方法与字段，安装资源交付与主线程 pump 钩。
5. 资源交付入口是 `Common.Beyond.dll / Beyond.Resource.Runtime / BundleLoader.AssetProxy._FinishWithAsset(Object):Void`。实际资源引用走 `FAssetProxyHandle`；本链路没有名为 `ResourceRef` 的硬编码类型依赖。

本次只读检查没有安装、删除、禁用或切换包，也没有改实验选项。读取时游戏未运行。已找到明确可测试的目标包，但依用户指示不再进行手机启动试验。

## 当前代码与布局证据

复用上一轮 ELF/metadata 定向解析器，增加可选目标类与 metadata-only 模式；输出范围是 21 个相关类、854 条方法元数据，**没有全量 dump 或反编译库**。验证脚本 `check_bem_contracts.py` 从生产代码提取此次范围内的合同，逐项对比参数、返回值、参数数目与程序集/类身份。

`contract-check.json` 的 **69/69 方法、10/10 字段** 匹配，主要覆盖：

- `ResourceManager.IsInitialized` / `IsAsyncIniting`。
- `StringPathHash.CreateStringPathHash`；`I18NAssetLoader.Load(StringPathHash, Type, RootCategory)`。
- `FAssetProxyHandle.Get():Object` / `Dispose():Void`。
- `BundleLoader.AssetProxy._FinishWithAsset(Object)`。
- MeshData 只读/可写分配、构造、GetItem、指针与尺寸、顶点声明、索引与 submesh 设置、ApplyAndDispose。
- `SkinnedMeshRenderer` sharedMesh / bones、`MeshFilter` sharedMesh、`Renderer` sharedMaterials / enabled / shadow 属性。
- `Object.m_CachedPtr`、`MeshData.m_Ptr`、`MeshDataArray` 指针/长度、VertexAttributeDescriptor 四字段及 SubMeshDescriptor 首尾字段的实际布局。

这里只将核对过的十项 Unity 字段偏移作为布局证据；导出的其他类中包含受文件掩码影响的原始字段表读取，不作为未经核实的私有偏移依据。生产资源句柄通过运行时元数据及托管调用处理，不依赖本报告推导的句柄内部偏移。

单独从同一安装读取 `inputs/1.6.4/Android/libunity.so`（30,305,720 字节），没有获取 VFS。`check_native_mesh_layout.py` 按生产 `ProbeLoadedUnityMeshLayout` 的 descriptor 和 reader 序列扫描，结果见 `native-mesh-layout.json`：

| 准入项 | 结果 |
| --- | --- |
| `m_BonesPerVertex` 具名地址 | `0x1674CE` |
| descriptor path | 1 |
| reader path | 1 |
| 不同序列化函数一致数 | 2 |
| 唯一字段偏移 | `448 / 0x1C0` |
| 生产准入条件 | 满足 |

所有地址均来自 Android ARM64 安装，未搬用 Windows RVA。该离线扫描没有写 setter、没有读写运行中对象内存。

## Android donor、LOD1 与恢复

旧格式包的 world 路径由 `world_resource_adapter.inc` 处理：

- 确认 ResourceManager 已初始化且不在 async init；按 UI 资源全路径创建 StringPathHash，经 I18NAssetLoader 同步取得 `FAssetProxyHandle`，用 GC handle 保活，再以 `Get()` 得到 UI donor。
- 以 UI LOD0 的确切目标身份准备替换；枚举 world 的 LOD1 receiver 和 Transform 路径，读取 world 自身 bones，校验原始网格、材质数组、对应关系及相对 mesh space。
- 当前生成表 `android_lod_relations.generated.h` 的 snapshot 仍为 `2954fa80-23c1-1579-2b22-4ecfd6d70418`。本次没有新 Android donor 采集，不能把该表描述为已按 1.6.4 更新。
- UI donor 若尚未 commit，与 world 合入同一事务；若 UI 已完成，读取保存的原始身份与现有结果，防止拿替换后的网格计数再次验证原包。
- 发布前保存 original mesh / materials / bones / enabled / shadow。提交失败调用恢复；`RestorePreparedBinding` 分别尝试各字段，不因前一项失败短路。恢复失败则保留可能仍被 renderer 引用的生成资源，不销毁仍在使用的对象。
- 正常禁用后的实时恢复要求本次进程已启用 hot switch；未启用时设置变更在重启后生效。Android legacy world 禁用会同时处理已经修改的成对 UI donor；不能只恢复 world 而留下 UI 替换。
- UI handle 在 scope 结束时 `Dispose` 并释放 GC handle。开发 rollback 分支已存在，但本轮不为验证而开启它。

上述是源码与离线合同证据，不是已经在新版手机资源上执行过。若实际 target path、骨骼、材质或 donor 身份改变，运行时校验仍可能拒绝替换并保留原资源；本轮未关闭这些校验。

并行离线回归已完成：使用 JDK 21 / `--release 17` 编译生产设置类，`OverlayWritePolicyTest` 与 `BemHotSwitchUpdateTest` 均通过；`AndroidWorldBindingTests` **213 项通过**，binding `--android-discovery` 的显式 LOD1 与 legacy 配对场景也通过。执行记录见 [离线验证](../../../1.6.3/Windows/bem-chain-audit/offline-validation.md)。这些使用离线 fixture，不计作手机新版 donor 运行结果。

## 运行证据边界

只读查看的是上轮 **10:02** 的 BE 专属 diagnostics，曾记录当前包目标、`Resource runtime enabled: mods=1`、`Android MeshData replacement transaction ready`。这些说明此前一次初始化成功，**未包含该包资源 `Resource committed` 或 `Android renderer commit/restore PASS`**，不是本次 BEM 运行验收。本轮未产生新的手机加载日志。

目前可以确认新版挂点、托管 MeshData API、实际 Unity 私有骨骼字段准入与代码恢复链；尚不能确认 1.6.4 Android 实际 UI/world donor、外观效果、LOD1 动作、切场景与运行时恢复。待手机具备进入目标场景的资源后，用现有凛包做一次 UI donor → world LOD1 → 恢复即可，无需重新全量扫描。

本轮没有修改生产 native 代码或设备设置，也没有下载语言包、全量资源或计算产物哈希。
