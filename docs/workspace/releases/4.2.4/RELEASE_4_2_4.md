# Better Endfield Next 4.2.4

Windows / Android 软件版本 4.2.4，Android versionCode 40204。

## 资源覆盖与更新策略修复（Windows / Android）

- **软件安装 / 升级时无条件重置并使用包内最新基础资源（`core`）。**
  - **Windows**：Inno Setup 安装器在 `[InstallDelete]` 中清理旧版 `{app}\resources\core`，构建阶段通过 `BuildResourcePacks.py --bundled` 写入 `.bundled` 版本及构建令牌；无论是运行 `Setup.exe` 覆盖升级还是解压覆盖便携包，UI（`GameResourceStore`）与原生模块（`game_resources.h`）在令牌变更后均会无条件忽略旧 `state.json` 固定记录、加载本次包内基础资源并重置 `auto.core.windows`；之后通过“更新基础资源”或“导入资源包 / 回退”产生的版本选择在同安装包周期内继续保留。
  - **Android**：`GameResourceStore.ensureBundledCore` 通过 APK 内 `bundled-resources/core.zip` 的 SHA-256 与应用 `PackageInfo.lastUpdateTime` 校验安装指纹；当安装或升级 APK 后不再受旧版本或 `state.json` 阻塞，自动清空旧 `core/android` 目录并解压启用随包内置的基础资源；同一次安装后的在线热更与手动导入仍可正常保留。
- **双端资源检查与下载改为直接选取本平台最新版本。**
  - PC（`DistributionService`）与安卓（`DistributionUpdater`）不再强制要求线上资源包的 `gameVersion` 与本地基线严格相等，也不再在安卓端要求预先安装官服包才能检查更新，统一按 `resourceApi == 1` 取本平台（`windows` / `android`）版本号最高的可用 `core` 与 `preview` 包，并按四段版本号比较防止跨基线包重复下载。
