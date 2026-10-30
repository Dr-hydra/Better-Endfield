# Better Endfield Next 4.1.4

Windows / Android 4.1.4（40104），BEM Tools / DSH 1.6.5。

Better Endfield Next **4.1.4** / BEM Tools & DSH **1.6.5**

## 游戏资源独立更新

- Windows / Android 的模型预设、动作和语音清单等游戏基础资源迁入独立版本目录，与软件版本解耦。Windows 安装包携带基础资源；Android 通过更新入口或手动导入取得基础资源。
- 双端“关于”新增资源更新、可选预览下载、ZIP 导入/回退、恢复自动更新及取消入口；保留手动安装方式。
- 下载与解压完成并校验后再切换版本；中断和取消保留原版。手动导入会固定所选版本，避免刚回退就被自动升回。
- 软件、工具和资源使用统一分发索引。资源集中在独立的 game-resources Release，不与软件 Latest 混用。

## 全角色预览资源

- 已从当前 Windows 游戏资源生成 37 个可玩角色 / 模型变体，输出 BC7 与 ASTC 6×6 两个可选包；Android 纹理由开发机离线转码，用户端无需转码。
- 原生补全从单角色样包扩展为按精确角色、prefab、renderer 和材质身份读取资源；遵守保留、隐藏与替换规则，不用不匹配的原生模型强行补齐。
- 预览资源单独下载，不捆入软件安装包。已有本地资源可复用，缺失或损坏时仍显示 BEM 自带内容。

## BEM Tools / DSH 1.6.5

- DSH 预览页增加“预览资源”入口，可选择本地目录以及 BC7 / ASTC 格式；选择依据实际浏览器能力，由用户决定，不按 DSH 服务器系统写死。
- 支持浏览器 ASTC GPU 直传；不支持时提示选择 BC7，不自动下载或切换资源包。
- bem 工具增加 resources 操作，支持查询、选择本地资源和刷新预览；skill 补齐按本地环境选包、优先复用本地文件的说明。
- Windows 创作者工具包、Android CLI 源码包与 DSH 插件同步升级；BEM 格式版本仍为 1.0–1.5。

## 祀的冲刺过渡与日志

- 合入上次发布后的历史提交：双端提供默认关闭的“高质量过渡”开关，改善祀及坐骑的循环接缝与附属件动作。
- 调整高质量模式下的循环特效保持、冲刺起始音效停止及角色 / 坐骑根对象替换后的回退，抑制预排队的收尾水花。
- 逐帧诊断迁入可选 verbose 级别，默认保留每次冲刺摘要和失败信息；保存设置时保留相应 INI 配置。

## 下载与资源

| 文件 | 用途 |
| --- | --- |
| BetterEndfieldNext-4.1.4-Setup.exe | Windows x64 安装包 |
| BetterEndfieldNext-4.1.4-Android-arm64.apk | Android arm64 Release APK |
| BEM-Tools-1.6.5-win-x64.zip | Windows 创作者 GUI、CLI、组合引擎及 skill |
| BEM-Tools-1.6.5-android-arm64.tar.gz | Android CLI 源码发行包 |
| dsh-bem-creator-1.6.5.tgz | DSH 创作者 skill 与预览插件 |

基础与可选预览资源见 [资源 Release](https://github.com/Dr-hydra/Better-Endfield/releases/tag/game-resources)。Windows 与 Android 游戏基线可能不同，资源版本不按软件版本比较。Windows 跨游戏基线通过新版携带基础包或手动导入切换，尚未自动检测游戏的新基线。

本次执行本地构建、回归与打包检查，不额外下载已生成的资源包作联网测试；37 个角色生成成功不代表全部完成实机显示验收。静态预览不复现完整游戏 shader 和动画。

来源提交：e8ed18e2、e7b46cf6、b96da616、51170f9c、44184644、de9b1642、6251bfb0、1c96ca7d、512b493d，以及本轮资源解耦与 DSH 预览资源接入。构建和发布结果见 ASSETS.json。
