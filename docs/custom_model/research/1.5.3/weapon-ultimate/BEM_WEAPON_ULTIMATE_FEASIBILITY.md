# BEM 武器与庄方宜大招形态可行性调研

日期：2026-10-05。源码基线：`3510fa7`，软件 3.5.1。工作区：`G:\Better Endfield`。

本文保留调研时基线的判断，下文“当前”均指该基线。后续开发分支 `dev/bem-1.4-weapons-forms` 已实现协议、解析器和运行时接线，见 [BEM 1.4 规范](../../../BEM_V1_4_SPEC.md) 与 [开发验证记录](BEM_V1_4_DEVELOPMENT_VALIDATION.md)；游戏内验收仍未完成。

## 结论

**现有 BEM 1.3 可以复用容器和大部分蒙皮几何能力，但不足以完整表达“角色普通形态＋大招主体＋镜像＋武器”，现有运行时也没有接通这些目标。** 建议增量设计 BEM 1.4，保留原有 payload、纹理、选项和位置形变机制，增加多资源目标、静态 Renderer 与平台绑定规则；继续读取旧包。

| 对象 | 离线证据 | 1.3 数据表达 | 当前替换路径 |
| --- | --- | --- | --- |
| 庄方宜普通场景／详情 | 既有角色资源，各 12 个 LOD0 SMR，包含特效部件 | 既有支持范围；正式目录筛选范围更窄 | 已有路径 |
| 庄方宜大招主体与技能实体 | 两个独立 prefab，各 12 个 LOD0 SMR；Mesh、骨骼相对路径／顺序、材质槽均相同 | 蒙皮网格、材质与贴图结构可复用；不能默认复用普通形态的映射 | Windows 通用资源入口可扩展；现有普通角色包不会命中这些根 |
| 庄方宜大招镜像 | 第三个独立 prefab，12 个 LOD0 SMR，另一组网格／骨骼契约 | 需要独立目标与映射，不能作为主体根的简单别名 | 现有两资源根契约容纳不了完整组合 |
| 样本法器 `wpn_funnel_0014` | 15 个 SMR，LOD0 4 个，骨骼数 1 或 3 | 具有可复用的蒙皮路径，但序列化骨骼索引为 UInt32×1；实际运行时声明／转换仍需验证 | 目标注册、单资源身份、武器目录和平台绑定缺失 |
| 样本剑 `wpn_sword_0014` | 4 个 MeshRenderer＋MeshFilter，LOD0–3；无 bindpose、无蒙皮通道 | **当前替换几何协议不支持**，不是缺少一个武器 ID | 当前运行时只枚举 SMR，也没有 MeshFilter 提交／恢复分支 |

这里的“可扩展”是源码与离线资源层面的可行性，不代表已验证游戏内替换。此次未运行游戏、未安装设备包、未修改正式解析器或运行时代码。

## 一、庄方宜并非只有一张大招网格

从本机 `E:\Endfield Game` 的 StreamingAssets＋Persistent VFS 覆盖层定向提取了 8 个目标及 133 个依赖 bundle，无缺失 bundle。当前 manifest 为 `2954fa80-23c1-1579-2b22-4ecfd6d70418`。

普通角色资源之外，需要区分：

| 资源根 | ModelTable.type | usePersistentPool | LOD0 SMR |
| --- | --- | --- | --- |
| `chr_0030_zhuangfy_ult_postmodel` | 2 | true | 12 |
| `abilityentity_chr_0030_zhuangfy_ult_postmodel` | 4 | true | 12 |
| `abilityentity_chr_0030_zhuangfy_ult_mirror_postmodel` | 4 | true | 12 |

前两项的 12 个组件通过现有 `paired_resource()` 比较：Mesh 对象身份、骨骼相对路径与顺序、材质对象／槽位均一致，引用完整，比较无差异。这提供了共享几何 payload 的依据，骨骼 Transform 仍须按每个接收资源重新绑定，不能共享实例骨骼对象。

镜像是独立契约。例如大招主体使用 `S_actor_zhuangfy_body_02_lod0`、`body_03`、`hair_02`、`vfxbody_*`；镜像使用 `body_04`、`cloth_03/04`、`hair_03`、`vfxpart_*`。普通形态使用 `body_01` 等。因此现成普通庄方宜 BEM 不能仅通过增加资源别名就自动覆盖全部大招表现；作者网格需要分别适配各目标的骨架、bindpose、材质与部件划分。

本次解析得到 233 个 SMR，其骨骼、Mesh、材质／纹理引用均通过离线检查。**运行时顶点声明观察数为 0。** 角色序列化通道含 HG 标志位，不能把离线存储声明直接当作最终 GPU／运行时声明。

另有 `abilityentity_chr_0030_zhuangfy_sword_postmodel`。本次解出的层级只有根、`VBhit` 和 `Battleshape`，没有 SMR、MeshRenderer 或 MeshFilter。它不能被直接当作可替换飞剑网格；可见飞剑效果还需沿技能／特效资源引用继续定位。此次没有完成其 VFX 视觉链路解析。

技能二进制中可观察到 `UltimateSkillHenshin`、大招实体、镜像实体和 `ult_postmodel` 引用；`ModelTable.json` 给出精确 prefab 路径与池配置。现有通用 JSON 解码器对相关 SkillData/BuffData 只产生字符串／元数据摘要，**尚未结构化还原技能事件时序**，不能凭这些字符串声称已证明创建、隐藏、切换、回收的完整顺序。

## 二、BEM 1.3 的边界

### 能复用的部分

- 容器、压缩 payload、索引与顶点缓冲、原材质 donor、纹理／纹理槽。
- 蒙皮 palette 与按原资源身份绑定的骨骼；大招样本 LOD0 的单部件骨骼数在现有 256 上限内。
- 作者选项、keep／hide／replace 和位置增量。
- 对独立资源先构建替换，再由游戏原本的动画／技能机制使用它们的思路。

1.3 的 `parameters`／`mesh_deformations` 只在同一网格上做位置增量，保留原法线、切线、UV、权重与骨架。选项谓词引用作者选项，不读取游戏“正在开大”的状态。**不要用一个形态滑条替代游戏的大招状态机。** 本阶段更合理的是替换相应资源，继续由游戏决定何时切换。

### 不能完整表达的部分

1. 单包只有一个 `character_id`、一对 `world_resource`／`ui_resource` 和一份完整组件表。三类大招资源加普通场景／详情已经超过这一模型，镜像与普通形态的组件身份又不相同。
2. 所有目标组件均须在当前接收资源里唯一匹配。把不同 prefab 的组件并入一张表会导致缺件拒绝，不能靠 keep 绕过。
3. 一个角色同时只能启用一个包。拆成几个同角色 BEM 会被原生注册表判冲突，Windows／Android 管理器也会关闭同角色的其他包。
4. `replace` 网格固定三条流，第三条为受限蒙皮声明，palette 非空；没有表达无蒙皮静态网格的分支。4 字节“刚性蒙皮”仍然依赖骨骼，并不等于 MeshRenderer 静态网格。
5. 当前包不携带新骨架、bindpose、动画、Shader 程序或游戏事件逻辑。若目标是添加全新武器行为、粒子系统、特效 Shader 或技能动画，不能把它当作单纯 BEM 模型替换。

最小格式探针使用仓库已有的合成 1.3 三角形夹具：将两个资源名改为大招主体／技能实体后，Python 容器、manifest 和几何验证均通过；移除第三流、清空 palette 或移除 ui_resource 均被拒绝。这只证明解析器边界，夹具不是可玩的角色 Mod。

资源名解析器没有强制 `*_postmodel`／`*_uimodel` 后缀，因此 Windows 上可考虑有限的“两根蒙皮资源”验证原型。但拿技能实体冒充 `ui_resource`、为同角色伪造多个角色 ID，不能作为正式协议方案，也无法解决安卓限制。

## 三、替换入口能否扩展

### Windows：入口可复用，目标注册与对象种类需要扩展

共用模块已经截获 `BundleLoader.AssetProxy._FinishWithAsset`，并有资源交付、帧分片构建、克隆覆盖、提交／恢复和缓存路径；入口本身不是仅对普通角色的专用函数。`ModRegistry::Match()` 当前只按包里的两个资源根匹配。

庄方宜三个大招 prefab 的 SMR 位于直属 `Mesh_all/lod0/`，因此不必为它们重写全部网格上传过程。应增加明确的资源目标注册、独立组件契约、donor 和缓存身份。

独立武器 prefab 优先在自身交付时替换，再让游戏把其克隆挂到角色身上。不要把角色下所有嵌套武器都放进主体 Mesh 扫描，否则会破坏当前避免同名／重复 Mesh 误匹配的边界。静态武器还需要 MeshRenderer＋MeshFilter 枚举、sharedMesh 赋值、材质复制及完整回滚。

ModelTable 明确标记三种大招资源使用持久池。需验证首次开大、连续开大、打断、回池复用、已加载后启用包和恢复原模型；只覆盖首次资源加载不足以证明池内实例正确。短暂大招演出还应检查异步构建完成前是否出现原模型／混合状态。

武器默认应按武器资源 ID 管理，明确“一把武器的所有使用者”这一作用域。如果只替换某个角色手中的同一武器，还需要实例所属角色的可靠证据，不能直接改共享模板后声称仅影响该角色。

### Android：不能沿用现有 UI donor 假设

`AndroidLoadUiDonor()` 明确要求 `chr_` 开头、`_uimodel` 结尾，并将路径固定拼到 `Gameplay/Prefabs/UIModels/`。world 绑定又依赖 UI LOD0 → world LOD1 的明确映射。

本次 Windows manifest 中不存在对应的大招 `_uimodel`。把 skill entity 根写进 `ui_resource` 会在加载器前置检查就失败。应改为明确的 donor 资源路径和平台绑定策略，按 Android 实际资源确认可用 LOD、顶点声明、骨骼、材质与 shadow proxy；必要时从实际存在的目标 LOD 构建。**不能以本次 Windows 图谱声称已验证 Android 大招或武器资源布局。**

## 四、解析器与转换器分别需要什么

| 层 | 当前情况 | 建议增量 |
| --- | --- | --- |
| BEM 容器／原生读取器 | C++、Python 和 Windows 管理器只支持 1.0–1.3；目标与蒙皮结构有硬约束 | 保留 framing；新增 1.4 manifest 分支与 capability；旧包归一化成旧式角色目标，继续支持 |
| 游戏原生资源读取器 | `NativeAssetReader` 支持列表只启用 SMR；底层 AnimeStudio 已有 MeshRenderer／MeshFilter 类型 | 增加两类元数据，保存 Renderer → GameObject → MeshFilter → Mesh 的真实引用 |
| 资源图谱解析器 | `parse_native_models.py` 只产出 SMR，所有 Mesh 都要求非空 bindpose | 按 Renderer 类型分支；静态 Mesh 允许无 bindpose／骨骼，继续严格验证材质与引用 |
| 角色目录生成 | `import_runtime_catalog.py` 固定普通角色 world/UI 配对，筛 `S_actor_` 且排除 `_vfxpart_` | 增加武器／形态资源目录，按每个目标保留 receiver 路径、Mesh 身份、LOD、骨骼和材质证据 |
| 源 Mod 转换 | 依赖已验证的角色 profile 与 native donor；现有目录未覆盖本次新目标 | 增加武器、主体与镜像的 source → native 映射；INIs 中的游戏状态／特效逻辑不能自动视为已支持 |
| 管理器与安装器 | 按 `character_id` 展示／互斥；安卓 JNI 重用原生格式读取 | 增加目标类别、所有者信息和按实际资源作用域的冲突检测，允许角色包与独立武器包共存 |

研究目录中已做一个仅增加 MeshRenderer／MeshFilter 的读取器原型：新增 8 个对象，成功解析样本剑的 4 个 Renderer 和 4 个 MeshFilter。LOD0 为 9,829 顶点、27,804 索引，bindpose 为 0，顶点属性只有位置／法线／切线／UV；证明该类武器可被现有底层后端解析，缺口位于上层封装与后续处理，不必重写整个资源解包器。

原始读取器有 1 条公共 bundle 子流读取告警，与既有普通庄方宜基准相同；研究原型另有首次启动缺少 CNKeys 配置的告警。目标引用闭合不等于整个后端无告警，记录中保留原始日志和未实机验证标记。

## 五、建议的最小演进与验证顺序

1. 定义多个资源目标及其组件契约：角色、武器、技能实体是所有者／类别信息，实际匹配必须包含明确资源与接收器身份。组件／骨骼／材质 donor 按资源目标隔离，payload 可以共享。
2. 几何声明区分 `skinned` 与 `static`；静态分支允许无蒙皮流和 palette。资源路径、实际 LOD 与 donor 配对独立描述，不再把全部平台都压成 world/UI 两根。
3. 先验证 Windows 庄方宜主体＋技能实体两个共享网格目标，再加镜像。作者网格分目标适配，动画／技能状态继续由游戏驱动。
4. 补上静态武器读取、制作、绑定与回滚，以样本剑验证；法器另做 UInt32×1 骨骼索引的运行时布局观察，不从序列化格式直接承诺可上传。
5. 分别确认 Android 的真实资源／LOD／donor；验证短技能首次展示、对象池复用、换装／换武器／预览、切场景、停用恢复和旧 1.0–1.3 包回归。

这不是已定稿的 1.4 字段规范。确定这些语义后再定字段，避免新包被旧读取器部分执行；新增能力应由版本／required_capabilities 明确拒绝旧客户端。

## 证据与复现入口

- 离线摘要、脚本、格式探针、研究读取器差异：`research/custom_model/1.5.3/Windows/weapon-ultimate-bem/`。提取资源和大型图谱由该目录 `.gitignore` 排除。
- 目标／版本／蒙皮校验：`native/modules/custom_model/bem.cpp:128`、`:198`、`:705`、`:751`；`tools/CustomModel/bem_v11.py:574`、`:675`。
- 资源注册和同角色冲突：`native/modules/custom_model/mod_registry.cpp:59`、`:117`；`ui/BetterEndfield.UI/Services/BemPackageService.cs:531`；`android/app/src/main/java/dev/betterendfield/android/BemInstaller.java:214`。
- 接收器范围、SMR 枚举与资源交付：`native/modules/custom_model/generic_model_matcher.h:17`；`native/modules/custom_model/module.cpp:3155`、`:3616`。
- Android donor 与绑定：`android/app/src/main/cpp/modules/custom_model/android_mesh_builder.cpp:367`；`world_resource_adapter.inc:6`。
- 原生读取器／图谱／目录生成：`tools/CustomModel/NativeAssetReader/Program.cs:67`、`:124`；`tools/CustomModel/parse_native_models.py:87`、`:97`；`tools/CustomModel/import_runtime_catalog.py:81`。
- 形态参数定义：`docs/custom_model/history/bem-1.3/BEM_V1_3_SPEC.md`。

未计算产物哈希；BEM 自身必需的容器完整性字段与资源引用身份不是发布产物的额外哈希核对。
