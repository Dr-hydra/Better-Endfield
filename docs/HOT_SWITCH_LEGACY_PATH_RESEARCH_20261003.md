# 热切换与 legacy 实例路径调研（2026-10-03）

结论：**建议沿用当前资源事务，补上“配置变化驱动的目标实例重绑、按原始 donor 身份复用资产、分代生命周期管理”。** 旧 PoC 的价值是能直接修改已经显示的实例，绕过等待下一次资源交付；不能把其全局轮询、长期强持有和扫描后销毁整套搬回。

当前已具备 selection key、pristine Original donor、失败回滚和旧资产 weak lineage，能够在**再次交付**同一缓存 root 或旧包的自然克隆时换包。但配置入队、registry 接受、模板更新、当前实例更新、旧资产释放是五个不同阶段；现有代码没有保证它们即时连续完成。

本次只读研究，唯一新增文件为本文。依据 `main dd7131b44cd6f99bdcd67386cd8a778249c1e396` 加工作区已有未提交改动，核对共享 native、Android 适配器、两处历史实现及既存日志。未修改生产代码或配置，未启动游戏、操作手机、读取驱动全量 API、发送外部消息或构建安装。世界命名普遍性由其他调研负责，本文不展开。已检查适用 AGENTS.md；涉及 Android 文件遵循 `android/AGENTS.md`。

## 1. 当前调用链与实际边界

以下 `module.cpp` 均指 `native/modules/custom_model/module.cpp`，行号是本次工作区位置。

```text
PC 选包/选项写 runtime.ini
  → 下一次任意 _FinishWithAsset
  → ResourceFinish:2881 → ReloadRegistryAtDelivery:2632
      最多每 500ms 比较配置文本；没有交付就没有这次检查

Android BemHotSwitchUpdater 后台检查 index、准备不可变 generation 文件
  → JNI updateCustomModelConfig → QueueConfiguration（仅入队）
  → UnityPlayer.nativeRender 后 frame → ApplyPendingConfiguration
  → UpdateSharedReplacement → ResourceConfigurationChanged:3094（共享层再次入队）
  → 下一次 _FinishWithAsset → ReloadRegistryAtDelivery 消费

InstallRegistryUpdate:2613
  → ParseModRegistry + metadata/选项诊断 + AcquirePayload 校验候选启用包
  → 接受完整 registry；开关 hot_switch/skip_validation/loading_optimization 仍锁定启动值
  → ProcessResource:2755 → Match → IsCompletedResource(adapter, root, selection_key)
  → PrepareResource:1906 / PrepareAndroidWorldResource
      读取当前绑定作为 rollback；UseSavedOriginal 提供 pristine 构建 donor
  → RememberResource → CommitResource（逐 binding 写入、读回；失败逆序恢复）
  → PublishCompleted → 原游戏 _FinishWithAsset 恰好一次

ResourcePump:2904
  → 原 pump → LOD/停止维护 → 每秒 PrunePayloadCache、PruneCompletedResources
  当前不消费 registry，也不主动重绑场景实例
```

Android 链的依据为 `BemHotSwitchUpdater.java:12`、`XposedEntry.java:100`、`native_bridge.cpp:250`、`custom_model_module.cpp:271`。JNI 返回 true 只证明入队，不证明 registry 被接受或模型已更新。

### 已解决的部分

- **选包身份与 adapter 寿命：** `mod_registry.cpp::ParseModRegistry` 的 key 包含规范化路径、外观/编码 options、parameters、校验/优化模式、文件时间与大小；Android 路径含 generation。`ModRegistry::owned_adapters` 与 `CompletedResource::adapter_owner` 共享 ownership，覆盖 registry 不再使旧 completed 的 adapter 悬空。
- **构建 donor 与回滚目标分离：** `PreparedBinding:1647` 的 `original_*` 是当前旧包绑定；`donor_*` 来自 `SavedOriginalBinding:2094`。`UseSavedOriginal:2333` 先按同 root 与 renderer 名找 Original；自然克隆还需旧 mesh/material 身份一致，并按相对骨骼路径建立该实例自己的 bones。不能把上一包当原模型再套一遍。
- **同 root 与旧克隆：** `PublishCompleted:2665` 清掉上一代同 root 的弱 root 身份，保留旧 mesh/material 弱归属和 Original snapshot。仍使用 A 资产的克隆可追溯 A 的 Original，在下一次交付时构建 B；它不是持续追踪所有场景消费者。
- **Android 联合事务：** 当前 `world_resource_adapter.inc:20` 在提供 paired 输出时，即使普通模式也交还新建 UI sources；world/UI 一起提交和回滚。已不同于旧 main 的“仅热切换模式联合发布”。匹配当前 key 的已提交 UI donor 可直接复用，旧 key donor 则从保存的 Original 重建。
- **关包恢复：** `RestoreDisabledResource:2707` 使用 Original 恢复 mesh/material/bones/enabled，Android 还恢复 shadow 与代理状态；也要等这个目标再次进入处理链。

### 仍有的具体限制

| 限制 | 代码事实与影响 |
| --- | --- |
| 生效依赖游戏交付 | 共享层只在 `ResourceFinish` 读取/消费配置。队伍或详情打开可能触发交付，也可能直接使用池中对象；同一场景一直显示时可一直维持 A。 |
| 改模板不等于改已生成实例 | 克隆的 renderer 已持有 A 的 mesh/material。把模板改成 B 不会让它自动改引用；`g_completed` 没有完整的实例/激活登记。 |
| completed 是结果身份，不是跨事务资产缓存 | 同 root/同 key 会跳过；共享完整 custom mesh/material 的克隆也可判重。**新的原版 root 即使共用同一 pristine Mesh，也没有“按该原 Mesh 找现成 B”入口**，可能再次造 Mesh/Material/Texture。CPU payload 命中不能避免这些 GPU 对象构建。 |
| 同 root 快速判重不检查游戏回写 | `IsCompletedResource:2427` 首先凭 root+key 返回 true；后面的完整 mesh/material/bones/enabled 检查只服务克隆匹配。若游戏在同 root 上重新装回原绑定或装入其他绑定，当前快速分支不能检测修复需求。新增重绑前应核对实际绑定。 |
| key 不是内容摘要 | PC 以时间/大小区分文件版本，且只在配置文本变化后重新解析；原地覆盖、保持时间/大小或配置不变不能保证检测。现有 PC 运行中导入限制与 Android 不可变 generation 应继续保持；未来用明确 revision，而不是扩大到全包读取。 |
| 接受 registry 不等于全场景事务成功 | payload 校验失败拒绝候选；registry 一旦接受，某个 renderer 提交失败会回滚该对象，其他对象可已成功。没有整场景一致提交；失败对象需要保留“仍为上一包”的状态与受控重试。 |
| 校验更新本身可阻塞交付 | `InstallRegistryUpdate` 在交付线程校验候选中所有启用包，`AcquirePayload` 可能解压；128MiB/10秒 CPU cache 不保证大包命中。主动切换宜把非 Unity 解析移至后台，再由主线程接受准备好的快照。 |
| Original 与历史记录没有独立预算 | 强 Original 跟随 completed，旧资产/游戏缓存只要仍存活就可延长它的寿命；当前没有按角色、原始资产或旧代字节预算，也没有已验证的单资源缓存驱逐。 |

## 2. 两处 legacy 路径分别能借鉴什么

### 2.3.1：登录界面的完整替代对象

`legacy/better-endfield-2.3.1/source/native/modules/model/module.cpp` 是登录模型替换，不是 BEM world/UI 通用运行时。

```text
ConfigurationChanged:3827 → revision++ / cleanup_requested
  → LoginSceneAnimCtrl.Tick hook:3543 → TryActivate:3395
  → CleanupScene:2567 / ReleaseAssets:1390
  → DiscoverExistingLoginActor → LoadConfiguredAssets:2639
      I18NAssetLoader.Load → Get → 强 GC roots 保存 prefab 和动画
  → InstantiateReplacement:3060（同 parent 下克隆完整模型）
  → 对齐 Transform/Layer → 隐藏原 renderer → 激活替代对象
  → 建立自己的 Animator/PlayableGraph
OnRelease:3604 → 恢复原 renderer → 销毁本模块替代对象/graph
```

可借鉴的是**配置 revision 驱动定向更新、明确的 owner 与场景退出清理**。完整替代对象保留自己的骨架，避免强行混用不兼容 donor；模块自己的对象可按明确 ownership 销毁。

局限是同时保留原 actor 和替代对象、维护两套层级/动画状态，而且释放 GC root 仍不等于游戏 loader 缓存已驱逐。它只覆盖登录表现，不能直接替代当前玩家动作、物理、技能、表情及 world/UI renderer 绑定。配置变化后先清理再加载，也不具备当前“B 构建失败仍保留 A”的完整事务语义。

归档 `references/source-research/experiments/2026-08-04-pelica-skeleton-bind/README.md` 已记录跨角色骨架映射造成严重变形；这是历史客户端记录。本次不能据此声称当前游戏已验证，也不能把“骨骼名字能映射”当 bindpose 兼容证明。

### legacy-runtime：直接重绑场景 renderer

`research/custom-model/README.md` 明确这些是历史研究；`legacy-runtime/CMakeLists.txt` 只提供排除默认构建的研究测试，当前生产没有启用它们。

```text
F12 请求 / armed session
  → Canvas pump → PumpCustomModel（每帧入口，按 frame 去重）
  → UpdateReplacement / PumpMultiCharacter
      活跃时按 kActiveRefreshIntervalMs=500 做枚举，不是每帧都全扫
  → Resources.FindObjectsOfTypeAll(SkinnedMeshRenderer)
  → 过滤角色名、LOD0、active scene、路径
  → ApplyReplacement / RefreshActiveReplacement
      保存原 mesh/material/bones，SetSharedMesh 等直接改当前实例
      新实例与游戏回写可补绑；不同 donor 按自己的 bindpose 造 Mesh

另有事件路径：BaseModelViewPart / ComplexModelViewPart.PostDealLoadedModel 原函数后
  → MultiAssemblyComplete → get_model → MultiApplyModel
  → 只枚举这个 model 的子树 → apply/refresh
```

证据：`module_poc2_part_03.inc:356,472,504`、`module_poc2_part_02.inc:53,2957`；事件入口在 `module_asset_delivery_trace.inc:164,171,250`、`module_multi_path.inc:20,81`。旧事件模式与 resource 模式有显式开关，不能把全部探针当作同时生效的生产能力。

**优点：** 有现成的“已显示实例直接绑定”、clone provenance、当前绑定身份检查与回写修复思路。`PostDealLoadedModel` 特别适合借鉴为定向登记入口；20260903 PC dump 中仍有同名方法（`Gameplay.Beyond.dll.cs:330866,331684`），但其当前运行覆盖率、池复用、Android 对应方法均待实机核实。

**代价：** `FindObjectsOfTypeAll` 先返回所有已加载 SMR，再过滤；包括 prefab、池和非目标资产。500ms 限频仍有全局对象/托管调用开销，且非活跃池对象不会及时更新。`ApplyReplacement` 明确每个接收者独立造 Mesh；场景增多可扩大构建与常驻。逐组件 apply/refresh 也不等价于当前完整资源准备后提交的事务。

**旧 ownership 更重：** `PinReplacement` 长期强持有 renderer、Original 和 custom 资产；`RetainAsset` 还加 `DontUnloadUnusedAsset`（`part_02.inc:249`）。退休记录可继续持有旧变体，直到 rollback。`RollbackReplacement` 用全局 SMR 快照追查继承克隆，先恢复再销毁自己的 Mesh；这套全局追查成本与销毁依据不应照搬。

`module_generation_diag.inc` / `module_twin_mesh_diag.inc` 把 build 与 attach 拆开，attach 可零新建地重绑预建 Mesh，说明方案在代码结构上可行；它们只做有限 Mesh 诊断，不代表整套 BEM 纹理复用已解决。`module_generation_recommit_diag.inc` 的原地重写会影响共享 Mesh 的所有消费者，并可能触发 buffer 重分配，不能提供跨拓扑/材质变化的可靠回滚，也没有 GPU 降峰证明。

## 3. 所有权与回收：必须单独处理旧缓存

| 对象 | 当前 owner / 寿命 | 新方案的边界 |
| --- | --- | --- |
| 游戏原始 root/Mesh/Material/Texture | 游戏 loader、模板、实例；hot switch 的 Original 另有强 GCHandle | 不销毁游戏原资产；强 handle 保住托管引用，不保证原生资产不会被游戏卸载。重建前核对 native 存活和 shader/材质/骨骼可用性。 |
| Original snapshot | `SavedOriginalBinding` 强 mesh、materials 数组、bones 数组，Android 可含 shadow mesh | 建立按原始资产代际共享的 donor 表；资产级数据与实例骨骼状态分开，避免每个克隆长期保存整套 donor 或退休实例的骨骼。 |
| 构建中自定义资产 | `ConstructionScope` 临时强 roots；未发布则只销毁本次创建资产 | 异步分帧任务需要显式任务 owner，不能沿用栈对象保存裸指针跨帧。取消/过期任务也须受控清理。 |
| 已发布自定义 Mesh/Material/Texture | Renderer/Material 等自然引用；completed 弱跟踪 mesh/material，没有独立 Texture retirement 表 | 增加资产归属和代次账本；不能仅因更新模板、取消 handle 或清 completed 就宣布释放。 |
| completed 旧归属 | 弱 custom 身份 + 强 Original + shared adapter | 旧克隆仍使用 A 时保留 A→Original 追溯；与“供再次使用的 B 资产缓存”分开，防止用所有历史代常驻实现复用。 |
| CPU payload / CPU 几何 | 128MiB/10秒 payload；64MiB 几何预算 | 与 GPU 资产池独立计量；释放这些容器不代表贴图/驱动显存回收。 |
| Android donor loader handle | `AndroidLoadUiDonor` 获取，RAII `AndroidReleaseUiDonor` Dispose/释放 handle | Dispose 是归还加载引用，不能证明 UI cache 驱逐，更不能保证下次返回 pristine 资源。 |

一个应优先验证的静态问题：`PruneCompletedResources:2477` 在 root 失活后，只要任一 material 弱引用仍活就保留记录。**关包恢复记录**的 materials 就是 Original materials，而该记录自己的 `original->materials` 强 handle 又持有同一批材质；Android 仅隐藏的 shadow proxy 也保留原 mesh/material。只要这些 native 材质未失活，记录可能被自身 donor 引用维持，无法靠当前 prune 自动退休。普通 keep/no override 经 `CopyMaterials` 仍克隆 Material，不应把它误报为相同的原材质自保持路径。这不是已测 GPU 泄漏，但说明“weak lineage 总会自然结束”不能直接成立。

应把“生成资产仍有外部消费者”“donor 自身仍被模块持有”“恢复原版的状态记录”分别判断：保存 generated material/texture 身份，原材质不作为旧生成代存活证据；关包后的 donor 用真实目标消费者/资源 lease 和预算管理。预算耗尽时可放弃闲置 donor 的未来热切换能力，待自然新原版加载重新捕获，不能无条件持有，也不能错误回收仍在使用的 Original。

安全回收顺序是：准备 B → 校验 → 提交目标 bindings → 读回确认脱离 A → A 进入退休账本 → 所有**已覆盖的**模板/实例/任务租约结束后取消额外持有。只有证明消费者登记完整，才考虑主线程 Destroy 本模块创建且确定无消费者的对象；当前 weak lineage 不足以提供这个证明。未知缓存/池消费者存在时保留待查状态，不扩大到全场景 Destroy 或 UnloadUnusedAssets。

GCHandle free、弱引用失活、Destroy 调用、Unity 原生销毁、GPU/驱动回收是不同证据。固定等两帧也不是 GPU fence。本文没有实测释放 GPU。

## 4. 推荐组合方案

### 配置变化主动调度，事务保持复用

后台准备候选 registry/payload 与不可变文件 lease；Unity 主线程在交付事务外接受候选，产生**受影响角色的 dirty 队列**。相同角色多次修改取最新 revision；任务持有 registry/adapter 与包代次，过期 revision 在提交前丢弃。配置接受不再必须等 `_FinishWithAsset`。

复用当前 prepare/commit/restore、Original/rollback 分离和 Android world/UI 联合事务。主动入口对已登记 target root 做子树查询；不能假装调用游戏 `_FinishWithAsset`，也不能复用当前默认同 root 快速判重跳过实际核对。现有准备校验主要针对资源模板；已动画化的活实例须把 donor 布局/静止 bindpose 校验与接收者当下 Transform/骨骼状态分开，不能不加适配便直接调用模板准备函数。

模板在交付时登记，实例在经验证的组装完成/详情生命周期入口登记，池的重新激活另需覆盖。登记只留弱 root/renderer 和游戏资源 lease 身份；先识别该角色、资源类别、实例代际，再核对当前 mesh/material/enabled 等。发现游戏或其他逻辑装了未知绑定时暂停该目标，重新确认身份；不会依据名字强行覆盖。重绑后的 bones 必须来自接收者自己的骨架。

只登记模板不能保证已显示实例即时更新。第一版应明确“已登记目标可立即更新，未覆盖实例等待登记/自然交付”；不能用此范围承诺所有场景即时切换。日常维护只查登记表与 dirty 目标，不恢复固定频率全局 SMR 扫描。

### 原始资源身份与跨事务复用

区分三类身份：**游戏原资产/实例代际、包 selection revision、已创建 custom 资产代次**。原资产 key 至少含角色/逻辑资源类别、组件身份、native-live 原 Mesh 身份、原材质/shader/贴图槽身份、布局/bindpose/palette 兼容描述；对象身份须结合弱存活及 instance ID，不能永久用裸地址或只用名字/index count。

按 `(原 donor 身份 + selection key + 兼容描述)` 查已构建资产。弱命中且 native-live 可复用；需要持有闲置 A/B 以便往返切换时，另设按估算字节与 TTL 的有限强 lease，超预算淘汰闲置项。Mesh 仅在布局、bindpose 和 palette 语义一致时共享，实例 bones 数组重新映射；UI/world 复用继续经过现有适配校验，不能只凭同角色合并。

Texture 可进一步按已知不可变包条目、格式/尺寸/mip/色彩空间和完整 sampler 兼容性复用。不能去掉 source 条件后无差别共用；Material 的实例属性/keyword 会变时应保留私有材质，仅共享适用的 Mesh/Texture。A→B→A 命中存活 A 可零上传；A 已退休则重新构建，不能同时承诺零上传和零旧资产常驻。

### 主线程要求

Unity 对象查询、native-live/instance ID 验证、骨骼枚举、Mesh/Material/Texture 创建与 Apply、绑定读回、恢复、Destroy 及 loader handle Dispose 都应在已确认的游戏 Unity 主线程执行。文件/metadata/payload 解析与纯数据计划可后台完成；IL2CPP thread attach 或持锁不等于主线程许可。

当前 `ResourceFinish` 用锁与 thread-local 防重入，**没有 CurrentThreadIsMainThread 校验**；pump 记录线程 ID也未在 delivery 中强制对照。旧 `MultiMainThread` 的检查值得借鉴，不能从 dump 的方法存在或日志 tid 直接证明所有交付都在主线程。Android `nativeRender` 后 frame 是现有调度入口，仍需核对它与实际 Unity 对象许可线程的关系。

不得在同步交付中 sleep、阻塞等下一帧或交付半成品。分帧创建需独立任务 lease；旧实例 A 可继续显示直到 B 准备完毕，但首次交付没有 A 时，仍需确认游戏可延迟完成入口或允许先交付原模型、随后定向重绑的语义。

## 5. 与首次上传峰值的区别

旧缓存问题问的是：A 在哪些模板/实例/资产池仍有消费者、为何新 UI 再造同一批 B、何时退休 A。主动重绑减少“仍显示 A”的等待，资产池减少重复上传，有限 lease/退休规则约束旧代常驻；三者不能互相替代。

首次构建峰值则是一次上传窗口内的新资源、原 donor、Unity CPU 存储与驱动 staging 的重叠。第一次构建 B 即使没有 A，也存在；重绑会把这次构建主动触发得更早，不保证降低峰值。减少其在途量仍应独立研究上传预算、分帧任务及交付时序，见 `MODEL_UPLOAD_PEAK_REVIEW_20261003.md`。

只读已有 `artifacts/model-upload-20261003/device-current-peak-diagnostics.log`：3438 行 world、3561/3619 行后续 UI 各有一轮 8 次 Texture Apply、387,856,640B（约369.89MiB）请求量；首次 world 的联合发布已避免紧接着再造第二批，但后来 UI 再交付仍重复构建。日志没有这些轮次的 root/mesh 身份或 retirement 证据，不能断言它们是同 root 缓存失效、测得泄漏或旧资产未释放。样本实际为 8K ASTC；用户反馈单轮约0.9GB瞬时上涨，不是三轮请求量相加，也不是所有4K包的结论。

因此优先把同 key 新 root 的可兼容资产复用补齐；同时分别测“冷构建一次的 peak”和“多次切换后的 baseline/旧代寿命”。原地改写共享 Mesh/Texture 虽可减少对象数量，却损坏失败保留 A 的基础，也不能保证驱动不再分配 staging，不作为首选。

## 6. 分阶段实施范围与待实机项

以下是后续实施建议，本次未执行。

| 阶段 | 最小范围 | 验收与保留边界 |
| --- | --- | --- |
| 0：补齐身份与回收证据 | 单角色、world/详情两个已知入口；记录 desired/applied key、root/renderer/mesh/material 原始与生成身份、线程、Original owner、退休原因及构造量 | 先确认到底是未交付、同 root、原版新 root、旧 custom clone 还是游戏回写；优先核对关包/仅改可见性代理的 prune 自保持。无需全量游戏/驱动 API。 |
| 1：定向主动重绑 | 主线程接受配置；单角色 dirty 队列；只更新已登记模板和实例；保持现有准备/提交/回滚 | 同场景 A→B、关包、失败保留 A；覆盖双实例、骨骼、阴影、详情；未登记池实例明确等待，暂不主动驱逐游戏缓存。 |
| 2：有界资产复用与 Original 管理 | 原资产表与每实例 bones 分离；先复用同 donor/同 key 的存活 Mesh/Texture，再加少量闲置代 lease；生成资产 retirement 账本 | 新 UI root 命中兼容资产时构造/上传量为零；不兼容则独立构建；A→B→A 与长时切换不无限累计 Original/代次。消费者覆盖未证实前只取消额外持有，不自动 Destroy 成功资产。 |
| 3：控制单次构建在途量 | 独立主线程构建任务、上传字节预算、最新 revision 取消、完整发布；首次交付等待策略先证实 | 按单次 GPU/系统时间线验证 peak、完成延迟和取消残留；不能把阶段2减少重复次数当单次peak下降。 |

实机仍须验证：

1. PC/Android `_FinishWithAsset`、配置 frame、组装完成与池激活的真实线程、重入顺序和覆盖；同场景无新交付时能否更新当前实例。Sep PC dump 的方法签名不能替代现版本事件证据。
2. world-first/UI-first、详情重复打开、切队伍、池复用、多实例 A→B→A：记录对象身份而非只看页面变化；确认模板更新后哪些实例仍持有旧代。
3. 游戏自然卸载后 Original native Mesh/Material/Shader/Texture/Transform 是否仍可用；克隆骨骼、bindpose、表情、隐藏组件与 Android shadow/代理能否恢复。强 GCHandle 不是卸载豁免。
4. 关包、keep/no override、空材料、共享贴图、adapter component 顺序变化及游戏回写：区分状态记录与生成归属，验证 prune 不被自身 donor 保活。当前 fixture 模拟 weak handles，并在结尾显式清记录，不能证明自然 GC/原生回收。
5. 构建失败、setter/restore 失败、快速连续改包、角色退出时任务取消：保留旧绑定；无悬空 adapter、跨实例骨骼或过期配置提交。配置接受/逐对象成功分别报告。
6. 只跟踪本模块已知生成资产：比较零复用冷构建、复用命中、退休后的重新构建，核对构造量、CPU baseline、Graphics/GL/GPU 时间线。无消费者证明和实测前，不写“已释放 GPU”“peak 必然减半”。

现有 `native/tests/custom_model_binding_tests.cpp::HotSwitchTests` 可作为事务与 ownership 回归起点；历史 lifecycle fixture 仅供理解旧行为。本次未运行游戏或测试，完成的是源码/已有日志核对与方案整合。
