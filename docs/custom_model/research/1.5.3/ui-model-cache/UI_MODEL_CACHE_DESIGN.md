# UI 模型成品资产缓存设计（2026-10-03，任务 C）

建议增加**按原始 donor 与兼容性寻址的有界 Mesh/Texture 成品池**，把“获取成品”和“绑定这个 UI 实例”拆开。每个接收实例重新解析自己的 bones、创建或沿用自己的 `privateMaterials`；成品池不保存上一实例的 Transform、动画状态或整套 UI prefab。Mesh 与 Texture 使用不同的依赖 key，只有几何参数改变时可以重建 Mesh、继续使用兼容的 Texture。

**零重构只指：兼容、仍存活的成品命中时，模组不再新建/上传对应 Mesh 与 Texture。** 游戏仍可加载原始资源、执行 `instantiateUI`、建立展示对象、骨架、Animator、物理和私有材质；这不是“每次打开 UI 都完全零加载、零分配、零卡顿”的承诺。淘汰、资源版本变化、不兼容或原生对象失活均允许重新构建。

本文为可评审的后续实现方案，**没有实现或实机性能结论**。基线为 `main dd7131b44cd6f99bdcd67386cd8a778249c1e396` 加读到的未提交改动；本任务只新增本文。已检查仓库/目录祖先的适用 AGENTS.md，读到的约束为 `android/AGENTS.md`：Android 实验功能和相机 UI 不添加说明文字。未改生产代码、配置、其他研究文档或包，未构建、安装、启动游戏或操作设备，未重跑上一轮研究/测试，未全量提取、复制或 clean 外置硬盘，也未回滚他人改动。

## 1. 证据与已有工作边界

先读 [热切换与 legacy 路径研究](../hot-switch/HOT_SWITCH_LEGACY_PATH_RESEARCH.md)，沿用其 donor/rollback 分离、联合事务、实例登记与退休限制；本文只细化成品复用。原生加载与单次上传峰值分别参照 [原生加载研究](../loading-memory/NATIVE_MODEL_LOADING_RESEARCH.md) 和 [上传峰值研究](../loading-memory/MODEL_UPLOAD_PEAK_REVIEW.md)，不重复反汇编、包分析、日志提取或现场测试。成稿时参考并行 [任务 B 的全局加载队列](../loading-memory/TEAM_MODEL_LOADING_DESIGN.md) 的 decoded/上传总账；后续 donor 定位接入 [任务 A 的通用身份合同](../matching-identity/GENERIC_MODEL_MATCHING_DESIGN.md)，不另建角色特例。未修改这两份文档。

下表行号对应本次读取的工作区，`module.cpp` 指 `native/modules/custom_model/module.cpp`。2026-10-03 16:28 +08:00 复核其 SHA-256 为 `E12AF65FCFB842676437A1078BC824F9973C516421F182E414719F6D923E0BC0`；并行任务后续可能改变行号，以函数名为准。

| 当前事实 / 定点依据 | 能得出的结论与限制 |
| --- | --- |
| `ProcessResource:2793` 先 completed 判重，再 `AcquirePayload`，再 prepare；`IsCompletedResource:2427` 支持同 root/key 与完整生成绑定一致的自然克隆 | 可以跳过部分交付；没有“新原版 root 按 pristine donor 找现成成品”的入口。同 root 的快路径也没有重新检查实际绑定。 |
| `AcquirePayload:2559`：128 MiB、10 秒 CPU payload 缓存；超限不入池。淘汰使用容器头，不是命中后移到末尾的完整 LRU | CPU 命中不等于 Mesh/Texture 命中，延长此 TTL 不能单独解决 GPU 重建。不能把 payload 缓存与本方案混称为一个缓存。 |
| `PrepareResource:1955`、`ApplyTextureMask:1772`：纹理复用表局限于本次 prepare；优化开启后跨组件共用 `(本次 texture index, 原 Texture 对象)` | 已有事务内去重，不是跨 UI root/跨事务成品池。索引来自选中结果，不能直接作为跨选项稳定身份。 |
| `BuildMeshFromComponent:1148`、`CreateTextureFromBem:1486` 每次 prepare 新建资产；纹理走 ctor → raw data → `Apply(false,true)` | 再次 prepare 可能再提交对应 API 请求。Apply 返回、CPU 不可读不证明 GPU staging 已回收。 |
| `CopyMaterials:1677`、`PrepareDrawMaterials:1812` 克隆 donor 材质；`PreparePalette:1722` 和 `UseSavedOriginal:2333` 处理 palette/实例骨骼 | 可以保留私有材质与实例骨骼，同时共享适用的成品 Mesh/Texture；不能直接复用前一 UI 的 bones 数组。 |
| `bem.cpp::ApplyMorph:497` 只改位置增量；`Decode:590` 将 manifest texture id 重映射到当次输出 tid，`Decode:686` 应用 Morph | 当前位置 Morph 给出了细分 Mesh/Texture 依赖的源码依据；仍需保留稳定 manifest/payload 身份，不能仅从输出 tid 或参数名推断贴图不变。 |
| `world_resource_adapter.inc:17` 获取 UI donor；`:20` 可读取已完成 donor，`:25` 将新建 UI sources 与 world 联合提交 | 本轮已有 world-first 重复构建修正须保留，不能将其重新列为待修缺陷。后续新 UI root 的复用仍有独立缺口。 |
| `android_mesh_builder.cpp:356,367` 的 `AndroidLoadUiDonor` 使用同步 Load/Get，临时持有 boxed `FAssetProxyHandle`，作用域退出 Dispose/free root | 已有获取/归还句柄基础；“UI donor loaded”日志只说明调用成功，不证明每次真的从盘重载，也不证明 Dispose 后资源立即卸载。 |
| `SavedOriginalBinding:2094` 强持有原 mesh/materials/bones，Android 还含 shadow mesh；`PruneCompletedResources:2477` 用弱 mesh/material 存活保留记录 | 原材质可能被 Original 自身强引用维持。新池自己的强引用也可能让 generated 弱引用一直存活；不能拿“任一 weak 对象还活着”当外部消费者证明。 |
| `ConstructionScope:350,1621` 记录本次创建资产，未发布则清理；`WeakObject:1980` 校验 weak target/native-live/instance ID | 添加借用成品后必须区分 owned 与 borrowed。不能让失败析构销毁池内共享资产；weak 存活不等于游戏 retain。 |

既有上传研究记录了三轮各 8 次 Texture Apply、387,856,640 B 请求量，后两轮为 UI。本文只引用其结论，没有重新读取日志。它支持“重复提交值得消除”，不提供 root/donor/retirement 身份，不能证明同一原版对象被重载、缓存泄漏或未来节省多少毫秒/显存。该样本是 8K ASTC，也不代表所有 4K 包。

已有 PC dump 的 `Gameplay.Beyond.dll.cs:324626–324635` 声明 ModelManager 的 persistent pool、ObjectLru、GameObject→handle 表，`:324649–324669` 有 Load/LoadAsync、instantiate 完成与 Unload 声明；`CharUIModelMono:325120` 包含 Animator、骨骼、物理和展示状态。这只是历史 PC 类型声明，未核实当前 Android 分支/运行覆盖。本文用 `instantiateUI` 表示游戏创建 UI 展示实例这一阶段，`privateMaterials` 表示每实例材质所有权，**不据此声称存在已解析、已验证的同名 hook/字段**。

## 2. 三条链分别优化

```text
游戏链：请求/资源池 → 游戏原资源 Load → instantiateUI / 池中对象激活
        → Animator、物理、表情、材质状态 → 展示对象退出/回池

模组冷链：选择快照 → BEM 必要 payload 解压 → Mesh/Texture 创建与上传
          → 完整校验 → 事务绑定

模组命中链：小型计划 + 原 donor/兼容性核对 → 借用存活 Mesh/Texture
            → 本实例 bones/privateMaterials → 事务绑定/读回
```

主方案优化模组链。新 UI 的 GameObject 身份变化不应使所有成品失效；其 donor 变了或兼容性无法证明，则应失效。游戏原资源真的重载需要单独的 loader 身份/请求证据；成品命中并不消除这段成本。

只把缓存查询加在 `PrepareResource` 内仍会先触发 `AcquirePayload`，不能保证避免解压。应在 payload 获取前先用已验证的**小型不可变计划**做查找；计划包含选中组件/纹理条目、依赖、布局和绑定配方，不保存完整 decoded vectors 或 Unity 实例。完整命中不读/解压 payload；部分命中只为 miss 读取所需块。当前 `LoadBem` 会解码整份选中结果，选择性解码是后续明确工作项，不能把它说成已有能力。

## 3. 身份与 key：分层保存，逐层验证

所有 key 都带 schema/builder revision。哈希只用于定位桶，命中后比较规范化描述，避免 CRC/裸地址碰撞导致误复用。包 revision 必须指向不可变文件；不在 UI 打开路径全包 hash，不依赖文件名、时间/大小冒充内容身份。Android 可承接已有不可变 generation；PC 后续须在导入阶段固定 revision 与文件 lease，未建立不可变身份前保守拒绝跨 revision 复用。

| 层 | key / 描述必须包含 | 明确不作为共享身份的内容 |
| --- | --- | --- |
| `SelectionSnapshot` / `PlanKey` | 不可变 package id + revision；BEM major/minor、decoder revision；规范化有效 options/appearance、UInt32 参数 ticks；adapter/component 规则 revision；校验/优化模式；资源平台/游戏资源版本 | 单独路径、mtime/size、当前 registry 裸指针。当前 `mod_registry.cpp:102` 的 selection key 是起点，不是内容摘要。 |
| `ResourceContext` | PC/Android、架构/资源平台、实际 graphics backend/支持格式、Unity/游戏资源 build 或 manifest generation、adapter 版本；资源逻辑路径与 UI/world/LOD 类别 | 仅角色 id；没有可靠资源版本时使用进程内保守 epoch，不能跨进程持久复用 Unity 对象。 |
| `PristineDonorIdentity` | 原 Mesh 的 weak token/native-live/instance ID + 捕获代际；原材质与槽、shader、原 Texture 来源；资源/组件身份；可审计的原版 provenance | 已替换的 custom A、上次 UI root、对象名/index count、可能复用的裸地址。reload 后同名同路径也不自动等价。 |
| `MeshKey` | package/revision + 该组件选中几何/索引/draw 稳定 id；影响该几何的 options/精确 Morph ticks；输出 attribute/stream/stride/index format/topology/submesh/bounds；skin 格式/influences；原 donor；有序 bindpose 矩阵及 mesh space；palette `(donor component, bone index, 唯一路径)` 有序映射和适配变换；builder/resource context | 前一实例 Transform 地址、当前动画姿态。Mesh 自带 bindposes，只看 bones 名称或长度不够。 |
| `TextureKey` | package/revision + **稳定 manifest texture id、payload id/内容身份及条目描述**；确实影响该条目的 options 投影；dimensions、format/graphicsFormat、mips、sRGB/linear、解码/转换版本；原 Texture donor；完整 sampler 描述；resource context 与必要 shader/slot 兼容证书 | 当次 `BemPocData.textures[t]` 的 t、仅 payload id、仅原纹理名。相同 payload 可以有不同 format/sRGB/sampler，不能直接合并。 |
| `BindingRecipeKey` | 完整 selection snapshot + donor/resource context；draw/keep/hidden/enabled/shadow 策略；shader 身份/版本、keyword、render queue、材质复制规则、texture pin 与槽语义、palette/mesh space 兼容证书；所引用 MeshKey/TextureKey | 某一 UI 的可变 Material、表情/发光/dither 数值、MaterialPropertyBlock、Animator、Transform。 |

完整 selection key 始终留在计划和绑定记录中，**不同资产 key 只投影自身依赖**。例如只有 Morph ticks 改变：PlanKey/BindingRecipeKey 改变，受影响 MeshKey 改变；如果 resolver 证实选中 texture 条目、donor、shader/slot 与 sampler 都相同，TextureKey 保持不变。依赖证据缺失时采用完整 options/参数 key 造成保守 miss，不能为了命中直接删除条件。

Mesh 的 layout 条件同时记录 BEM 输出声明和 donor 的 skin/palette 兼容信息。当前 builder 允许原 donor 与 BEM 顶点声明不同并按 BEM 重建；本设计不新增“二者必须相等”的要求。复用要求**已建成输出**与本次预期输出、bindpose 和 palette 语义一致。

shader/slot 兼容在每次材质绑定时验证。Texture 可在已证实等价的 shader 槽之间共享，但不能以“像素相同”省掉绑定证书。首版只接受同 pristine donor；跨 donor 或 UI/world 共用需现有适配器证明后才放开。Android world 的 LOD1 接收者与 UI LOD0 donor 不因同角色而合并。

sampler 至少包括 wrap U/V/W、filter、aniso、mip bias，以及该 backend/格式需要的采样语义。当前 `CopySamplerState:1468` 只核对 wrapMode/filter/aniso/mip bias，**不是完整 U/V/W 兼容证明**。未能解析/读取完整状态时，禁止扩大到不同 donor 的 sampler 等价复用；只有已验证的构建合同限定了采样状态、且能核对状态未变，才允许同 donor 复用，否则该层 miss。共享 Texture 创建后视为不可变，不对命中对象再次 Apply 或改 sampler，以免影响其他实例。

unchecked 构建不能直接满足 checked 命中。记录独立的 validation certificate/revision，严格模式须已有相应校验或重新校验；`skip_validation` 仍不能跳过 native-live、数组边界、资源身份及所有权安全。优化模式影响布局/解码或输出时纳入资产 key；只影响执行策略的差异也要有明确等价证明。

## 4. 缓存对象与每实例绑定

| 对象 | 保存 / owner | 生命周期约束 |
| --- | --- | --- |
| `PlanEntry` | 纯数据计划、依赖投影、验证证书、包文件 lease/adapter snapshot | CPU 有界；不持有 prefab、renderer、Transform、privateMaterials 或整份 decoded payload。 |
| `MeshEntry` / `TextureEntry` | 描述、来源、估算字节、generated id、weak Unity token；有限 cache strong lease | 只持有本模块成品，不反向拥有 BindingRecord/Original；分别计命中和驱逐。Texture 不借 Material 的强引用实现隐式无限保活。 |
| `InstanceBindingRecord` | weak root/renderer + 实例 epoch；desired/applied revision；该实例 Original、bones 有序弱身份/路径、privateMaterials 身份、生成资产 lease | 绑定成功的实例/任务负责必要恢复状态；事务内才提升 bones/材质到临时强数组，全局池只保存数值/路径配方。退出后拆掉实例状态，不将骨架带入闲置池。 |
| `OriginalAssetSnapshot` | 原 mesh/material/shadow 的资产级恢复资料、native-live token、明确 owner/lease | 同原 donor 可共享资产级 snapshot，但 enabled/shadow 模式及 bones 属于实例。不能由 completed 弱存活条件间接无限持有。 |
| `DonorLeaseOwner` | move-only 的 tracked `FAssetProxyHandle` owner、boxed handle root、请求代际、borrow count | Dispose 恰好一次；root 只保住句柄盒，不替代游戏资源 retain。不同时给多个 RAII 副本各做一次 Dispose。 |
| `BuildOwner` | 本 job decoded backing、reservation、仅本次创建的 Unity 资产、借用 leases | CPU 释放与 Unity 清理分开；不能保留栈 `ConstructionScope` 或 thread-local 的裸引用跨帧。 |
| `RetirementRecord` | generated Mesh/Material/Texture 的弱身份、已登记消费者计数、未知消费者状态、恢复义务 | 与未来命中池分开；不包含 root/skeleton 强引用。所有 retired 都要计账，不能移动到“不算预算”的容器。 |

对新 UI 实例，按接收 root 的唯一相对骨骼路径/组件和 palette 顺序重新生成 bones 数组，核对父层级、bone count、mesh space 与 bindpose；路径重复、缺骨骼或 native 失活即拒绝命中。不得把上一 UI 或 world 的 Transform 数组塞给它，也不得用正在播放的动画矩阵重算“静止兼容性”。如果游戏 clone 已带有正确的共享 Mesh/Texture，验证后可以继续使用，但其 bones、私有材质及恢复资料仍归这个实例。

与当前强 `SavedOriginalBinding.bones` 不同，拟议实例记录在事务外只保留恢复所需的有序路径/弱 Transform 身份，下一次从这个接收者重建临时数组；必要 Original 资产保护不包含旧 UI 骨架。root native-live 也不能单独证明实例仍在展示或池中有真实借用者，消费者状态必须来自已验证生命周期，避免记录自己的骨骼句柄把 UI 对象间接保活。

缓存 Mesh 也视为不可变。若后续表情、遮罩或其他功能确需改写几何，应借用 base 后生成独立、同样计账的派生 Mesh，或以完整依赖 key 建立变体；不得原地改写共享成品，让另一个 UI/world 消费者一起变化。

`privateMaterials` 优先沿用该实例已经验证、确属它所有的材质；新实例按原版材质/当前规则克隆再绑共享 Texture。MaterialPropertyBlock、表情、溶解、dither、发光、keyword 等可变状态不放入跨实例成品池。这样命中仍可能新建少量 Material/数组，不能把“Mesh/Texture 零新建”写成“所有 Unity 对象零新建”。游戏材质管理器后续可能回写 renderer；实例登记入口必须覆盖并核对实际绑定，不能依赖 completed 的同 root 快路径永久跳过。

## 5. activeTeam4 + selectedUI1：一个总账，短 TTL LRU

候选角色集合为 `unique(activeTeam[最多4] ∪ selectedUI[最多1])`，最多 5 个不同角色；selectedUI 在队伍内不增加角色槽，但可以多一个实例。仅候选资格不构成常驻 lease：需要实际存活接收者、构建任务或预算允许的短时预热。输入来自经过验证的队伍/详情生命周期和实例登记，未建立覆盖前不得用全局 FindObjectsOfTypeAll 扫描填满集合。

world/UI、多组件共享 Texture、同角色多实例共用**同一 AssetBudget**。按唯一资产 id 收费，引用数不乘容量；每个实例的 privateMaterials、Original、pending/retired 仍各按真实所有权计账。不得建立“每角色一个全额池”或 selectedUI 独立的第二个预算。队伍外 UI 关闭后进入闲置 TTL，不能继续当第六个活跃角色预热。

以下为**待评审的起步参数，不是实测最佳值或本轮配置修改**：

| 额度 / 策略 | 起步值与含义 |
| --- | --- |
| 成品/恢复资产共同总账 `B_asset` | 覆盖 5 角色并集的 active、pending、idle、retired/quarantine 及额外 donor/Original 保护成本，按唯一资产 id 去重。活跃角色的必要资产按实际需求记账，不以固定 512 MiB 门槛拒绝其它队伍成员；设备相关软目标用于约束预热、额外缓存和同时构建。 |
| 可选闲置强持有额度 `B_idle` | 起步至多 128 MiB，TTL 10 秒；仅限制额外留下的闲置成品，不能作为四角色必要常驻的硬门槛。命中更新 LRU，维护观察不续期。最后真实 borrower 释放时开始 TTL；关包取消该 revision 的复用资格，共享资产的其他合法接收者仍可使用。 |
| CPU/decoded 共同总账 | 直接使用任务 B 的 **256 MiB decoded live + reserved** 额度，闲置 decoded ≤64 MiB、TTL 10 秒，包含在 256 MiB 内；当前 128 MiB payload 表由这个账本接管，不另叠加。compact plans/证书与相机 CPU 几何共用一个起步 64 MiB 辅助额度，几何的现有 64 MiB 为子上限。两部分合计 320 MiB 可管理逻辑容量，同一 backing 去重；压缩输入/Zstd workspace、Unity 副本等另按任务 B 计量，不称整个进程硬上限。 |
| donor lease | 每个逻辑 donor/request epoch 一位，去重，至多 5 个；只在需要 loader 保护时持有。无 borrower 后 idle TTL 起步 2 秒，且字节/数量超限即先归还；持续任务的 deadline 另计，不能靠轮询无限延长。 |
| 并发 miss | 首版一个执行 Unity 重步骤的构建 owner，同 key 请求合并；后台解压 worker/额度沿用任务 B。新旧重叠预留后才能准入，全部创建/上传/取消清理共用任务 B 的每帧账本，本文不另起第二条队列。 |

`B_asset` 是模组管理的**可核对估算与准入目标**，不是进程/GPU 物理内存硬上限。Texture 按实际格式/mip 块大小估算，Mesh 计 vertex/index/bindpose/必要 CPU backing，材质/Original/loader 隐含资源开销标记已知/未知；请求 bytes、shader/bundle 依赖、Unity storage、driver staging 与真实 residency 分别记录。无法估算 donor 的隐藏 bundle 成本时不得宣称已纳入精确显存上限，也不准借未知成本长期闲置持有整个 prefab。

准入使用唯一资产并集：`charge(active ∪ pending ∪ idle ∪ retired ∪ donor保护) + reservation(miss)`。先清过期 idle，再驱逐无 borrower/无恢复义务的闲置项，必要时取消低优先预热。额外缓存和预热服从额度，游戏当前需要的冷构建则通过全局队列限制在途量；不能因闲置缓存额度较小而让第二至第四角色永远不构建。已经使用的 active 或待恢复 Original 不被错误销毁，共享 Texture 只有全部 borrowers 结束才可淘汰。

若游戏新出现的消费者、不可销毁的旧池克隆或成本修正让估算超过目标，进入 pressure 状态：清 optional roots、暂停预热/新代准入、记录超额；保住现有绑定与恢复状态。**不能为了宣称硬上限而强卸载活跃角色，也不能退回无限同步构建。** 新 root 冷 miss 可以保持原版自然交付；已有 A 的切换若无法预留，则保留 A/等待，具体后补依赖已验证的实例重绑入口。

历史样本单角色仅 Texture 约 369.89 MiB，四套约 1.45 GiB，五套约 1.81 GiB。固定 512 MiB 总容量不足以覆盖这些必要资产，因此本次审阅将活跃需求总账与可选闲置额度分开。分帧只能降低临时叠加，不能消除必要常驻；实际设备容量不足时要单独评审质量或显示策略，不能以缓存 miss 或永久暂停其它队伍成员掩盖。五角色是候选/消费者能力，也不保证任意大包无限闲置命中。

任务 B 的最小 Ready 阶段可以保持“不额外强持有多代闲置成品”；本任务 C 加入短 idle lease 时仍使用同一资产 owner/总账，不叠加一份 Ready 池。任务 B 的预热集合最多四队伍角色，selectedUI 的真实请求优先挤占预热额度；本池允许五角色消费者共用成品，不要求同时推进五个上传 owner。

## 6. donor 短 lease：只解决确证的游戏重载

先区分两种 miss：成品被淘汰需要 BEM 构建，还是原 UI donor 被游戏卸载需要 Load。成品仍有存活资产、原 donor 兼容证书可验证且接收者自带可用 Original 时，不额外 Load donor 来“刷新缓存”。Android world 确需 UI donor 的路径仍保持现有适配校验。

若后续身份日志证实同角色短时间内 donor 真正重复加载，可在 `DonorLeaseOwner` 中跨相邻事务借用**同一个 tracked handle**，按逻辑资源、platform/build、resource epoch/type/category 定位。borrow 时验证 handle/error/native-live 与 pristine provenance；结束只减少 borrower，最后一位退出后短 TTL Dispose。此策略是防止 loader 引用过早归还的候选，仍需验证游戏实际 retain/unload 语义，不保证阻止游戏重建实例或强制卸载。

成品池不持有 UI prefab；donor owner 在必要 borrower/短 lease 内可能间接保留 prefab/bundle，必须独立计账、限数量与 TTL，到期释放 loader 引用和 boxed root。GCHandle 强 root、`DontUnloadUnusedAsset`、tracked game handle 不是同一种保护，不用长期强 root 或标记把原 prefab 永久锁住。

Load 返回已替换的 donor 也不意味着它是 pristine。先查 Original provenance，不能用 A 的几何/材质当 B 的原版输入；重新 Load 同路径不能保证取回 pristine。无法获得真正 Original 时保留当前结果、拒绝这次提交，等待可验证的原版交付，不套包叠包。Load/Dispose/对象核对均在已验证的 Unity 许可线程上做，PC dump 声明不作为 Android ABI/线程许可。

## 7. 无循环保活的退休与析构

所有权方向固定为 `task/instance → lease → asset owner`；计划/asset owner/retirement 不反向强持有 task、instance 或 Original。资产级 Original 与实例恢复状态明确拆开。`completed` 继续提供“已应用状态/旧克隆来源”，不能兼职无限命中池，也不能依据池自己持有的对象推断外部使用。

至少区分四类证据：登记的外部消费者、模组 cache roots、恢复义务、未知游戏池消费者。`generated Material/Texture 还 native-live` 只说明对象存在；可能完全由本池 lease 保住。Original 原材质或仅隐藏 shadowProxy 所保留的 mesh/material 不计为 generated 消费者，也不得给 idle TTL 续期。

| 退出情况 | 处理顺序 |
| --- | --- |
| 完全未发布的构建失败/取消 | 先释放私有材质对借用 Texture 的依赖；只清理本 owner 创建且确定未发布的 Material/Mesh/Texture；借用池成品仅减 lease；最后释放 decoded backing、reservation、donor borrow。 |
| 绑定成功，旧代进入退休 | 先读回全部目标 mesh/material/bones/enabled/shadow，登记新消费者；再解除旧消费者 lease。old Original 仅在仍有真实恢复义务时保留，不以原材质存活自证。 |
| UI 退出/回池 | 回池不自动等于对象已销毁；根据经验证的 pool 生命周期移交或取消消费者。无消费者时移除实例骨骼/私有材质/Original 额外 roots，成品按预算进入短 idle；不强 hold UI root。 |
| 关包恢复成功 | 先原样恢复并读回；移除这个接收者 generated 归属/lease。恢复后指向 Original 的材质不是旧生成代存活证据；单独处理 Android shadow/proxy 复原，不让 shadow→Original→completed 形成保活。 |
| 存在未知模板/池克隆 | retirement 保存有限描述与弱身份，不扩大扫描或 Destroy。若仍有必须保住的恢复义务，计入不可驱逐额度并暂停新代准入；不能隐藏在预算外，也不能丢 Original 换出“零常驻”数字。 |

成功发布资产的 cache 驱逐默认只取消模组额外 roots/lease，让正常 Unity/游戏引用继续使用。消费者登记尚不完整时不显式 Destroy。只有后续证明所有消费者已脱离且确属本模块创建，才讨论主线程 Destroy；绝不销毁游戏 Original、prefab、shader 或借用 donor，也不触发全场景 UnloadUnusedAssets。

对未登记旧克隆，仅靠 weak lineage 无法同时保证任意未来恢复、固定内存上限与永远零重建。首版必须记录此边界：保留可验证的弱 provenance/恢复义务；身份不全时停止覆盖、保留当前显示。注册覆盖确认前不宣布“全场景关包立即恢复”。不能通过无限强 Original 留存掩盖未知消费者问题。

CPU owner 可以在工作线程释放纯数据；含 Unity 资产/handle Dispose 的 owner 析构只能向已验证主线程清理队列移交 move-only ownership。不得在任意析构线程 Invoke/Destroy；模块停止时先取消任务、停止准入、完成可恢复绑定并排空清理队列，再卸载 hook/host。进程退出无法证明 GPU 已回收，单独标记为 teardown，不能当正常 eviction 验证。

## 8. 获取—绑定事务与错误路径

以下是拟议结构，不是现有接口或本轮代码：

```text
OnKnownDelivery / OnVerifiedUIAssembledOrPoolActivated(target):
  capture(target epoch, actual bindings, immutable desired revision)
  resolve pristine donor + compact validated plan + compatibility
  TryAcquireMeshes / TryAcquireTextures             // 可部分命中
  reserve only misses + 必要 Original/新旧重叠
  DecodeNeededBlocks → BuildMissing → Validate      // 完全命中跳过
  prepare target-local bones + privateMaterials + rollback snapshot
  recheck desired revision, target epoch, actual bindings, native-live
  CommitResource(world/UI 必要时联合) → readback
  success: publish complete entries/leases + applied revision
  failure: reverse restore → 清未发布 owned / 保留仍可能被引用资产
```

cache hit 必须借出短期强 lease 跨越 prepare/commit，避免查到 weak 对象后在事务中失活。池的 borrowed Mesh/Texture 不进入 `ConstructionScope.assets` 的“本次创建”集合；新造资产显式移交 owner。`published` 不能继续作为整池所有权转移的唯一布尔值：要逐资产记录 owned/borrowed、已被哪些目标引用，以及回滚是否完整。

相同 key 的重入/并发请求共用一个 pending owner 和 reservation，完成后各自绑定本实例。包/adapter snapshot 和文件 lease 随任务存活；同角色选项更新只提交最新 desired revision。游戏原交付函数仍恰好一次，不重放 `_FinishWithAsset` 来伪造展示实例登记。

| 场景 | 必须保住的结果 |
| --- | --- |
| 解压、Mesh/Texture、材质准备、预算准入失败 | 尚未写 target；已有 A 和 Original 不动。释放本次未发布对象，借用成品保持其他消费者可用。 |
| 提交中 setter 失败但逆序恢复成功 | 恢复**进入事务前的当前绑定 A**，不是直接换回 pristine；不更新 applied revision、不把半成品发布成 cache Ready。 |
| restore 本身失败 | 标记 target/资产 quarantine，保留可能仍被 renderer 引用的新旧资产、恢复快照及 lease，禁止自动销毁/淘汰；记录超额并阻止继续新代。不能输出“失败仍完整保留 A”。 |
| revision 过期、目标销毁或游戏已改写未知绑定 | 提交前取消；按当前所有权清理，不绑到同地址的新实例。不凭名字强行覆盖游戏新状态。 |
| 关包/角色取消 | 先取消未提交任务与新 lease 准入，再对已登记存活 target 做 Original 恢复事务；读回成功后释放生成消费者。失败时保存当前可显示结果和恢复义务，允许受控重试。 |
| Original/成品已被游戏原生卸载 | 将该条失活证书作 miss，重新取得可验证 donor；不能用 GCHandle 非空绕过 native-live。无法恢复时不丢当前 binding/记录、不销毁其资产，也不伪报恢复成功。 |

“失败和关包无丢失”是 ownership/事务要求：不得因清池、过期或失败遗失仍在用的资产、当前绑定或必要恢复资料。游戏强制卸载、恢复 setter 失败、未登记实例仍是可见限制，不能据静态源码承诺任何故障都能立即恢复原版。恢复资料预算在首次替换前预留，不等关包时才试图补回。

## 9. 与 current module.cpp 的后续实施接点

| 建议顺序 | 可评审改动范围 | 完成条件 |
| --- | --- | --- |
| 1：成品池与身份 | 在 `ProcessResource` payload 前增加计划/兼容查找；`PrepareResource` 拆 donor capture 与 asset acquire/build；Mesh/Texture 独立 entry/lease | 新 pristine root + 同 key + 同 donor 可命中；无需改原生游戏加载流程。最初只跨已确认 donor，完整事务不变。 |
| 2：稳定计划/选择性解码 | `bem.cpp` 保留 manifest mesh/texture/payload id 和依赖投影；按 miss 获取 blocks；Morph 用精确 ticks/decoder revision | 完全命中无 LoadBem 解压；Morph-only miss 只重建相关 Mesh，不重传已命中贴图。现有整份解码不冒充此结果。 |
| 3：实例接收与私有状态 | 接入已验证组装/详情/池激活入口，绑定实际接收者 bones/privateMaterials；纠正同 root 快路径只按状态表跳过 | 模板和当前实例分别登记。未验证入口不列覆盖，不用全局固定扫描。与热切换任务共用登记/dirty 队列。 |
| 4：预算、Original/影子退休 | 替换自保活 prune 条件，新增唯一资产总账、短 idle LRU、恢复 reservation、主线程 owner 清理 | activeTeam4+selectedUI1 共用配额；关包/keep/shadow 不以 Original 自持算旧生成消费者；失败 borrowed 资产不被销毁。 |
| 5：必要 donor lease | 仅针对日志确认的重复原资源加载加 tracked handle 短 lease，合并相同请求 | 每个 owner 恰好一次 Dispose；不长期持有整 prefab；loader native 卸载行为须实机验证。 |

分帧/延迟交付沿用原生加载研究与任务 B 的队列、许可线程核实，不在此任务另造 hook。缓存不能消除首次冷构建峰值；命中不需上传也不证明游戏原资源加载完全没有 GPU 工作。所有 Unity 查询、创建、绑定、读回、Destroy/Dispose 在已确认线程执行，当前交付锁/thread-local 与 pump tid 记录不等于主线程许可证明。

## 10. 后续验证提案与可接受表述

以下均为未来获授权后的验收项目，本轮未执行，也未重跑现有 fixtures。先以身份与 API 构造量验收，再讨论时间/内存效果。

| 用例 | 需记录的证据 / 通过条件 |
| --- | --- |
| 同角色详情连续打开，同 root 与新 pristine root 各一次 | 区分游戏 Load 请求、donor native identity、instantiate/池激活、payload decode、Mesh/Texture 构造/API bytes。兼容存活命中时后二者为零，实例 bones 与私有材质正确。 |
| UI/world 两种交付顺序、同时两个 UI 实例 | 保留已有联合事务结果；共享成品但各自 bones/materials 独立，动画/表情/发光与阴影不串。 |
| Morph-only、只换贴图、改 submesh/选项、A→B→A | 分层 miss 原因可解释。Morph-only 不变 TextureKey 命中；贴图变化不必要重建兼容 Mesh；A 已被淘汰时允许重建。 |
| resource build/platform、donor、bindpose/palette/shader/sampler 改变；unchecked→checked | 每个不兼容路径明确拒绝命中或重新验证，不仅比较角色名/CRC；无错贴图、变形或跨实例骨骼。 |
| 队伍4 + 队伍外详情1，再切详情/退出角色 | 唯一资产去重、总预算/idle TTL/LRU/lease 数量日志；第六角色不继续常驻，超额不误毁活跃/恢复资产。 |
| keep/no override、隐藏组件、关包和 Android shadowProxy | 区分 generated 与 Original；恢复后 old record 不靠自己 original materials 或 shadow native-live 无限续期。 |
| 构建/提交/恢复失败、快速 revision、取消、native 卸载与模块退出 | owned/borrowed 清理正确，旧结果与必要 Original 保存；RestoreFailed quarantine 不自动销毁；Dispose 无重复。 |
| 超 TTL/预算驱逐后再次打开、长期轮换 | 允许 miss，确认强 roots/Original/retired/prefab lease 有界；区分取消额外持有、Unity 原生回收与真实 GPU 回收。 |

新增诊断只进日志/研究记录，不往 UI 添加说明。至少保留 resource/target epoch、desired/applied revision、plan/mesh/texture hit/miss reason、pristine donor 和 generated id、验证证书、borrower/owner 类别、owned vs borrowed、估算 active/pending/idle/retired/Original/donor 字节、过期/驱逐/恢复结果，以及 decode blocks/bytes 和 Mesh/Texture 构造与提交计数。不能只沿用“有新对象才输出”的事务日志：零构造命中也需要一条简洁的 cache/attach 证据。

**可作为未来效果假设的表述：**重复 UI 在兼容存活命中时应减少模组解压、Mesh/Texture 构造和重复 API 上传；分层 key 应让 Morph-only 更新继续共享贴图。闲置短 lease 可能提高往返命中，也会增加常驻；必要 donor lease 可能减少真正的游戏重载。实际卡顿、峰值、常驻和命中率须对同包同场景测量，本文没有保证性能比例、绝对延迟或 GPU 已释放。

**本轮交付边界：**一份静态设计与可追溯限制；生产实现、新 hook/配置、构建安装、游戏实测与性能承诺均未发生。
