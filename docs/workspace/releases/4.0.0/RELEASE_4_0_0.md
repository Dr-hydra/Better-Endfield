# Better Endfield Next 4.0.0

4.0.0 是使用独立安装身份的 Next 版本，Windows 与 Android 均需重新安装和设置。BEM / MMD 用户素材格式继续支持，可手动重新导入。

## 更新

- 更名为 **Better Endfield Next**，保留原 Logo；更新双端安装身份、内部模块与通信标识、发布签名。
- 移除双端第一人称功能，相关代码、工具、测试与原始快照归档至 `legacy/retired-before-next/`，不参与当前构建。
- 暂时隐藏双端第三方模块管理入口和旧入口路由，保留模块装载器、网页桥及管理实现。旧模块二进制不承诺兼容。
- Android 新增包含多个 BEM 的 ZIP 导入：支持根目录及子目录，校验后可勾选导入项；支持文件管理器打开和分享。逐包处理冲突与失败，已成功导入的包保留。
- Windows Release 使用 Obfuscar 程序集混淆；Android Release 使用 R8 和资源收缩；原生 C++ 使用优化与符号收敛，保留必要公开契约。
- 修正 Windows 仅启用时间冻结时的相机模块启用条件。

## 安装

请先卸载旧版。Windows 同时卸载旧 XInput 代理，再安装 Next；Android 重新启用框架模块、选择实际游戏客户端作用域，并彻底停止后重启游戏。旧设置、缓存与安装记录不会自动迁移，需重新设置并导入素材。

Android 包名为 `dev.betterendfield.next`，versionCode 为 `40000`，versionName 为 `4.0.0`，使用新的 Next 发布密钥。Windows 当前使用 Next 内部自签代码签名证书，不具备公共 CA 信任。

## 下载附件

- `BetterEndfieldNext-4.0.0-Setup.exe`：Windows x64 安装包。
- `BetterEndfieldNext-4.0.0-Android-arm64.apk`：Android arm64，包含 BEM ZIP 导入。
- `BEM-Tools-1.5.2-win-x64.zip`：独立 BEM 创作者工具。
- `BetterEndfieldNext-ThirdPartySDK-1.0.0.zip` 与 `BetterEndfieldNext-Echo-1.0.0-Dual.zip`：Next 模块开发参考与双端示例。4.0.0 的公开模块入口暂时隐藏，无法从该入口导入或打开示例。
- `SHA256SUMS.txt`：上述附件的 SHA-256 校验值。

## 验证

Windows / Android Release、Android lint、R8、APK v2 签名、混淆后桌面启动及相关离线回归已通过。ZIP 导入的主机回归包含 31 项流检查、27 项 ZIP 检查和 150 项安装索引及批量事务检查。新工作区的安装身份与归档字节校验已通过。构建和主机回归不替代文件管理器路由、导入界面及所有游戏内场景的手机验收。

发布使用已验证的 Next Windows 构建和隔离构建的 Android BEM ZIP 导入版本。附件大小、哈希和构建来源见同目录的 `ASSETS.json`；实现记录见 [NEXT_IMPLEMENTATION.md](../../NEXT_IMPLEMENTATION.md)，ZIP 行为见 [ANDROID_BEM_ZIP_IMPORT.md](../../../custom_model/ANDROID_BEM_ZIP_IMPORT.md)。
