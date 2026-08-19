# Better Endfield Runtime Interfaces

本文记录重构后的进程内接口协议。运行时不保存客户端地址、文件哈希白名单或硬编码字段偏移。

## Host 与模块

Host 导出模块发现所需的 `BetterEndfield_GetModuleApiV1`。模块清单使用 `modules/*.module.ini`，包含模块 ID、DLL、ABI、契约组及 `requires` 程序集列表。Host 等待所需 IL2CPP 程序集注册后，按清单文件名的稳定顺序加载 DLL，先调用 `initialize`，再推送对应配置；依赖未就绪会在 90 秒窗口内重试，ABI 或契约拒绝则不重复加载。模块配置节中的 `enabled=false` 会在本次进程启动时跳过该 DLL；已经加载的模块仍会收到后续配置变更并可停用自身行为，但从关闭改为开启需要下一次注入。

Host 是唯一的 HookBroker 所有者，负责 MinHook 初始化、目标冲突检查、启用、禁用和移除。运行中只停用模块行为，不卸载 DLL；模块卸载在游戏进程结束后完成。

## 动态 IL2CPP 解析

Host 使用 `GameAssembly.dll` 的 IL2CPP 导出解析：

- 域和程序集：`il2cpp_domain_get`、`il2cpp_domain_get_assemblies`、`il2cpp_assembly_get_image`、`il2cpp_image_get_name`
- 类与方法：`il2cpp_class_from_name`、`il2cpp_class_get_methods`、`il2cpp_method_get_name`、`il2cpp_method_get_param_count`、`il2cpp_method_get_param`、`il2cpp_method_get_return_type`。Unity 2021 IL2CPP 的 `MethodInfo` ABI 以 `methodPointer` 开头；Host 只接受该入口位于当前 `GameAssembly.dll` 可执行 PE 节的结果，不扫描其他字段，也不尝试客户端地址。
- 嵌套类：当描述符使用 `Outer.Inner` 时，Host 通过 `il2cpp_class_get_nested_types` 和 `il2cpp_class_get_name` 逐级解析嵌套类型；缺少这两个导出只影响使用嵌套类型的模块。
- 字段：`il2cpp_class_get_field_from_name`、`il2cpp_field_get_offset`、`il2cpp_field_get_type`
- 线程与字符串：`il2cpp_thread_attach`、`il2cpp_thread_detach`、`il2cpp_string_length`、`il2cpp_string_chars`

每个模块提供程序集、命名空间、类型、方法或字段描述符。Host 工作线程在使用元数据前附加到 IL2CPP 域，退出时解除附加。

## 开屏模块

`BetterEndfield.Model.dll` 在同一登录场景生命周期中提供模型替换、Logo 与登录色带主题。各能力组独立解析、独立安装 Hook；任一视觉契约缺失只停用对应功能。

模型替换动态验证并使用以下契约：

- `Entry.Beyond.dll / Beyond.Login / LoginSceneRoot.OnBindToManager`
- `Entry.Beyond.dll / Beyond.Login / LoginSceneAnimCtrl`
- `Common.Beyond.dll / Beyond.Resource / HashStringPathProcessor.InitMainPathHash`
- `Common.Beyond.dll / Beyond.Resource / I18NAssetLoader.Load`
- Unity `Object`、`GameObject`、`Transform`、`Renderer`、`Animator` 和 `AnimationClip`
- Unity `PlayableGraph`、`AnimationClipPlayable`、`AnimationMixerPlayable` 和 `AnimationPlayableOutput`
- `mscorlib / System.Array`，用于遍历原演员 Renderer 与 Animator

模块监听登录演员的克隆实例，并只接受原 Prefab 名称以 `SK_actor_` 开头的对象。如果 Host 安装时演员已经创建，则按 250 ms 周期使用 `GameObject.Find` 精确查找男女演员名称；不执行全局对象枚举。`LoginSceneAnimCtrl._target` 可能指向相机或登录场景根节点，因此不得作为演员回退来源。Initial 与 Main 路径哈希均就绪后，模块使用配置中的当前 VFS 路径哈希加载完整角色 Prefab 和四段 Humanoid `AnimationClip`，在原演员同级实例化替换体并同步本地变换、原始缩放与 Layer。

原演员根对象和 Animator 始终保持活动，只关闭 Renderer，并强制 Animator 使用 `AlwaysAnimate`。替换体与原演员的 `Bip001_Pelvis` 在创建后执行一次 XYZ 对齐，此后每帧精确跟随 XZ，并对 Y 使用 0.45 秒低通响应。初始 `sitLoop` 使用 `sitToWalk` Clip 在 `forward_lean_sample` 处以零速度定格；`sitToWalk` 从同一时刻继续，并在 `turn_duration` 内把 `start_yaw` 平滑插值到零。每个阶段动态创建 PlayableGraph，将 AnimationPlayableOutput 绑定替换体 Animator，使用非缩放时间并在 Play 后立即 `Evaluate(0)`。原生循环保持同一图连续播放；启用 `use_crossfade` 时，最终阶段使用双 AnimationClipPlayable 与 AnimationMixerPlayable 在配置区间交叉淡化。

首次 A1 请求发生而 Main 资源尚未就绪时，模块最多暂停该登录控制器 Tick 5 秒；替换体准备完成后调用原始 `_ResetToA1` 统一时间原点。只有 Prefab、四段有效动作、Animator、首个 PlayableGraph、原 Renderer 隐藏和初次锚点准备全部完成后才进入替换状态；任一步失败都会保留或恢复原演员。

模型及动作路径来自当前 `manifests/model/action-manifest.json` 和 UI 写入的配置，不编译角色资源哈希白名单，也不依赖可执行文件身份。

Logo 主题使用以下强类型契约：

- `Entry.Beyond.dll / Beyond.Login / LoginDecorateUI.Tick` 与 `OnRelease`
- `LoginDecorateUI._imgLogo` 与 `_targetGlow`
- `Entry.Beyond.dll / Beyond.Login / LoginEnterGamePanel.OnValueChanged`
- Unity `GameObject.Find("GameLogoRaw")`
- Unity UI `Graphic.get_color/set_color` 与 `GetComponentsInChildren`

模块在原 `Tick` 返回后覆盖 `_imgLogo`、`_targetGlow` 两个精确子树以及登录主界面独立 `GameLogoRaw` 的 `Graphic.color` RGB，并保留动画当前 Alpha。`GameLogoRaw` 是世界层级快照中确认的 `UIRawImage + UIMaterialAnimation` 对象，不属于 `LoginDecorateUI` 的两个字段子树；运行时按 500 ms 重试精确名称查找，不枚举全局 UI。

登录界面的长条色带位于 `EnterGamePanel/MiddlePanel/Line` 的 UGUI 子树。`LoginEnterGamePanel.OnValueChanged` 返回后，模块按面板实例缓存该子树的 `Graphic`，并以精确层级、`login_deco_line*`/`login_deco_glitch*` Sprite/RawTexture 以及原始黄色 RGB 共同识别主题目标。这会覆盖入场特效、最终静态 `LineLeft` 和两侧 `LineDecoLeft`/`LineDecoRight` 小区块。主题处理为每个目标复制当前 `Material`，在副本的 `_Color`、`_TintColor`、`_BaseColor` 或 `_GlowColor` 属性上写入主题色，再把副本只赋给当前 `Graphic`；原 Sprite、纹理、UV、RectTransform、Shader 和层级保持不变，不创建白色 UI 四边形，也不修改共享材质。`Graphic.color` 仅把 RGB 归一为白色并保留游戏当前 Alpha，避免原黄色乘色污染材质主题色。模块在 `LoginDecorateUI.Tick` 和 `UIMaterialAnimation.LateTick` 返回后重新确认材质与颜色，因此游戏的最终动画状态不会覆盖主题色。重复的 `OnValueChanged` 不会重新枚举同一面板；热停用或界面释放时恢复原材质与原 RGB，并销毁运行时材质副本。

## 语音模块

语音模块动态验证并挂接：

- `Gameplay.Beyond.dll / Beyond.Gameplay.Audio / VoicePlayer.PlayVoice`
- `Gameplay.Beyond.dll / Beyond.Gameplay.Audio / VoiceSpeakChannelProcessor._PlayVoice`
- `Gameplay.Beyond.dll / Beyond.Gameplay.Audio / VoicePlayer._PlayEvent`
- `Gameplay.Beyond.dll / Beyond.Gameplay.Audio / VoiceManager._SpeakNarrative`
- `Gameplay.Beyond.dll / Beyond.Gameplay.Audio / VoiceUtils.TryGetVoiceDuration`
- `Gameplay.Beyond.dll / Beyond.Gameplay.Audio / VoiceI18n.GetCurrentLanguage`
- `AK.Wwise.Unity.API.dll / AkSoundEnginePINVOKE.CSharp_SetMedia`
- `AK.Wwise.Unity.API.dll / AkSoundEnginePINVOKE.CSharp_UnsetMedia`

可选契约还包括 `Beyond.Gameplay.Core.DialogManager._PlayLipSyncTrack`、
`Beyond.Gameplay.View.LipSync.LipSyncUtils.GetLipSyncTrackPath`、
`Beyond.Gameplay.View.LipSync.LipSyncUtils.TryLoadTrack` 及对话动作的角色标识 getter。
剧情请求在 `VoicePlayer.PlayVoice` 调用作用域内保存已识别的角色和目标语言，外部语音提交按
“请求作用域、外部源路径、Wwise Event”顺序匹配规则。剧情文件路径不包含角色名时，可由
`vo_narrating_<角色>_*` Event 或上游 `speakerChannel` 继续完成路由；作用域支持嵌套并在原调用
返回后恢复。剧情 WEM 成功重定向后，模块还会按对白 ID 挂起一次目标口型语言；
`LipSyncUtils.TryLoadTrack` 只有在收到完全相同的对白 ID 时才消费该状态，并在目标 Track
不可用时重试游戏原语言。配置代数变化会使尚未消费的状态失效。时长和口型通过各自的线程
局部语言覆盖生效，均不修改 Wwise 全局语言；时长覆盖独立于剧情语音开关。
Wwise Media 只使用本机生成的 `BEVCAT01` Catalog；Catalog 中的 WEM 在 `SetMedia` 成功前保持驻留，清理状态确认后才释放。

## 外部 Catalog

`scripts/BuildVoiceCatalog.py` 从当前 VFS 的 PCK 和仓库资源清单生成角色级 Catalog。格式为：

- `VoiceCatalogHeaderV1`：格式版本、目标语言、条目数和数据区位置。
- `VoiceCatalogEntryV1`：源 Media ID、目标 Media ID、驻留数据偏移和长度。

Catalog 不包含 `GameAssembly.dll` 身份条件。B 服只要提供可解析的 PCK/BNK/HIRC 和 `AudioDialog`，即可重新生成自己的 Catalog。

## 战斗数据模块

`BetterEndfield.CombatStats.dll` 是第四个独立模块，默认关闭。它动态解析：

- `Gameplay.Beyond.dll / Beyond.Gameplay.Core.BattleManager.BattleRecorder.DamageDetail.Init`
- `Gameplay.Beyond.dll / Beyond.Gameplay.Core.DamageTextProcessor.ProcessDamagePackDataInternal`
- `Gameplay.Beyond.dll / Beyond.Gameplay.Core.BattleManager.Tick`

`DamageDetail.Init` 在原函数返回后读取 `attackerId`、`originSkillId`、`damageValue`、`hpDelta`、
`damageType`、`damageDecorateMask` 和 `isCritical` 字段。事件先进入有界队列，再由模块线程汇总；
队列满时只丢弃统计事件，不影响游戏线程。F7/F8（可配置）分别开始和结束会话，结果写入
`%LocalAppData%\\BetterEndfield\\combat-sessions\\combat-*.json`。

隐藏伤害数字只在开关开启时跳过 `DamageTextProcessor`，不触碰 `DamagePackData`、BattleRecorder、
生命值或韧性处理。若任一契约在 B 服缺失，模块只禁用对应能力，其他三个模块继续运行。

## 音乐模块

音乐模块动态验证并挂接：

- `Gameplay.Beyond.dll / Beyond.Gameplay.Audio / AudioMusicSystem.PostMusicEvent`
- `AudioMusicSystem._StartMusicWithEvent`、`_StopMusicByPlayingId`
- `AudioMusicSystem.PauseMusic`、`ResumeMusic`、`StopMusic`
- `AudioMusicSystem.OnTimelinePause`、`OnTimelineResume`
- `UnityEngine.CoreModule.dll / UnityEngine / UnitySynchronizationContext.Exec`
- `AK.Wwise.Unity.API.dll / AkAudioInputManager.PostAudioInputEvent`
- `AkAudioInputManager.InternalAudioFormatDelegate`、`InternalAudioSamplesDelegate`
- `AkSoundEngine.LoadBankMemoryCopy`、`RegisterGameObj`、`ExecuteActionOnPlayingID`

`UnitySynchronizationContext.Exec` 只作为 Unity 主线程命令泵。共享内存连接、心跳、读取、声道转换和重采样全部在模块自己的工作线程执行；Wwise 实时回调只读取预分配的本地 SPSC 缓冲，不分配、不加锁、不记录日志，也不调用 OmniPcmShared。

OmniPcmShared 在加载后先通过 `OmniPcm_GetAbiVersion` 和
`OmniPcm_GetAbiInfo` 验证 ABI 主版本 `2`、共享协议 `2` 与交错
`float32`。实例使用 `OmniPcm_OpenInstanceUtf8`，流状态只读取带大小标记的
`OmniPcm_GetSnapshotV2`；该检查失败时不会连接共享内存，也不会暂停原生
Playing ID。

模块内嵌 188 字节、四 HIRC 对象的 Audio Input Bank，不发布独立 `.bnk`。正式版继续使用已经在当前客户端验证的 `PostAudioInputEvent(..., null, null)` 路径，并只为自己的 Playing ID 截获 Internal 回调；其他 Playing ID 无条件转发给游戏原实现。构造纯 C++ IL2CPP 托管 Delegate 不属于当前 ABI。

原游戏登录、主界面和游戏内 Playing ID 通过字段元数据动态取得。自定义 Event 路由到同一 Music Bus，因此模块只对选中范围的原生 Playing ID 执行 Pause/Resume，不静音总线。后端、流、预缓冲、格式回调和首个采样回调全部健康后才暂停原音乐；故障时按配置恢复。Audio Input 初始化失败按 2 秒退避重试；可闻游标在本地环形缓冲之外再扣除 100 ms 输出余量。

Audio Input Event 是进程期持久 Source。OmniMix 暂停、停止、流切换或故障时，模块通过
原子 PCM 门控让回调补零且不消费缓冲，不对持久 Event 执行 Pause/Resume；真正的 Source
故障使用最近一次 Wwise 样本回调时间判定。实例声明 `QueueManagement`、`Seek` 能力，`stream_id`、格式
generation 或 Seek generation 变化都会先清空旧流再绑定新流。策略只在状态边沿记录
`[music.policy]`，可区分正常接管、场景离开、传输失效和后端会话丢失。

图形后端不属于音乐模块接口。2026-08-17 的联调中曾偶发一次呈现线程崩溃，但后续测试
无法稳定复现，也没有证据表明它由音乐模块、OmniMix 或特定图形后端触发。因此音乐模块
不检测或修改渲染 API；UI 的自定义启动参数仅作为通用游戏启动能力保留。

## 加载适配器

`BetterEndfield.Injector.exe` 启动目标程序，并通过普通映像 Bootstrap 在启动阶段加载 Host；Host 根据自身 DLL 路径找到软件根目录。注入器支持在 `--` 后接收并转发游戏参数。

`payloads/xinput1_4.dll` 是唯一会部署到游戏目录的代理。它用 PE forwarder 转发 Windows XInput 1.4 API，仅从 `%LocalAppData%\BetterEndfield\BetterEndfield.ini` 的 `[Loader] install_root` 定位 Host，不包含旧代理配置回退。

### XInput 代理时序

`xinput1_4.dll` 的 `DllMain` 只禁用线程通知并创建 Worker，不在 Loader Lock 中加载 Host 或模块。Worker 使用以下时序：

1. 写入代理加载标记和状态文件，便于诊断 Windows 是否选择了本地 XInput DLL。
2. 读取 `[Loader] load_host` 和 `install_root`，验证软件目录中的 Host 存在。
3. 等待 `GameAssembly.dll` 出现，并取得 `il2cpp_domain_get`、`il2cpp_thread_attach` 和 `il2cpp_thread_detach`。
4. 等待 Domain 就绪，在 Worker 线程附加 IL2CPP 后加载 `runtime\BetterEndfield.Host.dll`，随后解除附加。

UI 安装 XInput 时会同时写入 `BetterEndfield.xinput.install.json`。更新和卸载要求目标 DLL 与当前随包文件哈希一致，或与归属记录中的哈希一致；未知 `xinput1_4.dll` 一律不覆盖、不删除。软件安装器卸载前会调用同一清理流程。

## 失败规则

动态方法、字段、Hook 目标或 Catalog 校验失败时，模块进入 `contract-mismatch` 或 `failed` 状态并记录原因。Host 不尝试其他地址、过期配置、过期资源映射或未知代理链。
