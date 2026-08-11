# EF Start Change UI

EF Start Change 的 WinUI 3 中文控制器。应用负责编辑 `EFStartChange.ini`、显示运行状态并启动原生注入器。

配置文件保存在 `Il2cppDumper.exe` 所在目录。语言开关和角色语言规则会在运行时自动热更新；模型与动画参数仍在下一次启动和注入时读取。

## 界面功能

- 31 个模型预设、32 个配音角色和完整动画索引。
- 最终动作选择及运行时动画时长显示。
- 初始朝向、转身时间、缩放和前倾采样时间。
- 四阶段独立动画速度。
- 原生循环、强制循环和双 Playable 交叉淡化循环。
- 各角色独立的中、英、日、韩配音语言规则，以及可独立开关的剧情语音与口型联动。
- 游戏与注入器路径保存、日志入口、启动和运行状态显示。
- 跟随系统、浅色和深色主题，以及 Windows 11 Mica 背景。
- 控制器快捷方式和一键启动游戏快捷方式。
- 关于页、风险说明、联系方式和按需检查更新。

## 构建

在仓库根目录执行：

```powershell
.\scripts\BuildEFStartChange.ps1
```

自包含单文件应用和原生注入器会输出到 `artifacts\EFStartChange-win-x64`。

构建中文 Inno Setup 安装器：

```powershell
.\scripts\BuildInstaller.ps1 -Version 1.0.2
```

## 开源许可

项目自有代码使用 `AGPL-3.0-only`，完整条款见仓库根目录的 `LICENSE`。第三方组件继续使用其目录中声明的原有许可证。
