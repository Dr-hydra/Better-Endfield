# Better Endfield 3.5.4

**状态：GitHub Release 草稿。** 本轮补齐全部五份发布附件，草稿上传完成后可进行正式发布。

## 更新

- Android 增加场景纹理池协调：根据实际纹理需求、游戏当前预算和系统内存余量调整额度，跟随画质切档，并在停止或不再需要时恢复游戏预算。保留既定全局 LOD、NPC 可见范围及 Android 物理 LOD1，不通过降低这些设置解决场景贴图模糊。
- 修复 Android 普通角色首次热切换的资源发现缺口：此前模型未启用、没有生成记录时，已加载的主世界实例可能一直等待下一次资源投递。现在沿原有 UI LOD0 donor / 主世界 LOD1 配对和发布事务发现已有实例；临时缺少 donor 时保留重试，结构与身份校验继续保留。
- 修正热切换时实例与模板的原版资产记录归属，按角色隔离失败重试，避免一个角色的开关重新触发其他角色的遗留请求。恢复仅校验实际原版骨骼路径，无关辅助节点不再阻止恢复；必需骨骼缺失或歧义继续拒绝，不关闭全局模型校验。修复测试包已在本轮手机实机确认可用。
- 模型悬浮窗区分首次加载、读取失败和真实空列表；读取失败显示错误及重试入口，保留上次成功的列表并暂停编辑，恢复后重新启用。校验无法显示的目录行，避免错误数据覆盖有效缓存；已启用热切换时，开关文案改为 **“启用（热切换）”**。
- 修复更新 APK 后可能需要手动打开一次 BE 才能连接设置桥的问题：增加更新后初始化入口，以及读取连接失败时由前台游戏唤醒的透明 Bootstrap 入口。透明入口短暂等待框架 Binder 更新后退出，不读取启动参数中的设置。恢复只对读取请求进行有界重试，不自动重放设置写入，也不改变原调用授权检查。
- 针对 [issue #26](https://github.com/Dr-hydra/Better-Endfield/issues/26) 加入 Android PCUI 输入修正：排除 Android 专属退出绑定与 PC 菜单动作的冲突，补齐菜单系统光标和根视图绝对鼠标位置，并保留真实触屏优先及失焦恢复。代码及宿主检查已完成，**尚未完成游戏内键鼠实机验收**。
- Windows / Android 版本统一为 **3.5.4**（Android versionCode **30504**）。本轮主要改动在 Android；BEM Tools 沿用 **1.5.2**，第三方 SDK 与 Echo 沿用 **1.0.0**。

## 发布产物

3.5.4 构建及包检查已完成，以下五项产物从 `releases/3.5.4/` 上传到现有 GitHub Release 草稿：

- `BetterEndfield-3.5.4-Setup.exe`：Windows x64 安装包。
- `BetterEndfield-3.5.4-Android-arm64.apk`：Android ARM64 模块。
- `BEM-Tools-1.5.2-win-x64.zip`：独立创作者 GUI、CLI、Blender 插件、示例、文档及 Skill。
- `BetterEndfield-ThirdPartySDK-1.0.0.zip`：第三方原生模块 SDK。
- `BetterEndfield-Echo-1.0.0-Dual.zip`：Windows / Android 双端模块示例。

工具、SDK 和 Echo 的版本号没有提升，本轮已重新构建或打包。本地还保留 Windows 便携 ZIP，正式 Windows 分发入口沿用安装包。

## 验证范围

用户已在测试手机确认场景纹理恢复正常，并确认列表读取、更新后的自动连接和热切换可以使用。自动恢复采样记录了原授权仍有效、设置桥连接异常、Bootstrap 建立连接、随后 `read_models` 成功且取得有效 revision 的顺序；证据保存在 `build/android-issues-25-26/device/20261008-overlay-auto-reconnect-confirmed`。这些确认限于本轮设备与使用场景，不推断所有 ROM、框架版本或游戏资源均已覆盖。

本轮候选代码已通过 Android Release / lint、候选 APK 签名及 native 打包核对。宿主验证包括纹理预算策略与接口、Android LOD1 配对及普通角色首次资源发现、PCUI Java / native 输入桥、设置写入边界与热切换事务。模型列表状态、读取恢复策略及真实 Bootstrap Activity / Receiver 生命周期分别有 **66、35、43 项检查**通过；覆盖旧回调、错误目录、短暂失败恢复、前台等待上限、暂停 / 销毁回调清理和广播释放。

**3.5.4 最终 Windows Release / 安装包、Android Release / lint、BEM Tools GUI / CLI、SDK / Echo 构建及包检查已完成。** Windows 主程序版本为 3.5.4，Android 为 3.5.4 / 30504，APK 原签名与两份 arm64 库逐字节核对通过；ZIP 完整性、路径、内嵌双端包及 SDK 独立构建检查通过。BEM Tools 的 GUI 与 CLI 均为 1.5.2。产物及验证收据在 `releases/3.5.4/BUILD.json`；附件上传阶段校验远端名称、大小与摘要。构建和宿主验证不代替全部场景的游戏内验收。

Issue #26 的反馈涉及外接键鼠，用户当前手机测试未覆盖该场景。主世界 Esc、工业模式 Alt 光标、制造 / 寻访 / 简易制作等菜单点击与拖动、触屏与鼠标交替、失焦恢复及关闭 PCUI 后的原输入行为仍待实机检查，不能将本轮修正写成全部菜单问题已解决。范围与现场检查见 [Android PCUI issue #26](../../../ui/research/1.5.3/android-input-layout/ANDROID_PC_UI_ISSUE26_20261007.md)。

纹理池与首次热切换的证据、保留 LOD/NPC 的约束见 [Android 场景纹理 issue #25 调查](../../../custom_model/research/1.5.3/android-texture-streaming/ANDROID_TEXTURE_STREAMING_ISSUE_25_20261007.md)。

## Shizuku 研究边界

本轮只完成 [Shizuku 可行性研究](../../../host/research/not_applicable/shizuku/SHIZUKU_FEASIBILITY_20261007.md)，未接入 Shizuku SDK，未交付免 root 注入或免 root 模组支持。adb 模式的辅助权限不能单独替代现有游戏进程内加载入口；当前 Android 运行能力仍依赖兼容注入框架。

## English

**Status: Release draft.** All five distribution packages are built and validated for upload to the existing GitHub draft. Publishing the release remains a separate step.

- Added Android texture-pool coordination based on actual demand, the game's current budget and available memory, with quality-profile tracking and budget restoration. Existing global LOD and NPC visibility behavior, including Android's physical LOD1 mapping, is preserved.
- Fixed first-time Android character hot-switch discovery when the visible world instance was already loaded but no generated model record existed. Discovery keeps the existing UI-donor/world-receiver pairing and validation, and retries temporary donor unavailability.
- Corrected Original asset ownership between templates and scene instances, and isolated failed retries by role so editing one character does not replay another character's pending selection. Restoration checks the required Original bone paths, ignoring unrelated helper nodes while still rejecting missing or ambiguous required bones. The repaired Android test package has user gameplay acceptance on the test phone.
- Model overlays now distinguish loading, read failure and a genuinely empty catalog. Failed reads preserve the last successful list with editing disabled until recovery; malformed rows cannot replace a valid cached list. Enabled hot switching is labeled accurately.
- Added post-update initialization and a bounded transparent Bootstrap activity to reconnect the framework settings bridge from a foreground game. The entry consumes no setting payload, and only reads are retried; writes and caller authorization retain their existing rules.
- Added Android PCUI fixes for the extra quit binding, menu cursor visibility and absolute mouse positions, with touch priority and focus cleanup. These changes passed host checks; **in-game keyboard/mouse validation for issue #26 is still pending**.
- Windows and Android are version **3.5.4**, with Android versionCode **30504**. BEM Tools remains **1.5.2**; the third-party SDK and Echo remain **1.0.0**.

The user confirmed scene-texture recovery, model-list access, automatic reconnection after updating and the final hot-switch repair on the test phone. Android builds and host regressions passed, including catalog-state, read-recovery and bootstrap lifecycle checks, plus regressions for role isolation and required bone paths. Windows/Android builds, installer packaging, original APK signature and version checks, native-byte comparisons and archive validation passed. All five distribution assets are prepared for the existing draft; publication remains a separate step.

Shizuku work is research only. This update does not include or claim rootless game injection or rootless mod support.
