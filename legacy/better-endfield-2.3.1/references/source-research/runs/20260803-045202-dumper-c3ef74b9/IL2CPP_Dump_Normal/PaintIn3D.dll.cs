// ========================================================
// Dumped by @desirepro
// Assembly: PaintIn3D.dll
// Classes:  164
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000008  // size: 0x30
    public class StringEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0A8F6090  token: 0x6000029
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x14
    public sealed struct ChannelType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dChannelCounterEvent.ChannelType Red;  // const
        public static PaintIn3D.P3dChannelCounterEvent.ChannelType Green;  // const
        public static PaintIn3D.P3dChannelCounterEvent.ChannelType Blue;  // const
        public static PaintIn3D.P3dChannelCounterEvent.ChannelType Alpha;  // const

    }

    // TypeToken: 0x200000C  // size: 0x14
    public sealed struct ChannelType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dChannelCounterFill.ChannelType Red;  // const
        public static PaintIn3D.P3dChannelCounterFill.ChannelType Green;  // const
        public static PaintIn3D.P3dChannelCounterFill.ChannelType Blue;  // const
        public static PaintIn3D.P3dChannelCounterFill.ChannelType Alpha;  // const

    }

    // TypeToken: 0x200000E  // size: 0x30
    public class StringEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0A8F6054  token: 0x600004B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000F  // size: 0x14
    public sealed struct ChannelType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dChannelCounterText.ChannelType Red;  // const
        public static PaintIn3D.P3dChannelCounterText.ChannelType Green;  // const
        public static PaintIn3D.P3dChannelCounterText.ChannelType Blue;  // const
        public static PaintIn3D.P3dChannelCounterText.ChannelType Alpha;  // const

    }

    // TypeToken: 0x2000013  // size: 0x30
    public class StringEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0A8F6018  token: 0x600006D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001B  // size: 0x14
    public sealed struct PhaseType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitBetween.PhaseType Update;  // const
        public static PaintIn3D.P3dHitBetween.PhaseType FixedUpdate;  // const

    }

    // TypeToken: 0x200001C  // size: 0x14
    public sealed struct OrientationType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitBetween.OrientationType WorldUp;  // const
        public static PaintIn3D.P3dHitBetween.OrientationType CameraUp;  // const
        public static PaintIn3D.P3dHitBetween.OrientationType ThisRotation;  // const
        public static PaintIn3D.P3dHitBetween.OrientationType ThisLocalRotation;  // const
        public static PaintIn3D.P3dHitBetween.OrientationType CustomRotation;  // const
        public static PaintIn3D.P3dHitBetween.OrientationType CustomLocalRotation;  // const

    }

    // TypeToken: 0x200001D  // size: 0x14
    public sealed struct NormalType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitBetween.NormalType HitNormal;  // const
        public static PaintIn3D.P3dHitBetween.NormalType RayDirection;  // const

    }

    // TypeToken: 0x200001E  // size: 0x14
    public sealed struct EmitType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitBetween.EmitType PointsIn3D;  // const
        public static PaintIn3D.P3dHitBetween.EmitType PointsOnUV;  // const
        public static PaintIn3D.P3dHitBetween.EmitType TrianglesIn3D;  // const

    }

    // TypeToken: 0x2000021  // size: 0x14
    public sealed struct EmitType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitCollisions.EmitType PointsIn3D;  // const
        public static PaintIn3D.P3dHitCollisions.EmitType PointsOnUV;  // const
        public static PaintIn3D.P3dHitCollisions.EmitType TrianglesIn3D;  // const

    }

    // TypeToken: 0x2000022  // size: 0x14
    public sealed struct OrientationType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitCollisions.OrientationType WorldUp;  // const
        public static PaintIn3D.P3dHitCollisions.OrientationType CameraUp;  // const

    }

    // TypeToken: 0x2000023  // size: 0x14
    public sealed struct PressureType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitCollisions.PressureType Constant;  // const
        public static PaintIn3D.P3dHitCollisions.PressureType ImpactSpeed;  // const

    }

    // TypeToken: 0x2000025  // size: 0x14
    public sealed struct PhaseType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitNearby.PhaseType ManuallyOnly;  // const
        public static PaintIn3D.P3dHitNearby.PhaseType Update;  // const
        public static PaintIn3D.P3dHitNearby.PhaseType FixedUpdate;  // const

    }

    // TypeToken: 0x2000027  // size: 0x14
    public sealed struct EmitType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitParticles.EmitType PointsIn3D;  // const
        public static PaintIn3D.P3dHitParticles.EmitType PointsOnUV;  // const
        public static PaintIn3D.P3dHitParticles.EmitType TrianglesIn3D;  // const

    }

    // TypeToken: 0x2000028  // size: 0x14
    public sealed struct OrientationType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitParticles.OrientationType WorldUp;  // const
        public static PaintIn3D.P3dHitParticles.OrientationType CameraUp;  // const

    }

    // TypeToken: 0x2000029  // size: 0x14
    public sealed struct NormalType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitParticles.NormalType ParticleVelocity;  // const
        public static PaintIn3D.P3dHitParticles.NormalType CollisionNormal;  // const

    }

    // TypeToken: 0x200002A  // size: 0x14
    public sealed struct PressureType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitParticles.PressureType Constant;  // const
        public static PaintIn3D.P3dHitParticles.PressureType Distance;  // const
        public static PaintIn3D.P3dHitParticles.PressureType Speed;  // const

    }

    // TypeToken: 0x200002C  // size: 0x40
    public class Link : Link
    {
        // Fields
        public System.Single Age;  // 0x18
        public System.Boolean Down;  // 0x1c
        public System.Int32 State;  // 0x20
        public System.Single Distance;  // 0x24
        public UnityEngine.Vector2 ScreenDelta;  // 0x28
        public UnityEngine.Vector2 ScreenOld;  // 0x30
        public System.Collections.Generic.List<UnityEngine.Vector2> History;  // 0x38

        // Methods
        // RVA: 0x0A8E34D0  token: 0x600016E
        public System.Void Move(UnityEngine.Vector2 screenNew) { }
        // RVA: 0x0A8E3508  token: 0x600016F
        private System.Boolean TryMove(UnityEngine.Vector2 screenNew) { }
        // RVA: 0x0A8E346C  token: 0x6000170
        public virtual System.Void Clear() { }
        // RVA: 0x0A8E3594  token: 0x6000171
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002D  // size: 0x14
    public sealed struct FrequencyType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitScreen.FrequencyType PixelInterval;  // const
        public static PaintIn3D.P3dHitScreen.FrequencyType ScaledPixelInterval;  // const
        public static PaintIn3D.P3dHitScreen.FrequencyType TimeInterval;  // const
        public static PaintIn3D.P3dHitScreen.FrequencyType OnceOnRelease;  // const
        public static PaintIn3D.P3dHitScreen.FrequencyType OnceOnPress;  // const
        public static PaintIn3D.P3dHitScreen.FrequencyType OnceEveryFrame;  // const

    }

    // TypeToken: 0x200002F  // size: 0x14
    public sealed struct RotationType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitScreenBase.RotationType Normal;  // const
        public static PaintIn3D.P3dHitScreenBase.RotationType World;  // const
        public static PaintIn3D.P3dHitScreenBase.RotationType ThisRotation;  // const
        public static PaintIn3D.P3dHitScreenBase.RotationType ThisLocalRotation;  // const
        public static PaintIn3D.P3dHitScreenBase.RotationType CustomRotation;  // const
        public static PaintIn3D.P3dHitScreenBase.RotationType CustomLocalRotation;  // const

    }

    // TypeToken: 0x2000030  // size: 0x14
    public sealed struct RelativeType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitScreenBase.RelativeType WorldUp;  // const
        public static PaintIn3D.P3dHitScreenBase.RelativeType CameraUp;  // const
        public static PaintIn3D.P3dHitScreenBase.RelativeType DrawAngle;  // const

    }

    // TypeToken: 0x2000031  // size: 0x14
    public sealed struct DirectionType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitScreenBase.DirectionType HitNormal;  // const
        public static PaintIn3D.P3dHitScreenBase.DirectionType RayDirection;  // const
        public static PaintIn3D.P3dHitScreenBase.DirectionType CameraDirection;  // const

    }

    // TypeToken: 0x2000032  // size: 0x14
    public sealed struct EmitType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitScreenBase.EmitType PointsIn3D;  // const
        public static PaintIn3D.P3dHitScreenBase.EmitType PointsOnUV;  // const
        public static PaintIn3D.P3dHitScreenBase.EmitType TrianglesIn3D;  // const

    }

    // TypeToken: 0x2000033  // size: 0x14
    public sealed struct ButtonTypes
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitScreenBase.ButtonTypes LeftMouse;  // const
        public static PaintIn3D.P3dHitScreenBase.ButtonTypes RightMouse;  // const
        public static PaintIn3D.P3dHitScreenBase.ButtonTypes MiddleMouse;  // const
        public static PaintIn3D.P3dHitScreenBase.ButtonTypes Touch;  // const

    }

    // TypeToken: 0x2000036  // size: 0x14
    public sealed struct FrequencyType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitScreenLine.FrequencyType StartAndEnd;  // const
        public static PaintIn3D.P3dHitScreenLine.FrequencyType PixelInterval;  // const
        public static PaintIn3D.P3dHitScreenLine.FrequencyType ScaledPixelInterval;  // const
        public static PaintIn3D.P3dHitScreenLine.FrequencyType StretchedPixelInterval;  // const
        public static PaintIn3D.P3dHitScreenLine.FrequencyType StretchedScaledPixelInterval;  // const
        public static PaintIn3D.P3dHitScreenLine.FrequencyType Once;  // const

    }

    // TypeToken: 0x2000038  // size: 0x14
    public sealed struct PhaseType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitThrough.PhaseType Update;  // const
        public static PaintIn3D.P3dHitThrough.PhaseType FixedUpdate;  // const

    }

    // TypeToken: 0x2000039  // size: 0x14
    public sealed struct OrientationType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dHitThrough.OrientationType WorldUp;  // const
        public static PaintIn3D.P3dHitThrough.OrientationType CameraUp;  // const

    }

    // TypeToken: 0x2000046  // size: 0x20
    public class Pair
    {
        // Fields
        public UnityEngine.Mesh Source;  // 0x10
        public UnityEngine.Mesh Output;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600020B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000047  // size: 0x18
    public class Ring
    {
        // Fields
        public System.Collections.Generic.List<PaintIn3D.P3dSeamFixer.Point> Points;  // 0x10

        // Methods
        // RVA: 0x0A8F5F20  token: 0x600020C
        public PaintIn3D.P3dSeamFixer.Point GetPoint(System.Int32 index) { }
        // RVA: 0x0A8F5FA0  token: 0x600020D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000048  // size: 0x30
    public class Edge
    {
        // Fields
        public System.Boolean Used;  // 0x10
        public PaintIn3D.P3dSeamFixer.Point PointA;  // 0x18
        public PaintIn3D.P3dSeamFixer.Point PointB;  // 0x20
        public System.Boolean Flip;  // 0x28

        // Methods
        // RVA: 0x0A8E344C  token: 0x600020E
        public System.Boolean Match(PaintIn3D.P3dSeamFixer.Point a, PaintIn3D.P3dSeamFixer.Point b) { }
        // RVA: 0x041E1670  token: 0x600020F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000049  // size: 0x30
    public class Point
    {
        // Fields
        public System.Int32 Index;  // 0x10
        public UnityEngine.Vector2 Coord;  // 0x14
        public PaintIn3D.P3dSeamFixer.Insertion Outer;  // 0x20
        public System.Collections.Generic.List<PaintIn3D.P3dSeamFixer.Edge> Edges;  // 0x28

        // Methods
        // RVA: 0x0A8F5EA8  token: 0x6000210
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004A  // size: 0x20
    public class Insertion
    {
        // Fields
        public System.Int32 Index;  // 0x10
        public System.Int32 NewIndex;  // 0x14
        public UnityEngine.Vector2 NewCoord;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000211
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004E  // size: 0x14
    public sealed struct BlendType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dModifyAngleRandom.BlendType Replace;  // const
        public static PaintIn3D.P3dModifyAngleRandom.BlendType Multiply;  // const
        public static PaintIn3D.P3dModifyAngleRandom.BlendType Increment;  // const

    }

    // TypeToken: 0x2000050  // size: 0x14
    public sealed struct BlendType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dModifyColorRandom.BlendType Replace;  // const
        public static PaintIn3D.P3dModifyColorRandom.BlendType Multiply;  // const
        public static PaintIn3D.P3dModifyColorRandom.BlendType Increment;  // const

    }

    // TypeToken: 0x2000052  // size: 0x14
    public sealed struct BlendType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dModifyHardnessPressure.BlendType Replace;  // const
        public static PaintIn3D.P3dModifyHardnessPressure.BlendType Multiply;  // const
        public static PaintIn3D.P3dModifyHardnessPressure.BlendType Increment;  // const

    }

    // TypeToken: 0x2000054  // size: 0x14
    public sealed struct BlendType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dModifyHardnessRandom.BlendType Replace;  // const
        public static PaintIn3D.P3dModifyHardnessRandom.BlendType Multiply;  // const
        public static PaintIn3D.P3dModifyHardnessRandom.BlendType Increment;  // const

    }

    // TypeToken: 0x2000056  // size: 0x14
    public sealed struct BlendType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dModifyOpacityPressure.BlendType Replace;  // const
        public static PaintIn3D.P3dModifyOpacityPressure.BlendType Multiply;  // const
        public static PaintIn3D.P3dModifyOpacityPressure.BlendType Increment;  // const

    }

    // TypeToken: 0x2000058  // size: 0x14
    public sealed struct BlendType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dModifyOpacityRandom.BlendType Replace;  // const
        public static PaintIn3D.P3dModifyOpacityRandom.BlendType Multiply;  // const
        public static PaintIn3D.P3dModifyOpacityRandom.BlendType Increment;  // const

    }

    // TypeToken: 0x200005B  // size: 0x14
    public sealed struct BlendType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dModifyRadiusPressure.BlendType Replace;  // const
        public static PaintIn3D.P3dModifyRadiusPressure.BlendType Multiply;  // const
        public static PaintIn3D.P3dModifyRadiusPressure.BlendType Increment;  // const

    }

    // TypeToken: 0x200005D  // size: 0x14
    public sealed struct BlendType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dModifyRadiusRandom.BlendType Replace;  // const
        public static PaintIn3D.P3dModifyRadiusRandom.BlendType Multiply;  // const
        public static PaintIn3D.P3dModifyRadiusRandom.BlendType Increment;  // const

    }

    // TypeToken: 0x200005F  // size: 0x14
    public sealed struct BlendType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dModifyScaleRandom.BlendType Replace;  // const
        public static PaintIn3D.P3dModifyScaleRandom.BlendType Multiply;  // const
        public static PaintIn3D.P3dModifyScaleRandom.BlendType Increment;  // const

    }

    // TypeToken: 0x200006A  // size: 0x20
    public class Contribution
    {
        // Fields
        public PaintIn3D.P3dColorCounter Counter;  // 0x10
        public System.Int32 Solid;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60002E9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200006C  // size: 0x28
    public class Contribution
    {
        // Fields
        public PaintIn3D.P3dColor Color;  // 0x10
        public System.Int32 Count;  // 0x18
        public System.Single Ratio;  // 0x1c
        public System.Byte R;  // 0x20
        public System.Byte G;  // 0x21
        public System.Byte B;  // 0x22
        public System.Byte A;  // 0x23
        public static System.Collections.Generic.Stack<PaintIn3D.P3dColorCounter.Contribution> Pool;  // static @ 0x0

        // Methods
        // RVA: 0x041E1670  token: 0x60002FA
        public System.Void .ctor() { }
        // RVA: 0x0A8F61A8  token: 0x60002FB
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200006F  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly PaintIn3D.P3dCommon.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x0A90C284  token: 0x6000336
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000337
        public System.Void .ctor() { }
        // RVA: 0x0A90C194  token: 0x6000338
        private System.Void <.cctor>b__5_0(UnityEngine.Camera camera) { }
        // RVA: 0x0A90C20C  token: 0x6000339
        private System.Void <.cctor>b__5_1(UnityEngine.Rendering.ScriptableRenderContext context, UnityEngine.Camera camera) { }

    }

    // TypeToken: 0x2000072  // size: 0x14
    public sealed struct Coord
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dCoordCopier.Coord First;  // const
        public static PaintIn3D.P3dCoordCopier.Coord Second;  // const
        public static PaintIn3D.P3dCoordCopier.Coord Third;  // const
        public static PaintIn3D.P3dCoordCopier.Coord Fourth;  // const
        public static PaintIn3D.P3dCoordCopier.Coord None;  // const

    }

    // TypeToken: 0x2000076  // size: 0x50
    public class TextureData
    {
        // Fields
        public System.String Name;  // 0x10
        public PaintIn3D.P3dBlendMode BlendMode;  // 0x18

        // Methods
        // RVA: 0x0A90C0FC  token: 0x600036C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000077  // size: 0x20
    public class Entry
    {
        // Fields
        public System.String Path;  // 0x10
        public System.String Property;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600036D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007E  // size: 0x38
    public class Link
    {
        // Fields
        public System.Object Owner;  // 0x10
        public UnityEngine.Vector3 Position;  // 0x18
        public UnityEngine.Vector3 EndPosition;  // 0x24
        public System.Single Age;  // 0x30
        public System.Boolean Preview;  // 0x34

        // Methods
        // RVA: 0x041E1670  token: 0x600038D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000082  // size: 0x28
    public class MeshData
    {
        // Fields
        private UnityEngine.Vector3[] positions;  // 0x10
        private System.Int32[] indices;  // 0x18
        private System.Int32 total;  // 0x20

        // Methods
        // RVA: 0x0A8F63E8  token: 0x60003A9
        public System.Void Update(UnityEngine.Mesh mesh) { }
        // RVA: 0x0A8F6234  token: 0x60003AA
        public System.Boolean GetTrianglePositions(UnityEngine.RaycastHit hit, UnityEngine.Vector3& positionA, UnityEngine.Vector3& positionB, UnityEngine.Vector3& positionC) { }
        // RVA: 0x041E1670  token: 0x60003AB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000084  // size: 0x14
    public sealed struct UseMeshType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dModel.UseMeshType AsIs;  // const
        public static PaintIn3D.P3dModel.UseMeshType AutoSeamFix;  // const

    }

    // TypeToken: 0x2000088  // size: 0x14
    public sealed struct ActivationType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dPaintable.ActivationType Awake;  // const
        public static PaintIn3D.P3dPaintable.ActivationType OnEnable;  // const
        public static PaintIn3D.P3dPaintable.ActivationType Start;  // const
        public static PaintIn3D.P3dPaintable.ActivationType OnFirstUse;  // const

    }

    // TypeToken: 0x200008C  // size: 0x14
    public sealed struct UndoRedoType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dPaintableTexture.UndoRedoType None;  // const
        public static PaintIn3D.P3dPaintableTexture.UndoRedoType FullTextureCopy;  // const
        public static PaintIn3D.P3dPaintableTexture.UndoRedoType LocalCommandCopy;  // const

    }

    // TypeToken: 0x200008D  // size: 0x14
    public sealed struct SaveLoadType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dPaintableTexture.SaveLoadType Manual;  // const
        public static PaintIn3D.P3dPaintableTexture.SaveLoadType Automatic;  // const
        public static PaintIn3D.P3dPaintableTexture.SaveLoadType SemiManual;  // const

    }

    // TypeToken: 0x200008E  // size: 0x14
    public sealed struct MipType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dPaintableTexture.MipType Auto;  // const
        public static PaintIn3D.P3dPaintableTexture.MipType On;  // const
        public static PaintIn3D.P3dPaintableTexture.MipType Off;  // const

    }

    // TypeToken: 0x200008F  // size: 0x14
    public sealed struct FilterType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dPaintableTexture.FilterType Auto;  // const
        public static PaintIn3D.P3dPaintableTexture.FilterType Point;  // const
        public static PaintIn3D.P3dPaintableTexture.FilterType Bilinear;  // const
        public static PaintIn3D.P3dPaintableTexture.FilterType Trilinear;  // const

    }

    // TypeToken: 0x2000090  // size: 0x14
    public sealed struct AnisoType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dPaintableTexture.AnisoType Auto;  // const
        public static PaintIn3D.P3dPaintableTexture.AnisoType Off;  // const
        public static PaintIn3D.P3dPaintableTexture.AnisoType One;  // const
        public static PaintIn3D.P3dPaintableTexture.AnisoType Four;  // const
        public static PaintIn3D.P3dPaintableTexture.AnisoType Eight;  // const
        public static PaintIn3D.P3dPaintableTexture.AnisoType Sixteen;  // const

    }

    // TypeToken: 0x2000091  // size: 0x14
    public sealed struct WrapType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dPaintableTexture.WrapType Auto;  // const
        public static PaintIn3D.P3dPaintableTexture.WrapType Repeat;  // const
        public static PaintIn3D.P3dPaintableTexture.WrapType Clamp;  // const
        public static PaintIn3D.P3dPaintableTexture.WrapType Mirror;  // const
        public static PaintIn3D.P3dPaintableTexture.WrapType MirrorOnce;  // const

    }

    // TypeToken: 0x2000092  // size: 0x14
    public sealed struct ExistingType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dPaintableTexture.ExistingType Ignore;  // const
        public static PaintIn3D.P3dPaintableTexture.ExistingType Use;  // const
        public static PaintIn3D.P3dPaintableTexture.ExistingType UseAndKeep;  // const

    }

    // TypeToken: 0x2000093  // size: 0x14
    public sealed struct ConversionType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dPaintableTexture.ConversionType None;  // const
        public static PaintIn3D.P3dPaintableTexture.ConversionType Normal;  // const
        public static PaintIn3D.P3dPaintableTexture.ConversionType Premultiply;  // const

    }

    // TypeToken: 0x2000094  // size: 0x30
    public class PaintableTextureEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0A90C0C0  token: 0x600048C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000098  // size: 0x50
    public class Link
    {
        // Fields
        public System.Object Owner;  // 0x10
        public UnityEngine.Vector3 Position;  // 0x18
        public System.Single Age;  // 0x24
        public System.Boolean Preview;  // 0x28
        public UnityEngine.Vector3 LastPosition;  // 0x2c
        public System.Single LastPressure;  // 0x38
        public System.Int32 LastPriority;  // 0x3c
        public UnityEngine.Quaternion LastRotation;  // 0x40

        // Methods
        // RVA: 0x041E1670  token: 0x60004C8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200009A  // size: 0x30
    public class ColorEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0A8F616C  token: 0x60004D2
        public System.Void .ctor() { }

    }

namespace PaintIn3D
{

    // TypeToken: 0x2000002  // size: 0x20
    public class P3dButtonClearAll : UnityEngine.MonoBehaviour, UnityEngine.EventSystems.IPointerClickHandler, UnityEngine.EventSystems.IEventSystemHandler
    {
        // Fields
        private System.Boolean clearStates;  // 0x18

        // Properties
        System.Boolean ClearStates { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x04D86350 */ }

        // Methods
        // RVA: 0x0A8E4350  token: 0x6000003
        public virtual System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0A8E41C4  token: 0x6000004
        public System.Void ClearAll() { }
        // RVA: 0x05394740  token: 0x6000005
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000003  // size: 0x18
    public class P3dButtonRedoAll : UnityEngine.MonoBehaviour, UnityEngine.EventSystems.IPointerClickHandler, UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: 0x0A8E4358  token: 0x6000006
        public virtual System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0A8E4358  token: 0x6000007
        public System.Void RedoAll() { }
        // RVA: 0x0A8E4360  token: 0x6000008
        protected virtual System.Void Update() { }
        // RVA: 0x05393520  token: 0x6000009
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000004  // size: 0x18
    public class P3dButtonUndoAll : UnityEngine.MonoBehaviour, UnityEngine.EventSystems.IPointerClickHandler, UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: 0x0A8E43FC  token: 0x600000A
        public virtual System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0A8E43FC  token: 0x600000B
        public System.Void UndoAll() { }
        // RVA: 0x0A8E4404  token: 0x600000C
        protected virtual System.Void Update() { }
        // RVA: 0x05393520  token: 0x600000D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000005  // size: 0x40
    public class P3dChangeCounterEvent : UnityEngine.MonoBehaviour
    {
        // Fields
        private System.Collections.Generic.List<PaintIn3D.P3dChangeCounter> counters;  // 0x18
        private UnityEngine.Vector2 range;  // 0x20
        private System.Boolean inside;  // 0x28
        private UnityEngine.Events.UnityEvent onInside;  // 0x30
        private UnityEngine.Events.UnityEvent onOutside;  // 0x38

        // Properties
        System.Collections.Generic.List<PaintIn3D.P3dChangeCounter> Counters { get; /* RVA: 0x0A8E4558 */ }
        UnityEngine.Vector2 Range { get; /* RVA: 0x04DA5EA0 */ set; /* RVA: 0x04D86500 */ }
        System.Boolean Inside { get; /* RVA: 0x011F33C0 */ set; /* RVA: 0x011F33D0 */ }
        UnityEngine.Events.UnityEvent OnInside { get; /* RVA: 0x0A8E45DC */ }
        UnityEngine.Events.UnityEvent OnOutside { get; /* RVA: 0x0A8E464C */ }
        System.Single Ratio { get; /* RVA: 0x0A8E46BC */ }

        // Methods
        // RVA: 0x0A8E4510  token: 0x6000016
        protected virtual System.Void Update() { }
        // RVA: 0x0A8E44A0  token: 0x6000017
        private System.Void UpdateInside(System.Single ratio) { }
        // RVA: 0x0A8E4534  token: 0x6000018
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x30
    public class P3dChangeCounterFill : UnityEngine.MonoBehaviour
    {
        // Fields
        private System.Collections.Generic.List<PaintIn3D.P3dChangeCounter> counters;  // 0x18
        private System.Boolean inverse;  // 0x20
        private UnityEngine.UI.Image cachedImage;  // 0x28

        // Properties
        System.Collections.Generic.List<PaintIn3D.P3dChangeCounter> Counters { get; /* RVA: 0x0A8E480C */ }
        System.Boolean Inverse { get; /* RVA: 0x04D866B0 */ set; /* RVA: 0x04D866D0 */ }

        // Methods
        // RVA: 0x0A8E4724  token: 0x600001C
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A8E476C  token: 0x600001D
        protected virtual System.Void Update() { }
        // RVA: 0x05393520  token: 0x600001E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x38
    public class P3dChangeCounterText : UnityEngine.MonoBehaviour
    {
        // Fields
        private System.Collections.Generic.List<PaintIn3D.P3dChangeCounter> counters;  // 0x18
        private System.Boolean inverse;  // 0x20
        private System.Int32 decimalPlaces;  // 0x24
        private System.String format;  // 0x28
        private PaintIn3D.P3dChangeCounterText.StringEvent onString;  // 0x30

        // Properties
        System.Collections.Generic.List<PaintIn3D.P3dChangeCounter> Counters { get; /* RVA: 0x0A8E4AB8 */ }
        System.Boolean Inverse { get; /* RVA: 0x04D866B0 */ set; /* RVA: 0x04D866D0 */ }
        System.Int32 DecimalPlaces { get; /* RVA: 0x011F0020 */ set; /* RVA: 0x011F0030 */ }
        System.String Format { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        PaintIn3D.P3dChangeCounterText.StringEvent OnString { get; /* RVA: 0x0A8E4B3C */ }

        // Methods
        // RVA: 0x0A8E4890  token: 0x6000027
        protected virtual System.Void Update() { }
        // RVA: 0x0A8E4A5C  token: 0x6000028
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x40
    public class P3dChannelCounterEvent : UnityEngine.MonoBehaviour
    {
        // Fields
        private System.Collections.Generic.List<PaintIn3D.P3dChannelCounter> counters;  // 0x18
        private PaintIn3D.P3dChannelCounterEvent.ChannelType channel;  // 0x20
        private UnityEngine.Vector2 range;  // 0x24
        private System.Boolean inside;  // 0x2c
        private UnityEngine.Events.UnityEvent onInside;  // 0x30
        private UnityEngine.Events.UnityEvent onOutside;  // 0x38

        // Properties
        System.Collections.Generic.List<PaintIn3D.P3dChannelCounter> Counters { get; /* RVA: 0x0A8E4C64 */ }
        PaintIn3D.P3dChannelCounterEvent.ChannelType Channel { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }
        UnityEngine.Vector2 Range { get; /* RVA: 0x04D88060 */ set; /* RVA: 0x04DA5EC0 */ }
        System.Boolean Inside { get; /* RVA: 0x04D869F0 */ set; /* RVA: 0x04D86A00 */ }
        UnityEngine.Events.UnityEvent OnInside { get; /* RVA: 0x0A8E4CE8 */ }
        UnityEngine.Events.UnityEvent OnOutside { get; /* RVA: 0x0A8E4D58 */ }
        System.Single Ratio { get; /* RVA: 0x0A8E4DC8 */ }

        // Methods
        // RVA: 0x0A8E4C1C  token: 0x6000034
        protected virtual System.Void Update() { }
        // RVA: 0x0A8E4BAC  token: 0x6000035
        private System.Void UpdateInside(System.Single ratio) { }
        // RVA: 0x0A8E4C40  token: 0x6000036
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000B  // size: 0x30
    public class P3dChannelCounterFill : UnityEngine.MonoBehaviour
    {
        // Fields
        private System.Collections.Generic.List<PaintIn3D.P3dChannelCounter> counters;  // 0x18
        private PaintIn3D.P3dChannelCounterFill.ChannelType channel;  // 0x20
        private System.Boolean inverse;  // 0x24
        private UnityEngine.UI.Image cachedImage;  // 0x28

        // Properties
        System.Collections.Generic.List<PaintIn3D.P3dChannelCounter> Counters { get; /* RVA: 0x0A8E4FF0 */ }
        PaintIn3D.P3dChannelCounterFill.ChannelType Channel { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }
        System.Boolean Inverse { get; /* RVA: 0x04D86870 */ set; /* RVA: 0x04D86880 */ }

        // Methods
        // RVA: 0x0A8E4E9C  token: 0x600003C
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A8E4EE4  token: 0x600003D
        protected virtual System.Void Update() { }
        // RVA: 0x05393520  token: 0x600003E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000D  // size: 0x40
    public class P3dChannelCounterText : UnityEngine.MonoBehaviour
    {
        // Fields
        private System.Collections.Generic.List<PaintIn3D.P3dChannelCounter> counters;  // 0x18
        private PaintIn3D.P3dChannelCounterText.ChannelType channel;  // 0x20
        private System.Boolean inverse;  // 0x24
        private System.Int32 decimalPlaces;  // 0x28
        private System.String format;  // 0x30
        private PaintIn3D.P3dChannelCounterText.StringEvent onString;  // 0x38

        // Properties
        System.Collections.Generic.List<PaintIn3D.P3dChannelCounter> Counters { get; /* RVA: 0x0A8E530C */ }
        PaintIn3D.P3dChannelCounterText.ChannelType Channel { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }
        System.Boolean Inverse { get; /* RVA: 0x04D86870 */ set; /* RVA: 0x04D86880 */ }
        System.Int32 DecimalPlaces { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }
        System.String Format { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        PaintIn3D.P3dChannelCounterText.StringEvent OnString { get; /* RVA: 0x0A8E5390 */ }

        // Methods
        // RVA: 0x0A8E5074  token: 0x6000049
        protected virtual System.Void Update() { }
        // RVA: 0x0A8E52B0  token: 0x600004A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000010  // size: 0x48
    public class P3dColorCounterEvent : UnityEngine.MonoBehaviour
    {
        // Fields
        private System.Collections.Generic.List<PaintIn3D.P3dColorCounter> counters;  // 0x18
        private PaintIn3D.P3dColor color;  // 0x20
        private UnityEngine.Vector2 range;  // 0x28
        private System.Boolean inside;  // 0x30
        private UnityEngine.Events.UnityEvent onInside;  // 0x38
        private UnityEngine.Events.UnityEvent onOutside;  // 0x40

        // Properties
        System.Collections.Generic.List<PaintIn3D.P3dColorCounter> Counters { get; /* RVA: 0x0A8E54B8 */ }
        PaintIn3D.P3dColor Color { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        UnityEngine.Vector2 Range { get; /* RVA: 0x04D9E6E0 */ set; /* RVA: 0x04D86520 */ }
        System.Boolean Inside { get; /* RVA: 0x04D865F0 */ set; /* RVA: 0x04D86620 */ }
        UnityEngine.Events.UnityEvent OnInside { get; /* RVA: 0x0A8E553C */ }
        UnityEngine.Events.UnityEvent OnOutside { get; /* RVA: 0x0A8E55AC */ }
        System.Single Ratio { get; /* RVA: 0x0A8E561C */ }

        // Methods
        // RVA: 0x0A8E5470  token: 0x6000056
        protected virtual System.Void Update() { }
        // RVA: 0x0A8E5400  token: 0x6000057
        private System.Void UpdateInside(System.Single ratio) { }
        // RVA: 0x0A8E5494  token: 0x6000058
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000011  // size: 0x38
    public class P3dColorCounterFill : UnityEngine.MonoBehaviour
    {
        // Fields
        private System.Collections.Generic.List<PaintIn3D.P3dColorCounter> counters;  // 0x18
        private PaintIn3D.P3dColor color;  // 0x20
        private System.Boolean inverse;  // 0x28
        private UnityEngine.UI.Image cachedImage;  // 0x30

        // Properties
        System.Collections.Generic.List<PaintIn3D.P3dColorCounter> Counters { get; /* RVA: 0x0A8E578C */ }
        PaintIn3D.P3dColor Color { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.Boolean Inverse { get; /* RVA: 0x011F33C0 */ set; /* RVA: 0x011F33D0 */ }

        // Methods
        // RVA: 0x0A8E5690  token: 0x600005E
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A8E56D8  token: 0x600005F
        protected virtual System.Void Update() { }
        // RVA: 0x05393520  token: 0x6000060
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000012  // size: 0x40
    public class P3dColorCounterText : UnityEngine.MonoBehaviour
    {
        // Fields
        private System.Collections.Generic.List<PaintIn3D.P3dColorCounter> counters;  // 0x18
        private PaintIn3D.P3dColor color;  // 0x20
        private System.Boolean inverse;  // 0x28
        private System.Int32 decimalPlaces;  // 0x2c
        private System.String format;  // 0x30
        private PaintIn3D.P3dColorCounterText.StringEvent onString;  // 0x38

        // Properties
        System.Collections.Generic.List<PaintIn3D.P3dColorCounter> Counters { get; /* RVA: 0x0A8E5A3C */ }
        PaintIn3D.P3dColor Color { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.Boolean Inverse { get; /* RVA: 0x011F33C0 */ set; /* RVA: 0x011F33D0 */ }
        System.Int32 DecimalPlaces { get; /* RVA: 0x04D86710 */ set; /* RVA: 0x04D86720 */ }
        System.String Format { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        PaintIn3D.P3dColorCounterText.StringEvent OnString { get; /* RVA: 0x0A8E5AC0 */ }

        // Methods
        // RVA: 0x0A8E5810  token: 0x600006B
        protected virtual System.Void Update() { }
        // RVA: 0x0A8E59E0  token: 0x600006C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000014  // size: 0x30
    public class P3dPaintDebug : UnityEngine.MonoBehaviour, PaintIn3D.IHitPoint, PaintIn3D.IHit, PaintIn3D.IHitLine, PaintIn3D.IHitTriangle, PaintIn3D.IHitQuad
    {
        // Fields
        private UnityEngine.Color color;  // 0x18
        private System.Single duration;  // 0x28
        private System.Single size;  // 0x2c

        // Properties
        UnityEngine.Color Color { get; /* RVA: 0x04D890C0 */ set; /* RVA: 0x04D890D0 */ }
        System.Single Duration { get; /* RVA: 0x04D86BF0 */ set; /* RVA: 0x04D86C00 */ }
        System.Single Size { get; /* RVA: 0x04D86AF0 */ set; /* RVA: 0x04D86B00 */ }

        // Methods
        // RVA: 0x0A8F2094  token: 0x6000074
        public virtual System.Void HandleHitPoint(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0A8F1F74  token: 0x6000075
        public virtual System.Void HandleHitLine(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Quaternion rotation, System.Boolean clip) { }
        // RVA: 0x0A8F2474  token: 0x6000076
        public virtual System.Void HandleHitTriangle(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 positionA, UnityEngine.Vector3 positionB, UnityEngine.Vector3 positionC, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0A8F223C  token: 0x6000077
        public virtual System.Void HandleHitQuad(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Vector3 position2, UnityEngine.Vector3 endPosition2, UnityEngine.Quaternion rotation, System.Boolean clip) { }
        // RVA: 0x0A8F1F30  token: 0x6000078
        private UnityEngine.Color GetColor(System.Boolean preview, System.Single pressure, UnityEngine.Color color) { }
        // RVA: 0x0A8F1A68  token: 0x6000079
        private System.Void DrawArrow(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Color tint) { }
        // RVA: 0x0A8F2698  token: 0x600007A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000015  // size: 0x258
    public class P3dCommandDecal : PaintIn3D.P3dCommand
    {
        // Fields
        public PaintIn3D.P3dBlendMode Blend;  // 0x70
        public System.Boolean In3D;  // 0xa8
        public UnityEngine.Vector3 Position;  // 0xac
        public UnityEngine.Vector3 EndPosition;  // 0xb8
        public UnityEngine.Vector3 Position2;  // 0xc4
        public UnityEngine.Vector3 EndPosition2;  // 0xd0
        public System.Int32 Extrusions;  // 0xdc
        public System.Boolean Clip;  // 0xe0
        public UnityEngine.Matrix4x4 Matrix;  // 0xe4
        public UnityEngine.Vector3 Direction;  // 0x124
        public UnityEngine.Color Color;  // 0x130
        public System.Single Opacity;  // 0x140
        public System.Single Hardness;  // 0x144
        public System.Single Wrapping;  // 0x148
        public PaintIn3D.P3dHashedTexture Texture;  // 0x150
        public PaintIn3D.P3dHashedTexture Shape;  // 0x160
        public UnityEngine.Vector4 ShapeChannel;  // 0x170
        public UnityEngine.Vector2 NormalFront;  // 0x180
        public UnityEngine.Vector2 NormalBack;  // 0x188
        public PaintIn3D.P3dHashedTexture TileTexture;  // 0x190
        public UnityEngine.Matrix4x4 TileMatrix;  // 0x1a0
        public System.Single TileOpacity;  // 0x1e0
        public System.Single TileTransition;  // 0x1e4
        public UnityEngine.Matrix4x4 MaskMatrix;  // 0x1e8
        public PaintIn3D.P3dHashedTexture MaskShape;  // 0x228
        public UnityEngine.Vector4 MaskChannel;  // 0x238
        public UnityEngine.Vector3 MaskStretch;  // 0x248
        public static PaintIn3D.P3dCommandDecal Instance;  // static @ 0x0
        private static System.Collections.Generic.Stack<PaintIn3D.P3dCommandDecal> pool;  // static @ 0x8
        private static UnityEngine.Material cachedSpotMaterial;  // static @ 0x10
        private static UnityEngine.Material cachedLineMaterial;  // static @ 0x18
        private static UnityEngine.Material cachedQuadMaterial;  // static @ 0x20
        private static UnityEngine.Material cachedLineClipMaterial;  // static @ 0x28
        private static UnityEngine.Material cachedQuadClipMaterial;  // static @ 0x30
        private static System.Int32 cachedSpotMaterialHash;  // static @ 0x38
        private static System.Int32 cachedLineMaterialHash;  // static @ 0x3c
        private static System.Int32 cachedQuadMaterialHash;  // static @ 0x40
        private static System.Int32 cachedLineClipMaterialHash;  // static @ 0x44
        private static System.Int32 cachedQuadClipMaterialHash;  // static @ 0x48
        private static System.Int32 _In3D;  // static @ 0x4c
        private static System.Int32 _Position;  // static @ 0x50
        private static System.Int32 _EndPosition;  // static @ 0x54
        private static System.Int32 _Position2;  // static @ 0x58
        private static System.Int32 _EndPosition2;  // static @ 0x5c
        private static System.Int32 _Matrix;  // static @ 0x60
        private static System.Int32 _Direction;  // static @ 0x64
        private static System.Int32 _Color;  // static @ 0x68
        private static System.Int32 _Opacity;  // static @ 0x6c
        private static System.Int32 _Hardness;  // static @ 0x70
        private static System.Int32 _Wrapping;  // static @ 0x74
        private static System.Int32 _Texture;  // static @ 0x78
        private static System.Int32 _Shape;  // static @ 0x7c
        private static System.Int32 _ShapeChannel;  // static @ 0x80
        private static System.Int32 _NormalFront;  // static @ 0x84
        private static System.Int32 _NormalBack;  // static @ 0x88
        private static System.Int32 _TileTexture;  // static @ 0x8c
        private static System.Int32 _TileMatrix;  // static @ 0x90
        private static System.Int32 _TileOpacity;  // static @ 0x94
        private static System.Int32 _TileTransition;  // static @ 0x98
        private static System.Int32 _MaskMatrix;  // static @ 0x9c
        private static System.Int32 _MaskTexture;  // static @ 0xa0
        private static System.Int32 _MaskChannel;  // static @ 0xa4
        private static System.Int32 _MaskStretch;  // static @ 0xa8

        // Properties
        System.Boolean RequireMesh { get; /* RVA: 0x02FFF600 */ }

        // Methods
        // RVA: 0x0A8E7098  token: 0x600007C
        private static System.Void .cctor() { }
        // RVA: 0x0A8E5C50  token: 0x600007D
        public virtual System.Void Apply(UnityEngine.Material material) { }
        // RVA: 0x0A8E6454  token: 0x600007E
        public virtual System.Void Pool() { }
        // RVA: 0x0A8E6E68  token: 0x600007F
        public virtual System.Void Transform(UnityEngine.Matrix4x4 posMatrix, UnityEngine.Matrix4x4 rotMatrix) { }
        // RVA: 0x0A8E6AE4  token: 0x6000080
        public virtual PaintIn3D.P3dCommand SpawnCopy() { }
        // RVA: 0x0A8E5BF4  token: 0x6000081
        public virtual System.Void Apply(PaintIn3D.P3dPaintableTexture paintableTexture) { }
        // RVA: 0x0A8E695C  token: 0x6000082
        public System.Void SetShape(UnityEngine.Quaternion rotation, UnityEngine.Vector3 size, System.Single angle) { }
        // RVA: 0x04DA5F50  token: 0x6000083
        public System.Void SetLocation(UnityEngine.Vector3 position, System.Boolean in3D) { }
        // RVA: 0x04DA5F80  token: 0x6000084
        public System.Void SetLocation(UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, System.Boolean in3D, System.Boolean clip) { }
        // RVA: 0x04DA5ED0  token: 0x6000085
        public System.Void SetLocation(UnityEngine.Vector3 positionA, UnityEngine.Vector3 positionB, UnityEngine.Vector3 positionC, System.Boolean in3D) { }
        // RVA: 0x04DA5FD0  token: 0x6000086
        public System.Void SetLocation(UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Vector3 position2, UnityEngine.Vector3 endPosition2, System.Boolean in3D, System.Boolean clip) { }
        // RVA: 0x0A8E63E8  token: 0x6000087
        public System.Void ClearMask() { }
        // RVA: 0x0A8E64C4  token: 0x6000088
        public System.Void SetMask(UnityEngine.Matrix4x4 matrix, UnityEngine.Texture shape, PaintIn3D.P3dChannel channel, UnityEngine.Vector3 stretch) { }
        // RVA: 0x0A8E5B30  token: 0x6000089
        public System.Void ApplyAspect(UnityEngine.Texture texture) { }
        // RVA: 0x0A8E65C0  token: 0x600008A
        public System.Void SetMaterial(PaintIn3D.P3dBlendMode blendMode, UnityEngine.Texture texture, UnityEngine.Texture shape, PaintIn3D.P3dChannel shapeChannel, System.Single hardness, System.Single wrapping, System.Single normalBack, System.Single normalFront, System.Single normalFade, UnityEngine.Color color, System.Single opacity, UnityEngine.Texture tileTexture, UnityEngine.Matrix4x4 tileMatrix, System.Single tileOpacity, System.Single tileTransition) { }
        // RVA: 0x0A8E7700  token: 0x600008B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000016  // size: 0xD0
    public class P3dCommandFill : PaintIn3D.P3dCommand
    {
        // Fields
        public PaintIn3D.P3dBlendMode Blend;  // 0x70
        public PaintIn3D.P3dHashedTexture Texture;  // 0xa8
        public UnityEngine.Color Color;  // 0xb8
        public System.Single Opacity;  // 0xc8
        public System.Single Minimum;  // 0xcc
        public static PaintIn3D.P3dCommandFill Instance;  // static @ 0x0
        private static System.Collections.Generic.Stack<PaintIn3D.P3dCommandFill> pool;  // static @ 0x8
        private static UnityEngine.Material cachedMaterial;  // static @ 0x10
        private static System.Int32 cachedMaterialHash;  // static @ 0x18
        private static System.Int32 _Buffer;  // static @ 0x1c
        private static System.Int32 _BufferSize;  // static @ 0x20
        private static System.Int32 _Texture;  // static @ 0x24
        private static System.Int32 _Color;  // static @ 0x28
        private static System.Int32 _Opacity;  // static @ 0x2c
        private static System.Int32 _Minimum;  // static @ 0x30

        // Properties
        System.Boolean RequireMesh { get; /* RVA: 0x012081B0 */ }

        // Methods
        // RVA: 0x0A8E7F70  token: 0x600008D
        private static System.Void .cctor() { }
        // RVA: 0x0A8E7B34  token: 0x600008E
        public static UnityEngine.RenderTexture Blit(UnityEngine.RenderTexture main, PaintIn3D.P3dBlendMode blendMode, UnityEngine.Texture texture, UnityEngine.Color color, System.Single opacity, System.Single minimum) { }
        // RVA: 0x0A8E78E8  token: 0x600008F
        public static System.Void Blit(UnityEngine.RenderTexture& main, UnityEngine.RenderTexture& swap, PaintIn3D.P3dBlendMode blendMode, UnityEngine.Texture texture, UnityEngine.Color color, System.Single opacity, System.Single minimum) { }
        // RVA: 0x0A8E7740  token: 0x6000090
        public virtual System.Void Apply(UnityEngine.Material material) { }
        // RVA: 0x0A8E7C9C  token: 0x6000091
        public virtual System.Void Pool() { }
        // RVA: 0x041E1670  token: 0x6000092
        public virtual System.Void Transform(UnityEngine.Matrix4x4 posMatrix, UnityEngine.Matrix4x4 rotMatrix) { }
        // RVA: 0x0A8E7E6C  token: 0x6000093
        public virtual PaintIn3D.P3dCommand SpawnCopy() { }
        // RVA: 0x0A8E5BF4  token: 0x6000094
        public virtual System.Void Apply(PaintIn3D.P3dPaintableTexture paintableTexture) { }
        // RVA: 0x0A8E7D0C  token: 0x6000095
        public UnityEngine.Material SetMaterial(PaintIn3D.P3dBlendMode blendMode, UnityEngine.Texture texture, UnityEngine.Color color, System.Single opacity, System.Single minimum) { }
        // RVA: 0x0A8E81A8  token: 0x6000096
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000017  // size: 0x90
    public class P3dCommandReplace : PaintIn3D.P3dCommand
    {
        // Fields
        public PaintIn3D.P3dHashedTexture Texture;  // 0x70
        public UnityEngine.Color Color;  // 0x80
        public static PaintIn3D.P3dCommandReplace Instance;  // static @ 0x0
        private static System.Collections.Generic.Stack<PaintIn3D.P3dCommandReplace> pool;  // static @ 0x8
        private static UnityEngine.Material cachedMaterial;  // static @ 0x10
        private static System.Int32 cachedMaterialHash;  // static @ 0x18
        private static System.Int32 _Texture;  // static @ 0x1c
        private static System.Int32 _Color;  // static @ 0x20

        // Properties
        System.Boolean RequireMesh { get; /* RVA: 0x012081B0 */ }

        // Methods
        // RVA: 0x0A8E9160  token: 0x6000098
        private static System.Void .cctor() { }
        // RVA: 0x0A8E8E9C  token: 0x6000099
        public static System.Void Blit(UnityEngine.RenderTexture renderTexture, UnityEngine.Texture texture, UnityEngine.Color tint) { }
        // RVA: 0x0A8E8DC8  token: 0x600009A
        public static System.Void BlitFast(UnityEngine.RenderTexture renderTexture, UnityEngine.Texture texture, UnityEngine.Color tint) { }
        // RVA: 0x0A8E8CBC  token: 0x600009B
        public virtual System.Void Apply(UnityEngine.Material material) { }
        // RVA: 0x0A8E8F8C  token: 0x600009C
        public virtual System.Void Pool() { }
        // RVA: 0x041E1670  token: 0x600009D
        public virtual System.Void Transform(UnityEngine.Matrix4x4 posMatrix, UnityEngine.Matrix4x4 rotMatrix) { }
        // RVA: 0x0A8E90C8  token: 0x600009E
        public virtual PaintIn3D.P3dCommand SpawnCopy() { }
        // RVA: 0x0A8E8FFC  token: 0x600009F
        public UnityEngine.Material SetMaterial(UnityEngine.Texture texture, UnityEngine.Color color) { }
        // RVA: 0x0A8E92E8  token: 0x60000A0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000018  // size: 0xF0
    public class P3dCommandReplaceChannels : PaintIn3D.P3dCommand
    {
        // Fields
        public PaintIn3D.P3dHashedTexture TextureR;  // 0x70
        public PaintIn3D.P3dHashedTexture TextureG;  // 0x80
        public PaintIn3D.P3dHashedTexture TextureB;  // 0x90
        public PaintIn3D.P3dHashedTexture TextureA;  // 0xa0
        public UnityEngine.Vector4 ChannelR;  // 0xb0
        public UnityEngine.Vector4 ChannelG;  // 0xc0
        public UnityEngine.Vector4 ChannelB;  // 0xd0
        public UnityEngine.Vector4 ChannelA;  // 0xe0
        public static PaintIn3D.P3dCommandReplaceChannels Instance;  // static @ 0x0
        private static System.Collections.Generic.Stack<PaintIn3D.P3dCommandReplaceChannels> pool;  // static @ 0x8
        private static UnityEngine.Material cachedMaterial;  // static @ 0x10
        private static System.Int32 cachedMaterialHash;  // static @ 0x18
        private static System.Int32 _TextureR;  // static @ 0x1c
        private static System.Int32 _TextureG;  // static @ 0x20
        private static System.Int32 _TextureB;  // static @ 0x24
        private static System.Int32 _TextureA;  // static @ 0x28
        private static System.Int32 _ChannelR;  // static @ 0x2c
        private static System.Int32 _ChannelG;  // static @ 0x30
        private static System.Int32 _ChannelB;  // static @ 0x34
        private static System.Int32 _ChannelA;  // static @ 0x38

        // Properties
        System.Boolean RequireMesh { get; /* RVA: 0x012081B0 */ }

        // Methods
        // RVA: 0x0A8E89EC  token: 0x60000A2
        private static System.Void .cctor() { }
        // RVA: 0x0A8E854C  token: 0x60000A3
        public static System.Void Blit(UnityEngine.RenderTexture renderTexture, UnityEngine.Texture textureR, UnityEngine.Texture textureG, UnityEngine.Texture textureB, UnityEngine.Texture textureA, UnityEngine.Vector4 channelR, UnityEngine.Vector4 channelG, UnityEngine.Vector4 channelB, UnityEngine.Vector4 channelA, UnityEngine.Vector4 channels) { }
        // RVA: 0x0A8E83E8  token: 0x60000A4
        public static System.Void BlitFast(UnityEngine.RenderTexture renderTexture, UnityEngine.Texture textureR, UnityEngine.Texture textureG, UnityEngine.Texture textureB, UnityEngine.Texture textureA, UnityEngine.Vector4 channelR, UnityEngine.Vector4 channelG, UnityEngine.Vector4 channelB, UnityEngine.Vector4 channelA) { }
        // RVA: 0x0A8E81E8  token: 0x60000A5
        public virtual System.Void Apply(UnityEngine.Material material) { }
        // RVA: 0x0A8E86CC  token: 0x60000A6
        public virtual System.Void Pool() { }
        // RVA: 0x041E1670  token: 0x60000A7
        public virtual System.Void Transform(UnityEngine.Matrix4x4 posMatrix, UnityEngine.Matrix4x4 rotMatrix) { }
        // RVA: 0x0A8E88CC  token: 0x60000A8
        public virtual PaintIn3D.P3dCommand SpawnCopy() { }
        // RVA: 0x0A8E873C  token: 0x60000A9
        public UnityEngine.Material SetMaterial(UnityEngine.Texture textureR, UnityEngine.Texture textureG, UnityEngine.Texture textureB, UnityEngine.Texture textureA, UnityEngine.Vector4 channelR, UnityEngine.Vector4 channelG, UnityEngine.Vector4 channelB, UnityEngine.Vector4 channelA) { }
        // RVA: 0x0A8E8C7C  token: 0x60000AA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000019  // size: 0x208
    public class P3dCommandSphere : PaintIn3D.P3dCommand
    {
        // Fields
        public PaintIn3D.P3dBlendMode Blend;  // 0x70
        public System.Boolean In3D;  // 0xa8
        public UnityEngine.Vector3 Position;  // 0xac
        public UnityEngine.Vector3 EndPosition;  // 0xb8
        public UnityEngine.Vector3 Position2;  // 0xc4
        public UnityEngine.Vector3 EndPosition2;  // 0xd0
        public System.Int32 Extrusions;  // 0xdc
        public System.Boolean Clip;  // 0xe0
        public UnityEngine.Matrix4x4 Matrix;  // 0xe4
        public UnityEngine.Color Color;  // 0x124
        public System.Single Opacity;  // 0x134
        public System.Single Hardness;  // 0x138
        public PaintIn3D.P3dHashedTexture TileTexture;  // 0x140
        public UnityEngine.Matrix4x4 TileMatrix;  // 0x150
        public System.Single TileOpacity;  // 0x190
        public System.Single TileTransition;  // 0x194
        public UnityEngine.Matrix4x4 MaskMatrix;  // 0x198
        public PaintIn3D.P3dHashedTexture MaskShape;  // 0x1d8
        public UnityEngine.Vector4 MaskChannel;  // 0x1e8
        public UnityEngine.Vector3 MaskStretch;  // 0x1f8
        public static PaintIn3D.P3dCommandSphere Instance;  // static @ 0x0
        private static System.Collections.Generic.Stack<PaintIn3D.P3dCommandSphere> pool;  // static @ 0x8
        private static UnityEngine.Material cachedSpotMaterial;  // static @ 0x10
        private static UnityEngine.Material cachedLineMaterial;  // static @ 0x18
        private static UnityEngine.Material cachedQuadMaterial;  // static @ 0x20
        private static UnityEngine.Material cachedLineClipMaterial;  // static @ 0x28
        private static UnityEngine.Material cachedQuadClipMaterial;  // static @ 0x30
        private static System.Int32 cachedSpotMaterialHash;  // static @ 0x38
        private static System.Int32 cachedLineMaterialHash;  // static @ 0x3c
        private static System.Int32 cachedQuadMaterialHash;  // static @ 0x40
        private static System.Int32 cachedLineClipMaterialHash;  // static @ 0x44
        private static System.Int32 cachedQuadClipMaterialHash;  // static @ 0x48
        private static System.Int32 _In3D;  // static @ 0x4c
        private static System.Int32 _Position;  // static @ 0x50
        private static System.Int32 _EndPosition;  // static @ 0x54
        private static System.Int32 _Position2;  // static @ 0x58
        private static System.Int32 _EndPosition2;  // static @ 0x5c
        private static System.Int32 _Matrix;  // static @ 0x60
        private static System.Int32 _Color;  // static @ 0x64
        private static System.Int32 _Opacity;  // static @ 0x68
        private static System.Int32 _Hardness;  // static @ 0x6c
        private static System.Int32 _TileTexture;  // static @ 0x70
        private static System.Int32 _TileMatrix;  // static @ 0x74
        private static System.Int32 _TileOpacity;  // static @ 0x78
        private static System.Int32 _TileTransition;  // static @ 0x7c
        private static System.Int32 _MaskMatrix;  // static @ 0x80
        private static System.Int32 _MaskTexture;  // static @ 0x84
        private static System.Int32 _MaskChannel;  // static @ 0x88
        private static System.Int32 _MaskStretch;  // static @ 0x8c

        // Properties
        System.Boolean RequireMesh { get; /* RVA: 0x02FFF600 */ }

        // Methods
        // RVA: 0x0A8EA46C  token: 0x60000AC
        private static System.Void .cctor() { }
        // RVA: 0x0A8E93EC  token: 0x60000AD
        public virtual System.Void Apply(UnityEngine.Material material) { }
        // RVA: 0x0A8E9A24  token: 0x60000AE
        public virtual System.Void Pool() { }
        // RVA: 0x0A8EA2A4  token: 0x60000AF
        public virtual System.Void Transform(UnityEngine.Matrix4x4 posMatrix, UnityEngine.Matrix4x4 rotMatrix) { }
        // RVA: 0x0A8E9FDC  token: 0x60000B0
        public virtual PaintIn3D.P3dCommand SpawnCopy() { }
        // RVA: 0x0A8E5BF4  token: 0x60000B1
        public virtual System.Void Apply(PaintIn3D.P3dPaintableTexture paintableTexture) { }
        // RVA: 0x04DA5F50  token: 0x60000B2
        public System.Void SetLocation(UnityEngine.Vector3 position, System.Boolean in3D) { }
        // RVA: 0x04DA5F80  token: 0x60000B3
        public System.Void SetLocation(UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, System.Boolean in3D, System.Boolean clip) { }
        // RVA: 0x04DA5ED0  token: 0x60000B4
        public System.Void SetLocation(UnityEngine.Vector3 positionA, UnityEngine.Vector3 positionB, UnityEngine.Vector3 positionC, System.Boolean in3D) { }
        // RVA: 0x04DA5FD0  token: 0x60000B5
        public System.Void SetLocation(UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Vector3 position2, UnityEngine.Vector3 endPosition2, System.Boolean in3D, System.Boolean clip) { }
        // RVA: 0x0A8E99B8  token: 0x60000B6
        public System.Void ClearMask() { }
        // RVA: 0x0A8E9A94  token: 0x60000B7
        public System.Void SetMask(UnityEngine.Matrix4x4 matrix, UnityEngine.Texture shape, PaintIn3D.P3dChannel channel, UnityEngine.Vector3 stretch) { }
        // RVA: 0x0A8E9328  token: 0x60000B8
        public System.Void ApplyAspect(UnityEngine.Texture texture) { }
        // RVA: 0x0A8E9F10  token: 0x60000B9
        public System.Void SetShape(System.Single radius) { }
        // RVA: 0x0A8E9DD4  token: 0x60000BA
        public System.Void SetShape(UnityEngine.Quaternion rotation, UnityEngine.Vector3 size, System.Single angle) { }
        // RVA: 0x0A8E9B90  token: 0x60000BB
        public System.Void SetMaterial(PaintIn3D.P3dBlendMode blendMode, System.Single hardness, UnityEngine.Color color, System.Single opacity, UnityEngine.Texture tileTexture, UnityEngine.Matrix4x4 tileMatrix, System.Single tileOpacity, System.Single tileTransition) { }
        // RVA: 0x0A8EA994  token: 0x60000BC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001A  // size: 0x88
    public class P3dHitBetween : UnityEngine.MonoBehaviour
    {
        // Fields
        private PaintIn3D.P3dHitBetween.PhaseType paintIn;  // 0x18
        private System.Single interval;  // 0x1c
        private UnityEngine.Transform pointA;  // 0x20
        private UnityEngine.Transform pointB;  // 0x28
        private System.Single fraction;  // 0x30
        private UnityEngine.LayerMask layers;  // 0x34
        private PaintIn3D.P3dHitBetween.OrientationType orientation;  // 0x38
        private UnityEngine.Camera _camera;  // 0x40
        private UnityEngine.Transform customTransform;  // 0x48
        private PaintIn3D.P3dHitBetween.NormalType normal;  // 0x50
        private System.Single offset;  // 0x54
        private System.Boolean preview;  // 0x58
        private System.Int32 priority;  // 0x5c
        private System.Single pressure;  // 0x60
        private PaintIn3D.P3dHitBetween.EmitType emit;  // 0x64
        private UnityEngine.Transform point;  // 0x68
        private UnityEngine.LineRenderer line;  // 0x70
        private PaintIn3D.P3dPointConnector connector;  // 0x78
        private System.Single current;  // 0x80

        // Properties
        PaintIn3D.P3dHitBetween.PhaseType PaintIn { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        System.Single Interval { get; /* RVA: 0x04D868F0 */ set; /* RVA: 0x04D86900 */ }
        UnityEngine.Transform PointA { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        UnityEngine.Transform PointB { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        System.Single Fraction { get; /* RVA: 0x04D86E20 */ }
        UnityEngine.LayerMask Layers { get; /* RVA: 0x04D86730 */ set; /* RVA: 0x04D86760 */ }
        PaintIn3D.P3dHitBetween.OrientationType Orientation { get; /* RVA: 0x04D863B0 */ set; /* RVA: 0x04D86430 */ }
        UnityEngine.Camera Camera { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        UnityEngine.Transform CustomTransform { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }
        PaintIn3D.P3dHitBetween.NormalType Normal { get; /* RVA: 0x04D86550 */ set; /* RVA: 0x04D865A0 */ }
        System.Single Offset { get; /* RVA: 0x04D86E00 */ set; /* RVA: 0x04D86E10 */ }
        System.Boolean Preview { get; /* RVA: 0x04D86F70 */ set; /* RVA: 0x04D86F80 */ }
        System.Int32 Priority { get; /* RVA: 0x04D86560 */ set; /* RVA: 0x04D865C0 */ }
        System.Single Pressure { get; /* RVA: 0x04D88480 */ set; /* RVA: 0x04D884A0 */ }
        PaintIn3D.P3dHitBetween.EmitType Draw { get; /* RVA: 0x04D88110 */ set; /* RVA: 0x04D88120 */ }
        UnityEngine.Transform Point { get; /* RVA: 0x04D86230 */ set; /* RVA: 0x0485F170 */ }
        UnityEngine.LineRenderer Line { get; /* RVA: 0x04D862B0 */ set; /* RVA: 0x063AD760 */ }
        PaintIn3D.P3dPointConnector Connector { get; /* RVA: 0x0A8EB67C */ }

        // Methods
        // RVA: 0x0A8EAC88  token: 0x60000DF
        public System.Void ManuallyHitNow() { }
        // RVA: 0x0A8EAC48  token: 0x60000E0
        public System.Void ClearHitCache() { }
        // RVA: 0x0A8EAD7C  token: 0x60000E1
        public System.Void ResetConnections() { }
        // RVA: 0x0A8EAD58  token: 0x60000E2
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A8EAC94  token: 0x60000E3
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0A8EB5F8  token: 0x60000E4
        protected virtual System.Void Update() { }
        // RVA: 0x0A8EAC80  token: 0x60000E5
        protected virtual System.Void LateUpdate() { }
        // RVA: 0x0A8EAC6C  token: 0x60000E6
        protected virtual System.Void FixedUpdate() { }
        // RVA: 0x0A8EAD9C  token: 0x60000E7
        private System.Void SubmitHit(System.Boolean preview) { }
        // RVA: 0x0A8EA9D4  token: 0x60000E8
        private System.Void CalcHitData(UnityEngine.Vector3 hitPoint, UnityEngine.Vector3 hitNormal, UnityEngine.Ray ray, UnityEngine.Vector3& finalPosition, UnityEngine.Quaternion& finalRotation) { }
        // RVA: 0x0A8EB40C  token: 0x60000E9
        private System.Void UpdatePointAndLine() { }
        // RVA: 0x0A8EB39C  token: 0x60000EA
        private System.Void UpdateHit() { }
        // RVA: 0x0A8EB648  token: 0x60000EB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001F  // size: 0x40
    public class P3dHitCache
    {
        // Fields
        private System.Boolean cached;  // 0x10
        private System.Collections.Generic.List<PaintIn3D.IHitPoint> hitPoints;  // 0x18
        private System.Collections.Generic.List<PaintIn3D.IHitLine> hitLines;  // 0x20
        private System.Collections.Generic.List<PaintIn3D.IHitTriangle> hitTriangles;  // 0x28
        private System.Collections.Generic.List<PaintIn3D.IHitQuad> hitQuads;  // 0x30
        private System.Collections.Generic.List<PaintIn3D.IHitCoord> hitCoords;  // 0x38
        private static System.Collections.Generic.List<PaintIn3D.IHit> hits;  // static @ 0x0

        // Properties
        System.Boolean Cached { get; /* RVA: 0x015EFCE0 */ }

        // Methods
        // RVA: 0x0A8EBD70  token: 0x60000ED
        public System.Void InvokePoint(UnityEngine.GameObject gameObject, System.Boolean preview, System.Int32 priority, System.Single pressure, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0A8EBC10  token: 0x60000EE
        public System.Void InvokeLine(UnityEngine.GameObject gameObject, System.Boolean preview, System.Int32 priority, System.Single pressure, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Quaternion rotation, System.Boolean clip) { }
        // RVA: 0x0A8EC1A8  token: 0x60000EF
        public System.Void InvokeTriangle(UnityEngine.GameObject gameObject, System.Boolean preview, System.Int32 priority, System.Single pressure, UnityEngine.RaycastHit hit, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0A8EC020  token: 0x60000F0
        public System.Void InvokeTriangle(UnityEngine.GameObject gameObject, System.Boolean preview, System.Int32 priority, System.Single pressure, UnityEngine.Vector3 positionA, UnityEngine.Vector3 positionB, UnityEngine.Vector3 positionC, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0A8EBE94  token: 0x60000F1
        public System.Void InvokeQuad(UnityEngine.GameObject gameObject, System.Boolean preview, System.Int32 priority, System.Single pressure, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Vector3 position2, UnityEngine.Vector3 endPosition2, UnityEngine.Quaternion rotation, System.Boolean clip) { }
        // RVA: 0x0A8EBAE4  token: 0x60000F2
        public System.Void InvokeCoord(UnityEngine.GameObject gameObject, System.Boolean preview, System.Int32 priority, System.Single pressure, PaintIn3D.P3dHit hit, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0A8EBA14  token: 0x60000F3
        public System.Void Clear() { }
        // RVA: 0x0A8EB6EC  token: 0x60000F4
        private System.Void Cache(UnityEngine.GameObject gameObject) { }
        // RVA: 0x0A8EC384  token: 0x60000F5
        public System.Void .ctor() { }
        // RVA: 0x0A8EC2F8  token: 0x60000F6
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000020  // size: 0x78
    public class P3dHitCollisions : UnityEngine.MonoBehaviour
    {
        // Fields
        private PaintIn3D.P3dHitCollisions.EmitType emit;  // 0x18
        private System.Single raycastDistance;  // 0x1c
        private UnityEngine.LayerMask layers;  // 0x20
        private System.Boolean onlyUseFirstContact;  // 0x24
        private System.Single delay;  // 0x28
        private PaintIn3D.P3dHitCollisions.OrientationType orientation;  // 0x2c
        private UnityEngine.Camera _camera;  // 0x30
        private System.Boolean preview;  // 0x38
        private System.Single threshold;  // 0x3c
        private PaintIn3D.P3dHitCollisions.PressureType pressureMode;  // 0x40
        private System.Single pressureMin;  // 0x44
        private System.Single pressureMax;  // 0x48
        private System.Single pressureConstant;  // 0x4c
        private System.Single pressureMultiplier;  // 0x50
        private System.Single offset;  // 0x54
        private System.Int32 priority;  // 0x58
        private UnityEngine.GameObject root;  // 0x60
        private System.Single cooldown;  // 0x68
        private PaintIn3D.P3dHitCache hitCache;  // 0x70

        // Properties
        PaintIn3D.P3dHitCollisions.EmitType Emit { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        System.Single RaycastDistance { get; /* RVA: 0x04D868F0 */ set; /* RVA: 0x04D86900 */ }
        UnityEngine.LayerMask Layers { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }
        System.Boolean OnlyUseFirstContact { get; /* RVA: 0x04D86870 */ set; /* RVA: 0x04D86880 */ }
        System.Single Delay { get; /* RVA: 0x04D86BF0 */ set; /* RVA: 0x04D86C00 */ }
        PaintIn3D.P3dHitCollisions.OrientationType Orientation { get; /* RVA: 0x04D86710 */ set; /* RVA: 0x04D86720 */ }
        UnityEngine.Camera Camera { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        System.Boolean Preview { get; /* RVA: 0x011F2A70 */ set; /* RVA: 0x011F2A80 */ }
        System.Single Threshold { get; /* RVA: 0x04D863E0 */ set; /* RVA: 0x04D86460 */ }
        PaintIn3D.P3dHitCollisions.PressureType PressureMode { get; /* RVA: 0x04D865E0 */ set; /* RVA: 0x04D86610 */ }
        System.Single PressureMin { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single PressureMax { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Single PressureConstant { get; /* RVA: 0x04D87860 */ set; /* RVA: 0x04D91480 */ }
        System.Single PressureMultiplier { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }
        System.Single Offset { get; /* RVA: 0x04D86E00 */ set; /* RVA: 0x04D86E10 */ }
        System.Int32 Priority { get; /* RVA: 0x04D86570 */ set; /* RVA: 0x04D865D0 */ }
        UnityEngine.GameObject Root { get; /* RVA: 0x04D86280 */ set; /* RVA: 0x0A8ECCEC */ }
        PaintIn3D.P3dHitCache HitCache { get; /* RVA: 0x04D862B0 */ }

        // Methods
        // RVA: 0x0A8ECAA4  token: 0x600011A
        public System.Void ClearHitCache() { }
        // RVA: 0x0A8ECAC4  token: 0x600011B
        protected virtual System.Void OnCollisionEnter(UnityEngine.Collision collision) { }
        // RVA: 0x0A8ECAC4  token: 0x600011C
        protected virtual System.Void OnCollisionStay(UnityEngine.Collision collision) { }
        // RVA: 0x0A8ECC18  token: 0x600011D
        protected virtual System.Void Update() { }
        // RVA: 0x0A8ECACC  token: 0x600011E
        private System.Boolean TryGetRaycastHit(UnityEngine.ContactPoint contact, UnityEngine.RaycastHit& hit) { }
        // RVA: 0x0A8EC528  token: 0x600011F
        private System.Void CheckCollision(UnityEngine.Collision collision) { }
        // RVA: 0x0A8ECC48  token: 0x6000120
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000024  // size: 0x48
    public class P3dHitNearby : UnityEngine.MonoBehaviour
    {
        // Fields
        private PaintIn3D.P3dHitNearby.PhaseType paintIn;  // 0x18
        private System.Single interval;  // 0x1c
        private System.Boolean preview;  // 0x20
        private System.Int32 priority;  // 0x24
        private System.Single pressure;  // 0x28
        private PaintIn3D.P3dPointConnector connector;  // 0x30
        private System.Single current;  // 0x38
        private UnityEngine.Vector3 lastPosition;  // 0x3c

        // Properties
        PaintIn3D.P3dHitNearby.PhaseType PaintIn { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        System.Single Interval { get; /* RVA: 0x04D868F0 */ set; /* RVA: 0x04D86900 */ }
        System.Boolean Preview { get; /* RVA: 0x04D866B0 */ set; /* RVA: 0x04D866D0 */ }
        System.Int32 Priority { get; /* RVA: 0x011F0020 */ set; /* RVA: 0x011F0030 */ }
        System.Single Pressure { get; /* RVA: 0x04D86BF0 */ set; /* RVA: 0x04D86C00 */ }
        PaintIn3D.P3dPointConnector Connector { get; /* RVA: 0x0A8ECF94 */ }

        // Methods
        // RVA: 0x0A8ECD54  token: 0x600012C
        public System.Void ManuallyHitNow() { }
        // RVA: 0x0A8ECD1C  token: 0x600012D
        public System.Void ClearHitCache() { }
        // RVA: 0x0A8ECD84  token: 0x600012E
        public System.Void ResetConnections() { }
        // RVA: 0x0A8ECD60  token: 0x600012F
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A8ECF20  token: 0x6000130
        protected virtual System.Void Update() { }
        // RVA: 0x0A8ECD40  token: 0x6000131
        protected virtual System.Void FixedUpdate() { }
        // RVA: 0x0A8ECDA4  token: 0x6000132
        private System.Void SubmitHit(System.Boolean preview) { }
        // RVA: 0x0A8ECEB0  token: 0x6000133
        private System.Void UpdateHit() { }
        // RVA: 0x0A8ECF70  token: 0x6000134
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000026  // size: 0x80
    public class P3dHitParticles : UnityEngine.MonoBehaviour
    {
        // Fields
        private PaintIn3D.P3dHitParticles.EmitType emit;  // 0x18
        private System.Single raycastDistance;  // 0x1c
        private UnityEngine.LayerMask layers;  // 0x20
        private PaintIn3D.P3dHitParticles.OrientationType orientation;  // 0x24
        private UnityEngine.Camera _camera;  // 0x28
        private PaintIn3D.P3dHitParticles.NormalType normal;  // 0x30
        private System.Single offset;  // 0x34
        private System.Int32 skip;  // 0x38
        private System.Boolean preview;  // 0x3c
        private System.Int32 priority;  // 0x40
        private PaintIn3D.P3dHitParticles.PressureType pressureMode;  // 0x44
        private System.Single pressureMin;  // 0x48
        private System.Single pressureMax;  // 0x4c
        private System.Single pressureConstant;  // 0x50
        private System.Single pressureMultiplier;  // 0x54
        private UnityEngine.GameObject root;  // 0x58
        private UnityEngine.ParticleSystem cachedParticleSystem;  // 0x60
        private System.Boolean cachedParticleSystemSet;  // 0x68
        private static System.Collections.Generic.List<UnityEngine.ParticleCollisionEvent> particleCollisionEvents;  // static @ 0x0
        private PaintIn3D.P3dHitCache hitCache;  // 0x70
        private System.Int32 skipCounter;  // 0x78

        // Properties
        PaintIn3D.P3dHitParticles.EmitType Emit { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        System.Single RaycastDistance { get; /* RVA: 0x04D868F0 */ set; /* RVA: 0x04D86900 */ }
        UnityEngine.LayerMask Layers { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }
        PaintIn3D.P3dHitParticles.OrientationType Orientation { get; /* RVA: 0x011F0020 */ set; /* RVA: 0x011F0030 */ }
        UnityEngine.Camera Camera { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        PaintIn3D.P3dHitParticles.NormalType Normal { get; /* RVA: 0x04D864F0 */ set; /* RVA: 0x04D86740 */ }
        System.Single Offset { get; /* RVA: 0x04D8E050 */ set; /* RVA: 0x04D8E060 */ }
        System.Int32 Skip { get; /* RVA: 0x04D863B0 */ set; /* RVA: 0x04D86430 */ }
        System.Boolean Preview { get; /* RVA: 0x04D86BD0 */ set; /* RVA: 0x04D86BE0 */ }
        System.Int32 Priority { get; /* RVA: 0x04D865E0 */ set; /* RVA: 0x04D86610 */ }
        PaintIn3D.P3dHitParticles.PressureType PressureMode { get; /* RVA: 0x04D86530 */ set; /* RVA: 0x04D86580 */ }
        System.Single PressureMin { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Single PressureMax { get; /* RVA: 0x04D87860 */ set; /* RVA: 0x04D91480 */ }
        System.Single PressureConstant { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }
        System.Single PressureMultiplier { get; /* RVA: 0x04D86E00 */ set; /* RVA: 0x04D86E10 */ }
        UnityEngine.GameObject Root { get; /* RVA: 0x02E56440 */ set; /* RVA: 0x0A8EDAB4 */ }
        PaintIn3D.P3dHitCache HitCache { get; /* RVA: 0x04D862B0 */ }

        // Methods
        // RVA: 0x0A8ECAA4  token: 0x6000156
        public System.Void ClearHitCache() { }
        // RVA: 0x0A8ED7F0  token: 0x6000157
        private System.Boolean TryGetRaycastHit(UnityEngine.ParticleCollisionEvent collision, UnityEngine.RaycastHit& hit) { }
        // RVA: 0x0A8ED004  token: 0x6000158
        protected virtual System.Void OnParticleCollision(UnityEngine.GameObject hitGameObject) { }
        // RVA: 0x0A8EDA30  token: 0x6000159
        public System.Void .ctor() { }
        // RVA: 0x0A8ED9A4  token: 0x600015A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200002B  // size: 0x88
    public class P3dHitScreen : PaintIn3D.P3dHitScreenBase
    {
        // Fields
        private PaintIn3D.P3dHitScreen.FrequencyType frequency;  // 0x70
        private System.Single interval;  // 0x74
        private PaintIn3D.P3dPointConnector connector;  // 0x78
        private System.Collections.Generic.List<PaintIn3D.P3dHitScreen.Link> links;  // 0x80

        // Properties
        PaintIn3D.P3dHitScreen.FrequencyType Frequency { get; /* RVA: 0x04D868A0 */ set; /* RVA: 0x04D868D0 */ }
        System.Single Interval { get; /* RVA: 0x04DA60B0 */ set; /* RVA: 0x04DA60C0 */ }
        PaintIn3D.P3dPointConnector Connector { get; /* RVA: 0x0A8F05AC */ }

        // Methods
        // RVA: 0x0A8EFA3C  token: 0x6000160
        public System.Void ClearHitCache() { }
        // RVA: 0x0A8EAD7C  token: 0x6000161
        public System.Void ResetConnections() { }
        // RVA: 0x0A8EFD50  token: 0x6000162
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A8F04F8  token: 0x6000163
        protected virtual System.Void Update() { }
        // RVA: 0x0A8EFB30  token: 0x6000164
        protected virtual System.Void HandleFingerUpdate(CW.Common.CwInputManager.Finger finger, System.Boolean down, System.Boolean up) { }
        // RVA: 0x0A8EFA60  token: 0x6000165
        protected virtual System.Void HandleFingerUp(CW.Common.CwInputManager.Finger finger) { }
        // RVA: 0x0A8F0148  token: 0x6000166
        private System.Void PaintSmooth(PaintIn3D.P3dHitScreen.Link link, System.Boolean down, System.Single pixelSpacing) { }
        // RVA: 0x041E1670  token: 0x6000167
        protected virtual System.Void OnFingerUp(PaintIn3D.P3dHitScreen.Link link) { }
        // RVA: 0x0A8EFF30  token: 0x6000168
        private System.Void PaintInterval(PaintIn3D.P3dHitScreen.Link link, System.Boolean down) { }
        // RVA: 0x0A8F00AC  token: 0x6000169
        private System.Void PaintRelease(PaintIn3D.P3dHitScreen.Link link, System.Boolean up) { }
        // RVA: 0x0A8F0020  token: 0x600016A
        private System.Void PaintPress(PaintIn3D.P3dHitScreen.Link link, System.Boolean down) { }
        // RVA: 0x0A8EFEA4  token: 0x600016B
        private System.Void PaintEvery(PaintIn3D.P3dHitScreen.Link link, System.Boolean down) { }
        // RVA: 0x0A8F0424  token: 0x600016C
        private System.Void RecordAndPaintAt(PaintIn3D.P3dHitScreen.Link link, UnityEngine.Vector2 screenNew, UnityEngine.Vector2 screenOld, System.Boolean preview, System.Single pressure, System.Object owner) { }
        // RVA: 0x0A8F0518  token: 0x600016D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002E  // size: 0x70
    public abstract class P3dHitScreenBase : UnityEngine.MonoBehaviour
    {
        // Fields
        private UnityEngine.Camera _camera;  // 0x18
        private UnityEngine.LayerMask layers;  // 0x20
        private PaintIn3D.P3dHitScreenBase.ButtonTypes requiredButtons;  // 0x24
        private UnityEngine.KeyCode requiredKey;  // 0x28
        private UnityEngine.LayerMask guiLayers;  // 0x2c
        private PaintIn3D.P3dHitScreenBase.EmitType emit;  // 0x30
        private PaintIn3D.P3dHitScreenBase.RotationType rotateTo;  // 0x34
        private PaintIn3D.P3dHitScreenBase.DirectionType normalDirection;  // 0x38
        private PaintIn3D.P3dHitScreenBase.RelativeType normalRelativeTo;  // 0x3c
        private UnityEngine.Transform customTransform;  // 0x40
        private System.Single normalOffset;  // 0x48
        private System.Single mouseOffset;  // 0x4c
        private System.Single touchOffset;  // 0x50
        protected System.Boolean storeStates;  // 0x54
        protected System.Boolean showPreview;  // 0x55
        private System.Int32 priority;  // 0x58
        protected System.Collections.Generic.List<CW.Common.CwInputManager.Finger> fingers;  // 0x60
        protected System.Boolean keyPressed;  // 0x68

        // Properties
        UnityEngine.Camera Camera { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        UnityEngine.LayerMask Layers { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }
        PaintIn3D.P3dHitScreenBase.ButtonTypes RequiredButtons { get; /* RVA: 0x011F0020 */ set; /* RVA: 0x011F0030 */ }
        UnityEngine.KeyCode RequiredKey { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }
        UnityEngine.LayerMask GuiLayers { get; /* RVA: 0x04D86710 */ set; /* RVA: 0x04D86720 */ }
        PaintIn3D.P3dHitScreenBase.EmitType Emit { get; /* RVA: 0x04D864F0 */ set; /* RVA: 0x04D86740 */ }
        PaintIn3D.P3dHitScreenBase.RotationType RotateTo { get; /* RVA: 0x04D86730 */ set; /* RVA: 0x04D86760 */ }
        PaintIn3D.P3dHitScreenBase.DirectionType NormalDirection { get; /* RVA: 0x04D863B0 */ set; /* RVA: 0x04D86430 */ }
        PaintIn3D.P3dHitScreenBase.RelativeType NormalRelativeTo { get; /* RVA: 0x04D86640 */ set; /* RVA: 0x04D86670 */ }
        UnityEngine.Transform CustomTransform { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        System.Single NormalOffset { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Single MouseOffset { get; /* RVA: 0x04D87860 */ set; /* RVA: 0x04D91480 */ }
        System.Single TouchOffset { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }
        System.Boolean StoreStates { get; /* RVA: 0x04D86C20 */ set; /* RVA: 0x04D86C40 */ }
        System.Boolean ShowPreview { get; /* RVA: 0x04D86C10 */ set; /* RVA: 0x04D86C30 */ }
        System.Int32 Priority { get; /* RVA: 0x04D86570 */ set; /* RVA: 0x04D865D0 */ }
        System.Boolean NeedsDrawAngle { get; /* RVA: 0x0A8EECC4 */ }

        // Methods
        // RVA: 0x0A8EE51C  token: 0x6000193
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A8EE254  token: 0x6000194
        protected virtual System.Void LateUpdate() { }
        // RVA: 0x041E1670  token: 0x6000195
        protected virtual System.Void HandleFingerUpdate(CW.Common.CwInputManager.Finger finger, System.Boolean down, System.Boolean up) { }
        // RVA: 0x041E1670  token: 0x6000196
        protected virtual System.Void HandleFingerUp(CW.Common.CwInputManager.Finger finger) { }
        // RVA: 0x0A8EE468  token: 0x6000197
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0A8EE0A8  token: 0x6000198
        private System.Boolean FingerWentDown(CW.Common.CwInputManager.Finger finger) { }
        // RVA: 0x0A8EE184  token: 0x6000199
        private System.Void HandleFingerDown(CW.Common.CwInputManager.Finger finger) { }
        // RVA: 0x0A8EDF30  token: 0x600019A
        protected virtual System.Void DoQuery(UnityEngine.Vector2 screenPosition, UnityEngine.Camera& camera, UnityEngine.Ray& ray, UnityEngine.RaycastHit& hit3D, UnityEngine.RaycastHit2D& hit2D) { }
        // RVA: 0x0A8EE6DC  token: 0x600019B
        protected System.Void PaintAt(PaintIn3D.P3dPointConnector connector, PaintIn3D.P3dHitCache hitCache, UnityEngine.Vector2 screenPosition, UnityEngine.Vector2 screenPositionOld, System.Boolean preview, System.Single pressure, System.Object owner) { }
        // RVA: 0x0A8EDAE4  token: 0x600019C
        private System.Void CalcHitData(UnityEngine.Vector3 hitPoint, UnityEngine.Vector3 hitNormal, UnityEngine.Ray ray, UnityEngine.Camera camera, UnityEngine.Vector2 screenPositionOld, UnityEngine.Vector3& finalPosition, UnityEngine.Quaternion& finalRotation) { }
        // RVA: 0x0A8EEC1C  token: 0x600019D
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000034  // size: 0x90
    public class P3dHitScreenFill : PaintIn3D.P3dHitScreen
    {
        // Fields
        private System.Single fillSpacing;  // 0x88

        // Properties
        System.Single FillSpacing { get; /* RVA: 0x04D8E220 */ set; /* RVA: 0x04D8E250 */ }

        // Methods
        // RVA: 0x0A8EF0C0  token: 0x60001A0
        protected virtual System.Void OnFingerUp(PaintIn3D.P3dHitScreen.Link link) { }
        // RVA: 0x0A8EEE7C  token: 0x60001A1
        private static UnityEngine.Rect GetArea(System.Collections.Generic.List<UnityEngine.Vector2> points) { }
        // RVA: 0x04DA6050  token: 0x60001A2
        private static System.Double LineSide(UnityEngine.Vector2 a, UnityEngine.Vector2 b, UnityEngine.Vector2 p) { }
        // RVA: 0x0A8EECD8  token: 0x60001A3
        private static System.Boolean Contains(System.Collections.Generic.List<UnityEngine.Vector2> points, UnityEngine.Vector2 xy) { }
        // RVA: 0x0A8EF394  token: 0x60001A4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000035  // size: 0x88
    public class P3dHitScreenLine : PaintIn3D.P3dHitScreenBase
    {
        // Fields
        private PaintIn3D.P3dHitScreenLine.FrequencyType frequency;  // 0x70
        private System.Single interval;  // 0x74
        private System.Single position;  // 0x78
        private System.Single pixelOffset;  // 0x7c
        private PaintIn3D.P3dPointConnector connector;  // 0x80

        // Properties
        PaintIn3D.P3dHitScreenLine.FrequencyType Frequency { get; /* RVA: 0x04D868A0 */ set; /* RVA: 0x04D868D0 */ }
        System.Single Interval { get; /* RVA: 0x04DA60B0 */ set; /* RVA: 0x04DA60C0 */ }
        System.Single Position { get; /* RVA: 0x04D8ED60 */ set; /* RVA: 0x04D8ED70 */ }
        System.Single PixelOffset { get; /* RVA: 0x04D88E80 */ set; /* RVA: 0x04D8ED80 */ }
        PaintIn3D.P3dPointConnector Connector { get; /* RVA: 0x0A8EF9C0 */ }

        // Methods
        // RVA: 0x0A8EF3A8  token: 0x60001AE
        public System.Void ClearHitCache() { }
        // RVA: 0x0A8EF960  token: 0x60001AF
        public System.Void ResetConnections() { }
        // RVA: 0x0A8EF544  token: 0x60001B0
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A8EF984  token: 0x60001B1
        protected virtual System.Void Update() { }
        // RVA: 0x0A8EF3CC  token: 0x60001B2
        protected virtual System.Void HandleFingerUpdate(CW.Common.CwInputManager.Finger finger, System.Boolean down, System.Boolean up) { }
        // RVA: 0x0A8EF6BC  token: 0x60001B3
        private System.Void PaintStartEnd(CW.Common.CwInputManager.Finger finger, System.Boolean up) { }
        // RVA: 0x0A8EF7E4  token: 0x60001B4
        private System.Void PaintStartInterval(CW.Common.CwInputManager.Finger finger, System.Boolean up, System.Single pixelSpacing, System.Boolean stretch) { }
        // RVA: 0x0A8EF578  token: 0x60001B5
        private System.Void PaintOne(CW.Common.CwInputManager.Finger finger, System.Boolean up, System.Single frac, System.Single pixelOff) { }
        // RVA: 0x0A8EF9A8  token: 0x60001B6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000037  // size: 0x68
    public class P3dHitThrough : UnityEngine.MonoBehaviour
    {
        // Fields
        private PaintIn3D.P3dHitThrough.PhaseType paintIn;  // 0x18
        private System.Single interval;  // 0x1c
        private UnityEngine.Transform pointA;  // 0x20
        private UnityEngine.Transform pointB;  // 0x28
        private PaintIn3D.P3dHitThrough.OrientationType orientation;  // 0x30
        private UnityEngine.Camera _camera;  // 0x38
        private System.Single pressure;  // 0x40
        private System.Boolean preview;  // 0x44
        private System.Int32 priority;  // 0x48
        private UnityEngine.LineRenderer line;  // 0x50
        private PaintIn3D.P3dLineConnector connector;  // 0x58
        private System.Single current;  // 0x60

        // Properties
        PaintIn3D.P3dHitThrough.PhaseType PaintIn { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        System.Single Interval { get; /* RVA: 0x04D868F0 */ set; /* RVA: 0x04D86900 */ }
        UnityEngine.Transform PointA { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        UnityEngine.Transform PointB { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        PaintIn3D.P3dHitThrough.OrientationType Orientation { get; /* RVA: 0x04D864F0 */ set; /* RVA: 0x04D86740 */ }
        UnityEngine.Camera Camera { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }
        System.Single Pressure { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        System.Boolean Preview { get; /* RVA: 0x04D86A30 */ set; /* RVA: 0x04D86A40 */ }
        System.Int32 Priority { get; /* RVA: 0x04D863D0 */ set; /* RVA: 0x04D86450 */ }
        UnityEngine.LineRenderer Line { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x02FDAEA0 */ }
        PaintIn3D.P3dLineConnector Connector { get; /* RVA: 0x0A8F0C0C */ }

        // Methods
        // RVA: 0x0A8F065C  token: 0x60001CC
        public System.Void ManuallyHitNow() { }
        // RVA: 0x0A8F061C  token: 0x60001CD
        public System.Void ClearHitCache() { }
        // RVA: 0x0A8F068C  token: 0x60001CE
        public System.Void ResetConnections() { }
        // RVA: 0x0A8F0668  token: 0x60001CF
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A8F0B98  token: 0x60001D0
        protected virtual System.Void Update() { }
        // RVA: 0x0A8F0654  token: 0x60001D1
        protected virtual System.Void LateUpdate() { }
        // RVA: 0x0A8F0640  token: 0x60001D2
        protected virtual System.Void FixedUpdate() { }
        // RVA: 0x0A8F06AC  token: 0x60001D3
        private System.Void SubmitHit(System.Boolean preview) { }
        // RVA: 0x0A8F09CC  token: 0x60001D4
        private System.Void UpdateHit() { }
        // RVA: 0x0A8F0A34  token: 0x60001D5
        private System.Void UpdatePointAndLine() { }
        // RVA: 0x0A8F0BE8  token: 0x60001D6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003A
    public interface IClone
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001D7
        public virtual System.Void Transform(UnityEngine.Matrix4x4& posMatrix, UnityEngine.Matrix4x4& rotMatrix) { }

    }

    // TypeToken: 0x200003B
    public interface IHit
    {
    }

    // TypeToken: 0x200003C
    public interface IHitCoord : PaintIn3D.IHit
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001D8
        public virtual System.Void HandleHitCoord(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, PaintIn3D.P3dHit hit, UnityEngine.Quaternion rotation) { }

    }

    // TypeToken: 0x200003D
    public interface IHitLine : PaintIn3D.IHit
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001D9
        public virtual System.Void HandleHitLine(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Quaternion rotation, System.Boolean clip) { }

    }

    // TypeToken: 0x200003E
    public interface IHitPoint : PaintIn3D.IHit
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001DA
        public virtual System.Void HandleHitPoint(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation) { }

    }

    // TypeToken: 0x200003F
    public interface IHitQuad : PaintIn3D.IHit
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001DB
        public virtual System.Void HandleHitQuad(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Vector3 position2, UnityEngine.Vector3 endPosition2, UnityEngine.Quaternion rotation, System.Boolean clip) { }

    }

    // TypeToken: 0x2000040
    public interface IHitTriangle : PaintIn3D.IHit
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001DC
        public virtual System.Void HandleHitTriangle(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 positionA, UnityEngine.Vector3 positionB, UnityEngine.Vector3 positionC, UnityEngine.Quaternion rotation) { }

    }

    // TypeToken: 0x2000041
    public interface IBrowsable
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001DD
        public virtual System.String GetCategory() { }
        // RVA: -1  // abstract  token: 0x60001DE
        public virtual System.String GetTitle() { }
        // RVA: -1  // abstract  token: 0x60001DF
        public virtual UnityEngine.Texture2D GetIcon() { }
        // RVA: -1  // abstract  token: 0x60001E0
        public virtual UnityEngine.Object GetObject() { }

    }

    // TypeToken: 0x2000042  // size: 0x88
    public class P3dHitScreen_InEditor : PaintIn3D.P3dHitScreen
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60001E1
        protected virtual System.Void DoQuery(UnityEngine.Vector2 screenPosition, UnityEngine.Camera& camera, UnityEngine.Ray& ray, UnityEngine.RaycastHit& hit3D, UnityEngine.RaycastHit2D& hit2D) { }
        // RVA: 0x041E1670  token: 0x60001E2
        protected virtual System.Void LateUpdate() { }
        // RVA: 0x0A8EFD48  token: 0x60001E3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000043  // size: 0x30
    public class P3dMaterial : UnityEngine.MonoBehaviour, PaintIn3D.IBrowsable
    {
        // Fields
        private System.String category;  // 0x18
        private UnityEngine.Texture2D icon;  // 0x20
        private System.Collections.Generic.List<UnityEngine.Texture> textures;  // 0x28
        private static System.Collections.Generic.List<PaintIn3D.P3dMaterial> cachedMaterials;  // static @ 0x0

        // Properties
        System.String Category { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        UnityEngine.Texture2D Icon { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.Collections.Generic.List<UnityEngine.Texture> Textures { get; /* RVA: 0x0A8F0D7C */ }
        System.Collections.Generic.List<PaintIn3D.P3dMaterial> CachedMaterials { get; /* RVA: 0x0A8F0CC8 */ }

        // Methods
        // RVA: 0x0A8F0C7C  token: 0x60001EA
        public static System.Void ClearCache() { }
        // RVA: 0x0385B100  token: 0x60001EB
        public virtual System.String GetCategory() { }
        // RVA: 0x05396F58  token: 0x60001EC
        public virtual System.String GetTitle() { }
        // RVA: 0x04D862C0  token: 0x60001ED
        public virtual UnityEngine.Texture2D GetIcon() { }
        // RVA: 0x0464E630  token: 0x60001EE
        public virtual UnityEngine.Object GetObject() { }
        // RVA: 0x05393520  token: 0x60001EF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000044  // size: 0x28
    public class P3dPreset : UnityEngine.MonoBehaviour
    {
        // Fields
        private System.String title;  // 0x18
        private System.Boolean addMaterialCloner;  // 0x20
        private static System.Collections.Generic.List<PaintIn3D.P3dPreset> cachedPresets;  // static @ 0x0

        // Properties
        System.String Title { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Boolean AddMaterialCloner { get; /* RVA: 0x04D866B0 */ set; /* RVA: 0x04D866D0 */ }

        // Methods
        // RVA: 0x04D84C40  token: 0x60001F4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000045  // size: 0x40
    public class P3dSeamFixer : UnityEngine.ScriptableObject
    {
        // Fields
        private UnityEngine.Mesh source;  // 0x18
        private UnityEngine.Mesh mesh;  // 0x20
        private System.Collections.Generic.List<PaintIn3D.P3dSeamFixer.Pair> meshes;  // 0x28
        private PaintIn3D.P3dCoord coord;  // 0x30
        private System.Single threshold;  // 0x34
        private System.Single border;  // 0x38
        private static System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh> cacheFirst;  // static @ 0x0
        private static System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh> cacheSecond;  // static @ 0x8
        private static System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh> cacheThird;  // static @ 0x10
        private static System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh> cacheFourth;  // static @ 0x18

        // Properties
        System.Collections.Generic.List<PaintIn3D.P3dSeamFixer.Pair> Meshes { get; /* RVA: 0x0A8F5C24 */ }
        PaintIn3D.P3dCoord Coord { get; /* RVA: 0x04D864F0 */ set; /* RVA: 0x04D86740 */ }
        System.Single Threshold { get; /* RVA: 0x04D8E050 */ set; /* RVA: 0x04D8E060 */ }
        System.Single Border { get; /* RVA: 0x04D88360 */ set; /* RVA: 0x04D88370 */ }

        // Methods
        // RVA: 0x0A8F5524  token: 0x60001FC
        public static UnityEngine.Mesh GetCachedMesh(UnityEngine.Mesh source, PaintIn3D.P3dCoord coord, System.Boolean allowGeneration) { }
        // RVA: 0x0A8F58F4  token: 0x60001FD
        private static UnityEngine.Mesh TryGetCachedMesh(System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh> cache, UnityEngine.Mesh source, PaintIn3D.P3dCoord coord, System.Boolean allowGeneration) { }
        // RVA: 0x0A8F27C8  token: 0x60001FE
        public System.Void AddMesh(UnityEngine.Mesh mesh) { }
        // RVA: 0x0A8F2BE4  token: 0x60001FF
        public System.Void ConvertLegacy() { }
        // RVA: 0x0A8F51E4  token: 0x6000200
        public System.Void Generate() { }
        // RVA: 0x0A8F440C  token: 0x6000201
        public static System.Void Generate(UnityEngine.Mesh source, UnityEngine.Mesh output, PaintIn3D.P3dCoord coord, System.Single threshold, System.Single border) { }
        // RVA: 0x0A8F26DC  token: 0x6000202
        private static System.Void AddCoord(System.Collections.Generic.List<UnityEngine.Vector4> coords, PaintIn3D.P3dSeamFixer.Insertion insertion, System.Boolean write) { }
        // RVA: 0x0A8F2CD0  token: 0x6000203
        private static System.Void FixSeams(UnityEngine.Mesh source, UnityEngine.Mesh output, System.Collections.Generic.List<System.Collections.Generic.List<System.Int32>> submeshes, System.Collections.Generic.List<PaintIn3D.P3dSeamFixer.Ring> rings, System.Collections.Generic.List<PaintIn3D.P3dSeamFixer.Insertion> insertions, PaintIn3D.P3dCoord coord) { }
        // RVA: 0x0A8F57D4  token: 0x6000204
        private static System.Void TraceEdges(PaintIn3D.P3dSeamFixer.Ring ring, PaintIn3D.P3dSeamFixer.Point point) { }
        // RVA: 0x0A8F2880  token: 0x6000205
        private static System.Void AddPoint(System.Collections.Generic.Dictionary<UnityEngine.Vector2,PaintIn3D.P3dSeamFixer.Point> points, UnityEngine.Vector2 coord, System.Int32 index) { }
        // RVA: 0x0A8F2A2C  token: 0x6000206
        private static System.Void AddTriangle(System.Collections.Generic.List<PaintIn3D.P3dSeamFixer.Edge> edges, PaintIn3D.P3dSeamFixer.Point pointA, PaintIn3D.P3dSeamFixer.Point pointB, PaintIn3D.P3dSeamFixer.Point pointC) { }
        // RVA: 0x0A8F2980  token: 0x6000207
        private static System.Void AddTriangle2(System.Collections.Generic.List<PaintIn3D.P3dSeamFixer.Edge> edges, PaintIn3D.P3dSeamFixer.Point pointA, PaintIn3D.P3dSeamFixer.Point pointB, PaintIn3D.P3dSeamFixer.Point pointC, System.Boolean flip) { }
        // RVA: 0x0A8F5628  token: 0x6000208
        private static System.Void RemoveOrAddEdge(System.Collections.Generic.List<PaintIn3D.P3dSeamFixer.Edge> edges, PaintIn3D.P3dSeamFixer.Point pointA, PaintIn3D.P3dSeamFixer.Point pointB, System.Boolean flip) { }
        // RVA: 0x0A8F5C0C  token: 0x6000209
        public System.Void .ctor() { }
        // RVA: 0x0A8F5A90  token: 0x600020A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200004B  // size: 0x28
    public class P3dShape : UnityEngine.MonoBehaviour, PaintIn3D.IBrowsable
    {
        // Fields
        private System.String category;  // 0x18
        private UnityEngine.Texture2D icon;  // 0x20
        private static System.Collections.Generic.List<PaintIn3D.P3dShape> cachedShapes;  // static @ 0x0

        // Properties
        System.String Category { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        UnityEngine.Texture2D Icon { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.Collections.Generic.List<PaintIn3D.P3dShape> CachedShapes { get; /* RVA: 0x0A8F5CF4 */ }

        // Methods
        // RVA: 0x0A8F5CA8  token: 0x6000217
        public static System.Void ClearCache() { }
        // RVA: 0x0385B100  token: 0x6000218
        public virtual System.String GetCategory() { }
        // RVA: 0x05396F58  token: 0x6000219
        public virtual System.String GetTitle() { }
        // RVA: 0x04D862C0  token: 0x600021A
        public virtual UnityEngine.Texture2D GetIcon() { }
        // RVA: 0x0464E630  token: 0x600021B
        public virtual UnityEngine.Object GetObject() { }
        // RVA: 0x05393520  token: 0x600021C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004C  // size: 0x28
    public class P3dTool : UnityEngine.MonoBehaviour, PaintIn3D.IBrowsable
    {
        // Fields
        private System.String category;  // 0x18
        private UnityEngine.Texture2D icon;  // 0x20
        private static System.Collections.Generic.List<PaintIn3D.P3dTool> cachedTools;  // static @ 0x0

        // Properties
        System.String Category { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        UnityEngine.Texture2D Icon { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.Collections.Generic.List<PaintIn3D.P3dTool> CachedTools { get; /* RVA: 0x0A8F5DF4 */ }

        // Methods
        // RVA: 0x0A8F5DA8  token: 0x6000222
        public static System.Void ClearCache() { }
        // RVA: 0x0385B100  token: 0x6000223
        public virtual System.String GetCategory() { }
        // RVA: 0x05396F58  token: 0x6000224
        public virtual System.String GetTitle() { }
        // RVA: 0x04D862C0  token: 0x6000225
        public virtual UnityEngine.Texture2D GetIcon() { }
        // RVA: 0x0464E630  token: 0x6000226
        public virtual UnityEngine.Object GetObject() { }
        // RVA: 0x05393520  token: 0x6000227
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004D  // size: 0x28
    public class P3dModifyAngleRandom : PaintIn3D.P3dModifier
    {
        // Fields
        public static System.String Group;  // static @ 0x0
        public static System.String Title;  // static @ 0x8
        private System.Single min;  // 0x18
        private System.Single max;  // 0x1c
        private PaintIn3D.P3dModifyAngleRandom.BlendType blend;  // 0x20

        // Properties
        System.Single Min { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x04D86390 */ }
        System.Single Max { get; /* RVA: 0x04D868F0 */ set; /* RVA: 0x04D86900 */ }
        PaintIn3D.P3dModifyAngleRandom.BlendType Blend { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }

        // Methods
        // RVA: 0x0A8F0E00  token: 0x600022E
        protected virtual System.Void OnModifyAngle(System.Single& angle, System.Single pressure) { }
        // RVA: 0x04DA60D0  token: 0x600022F
        public System.Void .ctor() { }
        // RVA: 0x0A8F0E4C  token: 0x6000230
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200004F  // size: 0x28
    public class P3dModifyColorRandom : PaintIn3D.P3dModifier
    {
        // Fields
        public static System.String Group;  // static @ 0x0
        public static System.String Title;  // static @ 0x8
        private UnityEngine.Gradient gradient;  // 0x18
        private PaintIn3D.P3dModifyColorRandom.BlendType blend;  // 0x20

        // Properties
        UnityEngine.Gradient Gradient { get; /* RVA: 0x0A8F1038 */ }
        PaintIn3D.P3dModifyColorRandom.BlendType Blend { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }

        // Methods
        // RVA: 0x0A8F0EE4  token: 0x6000234
        protected virtual System.Void OnModifyColor(UnityEngine.Color& color, System.Single pressure) { }
        // RVA: 0x04DA60F0  token: 0x6000235
        public System.Void .ctor() { }
        // RVA: 0x0A8F0FA0  token: 0x6000236
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000051  // size: 0x20
    public class P3dModifyHardnessPressure : PaintIn3D.P3dModifier
    {
        // Fields
        public static System.String Group;  // static @ 0x0
        public static System.String Title;  // static @ 0x8
        private System.Single hardness;  // 0x18
        private PaintIn3D.P3dModifyHardnessPressure.BlendType blend;  // 0x1c

        // Properties
        System.Single Hardness { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x04D86390 */ }
        PaintIn3D.P3dModifyHardnessPressure.BlendType Blend { get; /* RVA: 0x04D86310 */ set; /* RVA: 0x04D86320 */ }

        // Methods
        // RVA: 0x0A8F10AC  token: 0x600023B
        protected virtual System.Void OnModifyHardness(System.Single& currentHardness, System.Single pressure) { }
        // RVA: 0x04DA6100  token: 0x600023C
        public System.Void .ctor() { }
        // RVA: 0x0A8F10F0  token: 0x600023D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000053  // size: 0x28
    public class P3dModifyHardnessRandom : PaintIn3D.P3dModifier
    {
        // Fields
        public static System.String Group;  // static @ 0x0
        public static System.String Title;  // static @ 0x8
        private System.Single min;  // 0x18
        private System.Single max;  // 0x1c
        private PaintIn3D.P3dModifyHardnessRandom.BlendType blend;  // 0x20

        // Properties
        System.Single Min { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x04D86390 */ }
        System.Single Max { get; /* RVA: 0x04D868F0 */ set; /* RVA: 0x04D86900 */ }
        PaintIn3D.P3dModifyHardnessRandom.BlendType Blend { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }

        // Methods
        // RVA: 0x0A8F0E00  token: 0x6000244
        protected virtual System.Void OnModifyHardness(System.Single& hardness, System.Single pressure) { }
        // RVA: 0x04DA6110  token: 0x6000245
        public System.Void .ctor() { }
        // RVA: 0x0A8F1188  token: 0x6000246
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000055  // size: 0x20
    public class P3dModifyOpacityPressure : PaintIn3D.P3dModifier
    {
        // Fields
        public static System.String Group;  // static @ 0x0
        public static System.String Title;  // static @ 0x8
        private System.Single opacity;  // 0x18
        private PaintIn3D.P3dModifyOpacityPressure.BlendType blend;  // 0x1c

        // Properties
        System.Single Opacity { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x04D86390 */ }
        PaintIn3D.P3dModifyOpacityPressure.BlendType Blend { get; /* RVA: 0x04D86310 */ set; /* RVA: 0x04D86320 */ }

        // Methods
        // RVA: 0x0A8F10AC  token: 0x600024B
        protected virtual System.Void OnModifyOpacity(System.Single& currentOpacity, System.Single pressure) { }
        // RVA: 0x04DA6100  token: 0x600024C
        public System.Void .ctor() { }
        // RVA: 0x0A8F1220  token: 0x600024D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000057  // size: 0x28
    public class P3dModifyOpacityRandom : PaintIn3D.P3dModifier
    {
        // Fields
        public static System.String Group;  // static @ 0x0
        public static System.String Title;  // static @ 0x8
        private System.Single min;  // 0x18
        private System.Single max;  // 0x1c
        private PaintIn3D.P3dModifyOpacityRandom.BlendType blend;  // 0x20

        // Properties
        System.Single Min { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x04D86390 */ }
        System.Single Max { get; /* RVA: 0x04D868F0 */ set; /* RVA: 0x04D86900 */ }
        PaintIn3D.P3dModifyOpacityRandom.BlendType Blend { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }

        // Methods
        // RVA: 0x0A8F0E00  token: 0x6000254
        protected virtual System.Void OnModifyOpacity(System.Single& opacity, System.Single pressure) { }
        // RVA: 0x04DA6110  token: 0x6000255
        public System.Void .ctor() { }
        // RVA: 0x0A8F12B8  token: 0x6000256
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000059  // size: 0x20
    public class P3dModifyPositionRandom : PaintIn3D.P3dModifier
    {
        // Fields
        public static System.String Group;  // static @ 0x0
        public static System.String Title;  // static @ 0x8
        private System.Single radius;  // 0x18

        // Properties
        System.Single Radius { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x04D86390 */ }

        // Methods
        // RVA: 0x0A8F1350  token: 0x6000259
        protected virtual System.Void OnModifyPosition(UnityEngine.Vector3& position, System.Single pressure) { }
        // RVA: 0x04DA6100  token: 0x600025A
        public System.Void .ctor() { }
        // RVA: 0x0A8F13F8  token: 0x600025B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200005A  // size: 0x20
    public class P3dModifyRadiusPressure : PaintIn3D.P3dModifier
    {
        // Fields
        public static System.String Group;  // static @ 0x0
        public static System.String Title;  // static @ 0x8
        private System.Single radius;  // 0x18
        private PaintIn3D.P3dModifyRadiusPressure.BlendType blend;  // 0x1c

        // Properties
        System.Single Radius { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x04D86390 */ }
        PaintIn3D.P3dModifyRadiusPressure.BlendType Blend { get; /* RVA: 0x04D86310 */ set; /* RVA: 0x04D86320 */ }

        // Methods
        // RVA: 0x0A8F10AC  token: 0x6000260
        protected virtual System.Void OnModifyRadius(System.Single& currentRadius, System.Single pressure) { }
        // RVA: 0x04DA6100  token: 0x6000261
        public System.Void .ctor() { }
        // RVA: 0x0A8F1490  token: 0x6000262
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200005C  // size: 0x28
    public class P3dModifyRadiusRandom : PaintIn3D.P3dModifier
    {
        // Fields
        public static System.String Group;  // static @ 0x0
        public static System.String Title;  // static @ 0x8
        private System.Single min;  // 0x18
        private System.Single max;  // 0x1c
        private PaintIn3D.P3dModifyRadiusRandom.BlendType blend;  // 0x20

        // Properties
        System.Single Min { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x04D86390 */ }
        System.Single Max { get; /* RVA: 0x04D868F0 */ set; /* RVA: 0x04D86900 */ }
        PaintIn3D.P3dModifyRadiusRandom.BlendType Blend { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }

        // Methods
        // RVA: 0x0A8F0E00  token: 0x6000269
        protected virtual System.Void OnModifyRadius(System.Single& radius, System.Single pressure) { }
        // RVA: 0x04DA6110  token: 0x600026A
        public System.Void .ctor() { }
        // RVA: 0x0A8F1528  token: 0x600026B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200005E  // size: 0x38
    public class P3dModifyScaleRandom : PaintIn3D.P3dModifier
    {
        // Fields
        public static System.String Group;  // static @ 0x0
        public static System.String Title;  // static @ 0x8
        private UnityEngine.Vector3 min;  // 0x18
        private UnityEngine.Vector3 max;  // 0x24
        private PaintIn3D.P3dModifyScaleRandom.BlendType blend;  // 0x30
        private System.Boolean uniform;  // 0x34

        // Properties
        UnityEngine.Vector3 Min { get; /* RVA: 0x04D89870 */ set; /* RVA: 0x04D89890 */ }
        UnityEngine.Vector3 Max { get; /* RVA: 0x04D905E0 */ set; /* RVA: 0x04D905D0 */ }
        PaintIn3D.P3dModifyScaleRandom.BlendType Blend { get; /* RVA: 0x04D864F0 */ set; /* RVA: 0x04D86740 */ }
        System.Boolean Uniform { get; /* RVA: 0x04D86A90 */ set; /* RVA: 0x04D86AA0 */ }

        // Methods
        // RVA: 0x0A8F15C0  token: 0x6000274
        protected virtual System.Void OnModifyScale(UnityEngine.Vector3& scale, System.Single pressure) { }
        // RVA: 0x04DA6130  token: 0x6000275
        public System.Void .ctor() { }
        // RVA: 0x0A8F1770  token: 0x6000276
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000060  // size: 0x28
    public class P3dModifyTexturePressure : PaintIn3D.P3dModifier
    {
        // Fields
        public static System.String Group;  // static @ 0x0
        public static System.String Title;  // static @ 0x8
        private UnityEngine.Texture texture;  // 0x18
        private System.Single pressureMin;  // 0x20
        private System.Single pressureMax;  // 0x24

        // Properties
        UnityEngine.Texture Texture { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Single PressureMin { get; /* RVA: 0x011EC8A0 */ set; /* RVA: 0x011EC9E0 */ }
        System.Single PressureMax { get; /* RVA: 0x04D89850 */ set; /* RVA: 0x04D89860 */ }

        // Methods
        // RVA: 0x0A8F1808  token: 0x600027D
        protected virtual System.Void OnModifyTexture(UnityEngine.Texture& currentTexture, System.Single pressure) { }
        // RVA: 0x04DA6170  token: 0x600027E
        public System.Void .ctor() { }
        // RVA: 0x0A8F1828  token: 0x600027F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000061  // size: 0x20
    public class P3dModifyTextureRandom : PaintIn3D.P3dModifier
    {
        // Fields
        public static System.String Group;  // static @ 0x0
        public static System.String Title;  // static @ 0x8
        private System.Collections.Generic.List<UnityEngine.Texture> textures;  // 0x18

        // Properties
        System.Collections.Generic.List<UnityEngine.Texture> Textures { get; /* RVA: 0x0A8F19E4 */ }

        // Methods
        // RVA: 0x0A8F18C0  token: 0x6000281
        protected virtual System.Void OnModifyTexture(UnityEngine.Texture& texture, System.Single pressure) { }
        // RVA: 0x04DA60F0  token: 0x6000282
        public System.Void .ctor() { }
        // RVA: 0x0A8F194C  token: 0x6000283
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000062  // size: 0x48
    public sealed struct P3dBlendMode
    {
        // Fields
        public static System.Int32 ALPHA_BLEND;  // const
        public static System.Int32 ALPHA_BLEND_INVERSE;  // const
        public static System.Int32 PREMULTIPLIED;  // const
        public static System.Int32 ADDITIVE;  // const
        public static System.Int32 ADDITIVE_SOFT;  // const
        public static System.Int32 SUBTRACTIVE;  // const
        public static System.Int32 SUBTRACTIVE_SOFT;  // const
        public static System.Int32 REPLACE;  // const
        public static System.Int32 REPLACE_ORIGINAL;  // const
        public static System.Int32 REPLACE_CUSTOM;  // const
        public static System.Int32 MULTIPLY_INVERSE_RGB;  // const
        public static System.Int32 BLUR;  // const
        public static System.Int32 NORMAL_BLEND;  // const
        public static System.Int32 NORMAL_REPLACE;  // const
        public static System.Int32 FLOW;  // const
        public static System.Int32 NORMAL_REPLACE_ORIGINAL;  // const
        public static System.Int32 NORMAL_REPLACE_CUSTOM;  // const
        public static System.Int32 COUNT;  // const
        public static readonly System.String[] NAMES;  // static @ 0x0
        public System.Int32 Index;  // 0x10
        public UnityEngine.Color Color;  // 0x14
        public UnityEngine.Texture Texture;  // 0x28
        public System.Single Kernel;  // 0x30
        public UnityEngine.Vector4 Channels;  // 0x34
        private static System.Int32 _Channels;  // static @ 0x8
        private static System.Int32 _ReplaceColor;  // static @ 0xc
        private static System.Int32 _ReplaceTexture;  // static @ 0x10
        private static System.Int32 _ReplaceTextureSize;  // static @ 0x14
        private static System.Int32 _Kernel;  // static @ 0x18

        // Methods
        // RVA: 0x0A8E36E4  token: 0x6000284
        public static PaintIn3D.P3dBlendMode AlphaBlend(UnityEngine.Vector4 channels) { }
        // RVA: 0x0A8E369C  token: 0x6000285
        public static PaintIn3D.P3dBlendMode AlphaBlendInverse(UnityEngine.Vector4 channels) { }
        // RVA: 0x0A8E3C94  token: 0x6000286
        public static PaintIn3D.P3dBlendMode Premultiplied(UnityEngine.Vector4 channels) { }
        // RVA: 0x0A8E3654  token: 0x6000287
        public static PaintIn3D.P3dBlendMode Additive(UnityEngine.Vector4 channels) { }
        // RVA: 0x0A8E360C  token: 0x6000288
        public static PaintIn3D.P3dBlendMode AdditiveSoft(UnityEngine.Vector4 channels) { }
        // RVA: 0x0A8E3E04  token: 0x6000289
        public static PaintIn3D.P3dBlendMode Subtractive(UnityEngine.Vector4 channels) { }
        // RVA: 0x0A8E3DBC  token: 0x600028A
        public static PaintIn3D.P3dBlendMode SubtractiveSoft(UnityEngine.Vector4 channels) { }
        // RVA: 0x0A8E3D74  token: 0x600028B
        public static PaintIn3D.P3dBlendMode Replace(UnityEngine.Vector4 channels) { }
        // RVA: 0x0A8E3D2C  token: 0x600028C
        public static PaintIn3D.P3dBlendMode ReplaceOriginal(UnityEngine.Vector4 channels) { }
        // RVA: 0x0A8E3CDC  token: 0x600028D
        public static PaintIn3D.P3dBlendMode ReplaceCustom(UnityEngine.Color color, UnityEngine.Texture texture, UnityEngine.Vector4 channels) { }
        // RVA: 0x0A8E3B24  token: 0x600028E
        public static PaintIn3D.P3dBlendMode MultiplyInverseRGB(UnityEngine.Vector4 channels) { }
        // RVA: 0x0A8E3948  token: 0x600028F
        public static PaintIn3D.P3dBlendMode Blur(System.Single kernel, UnityEngine.Vector4 channels) { }
        // RVA: 0x0A8E3B6C  token: 0x6000290
        public static PaintIn3D.P3dBlendMode NormalBlend(UnityEngine.Vector4 channels) { }
        // RVA: 0x0A8E3C4C  token: 0x6000291
        public static PaintIn3D.P3dBlendMode NormalReplace(UnityEngine.Vector4 channels) { }
        // RVA: 0x0A8E3A08  token: 0x6000292
        public static PaintIn3D.P3dBlendMode Flow(System.Single kernel, UnityEngine.Vector4 channels) { }
        // RVA: 0x0A8E3C04  token: 0x6000293
        public static PaintIn3D.P3dBlendMode NormalReplaceOriginal(UnityEngine.Vector4 channels) { }
        // RVA: 0x0A8E3BB4  token: 0x6000294
        public static PaintIn3D.P3dBlendMode NormalReplaceCustom(UnityEngine.Color color, UnityEngine.Texture texture, UnityEngine.Vector4 channels) { }
        // RVA: 0x0A8E3728  token: 0x6000295
        public System.Void Apply(UnityEngine.Material material) { }
        // RVA: 0x0A8E3AB8  token: 0x6000296
        public static System.String GetName(System.Int32 index) { }
        // RVA: 0x02EED260  token: 0x6000297
        public static System.Boolean op_Equality(PaintIn3D.P3dBlendMode a, System.Int32 b) { }
        // RVA: 0x0A8E41BC  token: 0x6000298
        public static System.Boolean op_Inequality(PaintIn3D.P3dBlendMode a, System.Int32 b) { }
        // RVA: 0x02B77620  token: 0x6000299
        public static System.Int32 op_Implicit(PaintIn3D.P3dBlendMode a) { }
        // RVA: 0x0A8E3A4C  token: 0x600029A
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0A8E398C  token: 0x600029B
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0A8E3E4C  token: 0x600029C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000063  // size: 0x10
    public static class P3dBlit
    {
        // Fields
        private static UnityEngine.Material cachedWhite;  // static @ 0x0
        private static System.Boolean cachedWhiteSet;  // static @ 0x8
        private static UnityEngine.Material cachedTexture;  // static @ 0x10
        private static System.Boolean cachedTextureSet;  // static @ 0x18
        private static UnityEngine.Material cachedNormal;  // static @ 0x20
        private static System.Boolean cachedNormalSet;  // static @ 0x28
        private static UnityEngine.Material cachedPremultiply;  // static @ 0x30
        private static System.Boolean cachedPremultiplySet;  // static @ 0x38
        private static System.Int32 _Buffer;  // static @ 0x3c
        private static System.Int32 _BufferSize;  // static @ 0x40
        private static System.Int32 _Texture;  // static @ 0x44
        private static System.Int32 _Color;  // static @ 0x48

        // Methods
        // RVA: 0x0A8F69E4  token: 0x600029D
        public static System.Void White(UnityEngine.RenderTexture rendertexture, UnityEngine.Mesh mesh, System.Int32 submesh, PaintIn3D.P3dCoord coord) { }
        // RVA: 0x0A8F6760  token: 0x600029E
        public static System.Void Texture(UnityEngine.RenderTexture rendertexture, UnityEngine.Mesh mesh, System.Int32 submesh, UnityEngine.Texture texture, PaintIn3D.P3dCoord coord) { }
        // RVA: 0x0A8F6458  token: 0x600029F
        public static System.Void Normal(UnityEngine.RenderTexture rendertexture, UnityEngine.Texture texture) { }
        // RVA: 0x0A8F65A0  token: 0x60002A0
        public static System.Void Premultiply(UnityEngine.RenderTexture rendertexture, UnityEngine.Texture texture, UnityEngine.Color tint) { }
        // RVA: 0x0A8F6C28  token: 0x60002A1
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000064  // size: 0xE0
    public class P3dChangeCounter : PaintIn3D.P3dPaintableTextureMonitorMask
    {
        // Fields
        public static System.Collections.Generic.LinkedList<PaintIn3D.P3dChangeCounter> Instances;  // static @ 0x0
        private System.Collections.Generic.LinkedListNode<PaintIn3D.P3dChangeCounter> instancesNode;  // 0x98
        private System.Single threshold;  // 0xa0
        private UnityEngine.Texture texture;  // 0xa8
        private UnityEngine.Color color;  // 0xb0
        private System.Int32 count;  // 0xc0
        private PaintIn3D.P3dReader changeReader;  // 0xc8
        protected Unity.Collections.NativeArray<UnityEngine.Color32> changePixels;  // 0xd0

        // Properties
        System.Single Threshold { get; /* RVA: 0x04D91E60 */ set; /* RVA: 0x0A8F7AD0 */ }
        UnityEngine.Texture Texture { get; /* RVA: 0x04D86290 */ set; /* RVA: 0x0A8F7A4C */ }
        UnityEngine.Color Color { get; /* RVA: 0x04DA1230 */ set; /* RVA: 0x0A8F79FC */ }
        System.Int32 Count { get; /* RVA: 0x04DA4B70 */ }
        System.Single Ratio { get; /* RVA: 0x0A8F79D8 */ }
        PaintIn3D.P3dReader ChangeReader { get; /* RVA: 0x04D8D1D0 */ }

        // Methods
        // RVA: 0x0A8F73C0  token: 0x60002AB
        public System.Void MarkChangeReaderAsDirty() { }
        // RVA: 0x0A8F6F20  token: 0x60002AC
        public static System.Int64 GetTotal(System.Collections.Generic.ICollection<PaintIn3D.P3dChangeCounter> counters) { }
        // RVA: 0x0A8F6CFC  token: 0x60002AD
        public static System.Int64 GetCount(System.Collections.Generic.ICollection<PaintIn3D.P3dChangeCounter> counters) { }
        // RVA: 0x0A8F6E94  token: 0x60002AE
        public static System.Single GetRatio(System.Collections.Generic.ICollection<PaintIn3D.P3dChangeCounter> counters) { }
        // RVA: 0x0A8F70B4  token: 0x60002AF
        private System.Void HandleCompleteChange(Unity.Collections.NativeArray<UnityEngine.Color32> pixels) { }
        // RVA: 0x0A8F721C  token: 0x60002B0
        protected virtual System.Void HandleComplete(System.Int32 boost) { }
        // RVA: 0x0A8F7558  token: 0x60002B1
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A8F7450  token: 0x60002B2
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0A8F73D8  token: 0x60002B3
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x0A8F765C  token: 0x60002B4
        protected virtual System.Void Update() { }
        // RVA: 0x0A8F799C  token: 0x60002B5
        public System.Void .ctor() { }
        // RVA: 0x0A8F7920  token: 0x60002B6
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000065  // size: 0x14
    public sealed struct P3dChannel
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dChannel Red;  // const
        public static PaintIn3D.P3dChannel Green;  // const
        public static PaintIn3D.P3dChannel Blue;  // const
        public static PaintIn3D.P3dChannel Alpha;  // const

    }

    // TypeToken: 0x2000066  // size: 0xB8
    public class P3dChannelCounter : PaintIn3D.P3dPaintableTextureMonitorMask
    {
        // Fields
        public static System.Collections.Generic.LinkedList<PaintIn3D.P3dChannelCounter> Instances;  // static @ 0x0
        private System.Collections.Generic.LinkedListNode<PaintIn3D.P3dChannelCounter> instancesNode;  // 0x98
        private System.Single threshold;  // 0xa0
        private System.Int32 countR;  // 0xa4
        private System.Int32 countG;  // 0xa8
        private System.Int32 countB;  // 0xac
        private System.Int32 countA;  // 0xb0

        // Properties
        System.Single Threshold { get; /* RVA: 0x04D91E60 */ set; /* RVA: 0x0A8F8C60 */ }
        System.Int32 CountR { get; /* RVA: 0x04D88C90 */ }
        System.Int32 CountG { get; /* RVA: 0x04D88C80 */ }
        System.Int32 CountB { get; /* RVA: 0x04D88CA0 */ }
        System.Int32 CountA { get; /* RVA: 0x04D8F8E0 */ }
        System.Single RatioR { get; /* RVA: 0x0A8F8C3C */ }
        System.Single RatioG { get; /* RVA: 0x0A8F8B64 */ }
        System.Single RatioB { get; /* RVA: 0x0A8F8B40 */ }
        System.Single RatioA { get; /* RVA: 0x0A8F8B1C */ }
        UnityEngine.Vector4 RatioRGBA { get; /* RVA: 0x0A8F8B88 */ }

        // Methods
        // RVA: 0x0A8F86A8  token: 0x60002C2
        public static System.Int64 GetTotal(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters) { }
        // RVA: 0x0A8F7FC8  token: 0x60002C3
        public static System.Int64 GetCountR(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters) { }
        // RVA: 0x0A8F7E30  token: 0x60002C4
        public static System.Int64 GetCountG(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters) { }
        // RVA: 0x0A8F7C98  token: 0x60002C5
        public static System.Int64 GetCountB(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters) { }
        // RVA: 0x0A8F7B00  token: 0x60002C6
        public static System.Int64 GetCountA(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters) { }
        // RVA: 0x0A8F861C  token: 0x60002C7
        public static System.Single GetRatioR(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters) { }
        // RVA: 0x0A8F8278  token: 0x60002C8
        public static System.Single GetRatioG(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters) { }
        // RVA: 0x0A8F81EC  token: 0x60002C9
        public static System.Single GetRatioB(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters) { }
        // RVA: 0x0A8F8160  token: 0x60002CA
        public static System.Single GetRatioA(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters) { }
        // RVA: 0x0A8F8304  token: 0x60002CB
        public static UnityEngine.Vector4 GetRatioRGBA(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters) { }
        // RVA: 0x0A8F89FC  token: 0x60002CC
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A8F896C  token: 0x60002CD
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0A8F883C  token: 0x60002CE
        protected virtual System.Void HandleComplete(System.Int32 boost) { }
        // RVA: 0x0A8F8B04  token: 0x60002CF
        public System.Void .ctor() { }
        // RVA: 0x0A8F8A88  token: 0x60002D0
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000067  // size: 0x20
    public abstract class P3dClone : UnityEngine.MonoBehaviour, PaintIn3D.IClone
    {
        // Fields
        public static System.Int32 MatrixCount;  // static @ 0x0
        public static System.Int32 ClonerCount;  // static @ 0x4
        private static System.Collections.Generic.List<UnityEngine.Matrix4x4> tempPosMatrices;  // static @ 0x8
        private static System.Collections.Generic.List<UnityEngine.Matrix4x4> tempRotMatrices;  // static @ 0x10
        private static System.Collections.Generic.List<PaintIn3D.IClone> tempCloners;  // static @ 0x18
        private static System.Collections.Generic.LinkedList<PaintIn3D.P3dClone> instances;  // static @ 0x20
        private System.Collections.Generic.LinkedListNode<PaintIn3D.P3dClone> instancesNode;  // 0x18

        // Properties
        System.Collections.Generic.LinkedList<PaintIn3D.P3dClone> Instances { get; /* RVA: 0x0A8F9D8C */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60002D1
        public virtual System.Void Transform(UnityEngine.Matrix4x4& posMatrix, UnityEngine.Matrix4x4& rotMatrix) { }
        // RVA: 0x0A8F93CC  token: 0x60002D3
        public static System.Void BuildCloners(System.Collections.Generic.List<PaintIn3D.IClone> cloners) { }
        // RVA: 0x0A8F9810  token: 0x60002D4
        public static System.Void Clone(PaintIn3D.P3dCommand command, System.Int32 clonerIndex, System.Int32 matrixIndex) { }
        // RVA: 0x0A8F9B74  token: 0x60002D5
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A8F9AF4  token: 0x60002D6
        protected virtual System.Void OnDisable() { }
        // RVA: 0x05393520  token: 0x60002D7
        protected System.Void .ctor() { }
        // RVA: 0x0A8F9BF0  token: 0x60002D8
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000068  // size: 0x28
    public class P3dCloneMirror : PaintIn3D.P3dClone
    {
        // Fields
        private System.Boolean flip;  // 0x20

        // Properties
        System.Boolean Flip { get; /* RVA: 0x04D866B0 */ set; /* RVA: 0x04D866D0 */ }

        // Methods
        // RVA: 0x0A8F8C88  token: 0x60002DB
        public virtual System.Void Transform(UnityEngine.Matrix4x4& posMatrix, UnityEngine.Matrix4x4& rotMatrix) { }
        // RVA: 0x0A8F9378  token: 0x60002DC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000069  // size: 0x38
    public class P3dColor : UnityEngine.MonoBehaviour
    {
        // Fields
        private UnityEngine.Color color;  // 0x18
        private System.Collections.Generic.List<PaintIn3D.P3dColor.Contribution> contributions;  // 0x28
        private static System.Collections.Generic.LinkedList<PaintIn3D.P3dColor> instances;  // static @ 0x0
        private System.Collections.Generic.LinkedListNode<PaintIn3D.P3dColor> instancesNode;  // 0x30

        // Properties
        UnityEngine.Color Color { get; /* RVA: 0x04D890C0 */ set; /* RVA: 0x04D890D0 */ }
        System.Collections.Generic.LinkedList<PaintIn3D.P3dColor> Instances { get; /* RVA: 0x0A8FB340 */ }
        System.Int32 Total { get; /* RVA: 0x0A8FB4C8 */ }
        System.Int32 Solid { get; /* RVA: 0x0A8FB3D8 */ }
        System.Single Ratio { get; /* RVA: 0x0A8FB390 */ }

        // Methods
        // RVA: 0x0A8FAFDC  token: 0x60002E3
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A8FAE14  token: 0x60002E4
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0A8FAD48  token: 0x60002E5
        public System.Void Contribute(PaintIn3D.P3dColorCounter counter, System.Int32 solid) { }
        // RVA: 0x0A8FB194  token: 0x60002E6
        private System.Boolean TryGetContribution(PaintIn3D.P3dColorCounter counter, PaintIn3D.P3dColor.Contribution& contribution) { }
        // RVA: 0x05393520  token: 0x60002E7
        public System.Void .ctor() { }
        // RVA: 0x0A8FB2C4  token: 0x60002E8
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200006B  // size: 0xB0
    public class P3dColorCounter : PaintIn3D.P3dPaintableTextureMonitorMask
    {
        // Fields
        public static System.Collections.Generic.LinkedList<PaintIn3D.P3dColorCounter> Instances;  // static @ 0x0
        private System.Collections.Generic.LinkedListNode<PaintIn3D.P3dColorCounter> instancesNode;  // 0x98
        private System.Single threshold;  // 0xa0
        private System.Collections.Generic.List<PaintIn3D.P3dColorCounter.Contribution> contributions;  // 0xa8

        // Properties
        System.Single Threshold { get; /* RVA: 0x04D91E60 */ set; /* RVA: 0x0A8F8C60 */ }
        System.Collections.Generic.List<PaintIn3D.P3dColorCounter.Contribution> Contributions { get; /* RVA: 0x04D86290 */ }

        // Methods
        // RVA: 0x0A8FA420  token: 0x60002ED
        public static System.Int64 GetTotal(System.Collections.Generic.ICollection<PaintIn3D.P3dColorCounter> counters) { }
        // RVA: 0x0A8FA1E0  token: 0x60002EE
        public static System.Int64 GetCount(PaintIn3D.P3dColor color, System.Collections.Generic.ICollection<PaintIn3D.P3dColorCounter> counters) { }
        // RVA: 0x0A8FA390  token: 0x60002EF
        public static System.Single GetRatio(PaintIn3D.P3dColor color, System.Collections.Generic.ICollection<PaintIn3D.P3dColorCounter> counters) { }
        // RVA: 0x0A8FA07C  token: 0x60002F0
        public System.Int32 Count(PaintIn3D.P3dColor color) { }
        // RVA: 0x0A8FAC04  token: 0x60002F1
        public System.Single Ratio(PaintIn3D.P3dColor color) { }
        // RVA: 0x0A8FA8AC  token: 0x60002F2
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A8FA810  token: 0x60002F3
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0A8FA5B4  token: 0x60002F4
        protected virtual System.Void HandleComplete(System.Int32 boost) { }
        // RVA: 0x0A8F9DDC  token: 0x60002F5
        private System.Void ClearContributions() { }
        // RVA: 0x0A8FA938  token: 0x60002F6
        private System.Void PrepareContributions() { }
        // RVA: 0x0A8F9ECC  token: 0x60002F7
        private System.Void Contribute(System.Int32 scale) { }
        // RVA: 0x0A8FACB8  token: 0x60002F8
        public System.Void .ctor() { }
        // RVA: 0x0A8FAC3C  token: 0x60002F9
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200006D  // size: 0x70
    public abstract class P3dCommand
    {
        // Fields
        public System.Int32 Index;  // 0x10
        public System.Boolean Preview;  // 0x14
        public System.Int32 Priority;  // 0x18
        public PaintIn3D.P3dHashedMaterial Material;  // 0x20
        public System.Int32 Pass;  // 0x30
        public PaintIn3D.P3dHashedModel Model;  // 0x38
        public System.Int32 Submesh;  // 0x48
        public PaintIn3D.P3dHashedTexture LocalMaskTexture;  // 0x50
        public UnityEngine.Vector4 LocalMaskChannel;  // 0x60
        private static System.Int32 _LocalMaskTexture;  // static @ 0x0
        private static System.Int32 _LocalMaskChannel;  // static @ 0x4

        // Properties
        System.Boolean RequireMesh { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: 0x0A8FB780  token: 0x60002FC
        public static System.Void BuildMaterial(UnityEngine.Material& material, System.Int32& materialHash, System.String path, System.String keyword) { }
        // RVA: 0x0A8FB820  token: 0x60002FD
        public static System.Int32 Compare(PaintIn3D.P3dCommand a, PaintIn3D.P3dCommand b) { }
        // RVA: 0x04DA6190  token: 0x60002FF
        public System.Void SetState(System.Boolean preview, System.Int32 priority) { }
        // RVA: 0x0A8FB630  token: 0x6000300
        public virtual System.Void Apply(UnityEngine.Material material) { }
        // RVA: -1  // abstract  token: 0x6000301
        public virtual System.Void Pool() { }
        // RVA: -1  // abstract  token: 0x6000302
        public virtual System.Void Transform(UnityEngine.Matrix4x4 posMatrix, UnityEngine.Matrix4x4 rotMatrix) { }
        // RVA: -1  // abstract  token: 0x6000303
        public virtual PaintIn3D.P3dCommand SpawnCopy() { }
        // RVA: 0x0A8FB87C  token: 0x6000304
        public PaintIn3D.P3dCommand SpawnCopyLocal(UnityEngine.Transform transform) { }
        // RVA: 0x0A8FB9BC  token: 0x6000305
        public PaintIn3D.P3dCommand SpawnCopyWorld(UnityEngine.Transform transform) { }
        // RVA: -1  // generic def  token: 0x6000306
        protected T SpawnCopy(System.Collections.Generic.Stack<T> pool) { }
        // RVA: 0x0A8FB6EC  token: 0x6000307
        public virtual System.Void Apply(PaintIn3D.P3dPaintableTexture paintableTexture) { }
        // RVA: 0x041E1670  token: 0x6000308
        protected System.Void .ctor() { }
        // RVA: 0x0A8FBAFC  token: 0x6000309
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200006E  // size: 0x10
    public static class P3dCommon
    {
        // Fields
        public static System.String HelpUrlPrefix;  // const
        public static System.String ComponentMenuPrefix;  // const
        public static System.String ComponentHitMenuPrefix;  // const
        public static System.Action<UnityEngine.Camera> OnCameraDraw;  // static @ 0x0
        private static System.Int32 _Coord;  // static @ 0x8
        private static UnityEngine.Mesh sphereMesh;  // static @ 0x10
        private static System.Boolean sphereMeshSet;  // static @ 0x18
        private static UnityEngine.Mesh quadMesh;  // static @ 0x20
        private static System.Boolean quadMeshSet;  // static @ 0x28
        private static UnityEngine.Texture2D tempReadTexture;  // static @ 0x30

        // Methods
        // RVA: 0x0A8FE3B8  token: 0x600030A
        private static System.Void .cctor() { }
        // RVA: 0x0A8FDDE8  token: 0x600030B
        public static System.Single RatioToPercentage(System.Single ratio01, System.Int32 decimalPlaces) { }
        // RVA: 0x0A8FD7D8  token: 0x600030C
        public static UnityEngine.RenderTexture GetRenderTexture(UnityEngine.RenderTexture other) { }
        // RVA: 0x0A8FD678  token: 0x600030D
        public static UnityEngine.RenderTexture GetRenderTexture(UnityEngine.RenderTextureDescriptor desc, UnityEngine.RenderTexture other) { }
        // RVA: 0x0A8FD890  token: 0x600030E
        public static UnityEngine.RenderTexture GetRenderTexture(UnityEngine.RenderTextureDescriptor desc) { }
        // RVA: 0x0A8FD75C  token: 0x600030F
        public static UnityEngine.RenderTexture GetRenderTexture(UnityEngine.RenderTextureDescriptor desc, System.Boolean sRGB) { }
        // RVA: 0x0A8FE2F8  token: 0x6000310
        public static UnityEngine.RenderTexture ReleaseRenderTexture(UnityEngine.RenderTexture renderTexture) { }
        // RVA: 0x0A8FDCA4  token: 0x6000311
        public static UnityEngine.Quaternion NormalToCameraRotation(UnityEngine.Vector3 normal, UnityEngine.Camera optionalCamera) { }
        // RVA: 0x0A8FC684  token: 0x6000312
        public static UnityEngine.Vector3 GetCameraUp(UnityEngine.Camera camera) { }
        // RVA: 0x0A8FBEDC  token: 0x6000313
        public static System.Boolean CanReadPixels(UnityEngine.TextureFormat format) { }
        // RVA: 0x0A8FDE94  token: 0x6000314
        public static System.Void ReadPixelsLinearGamma(UnityEngine.Texture2D texture2D, UnityEngine.RenderTexture renderTexture) { }
        // RVA: 0x0A8FE08C  token: 0x6000315
        public static System.Void ReadPixels(UnityEngine.Texture2D texture2D, UnityEngine.RenderTexture renderTexture) { }
        // RVA: 0x0A8FC110  token: 0x6000316
        public static System.Boolean Downsample(UnityEngine.RenderTexture renderTexture, System.Int32 steps, UnityEngine.RenderTexture& temporary) { }
        // RVA: 0x0A8FDA24  token: 0x6000317
        public static System.Boolean HasMipMaps(UnityEngine.Texture texture) { }
        // RVA: 0x0A8FD90C  token: 0x6000318
        public static UnityEngine.Mesh GetSphereMesh() { }
        // RVA: 0x0A8FC7D0  token: 0x6000319
        public static UnityEngine.Mesh GetMesh(UnityEngine.GameObject root, UnityEngine.Mesh mesh) { }
        // RVA: 0x0A8FCDC0  token: 0x600031A
        public static UnityEngine.Mesh GetQuadMesh() { }
        // RVA: 0x0A8FC8DC  token: 0x600031B
        public static UnityEngine.Color GetPixel(UnityEngine.RenderTexture renderTexture, UnityEngine.Vector2 uv, System.Boolean mipMaps) { }
        // RVA: 0x0A8FD428  token: 0x600031C
        public static UnityEngine.Texture2D GetReadableCopy(UnityEngine.Texture texture, UnityEngine.TextureFormat format, System.Boolean mipMaps, System.Int32 width, System.Int32 height) { }
        // RVA: 0x0A8FE300  token: 0x600031D
        public static System.Void SaveBytes(System.String saveName, System.Byte[] data, System.Boolean save) { }
        // RVA: 0x0A8FDB7C  token: 0x600031E
        public static System.Byte[] LoadBytes(System.String saveName) { }
        // RVA: 0x0A8FE37C  token: 0x600031F
        public static System.Boolean SaveExists(System.String saveName) { }
        // RVA: 0x0A8FBEF4  token: 0x6000320
        public static System.Void ClearSave(System.String saveName, System.Boolean save) { }
        // RVA: 0x0A8FCBA4  token: 0x6000321
        public static UnityEngine.Vector3 GetPosition(UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition) { }
        // RVA: 0x0A8FCC28  token: 0x6000322
        public static UnityEngine.Vector3 GetPosition(UnityEngine.Vector3 positionA, UnityEngine.Vector3 positionB, UnityEngine.Vector3 positionC) { }
        // RVA: 0x0A8FCCDC  token: 0x6000323
        public static UnityEngine.Vector3 GetPosition(UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Vector3 position2, UnityEngine.Vector3 endPosition2) { }
        // RVA: 0x0A8FD194  token: 0x6000324
        public static System.Single GetRadius(UnityEngine.Vector3 size) { }
        // RVA: 0x0A8FCEDC  token: 0x6000325
        public static System.Single GetRadius(UnityEngine.Vector3 size, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition) { }
        // RVA: 0x0A8FCFD0  token: 0x6000326
        public static System.Single GetRadius(UnityEngine.Vector3 size, UnityEngine.Vector3 positionA, UnityEngine.Vector3 positionB, UnityEngine.Vector3 positionC) { }
        // RVA: 0x0A8FD1CC  token: 0x6000327
        public static System.Single GetRadius(UnityEngine.Vector3 size, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Vector3 position2, UnityEngine.Vector3 endPosition2) { }
        // RVA: 0x0A8FE384  token: 0x6000328
        public static UnityEngine.Vector3 ScaleAspect(UnityEngine.Vector3 size, System.Single aspect) { }
        // RVA: 0x0A8FC5AC  token: 0x6000329
        public static System.Single GetAspect(UnityEngine.Texture textureA, UnityEngine.Texture textureB) { }
        // RVA: 0x0A8FBCDC  token: 0x600032A
        public static System.Void Blit(UnityEngine.RenderTexture renderTexture, UnityEngine.Texture other) { }
        // RVA: 0x0A8FBD4C  token: 0x600032B
        public static System.Void Blit(UnityEngine.RenderTexture renderTexture, UnityEngine.Material material, System.Int32 pass) { }
        // RVA: 0x0A8FDB24  token: 0x600032C
        public static UnityEngine.Vector4 IndexToVector(System.Int32 index) { }
        // RVA: 0x0A8FC494  token: 0x600032D
        public static System.Void Draw(UnityEngine.Material material, System.Int32 pass, UnityEngine.Mesh mesh, UnityEngine.Matrix4x4 matrix, System.Int32 subMesh, PaintIn3D.P3dCoord coord) { }
        // RVA: 0x0A8FC394  token: 0x600032E
        public static System.Void Draw(UnityEngine.Material material, System.Int32 pass) { }
        // RVA: 0x0A8FC070  token: 0x600032F
        public static UnityEngine.Texture2D CreateTexture(System.Int32 width, System.Int32 height, UnityEngine.TextureFormat format, System.Boolean mipMaps) { }
        // RVA: 0x0A8FC748  token: 0x6000330
        public static UnityEngine.Material GetMaterial(UnityEngine.Renderer renderer, System.Int32 materialIndex) { }
        // RVA: 0x0A8FBF30  token: 0x6000331
        public static UnityEngine.Material CloneMaterial(UnityEngine.GameObject gameObject, System.Int32 materialIndex) { }
        // RVA: 0x0A8FBB78  token: 0x6000332
        public static UnityEngine.Material AddMaterial(UnityEngine.Renderer renderer, UnityEngine.Shader shader, System.Int32 materialIndex) { }
        // RVA: 0x0A8FDBDC  token: 0x6000333
        public static UnityEngine.Shader LoadShader(System.String shaderName) { }
        // RVA: 0x0A8FBDD4  token: 0x6000334
        public static UnityEngine.Material BuildMaterial(UnityEngine.Shader shader) { }
        // RVA: 0x0A8FBE34  token: 0x6000335
        public static UnityEngine.Material BuildMaterial(System.String shaderName, System.String keyword) { }

    }

    // TypeToken: 0x2000070  // size: 0x14
    public sealed struct P3dCoord
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static PaintIn3D.P3dCoord First;  // const
        public static PaintIn3D.P3dCoord Second;  // const
        public static PaintIn3D.P3dCoord Third;  // const
        public static PaintIn3D.P3dCoord Fourth;  // const

    }

    // TypeToken: 0x2000071  // size: 0x38
    public class P3dCoordCopier : UnityEngine.ScriptableObject
    {
        // Fields
        private UnityEngine.Mesh source;  // 0x18
        private PaintIn3D.P3dCoordCopier.Coord first;  // 0x20
        private PaintIn3D.P3dCoordCopier.Coord second;  // 0x24
        private PaintIn3D.P3dCoordCopier.Coord third;  // 0x28
        private PaintIn3D.P3dCoordCopier.Coord fourth;  // 0x2c
        private UnityEngine.Mesh mesh;  // 0x30
        private static System.Collections.Generic.List<UnityEngine.BoneWeight> boneWeights;  // static @ 0x0
        private static System.Collections.Generic.List<UnityEngine.Color32> colors;  // static @ 0x8
        private static System.Collections.Generic.List<UnityEngine.Vector3> positions;  // static @ 0x10
        private static System.Collections.Generic.List<UnityEngine.Vector3> normals;  // static @ 0x18
        private static System.Collections.Generic.List<UnityEngine.Vector4> tangents;  // static @ 0x20
        private static System.Collections.Generic.List<UnityEngine.Vector4> coords0;  // static @ 0x28
        private static System.Collections.Generic.List<UnityEngine.Vector4> coords1;  // static @ 0x30
        private static System.Collections.Generic.List<UnityEngine.Vector4> coords2;  // static @ 0x38
        private static System.Collections.Generic.List<UnityEngine.Vector4> coords3;  // static @ 0x40
        private static System.Collections.Generic.List<UnityEngine.Vector4> coordsNone;  // static @ 0x48
        private static System.Collections.Generic.List<System.Int32> indices;  // static @ 0x50

        // Properties
        UnityEngine.Mesh Source { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        PaintIn3D.P3dCoordCopier.Coord First { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }
        PaintIn3D.P3dCoordCopier.Coord Second { get; /* RVA: 0x011F0020 */ set; /* RVA: 0x011F0030 */ }
        PaintIn3D.P3dCoordCopier.Coord Third { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }
        PaintIn3D.P3dCoordCopier.Coord Fourth { get; /* RVA: 0x04D86710 */ set; /* RVA: 0x04D86720 */ }

        // Methods
        // RVA: 0x0A8FECB8  token: 0x6000344
        public System.Collections.Generic.List<UnityEngine.Vector4> GetCoords(PaintIn3D.P3dCoordCopier.Coord coord) { }
        // RVA: 0x0A8FE598  token: 0x6000345
        public System.Void Generate() { }
        // RVA: 0x0A8FED64  token: 0x6000346
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x0A8FF1CC  token: 0x6000347
        public System.Void .ctor() { }
        // RVA: 0x0A8FEDBC  token: 0x6000348
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000073  // size: 0x98
    public class P3dGraduallyFade : UnityEngine.MonoBehaviour
    {
        // Fields
        private PaintIn3D.P3dPaintableTexture paintableTexture;  // 0x18
        private System.Single threshold;  // 0x20
        private System.Single speed;  // 0x24
        private PaintIn3D.P3dBlendMode blendMode;  // 0x28
        private UnityEngine.Texture blendTexture;  // 0x60
        private PaintIn3D.P3dPaintableTexture blendPaintableTexture;  // 0x68
        private UnityEngine.Color blendColor;  // 0x70
        private UnityEngine.Texture maskTexture;  // 0x80
        private PaintIn3D.P3dPaintableTexture maskPaintableTexture;  // 0x88
        private PaintIn3D.P3dChannel maskChannel;  // 0x90
        private System.Single counter;  // 0x94

        // Properties
        PaintIn3D.P3dPaintableTexture PaintableTexture { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Single Threshold { get; /* RVA: 0x011EC8A0 */ set; /* RVA: 0x011EC9E0 */ }
        System.Single Speed { get; /* RVA: 0x04D89850 */ set; /* RVA: 0x04D89860 */ }
        PaintIn3D.P3dBlendMode BlendMode { get; /* RVA: 0x04DA61A0 */ set; /* RVA: 0x0A8FF640 */ }
        UnityEngine.Texture BlendTexture { get; /* RVA: 0x04D86280 */ set; /* RVA: 0x063CC164 */ }
        PaintIn3D.P3dPaintableTexture BlendPaintableTexture { get; /* RVA: 0x04D86230 */ set; /* RVA: 0x0485F170 */ }
        UnityEngine.Color BlendColor { get; /* RVA: 0x04D8FD40 */ set; /* RVA: 0x04DA1310 */ }
        UnityEngine.Texture MaskTexture { get; /* RVA: 0x04D86260 */ set; /* RVA: 0x04CD9970 */ }
        PaintIn3D.P3dPaintableTexture MaskPaintableTexture { get; /* RVA: 0x04D861F0 */ set; /* RVA: 0x05395D30 */ }
        PaintIn3D.P3dChannel MaskChannel { get; /* RVA: 0x04D86B60 */ set; /* RVA: 0x04D86BC0 */ }

        // Methods
        // RVA: 0x0A8FF1F0  token: 0x600035D
        protected virtual System.Void Update() { }
        // RVA: 0x0A8FF57C  token: 0x600035E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000074  // size: 0x14
    public sealed struct P3dGroup
    {
        // Fields
        private System.Int32 index;  // 0x10

        // Methods
        // RVA: 0x04D86140  token: 0x600035F
        public System.Void .ctor(System.Int32 newIndex) { }
        // RVA: 0x04D88BE0  token: 0x6000360
        public static System.Int32 op_Implicit(PaintIn3D.P3dGroup group) { }
        // RVA: 0x04D88BE0  token: 0x6000361
        public static PaintIn3D.P3dGroup op_Implicit(System.Int32 index) { }
        // RVA: 0x0A8FFBF8  token: 0x6000362
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000075  // size: 0x38
    public class P3dGroupData : UnityEngine.ScriptableObject
    {
        // Fields
        private System.Int32 index;  // 0x18
        private System.Collections.Generic.List<PaintIn3D.P3dGroupData.TextureData> textureDatas;  // 0x20
        private System.String shaderData;  // 0x28
        private System.Collections.Generic.List<PaintIn3D.P3dGroupData.Entry> entries;  // 0x30

        // Properties
        System.Int32 Index { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        System.Collections.Generic.List<PaintIn3D.P3dGroupData.TextureData> TextureDatas { get; /* RVA: 0x0A8FFB74 */ }
        System.String ShaderData { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }

        // Methods
        // RVA: 0x0A8FF75C  token: 0x6000368
        public System.Void TryGetShaderSlotName(System.String shaderPath, System.String& propertyName) { }
        // RVA: 0x0A8FF6E0  token: 0x6000369
        public System.Boolean Supports(UnityEngine.Shader shader) { }
        // RVA: 0x0A8FF66C  token: 0x600036A
        public System.String GetName(System.Boolean prefixNumber) { }
        // RVA: 0x0A8FFAF4  token: 0x600036B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000078  // size: 0x14
    public sealed struct P3dHash
    {
        // Fields
        private System.Int32 v;  // 0x10

        // Methods
        // RVA: 0x0A8FFC00  token: 0x600036E
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x02B77620  token: 0x600036F
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x04D86140  token: 0x6000370
        public System.Void .ctor(System.Int32 newValue) { }
        // RVA: 0x04D88BE0  token: 0x6000371
        public static System.Int32 op_Implicit(PaintIn3D.P3dHash hash) { }
        // RVA: 0x04D88BE0  token: 0x6000372
        public static PaintIn3D.P3dHash op_Implicit(System.Int32 index) { }
        // RVA: 0x0A8FFBF8  token: 0x6000373
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000079  // size: 0x20
    public sealed struct P3dHashedMaterial
    {
        // Fields
        private UnityEngine.Material instance;  // 0x10
        private System.Int32 hash;  // 0x18

        // Methods
        // RVA: 0x053955EC  token: 0x6000374
        public System.Void .ctor(UnityEngine.Material newInstance, System.Int32 newHash) { }
        // RVA: 0x0A8FFC5C  token: 0x6000375
        public System.Boolean TryGetInstance(UnityEngine.Material& model) { }

    }

    // TypeToken: 0x200007A  // size: 0x20
    public sealed struct P3dHashedModel
    {
        // Fields
        private PaintIn3D.P3dModel instance;  // 0x10
        private PaintIn3D.P3dHash hash;  // 0x18

        // Methods
        // RVA: 0x0A8FFE1C  token: 0x6000376
        public static PaintIn3D.P3dHashedModel op_Implicit(PaintIn3D.P3dModel newInstance) { }
        // RVA: 0x0A8FFD24  token: 0x6000377
        public System.Boolean TryGetInstance(PaintIn3D.P3dModel& model) { }

    }

    // TypeToken: 0x200007B  // size: 0x20
    public sealed struct P3dHashedTexture
    {
        // Fields
        private UnityEngine.Texture instance;  // 0x10
        private PaintIn3D.P3dHash hash;  // 0x18

        // Methods
        // RVA: 0x0A8FFF9C  token: 0x6000378
        public static PaintIn3D.P3dHashedTexture op_Implicit(UnityEngine.Texture newInstance) { }
        // RVA: 0x0A8FFF78  token: 0x6000379
        public static UnityEngine.Texture op_Implicit(PaintIn3D.P3dHashedTexture hashed) { }
        // RVA: 0x0A8FFEB0  token: 0x600037A
        public System.Boolean TryGetInstance(UnityEngine.Texture& texture) { }

    }

    // TypeToken: 0x200007C  // size: 0x28
    public sealed struct P3dHit
    {
        // Fields
        public UnityEngine.Transform Root;  // 0x10
        public UnityEngine.Vector2 First;  // 0x18
        public UnityEngine.Vector2 Second;  // 0x20

        // Methods
        // RVA: 0x0A900060  token: 0x600037B
        public System.Void .ctor(UnityEngine.RaycastHit hit) { }

    }

    // TypeToken: 0x200007D  // size: 0x30
    public class P3dLineConnector
    {
        // Fields
        private System.Single hitSpacing;  // 0x10
        private System.Int32 hitLimit;  // 0x14
        protected System.Boolean connectHits;  // 0x18
        protected System.Boolean clipConnected;  // 0x19
        private System.Collections.Generic.List<PaintIn3D.P3dLineConnector.Link> links;  // 0x20
        private static System.Collections.Generic.Stack<PaintIn3D.P3dLineConnector.Link> linkPool;  // static @ 0x0
        protected PaintIn3D.P3dHitCache hitCache;  // 0x28

        // Properties
        System.Single HitSpacing { get; /* RVA: 0x04D85F70 */ set; /* RVA: 0x04D88280 */ }
        System.Int32 HitLimit { get; /* RVA: 0x04D882A0 */ set; /* RVA: 0x04D8F540 */ }
        System.Boolean ConnectHits { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x04D86350 */ }
        System.Boolean ClipConnected { get; /* RVA: 0x04D866A0 */ set; /* RVA: 0x04D866C0 */ }
        PaintIn3D.P3dHitCache HitCache { get; /* RVA: 0x04D86240 */ }

        // Methods
        // RVA: 0x0A9001FC  token: 0x6000385
        public System.Void ClearHitCache() { }
        // RVA: 0x0A90021C  token: 0x6000386
        public System.Void ResetConnections() { }
        // RVA: 0x0A9000F0  token: 0x6000387
        public System.Void BreakHits(System.Object owner) { }
        // RVA: 0x0A900304  token: 0x6000388
        public System.Void SubmitLine(UnityEngine.GameObject gameObject, System.Boolean preview, System.Int32 priority, System.Single pressure, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Quaternion rotation, System.Object owner) { }
        // RVA: 0x0A900B20  token: 0x6000389
        public System.Void Update() { }
        // RVA: 0x0A900A6C  token: 0x600038A
        private System.Boolean TryGetLink(System.Object owner, PaintIn3D.P3dLineConnector.Link& link) { }
        // RVA: 0x0A900CDC  token: 0x600038B
        public System.Void .ctor() { }
        // RVA: 0x0A900C50  token: 0x600038C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200007F  // size: 0x38
    public class P3dMask : UnityEngine.MonoBehaviour
    {
        // Fields
        private UnityEngine.Texture texture;  // 0x18
        private PaintIn3D.P3dChannel channel;  // 0x20
        private UnityEngine.Vector2 stretch;  // 0x24
        private static System.Collections.Generic.LinkedList<PaintIn3D.P3dMask> instances;  // static @ 0x0
        private System.Collections.Generic.LinkedListNode<PaintIn3D.P3dMask> instancesNode;  // 0x30

        // Properties
        UnityEngine.Texture Texture { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        PaintIn3D.P3dChannel Channel { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }
        UnityEngine.Vector2 Stretch { get; /* RVA: 0x04D88060 */ set; /* RVA: 0x04DA5EC0 */ }
        System.Collections.Generic.LinkedList<PaintIn3D.P3dMask> Instances { get; /* RVA: 0x0A9011B8 */ }
        UnityEngine.Matrix4x4 Matrix { get; /* RVA: 0x0A901208 */ }

        // Methods
        // RVA: 0x0A900D90  token: 0x6000396
        public static PaintIn3D.P3dMask Find(UnityEngine.Vector3 position, UnityEngine.LayerMask layers) { }
        // RVA: 0x0A901094  token: 0x6000397
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A901018  token: 0x6000398
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0A90118C  token: 0x6000399
        public System.Void .ctor() { }
        // RVA: 0x0A901110  token: 0x600039A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000080  // size: 0x50
    public class P3dMaterialCloner : UnityEngine.MonoBehaviour
    {
        // Fields
        private System.Int32 index;  // 0x18
        private UnityEngine.Renderer cachedRenderer;  // 0x20
        private System.Boolean cachedRendererSet;  // 0x28
        private PaintIn3D.P3dPaintable cachedPaintable;  // 0x30
        private System.Boolean cachedPaintableSet;  // 0x38
        private System.Boolean activated;  // 0x39
        private UnityEngine.Material current;  // 0x40
        private UnityEngine.Material original;  // 0x48

        // Properties
        System.Int32 Index { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        UnityEngine.Renderer CachedRenderer { get; /* RVA: 0x0A901684 */ }
        PaintIn3D.P3dPaintable CachedPaintable { get; /* RVA: 0x0A90162C */ }
        UnityEngine.Material Original { get; /* RVA: 0x04D86200 */ }
        UnityEngine.Material Current { get; /* RVA: 0x04D85A60 */ }
        System.Boolean Activated { get; /* RVA: 0x04D86650 */ }

        // Methods
        // RVA: 0x0A901260  token: 0x60003A2
        public System.Void Activate() { }
        // RVA: 0x0A901374  token: 0x60003A3
        public System.Void Deactivate() { }
        // RVA: 0x0A9013FC  token: 0x60003A4
        public System.Void ReplaceAll(UnityEngine.Material from, UnityEngine.Material to) { }
        // RVA: 0x0A90154C  token: 0x60003A5
        private System.Void Replace(UnityEngine.Renderer renderer, UnityEngine.Material[] materials, UnityEngine.Material oldMaterial, UnityEngine.Material newMaterial) { }
        // RVA: 0x05393520  token: 0x60003A6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000081  // size: 0x10
    public static class P3dMeshCache
    {
        // Fields
        private static System.Collections.Generic.Dictionary<UnityEngine.Mesh,PaintIn3D.P3dMeshCache.MeshData> cachedData;  // static @ 0x0

        // Methods
        // RVA: 0x0A9016DC  token: 0x60003A7
        public static System.Boolean GetTrianglePositions(UnityEngine.RaycastHit hit, UnityEngine.Vector3& positionA, UnityEngine.Vector3& positionB, UnityEngine.Vector3& positionC) { }
        // RVA: 0x0A9018F4  token: 0x60003A8
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000083  // size: 0xF8
    public class P3dModel : UnityEngine.MonoBehaviour
    {
        // Fields
        protected PaintIn3D.P3dPaintable paintable;  // 0x18
        protected System.Boolean includeScale;  // 0x20
        protected PaintIn3D.P3dModel.UseMeshType useMesh;  // 0x24
        protected PaintIn3D.P3dHash hash;  // 0x28
        private UnityEngine.Renderer cachedRenderer;  // 0x30
        private System.Boolean cachedRendererSet;  // 0x38
        private UnityEngine.SkinnedMeshRenderer cachedSkinned;  // 0x40
        private UnityEngine.MeshFilter cachedFilter;  // 0x48
        private System.Boolean cachedSkinnedSet;  // 0x50
        private UnityEngine.Transform cachedTransform;  // 0x58
        private UnityEngine.GameObject cachedGameObject;  // 0x60
        private UnityEngine.Material[] materials;  // 0x68
        private System.Boolean materialsSet;  // 0x70
        private UnityEngine.Mesh bakedMesh;  // 0x78
        private System.Boolean bakedMeshSet;  // 0x80
        protected System.Boolean prepared;  // 0x81
        private UnityEngine.Mesh preparedMesh;  // 0x88
        private UnityEngine.Matrix4x4 preparedMatrix;  // 0x90
        private System.Int32[] preparedTriangles;  // 0xd0
        private UnityEngine.Vector3[] preparedPositions;  // 0xd8
        private UnityEngine.Vector2[] preparedCoord0;  // 0xe0
        private UnityEngine.Vector2[] preparedCoord1;  // 0xe8
        private static System.Collections.Generic.List<PaintIn3D.P3dModel> tempModels;  // static @ 0x0
        private static System.Collections.Generic.LinkedList<PaintIn3D.P3dModel> instances;  // static @ 0x8
        private System.Collections.Generic.LinkedListNode<PaintIn3D.P3dModel> instancesNode;  // 0xf0

        // Properties
        PaintIn3D.P3dPaintable Paintable { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Boolean IncludeScale { get; /* RVA: 0x04D866B0 */ set; /* RVA: 0x04D866D0 */ }
        PaintIn3D.P3dModel.UseMeshType UseMesh { get; /* RVA: 0x011F0020 */ set; /* RVA: 0x011F0030 */ }
        PaintIn3D.P3dHash Hash { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x0A902D2C */ }
        System.Collections.Generic.LinkedList<PaintIn3D.P3dModel> Instances { get; /* RVA: 0x0A902C94 */ }
        System.Boolean Prepared { get; /* RVA: 0x04D8FEE0 */ set; /* RVA: 0x04D8FEF0 */ }
        UnityEngine.Mesh PreparedMesh { get; /* RVA: 0x04D861F0 */ }
        UnityEngine.GameObject CachedGameObject { get; /* RVA: 0x04D86280 */ }
        UnityEngine.Renderer CachedRenderer { get; /* RVA: 0x0A902C6C */ }
        UnityEngine.Material[] Materials { get; /* RVA: 0x0A902CE4 */ }

        // Methods
        // RVA: 0x0A901DF8  token: 0x60003BB
        public System.Int32 GetMaterialIndex(UnityEngine.Material material) { }
        // RVA: 0x0A901980  token: 0x60003BC
        private System.Void CacheRenderer() { }
        // RVA: 0x04DA61D0  token: 0x60003BD
        public System.Void DirtyMaterials() { }
        // RVA: 0x0A901A54  token: 0x60003BE
        public static System.Collections.Generic.List<PaintIn3D.P3dModel> FindOverlap(UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask) { }
        // RVA: 0x0A9022D8  token: 0x60003BF
        public System.Void GetPreparedPoints(System.Int32 triangleIndex, UnityEngine.Vector3& pointA, UnityEngine.Vector3& pointB, UnityEngine.Vector3& pointC) { }
        // RVA: 0x0A901F48  token: 0x60003C0
        public System.Void GetPreparedCoords0(System.Int32 triangleIndex, UnityEngine.Vector2& coordA, UnityEngine.Vector2& coordB, UnityEngine.Vector2& coordC) { }
        // RVA: 0x0A902110  token: 0x60003C1
        public System.Void GetPreparedCoords1(System.Int32 triangleIndex, UnityEngine.Vector2& coordA, UnityEngine.Vector2& coordB, UnityEngine.Vector2& coordC) { }
        // RVA: 0x0A9028F8  token: 0x60003C2
        public System.Int32 GetSubmesh(PaintIn3D.P3dPaintableTexture paintableTexture) { }
        // RVA: 0x0A9024D0  token: 0x60003C3
        public System.Void GetPrepared(UnityEngine.Mesh& mesh, UnityEngine.Matrix4x4& matrix, PaintIn3D.P3dCoord coord) { }
        // RVA: 0x0A902AAC  token: 0x60003C4
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A902A24  token: 0x60003C5
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0A9029A0  token: 0x60003C6
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x04D84C40  token: 0x60003C7
        public System.Void .ctor() { }
        // RVA: 0x0A902B8C  token: 0x60003C8
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000085  // size: 0x18
    public abstract class P3dModifier
    {
        // Fields
        private System.Boolean preview;  // 0x10
        private System.Boolean unique;  // 0x11

        // Properties
        System.Boolean Preview { get; /* RVA: 0x015EFCE0 */ set; /* RVA: 0x04D86130 */ }
        System.Boolean Unique { get; /* RVA: 0x04D88380 */ set; /* RVA: 0x04D88460 */ }

        // Methods
        // RVA: 0x0A903844  token: 0x60003CD
        public System.Void ModifyAngle(System.Single& angle, System.Single pressure) { }
        // RVA: 0x041E1670  token: 0x60003CE
        protected virtual System.Void OnModifyAngle(System.Single& angle, System.Single pressure) { }
        // RVA: 0x0A9038D0  token: 0x60003CF
        public System.Void ModifyColor(UnityEngine.Color& color, System.Single pressure) { }
        // RVA: 0x041E1670  token: 0x60003D0
        protected virtual System.Void OnModifyColor(UnityEngine.Color& color, System.Single pressure) { }
        // RVA: 0x0A903954  token: 0x60003D1
        public System.Void ModifyHardness(System.Single& hardness, System.Single pressure) { }
        // RVA: 0x041E1670  token: 0x60003D2
        protected virtual System.Void OnModifyHardness(System.Single& hardness, System.Single pressure) { }
        // RVA: 0x0A9039E0  token: 0x60003D3
        public System.Void ModifyOpacity(System.Single& opacity, System.Single pressure) { }
        // RVA: 0x041E1670  token: 0x60003D4
        protected virtual System.Void OnModifyOpacity(System.Single& opacity, System.Single pressure) { }
        // RVA: 0x0A903AF8  token: 0x60003D5
        public System.Void ModifyRadius(System.Single& radius, System.Single pressure) { }
        // RVA: 0x041E1670  token: 0x60003D6
        protected virtual System.Void OnModifyRadius(System.Single& radius, System.Single pressure) { }
        // RVA: 0x0A903B84  token: 0x60003D7
        public System.Void ModifyScale(UnityEngine.Vector3& scale, System.Single pressure) { }
        // RVA: 0x041E1670  token: 0x60003D8
        protected virtual System.Void OnModifyScale(UnityEngine.Vector3& scale, System.Single pressure) { }
        // RVA: 0x0A903C10  token: 0x60003D9
        public System.Void ModifyTexture(UnityEngine.Texture& texture, System.Single pressure) { }
        // RVA: 0x041E1670  token: 0x60003DA
        protected virtual System.Void OnModifyTexture(UnityEngine.Texture& texture, System.Single pressure) { }
        // RVA: 0x0A903A6C  token: 0x60003DB
        public System.Void ModifyPosition(UnityEngine.Vector3& position, System.Single pressure) { }
        // RVA: 0x041E1670  token: 0x60003DC
        protected virtual System.Void OnModifyPosition(UnityEngine.Vector3& position, System.Single pressure) { }
        // RVA: 0x04DA60F0  token: 0x60003DD
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000086  // size: 0x18
    public class P3dModifierList
    {
        // Fields
        private System.Collections.Generic.List<PaintIn3D.P3dModifier> modifiers;  // 0x10

        // Properties
        System.Int32 Count { get; /* RVA: 0x0A903780 */ }
        System.Collections.Generic.List<PaintIn3D.P3dModifier> Instances { get; /* RVA: 0x0A9037C0 */ }

        // Methods
        // RVA: 0x0A902D80  token: 0x60003E0
        public System.Void ModifyAngle(System.Single& angle, System.Boolean preview, System.Single pressure) { }
        // RVA: 0x0A902EC0  token: 0x60003E1
        public System.Void ModifyColor(UnityEngine.Color& color, System.Boolean preview, System.Single pressure) { }
        // RVA: 0x0A903000  token: 0x60003E2
        public System.Void ModifyHardness(System.Single& hardness, System.Boolean preview, System.Single pressure) { }
        // RVA: 0x0A903140  token: 0x60003E3
        public System.Void ModifyOpacity(System.Single& opacity, System.Boolean preview, System.Single pressure) { }
        // RVA: 0x0A9033C0  token: 0x60003E4
        public System.Void ModifyRadius(System.Single& radius, System.Boolean preview, System.Single pressure) { }
        // RVA: 0x0A903500  token: 0x60003E5
        public System.Void ModifyScale(UnityEngine.Vector3& scale, System.Boolean preview, System.Single pressure) { }
        // RVA: 0x0A903640  token: 0x60003E6
        public System.Void ModifyTexture(UnityEngine.Texture& texture, System.Boolean preview, System.Single pressure) { }
        // RVA: 0x0A903280  token: 0x60003E7
        public System.Void ModifyPosition(UnityEngine.Vector3& position, System.Boolean preview, System.Single pressure) { }
        // RVA: 0x041E1670  token: 0x60003E8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000087  // size: 0x138
    public class P3dPaintable : PaintIn3D.P3dModel
    {
        // Fields
        private PaintIn3D.P3dPaintable.ActivationType activation;  // 0xf8
        private UnityEngine.Vector3 baseScale;  // 0xfc
        private System.Collections.Generic.List<UnityEngine.Renderer> otherRenderers;  // 0x108
        private UnityEngine.Events.UnityEvent onActivating;  // 0x110
        private UnityEngine.Events.UnityEvent onActivated;  // 0x118
        private System.Boolean activated;  // 0x120
        private System.Collections.Generic.HashSet<PaintIn3D.P3dPaintableTexture> paintableTextures;  // 0x128
        private static System.Collections.Generic.List<PaintIn3D.P3dMaterialCloner> materialCloners;  // static @ 0x0
        private System.Collections.Generic.List<PaintIn3D.P3dPaintableTexture> tempPaintableTextures;  // 0x130

        // Properties
        PaintIn3D.P3dPaintable Paintable { get; /* RVA: 0x0A90A960 */ set; /* RVA: 0x041E1670 */ }
        PaintIn3D.P3dPaintable.ActivationType Activation { get; /* RVA: 0x04DA61F0 */ set; /* RVA: 0x04DA6220 */ }
        UnityEngine.Vector3 BaseScale { get; /* RVA: 0x04DA6200 */ set; /* RVA: 0x04DA6230 */ }
        System.Collections.Generic.List<UnityEngine.Renderer> OtherRenderers { get; /* RVA: 0x04D85F90 */ set; /* RVA: 0x068C2E50 */ }
        UnityEngine.Events.UnityEvent OnActivating { get; /* RVA: 0x0A90A8E4 */ }
        UnityEngine.Events.UnityEvent OnActivated { get; /* RVA: 0x0A90A868 */ }
        System.Boolean Activated { get; /* RVA: 0x04D8FE70 */ }
        System.Collections.Generic.HashSet<PaintIn3D.P3dPaintableTexture> PaintableTextures { get; /* RVA: 0x04D8D910 */ }

        // Methods
        // RVA: 0x0A90A3F8  token: 0x60003F5
        public System.Void RemoveComponents() { }
        // RVA: 0x0A90A560  token: 0x60003F6
        public System.Void ScaleSize(System.Int32& width, System.Int32& height) { }
        // RVA: 0x0A9099C4  token: 0x60003F7
        public System.Void Activate() { }
        // RVA: 0x0A909C28  token: 0x60003F8
        private System.Void AddPaintableTextures(UnityEngine.Transform root) { }
        // RVA: 0x0A90A014  token: 0x60003F9
        public System.Void Deactivate() { }
        // RVA: 0x0A909E8C  token: 0x60003FA
        public System.Void ClearAll(UnityEngine.Color color) { }
        // RVA: 0x0A909EAC  token: 0x60003FB
        public System.Void ClearAll(UnityEngine.Texture texture, UnityEngine.Color color) { }
        // RVA: 0x0A90A3A0  token: 0x60003FC
        public System.Void Register(PaintIn3D.P3dPaintableTexture paintableTexture) { }
        // RVA: 0x0A90A68C  token: 0x60003FD
        public System.Void Unregister(PaintIn3D.P3dPaintableTexture paintableTexture) { }
        // RVA: 0x0A909E70  token: 0x60003FE
        protected virtual System.Void Awake() { }
        // RVA: 0x0A90A33C  token: 0x60003FF
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A90A670  token: 0x6000400
        protected virtual System.Void Start() { }
        // RVA: 0x0A90A770  token: 0x6000401
        public System.Void .ctor() { }
        // RVA: 0x0A90A6E4  token: 0x6000402
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000089  // size: 0x20
    public class P3dPaintableManager : UnityEngine.MonoBehaviour
    {
        // Fields
        private static System.Collections.Generic.LinkedList<PaintIn3D.P3dPaintableManager> instances;  // static @ 0x0
        private System.Collections.Generic.LinkedListNode<PaintIn3D.P3dPaintableManager> instancesNode;  // 0x18

        // Properties
        System.Collections.Generic.LinkedList<PaintIn3D.P3dPaintableManager> Instances { get; /* RVA: 0x0A904920 */ }

        // Methods
        // RVA: 0x0A903F4C  token: 0x6000404
        public static PaintIn3D.P3dPaintableManager GetOrCreateInstance() { }
        // RVA: 0x0A9043CC  token: 0x6000405
        public static System.Void SubmitAll(PaintIn3D.P3dCommand command, UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask, PaintIn3D.P3dGroup group, PaintIn3D.P3dModel targetModel, PaintIn3D.P3dPaintableTexture targetTexture) { }
        // RVA: 0x0A903DD0  token: 0x6000406
        private static System.Void DoSubmitAll(PaintIn3D.P3dCommand command, UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask, PaintIn3D.P3dGroup group, PaintIn3D.P3dModel targetModel, PaintIn3D.P3dPaintableTexture targetTexture) { }
        // RVA: 0x0A9042D0  token: 0x6000407
        private static System.Void SubmitAll(PaintIn3D.P3dCommand command, UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask, PaintIn3D.P3dGroup group) { }
        // RVA: 0x0A9045D0  token: 0x6000408
        private static System.Void SubmitAll(PaintIn3D.P3dCommand command, PaintIn3D.P3dModel model, PaintIn3D.P3dGroup group) { }
        // RVA: 0x0A9046B0  token: 0x6000409
        public static PaintIn3D.P3dCommand Submit(PaintIn3D.P3dCommand command, PaintIn3D.P3dModel model, PaintIn3D.P3dPaintableTexture paintableTexture) { }
        // RVA: 0x0A904254  token: 0x600040A
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A9041D8  token: 0x600040B
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0A904098  token: 0x600040C
        protected virtual System.Void LateUpdate() { }
        // RVA: 0x0A903C94  token: 0x600040D
        private System.Void ClearAll() { }
        // RVA: 0x0A904760  token: 0x600040E
        private System.Void UpdateAll() { }
        // RVA: 0x05393520  token: 0x600040F
        public System.Void .ctor() { }
        // RVA: 0x0A9048A4  token: 0x6000410
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200008A  // size: 0x20
    public class P3dPaintableState
    {
        // Fields
        public UnityEngine.RenderTexture Texture;  // 0x10
        public System.Collections.Generic.List<PaintIn3D.P3dCommand> Commands;  // 0x18
        private static System.Collections.Generic.Stack<PaintIn3D.P3dPaintableState> pool;  // static @ 0x0

        // Methods
        // RVA: 0x0A904AE8  token: 0x6000411
        public static PaintIn3D.P3dPaintableState Pop() { }
        // RVA: 0x0A904B9C  token: 0x6000412
        public System.Void Write(UnityEngine.RenderTexture current) { }
        // RVA: 0x0A904C84  token: 0x6000413
        public System.Void Write(System.Collections.Generic.List<PaintIn3D.P3dCommand> commands) { }
        // RVA: 0x0A904CE4  token: 0x6000414
        public System.Void Write(UnityEngine.RenderTexture current, System.Collections.Generic.List<PaintIn3D.P3dCommand> commands) { }
        // RVA: 0x0A904970  token: 0x6000415
        private System.Void Clear() { }
        // RVA: 0x0A904A70  token: 0x6000416
        public System.Void Pool() { }
        // RVA: 0x0A904E8C  token: 0x6000417
        public System.Void .ctor() { }
        // RVA: 0x0A904E00  token: 0x6000418
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200008B  // size: 0x130
    public class P3dPaintableTexture : UnityEngine.MonoBehaviour
    {
        // Fields
        private PaintIn3D.P3dSlot slot;  // 0x18
        private PaintIn3D.P3dCoord coord;  // 0x28
        private PaintIn3D.P3dGroup group;  // 0x2c
        private PaintIn3D.P3dPaintableTexture.UndoRedoType undoRedo;  // 0x30
        private System.Int32 stateLimit;  // 0x34
        private PaintIn3D.P3dPaintableTexture.SaveLoadType saveLoad;  // 0x38
        private System.String saveName;  // 0x40
        private System.Int32 width;  // 0x48
        private System.Int32 height;  // 0x4c
        private UnityEngine.Texture texture;  // 0x50
        private UnityEngine.Color color;  // 0x58
        private UnityEngine.RenderTextureFormat format;  // 0x68
        private PaintIn3D.P3dPaintableTexture.MipType mipMaps;  // 0x6c
        private PaintIn3D.P3dPaintableTexture.FilterType filter;  // 0x70
        private PaintIn3D.P3dPaintableTexture.AnisoType aniso;  // 0x74
        private PaintIn3D.P3dPaintableTexture.WrapType wrapU;  // 0x78
        private PaintIn3D.P3dPaintableTexture.WrapType wrapV;  // 0x7c
        private PaintIn3D.P3dPaintableTexture.ExistingType existing;  // 0x80
        private PaintIn3D.P3dPaintableTexture.ConversionType conversion;  // 0x84
        private UnityEngine.Texture localMaskTexture;  // 0x88
        private PaintIn3D.P3dChannel localMaskChannel;  // 0x90
        private System.String shaderKeyword;  // 0x98
        protected PaintIn3D.P3dHash hash;  // 0xa0
        private System.Boolean isDummy;  // 0xa4
        private System.String output;  // 0xa8
        private System.Action<PaintIn3D.P3dCommand> OnAddCommand;  // 0xb0
        private static System.Action<PaintIn3D.P3dPaintableTexture,PaintIn3D.P3dCommand> OnAddCommandGlobal;  // static @ 0x0
        public static System.Action<PaintIn3D.P3dPaintableTexture> OnInstanceAdded;  // static @ 0x8
        public static System.Action<PaintIn3D.P3dPaintableTexture> OnInstanceRemoved;  // static @ 0x10
        private System.Action<System.Boolean> OnModified;  // 0xb8
        private System.Boolean activated;  // 0xc0
        private UnityEngine.RenderTexture current;  // 0xc8
        private UnityEngine.RenderTexture preview;  // 0xd0
        private System.Collections.Generic.List<PaintIn3D.P3dPaintableState> paintableStates;  // 0xd8
        private System.Int32 stateIndex;  // 0xe0
        private PaintIn3D.P3dPaintable paintable;  // 0xe8
        private System.Boolean paintableSet;  // 0xf0
        private UnityEngine.Material material;  // 0xf8
        private System.Boolean materialSet;  // 0x100
        private UnityEngine.Texture oldTexture;  // 0x108
        private System.Collections.Generic.List<PaintIn3D.P3dCommand> paintCommands;  // 0x110
        private System.Collections.Generic.List<PaintIn3D.P3dCommand> previewCommands;  // 0x118
        private System.Collections.Generic.List<PaintIn3D.P3dCommand> localCommands;  // 0x120
        private static System.Collections.Generic.List<PaintIn3D.P3dPaintableTexture> tempPaintableTextures;  // static @ 0x18
        private static System.Collections.Generic.LinkedList<PaintIn3D.P3dPaintableTexture> instances;  // static @ 0x20
        private System.Collections.Generic.LinkedListNode<PaintIn3D.P3dPaintableTexture> instancesNode;  // 0x128
        private static System.Int32 _Buffer;  // static @ 0x28
        private static System.Int32 _BufferSize;  // static @ 0x2c

        // Properties
        PaintIn3D.P3dSlot Slot { get; /* RVA: 0x04D890C0 */ set; /* RVA: 0x0A9099B4 */ }
        PaintIn3D.P3dCoord Coord { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }
        PaintIn3D.P3dGroup Group { get; /* RVA: 0x04D86710 */ set; /* RVA: 0x04D86720 */ }
        PaintIn3D.P3dPaintableTexture.UndoRedoType UndoRedo { get; /* RVA: 0x04D864F0 */ set; /* RVA: 0x04D86740 */ }
        System.Int32 StateLimit { get; /* RVA: 0x04D86730 */ set; /* RVA: 0x04D86760 */ }
        PaintIn3D.P3dPaintableTexture.SaveLoadType SaveLoad { get; /* RVA: 0x04D863B0 */ set; /* RVA: 0x04D86430 */ }
        System.String SaveName { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        System.Int32 Width { get; /* RVA: 0x04D863D0 */ set; /* RVA: 0x04D86450 */ }
        System.Int32 Height { get; /* RVA: 0x04D86600 */ set; /* RVA: 0x04D86630 */ }
        UnityEngine.Texture Texture { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x02FDAEA0 */ }
        UnityEngine.Color Color { get; /* RVA: 0x04D87E00 */ set; /* RVA: 0x04DA61E0 */ }
        UnityEngine.RenderTextureFormat Format { get; /* RVA: 0x04D86CF0 */ set; /* RVA: 0x04D86D00 */ }
        PaintIn3D.P3dPaintableTexture.MipType MipMaps { get; /* RVA: 0x04D86B50 */ set; /* RVA: 0x04D86BB0 */ }
        PaintIn3D.P3dPaintableTexture.FilterType Filter { get; /* RVA: 0x04D868A0 */ set; /* RVA: 0x04D868D0 */ }
        PaintIn3D.P3dPaintableTexture.AnisoType Aniso { get; /* RVA: 0x04D868B0 */ set; /* RVA: 0x04D868E0 */ }
        PaintIn3D.P3dPaintableTexture.WrapType WrapU { get; /* RVA: 0x04D863C0 */ set; /* RVA: 0x04D86440 */ }
        PaintIn3D.P3dPaintableTexture.WrapType WrapV { get; /* RVA: 0x04D86410 */ set; /* RVA: 0x04D86490 */ }
        PaintIn3D.P3dPaintableTexture.ExistingType Existing { get; /* RVA: 0x04D863A0 */ set; /* RVA: 0x04D86420 */ }
        PaintIn3D.P3dPaintableTexture.ConversionType Conversion { get; /* RVA: 0x04D8FCE0 */ set; /* RVA: 0x04D8FCF0 */ }
        UnityEngine.Texture LocalMaskTexture { get; /* RVA: 0x04D861F0 */ set; /* RVA: 0x05395D30 */ }
        PaintIn3D.P3dChannel LocalMaskChannel { get; /* RVA: 0x04D86B60 */ set; /* RVA: 0x04D86BC0 */ }
        System.String ShaderKeyword { get; /* RVA: 0x04D85EE0 */ set; /* RVA: 0x06352D0C */ }
        PaintIn3D.P3dHash Hash { get; /* RVA: 0x04D88CD0 */ set; /* RVA: 0x0A909960 */ }
        System.Boolean IsDummy { get; /* RVA: 0x04D88D60 */ set; /* RVA: 0x04D88D70 */ }
        System.String Output { get; /* RVA: 0x04D86290 */ set; /* RVA: 0x04D84CC0 */ }
        System.Collections.Generic.LinkedList<PaintIn3D.P3dPaintableTexture> Instances { get; /* RVA: 0x0A909614 */ }
        System.Boolean Activated { get; /* RVA: 0x04D91920 */ }
        System.Boolean CanUndo { get; /* RVA: 0x0A9095AC */ }
        System.Boolean CanRedo { get; /* RVA: 0x0A909554 */ }
        System.Collections.Generic.List<PaintIn3D.P3dPaintableState> States { get; /* RVA: 0x04D88EF0 */ }
        System.Int32 StateIndex { get; /* RVA: 0x04D9A880 */ }
        UnityEngine.Material Material { get; /* RVA: 0x0A909664 */ }
        PaintIn3D.P3dPaintable Paintable { get; /* RVA: 0x0A909694 */ }
        UnityEngine.RenderTexture Current { get; /* RVA: 0x04D8D1D0 */ set; /* RVA: 0x0A909908 */ }
        UnityEngine.RenderTexture Preview { get; /* RVA: 0x04D876B0 */ }
        System.Boolean CommandsPending { get; /* RVA: 0x0A9095C0 */ }

        // Events
        event System.Action<PaintIn3D.P3dCommand> OnAddCommand;
        event System.Action<PaintIn3D.P3dPaintableTexture,PaintIn3D.P3dCommand> OnAddCommandGlobal;
        event System.Action<System.Boolean> OnModified;

        // Methods
        // RVA: 0x0A9075B8  token: 0x600045C
        public static System.Collections.Generic.List<PaintIn3D.P3dPaintableTexture> FilterAll(PaintIn3D.P3dModel model, PaintIn3D.P3dGroup group) { }
        // RVA: 0x0A9067CC  token: 0x600045D
        public System.Void ClearStates() { }
        // RVA: 0x0A908C44  token: 0x600045E
        public System.Void StoreState() { }
        // RVA: 0x0A908EA0  token: 0x600045F
        public System.Void Undo() { }
        // RVA: 0x0A908720  token: 0x6000460
        public System.Void Redo() { }
        // RVA: 0x0A908B38  token: 0x6000461
        public System.Void SetColor(System.String html) { }
        // RVA: 0x0A90780C  token: 0x6000462
        public UnityEngine.Vector2 GetCoord(PaintIn3D.P3dHit& hit) { }
        // RVA: 0x0A907840  token: 0x6000463
        public UnityEngine.Vector2 GetCoord(UnityEngine.RaycastHit& hit) { }
        // RVA: 0x0A908B4C  token: 0x6000464
        private System.Boolean StatesContainTextureOrCommands() { }
        // RVA: 0x0A907E60  token: 0x6000465
        private System.Boolean LastStateWithTextureOr0(System.Int32& startIndex) { }
        // RVA: 0x0A908384  token: 0x6000466
        private System.Void RebuildFromCommands() { }
        // RVA: 0x0A90648C  token: 0x6000467
        private System.Void AddState() { }
        // RVA: 0x0A908CD0  token: 0x6000468
        private System.Void TrimFuture() { }
        // RVA: 0x0A908DF0  token: 0x6000469
        private System.Void TrimPast() { }
        // RVA: 0x0A9080C0  token: 0x600046A
        public System.Void NotifyOnModified(System.Boolean preview) { }
        // RVA: 0x0A907918  token: 0x600046B
        public UnityEngine.Texture2D GetReadableCopy(System.Boolean convertBack) { }
        // RVA: 0x0A90786C  token: 0x600046C
        public System.Byte[] GetPngData(System.Boolean convertBack) { }
        // RVA: 0x0A907BB4  token: 0x600046D
        public System.Byte[] GetTgaData(System.Boolean convertBack) { }
        // RVA: 0x0A90687C  token: 0x600046E
        public System.Void Clear() { }
        // RVA: 0x0A9068A8  token: 0x600046F
        public System.Void Clear(UnityEngine.Texture texture, UnityEngine.Color tint, System.Boolean updateMips) { }
        // RVA: 0x0A9088BC  token: 0x6000470
        public System.Void Replace() { }
        // RVA: 0x0A9087E8  token: 0x6000471
        public System.Void Replace(UnityEngine.Texture texture, UnityEngine.Color tint) { }
        // RVA: 0x0A9088E0  token: 0x6000472
        public System.Boolean Resize(System.Int32 width, System.Int32 height, System.Boolean copyContents) { }
        // RVA: 0x0A908B2C  token: 0x6000473
        public System.Void Save() { }
        // RVA: 0x0A908AB4  token: 0x6000474
        public System.Void Save(System.String saveName) { }
        // RVA: 0x0A9080B0  token: 0x6000475
        public System.Void Load() { }
        // RVA: 0x0A908048  token: 0x6000476
        public System.Void Load(System.String saveName, System.Boolean replace) { }
        // RVA: 0x0A907F2C  token: 0x6000477
        public System.Void LoadFromData(System.Byte[] data, System.Boolean allowResize) { }
        // RVA: 0x0A907DAC  token: 0x6000478
        public System.Void HidePreview() { }
        // RVA: 0x0A907C60  token: 0x6000479
        public static System.Void HideAllPreviews() { }
        // RVA: 0x0A90677C  token: 0x600047A
        public System.Void ClearSave() { }
        // RVA: 0x0A906730  token: 0x600047B
        public static System.Void ClearSave(System.String saveName) { }
        // RVA: 0x0A908F98  token: 0x600047C
        public System.Void UpdateMaterial() { }
        // RVA: 0x0A9069DC  token: 0x600047D
        public System.Void CopySize() { }
        // RVA: 0x0A906A80  token: 0x600047E
        public System.Void CopyTexture() { }
        // RVA: 0x0A905E38  token: 0x600047F
        public System.Void Activate() { }
        // RVA: 0x0A906ABC  token: 0x6000480
        public System.Void Deactivate() { }
        // RVA: 0x0A90828C  token: 0x6000481
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A9081C8  token: 0x6000482
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0A9080DC  token: 0x6000483
        protected virtual System.Void OnApplicationPause(System.Boolean paused) { }
        // RVA: 0x0A908110  token: 0x6000484
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x0A9062FC  token: 0x6000485
        public System.Void AddCommand(PaintIn3D.P3dCommand command) { }
        // RVA: 0x0A9065C8  token: 0x6000487
        public System.Void ClearCommands() { }
        // RVA: 0x0A906BD8  token: 0x6000488
        public System.Void ExecuteCommands(System.Boolean sendNotifications, System.Boolean doSort) { }
        // RVA: 0x0A906F80  token: 0x6000489
        private System.Void ExecuteCommands(System.Collections.Generic.List<PaintIn3D.P3dCommand> commands, System.Boolean sendNotifications, UnityEngine.RenderTexture main, UnityEngine.RenderTexture& swap) { }
        // RVA: 0x0A9091A8  token: 0x600048A
        public System.Void .ctor() { }
        // RVA: 0x0A909068  token: 0x600048B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000095  // size: 0x60
    public abstract class P3dPaintableTextureMonitor : UnityEngine.MonoBehaviour
    {
        // Fields
        private PaintIn3D.P3dPaintableTexture paintableTexture;  // 0x18
        private System.Single interval;  // 0x20
        private System.Boolean async;  // 0x24
        protected System.Int32 downsampleSteps;  // 0x28
        private System.Action OnUpdated;  // 0x30
        protected PaintIn3D.P3dPaintableTexture registeredPaintableTexture;  // 0x38
        private System.Single cooldown;  // 0x40
        private PaintIn3D.P3dReader currentReader;  // 0x48
        protected Unity.Collections.NativeArray<UnityEngine.Color32> currentPixels;  // 0x50

        // Properties
        PaintIn3D.P3dPaintableTexture PaintableTexture { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x0A905E14 */ }
        System.Single Interval { get; /* RVA: 0x011EC8A0 */ set; /* RVA: 0x011EC9E0 */ }
        System.Boolean Async { get; /* RVA: 0x04D86870 */ set; /* RVA: 0x04D86880 */ }
        System.Int32 DownsampleSteps { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }
        System.Boolean Registered { get; /* RVA: 0x0A905D30 */ }
        PaintIn3D.P3dReader CurrentReader { get; /* RVA: 0x04D86200 */ }

        // Events
        event System.Action OnUpdated;

        // Methods
        // RVA: 0x0A905760  token: 0x6000499
        public System.Void MarkCurrentReaderAsDirty() { }
        // RVA: 0x0A905908  token: 0x600049A
        public System.Void Register() { }
        // RVA: 0x0A9059E0  token: 0x600049B
        public System.Void Unregister() { }
        // RVA: 0x0A90574C  token: 0x600049C
        protected System.Void InvokeOnUpdated() { }
        // RVA: 0x0A90586C  token: 0x600049D
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A9057D0  token: 0x600049E
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0A905770  token: 0x600049F
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x0A905AAC  token: 0x60004A0
        protected virtual System.Void Update() { }
        // RVA: 0x0A905634  token: 0x60004A1
        private System.Void HandleCompleteCurrent(Unity.Collections.NativeArray<UnityEngine.Color32> pixels) { }
        // RVA: 0x0A905738  token: 0x60004A2
        private System.Void HandleModified(System.Boolean preview) { }
        // RVA: -1  // abstract  token: 0x60004A3
        protected virtual System.Void HandleComplete(System.Int32 boost) { }
        // RVA: 0x0A905C7C  token: 0x60004A4
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000096  // size: 0x98
    public abstract class P3dPaintableTextureMonitorMask : PaintIn3D.P3dPaintableTextureMonitor
    {
        // Fields
        private UnityEngine.Mesh maskMesh;  // 0x60
        private System.Int32 maskSubmesh;  // 0x68
        private UnityEngine.Texture maskTexture;  // 0x70
        private PaintIn3D.P3dChannel maskChannel;  // 0x78
        protected System.Int32 total;  // 0x7c
        private PaintIn3D.P3dReader maskReader;  // 0x80
        protected Unity.Collections.NativeArray<System.Byte> maskPixels;  // 0x88

        // Properties
        UnityEngine.Mesh MaskMesh { get; /* RVA: 0x04D86280 */ set; /* RVA: 0x063CC164 */ }
        System.Int32 MaskSubmesh { get; /* RVA: 0x04D86CF0 */ set; /* RVA: 0x04D86D00 */ }
        UnityEngine.Texture MaskTexture { get; /* RVA: 0x04D862B0 */ set; /* RVA: 0x063AD760 */ }
        PaintIn3D.P3dChannel MaskChannel { get; /* RVA: 0x04D863C0 */ set; /* RVA: 0x04D86440 */ }
        System.Int32 Total { get; /* RVA: 0x04D86410 */ }
        PaintIn3D.P3dReader MaskReader { get; /* RVA: 0x04D86260 */ }

        // Methods
        // RVA: 0x0A905110  token: 0x60004AF
        public System.Void MarkMaskReaderAsDirty() { }
        // RVA: 0x0A904F04  token: 0x60004B0
        private System.Void HandleCompleteMask(Unity.Collections.NativeArray<UnityEngine.Color32> pixels) { }
        // RVA: 0x0A905244  token: 0x60004B1
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A9051A0  token: 0x60004B2
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0A905128  token: 0x60004B3
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x0A9052E8  token: 0x60004B4
        protected virtual System.Void Update() { }
        // RVA: 0x0A905624  token: 0x60004B5
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000097  // size: 0x30
    public class P3dPointConnector
    {
        // Fields
        private System.Single hitSpacing;  // 0x10
        private System.Int32 hitLimit;  // 0x14
        protected System.Boolean connectHits;  // 0x18
        protected System.Boolean clipConnected;  // 0x19
        private System.Collections.Generic.List<PaintIn3D.P3dPointConnector.Link> links;  // 0x20
        private static System.Collections.Generic.Stack<PaintIn3D.P3dPointConnector.Link> linkPool;  // static @ 0x0
        protected PaintIn3D.P3dHitCache hitCache;  // 0x28

        // Properties
        System.Single HitSpacing { get; /* RVA: 0x04D85F70 */ set; /* RVA: 0x04D88280 */ }
        System.Int32 HitLimit { get; /* RVA: 0x04D882A0 */ set; /* RVA: 0x04D8F540 */ }
        System.Boolean ConnectHits { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x04D86350 */ }
        System.Boolean ClipConnected { get; /* RVA: 0x04D866A0 */ set; /* RVA: 0x04D866C0 */ }
        PaintIn3D.P3dHitCache HitCache { get; /* RVA: 0x04D86240 */ }

        // Methods
        // RVA: 0x0A9001FC  token: 0x60004BF
        public System.Void ClearHitCache() { }
        // RVA: 0x0A90AA8C  token: 0x60004C0
        public System.Void ResetConnections() { }
        // RVA: 0x0A90A980  token: 0x60004C1
        public System.Void BreakHits(System.Object owner) { }
        // RVA: 0x0A90AB74  token: 0x60004C2
        public System.Void SubmitLastPoint(UnityEngine.GameObject gameObject, System.Boolean preview, System.Object owner) { }
        // RVA: 0x0A90ADB8  token: 0x60004C3
        public System.Void SubmitPoint(UnityEngine.GameObject gameObject, System.Boolean preview, System.Int32 priority, System.Single pressure, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, System.Object owner) { }
        // RVA: 0x0A90B3C0  token: 0x60004C4
        public System.Void Update() { }
        // RVA: 0x0A90B30C  token: 0x60004C5
        private System.Boolean TryGetLink(System.Object owner, PaintIn3D.P3dPointConnector.Link& link) { }
        // RVA: 0x0A90B57C  token: 0x60004C6
        public System.Void .ctor() { }
        // RVA: 0x0A90B4F0  token: 0x60004C7
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000099  // size: 0x38
    public class P3dReadColor : UnityEngine.MonoBehaviour, PaintIn3D.IHitCoord, PaintIn3D.IHit
    {
        // Fields
        private PaintIn3D.P3dGroup group;  // 0x18
        private System.Boolean preview;  // 0x1c
        private UnityEngine.Color color;  // 0x20
        private PaintIn3D.P3dReadColor.ColorEvent onColor;  // 0x30

        // Properties
        PaintIn3D.P3dGroup Group { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        System.Boolean Preview { get; /* RVA: 0x04D86150 */ set; /* RVA: 0x04D86160 */ }
        UnityEngine.Color Color { get; /* RVA: 0x04D8C200 */ set; /* RVA: 0x04D8C210 */ }
        PaintIn3D.P3dReadColor.ColorEvent OnColor { get; /* RVA: 0x0A90B7F8 */ }

        // Methods
        // RVA: 0x0A90B630  token: 0x60004D0
        public virtual System.Void HandleHitCoord(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, PaintIn3D.P3dHit hit, UnityEngine.Quaternion rotation) { }
        // RVA: 0x05393520  token: 0x60004D1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200009B  // size: 0x58
    public class P3dReader
    {
        // Fields
        private UnityEngine.Rendering.AsyncGPUReadbackRequest request;  // 0x10
        private System.Boolean dirty;  // 0x20
        private System.Boolean requested;  // 0x21
        private UnityEngine.RenderTexture buffer;  // 0x28
        private UnityEngine.Vector2Int originalSize;  // 0x30
        private UnityEngine.Vector2Int downsampledSize;  // 0x38
        private System.Int32 downsampleSteps;  // 0x40
        private System.Int32 downsampleBoost;  // 0x44
        private UnityEngine.Texture2D tempTexture;  // 0x48
        private System.Action<Unity.Collections.NativeArray<UnityEngine.Color32>> OnComplete;  // 0x50

        // Properties
        System.Boolean Dirty { get; /* RVA: 0x04D866B0 */ }
        System.Boolean Requested { get; /* RVA: 0x04D86850 */ }
        UnityEngine.Vector2Int OriginalSize { get; /* RVA: 0x011F36E0 */ }
        System.Int32 DownsampleSteps { get; /* RVA: 0x04D865E0 */ }
        UnityEngine.Vector2Int DownsampledSize { get; /* RVA: 0x04D85A50 */ }
        System.Int32 DownsampleBoost { get; /* RVA: 0x04D86530 */ }

        // Events
        event System.Action<Unity.Collections.NativeArray<UnityEngine.Color32>> OnComplete;

        // Methods
        // RVA: 0x04D8F6A0  token: 0x60004DB
        public System.Void MarkAsDirty() { }
        // RVA: 0x0A90BEC8  token: 0x60004DC
        public System.Void UpdateRequest() { }
        // RVA: -1  // generic def  token: 0x60004DD
        public static System.Boolean NeedsUpdating(PaintIn3D.P3dReader reader, Unity.Collections.NativeArray<T> array, UnityEngine.RenderTexture texture, System.Int32 downsampleSteps) { }
        // RVA: 0x0A90BBF8  token: 0x60004DE
        public System.Void Request(UnityEngine.RenderTexture texture, System.Int32 downsample, System.Boolean async) { }
        // RVA: 0x0A90BB58  token: 0x60004DF
        public System.Void Release() { }
        // RVA: 0x0A90B868  token: 0x60004E0
        private System.Void CompleteDirectly() { }
        // RVA: 0x041E1670  token: 0x60004E1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200009C  // size: 0x10
    public static class P3dSerialization
    {
        // Fields
        public static System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Material> HashToMaterial;  // static @ 0x0
        public static System.Collections.Generic.Dictionary<UnityEngine.Material,System.Int32> MaterialToHash;  // static @ 0x8
        public static System.Collections.Generic.Dictionary<PaintIn3D.P3dHash,PaintIn3D.P3dModel> HashToModel;  // static @ 0x10
        public static System.Collections.Generic.Dictionary<PaintIn3D.P3dModel,PaintIn3D.P3dHash> ModelToHash;  // static @ 0x18
        public static System.Collections.Generic.Dictionary<PaintIn3D.P3dHash,UnityEngine.Texture> HashToTexture;  // static @ 0x20
        public static System.Collections.Generic.Dictionary<UnityEngine.Texture,PaintIn3D.P3dHash> TextureToHash;  // static @ 0x28
        public static System.Collections.Generic.Dictionary<PaintIn3D.P3dHash,PaintIn3D.P3dPaintableTexture> HashToPaintableTexture;  // static @ 0x30
        public static System.Collections.Generic.Dictionary<PaintIn3D.P3dPaintableTexture,PaintIn3D.P3dHash> PaintableTextureToHash;  // static @ 0x38

        // Methods
        // RVA: 0x0A90FD4C  token: 0x60004E2
        public static System.Void TryRegister(PaintIn3D.P3dPaintableTexture paintableTexture, PaintIn3D.P3dHash hash) { }
        // RVA: 0x0A90F9D0  token: 0x60004E3
        public static System.Void TryRegister(PaintIn3D.P3dModel model, PaintIn3D.P3dHash hash) { }
        // RVA: 0x0A90FCD0  token: 0x60004E4
        public static System.Void TryRegister(UnityEngine.Texture texture, PaintIn3D.P3dHash hash) { }
        // RVA: -1  // generic def  token: 0x60004E5
        public static System.Void TryRegister(T obj, PaintIn3D.P3dHash hash, System.Collections.Generic.Dictionary<PaintIn3D.P3dHash,T> hashToObj, System.Collections.Generic.Dictionary<T,PaintIn3D.P3dHash> objToHash) { }
        // RVA: 0x0A90FA58  token: 0x60004E6
        public static System.Int32 TryRegister(UnityEngine.Material material) { }
        // RVA: 0x0A90F97C  token: 0x60004E7
        private static System.Int32 GetStableStringHash(System.String s) { }
        // RVA: 0x0A90FDC8  token: 0x60004E8
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200009D  // size: 0x20
    public sealed struct P3dSlot
    {
        // Fields
        public System.Int32 Index;  // 0x10
        public System.String Name;  // 0x18

        // Methods
        // RVA: 0x06B17430  token: 0x60004E9
        public System.Void .ctor(System.Int32 newIndex, System.String newName) { }
        // RVA: 0x0A910190  token: 0x60004EA
        public UnityEngine.Texture FindTexture(UnityEngine.GameObject gameObject) { }
        // RVA: 0x0A9102EC  token: 0x60004EB
        public System.Boolean IsTransformed(UnityEngine.GameObject gameObject) { }
        // RVA: 0x0A910130  token: 0x60004EC
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0A9102C0  token: 0x60004ED
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0A91049C  token: 0x60004EE
        public static System.Boolean op_Equality(PaintIn3D.P3dSlot a, PaintIn3D.P3dSlot b) { }
        // RVA: 0x0A9104B8  token: 0x60004EF
        public static System.Boolean op_Inequality(PaintIn3D.P3dSlot a, PaintIn3D.P3dSlot b) { }

    }

    // TypeToken: 0x200009E  // size: 0x10
    public static class P3dStateManager
    {
        // Properties
        System.Boolean CanUndo { get; /* RVA: 0x0A910B08 */ }
        System.Boolean CanRedo { get; /* RVA: 0x0A9109C4 */ }

        // Methods
        // RVA: 0x0A91074C  token: 0x60004F2
        public static System.Void StoreAllStates() { }
        // RVA: 0x0A9104D4  token: 0x60004F3
        public static System.Void ClearAllStates() { }
        // RVA: 0x0A910888  token: 0x60004F4
        public static System.Void UndoAll() { }
        // RVA: 0x0A910610  token: 0x60004F5
        public static System.Void RedoAll() { }

    }

    // TypeToken: 0x200009F  // size: 0x28
    public class P3dTextureHash : UnityEngine.MonoBehaviour
    {
        // Fields
        private UnityEngine.Texture texture;  // 0x18
        private PaintIn3D.P3dHash hash;  // 0x20

        // Properties
        UnityEngine.Texture Texture { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        PaintIn3D.P3dHash Hash { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x0A910CF0 */ }

        // Methods
        // RVA: 0x0A910C9C  token: 0x60004FA
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0A910C4C  token: 0x60004FB
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x05393520  token: 0x60004FC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A0  // size: 0xE0
    public class P3dPaintDecal : UnityEngine.MonoBehaviour, PaintIn3D.IHitPoint, PaintIn3D.IHit, PaintIn3D.IHitLine, PaintIn3D.IHitTriangle, PaintIn3D.IHitQuad, PaintIn3D.IHitCoord
    {
        // Fields
        private UnityEngine.LayerMask layers;  // 0x18
        private PaintIn3D.P3dModel targetModel;  // 0x20
        private PaintIn3D.P3dGroup group;  // 0x28
        private PaintIn3D.P3dPaintableTexture targetTexture;  // 0x30
        private PaintIn3D.P3dBlendMode blendMode;  // 0x38
        private UnityEngine.Texture texture;  // 0x70
        private UnityEngine.Texture shape;  // 0x78
        private PaintIn3D.P3dChannel shapeChannel;  // 0x80
        private UnityEngine.Color color;  // 0x84
        private System.Single opacity;  // 0x94
        private System.Single angle;  // 0x98
        private UnityEngine.Vector3 scale;  // 0x9c
        private System.Single radius;  // 0xa8
        private System.Single hardness;  // 0xac
        private System.Single wrapping;  // 0xb0
        private System.Single normalFront;  // 0xb4
        private System.Single normalBack;  // 0xb8
        private System.Single normalFade;  // 0xbc
        private UnityEngine.Texture tileTexture;  // 0xc0
        private UnityEngine.Transform tileTransform;  // 0xc8
        private System.Single tileOpacity;  // 0xd0
        private System.Single tileTransition;  // 0xd4
        private PaintIn3D.P3dModifierList modifiers;  // 0xd8

        // Properties
        UnityEngine.LayerMask Layers { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        PaintIn3D.P3dModel TargetModel { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        PaintIn3D.P3dGroup Group { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }
        PaintIn3D.P3dPaintableTexture TargetTexture { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        PaintIn3D.P3dBlendMode BlendMode { get; /* RVA: 0x04DA62B0 */ set; /* RVA: 0x0A90D980 */ }
        UnityEngine.Texture Texture { get; /* RVA: 0x04D862B0 */ set; /* RVA: 0x063AD760 */ }
        UnityEngine.Texture Shape { get; /* RVA: 0x04D862A0 */ set; /* RVA: 0x06402320 */ }
        PaintIn3D.P3dChannel ShapeChannel { get; /* RVA: 0x04D863A0 */ set; /* RVA: 0x04D86420 */ }
        UnityEngine.Color Color { get; /* RVA: 0x04DA62E0 */ set; /* RVA: 0x04DA6310 */ }
        System.Single Opacity { get; /* RVA: 0x04D8F070 */ set; /* RVA: 0x04D8F0A0 */ }
        System.Single Angle { get; /* RVA: 0x04D8E570 */ set; /* RVA: 0x04D8E590 */ }
        UnityEngine.Vector3 Scale { get; /* RVA: 0x04D90DF0 */ set; /* RVA: 0x04D90E50 */ }
        System.Single Radius { get; /* RVA: 0x04D8E650 */ set; /* RVA: 0x04D8E680 */ }
        System.Single Hardness { get; /* RVA: 0x04D8E660 */ set; /* RVA: 0x04D8E690 */ }
        System.Single Wrapping { get; /* RVA: 0x04DA6300 */ set; /* RVA: 0x04DA6330 */ }
        System.Single NormalFront { get; /* RVA: 0x04DA32B0 */ set; /* RVA: 0x04DA3350 */ }
        System.Single NormalBack { get; /* RVA: 0x04DA32D0 */ set; /* RVA: 0x04D87E30 */ }
        System.Single NormalFade { get; /* RVA: 0x04DA62F0 */ set; /* RVA: 0x04DA6320 */ }
        UnityEngine.Texture TileTexture { get; /* RVA: 0x04D892F0 */ set; /* RVA: 0x06AC5ACC */ }
        UnityEngine.Transform TileTransform { get; /* RVA: 0x04D8D1D0 */ set; /* RVA: 0x06AC5B30 */ }
        System.Single TileOpacity { get; /* RVA: 0x04D8E2E0 */ set; /* RVA: 0x04D8E320 */ }
        System.Single TileTransition { get; /* RVA: 0x04D8E2C0 */ set; /* RVA: 0x04D8E300 */ }
        PaintIn3D.P3dModifierList Modifiers { get; /* RVA: 0x0A90D918 */ }

        // Methods
        // RVA: 0x04DA6250  token: 0x600052A
        public System.Void FlipHorizontal() { }
        // RVA: 0x04DA6270  token: 0x600052B
        public System.Void FlipVertical() { }
        // RVA: 0x0A90D77C  token: 0x600052C
        public System.Void IncrementOpacity(System.Single delta) { }
        // RVA: 0x0A90D73C  token: 0x600052D
        public System.Void IncrementAngle(System.Single degrees) { }
        // RVA: 0x0A90D7A0  token: 0x600052E
        public System.Void MultiplyScale(System.Single multiplier) { }
        // RVA: 0x04DA6290  token: 0x600052F
        public System.Void MultiplyHardness(System.Single multiplier) { }
        // RVA: 0x0A90CDA8  token: 0x6000530
        public virtual System.Void HandleHitPoint(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0A90CB40  token: 0x6000531
        public virtual System.Void HandleHitLine(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Quaternion rotation, System.Boolean clip) { }
        // RVA: 0x0A90D30C  token: 0x6000532
        public virtual System.Void HandleHitTriangle(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 positionA, UnityEngine.Vector3 positionB, UnityEngine.Vector3 positionC, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0A90D010  token: 0x6000533
        public virtual System.Void HandleHitQuad(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Vector3 position2, UnityEngine.Vector3 endPosition2, UnityEngine.Quaternion rotation, System.Boolean clip) { }
        // RVA: 0x0A90C7C0  token: 0x6000534
        public virtual System.Void HandleHitCoord(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, PaintIn3D.P3dHit hit, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0A90C2E8  token: 0x6000535
        private UnityEngine.Vector3 HandleHitCommon(System.Boolean preview, System.Single pressure, System.Int32 seed, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0A90D5AC  token: 0x6000536
        private System.Void HandleMaskCommon(UnityEngine.Vector3 worldPosition) { }
        // RVA: 0x0A90D7F0  token: 0x6000537
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A1  // size: 0x80
    public class P3dPaintFill : UnityEngine.MonoBehaviour, PaintIn3D.IHitCoord, PaintIn3D.IHit
    {
        // Fields
        private PaintIn3D.P3dGroup group;  // 0x18
        private PaintIn3D.P3dBlendMode blendMode;  // 0x20
        private UnityEngine.Texture texture;  // 0x58
        private UnityEngine.Color color;  // 0x60
        private System.Single opacity;  // 0x70
        private System.Single minimum;  // 0x74
        private PaintIn3D.P3dModifierList modifiers;  // 0x78

        // Properties
        PaintIn3D.P3dGroup Group { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        PaintIn3D.P3dBlendMode BlendMode { get; /* RVA: 0x04DA6340 */ set; /* RVA: 0x0A90DDEC */ }
        UnityEngine.Texture Texture { get; /* RVA: 0x02E56440 */ set; /* RVA: 0x036E5590 */ }
        UnityEngine.Color Color { get; /* RVA: 0x04DA1280 */ set; /* RVA: 0x04DA1330 */ }
        System.Single Opacity { get; /* RVA: 0x04D90610 */ set; /* RVA: 0x04D90600 */ }
        System.Single Minimum { get; /* RVA: 0x04DA60B0 */ set; /* RVA: 0x04DA60C0 */ }
        PaintIn3D.P3dModifierList Modifiers { get; /* RVA: 0x0A90DD90 */ }

        // Methods
        // RVA: 0x0A90DCB8  token: 0x6000545
        public System.Void IncrementOpacity(System.Single delta) { }
        // RVA: 0x0A90D9AC  token: 0x6000546
        public virtual System.Void HandleHitCoord(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, PaintIn3D.P3dHit hit, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0A90DCD4  token: 0x6000547
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A2  // size: 0x40
    public class P3dPaintReplace : UnityEngine.MonoBehaviour, PaintIn3D.IHitCoord, PaintIn3D.IHit
    {
        // Fields
        private PaintIn3D.P3dGroup group;  // 0x18
        private UnityEngine.Texture texture;  // 0x20
        private UnityEngine.Color color;  // 0x28
        private PaintIn3D.P3dModifierList modifiers;  // 0x38

        // Properties
        PaintIn3D.P3dGroup Group { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        UnityEngine.Texture Texture { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        UnityEngine.Color Color { get; /* RVA: 0x04D8FAD0 */ set; /* RVA: 0x04D8FAE0 */ }
        PaintIn3D.P3dModifierList Modifiers { get; /* RVA: 0x0A90E36C */ }

        // Methods
        // RVA: 0x0A90E094  token: 0x600054F
        public virtual System.Void HandleHitCoord(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, PaintIn3D.P3dHit hit, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0A90E334  token: 0x6000550
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A3  // size: 0x80
    public class P3dPaintReplaceChannels : UnityEngine.MonoBehaviour, PaintIn3D.IHitCoord, PaintIn3D.IHit
    {
        // Fields
        private PaintIn3D.P3dGroup group;  // 0x18
        private UnityEngine.Texture textureR;  // 0x20
        private UnityEngine.Texture textureG;  // 0x28
        private UnityEngine.Texture textureB;  // 0x30
        private UnityEngine.Texture textureA;  // 0x38
        private UnityEngine.Vector4 channelR;  // 0x40
        private UnityEngine.Vector4 channelG;  // 0x50
        private UnityEngine.Vector4 channelB;  // 0x60
        private UnityEngine.Vector4 channelA;  // 0x70

        // Properties
        PaintIn3D.P3dGroup Group { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        UnityEngine.Texture TextureR { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        UnityEngine.Texture TextureG { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        UnityEngine.Texture TextureB { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        UnityEngine.Texture TextureA { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }
        UnityEngine.Vector4 ChannelR { get; /* RVA: 0x04D905C0 */ set; /* RVA: 0x04D926F0 */ }
        UnityEngine.Vector4 ChannelG { get; /* RVA: 0x04D8CE10 */ set; /* RVA: 0x04D926F0 */ }
        UnityEngine.Vector4 ChannelB { get; /* RVA: 0x04DA1280 */ set; /* RVA: 0x04D926F0 */ }
        UnityEngine.Vector4 ChannelA { get; /* RVA: 0x04D8FD40 */ set; /* RVA: 0x04D926F0 */ }

        // Methods
        // RVA: 0x0A90DE18  token: 0x6000563
        public virtual System.Void HandleHitCoord(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, PaintIn3D.P3dHit hit, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0A90E064  token: 0x6000564
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A4  // size: 0xB8
    public class P3dPaintSphere : UnityEngine.MonoBehaviour, PaintIn3D.IHitPoint, PaintIn3D.IHit, PaintIn3D.IHitLine, PaintIn3D.IHitTriangle, PaintIn3D.IHitQuad, PaintIn3D.IHitCoord
    {
        // Fields
        private UnityEngine.LayerMask layers;  // 0x18
        private PaintIn3D.P3dGroup group;  // 0x1c
        private PaintIn3D.P3dModel targetModel;  // 0x20
        private PaintIn3D.P3dPaintableTexture targetTexture;  // 0x28
        private PaintIn3D.P3dBlendMode blendMode;  // 0x30
        private UnityEngine.Color color;  // 0x68
        private System.Single opacity;  // 0x78
        private System.Single angle;  // 0x7c
        private UnityEngine.Vector3 scale;  // 0x80
        private System.Single radius;  // 0x8c
        private System.Single hardness;  // 0x90
        private UnityEngine.Texture tileTexture;  // 0x98
        private UnityEngine.Transform tileTransform;  // 0xa0
        private System.Single tileOpacity;  // 0xa8
        private System.Single tileTransition;  // 0xac
        private PaintIn3D.P3dModifierList modifiers;  // 0xb0

        // Properties
        UnityEngine.LayerMask Layers { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        PaintIn3D.P3dGroup Group { get; /* RVA: 0x04D86310 */ set; /* RVA: 0x04D86320 */ }
        PaintIn3D.P3dModel TargetModel { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        PaintIn3D.P3dPaintableTexture TargetTexture { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        PaintIn3D.P3dBlendMode BlendMode { get; /* RVA: 0x04DA6390 */ set; /* RVA: 0x0A90F950 */ }
        UnityEngine.Color Color { get; /* RVA: 0x04DA63C0 */ set; /* RVA: 0x04DA63D0 */ }
        System.Single Opacity { get; /* RVA: 0x04D8ED60 */ set; /* RVA: 0x04D8ED70 */ }
        System.Single Angle { get; /* RVA: 0x04D88E80 */ set; /* RVA: 0x04D8ED80 */ }
        UnityEngine.Vector3 Scale { get; /* RVA: 0x04D91900 */ set; /* RVA: 0x04D91970 */ }
        System.Single Radius { get; /* RVA: 0x04D8E210 */ set; /* RVA: 0x04D8E230 */ }
        System.Single Hardness { get; /* RVA: 0x04D8FD30 */ set; /* RVA: 0x04D8FD60 */ }
        UnityEngine.Texture TileTexture { get; /* RVA: 0x04D85EE0 */ set; /* RVA: 0x06352D0C */ }
        UnityEngine.Transform TileTransform { get; /* RVA: 0x04D86210 */ set; /* RVA: 0x05391334 */ }
        System.Single TileOpacity { get; /* RVA: 0x04D8E650 */ set; /* RVA: 0x04D8E680 */ }
        System.Single TileTransition { get; /* RVA: 0x04D8E660 */ set; /* RVA: 0x04D8E690 */ }
        PaintIn3D.P3dModifierList Modifiers { get; /* RVA: 0x0A90F8E8 */ }

        // Methods
        // RVA: 0x0A90F778  token: 0x6000584
        public System.Void IncrementOpacity(System.Single delta) { }
        // RVA: 0x0A90F73C  token: 0x6000585
        public System.Void IncrementAngle(System.Single degrees) { }
        // RVA: 0x04DA6370  token: 0x6000586
        public System.Void MultiplyRadius(System.Single multiplier) { }
        // RVA: 0x0A90F794  token: 0x6000587
        public System.Void MultiplyScale(System.Single multiplier) { }
        // RVA: 0x0A90EDA8  token: 0x6000588
        public virtual System.Void HandleHitPoint(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0A90EB40  token: 0x6000589
        public virtual System.Void HandleHitLine(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Quaternion rotation, System.Boolean clip) { }
        // RVA: 0x0A90F30C  token: 0x600058A
        public virtual System.Void HandleHitTriangle(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 positionA, UnityEngine.Vector3 positionB, UnityEngine.Vector3 positionC, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0A90F010  token: 0x600058B
        public virtual System.Void HandleHitQuad(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Vector3 position2, UnityEngine.Vector3 endPosition2, UnityEngine.Quaternion rotation, System.Boolean clip) { }
        // RVA: 0x0A90E7C0  token: 0x600058C
        public virtual System.Void HandleHitCoord(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, PaintIn3D.P3dHit hit, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0A90E3C8  token: 0x600058D
        private UnityEngine.Vector3 HandleHitCommon(System.Boolean preview, System.Single pressure, System.Int32 seed, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0A90F5AC  token: 0x600058E
        private System.Void HandleMaskCommon(UnityEngine.Vector3 worldPosition) { }
        // RVA: 0x0A90F7E4  token: 0x600058F
        public System.Void .ctor() { }

    }

}

