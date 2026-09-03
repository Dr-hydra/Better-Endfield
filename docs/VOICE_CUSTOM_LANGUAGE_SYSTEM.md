# 游戏语音"按角色自定义语言"系统解析(2026-09-03 客户端)

本文回答一个问题:2026-09-03 客户端更新中,游戏是否新增了"按角色(per-speaker)自定义语言"的
语音系统?结论按证据强度分三级:**实证**(IL2CPP dump 可直接证明)、**推断**(结构合理但需行为验证)、
**待验证**(静态 dump 不可见,已列入 IDA/动态验证清单 §7)。

背景与总对照见 [GAME_UPDATE_20260903_HOOK_DIFF.md](GAME_UPDATE_20260903_HOOK_DIFF.md);
接口契约背景见 [GAME_INTERFACES.md](GAME_INTERFACES.md)。

## 结论速览

| 级别 | 结论 |
|---|---|
| 实证 | `Beyond.Gameplay.Audio.VoiceI18n` 本次更新新增静态字典 `s_speakerCustomLangMap`(speaker → 语言名字符串)与三个方法:`SetSpeakerCustomLang(String, AudioLang)`、`TryGetSpeakerCustomLang(String, String&)`、`GetCustomLanguageVoicePath(String, String&, EVoiceDevStage, String)`;全库扫描确认 custom-language 命名仅此一处 |
| 实证 | `VoiceI18n` 既有 6 个方法(`SetLanguage/GetCurrentLanguage/GetLanguageName/GetVoicePath/GetDebugVoicePath/.cctor`)签名不变;`AudioLang` 枚举仍为 Chinese/English/Japanese/Korean 四值 |
| 实证 | VFS 块类型(`EVFSBlockType` 23 值)与文件标签(`EVFSFileTag` 2 值)未变;`AudioVFSLoader` 16 个方法签名不变;每条语音的配置行 `Beyond.Cfg.VoiceData` 列集未变 |
| 推断 | 该系统允许为**特定 speaker** 把语音路径的语言段覆盖为其他语言,即"这个角色说话时改用另一语言的语音目录" |
| 待验证 | 映射表的填充来源(配置表/服务器下发/IFix 补丁)、`GetVoicePath` 主体是否查询该表、额外语言小包的语义 —— 见 §7 |

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
static Dictionary<String, String> s_speakerCustomLangMap;
// 既有(未变)
static readonly Regex VERSION_PATH_PREFIX_REGEX;   // static @ 0x0
static event Action<AudioLang> OnVoiceLanguageChanged;  // static @ 0x8
static String    s_languagePrefix;                 // static @ 0x10
static AudioLang s_currentLanguage;                // static @ 0x18
```

### 1.3 语义判读(推断)

- 映射表是 **speaker → String**:键为角色 speaker 标识(`chr_xxx` 或 speakerChannel 名),
  值为**语言名字符串**(与路径中的语言段同形态,如 `"japanese"`)。
  `SetSpeakerCustomLang` 入参是 `AudioLang` 枚举,内部应经 `GetLanguageName(lang)` 转成
  字符串存表——因此该系统仍然只在 **AudioLang 四语言**范围内做覆盖,并不引入"第五种语言"。
- `TryGetSpeakerCustomLang` 输出 `String& lang`,与"值为语言名"互洽。
- `GetCustomLanguageVoicePath(path, out voPath, devStage, lang)` 与 `GetVoicePath`
  只差一个末位 `lang` 参数:即**同一套路径构造逻辑,但用调用方给定的语言名覆盖语言段**。
  结合 `TryGetSpeakerCustomLang`,典型用法是:
  `TryGetSpeakerCustomLang(speaker, out lang)` 命中 → `GetCustomLanguageVoicePath(path, out voPath, devStage, lang)`;
  未命中 → 走原 `GetVoicePath`。
- `path` 入参以 `String` 传入、`voPath` 以 `String&` 出参返回,与既有的
  "传入逻辑路径、返回拼接后的可寻址路径"模式一致(见 §2 的运行时路径形态)。

## 2. 语音路径解剖

常量(新旧 dump 完全一致,strings dump 原文):

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

## 3. 配置来源排查(实证边界)

对"谁填充 `s_speakerCustomLangMap`"做了如下排除(均为实证):

| 排查点 | 结果 |
|---|---|
| `Beyond.Cfg.VoiceData`(每条语音的配置行) | 未变。列集为 `RTPCMap{,CN,EN,JP,KR}`、`wavDuration{,EN,JP,KR}`、`path`、`speakerChannel`、`overrideWwiseEvent`、`codec`、`devStage`、`isPlaceholder`、`voType`——**没有自定义语言列** |
| 本次新增的配置表 | `Beyond.Cfg` 共 +32 类,其中语音相关仅 `VoiceSpeakerDetailData`(列:`characterName/characterType/commonVoActor/ingameNpcName/placeholderActor/unityID/wwiseID`)——是配音演员/占位语音元数据表,**无语言列** |
| 全库命名扫描 | `customlang|custom_language|speakerlang|langoverride` 模式在两套 dump 的全部 170 个程序集文件中,**仅命中 VoiceI18n 自身**(及 IFix 的 IDMAP 索引行) |
| 网络协议 | `Beyond.Network.Proto` 体量有增长,但无 custom/lang 相关类名命中 |
| IFix 热补丁体系 | 三个新方法在 `IFix.IDMAP0/IDMAP1` 有映射(`Beyond-Gameplay-Audio-VoiceI18n-SetSpeakerCustomLang0` 等),且 `EVFSBlockType.IFixPatch` 块类型存在 |

判读(推断):映射表的**填充与生效逻辑不在静态 IL2CPP 元数据可见范围内**——最可能由
IFix 补丁脚本或服务器/设置下发在运行时调用 `SetSpeakerCustomLang`。因此"当前版本里哪些角色
被配置了自定义语言"无法从 dump 得知,需动态验证(§7)。

## 4. 与 VFS 语言包加载的关系

- `EVFSBlockType`(None/InitialAudio/…/Audio/AudioChinese/AudioEnglish/AudioJapanese/AudioKorean/HotfixAudio/Raw,23 值)
  与 `EVFSFileTag`(None/Audit)**新旧完全一致**——本系统没有引入新的资源块类型。
- `AudioVFSLoader` 16 个方法签名不变,内部机制为:语言块类型由
  `_GetVfsBlockTypeFromLanguage(language)` 映射,`_DoLoadLanguageAndHotfixPck(langBlockType)`
  一次性装载该语言的常规 PCK 与热修 PCK,`_SplitChunksByTag` 按 tag 拆分 chunk。
- 本次更新后 VFS 中每个非中文语言出现 **2 个 `.chk`**(主包 ~1.6GB + 小包 33–107MB;
  中文仍 1 个 1.47GB,见 `ui/BetterEndfield.UI/Assets/voice/voice-catalog-index.json`)。
  在块类型未变的前提下,这些包仍由既有路径装载。**小包语义已定案(§7-Q3)**:
  与主包媒体完全不相交、全部不在语音 manifest 内、以 SFX 类媒体为主
  (JP:1413/1534)——是随语言发布的增量/热修类音频(新内容),
  **不是**自定义语言语音包;自定义语言的内容投放应经主包 + IFix 路由层。
- 关键约束(推断,影响 §6 机会评估):`TryLoadLanguagePck` 只装载**当前语言**的块;
  若某角色被配置为其他语言,其语音所在的其他语言媒体默认**不在 Wwise 中**——
  官方要让该系统出声,必然伴随跨语言媒体装载或 Wwise 媒体注册,这正是本系统最值得
  逆向确认的部分。

## 5. 与播放链、时长、口型的关系

- 播放链(简化):`VoicePlayer.PlayVoice → 路径解析(GetVoicePath / GetCustomLanguageVoicePath)
  → External Source / Wwise 媒体 → AudioAdapter.PostEventExternal`。
- 本次更新**同期**重构了同一条链路的时长与进度:VoiceContext 池化移入 `VoiceContextRegistry`、
  新增 Seek 族(`SeekVoice/_FlushPendingSeek/pendingSeekTimeMs`)。自定义语言是在这条
  重做过的链路上新加的维度,两者互不替代。
- `VoiceData` 配置行保留每语言 `wavDurationEN/JP/KR` 列 → 时长查询仍按 AudioLang 四语言查表;
  mod 的时长覆盖(线程局部语言 override 后重查 `TryGetVoiceDuration`)与该机制正交,理论上兼容,
  但整体时长路径已受 Seek 重构影响(见 GAME_UPDATE 文档 §5.3)。

## 6. 对 Better Endfield 的影响与机会

### 6.1 风险与适配建议

1. **路径语言段不再必然等于当前语言。** mod 的外部语音路由(`[voice-external]`)与 catalog
   匹配目前以路径/事件为键;若游戏为某些 speaker 启用自定义语言,匹配前应把路径中的语言段
   归一化(或改用 speakerChannel/voiceId 维度匹配),避免规则失配。
2. **时长/口型 override 的线程局部 `GetCurrentLanguage` 覆盖**:若游戏自定义语言分支读取的是
   `s_currentLanguage` 字段而非经 `GetCurrentLanguage` 方法(hook 不可见),mod 的语言覆盖
   对该分支不生效。需动态确认(§7-Q4)。
3. **多包结构(已定案)**:小包为随语言发布的增量/热修类音频(§7-Q3),与主包媒体不相交、
   不在语音 manifest 内;mod 解析器多包化保留即可,catalog 构建继续按
   "目标语音确在所选包中"校验 media(与 GAME_UPDATE 文档 §5.2 的无声问题对照——
   该问题已定案为 IFix 层卸载绕过,与小包无关)。

### 6.2 机会:原生按角色换语言(实验方向,默认关闭)

`SetSpeakerCustomLang` 是官方的"按角色换语言"入口。mod 可以经 Host 的 `runtime_invoke`
调用它,把某个 speaker 指到目标语言,让游戏自己完成路径覆盖;mod 的 catalog `SetMedia`
常驻数据恰好能解决"目标语言媒体未装载"的内容供给问题。这构成 mod 现有
"事件/路径匹配 + Wwise 媒体重定向"之外的第二条实现路径。前提核验状态(§7):
- Q1 已答:`GetVoicePath` 静态代码**不查**映射表 → 表的消费方在 IFix 补丁层;
  mod 注入的表项能否被消费,取决于补丁层运行时行为,**需动态验证后才能立项**;
- Q4 已答:口型链路的 override 通道有效,时长链路无效(时长覆盖需另行实现,见
  GAME_UPDATE 文档 §5.3);
- 与 mod 现有规则表的冲突面(两套覆盖同时生效时的优先级)需要在实现前定义清楚。

建议:保留为规则后端的备选实现,暂不投入;待动态验证(注入一个测试 speaker 表项并
观察路径覆盖是否发生)后再评估。

## 7. 待验证问题清单 → 核验结果(2026-09-03 字节级函数体扫描 + 日志)

| # | 问题 | 核验结果 |
|---|---|---|
| Q1 | `GetVoicePath` 主体是否读取 `s_speakerCustomLangMap` 并改走 `GetCustomLanguageVoicePath`? | **已答(静态)**:`GetVoicePath`(0x05DFA8F4,约 0x100 字节)体内**没有任何**对 `TryGetSpeakerCustomLang/GetCustomLanguageVoicePath` 的调用,语言前缀取自 `s_languagePrefix` 静态字段——静态代码不查自定义语言表;整个四件套(`GetVoicePath/SetSpeakerCustomLang/TryGetSpeakerCustomLang/GetCustomLanguageVoicePath`)在静态代码中**零调用方**,消费方在 IFix 补丁层(`VoiceI18n` 全类在 IFix IDMAP 登记;运行时行为需动态确认) |
| Q2 | 谁调用 `SetSpeakerCustomLang`? | **已答(静态)**:零静态调用方,仅剩 `.data` 方法指针表槽位(0x0D6A16F0)→ 由 IFix 补丁/配置在运行时填充;`SetSpeakerCustomLang` 体内确认先经 `GetLanguageName(lang)` 转语言名再入表(值形态 = 路径语言段字符串) |
| Q3 | 非中文语言的第二个 `.chk` 是热修分包还是自定义语言内容? | **已答:两者都不是"自定义语言语音包"**。JP 小包 1534 条媒体与 JP 主包 30101 条**交集为 0**,且全部不在语音事件 manifest 内;语言直方图以 SFX(语言无关)为主(1413/1534,日语仅 121)→ 是随语言发布的增量/热修类音频(新内容),不是跨语言语音内容。`EVFSBlockType/EVFSFileTag` 未变,装载走既有"语言+热修"路径 |
| Q4 | 时长/口型链路读取的是 `GetCurrentLanguage()`(可被 hook)还是 `s_currentLanguage` 字段? | **已答(拆开)**:**口型**——`LipSyncUtils.GetLipSyncTrackPath` 体内两处直调 `GetCurrentLanguage`(E8 0x05F9639E/0x05F964A2)→ mod 的线程局部 override **有效**;**时长**——`TryGetVoiceDuration(String)` 与 `_GetVoDurationFromVoData` 体内**零** `GetCurrentLanguage` 调用 → mod 的语言覆盖对游戏时长计算**无效**(与运行日志全程零 `[voice-language]` 行吻合),时长覆盖需改由 mod 直接写回替换音频时长(GAME_UPDATE 文档 §5.3) |
| Q5 | 路径语言段覆盖后,Wwise 侧如何保证对应语言媒体可用? | **部分已答**:`TryLoadLanguagePck →(尾跳)→ _DoLoadLanguageAndHotfixPck → 内联 _UnloadPcks(经 IFix 包装)`,一次日语辅助挂载实测装载 14 个包(3 个日语组 + 11 个基础组 `F151B649`)——挂载动作本身能带回大量媒体;但卸载同样经 IFix 包装、绕过 mod 的 `UnloadFilePackage` hook(GAME_UPDATE 文档 §5.2 无声问题的直接原因) |

补充:本次同时确认了 **IFix 包装分发**是这批"零静态调用方"现象的成因——对 mod 的启示是
hook 点应尽量选最低稳定层(PINVOKE/被补丁引用的原生方法),不要只挂在高层包装器上。

## 8. 附录

### 8.1 VoiceI18n 新旧全量对照

OLD(2026-08-03):字段 7 个(VOICE_ROOT/RAW_VOICE_ROOT/PLACEHOLDER_VOICE_ROOT 常量、
VERSION_PATH_PREFIX_REGEX、OnVoiceLanguageChanged、s_languagePrefix、s_currentLanguage),
方法 6 个;NEW(2026-09-03):字段 +`s_speakerCustomLangMap`(static @ 0x20),
方法 +3(§1.1),其余完全一致。完整类块原文见本节末尾说明(由 dump AI 模式直接摘取,
RVA/token 见 GAME_UPDATE 文档 §3.2)。

### 8.2 排查方法

以 `IL2CPP_Dump_AI` 的 `CLASS: … END_CLASS` 块为单位,对全部程序集做
`customlang|custom_language|speakerlang|languageoverride|langoverride` 模式扫描(类名 + 成员行),
并对 `Beyond.Cfg/Beyond.GEnums/Beyond.VFS/Beyond.Gameplay.Audio/Beyond.I18n/Beyond.Audio`
六个命名空间做类名集合双向 diff;strings dump 以常量字符串佐证。分析用一次性脚本已随收尾清理,
方法可按本节复现。
