# 终末地模型替换 LOD 方案现状与跨 LOD 骨骼补齐记录（2026-09-15）

状态：**当前采用原生渲染管线 LOD0 锁定；跨 LOD 骨骼补齐作为已有辅助实现保留**

分支：`dev/custom-model-design`

涉及模块：`BetterEndfield.CustomModel`

涉及核心代码：
- `native/modules/custom_model/module_npc_lod_lock.inc`
- `native/modules/custom_model/module_ai_shadow_coverage.inc`
- `native/modules/custom_model/module_poc2_part_02.inc`
- `native/modules/custom_model/module_early_delivery_c9.inc`

---

## 1. 背景与历史试错复盘

在将第三方高精度自定义模型（以休闲服女管理员 `endmin in casualwear` 为基准测试样本）引入《明日方舟：终末地》运行时的过程中，随着视点距离拉远（角色成为 AI 队友跟随、主控跑开、或镜头拉远），历经了多次方案迭代与失败教训：

### 1.1 试错历史与现象演变

| 阶段 | 采取策略 | 实际现象 | 失败原因复盘 |
| :--- | :--- | :--- | :--- |
| **初期** | 仅替换 LOD0 网格，未处理低 LOD | 远距离角色躯干变回原版黑色战术大衣，手臂与头部仍为休闲服；大衣扣带与胸前发光信标浮空穿模 | 引擎在拉远后将渲染器切至 `_lod1/_lod2/_lod3`，未替换的低模网格与原版材质直接显示出来 |
| **中期** | 尝试向低 LOD 赋予 LOD0 自定义网格 | “变成奇怪的模型”：模型剧烈拉扯破面，胸前发光信标仍然亮起 | 1. 低 LOD 骨骼数严重精简（如 `cloth_01_lod2` 仅 136 骨，`lod3` 仅 69 骨），而自定义网格绑了 156 骨，顶点蒙皮读取越界权重矩阵；<br>2. 未赋予定制材质，沿用了黑大衣原贴图；<br>3. 原版大衣散件与发光信标被引擎重新点亮 |
| **后期 A** | 后台循环暴力关闭所有非 `_lod0` 渲染器 | “不行，人没了”：距离稍远角色全身隐形消失 | 引擎在拉远后自动关停 LOD0 渲染器，模块又关停了非 LOD0 渲染器，导致全身没有任何启用的渲染器 |
| **后期 B** | Hook 强制锁定 LOD0（劫持 `NPCCrowdLOD` / `NPCAvatar` / `LODGroup`） | 游戏在 F12 替换时直接崩溃闪退（`unityplayer.dll + 0xBE9DF: 0xC0000005 Access Violation`） | 强行向 Unity 原生对象传递非法指针解包，且破坏了终末地人群与视锥调度管线 |

### 1.2 当前确认的方案（2026-09-15 修订）

当前采用 `module_npc_lod_lock.inc` 中的 **HG 原生渲染管线 LOD0 锁定 + NPC 距离与裁剪参数调整**。这是用户确认保留的方案，也是当前工作树的调用路径。

```text
PumpAiShadowCoverage()
  └─ PumpNpcLodLock()
       ├─ g_replacement_active：ApplyLockedCrowdLODs()
       │    ├─ HGRenderPipeline.EnableForceLOD0()
       │    ├─ QualitySettings.maximumLODLevel = 0
       │    └─ NPCCrowdLODSetting 静态参数调整
       └─ 替换停用：ResumeLockedCrowdLODs()
```

早期逐实例拦截 `NPCAvatar` / `NPCCrowdLOD`、猜测指针并调用 `LODGroup.ForceLOD` 的尝试见上表。它与当前通过 `HGRenderPipeline` 接口控制的实现不同，早期故障不能作为删除当前方案的依据。

本文原先“禁止强制 LOD0”“全面清空锁定模块”“原生 LOD 完全自由运行”的要求已撤销。后续维护保留上述调用链。跨 LOD 骨骼补齐、材质同步和散件治理已有代码，作为辅助覆盖能力保留；其存在不表示主方案已经改成自由切换 LOD。

本文核对范围是分支工作树源码。尚未核实游戏进程实际加载的 DLL，也未在本次文档修订中执行构建、部署或游戏验证。

---

## 2. 终末地角色 LOD 骨骼与渲染器体系剖析

以女管理员（`chr_0003_endminf`）为例，通过对运行时 GameObject 树及网格资产的逐层反编译与内存扫描，梳理出完整的骨骼分布与构件矩阵：

### 2.1 各 LOD 级别的骨骼数事实（Ground Truth）

终末地角色的骨骼 Transform 层次结构存在于 `chr_0003_endminf_postmodel(Clone)/Root/...` 节点树下。该层级树始终常驻内存，拥有完整的全部骨骼节点（>= 156 根）。各个部件 `SkinnedMeshRenderer.bones` 数组的大小如下：

| 部件类型 | 部件名标识 | LOD0 骨骼数 | LOD1 骨骼数 | LOD2 骨骼数 | LOD3 骨骼数 | 差异说明 |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **上衣/外套** | `cloth_01` | **156** | **156** | **136** (-20) | **69** (-87) | LOD2 精简了布料次要飘带骨，LOD3 仅留核心躯干 |
| **身体/四肢** | `body_01` | **44** | **44** | **44** (一致) | **6** (-38) | LOD2 骨骼与 LOD0 完全相同；LOD3 大幅精简 |
| **鞋子/下装** | `cloth_04` | **14** | **14** | **14** (一致) | **8** (-6) | LOD2 骨骼与 LOD0 完全相同；LOD3 轻微精简 |
| **头发** | `hair_01` | **28** | **28** | **21** (-7) | **1** (-27) | LOD2 精简部分发丝骨，LOD3 仅留头部主骨 |
| **脸部** | `face_01` | **53** | **53** | **5** (-48) | **1** (-52) | LOD2 剔除大部分面部表情细分骨 |
| **眉毛** | `eyebrow_01` | **42** | **42** | **3** (-39) | **1** (-41) | LOD2 仅留主控制骨 |
| **眼影** | `eyeshadow_01` | **21** | **21** | 0 / 剔除 | 0 / 剔除 | LOD2/3 通常不单独渲染眼影细节 |
| **眼睛** | `iris_01` | **6** | **6** | **1** (-5) | **1** (-5) | LOD2/3 仅留主眼球骨 |
| **大衣发光信标** | `vfxpart_01` | **5** | **5** | **5** | **5** | 原版科技发光组件（Mod 剔除物） |
| **大衣绑带扣件** | `cloth_02` | **29** | **29** | **20** | **5** | 原版大衣附属物（Mod 剔除物） |
| **大衣后摆披风** | `cloth_03` | **1** | **1** | **1** | 0 | 原版大衣附属物（Mod 剔除物） |
| **大衣立领领饰** | `cloth_05` | **1** | **1** | **1** | 0 | 原版大衣附属物（Mod 剔除物） |

### 2.2 核心发现

1. **LOD1 骨骼与 LOD0 完全 100% 相同**：
   所有部件在 LOD0 与 LOD1 之间的骨骼数量、顺序、Transform 引用完全一致！这意味着 **LOD1 层级可直接无缝赋予 LOD0 的自定义网格与材质，完全不需要任何骨骼调整**。
2. **LOD2 的 `body_01` 与 `cloth_04` 骨骼完全相同**：
   身体（44 骨）和鞋子（14 骨）在 LOD2 上也与 LOD0 完全相同，可直接无缝替换。
3. **骨骼缺口集中于 LOD2 的 `cloth_01`（少 20 骨）以及 LOD3 全部件**。

---

## 3. 动态骨骼借调与升配技术（Bone Palette Elevation）

### 3.1 Unity 蒙皮网格渲染器原生契约

在 Unity 原生 C++ 核心与 IL2CPP 接口中，`UnityEngine.SkinnedMeshRenderer` 具备以下关键特性：
- `Transform[] bones { get; set; }`：渲染器蒙皮所绑定的骨骼变换数组。
- `Transform rootBone { get; set; }`：渲染器的根骨骼变换节点。
- `Mesh sharedMesh { get; set; }`：渲染器引用的网格资产。
- `Material[] sharedMaterials { get; set; }`：渲染器引用的材质数组。

**关键结论**：
Unity 并没有在原生层将一个特定的 `SkinnedMeshRenderer` 实例与固定的骨骼数组长度锁死。
只要赋予渲染器的 `Mesh` 其顶点蒙皮权重索引（`BlendIndices`）所指向的下标不超过 `bones.Length`，且 `bones[i]` 指向合法的 `Transform` 节点，Unity 顶点着色器蒙皮计算就能完全合法合规地进行变换矩阵混合（Matrix Blending），**绝不崩溃、绝不越界、绝无拉扯**。

### 3.2 动态骨骼升配（Elevation）流程

在角色实例中，高精度骨骼树与所有部件的 LOD0 渲染器始终并存。当处理低 LOD（如 `cloth_01_lod2` 或 `cloth_01_lod3`）时：

```text
       角色同一 GameObject 树: chr_0003_endminf_postmodel(Clone)
                            │
        ┌───────────────────┴───────────────────┐
        ▼                                       ▼
cloth_01_lod0 (LOD0 渲染器)             cloth_01_lod2 (低 LOD 渲染器)
  ├─ bones: 156 根完整骨骼 Transform        ├─ 原 bones: 136 根精简骨骼
  └─ rootBone: Pelvis 根节点                └─ 原 rootBone: Pelvis
        │                                       │
        │ 1. 提取 156 根骨骼与 rootBone         │ 2. 暂存原 136 根骨骼（用于 F11 还原）
        └─────────────────► 借调升配 ───────────┘
                                                │
                                                ▼
                                    cloth_01_lod2 升配完成
                                      ├─ bones: 156 根完整骨骼 Transform
                                      ├─ rootBone: Pelvis
                                      ├─ sharedMesh: 自定义休闲服网格
                                      └─ sharedMaterials: 自定义休闲服材质贴图
```

### 3.3 借调查找器（`AiCoverageFindDonorBones`）设计规范

为确保线程安全与跨实例隔离，借调供体必须满足：
1. **严格限定于同一角色实例（Same Avatar / Same PostModel Clone）**：禁止跨角色的渲染器互相借调，防止矩阵坐标系发生世界空间错乱；
2. **同名语义部件优先匹配**：例如 `cloth_01_lod2` 优先向同实体的 `cloth_01_lod0` 借调骨骼；若找不到则向 `cloth_01_lod1` 借调；
3. **暂存原始状态**：在重写 `bones` 与 `rootBone` 之前，必须获取原骨骼数组指针并加入无损回滚记录（`ReplacedComponent`），确保 F11 按下时恢复原生 136 骨结构。

---

## 4. 全 LOD 层级附属散件与发光部件治理

### 4.1 散件显隐穿模的机理

休闲服 Mod 本质上是将原版复杂的长风衣大衣重构为了简约的白 T 恤与短外套：
- 原版大衣由多个分散的 SkinnedMeshRenderer 拼装而成：
  - `cloth_01`：大衣主体；
  - `cloth_02`：战术背带与扣带（29 骨）；
  - `cloth_03`：大衣后背披风（1 骨）；
  - `cloth_05`：防风立领饰件（1 骨）；
  - `vfxpart_01`：科技发光信标与蓄电池部件（5 骨）。
- 休闲服 Mod 仅提供了 `cloth_01` 的新网格，并将 `cloth_02`、`cloth_03`、`cloth_05` 标记为 `hidden_by_mod`（完全隐藏）。
- 当距离拉远，引擎原生的 LOD 调度系统触发 `NPCAvatar.SetLodVisible(level)` 时，引擎会根据自身内部的部件可见性表，**自动把 `cloth_02_lod1/2/3`、`cloth_03_lod1/2/3`、`cloth_05_lod1/2/3` 与 `vfxpart_01_lod1/2/3` 强制设为 `enabled = true`！**
- 这导致原本在近距离隐藏的战术扣带、披风和发光信标，在拉远后突然从 T 恤表面钻出穿模。

### 4.2 治理规则

1. **原版大衣散件治理**：
   在自定义模型激活状态下，维护循环必须持续巡检并确保：
   所有包含 `cloth_02`、`cloth_03`、`cloth_05` 的渲染器（不限 LOD，覆盖 `_lod0` 至 `_lod3`），一律维持 `enabled = false`。
2. **发光部件（`vfxpart`）治理**：
   所有名称包含 `vfxpart` 的渲染器（覆盖 `_lod0`、`_lod1`、`_lod2`、`_lod3` 及 `shadowProxy`）：
   - 在首次隐藏时记录原始 `enabled` 状态至 `g_ai_coverage_hidden_vfxparts`；
   - 持续维持 `enabled = false`；
   - F11 回滚时统一恢复。
3. **杜绝一刀切禁用**：
   彻底删除历史版本中 `if (name.find("_lod0") == std::string::npos) SetRendererEnabled(false);` 的暴力逻辑，让有替换网格的低 LOD（`body_01_lod*`、`cloth_01_lod*`、`hair_01_lod*`、`cloth_04_lod*` 等）能够正常被引擎点亮渲染。

---

## 5. 材质同步与继承（Material Propagation）

在之前的代码路径中，`module_poc2_part_02.inc` 创建 `ReplacedComponent` 记录时，仅填充了 `custom_mesh`，而将 `custom_material` 留为了 `nullptr`。
这导致低 LOD 渲染器即便挂载了自定义网格，执行 `ApplyRendererMaterials` 时却无自定义材质可用，只能保留原版黑色大衣的皮革材质，渲染出极其恐怖扭曲的黑皮纹理。

### 规范要求
1. 在初次从 Mod Bundle 提取材质或复制材质时，必须将分配给 `cloth_01_lod0` 的自定义材质指针完整保存在 `record.custom_material` 中。
2. 在对 `_lod1`、`_lod2`、`_lod3` 进行网格替换时，必须同步调用 `ApplyRendererMaterials(renderer, custom_material)`，确保全层级贴图与着色器统一。

---

## 6. 当前原生管线锁定实现

`module_npc_lod_lock.inc` 保留并参与运行时调用：

1. `ResolveNpcLodLockBindings()` 解析 `NPCCrowdLODSetting` 并取得静态字段区。
2. `InstallNpcLodLockHooks()` 当前只记录初始化日志并返回成功；函数名沿用历史命名，内部没有安装逐实例 Detour。
3. `ApplyLockedCrowdLODs()` 获取当前管线并调用 `EnableForceLOD0()`，设置 `QualitySettings.maximumLODLevel = 0`。
4. NPC 静态字段写入距离参数 `1000.0f / 1000.0f`、最大渲染数量 `100`、裁剪索引 `0`、物理裁剪开关 `false`。这些是全局参数，不是仅对女管理员实例生效。
5. `ResumeLockedCrowdLODs()` 调用 `DisableForceLOD0()`，并通过保存的值回写 NPC 参数；替换停用和模块卸载路径会调用该恢复逻辑。

### 恢复范围的源码现状

当前代码没有保存或恢复 `QualitySettings.maximumLODLevel` 的原值，也没有保存启用前的管线强制 LOD0 状态。NPC 字段读取只保存 `+0x0/+0x4` 的距离值，写入与恢复同时覆盖 `+0x0/+0x4` 和 `+0x8/+0xc`。因此不能把该实现描述为所有全局参数均已逐项无损恢复；本次仅校正文档，不修改代码。

---

## 7. F11 逆向无损还原契约（Reversible Rollback Contract）

以下是渲染器恢复的设计契约，已有原骨骼与根骨骼记录及恢复代码；不代表已完成游戏内无损还原验收。全局 LOD 参数的恢复范围见第 6 节。

```cpp
struct ReplacedComponent {
    // 渲染器句柄
    void* renderer = nullptr;
    // 原始网格与自定义网格
    void* original_mesh = nullptr;
    void* custom_mesh = nullptr;
    // 原始材质与自定义材质
    void* original_material = nullptr;
    void* custom_material = nullptr;
    // 原始骨骼与根骨骼（骨骼升配前保存）
    void* original_bones = nullptr;
    void* original_root_bone = nullptr;
    // 原始显隐状态
    bool original_enabled = true;
};
```

按下 F11 时依次执行：
1. `skinned.set_shared_mesh(original_mesh)`
2. `skinned.set_bones(original_bones)`
3. `skinned.set_root_bone(original_root_bone)`
4. `ApplyRendererMaterials(renderer, original_material)`
5. `SetRendererEnabled(renderer, original_enabled)`
6. 恢复所有被隐藏的发光信标与大衣散件。

---

## 8. 源码现状与验证状态

- [x] 当前调用链保留 `PumpNpcLodLock()` 与原生渲染管线 LOD0 锁定。
- [x] 已有 `original_bones`、`original_root_bone` 及对应句柄和恢复代码。
- [x] 常规替换与早期交付路径已有自定义材质记录。
- [x] `AiCoverageFindDonorBones()`、骨骼补齐、跨 LOD 材质同步与散件治理代码已接入辅助覆盖路径。
- [ ] 核实游戏当前加载的 DLL 与本工作树源码版本对应关系。
- [ ] 记录当前版本的构建、部署及游戏内远近距离表现。
- [ ] 验证 F11 与卸载时渲染器和全局 LOD 参数的实际恢复结果。

勾选项表示源码中存在对应实现，不表示已提交、已部署或通过游戏验证。当前方案说明同时见 [LOD 锁定与 VFX 调研报告](CUSTOM_MODEL_LOD_LOCK_AND_VFX_FINDINGS_20260914.md)。
