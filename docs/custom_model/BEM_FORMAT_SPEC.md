# BEM 格式规范（1.0–1.3）

BEM（Better Endfield Model）是《终末地》角色模型替换包，扩展名 `.bem`。Windows 与 Android 读取同一个文件。本文合并 1.0–1.3 的全部协议内容；各版本只在下文标注的地方不同。制作流程见 [创作者指南](BEM_CREATOR_GUIDE.md)，游戏内的匹配与限制见 [运行时行为与兼容性](BEM_RUNTIME_COMPATIBILITY.md)。

参考实现：写入与校验 `tools/CustomModel/bem_v1.py`、`bem_v11.py`、`bem_v13.py`；读取 `native/modules/custom_model/bem.cpp`。

## 1. 版本

| 次版本 | 新增内容 | 必需能力 |
| --- | --- | --- |
| 1.0 | 固定外观 `appearances` | `fixed-appearances` |
| 1.1 | 组合选项 `option_groups` + `component_rules`，按条件选择 draw；keep 部件贴图覆盖 | `composable-options`；覆盖贴图时加 `keep-material-textures` |
| 1.2 | 贴图槽 `texture_slots`、按资源区分的骨骼名别名、32 字节非压缩蒙皮，以及更高的资源上限 | 按使用加 `texture-slots`、`resource-bone-aliases` |
| 1.3 | 连续形态参数 `parameters` + 位置增量 `mesh_deformations` | `body-parameters`、`mesh-position-deltas` |

- 写入器按内容选择**能表达该包的最低版本**：不需要新特性的包仍写成旧版本，让旧运行时可读。不能把高版本内容写进低版本头部，读取器会拒绝。
- 读取器按头部版本执行对应的上限和语义；当前运行时接受 1.0–1.3。
- 所有版本都要求能力 `native-materials`、`palette-u8`、`indices-u32`。未知能力、重复能力，或使用了某项功能却没声明对应能力，一律拒绝。只声明不使用是允许的。

## 2. 容器

顺序：40 字节 Header、UTF-8 JSON Manifest、Payload 目录、连续的 Payload 字节。整数一律小端，结构无填充，文件最大 2 GiB，无文件哈希。

| Header 偏移 | 类型 | 字段 |
| --- | --- | --- |
| 0 | byte[8] | magic `42 45 4D 00 50 4B 47 00`（`BEM\0PKG\0`） |
| 8 | uint16 | major = 1 |
| 10 | uint16 | minor = 0..3 |
| 12 | uint32 | header_size = 40 |
| 16 | uint64 | file_size，等于实际文件长度 |
| 24 | uint64 | manifest_size，1 字节..4 MiB |
| 32 | uint32 | payload_count |
| 36 | uint32 | flags = 0 |

Payload 目录每项 32 字节，数组下标就是 Payload ID：

| 项内偏移 | 类型 | 字段 |
| --- | --- | --- |
| 0 | uint32 | codec：0 raw，1 Zstd |
| 4 | uint32 | reserved = 0 |
| 8 | uint64 | offset（文件绝对偏移） |
| 16 | uint64 | stored_size |
| 24 | uint64 | decoded_size |

- 每个 Payload 1 字节..512 MiB；raw 的两个长度相等。
- 目录按偏移排列：第一块紧接目录，后续块紧接前一块，不允许间隙、重叠、越界或尾随字节。
- Zstd 必须是独立单帧、无字典、带准确内容长度。压缩等级不影响兼容性；压缩无收益时写 raw。
- 字节相同的数据共享同一个 Payload ID。

分发时可以把一个或多个 `.bem` 装进标准 ZIP；ZIP 不改变 BEM 本身，管理器会解出后分别管理。

## 3. Manifest 通用规则

- JSON 不允许重复键、NUL 字符，嵌套不超过 48 层。数值字段为 UInt32 整数，`srgb` 为布尔值。
- 稳定 ID 匹配 `[A-Za-z0-9][A-Za-z0-9_.-]{0,95}`，区分大小写。名称和身份字符串最多 256 UTF-8 字节。
- 资源之间用数组下标引用，不含本机路径或运行时地址。
- 未知的普通字段视为可选元数据。会影响渲染结果的新语义必须通过新能力或新版本引入，不能藏在可选字段里。

顶层字段：

| 字段 | 说明 |
| --- | --- |
| `schema` | 固定 `1` |
| `package_id` | 包身份；发布更新时保持不变 |
| `name`、`author`、`version` | 显示名、作者、显示版本 |
| `required_capabilities` | 必需能力数组 |
| `target` | 目标角色契约，见第 4 节 |
| `meshes` | 网格资源，最多 4096 |
| `textures` | 贴图资源，最多 4096 |
| 1.0：`default_appearance_id`、`appearances` | 固定外观，见第 5 节 |
| 1.1+：`option_groups`、`component_rules`、可选 `selection_constraints` | 组合选项，见第 6 节 |
| 1.2+：可选 `texture_slots` | 见第 9 节 |
| 1.3：可选 `parameters`、`mesh_deformations` | 见第 10 节 |

## 4. Target：目标角色契约

```json
"target": {
  "character_id": "chr_0013_aglina",
  "platform": "windows-x64",
  "profile_id": "aglina.windows", "revision": "20260918", "snapshot": "",
  "world_resource": "chr_0013_aglina_postmodel",
  "ui_resource": "chr_0013_aglina_uimodel",
  "components": [
    {"id": 0, "mesh_name": "S_actor_example_body_01_lod0", "original_index_count": 300,
     "bone_names": ["Bip001_Pelvis"], "materials": ["M_actor_example_body_01"]}
  ]
}
```

- `character_id` 是完整角色 ID。一个包只对应一个角色。
- `platform` 是格式常量，固定为 `windows-x64`。Android 读取同一个包，不要改成其他值，否则会被拒绝。
- `world_resource`（场景模型根，`*_postmodel`）和 `ui_resource`（详情模型根，`*_uimodel`）必须不同。
- `profile_id`、`revision`、`snapshot` 记录资料来源，`snapshot` 可以为空。
- `components` 列出替换、保留、隐藏以及只作为骨骼或材质来源（donor）的**全部**部件，1..64 项：
  - `id` 按 0..N-1 连续；`mesh_name` 是原游戏 LOD0 Mesh 的**完整名称**，不可重复；`original_index_count` 是原 Mesh 所有子网格索引数之和，为 3 的正倍数。
  - `bone_names` 按原生骨骼槽位顺序排列，最多 65536；`materials` 按原生材质槽位顺序排列，最多 256。
  - 1.2+ 可选 `bone_name_aliases`，见第 9 节。

包里不带 bindpose、骨架或 Shader。运行时使用游戏当前原 Mesh 的 bindpose 和原材质。

## 5. 1.0 固定外观

```json
"default_appearance_id": "outfit-a",
"appearances": [
  {"id": "outfit-b", "name": "另一套服装", "description": "固定状态",
   "components": [{"target": 0, "operation": "replace", "mesh": 0},
                  {"target": 1, "operation": "keep"},
                  {"target": 2, "operation": "hide"}]}
]
```

- 1..64 个外观。每个外观独立、完整地列出 target 的全部部件，不继承、不叠加。
- `operation`：`replace`（换成 `meshes` 中的一项）、`keep`（保留原网格）、`hide`（隐藏）。
- 可选 `preview`：PNG Payload ID，最多 8 MiB。当前管理界面显示角色公共头像，预览图留给工具使用。

## 6. 1.1+ 组合选项

1.1 起不再使用 `default_appearance_id` 和 `appearances`，改用选项组与部件规则。

```json
"option_groups": [
  {"id": "outfit", "name": "服装", "default": "a",
   "choices": [{"id": "a", "name": "外观 A"}, {"id": "b", "name": "外观 B"}]},
  {"id": "part21", "name": "局部部件", "default": "on",
   "available_when": {"eq": ["outfit", "b"]},
   "choices": [{"id": "on", "name": "显示"}, {"id": "off", "name": "隐藏"}]}
],
"component_rules": [
  {"target": 0, "candidates": [
    {"when": {"eq": ["outfit", "a"]}, "operation": "keep"},
    {"when": {"eq": ["outfit", "b"]}, "operation": "replace", "mesh": 0}
  ]}
]
```

**选项组**
- 每组有稳定 `id`、显示 `name`、`default` 和若干互斥 `choices`。组数上限 64，每组选项数上限见第 11 节。组合总数没有上限。
- `available_when` 只能引用排在前面的组。组不可用时，界面隐藏它但保留用户的保存值；求值时该组没有有效值，针对它的 `eq` 为 false。重新可用时恢复保存值。
- 可选 `selection_constraints`：条件数组，全部成立才是合法组合，用来表示源 Mod 实际能到达的状态。默认组合必须合法。
- 1.3 只有滑条、没有离散选项的包，`option_groups` 可以是空数组，规则用无条件谓词。1.1/1.2 至少需要一个组。

**条件语法**：`true`、`false`、`{"eq":["组","选项"]}`、`{"all":[...]}`、`{"any":[...]}`、`{"not":条件}`。深度不超过 16，`all`/`any` 每个节点 1..32 项。不执行任何源 INI、表达式或 Shader。

**部件规则**
- `component_rules` 按 `target` 0..N-1 顺序覆盖全部部件。每个候选的操作为 `keep`、`hide` 或 `replace`（带 `mesh`）。
- 在任意合法组合下，每个部件**恰好**有一个候选成立；`replace` 的网格至少选中一条 draw。创作者工具用精确决策图证明这一点（不抽样），并求出最坏情况下的 draw、索引、贴图数和内存。决策图最多 250000 个节点，超过即拒绝。

**keep 贴图覆盖**（能力 `keep-material-textures`）

保留原网格但替换其材质贴图时，在 keep 候选中写：

```json
{"operation": "keep", "material_overrides": [
  {"material_slot": 0, "material_name": "原生材质名", "textures": [0]}]}
```

槽位按目标部件的原生材质表定位，名称必须一致。每个槽位不得重复，每项至少一张贴图。`hide`、`replace` 不能带这个字段。

**保存与读取**
- 设置保存为 `组:选项&组:选项`（例如 `outfit:a&part21:on`），缺省的组取 `default`。1.0 保存为外观 ID。
- 包必须保留所有候选资源，不按安装时的选择裁剪，同一个包可以 A→B→A 切换。
- 运行时只读取当前选择用到的 payload，未选中的内容不读取、不解压、不上传。

## 7. Mesh

| 字段 | 规则 |
| --- | --- |
| `vertex_count` | 1..1,048,576 |
| `index_size` | 2 或 4（UInt16/UInt32），每个索引小于 `vertex_count` |
| `streams` | 固定 3 项 `{"stride":N,"payload":ID}`；stride 1..64，payload 长度等于 `vertex_count × stride` |
| `attributes` | 每项 `[semantic, format, dimension, stream, offset]`，取值为 Unity `VertexAttributeDescriptor` 的枚举 |
| `bones` | 局部骨骼表（palette），1..256 项 |
| `draws` | 有序绘制段，每部件最多 256 条 |
| 1.0：`indices`、`index_count` | 整个网格一条索引 payload |

**顶点属性**
- 最多 16 项；semantic 0..13 且不重复；dimension 1..4；stream 0..2。
- format 0..11 的元素字节数依次为 `4,2,1,1,2,2,1,1,2,2,4,4`。同一流内按声明顺序紧密排列，offset 等于前面属性大小之和，总和等于 stride。
- 运行时使用包内的声明构建新网格，不要求与原 Mesh 布局相同。

**蒙皮流（stream 2）**：只允许以下三种，BlendIndices 必须是最后一个属性。

| stride | 声明 | 说明 |
| --- | --- | --- |
| 4 | `[13,6,4,2,0]` | UInt8×4 骨骼索引，刚性蒙皮，只用第一个索引，权重 1 |
| 12 | `[12,4,4,2,0]`、`[13,6,4,2,8]` | UNorm16×4 权重 + UInt8×4 索引 |
| 32（1.2+） | `[12,0,4,2,0]`、`[13,10,4,2,16]` | Float32×4 权重 + UInt32×4 索引，权重非负、有限 |

每个顶点的权重之和为 1±0.01。运行时另有影响数限制，见运行时文档。

**骨骼表 `bones`**

```json
{"component": 0, "index": 17, "name": "Bip001_Head"}
```

顶点骨骼索引指向本表。每项的 donor 部件、索引和名称必须与 target 表及实际游戏资源一致。表长不超过 256；源数据的 16 位骨骼索引可以离线重映射到本表，超过 256 拒绝，不截断、不自动分片。骨骼来源和材质来源可以是不同部件。

**绘制段 `draws`**

1.0：

```json
{"start": 0, "count": 300, "material_component": 1, "material_slot": 0,
 "material_name": "M_actor_example_body_01", "textures": [0, 1]}
```

绘制段必须按顺序、无缝覆盖整条索引流，每段 `count` 为 3 的正倍数。

1.1+：每条 draw 自带索引 payload，不再使用网格级 `indices`/`index_count`：

```json
{"when": {"eq": ["part21", "on"]}, "indices": 7, "count": 300,
 "material_component": 0, "material_slot": 0, "material_name": "原生材质名", "textures": [0]}
```

- `indices` payload 恰好保存 `count × index_size` 字节。几何相同的两条 draw 可以引用同一个 payload。
- 运行时按清单顺序把选中的 draw 拼成一条索引流，重新计算各段 `start`。
- 不要把未选 draw 独有的索引和其他 draw 合并进同一个 payload，运行时按 payload 读取。

**材质**：`material_component` + `material_slot` 指定 donor 部件的原生材质槽，`material_name` 必须与该槽的材质名一致。运行时复制原材质，保留原 Shader、关键字、参数和采样状态；包里不带 Shader 程序。

**贴图引用**：`textures` 引用顶层贴图表（1.2+ 也可写 `{"slot":"槽ID"}`）。同一条 draw 不能重复引用同一项，也不能对同一原始贴图赋值两次。

## 8. Texture

| 字段 | 规则 |
| --- | --- |
| `width`、`height` | 1..32768；BC 格式要求宽高为 4 的倍数 |
| `mips` | 1..16；payload 包含完整声明的 mip 链，由大到小连续，每级宽高减半取整且至少为 1，无行填充 |
| `format` | Unity TextureFormat，见下表 |
| `srgb` | 颜色空间 |
| `original_name` | donor 材质上被替换的原 Texture 对象的完整名称 |
| `payload` | 数据 Payload ID |
| 可选 `semantic` | `"normal"` 表示法线贴图 |
| 可选 `normal_encoding` | `"xy-unorm"` 或 `"xyz-unorm"`；Android 转换法线贴图时需要 |

| format | 格式 | 备注 |
| --- | --- | --- |
| 4 | RGBA32 | 每像素 4 字节，R/G/B/A 顺序 |
| 10 | DXT1 (BC1) | |
| 12 | DXT5 (BC3) | |
| 25 | BC7 | |
| 26 | BC4 | |
| 27 | BC5 | |
| 48 / 49 / 50 | ASTC 4×4 / 5×5 / 6×6 | 通常由 Android 安装器生成；桌面显卡一般不支持 |
| 63 | R8 | 每像素 1 字节，`srgb` 必须为 false |

- BC6H 不支持。
- 运行时在复制出的材质里按 `original_name` 逐字匹配原贴图来替换，不能用源 DDS 文件名代替。
- 同一份像素数据绑定到不同名称时，复用同一个 payload。

## 9. 1.2 新增

### 贴图槽 `texture_slots`（能力 `texture-slots`）

换色时不必复制 draw：一个槽对应一张原生贴图，按条件选出替换贴图。

```json
"texture_slots": [
  {"id": "dress_d", "candidates": [
    {"when": {"eq": ["colour", "c0"]}, "texture": null},
    {"when": {"eq": ["colour", "c1"]}, "texture": 7}
  ]}
]
```

- draw 的 `textures` 和 keep 的 `material_overrides[].textures` 可以写 `{"slot":"dress_d"}`。
- 任意合法组合下每个槽恰好一个候选成立；`null` 表示保留原贴图。
- 一个槽的所有非空候选必须替换同一张原贴图（`original_name` 相同）。同一 draw 或 keep 列表中，各固定贴图和各槽替换的原贴图互不相同。

### 骨骼名别名 `bone_name_aliases`（能力 `resource-bone-aliases`）

同一个骨骼在场景资源和详情资源里名字不同时使用（例如游戏骨架里的拼写错误）。

```json
{"id": 13, "mesh_name": "S_actor_typhoea_cloth_01_lod0",
 "bone_names": ["...", "skirt_base_L_c_03_jnt", "..."],
 "bone_name_aliases": [{"index": 24, "resource": "world", "name": "skirt_base_R_c_03_jnt"}]}
```

- `bone_names` 写正确的标准名，palette 照常引用标准名。
- 每条别名包含 `index`、`resource`（`world` 或 `ui`）和 `name`，不能与标准名相同；同一 `(index, resource)` 只能出现一次。
- 运行时骨骼名等于标准名或任一别名都接受，绑定的仍是该索引上的骨骼对象。游戏修正名称后，已发布的包无需重建。

### 32 字节蒙皮

见第 7 节蒙皮流表。用于原生 Mesh 本身就是非压缩蒙皮格式的部件（例如提弗洛斯 `cloth_01`）。

## 10. 1.3 形态参数

形态模型与 EFMI ShapeKey 相同：`position = 原始位置 + Σ 参数增量`。只改变位置，原法线、切线、UV、权重、bindpose、骨骼不变。包里只有数据，不含 INI、Shader 或界面脚本。

```json
"parameters": [
  {"id": "body", "name": "体型", "min": 0, "max": 1000, "neutral": 0, "default": 0, "step": 1}
],
"mesh_deformations": [
  {"mesh": 0, "parameter": "body", "frames": [
    {"value": 0, "neutral": true},
    {"value": 1000, "payload": 12, "count": 350, "encoding": "sparse-position-f32"}
  ]}
]
```

**参数**
- 最多 64 个。值为 0..1000 的整数刻度，`min < max`，`step ≥ 1`，`max`、`neutral`、`default` 和所有帧值都必须从 `min` 起按 `step` 对齐。
- `neutral` 是零形变位置；`default` 是作者推荐的初始值，可以不同。
- 可选 `available_when` 使用选项组条件（只能引用离散选项，不能引用其他滑条）。参数不可用时按 `neutral` 计算，保留用户的保存值。
- 设置保存为 `id:刻度&id:刻度`，按清单顺序。无效或未知的保存值会被运行时规整，不会用来索引数据。

**形变通道**
- 最多 4096 个。`mesh` 是网格下标，`parameter` 是参数 ID，同一 `(mesh, parameter)` 唯一。
- 每个通道 2..64 帧，帧值严格递增，必须覆盖 `min` 和 `max`，并且恰好包含一个 `{"value":neutral,"neutral":true}`（中性帧不带 payload）。
- 其他帧恰好包含 `value`、`payload`、`count`、`encoding`，`encoding` 固定为 `sparse-position-f32`。`count` 可以为 0（表示零效果端点），不超过网格顶点数。
- 形变网格必须有 Float32 XYZ 位置属性（semantic 0、format 0、dimension 3）。通道只在该网格被选中替换时生效。多个部件共用同一网格时，相同设置下得到相同几何。

**增量 payload**

| 偏移 | 内容 |
| --- | --- |
| 0 | UInt32：JSON 描述的字节长度 J（1..4096） |
| 4 | J 字节 JSON：`{"count":N,"encoding":"sparse-position-f32"}` |
| 4+J | N 条记录，每条 16 字节：UInt32 顶点索引 + Float32 ΔX/ΔY/ΔZ |

- 总长度恰好为 `4 + J + 16N`。顶点索引小于顶点数且在同一帧内唯一；增量为有限值。没有记录的顶点增量为 0。
- 增量的单位和坐标轴与替换网格一致，是相对原始位置的绝对偏移，不是相对上一帧的增量。

**求值**：刻度正好落在某帧时取该帧增量；落在相邻两帧 a、b 之间时，按 `(b-v)/(b-a)` 和 `(v-a)/(b-a)` 线性插值两帧的绝对增量。各通道结果相加后，一次性加到原始位置上。

## 11. 上限汇总

| 项 | 1.0 | 1.1 | 1.2 / 1.3 |
| --- | --- | --- | --- |
| 外观 / 选项组 | 1..64 个外观 | 64 组 | 64 组 |
| 每组选项 | — | 16 | 64 |
| 部件候选 + draw 候选 + 贴图槽候选 | — | 512 | 4096 |
| 单次选择的贴图绑定 | 32 | 32 | 64 |
| 单张贴图（完整 mip 链） | 64 MiB | 64 MiB | 256 MiB |
| 单次选择的已解码数据 / 常驻资源 | 512 / 512 MiB | 768 / 768 MiB | 1536 / 1536 MiB |
| Payload 目录项 | 4096 | 4096 | 16384 |

各版本相同：容器 2 GiB；Manifest 4 MiB；单个 Payload 512 MiB；部件 1..64；骨骼表 256；每部件 draw 256；选中索引 16,777,216；顶点 1,048,576；形态参数 64、形变通道 4096。

预算按单次选择计算，不等于实际显存峰值；1.3 的预算按各通道全部帧保守计算。实际加载开销见运行时文档。
