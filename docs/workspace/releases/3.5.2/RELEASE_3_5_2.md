# Better Endfield 3.5.2

## 更新

- BEM Tools 1.5.1 正式支持 BEM 1.4：显式资源目标、静态网格、平台/LOD 与 donor 声明，覆盖武器和角色大招形态；旧版 1.0–1.3 包保持兼容。
- 修正双端模型热切换的发现、实例更新与恢复路径，降低重复诊断扫描造成的切换卡顿。
- Android PCUI 接入相对鼠标输入，修正桌面布局在不同 DPI 下的定位；游戏原生手柄输入继续由游戏处理，未新增手柄映射器。
- 第三方模块与创意工坊入口统一到 Android 增强功能页，桌面端提供独立 Workshop 导航。

## 下载

- `BetterEndfield-3.5.2-Setup.exe`：Windows x64 安装包。
- `BetterEndfield-3.5.2-Android-arm64.apk`：Android ARM64 模块，versionCode 30502。
- `BEM-Tools-1.5.1-win-x64.zip`：BEM 1.4 创作者工具。
- `BetterEndfield-ThirdPartySDK-1.0.0.zip`：第三方原生模块创作者 SDK。
- `BetterEndfield-Echo-1.0.0-Dual.zip`：第三方模块 Windows／Android 双端示例。

## 验证范围

Windows Release、WinUI、BEM Tools、Inno Setup、Android Release、APK 签名、SDK/Echo 构建和离线回归通过后发布。BEM creator smoke 使用冻结工具包执行；未连接 Android 设备，未宣称武器/大招或 PCUI 输入已完成实机验证。用户配置未重置，未安装覆盖本机测试目录。

## English

- BEM Tools 1.5.1 formally supports BEM 1.4 resource targets and static meshes for weapons and ultimate forms, with explicit platform/LOD/donor declarations and compatibility with 1.0–1.3 packages.
- Fixed model hot-switch discovery, instance updates and restoration, reducing repeated diagnostic scans that caused stutter.
- Android PCUI now bridges relative mouse input and desktop layout placement is DPI-aware. The game’s native controller path remains available; no custom mapper was added.
- Third-party Modules and Workshop are grouped under Android Enhancement, with an independent Workshop navigation entry on desktop.

The release includes the Windows installer, Android APK, BEM Tools 1.5.1, ThirdPartySDK 1.0.0 and the dual-platform Echo example. Production builds and offline regressions are recorded after the final artifacts are produced; no Android device was attached for this release.
