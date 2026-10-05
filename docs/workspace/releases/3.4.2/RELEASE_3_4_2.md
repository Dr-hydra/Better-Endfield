# Better Endfield 3.4.2

Windows、Android 同步升级至 3.4.2，Android versionCode 30402；BEM Tools 升级至 1.4.1。

## 新功能

- **实验性第三方模块加载器**：双端新增统一入口，支持导入 ZIP、启停、排序、移除、模块状态和日志。每个模块可提供独立 HTML/CSS/JavaScript 页面，通过宿主网页桥读取配置、保存参数和与原生模块交换消息。Windows DLL / Android ARM64 SO 统一由游戏进程 Host 加载。
- **共享 Hook 链**：参与新接口的模块可以挂同一目标，每个节点通过稳定的 `next` 调用后续节点。现有独占 Hook 仍保留冲突检查；这不代表第三方模块能自动与所有内置 Hook 共存。开发者自行维护游戏函数表、签名和实现逻辑。附 [创作者接口文档](https://github.com/Dr-hydra/Better-Endfield/blob/v3.4.2/docs/THIRD_PARTY_MODULE_CREATOR_GUIDE.md) 与双端 Echo 示例 SDK。
- **BEM 1.3 体型参数**：支持稀疏顶点位移、分段插值和多参数叠加；双端增加包作者定义的滑条和参数保存。创作者工程、桌面导出窗口、CLI、EFMI 形变转换与格式文档同步升级。参数随资源重新构建生效，游戏内调整需启用现有模型热切换实验，并触发正常资源重载。
- **相机增强**：新增全局 FOV 调节与自由相机相对人物平移跟随。

## 修复与体验

- 修复第一人称关闭后偶发残留旋转角度限制，以及切人过程中旧对象、骨骼与相机引用的生命周期问题。
- 第一人称去头发结合实时骨骼与 BEM 替换后的 CPU 几何识别头部，保留完整阴影；角色资源未知或几何不可读时保守回退。
- 双端补齐噗切娜的开屏模型资源。
- 桌面角色外观页新增 `.bem` / `.zip` 多文件拖入区，复用现有导入流程。
- Android 手机导航支持横向滚动，第三方模块 UI 使用独立全尺寸页面。
- 翻新双语 README、Android 说明和仓库 About。

## 下载与验证

- `BetterEndfield-3.4.2-Setup.exe`：Windows 安装包，包含 BEM 创作者工具、33 角色资料和新开发文档。
- `BetterEndfield-3.4.2-Android-arm64.apk`：Android ARM64，versionCode 30402。
- `BEM-Tools-1.4.1-win-x64.zip`：独立创作者工具、BEM 1.3 规范与工程示例。
- `BetterEndfield-ThirdPartySDK-1.0.0.zip`：原生接口头文件、网页桥说明、Echo 源码及可导入的双端示例包。
- `BetterEndfield-Echo-1.0.0-Dual.zip`：可直接在第三方模块页导入的双端 Echo 示例。

已完成 Windows 与 Android 发布构建，以及模块加载/消息、网页桥、导入、Hook 链、BEM 1.3 与相机相关回归；Android Lint 和 APK 签名检查通过。ARM64 Hook 链完成交叉编译，当前没有连接的 ADB 设备，新增游戏内效果尚未实机验收。

第三方原生模块不会在运行中卸载；更新库、替换已加载代次或改变已加载顺序需要重启游戏。旧代次目录及 Android 远端 ZIP 暂不自动回收。请只导入信任的模块；第三方代码的稳定性与游戏兼容性由其作者负责。
