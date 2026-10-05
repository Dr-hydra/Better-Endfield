# 创作者工具工程化评估（2026-10-01）

现已实现“打开工程、改参数、导出”的工作流，复用现有打包和转换后端。评估发现的 1.0 别名导出缺口也已修复：转换或项目打包时正规生成所需的 1.2 manifest；普通内容仍保留 1.0。下文保留评估依据，并标明最终实现。

## 当前已经支持的两种工程

| 入口 | 已有能力 | 当前缺口 |
| --- | --- | --- |
| `conversion.recipe.json` + 源 Mod | 保存包 ID、版本、目标资料、源 INI、每个固定外观及骨架/材质映射；路径相对配方目录；CLI 可重复执行 `convert` | 默认源路径和输出位置仍由命令行或 GUI 单独提供；转换输出仍是 BEM 1.0 固定外观，不是通用 1.1/1.2 规则编辑器 |
| `project.json` + `payloads/` | `unpack` 输出完整 manifest 和二进制资源，编辑后 `pack`；支持 1.0/1.1/1.2，按内容选择包版本 | 属于原生流和协议层工程，需要填写较低层字段；不恢复源 INI、Blender、FBX，也不直接从这些建模工程导出 |

改进前 GUI 已有“将项目打包为 BEM”，实际调用同一 `pack` 后端。转换 GUI 也可加载配方，但 source、recipe、输出路径和当前任务没有完整工程保存/恢复入口。现新增独立导出任务工程，支持新建、打开、保存输入/配方/输出/报告和包信息；再次打开后可直接导出。

证据：

- [bem_projects.py](../../../../../tools/CustomModel/bem_projects.py)：`pack_project()`、`unpack()`。
- [bem_tool.py](../../../../../tools/CustomModel/bem_tool.py)：`convert()`、`convert_automatic()`、CLI 命令表。
- [BemConverterWindow.cs](../../../../../ui/BetterEndfield.UI/Views/BemConverterWindow.cs)：`ResetTask()`、`PrepareConversion()`、`ExportPrepared()`、`ProcessProject()`。
- [conversion.recipe.json](../../../../../tools/CustomModel/examples/conversion.recipe.json)：现有转换配方模板。

## 现在就能使用的重复导出

对已经适配好的 BEM，首次解包，随后保持 `package_id`，直接修改工程内的配置或 payload 后重新打包：

```text
BetterEndfield.BemConverter.exe unpack original.bem -o editable
BetterEndfield.BemConverter.exe pack editable/project.json -o dist/updated.bem --report reports/packing.json
BetterEndfield.BemConverter.exe validate dist/updated.bem --report reports/validation.json
```

也可在 GUI 中选择“将项目打包为 BEM”并打开 `editable/project.json`。payload 路径必须位于工程目录内；工程移动后仍可使用其相对路径。解包目标目录必须尚不存在；重新导出直接 `pack`，不必再次解包。

对需要重新读取源 Mod 的项目，保存已核实配方，再运行：

```text
BetterEndfield.BemConverter.exe convert source --recipe conversion.recipe.json -o dist/updated.bem --report reports/conversion.json
```

配方里的每个 appearance 可以保存 `source`，省去依赖 GUI 先选的源路径；CLI 目前仍要求给出 `source` 位置参数。配方中的相对路径按配方目录解析，位置参数按调用者工作目录解析。已有的显式配方保留作者指定的包 ID；无配方自动转换每次创建新的随机 ID，不能直接当成已发布包的稳定更新工作流。

## 新发现的导出一致性缺口

`target_from_profile()` 已透传 `bone_name_aliases`，提夫罗斯资料也包含该字段，但 `convert()` / `convert_automatic()` 仍使用固定外观的 1.0 `Builder`。1.0 Python 校验没有检查该扩展字段与能力声明，写入器也不会因为别名自动升到 1.2；原生正常模式会要求 `resource-bone-aliases` 能力声明。

已用最小 fixture 验证：向合法 1.0 工程的 target 添加合法 world 别名，Python `check_geometry()` 和写包均成功，头部 `minor=0`；生产原生 validator 返回 `Bone name alias capability mismatch`。这不是某个真实源包的完整转换结果，但证实该组合存在创作者与运行时校验不一致。不能将现有 `convert` 描述成已经完整支持所有 1.2 工程参数。

现通过 `bem_export.prepare_export()` 显式完成创作者版本适配：需要别名、非压缩 skin stride 32 或贴图槽的固定外观工程正规升级为选项组/组件规则，每 draw 独立索引；声明所需能力后由写入器选 1.2。Python 严格 1.0 校验也明确拒绝不带版本适配的别名/槽字段，避免创作者写包成功但原生拒绝。现有组合项目的 `pack` 路线继续支持 1.2。

## 已实现的最小优化

本轮增加可保存的导出任务，复用现有 CLI，并补齐 hash/LOD 的明确 float32/UInt32 蒙皮输入及原生 32 字节布局导出：

1. GUI 加入“新建工程 / 打开工程 / 保存工程 / 导出”，保存模式、输入、配方、输出及报告路径；重新打开工程即可重复导出。
2. 工程描述文件与现有 `project.json` 分开，避免混淆“导出任务”和“BEM manifest + payload 工程”。所有相对路径按工程文件所在目录解析，命令调用使用明确路径。
3. 转换任务保留稳定 package ID、作者及版本参数；自动匹配后的目标资料继续由已有 catalog/profile 提供，工程记录资料 ID 和 revision。
4. 输出继续使用现有临时文件完成后替换的写法；失败保留既有成品，并展示已有结构化报告。导出结束保留工程配置，支持再次导出。

下面是已实现的任务文件结构，使用 `new-project` 创建，`build` 重复导出；GUI 读写相同 JSON：

```json
{
  "schema": 1,
  "kind": "bem-export-task",
  "mode": "convert",
  "source": "source",
  "recipe": "conversion.recipe.json",
  "output": "dist/character.bem",
  "report": "reports/conversion.json",
  "package": {"id": "creator.character", "name": "角色外观", "author": "作者", "version": "1.0.0"}
}
```

`mode=pack` 时 `source` 指向现有 `editable/project.json`，不需要 `recipe`。包信息、组件规则、别名、贴图绑定仍放在原有配方或 manifest 中，避免两份配置相互覆盖。若要再降低创作者填写门槛，第二阶段可给已有 manifest 添加受约束的参数编辑界面；直接从 `.blend` / FBX 导出需要额外的顶点、骨骼和原生材质转换工作，属于另一项工具工程。

## 工程参数与双端边界

- **输入与规则**：固定外观配方可保存 source、INI、profile、source/material profile、reviewed 依赖；组合项目使用 `option_groups`、`selection_constraints`、`component_rules` 与条件 draws。工具不能从任意 Shader 自动推断这些语义。
- **别名**：组合项目在 target component 写 `bone_name_aliases`，声明 `resource-bone-aliases`，由写入器生成 1.2。绑定仍对应原骨骼索引，别名不重排骨架。
- **纹理**：manifest 保存原始贴图名、格式、尺寸、mips、sRGB、payload；需要手机转换的法线应声明已经确认的 `semantic` / `normal_encoding`。Android 安装器识别 `xy-unorm` / `xyz-unorm`，缺少可靠编码时会拒绝转换；不能根据文件名推测。
- **平台**：当前同一 BEM 供双端使用，`target.platform` 仍为格式常量 `windows-x64`，不能通过改成 `android` 来生成手机包。Android 安装时执行既有纹理适配与必要的 mip/尺寸缩减。新的导出任务不应先引入一套未经实现的“Android 导出格式”。
- **再次导出**：修改二进制资源时必须同步维持顶点/索引/纹理描述；只改参数也需要运行既有校验。离线通过仍不等于游戏材质和渲染效果已经实测。

## 验证

- 新增工程创建/重开/移动/重复构建、稳定 ID、参数覆盖、输入保护以及真实配方别名/32 字节蒙皮输出测试，使用生产原生 validator 验证。
- 现有项目、ComponentN、Hash/LOD、BEM 1.0/1.1/1.2 与审阅转换回归已通过；独立 C# CreatorProjectChecks 覆盖 GUI 读写、参数恢复、稳定 ID、另存路径及覆盖保护。
- 另用生产 `BetterEndfield.BemValidate.exe` 复现上述 1.0 别名写包不一致，原生明确拒绝。
- Windows UI 编译通过；本代理未生成发布包、未部署、未计算产物哈希。
