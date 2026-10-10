# Better Endfield Next 4.0.1

4.0.0 用户可直接覆盖更新至 4.0.1，保留 Next 设置。Android versionCode 为 40001，安装身份与签名沿用 Next。

## 更新

- **NPC 模型接入**：同一个 BEM 也可替换使用同名角色模型的非编队 NPC，无需单独制作 NPC 包；保留原有校验、加载登记和关闭恢复。
- **桌面端修复**：DLL / XInput 代理安装后及时刷新状态；修复“保存并启动”卡在“正在准备配音资源”、无法启动游戏的问题。
- **模型二次交付修复**：资源恢复为已记录的原版 Mesh 后允许重新准备替换，避免世界模型持续显示原版。
- **实验：克隆模型支持**：PC、Android 提供独立开关，默认关闭，重启游戏后生效。记录克隆生成时的来源与包代次，使用实例自身骨骼，保留已继承结果的私有材质，模板继续同步替换。
- **第三方模块迁移**：双端彻底移除第三方模块装载器、管理页面、网页桥、扩展配置与 SDK / Echo 打包项，迁移至 [Endfield Mod Loader](https://github.com/Dr-hydra/Endfield-Mod-Loader)。BEM 模型、MMD、内置模块与创意工坊继续由 BE 支持。
- **废弃研究归档**：BEM 原生布料物理及后续权重、裁剪试验归入 `legacy/native-bem-cloth-20261010/`，不参与本版本构建。

## 下载

- `BetterEndfieldNext-4.0.1-Setup.exe`：Windows x64 安装包。
- `BetterEndfieldNext-4.0.1-Android-arm64.apk`：Android arm64。
- `BEM-Tools-1.5.2-win-x64.zip`：独立 BEM 创作者工具。
- `SHA256SUMS.txt`：附件校验值。

Windows 安装包、Android APK 均沿用 Next 发布签名；Windows 使用既有内部自签证书。从 3.x 升级仍需按 Next 首次安装说明重新安装与设置。

## 验证范围

双端 Release 构建、Windows Obfuscar、Android R8 / lint、签名验证通过。桌面启动回归在普通与生产混淆后各通过 17 项；配置 / 设置回归、150 项 Android BEM 安装状态检查、页面路由与工坊构建通过。模型完整绑定、异步加载、来源追踪、NPC 与 Clone 生命周期回归通过。Android 通用跨页 Hook 写入通过手机上的自包含原生测试。

NPC 接入已有用户实机确认。上述离线回归与原生自测不等于完整游戏画面验收；Clone 支持仍以实验开关提供。
