// ========================================================
// Dumped by @desirepro
// Assembly: PaintIn3D.dll
// Classes:  164
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

CLASS: StringEvent
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0A8F6090  token=0x6000029  System.Void .ctor()
END_CLASS

CLASS: ChannelType
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dChannelCounterEvent.ChannelTypeRed  // const
  public    static  PaintIn3D.P3dChannelCounterEvent.ChannelTypeGreen  // const
  public    static  PaintIn3D.P3dChannelCounterEvent.ChannelTypeBlue  // const
  public    static  PaintIn3D.P3dChannelCounterEvent.ChannelTypeAlpha  // const
METHODS:
END_CLASS

CLASS: ChannelType
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dChannelCounterFill.ChannelTypeRed  // const
  public    static  PaintIn3D.P3dChannelCounterFill.ChannelTypeGreen  // const
  public    static  PaintIn3D.P3dChannelCounterFill.ChannelTypeBlue  // const
  public    static  PaintIn3D.P3dChannelCounterFill.ChannelTypeAlpha  // const
METHODS:
END_CLASS

CLASS: StringEvent
TYPE:  class
TOKEN: 0x200000E
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0A8F6054  token=0x600004B  System.Void .ctor()
END_CLASS

CLASS: ChannelType
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dChannelCounterText.ChannelTypeRed  // const
  public    static  PaintIn3D.P3dChannelCounterText.ChannelTypeGreen  // const
  public    static  PaintIn3D.P3dChannelCounterText.ChannelTypeBlue  // const
  public    static  PaintIn3D.P3dChannelCounterText.ChannelTypeAlpha  // const
METHODS:
END_CLASS

CLASS: StringEvent
TYPE:  class
TOKEN: 0x2000013
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0A8F6018  token=0x600006D  System.Void .ctor()
END_CLASS

CLASS: PhaseType
TYPE:  sealed struct
TOKEN: 0x200001B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitBetween.PhaseTypeUpdate  // const
  public    static  PaintIn3D.P3dHitBetween.PhaseTypeFixedUpdate  // const
METHODS:
END_CLASS

CLASS: OrientationType
TYPE:  sealed struct
TOKEN: 0x200001C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitBetween.OrientationTypeWorldUp  // const
  public    static  PaintIn3D.P3dHitBetween.OrientationTypeCameraUp  // const
  public    static  PaintIn3D.P3dHitBetween.OrientationTypeThisRotation  // const
  public    static  PaintIn3D.P3dHitBetween.OrientationTypeThisLocalRotation  // const
  public    static  PaintIn3D.P3dHitBetween.OrientationTypeCustomRotation  // const
  public    static  PaintIn3D.P3dHitBetween.OrientationTypeCustomLocalRotation  // const
METHODS:
END_CLASS

CLASS: NormalType
TYPE:  sealed struct
TOKEN: 0x200001D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitBetween.NormalTypeHitNormal  // const
  public    static  PaintIn3D.P3dHitBetween.NormalTypeRayDirection  // const
METHODS:
END_CLASS

CLASS: EmitType
TYPE:  sealed struct
TOKEN: 0x200001E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitBetween.EmitTypePointsIn3D  // const
  public    static  PaintIn3D.P3dHitBetween.EmitTypePointsOnUV  // const
  public    static  PaintIn3D.P3dHitBetween.EmitTypeTrianglesIn3D  // const
METHODS:
END_CLASS

CLASS: EmitType
TYPE:  sealed struct
TOKEN: 0x2000021
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitCollisions.EmitTypePointsIn3D  // const
  public    static  PaintIn3D.P3dHitCollisions.EmitTypePointsOnUV  // const
  public    static  PaintIn3D.P3dHitCollisions.EmitTypeTrianglesIn3D  // const
METHODS:
END_CLASS

CLASS: OrientationType
TYPE:  sealed struct
TOKEN: 0x2000022
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitCollisions.OrientationTypeWorldUp  // const
  public    static  PaintIn3D.P3dHitCollisions.OrientationTypeCameraUp  // const
METHODS:
END_CLASS

CLASS: PressureType
TYPE:  sealed struct
TOKEN: 0x2000023
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitCollisions.PressureTypeConstant  // const
  public    static  PaintIn3D.P3dHitCollisions.PressureTypeImpactSpeed  // const
METHODS:
END_CLASS

CLASS: PhaseType
TYPE:  sealed struct
TOKEN: 0x2000025
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitNearby.PhaseTypeManuallyOnly  // const
  public    static  PaintIn3D.P3dHitNearby.PhaseTypeUpdate  // const
  public    static  PaintIn3D.P3dHitNearby.PhaseTypeFixedUpdate  // const
METHODS:
END_CLASS

CLASS: EmitType
TYPE:  sealed struct
TOKEN: 0x2000027
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitParticles.EmitTypePointsIn3D  // const
  public    static  PaintIn3D.P3dHitParticles.EmitTypePointsOnUV  // const
  public    static  PaintIn3D.P3dHitParticles.EmitTypeTrianglesIn3D  // const
METHODS:
END_CLASS

CLASS: OrientationType
TYPE:  sealed struct
TOKEN: 0x2000028
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitParticles.OrientationTypeWorldUp  // const
  public    static  PaintIn3D.P3dHitParticles.OrientationTypeCameraUp  // const
METHODS:
END_CLASS

CLASS: NormalType
TYPE:  sealed struct
TOKEN: 0x2000029
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitParticles.NormalTypeParticleVelocity  // const
  public    static  PaintIn3D.P3dHitParticles.NormalTypeCollisionNormal  // const
METHODS:
END_CLASS

CLASS: PressureType
TYPE:  sealed struct
TOKEN: 0x200002A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitParticles.PressureTypeConstant  // const
  public    static  PaintIn3D.P3dHitParticles.PressureTypeDistance  // const
  public    static  PaintIn3D.P3dHitParticles.PressureTypeSpeed  // const
METHODS:
END_CLASS

CLASS: Link
TYPE:  class
TOKEN: 0x200002C
SIZE:  0x40
EXTENDS: Link
FIELDS:
  public            System.Single                   Age  // 0x18
  public            System.Boolean                  Down  // 0x1c
  public            System.Int32                    State  // 0x20
  public            System.Single                   Distance  // 0x24
  public            UnityEngine.Vector2             ScreenDelta  // 0x28
  public            UnityEngine.Vector2             ScreenOld  // 0x30
  public            System.Collections.Generic.List<UnityEngine.Vector2>History  // 0x38
METHODS:
  RVA=0x0A8E34D0  token=0x600016E  System.Void Move(UnityEngine.Vector2 screenNew)
  RVA=0x0A8E3508  token=0x600016F  System.Boolean TryMove(UnityEngine.Vector2 screenNew)
  RVA=0x0A8E346C  token=0x6000170  System.Void Clear()
  RVA=0x0A8E3594  token=0x6000171  System.Void .ctor()
END_CLASS

CLASS: FrequencyType
TYPE:  sealed struct
TOKEN: 0x200002D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitScreen.FrequencyTypePixelInterval  // const
  public    static  PaintIn3D.P3dHitScreen.FrequencyTypeScaledPixelInterval  // const
  public    static  PaintIn3D.P3dHitScreen.FrequencyTypeTimeInterval  // const
  public    static  PaintIn3D.P3dHitScreen.FrequencyTypeOnceOnRelease  // const
  public    static  PaintIn3D.P3dHitScreen.FrequencyTypeOnceOnPress  // const
  public    static  PaintIn3D.P3dHitScreen.FrequencyTypeOnceEveryFrame  // const
METHODS:
END_CLASS

CLASS: RotationType
TYPE:  sealed struct
TOKEN: 0x200002F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitScreenBase.RotationTypeNormal  // const
  public    static  PaintIn3D.P3dHitScreenBase.RotationTypeWorld  // const
  public    static  PaintIn3D.P3dHitScreenBase.RotationTypeThisRotation  // const
  public    static  PaintIn3D.P3dHitScreenBase.RotationTypeThisLocalRotation  // const
  public    static  PaintIn3D.P3dHitScreenBase.RotationTypeCustomRotation  // const
  public    static  PaintIn3D.P3dHitScreenBase.RotationTypeCustomLocalRotation  // const
METHODS:
END_CLASS

CLASS: RelativeType
TYPE:  sealed struct
TOKEN: 0x2000030
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitScreenBase.RelativeTypeWorldUp  // const
  public    static  PaintIn3D.P3dHitScreenBase.RelativeTypeCameraUp  // const
  public    static  PaintIn3D.P3dHitScreenBase.RelativeTypeDrawAngle  // const
METHODS:
END_CLASS

CLASS: DirectionType
TYPE:  sealed struct
TOKEN: 0x2000031
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitScreenBase.DirectionTypeHitNormal  // const
  public    static  PaintIn3D.P3dHitScreenBase.DirectionTypeRayDirection  // const
  public    static  PaintIn3D.P3dHitScreenBase.DirectionTypeCameraDirection  // const
METHODS:
END_CLASS

CLASS: EmitType
TYPE:  sealed struct
TOKEN: 0x2000032
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitScreenBase.EmitTypePointsIn3D  // const
  public    static  PaintIn3D.P3dHitScreenBase.EmitTypePointsOnUV  // const
  public    static  PaintIn3D.P3dHitScreenBase.EmitTypeTrianglesIn3D  // const
METHODS:
END_CLASS

CLASS: ButtonTypes
TYPE:  sealed struct
TOKEN: 0x2000033
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitScreenBase.ButtonTypesLeftMouse  // const
  public    static  PaintIn3D.P3dHitScreenBase.ButtonTypesRightMouse  // const
  public    static  PaintIn3D.P3dHitScreenBase.ButtonTypesMiddleMouse  // const
  public    static  PaintIn3D.P3dHitScreenBase.ButtonTypesTouch  // const
METHODS:
END_CLASS

CLASS: FrequencyType
TYPE:  sealed struct
TOKEN: 0x2000036
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitScreenLine.FrequencyTypeStartAndEnd  // const
  public    static  PaintIn3D.P3dHitScreenLine.FrequencyTypePixelInterval  // const
  public    static  PaintIn3D.P3dHitScreenLine.FrequencyTypeScaledPixelInterval  // const
  public    static  PaintIn3D.P3dHitScreenLine.FrequencyTypeStretchedPixelInterval  // const
  public    static  PaintIn3D.P3dHitScreenLine.FrequencyTypeStretchedScaledPixelInterval  // const
  public    static  PaintIn3D.P3dHitScreenLine.FrequencyTypeOnce  // const
METHODS:
END_CLASS

CLASS: PhaseType
TYPE:  sealed struct
TOKEN: 0x2000038
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitThrough.PhaseTypeUpdate  // const
  public    static  PaintIn3D.P3dHitThrough.PhaseTypeFixedUpdate  // const
METHODS:
END_CLASS

CLASS: OrientationType
TYPE:  sealed struct
TOKEN: 0x2000039
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dHitThrough.OrientationTypeWorldUp  // const
  public    static  PaintIn3D.P3dHitThrough.OrientationTypeCameraUp  // const
METHODS:
END_CLASS

CLASS: Pair
TYPE:  class
TOKEN: 0x2000046
SIZE:  0x20
FIELDS:
  public            UnityEngine.Mesh                Source  // 0x10
  public            UnityEngine.Mesh                Output  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600020B  System.Void .ctor()
END_CLASS

CLASS: Ring
TYPE:  class
TOKEN: 0x2000047
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.List<PaintIn3D.P3dSeamFixer.Point>Points  // 0x10
METHODS:
  RVA=0x0A8F5F20  token=0x600020C  PaintIn3D.P3dSeamFixer.Point GetPoint(System.Int32 index)
  RVA=0x0A8F5FA0  token=0x600020D  System.Void .ctor()
END_CLASS

CLASS: Edge
TYPE:  class
TOKEN: 0x2000048
SIZE:  0x30
FIELDS:
  public            System.Boolean                  Used  // 0x10
  public            PaintIn3D.P3dSeamFixer.Point    PointA  // 0x18
  public            PaintIn3D.P3dSeamFixer.Point    PointB  // 0x20
  public            System.Boolean                  Flip  // 0x28
METHODS:
  RVA=0x0A8E344C  token=0x600020E  System.Boolean Match(PaintIn3D.P3dSeamFixer.Point a, PaintIn3D.P3dSeamFixer.Point b)
  RVA=0x041E1670  token=0x600020F  System.Void .ctor()
END_CLASS

CLASS: Point
TYPE:  class
TOKEN: 0x2000049
SIZE:  0x30
FIELDS:
  public            System.Int32                    Index  // 0x10
  public            UnityEngine.Vector2             Coord  // 0x14
  public            PaintIn3D.P3dSeamFixer.InsertionOuter  // 0x20
  public            System.Collections.Generic.List<PaintIn3D.P3dSeamFixer.Edge>Edges  // 0x28
METHODS:
  RVA=0x0A8F5EA8  token=0x6000210  System.Void .ctor()
END_CLASS

CLASS: Insertion
TYPE:  class
TOKEN: 0x200004A
SIZE:  0x20
FIELDS:
  public            System.Int32                    Index  // 0x10
  public            System.Int32                    NewIndex  // 0x14
  public            UnityEngine.Vector2             NewCoord  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000211  System.Void .ctor()
END_CLASS

CLASS: BlendType
TYPE:  sealed struct
TOKEN: 0x200004E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dModifyAngleRandom.BlendTypeReplace  // const
  public    static  PaintIn3D.P3dModifyAngleRandom.BlendTypeMultiply  // const
  public    static  PaintIn3D.P3dModifyAngleRandom.BlendTypeIncrement  // const
METHODS:
END_CLASS

CLASS: BlendType
TYPE:  sealed struct
TOKEN: 0x2000050
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dModifyColorRandom.BlendTypeReplace  // const
  public    static  PaintIn3D.P3dModifyColorRandom.BlendTypeMultiply  // const
  public    static  PaintIn3D.P3dModifyColorRandom.BlendTypeIncrement  // const
METHODS:
END_CLASS

CLASS: BlendType
TYPE:  sealed struct
TOKEN: 0x2000052
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dModifyHardnessPressure.BlendTypeReplace  // const
  public    static  PaintIn3D.P3dModifyHardnessPressure.BlendTypeMultiply  // const
  public    static  PaintIn3D.P3dModifyHardnessPressure.BlendTypeIncrement  // const
METHODS:
END_CLASS

CLASS: BlendType
TYPE:  sealed struct
TOKEN: 0x2000054
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dModifyHardnessRandom.BlendTypeReplace  // const
  public    static  PaintIn3D.P3dModifyHardnessRandom.BlendTypeMultiply  // const
  public    static  PaintIn3D.P3dModifyHardnessRandom.BlendTypeIncrement  // const
METHODS:
END_CLASS

CLASS: BlendType
TYPE:  sealed struct
TOKEN: 0x2000056
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dModifyOpacityPressure.BlendTypeReplace  // const
  public    static  PaintIn3D.P3dModifyOpacityPressure.BlendTypeMultiply  // const
  public    static  PaintIn3D.P3dModifyOpacityPressure.BlendTypeIncrement  // const
METHODS:
END_CLASS

CLASS: BlendType
TYPE:  sealed struct
TOKEN: 0x2000058
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dModifyOpacityRandom.BlendTypeReplace  // const
  public    static  PaintIn3D.P3dModifyOpacityRandom.BlendTypeMultiply  // const
  public    static  PaintIn3D.P3dModifyOpacityRandom.BlendTypeIncrement  // const
METHODS:
END_CLASS

CLASS: BlendType
TYPE:  sealed struct
TOKEN: 0x200005B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dModifyRadiusPressure.BlendTypeReplace  // const
  public    static  PaintIn3D.P3dModifyRadiusPressure.BlendTypeMultiply  // const
  public    static  PaintIn3D.P3dModifyRadiusPressure.BlendTypeIncrement  // const
METHODS:
END_CLASS

CLASS: BlendType
TYPE:  sealed struct
TOKEN: 0x200005D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dModifyRadiusRandom.BlendTypeReplace  // const
  public    static  PaintIn3D.P3dModifyRadiusRandom.BlendTypeMultiply  // const
  public    static  PaintIn3D.P3dModifyRadiusRandom.BlendTypeIncrement  // const
METHODS:
END_CLASS

CLASS: BlendType
TYPE:  sealed struct
TOKEN: 0x200005F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dModifyScaleRandom.BlendTypeReplace  // const
  public    static  PaintIn3D.P3dModifyScaleRandom.BlendTypeMultiply  // const
  public    static  PaintIn3D.P3dModifyScaleRandom.BlendTypeIncrement  // const
METHODS:
END_CLASS

CLASS: Contribution
TYPE:  class
TOKEN: 0x200006A
SIZE:  0x20
FIELDS:
  public            PaintIn3D.P3dColorCounter       Counter  // 0x10
  public            System.Int32                    Solid  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60002E9  System.Void .ctor()
END_CLASS

CLASS: Contribution
TYPE:  class
TOKEN: 0x200006C
SIZE:  0x28
FIELDS:
  public            PaintIn3D.P3dColor              Color  // 0x10
  public            System.Int32                    Count  // 0x18
  public            System.Single                   Ratio  // 0x1c
  public            System.Byte                     R  // 0x20
  public            System.Byte                     G  // 0x21
  public            System.Byte                     B  // 0x22
  public            System.Byte                     A  // 0x23
  public    static  System.Collections.Generic.Stack<PaintIn3D.P3dColorCounter.Contribution>Pool  // static @ 0x0
METHODS:
  RVA=0x041E1670  token=0x60002FA  System.Void .ctor()
  RVA=0x0A8F61A8  token=0x60002FB  System.Void .cctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200006F
SIZE:  0x10
FIELDS:
  public    static readonly PaintIn3D.P3dCommon.<>c         <>9  // static @ 0x0
METHODS:
  RVA=0x0A90C284  token=0x6000336  System.Void .cctor()
  RVA=0x041E1670  token=0x6000337  System.Void .ctor()
  RVA=0x0A90C194  token=0x6000338  System.Void <.cctor>b__5_0(UnityEngine.Camera camera)
  RVA=0x0A90C20C  token=0x6000339  System.Void <.cctor>b__5_1(UnityEngine.Rendering.ScriptableRenderContext context, UnityEngine.Camera camera)
END_CLASS

CLASS: Coord
TYPE:  sealed struct
TOKEN: 0x2000072
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dCoordCopier.Coord  First  // const
  public    static  PaintIn3D.P3dCoordCopier.Coord  Second  // const
  public    static  PaintIn3D.P3dCoordCopier.Coord  Third  // const
  public    static  PaintIn3D.P3dCoordCopier.Coord  Fourth  // const
  public    static  PaintIn3D.P3dCoordCopier.Coord  None  // const
METHODS:
END_CLASS

CLASS: TextureData
TYPE:  class
TOKEN: 0x2000076
SIZE:  0x50
FIELDS:
  public            System.String                   Name  // 0x10
  public            PaintIn3D.P3dBlendMode          BlendMode  // 0x18
METHODS:
  RVA=0x0A90C0FC  token=0x600036C  System.Void .ctor()
END_CLASS

CLASS: Entry
TYPE:  class
TOKEN: 0x2000077
SIZE:  0x20
FIELDS:
  public            System.String                   Path  // 0x10
  public            System.String                   Property  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600036D  System.Void .ctor()
END_CLASS

CLASS: Link
TYPE:  class
TOKEN: 0x200007E
SIZE:  0x38
FIELDS:
  public            System.Object                   Owner  // 0x10
  public            UnityEngine.Vector3             Position  // 0x18
  public            UnityEngine.Vector3             EndPosition  // 0x24
  public            System.Single                   Age  // 0x30
  public            System.Boolean                  Preview  // 0x34
METHODS:
  RVA=0x041E1670  token=0x600038D  System.Void .ctor()
END_CLASS

CLASS: MeshData
TYPE:  class
TOKEN: 0x2000082
SIZE:  0x28
FIELDS:
  private           UnityEngine.Vector3[]           positions  // 0x10
  private           System.Int32[]                  indices  // 0x18
  private           System.Int32                    total  // 0x20
METHODS:
  RVA=0x0A8F63E8  token=0x60003A9  System.Void Update(UnityEngine.Mesh mesh)
  RVA=0x0A8F6234  token=0x60003AA  System.Boolean GetTrianglePositions(UnityEngine.RaycastHit hit, UnityEngine.Vector3& positionA, UnityEngine.Vector3& positionB, UnityEngine.Vector3& positionC)
  RVA=0x041E1670  token=0x60003AB  System.Void .ctor()
END_CLASS

CLASS: UseMeshType
TYPE:  sealed struct
TOKEN: 0x2000084
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dModel.UseMeshType  AsIs  // const
  public    static  PaintIn3D.P3dModel.UseMeshType  AutoSeamFix  // const
METHODS:
END_CLASS

CLASS: ActivationType
TYPE:  sealed struct
TOKEN: 0x2000088
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dPaintable.ActivationTypeAwake  // const
  public    static  PaintIn3D.P3dPaintable.ActivationTypeOnEnable  // const
  public    static  PaintIn3D.P3dPaintable.ActivationTypeStart  // const
  public    static  PaintIn3D.P3dPaintable.ActivationTypeOnFirstUse  // const
METHODS:
END_CLASS

CLASS: UndoRedoType
TYPE:  sealed struct
TOKEN: 0x200008C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dPaintableTexture.UndoRedoTypeNone  // const
  public    static  PaintIn3D.P3dPaintableTexture.UndoRedoTypeFullTextureCopy  // const
  public    static  PaintIn3D.P3dPaintableTexture.UndoRedoTypeLocalCommandCopy  // const
METHODS:
END_CLASS

CLASS: SaveLoadType
TYPE:  sealed struct
TOKEN: 0x200008D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dPaintableTexture.SaveLoadTypeManual  // const
  public    static  PaintIn3D.P3dPaintableTexture.SaveLoadTypeAutomatic  // const
  public    static  PaintIn3D.P3dPaintableTexture.SaveLoadTypeSemiManual  // const
METHODS:
END_CLASS

CLASS: MipType
TYPE:  sealed struct
TOKEN: 0x200008E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dPaintableTexture.MipTypeAuto  // const
  public    static  PaintIn3D.P3dPaintableTexture.MipTypeOn  // const
  public    static  PaintIn3D.P3dPaintableTexture.MipTypeOff  // const
METHODS:
END_CLASS

CLASS: FilterType
TYPE:  sealed struct
TOKEN: 0x200008F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dPaintableTexture.FilterTypeAuto  // const
  public    static  PaintIn3D.P3dPaintableTexture.FilterTypePoint  // const
  public    static  PaintIn3D.P3dPaintableTexture.FilterTypeBilinear  // const
  public    static  PaintIn3D.P3dPaintableTexture.FilterTypeTrilinear  // const
METHODS:
END_CLASS

CLASS: AnisoType
TYPE:  sealed struct
TOKEN: 0x2000090
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dPaintableTexture.AnisoTypeAuto  // const
  public    static  PaintIn3D.P3dPaintableTexture.AnisoTypeOff  // const
  public    static  PaintIn3D.P3dPaintableTexture.AnisoTypeOne  // const
  public    static  PaintIn3D.P3dPaintableTexture.AnisoTypeFour  // const
  public    static  PaintIn3D.P3dPaintableTexture.AnisoTypeEight  // const
  public    static  PaintIn3D.P3dPaintableTexture.AnisoTypeSixteen  // const
METHODS:
END_CLASS

CLASS: WrapType
TYPE:  sealed struct
TOKEN: 0x2000091
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dPaintableTexture.WrapTypeAuto  // const
  public    static  PaintIn3D.P3dPaintableTexture.WrapTypeRepeat  // const
  public    static  PaintIn3D.P3dPaintableTexture.WrapTypeClamp  // const
  public    static  PaintIn3D.P3dPaintableTexture.WrapTypeMirror  // const
  public    static  PaintIn3D.P3dPaintableTexture.WrapTypeMirrorOnce  // const
METHODS:
END_CLASS

CLASS: ExistingType
TYPE:  sealed struct
TOKEN: 0x2000092
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dPaintableTexture.ExistingTypeIgnore  // const
  public    static  PaintIn3D.P3dPaintableTexture.ExistingTypeUse  // const
  public    static  PaintIn3D.P3dPaintableTexture.ExistingTypeUseAndKeep  // const
METHODS:
END_CLASS

CLASS: ConversionType
TYPE:  sealed struct
TOKEN: 0x2000093
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dPaintableTexture.ConversionTypeNone  // const
  public    static  PaintIn3D.P3dPaintableTexture.ConversionTypeNormal  // const
  public    static  PaintIn3D.P3dPaintableTexture.ConversionTypePremultiply  // const
METHODS:
END_CLASS

CLASS: PaintableTextureEvent
TYPE:  class
TOKEN: 0x2000094
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0A90C0C0  token=0x600048C  System.Void .ctor()
END_CLASS

CLASS: Link
TYPE:  class
TOKEN: 0x2000098
SIZE:  0x50
FIELDS:
  public            System.Object                   Owner  // 0x10
  public            UnityEngine.Vector3             Position  // 0x18
  public            System.Single                   Age  // 0x24
  public            System.Boolean                  Preview  // 0x28
  public            UnityEngine.Vector3             LastPosition  // 0x2c
  public            System.Single                   LastPressure  // 0x38
  public            System.Int32                    LastPriority  // 0x3c
  public            UnityEngine.Quaternion          LastRotation  // 0x40
METHODS:
  RVA=0x041E1670  token=0x60004C8  System.Void .ctor()
END_CLASS

CLASS: ColorEvent
TYPE:  class
TOKEN: 0x200009A
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0A8F616C  token=0x60004D2  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dButtonClearAll
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
  private           System.Boolean                  clearStates  // 0x18
PROPERTIES:
  ClearStates  get=0x04D86340  set=0x04D86350
METHODS:
  RVA=0x0A8E4350  token=0x6000003  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0A8E41C4  token=0x6000004  System.Void ClearAll()
  RVA=0x05394740  token=0x6000005  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dButtonRedoAll
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=0x0A8E4358  token=0x6000006  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0A8E4358  token=0x6000007  System.Void RedoAll()
  RVA=0x0A8E4360  token=0x6000008  System.Void Update()
  RVA=0x05393520  token=0x6000009  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dButtonUndoAll
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
METHODS:
  RVA=0x0A8E43FC  token=0x600000A  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0A8E43FC  token=0x600000B  System.Void UndoAll()
  RVA=0x0A8E4404  token=0x600000C  System.Void Update()
  RVA=0x05393520  token=0x600000D  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dChangeCounterEvent
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Collections.Generic.List<PaintIn3D.P3dChangeCounter>counters  // 0x18
  private           UnityEngine.Vector2             range  // 0x20
  private           System.Boolean                  inside  // 0x28
  private           UnityEngine.Events.UnityEvent   onInside  // 0x30
  private           UnityEngine.Events.UnityEvent   onOutside  // 0x38
PROPERTIES:
  Counters  get=0x0A8E4558
  Range  get=0x04DA5EA0  set=0x04D86500
  Inside  get=0x011F33C0  set=0x011F33D0
  OnInside  get=0x0A8E45DC
  OnOutside  get=0x0A8E464C
  Ratio  get=0x0A8E46BC
METHODS:
  RVA=0x0A8E4510  token=0x6000016  System.Void Update()
  RVA=0x0A8E44A0  token=0x6000017  System.Void UpdateInside(System.Single ratio)
  RVA=0x0A8E4534  token=0x6000018  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dChangeCounterFill
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Collections.Generic.List<PaintIn3D.P3dChangeCounter>counters  // 0x18
  private           System.Boolean                  inverse  // 0x20
  private           UnityEngine.UI.Image            cachedImage  // 0x28
PROPERTIES:
  Counters  get=0x0A8E480C
  Inverse  get=0x04D866B0  set=0x04D866D0
METHODS:
  RVA=0x0A8E4724  token=0x600001C  System.Void OnEnable()
  RVA=0x0A8E476C  token=0x600001D  System.Void Update()
  RVA=0x05393520  token=0x600001E  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dChangeCounterText
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Collections.Generic.List<PaintIn3D.P3dChangeCounter>counters  // 0x18
  private           System.Boolean                  inverse  // 0x20
  private           System.Int32                    decimalPlaces  // 0x24
  private           System.String                   format  // 0x28
  private           PaintIn3D.P3dChangeCounterText.StringEventonString  // 0x30
PROPERTIES:
  Counters  get=0x0A8E4AB8
  Inverse  get=0x04D866B0  set=0x04D866D0
  DecimalPlaces  get=0x011F0020  set=0x011F0030
  Format  get=0x04D86240  set=0x02FDB880
  OnString  get=0x0A8E4B3C
METHODS:
  RVA=0x0A8E4890  token=0x6000027  System.Void Update()
  RVA=0x0A8E4A5C  token=0x6000028  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dChannelCounterEvent
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Collections.Generic.List<PaintIn3D.P3dChannelCounter>counters  // 0x18
  private           PaintIn3D.P3dChannelCounterEvent.ChannelTypechannel  // 0x20
  private           UnityEngine.Vector2             range  // 0x24
  private           System.Boolean                  inside  // 0x2c
  private           UnityEngine.Events.UnityEvent   onInside  // 0x30
  private           UnityEngine.Events.UnityEvent   onOutside  // 0x38
PROPERTIES:
  Counters  get=0x0A8E4C64
  Channel  get=0x02B70F90  set=0x04D86330
  Range  get=0x04D88060  set=0x04DA5EC0
  Inside  get=0x04D869F0  set=0x04D86A00
  OnInside  get=0x0A8E4CE8
  OnOutside  get=0x0A8E4D58
  Ratio  get=0x0A8E4DC8
METHODS:
  RVA=0x0A8E4C1C  token=0x6000034  System.Void Update()
  RVA=0x0A8E4BAC  token=0x6000035  System.Void UpdateInside(System.Single ratio)
  RVA=0x0A8E4C40  token=0x6000036  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dChannelCounterFill
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Collections.Generic.List<PaintIn3D.P3dChannelCounter>counters  // 0x18
  private           PaintIn3D.P3dChannelCounterFill.ChannelTypechannel  // 0x20
  private           System.Boolean                  inverse  // 0x24
  private           UnityEngine.UI.Image            cachedImage  // 0x28
PROPERTIES:
  Counters  get=0x0A8E4FF0
  Channel  get=0x02B70F90  set=0x04D86330
  Inverse  get=0x04D86870  set=0x04D86880
METHODS:
  RVA=0x0A8E4E9C  token=0x600003C  System.Void OnEnable()
  RVA=0x0A8E4EE4  token=0x600003D  System.Void Update()
  RVA=0x05393520  token=0x600003E  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dChannelCounterText
TYPE:  class
TOKEN: 0x200000D
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Collections.Generic.List<PaintIn3D.P3dChannelCounter>counters  // 0x18
  private           PaintIn3D.P3dChannelCounterText.ChannelTypechannel  // 0x20
  private           System.Boolean                  inverse  // 0x24
  private           System.Int32                    decimalPlaces  // 0x28
  private           System.String                   format  // 0x30
  private           PaintIn3D.P3dChannelCounterText.StringEventonString  // 0x38
PROPERTIES:
  Counters  get=0x0A8E530C
  Channel  get=0x02B70F90  set=0x04D86330
  Inverse  get=0x04D86870  set=0x04D86880
  DecimalPlaces  get=0x011F2A90  set=0x011F2AA0
  Format  get=0x011F36E0  set=0x05396200
  OnString  get=0x0A8E5390
METHODS:
  RVA=0x0A8E5074  token=0x6000049  System.Void Update()
  RVA=0x0A8E52B0  token=0x600004A  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dColorCounterEvent
TYPE:  class
TOKEN: 0x2000010
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Collections.Generic.List<PaintIn3D.P3dColorCounter>counters  // 0x18
  private           PaintIn3D.P3dColor              color  // 0x20
  private           UnityEngine.Vector2             range  // 0x28
  private           System.Boolean                  inside  // 0x30
  private           UnityEngine.Events.UnityEvent   onInside  // 0x38
  private           UnityEngine.Events.UnityEvent   onOutside  // 0x40
PROPERTIES:
  Counters  get=0x0A8E54B8
  Color  get=0x04D862C0  set=0x05390F40
  Range  get=0x04D9E6E0  set=0x04D86520
  Inside  get=0x04D865F0  set=0x04D86620
  OnInside  get=0x0A8E553C
  OnOutside  get=0x0A8E55AC
  Ratio  get=0x0A8E561C
METHODS:
  RVA=0x0A8E5470  token=0x6000056  System.Void Update()
  RVA=0x0A8E5400  token=0x6000057  System.Void UpdateInside(System.Single ratio)
  RVA=0x0A8E5494  token=0x6000058  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dColorCounterFill
TYPE:  class
TOKEN: 0x2000011
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Collections.Generic.List<PaintIn3D.P3dColorCounter>counters  // 0x18
  private           PaintIn3D.P3dColor              color  // 0x20
  private           System.Boolean                  inverse  // 0x28
  private           UnityEngine.UI.Image            cachedImage  // 0x30
PROPERTIES:
  Counters  get=0x0A8E578C
  Color  get=0x04D862C0  set=0x05390F40
  Inverse  get=0x011F33C0  set=0x011F33D0
METHODS:
  RVA=0x0A8E5690  token=0x600005E  System.Void OnEnable()
  RVA=0x0A8E56D8  token=0x600005F  System.Void Update()
  RVA=0x05393520  token=0x6000060  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dColorCounterText
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Collections.Generic.List<PaintIn3D.P3dColorCounter>counters  // 0x18
  private           PaintIn3D.P3dColor              color  // 0x20
  private           System.Boolean                  inverse  // 0x28
  private           System.Int32                    decimalPlaces  // 0x2c
  private           System.String                   format  // 0x30
  private           PaintIn3D.P3dColorCounterText.StringEventonString  // 0x38
PROPERTIES:
  Counters  get=0x0A8E5A3C
  Color  get=0x04D862C0  set=0x05390F40
  Inverse  get=0x011F33C0  set=0x011F33D0
  DecimalPlaces  get=0x04D86710  set=0x04D86720
  Format  get=0x011F36E0  set=0x05396200
  OnString  get=0x0A8E5AC0
METHODS:
  RVA=0x0A8E5810  token=0x600006B  System.Void Update()
  RVA=0x0A8E59E0  token=0x600006C  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dPaintDebug
TYPE:  class
TOKEN: 0x2000014
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: PaintIn3D.IHitPoint PaintIn3D.IHit PaintIn3D.IHitLine PaintIn3D.IHitTriangle PaintIn3D.IHitQuad
FIELDS:
  private           UnityEngine.Color               color  // 0x18
  private           System.Single                   duration  // 0x28
  private           System.Single                   size  // 0x2c
PROPERTIES:
  Color  get=0x04D890C0  set=0x04D890D0
  Duration  get=0x04D86BF0  set=0x04D86C00
  Size  get=0x04D86AF0  set=0x04D86B00
METHODS:
  RVA=0x0A8F2094  token=0x6000074  System.Void HandleHitPoint(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
  RVA=0x0A8F1F74  token=0x6000075  System.Void HandleHitLine(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Quaternion rotation, System.Boolean clip)
  RVA=0x0A8F2474  token=0x6000076  System.Void HandleHitTriangle(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 positionA, UnityEngine.Vector3 positionB, UnityEngine.Vector3 positionC, UnityEngine.Quaternion rotation)
  RVA=0x0A8F223C  token=0x6000077  System.Void HandleHitQuad(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Vector3 position2, UnityEngine.Vector3 endPosition2, UnityEngine.Quaternion rotation, System.Boolean clip)
  RVA=0x0A8F1F30  token=0x6000078  UnityEngine.Color GetColor(System.Boolean preview, System.Single pressure, UnityEngine.Color color)
  RVA=0x0A8F1A68  token=0x6000079  System.Void DrawArrow(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Color tint)
  RVA=0x0A8F2698  token=0x600007A  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dCommandDecal
TYPE:  class
TOKEN: 0x2000015
SIZE:  0x258
EXTENDS: PaintIn3D.P3dCommand
FIELDS:
  public            PaintIn3D.P3dBlendMode          Blend  // 0x70
  public            System.Boolean                  In3D  // 0xa8
  public            UnityEngine.Vector3             Position  // 0xac
  public            UnityEngine.Vector3             EndPosition  // 0xb8
  public            UnityEngine.Vector3             Position2  // 0xc4
  public            UnityEngine.Vector3             EndPosition2  // 0xd0
  public            System.Int32                    Extrusions  // 0xdc
  public            System.Boolean                  Clip  // 0xe0
  public            UnityEngine.Matrix4x4           Matrix  // 0xe4
  public            UnityEngine.Vector3             Direction  // 0x124
  public            UnityEngine.Color               Color  // 0x130
  public            System.Single                   Opacity  // 0x140
  public            System.Single                   Hardness  // 0x144
  public            System.Single                   Wrapping  // 0x148
  public            PaintIn3D.P3dHashedTexture      Texture  // 0x150
  public            PaintIn3D.P3dHashedTexture      Shape  // 0x160
  public            UnityEngine.Vector4             ShapeChannel  // 0x170
  public            UnityEngine.Vector2             NormalFront  // 0x180
  public            UnityEngine.Vector2             NormalBack  // 0x188
  public            PaintIn3D.P3dHashedTexture      TileTexture  // 0x190
  public            UnityEngine.Matrix4x4           TileMatrix  // 0x1a0
  public            System.Single                   TileOpacity  // 0x1e0
  public            System.Single                   TileTransition  // 0x1e4
  public            UnityEngine.Matrix4x4           MaskMatrix  // 0x1e8
  public            PaintIn3D.P3dHashedTexture      MaskShape  // 0x228
  public            UnityEngine.Vector4             MaskChannel  // 0x238
  public            UnityEngine.Vector3             MaskStretch  // 0x248
  public    static  PaintIn3D.P3dCommandDecal       Instance  // static @ 0x0
  private   static  System.Collections.Generic.Stack<PaintIn3D.P3dCommandDecal>pool  // static @ 0x8
  private   static  UnityEngine.Material            cachedSpotMaterial  // static @ 0x10
  private   static  UnityEngine.Material            cachedLineMaterial  // static @ 0x18
  private   static  UnityEngine.Material            cachedQuadMaterial  // static @ 0x20
  private   static  UnityEngine.Material            cachedLineClipMaterial  // static @ 0x28
  private   static  UnityEngine.Material            cachedQuadClipMaterial  // static @ 0x30
  private   static  System.Int32                    cachedSpotMaterialHash  // static @ 0x38
  private   static  System.Int32                    cachedLineMaterialHash  // static @ 0x3c
  private   static  System.Int32                    cachedQuadMaterialHash  // static @ 0x40
  private   static  System.Int32                    cachedLineClipMaterialHash  // static @ 0x44
  private   static  System.Int32                    cachedQuadClipMaterialHash  // static @ 0x48
  private   static  System.Int32                    _In3D  // static @ 0x4c
  private   static  System.Int32                    _Position  // static @ 0x50
  private   static  System.Int32                    _EndPosition  // static @ 0x54
  private   static  System.Int32                    _Position2  // static @ 0x58
  private   static  System.Int32                    _EndPosition2  // static @ 0x5c
  private   static  System.Int32                    _Matrix  // static @ 0x60
  private   static  System.Int32                    _Direction  // static @ 0x64
  private   static  System.Int32                    _Color  // static @ 0x68
  private   static  System.Int32                    _Opacity  // static @ 0x6c
  private   static  System.Int32                    _Hardness  // static @ 0x70
  private   static  System.Int32                    _Wrapping  // static @ 0x74
  private   static  System.Int32                    _Texture  // static @ 0x78
  private   static  System.Int32                    _Shape  // static @ 0x7c
  private   static  System.Int32                    _ShapeChannel  // static @ 0x80
  private   static  System.Int32                    _NormalFront  // static @ 0x84
  private   static  System.Int32                    _NormalBack  // static @ 0x88
  private   static  System.Int32                    _TileTexture  // static @ 0x8c
  private   static  System.Int32                    _TileMatrix  // static @ 0x90
  private   static  System.Int32                    _TileOpacity  // static @ 0x94
  private   static  System.Int32                    _TileTransition  // static @ 0x98
  private   static  System.Int32                    _MaskMatrix  // static @ 0x9c
  private   static  System.Int32                    _MaskTexture  // static @ 0xa0
  private   static  System.Int32                    _MaskChannel  // static @ 0xa4
  private   static  System.Int32                    _MaskStretch  // static @ 0xa8
PROPERTIES:
  RequireMesh  get=0x02FFF600
METHODS:
  RVA=0x0A8E7098  token=0x600007C  System.Void .cctor()
  RVA=0x0A8E5C50  token=0x600007D  System.Void Apply(UnityEngine.Material material)
  RVA=0x0A8E6454  token=0x600007E  System.Void Pool()
  RVA=0x0A8E6E68  token=0x600007F  System.Void Transform(UnityEngine.Matrix4x4 posMatrix, UnityEngine.Matrix4x4 rotMatrix)
  RVA=0x0A8E6AE4  token=0x6000080  PaintIn3D.P3dCommand SpawnCopy()
  RVA=0x0A8E5BF4  token=0x6000081  System.Void Apply(PaintIn3D.P3dPaintableTexture paintableTexture)
  RVA=0x0A8E695C  token=0x6000082  System.Void SetShape(UnityEngine.Quaternion rotation, UnityEngine.Vector3 size, System.Single angle)
  RVA=0x04DA5F50  token=0x6000083  System.Void SetLocation(UnityEngine.Vector3 position, System.Boolean in3D)
  RVA=0x04DA5F80  token=0x6000084  System.Void SetLocation(UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, System.Boolean in3D, System.Boolean clip)
  RVA=0x04DA5ED0  token=0x6000085  System.Void SetLocation(UnityEngine.Vector3 positionA, UnityEngine.Vector3 positionB, UnityEngine.Vector3 positionC, System.Boolean in3D)
  RVA=0x04DA5FD0  token=0x6000086  System.Void SetLocation(UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Vector3 position2, UnityEngine.Vector3 endPosition2, System.Boolean in3D, System.Boolean clip)
  RVA=0x0A8E63E8  token=0x6000087  System.Void ClearMask()
  RVA=0x0A8E64C4  token=0x6000088  System.Void SetMask(UnityEngine.Matrix4x4 matrix, UnityEngine.Texture shape, PaintIn3D.P3dChannel channel, UnityEngine.Vector3 stretch)
  RVA=0x0A8E5B30  token=0x6000089  System.Void ApplyAspect(UnityEngine.Texture texture)
  RVA=0x0A8E65C0  token=0x600008A  System.Void SetMaterial(PaintIn3D.P3dBlendMode blendMode, UnityEngine.Texture texture, UnityEngine.Texture shape, PaintIn3D.P3dChannel shapeChannel, System.Single hardness, System.Single wrapping, System.Single normalBack, System.Single normalFront, System.Single normalFade, UnityEngine.Color color, System.Single opacity, UnityEngine.Texture tileTexture, UnityEngine.Matrix4x4 tileMatrix, System.Single tileOpacity, System.Single tileTransition)
  RVA=0x0A8E7700  token=0x600008B  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dCommandFill
TYPE:  class
TOKEN: 0x2000016
SIZE:  0xD0
EXTENDS: PaintIn3D.P3dCommand
FIELDS:
  public            PaintIn3D.P3dBlendMode          Blend  // 0x70
  public            PaintIn3D.P3dHashedTexture      Texture  // 0xa8
  public            UnityEngine.Color               Color  // 0xb8
  public            System.Single                   Opacity  // 0xc8
  public            System.Single                   Minimum  // 0xcc
  public    static  PaintIn3D.P3dCommandFill        Instance  // static @ 0x0
  private   static  System.Collections.Generic.Stack<PaintIn3D.P3dCommandFill>pool  // static @ 0x8
  private   static  UnityEngine.Material            cachedMaterial  // static @ 0x10
  private   static  System.Int32                    cachedMaterialHash  // static @ 0x18
  private   static  System.Int32                    _Buffer  // static @ 0x1c
  private   static  System.Int32                    _BufferSize  // static @ 0x20
  private   static  System.Int32                    _Texture  // static @ 0x24
  private   static  System.Int32                    _Color  // static @ 0x28
  private   static  System.Int32                    _Opacity  // static @ 0x2c
  private   static  System.Int32                    _Minimum  // static @ 0x30
PROPERTIES:
  RequireMesh  get=0x012081B0
METHODS:
  RVA=0x0A8E7F70  token=0x600008D  System.Void .cctor()
  RVA=0x0A8E7B34  token=0x600008E  UnityEngine.RenderTexture Blit(UnityEngine.RenderTexture main, PaintIn3D.P3dBlendMode blendMode, UnityEngine.Texture texture, UnityEngine.Color color, System.Single opacity, System.Single minimum)
  RVA=0x0A8E78E8  token=0x600008F  System.Void Blit(UnityEngine.RenderTexture& main, UnityEngine.RenderTexture& swap, PaintIn3D.P3dBlendMode blendMode, UnityEngine.Texture texture, UnityEngine.Color color, System.Single opacity, System.Single minimum)
  RVA=0x0A8E7740  token=0x6000090  System.Void Apply(UnityEngine.Material material)
  RVA=0x0A8E7C9C  token=0x6000091  System.Void Pool()
  RVA=0x041E1670  token=0x6000092  System.Void Transform(UnityEngine.Matrix4x4 posMatrix, UnityEngine.Matrix4x4 rotMatrix)
  RVA=0x0A8E7E6C  token=0x6000093  PaintIn3D.P3dCommand SpawnCopy()
  RVA=0x0A8E5BF4  token=0x6000094  System.Void Apply(PaintIn3D.P3dPaintableTexture paintableTexture)
  RVA=0x0A8E7D0C  token=0x6000095  UnityEngine.Material SetMaterial(PaintIn3D.P3dBlendMode blendMode, UnityEngine.Texture texture, UnityEngine.Color color, System.Single opacity, System.Single minimum)
  RVA=0x0A8E81A8  token=0x6000096  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dCommandReplace
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x90
EXTENDS: PaintIn3D.P3dCommand
FIELDS:
  public            PaintIn3D.P3dHashedTexture      Texture  // 0x70
  public            UnityEngine.Color               Color  // 0x80
  public    static  PaintIn3D.P3dCommandReplace     Instance  // static @ 0x0
  private   static  System.Collections.Generic.Stack<PaintIn3D.P3dCommandReplace>pool  // static @ 0x8
  private   static  UnityEngine.Material            cachedMaterial  // static @ 0x10
  private   static  System.Int32                    cachedMaterialHash  // static @ 0x18
  private   static  System.Int32                    _Texture  // static @ 0x1c
  private   static  System.Int32                    _Color  // static @ 0x20
PROPERTIES:
  RequireMesh  get=0x012081B0
METHODS:
  RVA=0x0A8E9160  token=0x6000098  System.Void .cctor()
  RVA=0x0A8E8E9C  token=0x6000099  System.Void Blit(UnityEngine.RenderTexture renderTexture, UnityEngine.Texture texture, UnityEngine.Color tint)
  RVA=0x0A8E8DC8  token=0x600009A  System.Void BlitFast(UnityEngine.RenderTexture renderTexture, UnityEngine.Texture texture, UnityEngine.Color tint)
  RVA=0x0A8E8CBC  token=0x600009B  System.Void Apply(UnityEngine.Material material)
  RVA=0x0A8E8F8C  token=0x600009C  System.Void Pool()
  RVA=0x041E1670  token=0x600009D  System.Void Transform(UnityEngine.Matrix4x4 posMatrix, UnityEngine.Matrix4x4 rotMatrix)
  RVA=0x0A8E90C8  token=0x600009E  PaintIn3D.P3dCommand SpawnCopy()
  RVA=0x0A8E8FFC  token=0x600009F  UnityEngine.Material SetMaterial(UnityEngine.Texture texture, UnityEngine.Color color)
  RVA=0x0A8E92E8  token=0x60000A0  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dCommandReplaceChannels
TYPE:  class
TOKEN: 0x2000018
SIZE:  0xF0
EXTENDS: PaintIn3D.P3dCommand
FIELDS:
  public            PaintIn3D.P3dHashedTexture      TextureR  // 0x70
  public            PaintIn3D.P3dHashedTexture      TextureG  // 0x80
  public            PaintIn3D.P3dHashedTexture      TextureB  // 0x90
  public            PaintIn3D.P3dHashedTexture      TextureA  // 0xa0
  public            UnityEngine.Vector4             ChannelR  // 0xb0
  public            UnityEngine.Vector4             ChannelG  // 0xc0
  public            UnityEngine.Vector4             ChannelB  // 0xd0
  public            UnityEngine.Vector4             ChannelA  // 0xe0
  public    static  PaintIn3D.P3dCommandReplaceChannelsInstance  // static @ 0x0
  private   static  System.Collections.Generic.Stack<PaintIn3D.P3dCommandReplaceChannels>pool  // static @ 0x8
  private   static  UnityEngine.Material            cachedMaterial  // static @ 0x10
  private   static  System.Int32                    cachedMaterialHash  // static @ 0x18
  private   static  System.Int32                    _TextureR  // static @ 0x1c
  private   static  System.Int32                    _TextureG  // static @ 0x20
  private   static  System.Int32                    _TextureB  // static @ 0x24
  private   static  System.Int32                    _TextureA  // static @ 0x28
  private   static  System.Int32                    _ChannelR  // static @ 0x2c
  private   static  System.Int32                    _ChannelG  // static @ 0x30
  private   static  System.Int32                    _ChannelB  // static @ 0x34
  private   static  System.Int32                    _ChannelA  // static @ 0x38
PROPERTIES:
  RequireMesh  get=0x012081B0
METHODS:
  RVA=0x0A8E89EC  token=0x60000A2  System.Void .cctor()
  RVA=0x0A8E854C  token=0x60000A3  System.Void Blit(UnityEngine.RenderTexture renderTexture, UnityEngine.Texture textureR, UnityEngine.Texture textureG, UnityEngine.Texture textureB, UnityEngine.Texture textureA, UnityEngine.Vector4 channelR, UnityEngine.Vector4 channelG, UnityEngine.Vector4 channelB, UnityEngine.Vector4 channelA, UnityEngine.Vector4 channels)
  RVA=0x0A8E83E8  token=0x60000A4  System.Void BlitFast(UnityEngine.RenderTexture renderTexture, UnityEngine.Texture textureR, UnityEngine.Texture textureG, UnityEngine.Texture textureB, UnityEngine.Texture textureA, UnityEngine.Vector4 channelR, UnityEngine.Vector4 channelG, UnityEngine.Vector4 channelB, UnityEngine.Vector4 channelA)
  RVA=0x0A8E81E8  token=0x60000A5  System.Void Apply(UnityEngine.Material material)
  RVA=0x0A8E86CC  token=0x60000A6  System.Void Pool()
  RVA=0x041E1670  token=0x60000A7  System.Void Transform(UnityEngine.Matrix4x4 posMatrix, UnityEngine.Matrix4x4 rotMatrix)
  RVA=0x0A8E88CC  token=0x60000A8  PaintIn3D.P3dCommand SpawnCopy()
  RVA=0x0A8E873C  token=0x60000A9  UnityEngine.Material SetMaterial(UnityEngine.Texture textureR, UnityEngine.Texture textureG, UnityEngine.Texture textureB, UnityEngine.Texture textureA, UnityEngine.Vector4 channelR, UnityEngine.Vector4 channelG, UnityEngine.Vector4 channelB, UnityEngine.Vector4 channelA)
  RVA=0x0A8E8C7C  token=0x60000AA  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dCommandSphere
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x208
EXTENDS: PaintIn3D.P3dCommand
FIELDS:
  public            PaintIn3D.P3dBlendMode          Blend  // 0x70
  public            System.Boolean                  In3D  // 0xa8
  public            UnityEngine.Vector3             Position  // 0xac
  public            UnityEngine.Vector3             EndPosition  // 0xb8
  public            UnityEngine.Vector3             Position2  // 0xc4
  public            UnityEngine.Vector3             EndPosition2  // 0xd0
  public            System.Int32                    Extrusions  // 0xdc
  public            System.Boolean                  Clip  // 0xe0
  public            UnityEngine.Matrix4x4           Matrix  // 0xe4
  public            UnityEngine.Color               Color  // 0x124
  public            System.Single                   Opacity  // 0x134
  public            System.Single                   Hardness  // 0x138
  public            PaintIn3D.P3dHashedTexture      TileTexture  // 0x140
  public            UnityEngine.Matrix4x4           TileMatrix  // 0x150
  public            System.Single                   TileOpacity  // 0x190
  public            System.Single                   TileTransition  // 0x194
  public            UnityEngine.Matrix4x4           MaskMatrix  // 0x198
  public            PaintIn3D.P3dHashedTexture      MaskShape  // 0x1d8
  public            UnityEngine.Vector4             MaskChannel  // 0x1e8
  public            UnityEngine.Vector3             MaskStretch  // 0x1f8
  public    static  PaintIn3D.P3dCommandSphere      Instance  // static @ 0x0
  private   static  System.Collections.Generic.Stack<PaintIn3D.P3dCommandSphere>pool  // static @ 0x8
  private   static  UnityEngine.Material            cachedSpotMaterial  // static @ 0x10
  private   static  UnityEngine.Material            cachedLineMaterial  // static @ 0x18
  private   static  UnityEngine.Material            cachedQuadMaterial  // static @ 0x20
  private   static  UnityEngine.Material            cachedLineClipMaterial  // static @ 0x28
  private   static  UnityEngine.Material            cachedQuadClipMaterial  // static @ 0x30
  private   static  System.Int32                    cachedSpotMaterialHash  // static @ 0x38
  private   static  System.Int32                    cachedLineMaterialHash  // static @ 0x3c
  private   static  System.Int32                    cachedQuadMaterialHash  // static @ 0x40
  private   static  System.Int32                    cachedLineClipMaterialHash  // static @ 0x44
  private   static  System.Int32                    cachedQuadClipMaterialHash  // static @ 0x48
  private   static  System.Int32                    _In3D  // static @ 0x4c
  private   static  System.Int32                    _Position  // static @ 0x50
  private   static  System.Int32                    _EndPosition  // static @ 0x54
  private   static  System.Int32                    _Position2  // static @ 0x58
  private   static  System.Int32                    _EndPosition2  // static @ 0x5c
  private   static  System.Int32                    _Matrix  // static @ 0x60
  private   static  System.Int32                    _Color  // static @ 0x64
  private   static  System.Int32                    _Opacity  // static @ 0x68
  private   static  System.Int32                    _Hardness  // static @ 0x6c
  private   static  System.Int32                    _TileTexture  // static @ 0x70
  private   static  System.Int32                    _TileMatrix  // static @ 0x74
  private   static  System.Int32                    _TileOpacity  // static @ 0x78
  private   static  System.Int32                    _TileTransition  // static @ 0x7c
  private   static  System.Int32                    _MaskMatrix  // static @ 0x80
  private   static  System.Int32                    _MaskTexture  // static @ 0x84
  private   static  System.Int32                    _MaskChannel  // static @ 0x88
  private   static  System.Int32                    _MaskStretch  // static @ 0x8c
PROPERTIES:
  RequireMesh  get=0x02FFF600
METHODS:
  RVA=0x0A8EA46C  token=0x60000AC  System.Void .cctor()
  RVA=0x0A8E93EC  token=0x60000AD  System.Void Apply(UnityEngine.Material material)
  RVA=0x0A8E9A24  token=0x60000AE  System.Void Pool()
  RVA=0x0A8EA2A4  token=0x60000AF  System.Void Transform(UnityEngine.Matrix4x4 posMatrix, UnityEngine.Matrix4x4 rotMatrix)
  RVA=0x0A8E9FDC  token=0x60000B0  PaintIn3D.P3dCommand SpawnCopy()
  RVA=0x0A8E5BF4  token=0x60000B1  System.Void Apply(PaintIn3D.P3dPaintableTexture paintableTexture)
  RVA=0x04DA5F50  token=0x60000B2  System.Void SetLocation(UnityEngine.Vector3 position, System.Boolean in3D)
  RVA=0x04DA5F80  token=0x60000B3  System.Void SetLocation(UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, System.Boolean in3D, System.Boolean clip)
  RVA=0x04DA5ED0  token=0x60000B4  System.Void SetLocation(UnityEngine.Vector3 positionA, UnityEngine.Vector3 positionB, UnityEngine.Vector3 positionC, System.Boolean in3D)
  RVA=0x04DA5FD0  token=0x60000B5  System.Void SetLocation(UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Vector3 position2, UnityEngine.Vector3 endPosition2, System.Boolean in3D, System.Boolean clip)
  RVA=0x0A8E99B8  token=0x60000B6  System.Void ClearMask()
  RVA=0x0A8E9A94  token=0x60000B7  System.Void SetMask(UnityEngine.Matrix4x4 matrix, UnityEngine.Texture shape, PaintIn3D.P3dChannel channel, UnityEngine.Vector3 stretch)
  RVA=0x0A8E9328  token=0x60000B8  System.Void ApplyAspect(UnityEngine.Texture texture)
  RVA=0x0A8E9F10  token=0x60000B9  System.Void SetShape(System.Single radius)
  RVA=0x0A8E9DD4  token=0x60000BA  System.Void SetShape(UnityEngine.Quaternion rotation, UnityEngine.Vector3 size, System.Single angle)
  RVA=0x0A8E9B90  token=0x60000BB  System.Void SetMaterial(PaintIn3D.P3dBlendMode blendMode, System.Single hardness, UnityEngine.Color color, System.Single opacity, UnityEngine.Texture tileTexture, UnityEngine.Matrix4x4 tileMatrix, System.Single tileOpacity, System.Single tileTransition)
  RVA=0x0A8EA994  token=0x60000BC  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dHitBetween
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x88
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           PaintIn3D.P3dHitBetween.PhaseTypepaintIn  // 0x18
  private           System.Single                   interval  // 0x1c
  private           UnityEngine.Transform           pointA  // 0x20
  private           UnityEngine.Transform           pointB  // 0x28
  private           System.Single                   fraction  // 0x30
  private           UnityEngine.LayerMask           layers  // 0x34
  private           PaintIn3D.P3dHitBetween.OrientationTypeorientation  // 0x38
  private           UnityEngine.Camera              _camera  // 0x40
  private           UnityEngine.Transform           customTransform  // 0x48
  private           PaintIn3D.P3dHitBetween.NormalTypenormal  // 0x50
  private           System.Single                   offset  // 0x54
  private           System.Boolean                  preview  // 0x58
  private           System.Int32                    priority  // 0x5c
  private           System.Single                   pressure  // 0x60
  private           PaintIn3D.P3dHitBetween.EmitTypeemit  // 0x64
  private           UnityEngine.Transform           point  // 0x68
  private           UnityEngine.LineRenderer        line  // 0x70
  private           PaintIn3D.P3dPointConnector     connector  // 0x78
  private           System.Single                   current  // 0x80
PROPERTIES:
  PaintIn  get=0x011EF5B0  set=0x011EF9B0
  Interval  get=0x04D868F0  set=0x04D86900
  PointA  get=0x04D862C0  set=0x05390F40
  PointB  get=0x04D86240  set=0x02FDB880
  Fraction  get=0x04D86E20
  Layers  get=0x04D86730  set=0x04D86760
  Orientation  get=0x04D863B0  set=0x04D86430
  Camera  get=0x04D85A60  set=0x042B4AE0
  CustomTransform  get=0x04D86200  set=0x035A41A0
  Normal  get=0x04D86550  set=0x04D865A0
  Offset  get=0x04D86E00  set=0x04D86E10
  Preview  get=0x04D86F70  set=0x04D86F80
  Priority  get=0x04D86560  set=0x04D865C0
  Pressure  get=0x04D88480  set=0x04D884A0
  Draw  get=0x04D88110  set=0x04D88120
  Point  get=0x04D86230  set=0x0485F170
  Line  get=0x04D862B0  set=0x063AD760
  Connector  get=0x0A8EB67C
METHODS:
  RVA=0x0A8EAC88  token=0x60000DF  System.Void ManuallyHitNow()
  RVA=0x0A8EAC48  token=0x60000E0  System.Void ClearHitCache()
  RVA=0x0A8EAD7C  token=0x60000E1  System.Void ResetConnections()
  RVA=0x0A8EAD58  token=0x60000E2  System.Void OnEnable()
  RVA=0x0A8EAC94  token=0x60000E3  System.Void OnDisable()
  RVA=0x0A8EB5F8  token=0x60000E4  System.Void Update()
  RVA=0x0A8EAC80  token=0x60000E5  System.Void LateUpdate()
  RVA=0x0A8EAC6C  token=0x60000E6  System.Void FixedUpdate()
  RVA=0x0A8EAD9C  token=0x60000E7  System.Void SubmitHit(System.Boolean preview)
  RVA=0x0A8EA9D4  token=0x60000E8  System.Void CalcHitData(UnityEngine.Vector3 hitPoint, UnityEngine.Vector3 hitNormal, UnityEngine.Ray ray, UnityEngine.Vector3& finalPosition, UnityEngine.Quaternion& finalRotation)
  RVA=0x0A8EB40C  token=0x60000E9  System.Void UpdatePointAndLine()
  RVA=0x0A8EB39C  token=0x60000EA  System.Void UpdateHit()
  RVA=0x0A8EB648  token=0x60000EB  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dHitCache
TYPE:  class
TOKEN: 0x200001F
SIZE:  0x40
FIELDS:
  private           System.Boolean                  cached  // 0x10
  private           System.Collections.Generic.List<PaintIn3D.IHitPoint>hitPoints  // 0x18
  private           System.Collections.Generic.List<PaintIn3D.IHitLine>hitLines  // 0x20
  private           System.Collections.Generic.List<PaintIn3D.IHitTriangle>hitTriangles  // 0x28
  private           System.Collections.Generic.List<PaintIn3D.IHitQuad>hitQuads  // 0x30
  private           System.Collections.Generic.List<PaintIn3D.IHitCoord>hitCoords  // 0x38
  private   static  System.Collections.Generic.List<PaintIn3D.IHit>hits  // static @ 0x0
PROPERTIES:
  Cached  get=0x015EFCE0
METHODS:
  RVA=0x0A8EBD70  token=0x60000ED  System.Void InvokePoint(UnityEngine.GameObject gameObject, System.Boolean preview, System.Int32 priority, System.Single pressure, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
  RVA=0x0A8EBC10  token=0x60000EE  System.Void InvokeLine(UnityEngine.GameObject gameObject, System.Boolean preview, System.Int32 priority, System.Single pressure, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Quaternion rotation, System.Boolean clip)
  RVA=0x0A8EC1A8  token=0x60000EF  System.Void InvokeTriangle(UnityEngine.GameObject gameObject, System.Boolean preview, System.Int32 priority, System.Single pressure, UnityEngine.RaycastHit hit, UnityEngine.Quaternion rotation)
  RVA=0x0A8EC020  token=0x60000F0  System.Void InvokeTriangle(UnityEngine.GameObject gameObject, System.Boolean preview, System.Int32 priority, System.Single pressure, UnityEngine.Vector3 positionA, UnityEngine.Vector3 positionB, UnityEngine.Vector3 positionC, UnityEngine.Quaternion rotation)
  RVA=0x0A8EBE94  token=0x60000F1  System.Void InvokeQuad(UnityEngine.GameObject gameObject, System.Boolean preview, System.Int32 priority, System.Single pressure, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Vector3 position2, UnityEngine.Vector3 endPosition2, UnityEngine.Quaternion rotation, System.Boolean clip)
  RVA=0x0A8EBAE4  token=0x60000F2  System.Void InvokeCoord(UnityEngine.GameObject gameObject, System.Boolean preview, System.Int32 priority, System.Single pressure, PaintIn3D.P3dHit hit, UnityEngine.Quaternion rotation)
  RVA=0x0A8EBA14  token=0x60000F3  System.Void Clear()
  RVA=0x0A8EB6EC  token=0x60000F4  System.Void Cache(UnityEngine.GameObject gameObject)
  RVA=0x0A8EC384  token=0x60000F5  System.Void .ctor()
  RVA=0x0A8EC2F8  token=0x60000F6  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dHitCollisions
TYPE:  class
TOKEN: 0x2000020
SIZE:  0x78
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           PaintIn3D.P3dHitCollisions.EmitTypeemit  // 0x18
  private           System.Single                   raycastDistance  // 0x1c
  private           UnityEngine.LayerMask           layers  // 0x20
  private           System.Boolean                  onlyUseFirstContact  // 0x24
  private           System.Single                   delay  // 0x28
  private           PaintIn3D.P3dHitCollisions.OrientationTypeorientation  // 0x2c
  private           UnityEngine.Camera              _camera  // 0x30
  private           System.Boolean                  preview  // 0x38
  private           System.Single                   threshold  // 0x3c
  private           PaintIn3D.P3dHitCollisions.PressureTypepressureMode  // 0x40
  private           System.Single                   pressureMin  // 0x44
  private           System.Single                   pressureMax  // 0x48
  private           System.Single                   pressureConstant  // 0x4c
  private           System.Single                   pressureMultiplier  // 0x50
  private           System.Single                   offset  // 0x54
  private           System.Int32                    priority  // 0x58
  private           UnityEngine.GameObject          root  // 0x60
  private           System.Single                   cooldown  // 0x68
  private           PaintIn3D.P3dHitCache           hitCache  // 0x70
PROPERTIES:
  Emit  get=0x011EF5B0  set=0x011EF9B0
  RaycastDistance  get=0x04D868F0  set=0x04D86900
  Layers  get=0x02B70F90  set=0x04D86330
  OnlyUseFirstContact  get=0x04D86870  set=0x04D86880
  Delay  get=0x04D86BF0  set=0x04D86C00
  Orientation  get=0x04D86710  set=0x04D86720
  Camera  get=0x011F36E0  set=0x05396200
  Preview  get=0x011F2A70  set=0x011F2A80
  Threshold  get=0x04D863E0  set=0x04D86460
  PressureMode  get=0x04D865E0  set=0x04D86610
  PressureMin  get=0x04D86400  set=0x04D86480
  PressureMax  get=0x04D8D350  set=0x04D8D380
  PressureConstant  get=0x04D87860  set=0x04D91480
  PressureMultiplier  get=0x04D885A0  set=0x04D88FF0
  Offset  get=0x04D86E00  set=0x04D86E10
  Priority  get=0x04D86570  set=0x04D865D0
  Root  get=0x04D86280  set=0x0A8ECCEC
  HitCache  get=0x04D862B0
METHODS:
  RVA=0x0A8ECAA4  token=0x600011A  System.Void ClearHitCache()
  RVA=0x0A8ECAC4  token=0x600011B  System.Void OnCollisionEnter(UnityEngine.Collision collision)
  RVA=0x0A8ECAC4  token=0x600011C  System.Void OnCollisionStay(UnityEngine.Collision collision)
  RVA=0x0A8ECC18  token=0x600011D  System.Void Update()
  RVA=0x0A8ECACC  token=0x600011E  System.Boolean TryGetRaycastHit(UnityEngine.ContactPoint contact, UnityEngine.RaycastHit& hit)
  RVA=0x0A8EC528  token=0x600011F  System.Void CheckCollision(UnityEngine.Collision collision)
  RVA=0x0A8ECC48  token=0x6000120  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dHitNearby
TYPE:  class
TOKEN: 0x2000024
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           PaintIn3D.P3dHitNearby.PhaseTypepaintIn  // 0x18
  private           System.Single                   interval  // 0x1c
  private           System.Boolean                  preview  // 0x20
  private           System.Int32                    priority  // 0x24
  private           System.Single                   pressure  // 0x28
  private           PaintIn3D.P3dPointConnector     connector  // 0x30
  private           System.Single                   current  // 0x38
  private           UnityEngine.Vector3             lastPosition  // 0x3c
PROPERTIES:
  PaintIn  get=0x011EF5B0  set=0x011EF9B0
  Interval  get=0x04D868F0  set=0x04D86900
  Preview  get=0x04D866B0  set=0x04D866D0
  Priority  get=0x011F0020  set=0x011F0030
  Pressure  get=0x04D86BF0  set=0x04D86C00
  Connector  get=0x0A8ECF94
METHODS:
  RVA=0x0A8ECD54  token=0x600012C  System.Void ManuallyHitNow()
  RVA=0x0A8ECD1C  token=0x600012D  System.Void ClearHitCache()
  RVA=0x0A8ECD84  token=0x600012E  System.Void ResetConnections()
  RVA=0x0A8ECD60  token=0x600012F  System.Void OnEnable()
  RVA=0x0A8ECF20  token=0x6000130  System.Void Update()
  RVA=0x0A8ECD40  token=0x6000131  System.Void FixedUpdate()
  RVA=0x0A8ECDA4  token=0x6000132  System.Void SubmitHit(System.Boolean preview)
  RVA=0x0A8ECEB0  token=0x6000133  System.Void UpdateHit()
  RVA=0x0A8ECF70  token=0x6000134  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dHitParticles
TYPE:  class
TOKEN: 0x2000026
SIZE:  0x80
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           PaintIn3D.P3dHitParticles.EmitTypeemit  // 0x18
  private           System.Single                   raycastDistance  // 0x1c
  private           UnityEngine.LayerMask           layers  // 0x20
  private           PaintIn3D.P3dHitParticles.OrientationTypeorientation  // 0x24
  private           UnityEngine.Camera              _camera  // 0x28
  private           PaintIn3D.P3dHitParticles.NormalTypenormal  // 0x30
  private           System.Single                   offset  // 0x34
  private           System.Int32                    skip  // 0x38
  private           System.Boolean                  preview  // 0x3c
  private           System.Int32                    priority  // 0x40
  private           PaintIn3D.P3dHitParticles.PressureTypepressureMode  // 0x44
  private           System.Single                   pressureMin  // 0x48
  private           System.Single                   pressureMax  // 0x4c
  private           System.Single                   pressureConstant  // 0x50
  private           System.Single                   pressureMultiplier  // 0x54
  private           UnityEngine.GameObject          root  // 0x58
  private           UnityEngine.ParticleSystem      cachedParticleSystem  // 0x60
  private           System.Boolean                  cachedParticleSystemSet  // 0x68
  private   static  System.Collections.Generic.List<UnityEngine.ParticleCollisionEvent>particleCollisionEvents  // static @ 0x0
  private           PaintIn3D.P3dHitCache           hitCache  // 0x70
  private           System.Int32                    skipCounter  // 0x78
PROPERTIES:
  Emit  get=0x011EF5B0  set=0x011EF9B0
  RaycastDistance  get=0x04D868F0  set=0x04D86900
  Layers  get=0x02B70F90  set=0x04D86330
  Orientation  get=0x011F0020  set=0x011F0030
  Camera  get=0x04D86240  set=0x02FDB880
  Normal  get=0x04D864F0  set=0x04D86740
  Offset  get=0x04D8E050  set=0x04D8E060
  Skip  get=0x04D863B0  set=0x04D86430
  Preview  get=0x04D86BD0  set=0x04D86BE0
  Priority  get=0x04D865E0  set=0x04D86610
  PressureMode  get=0x04D86530  set=0x04D86580
  PressureMin  get=0x04D8D350  set=0x04D8D380
  PressureMax  get=0x04D87860  set=0x04D91480
  PressureConstant  get=0x04D885A0  set=0x04D88FF0
  PressureMultiplier  get=0x04D86E00  set=0x04D86E10
  Root  get=0x02E56440  set=0x0A8EDAB4
  HitCache  get=0x04D862B0
METHODS:
  RVA=0x0A8ECAA4  token=0x6000156  System.Void ClearHitCache()
  RVA=0x0A8ED7F0  token=0x6000157  System.Boolean TryGetRaycastHit(UnityEngine.ParticleCollisionEvent collision, UnityEngine.RaycastHit& hit)
  RVA=0x0A8ED004  token=0x6000158  System.Void OnParticleCollision(UnityEngine.GameObject hitGameObject)
  RVA=0x0A8EDA30  token=0x6000159  System.Void .ctor()
  RVA=0x0A8ED9A4  token=0x600015A  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dHitScreen
TYPE:  class
TOKEN: 0x200002B
SIZE:  0x88
EXTENDS: PaintIn3D.P3dHitScreenBase
FIELDS:
  private           PaintIn3D.P3dHitScreen.FrequencyTypefrequency  // 0x70
  private           System.Single                   interval  // 0x74
  private           PaintIn3D.P3dPointConnector     connector  // 0x78
  private           System.Collections.Generic.List<PaintIn3D.P3dHitScreen.Link>links  // 0x80
PROPERTIES:
  Frequency  get=0x04D868A0  set=0x04D868D0
  Interval  get=0x04DA60B0  set=0x04DA60C0
  Connector  get=0x0A8F05AC
METHODS:
  RVA=0x0A8EFA3C  token=0x6000160  System.Void ClearHitCache()
  RVA=0x0A8EAD7C  token=0x6000161  System.Void ResetConnections()
  RVA=0x0A8EFD50  token=0x6000162  System.Void OnEnable()
  RVA=0x0A8F04F8  token=0x6000163  System.Void Update()
  RVA=0x0A8EFB30  token=0x6000164  System.Void HandleFingerUpdate(CW.Common.CwInputManager.Finger finger, System.Boolean down, System.Boolean up)
  RVA=0x0A8EFA60  token=0x6000165  System.Void HandleFingerUp(CW.Common.CwInputManager.Finger finger)
  RVA=0x0A8F0148  token=0x6000166  System.Void PaintSmooth(PaintIn3D.P3dHitScreen.Link link, System.Boolean down, System.Single pixelSpacing)
  RVA=0x041E1670  token=0x6000167  System.Void OnFingerUp(PaintIn3D.P3dHitScreen.Link link)
  RVA=0x0A8EFF30  token=0x6000168  System.Void PaintInterval(PaintIn3D.P3dHitScreen.Link link, System.Boolean down)
  RVA=0x0A8F00AC  token=0x6000169  System.Void PaintRelease(PaintIn3D.P3dHitScreen.Link link, System.Boolean up)
  RVA=0x0A8F0020  token=0x600016A  System.Void PaintPress(PaintIn3D.P3dHitScreen.Link link, System.Boolean down)
  RVA=0x0A8EFEA4  token=0x600016B  System.Void PaintEvery(PaintIn3D.P3dHitScreen.Link link, System.Boolean down)
  RVA=0x0A8F0424  token=0x600016C  System.Void RecordAndPaintAt(PaintIn3D.P3dHitScreen.Link link, UnityEngine.Vector2 screenNew, UnityEngine.Vector2 screenOld, System.Boolean preview, System.Single pressure, System.Object owner)
  RVA=0x0A8F0518  token=0x600016D  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dHitScreenBase
TYPE:  abstract class
TOKEN: 0x200002E
SIZE:  0x70
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.Camera              _camera  // 0x18
  private           UnityEngine.LayerMask           layers  // 0x20
  private           PaintIn3D.P3dHitScreenBase.ButtonTypesrequiredButtons  // 0x24
  private           UnityEngine.KeyCode             requiredKey  // 0x28
  private           UnityEngine.LayerMask           guiLayers  // 0x2c
  private           PaintIn3D.P3dHitScreenBase.EmitTypeemit  // 0x30
  private           PaintIn3D.P3dHitScreenBase.RotationTyperotateTo  // 0x34
  private           PaintIn3D.P3dHitScreenBase.DirectionTypenormalDirection  // 0x38
  private           PaintIn3D.P3dHitScreenBase.RelativeTypenormalRelativeTo  // 0x3c
  private           UnityEngine.Transform           customTransform  // 0x40
  private           System.Single                   normalOffset  // 0x48
  private           System.Single                   mouseOffset  // 0x4c
  private           System.Single                   touchOffset  // 0x50
  protected         System.Boolean                  storeStates  // 0x54
  protected         System.Boolean                  showPreview  // 0x55
  private           System.Int32                    priority  // 0x58
  protected         System.Collections.Generic.List<CW.Common.CwInputManager.Finger>fingers  // 0x60
  protected         System.Boolean                  keyPressed  // 0x68
PROPERTIES:
  Camera  get=0x0385B100  set=0x05392C40
  Layers  get=0x02B70F90  set=0x04D86330
  RequiredButtons  get=0x011F0020  set=0x011F0030
  RequiredKey  get=0x011F2A90  set=0x011F2AA0
  GuiLayers  get=0x04D86710  set=0x04D86720
  Emit  get=0x04D864F0  set=0x04D86740
  RotateTo  get=0x04D86730  set=0x04D86760
  NormalDirection  get=0x04D863B0  set=0x04D86430
  NormalRelativeTo  get=0x04D86640  set=0x04D86670
  CustomTransform  get=0x04D85A60  set=0x042B4AE0
  NormalOffset  get=0x04D8D350  set=0x04D8D380
  MouseOffset  get=0x04D87860  set=0x04D91480
  TouchOffset  get=0x04D885A0  set=0x04D88FF0
  StoreStates  get=0x04D86C20  set=0x04D86C40
  ShowPreview  get=0x04D86C10  set=0x04D86C30
  Priority  get=0x04D86570  set=0x04D865D0
  NeedsDrawAngle  get=0x0A8EECC4
METHODS:
  RVA=0x0A8EE51C  token=0x6000193  System.Void OnEnable()
  RVA=0x0A8EE254  token=0x6000194  System.Void LateUpdate()
  RVA=0x041E1670  token=0x6000195  System.Void HandleFingerUpdate(CW.Common.CwInputManager.Finger finger, System.Boolean down, System.Boolean up)
  RVA=0x041E1670  token=0x6000196  System.Void HandleFingerUp(CW.Common.CwInputManager.Finger finger)
  RVA=0x0A8EE468  token=0x6000197  System.Void OnDisable()
  RVA=0x0A8EE0A8  token=0x6000198  System.Boolean FingerWentDown(CW.Common.CwInputManager.Finger finger)
  RVA=0x0A8EE184  token=0x6000199  System.Void HandleFingerDown(CW.Common.CwInputManager.Finger finger)
  RVA=0x0A8EDF30  token=0x600019A  System.Void DoQuery(UnityEngine.Vector2 screenPosition, UnityEngine.Camera& camera, UnityEngine.Ray& ray, UnityEngine.RaycastHit& hit3D, UnityEngine.RaycastHit2D& hit2D)
  RVA=0x0A8EE6DC  token=0x600019B  System.Void PaintAt(PaintIn3D.P3dPointConnector connector, PaintIn3D.P3dHitCache hitCache, UnityEngine.Vector2 screenPosition, UnityEngine.Vector2 screenPositionOld, System.Boolean preview, System.Single pressure, System.Object owner)
  RVA=0x0A8EDAE4  token=0x600019C  System.Void CalcHitData(UnityEngine.Vector3 hitPoint, UnityEngine.Vector3 hitNormal, UnityEngine.Ray ray, UnityEngine.Camera camera, UnityEngine.Vector2 screenPositionOld, UnityEngine.Vector3& finalPosition, UnityEngine.Quaternion& finalRotation)
  RVA=0x0A8EEC1C  token=0x600019D  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dHitScreenFill
TYPE:  class
TOKEN: 0x2000034
SIZE:  0x90
EXTENDS: PaintIn3D.P3dHitScreen
FIELDS:
  private           System.Single                   fillSpacing  // 0x88
PROPERTIES:
  FillSpacing  get=0x04D8E220  set=0x04D8E250
METHODS:
  RVA=0x0A8EF0C0  token=0x60001A0  System.Void OnFingerUp(PaintIn3D.P3dHitScreen.Link link)
  RVA=0x0A8EEE7C  token=0x60001A1  UnityEngine.Rect GetArea(System.Collections.Generic.List<UnityEngine.Vector2> points)
  RVA=0x04DA6050  token=0x60001A2  System.Double LineSide(UnityEngine.Vector2 a, UnityEngine.Vector2 b, UnityEngine.Vector2 p)
  RVA=0x0A8EECD8  token=0x60001A3  System.Boolean Contains(System.Collections.Generic.List<UnityEngine.Vector2> points, UnityEngine.Vector2 xy)
  RVA=0x0A8EF394  token=0x60001A4  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dHitScreenLine
TYPE:  class
TOKEN: 0x2000035
SIZE:  0x88
EXTENDS: PaintIn3D.P3dHitScreenBase
FIELDS:
  private           PaintIn3D.P3dHitScreenLine.FrequencyTypefrequency  // 0x70
  private           System.Single                   interval  // 0x74
  private           System.Single                   position  // 0x78
  private           System.Single                   pixelOffset  // 0x7c
  private           PaintIn3D.P3dPointConnector     connector  // 0x80
PROPERTIES:
  Frequency  get=0x04D868A0  set=0x04D868D0
  Interval  get=0x04DA60B0  set=0x04DA60C0
  Position  get=0x04D8ED60  set=0x04D8ED70
  PixelOffset  get=0x04D88E80  set=0x04D8ED80
  Connector  get=0x0A8EF9C0
METHODS:
  RVA=0x0A8EF3A8  token=0x60001AE  System.Void ClearHitCache()
  RVA=0x0A8EF960  token=0x60001AF  System.Void ResetConnections()
  RVA=0x0A8EF544  token=0x60001B0  System.Void OnEnable()
  RVA=0x0A8EF984  token=0x60001B1  System.Void Update()
  RVA=0x0A8EF3CC  token=0x60001B2  System.Void HandleFingerUpdate(CW.Common.CwInputManager.Finger finger, System.Boolean down, System.Boolean up)
  RVA=0x0A8EF6BC  token=0x60001B3  System.Void PaintStartEnd(CW.Common.CwInputManager.Finger finger, System.Boolean up)
  RVA=0x0A8EF7E4  token=0x60001B4  System.Void PaintStartInterval(CW.Common.CwInputManager.Finger finger, System.Boolean up, System.Single pixelSpacing, System.Boolean stretch)
  RVA=0x0A8EF578  token=0x60001B5  System.Void PaintOne(CW.Common.CwInputManager.Finger finger, System.Boolean up, System.Single frac, System.Single pixelOff)
  RVA=0x0A8EF9A8  token=0x60001B6  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dHitThrough
TYPE:  class
TOKEN: 0x2000037
SIZE:  0x68
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           PaintIn3D.P3dHitThrough.PhaseTypepaintIn  // 0x18
  private           System.Single                   interval  // 0x1c
  private           UnityEngine.Transform           pointA  // 0x20
  private           UnityEngine.Transform           pointB  // 0x28
  private           PaintIn3D.P3dHitThrough.OrientationTypeorientation  // 0x30
  private           UnityEngine.Camera              _camera  // 0x38
  private           System.Single                   pressure  // 0x40
  private           System.Boolean                  preview  // 0x44
  private           System.Int32                    priority  // 0x48
  private           UnityEngine.LineRenderer        line  // 0x50
  private           PaintIn3D.P3dLineConnector      connector  // 0x58
  private           System.Single                   current  // 0x60
PROPERTIES:
  PaintIn  get=0x011EF5B0  set=0x011EF9B0
  Interval  get=0x04D868F0  set=0x04D86900
  PointA  get=0x04D862C0  set=0x05390F40
  PointB  get=0x04D86240  set=0x02FDB880
  Orientation  get=0x04D864F0  set=0x04D86740
  Camera  get=0x04D85A50  set=0x046A91B0
  Pressure  get=0x04D863F0  set=0x04D86470
  Preview  get=0x04D86A30  set=0x04D86A40
  Priority  get=0x04D863D0  set=0x04D86450
  Line  get=0x04D86270  set=0x02FDAEA0
  Connector  get=0x0A8F0C0C
METHODS:
  RVA=0x0A8F065C  token=0x60001CC  System.Void ManuallyHitNow()
  RVA=0x0A8F061C  token=0x60001CD  System.Void ClearHitCache()
  RVA=0x0A8F068C  token=0x60001CE  System.Void ResetConnections()
  RVA=0x0A8F0668  token=0x60001CF  System.Void OnEnable()
  RVA=0x0A8F0B98  token=0x60001D0  System.Void Update()
  RVA=0x0A8F0654  token=0x60001D1  System.Void LateUpdate()
  RVA=0x0A8F0640  token=0x60001D2  System.Void FixedUpdate()
  RVA=0x0A8F06AC  token=0x60001D3  System.Void SubmitHit(System.Boolean preview)
  RVA=0x0A8F09CC  token=0x60001D4  System.Void UpdateHit()
  RVA=0x0A8F0A34  token=0x60001D5  System.Void UpdatePointAndLine()
  RVA=0x0A8F0BE8  token=0x60001D6  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.IClone
TYPE:  interface
TOKEN: 0x200003A
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001D7  System.Void Transform(UnityEngine.Matrix4x4& posMatrix, UnityEngine.Matrix4x4& rotMatrix)
END_CLASS

CLASS: PaintIn3D.IHit
TYPE:  interface
TOKEN: 0x200003B
FIELDS:
METHODS:
END_CLASS

CLASS: PaintIn3D.IHitCoord
TYPE:  interface
TOKEN: 0x200003C
IMPLEMENTS: PaintIn3D.IHit
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001D8  System.Void HandleHitCoord(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, PaintIn3D.P3dHit hit, UnityEngine.Quaternion rotation)
END_CLASS

CLASS: PaintIn3D.IHitLine
TYPE:  interface
TOKEN: 0x200003D
IMPLEMENTS: PaintIn3D.IHit
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001D9  System.Void HandleHitLine(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Quaternion rotation, System.Boolean clip)
END_CLASS

CLASS: PaintIn3D.IHitPoint
TYPE:  interface
TOKEN: 0x200003E
IMPLEMENTS: PaintIn3D.IHit
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001DA  System.Void HandleHitPoint(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
END_CLASS

CLASS: PaintIn3D.IHitQuad
TYPE:  interface
TOKEN: 0x200003F
IMPLEMENTS: PaintIn3D.IHit
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001DB  System.Void HandleHitQuad(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Vector3 position2, UnityEngine.Vector3 endPosition2, UnityEngine.Quaternion rotation, System.Boolean clip)
END_CLASS

CLASS: PaintIn3D.IHitTriangle
TYPE:  interface
TOKEN: 0x2000040
IMPLEMENTS: PaintIn3D.IHit
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001DC  System.Void HandleHitTriangle(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 positionA, UnityEngine.Vector3 positionB, UnityEngine.Vector3 positionC, UnityEngine.Quaternion rotation)
END_CLASS

CLASS: PaintIn3D.IBrowsable
TYPE:  interface
TOKEN: 0x2000041
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001DD  System.String GetCategory()
  RVA=-1  // abstract  token=0x60001DE  System.String GetTitle()
  RVA=-1  // abstract  token=0x60001DF  UnityEngine.Texture2D GetIcon()
  RVA=-1  // abstract  token=0x60001E0  UnityEngine.Object GetObject()
END_CLASS

CLASS: PaintIn3D.P3dHitScreen_InEditor
TYPE:  class
TOKEN: 0x2000042
SIZE:  0x88
EXTENDS: PaintIn3D.P3dHitScreen
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60001E1  System.Void DoQuery(UnityEngine.Vector2 screenPosition, UnityEngine.Camera& camera, UnityEngine.Ray& ray, UnityEngine.RaycastHit& hit3D, UnityEngine.RaycastHit2D& hit2D)
  RVA=0x041E1670  token=0x60001E2  System.Void LateUpdate()
  RVA=0x0A8EFD48  token=0x60001E3  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dMaterial
TYPE:  class
TOKEN: 0x2000043
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: PaintIn3D.IBrowsable
FIELDS:
  private           System.String                   category  // 0x18
  private           UnityEngine.Texture2D           icon  // 0x20
  private           System.Collections.Generic.List<UnityEngine.Texture>textures  // 0x28
  private   static  System.Collections.Generic.List<PaintIn3D.P3dMaterial>cachedMaterials  // static @ 0x0
PROPERTIES:
  Category  get=0x0385B100  set=0x05392C40
  Icon  get=0x04D862C0  set=0x05390F40
  Textures  get=0x0A8F0D7C
  CachedMaterials  get=0x0A8F0CC8
METHODS:
  RVA=0x0A8F0C7C  token=0x60001EA  System.Void ClearCache()
  RVA=0x0385B100  token=0x60001EB  System.String GetCategory()
  RVA=0x05396F58  token=0x60001EC  System.String GetTitle()
  RVA=0x04D862C0  token=0x60001ED  UnityEngine.Texture2D GetIcon()
  RVA=0x0464E630  token=0x60001EE  UnityEngine.Object GetObject()
  RVA=0x05393520  token=0x60001EF  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dPreset
TYPE:  class
TOKEN: 0x2000044
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.String                   title  // 0x18
  private           System.Boolean                  addMaterialCloner  // 0x20
  private   static  System.Collections.Generic.List<PaintIn3D.P3dPreset>cachedPresets  // static @ 0x0
PROPERTIES:
  Title  get=0x0385B100  set=0x05392C40
  AddMaterialCloner  get=0x04D866B0  set=0x04D866D0
METHODS:
  RVA=0x04D84C40  token=0x60001F4  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dSeamFixer
TYPE:  class
TOKEN: 0x2000045
SIZE:  0x40
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  private           UnityEngine.Mesh                source  // 0x18
  private           UnityEngine.Mesh                mesh  // 0x20
  private           System.Collections.Generic.List<PaintIn3D.P3dSeamFixer.Pair>meshes  // 0x28
  private           PaintIn3D.P3dCoord              coord  // 0x30
  private           System.Single                   threshold  // 0x34
  private           System.Single                   border  // 0x38
  private   static  System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh>cacheFirst  // static @ 0x0
  private   static  System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh>cacheSecond  // static @ 0x8
  private   static  System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh>cacheThird  // static @ 0x10
  private   static  System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh>cacheFourth  // static @ 0x18
PROPERTIES:
  Meshes  get=0x0A8F5C24
  Coord  get=0x04D864F0  set=0x04D86740
  Threshold  get=0x04D8E050  set=0x04D8E060
  Border  get=0x04D88360  set=0x04D88370
METHODS:
  RVA=0x0A8F5524  token=0x60001FC  UnityEngine.Mesh GetCachedMesh(UnityEngine.Mesh source, PaintIn3D.P3dCoord coord, System.Boolean allowGeneration)
  RVA=0x0A8F58F4  token=0x60001FD  UnityEngine.Mesh TryGetCachedMesh(System.Collections.Generic.Dictionary<UnityEngine.Mesh,UnityEngine.Mesh> cache, UnityEngine.Mesh source, PaintIn3D.P3dCoord coord, System.Boolean allowGeneration)
  RVA=0x0A8F27C8  token=0x60001FE  System.Void AddMesh(UnityEngine.Mesh mesh)
  RVA=0x0A8F2BE4  token=0x60001FF  System.Void ConvertLegacy()
  RVA=0x0A8F51E4  token=0x6000200  System.Void Generate()
  RVA=0x0A8F440C  token=0x6000201  System.Void Generate(UnityEngine.Mesh source, UnityEngine.Mesh output, PaintIn3D.P3dCoord coord, System.Single threshold, System.Single border)
  RVA=0x0A8F26DC  token=0x6000202  System.Void AddCoord(System.Collections.Generic.List<UnityEngine.Vector4> coords, PaintIn3D.P3dSeamFixer.Insertion insertion, System.Boolean write)
  RVA=0x0A8F2CD0  token=0x6000203  System.Void FixSeams(UnityEngine.Mesh source, UnityEngine.Mesh output, System.Collections.Generic.List<System.Collections.Generic.List<System.Int32>> submeshes, System.Collections.Generic.List<PaintIn3D.P3dSeamFixer.Ring> rings, System.Collections.Generic.List<PaintIn3D.P3dSeamFixer.Insertion> insertions, PaintIn3D.P3dCoord coord)
  RVA=0x0A8F57D4  token=0x6000204  System.Void TraceEdges(PaintIn3D.P3dSeamFixer.Ring ring, PaintIn3D.P3dSeamFixer.Point point)
  RVA=0x0A8F2880  token=0x6000205  System.Void AddPoint(System.Collections.Generic.Dictionary<UnityEngine.Vector2,PaintIn3D.P3dSeamFixer.Point> points, UnityEngine.Vector2 coord, System.Int32 index)
  RVA=0x0A8F2A2C  token=0x6000206  System.Void AddTriangle(System.Collections.Generic.List<PaintIn3D.P3dSeamFixer.Edge> edges, PaintIn3D.P3dSeamFixer.Point pointA, PaintIn3D.P3dSeamFixer.Point pointB, PaintIn3D.P3dSeamFixer.Point pointC)
  RVA=0x0A8F2980  token=0x6000207  System.Void AddTriangle2(System.Collections.Generic.List<PaintIn3D.P3dSeamFixer.Edge> edges, PaintIn3D.P3dSeamFixer.Point pointA, PaintIn3D.P3dSeamFixer.Point pointB, PaintIn3D.P3dSeamFixer.Point pointC, System.Boolean flip)
  RVA=0x0A8F5628  token=0x6000208  System.Void RemoveOrAddEdge(System.Collections.Generic.List<PaintIn3D.P3dSeamFixer.Edge> edges, PaintIn3D.P3dSeamFixer.Point pointA, PaintIn3D.P3dSeamFixer.Point pointB, System.Boolean flip)
  RVA=0x0A8F5C0C  token=0x6000209  System.Void .ctor()
  RVA=0x0A8F5A90  token=0x600020A  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dShape
TYPE:  class
TOKEN: 0x200004B
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: PaintIn3D.IBrowsable
FIELDS:
  private           System.String                   category  // 0x18
  private           UnityEngine.Texture2D           icon  // 0x20
  private   static  System.Collections.Generic.List<PaintIn3D.P3dShape>cachedShapes  // static @ 0x0
PROPERTIES:
  Category  get=0x0385B100  set=0x05392C40
  Icon  get=0x04D862C0  set=0x05390F40
  CachedShapes  get=0x0A8F5CF4
METHODS:
  RVA=0x0A8F5CA8  token=0x6000217  System.Void ClearCache()
  RVA=0x0385B100  token=0x6000218  System.String GetCategory()
  RVA=0x05396F58  token=0x6000219  System.String GetTitle()
  RVA=0x04D862C0  token=0x600021A  UnityEngine.Texture2D GetIcon()
  RVA=0x0464E630  token=0x600021B  UnityEngine.Object GetObject()
  RVA=0x05393520  token=0x600021C  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dTool
TYPE:  class
TOKEN: 0x200004C
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: PaintIn3D.IBrowsable
FIELDS:
  private           System.String                   category  // 0x18
  private           UnityEngine.Texture2D           icon  // 0x20
  private   static  System.Collections.Generic.List<PaintIn3D.P3dTool>cachedTools  // static @ 0x0
PROPERTIES:
  Category  get=0x0385B100  set=0x05392C40
  Icon  get=0x04D862C0  set=0x05390F40
  CachedTools  get=0x0A8F5DF4
METHODS:
  RVA=0x0A8F5DA8  token=0x6000222  System.Void ClearCache()
  RVA=0x0385B100  token=0x6000223  System.String GetCategory()
  RVA=0x05396F58  token=0x6000224  System.String GetTitle()
  RVA=0x04D862C0  token=0x6000225  UnityEngine.Texture2D GetIcon()
  RVA=0x0464E630  token=0x6000226  UnityEngine.Object GetObject()
  RVA=0x05393520  token=0x6000227  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dModifyAngleRandom
TYPE:  class
TOKEN: 0x200004D
SIZE:  0x28
EXTENDS: PaintIn3D.P3dModifier
FIELDS:
  public    static  System.String                   Group  // static @ 0x0
  public    static  System.String                   Title  // static @ 0x8
  private           System.Single                   min  // 0x18
  private           System.Single                   max  // 0x1c
  private           PaintIn3D.P3dModifyAngleRandom.BlendTypeblend  // 0x20
PROPERTIES:
  Min  get=0x04D86380  set=0x04D86390
  Max  get=0x04D868F0  set=0x04D86900
  Blend  get=0x02B70F90  set=0x04D86330
METHODS:
  RVA=0x0A8F0E00  token=0x600022E  System.Void OnModifyAngle(System.Single& angle, System.Single pressure)
  RVA=0x04DA60D0  token=0x600022F  System.Void .ctor()
  RVA=0x0A8F0E4C  token=0x6000230  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dModifyColorRandom
TYPE:  class
TOKEN: 0x200004F
SIZE:  0x28
EXTENDS: PaintIn3D.P3dModifier
FIELDS:
  public    static  System.String                   Group  // static @ 0x0
  public    static  System.String                   Title  // static @ 0x8
  private           UnityEngine.Gradient            gradient  // 0x18
  private           PaintIn3D.P3dModifyColorRandom.BlendTypeblend  // 0x20
PROPERTIES:
  Gradient  get=0x0A8F1038
  Blend  get=0x02B70F90  set=0x04D86330
METHODS:
  RVA=0x0A8F0EE4  token=0x6000234  System.Void OnModifyColor(UnityEngine.Color& color, System.Single pressure)
  RVA=0x04DA60F0  token=0x6000235  System.Void .ctor()
  RVA=0x0A8F0FA0  token=0x6000236  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dModifyHardnessPressure
TYPE:  class
TOKEN: 0x2000051
SIZE:  0x20
EXTENDS: PaintIn3D.P3dModifier
FIELDS:
  public    static  System.String                   Group  // static @ 0x0
  public    static  System.String                   Title  // static @ 0x8
  private           System.Single                   hardness  // 0x18
  private           PaintIn3D.P3dModifyHardnessPressure.BlendTypeblend  // 0x1c
PROPERTIES:
  Hardness  get=0x04D86380  set=0x04D86390
  Blend  get=0x04D86310  set=0x04D86320
METHODS:
  RVA=0x0A8F10AC  token=0x600023B  System.Void OnModifyHardness(System.Single& currentHardness, System.Single pressure)
  RVA=0x04DA6100  token=0x600023C  System.Void .ctor()
  RVA=0x0A8F10F0  token=0x600023D  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dModifyHardnessRandom
TYPE:  class
TOKEN: 0x2000053
SIZE:  0x28
EXTENDS: PaintIn3D.P3dModifier
FIELDS:
  public    static  System.String                   Group  // static @ 0x0
  public    static  System.String                   Title  // static @ 0x8
  private           System.Single                   min  // 0x18
  private           System.Single                   max  // 0x1c
  private           PaintIn3D.P3dModifyHardnessRandom.BlendTypeblend  // 0x20
PROPERTIES:
  Min  get=0x04D86380  set=0x04D86390
  Max  get=0x04D868F0  set=0x04D86900
  Blend  get=0x02B70F90  set=0x04D86330
METHODS:
  RVA=0x0A8F0E00  token=0x6000244  System.Void OnModifyHardness(System.Single& hardness, System.Single pressure)
  RVA=0x04DA6110  token=0x6000245  System.Void .ctor()
  RVA=0x0A8F1188  token=0x6000246  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dModifyOpacityPressure
TYPE:  class
TOKEN: 0x2000055
SIZE:  0x20
EXTENDS: PaintIn3D.P3dModifier
FIELDS:
  public    static  System.String                   Group  // static @ 0x0
  public    static  System.String                   Title  // static @ 0x8
  private           System.Single                   opacity  // 0x18
  private           PaintIn3D.P3dModifyOpacityPressure.BlendTypeblend  // 0x1c
PROPERTIES:
  Opacity  get=0x04D86380  set=0x04D86390
  Blend  get=0x04D86310  set=0x04D86320
METHODS:
  RVA=0x0A8F10AC  token=0x600024B  System.Void OnModifyOpacity(System.Single& currentOpacity, System.Single pressure)
  RVA=0x04DA6100  token=0x600024C  System.Void .ctor()
  RVA=0x0A8F1220  token=0x600024D  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dModifyOpacityRandom
TYPE:  class
TOKEN: 0x2000057
SIZE:  0x28
EXTENDS: PaintIn3D.P3dModifier
FIELDS:
  public    static  System.String                   Group  // static @ 0x0
  public    static  System.String                   Title  // static @ 0x8
  private           System.Single                   min  // 0x18
  private           System.Single                   max  // 0x1c
  private           PaintIn3D.P3dModifyOpacityRandom.BlendTypeblend  // 0x20
PROPERTIES:
  Min  get=0x04D86380  set=0x04D86390
  Max  get=0x04D868F0  set=0x04D86900
  Blend  get=0x02B70F90  set=0x04D86330
METHODS:
  RVA=0x0A8F0E00  token=0x6000254  System.Void OnModifyOpacity(System.Single& opacity, System.Single pressure)
  RVA=0x04DA6110  token=0x6000255  System.Void .ctor()
  RVA=0x0A8F12B8  token=0x6000256  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dModifyPositionRandom
TYPE:  class
TOKEN: 0x2000059
SIZE:  0x20
EXTENDS: PaintIn3D.P3dModifier
FIELDS:
  public    static  System.String                   Group  // static @ 0x0
  public    static  System.String                   Title  // static @ 0x8
  private           System.Single                   radius  // 0x18
PROPERTIES:
  Radius  get=0x04D86380  set=0x04D86390
METHODS:
  RVA=0x0A8F1350  token=0x6000259  System.Void OnModifyPosition(UnityEngine.Vector3& position, System.Single pressure)
  RVA=0x04DA6100  token=0x600025A  System.Void .ctor()
  RVA=0x0A8F13F8  token=0x600025B  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dModifyRadiusPressure
TYPE:  class
TOKEN: 0x200005A
SIZE:  0x20
EXTENDS: PaintIn3D.P3dModifier
FIELDS:
  public    static  System.String                   Group  // static @ 0x0
  public    static  System.String                   Title  // static @ 0x8
  private           System.Single                   radius  // 0x18
  private           PaintIn3D.P3dModifyRadiusPressure.BlendTypeblend  // 0x1c
PROPERTIES:
  Radius  get=0x04D86380  set=0x04D86390
  Blend  get=0x04D86310  set=0x04D86320
METHODS:
  RVA=0x0A8F10AC  token=0x6000260  System.Void OnModifyRadius(System.Single& currentRadius, System.Single pressure)
  RVA=0x04DA6100  token=0x6000261  System.Void .ctor()
  RVA=0x0A8F1490  token=0x6000262  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dModifyRadiusRandom
TYPE:  class
TOKEN: 0x200005C
SIZE:  0x28
EXTENDS: PaintIn3D.P3dModifier
FIELDS:
  public    static  System.String                   Group  // static @ 0x0
  public    static  System.String                   Title  // static @ 0x8
  private           System.Single                   min  // 0x18
  private           System.Single                   max  // 0x1c
  private           PaintIn3D.P3dModifyRadiusRandom.BlendTypeblend  // 0x20
PROPERTIES:
  Min  get=0x04D86380  set=0x04D86390
  Max  get=0x04D868F0  set=0x04D86900
  Blend  get=0x02B70F90  set=0x04D86330
METHODS:
  RVA=0x0A8F0E00  token=0x6000269  System.Void OnModifyRadius(System.Single& radius, System.Single pressure)
  RVA=0x04DA6110  token=0x600026A  System.Void .ctor()
  RVA=0x0A8F1528  token=0x600026B  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dModifyScaleRandom
TYPE:  class
TOKEN: 0x200005E
SIZE:  0x38
EXTENDS: PaintIn3D.P3dModifier
FIELDS:
  public    static  System.String                   Group  // static @ 0x0
  public    static  System.String                   Title  // static @ 0x8
  private           UnityEngine.Vector3             min  // 0x18
  private           UnityEngine.Vector3             max  // 0x24
  private           PaintIn3D.P3dModifyScaleRandom.BlendTypeblend  // 0x30
  private           System.Boolean                  uniform  // 0x34
PROPERTIES:
  Min  get=0x04D89870  set=0x04D89890
  Max  get=0x04D905E0  set=0x04D905D0
  Blend  get=0x04D864F0  set=0x04D86740
  Uniform  get=0x04D86A90  set=0x04D86AA0
METHODS:
  RVA=0x0A8F15C0  token=0x6000274  System.Void OnModifyScale(UnityEngine.Vector3& scale, System.Single pressure)
  RVA=0x04DA6130  token=0x6000275  System.Void .ctor()
  RVA=0x0A8F1770  token=0x6000276  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dModifyTexturePressure
TYPE:  class
TOKEN: 0x2000060
SIZE:  0x28
EXTENDS: PaintIn3D.P3dModifier
FIELDS:
  public    static  System.String                   Group  // static @ 0x0
  public    static  System.String                   Title  // static @ 0x8
  private           UnityEngine.Texture             texture  // 0x18
  private           System.Single                   pressureMin  // 0x20
  private           System.Single                   pressureMax  // 0x24
PROPERTIES:
  Texture  get=0x0385B100  set=0x05392C40
  PressureMin  get=0x011EC8A0  set=0x011EC9E0
  PressureMax  get=0x04D89850  set=0x04D89860
METHODS:
  RVA=0x0A8F1808  token=0x600027D  System.Void OnModifyTexture(UnityEngine.Texture& currentTexture, System.Single pressure)
  RVA=0x04DA6170  token=0x600027E  System.Void .ctor()
  RVA=0x0A8F1828  token=0x600027F  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dModifyTextureRandom
TYPE:  class
TOKEN: 0x2000061
SIZE:  0x20
EXTENDS: PaintIn3D.P3dModifier
FIELDS:
  public    static  System.String                   Group  // static @ 0x0
  public    static  System.String                   Title  // static @ 0x8
  private           System.Collections.Generic.List<UnityEngine.Texture>textures  // 0x18
PROPERTIES:
  Textures  get=0x0A8F19E4
METHODS:
  RVA=0x0A8F18C0  token=0x6000281  System.Void OnModifyTexture(UnityEngine.Texture& texture, System.Single pressure)
  RVA=0x04DA60F0  token=0x6000282  System.Void .ctor()
  RVA=0x0A8F194C  token=0x6000283  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dBlendMode
TYPE:  sealed struct
TOKEN: 0x2000062
SIZE:  0x48
FIELDS:
  public    static  System.Int32                    ALPHA_BLEND  // const
  public    static  System.Int32                    ALPHA_BLEND_INVERSE  // const
  public    static  System.Int32                    PREMULTIPLIED  // const
  public    static  System.Int32                    ADDITIVE  // const
  public    static  System.Int32                    ADDITIVE_SOFT  // const
  public    static  System.Int32                    SUBTRACTIVE  // const
  public    static  System.Int32                    SUBTRACTIVE_SOFT  // const
  public    static  System.Int32                    REPLACE  // const
  public    static  System.Int32                    REPLACE_ORIGINAL  // const
  public    static  System.Int32                    REPLACE_CUSTOM  // const
  public    static  System.Int32                    MULTIPLY_INVERSE_RGB  // const
  public    static  System.Int32                    BLUR  // const
  public    static  System.Int32                    NORMAL_BLEND  // const
  public    static  System.Int32                    NORMAL_REPLACE  // const
  public    static  System.Int32                    FLOW  // const
  public    static  System.Int32                    NORMAL_REPLACE_ORIGINAL  // const
  public    static  System.Int32                    NORMAL_REPLACE_CUSTOM  // const
  public    static  System.Int32                    COUNT  // const
  public    static readonly System.String[]                 NAMES  // static @ 0x0
  public            System.Int32                    Index  // 0x10
  public            UnityEngine.Color               Color  // 0x14
  public            UnityEngine.Texture             Texture  // 0x28
  public            System.Single                   Kernel  // 0x30
  public            UnityEngine.Vector4             Channels  // 0x34
  private   static  System.Int32                    _Channels  // static @ 0x8
  private   static  System.Int32                    _ReplaceColor  // static @ 0xc
  private   static  System.Int32                    _ReplaceTexture  // static @ 0x10
  private   static  System.Int32                    _ReplaceTextureSize  // static @ 0x14
  private   static  System.Int32                    _Kernel  // static @ 0x18
METHODS:
  RVA=0x0A8E36E4  token=0x6000284  PaintIn3D.P3dBlendMode AlphaBlend(UnityEngine.Vector4 channels)
  RVA=0x0A8E369C  token=0x6000285  PaintIn3D.P3dBlendMode AlphaBlendInverse(UnityEngine.Vector4 channels)
  RVA=0x0A8E3C94  token=0x6000286  PaintIn3D.P3dBlendMode Premultiplied(UnityEngine.Vector4 channels)
  RVA=0x0A8E3654  token=0x6000287  PaintIn3D.P3dBlendMode Additive(UnityEngine.Vector4 channels)
  RVA=0x0A8E360C  token=0x6000288  PaintIn3D.P3dBlendMode AdditiveSoft(UnityEngine.Vector4 channels)
  RVA=0x0A8E3E04  token=0x6000289  PaintIn3D.P3dBlendMode Subtractive(UnityEngine.Vector4 channels)
  RVA=0x0A8E3DBC  token=0x600028A  PaintIn3D.P3dBlendMode SubtractiveSoft(UnityEngine.Vector4 channels)
  RVA=0x0A8E3D74  token=0x600028B  PaintIn3D.P3dBlendMode Replace(UnityEngine.Vector4 channels)
  RVA=0x0A8E3D2C  token=0x600028C  PaintIn3D.P3dBlendMode ReplaceOriginal(UnityEngine.Vector4 channels)
  RVA=0x0A8E3CDC  token=0x600028D  PaintIn3D.P3dBlendMode ReplaceCustom(UnityEngine.Color color, UnityEngine.Texture texture, UnityEngine.Vector4 channels)
  RVA=0x0A8E3B24  token=0x600028E  PaintIn3D.P3dBlendMode MultiplyInverseRGB(UnityEngine.Vector4 channels)
  RVA=0x0A8E3948  token=0x600028F  PaintIn3D.P3dBlendMode Blur(System.Single kernel, UnityEngine.Vector4 channels)
  RVA=0x0A8E3B6C  token=0x6000290  PaintIn3D.P3dBlendMode NormalBlend(UnityEngine.Vector4 channels)
  RVA=0x0A8E3C4C  token=0x6000291  PaintIn3D.P3dBlendMode NormalReplace(UnityEngine.Vector4 channels)
  RVA=0x0A8E3A08  token=0x6000292  PaintIn3D.P3dBlendMode Flow(System.Single kernel, UnityEngine.Vector4 channels)
  RVA=0x0A8E3C04  token=0x6000293  PaintIn3D.P3dBlendMode NormalReplaceOriginal(UnityEngine.Vector4 channels)
  RVA=0x0A8E3BB4  token=0x6000294  PaintIn3D.P3dBlendMode NormalReplaceCustom(UnityEngine.Color color, UnityEngine.Texture texture, UnityEngine.Vector4 channels)
  RVA=0x0A8E3728  token=0x6000295  System.Void Apply(UnityEngine.Material material)
  RVA=0x0A8E3AB8  token=0x6000296  System.String GetName(System.Int32 index)
  RVA=0x02EED260  token=0x6000297  System.Boolean op_Equality(PaintIn3D.P3dBlendMode a, System.Int32 b)
  RVA=0x0A8E41BC  token=0x6000298  System.Boolean op_Inequality(PaintIn3D.P3dBlendMode a, System.Int32 b)
  RVA=0x02B77620  token=0x6000299  System.Int32 op_Implicit(PaintIn3D.P3dBlendMode a)
  RVA=0x0A8E3A4C  token=0x600029A  System.Int32 GetHashCode()
  RVA=0x0A8E398C  token=0x600029B  System.Boolean Equals(System.Object obj)
  RVA=0x0A8E3E4C  token=0x600029C  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dBlit
TYPE:  static class
TOKEN: 0x2000063
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.Material            cachedWhite  // static @ 0x0
  private   static  System.Boolean                  cachedWhiteSet  // static @ 0x8
  private   static  UnityEngine.Material            cachedTexture  // static @ 0x10
  private   static  System.Boolean                  cachedTextureSet  // static @ 0x18
  private   static  UnityEngine.Material            cachedNormal  // static @ 0x20
  private   static  System.Boolean                  cachedNormalSet  // static @ 0x28
  private   static  UnityEngine.Material            cachedPremultiply  // static @ 0x30
  private   static  System.Boolean                  cachedPremultiplySet  // static @ 0x38
  private   static  System.Int32                    _Buffer  // static @ 0x3c
  private   static  System.Int32                    _BufferSize  // static @ 0x40
  private   static  System.Int32                    _Texture  // static @ 0x44
  private   static  System.Int32                    _Color  // static @ 0x48
METHODS:
  RVA=0x0A8F69E4  token=0x600029D  System.Void White(UnityEngine.RenderTexture rendertexture, UnityEngine.Mesh mesh, System.Int32 submesh, PaintIn3D.P3dCoord coord)
  RVA=0x0A8F6760  token=0x600029E  System.Void Texture(UnityEngine.RenderTexture rendertexture, UnityEngine.Mesh mesh, System.Int32 submesh, UnityEngine.Texture texture, PaintIn3D.P3dCoord coord)
  RVA=0x0A8F6458  token=0x600029F  System.Void Normal(UnityEngine.RenderTexture rendertexture, UnityEngine.Texture texture)
  RVA=0x0A8F65A0  token=0x60002A0  System.Void Premultiply(UnityEngine.RenderTexture rendertexture, UnityEngine.Texture texture, UnityEngine.Color tint)
  RVA=0x0A8F6C28  token=0x60002A1  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dChangeCounter
TYPE:  class
TOKEN: 0x2000064
SIZE:  0xE0
EXTENDS: PaintIn3D.P3dPaintableTextureMonitorMask
FIELDS:
  public    static  System.Collections.Generic.LinkedList<PaintIn3D.P3dChangeCounter>Instances  // static @ 0x0
  private           System.Collections.Generic.LinkedListNode<PaintIn3D.P3dChangeCounter>instancesNode  // 0x98
  private           System.Single                   threshold  // 0xa0
  private           UnityEngine.Texture             texture  // 0xa8
  private           UnityEngine.Color               color  // 0xb0
  private           System.Int32                    count  // 0xc0
  private           PaintIn3D.P3dReader             changeReader  // 0xc8
  protected         Unity.Collections.NativeArray<UnityEngine.Color32>changePixels  // 0xd0
PROPERTIES:
  Threshold  get=0x04D91E60  set=0x0A8F7AD0
  Texture  get=0x04D86290  set=0x0A8F7A4C
  Color  get=0x04DA1230  set=0x0A8F79FC
  Count  get=0x04DA4B70
  Ratio  get=0x0A8F79D8
  ChangeReader  get=0x04D8D1D0
METHODS:
  RVA=0x0A8F73C0  token=0x60002AB  System.Void MarkChangeReaderAsDirty()
  RVA=0x0A8F6F20  token=0x60002AC  System.Int64 GetTotal(System.Collections.Generic.ICollection<PaintIn3D.P3dChangeCounter> counters)
  RVA=0x0A8F6CFC  token=0x60002AD  System.Int64 GetCount(System.Collections.Generic.ICollection<PaintIn3D.P3dChangeCounter> counters)
  RVA=0x0A8F6E94  token=0x60002AE  System.Single GetRatio(System.Collections.Generic.ICollection<PaintIn3D.P3dChangeCounter> counters)
  RVA=0x0A8F70B4  token=0x60002AF  System.Void HandleCompleteChange(Unity.Collections.NativeArray<UnityEngine.Color32> pixels)
  RVA=0x0A8F721C  token=0x60002B0  System.Void HandleComplete(System.Int32 boost)
  RVA=0x0A8F7558  token=0x60002B1  System.Void OnEnable()
  RVA=0x0A8F7450  token=0x60002B2  System.Void OnDisable()
  RVA=0x0A8F73D8  token=0x60002B3  System.Void OnDestroy()
  RVA=0x0A8F765C  token=0x60002B4  System.Void Update()
  RVA=0x0A8F799C  token=0x60002B5  System.Void .ctor()
  RVA=0x0A8F7920  token=0x60002B6  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dChannel
TYPE:  sealed struct
TOKEN: 0x2000065
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dChannel            Red  // const
  public    static  PaintIn3D.P3dChannel            Green  // const
  public    static  PaintIn3D.P3dChannel            Blue  // const
  public    static  PaintIn3D.P3dChannel            Alpha  // const
METHODS:
END_CLASS

CLASS: PaintIn3D.P3dChannelCounter
TYPE:  class
TOKEN: 0x2000066
SIZE:  0xB8
EXTENDS: PaintIn3D.P3dPaintableTextureMonitorMask
FIELDS:
  public    static  System.Collections.Generic.LinkedList<PaintIn3D.P3dChannelCounter>Instances  // static @ 0x0
  private           System.Collections.Generic.LinkedListNode<PaintIn3D.P3dChannelCounter>instancesNode  // 0x98
  private           System.Single                   threshold  // 0xa0
  private           System.Int32                    countR  // 0xa4
  private           System.Int32                    countG  // 0xa8
  private           System.Int32                    countB  // 0xac
  private           System.Int32                    countA  // 0xb0
PROPERTIES:
  Threshold  get=0x04D91E60  set=0x0A8F8C60
  CountR  get=0x04D88C90
  CountG  get=0x04D88C80
  CountB  get=0x04D88CA0
  CountA  get=0x04D8F8E0
  RatioR  get=0x0A8F8C3C
  RatioG  get=0x0A8F8B64
  RatioB  get=0x0A8F8B40
  RatioA  get=0x0A8F8B1C
  RatioRGBA  get=0x0A8F8B88
METHODS:
  RVA=0x0A8F86A8  token=0x60002C2  System.Int64 GetTotal(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters)
  RVA=0x0A8F7FC8  token=0x60002C3  System.Int64 GetCountR(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters)
  RVA=0x0A8F7E30  token=0x60002C4  System.Int64 GetCountG(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters)
  RVA=0x0A8F7C98  token=0x60002C5  System.Int64 GetCountB(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters)
  RVA=0x0A8F7B00  token=0x60002C6  System.Int64 GetCountA(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters)
  RVA=0x0A8F861C  token=0x60002C7  System.Single GetRatioR(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters)
  RVA=0x0A8F8278  token=0x60002C8  System.Single GetRatioG(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters)
  RVA=0x0A8F81EC  token=0x60002C9  System.Single GetRatioB(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters)
  RVA=0x0A8F8160  token=0x60002CA  System.Single GetRatioA(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters)
  RVA=0x0A8F8304  token=0x60002CB  UnityEngine.Vector4 GetRatioRGBA(System.Collections.Generic.ICollection<PaintIn3D.P3dChannelCounter> counters)
  RVA=0x0A8F89FC  token=0x60002CC  System.Void OnEnable()
  RVA=0x0A8F896C  token=0x60002CD  System.Void OnDisable()
  RVA=0x0A8F883C  token=0x60002CE  System.Void HandleComplete(System.Int32 boost)
  RVA=0x0A8F8B04  token=0x60002CF  System.Void .ctor()
  RVA=0x0A8F8A88  token=0x60002D0  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dClone
TYPE:  abstract class
TOKEN: 0x2000067
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: PaintIn3D.IClone
FIELDS:
  public    static  System.Int32                    MatrixCount  // static @ 0x0
  public    static  System.Int32                    ClonerCount  // static @ 0x4
  private   static  System.Collections.Generic.List<UnityEngine.Matrix4x4>tempPosMatrices  // static @ 0x8
  private   static  System.Collections.Generic.List<UnityEngine.Matrix4x4>tempRotMatrices  // static @ 0x10
  private   static  System.Collections.Generic.List<PaintIn3D.IClone>tempCloners  // static @ 0x18
  private   static  System.Collections.Generic.LinkedList<PaintIn3D.P3dClone>instances  // static @ 0x20
  private           System.Collections.Generic.LinkedListNode<PaintIn3D.P3dClone>instancesNode  // 0x18
PROPERTIES:
  Instances  get=0x0A8F9D8C
METHODS:
  RVA=-1  // abstract  token=0x60002D1  System.Void Transform(UnityEngine.Matrix4x4& posMatrix, UnityEngine.Matrix4x4& rotMatrix)
  RVA=0x0A8F93CC  token=0x60002D3  System.Void BuildCloners(System.Collections.Generic.List<PaintIn3D.IClone> cloners)
  RVA=0x0A8F9810  token=0x60002D4  System.Void Clone(PaintIn3D.P3dCommand command, System.Int32 clonerIndex, System.Int32 matrixIndex)
  RVA=0x0A8F9B74  token=0x60002D5  System.Void OnEnable()
  RVA=0x0A8F9AF4  token=0x60002D6  System.Void OnDisable()
  RVA=0x05393520  token=0x60002D7  System.Void .ctor()
  RVA=0x0A8F9BF0  token=0x60002D8  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dCloneMirror
TYPE:  class
TOKEN: 0x2000068
SIZE:  0x28
EXTENDS: PaintIn3D.P3dClone
FIELDS:
  private           System.Boolean                  flip  // 0x20
PROPERTIES:
  Flip  get=0x04D866B0  set=0x04D866D0
METHODS:
  RVA=0x0A8F8C88  token=0x60002DB  System.Void Transform(UnityEngine.Matrix4x4& posMatrix, UnityEngine.Matrix4x4& rotMatrix)
  RVA=0x0A8F9378  token=0x60002DC  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dColor
TYPE:  class
TOKEN: 0x2000069
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.Color               color  // 0x18
  private           System.Collections.Generic.List<PaintIn3D.P3dColor.Contribution>contributions  // 0x28
  private   static  System.Collections.Generic.LinkedList<PaintIn3D.P3dColor>instances  // static @ 0x0
  private           System.Collections.Generic.LinkedListNode<PaintIn3D.P3dColor>instancesNode  // 0x30
PROPERTIES:
  Color  get=0x04D890C0  set=0x04D890D0
  Instances  get=0x0A8FB340
  Total  get=0x0A8FB4C8
  Solid  get=0x0A8FB3D8
  Ratio  get=0x0A8FB390
METHODS:
  RVA=0x0A8FAFDC  token=0x60002E3  System.Void OnEnable()
  RVA=0x0A8FAE14  token=0x60002E4  System.Void OnDisable()
  RVA=0x0A8FAD48  token=0x60002E5  System.Void Contribute(PaintIn3D.P3dColorCounter counter, System.Int32 solid)
  RVA=0x0A8FB194  token=0x60002E6  System.Boolean TryGetContribution(PaintIn3D.P3dColorCounter counter, PaintIn3D.P3dColor.Contribution& contribution)
  RVA=0x05393520  token=0x60002E7  System.Void .ctor()
  RVA=0x0A8FB2C4  token=0x60002E8  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dColorCounter
TYPE:  class
TOKEN: 0x200006B
SIZE:  0xB0
EXTENDS: PaintIn3D.P3dPaintableTextureMonitorMask
FIELDS:
  public    static  System.Collections.Generic.LinkedList<PaintIn3D.P3dColorCounter>Instances  // static @ 0x0
  private           System.Collections.Generic.LinkedListNode<PaintIn3D.P3dColorCounter>instancesNode  // 0x98
  private           System.Single                   threshold  // 0xa0
  private           System.Collections.Generic.List<PaintIn3D.P3dColorCounter.Contribution>contributions  // 0xa8
PROPERTIES:
  Threshold  get=0x04D91E60  set=0x0A8F8C60
  Contributions  get=0x04D86290
METHODS:
  RVA=0x0A8FA420  token=0x60002ED  System.Int64 GetTotal(System.Collections.Generic.ICollection<PaintIn3D.P3dColorCounter> counters)
  RVA=0x0A8FA1E0  token=0x60002EE  System.Int64 GetCount(PaintIn3D.P3dColor color, System.Collections.Generic.ICollection<PaintIn3D.P3dColorCounter> counters)
  RVA=0x0A8FA390  token=0x60002EF  System.Single GetRatio(PaintIn3D.P3dColor color, System.Collections.Generic.ICollection<PaintIn3D.P3dColorCounter> counters)
  RVA=0x0A8FA07C  token=0x60002F0  System.Int32 Count(PaintIn3D.P3dColor color)
  RVA=0x0A8FAC04  token=0x60002F1  System.Single Ratio(PaintIn3D.P3dColor color)
  RVA=0x0A8FA8AC  token=0x60002F2  System.Void OnEnable()
  RVA=0x0A8FA810  token=0x60002F3  System.Void OnDisable()
  RVA=0x0A8FA5B4  token=0x60002F4  System.Void HandleComplete(System.Int32 boost)
  RVA=0x0A8F9DDC  token=0x60002F5  System.Void ClearContributions()
  RVA=0x0A8FA938  token=0x60002F6  System.Void PrepareContributions()
  RVA=0x0A8F9ECC  token=0x60002F7  System.Void Contribute(System.Int32 scale)
  RVA=0x0A8FACB8  token=0x60002F8  System.Void .ctor()
  RVA=0x0A8FAC3C  token=0x60002F9  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dCommand
TYPE:  abstract class
TOKEN: 0x200006D
SIZE:  0x70
FIELDS:
  public            System.Int32                    Index  // 0x10
  public            System.Boolean                  Preview  // 0x14
  public            System.Int32                    Priority  // 0x18
  public            PaintIn3D.P3dHashedMaterial     Material  // 0x20
  public            System.Int32                    Pass  // 0x30
  public            PaintIn3D.P3dHashedModel        Model  // 0x38
  public            System.Int32                    Submesh  // 0x48
  public            PaintIn3D.P3dHashedTexture      LocalMaskTexture  // 0x50
  public            UnityEngine.Vector4             LocalMaskChannel  // 0x60
  private   static  System.Int32                    _LocalMaskTexture  // static @ 0x0
  private   static  System.Int32                    _LocalMaskChannel  // static @ 0x4
PROPERTIES:
  RequireMesh  get=-1  // abstract
METHODS:
  RVA=0x0A8FB780  token=0x60002FC  System.Void BuildMaterial(UnityEngine.Material& material, System.Int32& materialHash, System.String path, System.String keyword)
  RVA=0x0A8FB820  token=0x60002FD  System.Int32 Compare(PaintIn3D.P3dCommand a, PaintIn3D.P3dCommand b)
  RVA=0x04DA6190  token=0x60002FF  System.Void SetState(System.Boolean preview, System.Int32 priority)
  RVA=0x0A8FB630  token=0x6000300  System.Void Apply(UnityEngine.Material material)
  RVA=-1  // abstract  token=0x6000301  System.Void Pool()
  RVA=-1  // abstract  token=0x6000302  System.Void Transform(UnityEngine.Matrix4x4 posMatrix, UnityEngine.Matrix4x4 rotMatrix)
  RVA=-1  // abstract  token=0x6000303  PaintIn3D.P3dCommand SpawnCopy()
  RVA=0x0A8FB87C  token=0x6000304  PaintIn3D.P3dCommand SpawnCopyLocal(UnityEngine.Transform transform)
  RVA=0x0A8FB9BC  token=0x6000305  PaintIn3D.P3dCommand SpawnCopyWorld(UnityEngine.Transform transform)
  RVA=-1  // generic def  token=0x6000306  T SpawnCopy(System.Collections.Generic.Stack<T> pool)
  RVA=0x0A8FB6EC  token=0x6000307  System.Void Apply(PaintIn3D.P3dPaintableTexture paintableTexture)
  RVA=0x041E1670  token=0x6000308  System.Void .ctor()
  RVA=0x0A8FBAFC  token=0x6000309  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dCommon
TYPE:  static class
TOKEN: 0x200006E
SIZE:  0x10
FIELDS:
  public    static  System.String                   HelpUrlPrefix  // const
  public    static  System.String                   ComponentMenuPrefix  // const
  public    static  System.String                   ComponentHitMenuPrefix  // const
  public    static  System.Action<UnityEngine.Camera>OnCameraDraw  // static @ 0x0
  private   static  System.Int32                    _Coord  // static @ 0x8
  private   static  UnityEngine.Mesh                sphereMesh  // static @ 0x10
  private   static  System.Boolean                  sphereMeshSet  // static @ 0x18
  private   static  UnityEngine.Mesh                quadMesh  // static @ 0x20
  private   static  System.Boolean                  quadMeshSet  // static @ 0x28
  private   static  UnityEngine.Texture2D           tempReadTexture  // static @ 0x30
METHODS:
  RVA=0x0A8FE3B8  token=0x600030A  System.Void .cctor()
  RVA=0x0A8FDDE8  token=0x600030B  System.Single RatioToPercentage(System.Single ratio01, System.Int32 decimalPlaces)
  RVA=0x0A8FD7D8  token=0x600030C  UnityEngine.RenderTexture GetRenderTexture(UnityEngine.RenderTexture other)
  RVA=0x0A8FD678  token=0x600030D  UnityEngine.RenderTexture GetRenderTexture(UnityEngine.RenderTextureDescriptor desc, UnityEngine.RenderTexture other)
  RVA=0x0A8FD890  token=0x600030E  UnityEngine.RenderTexture GetRenderTexture(UnityEngine.RenderTextureDescriptor desc)
  RVA=0x0A8FD75C  token=0x600030F  UnityEngine.RenderTexture GetRenderTexture(UnityEngine.RenderTextureDescriptor desc, System.Boolean sRGB)
  RVA=0x0A8FE2F8  token=0x6000310  UnityEngine.RenderTexture ReleaseRenderTexture(UnityEngine.RenderTexture renderTexture)
  RVA=0x0A8FDCA4  token=0x6000311  UnityEngine.Quaternion NormalToCameraRotation(UnityEngine.Vector3 normal, UnityEngine.Camera optionalCamera)
  RVA=0x0A8FC684  token=0x6000312  UnityEngine.Vector3 GetCameraUp(UnityEngine.Camera camera)
  RVA=0x0A8FBEDC  token=0x6000313  System.Boolean CanReadPixels(UnityEngine.TextureFormat format)
  RVA=0x0A8FDE94  token=0x6000314  System.Void ReadPixelsLinearGamma(UnityEngine.Texture2D texture2D, UnityEngine.RenderTexture renderTexture)
  RVA=0x0A8FE08C  token=0x6000315  System.Void ReadPixels(UnityEngine.Texture2D texture2D, UnityEngine.RenderTexture renderTexture)
  RVA=0x0A8FC110  token=0x6000316  System.Boolean Downsample(UnityEngine.RenderTexture renderTexture, System.Int32 steps, UnityEngine.RenderTexture& temporary)
  RVA=0x0A8FDA24  token=0x6000317  System.Boolean HasMipMaps(UnityEngine.Texture texture)
  RVA=0x0A8FD90C  token=0x6000318  UnityEngine.Mesh GetSphereMesh()
  RVA=0x0A8FC7D0  token=0x6000319  UnityEngine.Mesh GetMesh(UnityEngine.GameObject root, UnityEngine.Mesh mesh)
  RVA=0x0A8FCDC0  token=0x600031A  UnityEngine.Mesh GetQuadMesh()
  RVA=0x0A8FC8DC  token=0x600031B  UnityEngine.Color GetPixel(UnityEngine.RenderTexture renderTexture, UnityEngine.Vector2 uv, System.Boolean mipMaps)
  RVA=0x0A8FD428  token=0x600031C  UnityEngine.Texture2D GetReadableCopy(UnityEngine.Texture texture, UnityEngine.TextureFormat format, System.Boolean mipMaps, System.Int32 width, System.Int32 height)
  RVA=0x0A8FE300  token=0x600031D  System.Void SaveBytes(System.String saveName, System.Byte[] data, System.Boolean save)
  RVA=0x0A8FDB7C  token=0x600031E  System.Byte[] LoadBytes(System.String saveName)
  RVA=0x0A8FE37C  token=0x600031F  System.Boolean SaveExists(System.String saveName)
  RVA=0x0A8FBEF4  token=0x6000320  System.Void ClearSave(System.String saveName, System.Boolean save)
  RVA=0x0A8FCBA4  token=0x6000321  UnityEngine.Vector3 GetPosition(UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition)
  RVA=0x0A8FCC28  token=0x6000322  UnityEngine.Vector3 GetPosition(UnityEngine.Vector3 positionA, UnityEngine.Vector3 positionB, UnityEngine.Vector3 positionC)
  RVA=0x0A8FCCDC  token=0x6000323  UnityEngine.Vector3 GetPosition(UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Vector3 position2, UnityEngine.Vector3 endPosition2)
  RVA=0x0A8FD194  token=0x6000324  System.Single GetRadius(UnityEngine.Vector3 size)
  RVA=0x0A8FCEDC  token=0x6000325  System.Single GetRadius(UnityEngine.Vector3 size, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition)
  RVA=0x0A8FCFD0  token=0x6000326  System.Single GetRadius(UnityEngine.Vector3 size, UnityEngine.Vector3 positionA, UnityEngine.Vector3 positionB, UnityEngine.Vector3 positionC)
  RVA=0x0A8FD1CC  token=0x6000327  System.Single GetRadius(UnityEngine.Vector3 size, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Vector3 position2, UnityEngine.Vector3 endPosition2)
  RVA=0x0A8FE384  token=0x6000328  UnityEngine.Vector3 ScaleAspect(UnityEngine.Vector3 size, System.Single aspect)
  RVA=0x0A8FC5AC  token=0x6000329  System.Single GetAspect(UnityEngine.Texture textureA, UnityEngine.Texture textureB)
  RVA=0x0A8FBCDC  token=0x600032A  System.Void Blit(UnityEngine.RenderTexture renderTexture, UnityEngine.Texture other)
  RVA=0x0A8FBD4C  token=0x600032B  System.Void Blit(UnityEngine.RenderTexture renderTexture, UnityEngine.Material material, System.Int32 pass)
  RVA=0x0A8FDB24  token=0x600032C  UnityEngine.Vector4 IndexToVector(System.Int32 index)
  RVA=0x0A8FC494  token=0x600032D  System.Void Draw(UnityEngine.Material material, System.Int32 pass, UnityEngine.Mesh mesh, UnityEngine.Matrix4x4 matrix, System.Int32 subMesh, PaintIn3D.P3dCoord coord)
  RVA=0x0A8FC394  token=0x600032E  System.Void Draw(UnityEngine.Material material, System.Int32 pass)
  RVA=0x0A8FC070  token=0x600032F  UnityEngine.Texture2D CreateTexture(System.Int32 width, System.Int32 height, UnityEngine.TextureFormat format, System.Boolean mipMaps)
  RVA=0x0A8FC748  token=0x6000330  UnityEngine.Material GetMaterial(UnityEngine.Renderer renderer, System.Int32 materialIndex)
  RVA=0x0A8FBF30  token=0x6000331  UnityEngine.Material CloneMaterial(UnityEngine.GameObject gameObject, System.Int32 materialIndex)
  RVA=0x0A8FBB78  token=0x6000332  UnityEngine.Material AddMaterial(UnityEngine.Renderer renderer, UnityEngine.Shader shader, System.Int32 materialIndex)
  RVA=0x0A8FDBDC  token=0x6000333  UnityEngine.Shader LoadShader(System.String shaderName)
  RVA=0x0A8FBDD4  token=0x6000334  UnityEngine.Material BuildMaterial(UnityEngine.Shader shader)
  RVA=0x0A8FBE34  token=0x6000335  UnityEngine.Material BuildMaterial(System.String shaderName, System.String keyword)
END_CLASS

CLASS: PaintIn3D.P3dCoord
TYPE:  sealed struct
TOKEN: 0x2000070
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  PaintIn3D.P3dCoord              First  // const
  public    static  PaintIn3D.P3dCoord              Second  // const
  public    static  PaintIn3D.P3dCoord              Third  // const
  public    static  PaintIn3D.P3dCoord              Fourth  // const
METHODS:
END_CLASS

CLASS: PaintIn3D.P3dCoordCopier
TYPE:  class
TOKEN: 0x2000071
SIZE:  0x38
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  private           UnityEngine.Mesh                source  // 0x18
  private           PaintIn3D.P3dCoordCopier.Coord  first  // 0x20
  private           PaintIn3D.P3dCoordCopier.Coord  second  // 0x24
  private           PaintIn3D.P3dCoordCopier.Coord  third  // 0x28
  private           PaintIn3D.P3dCoordCopier.Coord  fourth  // 0x2c
  private           UnityEngine.Mesh                mesh  // 0x30
  private   static  System.Collections.Generic.List<UnityEngine.BoneWeight>boneWeights  // static @ 0x0
  private   static  System.Collections.Generic.List<UnityEngine.Color32>colors  // static @ 0x8
  private   static  System.Collections.Generic.List<UnityEngine.Vector3>positions  // static @ 0x10
  private   static  System.Collections.Generic.List<UnityEngine.Vector3>normals  // static @ 0x18
  private   static  System.Collections.Generic.List<UnityEngine.Vector4>tangents  // static @ 0x20
  private   static  System.Collections.Generic.List<UnityEngine.Vector4>coords0  // static @ 0x28
  private   static  System.Collections.Generic.List<UnityEngine.Vector4>coords1  // static @ 0x30
  private   static  System.Collections.Generic.List<UnityEngine.Vector4>coords2  // static @ 0x38
  private   static  System.Collections.Generic.List<UnityEngine.Vector4>coords3  // static @ 0x40
  private   static  System.Collections.Generic.List<UnityEngine.Vector4>coordsNone  // static @ 0x48
  private   static  System.Collections.Generic.List<System.Int32>indices  // static @ 0x50
PROPERTIES:
  Source  get=0x0385B100  set=0x05392C40
  First  get=0x02B70F90  set=0x04D86330
  Second  get=0x011F0020  set=0x011F0030
  Third  get=0x011F2A90  set=0x011F2AA0
  Fourth  get=0x04D86710  set=0x04D86720
METHODS:
  RVA=0x0A8FECB8  token=0x6000344  System.Collections.Generic.List<UnityEngine.Vector4> GetCoords(PaintIn3D.P3dCoordCopier.Coord coord)
  RVA=0x0A8FE598  token=0x6000345  System.Void Generate()
  RVA=0x0A8FED64  token=0x6000346  System.Void OnDestroy()
  RVA=0x0A8FF1CC  token=0x6000347  System.Void .ctor()
  RVA=0x0A8FEDBC  token=0x6000348  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dGraduallyFade
TYPE:  class
TOKEN: 0x2000073
SIZE:  0x98
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           PaintIn3D.P3dPaintableTexture   paintableTexture  // 0x18
  private           System.Single                   threshold  // 0x20
  private           System.Single                   speed  // 0x24
  private           PaintIn3D.P3dBlendMode          blendMode  // 0x28
  private           UnityEngine.Texture             blendTexture  // 0x60
  private           PaintIn3D.P3dPaintableTexture   blendPaintableTexture  // 0x68
  private           UnityEngine.Color               blendColor  // 0x70
  private           UnityEngine.Texture             maskTexture  // 0x80
  private           PaintIn3D.P3dPaintableTexture   maskPaintableTexture  // 0x88
  private           PaintIn3D.P3dChannel            maskChannel  // 0x90
  private           System.Single                   counter  // 0x94
PROPERTIES:
  PaintableTexture  get=0x0385B100  set=0x05392C40
  Threshold  get=0x011EC8A0  set=0x011EC9E0
  Speed  get=0x04D89850  set=0x04D89860
  BlendMode  get=0x04DA61A0  set=0x0A8FF640
  BlendTexture  get=0x04D86280  set=0x063CC164
  BlendPaintableTexture  get=0x04D86230  set=0x0485F170
  BlendColor  get=0x04D8FD40  set=0x04DA1310
  MaskTexture  get=0x04D86260  set=0x04CD9970
  MaskPaintableTexture  get=0x04D861F0  set=0x05395D30
  MaskChannel  get=0x04D86B60  set=0x04D86BC0
METHODS:
  RVA=0x0A8FF1F0  token=0x600035D  System.Void Update()
  RVA=0x0A8FF57C  token=0x600035E  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dGroup
TYPE:  sealed struct
TOKEN: 0x2000074
SIZE:  0x14
FIELDS:
  private           System.Int32                    index  // 0x10
METHODS:
  RVA=0x04D86140  token=0x600035F  System.Void .ctor(System.Int32 newIndex)
  RVA=0x04D88BE0  token=0x6000360  System.Int32 op_Implicit(PaintIn3D.P3dGroup group)
  RVA=0x04D88BE0  token=0x6000361  PaintIn3D.P3dGroup op_Implicit(System.Int32 index)
  RVA=0x0A8FFBF8  token=0x6000362  System.String ToString()
END_CLASS

CLASS: PaintIn3D.P3dGroupData
TYPE:  class
TOKEN: 0x2000075
SIZE:  0x38
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  private           System.Int32                    index  // 0x18
  private           System.Collections.Generic.List<PaintIn3D.P3dGroupData.TextureData>textureDatas  // 0x20
  private           System.String                   shaderData  // 0x28
  private           System.Collections.Generic.List<PaintIn3D.P3dGroupData.Entry>entries  // 0x30
PROPERTIES:
  Index  get=0x011EF5B0  set=0x011EF9B0
  TextureDatas  get=0x0A8FFB74
  ShaderData  get=0x04D86240  set=0x02FDB880
METHODS:
  RVA=0x0A8FF75C  token=0x6000368  System.Void TryGetShaderSlotName(System.String shaderPath, System.String& propertyName)
  RVA=0x0A8FF6E0  token=0x6000369  System.Boolean Supports(UnityEngine.Shader shader)
  RVA=0x0A8FF66C  token=0x600036A  System.String GetName(System.Boolean prefixNumber)
  RVA=0x0A8FFAF4  token=0x600036B  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dHash
TYPE:  sealed struct
TOKEN: 0x2000078
SIZE:  0x14
FIELDS:
  private           System.Int32                    v  // 0x10
METHODS:
  RVA=0x0A8FFC00  token=0x600036E  System.Boolean Equals(System.Object obj)
  RVA=0x02B77620  token=0x600036F  System.Int32 GetHashCode()
  RVA=0x04D86140  token=0x6000370  System.Void .ctor(System.Int32 newValue)
  RVA=0x04D88BE0  token=0x6000371  System.Int32 op_Implicit(PaintIn3D.P3dHash hash)
  RVA=0x04D88BE0  token=0x6000372  PaintIn3D.P3dHash op_Implicit(System.Int32 index)
  RVA=0x0A8FFBF8  token=0x6000373  System.String ToString()
END_CLASS

CLASS: PaintIn3D.P3dHashedMaterial
TYPE:  sealed struct
TOKEN: 0x2000079
SIZE:  0x20
FIELDS:
  private           UnityEngine.Material            instance  // 0x10
  private           System.Int32                    hash  // 0x18
METHODS:
  RVA=0x053955EC  token=0x6000374  System.Void .ctor(UnityEngine.Material newInstance, System.Int32 newHash)
  RVA=0x0A8FFC5C  token=0x6000375  System.Boolean TryGetInstance(UnityEngine.Material& model)
END_CLASS

CLASS: PaintIn3D.P3dHashedModel
TYPE:  sealed struct
TOKEN: 0x200007A
SIZE:  0x20
FIELDS:
  private           PaintIn3D.P3dModel              instance  // 0x10
  private           PaintIn3D.P3dHash               hash  // 0x18
METHODS:
  RVA=0x0A8FFE1C  token=0x6000376  PaintIn3D.P3dHashedModel op_Implicit(PaintIn3D.P3dModel newInstance)
  RVA=0x0A8FFD24  token=0x6000377  System.Boolean TryGetInstance(PaintIn3D.P3dModel& model)
END_CLASS

CLASS: PaintIn3D.P3dHashedTexture
TYPE:  sealed struct
TOKEN: 0x200007B
SIZE:  0x20
FIELDS:
  private           UnityEngine.Texture             instance  // 0x10
  private           PaintIn3D.P3dHash               hash  // 0x18
METHODS:
  RVA=0x0A8FFF9C  token=0x6000378  PaintIn3D.P3dHashedTexture op_Implicit(UnityEngine.Texture newInstance)
  RVA=0x0A8FFF78  token=0x6000379  UnityEngine.Texture op_Implicit(PaintIn3D.P3dHashedTexture hashed)
  RVA=0x0A8FFEB0  token=0x600037A  System.Boolean TryGetInstance(UnityEngine.Texture& texture)
END_CLASS

CLASS: PaintIn3D.P3dHit
TYPE:  sealed struct
TOKEN: 0x200007C
SIZE:  0x28
FIELDS:
  public            UnityEngine.Transform           Root  // 0x10
  public            UnityEngine.Vector2             First  // 0x18
  public            UnityEngine.Vector2             Second  // 0x20
METHODS:
  RVA=0x0A900060  token=0x600037B  System.Void .ctor(UnityEngine.RaycastHit hit)
END_CLASS

CLASS: PaintIn3D.P3dLineConnector
TYPE:  class
TOKEN: 0x200007D
SIZE:  0x30
FIELDS:
  private           System.Single                   hitSpacing  // 0x10
  private           System.Int32                    hitLimit  // 0x14
  protected         System.Boolean                  connectHits  // 0x18
  protected         System.Boolean                  clipConnected  // 0x19
  private           System.Collections.Generic.List<PaintIn3D.P3dLineConnector.Link>links  // 0x20
  private   static  System.Collections.Generic.Stack<PaintIn3D.P3dLineConnector.Link>linkPool  // static @ 0x0
  protected         PaintIn3D.P3dHitCache           hitCache  // 0x28
PROPERTIES:
  HitSpacing  get=0x04D85F70  set=0x04D88280
  HitLimit  get=0x04D882A0  set=0x04D8F540
  ConnectHits  get=0x04D86340  set=0x04D86350
  ClipConnected  get=0x04D866A0  set=0x04D866C0
  HitCache  get=0x04D86240
METHODS:
  RVA=0x0A9001FC  token=0x6000385  System.Void ClearHitCache()
  RVA=0x0A90021C  token=0x6000386  System.Void ResetConnections()
  RVA=0x0A9000F0  token=0x6000387  System.Void BreakHits(System.Object owner)
  RVA=0x0A900304  token=0x6000388  System.Void SubmitLine(UnityEngine.GameObject gameObject, System.Boolean preview, System.Int32 priority, System.Single pressure, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Quaternion rotation, System.Object owner)
  RVA=0x0A900B20  token=0x6000389  System.Void Update()
  RVA=0x0A900A6C  token=0x600038A  System.Boolean TryGetLink(System.Object owner, PaintIn3D.P3dLineConnector.Link& link)
  RVA=0x0A900CDC  token=0x600038B  System.Void .ctor()
  RVA=0x0A900C50  token=0x600038C  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dMask
TYPE:  class
TOKEN: 0x200007F
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.Texture             texture  // 0x18
  private           PaintIn3D.P3dChannel            channel  // 0x20
  private           UnityEngine.Vector2             stretch  // 0x24
  private   static  System.Collections.Generic.LinkedList<PaintIn3D.P3dMask>instances  // static @ 0x0
  private           System.Collections.Generic.LinkedListNode<PaintIn3D.P3dMask>instancesNode  // 0x30
PROPERTIES:
  Texture  get=0x0385B100  set=0x05392C40
  Channel  get=0x02B70F90  set=0x04D86330
  Stretch  get=0x04D88060  set=0x04DA5EC0
  Instances  get=0x0A9011B8
  Matrix  get=0x0A901208
METHODS:
  RVA=0x0A900D90  token=0x6000396  PaintIn3D.P3dMask Find(UnityEngine.Vector3 position, UnityEngine.LayerMask layers)
  RVA=0x0A901094  token=0x6000397  System.Void OnEnable()
  RVA=0x0A901018  token=0x6000398  System.Void OnDisable()
  RVA=0x0A90118C  token=0x6000399  System.Void .ctor()
  RVA=0x0A901110  token=0x600039A  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dMaterialCloner
TYPE:  class
TOKEN: 0x2000080
SIZE:  0x50
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Int32                    index  // 0x18
  private           UnityEngine.Renderer            cachedRenderer  // 0x20
  private           System.Boolean                  cachedRendererSet  // 0x28
  private           PaintIn3D.P3dPaintable          cachedPaintable  // 0x30
  private           System.Boolean                  cachedPaintableSet  // 0x38
  private           System.Boolean                  activated  // 0x39
  private           UnityEngine.Material            current  // 0x40
  private           UnityEngine.Material            original  // 0x48
PROPERTIES:
  Index  get=0x011EF5B0  set=0x011EF9B0
  CachedRenderer  get=0x0A901684
  CachedPaintable  get=0x0A90162C
  Original  get=0x04D86200
  Current  get=0x04D85A60
  Activated  get=0x04D86650
METHODS:
  RVA=0x0A901260  token=0x60003A2  System.Void Activate()
  RVA=0x0A901374  token=0x60003A3  System.Void Deactivate()
  RVA=0x0A9013FC  token=0x60003A4  System.Void ReplaceAll(UnityEngine.Material from, UnityEngine.Material to)
  RVA=0x0A90154C  token=0x60003A5  System.Void Replace(UnityEngine.Renderer renderer, UnityEngine.Material[] materials, UnityEngine.Material oldMaterial, UnityEngine.Material newMaterial)
  RVA=0x05393520  token=0x60003A6  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dMeshCache
TYPE:  static class
TOKEN: 0x2000081
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.Dictionary<UnityEngine.Mesh,PaintIn3D.P3dMeshCache.MeshData>cachedData  // static @ 0x0
METHODS:
  RVA=0x0A9016DC  token=0x60003A7  System.Boolean GetTrianglePositions(UnityEngine.RaycastHit hit, UnityEngine.Vector3& positionA, UnityEngine.Vector3& positionB, UnityEngine.Vector3& positionC)
  RVA=0x0A9018F4  token=0x60003A8  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dModel
TYPE:  class
TOKEN: 0x2000083
SIZE:  0xF8
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  protected         PaintIn3D.P3dPaintable          paintable  // 0x18
  protected         System.Boolean                  includeScale  // 0x20
  protected         PaintIn3D.P3dModel.UseMeshType  useMesh  // 0x24
  protected         PaintIn3D.P3dHash               hash  // 0x28
  private           UnityEngine.Renderer            cachedRenderer  // 0x30
  private           System.Boolean                  cachedRendererSet  // 0x38
  private           UnityEngine.SkinnedMeshRenderer cachedSkinned  // 0x40
  private           UnityEngine.MeshFilter          cachedFilter  // 0x48
  private           System.Boolean                  cachedSkinnedSet  // 0x50
  private           UnityEngine.Transform           cachedTransform  // 0x58
  private           UnityEngine.GameObject          cachedGameObject  // 0x60
  private           UnityEngine.Material[]          materials  // 0x68
  private           System.Boolean                  materialsSet  // 0x70
  private           UnityEngine.Mesh                bakedMesh  // 0x78
  private           System.Boolean                  bakedMeshSet  // 0x80
  protected         System.Boolean                  prepared  // 0x81
  private           UnityEngine.Mesh                preparedMesh  // 0x88
  private           UnityEngine.Matrix4x4           preparedMatrix  // 0x90
  private           System.Int32[]                  preparedTriangles  // 0xd0
  private           UnityEngine.Vector3[]           preparedPositions  // 0xd8
  private           UnityEngine.Vector2[]           preparedCoord0  // 0xe0
  private           UnityEngine.Vector2[]           preparedCoord1  // 0xe8
  private   static  System.Collections.Generic.List<PaintIn3D.P3dModel>tempModels  // static @ 0x0
  private   static  System.Collections.Generic.LinkedList<PaintIn3D.P3dModel>instances  // static @ 0x8
  private           System.Collections.Generic.LinkedListNode<PaintIn3D.P3dModel>instancesNode  // 0xf0
PROPERTIES:
  Paintable  get=0x0385B100  set=0x05392C40
  IncludeScale  get=0x04D866B0  set=0x04D866D0
  UseMesh  get=0x011F0020  set=0x011F0030
  Hash  get=0x011F2A90  set=0x0A902D2C
  Instances  get=0x0A902C94
  Prepared  get=0x04D8FEE0  set=0x04D8FEF0
  PreparedMesh  get=0x04D861F0
  CachedGameObject  get=0x04D86280
  CachedRenderer  get=0x0A902C6C
  Materials  get=0x0A902CE4
METHODS:
  RVA=0x0A901DF8  token=0x60003BB  System.Int32 GetMaterialIndex(UnityEngine.Material material)
  RVA=0x0A901980  token=0x60003BC  System.Void CacheRenderer()
  RVA=0x04DA61D0  token=0x60003BD  System.Void DirtyMaterials()
  RVA=0x0A901A54  token=0x60003BE  System.Collections.Generic.List<PaintIn3D.P3dModel> FindOverlap(UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask)
  RVA=0x0A9022D8  token=0x60003BF  System.Void GetPreparedPoints(System.Int32 triangleIndex, UnityEngine.Vector3& pointA, UnityEngine.Vector3& pointB, UnityEngine.Vector3& pointC)
  RVA=0x0A901F48  token=0x60003C0  System.Void GetPreparedCoords0(System.Int32 triangleIndex, UnityEngine.Vector2& coordA, UnityEngine.Vector2& coordB, UnityEngine.Vector2& coordC)
  RVA=0x0A902110  token=0x60003C1  System.Void GetPreparedCoords1(System.Int32 triangleIndex, UnityEngine.Vector2& coordA, UnityEngine.Vector2& coordB, UnityEngine.Vector2& coordC)
  RVA=0x0A9028F8  token=0x60003C2  System.Int32 GetSubmesh(PaintIn3D.P3dPaintableTexture paintableTexture)
  RVA=0x0A9024D0  token=0x60003C3  System.Void GetPrepared(UnityEngine.Mesh& mesh, UnityEngine.Matrix4x4& matrix, PaintIn3D.P3dCoord coord)
  RVA=0x0A902AAC  token=0x60003C4  System.Void OnEnable()
  RVA=0x0A902A24  token=0x60003C5  System.Void OnDisable()
  RVA=0x0A9029A0  token=0x60003C6  System.Void OnDestroy()
  RVA=0x04D84C40  token=0x60003C7  System.Void .ctor()
  RVA=0x0A902B8C  token=0x60003C8  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dModifier
TYPE:  abstract class
TOKEN: 0x2000085
SIZE:  0x18
FIELDS:
  private           System.Boolean                  preview  // 0x10
  private           System.Boolean                  unique  // 0x11
PROPERTIES:
  Preview  get=0x015EFCE0  set=0x04D86130
  Unique  get=0x04D88380  set=0x04D88460
METHODS:
  RVA=0x0A903844  token=0x60003CD  System.Void ModifyAngle(System.Single& angle, System.Single pressure)
  RVA=0x041E1670  token=0x60003CE  System.Void OnModifyAngle(System.Single& angle, System.Single pressure)
  RVA=0x0A9038D0  token=0x60003CF  System.Void ModifyColor(UnityEngine.Color& color, System.Single pressure)
  RVA=0x041E1670  token=0x60003D0  System.Void OnModifyColor(UnityEngine.Color& color, System.Single pressure)
  RVA=0x0A903954  token=0x60003D1  System.Void ModifyHardness(System.Single& hardness, System.Single pressure)
  RVA=0x041E1670  token=0x60003D2  System.Void OnModifyHardness(System.Single& hardness, System.Single pressure)
  RVA=0x0A9039E0  token=0x60003D3  System.Void ModifyOpacity(System.Single& opacity, System.Single pressure)
  RVA=0x041E1670  token=0x60003D4  System.Void OnModifyOpacity(System.Single& opacity, System.Single pressure)
  RVA=0x0A903AF8  token=0x60003D5  System.Void ModifyRadius(System.Single& radius, System.Single pressure)
  RVA=0x041E1670  token=0x60003D6  System.Void OnModifyRadius(System.Single& radius, System.Single pressure)
  RVA=0x0A903B84  token=0x60003D7  System.Void ModifyScale(UnityEngine.Vector3& scale, System.Single pressure)
  RVA=0x041E1670  token=0x60003D8  System.Void OnModifyScale(UnityEngine.Vector3& scale, System.Single pressure)
  RVA=0x0A903C10  token=0x60003D9  System.Void ModifyTexture(UnityEngine.Texture& texture, System.Single pressure)
  RVA=0x041E1670  token=0x60003DA  System.Void OnModifyTexture(UnityEngine.Texture& texture, System.Single pressure)
  RVA=0x0A903A6C  token=0x60003DB  System.Void ModifyPosition(UnityEngine.Vector3& position, System.Single pressure)
  RVA=0x041E1670  token=0x60003DC  System.Void OnModifyPosition(UnityEngine.Vector3& position, System.Single pressure)
  RVA=0x04DA60F0  token=0x60003DD  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dModifierList
TYPE:  class
TOKEN: 0x2000086
SIZE:  0x18
FIELDS:
  private           System.Collections.Generic.List<PaintIn3D.P3dModifier>modifiers  // 0x10
PROPERTIES:
  Count  get=0x0A903780
  Instances  get=0x0A9037C0
METHODS:
  RVA=0x0A902D80  token=0x60003E0  System.Void ModifyAngle(System.Single& angle, System.Boolean preview, System.Single pressure)
  RVA=0x0A902EC0  token=0x60003E1  System.Void ModifyColor(UnityEngine.Color& color, System.Boolean preview, System.Single pressure)
  RVA=0x0A903000  token=0x60003E2  System.Void ModifyHardness(System.Single& hardness, System.Boolean preview, System.Single pressure)
  RVA=0x0A903140  token=0x60003E3  System.Void ModifyOpacity(System.Single& opacity, System.Boolean preview, System.Single pressure)
  RVA=0x0A9033C0  token=0x60003E4  System.Void ModifyRadius(System.Single& radius, System.Boolean preview, System.Single pressure)
  RVA=0x0A903500  token=0x60003E5  System.Void ModifyScale(UnityEngine.Vector3& scale, System.Boolean preview, System.Single pressure)
  RVA=0x0A903640  token=0x60003E6  System.Void ModifyTexture(UnityEngine.Texture& texture, System.Boolean preview, System.Single pressure)
  RVA=0x0A903280  token=0x60003E7  System.Void ModifyPosition(UnityEngine.Vector3& position, System.Boolean preview, System.Single pressure)
  RVA=0x041E1670  token=0x60003E8  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dPaintable
TYPE:  class
TOKEN: 0x2000087
SIZE:  0x138
EXTENDS: PaintIn3D.P3dModel
FIELDS:
  private           PaintIn3D.P3dPaintable.ActivationTypeactivation  // 0xf8
  private           UnityEngine.Vector3             baseScale  // 0xfc
  private           System.Collections.Generic.List<UnityEngine.Renderer>otherRenderers  // 0x108
  private           UnityEngine.Events.UnityEvent   onActivating  // 0x110
  private           UnityEngine.Events.UnityEvent   onActivated  // 0x118
  private           System.Boolean                  activated  // 0x120
  private           System.Collections.Generic.HashSet<PaintIn3D.P3dPaintableTexture>paintableTextures  // 0x128
  private   static  System.Collections.Generic.List<PaintIn3D.P3dMaterialCloner>materialCloners  // static @ 0x0
  private           System.Collections.Generic.List<PaintIn3D.P3dPaintableTexture>tempPaintableTextures  // 0x130
PROPERTIES:
  Paintable  get=0x0A90A960  set=0x041E1670
  Activation  get=0x04DA61F0  set=0x04DA6220
  BaseScale  get=0x04DA6200  set=0x04DA6230
  OtherRenderers  get=0x04D85F90  set=0x068C2E50
  OnActivating  get=0x0A90A8E4
  OnActivated  get=0x0A90A868
  Activated  get=0x04D8FE70
  PaintableTextures  get=0x04D8D910
METHODS:
  RVA=0x0A90A3F8  token=0x60003F5  System.Void RemoveComponents()
  RVA=0x0A90A560  token=0x60003F6  System.Void ScaleSize(System.Int32& width, System.Int32& height)
  RVA=0x0A9099C4  token=0x60003F7  System.Void Activate()
  RVA=0x0A909C28  token=0x60003F8  System.Void AddPaintableTextures(UnityEngine.Transform root)
  RVA=0x0A90A014  token=0x60003F9  System.Void Deactivate()
  RVA=0x0A909E8C  token=0x60003FA  System.Void ClearAll(UnityEngine.Color color)
  RVA=0x0A909EAC  token=0x60003FB  System.Void ClearAll(UnityEngine.Texture texture, UnityEngine.Color color)
  RVA=0x0A90A3A0  token=0x60003FC  System.Void Register(PaintIn3D.P3dPaintableTexture paintableTexture)
  RVA=0x0A90A68C  token=0x60003FD  System.Void Unregister(PaintIn3D.P3dPaintableTexture paintableTexture)
  RVA=0x0A909E70  token=0x60003FE  System.Void Awake()
  RVA=0x0A90A33C  token=0x60003FF  System.Void OnEnable()
  RVA=0x0A90A670  token=0x6000400  System.Void Start()
  RVA=0x0A90A770  token=0x6000401  System.Void .ctor()
  RVA=0x0A90A6E4  token=0x6000402  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dPaintableManager
TYPE:  class
TOKEN: 0x2000089
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  System.Collections.Generic.LinkedList<PaintIn3D.P3dPaintableManager>instances  // static @ 0x0
  private           System.Collections.Generic.LinkedListNode<PaintIn3D.P3dPaintableManager>instancesNode  // 0x18
PROPERTIES:
  Instances  get=0x0A904920
METHODS:
  RVA=0x0A903F4C  token=0x6000404  PaintIn3D.P3dPaintableManager GetOrCreateInstance()
  RVA=0x0A9043CC  token=0x6000405  System.Void SubmitAll(PaintIn3D.P3dCommand command, UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask, PaintIn3D.P3dGroup group, PaintIn3D.P3dModel targetModel, PaintIn3D.P3dPaintableTexture targetTexture)
  RVA=0x0A903DD0  token=0x6000406  System.Void DoSubmitAll(PaintIn3D.P3dCommand command, UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask, PaintIn3D.P3dGroup group, PaintIn3D.P3dModel targetModel, PaintIn3D.P3dPaintableTexture targetTexture)
  RVA=0x0A9042D0  token=0x6000407  System.Void SubmitAll(PaintIn3D.P3dCommand command, UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask, PaintIn3D.P3dGroup group)
  RVA=0x0A9045D0  token=0x6000408  System.Void SubmitAll(PaintIn3D.P3dCommand command, PaintIn3D.P3dModel model, PaintIn3D.P3dGroup group)
  RVA=0x0A9046B0  token=0x6000409  PaintIn3D.P3dCommand Submit(PaintIn3D.P3dCommand command, PaintIn3D.P3dModel model, PaintIn3D.P3dPaintableTexture paintableTexture)
  RVA=0x0A904254  token=0x600040A  System.Void OnEnable()
  RVA=0x0A9041D8  token=0x600040B  System.Void OnDisable()
  RVA=0x0A904098  token=0x600040C  System.Void LateUpdate()
  RVA=0x0A903C94  token=0x600040D  System.Void ClearAll()
  RVA=0x0A904760  token=0x600040E  System.Void UpdateAll()
  RVA=0x05393520  token=0x600040F  System.Void .ctor()
  RVA=0x0A9048A4  token=0x6000410  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dPaintableState
TYPE:  class
TOKEN: 0x200008A
SIZE:  0x20
FIELDS:
  public            UnityEngine.RenderTexture       Texture  // 0x10
  public            System.Collections.Generic.List<PaintIn3D.P3dCommand>Commands  // 0x18
  private   static  System.Collections.Generic.Stack<PaintIn3D.P3dPaintableState>pool  // static @ 0x0
METHODS:
  RVA=0x0A904AE8  token=0x6000411  PaintIn3D.P3dPaintableState Pop()
  RVA=0x0A904B9C  token=0x6000412  System.Void Write(UnityEngine.RenderTexture current)
  RVA=0x0A904C84  token=0x6000413  System.Void Write(System.Collections.Generic.List<PaintIn3D.P3dCommand> commands)
  RVA=0x0A904CE4  token=0x6000414  System.Void Write(UnityEngine.RenderTexture current, System.Collections.Generic.List<PaintIn3D.P3dCommand> commands)
  RVA=0x0A904970  token=0x6000415  System.Void Clear()
  RVA=0x0A904A70  token=0x6000416  System.Void Pool()
  RVA=0x0A904E8C  token=0x6000417  System.Void .ctor()
  RVA=0x0A904E00  token=0x6000418  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dPaintableTexture
TYPE:  class
TOKEN: 0x200008B
SIZE:  0x130
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           PaintIn3D.P3dSlot               slot  // 0x18
  private           PaintIn3D.P3dCoord              coord  // 0x28
  private           PaintIn3D.P3dGroup              group  // 0x2c
  private           PaintIn3D.P3dPaintableTexture.UndoRedoTypeundoRedo  // 0x30
  private           System.Int32                    stateLimit  // 0x34
  private           PaintIn3D.P3dPaintableTexture.SaveLoadTypesaveLoad  // 0x38
  private           System.String                   saveName  // 0x40
  private           System.Int32                    width  // 0x48
  private           System.Int32                    height  // 0x4c
  private           UnityEngine.Texture             texture  // 0x50
  private           UnityEngine.Color               color  // 0x58
  private           UnityEngine.RenderTextureFormat format  // 0x68
  private           PaintIn3D.P3dPaintableTexture.MipTypemipMaps  // 0x6c
  private           PaintIn3D.P3dPaintableTexture.FilterTypefilter  // 0x70
  private           PaintIn3D.P3dPaintableTexture.AnisoTypeaniso  // 0x74
  private           PaintIn3D.P3dPaintableTexture.WrapTypewrapU  // 0x78
  private           PaintIn3D.P3dPaintableTexture.WrapTypewrapV  // 0x7c
  private           PaintIn3D.P3dPaintableTexture.ExistingTypeexisting  // 0x80
  private           PaintIn3D.P3dPaintableTexture.ConversionTypeconversion  // 0x84
  private           UnityEngine.Texture             localMaskTexture  // 0x88
  private           PaintIn3D.P3dChannel            localMaskChannel  // 0x90
  private           System.String                   shaderKeyword  // 0x98
  protected         PaintIn3D.P3dHash               hash  // 0xa0
  private           System.Boolean                  isDummy  // 0xa4
  private           System.String                   output  // 0xa8
  private           System.Action<PaintIn3D.P3dCommand>OnAddCommand  // 0xb0
  private   static  System.Action<PaintIn3D.P3dPaintableTexture,PaintIn3D.P3dCommand>OnAddCommandGlobal  // static @ 0x0
  public    static  System.Action<PaintIn3D.P3dPaintableTexture>OnInstanceAdded  // static @ 0x8
  public    static  System.Action<PaintIn3D.P3dPaintableTexture>OnInstanceRemoved  // static @ 0x10
  private           System.Action<System.Boolean>   OnModified  // 0xb8
  private           System.Boolean                  activated  // 0xc0
  private           UnityEngine.RenderTexture       current  // 0xc8
  private           UnityEngine.RenderTexture       preview  // 0xd0
  private           System.Collections.Generic.List<PaintIn3D.P3dPaintableState>paintableStates  // 0xd8
  private           System.Int32                    stateIndex  // 0xe0
  private           PaintIn3D.P3dPaintable          paintable  // 0xe8
  private           System.Boolean                  paintableSet  // 0xf0
  private           UnityEngine.Material            material  // 0xf8
  private           System.Boolean                  materialSet  // 0x100
  private           UnityEngine.Texture             oldTexture  // 0x108
  private           System.Collections.Generic.List<PaintIn3D.P3dCommand>paintCommands  // 0x110
  private           System.Collections.Generic.List<PaintIn3D.P3dCommand>previewCommands  // 0x118
  private           System.Collections.Generic.List<PaintIn3D.P3dCommand>localCommands  // 0x120
  private   static  System.Collections.Generic.List<PaintIn3D.P3dPaintableTexture>tempPaintableTextures  // static @ 0x18
  private   static  System.Collections.Generic.LinkedList<PaintIn3D.P3dPaintableTexture>instances  // static @ 0x20
  private           System.Collections.Generic.LinkedListNode<PaintIn3D.P3dPaintableTexture>instancesNode  // 0x128
  private   static  System.Int32                    _Buffer  // static @ 0x28
  private   static  System.Int32                    _BufferSize  // static @ 0x2c
PROPERTIES:
  Slot  get=0x04D890C0  set=0x0A9099B4
  Coord  get=0x011F2A90  set=0x011F2AA0
  Group  get=0x04D86710  set=0x04D86720
  UndoRedo  get=0x04D864F0  set=0x04D86740
  StateLimit  get=0x04D86730  set=0x04D86760
  SaveLoad  get=0x04D863B0  set=0x04D86430
  SaveName  get=0x04D85A60  set=0x042B4AE0
  Width  get=0x04D863D0  set=0x04D86450
  Height  get=0x04D86600  set=0x04D86630
  Texture  get=0x04D86270  set=0x02FDAEA0
  Color  get=0x04D87E00  set=0x04DA61E0
  Format  get=0x04D86CF0  set=0x04D86D00
  MipMaps  get=0x04D86B50  set=0x04D86BB0
  Filter  get=0x04D868A0  set=0x04D868D0
  Aniso  get=0x04D868B0  set=0x04D868E0
  WrapU  get=0x04D863C0  set=0x04D86440
  WrapV  get=0x04D86410  set=0x04D86490
  Existing  get=0x04D863A0  set=0x04D86420
  Conversion  get=0x04D8FCE0  set=0x04D8FCF0
  LocalMaskTexture  get=0x04D861F0  set=0x05395D30
  LocalMaskChannel  get=0x04D86B60  set=0x04D86BC0
  ShaderKeyword  get=0x04D85EE0  set=0x06352D0C
  Hash  get=0x04D88CD0  set=0x0A909960
  IsDummy  get=0x04D88D60  set=0x04D88D70
  Output  get=0x04D86290  set=0x04D84CC0
  Instances  get=0x0A909614
  Activated  get=0x04D91920
  CanUndo  get=0x0A9095AC
  CanRedo  get=0x0A909554
  States  get=0x04D88EF0
  StateIndex  get=0x04D9A880
  Material  get=0x0A909664
  Paintable  get=0x0A909694
  Current  get=0x04D8D1D0  set=0x0A909908
  Preview  get=0x04D876B0
  CommandsPending  get=0x0A9095C0
EVENTS:
  OnAddCommand  add=add_OnAddCommand  remove=remove_OnAddCommand
  OnAddCommandGlobal  add=add_OnAddCommandGlobal  remove=remove_OnAddCommandGlobal
  OnModified  add=add_OnModified  remove=remove_OnModified
METHODS:
  RVA=0x0A9075B8  token=0x600045C  System.Collections.Generic.List<PaintIn3D.P3dPaintableTexture> FilterAll(PaintIn3D.P3dModel model, PaintIn3D.P3dGroup group)
  RVA=0x0A9067CC  token=0x600045D  System.Void ClearStates()
  RVA=0x0A908C44  token=0x600045E  System.Void StoreState()
  RVA=0x0A908EA0  token=0x600045F  System.Void Undo()
  RVA=0x0A908720  token=0x6000460  System.Void Redo()
  RVA=0x0A908B38  token=0x6000461  System.Void SetColor(System.String html)
  RVA=0x0A90780C  token=0x6000462  UnityEngine.Vector2 GetCoord(PaintIn3D.P3dHit& hit)
  RVA=0x0A907840  token=0x6000463  UnityEngine.Vector2 GetCoord(UnityEngine.RaycastHit& hit)
  RVA=0x0A908B4C  token=0x6000464  System.Boolean StatesContainTextureOrCommands()
  RVA=0x0A907E60  token=0x6000465  System.Boolean LastStateWithTextureOr0(System.Int32& startIndex)
  RVA=0x0A908384  token=0x6000466  System.Void RebuildFromCommands()
  RVA=0x0A90648C  token=0x6000467  System.Void AddState()
  RVA=0x0A908CD0  token=0x6000468  System.Void TrimFuture()
  RVA=0x0A908DF0  token=0x6000469  System.Void TrimPast()
  RVA=0x0A9080C0  token=0x600046A  System.Void NotifyOnModified(System.Boolean preview)
  RVA=0x0A907918  token=0x600046B  UnityEngine.Texture2D GetReadableCopy(System.Boolean convertBack)
  RVA=0x0A90786C  token=0x600046C  System.Byte[] GetPngData(System.Boolean convertBack)
  RVA=0x0A907BB4  token=0x600046D  System.Byte[] GetTgaData(System.Boolean convertBack)
  RVA=0x0A90687C  token=0x600046E  System.Void Clear()
  RVA=0x0A9068A8  token=0x600046F  System.Void Clear(UnityEngine.Texture texture, UnityEngine.Color tint, System.Boolean updateMips)
  RVA=0x0A9088BC  token=0x6000470  System.Void Replace()
  RVA=0x0A9087E8  token=0x6000471  System.Void Replace(UnityEngine.Texture texture, UnityEngine.Color tint)
  RVA=0x0A9088E0  token=0x6000472  System.Boolean Resize(System.Int32 width, System.Int32 height, System.Boolean copyContents)
  RVA=0x0A908B2C  token=0x6000473  System.Void Save()
  RVA=0x0A908AB4  token=0x6000474  System.Void Save(System.String saveName)
  RVA=0x0A9080B0  token=0x6000475  System.Void Load()
  RVA=0x0A908048  token=0x6000476  System.Void Load(System.String saveName, System.Boolean replace)
  RVA=0x0A907F2C  token=0x6000477  System.Void LoadFromData(System.Byte[] data, System.Boolean allowResize)
  RVA=0x0A907DAC  token=0x6000478  System.Void HidePreview()
  RVA=0x0A907C60  token=0x6000479  System.Void HideAllPreviews()
  RVA=0x0A90677C  token=0x600047A  System.Void ClearSave()
  RVA=0x0A906730  token=0x600047B  System.Void ClearSave(System.String saveName)
  RVA=0x0A908F98  token=0x600047C  System.Void UpdateMaterial()
  RVA=0x0A9069DC  token=0x600047D  System.Void CopySize()
  RVA=0x0A906A80  token=0x600047E  System.Void CopyTexture()
  RVA=0x0A905E38  token=0x600047F  System.Void Activate()
  RVA=0x0A906ABC  token=0x6000480  System.Void Deactivate()
  RVA=0x0A90828C  token=0x6000481  System.Void OnEnable()
  RVA=0x0A9081C8  token=0x6000482  System.Void OnDisable()
  RVA=0x0A9080DC  token=0x6000483  System.Void OnApplicationPause(System.Boolean paused)
  RVA=0x0A908110  token=0x6000484  System.Void OnDestroy()
  RVA=0x0A9062FC  token=0x6000485  System.Void AddCommand(PaintIn3D.P3dCommand command)
  RVA=0x0A9065C8  token=0x6000487  System.Void ClearCommands()
  RVA=0x0A906BD8  token=0x6000488  System.Void ExecuteCommands(System.Boolean sendNotifications, System.Boolean doSort)
  RVA=0x0A906F80  token=0x6000489  System.Void ExecuteCommands(System.Collections.Generic.List<PaintIn3D.P3dCommand> commands, System.Boolean sendNotifications, UnityEngine.RenderTexture main, UnityEngine.RenderTexture& swap)
  RVA=0x0A9091A8  token=0x600048A  System.Void .ctor()
  RVA=0x0A909068  token=0x600048B  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dPaintableTextureMonitor
TYPE:  abstract class
TOKEN: 0x2000095
SIZE:  0x60
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           PaintIn3D.P3dPaintableTexture   paintableTexture  // 0x18
  private           System.Single                   interval  // 0x20
  private           System.Boolean                  async  // 0x24
  protected         System.Int32                    downsampleSteps  // 0x28
  private           System.Action                   OnUpdated  // 0x30
  protected         PaintIn3D.P3dPaintableTexture   registeredPaintableTexture  // 0x38
  private           System.Single                   cooldown  // 0x40
  private           PaintIn3D.P3dReader             currentReader  // 0x48
  protected         Unity.Collections.NativeArray<UnityEngine.Color32>currentPixels  // 0x50
PROPERTIES:
  PaintableTexture  get=0x0385B100  set=0x0A905E14
  Interval  get=0x011EC8A0  set=0x011EC9E0
  Async  get=0x04D86870  set=0x04D86880
  DownsampleSteps  get=0x011F2A90  set=0x011F2AA0
  Registered  get=0x0A905D30
  CurrentReader  get=0x04D86200
EVENTS:
  OnUpdated  add=add_OnUpdated  remove=remove_OnUpdated
METHODS:
  RVA=0x0A905760  token=0x6000499  System.Void MarkCurrentReaderAsDirty()
  RVA=0x0A905908  token=0x600049A  System.Void Register()
  RVA=0x0A9059E0  token=0x600049B  System.Void Unregister()
  RVA=0x0A90574C  token=0x600049C  System.Void InvokeOnUpdated()
  RVA=0x0A90586C  token=0x600049D  System.Void OnEnable()
  RVA=0x0A9057D0  token=0x600049E  System.Void OnDisable()
  RVA=0x0A905770  token=0x600049F  System.Void OnDestroy()
  RVA=0x0A905AAC  token=0x60004A0  System.Void Update()
  RVA=0x0A905634  token=0x60004A1  System.Void HandleCompleteCurrent(Unity.Collections.NativeArray<UnityEngine.Color32> pixels)
  RVA=0x0A905738  token=0x60004A2  System.Void HandleModified(System.Boolean preview)
  RVA=-1  // abstract  token=0x60004A3  System.Void HandleComplete(System.Int32 boost)
  RVA=0x0A905C7C  token=0x60004A4  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dPaintableTextureMonitorMask
TYPE:  abstract class
TOKEN: 0x2000096
SIZE:  0x98
EXTENDS: PaintIn3D.P3dPaintableTextureMonitor
FIELDS:
  private           UnityEngine.Mesh                maskMesh  // 0x60
  private           System.Int32                    maskSubmesh  // 0x68
  private           UnityEngine.Texture             maskTexture  // 0x70
  private           PaintIn3D.P3dChannel            maskChannel  // 0x78
  protected         System.Int32                    total  // 0x7c
  private           PaintIn3D.P3dReader             maskReader  // 0x80
  protected         Unity.Collections.NativeArray<System.Byte>maskPixels  // 0x88
PROPERTIES:
  MaskMesh  get=0x04D86280  set=0x063CC164
  MaskSubmesh  get=0x04D86CF0  set=0x04D86D00
  MaskTexture  get=0x04D862B0  set=0x063AD760
  MaskChannel  get=0x04D863C0  set=0x04D86440
  Total  get=0x04D86410
  MaskReader  get=0x04D86260
METHODS:
  RVA=0x0A905110  token=0x60004AF  System.Void MarkMaskReaderAsDirty()
  RVA=0x0A904F04  token=0x60004B0  System.Void HandleCompleteMask(Unity.Collections.NativeArray<UnityEngine.Color32> pixels)
  RVA=0x0A905244  token=0x60004B1  System.Void OnEnable()
  RVA=0x0A9051A0  token=0x60004B2  System.Void OnDisable()
  RVA=0x0A905128  token=0x60004B3  System.Void OnDestroy()
  RVA=0x0A9052E8  token=0x60004B4  System.Void Update()
  RVA=0x0A905624  token=0x60004B5  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dPointConnector
TYPE:  class
TOKEN: 0x2000097
SIZE:  0x30
FIELDS:
  private           System.Single                   hitSpacing  // 0x10
  private           System.Int32                    hitLimit  // 0x14
  protected         System.Boolean                  connectHits  // 0x18
  protected         System.Boolean                  clipConnected  // 0x19
  private           System.Collections.Generic.List<PaintIn3D.P3dPointConnector.Link>links  // 0x20
  private   static  System.Collections.Generic.Stack<PaintIn3D.P3dPointConnector.Link>linkPool  // static @ 0x0
  protected         PaintIn3D.P3dHitCache           hitCache  // 0x28
PROPERTIES:
  HitSpacing  get=0x04D85F70  set=0x04D88280
  HitLimit  get=0x04D882A0  set=0x04D8F540
  ConnectHits  get=0x04D86340  set=0x04D86350
  ClipConnected  get=0x04D866A0  set=0x04D866C0
  HitCache  get=0x04D86240
METHODS:
  RVA=0x0A9001FC  token=0x60004BF  System.Void ClearHitCache()
  RVA=0x0A90AA8C  token=0x60004C0  System.Void ResetConnections()
  RVA=0x0A90A980  token=0x60004C1  System.Void BreakHits(System.Object owner)
  RVA=0x0A90AB74  token=0x60004C2  System.Void SubmitLastPoint(UnityEngine.GameObject gameObject, System.Boolean preview, System.Object owner)
  RVA=0x0A90ADB8  token=0x60004C3  System.Void SubmitPoint(UnityEngine.GameObject gameObject, System.Boolean preview, System.Int32 priority, System.Single pressure, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, System.Object owner)
  RVA=0x0A90B3C0  token=0x60004C4  System.Void Update()
  RVA=0x0A90B30C  token=0x60004C5  System.Boolean TryGetLink(System.Object owner, PaintIn3D.P3dPointConnector.Link& link)
  RVA=0x0A90B57C  token=0x60004C6  System.Void .ctor()
  RVA=0x0A90B4F0  token=0x60004C7  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dReadColor
TYPE:  class
TOKEN: 0x2000099
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: PaintIn3D.IHitCoord PaintIn3D.IHit
FIELDS:
  private           PaintIn3D.P3dGroup              group  // 0x18
  private           System.Boolean                  preview  // 0x1c
  private           UnityEngine.Color               color  // 0x20
  private           PaintIn3D.P3dReadColor.ColorEventonColor  // 0x30
PROPERTIES:
  Group  get=0x011EF5B0  set=0x011EF9B0
  Preview  get=0x04D86150  set=0x04D86160
  Color  get=0x04D8C200  set=0x04D8C210
  OnColor  get=0x0A90B7F8
METHODS:
  RVA=0x0A90B630  token=0x60004D0  System.Void HandleHitCoord(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, PaintIn3D.P3dHit hit, UnityEngine.Quaternion rotation)
  RVA=0x05393520  token=0x60004D1  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dReader
TYPE:  class
TOKEN: 0x200009B
SIZE:  0x58
FIELDS:
  private           UnityEngine.Rendering.AsyncGPUReadbackRequestrequest  // 0x10
  private           System.Boolean                  dirty  // 0x20
  private           System.Boolean                  requested  // 0x21
  private           UnityEngine.RenderTexture       buffer  // 0x28
  private           UnityEngine.Vector2Int          originalSize  // 0x30
  private           UnityEngine.Vector2Int          downsampledSize  // 0x38
  private           System.Int32                    downsampleSteps  // 0x40
  private           System.Int32                    downsampleBoost  // 0x44
  private           UnityEngine.Texture2D           tempTexture  // 0x48
  private           System.Action<Unity.Collections.NativeArray<UnityEngine.Color32>>OnComplete  // 0x50
PROPERTIES:
  Dirty  get=0x04D866B0
  Requested  get=0x04D86850
  OriginalSize  get=0x011F36E0
  DownsampleSteps  get=0x04D865E0
  DownsampledSize  get=0x04D85A50
  DownsampleBoost  get=0x04D86530
EVENTS:
  OnComplete  add=add_OnComplete  remove=remove_OnComplete
METHODS:
  RVA=0x04D8F6A0  token=0x60004DB  System.Void MarkAsDirty()
  RVA=0x0A90BEC8  token=0x60004DC  System.Void UpdateRequest()
  RVA=-1  // generic def  token=0x60004DD  System.Boolean NeedsUpdating(PaintIn3D.P3dReader reader, Unity.Collections.NativeArray<T> array, UnityEngine.RenderTexture texture, System.Int32 downsampleSteps)
  RVA=0x0A90BBF8  token=0x60004DE  System.Void Request(UnityEngine.RenderTexture texture, System.Int32 downsample, System.Boolean async)
  RVA=0x0A90BB58  token=0x60004DF  System.Void Release()
  RVA=0x0A90B868  token=0x60004E0  System.Void CompleteDirectly()
  RVA=0x041E1670  token=0x60004E1  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dSerialization
TYPE:  static class
TOKEN: 0x200009C
SIZE:  0x10
FIELDS:
  public    static  System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Material>HashToMaterial  // static @ 0x0
  public    static  System.Collections.Generic.Dictionary<UnityEngine.Material,System.Int32>MaterialToHash  // static @ 0x8
  public    static  System.Collections.Generic.Dictionary<PaintIn3D.P3dHash,PaintIn3D.P3dModel>HashToModel  // static @ 0x10
  public    static  System.Collections.Generic.Dictionary<PaintIn3D.P3dModel,PaintIn3D.P3dHash>ModelToHash  // static @ 0x18
  public    static  System.Collections.Generic.Dictionary<PaintIn3D.P3dHash,UnityEngine.Texture>HashToTexture  // static @ 0x20
  public    static  System.Collections.Generic.Dictionary<UnityEngine.Texture,PaintIn3D.P3dHash>TextureToHash  // static @ 0x28
  public    static  System.Collections.Generic.Dictionary<PaintIn3D.P3dHash,PaintIn3D.P3dPaintableTexture>HashToPaintableTexture  // static @ 0x30
  public    static  System.Collections.Generic.Dictionary<PaintIn3D.P3dPaintableTexture,PaintIn3D.P3dHash>PaintableTextureToHash  // static @ 0x38
METHODS:
  RVA=0x0A90FD4C  token=0x60004E2  System.Void TryRegister(PaintIn3D.P3dPaintableTexture paintableTexture, PaintIn3D.P3dHash hash)
  RVA=0x0A90F9D0  token=0x60004E3  System.Void TryRegister(PaintIn3D.P3dModel model, PaintIn3D.P3dHash hash)
  RVA=0x0A90FCD0  token=0x60004E4  System.Void TryRegister(UnityEngine.Texture texture, PaintIn3D.P3dHash hash)
  RVA=-1  // generic def  token=0x60004E5  System.Void TryRegister(T obj, PaintIn3D.P3dHash hash, System.Collections.Generic.Dictionary<PaintIn3D.P3dHash,T> hashToObj, System.Collections.Generic.Dictionary<T,PaintIn3D.P3dHash> objToHash)
  RVA=0x0A90FA58  token=0x60004E6  System.Int32 TryRegister(UnityEngine.Material material)
  RVA=0x0A90F97C  token=0x60004E7  System.Int32 GetStableStringHash(System.String s)
  RVA=0x0A90FDC8  token=0x60004E8  System.Void .cctor()
END_CLASS

CLASS: PaintIn3D.P3dSlot
TYPE:  sealed struct
TOKEN: 0x200009D
SIZE:  0x20
FIELDS:
  public            System.Int32                    Index  // 0x10
  public            System.String                   Name  // 0x18
METHODS:
  RVA=0x06B17430  token=0x60004E9  System.Void .ctor(System.Int32 newIndex, System.String newName)
  RVA=0x0A910190  token=0x60004EA  UnityEngine.Texture FindTexture(UnityEngine.GameObject gameObject)
  RVA=0x0A9102EC  token=0x60004EB  System.Boolean IsTransformed(UnityEngine.GameObject gameObject)
  RVA=0x0A910130  token=0x60004EC  System.Boolean Equals(System.Object obj)
  RVA=0x0A9102C0  token=0x60004ED  System.Int32 GetHashCode()
  RVA=0x0A91049C  token=0x60004EE  System.Boolean op_Equality(PaintIn3D.P3dSlot a, PaintIn3D.P3dSlot b)
  RVA=0x0A9104B8  token=0x60004EF  System.Boolean op_Inequality(PaintIn3D.P3dSlot a, PaintIn3D.P3dSlot b)
END_CLASS

CLASS: PaintIn3D.P3dStateManager
TYPE:  static class
TOKEN: 0x200009E
SIZE:  0x10
FIELDS:
PROPERTIES:
  CanUndo  get=0x0A910B08
  CanRedo  get=0x0A9109C4
METHODS:
  RVA=0x0A91074C  token=0x60004F2  System.Void StoreAllStates()
  RVA=0x0A9104D4  token=0x60004F3  System.Void ClearAllStates()
  RVA=0x0A910888  token=0x60004F4  System.Void UndoAll()
  RVA=0x0A910610  token=0x60004F5  System.Void RedoAll()
END_CLASS

CLASS: PaintIn3D.P3dTextureHash
TYPE:  class
TOKEN: 0x200009F
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.Texture             texture  // 0x18
  private           PaintIn3D.P3dHash               hash  // 0x20
PROPERTIES:
  Texture  get=0x0385B100  set=0x05392C40
  Hash  get=0x02B70F90  set=0x0A910CF0
METHODS:
  RVA=0x0A910C9C  token=0x60004FA  System.Void OnEnable()
  RVA=0x0A910C4C  token=0x60004FB  System.Void OnDestroy()
  RVA=0x05393520  token=0x60004FC  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dPaintDecal
TYPE:  class
TOKEN: 0x20000A0
SIZE:  0xE0
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: PaintIn3D.IHitPoint PaintIn3D.IHit PaintIn3D.IHitLine PaintIn3D.IHitTriangle PaintIn3D.IHitQuad PaintIn3D.IHitCoord
FIELDS:
  private           UnityEngine.LayerMask           layers  // 0x18
  private           PaintIn3D.P3dModel              targetModel  // 0x20
  private           PaintIn3D.P3dGroup              group  // 0x28
  private           PaintIn3D.P3dPaintableTexture   targetTexture  // 0x30
  private           PaintIn3D.P3dBlendMode          blendMode  // 0x38
  private           UnityEngine.Texture             texture  // 0x70
  private           UnityEngine.Texture             shape  // 0x78
  private           PaintIn3D.P3dChannel            shapeChannel  // 0x80
  private           UnityEngine.Color               color  // 0x84
  private           System.Single                   opacity  // 0x94
  private           System.Single                   angle  // 0x98
  private           UnityEngine.Vector3             scale  // 0x9c
  private           System.Single                   radius  // 0xa8
  private           System.Single                   hardness  // 0xac
  private           System.Single                   wrapping  // 0xb0
  private           System.Single                   normalFront  // 0xb4
  private           System.Single                   normalBack  // 0xb8
  private           System.Single                   normalFade  // 0xbc
  private           UnityEngine.Texture             tileTexture  // 0xc0
  private           UnityEngine.Transform           tileTransform  // 0xc8
  private           System.Single                   tileOpacity  // 0xd0
  private           System.Single                   tileTransition  // 0xd4
  private           PaintIn3D.P3dModifierList       modifiers  // 0xd8
PROPERTIES:
  Layers  get=0x011EF5B0  set=0x011EF9B0
  TargetModel  get=0x04D862C0  set=0x05390F40
  Group  get=0x011F2A90  set=0x011F2AA0
  TargetTexture  get=0x011F36E0  set=0x05396200
  BlendMode  get=0x04DA62B0  set=0x0A90D980
  Texture  get=0x04D862B0  set=0x063AD760
  Shape  get=0x04D862A0  set=0x06402320
  ShapeChannel  get=0x04D863A0  set=0x04D86420
  Color  get=0x04DA62E0  set=0x04DA6310
  Opacity  get=0x04D8F070  set=0x04D8F0A0
  Angle  get=0x04D8E570  set=0x04D8E590
  Scale  get=0x04D90DF0  set=0x04D90E50
  Radius  get=0x04D8E650  set=0x04D8E680
  Hardness  get=0x04D8E660  set=0x04D8E690
  Wrapping  get=0x04DA6300  set=0x04DA6330
  NormalFront  get=0x04DA32B0  set=0x04DA3350
  NormalBack  get=0x04DA32D0  set=0x04D87E30
  NormalFade  get=0x04DA62F0  set=0x04DA6320
  TileTexture  get=0x04D892F0  set=0x06AC5ACC
  TileTransform  get=0x04D8D1D0  set=0x06AC5B30
  TileOpacity  get=0x04D8E2E0  set=0x04D8E320
  TileTransition  get=0x04D8E2C0  set=0x04D8E300
  Modifiers  get=0x0A90D918
METHODS:
  RVA=0x04DA6250  token=0x600052A  System.Void FlipHorizontal()
  RVA=0x04DA6270  token=0x600052B  System.Void FlipVertical()
  RVA=0x0A90D77C  token=0x600052C  System.Void IncrementOpacity(System.Single delta)
  RVA=0x0A90D73C  token=0x600052D  System.Void IncrementAngle(System.Single degrees)
  RVA=0x0A90D7A0  token=0x600052E  System.Void MultiplyScale(System.Single multiplier)
  RVA=0x04DA6290  token=0x600052F  System.Void MultiplyHardness(System.Single multiplier)
  RVA=0x0A90CDA8  token=0x6000530  System.Void HandleHitPoint(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
  RVA=0x0A90CB40  token=0x6000531  System.Void HandleHitLine(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Quaternion rotation, System.Boolean clip)
  RVA=0x0A90D30C  token=0x6000532  System.Void HandleHitTriangle(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 positionA, UnityEngine.Vector3 positionB, UnityEngine.Vector3 positionC, UnityEngine.Quaternion rotation)
  RVA=0x0A90D010  token=0x6000533  System.Void HandleHitQuad(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Vector3 position2, UnityEngine.Vector3 endPosition2, UnityEngine.Quaternion rotation, System.Boolean clip)
  RVA=0x0A90C7C0  token=0x6000534  System.Void HandleHitCoord(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, PaintIn3D.P3dHit hit, UnityEngine.Quaternion rotation)
  RVA=0x0A90C2E8  token=0x6000535  UnityEngine.Vector3 HandleHitCommon(System.Boolean preview, System.Single pressure, System.Int32 seed, UnityEngine.Quaternion rotation)
  RVA=0x0A90D5AC  token=0x6000536  System.Void HandleMaskCommon(UnityEngine.Vector3 worldPosition)
  RVA=0x0A90D7F0  token=0x6000537  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dPaintFill
TYPE:  class
TOKEN: 0x20000A1
SIZE:  0x80
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: PaintIn3D.IHitCoord PaintIn3D.IHit
FIELDS:
  private           PaintIn3D.P3dGroup              group  // 0x18
  private           PaintIn3D.P3dBlendMode          blendMode  // 0x20
  private           UnityEngine.Texture             texture  // 0x58
  private           UnityEngine.Color               color  // 0x60
  private           System.Single                   opacity  // 0x70
  private           System.Single                   minimum  // 0x74
  private           PaintIn3D.P3dModifierList       modifiers  // 0x78
PROPERTIES:
  Group  get=0x011EF5B0  set=0x011EF9B0
  BlendMode  get=0x04DA6340  set=0x0A90DDEC
  Texture  get=0x02E56440  set=0x036E5590
  Color  get=0x04DA1280  set=0x04DA1330
  Opacity  get=0x04D90610  set=0x04D90600
  Minimum  get=0x04DA60B0  set=0x04DA60C0
  Modifiers  get=0x0A90DD90
METHODS:
  RVA=0x0A90DCB8  token=0x6000545  System.Void IncrementOpacity(System.Single delta)
  RVA=0x0A90D9AC  token=0x6000546  System.Void HandleHitCoord(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, PaintIn3D.P3dHit hit, UnityEngine.Quaternion rotation)
  RVA=0x0A90DCD4  token=0x6000547  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dPaintReplace
TYPE:  class
TOKEN: 0x20000A2
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: PaintIn3D.IHitCoord PaintIn3D.IHit
FIELDS:
  private           PaintIn3D.P3dGroup              group  // 0x18
  private           UnityEngine.Texture             texture  // 0x20
  private           UnityEngine.Color               color  // 0x28
  private           PaintIn3D.P3dModifierList       modifiers  // 0x38
PROPERTIES:
  Group  get=0x011EF5B0  set=0x011EF9B0
  Texture  get=0x04D862C0  set=0x05390F40
  Color  get=0x04D8FAD0  set=0x04D8FAE0
  Modifiers  get=0x0A90E36C
METHODS:
  RVA=0x0A90E094  token=0x600054F  System.Void HandleHitCoord(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, PaintIn3D.P3dHit hit, UnityEngine.Quaternion rotation)
  RVA=0x0A90E334  token=0x6000550  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dPaintReplaceChannels
TYPE:  class
TOKEN: 0x20000A3
SIZE:  0x80
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: PaintIn3D.IHitCoord PaintIn3D.IHit
FIELDS:
  private           PaintIn3D.P3dGroup              group  // 0x18
  private           UnityEngine.Texture             textureR  // 0x20
  private           UnityEngine.Texture             textureG  // 0x28
  private           UnityEngine.Texture             textureB  // 0x30
  private           UnityEngine.Texture             textureA  // 0x38
  private           UnityEngine.Vector4             channelR  // 0x40
  private           UnityEngine.Vector4             channelG  // 0x50
  private           UnityEngine.Vector4             channelB  // 0x60
  private           UnityEngine.Vector4             channelA  // 0x70
PROPERTIES:
  Group  get=0x011EF5B0  set=0x011EF9B0
  TextureR  get=0x04D862C0  set=0x05390F40
  TextureG  get=0x04D86240  set=0x02FDB880
  TextureB  get=0x011F36E0  set=0x05396200
  TextureA  get=0x04D85A50  set=0x046A91B0
  ChannelR  get=0x04D905C0  set=0x04D926F0
  ChannelG  get=0x04D8CE10  set=0x04D926F0
  ChannelB  get=0x04DA1280  set=0x04D926F0
  ChannelA  get=0x04D8FD40  set=0x04D926F0
METHODS:
  RVA=0x0A90DE18  token=0x6000563  System.Void HandleHitCoord(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, PaintIn3D.P3dHit hit, UnityEngine.Quaternion rotation)
  RVA=0x0A90E064  token=0x6000564  System.Void .ctor()
END_CLASS

CLASS: PaintIn3D.P3dPaintSphere
TYPE:  class
TOKEN: 0x20000A4
SIZE:  0xB8
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: PaintIn3D.IHitPoint PaintIn3D.IHit PaintIn3D.IHitLine PaintIn3D.IHitTriangle PaintIn3D.IHitQuad PaintIn3D.IHitCoord
FIELDS:
  private           UnityEngine.LayerMask           layers  // 0x18
  private           PaintIn3D.P3dGroup              group  // 0x1c
  private           PaintIn3D.P3dModel              targetModel  // 0x20
  private           PaintIn3D.P3dPaintableTexture   targetTexture  // 0x28
  private           PaintIn3D.P3dBlendMode          blendMode  // 0x30
  private           UnityEngine.Color               color  // 0x68
  private           System.Single                   opacity  // 0x78
  private           System.Single                   angle  // 0x7c
  private           UnityEngine.Vector3             scale  // 0x80
  private           System.Single                   radius  // 0x8c
  private           System.Single                   hardness  // 0x90
  private           UnityEngine.Texture             tileTexture  // 0x98
  private           UnityEngine.Transform           tileTransform  // 0xa0
  private           System.Single                   tileOpacity  // 0xa8
  private           System.Single                   tileTransition  // 0xac
  private           PaintIn3D.P3dModifierList       modifiers  // 0xb0
PROPERTIES:
  Layers  get=0x011EF5B0  set=0x011EF9B0
  Group  get=0x04D86310  set=0x04D86320
  TargetModel  get=0x04D862C0  set=0x05390F40
  TargetTexture  get=0x04D86240  set=0x02FDB880
  BlendMode  get=0x04DA6390  set=0x0A90F950
  Color  get=0x04DA63C0  set=0x04DA63D0
  Opacity  get=0x04D8ED60  set=0x04D8ED70
  Angle  get=0x04D88E80  set=0x04D8ED80
  Scale  get=0x04D91900  set=0x04D91970
  Radius  get=0x04D8E210  set=0x04D8E230
  Hardness  get=0x04D8FD30  set=0x04D8FD60
  TileTexture  get=0x04D85EE0  set=0x06352D0C
  TileTransform  get=0x04D86210  set=0x05391334
  TileOpacity  get=0x04D8E650  set=0x04D8E680
  TileTransition  get=0x04D8E660  set=0x04D8E690
  Modifiers  get=0x0A90F8E8
METHODS:
  RVA=0x0A90F778  token=0x6000584  System.Void IncrementOpacity(System.Single delta)
  RVA=0x0A90F73C  token=0x6000585  System.Void IncrementAngle(System.Single degrees)
  RVA=0x04DA6370  token=0x6000586  System.Void MultiplyRadius(System.Single multiplier)
  RVA=0x0A90F794  token=0x6000587  System.Void MultiplyScale(System.Single multiplier)
  RVA=0x0A90EDA8  token=0x6000588  System.Void HandleHitPoint(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
  RVA=0x0A90EB40  token=0x6000589  System.Void HandleHitLine(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Quaternion rotation, System.Boolean clip)
  RVA=0x0A90F30C  token=0x600058A  System.Void HandleHitTriangle(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 positionA, UnityEngine.Vector3 positionB, UnityEngine.Vector3 positionC, UnityEngine.Quaternion rotation)
  RVA=0x0A90F010  token=0x600058B  System.Void HandleHitQuad(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, UnityEngine.Vector3 position, UnityEngine.Vector3 endPosition, UnityEngine.Vector3 position2, UnityEngine.Vector3 endPosition2, UnityEngine.Quaternion rotation, System.Boolean clip)
  RVA=0x0A90E7C0  token=0x600058C  System.Void HandleHitCoord(System.Boolean preview, System.Int32 priority, System.Single pressure, System.Int32 seed, PaintIn3D.P3dHit hit, UnityEngine.Quaternion rotation)
  RVA=0x0A90E3C8  token=0x600058D  UnityEngine.Vector3 HandleHitCommon(System.Boolean preview, System.Single pressure, System.Int32 seed, UnityEngine.Quaternion rotation)
  RVA=0x0A90F5AC  token=0x600058E  System.Void HandleMaskCommon(UnityEngine.Vector3 worldPosition)
  RVA=0x0A90F7E4  token=0x600058F  System.Void .ctor()
END_CLASS

