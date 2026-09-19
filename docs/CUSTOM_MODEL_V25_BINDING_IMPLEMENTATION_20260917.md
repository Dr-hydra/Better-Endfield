# BEMPC25：限定四项兼容能力

> 2026-09-18 用户为洁尔佩塔 Outfit B 明确放宽索引/分片范围。v25 现支持组件头
> `index_element_size=4` 的 UInt32 索引，转换需显式 `allow_uint32_indices=true`，小组件仍用 UInt16；
> v24 仍只接受 UInt16。未增加自动分片，其他边界不变。详见[修正记录](CUSTOM_MODEL_GILBERTA_OUTFIT_B_20260918.md)。
> 下文 UInt16 限制是初始实现阶段的记录。

> 庄方宜 world/UI 八个目标 renderer 已完成实机采集，首个真实转换包已生成并部署。
> 当前证据、材质适配取舍及待实机项目见[庄方宜验证记录](CUSTOM_MODEL_ZHUANGFANGYI_VALIDATION_20260917.md)。

## 当前交付状态

用户确认的范围：骨架与材质分离、按绘制段固定分配材质、合并骨架、16 位输入骨骼索引。
不增加 Shader 改写、逐 pass 拦截、运行时换装、32 位输出 IB 或自动网格分片。

已实现转换器与生产模块路径；合成数据已打通 Python → BEMPC25 → C++ 生产解析器，
并在模拟 Host/Unity 边界验证生产骨骼/材质准备与回滚。
真实庄方宜包已通过生产 BEM 解析器，并已安装到指定测试目录和实际 catalog，加入三角色配置。
用户随后反馈“看上去是正常的”，庄方宜默认包的实机外观检查记为通过。
生成时 profile 的 `render_verified=false` 保留为工具输出状态；人工验收及范围见庄方宜验证记录。
代码位于 `dev/custom-model-design`，未合并 main、未 push。

## 实现

### 独立转换器

入口 `tools/CustomModel/convert_hash_lod.py`，核心 `hash_lod_lowering.py`。

- 只处理明确的 LOD0 回调绘制入口；按声明的持久化默认值解析条件。
- 条件用 Python AST 白名单解释，不调用 eval，不执行 INI、HLSL 或包内程序。
- 支持显式 VB/IB/贴图赋值、局部命令列表及固定 draw 范围。未知命令、GPU Shader、
  未知 pass 条件、递归和未映射材质状态均拒绝。
- 依据 `CommandListInitializeMergedSkeleton` 的 offset/count 和经核实的 group→原生骨骼映射，
  把 UNORM16×4 权重＋UINT16×4 骨骼编号映射为紧凑 palette。
  同时支持显式声明的 UINT8 输入。零权重槽不参与骨骼身份判断，输出索引清零。
- 每根输出骨骼携带“原生组件 ID、组件内骨骼索引、骨骼名称 CRC”；不按当前部件简单减 offset。
- 只收集选中 draw 实际引用的顶点，去除未选分支的几何；重建 16 位 IB 和连续绘制段。
  紧凑后超过 65536 顶点或 256 骨骼槽则拒绝，不截断、不自动分片。
- 原始 VB0/VB1 按选中顶点复制，完整通道布局由 profile 声明并由运行时核对。
  不自动裁剪 20 字节 UV。权重不擅自归一化或丢弃有效影响。
- 每个绘制段携带材质来源、原材质名称及明确的贴图绑定集合；不允许悄悄忽略显式贴图赋值。
- 全局 `TextureOverride ... this=Resource` 也必须在 profile 中明确处理，不能仅转换 `ps-t` 赋值。
- 单个源 draw 可用 `repeat_material_slots` 保留同一原生 submesh 的附加材质绘制，
  只追加索引和 draw 记录，共用顶点/palette；多源 draw 的附加材质顺序暂不猜测。
- 输出在完整转换成功后通过同目录临时文件替换；失败保留已有目标文件。

### 原生资源交付路径

`module.cpp` 继续使用 `_FinishWithAsset` 交付事务，不恢复旧实例扫描路线。

1. 先收集所有组件的原 Mesh、材质和 bones，再进行构建，避免使用已修改的邻接组件。
2. 从指定来源 renderer 取真实 Transform 与对应 bindpose，核对骨骼名称、有效性和矩阵。
3. 当前只接受相同 mesh space 的跨组件 palette；renderer 的 localToWorld 不同则拒绝，
   不猜测坐标变换。游戏实际是否满足该条件仍需原生资源验证。
4. 创建 typed bones/bindposes 数组，复用原生顶点布局，初始化原 `m_BonesPerVertex` 后上传。
   新包额外检查有效骨骼影响数不超过原生字段；字段仍使用已有动态偏移解析。
5. 按 draw 范围创建多个 submesh，每段复制指定原生材质，保留其 Shader/参数。
   同名贴图可在不同段绑定不同 payload；相同 payload＋相同 sampler 来源复用纹理。
6. 提交 bones、Mesh、materials、enabled，并逐项读回。失败恢复原 bones、Mesh、materials、enabled。
   临时强引用在交付结束释放，运行时不新增长期强引用。
7. 自然克隆判重保留 Mesh/材质身份检查，并检查新 palette 的骨骼名称顺序。

v24 不修改 bones，继续单 submesh 路径。v25 的 `NoGeometry` 保留原部件可见状态；
`Hidden` 才明确隐藏。v24 历史处理保持原样。

新增 typed array 创建使用 GameAssembly 的 `il2cpp_object_get_class` 和
`il2cpp_array_new_specific`，不增加固定字段偏移。导出缺失时 v25 构建拒绝，v24 不依赖它们。

### 角色资料接入

`mod_registry` 支持 runtime.ini 内的显式适配描述，避免每加一个角色就改 C++ 表：

```ini
[Adapter.example]
evidence=verified source export and runtime declaration reference
world_resource=EXACT_VERIFIED_WORLD_RESOURCE
ui_resource=EXACT_VERIFIED_UI_RESOURCE
component_count=2
component0=EXACT_VERIFIED_MESH_0,6
component1=EXACT_VERIFIED_MESH_1,3

[Mod.example]
enabled=true
character=example
package=example.bempoc
```

上述仅为格式示例，不能作为真实角色配置。组件 ID 必须和 profile/包一致；
禁止覆盖内置角色，禁止重复资源路由、重复 Mesh 名或无效索引数。
配置拥有其字符串和组件存储，registry 移动后 adapter 指针仍有效。

## Profile schema=2

必须包含 `verified: true` 与 `evidence`，这只是记录核实责任，不能替代真实证据。
**不得为了绕过校验而给未知资料填写占位名称并标记 verified。**

| 字段 | 内容 |
| --- | --- |
| `components` | 连续整数 ID 的对象表，各项含 mesh_name、original_index_count、strides、attributes、bone_names、materials、evidence |
| `attributes` | 按 attribute ID 排序的 `[attribute, format, dimension, stream]` 数组；使用 Unity 原生枚举，含完整通道 |
| `bone_names` | 原 renderer 的真实 bones 顺序，不是 Mod 内的合并编号 |
| `materials` | 原 renderer 的材质槽名称顺序 |
| `unmodified` | 没有替换入口的组件必须显式标 true，保留原几何与可见状态 |
| `skeleton_groups` | INI group ID → `{component, indices, evidence}`，indices 把组内编号映射到指定原生组件的骨骼索引 |
| `entries` | 原 draw hash → `{component, input_strides, skin, material_rules}` |
| `skin` | `unorm16x4_uint16x4`、`unorm16x4_uint8x4` 或 `rigid_uint8x4` |
| `local_bones` | entry 可选 `{component, indices, evidence}`：输入局部骨骼编号映射到明确原生组件；与源合并区间/`skeleton_groups` 互斥 |
| `material_rules` | 精确匹配 `bindings` 状态；给出 donor `component`、`slot`、`evidence` 和 `textures` |
| `textures` | `[{resource: INI资源名, original_texture: 原生贴图对象名}]` |
| `preserve_resources` | 明确保留原纹理时的 `资源名→核实依据`，禁止无依据忽略 |
| `repeat_material_slots` | material rule 的可选同 donor 附加槽数组，仅接受一个源 draw，按数组顺序重复绘制 |
| `texture_overrides` | 顶层纹理 hash→resource、evidence、disposition；`material_rules` 还需 original_texture 且实际选中绘制必须交付；`preserve_native` 需 reason |

完整可执行的**合成**示例由 `tools/CustomModel/test_hash_lod_lowering.py::fixture` 生成，
不代表任何游戏角色。角色布局、材质语义、骨骼语义均需从真实资源核实。

## BEMPC25 wire layout

仍为 little-endian，保留 v24 的文件头、组件头和纹理记录布局；magic=`BEMPC25\0`，version=25。
有几何的组件：

```text
原 52 字节 component header
  reserved0 = palette_count（1..256）
  reserved1 = draw_count（1..256）
VB0 + VB1 + 原生布局 VB2 + UINT16 IB
uint32 native_layout_crc
palette_count × {uint32 component, index, bone_name_crc}
draw_count × {uint32 start, count, material_component, material_slot, material_name_crc, texture_bitmask}
```

布局 CRC 是原生 VertexAttributeDescriptor 的小端字节序身份校验；名称 CRC 沿用已有 pin 做法。
这些是协议字段，不计算发行产物哈希。纹理仍最多 32 项、骨骼输出仍使用原生 UINT8 通道。
输出 16 位骨骼通道不在本阶段范围内：支持的是读取、正确重映射 16 位输入。

draw 必须连续且完整覆盖输出 IB；骨骼和材质必须引用包内组件；纹理必须有明确名称 pin，
必须被 draw 使用且组件掩码对应。坏引用、越界、截断、空身份等由生产解析器拒绝。

## 实际样本的离线结果

庄方宜默认 `$swapkey7=0`：

| hash | 选中 draw | 选中索引 | 实际引用顶点 | 实际引用合并骨骼 |
| --- | ---: | ---: | ---: | ---: |
| 6af95e58 | 1 | 52332 | 14370 | 53 |
| fe47dc61 | 2 | 40167 | 7672 | 77 |
| 0f9e1087 | 2 | 7362 | 1731 | 11 |
| a8b9073c | 11 | 269799 | 59607 | 61 |

四组默认数据均在 16 位输出索引和 256 骨骼槽范围内；所选权重和 INI 声明的合并区间未发现错误。
但这只证明数值与结构可进入该路径，不证明原生 bones/bindposes、20 字节 UV 或材质对应正确。
详细数据：`artifacts/converter-new-mods/zhuangfangyi-default-audit.json`。

洁尔佩塔包含自定义骨骼 Shader、按 pass 分流和跨 IB 绘制，通用 hash/LOD 入口仍会拒绝。
后续新增明确审阅的源程序配方入口，将本样本的默认绘制归并后复用相同 BEMPC25 输出，
见[洁尔佩塔测试包记录](CUSTOM_MODEL_GILBERTA_VALIDATION_20260917.md)。
不执行 RabbitFX 或 Shader；Hash/LOD 命名本身不代表使用同一种骨架机制。

## 验证与剩余项

- 独立前端 8 项测试、lowering 11 项测试通过。
- 生产 CustomModel Release DLL 构建通过。
- 新增生产边界测试：v25 解析/损坏拒绝、跨部件骨骼及 bindpose 配对、材质/同名贴图隔离、
  纹理复用、准备期间不发布、提交失败全量恢复、临时引用释放、动态角色路由、v24 实际包读取。
- 测试程序 `BetterEndfield.CustomModelBindingTests` 使用生产函数及模拟 Unity 对象，
  不执行游戏 GPU 上传。既有游戏场景未重测，也未添加运行时诊断扫描。

庄方宜真实 profile 的提取核对及测试包部署已经完成，用户确认默认包看上去正常。
具体动作/技能覆盖未单独确认，不宣称所有场景验收；不扩展到 Shader 改写，不重复既有场景回归。

### 资料接入进展

公共高光的后端误判已修正，庄方宜 64/64 renderer 离线引用完整。新增 `prepare_native_profile.py`，
核对四个候选组件的世界/UI 身份和八组合并骨架；LOD1 组通过相同 Transform 与 bindpose 映射回 LOD0。
输出 `artifacts/native-parser/zhuangfangyi-profile-preparation.json` 保留未放行的 profile 草稿、
原生材质目录和所有默认 draw 状态；此准备阶段草稿不作为最终放行状态。
后续运行时声明已核实，十项全局覆盖由原生 mip 字节一致性处理，显式 ps-t 使用已审阅材质配方。
`finalize_native_profile.py` 输出最终 profile；身体首段的颜色/法线槽按语义适配，详见验证记录。
本轮 lowering 17 项、Shader/材质证据 4 项测试通过，真实包和三角色配置通过生产解析。
仅验证本次变更，未重跑既有游戏场景。
