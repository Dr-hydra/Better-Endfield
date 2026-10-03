# 通用模型定位与身份验证方案（2026-10-03）

任务 A，研究稿。基线为 `main dd7131b4` 加共享工作区未提交改动；以下行号对应本次读取的工作区文件，不是只看 HEAD。已读 `android/AGENTS.md`、`WORLD_MODEL_BINDING_20261003.md` 及相关匹配、原始 donor、阴影和身份文档。本轮只新增本文和 ignored `artifacts/generic-model-matching-20261003/` 下的研究辅助文件；没有改生产代码、配置、UI 或他人的文档，没有构建、安装、启动游戏，也没有重新提取资源、复制依赖闭包或重跑上一轮测试。

## 建议决策

建议采用 **通用候选定位 + 完整原 Mesh 身份验证 + 唯一性验证**，让正常模式不再要求 Renderer 名必须等于 Mesh 名。首次加载按候选 Renderer 实际引用的 `sharedMesh` 找源 Mesh；缓存已改写时按完成记录中的原始身份及保存的 `Original` 找 donor。角色资源根、所属 LOD、骨骼、材质和布局继续参与验证，最后只能有一个合法接收者。

这里适度放宽的是 Renderer 命名条件。BEM 中的完整源 Mesh 名、原始索引计数、骨骼/alias、材质/texture pin、几何空间与上传结构校验继续保留。`_20`、`_8` 是完整 Mesh 名的一部分，不统一删除；大小写、`brow/eyebrow` 也不归一化。源 Mesh 名发生真实变化而旧包身份不符时，位置差异表不能替旧包改写源身份。

Android UI LOD0 → world LOD1 单独处理：两端引用不同 Mesh，必须有真实部件对应及各自准确的 Mesh 身份。名称改写最多产生待核对候选。差异表由引用元数据按平台、资源版本、资源根及 LOD 自动生成，仅在通用关系不能唯一定位时后备使用；不能继续扩展逐角色 `if`，也不能让表覆盖验证失败。

| 选择 | 能处理的范围 | 维护与限制 | 建议 |
|---|---|---|---|
| 通用定位后严格验证 | 同 LOD 的 Renderer/Mesh 不同名；有原始身份的缓存及克隆 | 必须处理 donor 来源、路径和多接收者歧义 | 主路径 |
| 自动生成的精确差异表 | 有证据的特殊路径、跨 LOD 对应、独立 proxy Mesh | 平台/版本分别管理；缺证据的字段不能发布为已验证 | 后备 |
| 手写每角色 Renderer 名 | 已知单点 | 易漏 `_8`、proxy 与缓存路径，持续依赖维护人 | 不继续增加 |
| 全局删数字后缀或按编号/数量选对象 | 不能证明源身份 | 同名、武器、VFX、阴影和多 LOD 可能混入 | 不采用 |

无需升级 BEM 或重转全部包。`mod_registry.cpp:94` 已从现有 BEM 元数据的 `component_names/original_counts` 创建 adapter；同 LOD 主路径直接使用这些完整名称和计数。运行时接收者身份、缓存来源和后备关系属于加载器内部数据。真实包元数据错误、原游戏身份更新、原先不支持的源布局仍是各自问题，不因定位改进自动转为兼容。

## 证据与覆盖边界

既有盘点见 [WORLD_MODEL_BINDING_20261003.md](WORLD_MODEL_BINDING_20261003.md)，明细为 `artifacts/world-binding-20261003/existing-renderer-mesh-name-audit.json`：

- 32 个 Windows 原生 graph、32 角色、64 个 world/UI 根，2,267 条去重后的可解析 Renderer → Mesh 绑定；版本为 `2954fa80-23c1-1579-2b22-4ecfd6d70418`。这是已有离线快照，不等于当前所有双端资源。
- LOD0 有 7 个角色、7 个 fur 组件、14 条 world/UI 名称差异。完整源名都是 `*_lod0_20`，Renderer 是 `*_lod0`。其中 6 个角色的 PC world LOD1 Mesh 为 `*_lod1_8`，Renderer 无 `_8`；另有大潘 UI LOD1 一条同类记录。这证明只改 LOD0 Renderer 名不构成完整通用设计。
- 280 条 proxy 绑定中，273 条 Renderer/Mesh 不同名。它们是独立的阴影路径，不计入主体命名问题。狼卫样本的 72 条非阴影记录全部同名，不能把其不加载归因于 fur 后缀问题。
- 艾尔黛拉、狼卫、提弗洛斯已有 Android prefab 提供真实根、Renderer/Transform 路径、骨架和外部 Mesh 引用；它们不能单独证明外部 Mesh 的当前 `m_Name`、材质内容、布局和 bindpose。Android 独立原始 probe 的 40 条记录只覆盖女管理员。
- 33 个 catalog 中，噗切娜缺该类完整 graph；其它未采集角色也不外推。7/32 是这个样本的计数，不是总体故障率。

本轮没有重做上述名称盘点。辅助脚本 `artifacts/generic-model-matching-20261003/derive_binding_relations.py` **只读取上一轮已完成的 audit JSON**，追加“引用关系与歧义”的推导，输出 `binding-relation-evidence.json`：

| 本轮推导 | 结果 | 能证明什么 |
|---|---|---|
| 同角色 UI/world LOD0 按精确 Mesh ID 配对 | 409 组；每组 UI/world 各一个 Renderer；无无法唯一配对的 LOD0 Mesh ID 组 | 可自动生成同快照共享源资产关系；不等于骨骼、材质、空间均已等价 |
| 同角色、同资源根、同 scope，共用 Mesh 的多 Renderer 组 | 16 组；直属 `lod0` 为 0；16 组均为 `other` | Mesh 引用相同仍可能有多个接收者；角色根和直属 LOD 区域都必须限定 |
| 每条 proxy 的 Mesh ID → 同根可见 LOD Renderer | 280 条均有一个引用相同 Mesh 的可见 Renderer | 可自动生成该 Windows 快照的 proxy 引用关系；不能直接变成 Android mobile 阴影行为许可 |

多接收者实例包括 `chr_0029_pograni_postmodel/Root/Soldier_Root/Rush01..04/P_wpn_misc_0033_01/Mesh_all/lod3/S_wpn_misc_0033_03_lod3`：不同接收者引用同一武器 Mesh。按“路径包含 Mesh_all/lodN”或“第一个相同 Mesh”会混入这种对象；这里的 `other` 是既有 audit 分类，不代表路径里完全没有 LOD。

另只读核对艾尔黛拉 catalog C9 与一份已有 `native.json` 的字段。C9 的完整 Mesh 名为 `S_actor_ardelia_fur_01_lod0_20`，原始索引总数 51,300；证据分别存 Mesh ID 与实际 Renderer 路径。catalog 有 `source_layout_supported=false`，这不能被绑定关系生成器改成 true。当前 `NativeAssetReader/Program.cs:124` 的导出类型没有 LODGroup；该 native graph 也没有 LODGroup 记录。不能假定已经采集了游戏的逐部件跨 LOD 分组。

## 当前代码：要改的是一条完整身份链

| 位置 | 当前工作区行为 | 通用设计要补足的部分 |
|---|---|---|
| `module.cpp:1906` `PrepareResource` | 先用 `ComponentRendererName` 匹配 Renderer，再排除若干路径；随后调用 `UseSavedOriginal`，用 `DonorMesh` 的完整名和索引数验证，最后准备骨骼、几何、材质 | 候选阶段就能读原始 Mesh 身份；按真正资源根和区域过滤，并对全部候选验证后检查唯一性 |
| `resource_policy.h:14` | 仅艾尔黛拉 C9 的完整 Mesh 名映射为无 `_20` Renderer 名，其余原样返回 | 通用主路径不依赖每角色名字映射；本轮保留现有代码，不增加特例 |
| `module.cpp:2333` `UseSavedOriginal` | 按 Renderer 名和同 root，或克隆当前 Mesh/material 对象找到 `old.original`；克隆按保存的骨骼相对路径重建本地骨骼数组；未找到 Original 也返回 true | 查找须带角色、路由、区域和接收者路径，区分未改写原资源、已知 custom 无 Original、Original 不可用和歧义 |
| `module.cpp:2389` `RememberResource` | 记录组件号、Renderer 名、自定义 Mesh/material 弱身份等；仅热切换开启时新增强持有的 `SavedOriginalBinding` | 普通模式也保存轻量的原始身份及接收者路径；强 donor 是否保留仍服从已有热切换策略 |
| `module.cpp:2427` `IsCompletedResource` | 同 root + selection key 直接成功；自然克隆按 Renderer 名及 Mesh/material、enabled、部分阴影/骨骼状态核验 | 缓存 donor 复用前验证实际逐接收者绑定；克隆用路径/区域和资产归属，避免只按名字找对象 |
| `module.cpp:2522` `ReadCompletedAndroidDonor` | 要求当前 selection 已完成，仍按映射 Renderer 名查找，读现有 custom 资产与骨骼，再尝试接 Original | 用同一已验证接收者记录读取 custom；不能因为已改写 Mesh 的名字像源名就走首次原版校验 |
| `world_resource_adapter.inc:53` | 映射后的源 Renderer 必须以 `_lod0` 结尾，再改末位为 `1`，要求准确 `Mesh_all/lod1/<Renderer>`；`76` 行又要求 world donor Mesh 与该 Renderer 同名 | world Renderer 定位与 world Mesh 身份分开；源/目标 LOD 使用真实对应关系，各自验证 |
| `world_resource_adapter.inc:172` | 从映射 Renderer 去掉 `_lod0` 生成 mobile proxy 名及路径，保存状态后关掉代理 | 根据原始引用/已验证关系找 proxy；不从源 Mesh 后缀猜主体或代理名字 |

因此只把两处 `ObjectName(renderer)` 比较替换为 `ObjectName(sharedMesh)` 不够：缓存中的 Mesh 已改写，Original 查找和完成记录仍依赖名字，Android 又有不同 LOD 的源 Mesh 与 proxy。需要所有入口共用同一种接收者身份。

新 Mesh 目前会取 donor 的原名（`module.cpp:1198`），所以 **Mesh 名相同不证明 pristine**。现有 `PreparedBinding.original_*` 是提交失败要恢复的当前绑定；`donor_*` / `SavedOriginalBinding` 才可能是首次原版 donor，二者不能合并。

## 通用定位合同

### 1. 先确定资源边界，再建索引

以实际交付的 GameObject 为边界；角色和 world/UI 路由由现有精确 resource 匹配确定。遍历必须确认 parent 链到达这个对象，生成相对于该对象的路径。不能把 `BuildTransformPath` 的字符串第一个 `/` 后都当成真实资源相对路径：当前函数还会走到更外层 parent，深度上限为 24（`module.cpp:475`）。新辅助逻辑应明确报告未到达 root、循环、深度超限和重复路径。

主体候选只来自该角色直属、实际存在的 `Mesh_all/lod0` 区域；Android 目标使用其已证实的直属 world LOD 区域。存在 LODGroup/引擎逐部件引用时读取真实归属；现有数据没有时，使用准确的已知 prefab 区域合同，不猜其它结构。嵌套武器、NPC 根、VFX、`Shadow_Proxy` 和其它 LOD 分别标记，不能凭名称或顶点数并入。无替换项目的区域保留原行为；结构未知则报告缺对应证据。

每次交付构建一次候选索引，包含：实际 Renderer 对象及受检查的弱身份、root 相对路径、区域/LOD、当前 Mesh/material/bones/enabled，以及可获得的 pristine 身份/完成记录。不能仅用 `mesh pointer -> renderer` 的单值 map；值必须是候选集合。

### 2. 从 sharedMesh 取得源身份，先确认它属于哪一代

首次未改写资源：读取 `renderer.sharedMesh`，完整 `ObjectName(mesh)` 必须等于 BEM adapter 源名。名字比较对象从 Renderer 改为实际 Mesh，Renderer 名只保留为诊断或后备位置证据。索引计数仍是额外兼容检查，不能独立证明身份。

已知完成资源：先以接收者 key 和当前 custom Mesh/material 弱对象身份确认归属，再读保存的 pristine 身份。有 `Original` 时使用它验证和构建；没有时只允许复用已核验的同 selection 已完成结果，不把 custom 当原版重建。未知来源且不能证明未改写的 donor 不自动接受。

离线的 `CAB/file + PathID`、运行时的 Unity instance ID、弱对象身份和 Mesh 名是不同层面的证据。当前 BEM 和引擎合同没有提供运行时读取序列化 PathID 的通用入口；不能宣称运行时已能直接按离线 Mesh ID 定位。离线 ID 用于生成/检查关系，运行时主路径用实际对象、完整源名、资源区域及兼容合同验证。

### 3. 对候选做完整验证，再判唯一

先以纯读取/已有 payload 的计划数据筛选，不在候选搜索中建 Mesh、复制材质或发布状态。`UseSavedOriginal` 当前可能为克隆分配骨骼数组，不能直接作为每个候选的无副作用谓词；建议拆出只读 lineage 查询，唯一候选确定后才沿原构建路径解析实际 donor。

| 验证维度 | 保持或增加的判断 |
|---|---|
| root / 接收者 | 实际 descendant、准确相对路径、正确角色及 world/UI 路由；相同位置的多个对象仍视为冲突 |
| LOD / 角色区域 | 实际所属区域符合源或目标 LOD；主体不能被 proxy、嵌套武器、其它 LOD 取代 |
| pristine Mesh | 完整源名、活对象/已验证原始记录、适用的原始索引数与子网格边界；多个同名不同 Mesh 不能合并为同一身份 |
| 骨骼 | BEM 的跨组件 donor 和源 bone index、准确名称或已声明 alias、可用 bindpose、有限数值；需要路径的映射检查 parent/相对路径及顺序 |
| 材质 / texture pin | 原 donor 的 component/slot/名称；同名 Texture 必须按真实对象消歧；多属性指向同一个 Texture 的已有合法行为继续保留 |
| 空间 | 保留 `SameMeshSpace` 等现有合同；不同空间必须有明确支持的变换，不能靠骨骼名称相似推断 |
| 布局 / 上传 | 保留包声明、stride/字节边界、索引范围、skin palette 和新 Mesh 回读；有对应平台原始布局证据时可作候选指纹，不能只凭数量猜身份 |

布局必须区分“原 donor 的身份指纹”和“替换 Mesh 的布局”。当前 `BuildMeshFromComponent:1159` 已明确替换使用 BEM 声明，Android 原 Mesh 可以有不同压缩布局。**不能新增要求 PC BEM 与 Android 原 Mesh 声明逐字节相等，也不能要求 LOD0/LOD1 顶点、索引和骨骼数组等长。** 原始布局资料缺失时记录缺失，保留实际构建结构校验；不要用近似布局做兜底或将缺证据标为已验证。

跨组件 palette/material donor 的验证需要完整映射计划；先收集各组件候选，再验证依赖约束。只有整个映射唯一并且每个组件的合同成立才进入准备。独立检查都通过但仍有两个接收者时，必须拒绝歧义，不能按顺序、相似名称、距离或顶点数排序选一个。

### 4. 同 Mesh 多 Renderer 的处理

同一 Mesh 可以被 world/UI、主体/proxy、不同 LOD 或不同子实例共用。先限定本次 root、路由和区域可排除许多共享者；剩余仍是集合。骨骼路径、root bone、材料槽和空间在有准确预期证据时可进一步消歧。

若两者仍完全符合合同，只能使用已验证的接收者路径/逐部件引用关系进一步限定。没有这种证据就拒绝并列出两个路径。默认不“把同 Mesh 的所有 Renderer 都替换”；若未来需要合法一对多，必须用已验证的明确接收者集合表达并逐个准备，而不是通用自动广播。多个 BEM 组件意外争用同一个接收者也拒绝；替换输出共用 Mesh 不代表接收者身份可合并。

## 保存原始身份与缓存已改写 Mesh

建议保存两层资料，避免默认模式为定位增加整套原资产的长期强引用。

| 内部记录（设计名） | 主要内容 | 生命周期 |
|---|---|---|
| `ReceiverKey` | 稳定角色/资源路由、root 相对路径、区域/LOD、receiver 类型；组件号只在当前包内使用 | 跟随有效完成记录；root/Renderer 用受检查的弱身份，不保存长期裸指针 |
| `PristineIdentity` | 首次成功准备时确认的完整 Mesh 名、适用计数/子网格、可用原始 Mesh 弱身份及骨骼/材质/布局证据；proxy 对应也保存 | 普通模式也保存元数据；不额外强 root 原纹理或整个 prefab |
| `CommittedIdentity` | 当前 selection key、custom Mesh/material 弱身份、骨骼路径/状态、enabled/shadow 状态、生成资产归属 | 用于同代复用、克隆归属、过期清理与复用前回读 |
| 既有 `SavedOriginalBinding` | 原 Mesh/material/bones 强 donor、bone paths、enabled 与 Android shadow 信息 | 继续只在已有热切换策略要求时强持有；不自动开启实验开关 |

`PristineIdentity` 在覆盖前从验证通过的 donor 取得，成功提交才随 completed 发布。失败仍走当前事务，不能把待发布的身份登记为当前事实。`original_*` 保留事务开始时的当前状态作为回滚目标，构建始终从已证实的 pristine donor 读取。

缓存场景的动作必须明确：

| 状态 | 动作 |
|---|---|
| 原版首次交付 | 实际 sharedMesh 完整源名匹配，完整验证并保存轻量原始身份，再正常准备 |
| 同 root、同 selection 已完成 | 复用前按接收者 key 回读 custom 绑定；使用已验证结果，不重验 replacement 索引数是否等于 original 数 |
| 已完成 UI donor、同 selection、普通模式无 Original | 可以复用核验的 custom 输出与本地骨骼；几何是否替换使用保存的 `generated_mesh`/准备动作判断，不凭 Mesh 名或空 donor 指针猜测 |
| selection 改变、保存了 Original | 用正确接收者的 Original 验证新包并重建；当前旧包状态单独保存，失败恢复旧包 |
| selection 改变但无 Original，或保存对象失活 | 拒绝重建，保留当前对象；不能降级为当前 custom donor，也不为此关闭校验 |
| 自然克隆共享已知 custom Mesh/material | 核对角色、路由、接收者路径/LOD与完整归属，再按克隆 root 的真实骨骼路径重绑；重复骨骼路径拒绝 |
| 当前对象被外部改变、归属不一致或存在多个 Original | 作为失配/歧义处理；同 root 或同名不能掩盖变化 |

保存的 Original 可来自旧 selection，条件是它仍是相同资源合同的 pristine 来源；新包仍完整验证。custom 复用则必须匹配当前 selection。生成资产用弱归属处理历史克隆，沿已有 `PruneCompletedResources` 清理，不能为便于匹配无限保留旧代资产。

`UseSavedOriginal`、`IsCompletedResource`、`ReadCompletedAndroidDonor`、关闭包恢复和 proxy 恢复应共用上述 key；否则普通准备改对了，热切换或关包仍可能按同名误找对象。本文提出后续修改点，没有修改这些函数。

## Android UI/world 异 LOD 的真实对应

同 LOD 通用定位确定 UI 源接收者后，world 定位走独立的对应合同：

1. 首选本次原 prefab/引擎已经提供且可读取的逐部件关系，或此前针对同资源版本核验并保存的关系。仅“在一个 LODGroup 里”不证明每个子部件逐一对应。
2. 已有名字规则只能枚举候选。`Renderer _lod0 -> _lod1` 可以帮助检查当前已证实结构；不得把 `Mesh *_lod0_20` 直接变成 `*_lod1_20`，也不得从无后缀 Renderer 推断 world Mesh 无 `_8`。
3. 对候选 world Renderer，独立读取其 pristine Mesh 名、骨骼/材质/空间及平台布局证据，检查它与 UI 接收者的真实对应。多个候选仍通过时不选第一个。骨骼/材质相似或唯一候选本身不能代替逐部件关系的来源证据。
4. 若运行时没有足够的对应资料，才查询同平台、同版本、同根、同源/目标 LOD 的生成后备表。表给出准确 world Renderer 路径及 **world Mesh 名**，后者与实际 `DonorMesh(target)` 比较；不要求它与 Renderer 名相等。
5. 无关系、版本不符、external Mesh 名未确认、歧义或兼容验证失败，都拒绝该准备事务；不跨平台使用 PC 表，也不由表忽略失败。

艾尔黛拉的已知关系可作为评审例子：UI Renderer `S_actor_ardelia_fur_01_lod0` 引用完整 Mesh `S_actor_ardelia_fur_01_lod0_20`；world Renderer 为 `S_actor_ardelia_fur_01_lod1`。当前 Android prefab 对 world Mesh 只有精确 external ref；已有 graph 对该 ID 记录 `S_actor_ardelia_fur_01_lod1`，但还不能宣称重新核实了当前设备 payload 的名字。六个其它 fur 角色的 PC LOD1 `_8` 不能外推到它们的 Android Mesh。后备记录应分别标注 `reference-confirmed`、`mesh-name-confirmed` 和平台来源。

UI 用原 LOD0 的索引合同，world 用自己 LOD 的源身份合同；不能把 UI 的 original index count 套到 world LOD1。world custom 几何来自已验证的 UI 输出，骨骼使用 world 活对象，继续保留相对父路径及包内 alias、材质 slot 和已有 `M_actor_* -> M_actor_lod_*` 精确对应策略，不新增名称替换。

当前 adapter 对某些 UI/world bone rest space 差异记录日志后继续，且另有 `SameMeshSpace` 检查。新的定位方案不把 rest-space 日志当成对应关系证明，不承诺姿态/变形已实测，也不在本任务新增通用骨骼 alias 或改变该兼容政策。

world/UI 新准备结果继续使用已有 paired 事务发布，保存和恢复必须携带各自接收者 key。已完成 donor 的 selection 不符就不能复用它的 custom 结果。现有首次 world 返回 `prepared_ui` 的修复已经完成，不重复实施或重跑上传测试。

## shadowProxies 必须单独建关系

先在 pristine 索引中将主体、`Shadow_Proxy/SP_Mobile`、`SP_Desktop` 和其它代理分别分类。proxy 名与 Mesh 不同是正常资源关系，不可由主体匹配器吸收。

可自动判定的主路径是：同 root 的 proxy 原始 `sharedMesh` 精确引用到已唯一定位的可见 world Mesh，且 proxy 路径/平台类别与该角色资源合同一致。保存 `proxy ReceiverKey -> visible ReceiverKey/component`，然后按现有策略操作该 proxy。已有 Windows 的 280 条引用均可生成这种关系；这是离线引用关系证据，Android 必须从自己的原资源或已核验记录取得。

若 proxy 使用专门的另一份 Mesh，引用相等法不能确定 owner；查询有来源证据的精确后备关系。禁止用“删 `_20`/`_8` 再拼 `_shadowProxyMobile`”推断。一个原 Mesh 对应多个可见接收者时，仍需明确 owner/接收者集合；多个不同 proxy 可以合法服务同一组件，但每条关系都要核验。

缓存世界 Mesh 已换成 UI custom Mesh 时，不能再拿当前 Mesh 指针与原 proxy 直接比。优先复用提交前保存的 pristine/proxy 关系；有 Original 时可核对原始引用。缺记录且无法证明关系时拒绝猜测，不因所有主体共用 custom Mesh 而扩大代理隐藏范围。

保留当前策略：替换主体使用新可见 Mesh 投影，清空其旧 `shadowProxyMesh`，关闭已确认对应的 mobile proxy；隐藏部件同步处理。无几何变化且未隐藏的组件不额外改 proxy。只处理当前角色和平台的对应代理，不全局禁用，不自动改 desktop 策略。

proxy 自身的当前 Mesh/material/bones/enabled 与主体 shadow mode、shadow mesh 都进入同一事务。Original 用于关包恢复，当前状态用于提交失败回滚。资源确实未声明 proxy 时允许没有 proxy；已证实应存在的 proxy 缺失、重复或关系冲突必须显式失配，不能把零匹配静默当成功。

## 现有数据足以自动生成什么

| 数据 | 现在可自动生成 | 现在不能确认 |
|---|---|---|
| 已有 32 角色审计明细 | 2,267 条角色/root/路径/LOD或区域、独立 Renderer/Mesh 名、精确 Mesh/Renderer ID 关系；14 条 LOD0 名差异及 LOD1 `_8` 的精确记录 | 当前所有平台名字仍一致、材质/布局/bindpose内容一致 |
| 本轮引用关系推导 | 409 组同快照 UI/world LOD0 共享 Mesh 关系，16 组多接收者歧义，280 条同根 proxy 引用关系 | 409 组全都能转换或上屏，Android 同样关系，跨 LOD 逐部件绑定 |
| catalog + 已有 native graph | 按完整源 Mesh 名/精确 ID 连接 catalog 组件与真实路径；补入已具备的材料引用、骨骼相对路径/顺序、子网格与证据来源 | 把离线 serialized channels 直接当当前运行时声明；把未支持布局、excluded component 改成支持 |
| 三角色现有 Android prefab | Android 根、真实 Renderer 路径、骨架、精确 external Mesh ref，以及有待进一步验证的 LOD 对应候选 | 所有 external Mesh 的当前名字/布局、材质纹理内容；其它角色当前 Android 全表 |
| 现有 Android 女管理员 probe | 该次运行实际路径/Mesh 名、正常主体与 proxy 的区分 | 其它角色或当前运行的 Android 名称确认 |
| 未来首次已验证交付记录 | 本次运行接收者 key、完整 pristine 身份、custom 归属与已核实 proxy/LOD 关系 | 无 Original 情况下从元数据还原原几何、材质模板和贴图 |

离线生成器输入以现有 graph、catalog、receipt/probe 为主，按精确对象引用和 parent 链连接，沿上一轮的去重/冲突政策。可以输出名字差异与真实引用的候选表，缺字段保持空及未验证状态；不要求再抓所有包、更换 BEM 格式或分发原游戏 payload。

后备表的建议逻辑 key 为：

```text
(platform, asset_snapshot, character_resource_route,
 source_root, source_region/LOD, full_source_mesh_name,
 target_root, target_region/LOD, receiver_kind)
```

值分别保存 source/target 的准确 Renderer 相对路径、各自完整 Mesh 名与可得精确资产 ID、关系证据、骨骼/材质/空间/布局验证状态及来源。组件 C 编号可用于当前包 diagnostics，但不能作为跨包、跨 catalog revision 的唯一表键。mobile/desktop proxy 单列，不能混成主 Renderer 的名字 alias。

platform 与 asset snapshot 是游戏资源身份，不是 BEM 文件 generation、selection key 或本模块版本。相同 manifest Version 的 PC/Android 仍有不同 manifest Hash 和 bundle 内容，来源必须分别保留。当前匹配模块没有现成的完整资源版本/序列化 ID 参数；后续实现要先明确 Host/资源加载链能提供的版本来源。不能把某个日期硬编码成“当前版本”，也不能在版本未知时自动使用版本专属表。

同 LOD 主路径在无需差异表时不因缺版本表阻断：它仍按本次实际 Mesh 与包合同严格验证。表只在位置/关系证据不足时参与；若通用搜索已有多个候选，表必须具有真实独立的路径/引用证据才能消歧。完整源 Mesh、骨骼、材料或空间已经失配时，不再换一行表重试以掩盖失配。

生成候选资料可以立即使用现有小型元数据完成；进入可分发后备表还需要平台/版本 provenance、完整关系证据及对应 runtime 合同。当前资料不足以发布“所有 Android 角色 LOD1 Mesh 名已验证”的表。以后如遇具体缺字段，只定点读取所需对象元数据/既有日志，不全量提取外置硬盘。

## 后续实现范围与验收合同

以下为可拆分评审的后续实现计划，本轮没有实施或执行测试。

1. **统一接收者与 donor 身份。** 提供按实际资源 root 建候选索引的只读定位函数、保存轻量 pristine 身份；在 `PrepareResource`、完成记录、cached donor、Original 查询及关闭包恢复共用。先处理同 LOD 问题，原始身份和上传校验全部保留。
2. **Android 异 LOD / proxy 关系。** 为源/目标分别保存身份，替换目前 Renderer 名同时充当 world Mesh 名的假设。直接引用/已验证关系优先，生成表后备；未取得真实 Android 关系的角色不自动推广。已有已核实规则在迁移阶段仍需其验证合同，不能一次性删除校验。
3. **生成与版本接入。** 从既有元数据生成关系候选和冲突报告；确认 runtime provenance 接入及字段支持后，才发布精确平台/版本表。已知艾尔黛拉特例是否删除，取决于所有调用入口迁移并通过既有行为验收，而非先删掉再兜底。

| 后续需验证的场景 | 应有结果 |
|---|---|
| Renderer 无 `_20`，实际 pristine Mesh 保留 `_lod0_20` | 正常模式唯一定位成功；完整源名和原始索引仍核验 |
| Renderer 名“正确”，但原 Mesh 名/索引错误 | 拒绝；位置表不绕过源身份 |
| 同一 Mesh 在主体、proxy、嵌套武器或其它 LOD 出现 | 按 root/准确区域分开；无法唯一证明的两个主体拒绝 |
| 缓存 custom 名与源名相同、replacement 索引改变 | 同 selection 验证记录后复用；有 Original 的换包从原版重建；无 Original 不重建 |
| 克隆共享 custom Mesh/material，骨骼为新 root 对象 | 用本地准确路径重绑；重复路径、不同材料归属和错误角色拒绝 |
| world Renderer 无 `_8`，实际 Mesh 有 `_8` | 只有准确平台/版本对应已证实才接受；比较独立完整 world Mesh 名 |
| PC/Android 原布局不同、UI/world 索引数不同 | 不按跨平台/跨 LOD等长误拒绝；仍执行各自身份和 BEM 上传结构校验 |
| proxy 共享原 world Mesh、proxy 使用独立 Mesh、proxy owner 歧义 | 分别走精确引用、已验证后备、拒绝；无全局隐藏 |
| 世界/UI/proxy 任一准备或提交失败、关包恢复 | 不发布半套身份；按当前事务恢复各字段，保存的 Original 不覆盖当前回滚目标 |
| 版本未知、换平台、版本表过期、未解析 external Mesh | 不使用无适用性证据的后备关系；保留当前对象并记录具体原因 |

诊断仅进开发日志，包含角色/路由、源完整 Mesh 名、候选路径及数量、当前/Original 来源、selection、目标 LOD 和明确的失败字段；不增加 UI 说明文字。预期收益是降低名字差异导致的拒绝及逐角色维护成本。一次交付建索引可减少重复枚举，但本轮没有计时或游戏测试，**不保证 FPS、加载时间、上传峰值或内存改善**；强 donor 的现有热切换内存成本仍存在。

## 本轮检查与材料

- 只读核对 `PrepareResource`、`ReadCompletedAndroidDonor`、`UseSavedOriginal`、完成记录、Mesh 构建及 world/proxy 事务；研究按当前未提交改动编写。
- 新增关系推导脚本只运行一次，读取已完成 audit JSON，没有再跑原盘点、原生读取器、BEM 转换或旧回归测试；上述新计数不属于游戏实测。
- 本文路径/引用、UTF-8 和尾空白检查通过；`git diff --no-index --check -- NUL docs/GENERIC_MODEL_MATCHING_DESIGN_20261003.md` 通过，ignored 辅助文件状态已确认。文件保持未提交，其他共享工作区改动不回滚。

相关说明：[原始 donor 与热切换](MODEL_HOT_SWITCH_REVIEW_20261001.md)、[已有匹配政策](BEM_MATCHING_REVIEW_20261001.md)、[Android 阴影与 LOD](ANDROID_LIGHTING_SHADOW_LOD_20260921.md)、[原资源身份资料及限制](BEM_EFMI_IDENTITIES_20260920.md)。这些文档记录的既有构建或实机结果不是本轮新增验证。
