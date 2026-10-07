# Better Endfield 3.5.3

## 更新

- Windows 设置页新增 **Steam 国服启动（预览）**：查询、缓存或导入元数据，生成安装状态 ACF 和国服启动命令，使用 BE 管理的 XInput 自启动代理。首次需将生成的命令粘贴到 Steam 游戏的启动选项。
- BE 对已管理配置在启动时及每小时查询新版本；Steam 运行期间只缓存，完整退出（含托盘后台）后更新 ACF。写入与移除提供备份、归属检查及中断恢复，不接管现有国际服安装。
- Steam 管理员启动默认关闭，主要作为覆盖层兼容选项；可同步 / 恢复当前 Windows 用户的管理员兼容标记，保留其他兼容选项，仍使用正常 UAC。
- 游戏目录存在 `xinput1_4.dll` 时，启动按钮、注入器快捷方式及独立注入器均拒绝二次注入；XInput 部署、更新及卸载仍由 BE 管理。
- **BEM Tools 1.5.2** 独立 ZIP 新增可直接打开的 `BetterEndfield.BemTools.exe` 创作者 GUI，与主程序共用创作者窗口，完整解压后无需安装 Python 或 .NET。
- Windows / Android 版本统一为 3.5.3（Android versionCode 30503）。本轮 Steam 和注入器改动仅适用于 Windows，Android 功能沿用 3.5.2。

## 下载

- `BetterEndfield-3.5.3-Setup.exe`：Windows x64 安装包。
- `BetterEndfield-3.5.3-Android-arm64.apk`：Android ARM64 模块。
- `BEM-Tools-1.5.2-win-x64.zip`：独立创作者 GUI、CLI、Blender 插件、示例、文档及 Skill。
- `BetterEndfield-ThirdPartySDK-1.0.0.zip`：第三方原生模块 SDK。
- `BetterEndfield-Echo-1.0.0-Dual.zip`：Windows / Android 双端模块示例。

## Steam 预览边界

截至 2026-10-07，本次元数据查询尚缺 Windows 启动路径、安装目录、public BuildID 与 Depot Manifest。BE 不使用猜测数据生成真实配置；完整数据公布前可以查看界面、导入有效数据或预览 ACF。

实际 Steam 启动、游戏生命周期、计时、覆盖层、截图和 Steam Input 尚待实机验证。普通权限仍能记录时长与展示状态来自用户当前实测，不代表已完成所有 Steam 场景验证。ACF 不授予许可或提前解锁；国服资源仍由官方启动器更新。

使用与恢复说明见 [Steam 国服启动](../../../host/STEAM_CN_LAUNCH.md)。

## 验证范围

Steam 服务已有 64 项 C# 离线检查、3 项原生文件检查；原生注入器三种启动入口已验证冲突退出码 6。隔离界面验证涵盖完整 ACF 内容、写入 / 启动按钮禁用及普通 / 760×700 窗口布局，未修改真实 Steam 配置、注册表或游戏目录。

Windows 全量 Release、WinUI、BEM Tools GUI / CLI、Inno Setup、Android Release 及 APK 签名 / 版本检查通过。冻结工具 ZIP 在中文路径下完成 GUI 工程打开、BEM 1.4 导出、Android 武器目标校验和内置帮助检查；SDK / Echo 双端构建与包结构检查通过。 未连接 Android 设备，未将离线或构建验证视为游戏内验证。

## English

- Added a Windows **Steam CN launch preview** with validated metadata, ACF generation, CN launch options and the BE-managed XInput loader. Steam must be fully closed for configuration changes; metadata can be cached while it is running.
- Added automatic metadata refresh for managed configurations, backups and interrupted-setup recovery. Steam elevation is optional and off by default, with reversible current-user Windows compatibility settings.
- Blocked built-in injector launches and injector shortcuts when a local `xinput1_4.dll` is present, preventing duplicate loading.
- Standalone **BEM Tools 1.5.2** now includes the creator GUI alongside its CLI and authoring resources.
- Windows and Android are version 3.5.3; Android functionality carries forward from 3.5.2.

Complete Steam metadata and real-client validation are still pending. This release does not claim verified Steam launch, playtime, overlay or controller behavior, and an ACF does not grant a Steam entitlement or early access.

Production Windows/Android builds, the installer, APK signing/version checks, the portable creator GUI export workflow, and SDK/Echo package validation passed. No Android device was attached for this release.
