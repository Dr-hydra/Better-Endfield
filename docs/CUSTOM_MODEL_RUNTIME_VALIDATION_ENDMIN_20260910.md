# BetterEndfield.CustomModel 运行时验证：Endministrator (F)

状态：**PoC-1 设计输入已验证**  
日期：2026-09-10  
目标：`chr_0003_endminf` / Endministrator (F)  
EFMI 样本：`Endmin Casualwear`（EFMI ALPHA-12，required EFMI 1.43）

> 本文记录真实游戏运行时 probe 与用户提供、当前 EFMI 可正常运行的 MOD 样本交叉验证后已经可以冻结的结论。样本 MOD 与运行日志本身不提交仓库。

## 1. Probe 结果概览

`BetterEndfield.CustomModel 0.0.2-probe` 在当前 Windows 客户端中成功：

- 动态解析 `SkinnedMeshRenderer` / `Mesh` / `Renderer` / `Resources` 所需 IL2CPP 方法；
- 使用 `CameraMono._ProcessDitherByPitch()` 作为测试环境主线程 pump；
- F8 请求由后台线程锁存，在 Unity 主线程消费；
- `Resources.FindObjectsOfTypeAll(typeof(SkinnedMeshRenderer))` 返回 918 个对象；
- 其中名称/路径匹配 `endminf` 的候选为 106 个。

候选较多是因为当前内存同时存在：

- 实际实例化的 `chr_0003_endminf_postmodel(Clone)`；
- 原始 prefab/resource 对象；
- LOD0 / LOD1；
- Shadow Proxy；
- VFX 辅助 Renderer。

真正用于本次 EFMI Component 映射的是活动角色实例：

```text
#..._chr_0003_endminf/
  mesh/
    chr_0003_endminf_postmodel(Clone)#80/
      Mesh_all/
        lod0/
        lod1/
```

## 2. EFMI Component = LOD0 SkinnedMeshRenderer

本样本的 11 个 EFMI Component 在角色实例 `Mesh_all/lod0` 中全部找到**唯一的精确 indexCount 对应项**。

这把之前的开放问题正式关闭：

> 对该标准 EFMI Tools 输出，EFMI `Component` 的原始对象语义是 Renderer 级，而不是“同一个 Unity Renderer 内的一个 submesh”。

每个对应的原始 LOD0 Renderer 当前都只有一个 Unity submesh。

| EFMI | source hash | `match_index_count` | LOD0 Renderer / Mesh | 原 vertex | bones | bindposes |
|---:|---|---:|---|---:|---:|---:|
| C0 | `638e0992` | 27615 | `S_actor_endminf_hair_01_lod0` | 7900 | 28 | 28 |
| C1 | `4eabed4f` | 9000 | `S_actor_endminf_face_01_lod0` | 1807 | 53 | 53 |
| C2 | `5825df15` | 4524 | `S_actor_endminf_cloth_03_lod0` | 1548 | 1 | 1 |
| C3 | `827a29bc` | 20577 | `S_actor_endminf_cloth_04_lod0` | 7811 | 14 | 14 |
| C4 | `586dadd6` | 1638 | `S_actor_endminf_eyebrow_01_lod0` | 508 | 42 | 42 |
| C5 | `fa04a7b6` | 16524 | `S_actor_endminf_body_01_lod0` | 3424 | 44 | 44 |
| C6 | `b1f947ec` | 117 | `S_actor_endminf_cloth_05_lod0` | 40 | 1 | 1 |
| C7 | `57f73db4` | 1386 | `S_actor_endminf_iris_01_lod0` | 276 | 6 | 6 |
| C8 | `ca67e19e` | 90 | `S_actor_endminf_eyeshadow_01_lod0` | 34 | 21 | 21 |
| C9 | `9cd919fa` | 101994 | `S_actor_endminf_cloth_01_lod0` | 27314 | 156 | 156 |
| C10 | `316571b5` | 2286 | `S_actor_endminf_cloth_02_lod0` | 470 | 29 | 29 |

C10 没有自定义 VB/IB；EFMI 仍使用它的 source signature 识别原 Renderer，然后不重画，从而隐藏该原 Component。因此 BEM 必须允许：

```text
component identity exists
replacement geometry absent
original renderer hidden
```

## 3. draw range 的最终语义

之前样本分析已经发现 C9 的自定义 IB 中有 7 个 `drawindexed` range、C3 有 2 个 range。

运行时 probe 现在证明：

- 原 C9 对应 `S_actor_endminf_cloth_01_lod0`，原 Unity Mesh 只有 **1 个 submesh**；
- 原 C3 对应 `S_actor_endminf_cloth_04_lod0`，原 Unity Mesh 同样只有 **1 个 submesh**。

因此 EFMI 自定义 `drawindexed` range 不是对原 Unity submesh 的镜像。

BEM 应继续保留两层：

```text
Component             // 对应目标 SkinnedMeshRenderer
  └─ DrawRange[]       // 自定义模型内部的可见性/Toggle/分段绘制语义
```

PoC-1 若暂不实现 Toggle，可把同一 Component 内所有默认可见 range 合并为一个 Unity submesh/index list；正式支持 visibility group 时再把 range 映射成多个 Unity submesh、动态 index list，或其他内部实现。BEM 文件层不得丢失这些 range。

## 4. Bone Index 语义已确认

对用户提供的 EFMI 样本，直接按 `Components.buf` 描述解析每个 `VB2` 的 `BLENDINDICES R8G8B8A8_UINT`，并与对应运行时 Renderer 的 `bones[]` 数量比较：

| Component | VB2 模式 | 自定义 vertex | EFMI 使用 bone index | 最大 index | Runtime `bones.Length` |
|---:|---|---:|---:|---:|---:|
| C0 | weighted | 7911 | 28 个 | 27 | 28 |
| C1 | weighted | 1807 | 53 个 | 52 | 53 |
| C2 | rigid-index | 1548 | `{0}` | 0 | 1 |
| C3 | weighted | 2639 | 9 个（子集） | 13 | 14 |
| C4 | weighted | 508 | 42 个 | 41 | 42 |
| C5 | weighted | 3490 | 44 个 | 43 | 44 |
| C6 | rigid-index | 41 | `{0}` | 0 | 1 |
| C7 | rigid-index | 276 | 6 个 | 5 | 6 |
| C8 | weighted | 34 | 21 个 | 20 | 21 |
| C9 | weighted | 12873 | 62 个（稀疏子集） | **155** | **156** |
| C10 | no custom geometry | 0 | - | - | 29 |

其中 C9 是最强的验证点：自定义模型虽然只使用 62 个不同 bone slot，但最高 slot 保留为 155，而游戏原 `S_actor_endminf_cloth_01_lod0.bones` 恰好为 156 项。

其他多个 Component 同时满足：

```text
max(BLENDINDICES) == renderer.bones.Length - 1
```

并且所有对应 LOD0 Mesh 都满足：

```text
mesh.bindposes.Length == renderer.bones.Length
```

因此 PoC-1 可以冻结如下语义：

> 非 Merged-Skeleton 的标准 EFMI Component 中，`BLENDINDICES` 是该 Component 对应 `SkinnedMeshRenderer.bones[]` 的 **renderer-local palette index**。

### PoC-1 Skeleton 绑定规则

对于这种 standard/non-merged EFMI：

1. 找到目标原 `SkinnedMeshRenderer`；
2. 保留原 `renderer.bones`；
3. 保留原 `renderer.rootBone`；
4. 新 Mesh 的 skin weights 中直接使用转换后的 EFMI bone index；
5. bindposes 首版直接复用原 Mesh 的 `bindposes[]`，前提是 BEM 校验 `maxBoneIndex < bindposes.Length == bones.Length`；
6. 不做基于骨骼名字的猜测式 remap。

若未来遇到 EFMI Merged Skeleton，则作为独立 capability 处理，不能套用本规则。

## 5. LOD 映射已确认

当前角色运行时有对应的 `Mesh_all/lod0` 与 `Mesh_all/lod1` Renderer。EFMI 的 LOD entry count 与运行时也能一一对应。

| C | LOD0 count | Runtime LOD0 | LOD1 count/语义 | Runtime LOD1 |
|---:|---:|---|---:|---|
| 0 | 27615 | hair lod0 | 24939 | hair lod1 = 24939 |
| 1 | 9000 | face lod0 | 同 IB count | face lod1 = 9000 |
| 2 | 4524 | cloth_03 lod0 | 2028 | cloth_03 lod1 = 2028 |
| 3 | 20577 | cloth_04 lod0 | 10014 | cloth_04 lod1 = 10014 |
| 4 | 1638 | eyebrow lod0 | 1638 | eyebrow lod1 = 1638 |
| 5 | 16524 | body lod0 | 同 IB count | body lod1 = 16524 |
| 6 | 117 | cloth_05 lod0 | 69 | cloth_05 lod1 = 69 |
| 7 | 1386 | iris lod0 | 同 IB count | iris lod1 = 1386 |
| 8 | 90 | eyeshadow lod0 | 同 IB count | eyeshadow lod1 = 90 |
| 9 | 101994 | cloth_01 lod0 | 59301 | cloth_01 lod1 = 59301 |
| 10 | 2286 | cloth_02 lod0 | 777 | cloth_02 lod1 = 777 |

重要的是：EFMI 自定义资源不一定提供一套独立低模 Position VB。该样本多处在 LOD1 继续复用自定义 VB0/IB，只切换 VB1/VB2；所以 BEM 不应简单把 EFMI LOD 理解成“第二个完整 Mesh 文件”。

BEM v1 应允许每个 Component 保存 source LOD signature 和可选 per-LOD stream overrides。

PoC-1 可以先只替换 LOD0；验证 Mesh + skin 后，再补 LOD1，避免第一步同时处理 LOD 切换。

## 6. Renderer identity：冻结到“复合签名”，不只看 indexCount

本次样本中 11 个 LOD0 `match_index_count` 都是唯一的，因此仅靠 count 已能找到正确 Renderer；但 BEM/runtime 不应把单个整数当成长期稳定主键。

BEM v1 Component 应至少保留：

```text
source:
  componentId
  sourceHash
  originalIndexCount
  lodSignatures[]

targetHint (可选/由 catalog 补全):
  characterId
  rendererName
  meshName
  lodLevel
```

运行时匹配推荐优先级：

1. `characterId + rendererName/meshName + lodLevel`（有目标 catalog 时）；
2. `characterId + lodLevel + originalIndexCount`；
3. 若唯一候选无法确定，拒绝应用，而不是猜测。

`sourceHash` 继续保留用于 EFMI/source provenance 和 converter 校验；Unity 对象层当前没有证据表明它能直接读取同一个 GPU buffer hash，因此不把它作为 runtime Unity 对象查找键。

## 7. BEM v1 当前可冻结字段

基于 EFMI 样本 + 实机 runtime probe，以下内容已经有足够证据冻结为 v1 语义：

```text
Model
  formatVersion
  sourceFormat
  targetCharacterId
  components[]

Component
  componentId
  sourceHash
  originalIndexCount
  targetRendererHint?       // renderer/mesh/lod，catalog 可补
  replacementMode           // replace | hide-original
  vertexStreams
  indexBuffer
  skinMode                  // weighted | rigid-index
  drawRanges[]
  lodVariants[]

Skin
  boneIndexSemantic = renderer-local
  maxBoneIndex

LODVariant
  lodLevel
  sourceHash?
  originalIndexCount?
  streamOverrides?

DrawRange
  name
  indexOffset
  indexCount
  defaultVisible
  visibilityGroup?          // 为未来 toggle 预留
```

仍不在本次冻结：

- container 物理封装（zip/单 bin/manifest + sidecar）；
-最终 texture payload；
- EFMI texture hash -> Unity material property；
- Merged Skeleton；
- GPU ShapeKey；
-任意 XXMI/3DMigoto INI VM。

## 8. PoC-1 实现路径已明确

首个真正替换 PoC 不需要再做 Renderer/bone 猜测：

```text
Endmin Casualwear Component
        |
        | importer/manual converter
        v
minimal BEM
        |
        v
find chr_0003_endminf live instance
        |
        v
find Mesh_all/lod0 target Renderer
        |
        v
copy original bones[] / rootBone / bindposes[]
        |
        v
create Unity Mesh from BEM
        |
        v
assign sharedMesh
```

建议第一轮只替换一个容易观察、结构简单且能验证动画的 Component，再扩到全模型：

- C0 Hair：28 bones，weighted，几何规模适中；或
- C5 Body：44 bones，weighted；

C9 虽然最能验证完整 156-bone palette，但自定义几何和 draw ranges 最复杂，不适合作为第一个写入 PoC。

## 9. 下一步

下一步不再需要新的 runtime mapping probe。

应进入 PoC-1：

1. 定义 minimal BEM reader/types（先不冻结最终 container）；
2. 把样本 C0 手工/离线转换为 minimal BEM test data；
3. 增加 Unity Mesh 创建与赋值所需 IL2CPP contract；
4. 在 `chr_0003_endminf` live LOD0 hair Renderer 上替换 `sharedMesh`；
5. 复用原 28 bones/rootBone/bindposes；
6. 首轮保留原 Material；
7. 验证静态几何、动画 skinning 和回滚；
8. 成功后扩展到 C1..C9，C10 走 hide-original；
9. 最后再接正式 EFMI importer 和 Android 同源 runtime。
