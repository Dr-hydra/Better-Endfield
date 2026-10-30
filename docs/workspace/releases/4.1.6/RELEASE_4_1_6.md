# Better Endfield Next 4.1.6

Windows / Android 软件版本 4.1.6，Android versionCode 40106。
BEM Tools / DSH 保持 1.6.5，资源包版本不变。

## 修复与改进

- Windows / Android 模型预览界面在未安装预览资源包时显示明确提示：前往「关于」页点击「下载预览资源」，下载完成后重新打开预览。提示不阻断自带模型资源的包进行预览。支持简体中文和英文。
- 修复 Windows 寻访面板点击「导出」崩溃（#36）：用明确的公共属性 DTO 替换匿名导出对象，避免发布混淆移除构造参数名后导致 JSON 序列化异常。导出格式及旧文件导入保持兼容。
- 寻访导出补齐序列化、权限和文件操作异常处理，失败时显示错误并恢复按钮。

## 下载

| 文件 | 用途 |
| --- | --- |
| BetterEndfieldNext-4.1.6-Setup.exe | Windows x64 安装包 |
| BetterEndfieldNext-4.1.6-Android-arm64.apk | Android arm64 Release APK |
| BEM-Tools-1.6.5-win-x64.zip | Windows 创作者工具，沿用原包 |
| BEM-Tools-1.6.5-android-arm64.tar.gz | Android CLI，沿用原包 |
| dsh-bem-creator-1.6.5.tgz | DSH 插件，沿用原包 |

基础与可选预览资源继续通过 [资源 Release](https://github.com/Dr-hydra/Better-Endfield/releases/tag/game-resources) 分发；本次不重发资源包。
