# 模型管理悬浮窗与 Android 全局 FOV

软件版本：3.5.1。游戏资源基线：1.5.3。本文描述实现与离线验证范围，不代表实机验收。

## PC

`BetterEndfield.ModelOverlay.exe` 复用 GDI+ 悬浮窗基础；CustomModel Host 发布只含生命周期和接收状态的进程映射，不在窗口线程调用 Unity。默认热键 `PLUS` 表示主键盘 `=`，`ADD` 保持表示小键盘加号。

窗口读取同一 `catalog/custom-model/runtime.ini` 和已安装包。只读包头与 manifest，按路径、mtime、长度和校验模式复用元数据；不解码网格、贴图或预热资源。角色名称复用原 UI 资源，窗口状态、未知字段或未启用包的参数变化不会增加模型重建代次。

窗口和 `BemPackageService` 共用 `Local\BetterEndfield.CustomModel.Settings`。窗口基于最新文件修改操作字段，管理器使用语义基线与增量合并；两端都原子替换文件，保留未改字段和 dormant 参数。显式启用保持同角色互斥，关闭全部覆盖最新文件全部 Mod 条目。主界面仅在显示、文件变化且没有未应用滑条时刷新。

## Android

原 GameOverlay 增加全局 FOV 与模型独立入口，详细选项展开显示。读写由后台设置客户端发往模块应用，复用 BemInstaller 的完整索引、generation、commit 与回滚，以及 BemHotSwitchUpdater 的准备／入队事务；不另复制一份模型库。

跨 UID 调用必须提供框架远程设置中的持久授权令牌，并通过实际 Binder UID、同 Android user 和单包 UID 检查。授权依据框架作用域，未新增必需客户端包名白名单。修改校验最新版本指纹与包代次；旧请求失败后读取权威状态，不覆盖新安装或其他操作。

全局 FOV 保存到原字段，通过独立原子值给 Cinemachine 推送路径读取，不复用自由相机 FOV。相机初始化后可即时应用；未载入 runtime 时游戏内控件禁用，相机未初始化时仅保存并提示重启后生效。普通主相机覆盖继续避开第一人称、自由相机和导入镜头。

设置桥每次模型索引响应限制为 200,000 字符以控制 Binder 大小；超限拒绝本次悬浮窗操作，原 App 管理、已有宽松校验和 native 加载路径保持原行为。热切换、校验和加载模式仍按游戏启动状态处理。

## 验证入口

- `custom_model.overlay_native`：原生模型选择、约束、步长、互斥、全部关闭、并发重试、字段保留、元数据复用及 OEM 热键，54 项通过。
- `custom_model.overlay_settings`：实际桌面保存服务、并发状态合并、缺失包关闭、失败文件保留、热键规范化，27 项通过。
- `android/tools/CheckOverlayHost.ps1`：授权边界、全局 FOV 保存回滚、BEM 模型准备、热切换事务四组通过。输出位于配置 build。
- 原生 BEM 1.3 的 86 项既有回归由实现代理执行通过。
- PC 与 Android 3.5.1 Release 编译通过，PC 安装包构建通过。无游戏、设备安装或实际替换效果验证。

实现入口：`native/modules/custom_model/overlay/INTEGRATION.md`、`BemRuntimeSettings.cs`、`OverlaySettingsProvider.java`、`OverlaySettingsPage.java`、`GlobalFovUpdater.java`。
