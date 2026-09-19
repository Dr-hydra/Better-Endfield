# CustomModel 当前能力与兼容性声明

日期：2026-09-18。本文描述当前分支已经实现、通过生产解析/边界验证并用于测试包的能力。
“兼容”表示转换器能在有明确原生 profile/运行时声明的前提下生成并交付 BEM；
不表示任意同名角色、任意游戏版本或任意 Mod 自动兼容。

## 已实现的能力

### 运行时替换

- 在资源交付边界处理 world/UI 模型模板；不依赖旧的全局实例扫描作为主路径。
- 角色通过 `runtime.ini` 的 `[Adapter.*]` 明确绑定资源根、Mesh 名和原始索引数，
  `[Mod.*]` 明确绑定一个包；未知角色、重复包或身份冲突会停用该角色。
- 一次可启用多个角色，角色配置互相隔离。当前真实验证过管理员、佩丽卡、庄方宜和洁尔佩塔。
- 交付是事务式的：收集原始 Mesh/bones/materials，构建、逐项读回，失败恢复原始绑定。
- LOD 锁定是独立开关：有任意 Mod 启用时强制启用；无 Mod 时由
  `[CustomModel] standalone_lod=true|false` 单独控制。LOD 只锁定原生 LOD0，不为 Mod 自动生成低模。

### BEM v24/v25

| 项目 | v24 | v25 |
| --- | --- | --- |
| 原始 VB/VB1/VB2 | 保留 | 保留并核对原生 VertexAttributeDescriptor |
| 骨骼处理 | 原路径 | 合并/局部骨架、明确 palette、骨骼名称 CRC |
| 材质 | 单 submesh 兼容路径 | 按 draw 段固定 donor 材质，可保留重复材质槽 |
| 输入骨骼索引 | 既有样本路径 | `UNORM16×4 + UINT16×4`、`UNORM16×4 + UINT8×4`、刚性 UINT8 |
| 几何索引 | UInt16 | 默认 UInt16；经 profile 显式授权可用 UInt32 |
| 角色资料 | 内置/既有映射 | schema=2 verified profile + world/UI runtime observations |
| 向后兼容 | 生产读取 | 生产读取 v24；v24 不接受 v25 UInt32 扩展 |

v25 UInt32 不是自动扩展：profile 必须写入 `allow_uint32_indices=true`，只有超过 65,536
顶点的组件使用四字节索引，其余组件仍用 UInt16；当前上限为 1,048,576 顶点。
当前没有自动网格分片，也没有 UInt32 骨骼通道输出。

### 转换器入口

| 输入类型 | 入口 | 当前状态 |
| --- | --- | --- |
| EFMI ALPHA ComponentN | `convert_efmi_poc.py` | 已有管理员/佩丽卡样本路径；需对应 source/material profile |
| Hash/LOD 静态绘制 | `convert_hash_lod.py` + `hash_lod_lowering.py` | 只接受明确条件、draw、骨架和材质映射；未知语义拒绝 |
| Hash/LOD 审阅样本 | `convert_reviewed_draws.py` | 已用于洁尔佩塔 Outfit B；严格匹配主 INI 和审阅过的 HLSL 文本 |
| 原生资料 | `extract_native_bundles.py` → `NativeAssetReader` → `parse_native_models.py` | 保留 PathID/PPtr、Mesh、bones、bindposes、材质、贴图和来源快照 |

原生资料解析支持离线文件和一次性定向运行时 probe。运行时声明不会被离线 stride 猜测覆盖；
例如当前样本的 Normal 为 Float32×1，不能仅按字节宽度改填成 UNorm8×4。

## 材质兼容边界

- 运行时保留游戏原 Shader、关键词、数值参数、sampler 来源，只替换明确绑定的纹理对象。
- 支持按绘制段使用不同 donor 材质、同一 source draw 重复原生材质槽，以及全局纹理覆盖的显式处理。
- 材质 property 名、原生贴图对象名和显式 ps-t 语义必须有资料证据；不把 Unity property ID 当作 D3D `ps-t` 槽号。
- 支持 BC7、BC5、BC4、DXT1、DXT5 的 DDS block-compressed payload；保留完整 mip 链时必须通过尺寸、格式和字节核对。
- BEM 当前不做包级压缩；纹理 payload 以原始压缩块写入，多个原生名称 pin 可能重复同一 DDS。
  因此包体积可能明显大于 RAR/ZIP 源包。洁尔佩塔 Outfit B 的 480,065,480 字节中，
  469,762,048 字节是七条 8K 自定义纹理记录。

## 已验证样本

| 样本 | 结果 | 范围 |
| --- | --- | --- |
| 管理员 | 实机正常 | v24/v25 既有资源路径、LOD 锁定 |
| 佩丽卡 | 实机路径已验证 | v25 材质/多角色资源交付 |
| 庄方宜 | 用户确认默认包显示正常 | 合并骨架、16 位输入骨骼、运行时声明 |
| 洁尔佩塔 Outfit B | 用户确认可用 | 审阅绘制归并、局部骨骼映射、四组件 UInt32/UInt16 混合索引 |

这些结果绑定当前游戏 VFS 快照、资源身份和包配方，不等于跨版本保证。

## 明确不兼容或暂不承诺

- 需要执行任意 HLSL/Shader 改写、逐 pass GPU 拦截、RabbitFX 行为或实时 Shader 注入的 Mod，
  不会被通用转换器自动转换。洁尔佩塔仅支持已审阅的固定 Outfit B 配方。
- 任意运行时换装面板、热切换状态、未审阅 `$swapkey` 组合、LOD1/2/3 自定义几何，
  不会自动继承；需要单独 profile 和测试。
- 不支持自动猜测骨骼 offset、bindpose、Mesh 空间、材质语义、纹理 hash 或缺失公共贴图。
- 不支持自动网格分片、三十二位骨骼索引输出、任意 UINT32 顶点格式扩展、非声明顶点通道适配。
- 不保证原生 Shader 的每个 pass 与 Mod 的自定义材质效果逐像素一致；法线关闭、LightMap/参数图
  的适配必须在配方中明确记录并由实机确认。
- v25 包当前无压缩封装；发布体积优化（纹理 alias/payload 去重、包级压缩）尚未实现。

## 兼容判断顺序

新 Mod 必须依次满足：

1. 入口格式和所有资源文件可静态读取，未知命令/条件不被忽略。
2. 原生 world/UI 资源身份、Mesh、顶点声明、bones、bindposes 和材质槽可对应。
3. 默认或明确选定状态的每个 draw、骨骼来源和纹理绑定都有 profile 证据。
4. 输出索引宽度、palette、材质 donor、纹理格式和包 EOF 通过生产解析器。
5. 只对修改的入口做针对性实机检查；未验证的状态保持未承诺。

当前代码位于 `dev/custom-model-design`，本声明不代表已经合并、push 或发布 Release。
