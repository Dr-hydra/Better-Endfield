# PC 语音时长调查暂存（2026-09-03）

> 状态：根因已于 2026-09-04 定案并实施修复（见 §7），待运行时验证。
> 本文只讨论当前 PC 版；Android 使用旧实现，不能作为当前 PC Hook 入口的依据。

## 1. 已证实的调用面

当前 PC VFS 的 1,338 个 Lua 覆盖文件已做全量内存扫描，两层 VFS 共扫描 2,676 条记录，
解密失败为 0。`VoiceUtils.TryGetVoiceDuration` 在最终覆盖层中共有 8 个文件、12 处调用：

| 场景 | 文件 | 时长用途 |
|---|---|---|
| 角色档案 | `CharInfoProfileCtrl.lua` | 最短时长修正和自动停止计时器 |
| 电台系统 | `RadioSystem.lua` | 分段/总时长、剩余时间、播放进度；另读取播放位置 |
| PRTS 电台 | `PRTSRadio.lua` | 总时长、分段切换、进度和剩余时间 |
| 对话回放 | `DialogRecordCtrl.lua` | 回放停止计时器和按钮状态 |
| 远程通讯 | `RemoteCommCtrl.lua` | 语音结束计时器 |
| 远程通讯 HUD | `RemoteCommHudCtrl.lua` | 剩余显示时间和超时 |
| 黑屏/遮罩对白 | `CommonMaskCtrl.lua` | 黑屏字幕持续时间，失败时回退文本时长 |
| Reflow 正式对话弹窗 | `ReflowFormalDialogueCtrl.lua` | 只检查查询是否成功；失败会直接拒绝播放 |

其中 `ReflowFormalDialogueCtrl.lua` 是本轮全量扫描新补出的消费者。它不使用返回的时长数值，
但把查询成功作为播放前置条件，所以时长链路失败在该场景会直接表现为无声。

当前 native 代码中的直接调用者为：

- `TryGetVoiceDuration(String, Single&)`：7 个调用点，覆盖 `DialogManager._PlayVoice`、
  `DialogTimelineManager` 的普通/指定 ID/情绪语音、`MainFlowHandle.GetDuration` 和
  `EnvTalkHandler._TryGetVoiceId`。
- `TryGetVoiceDuration(Int32, Single&)`：1 个调用点，位于
  `AIBarkManager.TriggerBarkVoice`，结果写入 `AIBarkRuntimeData.duration` 供 Bark UI 使用。
- `DialogManager.UpdateTrunkDuration` 和
  `DialogTimelineManager._AddPlayingVoiceHandleId` 会继续消费上述时长，用于对白自动推进、
  Timeline 等待和语音句柄清理。

## 2. 与语音表时长分开的链路

- 口型使用语言对应的 JSON 轨道，时长来自 `LipSyncTrack._duration/_actualDuration`，不是
  `VoiceUtils.TryGetVoiceDuration` 的返回值。口型路径应单独验证。
- `VoiceSequenceBarker` 主要依赖播放完成/停止回调推进连续 Bark，不直接依赖语音表时长；
  AIBark 的 UI 显示仍会读取 Int32 重载的时长。
- Wwise `AudioDurationCallbackInfo` 当前只在音乐占位和通用 `PlayAudioAndWait` 路径发现调用，
  不是 `VoicePlayer` 的统一时长来源。
- `AudioVoiceExtraData` 表的 `durationCN/EN/JP/KR` 在当前导出中全部为 0，不能作为修复来源。
- `LinkWireBrain.m_lastAudioLength` 是管线铺设长度，不是音频长度，已排除。

## 3. 当前故障点

当前测试日志已经证明：

1. `voice.duration`、`voice.duration-by-id`、两个 IFix wrapper 以及四个 `VoiceData` 语言
   getter 的 Hook 均成功解析并安装。
2. 角色档案实际播放期间，`VoiceManager._SpeakNarrative`、`VoicePlayer.PlayVoice` 和外部媒体
   重定向都有命中，但上述所有时长 Hook 均为零命中。
3. 当前 Lua 源码仍明确调用旧的 `VoiceUtils.TryGetVoiceDuration`；不是游戏把 Lua API 改名或
   删除了。

当前最可信的解释是 XLua 调用路径绕开了 Host 目前 Hook 的 `MethodInfo.methodPointer`：

- `LuaGenericDelegateCallerGen` 中存在对应的预生成调用器：
  `Luastringoutfloat_boolCall`、`Luaintoutfloat_boolCall` 和
  `Luauintoutint_boolCall`。
- 这些调用器接收 `System.Delegate` 并通过委托中的可调用入口间接执行目标方法；静态扫描
  因此看不到对 `TryGetVoiceDuration` 的直接 `call`。
- Host 的动态解析器目前只读取并 Hook `MethodInfo` 的第一个 `methodPointer`。Lua 反射优化
  创建出的委托可能使用另一可调用入口或已缓存的入口，因此 direct/IFix Hook 安装成功但
  Lua 调用不命中。

这仍需一次运行时诊断确认，不能在没有委托字段日志的情况下当作最终根因。

## 4. 已保留的调查材料

- 当前 PC IL2CPP dump：`research/il2cpp-dumps/20260903-pc-current/`
- 8 个相关 Lua 的最小解密集：
  `research/lua-dumps/20260903-pc-current-voice-duration/LuaScripts_decrypted/`
- 全量 Lua 内存扫描脚本：`tmp_analysis/scan_current_lua_voice_duration.py`
- native 调用点和 IFix/XLua 入口扫描脚本：`tmp_analysis/voice_duration_xrefs.py`

## 5. 恢复调查时的下一步

1. 只加诊断 Hook 到 XLua 的 `Luastringoutfloat_boolCall` 和
   `Luaintoutfloat_boolCall`，按 `delegate.method_info/original_method_info` 过滤目标方法，记录
   `method_ptr`、`invoke_impl`、`method_code` 和实际命中入口。
2. 确认委托真正调用的地址后，将修正下沉到该稳定入口；保留现有 direct Hook 覆盖 native
   的剧情、环境对白和 AIBark 调用者。
3. 时长值继续由 mod catalog 的目标 WEM 时长提供，并同时支持字符串 voiceId、
   AudioDialog 数字键和 Int32 voiceId 的映射。
4. 回归矩阵至少覆盖上表 8 个 Lua 场景、native 剧情/Timeline、EnvTalk、AIBark，口型作为
   独立链路验证。

## 6. 发版注意

§3 的 IFix wrapper Hook 与 `VoiceData.get_wavDuration*` getter Hook 从未出现运行时命中，
已在 §7 的修复中删除。§7 的叶子 Hook 在运行日志出现 `[voice-duration-leaf] … via=column|table`
之前，发布说明仍不应宣称“时长修正已恢复”。

## 7. 根因定案与修复（2026-09-04）

§3 的“XLua 委托绕开 methodPointer”猜测方向正确，但机制不同，且不需要运行时诊断即可从字节
级证据定案：

1. `Init.lua:120` 把 `VoiceUtils = CS.Beyond.Gameplay.Audio.VoiceUtils`，Assembly-CSharp 没有
   `VoiceUtilsWrap`，Lua 经 `LuaGenericDelegateCallerGen.Luastringoutfloat_boolCall`
   （0x04971B20）以委托调用。
2. 该委托的 `Invoke` 桩（0x04A43320，以及 `.text` 中的 0x000D5890）由 IL2CPP 生成为
   “`method_ptr == &TryGetVoiceDuration(String)` → 直接执行内联副本”的快路径：桩内完整复制了
   `IsPatched(0x4bb4) → IsPatched(0x3920) → _GetGenderSuffix → AudioVoiceUtil.TryGetVoiceData
   → _GetVoDurationFromVoData` 的函数体。MinHook 挂在 0x0364E0E0 的入口，Lua 路径从不经过该
   地址，因此 Hook 安装成功却零命中。全库对 0x0364E0E0 的 LEA rip-rel 引用恰好只有这两处。
3. 连带结论：`Beyond.Cfg.VoiceData.get_wavDuration*` 四个 getter 全库 0 个调用点（全部内联），
   getter Hook 不可能命中；旧的 IFix methodId 扫描取到的 14624=0x3920 实为被内联的
   `TryGetVoiceData` 的 id，`TryGetVoiceDuration(String)` 的真实 id 是 0x4bb4。
4. 稳定挂点：`VoiceUtils._GetVoDurationFromVoData(Int32 voId, VoiceData& vo) → Single`
   （0x0364F180）。全库恰好 4 个调用点——两个 `TryGetVoiceDuration` 重载与两个委托内联副本——
   且无 LEA 引用，即 §1 的全部 native 调用方、AIBark 的 Int32 重载与 8 个 Lua 场景都最终
   out-of-line 调用它。
5. 修复（`native/modules/voice/module.cpp`）：新增可选契约 `voice.duration-leaf` 挂接该叶子，
   返回后按 `VoiceData.speakerChannel`（其次按 `path` 的对白 ID token）选规则；目标语言与当前
   语言不同时先读该行目标语言的 `wavDuration*` 列（`voice.data.duration-*` 契约只调用不挂接），
   为空再查 Catalog 时长表（对白 ID / `voId` 数字键）。`narrating/` 路径受剧情语音开关约束。
   删除 IFix wrapper Hook、getter Hook 与相关字段解析；两个 `TryGetVoiceDuration` 入口 Hook
   仅保留 `[voice-duration]` 日志。
6. 数据面提醒：当前 Catalog 时长表只覆盖 manifest 中 970 条 `_sv` 短语音，角色档案/剧情等
   外部 WEM 语音一条都不在表内，因此这些场景的修正值完全依赖 `wavDurationEN/JP/KR` 列是否
   有值——需由运行日志 `[voice-duration-leaf] … source= routed= via=` 确认。
7. 验证点：角色档案播放已配置角色语音时出现 `[voice-duration-leaf] … matchedBy=speaker
   via=column routed=<目标语言时长>`，且档案页停止计时与替换语音对齐。
