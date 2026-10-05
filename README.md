# Better Endfield

[简体中文](README.md) | [English](README.en.md)

Better Endfield 是面向《明日方舟：终末地》的开源模块化工具，提供第三方角色模型、MMD 播放、相机和界面增强、按角色配音、开屏自定义，以及 PC 战斗统计和寻访记录管理。Windows 和 Android 共用主要原生功能源码；BEM 模型包和 MMD 作品可以跨端使用。另提供实验性的第三方原生模块加载与网页界面容器。

[下载正式版](https://github.com/Dr-hydra/Better-Endfield/releases/latest) · [更新说明](CHANGELOG.md) · [Android 使用与构建](android/README.md) · [BEM 创作者指南](docs/custom_model/BEM_CREATOR_GUIDE.md) · [模块开发指南](docs/host/THIRD_PARTY_MODULE_CREATOR_GUIDE.md)

当前版本为 **3.5.0**，独立 BEM Tools 为 **1.5.0**。3.5.0 增加 Android 跨 LOD 资源兼容、BEM 创作者英文文档、可移动创作者工作区和 Blender 导出器首版。

## 功能一览

「支持」表示有对应实现和入口，不表示每个角色、作品、设备或游戏版本均经过实机验证。

| 功能 | Windows x64 | Android ARM64 | 说明 |
| --- | --- | --- | --- |
| 第三方模型（BEM） | 支持 | 支持 | 同一标准包；导入、更新、多包管理、按角色启用、外观与部件选项 |
| BEM 1.3 形态滑条 | 支持（3.4.2） | 支持（3.4.2） | 作者提供顶点形变数据；插值、叠加、条件可用与参数保存 |
| 第三方原生模块与网页容器 | 实验（3.4.2） | 实验（3.4.2） | 统一导入/管理入口；游戏内 Host 加载 DLL/SO，作者自定义网页与功能 |
| 模型热切换 | 实验 | 实验 | 游戏启动前开启，选择变化随游戏正常重载资源生效 |
| 模型加载优化 | 实验 | 实验 | 减少解码副本并复用同次构建的等价贴图；不降低画质 |
| 开屏模型、动画与主题色 | 支持 | 支持 | 角色、最终动作、分阶段速度、缩放、转身、循环与交叉混合 |
| 角色配音语言 | 支持 | 支持 | 单独指定中文、英语、日语、韩语；可应用于剧情语音与口型 |
| 自由相机、第一人称、时间冻结 | 支持 | 支持 | 独立控制、FOV、运镜、关键帧、VMD 镜头、近景透明效果处理 |
| 全局 FOV、自由相机人物跟随 | 支持（3.4.2） | 支持（3.4.2） | 普通主相机 FOV 覆盖；自由相机跟随人物平移并保留手动偏移 |
| MMD 作品库与多人同台 | 支持 | 支持 | 最多四人，动作、表情、镜头、本地音乐、时间轴与衣物物理选项 |
| UID/HUD 显隐、界面布局 | 支持 | 支持 | PC 可用触屏布局与鼠标转触控；Android 可切换 PC 风格布局 |
| 持续特殊冲刺 | 支持 | 支持 | 洁尔佩塔、梨诺分角色开关；梨诺可隐藏机甲和光效 |
| 战斗统计与 rDPS | 支持 | — | 游戏内悬浮窗、角色/技能排行、时间轴、历史筛选与网页分析入口 |
| 寻访记录 | 支持 | — | 游戏同步、本地统计、JSON 导入导出与云端分享入口 |
| OmniMix 音乐集成 | 支持 | — | 外部后端音乐送入游戏 Wwise 总线，异常时回退原生音乐 |
| OptiScaler 显示增强 | 支持 | — | DLSS/FSR/XeSS 超分、帧生成与锐化；具体能力依赖 GPU、后端和游戏渲染路径 |

Windows 提供中文/英文界面、明暗主题、运行状态和日志、游戏路径发现、启动参数、快捷方式、更新检查和 XInput 自启动管理。Android 提供自动保存的分类设置、包导入和游戏内可收起控制面板。

## 安装与启动

### Windows

从 [Releases](https://github.com/Dr-hydra/Better-Endfield/releases) 下载 Windows 安装包，启动 Better Endfield，检查游戏路径并开启需要的功能，然后保存并启动游戏。运行环境为 Windows 10/11 x64。

- **内置注入器**：默认方式，由 Better Endfield 启动游戏，Host 和模块从软件目录加载，不把 Better Endfield 运行文件写入游戏目录。
- **XInput 自启动**：设置页可安装可选的 `xinput1_4.dll` 代理，此后从官方启动器或游戏快捷方式启动也能加载。安装与卸载核对归属记录；已有其他工具的同名文件时不会覆盖。

OptiScaler 是独立部署功能，会写入游戏目录并在下次启动时生效。游戏启动参数也会用于一键启动快捷方式。

### Android

Android 包是 **LSPosed/libxposed API 102 模块**，要求 Android 10 及以上、ARM64，以及能注入目标游戏的兼容框架；单独安装 APK 不会启用游戏功能。

安装后在框架中启用模块并选择实际使用的终末地客户端，在模块应用中设置功能，然后彻底停止并重启游戏。游戏内面板通过目标 Activity 显示，无需悬浮窗权限；相机、冻结、第一人称和 MMD 使用面板控制。详细操作、作用域排查和构建要求见 [Android README](android/README.md)。

## 第三方角色模型与创作者工具

**玩家使用 `.bem` 标准包即可**，无需安装 Python、原 Mod 框架、角色数据库或编辑运行配置。一个包对应一个角色，支持固定外观、可组合部件和材质/纹理替换。同一角色可以安装多个包，启用其中一个时停用其他包；更新保留仍有效的外观和参数选择。

PC 和 Android 使用同一 BEM 解析与装配核心，支持 1.0–1.3；**3.4.2 新增 BEM 1.3 位置形变**。包内滑条由作者定义，运行时在基础顶点上插值并叠加增量，沿用原骨骼、蒙皮和基础法线/切线。它不会自动给旧模型生成体型滑条，也不执行源 Mod 的热键脚本、GUI 表达式或任意 Shader。

常规选择在重启游戏后生效。实验热切换开启后，包、部件及 1.3 参数变化在切换配队、重新打开详情等**正常资源重载**时应用；没有每次拖动滑条立即更新当前网格的承诺。导入、更新包文件和删除仍建议在游戏关闭时进行。

热切换与加载优化相互独立，默认关闭。热切换为重建缓存保留原始模型资源，会增加内存占用；加载优化减少的是解析副本及重复资源，不保证所有设备的游戏显存峰值都会下降。

Android 可对贴图异常的包执行「转换手机纹理」；转换成功发布新一代并保留选择，失败或取消保留原包。该操作需要已验证的法线编码信息，模型本身的跨端标准不代表桌面纹理在所有手机 GPU 上均可直接显示。

**创作者工具**包含图形导出入口和独立 CLI，支持目录、ZIP、RAR、7z 输入，转换报告、结构校验、可保存的 `.bemproj.json` 导出工程、标准工作区和重复构建。角色资料随工具分发，包含 33 名角色；软件另附开屏资源索引。BEM Tools 1.5.0 增加 Blender 工程导出入口；BEM 1.3 仍支持同拓扑目标顶点、稀疏增量，以及作者显式绑定的官方 EFMI ShapeKey buffers，不会自动还原任意源 GUI 或猜测顶点对应关系。

```powershell
BetterEndfield.BemConverter.exe new-project editable/project.json --mode pack -o character.bemproj.json
BetterEndfield.BemConverter.exe build character.bemproj.json
```

两端还提供默认关闭的「实验：关闭模型校验」。该选项放开兼容性和策略限制，仍要求文件能解码且能被当前表示方式读取；不增加新编码支持。用于作者测试时可能出现错误渲染或游戏崩溃。

- [创作者指南](docs/custom_model/BEM_CREATOR_GUIDE.md)：工具、制作流程、导入测试、分发
- [格式规范（1.0–1.3）](docs/custom_model/BEM_FORMAT_SPEC.md)
- [运行时行为与兼容性](docs/custom_model/BEM_RUNTIME_COMPATIBILITY.md)
- [其他来源 Mod 转换](docs/custom_model/BEM_SOURCE_MOD_CONVERSION.md)
- [形态滑条可运行示例](tools/CustomModel/examples/body-slider/)
- [实验热切换与加载优化说明](docs/workspace/releases/3.4.1/RELEASE_3_4_1.md)

## 第三方模块（实验，3.4.2）

双端通过独立的「第三方模块」入口导入作者提供的 ZIP，管理启用状态、加载顺序和各模块的网页入口；新模块首次导入默认停用。一个包包含 `module.json`、对应平台的 Windows x64 DLL / Android ARM64 SO，以及可选 HTML/CSS/JS 资源；可以只提供一端，也可以只提供网页功能。第三方模块与 `.bem` 模型包分别管理。

原生库由**游戏进程中的 Host**加载；Windows 网页使用 WebView2，Android 使用 WebView。作者可以自由编写页面，通过统一桥读取/保存自身 JSON 配置、发送业务消息、接收结果和查询状态。网页可在游戏未连接时保存配置；需要游戏模块的消息则要求运行中的 Host 连接。

加载器提供入口 ABI、模块生命周期、配置和消息运输，游戏函数表、版本适配、调用线程、功能实现及停用恢复由作者维护。模块可以自行解析，也可选用 Host 已就绪的辅助接口；初始化不强制等待 IL2CPP。Host 回调在线程工作队列执行，不代表 Unity 主线程。配置与已加载模块启停可运行时更新；原生库仍随游戏进程驻留，二进制更新、删除和顺序调整需重启游戏，不做任意热卸载。

更新或移除会退休旧安装代次，但旧目录仍保留，Android 的旧 ZIP 也会保留；当前没有自动垃圾回收，也不在重启后自动删除这些文件，避免破坏运行中的原生库或网页资源。

Android 手机导航可横向滚动，大屏保留侧边导航；模块网页使用独立页面，完整编辑器不会挤在管理卡片里。跨端网页建议把静态资源随包分发；Android 容器只加载包内离线资源，网络业务可由作者原生模块处理。

共享 Hook 是可选接口：采用 chain 的模块按同一目标串联并调用 `next`，已有 exclusive Hook 仍会报告冲突，不自动迁移成链。函数签名、参数/返回值处理和功能冲突由参与者协调。加载成功不等于与所有内置或第三方模块兼容。

接入流程见 [第三方模块创作者指南](docs/host/THIRD_PARTY_MODULE_CREATOR_GUIDE.md)，契约见 [ThirdPartyModule.h](native/shared/include/BetterEndfield/ThirdPartyModule.h)、[HookChain.h](native/shared/include/BetterEndfield/HookChain.h)与[Echo 示例](tools/ThirdPartyModules/echo/)。Echo 原生库已完成 Windows/Android 构建，消息桥与生命周期通过隔离回归；真实游戏模块的效果、稳定性和兼容性仍由作者测试和说明。

## 相机、第一人称与 MMD

自由相机支持位置/朝向、滚转和 FOV 调整、鼠标转向、环绕/推拉/升降/平移运镜，以及可保存的关键帧路径和 VMD 镜头。时间冻结独立于自由相机；第一人称支持头部隐藏、颈部补口、侧看角度与平滑转身。快捷键可配置，Windows 支持主键盘、小键盘、鼠标和组合键；Android 用游戏内控制面板操作。

3.4.2 新增的全局 FOV 只覆盖普通主相机；自由相机、第一人称和导入镜头使用自己的 FOV。人物跟随仅平移自由相机，保留镜头朝向与手动偏移，在切人和传送后重新建立参考，并在运镜播放时暂停。[实现边界](docs/camera/research/1.5.3/fov-follow/CAMERA_FOV_FOLLOW_IMPLEMENTATION.md)

3.4.2 的第一人称去头发逻辑按**现场骨骼权重和实际绘制数据**分类，避免把含头部与衣物的混合网格整体隐藏；替换网格可使用 BEM 的 CPU 数据。未知角色走通用现场骨骼回退，不依赖另维护一整套必需角色表。独立部件尝试仅投影模式，混合部件保留完整投影网格；缺少可用几何数据时保留不确定部件。实际阴影和残留头发效果需要游戏验收。[方案与验证范围](docs/camera/research/1.5.3/first-person-geometry/BEM_HAIR_SHADOW_IMPLEMENTATION.md)

MMD 作品库管理动作、表情、镜头和本地音乐，可通过 `set.ini` 描述作品；支持播放/暂停/停止、跳转、循环、游戏/自由/VMD 镜头切换，最多四名队员同台，以及衣物物理和实验地形贴合。Windows 音轨走本地音乐接口，不要求 OmniMix；Android 使用本地媒体播放，会与游戏 BGM 叠加，可在游戏设置中关闭原背景音乐。双端身体/表情、布料和地形能力依赖实际客户端接口，作品转换或编译通过不能代替实机效果验证。[双端整合记录](docs/camera/research/1.5.3/android-mmd/ANDROID_CAMERA_MMD.md)

## 其他模块

**开屏**：替换登录演员，按角色资源选择坐姿链和最终动作，调整缩放、起始角度、转正时间、各阶段速度、原生/强制循环和双 Playable 混合；Logo 与登录色带可独立设置主题色。收录 33 名角色、4,262 条最终动作索引，3.4.2 补齐噗切娜资源。[新角色资源修复](docs/model/research/1.5.3/purrche-title/TITLE_MODEL_PURRCHE_FIX.md)

**配音**：在游戏全局语言不变的情况下分别指定角色中文、英语、日语或韩语，可扩展到剧情语音、时长和口型。先在游戏中下载对应语言包，软件从本机资源生成所需目录；发布包不携带 PCK、BNK 或 WEM 音频。[语音路由说明](docs/voice/VOICE_CUSTOM_LANGUAGE_SYSTEM.md)

**界面与动作**：隐藏 UID、通过快捷键/面板切换 HUD；PC 触屏布局搭配鼠标转触控，适用于串流或触控设备，转换默认快捷键为 `Ctrl+Alt+T`。Android 可启用 PC 风格布局，建议搭配键盘或手柄。布局切换与账号平台声明独立。持续冲刺当前适配洁尔佩塔和梨诺，梨诺另有隐藏机甲与光效选项。[动作模块](native/modules/actions/README.md)

**PC 战斗数据**：手动会话和关卡自动会话，伤害数字显隐、悬浮伤害/DPS 排行、技能分类、角色与技能时间轴、历史筛选和网页分析。rDPS 使用随软件更新的已验证 Buff/技能语义，将可确认的队友增益贡献重新归属；无法确认的项不参与贡献转移。数据保存在本机，网页分析由用户主动打开。[战斗契约](docs/combat_stats/COMBAT_RUNTIME_CONTRACTS.md)

**PC 寻访记录**：启用后从游戏连接同步记录，按卡池展示统计、六星、UP/非 UP、保底和免费抽信息，支持 JSON 导入导出及用户主动发起的云端分享。[网页功能说明](web/docs/GACHA_WEB_PLAN.md)

**PC 音乐与显示**：OmniMix 集成使用用户现有后端，不复制其程序或曲库；可分别替换登录、主界面/基地、游戏内音乐，流异常时保留或恢复原生音乐。OptiScaler 负责超分、帧生成和锐化，硬件/后端兼容性依实际环境。[OmniMix 对接](docs/music/OMNIMIX_INTEGRATION_HANDOFF.md) · [显示管线](docs/ui/DISPLAY_PIPELINE.md)

## 模块架构与兼容性

Windows 的 Host 加载独立功能 DLL；Android 的游戏内运行时编译共用模块源码并提供平台适配。Host 负责模块发现、生命周期、配置、动态 IL2CPP 解析和 Hook 管理。内置功能按程序集、类型、方法、签名和字段描述解析运行时接口，不依赖一套官服固定地址或 `GameAssembly.dll` 身份白名单。

这允许不同客户端共用代码，但**不保证任意游戏版本自动兼容**：方法签名、资源、渲染布局或设备接口变化仍可能要求更新。内置模块的契约缺失会禁用对应能力并记录日志。Android 应将作用域选到实际客户端；世界、详情和开屏以及 PC/手机资源布局也不能互相假定相同。

Windows 主配置位于 `%LocalAppData%\BetterEndfield\BetterEndfield.ini`，UI 设置为同目录的 `ui-settings.json`，BEM 包与状态位于 `catalog\custom-model`。Android 设置通过框架发布，资源复制到游戏自己的私有目录。角色/语音索引随软件分发，原游戏资源按需要从本机读取，不随仓库或安装包分发。

| 目录 | 内容 |
| --- | --- |
| `ui/BetterEndfield.UI/` | WinUI 桌面管理界面与资源 |
| `native/modules/` | 模型、BEM、配音、音乐、战斗、界面、相机、动作与寻访模块 |
| `native/shared/` | Host、公共 C ABI、平台兼容层与原生依赖 |
| `native/loaders/` | Windows 内置注入器与 XInput 自启动代理 |
| `android/` | Android 应用、框架入口、运行时与游戏内面板 |
| `tools/CustomModel/` | BEM 导出、转换、校验、工程与角色资料工具 |
| `manifests/`、`resources/` | 模型/动作/语音/战斗索引及生成输入 |
| `web/` | 战斗与寻访的网页分析/分享相关源码 |
| `scripts/`、`docs/` | 构建和资源生成脚本、接口文档与研究记录 |

内部运行时协议见 [GAME_INTERFACES.md](docs/host/GAME_INTERFACES.md)。研究目录和历史记录不代表全部已发布功能。

## 从源码构建

Windows 需要 Visual Studio 2022 C++ 工具集、CMake、.NET SDK 9 和 PowerShell；构建安装程序另需 Inno Setup 6。BEM 工具构建依赖见 [`requirements-build.txt`](tools/CustomModel/requirements-build.txt)。

```powershell
pwsh -File .\scripts\BuildBetterEndfield.ps1
pwsh -File .\scripts\BuildInstaller.ps1
pwsh -File .\scripts\BuildBemTools.ps1
```

Android 使用 JDK 17 及以上、SDK、NDK 和 CMake，版本以 [`android/app/build.gradle.kts`](android/app/build.gradle.kts) 为准；配置好工具链后：

```powershell
.\android\gradlew.bat -p android :app:assembleRelease --no-daemon
```

构建脚本使用仓库内已生成的资源索引；游戏更新后需先更新相应资料，不应把本机研究输出或游戏资源载荷打入发布包。

## 许可

Better Endfield 使用 [AGPL-3.0-only](LICENSE)，是独立的非官方项目，与游戏开发商及发行商无关联。MinHook、Dobby、EIEM、7-Zip 等依赖或引用保留各自许可证和来源说明；创作者应自行确认第三方模型、动作、音频和模块的分发授权。

功能效果取决于客户端、设备和用户导入内容。使用前请了解相关服务规则及账号/客户端风险；游戏更新后遇到契约失败，应关闭受影响功能并等待适配。
