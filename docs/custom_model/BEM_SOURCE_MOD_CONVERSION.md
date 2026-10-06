# 其他来源 Mod 转换为 BEM

本文说明如何把 EFMI / 3DMigoto 格式的角色 Mod 转换为 BEM：哪些能自动转换、哪些需要配方、转换器怎样确认身份，以及常见报错。工具和导出工程的用法见 [创作者指南](BEM_CREATOR_GUIDE.md)。

下文的通用 `convert` 与角色资料适用于旧角色目标。BEM 1.4 的多资源/静态网格已由 `pack/unpack/build/validate/bundle` 支持，但普通角色 profile 和配方不能直接套用于武器或独立大招。须取得目标平台的真实 prefab、LOD、receiver、骨骼与材质身份，使用 `target-profile` 生成未验证起点，再准备明确的来源映射与可编辑工程。Windows 已验收的测试 Mod 不构成 Android donor 证据；手机资源声明与纹理兼容均需独立核对。见 [1.4 制作流程](BEM_CREATOR_GUIDE.md#武器与大招工程14) 和 [1.4 规范](BEM_V1_4_SPEC.md)。

## 1. 基本原则

- 转换器读取源 Mod 的 INI 声明和资源，在**静态**条件下求出要绘制的网格和贴图，再按原游戏资源身份写成 BEM。
- 不执行源 Mod 的热键、INI 命令列表、HLSL Shader 或 GUI 脚本。
- 身份只按实际 INI 声明和资源哈希判断，不按角色名、压缩包文件名或作者说明放行。
- 转换成功只表示离线校验通过，报告中 `render_verified` 始终为 `false`，仍需要实机检查。
- 无法等价转换的效果必须在报告的 `excluded_features` 中列明，不能悄悄丢弃。允许移除不支持的特效、低模 LOD 和动画系统；LOD0 基础网格、基础材质贴图和全部有效静态外观必须保留。如果特效命令实际负责基础绘制、骨骼变换或基础贴图，需要先映射成 BEM 能表达的形式，不能直接删除。

## 2. 输入

- 支持已解压目录、ZIP、RAR、7z。RAR/7z 通过随工具附带的 7-Zip 26.03 读取，不解压到磁盘。
- 限制：单个文件 512 MiB，合计 4 GiB，最多 8192 个条目。拒绝加密、分卷不全、符号链接、不安全路径和大小写冲突的压缩包。
- 入口 INI：扫描全部 `.ini`，跳过路径中以 `disabled` 开头或名为 `backup(s)` 的部分。必须恰好找到一个入口，否则报 `ENTRY_SELECTION`，可以用 `--ini` 或配方的 `ini` 指定。

## 3. 自动化分级

`inspect` 报告中的 `automation.status`：

| 状态 | 条件 | 能否直接转换 |
| --- | --- | --- |
| `ready` | 标准 ComponentN，角色资料唯一匹配，默认外观完整校验通过 | 可以，不需要配方 |
| `standard_candidate` | 标准 ComponentN，但角色匹配或校验未通过 | 否，原因见 `issues` |
| `requires_mapping` | Hash/LOD 格式；或 ComponentN 中含 `ps-tN =` 逐段贴图写入、合并骨架、16 位骨骼索引 | 需要已核实的 profile 和配方 |
| `manual_only` | 作者自定义 Shader（EFMI 自带的 `CustomShader\EFMIv1\` 除外）、任何 RabbitFX、ShapeKey、多种入口混合 | 不能自动转换，需要专门审阅的配方 |

入口格式识别：
- `TextureOverride_(EntryPoint_)?ComponentN(_LODn)?`：ComponentN 格式。
- `TextureOverride_(EntryPoint_)?LOD<n>.<hash8>_<索引数>_<起始索引>`：Hash/LOD 格式。

## 4. 支持范围

| 源特性 | 结果 | 原因 |
| --- | --- | --- |
| 标准 ComponentN 静态外观 | 自动 | 身份和布局能由角色资料证明 |
| Hash/LOD | 配方 | 需要核实的 profile 映射骨架和材质 |
| `ps-tN` 逐段贴图写入 | 配方 | 槽号与原生材质属性之间没有全角色通用的对应表 |
| 合并骨架、16 位骨骼索引 | 配方 | 需要核对骨骼分组和原生骨骼顺序 |
| RabbitFX（包括 Stable Textures） | 不支持自动 | 外部材质框架，不能假定与原生材质等价 |
| GlowFX、自定义 Shader、ShaderOverride/Regex | 不支持自动 | 运行时没有对应的执行器 |
| ShapeKey | 不能静态转换；可用 1.3 滑条显式绑定（第 7 节） | 源 GUI 不执行，滑条名称和范围需作者给出 |
| 热键切换的多套外观 | 默认只取静态默认状态；多外观需要整理为 1.1 选项 | 不盲目枚举热键组合 |
| 低模 LOD、动画 | 不转换 | 只替换 LOD0 |
| 雨雪等全局贴图 | 与原游戏资源一致时沿用；被改写时拒绝（`GLOBAL_TEXTURE`） | 不修改全局效果 |

ComponentN 自动路线具体支持：标准本地骨架入口；vb0/1/2 三路顶点流（vb3 只能等于 vb0），布局与资料一致；UINT16/UINT32 索引；零基顶点；能静态求值的默认开关；固定贴图覆盖（单一 `this = Resource…`）。绘制回调为 `drawindexed = 索引数, 起始索引, 0` 表示保留原绘制，回调为空表示隐藏该部件；多个原生材质按游戏顺序重放。

## 5. 身份与角色资料

- **部件身份**：使用入口的 `hash`（EFMI 区域哈希，即绘制区域内原始索引字节的 CRC32C）和 `match_index_count`。`match_first_index` 必须为 0，否则报 `SUBMESH_MAPPING`。
- **ComponentN 编号只是包内标签**，不对应固定部位。
- **角色资料**：工具自带 `catalog/`，按哈希自动匹配，用户不需要手选。所有哈希必须恰好命中一份资料，否则报 `CHARACTER_CATALOG`。哈希相同但索引数不同，说明游戏已更新，报 `CHARACTER_REVISION`。
- **当前资料规模**：33 个角色、371 个部件入口、1,210 项贴图身份。贴图按角色累计，共享的公共贴图会重复计数。
- **不进入自动转换的部件**：
  - 62 个部件的顶点布局或多子网格暂不支持，选中替换时报 `NATIVE_LAYOUT_UNSUPPORTED`。
  - 10 个多子网格入口需要专门规则。
  - 卡缪 `skill_01`、提弗洛斯 `cloth_01` 因场景与详情模型存在差异，不作为共用部件。
- **贴图身份**：资料把贴图哈希映射到原游戏 Texture 名称（BEM 的 `original_name`）。哈希未知时报 `TEXTURE_MAPPING`，不按 DDS 文件名猜测。

资料只包含名称、身份、布局和证据，不包含游戏几何或像素。游戏更新后由维护者用 `developer-tools/` 重新采集。

## 6. 源 DDS

| 类型 | 支持 |
| --- | --- |
| DX10 头 | BC1、BC3、BC4_UNORM、BC5_UNORM、BC7、RGBA8、BGRA8（自动换成 RGBA）、R8 |
| 旧式 FourCC | `DXT1`、`DXT5`、`ATI1`/`BC4U`、`ATI2`/`BC5U` |
| 未压缩（FourCC 为 0） | RGBA32、BGRA32 掩码、L8（映射为 R8） |
| 拒绝 | BC2/DXT3、BC6H、SNORM BC4/BC5、立方体/体积/数组贴图、带行填充的未压缩数据、BC 尺寸不是 4 的倍数 |

- sRGB 只从 DDS 头读取；旧式 FourCC 和旧式 RGBA 头一律按线性读取。
- 尺寸上限 32768，mip 不超过 16。错误前缀为 `DDS_FORMAT:`。
- 文件名里的格式或用途标签不作为依据。

## 7. 转换配方

配方用于固定包信息、组合多个外观，或给需要映射的来源提供 profile。示例在工具目录 `examples/conversion.recipe.json`，路径相对配方所在目录。

```json
{
  "schema": 1,
  "package": {"id": "creator.character.outfits", "name": "外观合集", "author": "作者名", "version": "1.0.0"},
  "target": {"character_id": "chr_0013_aglina", "world_resource": "chr_0013_aglina_postmodel",
    "ui_resource": "chr_0013_aglina_uimodel", "profile_id": "aglina.windows", "revision": "20260918"},
  "default_appearance_id": "default",
  "appearances": [{"id": "default", "name": "默认外观", "format": "hash-lod",
    "profile": "profiles/verified-native.json", "ini": "角色.ini"}]
}
```

| 字段 | 说明 |
| --- | --- |
| `package` | 包 ID、名称、作者、版本；更新已发布的包时保持 ID 不变 |
| `target` | 目标角色；所有外观必须一致，否则报 `APPEARANCE_TARGET` |
| `deformations` | 可选，1.3 形态配置路径 |
| `appearances[].format` | `auto`、`component-n`、`hash-lod`、`reviewed-draws`。`auto` 只识别入口格式，不能生成缺失的映射 |
| `appearances[].source` | 可选，从不同源组合外观（目标必须一致） |
| `appearances[].description` / `preview` | 外观说明；预览 PNG 不超过 8 MiB |
| ComponentN | 可加 `source_profile`、`material_profile`；profile 需为替换部件提供 `v24_draws`（转换桥接用的绘制映射） |
| Hash/LOD | profile 必须是 `schema=2`、`verified=true` 且带证据，包含各部件的 `mesh_name`、`original_index_count`、`strides`、`attributes`、`bone_names`、`materials` 及蒙皮和材质规则 |
| `reviewed-draws` | `reviewed` 对象：`recipe`、`database`、`observations`、`native_textures`、`texture_dir`。只适用于维护者审阅过的特定源包，源 INI 或 Shader 改变后需要重新审阅 |

profile 的 `verified` 标记必须有真实证据支撑，不能只改成 `true` 来放行。

## 8. ShapeKey 绑定到 1.3 滑条

含 ShapeKey 的源包不能静态转换（`SHAPE_BINDING_REQUIRED`）。作者确认哪些形态键对应哪个参数后，可以在形态配置中显式绑定：

```json
{"value": 1000, "efmi": {
  "source": "source-mod.zip", "ini": "character/mod.ini", "component": 0,
  "shape_key": 1, "vertex_order": "exported"
}}
```

- `source` 为源 Mod（目录、ZIP、RAR、7z）；`ini` 为声明它的 INI；`component` 选择 `Resource_ComponentN_*` 资源组；`shape_key` 是 `CommandListSetShapeKey` 前赋给 `$shapekey_id` 的整数。
- `vertex_order:"exported"` 是作者的声明：导出网格与源顶点一一对应。工具不会根据部件号、stride 或名称推测。
- 导出时复制或重排了顶点，需要额外提供 `vertex_map`：为每个 BEM 顶点指定一个源顶点 ID。不同 LOD 需要各自的映射。
- 工具读取 `ShapeKeyBatchConfigs`、`ShapeKeyVertexIds`、`ShapeKeyVertexOffsets`，只取选定形态键的记录，把 FP16 增量转为 Float32 写入 BEM。
- 参数的名称、范围和默认值由作者填写，工具不从源 GUI 推断。

形态配置的其他写法见 [创作者指南](BEM_CREATOR_GUIDE.md#形态滑条13)。

## 9. 常见转换错误

| 报错 | 含义 | 解决 |
| --- | --- | --- |
| `ENTRY_SELECTION` | 没有或有多个入口 INI | 检查解压层级，用 `--ini` 指定 |
| `ARCHIVE_FORMAT` / `ARCHIVE_READ` / `ARCHIVE_PASSWORD` / `ARCHIVE_LIMIT` / `ARCHIVE_BACKEND` | 压缩包格式错误、损坏、有密码、超限，或缺少 7-Zip | 提供完整无密码的包；使用完整的工具目录 |
| `CHARACTER_CATALOG` | 哈希没有命中角色资料，或命中多份 | 确认源 Mod 对应当前游戏版本；需要维护者补资料 |
| `CHARACTER_REVISION` | 哈希相同但索引数不同 | 游戏已更新，需要更新角色资料 |
| `NATIVE_LAYOUT_UNSUPPORTED` | 部件布局或多子网格暂不支持 | 走配方路线 |
| `SUBMESH_MAPPING` / `ENTRY_PROGRAM` / `STATIC_STATE` / `DRAW_STATE` / `VERTEX_LAYOUT` / `INDEX_FORMAT` | 非标准入口、无法静态求值的开关、非标准绘制、顶点布局或索引格式 | 需要专门映射或配方 |
| `TEXTURE_MAPPING` / `TEXTURE_STATE` | 贴图哈希未知，或覆盖语句不是单一 `this = Resource…` | 简化覆盖语句；未知贴图需要证据 |
| `GLOBAL_TEXTURE` | 源包改写了雨雪全局贴图 | 去掉该覆盖 |
| `COMPANION_INI` / `MIXED_ENTRY` / `MANUAL_ADAPTATION` | 多个活动 INI、混合入口、自定义 Shader、RabbitFX | 需要专门审阅 |
| `AUTO_MAPPING_PENDING` | 需要骨架和材质映射 | 使用核实的 profile 和 `--recipe` |
| `TARGET_PROFILE` | 配方缺少已验证的 profile 或 `v24_draws` | 补全 profile |
| `source INI/shader differs …; re-audit required` | 审阅过的源包内容变了 | 重新审阅 |
| `Legacy … differs` | 转换中间结果与 profile 不一致 | 核对 profile，与运行时版本无关 |
| `SHAPE_BINDING_REQUIRED` | 源包有 ShapeKey 但未绑定 | 按第 8 节绑定 |
| `DDS_FORMAT:` | 贴图格式不受支持 | 按第 6 节转换贴图 |
| `Bone index outside palette` / `Palette/draw limit exceeded` | 骨骼映射错误或超过 256 | 修正映射，不能截断 |
| `Draws must partition IB` | 绘制段没有连续覆盖索引流 | 重叠的绘制需要显式复制对应索引 |
