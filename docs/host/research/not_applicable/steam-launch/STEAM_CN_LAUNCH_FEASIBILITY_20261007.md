# Steam 国服启动预研

日期：2026-10-07。此记录是方案和证据，不代表已完成 Steam 接入或实机验证。

## 结论

首选路线为 **安装状态 ACF + Steam 启动选项重定向 + XInput 自启动**。
Steam 直接启动国服 `Endfield.exe`，已有 `xinput1_4.dll` 加载 Better Endfield。
无需额外启动器等待游戏退出；覆盖层和计时是否正确仍需测试。

用户实测：添加本地 EXE 时，Steam 需要管理员启动，覆盖层才工作正常。
XInput 不改变游戏权限，不能据此声称解决提权问题。接入时应检查游戏和
Steam 的权限；若游戏提权，应提示以管理员身份重启 Steam。

## 安装状态与元数据

- 终末地 Steam AppID 为 `4732690`。商店目前列出的计划发布时间是 2026-10-14。
- ACF 描述安装状态，启动选项决定执行哪个程序；ACF 不切换服务器或改变账号体系。
- 元数据可在未安装游戏时从 SteamDB 或 Steam 的 appinfo 查询，不需要下载国际服本体。
- 2026-10-07 直接访问 SteamDB 配置页返回 HTTP 403；自动功能不应只依赖 HTML 抓取。
- 同日查询鸣潮助手使用的 `api.steamcmd.net/v1/info/4732690` 缓存 API，得到成功状态和游戏名，但未提供启动配置、安装目录、public BuildID 或 Depot 列表。这只是该缓存本次响应的范围，不证明其他渠道没有这些字段。
- 正式接入前必须取得实际 EXE 相对路径、安装目录、Depot、BuildID 与 Manifest；不可推断 `DepotID = AppID + 1`，也不可填写虚构版本。缓存源应与 Steam 元数据核对。

## 实现范围

1. 在设置页识别 Steam 路径和 `libraryfolders.vdf` 中的库；复用已有国服游戏路径。
2. 读取并验证当前启动与安装元数据；支持缓存、手工导入和查询失败状态。
3. Steam 完整退出后，备份并原子写入 ACF，创建符合官方启动路径的占位文件。记录工具创建的文件，保留恢复功能。
4. 生成指向国服 EXE 的启动选项，例如 `"D:\国服游戏\Endfield.exe" %command%`。已有启动参数须保留；国际服专用渠道/认证参数不可直接带入国服。
5. 使用已有 `XInputDeploymentService` 安装自启动代理，保留归属检查和恢复能力。Steam 确认安装状态后可通过库中的开始按钮或 `steam://rungameid/4732690` 启动。

占位目录不应链接到真实国服目录。Steam 更新/校验可能下载国际服资源；
`AutoUpdateBehavior` 不是永久禁用下载的保证。工具应识别状态变化并提供修复，
同时避免覆盖用户已经安装的国际服。

ACF 不授予许可或提前解锁。实际客户端需先正常入库，发行限制仍由 Steam 决定。
国服账号、充值与资源更新仍属于国服；生成 ACF 不会自动增加 Steam 成就或云存档。

## 验证要求

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
- 本仓库 `ui/BetterEndfield.UI/Services/XInputDeploymentService.cs`、`native/loaders/injector/main.cpp`。
