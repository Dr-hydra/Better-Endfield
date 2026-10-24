# BEM 运行时行为与兼容性

当前 BE 4.0.3 / Windows 游戏 1.6.3、Android 游戏 1.6.4 的定向检查与实机边界见 [BEM 链路核查](research/1.6.3/bem-chain-audit/BEM_CHAIN_AUDIT.md)。Windows 已用现有凛包验证真实开屏提交；手机缺语言资源，本轮只完成静态与离线检查。

本页下文说明旧 1.0–1.3 角色包的 world/UI 路径。当前版本的多资源、武器与形态目标见 [BEM 1.4 规范](BEM_V1_4_SPEC.md)：各资源使用显式接收器路径与平台声明；Windows 仅执行 LOD0 目标，Android 使用声明资源自身的 donor。真实新目标仍需平台资料和游戏内验收。

Android 场景使用 LOD1 是原有设计：旧包的 LOD0 指 UI donor，场景接收器仍是 LOD1；1.4 显式包可直接声明自身的 LOD1 接收器与 donor。Windows 的 LOD0 限制不会套到 Android。Android 所有启用模型包均参与既有 `lod_pipeline` 偏置维护，保留游戏的 QualitySettings；`EnableForceLOD0` 的函数名不表示手机必须使用 LOD0。

### BEM 1.4 的两端资源路径

- Windows 与 Android 都按当前平台展开 `target.resources`，使用每个组件声明的精确路径与 renderer 类型；同名 Sprite/Texture2D 不进入模型 API。
- 蒙皮大招使用所属 prefab 的骨骼和材质 donor；静态武器使用 MeshRenderer 与同对象的唯一 MeshFilter，不插入伪骨骼。Android 显式资源不借普通角色的 UI donor。
- Android 1.4 首次启用发现只处理声明给 `android-arm64` 的显式资源，按包中 LOD 与接收器路径校验后重绑；Windows 仍限制显式 LOD0。旧 Android 包继续使用 UI LOD0→world LOD1 适配，未将其替换成显式资源算法。
- 1.4 冲突按当前平台资源根判断。没有当前平台资源的包不能导入；纹理转换不改变平台资源声明。
- 武器、大招加载及 PC 热切换已有用户实机验收；Android 接线和回归不等于手机渲染验收。Windows 样包不能仅补平台标签就作为 Android 已验证样包。

下文第 1–6 节的 world/UI、LOD 与骨骼说明主要针对旧角色包；1.4 的具体资源合同以扩展规范为准。

本文说明游戏加载 BEM 包时实际做了什么：怎样找到要替换的部件、对几何和贴图有哪些要求、加载开销、选择何时生效，以及日志里各类拒绝信息的含义。格式字段见 [格式规范](BEM_FORMAT_SPEC.md)，制作流程见 [创作者指南](BEM_CREATOR_GUIDE.md)。

Windows 与 Android 使用同一套原生读取与构建代码（`native/modules/custom_model`），差异只在场景模型、阴影和贴图格式三处，下文分别标注。

## 1. 替换发生的时机

- 游戏加载角色资源（`*_postmodel` 场景模型、`*_uimodel` 详情模型）时，模块在资源交给游戏之前**同步**构建替换网格和材质并一次提交。之后游戏实例化的所有副本都直接使用替换结果。
- 任何一步失败，都会恢复原绑定并交付原模型，游戏不会拿到半成品。
- 每个角色同时只能启用一个包。同一角色有两个启用包、或两个包的资源根重复时，这些包全部不生效，日志为 `Conflicting enabled package`。
- 资源名比较前会去掉 `(Clone)` 和 `#数字` 后缀，同一角色的场景副本都能识别。
- 开屏角色走场景模型（`*_postmodel`）的加载路径，按场景模型规则替换。
- 普通角色的非编队 NPC 若使用同名 `*_postmodel`，且在外层 prefab 下有唯一的同名模型子节点，可以复用同一个 BEM。运行时定位实际模型根后继续执行原有网格、材质、骨骼和 LOD 校验，资源仍以原来的外层 prefab 交给游戏；加载登记及关闭恢复使用实际模型根。无需另做或另导入一份 NPC 模型包。
- 包装层有歧义、缺少必要方法或部件校验失败时保留原模型。该适配不改变 BEM 1.4 显式路径的根。2026-10-10，Android aglina 非编队 NPC 使用凛 1.2.1 BEM 的替换已通过用户实机测试；其他 NPC 仍需相应资源结构与实机验证。

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
  - 之后修改选择由游戏帧调度消费，更新已登记模板与场景实例；不必等待切换配队或重新打开详情页。PC 还发现首次启用时未登记的缓存资源；Android 1.4 也对显式资源执行这项发现。后续正常资源交付仍可应用最新选择。
  - 停用包会恢复原模型。为此，显示替换模型期间会保留原 Mesh，内存略有增加。
  - 原资源已被游戏卸载时，重建会被拒绝（`Hot switch Original ... unavailable`），重新进入场景或重启游戏即可。
  - 热切换、关闭模型校验、加载速度优先这三个开关本身都只在重启后生效。
  - 未开启热切换时加载的模型，开启后也不能直接切换，需要重启一次。
- 1.3/1.4 滑条通过选择变化触发重建，不是逐帧顶点动画；开启热切换时按上述调度应用，否则下次启动生效。

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

日志位置：Windows `%LOCALAPPDATA%\BetterEndfieldNext\logs\BetterEndfieldNext.log`；Android 为框架模块日志。

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

## 实验：克隆模型支持

PC 与 Android 模型管理页提供独立的“实验：克隆模型支持”开关，默认关闭，重启游戏后生效。PC 的配置项为 `[CustomModel] clone_support=false`；Android 保存为 `bem_clone_support`，启动和热更新均传递同一会话值。热切换、加载速度优先和关闭模型校验仍各自独立。

开启后，模块监听已登记模型来源的实例化，保存生成时的来源与包选择代次。模板交付仍同步完成，不因开启 Clone 支持切换为分帧异步。已完整继承结果的实例保留自己的材质对象和参数，不重复上传模型；需要重新准备的实例使用记录中的原版数据，并将骨骼按资源根内路径映射到自身。

克隆实例的独立材质对象不会单独导致拒绝。未知 Mesh、原版数据已释放、来源声明变化、接收器歧义、缺失或重复的必要骨骼仍拒绝，保留当前绑定。识别依赖真实来源和对象身份，`(Clone)` 名称仅用于资源路由。Android 旧角色包继续按 UI LOD0 → world LOD1 的原有适配保存来源；显式包沿用自身资源路径。

同一个已完成接收器重新持有记录中的原版 Mesh 时，会重新准备当前选择，修复资源二次交付后世界模型保持原版的问题。此修复始终启用，不依赖实验开关。

目前已通过离线生命周期回归及两端 Release 构建；大招状态下传送、对象池复用、NPC 包装根和真实私有材质变化仍需实机验收。


## BEM 1.5 材质覆盖运行时行为与容错规则

BEM 1.5 引入的 `material-overrides-v1` 特质在游戏运行时具有以下确定的生命周期与错误隔离行为：

### 1. 作用域隔离与材质克隆
- 游戏为当前角色部位准备网格与材质时，从原始角色 Renderer 的共享材质中提取模板，并通过 `UnityEngine.Object.Instantiate` 进行实例克隆。
- 所有 `material_overrides`（浮点、颜色、向量、着色器关键字）均只注入到当前克隆实例上，绝不修改共享全局材质，确保切换配队或停用 Mod 时材质状态不被跨场景污染。

### 2. 注入时序与属性降级
- 材质属性覆盖在网格装配阶段逐个 DrawCall 依次执行：
  - `Material.SetFloat(name, value)`
  - `Material.SetColor(name, color)`
  - `Material.SetVector(name, vector)`
  - `Material.DisableKeyword(keyword)`
  - `Material.EnableKeyword(keyword)`
- 若目标原生 Shader 未定义某个重载属性，底层引擎会自动忽略该属性写入，不会引发渲染中断或异常退出。

### 3. 「实验：关闭模型校验」联动
开启「实验：关闭模型校验」（`skip_validation=true`）时：
- 放宽命名与格式限制：允许属性名缺少 `_` 前缀，放宽关键字大小写要求；
- 数值饱和截断：超出范围的浮点数与颜色值自动截断（Clamp）至合法边界，不拒绝模型包加载；
- 扩展属性配额：单个 DrawCall 的重载项上限放宽至严格模式的 4 倍。

### 4. 根骨骼位移变换与防漂移回滚（root-offset-v1）
- **基准锁定（Pristine Baseline）**：模型初次挂载时，系统捕获指定骨骼节点的原生 `localPosition` 存入 `visual_pristine`；所有换装与形态切换计算均以此绝对基准为底数累加，避免多次换装导致高度累加。
- **卸载还原**：在停用 Mod 或切换到无偏移的模型包时，精确将 Transform 的本地坐标恢复至 `visual_original`。


## BEM 1.5 动态时钟驱动器运行时行为（dynamic-drivers-v1）

本节记录 `drivers` 在当前开发树中的实际行为。它是实现边界说明，不是完整承诺；目标语义见[格式规范第 13 节](BEM_FORMAT_SPEC.md)。

### 1. 挂载与时钟来源
- 驱动器在 **Windows 与 Android 两端**都运行。PC 端在渲染线程的 `ResourcePump` 中每帧调用一次 Tick；Android 端由同一个 pump 在 Unity 线程经原生桥驱动。
- Δt 取 `GetTickCount64()` 的相邻帧差，首帧按 16 ms 处理，单帧上限截断为 0.1 s，避免卡顿或断点后相位跳变。
- `loop` 下相位按 `phase = (phase + Δt × base_frequency × speed_multiplier) mod 1` 累加，再按 `waveform` 映射到 0..1 的归一化值。
- `once` 下相位同样推进，但**钳制在 1**。到达后驱动进入「已结束」，仍每帧投递 phase = 1，因此末帧保持住、不回弹。时长 = `1 / (base_frequency × speed_multiplier)` 秒。

### 2. 加载与切换
- 目标结构由 `target` 自身决定：带 `resources` 走 1.4 显式资源目标，不带就用 `world_resource` / `ui_resource`，与头部次版本无关。因此 1.3 结构的包只要声明 `dynamic-drivers-v1` 就按 1.5 接受，不需要重建为 1.4 结构。
- 启用包清单里出现 `drivers` 时，运行时在需要它的第一帧就地解析并安装；包路径与当前已加载路径相同就不再重读（没有驱动的包也会记住路径，不会每帧重新打开文件）。
- 清单解析失败记 `[AnimDriver]` 日志并保持无驱动状态，5 s 后重试；不影响模型替换本身。
- 只有一份驱动宿主：启用列表里第一个真正带驱动的包生效，处于暂停的驱动集不会被下一个包顶掉。
- 侧车 `.anim.json` 兜底已删除，只认清单内化的 `drivers`。

### 3. 目标分发
- **材质 float / color / keyword**：按 `component` + `material_slot` 定位已装配的克隆材质，逐帧调用 `material.set_float` / `set_color` / `enable_keyword`。属性名按第 12 节正则校验；槽位越界或组件不存在时不写入，并记一次日志。
- **形态（Windows）**：按参数名匹配动态网格通道，在该参数相邻的 `mesh_deformations` 帧之间插值出每顶点绝对偏移，加到加载时捕获的基准顶点位置上再上传。存在两条等价路径，可在悬浮窗运行期切换：
  - `channel`（默认）：重建整条 Position 通道，通过 `SetNativeArrayForChannelImpl` 上传。跨平台，也是 Android 唯一可用的路径；
  - `byte_range`：只重写发生位移的顶点区间，通过 `InternalSetVertexBufferData` 上传。
- **形态（Android）**：同样的「基准 + 偏移」计算，但只经 `Mesh.SetNativeArrayForChannelImpl` 上传。`InternalSetVertexBufferData` 与 `MarkModified` 未被 Android 的 Unity 导出，因此该端不存在区间写入路径。
- 两条路径都不触碰 Stream 1（法线/切线/UV）与 Stream 2（骨骼），因此着色与蒙皮保持原样。必须守住的不变量是：**绝不在这类原生多流网格上调用 `Mesh.AddBlendShapeFrame`**，它会重组内部流并破坏着色。
- 未匹配到目标时该绑定静默跳过，不报错、不中断其余绑定。

#### 重要限制：不要同时把动画和滑条绑定到同一组顶点

同一参数的动画驱动与手动滑条不应同时作用于同一批顶点，实测会出现以下现象：

- 动画播放后原位置仍有残留几何，残留姿势随滑条值变化；
- 动画过程中模型表面闪烁、看起来发虚；
- 不同道具之间表现不一致（静止姿势是否露出体表决定了残影是否可见）。

根因：滑条变更在加载期按所选刻度重建网格并整条上传，驱动则在运行期每帧改写位置分量。两者共同作用于同一组顶点时，运行期写入会与重建时上传的基准姿势相互覆盖。

推荐做法：

- 每个参数只保留一种控制方式（驱动播放或手动滑条），不要在两者间反复切换；
- 需要「滑条定基准 + 动画往复」时，拆成两个参数并各自绑定互不重叠的顶点区间；
- 不同顶点区间上的不同参数（例如道具 A 与道具 B）可以各自独立控制，互不影响。

### 4. 播放状态与控制

每个控制器有四个状态：`stopped`、`playing`、`paused`、`finished`（最后一种仅 `once`）。暂停与已结束都**继续投递**冻结或末帧的采样值，只有停止才释放参数。

| 入口 | 可用操作 | 平台 |
| --- | --- | --- |
| 悬浮窗「动效」分区 | 开始 / 暂停 / 停止 + 速度滑条 | Windows、Android |
| 管理器 GUI 配置的快捷键 | 开始 / 结束 二元切换（无暂停） | 仅 Windows |
| `runtime.ini` `drivers=` | `id:play` / `id:pause` | 双端，无面板时的兼容入口 |

- `runtime.ini` 的 `[Mod.*]` 段 `drivers=控制器ID:play&控制器ID:pause` 仍可读，作为无面板时的入口；只有文本变化时才重新应用，不会每帧覆盖面板设置。
- PC 悬浮窗的模型详情页已有「动效」分区：每个控制器一行，含开始、暂停、停止三个按钮与一条 0.25x~4.00x 速度滑条。改动经共享内存回写运行时，**当次会话有效**，不写回 `runtime.ini`，也不改包内默认值。
- Android 的 GameOverlay 设置页提供同样的三按钮布局与速度滑条，经 `NativeCommandBridge.setCustomModelDriver` 转发。该桥只承载播放状态与千分比速度，会话语义与 PC 一致。
- **桌面快捷键**由管理器 GUI 按控制器配置，保存在 `runtime.ini` 的 `[Mod.<包id>]` 段 `driver_hotkeys`。它只做开始/结束切换，在游戏前台时以只读方式轮询，不拦截游戏输入；由悬浮窗进程执行，所以面板隐藏时依然有效，也不依赖管理器保持运行。
- 停用 Mod 或卸载模型时，材质属性按第 13 节的恢复规则交还（静态覆盖值，或绑定的下界），形变回放加载时采样的帧权重。停止、换包、模型卸载与模块退出走同一条路径；暂停与已结束不走。

### 5. 验证边界
- 已在真实包上核对形态通道与差量（`vag_morph 1637`、`anal_morph 180`、`dildo_depth 6358`、`anal_plug_depth 1434`，与 Python 解包逐项吻合），单元测试覆盖通道合并、刻度域映射与帧插值。
- Android 已实机验证：日志 `Android position upload: vertices=119860 touched=6358 maxDelta=0.007353` 证明通道上传真正到达 GPU，两个驱动可并行且相位交替。
- 多驱动并发、热切换中途的相位连续性、以及大量顶点通道下的帧耗尚未系统测量。
- 关键字恢复存在已知限制：没有静态覆盖时一律关闭（见格式规范第 13 节）。
- 形态驱动与手动滑条共存于同一组顶点时会出现原位置残影、闪烁与发虚（见上文「重要限制」）；该组合不属于受支持用法。
