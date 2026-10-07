# Steam 国服启动预研

日期：2026-10-07。Windows 预搭建已在 `feat/steam-cn-launch` 实现；真实 Steam 接入仍未完成实机验证，正式发布包未包含此功能。使用步骤见 [Steam 国服启动使用与验证](../../../STEAM_CN_LAUNCH.md)。

## 结论

首选路线为 **安装状态 ACF + Steam 启动选项重定向 + XInput 自启动**。
Steam 直接启动国服 `Endfield.exe`，已有 `xinput1_4.dll` 加载 Better Endfield。
无需额外启动器等待游戏退出。BE 管理代理部署与模块配置，Steam 直接拥有游戏启动生命周期；覆盖层和计时是否正确仍需测试。

用户实测：添加本地 EXE 时，Steam 管理员启动后覆盖层工作正常；普通权限仍可记录时长和展示状态。管理员启动因此作为默认关闭的覆盖层兼容选项，不作为计时前提。XInput 不改变游戏权限，不能据此声称解决提权问题。勾选管理员启动而已有客户端未确认提权时，BE 要求先退出再重新启动，保留正常 UAC。

## 安装状态与元数据

- 终末地 Steam AppID 为 `4732690`。商店目前列出的计划发布时间是 2026-10-14。
- ACF 描述安装状态，启动选项决定执行哪个程序；ACF 不切换服务器或改变账号体系。
- 元数据可在未安装游戏时从 SteamDB 或 Steam 的 appinfo 查询，不需要下载国际服本体。
- 2026-10-07 直接访问 SteamDB 配置页返回 HTTP 403；自动功能不应只依赖 HTML 抓取。
- 同日查询鸣潮助手使用的 `api.steamcmd.net/v1/info/4732690` 缓存 API，得到成功状态和游戏名，但未提供启动配置、安装目录、public BuildID 或 Depot 列表。这只是该缓存本次响应的范围，不证明其他渠道没有这些字段。
- 正式接入前必须取得实际 EXE 相对路径、安装目录、Depot、BuildID 与 Manifest；不可推断 `DepotID = AppID + 1`，也不可填写虚构版本。缓存源应与 Steam 元数据核对。

## 已实现范围

1. 设置页识别 Steam 路径和 `libraryfolders.vdf` 中的库；复用已有国服游戏路径与启动参数。
2. 查询鸣潮助手采用的 `api.steamcmd.net` 数据源，支持缓存与手工导入。验证完整字段、Windows 目标、多 Depot 与 UInt64 Manifest；无完整数据时拒绝写入，查询失败保留有效缓存。
3. Steam 完整退出后备份并原子写入 ACF，创建独立目录内符合元数据启动路径的空占位文件。界面与服务双重检查客户端进程，跨实例操作锁避免同时写入；先写归属日志，再写 Steam 文件，中断后可继续提交或移除。
4. 生成指向国服 EXE 的启动选项，例如 `"D:\国服游戏\Endfield.exe" 国服参数 %command%`。用户手动粘贴到 Steam 属性，BE 不修改 `localconfig.vdf`；解析并保留上游启动参数，但不主动混入国服命令。`%command%` 的实际展开是否携带影响国服的国际服参数仍需测试。
5. 通过已有 `XInputDeploymentService` 安装 / 更新自启动代理，保存当前模块配置并选择 XInput 模式。通过 `steam.exe -applaunch 4732690` 发出启动请求；客户端运行时允许启动，文件写入则必须退出。
6. 对 BE 已管理配置，启动时及每小时查询版本，Steam 退出后应用待更新 ACF，保留时长 / 所有者等原字段；占位目录含实际资源、已有其他来源清单或其他库安装时拒绝接管。
7. 独立提供当前 Windows 用户的 `RUNASADMIN` 同步 / 恢复，保留其他兼容标记与原管理员状态；仅只读检查全用户标记。
8. 游戏目录存在本地 `xinput1_4.dll` 时，主界面、快捷方式创建和原生注入器均拒绝二次注入，未知文件不自动清除。

占位目录不应链接到真实国服目录。Steam 更新/校验可能下载国际服资源；
`AutoUpdateBehavior` 不是永久禁用下载的保证。工具应识别状态变化并提供修复，
同时避免覆盖用户已经安装的国际服。

ACF 不授予许可或提前解锁。实际客户端需先正常入库，发行限制仍由 Steam 决定。
国服账号、充值与资源更新仍属于国服；生成 ACF 不会自动增加 Steam 成就或云存档。

## 验证要求

离线服务检查、原生注入器拦截检查和隔离 UI 检查已经执行，涵盖版本更新、运行状态拦截、外部安装保护、中断恢复及管理员标记保留。UI 检查使用合成数据，只证明代码路径与显示行为；不证明真实 Steam 接入可用。

- Steam 的开始/停止状态、好友正在游玩状态与实际游戏生命周期一致。
- Steam 普通权限与管理员权限分别验证覆盖层、截图和 Steam Input；记录游戏实际权限。
- XInput 自动加载 Host 和用户已启用的模块，启动参数与中文路径有效。
- 关闭游戏后状态恢复；既有 Steam 安装、其他库和启动参数不被覆盖。
- 测试新版本 BuildID、下载/校验状态、多个 Windows/Steam 用户及恢复流程。

## 来源

- [终末地 Steam 商店](https://store.steampowered.com/app/4732690/Arknights_Endfield/)
- [Valve SteamPipe 文档：安装状态和启动配置](https://partner.steamgames.com/doc/sdk/uploading)
- [鸣潮助手源码：ACF、占位 EXE 和启动选项](https://github.com/iRyougi/WutheringWavesSteamHelper/blob/master/Services/SteamService.cs)
- [鸣潮助手常见问题：覆盖层与管理员权限](https://www.iryougi.com/index.php/docs/wuwa-helper/faq/)
- [Microsoft ShellExecute 与启动方式](https://learn.microsoft.com/en-us/windows/win32/shell/launch)
- [Microsoft 管理员权限与 UAC](https://learn.microsoft.com/en-us/windows/win32/secbp/running-with-administrator-privileges)
- [RunAsAdmin 源码：当前用户兼容性标记](https://github.com/sboulema/RunAsAdmin)
- 本仓库 `ui/BetterEndfield.UI/Services/XInputDeploymentService.cs`、`native/loaders/injector/main.cpp`。
