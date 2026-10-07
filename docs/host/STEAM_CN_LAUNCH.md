# Steam 国服启动（Windows 预览）

本功能在 `feat/steam-cn-launch` 开发分支预搭建，主程序版本仍为 3.5.2。设置页提供独立的“Steam 国服启动（预览）”区域；尚未随正式版发布。

## 配置流程

1. 在 BE 中选择国服 `Endfield.exe`、启动参数和需要启用的模块。
2. 在 Steam 区域检测 `steam.exe`，选择已登记的 Steam 游戏库。
3. 获取元数据，或导入保存的 `api.steamcmd.net` 原始 JSON / BE 规范化 JSON。必须具备应用 `4732690` 的安装目录、Windows 启动 EXE、public BuildID 和 Depot Manifest；缺失数据时只能等待，BE 不会填写猜测值。
4. 在 Steam 菜单选择“退出”，确认托盘中的客户端也已结束。点击“应用 / 更新 ACF”：BE 保存模块设置、切换至 XInput 模式并部署自启动代理，随后写入 ACF 和独立目录内的空占位 EXE。
5. 点击“复制启动命令”，在 Steam 游戏属性的“启动选项”中粘贴。首次手动设置完成后可用 Steam 的开始按钮，或 BE 的“从 Steam 启动国服”。

启动命令为 `"国服 Endfield.exe 完整路径" 已保存的国服参数 %command%`。BE 不自动修改 `localconfig.vdf`。修改国服路径或参数后，应重新复制启动命令。国服资源继续由官方启动器更新；需要先在 Steam 正常入库，ACF 不授予许可或提前解锁。

截至 2026-10-07，本次查询仅返回游戏名称，未返回完整启动与 Depot 信息。界面支持导入和预览；真实 Steam 启动效果仍待完整元数据与实际客户端验证。测试用元数据仅为离线夹具，不可作为真实游戏配置。

## 版本更新和恢复

自动更新默认开启，但只在 BE 已管理此应用时运行：BE 启动时查询，之后每小时查询。Steam 运行时允许查询和缓存；退出后自动更新已归属 BE 的 ACF。应用失败至少等待五分钟再尝试，网络或字段缺失保留上次有效缓存。BE 未运行时不会在后台更新。

BuildID / Manifest 更新保留 `LastPlayed`、`LastOwner` 等未知或运行时字段。安装目录、Windows 启动路径或所选库改变时，需先移除旧配置再重新应用。Steam 启动打断首次配置时，BE 保存待提交记录，客户端退出后可以继续提交或移除。

“移除 BE 的 ACF”只清除 BE 管理的清单、空占位文件和本次创建的空目录，保留备份及 XInput 代理。XInput 由主程序原有安装 / 卸载功能管理。状态与备份位于 `%LOCALAPPDATA%\BetterEndfield\steam`。

BE 拒绝接管其他来源的 ACF、其他库已有安装、包含下载资源的占位目录或目录链接。占位目录与国服目录分开；`AutoUpdateBehavior=1` 不能保证 Steam 永不下载。Steam 下载或校验后目录有实际文件时，BE 会停止更新 / 移除，需检查 Steam 安装状态。

## 管理员权限

“以管理员身份启动 Steam”默认关闭。依据用户当前实测，普通权限仍可记录时长并展示状态；游戏提权时，Steam 提权主要用于覆盖层和截图兼容，效果需按机器验证。

勾选后，BE 通过 Windows `runas` 请求启动，仍遵循 UAC。已有 Steam 未确认提权时，需用户先退出再重启；BE 不结束客户端。“同步 Windows 管理员设置”是独立的显式操作，设置当前用户 `AppCompatFlags\Layers` 中 `steam.exe` 的 `RUNASADMIN`，保留其他兼容选项。“恢复管理员设置”恢复原管理员标记，保留后续其他选项修改。BE 不写所有用户设置；所有用户已要求管理员启动时会提示。

## 避免重复注入

游戏目录已经有 `xinput1_4.dll` 时，BE 启动按钮、创建注入器快捷方式和独立 `BetterEndfield.Injector.exe` 均拒绝内置注入器启动。改用 XInput 模式，或使用 BE 卸载其代理后再用注入器。未知来源的同名文件不会被自动删除。历史快捷方式也会受到新注入器的检查，拒绝时退出码为 6。

## 构建和离线验证

从仓库根目录运行以下命令。所有测试使用独立的合成游戏库、HTTP 响应及注册表存储替身，不修改真实 Steam、国服目录或管理员设置。

```powershell
& 'toolchains/dotnet/dotnet.exe' run --project 'ui/tests/SteamIntegration/SteamIntegration.csproj' -c Release '-p:BEWorkspaceBuildRoot=G:/Better Endfield/build/steam-cn-launch/build' -- 'G:/Better Endfield/build/steam-cn-launch/fixtures'

& 'C:/Program Files/CMake/bin/cmake.exe' --build 'build/steam-cn-launch/native' --config Release --target BetterEndfield.Injector BetterEndfield.InjectorLaunchGuardTests
& 'build/steam-cn-launch/native/Release/BetterEndfield.InjectorLaunchGuardTests.exe' 'build/steam-cn-launch/native-guard-fixture'

$latest = Get-Content 'build/steam-cn-launch/fixtures/latest.json' -Raw | ConvertFrom-Json
& 'ui/tests/SteamIntegration/SmokeUi.ps1' -Executable 'G:/Better Endfield/build/steam-cn-launch/ui-publish/BetterEndfield.exe' -Metadata $latest.previewMetadata -WorkDirectory 'G:/Better Endfield/build/steam-cn-launch/ui-smoke'
```

界面脚本使用 PowerShell 7，在隔离的 `--steam-setup-preview` 窗口检查写入 / 启动按钮禁用、完整 ACF 预览以及普通 / 760×700 窗口截图。隔离预览不载入主程序设置，不安装代理、不写 Steam 文件或注册表、不启动游戏。

离线覆盖包括多 Depot 与 64 位 Manifest、Windows 目标筛选、版本更新、Steam 运行时拒绝写入、既有安装保护、备份 / 移除、中断恢复、管理员选项保留及 XInput 冲突。实际 Steam 开始 / 停止状态、时长、覆盖层、截图、Steam Input 和版本更新必须另行实机验证。
