# Better Endfield Runtime Interfaces

本文记录重构后的进程内接口协议。运行时不保存客户端地址、文件哈希白名单或硬编码字段偏移。

## Host 与模块

Host 导出模块发现所需的 `BetterEndfield_GetModuleApiV1`。模块清单使用 `modules/*.module.ini`，包含模块 ID、DLL、ABI、契约组及 `requires` 程序集列表。Host 等待所需 IL2CPP 程序集注册后，按清单文件名的稳定顺序加载 DLL，先调用 `initialize`，再推送对应配置；依赖未就绪会在 90 秒窗口内重试，ABI 或契约拒绝则不重复加载。清单的 `enabled=false` 会禁用模块发现，而模块配置节中的 `enabled` 只控制该 DLL 是否激活 Hook，因此可在运行时热启用或停用。

Host 是唯一的 HookBroker 所有者，负责 MinHook 初始化、目标冲突检查、启用、禁用和移除。运行中只停用模块行为，不卸载 DLL；模块卸载在游戏进程结束后完成。

## 动态 IL2CPP 解析

Host 使用 `GameAssembly.dll` 的 IL2CPP 导出解析：

- 域和程序集：`il2cpp_domain_get`、`il2cpp_domain_get_assemblies`、`il2cpp_assembly_get_image`、`il2cpp_image_get_name`
- 类与方法：`il2cpp_class_from_name`、`il2cpp_class_get_methods`、`il2cpp_method_get_name`、`il2cpp_method_get_param_count`、`il2cpp_method_get_param`、`il2cpp_method_get_return_type`。Unity 2021 IL2CPP 的 `MethodInfo` ABI 以 `methodPointer` 开头；Host 只接受该入口位于当前 `GameAssembly.dll` 可执行 PE 节的结果，不扫描其他字段，也不尝试客户端地址。
- 字段：`il2cpp_class_get_field_from_name`、`il2cpp_field_get_offset`、`il2cpp_field_get_type`
- 线程与字符串：`il2cpp_thread_attach`、`il2cpp_thread_detach`、`il2cpp_string_length`、`il2cpp_string_chars`

每个模块提供程序集、命名空间、类型、方法或字段描述符。Host 工作线程在使用元数据前附加到 IL2CPP 域，退出时解除附加。

## 模型模块

模型模块动态验证并使用以下契约：

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

模型及动作路径来自当前 `action-manifest.json` 和 UI 写入的配置，不编译角色资源哈希白名单，也不依赖可执行文件身份。

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
剧情、时长和口型路由通过线程局部语言覆盖生效，不修改 Wwise 全局语言；时长覆盖独立于剧情语音开关。
Wwise Media 只使用本机生成的 `BEVCAT01` Catalog；Catalog 中的 WEM 在 `SetMedia` 成功前保持驻留，清理状态确认后才释放。

## 外部 Catalog

`scripts/BuildVoiceCatalog.py` 从当前 VFS 的 PCK 和仓库资源清单生成角色级 Catalog。格式为：

- `VoiceCatalogHeaderV1`：格式版本、目标语言、条目数和数据区位置。
- `VoiceCatalogEntryV1`：源 Media ID、目标 Media ID、驻留数据偏移和长度。

Catalog 不包含 `GameAssembly.dll` 身份条件。B 服只要提供可解析的 PCK/BNK/HIRC 和 `AudioDialog`，即可重新生成自己的 Catalog。

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
