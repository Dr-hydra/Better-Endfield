# Better Endfield UI

Better Endfield 的 WinUI 3 中文控制器。应用负责编辑模块配置、显示运行状态并启动原生注入器。

项目位于仓库的 `ui/BetterEndfield.UI`。`Assets/model` 保存模型预设与角色名，`Assets/voice` 保存不含音频内容的紧凑语音索引，`Assets/shared` 保存 UI 与安装器共用的品牌资源。

配置文件保存在 `%LocalAppData%\BetterEndfield\BetterEndfield.ini`。语言开关和角色语言规则会在运行时自动热更新；模型与动画参数仍在下一次启动并加载 Host 时读取。

## 界面功能

- 31 个模型预设、32 个配音角色和完整动画索引。
- 最终动作选择及运行时动画时长显示。
- 初始朝向、转身时间、缩放和前倾采样时间。
- 四阶段独立动画速度。
- 原生循环、强制循环和双 Playable 交叉淡化循环。
- 各角色独立的中、英、日、韩配音语言规则，以及可独立开关的剧情语音与口型联动。
- 保存时从本机已下载的语言 PCK 选择性生成角色 catalog；音频内容只写入 `%LocalAppData%\BetterEndfield\catalog`，不会进入发布包。
- 游戏与注入器路径自动发现、有效性提示、手动重扫和日志入口。
- 自定义游戏启动参数，例如使用 `-force-d3d11` 选择 Direct3D 11。
- 独立相机增强模块：可配置热键的自由视角、方向键移动、原生鼠标旋转、速度/FOV 调节和近距离反虚化。
- 界面增强支持隐藏 UID，以及通过原生 UI 相机遮罩和自定义热键隐藏或恢复游戏内全部界面，同时保留角色与镜头操作。
- 跟随系统、浅色和深色主题，以及 Windows 11 Mica 背景。
- 控制器快捷方式和一键启动游戏快捷方式。
- 内置注入器与 XInput 自启动两种加载方式；XInput 可安装、更新和安全卸载。
- XInput 适用于兼容其他加载器或从官方启动器自启动，并明确提示会写入游戏目录。
- 关于页、风险说明、联系方式和按需检查更新。

## 构建

在仓库根目录执行：

```powershell
.\scripts\BuildBetterEndfield.ps1
```

自包含单文件应用和原生注入器会输出到 `artifacts\BetterEndfield-win-x64`。

构建中文 Inno Setup 安装器：

```powershell
.\scripts\BuildInstaller.ps1 -Version 2.3.1
```

## 开源许可

项目自有代码使用 `AGPL-3.0-only`，完整条款见仓库根目录的 `LICENSE`。第三方组件继续使用其目录中声明的原有许可证。
