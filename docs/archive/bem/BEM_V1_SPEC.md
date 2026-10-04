# BEM 1.0 格式规范

2026-09-18；扩展名 `.bem`。这是 1.0 正式协议；BEMPC24/25 仅为内部研究格式，运行时与管理 UI 不接受。组合外观扩展见 [BEM_V1_1_SPEC.md](BEM_V1_1_SPEC.md)。
当前 Windows 与 Android 读取器均接受 1.0；Android 纹理适配仍需单独处理。

分发层可使用标准 ZIP，包含一个或多个独立 `.bem`；ZIP 不改变 BEM wire 格式。
管理器解出并独立管理所选包，运行时仍直接读取 `.bem`，不在资源交付时解压 ZIP。

## 容器布局

顺序为 40 字节 Header、UTF-8 JSON Manifest、PayloadDirectory、连续 Payload 字节。
所有二进制整数为小端，结构无对齐填充。文件最大 2 GiB。没有文件校验哈希。

| Header 偏移 | 类型 | 字段 |
| --- | --- | --- |
| 0 | byte[8] | magic：`42 45 4D 00 50 4B 47 00` |
| 8 | uint16 | major = 1 |
| 10 | uint16 | minor = 0 |
| 12 | uint32 | header_size = 40 |
| 16 | uint64 | file_size，必须等于实际文件长度 |
| 24 | uint64 | manifest_size，1..4 MiB |
| 32 | uint32 | payload_count，0..4096 |
| 36 | uint32 | flags = 0 |

目录每项 32 字节，数组下标即 Payload ID：

| 项内偏移 | 类型 | 字段 |
| --- | --- | --- |
| 0 | uint32 | codec：0 raw；1 Zstd |
| 4 | uint32 | reserved = 0 |
| 8 | uint64 | offset，文件绝对偏移 |
| 16 | uint64 | stored_size |
| 24 | uint64 | decoded_size |

每个 Payload 1..512 MiB；raw 的两种长度相等。目录按文件偏移排列；首块紧接目录，后续块紧接前块，
不允许间隙、重叠、越界或尾随字节。Zstd 为独立单帧、无字典、带准确内容长度，禁止拼接帧或尾随数据。
写入器默认 level 9；压缩无收益时用 raw。等级不影响兼容性。

相同数据经逐字节比较共享 Payload ID。只有所选外观的依赖被运行时解压；工具导入验证会检查所有外观。
选定外观上传数据的累计内存预算为 512 MiB（重复绑定的数据也计入），不是 GPU 峰值内存承诺。
运行时构建还会持有压缩块、解压缓存、原模型和 Unity 对象，实际峰值可能更高。

## Manifest

JSON 不接受重复键、NUL 字符及超过 48 层嵌套。数值字段为 UInt32 整数，`srgb` 为布尔值。
字符串 UTF-8；稳定 ID 匹配 `[A-Za-z0-9][A-Za-z0-9_.-]{0,95}`，大小写敏感。
名称/身份字符串最多 256 UTF-8 字节。资源 ID 为对应数组下标，不含本机路径和运行时地址。

顶层字段：

| 字段 | 含义 |
| --- | --- |
| schema | 1 |
| package_id, name, author, version | 包身份、名称、作者、显示版本；更新复用 package_id |
| required_capabilities | 必需能力字符串数组；未知能力拒绝 |
| target | 当前包的角色/平台/原生资料契约 |
| default_appearance_id | 默认外观稳定 ID |
| appearances | 1..64 个完整外观 |
| meshes | 可共享网格资源，最多 4096 |
| textures | 纹理资源与原生名称绑定，最多 4096；每外观实际使用最多 32 项 |

当前能力字符串：`native-materials`、`palette-u8`、`indices-u32`、`fixed-appearances`。
1.0 字段语义保持不变；升级后的读取器还接受明确声明 `composable-options` 的 1.1 包。
未知普通 JSON 字段视为可选元数据；影响正确渲染的新语义必须增加必需能力或升级版本，不能藏在可选字段中。

### Target

`character_id` 使用项目的完整角色 ID，例如 `chr_0013_aglina`。
必填 `platform`（当前仅 `windows-x64`）、`profile_id`、`revision`、`world_resource`、`ui_resource`、`components`。
`snapshot` 记录资料来源游戏快照，允许空字符串但不代表已验证任意版本。两个资源根不能相同。

`components` 包含实际替换、保留、隐藏以及仅作骨骼/材质 donor 的全部组件，1..64 项：

```json
{"id":0,"mesh_name":"S_actor_example_body_01_lod0","original_index_count":300,
 "bone_names":["Bip001_Pelvis"],"materials":["M_actor_example_body_01"]}
```

组件 ID 必须按 0..N-1 连续排列，Mesh 名唯一，原索引数为正的 3 倍数。
骨骼名称按原生槽位排列，最多 65536；材质名称按原生槽位排列，最多 256。
包内无需完整离线数据库。creator profile 留存完整的资源引用、bindpose/空间关系和 world/UI 观测证据。
加载时仍核对实际原网格名称、索引数、声明、donor 名称和 Mesh 空间；使用当前原生 bindpose。

### Appearance

`id`、`name`、`description`、`components`；可选 `preview` 为 PNG Payload ID，最多 8 MiB。
当前管理 UI 展示角色共用头像和外观名称/说明，包内预览保留供工具使用。
各外观必须独立完整列出 target 的所有组件，不继承、不叠加：

```json
{"id":"outfit-b","name":"另一套服装","description":"固定状态",
 "components":[{"target":0,"operation":"replace","mesh":0},
               {"target":1,"operation":"keep"},
               {"target":2,"operation":"hide"}]}
```

每角色同时启用一个包，每包同时选择一个外观。启用状态和外观选择存在本机配置，下次启动生效。
更新移除所选外观时提示并回退默认值。稳定 ID 不应随着显示名改变。

### Mesh

字段：`vertex_count`、`index_count`、`index_size`、`streams`、`indices`、`attributes`、`bones`、`draws`。
顶点上限 1,048,576；索引上限 16,777,216，数量为正且可被 3 整除。
`index_size` 为 2 或 4，索引为 UInt16/UInt32 且每项小于 vertex_count。
`indices` 是索引 Payload ID。

`streams` 固定三个元素，`{"stride":16,"payload":0}`。stride 1..64，字节数必须为 vertex_count × stride。
`attributes` 每项为 `[semantic, format, dimension, stream, offset]`：前四项采用 Unity VertexAttributeDescriptor 的枚举值。
最多 16 项；semantic 0..13 且不重复，dimension 1..4，stream 0..2。
format 0..11 的元素大小依次是 `4,2,1,1,2,2,1,1,2,2,4,4`。
各流中按声明顺序紧密排列，offset 必须与累计大小相等，累计大小必须等于 stride。
运行时要求声明与目标原 Mesh 逐项一致；自描述不意味着允许任意布局。
流 2 只接受 4 或 12 字节，末属性必须为 `[13,6,4,2,offset]`（UINT8x4 骨骼索引）。
12 字节模式前 8 字节为 UNorm16x4 权重；4 字节模式为刚性蒙皮。边界由运行时重算，不携带可被篡改的 culling bounds。

`bones` 为 1..256 项局部 palette：

```json
{"component":0,"index":17,"name":"Bip001_Head"}
```

顶点的骨骼索引指向本 palette；donor component/index/name 必须匹配 target 表以及实际原生资源。
16 位**输入**骨骼索引可以离线映射到此表；输出仍是 UINT8，超过 256 拒绝，不截断、不自动分片。

`draws` 为 1..256 项有序绘制段：

```json
{"start":0,"count":300,"material_component":1,"material_slot":0,
 "material_name":"M_actor_example_body_01","textures":[0,1]}
```

绘制段必须按序无缝覆盖整个 IB，每段为正且可被 3 整除。骨骼 donor 与材质 donor 可不同。
textures 引用顶层纹理表，同一 draw 不可重复引用同一项，也不可在实际材质上重复赋值同一纹理槽。
保留原游戏 Shader、关键词、参数与采样状态，不携带 EFMI Shader 程序。

### Texture

`width`、`height`、`mips`、`format`、`srgb`、`original_name`、`payload`。
格式采用 Unity TextureFormat：4 RGBA32、10 DXT1、12 DXT5、25 BC7、26 BC4、27 BC5、63 R8。
宽高为正数且不超过 32768；BC 格式还要求宽高为 4 的倍数；mips 1..16。Payload 包含完整声明的 mip 链，单项不超过 64 MiB。
RGBA32 为紧密排列的 R/G/B/A 字节，每像素 4 字节；R8 每像素 1 字节且 srgb 必须为 false。mip 按由大到小连续排列，每级宽高减半取整且至少为 1；不带行填充。上述未压缩格式从 BEM Tools 1.1.4 与同期原生模块开始支持，需同步更新工具和运行时。
`original_name` 是 donor 材质上现有 Texture 对象的完整名称；运行时逐字串匹配，发现同名多个槽位即拒绝。
同一像素数据用于不同名称绑定时复用 payload。源 DDS 文件名不能代替 original_name。

## 失败与交付

版本/能力不支持、长度越界、压缩错误、悬空引用、无效蒙皮/索引/绘制段、目标声明或 donor 身份不匹配都拒绝。
准备成功后才事务提交模型绑定，失败保留或恢复原绑定。解析通过、原生契约匹配、实际外观通过分别记录。
用户启用任意包时强制 LOD 锁定；无启用包时使用独立 LOD 偏好。此状态不写进作者包。

参考实现：`tools/CustomModel/bem_v1.py`、`bem_tool.py`、`native/modules/custom_model/bem.cpp`。
