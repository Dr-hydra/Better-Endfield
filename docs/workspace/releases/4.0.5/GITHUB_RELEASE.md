Better Endfield Next **4.0.5** 与 BEM Tools **1.5.5**，支持多启动器自动识别与免启动器模式、安卓 1.6.4 四语种短语音目录同步，并整合昼雪角色创作资料。

## Windows 4.0.5

- **多启动器自动识别与切换**：支持官服及各渠道服启动器自动扫描与下拉列表切换，支持手动选择并记住自定义启动器路径，支持从列表移除。
- **免启动器直接拉起模式**：设置页新增“跳过官方启动器”开关，开启后无需打开任何启动器即可直接启动游戏。在 XInput 模式下启用跳过启动器时，自动以管理员权限直接运行游戏主程序，保证注入和游戏正常拉起。
- **Steam 兼容保持**：免启动器模式与 Steam 启动选项配置及伪装 Steam 功能完全兼容，互不干扰。
- **串流触控与多指输入优化**：接入 Unity `InputSystemUIInputModule` 独立指针路由策略（`AllPointersAsIs`），彻底根治 iOS / Moonlight 串流触控及触屏多指操作时 UI 按钮按下不释放卡死的问题；新增 `mouse_to_touch_enabled` 开关，实现原生触控与鼠标模拟转换解耦。（感谢 @orangetooth 的逆向分析与 @yumi233 的适配提交，详见 Issue #29）。

## Android 4.0.5

- **安卓 1.6.4 全语种短语音目录同步**：提取并校验真机（1.6.4 / versionCode 61）最新 VFS 音频分区，完成中、英、日、韩四语种共 1,359 个 Banks 与 14 万+ 条 Media 记录验证，生成包含全部 35 个角色 × 4 种语言（共 140 目录）的完整运行时索引，彻底解决韩语语音缺失与新短语音映射问题。
- **媒体屏管理界面风格统一**：重构媒体屏增强页面，统一为 Better Endfield 原生 UI 风格，完善媒体导入提示与状态反馈。

## BEM Tools 1.5.5

- 将 4.0.4 补充包的角色资料直接整合入完整包，无需单独覆盖补丁；保留昼雪、祀、明河的 Windows 1.6.3 目标合同、profiles 与 keep-only 创作起点工程。

## 下载

| 附件 | 用途 |
| --- | --- |
| `BetterEndfieldNext-4.0.5-Setup.exe` | Windows x64 主程序安装包，包含多启动器与免启动器支持 |
| `BetterEndfieldNext-4.0.5-Android-arm64.apk` | Android 4.0.5 Release APK，集成 1.6.4 四语种短语音路由 |
| `BEM-Tools-1.5.5-win-x64.zip` | 完整独立创作者 GUI、CLI 和全量角色资料 |
