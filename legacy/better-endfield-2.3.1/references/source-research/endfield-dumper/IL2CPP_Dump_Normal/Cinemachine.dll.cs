// ========================================================
// Dumped by @desirepro
// Assembly: Cinemachine.dll
// Classes:  204
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000002  // size: 0x40
    public class CinemachineCameraOffset : Cinemachine.CinemachineExtension
    {
        // Fields
        public UnityEngine.Vector3 m_Offset;  // 0x28
        public Cinemachine.CinemachineCore.Stage m_ApplyAfter;  // 0x34
        public System.Boolean m_PreserveComposition;  // 0x38

        // Methods
        // RVA: 0x0678E3A8  token: 0x6000001
        protected virtual System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime) { }
        // RVA: 0x0678E6D0  token: 0x6000002
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000003  // size: 0x48
    public class CinemachineRecomposer : Cinemachine.CinemachineExtension
    {
        // Fields
        public Cinemachine.CinemachineCore.Stage m_ApplyAfter;  // 0x28
        public System.Single m_Tilt;  // 0x2c
        public System.Single m_Pan;  // 0x30
        public System.Single m_Dutch;  // 0x34
        public System.Single m_ZoomScale;  // 0x38
        public System.Single m_FollowAttachment;  // 0x3c
        public System.Single m_LookAtAttachment;  // 0x40

        // Methods
        // RVA: 0x04D879E0  token: 0x6000003
        private System.Void Reset() { }
        // RVA: 0x0679A754  token: 0x6000004
        private System.Void OnValidate() { }
        // RVA: 0x04073A30  token: 0x6000005
        public virtual System.Void PrePipelineMutateCameraStateCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CameraState& curState, System.Single deltaTime) { }
        // RVA: 0x031FE9B0  token: 0x6000006
        protected virtual System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime) { }
        // RVA: 0x05393520  token: 0x6000007
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000004  // size: 0x10
    public static class CinemachineBeyondCommunicator
    {
        // Fields
        private static System.Func<Cinemachine.CinemachineVirtualCameraBase> <GetLevelCamera>k__BackingField;  // static @ 0x0

        // Properties
        System.Func<Cinemachine.CinemachineVirtualCameraBase> GetLevelCamera { get; /* RVA: 0x0678C1C0 */ set; /* RVA: 0x0678C1F8 */ }

    }

    // TypeToken: 0x2000005  // size: 0x30
    public class CinemachineTouchInputMapper : UnityEngine.MonoBehaviour
    {
        // Fields
        public System.Single TouchSensitivityX;  // 0x18
        public System.Single TouchSensitivityY;  // 0x1c
        public System.String TouchXInputMapTo;  // 0x20
        public System.String TouchYInputMapTo;  // 0x28

        // Methods
        // RVA: 0x0679AD50  token: 0x600000A
        private System.Void Start() { }
        // RVA: 0x0679ACB0  token: 0x600000B
        private System.Single GetInputAxis(System.String axisName) { }
        // RVA: 0x0679AE00  token: 0x600000C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x20
    public sealed class CinemachineMixer : UnityEngine.Playables.PlayableBehaviour
    {
        // Fields
        private Cinemachine.CinemachineBrain mBrain;  // 0x10
        private System.Int32 mBrainOverrideId;  // 0x18
        private System.Boolean mPreviewPlay;  // 0x1c

        // Methods
        // RVA: 0x06798578  token: 0x600000D
        public virtual System.Void OnPlayableDestroy(UnityEngine.Playables.Playable playable) { }
        // RVA: 0x04D87900  token: 0x600000E
        public virtual System.Void PrepareFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info) { }
        // RVA: 0x067985EC  token: 0x600000F
        public virtual System.Void ProcessFrame(UnityEngine.Playables.Playable playable, UnityEngine.Playables.FrameData info, System.Object playerData) { }
        // RVA: 0x06798528  token: 0x6000010
        private System.Single GetDeltaTime(System.Single deltaTime) { }
        // RVA: 0x04D87910  token: 0x6000011
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x38
    public sealed class CinemachineShot : UnityEngine.Playables.PlayableAsset, UnityEngine.Timeline.IPropertyPreview, UnityEngine.Timeline.ITimelineClipAsset
    {
        // Fields
        public System.Boolean DisableEvaluateTwiceWhenEnabled;  // 0x18
        public System.Boolean UseVcamBeforeTimeline;  // 0x19
        public System.Boolean UseLevelCamera;  // 0x1a
        public System.String DisplayName;  // 0x20
        public UnityEngine.ExposedReference<Cinemachine.CinemachineVirtualCameraBase> VirtualCamera;  // 0x28

        // Properties
        UnityEngine.Timeline.ClipCaps clipCaps { get; /* RVA: 0x0679AC9C */ }

        // Methods
        // RVA: 0x0679A7E0  token: 0x6000013
        public virtual UnityEngine.Playables.Playable CreatePlayable(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject owner) { }
        // RVA: 0x0679AB00  token: 0x6000014
        public virtual System.Void GatherProperties(UnityEngine.Playables.PlayableDirector director, UnityEngine.Timeline.IPropertyCollector driver) { }
        // RVA: 0x03573370  token: 0x6000015
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000008  // size: 0x18
    public sealed class CinemachineShotPlayable : UnityEngine.Playables.PlayableBehaviour
    {
        // Fields
        public Cinemachine.CinemachineVirtualCameraBase VirtualCamera;  // 0x10

        // Properties
        System.Boolean IsValid { get; /* RVA: 0x0679A790 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000017
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000009  // size: 0xC0
    public class CinemachineTrack : UnityEngine.Timeline.TrackAsset
    {
        // Methods
        // RVA: 0x0679AE74  token: 0x6000018
        public virtual UnityEngine.Playables.Playable CreateTrackMixer(UnityEngine.Playables.PlayableGraph graph, UnityEngine.GameObject go, System.Int32 inputCount) { }
        // RVA: 0x0679AF54  token: 0x6000019
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000C  // size: 0x38
    public sealed struct Instruction
    {
        // Fields
        public Cinemachine.CinemachineVirtualCameraBase m_VirtualCamera;  // 0x10
        public System.Single m_Hold;  // 0x18
        public Cinemachine.CinemachineBlendDefinition m_Blend;  // 0x20

    }

    // TypeToken: 0x200000E  // size: 0x14
    public sealed struct UpdateMethod
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineBrain.UpdateMethod FixedUpdate;  // const
        public static Cinemachine.CinemachineBrain.UpdateMethod LateUpdate;  // const
        public static Cinemachine.CinemachineBrain.UpdateMethod SmartUpdate;  // const
        public static Cinemachine.CinemachineBrain.UpdateMethod ManualUpdate;  // const

    }

    // TypeToken: 0x200000F  // size: 0x14
    public sealed struct BrainUpdateMethod
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineBrain.BrainUpdateMethod FixedUpdate;  // const
        public static Cinemachine.CinemachineBrain.BrainUpdateMethod LateUpdate;  // const

    }

    // TypeToken: 0x2000010  // size: 0x30
    public class BrainEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x03FCFD10  token: 0x6000061
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000011  // size: 0x30
    public class VcamActivatedEvent : UnityEngine.Events.UnityEvent`2
    {
        // Methods
        // RVA: 0x03FCFB00  token: 0x6000062
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000012  // size: 0x38
    public class BrainFrame
    {
        // Fields
        public System.Int32 id;  // 0x10
        public Cinemachine.CinemachineBlend blend;  // 0x18
        public Cinemachine.CinemachineBlend workingBlend;  // 0x20
        public Cinemachine.BlendSourceVirtualCamera workingBlendSource;  // 0x28
        public System.Single deltaTimeOverride;  // 0x30

        // Properties
        System.Boolean Active { get; /* RVA: 0x0678B750 */ }

        // Methods
        // RVA: 0x03FCFD50  token: 0x6000064
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000013  // size: 0x28
    public sealed class <AfterPhysics>d__36 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public Cinemachine.CinemachineBrain <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000065
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000066
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03D30400  token: 0x6000067
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0679BB74  token: 0x6000069
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000015  // size: 0x18
    public sealed struct Pair
    {
        // Fields
        public System.Int32 a;  // 0x10
        public System.Single b;  // 0x14

    }

    // TypeToken: 0x2000016  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Cinemachine.CinemachineClearShot.<>c <>9;  // static @ 0x0
        public static System.Comparison<Cinemachine.CinemachineClearShot.Pair> <>9__47_0;  // static @ 0x8

        // Methods
        // RVA: 0x0679BBC0  token: 0x6000085
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000086
        public System.Void .ctor() { }
        // RVA: 0x04D87A20  token: 0x6000087
        private System.Int32 <Randomize>b__47_0(Cinemachine.CinemachineClearShot.Pair p1, Cinemachine.CinemachineClearShot.Pair p2) { }

    }

    // TypeToken: 0x2000018  // size: 0x14
    public sealed struct ResolutionStrategy
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineCollider.ResolutionStrategy PullCameraForward;  // const
        public static Cinemachine.CinemachineCollider.ResolutionStrategy PreserveCameraHeight;  // const
        public static Cinemachine.CinemachineCollider.ResolutionStrategy PreserveCameraDistance;  // const

    }

    // TypeToken: 0x2000019  // size: 0x38
    public class VcamExtraState
    {
        // Fields
        public UnityEngine.Vector3 previousDisplacement;  // 0x10
        public System.Single previousLookAt2DisplacedPosLen;  // 0x1c
        public System.Boolean shouldPerformCollisionDamping;  // 0x20
        public System.Boolean previousHitSomething;  // 0x21
        public System.Boolean targetObscured;  // 0x22
        public System.Single occlusionStartTime;  // 0x24
        public System.Collections.Generic.List<UnityEngine.Vector3> debugResolutionPath;  // 0x28
        private System.Single m_SmoothedDistance;  // 0x30
        private System.Single m_SmoothedTime;  // 0x34

        // Methods
        // RVA: 0x041E1670  token: 0x600009D
        public System.Void AddPointToDebugPath(UnityEngine.Vector3 p) { }
        // RVA: 0x0403BFB0  token: 0x600009E
        public System.Single ApplyDistanceSmoothing(System.Single distance, System.Single smoothingTime) { }
        // RVA: 0x0679BC24  token: 0x600009F
        public System.Void UpdateDistanceSmoothing(System.Single distance) { }
        // RVA: 0x03FC2820  token: 0x60000A0
        public System.Void ResetDistanceSmoothing(System.Single smoothingTime) { }
        // RVA: 0x041E1670  token: 0x60000A1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001B  // size: 0x14
    public sealed struct Mode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineConfiner.Mode Confine2D;  // const
        public static Cinemachine.CinemachineConfiner.Mode Confine3D;  // const

    }

    // TypeToken: 0x200001C  // size: 0x20
    public class VcamExtraState
    {
        // Fields
        public UnityEngine.Vector3 m_previousDisplacement;  // 0x10
        public System.Single confinerDisplacement;  // 0x1c

        // Methods
        // RVA: 0x041E1670  token: 0x60000AE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001E  // size: 0x38
    public class VcamExtraState
    {
        // Fields
        public UnityEngine.Vector3 m_PreviousDisplacement;  // 0x10
        public UnityEngine.Vector3 m_DampedDisplacement;  // 0x1c
        public Cinemachine.ConfinerOven.BakedSolution m_BakedSolution;  // 0x28
        public Cinemachine.CinemachineVirtualCameraBase m_vcam;  // 0x30

        // Methods
        // RVA: 0x041E1670  token: 0x60000B6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001F  // size: 0xF8
    public sealed struct ShapeCache
    {
        // Fields
        public Cinemachine.ConfinerOven m_confinerOven;  // 0x10
        public System.Collections.Generic.List<System.Collections.Generic.List<UnityEngine.Vector2>> m_OriginalPath;  // 0x18
        public UnityEngine.Matrix4x4 m_DeltaWorldToBaked;  // 0x20
        public UnityEngine.Matrix4x4 m_DeltaBakedToWorld;  // 0x60
        private System.Single m_aspectRatio;  // 0xa0
        private System.Single m_maxWindowSize;  // 0xa4
        private System.Single m_maxComputationTimePerFrameInSeconds;  // 0xa8
        private UnityEngine.Matrix4x4 m_bakedToWorld;  // 0xac
        private UnityEngine.Collider2D m_boundingShape2D;  // 0xf0

        // Methods
        // RVA: 0x0679B384  token: 0x60000B7
        public System.Void Invalidate() { }
        // RVA: 0x0679B53C  token: 0x60000B8
        public System.Boolean ValidateCache(UnityEngine.Collider2D boundingShape2D, System.Single maxWindowSize, System.Single aspectRatio, System.Boolean& confinerStateChanged) { }
        // RVA: 0x0679B424  token: 0x60000B9
        private System.Boolean IsValid(UnityEngine.Collider2D& boundingShape2D, System.Single& aspectRatio, System.Single& maxOrthoSize) { }
        // RVA: 0x0679B19C  token: 0x60000BA
        private System.Void CalculateDeltaTransformationMatrix() { }

    }

    // TypeToken: 0x2000021  // size: 0x14
    public sealed struct UpdateMethod
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineDollyCart.UpdateMethod Update;  // const
        public static Cinemachine.CinemachineDollyCart.UpdateMethod FixedUpdate;  // const
        public static Cinemachine.CinemachineDollyCart.UpdateMethod LateUpdate;  // const

    }

    // TypeToken: 0x2000024  // size: 0x18
    public class VcamExtraState
    {
        // Fields
        public System.Single m_previousFrameZoom;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60000CB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000026  // size: 0x18
    public sealed struct Orbit
    {
        // Fields
        public System.Single m_Height;  // 0x10
        public System.Single m_Radius;  // 0x14

        // Methods
        // RVA: 0x04D87A10  token: 0x60000EE
        public System.Void .ctor(System.Single h, System.Single r) { }

    }

    // TypeToken: 0x2000027  // size: 0x80
    public sealed class CreateRigDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0679AFA0  token: 0x60000EF
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014F3120  token: 0x60000F0
        public virtual Cinemachine.CinemachineVirtualCamera Invoke(Cinemachine.CinemachineFreeLook vcam, System.String name, Cinemachine.CinemachineVirtualCamera copyFrom) { }

    }

    // TypeToken: 0x2000028  // size: 0x80
    public sealed class DestroyRigDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0679B0C8  token: 0x60000F1
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x60000F2
        public virtual System.Void Invoke(UnityEngine.GameObject rig) { }

    }

    // TypeToken: 0x200002B  // size: 0x2C
    public sealed struct Waypoint
    {
        // Fields
        public UnityEngine.Vector3 position;  // 0x10
        public UnityEngine.Vector3 tangent;  // 0x1c
        public System.Single roll;  // 0x28

    }

    // TypeToken: 0x200002F  // size: 0x20
    public sealed struct Waypoint
    {
        // Fields
        public UnityEngine.Vector3 position;  // 0x10
        public System.Single roll;  // 0x1c

        // Properties
        UnityEngine.Vector4 AsVector4 { get; /* RVA: 0x0328FB00 */ }

        // Methods
        // RVA: 0x067A9D14  token: 0x6000126
        private static Cinemachine.CinemachineSmoothPath.Waypoint FromVector4(UnityEngine.Vector4 v) { }

    }

    // TypeToken: 0x2000031  // size: 0x28
    public sealed struct Instruction
    {
        // Fields
        public System.Int32 m_FullHash;  // 0x10
        public Cinemachine.CinemachineVirtualCameraBase m_VirtualCamera;  // 0x18
        public System.Single m_ActivateAfter;  // 0x20
        public System.Single m_MinDuration;  // 0x24

    }

    // TypeToken: 0x2000032  // size: 0x18
    public sealed struct ParentHash
    {
        // Fields
        public System.Int32 m_Hash;  // 0x10
        public System.Int32 m_ParentHash;  // 0x14

    }

    // TypeToken: 0x2000033  // size: 0x18
    public sealed struct HashPair
    {
        // Fields
        public System.Int32 parentHash;  // 0x10
        public System.Int32 hash;  // 0x14

    }

    // TypeToken: 0x2000035  // size: 0x14
    public sealed struct FillStrategy
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineStoryboard.FillStrategy BestFit;  // const
        public static Cinemachine.CinemachineStoryboard.FillStrategy CropImageToFit;  // const
        public static Cinemachine.CinemachineStoryboard.FillStrategy StretchToFit;  // const

    }

    // TypeToken: 0x2000036  // size: 0x38
    public class CanvasInfo
    {
        // Fields
        public UnityEngine.GameObject mCanvas;  // 0x10
        public UnityEngine.Canvas mCanvasComponent;  // 0x18
        public Cinemachine.CinemachineBrain mCanvasParent;  // 0x20
        public UnityEngine.RectTransform mViewport;  // 0x28
        public UnityEngine.UI.RawImage mRawImage;  // 0x30

        // Methods
        // RVA: 0x041E1670  token: 0x600014F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000037  // size: 0x14
    public sealed struct StoryboardRenderMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineStoryboard.StoryboardRenderMode ScreenSpaceOverlay;  // const
        public static Cinemachine.CinemachineStoryboard.StoryboardRenderMode ScreenSpaceCamera;  // const

    }

    // TypeToken: 0x200003A  // size: 0x20
    public sealed struct Target
    {
        // Fields
        public UnityEngine.Transform target;  // 0x10
        public System.Single weight;  // 0x18
        public System.Single radius;  // 0x1c

    }

    // TypeToken: 0x200003B  // size: 0x14
    public sealed struct PositionMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineTargetGroup.PositionMode GroupCenter;  // const
        public static Cinemachine.CinemachineTargetGroup.PositionMode GroupAverage;  // const

    }

    // TypeToken: 0x200003C  // size: 0x14
    public sealed struct RotationMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineTargetGroup.RotationMode Manual;  // const
        public static Cinemachine.CinemachineTargetGroup.RotationMode GroupAverage;  // const

    }

    // TypeToken: 0x200003D  // size: 0x14
    public sealed struct UpdateMethod
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineTargetGroup.UpdateMethod Update;  // const
        public static Cinemachine.CinemachineTargetGroup.UpdateMethod FixedUpdate;  // const
        public static Cinemachine.CinemachineTargetGroup.UpdateMethod LateUpdate;  // const

    }

    // TypeToken: 0x200003F  // size: 0x80
    public sealed class CreatePipelineDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0679AFA0  token: 0x6000189
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014F3120  token: 0x600018A
        public virtual UnityEngine.Transform Invoke(Cinemachine.CinemachineVirtualCamera vcam, System.String name, Cinemachine.CinemachineComponentBase[] copyFrom) { }

    }

    // TypeToken: 0x2000040  // size: 0x80
    public sealed class DestroyPipelineDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0679B0C8  token: 0x600018B
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x600018C
        public virtual System.Void Invoke(UnityEngine.GameObject pipeline) { }

    }

    // TypeToken: 0x2000041  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Cinemachine.CinemachineVirtualCamera.<>c <>9;  // static @ 0x0
        public static System.Comparison<Cinemachine.CinemachineComponentBase> <>9__39_0;  // static @ 0x8
        public static System.Func<Cinemachine.CinemachineComponentBase,System.Boolean> <>9__48_0;  // static @ 0x10

        // Methods
        // RVA: 0x04D4C1B0  token: 0x600018D
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600018E
        public System.Void .ctor() { }
        // RVA: 0x0389DF10  token: 0x600018F
        private System.Int32 <UpdateComponentPipeline>b__39_0(Cinemachine.CinemachineComponentBase c1, Cinemachine.CinemachineComponentBase c2) { }
        // RVA: 0x067A9A18  token: 0x6000190
        private System.Boolean <RequiresUserInput>b__48_0(Cinemachine.CinemachineComponentBase c) { }

    }

    // TypeToken: 0x2000045  // size: 0x60
    public sealed struct FovCache
    {
        // Fields
        public UnityEngine.Rect mFovSoftGuideRect;  // 0x10
        public UnityEngine.Rect mFovHardGuideRect;  // 0x20
        public System.Single mFovH;  // 0x30
        public System.Single mFov;  // 0x34
        private System.Single mOrthoSizeOverDistance;  // 0x38
        private System.Single mAspect;  // 0x3c
        private UnityEngine.Rect mSoftGuideRect;  // 0x40
        private UnityEngine.Rect mHardGuideRect;  // 0x50

        // Methods
        // RVA: 0x032499E0  token: 0x60001B9
        public System.Void UpdateCache(Cinemachine.LensSettings lens, UnityEngine.Rect softGuide, UnityEngine.Rect hardGuide, System.Single targetDistance) { }
        // RVA: 0x0434E5D0  token: 0x60001BA
        private UnityEngine.Rect ScreenToFOV(UnityEngine.Rect rScreen, System.Single fov, System.Single fovH, System.Single aspect) { }

    }

    // TypeToken: 0x2000047  // size: 0x14
    public sealed struct FramingMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineFramingTransposer.FramingMode Horizontal;  // const
        public static Cinemachine.CinemachineFramingTransposer.FramingMode Vertical;  // const
        public static Cinemachine.CinemachineFramingTransposer.FramingMode HorizontalAndVertical;  // const
        public static Cinemachine.CinemachineFramingTransposer.FramingMode None;  // const

    }

    // TypeToken: 0x2000048  // size: 0x14
    public sealed struct AdjustmentMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineFramingTransposer.AdjustmentMode ZoomOnly;  // const
        public static Cinemachine.CinemachineFramingTransposer.AdjustmentMode DollyOnly;  // const
        public static Cinemachine.CinemachineFramingTransposer.AdjustmentMode DollyThenZoom;  // const

    }

    // TypeToken: 0x200004A  // size: 0x14
    public sealed struct FramingMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineGroupComposer.FramingMode Horizontal;  // const
        public static Cinemachine.CinemachineGroupComposer.FramingMode Vertical;  // const
        public static Cinemachine.CinemachineGroupComposer.FramingMode HorizontalAndVertical;  // const

    }

    // TypeToken: 0x200004B  // size: 0x14
    public sealed struct AdjustmentMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineGroupComposer.AdjustmentMode ZoomOnly;  // const
        public static Cinemachine.CinemachineGroupComposer.AdjustmentMode DollyOnly;  // const
        public static Cinemachine.CinemachineGroupComposer.AdjustmentMode DollyThenZoom;  // const

    }

    // TypeToken: 0x2000050  // size: 0x14
    public sealed struct HeadingDefinition
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineOrbitalTransposer.Heading.HeadingDefinition PositionDelta;  // const
        public static Cinemachine.CinemachineOrbitalTransposer.Heading.HeadingDefinition Velocity;  // const
        public static Cinemachine.CinemachineOrbitalTransposer.Heading.HeadingDefinition TargetForward;  // const
        public static Cinemachine.CinemachineOrbitalTransposer.Heading.HeadingDefinition WorldForward;  // const

    }

    // TypeToken: 0x200004F  // size: 0x1C
    public sealed struct Heading
    {
        // Fields
        public Cinemachine.CinemachineOrbitalTransposer.Heading.HeadingDefinition m_Definition;  // 0x10
        public System.Int32 m_VelocityFilterStrength;  // 0x14
        public System.Single m_Bias;  // 0x18

        // Methods
        // RVA: 0x04D87E60  token: 0x60001F5
        public System.Void .ctor(Cinemachine.CinemachineOrbitalTransposer.Heading.HeadingDefinition def, System.Int32 filterStrength, System.Single bias) { }

    }

    // TypeToken: 0x2000051  // size: 0x80
    public sealed class UpdateHeadingDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x067A9BEC  token: 0x60001F6
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x067A9BB4  token: 0x60001F7
        public virtual System.Single Invoke(Cinemachine.CinemachineOrbitalTransposer orbital, System.Single deltaTime, UnityEngine.Vector3 up) { }

    }

    // TypeToken: 0x2000052  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Cinemachine.CinemachineOrbitalTransposer.<>c <>9;  // static @ 0x0
        public static Cinemachine.CinemachineOrbitalTransposer.UpdateHeadingDelegate <>9__30_0;  // static @ 0x8

        // Methods
        // RVA: 0x067A9B50  token: 0x60001F8
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60001F9
        public System.Void .ctor() { }
        // RVA: 0x067A9A80  token: 0x60001FA
        private System.Single <.ctor>b__30_0(Cinemachine.CinemachineOrbitalTransposer orbital, System.Single deltaTime, UnityEngine.Vector3 up) { }

    }

    // TypeToken: 0x2000054  // size: 0x14
    public sealed struct RecenterTargetMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachinePOV.RecenterTargetMode None;  // const
        public static Cinemachine.CinemachinePOV.RecenterTargetMode FollowTargetForward;  // const
        public static Cinemachine.CinemachinePOV.RecenterTargetMode LookAtTargetForward;  // const

    }

    // TypeToken: 0x2000057  // size: 0x14
    public sealed struct CameraUpMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineTrackedDolly.CameraUpMode Default;  // const
        public static Cinemachine.CinemachineTrackedDolly.CameraUpMode Path;  // const
        public static Cinemachine.CinemachineTrackedDolly.CameraUpMode PathNoRoll;  // const
        public static Cinemachine.CinemachineTrackedDolly.CameraUpMode FollowTarget;  // const
        public static Cinemachine.CinemachineTrackedDolly.CameraUpMode FollowTargetNoRoll;  // const

    }

    // TypeToken: 0x2000058  // size: 0x20
    public sealed struct AutoDolly
    {
        // Fields
        public System.Boolean m_Enabled;  // 0x10
        public System.Single m_PositionOffset;  // 0x14
        public System.Int32 m_SearchRadius;  // 0x18
        public System.Int32 m_SearchResolution;  // 0x1c

        // Methods
        // RVA: 0x04D87EF0  token: 0x6000215
        public System.Void .ctor(System.Boolean enabled, System.Single positionOffset, System.Int32 searchRadius, System.Int32 stepsPerSegment) { }

    }

    // TypeToken: 0x200005A  // size: 0x14
    public sealed struct BindingMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineTransposer.BindingMode LockToTargetOnAssign;  // const
        public static Cinemachine.CinemachineTransposer.BindingMode LockToTargetWithWorldUp;  // const
        public static Cinemachine.CinemachineTransposer.BindingMode LockToTargetNoRoll;  // const
        public static Cinemachine.CinemachineTransposer.BindingMode LockToTarget;  // const
        public static Cinemachine.CinemachineTransposer.BindingMode WorldSpace;  // const
        public static Cinemachine.CinemachineTransposer.BindingMode SimpleFollowWithWorldUp;  // const

    }

    // TypeToken: 0x200005B  // size: 0x14
    public sealed struct AngularDampingMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineTransposer.AngularDampingMode Euler;  // const
        public static Cinemachine.CinemachineTransposer.AngularDampingMode Quaternion;  // const

    }

    // TypeToken: 0x200005D  // size: 0x14
    public sealed struct SpeedMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.AxisState.SpeedMode MaxSpeed;  // const
        public static Cinemachine.AxisState.SpeedMode InputValueGain;  // const

    }

    // TypeToken: 0x200005E
    public interface IInputAxisProvider
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000232
        public virtual System.Single GetAxisValue(System.Int32 axis) { }

    }

    // TypeToken: 0x200005F  // size: 0x2C
    public sealed struct Recentering
    {
        // Fields
        public System.Boolean m_enabled;  // 0x10
        public System.Single m_WaitTime;  // 0x14
        public System.Single m_RecenteringTime;  // 0x18
        private System.Single mLastAxisInputTime;  // 0x1c
        private System.Single mRecenteringVelocity;  // 0x20
        private System.Int32 m_LegacyHeadingDefinition;  // 0x24
        private System.Int32 m_LegacyVelocityFilterStrength;  // 0x28

        // Methods
        // RVA: 0x04D88080  token: 0x6000233
        public System.Void .ctor(System.Boolean enabled, System.Single waitTime, System.Single recenteringTime) { }
        // RVA: 0x067B1C84  token: 0x6000234
        public System.Void Validate() { }
        // RVA: 0x067B1A6C  token: 0x6000235
        public System.Void CancelRecentering() { }
        // RVA: 0x067B1AC0  token: 0x6000236
        public System.Void DoRecentering(Cinemachine.AxisState& axis, System.Single deltaTime, System.Single recenterTarget) { }
        // RVA: 0x067B1C5C  token: 0x6000237
        private System.Boolean LegacyUpgrade(System.Int32& heading, System.Int32& velocityFilter) { }

    }

    // TypeToken: 0x2000061  // size: 0x30
    public sealed struct HGDOFParams
    {
        // Fields
        public System.Boolean enabledForVirtualCamera;  // 0x10
        public System.Boolean enable;  // 0x11
        public System.Single nearRadius;  // 0x14
        public System.Single nearFocusStart;  // 0x18
        public System.Single nearFocusEnd;  // 0x1c
        public System.Single farRadius;  // 0x20
        public System.Single farFocusStart;  // 0x24
        public System.Single farFocusEnd;  // 0x28
        public System.Single temporalFactor;  // 0x2c

    }

    // TypeToken: 0x2000062  // size: 0x14
    public sealed struct BlendHintValue
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CameraState.BlendHintValue Nothing;  // const
        public static Cinemachine.CameraState.BlendHintValue NoPosition;  // const
        public static Cinemachine.CameraState.BlendHintValue NoOrientation;  // const
        public static Cinemachine.CameraState.BlendHintValue NoTransform;  // const
        public static Cinemachine.CameraState.BlendHintValue SphericalPositionBlend;  // const
        public static Cinemachine.CameraState.BlendHintValue CylindricalPositionBlend;  // const
        public static Cinemachine.CameraState.BlendHintValue RadialAimBlend;  // const
        public static Cinemachine.CameraState.BlendHintValue IgnoreLookAtTarget;  // const
        public static Cinemachine.CameraState.BlendHintValue NoLens;  // const

    }

    // TypeToken: 0x2000063  // size: 0x20
    public sealed struct CustomBlendable
    {
        // Fields
        public UnityEngine.Object m_Custom;  // 0x10
        public System.Single m_Weight;  // 0x18

        // Methods
        // RVA: 0x067B1770  token: 0x6000249
        public System.Void .ctor(UnityEngine.Object custom, System.Single weight) { }

    }

    // TypeToken: 0x2000066  // size: 0x14
    public sealed struct Style
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineBlendDefinition.Style Cut;  // const
        public static Cinemachine.CinemachineBlendDefinition.Style EaseInOut;  // const
        public static Cinemachine.CinemachineBlendDefinition.Style EaseIn;  // const
        public static Cinemachine.CinemachineBlendDefinition.Style EaseOut;  // const
        public static Cinemachine.CinemachineBlendDefinition.Style HardIn;  // const
        public static Cinemachine.CinemachineBlendDefinition.Style HardOut;  // const
        public static Cinemachine.CinemachineBlendDefinition.Style Linear;  // const
        public static Cinemachine.CinemachineBlendDefinition.Style Custom;  // const

    }

    // TypeToken: 0x200006A  // size: 0x38
    public sealed struct CustomBlend
    {
        // Fields
        public System.String m_From;  // 0x10
        public System.String m_To;  // 0x18
        public Cinemachine.CinemachineBlendDefinition m_Blend;  // 0x20

    }

    // TypeToken: 0x200006B  // size: 0x78
    public class CustomBlendGroup
    {
        // Fields
        public System.Collections.Generic.List<System.String> cameras;  // 0x10
        public Cinemachine.CinemachineBlendDefinition fromSameCamera;  // 0x18
        public Cinemachine.CinemachineBlendDefinition insideGroup;  // 0x30
        public Cinemachine.CinemachineBlendDefinition toGroup;  // 0x48
        public Cinemachine.CinemachineBlendDefinition fromGroup;  // 0x60

        // Methods
        // RVA: 0x041E1670  token: 0x6000278
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200006E  // size: 0x14
    public sealed struct Stage
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineCore.Stage Body;  // const
        public static Cinemachine.CinemachineCore.Stage Aim;  // const
        public static Cinemachine.CinemachineCore.Stage Noise;  // const
        public static Cinemachine.CinemachineCore.Stage Finalize;  // const

    }

    // TypeToken: 0x200006F  // size: 0x80
    public sealed class AxisInputDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x03FCFEE0  token: 0x60002AB
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014E2060  token: 0x60002AC
        public virtual System.Single Invoke(System.String axisName) { }

    }

    // TypeToken: 0x2000070  // size: 0x80
    public sealed class GetBlendOverrideDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0394E680  token: 0x60002AD
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x067B178C  token: 0x60002AE
        public virtual Cinemachine.CinemachineBlendDefinition Invoke(Cinemachine.ICinemachineCamera fromVcam, Cinemachine.ICinemachineCamera toVcam, Cinemachine.CinemachineBlendDefinition defaultBlend, UnityEngine.MonoBehaviour owner) { }

    }

    // TypeToken: 0x2000071  // size: 0x20
    public class UpdateStatus
    {
        // Fields
        public System.Int32 lastUpdateFrame;  // 0x10
        public System.Int32 lastUpdateFixedFrame;  // 0x14
        public Cinemachine.UpdateTracker.UpdateClock lastUpdateMode;  // 0x18
        public System.Single lastUpdateDeltaTime;  // 0x1c

        // Methods
        // RVA: 0x041E1670  token: 0x60002AF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000072  // size: 0x14
    public sealed struct UpdateFilter
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineCore.UpdateFilter Fixed;  // const
        public static Cinemachine.CinemachineCore.UpdateFilter Late;  // const
        public static Cinemachine.CinemachineCore.UpdateFilter Smart;  // const
        public static Cinemachine.CinemachineCore.UpdateFilter SmartFixed;  // const
        public static Cinemachine.CinemachineCore.UpdateFilter SmartLate;  // const

    }

    // TypeToken: 0x2000073  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Cinemachine.CinemachineCore.<>c <>9;  // static @ 0x0
        public static System.Comparison<Cinemachine.CinemachineVirtualCameraBase> <>9__30_0;  // static @ 0x8

        // Methods
        // RVA: 0x067B272C  token: 0x60002B0
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60002B1
        public System.Void .ctor() { }
        // RVA: 0x067B263C  token: 0x60002B2
        private System.Int32 <GetVirtualCamera>b__30_0(Cinemachine.CinemachineVirtualCameraBase x, Cinemachine.CinemachineVirtualCameraBase y) { }

    }

    // TypeToken: 0x2000076  // size: 0x38
    public class Appearance
    {
        // Fields
        public UnityEngine.Color pathColor;  // 0x10
        public UnityEngine.Color inactivePathColor;  // 0x20
        public System.Single width;  // 0x30

        // Methods
        // RVA: 0x067AA0EC  token: 0x60002DB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000077  // size: 0x14
    public sealed struct PositionUnits
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachinePathBase.PositionUnits PathUnits;  // const
        public static Cinemachine.CinemachinePathBase.PositionUnits Distance;  // const
        public static Cinemachine.CinemachinePathBase.PositionUnits Normalized;  // const

    }

    // TypeToken: 0x2000082  // size: 0x14
    public sealed struct Level
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.DocumentationSortingAttribute.Level Undoc;  // const
        public static Cinemachine.DocumentationSortingAttribute.Level API;  // const
        public static Cinemachine.DocumentationSortingAttribute.Level UserRef;  // const

    }

    // TypeToken: 0x2000084  // size: 0x14
    public sealed struct StandbyUpdateMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineVirtualCameraBase.StandbyUpdateMode Never;  // const
        public static Cinemachine.CinemachineVirtualCameraBase.StandbyUpdateMode Always;  // const
        public static Cinemachine.CinemachineVirtualCameraBase.StandbyUpdateMode RoundRobin;  // const

    }

    // TypeToken: 0x2000085  // size: 0x14
    public sealed struct BlendHint
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineVirtualCameraBase.BlendHint None;  // const
        public static Cinemachine.CinemachineVirtualCameraBase.BlendHint SphericalPosition;  // const
        public static Cinemachine.CinemachineVirtualCameraBase.BlendHint CylindricalPosition;  // const
        public static Cinemachine.CinemachineVirtualCameraBase.BlendHint ScreenSpaceAimWhenTargetsDiffer;  // const

    }

    // TypeToken: 0x2000086  // size: 0x20
    public sealed struct TransitionParams
    {
        // Fields
        public Cinemachine.CinemachineVirtualCameraBase.BlendHint m_BlendHint;  // 0x10
        public System.Boolean m_InheritPosition;  // 0x14
        public Cinemachine.CinemachineBrain.VcamActivatedEvent m_OnCameraLive;  // 0x18

    }

    // TypeToken: 0x2000087  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Cinemachine.CinemachineVirtualCameraBase.<>c <>9;  // static @ 0x0
        public static System.Func<Cinemachine.CinemachineExtension,System.Boolean> <>9__67_0;  // static @ 0x8

        // Methods
        // RVA: 0x067B26C8  token: 0x6000329
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600032A
        public System.Void .ctor() { }
        // RVA: 0x067B2670  token: 0x600032B
        private System.Boolean <RequiresUserInput>b__67_0(Cinemachine.CinemachineExtension extension) { }

    }

    // TypeToken: 0x2000089  // size: 0x40
    public class BakedSolution
    {
        // Fields
        private readonly System.Single <FrustumHeight>k__BackingField;  // 0x10
        private System.Single m_frustumSizeIntSpace;  // 0x14
        private readonly Cinemachine.ConfinerOven.AspectStretcher m_AspectStretcher;  // 0x18
        private readonly System.Boolean m_HasBones;  // 0x24
        private readonly System.Double m_SqrPolygonDiagonal;  // 0x28
        private System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>> m_OriginalPolygon;  // 0x30
        public System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>> m_Solution;  // 0x38

        // Properties
        System.Single FrustumHeight { get; /* RVA: 0x04D85F70 */ }

        // Methods
        // RVA: 0x067AB2F0  token: 0x6000335
        public System.Void .ctor(System.Single aspectRatio, System.Single frustumHeight, System.Boolean hasBones, UnityEngine.Rect polygonBounds, System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>> originalPolygon, System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>> solution) { }
        // RVA: 0x067AB2C8  token: 0x6000336
        public System.Boolean IsValid(System.Single frustumHeight) { }
        // RVA: 0x067AA9A0  token: 0x6000337
        public UnityEngine.Vector2 ConfinePoint(UnityEngine.Vector2& pointToConfine) { }
        // RVA: 0x067AB22C  token: 0x6000338
        private System.Boolean IsInsideOriginal(ClipperLib.IntPoint p) { }
        // RVA: 0x067AA920  token: 0x6000339
        private static System.Single ClosestPointOnSegment(ClipperLib.IntPoint p, ClipperLib.IntPoint s0, ClipperLib.IntPoint s1) { }
        // RVA: 0x067AB198  token: 0x600033A
        private static ClipperLib.IntPoint IntPointLerp(ClipperLib.IntPoint a, ClipperLib.IntPoint b, System.Single lerp) { }
        // RVA: 0x067AAD74  token: 0x600033B
        private System.Boolean DoesIntersectOriginal(ClipperLib.IntPoint l1, ClipperLib.IntPoint l2) { }
        // RVA: 0x067AAF74  token: 0x600033C
        private static System.Int32 FindIntersection(ClipperLib.IntPoint& p1, ClipperLib.IntPoint& p2, ClipperLib.IntPoint& p3, ClipperLib.IntPoint& p4) { }
        // RVA: 0x04D87F40  token: 0x600033D
        private static System.Double IntPointDiffSqrMagnitude(ClipperLib.IntPoint p1, ClipperLib.IntPoint p2) { }

    }

    // TypeToken: 0x200008A  // size: 0x1C
    public sealed struct AspectStretcher
    {
        // Fields
        private readonly System.Single <Aspect>k__BackingField;  // 0x10
        private readonly System.Single m_InverseAspect;  // 0x14
        private readonly System.Single m_CenterX;  // 0x18

        // Properties
        System.Single Aspect { get; /* RVA: 0x04D871D0 */ }

        // Methods
        // RVA: 0x04D87ED0  token: 0x600033F
        public System.Void .ctor(System.Single aspect, System.Single centerX) { }
        // RVA: 0x04D87E70  token: 0x6000340
        public UnityEngine.Vector2 Stretch(UnityEngine.Vector2 p) { }
        // RVA: 0x04D87EA0  token: 0x6000341
        public UnityEngine.Vector2 Unstretch(UnityEngine.Vector2 p) { }

    }

    // TypeToken: 0x200008B  // size: 0x20
    public sealed struct PolygonSolution
    {
        // Fields
        public System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>> m_Polygons;  // 0x10
        public System.Single m_FrustumHeight;  // 0x18

        // Properties
        System.Boolean IsEmpty { get; /* RVA: 0x0314CD60 */ }

        // Methods
        // RVA: 0x067B1998  token: 0x6000342
        public System.Boolean StateChanged(System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>>& paths) { }

    }

    // TypeToken: 0x200008C  // size: 0x14
    public sealed struct BakingState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.ConfinerOven.BakingState BAKING;  // const
        public static Cinemachine.ConfinerOven.BakingState BAKED;  // const
        public static Cinemachine.ConfinerOven.BakingState TIMEOUT;  // const

    }

    // TypeToken: 0x200008D  // size: 0x58
    public sealed struct BakingStateCache
    {
        // Fields
        public ClipperLib.ClipperOffset offsetter;  // 0x10
        public System.Collections.Generic.List<Cinemachine.ConfinerOven.PolygonSolution> solutions;  // 0x18
        public Cinemachine.ConfinerOven.PolygonSolution rightCandidate;  // 0x20
        public Cinemachine.ConfinerOven.PolygonSolution leftCandidate;  // 0x30
        public System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>> maxCandidate;  // 0x40
        public System.Single stepSize;  // 0x48
        public System.Single maxFrustumHeight;  // 0x4c
        public System.Single currentFrustumHeight;  // 0x50
        public System.Single bakeTime;  // 0x54

    }

    // TypeToken: 0x2000090  // size: 0x14
    public sealed struct OverrideModes
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.LensSettings.OverrideModes None;  // const
        public static Cinemachine.LensSettings.OverrideModes Orthographic;  // const
        public static Cinemachine.LensSettings.OverrideModes Perspective;  // const
        public static Cinemachine.LensSettings.OverrideModes Physical;  // const

    }

    // TypeToken: 0x2000092  // size: 0x1C
    public sealed struct NoiseParams
    {
        // Fields
        public System.Single Frequency;  // 0x10
        public System.Single Amplitude;  // 0x14
        public System.Boolean Constant;  // 0x18

        // Methods
        // RVA: 0x03C01300  token: 0x600035D
        public System.Single GetValueAt(System.Single time, System.Single timeOffset) { }

    }

    // TypeToken: 0x2000093  // size: 0x34
    public sealed struct TransformNoiseParams
    {
        // Fields
        public Cinemachine.NoiseSettings.NoiseParams X;  // 0x10
        public Cinemachine.NoiseSettings.NoiseParams Y;  // 0x1c
        public Cinemachine.NoiseSettings.NoiseParams Z;  // 0x28

        // Methods
        // RVA: 0x03C01260  token: 0x600035E
        public UnityEngine.Vector3 GetValueAt(System.Single time, UnityEngine.Vector3 timeOffsets) { }

    }

    // TypeToken: 0x2000095  // size: 0x80
    public sealed class OverlapSphereNonAllocDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0454E620  token: 0x600036A
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x062248AC  token: 0x600036B
        public virtual System.Int32 Invoke(UnityEngine.Vector3 position, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }

    }

    // TypeToken: 0x2000096  // size: 0x80
    public sealed class RaycastDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0454E590  token: 0x600036C
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x06221374  token: 0x600036D
        public virtual System.Boolean Invoke(UnityEngine.Ray ray, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }

    }

    // TypeToken: 0x2000097  // size: 0x80
    public sealed class RaycastNonAllocDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0454E500  token: 0x600036E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x06221374  token: 0x600036F
        public virtual System.Int32 Invoke(UnityEngine.Ray ray, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }

    }

    // TypeToken: 0x2000098  // size: 0x80
    public sealed class SphereCastNonAllocDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0454E470  token: 0x6000370
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x06225970  token: 0x6000371
        public virtual System.Int32 Invoke(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }

    }

    // TypeToken: 0x2000099  // size: 0x80
    public sealed class ComputePenetrationDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0454E3E0  token: 0x6000372
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x06220F5C  token: 0x6000373
        public virtual System.Boolean Invoke(UnityEngine.ECSColliderResultProxy colliderA, UnityEngine.Vector3 positionA, UnityEngine.Quaternion rotationA, UnityEngine.ECSColliderResultProxy colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance) { }

    }

    // TypeToken: 0x200009D  // size: 0x14
    public sealed struct Mode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.TargetPositionCache.Mode Disabled;  // const
        public static Cinemachine.TargetPositionCache.Mode Record;  // const
        public static Cinemachine.TargetPositionCache.Mode Playback;  // const

    }

    // TypeToken: 0x200009F  // size: 0x2C
    public sealed struct Item
    {
        // Fields
        public UnityEngine.Vector3 Pos;  // 0x10
        public UnityEngine.Quaternion Rot;  // 0x1c

        // Properties
        Cinemachine.TargetPositionCache.CacheCurve.Item Empty { get; /* RVA: 0x067B1904 */ }

        // Methods
        // RVA: 0x067B17FC  token: 0x600037E
        public static Cinemachine.TargetPositionCache.CacheCurve.Item Lerp(Cinemachine.TargetPositionCache.CacheCurve.Item a, Cinemachine.TargetPositionCache.CacheCurve.Item b, System.Single t) { }

    }

    // TypeToken: 0x200009E  // size: 0x20
    public class CacheCurve
    {
        // Fields
        public System.Single StartTime;  // 0x10
        public System.Single StepSize;  // 0x14
        private System.Collections.Generic.List<Cinemachine.TargetPositionCache.CacheCurve.Item> m_Cache;  // 0x18

        // Methods
        // RVA: 0x067AB550  token: 0x600037D
        public Cinemachine.TargetPositionCache.CacheCurve.Item Evaluate(System.Single time) { }

    }

    // TypeToken: 0x20000A1  // size: 0x34
    public sealed struct RecordingItem
    {
        // Fields
        public System.Single Time;  // 0x10
        public System.Boolean IsCut;  // 0x14
        public Cinemachine.TargetPositionCache.CacheCurve.Item Item;  // 0x18

    }

    // TypeToken: 0x20000A0  // size: 0x20
    public class CacheEntry
    {
        // Fields
        public Cinemachine.TargetPositionCache.CacheCurve Curve;  // 0x10
        private System.Collections.Generic.List<Cinemachine.TargetPositionCache.CacheEntry.RecordingItem> RawItems;  // 0x18

        // Methods
        // RVA: 0x067AB6F0  token: 0x6000380
        public System.Void AddRawItem(System.Single time, System.Boolean isCut, UnityEngine.Transform target) { }
        // RVA: 0x067AB988  token: 0x6000381
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A2  // size: 0x18
    public sealed struct TimeRange
    {
        // Fields
        public System.Single Start;  // 0x10
        public System.Single End;  // 0x14

        // Properties
        System.Boolean IsEmpty { get; /* RVA: 0x067B2630 */ }
        Cinemachine.TargetPositionCache.TimeRange Empty { get; /* RVA: 0x04D880A0 */ }

        // Methods
        // RVA: 0x067B25FC  token: 0x6000383
        public System.Boolean Contains(System.Single time) { }
        // RVA: 0x067B2610  token: 0x6000385
        public System.Void Include(System.Single time) { }

    }

    // TypeToken: 0x20000A4  // size: 0x14
    public sealed struct UpdateClock
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.UpdateTracker.UpdateClock Fixed;  // const
        public static Cinemachine.UpdateTracker.UpdateClock Late;  // const

    }

    // TypeToken: 0x20000A5  // size: 0x68
    public class UpdateStatus
    {
        // Fields
        private System.Int32 windowStart;  // 0x10
        private System.Int32 numWindowLateUpdateMoves;  // 0x14
        private System.Int32 numWindowFixedUpdateMoves;  // 0x18
        private System.Int32 numWindows;  // 0x1c
        private System.Int32 lastFrameUpdated;  // 0x20
        private UnityEngine.Matrix4x4 lastPos;  // 0x24
        private Cinemachine.UpdateTracker.UpdateClock <PreferredUpdate>k__BackingField;  // 0x64

        // Properties
        Cinemachine.UpdateTracker.UpdateClock PreferredUpdate { get; /* RVA: 0x04D88110 */ set; /* RVA: 0x04D88120 */ }

        // Methods
        // RVA: 0x067B87C0  token: 0x600038D
        public System.Void .ctor(System.Int32 currentFrame, UnityEngine.Matrix4x4 pos) { }
        // RVA: 0x067B86B0  token: 0x600038E
        public System.Void OnUpdate(System.Int32 currentFrame, Cinemachine.UpdateTracker.UpdateClock currentClock, UnityEngine.Matrix4x4 pos) { }

    }

    // TypeToken: 0x20000A7  // size: 0x18
    public sealed class <>c__DisplayClass7_0
    {
        // Fields
        public UnityEngine.InputSystem.InputActionReference aRef;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000394
        public System.Void .ctor() { }
        // RVA: 0x067B8068  token: 0x6000395
        private System.Boolean <ResolveForPlayer>b__1(UnityEngine.InputSystem.InputAction x) { }

    }

    // TypeToken: 0x20000AA  // size: 0x14
    public sealed struct Mode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineTriggerAction.ActionSettings.Mode Custom;  // const
        public static Cinemachine.CinemachineTriggerAction.ActionSettings.Mode PriorityBoost;  // const
        public static Cinemachine.CinemachineTriggerAction.ActionSettings.Mode Activate;  // const
        public static Cinemachine.CinemachineTriggerAction.ActionSettings.Mode Deactivate;  // const
        public static Cinemachine.CinemachineTriggerAction.ActionSettings.Mode Enable;  // const
        public static Cinemachine.CinemachineTriggerAction.ActionSettings.Mode Disable;  // const
        public static Cinemachine.CinemachineTriggerAction.ActionSettings.Mode Play;  // const
        public static Cinemachine.CinemachineTriggerAction.ActionSettings.Mode Stop;  // const

    }

    // TypeToken: 0x20000AB  // size: 0x30
    public class TriggerEvent : UnityEngine.Events.UnityEvent
    {
        // Methods
        // RVA: 0x05393538  token: 0x60003A5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000AC  // size: 0x14
    public sealed struct TimeMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineTriggerAction.ActionSettings.TimeMode FromStart;  // const
        public static Cinemachine.CinemachineTriggerAction.ActionSettings.TimeMode FromEnd;  // const
        public static Cinemachine.CinemachineTriggerAction.ActionSettings.TimeMode BeforeNow;  // const
        public static Cinemachine.CinemachineTriggerAction.ActionSettings.TimeMode AfterNow;  // const

    }

    // TypeToken: 0x20000A9  // size: 0x38
    public sealed struct ActionSettings
    {
        // Fields
        public Cinemachine.CinemachineTriggerAction.ActionSettings.Mode m_Action;  // 0x10
        public UnityEngine.Object m_Target;  // 0x18
        public System.Int32 m_BoostAmount;  // 0x20
        public System.Single m_StartTime;  // 0x24
        public Cinemachine.CinemachineTriggerAction.ActionSettings.TimeMode m_Mode;  // 0x28
        public Cinemachine.CinemachineTriggerAction.ActionSettings.TriggerEvent m_Event;  // 0x30

        // Methods
        // RVA: 0x067B3140  token: 0x60003A3
        public System.Void .ctor(Cinemachine.CinemachineTriggerAction.ActionSettings.Mode action) { }
        // RVA: 0x067B2CDC  token: 0x60003A4
        public System.Void Invoke() { }

    }

    // TypeToken: 0x20000B1  // size: 0x14
    public sealed struct ImpulseShapes
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineImpulseDefinition.ImpulseShapes Custom;  // const
        public static Cinemachine.CinemachineImpulseDefinition.ImpulseShapes Recoil;  // const
        public static Cinemachine.CinemachineImpulseDefinition.ImpulseShapes Bump;  // const
        public static Cinemachine.CinemachineImpulseDefinition.ImpulseShapes Explosion;  // const
        public static Cinemachine.CinemachineImpulseDefinition.ImpulseShapes Rumble;  // const

    }

    // TypeToken: 0x20000B2  // size: 0x14
    public sealed struct ImpulseTypes
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineImpulseDefinition.ImpulseTypes Uniform;  // const
        public static Cinemachine.CinemachineImpulseDefinition.ImpulseTypes Dissipating;  // const
        public static Cinemachine.CinemachineImpulseDefinition.ImpulseTypes Propagating;  // const
        public static Cinemachine.CinemachineImpulseDefinition.ImpulseTypes Legacy;  // const

    }

    // TypeToken: 0x20000B3  // size: 0x14
    public sealed struct RepeatMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineImpulseDefinition.RepeatMode Stretch;  // const
        public static Cinemachine.CinemachineImpulseDefinition.RepeatMode Loop;  // const

    }

    // TypeToken: 0x20000B4  // size: 0x28
    public class SignalSource : Cinemachine.ISignalSource6D
    {
        // Fields
        private Cinemachine.CinemachineImpulseDefinition m_Def;  // 0x10
        private UnityEngine.Vector3 m_Velocity;  // 0x18

        // Properties
        System.Single SignalDuration { get; /* RVA: 0x067B70D8 */ }

        // Methods
        // RVA: 0x067B70A8  token: 0x60003C3
        public System.Void .ctor(Cinemachine.CinemachineImpulseDefinition def, UnityEngine.Vector3 velocity) { }
        // RVA: 0x067B6FD0  token: 0x60003C5
        public virtual System.Void GetSignal(System.Single timeSinceSignalStart, UnityEngine.Vector3& pos, UnityEngine.Quaternion& rot) { }

    }

    // TypeToken: 0x20000B5  // size: 0x28
    public class LegacySignalSource : Cinemachine.ISignalSource6D
    {
        // Fields
        private Cinemachine.CinemachineImpulseDefinition m_Def;  // 0x10
        private UnityEngine.Vector3 m_Velocity;  // 0x18
        private System.Single m_StartTimeOffset;  // 0x24

        // Properties
        System.Single SignalDuration { get; /* RVA: 0x033CD640 */ }

        // Methods
        // RVA: 0x033CEB50  token: 0x60003C6
        public System.Void .ctor(Cinemachine.CinemachineImpulseDefinition def, UnityEngine.Vector3 velocity) { }
        // RVA: 0x033CD430  token: 0x60003C8
        public virtual System.Void GetSignal(System.Single timeSinceSignalStart, UnityEngine.Vector3& pos, UnityEngine.Quaternion& rot) { }

    }

    // TypeToken: 0x20000B7  // size: 0x40
    public sealed struct ImpulseReaction
    {
        // Fields
        public Cinemachine.NoiseSettings m_SecondaryNoise;  // 0x10
        public System.Single m_AmplitudeGain;  // 0x18
        public System.Single m_FrequencyGain;  // 0x1c
        public System.Single m_Duration;  // 0x20
        private System.Single m_CurrentAmount;  // 0x24
        private System.Single m_CurrentTime;  // 0x28
        private System.Single m_CurrentDamping;  // 0x2c
        private System.Boolean m_Initialized;  // 0x30
        private UnityEngine.Vector3 m_NoiseOffsets;  // 0x34

        // Methods
        // RVA: 0x067B6CE0  token: 0x60003CC
        public System.Void ReSeed() { }
        // RVA: 0x03E32D20  token: 0x60003CD
        public System.Boolean GetReaction(System.Single deltaTime, UnityEngine.Vector3 impulsePos, UnityEngine.Vector3& pos, UnityEngine.Quaternion& rot) { }

    }

    // TypeToken: 0x20000BA  // size: 0x30
    public sealed struct EnvelopeDefinition
    {
        // Fields
        public UnityEngine.AnimationCurve m_AttackShape;  // 0x10
        public UnityEngine.AnimationCurve m_DecayShape;  // 0x18
        public System.Single m_AttackTime;  // 0x20
        public System.Single m_SustainTime;  // 0x24
        public System.Single m_DecayTime;  // 0x28
        public System.Boolean m_ScaleWithImpact;  // 0x2c
        public System.Boolean m_HoldForever;  // 0x2d

        // Properties
        System.Single Duration { get; /* RVA: 0x033CD220 */ }

        // Methods
        // RVA: 0x04D880C0  token: 0x60003D8
        public static Cinemachine.CinemachineImpulseManager.EnvelopeDefinition Default() { }
        // RVA: 0x033CD2B0  token: 0x60003DA
        public System.Single GetValueAt(System.Single offset) { }
        // RVA: 0x067B6378  token: 0x60003DB
        public System.Void ChangeStopTime(System.Single offset, System.Boolean forceNoDecay) { }
        // RVA: 0x033CEB20  token: 0x60003DC
        public System.Void Clear() { }
        // RVA: 0x067B63A4  token: 0x60003DD
        public System.Void Validate() { }

    }

    // TypeToken: 0x20000BC  // size: 0x14
    public sealed struct DirectionMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineImpulseManager.ImpulseEvent.DirectionMode Fixed;  // const
        public static Cinemachine.CinemachineImpulseManager.ImpulseEvent.DirectionMode RotateTowardSource;  // const

    }

    // TypeToken: 0x20000BD  // size: 0x14
    public sealed struct DissipationMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Cinemachine.CinemachineImpulseManager.ImpulseEvent.DissipationMode LinearDecay;  // const
        public static Cinemachine.CinemachineImpulseManager.ImpulseEvent.DissipationMode SoftDecay;  // const
        public static Cinemachine.CinemachineImpulseManager.ImpulseEvent.DissipationMode ExponentialDecay;  // const

    }

    // TypeToken: 0x20000BB  // size: 0x70
    public class ImpulseEvent
    {
        // Fields
        public System.Single m_StartTime;  // 0x10
        public Cinemachine.CinemachineImpulseManager.EnvelopeDefinition m_Envelope;  // 0x18
        public Cinemachine.ISignalSource6D m_SignalSource;  // 0x38
        public UnityEngine.Vector3 m_Position;  // 0x40
        public System.Single m_Radius;  // 0x4c
        public Cinemachine.CinemachineImpulseManager.ImpulseEvent.DirectionMode m_DirectionMode;  // 0x50
        public System.Int32 m_Channel;  // 0x54
        public Cinemachine.CinemachineImpulseManager.ImpulseEvent.DissipationMode m_DissipationMode;  // 0x58
        public System.Single m_DissipationDistance;  // 0x5c
        public System.Single m_CustomDissipation;  // 0x60
        public System.Single m_PropagationSpeed;  // 0x64
        public System.Boolean m_IgnoreDistance;  // 0x68

        // Properties
        System.Boolean Expired { get; /* RVA: 0x033CD100 */ }

        // Methods
        // RVA: 0x067B6CC8  token: 0x60003DF
        public System.Void Cancel(System.Single time, System.Boolean forceNoDecay) { }
        // RVA: 0x033CD280  token: 0x60003E0
        public System.Single DistanceDecay(System.Single distance) { }
        // RVA: 0x033CCC40  token: 0x60003E1
        public System.Boolean GetDecayedSignal(UnityEngine.Vector3 listenerPosition, System.Boolean use2D, UnityEngine.Vector3& pos, UnityEngine.Quaternion& rot) { }
        // RVA: 0x033CEAC0  token: 0x60003E2
        public System.Void Clear() { }
        // RVA: 0x041E1670  token: 0x60003E3
        private System.Void .ctor() { }

    }

    // TypeToken: 0x20000C3  // size: 0x80
    public sealed class OnGUIDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x02F28650  token: 0x60003FA
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x053DEDB8  token: 0x60003FB
        public virtual System.Void Invoke() { }

    }

    // TypeToken: 0x20000C7  // size: 0x24
    public sealed struct Item
    {
        // Fields
        public UnityEngine.Vector3 velocity;  // 0x10
        public System.Single weight;  // 0x1c
        public System.Single time;  // 0x20

    }

    // TypeToken: 0x20000CC  // size: 0x1C
    public sealed struct __StaticArrayInitTypeSize=12
    {
    }

    // TypeToken: 0x20000CB  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=12 AA5072B96927F710758B3E5237A84AC68E397BC4E1BF7E864A950696D743CC32;  // static @ 0x0

    }

namespace Cinemachine
{

    // TypeToken: 0x200000A  // size: 0x58
    public class Cinemachine3rdPersonAim : Cinemachine.CinemachineExtension
    {
        // Fields
        public UnityEngine.LayerMask AimCollisionFilter;  // 0x28
        public System.String IgnoreTag;  // 0x30
        public System.Single AimDistance;  // 0x38
        public UnityEngine.RectTransform AimTargetReticle;  // 0x40
        private UnityEngine.Vector3 <AimTarget>k__BackingField;  // 0x48

        // Properties
        UnityEngine.Vector3 AimTarget { get; /* RVA: 0x04D87680 */ set; /* RVA: 0x04D876A0 */ }

        // Methods
        // RVA: 0x0678BEE8  token: 0x600001C
        private System.Void OnValidate() { }
        // RVA: 0x0678C108  token: 0x600001D
        private System.Void Reset() { }
        // RVA: 0x0678BDBC  token: 0x600001E
        public virtual System.Boolean OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x0678BC00  token: 0x600001F
        private System.Void DrawReticle(Cinemachine.CinemachineBrain brain) { }
        // RVA: 0x0678B8FC  token: 0x6000020
        private UnityEngine.Vector3 ComputeLookAtPoint(UnityEngine.Vector3 camPos, UnityEngine.Transform player) { }
        // RVA: 0x0678B770  token: 0x6000021
        private UnityEngine.Vector3 ComputeAimTarget(UnityEngine.Vector3 cameraLookAt, UnityEngine.Transform player) { }
        // RVA: 0x0678BF00  token: 0x6000022
        protected virtual System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime) { }
        // RVA: 0x0678C16C  token: 0x6000023
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000B  // size: 0x210
    public class CinemachineBlendListCamera : Cinemachine.CinemachineVirtualCameraBase
    {
        // Fields
        public UnityEngine.Transform m_LookAt;  // 0xa8
        public UnityEngine.Transform m_Follow;  // 0xb0
        public System.Boolean m_ShowDebugText;  // 0xb8
        public System.Boolean m_Loop;  // 0xb9
        private Cinemachine.CinemachineVirtualCameraBase[] m_ChildCameras;  // 0xc0
        public Cinemachine.CinemachineBlendListCamera.Instruction[] m_Instructions;  // 0xc8
        private Cinemachine.ICinemachineCamera <LiveChild>k__BackingField;  // 0xd0
        private Cinemachine.ICinemachineCamera m_TransitioningFrom;  // 0xd8
        private Cinemachine.CameraState m_State;  // 0xe0
        private System.Single mActivationTime;  // 0x200
        private System.Int32 mCurrentInstruction;  // 0x204
        private Cinemachine.CinemachineBlend mActiveBlend;  // 0x208

        // Properties
        System.String Description { get; /* RVA: 0x0678D6A4 */ }
        Cinemachine.ICinemachineCamera LiveChild { get; /* RVA: 0x04D876B0 */ set; /* RVA: 0x0678D7BC */ }
        Cinemachine.CameraState State { get; /* RVA: 0x04D876C0 */ }
        UnityEngine.Transform LookAt { get; /* RVA: 0x0678D7AC */ set; /* RVA: 0x04D84CC0 */ }
        UnityEngine.Transform Follow { get; /* RVA: 0x0678D790 */ set; /* RVA: 0x04D84CE0 */ }
        Cinemachine.CinemachineVirtualCameraBase[] ChildCameras { get; /* RVA: 0x0678D684 */ }
        System.Boolean IsBlending { get; /* RVA: 0x0678D7A0 */ }

        // Methods
        // RVA: 0x0678D1C4  token: 0x6000025
        private System.Void Reset() { }
        // RVA: 0x0678CB48  token: 0x6000028
        public virtual System.Boolean IsLiveChild(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly) { }
        // RVA: 0x0678CFD0  token: 0x600002E
        public virtual System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta) { }
        // RVA: 0x0678C44C  token: 0x600002F
        public virtual System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot) { }
        // RVA: 0x0678D09C  token: 0x6000030
        public virtual System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x0678C52C  token: 0x6000031
        public virtual System.Void InternalUpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x0678CC88  token: 0x6000032
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0678CB70  token: 0x6000033
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0678D094  token: 0x6000034
        private System.Void OnTransformChildrenChanged() { }
        // RVA: 0x0678CE8C  token: 0x6000035
        private System.Void OnGuiHandler() { }
        // RVA: 0x0678CB14  token: 0x6000038
        private System.Void InvalidateListOfChildren() { }
        // RVA: 0x0678D228  token: 0x6000039
        private System.Void UpdateListOfChildren() { }
        // RVA: 0x0678D3C8  token: 0x600003A
        private System.Void ValidateInstructions() { }
        // RVA: 0x0678C248  token: 0x600003B
        private System.Void AdvanceCurrentInstruction(System.Single deltaTime) { }
        // RVA: 0x0678D54C  token: 0x600003C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000D  // size: 0x1D0
    public class CinemachineBrain : UnityEngine.MonoBehaviour
    {
        // Fields
        public System.Boolean m_ShowDebugText;  // 0x18
        public System.Boolean m_ShowCameraFrustum;  // 0x19
        public System.Boolean m_IgnoreTimeScale;  // 0x1a
        public UnityEngine.Transform m_WorldUpOverride;  // 0x20
        public Cinemachine.CinemachineBrain.UpdateMethod m_UpdateMethod;  // 0x28
        public Cinemachine.CinemachineBrain.BrainUpdateMethod m_BlendUpdateMethod;  // 0x2c
        public Cinemachine.CinemachineBlendDefinition m_DefaultBlend;  // 0x30
        public Cinemachine.CinemachineBlenderSettings m_CustomBlends;  // 0x48
        private UnityEngine.Camera m_OutputCamera;  // 0x50
        private HG.Rendering.Runtime.HGDepthOfField m_OutputDOF;  // 0x58
        public Cinemachine.CinemachineBrain.BrainEvent m_CameraCutEvent;  // 0x60
        public Cinemachine.CinemachineBrain.VcamActivatedEvent m_CameraActivatedEvent;  // 0x68
        private static Cinemachine.ICinemachineCamera mSoloCamera;  // static @ 0x0
        private UnityEngine.Coroutine mPhysicsCoroutine;  // 0x70
        private System.Int32 m_LastFrameUpdated;  // 0x78
        private UnityEngine.WaitForFixedUpdate mWaitForFixedUpdate;  // 0x80
        private System.Collections.Generic.List<Cinemachine.CinemachineBrain.BrainFrame> mFrameStack;  // 0x88
        private System.Int32 mNextFrameId;  // 0x90
        private Cinemachine.CinemachineBlend mCurrentLiveCameras;  // 0x98
        private static readonly UnityEngine.AnimationCurve mDefaultLinearAnimationCurve;  // static @ 0x8
        private Cinemachine.ICinemachineCamera mActiveCameraPreviousFrame;  // 0xa0
        private UnityEngine.GameObject mActiveCameraPreviousFrameGameObject;  // 0xa8
        private Cinemachine.CameraState <CurrentCameraState>k__BackingField;  // 0xb0

        // Properties
        UnityEngine.Camera OutputCamera { get; /* RVA: 0x033C8A00 */ }
        HG.Rendering.Runtime.HGDepthOfField OutputDOF { get; /* RVA: 0x033C7310 */ }
        Cinemachine.ICinemachineCamera SoloCamera { get; /* RVA: 0x0678E20C */ set; /* RVA: 0x0678E2B8 */ }
        UnityEngine.Vector3 DefaultWorldUp { get; /* RVA: 0x033C8F90 */ }
        Cinemachine.ICinemachineCamera ActiveVirtualCamera { get; /* RVA: 0x033C9960 */ }
        System.Boolean IsBlending { get; /* RVA: 0x033C7B70 */ }
        Cinemachine.CinemachineBlend ActiveBlend { get; /* RVA: 0x033C9850 */ }
        Cinemachine.CameraState CurrentCameraState { get; /* RVA: 0x04D87740 */ set; /* RVA: 0x0678E240 */ }

        // Methods
        // RVA: 0x0678D8DC  token: 0x6000041
        public static UnityEngine.Color GetSoloGUIColor() { }
        // RVA: 0x03FCE9A0  token: 0x6000043
        private System.Void OnEnable() { }
        // RVA: 0x0678DA00  token: 0x6000044
        private System.Void OnDisable() { }
        // RVA: 0x0678DEF0  token: 0x6000045
        private System.Void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode) { }
        // RVA: 0x04D7AB90  token: 0x6000046
        private System.Void OnSceneUnloaded(UnityEngine.SceneManagement.Scene scene) { }
        // RVA: 0x04D83C20  token: 0x6000047
        private System.Void Start() { }
        // RVA: 0x0678DC30  token: 0x6000048
        private System.Void OnGuiHandler() { }
        // RVA: 0x03FCED60  token: 0x6000049
        private System.Collections.IEnumerator AfterPhysics() { }
        // RVA: 0x041AEA60  token: 0x600004A
        private System.Void LateUpdate() { }
        // RVA: 0x033CA8C0  token: 0x600004B
        public System.Void ManualUpdate(System.Single deltaTime) { }
        // RVA: 0x033CAA10  token: 0x600004C
        private System.Single GetEffectiveDeltaTime(System.Boolean fixedDelta) { }
        // RVA: 0x033C8B10  token: 0x600004D
        private System.Void UpdateVirtualCameras(Cinemachine.CinemachineCore.UpdateFilter updateFilter, System.Single deltaTime) { }
        // RVA: 0x033C9A50  token: 0x600004F
        private static Cinemachine.ICinemachineCamera DeepCamBFromBlend(Cinemachine.CinemachineBlend blend) { }
        // RVA: 0x0678D938  token: 0x6000050
        public System.Boolean IsLiveInBlend(Cinemachine.ICinemachineCamera vcam) { }
        // RVA: 0x0678D7D0  token: 0x6000053
        private System.Int32 GetBrainFrame(System.Int32 withId) { }
        // RVA: 0x0678E00C  token: 0x6000054
        public System.Int32 SetCameraOverride(System.Int32 overrideId, Cinemachine.ICinemachineCamera camA, Cinemachine.ICinemachineCamera camB, System.Single weightB, System.Single deltaTime) { }
        // RVA: 0x0678DF54  token: 0x6000055
        public System.Void ReleaseCameraOverride(System.Int32 overrideId) { }
        // RVA: 0x033CA000  token: 0x6000056
        private System.Void ProcessActiveCamera(System.Single deltaTime) { }
        // RVA: 0x033C6D10  token: 0x6000057
        private System.Void UpdateFrame0(System.Single deltaTime) { }
        // RVA: 0x033C7010  token: 0x6000058
        public System.Void ComputeCurrentBlend(Cinemachine.CinemachineBlend& outputBlend, System.Int32 numTopLayersToExclude) { }
        // RVA: 0x033C98A0  token: 0x6000059
        public System.Boolean IsLive(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly) { }
        // RVA: 0x033C8670  token: 0x600005C
        public Cinemachine.ICinemachineCamera TopCameraFromPriorityQueue() { }
        // RVA: 0x040F7700  token: 0x600005D
        private Cinemachine.CinemachineBlendDefinition LookupBlend(Cinemachine.ICinemachineCamera fromKey, Cinemachine.ICinemachineCamera toKey) { }
        // RVA: 0x033C7390  token: 0x600005E
        private System.Void PushStateToUnityCamera(Cinemachine.CameraState& state) { }
        // RVA: 0x03FCF900  token: 0x600005F
        public System.Void .ctor() { }
        // RVA: 0x04D34F10  token: 0x6000060
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000014  // size: 0x250
    public class CinemachineClearShot : Cinemachine.CinemachineVirtualCameraBase
    {
        // Fields
        public UnityEngine.Transform m_LookAt;  // 0xa8
        public UnityEngine.Transform m_Follow;  // 0xb0
        public System.Boolean m_ShowDebugText;  // 0xb8
        private Cinemachine.CinemachineVirtualCameraBase[] m_ChildCameras;  // 0xc0
        public System.Single m_ActivateAfter;  // 0xc8
        public System.Single m_MinDuration;  // 0xcc
        public System.Boolean m_RandomizeChoice;  // 0xd0
        public Cinemachine.CinemachineBlendDefinition m_DefaultBlend;  // 0xd8
        public Cinemachine.CinemachineBlenderSettings m_CustomBlends;  // 0xf0
        private Cinemachine.ICinemachineCamera <LiveChild>k__BackingField;  // 0xf8
        private Cinemachine.CameraState m_State;  // 0x100
        private System.Single mActivationTime;  // 0x220
        private System.Single mPendingActivationTime;  // 0x224
        private Cinemachine.ICinemachineCamera mPendingCamera;  // 0x228
        private Cinemachine.CinemachineBlend mActiveBlend;  // 0x230
        private System.Boolean mRandomizeNow;  // 0x238
        private Cinemachine.CinemachineVirtualCameraBase[] m_RandomizedChilden;  // 0x240
        private Cinemachine.ICinemachineCamera m_TransitioningFrom;  // 0x248

        // Properties
        System.String Description { get; /* RVA: 0x067901A0 */ }
        Cinemachine.ICinemachineCamera LiveChild { get; /* RVA: 0x04D877C0 */ set; /* RVA: 0x06790298 */ }
        Cinemachine.CameraState State { get; /* RVA: 0x04D877D0 */ }
        UnityEngine.Transform LookAt { get; /* RVA: 0x0678D7AC */ set; /* RVA: 0x04D84CC0 */ }
        UnityEngine.Transform Follow { get; /* RVA: 0x0678D790 */ set; /* RVA: 0x04D84CE0 */ }
        System.Boolean IsBlending { get; /* RVA: 0x0679028C */ }
        Cinemachine.CinemachineVirtualCameraBase[] ChildCameras { get; /* RVA: 0x06790180 */ }

        // Methods
        // RVA: 0x0678F39C  token: 0x600006F
        public virtual System.Boolean IsLiveChild(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly) { }
        // RVA: 0x0678F9E8  token: 0x6000074
        public virtual System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta) { }
        // RVA: 0x0678ED80  token: 0x6000075
        public virtual System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot) { }
        // RVA: 0x0678EE60  token: 0x6000076
        public virtual System.Void InternalUpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x0678F6B4  token: 0x6000077
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0678F59C  token: 0x6000078
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0678FAAC  token: 0x6000079
        public System.Void OnTransformChildrenChanged() { }
        // RVA: 0x0678F8A4  token: 0x600007A
        private System.Void OnGuiHandler() { }
        // RVA: 0x0678F354  token: 0x600007D
        private System.Void InvalidateListOfChildren() { }
        // RVA: 0x0678FDFC  token: 0x600007E
        public System.Void ResetRandomization() { }
        // RVA: 0x0678FE28  token: 0x600007F
        private System.Void UpdateListOfChildren() { }
        // RVA: 0x0678E700  token: 0x6000080
        private Cinemachine.ICinemachineCamera ChooseCurrentCamera(UnityEngine.Vector3 worldUp) { }
        // RVA: 0x0678FBC4  token: 0x6000081
        private Cinemachine.CinemachineVirtualCameraBase[] Randomize(Cinemachine.CinemachineVirtualCameraBase[] src) { }
        // RVA: 0x0678F3C4  token: 0x6000082
        private Cinemachine.CinemachineBlendDefinition LookupBlend(Cinemachine.ICinemachineCamera fromKey, Cinemachine.ICinemachineCamera toKey) { }
        // RVA: 0x0678FACC  token: 0x6000083
        public virtual System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x06790008  token: 0x6000084
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000017  // size: 0x78
    public class CinemachineCollider : Cinemachine.CinemachineExtension
    {
        // Fields
        public UnityEngine.LayerMask m_CollideAgainst;  // 0x28
        public System.String m_IgnoreTag;  // 0x30
        public UnityEngine.LayerMask m_TransparentLayers;  // 0x38
        public System.Single m_MinimumDistanceFromTarget;  // 0x3c
        public System.Boolean m_AvoidObstacles;  // 0x40
        public System.Single m_DistanceLimit;  // 0x44
        public System.Single m_MinimumOcclusionTime;  // 0x48
        public System.Single m_CameraRadius;  // 0x4c
        public Cinemachine.CinemachineCollider.ResolutionStrategy m_Strategy;  // 0x50
        public System.Boolean m_UseFollowInsteadOfLookAt;  // 0x54
        public System.Int32 m_MaximumEffort;  // 0x58
        public System.Single m_SmoothingTime;  // 0x5c
        public System.Single m_Damping;  // 0x60
        public System.Single m_DampingWhenOccluded;  // 0x64
        public System.Single m_OptimalTargetDistance;  // 0x68
        private static System.Single k_PrecisionSlush;  // const
        private UnityEngine.RaycastHit[] m_CornerBuffer;  // 0x70
        private static System.Single k_AngleThreshold;  // const
        private static UnityEngine.ECSColliderResultProxy[] s_ColliderBuffer;  // static @ 0x0

        // Properties
        System.Collections.Generic.List<System.Collections.Generic.List<UnityEngine.Vector3>> DebugPaths { get; /* RVA: 0x06792350 */ }

        // Methods
        // RVA: 0x06790FB0  token: 0x6000088
        public System.Boolean IsTargetObscured(Cinemachine.ICinemachineCamera vcam) { }
        // RVA: 0x067902AC  token: 0x6000089
        public System.Boolean CameraWasDisplaced(Cinemachine.ICinemachineCamera vcam) { }
        // RVA: 0x067906D4  token: 0x600008A
        public System.Single GetCameraDisplacementDistance(Cinemachine.ICinemachineCamera vcam) { }
        // RVA: 0x06791050  token: 0x600008B
        private System.Void OnValidate() { }
        // RVA: 0x06791008  token: 0x600008C
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x06790730  token: 0x600008E
        public virtual System.Single GetMaxDampTime() { }
        // RVA: 0x03200260  token: 0x600008F
        protected virtual System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime) { }
        // RVA: 0x067917EC  token: 0x6000090
        public System.Void ResetExtraData(Cinemachine.ICinemachineCamera vcam) { }
        // RVA: 0x03200320  token: 0x6000091
        public virtual System.Void GuardedPostPipelineStageCallback(Cinemachine.ICinemachineCamera vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime) { }
        // RVA: 0x03249DD0  token: 0x6000092
        private UnityEngine.Vector3 PreserveLineOfSight(Cinemachine.CameraState& state, Cinemachine.CinemachineCollider.VcamExtraState& extra) { }
        // RVA: 0x0324A0D0  token: 0x6000093
        private UnityEngine.Vector3 PullCameraInFrontOfNearestObstacle(UnityEngine.Vector3 cameraPos, UnityEngine.Vector3 lookAtPos, System.Int32 layerMask, UnityEngine.RaycastHit& hitInfo) { }
        // RVA: 0x0679109C  token: 0x6000094
        private UnityEngine.Vector3 PushCameraBack(UnityEngine.Vector3 currentPos, UnityEngine.Vector3 pushDir, UnityEngine.RaycastHit obstacle, UnityEngine.Vector3 lookAtPos, UnityEngine.Plane startPlane, System.Single targetDistance, System.Int32 iterations, Cinemachine.CinemachineCollider.VcamExtraState& extra) { }
        // RVA: 0x067908AC  token: 0x6000095
        private System.Boolean GetWalkingDirection(UnityEngine.Vector3 pos, UnityEngine.Vector3 pushDir, UnityEngine.RaycastHit obstacle, UnityEngine.Vector3& outDir) { }
        // RVA: 0x06790748  token: 0x6000096
        private System.Single GetPushBackDistance(UnityEngine.Ray ray, UnityEngine.Plane startPlane, System.Single targetDistance, UnityEngine.Vector3 lookAtPos) { }
        // RVA: 0x067902C8  token: 0x6000097
        private static System.Single ClampRayToBounds(UnityEngine.Ray ray, System.Single distance, UnityEngine.Bounds bounds) { }
        // RVA: 0x0679188C  token: 0x6000098
        private UnityEngine.Vector3 RespectCameraRadius(UnityEngine.Vector3 cameraPos, UnityEngine.Vector3 lookAtPos) { }
        // RVA: 0x03248E30  token: 0x6000099
        private System.Boolean CheckForTargetObstructions(Cinemachine.CameraState& state) { }
        // RVA: 0x032492D0  token: 0x600009A
        private static System.Boolean IsTargetOffscreen(Cinemachine.CameraState& state) { }
        // RVA: 0x049C8EB0  token: 0x600009B
        public System.Void .ctor() { }
        // RVA: 0x04D38910  token: 0x600009C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001A  // size: 0x60
    public class CinemachineConfiner : Cinemachine.CinemachineExtension
    {
        // Fields
        public Cinemachine.CinemachineConfiner.Mode m_ConfineMode;  // 0x28
        public UnityEngine.Collider m_BoundingVolume;  // 0x30
        public UnityEngine.Collider2D m_BoundingShape2D;  // 0x38
        private UnityEngine.Collider2D m_BoundingShape2DCache;  // 0x40
        public System.Boolean m_ConfineScreenEdges;  // 0x48
        public System.Single m_Damping;  // 0x4c
        private System.Collections.Generic.List<System.Collections.Generic.List<UnityEngine.Vector2>> m_pathCache;  // 0x50
        private System.Int32 m_pathTotalPointCount;  // 0x58

        // Properties
        System.Boolean IsValid { get; /* RVA: 0x06793EA0 */ }

        // Methods
        // RVA: 0x06792B88  token: 0x60000A2
        public System.Boolean CameraWasDisplaced(Cinemachine.CinemachineVirtualCameraBase vcam) { }
        // RVA: 0x06793668  token: 0x60000A3
        public System.Single GetCameraDisplacementDistance(Cinemachine.CinemachineVirtualCameraBase vcam) { }
        // RVA: 0x067936EC  token: 0x60000A4
        private System.Void OnValidate() { }
        // RVA: 0x06793660  token: 0x60000A5
        protected virtual System.Void ConnectToVcam(System.Boolean connect) { }
        // RVA: 0x04D87860  token: 0x60000A7
        public virtual System.Single GetMaxDampTime() { }
        // RVA: 0x067936FC  token: 0x60000A8
        protected virtual System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime) { }
        // RVA: 0x067936C4  token: 0x60000A9
        public System.Void InvalidatePathCache() { }
        // RVA: 0x06793994  token: 0x60000AA
        private System.Boolean ValidatePathCache() { }
        // RVA: 0x06792BA4  token: 0x60000AB
        private UnityEngine.Vector3 ConfinePoint(UnityEngine.Vector3 camPos) { }
        // RVA: 0x0679301C  token: 0x60000AC
        private UnityEngine.Vector3 ConfineScreenEdges(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CameraState& state) { }
        // RVA: 0x06793E84  token: 0x60000AD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001D  // size: 0x128
    public class CinemachineConfiner2D : Cinemachine.CinemachineExtension
    {
        // Fields
        public UnityEngine.Collider2D m_BoundingShape2D;  // 0x28
        public System.Single m_Damping;  // 0x30
        public System.Single m_MaxWindowSize;  // 0x34
        private System.Single m_MaxComputationTimePerFrameInSeconds;  // 0x38
        private static System.Single k_cornerAngleTreshold;  // const
        private Cinemachine.CinemachineConfiner2D.ShapeCache m_shapeCache;  // 0x40

        // Methods
        // RVA: 0x067925B4  token: 0x60000AF
        public System.Void InvalidateCache() { }
        // RVA: 0x06792B30  token: 0x60000B0
        public System.Boolean ValidateCache(System.Single cameraAspectRatio) { }
        // RVA: 0x067925DC  token: 0x60000B1
        protected virtual System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime) { }
        // RVA: 0x06792518  token: 0x60000B2
        private System.Single CalculateHalfFrustumHeight(Cinemachine.CameraState& state, System.Single& cameraPosLocalZ) { }
        // RVA: 0x067925C0  token: 0x60000B3
        private System.Void OnValidate() { }
        // RVA: 0x04D87850  token: 0x60000B4
        private System.Void Reset() { }
        // RVA: 0x06792B6C  token: 0x60000B5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000020  // size: 0x38
    public class CinemachineDollyCart : UnityEngine.MonoBehaviour
    {
        // Fields
        public Cinemachine.CinemachinePathBase m_Path;  // 0x18
        public System.Boolean forbidRotation;  // 0x20
        public Cinemachine.CinemachineDollyCart.UpdateMethod m_UpdateMethod;  // 0x24
        public Cinemachine.CinemachinePathBase.PositionUnits m_PositionUnits;  // 0x28
        public System.Single m_Speed;  // 0x2c
        public System.Single m_Position;  // 0x30

        // Methods
        // RVA: 0x06793FA0  token: 0x60000BB
        private System.Void FixedUpdate() { }
        // RVA: 0x0679419C  token: 0x60000BC
        private System.Void Update() { }
        // RVA: 0x06793FF0  token: 0x60000BD
        private System.Void LateUpdate() { }
        // RVA: 0x06794054  token: 0x60000BE
        private System.Void SetCartPosition(System.Single distanceAlongPath) { }
        // RVA: 0x067941FC  token: 0x60000BF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000022  // size: 0x1E8
    public class CinemachineExternalCamera : Cinemachine.CinemachineVirtualCameraBase
    {
        // Fields
        public UnityEngine.Transform m_LookAt;  // 0xa8
        private UnityEngine.Camera m_Camera;  // 0xb0
        private Cinemachine.CameraState m_State;  // 0xb8
        private UnityEngine.Transform <Follow>k__BackingField;  // 0x1d8
        public Cinemachine.CinemachineVirtualCameraBase.BlendHint m_BlendHint;  // 0x1e0

        // Properties
        Cinemachine.CameraState State { get; /* RVA: 0x0441A2B0 */ }
        UnityEngine.Transform LookAt { get; /* RVA: 0x04D86290 */ set; /* RVA: 0x04D849C0 */ }
        UnityEngine.Transform Follow { get; /* RVA: 0x04D87870 */ set; /* RVA: 0x04D849E0 */ }

        // Methods
        // RVA: 0x0391CE60  token: 0x60000C5
        public virtual System.Void InternalUpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x04220280  token: 0x60000C6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000023  // size: 0x38
    public class CinemachineFollowZoom : Cinemachine.CinemachineExtension
    {
        // Fields
        public System.Single m_Width;  // 0x28
        public System.Single m_Damping;  // 0x2c
        public System.Single m_MinFOV;  // 0x30
        public System.Single m_MaxFOV;  // 0x34

        // Methods
        // RVA: 0x06794218  token: 0x60000C7
        private System.Void OnValidate() { }
        // RVA: 0x04D86AF0  token: 0x60000C8
        public virtual System.Single GetMaxDampTime() { }
        // RVA: 0x06794274  token: 0x60000C9
        protected virtual System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime) { }
        // RVA: 0x06794530  token: 0x60000CA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000025  // size: 0x3B8
    public class CinemachineFreeLook : Cinemachine.CinemachineVirtualCameraBase
    {
        // Fields
        public UnityEngine.Transform m_LookAt;  // 0xa8
        public UnityEngine.Transform m_Follow;  // 0xb0
        public System.Boolean m_CommonLens;  // 0xb8
        public Cinemachine.LensSettings m_Lens;  // 0xbc
        public Cinemachine.CinemachineVirtualCameraBase.TransitionParams m_Transitions;  // 0xf0
        private Cinemachine.CinemachineVirtualCameraBase.BlendHint m_LegacyBlendHint;  // 0x100
        public Cinemachine.AxisState m_YAxis;  // 0x108
        public Cinemachine.AxisState.Recentering m_YAxisRecentering;  // 0x178
        public Cinemachine.AxisState m_XAxis;  // 0x198
        public Cinemachine.CinemachineOrbitalTransposer.Heading m_Heading;  // 0x208
        public Cinemachine.AxisState.Recentering m_RecenterToTargetHeading;  // 0x214
        public Cinemachine.CinemachineTransposer.BindingMode m_BindingMode;  // 0x230
        public System.Single m_SplineCurvature;  // 0x234
        public Cinemachine.CinemachineFreeLook.Orbit[] m_Orbits;  // 0x238
        private System.Single m_LegacyHeadingBias;  // 0x240
        private System.Boolean mUseLegacyRigDefinitions;  // 0x244
        private System.Boolean mIsDestroyed;  // 0x245
        private Cinemachine.CameraState m_State;  // 0x248
        private Cinemachine.CinemachineVirtualCamera[] m_Rigs;  // 0x368
        private Cinemachine.CinemachineOrbitalTransposer[] mOrbitals;  // 0x370
        private Cinemachine.CinemachineBlend mBlendA;  // 0x378
        private Cinemachine.CinemachineBlend mBlendB;  // 0x380
        public static Cinemachine.CinemachineFreeLook.CreateRigDelegate CreateRigOverride;  // static @ 0x0
        public static Cinemachine.CinemachineFreeLook.DestroyRigDelegate DestroyRigOverride;  // static @ 0x8
        private System.Single m_CachedXAxisHeading;  // 0x388
        private Cinemachine.CinemachineFreeLook.Orbit[] m_CachedOrbits;  // 0x390
        private System.Single m_CachedTension;  // 0x398
        private UnityEngine.Vector4[] m_CachedKnots;  // 0x3a0
        private UnityEngine.Vector4[] m_CachedCtrl1;  // 0x3a8
        private UnityEngine.Vector4[] m_CachedCtrl2;  // 0x3b0

        // Properties
        System.String[] RigNames { get; /* RVA: 0x06798398 */ }
        System.Boolean PreviousStateIsValid { get; /* RVA: 0x04D86F70 */ set; /* RVA: 0x06798440 */ }
        Cinemachine.CameraState State { get; /* RVA: 0x04D87880 */ }
        UnityEngine.Transform LookAt { get; /* RVA: 0x0678D7AC */ set; /* RVA: 0x04D84CC0 */ }
        UnityEngine.Transform Follow { get; /* RVA: 0x0678D790 */ set; /* RVA: 0x04D84CE0 */ }

        // Methods
        // RVA: 0x06796E2C  token: 0x60000CC
        protected virtual System.Void OnValidate() { }
        // RVA: 0x06795620  token: 0x60000CD
        public Cinemachine.CinemachineVirtualCamera GetRig(System.Int32 i) { }
        // RVA: 0x067969A4  token: 0x60000CF
        protected virtual System.Void OnEnable() { }
        // RVA: 0x06797A6C  token: 0x60000D0
        public System.Void UpdateInputAxisProvider() { }
        // RVA: 0x06796888  token: 0x60000D1
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x06796060  token: 0x60000D2
        private System.Void OnTransformChildrenChanged() { }
        // RVA: 0x067975C4  token: 0x60000D3
        private System.Void Reset() { }
        // RVA: 0x06796078  token: 0x60000DB
        public virtual System.Boolean IsLiveChild(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly) { }
        // RVA: 0x067969E4  token: 0x60000DC
        public virtual System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta) { }
        // RVA: 0x06795100  token: 0x60000DD
        public virtual System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot) { }
        // RVA: 0x06795A44  token: 0x60000DE
        public virtual System.Void InternalUpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x06796AA8  token: 0x60000DF
        public virtual System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x02FFF600  token: 0x60000E0
        private virtual System.Boolean RequiresUserInput() { }
        // RVA: 0x06795670  token: 0x60000E1
        private System.Single GetYAxisClosestValue(UnityEngine.Vector3 cameraPos, UnityEngine.Vector3 up) { }
        // RVA: 0x06796060  token: 0x60000E2
        private System.Void InvalidateRigCache() { }
        // RVA: 0x06794C7C  token: 0x60000E3
        private System.Void DestroyRigs() { }
        // RVA: 0x06794828  token: 0x60000E4
        private Cinemachine.CinemachineVirtualCamera[] CreateRigs(Cinemachine.CinemachineVirtualCamera[] copyFrom) { }
        // RVA: 0x06797B00  token: 0x60000E5
        private System.Void UpdateRigCache() { }
        // RVA: 0x06796184  token: 0x60000E6
        private System.Int32 LocateExistingRigs(System.String[] rigNames, System.Boolean forceOrbital) { }
        // RVA: 0x06797D38  token: 0x60000E7
        private System.Single UpdateXAxisHeading(Cinemachine.CinemachineOrbitalTransposer orbital, System.Single deltaTime, UnityEngine.Vector3 up) { }
        // RVA: 0x06796F6C  token: 0x60000E8
        private System.Void PushSettingsToRigs() { }
        // RVA: 0x06795A14  token: 0x60000E9
        private System.Single GetYAxisValue() { }
        // RVA: 0x06794564  token: 0x60000EA
        private Cinemachine.CameraState CalculateNewState(UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x067953E0  token: 0x60000EB
        public UnityEngine.Vector3 GetLocalPositionForCameraFromInput(System.Single t) { }
        // RVA: 0x067975CC  token: 0x60000EC
        private System.Void UpdateCachedSpline() { }
        // RVA: 0x06797EC8  token: 0x60000ED
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000029  // size: 0x210
    public class CinemachineMixingCamera : Cinemachine.CinemachineVirtualCameraBase
    {
        // Fields
        public static System.Int32 MaxCameras;  // const
        public System.Single m_Weight0;  // 0xa8
        public System.Single m_Weight1;  // 0xac
        public System.Single m_Weight2;  // 0xb0
        public System.Single m_Weight3;  // 0xb4
        public System.Single m_Weight4;  // 0xb8
        public System.Single m_Weight5;  // 0xbc
        public System.Single m_Weight6;  // 0xc0
        public System.Single m_Weight7;  // 0xc4
        private Cinemachine.CameraState m_State;  // 0xc8
        private Cinemachine.ICinemachineCamera <LiveChild>k__BackingField;  // 0x1e8
        private UnityEngine.Transform <LookAt>k__BackingField;  // 0x1f0
        private UnityEngine.Transform <Follow>k__BackingField;  // 0x1f8
        private Cinemachine.CinemachineVirtualCameraBase[] m_ChildCameras;  // 0x200
        private System.Collections.Generic.Dictionary<Cinemachine.CinemachineVirtualCameraBase,System.Int32> m_indexMap;  // 0x208

        // Properties
        Cinemachine.ICinemachineCamera LiveChild { get; /* RVA: 0x04D87930 */ set; /* RVA: 0x06799DA0 */ }
        Cinemachine.CameraState State { get; /* RVA: 0x04D87950 */ }
        UnityEngine.Transform LookAt { get; /* RVA: 0x04D87940 */ set; /* RVA: 0x06799DB4 */ }
        UnityEngine.Transform Follow { get; /* RVA: 0x04D87920 */ set; /* RVA: 0x06799D8C */ }
        Cinemachine.CinemachineVirtualCameraBase[] ChildCameras { get; /* RVA: 0x06799D6C */ }

        // Methods
        // RVA: 0x06798F24  token: 0x60000F3
        public System.Single GetWeight(System.Int32 index) { }
        // RVA: 0x067998C8  token: 0x60000F4
        public System.Void SetWeight(System.Int32 index, System.Single w) { }
        // RVA: 0x06798E08  token: 0x60000F5
        public System.Single GetWeight(Cinemachine.CinemachineVirtualCameraBase vcam) { }
        // RVA: 0x067997A0  token: 0x60000F6
        public System.Void SetWeight(Cinemachine.CinemachineVirtualCameraBase vcam, System.Single w) { }
        // RVA: 0x06799524  token: 0x60000FE
        public virtual System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta) { }
        // RVA: 0x06798D28  token: 0x60000FF
        public virtual System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot) { }
        // RVA: 0x06799504  token: 0x6000100
        protected virtual System.Void OnEnable() { }
        // RVA: 0x067995E8  token: 0x6000101
        public System.Void OnTransformChildrenChanged() { }
        // RVA: 0x06799744  token: 0x6000102
        protected virtual System.Void OnValidate() { }
        // RVA: 0x06799468  token: 0x6000103
        public virtual System.Boolean IsLiveChild(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly) { }
        // RVA: 0x06799420  token: 0x6000105
        protected System.Void InvalidateListOfChildren() { }
        // RVA: 0x067999D8  token: 0x6000106
        protected System.Void ValidateListOfChildren() { }
        // RVA: 0x067995F0  token: 0x6000107
        public virtual System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x06799028  token: 0x6000108
        public virtual System.Void InternalUpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x06799C08  token: 0x6000109
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002A  // size: 0x58
    public class CinemachinePath : Cinemachine.CinemachinePathBase
    {
        // Fields
        public System.Boolean m_Looped;  // 0x48
        public Cinemachine.CinemachinePath.Waypoint[] m_Waypoints;  // 0x50

        // Properties
        System.Single MinPos { get; /* RVA: 0x04D879D0 */ }
        System.Single MaxPos { get; /* RVA: 0x0679A718 */ }
        System.Boolean Looped { get; /* RVA: 0x04D867D0 */ }
        System.Int32 DistanceCacheSampleStepsPerSegment { get; /* RVA: 0x011EF5B0 */ }

        // Methods
        // RVA: 0x0679A5A8  token: 0x600010D
        private System.Void Reset() { }
        // RVA: 0x0679A4C0  token: 0x600010F
        private System.Single GetBoundingIndices(System.Single pos, System.Int32& indexA, System.Int32& indexB) { }
        // RVA: 0x0679A058  token: 0x6000110
        public virtual UnityEngine.Vector3 EvaluatePosition(System.Single pos) { }
        // RVA: 0x0679A268  token: 0x6000111
        public virtual UnityEngine.Vector3 EvaluateTangent(System.Single pos) { }
        // RVA: 0x06799DC8  token: 0x6000112
        public virtual UnityEngine.Quaternion EvaluateOrientation(System.Single pos) { }
        // RVA: 0x0679A598  token: 0x6000113
        private System.Void OnValidate() { }
        // RVA: 0x0679A6C8  token: 0x6000114
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002C  // size: 0x18
    public sealed class CinemachinePipeline : UnityEngine.MonoBehaviour
    {
        // Methods
        // RVA: 0x03695570  token: 0x6000115
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002D  // size: 0x18
    public class CinemachinePixelPerfect : UnityEngine.MonoBehaviour
    {
        // Methods
        // RVA: 0x05393520  token: 0x6000116
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002E  // size: 0x70
    public class CinemachineSmoothPath : Cinemachine.CinemachinePathBase
    {
        // Fields
        public System.Boolean m_Looped;  // 0x48
        public Cinemachine.CinemachineSmoothPath.Waypoint[] m_Waypoints;  // 0x50
        private Cinemachine.CinemachineSmoothPath.Waypoint[] m_ControlPoints1;  // 0x58
        private Cinemachine.CinemachineSmoothPath.Waypoint[] m_ControlPoints2;  // 0x60
        private System.Boolean m_IsLoopedCache;  // 0x68

        // Properties
        System.Single MinPos { get; /* RVA: 0x04D879D0 */ }
        System.Single MaxPos { get; /* RVA: 0x0679A718 */ }
        System.Boolean Looped { get; /* RVA: 0x04D867D0 */ }
        System.Int32 DistanceCacheSampleStepsPerSegment { get; /* RVA: 0x011EF5B0 */ }

        // Methods
        // RVA: 0x0679A598  token: 0x600011B
        private System.Void OnValidate() { }
        // RVA: 0x067A3C1C  token: 0x600011C
        private System.Void Reset() { }
        // RVA: 0x067A3BE8  token: 0x600011D
        public virtual System.Void InvalidateDistanceCache() { }
        // RVA: 0x067A3D24  token: 0x600011E
        private System.Void UpdateControlPoints() { }
        // RVA: 0x067A3B3C  token: 0x600011F
        private System.Single GetBoundingIndices(System.Single pos, System.Int32& indexA, System.Int32& indexB) { }
        // RVA: 0x067A36E8  token: 0x6000120
        public virtual UnityEngine.Vector3 EvaluatePosition(System.Single pos) { }
        // RVA: 0x067A38E0  token: 0x6000121
        public virtual UnityEngine.Vector3 EvaluateTangent(System.Single pos) { }
        // RVA: 0x067A3458  token: 0x6000122
        public virtual UnityEngine.Quaternion EvaluateOrientation(System.Single pos) { }
        // RVA: 0x04D87D00  token: 0x6000123
        private UnityEngine.Quaternion RollAroundForward(System.Single angle) { }
        // RVA: 0x067A3F2C  token: 0x6000124
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000030  // size: 0x298
    public class CinemachineStateDrivenCamera : Cinemachine.CinemachineVirtualCameraBase
    {
        // Fields
        public UnityEngine.Transform m_LookAt;  // 0xa8
        public UnityEngine.Transform m_Follow;  // 0xb0
        public UnityEngine.Animator m_AnimatedTarget;  // 0xb8
        public System.Int32 m_LayerIndex;  // 0xc0
        public System.Boolean m_ShowDebugText;  // 0xc4
        private Cinemachine.CinemachineVirtualCameraBase[] m_ChildCameras;  // 0xc8
        public Cinemachine.CinemachineStateDrivenCamera.Instruction[] m_Instructions;  // 0xd0
        public Cinemachine.CinemachineBlendDefinition m_DefaultBlend;  // 0xd8
        public Cinemachine.CinemachineBlenderSettings m_CustomBlends;  // 0xf0
        private Cinemachine.CinemachineStateDrivenCamera.ParentHash[] m_ParentHash;  // 0xf8
        private Cinemachine.ICinemachineCamera <LiveChild>k__BackingField;  // 0x100
        private Cinemachine.ICinemachineCamera m_TransitioningFrom;  // 0x108
        private Cinemachine.CameraState m_State;  // 0x110
        private System.Collections.Generic.Dictionary<UnityEngine.AnimationClip,System.Collections.Generic.List<Cinemachine.CinemachineStateDrivenCamera.HashPair>> mHashCache;  // 0x230
        private System.Single mActivationTime;  // 0x238
        private Cinemachine.CinemachineStateDrivenCamera.Instruction mActiveInstruction;  // 0x240
        private System.Single mPendingActivationTime;  // 0x258
        private Cinemachine.CinemachineStateDrivenCamera.Instruction mPendingInstruction;  // 0x260
        private Cinemachine.CinemachineBlend mActiveBlend;  // 0x278
        private System.Collections.Generic.Dictionary<System.Int32,System.Int32> mInstructionDictionary;  // 0x280
        private System.Collections.Generic.Dictionary<System.Int32,System.Int32> mStateParentLookup;  // 0x288
        private System.Collections.Generic.List<UnityEngine.AnimatorClipInfo> m_clipInfoList;  // 0x290

        // Properties
        System.String Description { get; /* RVA: 0x067A5DA8 */ }
        Cinemachine.ICinemachineCamera LiveChild { get; /* RVA: 0x04D87D50 */ set; /* RVA: 0x067A5EA0 */ }
        Cinemachine.CameraState State { get; /* RVA: 0x04D87D60 */ }
        UnityEngine.Transform LookAt { get; /* RVA: 0x0678D7AC */ set; /* RVA: 0x04D84CC0 */ }
        UnityEngine.Transform Follow { get; /* RVA: 0x0678D790 */ set; /* RVA: 0x04D84CE0 */ }
        Cinemachine.CinemachineVirtualCameraBase[] ChildCameras { get; /* RVA: 0x067A5D88 */ }
        System.Boolean IsBlending { get; /* RVA: 0x067A5E94 */ }

        // Methods
        // RVA: 0x067A4D3C  token: 0x600012A
        public virtual System.Boolean IsLiveChild(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly) { }
        // RVA: 0x067A55A4  token: 0x6000130
        public virtual System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta) { }
        // RVA: 0x067A458C  token: 0x6000131
        public virtual System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot) { }
        // RVA: 0x067A5670  token: 0x6000132
        public virtual System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x067A4788  token: 0x6000133
        public virtual System.Void InternalUpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x067A5270  token: 0x6000134
        protected virtual System.Void OnEnable() { }
        // RVA: 0x067A5158  token: 0x6000135
        protected virtual System.Void OnDisable() { }
        // RVA: 0x067A5668  token: 0x6000136
        public System.Void OnTransformChildrenChanged() { }
        // RVA: 0x067A5460  token: 0x6000137
        private System.Void OnGuiHandler() { }
        // RVA: 0x067A4510  token: 0x600013A
        public static System.Int32 CreateFakeHash(System.Int32 parentHash, UnityEngine.AnimationClip clip) { }
        // RVA: 0x067A4F3C  token: 0x600013B
        private System.Int32 LookupFakeHash(System.Int32 parentHash, UnityEngine.AnimationClip clip) { }
        // RVA: 0x067A4D08  token: 0x600013C
        private System.Void InvalidateListOfChildren() { }
        // RVA: 0x067A572C  token: 0x600013D
        private System.Void UpdateListOfChildren() { }
        // RVA: 0x067A58E0  token: 0x600013E
        private System.Void ValidateInstructions() { }
        // RVA: 0x067A3F60  token: 0x600013F
        private Cinemachine.CinemachineVirtualCameraBase ChooseCurrentCamera() { }
        // RVA: 0x067A466C  token: 0x6000140
        private System.Int32 GetClipHash(System.Int32 hash, System.Collections.Generic.List<UnityEngine.AnimatorClipInfo> clips) { }
        // RVA: 0x067A4D64  token: 0x6000141
        private Cinemachine.CinemachineBlendDefinition LookupBlend(Cinemachine.ICinemachineCamera fromKey, Cinemachine.ICinemachineCamera toKey) { }
        // RVA: 0x067A5BAC  token: 0x6000142
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000034  // size: 0x78
    public class CinemachineStoryboard : Cinemachine.CinemachineExtension
    {
        // Fields
        public static System.Boolean s_StoryboardGlobalMute;  // static @ 0x0
        public System.Boolean m_ShowImage;  // 0x28
        public UnityEngine.Texture m_Image;  // 0x30
        public Cinemachine.CinemachineStoryboard.FillStrategy m_Aspect;  // 0x38
        public System.Single m_Alpha;  // 0x3c
        public UnityEngine.Vector2 m_Center;  // 0x40
        public UnityEngine.Vector3 m_Rotation;  // 0x48
        public UnityEngine.Vector2 m_Scale;  // 0x54
        public System.Boolean m_SyncScale;  // 0x5c
        public System.Boolean m_MuteCamera;  // 0x5d
        public System.Single m_SplitView;  // 0x60
        public Cinemachine.CinemachineStoryboard.StoryboardRenderMode m_RenderMode;  // 0x64
        public System.Int32 m_SortingOrder;  // 0x68
        public System.Single m_PlaneDistance;  // 0x6c
        private System.Collections.Generic.List<Cinemachine.CinemachineStoryboard.CanvasInfo> mCanvasInfo;  // 0x70

        // Properties
        System.String CanvasName { get; /* RVA: 0x067A75B8 */ }

        // Methods
        // RVA: 0x067A7220  token: 0x6000143
        protected virtual System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime) { }
        // RVA: 0x067A731C  token: 0x6000144
        private System.Void UpdateRenderCanvas() { }
        // RVA: 0x067A6064  token: 0x6000145
        protected virtual System.Void ConnectToVcam(System.Boolean connect) { }
        // RVA: 0x067A5EB4  token: 0x6000147
        private System.Void CameraUpdatedCallback(Cinemachine.CinemachineBrain brain) { }
        // RVA: 0x067A6804  token: 0x6000148
        private Cinemachine.CinemachineStoryboard.CanvasInfo LocateMyCanvas(Cinemachine.CinemachineBrain parent, System.Boolean createIfNotFound) { }
        // RVA: 0x067A61C8  token: 0x6000149
        private System.Void CreateCanvas(Cinemachine.CinemachineStoryboard.CanvasInfo ci) { }
        // RVA: 0x067A6614  token: 0x600014A
        private System.Void DestroyCanvas() { }
        // RVA: 0x067A6BE8  token: 0x600014B
        private System.Void PlaceImage(Cinemachine.CinemachineStoryboard.CanvasInfo ci, System.Single alpha) { }
        // RVA: 0x031294E0  token: 0x600014C
        private static System.Void StaticBlendingHandler(Cinemachine.CinemachineBrain brain) { }
        // RVA: 0x0394EED0  token: 0x600014D
        private static System.Void InitializeModule() { }
        // RVA: 0x067A74C0  token: 0x600014E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000038
    public interface ICinemachineTargetGroup
    {
        // Properties
        UnityEngine.BoundingSphere Sphere { get; /* RVA: -1  // abstract */ }
        System.Boolean IsEmpty { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000152
        public virtual UnityEngine.Bounds GetViewSpaceBoundingBox(UnityEngine.Matrix4x4 observer) { }
        // RVA: -1  // abstract  token: 0x6000153
        public virtual System.Void GetViewSpaceAngularBounds(UnityEngine.Matrix4x4 observer, UnityEngine.Vector2& minAngles, UnityEngine.Vector2& maxAngles, UnityEngine.Vector2& zRange) { }

    }

    // TypeToken: 0x2000039  // size: 0x68
    public class CinemachineTargetGroup : UnityEngine.MonoBehaviour, Cinemachine.ICinemachineTargetGroup
    {
        // Fields
        public Cinemachine.CinemachineTargetGroup.PositionMode m_PositionMode;  // 0x18
        public Cinemachine.CinemachineTargetGroup.RotationMode m_RotationMode;  // 0x1c
        public Cinemachine.CinemachineTargetGroup.UpdateMethod m_UpdateMethod;  // 0x20
        public Cinemachine.CinemachineTargetGroup.Target[] m_Targets;  // 0x28
        private UnityEngine.Bounds <BoundingBox>k__BackingField;  // 0x30
        private System.Single m_MaxWeight;  // 0x48
        private UnityEngine.Vector3 m_AveragePos;  // 0x4c
        private UnityEngine.BoundingSphere m_BoundingSphere;  // 0x58

        // Properties
        UnityEngine.Transform Transform { get; /* RVA: 0x053953F4 */ }
        UnityEngine.Bounds BoundingBox { get; /* RVA: 0x04D87DE0 */ set; /* RVA: 0x04D87E10 */ }
        UnityEngine.BoundingSphere Sphere { get; /* RVA: 0x04D87E00 */ }
        System.Boolean IsEmpty { get; /* RVA: 0x067A8BE8 */ }

        // Methods
        // RVA: 0x067A7624  token: 0x6000159
        public System.Void AddMember(UnityEngine.Transform t, System.Single weight, System.Single radius) { }
        // RVA: 0x067A89EC  token: 0x600015A
        public System.Void RemoveMember(UnityEngine.Transform t) { }
        // RVA: 0x067A814C  token: 0x600015B
        public System.Int32 FindMember(UnityEngine.Transform t) { }
        // RVA: 0x067A8894  token: 0x600015C
        public UnityEngine.BoundingSphere GetWeightedBoundsForMember(System.Int32 index) { }
        // RVA: 0x067A869C  token: 0x600015D
        public virtual UnityEngine.Bounds GetViewSpaceBoundingBox(UnityEngine.Matrix4x4 observer) { }
        // RVA: 0x067A8AE0  token: 0x600015E
        private static UnityEngine.BoundingSphere WeightedMemberBounds(Cinemachine.CinemachineTargetGroup.Target t, UnityEngine.Vector3 avgPos, System.Single maxWeight) { }
        // RVA: 0x067A7FFC  token: 0x600015F
        public System.Void DoUpdate() { }
        // RVA: 0x067A7D3C  token: 0x6000160
        private UnityEngine.BoundingSphere CalculateBoundingSphere(System.Single maxWeight) { }
        // RVA: 0x067A7930  token: 0x6000161
        private UnityEngine.Vector3 CalculateAveragePosition(System.Single& maxWeight) { }
        // RVA: 0x067A773C  token: 0x6000162
        private UnityEngine.Quaternion CalculateAverageOrientation() { }
        // RVA: 0x067A7B58  token: 0x6000163
        private UnityEngine.Bounds CalculateBoundingBox(UnityEngine.Vector3 avgPos, System.Single maxWeight) { }
        // RVA: 0x067A893C  token: 0x6000164
        private System.Void OnValidate() { }
        // RVA: 0x067A81EC  token: 0x6000165
        private System.Void FixedUpdate() { }
        // RVA: 0x067A8AB0  token: 0x6000166
        private System.Void Update() { }
        // RVA: 0x067A892C  token: 0x6000167
        private System.Void LateUpdate() { }
        // RVA: 0x067A81FC  token: 0x6000168
        public virtual System.Void GetViewSpaceAngularBounds(UnityEngine.Matrix4x4 observer, UnityEngine.Vector2& minAngles, UnityEngine.Vector2& maxAngles, UnityEngine.Vector2& zRange) { }
        // RVA: 0x04CEAC30  token: 0x6000169
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003E  // size: 0x240
    public class CinemachineVirtualCamera : Cinemachine.CinemachineVirtualCameraBase
    {
        // Fields
        public UnityEngine.Transform m_LookAt;  // 0xa8
        public UnityEngine.Transform m_Follow;  // 0xb0
        public Cinemachine.LensSettings m_Lens;  // 0xb8
        public Cinemachine.CinemachineVirtualCameraBase.TransitionParams m_Transitions;  // 0xe8
        private Cinemachine.CinemachineVirtualCameraBase.BlendHint m_LegacyBlendHint;  // 0xf8
        public static System.String PipelineName;  // const
        public static Cinemachine.CinemachineVirtualCamera.CreatePipelineDelegate CreatePipelineOverride;  // static @ 0x0
        public static Cinemachine.CinemachineVirtualCamera.DestroyPipelineDelegate DestroyPipelineOverride;  // static @ 0x8
        private System.Boolean m_UserIsDragging;  // 0xfc
        protected Cinemachine.CameraState m_State;  // 0x100
        private Cinemachine.CinemachineComponentBase[] m_ComponentPipeline;  // 0x220
        private UnityEngine.Transform m_ComponentOwner;  // 0x228
        private UnityEngine.Transform mCachedLookAtTarget;  // 0x230
        private Cinemachine.CinemachineVirtualCameraBase mCachedLookAtTargetVcam;  // 0x238

        // Properties
        Cinemachine.CameraState State { get; /* RVA: 0x04D877D0 */ }
        UnityEngine.Transform LookAt { get; /* RVA: 0x031FD680 */ set; /* RVA: 0x04D84CC0 */ }
        UnityEngine.Transform Follow { get; /* RVA: 0x031FE3A0 */ set; /* RVA: 0x04D84CE0 */ }

        // Methods
        // RVA: 0x04D87E30  token: 0x600016F
        public System.Void SetFieldOfView(System.Single fov) { }
        // RVA: 0x067A9404  token: 0x6000170
        public virtual System.Single GetMaxDampTime() { }
        // RVA: 0x031FD040  token: 0x6000171
        public virtual System.Void InternalUpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x0413B3D0  token: 0x6000172
        protected virtual System.Void OnEnable() { }
        // RVA: 0x067A9490  token: 0x6000173
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x067A9870  token: 0x6000174
        protected virtual System.Void OnValidate() { }
        // RVA: 0x0539700C  token: 0x6000175
        private System.Void OnTransformChildrenChanged() { }
        // RVA: 0x067A9A10  token: 0x6000176
        private System.Void Reset() { }
        // RVA: 0x067A8E04  token: 0x6000177
        private System.Void DestroyPipeline() { }
        // RVA: 0x067A8C90  token: 0x6000178
        private UnityEngine.Transform CreatePipeline(Cinemachine.CinemachineVirtualCamera copyFrom) { }
        // RVA: 0x0539700C  token: 0x6000179
        public System.Void InvalidateComponentPipeline() { }
        // RVA: 0x067A93E4  token: 0x600017A
        public UnityEngine.Transform GetComponentOwner() { }
        // RVA: 0x05397AD4  token: 0x600017B
        public Cinemachine.CinemachineComponentBase[] GetComponentPipeline() { }
        // RVA: 0x04638530  token: 0x600017C
        public Cinemachine.CinemachineComponentBase GetCinemachineComponent(Cinemachine.CinemachineCore.Stage stage) { }
        // RVA: -1  // generic def  token: 0x600017D
        public T GetCinemachineComponent() { }
        // RVA: -1  // generic def  token: 0x600017E
        public T AddCinemachineComponent() { }
        // RVA: -1  // generic def  token: 0x600017F
        public System.Void DestroyCinemachineComponent() { }
        // RVA: 0x03E69980  token: 0x6000180
        private System.Void UpdateComponentPipeline() { }
        // RVA: 0x03E6A460  token: 0x6000181
        private static System.Void SetFlagsForHiddenChild(UnityEngine.GameObject child) { }
        // RVA: 0x031FD930  token: 0x6000182
        protected Cinemachine.CameraState CalculateNewState(UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x067A969C  token: 0x6000183
        public virtual System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta) { }
        // RVA: 0x067A9274  token: 0x6000184
        public virtual System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot) { }
        // RVA: 0x04D87E40  token: 0x6000185
        private System.Void SetStateRawPosition(UnityEngine.Vector3 pos) { }
        // RVA: 0x03E695D0  token: 0x6000186
        public virtual System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x067A98DC  token: 0x6000187
        private virtual System.Boolean RequiresUserInput() { }
        // RVA: 0x042203E0  token: 0x6000188
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000042  // size: 0x80
    public class Cinemachine3rdPersonFollow : Cinemachine.CinemachineComponentBase
    {
        // Fields
        public UnityEngine.Vector3 Damping;  // 0x20
        public UnityEngine.Vector3 ShoulderOffset;  // 0x2c
        public System.Single VerticalArmLength;  // 0x38
        public System.Single CameraSide;  // 0x3c
        public System.Single CameraDistance;  // 0x40
        public UnityEngine.LayerMask CameraCollisionFilter;  // 0x44
        public System.String IgnoreTag;  // 0x48
        public System.Single CameraRadius;  // 0x50
        public System.Single DampingIntoCollision;  // 0x54
        public System.Single DampingFromCollision;  // 0x58
        public System.Boolean DisableDampingCorrection;  // 0x5c
        private UnityEngine.Vector3 m_PreviousFollowTargetPosition;  // 0x60
        private UnityEngine.Vector3 m_DampingCorrection;  // 0x6c
        private System.Single m_CamPosCollisionCorrection;  // 0x78

        // Properties
        System.Boolean IsValid { get; /* RVA: 0x031FA930 */ }
        Cinemachine.CinemachineCore.Stage Stage { get; /* RVA: 0x011EC580 */ }

        // Methods
        // RVA: 0x0679C2FC  token: 0x6000191
        private System.Void OnValidate() { }
        // RVA: 0x04D87A40  token: 0x6000192
        private System.Void Reset() { }
        // RVA: 0x0679C1FC  token: 0x6000193
        private System.Void OnDestroy() { }
        // RVA: 0x0679BFEC  token: 0x6000196
        public virtual System.Single GetMaxDampTime() { }
        // RVA: 0x03248DB0  token: 0x6000197
        public virtual System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime) { }
        // RVA: 0x0679C23C  token: 0x6000198
        public virtual System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta) { }
        // RVA: 0x0324B240  token: 0x6000199
        private System.Void PositionCamera(Cinemachine.CameraState& curState, System.Single deltaTime) { }
        // RVA: 0x0679C010  token: 0x600019A
        public System.Void GetRigPositions(UnityEngine.Vector3& root, UnityEngine.Vector3& shoulder, UnityEngine.Vector3& hand) { }
        // RVA: 0x0324B060  token: 0x600019B
        private static UnityEngine.Quaternion GetHeading(UnityEngine.Quaternion targetRot, UnityEngine.Vector3 up) { }
        // RVA: 0x03D42CA0  token: 0x600019C
        private System.Void GetRawRigPositions(UnityEngine.Vector3 root, UnityEngine.Quaternion targetRot, UnityEngine.Quaternion heading, UnityEngine.Vector3& shoulder, UnityEngine.Vector3& hand) { }
        // RVA: 0x0679BD94  token: 0x600019D
        private UnityEngine.Vector3 BeyondResolveCollisions(UnityEngine.Vector3 root, UnityEngine.Vector3 tip, System.Single cameraRadius) { }
        // RVA: 0x0679C378  token: 0x600019E
        private UnityEngine.Vector3 ResolveCollisions(UnityEngine.Vector3 root, UnityEngine.Vector3 tip, System.Single deltaTime, System.Single cameraRadius, System.Single& collisionCorrection) { }
        // RVA: 0x0418E850  token: 0x600019F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000043  // size: 0x50
    public class CinemachineBasicMultiChannelPerlin : Cinemachine.CinemachineComponentBase
    {
        // Fields
        public Cinemachine.NoiseSettings m_NoiseProfile;  // 0x20
        public UnityEngine.Vector3 m_PivotOffset;  // 0x28
        public System.Single m_AmplitudeGain;  // 0x34
        public System.Single m_FrequencyGain;  // 0x38
        private System.Boolean mInitialized;  // 0x3c
        private System.Single mNoiseTime;  // 0x40
        private UnityEngine.Vector3 mNoiseOffsets;  // 0x44

        // Properties
        System.Boolean IsValid { get; /* RVA: 0x0679CDAC */ }
        Cinemachine.CinemachineCore.Stage Stage { get; /* RVA: 0x02BBE540 */ }

        // Methods
        // RVA: 0x0679C780  token: 0x60001A2
        public virtual System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime) { }
        // RVA: 0x0679CCD0  token: 0x60001A3
        public System.Void ReSeed() { }
        // RVA: 0x0679C6C8  token: 0x60001A4
        private System.Void Initialize() { }
        // RVA: 0x0679CD54  token: 0x60001A5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000044  // size: 0x110
    public class CinemachineComposer : Cinemachine.CinemachineComponentBase
    {
        // Fields
        public UnityEngine.Vector3 m_TrackedObjectOffset;  // 0x20
        public System.Single m_LookaheadTime;  // 0x2c
        public System.Single m_LookaheadSmoothing;  // 0x30
        public System.Boolean m_LookaheadIgnoreY;  // 0x34
        public System.Single m_HorizontalDamping;  // 0x38
        public System.Single m_VerticalDamping;  // 0x3c
        public System.Single m_ScreenX;  // 0x40
        public System.Single m_ScreenY;  // 0x44
        public System.Single m_DeadZoneWidth;  // 0x48
        public System.Single m_DeadZoneHeight;  // 0x4c
        public System.Single m_SoftZoneWidth;  // 0x50
        public System.Single m_SoftZoneHeight;  // 0x54
        public System.Single m_BiasX;  // 0x58
        public System.Single m_BiasY;  // 0x5c
        public System.Boolean m_CenterOnActivate;  // 0x60
        private UnityEngine.Vector3 <TrackedPoint>k__BackingField;  // 0x64
        private UnityEngine.Vector3 m_CameraPosPrevFrame;  // 0x70
        private UnityEngine.Vector3 m_LookAtPrevFrame;  // 0x7c
        private UnityEngine.Vector2 m_ScreenOffsetPrevFrame;  // 0x88
        private UnityEngine.Quaternion m_CameraOrientationPrevFrame;  // 0x90
        private Cinemachine.Utility.PositionPredictor m_Predictor;  // 0xa0
        private Cinemachine.CinemachineComposer.FovCache mCache;  // 0xa8
        private UnityEngine.Vector2 _lastExtra;  // 0xf8
        private UnityEngine.Vector2 _lastOriginalRotToRect;  // 0x100
        public UnityEngine.Vector2 deadZoneDampFactor;  // 0x108

        // Properties
        System.Boolean IsValid { get; /* RVA: 0x031FC1A0 */ }
        Cinemachine.CinemachineCore.Stage Stage { get; /* RVA: 0x04D85B80 */ }
        UnityEngine.Vector3 TrackedPoint { get; /* RVA: 0x04D87AF0 */ set; /* RVA: 0x04D87B10 */ }
        System.Boolean useTrackedObjectOffsetCollision { get; /* RVA: 0x02FFF600 */ }
        UnityEngine.Rect SoftGuideRect { get; /* RVA: 0x03F650D0 */ set; /* RVA: 0x0679D0BC */ }
        UnityEngine.Rect HardGuideRect { get; /* RVA: 0x03E319F0 */ set; /* RVA: 0x0679CF54 */ }

        // Methods
        // RVA: 0x0324B6C0  token: 0x60001AA
        protected virtual UnityEngine.Vector3 GetLookAtPointAndSetTrackedPoint(UnityEngine.Vector3 lookAt, UnityEngine.Vector3 up, System.Single deltaTime) { }
        // RVA: 0x0679CE18  token: 0x60001AC
        public virtual System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta) { }
        // RVA: 0x04D87AD0  token: 0x60001AD
        public virtual System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot) { }
        // RVA: 0x0679CE08  token: 0x60001AE
        public virtual System.Single GetMaxDampTime() { }
        // RVA: 0x0324AF50  token: 0x60001AF
        public virtual System.Void PrePipelineMutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime) { }
        // RVA: 0x031FEC00  token: 0x60001B0
        public virtual System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime) { }
        // RVA: 0x04D87AB0  token: 0x60001B5
        public System.Void ClearOriginalRotToRect() { }
        // RVA: 0x031FC230  token: 0x60001B6
        private System.Void RotateToScreenBounds(Cinemachine.CameraState& state, UnityEngine.Rect screenRect, UnityEngine.Vector3 trackedPoint, UnityEngine.Quaternion& rigOrientation, System.Single fov, System.Single fovH, System.Single deltaTime) { }
        // RVA: 0x031FC110  token: 0x60001B7
        private System.Boolean ClampVerticalBounds(UnityEngine.Rect& r, UnityEngine.Vector3 dir, UnityEngine.Vector3 up, System.Single fov) { }
        // RVA: 0x0418EA10  token: 0x60001B8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000046  // size: 0x138
    public class CinemachineFramingTransposer : Cinemachine.CinemachineComponentBase
    {
        // Fields
        public UnityEngine.Vector3 m_TrackedObjectOffset;  // 0x20
        public System.Single m_LookaheadTime;  // 0x2c
        public System.Single m_LookaheadSmoothing;  // 0x30
        public System.Boolean m_LookaheadIgnoreY;  // 0x34
        public System.Single m_XDamping;  // 0x38
        public System.Single m_YDamping;  // 0x3c
        public System.Single m_ZDamping;  // 0x40
        public System.Boolean m_TargetMovementOnly;  // 0x44
        public System.Single m_ScreenX;  // 0x48
        public System.Single m_ScreenY;  // 0x4c
        public System.Single m_CameraDistance;  // 0x50
        public System.Single m_DeadZoneWidth;  // 0x54
        public System.Single m_DeadZoneHeight;  // 0x58
        public System.Single m_DeadZoneDepth;  // 0x5c
        public System.Boolean m_UnlimitedSoftZone;  // 0x60
        public System.Single m_SoftZoneWidth;  // 0x64
        public System.Single m_SoftZoneHeight;  // 0x68
        public System.Single m_BiasX;  // 0x6c
        public System.Single m_BiasY;  // 0x70
        public System.Boolean m_CenterOnActivate;  // 0x74
        public Cinemachine.CinemachineFramingTransposer.FramingMode m_GroupFramingMode;  // 0x78
        public Cinemachine.CinemachineFramingTransposer.AdjustmentMode m_AdjustmentMode;  // 0x7c
        public System.Single m_GroupFramingSize;  // 0x80
        public System.Single m_MaxDollyIn;  // 0x84
        public System.Single m_MaxDollyOut;  // 0x88
        public System.Single m_MinimumDistance;  // 0x8c
        public System.Single m_MaximumDistance;  // 0x90
        public System.Single m_MinimumFOV;  // 0x94
        public System.Single m_MaximumFOV;  // 0x98
        public System.Single m_MinimumOrthoSize;  // 0x9c
        public System.Single m_MaximumOrthoSize;  // 0xa0
        private static System.Single kMinimumCameraDistance;  // const
        private static System.Single kMinimumGroupSize;  // const
        private UnityEngine.Vector3 m_PreviousCameraPosition;  // 0xa4
        private Cinemachine.Utility.PositionPredictor m_Predictor;  // 0xb0
        private UnityEngine.Vector3 <TrackedPoint>k__BackingField;  // 0xb8
        private System.Boolean m_InheritingPosition;  // 0xc4
        private System.Single m_prevFOV;  // 0xc8
        private UnityEngine.Quaternion m_prevRotation;  // 0xcc
        private UnityEngine.Bounds <LastBounds>k__BackingField;  // 0xdc
        private UnityEngine.Matrix4x4 <LastBoundsMatrix>k__BackingField;  // 0xf4

        // Properties
        UnityEngine.Rect SoftGuideRect { get; /* RVA: 0x0679F488 */ set; /* RVA: 0x0679F638 */ }
        UnityEngine.Rect HardGuideRect { get; /* RVA: 0x0679F36C */ set; /* RVA: 0x0679F4D0 */ }
        System.Boolean IsValid { get; /* RVA: 0x0679F424 */ }
        Cinemachine.CinemachineCore.Stage Stage { get; /* RVA: 0x011EC580 */ }
        System.Boolean BodyAppliesAfterAim { get; /* RVA: 0x02FFF600 */ }
        UnityEngine.Vector3 TrackedPoint { get; /* RVA: 0x04D87BA0 */ set; /* RVA: 0x04D87C10 */ }
        UnityEngine.Bounds LastBounds { get; /* RVA: 0x04D87B80 */ set; /* RVA: 0x04D87BF0 */ }
        UnityEngine.Matrix4x4 LastBoundsMatrix { get; /* RVA: 0x04D87B50 */ set; /* RVA: 0x04D87BC0 */ }

        // Methods
        // RVA: 0x0679EFCC  token: 0x60001BF
        private System.Void OnValidate() { }
        // RVA: 0x0679ECFC  token: 0x60001C5
        public virtual System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta) { }
        // RVA: 0x04D87B20  token: 0x60001C6
        public virtual System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot) { }
        // RVA: 0x0679D608  token: 0x60001C7
        public virtual System.Single GetMaxDampTime() { }
        // RVA: 0x0679EDFC  token: 0x60001C8
        public virtual System.Boolean OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime, Cinemachine.CinemachineVirtualCameraBase.TransitionParams& transitionParams) { }
        // RVA: 0x0679F19C  token: 0x60001C9
        private UnityEngine.Rect ScreenToOrtho(UnityEngine.Rect rScreen, System.Single orthoSize, System.Single aspect) { }
        // RVA: 0x0679F0B8  token: 0x60001CA
        private UnityEngine.Vector3 OrthoOffsetToScreenBounds(UnityEngine.Vector3 targetPos2D, UnityEngine.Rect screenRect) { }
        // RVA: 0x0679DBC8  token: 0x60001CF
        public virtual System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime) { }
        // RVA: 0x0679DA2C  token: 0x60001D0
        private System.Single GetTargetHeight(UnityEngine.Vector2 boundsSize) { }
        // RVA: 0x0679D17C  token: 0x60001D1
        private UnityEngine.Vector3 ComputeGroupBounds(Cinemachine.ICinemachineTargetGroup group, Cinemachine.CameraState& curState) { }
        // RVA: 0x0679D620  token: 0x60001D2
        private static UnityEngine.Bounds GetScreenSpaceGroupBoundingBox(Cinemachine.ICinemachineTargetGroup group, UnityEngine.Vector3& pos, UnityEngine.Quaternion orientation) { }
        // RVA: 0x0679F270  token: 0x60001D3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000049  // size: 0x1A0
    public class CinemachineGroupComposer : Cinemachine.CinemachineComposer
    {
        // Fields
        public System.Single m_GroupFramingSize;  // 0x110
        public Cinemachine.CinemachineGroupComposer.FramingMode m_FramingMode;  // 0x114
        public System.Single m_FrameDamping;  // 0x118
        public Cinemachine.CinemachineGroupComposer.AdjustmentMode m_AdjustmentMode;  // 0x11c
        public System.Single m_MaxDollyIn;  // 0x120
        public System.Single m_MaxDollyOut;  // 0x124
        public System.Single m_MinimumDistance;  // 0x128
        public System.Single m_MaximumDistance;  // 0x12c
        public System.Single m_MinimumFOV;  // 0x130
        public System.Single m_MaximumFOV;  // 0x134
        public System.Single m_MinimumOrthoSize;  // 0x138
        public System.Single m_MaximumOrthoSize;  // 0x13c
        private System.Single m_prevFramingDistance;  // 0x140
        private System.Single m_prevFOV;  // 0x144
        private UnityEngine.Bounds <LastBounds>k__BackingField;  // 0x148
        private UnityEngine.Matrix4x4 <LastBoundsMatrix>k__BackingField;  // 0x160

        // Properties
        UnityEngine.Bounds LastBounds { get; /* RVA: 0x04D87C60 */ set; /* RVA: 0x04D87CB0 */ }
        UnityEngine.Matrix4x4 LastBoundsMatrix { get; /* RVA: 0x04D87C30 */ set; /* RVA: 0x04D87C80 */ }

        // Methods
        // RVA: 0x067A06F0  token: 0x60001D4
        private System.Void OnValidate() { }
        // RVA: 0x0679F6F8  token: 0x60001D9
        public virtual System.Single GetMaxDampTime() { }
        // RVA: 0x0679FB74  token: 0x60001DA
        public virtual System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime) { }
        // RVA: 0x0679F99C  token: 0x60001DB
        private System.Single GetTargetHeight(UnityEngine.Vector2 boundsSize) { }
        // RVA: 0x0679F710  token: 0x60001DC
        private static UnityEngine.Bounds GetScreenSpaceGroupBoundingBox(Cinemachine.ICinemachineTargetGroup group, UnityEngine.Matrix4x4 observer, UnityEngine.Vector3& newFwd) { }
        // RVA: 0x067A07C0  token: 0x60001DD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004C  // size: 0x30
    public class CinemachineHardLockToTarget : Cinemachine.CinemachineComponentBase
    {
        // Fields
        public System.Single m_Damping;  // 0x20
        private UnityEngine.Vector3 m_PreviousTargetPosition;  // 0x24

        // Properties
        System.Boolean IsValid { get; /* RVA: 0x067A0994 */ }
        Cinemachine.CinemachineCore.Stage Stage { get; /* RVA: 0x011EC580 */ }

        // Methods
        // RVA: 0x011EC8A0  token: 0x60001E0
        public virtual System.Single GetMaxDampTime() { }
        // RVA: 0x067A0838  token: 0x60001E1
        public virtual System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime) { }
        // RVA: 0x03695570  token: 0x60001E2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004D  // size: 0x20
    public class CinemachineHardLookAt : Cinemachine.CinemachineComponentBase
    {
        // Properties
        System.Boolean IsValid { get; /* RVA: 0x067A0BC0 */ }
        Cinemachine.CinemachineCore.Stage Stage { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x067A09F8  token: 0x60001E5
        public virtual System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime) { }
        // RVA: 0x05393520  token: 0x60001E6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004E  // size: 0x180
    public class CinemachineOrbitalTransposer : Cinemachine.CinemachineTransposer
    {
        // Fields
        public Cinemachine.CinemachineOrbitalTransposer.Heading m_Heading;  // 0x98
        public Cinemachine.AxisState.Recentering m_RecenterToTargetHeading;  // 0xa4
        public Cinemachine.AxisState m_XAxis;  // 0xc0
        private System.Single m_LegacyRadius;  // 0x130
        private System.Single m_LegacyHeightOffset;  // 0x134
        private System.Single m_LegacyHeadingBias;  // 0x138
        public System.Boolean m_HeadingIsSlave;  // 0x13c
        private Cinemachine.CinemachineOrbitalTransposer.UpdateHeadingDelegate HeadingUpdater;  // 0x140
        private UnityEngine.Vector3 m_LastTargetPosition;  // 0x148
        private Cinemachine.Utility.HeadingTracker mHeadingTracker;  // 0x158
        private UnityEngine.Rigidbody m_TargetRigidBody;  // 0x160
        private UnityEngine.Transform m_PreviousTarget;  // 0x168
        private UnityEngine.Vector3 m_LastCameraPosition;  // 0x170
        private System.Single m_LastHeading;  // 0x17c

        // Properties
        System.Boolean RequiresUserInput { get; /* RVA: 0x02FFF600 */ }

        // Methods
        // RVA: 0x067A1FBC  token: 0x60001E7
        protected virtual System.Void OnValidate() { }
        // RVA: 0x067A2114  token: 0x60001E8
        public System.Single UpdateHeading(System.Single deltaTime, UnityEngine.Vector3 up, Cinemachine.AxisState& axis) { }
        // RVA: 0x067A2154  token: 0x60001E9
        public System.Single UpdateHeading(System.Single deltaTime, UnityEngine.Vector3 up, Cinemachine.AxisState& axis, Cinemachine.AxisState.Recentering& recentering, System.Boolean isLive) { }
        // RVA: 0x067A1C80  token: 0x60001EA
        private System.Void OnEnable() { }
        // RVA: 0x067A2288  token: 0x60001EB
        public System.Void UpdateInputAxisProvider() { }
        // RVA: 0x067A1CC8  token: 0x60001EC
        public virtual System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta) { }
        // RVA: 0x067A0C24  token: 0x60001ED
        public virtual System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot) { }
        // RVA: 0x067A1E08  token: 0x60001EE
        public virtual System.Boolean OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime, Cinemachine.CinemachineVirtualCameraBase.TransitionParams& transitionParams) { }
        // RVA: 0x067A0D64  token: 0x60001EF
        public System.Single GetAxisClosestValue(UnityEngine.Vector3 cameraPos, UnityEngine.Vector3 up) { }
        // RVA: 0x067A15A0  token: 0x60001F0
        public virtual System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime) { }
        // RVA: 0x067A1084  token: 0x60001F1
        public virtual UnityEngine.Vector3 GetTargetCameraPosition(UnityEngine.Vector3 worldUp) { }
        // RVA: 0x067A11E0  token: 0x60001F3
        private System.Single GetTargetHeading(System.Single currentHeading, UnityEngine.Quaternion targetOrientation) { }
        // RVA: 0x067A2348  token: 0x60001F4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000053  // size: 0x148
    public class CinemachinePOV : Cinemachine.CinemachineComponentBase
    {
        // Fields
        public Cinemachine.CinemachinePOV.RecenterTargetMode m_RecenterTarget;  // 0x20
        public Cinemachine.AxisState m_VerticalAxis;  // 0x28
        public Cinemachine.AxisState.Recentering m_VerticalRecentering;  // 0x98
        public Cinemachine.AxisState m_HorizontalAxis;  // 0xb8
        public Cinemachine.AxisState.Recentering m_HorizontalRecentering;  // 0x128
        public System.Boolean m_ApplyBeforeBody;  // 0x144

        // Properties
        System.Boolean IsValid { get; /* RVA: 0x067A3450 */ }
        Cinemachine.CinemachineCore.Stage Stage { get; /* RVA: 0x04D85B80 */ }
        System.Boolean RequiresUserInput { get; /* RVA: 0x02FFF600 */ }

        // Methods
        // RVA: 0x067A2C60  token: 0x60001FD
        private System.Void OnValidate() { }
        // RVA: 0x067A2AB8  token: 0x60001FE
        private System.Void OnEnable() { }
        // RVA: 0x067A30FC  token: 0x60001FF
        public System.Void UpdateInputAxisProvider() { }
        // RVA: 0x041E1670  token: 0x6000200
        public virtual System.Void PrePipelineMutateCameraState(Cinemachine.CameraState& state, System.Single deltaTime) { }
        // RVA: 0x067A2824  token: 0x6000201
        public virtual System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime) { }
        // RVA: 0x067A25F4  token: 0x6000202
        public UnityEngine.Vector2 GetRecenterTarget() { }
        // RVA: 0x04D87CD0  token: 0x6000203
        private static System.Single NormalizeAngle(System.Single angle) { }
        // RVA: 0x067A25D4  token: 0x6000204
        public virtual System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot) { }
        // RVA: 0x067A2AC0  token: 0x6000205
        public virtual System.Boolean OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime, Cinemachine.CinemachineVirtualCameraBase.TransitionParams& transitionParams) { }
        // RVA: 0x067A2CE0  token: 0x6000207
        private System.Void SetAxesForRotation(UnityEngine.Quaternion targetRot) { }
        // RVA: 0x067A31F4  token: 0x6000208
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000055  // size: 0x38
    public class CinemachineSameAsFollowTarget : Cinemachine.CinemachineComponentBase
    {
        // Fields
        public System.Single m_Damping;  // 0x20
        private UnityEngine.Quaternion m_PreviousReferenceOrientation;  // 0x24

        // Properties
        System.Boolean IsValid { get; /* RVA: 0x067AD968 */ }
        Cinemachine.CinemachineCore.Stage Stage { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x011EC8A0  token: 0x600020B
        public virtual System.Single GetMaxDampTime() { }
        // RVA: 0x067AD870  token: 0x600020C
        public virtual System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime) { }
        // RVA: 0x0418E810  token: 0x600020D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000056  // size: 0x88
    public class CinemachineTrackedDolly : Cinemachine.CinemachineComponentBase
    {
        // Fields
        public Cinemachine.CinemachinePathBase m_Path;  // 0x20
        public System.Single m_PathPosition;  // 0x28
        public Cinemachine.CinemachinePathBase.PositionUnits m_PositionUnits;  // 0x2c
        public UnityEngine.Vector3 m_PathOffset;  // 0x30
        public System.Single m_XDamping;  // 0x3c
        public System.Single m_YDamping;  // 0x40
        public System.Single m_ZDamping;  // 0x44
        public Cinemachine.CinemachineTrackedDolly.CameraUpMode m_CameraUp;  // 0x48
        public System.Single m_PitchDamping;  // 0x4c
        public System.Single m_YawDamping;  // 0x50
        public System.Single m_RollDamping;  // 0x54
        public Cinemachine.CinemachineTrackedDolly.AutoDolly m_AutoDolly;  // 0x58
        private System.Single m_PreviousPathPosition;  // 0x68
        private UnityEngine.Quaternion m_PreviousOrientation;  // 0x6c
        private UnityEngine.Vector3 m_PreviousCameraPosition;  // 0x7c

        // Properties
        System.Boolean IsValid { get; /* RVA: 0x067AE4C0 */ }
        Cinemachine.CinemachineCore.Stage Stage { get; /* RVA: 0x011EC580 */ }
        UnityEngine.Vector3 AngularDamping { get; /* RVA: 0x067AE450 */ }

        // Methods
        // RVA: 0x067ADBF8  token: 0x6000210
        public virtual System.Single GetMaxDampTime() { }
        // RVA: 0x067ADC50  token: 0x6000211
        public virtual System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime) { }
        // RVA: 0x067AD9CC  token: 0x6000212
        private UnityEngine.Quaternion GetCameraOrientationAtPathPoint(UnityEngine.Quaternion pathOrientation, UnityEngine.Vector3 up) { }
        // RVA: 0x0418E8C0  token: 0x6000214
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000059  // size: 0x98
    public class CinemachineTransposer : Cinemachine.CinemachineComponentBase
    {
        // Fields
        public Cinemachine.CinemachineTransposer.BindingMode m_BindingMode;  // 0x20
        public UnityEngine.Vector3 m_FollowOffset;  // 0x24
        public System.Single m_XDamping;  // 0x30
        public System.Single m_YDamping;  // 0x34
        public System.Single m_ZDamping;  // 0x38
        public Cinemachine.CinemachineTransposer.AngularDampingMode m_AngularDampingMode;  // 0x3c
        public System.Single m_PitchDamping;  // 0x40
        public System.Single m_YawDamping;  // 0x44
        public System.Single m_RollDamping;  // 0x48
        public System.Single m_AngularDamping;  // 0x4c
        private System.Boolean <HideOffsetInInspector>k__BackingField;  // 0x50
        private UnityEngine.Vector3 m_PreviousTargetPosition;  // 0x54
        private UnityEngine.Quaternion m_PreviousReferenceOrientation;  // 0x60
        private UnityEngine.Quaternion m_targetOrientationOnAssign;  // 0x70
        private UnityEngine.Vector3 m_PreviousOffset;  // 0x80
        private UnityEngine.Transform m_previousTarget;  // 0x90

        // Properties
        System.Boolean HideOffsetInInspector { get; /* RVA: 0x04D86800 */ set; /* RVA: 0x04D86840 */ }
        UnityEngine.Vector3 EffectiveOffset { get; /* RVA: 0x067AFC3C */ }
        System.Boolean IsValid { get; /* RVA: 0x067AFC74 */ }
        Cinemachine.CinemachineCore.Stage Stage { get; /* RVA: 0x011EC580 */ }
        UnityEngine.Vector3 Damping { get; /* RVA: 0x067AFC1C */ }
        UnityEngine.Vector3 AngularDamping { get; /* RVA: 0x067AFBA4 */ }

        // Methods
        // RVA: 0x067AF3E4  token: 0x6000216
        protected virtual System.Void OnValidate() { }
        // RVA: 0x067AE688  token: 0x600021C
        public virtual System.Single GetMaxDampTime() { }
        // RVA: 0x067AF088  token: 0x600021D
        public virtual System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime) { }
        // RVA: 0x067AF324  token: 0x600021E
        public virtual System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta) { }
        // RVA: 0x067AE508  token: 0x600021F
        public virtual System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot) { }
        // RVA: 0x067AEF54  token: 0x6000220
        protected System.Void InitPrevFrameStateInfo(Cinemachine.CameraState& curState, System.Single deltaTime) { }
        // RVA: 0x067AF414  token: 0x6000221
        protected System.Void TrackTarget(System.Single deltaTime, UnityEngine.Vector3 up, UnityEngine.Vector3 desiredCameraOffset, UnityEngine.Vector3& outTargetPosition, UnityEngine.Quaternion& outTargetOrient) { }
        // RVA: 0x067AE708  token: 0x6000222
        protected UnityEngine.Vector3 GetOffsetForMinimumTargetDistance(UnityEngine.Vector3 dampedTargetPos, UnityEngine.Vector3 cameraOffset, UnityEngine.Vector3 cameraFwd, UnityEngine.Vector3 up, UnityEngine.Vector3 actualTargetPos) { }
        // RVA: 0x067AEE40  token: 0x6000225
        public virtual UnityEngine.Vector3 GetTargetCameraPosition(UnityEngine.Vector3 worldUp) { }
        // RVA: 0x067AEAD8  token: 0x6000226
        public UnityEngine.Quaternion GetReferenceOrientation(UnityEngine.Vector3 worldUp) { }
        // RVA: 0x0418E950  token: 0x6000227
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005C  // size: 0x80
    public sealed struct AxisState
    {
        // Fields
        public System.Single Value;  // 0x10
        public Cinemachine.AxisState.SpeedMode m_SpeedMode;  // 0x14
        public System.Single m_MaxSpeed;  // 0x18
        public System.Single m_AccelTime;  // 0x1c
        public System.Single m_DecelTime;  // 0x20
        public System.String m_InputAxisName;  // 0x28
        public System.Single m_InputAxisValue;  // 0x30
        public System.Boolean m_InvertInput;  // 0x34
        public System.Single m_MinValue;  // 0x38
        public System.Single m_MaxValue;  // 0x3c
        public System.Boolean m_Wrap;  // 0x40
        public Cinemachine.AxisState.Recentering m_Recentering;  // 0x44
        private System.Single m_CurrentSpeed;  // 0x60
        private System.Single m_LastUpdateTime;  // 0x64
        private System.Int32 m_LastUpdateFrame;  // 0x68
        private static System.Single Epsilon;  // const
        private Cinemachine.AxisState.IInputAxisProvider m_InputAxisProvider;  // 0x70
        private System.Int32 m_InputAxisIndex;  // 0x78
        private System.Boolean <ValueRangeLocked>k__BackingField;  // 0x7c
        private System.Boolean <HasRecentering>k__BackingField;  // 0x7d

        // Properties
        System.Boolean ValueRangeLocked { set; /* RVA: 0x04D87F30 */ }
        System.Boolean HasRecentering { set; /* RVA: 0x04D87F20 */ }

        // Methods
        // RVA: 0x067AA840  token: 0x6000228
        public System.Void .ctor(System.Single minValue, System.Single maxValue, System.Boolean wrap, System.Boolean rangeLocked, System.Single maxSpeed, System.Single accelTime, System.Single decelTime, System.String name, System.Boolean invert) { }
        // RVA: 0x067AA7F8  token: 0x6000229
        public System.Void Validate() { }
        // RVA: 0x04D87F10  token: 0x600022A
        public System.Void Reset() { }
        // RVA: 0x067AA3EC  token: 0x600022B
        public System.Void SetInputAxisProvider(System.Int32 axis, Cinemachine.AxisState.IInputAxisProvider provider) { }
        // RVA: 0x067AA3FC  token: 0x600022C
        public System.Boolean Update(System.Single deltaTime) { }
        // RVA: 0x067AA124  token: 0x600022D
        private System.Single ClampValue(System.Single v) { }
        // RVA: 0x067AA20C  token: 0x600022E
        private System.Boolean MaxSpeedUpdate(System.Single input, System.Single deltaTime) { }
        // RVA: 0x067AA194  token: 0x600022F
        private System.Single GetMaxSpeed() { }

    }

    // TypeToken: 0x2000060  // size: 0x130
    public sealed struct CameraState
    {
        // Fields
        public Cinemachine.CameraState.HGDOFParams DofParams;  // 0x10
        public Cinemachine.LensSettings Lens;  // 0x30
        public UnityEngine.Vector3 ReferenceUp;  // 0x60
        public UnityEngine.Vector3 ReferenceLookAt;  // 0x6c
        public UnityEngine.Vector3 Pivot;  // 0x78
        public UnityEngine.Vector3 NoOffsetPivot;  // 0x84
        public static UnityEngine.Vector3 kNoPoint;  // static @ 0x0
        public UnityEngine.Vector3 RawPosition;  // 0x90
        public UnityEngine.Quaternion RawOrientation;  // 0x9c
        public UnityEngine.Vector3 PositionDampingBypass;  // 0xac
        public System.Single ShotQuality;  // 0xb8
        public UnityEngine.Vector3 PositionCorrection;  // 0xbc
        public UnityEngine.Quaternion OrientationCorrection;  // 0xc8
        public Cinemachine.CameraState.BlendHintValue BlendHint;  // 0xd8
        private Cinemachine.CameraState.CustomBlendable mCustom0;  // 0xe0
        private Cinemachine.CameraState.CustomBlendable mCustom1;  // 0xf0
        private Cinemachine.CameraState.CustomBlendable mCustom2;  // 0x100
        private Cinemachine.CameraState.CustomBlendable mCustom3;  // 0x110
        private System.Collections.Generic.List<Cinemachine.CameraState.CustomBlendable> m_CustomOverflow;  // 0x120
        private System.Int32 <NumCustomBlendables>k__BackingField;  // 0x128

        // Properties
        System.Boolean HasLookAt { get; /* RVA: 0x05391AA8 */ }
        UnityEngine.Vector3 CorrectedPosition { get; /* RVA: 0x031FF6B0 */ }
        UnityEngine.Quaternion CorrectedOrientation { get; /* RVA: 0x03249990 */ }
        UnityEngine.Vector3 FinalPosition { get; /* RVA: 0x031FF6B0 */ }
        UnityEngine.Quaternion FinalOrientation { get; /* RVA: 0x03249910 */ }
        Cinemachine.CameraState Default { get; /* RVA: 0x0324BFC0 */ }
        System.Int32 NumCustomBlendables { get; /* RVA: 0x04D87FF0 */ set; /* RVA: 0x04D88000 */ }

        // Methods
        // RVA: 0x067ABD30  token: 0x6000240
        public Cinemachine.CameraState.CustomBlendable GetCustomBlendable(System.Int32 index) { }
        // RVA: 0x067ABBAC  token: 0x6000241
        private System.Int32 FindCustomBlendable(UnityEngine.Object custom) { }
        // RVA: 0x067ABA00  token: 0x6000242
        public System.Void AddCustomBlendable(Cinemachine.CameraState.CustomBlendable b) { }
        // RVA: 0x03245B00  token: 0x6000243
        public static Cinemachine.CameraState Lerp(Cinemachine.CameraState& stateA, Cinemachine.CameraState& stateB, System.Single t) { }
        // RVA: 0x067ABE1C  token: 0x6000244
        private static System.Single InterpolateFOV(System.Single fovA, System.Single fovB, System.Single dA, System.Single dB, System.Single t) { }
        // RVA: 0x03246C90  token: 0x6000245
        private static UnityEngine.Vector3 ApplyPosBlendHint(UnityEngine.Vector3 posA, Cinemachine.CameraState.BlendHintValue hintA, UnityEngine.Vector3 posB, Cinemachine.CameraState.BlendHintValue hintB, UnityEngine.Vector3 original, UnityEngine.Vector3 blended) { }
        // RVA: 0x03246C60  token: 0x6000246
        private static UnityEngine.Quaternion ApplyRotBlendHint(UnityEngine.Quaternion rotA, Cinemachine.CameraState.BlendHintValue hintA, UnityEngine.Quaternion rotB, Cinemachine.CameraState.BlendHintValue hintB, UnityEngine.Quaternion original, UnityEngine.Quaternion blended) { }
        // RVA: 0x03245530  token: 0x6000247
        private UnityEngine.Vector3 InterpolatePosition(UnityEngine.Vector3 posA, UnityEngine.Vector3 pivotA, UnityEngine.Vector3 posB, UnityEngine.Vector3 pivotB, System.Single t, System.Int32& positionCylindricalDir) { }
        // RVA: 0x04D59CF0  token: 0x6000248
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000064  // size: 0x398
    public class CinemachineBlend
    {
        // Fields
        private Cinemachine.ICinemachineCamera m_CamA;  // 0x10
        public Cinemachine.ICinemachineCamera CamB;  // 0x18
        public System.Boolean useSimpleInertialization;  // 0x20
        public System.Boolean InitStateGot;  // 0x21
        public Cinemachine.CameraState InitStateA;  // 0x28
        public Cinemachine.CameraState CurrStateA;  // 0x148
        public Cinemachine.CameraState InitStateB;  // 0x268
        public UnityEngine.AnimationCurve BlendCurve;  // 0x388
        public System.Single TimeInBlend;  // 0x390
        public System.Single Duration;  // 0x394

        // Properties
        Cinemachine.ICinemachineCamera CamA { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x033C72F0 */ }
        System.Single BlendWeight { get; /* RVA: 0x067ABF48 */ }
        System.Boolean IsValid { get; /* RVA: 0x067AC190 */ }
        System.Boolean IsComplete { get; /* RVA: 0x067AC168 */ }
        System.String Description { get; /* RVA: 0x067ABFC0 */ }
        Cinemachine.CameraState State { get; /* RVA: 0x033CA470 */ }

        // Methods
        // RVA: 0x04D88010  token: 0x600024C
        public System.Void StartSimpleInertialization() { }
        // RVA: 0x033C9BF0  token: 0x6000251
        public System.Boolean Uses(Cinemachine.ICinemachineCamera cam) { }
        // RVA: 0x03FCFE70  token: 0x6000252
        public System.Void .ctor(Cinemachine.ICinemachineCamera a, Cinemachine.ICinemachineCamera b, UnityEngine.AnimationCurve curve, System.Single duration, System.Single t) { }
        // RVA: 0x033C9F50  token: 0x6000253
        public System.Void UpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime) { }

    }

    // TypeToken: 0x2000065  // size: 0x28
    public sealed struct CinemachineBlendDefinition
    {
        // Fields
        public Cinemachine.CinemachineBlendDefinition.Style m_Style;  // 0x10
        public System.Single m_Time;  // 0x14
        public UnityEngine.AnimationCurve m_CustomCurve;  // 0x18
        private static UnityEngine.AnimationCurve[] sStandardCurves;  // static @ 0x0
        public System.Boolean m_UseSimpleInertialization;  // 0x20

        // Properties
        System.Single BlendTime { get; /* RVA: 0x03245AF0 */ }
        UnityEngine.AnimationCurve BlendCurve { get; /* RVA: 0x03247400 */ }

        // Methods
        // RVA: 0x05396C64  token: 0x6000256
        public System.Void .ctor(Cinemachine.CinemachineBlendDefinition.Style style, System.Single time) { }
        // RVA: 0x046FFE80  token: 0x6000257
        private System.Void CreateStandardCurves() { }

    }

    // TypeToken: 0x2000067  // size: 0x150
    public class StaticPointVirtualCamera : Cinemachine.ICinemachineCamera
    {
        // Fields
        private System.String <Name>k__BackingField;  // 0x10
        private System.Int32 <Priority>k__BackingField;  // 0x18
        private UnityEngine.Transform <LookAt>k__BackingField;  // 0x20
        private UnityEngine.Transform <Follow>k__BackingField;  // 0x28
        private Cinemachine.CameraState <State>k__BackingField;  // 0x30

        // Properties
        System.String Name { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.Int32 Priority { get; /* RVA: 0x011EF5B0 */ }
        UnityEngine.Transform LookAt { get; /* RVA: 0x04D862C0 */ }
        UnityEngine.Transform Follow { get; /* RVA: 0x04D86240 */ }
        Cinemachine.CameraState State { get; /* RVA: 0x04D87F70 */ set; /* RVA: 0x067AB4DC */ }
        UnityEngine.GameObject VirtualCameraGameObject { get; /* RVA: 0x011EC580 */ }
        System.Boolean IsValid { get; /* RVA: 0x02FFF600 */ }
        Cinemachine.ICinemachineCamera ParentCamera { get; /* RVA: 0x011EC580 */ }

        // Methods
        // RVA: 0x067B2390  token: 0x6000259
        public System.Void .ctor(Cinemachine.CameraState& state, System.String name) { }
        // RVA: 0x012081B0  token: 0x6000264
        public virtual System.Boolean IsLiveChild(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly) { }
        // RVA: 0x041E1670  token: 0x6000265
        public virtual System.Void UpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x041E1670  token: 0x6000266
        public virtual System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime) { }

    }

    // TypeToken: 0x2000068  // size: 0x150
    public class BlendSourceVirtualCamera : Cinemachine.ICinemachineCamera
    {
        // Fields
        private Cinemachine.CinemachineBlend <Blend>k__BackingField;  // 0x10
        private System.Int32 <Priority>k__BackingField;  // 0x18
        private UnityEngine.Transform <LookAt>k__BackingField;  // 0x20
        private UnityEngine.Transform <Follow>k__BackingField;  // 0x28
        private Cinemachine.CameraState <State>k__BackingField;  // 0x30

        // Properties
        Cinemachine.CinemachineBlend Blend { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.String Name { get; /* RVA: 0x067AB4B0 */ }
        System.Int32 Priority { get; /* RVA: 0x011EF5B0 */ }
        UnityEngine.Transform LookAt { get; /* RVA: 0x04D862C0 */ }
        UnityEngine.Transform Follow { get; /* RVA: 0x04D86240 */ }
        Cinemachine.CameraState State { get; /* RVA: 0x04D87F70 */ set; /* RVA: 0x067AB4DC */ }
        UnityEngine.GameObject VirtualCameraGameObject { get; /* RVA: 0x011EC580 */ }
        System.Boolean IsValid { get; /* RVA: 0x067AB498 */ }
        Cinemachine.ICinemachineCamera ParentCamera { get; /* RVA: 0x011EC580 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000267
        public System.Void .ctor(Cinemachine.CinemachineBlend blend) { }
        // RVA: 0x067AB3A4  token: 0x6000273
        public virtual System.Boolean IsLiveChild(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly) { }
        // RVA: 0x067AB3C4  token: 0x6000274
        public virtual System.Void UpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x041E1670  token: 0x6000275
        public virtual System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime) { }

    }

    // TypeToken: 0x2000069  // size: 0x28
    public sealed class CinemachineBlenderSettings : UnityEngine.ScriptableObject
    {
        // Fields
        public Cinemachine.CinemachineBlenderSettings.CustomBlend[] m_CustomBlends;  // 0x18
        public Cinemachine.CinemachineBlenderSettings.CustomBlendGroup[] m_CustomBlendGroups;  // 0x20
        public static System.String kBlendFromAnyCameraLabel;  // const

        // Methods
        // RVA: 0x040F7B40  token: 0x6000276
        public Cinemachine.CinemachineBlendDefinition GetBlendForVirtualCameras(System.String fromCameraName, System.String toCameraName, Cinemachine.CinemachineBlendDefinition defaultBlend) { }
        // RVA: 0x03573370  token: 0x6000277
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200006C  // size: 0x20
    public abstract class CinemachineComponentBase : UnityEngine.MonoBehaviour
    {
        // Fields
        protected static System.Single Epsilon;  // const
        private Cinemachine.CinemachineVirtualCameraBase m_vcamOwner;  // 0x18

        // Properties
        Cinemachine.CinemachineVirtualCameraBase VirtualCamera { get; /* RVA: 0x031FD750 */ }
        UnityEngine.Transform FollowTarget { get; /* RVA: 0x031FC8C0 */ }
        UnityEngine.Transform LookAtTarget { get; /* RVA: 0x031FD590 */ }
        Cinemachine.ICinemachineTargetGroup AbstractFollowTargetGroup { get; /* RVA: 0x067AC1D8 */ }
        Cinemachine.CinemachineTargetGroup FollowTargetGroup { get; /* RVA: 0x067AC220 */ }
        UnityEngine.Vector3 FollowTargetPosition { get; /* RVA: 0x0324B8D0 */ }
        UnityEngine.Quaternion FollowTargetRotation { get; /* RVA: 0x0324BBA0 */ }
        Cinemachine.ICinemachineTargetGroup AbstractLookAtTargetGroup { get; /* RVA: 0x067AC1FC */ }
        Cinemachine.CinemachineTargetGroup LookAtTargetGroup { get; /* RVA: 0x067AC264 */ }
        UnityEngine.Vector3 LookAtTargetPosition { get; /* RVA: 0x067AC2A8 */ }
        UnityEngine.Quaternion LookAtTargetRotation { get; /* RVA: 0x0324BA40 */ }
        Cinemachine.CameraState VcamState { get; /* RVA: 0x067AC430 */ }
        System.Boolean IsValid { get; /* RVA: -1  // abstract */ }
        Cinemachine.CinemachineCore.Stage Stage { get; /* RVA: -1  // abstract */ }
        System.Boolean BodyAppliesAfterAim { get; /* RVA: 0x012081B0 */ }
        System.Boolean RequiresUserInput { get; /* RVA: 0x012081B0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000286
        public virtual System.Void PrePipelineMutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime) { }
        // RVA: -1  // abstract  token: 0x6000289
        public virtual System.Void MutateCameraState(Cinemachine.CameraState& curState, System.Single deltaTime) { }
        // RVA: 0x012081B0  token: 0x600028A
        public virtual System.Boolean OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime, Cinemachine.CinemachineVirtualCameraBase.TransitionParams& transitionParams) { }
        // RVA: 0x041E1670  token: 0x600028B
        public virtual System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta) { }
        // RVA: 0x041E1670  token: 0x600028C
        public virtual System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot) { }
        // RVA: 0x04D879D0  token: 0x600028D
        public virtual System.Single GetMaxDampTime() { }
        // RVA: 0x05393520  token: 0x600028F
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200006D  // size: 0x48
    public sealed class CinemachineCore
    {
        // Fields
        public static readonly System.Int32 kStreamingVersion;  // static @ 0x0
        public static readonly System.String kVersionString;  // static @ 0x8
        private static Cinemachine.CinemachineCore sInstance;  // static @ 0x10
        public static System.Boolean sShowHiddenObjects;  // static @ 0x18
        public static Cinemachine.CinemachineCore.AxisInputDelegate GetInputAxis;  // static @ 0x20
        public static System.Single UniformDeltaTimeOverride;  // static @ 0x28
        public static System.Single CurrentTimeOverride;  // static @ 0x2c
        public static Cinemachine.CinemachineCore.GetBlendOverrideDelegate GetBlendOverride;  // static @ 0x30
        public static Cinemachine.CinemachineBrain.BrainEvent CameraUpdatedEvent;  // static @ 0x38
        public static Cinemachine.CinemachineBrain.BrainEvent CameraCutEvent;  // static @ 0x40
        private System.Collections.Generic.List<Cinemachine.CinemachineBrain> mActiveBrains;  // 0x10
        private System.Collections.Generic.List<Cinemachine.CinemachineVirtualCameraBase> mActiveCameras;  // 0x18
        private System.Boolean m_ActiveCamerasAreSorted;  // 0x20
        private System.Int32 m_ActivationSequence;  // 0x24
        private System.Collections.Generic.List<System.Collections.Generic.List<Cinemachine.CinemachineVirtualCameraBase>> mAllCameras;  // 0x28
        private Cinemachine.CinemachineVirtualCameraBase mRoundRobinVcamLastFrame;  // 0x30
        private static System.Single s_LastUpdateTime;  // static @ 0x48
        private static System.Int32 s_FixedFrameCount;  // static @ 0x4c
        private System.Collections.Generic.Dictionary<Cinemachine.CinemachineVirtualCameraBase,Cinemachine.CinemachineCore.UpdateStatus> mUpdateStatus;  // 0x38
        private Cinemachine.CinemachineCore.UpdateFilter m_CurrentUpdateFilter;  // 0x40

        // Properties
        Cinemachine.CinemachineCore Instance { get; /* RVA: 0x033C8EA0 */ }
        System.Single DeltaTime { get; /* RVA: 0x067ACB74 */ }
        System.Single CurrentTime { get; /* RVA: 0x033C7B20 */ }
        System.Int32 BrainCount { get; /* RVA: 0x03140AD0 */ }
        System.Int32 VirtualCameraCount { get; /* RVA: 0x033C9030 */ }

        // Methods
        // RVA: 0x033C91D0  token: 0x6000294
        public Cinemachine.CinemachineBrain GetActiveBrain(System.Int32 index) { }
        // RVA: 0x03FCF5F0  token: 0x6000295
        private System.Void AddActiveBrain(Cinemachine.CinemachineBrain brain) { }
        // RVA: 0x03FCF660  token: 0x6000296
        private System.Void RemoveActiveBrain(Cinemachine.CinemachineBrain brain) { }
        // RVA: 0x033C8A80  token: 0x6000298
        public Cinemachine.CinemachineVirtualCameraBase GetVirtualCamera(System.Int32 index) { }
        // RVA: 0x0413BBA0  token: 0x6000299
        private System.Void AddActiveCamera(Cinemachine.CinemachineVirtualCameraBase vcam) { }
        // RVA: 0x0413BB40  token: 0x600029A
        private System.Void RemoveActiveCamera(Cinemachine.CinemachineVirtualCameraBase vcam) { }
        // RVA: 0x067AC598  token: 0x600029B
        private System.Void CameraDestroyed(Cinemachine.CinemachineVirtualCameraBase vcam) { }
        // RVA: 0x0413BBE0  token: 0x600029C
        private System.Void CameraEnabled(Cinemachine.CinemachineVirtualCameraBase vcam) { }
        // RVA: 0x0413B8D0  token: 0x600029D
        private System.Void CameraDisabled(Cinemachine.CinemachineVirtualCameraBase vcam) { }
        // RVA: 0x033C7BC0  token: 0x600029E
        private System.Void UpdateAllActiveVirtualCameras(System.Int32 layerMask, UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x033C9C60  token: 0x600029F
        private System.Void UpdateVirtualCamera(Cinemachine.CinemachineVirtualCameraBase vcam, UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x04CE97C0  token: 0x60002A0
        private static System.Void InitializeModule() { }
        // RVA: 0x067AC8AC  token: 0x60002A1
        private static UnityEngine.Transform GetUpdateTarget(Cinemachine.CinemachineVirtualCameraBase vcam) { }
        // RVA: 0x067AC9A4  token: 0x60002A2
        private Cinemachine.UpdateTracker.UpdateClock GetVcamUpdateStatus(Cinemachine.CinemachineVirtualCameraBase vcam) { }
        // RVA: 0x033C9050  token: 0x60002A3
        public System.Boolean IsLive(Cinemachine.ICinemachineCamera vcam) { }
        // RVA: 0x067ACA20  token: 0x60002A4
        public System.Boolean IsLiveInBlend(Cinemachine.ICinemachineCamera vcam) { }
        // RVA: 0x067AC664  token: 0x60002A5
        public System.Void GenerateCameraActivationEvent(Cinemachine.ICinemachineCamera vcam, Cinemachine.ICinemachineCamera vcamFrom) { }
        // RVA: 0x067AC75C  token: 0x60002A6
        public System.Void GenerateCameraCutEvent(Cinemachine.ICinemachineCamera vcam) { }
        // RVA: 0x033C8210  token: 0x60002A7
        public Cinemachine.CinemachineBrain FindPotentialTargetBrain(Cinemachine.CinemachineVirtualCameraBase vcam) { }
        // RVA: 0x067ACAE0  token: 0x60002A8
        public System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta) { }
        // RVA: 0x0413B2C0  token: 0x60002A9
        public System.Void .ctor() { }
        // RVA: 0x03FCFB10  token: 0x60002AA
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000074  // size: 0x28
    public abstract class CinemachineExtension : UnityEngine.MonoBehaviour
    {
        // Fields
        protected static System.Single Epsilon;  // const
        private Cinemachine.CinemachineVirtualCameraBase m_vcamOwner;  // 0x18
        private System.Collections.Generic.Dictionary<Cinemachine.ICinemachineCamera,System.Object> mExtraState;  // 0x20

        // Properties
        Cinemachine.CinemachineVirtualCameraBase VirtualCamera { get; /* RVA: 0x03FD98F0 */ }
        System.Boolean RequiresUserInput { get; /* RVA: 0x012081B0 */ }

        // Methods
        // RVA: 0x04CAE4D0  token: 0x60002B4
        protected virtual System.Void Awake() { }
        // RVA: 0x041E1670  token: 0x60002B5
        protected virtual System.Void OnEnable() { }
        // RVA: 0x067ACBE4  token: 0x60002B6
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x04CAE4D0  token: 0x60002B7
        private System.Void EnsureStarted() { }
        // RVA: 0x03FD9750  token: 0x60002B8
        protected virtual System.Void ConnectToVcam(System.Boolean connect) { }
        // RVA: 0x041E1670  token: 0x60002B9
        public virtual System.Void PrePipelineMutateCameraStateCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CameraState& curState, System.Single deltaTime) { }
        // RVA: 0x05391850  token: 0x60002BA
        public System.Void InvokePostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime) { }
        // RVA: -1  // abstract  token: 0x60002BB
        protected virtual System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime) { }
        // RVA: 0x041E1670  token: 0x60002BC
        public virtual System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta) { }
        // RVA: 0x041E1670  token: 0x60002BD
        public virtual System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot) { }
        // RVA: 0x012081B0  token: 0x60002BE
        public virtual System.Boolean OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x04D879D0  token: 0x60002BF
        public virtual System.Single GetMaxDampTime() { }
        // RVA: -1  // generic def  token: 0x60002C1
        protected T GetExtraState(Cinemachine.ICinemachineCamera vcam) { }
        // RVA: -1  // generic def  token: 0x60002C2
        protected System.Collections.Generic.List<T> GetAllExtraStates() { }
        // RVA: 0x05393520  token: 0x60002C3
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000075  // size: 0x48
    public abstract class CinemachinePathBase : UnityEngine.MonoBehaviour
    {
        // Fields
        public System.Int32 m_Resolution;  // 0x18
        public Cinemachine.CinemachinePathBase.Appearance m_Appearance;  // 0x20
        private System.Single[] m_DistanceToPos;  // 0x28
        private System.Single[] m_PosToDistance;  // 0x30
        private System.Int32 m_CachedSampleSteps;  // 0x38
        private System.Single m_PathLength;  // 0x3c
        private System.Single m_cachedPosStepSize;  // 0x40
        private System.Single m_cachedDistanceStepSize;  // 0x44

        // Properties
        System.Single MinPos { get; /* RVA: -1  // abstract */ }
        System.Single MaxPos { get; /* RVA: -1  // abstract */ }
        System.Boolean Looped { get; /* RVA: -1  // abstract */ }
        System.Int32 DistanceCacheSampleStepsPerSegment { get; /* RVA: -1  // abstract */ }
        System.Single PathLength { get; /* RVA: 0x067AD81C */ }

        // Methods
        // RVA: 0x067AD588  token: 0x60002C7
        public virtual System.Single StandardizePos(System.Single pos) { }
        // RVA: -1  // abstract  token: 0x60002C8
        public virtual UnityEngine.Vector3 EvaluatePosition(System.Single pos) { }
        // RVA: -1  // abstract  token: 0x60002C9
        public virtual UnityEngine.Vector3 EvaluateTangent(System.Single pos) { }
        // RVA: -1  // abstract  token: 0x60002CA
        public virtual UnityEngine.Quaternion EvaluateOrientation(System.Single pos) { }
        // RVA: 0x067ACD60  token: 0x60002CB
        public virtual System.Single FindClosestPoint(UnityEngine.Vector3 p, System.Int32 startSegment, System.Int32 searchRadius, System.Int32 stepsPerSegment) { }
        // RVA: 0x067AD1D8  token: 0x60002CC
        public System.Single MinUnit(Cinemachine.CinemachinePathBase.PositionUnits units) { }
        // RVA: 0x067AD1A0  token: 0x60002CD
        public System.Single MaxUnit(Cinemachine.CinemachinePathBase.PositionUnits units) { }
        // RVA: 0x067AD62C  token: 0x60002CE
        public virtual System.Single StandardizeUnit(System.Single pos, Cinemachine.CinemachinePathBase.PositionUnits units) { }
        // RVA: 0x067ACCB8  token: 0x60002CF
        public UnityEngine.Vector3 EvaluatePositionAtUnit(System.Single pos, Cinemachine.CinemachinePathBase.PositionUnits units) { }
        // RVA: 0x067ACD0C  token: 0x60002D0
        public UnityEngine.Vector3 EvaluateTangentAtUnit(System.Single pos, Cinemachine.CinemachinePathBase.PositionUnits units) { }
        // RVA: 0x067ACC6C  token: 0x60002D1
        public UnityEngine.Quaternion EvaluateOrientationAtUnit(System.Single pos, Cinemachine.CinemachinePathBase.PositionUnits units) { }
        // RVA: 0x067AD170  token: 0x60002D3
        public virtual System.Void InvalidateDistanceCache() { }
        // RVA: 0x067ACBF4  token: 0x60002D4
        public System.Boolean DistanceCacheIsValid() { }
        // RVA: 0x067AD4F4  token: 0x60002D6
        public System.Single StandardizePathDistance(System.Single distance) { }
        // RVA: 0x067AD6A4  token: 0x60002D7
        public System.Single ToNativePathUnits(System.Single pos, Cinemachine.CinemachinePathBase.PositionUnits units) { }
        // RVA: 0x067AD074  token: 0x60002D8
        public System.Single FromPathNativeUnits(System.Single pos, Cinemachine.CinemachinePathBase.PositionUnits units) { }
        // RVA: 0x067AD200  token: 0x60002D9
        private System.Void ResamplePath(System.Int32 stepsPerSegment) { }
        // RVA: 0x067AD7C0  token: 0x60002DA
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000078  // size: 0x10
    public sealed class AxisStatePropertyAttribute : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60002DC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000079  // size: 0x10
    public sealed class OrbitalTransposerHeadingPropertyAttribute : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60002DD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007A  // size: 0x10
    public sealed class VcamTargetPropertyAttribute : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60002DE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007B  // size: 0x10
    public sealed class CinemachineBlendDefinitionPropertyAttribute : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60002DF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007C  // size: 0x10
    public sealed class SaveDuringPlayAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60002E0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007D  // size: 0x10
    public sealed class NoSaveDuringPlayAttribute : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60002E1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007E  // size: 0x10
    public sealed class TagFieldAttribute : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60002E2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007F  // size: 0x10
    public sealed class NoiseSettingsPropertyAttribute : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60002E3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000080  // size: 0x18
    public sealed class CinemachineEmbeddedAssetPropertyAttribute : UnityEngine.PropertyAttribute
    {
        // Fields
        public System.Boolean WarnIfNull;  // 0x10

        // Methods
        // RVA: 0x04D86130  token: 0x60002E4
        public System.Void .ctor(System.Boolean warnIfNull) { }

    }

    // TypeToken: 0x2000081  // size: 0x18
    public sealed class DocumentationSortingAttribute : System.Attribute
    {
        // Fields
        private Cinemachine.DocumentationSortingAttribute.Level <Category>k__BackingField;  // 0x10

        // Properties
        Cinemachine.DocumentationSortingAttribute.Level Category { set; /* RVA: 0x014F51F0 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60002E6
        public System.Void .ctor(Cinemachine.DocumentationSortingAttribute.Level category) { }

    }

    // TypeToken: 0x2000083  // size: 0xA8
    public abstract class CinemachineVirtualCameraBase : UnityEngine.MonoBehaviour, Cinemachine.ICinemachineCamera
    {
        // Fields
        public System.String[] m_ExcludedPropertiesInInspector;  // 0x18
        public Cinemachine.CinemachineCore.Stage[] m_LockStageInInspector;  // 0x20
        private System.Int32 m_ValidatingStreamVersion;  // 0x28
        private System.Boolean m_OnValidateCalled;  // 0x2c
        private System.Int32 m_StreamingVersion;  // 0x30
        public System.Int32 m_Priority;  // 0x34
        private System.Int32 m_ActivationId;  // 0x38
        public System.Single FollowTargetAttachment;  // 0x3c
        public System.Single LookAtTargetAttachment;  // 0x40
        protected System.Boolean _ignoreLookAtBlend;  // 0x44
        public Cinemachine.CinemachineVirtualCameraBase.StandbyUpdateMode m_StandbyUpdate;  // 0x48
        private System.Collections.Generic.List<Cinemachine.CinemachineExtension> <mExtensions>k__BackingField;  // 0x50
        private System.Boolean <PreviousStateIsValid>k__BackingField;  // 0x58
        private System.Boolean m_WasStarted;  // 0x59
        private System.Boolean mSlaveStatusUpdated;  // 0x5a
        private Cinemachine.CinemachineVirtualCameraBase m_parentVcam;  // 0x60
        private System.Int32 m_QueuePriority;  // 0x68
        private UnityEngine.Transform m_CachedFollowTarget;  // 0x70
        private Cinemachine.CinemachineVirtualCameraBase m_CachedFollowTargetVcam;  // 0x78
        private Cinemachine.ICinemachineTargetGroup m_CachedFollowTargetGroup;  // 0x80
        private UnityEngine.Transform m_CachedLookAtTarget;  // 0x88
        private Cinemachine.CinemachineVirtualCameraBase m_CachedLookAtTargetVcam;  // 0x90
        private Cinemachine.ICinemachineTargetGroup m_CachedLookAtTargetGroup;  // 0x98
        private System.Boolean <FollowTargetChanged>k__BackingField;  // 0xa0
        private System.Boolean <LookAtTargetChanged>k__BackingField;  // 0xa1

        // Properties
        System.Int32 ValidatingStreamVersion { get; /* RVA: 0x0413B590 */ set; /* RVA: 0x011F2AA0 */ }
        System.Collections.Generic.List<Cinemachine.CinemachineExtension> mExtensions { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x02FDAEA0 */ }
        System.String Name { get; /* RVA: 0x05396F58 */ }
        System.String Description { get; /* RVA: 0x067B05F8 */ }
        System.Int32 Priority { get; /* RVA: 0x04D86730 */ set; /* RVA: 0x04D86760 */ }
        UnityEngine.GameObject VirtualCameraGameObject { get; /* RVA: 0x033CA6B0 */ }
        System.Boolean IsValid { get; /* RVA: 0x033C9B80 */ }
        Cinemachine.CameraState State { get; /* RVA: -1  // abstract */ }
        Cinemachine.ICinemachineCamera ParentCamera { get; /* RVA: 0x031FE360 */ }
        UnityEngine.Transform LookAt { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        UnityEngine.Transform Follow { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Boolean PreviousStateIsValid { get; /* RVA: 0x04D86F70 */ set; /* RVA: 0x04D86F80 */ }
        System.Boolean FollowTargetChanged { get; /* RVA: 0x04D88020 */ set; /* RVA: 0x04D88040 */ }
        System.Boolean LookAtTargetChanged { get; /* RVA: 0x04D88030 */ set; /* RVA: 0x04D88050 */ }
        Cinemachine.ICinemachineTargetGroup AbstractFollowTargetGroup { get; /* RVA: 0x04D86260 */ }
        Cinemachine.CinemachineVirtualCameraBase FollowTargetAsVcam { get; /* RVA: 0x04D862A0 */ }
        Cinemachine.ICinemachineTargetGroup AbstractLookAtTargetGroup { get; /* RVA: 0x04D85EE0 */ }
        Cinemachine.CinemachineVirtualCameraBase LookAtTargetAsVcam { get; /* RVA: 0x04D86220 */ }

        // Methods
        // RVA: 0x067B02C4  token: 0x60002E9
        public virtual System.Single GetMaxDampTime() { }
        // RVA: 0x067AFF74  token: 0x60002EA
        public System.Single DetachedFollowTargetDamp(System.Single initial, System.Single dampTime, System.Single deltaTime) { }
        // RVA: 0x031FA6A0  token: 0x60002EB
        public UnityEngine.Vector3 DetachedFollowTargetDamp(UnityEngine.Vector3 initial, UnityEngine.Vector3 dampTime, System.Single deltaTime) { }
        // RVA: 0x067AFEF4  token: 0x60002EC
        public UnityEngine.Vector3 DetachedFollowTargetDamp(UnityEngine.Vector3 initial, System.Single dampTime, System.Single deltaTime) { }
        // RVA: 0x031FAEE0  token: 0x60002ED
        public System.Single DetachedLookAtTargetDamp(System.Single initial, System.Single dampTime, System.Single deltaTime) { }
        // RVA: 0x067AFFCC  token: 0x60002EE
        public UnityEngine.Vector3 DetachedLookAtTargetDamp(UnityEngine.Vector3 initial, UnityEngine.Vector3 dampTime, System.Single deltaTime) { }
        // RVA: 0x067B00BC  token: 0x60002EF
        public UnityEngine.Vector3 DetachedLookAtTargetDamp(UnityEngine.Vector3 initial, System.Single dampTime, System.Single deltaTime) { }
        // RVA: 0x03FD9990  token: 0x60002F0
        public virtual System.Void AddExtension(Cinemachine.CinemachineExtension extension) { }
        // RVA: 0x067B048C  token: 0x60002F1
        public virtual System.Void RemoveExtension(Cinemachine.CinemachineExtension extension) { }
        // RVA: 0x031FE0B0  token: 0x60002F4
        protected System.Void InvokePostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& newState, System.Single deltaTime) { }
        // RVA: 0x031FC9B0  token: 0x60002F5
        protected System.Void InvokePrePipelineMutateCameraStateCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CameraState& newState, System.Single deltaTime) { }
        // RVA: 0x03E697F0  token: 0x60002F6
        protected System.Boolean InvokeOnTransitionInExtensions(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x0391CE50  token: 0x60002FB
        protected System.Void ApplyPositionBlendMethod(Cinemachine.CameraState& state, Cinemachine.CinemachineVirtualCameraBase.BlendHint hint) { }
        // RVA: 0x012081B0  token: 0x6000300
        public virtual System.Boolean IsLiveChild(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly) { }
        // RVA: 0x033C8190  token: 0x6000307
        public virtual System.Void UpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: -1  // abstract  token: 0x6000308
        public virtual System.Void InternalUpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x03E697B0  token: 0x6000309
        public virtual System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: 0x067B0368  token: 0x600030A
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x0413B830  token: 0x600030B
        protected virtual System.Void OnTransformParentChanged() { }
        // RVA: 0x04D84E30  token: 0x600030C
        protected virtual System.Void Start() { }
        // RVA: 0x067B04E0  token: 0x600030D
        private virtual System.Boolean RequiresUserInput() { }
        // RVA: 0x067B013C  token: 0x600030E
        private System.Void EnsureStarted() { }
        // RVA: 0x067B0238  token: 0x600030F
        public Cinemachine.AxisState.IInputAxisProvider GetInputAxisProvider() { }
        // RVA: 0x067B0438  token: 0x6000310
        protected virtual System.Void OnValidate() { }
        // RVA: 0x0413B650  token: 0x6000311
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0413B600  token: 0x6000312
        protected virtual System.Void OnDisable() { }
        // RVA: 0x041190F0  token: 0x6000313
        protected virtual System.Void Update() { }
        // RVA: 0x0413B970  token: 0x6000314
        private System.Void UpdateSlaveStatus() { }
        // RVA: 0x031FD690  token: 0x6000315
        public UnityEngine.Transform ResolveLookAt(UnityEngine.Transform localLookAt) { }
        // RVA: 0x031FE3B0  token: 0x6000316
        public UnityEngine.Transform ResolveFollow(UnityEngine.Transform localFollow) { }
        // RVA: 0x0413BA30  token: 0x6000317
        private System.Void UpdateVcamPoolStatus() { }
        // RVA: 0x067B0360  token: 0x6000318
        public System.Void MoveToTopOfPrioritySubqueue() { }
        // RVA: 0x067B03C0  token: 0x6000319
        public virtual System.Void OnTargetObjectWarped(UnityEngine.Transform target, UnityEngine.Vector3 positionDelta) { }
        // RVA: 0x067B01C0  token: 0x600031A
        public virtual System.Void ForceCameraPosition(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot) { }
        // RVA: 0x067AFCBC  token: 0x600031B
        protected Cinemachine.CinemachineBlend CreateBlend(Cinemachine.ICinemachineCamera camA, Cinemachine.ICinemachineCamera camB, Cinemachine.CinemachineBlendDefinition blendDef, Cinemachine.CinemachineBlend activeBlend) { }
        // RVA: 0x0324BD00  token: 0x600031C
        protected Cinemachine.CameraState PullStateFromVirtualCamera(UnityEngine.Vector3 worldUp, Cinemachine.LensSettings& lens) { }
        // RVA: 0x0413B7B0  token: 0x600031D
        private System.Void InvalidateCachedTargets() { }
        // RVA: 0x031FCBF0  token: 0x6000322
        protected System.Void UpdateTargetCache() { }
        // RVA: -1  // generic def  token: 0x6000327
        public System.Object GetCinemachineComponent() { }
        // RVA: 0x04220570  token: 0x6000328
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000088  // size: 0x98
    public class ConfinerOven
    {
        // Fields
        private System.Single m_MinFrustumHeightWithBones;  // 0x10
        private System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>> m_OriginalPolygon;  // 0x18
        private System.Collections.Generic.List<System.Collections.Generic.List<ClipperLib.IntPoint>> m_Skeleton;  // 0x20
        private static System.Int64 k_FloatToIntScaler;  // const
        private static System.Single k_IntToFloatScaler;  // const
        private static System.Single k_MinStepSize;  // const
        private UnityEngine.Rect m_PolygonRect;  // 0x28
        private Cinemachine.ConfinerOven.AspectStretcher m_AspectStretcher;  // 0x38
        private System.Single m_maxComputationTimeForFullSkeletonBakeInSeconds;  // 0x44
        private Cinemachine.ConfinerOven.BakingState <State>k__BackingField;  // 0x48
        public System.Single m_BakeProgress;  // 0x4c
        private Cinemachine.ConfinerOven.BakingStateCache m_Cache;  // 0x50

        // Properties
        Cinemachine.ConfinerOven.BakingState State { get; /* RVA: 0x04D863D0 */ set; /* RVA: 0x04D86450 */ }

        // Methods
        // RVA: 0x067B1698  token: 0x600032C
        public System.Void .ctor(System.Collections.Generic.List<System.Collections.Generic.List<UnityEngine.Vector2>>& inputPath, System.Single& aspectRatio, System.Single maxFrustumHeight) { }
        // RVA: 0x067B0D70  token: 0x600032D
        public Cinemachine.ConfinerOven.BakedSolution GetBakedSolution(System.Single frustumHeight) { }
        // RVA: 0x067B116C  token: 0x6000330
        private System.Void Initialize(System.Collections.Generic.List<System.Collections.Generic.List<UnityEngine.Vector2>>& inputPath, System.Single& aspectRatio, System.Single maxFrustumHeight) { }
        // RVA: 0x067B0624  token: 0x6000331
        public System.Void BakeConfiner(System.Single maxComputationTimePerFrameInSeconds) { }
        // RVA: 0x067B0FF4  token: 0x6000332
        private static UnityEngine.Rect GetPolygonBoundingBox(System.Collections.Generic.List<System.Collections.Generic.List<UnityEngine.Vector2>>& polygons) { }
        // RVA: 0x067B09E8  token: 0x6000333
        private System.Void ComputeSkeleton(System.Collections.Generic.List<Cinemachine.ConfinerOven.PolygonSolution>& solutions) { }

    }

    // TypeToken: 0x200008E
    public interface ICinemachineCamera
    {
        // Properties
        System.String Name { get; /* RVA: -1  // abstract */ }
        System.Int32 Priority { get; /* RVA: -1  // abstract */ }
        UnityEngine.Transform LookAt { get; /* RVA: -1  // abstract */ }
        UnityEngine.Transform Follow { get; /* RVA: -1  // abstract */ }
        Cinemachine.CameraState State { get; /* RVA: -1  // abstract */ }
        UnityEngine.GameObject VirtualCameraGameObject { get; /* RVA: -1  // abstract */ }
        System.Boolean IsValid { get; /* RVA: -1  // abstract */ }
        Cinemachine.ICinemachineCamera ParentCamera { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x600034C
        public virtual System.Boolean IsLiveChild(Cinemachine.ICinemachineCamera vcam, System.Boolean dominantChildOnly) { }
        // RVA: -1  // abstract  token: 0x600034D
        public virtual System.Void UpdateCameraState(UnityEngine.Vector3 worldUp, System.Single deltaTime) { }
        // RVA: -1  // abstract  token: 0x600034E
        public virtual System.Void OnTransitionFromCamera(Cinemachine.ICinemachineCamera fromCam, UnityEngine.Vector3 worldUp, System.Single deltaTime) { }

    }

    // TypeToken: 0x200008F  // size: 0x40
    public sealed struct LensSettings
    {
        // Fields
        public static Cinemachine.LensSettings Default;  // static @ 0x0
        public System.Single FieldOfView;  // 0x10
        public System.Single OrthographicSize;  // 0x14
        public System.Single NearClipPlane;  // 0x18
        public System.Single FarClipPlane;  // 0x1c
        public System.Single Dutch;  // 0x20
        public Cinemachine.LensSettings.OverrideModes ModeOverride;  // 0x24
        public UnityEngine.Vector2 LensShift;  // 0x28
        public UnityEngine.Camera.GateFitMode GateFit;  // 0x30
        private UnityEngine.Vector2 m_SensorSize;  // 0x34
        private System.Boolean m_OrthoFromCamera;  // 0x3c
        private System.Boolean m_PhysicalFromCamera;  // 0x3d

        // Properties
        System.Boolean Orthographic { get; /* RVA: 0x03249DB0 */ }
        UnityEngine.Vector2 SensorSize { get; /* RVA: 0x04D88060 */ }
        System.Single Aspect { get; /* RVA: 0x03249D50 */ }
        System.Boolean IsPhysicalCamera { get; /* RVA: 0x0324C270 */ }

        // Methods
        // RVA: 0x0391D6D0  token: 0x6000353
        public static Cinemachine.LensSettings FromCamera(UnityEngine.Camera fromCamera) { }
        // RVA: 0x0324C290  token: 0x6000354
        public System.Void SnapshotCameraReadOnlyProperties(UnityEngine.Camera camera) { }
        // RVA: 0x04CB6510  token: 0x6000355
        public System.Void .ctor(System.Single verticalFOV, System.Single orthographicSize, System.Single nearClip, System.Single farClip, System.Single dutch) { }
        // RVA: 0x03245990  token: 0x6000356
        public static Cinemachine.LensSettings Lerp(Cinemachine.LensSettings lensA, Cinemachine.LensSettings lensB, System.Single t) { }
        // RVA: 0x067B192C  token: 0x6000357
        public System.Void Validate() { }
        // RVA: 0x04CB6470  token: 0x6000358
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000091  // size: 0x28
    public sealed class NoiseSettings : Cinemachine.SignalSourceAsset
    {
        // Fields
        public Cinemachine.NoiseSettings.TransformNoiseParams[] PositionNoise;  // 0x18
        public Cinemachine.NoiseSettings.TransformNoiseParams[] OrientationNoise;  // 0x20

        // Properties
        System.Single SignalDuration { get; /* RVA: 0x04D879D0 */ }

        // Methods
        // RVA: 0x03C01150  token: 0x6000359
        public static UnityEngine.Vector3 GetCombinedFilterResults(Cinemachine.NoiseSettings.TransformNoiseParams[] noiseParams, System.Single time, UnityEngine.Vector3 timeOffsets) { }
        // RVA: 0x03C01010  token: 0x600035B
        public virtual System.Void GetSignal(System.Single timeSinceSignalStart, UnityEngine.Vector3& pos, UnityEngine.Quaternion& rot) { }
        // RVA: 0x04955230  token: 0x600035C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000094  // size: 0x10
    public static class RuntimeUtility
    {
        // Fields
        public static Cinemachine.RuntimeUtility.OverlapSphereNonAllocDelegate OverlapSphereNonAlloc;  // static @ 0x0
        public static Cinemachine.RuntimeUtility.RaycastDelegate Raycast;  // static @ 0x8
        public static Cinemachine.RuntimeUtility.RaycastNonAllocDelegate RaycastNonAlloc;  // static @ 0x10
        public static Cinemachine.RuntimeUtility.SphereCastNonAllocDelegate SphereCastNonAlloc;  // static @ 0x18
        public static Cinemachine.RuntimeUtility.ComputePenetrationDelegate ComputePenetration;  // static @ 0x20
        public static System.Int32 ignoreColliderOption;  // static @ 0x28
        private static UnityEngine.RaycastHit[] s_HitBuffer;  // static @ 0x30
        private static System.Int32[] s_PenetrationIndexBuffer;  // static @ 0x38
        private static UnityEngine.SphereCollider s_ScratchCollider;  // static @ 0x40
        private static UnityEngine.GameObject s_ScratchColliderGameObject;  // static @ 0x48

        // Methods
        // RVA: 0x067B1CA4  token: 0x600035F
        public static System.Void DestroyObject(UnityEngine.Object obj) { }
        // RVA: 0x012081B0  token: 0x6000360
        public static System.Boolean IsPrefab(UnityEngine.GameObject gameObject) { }
        // RVA: 0x032489A0  token: 0x6000361
        public static System.Boolean RaycastIgnoreTag(UnityEngine.Ray ray, UnityEngine.RaycastHit& hitInfo, System.Single rayLength, System.Int32 layerMask, System.String& ignoreTag) { }
        // RVA: 0x044C5210  token: 0x6000362
        public static System.Int32 GetColliderOptionsFromHit(UnityEngine.RaycastHit hit) { }
        // RVA: 0x044C5150  token: 0x6000363
        public static System.Boolean HasIgnoreColliderOptionFromHit(UnityEngine.RaycastHit hit) { }
        // RVA: 0x067B2100  token: 0x6000364
        public static System.Boolean HasIgnoreColliderOptionFromCollider(UnityEngine.ECSColliderResultProxy collider) { }
        // RVA: 0x0324A310  token: 0x6000365
        public static System.Boolean SphereCastIgnoreTag(UnityEngine.Vector3 rayStart, System.Single radius, UnityEngine.Vector3 dir, UnityEngine.RaycastHit& hitInfo, System.Single rayLength, System.Int32 layerMask, System.String& ignoreTag) { }
        // RVA: 0x067B1E78  token: 0x6000366
        private static UnityEngine.SphereCollider GetScratchCollider() { }
        // RVA: 0x067B1D04  token: 0x6000367
        private static System.Void DestroyScratchCollider() { }
        // RVA: 0x067B2170  token: 0x6000368
        public static UnityEngine.AnimationCurve NormalizeCurve(UnityEngine.AnimationCurve curve, System.Boolean normalizeX, System.Boolean normalizeY) { }
        // RVA: 0x0454DE60  token: 0x6000369
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200009A
    public interface ISignalSource6D
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000374
        public virtual System.Void GetSignal(System.Single timeSinceSignalStart, UnityEngine.Vector3& pos, UnityEngine.Quaternion& rot) { }

    }

    // TypeToken: 0x200009B  // size: 0x18
    public abstract class SignalSourceAsset : UnityEngine.ScriptableObject, Cinemachine.ISignalSource6D
    {
        // Properties
        System.Single SignalDuration { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000376
        public virtual System.Void GetSignal(System.Single timeSinceSignalStart, UnityEngine.Vector3& pos, UnityEngine.Quaternion& rot) { }
        // RVA: 0x03573370  token: 0x6000377
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200009C  // size: 0x10
    public class TargetPositionCache
    {
        // Fields
        public static System.Boolean UseCache;  // static @ 0x0
        private static Cinemachine.TargetPositionCache.Mode m_CacheMode;  // static @ 0x4
        public static System.Single CurrentTime;  // static @ 0x8
        public static System.Int32 CurrentFrame;  // static @ 0xc
        public static System.Boolean IsCameraCut;  // static @ 0x10
        private static System.Collections.Generic.Dictionary<UnityEngine.Transform,Cinemachine.TargetPositionCache.CacheEntry> m_Cache;  // static @ 0x18
        private static Cinemachine.TargetPositionCache.TimeRange m_CacheTimeRange;  // static @ 0x20

        // Properties
        Cinemachine.TargetPositionCache.Mode CacheMode { get; /* RVA: 0x067B2580 */ }
        System.Boolean HasCurrentTime { get; /* RVA: 0x067B25B8 */ }

        // Methods
        // RVA: 0x067B2488  token: 0x600037A
        public static System.Void ClearCache() { }
        // RVA: 0x0324C1D0  token: 0x600037B
        public static UnityEngine.Vector3 GetTargetPosition(UnityEngine.Transform target) { }
        // RVA: 0x0324C110  token: 0x600037C
        public static UnityEngine.Quaternion GetTargetRotation(UnityEngine.Transform target) { }

    }

    // TypeToken: 0x20000A3  // size: 0x10
    public class UpdateTracker
    {
        // Fields
        private static System.Collections.Generic.Dictionary<UnityEngine.Transform,Cinemachine.UpdateTracker.UpdateStatus> mUpdateStatus;  // static @ 0x0
        private static System.Collections.Generic.List<UnityEngine.Transform> sToDelete;  // static @ 0x8
        private static System.Single mLastUpdateTime;  // static @ 0x10

        // Methods
        // RVA: 0x04D34810  token: 0x6000386
        private static System.Void InitializeModule() { }
        // RVA: 0x067B2A1C  token: 0x6000387
        private static System.Void UpdateTargets(Cinemachine.UpdateTracker.UpdateClock currentClock) { }
        // RVA: 0x067B2790  token: 0x6000388
        public static Cinemachine.UpdateTracker.UpdateClock GetPreferredUpdate(UnityEngine.Transform target) { }
        // RVA: 0x067B2980  token: 0x6000389
        public static System.Void OnUpdate(Cinemachine.UpdateTracker.UpdateClock currentClock) { }
        // RVA: 0x04CA3930  token: 0x600038A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000A6  // size: 0x38
    public class CinemachineInputProvider : UnityEngine.MonoBehaviour, IInputAxisProvider
    {
        // Fields
        public System.Int32 PlayerIndex;  // 0x18
        public System.Boolean AutoEnableInputs;  // 0x1c
        public UnityEngine.InputSystem.InputActionReference XYAxis;  // 0x20
        public UnityEngine.InputSystem.InputActionReference ZAxis;  // 0x28
        private static System.Int32 NUM_AXES;  // const
        private UnityEngine.InputSystem.InputAction[] m_cachedActions;  // 0x30

        // Methods
        // RVA: 0x067B5934  token: 0x600038F
        public virtual System.Single GetAxisValue(System.Int32 axis) { }
        // RVA: 0x067B5A14  token: 0x6000390
        protected UnityEngine.InputSystem.InputAction ResolveForPlayer(System.Int32 axis, UnityEngine.InputSystem.InputActionReference actionRef) { }
        // RVA: 0x067B5A00  token: 0x6000391
        protected virtual System.Void OnDisable() { }
        // RVA: 0x067B5E74  token: 0x6000392
        public System.Void .ctor() { }
        // RVA: 0x067B5D7C  token: 0x6000393
        private static UnityEngine.InputSystem.InputAction <ResolveForPlayer>g__GetFirstMatch|7_0(UnityEngine.InputSystem.Users.InputUser& user, UnityEngine.InputSystem.InputActionReference aRef) { }

    }

    // TypeToken: 0x20000A8  // size: 0x90
    public class CinemachineTriggerAction : UnityEngine.MonoBehaviour
    {
        // Fields
        public UnityEngine.LayerMask m_LayerMask;  // 0x18
        public System.String m_WithTag;  // 0x20
        public System.String m_WithoutTag;  // 0x28
        public System.Int32 m_SkipFirst;  // 0x30
        public System.Boolean m_Repeating;  // 0x34
        public Cinemachine.CinemachineTriggerAction.ActionSettings m_OnObjectEnter;  // 0x38
        public Cinemachine.CinemachineTriggerAction.ActionSettings m_OnObjectExit;  // 0x60
        private System.Collections.Generic.HashSet<UnityEngine.GameObject> m_ActiveTriggerObjects;  // 0x88

        // Methods
        // RVA: 0x067B5E94  token: 0x6000396
        private System.Boolean Filter(UnityEngine.GameObject other) { }
        // RVA: 0x067B5F24  token: 0x6000397
        private System.Void InternalDoTriggerEnter(UnityEngine.GameObject other) { }
        // RVA: 0x067B5FB4  token: 0x6000398
        private System.Void InternalDoTriggerExit(UnityEngine.GameObject other) { }
        // RVA: 0x067B6120  token: 0x6000399
        private System.Void OnTriggerEnter(UnityEngine.Collider other) { }
        // RVA: 0x067B6154  token: 0x600039A
        private System.Void OnTriggerExit(UnityEngine.Collider other) { }
        // RVA: 0x067B6084  token: 0x600039B
        private System.Void OnCollisionEnter(UnityEngine.Collision other) { }
        // RVA: 0x067B60EC  token: 0x600039C
        private System.Void OnCollisionExit(UnityEngine.Collision other) { }
        // RVA: 0x067B6120  token: 0x600039D
        private System.Void OnTriggerEnter2D(UnityEngine.Collider2D other) { }
        // RVA: 0x067B6154  token: 0x600039E
        private System.Void OnTriggerExit2D(UnityEngine.Collider2D other) { }
        // RVA: 0x067B6050  token: 0x600039F
        private System.Void OnCollisionEnter2D(UnityEngine.Collision2D other) { }
        // RVA: 0x067B60B8  token: 0x60003A0
        private System.Void OnCollisionExit2D(UnityEngine.Collision2D other) { }
        // RVA: 0x041E1670  token: 0x60003A1
        private System.Void OnEnable() { }
        // RVA: 0x067B6188  token: 0x60003A2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000AD  // size: 0x40
    public class GroupWeightManipulator : UnityEngine.MonoBehaviour
    {
        // Fields
        public System.Single m_Weight0;  // 0x18
        public System.Single m_Weight1;  // 0x1c
        public System.Single m_Weight2;  // 0x20
        public System.Single m_Weight3;  // 0x24
        public System.Single m_Weight4;  // 0x28
        public System.Single m_Weight5;  // 0x2c
        public System.Single m_Weight6;  // 0x30
        public System.Single m_Weight7;  // 0x34
        private Cinemachine.CinemachineTargetGroup m_group;  // 0x38

        // Methods
        // RVA: 0x067B643C  token: 0x60003A6
        private System.Void Start() { }
        // RVA: 0x067B63D0  token: 0x60003A7
        private System.Void OnValidate() { }
        // RVA: 0x067B65B8  token: 0x60003A8
        private System.Void Update() { }
        // RVA: 0x067B6484  token: 0x60003A9
        private System.Void UpdateWeights() { }
        // RVA: 0x067B6618  token: 0x60003AA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000AE  // size: 0x58
    public class CinemachineCollisionImpulseSource : Cinemachine.CinemachineImpulseSource
    {
        // Fields
        public UnityEngine.LayerMask m_LayerMask;  // 0x30
        public System.String m_IgnoreTag;  // 0x38
        public System.Boolean m_UseImpactDirection;  // 0x40
        public System.Boolean m_ScaleImpactWithMass;  // 0x41
        public System.Boolean m_ScaleImpactWithSpeed;  // 0x42
        private UnityEngine.Rigidbody mRigidBody;  // 0x48
        private UnityEngine.Rigidbody2D mRigidBody2D;  // 0x50

        // Methods
        // RVA: 0x067B3B08  token: 0x60003AB
        private System.Void Start() { }
        // RVA: 0x041E1670  token: 0x60003AC
        private System.Void OnEnable() { }
        // RVA: 0x067B3A78  token: 0x60003AD
        private System.Void OnCollisionEnter(UnityEngine.Collision c) { }
        // RVA: 0x067B3ADC  token: 0x60003AE
        private System.Void OnTriggerEnter(UnityEngine.Collider c) { }
        // RVA: 0x067B37D0  token: 0x60003AF
        private System.Single GetMassAndVelocity(UnityEngine.Collider other, UnityEngine.Vector3& vel) { }
        // RVA: 0x067B33B0  token: 0x60003B0
        private System.Void GenerateImpactEvent(UnityEngine.Collider other, UnityEngine.Vector3 vel) { }
        // RVA: 0x067B3A04  token: 0x60003B1
        private System.Void OnCollisionEnter2D(UnityEngine.Collision2D c) { }
        // RVA: 0x067B3AB0  token: 0x60003B2
        private System.Void OnTriggerEnter2D(UnityEngine.Collider2D c) { }
        // RVA: 0x067B359C  token: 0x60003B3
        private System.Single GetMassAndVelocity2D(UnityEngine.Collider2D other2d, UnityEngine.Vector3& vel) { }
        // RVA: 0x067B31C4  token: 0x60003B4
        private System.Void GenerateImpactEvent2D(UnityEngine.Collider2D other2d, UnityEngine.Vector3 vel) { }
        // RVA: 0x067B3B78  token: 0x60003B5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000AF  // size: 0x30
    public class CinemachineFixedSignal : Cinemachine.SignalSourceAsset
    {
        // Fields
        public UnityEngine.AnimationCurve m_XCurve;  // 0x18
        public UnityEngine.AnimationCurve m_YCurve;  // 0x20
        public UnityEngine.AnimationCurve m_ZCurve;  // 0x28

        // Properties
        System.Single SignalDuration { get; /* RVA: 0x03DF0B90 */ }

        // Methods
        // RVA: 0x03DF0BF0  token: 0x60003B7
        private System.Single AxisDuration(UnityEngine.AnimationCurve axis) { }
        // RVA: 0x043F9E10  token: 0x60003B8
        public virtual System.Void GetSignal(System.Single timeSinceSignalStart, UnityEngine.Vector3& pos, UnityEngine.Quaternion& rot) { }
        // RVA: 0x043F9EB0  token: 0x60003B9
        private System.Single AxisValue(UnityEngine.AnimationCurve axis, System.Single time) { }
        // RVA: 0x03573370  token: 0x60003BA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000B0  // size: 0x80
    public class CinemachineImpulseDefinition
    {
        // Fields
        public System.Int32 m_ImpulseChannel;  // 0x10
        public Cinemachine.CinemachineImpulseDefinition.ImpulseShapes m_ImpulseShape;  // 0x14
        public UnityEngine.AnimationCurve m_CustomImpulseShape;  // 0x18
        public System.Single m_ImpulseDuration;  // 0x20
        public Cinemachine.CinemachineImpulseDefinition.ImpulseTypes m_ImpulseType;  // 0x24
        public System.Single m_DissipationRate;  // 0x28
        public Cinemachine.SignalSourceAsset m_RawSignal;  // 0x30
        public System.Single m_AmplitudeGain;  // 0x38
        public System.Single m_FrequencyGain;  // 0x3c
        public Cinemachine.CinemachineImpulseDefinition.RepeatMode m_RepeatMode;  // 0x40
        public System.Boolean m_Randomize;  // 0x44
        public Cinemachine.CinemachineImpulseManager.EnvelopeDefinition m_TimeEnvelope;  // 0x48
        public System.Single m_ImpactRadius;  // 0x68
        public Cinemachine.CinemachineImpulseManager.ImpulseEvent.DirectionMode m_DirectionMode;  // 0x6c
        public Cinemachine.CinemachineImpulseManager.ImpulseEvent.DissipationMode m_DissipationMode;  // 0x70
        public System.Single m_DissipationDistance;  // 0x74
        public System.Single m_PropagationSpeed;  // 0x78
        private static UnityEngine.AnimationCurve[] sStandardShapes;  // static @ 0x0

        // Properties
        UnityEngine.AnimationCurve ImpulseCurve { get; /* RVA: 0x067B4EAC */ }

        // Methods
        // RVA: 0x067B4DD4  token: 0x60003BB
        public System.Void OnValidate() { }
        // RVA: 0x067B41FC  token: 0x60003BC
        private static System.Void CreateStandardShapes() { }
        // RVA: 0x067B4D60  token: 0x60003BD
        private static UnityEngine.AnimationCurve GetStandardCurve(Cinemachine.CinemachineImpulseDefinition.ImpulseShapes shape) { }
        // RVA: 0x067B41BC  token: 0x60003BF
        public System.Void CreateEvent(UnityEngine.Vector3 position, UnityEngine.Vector3 velocity) { }
        // RVA: 0x033CDDB0  token: 0x60003C0
        public Cinemachine.CinemachineImpulseManager.ImpulseEvent CreateAndReturnEvent(UnityEngine.Vector3 position, UnityEngine.Vector3 velocity) { }
        // RVA: 0x033CD670  token: 0x60003C1
        private Cinemachine.CinemachineImpulseManager.ImpulseEvent LegacyCreateAndReturnEvent(UnityEngine.Vector3 position, UnityEngine.Vector3 velocity) { }
        // RVA: 0x0395F2B0  token: 0x60003C2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000B6  // size: 0x68
    public class CinemachineImpulseListener : Cinemachine.CinemachineExtension
    {
        // Fields
        public Cinemachine.CinemachineCore.Stage m_ApplyAfter;  // 0x28
        public System.Int32 m_ChannelMask;  // 0x2c
        public System.Single m_Gain;  // 0x30
        public System.Boolean m_Use2DDistance;  // 0x34
        public System.Boolean m_UseCameraSpace;  // 0x35
        public Cinemachine.CinemachineImpulseListener.ImpulseReaction m_ReactionSettings;  // 0x38

        // Methods
        // RVA: 0x067B4F04  token: 0x60003C9
        private System.Void Reset() { }
        // RVA: 0x031FE6C0  token: 0x60003CA
        protected virtual System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime) { }
        // RVA: 0x049C8F70  token: 0x60003CB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000B8  // size: 0x10
    public sealed class CinemachineImpulseChannelPropertyAttribute : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60003CE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000B9  // size: 0x28
    public class CinemachineImpulseManager
    {
        // Fields
        private static Cinemachine.CinemachineImpulseManager sInstance;  // static @ 0x0
        private System.Collections.Generic.List<Cinemachine.CinemachineImpulseManager.ImpulseEvent> m_ExpiredEvents;  // 0x10
        private System.Collections.Generic.List<Cinemachine.CinemachineImpulseManager.ImpulseEvent> m_ActiveEvents;  // 0x18
        public System.Boolean IgnoreTimeScale;  // 0x20

        // Properties
        Cinemachine.CinemachineImpulseManager Instance { get; /* RVA: 0x033CD1B0 */ }
        System.Single CurrentTime { get; /* RVA: 0x033CD240 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60003CF
        private System.Void .ctor() { }
        // RVA: 0x04D75DC0  token: 0x60003D1
        private static System.Void InitializeModule() { }
        // RVA: 0x067B5008  token: 0x60003D2
        private static System.Single EvaluateDissipationScale(System.Single spread, System.Single normalizedDistance) { }
        // RVA: 0x033CCE90  token: 0x60003D3
        public System.Boolean GetImpulseAt(UnityEngine.Vector3 listenerLocation, System.Boolean distance2D, System.Int32 channelMask, UnityEngine.Vector3& pos, UnityEngine.Quaternion& rot) { }
        // RVA: 0x033CDA50  token: 0x60003D5
        public Cinemachine.CinemachineImpulseManager.ImpulseEvent NewImpulseEvent() { }
        // RVA: 0x033CDE20  token: 0x60003D6
        public System.Void AddImpulseEvent(Cinemachine.CinemachineImpulseManager.ImpulseEvent e) { }
        // RVA: 0x067B4F68  token: 0x60003D7
        public System.Void Clear() { }

    }

    // TypeToken: 0x20000BE  // size: 0x30
    public class CinemachineImpulseSource : UnityEngine.MonoBehaviour
    {
        // Fields
        public Cinemachine.CinemachineImpulseDefinition m_ImpulseDefinition;  // 0x18
        public UnityEngine.Vector3 m_DefaultVelocity;  // 0x20

        // Methods
        // RVA: 0x067B52A0  token: 0x60003E4
        private System.Void OnValidate() { }
        // RVA: 0x067B52C0  token: 0x60003E5
        private System.Void Reset() { }
        // RVA: 0x067B50A8  token: 0x60003E6
        public System.Void GenerateImpulseAtPositionWithVelocity(UnityEngine.Vector3 position, UnityEngine.Vector3 velocity) { }
        // RVA: 0x067B51D4  token: 0x60003E7
        public System.Void GenerateImpulseWithVelocity(UnityEngine.Vector3 velocity) { }
        // RVA: 0x067B5134  token: 0x60003E8
        public System.Void GenerateImpulseWithForce(System.Single force) { }
        // RVA: 0x067B5248  token: 0x60003E9
        public System.Void GenerateImpulse() { }
        // RVA: 0x067B50F4  token: 0x60003EA
        public System.Void GenerateImpulseAt(UnityEngine.Vector3 position, UnityEngine.Vector3 velocity) { }
        // RVA: 0x067B5278  token: 0x60003EB
        public System.Void GenerateImpulse(UnityEngine.Vector3 velocity) { }
        // RVA: 0x067B5270  token: 0x60003EC
        public System.Void GenerateImpulse(System.Single force) { }
        // RVA: 0x067B53B4  token: 0x60003ED
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BF  // size: 0x70
    public class CinemachineIndependentImpulseListener : UnityEngine.MonoBehaviour
    {
        // Fields
        private UnityEngine.Vector3 impulsePosLastFrame;  // 0x18
        private UnityEngine.Quaternion impulseRotLastFrame;  // 0x24
        public System.Int32 m_ChannelMask;  // 0x34
        public System.Single m_Gain;  // 0x38
        public System.Boolean m_Use2DDistance;  // 0x3c
        public System.Boolean m_UseLocalSpace;  // 0x3d
        public Cinemachine.CinemachineImpulseListener.ImpulseReaction m_ReactionSettings;  // 0x40

        // Methods
        // RVA: 0x067B57A4  token: 0x60003EE
        private System.Void Reset() { }
        // RVA: 0x067B5774  token: 0x60003EF
        private System.Void OnEnable() { }
        // RVA: 0x067B5800  token: 0x60003F0
        private System.Void Update() { }
        // RVA: 0x067B543C  token: 0x60003F1
        private System.Void LateUpdate() { }
        // RVA: 0x05393520  token: 0x60003F2
        public System.Void .ctor() { }

    }

}

namespace Cinemachine.PostFX
{

    // TypeToken: 0x20000C0  // size: 0x28
    public class CinemachinePostProcessing : Cinemachine.CinemachineExtension
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60003F3
        protected virtual System.Void PostPipelineStageCallback(Cinemachine.CinemachineVirtualCameraBase vcam, Cinemachine.CinemachineCore.Stage stage, Cinemachine.CameraState& state, System.Single deltaTime) { }
        // RVA: 0x05393520  token: 0x60003F4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C1  // size: 0x18
    public class CinemachineVolumeSettings : UnityEngine.MonoBehaviour
    {
        // Methods
        // RVA: 0x05393520  token: 0x60003F5
        public System.Void .ctor() { }

    }

}

namespace Cinemachine.Utility
{

    // TypeToken: 0x20000C2  // size: 0x10
    public class CinemachineDebug
    {
        // Fields
        private static System.Collections.Generic.HashSet<UnityEngine.Object> mClients;  // static @ 0x0
        public static Cinemachine.Utility.CinemachineDebug.OnGUIDelegate OnGUIHandlers;  // static @ 0x8
        private static System.Collections.Generic.List<System.Text.StringBuilder> mAvailableStringBuilders;  // static @ 0x10

        // Methods
        // RVA: 0x067B3F4C  token: 0x60003F6
        public static System.Void ReleaseScreenPos(UnityEngine.Object client) { }
        // RVA: 0x067B3BD4  token: 0x60003F7
        public static UnityEngine.Rect GetScreenPos(UnityEngine.Object client, System.String text, UnityEngine.GUIStyle style) { }
        // RVA: 0x067B40C8  token: 0x60003F8
        public static System.Text.StringBuilder SBFromPool() { }
        // RVA: 0x067B3FE4  token: 0x60003F9
        public static System.Void ReturnToPool(System.Text.StringBuilder sb) { }

    }

    // TypeToken: 0x20000C4  // size: 0x40
    public class PositionPredictor
    {
        // Fields
        private UnityEngine.Vector3 m_Velocity;  // 0x10
        private UnityEngine.Vector3 m_SmoothDampVelocity;  // 0x1c
        private UnityEngine.Vector3 m_Pos;  // 0x28
        private System.Boolean m_HavePos;  // 0x34
        public System.Single Smoothing;  // 0x38

        // Methods
        // RVA: 0x067B6F14  token: 0x60003FC
        public System.Void ApplyTransformDelta(UnityEngine.Vector3 positionDelta) { }
        // RVA: 0x067B6FA8  token: 0x60003FD
        public System.Void Reset() { }
        // RVA: 0x067B6D64  token: 0x60003FE
        public System.Void AddPosition(UnityEngine.Vector3 pos, System.Single deltaTime, System.Single lookaheadTime) { }
        // RVA: 0x067B6F68  token: 0x60003FF
        public UnityEngine.Vector3 PredictPositionDelta(System.Single lookaheadTime) { }
        // RVA: 0x041E1670  token: 0x6000400
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C5  // size: 0x10
    public static class Damper
    {
        // Methods
        // RVA: 0x031FBEF0  token: 0x6000401
        public static System.Single Damp(System.Single initial, System.Single dampTime, System.Single deltaTime) { }
        // RVA: 0x031FB600  token: 0x6000402
        public static UnityEngine.Vector3 Damp(UnityEngine.Vector3 initial, UnityEngine.Vector3 dampTime, System.Single deltaTime) { }
        // RVA: 0x067B62F8  token: 0x6000403
        public static UnityEngine.Vector3 Damp(UnityEngine.Vector3 initial, System.Single dampTime, System.Single deltaTime) { }

    }

    // TypeToken: 0x20000C6  // size: 0x48
    public class HeadingTracker
    {
        // Fields
        private Cinemachine.Utility.HeadingTracker.Item[] mHistory;  // 0x10
        private System.Int32 mTop;  // 0x18
        private System.Int32 mBottom;  // 0x1c
        private System.Int32 mCount;  // 0x20
        private UnityEngine.Vector3 mHeadingSum;  // 0x24
        private System.Single mWeightSum;  // 0x30
        private System.Single mWeightTime;  // 0x34
        private UnityEngine.Vector3 mLastGoodHeading;  // 0x38
        private static System.Single mDecayExponent;  // static @ 0x0

        // Properties
        System.Int32 FilterSize { get; /* RVA: 0x067B6CAC */ }

        // Methods
        // RVA: 0x067B6BD0  token: 0x6000404
        public System.Void .ctor(System.Int32 filterSize) { }
        // RVA: 0x067B6824  token: 0x6000406
        private System.Void ClearHistory() { }
        // RVA: 0x067B6904  token: 0x6000407
        private static System.Single Decay(System.Single time) { }
        // RVA: 0x067B6668  token: 0x6000408
        public System.Void Add(UnityEngine.Vector3 velocity) { }
        // RVA: 0x067B6A44  token: 0x6000409
        private System.Void PopBottom() { }
        // RVA: 0x067B6844  token: 0x600040A
        public System.Void DecayHistory() { }
        // RVA: 0x067B6950  token: 0x600040B
        public UnityEngine.Vector3 GetReliableHeading() { }

    }

    // TypeToken: 0x20000C8  // size: 0x10
    public static class SplineHelpers
    {
        // Methods
        // RVA: 0x067B7184  token: 0x600040C
        public static UnityEngine.Vector3 Bezier3(System.Single t, UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, UnityEngine.Vector3 p2, UnityEngine.Vector3 p3) { }
        // RVA: 0x067B7310  token: 0x600040D
        public static UnityEngine.Vector3 BezierTangent3(System.Single t, UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, UnityEngine.Vector3 p2, UnityEngine.Vector3 p3) { }
        // RVA: 0x067B70F8  token: 0x600040E
        public static System.Single Bezier1(System.Single t, System.Single p0, System.Single p1, System.Single p2, System.Single p3) { }
        // RVA: 0x067B7924  token: 0x600040F
        public static System.Void ComputeSmoothControlPoints(UnityEngine.Vector4[]& knot, UnityEngine.Vector4[]& ctrl1, UnityEngine.Vector4[]& ctrl2) { }
        // RVA: 0x067B7668  token: 0x6000410
        public static System.Void ComputeSmoothControlPointsLooped(UnityEngine.Vector4[]& knot, UnityEngine.Vector4[]& ctrl1, UnityEngine.Vector4[]& ctrl2) { }

    }

    // TypeToken: 0x20000C9  // size: 0x10
    public static class UnityVectorExtensions
    {
        // Methods
        // RVA: 0x0364D1E0  token: 0x6000411
        public static System.Boolean IsNaN(UnityEngine.Vector3 v) { }
        // RVA: 0x067B848C  token: 0x6000412
        public static System.Single ClosestPointOnSegment(UnityEngine.Vector3 p, UnityEngine.Vector3 s0, UnityEngine.Vector3 s1) { }
        // RVA: 0x067B83F4  token: 0x6000413
        public static System.Single ClosestPointOnSegment(UnityEngine.Vector2 p, UnityEngine.Vector2 s0, UnityEngine.Vector2 s1) { }
        // RVA: 0x03201FB0  token: 0x6000414
        public static UnityEngine.Vector3 ProjectOntoPlane(UnityEngine.Vector3 vector, UnityEngine.Vector3 planeNormal) { }
        // RVA: 0x04D880E0  token: 0x6000415
        public static UnityEngine.Vector2 Abs(UnityEngine.Vector2 v) { }
        // RVA: 0x067B856C  token: 0x6000416
        public static System.Boolean IsUniform(UnityEngine.Vector2 v) { }
        // RVA: 0x03201F90  token: 0x6000417
        public static System.Boolean AlmostZero(UnityEngine.Vector3 v) { }
        // RVA: 0x02F9FBB0  token: 0x6000418
        public static System.Single Angle(UnityEngine.Vector3 v1, UnityEngine.Vector3 v2) { }
        // RVA: 0x03202070  token: 0x6000419
        public static System.Single SignedAngle(UnityEngine.Vector3 v1, UnityEngine.Vector3 v2, UnityEngine.Vector3 up) { }
        // RVA: 0x067B85C4  token: 0x600041A
        public static UnityEngine.Quaternion SafeFromToRotation(UnityEngine.Vector3 v1, UnityEngine.Vector3 v2, UnityEngine.Vector3 up) { }

    }

    // TypeToken: 0x20000CA  // size: 0x10
    public static class UnityQuaternionExtensions
    {
        // Methods
        // RVA: 0x067B80E4  token: 0x600041B
        public static UnityEngine.Quaternion SlerpWithReferenceUp(UnityEngine.Quaternion qA, UnityEngine.Quaternion qB, System.Single t, UnityEngine.Vector3 up) { }
        // RVA: 0x031CC020  token: 0x600041C
        public static UnityEngine.Quaternion Normalized(UnityEngine.Quaternion q) { }
        // RVA: 0x03201980  token: 0x600041D
        public static UnityEngine.Vector2 GetCameraRotationToTarget(UnityEngine.Quaternion orient, UnityEngine.Vector3 lookAtDir, UnityEngine.Vector3 worldUp) { }
        // RVA: 0x03200080  token: 0x600041E
        public static UnityEngine.Quaternion ApplyCameraRotation(UnityEngine.Quaternion orient, UnityEngine.Vector2 rot, UnityEngine.Vector3 worldUp) { }

    }

}

