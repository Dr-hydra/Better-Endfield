# 1.6.3 Steam 元数据检查

日期：2026-10-15。当前工作区 `G:\Better Endfield`，国服目录 `G:\Arknights Endfield`。本次针对新版，不增加旧版游戏兼容分支。

## 结果

Steam 正式上线后，原先缺少的启动配置、public BuildID 与 Depot Manifest 已全部提供。现有 `SteamMetadata` 解析器和 `SteamIntegrationService.GenerateAcf` 可以使用，无需修改生产代码或硬编码版本。国服启动器校验属于另一项运行时兼容性问题，尚未实测 Steam → 国服 EXE 的启动路径。

| 字段 | 当前实际值 |
| --- | --- |
| AppID | `4732690` |
| Name | `Arknights: Endfield` |
| ReleaseState | `released` |
| InstallDirectory | `Arknights Endfiled` |
| 默认 Executable | `Endfield.exe` |
| 默认 LaunchArguments | 空 |
| 备选启动项 | `Endfield.exe -force-d3d11` |
| public BuildID | `25728898` |
| DepotID | `4732691` |
| public Manifest | `3024011413268238677` |

`Endfiled` 为上游安装目录的实际拼写，原样保留。Manifest 等标识保存为字符串，避免大整数精度损失。这些值是查询时的快照，后续更新仍由现有在线查询取得。

来源：[SteamDB 启动配置](https://steamdb.info/app/4732690/config/)、[public 分支与 Depot](https://steamdb.info/app/4732690/depots/)、[Depot Manifest](https://steamdb.info/depot/4732691/)。现有查询接口 `https://api.steamcmd.net/v1/info/4732690` 返回 HTTP 200，其响应与上述字段一致。

## 本地资料与验证

- [原始 SteamCMD API 响应](../../../../../research/host/1.6.3/Windows/steam-launch/steamcmd-info-4732690.json)
- [可导入 BE 的规范 JSON](../../../../../research/host/1.6.3/Windows/steam-launch/normalized-metadata.json)
- [ACF 预览](../../../../../research/host/1.6.3/Windows/steam-launch/preview-only.acf)
- [最小验证工具及运行方法](../../../../../research/host/1.6.3/Windows/steam-launch/metadata-preview/README.md)

验证工具直接链接生产类，只运行三项必要检查：

1. 原始响应调用 `SteamMetadata.Parse()` 后显式 `Validate()`：通过，`MissingFields=0`。
2. 规范 JSON 序列化后重新解析：应用、安装目录、启动配置、BuildID 和 Depot Manifest 一致。
3. `SteamIntegrationService.GenerateAcf()` 后用 VDF 解析器回读：AppID、安装目录、BuildID 和 Depot Manifest 一致。

验证产物置于 `build/steam-metadata-1.6.3`。没有实例化 Steam 集成服务、缓存到正式 AppData、调用 Apply、写真实 Steam 文件或启动游戏。不需额外哈希、全量资源导出或 IDA 数据库。

## 当前启动限制与下一步

现有 `GenerateLaunchCommand` 生成 `"国服 Endfield.exe 完整路径" 国服参数 %command%`，`SteamEnvironment` 使用 `steam.exe -applaunch 4732690`。SteamDB 的国际服默认 EXE 为 `Endfield.exe`，不证明国服 SDK 接受 Steam 作为父进程。

本机已验证 BE Injector 直接启动被拒，官方 `Games.exe` 启动并经 XInput 加载 BE 成功。对 CN `OneChannelLC.dll` 和相关日志的局部只读检查尚未取得精确允许列表，因此不能断言 Steam 一定失败，也不能宣称只改父进程名称就能绕过。证据及未确认项见[官方启动器检查](../launcher-check/LAUNCHER_CHECK.md)。

已找到官方桌面快捷方式入口：`"D:\Hypergryph Launcher\Launcher.exe" --game=endfield --reason=4`，工作目录为 `D:\Hypergryph Launcher`。它可作为后续接入官方启动流程的依据；是否自动开始游戏、复用现有启动器进程，以及 Steam 状态、时长、覆盖层能否正确跟踪仍未验证。本次没有改变现有启动命令。

后续最小实测应先判断 Steam 直接启动当前 CN 客户端能否通过启动检查。若被拒，再评估接入官方启动链所需改动；此前已成功的官方启动器 + XInput 方式仍是当前有实测依据的可用入口。

## 同日启动桥研究更新

09:32 的实际游戏测试已证明，使用本机正版 `Games.exe` 的暂停进程作为直接父进程，可在不运行其界面的情况下通过国服启动检查。该检查实际验证父进程 basename、文件数字签名和签名证书指纹；同名自制 EXE 不足以满足条件。独立工具会等待目标结束并清理自己创建的暂停父进程，详见[最新启动器检查记录](../launcher-check/LAUNCHER_CHECK.md)。

本次未改 Steam 生产启动选项、ACF 或游戏文件，未重新安装 XInput。Steam 覆盖层、计时、停止状态和 BE 模块加载仍需在接入后另行实测。前文“允许名单未知”的描述为该次元数据查询结束时的状态，精确检查条件现已由小 DLL 局部分析补充。
