// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.AudioModule.dll
// Classes:  19
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

CLASS: AudioConfigurationChangeHandler
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x07C41504  token=0x6000004  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000005  System.Void Invoke(System.Boolean deviceWasChanged)
END_CLASS

CLASS: PCMReaderCallback
TYPE:  sealed class
TOKEN: 0x2000005
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x082A4F84  token=0x600000B  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x600000C  System.Void Invoke(System.Single[] data)
END_CLASS

CLASS: PCMSetPositionCallback
TYPE:  sealed class
TOKEN: 0x2000006
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x069095FC  token=0x600000D  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x600000E  System.Void Invoke(System.Int32 position)
END_CLASS

CLASS: SampleFramesHandler
TYPE:  sealed class
TOKEN: 0x2000013
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B3A4614  token=0x6000052  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x6000053  System.Void Invoke(UnityEngine.Experimental.Audio.AudioSampleProvider provider, System.UInt32 sampleFrameCount)
END_CLASS

CLASS: UnityEngine.AudioSettings
TYPE:  sealed class
TOKEN: 0x2000002
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.AudioSettings.AudioConfigurationChangeHandlerOnAudioConfigurationChanged  // static @ 0x0
  private   static  System.Action                   OnAudioSystemShuttingDown  // static @ 0x8
  private   static  System.Action                   OnAudioSystemStartedUp  // static @ 0x10
METHODS:
  RVA=0x0B3A3EFC  token=0x6000001  System.Void InvokeOnAudioConfigurationChanged(System.Boolean deviceWasChanged)
  RVA=0x0B3A3F4C  token=0x6000002  System.Void InvokeOnAudioSystemShuttingDown()
  RVA=0x0B3A3F90  token=0x6000003  System.Void InvokeOnAudioSystemStartedUp()
END_CLASS

CLASS: UnityEngine.AudioClip
TYPE:  sealed class
TOKEN: 0x2000004
SIZE:  0x28
EXTENDS: UnityEngine.Object
FIELDS:
  private           UnityEngine.AudioClip.PCMReaderCallbackm_PCMReaderCallback  // 0x18
  private           UnityEngine.AudioClip.PCMSetPositionCallbackm_PCMSetPositionCallback  // 0x20
PROPERTIES:
  length  get=0x0B3A330C
  samples  get=0x0B3A3340
  frequency  get=0x0B3A32D8
METHODS:
  RVA=0x0B3A32A8  token=0x6000009  System.Void InvokePCMReaderCallback_Internal(System.Single[] data)
  RVA=0x0B3A32C0  token=0x600000A  System.Void InvokePCMSetPositionCallback_Internal(System.Int32 position)
END_CLASS

CLASS: UnityEngine.AudioBehaviour
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x18
EXTENDS: UnityEngine.Behaviour
FIELDS:
METHODS:
  RVA=0x05393520  token=0x600000F  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.AudioListener
TYPE:  sealed class
TOKEN: 0x2000008
SIZE:  0x18
EXTENDS: UnityEngine.AudioBehaviour
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.AudioSource
TYPE:  sealed class
TOKEN: 0x2000009
SIZE:  0x18
EXTENDS: UnityEngine.AudioBehaviour
FIELDS:
PROPERTIES:
  volume  set=0x0B3A45D0
  pitch  set=0x0B3A44FC
  time  get=0x0B3A43B8  set=0x0B3A458C
  clip  set=0x0B3A4430
  outputAudioMixerGroup  set=0x0B3A4474
  isPlaying  get=0x0B3A4384
  playOnAwake  set=0x0B3A4504
  panStereo  set=0x0B3A44B8
  spatialBlend  set=0x0B3A4548
  bypassReverbZones  set=0x0B3A43EC
METHODS:
  RVA=0x0B3A42F0  token=0x6000010  System.Void SetPitch(UnityEngine.AudioSource source, System.Single pitch)
  RVA=0x0B3A41A4  token=0x6000011  System.Void PlayHelper(UnityEngine.AudioSource source, System.UInt64 delay)
  RVA=0x0B3A41E8  token=0x6000012  System.Void PlayOneShotHelper(UnityEngine.AudioSource source, UnityEngine.AudioClip clip, System.Single volumeScale)
  RVA=0x0B3A4340  token=0x6000013  System.Void Stop(System.Boolean stopOneShots)
  RVA=0x0B3A42E4  token=0x600001A  System.Void Play()
  RVA=0x0B3A423C  token=0x600001B  System.Void PlayOneShot(UnityEngine.AudioClip clip, System.Single volumeScale)
  RVA=0x0B3A4334  token=0x600001C  System.Void Stop()
  RVA=0x0B3A3FD4  token=0x600001E  System.Void PlayClipAtPoint(UnityEngine.AudioClip clip, UnityEngine.Vector3 position, System.Single volume)
  RVA=0x05393520  token=0x6000023  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Audio.AudioClipPlayable
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x20
IMPLEMENTS: UnityEngine.Playables.IPlayable System.IEquatable`1
FIELDS:
  private           UnityEngine.Playables.PlayableHandlem_Handle  // 0x10
METHODS:
  RVA=0x0B3A2AF8  token=0x6000024  UnityEngine.Audio.AudioClipPlayable Create(UnityEngine.Playables.PlayableGraph graph, UnityEngine.AudioClip clip, System.Boolean looping)
  RVA=0x0B3A2A30  token=0x6000025  UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph, UnityEngine.AudioClip clip, System.Boolean looping)
  RVA=0x0B3A3158  token=0x6000026  System.Void .ctor(UnityEngine.Playables.PlayableHandle handle)
  RVA=0x04D9E560  token=0x6000027  UnityEngine.Playables.PlayableHandle GetHandle()
  RVA=0x0315BC70  token=0x6000028  UnityEngine.Playables.Playable op_Implicit(UnityEngine.Audio.AudioClipPlayable playable)
  RVA=0x0B3A3230  token=0x6000029  UnityEngine.Audio.AudioClipPlayable op_Explicit(UnityEngine.Playables.Playable playable)
  RVA=0x0B3A2C0C  token=0x600002A  System.Boolean Equals(UnityEngine.Audio.AudioClipPlayable other)
  RVA=0x0B3A30B0  token=0x600002B  System.Void SetVolume(System.Single value)
  RVA=0x0B3A2FC0  token=0x600002C  System.Void SetStereoPan(System.Single value)
  RVA=0x0B3A2E90  token=0x600002D  System.Void SetSpatialBlend(System.Single value)
  RVA=0x0B3A2CE4  token=0x600002E  System.Void Seek(System.Double startTime, System.Double startDelay, System.Double duration)
  RVA=0x0B3A306C  token=0x600002F  System.Void SetVolumeInternal(UnityEngine.Playables.PlayableHandle& hdl, System.Single volume)
  RVA=0x0B3A2F7C  token=0x6000030  System.Void SetStereoPanInternal(UnityEngine.Playables.PlayableHandle& hdl, System.Single stereoPan)
  RVA=0x0B3A2E4C  token=0x6000031  System.Void SetSpatialBlendInternal(UnityEngine.Playables.PlayableHandle& hdl, System.Single spatialBlend)
  RVA=0x0B3A2F38  token=0x6000032  System.Void SetStartDelayInternal(UnityEngine.Playables.PlayableHandle& hdl, System.Double delay)
  RVA=0x0B3A2E08  token=0x6000033  System.Void SetPauseDelayInternal(UnityEngine.Playables.PlayableHandle& hdl, System.Double delay)
  RVA=0x0B3A2C80  token=0x6000034  System.Boolean InternalCreateAudioClipPlayable(UnityEngine.Playables.PlayableGraph& graph, UnityEngine.AudioClip clip, System.Boolean looping, UnityEngine.Playables.PlayableHandle& handle)
END_CLASS

CLASS: UnityEngine.Audio.AudioMixer
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
METHODS:
  RVA=0x0B3A39C4  token=0x6000035  System.Void .ctor()
  RVA=0x0B3A36FC  token=0x6000036  UnityEngine.Audio.AudioMixerSnapshot FindSnapshot(System.String name)
  RVA=0x0B3A3794  token=0x6000037  System.Void TransitionToSnapshot(UnityEngine.Audio.AudioMixerSnapshot snapshot, System.Single timeToReach)
  RVA=0x0B3A3740  token=0x6000038  System.Void TransitionToSnapshotInternal(UnityEngine.Audio.AudioMixerSnapshot snapshot, System.Single timeToReach)
END_CLASS

CLASS: UnityEngine.Audio.AudioMixerGroup
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Audio.AudioMixerPlayable
TYPE:  sealed struct
TOKEN: 0x200000D
SIZE:  0x20
IMPLEMENTS: UnityEngine.Playables.IPlayable System.IEquatable`1
FIELDS:
  private           UnityEngine.Playables.PlayableHandlem_Handle  // 0x10
METHODS:
  RVA=0x0B3A349C  token=0x6000039  UnityEngine.Audio.AudioMixerPlayable Create(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount, System.Boolean normalizeInputVolumes)
  RVA=0x0B3A33C8  token=0x600003A  UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount, System.Boolean normalizeInputVolumes)
  RVA=0x0B3A3570  token=0x600003B  System.Void .ctor(UnityEngine.Playables.PlayableHandle handle)
  RVA=0x04D9E560  token=0x600003C  UnityEngine.Playables.PlayableHandle GetHandle()
  RVA=0x0315BC70  token=0x600003D  UnityEngine.Playables.Playable op_Implicit(UnityEngine.Audio.AudioMixerPlayable playable)
  RVA=0x0B3A34FC  token=0x600003E  System.Boolean Equals(UnityEngine.Audio.AudioMixerPlayable other)
  RVA=0x0B3A3374  token=0x600003F  System.Boolean CreateAudioMixerPlayableInternal(UnityEngine.Playables.PlayableGraph& graph, System.Boolean normalizeInputVolumes, UnityEngine.Playables.PlayableHandle& handle)
END_CLASS

CLASS: UnityEngine.Audio.AudioMixerSnapshot
TYPE:  class
TOKEN: 0x200000E
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
PROPERTIES:
  audioMixer  get=0x0B3A36C8
METHODS:
  RVA=0x0B3A3688  token=0x6000040  System.Void .ctor()
  RVA=0x0B3A3648  token=0x6000042  System.Void TransitionTo(System.Single timeToReach)
END_CLASS

CLASS: UnityEngine.Audio.AudioPlayableBinding
TYPE:  static class
TOKEN: 0x200000F
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B3A3A44  token=0x6000043  UnityEngine.Playables.PlayableBinding Create(System.String name, UnityEngine.Object key)
  RVA=0x0B3A3A04  token=0x6000044  UnityEngine.Playables.PlayableOutput CreateAudioOutput(UnityEngine.Playables.PlayableGraph graph, System.String name)
END_CLASS

CLASS: UnityEngine.Audio.AudioPlayableGraphExtensions
TYPE:  static class
TOKEN: 0x2000010
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B3A3B60  token=0x6000045  System.Boolean InternalCreateAudioOutput(UnityEngine.Playables.PlayableGraph& graph, System.String name, UnityEngine.Playables.PlayableOutputHandle& handle)
END_CLASS

CLASS: UnityEngine.Audio.AudioPlayableOutput
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x20
IMPLEMENTS: UnityEngine.Playables.IPlayableOutput
FIELDS:
  private           UnityEngine.Playables.PlayableOutputHandlem_Handle  // 0x10
PROPERTIES:
  Null  get=0x0B3A3DBC
METHODS:
  RVA=0x0B3A3BB4  token=0x6000046  UnityEngine.Audio.AudioPlayableOutput Create(UnityEngine.Playables.PlayableGraph graph, System.String name, UnityEngine.AudioSource target)
  RVA=0x0B3A3CE4  token=0x6000047  System.Void .ctor(UnityEngine.Playables.PlayableOutputHandle handle)
  RVA=0x04D9E560  token=0x6000049  UnityEngine.Playables.PlayableOutputHandle GetHandle()
  RVA=0x0315BC70  token=0x600004A  UnityEngine.Playables.PlayableOutput op_Implicit(UnityEngine.Audio.AudioPlayableOutput output)
  RVA=0x0B3A3E34  token=0x600004B  UnityEngine.Audio.AudioPlayableOutput op_Explicit(UnityEngine.Playables.PlayableOutput output)
  RVA=0x0B3A3CDC  token=0x600004C  System.Void SetTarget(UnityEngine.AudioSource value)
  RVA=0x0B3A3CD4  token=0x600004D  System.Void SetEvaluateOnSeek(System.Boolean value)
  RVA=0x0B3A3C90  token=0x600004E  System.Void InternalSetTarget(UnityEngine.Playables.PlayableOutputHandle& output, UnityEngine.AudioSource target)
  RVA=0x0B3A3C4C  token=0x600004F  System.Void InternalSetEvaluateOnSeek(UnityEngine.Playables.PlayableOutputHandle& output, System.Boolean value)
END_CLASS

CLASS: UnityEngine.Experimental.Audio.AudioSampleProvider
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x20
FIELDS:
  private           UnityEngine.Experimental.Audio.AudioSampleProvider.SampleFramesHandlersampleFramesAvailable  // 0x10
  private           UnityEngine.Experimental.Audio.AudioSampleProvider.SampleFramesHandlersampleFramesOverflow  // 0x18
METHODS:
  RVA=0x0B3A3EAC  token=0x6000050  System.Void InvokeSampleFramesAvailable(System.Int32 sampleFrameCount)
  RVA=0x0B3A3ED4  token=0x6000051  System.Void InvokeSampleFramesOverflow(System.Int32 droppedSampleFrameCount)
END_CLASS

