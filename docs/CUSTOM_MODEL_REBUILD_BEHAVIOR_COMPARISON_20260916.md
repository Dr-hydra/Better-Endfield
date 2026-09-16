# CustomModel 重建前后行为对照

后续状态：世界/详情替换、B5 的 AI 角色 LOD 和懒加载均已获用户确认正常，用户授权本阶段开发收尾并推送开发分支。正式版保留 B5 行为，临时 A/B 与检测代码撤除；B1～B4 未执行实机对照，配队偶发缺件另记 CM-001。以下早期失败记录保留为历史，不代表当前结果。最新证据及未执行清单见 [A/B 记录](CUSTOM_MODEL_RESOURCE_AB_VALIDATION_20260916.md)。

审查日期：2026-09-16。用户要求先对照原实现，不急于增加诊断。本轮只进行静态对照及复核已有日志，没有新增实机采样、修改运行配置或覆盖测试 DLL。刚开始添加、尚未构建部署的 `validation_capture` 配置与四个 `capture.*` 契约已撤回。

基线是 `84b88bfb` 对应的 `module_legacy.cpp` 及原有 `.inc`，运行路径按 `target=both / path=resource` 对照；不能用旧版 instance 模式的扫描行为来解释 resource 模式。本次新实现是当前工作树 `native/modules/custom_model/module.cpp`。

## 1. 优先处理的行为差异

### 1.1 包内贴图清单被提升为“每项都必须绑定”

- 旧版 `module_poc2_part_02.inc:2243` 的 `InstallCustomTextures` 按实际材质槽找候选。未命中、歧义、创建失败时继续其他槽，没有要求每条 BEM 贴图至少使用一次；函数不向 `ApplyReplacement` 返回整套外观成功状态。
- 新版 `module.cpp:1546` 的 `PrepareTextures` 收集所有已复制材质，匹配后遍历 `used`；任何未使用的 BEM 贴图都使整份资源失败。创建、采样参数复制和赋值读回失败也全部拒绝。
- 已确认设计要求“必需环节失败时保持原样”。但既有 BEM 的源贴图清单与“已经验证的原生材质必需绑定集合”并没有先完成语义对齐。直接把每条库存都视为必需，是迁移中新增的兼容性条件。
- 本轮日志在管理员世界资源和详情资源均停于 `Required payload texture has no binding: 290f53c8`，随后明确保留原资源。这一差异能解释管理员未替换。
- 之前成功会话中，管理员包的 24 条贴图只有 20 条不同的源贴图实际安装；`290f53c8`、`9750205d`、`7d330dbd`、`908140d5` 不在该次已安装集合中。这证明旧成功条件与新条件不同，不证明这四条可以无条件删除。
- 佩丽卡本轮已越过该检查并提交，因此不能用管理员的贴图错误解释佩丽卡。

### 1.2 同一资源根对象被直接认为仍然完成

- 旧版 `module_multi_path.inc:64` 对已激活角色调用 `RefreshActiveReplacement`。`module_poc2_part_03.inc:118` 会检查 renderer 当前 Mesh、材质和隐藏状态，在再次收到该资源时恢复被改回的绑定。
- 新版 `module.cpp:1837` 的首个循环只要 `record.root.Get()==asset` 就返回 true；`ProcessResource` 随后跳过处理，不核对该根对象的当前绑定。
- 新版对自然克隆做了完整 Mesh／材质／enabled 签名比较，但同一根对象绕过了这些比较。若游戏改回同一个对象的绑定并再次交付，行为将与旧版不同。
- 这是明确的行为缺口，但现有日志没有证明佩丽卡本次发生了“绑定被改回后再次交付”。后续修正应仍局限于资源交付入口，无需引入场景扫描。

### 1.3 主线程策略按设计暂缓，并非迁移遗漏

- 旧版 `module_multi_path.inc:3` 调用 `Object.CurrentThreadIsMainThread`，`MultiApplyModel` 在读取资源身份和构建之前拒绝非主线程交付。
- 新版 `module.cpp:1978` 只有启用、停用和线程局部重入判断，再加互斥锁；没有相同的主线程检查。互斥锁并不保证执行线程正确。
- 复核设计文档第 90 行及 K1 后修正：用户明确指定正式版暂不实现“非主线程时跳过”，因此这属于已确认的策略差异，不能称为迁移遗漏。现有日志仅给出提交线程 ID，不能证明该线程就是主线程，也不能认定它是本轮未显示的原因。
- 用户随后要求逐项 A/B，允许以独立实验版验证旧跳过策略；实验结果不自动改变正式设计。

## 2. 已确认设计带来的变化及其证据边界

| 项目 | 旧资源实现 | 当前重建实现 | 审查结果 |
| --- | --- | --- | --- |
| 写入顺序 | 每个 Mesh 构建后立即绑定；复制材质并绑定后再安装贴图 | 所有 Mesh、私有材质和贴图先离线准备，再逐一提交；失败恢复本次写入 | 符合原子提交方向；成功日志只表示当时的提交及读回成功 |
| 资产持有 | `RetainAsset` 对相关 Mesh、Material、原贴图增加强句柄与卸载保护；renderer、资产、替换记录长期保存 | 构建期临时强句柄；成功后释放，仅保留弱引用和对象身份 | 符合取消长期强持有的方向，真实原生资产生命周期仍未验收 |
| 临时保护结束点 | 替换记录一直存在 | `ProcessResource` 返回时 `ConstructionScope` 析构，随后才调用原 `_FinishWithAsset` | 游戏继续交付时只依靠已经写入的正常对象引用；与旧版差异很大，但不能仅凭此断言资产已回收 |
| Mesh 名称 | `BetterEndfield.C<id>.BEM` | 与当前接收者原 Mesh 同名 | 符合已确认设计；新旧日志不能再以旧名称前缀判断是否替换 |
| 蒙皮字段 | 构造后从当前原 Mesh 的固定 `+0x1C8` 复制；上传后异常只记日志 | 相同时点、相同数据来源，位移由序列化代码解析；源值及上传后一致性失败时拒绝 | 当前客户端解析结果仍为 456（`0x1C8`）；不是改用另一套蒙皮生成算法 |
| Renderer 命中 | 管理员主要是 renderer 前缀和原始索引数；佩丽卡还核对精确名称；允许多个接收者、缺失时跳过 | 两角色均要求精确 renderer 名、原 Mesh 名、索引数，每组件唯一；缺失或重复拒绝整个资源 | 新版更严格；本轮两角色几何均已构建，当前已知失败发生在管理员贴图阶段 |
| 材质复制 | `target=both` 的旧资源模式已复制私有材质 | 仍复制私有材质，但先修改脱离 renderer 的副本，最后提交 | 不能将本轮问题笼统归因于“从共享材质首次改成私有材质” |
| Texture 共享 | 同一 payload 条目共享一张新贴图，后续槽可能覆盖其采样设置 | 按 payload 条目和原 Texture 对象共同缓存，逐项校验采样设置 | 可能产生更多新 Texture，但保留不同原纹理的采样设置 |
| enabled 写入 | 常规组件主要只绑定 Mesh，隐藏组件单独处理 | 每个提交组件都调用 enabled setter，即使值与原值相同 | 额外调用差异；没有证据表明这是本轮失效原因 |
| LOD | 替换激活后一次性开启；固定字段偏移；恢复有遗漏 | 有启用 Mod 或独立开关即开启；字段按元数据名称解析，分别保存并恢复；同管线 applied 状态直接返回 | 开关策略符合设计，但新旧均缺少持续防覆盖；`lodActive=1` 只说明本模块曾应用，不能证明实际仍为 LOD0 |
| BEM 内存 | 成功后的角色 payload 保留在 `g_active_bem` | 按需加载，128 MiB 上限、10 秒闲置到期 | 回收的是 CPU 包数据。上传后的 Mesh/Texture 生命周期不能用该缓存字节数代替判断 |
| 初始化失败条件 | 部分材质、LOD 契约可选，可能降级继续 | 启用模型时大部分契约必须成功，否则模块拒绝启动 | 更严格；本轮模块已成功初始化，不是已知阻断点 |

源码定位：旧资产保护 `module_poc2_part_02.inc:249`；旧材质复制 `module_poc2_part_02.inc:2132`；新临时保护析构 `module.cpp:1508`；新准备与提交 `module.cpp:1530`、`:1608`、`:1635`；新 LOD `module.cpp:1742`。

## 3. 核对后保持一致的部分

- 资源入口仍然是 `BundleLoader.AssetProxy._FinishWithAsset`，都先替换、再调用原函数。旧位置 `module_asset_delivery_trace.inc:73`，新位置 `module.cpp:1978`。
- 管理员／佩丽卡 `postmodel`、`uimodel` 及 `(Clone)` 的精确资源名称一致。注册表化没有改成只匹配开屏资源。
- 两版都从收到资源的子树枚举 SkinnedMeshRenderer，包含 inactive。旧 resource 模式本就跳过场景实例扫描及 AI shadow 场景补写；instance 模式的能力不能当作这里的旧基线。
- `ReadVertexDeclaration`、`ReadMeshStrides`、`MatchesDeclaration`、`DecodeComponentSkin`、packed 顶点与索引 buffer 写入 helper、submesh helper、`SafeUploadMeshData` 对照相同。
- `BuildMeshFromComponent` 仍用当前原 Mesh 的声明、stride 和 bindposes，仍按原顺序上传 packed 数据、设置 submesh、bounds，执行 `UploadMeshData(false)`。变化集中在对象保护、名称、动态位移和失败条件。
- 旧文件虽然有 `InitializeMeshSkin`，其在该旧实现中没有调用点；不能把新版没有该函数当作遗漏了一个原本执行的初始化步骤。
- 贴图名字 pin、slot pin、component mask、尺寸消歧、共享纹理过滤的核心匹配规则沿用；`UnionTextureComponentMasks` 与 `IsSharedAssetName` 的实现对照一致。

## 4. 本轮实机反馈允许作出的结论

用户反馈：佩丽卡世界场景与角色详情页都没有替换。最新会话从 20:05:39 开始：

- 20:05:55：两个 Mod 已启用。
- 20:05:56：LOD 已应用。
- 20:06:19：管理员世界资源因未绑定贴图 `290f53c8` 保持原样。
- 20:06:43：佩丽卡世界资源记录 10 组件提交成功。
- 20:07:06：管理员详情资源因相同贴图保持原样。

没有佩丽卡详情资源成功提交的日志。世界资源的提交读回不能代替最终显示验证；当前信息不能证明“是开屏模型导致”，也不能证明是 GC 回收。本轮没有运行 `betterendfield.model` 模块的启动记录。

先处理迁移一致性：厘清已有 BEM 贴图清单与必需绑定的语义，核对同一资源根对象的当前绑定。主线程策略、生命周期和原 Mesh 名称都已有设计决定，应保留证据边界。此次静态对照阶段没有改包或部署；后续用户已授权逐项 A/B 并直接覆盖测试目录。

本轮没有运行新一轮编译或测试，因为未引入功能修复；既有测试通过仍不能验证真实渲染。当前同根对象测试只验证“未改动时判重”，没有覆盖“被游戏改回后再次交付”。

## 5. 按用户补充怀疑继续核对资源交付链

用户认为替换根本没有加载，要求优先关注资源交付链。沿旧 Hook 的实际调用点复核结果：

1. 旧 `DeliveryFinishHook` 调用 `MultiApplyModel(asset, ..., true)`，再向原 `_FinishWithAsset` 传回相同 asset；新版 `ResourceFinish` 同样先处理 asset，再向原函数传回相同 asset。目标方法的程序集、命名空间、嵌套类、参数及返回类型一致。
2. 旧 `Internal_CloneSingle` 与 `Instantiate` Hook 只观察克隆；`CreateSMSInfoForPostModel` Hook 只观察装配；`PostDealLoadedModel` Hook 的替换调用经过 `MultiAssemblyComplete`，而该函数在 `g_resource_path` 为 true 时直接返回。没有找到另一个被删去的 resource 模式写入入口。
3. 旧 resource 模式确有后半段成功证据，不能仅根据模板提交推测。2026-09-16 01:11:36，`chr_0004_pelica_postmodel` 的 `RESOURCE-BEFORE-FINISH` 记录 10 个组件；紧接着 `INSTANTIATE-POS-PARENT` 的原模板 ID 为 232836、克隆 ID 为 -109810。01:11:37 的 `PART-BASE-END` 仍为同一克隆 -109810，名称变为 `chr_0004_pelica_postmodel(Clone)#47`，其 10 个槽仍持有 `BetterEndfield.C*.BEM` Mesh。例如 C0 的 Mesh ID 为 -107526。相邻两次旧会话也记录了同类继承结果。
4. 新版当前证据止于 `Resource committed` 的模板绑定读回，没有对应实例身份、克隆来源及装配后绑定记录。用户确认的“最终画面没有替换”没有被这条日志否定。尚无法从静态对照判断该模板未被使用、引用/绑定在后续发生变化，还是另一条显示资源路径未经过该入口。
5. manifest 新增 `Common.Beyond.dll`、`UnityEngine.UIModule.dll` 为显式依赖。旧版其实也解析这些程序集的方法。新版在 20:05:55 完成 Hook 安装并启用，早于已记录的管理员交付（20:06:19）和佩丽卡交付（20:06:43）；所以不是模块整段未启用。没有对更早或缓存命中的资源作出覆盖结论。

后续应沿“收到的资源 → 被修改的模板 → 游戏克隆 → 装配后的实际 renderer”比较对象身份与绑定。源码层面保留同根对象无条件判重、主线程策略、临时引用在原交付调用前释放这三个具体差异作为复核点；不能提前把其中任何一个宣布为佩丽卡根因。主线程项的旧“遗漏”措辞已根据设计原文纠正。
