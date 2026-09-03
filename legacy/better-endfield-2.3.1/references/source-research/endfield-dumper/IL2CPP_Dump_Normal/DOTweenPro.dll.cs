// ========================================================
// Dumped by @desirepro
// Assembly: DOTweenPro.dll
// Classes:  13
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

namespace DG.Tweening
{

    // TypeToken: 0x2000002  // size: 0x30
    public class DOTweenVisualManager : UnityEngine.MonoBehaviour
    {
        // Fields
        public DG.Tweening.Core.VisualManagerPreset preset;  // 0x18
        public DG.Tweening.Core.OnEnableBehaviour onEnableBehaviour;  // 0x1c
        public DG.Tweening.Core.OnDisableBehaviour onDisableBehaviour;  // 0x20
        private System.Boolean _requiresRestartFromSpawnPoint;  // 0x24
        private DG.Tweening.Core.ABSAnimationComponent _animComponent;  // 0x28

        // Methods
        // RVA: 0x06974008  token: 0x6000001
        private System.Void Awake() { }
        // RVA: 0x069742B0  token: 0x6000002
        private System.Void Update() { }
        // RVA: 0x069741F8  token: 0x6000003
        private System.Void OnEnable() { }
        // RVA: 0x06974050  token: 0x6000004
        private System.Void OnDisable() { }
        // RVA: 0x05393520  token: 0x6000005
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000003  // size: 0x14
    public sealed struct HandlesDrawMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static DG.Tweening.HandlesDrawMode Orthographic;  // const
        public static DG.Tweening.HandlesDrawMode Perspective;  // const

    }

    // TypeToken: 0x2000004  // size: 0x14
    public sealed struct HandlesType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static DG.Tweening.HandlesType Free;  // const
        public static DG.Tweening.HandlesType Full;  // const

    }

    // TypeToken: 0x2000005  // size: 0x14
    public sealed struct DOTweenInspectorMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static DG.Tweening.DOTweenInspectorMode Default;  // const
        public static DG.Tweening.DOTweenInspectorMode InfoAndWaypointsOnly;  // const
        public static DG.Tweening.DOTweenInspectorMode Developer;  // const
        public static DG.Tweening.DOTweenInspectorMode OnlyPath;  // const

    }

    // TypeToken: 0x2000006  // size: 0x150
    public class DOTweenPath : DG.Tweening.Core.ABSAnimationComponent
    {
        // Fields
        private static System.Action<DG.Tweening.DOTweenPath> OnReset;  // static @ 0x0
        public System.Single delay;  // 0x68
        public System.Single duration;  // 0x6c
        public DG.Tweening.Ease easeType;  // 0x70
        public UnityEngine.AnimationCurve easeCurve;  // 0x78
        public System.Int32 loops;  // 0x80
        public System.String id;  // 0x88
        public DG.Tweening.LoopType loopType;  // 0x90
        public DG.Tweening.Plugins.Options.OrientType orientType;  // 0x94
        public UnityEngine.Transform lookAtTransform;  // 0x98
        public UnityEngine.Vector3 lookAtPosition;  // 0xa0
        public System.Single lookAhead;  // 0xac
        public System.Boolean autoPlay;  // 0xb0
        public System.Boolean autoKill;  // 0xb1
        public System.Boolean relative;  // 0xb2
        public System.Boolean isLocal;  // 0xb3
        public System.Boolean isClosedPath;  // 0xb4
        public System.Int32 pathResolution;  // 0xb8
        public DG.Tweening.PathMode pathMode;  // 0xbc
        public DG.Tweening.AxisConstraint lockRotation;  // 0xc0
        public System.Boolean assignForwardAndUp;  // 0xc4
        public UnityEngine.Vector3 forwardDirection;  // 0xc8
        public UnityEngine.Vector3 upDirection;  // 0xd4
        public System.Boolean tweenRigidbody;  // 0xe0
        public System.Collections.Generic.List<UnityEngine.Vector3> wps;  // 0xe8
        public System.Collections.Generic.List<UnityEngine.Vector3> fullWps;  // 0xf0
        public DG.Tweening.Plugins.Core.PathCore.Path path;  // 0xf8
        public DG.Tweening.DOTweenInspectorMode inspectorMode;  // 0x100
        public DG.Tweening.PathType pathType;  // 0x104
        public DG.Tweening.HandlesType handlesType;  // 0x108
        public System.Boolean livePreview;  // 0x10c
        public DG.Tweening.HandlesDrawMode handlesDrawMode;  // 0x110
        public System.Single perspectiveHandleSize;  // 0x114
        public System.Boolean showIndexes;  // 0x118
        public System.Boolean showWpLength;  // 0x119
        public UnityEngine.Color pathColor;  // 0x11c
        public UnityEngine.Vector3 lastSrcPosition;  // 0x12c
        public UnityEngine.Quaternion lastSrcRotation;  // 0x138
        public System.Boolean wpsDropdown;  // 0x148
        public System.Single dropToFloorOffset;  // 0x14c
        private static System.Reflection.MethodInfo _miCreateTween;  // static @ 0x8

        // Events
        event System.Action<DG.Tweening.DOTweenPath> OnReset;

        // Methods
        // RVA: 0x06973578  token: 0x6000008
        private static System.Void Dispatch_OnReset(DG.Tweening.DOTweenPath path) { }
        // RVA: 0x06972600  token: 0x6000009
        private System.Void Awake() { }
        // RVA: 0x06973B28  token: 0x600000A
        private System.Void Reset() { }
        // RVA: 0x06973810  token: 0x600000B
        private System.Void OnDestroy() { }
        // RVA: 0x069734AC  token: 0x600000C
        public virtual System.Void DOPlay() { }
        // RVA: 0x06973440  token: 0x600000D
        public System.Void DOPlayById(System.String id) { }
        // RVA: 0x069733E8  token: 0x600000E
        public System.Void DOPlayAllById(System.String id) { }
        // RVA: 0x06973434  token: 0x600000F
        public virtual System.Void DOPlayBackwards() { }
        // RVA: 0x069734A0  token: 0x6000010
        public virtual System.Void DOPlayForward() { }
        // RVA: 0x069733AC  token: 0x6000011
        public virtual System.Void DOPause() { }
        // RVA: 0x0697356C  token: 0x6000012
        public virtual System.Void DOTogglePause() { }
        // RVA: 0x0697355C  token: 0x6000013
        public virtual System.Void DORewind() { }
        // RVA: 0x069734E8  token: 0x6000014
        public virtual System.Void DORestart() { }
        // RVA: 0x069734F8  token: 0x6000015
        public virtual System.Void DORestart(System.Boolean fromHere) { }
        // RVA: 0x06973340  token: 0x6000016
        public virtual System.Void DOComplete() { }
        // RVA: 0x0697339C  token: 0x6000017
        public virtual System.Void DOKill() { }
        // RVA: 0x06973350  token: 0x6000018
        public System.Void DOKillAllById(System.String id) { }
        // RVA: 0x069737C8  token: 0x6000019
        public DG.Tweening.Tween GetTween() { }
        // RVA: 0x069735C8  token: 0x600001A
        public UnityEngine.Vector3[] GetDrawPoints() { }
        // RVA: 0x06973644  token: 0x600001B
        private UnityEngine.Vector3[] GetFullWps() { }
        // RVA: 0x06973850  token: 0x600001C
        private System.Void ReEvaluateRelativeTween() { }
        // RVA: 0x06973C54  token: 0x600001D
        public System.Void .ctor() { }
        // RVA: 0x06973C40  token: 0x600001E
        private System.Void <Awake>b__44_0() { }

    }

    // TypeToken: 0x2000007  // size: 0x14
    public sealed struct SpiralMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static DG.Tweening.SpiralMode Expand;  // const
        public static DG.Tweening.SpiralMode ExpandThenContract;  // const

    }

}

namespace DG.Tweening.Core
{

    // TypeToken: 0x200000A  // size: 0x68
    public abstract class ABSAnimationComponent : UnityEngine.MonoBehaviour
    {
        // Fields
        public DG.Tweening.UpdateType updateType;  // 0x18
        public System.Boolean isSpeedBased;  // 0x1c
        public System.Boolean hasOnStart;  // 0x1d
        public System.Boolean hasOnPlay;  // 0x1e
        public System.Boolean hasOnUpdate;  // 0x1f
        public System.Boolean hasOnStepComplete;  // 0x20
        public System.Boolean hasOnComplete;  // 0x21
        public System.Boolean hasOnTweenCreated;  // 0x22
        public System.Boolean hasOnRewind;  // 0x23
        public UnityEngine.Events.UnityEvent onStart;  // 0x28
        public UnityEngine.Events.UnityEvent onPlay;  // 0x30
        public UnityEngine.Events.UnityEvent onUpdate;  // 0x38
        public UnityEngine.Events.UnityEvent onStepComplete;  // 0x40
        public UnityEngine.Events.UnityEvent onComplete;  // 0x48
        public UnityEngine.Events.UnityEvent onTweenCreated;  // 0x50
        public UnityEngine.Events.UnityEvent onRewind;  // 0x58
        public DG.Tweening.Tween tween;  // 0x60

        // Methods
        // RVA: -1  // abstract  token: 0x600002B
        public virtual System.Void DOPlay() { }
        // RVA: -1  // abstract  token: 0x600002C
        public virtual System.Void DOPlayBackwards() { }
        // RVA: -1  // abstract  token: 0x600002D
        public virtual System.Void DOPlayForward() { }
        // RVA: -1  // abstract  token: 0x600002E
        public virtual System.Void DOPause() { }
        // RVA: -1  // abstract  token: 0x600002F
        public virtual System.Void DOTogglePause() { }
        // RVA: -1  // abstract  token: 0x6000030
        public virtual System.Void DORewind() { }
        // RVA: -1  // abstract  token: 0x6000031
        public virtual System.Void DORestart() { }
        // RVA: -1  // abstract  token: 0x6000032
        public virtual System.Void DORestart(System.Boolean fromHere) { }
        // RVA: -1  // abstract  token: 0x6000033
        public virtual System.Void DOComplete() { }
        // RVA: -1  // abstract  token: 0x6000034
        public virtual System.Void DOKill() { }
        // RVA: 0x05393520  token: 0x6000035
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200000B  // size: 0x14
    public sealed struct OnDisableBehaviour
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static DG.Tweening.Core.OnDisableBehaviour None;  // const
        public static DG.Tweening.Core.OnDisableBehaviour Pause;  // const
        public static DG.Tweening.Core.OnDisableBehaviour Rewind;  // const
        public static DG.Tweening.Core.OnDisableBehaviour Kill;  // const
        public static DG.Tweening.Core.OnDisableBehaviour KillAndComplete;  // const
        public static DG.Tweening.Core.OnDisableBehaviour DestroyGameObject;  // const

    }

    // TypeToken: 0x200000C  // size: 0x14
    public sealed struct OnEnableBehaviour
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static DG.Tweening.Core.OnEnableBehaviour None;  // const
        public static DG.Tweening.Core.OnEnableBehaviour Play;  // const
        public static DG.Tweening.Core.OnEnableBehaviour Restart;  // const
        public static DG.Tweening.Core.OnEnableBehaviour RestartFromSpawnPoint;  // const

    }

    // TypeToken: 0x200000D  // size: 0x14
    public sealed struct VisualManagerPreset
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static DG.Tweening.Core.VisualManagerPreset Custom;  // const
        public static DG.Tweening.Core.VisualManagerPreset PoolingSystem;  // const

    }

}

namespace DG.Tweening.Plugins
{

    // TypeToken: 0x2000008  // size: 0x38
    public sealed struct SpiralOptions : DG.Tweening.Plugins.Options.IPlugOptions
    {
        // Fields
        public System.Single depth;  // 0x10
        public System.Single frequency;  // 0x14
        public System.Single speed;  // 0x18
        public DG.Tweening.SpiralMode mode;  // 0x1c
        public System.Boolean snapping;  // 0x20
        private System.Single unit;  // 0x24
        private UnityEngine.Quaternion axisQ;  // 0x28

        // Methods
        // RVA: 0x04D8C990  token: 0x600001F
        public virtual System.Void Reset() { }

    }

    // TypeToken: 0x2000009  // size: 0x10
    public class SpiralPlugin : DG.Tweening.Plugins.Core.ABSTweenPlugin`3
    {
        // Fields
        public static readonly UnityEngine.Vector3 DefaultDirection;  // static @ 0x0

        // Methods
        // RVA: 0x041E1670  token: 0x6000020
        public virtual System.Void Reset(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.SpiralOptions> t) { }
        // RVA: 0x041E1670  token: 0x6000021
        public virtual System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.SpiralOptions> t, System.Boolean isRelative) { }
        // RVA: 0x041E1670  token: 0x6000022
        public virtual System.Void SetFrom(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.SpiralOptions> t, UnityEngine.Vector3 fromValue, System.Boolean setImmediately, System.Boolean isRelative) { }
        // RVA: 0x06974674  token: 0x6000023
        public static DG.Tweening.Plugins.Core.ABSTweenPlugin<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.SpiralOptions> Get() { }
        // RVA: 0x04D8C9A0  token: 0x6000024
        public virtual UnityEngine.Vector3 ConvertToStartValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.SpiralOptions> t, UnityEngine.Vector3 value) { }
        // RVA: 0x041E1670  token: 0x6000025
        public virtual System.Void SetRelativeEndValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.SpiralOptions> t) { }
        // RVA: 0x069746A4  token: 0x6000026
        public virtual System.Void SetChangeValue(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3,UnityEngine.Vector3,DG.Tweening.Plugins.SpiralOptions> t) { }
        // RVA: 0x04D8C9C0  token: 0x6000027
        public virtual System.Single GetSpeedBasedDuration(DG.Tweening.Plugins.SpiralOptions options, System.Single unitsXSecond, UnityEngine.Vector3 changeValue) { }
        // RVA: 0x0697432C  token: 0x6000028
        public virtual System.Void EvaluateAndApply(DG.Tweening.Plugins.SpiralOptions options, DG.Tweening.Tween t, System.Boolean isRelative, DG.Tweening.Core.DOGetter<UnityEngine.Vector3> getter, DG.Tweening.Core.DOSetter<UnityEngine.Vector3> setter, System.Single elapsed, UnityEngine.Vector3 startValue, UnityEngine.Vector3 changeValue, System.Single duration, System.Boolean usingInversePosition, System.Int32 newCompletedSteps, DG.Tweening.Core.Enums.UpdateNotice updateNotice) { }
        // RVA: 0x06974784  token: 0x6000029
        public System.Void .ctor() { }
        // RVA: 0x06974734  token: 0x600002A
        private static System.Void .cctor() { }

    }

}

