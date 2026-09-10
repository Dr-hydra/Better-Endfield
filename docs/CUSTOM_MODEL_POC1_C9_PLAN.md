# BetterEndfield.CustomModel PoC-1：Endmin Casualwear 衣物优先

状态：当前 PoC-1 实现计划  
日期：2026-09-10  
目标：`chr_0003_endminf` / Endministrator (F)  
样本：`Endmin Casualwear`

> 本文根据样本实际视觉改动修正 PoC-1 目标。它取代 `CUSTOM_MODEL_RUNTIME_VALIDATION_ENDMIN_20260910.md` 第 8、9 节中“先用 C0 Hair”的旧建议；运行时映射与 bone-index 结论不变。

## 1. 为什么不再用 C0 Hair 做首个视觉 PoC

C0 虽然是一个结构较简单的 weighted-skin Renderer，适合纯技术验证，但这个 MOD 的头发基本保持原角色外观，首轮即使成功也不容易肉眼判断完整替换是否正确。

样本的主要视觉变化集中在衣物与面罩：

- C9 是主衣物 Renderer；
- C2/C6 是 Mask 几何，受同一个 toggle 控制；
- C10 在该 MOD 中没有自定义几何，原 Component 被隐藏；
- C3 还包含 Hair clip / Watch 两个自定义 draw range，但不是首轮核心。

因此 PoC-1 应优先复现“休闲服 + 默认无面罩”的可观察结果。

## 2. 首个真实替换目标：C9

运行时已经确认：

```text
EFMI Component 9
source hash        = 9cd919fa
originalIndexCount = 101994
runtime Renderer   = S_actor_endminf_cloth_01_lod0
runtime vertex     = 27314
runtime subMesh    = 1
runtime bones      = 156
runtime bindposes  = 156
rootBone           = Bip001_Pelvis
```

EFMI 自定义 C9：

```text
custom vertex count = 12873
custom index count  = 59073
skin                = weighted
max blend index     = 155
```

这正好覆盖我们最需要验证的完整路径：

```text
EFMI VB0/VB1/VB2/IB
  -> BEM
  -> Unity Mesh
  -> original 156-slot bones[]
  -> original rootBone
  -> original 156 bindposes
  -> assign sharedMesh
```

C9 有 7 个 draw range：

```text
Clothes 01       10485 @ 0
Clothes 02        2424 @ 10485
Clothes 03        5208 @ 12909
Shoes             9564 @ 18117
Thighhighs       28080 @ 27681
Watch             1437 @ 55761
Component9.005    1875 @ 57198
```

这些 range 当前全部默认可见且不受本样本的 Mask toggle 控制。因此 PoC-1 可以保留 draw-range 元数据，但在 runtime 临时把七段按原顺序合并为一个 index list / 一个 Unity submesh。这样与目标 C9 原 Renderer 的单 submesh、单 material 结构最接近，也最适合先验证几何和 skinning。

## 3. 默认无面罩必须一起复现

样本 `mod.ini`：

```text
global persist $swapvar_mask = -1

[CommandListProcessToggles]
$draw_component_2_mask = ($swapvar_mask == 0)
$draw_component_6_mask = ($swapvar_mask == 0)
```

因此默认 `-1` 状态下 C2/C6 的自定义 Mask 都不绘制；EFMI 同时接管原 Component draw，所以视觉结果是无面罩。

PoC-1 不需要先实现 toggle VM，但需要支持初始可见性：

```text
C2: replacement geometry exists, defaultVisible=false, hide original
C6: replacement geometry exists, defaultVisible=false, hide original
```

后续实现常见 toggle 时，再把两者绑定到同一个 BEM `visibilityGroup = mask`。

## 4. C10 作为 hide-original 验证

C10：

```text
runtime Renderer   = S_actor_endminf_cloth_02_lod0
originalIndexCount = 2286
bones              = 29
```

样本没有 C10 自定义 VB/IB，并且 `CommandList_Draw_Component10` 不重画原对象，所以它应作为：

```text
replacementMode = hide-original
```

这可以在同一轮 PoC 中顺手验证 BEM/runtime 的“有 Component identity、无 replacement mesh、只隐藏原 Renderer”能力。

## 5. PoC-1 最小视觉目标

第一轮不要求完整复刻全部材质、纹理、LOD 或 Toggle UI。验收目标改为：

1. 找到 live `chr_0003_endminf` 的 LOD0 C9 Renderer；
2. 从样本转换并创建 C9 自定义 Unity Mesh；
3. 直接复用 C9 原 `bones[156]` / `rootBone` / `bindposes[156]`；
4. 暂时保留 C9 原 Material；
5. 将七个默认可见 draw range 合并为一个 submesh；
6. 将 C2/C6 置为默认隐藏，以得到“无面罩”外观；
7. 将 C10 按 `hide-original` 隐藏；
8. 保留所有被修改 Renderer 的原始状态，支持 F8/配置触发回滚；
9. 验证站立、跑动、转身等动画中 C9 衣物没有明显骨骼错位、炸点或静止不跟随；
10. 成功后再扩展 C3/C5 等其余被改 Component、LOD1 和纹理。

## 6. PoC-1 暂不处理

- DDS -> Unity Material texture property 映射；
- BC7 Android 转码；
- LOD1/LOD2 正式切换；
- Mask toggle 热键/UI；
- Merged Skeleton；
- ShapeKey；
- 任意 EFMI/XXMI CommandList VM。

这些都不阻塞 C9 衣物几何 + skinning 的首次真实验证。
