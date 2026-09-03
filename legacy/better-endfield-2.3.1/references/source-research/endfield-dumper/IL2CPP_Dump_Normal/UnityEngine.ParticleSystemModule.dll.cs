// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.ParticleSystemModule.dll
// Classes:  65
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000003  // size: 0x18
    public sealed struct MainModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Properties
        System.Single duration { get; /* RVA: 0x0B41851C */ set; /* RVA: 0x0B418964 */ }
        System.Boolean loop { get; /* RVA: 0x0B418594 */ set; /* RVA: 0x0B4189FC */ }
        System.Single startDelayMultiplier { get; /* RVA: 0x0B41868C */ set; /* RVA: 0x0B418ADC */ }
        UnityEngine.ParticleSystem.MinMaxCurve startLifetime { get; /* RVA: 0x0B418714 */ set; /* RVA: 0x0B418B74 */ }
        System.Single startLifetimeMultiplier { get; /* RVA: 0x0B4186C8 */ set; /* RVA: 0x0B418B28 */ }
        UnityEngine.ParticleSystem.MinMaxCurve startSpeed { get; /* RVA: 0x0B4188F0 */ }
        System.Single startSpeedMultiplier { get; /* RVA: 0x0B4188A4 */ set; /* RVA: 0x0B418DA0 */ }
        UnityEngine.ParticleSystem.MinMaxCurve startSize { set; /* RVA: 0x0B418D48 */ }
        System.Single startSizeMultiplier { get; /* RVA: 0x0B418868 */ set; /* RVA: 0x0B418CFC */ }
        System.Single startRotationMultiplier { get; /* RVA: 0x0B418778 */ set; /* RVA: 0x0B418BCC */ }
        System.Single startRotationXMultiplier { get; /* RVA: 0x0B4187B4 */ set; /* RVA: 0x0B418C18 */ }
        System.Single startRotationYMultiplier { get; /* RVA: 0x0B4187F0 */ set; /* RVA: 0x0B418C64 */ }
        System.Single startRotationZMultiplier { get; /* RVA: 0x0B41882C */ set; /* RVA: 0x0B418CB0 */ }
        UnityEngine.ParticleSystem.MinMaxGradient startColor { get; /* RVA: 0x03820C70 */ set; /* RVA: 0x05393AA4 */ }
        System.Single gravityModifierMultiplier { get; /* RVA: 0x0B418558 */ set; /* RVA: 0x0B4189B0 */ }
        UnityEngine.ParticleSystemSimulationSpace simulationSpace { get; /* RVA: 0x0B418648 */ set; /* RVA: 0x0B418A90 */ }
        System.Single simulationSpeed { get; /* RVA: 0x0B418650 */ set; /* RVA: 0x05392318 */ }
        UnityEngine.ParticleSystemScalingMode scalingMode { get; /* RVA: 0x0B41860C */ set; /* RVA: 0x05395D88 */ }
        System.Boolean playOnAwake { get; /* RVA: 0x0B4185D0 */ set; /* RVA: 0x0B418A48 */ }
        System.Int32 maxParticles { get; /* RVA: 0x05395D80 */ set; /* RVA: 0x05395D90 */ }

        // Methods
        // RVA: 0x05392320  token: 0x600008E
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }
        // RVA: 0x0B4184E8  token: 0x60000B5
        private static System.Single get_duration_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self) { }
        // RVA: 0x0B418920  token: 0x60000B6
        private static System.Void set_duration_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, System.Single value) { }
        // RVA: 0x0B418560  token: 0x60000B7
        private static System.Boolean get_loop_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self) { }
        // RVA: 0x0B4189B8  token: 0x60000B8
        private static System.Void set_loop_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, System.Boolean value) { }
        // RVA: 0x0B418658  token: 0x60000B9
        private static System.Single get_startDelayMultiplier_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self) { }
        // RVA: 0x0B418A98  token: 0x60000BA
        private static System.Void set_startDelayMultiplier_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, System.Single value) { }
        // RVA: 0x0B4186D0  token: 0x60000BB
        private static System.Void get_startLifetime_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, UnityEngine.ParticleSystem.MinMaxCurve& ret) { }
        // RVA: 0x0B418B30  token: 0x60000BC
        private static System.Void set_startLifetime_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, UnityEngine.ParticleSystem.MinMaxCurve& value) { }
        // RVA: 0x0B418694  token: 0x60000BD
        private static System.Single get_startLifetimeMultiplier_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self) { }
        // RVA: 0x0B418AE4  token: 0x60000BE
        private static System.Void set_startLifetimeMultiplier_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, System.Single value) { }
        // RVA: 0x0B4188AC  token: 0x60000BF
        private static System.Void get_startSpeed_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, UnityEngine.ParticleSystem.MinMaxCurve& ret) { }
        // RVA: 0x0B418870  token: 0x60000C0
        private static System.Single get_startSpeedMultiplier_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self) { }
        // RVA: 0x0B418D5C  token: 0x60000C1
        private static System.Void set_startSpeedMultiplier_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, System.Single value) { }
        // RVA: 0x0B418D04  token: 0x60000C2
        private static System.Void set_startSize_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, UnityEngine.ParticleSystem.MinMaxCurve& value) { }
        // RVA: 0x0B418834  token: 0x60000C3
        private static System.Single get_startSizeMultiplier_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self) { }
        // RVA: 0x0B418CB8  token: 0x60000C4
        private static System.Void set_startSizeMultiplier_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, System.Single value) { }
        // RVA: 0x0B418744  token: 0x60000C5
        private static System.Single get_startRotationMultiplier_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self) { }
        // RVA: 0x0B418B88  token: 0x60000C6
        private static System.Void set_startRotationMultiplier_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, System.Single value) { }
        // RVA: 0x0B418780  token: 0x60000C7
        private static System.Single get_startRotationXMultiplier_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self) { }
        // RVA: 0x0B418BD4  token: 0x60000C8
        private static System.Void set_startRotationXMultiplier_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, System.Single value) { }
        // RVA: 0x0B4187BC  token: 0x60000C9
        private static System.Single get_startRotationYMultiplier_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self) { }
        // RVA: 0x0B418C20  token: 0x60000CA
        private static System.Void set_startRotationYMultiplier_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, System.Single value) { }
        // RVA: 0x0B4187F8  token: 0x60000CB
        private static System.Single get_startRotationZMultiplier_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self) { }
        // RVA: 0x0B418C6C  token: 0x60000CC
        private static System.Void set_startRotationZMultiplier_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, System.Single value) { }
        // RVA: 0x03820CB0  token: 0x60000CD
        private static System.Void get_startColor_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, UnityEngine.ParticleSystem.MinMaxGradient& ret) { }
        // RVA: 0x04B37B00  token: 0x60000CE
        private static System.Void set_startColor_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, UnityEngine.ParticleSystem.MinMaxGradient& value) { }
        // RVA: 0x0B418524  token: 0x60000CF
        private static System.Single get_gravityModifierMultiplier_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self) { }
        // RVA: 0x0B41896C  token: 0x60000D0
        private static System.Void set_gravityModifierMultiplier_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, System.Single value) { }
        // RVA: 0x0B418614  token: 0x60000D1
        private static UnityEngine.ParticleSystemSimulationSpace get_simulationSpace_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self) { }
        // RVA: 0x0B418A50  token: 0x60000D2
        private static System.Void set_simulationSpace_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, UnityEngine.ParticleSystemSimulationSpace value) { }
        // RVA: 0x04048780  token: 0x60000D3
        private static System.Single get_simulationSpeed_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self) { }
        // RVA: 0x03FAA510  token: 0x60000D4
        private static System.Void set_simulationSpeed_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, System.Single value) { }
        // RVA: 0x0B4185D8  token: 0x60000D5
        private static UnityEngine.ParticleSystemScalingMode get_scalingMode_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self) { }
        // RVA: 0x03E042A0  token: 0x60000D6
        private static System.Void set_scalingMode_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, UnityEngine.ParticleSystemScalingMode value) { }
        // RVA: 0x0B41859C  token: 0x60000D7
        private static System.Boolean get_playOnAwake_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self) { }
        // RVA: 0x0B418A04  token: 0x60000D8
        private static System.Void set_playOnAwake_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, System.Boolean value) { }
        // RVA: 0x03E04210  token: 0x60000D9
        private static System.Int32 get_maxParticles_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self) { }
        // RVA: 0x03E04250  token: 0x60000DA
        private static System.Void set_maxParticles_Injected(UnityEngine.ParticleSystem.MainModule& _unity_self, System.Int32 value) { }

    }

    // TypeToken: 0x2000004  // size: 0x18
    public sealed struct EmissionModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Properties
        System.Boolean enabled { get; /* RVA: 0x0B418458 */ set; /* RVA: 0x0B4184E0 */ }
        UnityEngine.ParticleSystem.MinMaxCurve rateOverTime { get; /* RVA: 0x03E04160 */ set; /* RVA: 0x05395D98 */ }
        System.Single rateOverTimeMultiplier { get; /* RVA: 0x0B418494 */ }

        // Methods
        // RVA: 0x05392320  token: 0x60000DB
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }
        // RVA: 0x0B418424  token: 0x60000E1
        private static System.Boolean get_enabled_Injected(UnityEngine.ParticleSystem.EmissionModule& _unity_self) { }
        // RVA: 0x0B41849C  token: 0x60000E2
        private static System.Void set_enabled_Injected(UnityEngine.ParticleSystem.EmissionModule& _unity_self, System.Boolean value) { }
        // RVA: 0x03E041C0  token: 0x60000E3
        private static System.Void get_rateOverTime_Injected(UnityEngine.ParticleSystem.EmissionModule& _unity_self, UnityEngine.ParticleSystem.MinMaxCurve& ret) { }
        // RVA: 0x03E042F0  token: 0x60000E4
        private static System.Void set_rateOverTime_Injected(UnityEngine.ParticleSystem.EmissionModule& _unity_self, UnityEngine.ParticleSystem.MinMaxCurve& value) { }
        // RVA: 0x0B418460  token: 0x60000E5
        private static System.Single get_rateOverTimeMultiplier_Injected(UnityEngine.ParticleSystem.EmissionModule& _unity_self) { }

    }

    // TypeToken: 0x2000005  // size: 0x18
    public sealed struct ShapeModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Properties
        System.Boolean enabled { set; /* RVA: 0x0B41CDE4 */ }
        UnityEngine.ParticleSystemShapeType shapeType { set; /* RVA: 0x05393370 */ }
        System.Single radius { set; /* RVA: 0x0B41CEC8 */ }
        UnityEngine.Mesh mesh { set; /* RVA: 0x0B41CE7C */ }
        UnityEngine.MeshRenderer meshRenderer { set; /* RVA: 0x0B41CE30 */ }
        UnityEngine.SkinnedMeshRenderer skinnedMeshRenderer { set; /* RVA: 0x05393378 */ }
        System.Single textureClipThreshold { set; /* RVA: 0x0B41CF60 */ }
        System.Single textureClipThresholdUpper { set; /* RVA: 0x0B41CF14 */ }

        // Methods
        // RVA: 0x05392320  token: 0x60000E6
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }
        // RVA: 0x0B41CDA0  token: 0x60000EF
        private static System.Void set_enabled_Injected(UnityEngine.ParticleSystem.ShapeModule& _unity_self, System.Boolean value) { }
        // RVA: 0x04CD8D60  token: 0x60000F0
        private static System.Void set_shapeType_Injected(UnityEngine.ParticleSystem.ShapeModule& _unity_self, UnityEngine.ParticleSystemShapeType value) { }
        // RVA: 0x0B41CE84  token: 0x60000F1
        private static System.Void set_radius_Injected(UnityEngine.ParticleSystem.ShapeModule& _unity_self, System.Single value) { }
        // RVA: 0x0B41CE38  token: 0x60000F2
        private static System.Void set_mesh_Injected(UnityEngine.ParticleSystem.ShapeModule& _unity_self, UnityEngine.Mesh value) { }
        // RVA: 0x0B41CDEC  token: 0x60000F3
        private static System.Void set_meshRenderer_Injected(UnityEngine.ParticleSystem.ShapeModule& _unity_self, UnityEngine.MeshRenderer value) { }
        // RVA: 0x04CD8D10  token: 0x60000F4
        private static System.Void set_skinnedMeshRenderer_Injected(UnityEngine.ParticleSystem.ShapeModule& _unity_self, UnityEngine.SkinnedMeshRenderer value) { }
        // RVA: 0x0B41CF1C  token: 0x60000F5
        private static System.Void set_textureClipThreshold_Injected(UnityEngine.ParticleSystem.ShapeModule& _unity_self, System.Single value) { }
        // RVA: 0x0B41CED0  token: 0x60000F6
        private static System.Void set_textureClipThresholdUpper_Injected(UnityEngine.ParticleSystem.ShapeModule& _unity_self, System.Single value) { }

    }

    // TypeToken: 0x2000006  // size: 0x18
    public sealed struct CollisionModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Properties
        System.Boolean enabled { get; /* RVA: 0x0B41841C */ }

        // Methods
        // RVA: 0x05392320  token: 0x60000F7
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }
        // RVA: 0x0B4183E8  token: 0x60000F9
        private static System.Boolean get_enabled_Injected(UnityEngine.ParticleSystem.CollisionModule& _unity_self) { }

    }

    // TypeToken: 0x2000007  // size: 0x18
    public sealed struct TriggerModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Methods
        // RVA: 0x05392320  token: 0x60000FA
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }

    }

    // TypeToken: 0x2000008  // size: 0x18
    public sealed struct SubEmittersModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Properties
        System.Int32 subEmittersCount { get; /* RVA: 0x0B41CFE4 */ }

        // Methods
        // RVA: 0x05392320  token: 0x60000FB
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }
        // RVA: 0x0B41CFA8  token: 0x60000FD
        public UnityEngine.ParticleSystem GetSubEmitterSystem(System.Int32 index) { }
        // RVA: 0x0B41CFB0  token: 0x60000FE
        private static System.Int32 get_subEmittersCount_Injected(UnityEngine.ParticleSystem.SubEmittersModule& _unity_self) { }
        // RVA: 0x0B41CF68  token: 0x60000FF
        private static UnityEngine.ParticleSystem GetSubEmitterSystem_Injected(UnityEngine.ParticleSystem.SubEmittersModule& _unity_self, System.Int32 index) { }

    }

    // TypeToken: 0x2000009  // size: 0x18
    public sealed struct TextureSheetAnimationModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Methods
        // RVA: 0x05392320  token: 0x6000100
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }

    }

    // TypeToken: 0x200000A  // size: 0x94
    public sealed struct Particle
    {
        // Fields
        private UnityEngine.Vector3 m_Position;  // 0x10
        private UnityEngine.Vector3 m_Velocity;  // 0x1c
        private UnityEngine.Vector3 m_AnimatedVelocity;  // 0x28
        private UnityEngine.Vector3 m_InitialVelocity;  // 0x34
        private UnityEngine.Vector3 m_AxisOfRotation;  // 0x40
        private UnityEngine.Vector3 m_Rotation;  // 0x4c
        private UnityEngine.Vector3 m_AngularVelocity;  // 0x58
        private UnityEngine.Vector3 m_StartSize;  // 0x64
        private UnityEngine.Color32 m_StartColor;  // 0x70
        private System.UInt32 m_RandomSeed;  // 0x74
        private System.UInt32 m_ParentRandomSeed;  // 0x78
        private System.Single m_Lifetime;  // 0x7c
        private System.Single m_StartLifetime;  // 0x80
        private System.Int32 m_MeshIndex;  // 0x84
        private System.Single m_EmitAccumulator0;  // 0x88
        private System.Single m_EmitAccumulator1;  // 0x8c
        private System.UInt32 m_Flags;  // 0x90

        // Properties
        System.Single lifetime { set; /* RVA: 0x04DA3330 */ }
        UnityEngine.Vector3 position { set; /* RVA: 0x04DABF80 */ }
        UnityEngine.Vector3 velocity { set; /* RVA: 0x04DBAA60 */ }
        System.Single remainingLifetime { set; /* RVA: 0x04DA3330 */ }
        System.Single startLifetime { set; /* RVA: 0x04D90600 */ }
        UnityEngine.Color32 startColor { set; /* RVA: 0x04D86D50 */ }
        System.UInt32 randomSeed { set; /* RVA: 0x04D88120 */ }
        System.Single startSize { set; /* RVA: 0x04DBD210 */ }
        UnityEngine.Vector3 rotation3D { set; /* RVA: 0x0B41CD50 */ }
        UnityEngine.Vector3 angularVelocity3D { set; /* RVA: 0x0B41CD00 */ }

    }

    // TypeToken: 0x200000B  // size: 0x30
    public sealed struct MinMaxCurve
    {
        // Fields
        private UnityEngine.ParticleSystemCurveMode m_Mode;  // 0x10
        private System.Single m_CurveMultiplier;  // 0x14
        private UnityEngine.AnimationCurve m_CurveMin;  // 0x18
        private UnityEngine.AnimationCurve m_CurveMax;  // 0x20
        private System.Single m_ConstantMin;  // 0x28
        private System.Single m_ConstantMax;  // 0x2c

        // Properties
        UnityEngine.ParticleSystemCurveMode mode { get; /* RVA: 0x04DBA750 */ }
        UnityEngine.AnimationCurve curveMax { get; /* RVA: 0x04D9B600 */ }
        UnityEngine.AnimationCurve curveMin { get; /* RVA: 0x04DBA980 */ }
        System.Single constantMax { get; /* RVA: 0x04DBD180 */ }
        System.Single constantMin { get; /* RVA: 0x04DBAB50 */ }
        System.Single constant { get; /* RVA: 0x04DBD180 */ set; /* RVA: 0x04D86900 */ }
        UnityEngine.AnimationCurve curve { get; /* RVA: 0x04D9B600 */ }

        // Methods
        // RVA: 0x047A9B90  token: 0x600010B
        public System.Void .ctor(System.Single constant) { }
        // RVA: 0x047A9B20  token: 0x6000114
        public static UnityEngine.ParticleSystem.MinMaxCurve op_Implicit(System.Single constant) { }

    }

    // TypeToken: 0x200000C  // size: 0x48
    public sealed struct MinMaxGradient
    {
        // Fields
        private UnityEngine.ParticleSystemGradientMode m_Mode;  // 0x10
        private UnityEngine.Gradient m_GradientMin;  // 0x18
        private UnityEngine.Gradient m_GradientMax;  // 0x20
        private UnityEngine.Color m_ColorMin;  // 0x28
        private UnityEngine.Color m_ColorMax;  // 0x38

        // Properties
        UnityEngine.Color colorMax { set; /* RVA: 0x04D8FAE0 */ }
        UnityEngine.Color colorMin { set; /* RVA: 0x04D890D0 */ }
        UnityEngine.Color color { get; /* RVA: 0x04DBCFB0 */ set; /* RVA: 0x04D8FAE0 */ }

        // Methods
        // RVA: 0x0B418DA8  token: 0x6000115
        public System.Void .ctor(UnityEngine.Color color) { }
        // RVA: 0x0B418DF8  token: 0x600011A
        public static UnityEngine.ParticleSystem.MinMaxGradient op_Implicit(UnityEngine.Color color) { }

    }

    // TypeToken: 0x200000D  // size: 0xA0
    public sealed struct EmitParams
    {
        // Fields
        private UnityEngine.ParticleSystem.Particle m_Particle;  // 0x10
        private System.Boolean m_PositionSet;  // 0x94
        private System.Boolean m_VelocitySet;  // 0x95
        private System.Boolean m_AxisOfRotationSet;  // 0x96
        private System.Boolean m_RotationSet;  // 0x97
        private System.Boolean m_AngularVelocitySet;  // 0x98
        private System.Boolean m_StartSizeSet;  // 0x99
        private System.Boolean m_StartColorSet;  // 0x9a
        private System.Boolean m_RandomSeedSet;  // 0x9b
        private System.Boolean m_StartLifetimeSet;  // 0x9c
        private System.Boolean m_MeshIndexSet;  // 0x9d
        private System.Boolean m_ApplyShapeToPosition;  // 0x9e

    }

    // TypeToken: 0x200000F  // size: 0x20
    public sealed struct Seed
    {
        // Fields
        public System.UInt32 x;  // 0x10
        public System.UInt32 y;  // 0x14
        public System.UInt32 z;  // 0x18
        public System.UInt32 w;  // 0x1c

    }

    // TypeToken: 0x2000010  // size: 0x50
    public sealed struct Seed4
    {
        // Fields
        public UnityEngine.ParticleSystem.PlaybackState.Seed x;  // 0x10
        public UnityEngine.ParticleSystem.PlaybackState.Seed y;  // 0x20
        public UnityEngine.ParticleSystem.PlaybackState.Seed z;  // 0x30
        public UnityEngine.ParticleSystem.PlaybackState.Seed w;  // 0x40

    }

    // TypeToken: 0x2000011  // size: 0x28
    public sealed struct Emission
    {
        // Fields
        public System.Single m_ParticleSpacing;  // 0x10
        public System.Single m_ToEmitAccumulator;  // 0x14
        public UnityEngine.ParticleSystem.PlaybackState.Seed m_Random;  // 0x18

    }

    // TypeToken: 0x2000012  // size: 0x50
    public sealed struct Initial
    {
        // Fields
        public UnityEngine.ParticleSystem.PlaybackState.Seed4 m_Random;  // 0x10

    }

    // TypeToken: 0x2000013  // size: 0x6C
    public sealed struct Shape
    {
        // Fields
        public UnityEngine.ParticleSystem.PlaybackState.Seed4 m_Random;  // 0x10
        public System.Single m_RadiusTimer;  // 0x50
        public System.Single m_RadiusTimerPrev;  // 0x54
        public System.Single m_ArcTimer;  // 0x58
        public System.Single m_ArcTimerPrev;  // 0x5c
        public System.Single m_MeshSpawnTimer;  // 0x60
        public System.Single m_MeshSpawnTimerPrev;  // 0x64
        public System.Int32 m_OrderedMeshVertexIndex;  // 0x68

    }

    // TypeToken: 0x2000014  // size: 0x50
    public sealed struct Force
    {
        // Fields
        public UnityEngine.ParticleSystem.PlaybackState.Seed4 m_Random;  // 0x10

    }

    // TypeToken: 0x2000015  // size: 0x50
    public sealed struct Collision
    {
        // Fields
        public UnityEngine.ParticleSystem.PlaybackState.Seed4 m_Random;  // 0x10

    }

    // TypeToken: 0x2000016  // size: 0x14
    public sealed struct Noise
    {
        // Fields
        public System.Single m_ScrollOffset;  // 0x10

    }

    // TypeToken: 0x2000017  // size: 0x24
    public sealed struct Lights
    {
        // Fields
        public UnityEngine.ParticleSystem.PlaybackState.Seed m_Random;  // 0x10
        public System.Single m_ParticleEmissionCounter;  // 0x20

    }

    // TypeToken: 0x2000018  // size: 0x14
    public sealed struct Trail
    {
        // Fields
        public System.Single m_Timer;  // 0x10

    }

    // TypeToken: 0x200000E  // size: 0x170
    public sealed struct PlaybackState
    {
        // Fields
        private System.Single m_AccumulatedDt;  // 0x10
        private System.Single m_StartDelay;  // 0x14
        private System.Single m_PlaybackTime;  // 0x18
        private System.Int32 m_RingBufferIndex;  // 0x1c
        private UnityEngine.ParticleSystem.PlaybackState.Emission m_Emission;  // 0x20
        private UnityEngine.ParticleSystem.PlaybackState.Initial m_Initial;  // 0x38
        private UnityEngine.ParticleSystem.PlaybackState.Shape m_Shape;  // 0x78
        private UnityEngine.ParticleSystem.PlaybackState.Force m_Force;  // 0xd4
        private UnityEngine.ParticleSystem.PlaybackState.Collision m_Collision;  // 0x114
        private UnityEngine.ParticleSystem.PlaybackState.Noise m_Noise;  // 0x154
        private UnityEngine.ParticleSystem.PlaybackState.Lights m_Lights;  // 0x158
        private UnityEngine.ParticleSystem.PlaybackState.Trail m_Trail;  // 0x16c

    }

    // TypeToken: 0x2000019  // size: 0x38
    public sealed struct Trails
    {
        // Fields
        private System.Collections.Generic.List<UnityEngine.Vector4> positions;  // 0x10
        private System.Collections.Generic.List<System.Int32> frontPositions;  // 0x18
        private System.Collections.Generic.List<System.Int32> backPositions;  // 0x20
        private System.Collections.Generic.List<System.Int32> positionCounts;  // 0x28
        private System.Int32 maxTrailCount;  // 0x30
        private System.Int32 maxPositionsPerTrailCount;  // 0x34

        // Methods
        // RVA: 0x0B41CFEC  token: 0x600011B
        private System.Void Allocate() { }

    }

    // TypeToken: 0x200001A  // size: 0x18
    public sealed struct VelocityOverLifetimeModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Methods
        // RVA: 0x05392320  token: 0x600011C
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }

    }

    // TypeToken: 0x200001B  // size: 0x18
    public sealed struct LimitVelocityOverLifetimeModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Methods
        // RVA: 0x05392320  token: 0x600011D
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }

    }

    // TypeToken: 0x200001C  // size: 0x18
    public sealed struct InheritVelocityModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Methods
        // RVA: 0x05392320  token: 0x600011E
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }

    }

    // TypeToken: 0x200001D  // size: 0x18
    public sealed struct LifetimeByEmitterSpeedModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Methods
        // RVA: 0x05392320  token: 0x600011F
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }

    }

    // TypeToken: 0x200001E  // size: 0x18
    public sealed struct ForceOverLifetimeModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Methods
        // RVA: 0x05392320  token: 0x6000120
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }

    }

    // TypeToken: 0x200001F  // size: 0x18
    public sealed struct ColorOverLifetimeModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Methods
        // RVA: 0x05392320  token: 0x6000121
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }

    }

    // TypeToken: 0x2000020  // size: 0x18
    public sealed struct ColorBySpeedModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Methods
        // RVA: 0x05392320  token: 0x6000122
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }

    }

    // TypeToken: 0x2000021  // size: 0x18
    public sealed struct SizeOverLifetimeModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Methods
        // RVA: 0x05392320  token: 0x6000123
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }

    }

    // TypeToken: 0x2000022  // size: 0x18
    public sealed struct SizeBySpeedModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Methods
        // RVA: 0x05392320  token: 0x6000124
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }

    }

    // TypeToken: 0x2000023  // size: 0x18
    public sealed struct RotationOverLifetimeModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Methods
        // RVA: 0x05392320  token: 0x6000125
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }

    }

    // TypeToken: 0x2000024  // size: 0x18
    public sealed struct RotationBySpeedModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Methods
        // RVA: 0x05392320  token: 0x6000126
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }

    }

    // TypeToken: 0x2000025  // size: 0x18
    public sealed struct ExternalForcesModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Methods
        // RVA: 0x05392320  token: 0x6000127
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }

    }

    // TypeToken: 0x2000026  // size: 0x18
    public sealed struct NoiseModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Methods
        // RVA: 0x05392320  token: 0x6000128
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }

    }

    // TypeToken: 0x2000027  // size: 0x18
    public sealed struct LightsModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Methods
        // RVA: 0x05392320  token: 0x6000129
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }

    }

    // TypeToken: 0x2000028  // size: 0x18
    public sealed struct TrailModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Methods
        // RVA: 0x05392320  token: 0x600012A
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }

    }

    // TypeToken: 0x2000029  // size: 0x18
    public sealed struct CustomDataModule
    {
        // Fields
        private UnityEngine.ParticleSystem m_ParticleSystem;  // 0x10

        // Methods
        // RVA: 0x05392320  token: 0x600012B
        private System.Void .ctor(UnityEngine.ParticleSystem particleSystem) { }

    }

    // TypeToken: 0x2000040  // size: 0x28
    public sealed struct Array3
    {
        // Fields
        private System.Single* x;  // 0x10
        private System.Single* y;  // 0x18
        private System.Single* z;  // 0x20

    }

    // TypeToken: 0x2000041  // size: 0x30
    public sealed struct Array4
    {
        // Fields
        private System.Single* x;  // 0x10
        private System.Single* y;  // 0x18
        private System.Single* z;  // 0x20
        private System.Single* w;  // 0x28

    }

namespace UnityEngine
{

    // TypeToken: 0x2000002  // size: 0x18
    public sealed class ParticleSystem : UnityEngine.Component
    {
        // Properties
        System.Single startDelay { get; /* RVA: 0x0B41C678 */ set; /* RVA: 0x0B41CB38 */ }
        System.Boolean loop { get; /* RVA: 0x0B41C488 */ set; /* RVA: 0x0B41C934 */ }
        System.Boolean playOnAwake { get; /* RVA: 0x0B41C4E8 */ set; /* RVA: 0x0B41C9A4 */ }
        System.Single duration { get; /* RVA: 0x0B41C2F8 */ }
        System.Single playbackSpeed { get; /* RVA: 0x0B41C518 */ set; /* RVA: 0x0B41C9DC */ }
        System.Boolean enableEmission { get; /* RVA: 0x0B41C358 */ set; /* RVA: 0x0B41C8CC */ }
        System.Single emissionRate { get; /* RVA: 0x0B41C328 */ set; /* RVA: 0x0B41C870 */ }
        System.Single startSpeed { get; /* RVA: 0x0B41C7D8 */ set; /* RVA: 0x0B41CC48 */ }
        System.Single startSize { get; /* RVA: 0x0B41C7A8 */ set; /* RVA: 0x0B41CC18 */ }
        UnityEngine.Color startColor { get; /* RVA: 0x0B41C610 */ set; /* RVA: 0x0B41CABC */ }
        System.Single startRotation { get; /* RVA: 0x0B41C778 */ set; /* RVA: 0x0B41CBE8 */ }
        UnityEngine.Vector3 startRotation3D { get; /* RVA: 0x0B41C6D8 */ set; /* RVA: 0x0B41CB98 */ }
        System.Single startLifetime { get; /* RVA: 0x0B41C6A8 */ set; /* RVA: 0x0B41CB68 */ }
        System.Single gravityModifier { get; /* RVA: 0x0B41C388 */ set; /* RVA: 0x0B41C904 */ }
        System.Int32 maxParticles { get; /* RVA: 0x0B41C4B8 */ set; /* RVA: 0x0B41C96C */ }
        UnityEngine.ParticleSystemSimulationSpace simulationSpace { get; /* RVA: 0x0B41C5E0 */ set; /* RVA: 0x0B41CA84 */ }
        UnityEngine.ParticleSystemScalingMode scalingMode { get; /* RVA: 0x0B41C5B0 */ set; /* RVA: 0x0B41CA4C */ }
        System.Boolean automaticCullingEnabled { get; /* RVA: 0x0B41C2EC */ }
        System.Boolean isPlaying { get; /* RVA: 0x0B41C420 */ }
        System.Boolean isEmitting { get; /* RVA: 0x0B41C3B8 */ }
        System.Boolean isStopped { get; /* RVA: 0x0B41C454 */ }
        System.Boolean isPaused { get; /* RVA: 0x0B41C3EC */ }
        System.Int32 particleCount { get; /* RVA: 0x0330C8B0 */ }
        System.Single time { get; /* RVA: 0x0B41C808 */ set; /* RVA: 0x0B41CC78 */ }
        System.UInt32 randomSeed { get; /* RVA: 0x0B41C57C */ set; /* RVA: 0x0B41CA0C */ }
        System.Boolean useAutoRandomSeed { get; /* RVA: 0x0B41C83C */ set; /* RVA: 0x0B41CCBC */ }
        System.Boolean proceduralSimulationSupported { get; /* RVA: 0x0B41C548 */ }
        UnityEngine.ParticleSystem.MainModule main { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.EmissionModule emission { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.ShapeModule shape { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.LimitVelocityOverLifetimeModule limitVelocityOverLifetime { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.InheritVelocityModule inheritVelocity { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.LifetimeByEmitterSpeedModule lifetimeByEmitterSpeed { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.ForceOverLifetimeModule forceOverLifetime { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.ColorOverLifetimeModule colorOverLifetime { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.ColorBySpeedModule colorBySpeed { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.SizeOverLifetimeModule sizeOverLifetime { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.SizeBySpeedModule sizeBySpeed { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.RotationOverLifetimeModule rotationOverLifetime { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.RotationBySpeedModule rotationBySpeed { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.ExternalForcesModule externalForces { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.NoiseModule noise { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.CollisionModule collision { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.TriggerModule trigger { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.SubEmittersModule subEmitters { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.TextureSheetAnimationModule textureSheetAnimation { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.LightsModule lights { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.TrailModule trails { get; /* RVA: 0x0339B870 */ }
        UnityEngine.ParticleSystem.CustomDataModule customData { get; /* RVA: 0x0339B870 */ }

        // Methods
        // RVA: 0x0B41B540  token: 0x6000001
        public System.Void Emit(UnityEngine.Vector3 position, UnityEngine.Vector3 velocity, System.Single size, System.Single lifetime, UnityEngine.Color32 color) { }
        // RVA: 0x0B41B648  token: 0x6000002
        public System.Void Emit(UnityEngine.ParticleSystem.Particle particle) { }
        // RVA: 0x0B41B870  token: 0x6000031
        private System.Single GetParticleCurrentSize(UnityEngine.ParticleSystem.Particle& particle) { }
        // RVA: 0x0B41B83C  token: 0x6000032
        private UnityEngine.Vector3 GetParticleCurrentSize3D(UnityEngine.ParticleSystem.Particle& particle) { }
        // RVA: 0x0B41B7C4  token: 0x6000033
        private UnityEngine.Color32 GetParticleCurrentColor(UnityEngine.ParticleSystem.Particle& particle) { }
        // RVA: 0x0B41B8B4  token: 0x6000034
        private System.Int32 GetParticleMeshIndex(UnityEngine.ParticleSystem.Particle& particle) { }
        // RVA: 0x0B41BF5C  token: 0x6000035
        public System.Void SetParticles(UnityEngine.ParticleSystem.Particle[] particles, System.Int32 size, System.Int32 offset) { }
        // RVA: 0x0B41BF40  token: 0x6000036
        public System.Void SetParticles(UnityEngine.ParticleSystem.Particle[] particles, System.Int32 size) { }
        // RVA: 0x0B41BEE4  token: 0x6000037
        public System.Void SetParticles(UnityEngine.ParticleSystem.Particle[] particles) { }
        // RVA: 0x0B41BE80  token: 0x6000038
        private System.Void SetParticlesWithNativeArray(System.IntPtr particles, System.Int32 particlesLength, System.Int32 size, System.Int32 offset) { }
        // RVA: 0x0B41BFC0  token: 0x6000039
        public System.Void SetParticles(Unity.Collections.NativeArray<UnityEngine.ParticleSystem.Particle> particles, System.Int32 size, System.Int32 offset) { }
        // RVA: 0x0B41BF18  token: 0x600003A
        public System.Void SetParticles(Unity.Collections.NativeArray<UnityEngine.ParticleSystem.Particle> particles, System.Int32 size) { }
        // RVA: 0x0B41BEF4  token: 0x600003B
        public System.Void SetParticles(Unity.Collections.NativeArray<UnityEngine.ParticleSystem.Particle> particles) { }
        // RVA: 0x0B41BA28  token: 0x600003C
        public System.Int32 GetParticles(UnityEngine.ParticleSystem.Particle[] particles, System.Int32 size, System.Int32 offset) { }
        // RVA: 0x0B41BA8C  token: 0x600003D
        public System.Int32 GetParticles(UnityEngine.ParticleSystem.Particle[] particles, System.Int32 size) { }
        // RVA: 0x0B41B95C  token: 0x600003E
        public System.Int32 GetParticles(UnityEngine.ParticleSystem.Particle[] particles) { }
        // RVA: 0x0B41B8F8  token: 0x600003F
        private System.Int32 GetParticlesWithNativeArray(System.IntPtr particles, System.Int32 particlesLength, System.Int32 size, System.Int32 offset) { }
        // RVA: 0x0B41B9B8  token: 0x6000040
        public System.Int32 GetParticles(Unity.Collections.NativeArray<UnityEngine.ParticleSystem.Particle> particles, System.Int32 size, System.Int32 offset) { }
        // RVA: 0x0B41B990  token: 0x6000041
        public System.Int32 GetParticles(Unity.Collections.NativeArray<UnityEngine.ParticleSystem.Particle> particles, System.Int32 size) { }
        // RVA: 0x0B41B96C  token: 0x6000042
        public System.Int32 GetParticles(Unity.Collections.NativeArray<UnityEngine.ParticleSystem.Particle> particles) { }
        // RVA: 0x0B41BD94  token: 0x6000043
        public System.Void SetCustomParticleData(System.Collections.Generic.List<UnityEngine.Vector4> customData, UnityEngine.ParticleSystemCustomData streamIndex) { }
        // RVA: 0x0B41B678  token: 0x6000044
        public System.Int32 GetCustomParticleData(System.Collections.Generic.List<UnityEngine.Vector4> customData, UnityEngine.ParticleSystemCustomData streamIndex) { }
        // RVA: 0x0B41BAEC  token: 0x6000045
        public UnityEngine.ParticleSystem.PlaybackState GetPlaybackState() { }
        // RVA: 0x0B41C074  token: 0x6000046
        public System.Void SetPlaybackState(UnityEngine.ParticleSystem.PlaybackState playbackState) { }
        // RVA: 0x0B41BB28  token: 0x6000047
        private System.Void GetTrailDataInternal(UnityEngine.ParticleSystem.Trails& trailData) { }
        // RVA: 0x0B41BB6C  token: 0x6000048
        public UnityEngine.ParticleSystem.Trails GetTrails() { }
        // RVA: 0x0B41BBE4  token: 0x6000049
        public System.Int32 GetTrails(UnityEngine.ParticleSystem.Trails& trailData) { }
        // RVA: 0x0B41C0CC  token: 0x600004A
        public System.Void SetTrails(UnityEngine.ParticleSystem.Trails trailData) { }
        // RVA: 0x0B41C0E0  token: 0x600004B
        public System.Void Simulate(System.Single t, System.Boolean withChildren, System.Boolean restart, System.Boolean fixedTimeStep) { }
        // RVA: 0x0B41C16C  token: 0x600004C
        public System.Void Simulate(System.Single t, System.Boolean withChildren, System.Boolean restart) { }
        // RVA: 0x0B41C150  token: 0x600004D
        public System.Void Simulate(System.Single t, System.Boolean withChildren) { }
        // RVA: 0x0B41C144  token: 0x600004E
        public System.Void Simulate(System.Single t) { }
        // RVA: 0x04279190  token: 0x600004F
        public System.Void Play(System.Boolean withChildren) { }
        // RVA: 0x05392D00  token: 0x6000050
        public System.Void Play() { }
        // RVA: 0x0B41BCA0  token: 0x6000051
        public System.Void Pause(System.Boolean withChildren) { }
        // RVA: 0x0B41BC94  token: 0x6000052
        public System.Void Pause() { }
        // RVA: 0x03FDE640  token: 0x6000053
        public System.Void Stop(System.Boolean withChildren, UnityEngine.ParticleSystemStopBehavior stopBehavior) { }
        // RVA: 0x0B41C19C  token: 0x6000054
        public System.Void Stop(System.Boolean withChildren) { }
        // RVA: 0x0B41C188  token: 0x6000055
        public System.Void Stop() { }
        // RVA: 0x04046500  token: 0x6000056
        public System.Void Clear(System.Boolean withChildren) { }
        // RVA: 0x0B41B418  token: 0x6000057
        public System.Void Clear() { }
        // RVA: 0x0B41BC44  token: 0x6000058
        public System.Boolean IsAlive(System.Boolean withChildren) { }
        // RVA: 0x0B41BC88  token: 0x6000059
        public System.Boolean IsAlive() { }
        // RVA: 0x0B41B65C  token: 0x600005A
        public System.Void Emit(System.Int32 count) { }
        // RVA: 0x0B41B500  token: 0x600005B
        private System.Void Emit_Internal(System.Int32 count) { }
        // RVA: 0x0B41B664  token: 0x600005C
        public System.Void Emit(UnityEngine.ParticleSystem.EmitParams emitParams, System.Int32 count) { }
        // RVA: 0x0B41B468  token: 0x600005D
        private System.Void EmitOld_Internal(UnityEngine.ParticleSystem.Particle& particle) { }
        // RVA: 0x0B41C210  token: 0x600005E
        public System.Void TriggerSubEmitter(System.Int32 subEmitterIndex) { }
        // RVA: 0x0B41C26C  token: 0x600005F
        public System.Void TriggerSubEmitter(System.Int32 subEmitterIndex, UnityEngine.ParticleSystem.Particle& particle) { }
        // RVA: 0x0B41C1FC  token: 0x6000060
        private System.Void TriggerSubEmitterForParticle(System.Int32 subEmitterIndex, UnityEngine.ParticleSystem.Particle particle) { }
        // RVA: 0x0B41C21C  token: 0x6000061
        public System.Void TriggerSubEmitter(System.Int32 subEmitterIndex, System.Collections.Generic.List<UnityEngine.ParticleSystem.Particle> particles) { }
        // RVA: 0x0B41BCE4  token: 0x6000062
        public static System.Void ResetPreMappedBufferMemory() { }
        // RVA: 0x0B41BE40  token: 0x6000063
        public static System.Void SetMaximumPreMappedBufferCounts(System.Int32 vertexBuffersCount, System.Int32 indexBuffersCount) { }
        // RVA: 0x0B41B370  token: 0x6000064
        public System.Void AllocateAxisOfRotationAttribute() { }
        // RVA: 0x0B41B3E4  token: 0x6000065
        public System.Void AllocateMeshIndexAttribute() { }
        // RVA: 0x0B41B3A4  token: 0x6000066
        public System.Void AllocateCustomDataAttribute(UnityEngine.ParticleSystemCustomData stream) { }
        // RVA: 0x0B41B6CC  token: 0x6000067
        private System.Void* GetManagedJobData() { }
        // RVA: 0x0B41B744  token: 0x6000068
        private Unity.Jobs.JobHandle GetManagedJobHandle() { }
        // RVA: 0x0B41BE2C  token: 0x6000069
        private System.Void SetManagedJobHandle(Unity.Jobs.JobHandle handle) { }
        // RVA: 0x0B41BD64  token: 0x600006A
        private static Unity.Jobs.JobHandle ScheduleManagedJob(Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters& parameters, System.Void* additionalData) { }
        // RVA: 0x0B41B424  token: 0x600006B
        private static System.Void CopyManagedJobData(System.Void* systemPtr, UnityEngine.ParticleSystemJobs.NativeParticleData& particleData) { }
        // RVA: 0x05393520  token: 0x6000083
        public System.Void .ctor() { }
        // RVA: 0x0B41B7E8  token: 0x6000084
        private System.Void GetParticleCurrentSize3D_Injected(UnityEngine.ParticleSystem.Particle& particle, UnityEngine.Vector3& ret) { }
        // RVA: 0x0B41B770  token: 0x6000085
        private System.Void GetParticleCurrentColor_Injected(UnityEngine.ParticleSystem.Particle& particle, UnityEngine.Color32& ret) { }
        // RVA: 0x0B41BAA8  token: 0x6000086
        private System.Void GetPlaybackState_Injected(UnityEngine.ParticleSystem.PlaybackState& ret) { }
        // RVA: 0x0B41C030  token: 0x6000087
        private System.Void SetPlaybackState_Injected(UnityEngine.ParticleSystem.PlaybackState& playbackState) { }
        // RVA: 0x0B41C088  token: 0x6000088
        private System.Void SetTrails_Injected(UnityEngine.ParticleSystem.Trails& trailData) { }
        // RVA: 0x0B41B4AC  token: 0x6000089
        private System.Void Emit_Injected(UnityEngine.ParticleSystem.EmitParams& emitParams, System.Int32 count) { }
        // RVA: 0x0B41C1AC  token: 0x600008A
        private System.Void TriggerSubEmitterForParticle_Injected(System.Int32 subEmitterIndex, UnityEngine.ParticleSystem.Particle& particle) { }
        // RVA: 0x0B41B700  token: 0x600008B
        private System.Void GetManagedJobHandle_Injected(Unity.Jobs.JobHandle& ret) { }
        // RVA: 0x0B41BDE8  token: 0x600008C
        private System.Void SetManagedJobHandle_Injected(Unity.Jobs.JobHandle& handle) { }
        // RVA: 0x0B41BD10  token: 0x600008D
        private static System.Void ScheduleManagedJob_Injected(Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters& parameters, System.Void* additionalData, Unity.Jobs.JobHandle& ret) { }

    }

    // TypeToken: 0x200002A  // size: 0x10
    public static class ParticlePhysicsExtensions
    {
        // Methods
        // RVA: 0x0B418EC0  token: 0x600012C
        public static System.Int32 GetSafeCollisionEventSize(UnityEngine.ParticleSystem ps) { }
        // RVA: 0x0B418EB4  token: 0x600012D
        public static System.Int32 GetCollisionEvents(UnityEngine.ParticleSystem ps, UnityEngine.GameObject go, System.Collections.Generic.List<UnityEngine.ParticleCollisionEvent> collisionEvents) { }

    }

    // TypeToken: 0x200002B  // size: 0x14
    public sealed struct ParticleSystemRenderMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ParticleSystemRenderMode Billboard;  // const
        public static UnityEngine.ParticleSystemRenderMode Stretch;  // const
        public static UnityEngine.ParticleSystemRenderMode HorizontalBillboard;  // const
        public static UnityEngine.ParticleSystemRenderMode VerticalBillboard;  // const
        public static UnityEngine.ParticleSystemRenderMode Mesh;  // const
        public static UnityEngine.ParticleSystemRenderMode None;  // const

    }

    // TypeToken: 0x200002C  // size: 0x14
    public sealed struct ParticleSystemMeshDistribution
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ParticleSystemMeshDistribution UniformRandom;  // const
        public static UnityEngine.ParticleSystemMeshDistribution NonUniformRandom;  // const

    }

    // TypeToken: 0x200002D  // size: 0x14
    public sealed struct ParticleSystemSortMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ParticleSystemSortMode None;  // const
        public static UnityEngine.ParticleSystemSortMode Distance;  // const
        public static UnityEngine.ParticleSystemSortMode OldestInFront;  // const
        public static UnityEngine.ParticleSystemSortMode YoungestInFront;  // const
        public static UnityEngine.ParticleSystemSortMode Depth;  // const

    }

    // TypeToken: 0x200002E  // size: 0x14
    public sealed struct ParticleSystemRenderSpace
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ParticleSystemRenderSpace View;  // const
        public static UnityEngine.ParticleSystemRenderSpace World;  // const
        public static UnityEngine.ParticleSystemRenderSpace Local;  // const
        public static UnityEngine.ParticleSystemRenderSpace Facing;  // const
        public static UnityEngine.ParticleSystemRenderSpace Velocity;  // const

    }

    // TypeToken: 0x200002F  // size: 0x14
    public sealed struct ParticleSystemCurveMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ParticleSystemCurveMode Constant;  // const
        public static UnityEngine.ParticleSystemCurveMode Curve;  // const
        public static UnityEngine.ParticleSystemCurveMode TwoCurves;  // const
        public static UnityEngine.ParticleSystemCurveMode TwoConstants;  // const

    }

    // TypeToken: 0x2000030  // size: 0x14
    public sealed struct ParticleSystemGradientMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ParticleSystemGradientMode Color;  // const
        public static UnityEngine.ParticleSystemGradientMode Gradient;  // const
        public static UnityEngine.ParticleSystemGradientMode TwoColors;  // const
        public static UnityEngine.ParticleSystemGradientMode TwoGradients;  // const
        public static UnityEngine.ParticleSystemGradientMode RandomColor;  // const

    }

    // TypeToken: 0x2000031  // size: 0x14
    public sealed struct ParticleSystemShapeType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ParticleSystemShapeType Sphere;  // const
        public static UnityEngine.ParticleSystemShapeType SphereShell;  // const
        public static UnityEngine.ParticleSystemShapeType Hemisphere;  // const
        public static UnityEngine.ParticleSystemShapeType HemisphereShell;  // const
        public static UnityEngine.ParticleSystemShapeType Cone;  // const
        public static UnityEngine.ParticleSystemShapeType Box;  // const
        public static UnityEngine.ParticleSystemShapeType Mesh;  // const
        public static UnityEngine.ParticleSystemShapeType ConeShell;  // const
        public static UnityEngine.ParticleSystemShapeType ConeVolume;  // const
        public static UnityEngine.ParticleSystemShapeType ConeVolumeShell;  // const
        public static UnityEngine.ParticleSystemShapeType Circle;  // const
        public static UnityEngine.ParticleSystemShapeType CircleEdge;  // const
        public static UnityEngine.ParticleSystemShapeType SingleSidedEdge;  // const
        public static UnityEngine.ParticleSystemShapeType MeshRenderer;  // const
        public static UnityEngine.ParticleSystemShapeType SkinnedMeshRenderer;  // const
        public static UnityEngine.ParticleSystemShapeType BoxShell;  // const
        public static UnityEngine.ParticleSystemShapeType BoxEdge;  // const
        public static UnityEngine.ParticleSystemShapeType Donut;  // const
        public static UnityEngine.ParticleSystemShapeType Rectangle;  // const
        public static UnityEngine.ParticleSystemShapeType Sprite;  // const
        public static UnityEngine.ParticleSystemShapeType SpriteRenderer;  // const

    }

    // TypeToken: 0x2000032  // size: 0x14
    public sealed struct ParticleSystemSimulationSpace
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ParticleSystemSimulationSpace Local;  // const
        public static UnityEngine.ParticleSystemSimulationSpace World;  // const
        public static UnityEngine.ParticleSystemSimulationSpace Custom;  // const

    }

    // TypeToken: 0x2000033  // size: 0x14
    public sealed struct ParticleSystemStopBehavior
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ParticleSystemStopBehavior StopEmittingAndClear;  // const
        public static UnityEngine.ParticleSystemStopBehavior StopEmitting;  // const

    }

    // TypeToken: 0x2000034  // size: 0x14
    public sealed struct ParticleSystemScalingMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ParticleSystemScalingMode Hierarchy;  // const
        public static UnityEngine.ParticleSystemScalingMode Local;  // const
        public static UnityEngine.ParticleSystemScalingMode Shape;  // const

    }

    // TypeToken: 0x2000035  // size: 0x14
    public sealed struct ParticleSystemVertexStream
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ParticleSystemVertexStream Position;  // const
        public static UnityEngine.ParticleSystemVertexStream Normal;  // const
        public static UnityEngine.ParticleSystemVertexStream Tangent;  // const
        public static UnityEngine.ParticleSystemVertexStream Color;  // const
        public static UnityEngine.ParticleSystemVertexStream UV;  // const
        public static UnityEngine.ParticleSystemVertexStream UV2;  // const
        public static UnityEngine.ParticleSystemVertexStream UV3;  // const
        public static UnityEngine.ParticleSystemVertexStream UV4;  // const
        public static UnityEngine.ParticleSystemVertexStream AnimBlend;  // const
        public static UnityEngine.ParticleSystemVertexStream AnimFrame;  // const
        public static UnityEngine.ParticleSystemVertexStream Center;  // const
        public static UnityEngine.ParticleSystemVertexStream VertexID;  // const
        public static UnityEngine.ParticleSystemVertexStream SizeX;  // const
        public static UnityEngine.ParticleSystemVertexStream SizeXY;  // const
        public static UnityEngine.ParticleSystemVertexStream SizeXYZ;  // const
        public static UnityEngine.ParticleSystemVertexStream Rotation;  // const
        public static UnityEngine.ParticleSystemVertexStream Rotation3D;  // const
        public static UnityEngine.ParticleSystemVertexStream RotationSpeed;  // const
        public static UnityEngine.ParticleSystemVertexStream RotationSpeed3D;  // const
        public static UnityEngine.ParticleSystemVertexStream Velocity;  // const
        public static UnityEngine.ParticleSystemVertexStream Speed;  // const
        public static UnityEngine.ParticleSystemVertexStream AgePercent;  // const
        public static UnityEngine.ParticleSystemVertexStream InvStartLifetime;  // const
        public static UnityEngine.ParticleSystemVertexStream StableRandomX;  // const
        public static UnityEngine.ParticleSystemVertexStream StableRandomXY;  // const
        public static UnityEngine.ParticleSystemVertexStream StableRandomXYZ;  // const
        public static UnityEngine.ParticleSystemVertexStream StableRandomXYZW;  // const
        public static UnityEngine.ParticleSystemVertexStream VaryingRandomX;  // const
        public static UnityEngine.ParticleSystemVertexStream VaryingRandomXY;  // const
        public static UnityEngine.ParticleSystemVertexStream VaryingRandomXYZ;  // const
        public static UnityEngine.ParticleSystemVertexStream VaryingRandomXYZW;  // const
        public static UnityEngine.ParticleSystemVertexStream Custom1X;  // const
        public static UnityEngine.ParticleSystemVertexStream Custom1XY;  // const
        public static UnityEngine.ParticleSystemVertexStream Custom1XYZ;  // const
        public static UnityEngine.ParticleSystemVertexStream Custom1XYZW;  // const
        public static UnityEngine.ParticleSystemVertexStream Custom2X;  // const
        public static UnityEngine.ParticleSystemVertexStream Custom2XY;  // const
        public static UnityEngine.ParticleSystemVertexStream Custom2XYZ;  // const
        public static UnityEngine.ParticleSystemVertexStream Custom2XYZW;  // const
        public static UnityEngine.ParticleSystemVertexStream NoiseSumX;  // const
        public static UnityEngine.ParticleSystemVertexStream NoiseSumXY;  // const
        public static UnityEngine.ParticleSystemVertexStream NoiseSumXYZ;  // const
        public static UnityEngine.ParticleSystemVertexStream NoiseImpulseX;  // const
        public static UnityEngine.ParticleSystemVertexStream NoiseImpulseXY;  // const
        public static UnityEngine.ParticleSystemVertexStream NoiseImpulseXYZ;  // const
        public static UnityEngine.ParticleSystemVertexStream MeshIndex;  // const

    }

    // TypeToken: 0x2000036  // size: 0x14
    public sealed struct ParticleSystemCustomData
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ParticleSystemCustomData Custom1;  // const
        public static UnityEngine.ParticleSystemCustomData Custom2;  // const

    }

    // TypeToken: 0x2000037  // size: 0x14
    public sealed struct ParticleSystemTrailTextureMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ParticleSystemTrailTextureMode Stretch;  // const
        public static UnityEngine.ParticleSystemTrailTextureMode Tile;  // const
        public static UnityEngine.ParticleSystemTrailTextureMode DistributePerSegment;  // const
        public static UnityEngine.ParticleSystemTrailTextureMode RepeatPerSegment;  // const

    }

    // TypeToken: 0x2000038  // size: 0x14
    public sealed struct ParticleSystemForceFieldShape
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ParticleSystemForceFieldShape Sphere;  // const
        public static UnityEngine.ParticleSystemForceFieldShape Hemisphere;  // const
        public static UnityEngine.ParticleSystemForceFieldShape Cylinder;  // const
        public static UnityEngine.ParticleSystemForceFieldShape Box;  // const

    }

    // TypeToken: 0x2000039  // size: 0x14
    public sealed struct ParticleSystemVertexStreams
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ParticleSystemVertexStreams Position;  // const
        public static UnityEngine.ParticleSystemVertexStreams Normal;  // const
        public static UnityEngine.ParticleSystemVertexStreams Tangent;  // const
        public static UnityEngine.ParticleSystemVertexStreams Color;  // const
        public static UnityEngine.ParticleSystemVertexStreams UV;  // const
        public static UnityEngine.ParticleSystemVertexStreams UV2BlendAndFrame;  // const
        public static UnityEngine.ParticleSystemVertexStreams CenterAndVertexID;  // const
        public static UnityEngine.ParticleSystemVertexStreams Size;  // const
        public static UnityEngine.ParticleSystemVertexStreams Rotation;  // const
        public static UnityEngine.ParticleSystemVertexStreams Velocity;  // const
        public static UnityEngine.ParticleSystemVertexStreams Lifetime;  // const
        public static UnityEngine.ParticleSystemVertexStreams Custom1;  // const
        public static UnityEngine.ParticleSystemVertexStreams Custom2;  // const
        public static UnityEngine.ParticleSystemVertexStreams Random;  // const
        public static UnityEngine.ParticleSystemVertexStreams None;  // const
        public static UnityEngine.ParticleSystemVertexStreams All;  // const

    }

    // TypeToken: 0x200003A  // size: 0x18
    public sealed class ParticleSystemRenderer : UnityEngine.Renderer
    {
        // Properties
        UnityEngine.ParticleSystemRenderSpace alignment { get; /* RVA: 0x0B41A7A8 */ set; /* RVA: 0x0B41AD00 */ }
        UnityEngine.ParticleSystemRenderMode renderMode { get; /* RVA: 0x0B41AB94 */ set; /* RVA: 0x0B41B1A0 */ }
        UnityEngine.ParticleSystemMeshDistribution meshDistribution { get; /* RVA: 0x0B41AA20 */ set; /* RVA: 0x0B41AFB4 */ }
        UnityEngine.ParticleSystemSortMode sortMode { get; /* RVA: 0x0B41AC30 */ set; /* RVA: 0x0B41B268 */ }
        System.Single lengthScale { get; /* RVA: 0x0B41A950 */ set; /* RVA: 0x0B41AEEC */ }
        System.Single velocityScale { get; /* RVA: 0x0B41ACCC */ set; /* RVA: 0x0B41B32C */ }
        System.Single cameraVelocityScale { get; /* RVA: 0x0B41A810 */ set; /* RVA: 0x0B41AD84 */ }
        System.Single normalDirection { get; /* RVA: 0x0B41AABC */ set; /* RVA: 0x0B41B07C */ }
        System.Single shadowBias { get; /* RVA: 0x0B41ABFC */ set; /* RVA: 0x0B41B224 */ }
        System.Single minParticleSize { get; /* RVA: 0x0B41AA88 */ set; /* RVA: 0x0B41B038 */ }
        System.Single maxParticleSize { get; /* RVA: 0x0B41A9B8 */ set; /* RVA: 0x0B41AF70 */ }
        UnityEngine.Vector3 pivot { get; /* RVA: 0x0B41AB34 */ set; /* RVA: 0x0B41B148 */ }
        UnityEngine.Vector3 flip { get; /* RVA: 0x0B41A8F0 */ set; /* RVA: 0x0B41AE94 */ }
        UnityEngine.SpriteMaskInteraction maskInteraction { get; /* RVA: 0x0B41A984 */ set; /* RVA: 0x0B41AF30 */ }
        UnityEngine.Material trailMaterial { get; /* RVA: 0x0B41AC64 */ set; /* RVA: 0x0B41B2A8 */ }
        UnityEngine.Material oldTrailMaterial { set; /* RVA: 0x0B41B0C0 */ }
        System.Boolean enableGPUInstancing { get; /* RVA: 0x0B41A844 */ set; /* RVA: 0x0B41ADC8 */ }
        System.Boolean allowRoll { get; /* RVA: 0x0B41A7DC */ set; /* RVA: 0x0B41AD40 */ }
        System.Boolean freeformStretching { get; /* RVA: 0x0B41A91C */ set; /* RVA: 0x0B41AEA8 */ }
        System.Boolean rotateWithStretchDirection { get; /* RVA: 0x0B41ABC8 */ set; /* RVA: 0x0B41B1E0 */ }
        System.Boolean enableHGGPUInstancing { get; /* RVA: 0x0B41A878 */ set; /* RVA: 0x0B41AE0C */ }
        System.Boolean renderInUI { get; /* RVA: 0x0B41AB60 */ set; /* RVA: 0x0B41B15C */ }
        System.Int32 uiSortingOrder { get; /* RVA: 0x0B41AC98 */ set; /* RVA: 0x0B41B2EC */ }
        UnityEngine.Mesh mesh { get; /* RVA: 0x0B41AA54 */ set; /* RVA: 0x0B41AFF4 */ }
        System.Int32 meshCount { get; /* RVA: 0x0B41A9EC */ }
        System.Int32 activeVertexStreamsCount { get; /* RVA: 0x0B41A774 */ }
        System.Boolean DisableCutOutAnimation { get; /* RVA: 0x0B41A740 */ }
        System.Int32 CutOutMode { get; /* RVA: 0x0B41A70C */ }
        System.Int32 CutOutDataSize { get; /* RVA: 0x0B41A6D8 */ }

        // Methods
        // RVA: 0x0B419C8C  token: 0x600012E
        public System.Void EnableVertexStreams(UnityEngine.ParticleSystemVertexStreams streams) { }
        // RVA: 0x0B419C80  token: 0x600012F
        public System.Void DisableVertexStreams(UnityEngine.ParticleSystemVertexStreams streams) { }
        // RVA: 0x0B419B04  token: 0x6000130
        public System.Boolean AreVertexStreamsEnabled(UnityEngine.ParticleSystemVertexStreams streams) { }
        // RVA: 0x0B419CDC  token: 0x6000131
        public UnityEngine.ParticleSystemVertexStreams GetEnabledVertexStreams(UnityEngine.ParticleSystemVertexStreams streams) { }
        // RVA: 0x0B419FC8  token: 0x6000132
        private System.Void Internal_SetVertexStreams(UnityEngine.ParticleSystemVertexStreams streams, System.Boolean enabled) { }
        // RVA: 0x0B419D70  token: 0x6000133
        private UnityEngine.ParticleSystemVertexStreams Internal_GetEnabledVertexStreams(UnityEngine.ParticleSystemVertexStreams streams) { }
        // RVA: 0x0B419D2C  token: 0x6000163
        public System.Int32 GetMeshes(UnityEngine.Mesh[] meshes) { }
        // RVA: 0x0B41A664  token: 0x6000164
        public System.Void SetMeshes(UnityEngine.Mesh[] meshes, System.Int32 size) { }
        // RVA: 0x0B41A6B8  token: 0x6000165
        public System.Void SetMeshes(UnityEngine.Mesh[] meshes) { }
        // RVA: 0x0B419CE8  token: 0x6000166
        public System.Int32 GetMeshWeightings(System.Single[] weightings) { }
        // RVA: 0x0B41A5F0  token: 0x6000167
        public System.Void SetMeshWeightings(System.Single[] weightings, System.Int32 size) { }
        // RVA: 0x0B41A644  token: 0x6000168
        public System.Void SetMeshWeightings(System.Single[] weightings) { }
        // RVA: 0x0B419B84  token: 0x600016A
        public System.Void BakeMesh(UnityEngine.Mesh mesh, System.Boolean useTransform) { }
        // RVA: 0x0B419B20  token: 0x600016B
        public System.Void BakeMesh(UnityEngine.Mesh mesh, UnityEngine.Camera camera, System.Boolean useTransform) { }
        // RVA: 0x0B419BD0  token: 0x600016C
        public System.Void BakeTrailsMesh(UnityEngine.Mesh mesh, System.Boolean useTransform) { }
        // RVA: 0x0B419C1C  token: 0x600016D
        public System.Void BakeTrailsMesh(UnityEngine.Mesh mesh, UnityEngine.Camera camera, System.Boolean useTransform) { }
        // RVA: 0x0B41A5AC  token: 0x600016F
        public System.Void SetActiveVertexStreams(System.Collections.Generic.List<UnityEngine.ParticleSystemVertexStream> streams) { }
        // RVA: 0x0B419C98  token: 0x6000170
        public System.Void GetActiveVertexStreams(System.Collections.Generic.List<UnityEngine.ParticleSystemVertexStream> streams) { }
        // RVA: 0x05393520  token: 0x6000174
        public System.Void .ctor() { }
        // RVA: 0x0B41AAF0  token: 0x6000175
        private System.Void get_pivot_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x0B41B104  token: 0x6000176
        private System.Void set_pivot_Injected(UnityEngine.Vector3& value) { }
        // RVA: 0x0B41A8AC  token: 0x6000177
        private System.Void get_flip_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x0B41AE50  token: 0x6000178
        private System.Void set_flip_Injected(UnityEngine.Vector3& value) { }

    }

    // TypeToken: 0x200003B  // size: 0x38
    public sealed struct ParticleCollisionEvent
    {
        // Fields
        private UnityEngine.Vector3 m_Intersection;  // 0x10
        private UnityEngine.Vector3 m_Normal;  // 0x1c
        private UnityEngine.Vector3 m_Velocity;  // 0x28
        private System.Int32 m_ColliderInstanceID;  // 0x34

        // Properties
        UnityEngine.Vector3 intersection { get; /* RVA: 0x04DBA810 */ }
        UnityEngine.Vector3 normal { get; /* RVA: 0x04DBA7F0 */ }
        UnityEngine.Vector3 velocity { get; /* RVA: 0x04DBAC00 */ }
        UnityEngine.Component colliderComponent { get; /* RVA: 0x0B418EA8 */ }

        // Methods
        // RVA: 0x0B418E74  token: 0x600017D
        private static UnityEngine.Component InstanceIDToColliderComponent(System.Int32 instanceID) { }

    }

    // TypeToken: 0x200003C  // size: 0x10
    public class ParticleSystemExtensionsImpl
    {
        // Methods
        // RVA: 0x0B418F20  token: 0x600017E
        private static System.Int32 GetSafeCollisionEventSize(UnityEngine.ParticleSystem ps) { }
        // RVA: 0x0B418ECC  token: 0x600017F
        private static System.Int32 GetCollisionEvents(UnityEngine.ParticleSystem ps, UnityEngine.GameObject go, System.Collections.Generic.List<UnityEngine.ParticleCollisionEvent> collisionEvents) { }

    }

    // TypeToken: 0x200003D  // size: 0x18
    public class ParticleSystemForceField : UnityEngine.Behaviour
    {
        // Properties
        UnityEngine.ParticleSystemForceFieldShape shape { get; /* RVA: 0x0B4193EC */ set; /* RVA: 0x0B41998C */ }
        System.Single startRange { get; /* RVA: 0x0B419420 */ set; /* RVA: 0x0B4199CC */ }
        System.Single endRange { get; /* RVA: 0x0B419124 */ set; /* RVA: 0x0B4196D0 */ }
        System.Single length { get; /* RVA: 0x0B419200 */ set; /* RVA: 0x0B4197B0 */ }
        System.Single gravityFocus { get; /* RVA: 0x0B419158 */ set; /* RVA: 0x0B419714 */ }
        UnityEngine.Vector2 rotationRandomness { get; /* RVA: 0x0B419354 */ set; /* RVA: 0x0B419918 */ }
        System.Boolean multiplyDragByParticleSize { get; /* RVA: 0x0B419234 */ set; /* RVA: 0x0B4197F4 */ }
        System.Boolean multiplyDragByParticleVelocity { get; /* RVA: 0x0B419268 */ set; /* RVA: 0x0B419838 */ }
        UnityEngine.Texture3D vectorField { get; /* RVA: 0x0B41953C */ set; /* RVA: 0x0B419AC0 */ }
        UnityEngine.ParticleSystem.MinMaxCurve directionX { get; /* RVA: 0x0B418F98 */ set; /* RVA: 0x0B4195B4 */ }
        UnityEngine.ParticleSystem.MinMaxCurve directionY { get; /* RVA: 0x0B41900C */ set; /* RVA: 0x0B41960C */ }
        UnityEngine.ParticleSystem.MinMaxCurve directionZ { get; /* RVA: 0x0B419080 */ set; /* RVA: 0x0B419664 */ }
        UnityEngine.ParticleSystem.MinMaxCurve gravity { get; /* RVA: 0x0B4191D0 */ set; /* RVA: 0x0B41979C */ }
        UnityEngine.ParticleSystem.MinMaxCurve rotationSpeed { get; /* RVA: 0x0B4193BC */ set; /* RVA: 0x0B419978 */ }
        UnityEngine.ParticleSystem.MinMaxCurve rotationAttraction { get; /* RVA: 0x0B4192E0 */ set; /* RVA: 0x0B4198C0 */ }
        UnityEngine.ParticleSystem.MinMaxCurve drag { get; /* RVA: 0x0B4190F4 */ set; /* RVA: 0x0B4196BC */ }
        UnityEngine.ParticleSystem.MinMaxCurve vectorFieldSpeed { get; /* RVA: 0x0B41950C */ set; /* RVA: 0x0B419AAC */ }
        UnityEngine.ParticleSystem.MinMaxCurve vectorFieldAttraction { get; /* RVA: 0x0B419498 */ set; /* RVA: 0x0B419A54 */ }

        // Methods
        // RVA: 0x05393520  token: 0x60001A4
        public System.Void .ctor() { }
        // RVA: 0x0B419310  token: 0x60001A5
        private System.Void get_rotationRandomness_Injected(UnityEngine.Vector2& ret) { }
        // RVA: 0x0B4198D4  token: 0x60001A6
        private System.Void set_rotationRandomness_Injected(UnityEngine.Vector2& value) { }
        // RVA: 0x0B418F54  token: 0x60001A7
        private System.Void get_directionX_Injected(UnityEngine.ParticleSystem.MinMaxCurve& ret) { }
        // RVA: 0x0B419570  token: 0x60001A8
        private System.Void set_directionX_Injected(UnityEngine.ParticleSystem.MinMaxCurve& value) { }
        // RVA: 0x0B418FC8  token: 0x60001A9
        private System.Void get_directionY_Injected(UnityEngine.ParticleSystem.MinMaxCurve& ret) { }
        // RVA: 0x0B4195C8  token: 0x60001AA
        private System.Void set_directionY_Injected(UnityEngine.ParticleSystem.MinMaxCurve& value) { }
        // RVA: 0x0B41903C  token: 0x60001AB
        private System.Void get_directionZ_Injected(UnityEngine.ParticleSystem.MinMaxCurve& ret) { }
        // RVA: 0x0B419620  token: 0x60001AC
        private System.Void set_directionZ_Injected(UnityEngine.ParticleSystem.MinMaxCurve& value) { }
        // RVA: 0x0B41918C  token: 0x60001AD
        private System.Void get_gravity_Injected(UnityEngine.ParticleSystem.MinMaxCurve& ret) { }
        // RVA: 0x0B419758  token: 0x60001AE
        private System.Void set_gravity_Injected(UnityEngine.ParticleSystem.MinMaxCurve& value) { }
        // RVA: 0x0B419378  token: 0x60001AF
        private System.Void get_rotationSpeed_Injected(UnityEngine.ParticleSystem.MinMaxCurve& ret) { }
        // RVA: 0x0B419934  token: 0x60001B0
        private System.Void set_rotationSpeed_Injected(UnityEngine.ParticleSystem.MinMaxCurve& value) { }
        // RVA: 0x0B41929C  token: 0x60001B1
        private System.Void get_rotationAttraction_Injected(UnityEngine.ParticleSystem.MinMaxCurve& ret) { }
        // RVA: 0x0B41987C  token: 0x60001B2
        private System.Void set_rotationAttraction_Injected(UnityEngine.ParticleSystem.MinMaxCurve& value) { }
        // RVA: 0x0B4190B0  token: 0x60001B3
        private System.Void get_drag_Injected(UnityEngine.ParticleSystem.MinMaxCurve& ret) { }
        // RVA: 0x0B419678  token: 0x60001B4
        private System.Void set_drag_Injected(UnityEngine.ParticleSystem.MinMaxCurve& value) { }
        // RVA: 0x0B4194C8  token: 0x60001B5
        private System.Void get_vectorFieldSpeed_Injected(UnityEngine.ParticleSystem.MinMaxCurve& ret) { }
        // RVA: 0x0B419A68  token: 0x60001B6
        private System.Void set_vectorFieldSpeed_Injected(UnityEngine.ParticleSystem.MinMaxCurve& value) { }
        // RVA: 0x0B419454  token: 0x60001B7
        private System.Void get_vectorFieldAttraction_Injected(UnityEngine.ParticleSystem.MinMaxCurve& ret) { }
        // RVA: 0x0B419A10  token: 0x60001B8
        private System.Void set_vectorFieldAttraction_Injected(UnityEngine.ParticleSystem.MinMaxCurve& value) { }

    }

    // TypeToken: 0x200003E  // size: 0x18
    public sealed class HGParticleMeshInstanceRenderer : UnityEngine.Renderer
    {
    }

}

namespace UnityEngine.ParticleSystemJobs
{

    // TypeToken: 0x200003F  // size: 0x110
    public sealed struct NativeParticleData
    {
        // Fields
        private System.Int32 count;  // 0x10
        private UnityEngine.ParticleSystemJobs.NativeParticleData.Array3 positions;  // 0x18
        private UnityEngine.ParticleSystemJobs.NativeParticleData.Array3 velocities;  // 0x30
        private UnityEngine.ParticleSystemJobs.NativeParticleData.Array3 axisOfRotations;  // 0x48
        private UnityEngine.ParticleSystemJobs.NativeParticleData.Array3 rotations;  // 0x60
        private UnityEngine.ParticleSystemJobs.NativeParticleData.Array3 rotationalSpeeds;  // 0x78
        private UnityEngine.ParticleSystemJobs.NativeParticleData.Array3 sizes;  // 0x90
        private System.Void* startColors;  // 0xa8
        private System.Void* aliveTimePercent;  // 0xb0
        private System.Void* inverseStartLifetimes;  // 0xb8
        private System.Void* randomSeeds;  // 0xc0
        private UnityEngine.ParticleSystemJobs.NativeParticleData.Array4 customData1;  // 0xc8
        private UnityEngine.ParticleSystemJobs.NativeParticleData.Array4 customData2;  // 0xe8
        private System.Void* meshIndices;  // 0x108

    }

}

