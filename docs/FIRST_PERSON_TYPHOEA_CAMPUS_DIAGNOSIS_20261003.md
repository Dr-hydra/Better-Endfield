# 提弗洛斯死库水包与逐角色骨骼规则（2026-10-03）

本轮只读排查和方案讨论。用户指定“提弗洛斯死库水 mod 第一人称隐藏不佳”；从已连接手机读取现有诊断与模型安装索引，没有改生产代码、配置、启用状态，未启动游戏、构建或安装。

## 当前到底做到了什么

现有实现完成了 BEM CPU geometry V1 数据桥及按实际非零权重逐面裁剪；`FpBoneKind` 允许实际 Head 子树和当前模型内的明确 tail 链，Neck 单列，其他骨骼保留。**尚未实现逐角色额外头饰骨骼名单。** 这与“按每个角色精确骨骼表识别帽骨等，再执行裁剪”不是完整的同一层能力。

骨骼表只负责哪些骨骼具有头部/头饰语义，裁剪执行还需要真实权重、索引及可用的 Mesh 构建/绑定能力。不能用一份骨骼名表恢复缺失的三角形数据，也不通过缩放骨骼隐藏，避免同时改变动画与投影骨架。

## 手机确认的执行缺口

忽略目录 `artifacts/first-person-typhoea-campus-20261003/` 保留此次只读采样。

- 当前记录 `tid=23137`：4074–4083 行缺少 `Mesh::get_vertexBufferCount`、五项 `GetVertexAttribute*`/stride、`GetSubMesh_Injected`、`SetSubMesh_Injected`、`SetIndexBufferParams` 和 `InternalSetIndexBufferData`；4086 行 `clone/index bindings unavailable`。
- 同会话提弗洛斯的 `face_01_lod1/2/3`、`hair_01_lod1/2/3` 反复记录 `clone bindings unavailable; retaining uncertain mixed geometry`。完整 palette 已确认头/尾的眉毛、虹膜、cloth_04/05 仍能走直接隐藏路径。
- `FpEngine::Init` 将复制/布局/索引接口汇总为 `ready`；`EnsureNeckCap` 在 `!engine_ready` 时先跳过，**尚未进入 `FpBuildPatch`，也就尚未调用 BEM CPU 查询**。因此不能把本次日志写成“CPU 数据成功读取后只剩几个 Neck 边界面”。
- CPU 桥和 GPU 回读已在设计上分开，但 CPU 可用不会自动补齐 clone/index 接口。日志中的“CPU geometry/palette fallback remains available”也不证明 CPU 裁剪真的已经执行；palette 独立部件路径仍可工作。

这不是“角色骨骼命名遗漏”能单独解决的问题。应先以现有已验证的 CustomModel Android MeshData/具名托管 API 为参考，补齐所需的复制、布局查询、索引写入适配，保持精确布局、bindposes、palette、完整投影及恢复检查。可优先研究具名托管调用的后备实现，而非猜测缺失 icall 的地址；若由 CustomModel 提供可见索引变体，仍需明确资源所有权与缓存预算。此处仅给方案，没有实现该适配。

采样时安装索引中校园包 generation 为 `7ade3357-b625-421d-abda-56811b925698`、selected_options 为 `appearance:v0`、当前 enabled=false；逆兔也关闭。这个当前设置不代表用户此前测试时的状态。新会话没有新的提弗洛斯 BEM 提交记录，旧日志有该角色提交；故尚不能证明本次每个保留 Renderer 当时一定使用了校园包几何。接口缺口本身有当前会话证据，但不能把旧默认包统计直接作为用户现场遮挡面积。

## 已有包分析能给出的角色规则

依据 [BEM 实际骨骼/权重分析](BEM_HAIR_BONE_ANALYSIS_20261002.md)，以下是既有 PC 几何样本，不是本次 Android 的新权重采样，也不代表所有可选外观：

- 校园 `cloth_02` 的实际 draw 为 2,610 个纯 Head/tail 面，没有身体面；逆兔同名部件另有 9,674 个身体面。**不能把“提弗洛斯 cloth_02 整块删除”写成适用于所有 BEM 的角色规则。**
- 校园 `hair_01` 有 96 个 Head/Neck 混合顶点，因双材质 draw 提交 248 个混合边界面；需要局部头发边界规则。不能只给角色全局允许 Neck，否则会扩大到衣领/身体。
- 当前 package geometry、bone palette 和 draw 是裁剪依据；角色表增加骨骼语义，不把原版 Renderer 名称自动赋予每个替换包相同的几何语义。

## 建议的逐角色表

通用规则作为兜底，角色表优先补经过验证的例外；未来录入一个角色主要更新数据，不反复改全角色算法。

| 数据 | 用途与限制 |
|---|---|
| 实际模型/骨架来源 ID、平台、资源版本、world/UI/LOD | 区分当前骨架与显示外观；不要只根据中文角色名或 Mesh 名推断。现有 `Entity → modelCom` 路径可复用，PC dump 的 `BaseModelComponent.modelId/modelPath` 是待验证的具名入口，不直接移植 RVA。 |
| Head/neck 根、额外隐藏骨骼根的精确相对路径、已确认路径 alias、保留根 | 名称用于找到当前真实 Transform，然后依据祖先关系映射本 renderer 的 palette。BEM palette 可能重排或合并，不能存一份角色通用的固定 bone index。 |
| 独立原版部件例外及其版本/几何身份 | 仅在有对应原版数据证明时完整隐藏；同名 BEM 替换件不能自动继承。 |
| 局部 Neck 边界规则 | 限定角色、部件及已确认几何用途；BEM mixed 继续根据当前包权重/必要空间边界处理，不全局放行 Neck。 |
| 特殊 Renderer 类型、真实可见性与 shadow/proxy 策略 | 确认 `hairshadow` 是否为可见辅助件、是否存在普通 MeshRenderer；不同角色/材质的 ShadowsOnly 需要视觉验收，无法仅靠 getter 证明渲染结果。 |
| 证据状态与可用数据路径 | 已核实、待核实分开。CPU cache 未就绪/淘汰或原版不可读时，不能把未知几何当成已精确剔除。 |

例子：女管理员 Spine2 下 `maozi_a1_M → maozi_a2_M → maozi_a3_M` 可作为待核实帽链候选；现有数据只确认路径，不包含原版权重/索引，不能将候选直接宣称为已经可安全隐藏的规则。提弗洛斯应优先验证 hair/face 的局部 Neck 边界，而非寻找并不存在于当前证据中的“骨骼改名”。

运行顺序为：确认当前模型来源 → 选角色规则/通用兜底 → 找真实骨骼路径 → 映射当前 palette → 按实际 draw 的非零影响算可见索引 → 可用适配层构建变体 → 保留完整源投影 → 退出/切人/换包恢复所属状态。BEM V1 桥 `NotFound` 同时包括缓存淘汰和非 BEM 网格，不能将它直接解释为“原版，可以套用整块原版例外”；需要额外的来源确认或继续保守处理。

## 后续最小实施与验收顺序

1. 修复 Android clone/layout/index 适配，使当前精确裁剪能够实际执行；一次日志应明确 CPU 来源、实际裁剪/保留面及变体绑定结果。
2. 固定校园包 generation、`appearance:v0`、当前 LOD 和角色，再核对 hair/face 的剩余几何，补提弗洛斯局部骨骼语义/边界规则。
3. 其它角色逐一录入其实际帽链、独立部件和必要边界；通用规则不继续无证据扩大。原版无权重数据的 mixed 部件须对应平台数据或已验证部件策略。
4. 检查逆兔/校园之间换包、退出/切人、Head+Neck、Spine2 帽、完整投影与外部接管。没有新实机裁剪结果前，不声称角色表已解决用户这个包的所有残留。
