# BEM Tools 1.5.3

解压整个工具包，双击 **BetterEndfieldNext.BemTools.exe** 打开创作者 GUI。
无需安装 Better Endfield、Python 或 .NET。请保留同目录的命令行程序、
`_internal/`、`7zip/`、`catalog/`、`profiles/` 和 `docs/`。

GUI 与 Better Endfield 的“角色外观 → BEM 创作者工具…”使用同一份窗口代码，
支持导出工程、标准工作区、源 Mod 转换、BEM 解包、项目打包和 ZIP 合集。
导出的模型包在 Better Endfield 中导入使用。

1.5.3 更新 Windows 1.6.3 角色目标资料；独立 GUI 自 1.5.2 起提供。
新增采集资料随 `catalog/`、`profiles/` 一起分发。资料内的准备状态仍是准则：
运行时采集完成不代表任意源 Mod 可以自动转换，也不代表替换模型已通过游戏内验证。
本次没有加入 Android 新版游戏目标证据。

新增资料入口：[1.6.3 目标资料说明](profiles/endfield-1.6.3/README.md) 与
[目标索引](profiles/endfield-1.6.3/index.json)。包含祀的部分常规角色 catalog、
祀完整世界/详情与 cloth_04 的 1.4 起点，以及明河三个世界模型的 1.4 起点。
这些显式项目均为 `keep-only`：可读取、打包，但尚无替换网格；
`runtime_verified`、`conversion_ready`、`render_verified` 均保持 `false`。
便携证据随该目录的 `evidence/` 分发，不需要开发者的研究工作区。

也可以把一个 `.bemproj.json` 工程拖到 GUI 的 EXE 图标上，或运行：

```powershell
.\BetterEndfieldNext.BemTools.exe "D:\我的工程\export.bemproj.json"
.\BetterEndfieldNext.BemConverter.exe --version
```

命令行入口保持为 **BetterEndfieldNext.BemConverter.exe**，支持 BEM 1.0–1.4。
武器和大招的 1.4 项目可打包、校验和解包；普通角色转换流程不会自动成为通用武器转换器。
详情见 [创作者指南](docs/BEM_CREATOR_GUIDE.md) 和 [BEM 1.4 格式](docs/BEM_V1_4_SPEC.md)。

## English

Extract the complete archive and launch **BetterEndfieldNext.BemTools.exe** for the GUI.
Better Endfield, Python and .NET do not need to be installed. Keep the bundled CLI
and support directories beside the GUI executable. The UI follows the system language.

The standalone GUI shares its creator window with Better Endfield. It supports
export projects, portable workspaces, source Mod conversion, unpacking, packing
and ZIP bundles. Pass one `.bemproj.json` path to open an existing export project.
Import the resulting packages in Better Endfield to use them in the game.

Version 1.5.3 refreshes Windows 1.6.3 character target data; the standalone GUI
was introduced in 1.5.2. Target collections travel with `catalog/` and `profiles/`.
Their readiness fields remain authoritative: captured runtime data alone does not
establish automatic conversion or in-game rendering. This update adds no evidence
for targets in a new Android game version.

See the bundled [1.6.3 collection](profiles/endfield-1.6.3/README.md) and
[target index](profiles/endfield-1.6.3/index.json): a partial legacy catalog for
Jsspsi, full world/UI and cloth_04 resource projects for Jsspsi, and three world
variants for Minghe. Explicit projects are keep-only authoring starting points,
with all three readiness flags still false. Their portable evidence is included
under the same collection's `evidence/` directory.

**BetterEndfieldNext.BemConverter.exe** remains the CLI entry point, with BEM 1.0–1.4
support. Read the [Creator Guide](docs/BEM_CREATOR_GUIDE.en.md) for format and
conversion limitations.
