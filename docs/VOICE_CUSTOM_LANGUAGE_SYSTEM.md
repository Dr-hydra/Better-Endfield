# 游戏语音"按角色自定义语言"系统解析(2026-09-03 客户端)

本文回答一个问题:2026-09-03 客户端更新中,游戏是否新增了"按角色(per-speaker)自定义语言"的
语音系统,它在当前构建里是否真的在工作。结论按证据强度分三级:**实证**(IL2CPP dump 或
GameAssembly.dll 字节级证据可直接证明)、**推断**(结构合理但需行为验证)、**待验证**(静态
不可见,已列入 §7)。

> 2026-09-05 修订:首版把"零静态调用方"归因于 IFix 包装分发,并据此认为自定义语言表的消费方
> 在热补丁层。复核后确认这是误判——真实原因是 IL2CPP/MSVC 的整程序内联,消费方就在静态代码
> `VoicePlayer._PlayVoice` 里(§3)。同类误判的修正见 [GAME_UPDATE_20260903_HOOK_DIFF.md](GAME_UPDATE_20260903_HOOK_DIFF.md) §5.2/§7。

背景与总对照见 [GAME_UPDATE_20260903_HOOK_DIFF.md](GAME_UPDATE_20260903_HOOK_DIFF.md);
接口契约背景见 [GAME_INTERFACES.md](GAME_INTERFACES.md)。

## 结论速览

| 级别 | 结论 |
|---|---|
| 实证 | `Beyond.Gameplay.Audio.VoiceI18n` 本次更新新增静态字典 `s_speakerCustomLangMap`(speaker → 语言名字符串)与三个方法:`SetSpeakerCustomLang(String, AudioLang)`、`TryGetSpeakerCustomLang(String, String&)`、`GetCustomLanguageVoicePath(String, String&, EVoiceDevStage, String)`;全库扫描确认 custom-language 命名仅此一处 |
| 实证 | `VoiceI18n` 既有 6 个方法签名不变;`AudioLang` 仍为 Chinese/English/Japanese/Korean 四值;VFS 块类型/文件标签、`AudioVFSLoader`、`Beyond.Cfg.VoiceData` 列集均未变 |
| 实证 | **读侧已接线**:`VoicePlayer._PlayVoice` 在构造外部语音路径时先以 `voiceContext.voiceData.speakerChannel` 查表,命中则走 `GetCustomLanguageVoicePath`(用表中语言名替换路径语言段),未命中走 `GetVoicePath`。三件套均被内联,静态调用图上因此看不到调用 |
| 实证 | **写侧在静态代码中休眠**:`SetSpeakerCustomLang` 全库零 call/jmp/LEA 引用、无内联副本;1,338 个 Lua 文件亦不引用 `VoiceI18n`。字典由 `.cctor` 分配为空表,所以当前构建里查表每次都不命中,行为与旧版一致 |
| 实证 | 时长(`_GetVoDurationFromVoData` 直读 `s_currentLanguage`)与口型(`GetLipSyncTrackPath` 调 `GetCurrentLanguage`)都**不查**该表——即便某 speaker 被官方切到其他语言,其时长列与口型轨仍按全局语言取 |
| 待验证 | 谁在运行时调用 `SetSpeakerCustomLang`(IFix 补丁 / 服务器下发 / 尚未启用),以及切换后目标语言媒体如何保证已装载 —— 见 §7 |

## 1. 核心证据:VoiceI18n 的新增成员

### 1.1 方法签名(带 AI dump 参数名)

```csharp
// 新增
static void  SetSpeakerCustomLang(String speaker, AudioLang lang);                     // 0x05DFA9F4
static bool  TryGetSpeakerCustomLang(String speaker, out String lang);                 // 0x03FA2C30
static void  GetCustomLanguageVoicePath(String path, out String voPath,
                                        EVoiceDevStage devStage, String lang);         // 0x05DFA7C4
// 对照:既有(签名未变)
static void  GetVoicePath(String path, out String voPath, EVoiceDevStage devStage);    // 0x06B02B1C → 0x05DFA8F4
static void  GetDebugVoicePath(String path, out String voPath, EVoiceDevStage devStage); // 0x06B0296C → 0x03FA2B70
```

### 1.2 字段与生命周期

```csharp
// 新增(static @ 0x20)
static Dictionary<String, String> s_speakerCustomLangMap;   // .cctor 中 new 为空表
// 既有(未变)
static readonly Regex VERSION_PATH_PREFIX_REGEX;   // static @ 0x0
static event Action<AudioLang> OnVoiceLanguageChanged;  // static @ 0x8
static String    s_languagePrefix;                 // static @ 0x10
static AudioLang s_currentLanguage;                // static @ 0x18
```

### 1.3 函数体判读(实证)

- `SetSpeakerCustomLang` 体内先调 `GetLanguageName(lang)` 把枚举转成语言名字符串再入表——值形态即路径
  语言段(如 `"japanese"`),系统只在 **AudioLang 四语言**范围内覆盖,不引入"第五种语言"。
- `TryGetSpeakerCustomLang` 体 = `IsPatched(0x392c)` 前导 → 读 `static+0x20` → `Dictionary<string,string>.TryGetValue`。
- `GetCustomLanguageVoicePath` 与 `GetVoicePath` 共用同一段路径拼接(同样的类初始化序列 +
  `ThreadUnsafeStringUtils.AppendPathInfo`),只差语言段来源:前者用入参 `lang`,后者读
  `s_languagePrefix`(`static+0x10`)。`GetVoicePath` 体内**不读** `static+0x20`,也没有内联的查表逻辑。
- `GetVoicePath` 没有 IFix 前导、也不在 IDMAP 中;三个新方法都带 `IsPatched` 前导并登记在
  IDMAP(`TryGetSpeakerCustomLang` id = 0x392c)。这只说明它们**可被**热补丁替换,不说明已被替换。

## 2. 语音路径解剖

常量(新旧 dump 完全一致):

```
Beyond.Gameplay.Audio.VoiceI18n::VOICE_ROOT          = "Voice"
Beyond.Gameplay.Audio.VoiceI18n::RAW_VOICE_ROOT      = "RawBuildVoice"
Beyond.Gameplay.Audio.VoiceI18n::PLACEHOLDER_VOICE_ROOT = "PlaceholderVoice"
```

运行时观察到的最终路径形态(取自 9/2 更新后 mod 日志 `[voice-external]` 行):

```
voice/chinese/v1d4/characters/chr_0035_liino/chr_0035_liino_sim_talk_lv01_01.wem
voice/japanese/v1d4/characters/chr_0035_liino/chr_0035_liino_sim_talk_lv01_01.wem
```

结构为 `voice/<语言名>/v<版本>/characters/<speaker>/<voiceId>.wem`:
- `v1d4` 版本段由静态 `VERSION_PATH_PREFIX_REGEX` 处理(新旧均存在);
- **语言段即本系统的覆盖对象**——`GetCustomLanguageVoicePath` 的 `lang` 参数替换的正是这一段。

## 3. 消费方定位:VoicePlayer._PlayVoice(实证)

对 `VoiceI18n` 类指针(`.data` 0x0D08D0A0)做全 `.text` rip 相对引用扫描,并逐一核对各引用点访问的
静态字段偏移,得到本系统的完整读写面:

| 函数 | 访问的 VoiceI18n 静态 | 含义 |
|---|---|---|
| `VoicePlayer._PlayVoice`(0x03FA2CE0,+0x960 ~ +0xBDC) | `+0x20` 读 | **查自定义语言表**(见下) |
| `VoiceI18n.TryGetSpeakerCustomLang` / `SetSpeakerCustomLang` | `+0x20` 读 / 写 | 自身实现 |
| `VoiceI18n..cctor` | `+0x20` 写 | `new Dictionary<string,string>()` 空表 |
| `VoiceUtils._GetVoDurationFromVoData`(+0x5A2) | `+0x18` 读 | 时长按 `s_currentLanguage` 选列 |
| `LipSyncUtils.GetLipSyncTrackPath` ×2、`RuntimeVoiceData.FromSparkBuffer`、`AudioManager.OnInit` | 经 `GetCurrentLanguage` | 全局语言 |
| `VoiceI18n.GetVoicePath` / `GetDebugVoicePath` / `SetLanguage` / `GameSettingSetter._OnLanguageAudioChange` | `+0x10` / `+0x18` | 语言前缀与全局语言 |
| `VoiceManager._DoInit` / `_DoRelease` | `+0x8` | 订阅/退订 `OnVoiceLanguageChanged` |

`_PlayVoice` 中的逻辑(反汇编还原):

```
speaker = voiceContext.voiceData.speakerChannel     // VoiceContext+0x48 内嵌结构体 +0x08
path    = voiceContext.voiceData.data               // +0x18
if (IsPatched(0x392c)) hit = GetPatch(0x392c).__Gen_Wrap_522(speaker, out lang)  // IFix 前导
else                   hit = s_speakerCustomLangMap != null && TryGetValue(speaker, out lang)
if (hit) GetCustomLanguageVoicePath(path, out voPath, devStage, lang)   // 内联
else     GetVoicePath(path, out voPath, devStage)                       // 内联
```

因此本系统的"读侧"是**发布构建里的静态代码**,不依赖任何热补丁。键是 `speakerChannel`(不是
`chr_xxx` 目录名,尽管多数角色两者同形)。

`GetVoicePath`、`GetCustomLanguageVoicePath`、`TryGetSpeakerCustomLang`、`SetSpeakerCustomLang`
四者全库零 call/jmp/LEA 引用,原因是 IL2CPP 生成的 C++ 经 MSVC 整程序优化后**全部内联**进调用方
(`VoicePlayer._PlayVoice` 同时含 `GetVoicePath` 与 `GetCustomLanguageVoicePath` 的副本)。这与
`VoiceData.get_wavDuration*` 零调用方、`AkSoundEngine.UnloadFilePackage` 零调用方、
`InitMainPathHash` 被整体内联是同一现象;它们体内出现的 `IsPatched / GetPatch / __Gen_Wrap_N`
只是 IFix 给可补丁方法统一插入的前导,**不构成**"该方法已被热补丁接管"的证据。

## 4. 填充来源排查(写侧)

| 排查点 | 结果 |
|---|---|
| 静态代码对 `SetSpeakerCustomLang` 的引用 | 零 call/jmp/LEA;对 `static+0x20` 的写入只有 `.cctor`(空表)与它自身 |
| Lua(当前 PC VFS 全部 1,338 个覆盖文件,解密全量扫描) | `VoiceI18n` 零引用(Lua 侧语音只用 `CS.Beyond.Gameplay.Audio.VoiceUtils`) |
| `Beyond.Cfg.VoiceData` | 列集未变:`RTPCMap{,CN,EN,JP,KR}`、`wavDuration{,EN,JP,KR}`、`path`、`speakerChannel`、`overrideWwiseEvent`、`codec`、`devStage`、`isPlaceholder`、`voType`——**没有自定义语言列** |
| 本次新增配置表 | `Beyond.Cfg` +32 类,语音相关仅 `VoiceSpeakerDetailData`(配音演员/占位语音元数据),**无语言列** |
| 全库命名扫描 | `customlang|custom_language|speakerlang|langoverride` 在 170 个程序集文件中仅命中 `VoiceI18n` 自身(及 IFix IDMAP 索引行) |
| 网络协议 | `Beyond.Network.Proto` 有增长,但无 custom/lang 相关类名 |

判读:写侧在当前构建的静态代码里**休眠**。可能的运行时填充者只剩两类——IFix 补丁脚本经反射/
方法指针表调用 `SetSpeakerCustomLang`(该方法在 IDMAP1 登记,可被补丁直接调用),或功能尚未
启用、仅为后续版本预埋。两者静态不可区分,需 §7 的运行时观察。

## 5. 与 VFS 语言包加载的关系

- `EVFSBlockType`(23 值)与 `EVFSFileTag`(2 值)新旧完全一致——本系统没有引入新的资源块类型。
- `AudioVFSLoader` 16 个方法签名不变:`_GetVfsBlockTypeFromLanguage(language)` 映射语言块类型,
  `_DoLoadLanguageAndHotfixPck(langBlockType)` 一次性装载该语言的常规 PCK 与热修 PCK。
- 本次更新后每个非中文语言出现 **2 个 `.chk`**(主包 ~1.6GB + 小包 33–107MB)。小包语义已定案:
  与主包媒体完全不相交、全部不在语音 manifest 内、以 SFX 为主(JP:1413/1534)——是随语言发布的
  增量/热修类音频,**不是**自定义语言语音包。
- 关键约束(推断):`TryLoadLanguagePck` 只装载**当前语言**的块。若某 speaker 被切到其他语言,
  `GetCustomLanguageVoicePath` 产出的路径指向的媒体默认**不在 Wwise 中**;官方要让该系统出声,
  必须伴随跨语言媒体装载,这一环在当前构建中同样看不到(§7-Q5)。
- 卸载链修正:`AudioVFSLoader._UnloadPcks`(0x041175B0)有 5 个 out-of-line 调用方
  (`_DoLoadLanguageAndHotfixPck`、`UnloadAll`、`_ReloadResourceIndexes`),其体内把
  `AkSoundEngine.UnloadFilePackage → AkSoundEnginePINVOKE.CSharp_UnloadFilePackage` 一路内联到
  `call rax`(缓存的 P/Invoke 原生指针槽 0x0DEA05E8)。首版"卸载经 IFix 包装绕过 hook"是误读;
  mod 现行实现(挂原生指针槽目标 + `_UnloadPcks`)恰好是正确层级。

## 6. 与播放链、时长、口型的关系

- 播放链(简化):`VoicePlayer.PlayVoice → _PlayVoice(speakerChannel 查表 → GetCustomLanguageVoicePath
  / GetVoicePath) → External Source / Wwise 媒体 → AudioAdapter.PostEventExternal`。
- 本次更新**同期**重构了同一条链路的时长与进度(VoiceContext 池化、Seek 族)。自定义语言是在这条
  链路上新加的维度,两者互不替代。
- **时长不跟随**:`_GetVoDurationFromVoData` 直读 `s_currentLanguage` 选 `wavDuration*` 列,不查表、
  也不经 `GetCurrentLanguage`。**口型不跟随**:`GetLipSyncTrackPath` 两个重载各直调一次
  `GetCurrentLanguage`。即官方系统若启用,被切语言的角色会出现"日语音频 + 中文时长/口型"的错位——
  除非补丁层同时改这两处。这与 mod 需要自行路由时长与口型的现状一致。

## 7. 对 Better Endfield 的影响与机会

### 7.1 风险与适配

1. **路径语言段不再必然等于当前语言。** 一旦写侧被启用,`[voice-external]` 看到的源路径语言段会因
   speaker 而异。mod 的 `BuildVoiceReplacementSource` 不看源语言段、直接整段替换为规则语言,所以
   已配置角色仍以 mod 规则为准(覆盖官方表),未配置角色则原样放行、跟随官方表。唯一需要留意的是
   未配置角色被官方切到的语言若未挂载,静音属于游戏侧问题,不应误判为 mod 卸载保护失效。
2. **`GetCurrentLanguage` 线程局部覆盖只对口型有效**(两处直调);时长自 3.1.1 起改挂
   `_GetVoDurationFromVoData` 叶子并读目标语言列,不再依赖该通道。官方系统不会改变这两条链的取值来源。
3. **多包结构**:小包为随语言发布的增量/热修音频,mod 解析器多包化保留即可。

### 7.2 机会:原生按角色换语言(备选后端,默认关闭)

读侧已在静态代码中就位(§3),所以 mod 经 Host `runtime_invoke` 调 `SetSpeakerCustomLang(speaker, lang)`
理论上能让游戏**自己**把该 speaker 的外部路径切到目标语言,不再需要 mod 重写路径;mod 的辅助挂载
与 `SetMedia` 常驻数据可解决"目标语言媒体未装载"。相对首版评估,前提从"取决于补丁层运行时行为"
收窄为两点:
- 验证成本极低:注入一个测试 speaker 表项后,观察 `[voice-external]` 的原始路径语言段是否已变
  (mod 的 `_PlayVoice` 上下文 Hook 在游戏查表之前,可直接读到结果);
- 冲突面:同一 speaker 若同时被官方表与 mod 规则路由,`RouteExternalSource` 会对已切换的路径再做
  一次整段重写(语言一致时 `replacement == source`,不分配新串、按 already-routed 处理;语言不一致时
  mod 规则覆盖官方表)。采用该后端时应按
  speaker 关闭 mod 自己的路径重写;时长/口型仍由 mod 现有 Hook 负责(§6)。

建议:仍不改变现有实现;下次需要改动路由层时,顺带做一次上述注入验证,再决定是否切换。

## 8. 待验证问题清单 → 核验结果

| # | 问题 | 核验结果 |
|---|---|---|
| Q1 | `GetVoicePath` 主体是否读取 `s_speakerCustomLangMap`? | **已答(实证)**:不读。查表在其调用方 `VoicePlayer._PlayVoice` 中完成,命中后走内联的 `GetCustomLanguageVoicePath`,未命中走内联的 `GetVoicePath`(§3)。首版"消费方在 IFix 补丁层"撤回 |
| Q2 | 谁调用 `SetSpeakerCustomLang`? | **静态已答,运行时待验证**:静态代码与 Lua 均零引用,表由 `.cctor` 建为空;运行时填充者只可能是 IFix 补丁经反射调用,或功能尚未启用。验证方法:登录后经 Host 字段解析读 `s_speakerCustomLangMap.Count`(无需 Hook;因方法被内联,挂 `SetSpeakerCustomLang` 入口不可靠) |
| Q3 | 非中文语言的第二个 `.chk` 是热修分包还是自定义语言内容? | **已答**:两者都不是"自定义语言语音包"。JP 小包 1534 条媒体与主包 30101 条交集为 0,全部不在语音 manifest 内,以 SFX 为主(1413/1534)→ 随语言发布的增量/热修音频 |
| Q4 | 时长/口型链路读取的是 `GetCurrentLanguage()` 还是 `s_currentLanguage` 字段? | **已答**:口型——`GetLipSyncTrackPath` 两重载各直调 `GetCurrentLanguage`(E8 0x05F9639E / 0x05F964A2),线程局部覆盖有效;时长——`_GetVoDurationFromVoData` 直读 `s_currentLanguage`,覆盖无效;mod 已改挂该叶子(3.1.1) |
| Q5 | 路径语言段覆盖后,Wwise 侧如何保证对应语言媒体可用? | **部分已答**:静态代码里没有随查表命中而装载其他语言 PCK 的逻辑;`TryLoadLanguagePck →(尾跳)→ _DoLoadLanguageAndHotfixPck` 仍只装当前语言。一次日语辅助挂载实测装载 14 个包(3 个日语组 + 11 个基础组)。官方若启用该系统,装载策略必在补丁层或后续版本 |

## 9. 附录

### 9.1 VoiceI18n 新旧全量对照

OLD(2026-08-03):字段 7 个(VOICE_ROOT/RAW_VOICE_ROOT/PLACEHOLDER_VOICE_ROOT 常量、
VERSION_PATH_PREFIX_REGEX、OnVoiceLanguageChanged、s_languagePrefix、s_currentLanguage),
方法 6 个;NEW(2026-09-03):字段 +`s_speakerCustomLangMap`(static @ 0x20),方法 +3(§1.1),
其余完全一致。RVA/token 见 GAME_UPDATE 文档 §3.2。

### 9.2 排查方法

- 元数据:以 `IL2CPP_Dump_AI` 的 `CLASS: … END_CLASS` 块为单位,对全部程序集做
  `customlang|custom_language|speakerlang|languageoverride|langoverride` 扫描,并对
  `Beyond.Cfg/Beyond.GEnums/Beyond.VFS/Beyond.Gameplay.Audio/Beyond.I18n/Beyond.Audio` 做类名双向 diff。
- 调用图:`tmp_analysis/xref_any.py <rva…>` 扫 `.text` 全部 E8/E9 与 LEA rip 相对引用;
  `tmp_analysis/xref_data.py <data-rva…>` 扫对 `.data` 槽(类指针/静态)的 rip 相对访问;
  `tmp_analysis/mainhash_dis.py <out> <rva…>`(`DIS_LEN`/`DIS_MAXN` 环境变量控制长度)反汇编函数体,
  按 `[klass+0xb8]`(static_fields)后的偏移判定访问的静态字段。
- 判定原则:"零调用方"先假设内联,再用类指针/静态字段引用反查真实消费方;IFix 前导不作为消费证据。
