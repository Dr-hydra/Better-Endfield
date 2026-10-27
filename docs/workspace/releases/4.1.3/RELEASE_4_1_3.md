# Better Endfield Next 4.1.3

Windows / Android 4.1.3（40103），BEM Tools / DSH 1.6.4。

Better Endfield Next **4.1.3** / BEM Tools **1.6.4**

## 双端外观组合

- 从多个已安装 BEM 包选择部件来源，保留来源的切换选项、绑定条件与默认值；版本自动从来源推导，修订号差异不再直接阻止兼容组合。
- 按来源选项名称分组并可展开绘制段；支持一键启用/停用、批量操作和多段高亮。重叠段按选择保留，不自动裁剪、补缝或消除穿插。
- Body 按骨骼权重细分头、颈、躯干、骨盆及四肢等区域；支持预览中手动调整三角形归属、撤销、恢复自动划分。来源不存在的区域不会凭空生成。
- BEM 1.4 大招与武器作为可选整体项；多输出组合可连续导入。不支持的形变、驱动及材质覆盖会明确拒绝，不静默丢弃。

## 交互预览

- 已安装包新增预览入口，可切换选项、查看绑定部件、旋转、缩放、平移及保存。Windows 支持多个独立窗口，组合界面预览常驻；Android 组合预览按需打开，支持触摸编辑。
- 支持 BCn（含 BC7）与 ASTC 贴图预览，减少重复解析与切换等待，并自动补全缺失的原生头部预览部件。
- 移除改为卡片右侧垃圾桶图标，与预览分开；配套中英文本地化。
- Windows 预览使用 WebView2；运行时缺失只影响预览，主程序仍可启动。

## 模型加载修复

- 合入先前提交：同步热切换过程的材质缓存与资源替换状态，修复过期材质复用及相关资源竞态。
- 新增可选的退役模型资源清理实验，沿用实验功能开关与配置流程。

## BEM Tools 1.6.4

- 新增 compose CLI；Windows 独立工具包随附原生组合引擎，可生成 PC / Android 目标包。
- bem-creator skill 增加组合文档及 Python 标准库脚本，支持计划、精简摘要、预览草稿与最终构建。
- Windows 工具包、Android CLI 源码包与 DSH 插件统一升级至 1.6.4。Android CLI 源码包不包含原生组合引擎；手机组合通过 Android 应用提供。

## 下载

| 文件 | 用途 |
| --- | --- |
| BetterEndfieldNext-4.1.3-Setup.exe | Windows x64 安装包 |
| BetterEndfieldNext-4.1.3-Android-arm64.apk | Android arm64 Release APK |
| BEM-Tools-1.6.4-win-x64.zip | Windows 创作者 GUI、CLI、组合引擎与技能文档 |
| BEM-Tools-1.6.4-android-arm64.tar.gz | Android CLI 源码发行包 |
| dsh-bem-creator-1.6.4.tgz | DeepSeek Harness 创作者技能与预览插件 |

组合接缝、穿插及动画效果取决于来源素材。本次未增加手机或游戏内交互验收。

来源提交：fda9ad66（组合与预览）、5865a053（材质缓存同步）、a5da6e3c（退役资源清理）。构建与发布结果见 ASSETS.json。
