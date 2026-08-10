<p align="center">
  <img src="src/EFStartChange.UI/Assets/gilberta.png" width="128" alt="EF Start Change Logo">
</p>

<h1 align="center">EF Start Change</h1>

<p align="center">《明日方舟：终末地》登录场景角色、动画与角色配音控制器</p>

> [!WARNING]
> 本项目是非官方实验工具，会向游戏进程注入本机代码并修改运行时行为。使用时可能遇到游戏崩溃、版本不兼容或账号限制风险。请先阅读下方风险说明，并自行遵守游戏服务条款。

## 功能

- 登录场景角色替换，内置 31 组完整角色模型预设。
- 复用角色自身骨架与动画，支持坐姿循环、坐姿特殊动作、起身过渡和最终动作四个阶段。
- 可分别调整四阶段播放速度，并配置朝向、转身时间、模型缩放和前倾采样时间。
- 支持原生循环、强制循环及双 Playable 交叉淡化循环。
- 内置 4,058 个已索引动画片段，可按角色选择最终动作并显示运行时动画时长。
- 32 个角色的独立配音语言路由，可选择中文、英语、日语、韩语或跟随全局设置。
- Windows 11 风格 WinUI 3 控制界面，支持浅色、深色和跟随系统主题。
- 支持保存配置、查看日志、启动注入器、检查更新和创建桌面快捷方式。
- 首次启动显示风险说明与免责声明。

## 兼容性

- Windows 10/11 x64。
- 当前版本面向 Unity IL2CPP 客户端，不支持其他架构。
- Hook 与已验证的游戏构建绑定。游戏更新后如入口签名不匹配，相关功能会停止加载，需等待项目适配。
- 已验证的 `GameAssembly.dll` SHA-256：

```text
0C5573679BC6DEC2D068A14335466DB7CCF20AF9BAE2B983FB9D45677D80FFCE
```

## 下载与安装

1. 打开 [Releases](https://github.com/Dr-hydra/EF-Start-Change/releases/latest)。
2. 下载 `EFStartChange-1.0.0-Setup.exe`。
3. 运行安装器并阅读风险说明。默认安装到 `%LocalAppData%\Programs\EF Start Change`，安装器本身不要求管理员权限。
4. 启动 `EF Start Change`。首次运行时需确认应用内免责声明。

安装器目前没有代码签名，Windows SmartScreen 可能显示未知发布者警告。请只从本仓库的 Releases 页面下载安装包，并在需要时核对发布页提供的 SHA-256。

发布版只安装两个程序文件：

- `EFStartChange.exe`：包含 .NET 及 WinUI 运行时的自包含单文件控制器。
- `Il2cppDumper.exe`：独立的原生启动器和手动映射器。

WinUI 原生运行时会在应用启动时由 .NET 单文件机制解压到当前用户的运行时缓存目录。启动注入器时可能出现 UAC 提权提示。

## 使用方法

### 角色替换

1. 在“设置”页确认 `Endfield.exe` 和随软件安装的 `Il2cppDumper.exe` 路径。
2. 打开“角色替换”页并启用功能。
3. 选择角色、最终动作及所需的速度、朝向、缩放和循环参数。
4. 点击“保存参数”，或直接使用“保存并启动”。

模型与动画配置会在下一次启动和注入时读取。游戏已经运行时，请先完整退出游戏再重新启动。

### 角色配音

> 请先通过游戏下载对应语言的完整语音包。未安装目标语言包时，角色可能没有语音。

1. 打开“配音语言”页并启用功能。
2. 选择角色和目标语言，点击添加或更新规则。
3. 保存参数并重新启动游戏。

界面会自动维护角色规则。底层配置示例：

```ini
voice_router_enabled=true
voice_language_rules=aglina:Japanese,chen:Chinese,*:FollowGlobal
```

如果角色标识与当前游戏数据不一致，可在 `IL2CPPDump_Log.txt` 中查找：

```text
[voice-lang] observed speakerChannel=...
```

使用日志中的 `speakerChannel` 值修正规则。配音路由仅改变匹配角色的外部语音媒体选择，不会修改游戏资源包。

## 配置与日志

- Mod 配置：`Il2cppDumper.exe` 所在目录下的 `EFStartChange.ini`。
- 注入日志：同目录下的 `IL2CPPDump_Log.txt`。
- 界面设置：`%LocalAppData%\EFStartChange\ui-settings.json`。

界面提供“打开目录”和“查看日志”按钮，不建议在游戏运行时手动编辑配置文件。

## 从源码构建

构建环境：

- Windows 10/11 x64。
- Visual Studio 2022，包含 MSVC v143 C++ 工具集和 Windows SDK。
- .NET SDK `9.0.314` 或符合 `global.json` 滚动策略的 9.0 补丁版本。
- 构建安装器时需安装 Inno Setup 6。

构建原生注入器和自包含 WinUI 单文件应用：

```powershell
pwsh -File .\scripts\BuildEFStartChange.ps1
```

输出目录为 `artifacts\EFStartChange-win-x64`。

构建中文安装器：

```powershell
pwsh -File .\scripts\BuildInstaller.ps1 -Version 1.0.0
```

安装器输出到 `artifacts\installer`。构建过程使用独立临时发布目录，完成后会自动清理。

## 项目结构

- `src/EFStartChange.UI`：WinUI 3 控制器、中文角色名称和动画预设。
- `tools/IL2CPP-Dumper-src/Dump`：注入运行时、模型动画替换和配音路由 Hook。
- `tools/IL2CPP-Dumper-src/Mapper`：自提权启动器和手动映射器。
- `tools/IL2CPP-Dumper-src/third_party/minhook`：MinHook 依赖及其许可证。
- `installer`：中文 Inno Setup 安装器定义与风险说明。
- `scripts`：应用和安装器构建脚本。

## 联系与反馈

- B站主页：[space.bilibili.com/441133155](https://space.bilibili.com/441133155)
- 小黑盒主页：[用户主页 38080236](https://www.xiaoheihe.cn/app/user/profile/38080236)
- QQ群：`851586605`
- 问题反馈：[GitHub Issues](https://github.com/Dr-hydra/EF-Start-Change/issues)

## 风险说明

本项目与鹰角网络、峘形山工作室及 GRYPHLINE 无关，不包含游戏资源、导出表格或反编译程序集。软件按“现状”提供，不附带任何明示或暗示的担保。

本项目不负责停用、规避或对抗反作弊组件。游戏更新后如签名校验失败，请停止使用相关 Hook 并等待适配。使用者应自行备份重要数据、评估账号风险并承担使用后果。
