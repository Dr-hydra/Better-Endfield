// ========================================================
// Dumped by @desirepro
// Assembly: DOTweenPro.Runtime.dll
// Classes:  7
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000003  // size: 0x14
    public sealed struct AnimationType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static DG.Tweening.DOTweenAnimation.AnimationType None;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType Move;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType LocalMove;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType Rotate;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType LocalRotate;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType Scale;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType Color;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType Fade;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType Text;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType PunchPosition;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType PunchRotation;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType PunchScale;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType ShakePosition;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType ShakeRotation;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType ShakeScale;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType CameraAspect;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType CameraBackgroundColor;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType CameraFieldOfView;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType CameraOrthoSize;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType CameraPixelRect;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType CameraRect;  // const
        public static DG.Tweening.DOTweenAnimation.AnimationType UIWidthHeight;  // const

    }

    // TypeToken: 0x2000004  // size: 0x14
    public sealed struct TargetType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static DG.Tweening.DOTweenAnimation.TargetType Unset;  // const
        public static DG.Tweening.DOTweenAnimation.TargetType Camera;  // const
        public static DG.Tweening.DOTweenAnimation.TargetType CanvasGroup;  // const
        public static DG.Tweening.DOTweenAnimation.TargetType Image;  // const
        public static DG.Tweening.DOTweenAnimation.TargetType Light;  // const
        public static DG.Tweening.DOTweenAnimation.TargetType RectTransform;  // const
        public static DG.Tweening.DOTweenAnimation.TargetType Renderer;  // const
        public static DG.Tweening.DOTweenAnimation.TargetType SpriteRenderer;  // const
        public static DG.Tweening.DOTweenAnimation.TargetType Rigidbody;  // const
        public static DG.Tweening.DOTweenAnimation.TargetType Rigidbody2D;  // const
        public static DG.Tweening.DOTweenAnimation.TargetType Text;  // const
        public static DG.Tweening.DOTweenAnimation.TargetType Transform;  // const
        public static DG.Tweening.DOTweenAnimation.TargetType tk2dBaseSprite;  // const
        public static DG.Tweening.DOTweenAnimation.TargetType tk2dTextMesh;  // const
        public static DG.Tweening.DOTweenAnimation.TargetType TextMeshPro;  // const
        public static DG.Tweening.DOTweenAnimation.TargetType TextMeshProUGUI;  // const

    }

    // TypeToken: 0x2000007  // size: 0x18
    public sealed class <>c__DisplayClass1_0
    {
        // Fields
        public UnityEngine.Transform target;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000031
        public System.Void .ctor() { }
        // RVA: 0x0696AF74  token: 0x6000032
        private UnityEngine.Vector3 <DOSpiral>b__0() { }
        // RVA: 0x0696AFB0  token: 0x6000033
        private System.Void <DOSpiral>b__1(UnityEngine.Vector3 x) { }

    }

namespace DG.Tweening
{

    // TypeToken: 0x2000002  // size: 0x130
    public class DOTweenAnimation : DG.Tweening.Core.ABSAnimationComponent
    {
        // Fields
        private static System.Action<DG.Tweening.DOTweenAnimation> OnReset;  // static @ 0x0
        public System.Boolean targetIsSelf;  // 0x68
        public UnityEngine.GameObject targetGO;  // 0x70
        public System.Boolean tweenTargetIsTargetGO;  // 0x78
        public System.Single delay;  // 0x7c
        public System.Single duration;  // 0x80
        public DG.Tweening.Ease easeType;  // 0x84
        public UnityEngine.AnimationCurve easeCurve;  // 0x88
        public DG.Tweening.LoopType loopType;  // 0x90
        public System.Int32 loops;  // 0x94
        public System.String id;  // 0x98
        public System.Boolean isRelative;  // 0xa0
        public System.Boolean isFrom;  // 0xa1
        public System.Boolean isIndependentUpdate;  // 0xa2
        public System.Boolean autoKill;  // 0xa3
        public System.Boolean autoGenerate;  // 0xa4
        public System.Boolean isActive;  // 0xa5
        public System.Boolean isValid;  // 0xa6
        public UnityEngine.Component target;  // 0xa8
        public DG.Tweening.DOTweenAnimation.AnimationType animationType;  // 0xb0
        public DG.Tweening.DOTweenAnimation.TargetType targetType;  // 0xb4
        public DG.Tweening.DOTweenAnimation.TargetType forcedTargetType;  // 0xb8
        public System.Boolean autoPlay;  // 0xbc
        public System.Boolean useTargetAsV3;  // 0xbd
        public System.Single endValueFloat;  // 0xc0
        public UnityEngine.Vector3 endValueV3;  // 0xc4
        public UnityEngine.Vector2 endValueV2;  // 0xd0
        public UnityEngine.Color endValueColor;  // 0xd8
        public System.String endValueString;  // 0xe8
        public UnityEngine.Rect endValueRect;  // 0xf0
        public UnityEngine.Transform endValueTransform;  // 0x100
        public System.Boolean optionalBool0;  // 0x108
        public System.Boolean optionalBool1;  // 0x109
        public System.Single optionalFloat0;  // 0x10c
        public System.Int32 optionalInt0;  // 0x110
        public DG.Tweening.RotateMode optionalRotationMode;  // 0x114
        public DG.Tweening.ScrambleMode optionalScrambleMode;  // 0x118
        public DG.Tweening.ShakeRandomnessMode optionalShakeRandomnessMode;  // 0x11c
        public System.String optionalString;  // 0x120
        private System.Boolean _tweenAutoGenerationCalled;  // 0x128
        private System.Int32 _playCount;  // 0x12c

        // Events
        event System.Action<DG.Tweening.DOTweenAnimation> OnReset;

        // Methods
        // RVA: 0x06976974  token: 0x6000003
        private static System.Void Dispatch_OnReset(DG.Tweening.DOTweenAnimation anim) { }
        // RVA: 0x069747AC  token: 0x6000004
        private System.Void Awake() { }
        // RVA: 0x06976E98  token: 0x6000005
        private System.Void Start() { }
        // RVA: 0x06976D40  token: 0x6000006
        private System.Void Reset() { }
        // RVA: 0x06973810  token: 0x6000007
        private System.Void OnDestroy() { }
        // RVA: 0x06976D88  token: 0x6000008
        public System.Void RewindThenRecreateTween() { }
        // RVA: 0x06976D48  token: 0x6000009
        public System.Void RewindThenRecreateTweenAndPlay() { }
        // RVA: 0x06976D30  token: 0x600000A
        public System.Void RecreateTween() { }
        // RVA: 0x06976D20  token: 0x600000B
        public System.Void RecreateTweenAndPlay() { }
        // RVA: 0x06974830  token: 0x600000C
        public System.Void CreateTween(System.Boolean regenerateIfExists, System.Boolean andPlay) { }
        // RVA: 0x069769FC  token: 0x600000D
        public System.Collections.Generic.List<DG.Tweening.Tween> GetTweens() { }
        // RVA: 0x06976DC8  token: 0x600000E
        public System.Void SetAnimationTarget(UnityEngine.Component tweenTarget, System.Boolean useTweenTargetGameObjectForGroupOperations) { }
        // RVA: 0x069765D8  token: 0x600000F
        public virtual System.Void DOPlay() { }
        // RVA: 0x06976324  token: 0x6000010
        public virtual System.Void DOPlayBackwards() { }
        // RVA: 0x06976480  token: 0x6000011
        public virtual System.Void DOPlayForward() { }
        // RVA: 0x069761DC  token: 0x6000012
        public virtual System.Void DOPause() { }
        // RVA: 0x06976924  token: 0x6000013
        public virtual System.Void DOTogglePause() { }
        // RVA: 0x06976864  token: 0x6000014
        public virtual System.Void DORewind() { }
        // RVA: 0x069734E8  token: 0x6000015
        public virtual System.Void DORestart() { }
        // RVA: 0x069766FC  token: 0x6000016
        public virtual System.Void DORestart(System.Boolean fromHere) { }
        // RVA: 0x0697602C  token: 0x6000017
        public virtual System.Void DOComplete() { }
        // RVA: 0x06976128  token: 0x6000018
        public virtual System.Void DOKill() { }
        // RVA: 0x06976374  token: 0x6000019
        public System.Void DOPlayById(System.String id) { }
        // RVA: 0x0697622C  token: 0x600001A
        public System.Void DOPlayAllById(System.String id) { }
        // RVA: 0x06976190  token: 0x600001B
        public System.Void DOPauseAllById(System.String id) { }
        // RVA: 0x069762C4  token: 0x600001C
        public System.Void DOPlayBackwardsById(System.String id) { }
        // RVA: 0x06976278  token: 0x600001D
        public System.Void DOPlayBackwardsAllById(System.String id) { }
        // RVA: 0x06976420  token: 0x600001E
        public System.Void DOPlayForwardById(System.String id) { }
        // RVA: 0x069763D4  token: 0x600001F
        public System.Void DOPlayForwardAllById(System.String id) { }
        // RVA: 0x069764D0  token: 0x6000020
        public System.Void DOPlayNext() { }
        // RVA: 0x069767F8  token: 0x6000021
        public System.Void DORewindAndPlayNext() { }
        // RVA: 0x069767A0  token: 0x6000022
        public System.Void DORewindAllById(System.String id) { }
        // RVA: 0x06976688  token: 0x6000023
        public System.Void DORestartById(System.String id) { }
        // RVA: 0x06976628  token: 0x6000024
        public System.Void DORestartAllById(System.String id) { }
        // RVA: 0x069760C8  token: 0x6000025
        public System.Void DOKillById(System.String id) { }
        // RVA: 0x0697607C  token: 0x6000026
        public System.Void DOKillAllById(System.String id) { }
        // RVA: 0x06976EDC  token: 0x6000027
        public static DG.Tweening.DOTweenAnimation.TargetType TypeToDOTargetType(System.Type t) { }
        // RVA: 0x069747F8  token: 0x6000028
        public DG.Tweening.Tween CreateEditorPreview() { }
        // RVA: 0x069769C4  token: 0x6000029
        private UnityEngine.GameObject GetTweenGO() { }
        // RVA: 0x069769E0  token: 0x600002A
        private UnityEngine.GameObject GetTweenTarget() { }
        // RVA: 0x06976AE8  token: 0x600002B
        private System.Void ReEvaluateRelativeTween() { }
        // RVA: 0x0697708C  token: 0x600002C
        public System.Void .ctor() { }
        // RVA: 0x06973C40  token: 0x600002D
        private System.Void <CreateTween>b__54_0() { }

    }

    // TypeToken: 0x2000005  // size: 0x10
    public static class DOTweenAnimationExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x600002E
        public static System.Boolean IsSameOrSubclassOf(UnityEngine.Component t) { }

    }

    // TypeToken: 0x2000006  // size: 0x10
    public static class DOTweenProShortcuts
    {
        // Methods
        // RVA: 0x069776C0  token: 0x600002F
        private static System.Void .cctor() { }
        // RVA: 0x0697738C  token: 0x6000030
        public static DG.Tweening.Tweener DOSpiral(UnityEngine.Transform target, System.Single duration, System.Nullable<UnityEngine.Vector3> axis, DG.Tweening.SpiralMode mode, System.Single speed, System.Single frequency, System.Single depth, System.Boolean snapping) { }

    }

}

