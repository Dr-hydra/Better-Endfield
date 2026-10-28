# Better Endfield Next 4.1.5

Windows / Android 软件版本 4.1.5，Android versionCode 40105。
BEM Tools / DSH 保持 1.6.5，资源包版本不变。

## 修复与新增

- Windows 外置资源定位从程序集解包临时目录改为实际可执行文件所在目录，修复单文件发行版无法读取已随安装器附带的基础资源。
- Android APK 携带独立格式的基础资源 ZIP，首次启动本地校验解压至版本目录，不要求联网下载后才能使用。已有资源与手动固定版本保持原有选择。可选预览包仍单独分发。
- Android 声明游戏包可见性，修复 Android 11+ 查询已安装游戏版本时可能失败的问题；查询失败显示明确提示。
- Android 资源更新失败按索引、游戏版本、下载、安装阶段写入 logcat 与私有 cache/betterendfieldnext-resources.log。旧版异常只显示在界面，未记录完整堆栈，无法从旧日志还原本次手机错误。
- 关于页新增“语言 / Language”入口：跟随系统、简体中文、English。跟随系统时中文环境用中文，其余环境用英文。统一 XML、Java 动态文案、模型预览与游戏悬浮窗语言，不修改游戏进程的全局 Locale。
- Android 配音列表与实际配音目录生成同时兼容 schema 1 / 2 资源表，修复新基础包导致角色配音页面加载失败。保留 Media ID 冲突校验，避免跨语言映射歧义。
- Gradle 发布归档不跟踪包含其他平台安装器的共享输出目录，避免同时打包时因扫描被占用文件导致失败。

## 本地验证边界

- Windows 混淆、签名后的实际单文件成品离线自检通过：基础包 1.6.3.2、126 个资源文件、37 个角色。
- Windows 资源存储 13 项检查、语言策略 12 项检查通过。
- Android 离线 Release 构建、APK 签名校验通过；包内基础包 1.6.4.3 的全部 6 个数据文件逐项校验长度通过，清单包含游戏 package query。
- 未重新下载资源 ZIP；未计算产物哈希；已通过 adb 保留数据覆盖安装到手机并冷启动，确认基础包 1.6.4.3 已本地解压并激活；未进行新增游戏内实机验收。
- 配音解析 18 项检查通过，完整解析当前资源中 140 份目录、56,664 条路由；修复版已保留数据安装到手机，用户确认恢复。
- 发布流程 4 项回归通过。

## 下载

| 文件 | 用途 |
| --- | --- |
| BetterEndfieldNext-4.1.5-Setup.exe | Windows x64 安装包 |
| BetterEndfieldNext-4.1.5-Android-arm64.apk | Android arm64 Release APK |
| BEM-Tools-1.6.5-win-x64.zip | Windows 创作者工具，沿用上个版本原包 |
| BEM-Tools-1.6.5-android-arm64.tar.gz | Android CLI，沿用上个版本原包 |
| dsh-bem-creator-1.6.5.tgz | DSH 插件，沿用上个版本原包 |

基础与可选预览资源继续通过 [资源 Release](https://github.com/Dr-hydra/Better-Endfield/releases/tag/game-resources) 分发；本次不重发资源包。
