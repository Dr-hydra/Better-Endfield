# 新工作区全量构建验证

记录日期：2026-10-05。工作区：`F:\Better Endfield`。分支：`chore/workspace-reorganization`，基线提交：`20ebf08`，包含本轮未提交的构建修正。软件版本为 3.5.0，游戏版本不影响此构建结论。

## 结果与产物

| 范围 | 实际结果 | 产物 |
| --- | --- | --- |
| Windows x64 原生运行时、内置模块、注入器与悬浮窗 | Release 编译通过 | `build/windows/win-x64/Release/native/stage/Release/` |
| WinUI 桌面管理器与完整 PC 目录 | 自包含发布及目录校验通过 | `build/windows/win-x64/Release/publish/` |
| PC ZIP | 生成通过，约 79.09 MiB | `releases/windows/win-x64/Release/BetterEndfield-3.5.0-win-x64.zip` |
| 独立 BEM Tools | PyInstaller 冻结及工具链打包通过，约 9.99 MiB | `releases/tools/bem/BEM-Tools-win-x64.zip` |
| Android ARM64 | `assembleRelease` 通过，约 10.12 MiB | `releases/android/app-3.5.0/release/app-release.apk` |
| Web | TypeScript 与 Vite 生产构建通过 | `build/web/dist/` |
| PC 安装程序 | 按用户要求跳过 | 未调用 Inno Setup |

Android 构建从空的新原生/Gradle构建目录执行了 55 项任务。PC 首次构建完成全部生产原生目标，补齐 SDK 后复用原生结果继续发布 UI 和打包。未构建 `EXCLUDE_FROM_ALL` 的诊断、测试或 legacy 研究目标。

## 发现与修正

1. PC 发布目录按公开 basename 保存说明文档；迁移后校验误用了源码模块子目录。已将要求的路径修正为 `docs/BEM_CREATOR_GUIDE.md`。
2. Android Gradle Plugin 9.1.1 拒绝旧 SourceSet API 接收 Provider。资产目录现在传入解析后的 File，已有 `preBuild` 依赖继续保证生成任务先完成。
3. 本机只有 .NET SDK 9.0.308，低于 `global.json` 要求的 9.0.314。将所需 SDK 单独安装到 `toolchains/dotnet/`，本机配置选择该程序，未降低仓库版本要求。
4. 本机 CMake 3.31.6 位于现有 VS 2022 安装目录，已通过本机配置引用。BEM 构建依赖在 `toolchains/python/bem-build/` 中准备，PyInstaller 为 6.20.0，zstandard 为 0.25.0。
5. 增加 `BuildWeb.ps1`，通过共同配置读取 Node/npm，TypeScript 构建记录与 Vite 输出进入 build，npm 下载缓存进入 cache。

## 实际检查及边界

- PC ZIP 的 157 项入口核对通过，主要 DLL/EXE、工具与文档存在；没有 `.becat/.wem/.pck/.bnk` 游戏音频载荷。
- 独立 BEM Tools ZIP 的 123 项入口核对通过，包含冻结程序与英文创作者指南；冻结 CLI 的 `--help` 启动成功。
- APK 包含资源索引、两份动作资源和两份 ARM64 原生库。`apksigner verify` 成功，现有 v2 签名有效；沿用项目现有 Release 签名配置。
- 工作区配置的 11 项模拟检查通过，Web 构建脚本 PowerShell 语法检查通过。
- 原生代码及 Android 依赖仍有已有的弃用、未使用函数和 SDK 目录提示，本次没有为消除提示改变运行时代码。
- 没有启动游戏、安装 APK、覆盖 `E:\Better Endfield`、刷新真实游戏资源、部署网站或推送 Git。构建和包结构成功不能证明游戏内效果。

普通日志保存在 `build/logs/full-build-20261005/`，允许清理；上述发布包保存在 releases，摘要保留在本专题。
