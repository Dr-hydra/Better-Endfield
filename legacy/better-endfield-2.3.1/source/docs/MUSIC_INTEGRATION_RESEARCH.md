# 终末地音乐输入研究

## 结论

终末地可以通过 Wwise Audio Input 接收 Better Endfield 提供的实时 PCM。
2026-08-16 的离线实验已完成一条 48 kHz、单声道、float32 正弦流：

- 格式回调：1 次。
- 采样回调：750 次。
- 每次请求：512 帧。
- 总帧数：384,000。
- 连续时长：8 秒。

这证明后续可以将正弦生成器替换为 OmniMix 共享内存环形缓冲区，
不需要把音乐预编码为 WEM，也不需要在运行时生成 PCK。正式模块随后已用
OmniMix 的 44.1 kHz/48 kHz 立体声在线流完成同一链路的游戏内验证。

## 已确认环境

- Wwise SDK 集成版本：2023.1.17。
- SoundBank 版本：150。
- Audio Input 插件 ID：`0x00C80002`。
- `IsPluginRegistered(Source, Audiokinetic, 200)` 返回 `true`。
- 当前测试 Bank ID：`3704265910`。
- 当前测试 Event ID：`95937692`。

接口均通过程序集、类型、方法名和完整签名解析，不依赖固定 RVA。

## 正确运行时链路

```text
LoadBankMemoryCopy
  -> 创建 UnityEngine.GameObject
  -> AkSoundEngine.RegisterGameObj(GameObject)
  -> AkAudioInputManager.PostAudioInputEvent(...)
     -> TryInitialize
     -> SetAudioInputCallbacks
     -> PostEvent + EndOfEvent callback
     -> AddPlayingID
  -> InternalAudioFormatDelegate
  -> InternalAudioSamplesDelegate
  -> Wwise Audio Input voice
```

必要条件：

1. 承载 Event 的 `GameObject` 必须先通过 Wwise 注册。
2. 必须保留游戏原生 `SetAudioInputCallbacks` 注册结果。
3. 不得再次调用原生 P/Invoke 目标覆盖游戏创建的回调指针。
4. Audio Input Bank 必须包含独立的 Source 插件 HIRC 对象。

## Bank 合同

最小可用 Bank 是四对象结构，不是三对象结构：

```text
CAkFxCustom (0x11, Audio Input Source)
  -> CAkSound (0x02)
     -> CAkActionPlay (0x03)
        -> CAkEvent (0x04)
```

关键引用：

- `CAkFxCustom.ulID == CAkSound.sourceID`。
- `CAkFxCustom.fxID == 0x00C80002`。
- `CAkSound.ulPluginID == 0x00C80002`。
- Sound 的 `DirectParentID` 为 0 时，`OverrideBusId` 必须指向游戏中的有效 Bus。
- Play Action 必须引用 Sound 和当前 Bank。
- Event 必须引用 Play Action。

三对象 Bank 只有 `Sound -> Action -> Event`。它能成功加载并返回 Playing ID，
但 Sound 的 `sourceID` 指向不存在的插件对象，因此不会创建 Audio Input Source，
格式与采样回调均不会发生。

Actor Mixer 不是最小结构的必需对象。Sound 直接路由到有效 Bus 即可。

## 探针实现说明

研究探针 `0.7.8` 使用公开的 `PostAudioInputEvent` 路径。当前版本向该方法传入
空的逐流委托，并通过 Hook `InternalAudioFormatDelegate` 和
`InternalAudioSamplesDelegate` 为探针 Playing ID 提供数据。这足以验证完整链路。

`GetSourcePlayPosition` 对 Audio Input 返回 94，即使音频已经正常连续播放，
因此该接口不能作为流式输入的健康检查。应以格式回调、采样回调、请求帧数和
EndOfEvent 生命周期为准。

## OmniMix 正式模块落地

正式模块 `BetterEndfield.Music.dll` 沿用已经实测通过的
`PostAudioInputEvent(..., null, null)` 路径，并 Hook Internal 格式/采样入口。
Hook 只处理模块自己记录的 Playing ID，其他调用全部转发。这样不依赖固定 RVA，
也不在纯 C++ 中构造布局和 GC 生命周期尚未验证的 IL2CPP 托管 Delegate。

采样回调位于音频实时线程，必须满足：

- 不分配内存。
- 不写普通日志。
- 不等待锁、文件或进程。
- 从模块内预分配的 SPSC 环形缓冲区复制 float32 PCM；共享内存只由工作线程读取。
- 缓冲区欠载时补零并累计计数。
- 停止时返回 `false`，正常供流时返回 `true`。

格式回调固定协商 Wwise 输出采样率、立体声、float32、non-interleaved。工作线程将
OmniMix 的单声道或多声道输入取前两声道并重采样到输出率。44.1 kHz/2ch 到
48 kHz/2ch 及原生 48 kHz/2ch 均已取得连续回调证据；左右声道的主观听感仍由界面
测试人员确认。

当前正式模块已经补齐：

- OmniPcmShared ABI `2.x`、共享协议 `2`、交错 `float32` 能力校验，以及实例握手、心跳和版本检查。
- 后端按需启动、`stream_id`/Seek generation 隔离及带 100 ms 输出余量的自然 EOF 可闻游标回报。
- 48 kHz 立体声重采样、本地 2 秒 SPSC 缓冲与欠载统计。
- Wwise Music Bus 常驻 Event，以及登录/主界面/游戏内原生 Playing ID 的 Pause/Resume。
- Timeline 与游戏音乐暂停/继续同步、后端失效回退、Audio Input 退避重试和安全回调排空。

Audio Input Event 在进程期保持运行。暂停、停止、场景未选中或后端失效时，实时回调
输出静音且不消费本地环形缓冲；恢复时只重新开放 PCM，不对持久 Event 执行 Wwise
Pause/Resume。Source 健康度依据最近一次 Wwise 样本回调时间，而不是最近一次非零 PCM
时间，因此后端暂停不会被误判为 Source 停滞。`QueueManagement` 与 `Seek` 能力已在 OmniMix 实例握手中声明，
模块以 `stream_id`、格式 generation 和 Seek generation 中任一变化作为清空旧缓冲的边界。

## 实验证据

当前测试目录：

```text
artifacts/BetterEndfield-win-x64
```

成功 Bank：

```text
size: 188 bytes
SHA-256: FADF37A7FDF9A98B622F8A3B393B06490A3F1D5188D1D819EE93F209CF9A7849
```

成功日志关键行：

```text
RegisterGameObj(GameObject) result=1 exception=false
PostAudioInputEvent returned playingId=3
format callback playingId=3 sampleRate=48000 channels=1 bits=32 blockAlign=4
first sample callback playingId=3 frames=512 sampleRate=48000
sine source completed playingId=3 callbacks=750 frames=384000
```

### OmniMix 正式链路实测

2026-08-16 使用部署后的 `playerbuild` 与真实游戏进程验证：

- SDK ABI `2.0`、共享协议 `2`，实例自动连接成功。
- 44.1 kHz/2ch 流绑定后以 48 kHz/2ch 输出，持久 Event 创建成功。
- 暂停/恢复及直接切歌期间 Playing ID 保持不变，回调从 0 连续增长到 3060；
  切换 `stream 14 -> 15` 后没有 `callback stalled`。
- Seek 产生 `stream 17 -> 18`，旧本地缓冲被清空；自然结束进入 `state=4`
  (`Draining`)，环形缓冲降至 1809 帧后约 2 秒结束。
- 强制终止后端后记录 `reason=session_unavailable`，游戏保持响应；模块自动重新启动后端、
  重连，并在后端流编号重置为 1 后复用原游戏内 Audio Input Event 继续播放。

本次动态场景证据来自登录音乐范围。Meta 与 Gameplay 的字段和 Hook 契约均在模块初始化时
解析成功，但离线客户端无法进入对应在线场景，因此两者尚无实际 Playing ID 切换证据。
当前 `playerbuild` 也未包含交接要求中的 2 秒 48 kHz 立体声测试 WAV；正式验收仍应补齐该
独立测试输入。

### 2026-08-17 队列能力复测

Better Endfield 最初只在 `OmniPcmClient_ConnectInstance` 中声明
`ServerControlledPlayback | Seek | AudioPlayback`。OmniMix 因此按能力策略拒绝
`insertIntoQueue`，返回 `FailedPrecondition: queue management not available`。正式模块现已
补充 `QueueManagement (1 << 2)`；重新连接会覆盖既有实例的布尔能力，无需删除实例。

队列修复后，OmniMix 能识别在线客户端、建立共享流并触发 Wwise Audio Input。联调期间
曾偶发一次 `0xc0000005` 呈现线程崩溃，故障栈为：

```text
D3D12Core.dll+0x13610
  -> amdvlk64!IcdPresentBuffers
  -> UnityPlayer
```

故障前音乐日志为 `44100Hz/2ch -> 48000Hz`、Playing ID `7`、1126 次采样回调且
`underflows=0`，崩溃线程不包含 Better Endfield、OmniPcmShared、Wwise 或音频回调。
后续播放测试正常，且该故障无法稳定复现；现有证据不足以把它归因于 OmniMix、音乐模块、
Vulkan 或 D3D。该事件只作为未归因的偶发现象留档，不形成图形后端兼容性结论，也不在
音乐模块内加入图形 API 特判。
