# BEM 创作者指南

BEM（`.bem`）是 Better Endfield 的模型替换包。1.0–1.3 包对应一个角色；1.4 包对应一个角色或武器 ID，可分别替换普通形态、大招 prefab 或武器资源，并保留外观选项和位置滑条。Windows 和 Android 可使用同一个包，但包必须为各平台声明对应资源与 donor 合同。

| 文档 | 内容 |
| --- | --- |
| 本文 | 工具、制作流程、导入测试、分发、常见问题 |
| [格式规范](BEM_FORMAT_SPEC.md) | 1.0–1.3 的文件结构、字段、上限 |
| [BEM 1.4 扩展](BEM_V1_4_SPEC.md) | 多资源目标、武器、静态网格和平台声明 |
| [运行时行为与兼容性](BEM_RUNTIME_COMPATIBILITY.md) | 游戏内如何匹配部件、几何和贴图要求、显存开销、热切换、第一人称、日志对照 |
| [其他来源 Mod 转换](BEM_SOURCE_MOD_CONVERSION.md) | EFMI / 3DMigoto Mod 的自动转换范围、配方、身份规则、转换报错 |

## 工具

| 工具 | 位置 | 用途 |
| --- | --- | --- |
| 独立创作者 GUI `BetterEndfield.BemTools.exe` | `BEM-Tools-1.5.2-win-x64.zip` 根目录，完整解压后双击使用 | 工程、工作区、转换、打包、解包与合集；无需安装主程序、Python 或 .NET |
| BEM Tools 命令行 `BetterEndfield.BemConverter.exe` | 程序目录 `tools/BemConverter/`，或独立工具包根目录 | 转换、打包、校验、解包、合集，所有功能的核心 |
| 主程序创作者窗口 | 角色外观页 →“BEM 创作者工具…”；旧版入口为“其他来源 Mod 转换…” | 与独立 GUI 共用窗口代码，调用同一个命令行 |
| Blender 导出插件 | 独立工具包 `blender_addon/bem_exporter/` | 从带有 `BEM_C<number>` 对象的 Blender 工程导出可编辑 BEM 工程 |
| 桌面模型管理页 | 角色外观页 | 导入、启用、选择外观和选项、调滑条 |
| Android 模型页 | Better Endfield App | 导入、启用、选择，以及手机纹理转换 |
| AI Skill `bem-creator` | 独立工具包 `skills/bem-creator` | 复制到 AI 工具的技能目录（Codex 为 `~/.codex/skills`），用 `$bem-creator` 调用。只辅助准备配方和解释报告，不能代替校验 |

BEM Tools 从 1.5.1 起正式支持写入和读取 BEM 1.0–1.4，1.5.2 增加独立 GUI；旧的 1.5.0 正式发行包不包含 BEM 1.4。工具版本与 BEM 格式版本不同，当前 `--version` 输出为 `BEM Tools 1.5.2 / BEM 1.0+1.1+1.2+1.3+1.4`。从源码运行时，用 `python tools/CustomModel/bem_tool.py` 代替 exe。

独立工具请完整解压后运行 `BetterEndfield.BemTools.exe`，保留同目录的命令行和支持文件。GUI 跟随系统语言；可将一个 `.bemproj.json` 工程拖到 EXE 图标上打开，或通过命令行传入该工程路径。主程序中的创作者窗口仍使用主程序的语言设置。

## 三种制作方式

1. **转换现有 Mod**：标准 EFMI ComponentN Mod 通常可以直接转换；其他格式需要配方。见 [其他来源 Mod 转换](BEM_SOURCE_MOD_CONVERSION.md)。
2. **编辑现有 BEM**：`unpack` 成可编辑项目，修改后 `pack`。
3. **直接制作**：按[格式规范](BEM_FORMAT_SPEC.md)准备 `project.json` 和二进制 payload，再 `pack`。

无论哪种方式，都建议用**导出工程**（`.bemproj.json`）保存参数，之后可以一键重复导出。

## 直接制作 BEM 工程

如果作者使用 Blender、Maya、3ds Max 或其他建模工具，可以跳过 EFMI 转换，直接准备 BEM 可编辑工程。作者自己的源文件通常包括：

```text
outfit.blend                 # 或其他建模工程
textures/                    # PNG、TGA、DDS 等作者贴图
project.json                 # BEM 工程清单
payloads/*.bin               # 网格、索引和贴图的原始字节
export.bemproj.json          # 可重复导出的任务工程
```

`project.json` 是低层 BEM 工程，适合由导出插件或高级工具生成，不建议手工从零编写。建模软件负责网格、权重、UV、法线和贴图；BEM Tools 负责版本选择、payload 去重、压缩、校验和最终打包。

当前随工具提供的 Blender 导出器先支持经过核实的 16/12/12 蒙皮布局、固定外观和显式贴图身份，输出旧角色目标工程。对象名使用 `BEM_C0`、`BEM_C1` 等，或者设置对象自定义属性 `bem_component_id`。插件尚不直接导出 1.4 多资源或静态武器；这些项目使用下述可编辑工程流程。特殊顶点布局、选项组和形态滑条仍应使用可编辑工程或配方流程。

直接制作仍需要针对目标角色确认组件、骨骼名称、材质槽、贴图身份和世界/UI 资源关系。`catalog/` 提供这些身份与布局资料，但不包含随工具分发的完整角色模型。作者应自行准备建模参考或本地模板。

BEM 不包含 Shader。每个绘制段仍需选择游戏中的原生材质作为 donor；作者自己的贴图替换原生材质上的明确纹理槽。Blender 中看起来正确，不代表没有完成材质身份映射。

推荐的工程目录是：

```text
my-outfit/
├─ source/                   # 作者自己的 blend、FBX 或其他源文件
├─ textures/                # 作者贴图
├─ project/                 # 导出插件生成的 project.json 和 payloads
├─ recipe/                  # 可选的人工映射资料
├─ dist/                    # 最终 .bem
├─ reports/                 # inspect / validate 报告
└─ character.bemproj.json   # 稳定的导出任务
```

最终给玩家分发 `.bem`；源工程、配方和 `project.json` 只在作者需要发布可编辑工程时一并提供。

## 导出工程

桌面创作者窗口默认就是“创建 / 打开导出工程”：选择源 Mod 或可编辑项目，填写包名称、作者、版本和输出路径，保存为 `*.bemproj.json` 后导出。以后打开同一工程，改参数后点“保存参数并导出 BEM”。

```json
{
  "schema": 1, "kind": "bem-export-task", "mode": "pack",
  "source": "editable/project.json",
  "recipe": "",
  "deformations": "body-morphs.json",
  "output": "dist/character.bem",
  "report": "reports/build.json",
  "package": {"id": "creator.my-character", "name": "我的角色", "author": "作者", "version": "1.0.0"}
}
```

- `mode`：`pack` 打包可编辑项目；`convert` 转换源 Mod，可配 `recipe`。
- 路径相对工程文件所在目录。另存工程时会自动调整相对路径。
- `package.id` 首次创建时确定，之后每次导出都保持不变，玩家导入新版本即为更新。ID 规则为 `[A-Za-z0-9][A-Za-z0-9_.-]{0,95}`。
- 导出失败时保留原有成品，不会留下损坏的文件。

命令行：

```text
BetterEndfield.BemConverter.exe new-project editable/project.json --mode pack -o character.bemproj.json
BetterEndfield.BemConverter.exe new-project source-mod.zip --recipe conversion.recipe.json -o character.bemproj.json
BetterEndfield.BemConverter.exe build character.bemproj.json
```

`new-project` 可选参数：`--deformations`、`--export-output`、`--package-id`、`--name`、`--author`、`--package-version`。`build` 只读取工程文件，输出路径、配方、形态配置都在工程里修改。

如果希望把输入文件和输出目录整理成可移动的完整工程，可以使用工作区初始化：

```text
BetterEndfield.BemConverter.exe workspace init 我的角色工程 --source 原始Mod.zip --mode convert
BetterEndfield.BemConverter.exe build 我的角色工程/export.bemproj.json
```

工作区会复制源文件或 `project.json` 及其 payload，创建 `source`、`project`、`textures`、`dist`、`reports` 等目录，并继续使用同一个 `.bemproj.json` 格式。目标目录必须为空或不存在。

## 可编辑项目

`unpack` 把 BEM 解成 `project.json` 和 `payloads/NNNN.bin`：

```json
{"manifest": {正式 BEM Manifest}, "payload_files": ["payloads/0000.bin", "payloads/0001.bin"]}
```

- `payload_files` 的顺序就是 Payload ID。路径必须在项目目录内，不能是绝对路径或含 `..`。最多 16384 个文件，单个不超过 512 MiB，合计不超过 2 GiB。
- payload 是原生顶点、索引、压缩贴图的原始字节，不是 Blender/FBX，也不会还原源 INI 或 Shader。
- `unpack` 要求输出目录不存在。重新 `pack` 保持数据语义，但压缩后的字节不保证相同。
- `pack` 会自动补齐用到的能力声明、选择最低可用的格式版本、共享相同字节。

## 选择格式版本

一般不需要手动指定版本，`pack` 会按内容选择。制作时只需决定用哪种外观结构：

| 需求 | 用法 |
| --- | --- |
| 少量固定的完整外观 | 1.0 `appearances`，最多 64 套 |
| 服装、部件、贴图可以独立组合 | 1.1+ `option_groups` + `component_rules` |
| 换色而不复制 draw | 1.2 贴图槽 `texture_slots` |
| 场景与详情模型骨骼名不同 | 1.2 骨骼别名 `bone_name_aliases` |
| 8K 等大贴图（单张超过 64 MiB） | 1.2 及以上 |
| 连续体型调节 | 1.3 `parameters` + `mesh_deformations` |
| 普通形态与独立大招资源分别替换 | 1.4 `target.resources` + 组件 `resource`/`renderer_path` |
| 静态武器 MeshRenderer + MeshFilter | 1.4 `renderer_kind: static`，不添加伪骨骼 |
| 同包包含 Windows 与 Android 的不同 LOD/donor | 1.4 按资源声明 `platforms` 和 `lod` |

## 武器与大招工程（1.4）

| 入口 | 1.4 支持范围 |
| --- | --- |
| `pack` / `unpack` / `build` / `validate` | 显式多资源、静态/蒙皮网格、原有选项与位置滑条 |
| `inspect` / `validate --resource ID --platform PLATFORM` | 查看指定资源的选中计划；`validate` 仍校验整个包 |
| `bundle` | 将旧角色包与 1.4 包放入同一个标准 ZIP |
| `target-profile` | 从精确原生图谱生成未验证的 profile 和全 `keep` 项目起点 |
| 源 Mod `convert` | 仍是已有角色资料和审阅配方入口，不是任意武器/大招的自动转换器 |
| Blender 插件 | 仍输出旧角色工程，尚不直接导出 1.4 资源表或静态网格 |

一个角色包的 `target.kind` 为 `character`，`target.id` 为角色 ID；武器包使用 `weapon` 和武器 ID。`target.resources` 中每个资源有独立 ID、根名、prefab 路径、平台和 LOD。组件必须声明所属 `resource`、精确 `renderer_path`、`renderer_kind`、原 Mesh/索引、材质及骨骼身份。每个 mesh 的全部 donor 必须来自自己的资源；跨资源共享几何时复制 descriptor 并共享 payload，不能借另一个 prefab 的 donor。

独立工具包包含可直接构建的合成工程：

```powershell
BetterEndfield.BemConverter.exe build examples/multi-resource/project/export.bemproj.json
BetterEndfield.BemConverter.exe inspect examples/multi-resource/project/dist/synthetic.bem --resource ultimate --platform windows-x64 --report ultimate-plan.json
BetterEndfield.BemConverter.exe validate examples/multi-resource/project/dist/synthetic.bem --resource weapon --platform android-arm64 --report android-plan.json
```

示例使用虚构资源，只验证制作和格式流程，不能安装为游戏 Mod。制作真实资源时，先准备相应平台的原生图谱与目标 spec：

```powershell
BetterEndfield.BemConverter.exe target-profile NATIVE_GRAPH.json --spec SPEC.json -o TARGET_PROFILE.json --project project.json
BetterEndfield.BemConverter.exe pack project.json -o target.bem --report build.json
```

生成器不会自动放行来源 Mod 映射或实机渲染：profile 的 `conversion_ready`、`runtime_verified` 保持 false。作者再加入替换几何、draw 与贴图映射，执行完整 `validate` 并实机检查。BEM 沿用游戏的大招触发和武器装备逻辑，不实现源 Mod 的脚本或技能状态机。

Windows 当前只执行显式 LOD0。Android 可声明自身 LOD1 接收器和 donor，保留手机原有 LOD 设计；不同平台合同应使用不同 resource ID。`target.platform` 仍保留历史常量 `windows-x64`，实际平台由 `resources[].platforms` 决定，不要把顶层常量改为 Android。不能只给 Windows 资源补上 `android-arm64` 标签来宣称兼容。没有 Android 资源声明的包不能在 Android 导入；转换贴图也不会补齐资源、骨骼或 LOD 合同。

## 组合外观（1.1+）

- 每个部件在任意合法组合下都必须恰好有一条 `keep`、`hide` 或 `replace` 规则成立，工具会精确证明这一点，并在 `validate` 报告的 `selection_space` 中给出可达组合数和最坏情况的资源量。
- 源 Mod 一个按键联动多个变量时，合并为一个组，或用 `selection_constraints` 限定，避免生成源 Mod 实际到不了的组合。
- `available_when` 只控制某个组是否显示；组被隐藏时保存值保留，切回后恢复。
- 只换贴图、保留原网格时，用 keep 的 `material_overrides`，并确认原贴图名在该材质中唯一。
- 每条 draw 使用自己的索引 payload，可以带 `when` 条件。相同几何可以共享 payload，但不要把只在某个选项出现的索引和其他 draw 合并。
- 源 INI 中的广告、注释不要做成选项组。

组合包保留全部候选资源，玩家切换选项不需要重新导入。游戏只读取当前选择用到的数据。

## 形态滑条（1.3）

在建模软件中做出形态，**保持导出网格的顶点数量和顺序不变**（包括 UV 和法线接缝处的重复顶点），然后提供目标位置或稀疏增量。滑条只改变位置，原骨骼蒙皮、材质、法线和切线保留；形变较大时光照、穿插和轮廓可能不自然。身体变形通常需要衣服和配件做相同形变。

形态配置文件（在导出工程的 `deformations` 中引用）：

```json
{
  "schema": 1, "kind": "bem-position-morphs",
  "parameters": [{"id": "body", "name": "体型", "min": 0, "max": 1000, "neutral": 0, "default": 0, "step": 1}],
  "mesh_deformations": [{"mesh": 0, "parameter": "body", "frames": [
    {"value": 0, "neutral": true},
    {"value": 1000, "target_positions": "body-target.json"}
  ]}]
}
```

- `mesh` 是导出 BEM 中的网格下标。
- 每个非中性帧恰好一种输入：
  - `target_positions`：每个顶点一组 XYZ，JSON 数组或 Float32 二进制文件。工具减去原始位置，只保存非零增量。
  - `deltas`：`[顶点索引, dx, dy, dz]` 列表，可以为空，表示零效果端点。
  - `efmi`：绑定源 Mod 的 ShapeKey，见 [转换文档](BEM_SOURCE_MOD_CONVERSION.md#8-shapekey-绑定到-13-滑条)。
- 坐标轴和单位必须与导出网格一致。顶点数相同并不代表拓扑相同，顺序需要作者保证。
- 中性值在中间时（例如体型可增可减），写三帧：`0`、`500 neutral`、`1000`。多帧之间分段线性插值，每帧都是相对原始位置的绝对偏移。
- 身体、衣服、配件使用同一个参数 ID，就会一起变化。参数可以带 `available_when`，例如只在某套服装下显示。

可运行示例：

```text
python examples/body-slider/create_project.py --output demo-body-slider
BetterEndfield.BemConverter.exe build demo-body-slider/export.bemproj.json
BetterEndfield.BemConverter.exe validate demo-body-slider/dist/synthetic.bem
```

这是一个三角形的格式测试，不是可用的角色包。

## 校验与报告

```text
BetterEndfield.BemConverter.exe validate character.bem --report validation.json
BetterEndfield.BemConverter.exe inspect source-mod.zip --report inspection.json
```

- 退出码 0 表示操作完成，失败时为 2，原因在报告的 `issues` 中。`inspect` 完成只代表检查结束，不代表可以转换。
- 报告包含 `tool_version`、`format_version`、`conversion_ready`、`render_verified`、`issues`。新导出的包 `render_verified` 始终为 `false`。
- `--report` 不能覆盖任何输入或输出文件。

## 导入与测试

| | Windows | Android |
| --- | --- | --- |
| 导入 | 模型管理页“导入 BEM / ZIP”，或把文件拖进页面；ZIP 合集可勾选要导入的包。**需要关闭游戏** | App 模型页导入单个 `.bem`，也可以从文件管理器“打开方式”或“分享”导入 |
| 新包状态 | 默认停用 | 默认启用，并停用与其资源冲突的其他包 |
| 存放位置 | 程序目录 `models/`（不可写时为 `%LOCALAPPDATA%\BetterEndfield\catalog\custom-model\packages`） | App 私有目录，并发布给游戏 |
| 更新 | 相同 `package_id` 即覆盖更新，保留启用状态和仍有效的选择 | 每次导入生成新版本，保留选择 |
| 生效 | 下次启动游戏；已开启热切换时，选择变化由游戏帧调度处理已加载实例 | 同左；1.4 按 Android 显式资源发现，仍需手机实机验收 |

- 旧角色包同一角色同时只能启用一个；1.4 按当前平台的资源根检查冲突，普通形态与大招包只有在资源不重叠时才能共同启用。同一 package ID 更新不能改变 target kind/id。
- 校验未通过的包不能导入。有“关闭模型校验”实验选项可以跳过兼容性检查，仅用于开发测试，可能渲染错误或崩溃。
- 实机测试至少检查场景、角色详情页和配队界面；1.4 还应检查实际武器、大招主体和声明的其他 prefab，以及启用、停用、再次启用。Android 旧包的场景模型由详情模型构建，1.4 显式资源使用自身 donor，规则见[运行时文档](BEM_RUNTIME_COMPATIBILITY.md#3-lod)。
- 游戏里没有生效时，先确认包已启用、外观选择正确，再查看日志中的拒绝原因，对照[运行时日志表](BEM_RUNTIME_COMPATIBILITY.md#11-日志拒绝信息对照)。

## 面向手机的注意事项

- 大多数手机不支持 BC 格式贴图。玩家需要在 App 中对该包点“转换纹理”，转为 ASTC 或 RGBA32。
- 转换后单张贴图不超过 8192 像素和 64 MiB，超出时自动降一级分辨率。面向手机的包建议使用 4K 或 2K 贴图。
- **法线贴图需要声明编码**，否则无法转换：在 `project.json` 的贴图项中加入 `"semantic": "normal"` 和 `"normal_encoding": "xy-unorm"`（BC5 等只存 XY 的法线）或 `"xyz-unorm"`。工具不会根据文件名自动推断。
- 贴图越大，加载时的瞬时内存和显存越高，与包文件大小无关，见[运行时文档第 7 节](BEM_RUNTIME_COMPATIBILITY.md#7-加载开销与显存)。

## 第一人称

头发、头饰、耳朵的权重只绑到 Head 子树或尾巴骨骼，第一人称时才会被正确隐藏；身体和衣服不要绑到 Head 上。详见[运行时文档第 9 节](BEM_RUNTIME_COMPATIBILITY.md#9-第一人称兼容)。

## 分发

- 分发 `.bem` 文件和许可说明即可。原始 dump、游戏贴图、日志不需要附带。
- 多个包可以做成 ZIP 合集：

```text
BetterEndfield.BemConverter.exe bundle first.bem second.bem -o collection.zip --report bundle.json
```

  `bundle` 会逐包校验后以“仅存储”方式打包。不要把 `.bem` 直接改名为 `.zip`。
- 合集限制：最多 256 个包，每个不超过 2 GiB，合计解包不超过 4 GiB；不支持加密 ZIP。说明文件、脚本和嵌套 ZIP 会被忽略。ID 重复（包括仅大小写不同）的包全部跳过，其余照常导入。
- Android 目前只能导入单个 `.bem`，ZIP 合集需要先解出。
- 发布更新时保持 `package_id`、选项组 ID、选项 ID 和参数 ID 不变，玩家的设置才能保留。只改显示名不影响。

## 命令速查

```text
BetterEndfield.BemConverter.exe inspect  <源目录|zip|rar|7z|bem> [--ini 路径] [--report r.json]
BetterEndfield.BemConverter.exe convert  <源> -o out.bem [--recipe recipe.json] [--ini 路径] [--deformations m.json] [--report r.json]
BetterEndfield.BemConverter.exe pack     project.json -o out.bem [--deformations m.json] [--report r.json]
BetterEndfield.BemConverter.exe validate pkg.bem [--report r.json]
BetterEndfield.BemConverter.exe unpack   pkg.bem|collection.zip -o 新目录 [--report r.json]
BetterEndfield.BemConverter.exe bundle   a.bem b.bem ... -o collection.zip [--report r.json]
BetterEndfield.BemConverter.exe new-project <源|project.json> -o task.bemproj.json [--mode convert|pack] [--recipe r.json] [...]
BetterEndfield.BemConverter.exe workspace init <目录> --source <源|project.json> [--mode convert|pack] [--recipe r.json]
BetterEndfield.BemConverter.exe build    task.bemproj.json [--report r.json]
BetterEndfield.BemConverter.exe --version
```

不带 `--recipe` 直接 `convert` 时，每次生成新的随机包 ID；需要稳定 ID 请使用导出工程。

## 常见问题

- **转换成功但游戏里没变化**：确认包已启用、外观选择正确、游戏已重启（或热切换已生效）；查看日志里的 `Package refused`、`Generic model donor is missing` 等信息。
- **游戏更新后包失效**：原 Mesh 名或索引数变化后，旧包会被拒绝（`Generic model donor is missing/ambiguous`），需要用更新后的角色资料重新导出。
- **手机上贴图不显示或报格式不支持**：在 App 中对该包执行“转换纹理”。提示缺少法线编码时，按上一节声明 `normal_encoding`。
- **第一人称时身体被裁掉，或头发没隐藏**：检查相关顶点的骨骼权重，见第一人称一节。
- **配队界面偶尔缺少部位**（例如管理员的眼睛）：已知问题，原因尚未确认，退出并重新进入配队界面即可恢复。
