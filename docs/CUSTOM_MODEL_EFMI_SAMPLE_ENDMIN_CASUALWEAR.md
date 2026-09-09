# EFMI 样本分析：Endmin in Casualwear

状态：真实样本验证记录  
用途：`BetterEndfield.CustomModel` / BEM v1 设计输入

> 本文只记录从用户提供的、在当前游戏与当前 EFMI 环境中可正常运行的 MOD 包里直接验证到的结构。MOD 文件本身不提交到仓库。

## 1. 样本基本信息

`mod.ini`：

- EFMI INI 标识：`EFMI ALPHA-12 INI`
- `required_efmi_version = 1.43`
- `object_guid = 185751`
- `mesh_vertex_count = 31127`
- `component_count = 11`
- `max_instance_count = 8`
- `bones_count = 0`
- Mod 名：`Endmin Casualwear`
- 作者字段：`Parrley & Tanuki, reuploaded by Tosa`

公开 MOD 页面将目标分类为 `Endministrator (F)`。

## 2. 文件结构

样本包含：

```text
mod.ini
Meshes/
  Component0..9_IB.buf
  Component0..9_VB0.buf
  Component0..9_VB1.buf
  Component0..9_VB2.buf
  若干 VB1_LOD1 / VB2_LOD1
  Components.buf
Textures/
  24 个 DDS
```

`Component10` 有原始 EFMI entry point，但没有自定义 VB/IB；它在自定义绘制阶段不重画原模型，因此属于“保留 Component identity，但默认隐藏原 draw”的情况。

## 3. Components.buf 结论

`Meshes/Components.buf` 虽然后缀为 `.buf`，本样本中实际为 UTF-8 JSON，而不是不可解释的二进制。

它逐个 Buffer 描述 semantic、DXGI format 和 semantic stride。因此 importer 不需要只依赖 `mod.ini` 推断顶点布局。

本样本验证到的布局如下。

### VB0

常见：

```text
POSITION0    R32G32B32_FLOAT   12 bytes
ENCODEDDATA0 R32_UINT           4 bytes
----------------------------------------
vertex stride                   16 bytes
```

少量 Component 把第二字段标为 `UNKNOWN R8G8B8A8_UINT`，总 stride 仍为 16。

EFMI Tools 当前实现确认 `ENCODEDDATA` 是打包的 TBN 数据：10:10:10:2 中包含 octahedral normal、编码 tangent 与 bitangent sign。因此高质量转换器可以复用 EFMI Tools 的解码算法；PoC 也可以先只读取 Position，再由 Unity 重算 normal/tangent 以验证 skinning 路径。

### VB1

两种结构：

```text
TEXCOORD0 R32G32_FLOAT       8 bytes
COLOR0    R8G8B8A8_SNORM     4 bytes
------------------------------------
stride                       12 bytes
```

或仅：

```text
TEXCOORD0 R32G32_FLOAT       8 bytes
```

部分 LOD1 只保留 8-byte UV buffer。

### VB2

加权 skin：

```text
BLENDWEIGHTS0 R16G16B16A16_UNORM  8 bytes
BLENDINDICES0 R8G8B8A8_UINT        4 bytes
-------------------------------------------
stride                              12 bytes
```

本样本所有加权顶点的四权重和均约等于 1.0。

另有无显式权重的 rigid-index 结构：

```text
BLENDINDICES0 R8G8B8A8_UINT  4 bytes
```

样本中的 Component 2、6、7 使用该结构。实际数据中 Component 2/6 全部是 `(0,0,0,0)`；Component 7 只改变第一个 index，后 3 个均为 0。因此 BEM/runtime 需要允许“单骨索引、隐式权重 1.0”的 skin 模式，不能把缺少 `BLENDWEIGHTS` 当成无 skin。

### IB

`Components.buf` 与 `mod.ini` 都把 IB 描述为 `R16_UINT`，semantic stride 为 6。实际 `.buf` 大小与 `drawindexed index_count * 2` 完全一致，因此物理数据是连续 little-endian uint16 index；这里的 6 bytes 对应三角形的 3 个 16-bit index，不应被 runtime 误解为单 index stride。

## 4. Component 统计

| Component | 原 entry hash | 原 index count | 自定义 vertex | 自定义 index | 自定义 draw range | Skin | 最大 bone index | 独立 LOD entry |
|---:|---|---:|---:|---:|---:|---|---:|---:|
| 0 | `638e0992` | 27615 | 7911 | 27615 | 1 | weighted | 27 | 1 |
| 1 | `4eabed4f` | 9000 | 1807 | 9000 | 1 | weighted | 52 | 0 |
| 2 | `5825df15` | 4524 | 1548 | 4524 | 1 | rigid-index | 0 | 1 |
| 3 | `827a29bc` | 20577 | 2639 | 6261 | 2 | weighted | 13 | 1 |
| 4 | `586dadd6` | 1638 | 508 | 1638 | 1 | weighted | 41 | 1 |
| 5 | `fa04a7b6` | 16524 | 3490 | 16884 | 1 | weighted | 43 | 0 |
| 6 | `b1f947ec` | 117 | 41 | 117 | 1 | rigid-index | 0 | 1 |
| 7 | `57f73db4` | 1386 | 276 | 1386 | 1 | rigid-index | 5 | 0 |
| 8 | `ca67e19e` | 90 | 34 | 90 | 1 | weighted | 20 | 0 |
| 9 | `9cd919fa` | 101994 | 12873 | 59073 | 7 | weighted | 155 | 1 |
| 10 | `316571b5` | 2286 | 0 | 0 | 0 | original hidden | - | 1 |

验证：11 个原始 Component 的 `match_index_count` 总和正好为 `185751`，与 `$object_guid` 完全相等。

自定义 VB0 顶点总数正好为 `31127`，与 `$mesh_vertex_count` 完全相等。

这说明：

- `object_guid` 在该标准输出中可以作为“原始对象所有 Component index count 合计”的快速身份校验；
- `mesh_vertex_count` 可以作为自定义几何总顶点数的完整性校验；
- 但两者都不应单独作为跨版本稳定主键。

## 5. BEM 必须增加 draw-range 层

该样本证明 EFMI Component 不能简单建模为“一个 Component = 一个最终 Unity Renderer / 一个最终 draw”。

例如 Component 9 共享一套 VB/IB，却包含 7 个独立 draw range：

```text
Clothes 01    indexCount=10485 offset=0
Clothes 02    indexCount=2424  offset=10485
Clothes 03    indexCount=5208  offset=12909
Shoes         indexCount=9564  offset=18117
Thighhighs    indexCount=28080 offset=27681
Watch         indexCount=1437  offset=55761
Component9.005 indexCount=1875 offset=57198
```

Component 3 也包含两个 range：Hair clip 与 Watch。

因此 BEM v1 至少应建模：

```text
Component
  - source identity / original signature
  - vertex streams
  - index buffer
  - skin mode
  - drawRanges[]
      - name
      - indexOffset
      - indexCount
      - defaultVisible
      - future variant/toggle metadata (optional)
```

运行时可以选择：

- 把 draw range 映射为 Unity submesh；或
- 在确认原游戏 Renderer/submesh 结构后合并兼容范围。

在拿到运行时 probe 前，不冻结“一 EFMI Component = 一 Unity Renderer”的映射。

## 6. LOD 结论

样本包含多处 LOD1 entry hash，并且若干 Component 提供 `VB1_LOD1` / `VB2_LOD1`。

值得注意的是，EFMI 自定义 draw 在 LOD1 仍可继续复用相同自定义 VB0 与 IB，仅替换与该 LOD shader/input layout 对应的 VB1/VB2。这说明 EFMI 的 LOD 数据不等同于“另一套低面数 Unity Mesh”。

BEM v1 不应机械复制 EFMI 的 LOD 表达。PoC 可先固定自定义几何并让 Unity 原有 LOD 生命周期保持不变；是否需要针对目标 Renderer/LODGroup 建立独立 Mesh 版本，需运行时 probe 后决定。

## 7. Toggle 结论

样本包含一个 `KeySwapMask`：

```text
$swapvar_mask = -1, 0
```

并控制 Component 2 与 6 的 Mask draw range。

第一阶段不实现通用 INI VM。

推荐：

1. PoC importer 解析标准 `drawindexed` range；
2. 默认按 INI 初始状态解析 `defaultVisible`；
3. 后续如需要兼容常见 EFMI toggle，把它转换为 BEM 自己的 variant/visibility group，而不是在 runtime 执行 EFMI 表达式。

## 8. Texture 结论与当前阻塞点

样本有 24 个 DDS，包含：

- BC7 UNORM；
- BC7 UNORM SRGB；
- legacy `BC5U`；
- 512、1024、2048，以及 4096x2048 等尺寸。

文件名通常包含受影响 Component 和原始 texture hash，例如：

```text
Components-2-3-9-10 t=7d330dbd BC7-Linear.dds
```

但标准 `mod.ini` 的 TextureOverride 只按 GPU texture hash 匹配：

```text
[TextureOverride_TextureX]
hash = ...
this = Resource_TextureX
```

它没有保存 Unity `Material` property 名或明确的 PS slot。也就是说，仅凭最终 MOD 包不能可靠得出：

```text
EFMI texture hash -> Unity Material._SomeTexture
```

因此纹理完整兼容需要额外的 target-character texture signature/mapping 数据，或从 EFMI 原始 Frame Analysis / extractor metadata 建立映射。

这个问题不阻塞 Mesh/skeleton PoC：PoC-1 先保留原 Material/Texture，仅验证 geometry + skin。

另外，DDS 文件名中的格式字符串不能作为唯一真值；converter 应读取 DDS header 判断真实格式。

## 9. Skeleton 结论

该样本不是 EFMI Merged Skeleton：`bones_count = 0`。

但 Component 9 的最大 blend index 达到 155，并且实际使用约 62 个 index 值。这进一步说明必须在目标 Endministrator (F) 运行时验证：

- 对应 `SkinnedMeshRenderer.bones.Length`；
- EFMI Component 是否对应 Renderer 或 Renderer submesh；
- blend index 是否可直接作为该 Renderer 的 local bone slot；
- 原 Mesh bindpose 数量及其与 bones 的关系。

在该 probe 完成前，不把 EFMI blend index 解释为全局 Transform ID，也不做猜测式重映射。

## 10. 下一步最小 probe

下一步最有价值的数据不是更多 EFMI 文件，而是当前游戏版本中 Endministrator (F) 的 Unity 运行时结构。

probe 至少输出：

```text
角色根对象名称 / instance id
所有 SkinnedMeshRenderer：
  renderer.name
  sharedMesh.name
  sharedMesh.vertexCount
  sharedMesh.subMeshCount
  每个 submesh indexCount / indexStart / baseVertex
  mesh.indexFormat
  bones.Length
  每个 bones[i].name（至少首轮调试保留）
  rootBone.name
  bindposes.Length
  sharedMaterials.Length
  material.name
  texture property name -> Texture.name / size / format（若可读取）
```

首轮尤其要把原始 11 个 EFMI `match_index_count`：

```text
27615, 9000, 4524, 20577, 1638, 16524,
117, 1386, 90, 101994, 2286
```

与 Unity Renderer/submesh 的 index count 做一一匹配。

如果能完整对上，就能确定 CustomModel runtime 应该是“替换原 Mesh 的 submesh/Renderer”还是“创建新的附属 Renderer 并绑定原 skeleton”。

## 11. 当前已经可以冻结的 BEM v1 语义

基于本样本，可把以下语义从“猜测”提升为 BEM v1 必须支持：

- 多 Component；
- 每 Component 独立 vertex streams；
- R16 index 输入至少必须支持；
- Position + packed TBN；
- UV；
- 可选 vertex color；
- weighted skin；
- rigid single-bone skin（无显式 weight）；
- Component 内多个 draw range；
- original component hide/no replacement geometry；
- source original signature：hash + original index count；
- source/converted total-count 完整性校验；
- LOD source metadata 可被 importer 记录，但 runtime 表达仍待 probe 后冻结；
- texture hash 作为 source metadata 保留，但不能把它当成 Unity material property mapping。

## 12. 当前仍不能冻结的部分

- Component -> Unity Renderer / submesh 的最终 identity；
- bindpose 策略；
- blend index 是 renderer-local palette 还是需要额外 remap；
- BEM texture property 映射；
- LODGroup 的实际应用策略；
- 是否需要创建新 SkinnedMeshRenderer，而不是改写现有 Renderer；
- Android 最终 texture payload 格式。
