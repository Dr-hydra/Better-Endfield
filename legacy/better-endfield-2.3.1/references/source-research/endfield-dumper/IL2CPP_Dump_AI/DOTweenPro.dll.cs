// ========================================================
// Dumped by @desirepro
// Assembly: DOTweenPro.dll
// Classes:  13
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

CLASS: DG.Tweening.DOTweenVisualManager
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            DG.Tweening.Core.VisualManagerPresetpreset  // 0x18
  public            DG.Tweening.Core.OnEnableBehaviouronEnableBehaviour  // 0x1c
  public            DG.Tweening.Core.OnDisableBehaviouronDisableBehaviour  // 0x20
  private           System.Boolean                  _requiresRestartFromSpawnPoint  // 0x24
  private           DG.Tweening.Core.ABSAnimationComponent_animComponent  // 0x28
METHODS:
  RVA=0x06974008  token=0x6000001  System.Void Awake()
  RVA=0x069742B0  token=0x6000002  System.Void Update()
  RVA=0x069741F8  token=0x6000003  System.Void OnEnable()
  RVA=0x06974050  token=0x6000004  System.Void OnDisable()
  RVA=0x05393520  token=0x6000005  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.HandlesDrawMode
TYPE:  sealed struct
TOKEN: 0x2000003
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.HandlesDrawMode     Orthographic  // const
  public    static  DG.Tweening.HandlesDrawMode     Perspective  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.HandlesType
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.HandlesType         Free  // const
  public    static  DG.Tweening.HandlesType         Full  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.DOTweenInspectorMode
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.DOTweenInspectorModeDefault  // const
  public    static  DG.Tweening.DOTweenInspectorModeInfoAndWaypointsOnly  // const
  public    static  DG.Tweening.DOTweenInspectorModeDeveloper  // const
  public    static  DG.Tweening.DOTweenInspectorModeOnlyPath  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.DOTweenPath
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x150
EXTENDS: DG.Tweening.Core.ABSAnimationComponent
FIELDS:
  private   static  System.Action<DG.Tweening.DOTweenPath>OnReset  // static @ 0x0
  public            System.Single                   delay  // 0x68
  public            System.Single                   duration  // 0x6c
  public            DG.Tweening.Ease                easeType  // 0x70
  public            UnityEngine.AnimationCurve      easeCurve  // 0x78
  public            System.Int32                    loops  // 0x80
  public            System.String                   id  // 0x88
  public            DG.Tweening.LoopType            loopType  // 0x90
  public            DG.Tweening.Plugins.Options.OrientTypeorientType  // 0x94
  public            UnityEngine.Transform           lookAtTransform  // 0x98
  public            UnityEngine.Vector3             lookAtPosition  // 0xa0
  public            System.Single                   lookAhead  // 0xac
  public            System.Boolean                  autoPlay  // 0xb0
  public            System.Boolean                  autoKill  // 0xb1
  public            System.Boolean                  relative  // 0xb2
  public            System.Boolean                  isLocal  // 0xb3
  public            System.Boolean                  isClosedPath  // 0xb4
  public            System.Int32                    pathResolution  // 0xb8
  public            DG.Tweening.PathMode            pathMode  // 0xbc
  public            DG.Tweening.AxisConstraint      lockRotation  // 0xc0
  public            System.Boolean                  assignForwardAndUp  // 0xc4
  public            UnityEngine.Vector3             forwardDirection  // 0xc8
  public            UnityEngine.Vector3             upDirection  // 0xd4
  public            System.Boolean                  tweenRigidbody  // 0xe0
  public            System.Collections.Generic.List<UnityEngine.Vector3>wps  // 0xe8
  public            System.Collections.Generic.List<UnityEngine.Vector3>fullWps  // 0xf0
  public            DG.Tweening.Plugins.Core.PathCore.Pathpath  // 0xf8
  public            DG.Tweening.DOTweenInspectorModeinspectorMode  // 0x100
  public            DG.Tweening.PathType            pathType  // 0x104
  public            DG.Tweening.HandlesType         handlesType  // 0x108
  public            System.Boolean                  livePreview  // 0x10c
  public            DG.Tweening.HandlesDrawMode     handlesDrawMode  // 0x110
  public            System.Single                   perspectiveHandleSize  // 0x114
  public            System.Boolean                  showIndexes  // 0x118
  public            System.Boolean                  showWpLength  // 0x119
  public            UnityEngine.Color               pathColor  // 0x11c
  public            UnityEngine.Vector3             lastSrcPosition  // 0x12c
  public            UnityEngine.Quaternion          lastSrcRotation  // 0x138
  public            System.Boolean                  wpsDropdown  // 0x148
  public            System.Single                   dropToFloorOffset  // 0x14c
  private   static  System.Reflection.MethodInfo    _miCreateTween  // static @ 0x8
EVENTS:
  OnReset  add=add_OnReset  remove=remove_OnReset
METHODS:
  RVA=0x06973578  token=0x6000008  System.Void Dispatch_OnReset(DG.Tweening.DOTweenPath path)
  RVA=0x06972600  token=0x6000009  System.Void Awake()
  RVA=0x06973B28  token=0x600000A  System.Void Reset()
  RVA=0x06973810  token=0x600000B  System.Void OnDestroy()
  RVA=0x069734AC  token=0x600000C  System.Void DOPlay()
  RVA=0x06973440  token=0x600000D  System.Void DOPlayById(System.String id)
  RVA=0x069733E8  token=0x600000E  System.Void DOPlayAllById(System.String id)
  RVA=0x06973434  token=0x600000F  System.Void DOPlayBackwards()
  RVA=0x069734A0  token=0x6000010  System.Void DOPlayForward()
  RVA=0x069733AC  token=0x6000011  System.Void DOPause()
  RVA=0x0697356C  token=0x6000012  System.Void DOTogglePause()
  RVA=0x0697355C  token=0x6000013  System.Void DORewind()
  RVA=0x069734E8  token=0x6000014  System.Void DORestart()
  RVA=0x069734F8  token=0x6000015  System.Void DORestart(System.Boolean fromHere)
  RVA=0x06973340  token=0x6000016  System.Void DOComplete()
  RVA=0x0697339C  token=0x6000017  System.Void DOKill()
  RVA=0x06973350  token=0x6000018  System.Void DOKillAllById(System.String id)
  RVA=0x069737C8  token=0x6000019  DG.Tweening.Tween GetTween()
  RVA=0x069735C8  token=0x600001A  UnityEngine.Vector3[] GetDrawPoints()
  RVA=0x06973644  token=0x600001B  UnityEngine.Vector3[] GetFullWps()
  RVA=0x06973850  token=0x600001C  System.Void ReEvaluateRelativeTween()
  RVA=0x06973C54  token=0x600001D  System.Void .ctor()
  RVA=0x06973C40  token=0x600001E  System.Void <Awake>b__44_0()
END_CLASS

CLASS: DG.Tweening.SpiralMode
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.SpiralMode          Expand  // const
  public    static  DG.Tweening.SpiralMode          ExpandThenContract  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.Core.ABSAnimationComponent
TYPE:  abstract class
TOKEN: 0x200000A
SIZE:  0x68
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            DG.Tweening.UpdateType          updateType  // 0x18
  public            System.Boolean                  isSpeedBased  // 0x1c
  public            System.Boolean                  hasOnStart  // 0x1d
  public            System.Boolean                  hasOnPlay  // 0x1e
  public            System.Boolean                  hasOnUpdate  // 0x1f
  public            System.Boolean                  hasOnStepComplete  // 0x20
  public            System.Boolean                  hasOnComplete  // 0x21
  public            System.Boolean                  hasOnTweenCreated  // 0x22
  public            System.Boolean                  hasOnRewind  // 0x23
  public            UnityEngine.Events.UnityEvent   onStart  // 0x28
  public            UnityEngine.Events.UnityEvent   onPlay  // 0x30
  public            UnityEngine.Events.UnityEvent   onUpdate  // 0x38
  public            UnityEngine.Events.UnityEvent   onStepComplete  // 0x40
  public            UnityEngine.Events.UnityEvent   onComplete  // 0x48
  public            UnityEngine.Events.UnityEvent   onTweenCreated  // 0x50
  public            UnityEngine.Events.UnityEvent   onRewind  // 0x58
  public            DG.Tweening.Tween               tween  // 0x60
METHODS:
  RVA=-1  // abstract  token=0x600002B  System.Void DOPlay()
  RVA=-1  // abstract  token=0x600002C  System.Void DOPlayBackwards()
  RVA=-1  // abstract  token=0x600002D  System.Void DOPlayForward()
  RVA=-1  // abstract  token=0x600002E  System.Void DOPause()
  RVA=-1  // abstract  token=0x600002F  System.Void DOTogglePause()
  RVA=-1  // abstract  token=0x6000030  System.Void DORewind()
  RVA=-1  // abstract  token=0x6000031  System.Void DORestart()
  RVA=-1  // abstract  token=0x6000032  System.Void DORestart(System.Boolean fromHere)
  RVA=-1  // abstract  token=0x6000033  System.Void DOComplete()
  RVA=-1  // abstract  token=0x6000034  System.Void DOKill()
  RVA=0x05393520  token=0x6000035  System.Void .ctor()
END_CLASS

CLASS: DG.Tweening.Core.OnDisableBehaviour
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.Core.OnDisableBehaviourNone  // const
  public    static  DG.Tweening.Core.OnDisableBehaviourPause  // const
  public    static  DG.Tweening.Core.OnDisableBehaviourRewind  // const
  public    static  DG.Tweening.Core.OnDisableBehaviourKill  // const
  public    static  DG.Tweening.Core.OnDisableBehaviourKillAndComplete  // const
  public    static  DG.Tweening.Core.OnDisableBehaviourDestroyGameObject  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.Core.OnEnableBehaviour
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.Core.OnEnableBehaviourNone  // const
  public    static  DG.Tweening.Core.OnEnableBehaviourPlay  // const
  public    static  DG.Tweening.Core.OnEnableBehaviourRestart  // const
  public    static  DG.Tweening.Core.OnEnableBehaviourRestartFromSpawnPoint  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.Core.VisualManagerPreset
TYPE:  sealed struct
TOKEN: 0x200000D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.Core.VisualManagerPresetCustom  // const
  public    static  DG.Tweening.Core.VisualManagerPresetPoolingSystem  // const
METHODS:
END_CLASS

CLASS: DG.Tweening.Plugins.SpiralOptions
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x38
IMPLEMENTS: DG.Tweening.Plugins.Options.IPlugOptions
FIELDS:
  public            System.Single                   depth  // 0x10
  public            System.Single                   frequency  // 0x14
  public            System.Single                   speed  // 0x18
  public            DG.Tweening.SpiralMode          mode  // 0x1c
  public            System.Boolean                  snapping  // 0x20
  private           System.Single                   unit  // 0x24
  private           UnityEngine.Quaternion          axisQ  // 0x28
METHODS:
  RVA=0x04D8C990  token=0x600001F  System.Void Reset()
END_CLASS

CLASS: DG.Tweening.Plugins.SpiralPlugin
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x10
EXTENDS: DG.Tweening.Plugins.Core.ABSTweenPlugin`3
FIELDS:
  public    static readonly UnityEngine.Vector3             DefaultDirection  // static @ 0x0
METHODS:
  RVA=0x041E1670  token=0x6000020  System.Void Reset(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.SpiralOptions> t)
  RVA=0x041E1670  token=0x6000021  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.SpiralOptions> t, System.Boolean isRelative)
  RVA=0x041E1670  token=0x6000022  System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.SpiralOptions> t, UnityEngine.Vector3 fromValue, System.Boolean setImmediately, System.Boolean isRelative)
  RVA=0x06974674  token=0x6000023  DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.SpiralOptions> Get()
  RVA=0x04D8C9A0  token=0x6000024  UnityEngine.Vector3 ConvertToStartValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.SpiralOptions> t, UnityEngine.Vector3 value)
  RVA=0x041E1670  token=0x6000025  System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.SpiralOptions> t)
  RVA=0x069746A4  token=0x6000026  System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.SpiralOptions> t)
  RVA=0x04D8C9C0  token=0x6000027  System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.SpiralOptions options, System.Single unitsXSecond, UnityEngine.Vector3 changeValue)
  RVA=0x0697432C  token=0x6000028  System.Void EvaluateAndApply(DG.Tweening.Plugins.SpiralOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<UnityEngine.Vector3> getter, DG.Tweening.Core.DOSetter<UnityEngine.Vector3> setter, System.Single elapsed, UnityEngine.Vector3 startValue, UnityEngine.Vector3 changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice)
  RVA=0x06974784  token=0x6000029  System.Void .ctor()
  RVA=0x06974734  token=0x600002A  System.Void .cctor()
END_CLASS

