# BEM 骨骼联动去头部：实际权重核对

结论：**可以用 BEM 保留的骨骼身份实现更可靠的剔除，而且有效包里的 CPU 几何能让 Android 绕过 GPU 回读。** 用户允许第一人称下把尾巴一起隐藏后，Aglina 两个 hair 部件的处理已经能给出明确分类。真正需要区分的是同一 Mesh 内仍要保留的躯干/衣服，以及 Head/Neck 混合边界；不是要求每根头发与尾巴都精确拆开。

本记录保留研究阶段的数据和结论；后续双端实现与通用回退见 [去头发与阴影实现记录](BEM_HAIR_SHADOW_IMPLEMENTATION_20261002.md)。离线分类和编译测试不等于已实机验证隐藏、阴影或退出恢复。

## 数据与统计口径

- PC 原始运行时：AppData `BetterEndfield/catalog/custom-model/native-probe/characters-20260919-202150-881718.jsonl`，本次选择 Aglina world/UI、提弗洛斯 UI、女管理员 world/UI；噗切娜采用 `artifacts/runtime-purrche-20261001/capture/` 的 world/UI。共 7 个 PC 根、232 个 Renderer。
- Android 已有直接记录：`artifacts/android-refactor/android-source-metadata.jsonl` 的女管理员 world，共 40 个 Renderer。本轮没有 Aglina 或噗切娜 Android 权重实采，不将 PC 顶点/索引布局直接当作 Android 原版世界布局。
- Aglina 当前离线：从安装游戏只读提取 world/UI 的 87 个依赖 bundle，`artifacts/bem-hair-analysis-20261002/aglina-current-inputs/`；Version 为 `2954fa80-23c1-1579-2b22-4ecfd6d70418`，没有缺失 bundle。
- 噗切娜离线：使用已归档的 85 个依赖 bundle，保留此前 manifest chunk 来源。未用“Version 相同”假定两次 payload 相同。
- BEM CPU 数据：逆兔修正包 `typhoea-rabbit-texture-pin-fixed.bem`、校园包 `typhoea-campus.bem`、女管理员 `endmin_in_casualwear.bem`，合计 **33 个 Mesh，33 个均完成权重与 draw 索引分析**。
- 结果/脚本保存在 ignored 的 `artifacts/bem-hair-analysis-20261002/`，没有导出/发布原版几何，也未计算产物哈希。

以下“使用骨骼”只计**组件实际 draw 索引引用的顶点中、权重严格非零**的项。palette 携带但没有这些影响的骨骼单列为 unused；未被 draw 引用的顶点不影响本次可见几何判断。重复材质 draw、fur 多层子网格的三角形按实际索引提交次数统计，不假装它们都是独立几何面。

基础允许隐藏集合 A：`Bip001_Head` 的实际子树（包含脸、眼、发、耳、帽等）加明确的 tail 骨骼链。其余集合先按“需要保留或进一步核对”处理；**Neck 不全局加入 A**，避免一起吞掉身体和衣领。

原版离线的小型分析器直接读原始 UNorm16 权重和 UInt8 索引流，不使用后端的再构造 `m_Skin` 来猜权重。Aglina 成功分析 48 个 Renderer，剩余 3 个无显式权重的 iris/eyebrow LOD 单独记录为格式缺口；噗切娜成功分析 46 个 Renderer，53 个无显式权重的声明单独记录。下面涉及的 hair/face/fur 部件全部完成了解码、权重归一和索引边界核对。

## Aglina：允许尾巴一起隐藏后，两个 hair 可以整体处理

当前原版 LOD0 非零权重结果如下，重复 world/UI 同 Mesh 不累加：

| 部件 | 实际引用顶点 | Head 子树顶点 | 尾巴顶点 | Head/尾巴与其他混合顶点 | 实际使用/palette |
| --- | ---: | ---: | ---: | ---: | ---: |
| `hair_01` | 15,493 | 15,192 | 301 | 0 | 38/38 |
| `hair_02` | 2,041 | 184 | 1,857 | 0 | 13/13 |
| `hairshadow_01` | 202 | 202 | 0 | 0 | 7/7 |

`hair_01` 的 21,710 个三角形由 21,257 个 Head 子树面和 453 个尾巴面组成；`hair_02` 的 2,360 个三角形由 140 个耳部面和 2,220 个尾巴面组成。两个部件的非 Head 影响都是 `tail_base_M_a_01_jnt` 至 `_09_jnt`，不是衣服或躯干。`ear_base_L/R_a_01/02_jnt` 的实际路径在 Head 之下。

因此，在用户允许“头部、耳饰、尾巴一起隐藏”的目标下，两个部件的全部绘制几何都落在 A 内，可以完整隐藏并保留完整投影，**不需要为了保尾巴先 GPU 回读或做局部裁剪**。原判定仅允许 Head 子树，所以 tail 项使它拒绝直接隐藏；这并不表示 BEM 破坏了名称。

但 `face_01` 还含 60 个 Head/Neck 混合顶点（1,699 个引用顶点中的 60 个），120 个边界混合三角形；这里需要角色脸部身份或已核对的部件例外，不能仅把 Neck 一概放行。

## 噗切娜：同叫 fur 的部件不同，骨骼能提供具体分类

| LOD0 部件 | 引用顶点 | 仅 Head 子树 | 仅保留集合 | Head 与保留集合混合 | 使用/palette |
| --- | ---: | ---: | ---: | ---: | ---: |
| `fur_01_lod0_20` | 17,800 | 740 | 11,240 | 5,820 | 15/15 |
| `fur_02_lod0_20` | 15,900 | 15,900 | 0 | 0 | 10/10 |
| `fur_03_lod0_20` | 28,600 | 26,100 | 0 | 2,500 | 16/16 |
| `cloth_02_lod0` | 1,958 | 1,958 | 0 | 0 | 7/7 |

- `fur_01` 的非 Head 项包括围巾 `scarf_baseCenChildren_*`、`scarf_baseDnChildren_*`、`scarf_base_L_e_*` 以及 Clavicle、Spine2、Neck，全部都有非零实际引用。因此不能把所有 fur 整块去掉。按索引提交计，它有 860 个纯 Head 面、19,060 个纯保留面、11,400 个混合面。
- `fur_02` 的 10 个使用骨骼都是 Head 子树，其中 9 个是 `hat_base_*` / `brim_base_*`，没有名字含 hair 的骨骼。它证明“Head 子树”可以明确识别用户也要隐藏的头饰，但不等于字面“头发”。
- `fur_03` 使用 9 条 `hair_base_*`、Head、5 条帽檐骨骼以及 Neck。唯一的非 Head 项是 Neck，影响 2,500 个顶点；没有纯躯干顶点。它适合作为经过角色资源核对的完整头部部件例外；不能把这一结果推广为全角色的 Neck 一律隐藏。
- `cloth_02` 全部 1,958 个顶点都受 Head 子树影响。名称 cloth 不能自动等同“必须保留的身体衣服”；同样，名称 hair 也不是唯一识别依据。

这些是 PC 已归档资源的实际权重证据。Android 的对应原版 world Mesh 是否同样可完整隐藏，需要该平台实际 palette/部件关系确认；BEM 替换后的 Mesh 则可以直接以包内已选 geometry 加实际绑定骨骼计算。

## 提弗洛斯：同一原始目标，两个 BEM 的可见几何不同

| BEM / 目标部件 | 引用顶点分类 | 提交三角形分类 | 使用/palette |
| --- | --- | --- | ---: |
| 逆兔 `cloth_02` | A 2,391；身体 8,179；混合 0 | A 2,610；身体 9,674；混合 0 | 126/126 |
| 校园 `cloth_02` | A 2,391；身体/混合 0 | A 2,610；身体/混合 0 | 27/27 |
| 逆兔 `cloth_04` | A 3,944；身体/混合 0 | A 5,367；身体/混合 0 | 7/7 |
| 逆兔 `body_01` | 身体 13,193；A/混合 0 | 身体 24,329；A/混合 0 | 36/86 |
| 逆兔 `cloth_03` | 身体 114；A/混合 0 | 身体 92；A/混合 0 | 9/37 |

逆兔 `cloth_02` 中参与 A 的骨骼既有 hair donor 的 `hair_R/L_side_*`，也有 cloth donor 的 `hair_L_bowknot_*`；例如 `hair_R_side_a_03_jnt` 来自 donor component 10/index 15，而蝴蝶结来源可以是 component 2。最终局部 palette 已合并重排，不能把 target 组件原始 bone index 直接解释为新 Mesh index。

这里 BEM 保留骨骼对象身份**确实有用**：逆兔 `cloth_02` 的头部几何与身体几何在非零影响上完全分离，可以依据 A 只删 2,610 个头部面，保留 9,674 个身体面；校园包同名目标已经完全是 A，可以整体处理。固定的“角色 cloth_02 整块隐藏”规则会在逆兔包误删身体，按**当前所选 BEM 的 CPU weights+indices**计算则能区分。

`body_01` palette 的 86 项只在本组件 draw 中使用 36 项，unused 的 50 项里包含 `Bip001_Head`；`cloth_03` 37 项只使用 9 项，unused 也包含 Head。这直接证明 **palette 包含 Head 不等于正在绘制头部，也不等于一个 Renderer 的所有骨骼都要参与可见分类**。

两包 `hair_01` 的 15,306 个引用顶点中，有 96 个同时受 Head 子树和 Neck 影响；不是骨骼改名，也不是原版身体大面积进入头发。逆兔提交 124 个此类边界面，校园因为双材质 draw 提交 248 次。

## 女管理员与 Android：保留名称可跨端定位，原版 world 几何仍要区分

女管理员 BEM 的 `hair_01` 7,911 个引用顶点中，7,827 个只受 Head 子树影响、84 个受 Head/Neck 混合影响，28/28 palette 项均使用。`face_01` 1,807 个顶点中有 50 个 Head/Neck 混合顶点。`cloth_01` palette 156 项，实际 draw 只使用 62 项；12,675 个顶点没有 A 影响，198 个混合，不能因为它含 Head 就整体隐藏。

Android 已采的女管理员 world `hair_01_lod1` palette 也是 Head 子树 27 项 + Neck 1 项；`face_01_lod1` 是 Head 子树 52 项 + Neck 1 项，实际路径关系可确认。`cloth_01_lod1` palette 则为 Head 1 项 + 非 Head 155 项。**这些 Android 记录没有权重/索引 payload**，因此不能声称 Android 原版 hair 的 84 个混合顶点也已直接测得，也不能仅看 palette 证明其中 155 项都实际使用。

这与 BEM 替换后的情况不同：替换几何、权重和 indices 已经在包内，Android `SupportsAsyncGPUReadback=false` 不妨碍读取那些 CPU 字节。

## 不新增标注即可做到的范围，以及真正的缺口

1. 原版或 BEM 独立部件：全部实际绘制影响都在 A，或者全部 palette 都是已确认 A（保守但无需读权重），直接处理整个 Renderer；Aglina 两个 hair 和噗切娜 `fur_02` 有明确数据支持。按用户要求，tail 归 A。
2. BEM 混合部件：`BemComponent` 已保存 `streams`、`indices`、`bones`、`bone_names`、`draws`（`native/modules/custom_model/bem.h:93`），`DecodeComponentSkin` 已从 CPU stream 解码有效权重（`custom_model/module.cpp:1050`）。无需重新 GPU 回读，也不必要求所有包都新增“第一人称 metadata”。在加载/选包时按实际引用顶点预计算 A 分类和替代索引，保留原 Mesh 投影。
3. 重绑后分类要以最终 bone 对象和 live 路径确认。`BuildMergedSkin` 从 donor 取得原骨骼 Transform，校验名称/alias 后放入新 palette（`custom_model/module.cpp:1695`）；Android world adapter 用相对路径及 BEM 1.2 alias 对照实际 world 骨骼。BEM 没有为了改模创造一套随意重命名的骨架，但 palette 的顺序、来源和某些 world/UI 名称别名可能不同。
4. 真正需要人工或角色例外的是 Head/Neck 或其他身体权重连续混合的边界，以及 Mod 把身体几何绑定到 Head 这类语义变化。相同 bone name+weight 本身不能区分两块不同用途的几何。对已证实的 face/hair/head accessory 可以允许局部 Neck 边界；对 body/cloth mixed 不使用全局 Neck 规则。更复杂包再提供可选语义标注，不能把它当所有现有包的先决条件。
5. 原版 Android mixed Mesh 若没有包内几何且 GPU 不可读，仍需平台对应离线数据/预计算结果，或者仅完整隐藏经过验证的部件；不能从仅含 names 的 catalog 现场恢复三角形权重。BEM `keep` / 纹理替换操作同样没有提供该部件原始 geometry，不能假设每个 BEM 都覆盖了所有原版 Mesh。

一个已发现的格式缺口：BEM stride4 蒙皮按第一个索引隐含 weight=1（`DecodeComponentSkin`），但声明仍可为 UInt8x4、无 BlendWeights。当前相机 `FpDecode` 在无权重且索引 dimension 不是 1 时拒绝。离线原版亦观察到这类声明。33 个 BEM Mesh 的本轮统计按现有 BEM CPU 解码规则处理了隐含权重及 Float32/UInt32 扩展；不把拓扑路径格式拒绝归因于骨骼名称无效。

## 建议落地边界

最小版本先扩大允许隐藏集合至 Head 子树 + tail，并把经验证的独立头部 Renderer 做可恢复的只投影显示；影子代理只用于保影子，不进入可见头发裁剪。完整投影、躯干/衣服保留和退出/切人恢复仍然必须满足。

BEM 联动版本在 CustomModel **选定 geometry 后**利用已有 CPU 数据预计算，提供正式的第一人称查询/请求接口或由 CustomModel 维护可见/投影两种 Mesh，避免 Camera 直接读取另一模块私有内存。缓存键涵盖包选择、Renderer、Mesh、palette；hot switch、LOD、切人重建。这样能复用已有有效包，不需一开始就升级所有创作者包格式。

最终效果仍需在当前 HG 双端渲染流程验证 `ShadowsOnly` / `shadowProxyMesh` 的投影行为、换包与退出恢复；本轮数据只证明分类和 CPU 计算的可行范围。
