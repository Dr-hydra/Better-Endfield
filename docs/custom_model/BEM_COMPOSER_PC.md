# BEM 自由组合：PC 首轮实现

更新：2026-10-27。此文描述已落地的首轮范围；完整双端规划见研究目录中的 BEM_COMPOSER_DUAL_PLATFORM_PLAN.md，尚未完成的阶段不作为当前能力。

## 使用入口

PC「角色与武器 · BEM」→「组合已安装包」。选择同一角色的至少两个已安装来源，也可附加武器来源；同一武器的候选也可独立组合。普通模型仍按部件选择，world/UI 的原有共用关系保持不变。

- 来源的当前选项/外观只作为默认值；保留全部离散选项及联动，源文件只读，不再将源包固定成当前状态快照。
- 外层选择部件来源，内层保留源包选项。一个源选项控制多个部件时共享同一个状态；某部件改用其他来源后不再执行原来源的部件规则。选项 ID、贴图槽 ID 使用独立命名空间，保留 available_when、selection_constraints、draw.when、条件贴图槽及 null 不覆盖语义。未使用的来源隐藏其选项，并暂不施加其约束。
- 选项旁以「关联 N 个部件」显示影响范围，点击打开可滚动的完整名称／资源／renderer 路径清单，并可复制。名称在常规界面缩略，完整内容不截断。组合器的源选项按来源折叠预览；实际生效仍取决于部件来源与其他选项。
- 未知资源、大招相关资源和武器按整组原子项选择，默认「不加入组合」。大招、ability、mirror 的关联资源和内部组件不拆分；不同武器和互不重叠的大招资源组可分别选或不选。
- 同一 1.4 来源里的普通 postmodel/uimodel 与大招保留在同一输出包中，共用一套源选项状态；普通模型仍逐部件选择，大招仍为整体项。资源根集合相同的整体来源是同一项的候选；不同集合仅在资源不重叠时作为独立项，不允许生成互相覆盖的资源。
- 普通来源的其他候选和「保留原版」仍保留为运行时选项；整体项选择一个来源后同样保留其替代候选。选「不加入组合」则该整体项及其资源、组件和 payload 不进入输出。
- 输出目录下创建全新的 compose.<随机 ID> 子目录及 compose-report.json。单一目标结构输出 composition.bem；混合旧角色目标、显式大招、武器等时，在 p0/p1 等子目录分别输出有效 BEM，由同一生成/导入流程处理，不要求把 world/UI 迁移成显式目标。暂不额外打 ZIP bundle。
- 移除手填版本。包显示版本取来源版本的去重组合（过长时使用 composed，完整版本保留在报告），BEM 格式按组合内容自动确定并显示。CLI 仍接受可选 version 覆盖。
- 支持仅生成、生成并导入、生成并启用。导入和启用复用现有包管理器；游戏正在运行时沿用其既有保护。
- 组合窗口右侧常驻模型预览，分析完成后自动显示，切换部件来源时自动更新并保留同一模型的视角。普通来源切换复用已校验的临时组合包；整体项加入／移除时重新准备快照。首次准备仍需组合及完整校验，原包和已安装配置保持只读。
- 预览中可切换外层部件来源和源包离散选项，点击「应用到组合」回写默认值，随后正常生成／导入；未应用的预览修改不写入组合。多输出时在预览区选择输出，源选项按输出隔离。组合来源变化后旧预览不能回写；中英文操作文案配套提供。
- 普通组件可展开「细分绘制段」，按来源／材质／绘制序号勾选多个包的替换网格段；勾选时切到「按绘制段组合」，不按不同包的段编号自动配对。「定位」在常驻预览中以黄色高亮对应段，再次点击取消；当前选项下没有显示的段明确提示。大招和武器仍作为整体项。
- 细分勾选保存成该组件的一个自定义方案，生成包可在整体来源、原版与该方案之间切换；每一段的勾选属于组合编辑配置，不额外占用一个运行时选项组。源包的离散选项、可用条件、约束、绘制条件和贴图槽逻辑继续保留。未勾选的替换段不输出，不从原版自动补回；全部未选则自定义方案隐藏该组件。
- 跨来源细分会合并顶点流、重映射骨骼调色板和材质 donor，并将逐段索引重定位为 UInt32。顶点声明及布局必须兼容，调色板不得超过 256，顶点不得超过 1048576；不兼容或超限明确拒绝，不静默丢段。仍需游戏内检查接缝、穿插和动画效果。
- 导入失败不删除已校验输出，可从结果区打开输出目录后重试。
- 保留原版表示该接收器使用 keep，不复制原版游戏资产。

## 支持范围

| 来源 | 首轮处理 |
| --- | --- |
| 1.0 固定外观 | 全部外观转为一个共享选项组，将共享索引的 start/count 切成逐 draw payload |
| 1.1 选项 | 保留选项组、可用条件、合法组合约束、部件规则、draw.when 和 keep 贴图覆盖 |
| 1.2 贴图槽与骨骼别名 | 保留全部条件贴图槽；null 表示不覆盖；保留 donor 合同 |
| 1.3 无参数/形变的包 | 可以组合；有参数、保存参数值或形变则明确拒绝 |
| 1.4 无参数/形变的包 | 筛选 Windows 资源，重编号 resource/component，保留静态/骨骼类型 |
| 二进制 BEM / 标准单包 ZIP | 复用现有 native Container 读取 |
| 1.5 无新功能的包 | 按实际目标结构组合，不因头部 1.5 而拒绝；材质属性覆盖、驱动器、root offset 仍按具体功能明确拒绝 |
| 旧角色包 + 显式 1.4 大招/武器 | 可混合，普通 world/UI 保持共用，整体附件单独有效出包；由 UI 连续导入/启用 |
| Android arm64 | 已支持；原生界面按需打开预览，按 android-arm64 筛选资源 |
| 跨角色 / ZIP bundle 交付 | 尚未实现；多武器独立包集合已支持 |

旧版来源统一输出 1.2，以使用 64 choice / 4096 rule 的容量合同；显式资源来源输出 1.4。不会把旧包缺失的 renderer_path 或 asset_path 猜成 1.4 数据。

外层来源选择和保留的源选项合计每包不得超过 64 组；规则深度、候选数量、资源预算仍受格式限制。超限明确拒绝，不静默拍平或丢弃选项。同一显式来源的普通模型与大招不会拆到不同输出，因此共享选项不会因出包而断开；不同目标或旧／显式资源合同仍分包。旧组合继续可用；旧快照已丢失的源选项必须读取原包重新生成。

同一输出内，相同资源名的 asset_path/LOD、相同接收器的 renderer_path/renderer_kind、原始索引数、骨骼名、材质表和别名必须一致。同一整体项候选的根集合和组件表必须一致。普通角色身份必须一致；武器按各自 kind/id 独立出包。revision 只记录资料来源，不作为兼容性门槛。每个输出保留本组第一个来源的 profile_id/revision，并由组合快照说明其来源。

组件预算按唯一接收器合并；目前保留来源表中的全部兼容接收器作为 donor 闭包的安全超集。即使某接收器同时是骨骼/材质 donor，也只生成一条受其槽位控制的规则，不追加无条件 keep。

同源 payload、mesh、texture 通过来源序号与原始 ID 去重。首轮不做跨来源内容哈希去重，也不计算产物哈希。

## CLI 与共享核心

格式逻辑在 CustomModelCore 的 bem_compose.cpp。bem_package_reader.h 的实现放在 bem.cpp，直接复用私有 Container、Select 和 Evaluate，不另写 BEM/ZIP 解析或条件求值器，不改现有运行时加载路径。

核心公开 PlanBemComposition / BuildBemComposition，使用 schema=1 的 JSON 边界。Plan 包含完整 request 和来源的规范路径、大小、修改时间；Build 重新规划并比较整个计划。来源变化或计划被修改时必须重新分析。

请求示例（JSON 为 UTF-8）：

    {
      "schema": 1,
      "platform": "windows-x64",
      "project_id": "compose.example",
      "name": "组合外观",
      "author": "Author",
      "version": "1",
      "sources": [
        {"path": "D:/models/a.bem", "selection": "outfit:a", "parameters": ""},
        {"path": "D:/models/b.bem", "selection": "", "parameters": ""}
      ]
    }

    BetterEndfieldNext.BemCompose.exe plan --request request.json --json plan.json
    BetterEndfieldNext.BemCompose.exe build --plan plan.json --choices choices.json --output-dir NEW_DIRECTORY
    BetterEndfieldNext.BemCompose.exe validate --package NEW_DIRECTORY/composition.bem --platform windows-x64
    python tools/CustomModel/bem_tool.py compose plan --request request.json

plan 也接受逐项参数：--platform、--project-id、--name、--author、--version、重复 --source，以及紧随来源的 --selection。Python 入口只委托给同一个原生工具；开发环境可用 BEM_COMPOSER 指定 exe。

choices.json 是选项组 ID 到候选 ID 的对象，例如 {"part0":"s1","atomic":"original"}。省略的组使用 plan.default。这里只设置输出默认值，不删除其他候选。组与候选 ID 从 plan.slots / plan.atomic_items 中读取，不根据部件名猜测。

choices.json 也接受生成包中保留的源选项 ID（例如 s0.g0），用于预览默认值回写。多输出必须带项目 ID 前缀，例如 p0.s0.g0。未知组、未知选项及不可达默认组合仍拒绝，不修改计划或来源文件。

细分组件将父选择设为 split，并通过 plan.slots[].segments[].id 设置 on/off，例如 {"part0":"split","part0.d0":"off","part0.d1":"on"}；多输出使用 p0.part0 / p0.part0.d0。未填写的段采用计划中的默认值。只有启用细分的组件才发布自定义方案；整包来源候选仍保留。生成包的 composition_segments 元数据记录所选段及其来源，供预览定位和结果追溯。

stdout 是 {"success":true,"result":...} 或 {"success":false,"error":"..."}；失败退出码 2。--json 输出原始 plan/report，禁止覆盖已有文件。--cancel-file 指向取消标记，CLI 也支持 Ctrl+C；WinUI 通过标记协作取消并等待进程清理，不强杀进程。

## 构建与测试

    cmake -S native -B build/bem-compose -G "Visual Studio 17 2022" -A x64
    cmake --build build/bem-compose --config Release --target BetterEndfieldNext.BemCompose BetterEndfieldNext.BemValidate --parallel
    python native/tests/bem_compose_cli_tests.py build/bem-compose/stage/Release/tools/BemCompose/BetterEndfieldNext.BemCompose.exe

PC 发布布局包含 tools/BemCompose/BetterEndfieldNext.BemCompose.exe，Layout 与发布文件清单均声明依赖。普通原生构建不依赖 Python。

WinUI 构建需要仓库已有的 resources/generated 产物；工作树可通过 -p:BEWorkspaceGeneratedRoot=<已有生成目录> 只读复用，不必复制或重建真实游戏资料。

构建先写同卷独立 stage，逐 payload 复制，生成完整包后复用 ReadBemPackageInfo/LoadBem 校验。原生多值决策图证明合法状态下每个部件／贴图槽恰好一个候选，覆盖每个可达操作、draw、贴图槽候选与有效选项的合法见证状态，再逐资源加载这些状态；不会把不可达的单项切换当成构建错误，也不会漏掉非默认来源里的非默认变体。逐资源同时生效的 draw／索引预算使用有界符号优化，搜索超限时采用保守上界；决策图上限为 250000 节点。验证完成再原子发布新目录。输出已存在、选择无效、来源变化、取消、读写或校验失败均不覆盖源包和已发布目录。

独立组的内存/贴图预算同时计算「各组最坏值之和」与「所有候选引用的唯一资源并集」，取两个安全上界中的较小值。超过保守上界会拒绝生成；复杂选择可能仍有误拒绝，后续可进一步收紧上界，不能放宽成漏检。

## 后续验收

- 真实游戏内普通部件混搭、贴图覆盖、大招与武器显示，以及游戏内切换尚需实机验收；结构校验不证明接缝、裁切或美术效果正确。
- 参数形变需要真实快照烘焙或完整参数命名空间与引用重写，不能只修改 default。
- 1.5 需要覆盖材质、driver bindings、音频 payload 和 root offset 全部引用，当前不静默降级。
- 多包 bundle、原子项快照降级、Android JNI/UI 与数值进度后续实现。

### 预览刷新与正式生成

- draft --plan ... --choices ... --output-dir ... 仅生成窗口私有的临时快照，验证当前选择；报告标记 preview_only: true。正式生成仍使用 build，完整检查可达的非默认组合，不复用草稿作为发布产物。
- 外部选项修改合并等待 400 ms；刷新时编辑区保持可操作。新修改取消旧的准备/导出任务，旧结果不能覆盖新选择。
- WebView 私有贴图缓存按来源文件路径、大小、修改时间、纹理编号、格式和输出尺寸预算区分；草稿间由生成器提供来源代际标识。缓存最多 64 项、512 MiB，随预览窗口清理，不增加发布依赖。
- WebGL 上下文和已上传的相同贴图在刷新时复用。首次遇到新贴图仍需解码，之后只重新导出变化的场景并复用贴图。

### 绘制段批量操作与重叠规则

- 列表勾选仅选择批量操作对象；右侧“已启用／未启用”是组合配置。可跨来源多选，或“选择此来源全部段”，然后统一启用或停用。每次操作只排队一次预览刷新。
- “高亮所选”进入临时查看：当前部件仅显示所选段，包括平时未启用的段；其他部件不变。它不会修改启用状态或正式输出默认值，预览内禁止保存及切换源选项。“退出高亮”恢复实际组合。源条件不满足的段仍不显示，并报告可见数量。
- 同一部件内启用的多个来源是几何叠加，没有先后覆盖、自动去重、布尔裁剪或重叠修复；共面三角形可能闪烁，交叉网格可能穿插。界面显示已叠加来源数与说明。
- 不能混合的顶点布局在批量启用前提示；须先停用冲突来源。完整生成仍执行原有容量、绘制数和所有可达变体的校验。
- 草稿准备和预览导出均有 25 秒期限；过期结果不覆盖新配置。失败时明确保留上次画面，禁止把失败配置当作可保存的当前预览。
