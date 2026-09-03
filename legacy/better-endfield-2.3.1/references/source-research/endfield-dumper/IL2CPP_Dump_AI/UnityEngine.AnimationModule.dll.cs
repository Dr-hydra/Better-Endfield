// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.AnimationModule.dll
// Classes:  106
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

CLASS: Enumerator
TYPE:  sealed class
TOKEN: 0x200000F
SIZE:  0x20
IMPLEMENTS: System.Collections.IEnumerator
FIELDS:
  private           UnityEngine.Animation           m_Outer  // 0x10
  private           System.Int32                    m_CurrentIndex  // 0x18
PROPERTIES:
  Current  get=0x0400B4A0
METHODS:
  RVA=0x032ED9A0  token=0x6000051  System.Void .ctor(UnityEngine.Animation outer)
  RVA=0x03F2F310  token=0x6000053  System.Boolean MoveNext()
  RVA=0x04D87910  token=0x6000054  System.Void Reset()
END_CLASS

CLASS: AnimationEventCallback
TYPE:  sealed class
TOKEN: 0x200002B
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x03B19A60  token=0x60002B6  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x0622AD60  token=0x60002B7  System.Boolean Invoke(UnityEngine.Animator animator, UnityEngine.FastAnimationEvent fastAnimationEvent)
END_CLASS

CLASS: AnimatorStateChangeCallback
TYPE:  sealed class
TOKEN: 0x200002C
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0408A900  token=0x60002B8  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x0B3999A8  token=0x60002B9  System.Void Invoke(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo animatorStateInfo, System.Int32 layerIndex)
END_CLASS

CLASS: AnimationEventCallbackOpt
TYPE:  sealed class
TOKEN: 0x200002D
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0378DD20  token=0x60002BA  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x0B395A90  token=0x60002BB  System.Boolean Invoke(UnityEngine.Animator animator, UnityEngine.FastAnimationEventGroup fastAnimationEvent)
END_CLASS

CLASS: AniamtorCommonCallback
TYPE:  sealed class
TOKEN: 0x200002E
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x074FF394  token=0x60002BC  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x60002BD  System.Void Invoke(UnityEngine.Animator animator)
END_CLASS

CLASS: RetargetBoneMode
TYPE:  sealed struct
TOKEN: 0x200002F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Animator.RetargetBoneModeTranslation  // const
  public    static  UnityEngine.Animator.RetargetBoneModeScale  // const
METHODS:
END_CLASS

CLASS: OnOverrideControllerDirtyCallback
TYPE:  sealed class
TOKEN: 0x2000033
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x053DEDC8  token=0x60002E0  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x053DEDB8  token=0x60002E1  System.Void Invoke()
END_CLASS

CLASS: ExecuteJobFunction
TYPE:  sealed class
TOKEN: 0x2000052
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000337  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000338  System.Void Invoke(T& data, System.IntPtr animationStreamPtr, System.IntPtr unusedPtr, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex)
END_CLASS

CLASS: Microsoft.CodeAnalysis.EmbeddedAttribute
TYPE:  sealed class
TOKEN: 0x2000002
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
END_CLASS

CLASS: System.Runtime.CompilerServices.IsReadOnlyAttribute
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000002  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.IAnimationClipSource
TYPE:  interface
TOKEN: 0x2000004
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.SharedBetweenAnimatorsAttribute
TYPE:  sealed class
TOKEN: 0x2000005
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000003  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.StateMachineBehaviour
TYPE:  abstract class
TOKEN: 0x2000006
SIZE:  0x18
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000004  System.Void OnStateEnter(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex)
  RVA=0x041E1670  token=0x6000005  System.Void OnStateUpdate(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex)
  RVA=0x041E1670  token=0x6000006  System.Void OnStateExit(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex)
  RVA=0x041E1670  token=0x6000007  System.Void OnStateMove(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex)
  RVA=0x041E1670  token=0x6000008  System.Void OnStateIK(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex)
  RVA=0x041E1670  token=0x6000009  System.Void OnStatePreExit(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex)
  RVA=0x041E1670  token=0x600000A  System.Void OnStateMachineEnter(UnityEngine.Animator animator, System.Int32 stateMachinePathHash)
  RVA=0x041E1670  token=0x600000B  System.Void OnStateMachineExit(UnityEngine.Animator animator, System.Int32 stateMachinePathHash)
  RVA=0x041E1670  token=0x600000C  System.Void OnStateEnter(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex, UnityEngine.Animations.AnimatorControllerPlayable controller)
  RVA=0x041E1670  token=0x600000D  System.Void OnStateUpdate(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex, UnityEngine.Animations.AnimatorControllerPlayable controller)
  RVA=0x041E1670  token=0x600000E  System.Void OnStateExit(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex, UnityEngine.Animations.AnimatorControllerPlayable controller)
  RVA=0x041E1670  token=0x600000F  System.Void OnStateMove(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex, UnityEngine.Animations.AnimatorControllerPlayable controller)
  RVA=0x041E1670  token=0x6000010  System.Void OnStateIK(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex, UnityEngine.Animations.AnimatorControllerPlayable controller)
  RVA=0x041E1670  token=0x6000011  System.Void OnStatePreExit(UnityEngine.Animator animator, UnityEngine.AnimatorStateInfo stateInfo, System.Int32 layerIndex, UnityEngine.Animations.AnimatorControllerPlayable controller)
  RVA=0x041E1670  token=0x6000012  System.Void OnStateMachineEnter(UnityEngine.Animator animator, System.Int32 stateMachinePathHash, UnityEngine.Animations.AnimatorControllerPlayable controller)
  RVA=0x041E1670  token=0x6000013  System.Void OnStateMachineExit(UnityEngine.Animator animator, System.Int32 stateMachinePathHash, UnityEngine.Animations.AnimatorControllerPlayable controller)
  RVA=0x03573370  token=0x6000014  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.PlayMode
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.PlayMode            StopSameLayer  // const
  public    static  UnityEngine.PlayMode            StopAll  // const
METHODS:
END_CLASS

CLASS: UnityEngine.QueueMode
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.QueueMode           CompleteOthers  // const
  public    static  UnityEngine.QueueMode           PlayNow  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AnimationBlendMode
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AnimationBlendMode  Blend  // const
  public    static  UnityEngine.AnimationBlendMode  Additive  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AnimationPlayMode
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AnimationPlayMode   Stop  // const
  public    static  UnityEngine.AnimationPlayMode   Queue  // const
  public    static  UnityEngine.AnimationPlayMode   Mix  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AnimationCullingType
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AnimationCullingTypeAlwaysAnimate  // const
  public    static  UnityEngine.AnimationCullingTypeBasedOnRenderers  // const
  public    static  UnityEngine.AnimationCullingTypeBasedOnClipBounds  // const
  public    static  UnityEngine.AnimationCullingTypeBasedOnUserBounds  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AnimationEventSource
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AnimationEventSourceNoSource  // const
  public    static  UnityEngine.AnimationEventSourceLegacy  // const
  public    static  UnityEngine.AnimationEventSourceAnimator  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AnimEventHashCodeType
TYPE:  sealed struct
TOKEN: 0x200000D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AnimEventHashCodeTypeNone  // const
  public    static  UnityEngine.AnimEventHashCodeTypeCRC32  // const
  public    static  UnityEngine.AnimEventHashCodeTypeHashCode64  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Animation
TYPE:  sealed class
TOKEN: 0x200000E
SIZE:  0x18
EXTENDS: UnityEngine.Behaviour
IMPLEMENTS: System.Collections.IEnumerable
FIELDS:
PROPERTIES:
  clip  get=0x0B3982AC  set=0x0B3984AC
  playAutomatically  get=0x0B3983BC  set=0x032E9F60
  wrapMode  get=0x0B3983F0  set=0x0B398588
  isPlaying  get=0x0B398314
  Item  get=0x0B398238
  animatePhysics  get=0x0B398278  set=0x0B398468
  animateOnlyIfVisible  get=0x0B398244  set=0x0B398424
  cullingType  get=0x0B3982E0  set=0x0B3984F0
  localBounds  get=0x0B39838C  set=0x0B398574
METHODS:
  RVA=0x0378E160  token=0x600001B  System.Void Stop()
  RVA=0x0B3981F0  token=0x600001C  System.Void Stop(System.String name)
  RVA=0x0B3981AC  token=0x600001D  System.Void StopNamed(System.String name)
  RVA=0x0B39813C  token=0x600001E  System.Void Rewind()
  RVA=0x0B398170  token=0x600001F  System.Void Rewind(System.String name)
  RVA=0x0B3980F8  token=0x6000020  System.Void RewindNamed(System.String name)
  RVA=0x0B398178  token=0x6000021  System.Void Sample()
  RVA=0x0B397F88  token=0x6000023  System.Boolean IsPlaying(System.String name)
  RVA=0x045C64C0  token=0x6000025  System.Boolean Play()
  RVA=0x05397994  token=0x6000026  System.Boolean Play(UnityEngine.PlayMode mode)
  RVA=0x04CD8EA0  token=0x6000027  System.Boolean PlayDefaultAnimation(UnityEngine.PlayMode mode)
  RVA=0x053979A0  token=0x6000028  System.Boolean Play(System.String animation)
  RVA=0x0378E100  token=0x6000029  System.Boolean Play(System.String animation, UnityEngine.PlayMode mode)
  RVA=0x0B397F44  token=0x600002A  System.Void CrossFade(System.String animation)
  RVA=0x0B397EC4  token=0x600002B  System.Void CrossFade(System.String animation, System.Single fadeLength)
  RVA=0x0B397EE0  token=0x600002C  System.Void CrossFade(System.String animation, System.Single fadeLength, UnityEngine.PlayMode mode)
  RVA=0x0B397D80  token=0x600002D  System.Void Blend(System.String animation)
  RVA=0x0B397DF4  token=0x600002E  System.Void Blend(System.String animation, System.Single targetWeight)
  RVA=0x0B397D90  token=0x600002F  System.Void Blend(System.String animation, System.Single targetWeight, System.Single fadeLength)
  RVA=0x0B397E4C  token=0x6000030  UnityEngine.AnimationState CrossFadeQueued(System.String animation)
  RVA=0x0B397E14  token=0x6000031  UnityEngine.AnimationState CrossFadeQueued(System.String animation, System.Single fadeLength)
  RVA=0x0B397E30  token=0x6000032  UnityEngine.AnimationState CrossFadeQueued(System.String animation, System.Single fadeLength, UnityEngine.QueueMode queue)
  RVA=0x0B397E60  token=0x6000033  UnityEngine.AnimationState CrossFadeQueued(System.String animation, System.Single fadeLength, UnityEngine.QueueMode queue, UnityEngine.PlayMode mode)
  RVA=0x0B397FCC  token=0x6000034  UnityEngine.AnimationState PlayQueued(System.String animation)
  RVA=0x0B398040  token=0x6000035  UnityEngine.AnimationState PlayQueued(System.String animation, UnityEngine.QueueMode queue)
  RVA=0x0B397FDC  token=0x6000036  UnityEngine.AnimationState PlayQueued(System.String animation, UnityEngine.QueueMode queue, UnityEngine.PlayMode mode)
  RVA=0x0B397CF4  token=0x6000037  System.Void AddClip(UnityEngine.AnimationClip clip, System.String newName)
  RVA=0x0B397CD0  token=0x6000038  System.Void AddClip(UnityEngine.AnimationClip clip, System.String newName, System.Int32 firstFrame, System.Int32 lastFrame)
  RVA=0x0B397D1C  token=0x6000039  System.Void AddClip(UnityEngine.AnimationClip clip, System.String newName, System.Int32 firstFrame, System.Int32 lastFrame, System.Boolean addLoopFrame)
  RVA=0x0B3980B4  token=0x600003A  System.Void RemoveClip(UnityEngine.AnimationClip clip)
  RVA=0x0B3980AC  token=0x600003B  System.Void RemoveClip(System.String clipName)
  RVA=0x0B398068  token=0x600003C  System.Void RemoveClipNamed(System.String clipName)
  RVA=0x0B397F54  token=0x600003D  System.Int32 GetClipCount()
  RVA=0x05397994  token=0x600003E  System.Boolean Play(UnityEngine.AnimationPlayMode mode)
  RVA=0x0B39805C  token=0x600003F  System.Boolean Play(System.String animation, UnityEngine.AnimationPlayMode mode)
  RVA=0x0B3981F8  token=0x6000040  System.Void SyncLayer(System.Int32 layer)
  RVA=0x032ED960  token=0x6000041  System.Collections.IEnumerator GetEnumerator()
  RVA=0x0378E1A0  token=0x6000042  UnityEngine.AnimationState GetState(System.String name)
  RVA=0x0400B500  token=0x6000043  UnityEngine.AnimationState GetStateAtIndex(System.Int32 index)
  RVA=0x03F2F380  token=0x6000044  System.Int32 GetStateCount()
  RVA=0x0378E1F0  token=0x6000045  UnityEngine.AnimationClip GetClip(System.String name)
  RVA=0x05393520  token=0x600004E  System.Void .ctor()
  RVA=0x0B398348  token=0x600004F  System.Void get_localBounds_Injected(UnityEngine.Bounds& ret)
  RVA=0x0B398530  token=0x6000050  System.Void set_localBounds_Injected(UnityEngine.Bounds& value)
END_CLASS

CLASS: UnityEngine.AnimationState
TYPE:  sealed class
TOKEN: 0x2000010
SIZE:  0x18
EXTENDS: UnityEngine.TrackedReference
FIELDS:
PROPERTIES:
  enabled  get=0x0B3975F0  set=0x0B39779C
  weight  get=0x0B397728  set=0x0B397934
  wrapMode  get=0x032EE490  set=0x0B397978
  time  get=0x0B3976F4  set=0x0B3978F0
  normalizedTime  get=0x0B39768C  set=0x0B397868
  speed  get=0x0B3976C0  set=0x0B3978AC
  normalizedSpeed  get=0x0B397658  set=0x0B397824
  length  get=0x032EB770
  layer  get=0x0B397624  set=0x032ED3A0
  clip  get=0x032ECBE0
  name  get=0x032ED360  set=0x0B3977E0
  blendMode  get=0x0B3975BC  set=0x0B39775C
METHODS:
  RVA=0x0B397518  token=0x600006B  System.Void AddMixingTransform(UnityEngine.Transform mix)
  RVA=0x0B397524  token=0x600006C  System.Void AddMixingTransform(UnityEngine.Transform mix, System.Boolean recursive)
  RVA=0x0B397578  token=0x600006D  System.Void RemoveMixingTransform(UnityEngine.Transform mix)
  RVA=0x041E1670  token=0x600006E  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.FastAnimationEvent
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x48
FIELDS:
  private           System.Single                   m_Time  // 0x10
  private           System.Single                   m_ClipWeight  // 0x14
  private           System.String                   m_FunctionName  // 0x18
  private           System.String                   m_StringParameter  // 0x20
  private           UnityEngine.Object              m_ObjectReferenceParameter  // 0x28
  private           System.Single                   m_FloatParameter  // 0x30
  private           System.Int32                    m_IntParameter  // 0x34
  private           UnityEngine.SendMessageOptions  m_MessageOptions  // 0x38
  private           System.Int32                    m_StringParameterHashCode  // 0x3c
  private           System.Int32                    m_FunctionNameHashCode  // 0x40
  private           System.Boolean                  m_IsInTransition  // 0x44
  private           System.Boolean                  m_IsFromMaxWeightClipState  // 0x45
  private           System.Boolean                  m_IsFromNextMaxWeightClipState  // 0x46
PROPERTIES:
  stringParameter  get=0x04D9B600  set=0x053908C0
  floatParameter  get=0x04DBA960  set=0x011EC9E0
  intParameter  get=0x04DBA990  set=0x011F0030
  functionName  get=0x04DBA980
  time  get=0x04DBA9D0  set=0x04D932C0
  messageOptions  set=0x011F2AA0
  clipWeight  get=0x04DBA740  set=0x04DAB920
  isInTransition  get=0x04DBA9C0  set=0x04D86AA0
  isFromMaxWeightClipState  get=0x04DBA9A0  set=0x04D86D40
  isFromNextMaxWeightClipState  get=0x04DBA9B0  set=0x04DBA9E0
  functionNameHashCode  get=0x04DBA970  set=0x04D86740
METHODS:
END_CLASS

CLASS: UnityEngine.FastAnimationEventGroup
TYPE:  sealed struct
TOKEN: 0x2000012
SIZE:  0x78
FIELDS:
  public            System.Int32                    count  // 0x10
  public            System.IntPtr                   times  // 0x18
  public            System.IntPtr                   clipWeights  // 0x20
  public            System.IntPtr                   functionNames  // 0x28
  public            System.IntPtr                   stringParameters  // 0x30
  public            System.IntPtr                   functionNamesHashCode  // 0x38
  public            System.IntPtr                   stringParametersHashCode  // 0x40
  public            System.IntPtr                   floatParameters  // 0x48
  public            System.IntPtr                   intParameters  // 0x50
  public            System.IntPtr                   messageOptions  // 0x58
  public            System.IntPtr                   inTransitions  // 0x60
  public            System.IntPtr                   isFromMaxWeightClipStatus  // 0x68
  public            System.IntPtr                   isFromNextMaxWeightClipStatus  // 0x70
METHODS:
END_CLASS

CLASS: UnityEngine.AnimationEvent
TYPE:  sealed class
TOKEN: 0x2000013
SIZE:  0x90
FIELDS:
  private           System.Single                   m_Time  // 0x10
  private           System.String                   m_FunctionName  // 0x18
  private           System.String                   m_StringParameter  // 0x20
  private           UnityEngine.Object              m_ObjectReferenceParameter  // 0x28
  private           System.Single                   m_FloatParameter  // 0x30
  private           System.Int32                    m_IntParameter  // 0x34
  private           System.Int32                    m_MessageOptions  // 0x38
  private           UnityEngine.AnimationEventSourcem_Source  // 0x3c
  private           UnityEngine.AnimationState      m_StateSender  // 0x40
  private           UnityEngine.AnimatorStateInfo   m_AnimatorStateInfo  // 0x48
  private           UnityEngine.AnimatorClipInfo    m_AnimatorClipInfo  // 0x7c
  private           System.Int32                    m_FunctionNameHashCode  // 0x84
  private           System.Int32                    m_StringParameterHashCode  // 0x88
  private           UnityEngine.AnimEventHashCodeTypem_HashCodeType  // 0x8c
PROPERTIES:
  stringParameter  get=0x04DBA910  set=0x05390F40
  floatParameter  get=0x04DB9A40  set=0x04D86E30
  intParameter  get=0x04DBA8F0  set=0x04D86760
  objectReferenceParameter  set=0x02FDB880
  functionName  get=0x04D96330  set=0x05392C40
  time  get=0x04DBA720  set=0x04D88280
  messageOptions  get=0x04DBA900  set=0x04D86430
  isFiredByAnimator  get=0x0B395BF4
  animatorClipInfo  get=0x0B395B98
METHODS:
  RVA=0x0B395AFC  token=0x6000083  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.AnimationClip
TYPE:  sealed class
TOKEN: 0x2000014
SIZE:  0x20
EXTENDS: UnityEngine.Motion
FIELDS:
PROPERTIES:
  length  get=0x0343B670
  startTime  get=0x0B395810
  stopTime  get=0x0B395844
  frameRate  get=0x0B395698  set=0x0B39592C
  clipTag  get=0x0B395620  set=0x0B3958E0
  wrapMode  get=0x032ECCE0  set=0x0B395A50
  localBounds  get=0x0B3957E0  set=0x0B3959F8
  legacy  get=0x04D770C0  set=0x0B395970
  humanMotion  get=0x0B395768
  empty  get=0x039F6FC0
  hasGenericRootTransform  get=0x03FA5D90
  hasMotionFloatCurves  get=0x0B395700
  hasMotionCurves  get=0x0B3956CC
  hasRootCurves  get=0x0B395734
  hasRootMotion  get=0x03FA5D50
  transferRotationCompressed  get=0x0B395878  set=0x0B395A0C
  uniqueId  get=0x0B3958AC
  events  get=0x0B395654  set=0x0B395924
METHODS:
  RVA=0x0386FF40  token=0x6000093  System.Void .ctor()
  RVA=0x0386FF70  token=0x6000094  System.Void Internal_CreateAnimationClip(UnityEngine.AnimationClip self)
  RVA=0x032ECC40  token=0x6000095  System.Void SampleAnimation(UnityEngine.GameObject go, System.Single time)
  RVA=0x03435540  token=0x6000096  System.Void SampleAnimationFast(UnityEngine.GameObject go, System.Single time)
  RVA=0x032ECD20  token=0x6000097  System.Void SampleAnimation(UnityEngine.GameObject go, UnityEngine.AnimationClip clip, System.Single inTime, UnityEngine.WrapMode wrapMode)
  RVA=0x034354D0  token=0x6000098  System.Void SampleAnimationFast(UnityEngine.GameObject go, UnityEngine.AnimationClip clip, System.Single inTime, UnityEngine.WrapMode wrapMode)
  RVA=0x0B395578  token=0x60000A0  System.Void SetCurve(System.String relativePath, System.Type type, System.String propertyName, UnityEngine.AnimationCurve curve)
  RVA=0x0B3954BC  token=0x60000A1  System.Boolean GetAnimatorFloatCurve(System.String propertyName, UnityEngine.AnimationCurve curve)
  RVA=0x0B395488  token=0x60000A2  System.Void EnsureQuaternionContinuity()
  RVA=0x0B395454  token=0x60000A3  System.Void ClearCurves()
  RVA=0x0B395544  token=0x60000A4  System.UInt32 GetRuntimeMemorySize()
  RVA=0x0B3953E8  token=0x60000B5  System.Void AddEvent(UnityEngine.AnimationEvent evt)
  RVA=0x0B3953A4  token=0x60000B6  System.Void AddEventInternal(System.Object evt)
  RVA=0x0B3955DC  token=0x60000B9  System.Void SetEventsInternal(System.Array value)
  RVA=0x0B395510  token=0x60000BA  System.Array GetEventsInternal()
  RVA=0x0B39579C  token=0x60000BB  System.Void get_localBounds_Injected(UnityEngine.Bounds& ret)
  RVA=0x0B3959B4  token=0x60000BC  System.Void set_localBounds_Injected(UnityEngine.Bounds& value)
END_CLASS

CLASS: UnityEngine.AvatarTarget
TYPE:  sealed struct
TOKEN: 0x2000015
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AvatarTarget        Root  // const
  public    static  UnityEngine.AvatarTarget        Body  // const
  public    static  UnityEngine.AvatarTarget        LeftFoot  // const
  public    static  UnityEngine.AvatarTarget        RightFoot  // const
  public    static  UnityEngine.AvatarTarget        LeftHand  // const
  public    static  UnityEngine.AvatarTarget        RightHand  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AvatarIKGoal
TYPE:  sealed struct
TOKEN: 0x2000016
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AvatarIKGoal        LeftFoot  // const
  public    static  UnityEngine.AvatarIKGoal        RightFoot  // const
  public    static  UnityEngine.AvatarIKGoal        LeftHand  // const
  public    static  UnityEngine.AvatarIKGoal        RightHand  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AvatarIKHint
TYPE:  sealed struct
TOKEN: 0x2000017
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AvatarIKHint        LeftKnee  // const
  public    static  UnityEngine.AvatarIKHint        RightKnee  // const
  public    static  UnityEngine.AvatarIKHint        LeftElbow  // const
  public    static  UnityEngine.AvatarIKHint        RightElbow  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AnimatorControllerParameterType
TYPE:  sealed struct
TOKEN: 0x2000018
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AnimatorControllerParameterTypeFloat  // const
  public    static  UnityEngine.AnimatorControllerParameterTypeInt  // const
  public    static  UnityEngine.AnimatorControllerParameterTypeBool  // const
  public    static  UnityEngine.AnimatorControllerParameterTypeTrigger  // const
METHODS:
END_CLASS

CLASS: UnityEngine.StateInfoIndex
TYPE:  sealed struct
TOKEN: 0x2000019
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.StateInfoIndex      CurrentState  // const
  public    static  UnityEngine.StateInfoIndex      NextState  // const
  public    static  UnityEngine.StateInfoIndex      ExitState  // const
  public    static  UnityEngine.StateInfoIndex      InterruptedState  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AnimationBlendStyle
TYPE:  sealed struct
TOKEN: 0x200001A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AnimationBlendStyle Linear  // const
  public    static  UnityEngine.AnimationBlendStyle CubicIn  // const
  public    static  UnityEngine.AnimationBlendStyle HermiteCubic  // const
  public    static  UnityEngine.AnimationBlendStyle Sinusoidal  // const
  public    static  UnityEngine.AnimationBlendStyle CubicInOut  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AnimatorTransitionInterruptionSource
TYPE:  sealed struct
TOKEN: 0x200001B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AnimatorTransitionInterruptionSourceNone  // const
  public    static  UnityEngine.AnimatorTransitionInterruptionSourceCurrent  // const
  public    static  UnityEngine.AnimatorTransitionInterruptionSourceNext  // const
  public    static  UnityEngine.AnimatorTransitionInterruptionSourceCurrentThenNext  // const
  public    static  UnityEngine.AnimatorTransitionInterruptionSourceNextThenCurrent  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AnimatorRecorderMode
TYPE:  sealed struct
TOKEN: 0x200001C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AnimatorRecorderModeOffline  // const
  public    static  UnityEngine.AnimatorRecorderModePlayback  // const
  public    static  UnityEngine.AnimatorRecorderModeRecord  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AnimatorUroMode
TYPE:  sealed struct
TOKEN: 0x200001D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AnimatorUroMode     Origin  // const
  public    static  UnityEngine.AnimatorUroMode     Predict  // const
  public    static  UnityEngine.AnimatorUroMode     PredictWithTick  // const
  public    static  UnityEngine.AnimatorUroMode     PredictWithTickAndRootMotion  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AnimatorCullingMode
TYPE:  sealed struct
TOKEN: 0x200001E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AnimatorCullingMode AlwaysAnimate  // const
  public    static  UnityEngine.AnimatorCullingMode CullUpdateTransforms  // const
  public    static  UnityEngine.AnimatorCullingMode CullCompletely  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AnimatorUpdateMode
TYPE:  sealed struct
TOKEN: 0x200001F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AnimatorUpdateMode  Normal  // const
  public    static  UnityEngine.AnimatorUpdateMode  AnimatePhysics  // const
  public    static  UnityEngine.AnimatorUpdateMode  UnscaledTime  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AnimatorJobDispatchMode
TYPE:  sealed struct
TOKEN: 0x2000020
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AnimatorJobDispatchModeDefault  // const
  public    static  UnityEngine.AnimatorJobDispatchModeForceMainThread  // const
  public    static  UnityEngine.AnimatorJobDispatchModePreUpdate  // const
  public    static  UnityEngine.AnimatorJobDispatchModeCrossFrame  // const
  public    static  UnityEngine.AnimatorJobDispatchModeCrossFrameLateUpdate  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AnimatorLOD
TYPE:  sealed struct
TOKEN: 0x2000021
SIZE:  0x2C
FIELDS:
  public            System.Single                   worldAbsoluteDistance  // 0x10
  public            System.Single                   maxDeltaTime  // 0x14
  public            System.Int32                    framesPerUpdate  // 0x18
  public            System.Single                   maxBlendDeltaTime  // 0x1c
  public            System.Int32                    framesPerWrite  // 0x20
  public            System.Int32                    delayFrame  // 0x24
  public            UnityEngine.AnimatorUroMode     uroMode  // 0x28
METHODS:
  RVA=0x04DBA920  token=0x60000BD  System.Void .ctor(System.Single worldAbsoluteDistance, System.Int32 framesPerUpdate, System.Single maxDeltaTime, System.Int32 framesPerWrite, System.Single maxBlendDeltaTime, UnityEngine.AnimatorUroMode uroMode, System.Int32 delayFrame)
END_CLASS

CLASS: UnityEngine.AnimationTransformRWBufferHandle
TYPE:  sealed struct
TOKEN: 0x2000022
SIZE:  0x80
FIELDS:
  public            System.UInt16                   count  // 0x10
  public            System.UInt16                   invalidCount  // 0x12
  public            System.IntPtr                   validTransformIndexsPtr  // 0x18
  public            System.IntPtr                   invalidTransformIndexsPtr  // 0x20
  public            System.IntPtr                   localPositionsPtr  // 0x28
  public            System.IntPtr                   localRotationsPtr  // 0x30
  public            System.IntPtr                   localScalesPtr  // 0x38
  public            System.IntPtr                   positionsPtr  // 0x40
  public            System.IntPtr                   rotationsPtr  // 0x48
  public            System.IntPtr                   scalesPtr  // 0x50
  public            System.IntPtr                   physicsRatioPtr  // 0x58
  public            System.IntPtr                   writeFlagPtr  // 0x60
  public            System.IntPtr                   relativePostionPtr  // 0x68
  public            System.IntPtr                   relativeRotationPtr  // 0x70
  public            System.IntPtr                   relativeScalePtr  // 0x78
METHODS:
END_CLASS

CLASS: UnityEngine.HGAnimParameterSGHandle
TYPE:  sealed struct
TOKEN: 0x2000023
SIZE:  0x58
FIELDS:
  public            System.Int32                    errorCode  // 0x10
  public            System.UInt16                   floatParameterCount  // 0x14
  public            System.UInt16                   boolParameterCount  // 0x16
  public            System.UInt16                   intParameterCount  // 0x18
  public            System.UInt16                   layerCount  // 0x1a
  public            System.IntPtr                   floatParameterNamesPtr  // 0x20
  public            System.IntPtr                   floatParameterValuesPtr  // 0x28
  public            System.IntPtr                   boolParameterNamesPtr  // 0x30
  public            System.IntPtr                   boolParameterValuesPtr  // 0x38
  public            System.IntPtr                   intParameterNamesPtr  // 0x40
  public            System.IntPtr                   intParameterValuesPtr  // 0x48
  public            System.IntPtr                   layerWeightPtr  // 0x50
METHODS:
END_CLASS

CLASS: UnityEngine.AnimFrameSyncMode
TYPE:  sealed struct
TOKEN: 0x2000024
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AnimFrameSyncMode   FrameSyncInRootPlayable  // const
  public    static  UnityEngine.AnimFrameSyncMode   FrameSyncInBaseLayerState  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AnimatorClipInfo
TYPE:  sealed struct
TOKEN: 0x2000025
SIZE:  0x18
FIELDS:
  private           System.Int32                    m_ClipInstanceID  // 0x10
  private           System.Single                   m_Weight  // 0x14
PROPERTIES:
  clip  get=0x0B3985FC
  weight  get=0x04DBA740
METHODS:
  RVA=0x0B3985C8  token=0x60000C0  UnityEngine.AnimationClip InstanceIDToAnimationClipPPtr(System.Int32 instanceID)
END_CLASS

CLASS: UnityEngine.AnimatorStateInfo
TYPE:  sealed struct
TOKEN: 0x2000026
SIZE:  0x44
FIELDS:
  private           System.Int32                    m_Name  // 0x10
  private           System.Int32                    m_Path  // 0x14
  private           System.Int32                    m_FullPath  // 0x18
  private           System.Single                   m_NormalizedTime  // 0x1c
  private           System.Single                   m_Length  // 0x20
  private           System.Single                   m_Speed  // 0x24
  private           System.Single                   m_SpeedMultiplier  // 0x28
  private           System.Int32                    m_Tag  // 0x2c
  private           System.Int32                    m_Loop  // 0x30
  private           System.Int32                    m_SyncGroup  // 0x34
  private           System.Int32                    m_SyncGroupRole  // 0x38
  private           System.Int32                    m_CullingMode  // 0x3c
  private           System.Int32                    m_SourceMsg  // 0x40
PROPERTIES:
  fullPathHash  get=0x04D9CF30
  shortNameHash  get=0x04DBA750
  normalizedTime  get=0x04DBA950
  length  get=0x04DBA720
  tagHash  get=0x04D9B610
  loop  get=0x0343C970
METHODS:
  RVA=0x03F7F9F0  token=0x60000C1  System.Boolean IsName(System.String name)
  RVA=0x03B071C0  token=0x60000C7  System.Boolean IsTag(System.String tag)
END_CLASS

CLASS: UnityEngine.AnimatorStateRootMotionInfo
TYPE:  sealed struct
TOKEN: 0x2000027
SIZE:  0x7C
FIELDS:
  private           System.Int32                    m_NameHashLeft  // 0x10
  private           UnityEngine.Vector3             m_DeltaPositionLeft  // 0x14
  private           UnityEngine.Quaternion          m_DeltaRotationLeft  // 0x20
  private           System.Int32                    m_NameHashRight  // 0x30
  private           UnityEngine.Vector3             m_DeltaPositionRight  // 0x34
  private           UnityEngine.Quaternion          m_DeltaRotationRight  // 0x40
  private           System.Single                   m_TransitionWeight  // 0x50
  private           System.Single                   m_DeltaTime  // 0x54
  private           System.Single                   m_InterruptedTransitionWeight  // 0x58
  private           System.Int32                    m_NameHashinterrupted  // 0x5c
  private           UnityEngine.Vector3             m_DeltaPositioninterrupted  // 0x60
  private           UnityEngine.Quaternion          m_DeltaRotationinterrupted  // 0x6c
METHODS:
END_CLASS

CLASS: UnityEngine.AnimatorTransitionInfo
TYPE:  sealed struct
TOKEN: 0x2000028
SIZE:  0x30
FIELDS:
  private           System.Int32                    m_FullPath  // 0x10
  private           System.Int32                    m_UserName  // 0x14
  private           System.Int32                    m_Name  // 0x18
  private           System.Boolean                  m_HasFixedDuration  // 0x1c
  private           System.Single                   m_Duration  // 0x20
  private           System.Single                   m_NormalizedTime  // 0x24
  private           System.Boolean                  m_AnyState  // 0x28
  private           System.Int32                    m_TransitionType  // 0x2c
PROPERTIES:
  nameHash  get=0x04D9CF30
METHODS:
  RVA=0x0B3999F0  token=0x60000C9  System.Boolean IsUserName(System.String name)
END_CLASS

CLASS: UnityEngine.MatchTargetWeightMask
TYPE:  sealed struct
TOKEN: 0x2000029
SIZE:  0x20
FIELDS:
  private           UnityEngine.Vector3             m_PositionXYZWeight  // 0x10
  private           System.Single                   m_RotationWeight  // 0x1c
METHODS:
  RVA=0x04D8C1B0  token=0x60000CB  System.Void .ctor(UnityEngine.Vector3 positionXYZWeight, System.Single rotationWeight)
END_CLASS

CLASS: UnityEngine.Animator
TYPE:  class
TOKEN: 0x200002A
SIZE:  0x48
EXTENDS: UnityEngine.Behaviour
FIELDS:
  private           UnityEngine.Animator.AnimationEventCallbackonAnimationEvent  // 0x18
  private           UnityEngine.Animator.AnimationEventCallbackOptonAnimationEventOpt  // 0x20
  private           UnityEngine.Animator.AnimatorStateChangeCallbackonAnimatorStateEnter  // 0x28
  private           UnityEngine.Animator.AnimatorStateChangeCallbackonAnimatorStateExit  // 0x30
  private           UnityEngine.Animator.AnimatorStateChangeCallbackonAnimatorStatePreExit  // 0x38
  private           UnityEngine.Animator.AniamtorCommonCallbackafterAnimatorUpdateGraphCB  // 0x40
PROPERTIES:
  fastAnimationEventsOpt  get=0x0B39E1E8  set=0x0378DC80
  callBackAfterUpdateGraph  get=0x0B39E14C  set=0x0B39ECDC
  fastAnimationEvents  get=0x0B39E21C  set=0x0378DCD0
  notifyAnimatorStateEnterAndExitEvent  get=0x0B39E604  set=0x0408AA50
  keepStateOnChangeActive  get=0x0B39E430  set=0x0B39EDF4
  pausePlayableOnChangeActive  get=0x0B39E638  set=0x0B39EFCC
  resetControllerPlayableOnChangeActive  get=0x0B39E7C8  set=0x0B39F054
  isOptimizable  get=0x0B39E388
  isHuman  get=0x03FA6110
  hasRootMotion  get=0x0B39E2B8
  isRootPositionOrRotationControlledByCurves  get=0x0B39E3BC
  humanScale  get=0x0B39E2EC
  isInitialized  get=0x0B39E320
  isVisible  get=0x033D0530
  updatedLastFrame  get=0x03503520
  enableLOD  get=0x0B39E180  set=0x036276B0
  LODLevel  get=0x02F937E0
  motionDeltaTime  get=0x0B39E5D0
  deltaPosition  get=0x0343D7F0
  deltaRotation  get=0x0343D8E0
  velocity  get=0x0343D690
  angularVelocity  get=0x0343D740
  rootPosition  get=0x0B39E874  set=0x0B39F0DC
  rootRotation  get=0x0B39E8E4  set=0x0B39F134
  applyRootMotion  get=0x0B39DF70  set=0x0B39EB68
  linearVelocityBlending  get=0x0B39E4CC  set=0x0B39EE7C
  animatePhysics  get=0x0B39DF24  set=0x0B39EB04
  updateMode  get=0x035BD460  set=0x0B39F18C
  hasTransformHierarchy  get=0x039C7C00
  allowConstantClipSamplingOptimization  get=0x0B39DEF0  set=0x0B39EAC0
  gravityWeight  get=0x0343D8A0
  bodyPosition  get=0x0B39E048  set=0x0B39EC04
  bodyPositionInternal  get=0x0B39E01C  set=0x0B39EBF0
  bodyRotation  get=0x0B39E108  set=0x0B39ECA0
  bodyRotationInternal  get=0x0B39E0DC  set=0x0B39EC8C
  stabilizeFeet  get=0x0B39E944  set=0x0B39F148
  layerCount  get=0x0322D150
  parameters  get=0x0378ADA0
  parameterCount  get=0x0378ADE0
  feetPivotActive  get=0x0B39E250  set=0x0B39ED64
  pivotWeight  get=0x0B39E6DC
  pivotPosition  get=0x0B39E6B0
  isMatchingTarget  get=0x0B39E354
  timeScale  get=0x03860A10  set=0x038602D0
  speed  get=0x0B39E910  set=0x03861540
  targetPosition  get=0x0B39E9F0
  targetRotation  get=0x0B39EA60
  avatarRoot  get=0x0B39DFA4
  cullingMode  get=0x035BD8B0  set=0x031DD660
  playbackTime  get=0x0B39E748  set=0x0B39F010
  recorderStartTime  get=0x0B39E7B0  set=0x041E1670
  recorderStopTime  get=0x0B39E7BC  set=0x041E1670
  recorderMode  get=0x0B39E77C
  runtimeAnimatorController  get=0x0300FA90  set=0x0322E3B0
  hasBoundPlayables  get=0x03796920
  avatar  get=0x039C99F0  set=0x03DD0120
  morphAvatar  get=0x0B39E59C  set=0x0B39EF88
  lodBoneMask  get=0x0B39E534  set=0x0B39EF00
  lodBoneMaskLevel  get=0x0B39E500  set=0x0B39EEC0
  playableGraph  get=0x0B39E710
  applyMorphPose  get=0x0B39DF3C  set=0x0B39EB24
  layersAffectMassCenter  get=0x0B39E464  set=0x0B39EE38
  leftFeetBottomHeight  get=0x0B39E498
  rightFeetBottomHeight  get=0x0B39E7FC
  supportsOnAnimatorMove  get=0x0B39E978
  logWarnings  get=0x0B39E568  set=0x0B39EF44
  enableParameterWarnings  get=0x0B39E1B4  set=0x0B39ED20
  fireEvents  get=0x0B39E284  set=0x0B39EDA8
  keepAnimatorControllerStateOnDisable  get=0x0B39E3F0  set=0x0B39EDEC
  keepAnimatorStateOnDisable  get=0x0B39E3FC  set=0x03DD00D0
  writeDefaultValuesOnDisable  get=0x0B39EA8C  set=0x0322E400
METHODS:
  RVA=0x0B39DEA4  token=0x60000CC  System.Void .ctor()
  RVA=0x045D15D0  token=0x60000CD  System.Void Internal_CallAnimatorStateChangeCallback(UnityEngine.Animator anim, UnityEngine.AnimatorStateInfo animatorStateInfo, System.Int32 layerIndex, System.Int32 methodIdx)
  RVA=0x0B39BC44  token=0x60000CE  System.Void Internal_CallAfterUpdateGraph(UnityEngine.Animator anim)
  RVA=0x04231DD0  token=0x60000CF  System.Boolean Internal_InvokeAnimationEventCallback(UnityEngine.Animator animator, UnityEngine.FastAnimationEvent fastAnimationEvent)
  RVA=0x03F32C60  token=0x60000D0  System.Boolean Internal_InvokeAnimationEventCallbackOpt(UnityEngine.Animator animator, UnityEngine.FastAnimationEventGroup fastAnimationEvent)
  RVA=0x042B4AE0  token=0x60000D1  System.Void SetCallbackAfterRootMotionStageCallback(UnityEngine.Animator.AniamtorCommonCallback cb)
  RVA=0x05392C40  token=0x60000D2  System.Void SetAnimationEventCallback(UnityEngine.Animator.AnimationEventCallback onAnimationEvent)
  RVA=0x05390F40  token=0x60000D3  System.Void SetAnimationEventCallbackOpt(UnityEngine.Animator.AnimationEventCallbackOpt onAnimationEvent)
  RVA=0x0408AA20  token=0x60000D4  System.Void SetNotifyAnimatorStateEnterAndExitEventCallBack(UnityEngine.Animator.AnimatorStateChangeCallback onAnimatorStateEnter, UnityEngine.Animator.AnimatorStateChangeCallback onAnimatorStateExit)
  RVA=0x046A91B0  token=0x60000D5  System.Void SetNotifyAnimatorStatePreExitEventCallBack(UnityEngine.Animator.AnimatorStateChangeCallback onAnimatorStatePreExit)
  RVA=0x03787AB0  token=0x60000EF  System.Boolean SetLODLevel(System.Int32 level)
  RVA=0x0B39B208  token=0x60000F0  System.Int32 GetCurrentLODLevel()
  RVA=0x033D1900  token=0x60000F1  UnityEngine.AnimatorLOD[] GetLODs()
  RVA=0x037878D0  token=0x60000F2  System.Void SetLODs(UnityEngine.AnimatorLOD[] lods)
  RVA=0x0B39C984  token=0x60000F3  System.Void SetConstraintEnableImmediately(System.Int32 nameHash)
  RVA=0x0B39C8F4  token=0x60000F4  System.Void SetConstraintDisableImmediately(System.Int32 nameHash)
  RVA=0x0B39C934  token=0x60000F5  System.Void SetConstraintEnableBlendIn(System.Int32 nameHash, System.Single duration)
  RVA=0x0B39C8A4  token=0x60000F6  System.Void SetConstraintDisableBlendOut(System.Int32 nameHash, System.Single duration)
  RVA=0x03434620  token=0x60000F7  System.Void BecameVisible()
  RVA=0x0464F170  token=0x60000F8  System.Void SetAnimatorJobOptEnable(System.Boolean enabel)
  RVA=0x0464F1B0  token=0x60000F9  System.Void SetAnimatorJobOptFunc(System.Int32 value)
  RVA=0x0464F1F0  token=0x60000FA  System.Void SetAnimatorWriteTransformOpt(System.Boolean value)
  RVA=0x0464F230  token=0x60000FB  System.Void SetGlobalAnimatorClipBindingsOpt(System.Boolean value)
  RVA=0x0464F270  token=0x60000FC  System.Void SetGlobalAnimatorClipBindingsConstantOpt(System.Boolean value)
  RVA=0x0464F2B0  token=0x60000FD  System.Void SetGlobalAnimatorClipBindingsOptByAvatar(System.Boolean value)
  RVA=0x0B39C644  token=0x60000FE  System.Void SetAnimatorUroMaxDeltaTimePerframe(System.Single value)
  RVA=0x0464F2F0  token=0x60000FF  System.Void SetGlobalAnimatorCacheClipBindingsOpt(System.Boolean value)
  RVA=0x0B39ABE0  token=0x6000100  System.Single GetAnimatorUroMaxDeltaTimePerframe()
  RVA=0x0B39C610  token=0x6000101  System.Void SetAnimatorMainThreadJobCount(System.Int32 value)
  RVA=0x0B39D8EC  token=0x6000102  System.Void SetPrepareFrameJobBatchCountOpt(System.Int32 value)
  RVA=0x0B39D920  token=0x6000103  System.Void SetProcessAnimJobBatchCountOpt(System.Int32 value)
  RVA=0x0464F330  token=0x6000104  System.Void SetAnimatorEnableProcessOneStage(System.Boolean value)
  RVA=0x0B39C574  token=0x6000105  System.Void SetAnimatorEnableApplyOnMoveInJob(System.Boolean value)
  RVA=0x0B39C5DC  token=0x6000106  System.Void SetAnimatorEnableRequireEventsAndBehaviour(System.Boolean value)
  RVA=0x0B39C680  token=0x6000107  System.Void SetApplyBuildInRootMotionInJob(System.Boolean enable)
  RVA=0x0464F370  token=0x6000108  System.Void SetAnimatorForbbidenApplyOnMoveCb(System.Boolean value)
  RVA=0x0B39C5A8  token=0x6000109  System.Void SetAnimatorEnableCrossFrameJobs(System.Boolean enabel)
  RVA=0x0378B130  token=0x600010A  System.Void SetAbilityLevel(System.Int32 abilityLevel)
  RVA=0x02F937A0  token=0x600010B  System.Int32 GetAbilityLevel()
  RVA=0x0B39DBD4  token=0x600010C  System.Void SetSampleClipCacheCreatePreFrameCount(System.Int32 value)
  RVA=0x0B39DC08  token=0x600010D  System.Void SetSampleClipCacheMaxCount(System.Int32 value)
  RVA=0x0378AAA0  token=0x600010E  System.Void SetLayerMeshSpace(System.Int32 layerIndex, System.Boolean enable)
  RVA=0x0B39B7D0  token=0x600010F  System.Boolean GetLayerMeshSpace(System.Int32 layerIndex)
  RVA=0x0B39BD00  token=0x6000110  System.Void Internal_SetLayerMaskBoneWeights(System.Int32 layerIndex, System.IntPtr bonePathHashes, System.IntPtr weights, System.Int32 count)
  RVA=0x0B399C30  token=0x6000111  System.Void ClearLayerMaskBoneWeights(System.Int32 layerIndex)
  RVA=0x0B39D1D0  token=0x6000112  System.Void SetLayerMaskBoneWeights(System.Int32 layerIndex, System.Int32[] bonePathHashes, System.Single[] weights)
  RVA=0x0B39D318  token=0x6000113  System.Void SetLayerMaskBoneWeights(System.Int32 layerIndex, Unity.Collections.NativeArray<System.Int32> bonePathHashes, Unity.Collections.NativeArray<System.Single> weights)
  RVA=0x0322E550  token=0x6000114  System.Void SetRetargetScaleEnabled(System.Boolean enabled)
  RVA=0x0B39BA90  token=0x6000115  System.Boolean GetRetargetScaleEnabled()
  RVA=0x0B39D954  token=0x6000116  System.Void SetRetargetScaleByAvatar(UnityEngine.Avatar referenceAvatar, System.String[] boneNames)
  RVA=0x0B39BDC0  token=0x6000117  System.Void Internal_SetRetargetScaleByAvatar(UnityEngine.Avatar referenceAvatar, System.Int32[] boneNameHashes, System.Int32 count)
  RVA=0x0B39DA98  token=0x6000118  System.Void SetRetargetScale(System.String[] boneNames, UnityEngine.Vector3 scaleFactor, UnityEngine.Animator.RetargetBoneMode[] boneModes)
  RVA=0x0B39BE88  token=0x6000119  System.Void Internal_SetRetargetScale(System.Int32[] boneNameHashes, System.Int32 count, UnityEngine.Vector3 scaleFactor, System.Int32[] boneModes)
  RVA=0x0B39DA54  token=0x600011A  System.Void SetRetargetScaleWeight(System.Single weight)
  RVA=0x0B39BAC4  token=0x600011B  System.Single GetRetargetScaleWeight()
  RVA=0x0B39C9C4  token=0x600011C  System.Void SetControllerDefaultValues(System.Boolean value)
  RVA=0x053925AC  token=0x600011D  System.Single GetFloat(System.String name)
  RVA=0x05390B1C  token=0x600011E  System.Single GetFloat(System.Int32 id)
  RVA=0x0B39B358  token=0x600011F  System.Single GetFloatFromTransition(System.Int32 layerIndex, System.Int32 id)
  RVA=0x053944F4  token=0x6000120  System.Single GetFloatFromTransitionWithDefault(System.Int32 layerIndex, System.Int32 id, System.Single defValue)
  RVA=0x0B39CD4C  token=0x6000121  System.Void SetFloat(System.String name, System.Single value)
  RVA=0x0B39CD3C  token=0x6000122  System.Void SetFloat(System.String name, System.Single value, System.Single dampTime, System.Single deltaTime)
  RVA=0x053925B8  token=0x6000123  System.Void SetFloat(System.Int32 id, System.Single value)
  RVA=0x0B39CD54  token=0x6000124  System.Void SetFloat(System.Int32 id, System.Single value, System.Single dampTime, System.Single deltaTime)
  RVA=0x0B39B040  token=0x6000125  System.Boolean GetBool(System.String name)
  RVA=0x05393DC4  token=0x6000126  System.Boolean GetBool(System.Int32 id)
  RVA=0x05397548  token=0x6000127  System.Void SetBool(System.String name, System.Boolean value)
  RVA=0x0539463C  token=0x6000128  System.Void SetBool(System.Int32 id, System.Boolean value)
  RVA=0x0B39B7C4  token=0x6000129  System.Int32 GetInteger(System.String name)
  RVA=0x053925A0  token=0x600012A  System.Int32 GetInteger(System.Int32 id)
  RVA=0x0B39D1C8  token=0x600012B  System.Void SetInteger(System.String name, System.Int32 value)
  RVA=0x05396E38  token=0x600012C  System.Void SetInteger(System.Int32 id, System.Int32 value)
  RVA=0x05393888  token=0x600012D  System.Void SetTrigger(System.String name)
  RVA=0x0B39DC8C  token=0x600012E  System.Void SetTrigger(System.Int32 id)
  RVA=0x05393880  token=0x600012F  System.Void ResetTrigger(System.String name)
  RVA=0x05390B14  token=0x6000130  System.Void ResetTrigger(System.Int32 id)
  RVA=0x0B39C060  token=0x6000131  System.Boolean IsParameterControlledByCurve(System.String name)
  RVA=0x0B39C06C  token=0x6000132  System.Boolean IsParameterControlledByCurve(System.Int32 id)
  RVA=0x0B39C530  token=0x6000145  System.Void SetAllowConstantClipSamplingOpt(System.Boolean enable)
  RVA=0x0B39B698  token=0x6000151  UnityEngine.Vector3 GetIKPosition(UnityEngine.AvatarIKGoal goal)
  RVA=0x0B39B3B4  token=0x6000152  UnityEngine.Vector3 GetGoalPosition(UnityEngine.AvatarIKGoal goal)
  RVA=0x0B39D098  token=0x6000153  System.Void SetIKPosition(UnityEngine.AvatarIKGoal goal, UnityEngine.Vector3 goalPosition)
  RVA=0x0B39CDF8  token=0x6000154  System.Void SetGoalPosition(UnityEngine.AvatarIKGoal goal, UnityEngine.Vector3 goalPosition)
  RVA=0x0B39B728  token=0x6000155  UnityEngine.Quaternion GetIKRotation(UnityEngine.AvatarIKGoal goal)
  RVA=0x0B39B434  token=0x6000156  UnityEngine.Quaternion GetGoalRotation(UnityEngine.AvatarIKGoal goal)
  RVA=0x0B39D12C  token=0x6000157  System.Void SetIKRotation(UnityEngine.AvatarIKGoal goal, UnityEngine.Quaternion goalRotation)
  RVA=0x0B39CE5C  token=0x6000158  System.Void SetGoalRotation(UnityEngine.AvatarIKGoal goal, UnityEngine.Quaternion goalRotation)
  RVA=0x0B39B668  token=0x6000159  System.Single GetIKPositionWeight(UnityEngine.AvatarIKGoal goal)
  RVA=0x0B39B464  token=0x600015A  System.Single GetGoalWeightPosition(UnityEngine.AvatarIKGoal goal)
  RVA=0x0B39D058  token=0x600015B  System.Void SetIKPositionWeight(UnityEngine.AvatarIKGoal goal, System.Single value)
  RVA=0x0B39CE70  token=0x600015C  System.Void SetGoalWeightPosition(UnityEngine.AvatarIKGoal goal, System.Single value)
  RVA=0x0B39B6F8  token=0x600015D  System.Single GetIKRotationWeight(UnityEngine.AvatarIKGoal goal)
  RVA=0x0B39B4A4  token=0x600015E  System.Single GetGoalWeightRotation(UnityEngine.AvatarIKGoal goal)
  RVA=0x0B39D0EC  token=0x600015F  System.Void SetIKRotationWeight(UnityEngine.AvatarIKGoal goal, System.Single value)
  RVA=0x0B39CEC0  token=0x6000160  System.Void SetGoalWeightRotation(UnityEngine.AvatarIKGoal goal, System.Single value)
  RVA=0x0B39B608  token=0x6000161  UnityEngine.Vector3 GetIKHintPosition(UnityEngine.AvatarIKHint hint)
  RVA=0x0B39B568  token=0x6000162  UnityEngine.Vector3 GetHintPosition(UnityEngine.AvatarIKHint hint)
  RVA=0x0B39D004  token=0x6000163  System.Void SetIKHintPosition(UnityEngine.AvatarIKHint hint, UnityEngine.Vector3 hintPosition)
  RVA=0x0B39CF60  token=0x6000164  System.Void SetHintPosition(UnityEngine.AvatarIKHint hint, UnityEngine.Vector3 hintPosition)
  RVA=0x0B39B5D8  token=0x6000165  System.Single GetIKHintPositionWeight(UnityEngine.AvatarIKHint hint)
  RVA=0x0B39B598  token=0x6000166  System.Single GetHintWeightPosition(UnityEngine.AvatarIKHint hint)
  RVA=0x0B39CFC4  token=0x6000167  System.Void SetIKHintPositionWeight(UnityEngine.AvatarIKHint hint, System.Single value)
  RVA=0x0B39CF74  token=0x6000168  System.Void SetHintWeightPosition(UnityEngine.AvatarIKHint hint, System.Single value)
  RVA=0x0B39D448  token=0x6000169  System.Void SetLookAtPosition(UnityEngine.Vector3 lookAtPosition)
  RVA=0x0B39D434  token=0x600016A  System.Void SetLookAtPositionInternal(UnityEngine.Vector3 lookAtPosition)
  RVA=0x0B39D6B8  token=0x600016B  System.Void SetLookAtWeight(System.Single weight)
  RVA=0x0B39D4F4  token=0x600016C  System.Void SetLookAtWeight(System.Single weight, System.Single bodyWeight)
  RVA=0x0B39D648  token=0x600016D  System.Void SetLookAtWeight(System.Single weight, System.Single bodyWeight, System.Single headWeight)
  RVA=0x0B39D558  token=0x600016E  System.Void SetLookAtWeight(System.Single weight, System.Single bodyWeight, System.Single headWeight, System.Single eyesWeight)
  RVA=0x0B39D5D0  token=0x600016F  System.Void SetLookAtWeight(System.Single weight, System.Single bodyWeight, System.Single headWeight, System.Single eyesWeight, System.Single clampWeight)
  RVA=0x0B39D48C  token=0x6000170  System.Void SetLookAtWeightInternal(System.Single weight, System.Single bodyWeight, System.Single headWeight, System.Single eyesWeight, System.Single clampWeight)
  RVA=0x0B39C7B8  token=0x6000171  System.Void SetBoneLocalRotation(UnityEngine.HumanBodyBones humanBoneId, UnityEngine.Quaternion rotation)
  RVA=0x0B39C7A4  token=0x6000172  System.Void SetBoneLocalRotationInternal(System.Int32 humanBoneId, UnityEngine.Quaternion rotation)
  RVA=0x05395CA8  token=0x6000173  System.Void SetTransformHierarchy(System.Boolean bHas)
  RVA=0x04B0E8C0  token=0x6000174  System.Void SetTransformHierarchyInternal(System.Boolean bHas)
  RVA=0x0B39AD48  token=0x6000175  System.Boolean GetBoneGlobalTRS(System.Int32 avatarAssetBoneID, UnityEngine.AnimationTransformf& trs)
  RVA=0x0B39ACF8  token=0x6000176  System.Boolean GetBoneGlobalTRSInternal(System.Int32 avatarAssetBoneID, UnityEngine.AnimationTransformf& trs)
  RVA=0x0B39AEAC  token=0x6000177  UnityEngine.AnimationTransformf GetBoneRootTRS(System.Int32 avatarAssetBoneID)
  RVA=0x0B39AE74  token=0x6000178  UnityEngine.AnimationTransformf GetBoneRootTRSInternal(System.Int32 avatarAssetBoneID)
  RVA=0x0B39ADDC  token=0x6000179  UnityEngine.AnimationTransformf GetBoneLocalTRS(System.Int32 avatarAssetBoneID)
  RVA=0x0B39ADA4  token=0x600017A  UnityEngine.AnimationTransformf GetBoneLocalTRSInternal(System.Int32 avatarAssetBoneID)
  RVA=0x0B39CA08  token=0x600017B  System.Void SetCorrectiveBoneData(UnityEngine.HGCorrectiveBoneData bindings)
  RVA=0x0B39CB68  token=0x600017C  System.Void SetEnableCorrectiveBoneSystem(System.Boolean enable)
  RVA=0x0B39C710  token=0x600017D  System.Void SetBlendProfileData(UnityEngine.HGAvatarBlendProfile bindings)
  RVA=0x0322E650  token=0x600017E  System.Void SetEnableAvatarBlendProfile(System.Boolean enable)
  RVA=0x03788050  token=0x600017F  System.Void SetSequenceLayerSkipTimeThreshold(System.Single value)
  RVA=0x0B39BAF8  token=0x6000180  System.Single GetSequenceLayerSkipTimeThreshold()
  RVA=0x0B39A92C  token=0x6000181  System.Void ForceUpdateCorrectiveBoneDataInEditor()
  RVA=0x0B39C6CC  token=0x6000182  System.Void SetBakedBindingData(UnityEngine.HGAnimationSetBindings bindings)
  RVA=0x0B39A844  token=0x6000183  System.Void EnableBakedBindingData(System.Boolean enable)
  RVA=0x0B39C80C  token=0x6000184  System.Boolean SetClipByIndex(UnityEngine.AnimationClip overrideClip, System.Int32 index)
  RVA=0x0B39DDE8  token=0x6000185  System.Void UnloadClipsNoRebind(UnityEngine.AnimationClip[] overrideClip, System.Boolean clearCache)
  RVA=0x0B39C478  token=0x6000186  System.Void ReloadClipsNoRebind(UnityEngine.AnimationClip[] overrideClip)
  RVA=0x03892500  token=0x6000187  System.Void CaptureLastHumanPose()
  RVA=0x0322E310  token=0x6000188  System.Void SetWriteTransformDefAsPostProcess(System.Boolean enable)
  RVA=0x0B39DC94  token=0x6000189  System.Void SetWriteTransformDefAsPostProcessSkipOpt(System.Boolean enable)
  RVA=0x0408AAA0  token=0x600018A  System.Void SetAnimatorJobDispatchMode(UnityEngine.AnimatorJobDispatchMode mode)
  RVA=0x0B39AA58  token=0x600018B  UnityEngine.AnimatorJobDispatchMode GetAnimatorJobDispatchMode()
  RVA=0x0B399A14  token=0x600018C  System.Boolean AddToFrameSyncGroup(System.String groupName, System.Boolean isMaster, UnityEngine.AnimFrameSyncMode syncMode)
  RVA=0x0B39C4BC  token=0x600018D  System.Boolean RemoveFromFrameSyncGroup()
  RVA=0x0B39CB24  token=0x600018E  System.Void SetEnableCollisionConstraintSolver(System.Boolean enable)
  RVA=0x0B39CBAC  token=0x600018F  System.Void SetEnableDefaultValuesForceUseDefaultPose(System.Boolean enable)
  RVA=0x0B39D8A8  token=0x6000190  System.Void SetPoseDriverSystemWeight(System.Single weight)
  RVA=0x0B39B9D4  token=0x6000191  System.Single GetPoseDriverSystemWeight()
  RVA=0x0B39BF74  token=0x6000192  System.Boolean IsEnableDefaultValuesForceUseDefaultPose()
  RVA=0x03FA6060  token=0x6000193  System.Void SetEnableConstantOptByDefaultPose(System.Boolean enable)
  RVA=0x0B39BF40  token=0x6000194  System.Boolean IsEnableConstantOptByDefaultPose()
  RVA=0x0B39CC34  token=0x6000195  System.Void SetEnableWriteDefaultValues(System.Boolean enable)
  RVA=0x0B39CAE0  token=0x6000196  System.Void SetEnableClipBindingOpt(System.Boolean enable)
  RVA=0x0B39CBF0  token=0x6000197  System.Void SetEnableOptBindingByAvatar(System.Boolean enable)
  RVA=0x0464E5D0  token=0x6000198  System.Boolean AddClipsToBindingsNoGrown(UnityEngine.AnimationClip[] overrideClip, System.Int32 nameHash)
  RVA=0x0322E360  token=0x6000199  System.Void SetDynamicBindingReserveCount(System.Int32 count)
  RVA=0x0B39CA4C  token=0x600019A  System.Void SetDynamicBindingReserveCountByTag(System.Int32 stringHash, System.Int32 count)
  RVA=0x0B39C430  token=0x600019B  System.Void ReCreatePlayableMemory()
  RVA=0x0B39CD64  token=0x600019C  System.Void SetForceNonConstantBindingTransforms(UnityEngine.Transform[] transforms)
  RVA=0x0B399D60  token=0x600019D  UnityEngine.AnimationTransformRWBufferHandle CreateClothBindings(UnityEngine.Transform[] transformAccessArray)
  RVA=0x0B399CC4  token=0x600019E  UnityEngine.AnimationTransformRWBufferHandle CreateClothBindingsByNameLst(System.String[] transformPathArray)
  RVA=0x0B39A7DC  token=0x600019F  System.Void DestroyClothBindings()
  RVA=0x0B39A888  token=0x60001A0  System.Void EnableClothBindings()
  RVA=0x0B39A810  token=0x60001A1  System.Void DisableClothBindings()
  RVA=0x0B39C860  token=0x60001A2  System.Void SetClothBindingsEnableWorldSpace(System.Boolean value)
  RVA=0x0B399AAC  token=0x60001A3  System.Boolean CheckClothBindingsEnableWorldSpace()
  RVA=0x0B39D710  token=0x60001A4  System.Void SetMorphBindingsEnableWorldSpace(System.Boolean value)
  RVA=0x0B399BC8  token=0x60001A5  System.Boolean CheckMorphBindingsEnableWorldSpace()
  RVA=0x03B6A110  token=0x60001A6  UnityEngine.AnimationTransformRWBufferHandle CreateMorphBindings(UnityEngine.Transform[] transformAccessArray)
  RVA=0x0B399DFC  token=0x60001A7  UnityEngine.AnimationTransformRWBufferHandle CreateMorphBindingsByNameLst(System.String[] transformPathArray)
  RVA=0x0B399B84  token=0x60001A8  System.Boolean CheckMorphBindingValid(UnityEngine.AnimationTransformRWBufferHandle& handle)
  RVA=0x03B6A0D0  token=0x60001A9  System.Void EnableMorphBindings()
  RVA=0x04B6CCA0  token=0x60001AA  System.Void DisableMorphBindings()
  RVA=0x044E35C0  token=0x60001AB  System.Void SetOptimizedAvatarDataSet(System.Boolean optimized)
  RVA=0x0B39CA9C  token=0x60001AC  System.Void SetEnabelMorphBindingWriteInLateUpdate(System.Boolean enable)
  RVA=0x0B39AC0C  token=0x60001AD  UnityEngine.ScriptableObject GetBehaviour(System.Type type)
  RVA=-1  // generic def  token=0x60001AE  T GetBehaviour()
  RVA=-1  // generic def  token=0x60001AF  T[] ConvertStateMachineBehaviour(UnityEngine.ScriptableObject[] rawObjects)
  RVA=-1  // generic def  token=0x60001B0  T[] GetBehaviours()
  RVA=0x0B39BC00  token=0x60001B1  UnityEngine.ScriptableObject[] InternalGetBehaviours(System.Type type)
  RVA=0x0B39AC50  token=0x60001B2  UnityEngine.StateMachineBehaviour[] GetBehaviours(System.Int32 fullPathHash, System.Int32 layerIndex)
  RVA=0x0B39BBA0  token=0x60001B3  UnityEngine.ScriptableObject[] InternalGetBehavioursByKey(System.Int32 fullPathHash, System.Int32 layerIndex, System.Type type)
  RVA=0x0485A130  token=0x60001B7  System.String GetLayerName(System.Int32 layerIndex)
  RVA=0x0378A7D0  token=0x60001B8  System.Int32 GetLayerIndex(System.String layerName)
  RVA=0x0B39B810  token=0x60001B9  System.Single GetLayerWeight(System.Int32 layerIndex)
  RVA=0x0413A9F0  token=0x60001BA  System.Void SetLayerWeight(System.Int32 layerIndex, System.Single weight)
  RVA=0x0300E630  token=0x60001BB  System.Void GetAnimatorStateInfo(System.Int32 layerIndex, UnityEngine.StateInfoIndex stateInfoIndex, UnityEngine.AnimatorStateInfo& info)
  RVA=0x0300E580  token=0x60001BC  UnityEngine.AnimatorStateInfo GetCurrentAnimatorStateInfo(System.Int32 layerIndex)
  RVA=0x0300E4E0  token=0x60001BD  UnityEngine.AnimatorStateInfo GetNextAnimatorStateInfo(System.Int32 layerIndex)
  RVA=0x0B39AB3C  token=0x60001BE  System.Void GetAnimatorTransitionInfo(System.Int32 layerIndex, UnityEngine.AnimatorTransitionInfo& info)
  RVA=0x0B39AB8C  token=0x60001BF  UnityEngine.AnimatorTransitionInfo GetAnimatorTransitionInfo(System.Int32 layerIndex)
  RVA=0x0B39A9A4  token=0x60001C0  System.Int32 GetAnimatorClipInfoCount(System.Int32 layerIndex, System.Boolean current)
  RVA=0x0B39B04C  token=0x60001C1  System.Int32 GetCurrentAnimatorClipInfoCount(System.Int32 layerIndex)
  RVA=0x0B39B8FC  token=0x60001C2  System.Int32 GetNextAnimatorClipInfoCount(System.Int32 layerIndex)
  RVA=0x0B39B0D4  token=0x60001C3  UnityEngine.AnimatorClipInfo[] GetCurrentAnimatorClipInfo(System.Int32 layerIndex)
  RVA=0x0B39B984  token=0x60001C4  UnityEngine.AnimatorClipInfo[] GetNextAnimatorClipInfo(System.Int32 layerIndex)
  RVA=0x0B39B29C  token=0x60001C5  System.Boolean GetDebugAnimatorClipInfo(System.Int32 layerIndex, System.Collections.Generic.List<UnityEngine.AnimatorClipInfo> clips)
  RVA=0x0B39B05C  token=0x60001C6  System.Void GetCurrentAnimatorClipInfo(System.Int32 layerIndex, System.Collections.Generic.List<UnityEngine.AnimatorClipInfo> clips)
  RVA=0x0B39A9F8  token=0x60001C7  System.Void GetAnimatorClipInfoInternal(System.Int32 layerIndex, System.Boolean isCurrent, System.Object clips)
  RVA=0x0B39B24C  token=0x60001C8  System.Boolean GetDebugAnimatorClipInfoInternal(System.Int32 layerIndex, System.Object clips)
  RVA=0x0B39B90C  token=0x60001C9  System.Void GetNextAnimatorClipInfo(System.Int32 layerIndex, System.Collections.Generic.List<UnityEngine.AnimatorClipInfo> clips)
  RVA=0x0300E6A0  token=0x60001CA  System.Boolean IsInTransition(System.Int32 layerIndex)
  RVA=0x0343E5E0  token=0x60001CB  System.Boolean IsPlayingTag(System.Int32 layerIndex, System.Int32 tagHash)
  RVA=0x03008F60  token=0x60001CC  System.Boolean IsPlayingState(System.Int32 layerIndex, System.Int32 stateNameHash)
  RVA=0x0343EA60  token=0x60001CD  System.Int32 GetAnimatorStateCullMode(System.Int32 layerIndex)
  RVA=0x0B39B114  token=0x60001CE  UnityEngine.AnimatorStateRootMotionInfo GetCurrentAnimatorStateRootmotionInfo(System.Int32 layerIndex)
  RVA=0x0B39AAEC  token=0x60001CF  System.Void GetAnimatorStateRootMotionInfo(System.Int32 layerIndex, UnityEngine.AnimatorStateRootMotionInfo& output)
  RVA=0x0B39BB60  token=0x60001D0  System.Boolean HasTrigger(System.Int32 triggerId)
  RVA=0x041CCA50  token=0x60001D3  UnityEngine.AnimatorControllerParameter GetParameterInternal(System.Int32 index)
  RVA=0x041CCA10  token=0x60001D4  UnityEngine.AnimatorControllerParameter GetParameter(System.Int32 index)
  RVA=0x0B39C1D4  token=0x60001D9  System.Void MatchTarget(UnityEngine.Vector3 matchPosition, UnityEngine.Quaternion matchRotation, System.Int32 targetBodyPart, UnityEngine.MatchTargetWeightMask weightMask, System.Single startNormalizedTime, System.Single targetNormalizedTime, System.Boolean completeMatch)
  RVA=0x0B39C158  token=0x60001DA  System.Void MatchTarget(UnityEngine.Vector3 matchPosition, UnityEngine.Quaternion matchRotation, UnityEngine.AvatarTarget targetBodyPart, UnityEngine.MatchTargetWeightMask weightMask, System.Single startNormalizedTime)
  RVA=0x0B39C0DC  token=0x60001DB  System.Void MatchTarget(UnityEngine.Vector3 matchPosition, UnityEngine.Quaternion matchRotation, UnityEngine.AvatarTarget targetBodyPart, UnityEngine.MatchTargetWeightMask weightMask, System.Single startNormalizedTime, System.Single targetNormalizedTime)
  RVA=0x0B39C224  token=0x60001DC  System.Void MatchTarget(UnityEngine.Vector3 matchPosition, UnityEngine.Quaternion matchRotation, UnityEngine.AvatarTarget targetBodyPart, UnityEngine.MatchTargetWeightMask weightMask, System.Single startNormalizedTime, System.Single targetNormalizedTime, System.Boolean completeMatch)
  RVA=0x0B39BEF0  token=0x60001DD  System.Void InterruptMatchTarget()
  RVA=0x0B39BEAC  token=0x60001DE  System.Void InterruptMatchTarget(System.Boolean completeMatch)
  RVA=0x0B39A90C  token=0x60001E4  System.Void ForceStateNormalizedTime(System.Single normalizedTime)
  RVA=0x0B39A1E8  token=0x60001E5  System.Void CrossFadeInFixedTime(System.String stateName, System.Single fixedTransitionDuration)
  RVA=0x0B399FAC  token=0x60001E6  System.Void CrossFadeInFixedTime(System.String stateName, System.Single fixedTransitionDuration, System.Int32 layer)
  RVA=0x0B39A0B8  token=0x60001E7  System.Void CrossFadeInFixedTime(System.String stateName, System.Single fixedTransitionDuration, System.Int32 layer, System.Single fixedTimeOffset)
  RVA=0x0B39A008  token=0x60001E8  System.Void CrossFadeInFixedTime(System.String stateName, System.Single fixedTransitionDuration, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource)
  RVA=0x0B399EB0  token=0x60001E9  System.Void CrossFadeInFixedTime(System.String stateName, System.Single fixedTransitionDuration, System.Int32 layer, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource)
  RVA=0x0B39A238  token=0x60001EA  System.Void CrossFadeInFixedTime(System.String stateName, System.Single fixedTransitionDuration, System.Int32 layer, System.Single fixedTimeOffset, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource, System.Boolean enableBlendRootMotion)
  RVA=0x0B399F28  token=0x60001EB  System.Void CrossFadeInFixedTime(System.String stateName, System.Single fixedTransitionDuration, System.Int32 layer, System.Single fixedTimeOffset, System.Single normalizedTransitionTime, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource)
  RVA=0x0B39A078  token=0x60001EC  System.Void CrossFadeInFixedTime(System.Int32 stateHashName, System.Single fixedTransitionDuration, System.Int32 layer, System.Single fixedTimeOffset, System.Single normalizedTransitionTime)
  RVA=0x0B39A11C  token=0x60001ED  System.Void CrossFadeInFixedTime(System.Int32 stateHashName, System.Single fixedTransitionDuration, System.Int32 layer, System.Single fixedTimeOffset)
  RVA=0x0B399E44  token=0x60001EE  System.Void CrossFadeInFixedTime(System.Int32 stateHashName, System.Single fixedTransitionDuration, System.Int32 layer)
  RVA=0x0B399E78  token=0x60001EF  System.Void CrossFadeInFixedTime(System.Int32 stateHashName, System.Single fixedTransitionDuration)
  RVA=0x0B39A158  token=0x60001F0  System.Void CrossFadeInFixedTime(System.Int32 stateHashName, System.Single fixedTransitionDuration, System.Int32 layer, System.Single fixedTimeOffset, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource)
  RVA=0x03D70D00  token=0x60001F1  System.Void CrossFadeInFixedTime(System.Int32 stateHashName, System.Single fixedTransitionDuration, System.Int32 layer, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource)
  RVA=0x0B39A1A4  token=0x60001F2  System.Void CrossFadeInFixedTime(System.Int32 stateHashName, System.Single fixedTransitionDuration, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource)
  RVA=0x03D710E0  token=0x60001F3  System.Void CrossFadeInFixedTime(System.Int32 stateHashName, System.Single fixedTransitionDuration, System.Int32 layer, System.Single fixedTimeOffset, System.Single normalizedTransitionTime, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource, System.Boolean enableBlendRootMotion)
  RVA=0x0B39DE70  token=0x60001F4  System.Void WriteDefaultValues()
  RVA=0x04A31860  token=0x60001F5  System.Void WriteDefaultValuesDirty()
  RVA=0x0B39A694  token=0x60001F6  System.Void CrossFade(System.String stateName, System.Single normalizedTransitionDuration, System.Int32 layer, System.Single normalizedTimeOffset, System.Single normalizedTransitionTime)
  RVA=0x0B39A3AC  token=0x60001F7  System.Void CrossFade(System.String stateName, System.Single normalizedTransitionDuration, System.Int32 layer, System.Single normalizedTimeOffset)
  RVA=0x0B39A378  token=0x60001F8  System.Void CrossFade(System.String stateName, System.Single normalizedTransitionDuration, System.Int32 layer)
  RVA=0x0B39A60C  token=0x60001F9  System.Void CrossFade(System.String stateName, System.Single normalizedTransitionDuration)
  RVA=0x0B39A6D0  token=0x60001FA  System.Void CrossFade(System.String stateName, System.Single normalizedTransitionDuration, System.Int32 layer, System.Single normalizedTimeOffset, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource, System.Boolean enableBlendRootMotion)
  RVA=0x0B39A2BC  token=0x60001FB  System.Void CrossFade(System.String stateName, System.Single normalizedTransitionDuration, System.Int32 layer, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource)
  RVA=0x0B39A510  token=0x60001FC  System.Void CrossFade(System.String stateName, System.Single normalizedTransitionDuration, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource)
  RVA=0x0B39A588  token=0x60001FD  System.Void CrossFade(System.String stateName, System.Single normalizedTransitionDuration, System.Int32 layer, System.Single normalizedTimeOffset, System.Single normalizedTransitionTime, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource)
  RVA=0x0B39A470  token=0x60001FE  System.Void CrossFade(System.Int32 stateHashName, System.Single normalizedTransitionDuration, System.Int32 layer, System.Single normalizedTimeOffset, System.Single normalizedTransitionTime, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource, System.Boolean enableBlendRootMotion)
  RVA=0x0B39A3E4  token=0x60001FF  System.Void CrossFade(System.Int32 stateHashName, System.Single normalizedTransitionDuration, System.Int32 layer, System.Single normalizedTimeOffset, System.Single normalizedTransitionTime)
  RVA=0x0B39A754  token=0x6000200  System.Void CrossFade(System.Int32 stateHashName, System.Single normalizedTransitionDuration, System.Int32 layer, System.Single normalizedTimeOffset)
  RVA=0x0B39A33C  token=0x6000201  System.Void CrossFade(System.Int32 stateHashName, System.Single normalizedTransitionDuration, System.Int32 layer)
  RVA=0x0B39A4D0  token=0x6000202  System.Void CrossFade(System.Int32 stateHashName, System.Single normalizedTransitionDuration)
  RVA=0x0B39A648  token=0x6000203  System.Void CrossFade(System.Int32 stateHashName, System.Single normalizedTransitionDuration, System.Int32 layer, System.Single normalizedTimeOffset, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource)
  RVA=0x0B39A424  token=0x6000204  System.Void CrossFade(System.Int32 stateHashName, System.Single normalizedTransitionDuration, System.Int32 layer, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource)
  RVA=0x0B39A790  token=0x6000205  System.Void CrossFade(System.Int32 stateHashName, System.Single normalizedTransitionDuration, UnityEngine.AnimationBlendStyle blendStyle, UnityEngine.AnimatorTransitionInterruptionSource interruptionSource)
  RVA=0x04CDE4F0  token=0x6000206  System.Void ResetToDefaultStates()
  RVA=0x0B39C3A8  token=0x6000207  System.Void PlayInFixedTime(System.String stateName, System.Int32 layer)
  RVA=0x0B39C330  token=0x6000208  System.Void PlayInFixedTime(System.String stateName)
  RVA=0x0B39C358  token=0x6000209  System.Void PlayInFixedTime(System.String stateName, System.Int32 layer, System.Single fixedTime)
  RVA=0x0495ABA0  token=0x600020A  System.Void PlayInFixedTime(System.Int32 stateNameHash, System.Int32 layer, System.Single fixedTime)
  RVA=0x0B39C310  token=0x600020B  System.Void PlayInFixedTime(System.Int32 stateNameHash, System.Int32 layer)
  RVA=0x0B39C3C8  token=0x600020C  System.Void PlayInFixedTime(System.Int32 stateNameHash)
  RVA=0x0B39C3F0  token=0x600020D  System.Void Play(System.String stateName, System.Int32 layer)
  RVA=0x031CEA20  token=0x600020E  System.Void Play(System.String stateName)
  RVA=0x031CEA50  token=0x600020F  System.Void Play(System.String stateName, System.Int32 layer, System.Single normalizedTime)
  RVA=0x031CEAA0  token=0x6000210  System.Void Play(System.Int32 stateNameHash, System.Int32 layer, System.Single normalizedTime)
  RVA=0x0B39C410  token=0x6000211  System.Void Play(System.Int32 stateNameHash, System.Int32 layer)
  RVA=0x034FCD00  token=0x6000212  System.Void Play(System.Int32 stateNameHash)
  RVA=0x0B39DC3C  token=0x6000213  System.Void SetTarget(UnityEngine.AvatarTarget targetIndex, System.Single targetNormalizedTime)
  RVA=0x04D85B20  token=0x6000216  System.Boolean IsControlled(UnityEngine.Transform transform)
  RVA=0x0B39BEFC  token=0x6000217  System.Boolean IsBoneTransform(UnityEngine.Transform transform)
  RVA=0x0B39AF34  token=0x6000219  UnityEngine.Transform GetBoneTransform(UnityEngine.HumanBodyBones humanBoneId)
  RVA=0x0B39AEF4  token=0x600021A  UnityEngine.Transform GetBoneTransformInternal(System.Int32 humanBoneId)
  RVA=0x039C7AE0  token=0x600021B  UnityEngine.Transform GetBoneTransformByBoneId(System.Int32 boneID)
  RVA=0x039C7A90  token=0x600021C  UnityEngine.Transform GetBoneTransformByBoneIdInternal(System.Int32 boneId)
  RVA=0x0B39DCD8  token=0x600021F  System.Void StartPlayback()
  RVA=0x0B39DD4C  token=0x6000220  System.Void StopPlayback()
  RVA=0x0B39DD0C  token=0x6000223  System.Void StartRecording(System.Int32 frameCount)
  RVA=0x0B39DD80  token=0x6000224  System.Void StopRecording()
  RVA=0x0B39BA28  token=0x6000227  System.Single GetRecorderStartTime()
  RVA=0x0B39BA5C  token=0x600022A  System.Single GetRecorderStopTime()
  RVA=0x0B399BFC  token=0x600022F  System.Void ClearInternalControllerPlayable()
  RVA=0x0322C0A0  token=0x6000230  System.Boolean HasState(System.Int32 layerIndex, System.Int32 stateID)
  RVA=0x03B06430  token=0x6000231  System.Int32 StringToHash(System.String name)
  RVA=0x0B39DDB4  token=0x6000232  System.Int32 StringToHashCRC32(System.IntPtr name)
  RVA=0x0B39BB2C  token=0x600023B  System.String GetStats()
  RVA=0x0B39B1C4  token=0x600023D  System.Void GetCurrentGraph(UnityEngine.Playables.PlayableGraph& graph)
  RVA=0x0B399B14  token=0x600023E  System.Void CheckIfInIKPass()
  RVA=0x0B39BFA8  token=0x600023F  System.Boolean IsInIKPass()
  RVA=0x03DF2D80  token=0x6000240  System.Void SetFloatString(System.String name, System.Single value)
  RVA=0x03BDEE90  token=0x6000241  System.Void SetFloatID(System.Int32 id, System.Single value)
  RVA=0x040B91E0  token=0x6000242  System.Single GetFloatString(System.String name)
  RVA=0x03A09690  token=0x6000243  System.Single GetFloatID(System.Int32 id)
  RVA=0x0B39B308  token=0x6000244  System.Single GetFloatFromTransitionByID(System.Int32 layerIndex, System.Int32 id)
  RVA=0x043EE130  token=0x6000245  System.Single GetFloatFromTransitionByIDWithDefault(System.Int32 layerIndex, System.Int32 id, System.Single defValue)
  RVA=0x046F7D80  token=0x6000246  System.Void SetBoolString(System.String name, System.Boolean value)
  RVA=0x0408E410  token=0x6000247  System.Void SetBoolID(System.Int32 id, System.Boolean value)
  RVA=0x0B39AFFC  token=0x6000248  System.Boolean GetBoolString(System.String name)
  RVA=0x03FA3EE0  token=0x6000249  System.Boolean GetBoolID(System.Int32 id)
  RVA=0x0B39D174  token=0x600024A  System.Void SetIntegerString(System.String name, System.Int32 value)
  RVA=0x044E7160  token=0x600024B  System.Void SetIntegerID(System.Int32 id, System.Int32 value)
  RVA=0x0B39B780  token=0x600024C  System.Int32 GetIntegerString(System.String name)
  RVA=0x04072CD0  token=0x600024D  System.Int32 GetIntegerID(System.Int32 id)
  RVA=0x041ECAE0  token=0x600024E  System.Void SetTriggerString(System.String name)
  RVA=0x045C8690  token=0x600024F  System.Void SetTriggerID(System.Int32 id)
  RVA=0x03F82A30  token=0x6000250  System.Void ResetTriggerString(System.String name)
  RVA=0x03B5B250  token=0x6000251  System.Void ResetTriggerID(System.Int32 id)
  RVA=0x0B39C01C  token=0x6000252  System.Boolean IsParameterControlledByCurveString(System.String name)
  RVA=0x0B39BFDC  token=0x6000253  System.Boolean IsParameterControlledByCurveID(System.Int32 id)
  RVA=0x0B39CCD8  token=0x6000254  System.Void SetFloatStringDamp(System.String name, System.Single value, System.Single dampTime, System.Single deltaTime)
  RVA=0x0B39CC78  token=0x6000255  System.Void SetFloatIDDamp(System.Int32 id, System.Single value, System.Single dampTime, System.Single deltaTime)
  RVA=0x0B39B4E4  token=0x6000256  UnityEngine.HGAnimParameterSGHandle[] GetHGParameterHandles()
  RVA=0x0B399AE0  token=0x6000257  System.Boolean CheckHGParmeterHandleValid()
  RVA=0x0B39B850  token=0x600025A  System.Int32 GetMorphBoneCount()
  RVA=0x0B39B8D8  token=0x600025B  System.Void GetMorphBoneIds(Unity.Collections.NativeArray<System.Int32> arrayToFill)
  RVA=0x0B39B884  token=0x600025C  System.Void GetMorphBoneIdsUnsafe(System.Int32 count, Unity.Collections.NativeArray<System.Int32> arrayToFill)
  RVA=0x0B39BCB0  token=0x600025D  System.Void Internal_GetMorphBoneIds(System.Int32 count, System.IntPtr arrayToFill)
  RVA=0x0B39D850  token=0x600025E  System.Void SetMorphTransforms(Unity.Collections.NativeArray<System.Int32> boneIds, Unity.Collections.NativeArray<UnityEngine.Vector3> translation, Unity.Collections.NativeArray<UnityEngine.Vector3> euler, Unity.Collections.NativeArray<UnityEngine.Vector3> scale)
  RVA=0x0B39D754  token=0x600025F  System.Void SetMorphTransformsUnsafe(Unity.Collections.NativeArray<System.Int32> boneIds, Unity.Collections.NativeArray<UnityEngine.Vector3> translation, Unity.Collections.NativeArray<UnityEngine.Vector3> euler, Unity.Collections.NativeArray<UnityEngine.Vector3> scale)
  RVA=0x0B39BD60  token=0x6000260  System.Void Internal_SetMorphTransforms(System.Int32 count, System.IntPtr boneIds, System.IntPtr translation, System.IntPtr euler, System.IntPtr scale)
  RVA=0x0B39C2DC  token=0x6000266  System.Void OnUpdateModeChanged()
  RVA=0x0B39C2A8  token=0x6000267  System.Void OnCullingModeChanged()
  RVA=0x0B39DE3C  token=0x6000268  System.Void WriteDefaultPose()
  RVA=0x0378AA50  token=0x6000269  System.Void Update(System.Single deltaTime)
  RVA=0x0B39A960  token=0x600026A  System.Void ForceUpdate(System.Single deltaTime)
  RVA=0x0B39C464  token=0x600026B  System.Void Rebind()
  RVA=0x049BBFC0  token=0x600026C  System.Void Rebind(System.Boolean writeDefaultValues)
  RVA=0x0B399A78  token=0x600026D  System.Void ApplyBuiltinRootMotion()
  RVA=0x0B39A900  token=0x600026E  System.Void EvaluateController()
  RVA=0x0B39A8BC  token=0x600026F  System.Void EvaluateController(System.Single deltaTime)
  RVA=0x0B39B23C  token=0x6000270  System.String GetCurrentStateName(System.Int32 layerIndex)
  RVA=0x0B39B9C4  token=0x6000271  System.String GetNextStateName(System.Int32 layerIndex)
  RVA=0x0B39AA98  token=0x6000272  System.String GetAnimatorStateName(System.Int32 layerIndex, System.Boolean current)
  RVA=0x0B39C4F0  token=0x6000273  System.String ResolveHash(System.Int32 hash)
  RVA=0x0B39AA8C  token=0x6000274  System.String GetAnimatorStateName(System.Int32 stateID)
  RVA=0x05397594  token=0x6000275  System.Void GotoState(System.Int32 layer, System.Int32 stateId, System.Single normalizedTime, System.Single transitionDuration, System.Single transitionTime, UnityEngine.AnimationBlendStyle blendStyle)
  RVA=0x05396E48  token=0x6000276  System.Void GotoStateInFixedTime(System.Int32 layerIndex, System.Int32 stateId, System.Single fixedTime, System.Single fixedTransitionDuration, System.Single normalizedTransitionTime, UnityEngine.AnimationBlendStyle blendStyle)
  RVA=0x05391A5C  token=0x6000277  System.Void SetAnimationStateOverride(UnityEngine.RuntimeAnimatorStateOverride stateOverride, System.Boolean rebind)
  RVA=0x05396E40  token=0x6000278  System.Void RefreshStateOverride(UnityEngine.RuntimeAnimatorStateOverride stateOverride)
  RVA=0x05393BF0  token=0x6000279  System.Void SetRuntimeControllerNoRebind(UnityEngine.RuntimeAnimatorController controller)
  RVA=0x0B39C470  token=0x600027A  System.Void RefreshAnimationStateAsyncCompleteOverride(UnityEngine.RuntimeAnimatorStateOverride stateOverride)
  RVA=0x04970B70  token=0x600027B  System.Void Internal_GotoState(System.Int32 layer, System.Int32 stateId, System.Single normalizedTime, System.Single transitionDuration, System.Single transitionTime, UnityEngine.AnimationBlendStyle blendStyle)
  RVA=0x049C7590  token=0x600027C  System.Void Internal_GotoStateInFixedTime(System.Int32 layerIndex, System.Int32 stateId, System.Single fixedTime, System.Single fixedTransitionDuration, System.Single normalizedTransitionTime, UnityEngine.AnimationBlendStyle blendStyle)
  RVA=0x045DFE50  token=0x600027D  System.Void Internal_SetAnimationStateOverride(UnityEngine.RuntimeAnimatorStateOverride stateOverride, System.Boolean rebind)
  RVA=0x04654110  token=0x600027E  System.Void Internal_RefreshAnimationStateOverride(UnityEngine.RuntimeAnimatorStateOverride stateOverride)
  RVA=0x047DAB80  token=0x600027F  System.Void Internal_SetRuntimeControllerNoRebind(UnityEngine.RuntimeAnimatorController controller)
  RVA=0x04A3DAA0  token=0x6000280  System.Void Internal_RefreshAnimationStateAsyncCompleteOverride(UnityEngine.RuntimeAnimatorStateOverride stateOverride)
  RVA=0x0B39C6C4  token=0x6000281  System.Void SetAvatarNoBind(UnityEngine.Avatar avatar)
  RVA=0x044E3610  token=0x6000282  System.Void Internal_SetAvatarNoBind(UnityEngine.Avatar avatar)
  RVA=0x032C6EA0  token=0x600028F  UnityEngine.Vector3 GetVector(System.String name)
  RVA=0x032C6EA0  token=0x6000290  UnityEngine.Vector3 GetVector(System.Int32 id)
  RVA=0x041E1670  token=0x6000291  System.Void SetVector(System.String name, UnityEngine.Vector3 value)
  RVA=0x041E1670  token=0x6000292  System.Void SetVector(System.Int32 id, UnityEngine.Vector3 value)
  RVA=0x0B39BA08  token=0x6000293  UnityEngine.Quaternion GetQuaternion(System.String name)
  RVA=0x0B39BA08  token=0x6000294  UnityEngine.Quaternion GetQuaternion(System.Int32 id)
  RVA=0x041E1670  token=0x6000295  System.Void SetQuaternion(System.String name, UnityEngine.Quaternion value)
  RVA=0x041E1670  token=0x6000296  System.Void SetQuaternion(System.Int32 id, UnityEngine.Quaternion value)
  RVA=0x0B39BE24  token=0x6000297  System.Void Internal_SetRetargetScale_Injected(System.Int32[] boneNameHashes, System.Int32 count, UnityEngine.Vector3& scaleFactor, System.Int32[] boneModes)
  RVA=0x0343D850  token=0x6000298  System.Void get_deltaPosition_Injected(UnityEngine.Vector3& ret)
  RVA=0x0343CE60  token=0x6000299  System.Void get_deltaRotation_Injected(UnityEngine.Quaternion& ret)
  RVA=0x0343D6F0  token=0x600029A  System.Void get_velocity_Injected(UnityEngine.Vector3& ret)
  RVA=0x0343D7A0  token=0x600029B  System.Void get_angularVelocity_Injected(UnityEngine.Vector3& ret)
  RVA=0x0B39E830  token=0x600029C  System.Void get_rootPosition_Injected(UnityEngine.Vector3& ret)
  RVA=0x0B39F098  token=0x600029D  System.Void set_rootPosition_Injected(UnityEngine.Vector3& value)
  RVA=0x0B39E8A0  token=0x600029E  System.Void get_rootRotation_Injected(UnityEngine.Quaternion& ret)
  RVA=0x0B39F0F0  token=0x600029F  System.Void set_rootRotation_Injected(UnityEngine.Quaternion& value)
  RVA=0x0B39DFD8  token=0x60002A0  System.Void get_bodyPositionInternal_Injected(UnityEngine.Vector3& ret)
  RVA=0x0B39EBAC  token=0x60002A1  System.Void set_bodyPositionInternal_Injected(UnityEngine.Vector3& value)
  RVA=0x0B39E098  token=0x60002A2  System.Void get_bodyRotationInternal_Injected(UnityEngine.Quaternion& ret)
  RVA=0x0B39EC48  token=0x60002A3  System.Void set_bodyRotationInternal_Injected(UnityEngine.Quaternion& value)
  RVA=0x0B39B364  token=0x60002A4  System.Void GetGoalPosition_Injected(UnityEngine.AvatarIKGoal goal, UnityEngine.Vector3& ret)
  RVA=0x0B39CDA8  token=0x60002A5  System.Void SetGoalPosition_Injected(UnityEngine.AvatarIKGoal goal, UnityEngine.Vector3& goalPosition)
  RVA=0x0B39B3E4  token=0x60002A6  System.Void GetGoalRotation_Injected(UnityEngine.AvatarIKGoal goal, UnityEngine.Quaternion& ret)
  RVA=0x0B39CE0C  token=0x60002A7  System.Void SetGoalRotation_Injected(UnityEngine.AvatarIKGoal goal, UnityEngine.Quaternion& goalRotation)
  RVA=0x0B39B518  token=0x60002A8  System.Void GetHintPosition_Injected(UnityEngine.AvatarIKHint hint, UnityEngine.Vector3& ret)
  RVA=0x0B39CF10  token=0x60002A9  System.Void SetHintPosition_Injected(UnityEngine.AvatarIKHint hint, UnityEngine.Vector3& hintPosition)
  RVA=0x0B39D3F0  token=0x60002AA  System.Void SetLookAtPositionInternal_Injected(UnityEngine.Vector3& lookAtPosition)
  RVA=0x0B39C754  token=0x60002AB  System.Void SetBoneLocalRotationInternal_Injected(System.Int32 humanBoneId, UnityEngine.Quaternion& rotation)
  RVA=0x0B39AE24  token=0x60002AC  System.Void GetBoneRootTRSInternal_Injected(System.Int32 avatarAssetBoneID, UnityEngine.AnimationTransformf& ret)
  RVA=0x0B39AD54  token=0x60002AD  System.Void GetBoneLocalTRSInternal_Injected(System.Int32 avatarAssetBoneID, UnityEngine.AnimationTransformf& ret)
  RVA=0x0B399D0C  token=0x60002AE  System.Void CreateClothBindings_Injected(UnityEngine.Transform[] transformAccessArray, UnityEngine.AnimationTransformRWBufferHandle& ret)
  RVA=0x0B399C70  token=0x60002AF  System.Void CreateClothBindingsByNameLst_Injected(System.String[] transformPathArray, UnityEngine.AnimationTransformRWBufferHandle& ret)
  RVA=0x03B6A070  token=0x60002B0  System.Void CreateMorphBindings_Injected(UnityEngine.Transform[] transformAccessArray, UnityEngine.AnimationTransformRWBufferHandle& ret)
  RVA=0x0B399DA8  token=0x60002B1  System.Void CreateMorphBindingsByNameLst_Injected(System.String[] transformPathArray, UnityEngine.AnimationTransformRWBufferHandle& ret)
  RVA=0x0B39E66C  token=0x60002B2  System.Void get_pivotPosition_Injected(UnityEngine.Vector3& ret)
  RVA=0x0B39C078  token=0x60002B3  System.Void MatchTarget_Injected(UnityEngine.Vector3& matchPosition, UnityEngine.Quaternion& matchRotation, System.Int32 targetBodyPart, UnityEngine.MatchTargetWeightMask& weightMask, System.Single startNormalizedTime, System.Single targetNormalizedTime, System.Boolean completeMatch)
  RVA=0x0B39E9AC  token=0x60002B4  System.Void get_targetPosition_Injected(UnityEngine.Vector3& ret)
  RVA=0x0B39EA1C  token=0x60002B5  System.Void get_targetRotation_Injected(UnityEngine.Quaternion& ret)
END_CLASS

CLASS: UnityEngine.AnimatorControllerParameter
TYPE:  class
TOKEN: 0x2000030
SIZE:  0x28
FIELDS:
  private           System.String                   m_Name  // 0x10
  private           UnityEngine.AnimatorControllerParameterTypem_Type  // 0x18
  private           System.Single                   m_DefaultFloat  // 0x1c
  private           System.Int32                    m_DefaultInt  // 0x20
  private           System.Boolean                  m_DefaultBool  // 0x24
PROPERTIES:
  name  get=0x04D9B600
  nameHash  get=0x05393844
  type  get=0x04D9D860
METHODS:
  RVA=0x0B398618  token=0x60002C1  System.Boolean Equals(System.Object o)
  RVA=0x08479504  token=0x60002C2  System.Int32 GetHashCode()
  RVA=0x0B3986B0  token=0x60002C3  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.AnimationClipPair
TYPE:  class
TOKEN: 0x2000031
SIZE:  0x20
FIELDS:
  public            UnityEngine.AnimationClip       originalClip  // 0x10
  public            UnityEngine.AnimationClip       overrideClip  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60002C4  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.AnimatorOverrideController
TYPE:  class
TOKEN: 0x2000032
SIZE:  0x20
EXTENDS: UnityEngine.RuntimeAnimatorController
FIELDS:
  private           UnityEngine.AnimatorOverrideController.OnOverrideControllerDirtyCallbackOnOverrideControllerDirty  // 0x18
PROPERTIES:
  runtimeAnimatorController  get=0x0B399808  set=0x0B399964
  Item  get=0x0B399614  set=0x0B399858
  Item  get=0x0B399614  set=0x0B399858
  enableBindingOpt  get=0x0B39976C  set=0x0B3998DC
  reserveCount  get=0x0B3997D4  set=0x0B399920
  overridesCount  get=0x0B3997A0
  clips  get=0x0B399624  set=0x0B399860
METHODS:
  RVA=0x0B399590  token=0x60002C5  System.Void .ctor()
  RVA=0x0B3995C4  token=0x60002C6  System.Void .ctor(UnityEngine.RuntimeAnimatorController controller)
  RVA=0x0B3993AC  token=0x60002C7  System.Void Internal_Create(UnityEngine.AnimatorOverrideController self, UnityEngine.RuntimeAnimatorController controller)
  RVA=0x0B3993F0  token=0x60002CC  UnityEngine.AnimationClip Internal_GetClipByName(System.String name, System.Boolean returnEffectiveClip)
  RVA=0x0B399444  token=0x60002CD  System.Void Internal_SetClipByName(System.String name, UnityEngine.AnimationClip clip)
  RVA=0x0B399150  token=0x60002D4  UnityEngine.AnimationClip GetClip(UnityEngine.AnimationClip originalClip, System.Boolean returnEffectiveClip)
  RVA=0x0B39952C  token=0x60002D5  System.Void SetClip(UnityEngine.AnimationClip originalClip, UnityEngine.AnimationClip overrideClip, System.Boolean notify)
  RVA=0x0B3994F8  token=0x60002D6  System.Void SendNotification()
  RVA=0x0B3991A4  token=0x60002D7  UnityEngine.AnimationClip GetOriginalClip(System.Int32 index)
  RVA=0x0B3991E4  token=0x60002D8  UnityEngine.AnimationClip GetOverrideClip(UnityEngine.AnimationClip originalClip)
  RVA=0x0B399228  token=0x60002DA  System.Void GetOverrides(System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<UnityEngine.AnimationClip,UnityEngine.AnimationClip>> overrides)
  RVA=0x0B399024  token=0x60002DB  System.Void ApplyOverrides(System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<UnityEngine.AnimationClip,UnityEngine.AnimationClip>> overrides)
  RVA=0x0B3994C4  token=0x60002DE  System.Void PerformOverrideClipListCleanup()
  RVA=0x0B399498  token=0x60002DF  System.Void OnInvalidateOverrideController(UnityEngine.AnimatorOverrideController controller)
END_CLASS

CLASS: UnityEngine.AnimatorUtility
TYPE:  class
TOKEN: 0x2000034
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x044E3660  token=0x60002E2  System.Void OptimizeTransformHierarchy(UnityEngine.GameObject go, System.String[] exposedTransforms)
  RVA=0x03DD01B0  token=0x60002E3  System.Void DeoptimizeTransformHierarchy(UnityEngine.GameObject go)
END_CLASS

CLASS: UnityEngine.HumanBodyBones
TYPE:  sealed struct
TOKEN: 0x2000035
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.HumanBodyBones      Hips  // const
  public    static  UnityEngine.HumanBodyBones      LeftUpperLeg  // const
  public    static  UnityEngine.HumanBodyBones      RightUpperLeg  // const
  public    static  UnityEngine.HumanBodyBones      LeftLowerLeg  // const
  public    static  UnityEngine.HumanBodyBones      RightLowerLeg  // const
  public    static  UnityEngine.HumanBodyBones      LeftFoot  // const
  public    static  UnityEngine.HumanBodyBones      RightFoot  // const
  public    static  UnityEngine.HumanBodyBones      Spine  // const
  public    static  UnityEngine.HumanBodyBones      Chest  // const
  public    static  UnityEngine.HumanBodyBones      UpperChest  // const
  public    static  UnityEngine.HumanBodyBones      Neck  // const
  public    static  UnityEngine.HumanBodyBones      Head  // const
  public    static  UnityEngine.HumanBodyBones      LeftShoulder  // const
  public    static  UnityEngine.HumanBodyBones      RightShoulder  // const
  public    static  UnityEngine.HumanBodyBones      LeftUpperArm  // const
  public    static  UnityEngine.HumanBodyBones      RightUpperArm  // const
  public    static  UnityEngine.HumanBodyBones      LeftLowerArm  // const
  public    static  UnityEngine.HumanBodyBones      RightLowerArm  // const
  public    static  UnityEngine.HumanBodyBones      LeftHand  // const
  public    static  UnityEngine.HumanBodyBones      RightHand  // const
  public    static  UnityEngine.HumanBodyBones      LeftToes  // const
  public    static  UnityEngine.HumanBodyBones      RightToes  // const
  public    static  UnityEngine.HumanBodyBones      LeftEye  // const
  public    static  UnityEngine.HumanBodyBones      RightEye  // const
  public    static  UnityEngine.HumanBodyBones      Jaw  // const
  public    static  UnityEngine.HumanBodyBones      LeftThumbProximal  // const
  public    static  UnityEngine.HumanBodyBones      LeftThumbIntermediate  // const
  public    static  UnityEngine.HumanBodyBones      LeftThumbDistal  // const
  public    static  UnityEngine.HumanBodyBones      LeftIndexProximal  // const
  public    static  UnityEngine.HumanBodyBones      LeftIndexIntermediate  // const
  public    static  UnityEngine.HumanBodyBones      LeftIndexDistal  // const
  public    static  UnityEngine.HumanBodyBones      LeftMiddleProximal  // const
  public    static  UnityEngine.HumanBodyBones      LeftMiddleIntermediate  // const
  public    static  UnityEngine.HumanBodyBones      LeftMiddleDistal  // const
  public    static  UnityEngine.HumanBodyBones      LeftRingProximal  // const
  public    static  UnityEngine.HumanBodyBones      LeftRingIntermediate  // const
  public    static  UnityEngine.HumanBodyBones      LeftRingDistal  // const
  public    static  UnityEngine.HumanBodyBones      LeftLittleProximal  // const
  public    static  UnityEngine.HumanBodyBones      LeftLittleIntermediate  // const
  public    static  UnityEngine.HumanBodyBones      LeftLittleDistal  // const
  public    static  UnityEngine.HumanBodyBones      RightThumbProximal  // const
  public    static  UnityEngine.HumanBodyBones      RightThumbIntermediate  // const
  public    static  UnityEngine.HumanBodyBones      RightThumbDistal  // const
  public    static  UnityEngine.HumanBodyBones      RightIndexProximal  // const
  public    static  UnityEngine.HumanBodyBones      RightIndexIntermediate  // const
  public    static  UnityEngine.HumanBodyBones      RightIndexDistal  // const
  public    static  UnityEngine.HumanBodyBones      RightMiddleProximal  // const
  public    static  UnityEngine.HumanBodyBones      RightMiddleIntermediate  // const
  public    static  UnityEngine.HumanBodyBones      RightMiddleDistal  // const
  public    static  UnityEngine.HumanBodyBones      RightRingProximal  // const
  public    static  UnityEngine.HumanBodyBones      RightRingIntermediate  // const
  public    static  UnityEngine.HumanBodyBones      RightRingDistal  // const
  public    static  UnityEngine.HumanBodyBones      RightLittleProximal  // const
  public    static  UnityEngine.HumanBodyBones      RightLittleIntermediate  // const
  public    static  UnityEngine.HumanBodyBones      RightLittleDistal  // const
  public    static  UnityEngine.HumanBodyBones      LastBone  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AnimationTransformf
TYPE:  sealed struct
TOKEN: 0x2000036
SIZE:  0x38
FIELDS:
  public            UnityEngine.Vector3             t  // 0x10
  public            UnityEngine.Quaternion          q  // 0x1c
  public            UnityEngine.Vector3             s  // 0x2c
METHODS:
END_CLASS

CLASS: UnityEngine.Avatar
TYPE:  class
TOKEN: 0x2000037
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
PROPERTIES:
  isValid  get=0x039C9A80
  isHuman  get=0x039C7C40
  humanDescription  get=0x0B39F944
METHODS:
  RVA=0x0B39F8C0  token=0x60002E4  System.Void .ctor()
  RVA=0x0B39F810  token=0x60002E8  System.Void SetMuscleMinMax(System.Int32 muscleId, System.Single min, System.Single max)
  RVA=0x0B39F870  token=0x60002E9  System.Void SetParameter(System.Int32 parameterId, System.Single value)
  RVA=0x0B39F2DC  token=0x60002EA  System.Single GetAxisLength(System.Int32 humanId)
  RVA=0x0B39F3D4  token=0x60002EB  UnityEngine.Quaternion GetPreRotation(System.Int32 humanId)
  RVA=0x0B39F38C  token=0x60002EC  UnityEngine.Quaternion GetPostRotation(System.Int32 humanId)
  RVA=0x0B39F41C  token=0x60002ED  UnityEngine.Quaternion GetZYPostQ(System.Int32 humanId, UnityEngine.Quaternion parentQ, UnityEngine.Quaternion q)
  RVA=0x0B39F4A0  token=0x60002EE  UnityEngine.Quaternion GetZYRoll(System.Int32 humanId, UnityEngine.Vector3 uvw)
  RVA=0x0B39F338  token=0x60002EF  UnityEngine.Vector3 GetLimitSign(System.Int32 humanId)
  RVA=0x0B39F510  token=0x60002F0  System.Single Internal_GetAxisLength(System.Int32 humanId)
  RVA=0x0B39F6A0  token=0x60002F1  UnityEngine.Quaternion Internal_GetPreRotation(System.Int32 humanId)
  RVA=0x0B39F620  token=0x60002F2  UnityEngine.Quaternion Internal_GetPostRotation(System.Int32 humanId)
  RVA=0x0B39F730  token=0x60002F3  UnityEngine.Quaternion Internal_GetZYPostQ(System.Int32 humanId, UnityEngine.Quaternion parentQ, UnityEngine.Quaternion q)
  RVA=0x0B39F7D4  token=0x60002F4  UnityEngine.Quaternion Internal_GetZYRoll(System.Int32 humanId, UnityEngine.Vector3 uvw)
  RVA=0x0B39F5A0  token=0x60002F5  UnityEngine.Vector3 Internal_GetLimitSign(System.Int32 humanId)
  RVA=0x0B39F304  token=0x60002F6  System.String[] GetBonePaths()
  RVA=0x0B39F900  token=0x60002F7  System.Void get_humanDescription_Injected(UnityEngine.HumanDescription& ret)
  RVA=0x0B39F650  token=0x60002F8  System.Void Internal_GetPreRotation_Injected(System.Int32 humanId, UnityEngine.Quaternion& ret)
  RVA=0x0B39F5D0  token=0x60002F9  System.Void Internal_GetPostRotation_Injected(System.Int32 humanId, UnityEngine.Quaternion& ret)
  RVA=0x0B39F6D0  token=0x60002FA  System.Void Internal_GetZYPostQ_Injected(System.Int32 humanId, UnityEngine.Quaternion& parentQ, UnityEngine.Quaternion& q, UnityEngine.Quaternion& ret)
  RVA=0x0B39F774  token=0x60002FB  System.Void Internal_GetZYRoll_Injected(System.Int32 humanId, UnityEngine.Vector3& uvw, UnityEngine.Quaternion& ret)
  RVA=0x0B39F550  token=0x60002FC  System.Void Internal_GetLimitSign_Injected(System.Int32 humanId, UnityEngine.Vector3& ret)
END_CLASS

CLASS: UnityEngine.SkeletonBone
TYPE:  sealed struct
TOKEN: 0x2000038
SIZE:  0x48
FIELDS:
  public            System.String                   name  // 0x10
  private           System.String                   parentName  // 0x18
  public            UnityEngine.Vector3             position  // 0x20
  public            UnityEngine.Quaternion          rotation  // 0x2c
  public            UnityEngine.Vector3             scale  // 0x3c
METHODS:
END_CLASS

CLASS: UnityEngine.HumanLimit
TYPE:  sealed struct
TOKEN: 0x2000039
SIZE:  0x3C
FIELDS:
  private           UnityEngine.Vector3             m_Min  // 0x10
  private           UnityEngine.Vector3             m_Max  // 0x1c
  private           UnityEngine.Vector3             m_Center  // 0x28
  private           System.Single                   m_AxisLength  // 0x34
  private           System.Int32                    m_UseDefaultValues  // 0x38
METHODS:
END_CLASS

CLASS: UnityEngine.HumanBone
TYPE:  sealed struct
TOKEN: 0x200003A
SIZE:  0x50
FIELDS:
  private           System.String                   m_BoneName  // 0x10
  private           System.String                   m_HumanName  // 0x18
  public            UnityEngine.HumanLimit          limit  // 0x20
METHODS:
END_CLASS

CLASS: UnityEngine.HumanDescription
TYPE:  sealed struct
TOKEN: 0x200003B
SIZE:  0x50
FIELDS:
  public            UnityEngine.HumanBone[]         human  // 0x10
  public            UnityEngine.SkeletonBone[]      skeleton  // 0x18
  private           System.Single                   m_ArmTwist  // 0x20
  private           System.Single                   m_ForeArmTwist  // 0x24
  private           System.Single                   m_UpperLegTwist  // 0x28
  private           System.Single                   m_LegTwist  // 0x2c
  private           System.Single                   m_ArmStretch  // 0x30
  private           System.Single                   m_LegStretch  // 0x34
  private           System.Single                   m_FeetSpacing  // 0x38
  private           System.Single                   m_GlobalScale  // 0x3c
  private           System.String                   m_RootMotionBoneName  // 0x40
  private           System.Boolean                  m_HasTranslationDoF  // 0x48
  private           System.Boolean                  m_HasExtraRoot  // 0x49
  private           System.Boolean                  m_SkeletonHasParents  // 0x4a
METHODS:
END_CLASS

CLASS: UnityEngine.AvatarMaskBodyPart
TYPE:  sealed struct
TOKEN: 0x200003C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AvatarMaskBodyPart  Root  // const
  public    static  UnityEngine.AvatarMaskBodyPart  Body  // const
  public    static  UnityEngine.AvatarMaskBodyPart  Head  // const
  public    static  UnityEngine.AvatarMaskBodyPart  LeftLeg  // const
  public    static  UnityEngine.AvatarMaskBodyPart  RightLeg  // const
  public    static  UnityEngine.AvatarMaskBodyPart  LeftArm  // const
  public    static  UnityEngine.AvatarMaskBodyPart  RightArm  // const
  public    static  UnityEngine.AvatarMaskBodyPart  LeftFingers  // const
  public    static  UnityEngine.AvatarMaskBodyPart  RightFingers  // const
  public    static  UnityEngine.AvatarMaskBodyPart  LeftFootIK  // const
  public    static  UnityEngine.AvatarMaskBodyPart  RightFootIK  // const
  public    static  UnityEngine.AvatarMaskBodyPart  LeftHandIK  // const
  public    static  UnityEngine.AvatarMaskBodyPart  RightHandIK  // const
  public    static  UnityEngine.AvatarMaskBodyPart  LastBodyPart  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AvatarMask
TYPE:  sealed class
TOKEN: 0x200003D
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
PROPERTIES:
  transformCount  get=0x0B39F2A8
METHODS:
  RVA=0x0B39F1CC  token=0x60002FD  System.Boolean GetHumanoidBodyPartActive(UnityEngine.AvatarMaskBodyPart index)
  RVA=0x0B39F228  token=0x60002FF  System.String GetTransformPath(System.Int32 index)
  RVA=0x0B39F268  token=0x6000300  System.Single GetTransformWeight(System.Int32 index)
  RVA=0x0B39F20C  token=0x6000301  System.Boolean GetTransformActive(System.Int32 index)
END_CLASS

CLASS: UnityEngine.HGAnimationSetBindings
TYPE:  class
TOKEN: 0x200003E
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.HGAvatarBlendProfile
TYPE:  class
TOKEN: 0x200003F
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
METHODS:
  RVA=0x0B39F9B0  token=0x6000302  System.Void .ctor()
  RVA=0x0B39F97C  token=0x6000303  System.Void Internal_Create(UnityEngine.HGAvatarBlendProfile self)
END_CLASS

CLASS: UnityEngine.HGCorrectiveBoneData
TYPE:  class
TOKEN: 0x2000040
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
METHODS:
  RVA=0x0B39FA30  token=0x6000304  System.Void .ctor()
  RVA=0x0B39F9FC  token=0x6000305  System.Void Internal_Create(UnityEngine.HGCorrectiveBoneData self)
END_CLASS

CLASS: UnityEngine.HGSkeletalMorphAvatar
TYPE:  sealed class
TOKEN: 0x2000041
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
METHODS:
  RVA=0x0B39FAB0  token=0x6000306  System.Void .ctor()
  RVA=0x0B39FA7C  token=0x6000307  System.Void Internal_Create(UnityEngine.HGSkeletalMorphAvatar self)
END_CLASS

CLASS: UnityEngine.HumanPose
TYPE:  sealed struct
TOKEN: 0x2000042
SIZE:  0x38
FIELDS:
  public            UnityEngine.Vector3             bodyPosition  // 0x10
  public            UnityEngine.Quaternion          bodyRotation  // 0x1c
  public            System.Single[]                 muscles  // 0x30
METHODS:
  RVA=0x0B39FED8  token=0x6000308  System.Void Init()
END_CLASS

CLASS: UnityEngine.HumanPoseHandler
TYPE:  class
TOKEN: 0x2000043
SIZE:  0x18
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.IntPtr                   m_Ptr  // 0x10
METHODS:
  RVA=0x0B39FC54  token=0x6000309  System.IntPtr Internal_CreateFromRoot(UnityEngine.Avatar avatar, UnityEngine.Transform root)
  RVA=0x0B39FC98  token=0x600030A  System.Void Internal_Destroy(System.IntPtr ptr)
  RVA=0x0B39FB54  token=0x600030B  System.Void GetHumanPose(UnityEngine.Vector3& bodyPosition, UnityEngine.Quaternion& bodyRotation, System.Single[] muscles)
  RVA=0x0B39FAFC  token=0x600030C  System.Void Dispose()
  RVA=0x0B39FCCC  token=0x600030D  System.Void .ctor(UnityEngine.Avatar avatar, UnityEngine.Transform root)
  RVA=0x0B39FBB8  token=0x600030E  System.Void GetHumanPose(UnityEngine.HumanPose& humanPose)
END_CLASS

CLASS: UnityEngine.HumanTrait
TYPE:  class
TOKEN: 0x2000044
SIZE:  0x10
FIELDS:
PROPERTIES:
  MuscleCount  get=0x0B3A0010
  MuscleName  get=0x0B3A003C
METHODS:
  RVA=0x0B39FFDC  token=0x6000310  System.Int32 GetBoneIndexFromMono(System.Int32 humanId)
END_CLASS

CLASS: UnityEngine.LodBoneMask
TYPE:  class
TOKEN: 0x2000045
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
METHODS:
  RVA=0x0B3A009C  token=0x6000312  System.Void .ctor()
  RVA=0x0B3A0068  token=0x6000313  System.Void Internal_Create(UnityEngine.LodBoneMask self)
END_CLASS

CLASS: UnityEngine.Motion
TYPE:  class
TOKEN: 0x2000046
SIZE:  0x20
EXTENDS: UnityEngine.Object
FIELDS:
  private   readonly System.Boolean                  <isAnimatorMotion>k__BackingField  // 0x18
PROPERTIES:
  isLooping  get=0x039F6F80
METHODS:
  RVA=0x03695570  token=0x6000314  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.RuntimeAnimatorController
TYPE:  class
TOKEN: 0x2000047
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
PROPERTIES:
  animationClips  get=0x04CDE3B0
METHODS:
  RVA=0x0B3A08B8  token=0x6000316  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.RuntimeAnimatorStateOverride
TYPE:  class
TOKEN: 0x2000048
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
METHODS:
  RVA=0x0322E2A0  token=0x6000318  System.Void .ctor()
  RVA=0x0322E2D0  token=0x6000319  System.Void Internal_Create(UnityEngine.RuntimeAnimatorStateOverride self)
  RVA=0x05393BD0  token=0x600031A  System.Void SetClip(System.Int32 stateID, UnityEngine.AnimationClip overrideClip)
  RVA=0x05393BD8  token=0x600031B  System.Void SetClipAsync(System.Int32 stateID, System.Single length, System.Boolean isHumanoid)
  RVA=0x0B3A0978  token=0x600031C  System.Void RemoveClip(System.Int32 stateID)
  RVA=0x05393BE8  token=0x600031D  System.Void ClearClip()
  RVA=0x05393BC4  token=0x600031E  System.Boolean PreloadClip(UnityEngine.AnimationClip clipB)
  RVA=0x05393560  token=0x600031F  System.Void SetTransition(System.Int32 transUserNameID, System.Single transitionDuration, System.Single transitionOffset, System.Single exitTime)
  RVA=0x0B3A0980  token=0x6000320  System.Void RemoveTransition(System.Int32 transUserNameID)
  RVA=0x05391A54  token=0x6000321  System.Void SetSyncGroupCurve(System.UInt32 clipNameID, UnityEngine.AnimationCurve curve)
  RVA=0x045304F0  token=0x6000322  System.Void Internal_SetClip(System.UInt32 stateID, UnityEngine.AnimationClip overrideClip)
  RVA=0x04640880  token=0x6000323  System.Void Internal_SetClipAsync(System.UInt32 stateID, System.Single length, System.Boolean isHumanoid)
  RVA=0x0B3A08F8  token=0x6000324  System.Void Internal_RemoveClip(System.UInt32 stateID)
  RVA=0x049B8A50  token=0x6000325  System.Void Internal_ClearClip()
  RVA=0x0455DEF0  token=0x6000326  System.Boolean Internal_PreloadClip(UnityEngine.AnimationClip clipB)
  RVA=0x04416620  token=0x6000327  System.Void Internal_SetTransition(System.UInt32 transUserNameID, System.Single transitionDuration, System.Single transitionOffset, System.Single exitTime)
  RVA=0x0B3A0938  token=0x6000328  System.Void Internal_RemoveTransition(System.UInt32 transUserNameID)
  RVA=0x040E0620  token=0x6000329  System.Void Internal_SetSyncGroupCurve(System.UInt32 clipNameID, UnityEngine.AnimationCurve curve)
END_CLASS

CLASS: UnityEngine.Animations.AnimationPlayableBinding
TYPE:  static class
TOKEN: 0x200004B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0441D9A0  token=0x6000330  UnityEngine.Playables.PlayableBinding Create(System.String name, UnityEngine.Object key)
  RVA=0x04D2B9A0  token=0x6000331  UnityEngine.Playables.PlayableOutput CreateAnimationOutput(UnityEngine.Playables.PlayableGraph graph, System.String name)
END_CLASS

CLASS: UnityEngine.Animations.DiscreteEvaluationAttribute
TYPE:  class
TOKEN: 0x200004C
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.IAnimationJob
TYPE:  interface
TOKEN: 0x200004D
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000332  System.Void ProcessAnimation(UnityEngine.Animations.AnimationStream stream)
  RVA=-1  // abstract  token=0x6000333  System.Void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream)
END_CLASS

CLASS: UnityEngine.Animations.IAnimationWindowPreview
TYPE:  interface
TOKEN: 0x200004E
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.NotKeyableAttribute
TYPE:  class
TOKEN: 0x200004F
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000334  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Animations.JobMethodIndex
TYPE:  sealed struct
TOKEN: 0x2000050
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Animations.JobMethodIndexProcessRootMotionMethodIndex  // const
  public    static  UnityEngine.Animations.JobMethodIndexProcessAnimationMethodIndex  // const
  public    static  UnityEngine.Animations.JobMethodIndexMethodIndexCount  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.ProcessAnimationJobStruct`1
TYPE:  sealed struct
TOKEN: 0x2000051
SIZE:  0x11
FIELDS:
  private   static  System.IntPtr                   jobReflectionData  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000335  System.IntPtr GetJobReflectionData()
  RVA=-1  // not resolved  token=0x6000336  System.Void Execute(T& data, System.IntPtr animationStreamPtr, System.IntPtr methodIndex, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex)
END_CLASS

CLASS: UnityEngine.Animations.AdvancedAnimationMixerPlayable
TYPE:  sealed struct
TOKEN: 0x2000053
SIZE:  0x20
IMPLEMENTS: UnityEngine.Playables.IPlayable System.IEquatable`1
FIELDS:
  private           UnityEngine.Playables.PlayableHandlem_Handle  // 0x10
  private   static readonly UnityEngine.Animations.AdvancedAnimationMixerPlayablem_NullPlayable  // static @ 0x0
METHODS:
  RVA=0x03E0F350  token=0x6000339  UnityEngine.Animations.AdvancedAnimationMixerPlayable Create(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount)
  RVA=0x03E0F450  token=0x600033A  UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount)
  RVA=0x03E0FA80  token=0x600033B  System.Void .ctor(UnityEngine.Playables.PlayableHandle handle)
  RVA=0x04D9E560  token=0x600033C  UnityEngine.Playables.PlayableHandle GetHandle()
  RVA=0x0B39513C  token=0x600033D  System.Boolean Equals(UnityEngine.Animations.AdvancedAnimationMixerPlayable other)
  RVA=0x03E0F5C0  token=0x600033E  System.Boolean CreateHandleInternal(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.PlayableHandle& handle)
  RVA=0x04B47850  token=0x600033F  System.Void .cctor()
  RVA=0x03E0FA30  token=0x6000340  System.Boolean CreateHandleInternal_Injected(UnityEngine.Playables.PlayableGraph& graph, UnityEngine.Playables.PlayableHandle& handle)
END_CLASS

CLASS: UnityEngine.Animations.AnimationClipPlayable
TYPE:  sealed struct
TOKEN: 0x2000054
SIZE:  0x20
IMPLEMENTS: UnityEngine.Playables.IPlayable System.IEquatable`1
FIELDS:
  private           UnityEngine.Playables.PlayableHandlem_Handle  // 0x10
METHODS:
  RVA=0x03438E90  token=0x6000341  UnityEngine.Animations.AnimationClipPlayable Create(UnityEngine.Playables.PlayableGraph graph, UnityEngine.AnimationClip clip)
  RVA=0x03439010  token=0x6000342  UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph, UnityEngine.AnimationClip clip)
  RVA=0x03439130  token=0x6000343  System.Void .ctor(UnityEngine.Playables.PlayableHandle handle)
  RVA=0x04D9E560  token=0x6000344  UnityEngine.Playables.PlayableHandle GetHandle()
  RVA=0x0315BC70  token=0x6000345  UnityEngine.Playables.Playable op_Implicit(UnityEngine.Animations.AnimationClipPlayable playable)
  RVA=0x0B39532C  token=0x6000346  UnityEngine.Animations.AnimationClipPlayable op_Explicit(UnityEngine.Playables.Playable playable)
  RVA=0x0B3951C8  token=0x6000347  System.Boolean Equals(UnityEngine.Animations.AnimationClipPlayable other)
  RVA=0x05392594  token=0x6000348  UnityEngine.AnimationClip GetAnimationClip()
  RVA=0x0B395270  token=0x6000349  System.Single GetAnimationClipPlayableLengthUncheck()
  RVA=0x0B3952D8  token=0x600034A  System.Void SetAnimationClip(UnityEngine.AnimationClip clip)
  RVA=0x0B3952D0  token=0x600034B  System.Void SetAnimationClipAsync(System.Single length, System.Boolean isHumanoid)
  RVA=0x0539815C  token=0x600034C  System.Void SetApplyFootIK(System.Boolean value)
  RVA=0x0B395324  token=0x600034D  System.Void SetApplyPlayableIK(System.Boolean value)
  RVA=0x05398144  token=0x600034E  System.Void SetRemoveStartOffset(System.Boolean value)
  RVA=0x0539814C  token=0x600034F  System.Void SetOverrideLoopTime(System.Boolean value)
  RVA=0x05398154  token=0x6000350  System.Void SetLoopTime(System.Boolean value)
  RVA=0x05392580  token=0x6000351  System.Boolean CreateHandleInternal(UnityEngine.Playables.PlayableGraph graph, UnityEngine.AnimationClip clip, UnityEngine.Playables.PlayableHandle& handle)
  RVA=0x03F633B0  token=0x6000352  UnityEngine.AnimationClip GetAnimationClipInternal(UnityEngine.Playables.PlayableHandle& handle)
  RVA=0x04CD8EF0  token=0x6000353  System.Void SetAnimationClipInternal(UnityEngine.Playables.PlayableHandle& handle, UnityEngine.AnimationClip clip)
  RVA=0x0B39527C  token=0x6000354  System.Void SetAnimationClipAsyncInternal(UnityEngine.Playables.PlayableHandle& handle, System.Single length, System.Boolean isHumanoid)
  RVA=0x04D5E540  token=0x6000355  System.Void SetApplyFootIKInternal(UnityEngine.Playables.PlayableHandle& handle, System.Boolean value)
  RVA=0x0B3952E0  token=0x6000356  System.Void SetApplyPlayableIKInternal(UnityEngine.Playables.PlayableHandle& handle, System.Boolean value)
  RVA=0x04D5E450  token=0x6000357  System.Void SetRemoveStartOffsetInternal(UnityEngine.Playables.PlayableHandle& handle, System.Boolean value)
  RVA=0x04D5E4A0  token=0x6000358  System.Void SetOverrideLoopTimeInternal(UnityEngine.Playables.PlayableHandle& handle, System.Boolean value)
  RVA=0x04D5E4F0  token=0x6000359  System.Void SetLoopTimeInternal(UnityEngine.Playables.PlayableHandle& handle, System.Boolean value)
  RVA=0x0B39523C  token=0x600035A  System.Single GetAnimationClipPlayableLengthInternalUncheck(UnityEngine.Playables.PlayableHandle& handle)
  RVA=0x04126100  token=0x600035B  System.Boolean CreateHandleInternal_Injected(UnityEngine.Playables.PlayableGraph& graph, UnityEngine.AnimationClip clip, UnityEngine.Playables.PlayableHandle& handle)
END_CLASS

CLASS: UnityEngine.Animations.AnimationHumanStream
TYPE:  sealed struct
TOKEN: 0x2000055
SIZE:  0x18
FIELDS:
  private           System.IntPtr                   stream  // 0x10
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.AnimationLayerMixerPlayable
TYPE:  sealed struct
TOKEN: 0x2000056
SIZE:  0x20
IMPLEMENTS: UnityEngine.Playables.IPlayable System.IEquatable`1
FIELDS:
  private           UnityEngine.Playables.PlayableHandlem_Handle  // 0x10
  private   static readonly UnityEngine.Animations.AnimationLayerMixerPlayablem_NullPlayable  // static @ 0x0
PROPERTIES:
  Null  get=0x0B3963D4
METHODS:
  RVA=0x03E0ECE0  token=0x600035D  UnityEngine.Animations.AnimationLayerMixerPlayable Create(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount)
  RVA=0x03E0ED80  token=0x600035E  UnityEngine.Animations.AnimationLayerMixerPlayable Create(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount, System.Boolean singleLayerOptimization)
  RVA=0x03E0EE50  token=0x600035F  UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount)
  RVA=0x03E0FC00  token=0x6000360  System.Void .ctor(UnityEngine.Playables.PlayableHandle handle, System.Boolean singleLayerOptimization)
  RVA=0x04D9E560  token=0x6000361  UnityEngine.Playables.PlayableHandle GetHandle()
  RVA=0x03FA72D0  token=0x6000362  UnityEngine.Playables.Playable op_Implicit(UnityEngine.Animations.AnimationLayerMixerPlayable playable)
  RVA=0x0B39642C  token=0x6000363  UnityEngine.Animations.AnimationLayerMixerPlayable op_Explicit(UnityEngine.Playables.Playable playable)
  RVA=0x0B395BFC  token=0x6000364  System.Boolean Equals(UnityEngine.Animations.AnimationLayerMixerPlayable other)
  RVA=0x0B395CDC  token=0x6000365  System.Void SetLayerAdditive(System.UInt32 layerIndex, System.Boolean value)
  RVA=0x0B396040  token=0x6000366  System.Void SetLayerMaskFromAvatarMask(System.UInt32 layerIndex, UnityEngine.AvatarMask mask)
  RVA=0x0B396274  token=0x6000367  System.Void SetLayerRootMotionDisable(System.UInt32 layerIndex, System.Boolean value)
  RVA=0x0B395E90  token=0x6000368  System.Void SetLayerEnableFloatCurve(System.UInt32 layerIndex, System.Boolean value)
  RVA=0x03E0FB50  token=0x6000369  System.Boolean CreateHandleInternal(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.PlayableHandle& handle)
  RVA=0x0B395C88  token=0x600036A  System.Void SetLayerAdditiveInternal(UnityEngine.Playables.PlayableHandle& handle, System.UInt32 layerIndex, System.Boolean value)
  RVA=0x03E0FD00  token=0x600036B  System.Void SetSingleLayerOptimizationInternal(UnityEngine.Playables.PlayableHandle& handle, System.Boolean value)
  RVA=0x0B395FF0  token=0x600036C  System.Void SetLayerMaskFromAvatarMaskInternal(UnityEngine.Playables.PlayableHandle& handle, System.UInt32 layerIndex, UnityEngine.AvatarMask mask)
  RVA=0x0B396220  token=0x600036D  System.Void SetLayerRootMotionDisableInternal(UnityEngine.Playables.PlayableHandle& handle, System.UInt32 layerIndex, System.Boolean value)
  RVA=0x0B395E3C  token=0x600036E  System.Void SetLayerEnableFloatCurveInternal(UnityEngine.Playables.PlayableHandle& handle, System.UInt32 layerIndex, System.Boolean value)
  RVA=0x04D11E00  token=0x600036F  System.Void .cctor()
  RVA=0x03E0FBB0  token=0x6000370  System.Boolean CreateHandleInternal_Injected(UnityEngine.Playables.PlayableGraph& graph, UnityEngine.Playables.PlayableHandle& handle)
END_CLASS

CLASS: UnityEngine.Animations.AnimationMixerPlayable
TYPE:  sealed struct
TOKEN: 0x2000057
SIZE:  0x20
IMPLEMENTS: UnityEngine.Playables.IPlayable System.IEquatable`1
FIELDS:
  private           UnityEngine.Playables.PlayableHandlem_Handle  // 0x10
  private   static readonly UnityEngine.Animations.AnimationMixerPlayablem_NullPlayable  // static @ 0x0
METHODS:
  RVA=0x0B3964A8  token=0x6000371  UnityEngine.Animations.AnimationMixerPlayable Create(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount, System.Boolean normalizeWeights)
  RVA=0x03E0F600  token=0x6000372  UnityEngine.Animations.AnimationMixerPlayable Create(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount)
  RVA=0x03E0F700  token=0x6000373  UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount)
  RVA=0x03E0F9B0  token=0x6000374  System.Void .ctor(UnityEngine.Playables.PlayableHandle handle)
  RVA=0x04D9E560  token=0x6000375  UnityEngine.Playables.PlayableHandle GetHandle()
  RVA=0x03FA6B30  token=0x6000376  UnityEngine.Playables.Playable op_Implicit(UnityEngine.Animations.AnimationMixerPlayable playable)
  RVA=0x0B396528  token=0x6000377  System.Boolean Equals(UnityEngine.Animations.AnimationMixerPlayable other)
  RVA=0x03E0F890  token=0x6000378  System.Boolean CreateHandleInternal(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.PlayableHandle& handle)
  RVA=0x04B47960  token=0x6000379  System.Void .cctor()
  RVA=0x03E0F960  token=0x600037A  System.Boolean CreateHandleInternal_Injected(UnityEngine.Playables.PlayableGraph& graph, UnityEngine.Playables.PlayableHandle& handle)
END_CLASS

CLASS: UnityEngine.Animations.AnimationMotionXToDeltaPlayable
TYPE:  sealed struct
TOKEN: 0x2000058
SIZE:  0x20
IMPLEMENTS: UnityEngine.Playables.IPlayable System.IEquatable`1
FIELDS:
  private           UnityEngine.Playables.PlayableHandlem_Handle  // 0x10
  private   static readonly UnityEngine.Animations.AnimationMotionXToDeltaPlayablem_NullPlayable  // static @ 0x0
METHODS:
  RVA=0x03FA7230  token=0x600037B  UnityEngine.Animations.AnimationMotionXToDeltaPlayable Create(UnityEngine.Playables.PlayableGraph graph)
  RVA=0x03FA6FB0  token=0x600037C  UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph)
  RVA=0x03FA5E80  token=0x600037D  System.Void .ctor(UnityEngine.Playables.PlayableHandle handle)
  RVA=0x04D9E560  token=0x600037E  UnityEngine.Playables.PlayableHandle GetHandle()
  RVA=0x03FA92E0  token=0x600037F  UnityEngine.Playables.Playable op_Implicit(UnityEngine.Animations.AnimationMotionXToDeltaPlayable playable)
  RVA=0x0B3965B4  token=0x6000380  System.Boolean Equals(UnityEngine.Animations.AnimationMotionXToDeltaPlayable other)
  RVA=0x03FA5FB0  token=0x6000381  System.Void SetAbsoluteMotion(System.Boolean value)
  RVA=0x03FA60B0  token=0x6000382  System.Boolean CreateHandleInternal(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.PlayableHandle& handle)
  RVA=0x03FA6010  token=0x6000383  System.Void SetAbsoluteMotionInternal(UnityEngine.Playables.PlayableHandle& handle, System.Boolean value)
  RVA=0x03FA5DD0  token=0x6000384  System.Void .cctor()
  RVA=0x03FA7120  token=0x6000385  System.Boolean CreateHandleInternal_Injected(UnityEngine.Playables.PlayableGraph& graph, UnityEngine.Playables.PlayableHandle& handle)
END_CLASS

CLASS: UnityEngine.Animations.AnimationOffsetPlayable
TYPE:  sealed struct
TOKEN: 0x2000059
SIZE:  0x20
IMPLEMENTS: UnityEngine.Playables.IPlayable System.IEquatable`1
FIELDS:
  private           UnityEngine.Playables.PlayableHandlem_Handle  // 0x10
  private   static readonly UnityEngine.Animations.AnimationOffsetPlayablem_NullPlayable  // static @ 0x0
METHODS:
  RVA=0x03E108B0  token=0x6000386  UnityEngine.Animations.AnimationOffsetPlayable Create(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, System.Int32 inputCount)
  RVA=0x03E109A0  token=0x6000387  UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, System.Int32 inputCount)
  RVA=0x03E10D50  token=0x6000388  System.Void .ctor(UnityEngine.Playables.PlayableHandle handle)
  RVA=0x04D9E560  token=0x6000389  UnityEngine.Playables.PlayableHandle GetHandle()
  RVA=0x0315BC10  token=0x600038A  UnityEngine.Playables.Playable op_Implicit(UnityEngine.Animations.AnimationOffsetPlayable playable)
  RVA=0x0B396640  token=0x600038B  System.Boolean Equals(UnityEngine.Animations.AnimationOffsetPlayable other)
  RVA=0x03E10C60  token=0x600038C  System.Boolean CreateHandleInternal(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Playables.PlayableHandle& handle)
  RVA=0x03E114B0  token=0x600038D  System.Void .cctor()
  RVA=0x03E10CE0  token=0x600038E  System.Boolean CreateHandleInternal_Injected(UnityEngine.Playables.PlayableGraph& graph, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Playables.PlayableHandle& handle)
END_CLASS

CLASS: UnityEngine.Animations.AnimationPlayableExtensions
TYPE:  static class
TOKEN: 0x200005A
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x600038F  System.Void SetAnimatedProperties(U playable, UnityEngine.AnimationClip clip)
  RVA=0x048AC0A0  token=0x6000390  System.Void SetAnimatedPropertiesInternal(UnityEngine.Playables.PlayableHandle& playable, UnityEngine.AnimationClip animatedProperties)
END_CLASS

CLASS: UnityEngine.Animations.AnimationPlayableGraphExtensions
TYPE:  static class
TOKEN: 0x200005B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x03FC8C80  token=0x6000391  System.Boolean InternalCreateAnimationOutput(UnityEngine.Playables.PlayableGraph& graph, System.String name, UnityEngine.Playables.PlayableOutputHandle& handle)
END_CLASS

CLASS: UnityEngine.Animations.AnimationPlayableOutput
TYPE:  sealed struct
TOKEN: 0x200005C
SIZE:  0x20
IMPLEMENTS: UnityEngine.Playables.IPlayableOutput
FIELDS:
  private           UnityEngine.Playables.PlayableOutputHandlem_Handle  // 0x10
PROPERTIES:
  Null  get=0x0B39684C
METHODS:
  RVA=0x03FC8B10  token=0x6000392  UnityEngine.Animations.AnimationPlayableOutput Create(UnityEngine.Playables.PlayableGraph graph, System.String name, UnityEngine.Animator target)
  RVA=0x03FC8CE0  token=0x6000393  System.Void .ctor(UnityEngine.Playables.PlayableOutputHandle handle)
  RVA=0x04D9E560  token=0x6000395  UnityEngine.Playables.PlayableOutputHandle GetHandle()
  RVA=0x0315BC70  token=0x6000396  UnityEngine.Playables.PlayableOutput op_Implicit(UnityEngine.Animations.AnimationPlayableOutput output)
  RVA=0x04D26620  token=0x6000397  UnityEngine.Animations.AnimationPlayableOutput op_Explicit(UnityEngine.Playables.PlayableOutput output)
  RVA=0x0B3967C0  token=0x6000398  UnityEngine.Animator GetTarget()
  RVA=0x05396C40  token=0x6000399  System.Void SetTarget(UnityEngine.Animator value)
  RVA=0x02ED1AA0  token=0x600039A  System.Void PreBindAnimator(UnityEngine.Animator value)
  RVA=0x0B396844  token=0x600039B  System.Void SetNeedRebindOnChanged(System.Boolean value)
  RVA=0x0B3967CC  token=0x600039C  UnityEngine.Animator InternalGetTarget(UnityEngine.Playables.PlayableOutputHandle& handle)
  RVA=0x043C1180  token=0x600039D  System.Void InternalSetTarget(UnityEngine.Playables.PlayableOutputHandle& handle, UnityEngine.Animator target)
  RVA=0x02ED1AA0  token=0x600039E  System.Void InternalPreBindAnimator(UnityEngine.Playables.PlayableOutputHandle& handle, UnityEngine.Animator target)
  RVA=0x0B396800  token=0x600039F  System.Void InternalSetNeedRebindOnChanged(UnityEngine.Playables.PlayableOutputHandle& handle, System.Boolean value)
END_CLASS

CLASS: UnityEngine.Animations.AnimationPosePlayable
TYPE:  sealed struct
TOKEN: 0x200005D
SIZE:  0x20
IMPLEMENTS: UnityEngine.Playables.IPlayable System.IEquatable`1
FIELDS:
  private           UnityEngine.Playables.PlayableHandlem_Handle  // 0x10
  private   static readonly UnityEngine.Animations.AnimationPosePlayablem_NullPlayable  // static @ 0x0
METHODS:
  RVA=0x0B3969DC  token=0x60003A0  System.Void .ctor(UnityEngine.Playables.PlayableHandle handle)
  RVA=0x04D9E560  token=0x60003A1  UnityEngine.Playables.PlayableHandle GetHandle()
  RVA=0x0B3968C4  token=0x60003A2  System.Boolean Equals(UnityEngine.Animations.AnimationPosePlayable other)
  RVA=0x0B396958  token=0x60003A3  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Animations.AnimationRemoveScalePlayable
TYPE:  sealed struct
TOKEN: 0x200005E
SIZE:  0x20
IMPLEMENTS: UnityEngine.Playables.IPlayable System.IEquatable`1
FIELDS:
  private           UnityEngine.Playables.PlayableHandlem_Handle  // 0x10
  private   static readonly UnityEngine.Animations.AnimationRemoveScalePlayablem_NullPlayable  // static @ 0x0
METHODS:
  RVA=0x0B396C3C  token=0x60003A4  UnityEngine.Animations.AnimationRemoveScalePlayable Create(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount)
  RVA=0x0B396B54  token=0x60003A5  UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount)
  RVA=0x0B396DF8  token=0x60003A6  System.Void .ctor(UnityEngine.Playables.PlayableHandle handle)
  RVA=0x04D9E560  token=0x60003A7  UnityEngine.Playables.PlayableHandle GetHandle()
  RVA=0x0B396ED0  token=0x60003A8  UnityEngine.Playables.Playable op_Implicit(UnityEngine.Animations.AnimationRemoveScalePlayable playable)
  RVA=0x0B396CE0  token=0x60003A9  System.Boolean Equals(UnityEngine.Animations.AnimationRemoveScalePlayable other)
  RVA=0x0B396AF8  token=0x60003AA  System.Boolean CreateHandleInternal(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.PlayableHandle& handle)
  RVA=0x0B396D74  token=0x60003AB  System.Void .cctor()
  RVA=0x0B396AB4  token=0x60003AC  System.Boolean CreateHandleInternal_Injected(UnityEngine.Playables.PlayableGraph& graph, UnityEngine.Playables.PlayableHandle& handle)
END_CLASS

CLASS: UnityEngine.Animations.AnimationScriptPlayable
TYPE:  sealed struct
TOKEN: 0x200005F
SIZE:  0x20
IMPLEMENTS: UnityEngine.Playables.IPlayable System.IEquatable`1
FIELDS:
  private           UnityEngine.Playables.PlayableHandlem_Handle  // 0x10
  private   static readonly UnityEngine.Animations.AnimationScriptPlayablem_NullPlayable  // static @ 0x0
METHODS:
  RVA=-1  // generic def  token=0x60003AD  UnityEngine.Animations.AnimationScriptPlayable Create(UnityEngine.Playables.PlayableGraph graph, T jobData, System.Int32 inputCount)
  RVA=-1  // generic def  token=0x60003AE  UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph, System.Int32 inputCount)
  RVA=0x0B3973EC  token=0x60003AF  System.Void .ctor(UnityEngine.Playables.PlayableHandle handle)
  RVA=0x04D9E560  token=0x60003B0  UnityEngine.Playables.PlayableHandle GetHandle()
  RVA=-1  // generic def  token=0x60003B1  System.Void CheckJobTypeValidity()
  RVA=-1  // generic def  token=0x60003B2  System.Void SetJobData(T jobData)
  RVA=0x0B3974C4  token=0x60003B3  UnityEngine.Playables.Playable op_Implicit(UnityEngine.Animations.AnimationScriptPlayable playable)
  RVA=0x0B3970E4  token=0x60003B4  System.Boolean Equals(UnityEngine.Animations.AnimationScriptPlayable other)
  RVA=0x0B39730C  token=0x60003B5  System.Void SetForceWriteRootMotionParam(System.Boolean value)
  RVA=0x0B397210  token=0x60003B6  System.Void SetForceResetParam(System.Boolean value)
  RVA=0x0B397080  token=0x60003B7  System.Boolean CreateHandleInternal(UnityEngine.Playables.PlayableGraph graph, UnityEngine.Playables.PlayableHandle& handle, System.IntPtr jobReflectionData)
  RVA=0x0B3972B0  token=0x60003B8  System.Void SetForceWriteRootMotionParamInternal(UnityEngine.Playables.PlayableHandle handle, System.Boolean value)
  RVA=0x0B3971B4  token=0x60003B9  System.Void SetForceResetParamInternal(UnityEngine.Playables.PlayableHandle handle, System.Boolean value)
  RVA=0x0B397368  token=0x60003BA  System.Void .cctor()
  RVA=0x0B39702C  token=0x60003BB  System.Boolean CreateHandleInternal_Injected(UnityEngine.Playables.PlayableGraph& graph, UnityEngine.Playables.PlayableHandle& handle, System.IntPtr jobReflectionData)
  RVA=0x0B39726C  token=0x60003BC  System.Void SetForceWriteRootMotionParamInternal_Injected(UnityEngine.Playables.PlayableHandle& handle, System.Boolean value)
  RVA=0x0B397170  token=0x60003BD  System.Void SetForceResetParamInternal_Injected(UnityEngine.Playables.PlayableHandle& handle, System.Boolean value)
END_CLASS

CLASS: UnityEngine.Animations.AnimationStream
TYPE:  sealed struct
TOKEN: 0x2000060
SIZE:  0x48
FIELDS:
  private           System.UInt32                   m_AnimatorBindingsVersion  // 0x10
  private           System.IntPtr                   constant  // 0x18
  private           System.IntPtr                   input  // 0x20
  private           System.IntPtr                   output  // 0x28
  private           System.IntPtr                   workspace  // 0x30
  private           System.IntPtr                   inputStreamAccessor  // 0x38
  private           System.IntPtr                   animationHandleBinder  // 0x40
PROPERTIES:
  animatorBindingsVersion  get=0x04DBA750
  isValid  get=0x0B397CA8
  deltaTime  get=0x0B397C88
METHODS:
  RVA=0x0B397BE0  token=0x60003C0  System.Void CheckIsValid()
  RVA=0x0B397C80  token=0x60003C2  System.Single GetDeltaTime()
  RVA=0x0B397C4C  token=0x60003C3  System.Single GetDeltaTime_Injected(UnityEngine.Animations.AnimationStream& _unity_self)
END_CLASS

CLASS: UnityEngine.Animations.TransformStreamHandle
TYPE:  sealed struct
TOKEN: 0x2000061
SIZE:  0x1C
FIELDS:
  private           System.UInt32                   m_AnimatorBindingsVersion  // 0x10
  private           System.Int32                    handleIndex  // 0x14
  private           System.Int32                    skeletonIndex  // 0x18
PROPERTIES:
  createdByNative  get=0x0B3A088C
  hasHandleIndex  get=0x0B3A08A0
  hasSkeletonIndex  get=0x0B3A08AC
  animatorBindingsVersion  get=0x04DBA750
METHODS:
  RVA=0x0B3A18FC  token=0x60003C4  System.Boolean IsValid(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B3A18CC  token=0x60003C5  System.Boolean IsValidInternal(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A04F0  token=0x60003C7  System.Boolean IsSameVersionAsStream(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A195C  token=0x60003CB  System.Void Resolve(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B3A18B8  token=0x60003CC  System.Boolean IsResolved(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B3A187C  token=0x60003CD  System.Boolean IsResolvedInternal(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A112C  token=0x60003CE  System.Void CheckIsValidAndResolve(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A1738  token=0x60003CF  UnityEngine.Vector3 GetPosition(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B3A1E3C  token=0x60003D0  System.Void SetPosition(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3 position)
  RVA=0x0B3A1820  token=0x60003D1  UnityEngine.Quaternion GetRotation(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B3A1EF8  token=0x60003D2  System.Void SetRotation(UnityEngine.Animations.AnimationStream stream, UnityEngine.Quaternion rotation)
  RVA=0x0B3A13A8  token=0x60003D3  UnityEngine.Vector3 GetLocalPosition(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B3A1ADC  token=0x60003D4  System.Void SetLocalPosition(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3 position)
  RVA=0x0B3A1490  token=0x60003D5  UnityEngine.Quaternion GetLocalRotation(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B3A1B98  token=0x60003D6  System.Void SetLocalRotation(UnityEngine.Animations.AnimationStream stream, UnityEngine.Quaternion rotation)
  RVA=0x0B3A1574  token=0x60003D7  UnityEngine.Vector3 GetLocalScale(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B3A1C4C  token=0x60003D8  System.Void SetLocalScale(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3 scale)
  RVA=0x0B3A164C  token=0x60003D9  System.Void GetLocalTRS(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& scale)
  RVA=0x0B3A1D30  token=0x60003DA  System.Void SetLocalTRS(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale, System.Boolean useMask)
  RVA=0x0B3A12C8  token=0x60003DB  System.Void GetGlobalTR(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation)
  RVA=0x0B3A19F4  token=0x60003DC  System.Void SetGlobalTR(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, System.Boolean useMask)
  RVA=0x0B3A1954  token=0x60003DD  System.Void ResolveInternal(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A1704  token=0x60003DE  UnityEngine.Vector3 GetPositionInternal(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A1E28  token=0x60003DF  System.Void SetPositionInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3 position)
  RVA=0x0B3A17F0  token=0x60003E0  UnityEngine.Quaternion GetRotationInternal(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A1EE4  token=0x60003E1  System.Void SetRotationInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Quaternion rotation)
  RVA=0x0B3A1374  token=0x60003E2  UnityEngine.Vector3 GetLocalPositionInternal(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A1AC8  token=0x60003E3  System.Void SetLocalPositionInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3 position)
  RVA=0x0B3A1460  token=0x60003E4  UnityEngine.Quaternion GetLocalRotationInternal(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A1B84  token=0x60003E5  System.Void SetLocalRotationInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Quaternion rotation)
  RVA=0x0B3A1540  token=0x60003E6  UnityEngine.Vector3 GetLocalScaleInternal(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A1C38  token=0x60003E7  System.Void SetLocalScaleInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3 scale)
  RVA=0x0B3A163C  token=0x60003E8  System.Void GetLocalTRSInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& scale)
  RVA=0x0B3A1D04  token=0x60003E9  System.Void SetLocalTRSInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 scale, System.Boolean useMask)
  RVA=0x0B3A12B8  token=0x60003EA  System.Void GetGlobalTRInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation)
  RVA=0x0B3A19D4  token=0x60003EB  System.Void SetGlobalTRInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, System.Boolean useMask)
  RVA=0x0B3A1910  token=0x60003EC  System.Void ResolveInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A16B0  token=0x60003ED  System.Void GetPositionInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& ret)
  RVA=0x0B3A1DD4  token=0x60003EE  System.Void SetPositionInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position)
  RVA=0x0B3A179C  token=0x60003EF  System.Void GetRotationInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Quaternion& ret)
  RVA=0x0B3A1E90  token=0x60003F0  System.Void SetRotationInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Quaternion& rotation)
  RVA=0x0B3A1320  token=0x60003F1  System.Void GetLocalPositionInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& ret)
  RVA=0x0B3A1A74  token=0x60003F2  System.Void SetLocalPositionInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position)
  RVA=0x0B3A140C  token=0x60003F3  System.Void GetLocalRotationInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Quaternion& ret)
  RVA=0x0B3A1B30  token=0x60003F4  System.Void SetLocalRotationInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Quaternion& rotation)
  RVA=0x0B3A14EC  token=0x60003F5  System.Void GetLocalScaleInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& ret)
  RVA=0x0B3A1BE4  token=0x60003F6  System.Void SetLocalScaleInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& scale)
  RVA=0x0B3A15D8  token=0x60003F7  System.Void GetLocalTRSInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& scale)
  RVA=0x0B3A1CA0  token=0x60003F8  System.Void SetLocalTRSInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& scale, System.Boolean useMask)
  RVA=0x0B3A1254  token=0x60003F9  System.Void GetGlobalTRInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation)
  RVA=0x0B3A1970  token=0x60003FA  System.Void SetGlobalTRInternal_Injected(UnityEngine.Animations.TransformStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, System.Boolean useMask)
END_CLASS

CLASS: UnityEngine.Animations.PropertyStreamHandle
TYPE:  sealed struct
TOKEN: 0x2000062
SIZE:  0x20
FIELDS:
  private           System.UInt32                   m_AnimatorBindingsVersion  // 0x10
  private           System.Int32                    handleIndex  // 0x14
  private           System.Int32                    valueArrayIndex  // 0x18
  private           System.Int32                    bindType  // 0x1c
PROPERTIES:
  createdByNative  get=0x0B3A088C
  hasHandleIndex  get=0x0B3A08A0
  hasValueArrayIndex  get=0x0B3A08AC
  hasBindType  get=0x0B3A0898
  animatorBindingsVersion  get=0x04DBA750
METHODS:
  RVA=0x0B3A0530  token=0x60003FB  System.Boolean IsValid(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B3A04FC  token=0x60003FC  System.Boolean IsValidInternal(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A04F0  token=0x60003FE  System.Boolean IsSameVersionAsStream(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A04B4  token=0x6000403  System.Boolean IsResolvedInternal(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A00E8  token=0x6000404  System.Void CheckIsValidAndResolve(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A0344  token=0x6000405  System.Single GetFloat(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B3A06EC  token=0x6000406  System.Void SetFloat(UnityEngine.Animations.AnimationStream stream, System.Single value)
  RVA=0x0B3A041C  token=0x6000407  System.Int32 GetInt(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B3A07E4  token=0x6000408  System.Void SetInt(UnityEngine.Animations.AnimationStream stream, System.Int32 value)
  RVA=0x0B3A0264  token=0x6000409  System.Boolean GetBool(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B3A05EC  token=0x600040A  System.Void SetBool(UnityEngine.Animations.AnimationStream stream, System.Boolean value)
  RVA=0x0B3A0588  token=0x600040B  System.Void ResolveInternal(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A033C  token=0x600040C  System.Single GetFloatInternal(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A06E4  token=0x600040D  System.Void SetFloatInternal(UnityEngine.Animations.AnimationStream& stream, System.Single value)
  RVA=0x0B3A0414  token=0x600040E  System.Int32 GetIntInternal(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A07DC  token=0x600040F  System.Void SetIntInternal(UnityEngine.Animations.AnimationStream& stream, System.Int32 value)
  RVA=0x0B3A025C  token=0x6000410  System.Boolean GetBoolInternal(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A05E4  token=0x6000411  System.Void SetBoolInternal(UnityEngine.Animations.AnimationStream& stream, System.Boolean value)
  RVA=0x0B3A0544  token=0x6000412  System.Void ResolveInternal_Injected(UnityEngine.Animations.PropertyStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A02F8  token=0x6000413  System.Single GetFloatInternal_Injected(UnityEngine.Animations.PropertyStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A0690  token=0x6000414  System.Void SetFloatInternal_Injected(UnityEngine.Animations.PropertyStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, System.Single value)
  RVA=0x0B3A03D0  token=0x6000415  System.Int32 GetIntInternal_Injected(UnityEngine.Animations.PropertyStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A0788  token=0x6000416  System.Void SetIntInternal_Injected(UnityEngine.Animations.PropertyStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, System.Int32 value)
  RVA=0x0B3A0218  token=0x6000417  System.Boolean GetBoolInternal_Injected(UnityEngine.Animations.PropertyStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A0590  token=0x6000418  System.Void SetBoolInternal_Injected(UnityEngine.Animations.PropertyStreamHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, System.Boolean value)
END_CLASS

CLASS: UnityEngine.Animations.TransformSceneHandle
TYPE:  sealed struct
TOKEN: 0x2000063
SIZE:  0x18
FIELDS:
  private           System.UInt32                   valid  // 0x10
  private           System.Int32                    transformSceneHandleDefinitionIndex  // 0x14
PROPERTIES:
  createdByNative  get=0x068B8E74
  hasTransformSceneHandleDefinitionIndex  get=0x0B3A08A0
METHODS:
  RVA=0x0B3A10E4  token=0x6000419  System.Boolean IsValid(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B3A0988  token=0x600041C  System.Void CheckIsValid(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A0F54  token=0x600041D  UnityEngine.Vector3 GetPosition(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B3A0BC4  token=0x600041E  UnityEngine.Vector3 GetLocalPosition(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B3A103C  token=0x600041F  UnityEngine.Quaternion GetRotation(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B3A0CAC  token=0x6000420  UnityEngine.Quaternion GetLocalRotation(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B3A0D90  token=0x6000421  UnityEngine.Vector3 GetLocalScale(UnityEngine.Animations.AnimationStream stream)
  RVA=0x0B3A0E68  token=0x6000422  System.Void GetLocalTRS(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& scale)
  RVA=0x0B3A0AE4  token=0x6000423  System.Void GetGlobalTR(UnityEngine.Animations.AnimationStream stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation)
  RVA=0x0B3A10DC  token=0x6000424  System.Boolean HasValidTransform(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A0F20  token=0x6000425  UnityEngine.Vector3 GetPositionInternal(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A0B90  token=0x6000426  UnityEngine.Vector3 GetLocalPositionInternal(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A100C  token=0x6000427  UnityEngine.Quaternion GetRotationInternal(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A0C7C  token=0x6000428  UnityEngine.Quaternion GetLocalRotationInternal(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A0D5C  token=0x6000429  UnityEngine.Vector3 GetLocalScaleInternal(UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A0E58  token=0x600042A  System.Void GetLocalTRSInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& scale)
  RVA=0x0B3A0AD4  token=0x600042B  System.Void GetGlobalTRInternal(UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation)
  RVA=0x0B3A1098  token=0x600042C  System.Boolean HasValidTransform_Injected(UnityEngine.Animations.TransformSceneHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream)
  RVA=0x0B3A0ECC  token=0x600042D  System.Void GetPositionInternal_Injected(UnityEngine.Animations.TransformSceneHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& ret)
  RVA=0x0B3A0B3C  token=0x600042E  System.Void GetLocalPositionInternal_Injected(UnityEngine.Animations.TransformSceneHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& ret)
  RVA=0x0B3A0FB8  token=0x600042F  System.Void GetRotationInternal_Injected(UnityEngine.Animations.TransformSceneHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Quaternion& ret)
  RVA=0x0B3A0C28  token=0x6000430  System.Void GetLocalRotationInternal_Injected(UnityEngine.Animations.TransformSceneHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Quaternion& ret)
  RVA=0x0B3A0D08  token=0x6000431  System.Void GetLocalScaleInternal_Injected(UnityEngine.Animations.TransformSceneHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& ret)
  RVA=0x0B3A0DF4  token=0x6000432  System.Void GetLocalTRSInternal_Injected(UnityEngine.Animations.TransformSceneHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& scale)
  RVA=0x0B3A0A70  token=0x6000433  System.Void GetGlobalTRInternal_Injected(UnityEngine.Animations.TransformSceneHandle& _unity_self, UnityEngine.Animations.AnimationStream& stream, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation)
END_CLASS

CLASS: UnityEngine.Animations.PropertySceneHandle
TYPE:  sealed struct
TOKEN: 0x2000064
SIZE:  0x18
FIELDS:
  private           System.UInt32                   valid  // 0x10
  private           System.Int32                    handleIndex  // 0x14
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.AnimationSceneHandleUtility
TYPE:  static class
TOKEN: 0x2000065
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B396F24  token=0x6000434  System.Void ReadFloats(UnityEngine.Animations.AnimationStream stream, Unity.Collections.NativeArray<UnityEngine.Animations.PropertySceneHandle> handles, Unity.Collections.NativeArray<System.Single> buffer)
  RVA=-1  // generic def  token=0x6000435  System.Int32 ValidateAndGetArrayCount(UnityEngine.Animations.AnimationStream& stream, Unity.Collections.NativeArray<T0> handles, Unity.Collections.NativeArray<T1> buffer)
  RVA=0x0B396FC8  token=0x6000436  System.Void ReadSceneFloatsInternal(UnityEngine.Animations.AnimationStream& stream, System.Void* propertySceneHandles, System.Void* floatBuffer, System.Int32 count)
END_CLASS

CLASS: UnityEngine.Animations.AnimationStreamHandleUtility
TYPE:  static class
TOKEN: 0x2000066
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B397AC4  token=0x6000437  System.Void WriteFloats(UnityEngine.Animations.AnimationStream stream, Unity.Collections.NativeArray<UnityEngine.Animations.PropertyStreamHandle> handles, Unity.Collections.NativeArray<System.Single> buffer, System.Boolean useMask)
  RVA=0x0B3979B8  token=0x6000438  System.Void ReadFloats(UnityEngine.Animations.AnimationStream stream, Unity.Collections.NativeArray<UnityEngine.Animations.PropertyStreamHandle> handles, Unity.Collections.NativeArray<System.Single> buffer)
  RVA=0x0B397A60  token=0x6000439  System.Void ReadStreamFloatsInternal(UnityEngine.Animations.AnimationStream& stream, System.Void* propertyStreamHandles, System.Void* floatBuffer, System.Int32 count)
  RVA=0x0B397B7C  token=0x600043A  System.Void WriteStreamFloatsInternal(UnityEngine.Animations.AnimationStream& stream, System.Void* propertyStreamHandles, System.Void* floatBuffer, System.Int32 count, System.Boolean useMask)
END_CLASS

CLASS: UnityEngine.Animations.AnimatorControllerPlayable
TYPE:  sealed struct
TOKEN: 0x2000067
SIZE:  0x20
IMPLEMENTS: UnityEngine.Playables.IPlayable System.IEquatable`1
FIELDS:
  private           UnityEngine.Playables.PlayableHandlem_Handle  // 0x10
  private   static readonly UnityEngine.Animations.AnimatorControllerPlayablem_NullPlayable  // static @ 0x0
METHODS:
  RVA=0x0B398880  token=0x600043B  UnityEngine.Animations.AnimatorControllerPlayable Create(UnityEngine.Playables.PlayableGraph graph, UnityEngine.RuntimeAnimatorController controller)
  RVA=0x0B3987A8  token=0x600043C  UnityEngine.Playables.PlayableHandle CreateHandle(UnityEngine.Playables.PlayableGraph graph, UnityEngine.RuntimeAnimatorController controller)
  RVA=0x0B398C10  token=0x600043D  System.Void .ctor(UnityEngine.Playables.PlayableHandle handle)
  RVA=0x04D9E560  token=0x600043E  UnityEngine.Playables.PlayableHandle GetHandle()
  RVA=0x0B398A48  token=0x600043F  System.Void SetHandle(UnityEngine.Playables.PlayableHandle handle)
  RVA=0x0B398924  token=0x6000440  System.Boolean Equals(UnityEngine.Animations.AnimatorControllerPlayable other)
  RVA=0x0B3989F0  token=0x6000441  System.Boolean IsInTransition(System.Int32 layerIndex)
  RVA=0x0B398744  token=0x6000442  System.Boolean CreateHandleInternal(UnityEngine.Playables.PlayableGraph graph, UnityEngine.RuntimeAnimatorController controller, UnityEngine.Playables.PlayableHandle& handle)
  RVA=0x0B3989B0  token=0x6000443  System.Boolean IsInTransitionInternal(UnityEngine.Playables.PlayableHandle& handle, System.Int32 layerIndex)
  RVA=0x0B398B8C  token=0x6000444  System.Void .cctor()
  RVA=0x0B3986F0  token=0x6000445  System.Boolean CreateHandleInternal_Injected(UnityEngine.Playables.PlayableGraph& graph, UnityEngine.RuntimeAnimatorController controller, UnityEngine.Playables.PlayableHandle& handle)
END_CLASS

CLASS: UnityEngine.Animations.CustomStreamPropertyType
TYPE:  sealed struct
TOKEN: 0x2000068
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Animations.CustomStreamPropertyTypeFloat  // const
  public    static  UnityEngine.Animations.CustomStreamPropertyTypeBool  // const
  public    static  UnityEngine.Animations.CustomStreamPropertyTypeInt  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Animations.AnimatorJobExtensions
TYPE:  static class
TOKEN: 0x2000069
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B398E00  token=0x6000446  UnityEngine.Animations.TransformStreamHandle BindStreamTransform(UnityEngine.Animator animator, UnityEngine.Transform transform)
  RVA=0x0B398DBC  token=0x6000447  UnityEngine.Animations.PropertyStreamHandle BindStreamProperty(UnityEngine.Animator animator, UnityEngine.Transform transform, System.Type type, System.String property)
  RVA=0x0B398C98  token=0x6000448  UnityEngine.Animations.PropertyStreamHandle BindCustomStreamProperty(UnityEngine.Animator animator, System.String property, UnityEngine.Animations.CustomStreamPropertyType type)
  RVA=0x0B398D54  token=0x6000449  UnityEngine.Animations.PropertyStreamHandle BindStreamProperty(UnityEngine.Animator animator, UnityEngine.Transform transform, System.Type type, System.String property, System.Boolean isObjectReference)
  RVA=0x0B398D30  token=0x600044A  UnityEngine.Animations.TransformSceneHandle BindSceneTransform(UnityEngine.Animator animator, UnityEngine.Transform transform)
  RVA=0x0B398D14  token=0x600044B  UnityEngine.Animations.PropertySceneHandle BindSceneProperty(UnityEngine.Animator animator, UnityEngine.Transform transform, System.Type type, System.String property)
  RVA=0x0B398CE0  token=0x600044C  UnityEngine.Animations.PropertySceneHandle BindSceneProperty(UnityEngine.Animator animator, UnityEngine.Transform transform, System.Type type, System.String property, System.Boolean isObjectReference)
  RVA=0x0B398FD0  token=0x600044D  System.Void InternalBindStreamTransform(UnityEngine.Animator animator, UnityEngine.Transform transform, UnityEngine.Animations.TransformStreamHandle& transformStreamHandle)
  RVA=0x0B398F6C  token=0x600044E  System.Void InternalBindStreamProperty(UnityEngine.Animator animator, UnityEngine.Transform transform, System.Type type, System.String property, System.Boolean isObjectReference, UnityEngine.Animations.PropertyStreamHandle& propertyStreamHandle)
  RVA=0x0B398E50  token=0x600044F  System.Void InternalBindCustomStreamProperty(UnityEngine.Animator animator, System.String property, UnityEngine.Animations.CustomStreamPropertyType propertyType, UnityEngine.Animations.PropertyStreamHandle& propertyStreamHandle)
  RVA=0x0B398F18  token=0x6000450  System.Void InternalBindSceneTransform(UnityEngine.Animator animator, UnityEngine.Transform transform, UnityEngine.Animations.TransformSceneHandle& transformSceneHandle)
  RVA=0x0B398EB4  token=0x6000451  System.Void InternalBindSceneProperty(UnityEngine.Animator animator, UnityEngine.Transform transform, System.Type type, System.String property, System.Boolean isObjectReference, UnityEngine.Animations.PropertySceneHandle& propertySceneHandle)
END_CLASS

CLASS: UnityEngine.Animations.Axis
TYPE:  sealed struct
TOKEN: 0x200006A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Animations.Axis     None  // const
  public    static  UnityEngine.Animations.Axis     X  // const
  public    static  UnityEngine.Animations.Axis     Y  // const
  public    static  UnityEngine.Animations.Axis     Z  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.Animations.AnimationStreamSource
TYPE:  sealed struct
TOKEN: 0x2000049
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Experimental.Animations.AnimationStreamSourceDefaultValues  // const
  public    static  UnityEngine.Experimental.Animations.AnimationStreamSourcePreviousInputs  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.Animations.AnimationPlayableOutputExtensions
TYPE:  static class
TOKEN: 0x200004A
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B39677C  token=0x600032A  System.Void SetAnimationStreamSource(UnityEngine.Animations.AnimationPlayableOutput output, UnityEngine.Experimental.Animations.AnimationStreamSource streamSource)
  RVA=0x0B39679C  token=0x600032B  System.Void SetSortingOrder(UnityEngine.Animations.AnimationPlayableOutput output, System.UInt16 sortingOrder)
  RVA=0x0B396714  token=0x600032C  System.Void InternalSetAnimationStreamSource(UnityEngine.Playables.PlayableOutputHandle output, UnityEngine.Experimental.Animations.AnimationStreamSource streamSource)
  RVA=0x0B396768  token=0x600032D  System.Void InternalSetSortingOrder(UnityEngine.Playables.PlayableOutputHandle output, System.Int32 sortingOrder)
  RVA=0x0B3966D4  token=0x600032E  System.Void InternalSetAnimationStreamSource_Injected(UnityEngine.Playables.PlayableOutputHandle& output, UnityEngine.Experimental.Animations.AnimationStreamSource streamSource)
  RVA=0x0B396728  token=0x600032F  System.Void InternalSetSortingOrder_Injected(UnityEngine.Playables.PlayableOutputHandle& output, System.Int32 sortingOrder)
END_CLASS

