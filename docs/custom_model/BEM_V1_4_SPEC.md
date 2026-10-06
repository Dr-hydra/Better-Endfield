# BEM 1.4：多资源目标与静态网格

本规范定义 BEM 1.4 的文件契约。它扩展 1.3 的容器和组合选项，覆盖角色普通形态、独立技能/大招 prefab，以及蒙皮或静态武器。资源进入游戏和状态切换仍由游戏原有逻辑负责；BEM 不执行脚本，不注入技能状态机。

格式支持、运行时接线和游戏内验证是三个独立状态。合成包通过解析检查，不代表任意游戏资源已验证可替换。随附示例使用虚构资源名，不能作为可玩 Mod 安装。

## 版本和容器

- magic、40 字节头、32 字节 payload 目录项、raw/zstd 编码与 BEM 1.0–1.3 相同；major=1、minor=4。
- `schema=1`，沿用 `meshes`、`textures`、`option_groups`、`component_rules`、`selection_constraints`、`texture_slots`、`parameters`、`mesh_deformations`。
- `option_groups` 必须存在，可为空；`parameters`、`mesh_deformations` 可省略或为空。
- `appearances` 和 `default_appearance_id` 不用于 1.4。制作工具可先将固定外观显式转换为组合规则。
- 必须声明 `composable-options` 和 `multi-resource-targets`。任意目标组件或 mesh 为静态时还必须声明 `static-meshes`。
- 其他功能保留既有能力声明，例如 `body-parameters`、`mesh-position-deltas`、`texture-slots`。出现 1.4 字段或能力时，写入器必须选择 minor=4；不能降为旧头。
- 1.0–1.3 包继续使用原有契约；旧版本头必须拒绝 1.4 能力及 target/component/mesh 字段，不能静默忽略。

1.2/1.3 的资源预算继续适用：manifest 不超过 4 MiB，目录不超过 16384 项，单 payload 解压不超过 512 MiB，文件不超过 2 GiB，组件总数不超过 64。选项/形变及选中几何、纹理的原有上限保留。

## 目标与资源表

```json
{
  "target": {
    "kind": "character",
    "id": "chr_0030_zhuangfy",
    "platform": "windows-x64",
    "profile_id": "creator.zhuangfy.forms",
    "revision": "1",
    "snapshot": "author-verified-snapshot",
    "resources": [
      {
        "id": "ultimate",
        "name": "example_ultimate",
        "asset_path": "assets/example/example_ultimate.prefab",
        "platforms": ["windows-x64"],
        "lod": 0
      }
    ],
    "components": [
      {
        "id": 0,
        "resource": "ultimate",
        "renderer_kind": "skinned",
        "renderer_path": "Mesh_all/lod0/body",
        "mesh_name": "body_lod0",
        "original_index_count": 300,
        "bone_names": ["root"],
        "materials": ["body_material"]
      }
    ]
  }
}
```

该 JSON 只说明字段，不是庄方宜实际资源证据。

`target.kind` 为 `character` 或 `weapon`；`target.id` 是所属角色/武器稳定 ID。`profile_id`、`revision` 使用既有稳定 ID 规则：ASCII 字母或数字起始，后续允许字母、数字、`_`、`.`、`-`，总长 1–96。`snapshot` 为 1–256 字节的非空 UTF-8 描述。`target.platform` 保留历史格式常量 `windows-x64`；真正的资源平台由各资源的 `platforms` 决定，不能据此字段假定整个包只供 Windows 使用。

1.4 不再携带 `character_id`、`world_resource` 或 `ui_resource`；带入这些字段应报错。

资源表包含 1–32 项，每项字段严格为：

| 字段 | 约束 |
| --- | --- |
| `id` | 全包唯一稳定 ID |
| `name` | 小写、规范化的精确资源根名，使用稳定 ID 字符规则 |
| `asset_path` | 小写 `assets/.../*.prefab`；文件 stem 必须等于 `name`；不允许反斜线、冒号、空路径段、`.` 或 `..`；不超过 1024 UTF-8 字节 |
| `platforms` | `windows-x64`、`android-arm64` 的非空无重复子集 |
| `lod` | 整数 0–3，表示该资源实际用作 donor 的 LOD |

同一平台下，资源 `name` 与 `asset_path` 各自不得重复。互不重叠的平台可用不同 resource ID 表示同一根名，例如 Windows LOD0 与 Android LOD1 的独立合同。每个资源至少有一个组件；资源表不能留下没有组件的占位项。管理器使用 `platform + ':' + name` 作为资源冲突键，只比较当前平台上的资源；没有当前平台资源的 1.4 包不能导入或启用。同一 package ID 的更新不能更换 target kind/id。

## 当前运行时范围

交付入口先确认实际 Unity Object 是 `GameObject`，同名 `Sprite`、`Texture2D` 不进入模型处理。当前运行时依据资源根名、renderer 相对路径及 Mesh/donor 合同匹配；`asset_path` 用于制作来源和离线校验，尚未与交付时 AssetProxy 的真实加载路径核对，不能将它视为运行时来源证明。

Windows 初版只执行 `lod=0` 的显式资源，依赖已有 LOD0 锁定机制；其他 LOD 会被明确拒绝。协议允许 LOD 0–3 不代表当前各平台均已实现所有组合。

Android 普通角色使用 LOD1 接收是既有设计，旧包仍走 UI LOD0 donor → world LOD1 转接。1.4 显式资源也支持 `lod=1`，但从声明的 LOD1 接收器自身取得网格、材质和骨骼 donor，不能拿 LOD0 的骨骼编号直接充作 LOD1 合同。Windows 的 LOD0 限制不会应用到 Android。

所有启用的 Android 模型包都参与既有管线 LOD 偏置维护，继续受 `lod_pipeline` 设置控制。游戏入口虽名为 `EnableForceLOD0`，其作用是设置最高可用层级偏置，不是强制 Android 存在或使用 LOD0；该分支不写 `QualitySettings.maximumLODLevel`。独立武器和大招包也不能绕过这项维护，否则可能随距离切换到未替换的层级。

制作 Android 显式目标仍需对应资源、LOD1 donor 和顶点声明证据；Windows 草稿不自动添加 `android-arm64`。这项资料要求不表示 Android LOD1 路径不受支持。

Android 替换或隐藏部件时，在同一提交/恢复事务中处理其 `shadowProxyMesh`。独立 `SP_Mobile` 代理只有在原网格或实际 shadow mesh 引用、骨骼对象顺序和网格空间能够唯一证明归属时才关闭；有关但归属不明的代理会导致替换拒绝。未改动部件及无关代理保持原状态，不依靠名称猜测归属。

资源注册、匹配和缓存按资源隔离；文件仍使用全局组件编号，原生运行时仅在读取选定资源时将组件和 donor 引用重映射为局部编号。关闭普通模型校验不能绕过跨资源 donor、目标类型或平台边界。两个旧包仍沿用同角色互斥规则；涉及 1.4 包时按当前平台资源是否重叠判断，重复 package ID 仍冲突。

此开发分支已覆盖离线解析与构建回归。庄方宜大招、武器挂接、资源池复用及 Android 阴影仍需真实游戏验收；随附真实资源草稿只有 `keep` 规则，不是已完成的替换 Mod。

## 组件与 donor 作用域

`target.components` 是全包统一、连续编号的 1–64 项表。`component_rules[*].target`、`bones[*].component` 和 `draws[*].material_component` 都引用此全局编号，不能按 resource 重新从零编号。

每个组件必须声明 `resource`、`renderer_kind`、`renderer_path`：

- `resource` 引用资源表 ID。
- `renderer_kind` 为 `skinned` 或 `static`。
- `renderer_path` 是大小写敏感的精确 root-relative Transform 路径；空字符串表示根自身的 renderer。非空路径不得以 `/` 开头/结尾，不得出现反斜线、冒号、空段、`.` 或 `..`，不超过 1024 UTF-8 字节。
- 已知 `Mesh_all/lod0`–`Mesh_all/lod3` 根路径及其子路径必须与所属资源的 `lod` 一致。自定义路径、根 renderer 或阴影代理路径不根据名称猜测 LOD。
- 同一 resource 内 `mesh_name` 必须唯一，`renderer_path` 也必须唯一；不同 resource 可重复这些值。
- `original_index_count` 为正的三角形索引数，即能被 3 整除。
- `bone_names`、`materials` 分别不超过 65536、256 项；名称为 1–256 UTF-8 字节非空字符串。静态组件的 `bone_names` 必须为空。
- 1.4 不允许 `bone_name_aliases` 字段（包括空数组）：每个资源使用其自身实际 LOD donor 的骨骼名称，不再沿用旧 world/UI 别名。

每个 mesh descriptor 的全部骨骼 donor 和材质 donor 必须属于同一个 resource。替换该 mesh 的目标组件也必须属于该 resource，并且 `renderer_kind` 必须一致。骨骼 donor 必须来自蒙皮组件。材质 donor 可以来自同资源内任意 renderer 类型。

跨 prefab 共享几何通过共享 payload 实现：复制 mesh descriptor，保留相同 stream/index payload ID，将 donor component 引用调整到各资源自己的组件。禁止直接跨资源复用带另一资源 donor 引用的 descriptor。写入器仍按字节去重 payload，不计算文件产物哈希。

## 静态与蒙皮 mesh

每个 1.4 mesh 必须有 `renderer_kind`。

蒙皮 mesh 保留 1.2/1.3 的三条非空连续流规则：第三流为已支持的 4/12/32 字节 skin 声明，1–256 个骨骼，既有索引范围、权重及 palette 检查继续生效。

静态 mesh 对应原生 MeshRenderer + 同一 GameObject 的唯一 MeshFilter：

- 允许 1–3 条非空连续 stream，每条 stride 1–64 字节；attributes 必须恰好覆盖各 stride，不能有未声明的流或空洞。
- 禁止 semantic 12（blend weights）和 13（blend indices）。
- `bones` 必须为空。
- 索引、draw、材质、纹理及位置形变继续使用既有字段和验证。静态 mesh 不插入伪骨骼或空 skin 流。

离线图谱读取器通过序列化 GameObject 身份连接 MeshRenderer 与 MeshFilter，不按名称配对；缺失/多个 MeshFilter、未解析 mesh 或 additional vertex streams 会显式报错。空 bindposes 对静态 mesh 合法。HG 存储声明与 runtime vertex declaration 继续分开保留，不能由离线解析成功推断 runtime 等价。

## 制作与选择计划

`bem_tool.py pack/unpack/build/validate/bundle` 支持 1.4 项目，ZIP 可同时包含旧角色包与 1.4 角色/武器包。通用 EFMI `convert` 与其 recipe 仍使用旧角色目标；真实武器/大招须先建立显式、资源内的来源映射，再由可编辑工程打包。本规范不会把普通角色 profile 自动套用于大招或武器。Blender 插件尚不直接输出显式资源表或静态武器。

`build_bem14_target.py` 从 NativeAssetReader 原始图谱或新版离线 metadata，按作者 spec 的精确 prefab 身份及 LOD 分支生成未验证目标 profile，并可输出所有组件为 `keep` 的项目起点。它检查 snapshot 与平台来源，不猜测 runtime 布局或来源 Mod 映射。真实 Windows 草稿见 [庄方宜大招、静态剑、蒙皮法器](../../tools/CustomModel/profiles/bem14-drafts/README.md)，分别含 36、1、4 个组件；这些草稿的 `runtime_verified`、`conversion_ready` 均为 false。

独立制作工具中的等价入口为 `BetterEndfield.BemConverter.exe target-profile NATIVE_GRAPH.json --spec SPEC.json -o PROFILE.json --project PROJECT.json`。发行目录的 `examples/multi-resource/project/export.bemproj.json` 是打包阶段生成的可直接构建示例，无需最终用户运行 Python 生成器。

```powershell
python tools/CustomModel/examples/multi-resource/create_project.py research/custom_model/bem14-example
python tools/CustomModel/bem_tool.py build research/custom_model/bem14-example/export.bemproj.json
python tools/CustomModel/bem_tool.py validate research/custom_model/bem14-example/dist/synthetic.bem --resource weapon --platform windows-x64
```

`--resource` 与 `--platform` 仅用于 `inspect`/`validate` 的选中计划输出。`validate` 始终校验整个包所有资源及所有可达选项的几何/预算，然后返回筛选计划。筛选不修改文件，不重排全局组件或 mesh ID，只收集相关流、所选 draw、纹理及活动形变端点。平台与资源不匹配必须报错。

Python `bem_v14.selection_plan(...)` 与 `bem_v11.read_selected_payloads(..., resource=..., platform=...)` 提供同一资源筛选能力；后者仅解码筛选后的 payload 依赖。共享 bytes 仍只解码一次。滑条设置属于包的参数，同一参数可分别影响多个资源；当前 prefab 只访问自己的形变端点。

交叉语言合成测试输入生成命令：

```powershell
python tools/CustomModel/examples/multi-resource/create_project.py research/custom_model/bem14-fixtures --native-fixtures
```

输出包括正常的混合蒙皮/静态包、跨资源 donor 恶意包和降级头恶意包。示例覆盖不同资源同名 mesh、几何 payload 复用、平台子集、选项隐藏和各资源独立位置形变。它不加载游戏或设备，不构成运行时视觉验收。
