# Windows 1.6.3 BEM 运行时接口定向审计

2026-10-15。范围：当前 `G:\Arknights Endfield` 与共享生产 `native/modules/custom_model`。本轮没有发现需要修改的新版 Windows BEM 方法或字段合同；这不是所有现有模型包已实机验收的结论。

## 本轮结果

| 检查 | 结果 | 证据边界 |
| --- | --- | --- |
| 当前源码的 Windows 方法描述符 | **88/88 匹配**，其中必需项 65 | 复用当前 1.6.3 定向元数据，按程序集/类/方法名/参数数/参数类型/返回类型匹配；未把 Android 条件项混入 Windows |
| CustomModel 挂钩 | **8/8 解析**，7 个 `ok`，1 个无直接调用者 | 复用已有局部调用扫描；无直接调用者的可选 clone 入口不据此判失效 |
| 8 个 Mesh 原生 icall 名称 | **8/8** 在当前 UnityPlayer 发现精确字符串与指针表引用 | 这是注册资料证据，不单凭字符串宣称原生调用 ABI 或 GPU 上传已实测 |
| `m_CachedPtr` | 当前元数据偏移 **0x10**，类型 `System.IntPtr` | 运行时仍动态解析，无写死旧 GameAssembly RVA |
| Native Mesh `m_BonesPerVertex` | **0x1C8，2 个独立序列化函数一致** | 直接调用生产 `ResolveNativeMeshLayout` 对当前 UnityPlayer 的普通字节映射分析；未 `LoadLibrary`，未执行游戏 DLL |
| 值类型内存布局 | 当前 `SubMeshDescriptor`、`VertexAttributeDescriptor`、`Bounds`、`Matrix4x4`、`BoneWeight`、`BoneWeight1` 与使用的结构相符 | 只增量提取缺少的三个相关类型，无全量 dump/IDA 库 |

输入新鲜性通过先前证据的文件大小和修改时间比对确认；未计算任何新产物哈希。机器可读结果为 [contracts.json](contracts.json)、[value-layouts.json](value-layouts.json) 和 [native-layout-result.json](native-layout-result.json)。

## 资源加载到绑定的链路

| 阶段 | 当前入口/合同 | 审计结论 |
| --- | --- | --- |
| 资源交付 | `BundleLoader.AssetProxy._FinishWithAsset`，RVA `0x5318670` | 当前入口有效，既有扫描 `ok`；先确认实际 GameObject 资源，按 resource root 及接收者定位进入准备逻辑 |
| Unity 线程任务 | `Canvas.SendWillRenderCanvases`，RVA `0x59E2D60` | `ok`；异步准备/纹理上传/配置更新经现有 Unity 线程 pump 完成 |
| world/UI renderer | `GameObject.GetComponentsInChildren(Type,bool)`、Transform/Renderer/SkinnedMeshRenderer/MeshFilter 相关描述符 | 所有 Windows 合同匹配；world/UI 名称、Mesh 和骨骼身份由具体包和实际资源继续核对，不能由方法匹配替代 |
| 骨骼/材质/网格 | sharedMesh、bones、sharedMaterials、bindposes、Material/Texture、Mesh 调用合同 | 方法和字段未发现漂移；写入后已有网格/骨骼/材质/启用状态回读检查 |
| LOD | `HGCullingSystem.set_parentLODBias` / `SetArtTagLODBias` | 两个挂点均 `ok`；相关读取与设置方法匹配 |
| 克隆观察 | Object Internal Clone/Instantiate 四个入口 | 三个 `ok`；`Internal_InstantiateSingleWithParent@0x5E66340` 无直接调用者，仍是可选路径，未发现确定失效证据 |
| 关闭某模型/切换 | `RestoreDisabledResource` → 原始绑定准备 → `CommitResource` | 使用已保存且归属明确的原始 Mesh、bones、materials、enabled；恢复不因前一个字段失败而跳过后面的字段 |

生产代码按方法描述符和元数据定位，而非依赖 Windows 旧地址。`InitializeResourceModule` 在必需合同、引擎绑定或挂钩失败时拒绝启动/停用对应入口，Host 的安全钩链退休接口仍是必需条件。

“关闭模型恢复”与“模块 shutdown”需要区分：热切换关闭模型经恢复事务；`ShutdownResourceModule` 明确**保留已经发布的模型绑定**，关闭任务和观察钩，不承诺卸载模块即把所有可见模型改回原版。这是当前既有生命周期设计，不能把资源采集清理或钩子退休解释成模型恢复实测。

## 原生布局细节

当前生产代码硬依赖的未装箱布局已对照：

- `SubMeshDescriptor`：Bounds@0，topology@24，indexStart@28，indexCount@32，baseVertex@36，firstVertex@40，vertexCount@44，总大小 48，与 `SubMeshDescriptorRaw` 相符。
- `VertexAttributeDescriptor`：attribute/format/dimension/stream 分别为 0/4/8/12。
- `Bounds`：center@0、extents@12；`Matrix4x4` 16 个 float 依现有列布局连续排列。
- `BoneWeight`：4 个 float 权重和 4 个 int 索引共 32 字节；`BoneWeight1` 为 float+int 共 8 字节。

Mesh 原生字段解析器不是读取固定版本偏移：它定位 `m_BonesPerVertex` 序列化名称，局部解码对应函数，要求至少两个序列化函数给出同一偏移，否则拒绝。当前真实 UnityPlayer 已满足此条件。8 个上传/查询 icall 的当前名称及指针表引用列在 `contracts.json`；其运行时返回及实际数据上传交由隔离实机验证补充。

## 352 条新角色记录的适用范围

既有 [CAPTURE_RESULT.md](../runtime-catalog-update/CAPTURE_RESULT.md) 和 [RUNTIME_IMPORT.md](../runtime-catalog-update/RUNTIME_IMPORT.md) 记录：祀 world/UI 与明河三个 world 资源根共 352 个 renderer 的资源身份、网格顶点/索引数、骨骼顺序、材质槽、bindpose、布局与离线资料匹配。它们说明当前资源加载和运行时读取链可以工作，**不证明旧有 33 角色目录的每个包在新版中均已匹配，也不证明新角色已经可在正式工具一键转换**。

- 祀暂存 15 个配对部件，其中 8 个布局符合现有转换规则、7 个不支持；源 buffer/texture 身份仍待补齐。
- 祀 cloth_04 的 world/UI 材质组合不同，不能伪造为等价双根。
- 明河三个变体各 18 个主部件，均只有 world 资料；现有双根入库器未将它们加入正式库。
- 正式 catalog 未覆盖，pending/staging 仍是研究资料。

旧包运行时仍按原 Mesh、骨骼路径/顺序、材质槽和资源根验证。若大版本改动了某个角色资源，即使这里的接口全部有效，该角色包仍可能拒绝或需要更新。最小实机补充应是现有真实旧角色包的一次开屏交付、观察 Mesh/材质/骨骼绑定、然后关闭并确认恢复；不需要为此再次采集全部 352 条新角色记录。

## 可复用入口

- `audit_contracts.py`：对当前 Windows 方法表复用既有元数据，汇总挂点、原生 icall 名称和布局证据。
- `current_player_layout.cpp` / `CMakeLists.txt`：将当前 PE 读入 RVA 对齐的普通 byte vector，直接复用生产布局解析器；不加载或执行 DLL。
- `build/Release/bem-current-player-layout.exe "G:\Arknights Endfield\UnityPlayer.dll"`：本次实际运行结果已保存，不必无改动重复检查。

本审计只在本研究目录写入脚本、报告和辅助构建文件，未更改任何生产 native 代码或正式 catalog。
