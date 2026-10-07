# BEM Tools 1.5.2

解压整个工具包，双击 **BetterEndfield.BemTools.exe** 打开创作者 GUI。
无需安装 Better Endfield、Python 或 .NET。请保留同目录的命令行程序、
`_internal/`、`7zip/`、`catalog/`、`profiles/` 和 `docs/`。

GUI 与 Better Endfield 的“角色外观 → BEM 创作者工具…”使用同一份窗口代码，
支持导出工程、标准工作区、源 Mod 转换、BEM 解包、项目打包和 ZIP 合集。
导出的模型包在 Better Endfield 中导入使用。

也可以把一个 `.bemproj.json` 工程拖到 GUI 的 EXE 图标上，或运行：

```powershell
.\BetterEndfield.BemTools.exe "D:\我的工程\export.bemproj.json"
.\BetterEndfield.BemConverter.exe --version
```

命令行入口保持为 **BetterEndfield.BemConverter.exe**，支持 BEM 1.0–1.4。
武器和大招的 1.4 项目可打包、校验和解包；普通角色转换流程不会自动成为通用武器转换器。
详情见 [创作者指南](docs/BEM_CREATOR_GUIDE.md) 和 [BEM 1.4 格式](docs/BEM_V1_4_SPEC.md)。

## English

Extract the complete archive and launch **BetterEndfield.BemTools.exe** for the GUI.
Better Endfield, Python and .NET do not need to be installed. Keep the bundled CLI
and support directories beside the GUI executable. The UI follows the system language.

The standalone GUI shares its creator window with Better Endfield. It supports
export projects, portable workspaces, source Mod conversion, unpacking, packing
and ZIP bundles. Pass one `.bemproj.json` path to open an existing export project.
Import the resulting packages in Better Endfield to use them in the game.

**BetterEndfield.BemConverter.exe** remains the CLI entry point, with BEM 1.0–1.4
support. Read the [Creator Guide](docs/BEM_CREATOR_GUIDE.en.md) for format and
conversion limitations.
