# 工作区构建

各入口读取 `config/workspace.defaults.json` 和本机覆盖 `config/workspace.local.json`，也支持显式 `-WorkspaceConfig`。本机工具路径与系统工具不一致时在本机配置的 `tools` 中指定，不修改仓库的版本要求。

## Windows 与 BEM Tools

```powershell
& scripts/BuildBetterEndfield.ps1 -Configuration Release
```

该入口构建 Windows x64 Host、全部内置模块、注入器、悬浮窗、WinUI 管理器和独立 BEM 工具，再检查发布目录。它不运行安装包构建器、不启动游戏、不部署到本机 BE 安装目录。

- PC 发布目录：`build/windows/win-x64/Release/publish/`。
- PC ZIP：`releases/windows/win-x64/Release/BetterEndfield-<version>-win-x64.zip`。
- 独立工具：`releases/tools/bem/BEM-Tools-win-x64.zip`。

需要 VS 2022 C++、CMake 3.25 以上，以及 `global.json` 指定的 .NET SDK。BEM 构建 Python 环境按 `tools/CustomModel/requirements-build.txt` 安装依赖。可以在 `toolchains` 中单独配置这些环境，不必全局安装。PC 安装包另外通过 `scripts/BuildInstaller.ps1` 构建，需要 Inno Setup。

本机 Inno Setup 6.7.3 编译器以独立文件安装到 `toolchains/inno-setup/6.7.3`，`tools.iscc` 指向其中 ISCC.exe。来源为保留上游许可证的 Tools.InnoSetup NuGet 包，编译器 Authenticode 签名有效；没有执行系统安装程序。可用 `BuildBetterEndfield.ps1 -Parallel 4` 限制并发，之后运行 `BuildInstaller.ps1`。

## Android

```powershell
. scripts/Workspace.ps1
$ws = Get-BEWorkspace
Set-BEWorkspaceEnvironment $ws
$env:ANDROID_HOME = $ws.tools.android_sdk
$env:ANDROID_SDK_ROOT = $ws.tools.android_sdk
# JAVA_HOME 指向本机 JDK 17 或更高版本。
& android/gradlew.bat -p android :app:assembleRelease --offline --no-daemon --max-workers=4
```

工具链版本由 `android/app/build.gradle.kts` 指定。首次取得 Gradle/Maven 依赖时去掉 `--offline`；已有依赖可离线构建。SDK 与 Dobby 路径来自工作区配置，不能继续指向备份目录。

- APK 构建目录：`build/android/gradle/app/outputs/apk/release/`。
- 完成后的保留副本：`releases/android/app-<version>/release/`。
- 原生中间目录：`build/android/native/app/`。

Release 签名沿用现有 Gradle 配置；构建不安装 APK，也不连接设备。

## Web

```powershell
& scripts/BuildWeb.ps1
```

入口通过配置中的 Node/npm 恢复锁定依赖，执行 TypeScript 和 Vite 构建。已有依赖时可以使用 `-SkipRestore`。依赖保留在忽略跟踪的 `web/node_modules`，npm 下载缓存、TypeScript 构建记录和最终输出分别位于配置的 `cache/npm` 与 `build/web/`。不部署网站或调用云端发布。

## 验证记录

本次全量构建的日志在 `build/logs/full-build-20261005/`，可随 build 清理；[持久构建摘要](research/not_applicable/workspace-build/BUILD_VALIDATION.md)保留结果、修正项和验证范围。
