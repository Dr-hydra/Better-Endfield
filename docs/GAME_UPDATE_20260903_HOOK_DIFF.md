# 2026-09-03 客户端更新:角色替换与配音替换失效差异研究

本文对照更新前(2.3.1,2026-08-03 dump)与更新后(2026-09-03 dump)两套 IL2CPP 结构化 dump,
逐条核对 mod 所 hook 与动态解析的游戏函数,并结合更新后运行日志,给出三个运行时症状的归因与处置建议。
契约命名与模块行为背景参见 [GAME_INTERFACES.md](GAME_INTERFACES.md)。

## 结论速览

1. **全部 hook 契约在新客户端元数据层完好。** 两个模块共 30 余个 hook/解析目标逐一比对:
   类、命名空间、方法名、参数类型、参数个数、返回类型、关键字段偏移**零变化**;
   `Audio.Beyond.dll`(83 类)与 `AK.Wwise.Unity.API.dll`(182 类)的 dump 逐方法数量与签名完全相同,
   仅 RVA 整体平移。9/2 更新后日志亦显示 `[model-contract]`、`[voice-contract]` 全部 ready,
   15+ hook 全部 `installed`。**"游戏更新破坏 hook 绑定"这一假设不成立。**
2. **角色替换的断因是运行时时序,不是契约。** 游戏重构了资源操作调度、路径哈希与登录流程
   (详见 §4.4–4.6),`HashStringPathProcessor.InitMainPathHash` 的执行时机相对 mod hook 安装点被提前,
   触发 `main-hash-callback-likely-predated-hook`(日志证实);工作区"就绪恢复"补丁已使资产门解锁。
3. 替换失败的**第二环节在资产层**:替换体首个 `Animator` 报告 `isHuman=false`、`avatar=null`,
   与游戏内容更新相关,dump 无法直接验证;工作区多候选 Animator 扫描待构建验证。
4. **"波及游戏内模型表现"没有游戏侧 API 变化证据。** Unity 对象克隆/销毁 API 与登录生命周期类
   方法面未变,登录流程也无对象池化迹象。最可能是 mod 失败路径的状态残留(替换体未销毁、
   原演员 Renderer/Animator 未完全恢复、锚点跟随未停),属 mod 侧待修项,见 §5.1。
5. **配音替换:游戏 Wwise/Audio 层逐方法未变,变的是语音数据结构与游戏新增能力。**
   非中文语言包从 1 个/语言变为 2 个/语言(新增 33–107MB 小包),游戏新增"按角色自定义语言"系统
   (`VoiceI18n.SetSpeakerCustomLang` 等)。"未配置角色无声"发生在 mod 新多包解析器/catalog 路径上,
   是 mod 侧问题,方向与验证步骤见 §5.2。
6. **时长修正失效与游戏语音播放链路重构同步发生。** `VoiceContextPool` 被删除并由
   `VoiceContextRegistry` 取代,`VoicePlayer`/`VoiceManager` 新增原生 Seek 一族方法,
   `VoiceContext` 新增 `pendingSeekTimeMs/hasPendingSeek`。`VoiceUtils.TryGetVoiceDuration`
   两个重载仍存在且签名未变,但更新后 `[voice-duration]` 从未命中 → 疑似调用点消失或调用重载切换,
   列为 IDA 兜底验证问题(§7)。

## 1. 背景与材料

| 项 | 内容 |
|---|---|
| 更新前对照 | `legacy/better-endfield-2.3.1/references/source-research/endfield-dumper/`(2.3.1,2026-08-03) |
| 更新后 dump | `research/il2cpp-dumps/20260903-pc-current/`(2026-09-03 02:33,由 `tools/EndfieldDumper/Il2cppDumper.exe` 进程内导出) |
| dump 格式 | `IL2CPP_Dump_Normal`(C# 风格,含 `// RVA: … token: …`、字段偏移)与 `IL2CPP_Dump_AI`(`CLASS:/SIZE:/FIELDS:/METHODS: RVA=… token=…` 结构化块);另有 `IL2CPP_Dump_Strings.txt` |
| 运行日志 | `%LocalAppData%\BetterEndfield\logs\BetterEndfield.log`(追加式,覆盖 2026-08-15 至 09-02 22:17;最后一次运行为 9/2 晚,即本次更新后的适配迭代) |
| mod 基线 | 当前工作区(含未提交适配改动;hook 契约定义相对 HEAD 零改动) |

## 2. mod 的 hook 定位方式与失效模式

Host 通过 `GameAssembly.dll` 的 IL2CPP 导出按
**程序集 → 命名空间 → 类(含嵌套类)→ 方法名 + 参数个数 + 参数类型 + 返回类型**
动态解析,取 `MethodInfo.methodPointer` 并校验其落在 PE 可执行节;字段按名解析、运行时取偏移。
**工程内没有任何硬编码偏移、哈希白名单或特征码**(`native/shared/host/dynamic_resolver.cpp`)。

因此对版本更新而言:

- RVA/metadata token 整体漂移是必然现象,**本身不构成失效**;
- 真正的失效模式只有两类:
  1. **元数据契约变化**(类/方法改名、签名/参数变化、程序集缺失)→ `contract-mismatch`,模块拒绝安装;
  2. **运行时时序/行为变化**(方法仍在、签名未变,但调用时机、调用路径或资产内容改变)→ hook 装上了却"打不中"或行为错位。
- 逐字段解析的字段(`VoiceContext.voiceData`、`RuntimeVoiceData.speakerChannel`、
  `LoginDecorateUI._imgLogo/_targetGlow`)即使布局漂移也会自适应,不构成失效。

## 3. 逐契约对照结果

### 3.1 模型/开屏模块(BetterEndfield.Model.dll)

全部 SAME-SIG(签名一致;RVA 为 旧 → 新):

| key | 目标 | 旧 RVA | 新 RVA |
|---|---|---|---|
| login.scene_root.bind | `Entry.Beyond/Beyond.Login/LoginSceneRoot.OnBindToManager()` | 0x0380F010 | 0x039443D0 |
| resource.main_hash | `Common.Beyond/Beyond.Resource/HashStringPathProcessor.InitMainPathHash()` | 0x04994340 | 0x0472E3D0 |
| resource.initial_hash | 同类 `.InitInitPathHash()` | 0x0311B400 | 0x036BD820 |
| login.animation.tick | `Entry.Beyond/Beyond.Login/LoginSceneAnimCtrl.Tick(Single)` | 0x02F446B0 | 0x03102780 |
| login.animation.release | 同类 `.OnRelease()` | 0x04994870 | 0x0472EB80 |
| login.animation.change_state | 同类 `._ChangeToState(EState)` | 0x03408830 | 0x0368F1B0 |
| login.animation.reset_a1 | 同类 `._ResetToA1()` | 0x03D4F930 | 0x03F2E050 |
| login.animation.special | 同类 `._PlayA1sAndTriggerNext()` | 0x03D4FD90 | 0x03F2DEB0 |
| login.animation.transition | 同类 `._PlayA1A2Impl()` | 0x03D4FB60 | 0x03F2E280 |
| unity.object.clone_with_parent | `UnityEngine.CoreModule/Object.Internal_CloneSingleWithParent(Object,Transform,Boolean)` | 0x03C78C70 | 0x03BD3C20 |
| login.decorate.tick | `Beyond.Login/LoginDecorateUI.Tick(Single)` | 0x03D4F360 | 0x03F2D930 |
| login.decorate.release | 同类 `.OnRelease()` | 0x049947F0 | 0x0472EB00 |
| login.enter_game.value_changed | `Beyond.Login/LoginEnterGamePanel.OnValueChanged(LoginViewModel)` | 0x03406E00 | 0x0368D330 |
| login.band.material_animation.late_tick(可选) | `UI.Beyond/Beyond.UI/UIMaterialAnimation.LateTick(Single)` | 0x03B70D30 | 0x03133460 |

解析调用(非 hook)的游戏侧目标同样全部未变:
`I18NAssetLoader.Load(StringPathHash,Type,RootCategory)`、`FAssetProxyHandle.Get()`、
`ResourceManager.IsInitialized()/IsAsyncIniting()`(该类 57 个方法签名逐一相同,仅新增 2 个调度方法,见 §4.4)。
Unity 层(`Object/GameObject/Transform/Renderer/Animator/AnimationClip/Graphic/Material` 及 Playables 全家)
方法数与签名逐一相同:`UnityEngine.CoreModule` 946→949 类、`AnimationModule` 106→106、`UI` 233→234,
属 Unity 小版本差异,不触及 mod 所用 API。

### 3.2 语音模块(BetterEndfield.Voice.dll)

必需契约 9 条 + 可选契约 11 条,全部 SAME-SIG:

| key | 目标 | 旧 RVA | 新 RVA |
|---|---|---|---|
| voice.player.play | `Gameplay.Beyond/Beyond.Gameplay.Audio/VoicePlayer.PlayVoice(VoiceContext)` | 0x03ABF7B0 | 0x03498970 |
| voice.player.play-internal | 同类 `._PlayVoice(VoiceContext&) → UInt32` | 0x03ABF1B0 | 0x03FA2CE0 |
| voice.player.event | 同类 `._PlayEvent(String,UInt64,UInt32) → UInt32` | 0x03ABF2D0 | 0x03D468E0 |
| voice.channel.play | `VoiceSpeakChannelProcessor._PlayVoice(VoiceContext)` | 0x03ABF3A0 | 0x03498660 |
| voice.manager.narrative | `VoiceManager._SpeakNarrative(String,UInt64,NarrativeVoiceConfig) → UInt32` | 0x045E3F20 | 0x04567A50 |
| voice.duration | `VoiceUtils.TryGetVoiceDuration(String,Single&) → Boolean` | 0x03ABB800 | 0x0364E0E0 |
| voice.duration(重载2) | `VoiceUtils.TryGetVoiceDuration(Int32,Single&) → Boolean` | 0x03ABCE00 | 0x03650240 |
| voice.language | `VoiceI18n.GetCurrentLanguage() → AudioLang` | 0x03AC13B0 | 0x03499610 |
| voice.language-name | `VoiceI18n.GetLanguageName(AudioLang) → String` | 0x03EB40D0 | 0x04114990 |
| wwise.media.set | `AK.Wwise.Unity.API/AkSoundEnginePINVOKE.CSharp_SetMedia(IntPtr,UInt32) → Int32` | 0x053C243C | 0x050ADD48 |
| wwise.media.unset | 同类 `.CSharp_UnsetMedia(IntPtr,UInt32) → Int32` | 0x053C4350 | 0x050AFD60 |
| wwise.package.load | `AkSoundEngine.LoadFilePackage(String,UInt32&) → AKRESULT` | 0x03EB5CD0 | 0x04117500 |
| wwise.package.unload | `AkSoundEngine.UnloadFilePackage(UInt32) → AKRESULT` | 0x053DC840 | 0x04117980 |
| audio.external-event | `Audio.Beyond/Beyond.Audio/AudioAdapter.PostEventExternal(String,UInt64,String,UInt32,AudioCallbackType,AudioEventCallback,Object,AudioCodec) → UInt32` | 0x03ABF0A0 | 0x03FA4970 |
| audio.external-event(重载2,eventId) | `PostEventExternal(UInt32,…) → UInt32` | 0x0635A51C | 0x0569EB6C |
| audio.language-package | `AudioVFSLoader.TryLoadLanguagePck(String) → Boolean` | 0x03EB45A0 | 0x04114DF0 |
| lip.dialog.play | `DialogManager._PlayLipSyncTrack(DialogPlayTrunkActionData,Entity)` | 0x06E284BC | 0x06126FC4 |
| lip.track.path | `LipSyncUtils.GetLipSyncTrackPath(…)` 3 个重载 | — | —(逐一 SAME-SIG) |
| lip.track.load | `LipSyncUtils.TryLoadTrack(String,LipSyncTrack&) → Boolean` | 0x06C984BC | 0x05F96518 |
| lip.dialog.real-actor / actor / trunk | `DialogPlayTrunkActionData.get_realActorNameId/get_actorNameId/get_trunkId` | — | —(SAME-SIG) |

`AudioAdapter`(118 方法)、`AudioVFSLoader`(16)、`AudioBankManager`(12)、`AkSoundEnginePINVOKE`(1319)、
`AkSoundEngine`(535)、`VoiceSpeakChannelProcessor`(22)、`LipSyncUtils`(6)、`RuntimeVoiceData`(1)
方法数全部不变;`Audio.Beyond.dll` 与 `AK.Wwise.Unity.API.dll` 的 AI dump **字节数完全一致**
(121,116 / 487,464 B),即除 RVA 外逐行相同。

### 3.3 字段偏移

| 字段 | 旧 | 新 | 结论 |
|---|---|---|---|
| `VoiceContext.voiceData`(RuntimeVoiceData) | 0x48 | 0x48 | 不变;类 SIZE 0x88→0x90,新增尾部字段(§4.2) |
| `RuntimeVoiceData.speakerChannel`(String) | 0x18 | 0x18 | 整个 struct 逐字段不变 |
| `LoginDecorateUI._imgLogo` / `_targetGlow` | — | — | 无移动 |
| `LoginSceneAnimCtrl._target` | — | — | 无移动 |

## 4. 游戏侧结构性变化(与 mod 相关的部分)

### 4.1 程序集规模

| 程序集 | 类数 旧→新 | 备注 |
|---|---|---|
| Gameplay.Beyond.dll | 13199 → 13623 | 新内容(新角色等) |
| Common.Beyond.dll | 2949 → 3056 | 资源调度等新增 |
| Entry.Beyond.dll | 286 → 312 | 登录流程扩展 |
| UI.Beyond.dll | 414 → 418 | 微增 |
| Audio.Beyond.dll | 83 → 83 | 结构不变 |
| AK.Wwise.Unity.API.dll | 182 → 182 | 结构不变 |
| UnityEngine.CoreModule | 946 → 949 | Unity 小版本 |
| Assembly-CSharp.dll | 5999 → 3958 | XLua Wrap 约 2101 → 217,脚本桥接层大幅削减;mod 不经 XLua,无直接影响 |

### 4.2 语音播放链路重构(与时长修正失效同期)

- `Beyond.Gameplay.Audio.VoiceContextPool` **被删除**,新增静态类 `VoiceContextRegistry`
  (内部持 `Beyond.PoolCore.ObjectPool<VoiceContext>` 与 handle 字典,提供
  `Reset/Allocate/Recycle/TryGetVoiceContext/GetAllVoiceContextPairs`);
  `VoiceContext` 不再实现 `IReusable`,其 `TryGetVoiceContext/GetAllVoiceContextPairs/Clear/OnAllocate/OnRecycle/.cctor`
  六个静态/池化方法随之上移。**mod 契约不涉及这些成员,无直接影响。**
- `VoicePlayer`:`SeekVoice(UInt32,Int32)` 移除,新增 `SeekVoice(UInt32,Int32,Int32)、
  _FlushPendingSeek(VoiceContext)、_ClearPendingSeek(VoiceContext)`;20 个原有方法签名不变。
- `VoiceManager`:新增 `SeekResponse/PauseResponse/ResumeResponse/SeekVoice`。
- `VoiceContext`:新增 `hasPendingSeek @0x88`、`pendingSeekTimeMs @0x8c`(SIZE 0x88→0x90);
  原有字段(含 `voiceData @0x48`)全部不变。
- 判读:游戏为语音加入了**原生 Seek/暂停恢复/进度**处理族,时长与进度的内部实现路径被重做。

### 4.3 语音"按角色自定义语言"(新能力)

`VoiceI18n` 新增:

- 静态字段 `s_speakerCustomLangMap`(Dictionary<String,String>);
- `SetSpeakerCustomLang(String, AudioLang)`、`TryGetSpeakerCustomLang(String, String&)`;
- `GetCustomLanguageVoicePath(String, String&, EVoiceDevStage, String)`。

`AudioLang` 枚举本身不变(Chinese/English/Japanese/Korean 四值,新旧逐字段相同)。
配套地,语音数据文件结构变化:`voice-catalog-index.json` 显示**非中文语言从 1 个 PCK 变为 2 个**
(英 33.6MB+1.61GB、日 100.99MB+1.61GB、韩 107.05MB+1.61GB,中文仍 1 个 1.47GB),
新增的小包与该自定义语言系统对应。mod 的配音路由/时长/口型均建立在
`GetVoicePath/GetDebugVoicePath/GetLipSyncTrackPath` 等既有路径之上,签名未变,但**游戏内部对
"哪个包提供哪条语音"的解析加入了自定义语言维度**。
本系统的专题解析(证据、路径解剖、配置来源排查、mod 机会与风险)见
[VOICE_CUSTOM_LANGUAGE_SYSTEM.md](VOICE_CUSTOM_LANGUAGE_SYSTEM.md)。

### 4.4 资源系统调度化与路径哈希

- `ResourceManager` 新增 `RequestOperationSchedulingMode(EResourceOperationSchedulingMode,
  FResourceOperationPriorityWaitEntry[]) → Boolean`、`TryGetOperationSchedulingState(FResourceOperationSchedulingState&) → Boolean`,
  以及新类型 `EResourceOperationSchedulingMode{KeepCurrent, LegacyLinearScan, DeadlinePriority}`、
  `FResourceOperationSchedulingState`(含 `blockedCount/readyCount/loadingCount` 等计数)、
  `FResourceOperationPriorityWaitEntry`。资源加载从"线性扫描"演化为**带优先级/截止时间的准入调度**。
- `HashStringPathProcessor` 新增 `_IsVfsRootedPath(String)`;`InitMainPathHash/InitInitPathHash`
  及各哈希函数签名不变。
- 判读:初始化与加载的**执行顺序与完成时机**被重排,这是"main hash 回调先于 hook 安装"的
  结构性背景(确切调用点变化需反汇编验证,见 §7-Q2)。

### 4.5 登录流程

`Beyond.Login` 仅新增 `LoginPreDownloadNode`(登录期预下载协程节点,
`OnEvent/Work/_PreDownloadCoroutine/_ApplyPreDownloadCoroutine/_DownloadPreDownloadCoroutine`)。
登录场景生命周期类 `LoginSceneRoot`(4 方法)、`LoginSceneAnimCtrl`(19)、`LoginDecorateUI`(12)、
`LoginEnterGamePanel`(4)方法面不变。**未发现登录演员对象池化的元数据迹象。**

### 4.6 其他

`Beyond.I18n` 新增 `I18nServerTextManager`;`DialogManager` 大量字段调整(叙事隐藏配置、
选项回调注册等),但 `_PlayLipSyncTrack` 及 399 个既有方法签名不变;
`DialogPlayTrunkActionData` 新增字幕相关字段(`_showAtTop/_keepTopSubtitle` 及
`showAtTop/hideTopSubtitle` 属性),getter 签名不变。

## 5. 症状归因与处置建议

### 5.1 角色替换:开屏不生效,且波及游戏内模型

证据链(更新后 9/2 21:41–22:17 会话):

1. `[model-contract] feature=model/logo/login-band result=ready`、`[model-hook] installed key=…` 全绿
   → 契约层完好(§3.1)。
2. 旧构建:`Initial path hash ready` 反复出现而 **`Main path hash ready` 全天零次**,
   `LoadConfiguredAssets` 资产门永久阻塞,`[model-gate] timed out after 5 seconds`。
   中间构建诊断输出 `gate blocked mainHash=false … reason=main-hash-callback-likely-predated-hook`
   → **`InitMainPathHash` 的回调先于 hook 安装发生**(游戏资源初始化时序变化,§4.4)。
3. 含恢复逻辑的构建:`Main path hash readiness recovered after pre-hook initialization`,
   资产随后 `model and all four animation clips are resident` → **时序问题已由工作区补丁修复**。
4. 随后三次会话(22:08/22:11/22:14)一致失败于
   `[model-animation] Animator ready … human=false … avatar=0000000000000000` →
   `animation graph setup failed; original retained` → 替换体上第一个 `Animator` 非人形且无 Avatar。
   该 Animator 属于 mod 按 manifest 路径从游戏 VFS 加载的**替换用 prefab**,失败源于
   **本次更新改动了 prefab/资产内容**(IL2CPP dump 层无法验证),工作区已改为
   `GetComponentsInChildren` 多候选扫描(挑选 `human && avatar 非空` 者),待构建运行验证。
5. "波及游戏内模型表现":游戏侧**无销毁路径变化证据**——`UnityEngine.Object` 68 方法签名逐一相同
   (`Internal_CloneSingleWithParent/Destroy` 均在),登录生命周期类方法面未变,登录流程无池化迹象。
   结合时间线(失败发生于"替换体已实例化、动画准备中止"的两阶段流程),最可能是 **mod 侧失败清理不彻底**:
   - 失败后替换体实例是否被销毁、`replacement` 指针与锚点跟随是否完全解除;
   - 原演员 Renderer 是否恢复(若替换体残留且原演员 Renderer 被关闭,场景卸载时序会把影响带入后续场景);
   - `CloneWithParentHook` 是否在登录场景释放期误捕获(工作区已加 `!g_login_scene_releasing` 条件)。
   建议在修复 Animator 候选扫描后,对失败路径补一条"销毁替换体 + 恢复原演员状态 + 停锚点跟随"的
   原子回滚,并以日志确认。

处置优先级:① 构建并验证多候选 Animator 扫描;② 失败路径原子回滚;③ 若仍失败,
用更新后客户端的 `SK_actor_*`/替换 prefab 检查其 Animator/Avatar 结构(资产层验证,不需要 IDA)。

> **修复已实施(2026-09-03)**:多候选扫描确认不足以解决(prefab 的 Animator 全部
> `avatar=null`),`PrepareAnimation` 增加 **Avatar 回填回退**:全部候选都无 Avatar 时,
> 从原登录演员(`g_actor.original`)的 Animator 中找带 Avatar 者,经新增可选契约
> `unity.animator.set_avatar`(Unity 层 `Animator.set_avatar`,签名与 Unity 版本无关地稳定)
> 写入替换体候选(优先无 Avatar 者),复核 `isHuman` 后继续既有 Rebind 流程;候选与原演员的
> 日志均补充 GameObject 名称(`candidate=N/M name=…`)。原演员若同样无 Avatar 会明确记录
> `avatar copy fallback failed`。运行时验证点:`avatar copy fallback applied … human=true`
> 且替换成功;若原演员 Avatar 也为空,则需资产层提取 prefab 进一步排查。

### 5.2 配音替换:已配置角色可路由,未配置角色无声(已定案:卸载保护被 IFix 层绕过)

> **修复已实施(2026-09-03)**:新增 PINVOKE 层卸载保护
> (`voice/module.cpp`:可选契约 `wwise.package.unload-pinvoke` =
> `AkSoundEnginePINVOKE.CSharp_UnloadFilePackage`,辅助挂载期间吞卸载,平时透传并记录
> `pinvoke-unload/preserved(pinvoke)` 日志)。PINVOKE 是托管卸载的必经层,IFix 动态分发
> 无法绕过;native 构建已通过。运行时验证点:配置角色发声触发挂载后,立即播放未配置角色,
> 默认配音应恢复,日志出现 `preserved(pinvoke)`。

1. 游戏侧元数据不变(§3.2);`AudioLang` 四语言值不变。
2. **数据层嫌疑全部排除(实证)**:
   - `.becat` 归属比对:三个 catalog 共 671 条 source media,**0 条**属于未配置角色
     (manifest 的 `mediaIds` ∪ `soundSlots.mediaId` 双口径;其余约 75% 为银行图遍历引入的
     本角色媒体,HEAD manifest 中同样不存在,属既有设计而非本次回归);
   - manifest 新旧对比:HEAD-only=0、CUR-only=59(新角色),无内容丢失;
   - 语言小包:与主包媒体**完全不相交**(JP:1534 vs 30101,交集 0)且全部不在语音 manifest 内
     → 不可能经 catalog 覆盖未配置角色媒体。
3. **真实机制(运行时日志 + 函数体扫描交叉定案)**:
   - 22:16:36 首个已配置角色命中,mod 辅助挂载日语:游戏经被 hook 的
     `AkSoundEngine.LoadFilePackage` 装载 **14 个包**(3 个日语组 `F668D4EE` + 11 个基础组
     `F151B649`),全部记录为 `[voice-pck] auxiliary-load`;
   - **整个挂载过程没有一条被 hook 的卸载记录**(全程零 `preserved packageId`、
     零 `UnloadFilePackage` 命中);
   - 静态证据:`AudioVFSLoader._UnloadPcks`(0x041175B0)函数体内调用
     `IFix.WrappersManagerImpl::GetPatch` 与 `IFix.ILFixDynamicMethodWrapper::__Gen_Wrap_44`
     → **卸载逻辑被 IFix 热补丁包装**;调用链
     `TryLoadLanguagePck →(尾跳)→ _DoLoadLanguageAndHotfixPck →(+0x9B 内联)→ _UnloadPcks`;
   - 结论:**换装语言包时对默认语言媒体的卸载走 IFix 补丁/更低层调用,绕过了 mod 挂在
     `AkSoundEngine.UnloadFilePackage` 包装器上的 hook,"挂载期间吞卸载"的保护失效**
     → 默认语言(中文)媒体被真实卸载 → 未配置角色(默认语言)无声;
     已配置角色媒体是 SetMedia 常驻数据、不随包卸载 → 正常。与全部症状吻合。
4. 修复方向(按侵入度递增):
   1. 卸载保护下沉一层:hook `AkSoundEnginePINVOKE.CSharp_UnloadFilePackage`(0x050AFD60)
      —— IFix 补丁最终走 PINVOKE 则必经;
   2. 直接 hook `AudioVFSLoader._UnloadPcks`(0x041175B0,静态方法、动态可解析),
      mod 辅助挂载期间整段跳过卸载;
   3. 挂载完成后主动重挂默认语言(`EnsureLanguagePackageReady(默认语言)`)并校验包集合;
   4. 运行时验证:挂载后立即播放一个未配置角色,确认 `[voice-pck]` 无卸载逃逸。
5. 附带发现:辅助挂载实际装载了 mod 索引之外的 11 个基础包(`F151B649` 组),
   `voice-catalog-index.json` 的 7 包清单不覆盖全部语言挂载行为,后续 catalog 构建应把
   "语言挂载实际装载的包集"纳入校验。

### 5.3 时长修正失效(调用点已确认,覆盖链路断点已定位)

> **调查暂存更新（2026-09-03）**：当前 PC 的完整 Lua 覆盖面、native 消费者、XLua
> 委托旁路证据和后续诊断步骤已整理到
> `docs/VOICE_DURATION_INVESTIGATION_20260903.md`。本专题为优先发版暂时冻结；下文的
> IFix wrapper/leaf Hook 仍未产生运行时命中，不代表修复完成。

> **当前测试修复(2026-09-03)**:运行日志进一步证明 `TryGetVoiceDuration` 两个入口及
> `Beyond.Cfg.VoiceData` 的四个语言 getter 均被 IFix 补丁执行路径绕过:所有 Hook
> 安装成功,实际播放期间仍为零命中。因此不再 Hook getter,改为 Hook
> `IFix.ILFixDynamicMethodWrapper` 中与两个时长重载对应的签名包装器,并通过实例字段
> `methodId` 仅放行时长补丁,避免影响同签名的其他 IFix 方法。游戏补丁返回后,mod 用
> `TryGetVoiceData / GetVoDataFromVoId` 取得同一条 `VoiceData`,按 `speakerChannel`
> 匹配角色规则并读取目标语言的 `wavDuration*`,最后改写 `out duration`。运行时验证
> 标志为 `[voice-duration-ifix] ... data=true ... override=true`。

> **中间方案(保留作短语音回退)**:catalog 升级 v2 + `HookDuration` 直写。
> - `GenerateVoiceCatalogIndex.py`:每个 catalog 追加 `voiceSources`(voiceId /
>   AudioDialog 数字键 → source media 列表,索引已重新生成);
> - `VoiceCatalogService.cs BuildCatalog`:写 v2 catalog(`reserved` 字段复用为
>   duration 表偏移,尾部为 identity→时长 表,时长由目标 WEM 的 RIFF/fmt 块
>   `AvgBytesPerSec` 现算,取该语音全部目标媒体里最长者);report 增加
>   `CatalogVersion/DurationIdentityCount` 用于失效判定(v1 catalog 会自动重建);
> - `voice/module.cpp`:接受 v1/v2 catalog,解析尾部表进 `duration_by_identity`;
>   `HookDuration` 先按 identity 直查替换时长并直接写回(不再依赖
>   `GetCurrentLanguage` 覆盖通道),legacy 重查路径保留为回退;
>   `[voice-duration]` 日志新增 `tableHit=`、`direct=` 字段。
> - 注意:`BuildVoiceCatalog.py`(CLI 参考实现)仍输出 v1,如需走 CLI 路径需同步升级。
> 运行时验证点:日志出现 `tableHit=true direct=true result=true` 且字幕时长与
> 替换语音对齐。

1. **调用点确认(字节级函数体扫描)**:`TryGetVoiceDuration(String)` 有 7 个真实调用方,
   全部在剧情/对白链路——`DialogManager::_PlayVoice`(×2)、
   `DialogTimelineManager::PlayVoice / PlayVoiceWithVoiceId / PlayEmotionVoice`、
   `MainFlowHandle::GetDuration`、`EnvTalkHandler::_TryGetVoiceId`;
   `Int32` 重载仅 `AIBarkManager::TriggerBarkVoice` 一个调用方。
   token 顺序 String(0x600A4AA)< Int32(0x600A4AB),mod 首匹配绑定 String 重载,
   即游戏实际调用的重载。**"调用点消失/重载错位"假设否定。**
2. **真实断点(两条,均有实证)**:
   - **时长查询发生在 mod 请求作用域之外,且传入身份可能不可匹配**:
     `DialogManager::_PlayVoice` 在 +0x1E7/+0x300 两次调用时长查询,而该函数体内
     没有任何 `VoicePlayer.PlayVoice` 调用(实际发声走其他路径);
     `HookDuration` 的 `SelectRule(identity)` 按 speaker token/通配匹配——对白链路传入的
     若是 `AudioDialog` 数字键(如 "891235691"),不含 `chr_` token → 匹配失败。
   - **语言覆盖通道对时长计算无效**:`TryGetVoiceDuration(String)` 与
     `_GetVoDurationFromVoData` 函数体内**没有任何 `VoiceI18n.GetCurrentLanguage` 调用**
     (语言维度由静态字段/voData 决定),而 mod 的重查机制依赖 hook `GetCurrentLanguage`
     的线程局部 override——日志全程零 `[voice-language]` 行证实覆盖从未生效;
     即便身份匹配修好,(b) 仍会使重查结果不变。
   - 旁证:日志全程(含更新前)零 `[voice-duration]` 行。
3. 修复方向(推荐 1,最小且稳):
   1. **由 mod 自身提供替换音频时长**:catalog 构建时已持有每个条目的目标 WEM
      (WEM 头可解出真实时长),`HookDuration` 命中 catalog 条目时直接写回替换时长,
      不再重查游戏表;identity 需同时支持 `chr_` token 与 `AudioDialog` 数字键
      (manifest 的 `audioDialogKey → voiceId → speakerChannel` 映射可离线构建);
   2. 或把覆盖点前移到 `RuntimeVoiceData.FromSparkBuffer`(其体内直调 `GetCurrentLanguage`,
      E8 0x034993CD)——影响面更大,需评估;
   3. 运行时验证:先合入工作区已有的 `HookDuration` 未命中日志,确认 identity 实际形态。
4. 口型覆盖不受影响:`LipSyncUtils.GetLipSyncTrackPath` 体内两处直调 `GetCurrentLanguage`
   (E8 0x05F9639E / 0x05F964A2),线程局部 override 通道有效。

## 6. 工作区未提交适配现状(分析时快照)

- `native/modules/model/module.cpp`(+150/−37):资源门诊断(`LogResourceGateDiagnostic`,含
  `main-hash-callback-likely-predated-hook` 等原因)、main-hash 就绪恢复、Animator 多候选扫描、
  两阶段激活 + 失败回滚加固、`CloneWithParentHook` 登录释放期防护。**已验证:时序恢复生效;
  待验证:多候选扫描。**
- `native/modules/voice/module.cpp`(+9):`HookDuration` 日志扩展(未命中也输出)。
- `ui/BetterEndfield.UI/Services/VoiceCatalogService.cs`(+136):语言包多包合并(1→2 包/语言)。
- `scripts/BuildBetterEndfield.ps1`(+27):构建前校验 `character-presets.json` 与
  `action-manifest.json` 的 manifestHash 一致性。
- manifests/voice、manifests/model、`voice-catalog-index.json` 等:更新后重新生成的数据
  (新角色 typhoea/liino、7 语言包)。
- **所有 hook 契约定义(类/方法/签名)相对 HEAD 零改动**——与本文 dump 比对结论一致。

## 7. 兜底问题清单 → 核验结果(2026-09-03 IDA/字节级)

原两个问题的核验结论。方法:全量函数体调用序列扫描(按 dump 方法区间取函数体,E8/E9 调用按
偏移排序并标注已知目标)+ 运行日志交叉验证;旧版 IDB
(`legacy/better-endfield-2.3.1/local-only/state/GameAssembly-rdps2.i64`,已拷贝至临时目录)
由 IDA 9.2 重分析后可做旧版逐调用方对比与全量反编译,目前仍在后台进行,完成后可补注。

| # | 问题 | 核验结果 |
|---|---|---|
| Q1 | `TryGetVoiceDuration` 是否仍被调用/被谁调用 | **已答**:7 个调用方(剧情/对白链路,§5.3);`Int32` 重载仅 AIBarkManager;绑定正确;真实断点 = 作用域外查询 + 语言覆盖通道无效(§5.3-2) |
| Q2 | `InitMainPathHash` 调用方/时机相对 2.3.1 是否变化 | **新侧已答**:新客户端**零静态调用方**(对照组 `InitInitPathHash` 有 `GameLoginState.OnEnter`、`GameInitState._InitInitialResourceManager`、`ResourceManager.ManualInit`、`<_ReloadResourceIndexes>d__46` 直调),仅剩 `.data` 方法指针槽(0x0D608AE0)→ 经元数据/委托间接调用,与"回调时机相对 hook 安装漂移"吻合;旧版逐调用方对比待旧库重分析完成 |
| Q1'(语音) | `GetVoicePath` 是否查自定义语言表 | **已答**:函数体内无任何对三件套的调用 → 查表消费方在 IFix 补丁层(专题文档 §7) |
| Q5'(语音) | 语言包挂载/卸载链路 | **已答**:挂载链 `TryLoadLanguagePck →(尾跳)→ _DoLoadLanguageAndHotfixPck → 内联 _UnloadPcks → IFix 包装器`;挂载实测装载 14 包;卸载绕过 mod 的 `UnloadFilePackage` hook(§5.2) |

另外确认的一个普遍现象:**IFix 包装分发**——若干方法(`VoiceI18n` 路径族、
`AudioVFSLoader._UnloadPcks` 等)在静态代码中零调用方,实际经 IFix 的
`WrappersManagerImpl::GetPatch` + `ILFixDynamicMethodWrapper` 分发。对 mod 的含义:
hook 静态入口仍能拦截普通调用,但补丁代码发起的调用可能直达 PINVOKE/其他层,
**hook 点应尽量选最低稳定层**。

## 8. 附录

### A. 结论所依据的方法

以 `IL2CPP_Dump_AI` 的 `CLASS: … END_CLASS` 块为解析单元,比对
`SIZE`、`FIELDS`(名称/类型/偏移)、`METHODS`(以"返回类型 + 方法名 + 顶层逗号分割的参数类型序列"
为键,剥离 RVA/token 与参数名),对 9 个程序集的目标类做双向 diff;另以 SHA1 抽样验证
音频层两程序集"仅 RVA 平移"的判断。分析用一次性脚本已随收尾清理,方法可按本节复现。

### B. 关键类方法面差异摘要(旧 → 新)

| 类 | 变化 |
|---|---|
| VoicePlayer | +`SeekVoice(UInt32,Int32,Int32)` −`SeekVoice(UInt32,Int32)`;+`_FlushPendingSeek/_ClearPendingSeek`;其余 19 方法不变 |
| VoiceContext | −6 个池化/静态方法(移入 Registry);+`hasPendingSeek/pendingSeekTimeMs`;`voiceData @0x48` 不变 |
| VoiceContextRegistry | 新增(Allocate/Recycle/Reset/TryGetVoiceContext/GetAllVoiceContextPairs) |
| VoiceContextPool | 删除 |
| VoiceManager | +`SeekResponse/PauseResponse/ResumeResponse/SeekVoice`;其余 44 方法不变 |
| VoiceI18n | +`s_speakerCustomLangMap`、`SetSpeakerCustomLang/TryGetSpeakerCustomLang/GetCustomLanguageVoicePath`;其余 6 方法不变 |
| HashStringPathProcessor | +`_IsVfsRootedPath(String)`;`InitMainPathHash/InitInitPathHash` 不变 |
| ResourceManager | +`RequestOperationSchedulingMode/TryGetOperationSchedulingState` 及 3 个新类型;其余 57 方法不变 |
| UIMaterialAnimation | +`_GetTargetMaterial()`;`LateTick` 不变 |
| DialogPlayTrunkActionData | +`_showAtTop/_keepTopSubtitle` 字段与 `showAtTop/hideTopSubtitle` 属性;getter 签名不变 |
| DialogManager | SIZE 0x568→0x598;+17 个叙事/选项相关方法;`_PlayLipSyncTrack` 与 399 个方法签名不变 |
| AudioAdapter / AudioVFSLoader / AkSoundEnginePINVOKE / AkSoundEngine / VoiceSpeakChannelProcessor / LipSyncUtils / RuntimeVoiceData | 方法数与签名完全不变(仅 RVA 平移) |
| UnityEngine.Object / Animator / AnimationClip / UI.Graphic | 方法数与 mod 所用签名不变 |
