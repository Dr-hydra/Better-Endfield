# 昼雪 BEM：1.6.3 静态资源变化

2026-10-15，Windows `chr_0014_aurora`（昼雪 / Snowshine）。仅定点读取当前有效 VFS 中昼雪的 world/UI 依赖、既有 1.5.3 graph 和本机七份包的 target；未修改游戏、catalog、BEM 或 `verified` 标记，未计算产物哈希。

## 已确认的失配

**昼雪模型确实发生了变化。C1 `S_actor_aurora_cloth_01_lod0` 的原索引数由 87120 变为 87288，而检查的七份包仍全部要求 87120。** 该差异同时存在于当前大世界 `postmodel` 和详情 `uimodel`，不需要运行时采集才能确认。

| 部件 | 1.5.3 / 旧 catalog | 当前 1.6.3 | 已确认的影响 |
| --- | --- | --- | --- |
| C1 cloth_01 LOD0 顶点数 | 23073 | 23141 | 网格顶点数量变化 |
| C1 cloth_01 LOD0 原索引数 | 87120 | 87288 | 旧包的严格原网格匹配条件不成立 |
| C1 骨骼 | 87 根 | 87 根 | 名称集合相同，10 个 palette 槽位重排 |
| C5 eyebrow LOD0 顶点数 | 632 | 1516 | 顶点数量变化，索引数仍为 2088 |
| C5 离线存储流步幅 | `[16,16,12]` | `[40,16,32]` | 序列化布局变化；**不是新运行时声明的实测值** |

两版均解析出 53 个 Mesh、65 个 renderer，65 条完整 renderer 路径及 mesh 名称均保持一致；当前 backend issue 为 0。world/UI prefab 路径未改，87 个依赖 bundle 的路径集合也未改，不能只凭文件名判断模型没有变化。旧 catalog 仍为 `20260920-identities1`，其 C1 原索引数记录在 [catalog](../../../../../tools/CustomModel/catalog/chr_0014_aurora.json:198)。

## 七份现有包的直接对照

以下是包内 `target.components[1].original_index_count` 与当前 world/UI C1 的直接比较，不依赖猜测包的来源。七份包的 12 项 target 名称、原索引数、骨骼名数组及材质名数组均与旧 catalog 相同。

| BEM | 格式 | 包内 C1 原索引数 | 当前 world / UI C1 | 匹配 |
| --- | --- | ---: | ---: | --- |
| 昼雪-坏猫 by 73layol | 1.1 | 87120 | 87288 / 87288 | 不符 |
| 昼雪-外卖员 by 孤军 | 1.1 | 87120 | 87288 / 87288 | 不符 |
| 昼雪-天狼星 by 虹汐哥 | 1.1 | 87120 | 87288 / 87288 | 不符 |
| 昼雪-快递员 by 孤军 | 1.1 | 87120 | 87288 / 87288 | 不符 |
| 昼雪-穗香比基尼 | 1.1 | 87120 | 87288 / 87288 | 不符 |
| 昼雪-纯白恶堕 by fenglis | 1.1 | 87120 | 87288 / 87288 | 不符 |
| 昼雪-运动服饰 by hajimicat | 1.2 | 87120 | 87288 / 87288 | 不符 |

包位于 `G:\zmd_bem\成品\昼雪`。只读取 target，未解码或更改 mesh/texture payload。原始检查结果见 [package-targets.json](../../../../../research/custom_model/1.6.3/Windows/snowshine-bem-investigation/package-targets.json)。

运行时 adapter 的原索引数直接取自包内 target，而不是读取最新全局 catalog：[mod_registry.cpp:165](../../../../../native/modules/custom_model/mod_registry.cpp:165)。默认严格校验时，[SelectUnique](../../../../../native/modules/custom_model/generic_model_matcher.h:81) 要求原网格名称及索引总数同时相符；[组件匹配入口](../../../../../native/modules/custom_model/module.cpp:3236) 遇到任一待安装组件不能唯一匹配即返回失败。因此包含 C1 的上述旧目标无法通过当前默认 matcher，这是能解释多个昼雪包同时失效的共同拒绝条件。

**这里确认的是静态不兼容及代码推导出的拒绝路径，尚未取得昼雪失败会话的实测日志。** 没有把当前没有记录的 `index-count` 日志写成已经发生的观测，也不声称所有用户设置、所有历史昼雪包都已逐一运行验收。

## 骨骼与布局的实际变化范围

C1 的骨骼名称没有新增或减少。零基槽位 37–44 的变化为：

```text
旧：L_Finger11, L_Finger12, L_Finger3, L_Finger4, L_Finger2, L_Finger31, L_Finger32, L_Finger02
新：L_Finger02, L_Finger11, L_Finger12, L_Finger3, L_Finger4, L_Finger2, L_Finger31, L_Finger32
```

以上名称均带 `Bip001_` 前缀；槽位 64/65 的 `L_sleeve_03_jnt` 与 `L_sleeve_01_jnt` 另有互换，共 10 个槽位。按骨骼名重新对齐后，所有 65 条 renderer 的 bindpose 最大绝对差异约为 **6.6×10⁻⁷**，均小于 `1e-5`。原始数组直接比较会把 palette 重排当成很大的矩阵变化；现有证据不支持“骨架或身体比例整体重做”。

C5 眉毛的序列化 normal 由 HG 打包标记变为 float3，新增 float4 tangent，权重/骨骼索引存储也改变。该变化要求后续检查实际运行时声明；本次 `runtime_layout_observed=0`，不能将离线步幅直接发布为已验证 profile。65 条 renderer 的材质名及实际绑定的纹理名称没有变化，部分材质增加了空纹理属性，这不是纹理被替换的证据。

## 修复所需的最小后续工作

确认模型变化和七份旧 target 的共同 C1 失配，**不再需要运行时数据**。若继续修复旧包，则需定点取得昼雪 world/UI 的当前运行时声明，重点核实 C5，并评估 C1 骨骼索引映射。之后用代表包完成两个资源的实际加载与显示验收；只更新 catalog 不会改变七份旧包中已经嵌入的 target。

不应仅改一个索引数字或全局关闭验证便宣称修复。正常旧包失败日志与原始 donor probe 是两个步骤：probe 会暂停该进程中的模型替换，不能同时拿它验证旧包失败或修复后的显示。最小采集方法及日志边界见 [运行时调查](RUNTIME_REVIEW.md)。

复现与证据：[定点提取说明](../../../../../research/custom_model/1.6.3/Windows/snowshine-bem-investigation/STATIC_EXTRACTION.md)、[当前原始 graph](../../../../../research/custom_model/1.6.3/Windows/snowshine-bem-investigation/current/raw.json)、[静态比较脚本](../../../../../research/custom_model/1.6.3/Windows/snowshine-bem-investigation/static_compare.py)、[比较结果](../../../../../research/custom_model/1.6.3/Windows/snowshine-bem-investigation/static_comparison.json)。比较按精确资源根和 renderer 路径配对，通过原始对象引用取 Mesh，不以相似名称或 manifest GUID 拼接对象。
