# Better Endfield Next 4.1.7

Windows / Android 软件版本 4.1.7，Android versionCode 40107。
BEM Tools / DSH 升级到 1.6.6。
资源包同步更新：core 1.6.3.3 / 1.6.4.4，preview 1.6.3.3 / 1.6.4.3。

## 修复与改进

- **Windows 世界模型阴影恢复。** 桌面阴影代理的归属和状态在应用、热切换、克隆继承与还原原版之间保持一致，不再出现世界模型阴影丢失；同时去掉多余的坐标系限制和临时诊断输出。
- **跨包格式切换后仍能还原原版。** 在旧格式包与资源级包之间切换、或包内资源 ID 变化之后，「还原原版」仍能找回原始网格与绑定。匹配仍按角色、实际资源和接收器路径精确判定，几何校验与失败回滚不变。
- **祀的模型在新客户端上可以替换了。** 客户端把两个特效部件和布料网格合并成了同一个网格对象，包声明的网格名不再等于渲染器实际持有的名字，替换会整条失败。新增兼容修复记录：按槽位路径认领唯一一个接收器，命中两个就放弃而不是猜。**1.3 与 1.4 两种包格式都恢复可用，已发布的旧包不需要重打。**
- **明河资源整理为单一模型。** 热更新为明河补上了独立 UI prefab 和 4 个装饰部件，旧的 `minghe02` / `minghe03` 变体不再随包分发，角色列表里明河只保留一项。
- **世界模型阴影与包格式的回归覆盖补齐**：旋转代理、歧义、回滚、还原、以及跨格式切换和场景克隆继承。

## 工具

BEM Tools / DSH 1.6.6：

- 修正祀的资源：记录特效部件槽位与实际渲染网格的对应关系和几何计数。
- 新增明河的角色目录：18 个部件，含来源布局与观测证据。

## 资源包

core 与 preview 各升一档（Windows core 1.6.3.3、Android core 1.6.4.4、Windows preview 1.6.3.3、Android preview 1.6.4.3），当前覆盖 35 个可玩角色/模型变体。预览几何仍只从 Windows 提取一份，Android 使用离线 ASTC 转码。

客户端会在启动后提示下载新版本资源包。

## 下载

| 文件 | 用途 |
| --- | --- |
| BetterEndfieldNext-4.1.7-Setup.exe | Windows x64 安装包 |
| BetterEndfieldNext-4.1.7-Android-arm64.apk | Android arm64 Release APK |
| BEM-Tools-1.6.6-win-x64.zip | Windows 创作者工具 |
| BEM-Tools-1.6.6-android-arm64.tar.gz | Android CLI |
| dsh-bem-creator-1.6.6.tgz | DSH 插件 |

基础与可选预览资源继续通过 [资源 Release](https://github.com/Dr-hydra/Better-Endfield/releases/tag/game-resources) 分发。
