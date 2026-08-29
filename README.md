# Better Endfield

Better Endfield 是一个面向《终末地》Windows 客户端的模块化运行时。模型、语音、OmniMix 音乐和战斗数据分别由独立 DLL 提供，Host 负责动态 IL2CPP 解析、Hook 生命周期、配置和模块发现。

## 架构

```text
BetterEndfield.exe
  runtime/BetterEndfield.Host.dll
  modules/BetterEndfield.Model.dll
  modules/BetterEndfield.Voice.dll
  modules/BetterEndfield.Music.dll
  modules/BetterEndfield.CombatStats.dll
  loaders/BetterEndfield.Injector.exe
  payloads/xinput1_4.dll
```

- `BetterEndfield.Host.dll`：唯一的进程内宿主、动态解析器和 HookBroker。
- `BetterEndfield.Model.dll`：开屏视觉、登录演员、模型资源和动画功能模块。
- `BetterEndfield.Voice.dll`：语音语言、Wwise 媒体和口型功能模块。
- `BetterEndfield.Music.dll`：OmniMix PCM、Wwise Audio Input 和原游戏音乐回退模块。
- `BetterEndfield.CombatStats.dll`：伤害数字隐藏、战斗伤害统计、快捷键会话和本地结果模块。
- `BetterEndfield.Injector.exe`：默认加载方式，Host 和模块均从软件目录加载。
- `payloads/xinput1_4.dll`：可选的 XInput 自启动代理，仅在用户确认后部署到游戏目录。

## 源码布局

```text
ui/BetterEndfield.UI/          WinUI 控制器与按领域分类的内嵌资源
native/modules/model/          开屏视觉、角色模型与动画模块
native/modules/voice/          配音语言、Wwise 媒体与口型模块
native/modules/music/          OmniMix 音乐集成模块
native/modules/combat_stats/   战斗数据与伤害显示模块
native/loaders/injector/       外部启动注入器
native/loaders/xinput/         XInput 代理与进程内 Bootstrap
native/shared/                 Host、公共 ABI 头文件与第三方原生依赖
native/research/music_probe/   不进入发布包的音乐诊断模块
manifests/model/               模型与动作资源清单
manifests/voice/               语音 Event/Media 映射清单
manifests/shared/              跨模块资源生成报告
resources/voice/               语音映射生成器的维护输入
android/                       独立开发中的 Android/Xposed 版本
scripts/                       公共构建、清单生成与资源扫描工具
tools/                         本地分析工具和工具链（不进入发布包）
docs/                          运行时接口、研究结论与集成交接文档
```

发布目录仍使用 `runtime/modules/loaders/payloads`，源码归类不会改变现有安装与加载路径。`artifacts`、`runs`、反编译结果和本地工具输出属于工作产物，不参与源码层级整理。

## 已知问题

开屏 Logo、中央色带和两侧色块的最终颜色可能与 UI 选择值存在偏差。这是因为主题色会与游戏源资源自带的颜色数据叠加；该问题在 `2.0.1` 中标记为不会修复，后续版本不再为此调整原始材质改色路径。

模块 ABI 使用纯 C 接口。模块通过程序集、命名空间、类、方法、参数和字段描述符动态解析 IL2CPP；Hook 入口由当前进程的 IL2CPP ABI 与 PE 可执行区间共同验证，不保存客户端地址或文件哈希条件。

## 加载方式

### 内置注入器

这是默认方式。UI 启动 `loaders/BetterEndfield.Injector.exe`，注入器启动目标游戏并加载 `runtime/BetterEndfield.Host.dll`。游戏目录不写入任何 Better Endfield 文件。

### XInput 自启动

当需要与其他加载器共同使用，或者希望通过官方启动器、桌面快捷方式直接启动时，可以安装 XInput 自启动代理。UI 会把 `payloads/xinput1_4.dll` 和一份归属记录写入 `Endfield.exe` 所在目录；游戏加载代理后，代理从 `%LocalAppData%\BetterEndfield\BetterEndfield.ini` 找到软件目录中的 Host。

安装器和设置页都提供卸载。卸载前会验证文件哈希和归属记录，不会覆盖或删除未知的同名 `xinput1_4.dll`；如果其他工具也占用该文件名，请改用内置注入器。项目不包含任何反作弊停用、规避或对抗逻辑。

## 路径与启动参数

UI 会优先验证已保存路径，再检查软件相邻目录、Windows 卸载信息、常见安装目录和固定磁盘根目录下的有限候选，不递归扫描整块磁盘。设置页可随时重新扫描或手动选择 `Endfield.exe` 与 `BetterEndfield.Injector.exe`。

游戏启动参数会同时用于“保存并启动”和一键启动快捷方式。例如填写 `-force-d3d11` 可要求 Unity 使用 Direct3D 11。内置注入器会把这些参数放在自身 `--` 分隔符之后再传给游戏。

## B 服兼容

B 服不通过官服 `GameAssembly.dll` 哈希判定。Host 在运行时解析 IL2CPP 元数据，模块只验证自己声明的类、方法、字段和资源契约。登录 SDK 或登录资源差异不会被当作全局失败条件。

模型和语音资源目录由当前游戏目录生成，PCK、BNK/HIRC 和 `AudioDialog` 不编译进 DLL。开屏模块分别解析模型替换、Logo 与登录色带契约；某个视觉契约缺失只停用对应能力，不会阻断其他功能，也不会套用官服地址。

音乐模块同样不验证 `GameAssembly.dll` 身份。它按完整 IL2CPP 元数据签名解析 `AudioMusicSystem`、`AkAudioInputManager` 与 Unity 主线程入口；官服/B 服登录 SDK 和登录资源差异不参与音乐契约。

战斗数据模块默认关闭。启用后按 `hotkey_toggle`（默认 F11）开始或停止一次统计会话，动态挂接
`BattleRecorder.RecordDamage(ref AbilitySystem.Modifier)`，从普通地图和关卡共用的结算后路径读取
攻击者、技能、伤害类型、伤害值和暴击字段；隐藏数字只挂接最终 UI 层的
`DamageTextCtrl/DamageTextCtrlV2._OnHpChanged`，不会阻断伤害计算、生命值或韧性流程。
结果写入 `%LocalAppData%\\BetterEndfield\\combat-sessions`，UI 的“战斗数据”页可刷新历史文件并显示总伤害排行。
模块会按需启动随软件分发的 `BetterEndfield.CombatOverlay.exe`，通过当前进程专属共享内存展示
角色头像、伤害排行、DPS 和按普攻、战技、终结技、连携技等技能分类分色的横向柱状图；F12（可配置）显示或隐藏，按住 Ctrl
并用鼠标左键拖动可保存相对游戏窗口的位置。悬浮窗不依赖 Better Endfield 主界面常驻，也不会联网读取头像。
伤害数字从一万起按每 10 倍切换“万、×10万、×100万、×1000万、亿”等显示单位。每次会话还会保存
0.25 秒粒度的技能分类与角色双维度时间桶；历史页默认显示最近三条，可按日期和最多四名参战角色筛选、删除记录，
并在角色排行与可拖动双端点的时间轴柱状图之间切换。时间轴可按技能类型或角色显示，并随模式显示对应图例。
开启 rDPS 口径后，模块按单次伤害实际扣血量守恒分配“直伤、攻击力、增伤、增幅、脆弱、承伤易伤、
减防/减抗、连携增益、法术强度、其他”十类贡献。跨乘区按乘数对数权重分配，同一乘区内按实际观测增量分配；
角色自身效果保留在直伤，只有其他角色提供且语义已验证的效果才转移贡献。随版本发布的
`modules/combat-semantics.besem` 提供 Buff、技能、元素和乘区语义，运行时不读取独立更新目录；软件升级时随模块一并更新。
每条新记录保存目录版本、验证覆盖率和有界未解析项审计，历史页可直接查看，无法验证的候选项不会参与 rDPS。
当前战斗记录使用 schema 11，只保存可验证的操作、原子结果、队伍快照和会话摘要；历史排行、技能统计、Buff 区间与时间轴均在读取时派生，不兼容更早的开发格式。64 位实例 ID 使用十进制字符串，避免浏览器解析时丢失精度。
字段和方法均按 IL2CPP 元数据描述解析，契约缺失时只停用该模块。schema 11 不按时间或 ID 前缀猜测归属，无法唯一验证的来源明确记录为未知。

## OmniMix 音乐集成

音乐集成默认关闭。Better Endfield 只保存用户选择的 `OmniMixPlayer.Backend.exe` 绝对路径，并从该后端的 `native\x64` 目录动态加载兼容的 `OmniPcmShared.dll`；不会复制曲库、音频或 OmniMix 程序。注册和运行时都会验证 OmniPcmShared ABI `2.x`、共享协议 `2` 与交错 `float32` 能力。后端路径缺失、ABI 不兼容、心跳中断或 PCM 缓冲不足时，模块保持或恢复原游戏音乐。

正式链路为：

```text
OmniMix instance shared memory
  -> BetterEndfield.Music 工作线程
  -> 48 kHz 立体声 SPSC 缓冲
  -> Wwise Audio Input Event
  -> 游戏 Music Bus
```

只有共享流、预缓冲、格式回调和采样回调全部健康后，模块才按登录、主界面/基地、游戏内三个独立范围暂停对应原生 Playing ID。它不会静音全局 Music Bus；Audio Input 暂态失败会退避重试，可闻游标额外保留 100 ms 输出队列余量。OmniMix 项目组的完整对接契约见 [`docs/OMNIMIX_INTEGRATION_HANDOFF.md`](docs/OMNIMIX_INTEGRATION_HANDOFF.md)。

Better Endfield 在实例握手中声明播放队列管理和 Seek 能力，因此可以直接在 OmniMix 中向该游戏实例添加、插入、移动和清空队列。

## 资源目录

UI 在保存配音规则时会从本机 PCK 选择性生成所需 Catalog。生成物位于
`%LocalAppData%\BetterEndfield\catalog`，发布包不会携带 PCK、BNK、WEM 或
`.becat`。角色规则写入配置前，UI 会先完成对应语言 Catalog 的原子更新；已删除
规则所对应的旧文件只会在 UI 自己的生成记录范围内清理。

开发或诊断时也可以手工生成：

```powershell
py -3 .\scripts\BuildVoiceCatalog.py `
  --game-path 'E:\Endfield Game' `
  --language Japanese `
  --character-id chr_0013_aglina `
  --output "$env:LOCALAPPDATA\BetterEndfield\catalog\voice.japanese.chr_0013_aglina.becat"
```

Catalog 只包含目标角色需要的 WEM，重复目标 Media 只存储一次。运行时会把所有已配置角色的 Catalog 合并为一张常驻路由表，并在第一条已配置角色语音到达、Wwise 已就绪时通过 `SetMedia` 一次性注册；其他角色发声不会触发卸载或重新读取。嵌入 UI 的索引只包含 Media ID、语言包指纹和相对路径，不包含音频内容；若官服与 B 服的 PCK 内容相同，即使 `GameAssembly.dll` 不同也复用同一映射，路径变化时会按 PCK 大小和解密后头部哈希定位。

## 构建

环境要求：Windows 10/11 x64、Visual Studio 2022 C++ 工具集、CMake、.NET SDK 9.0 和 Inno Setup 6。

```powershell
pwsh -File .\scripts\BuildBetterEndfield.ps1
pwsh -File .\scripts\BuildInstaller.ps1 -Version 2.2.1
```

原生构建入口是 `native/CMakeLists.txt`。MinHook 只由 Host 链接，模块不得自行初始化或卸载 Hook 引擎。

## 配置

主配置位于 `%LocalAppData%\BetterEndfield\BetterEndfield.ini`，使用 UTF-16LE BOM 以保证 Windows Profile API 能无损读取中文路径；UI 设置位于同目录的 `ui-settings.json`。配置按模块分节：

```ini
[betterendfield.model]
enabled=false
model_replacement_enabled=false
logo_theme_enabled=false
logo_theme_color=#FFC928

[betterendfield.voice]
enabled=false
voice_router_enabled=false
voice_language_rules=*:Japanese

[betterendfield.music]
enabled=false
music_replacement_enabled=false
backend_exe=C:\Path\To\OmniMixPlayer.Backend.exe
client_id=better-endfield-example
replace_login=true
replace_meta=true
replace_gameplay=true
target_latency=0.4
prebuffer_ms=150
fallback_to_native=true

[betterendfield.combat_stats]
enabled=false
combat_stats_enabled=false
hide_damage_numbers=false
overlay_enabled=true
hotkey_toggle=F11
overlay_hotkey=F12
rdps_display=false
auto_dungeon_session=true

[Loader]
install_root=C:\Path\To\Better Endfield
load_host=true
```

## 许可与风险

本项目以 [AGPL-3.0-only](LICENSE) 发布。第三方 MinHook 保留其原许可证，副本位于 `native/shared/third_party/minhook`。

Better Endfield 与游戏发行商无关。使用前请备份配置并自行评估账号、客户端完整性和第三方 Mod 冲突风险。游戏更新后如果动态契约不满足，请停止使用对应模块并等待适配。
