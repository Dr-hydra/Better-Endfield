# BEM 1.2 规范（在 1.1 基础上的增量）

BEM 1.2 沿用 BEM 1.1 的容器、manifest 结构、条件语法和“下次启动应用选择”模型（见 [BEM_V1_1_SPEC.md](BEM_V1_1_SPEC.md)）。头部 `minor=2`。本文只列出差异。

## 版本选择

- 写入器按内容选择版本：没有超出 1.1 上限、也没有使用下列新能力的包，仍写成 1.1，让 1.1 运行时继续可读。
- 读取器按头部版本执行对应上限。1.1 头部不能携带 1.2 内容，否则读取器拒绝。
- 1.0 和 1.1 的读取语义不变。

## 上限

| 项 | 1.1 | 1.2 |
| --- | --- | --- |
| 每组选项数 | 16 | 64 |
| 组件候选 + draw 候选 + 贴图槽候选总数 | 512 | 4096 |
| 单次选择的贴图绑定数 | 32 | 64 |
| 单张贴图（完整 mip 链） | 64 MiB | 256 MiB |
| 单次选择的解码缓存 / 常驻资源 | 768 MiB / 768 MiB | 1536 MiB / 1536 MiB |
| payload 目录项 | 4096 | 16384 |

不变的上限：
- 选项组 64 个；
- 每组件最终 draw 256 条；
- 调色板 256 根骨骼；
- 选定索引 16777216 个；
- manifest 4 MiB，容器 2 GiB，单个 payload 512 MiB；
- 决策图节点和优化状态各 250000。

## 贴图槽 `texture_slots`（能力 `texture-slots`）

作用：换色时不必复制 draw。每个槽代表一个原生贴图属性，按条件选出一张贴图。

```json
"texture_slots": [
  {"id": "dress_d", "candidates": [
    {"when": {"eq": ["colour", "c0"]}, "texture": null},
    {"when": {"eq": ["colour", "c1"]}, "texture": 7}
  ]}
]
```

- **引用方式**：draw 的 `textures`，以及 keep `material_overrides[].textures`，除了写固定贴图索引，也可以写 `{"slot": "dress_d"}`。
- **候选必须恰好命中一个**：在所有可达选择下，每个槽都只有一个候选成立，由精确决策图证明。
- **`null` 候选**：表示保留原生贴图，不绑定。
- **同一原始贴图**：一个槽的全部非空候选必须替换同一张原始贴图（`original_name` 相同）。
- **不能重复替换**：同一 draw 或 keep 列表中，固定贴图和各个槽替换的原始贴图两两不同。
- **计数**：槽候选计入 4096 条规则总数。
- **结果**：选中的贴图照常计入单次选择的贴图上限和内存预算。

## 按资源区分的骨骼名别名 `bone_name_aliases`（能力 `resource-bone-aliases`）

作用：同一 Mesh、同一骨骼对象，在 world 和 UI 两个资源里名称可以不同，例如游戏世界骨架中的命名笔误。

```json
"target": {"components": [{"id": 13, "mesh_name": "S_actor_typhoea_cloth_01_lod0",
  "bone_names": ["...", "skirt_base_L_c_03_jnt", "..."],
  "bone_name_aliases": [{"index": 24, "resource": "world", "name": "skirt_base_R_c_03_jnt"}]}]}
```

- **标准名**：`bone_names` 保存正确的标准名，调色板引用方式不变（组件 + 索引 + 标准名）。
- **别名条目**：每条别名包含 `index`、`resource`（`world` 或 `ui`）和 `name`，不得与标准名相同。同一 `(index, resource)` 只能出现一次。
- **运行时核对**：骨骼名等于标准名或任一别名都接受。绑定仍使用捐赠组件在该索引上的骨骼对象，别名不改变骨骼绑定。
- **官方修复后**：游戏改正名称后，world 骨骼直接匹配标准名，已发布的包无需重建。
- **Android**：world 资源从 UI LOD0 重绑到 world LOD1 时，如果按路径找不到，只在同一父路径下尝试已声明的别名。

## 非压缩蒙皮布局（skin stride 32）

- 部分原生 Mesh 使用非压缩顶点格式，例如提弗洛斯 cloth_01：流步长 40/16/32，权重为 float32，索引为 UInt32。
- 1.2 允许的蒙皮流声明：`[[12,0,4,2,0],[13,10,4,2,16]]`。
- 校验要求：权重非负、有限，4 个权重之和为 1±0.01；索引小于调色板长度（≤256）。
- 1.1 只允许步长 4 和 12。
- 运行时仍要求包内声明与游戏中的 Mesh 声明逐字节一致，不一致即拒绝替换。

## Android 安装

- 安装器上限不变：单边 8192，转换后单张贴图 64 MiB。
- 超出的贴图在安装时自动缩小：
  - 有 mip 链时丢弃顶层 mip；
  - 单级贴图按行带做 2^n 盒式降采样，不一次解码全分辨率。
- 被缩小的贴图记录 `android_install_mip_skip`。Windows 保持原分辨率。
- 这一步同样适用于 1.1 包：过去会因超限而安装失败的贴图，现在会被自动缩小。

## 实现位置

- **Python**：`tools/CustomModel/bem_v11.py`（`limits()`、`required_minor()`、贴图槽、别名、stride 32）、`bem_v1.py`（容器 minor 2、`package_minor()`、`target_from_profile` 透传别名）、`bem_tool.py`。
- **原生**：`native/modules/custom_model/bem.cpp`、`bem.h`（贴图掩码改为 64 位）、`module.cpp`（别名核对、stride 32 蒙皮解码）。
- **Android**：`world_resource_adapter.inc`、`installer/texture_install.cpp`。
- **管理器**：`ui/.../BemPackageService.cs`、`BemReportPresentation.cs`，以及 Android Java 中对 `bem_minor` 的判断。
- **测试**：`tools/CustomModel/test_bem_v12.py`。
