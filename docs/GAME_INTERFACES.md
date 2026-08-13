# Better Endfield 游戏内接口参考

本文记录 Better Endfield 当前实际使用的游戏内接口、解析方式、用途、写入行为和失效策略。它以仓库当前实现为准，用于游戏版本更新、官服/B 服兼容性判断和 Hook 冲突排查。

实现入口：

- 模型、登录场景与动画：`tools/IL2CPP-Dumper-src/Dump/src/model_replacer.cxx`
- 配音、Wwise 与口型：`tools/IL2CPP-Dumper-src/Dump/src/voice_language_router.cxx`
- IL2CPP 导出解析：`tools/IL2CPP-Dumper-src/Dump/src/il2cpp_api.cxx`
- 资源映射生成：`scripts/GenerateResourceManifests.py`
- 紧凑语音表生成：`scripts/CompileVoiceRuntimeMap.py`

## 兼容性基线

| 项目 | 当前值 | 说明 |
| --- | --- | --- |
| Unity | 2021.3.34 | Windows x64、IL2CPP |
| 官服 `GameAssembly.dll` | `0C5573679BC6DEC2D068A14335466DB7CCF20AF9BAE2B983FB9D45677D80FFCE` | 当前实现与 v10 原生 Wwise 容器路线已实测 |
| B 服 `GameAssembly.dll` | `22ED05932868855FCAA80D479BEA1320B9F977C50DF4C6498F85BD164B4BD727` | 仅记录用户提供的哈希，尚未取得样本并验证固定 RVA/签名 |
| VFS manifest | `5f521eb8-5202-dcdf-2412-02d992d0d771` | 当前资源清单输入 |
| `AudioDialog.bytes` | `533763086BE069FC42CF7E48669601A921506E0A8F09A8FDC6A22879C8827E82` | 语音 ID、语言时长与角色归属输入 |
| 语音运行时表 | `9C08874684A0D830E9DD86A0D30199EAF87CD385E370E064877D4B973A2F3051` | 格式 v2，110,160 bytes |

固定 RVA 以 `GameAssembly.dll` 的加载基址为基准，即运行时地址为 `moduleBase + RVA`。ASLR 不影响该计算，但任何会改变代码布局的客户端构建都必须重新验证。

资源映射与函数入口是两套兼容性边界：只要 VFS、BNK/HIRC 和 PCK 内容一致，官服与 B 服可以共用资源清单；`GameAssembly.dll` 不同仍可能导致固定 RVA 或入口字节签名不同。

## 接口解析层级

| 层级 | 解析方式 | 版本稳定性 | 当前策略 |
| --- | --- | --- | --- |
| A | IL2CPP 导出与元数据按程序集、命名空间、类、方法名解析 | 较高 | 优先用于 Unity API、Wwise P/Invoke 和口型方法 |
| B | `GameAssembly.dll + RVA`，并校验入口字节签名 | 中等 | 用于游戏业务入口、资源系统和部分 Unity injected API |
| C | VFS manifest、`AudioDialog`、BNK/HIRC、PCK 索引离线生成 | 与资源版本绑定 | 编译为紧凑运行时表，不读取 `GameAssembly.dll` |
| D | 运行时对象字段偏移 | 较低 | 仅在已验证的语音请求上下文中读取，入口签名失败即停用 |

所有核心固定入口在安装 Hook 前进行字节签名校验。核心组不匹配时整组停用；诊断、剧情或口型等可选组不匹配时只降级对应子功能。

## IL2CPP 基础接口

运行时通过 `GetProcAddress(GameAssembly.dll, ...)` 解析 IL2CPP 导出。主要使用：

- 域和程序集：`il2cpp_domain_get`、`il2cpp_domain_get_assemblies`、`il2cpp_assembly_get_image`、`il2cpp_image_get_name`
- 类型和方法：`il2cpp_class_from_name`、`il2cpp_class_get_method_from_name`、`il2cpp_method_get_pointer`
- 调用和对象：`il2cpp_runtime_invoke`、`il2cpp_object_new`、`il2cpp_object_unbox`、`il2cpp_array_new`
- 字符串：`il2cpp_string_new`、`il2cpp_string_length`、`il2cpp_string_chars`
- 线程：`il2cpp_thread_current`、`il2cpp_thread_attach`、`il2cpp_thread_detach`
- 字段和类型对象：`il2cpp_class_get_field_from_name`、`il2cpp_field_get_value`、`il2cpp_class_get_type`、`il2cpp_type_get_object`
- GC 句柄：`il2cpp_gchandle_new`、`il2cpp_gchandle_free`

部分客户端不导出 `il2cpp_method_get_pointer`。此时动态方法入口会在验证 `MethodInfo` 可读且首指针位于 `GameAssembly.dll` 可执行区后，使用 `MethodInfo[0]` 作为兼容回退。

## 登录场景和资源加载

### 资源系统固定入口

| 实现常量 | RVA | 用途 | 类型 |
| --- | ---: | --- | --- |
| `RVA_CACHED_LOAD_GAME_OBJECT` | `0x04701AF0` | 观察/捕获缓存的 `GameObject` 加载 | Hook |
| `RVA_I18N_LOAD_GAME_OBJECT` | `0x0304D350` | 捕获登录演员的 Initial VFS 加载上下文 | Hook |
| `RVA_I18N_RESOLVE_PATH_HASH` | `0x0304DB50` | 路径哈希分类诊断 | 可选 Hook |
| `RVA_INIT_INITIAL_PATH_HASH` | `0x0311B400` | 观察 Initial 路径哈希初始化 | Hook |
| `RVA_INIT_MAIN_PATH_HASH` | `0x04994340` | Main VFS 可用时触发延迟角色加载 | Hook |
| `RVA_RESOURCE_MANAGER_IS_INITIALIZED` | `0x036D47E0` | 查询资源管理器初始化状态 | 读取 |
| `RVA_RESOURCE_MANAGER_IS_ASYNC_INITING` | `0x04CDB190` | 查询异步初始化状态 | 读取 |
| `RVA_RESOURCE_MANAGER_USING_BUNDLE` | `0x03C8F590` | 查询是否使用 Bundle | 读取 |
| `RVA_RESOURCE_MANAGER_CHECK_STRING` | `0x0304B570` | 按字符串检查资源是否存在 | 读取 |
| `RVA_RESOURCE_MANAGER_CHECK_HASH` | `0x030492B0` | 按包装哈希检查资源 | 读取 |
| `RVA_RESOURCE_MANAGER_RAW_CHECK_HASH` | `0x0304A850` | 按原始 64-bit 哈希检查资源 | 读取 |
| `RVA_BUNDLE_LOAD_UNTRACKED_HASH` | `0x03393F90` | 以 Main 分类加载未跟踪资源句柄 | 调用 |
| `RVA_TRY_GET_ASSET_INFO_HASH` | `0x02FCE680` | 解析资源信息 | 诊断 Hook |
| `RVA_LOAD_ASSET_INTERNAL_HASH` | `0x02FCE2B0` | 观察 Bundle 内部加载 | 诊断 Hook |
| `RVA_BUNDLE_LOADER_LOAD_ASSET` | `0x02FCEE80` | 观察 Bundle Loader | 诊断 Hook |
| `RVA_UNTRACKED_HANDLE_GATE` | `0x02EFB620` | 验证未跟踪句柄外层 gate | 诊断 Hook |
| `RVA_UNTRACKED_HANDLE_IS_VALID` | `0x068AF788` | 验证未跟踪句柄 | 读取 |
| `RVA_ASSET_PROXY_HAS_ERROR` | `0x02FCDBE0` | 查询 AssetProxy 错误 | 读取 |
| `RVA_ASSET_PROXY_FINISH_ERROR` | `0x068C2D48` | 捕获资源失败原因 | 诊断 Hook |
| `RVA_BUNDLE_PROXY_LOAD_SYNC` | `0x02F67FE0` | 捕获目标及依赖 Bundle 同步加载 | 诊断 Hook |
| `RVA_BUNDLE_PROXY_GET_FILE_PATH` | `0x067DD024` | 获取 Bundle 文件路径 | 诊断 Hook |
| `RVA_BUNDLE_PROXY_LOAD_FILE` | `0x067DD138` | 观察 `AssetBundle.LoadFromFile` | 诊断 Hook |
| `RVA_ASSET_PROXY_HANDLE_GET` | `0x0360A4D0` | 从有效句柄取得 Prefab/AnimationClip | 调用 |
| `RVA_RESOURCE_MANAGER_LOAD_HASH_TYPE` | `0x03393BD0` | 按哈希和类型加载资源 | 调用 |

Main VFS 在登录流程早期尚未可用，因此不能把 `SK_actor_female.prefab` 的 Initial 加载直接改成 Main 角色 Prefab。当前流程保留原演员加载，捕获登录线程与父 `Transform`，等 `InitMainPathHash` 完成后再加载角色 PostModel 和动画。

### 登录演员生命周期

| 实现常量 | RVA | 用途 |
| --- | ---: | --- |
| `RVA_LOGIN_MANAGER_RELEASE_SCENE` | `0x0380E1E0` | 场景释放前销毁注入的 PlayableGraph 和替换实例 |
| `RVA_LOGIN_SCENE_ROOT_ON_BIND` | `0x0380F010` | 建立登录绑定作用域，捕获演员实例化 |
| `RVA_INSTANTIATE_GAME_OBJECT_PARENT` | `0x03E03C80` | 捕获 `Instantiate<GameObject>(original, parent, false)` 及原实例 |
| `RVA_LOGIN_ANIM_CHANGE_STATE` | `0x03408830` | 同步替换角色的登录动画阶段 |
| `RVA_LOGIN_ANIM_RESET_A1` | `0x03D4F930` | 同步回到 A1/坐姿阶段 |
| `RVA_LOGIN_ANIM_PLAY_A1S` | `0x03D4FD90` | 同步坐姿特殊动作 |
| `RVA_LOGIN_ANIM_PLAY_A1A2` | `0x03D4FB60` | 同步坐姿到行走过渡 |
| `RVA_LOGIN_ANIM_TICK` | `0x02F446B0` | 替换资源未就绪时短暂保持原流程 |
| `RVA_LOGIN_ANIM_RELEASE` | `0x04994870` | 登录动画控制器释放的幂等清理回退 |

替换成功后不会销毁原登录根节点：只隐藏原 Renderer，保留 `LoginSceneAnimCtrl` 和状态推进，使替换模型继续收到原登录阶段信号。

## Unity 对象、骨架和动画

### 通过 IL2CPP 元数据解析的 Unity API

涉及程序集：`UnityEngine.CoreModule.dll`、`UnityEngine.AnimationModule.dll`、`UnityEngine.GameObjectModule.dll`、`UnityEngine.dll`。

- `UnityEngine.Object`：`get_name`、`Destroy`
- `UnityEngine.Resources`：`FindObjectsOfTypeAll`
- `UnityEngine.GameObject`：`Find`、`get_transform`、`get_layer`、`set_layer`、`SetActive`、`GetComponentsInChildren`
- `UnityEngine.Component`：`get_gameObject`
- `UnityEngine.Transform`：父对象、世界/局部位置、局部旋转、局部缩放的 getter/setter
- `UnityEngine.Renderer`：`set_enabled`
- `UnityEngine.SkinnedMeshRenderer`：`get_bones`、`set_bones`、`get_rootBone`、`set_rootBone`
- `UnityEngine.Behaviour`：启用状态和 `isActiveAndEnabled`
- `UnityEngine.Animator`：Avatar、初始化、人形状态、Playable 绑定、CullingMode、UpdateMode、`Rebind(bool)`
- `UnityEngine.AnimationClip`：长度、空 Clip、Legacy、HumanMotion 属性

早期“把新网格绑定到原骨架”实验仍保留接口，但完整替换默认使用角色自身骨架。骨架重绑定只有在映射覆盖率和唯一骨骼数达到阈值时才允许执行，失败时保留原演员。

### Playables 固定入口

| 接口组 | RVA |
| --- | --- |
| `PlayableGraph.Create_Injected` | `0x0421E8F0` |
| `AnimationClipPlayable.CreateHandle` | `0x04126100` |
| `AnimationClipPlayable` 循环覆盖 | `0x0539814C`、`0x05398154` |
| `AnimationMixerPlayable.CreateHandleInternal_Injected` | `0x03E0F960` |
| 输入数量、连接、权重 | `0x03E0F910`、`0x03F2A1D0`、`0x038DE2D0` |
| `AnimationPlayableOutput` 创建、绑定 Animator、设置 Source | `0x03FC8C80`、`0x043C1180`、`0x03E11450` |
| WrapMode、Speed、Time | `0x0B3F4498`、`0x03D24A90`、`0x03B28180`、`0x03F1B500` |
| UpdateMode、Play、Evaluate、Destroy | `0x04398AA0`、`0x043DA5F0`、`0x03DAE950`、`0x0450B9D0` |

动画序列由四阶段组成：坐姿循环、坐姿特殊、起身、最终动作。替换角色使用自己的 Animator 和 Clip；原登录控制器只提供阶段信号。最终循环可使用原生循环，或通过双 Playable Mixer 交叉淡化。

此外会动态解析 `Beyond.NPC.AnimationGraphScript.NPCCPUAnimationGraphScript_SimpleOneState` 的 `.ctor`、`Init`、`SetMainAnimClip`、`SetMainClipSpeed`、`Play` 和原游戏拼写 `Destory`，用于兼容游戏内 NPC 单状态动画图路径。

## 配音请求和外部媒体

### 核心业务入口

| 接口 | RVA | 用途 |
| --- | ---: | --- |
| `VoiceManager.Speak(string)` | `0x03ABC4C0` | 建立字符串语音请求上下文 |
| `VoiceManager._Speak` | `0x03AC0EB0` | 捕获通用请求上下文 |
| `VoiceSpeakChannelProcessor._PlayVoice` | `0x03ABF3A0` | 读取角色、voice ID 和 Event |
| `VoiceUtilsInternal.SelectWwiseEvent` | `0x03ABF630` | 在 Event 选择窗口绑定角色规则 |
| `VoicePlayer.PlayVoice` | `0x03ABF7B0` | 语音播放总入口 |
| `VoicePlayer._PlayVoice(ref)` | `0x03ABF1B0` | 外部 WEM 播放入口 |
| `VoicePlayer._PlayEvent` | `0x03ABF2D0` | 普攻等普通 Wwise Event；原生容器路线在此提交原 Event |
| `VoicePlayer._PlayExternal` | `0x06AF0458` | 无原生 HIRC 映射时的 v9 外部媒体降级 |
| `AudioAdapter.PostEventExternal` | `0x03ABF0A0` | 将外部源路径改为目标语言路径 |

请求对象目前读取以下已验证字段偏移：

| 字段 | 偏移 |
| --- | ---: |
| Wwise Event | `0x20` |
| speakerChannel | `0x50` |
| data/voice ID | `0x60` |
| codec | `0x68` |

这些偏移只在核心入口签名全部匹配后使用。读取异常会终止本次识别并保留原语音。

### 普攻和短语音的原生容器路线

当前 937 条具名角色短语音中：

- 595 条四语 BNK/HIRC 路由结构完全一致，可保留原生 Wwise Random/Sequence Container。
- 342 条当前输入缺少可归属 Bank，继续使用 v9 `_PlayExternal` 或原语音降级。
- 1,606 个稳定 Sound Object 叶子槽位建立了四语 Media ID 映射。

运行流程：

```text
原始 Wwise Event
  -> 原始 Random/Sequence Container 按游戏权重、Avoid Repeat 等规则选择 Sound
  -> AkSoundEngine.SetMedia 将该 Sound 的源语言 Media ID 指向目标语言 WEM 内存
```

`SetMedia` 不改变 Event、Action、Container、Playlist、权重或随机模式。生成器要求四种语言的所有非 Sound 可达 HIRC 对象原始字节一致；Sound 仅允许 Media ID、媒体大小和语言特有增益不同。

### Wwise 动态接口

从 `AK.Wwise.Unity.API.dll` 动态解析：

| 类 | 方法 | 用途 |
| --- | --- | --- |
| `AkSoundEnginePINVOKE` | `CSharp_SetMedia` | 注册源 Media ID 到目标语言驻留 WEM |
| `AkSoundEnginePINVOKE` | `CSharp_UnsetMedia` | 热更新或退出前解除注册 |
| `AkExternalSourceInfo` | `set_szFile` | 捕获 v9 外部媒体占位路径 |
| `AkExternalSourceInfo` | `set_pInMemory` | 将 v9 外部媒体源设为驻留内存 |
| `AkExternalSourceInfo` | `set_uiMemorySize` | 设置外部媒体内存长度 |

目标 WEM 只从用户本机已安装 PCK 读取并驻留内存，不写出到磁盘、不随发布包分发。PCK 头部会按清单记录的大小和 SHA-256 验证，缺包、版本不符或预载失败时不启用原生路线。

`SetMedia` 热更新采用事务式处理：先预载新目标，再解除变化项并注册新项；失败时逐项清理新注册并恢复旧注册。任何无法确认的 `UnsetMedia` 状态都会关闭原生快速路径、保留 WEM 内存，并拒绝手动卸载模块，避免 Wwise 持有悬空指针。

### 语言包和 Wwise 状态

| 接口 | RVA | 用途 |
| --- | ---: | --- |
| `VoiceI18n.SetLanguage` | `0x03EB3F70` | 已保留的全局语言诊断/兼容接口，不用于当前原生叶子随机 |
| `VoiceI18n.GetCurrentLanguage` | `0x03AC13B0` | 查询游戏语音语言 |
| `VoiceI18n.GetLanguageName` | `0x03EB40D0` | 把内部枚举转换为 PCK 语言名 |
| `AudioVfs.TryLoadLanguagePck` | `0x03EB45A0` | 挂载目标语言所需 PCK |
| `AkSoundEngine.LoadFilePackage` | `0x03EB5CD0` | 观察并保留辅助语言包 |
| `AkSoundEngine.UnloadFilePackage` | `0x053DC840` | 抑制仍被目标角色使用的辅助包卸载 |
| `AkSoundEngine.GetCurrentLanguage` | `0x053D10A4` | 日志诊断 Wwise 当前全局语言 |

## 剧情语音、时长和口型

### 剧情与停止生命周期

| 接口 | RVA | 用途 |
| --- | ---: | --- |
| `VoiceManager._SpeakNarrative` | `0x045E3F20` | 识别剧情角色并为语音/口型建立语言作用域 |
| `VoiceUtils.TryGetVoiceDuration(string)` | `0x03ABB800` | 返回目标语言 `AudioDialog` 时长，避免按源语言提前停止 |
| `VoiceSpeakChannelHolder.StopChannel` | `0x03ABC9A0` | 记录频道停止来源和 50 ms 淡出 |
| `VoicePlayer.StopVoice` | `0x03ABCA20` | 记录最终停止调用 |
| `AudioAdapter.StopByPlayingId` | `0x0635BBD0` | 诊断 Playing ID 停止 |
| `AudioAdapter._ExecuteActionOnPlayingId` | `0x03870420` | 诊断 Wwise Action |
| `AudioActionQueueHelper.QueueExecuteAction` | `0x03870520` | 诊断排队 Action |
| `AudioActionQueueHelper._ConsumeExecute` | `0x0328C150` | 诊断 Action 消费 |

另外使用 `AudioAdapter` 的普通 PostEvent、内部提交、回调和 Playing ID/播放位置查询入口记录语音生命周期。这些属于诊断组，签名不匹配不会关闭基础角色配音。

### 动态口型方法

从 `Gameplay.Beyond.dll` 动态解析：

- `Beyond.Gameplay.Core.DialogManager._PlayLipSyncTrack`
- `Beyond.Gameplay.View.LipSync.LipSyncUtils.GetLipSyncTrackPath(AudioLang, string, string)`
- `Beyond.Gameplay.View.LipSync.LipSyncUtils.TryLoadTrack`
- `Beyond.Gameplay.DialogPlayTrunkActionData.get_realActorNameId`
- `Beyond.Gameplay.DialogPlayTrunkActionData.get_actorNameId`
- `Beyond.Gameplay.DialogPlayTrunkActionData.get_trunkId`

口型只在剧情请求作用域内临时覆盖语言路径。目标语言 Track 不存在或任一动态方法不可用时，保留原语言口型；口型组失败不影响战斗、探索和普通剧情语音。

## 资源清单接口

资源清单工作流只读以下游戏数据：

- 有效 VFS overlay：先 `StreamingAssets`，再应用 `Persistent` 覆盖。
- `Bundles/Windows/manifest.hgmmap`。
- `TableCfg/AudioDialog.bytes`。
- 可玩角色 `PrefabInfo`。
- 当前各语言最大且有效的本地化 Bank PCK，以及 Media PCK 索引。

Event ID 使用小写 UTF-8 的 FNV-1 32-bit；不能改成 FNV-1a。HIRC 映射要求 AudioDialog row、Event ID、BNK 名称/Event 对象以及可达 Sound 一致，Media ID 必须存在于对应语言 PCK。

更新游戏版本时使用仓库技能和脚本：

```powershell
& "$env:USERPROFILE\.codex\skills\endfield-resource-manifests\scripts\update_manifests.ps1" `
  -Workspace "E:\Dr.Hydra\EF-Start-Change" `
  -GamePath "E:\Endfield Game"
```

生成结果包括动作清单、语音 Event-to-Media 清单、紧凑二进制表和 C++ 嵌入头文件。发布物不包含 PCK、BNK 或 WEM 内容。

## 更新版本时的验证顺序

1. 记录新 `GameAssembly.dll`、VFS manifest 和 `AudioDialog.bytes` 哈希。
2. 重新生成资源清单，确认缺 Bank、无效 HIRC 和缺失 PCK Media 数量。
3. 对所有固定 RVA 重取入口并验证字节签名；不要只比较整个 DLL 哈希。
4. 优先验证核心 Hook，再验证诊断、剧情和口型可选组。
5. 离线测试模型加载、四阶段动画和登录场景释放。
6. 测试至少一个外部 WEM、一个原生容器短语音和一个 v9 降级语音。
7. 测试角色语言热更新以及 `SetMedia` 清理/恢复日志。
8. 检查目标语言剧情时长和口型路径。
9. 分别记录官服与 B 服结果；资源表一致不能替代函数入口验证。

关键成功日志：

```text
[voice-native] SetMedia API resolved dynamically
[voice-memory] selective preload complete ... result=ready
[voice-native] SetMedia routes ready ...
[voice-native] original Wwise container submitted ...
```

关键降级日志：

```text
[voice-lang] signature mismatch
[compat] ... unavailable
[voice-native] ... v9 fallback retained
[voice-native] final UnsetMedia failed ...
```

## 品牌兼容标识

项目自本次提交起对外名称为 **Better Endfield**。以下旧标识暂时保留，属于配置和升级兼容协议，不代表旧品牌仍在使用：

- 配置文件：`EFStartChange.ini`
- INI 节：`[EFStartChange]`
- 旧界面设置迁移源：`%LocalAppData%\EFStartChange`
- C# 内部命名空间和嵌入资源逻辑名：`EFStartChange.UI.*`
- v9 外部媒体内部占位协议：`efstartchange://`

删除或重命名这些标识前必须同时提供迁移逻辑，并回归测试旧安装升级、热更新和注入器配置读取。
