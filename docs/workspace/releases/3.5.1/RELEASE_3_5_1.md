# Better Endfield 3.5.1

## 更新

- Android 悬浮窗新增全局 FOV 开关与 5–150° 滑条，独立于自由相机 FOV，调整保存到原设置。
- 双端模型悬浮窗支持按角色筛选、关闭全部、每角色互斥启用、外观、组件和形态参数，详细选项按需展开。
- PC 默认按主键盘 `=` 显示／隐藏模型悬浮窗，无需 Shift；支持自定义组合键。PC 窗口跟随游戏、适配 DPI，支持中英界面及现有角色名称表。
- 保存继续走原模型库和热切换流程，保留原模型恢复、参数记忆和校验开关；未启用热切换时保存选择，在重启后生效。
- PC 模型设置采用跨进程互斥与增量合并；窗口开关不会触发模型重建。Android 设置桥使用框架授权及包代次、版本冲突校验。
- 工具链和构建输出接入工作区配置，修正 BEM 工具增量打包时只读目录造成的失败。

## 下载

- `BetterEndfield-3.5.1-Setup.exe`：Windows x64 安装包。
- `BetterEndfield-3.5.1-win-x64.zip`：Windows x64 完整目录包。
- `BetterEndfield-3.5.1-Android-arm64.apk`：Android ARM64 模块，versionCode 30501。
- `BEM-Tools-1.5.0-win-x64.zip`：独立创作者工具，版本保持 1.5.0。

## 验证范围

PC 原生模块、新悬浮窗、WinUI、BEM Tools、Inno Setup 安装包与 Android Release 构建通过。PC 模型回归 54 项通过；桌面真实模型保存服务及热键回归 27 项通过；Android 四组主机回归通过，覆盖授权、设置回滚、模型准备和热切换事务。APK 签名及双端版本号核对通过。

热切换沿用原资源重载与实例更新时机；“已接收”不代表当前模型已完成加载。未运行游戏、安装 APK 或覆盖本机测试安装目录，新功能的游戏内效果仍需实机确认。

## English

- Added a global FOV toggle and 5–150° slider to the Android overlay, separate from free-camera FOV and saved to the existing settings.
- Added model-management overlays on both platforms: character filtering, disable all, one active package per character, appearances, components and shape parameters with expandable details.
- The Windows model overlay uses the main keyboard **`=` without Shift** by default, supports custom chords, follows the game window and uses the existing character names and Chinese/English UI.
- Uses the existing model library, hot-switching and rollback paths. Concurrent desktop/overlay edits preserve unrelated selections; Android writes use the framework-authorized settings bridge.
- Includes Windows installer/ZIP, Android APK and BEM Tools 1.5.0. Production builds and offline regressions passed; new in-game behavior has not been device-tested.
