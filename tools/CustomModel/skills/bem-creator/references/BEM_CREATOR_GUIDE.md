# BEM 1.0/1.1 创作者指南

正式扩展名 `.bem`。玩家只需安装包，不需要 Python、原 Mod 注入框架、角色数据库或手工 runtime.ini。
Windows 与 Android 使用同一个标准包：Android 编译共用的 `native/modules/custom_model` 读取器，作者无需另出一份手机包。包头 `target.platform` 仍固定为 `windows-x64`，这是现有格式常量。手机纹理不兼容时，Android 可对安装包执行「转换手机纹理」；法线编码缺少已验证声明时不能转换。两端保存选项后均在下次启动游戏时应用。1.0 字段见 `BEM_V1_SPEC.md`，组合外观的 1.1 字段见 `BEM_V1_1_SPEC.md`。

## 选择 1.0 还是 1.1

固定的少量完整外观可以继续用 BEM 1.0；`appearances[]` 最多 64 项。服装、局部部件、材质或贴图可独立组合时使用 BEM 1.1 的 `option_groups[]`，一个包对应一个角色，玩家在一个包卡片中设置各组。组数最多 64、每组最多 16 项；所有组的组合数没有固定上限，创作者工具用精确条件图验证可达状态。源包只有一个按键却联动多个变量时，应合并为一个组或声明 `selection_constraints`，避免生成源 Mod 实际到不了的组合。

原生网格不变、但源 Mod 对它的材质贴图有活跃覆盖时，1.1 的 `keep` 候选应写 `material_overrides`，以原生 `material_slot`/`material_name` 和顶层纹理 ID 定位；同时把 `keep-material-textures` 加入 `required_capabilities`。创作者须逐槽确认原纹理名在材质中唯一；不能因 `keep` 保留网格就省去贴图效果。详见 `BEM_V1_1_SPEC.md`。

1.1 的 `available_when` 只控制局部组选项是否适用。组暂时隐藏时，保存值仍在；切回原服装会恢复原局部选择。每个组件对任何可达组合必须恰好有一条 `keep`、`hide` 或 `replace` 规则成立。替换 mesh 的每条候选 draw 自带索引 payload，可用 `when` 决定是否使用。共享顶点流和真正相同的索引可去重；不要把未选 draw 独有索引与当前 draw 强行放到一个 payload，因为运行时按 payload 读取。详细条件语法、资源预算和 `project.json` 清单见 `BEM_V1_1_SPEC.md`。

当前 `convert` 自动路线仍主要生成 1.0 的默认静态外观。制作 1.1 时先用已审阅的源 INI 状态、原游戏资料和 EFMI 身份映射准备可编辑项目，再用 `pack project.json -o 成品.bem` 与 `validate 成品.bem` 校验。普通项目的 `manifest` 放入 1.1 字段，`payload_files` 仍按 payload ID 指向项目目录内的二进制文件。`unpack` 后可以编辑再打包；不能把源脚本直接放进 manifest 当规则执行。

1.1 包保留所有可选内容，使同一个包以后能 A→B→A 热切换而不重新导入；现有管理 UI **没有热切换入口**，选择后仍需重启游戏。游戏加载时只读取所选组合引用的模型/贴图 payload；Android 首次安装时复制完整包到游戏私有**磁盘**目录，以便日后选择其他组合。这不等于运行时把整包读入内存。Shader/RabbitFX 效果、逐帧动画、连续滑块和任意 INI 命令不属于静态组合能力。源 INI 的广告、注释杂讯不得变成选项组；无法等价转换的效果须在报告中写明，不可悄悄忽略。

转换现有 Mod 时，可以移除当前版本不支持的 Shader 特效、低模 LOD 和新增动画系统；逐包报告的 `excluded_features` 应具体列出移除的功能。选定角色 LOD0 的基础网格、活动基础材质贴图及全部有效静态外观仍须保留。若 RabbitFX 或自定义 Shader 负责基础绘制、骨骼变换或基础贴图绑定，应先把这些内容映射到受支持的 BEM 表达；不能仅因它们位于特效命令中就删除。仅通过离线校验的包应标明 `render_verified=false`。

洁尔佩塔完整版是第四条参考路径：先固定源 INI 的 70 条 LOD0 draw、对应 LOD1 守卫与范围，再按已审阅的原生 Mesh、材质和贴图身份构建资源全集，最后把 33 个持久开关写为 1.1 组选项与逐 draw 条件。创作者工具精确证明 301,056 种有效静态选择均有唯一组件操作且不越过资源预算；源规则局部真值表、同部件绘制顺序、原生读取和 A→B→A 另行核对。这条路径需要人工确认源规则与资源映射，并不表示自定义 Shader 效果得到支持或做过实机渲染验证。

1.1.4 补充：源 DDS 支持 BC 格式及紧密排列的 RGBA32、R8；BGRA32 可按通道顺序无损整理为 RGBA32。未压缩 DDS 的 FourCC 为零是正常声明，不表示压缩包损坏。带行填充、数组/立方体/体积 DDS 仍明确拒绝。使用新增纹理格式时需同时更新原生模块；读取源压缩包成功也不等于包内自定义材质已经适配。

## 能做什么

一个包对应一个角色，可放多种固定外观。支持部件替换/保留/隐藏、分离骨骼与材质来源、合并骨骼 palette、
按绘制段使用游戏材质、替换指定原生纹理，以及 UInt16/UInt32 几何索引。

逐绘制段和 RabbitFX 的兼容性边界见 `BEM_PER_DRAW_COMPATIBILITY_20260920.md` 与 `BEM_RABBITFX_COMPATIBILITY_20260920.md`。Stable Textures 可在目标材质映射明确时自动转换；GlowFX 目前只生成效果报告；RabbitFX 实验性骨架/Shader 重写仍需人工适配。
支持共享资源和独立 Zstd 压缩，保持顶点与贴图精度。洁尔佩塔 Outfit B 实际正式包约 36.04 MB。
压缩降低下载与磁盘占用，不降低高模和大贴图的 GPU 开销。

每部件最多 256 个局部骨骼、256 个 draw；每个选定外观最多 32 个纹理绑定。BEM 1.0 的已解码 payload 缓存合计和重建后常驻资源分别最多 512 MiB，BEM 1.1 两项分别最多 768 MiB；单个 payload 仍最多 512 MiB。
源 16 位骨骼索引会重映射，输出是 UINT8 局部索引。超限会报告失败；没有自动分片、低 LOD 生成。
不执行源热键脚本、任意 Shader 或形态键。1.1 支持有限的静态选项组合，但当前游戏内不做实时切换；管理 UI 保存后下次启动生效。

## 使用软件界面

1. 打开“角色外观”→“其他来源 Mod 转换”，先选择转换、解包、打包或制作 ZIP 合集中的一项。
2. 转换任务选择已解压目录或 ZIP、RAR、7z 后自动读取并检查，不需要预先解压或安装解压软件，不直接运行包内程序。
3. 查看识别的内容和下一步说明。成品 BEM/合集会提示直接导入；未适配源包显示待适配原因。
4. 角色资料不由普通用户手选。工具用源资源身份匹配随工具发布的角色资料，核对索引数、顶点流、骨骼及材质；完整转换检查通过才显示“转换为 BEM”。点击转换后选择导出位置。
5. 尚未自动支持的来源，可保存报告。开发者已有完整离线转换规则时，可在高级项加载规则校验。成功后返回管理页导入并启用。
6. 实机核对世界、详情页等所需场景。转换成功不等于外观已经实机验证。

源 Mod 的 ZIP、RAR、7z 只是不同封装，解压后使用相同的格式识别和转换规则。压缩格式支持不代表其中 Mod 的材质和绘制逻辑一定可转换。成品 BEM 仍使用 ZIP 合集分发。
RAR、7z 读取使用随工具附带的 7-Zip 26.03；源码、许可证见工具的 `7zip/NOTICE.txt` 与 `7zip/License.txt`。有密码、损坏或缺少分卷的源包会报告读取错误。

导入会校验包内资源；1.0 验证全部固定外观，1.1 创作者工具精确验证所有可达组合的规则与资源预算，并在 `validate` 报告的 `selection_space` 中给出可达选择数及资源峰值。安装器至少验证默认组合。导入/更新/删除时关闭游戏，避免延迟加载读取正在变动的文件；开关与选择可先保存，下一次启动生效。
更新采用相同 `package_id`；保留本机启用状态及仍有效的外观或选项组 ID，移除/不可达的选择提示并回退默认项。

## 转换自动化边界

| 来源 | 能自动完成 | 需要人工资料的情况 |
| --- | --- | --- |
| 标准 ComponentN | 资源身份匹配、默认静态开关、绘制段、原生材质重放及打包；匹配完整资料时无需配方 | 缺角色资源身份/布局/贴图映射，非标准分发流程、动态状态或特殊材质 |
| Hash/LOD | 已验证 profile 下静态条件与绘制段的转换 | 未识别控制语句、自定义蒙皮、缺材质规则 |
| 经审阅的特殊 draw/Shader 包 | 匹配审阅配方的特定源程序转换 | 源 INI 或 Shader 改变需重新审阅，不能视为通用 Shader 支持 |
| 不明/动态格式 | 列出入口与解析问题 | 需要作者导出静态结果、补配方或明确不支持 |

全角色 profile 解决目标游戏资源资料，不能自动推断作者的 Shader 意图。
手工工作主要是确认骨骼、材质、组件和静态外观状态；完成映射后可重复自动转换，无需每次重新建模。
多外观不会盲目枚举源热键所有组合。1.1 的每个组、可达约束与条件 draw 都需要源 INI 状态和原生资源映射证据。

检查报告提供 `automation` 分类，独立于文件 `format`：

- `standard_candidate`：标准 ComponentN 候选；未完成角色匹配或转换校验时仍为 `conversion_ready=false`，具体缺项见 `issues`。
- `ready`：角色匹配和默认外观完整结构校验通过，可不带 `--recipe` 执行转换。仍须用户实机测试。
- `requires_mapping`：Hash/LOD＋合并骨架，或包含逐绘制段贴图槽覆盖的 ComponentN；可通用解析结构，但需核对材质/骨架映射。
- `manual_only`：发现作者自定义 Shader（含随包辅助 INI）、外部材质框架、形态键或混合入口；明确不能按标准规则自动转换，角色资料齐全也不自动放行。

ComponentN 包也可能依赖 RabbitFX；来源格式识别成功与材质能够自动转换是两件事。
当前工具保守地拦截 RabbitFX，并不表示其声明无法解析。后续计划为“只指定贴图且原生对应明确”的情况建立可复用规则；特有 Shader 效果和动态绘制仍需单独判断。该计划尚未实现，不承诺 RabbitFX 通用视觉等价。

当前女管理员 casualwear 已完成无配方转换。庄方宜为 Hash/LOD＋合并骨架、UINT16 输入骨骼索引，需映射；洁尔佩塔有自定义 Shader，属于专门适配。
佩丽卡只依据游戏原生数据接入资料和 EFMI 原资源身份，没有查看源 Mod 或引入旧样包的 RabbitFX 近似材质、绘制纠正规则。身份资料齐全也不代表任意该角色 Mod 的自定义逻辑可转换。

BEM Tools 1.1.3 已接入 32 个角色、370 个可复用 LOD0 部件的原生资料，补齐 360 个完整单子网格入口和 1,181 项角色内纹理身份。世界/UI 验证后复用，差异部件排除；62 个部件的布局或多子网格暂不支持自动转换。共享同 hash 的公共贴图会覆盖所有已核实的原生名称。资料及政策边界见 `BEM_CHARACTER_CATALOG_20260919.md` 和 `BEM_EFMI_IDENTITIES_20260920.md`。

ComponentN 当前处理标准本地骨架入口、三路顶点流、UINT16/UINT32 几何索引、零 base vertex、可静态求值的默认开关与固定贴图覆盖。
源组件编号仅是包内标签，不固定对应角色部位；同一 hash 的原生索引数改变时拒绝使用旧资料。
空绘制回调或默认关闭的全部绘制会隐藏该部件；多个原生材质按游戏顺序重放。只生成一套默认外观，不执行热键或源脚本。
公共雨雪覆盖只有尺寸/编码与输入资源内容身份符合既有核实记录时才沿用游戏原资源；改写过的全局效果明确拒绝，不静默丢弃。

```powershell
BetterEndfield.BemConverter.exe inspect source.zip --report inspection.json
BetterEndfield.BemConverter.exe convert source.zip -o appearance.bem --report conversion.json
```

自动转换创建新的 package_id；需要更新已发布包时应在解包项目中保留原 ID。`matching` 记录资料版本、组件对应及保留资源提示。
庄方宜样本身体首段与后续段的颜色/法线绑定槽不同，既有转换按核实的贴图语义适配，不是通用逐槽照搬。
识别依据是实际 INI 声明，不按角色名、压缩包文件名或作者文本直接放行。

## 转换配方

随工具提供 `examples/conversion.recipe.json`。路径相对配方所在目录；UI 选择的 source 作为默认源路径。
每个 appearance 可另写 `source`，以组合来自不同静态源的外观，但目标契约必须完全一致。

```json
{
  "schema": 1,
  "package": {"id":"creator.character.outfits","name":"外观合集","author":"作者名","version":"1.0.0"},
  "target": {"character_id":"chr_0013_aglina","world_resource":"chr_0013_aglina_postmodel",
    "ui_resource":"chr_0013_aglina_uimodel","profile_id":"aglina.windows","revision":"20260918"},
  "default_appearance_id":"default",
  "appearances":[{"id":"default","name":"默认外观","format":"hash-lod",
    "profile":"profiles/verified-native.json","ini":"角色.ini"}]
}
```

`format` 可以是 `auto`、`component-n`、`hash-lod`、`reviewed-draws`。
auto 只负责入口格式识别，不能生成缺失的 mapping。
`description` 可写外观说明；`preview` 可指向不超过 8 MiB 的 PNG，打包后共享存储，当前管理页以角色公共头像展示。

Hash/LOD 使用现有 schema=2 且 `verified=true` 的原生 profile，包含每组件的
`mesh_name/original_index_count/strides/attributes/bone_names/materials`，以及 entries 中的蒙皮和材质规则。
profile 中的验证标记必须有真实离线/运行时证据支撑，不能只改布尔值放行。

ComponentN 额外支持 `source_profile` 和 `material_profile`。原生 profile 还必须给替换组件提供
`v24_draws`（内部几何转换桥接需要的明确映射；它不表示正式包兼容旧格式）：

```json
{"v24_draws":[{"start":0,"count":300,"material_component":0,"material_slot":0,"textures":[0]}]}
```

纹理编号对应中间转换结果的记录顺序，profile 的 `texture_names` 提供每个原纹理的完整名称。
无法证明这些对应关系时转换器拒绝，不按贴图文件名猜测。

特殊审阅格式使用 appearance 的 `reviewed` 对象：`recipe`、`database`、`observations`、`native_textures`、`texture_dir`。
这些分别指向审阅配方、标准化离线数据库、运行时观测、原生纹理清单与纹理字节目录。
只复制/重排已审阅语义，不执行源 Shader。此路线需要维护者准备资料，不属于任意 Mod 一键转换。

## 直接制作 BEM

创作者也可绕过其他 Mod 格式，按正式 Manifest 和原生顶点流直接导出。
项目 JSON 包含 `manifest` 和 `payload_files`；后者按 Payload ID 顺序列出二进制文件路径。
工具会检查索引、权重索引范围、声明、draw 分区、纹理大小、外观引用，再压缩写包。
`examples/README.md` 提供入口说明；正式协议不保存 Python/C++ 结构体布局。

## 命令行

发布目录入口：`tools/BemConverter/BetterEndfield.BemConverter.exe`；开发时可用 `python tools/CustomModel/bem_tool.py`。

```text
BetterEndfield.BemConverter.exe inspect "mod目录" --report inspection.json
BetterEndfield.BemConverter.exe convert "mod目录" --recipe conversion.recipe.json -o appearance.bem --report conversion.json
BetterEndfield.BemConverter.exe pack project.json -o appearance.bem --report packing.json
BetterEndfield.BemConverter.exe validate appearance.bem --report validation.json
BetterEndfield.BemConverter.exe unpack appearance.bem -o editable-project --report unpack.json
BetterEndfield.BemConverter.exe pack editable-project/project.json -o edited.bem
BetterEndfield.BemConverter.exe bundle first.bem second.bem -o collection.zip --report bundle.json
BetterEndfield.BemConverter.exe inspect collection.zip --report inventory.json
BetterEndfield.BemConverter.exe unpack collection.zip -o extracted-packages --report extracted.json
BetterEndfield.BemConverter.exe --version
```

退出码 0 表示该操作完成；inspect 完成只代表检查结束。非零表示未完成，报告的 issues 给出原因。
`conversion_ready` 与 `render_verified` 分开；新转换始终 `render_verified=false`。
正式输出使用临时文件写完后替换，失败不会把部分字节当作有效包。

## 多 Mod ZIP 与可编辑项目

网站分发可以使用标准 `.zip`，里面装一个或多个 `.bem`。每个 BEM 仍只对应一个角色，可包含多个外观。
推荐用 `bundle`：逐包校验后以 ZIP “仅存储”模式封装，不对已 Zstd 压缩的数据重复压缩。
不把 BEM 直接改名成 ZIP；普通 ZIP 软件也能打开真正的分发合集。

管理页“导入 BEM / ZIP”会预先校验、列出角色/包版本/外观，让用户勾选导入。
坏包独立报错，有效包仍可导入；重复包 ID（包括仅大小写不同）对应的所有候选会跳过。
相同角色的不同 ID 可同时安装，但同时只能启用一个。同 ID 与已安装包对应时视为更新。
未选择的包不会安装，新包默认停用。忽略说明文件、脚本和嵌套 ZIP，不执行任何包内代码。
ZIP 支持 stored/deflate，不支持加密；每个 BEM 最大 2 GiB，最多 256 包，合集及累计解包字节最多 4 GiB。

`unpack BEM` 输出 `project.json` 和 `payloads/NNNN.bin`，共享 Payload 只输出一次。
这些是原生顶点/索引、压缩纹理和预览字节，未转换成 Blender/FBX，也不会恢复源 INI/Shader。
编辑后用 `pack project.json` 导出。重新打包会保持数据与配置语义，不保证压缩字节完全相同。
payload 文件路径限制在项目目录内；解包要求输出目录尚不存在，避免混入旧文件。
`unpack ZIP` 只解出有效的 BEM 文件；要编辑其中一包，再对该 BEM 执行一次 unpack。

## 独立工具链与 AI Skill

开发构建运行 `scripts/BuildBemTools.ps1`，生成 `artifacts/bem-tools/dist/BEM-Tools-win-x64.zip`。
解压即用，无需安装 Python，不附带游戏采样数据库；和玩家 UI 使用同一套 CLI 核心。
目录内含 exe、依赖、文档、配方示例与 `skills/bem-creator`。CLI 报告带 `tool_version`、`format_version`；
目标 profile 的 ID/revision 和快照保留在输出包及报告中。

可将完整 `bem-creator` 文件夹复制到所用 AI 工具支持的技能目录（Codex 为 `~/.codex/skills`），
通过 `$bem-creator` 调用。该 Skill 可辅助准备配方、调用命令和解释缺项；它不替代校验器，
也不能绕过缺失证据。普通玩家不需要安装 Skill。

## 常见问题

- `ENTRY_SELECTION`：存在多个或没有已识别 INI，检查解压层级并明确入口。
- `TARGET_PROFILE`：缺目标布局/骨骼/材质资料；使用与角色、平台、游戏资源版本相符的 verified profile。
- `source INI/shader differs`：特殊包与审阅版本不同，需要重新审阅。
- `Legacy ... differs`：开发转换桥接结果与 profile 不符；不是要求安装旧运行时。
- `palette / bone index`：输入映射不正确或超过 256 项，不能截断骨骼索引。
- `Draws must partition IB`：绘制段需连续覆盖索引流，重叠绘制应显式复制对应索引范围。
- `texture identity`：需原游戏 Texture 名称，并证明其对应关系。
- 转换成功但游戏不显示：先看运行时目标声明/材质身份拒绝日志，再检查启用包、外观和 LOD；不要把解析成功当作渲染通过。

## 资料与分发

作者分发 `.bem` 和许可/说明即可。原始 dump、原游戏纹理字节、采样日志属于研究证据，不要求随成品发给玩家。
角色 profile 是可复用工具资料；包只内嵌使用到的目标契约和 donor 子集。
本轮采集的 32 个角色原生资料已随工具提供；缺少 EFMI 资源对应与缺少原生采样是不同问题，报告分别说明。当前转换器不会因缺资料自动启动游戏采样。
详情页和开屏补采已验证可用；探针与跨重启采集脚本保留在 `tools/CustomModel/developer-tools/`，游戏更新后复用。完整自动加载队列仍属后续计划。
