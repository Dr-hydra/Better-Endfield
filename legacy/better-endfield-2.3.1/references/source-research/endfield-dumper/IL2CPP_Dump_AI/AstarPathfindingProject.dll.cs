// ========================================================
// Dumped by @desirepro
// Assembly: AstarPathfindingProject.dll
// Classes:  22
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

CLASS: RVOParallelJob
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0xB0
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Int32                    agentCount  // 0x10
  public            Unity.Collections.NativeList<RVO.RVOAgentJobData>agents  // 0x18
  public            System.Int32                    obstacleCount  // 0x28
  public            System.Single                   breakingBias  // 0x2c
  public            Unity.Collections.NativeArray<System.Int32>obsBufferMaxIndex  // 0x30
  public            Unity.Collections.NativeArray<System.Int32>agentBufferMaxIndex  // 0x40
  public            Unity.Collections.NativeArray<RVO.VO>obsVoBuffer  // 0x50
  public            Unity.Collections.NativeArray<RVO.Line>agentLineBuffer  // 0x60
  public            Unity.Collections.NativeArray<System.Single>calculatedSpeeds  // 0x70
  public            Unity.Collections.NativeArray<UnityEngine.Vector2>calculatedTargetPoints  // 0x80
  public            Unity.Collections.NativeArray<UnityEngine.Vector2>overlapRecoveryVelocities  // 0x90
  public            Unity.Collections.NativeArray<System.Int32>overlapRecoveryCounts  // 0xa0
METHODS:
  RVA=0x06352FC8  token=0x60000A4  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: JobForCalcObstacleVO
TYPE:  sealed struct
TOKEN: 0x2000010
SIZE:  0x78
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Int32                    agentCount  // 0x10
  public            Unity.Collections.NativeList<RVO.RVOAgentJobData>agents  // 0x18
  public            System.Int32                    obstacleCount  // 0x28
  public            Unity.Collections.NativeList<RVO.RVOObstacleJobData>obstacles  // 0x30
  public            System.Single                   deltaTime  // 0x40
  public            Unity.Collections.NativeArray<System.Int32>obsBufferMaxIndex  // 0x48
  public            Unity.Collections.NativeArray<RVO.VO>obsVoBuffer  // 0x58
  public            Unity.Collections.NativeArray<System.Int32>steeringTargetInsideObstacleMatrix  // 0x68
METHODS:
  RVA=0x063525B4  token=0x60000A5  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: JobForCalcAgentNeighbourVO
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x98
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Int32                    agentCount  // 0x10
  public            Unity.Collections.NativeList<RVO.RVOAgentJobData>agents  // 0x18
  public            Unity.Collections.NativeList<System.Boolean>agentNeighbours  // 0x28
  public            System.Single                   deltaTime  // 0x38
  public            Unity.Collections.NativeArray<System.Int32>agentBufferMaxIndex  // 0x40
  public            Unity.Collections.NativeArray<RVO.Line>agentLineBuffer  // 0x50
  public            Unity.Collections.NativeArray<UnityEngine.Vector2>overlapRecoveryVelocities  // 0x60
  public            Unity.Collections.NativeArray<System.Int32>overlapRecoveryCounts  // 0x70
  public            Unity.Collections.NativeArray<System.Int32>overlapRecoveryActiveFlags  // 0x80
  public            System.Single                   deepOverlapRatio  // 0x90
  public            System.Single                   overlapRecoveryExitRatio  // 0x94
METHODS:
  RVA=0x06352068  token=0x60000A6  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: Node
TYPE:  sealed struct
TOKEN: 0x2000014
SIZE:  0x28
FIELDS:
  public            System.Int32                    child00  // 0x10
  public            RVO.RVOAgent                    linkedList  // 0x18
  public            System.Byte                     count  // 0x20
  public            System.Single                   maxSpeed  // 0x24
METHODS:
  RVA=0x02EE47B0  token=0x60000B0  System.Void Add(RVO.RVOAgent rvoAgent)
  RVA=0x03BFB810  token=0x60000B1  System.Void Distribute(RVO.RVOQuadtree.Node[] nodes, UnityEngine.Rect r)
  RVA=0x02EE78A0  token=0x60000B2  System.Single CalculateMaxSpeed(RVO.RVOQuadtree.Node[] nodes, System.Int32 index)
END_CLASS

CLASS: QuadtreeQuery
TYPE:  sealed struct
TOKEN: 0x2000015
SIZE:  0x38
FIELDS:
  public            UnityEngine.Vector2             p  // 0x10
  public            System.Single                   speed  // 0x18
  public            System.Single                   timeHorizon  // 0x1c
  public            System.Single                   agentRadius  // 0x20
  public            System.Single                   maxRadius  // 0x24
  public            RVO.RVOAgent                    RvoAgent  // 0x28
  public            RVO.RVOQuadtree.Node[]          nodes  // 0x30
METHODS:
  RVA=0x02EE6B80  token=0x60000B3  System.Void QueryRec(System.Int32 i, UnityEngine.Rect r)
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

CLASS: RVO.AstarMemory
TYPE:  static class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000003  T[] ShrinkArray(T[] arr, System.Int32 newLength)
  RVA=-1  // generic def  token=0x6000004  System.Void Swap(T& a, T& b)
  RVA=-1  // generic def  token=0x6000005  System.Void Realloc(Unity.Collections.NativeArray<T>& arr, System.Int32 newSize, Unity.Collections.Allocator allocator, Unity.Collections.NativeArrayOptions options)
  RVA=-1  // generic def  token=0x6000006  System.Void Realloc(T[]& arr, System.Int32 newSize)
  RVA=-1  // generic def  token=0x6000007  T[] UnsafeAppendBufferToArray(Unity.Collections.LowLevel.Unsafe.UnsafeAppendBuffer src)
END_CLASS

CLASS: RVO.NativeCollectionExtensions
TYPE:  static class
TOKEN: 0x2000005
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000008  System.Void FillIn(Unity.Collections.NativeArray<T>& array, System.Int32 start, System.Int32 length, T value)
  RVA=-1  // generic def  token=0x6000009  System.Void SetValueAt(Unity.Collections.NativeArray<T>& array, System.Int32 index, T value)
  RVA=-1  // generic def  token=0x600000A  T GetValueAt(Unity.Collections.NativeArray<T>& array, System.Int32 index)
END_CLASS

CLASS: RVO.RVOLayer
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  RVO.RVOLayer                    DefaultAgent  // const
  public    static  RVO.RVOLayer                    DefaultObstacle  // const
  public    static  RVO.RVOLayer                    Character  // const
  public    static  RVO.RVOLayer                    Enemy  // const
  public    static  RVO.RVOLayer                    NPC  // const
  public    static  RVO.RVOLayer                    FactoryObstacle  // const
  public    static  RVO.RVOLayer                    Layer6  // const
  public    static  RVO.RVOLayer                    Layer7  // const
  public    static  RVO.RVOLayer                    Layer8  // const
  public    static  RVO.RVOLayer                    Layer9  // const
  public    static  RVO.RVOLayer                    Layer10  // const
  public    static  RVO.RVOLayer                    Layer11  // const
  public    static  RVO.RVOLayer                    Layer12  // const
  public    static  RVO.RVOLayer                    Layer13  // const
  public    static  RVO.RVOLayer                    Layer14  // const
  public    static  RVO.RVOLayer                    Layer15  // const
  public    static  RVO.RVOLayer                    Layer16  // const
  public    static  RVO.RVOLayer                    Layer17  // const
  public    static  RVO.RVOLayer                    Layer18  // const
  public    static  RVO.RVOLayer                    Layer19  // const
  public    static  RVO.RVOLayer                    Layer20  // const
  public    static  RVO.RVOLayer                    Layer21  // const
  public    static  RVO.RVOLayer                    Layer22  // const
  public    static  RVO.RVOLayer                    Layer23  // const
  public    static  RVO.RVOLayer                    Layer24  // const
  public    static  RVO.RVOLayer                    Layer25  // const
  public    static  RVO.RVOLayer                    Layer26  // const
  public    static  RVO.RVOLayer                    Layer27  // const
  public    static  RVO.RVOLayer                    Layer28  // const
  public    static  RVO.RVOLayer                    Layer29  // const
  public    static  RVO.RVOLayer                    Layer30  // const
METHODS:
END_CLASS

CLASS: RVO.Side
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  RVO.Side                        Colinear  // const
  public    static  RVO.Side                        Left  // const
  public    static  RVO.Side                        Right  // const
METHODS:
END_CLASS

CLASS: RVO.VectorMath
TYPE:  static class
TOKEN: 0x2000008
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04D86030  token=0x600000B  UnityEngine.Vector2 ComplexMultiply(UnityEngine.Vector2 a, UnityEngine.Vector2 b)
  RVA=0x04D85FE0  token=0x600000C  UnityEngine.Vector2 ComplexMultiplyConjugate(UnityEngine.Vector2 a, UnityEngine.Vector2 b)
  RVA=0x06356338  token=0x600000D  UnityEngine.Vector3 ClosestPointOnLine(UnityEngine.Vector3 lineStart, UnityEngine.Vector3 lineEnd, UnityEngine.Vector3 point)
  RVA=0x063560A0  token=0x600000E  System.Single ClosestPointOnLineFactor(UnityEngine.Vector3 lineStart, UnityEngine.Vector3 lineEnd, UnityEngine.Vector3 point)
  RVA=0x06356180  token=0x600000F  System.Single ClosestPointOnLineFactor(RVO.Int3 lineStart, RVO.Int3 lineEnd, RVO.Int3 point)
  RVA=0x06356288  token=0x6000010  System.Single ClosestPointOnLineFactor(RVO.Int2 lineStart, RVO.Int2 lineEnd, RVO.Int2 point)
  RVA=0x06356644  token=0x6000011  UnityEngine.Vector3 ClosestPointOnSegment(UnityEngine.Vector3 lineStart, UnityEngine.Vector3 lineEnd, UnityEngine.Vector3 point)
  RVA=0x0635647C  token=0x6000012  UnityEngine.Vector3 ClosestPointOnSegmentXZ(UnityEngine.Vector3 lineStart, UnityEngine.Vector3 lineEnd, UnityEngine.Vector3 point)
  RVA=0x0635867C  token=0x6000013  System.Single SqrDistancePointSegmentApproximate(System.Int32 x, System.Int32 z, System.Int32 px, System.Int32 pz, System.Int32 qx, System.Int32 qz)
  RVA=0x063585A8  token=0x6000014  System.Single SqrDistancePointSegmentApproximate(RVO.Int3 a, RVO.Int3 b, RVO.Int3 p)
  RVA=0x06358750  token=0x6000015  System.Single SqrDistancePointSegment(UnityEngine.Vector3 a, UnityEngine.Vector3 b, UnityEngine.Vector3 p)
  RVA=0x063587F8  token=0x6000016  System.Single SqrDistanceSegmentSegment(UnityEngine.Vector3 s1, UnityEngine.Vector3 e1, UnityEngine.Vector3 s2, UnityEngine.Vector3 e2)
  RVA=0x06358C64  token=0x6000017  System.Single SqrDistanceXZ(UnityEngine.Vector3 a, UnityEngine.Vector3 b)
  RVA=0x04D860F0  token=0x6000018  System.Int64 SignedTriangleAreaTimes2XZ(RVO.Int3 a, RVO.Int3 b, RVO.Int3 c)
  RVA=0x04D860B0  token=0x6000019  System.Single SignedTriangleAreaTimes2XZ(UnityEngine.Vector3 a, UnityEngine.Vector3 b, UnityEngine.Vector3 c)
  RVA=0x06357BA8  token=0x600001A  System.Boolean RightXZ(UnityEngine.Vector3 a, UnityEngine.Vector3 b, UnityEngine.Vector3 p)
  RVA=0x06357BEC  token=0x600001B  System.Boolean RightXZ(RVO.Int3 a, RVO.Int3 b, RVO.Int3 p)
  RVA=0x0635856C  token=0x600001C  RVO.Side SideXZ(RVO.Int3 a, RVO.Int3 b, RVO.Int3 p)
  RVA=0x06357B4C  token=0x600001D  System.Boolean RightOrColinear(UnityEngine.Vector2 a, UnityEngine.Vector2 b, UnityEngine.Vector2 p)
  RVA=0x06357B10  token=0x600001E  System.Boolean RightOrColinear(RVO.Int2 a, RVO.Int2 b, RVO.Int2 p)
  RVA=0x06357AA0  token=0x600001F  System.Boolean RightOrColinearXZ(UnityEngine.Vector3 a, UnityEngine.Vector3 b, UnityEngine.Vector3 p)
  RVA=0x06357ADC  token=0x6000020  System.Boolean RightOrColinearXZ(RVO.Int3 a, RVO.Int3 b, RVO.Int3 p)
  RVA=0x063567A4  token=0x6000021  System.Boolean IsClockwiseMarginXZ(UnityEngine.Vector3 a, UnityEngine.Vector3 b, UnityEngine.Vector3 c)
  RVA=0x04396FA0  token=0x6000022  System.Boolean IsClockwiseXZ(UnityEngine.Vector3 a, UnityEngine.Vector3 b, UnityEngine.Vector3 c)
  RVA=0x063568A8  token=0x6000023  System.Boolean IsClockwiseXZ(RVO.Int3 a, RVO.Int3 b, RVO.Int3 c)
  RVA=0x063567E8  token=0x6000024  System.Boolean IsClockwiseOrColinearXZ(RVO.Int3 a, RVO.Int3 b, RVO.Int3 c)
  RVA=0x0635686C  token=0x6000025  System.Boolean IsClockwiseOrColinear(RVO.Int2 a, RVO.Int2 b, RVO.Int2 c)
  RVA=0x06356A54  token=0x6000026  System.Boolean IsColinear(UnityEngine.Vector3 a, UnityEngine.Vector3 b, UnityEngine.Vector3 c)
  RVA=0x063569E8  token=0x6000027  System.Boolean IsColinear(UnityEngine.Vector2 a, UnityEngine.Vector2 b, UnityEngine.Vector2 c)
  RVA=0x063569B8  token=0x6000028  System.Boolean IsColinearXZ(RVO.Int3 a, RVO.Int3 b, RVO.Int3 c)
  RVA=0x06356968  token=0x6000029  System.Boolean IsColinearXZ(UnityEngine.Vector3 a, UnityEngine.Vector3 b, UnityEngine.Vector3 c)
  RVA=0x0635692C  token=0x600002A  System.Boolean IsColinearAlmostXZ(RVO.Int3 a, RVO.Int3 b, RVO.Int3 c)
  RVA=0x06358454  token=0x600002B  System.Boolean SegmentsIntersect(RVO.Int2 start1, RVO.Int2 end1, RVO.Int2 start2, RVO.Int2 end2)
  RVA=0x0635812C  token=0x600002C  System.Boolean SegmentsIntersectXZ(RVO.Int3 start1, RVO.Int3 end1, RVO.Int3 start2, RVO.Int3 end2)
  RVA=0x06358324  token=0x600002D  System.Boolean SegmentsIntersectXZ(UnityEngine.Vector3 start1, UnityEngine.Vector3 end1, UnityEngine.Vector3 start2, UnityEngine.Vector3 end2)
  RVA=0x063574EC  token=0x600002E  System.Boolean LineLineIntersectionFactor(UnityEngine.Vector2 start1, UnityEngine.Vector2 dir1, UnityEngine.Vector2 start2, UnityEngine.Vector2 dir2, System.Single& t)
  RVA=0x06356CB8  token=0x600002F  UnityEngine.Vector3 LineDirIntersectionPointXZ(UnityEngine.Vector3 start1, UnityEngine.Vector3 dir1, UnityEngine.Vector3 start2, UnityEngine.Vector3 dir2)
  RVA=0x06356D90  token=0x6000030  UnityEngine.Vector3 LineDirIntersectionPointXZ(UnityEngine.Vector3 start1, UnityEngine.Vector3 dir1, UnityEngine.Vector3 start2, UnityEngine.Vector3 dir2, System.Boolean& intersects)
  RVA=0x06357788  token=0x6000031  System.Boolean RaySegmentIntersectXZ(RVO.Int3 start1, RVO.Int3 end1, RVO.Int3 start2, RVO.Int3 end2)
  RVA=0x06356E80  token=0x6000032  System.Boolean LineIntersectionFactorXZ(RVO.Int3 start1, RVO.Int3 end1, RVO.Int3 start2, RVO.Int3 end2, System.Single& factor1, System.Single& factor2)
  RVA=0x063570B4  token=0x6000033  System.Boolean LineIntersectionFactorXZ(UnityEngine.Vector3 start1, UnityEngine.Vector3 end1, UnityEngine.Vector3 start2, UnityEngine.Vector3 end2, System.Single& factor1, System.Single& factor2)
  RVA=0x06357578  token=0x6000034  System.Single LineRayIntersectionFactorXZ(RVO.Int3 start1, RVO.Int3 end1, RVO.Int3 start2, RVO.Int3 end2)
  RVA=0x06356FC4  token=0x6000035  System.Single LineIntersectionFactorXZ(UnityEngine.Vector3 start1, UnityEngine.Vector3 end1, UnityEngine.Vector3 start2, UnityEngine.Vector3 end2)
  RVA=0x063571F0  token=0x6000036  UnityEngine.Vector3 LineIntersectionPointXZ(UnityEngine.Vector3 start1, UnityEngine.Vector3 end1, UnityEngine.Vector3 start2, UnityEngine.Vector3 end2)
  RVA=0x0635729C  token=0x6000037  UnityEngine.Vector3 LineIntersectionPointXZ(UnityEngine.Vector3 start1, UnityEngine.Vector3 end1, UnityEngine.Vector3 start2, UnityEngine.Vector3 end2, System.Boolean& intersects)
  RVA=0x063573F4  token=0x6000038  UnityEngine.Vector2 LineIntersectionPoint(UnityEngine.Vector2 start1, UnityEngine.Vector2 end1, UnityEngine.Vector2 start2, UnityEngine.Vector2 end2)
  RVA=0x06357418  token=0x6000039  UnityEngine.Vector2 LineIntersectionPoint(UnityEngine.Vector2 start1, UnityEngine.Vector2 end1, UnityEngine.Vector2 start2, UnityEngine.Vector2 end2, System.Boolean& intersects)
  RVA=0x06357C30  token=0x600003A  UnityEngine.Vector3 SegmentIntersectionPointXZ(UnityEngine.Vector3 start1, UnityEngine.Vector3 end1, UnityEngine.Vector3 start2, UnityEngine.Vector3 end2, System.Boolean& intersects)
  RVA=0x06357DE0  token=0x600003B  System.Boolean SegmentIntersectsBounds(UnityEngine.Bounds bounds, UnityEngine.Vector3 a, UnityEngine.Vector3 b)
  RVA=0x06356B48  token=0x600003C  System.Single LineCircleIntersectionFactor(UnityEngine.Vector3 circleCenter, UnityEngine.Vector3 linePoint1, UnityEngine.Vector3 linePoint2, System.Single radius)
  RVA=0x0635797C  token=0x600003D  System.Boolean ReversesFaceOrientations(UnityEngine.Matrix4x4 matrix)
  RVA=0x063578BC  token=0x600003E  System.Boolean ReversesFaceOrientationsXZ(UnityEngine.Matrix4x4 matrix)
  RVA=0x063576A4  token=0x600003F  UnityEngine.Vector3 Normalize(UnityEngine.Vector3 v, System.Single& magnitude)
  RVA=0x06357734  token=0x6000040  UnityEngine.Vector2 Normalize(UnityEngine.Vector2 v, System.Single& magnitude)
  RVA=0x0635601C  token=0x6000041  UnityEngine.Vector3 ClampMagnitudeXZ(UnityEngine.Vector3 v, System.Single maxMagnitude)
  RVA=0x04D86080  token=0x6000042  System.Single MagnitudeXZ(UnityEngine.Vector3 v)
END_CLASS

CLASS: RVO.Int3
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x1C
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            System.Int32                    x  // 0x10
  public            System.Int32                    y  // 0x14
  public            System.Int32                    z  // 0x18
  public    static  System.Int32                    Precision  // const
  public    static  System.Single                   FloatPrecision  // const
  public    static  System.Single                   PrecisionFactor  // const
PROPERTIES:
  zero  get=0x04D85DC0
  Item  get=0x06351C5C  set=0x06352050
  magnitude  get=0x06351CD0
  costMagnitude  get=0x06351C70
  sqrMagnitude  get=0x04D85D90
  sqrMagnitudeLong  get=0x04D85D70
METHODS:
  RVA=0x06351BC4  token=0x6000044  System.Void .ctor(UnityEngine.Vector3 position)
  RVA=0x04D85D60  token=0x6000045  System.Void .ctor(System.Int32 _x, System.Int32 _y, System.Int32 _z)
  RVA=0x06351A88  token=0x6000046  System.Boolean op_Equality(RVO.Int3 lhs, RVO.Int3 rhs)
  RVA=0x06351EDC  token=0x6000047  System.Boolean op_Inequality(RVO.Int3 lhs, RVO.Int3 rhs)
  RVA=0x06351E14  token=0x6000048  RVO.Int3 op_Explicit(UnityEngine.Vector3 ob)
  RVA=0x04D85E00  token=0x6000049  UnityEngine.Vector3 op_Explicit(RVO.Int3 ob)
  RVA=0x04D85E70  token=0x600004A  RVO.Int3 op_Subtraction(RVO.Int3 lhs, RVO.Int3 rhs)
  RVA=0x04D85EA0  token=0x600004B  RVO.Int3 op_UnaryNegation(RVO.Int3 lhs)
  RVA=0x04D85DD0  token=0x600004C  RVO.Int3 op_Addition(RVO.Int3 lhs, RVO.Int3 rhs)
  RVA=0x04D85E40  token=0x600004D  RVO.Int3 op_Multiply(RVO.Int3 lhs, System.Int32 rhs)
  RVA=0x06351EF8  token=0x600004E  RVO.Int3 op_Multiply(RVO.Int3 lhs, System.Single rhs)
  RVA=0x06351FA8  token=0x600004F  RVO.Int3 op_Multiply(RVO.Int3 lhs, System.Double rhs)
  RVA=0x06351D64  token=0x6000050  RVO.Int3 op_Division(RVO.Int3 lhs, System.Single rhs)
  RVA=0x06351920  token=0x6000053  System.Single Angle(RVO.Int3 lhs, RVO.Int3 rhs)
  RVA=0x04D85D00  token=0x6000054  System.Int32 Dot(RVO.Int3 lhs, RVO.Int3 rhs)
  RVA=0x04D85CD0  token=0x6000055  System.Int64 DotLong(RVO.Int3 lhs, RVO.Int3 rhs)
  RVA=0x04D85D40  token=0x6000056  RVO.Int3 Normal2D()
  RVA=0x06351ECC  token=0x600005B  System.String op_Implicit(RVO.Int3 obj)
  RVA=0x06351AA4  token=0x600005C  System.String ToString()
  RVA=0x06351A10  token=0x600005D  System.Boolean Equals(System.Object obj)
  RVA=0x06351A88  token=0x600005E  System.Boolean Equals(RVO.Int3 other)
  RVA=0x04D85D20  token=0x600005F  System.Int32 GetHashCode()
END_CLASS

CLASS: RVO.Int2
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x18
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            System.Int32                    x  // 0x10
  public            System.Int32                    y  // 0x14
PROPERTIES:
  sqrMagnitudeLong  get=0x04D85C50
METHODS:
  RVA=0x04D85C40  token=0x6000060  System.Void .ctor(System.Int32 x, System.Int32 y)
  RVA=0x04D85CB0  token=0x6000062  RVO.Int2 op_UnaryNegation(RVO.Int2 lhs)
  RVA=0x04D85C70  token=0x6000063  RVO.Int2 op_Addition(RVO.Int2 a, RVO.Int2 b)
  RVA=0x04D85C90  token=0x6000064  RVO.Int2 op_Subtraction(RVO.Int2 a, RVO.Int2 b)
  RVA=0x063518F0  token=0x6000065  System.Boolean op_Equality(RVO.Int2 a, RVO.Int2 b)
  RVA=0x06351908  token=0x6000066  System.Boolean op_Inequality(RVO.Int2 a, RVO.Int2 b)
  RVA=0x04D85BC0  token=0x6000067  System.Int64 DotLong(RVO.Int2 a, RVO.Int2 b)
  RVA=0x063516C8  token=0x6000068  System.Boolean Equals(System.Object o)
  RVA=0x063516B4  token=0x6000069  System.Boolean Equals(RVO.Int2 other)
  RVA=0x04D85C10  token=0x600006A  System.Int32 GetHashCode()
  RVA=0x06351794  token=0x600006B  RVO.Int2 Min(RVO.Int2 a, RVO.Int2 b)
  RVA=0x06351728  token=0x600006C  RVO.Int2 Max(RVO.Int2 a, RVO.Int2 b)
  RVA=0x04D85BF0  token=0x600006D  RVO.Int2 FromInt3XZ(RVO.Int3 o)
  RVA=0x04D85C20  token=0x600006E  RVO.Int3 ToInt3XZ(RVO.Int2 o)
  RVA=0x06351800  token=0x600006F  System.String ToString()
END_CLASS

CLASS: RVO.RVOAgentJobData
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x64
FIELDS:
  public            System.Int32                    agentID  // 0x10
  public            System.Int32                    agentIndex  // 0x14
  public            System.Boolean                  manuallyControlled  // 0x18
  public            System.Boolean                  locked  // 0x19
  public            System.Single                   radius  // 0x1c
  public            System.Single                   height  // 0x20
  public            System.Single                   desiredSpeed  // 0x24
  public            System.Single                   maxSpeed  // 0x28
  public            System.Single                   agentTimeHorizon  // 0x2c
  public            System.Single                   obstacleTimeHorizon  // 0x30
  public            System.Single                   priority  // 0x34
  public            System.Single                   elevationCoordinate  // 0x38
  public            RVO.RVOLayer                    layer  // 0x3c
  public            RVO.RVOLayer                    collidesWith  // 0x40
  public            UnityEngine.Vector2             position  // 0x44
  public            UnityEngine.Vector2             currentVelocity  // 0x4c
  public            UnityEngine.Vector2             desiredVelocity  // 0x54
  public            UnityEngine.Vector2             desiredTargetPointInVelocitySpace  // 0x5c
METHODS:
END_CLASS

CLASS: RVO.RVOAgent
TYPE:  class
TOKEN: 0x200000C
SIZE:  0xC8
FIELDS:
  public            System.Int32                    agentID  // 0x10
  public            System.Int32                    agentIndex  // 0x14
  public            System.Single                   radius  // 0x18
  public            System.Single                   height  // 0x1c
  public            System.Single                   desiredSpeed  // 0x20
  public            System.Single                   maxSpeed  // 0x24
  public            System.Single                   agentTimeHorizon  // 0x28
  public            System.Single                   obstacleTimeHorizon  // 0x2c
  public            System.Single                   priority  // 0x30
  public            System.Boolean                  locked  // 0x34
  public            RVO.RVOLayer                    layer  // 0x38
  public            RVO.RVOLayer                    collidesWith  // 0x3c
  public            System.Int32                    maxNeighbours  // 0x40
  public            UnityEngine.Vector2             position  // 0x44
  public            UnityEngine.Vector3             position3  // 0x4c
  public            System.Single                   elevationCoordinate  // 0x58
  public            RVO.RVOAgent                    ignoreAgent  // 0x60
  public            UnityEngine.Vector2             currentVelocity  // 0x68
  public            UnityEngine.Vector2             desiredTargetPointInVelocitySpace  // 0x70
  public            UnityEngine.Vector2             desiredVelocity  // 0x78
  public            UnityEngine.Vector2             nextTargetPoint  // 0x80
  public            UnityEngine.Vector2             collisionNormal  // 0x88
  public            System.Boolean                  manuallyControlled  // 0x90
  private           System.Action                   <PreCalculationCallback>k__BackingField  // 0x98
  public            RVO.RVOAgent                    next  // 0xa0
  public            System.Single                   calculatedSpeed  // 0xa8
  public            UnityEngine.Vector2             calculatedTargetPoint  // 0xac
  public            System.Boolean                  steeringTargetInsideObstacle  // 0xb4
  private           System.Collections.Generic.List<RVO.RVOAgent>m_neighbours  // 0xb8
  private           System.Collections.Generic.List<System.Single>m_neighbourDists  // 0xc0
PROPERTIES:
  PreCalculationCallback  get=0x04D85EE0  set=0x06352D0C
  neighbours  get=0x04D85EF0
METHODS:
  RVA=0x034EF4B0  token=0x6000072  System.Void SetTarget(UnityEngine.Vector2 targetPoint, System.Single desiredSpeed, System.Single maxSpeed)
  RVA=0x04D85ED0  token=0x6000073  System.Void SetCollisionNormal(UnityEngine.Vector2 normal)
  RVA=0x032C2240  token=0x6000074  System.Void ForceSetVelocity(UnityEngine.Vector2 velocity)
  RVA=0x0397F440  token=0x6000076  System.Void .ctor()
  RVA=0x02EE4890  token=0x6000077  System.Void PreCalculation()
  RVA=0x02EE48B0  token=0x6000078  RVO.RVOAgentJobData CreateJobData()
  RVA=0x0333C380  token=0x6000079  System.Void PostCalculation(System.Single resultSpeed, UnityEngine.Vector2 resultTargetPoint)
  RVA=0x02EE3510  token=0x600007A  System.Void CalculateNeighbours(RVO.RVOQuadtree quadtree, System.Single desiredDeltaTime)
  RVA=0x02EE6FD0  token=0x600007B  System.Single InsertAgentNeighbour(RVO.RVOAgent rvoAgent, System.Single rangeSq)
  RVA=0x06352CC4  token=0x600007C  UnityEngine.Color Rainbow(System.Single v)
END_CLASS

CLASS: RVO.RVOObstacleJobData
TYPE:  sealed struct
TOKEN: 0x200000D
SIZE:  0x2C
FIELDS:
  public            System.Int32                    obstacleID  // 0x10
  public            System.Int32                    obstacleIndex  // 0x14
  public            RVO.RVOLayer                    layer  // 0x18
  public            System.Single                   height  // 0x1c
  public            UnityEngine.Vector3             position  // 0x20
METHODS:
  RVA=0x06352E7C  token=0x600007D  System.Boolean op_Equality(RVO.RVOObstacleJobData a, RVO.RVOObstacleJobData b)
  RVA=0x06352F74  token=0x600007E  System.Boolean op_Inequality(RVO.RVOObstacleJobData a, RVO.RVOObstacleJobData b)
  RVA=0x06352D20  token=0x600007F  System.Boolean Equals(System.Object obj)
  RVA=0x06352DC4  token=0x6000080  System.Int32 GetHashCode()
END_CLASS

CLASS: RVO.Simulator
TYPE:  class
TOKEN: 0x200000E
SIZE:  0x158
FIELDS:
  private           System.Single                   desiredDeltaTime  // 0x10
  private           Unity.Collections.NativeList<RVO.RVOAgentJobData>m_jobAgents  // 0x18
  private           Unity.Collections.NativeList<System.Boolean>m_jobNeighbours  // 0x28
  private           Unity.Collections.NativeList<RVO.RVOObstacleJobData>m_jobObstacles  // 0x38
  private           Unity.Collections.NativeArray<System.Single>m_jobCalculatedSpeeds  // 0x48
  private           Unity.Collections.NativeArray<UnityEngine.Vector2>m_jobCalculatedTargetPoints  // 0x58
  private           Unity.Collections.NativeArray<UnityEngine.Vector2>m_jobOverlapRecoveryVelocities  // 0x68
  private           Unity.Collections.NativeArray<System.Int32>m_jobOverlapRecoveryCounts  // 0x78
  private           Unity.Collections.NativeArray<System.Int32>m_jobOverlapRecoveryActiveFlags  // 0x88
  private           Unity.Collections.NativeArray<System.Int32>m_jobSteeringTargetInsideObstacleMatrix  // 0x98
  private           Unity.Collections.NativeArray<System.Int32>m_jobAgentBufferMaxIndex  // 0xa8
  private           Unity.Collections.NativeArray<RVO.Line>m_jobAgentLineBuffer  // 0xb8
  private           Unity.Collections.NativeArray<System.Int32>m_jobObsBufferMaxIndex  // 0xc8
  private           Unity.Collections.NativeArray<RVO.VO>m_jobObsVoBuffer  // 0xd8
  private           Unity.Collections.NativeList<RVO.RVOObstacleJobData>m_obstacles  // 0xe8
  private           System.Collections.Generic.Dictionary<System.Int32,System.Int32>m_obstacleIDToIndex  // 0xf8
  private   static  System.Int32                    s_nextObstacleID  // static @ 0x0
  private           System.Boolean                  m_obstaclesChanged  // 0x100
  private           System.Collections.Generic.List<RVO.RVOAgent>m_agents  // 0x108
  private           System.Collections.Generic.Dictionary<System.Int32,System.Int32>m_agentIDToIndex  // 0x110
  private   static  System.Int32                    s_nextAgentID  // static @ 0x4
  private           RVO.RVOQuadtree                 <Quadtree>k__BackingField  // 0x118
  private           System.Single                   deltaTime  // 0x120
  private           System.Single                   lastStep  // 0x124
  private   readonly System.Single                   m_symmetryBreakingBias  // 0x128
  private   readonly System.Single                   m_deepOverlapRatio  // 0x12c
  private   readonly System.Single                   m_overlapRecoveryExitRatio  // 0x130
  private   readonly System.Int32                    m_vertexCount  // 0x134
  private   static  System.Single                   SHRINK_FACTOR  // const
  private   static  System.Int32                    INIT_AGENT_COUNT  // const
  private   static  System.Int32                    INIT_OBSTACLE_COUNT  // const
  private   static  System.Int32                    INIT_AGENT_VO_COUNT  // const
  private   static  System.Int32                    INIT_OBS_VO_COUNT  // const
  private           System.Int32                    m_agentCount  // 0x138
  private           System.Int32                    m_agentVOCount  // 0x13c
  private           System.Int32                    m_obsVOCount  // 0x140
  private           Unity.Jobs.JobHandle            m_handle  // 0x148
PROPERTIES:
  DesiredDeltaTime  get=0x04D85F70  set=0x04B423B0
  agents  get=0x04D85F90
  Quadtree  get=0x04D85F80  set=0x0539715C
METHODS:
  RVA=0x045A3290  token=0x6000086  System.Void .ctor()
  RVA=0x06354AA8  token=0x6000087  System.Void OnDestroy()
  RVA=0x0397F050  token=0x6000088  RVO.RVOAgent AddAgent()
  RVA=0x0397FF10  token=0x6000089  System.Void RemoveAgent(RVO.RVOAgent agent)
  RVA=0x0397F420  token=0x600008A  System.Void _CompletePendingJob()
  RVA=0x033A6EB0  token=0x600008B  System.Int32 AddObstacle(UnityEngine.Vector3[] vertices, System.Single height, RVO.RVOLayer layer)
  RVA=0x063555D0  token=0x600008C  System.Void UpdateObstacle(System.Int32 obstacleID, UnityEngine.Vector3[] vertices)
  RVA=0x033DF240  token=0x600008D  System.Void RemoveObstacle(System.Int32 obstacleID)
  RVA=0x02EE4970  token=0x600008E  System.Void BuildQuadtree()
  RVA=0x02EE4C40  token=0x600008F  System.Void Update()
  RVA=0x03AFFD00  token=0x6000090  System.Void _Realloc(System.Int32 agentCount, System.Int32 obstacleCount)
  RVA=0x0333C050  token=0x6000091  System.Void _WriteBackResult()
  RVA=0x04D85F50  token=0x6000092  UnityEngine.Vector2 _To2D(UnityEngine.Vector3 p, System.Single& elevation)
  RVA=0x06355760  token=0x6000093  System.Boolean _IsPointInConvexQuad(UnityEngine.Vector2 point, UnityEngine.Vector2 p0, UnityEngine.Vector2 p1, UnityEngine.Vector2 p2, UnityEngine.Vector2 p3)
  RVA=0x063556E4  token=0x6000094  System.Void _CheckEdgeSign(UnityEngine.Vector2 point, UnityEngine.Vector2 from, UnityEngine.Vector2 to, System.Boolean& hasPositive, System.Boolean& hasNegative, System.Single epsilon)
  RVA=0x06354CD8  token=0x6000095  RVO.VO SegmentObstacle(UnityEngine.Vector2 segmentStart, UnityEngine.Vector2 segmentEnd, UnityEngine.Vector2 offset, System.Single radius, System.Single inverseDt, System.Single inverseDeltaTime)
  RVA=0x06353340  token=0x6000096  System.Boolean BiasDesiredVelocity(System.Int32 obsStart, System.Int32 obsEnd, Unity.Collections.NativeArray<RVO.VO>& obsVos, UnityEngine.Vector2& desiredVelocity, UnityEngine.Vector2& targetPointInVelocitySpace, System.Single maxBiasRadians)
  RVA=0x06353E74  token=0x6000097  UnityEngine.Vector2 GradientDescent(System.Int32 obsStart, System.Int32 obsEnd, Unity.Collections.NativeArray<RVO.VO>& obsVos, RVO.RVOAgentJobData agent, UnityEngine.Vector2 sampleAround1, UnityEngine.Vector2 sampleAround2, System.Single desiredVelocityWeightScale)
  RVA=0x063553E4  token=0x6000098  UnityEngine.Vector2 Trace(System.Int32 obsStart, System.Int32 obsEnd, Unity.Collections.NativeArray<RVO.VO>& obsVos, RVO.RVOAgentJobData agent, UnityEngine.Vector2 p, System.Single desiredVelocityWeightScale, System.Single& score)
  RVA=0x06353BF8  token=0x6000099  UnityEngine.Vector2 EvaluateGradient(System.Int32 obsStart, System.Int32 obsEnd, Unity.Collections.NativeArray<RVO.VO>& obsVos, RVO.RVOAgentJobData agent, UnityEngine.Vector2 p, System.Single desiredVelocityWeightScale, System.Single& value)
  RVA=0x063532DC  token=0x600009A  UnityEngine.Vector2 ApplyOverlapRecoveryVelocity(System.Single maxSpeed, UnityEngine.Vector2 baseVelocity, UnityEngine.Vector2 overlapRecoveryVelocity, System.Int32 overlapRecoveryCount)
  RVA=0x06355290  token=0x600009B  System.Boolean ShouldUseOverlapRecovery(System.Single dist, System.Single totalRadius, System.Boolean wasActive, System.Single enterRatio, System.Single exitRatio)
  RVA=0x063539C8  token=0x600009C  UnityEngine.Vector2 CalculateStableOverlapRecoveryDirection(RVO.RVOAgentJobData agent, RVO.RVOAgentJobData other, UnityEngine.Vector2 awayFromOther)
  RVA=0x06353530  token=0x600009D  RVO.Line BuildAgentOrcaLine(RVO.RVOAgentJobData agent, RVO.RVOAgentJobData other, System.Single totalRadius, System.Single deltaTime, System.Single avoidanceStrength)
  RVA=0x04D85F20  token=0x600009E  System.Single Det(UnityEngine.Vector2 a, UnityEngine.Vector2 b)
  RVA=0x063552B4  token=0x600009F  UnityEngine.Vector2 SolveAgentLines(System.Int32 start, System.Int32 end, Unity.Collections.NativeArray<RVO.Line>& lines, System.Single radius, UnityEngine.Vector2 preferredVelocity)
  RVA=0x06353AC8  token=0x60000A0  System.Void ClampResultToAgentLinesFeasible(System.Int32 start, System.Int32 end, Unity.Collections.NativeArray<RVO.Line>& lines, System.Single radius, UnityEngine.Vector2& result)
  RVA=0x06353FA0  token=0x60000A1  System.Boolean LP1(System.Int32 start, System.Int32 end, Unity.Collections.NativeArray<RVO.Line>& lines, System.Int32 lineNo, System.Single radius, UnityEngine.Vector2 optVelocity, System.Boolean dirOpt, UnityEngine.Vector2& result)
  RVA=0x063542E8  token=0x60000A2  System.Int32 LP2(System.Int32 start, System.Int32 end, Unity.Collections.NativeArray<RVO.Line>& lines, System.Single radius, UnityEngine.Vector2 optVelocity, System.Boolean dirOpt, UnityEngine.Vector2& result)
  RVA=0x06354498  token=0x60000A3  System.Void LP3(System.Int32 start, System.Int32 end, Unity.Collections.NativeArray<RVO.Line>& lines, System.Int32 beginLine, System.Single radius, UnityEngine.Vector2& result)
END_CLASS

CLASS: RVO.Line
TYPE:  sealed struct
TOKEN: 0x2000012
SIZE:  0x20
FIELDS:
  public            UnityEngine.Vector2             point  // 0x10
  public            UnityEngine.Vector2             dir  // 0x18
METHODS:
END_CLASS

CLASS: RVO.RVOQuadtree
TYPE:  class
TOKEN: 0x2000013
SIZE:  0x38
FIELDS:
  private   static  System.Int32                    LeafSize  // const
  private           System.Single                   maxRadius  // 0x10
  private           RVO.RVOQuadtree.Node[]          m_nodes  // 0x18
  private           System.Int32                    filledNodes  // 0x20
  private           UnityEngine.Rect                m_bounds  // 0x24
PROPERTIES:
  nodes  get=0x0385B100
  bounds  get=0x04D85F10
METHODS:
  RVA=0x040DE2D0  token=0x60000A9  System.Void Clear()
  RVA=0x04D85F00  token=0x60000AA  System.Void SetBounds(UnityEngine.Rect r)
  RVA=0x03EA50D0  token=0x60000AB  System.Int32 GetNodeIndex()
  RVA=0x02EE66F0  token=0x60000AC  System.Void Insert(RVO.RVOAgent rvoAgent)
  RVA=0x040DE310  token=0x60000AD  System.Void CalculateSpeeds()
  RVA=0x02EE33F0  token=0x60000AE  System.Void Query(UnityEngine.Vector2 p, System.Single speed, System.Single timeHorizon, System.Single agentRadius, RVO.RVOAgent rvoAgent)
  RVA=0x045A3890  token=0x60000AF  System.Void .ctor()
END_CLASS

CLASS: RVO.VO
TYPE:  sealed struct
TOKEN: 0x2000016
SIZE:  0x6C
FIELDS:
  public            UnityEngine.Vector2             line1  // 0x10
  public            UnityEngine.Vector2             line2  // 0x18
  public            UnityEngine.Vector2             dir1  // 0x20
  public            UnityEngine.Vector2             dir2  // 0x28
  public            UnityEngine.Vector2             cutoffLine  // 0x30
  public            UnityEngine.Vector2             cutoffDir  // 0x38
  public            UnityEngine.Vector2             circleCenter  // 0x40
  public            System.Boolean                  colliding  // 0x48
  public            System.Single                   radius  // 0x4c
  public            System.Single                   weightFactor  // 0x50
  public            System.Single                   weightBonus  // 0x54
  public            UnityEngine.Vector2             segmentStart  // 0x58
  public            UnityEngine.Vector2             segmentEnd  // 0x60
  public            System.Boolean                  segment  // 0x68
METHODS:
  RVA=0x06355C4C  token=0x60000B4  System.Void .ctor(UnityEngine.Vector2 center, UnityEngine.Vector2 offset, System.Single radius, System.Single inverseDt, System.Single inverseDeltaTime)
  RVA=0x04D85FA0  token=0x60000B5  System.Single SignedDistanceFromLine(UnityEngine.Vector2 a, UnityEngine.Vector2 dir, UnityEngine.Vector2 p)
  RVA=0x06355BD4  token=0x60000B6  UnityEngine.Vector2 ScaledGradient(UnityEngine.Vector2 p, System.Single& weight)
  RVA=0x06355890  token=0x60000B7  UnityEngine.Vector2 Gradient(UnityEngine.Vector2 p, System.Single& weight)
END_CLASS

CLASS: System.Runtime.CompilerServices.IsUnmanagedAttribute
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000002  System.Void .ctor()
END_CLASS

