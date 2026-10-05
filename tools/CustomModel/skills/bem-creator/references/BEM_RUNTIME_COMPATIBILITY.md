# BEM 运行时行为与兼容性

本页下文说明旧 1.0–1.3 角色包的 world/UI 路径。开发分支新增的多资源、武器与形态目标见 [BEM 1.4 规范](BEM_V1_4_SPEC.md)：各资源使用显式接收器路径与平台声明；Windows 初版仅执行 LOD0 目标，Android 使用声明资源自身的 donor。真实新目标仍需平台资料和游戏内验收。

Android 场景使用 LOD1 是原有设计：旧包的 LOD0 指 UI donor，场景接收器仍是 LOD1；1.4 显式包可直接声明自身的 LOD1 接收器与 donor。Windows 的 LOD0 限制不会套到 Android。Android 所有启用模型包均参与既有 `lod_pipeline` 偏置维护，保留游戏的 QualitySettings；`EnableForceLOD0` 的函数名不表示手机必须使用 LOD0。

本文说明游戏加载 BEM 包时实际做了什么：怎样找到要替换的部件、对几何和贴图有哪些要求、加载开销、选择何时生效，以及日志里各类拒绝信息的含义。格式字段见 [格式规范](BEM_FORMAT_SPEC.md)，制作流程见 [创作者指南](BEM_CREATOR_GUIDE.md)。

Windows 与 Android 使用同一套原生读取与构建代码（`native/modules/custom_model`），差异只在场景模型、阴影和贴图格式三处，下文分别标注。

## 1. 替换发生的时机

- 游戏加载角色资源（`*_postmodel` 场景模型、`*_uimodel` 详情模型）时，模块在资源交给游戏之前**同步**构建替换网格和材质并一次提交。之后游戏实例化的所有副本都直接使用替换结果。
- 任何一步失败，都会恢复原绑定并交付原模型，游戏不会拿到半成品。
- 每个角色同时只能启用一个包。同一角色有两个启用包、或两个包的资源根重复时，这些包全部不生效，日志为 `Conflicting enabled package`。
- 资源名比较前会去掉 `(Clone)` 和 `#数字` 后缀，同一角色的场景副本都能识别。
- 开屏角色走场景模型（`*_postmodel`）的加载路径，按场景模型规则替换。

## 2. 部件匹配

`target.components` 里的**每个**部件（包括 keep 和 hide）都必须在资源里找到唯一的对应 Renderer，否则整包不生效。

匹配条件：
1. Renderer 位于资源根下的 `Mesh_all/lod0/`。
2. 它当前使用的 Mesh 的**完整名称**等于 `mesh_name`。`_lod0_20`、`_8` 这类后缀是名称的一部分，不会被去掉；大小写也不归一化。
3. 该 Mesh 所有子网格的索引数之和等于 `original_index_count`（子网格不超过 256 个）。
4. 材质数 1..256 且都有效；骨骼不超过 256 根，并且全部在资源根下。

补充说明：
- Renderer 自身的名字不参与匹配。Renderer 名和 Mesh 名不同的部件也能正常匹配。
- 满足条件的 Renderer 有两个及以上时视为歧义，拒绝；两个部件指向同一个 Renderer 也拒绝。
- 游戏更新改变了 Mesh 名或索引数时，旧包会被拒绝，需要用新的角色资料重新导出。

## 3. LOD

**Windows**：场景模型和详情模型都只替换 LOD0。启用任意包时，模块会强制使用 LOD0（`EnableForceLOD0`、`QualitySettings.maximumLODLevel=0`，并调高 NPC 人群的 LOD 距离），全部停用后恢复原设置。强制失败时日志为 `LOD prerequisite unavailable`，交付原模型。没有启用包时，可以在管理页单独开启“锁定高精度 LOD”。

**Android**：手机上的场景模型使用 LOD1，替换数据从详情模型的 LOD0 构建：
1. 先准备（或复用）同一选择下的详情模型 LOD0 替换结果。
2. 详情模型的 `Mesh_all/lod0/X_lod0` 对应场景模型的 `Mesh_all/lod1/X_lod1`。详情 Renderer 名不以 `_lod0` 结尾的部件不能映射，日志为 `unsupported UI receiver path`。
3. 严格校验时，场景 LOD1 的 Mesh 名必须**恰好**是 `X_lod1`；名称带其他后缀的角色会被拒绝（`world mesh identity differs`）。关闭模型校验后，会在同一角色、资源根和 LOD 区域内尝试受限的 `_8` / `_20` 后缀匹配；候选不唯一或资源结构不符仍会拒绝。
4. 详情与场景 Renderer 的局部空间必须一致，否则拒绝（`Android world mesh space differs`）。
5. 骨骼按相对资源根的完整路径映射到场景骨架；路径找不到时，只在同一父节点下尝试包里声明的骨骼别名。名称必须与包一致。
6. 详情模型与场景模型在同一事务中提交，失败时一起回滚。

手机上的 LOD 偏置由安装器默认开启，不修改 `QualitySettings`。

## 4. 阴影（Android 场景模型）

- 被替换的部件改为自身投影（`ShadowCastingMode.On`），不再使用阴影代理网格。
- 被替换或隐藏部件对应的 `Shadow_Proxy/SP_Mobile/*` 代理会被关闭。对应关系先按“代理原 Mesh 与该部件 LOD1 原 Mesh 是同一个对象”判断，再按路径 `Shadow_Proxy/SP_Mobile/X_shadowProxyMobile` 判断。
- 一个代理同时对应多个部件，而其中有被改动的部件时，拒绝替换（`ambiguous mobile proxy owner`）。
- 只保留（keep）的部件和 `SP_Desktop` 不受影响。

## 5. 几何与蒙皮

| 项 | 要求 |
| --- | --- |
| 顶点声明 | 运行时按包内声明构建新网格，不要求与原 Mesh 布局一致；构建后回读声明和 stride，必须与包一致 |
| 法线与切线 | 不重新计算，使用包里的数据 |
| bindpose | 包里没有 bindpose，使用原 Mesh 的 bindpose，数量必须大于最大骨骼索引。几何要按原 Mesh 的空间和 bindpose 制作 |
| 每顶点骨骼影响数 | 不得超过原 Mesh 的原生设置（1、2 或 4）。原 Mesh 是刚性蒙皮（1）时，替换也必须是刚性 |
| 权重 | 每个顶点至少一个非零影响，权重和为 1±0.01 |
| 跨部件借骨骼 | 借用的骨骼取自 donor 部件原 Renderer 的骨骼和原 Mesh 的 bindpose；donor 与目标必须在同一 Mesh 空间 |
| 骨骼名 | 运行时骨骼名必须等于包内名或已声明的别名 |
| 新 Mesh 名称 | 沿用原 Mesh 名称 |

## 6. 材质与贴图

**材质**
- `replace` 的每条 draw 复制指定的原材质（名称必须一致），保留原 Shader、关键字和数值参数。
- `keep` 保留原网格，但整组材质会被复制；`material_overrides` 只替换指定槽位的贴图。
- `hide` 关闭该 Renderer，原网格不变。
- Android 场景模型直接复用详情模型准备好的材质和网格。keep 覆盖要求两边材质同名，或符合 `M_actor_X` 对应 `M_actor_lod_X` 的关系。

**贴图替换**：在复制出的材质里，按 `original_name` 查找使用该原贴图的属性。
- 多个属性指向同一个原贴图对象：全部替换。
- 不同的贴图对象同名：判为歧义，拒绝。可以改用 1.2 贴图槽，或换一个 donor。
- 找不到对应属性：拒绝（`Texture name pin missing`）。
- 新贴图复制原贴图的采样设置（wrap、filter、各向异性、mip bias）；颜色空间由 `srgb` 决定；不重新生成 mip。

**平台格式**
- Windows：使用 BC1/BC3/BC4/BC5/BC7、RGBA32、R8。
- Android：设备不支持的格式会被拒绝（`Texture format unsupported by game backend`）。多数手机不支持 BC 格式，需要在安装页点“转换纹理”：
  - 支持 ASTC 的设备：颜色贴图转 ASTC 6×6，线性数据转 ASTC 4×4；不支持 ASTC 的设备转 RGBA32。R8 和已经是 ASTC 的贴图不转换。
  - 转换后单张不超过 8192 像素且不超过 64 MiB；超出时丢弃最高级 mip，单级贴图则按 2 的幂缩小。被缩小的贴图在报告里记为 `android_install_mip_skip`。
  - 法线贴图必须能确定编码：`semantic:"normal"` 加 `normal_encoding`，或命中内置法线规则（目前只覆盖部分角色的 `_N` 贴图）。否则会提示“此包缺少已确认的法线贴图编码信息”。`xy-unorm` 会重建 Z 分量。

## 7. 加载开销与显存

**上传量按解压后的贴图大小计算，和 BEM 文件大小、压缩率无关。** 常见单张贴图（含完整 mip 链）：

| 尺寸 | RGBA32 | BC7 / ASTC 4×4 | ASTC 6×6 |
| --- | --- | --- | --- |
| 2048² | 约 21 MiB | 约 5.3 MiB | 约 2.4 MiB |
| 4096² | 约 85 MiB | 约 21 MiB | 约 9.5 MiB |
| 8192² | 约 341 MiB | 约 85 MiB | 约 40 MiB |

参考：一套 7 张 8K BC7 贴图的包，每次加载要上传约 448 MiB。编队里多个角色同时加载时，这些开销会叠加。

对创作者的建议：
- 8K BC7 单张约 85 MiB，超过 1.0/1.1 的单张 64 MiB 上限，只能用 1.2 及以上；手机安装时也会被自动缩小。
- 面向手机的包优先使用 4K 或 2K 贴图。
- 不需要替换的贴图不要放进包里，未替换的槽位继续使用原贴图。

**加载模式**
- 默认是低峰值模式：每上传约 4 MiB 贴图就和渲染线程同步一次，降低瞬时内存和显存峰值。
- 打开“加载速度优先”后改为每 128 MiB 同步一次，加载更快、峰值更高，适合内存充足的设备。
- 两种模式都在同一次资源交付内完成，日志为 `Model loading mode=...`。该选项需要重启游戏生效。

**其他**
- 贴图数据逐张解压，上传后立即释放 CPU 副本。
- 同一次加载中重复引用的贴图只上传一次。同一角色已显示的模型仍在使用相同贴图时，详情页重建会直接复用。
- 没有空闲缓存：关掉的详情页再次打开时会重新加载。

## 8. 选择何时生效与热切换

- **默认**：启用/停用、外观、选项、滑条的修改都在**下次启动游戏**后生效。
- **实验性热切换**（需要先开启，并重启一次游戏）：
  - 之后修改选择会在下一次资源交付时生效，例如切换配队、重新打开详情页。已经在场景里显示的模型实例也会重建。
  - 停用包会恢复原模型。为此，显示替换模型期间会保留原 Mesh，内存略有增加。
  - 原资源已被游戏卸载时，重建会被拒绝（`Hot switch Original ... unavailable`），重新进入场景或重启游戏即可。
  - 热切换、关闭模型校验、加载速度优先这三个开关本身都只在重启后生效。
  - 未开启热切换时加载的模型，开启后也不能直接切换，需要重启一次。
- 1.3 滑条不会随拖动逐帧更新，按上面的规则在下一次交付或重启后生效。

## 9. 第一人称兼容

第一人称模式会隐藏头部附近的几何，避免挡住镜头。判断依据是**骨骼权重**，名称只用于缩小扫描范围。

- 归为“头部”的骨骼：`Bip001_Head` 子树、尾巴链（`tail`、`tail_*`、`bip001_tail*`），以及角色资料中登记的头饰骨骼（例如 `maozi_*`）。Neck 不算头部。
- 一个部件全部权重都在头部骨骼上时，整个部件改为只投影：不可见，但保留影子。
- 头部与身体混在同一网格时，只裁掉三个顶点的全部非零权重都在头部骨骼上的三角形。任何颈部、躯干、衣服权重都会让这个面保留。

制作建议：
- 头发、头饰、耳朵只绑定到 Head 子树或尾巴骨骼，第一人称时才能正确隐藏。
- 不要把身体或衣服的顶点绑到 Head 上，否则第一人称时会被裁掉。
- 替换网格不超过 200,000 顶点、6,000,000 索引、64 条 draw 时，可以精确裁剪；超出时使用通用回退。

## 10. 关闭模型校验（实验）

关闭后跳过兼容性校验，例如能力声明、上限、骨骼名、索引数、Mesh 空间、权重、影响数，以及 Android 提交后的回读。容器边界、payload 完整性、贴图格式、蒙皮布局和资源根/接收器边界仍然检查。Android 可以在同一角色、资源根、组件路径和 LOD 区域内使用受限的 `_8` / `_20` Mesh 名回退；候选不唯一时仍拒绝。找不到的贴图保留原贴图，同名的多个贴图会全部替换。

这个选项只用于开发测试，可能导致渲染错误或游戏崩溃。日志会出现 `Developer mode: model validation disabled`。

## 11. 日志拒绝信息对照

日志位置：Windows `%LOCALAPPDATA%\BetterEndfield\logs\BetterEndfield.log`；Android 为框架模块日志。

**包与配置**

| 信息 | 原因 | 处理 |
| --- | --- | --- |
| `Refused non-BEMv1 package` | 配置里的文件不是 `.bem` | 只导入 `.bem` |
| `Package refused: Mod.x: …` | 读取包失败，后面跟具体原因 | 按原因修正 |
| `Conflicting enabled package` | 同一角色启用了多个包 | 只保留一个启用 |
| `Appearance removed; using package default` / `Parameters removed or invalid` | 保存的选择在新版包里已不存在 | 重新选择 |
| `Only BEM 1.0/1.1/1.2/1.3 packages are supported` | 文件头或版本不对 | 用当前工具重新导出 |
| `Invalid BEM file size` / `Trailing or missing BEM bytes` / `Invalid payload …` | 文件损坏或超出容器上限 | 重新导出、重新下载 |
| `Unsupported required capability` / `… capability mismatch` | 能力未声明、未知或与版本不符 | 用 `pack` 重新打包，工具会补齐能力 |
| `Unsupported target platform` | `target.platform` 不是 `windows-x64` | 保持 `windows-x64` |

**结构与上限**

| 信息 | 原因 | 处理 |
| --- | --- | --- |
| `Target has multiple/no selected operation` / `Unreachable option combination` | 组合规则不唯一或不完整 | 修正 `component_rules` 或 `selection_constraints` |
| `… exceeds decoded payload budget` / `… runtime memory budget` | 选中内容超过版本预算 | 缩小贴图，或改用 1.2 |
| `Appearance exceeds N texture bindings` / `Texture exceeds N MiB` | 贴图数量或单张大小超限 | 合并、缩小贴图 |
| `Unsupported texture format` / `R8 texture must be linear` / `Invalid texture dimensions` | 贴图格式或尺寸不合法 | 换成受支持的格式 |
| `Invalid geometry counts/index type` / `Selected draw limit exceeded` / `Draws must partition IB` | 顶点、索引、draw 超限，或 draw 没有覆盖整条索引流 | 拆分部件、修正 draw |
| `Unsupported skin layout/declaration` / `Declaration/stride or skin indices differ` | 蒙皮流不是允许的三种布局之一 | 按格式规范第 7 节导出 |
| `Skin index outside palette` / `Bone identity differs` / `Invalid skin weight sum` | 骨骼索引越界、骨骼名不符、权重未归一 | 修正骨骼映射，归一化权重 |
| `Texture slot candidates must replace one original texture` | 一个贴图槽对应了多张原贴图 | 拆成多个槽 |

**游戏内构建**

| 信息 | 原因 | 处理 |
| --- | --- | --- |
| `Generic model donor is missing/ambiguous: <mesh_name>` | 找不到唯一匹配的原部件（第 2 节） | 核对 `mesh_name`、`original_index_count` 是否对应当前游戏版本 |
| `Merged palette donor missing or mesh spaces differ` | 借用的骨骼来源部件缺失或不在同一空间 | 只从同一空间的部件借骨骼 |
| `bindpose palette too small` | 原 bindpose 数量不足 | 检查骨骼表 |
| `source native skin field cannot represent replacement influences` | 每顶点影响数超过原 Mesh 设置 | 减少每顶点骨骼数 |
| `invalid skin weights at vertex` | 运行时解码出非法权重 | 归一化权重 |
| `Texture name pin missing` | 材质里没有 `original_name` 对应的原贴图 | 核对贴图名和 donor 材质 |
| `Ambiguous v25 texture name pin` | 多个不同的原贴图同名 | 改用贴图槽或换 donor |
| `Duplicate texture slot assignment` | 同一原贴图被赋值两次 | 去掉重复 |
| `Texture format unsupported by game backend` | 设备不支持该贴图格式（多见于手机上的 BC 格式） | 在安装页“转换纹理” |
| `Android world adapter refused: …` | 手机场景模型映射失败，后面跟具体原因 | 见第 3、4 节 |
| `Android world bone path missing` / `bone name differs from package` | 场景骨架路径或名称不同 | 声明骨骼别名 |
| `LOD prerequisite unavailable` | Windows 强制 LOD0 失败 | 通常是游戏更新导致，需要更新程序 |
| `Resource preparation failed; original retained` | 准备阶段失败，前面的日志有原因 | 查看前几行 |
| `Hot switch configuration rejected` / `Hot switch flags changed; restart the game` | 热切换期间配置无效，或修改了需要重启的开关 | 重启游戏 |
