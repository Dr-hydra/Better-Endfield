# BEM 1.6：骨骼绑定空间与轴向骨长缩放

本规范定义 BEM 1.6 的文件契约与运行时契约，目标是在不接管骨骼旋转与缩放的前提下，让替换网格摆脱原 Mesh 的绑定空间，从而改变角色比例。

三个状态互相独立：格式支持、运行时接线、游戏内验证。本文描述的是前两者的设计契约；本文中标为 TBD 的数值尚未在我们的环境实测，不得作为验收依据。

本文相对 v3 草案的主要改动：把"只能整链等比"修正为"按边独立取比例"；把绝对位置改为由比例推导；移除未经实测的阈值并改列标定计划。

## 1 数学边界

对每条父→子边 `e = (p, c)`，记 donor 静止局部位置为 `p_c^d`，目标为 `p_c^t`，定义该边的长度比

```
k_e = |p_c^t| / |p_c^d|,   k_e > 0
```

**可行条件**：`p_c^t = k_e · p_c^d`，即子关节沿父骨局部空间的同一射线移动。

在此条件下：

- 骨段 `p`（头部到 `c` 头部）的长度变为 `k_e` 倍，方向不变，因此 `p` 的静止旋转不变；
- `c` 的静止旋转只由它自己的子边决定，同样不变；
- 全骨架静止矩阵的旋转部分与 donor 完全一致，只有平移部分改变，运行时覆盖 `localPosition` 即可闭合。

由此得到两条必须写进工具的结论：

1. **每条边独立取 `k` 是合法的，不要求整链同比例。** 只缩短大腿（`Thigh` 的 `k = 0.85`、`Calf` 的 `k = 1.0`）在数学上成立。早期草案要求"整链等比"是过度保守。
2. **任何使 `p_c` 偏离原射线的改动都会改变 `p` 的静止朝向**，1.6 不做。这类改动需要运行时接管 `localRotation`，留待 1.7。

| 调整 | 1.6 可达性 |
| --- | --- |
| 腿部/手臂任意分段改长（单节或组合） | 可达 |
| 全身轴向等比缩放（身高） | 可达 |
| 骨盆高度（由腿部链推出） | 可达，自动推导 |
| 躯干/颈部改长 | 可达 |
| 关节横向移动（加宽肩、A→T pose、膝内扣、脚外八） | 不可达（改朝向） |
| 骨骼 `localScale` 改头身比 | 禁止 |
| 胖瘦、胸腰臀围、头部网格大小 | 只能建模期烘焙进网格顶点 |

## 2 版本与能力

- 头部 `major = 1`、`minor = 6`。`minor` 有效范围由 0..5 扩为 0..6。
- 必须声明 `required_capabilities: ["skeleton-binding-v1"]`。既有能力声明的写法沿用 `material-overrides-v1` 的形式。
- `format_version` 仅为工程文件提示，写二进制时丢弃；版本判定只看 `minor` 与能力集。
- 旧运行时遇到 `minor = 6` 或未实现 `skeleton-binding-v1` 时**必须拒绝加载并报错**。禁止静默忽略新字段后按 donor bindpose 蒙皮——那会产生错位而不报错。
- 与 1.5 `root_offset` 互斥：同包同时出现且 `root_offset.translation` 非零时，构建期直接报错；运行时遇到则告警并忽略 `root_offset`，不写 visual root 节点。
- 与 1.3 几何形变互斥：同包出现非空 `mesh_deformations` 时构建期报错。理由见第 8 节。
- 沿用既有预算：manifest ≤ 4 MiB、组件数 ≤ 64、单 mesh palette ≤ 256。

## 3 清单字段

`skeleton_retarget` 位于 manifest 根级，按角色给出。`bind_space` 与 `rest_digest` 属于单个组件，因为 bindpose 与 palette 都是部件级的。

```json
{
  "skeleton_retarget": {
    "pelvis_bone": "Bip001_Pelvis",
    "ground": {
      "reference": "Bip001_L_Foot",
      "mode": "match_rest_y",
      "offset": 0.0
    },
    "bones": [
      {"name": "Bip001_L_Thigh", "k": 0.8500},
      {"name": "Bip001_L_Calf",  "k": 0.9000},
      {"name": "Bip001_Spine",   "k": 1.0200},
      {"name": "Bip001_L_Clavicle", "k": 1.0000}
    ]
  },
  "target": {
    "components": [
      {
        "id": 0,
        "mesh_name": "S_actor_example_body_01_lod0",
        "bone_names": ["Bip001_Pelvis", "Bip001_L_Thigh"],
        "bind_space": {
          "mode": "package",
          "count": 142,
          "encoding": "matrix4x4-f32-colmajor",
          "poses": 7
        },
        "rest_digest": {
          "algorithm": "sha256",
          "value": "3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b"
        }
      }
    ]
  }
}
```

字段规则：

| 字段 | 规则 |
| --- | --- |
| `pelvis_bone` | 必须解析到实际骨骼，见第 6 节别名规则 |
| `ground.mode` | `match_rest_y`（默认）或 `none`；`none` 时不推导骨盆高度，由 `offset` 直接给世界 Y 位移 |
| `ground.reference` | 用于对齐接地高度的脚部骨骼名 |
| `ground.offset` | 附加世界 Y 位移，单位米，范围 −0.20..0.20；正值抬高 |
| `bones[].name` | 该骨**骨段**（自身头部到主链子骨头部）的长度比；不是点的绝对位置 |
| `bones[].k` | 0.50..2.00，见第 7 节 |
| `bones[].inherit_from` | 仅分叉节点需要：当父骨有多个主链子骨时，声明派生骨继承哪一条边，见第 6.3 节 |
| `bind_space.mode` | `target`（缺省，等同 1.5 行为）或 `package` |
| `bind_space.count` | 必须等于该组件 `bone_names` 的长度 |
| `bind_space.poses` | payload ID，长度为 `count × 64` 字节 |
| `rest_digest.value` | 64 位小写十六进制，无 `sha256:` 前缀 |

**绝对位置不再入清单。** 目标静止局部位置、各骨 `bindpose`、骨盆下移量全部由 `k` 与 donor 骨架推导，工具生成后写入 payload。清单只保留比例，避免出现"两处写了同一个数、互相对不上"的情况。

## 4 二进制布局

`bind_space.poses` 指向一个独立 payload，长度必须严格等于 `count × 64`。

| 偏移 | 类型 | 语义 |
| --- | --- | --- |
| `i × 64 + 0..63` | `float32[16]` LE | 第 `i` 根 palette 骨的 `worldToLocal` 矩阵 |

矩阵按 `UnityEngine.Matrix4x4` 的**内存布局**序列化，即列优先：

```
索引 0..15 依次为 m00, m10, m20, m30,  m01, m11, m21, m31,  m02, m12, m22, m32,  m03, m13, m23, m33
```

连续 4 个 float 构成一列。**禁止按教科书行主序（`m00, m01, m02, m03…`）写出**——运行时的 `Matrix4x4Raw` 是内存直读（`native/modules/custom_model/module.cpp:111-115`），行主序会被原样读成转置矩阵，蒙皮全错且不触发任何校验失败。

按同一索引的校验条件：

1. 16 个分量全部有限。
2. 平移分量在索引 `12, 13, 14`。
3. 齐次底行在索引 `3, 7, 11, 15`，要求 `|m[3]|,|m[7]|,|m[11]| ≤ 1e-5`、`|m[15] − 1| ≤ 1e-5`。
4. 取索引 `{0,1,2}`、`{4,5,6}`、`{8,9,10}` 组成 3×3，行列式 `> 1e-6`。

`mode = target` 时该 payload 必须不存在，`poses` 字段必须缺省。

## 5 摘要

`rest_digest` 用于发现游戏更新后的 donor 骨架漂移（`aurora_legacy_compat.h` 已为某个版本的 87 根骨硬编码过整张 bindpose 表，说明漂移真实发生过）。它必须对 **donor 骨架**求摘要，不得对包内自指字段求。

```
digest = SHA-256(
  palette_len        : uint32 LE
  path[0..n-1]       : UTF-8 + 0x00，节点相对骨架根的规范路径
  bindposes[0..n-1]  : 每根 16 × float32 LE，元素先 round 到 1e-5，按第 4 节内存布局
)
```

- 计算对象是 probe 导出的 donor 骨架（`native_probe.inl:167-176`，需开启 `g_probe.sweep`；导出要求 `pose_count == bone_count` 且 ≤ 1024）。
- probe 输出的 `bindposes` 已经是内存序，`parse_native_models.py:89-95` 不做转置。**Python 侧不得再转置一次。**
- 运行时在替换前用当前游戏原 Mesh 的 bindposes 重算并比对；不一致立即拒绝加载，日志 `donor rest digest mismatch (package=… runtime=…)`。
- 哈希实现复用 `native/modules/custom_model/model_content_identity.h` 的 `ModelContentSha256`。

## 6 工具链

### 6.1 donor rest 反解

从 donor bindposes 求逆得到静止世界矩阵，再由父链推出静止局部矩阵，取其第 4 列（索引 12/13/14）作为 `p^d`。父链由节点规范路径决定。

### 6.2 目标骨架与 payload 生成

1. 读入创作者导出的目标骨架，按同一父链反解出 `p^t`。
2. 对每条父→子边求 `k_e = |p^t| / |p^d|`；与清单中 `bones[]` 的 `k` 逐边比对，偏差 > 1e-4 报错（清单与网格不一致时不能猜）。
3. 按 `ground` 规则推导骨盆世界 Y 下移量，转回父空间后写入目标局部位置。
4. 依目标静止世界矩阵求逆，生成 `bind_space.poses` payload。
5. 生成 `rest_digest`。

### 6.3 派生骨与分叉

终末地骨架的每个肢段都挂着派生骨族（`*_Twist`、`*_Twist1`、`*_bend_jnt`、`*_corrective_ty_*`、`*_corrective_tz_*`、`*_ty_*`、`*_tz_*`）。这些骨的局部偏移必须跟随其所属骨段的比例一起缩放，否则膝肘处的 corrective 会停在旧位置，蒙皮立刻挤压。

规则：

- 派生骨缺省继承**父骨骨段那条边**的 `k`；`p^t = k · p^d`。
- 父骨是分叉节点（例如 `Bip001_Spine` 同时有 `Neck` 与左右 `Clavicle`）时，派生骨无法自动判断归属，必须在 `bones[]` 中显式给出 `inherit_from`，指向具体的主链子骨名；缺省报错。
- 边长 `|p^d| ≤ 1e-3 m` 的派生骨允许不缩放（视为贴在关节原点）。

### 6.4 旋转不变性校验

主骨与派生骨使用同一阈值 `θ ≤ 0.5°`（TBD，见第 10 节），计算方法为两者静止局部四元数夹角

```
θ = 2·arccos(min(1, |<q_donor, q_target>|))
```

超出即拒绝构建并报出骨骼名。**不要给派生骨更大的容差**：按第 1 节的等比条件，派生骨的理论偏差同样是 0°，给宽容差只会放过真实的朝向错误，而派生骨正是穿模的源头。若实测超限，应检查导出链路（bone roll、Apply Transform、骨骼朝向约定），而不是放宽阈值。

### 6.5 比例合理性校验

`k` 必须落在 `0.50..2.00`。此外若入表边的 `k` 全部相同且接近同一常数、且覆盖全骨架 80% 以上的边，判定为坐标系或单位错误的典型特征，输出告警并要求显式确认（`--accept-uniform-scale`）。

不再使用"差异骨数占 palette 百分比"作为拒绝条件：该比例取决于部件包含多少派生骨，与是否有错无关。

## 7 运行时

### 7.1 安装期

- `PreparePalette`（`module.cpp:2265-2302`）当前从 donor Mesh 取 poses（`:2271-2273`）。当组件 `bind_space.mode == "package"` 时改为读取包内 payload，其余逻辑不变。
- 现有的 `prepared_bindposes` 形参通道（`module.cpp:1378`、`:1416`）已能把外部矩阵传入网格构建，无需另建路径。
- `SameMeshSpace`（`:2245`、`:2277`）与"不推断 geometry-space 转换"（`:2252-2254`）是既有不变式。放宽条件必须写成合取：`minor == 6` 且声明 `skeleton-binding-v1` 且 `mode == "package"` 且 `count == palette_len` 且矩阵校验通过 且 `rest_digest` 匹配。任一不满足保持原拒绝逻辑。
- `mode == "package"` 时禁止跨部件借用骨骼（palette 中每一项必须来自本组件自身的 donor）。跨部件 donor 不共享绑定空间。
- donor 静止局部位置在安装期由 donor bindposes 反解得到，**不从当前帧的 `localPosition` 读取**——安装可能发生在动画中途，读活动值会拿到错误基线。

### 7.2 每帧

只对 `skeleton_retarget.bones` 中出现的骨执行，使用已绑定的 `get/set_localPosition`（`module.cpp:454-455`），不新增反射契约。

| 模式 | 适用 | 计算 | 每帧调用 |
| --- | --- | --- | --- |
| 骨盆 | 仅 `pelvis_bone` | `rest_p + (cur − donor_p) · k_len + ground_shift` | 1 读 + 1 写 |
| 覆盖 | 无平移动画的骨 | 目标常量 `k·p^d`；读出后 `|cur − target|∞ < 1e-5` 则跳过写 | 1 读 + 0..1 写 |
| 缩放 | 存在平移动画的骨（脊柱、锁骨、部分派生骨） | `cur · aff` | 1 读 + 1 写 |

`ReplaceRestPos` 的副作用必须写入文档与 UI 提示：若某条 clip 给该骨写了平移曲线，其平移会被常量覆盖抹平。

每帧写操作**不得**复用安装期的 `SetVisualPosition`（`module.cpp:2839-2846`，含回读与容差校验），那条路径用于一次性写入。

### 7.3 协调器

同一根骨（骨盆、脊柱、大腿）同时出现在躯干、服装、头发等多个组件的 palette 中，必须去重后再写。

- 协调器以骨架根 `Transform` 的实例 ID 为键，存放在 custom_model 模块内的弱映射中，引用计数归零即析构。这是模块级共享状态，不是"各部件共同持有"——需要显式承认，并给出清理时机（角色销毁、场景切换、热切换、切回 Original）。
- 以 `palette_len` 最大的组件为权威来源；同一 `Transform` 在不同组件中推出的 `p^t` 偏差 > 1e-5 即报错并触发该角色全部组件的原子回滚。
- 每帧对每个协调器执行**一次** `IsNativeObjectAlive(root)`；根失效即整体停用并清理。不要按骨判活（数十次/帧），也不要完全不判——游戏侧直接销毁 actor 时，raw `Transform*` 会变成悬垂指针，写入即崩。

### 7.4 挂载点

`CanvasUpdateRegistry.PerformUpdate` 现在由 `native/modules/model/module.cpp:4678-4695` 持有，用于登录界面色带主题。`model_runtime_notes.md` 的 “Root cause B” 记载过 custom_model 与 model 争抢同一 hook target 导致 `native hook target already owned` 的事故，因此：

1. 必须通过 Host 的链式 hook 注册为后置回调，禁止独占 Inline Hook；链式失败立即记录 `[custom_model] failed to chain PerformUpdate hook, switching to frame-pump fallback` 并降级，不阻塞 model 模块。
2. `Canvas.willRenderCanvases` 在没有活动 Canvas 的场景不保证每帧触发。协调器记录 `last_updated_frame`，在现有基于 `Time.frameCount` 的泵中补一次更新，保证每帧恰好执行一次且不重入。
3. 该挂载点在多角色战斗与过场下的表现属于**未实测项**，不得在文档中写成"已验证稳定"。

### 7.5 滑条与 w = 0

- 安装期使用 `prepared_bindposes`；运行期权重变化只能走 `mesh.set_bindposes`（`module.cpp:413-414`），`prepared_bindposes` 在网格建好之后不再参与。
- 提交节流：`|Δw| ≥ 1e-3` 且距上次提交至少一帧，或拖拽结束时提交一次。
- `w = 0` 必须与原版位相同：写回安装期缓存的 donor bindposes，并把已改骨骼恢复为 donor 静止局部位置，随后置协调器 `active = false`，此后每帧 `set_localPosition` 计数为 0。

### 7.6 双骨架

角色同时有 `world_resource` 与 `ui_resource` 两套骨架实例（`catalog/chr_*.json` 顶层键）。两者各自解析骨骼名（先原名、再 `bone_name_aliases`），任一侧解析失败即整包拒绝并报出是哪一侧、哪根骨。两侧各持有独立协调器；任一侧构建失败则整角色回滚，不允许出现"场景变了、详情页没变"。

## 8 与 BEM 1.3 形变的互斥

1.3 的 `mesh_deformations` 是"原始位置 + Σ 参数增量"，增量在 donor bind 空间内累加。1.6 改变了 bind 空间，同一组增量在新空间里的语义不明确（按 donor 尺度加会被拉扁，按目标尺度加则与原包的作者预期不符）。

1.6 的处理是**拒绝共存**：同包出现非空 `mesh_deformations` 即构建期报错，要求作者把形态烘焙进网格。只驱动材质或非几何项的 `parameters` 不受影响。复合语义留到后续版本，届时需要为每项增量声明其所属空间。

## 9 验收

阈值列中的 TBD 必须在第 10 节的标定完成后回填。

| 编号 | 判定 |
| --- | --- |
| AC-1 | `w = 0` 时，各组件克隆 Mesh 的 bindposes 与 donor 逐元素位相等；连续 60 帧内本管线发起的 `set_localPosition` 计数为 0 |
| AC-2 | `w = 0.5` 与 `w = 1.0` 静止态：从引擎取每根骨的 `localToWorldMatrix`（`module.cpp:446` 已绑定），与内存中 `B` 相乘，`‖W_engine·B − I‖_F / max(1, ‖W_engine‖_F) < 1e-3`。不得用工具自己算的 W 与 B 互乘——那是自证 |
| AC-3A | 合法包必须通过：真实角色、含派生骨的合法 `k` 表、列优先 payload、匹配的 `rest_digest`，双端 world/ui 均生效 |
| AC-3B | 非法包必须拒绝：空格骨名；关节偏离射线使 `θ > 阈值`；分叉节点派生骨缺 `inherit_from`；`rest_digest` 不匹配；行主序 payload（索引 3/7/11 非零）；`mode = package` 下跨部件借骨 |
| AC-4 | 三组件角色：`Bip001_Pelvis` 每帧只被写 1 次；world/ui 切换、热切换、切回 Original 无崩溃无残留 |
| AC-5 | 4 角色同屏：实测每帧 get/set 次数与单帧耗时（目标值 TBD）；隐藏全部 Canvas 时自泵接管，无缝 |

## 10 标定计划（TBD）

以下数值在本文中一律视为占位，标定后回填并注明样本。

| 项 | 占位 | 标定方法 |
| --- | --- | --- |
| 旋转容差 `θ_max` | 0.5° | Blender 对真实 `Bip001_*` 骨架做分段长度改动，导出 FBX，用 probe dump 反解，统计主骨与派生骨的 `θ` 分布；取最大值上浮 50% |
| `k` 合法区间 | 0.50..2.00 | 用同一批样本统计实际使用范围，确认边界不误杀 |
| 每帧耗时 | Windows < 0.15 ms / Android ARM64 < 0.35 ms | 4 角色同屏战斗 60 秒取 P95 |
| 单部件派生骨覆盖率 | 无 | 统计一个真实部件中"入表骨 / palette_len"，用于在日志里提示"这个包改了多少" |

标定脚本与原始数据要入 `docs/custom_model/research/`，不接受口头结论。

## 11 不支持

1. 改变骨骼静止朝向（关节偏离射线、A→T pose、加宽肩、脚外八）。
2. 任何骨骼 `localScale ≠ 1`。
3. `mode = package` 下的跨部件借骨与 `bone_remap`。
4. 布料碰撞体（`ClothField`）半径/高度的自动缩放。
5. 包内多套体型预设。
6. 与 `mesh_deformations` 共存。
7. Android 上的逐帧写骨通道：本规范给出契约，但 Android 侧的挂载点与性能尚未验证，实施时单列任务。

## 12 实施阶段

**Phase 1（可立即开始，不依赖 TBD）**

1. `docs/custom_model/BEM_FORMAT_SPEC.md`：`minor` 扩到 0..6，新增能力与 `bind_space` / `rest_digest` / `skeleton_retarget` 字段。
2. `docs/custom_model/BEM_RUNTIME_COMPATIBILITY.md`：更新 bindpose 与 mesh space 两行，写明放宽的合取条件。
3. `parse_native_models.py`：注明 bindposes 已是内存序、不得转置；输出 rest 反解结果到 `catalog/rest_poses/`。
4. `BemConverter`：`k` 提取、逐边比对、派生骨展开、`inherit_from` 校验、旋转校验、payload 生成、`rest_digest`（复用 `ModelContentSha256` 的等价实现或调用 sidecar）。

**Phase 2（前置 Spike，可与 Phase 1 并行）**

只做两根骨（`Bip001_Pelvis`、`Bip001_L_Calf`）、固定 `w = 1`、硬编码列优先矩阵。验证三件事：`W·B = I` 是否闭合；链式 hook 是否与 model 模块冲突；`w = 0` 是否零写入。这个 spike 不依赖任何 TBD，同时产出第 10 节的第一批数据。

**Phase 3（Spike 通过后）**

协调器、去重与权威规则、滑条与节流、双骨架回滚、UI 接入、悬垂指针清理。

## 13 评审项闭环

| 评审项 | 落点 |
| --- | --- |
| 列优先字节序 | §4 |
| 摘要对 donor 求、复用既有哈希 | §5 |
| hook 所有权与降级 | §7.4 |
| 后代派生骨覆盖 | §6.3 |
| 多部件同骨去重 | §7.3 |
| 双真相源 | 已消除：清单只存 `k`，位置全部推导 |
| 滑条走 `mesh.set_bindposes` | §7.5 |
| 阈值标定 | §10 |
| AC-2 自证 | §9 AC-2 |
| 1.3 形变复合 | §8 |
| 悬垂 `Transform*` | §7.3 |
| 整链等比约束过严 | §1 |
