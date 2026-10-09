# 工作区构建

各入口读取 `config/workspace.defaults.json` 和本机覆盖 `config/workspace.local.json`，也支持显式 `-WorkspaceConfig`。本机工具路径与系统工具不一致时在本机配置的 `tools` 中指定，不修改仓库的版本要求。

## Windows 与 BEM Tools

```powershell
& scripts/BuildBetterEndfieldNext.ps1 -Configuration Release
```

该入口构建 Windows x64 Host、全部内置模块、注入器、悬浮窗、WinUI 管理器和独立 BEM 工具，再检查发布目录。它不运行安装包构建器、不启动游戏、不部署到本机 BE 安装目录。

- PC 发布目录：`build/next/windows/win-x64/Release/publish/`。
- 所有发行文件统一存放于 `releases/<软件版本>/`，不再按平台或工具分散目录。
- PC 安装包：`BetterEndfieldNext-<version>-Setup.exe`；本地完整目录 ZIP：`BetterEndfieldNext-<version>-win-x64.zip`，不上传 Windows 应用 ZIP。
- 独立工具：`BEM-Tools-<工具版本>-win-x64.zip`，与第三方模块 SDK 和 Echo 双端示例放在同一软件版本目录。

需要 VS 2022 C++、CMake 3.25 以上，以及 `global.json` 指定的 .NET SDK。BEM 构建 Python 环境按 `tools/CustomModel/requirements-build.txt` 安装依赖。可以在 `toolchains` 中单独配置这些环境，不必全局安装。PC 安装包另外通过 `scripts/BuildInstaller.ps1` 构建，需要 Inno Setup。

本机 Inno Setup 6.7.3 编译器以独立文件安装到 `toolchains/inno-setup/6.7.3`，`tools.iscc` 指向其中 ISCC.exe。来源为保留上游许可证的 Tools.InnoSetup NuGet 包，编译器 Authenticode 签名有效；没有执行系统安装程序。可用 `BuildBetterEndfieldNext.ps1 -Parallel 4` 限制并发，之后运行 `BuildInstaller.ps1`。

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

- APK 构建目录：`build/next/android/gradle/app/outputs/apk/release/`。
- 完成后的保留副本：`releases/<version>/BetterEndfieldNext-<version>-Android-arm64.apk`。
- 原生中间目录：`build/next/android/native/app/`。

Next Release 使用独立的新发布签名，两台机器共用同一份 Next 私钥。首次建立新产品身份时执行：

```powershell
pwsh -File scripts/InitializeNextSigning.ps1 -JdkRoot <JDK>
```

已有 Next 身份的其他构建机应安全转移同一份私钥和本机配置，不重新初始化。Android 密钥配置为 `config/android-next-signing.local.properties`，别名 `next-release`；密码可使用 `BE_ANDROID_STORE_PASSWORD`／`BE_ANDROID_KEY_PASSWORD`。私钥及本机配置均不提交 Git，应另行安全备份。

证书 SHA-256 以 `config/workspace.defaults.json` 为准。Release 在编译前核对 Next 私钥、密码和证书；缺失或不匹配会失败，不回退到旧版或机器的 debug 密钥。Debug 构建保留 Android 原调试行为。构建不安装 APK，也不连接设备。

Windows 发布使用 Next 代码签名身份，当前为内部自签证书；公共 CA 信任需另行提供正式证书。Obfuscar 工具、R8 配置、签名和重新安装要求见 [Next 实施说明](NEXT_IMPLEMENTATION.md)。

## Web

```powershell
& scripts/BuildWeb.ps1
```

入口通过配置中的 Node/npm 恢复锁定依赖，执行 TypeScript 和 Vite 构建。已有依赖时可以使用 `-SkipRestore`。依赖保留在忽略跟踪的 `web/node_modules`，npm 下载缓存、TypeScript 构建记录和最终输出分别位于配置的 `cache/npm` 与 `build/web/`。不部署网站或调用云端发布。

## 验证记录

本次全量构建的日志在 `build/logs/full-build-20261005/`，可随 build 清理；[持久构建摘要](research/not_applicable/workspace-build/BUILD_VALIDATION.md)保留结果、修正项和验证范围。
