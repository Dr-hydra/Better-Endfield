// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.DirectorModule.dll
// Classes:  2
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

# AI-FRIENDLY STRUCTURED DUMP
# Optimized for LLM parsing / code generation

CLASS: <Module>
TYPE:  class
TOKEN: 0x2000001
SIZE:  0x1
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Playables.PlayableDirector
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x30
EXTENDS: UnityEngine.Behaviour
IMPLEMENTS: UnityEngine.IExposedPropertyTable
FIELDS:
  private           System.Action<UnityEngine.Playables.PlayableDirector>played  // 0x18
  private           System.Action<UnityEngine.Playables.PlayableDirector>paused  // 0x20
  private           System.Action<UnityEngine.Playables.PlayableDirector>stopped  // 0x28
PROPERTIES:
  state  get=0x05395DC0
  extrapolationMode  get=0x053947B0  set=0x0B3F7120
  playableAsset  get=0x039F07B0  set=0x0B3F7128
  playableGraph  get=0x03E0E9D0
  playOnAwake  get=0x0B3F6F3C  set=0x0539770C
  timeUpdateMode  get=0x039EDB60  set=0x0B3F7130
  time  get=0x0B3F6F48  set=0x0366D6A0
  initialTime  get=0x0B3F6F08  set=0x0366D6F0
  duration  get=0x0B3F6ED4
EVENTS:
  played  add=add_played  remove=remove_played
  paused  add=add_paused  remove=remove_paused
  stopped  add=add_stopped  remove=remove_stopped
METHODS:
  RVA=0x0B3F67A0  token=0x6000009  System.Void DeferredEvaluate()
  RVA=0x0B3F6990  token=0x600000A  System.Void Play(UnityEngine.Playables.FrameRate frameRate)
  RVA=0x0B3F69AC  token=0x600000B  System.Void Play(UnityEngine.Playables.PlayableAsset asset)
  RVA=0x0B3F68B4  token=0x600000C  System.Void Play(UnityEngine.Playables.PlayableAsset asset, UnityEngine.Playables.DirectorWrapMode mode)
  RVA=0x0B3F6C00  token=0x600000D  System.Void SetGenericBinding(UnityEngine.Object key, UnityEngine.Object value)
  RVA=0x03E0D660  token=0x6000015  System.Void Evaluate()
  RVA=0x0B3F6898  token=0x6000016  System.Void PlayOnFrame(UnityEngine.Playables.FrameRate frameRate)
  RVA=0x0B3F6A70  token=0x6000017  System.Void Play()
  RVA=0x0B3F6CFC  token=0x6000018  System.Void Stop()
  RVA=0x0366D660  token=0x6000019  System.Void Pause()
  RVA=0x0B3F6B6C  token=0x600001A  System.Void Resume()
  RVA=0x0B3F6B0C  token=0x600001B  System.Void RebuildGraph()
  RVA=0x0B3F6784  token=0x600001C  System.Void ClearReferenceValue(UnityEngine.PropertyName id)
  RVA=0x0B3F6CA0  token=0x600001D  System.Void SetReferenceValue(UnityEngine.PropertyName id, UnityEngine.Object value)
  RVA=0x05397C5C  token=0x600001E  UnityEngine.Object GetReferenceValue(UnityEngine.PropertyName id, System.Boolean& idValid)
  RVA=0x03FA6DB0  token=0x600001F  UnityEngine.Object GetGenericBinding(UnityEngine.Object key)
  RVA=0x0B3F66FC  token=0x6000020  System.Void ClearGenericBinding(UnityEngine.Object key)
  RVA=0x0B3F6AD8  token=0x6000021  System.Void RebindPlayableGraphOutputs()
  RVA=0x0B3F6AA4  token=0x6000022  System.Void ProcessPendingGraphChanges()
  RVA=0x0B3F6810  token=0x6000023  System.Boolean HasGenericBinding(UnityEngine.Object key)
  RVA=0x04B2D220  token=0x6000024  UnityEngine.Playables.PlayState GetPlayState()
  RVA=0x0B3F6CBC  token=0x6000025  System.Void SetWrapMode(UnityEngine.Playables.DirectorWrapMode mode)
  RVA=0x04A3E130  token=0x6000026  UnityEngine.Playables.DirectorWrapMode GetWrapMode()
  RVA=0x0B3F67A8  token=0x6000027  System.Void EvaluateNextFrame()
  RVA=0x03E0EA40  token=0x6000028  UnityEngine.Playables.PlayableGraph GetGraphHandle()
  RVA=0x049E1860  token=0x6000029  System.Void SetPlayOnAwake(System.Boolean on)
  RVA=0x0B3F67DC  token=0x600002A  System.Boolean GetPlayOnAwake()
  RVA=0x04D56410  token=0x600002B  System.Void Internal_SetGenericBinding(UnityEngine.Object key, UnityEngine.Object value)
  RVA=0x0B3F6C08  token=0x600002C  System.Void SetPlayableAsset(UnityEngine.ScriptableObject asset)
  RVA=0x039F0800  token=0x600002D  UnityEngine.ScriptableObject Internal_GetPlayableAsset()
  RVA=0x0B3F6B40  token=0x6000034  System.Void ResetFrameTiming()
  RVA=0x0B3F6BC0  token=0x6000035  System.Void SendOnPlayableDirectorPlay()
  RVA=0x0B3F6BA0  token=0x6000036  System.Void SendOnPlayableDirectorPause()
  RVA=0x0B3F6BE0  token=0x6000037  System.Void SendOnPlayableDirectorStop()
  RVA=0x05393520  token=0x6000038  System.Void .ctor()
  RVA=0x0B3F6854  token=0x6000039  System.Void PlayOnFrame_Injected(UnityEngine.Playables.FrameRate& frameRate)
  RVA=0x0B3F6740  token=0x600003A  System.Void ClearReferenceValue_Injected(UnityEngine.PropertyName& id)
  RVA=0x0B3F6C4C  token=0x600003B  System.Void SetReferenceValue_Injected(UnityEngine.PropertyName& id, UnityEngine.Object value)
  RVA=0x04B51070  token=0x600003C  UnityEngine.Object GetReferenceValue_Injected(UnityEngine.PropertyName& id, System.Boolean& idValid)
  RVA=0x03E0EA70  token=0x600003D  System.Void GetGraphHandle_Injected(UnityEngine.Playables.PlayableGraph& ret)
END_CLASS

