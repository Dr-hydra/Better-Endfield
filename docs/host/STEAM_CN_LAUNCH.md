# Steam 国服启动（Windows 预览）

本功能自 Windows 3.5.3 起提供。4.0.4 自动填写和更新当前 Steam 账号的启动选项，修复直接运行占位 EXE 时的“平台无效”；用户已确认启动修复可用。配置沿用主界面的注入器或 XInput 选择，不会自动安装 XInput。Steam 计时、覆盖层、截图及 Steam Input 仍需单独实机验证。

## 当前版本状态（2026-10-15）

应用 AppID 为 `4732690`，正式元数据已提供安装目录、Windows 启动程序、public BuildID 和 Depot Manifest。使用“获取元数据”取得最新资料，或导入与当前游戏版本一致的 JSON；目录与版本字段以本次解析结果为准，不在程序或使用说明中硬编码。

仓库保留的[规范 JSON](../../research/host/1.6.3/Windows/steam-launch/normalized-metadata.json)和 [ACF 预览](../../research/host/1.6.3/Windows/steam-launch/preview-only.acf)是早期查询快照，其中曾出现 `Arknights Endfiled` 拼写；它们不证明当前安装目录仍采用该值。本机当前配置已使用 `Arknights Endfield`，后续仍应遵循最新导入资料，并在 ACF 预览中核对路径。历史字段来源见 [1.6.3 Steam 元数据检查](research/1.6.3/steam-launch/STEAM_METADATA_20261015.md)，4.0.4 的实际验证范围见[发布记录](../workspace/releases/4.0.4/RELEASE_4_0_4.md)。

国服 1.6.3 已实际执行启动器父进程检查。4.0.3 的 Injector 和 Launcher 共用新的启动适配，自动寻找并验证本机正版 `Games.exe`，借用其暂停进程启动游戏，官方启动器界面无需运行。该方案已通过真实国服启动检查；本机仍需保留正版启动器文件，并遵循正常 UAC 授权。证据及验证边界见[启动器检查记录](research/1.6.3/launcher-check/LAUNCHER_CHECK.md)。

## 配置流程

1. 在 BE 中选择国服 `Endfield.exe`、启动参数、加载方式和需要启用的模块。注入器模式不需要 XInput；若选择 XInput，应通过现有“安装或更新”按钮自行部署。
2. 在 Steam 区域检测 `steam.exe`，选择已登记的 Steam 游戏库。
3. 获取元数据，或导入保存的 `api.steamcmd.net` 原始 JSON / BE 规范化 JSON。必须具备应用 `4732690` 的安装目录、Windows 启动 EXE、public BuildID 和 Depot Manifest；缺失数据时只能等待，BE 不会填写猜测值。
4. 在 Steam 菜单选择“退出”，确认托盘中的客户端也已结束。点击“应用 / 更新配置”：写入 ACF 和独立目录内的空占位 EXE，并自动填写当前账号的启动选项。
5. 配置成功后，可直接使用 Steam 的开始按钮或 BE 的“从 Steam 启动国服”。BE 的启动按钮会再次核对命令；需要修改而 Steam 仍运行时，会提示先退出客户端，再点击启动自动更新。

启动命令为 `"BE 启动组件完整路径" --hide-console --game "国服 Endfield.exe 完整路径" -- 已保存的国服参数 --steam-command %command%`。注入器模式使用 `BetterEndfieldNext.Injector.exe`，XInput 模式使用 `BetterEndfieldNext.Launcher.exe`；辅助程序隐藏控制台并保留错误提示，Steam 附带的原始命令不会混入国服参数。

4.0.4 自动修改所选 Steam 账号 `localconfig.vdf` 中此游戏的 `LaunchOptions` 字段，保留其他文本、游戏和账号配置。“复制启动命令”保留为手动配置入口，正常流程无需自行粘贴。切换账号、加载方式、BE 路径或国服参数后，请在 Steam 退出时使用“应用 / 更新配置”或 BE 的启动按钮刷新；每小时元数据查询不会在后台修改启动选项。

Steam 运行时使用当前活动账号；退出时按最近登录记录选择，存在歧义会要求先登录目标账号，BE 不猜测账户。首次接管会保存原启动选项；此后若用户另外改了该值，BE 会拒绝静默覆盖。可以手动填入当前生成的完整命令后重试。国服资源继续由官方启动器更新；需要先在 Steam 正常入库，ACF 不授予许可或提前解锁。

2026-10-07 的查询曾仅返回游戏名称，该数据缺口现已补齐。测试用合成元数据仍仅为离线夹具；应使用最新在线数据或上文保存的正式数据快照。

## 版本更新和恢复

自动更新默认开启，但只在 BE 已管理此应用时运行：BE 启动时查询，之后每小时查询。Steam 运行时允许查询和缓存；退出后自动更新已归属 BE 的 ACF。应用失败至少等待五分钟再尝试，网络或字段缺失保留上次有效缓存。BE 未运行时不会在后台更新。

BuildID / Manifest 更新保留 `LastPlayed`、`LastOwner` 等未知或运行时字段。安装目录、Windows 启动路径或所选库改变时，需先移除旧配置再重新应用。Steam 启动打断首次配置时，BE 保存待提交记录，客户端退出后可以继续提交或移除。

“移除 BE 配置”先恢复 BE 管理过的账号各自原启动选项，再清除归属 BE 的清单、空占位文件和本次创建的空目录。其他字段的后续修改会保留；启动选项已被另行更改时停止恢复，避免覆盖。备份及 XInput 代理保留，XInput 继续由原有安装 / 卸载功能管理。状态与备份位于 `%LOCALAPPDATA%\BetterEndfieldNext\steam`。

游戏目录根部或元数据指定 EXE 旁的 `steam_appid.txt`，若是普通小文件（至多 128 字节），UTF-8/ASCII 内容去掉可选 BOM、首尾空白及 NUL 后恰好为 `4732690`，可直接保留并更新。普通移除也保留该文件和因此非空的目录。其他位置同名文件、错误 AppID、异常内容或链接不属于此白名单。

## 冲突文件与确认清理

发现其他来源的 ACF、非空 EXE、日志或其他未归属 BE 的文件时，自动更新和普通移除不会删除它们。手动点击“应用 / 更新配置”可查看清理预览，显示完整 Steam 游戏目录、ACF 路径、文件数与总字节数；选择“删除并重新配置”才永久删除预览中的内容。已下载的 Steam 国际服资源也会被删除，程序只备份小型清单和配置记录，不备份游戏内容；取消则不删除。

清理仅限已登记 Steam 库中的指定游戏目录。国服目录、BE 安装和状态目录、其他库的同游戏安装、目录链接及重解析点都不能通过确认绕过。执行前复核目录快照，确认期间内容变化会停止；逐项删除中出现新文件也不会递归带走。Steam 或游戏运行时拒绝清理，中途启动、文件占用或取消会停止；未完成操作需退出客户端后再次点击“应用 / 更新配置”，重新查看并确认剩余范围。

占位目录始终与国服目录分开。`AutoUpdateBehavior=1` 不保证 Steam 永不下载；校验或更新可能下载国际服文件，遇到上述冲突应先检查清理预览，确认是否要删除已下载资源。

## 管理员权限

“以管理员身份启动 Steam”默认关闭。依据用户当前实测，普通权限仍可记录时长并展示状态；游戏提权时，Steam 提权主要用于覆盖层和截图兼容，效果需按机器验证。

勾选后，BE 通过 Windows `runas` 请求启动，仍遵循 UAC。已有 Steam 未确认提权时，需用户先退出再重启；BE 不结束客户端。“同步 Windows 管理员设置”是独立的显式操作，设置当前用户 `AppCompatFlags\Layers` 中 `steam.exe` 的 `RUNASADMIN`，保留其他兼容选项。“恢复管理员设置”恢复原管理员标记，保留后续其他选项修改。BE 不写所有用户设置；所有用户已要求管理员启动时会提示。

## 避免重复注入

游戏目录已经有 `xinput1_4.dll` 时，BE 启动按钮、创建注入器快捷方式和独立 `BetterEndfieldNext.Injector.exe` 均拒绝内置注入器启动。改用 XInput 模式，或使用 BE 卸载其代理后再用注入器。未知来源的同名文件不会被自动删除。历史快捷方式也会受到新注入器的检查，拒绝时退出码为 6。

## 构建和离线验证

从仓库根目录运行以下命令。所有测试使用独立的合成游戏库、HTTP 响应及注册表存储替身，不修改真实 Steam、国服目录或管理员设置。

```powershell
& 'toolchains/dotnet/dotnet.exe' run --project 'ui/tests/SteamIntegration/SteamIntegration.csproj' -c Release '-p:BEWorkspaceBuildRoot=G:/Better Endfield/build/steam-cn-launch/build' -- 'G:/Better Endfield/build/steam-cn-launch/fixtures'

& 'C:/Program Files/CMake/bin/cmake.exe' --build 'build/steam-cn-launch/native' --config Release --target BetterEndfieldNext.Injector BetterEndfieldNext.InjectorLaunchGuardTests
& 'build/steam-cn-launch/native/Release/BetterEndfieldNext.InjectorLaunchGuardTests.exe' 'build/steam-cn-launch/native-guard-fixture'

$latest = Get-Content 'build/steam-cn-launch/fixtures/latest.json' -Raw | ConvertFrom-Json
& 'ui/tests/SteamIntegration/SmokeUi.ps1' -Executable 'G:/Better Endfield/build/steam-cn-launch/ui-publish/BetterEndfieldNext.exe' -Metadata $latest.previewMetadata -WorkDirectory 'G:/Better Endfield/build/steam-cn-launch/ui-smoke'
```

界面脚本使用 PowerShell 7，在隔离的 `--steam-setup-preview` 窗口检查写入 / 启动按钮禁用、完整 ACF 预览以及普通 / 760×700 窗口截图。隔离预览不载入主程序设置，不安装代理、不写 Steam 文件或注册表、不启动游戏。

4.0.4 的 204 项离线检查涵盖多 Depot 与 64 位 Manifest、Windows 筛选、账号选择和启动选项恢复、Steam 运行时拒绝写入、辅助 AppID 文件保留、确认清理与中断恢复、目录和链接保护、管理员选项保留及 XInput 冲突。用户已确认本轮 Steam 启动修复；计时、覆盖层、截图、Steam Input 和后续 Steam 更新行为仍需分别验收。
