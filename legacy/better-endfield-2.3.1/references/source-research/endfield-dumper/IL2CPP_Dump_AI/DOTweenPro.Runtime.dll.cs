// ========================================================
// Dumped by @desirepro
// Assembly: DOTweenPro.Runtime.dll
// Classes:  7
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

CLASS: AnimationType
TYPE:  sealed struct
TOKEN: 0x2000003
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypeNone  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypeMove  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypeLocalMove  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypeRotate  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypeLocalRotate  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypeScale  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypeColor  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypeFade  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypeText  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypePunchPosition  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypePunchRotation  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypePunchScale  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypeShakePosition  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypeShakeRotation  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypeShakeScale  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypeCameraAspect  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypeCameraBackgroundColor  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypeCameraFieldOfView  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypeCameraOrthoSize  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypeCameraPixelRect  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypeCameraRect  // const
  public    static  DG.Tweening.DOTweenAnimation.AnimationTypeUIWidthHeight  // const
METHODS:
END_CLASS

CLASS: TargetType
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  DG.Tweening.DOTweenAnimation.TargetTypeUnset  // const
  public    static  DG.Tweening.DOTweenAnimation.TargetTypeCamera  // const
  public    static  DG.Tweening.DOTweenAnimation.TargetTypeCanvasGroup  // const
  public    static  DG.Tweening.DOTweenAnimation.TargetTypeImage  // const
  public    static  DG.Tweening.DOTweenAnimation.TargetTypeLight  // const
  public    static  DG.Tweening.DOTweenAnimation.TargetTypeRectTransform  // const
  public    static  DG.Tweening.DOTweenAnimation.TargetTypeRenderer  // const
  public    static  DG.Tweening.DOTweenAnimation.TargetTypeSpriteRenderer  // const
  public    static  DG.Tweening.DOTweenAnimation.TargetTypeRigidbody  // const
  public    static  DG.Tweening.DOTweenAnimation.TargetTypeRigidbody2D  // const
  public    static  DG.Tweening.DOTweenAnimation.TargetTypeText  // const
  public    static  DG.Tweening.DOTweenAnimation.TargetTypeTransform  // const
  public    static  DG.Tweening.DOTweenAnimation.TargetTypetk2dBaseSprite  // const
  public    static  DG.Tweening.DOTweenAnimation.TargetTypetk2dTextMesh  // const
  public    static  DG.Tweening.DOTweenAnimation.TargetTypeTextMeshPro  // const
  public    static  DG.Tweening.DOTweenAnimation.TargetTypeTextMeshProUGUI  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x2000007
SIZE:  0x18
FIELDS:
  public            UnityEngine.Transform           target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000031  System.Void .ctor()
  RVA=0x0696AF74  token=0x6000032  UnityEngine.Vector3 <DOSpiral>b__0()
  RVA=0x0696AFB0  token=0x6000033  System.Void <DOSpiral>b__1(UnityEngine.Vector3 x)
END_CLASS

CLASS: DG.Tweening.DOTweenAnimation
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x130
EXTENDS: DG.Tweening.Core.ABSAnimationComponent
FIELDS:
  private   static  System.Action<DG.Tweening.DOTweenAnimation>OnReset  // static @ 0x0
  public            System.Boolean                  targetIsSelf  // 0x68
  public            UnityEngine.GameObject          targetGO  // 0x70
  public            System.Boolean                  tweenTargetIsTargetGO  // 0x78
  public            System.Single                   delay  // 0x7c
  public            System.Single                   duration  // 0x80
  public            DG.Tweening.Ease                easeType  // 0x84
  public            UnityEngine.AnimationCurve      easeCurve  // 0x88
  public            DG.Tweening.LoopType            loopType  // 0x90
  public            System.Int32                    loops  // 0x94
  public            System.String                   id  // 0x98
  public            System.Boolean                  isRelative  // 0xa0
  public            System.Boolean                  isFrom  // 0xa1
  public            System.Boolean                  isIndependentUpdate  // 0xa2
  public            System.Boolean                  autoKill  // 0xa3
  public            System.Boolean                  autoGenerate  // 0xa4
  public            System.Boolean                  isActive  // 0xa5
  public            System.Boolean                  isValid  // 0xa6
  public            UnityEngine.Component           target  // 0xa8
  public            DG.Tweening.DOTweenAnimation.AnimationTypeanimationType  // 0xb0
  public            DG.Tweening.DOTweenAnimation.TargetTypetargetType  // 0xb4
  public            DG.Tweening.DOTweenAnimation.TargetTypeforcedTargetType  // 0xb8
  public            System.Boolean                  autoPlay  // 0xbc
  public            System.Boolean                  useTargetAsV3  // 0xbd
  public            System.Single                   endValueFloat  // 0xc0
  public            UnityEngine.Vector3             endValueV3  // 0xc4
  public            UnityEngine.Vector2             endValueV2  // 0xd0
  public            UnityEngine.Color               endValueColor  // 0xd8
  public            System.String                   endValueString  // 0xe8
  public            UnityEngine.Rect                endValueRect  // 0xf0
  public            UnityEngine.Transform           endValueTransform  // 0x100
  public            System.Boolean                  optionalBool0  // 0x108
  public            System.Boolean                  optionalBool1  // 0x109
  public            System.Single                   optionalFloat0  // 0x10c
  public            System.Int32                    optionalInt0  // 0x110
  public            DG.Tweening.RotateMode          optionalRotationMode  // 0x114
  public            DG.Tweening.ScrambleMode        optionalScrambleMode  // 0x118
  public            DG.Tweening.ShakeRandomnessMode optionalShakeRandomnessMode  // 0x11c
  public            System.String                   optionalString  // 0x120
  private           System.Boolean                  _tweenAutoGenerationCalled  // 0x128
  private           System.Int32                    _playCount  // 0x12c
EVENTS:
  OnReset  add=add_OnReset  remove=remove_OnReset
METHODS:
  RVA=0x06976974  token=0x6000003  System.Void Dispatch_OnReset(DG.Tweening.DOTweenAnimation anim)
  RVA=0x069747AC  token=0x6000004  System.Void Awake()
  RVA=0x06976E98  token=0x6000005  System.Void Start()
  RVA=0x06976D40  token=0x6000006  System.Void Reset()
  RVA=0x06973810  token=0x6000007  System.Void OnDestroy()
  RVA=0x06976D88  token=0x6000008  System.Void RewindThenRecreateTween()
  RVA=0x06976D48  token=0x6000009  System.Void RewindThenRecreateTweenAndPlay()
  RVA=0x06976D30  token=0x600000A  System.Void RecreateTween()
  RVA=0x06976D20  token=0x600000B  System.Void RecreateTweenAndPlay()
  RVA=0x06974830  token=0x600000C  System.Void CreateTween(System.Boolean regenerateIfExists, System.Boolean andPlay)
  RVA=0x069769FC  token=0x600000D  System.Collections.Generic.List<DG.Tweening.Tween> GetTweens()
  RVA=0x06976DC8  token=0x600000E  System.Void SetAnimationTarget(UnityEngine.Component tweenTarget, System.Boolean useTweenTargetGameObjectForGroupOperations)
  RVA=0x069765D8  token=0x600000F  System.Void DOPlay()
  RVA=0x06976324  token=0x6000010  System.Void DOPlayBackwards()
  RVA=0x06976480  token=0x6000011  System.Void DOPlayForward()
  RVA=0x069761DC  token=0x6000012  System.Void DOPause()
  RVA=0x06976924  token=0x6000013  System.Void DOTogglePause()
  RVA=0x06976864  token=0x6000014  System.Void DORewind()
  RVA=0x069734E8  token=0x6000015  System.Void DORestart()
  RVA=0x069766FC  token=0x6000016  System.Void DORestart(System.Boolean fromHere)
  RVA=0x0697602C  token=0x6000017  System.Void DOComplete()
  RVA=0x06976128  token=0x6000018  System.Void DOKill()
  RVA=0x06976374  token=0x6000019  System.Void DOPlayById(System.String id)
  RVA=0x0697622C  token=0x600001A  System.Void DOPlayAllById(System.String id)
  RVA=0x06976190  token=0x600001B  System.Void DOPauseAllById(System.String id)
  RVA=0x069762C4  token=0x600001C  System.Void DOPlayBackwardsById(System.String id)
  RVA=0x06976278  token=0x600001D  System.Void DOPlayBackwardsAllById(System.String id)
  RVA=0x06976420  token=0x600001E  System.Void DOPlayForwardById(System.String id)
  RVA=0x069763D4  token=0x600001F  System.Void DOPlayForwardAllById(System.String id)
  RVA=0x069764D0  token=0x6000020  System.Void DOPlayNext()
  RVA=0x069767F8  token=0x6000021  System.Void DORewindAndPlayNext()
  RVA=0x069767A0  token=0x6000022  System.Void DORewindAllById(System.String id)
  RVA=0x06976688  token=0x6000023  System.Void DORestartById(System.String id)
  RVA=0x06976628  token=0x6000024  System.Void DORestartAllById(System.String id)
  RVA=0x069760C8  token=0x6000025  System.Void DOKillById(System.String id)
  RVA=0x0697607C  token=0x6000026  System.Void DOKillAllById(System.String id)
  RVA=0x06976EDC  token=0x6000027  DG.Tweening.DOTweenAnimation.TargetType TypeToDOTargetType(System.Type t)
  RVA=0x069747F8  token=0x6000028  DG.Tweening.Tween CreateEditorPreview()
  RVA=0x069769C4  token=0x6000029  UnityEngine.GameObject GetTweenGO()
  RVA=0x069769E0  token=0x600002A  UnityEngine.GameObject GetTweenTarget()
  RVA=0x06976AE8  token=0x600002B  System.Void ReEvaluateRelativeTween()
  RVA=0x0697708C  token=0x600002C  System.Void .ctor()
  RVA=0x06973C40  token=0x600002D  System.Void <CreateTween>b__54_0()
END_CLASS

CLASS: DG.Tweening.DOTweenAnimationExtensions
TYPE:  static class
TOKEN: 0x2000005
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x600002E  System.Boolean IsSameOrSubclassOf(UnityEngine.Component t)
END_CLASS

CLASS: DG.Tweening.DOTweenProShortcuts
TYPE:  static class
TOKEN: 0x2000006
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x069776C0  token=0x600002F  System.Void .cctor()
  RVA=0x0697738C  token=0x6000030  DG.Tweening.Tweener DOSpiral(UnityEngine.Transform target, System.Single duration, System.Nullable<UnityEngine.Vector3> axis, DG.Tweening.SpiralMode mode, System.Single speed, System.Single frequency, System.Single depth, System.Boolean snapping)
END_CLASS

