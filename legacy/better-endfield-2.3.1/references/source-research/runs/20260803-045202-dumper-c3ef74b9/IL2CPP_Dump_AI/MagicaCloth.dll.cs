// ========================================================
// Dumped by @desirepro
// Assembly: MagicaCloth.dll
// Classes:  308
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

CLASS: ReductionWeightMode
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaReductionMesh.ReductionMesh.ReductionWeightModeDistance  // const
  public    static  MagicaReductionMesh.ReductionMesh.ReductionWeightModeAverage  // const
  public    static  MagicaReductionMesh.ReductionMesh.ReductionWeightModeDistanceAverage  // const
METHODS:
END_CLASS

CLASS: MeshIndexData
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.List<System.UInt32>meshIndexPackList  // 0x10
METHODS:
  RVA=0x09ECE46C  token=0x6000021  System.Void .ctor()
END_CLASS

CLASS: MeshInfo
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x48
FIELDS:
  public            System.Int32                    meshIndex  // 0x10
  public            UnityEngine.Mesh                mesh  // 0x18
  public            System.Collections.Generic.List<UnityEngine.Vector3>vertices  // 0x20
  public            System.Collections.Generic.List<UnityEngine.Vector3>normals  // 0x28
  public            System.Collections.Generic.List<UnityEngine.Vector4>tangents  // 0x30
  public            System.Collections.Generic.List<UnityEngine.BoneWeight>boneWeights  // 0x38
  public            System.Collections.Generic.List<System.Int32>parents  // 0x40
PROPERTIES:
  VertexCount  get=0x09ECE6E8
METHODS:
  RVA=0x09ECE4E4  token=0x6000023  System.Void .ctor()
END_CLASS

CLASS: WeightData
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x18
FIELDS:
  public            System.Int32                    boneIndex  // 0x10
  public            System.Single                   boneWeight  // 0x14
METHODS:
  RVA=0x041E1670  token=0x600004B  System.Void .ctor()
END_CLASS

CLASS: Vertex
TYPE:  class
TOKEN: 0x200000D
SIZE:  0x58
FIELDS:
  public            System.Int32                    meshIndex  // 0x10
  public            System.Int32                    vertexIndex  // 0x14
  public            UnityEngine.Vector3             wpos  // 0x18
  public            UnityEngine.Vector3             wnor  // 0x24
  public            UnityEngine.Vector3             wtan  // 0x30
  public            System.Single                   tanw  // 0x3c
  public            UnityEngine.Vector2             uv  // 0x40
  public            System.Int32                    parentIndex  // 0x48
  public            System.Collections.Generic.List<MagicaReductionMesh.MeshData.WeightData>boneWeightList  // 0x50
METHODS:
  RVA=0x09ED7C38  token=0x600004C  System.Void .ctor()
END_CLASS

CLASS: MeshInfo
TYPE:  class
TOKEN: 0x200000E
SIZE:  0x30
FIELDS:
  public            System.Int32                    index  // 0x10
  public            UnityEngine.Mesh                mesh  // 0x18
  public            System.Int32                    vertexCount  // 0x20
  public            System.Collections.Generic.List<MagicaReductionMesh.MeshData.Vertex>vertexList  // 0x28
METHODS:
  RVA=0x09ECE670  token=0x600004D  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass20_0
TYPE:  sealed class
TOKEN: 0x2000010
SIZE:  0x18
FIELDS:
  public            MagicaReductionMesh.MeshData.WeightDataw  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600005C  System.Void .ctor()
  RVA=0x09ED609C  token=0x600005D  System.Boolean <CalcBoneWeight_DistanceAverage>b__1(MagicaReductionMesh.MeshData.WeightData wdata)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000011
SIZE:  0x10
FIELDS:
  public    static readonly MagicaReductionMesh.MeshData.ShareVertex.<>c<>9  // static @ 0x0
  public    static  System.Comparison<MagicaReductionMesh.MeshData.WeightData><>9__20_0  // static @ 0x8
  public    static  System.Comparison<MagicaReductionMesh.MeshData.WeightData><>9__21_0  // static @ 0x10
  public    static  System.Comparison<MagicaReductionMesh.MeshData.WeightData><>9__23_0  // static @ 0x18
METHODS:
  RVA=0x09ED61C0  token=0x600005E  System.Void .cctor()
  RVA=0x041E1670  token=0x600005F  System.Void .ctor()
  RVA=0x09ED6034  token=0x6000060  System.Int32 <CalcBoneWeight_DistanceAverage>b__20_0(MagicaReductionMesh.MeshData.WeightData a, MagicaReductionMesh.MeshData.WeightData b)
  RVA=0x09ED6034  token=0x6000061  System.Int32 <CalcBoneWeight_Average>b__21_0(MagicaReductionMesh.MeshData.WeightData a, MagicaReductionMesh.MeshData.WeightData b)
  RVA=0x09ED6034  token=0x6000062  System.Int32 <CalcBoneWeight_Distance>b__23_0(MagicaReductionMesh.MeshData.WeightData a, MagicaReductionMesh.MeshData.WeightData b)
END_CLASS

CLASS: <>c__DisplayClass21_0
TYPE:  sealed class
TOKEN: 0x2000012
SIZE:  0x18
FIELDS:
  public            MagicaReductionMesh.MeshData.WeightDataw  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000063  System.Void .ctor()
  RVA=0x09ED609C  token=0x6000064  System.Boolean <CalcBoneWeight_Average>b__1(MagicaReductionMesh.MeshData.WeightData wdata)
END_CLASS

CLASS: <>c__DisplayClass23_0
TYPE:  sealed class
TOKEN: 0x2000013
SIZE:  0x18
FIELDS:
  public            MagicaReductionMesh.MeshData.WeightDataw  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000065  System.Void .ctor()
  RVA=0x09ED609C  token=0x6000066  System.Boolean <CalcBoneWeight_Distance>b__1(MagicaReductionMesh.MeshData.WeightData wdata)
END_CLASS

CLASS: ShareVertex
TYPE:  class
TOKEN: 0x200000F
SIZE:  0xE8
FIELDS:
  public            System.Int32                    sindex  // 0x10
  public            UnityEngine.Vector3             wpos  // 0x14
  public            UnityEngine.Vector3             wnor  // 0x20
  public            UnityEngine.Vector3             wtan  // 0x2c
  public            System.Single                   tanw  // 0x38
  public            UnityEngine.Vector2             uv  // 0x3c
  public            UnityEngine.Matrix4x4           worldToLocalMatrix  // 0x44
  public            UnityEngine.Matrix4x4           bindpose  // 0x84
  public            System.Collections.Generic.List<MagicaReductionMesh.MeshData.WeightData>boneWeightList  // 0xc8
  public            System.Collections.Generic.List<MagicaReductionMesh.MeshData.Vertex>vertexList  // 0xd0
  public            System.Collections.Generic.HashSet<MagicaReductionMesh.MeshData.ShareVertex>linkShareVertexSet  // 0xd8
  public            System.Collections.Generic.HashSet<MagicaReductionMesh.MeshData.Triangle>linkTriangleSet  // 0xe0
METHODS:
  RVA=0x09ED1A70  token=0x600004E  System.Void AddLink(MagicaReductionMesh.MeshData.ShareVertex mv)
  RVA=0x09ED3E50  token=0x600004F  System.Void ReplaseLink(MagicaReductionMesh.MeshData.ShareVertex old, MagicaReductionMesh.MeshData.ShareVertex mv)
  RVA=0x09ED3B68  token=0x6000050  System.Void RecalcCoordinate()
  RVA=0x09ED3728  token=0x6000051  System.Void CalcNormalTangentFromTriangle()
  RVA=0x09ED3634  token=0x6000052  UnityEngine.Vector3 CalcLocalPos(UnityEngine.Vector3 pos)
  RVA=0x09ED3584  token=0x6000053  UnityEngine.Vector3 CalcLocalDir(UnityEngine.Vector3 dir)
  RVA=0x09ED3964  token=0x6000054  UnityEngine.Matrix4x4 CalcWorldToLocalMatrix()
  RVA=0x09ED3548  token=0x6000055  System.Void CalcBoneWeight(MagicaReductionMesh.ReductionMesh.ReductionWeightMode weightMode, System.Single weightPow)
  RVA=0x09ED236C  token=0x6000056  System.Void CalcBoneWeight_DistanceAverage(System.Single weightPow)
  RVA=0x09ED1CF8  token=0x6000057  System.Void CalcBoneWeight_Average()
  RVA=0x09ED1AC8  token=0x6000058  System.Void AdjustWeight(System.Collections.Generic.List<MagicaReductionMesh.MeshData.WeightData> sumlist)
  RVA=0x09ED2D54  token=0x6000059  System.Void CalcBoneWeight_Distance(System.Single weightPow)
  RVA=0x09ED3A78  token=0x600005A  UnityEngine.BoneWeight GetBoneWeight()
  RVA=0x09ED3F08  token=0x600005B  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass9_0
TYPE:  sealed class
TOKEN: 0x2000015
SIZE:  0x18
FIELDS:
  public            System.Int32                    edgev0  // 0x10
  public            System.Int32                    edgev1  // 0x14
METHODS:
  RVA=0x041E1670  token=0x6000070  System.Void .ctor()
  RVA=0x09ED6130  token=0x6000071  System.Boolean <GetNonEdgeVertex>b__0(MagicaReductionMesh.MeshData.ShareVertex sv)
END_CLASS

CLASS: Triangle
TYPE:  class
TOKEN: 0x2000014
SIZE:  0x40
FIELDS:
  public            System.Int32                    tindex  // 0x10
  public            System.Collections.Generic.List<MagicaReductionMesh.MeshData.ShareVertex>shareVertexList  // 0x18
  public            UnityEngine.Vector3             wnor  // 0x20
  public            UnityEngine.Vector3             wtan  // 0x2c
  public            System.Boolean                  flipLock  // 0x38
METHODS:
  RVA=0x09ED5A0C  token=0x6000067  System.Void GetEdge(System.UInt32& edge0, System.UInt32& edge1, System.UInt32& edge2)
  RVA=0x09ED5254  token=0x6000068  UnityEngine.Vector3 CalcTriangleNormal()
  RVA=0x09ED5914  token=0x6000069  System.Void Flip()
  RVA=0x09ED55A0  token=0x600006A  UnityEngine.Vector3 CalcTriangleTangent()
  RVA=0x09ED5B78  token=0x600006B  MagicaReductionMesh.MeshData.ShareVertex GetNonEdgeVertex(System.Int32 edgev0, System.Int32 edgev1)
  RVA=0x09ED5D34  token=0x600006C  System.UInt64 GetTriangleHash()
  RVA=0x09ED5C4C  token=0x600006D  System.Single GetTriangleArea(MagicaReductionMesh.MeshData.ShareVertex sv0, MagicaReductionMesh.MeshData.ShareVertex sv1, MagicaReductionMesh.MeshData.ShareVertex sv2)
  RVA=0x09ED5DE8  token=0x600006E  System.String ToString()
  RVA=0x09ED5FBC  token=0x600006F  System.Void .ctor()
END_CLASS

CLASS: Line
TYPE:  class
TOKEN: 0x2000016
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.List<MagicaReductionMesh.MeshData.ShareVertex>shareVertexList  // 0x10
METHODS:
  RVA=0x09EC3760  token=0x6000072  System.Void .ctor()
END_CLASS

CLASS: UvWrapMode
TYPE:  sealed struct
TOKEN: 0x2000017
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaReductionMesh.MeshData.UvWrapModeNone  // const
  public    static  MagicaReductionMesh.MeshData.UvWrapModeSphere  // const
METHODS:
END_CLASS

CLASS: Square
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x28
FIELDS:
  public            System.UInt64                   shash  // 0x10
  public            System.Collections.Generic.List<MagicaReductionMesh.MeshData.Triangle>triangleList  // 0x18
  public            System.Single                   angle  // 0x20
METHODS:
  RVA=0x09ED4078  token=0x6000073  System.String ToString()
  RVA=0x09ED4200  token=0x6000074  System.Void .ctor()
END_CLASS

CLASS: Tetra
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x38
FIELDS:
  public            System.Collections.Generic.List<MagicaReductionMesh.MeshData.ShareVertex>shareVertexList  // 0x10
  public            UnityEngine.Vector3             circumCenter  // 0x18
  public            System.Single                   circumRadius  // 0x24
  public            UnityEngine.Vector3             tetraCenter  // 0x28
  public            System.Single                   tetraSize  // 0x34
METHODS:
  RVA=0x09ED50C0  token=0x6000075  System.Void .ctor()
  RVA=0x09ED5138  token=0x6000076  System.Void .ctor(MagicaReductionMesh.MeshData.ShareVertex a, MagicaReductionMesh.MeshData.ShareVertex b, MagicaReductionMesh.MeshData.ShareVertex c, MagicaReductionMesh.MeshData.ShareVertex d)
  RVA=0x09ED4D28  token=0x6000077  System.UInt64 GetTetraHash()
  RVA=0x09ED4278  token=0x6000078  System.Void CalcCircumcircle()
  RVA=0x09ED4E1C  token=0x6000079  System.Boolean IntersectCircumcircle(UnityEngine.Vector3 pos)
  RVA=0x09ED4B88  token=0x600007A  System.Boolean CheckSame(MagicaReductionMesh.MeshData.Tetra tri)
  RVA=0x09ED4C08  token=0x600007B  System.Boolean ContainsPoint(MagicaReductionMesh.MeshData.ShareVertex p1)
  RVA=0x09ED4C5C  token=0x600007C  System.Boolean ContainsPoint(MagicaReductionMesh.MeshData.ShareVertex p1, MagicaReductionMesh.MeshData.ShareVertex p2, MagicaReductionMesh.MeshData.ShareVertex p3, MagicaReductionMesh.MeshData.ShareVertex p4)
  RVA=0x09ED48E0  token=0x600007D  System.Void CalcSize()
  RVA=0x09ED4E68  token=0x600007E  System.Boolean Verification()
END_CLASS

CLASS: LinkInfo
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x20
FIELDS:
  public            MagicaReductionMesh.MeshData.ShareVertexsv  // 0x10
  public            System.Single                   length  // 0x18
  public            System.Int32                    count  // 0x1c
METHODS:
  RVA=0x041E1670  token=0x600007F  System.Void .ctor()
END_CLASS

CLASS: VertexLengthInfo
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x20
FIELDS:
  public            MagicaReductionMesh.MeshData.ShareVertexsv  // 0x10
  public            System.Single                   length  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000080  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass65_0
TYPE:  sealed class
TOKEN: 0x200001C
SIZE:  0x18
FIELDS:
  public            System.Single                   weightLength  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000081  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass65_1
TYPE:  sealed class
TOKEN: 0x200001D
SIZE:  0x20
FIELDS:
  public            MagicaReductionMesh.MeshData.Vertexvt  // 0x10
  public            MagicaReductionMesh.MeshData.<>c__DisplayClass65_0CS$<>8__locals1  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000082  System.Void .ctor()
  RVA=0x09ED60C4  token=0x6000083  System.Boolean <CreateOriginalMeshInfo>b__0(MagicaReductionMesh.MeshData.ShareVertex sv)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200001E
SIZE:  0x10
FIELDS:
  public    static readonly MagicaReductionMesh.MeshData.<>c<>9  // static @ 0x0
  public    static  System.Comparison<MagicaReductionMesh.MeshData.VertexLengthInfo><>9__69_0  // static @ 0x8
METHODS:
  RVA=0x09ED615C  token=0x6000084  System.Void .cctor()
  RVA=0x041E1670  token=0x6000085  System.Void .ctor()
  RVA=0x09ED606C  token=0x6000086  System.Int32 <SearchNearPointList>b__69_0(MagicaReductionMesh.MeshData.VertexLengthInfo a, MagicaReductionMesh.MeshData.VertexLengthInfo b)
END_CLASS

CLASS: Point
TYPE:  class
TOKEN: 0x2000021
SIZE:  0x40
FIELDS:
  public            MagicaReductionMesh.MeshData.ShareVertexshareVertex  // 0x10
  public            UnityEngine.Vector3             pos  // 0x18
  public            UnityEngine.Vector3Int          grid  // 0x24
  public            MagicaReductionMesh.NearPointReduction.PointnearPoint  // 0x30
  public            System.Single                   nearDist  // 0x38
METHODS:
  RVA=0x041E1670  token=0x6000099  System.Void .ctor()
END_CLASS

CLASS: Point
TYPE:  class
TOKEN: 0x2000023
SIZE:  0x28
FIELDS:
  public            MagicaReductionMesh.MeshData.ShareVertexshareVertex  // 0x10
  public            MagicaReductionMesh.PolygonLinkReduction.PointnearPoint  // 0x18
  public            System.Single                   nearDist  // 0x20
METHODS:
  RVA=0x041E1670  token=0x60000A4  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000026
SIZE:  0x10
FIELDS:
  public    static readonly MagicaCloth.BaseCloth.<>c       <>9  // static @ 0x0
  public    static  System.Func<UnityEngine.Renderer,System.Boolean><>9__106_0  // static @ 0x8
METHODS:
  RVA=0x09EEA808  token=0x600011B  System.Void .cctor()
  RVA=0x041E1670  token=0x600011C  System.Void .ctor()
  RVA=0x09EEA438  token=0x600011D  System.Boolean <GetCullRenderListCount>b__106_0(UnityEngine.Renderer x)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000030
SIZE:  0x10
FIELDS:
  public    static readonly MagicaCloth.MagicaPhysicsManager.<>c<>9  // static @ 0x0
  public    static  UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction<>9__69_11  // static @ 0x8
  public    static  UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction<>9__69_12  // static @ 0x10
  public    static  UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction<>9__69_13  // static @ 0x18
  public    static  UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction<>9__69_14  // static @ 0x20
  public    static  UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction<>9__69_15  // static @ 0x28
  public    static  UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction<>9__69_16  // static @ 0x30
  public    static  UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction<>9__69_17  // static @ 0x38
  public    static  System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem><>9__69_0  // static @ 0x40
  public    static  System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem><>9__69_1  // static @ 0x48
  public    static  System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem><>9__69_2  // static @ 0x50
  public    static  System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem><>9__69_3  // static @ 0x58
  public    static  System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem><>9__69_4  // static @ 0x60
  public    static  System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem><>9__69_5  // static @ 0x68
  public    static  System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem><>9__69_6  // static @ 0x70
  public    static  System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem><>9__69_7  // static @ 0x78
  public    static  System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem><>9__69_8  // static @ 0x80
  public    static  System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem><>9__69_9  // static @ 0x88
  public    static  System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem><>9__69_10  // static @ 0x90
METHODS:
  RVA=0x04D46B10  token=0x60001AF  System.Void .cctor()
  RVA=0x041E1670  token=0x60001B0  System.Void .ctor()
  RVA=0x03234FB0  token=0x60001B1  System.Void <SetCustomGameLoop>b__69_11()
  RVA=0x03235010  token=0x60001B2  System.Void <SetCustomGameLoop>b__69_12()
  RVA=0x03234DF0  token=0x60001B3  System.Void <SetCustomGameLoop>b__69_13()
  RVA=0x03234E60  token=0x60001B4  System.Void <SetCustomGameLoop>b__69_14()
  RVA=0x03234EB0  token=0x60001B5  System.Void <SetCustomGameLoop>b__69_15()
  RVA=0x03234F00  token=0x60001B6  System.Void <SetCustomGameLoop>b__69_16()
  RVA=0x03234F50  token=0x60001B7  System.Void <SetCustomGameLoop>b__69_17()
  RVA=0x04294250  token=0x60001B8  System.Boolean <SetCustomGameLoop>b__69_0(UnityEngine.LowLevel.PlayerLoopSystem s)
  RVA=0x04294140  token=0x60001B9  System.Boolean <SetCustomGameLoop>b__69_1(UnityEngine.LowLevel.PlayerLoopSystem s)
  RVA=0x042940D0  token=0x60001BA  System.Boolean <SetCustomGameLoop>b__69_2(UnityEngine.LowLevel.PlayerLoopSystem h)
  RVA=0x04294070  token=0x60001BB  System.Boolean <SetCustomGameLoop>b__69_3(UnityEngine.LowLevel.PlayerLoopSystem s)
  RVA=0x042942B0  token=0x60001BC  System.Boolean <SetCustomGameLoop>b__69_4(UnityEngine.LowLevel.PlayerLoopSystem h)
  RVA=0x04294010  token=0x60001BD  System.Boolean <SetCustomGameLoop>b__69_5(UnityEngine.LowLevel.PlayerLoopSystem s)
  RVA=0x04293E70  token=0x60001BE  System.Boolean <SetCustomGameLoop>b__69_6(UnityEngine.LowLevel.PlayerLoopSystem h)
  RVA=0x04293FB0  token=0x60001BF  System.Boolean <SetCustomGameLoop>b__69_7(UnityEngine.LowLevel.PlayerLoopSystem s)
  RVA=0x042941A0  token=0x60001C0  System.Boolean <SetCustomGameLoop>b__69_8(UnityEngine.LowLevel.PlayerLoopSystem h)
  RVA=0x04293F50  token=0x60001C1  System.Boolean <SetCustomGameLoop>b__69_9(UnityEngine.LowLevel.PlayerLoopSystem s)
  RVA=0x045F40F0  token=0x60001C2  System.Boolean <SetCustomGameLoop>b__69_10(UnityEngine.LowLevel.PlayerLoopSystem h)
END_CLASS

CLASS: <>c__DisplayClass70_0
TYPE:  sealed class
TOKEN: 0x2000031
SIZE:  0x20
FIELDS:
  public            System.Type                     t  // 0x10
  public            System.Func<UnityEngine.LowLevel.PlayerLoopSystem,System.Boolean><>9__0  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001C3  System.Void .ctor()
  RVA=0x044E7120  token=0x60001C4  System.Boolean <CheckRegist>b__0(UnityEngine.LowLevel.PlayerLoopSystem x)
END_CLASS

CLASS: <>c__DisplayClass20_0
TYPE:  sealed class
TOKEN: 0x2000036
SIZE:  0x18
FIELDS:
  public            System.Int32                    partsId  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000220  System.Void .ctor()
  RVA=0x09EEA7D8  token=0x6000221  System.Boolean <RemoveAvatarParts>b__0(MagicaCloth.MagicaAvatarParts p)
END_CLASS

CLASS: ConnectionMode
TYPE:  sealed struct
TOKEN: 0x2000038
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.BoneClothTarget.ConnectionModeLine  // const
  public    static  MagicaCloth.BoneClothTarget.ConnectionModeMeshAutomatic  // const
  public    static  MagicaCloth.BoneClothTarget.ConnectionModeMeshSequentialLoop  // const
  public    static  MagicaCloth.BoneClothTarget.ConnectionModeMeshSequentialNoLoop  // const
METHODS:
END_CLASS

CLASS: RestoreDistanceWork
TYPE:  class
TOKEN: 0x200003A
SIZE:  0x18
FIELDS:
  public            System.UInt32                   vertexPair  // 0x10
  public            System.Single                   dist  // 0x14
METHODS:
  RVA=0x041E1670  token=0x600025D  System.Void .ctor()
END_CLASS

CLASS: PenetrationBone
TYPE:  class
TOKEN: 0x200003B
SIZE:  0x20
FIELDS:
  public            UnityEngine.Transform           bone  // 0x10
  public            UnityEngine.Transform           childBone  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600025E  System.Void .ctor()
END_CLASS

CLASS: PenetrationWork
TYPE:  class
TOKEN: 0x200003C
SIZE:  0x30
FIELDS:
  public            UnityEngine.Transform           bone  // 0x10
  public            UnityEngine.Transform           childBone  // 0x18
  public            System.Int32                    boneIndex  // 0x20
  public            System.Single                   distance  // 0x24
  public            System.Single                   weight  // 0x28
METHODS:
  RVA=0x041E1670  token=0x600025F  System.Void .ctor()
END_CLASS

CLASS: VertexInfo
TYPE:  class
TOKEN: 0x200003D
SIZE:  0x30
FIELDS:
  public            System.Int32                    vertexIndex  // 0x10
  public            System.Int32                    parentVertexIndex  // 0x14
  public            System.Collections.Generic.List<System.Int32>childVertexList  // 0x18
  public            MagicaCloth.ClothData.VertexInfoparentInfo  // 0x20
  public            System.Collections.Generic.List<MagicaCloth.ClothData.VertexInfo>childInfoList  // 0x28
METHODS:
  RVA=0x09EEA8D0  token=0x6000260  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200003E
SIZE:  0x10
FIELDS:
  public    static readonly MagicaCloth.ClothData.<>c       <>9  // static @ 0x0
  public    static  System.Comparison<MagicaCloth.ClothData.RestoreDistanceWork><>9__108_0  // static @ 0x8
  public    static  System.Comparison<MagicaCloth.ClothData.RestoreDistanceWork><>9__108_1  // static @ 0x10
  public    static  System.Comparison<MagicaCloth.PenetrationConstraint.PenetrationData><>9__108_2  // static @ 0x18
METHODS:
  RVA=0x09EEA86C  token=0x6000261  System.Void .cctor()
  RVA=0x041E1670  token=0x6000262  System.Void .ctor()
  RVA=0x09EEA3F0  token=0x6000263  System.Int32 <CreateConstraintData>b__108_0(MagicaCloth.ClothData.RestoreDistanceWork a, MagicaCloth.ClothData.RestoreDistanceWork b)
  RVA=0x09EEA3F0  token=0x6000264  System.Int32 <CreateConstraintData>b__108_1(MagicaCloth.ClothData.RestoreDistanceWork a, MagicaCloth.ClothData.RestoreDistanceWork b)
  RVA=0x09EEA420  token=0x6000265  System.Int32 <CreateConstraintData>b__108_2(MagicaCloth.PenetrationConstraint.PenetrationData a, MagicaCloth.PenetrationConstraint.PenetrationData b)
END_CLASS

CLASS: <>c__DisplayClass110_0
TYPE:  sealed class
TOKEN: 0x200003F
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.List<System.Single>meshVertexDepthList  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000266  System.Void .ctor()
  RVA=0x09EEA484  token=0x6000267  System.Int32 <SortTetra>b__0(System.Int32 a, System.Int32 b)
END_CLASS

CLASS: <>c__DisplayClass111_0
TYPE:  sealed class
TOKEN: 0x2000040
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.List<System.Single>meshVertexDepthList  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000268  System.Void .ctor()
  RVA=0x09EEA520  token=0x6000269  System.Int32 <CheckTetraDirection>b__0(System.UInt64 a, System.UInt64 b)
END_CLASS

CLASS: <>c__DisplayClass114_0
TYPE:  sealed class
TOKEN: 0x2000041
SIZE:  0x20
FIELDS:
  public            System.Collections.Generic.List<System.Single>depthList  // 0x10
  public            System.Collections.Generic.List<System.Single>nearFixedDistList  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600026A  System.Void .ctor()
  RVA=0x09EEA6CC  token=0x600026B  System.Int32 <GetUseParentVertexList>b__0(System.Int32 a, System.Int32 b)
END_CLASS

CLASS: Algorithm
TYPE:  sealed struct
TOKEN: 0x2000043
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.ClothParams.AlgorithmAlgorithm_1  // const
  public    static  MagicaCloth.ClothParams.AlgorithmAlgorithm_2  // const
METHODS:
END_CLASS

CLASS: TeleportMode
TYPE:  sealed struct
TOKEN: 0x2000044
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.ClothParams.TeleportModeReset  // const
  public    static  MagicaCloth.ClothParams.TeleportModeKeep  // const
METHODS:
END_CLASS

CLASS: AdjustMode
TYPE:  sealed struct
TOKEN: 0x2000045
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.ClothParams.AdjustModeFixed  // const
  public    static  MagicaCloth.ClothParams.AdjustModeXYMove  // const
  public    static  MagicaCloth.ClothParams.AdjustModeXZMove  // const
  public    static  MagicaCloth.ClothParams.AdjustModeYZMove  // const
METHODS:
END_CLASS

CLASS: PenetrationMode
TYPE:  sealed struct
TOKEN: 0x2000046
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.ClothParams.PenetrationModeSurfacePenetration  // const
  public    static  MagicaCloth.ClothParams.PenetrationModeColliderPenetration  // const
METHODS:
END_CLASS

CLASS: PenetrationAxis
TYPE:  sealed struct
TOKEN: 0x2000047
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.ClothParams.PenetrationAxisX  // const
  public    static  MagicaCloth.ClothParams.PenetrationAxisY  // const
  public    static  MagicaCloth.ClothParams.PenetrationAxisZ  // const
  public    static  MagicaCloth.ClothParams.PenetrationAxisInverseX  // const
  public    static  MagicaCloth.ClothParams.PenetrationAxisInverseY  // const
  public    static  MagicaCloth.ClothParams.PenetrationAxisInverseZ  // const
METHODS:
END_CLASS

CLASS: ParamType
TYPE:  sealed struct
TOKEN: 0x2000048
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.ClothParams.ParamTypeRadius  // const
  public    static  MagicaCloth.ClothParams.ParamTypeMass  // const
  public    static  MagicaCloth.ClothParams.ParamTypeGravity  // const
  public    static  MagicaCloth.ClothParams.ParamTypeDrag  // const
  public    static  MagicaCloth.ClothParams.ParamTypeMaxVelocity  // const
  public    static  MagicaCloth.ClothParams.ParamTypeWorldInfluence  // const
  public    static  MagicaCloth.ClothParams.ParamTypeClampDistance  // const
  public    static  MagicaCloth.ClothParams.ParamTypeClampPosition  // const
  public    static  MagicaCloth.ClothParams.ParamTypeClampRotation  // const
  public    static  MagicaCloth.ClothParams.ParamTypeRestoreDistance  // const
  public    static  MagicaCloth.ClothParams.ParamTypeRestoreRotation  // const
  public    static  MagicaCloth.ClothParams.ParamTypeSpring  // const
  public    static  MagicaCloth.ClothParams.ParamTypeAdjustRotation  // const
  public    static  MagicaCloth.ClothParams.ParamTypeAirLine  // const
  public    static  MagicaCloth.ClothParams.ParamTypeTriangleBend  // const
  public    static  MagicaCloth.ClothParams.ParamTypeVolume  // const
  public    static  MagicaCloth.ClothParams.ParamTypeColliderCollision  // const
  public    static  MagicaCloth.ClothParams.ParamTypeRotationInterpolation  // const
  public    static  MagicaCloth.ClothParams.ParamTypeDistanceDisable  // const
  public    static  MagicaCloth.ClothParams.ParamTypeExternalForce  // const
  public    static  MagicaCloth.ClothParams.ParamTypePenetration  // const
  public    static  MagicaCloth.ClothParams.ParamTypeAlgorithm  // const
  public    static  MagicaCloth.ClothParams.ParamTypeBaseSkinning  // const
  public    static  MagicaCloth.ClothParams.ParamTypeMax  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x200004A
SIZE:  0x28
FIELDS:
  public            MagicaCloth.ClothData           clothData  // 0x10
  public            System.Func<System.Int32,System.UInt32>funcUserFlag  // 0x18
  public            MagicaCloth.ClothParams         param  // 0x20
METHODS:
  RVA=0x041E1670  token=0x60002FC  System.Void .ctor()
  RVA=0x09EFC63C  token=0x60002FD  System.UInt32 <ClothInit>b__0(System.Int32 i)
  RVA=0x09EFC6FC  token=0x60002FE  System.Single <ClothInit>b__1(System.Int32 i)
  RVA=0x09EFC728  token=0x60002FF  Unity.Mathematics.float3 <ClothInit>b__2(System.Int32 i)
END_CLASS

CLASS: Axis
TYPE:  sealed struct
TOKEN: 0x200004F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.MagicaMeshSpring.AxisX  // const
  public    static  MagicaCloth.MagicaMeshSpring.AxisY  // const
  public    static  MagicaCloth.MagicaMeshSpring.AxisZ  // const
  public    static  MagicaCloth.MagicaMeshSpring.AxisInverseX  // const
  public    static  MagicaCloth.MagicaMeshSpring.AxisInverseY  // const
  public    static  MagicaCloth.MagicaMeshSpring.AxisInverseZ  // const
METHODS:
END_CLASS

CLASS: DeformerSelection
TYPE:  class
TOKEN: 0x2000051
SIZE:  0x20
IMPLEMENTS: MagicaCloth.IDataHash
FIELDS:
  public            System.Collections.Generic.List<System.Int32>selectData  // 0x10
  public            System.Collections.Generic.List<System.UInt64>vertexHashList  // 0x18
METHODS:
  RVA=0x09EEF388  token=0x60003A6  System.Int32 GetDataHash()
  RVA=0x09EEF234  token=0x60003A7  System.Boolean Compare(MagicaCloth.SelectionData.DeformerSelection data)
  RVA=0x09EEF3C4  token=0x60003A8  System.Void .ctor()
END_CLASS

CLASS: DeformerData
TYPE:  class
TOKEN: 0x2000053
SIZE:  0x28
IMPLEMENTS: MagicaCloth.IDataHash
FIELDS:
  public            System.Int32                    deformerDataHash  // 0x10
  public            System.Int32                    vertexCount  // 0x14
  public            System.Int32[]                  useVertexIndexList  // 0x18
  public            System.Single[]                 weightList  // 0x20
PROPERTIES:
  UseVertexCount  get=0x09EEF220
METHODS:
  RVA=0x09EEF18C  token=0x60003AF  System.Int32 GetDataHash()
  RVA=0x041E1670  token=0x60003B0  System.Void .ctor()
END_CLASS

CLASS: Compute
TYPE:  static class
TOKEN: 0x2000055
SIZE:  0x10
FIELDS:
  public    static  System.Single                   Epsilon  // const
  public    static  System.Single                   CollisionFrictionRange  // const
  public    static  System.Single                   FrictionDampingRate  // const
  public    static  System.Single                   FrictionMoveRatio  // const
  public    static  System.Single                   FrictionPower  // const
  public    static  System.Single                   ClampPositionMaxVelocity  // const
  public    static  System.Single                   GlobalColliderMaxMoveDistance  // const
  public    static  System.Single                   GlobalColliderMaxRotationAngle  // const
  public    static  System.Single                   ColliderExtrusionMaxPower  // const
  public    static  System.Single                   ColliderExtrusionDirectionPower  // const
  public    static  System.Single                   ColliderExtrusionDistPower  // const
  public    static  System.Single                   ColliderExtrusionVelocityInfluence  // const
  public    static  System.Single                   MaxWindMain  // const
  public    static  System.Single                   ClampRotationMaxVelocity  // const
  public    static  System.Single                   ClampRotationMaxVelocity2  // const
  public    static  System.Single                   TriangleBendVelocityInfluence  // const
METHODS:
END_CLASS

CLASS: Error
TYPE:  sealed struct
TOKEN: 0x2000056
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.Define.Error        None  // const
  public    static  MagicaCloth.Define.Error        Cancel  // const
  public    static  MagicaCloth.Define.Error        EmptyData  // const
  public    static  MagicaCloth.Define.Error        InvalidDataHash  // const
  public    static  MagicaCloth.Define.Error        TooOldDataVersion  // const
  public    static  MagicaCloth.Define.Error        HigherDataVersion  // const
  public    static  MagicaCloth.Define.Error        MeshDataNull  // const
  public    static  MagicaCloth.Define.Error        MeshDataHashMismatch  // const
  public    static  MagicaCloth.Define.Error        MeshDataVersionMismatch  // const
  public    static  MagicaCloth.Define.Error        ClothDataNull  // const
  public    static  MagicaCloth.Define.Error        ClothDataHashMismatch  // const
  public    static  MagicaCloth.Define.Error        ClothDataVersionMismatch  // const
  public    static  MagicaCloth.Define.Error        ClothSelectionHashMismatch  // const
  public    static  MagicaCloth.Define.Error        ClothSelectionVersionMismatch  // const
  public    static  MagicaCloth.Define.Error        ClothTargetRootCountMismatch  // const
  public    static  MagicaCloth.Define.Error        UseTransformNull  // const
  public    static  MagicaCloth.Define.Error        UseTransformCountZero  // const
  public    static  MagicaCloth.Define.Error        UseTransformCountMismatch  // const
  public    static  MagicaCloth.Define.Error        DeformerNull  // const
  public    static  MagicaCloth.Define.Error        DeformerHashMismatch  // const
  public    static  MagicaCloth.Define.Error        DeformerVersionMismatch  // const
  public    static  MagicaCloth.Define.Error        DeformerCountZero  // const
  public    static  MagicaCloth.Define.Error        DeformerCountMismatch  // const
  public    static  MagicaCloth.Define.Error        VertexCountZero  // const
  public    static  MagicaCloth.Define.Error        VertexUseCountZero  // const
  public    static  MagicaCloth.Define.Error        VertexCountMismatch  // const
  public    static  MagicaCloth.Define.Error        RootListCountMismatch  // const
  public    static  MagicaCloth.Define.Error        SelectionDataCountMismatch  // const
  public    static  MagicaCloth.Define.Error        SelectionCountZero  // const
  public    static  MagicaCloth.Define.Error        CenterTransformNull  // const
  public    static  MagicaCloth.Define.Error        SpringDataNull  // const
  public    static  MagicaCloth.Define.Error        SpringDataHashMismatch  // const
  public    static  MagicaCloth.Define.Error        SpringDataVersionMismatch  // const
  public    static  MagicaCloth.Define.Error        TargetObjectNull  // const
  public    static  MagicaCloth.Define.Error        SharedMeshNull  // const
  public    static  MagicaCloth.Define.Error        SharedMeshCannotRead  // const
  public    static  MagicaCloth.Define.Error        SharedMeshDifferent  // const
  public    static  MagicaCloth.Define.Error        SharedMeshDifferentVertexCount  // const
  public    static  MagicaCloth.Define.Error        MeshOptimizeMismatch  // const
  public    static  MagicaCloth.Define.Error        MeshVertexCount65535Over  // const
  public    static  MagicaCloth.Define.Error        MeshKeepQuads  // const
  public    static  MagicaCloth.Define.Error        BoneListZero  // const
  public    static  MagicaCloth.Define.Error        BoneListNull  // const
  public    static  MagicaCloth.Define.Error        RendererNotFound  // const
  public    static  MagicaCloth.Define.Error        MeshFilterNotFound  // const
  public    static  MagicaCloth.Define.Error        BuildNoTransformList  // const
  public    static  MagicaCloth.Define.Error        BuildReadOnlyPrefab  // const
  public    static  MagicaCloth.Define.Error        BuildFailedSaveAssets  // const
  public    static  MagicaCloth.Define.Error        BuildPrefabCannotSaved  // const
  public    static  MagicaCloth.Define.Error        BuildNotSceneObject  // const
  public    static  MagicaCloth.Define.Error        BuildInvalidComponent  // const
  public    static  MagicaCloth.Define.Error        BuildInvalidData  // const
  public    static  MagicaCloth.Define.Error        BuildInvalidMeshData  // const
  public    static  MagicaCloth.Define.Error        BuildInvalidGameObject  // const
  public    static  MagicaCloth.Define.Error        BuildInvalidPrefab  // const
  public    static  MagicaCloth.Define.Error        BuildInvalidRenderDeformer  // const
  public    static  MagicaCloth.Define.Error        BuildInvalidScene  // const
  public    static  MagicaCloth.Define.Error        BuildInvalidSelection  // const
  public    static  MagicaCloth.Define.Error        BuildMissingDeformer  // const
  public    static  MagicaCloth.Define.Error        BuildMissingSelection  // const
  public    static  MagicaCloth.Define.Error        BuildMissingMesh  // const
  public    static  MagicaCloth.Define.Error        BuildMissingScriptOnPrefab  // const
  public    static  MagicaCloth.Define.Error        OverlappingTransform  // const
  public    static  MagicaCloth.Define.Error        AddOverlappingTransform  // const
  public    static  MagicaCloth.Define.Error        OldDataVersion  // const
  public    static  MagicaCloth.Define.Error        OldAlgorithm  // const
METHODS:
END_CLASS

CLASS: OptimizeMesh
TYPE:  static class
TOKEN: 0x2000057
SIZE:  0x10
FIELDS:
  public    static  System.Int32                    Unknown  // const
  public    static  System.Int32                    Nothing  // const
  public    static  System.Int32                    Unity2018_On  // const
  public    static  System.Int32                    Unity2019_PolygonOrder  // const
  public    static  System.Int32                    Unity2019_VertexOrder  // const
METHODS:
END_CLASS

CLASS: RenderMesh
TYPE:  static class
TOKEN: 0x2000058
SIZE:  0x10
FIELDS:
  public    static  System.Int32                    WorkerMultiplesOfVertexCollection  // const
METHODS:
END_CLASS

CLASS: VertexWeight
TYPE:  sealed struct
TOKEN: 0x200005D
SIZE:  0x3C
FIELDS:
  public            UnityEngine.Vector3             localPos  // 0x10
  public            UnityEngine.Vector3             localNor  // 0x1c
  public            UnityEngine.Vector3             localTan  // 0x28
  public            System.Int32                    parentIndex  // 0x34
  public            System.Single                   weight  // 0x38
METHODS:
END_CLASS

CLASS: ChildData
TYPE:  class
TOKEN: 0x200005E
SIZE:  0x30
IMPLEMENTS: MagicaCloth.IDataHash
FIELDS:
  public            System.Int32                    childDataHash  // 0x10
  public            System.Int32                    vertexCount  // 0x14
  public            System.UInt32[]                 vertexInfoList  // 0x18
  public            MagicaCloth.MeshData.VertexWeight[]vertexWeightList  // 0x20
  public            System.Int32[]                  parentIndexList  // 0x28
PROPERTIES:
  VertexCount  get=0x04D882A0
METHODS:
  RVA=0x09EEBEDC  token=0x600042E  System.Int32 GetDataHash()
  RVA=0x041E1670  token=0x600042F  System.Void .ctor()
END_CLASS

CLASS: RecalculateMode
TYPE:  sealed struct
TOKEN: 0x2000060
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.RenderMeshDeformer.RecalculateModeNone  // const
  public    static  MagicaCloth.RenderMeshDeformer.RecalculateModeUpdateNormalPerFrame  // const
  public    static  MagicaCloth.RenderMeshDeformer.RecalculateModeUpdateNormalAndTangentPerFrame  // const
METHODS:
END_CLASS

CLASS: BoundsMode
TYPE:  sealed struct
TOKEN: 0x2000061
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.RenderMeshDeformer.BoundsModeNone  // const
  public    static  MagicaCloth.RenderMeshDeformer.BoundsModeExpandedAtInitialization  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass35_0
TYPE:  sealed class
TOKEN: 0x2000063
SIZE:  0x18
FIELDS:
  public            MagicaCloth.RenderMeshDeformer  deformer  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000483  System.Void .ctor()
  RVA=0x09F10814  token=0x6000484  System.Boolean <GetRenderMeshDeformerIndex>b__0(MagicaCloth.MagicaRenderDeformer d)
END_CLASS

CLASS: ClampDistance2Data
TYPE:  sealed struct
TOKEN: 0x2000065
SIZE:  0x1C
FIELDS:
  public            System.Int32                    vertexIndex  // 0x10
  public            System.Int32                    parentVertexIndex  // 0x14
  public            System.Single                   length  // 0x18
METHODS:
END_CLASS

CLASS: ClampDistance2RootInfo
TYPE:  sealed struct
TOKEN: 0x2000066
SIZE:  0x14
FIELDS:
  public            System.UInt16                   startIndex  // 0x10
  public            System.UInt16                   dataLength  // 0x12
METHODS:
END_CLASS

CLASS: GroupData
TYPE:  sealed struct
TOKEN: 0x2000067
SIZE:  0x44
FIELDS:
  public            System.Int32                    teamId  // 0x10
  public            System.Int32                    active  // 0x14
  public            System.Single                   minRatio  // 0x18
  public            System.Single                   maxRatio  // 0x1c
  public            System.Single                   velocityInfluence  // 0x20
  public            MagicaCloth.ChunkData           dataChunk  // 0x24
  public            MagicaCloth.ChunkData           rootInfoChunk  // 0x34
METHODS:
END_CLASS

CLASS: ClampDistance2Job
TYPE:  sealed struct
TOKEN: 0x2000068
SIZE:  0xA8
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Int32                    runCount  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.ClampDistance2Constraint.ClampDistance2Data>dataList  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.ClampDistance2Constraint.ClampDistance2RootInfo>rootInfoList  // 0x28
  public            Unity.Collections.NativeArray<System.Int32>rootTeamList  // 0x38
  public            Unity.Collections.NativeArray<MagicaCloth.ClampDistance2Constraint.GroupData>groupList  // 0x48
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x58
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x68
  public            Unity.Collections.NativeArray<System.Single>frictionList  // 0x78
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0x88
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0x98
METHODS:
  RVA=0x09EFFC40  token=0x600048C  System.Void Execute(System.Int32 rootIndex)
END_CLASS

CLASS: ClampDistanceData
TYPE:  sealed struct
TOKEN: 0x200006A
SIZE:  0x18
FIELDS:
  public            System.UInt16                   vertexIndex  // 0x10
  public            System.UInt16                   targetVertexIndex  // 0x12
  public            System.Single                   length  // 0x14
METHODS:
  RVA=0x09F00928  token=0x6000494  System.Boolean IsValid()
END_CLASS

CLASS: GroupData
TYPE:  sealed struct
TOKEN: 0x200006B
SIZE:  0x44
FIELDS:
  public            System.Int32                    teamId  // 0x10
  public            System.Int32                    active  // 0x14
  public            System.Single                   minRatio  // 0x18
  public            System.Single                   maxRatio  // 0x1c
  public            System.Single                   velocityInfluence  // 0x20
  public            MagicaCloth.ChunkData           dataChunk  // 0x24
  public            MagicaCloth.ChunkData           refChunk  // 0x34
METHODS:
END_CLASS

CLASS: ClampDistanceJob
TYPE:  sealed struct
TOKEN: 0x200006C
SIZE:  0xB8
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Int32                    runCount  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.ClampDistanceConstraint.ClampDistanceData>clampDistanceList  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.ClampDistanceConstraint.GroupData>groupList  // 0x28
  public            Unity.Collections.NativeArray<MagicaCloth.ReferenceDataIndex>refDataList  // 0x38
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x48
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x58
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x68
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0x78
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>basePosList  // 0x88
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0x98
  public            Unity.Collections.NativeArray<System.Single>frictionList  // 0xa8
METHODS:
  RVA=0x09F0093C  token=0x6000495  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: GroupData
TYPE:  sealed struct
TOKEN: 0x200006E
SIZE:  0x38
FIELDS:
  public            System.Int32                    teamId  // 0x10
  public            System.Int32                    active  // 0x14
  public            MagicaCloth.CurveParam          limitLength  // 0x18
  public            Unity.Mathematics.float3        axisRatio  // 0x28
  public            System.Single                   velocityInfluence  // 0x34
METHODS:
  RVA=0x09F07FDC  token=0x600049D  System.Boolean IsAxisCheck()
END_CLASS

CLASS: ClampPositionJob
TYPE:  sealed struct
TOKEN: 0x200006F
SIZE:  0xB8
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Int32                    runCount  // 0x10
  public            System.Single                   maxMoveLength  // 0x14
  public            Unity.Collections.NativeArray<MagicaCloth.ClampPositionConstraint.GroupData>clampPositionGroupList  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x28
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x38
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x48
  public            Unity.Collections.NativeArray<System.Single>depthList  // 0x58
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>basePosList  // 0x68
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>baseRotList  // 0x78
  public            Unity.Collections.NativeArray<System.Single>frictionList  // 0x88
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0x98
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0xa8
METHODS:
  RVA=0x09F01650  token=0x600049E  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: ClampRotationData
TYPE:  sealed struct
TOKEN: 0x2000071
SIZE:  0x34
FIELDS:
  public            System.Int32                    vertexIndex  // 0x10
  public            System.Int32                    parentVertexIndex  // 0x14
  public            Unity.Mathematics.float3        localPos  // 0x18
  public            Unity.Mathematics.quaternion    localRot  // 0x24
METHODS:
  RVA=0x09F02704  token=0x60004A6  System.Boolean IsValid()
END_CLASS

CLASS: ClampRotationRootInfo
TYPE:  sealed struct
TOKEN: 0x2000072
SIZE:  0x14
FIELDS:
  public            System.UInt16                   startIndex  // 0x10
  public            System.UInt16                   dataLength  // 0x12
METHODS:
END_CLASS

CLASS: GroupData
TYPE:  sealed struct
TOKEN: 0x2000073
SIZE:  0x4C
FIELDS:
  public            System.Int32                    teamId  // 0x10
  public            System.Int32                    active  // 0x14
  public            MagicaCloth.CurveParam          maxAngle  // 0x18
  public            System.Single                   velocityInfluence  // 0x28
  public            MagicaCloth.ChunkData           dataChunk  // 0x2c
  public            MagicaCloth.ChunkData           rootInfoChunk  // 0x3c
METHODS:
END_CLASS

CLASS: ClampRotationJob
TYPE:  sealed struct
TOKEN: 0x2000074
SIZE:  0xD8
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Int32                    runCount  // 0x10
  public            System.Single                   maxMoveLength  // 0x14
  public            Unity.Collections.NativeArray<MagicaCloth.ClampRotationConstraint.ClampRotationData>dataList  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.ClampRotationConstraint.ClampRotationRootInfo>rootInfoList  // 0x28
  public            Unity.Collections.NativeArray<System.Int32>rootTeamList  // 0x38
  public            Unity.Collections.NativeArray<MagicaCloth.ClampRotationConstraint.GroupData>groupList  // 0x48
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x58
  public            Unity.Collections.NativeArray<System.Single>depthList  // 0x68
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x78
  public            Unity.Collections.NativeArray<System.Single>frictionList  // 0x88
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0x98
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>nextRotList  // 0xa8
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0xb8
  public            Unity.Collections.NativeArray<System.Single>lengthBuffer  // 0xc8
METHODS:
  RVA=0x09F02714  token=0x60004A7  System.Void Execute(System.Int32 rootIndex)
END_CLASS

CLASS: CollisionJob
TYPE:  sealed struct
TOKEN: 0x2000076
SIZE:  0x128
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Int32                    runCount  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x18
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x28
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>radiusList  // 0x38
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0x48
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>nextRotList  // 0x58
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0x68
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>rotList  // 0x78
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>localPosList  // 0x88
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>basePosList  // 0x98
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>baseRotList  // 0xa8
  public            Unity.Collections.NativeArray<System.Int32>transformIndexList  // 0xb8
  public            Unity.Collections.NativeArray<System.Int32>colliderList  // 0xc8
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>boneSclList  // 0xd8
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0xe8
  public            Unity.Collections.NativeArray<System.Single>frictionList  // 0xf8
  public            Unity.Collections.NativeArray<System.Int32>collisionLinkIdList  // 0x108
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>collisionNormalList  // 0x118
METHODS:
  RVA=0x09F04348  token=0x60004AE  System.Void Execute(System.Int32 index)
  RVA=0x09F0492C  token=0x60004AF  System.Single SphereColliderDetection(Unity.Mathematics.float3& nextpos, System.Single radius, System.Int32 cindex, Unity.Mathematics.float3& normal)
  RVA=0x09F03E94  token=0x60004B0  System.Single CapsuleColliderDetection(Unity.Mathematics.float3& nextpos, System.Single radius, System.Int32 cindex, Unity.Mathematics.float3 dir, Unity.Mathematics.float3& normal)
  RVA=0x09F04808  token=0x60004B1  System.Single PlaneColliderDetection(Unity.Mathematics.float3& nextpos, System.Single radius, System.Int32 cindex, Unity.Mathematics.float3& normal)
END_CLASS

CLASS: CollisionExtrusionJob
TYPE:  sealed struct
TOKEN: 0x2000078
SIZE:  0xB8
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Int32                    runCount  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x18
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x28
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0x38
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>nextRotList  // 0x48
  public            Unity.Collections.NativeArray<System.Int32>collisionLinkIdList  // 0x58
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0x68
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>rotList  // 0x78
  public            Unity.Collections.NativeArray<System.Single>frictionList  // 0x88
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>collisionNormalList  // 0x98
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0xa8
METHODS:
  RVA=0x09F03A18  token=0x60004B7  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: RotationData
TYPE:  sealed struct
TOKEN: 0x200007A
SIZE:  0x34
FIELDS:
  public            System.Int32                    vertexIndex  // 0x10
  public            System.Int32                    parentVertexIndex  // 0x14
  public            Unity.Mathematics.float3        localPos  // 0x18
  public            Unity.Mathematics.quaternion    localRot  // 0x24
METHODS:
  RVA=0x09F02704  token=0x60004BF  System.Boolean IsValid()
END_CLASS

CLASS: RootInfo
TYPE:  sealed struct
TOKEN: 0x200007B
SIZE:  0x14
FIELDS:
  public            System.UInt16                   startIndex  // 0x10
  public            System.UInt16                   dataLength  // 0x12
METHODS:
END_CLASS

CLASS: GroupData
TYPE:  sealed struct
TOKEN: 0x200007C
SIZE:  0x60
FIELDS:
  public            System.Int32                    teamId  // 0x10
  public            System.Int32                    useClamp  // 0x14
  public            System.Int32                    useRestore  // 0x18
  public            MagicaCloth.CurveParam          maxAngle  // 0x1c
  public            MagicaCloth.CurveParam          restorePower  // 0x2c
  public            System.Single                   restoreVelocityInfluence  // 0x3c
  public            MagicaCloth.ChunkData           dataChunk  // 0x40
  public            MagicaCloth.ChunkData           rootInfoChunk  // 0x50
METHODS:
END_CLASS

CLASS: RotationRootLineJob
TYPE:  sealed struct
TOKEN: 0x200007D
SIZE:  0x110
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Single                   updatePower  // 0x10
  public            System.Int32                    runCount  // 0x14
  public            System.Single                   maxMoveSpeed  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.CompositeRotationConstraint.RotationData>dataList  // 0x20
  public            Unity.Collections.NativeArray<MagicaCloth.CompositeRotationConstraint.RootInfo>rootInfoList  // 0x30
  public            Unity.Collections.NativeArray<System.Int32>rootTeamList  // 0x40
  public            Unity.Collections.NativeArray<MagicaCloth.CompositeRotationConstraint.GroupData>groupList  // 0x50
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x60
  public            Unity.Collections.NativeArray<MagicaCloth.CurveParam>teamGravityList  // 0x70
  public            Unity.Collections.NativeArray<System.Single>depthList  // 0x80
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x90
  public            Unity.Collections.NativeArray<System.Single>frictionList  // 0xa0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>basePosList  // 0xb0
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>baseRotList  // 0xc0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0xd0
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>nextRotList  // 0xe0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0xf0
  public            Unity.Collections.NativeArray<System.Single>lengthBuffer  // 0x100
METHODS:
  RVA=0x09F0BC5C  token=0x60004C0  System.Void Execute(System.Int32 rootIndex)
  RVA=0x09EFEFD4  token=0x60004C1  System.Single GetRotRatio(Unity.Mathematics.float3 tv, Unity.Mathematics.float3 gravityVector, System.Single gravity, System.Single minRatio, System.Single maxRatio)
END_CLASS

CLASS: EdgeCollisionData
TYPE:  sealed struct
TOKEN: 0x200007F
SIZE:  0x1C
FIELDS:
  public            System.UInt16                   vindex0  // 0x10
  public            System.UInt16                   vindex1  // 0x12
  public            System.Int32                    writeIndex0  // 0x14
  public            System.Int32                    writeIndex1  // 0x18
METHODS:
  RVA=0x09F07C84  token=0x60004C9  System.Boolean IsValid()
END_CLASS

CLASS: GroupData
TYPE:  sealed struct
TOKEN: 0x2000080
SIZE:  0x5C
FIELDS:
  public            System.Int32                    teamId  // 0x10
  public            System.Int32                    active  // 0x14
  public            System.Single                   edgeRadius  // 0x18
  public            MagicaCloth.ChunkData           dataChunk  // 0x1c
  public            MagicaCloth.ChunkData           groupIndexChunk  // 0x2c
  public            MagicaCloth.ChunkData           refDataChunk  // 0x3c
  public            MagicaCloth.ChunkData           writeDataChunk  // 0x4c
METHODS:
END_CLASS

CLASS: EdgeCollisionCalcJob
TYPE:  sealed struct
TOKEN: 0x2000081
SIZE:  0x108
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Single                   updatePower  // 0x10
  public            System.Int32                    runCount  // 0x14
  public            Unity.Collections.NativeArray<MagicaCloth.EdgeCollisionConstraint.GroupData>groupDataList  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.EdgeCollisionConstraint.EdgeCollisionData>dataList  // 0x28
  public            Unity.Collections.NativeArray<System.Int16>groupIndexList  // 0x38
  public            Unity.Collections.NativeArray<System.Int32>colliderList  // 0x48
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x58
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x68
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>radiusList  // 0x78
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0x88
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>rotList  // 0x98
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0xa8
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>nextRotList  // 0xb8
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>localPosList  // 0xc8
  public            Unity.Collections.NativeArray<System.Int32>transformIndexList  // 0xd8
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>boneSclList  // 0xe8
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>writeBuffer  // 0xf8
METHODS:
  RVA=0x09F063F0  token=0x60004CA  System.Void Execute(System.Int32 index)
  RVA=0x09F068C4  token=0x60004CB  System.Boolean SphereColliderDetection(Unity.Mathematics.float3 nextpos0, Unity.Mathematics.float3 nextpos1, Unity.Mathematics.float3& corr0, Unity.Mathematics.float3& corr1, System.Single radius, System.Int32 cindex)
  RVA=0x09F05D5C  token=0x60004CC  System.Boolean CapsuleColliderDetection(Unity.Mathematics.float3 nextpos0, Unity.Mathematics.float3 nextpos1, Unity.Mathematics.float3& corr0, Unity.Mathematics.float3& corr1, System.Single radius, System.Int32 cindex, Unity.Mathematics.float3 dir)
END_CLASS

CLASS: EdgeCollisionSumJob
TYPE:  sealed struct
TOKEN: 0x2000082
SIZE:  0x98
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Int32                    runCount  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.EdgeCollisionConstraint.GroupData>groupDataList  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.ReferenceDataIndex>refDataList  // 0x28
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>writeBuffer  // 0x38
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x48
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x58
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x68
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>inoutNextPosList  // 0x78
  public            Unity.Collections.NativeArray<System.Single>frictionList  // 0x88
METHODS:
  RVA=0x09F07C98  token=0x60004CD  System.Void Execute(System.Int32 pindex)
END_CLASS

CLASS: PenetrationData
TYPE:  sealed struct
TOKEN: 0x2000084
SIZE:  0x30
FIELDS:
  public            System.Int16                    vertexIndex  // 0x10
  public            System.Int16                    colliderIndex  // 0x12
  public            Unity.Mathematics.float3        localPos  // 0x14
  public            Unity.Mathematics.float3        localDir  // 0x20
  public            System.Single                   distance  // 0x2c
METHODS:
  RVA=0x09F08C44  token=0x60004D5  System.Boolean IsValid()
END_CLASS

CLASS: GroupData
TYPE:  sealed struct
TOKEN: 0x2000085
SIZE:  0x70
FIELDS:
  public            System.Int32                    teamId  // 0x10
  public            System.Int32                    active  // 0x14
  public            System.Int32                    mode  // 0x18
  public            System.Single                   maxDepth  // 0x1c
  public            MagicaCloth.CurveParam          radius  // 0x20
  public            MagicaCloth.CurveParam          distance  // 0x30
  public            MagicaCloth.ChunkData           dataChunk  // 0x40
  public            MagicaCloth.ChunkData           refDataChunk  // 0x50
  public            MagicaCloth.ChunkData           bonePenetrationDataChunk  // 0x60
METHODS:
END_CLASS

CLASS: PenetrationJob
TYPE:  sealed struct
TOKEN: 0x2000086
SIZE:  0x158
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Int32                    runCount  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.PenetrationConstraint.GroupData>groupList  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.PenetrationConstraint.PenetrationData>dataList  // 0x28
  public            Unity.Collections.NativeArray<MagicaCloth.ReferenceDataIndex>refDataList  // 0x38
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>bonePenetrationDataList  // 0x48
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x58
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x68
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0x78
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>nextRotList  // 0x88
  public            Unity.Collections.NativeArray<System.Int32>transformIndexList  // 0x98
  public            Unity.Collections.NativeArray<System.Single>depthList  // 0xa8
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>basePosList  // 0xb8
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>baseRotList  // 0xc8
  public            Unity.Collections.NativeArray<System.Int32>colliderList  // 0xd8
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>bonePosList  // 0xe8
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>boneRotList  // 0xf8
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>boneSclList  // 0x108
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x118
  public            Unity.Collections.NativeArray<System.Int32>skinningBoneList  // 0x128
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>outNextPosList  // 0x138
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0x148
METHODS:
  RVA=0x09F08C50  token=0x60004D6  System.Void Execute(System.Int32 index)
  RVA=0x09F098F0  token=0x60004D7  Unity.Mathematics.float3 InverseSpherePosition(MagicaCloth.PenetrationConstraint.PenetrationData& data, System.Single teamScale, Unity.Mathematics.float3 scaleDirection, System.Single distance, System.Int32 cindex, System.Single cr)
  RVA=0x09F097D0  token=0x60004D8  Unity.Mathematics.float3 InverseSpherePenetration(Unity.Mathematics.float3 c, System.Single cr, Unity.Mathematics.float3 nextpos)
END_CLASS

CLASS: RestoreDistanceData
TYPE:  sealed struct
TOKEN: 0x2000088
SIZE:  0x18
FIELDS:
  public            System.UInt16                   vertexIndex  // 0x10
  public            System.UInt16                   targetVertexIndex  // 0x12
  public            System.Single                   length  // 0x14
METHODS:
  RVA=0x09F00928  token=0x60004E1  System.Boolean IsValid()
END_CLASS

CLASS: RestoreDistanceGroupData
TYPE:  sealed struct
TOKEN: 0x2000089
SIZE:  0xC0
FIELDS:
  public            System.Int32                    teamId  // 0x10
  public            MagicaCloth.CurveParam          mass  // 0x14
  public            System.Single                   velocityInfluence  // 0x24
  public            MagicaCloth.CurveParam          structStiffness  // 0x28
  public            MagicaCloth.ChunkData           structDataChunk  // 0x38
  public            MagicaCloth.ChunkData           structRefChunk  // 0x48
  public            System.Int32                    useBend  // 0x58
  public            MagicaCloth.CurveParam          bendStiffness  // 0x5c
  public            MagicaCloth.ChunkData           bendDataChunk  // 0x6c
  public            MagicaCloth.ChunkData           bendRefChunk  // 0x7c
  public            System.Int32                    useNear  // 0x8c
  public            MagicaCloth.CurveParam          nearStiffness  // 0x90
  public            MagicaCloth.ChunkData           nearDataChunk  // 0xa0
  public            MagicaCloth.ChunkData           nearRefChunk  // 0xb0
METHODS:
  RVA=0x09F0AAE0  token=0x60004E2  System.Boolean IsValid(System.Int32 type)
  RVA=0x09F0AAB8  token=0x60004E3  MagicaCloth.CurveParam GetStiffness(System.Int32 type)
  RVA=0x09F0AA68  token=0x60004E4  MagicaCloth.ChunkData GetDataChunk(System.Int32 type)
  RVA=0x09F0AA90  token=0x60004E5  MagicaCloth.ChunkData GetRefChunk(System.Int32 type)
END_CLASS

CLASS: DistanceJob
TYPE:  sealed struct
TOKEN: 0x200008A
SIZE:  0xE0
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Single                   updatePower  // 0x10
  public            System.Int32                    runCount  // 0x14
  public            System.Int32                    type  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.RestoreDistanceConstraint.RestoreDistanceData>restoreDistanceDataList  // 0x20
  public            Unity.Collections.NativeArray<MagicaCloth.RestoreDistanceConstraint.RestoreDistanceGroupData>restoreDistanceGroupDataList  // 0x30
  public            Unity.Collections.NativeArray<MagicaCloth.ReferenceDataIndex>refDataList  // 0x40
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x50
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x60
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x70
  public            Unity.Collections.NativeArray<System.Single>depthList  // 0x80
  public            Unity.Collections.NativeArray<System.Single>frictionList  // 0x90
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>basePosList  // 0xa0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0xb0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>outNextPosList  // 0xc0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0xd0
METHODS:
  RVA=0x09F05564  token=0x60004E6  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: RotationData
TYPE:  sealed struct
TOKEN: 0x200008C
SIZE:  0x20
FIELDS:
  public            System.UInt16                   vertexIndex  // 0x10
  public            System.UInt16                   targetVertexIndex  // 0x12
  public            Unity.Mathematics.float3        localPos  // 0x14
METHODS:
  RVA=0x09F00928  token=0x60004EE  System.Boolean IsValid()
END_CLASS

CLASS: GroupData
TYPE:  sealed struct
TOKEN: 0x200008D
SIZE:  0x4C
FIELDS:
  public            System.Int32                    teamId  // 0x10
  public            System.Int32                    active  // 0x14
  public            MagicaCloth.CurveParam          restorePower  // 0x18
  public            System.Single                   velocityInfluence  // 0x28
  public            MagicaCloth.ChunkData           dataChunk  // 0x2c
  public            MagicaCloth.ChunkData           refChunk  // 0x3c
METHODS:
END_CLASS

CLASS: RotationJob
TYPE:  sealed struct
TOKEN: 0x200008E
SIZE:  0xD8
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Single                   updatePower  // 0x10
  public            System.Int32                    runCount  // 0x14
  public            Unity.Collections.NativeArray<MagicaCloth.RestoreRotationConstraint.RotationData>dataList  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.RestoreRotationConstraint.GroupData>groupList  // 0x28
  public            Unity.Collections.NativeArray<MagicaCloth.ReferenceDataIndex>refDataList  // 0x38
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x48
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x58
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x68
  public            Unity.Collections.NativeArray<System.Single>depthList  // 0x78
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>baseRotList  // 0x88
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0x98
  public            Unity.Collections.NativeArray<System.Single>frictionList  // 0xa8
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>outNextPosList  // 0xb8
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0xc8
METHODS:
  RVA=0x09F0B524  token=0x60004EF  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: GroupData
TYPE:  sealed struct
TOKEN: 0x2000090
SIZE:  0x1C
FIELDS:
  public            System.Int32                    teamId  // 0x10
  public            System.Int32                    active  // 0x14
  public            System.Single                   spring  // 0x18
METHODS:
END_CLASS

CLASS: SpringJob
TYPE:  sealed struct
TOKEN: 0x2000091
SIZE:  0x78
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Single                   updatePower  // 0x10
  public            System.Int32                    runCount  // 0x14
  public            Unity.Collections.NativeArray<MagicaCloth.SpringConstraint.GroupData>groupList  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x28
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x38
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x48
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>basePosList  // 0x58
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0x68
METHODS:
  RVA=0x09F0D8C8  token=0x60004F7  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: TriangleBendData
TYPE:  sealed struct
TOKEN: 0x2000093
SIZE:  0x3C
FIELDS:
  public            System.Int32                    vindex0  // 0x10
  public            System.Int32                    vindex1  // 0x14
  public            System.Int32                    vindex2  // 0x18
  public            System.Int32                    vindex3  // 0x1c
  public            System.Single                   restAngle  // 0x20
  public            System.Single                   direction  // 0x24
  public            System.Single                   depth  // 0x28
  public            System.Int32                    writeIndex0  // 0x2c
  public            System.Int32                    writeIndex1  // 0x30
  public            System.Int32                    writeIndex2  // 0x34
  public            System.Int32                    writeIndex3  // 0x38
METHODS:
  RVA=0x09F0F454  token=0x60004FF  System.Boolean IsValid()
  RVA=0x09F0F44C  token=0x6000500  System.Boolean IsPositionBend()
END_CLASS

CLASS: TriangleBendGroupData
TYPE:  sealed struct
TOKEN: 0x2000094
SIZE:  0x6C
FIELDS:
  public            System.Int32                    teamId  // 0x10
  public            System.Int32                    active  // 0x14
  public            System.Int32                    algorithm  // 0x18
  public            MagicaCloth.CurveParam          stiffness  // 0x1c
  public            MagicaCloth.ChunkData           dataChunk  // 0x2c
  public            MagicaCloth.ChunkData           groupIndexChunk  // 0x3c
  public            MagicaCloth.ChunkData           refDataChunk  // 0x4c
  public            MagicaCloth.ChunkData           writeDataChunk  // 0x5c
METHODS:
END_CLASS

CLASS: TriangleBendCalcJob
TYPE:  sealed struct
TOKEN: 0x2000095
SIZE:  0x88
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Single                   updatePower  // 0x10
  public            System.Int32                    runCount  // 0x14
  public            Unity.Collections.NativeArray<MagicaCloth.TriangleBendConstraint.TriangleBendGroupData>triangleBendGroupDataList  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.TriangleBendConstraint.TriangleBendData>triangleBendList  // 0x28
  public            Unity.Collections.NativeArray<System.Int16>groupIndexList  // 0x38
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x48
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0x58
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>basePosList  // 0x68
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>writeBuffer  // 0x78
METHODS:
  RVA=0x09F0DB30  token=0x6000501  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: TriangleBendSumJob
TYPE:  sealed struct
TOKEN: 0x2000096
SIZE:  0x98
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Int32                    runCount  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.TriangleBendConstraint.TriangleBendGroupData>triangleBendGroupDataList  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.ReferenceDataIndex>refDataList  // 0x28
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>writeBuffer  // 0x38
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x48
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x58
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x68
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>inoutNextPosList  // 0x78
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0x88
METHODS:
  RVA=0x09F0F464  token=0x6000502  System.Void Execute(System.Int32 pindex)
END_CLASS

CLASS: TwistData
TYPE:  sealed struct
TOKEN: 0x2000098
SIZE:  0x14
FIELDS:
  public            System.UInt16                   vertexIndex0  // 0x10
  public            System.UInt16                   vertexIndex1  // 0x12
METHODS:
  RVA=0x09F00928  token=0x600050A  System.Boolean IsValid()
END_CLASS

CLASS: GroupData
TYPE:  sealed struct
TOKEN: 0x2000099
SIZE:  0x3C
FIELDS:
  public            System.Int32                    teamId  // 0x10
  public            System.Int32                    active  // 0x14
  public            System.Single                   recoveryPower  // 0x18
  public            MagicaCloth.ChunkData           dataChunk  // 0x1c
  public            MagicaCloth.ChunkData           refChunk  // 0x2c
METHODS:
END_CLASS

CLASS: TwistJob2
TYPE:  sealed struct
TOKEN: 0x200009A
SIZE:  0xC8
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Int32                    runCount  // 0x10
  public            System.Single                   updatePower  // 0x14
  public            Unity.Collections.NativeArray<MagicaCloth.TwistConstraint.TwistData>dataList  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.ReferenceDataIndex>refDataList  // 0x28
  public            Unity.Collections.NativeArray<MagicaCloth.TwistConstraint.GroupData>groupList  // 0x38
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x48
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x58
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x68
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>basePosList  // 0x78
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0x88
  public            Unity.Collections.NativeArray<System.Single>frictionList  // 0x98
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>outNextPosList  // 0xa8
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0xb8
METHODS:
  RVA=0x09F10120  token=0x600050B  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: VolumeData
TYPE:  sealed struct
TOKEN: 0x200009C
SIZE:  0x5C
FIELDS:
  public            System.Int32                    vindex0  // 0x10
  public            System.Int32                    vindex1  // 0x14
  public            System.Int32                    vindex2  // 0x18
  public            System.Int32                    vindex3  // 0x1c
  public            Unity.Mathematics.float3x3      ivMat  // 0x20
  public            System.Single                   depth  // 0x44
  public            System.Int32                    direction  // 0x48
  public            System.Int32                    writeIndex0  // 0x4c
  public            System.Int32                    writeIndex1  // 0x50
  public            System.Int32                    writeIndex2  // 0x54
  public            System.Int32                    writeIndex3  // 0x58
METHODS:
  RVA=0x09F0F454  token=0x6000514  System.Boolean IsValid()
END_CLASS

CLASS: GroupData
TYPE:  sealed struct
TOKEN: 0x200009D
SIZE:  0x78
FIELDS:
  public            System.Int32                    teamId  // 0x10
  public            System.Int32                    active  // 0x14
  public            MagicaCloth.CurveParam          stretchStiffness  // 0x18
  public            MagicaCloth.CurveParam          shearStiffness  // 0x28
  public            MagicaCloth.ChunkData           dataChunk  // 0x38
  public            MagicaCloth.ChunkData           groupIndexChunk  // 0x48
  public            MagicaCloth.ChunkData           refDataChunk  // 0x58
  public            MagicaCloth.ChunkData           writeDataChunk  // 0x68
METHODS:
END_CLASS

CLASS: VolumeCalcJob
TYPE:  sealed struct
TOKEN: 0x200009E
SIZE:  0x78
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Single                   updatePower  // 0x10
  public            System.Int32                    runCount  // 0x14
  public            Unity.Collections.NativeArray<MagicaCloth.VolumeConstraint.GroupData>groupDataList  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.VolumeConstraint.VolumeData>dataList  // 0x28
  public            Unity.Collections.NativeArray<System.Int16>groupIndexList  // 0x38
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x48
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0x58
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>writeBuffer  // 0x68
METHODS:
  RVA=0x09F10844  token=0x6000515  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: VolumeSumJob
TYPE:  sealed struct
TOKEN: 0x200009F
SIZE:  0x88
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Int32                    runCount  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.VolumeConstraint.GroupData>groupDataList  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.ReferenceDataIndex>refDataList  // 0x28
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>writeBuffer  // 0x38
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x48
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x58
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x68
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>inoutNextPosList  // 0x78
METHODS:
  RVA=0x09F20DCC  token=0x6000516  System.Void Execute(System.Int32 pindex)
END_CLASS

CLASS: RestoreBoneJob
TYPE:  sealed struct
TOKEN: 0x20000A5
SIZE:  0x68
IMPLEMENTS: UnityEngine.Jobs.IJobParallelForTransform
FIELDS:
  public            System.Boolean                  fixedUpdate  // 0x10
  public            Unity.Collections.NativeArray<System.Int16>boneUnityPhysicsList  // 0x18
  public            Unity.Collections.NativeArray<System.Byte>boneFlagList  // 0x28
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>restoreBoneLocalPosList  // 0x38
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>restoreBoneLocalRotList  // 0x48
  public            Unity.Collections.NativeArray<System.Int32>restoreBoneIndexList  // 0x58
METHODS:
  RVA=0x09F20C10  token=0x600053F  System.Void Execute(System.Int32 index, UnityEngine.Jobs.TransformAccess transform)
END_CLASS

CLASS: ReadBoneJob0
TYPE:  sealed struct
TOKEN: 0x20000A6
SIZE:  0xA8
IMPLEMENTS: UnityEngine.Jobs.IJobParallelForTransform
FIELDS:
  public            System.Int32                    fixedUpdateCount  // 0x10
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>bonePosList  // 0x18
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>boneRotList  // 0x28
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>boneSclList  // 0x38
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>basePosList  // 0x48
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>baseRotList  // 0x58
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>futurePosList  // 0x68
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>futureRotList  // 0x78
  public            Unity.Collections.NativeArray<System.Int16>boneUnityPhysicsList  // 0x88
  public            Unity.Collections.NativeArray<System.Byte>boneFlagList  // 0x98
METHODS:
  RVA=0x09F201C0  token=0x6000540  System.Void Execute(System.Int32 index, UnityEngine.Jobs.TransformAccess transform)
END_CLASS

CLASS: ReadBoneJob1
TYPE:  sealed struct
TOKEN: 0x20000A7
SIZE:  0xB8
IMPLEMENTS: UnityEngine.Jobs.IJobParallelForTransform
FIELDS:
  public            System.Int32                    fixedUpdateCount  // 0x10
  public            System.Single                   normalFutureRatio  // 0x14
  public            System.Single                   fixedFutureRatio  // 0x18
  public            System.Single                   normalDeltaTime  // 0x1c
  public            System.Single                   fixedDeltaTime  // 0x20
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>bonePosList  // 0x28
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>boneRotList  // 0x38
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>boneSclList  // 0x48
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>basePosList  // 0x58
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>baseRotList  // 0x68
  public            Unity.Collections.NativeArray<System.Int16>boneUnityPhysicsList  // 0x78
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>futurePosList  // 0x88
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>futureRotList  // 0x98
  public            Unity.Collections.NativeArray<System.Byte>boneFlagList  // 0xa8
METHODS:
  RVA=0x09F2051C  token=0x6000541  System.Void Execute(System.Int32 index, UnityEngine.Jobs.TransformAccess transform)
END_CLASS

CLASS: ConvertWorldToLocalJob
TYPE:  sealed struct
TOKEN: 0x20000A8
SIZE:  0x90
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<System.Int32>writeBoneIndexList  // 0x10
  public            Unity.Collections.NativeArray<System.Byte>boneFlagList  // 0x20
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>bonePosList  // 0x30
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>boneRotList  // 0x40
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>boneSclList  // 0x50
  public            Unity.Collections.NativeArray<System.Int32>boneParentIndexList  // 0x60
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>writeBonePosList  // 0x70
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>writeBoneRotList  // 0x80
METHODS:
  RVA=0x09F126D0  token=0x6000542  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: WriteBontToTransformJob2
TYPE:  sealed struct
TOKEN: 0x20000A9
SIZE:  0x78
IMPLEMENTS: UnityEngine.Jobs.IJobParallelForTransform
FIELDS:
  public            System.Int32                    fixedUpdateCount  // 0x10
  public            Unity.Collections.NativeArray<System.Byte>boneFlagList  // 0x18
  public            Unity.Collections.NativeArray<System.Int32>writeBoneIndexList  // 0x28
  public            Unity.Collections.NativeArray<System.Int32>boneParentIndexList  // 0x38
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>writeBonePosList  // 0x48
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>writeBoneRotList  // 0x58
  public            Unity.Collections.NativeArray<System.Int16>boneUnityPhysicsList  // 0x68
METHODS:
  RVA=0x09F21104  token=0x6000543  System.Void Execute(System.Int32 index, UnityEngine.Jobs.TransformAccess transform)
END_CLASS

CLASS: CopyBoneJob0
TYPE:  sealed struct
TOKEN: 0x20000AA
SIZE:  0x50
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>bonePosList  // 0x10
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>boneRotList  // 0x20
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>backBonePosList  // 0x30
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>backBoneRotList  // 0x40
METHODS:
  RVA=0x04DA3550  token=0x6000544  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: CopyBoneJob1
TYPE:  sealed struct
TOKEN: 0x20000AB
SIZE:  0x30
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<System.Int32>writeBoneIndexList  // 0x10
  public            Unity.Collections.NativeArray<System.Int32>backWriteBoneIndexList  // 0x20
METHODS:
  RVA=0x04DA35A0  token=0x6000545  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: CopyBoneJob2
TYPE:  sealed struct
TOKEN: 0x20000AC
SIZE:  0x30
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<System.Byte>boneFlagList  // 0x10
  public            Unity.Collections.NativeArray<System.Byte>backBoneFlagList  // 0x20
METHODS:
  RVA=0x04DA35C0  token=0x6000546  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: ForceAndVelocityJob
TYPE:  sealed struct
TOKEN: 0x20000AF
SIZE:  0x1C0
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Single                   updateDeltaTime  // 0x10
  public            System.Single                   updatePower  // 0x14
  public            System.Int32                    runCount  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x20
  public            Unity.Collections.NativeArray<MagicaCloth.CurveParam>teamMassList  // 0x30
  public            Unity.Collections.NativeArray<MagicaCloth.CurveParam>teamGravityList  // 0x40
  public            Unity.Collections.NativeArray<MagicaCloth.CurveParam>teamDragList  // 0x50
  public            Unity.Collections.NativeArray<MagicaCloth.CurveParam>teamDepthInfluenceList  // 0x60
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.WindInfo>teamWindInfoList  // 0x70
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x80
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x90
  public            Unity.Collections.NativeArray<System.Single>depthList  // 0xa0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>snapBasePosList  // 0xb0
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>snapBaseRotList  // 0xc0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>basePosList  // 0xd0
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>baseRotList  // 0xe0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>oldBasePosList  // 0xf0
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>oldBaseRotList  // 0x100
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0x110
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>nextRotList  // 0x120
  public            Unity.Collections.NativeArray<System.Single>frictionList  // 0x130
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0x140
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>rotList  // 0x150
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>oldPosList  // 0x160
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>oldRotList  // 0x170
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>velocityList  // 0x180
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerWindData.WindData>windDataList  // 0x190
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>bonePosList  // 0x1a0
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>boneRotList  // 0x1b0
METHODS:
  RVA=0x09F132A0  token=0x6000596  System.Void Execute(System.Int32 index)
  RVA=0x09F13CA8  token=0x6000597  Unity.Mathematics.float3 Wind(System.Int32 teamId, MagicaCloth.PhysicsManagerTeamData.TeamData& teamData, Unity.Mathematics.float3& pos)
END_CLASS

CLASS: FixPositionJob
TYPE:  sealed struct
TOKEN: 0x20000B0
SIZE:  0x120
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Single                   updatePower  // 0x10
  public            System.Single                   updateDeltaTime  // 0x14
  public            System.Int32                    runCount  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x20
  public            Unity.Collections.NativeArray<MagicaCloth.CurveParam>teamMaxVelocityList  // 0x30
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x40
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x50
  public            Unity.Collections.NativeArray<System.Single>depthList  // 0x60
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0x70
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>nextRotList  // 0x80
  public            Unity.Collections.NativeArray<System.Single>frictionList  // 0x90
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>velocityList  // 0xa0
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>rotList  // 0xb0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>oldPosList  // 0xc0
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>oldRotList  // 0xd0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0xe0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>localPosList  // 0xf0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>collisionNormalList  // 0x100
  public            Unity.Collections.NativeArray<System.Single>staticFrictionList  // 0x110
METHODS:
  RVA=0x09F12AA4  token=0x6000598  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: PostUpdatePhysicsJob
TYPE:  sealed struct
TOKEN: 0x20000B1
SIZE:  0x138
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Single                   updateDeltaTime  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x28
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x38
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>snapBasePosList  // 0x48
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>snapBaseRotList  // 0x58
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>basePosList  // 0x68
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>baseRotList  // 0x78
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>oldBasePosList  // 0x88
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>oldBaseRotList  // 0x98
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>velocityList  // 0xa8
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>localPosList  // 0xb8
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>oldPosList  // 0xc8
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>oldRotList  // 0xd8
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0xe8
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>rotList  // 0xf8
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0x108
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>nextRotList  // 0x118
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>oldSlowPosList  // 0x128
METHODS:
  RVA=0x09F1FC24  token=0x6000599  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: SharedVirtualMeshInfo
TYPE:  sealed struct
TOKEN: 0x20000B4
SIZE:  0x60
FIELDS:
  public            System.Int32                    uid  // 0x10
  public            System.Int32                    useCount  // 0x14
  public            System.Int32                    sharedChildMeshStartIndex  // 0x18
  public            System.Int32                    sharedChildMeshCount  // 0x1c
  public            MagicaCloth.ChunkData           vertexChunk  // 0x20
  public            MagicaCloth.ChunkData           weightChunk  // 0x30
  public            MagicaCloth.ChunkData           triangleChunk  // 0x40
  public            MagicaCloth.ChunkData           vertexToTriangleChunk  // 0x50
METHODS:
END_CLASS

CLASS: VirtualMeshInfo
TYPE:  sealed struct
TOKEN: 0x20000B5
SIZE:  0x54
FIELDS:
  public            System.UInt32                   flag  // 0x10
  public            System.Int32                    sharedVirtualMeshIndex  // 0x14
  public            System.Int32                    meshUseCount  // 0x18
  public            System.Int32                    vertexUseCount  // 0x1c
  public            MagicaCloth.ChunkData           vertexChunk  // 0x20
  public            MagicaCloth.ChunkData           boneChunk  // 0x30
  public            MagicaCloth.ChunkData           triangleChunk  // 0x40
  public            System.Int32                    transformIndex  // 0x50
METHODS:
  RVA=0x09F20B04  token=0x60005EC  System.Boolean IsFlag(System.UInt32 flag)
  RVA=0x09F20BE4  token=0x60005ED  System.Void SetFlag(System.UInt32 flag, System.Boolean sw)
  RVA=0x09F20AF0  token=0x60005EE  System.Boolean IsActive()
  RVA=0x09F20DB4  token=0x60005EF  System.Boolean IsUse()
  RVA=0x09F20B24  token=0x60005F0  System.Boolean IsPause()
END_CLASS

CLASS: SharedChildMeshInfo
TYPE:  sealed struct
TOKEN: 0x20000B6
SIZE:  0x48
FIELDS:
  public            System.Int64                    cuid  // 0x10
  public            System.Int32                    sharedVirtualMeshIndex  // 0x18
  public            System.Int32                    virtualMeshIndex  // 0x1c
  public            System.Int32                    meshUseCount  // 0x20
  public            MagicaCloth.ChunkData           vertexChunk  // 0x24
  public            MagicaCloth.ChunkData           weightChunk  // 0x34
METHODS:
END_CLASS

CLASS: SharedRenderMeshInfo
TYPE:  sealed struct
TOKEN: 0x20000B7
SIZE:  0x50
FIELDS:
  public            System.Int32                    uid  // 0x10
  public            System.Int32                    useCount  // 0x14
  public            System.UInt32                   flag  // 0x18
  public            MagicaCloth.ChunkData           vertexChunk  // 0x1c
  public            MagicaCloth.ChunkData           bonePerVertexChunk  // 0x2c
  public            MagicaCloth.ChunkData           boneWeightsChunk  // 0x3c
  public            System.Int32                    rendererBoneIndex  // 0x4c
METHODS:
  RVA=0x09F20CF8  token=0x60005F1  System.Boolean IsFlag(System.UInt32 flag)
  RVA=0x09F20D08  token=0x60005F2  System.Void SetFlag(System.UInt32 flag, System.Boolean sw)
  RVA=0x09F20D00  token=0x60005F3  System.Boolean IsSkinning()
END_CLASS

CLASS: RenderMeshInfo
TYPE:  sealed struct
TOKEN: 0x20000B8
SIZE:  0x98
FIELDS:
  public            System.UInt32                   flag  // 0x10
  public            System.Int32                    renderSharedMeshIndex  // 0x14
  public            System.Int32                    sharedRenderMeshVertexStartIndex  // 0x18
  public            System.Int32                    meshUseCount  // 0x1c
  public            Unity.Mathematics.int4          childMeshVertexStartIndex  // 0x20
  public            Unity.Mathematics.int4          childMeshWeightStartIndex  // 0x30
  public            Unity.Mathematics.int4          virtualMeshVertexStartIndex  // 0x40
  public            Unity.Mathematics.int4          sharedVirtualMeshVertexStartIndex  // 0x50
  public            Unity.Mathematics.int4          linkMeshCount  // 0x60
  public            MagicaCloth.ChunkData           vertexChunk  // 0x70
  public            MagicaCloth.ChunkData           boneWeightsChunk  // 0x80
  public            System.Int32                    transformIndex  // 0x90
  public            System.Single                   baseScale  // 0x94
METHODS:
  RVA=0x09F20B04  token=0x60005F4  System.Boolean IsFlag(System.UInt32 flag)
  RVA=0x09F20BE4  token=0x60005F5  System.Void SetFlag(System.UInt32 flag, System.Boolean sw)
  RVA=0x09F20AF0  token=0x60005F6  System.Boolean IsActive()
  RVA=0x09F20B34  token=0x60005F7  System.Boolean IsUse()
  RVA=0x09F20B0C  token=0x60005F8  System.Boolean IsLinkMesh(System.Int32 index)
  RVA=0x09F20B24  token=0x60005F9  System.Boolean IsPause()
  RVA=0x09F20AF8  token=0x60005FA  System.Boolean IsFasterWrite()
  RVA=0x09F20B2C  token=0x60005FB  System.Boolean IsSkinning()
  RVA=0x09F20A30  token=0x60005FC  System.Boolean AddLinkMesh(System.Int32 renderMeshIndex, System.Int32 childMeshVertexStart, System.Int32 childMeshWeightStart, System.Int32 virtualMeshVertexStart, System.Int32 sharedVirtualMeshVertexStart)
  RVA=0x09F20B44  token=0x60005FD  System.Boolean RemoveLinkMesh(System.Int32 renderMeshIndex, System.Int32 childMeshVertexStart, System.Int32 childMeshWeightStart, System.Int32 virtualMeshVertexStart, System.Int32 sharedVirtualMeshVertexStart)
END_CLASS

CLASS: RenderMeshState
TYPE:  class
TOKEN: 0x20000B9
SIZE:  0x30
FIELDS:
  public            System.UInt32                   flag  // 0x10
  public            System.Int32                    RenderSharedMeshIndex  // 0x14
  public            System.Int32                    RenderSharedMeshId  // 0x18
  public            System.Int32                    VertexChunkStart  // 0x1c
  public            System.Int32                    VertexChunkLength  // 0x20
  public            System.Int32                    BoneWeightChunkStart  // 0x24
  public            System.Int32                    BoneWeightChunkLength  // 0x28
METHODS:
  RVA=0x0709EC80  token=0x60005FE  System.Boolean IsFlag(System.UInt32 flag)
  RVA=0x09F20BF8  token=0x60005FF  System.Void SetFlag(System.UInt32 flag, System.Boolean sw)
  RVA=0x041E1670  token=0x6000600  System.Void .ctor()
END_CLASS

CLASS: CopyRenderBuffer
TYPE:  sealed struct
TOKEN: 0x20000BA
SIZE:  0x50
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>renderPosList  // 0x10
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>renderNormalList  // 0x20
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>renderPosArray  // 0x30
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>renderNormalArray  // 0x40
METHODS:
  RVA=0x04DA35E0  token=0x6000601  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000BB
SIZE:  0x10
FIELDS:
  public    static readonly MagicaCloth.PhysicsManagerMeshData.<>c<>9  // static @ 0x0
  public    static  System.Comparison<MagicaCloth.RenderMeshDeformer><>9__158_0  // static @ 0x8
METHODS:
  RVA=0x09F20D50  token=0x6000602  System.Void .cctor()
  RVA=0x041E1670  token=0x6000603  System.Void .ctor()
  RVA=0x09F20D20  token=0x6000604  System.Int32 <DispatchWriting>b__158_0(MagicaCloth.RenderMeshDeformer a, MagicaCloth.RenderMeshDeformer b)
END_CLASS

CLASS: ParticleFlag
TYPE:  sealed struct
TOKEN: 0x20000BD
SIZE:  0x14
FIELDS:
  public            System.UInt32                   flag  // 0x10
METHODS:
  RVA=0x04430530  token=0x600061E  System.Void .ctor(System.UInt32[] flags)
  RVA=0x09F20B04  token=0x600061F  System.Boolean IsFlag(System.UInt32 flag)
  RVA=0x09F20BE4  token=0x6000620  System.Void SetFlag(System.UInt32 flag, System.Boolean sw)
  RVA=0x09F2A3F0  token=0x6000621  System.Boolean IsValid()
  RVA=0x04430680  token=0x6000622  System.Void SetEnable(System.Boolean sw)
  RVA=0x09F2A3B4  token=0x6000623  System.Boolean IsFixed()
  RVA=0x09F2A3C4  token=0x6000624  System.Boolean IsMove()
  RVA=0x09F2A3BC  token=0x6000625  System.Boolean IsKinematic()
  RVA=0x09F20B2C  token=0x6000626  System.Boolean IsHold()
  RVA=0x04430520  token=0x6000627  System.Boolean IsCollider()
  RVA=0x04430660  token=0x6000628  System.Boolean IsReadTransform()
  RVA=0x09F2A3F8  token=0x6000629  System.Boolean IsWriteTransform()
  RVA=0x04430670  token=0x600062A  System.Boolean IsRestoreTransform()
  RVA=0x09F2A3E4  token=0x600062B  System.Boolean IsUnityPhysics()
  RVA=0x09F2A3D8  token=0x600062C  System.Boolean IsReadSclTransform()
  RVA=0x09F2A3CC  token=0x600062D  System.Boolean IsParentTransform()
END_CLASS

CLASS: CopyBoneToParticleJob
TYPE:  sealed struct
TOKEN: 0x20000BE
SIZE:  0x160
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamData  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.WorldInfluence>teamWorldInfluenceList  // 0x20
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x30
  public            Unity.Collections.NativeArray<System.Single>depthList  // 0x40
  public            Unity.Collections.NativeArray<System.Int32>transformIndexList  // 0x50
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>localPosList  // 0x60
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x70
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>velocityList  // 0x80
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>bonePosList  // 0x90
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>boneRotList  // 0xa0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>boneSclList  // 0xb0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0xc0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>oldPosList  // 0xd0
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>oldRotList  // 0xe0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>oldSlowPosList  // 0xf0
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>rotList  // 0x100
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>snapBasePosList  // 0x110
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>snapBaseRotList  // 0x120
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>oldBasePosList  // 0x130
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>oldBaseRotList  // 0x140
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0x150
METHODS:
  RVA=0x09F25B74  token=0x600062E  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: ResetParticleJob
TYPE:  sealed struct
TOKEN: 0x20000BF
SIZE:  0x130
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamData  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x20
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x30
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>snapBasePosList  // 0x40
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>snapBaseRotList  // 0x50
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>basePosList  // 0x60
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>baseRotList  // 0x70
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>oldBasePosList  // 0x80
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>oldBaseRotList  // 0x90
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0xa0
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>rotList  // 0xb0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>oldPosList  // 0xc0
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>oldRotList  // 0xd0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>oldSlowPosList  // 0xe0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>velocityList  // 0xf0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>nextPosList  // 0x100
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>nextRotList  // 0x110
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>localPosList  // 0x120
METHODS:
  RVA=0x09F337CC  token=0x600062F  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: CopyParticleToBoneJob
TYPE:  sealed struct
TOKEN: 0x20000C0
SIZE:  0xC0
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x10
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0x20
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>rotList  // 0x30
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>boneBasePosList  // 0x40
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>boneBaseRotList  // 0x50
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x60
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x70
  public            Unity.Collections.NativeParallelMultiHashMap<System.Int32,System.Int32>transformParticleIndexMap  // 0x80
  public            Unity.Collections.NativeArray<System.Int32>writeBoneIndexList  // 0x90
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>bonePosList  // 0xa0
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>boneRotList  // 0xb0
METHODS:
  RVA=0x09F26484  token=0x6000630  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: ForceMode
TYPE:  sealed struct
TOKEN: 0x20000C2
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.PhysicsManagerTeamData.ForceModeNone  // const
  public    static  MagicaCloth.PhysicsManagerTeamData.ForceModeVelocityAdd  // const
  public    static  MagicaCloth.PhysicsManagerTeamData.ForceModeVelocityChange  // const
  public    static  MagicaCloth.PhysicsManagerTeamData.ForceModeVelocityAddWithoutMass  // const
  public    static  MagicaCloth.PhysicsManagerTeamData.ForceModeVelocityChangeWithoutMass  // const
METHODS:
END_CLASS

CLASS: TeamData
TYPE:  sealed struct
TOKEN: 0x20000C3
SIZE:  0x110
FIELDS:
  public            MagicaCloth.ChunkData           particleChunk  // 0x10
  public            MagicaCloth.ChunkData           colliderChunk  // 0x20
  public            MagicaCloth.ChunkData           skinningBoneChunk  // 0x30
  public            System.UInt32                   flag  // 0x40
  public            System.Single                   dynamicFriction  // 0x44
  public            System.Single                   staticFriction  // 0x48
  public            System.Single                   selfCollisionRange  // 0x4c
  public            System.Int32                    boneIndex  // 0x50
  public            Unity.Mathematics.float3        initScale  // 0x54
  public            System.Single                   scaleRatio  // 0x60
  public            Unity.Mathematics.float3        scaleDirection  // 0x64
  public            Unity.Mathematics.float4        quaternionScale  // 0x70
  public            System.Single                   time  // 0x80
  public            System.Single                   oldTime  // 0x84
  public            System.Single                   addTime  // 0x88
  public            System.Single                   timeScale  // 0x8c
  public            System.Single                   nowTime  // 0x90
  public            System.Single                   startTime  // 0x94
  public            System.Int32                    updateCount  // 0x98
  public            System.Single                   blendRatio  // 0x9c
  public            System.Single                   outPhysicsPoseBlendRatio  // 0xa0
  public            Unity.Mathematics.float3        externalForce  // 0xa4
  public            System.Single                   forceMassInfluence  // 0xb0
  public            System.Single                   forceWindInfluence  // 0xb4
  public            System.Single                   forceWindRandomScale  // 0xb8
  public            System.Single                   forceWindSynchronization  // 0xbc
  public            System.Single                   velocityWeight  // 0xc0
  public            System.Single                   velocityRecoverySpeed  // 0xc4
  public            Unity.Mathematics.float3        gravityDirection  // 0xc8
  public            MagicaCloth.PhysicsManagerTeamData.ForceModeforceMode  // 0xd4
  public            Unity.Mathematics.float3        impactForce  // 0xd8
  public            System.Int32                    calcCount  // 0xe4
  public            System.Int16                    restoreDistanceGroupIndex  // 0xe8
  public            System.Int16                    triangleBendGroupIndex  // 0xea
  public            System.Int16                    clampDistanceGroupIndex  // 0xec
  public            System.Int16                    clampDistance2GroupIndex  // 0xee
  public            System.Int16                    clampPositionGroupIndex  // 0xf0
  public            System.Int16                    clampRotationGroupIndex  // 0xf2
  public            System.Int16                    restoreRotationGroupIndex  // 0xf4
  public            System.Int16                    adjustRotationGroupIndex  // 0xf6
  public            System.Int16                    springGroupIndex  // 0xf8
  public            System.Int16                    volumeGroupIndex  // 0xfa
  public            System.Int16                    airLineGroupIndex  // 0xfc
  public            System.Int16                    lineWorkerGroupIndex  // 0xfe
  public            System.Int16                    triangleWorkerGroupIndex  // 0x100
  public            System.Int16                    selfCollisionGroupIndex  // 0x102
  public            System.Int16                    edgeCollisionGroupIndex  // 0x104
  public            System.Int16                    penetrationGroupIndex  // 0x106
  public            System.Int16                    baseSkinningGroupIndex  // 0x108
  public            System.Int16                    twistGroupIndex  // 0x10a
  public            System.Int16                    compositeRotationGroupIndex  // 0x10c
METHODS:
  RVA=0x09F34424  token=0x600066C  System.Boolean IsActive()
  RVA=0x09F34460  token=0x600066D  System.Boolean IsRunning()
  RVA=0x09F3446C  token=0x600066E  System.Boolean IsUpdate(System.Int32 runCount)
  RVA=0x09F34434  token=0x600066F  System.Boolean IsInterpolate()
  RVA=0x09F34448  token=0x6000670  System.Boolean IsPhysicsUpdate()
  RVA=0x09F3442C  token=0x6000671  System.Boolean IsFlag(System.UInt32 flag)
  RVA=0x09F34478  token=0x6000672  System.Void SetFlag(System.UInt32 flag, System.Boolean sw)
  RVA=0x09F34454  token=0x6000673  System.Boolean IsReset()
  RVA=0x09F3443C  token=0x6000674  System.Boolean IsPause()
END_CLASS

CLASS: WorldInfluence
TYPE:  sealed struct
TOKEN: 0x20000C4
SIZE:  0xAC
FIELDS:
  public            MagicaCloth.CurveParam          moveInfluence  // 0x10
  public            MagicaCloth.CurveParam          rotInfluence  // 0x20
  public            System.Single                   maxMoveSpeed  // 0x30
  public            System.Single                   maxRotationSpeed  // 0x34
  public            Unity.Mathematics.float3        nowPosition  // 0x38
  public            Unity.Mathematics.float3        oldPosition  // 0x44
  public            Unity.Mathematics.float3        moveOffset  // 0x50
  public            System.Single                   moveIgnoreRatio  // 0x5c
  public            Unity.Mathematics.quaternion    nowRotation  // 0x60
  public            Unity.Mathematics.quaternion    oldRotation  // 0x70
  public            Unity.Mathematics.quaternion    rotationOffset  // 0x80
  public            System.Single                   rotationIgnoreRatio  // 0x90
  public            System.Int32                    resetTeleport  // 0x94
  public            System.Single                   teleportDistance  // 0x98
  public            System.Single                   teleportRotation  // 0x9c
  public            MagicaCloth.ClothParams.TeleportModeteleportMode  // 0xa0
  public            System.Single                   stabilizationTime  // 0xa4
  public            System.Single                   clampRotationLimit  // 0xa8
METHODS:
END_CLASS

CLASS: WindInfo
TYPE:  sealed struct
TOKEN: 0x20000C5
SIZE:  0x64
FIELDS:
  public            System.Int32                    windCount  // 0x10
  public            Unity.Mathematics.int4          windDataIndexList  // 0x14
  public            Unity.Mathematics.float3x4      windDirectionList  // 0x24
  public            Unity.Mathematics.float4        windMainList  // 0x54
METHODS:
END_CLASS

CLASS: PreProcessTeamDataJob
TYPE:  sealed struct
TOKEN: 0x20000C6
SIZE:  0xA8
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Single                   deltaTime  // 0x10
  public            System.Single                   physicsDeltaTime  // 0x14
  public            System.Single                   updateDeltaTime  // 0x18
  public            System.Single                   globalTimeScale  // 0x1c
  public            System.Int32                    maxUpdateCount  // 0x20
  public            System.Single                   unityTimeScale  // 0x24
  public            System.Single                   elapsedTime  // 0x28
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamData  // 0x30
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.WorldInfluence>teamWorldInfluenceList  // 0x40
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.WindInfo>teamWindInfoList  // 0x50
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>bonePosList  // 0x60
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>boneRotList  // 0x70
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>boneSclList  // 0x80
  public            System.Int32                    windCount  // 0x90
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerWindData.WindData>windData  // 0x98
METHODS:
  RVA=0x09F2FEFC  token=0x6000675  System.Void Execute(System.Int32 teamId)
  RVA=0x09F30DA0  token=0x6000676  System.Void UpdateTime(MagicaCloth.PhysicsManagerTeamData.TeamData& tdata, System.Boolean reset, System.Single dtime)
  RVA=0x09F30E94  token=0x6000677  System.Void Wind(MagicaCloth.PhysicsManagerTeamData.TeamData& tdata, Unity.Mathematics.float3 pos, System.Int32 teamId)
END_CLASS

CLASS: PostProcessTeamDataJob
TYPE:  sealed struct
TOKEN: 0x20000C7
SIZE:  0x38
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Int32                    fixedUpdateCount  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamData  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.WorldInfluence>teamWorldInfluenceList  // 0x28
METHODS:
  RVA=0x09F2FBF8  token=0x6000678  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000C8
SIZE:  0x10
FIELDS:
  public    static readonly MagicaCloth.PhysicsManagerTeamData.<>c<>9  // static @ 0x0
  public    static  System.Action<System.Int32>     <>9__76_0  // static @ 0x8
METHODS:
  RVA=0x09F3526C  token=0x6000679  System.Void .cctor()
  RVA=0x041E1670  token=0x600067A  System.Void .ctor()
  RVA=0x09F35210  token=0x600067B  System.Void <ResetFuturePredictionCollidere>b__76_0(System.Int32 pindex)
END_CLASS

CLASS: WindType
TYPE:  sealed struct
TOKEN: 0x20000CA
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.PhysicsManagerWindData.WindTypeNone  // const
  public    static  MagicaCloth.PhysicsManagerWindData.WindTypeDirection  // const
  public    static  MagicaCloth.PhysicsManagerWindData.WindTypeArea  // const
METHODS:
END_CLASS

CLASS: ShapeType
TYPE:  sealed struct
TOKEN: 0x20000CB
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.PhysicsManagerWindData.ShapeTypeBox  // const
  public    static  MagicaCloth.PhysicsManagerWindData.ShapeTypeSphere  // const
METHODS:
END_CLASS

CLASS: DirectionType
TYPE:  sealed struct
TOKEN: 0x20000CC
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.PhysicsManagerWindData.DirectionTypeOneDirection  // const
  public    static  MagicaCloth.PhysicsManagerWindData.DirectionTypeRadial  // const
METHODS:
END_CLASS

CLASS: WindData
TYPE:  sealed struct
TOKEN: 0x20000CD
SIZE:  0x60
FIELDS:
  public            System.UInt32                   flag  // 0x10
  public            MagicaCloth.PhysicsManagerWindData.WindTypewindType  // 0x14
  public            MagicaCloth.PhysicsManagerWindData.ShapeTypeshapeType  // 0x18
  public            System.Int32                    transformIndex  // 0x1c
  public            Unity.Mathematics.float3        areaSize  // 0x20
  public            System.Single                   main  // 0x2c
  public            System.Single                   turbulence  // 0x30
  public            System.Single                   frequency  // 0x34
  public            Unity.Mathematics.float3        direction  // 0x38
  public            MagicaCloth.PhysicsManagerWindData.DirectionTypedirectionType  // 0x44
  public            System.Single                   areaVolume  // 0x48
  public            System.Single                   areaLength  // 0x4c
  public            MagicaCloth.CurveParam          attenuation  // 0x50
METHODS:
  RVA=0x09F20B04  token=0x6000687  System.Boolean IsFlag(System.UInt32 flag)
  RVA=0x09F20BE4  token=0x6000688  System.Void SetFlag(System.UInt32 flag, System.Boolean sw)
  RVA=0x09F35448  token=0x6000689  System.Void SetEnable(System.Boolean sw)
  RVA=0x09F2A3F0  token=0x600068A  System.Boolean IsActive()
END_CLASS

CLASS: Axis
TYPE:  sealed struct
TOKEN: 0x20000D1
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.MagicaCapsuleCollider.AxisX  // const
  public    static  MagicaCloth.MagicaCapsuleCollider.AxisY  // const
  public    static  MagicaCloth.MagicaCapsuleCollider.AxisZ  // const
METHODS:
END_CLASS

CLASS: TeamUpdateMode
TYPE:  sealed struct
TOKEN: 0x20000D6
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.PhysicsTeam.TeamUpdateModeNormal  // const
  public    static  MagicaCloth.PhysicsTeam.TeamUpdateModeUnityPhysics  // const
METHODS:
END_CLASS

CLASS: TeamCullingMode
TYPE:  sealed struct
TOKEN: 0x20000D7
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.PhysicsTeam.TeamCullingModeReset  // const
  public    static  MagicaCloth.PhysicsTeam.TeamCullingModePause  // const
  public    static  MagicaCloth.PhysicsTeam.TeamCullingModeOff  // const
METHODS:
END_CLASS

CLASS: TeamSkinningMode
TYPE:  sealed struct
TOKEN: 0x20000D8
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.PhysicsTeam.TeamSkinningModeNone  // const
  public    static  MagicaCloth.PhysicsTeam.TeamSkinningModeUserAnimation  // const
METHODS:
END_CLASS

CLASS: AdjustRotationData
TYPE:  sealed struct
TOKEN: 0x20000DB
SIZE:  0x24
FIELDS:
  public            System.Int32                    keyIndex  // 0x10
  public            System.Int32                    targetIndex  // 0x14
  public            Unity.Mathematics.float3        localPos  // 0x18
METHODS:
  RVA=0x09F228AC  token=0x600071A  System.Boolean IsValid()
END_CLASS

CLASS: GroupData
TYPE:  sealed struct
TOKEN: 0x20000DC
SIZE:  0x38
FIELDS:
  public            System.Int32                    teamId  // 0x10
  public            System.Int32                    active  // 0x14
  public            System.Int32                    adjustMode  // 0x18
  public            Unity.Mathematics.float3        axisRotationPower  // 0x1c
  public            MagicaCloth.ChunkData           chunk  // 0x28
METHODS:
END_CLASS

CLASS: AdjustRotationJob
TYPE:  sealed struct
TOKEN: 0x20000DD
SIZE:  0xB0
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<MagicaCloth.AdjustRotationWorker.AdjustRotationData>dataList  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.AdjustRotationWorker.GroupData>groupList  // 0x20
  public            Unity.Collections.NativeParallelMultiHashMap<System.Int32,System.Int32>particleMap  // 0x30
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x40
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x50
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x60
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>snapBasePosList  // 0x70
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>snapBaseRotList  // 0x80
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0x90
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>rotList  // 0xa0
METHODS:
  RVA=0x09F228BC  token=0x600071B  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: BaseSkinningData
TYPE:  sealed struct
TOKEN: 0x20000DF
SIZE:  0x58
FIELDS:
  public            Unity.Mathematics.float3        localPos  // 0x10
  public            Unity.Mathematics.float3        localNor  // 0x1c
  public            Unity.Mathematics.float3        localTan  // 0x28
  public            Unity.Mathematics.int4          boneIndices  // 0x34
  public            Unity.Mathematics.float4        weights  // 0x44
  public            System.Int16                    weightCount  // 0x54
METHODS:
  RVA=0x09F2371C  token=0x6000725  System.Boolean IsValid()
END_CLASS

CLASS: GroupData
TYPE:  sealed struct
TOKEN: 0x20000E0
SIZE:  0x3C
FIELDS:
  public            System.Int32                    teamId  // 0x10
  public            System.Int32                    active  // 0x14
  public            System.Int32                    updateFixed  // 0x18
  public            MagicaCloth.ChunkData           dataChunk  // 0x1c
  public            MagicaCloth.ChunkData           bindPoseChunk  // 0x2c
METHODS:
END_CLASS

CLASS: BaseSkinningJob
TYPE:  sealed struct
TOKEN: 0x20000E1
SIZE:  0xD0
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<MagicaCloth.BaseSkinningWorker.GroupData>groupList  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.BaseSkinningWorker.BaseSkinningData>dataList  // 0x20
  public            Unity.Collections.NativeArray<Unity.Mathematics.float4x4>bindPoseList  // 0x30
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x40
  public            Unity.Collections.NativeArray<System.Int32>skinningBoneList  // 0x50
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>bonePosList  // 0x60
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>boneRotList  // 0x70
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>boneSclList  // 0x80
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x90
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0xa0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>snapBasePosList  // 0xb0
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>snapBaseRotList  // 0xc0
METHODS:
  RVA=0x09F23728  token=0x6000726  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: LineRotationData
TYPE:  sealed struct
TOKEN: 0x20000E3
SIZE:  0x38
FIELDS:
  public            System.Int32                    vertexIndex  // 0x10
  public            System.Int32                    childCount  // 0x14
  public            System.Int32                    childStartDataIndex  // 0x18
  public            Unity.Mathematics.float3        localPos  // 0x1c
  public            Unity.Mathematics.quaternion    localRot  // 0x28
METHODS:
END_CLASS

CLASS: LineRotationRootInfo
TYPE:  sealed struct
TOKEN: 0x20000E4
SIZE:  0x14
FIELDS:
  public            System.UInt16                   startIndex  // 0x10
  public            System.UInt16                   dataLength  // 0x12
METHODS:
END_CLASS

CLASS: GroupData
TYPE:  sealed struct
TOKEN: 0x20000E5
SIZE:  0x38
FIELDS:
  public            System.Int32                    teamId  // 0x10
  public            System.Int32                    avarage  // 0x14
  public            MagicaCloth.ChunkData           dataChunk  // 0x18
  public            MagicaCloth.ChunkData           rootInfoChunk  // 0x28
METHODS:
END_CLASS

CLASS: LineRotationJob
TYPE:  sealed struct
TOKEN: 0x20000E6
SIZE:  0x98
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.Int32                    fixedUpdateCount  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.LineWorker.LineRotationData>dataList  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.LineWorker.LineRotationRootInfo>rootInfoList  // 0x28
  public            Unity.Collections.NativeArray<System.Int32>rootTeamList  // 0x38
  public            Unity.Collections.NativeArray<MagicaCloth.LineWorker.GroupData>groupList  // 0x48
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x58
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x68
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0x78
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>rotList  // 0x88
METHODS:
  RVA=0x09F267FC  token=0x6000730  System.Void Execute(System.Int32 rootIndex)
END_CLASS

CLASS: CreateData
TYPE:  sealed struct
TOKEN: 0x20000E8
SIZE:  0x18
FIELDS:
  public            System.Int32                    vertexIndex  // 0x10
  public            System.Int32                    particleIndex  // 0x14
METHODS:
END_CLASS

CLASS: VertexToParticleJob
TYPE:  sealed struct
TOKEN: 0x20000E9
SIZE:  0xA0
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.VirtualMeshInfo>virtualMeshInfoList  // 0x10
  public            Unity.Collections.NativeArray<System.Int32>vertexToParticleList  // 0x20
  public            Unity.Collections.NativeParallelMultiHashMap<System.Int32,System.Int32>vertexToParticleMap  // 0x30
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0x40
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>rotList  // 0x50
  public            Unity.Collections.NativeArray<System.Int16>virtualVertexMeshIndexList  // 0x60
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>snapBasePosList  // 0x70
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>snapBaseRotList  // 0x80
  private           Unity.Collections.NativeParallelMultiHashMapIterator<System.Int32>iterator  // 0x90
METHODS:
  RVA=0x09F352D0  token=0x6000739  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: ParticleToVertexJob
TYPE:  sealed struct
TOKEN: 0x20000EA
SIZE:  0xE0
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<System.Int32>vertexToParticleList  // 0x10
  public            Unity.Collections.NativeParallelMultiHashMap<System.Int32,System.Int32>vertexToParticleMap  // 0x20
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>virtualPosList  // 0x30
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>virtualRotList  // 0x40
  public            Unity.Collections.NativeArray<System.Byte>virtualVertexFlagList  // 0x50
  public            Unity.Collections.NativeArray<System.Int16>virtualVertexMeshIndexList  // 0x60
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.VirtualMeshInfo>virtualMeshInfoList  // 0x70
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x80
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x90
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>particleFlagList  // 0xa0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>particlePosList  // 0xb0
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>particleRotList  // 0xc0
  private           Unity.Collections.NativeParallelMultiHashMapIterator<System.Int32>iterator  // 0xd0
METHODS:
  RVA=0x09F2A404  token=0x600073A  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: CalcVertexUseFlagJob
TYPE:  sealed struct
TOKEN: 0x20000EC
SIZE:  0x148
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            System.UInt32                   updateFlag  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.RenderMeshInfo>renderMeshInfoList  // 0x18
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.SharedRenderMeshInfo>sharedRenderMeshInfoList  // 0x28
  public            Unity.Collections.NativeArray<System.Byte>virtualVertexUseList  // 0x38
  public            Unity.Collections.NativeArray<System.Byte>virtualVertexFixList  // 0x48
  public            Unity.Collections.NativeArray<System.UInt32>sharedChildVertexInfoList  // 0x58
  public            Unity.Collections.NativeArray<MagicaCloth.MeshData.VertexWeight>sharedChildVertexWeightList  // 0x68
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>sharedRenderVertices  // 0x78
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>sharedRenderNormals  // 0x88
  public            Unity.Collections.NativeArray<Unity.Mathematics.float4>sharedRenderTangents  // 0x98
  public            Unity.Collections.NativeArray<System.Byte>sharedBonesPerVertexList  // 0xa8
  public            Unity.Collections.NativeArray<System.Int32>sharedBonesPerVertexStartList  // 0xb8
  public            Unity.Collections.NativeArray<UnityEngine.BoneWeight1>sharedBoneWeightList  // 0xc8
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>renderPosList  // 0xd8
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>renderNormalList  // 0xe8
  public            Unity.Collections.NativeArray<Unity.Mathematics.float4>renderTangentList  // 0xf8
  public            Unity.Collections.NativeArray<UnityEngine.BoneWeight1>renderBoneWeightList  // 0x108
  public            Unity.Collections.NativeArray<System.UInt32>renderVertexFlagList  // 0x118
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>renderPosArray  // 0x128
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>renderNormalArray  // 0x138
METHODS:
  RVA=0x09F24994  token=0x6000744  System.Void Execute(System.Int32 rmindex)
END_CLASS

CLASS: CollectLocalPositionNormalTangentForEachMeshJob
TYPE:  sealed struct
TOKEN: 0x20000ED
SIZE:  0xF0
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.RenderMeshInfo>renderMeshInfoList  // 0x10
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>transformPosList  // 0x20
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>transformRotList  // 0x30
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>transformSclList  // 0x40
  public            Unity.Collections.NativeArray<System.UInt32>sharedChildVertexInfoList  // 0x50
  public            Unity.Collections.NativeArray<MagicaCloth.MeshData.VertexWeight>sharedChildVertexWeightList  // 0x60
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>virtualPosList  // 0x70
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>virtualRotList  // 0x80
  public            Unity.Collections.NativeArray<System.UInt32>renderVertexFlagList  // 0x90
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>renderPosList  // 0xa0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>renderNormalList  // 0xb0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float4>renderTangentList  // 0xc0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>renderPosArray  // 0xd0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>renderNormalArray  // 0xe0
METHODS:
  RVA=0x09F24F30  token=0x6000745  System.Void Execute(System.Int32 rmindex)
END_CLASS

CLASS: CollectLocalPositionNormalTangentForEachVertexJob
TYPE:  sealed struct
TOKEN: 0x20000EE
SIZE:  0xF0
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.RenderMeshInfo>renderMeshInfoList  // 0x10
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>transformPosList  // 0x20
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>transformRotList  // 0x30
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>transformSclList  // 0x40
  public            Unity.Collections.NativeArray<System.UInt32>sharedChildVertexInfoList  // 0x50
  public            Unity.Collections.NativeArray<MagicaCloth.MeshData.VertexWeight>sharedChildVertexWeightList  // 0x60
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>virtualPosList  // 0x70
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>virtualRotList  // 0x80
  public            Unity.Collections.NativeArray<System.UInt32>renderVertexFlagList  // 0x90
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>renderPosList  // 0xa0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>renderNormalList  // 0xb0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float4>renderTangentList  // 0xc0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>renderPosArray  // 0xd0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>renderNormalArray  // 0xe0
METHODS:
  RVA=0x09F2528C  token=0x6000746  System.Void Execute(System.Int32 vindex)
END_CLASS

CLASS: SpringData
TYPE:  sealed struct
TOKEN: 0x20000F0
SIZE:  0x18
FIELDS:
  public            System.Int32                    particleIndex  // 0x10
  public            System.Single                   weight  // 0x14
METHODS:
END_CLASS

CLASS: SpringJob
TYPE:  sealed struct
TOKEN: 0x20000F1
SIZE:  0xD0
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<System.Int32>springVertexList  // 0x10
  public            Unity.Collections.NativeParallelMultiHashMap<System.Int32,MagicaCloth.SpringMeshWorker.SpringData>springMap  // 0x20
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x30
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>particlePosList  // 0x40
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>particleRotList  // 0x50
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>snapBasePosList  // 0x60
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>snapBaseRotList  // 0x70
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>virtualPosList  // 0x80
  public            Unity.Collections.NativeArray<System.Byte>virtualVertexFlagList  // 0x90
  public            Unity.Collections.NativeArray<System.Int16>virtualVertexMeshIndexList  // 0xa0
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.VirtualMeshInfo>virtualMeshInfoList  // 0xb0
  private           Unity.Collections.NativeParallelMultiHashMapIterator<System.Int32>iterator  // 0xc0
METHODS:
  RVA=0x09F339B0  token=0x600074F  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: TriangleRotationData
TYPE:  sealed struct
TOKEN: 0x20000F3
SIZE:  0x2C
FIELDS:
  public            System.Int32                    targetIndex  // 0x10
  public            System.Int32                    triangleCount  // 0x14
  public            System.Int32                    triangleStartIndex  // 0x18
  public            Unity.Mathematics.quaternion    localRot  // 0x1c
METHODS:
  RVA=0x09F34490  token=0x6000758  System.Boolean IsValid()
END_CLASS

CLASS: GroupData
TYPE:  sealed struct
TOKEN: 0x20000F4
SIZE:  0x34
FIELDS:
  public            System.Int32                    teamId  // 0x10
  public            MagicaCloth.ChunkData           triangleDataChunk  // 0x14
  public            MagicaCloth.ChunkData           triangleIndexChunk  // 0x24
METHODS:
END_CLASS

CLASS: TriangleRotationJob
TYPE:  sealed struct
TOKEN: 0x20000F5
SIZE:  0x90
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<MagicaCloth.TriangleWorker.TriangleRotationData>triangleDataList  // 0x10
  public            Unity.Collections.NativeArray<System.Int32>triangleIndexList  // 0x20
  public            Unity.Collections.NativeArray<MagicaCloth.TriangleWorker.GroupData>groupList  // 0x30
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x40
  public            Unity.Collections.NativeArray<System.Int32>teamIdList  // 0x50
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x60
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>posList  // 0x70
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>rotList  // 0x80
METHODS:
  RVA=0x09F34498  token=0x6000759  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: ReadMeshPositionJob
TYPE:  sealed struct
TOKEN: 0x20000F7
SIZE:  0xE0
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.VirtualMeshInfo>virtualMeshInfoList  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.SharedVirtualMeshInfo>sharedVirtualMeshInfoList  // 0x20
  public            Unity.Collections.NativeArray<System.Int16>virtualVertexMeshIndexList  // 0x30
  public            Unity.Collections.NativeArray<System.Byte>virtualVertexUseList  // 0x40
  public            Unity.Collections.NativeArray<System.Int32>virtualTransformIndexList  // 0x50
  public            Unity.Collections.NativeArray<System.UInt32>sharedVirtualVertexInfoList  // 0x60
  public            Unity.Collections.NativeArray<MagicaCloth.MeshData.VertexWeight>sharedVirtualWeightList  // 0x70
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>transformPosList  // 0x80
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>transformRotList  // 0x90
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>transformSclList  // 0xa0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>virtualPosList  // 0xb0
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>virtualRotList  // 0xc0
  public            Unity.Collections.NativeArray<System.Byte>virtualVertexFlagList  // 0xd0
METHODS:
  RVA=0x09F474E0  token=0x6000762  System.Void Execute(System.Int32 vindex)
END_CLASS

CLASS: CalcMeshTriangleNormalTangentJob
TYPE:  sealed struct
TOKEN: 0x20000F8
SIZE:  0xB0
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.VirtualMeshInfo>virtualMeshInfoList  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.SharedVirtualMeshInfo>sharedVirtualMeshInfoList  // 0x20
  public            Unity.Collections.NativeArray<System.UInt16>virtualTriangleMeshIndexList  // 0x30
  public            Unity.Collections.NativeArray<System.Byte>virtualVertexUseList  // 0x40
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>virtualPosList  // 0x50
  public            Unity.Collections.NativeArray<System.Int32>sharedTriangles  // 0x60
  public            Unity.Collections.NativeArray<Unity.Mathematics.float2>sharedMeshUv  // 0x70
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>virtualTriangleNormalList  // 0x80
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>virtualTriangleTangentList  // 0x90
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>transformSclList  // 0xa0
METHODS:
  RVA=0x09F364E0  token=0x6000763  System.Void Execute(System.Int32 tindex)
END_CLASS

CLASS: CalcVertexNormalTangentFromTriangleJob
TYPE:  sealed struct
TOKEN: 0x20000F9
SIZE:  0xB0
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.VirtualMeshInfo>virtualMeshInfoList  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.SharedVirtualMeshInfo>sharedVirtualMeshInfoList  // 0x20
  public            Unity.Collections.NativeArray<System.Int16>virtualVertexMeshIndexList  // 0x30
  public            Unity.Collections.NativeArray<System.Byte>virtualVertexUseList  // 0x40
  public            Unity.Collections.NativeArray<System.Byte>virtualVertexFlagList  // 0x50
  public            Unity.Collections.NativeArray<System.UInt32>sharedVirtualVertexToTriangleInfoList  // 0x60
  public            Unity.Collections.NativeArray<System.Int32>sharedVirtualVertexToTriangleIndexList  // 0x70
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>virtualTriangleNormalList  // 0x80
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>virtualTriangleTangentList  // 0x90
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>virtualRotList  // 0xa0
METHODS:
  RVA=0x09F36ACC  token=0x6000764  System.Void Execute(System.Int32 vindex)
END_CLASS

CLASS: CalcMeshTriangleNormalTangentForEachMeshJob
TYPE:  sealed struct
TOKEN: 0x20000FA
SIZE:  0xE0
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.VirtualMeshInfo>virtualMeshInfoList  // 0x10
  public            Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.SharedVirtualMeshInfo>sharedVirtualMeshInfoList  // 0x20
  public            Unity.Collections.NativeArray<System.Byte>virtualVertexUseList  // 0x30
  public            Unity.Collections.NativeArray<System.Byte>virtualVertexFlagList  // 0x40
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>virtualPosList  // 0x50
  public            Unity.Collections.NativeArray<System.Int32>sharedTriangles  // 0x60
  public            Unity.Collections.NativeArray<Unity.Mathematics.float2>sharedMeshUv  // 0x70
  public            Unity.Collections.NativeArray<System.UInt32>sharedVirtualVertexToTriangleInfoList  // 0x80
  public            Unity.Collections.NativeArray<System.Int32>sharedVirtualVertexToTriangleIndexList  // 0x90
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>transformSclList  // 0xa0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>virtualTriangleNormalList  // 0xb0
  public            Unity.Collections.NativeArray<Unity.Mathematics.float3>virtualTriangleTangentList  // 0xc0
  public            Unity.Collections.NativeArray<Unity.Mathematics.quaternion>virtualRotList  // 0xd0
METHODS:
  RVA=0x09F35BA4  token=0x6000765  System.Void Execute(System.Int32 mindex)
END_CLASS

CLASS: <Start>d__3
TYPE:  sealed class
TOKEN: 0x20000FE
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            MagicaCloth.BlastWaveController <>4__this  // 0x20
  private           System.Single                   <main>5__2  // 0x28
  private           System.Single                   <time>5__3  // 0x2c
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000770  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000771  System.Void System.IDisposable.Dispose()
  RVA=0x09F49C0C  token=0x6000772  System.Boolean MoveNext()
  RVA=0x09F49D9C  token=0x6000774  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: MoveMode
TYPE:  sealed struct
TOKEN: 0x2000100
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.CameraOrbit.MoveModeNone  // const
  public    static  MagicaCloth.CameraOrbit.MoveModeUpDown  // const
  public    static  MagicaCloth.CameraOrbit.MoveModeFree  // const
METHODS:
END_CLASS

CLASS: <CreateObject>d__7
TYPE:  sealed class
TOKEN: 0x2000102
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            MagicaCloth.CopyObject          <>4__this  // 0x20
  private           System.Int32                    <i>5__2  // 0x28
  private           System.Int32                    <j>5__3  // 0x2c
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000786  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000787  System.Void System.IDisposable.Dispose()
  RVA=0x09F499DC  token=0x6000788  System.Boolean MoveNext()
  RVA=0x09F49BC0  token=0x600078A  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: AvatarPartsGroup
TYPE:  class
TOKEN: 0x2000104
SIZE:  0x30
FIELDS:
  public            System.String                   groupName  // 0x10
  public            System.Collections.Generic.List<UnityEngine.GameObject>partsPrefabList  // 0x18
  public            System.Int32                    id  // 0x20
  public            System.Int32                    handle  // 0x24
  public            System.Int32                    index  // 0x28
METHODS:
  RVA=0x09F35848  token=0x6000794  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000106
SIZE:  0x10
FIELDS:
  public    static readonly MagicaCloth.ModelController.<>c <>9  // static @ 0x0
  public    static  System.Action<UnityEngine.Animator><>9__7_0  // static @ 0x8
  public    static  System.Action<UnityEngine.Animator><>9__8_0  // static @ 0x10
  public    static  System.Action<MagicaCloth.BaseCloth><>9__10_0  // static @ 0x18
METHODS:
  RVA=0x09F49F84  token=0x600079E  System.Void .cctor()
  RVA=0x041E1670  token=0x600079F  System.Void .ctor()
  RVA=0x09F49E8C  token=0x60007A0  System.Void <OnNextButton>b__7_0(UnityEngine.Animator ani)
  RVA=0x09F49E44  token=0x60007A1  System.Void <OnBackButton>b__8_0(UnityEngine.Animator ani)
  RVA=0x09F49DE8  token=0x60007A2  System.Void <OnActiveButton>b__10_0(MagicaCloth.BaseCloth cloth)
END_CLASS

CLASS: <>c__DisplayClass9_0
TYPE:  sealed class
TOKEN: 0x2000107
SIZE:  0x18
FIELDS:
  public            System.Single                   timeScale  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60007A3  System.Void .ctor()
  RVA=0x09F49F3C  token=0x60007A4  System.Void <OnSlowButton>b__0(UnityEngine.Animator ani)
  RVA=0x09F49F60  token=0x60007A5  System.Void <OnSlowButton>b__1(MagicaCloth.BaseCloth cloth)
END_CLASS

CLASS: <>c__DisplayClass5_0
TYPE:  sealed class
TOKEN: 0x200010C
SIZE:  0x20
FIELDS:
  public            System.Action<System.Int32,System.Int32>onClick  // 0x10
  public            MagicaCloth.UIPartsItem         <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60007BE  System.Void .ctor()
  RVA=0x09F49ED4  token=0x60007BF  System.Void <Init>b__0()
  RVA=0x09F49F08  token=0x60007C0  System.Void <Init>b__1()
END_CLASS

CLASS: Point
TYPE:  class
TOKEN: 0x2000116
SIZE:  0x20
FIELDS:
  public            System.Int32                    id  // 0x10
  public            Unity.Mathematics.float3        pos  // 0x14
METHODS:
  RVA=0x041E1670  token=0x600080F  System.Void .ctor()
END_CLASS

CLASS: TetraVertex
TYPE:  class
TOKEN: 0x2000128
SIZE:  0x20
FIELDS:
  public            System.Int32                    index  // 0x10
  public            UnityEngine.Vector3             pos  // 0x14
METHODS:
  RVA=0x041E1670  token=0x60008F3  System.Void .ctor()
  RVA=0x04DA3830  token=0x60008F4  System.Void .ctor(UnityEngine.Vector3 pos, System.Int32 index)
END_CLASS

CLASS: Tetra
TYPE:  class
TOKEN: 0x2000129
SIZE:  0x38
FIELDS:
  public            System.Collections.Generic.List<MagicaCloth.MeshUtility.TetraVertex>vertexList  // 0x10
  public            UnityEngine.Vector3             circumCenter  // 0x18
  public            System.Single                   circumRadius  // 0x24
  public            UnityEngine.Vector3             tetraCenter  // 0x28
  public            System.Single                   tetraSize  // 0x34
METHODS:
  RVA=0x09F4C934  token=0x60008F5  System.Void .ctor()
  RVA=0x09F4C818  token=0x60008F6  System.Void .ctor(MagicaCloth.MeshUtility.TetraVertex a, MagicaCloth.MeshUtility.TetraVertex b, MagicaCloth.MeshUtility.TetraVertex c, MagicaCloth.MeshUtility.TetraVertex d)
  RVA=0x09F4C4CC  token=0x60008F7  System.UInt64 GetTetraHash()
  RVA=0x09F4BA9C  token=0x60008F8  System.Void CalcCircumcircle()
  RVA=0x09ED4E1C  token=0x60008F9  System.Boolean IntersectCircumcircle(UnityEngine.Vector3 pos)
  RVA=0x09ED4B88  token=0x60008FA  System.Boolean CheckSame(MagicaCloth.MeshUtility.Tetra tri)
  RVA=0x09F4C478  token=0x60008FB  System.Boolean ContainsPoint(MagicaCloth.MeshUtility.TetraVertex p1)
  RVA=0x09F4C3AC  token=0x60008FC  System.Boolean ContainsPoint(MagicaCloth.MeshUtility.TetraVertex p1, MagicaCloth.MeshUtility.TetraVertex p2, MagicaCloth.MeshUtility.TetraVertex p3, MagicaCloth.MeshUtility.TetraVertex p4)
  RVA=0x09F4C104  token=0x60008FD  System.Void CalcSize()
  RVA=0x09F4C5C0  token=0x60008FE  System.Boolean Verification()
END_CLASS

CLASS: UpdateCount
TYPE:  sealed struct
TOKEN: 0x200012C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.UpdateTimeManager.UpdateCount_60  // const
  public    static  MagicaCloth.UpdateTimeManager.UpdateCount_90_Default  // const
  public    static  MagicaCloth.UpdateTimeManager.UpdateCount_120  // const
  public    static  MagicaCloth.UpdateTimeManager.UpdateCount_150  // const
  public    static  MagicaCloth.UpdateTimeManager.UpdateCount_180  // const
METHODS:
END_CLASS

CLASS: UpdateMode
TYPE:  sealed struct
TOKEN: 0x200012D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.UpdateTimeManager.UpdateModeUnscaledTime  // const
  public    static  MagicaCloth.UpdateTimeManager.UpdateModeOncePerFrame  // const
  public    static  MagicaCloth.UpdateTimeManager.UpdateModeDelayUnscaledTime  // const
METHODS:
END_CLASS

CLASS: UpdateLocation
TYPE:  sealed struct
TOKEN: 0x200012E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.UpdateTimeManager.UpdateLocationAfterLateUpdate  // const
  public    static  MagicaCloth.UpdateTimeManager.UpdateLocationBeforeLateUpdate  // const
METHODS:
END_CLASS

CLASS: MagicaCloth.BaseCloth
TYPE:  abstract class
TOKEN: 0x2000025
SIZE:  0xC8
EXTENDS: MagicaCloth.PhysicsTeam
FIELDS:
  protected         MagicaCloth.ClothParams         clothParams  // 0x78
  protected         System.Collections.Generic.List<System.Int32>clothParamDataHashList  // 0x80
  private           MagicaCloth.ClothData           clothData  // 0x88
  protected         System.Int32                    clothDataHash  // 0x90
  protected         System.Int32                    clothDataVersion  // 0x94
  private           MagicaCloth.SelectionData       clothSelection  // 0x98
  private           System.Int32                    clothSelectionHash  // 0xa0
  private           System.Int32                    clothSelectionVersion  // 0xa4
  private           System.Collections.Generic.List<UnityEngine.Renderer>cullRendererList  // 0xa8
  protected         MagicaCloth.ClothSetup          setup  // 0xb0
  private           System.Single                   oldBlendRatio  // 0xb8
  private           MagicaCloth.PhysicsTeam.TeamUpdateModeoldUpdateMode  // 0xbc
  private           MagicaCloth.PhysicsTeam.TeamCullingModeoldCullingMode  // 0xc0
  private           System.Boolean                  oldUseAnimatedDistance  // 0xc4
PROPERTIES:
  BlendWeight  get=0x04D863F0  set=0x09EC1054
  Gravity_GravityDirection  get=0x09EC0F08  set=0x09EC1374
  DistanceDisable_Active  get=0x09EC0E48  set=0x09EC1130
  DistanceDisable_ReferenceObject  get=0x09EC0EA8  set=0x09EC11A0
  DistanceDisable_Distance  get=0x09EC0E68  set=0x09EC1150
  DistanceDisable_FadeDistance  get=0x09EC0E88  set=0x09EC1178
  ExternalForce_WindInfluence  get=0x09EC0EC8  set=0x09EC11CC
  ExternalForce_WindRandomScale  get=0x09EC0EE8  set=0x09EC12A0
  WorldInfluence_MaxMoveSpeed  get=0x09EC0F94  set=0x09EC16AC
  WorldInfluence_ResetAfterTeleport  get=0x09EC0FB4  set=0x09EC1774
  WorldInfluence_TeleportDistance  get=0x09EC0FF4  set=0x09EC18DC
  WorldInfluence_TeleportRotation  get=0x09EC1034  set=0x09EC1A60
  WorldInfluence_TeleportMode  get=0x09EC1014  set=0x09EC19A0
  WorldInfluence_StabilizationTime  get=0x09EC0FD4  set=0x09EC1838
  ColliderCollision_Active  get=0x09EC0E28  set=0x09EC106C
  Penetration_Active  get=0x09EC0F34  set=0x09EC1454
  Spring_Active  get=0x09EC0F54  set=0x09EC151C
  Spring_Power  get=0x09EC0F74  set=0x09EC15DC
  Params  get=0x04D862A0
  ClothData  get=0x04D861F0  set=0x05395D30
  ClothSelection  get=0x04D85EE0
  Setup  get=0x04D86250
METHODS:
  RVA=0x09EC01C0  token=0x60000B5  System.Void ResetCloth()
  RVA=0x09EC0190  token=0x60000B6  System.Void ResetCloth(System.Single resetStabilizationTime)
  RVA=0x09EC01F0  token=0x60000B7  System.Void ResetCloth(MagicaCloth.ClothParams.TeleportMode teleportMode, System.Single resetStabilizationTime)
  RVA=0x09EC01F8  token=0x60000B8  System.Void SetTimeScale(System.Single timeScale)
  RVA=0x09EBF494  token=0x60000B9  System.Single GetTimeScale()
  RVA=0x09EBE780  token=0x60000BA  System.Void AddForce(UnityEngine.Vector3 force, MagicaCloth.PhysicsManagerTeamData.ForceMode mode)
  RVA=0x09EBE6DC  token=0x60000BD  System.Void AddCollider(MagicaCloth.ColliderComponent collider)
  RVA=0x09EBFEE0  token=0x60000BE  System.Void RemoveCollider(MagicaCloth.ColliderComponent collider)
  RVA=0x04D865A0  token=0x60000BF  System.Void SetUpdateMode(MagicaCloth.PhysicsTeam.TeamUpdateMode updateMode)
  RVA=0x04D86590  token=0x60000C0  System.Void SetCullingMode(MagicaCloth.PhysicsTeam.TeamCullingMode cullingMode)
  RVA=0x09EBFD58  token=0x60000C1  System.Void Radius_SetRadius(System.Single startVal, System.Single endVal, System.Single curveVal)
  RVA=0x09EBF714  token=0x60000C2  System.Void Mass_SetMass(System.Single startVal, System.Single endVal, System.Single curveVal)
  RVA=0x09EBEA80  token=0x60000C3  System.Void ClampPosition_SetPositionLength(System.Single startVal, System.Single endVal, System.Single curveVal)
  RVA=0x09EBF58C  token=0x60000C4  System.Void Gravity_SetGravity(System.Single startVal, System.Single endVal, System.Single curveVal)
  RVA=0x09EBEF80  token=0x60000C7  System.Void Drag_SetDrag(System.Single startVal, System.Single endVal, System.Single curveVal)
  RVA=0x09EBF15C  token=0x60000D0  System.Void ExternalForce_DepthInfluence(System.Single startVal, System.Single endVal, System.Single curveVal)
  RVA=0x09EC0B30  token=0x60000D5  System.Void WorldInfluence_SetMovementInfluence(System.Single startVal, System.Single endVal, System.Single curveVal)
  RVA=0x09EC0C2C  token=0x60000D6  System.Void WorldInfluence_SetRotationInfluence(System.Single startVal, System.Single endVal, System.Single curveVal)
  RVA=0x09EC09E8  token=0x60000E3  System.Void WorldInfluence_ReplaceInfluenceTarget(UnityEngine.Transform target)
  RVA=0x09EBFC48  token=0x60000E8  System.Void Penetration_SetMovingRadius(System.Single startVal, System.Single endVal, System.Single curveVal)
  RVA=0x09EBF3C8  token=0x60000ED  System.Int32 GetDataHash()
  RVA=0x041E1670  token=0x60000F3  System.Void Reset()
  RVA=0x09EBFC00  token=0x60000F4  System.Void OnValidate()
  RVA=0x09EBFBE0  token=0x60000F5  System.Void OnInit()
  RVA=0x09EBF8AC  token=0x60000F6  System.Void OnActive()
  RVA=0x09EBFADC  token=0x60000F7  System.Void OnInactive()
  RVA=0x09EBFABC  token=0x60000F8  System.Void OnDispose()
  RVA=0x09EC0528  token=0x60000F9  System.Void UpdateCullingMode(MagicaCloth.CoreComponent caller)
  RVA=0x09EBF9C4  token=0x60000FA  System.Void OnChangeCalculation()
  RVA=0x09EBF2A8  token=0x60000FB  System.Int32 GetCullRenderListCount()
  RVA=0x09EBE8F8  token=0x60000FC  System.Void BaseClothInit()
  RVA=0x09EBE860  token=0x60000FD  System.Void BaseClothDispose()
  RVA=0x09EBECD0  token=0x60000FE  System.Void ClothInit()
  RVA=0x09EBEC2C  token=0x60000FF  System.Void ClothActive()
  RVA=0x09EBECA8  token=0x6000100  System.Void ClothInactive()
  RVA=0x041E1670  token=0x6000101  System.Void ClothDispose()
  RVA=-1  // abstract  token=0x6000102  System.UInt32 UserFlag(System.Int32 vindex)
  RVA=-1  // abstract  token=0x6000103  UnityEngine.Transform UserTransform(System.Int32 vindex)
  RVA=-1  // abstract  token=0x6000104  Unity.Mathematics.float3 UserTransformLocalPosition(System.Int32 vindex)
  RVA=-1  // abstract  token=0x6000105  Unity.Mathematics.quaternion UserTransformLocalRotation(System.Int32 vindex)
  RVA=-1  // abstract  token=0x6000106  System.Boolean IsRequiresDeformer()
  RVA=-1  // abstract  token=0x6000107  MagicaCloth.BaseMeshDeformer GetDeformer()
  RVA=-1  // abstract  token=0x6000108  MagicaCloth.MeshData GetMeshData()
  RVA=-1  // abstract  token=0x6000109  System.Void WorkerInit()
  RVA=0x09EC0280  token=0x600010A  System.Void SetUseMesh(System.Boolean sw)
  RVA=0x09EC02EC  token=0x600010B  System.Void SetUseVertex(System.Boolean sw)
  RVA=-1  // abstract  token=0x600010C  System.Void SetDeformerUseVertex(System.Boolean sw, MagicaCloth.BaseMeshDeformer deformer)
  RVA=0x09EBEF44  token=0x600010D  System.Void DeformerForEach(System.Action<MagicaCloth.BaseMeshDeformer> act)
  RVA=0x09EC0354  token=0x600010E  System.Void UpdateBlend()
  RVA=-1  // generic def  token=0x600010F  System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict)
  RVA=0x09EBF510  token=0x6000110  System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones()
  RVA=0x09EBE9F4  token=0x6000111  System.Void ChangeUseUnityPhysics(System.Boolean sw)
  RVA=0x09EBED98  token=0x6000112  System.Void CreateVerifyData()
  RVA=0x09EC08CC  token=0x6000113  MagicaCloth.Define.Error VerifyData()
  RVA=0x09EBF664  token=0x6000114  System.Boolean HasChangedParam(MagicaCloth.ClothParams.ParamType ptype)
  RVA=0x09EC0844  token=0x6000115  MagicaCloth.Define.Error VerifyAlgorithmVersion()
  RVA=0x09EC080C  token=0x6000116  System.Boolean UpgradeFormat()
  RVA=0x09EBF22C  token=0x6000117  System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject()
  RVA=0x09EBF058  token=0x6000118  MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source)
  RVA=0x09EBFF6C  token=0x6000119  System.Void ResetClothInternal(MagicaCloth.ClothParams.TeleportMode teleportMode, System.Single resetStabilizationTime)
  RVA=0x09EC0D28  token=0x600011A  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.CoreComponent
TYPE:  abstract class
TOKEN: 0x2000027
SIZE:  0x38
EXTENDS: MagicaCloth.BaseComponent
IMPLEMENTS: MagicaCloth.IShareDataObject MagicaCloth.IDataVerify MagicaCloth.IEditorMesh MagicaCloth.IEditorCloth MagicaCloth.IDataHash MagicaCloth.IBoneReplace
FIELDS:
  protected         System.Int32                    dataHash  // 0x18
  protected         System.Int32                    dataVersion  // 0x1c
  protected         MagicaCloth.RuntimeStatus       status  // 0x20
  private           System.Int32                    useUnityPhysicsCount  // 0x28
  private           System.Boolean                  nowUseUnityPhysics  // 0x2c
  private           System.Boolean                  <IsVisible>k__BackingField  // 0x2d
  protected         System.Int32                    calculateValue  // 0x30
PROPERTIES:
  Status  get=0x04D862C0
  IsVisible  get=0x04D86CB0  set=0x04D86CE0
  IsCalculate  get=0x068D2DA0
  SaveDataHash  get=0x011EF5B0
  SaveDataVersion  get=0x04D86310
METHODS:
  RVA=0x09EE5F4C  token=0x600011E  System.Collections.Generic.Dictionary<System.String,UnityEngine.Transform> GetUsedComponentBones()
  RVA=0x09EE5EB0  token=0x600011F  System.Collections.Generic.List<System.String> GetUsedComponentBoneNames()
  RVA=0x09EE65B8  token=0x6000120  System.Void ReplaceComponentBone(System.Collections.Generic.Dictionary<UnityEngine.Transform,UnityEngine.Transform> boneReplaceDict)
  RVA=0x09EE6570  token=0x6000121  System.Void ReplaceComponentBone(System.Collections.Generic.Dictionary<System.String,UnityEngine.Transform> boneReplaceDict)
  RVA=-1  // abstract  token=0x6000126  System.Int32 GetDataHash()
  RVA=0x09EE6698  token=0x6000129  System.Void Start()
  RVA=0x09EE64CC  token=0x600012A  System.Void OnEnable()
  RVA=0x09EE648C  token=0x600012B  System.Void OnDisable()
  RVA=0x09EE6418  token=0x600012C  System.Void OnDestroy()
  RVA=0x09EE622C  token=0x600012D  System.Void Init()
  RVA=-1  // abstract  token=0x600012E  System.Void OnInit()
  RVA=-1  // abstract  token=0x600012F  System.Void OnDispose()
  RVA=-1  // abstract  token=0x6000130  System.Void OnUpdate()
  RVA=-1  // abstract  token=0x6000131  System.Void OnActive()
  RVA=-1  // abstract  token=0x6000132  System.Void OnInactive()
  RVA=0x09EE6504  token=0x6000133  System.Void OnUpdateStatus()
  RVA=0x09EE64C4  token=0x6000134  System.Void OnDisconnectedStatus()
  RVA=0x09EE6600  token=0x6000135  System.Void SetUseUnityPhysics(System.Boolean sw)
  RVA=0x09EE5C58  token=0x6000136  System.Void ActiveUseUnityPhysics()
  RVA=0x09EE6210  token=0x6000137  System.Void InactiveUseUnityPhysics()
  RVA=0x041E1670  token=0x6000138  System.Void ChangeUseUnityPhysics(System.Boolean sw)
  RVA=0x0931FCE4  token=0x6000139  System.Boolean IsUseUnityPhysics()
  RVA=0x09EE5CAC  token=0x600013A  System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject()
  RVA=-1  // abstract  token=0x600013B  MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source)
  RVA=0x09EE6664  token=0x600013C  System.Void SetUserEnable(System.Boolean sw)
  RVA=0x041E1670  token=0x600013D  System.Void UpdateCullingMode(MagicaCloth.CoreComponent Caller)
  RVA=0x041E1670  token=0x600013E  System.Void OnChangeCalculation()
  RVA=-1  // abstract  token=0x600013F  System.Int32 GetVersion()
  RVA=-1  // abstract  token=0x6000140  System.Int32 GetErrorVersion()
  RVA=0x09EE66DC  token=0x6000141  MagicaCloth.Define.Error VerifyData()
  RVA=0x09EE66A0  token=0x6000142  MagicaCloth.Define.Error VerifyDataVersion()
  RVA=0x09EE63F4  token=0x6000143  System.Boolean IsOldDataVertion()
  RVA=0x09EE5C7C  token=0x6000144  System.Void CreateVerifyData()
  RVA=0x09EE5DF0  token=0x6000145  System.String GetInformation()
  RVA=0x012081B0  token=0x6000146  System.Boolean UpgradeFormat()
  RVA=-1  // generic def  token=0x6000147  System.Void ChangeAvatar(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict)
  RVA=-1  // generic def  token=0x6000148  System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict)
  RVA=0x09EE5E1C  token=0x6000149  System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones()
  RVA=0x09EE5D58  token=0x600014A  System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList)
  RVA=0x09EE5DA4  token=0x600014B  System.Collections.Generic.List<System.Int32> GetEditorTriangleList()
  RVA=0x09EE5D0C  token=0x600014C  System.Collections.Generic.List<System.Int32> GetEditorLineList()
  RVA=0x011EC580  token=0x600014D  System.Collections.Generic.List<System.Int32> GetSelectionList()
  RVA=0x011EC580  token=0x600014E  System.Collections.Generic.List<System.Int32> GetUseList()
  RVA=0x09EE6760  token=0x600014F  System.Void .ctor()
  RVA=0x0464E630  token=0x6000150  System.Object <Init>b__27_0()
END_CLASS

CLASS: MagicaCloth.AvatarPartsAttachEvent
TYPE:  class
TOKEN: 0x2000028
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`2
FIELDS:
METHODS:
  RVA=0x09ED815C  token=0x6000151  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.AvatarPartsDetachEvent
TYPE:  class
TOKEN: 0x2000029
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x09ED8198  token=0x6000152  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.PhysicsManagerPreUpdateEvent
TYPE:  class
TOKEN: 0x200002A
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent
FIELDS:
METHODS:
  RVA=0x05393538  token=0x6000153  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.PhysicsManagerPostUpdateEvent
TYPE:  class
TOKEN: 0x200002B
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent
FIELDS:
METHODS:
  RVA=0x05393538  token=0x6000154  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.MagicaAreaWind
TYPE:  class
TOKEN: 0x200002C
SIZE:  0x60
EXTENDS: MagicaCloth.WindComponent
FIELDS:
  private           MagicaCloth.PhysicsManagerWindData.ShapeTypeshapeType  // 0x58
  private           System.Boolean                  isAddition  // 0x5c
PROPERTIES:
  ShapeType  get=0x04D86570  set=0x09EE69DC
  Addition  get=0x04D86D90  set=0x09EE69BC
METHODS:
  RVA=0x04DA3370  token=0x6000159  MagicaCloth.ComponentType GetComponentType()
  RVA=0x02BBE540  token=0x600015A  MagicaCloth.PhysicsManagerWindData.WindType GetWindType()
  RVA=0x04D86570  token=0x600015B  MagicaCloth.PhysicsManagerWindData.ShapeType GetShapeType()
  RVA=0x09EE6944  token=0x600015C  MagicaCloth.PhysicsManagerWindData.DirectionType GetDirectionType()
  RVA=0x04D86D90  token=0x600015D  System.Boolean IsAddition()
  RVA=0x09EE6804  token=0x600015E  UnityEngine.Vector3 GetAreaSize()
  RVA=0x09EE689C  token=0x600015F  System.Single GetAreaVolume()
  RVA=0x09EE67B8  token=0x6000160  System.Single GetAreaLength()
  RVA=0x09EE6954  token=0x6000161  System.Void ResetParams()
  RVA=0x09EE69B4  token=0x6000162  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.MagicaAvatar
TYPE:  class
TOKEN: 0x200002D
SIZE:  0x58
EXTENDS: MagicaCloth.CoreComponent
FIELDS:
  private   static  System.Int32                    DATA_VERSION  // const
  private           System.Boolean                  dataReset  // 0x38
  private           MagicaCloth.MagicaAvatarRuntime runtime  // 0x40
  public            MagicaCloth.AvatarPartsAttachEventOnAttachParts  // 0x48
  public            MagicaCloth.AvatarPartsDetachEventOnDetachParts  // 0x50
PROPERTIES:
  DataReset  get=0x011F2A70  set=0x011F2A80
  Runtime  get=0x04A37980
METHODS:
  RVA=0x09EE959C  token=0x6000163  System.Int32 AttachAvatarParts(UnityEngine.GameObject avatarPartsPrefab, System.Action<UnityEngine.GameObject> instanceAction)
  RVA=0x09EE969C  token=0x6000164  System.Void DetachAvatarParts(System.Int32 partsId)
  RVA=0x09EE96C8  token=0x6000165  System.Void DetachAvatarParts(UnityEngine.GameObject avatarPartsObject)
  RVA=0x09EE966C  token=0x6000166  System.Void DetachAvatarParts(MagicaCloth.MagicaAvatarParts parts)
  RVA=0x04DA3390  token=0x6000167  MagicaCloth.ComponentType GetComponentType()
  RVA=0x011EC580  token=0x6000168  System.Int32 GetDataHash()
  RVA=0x04D919E0  token=0x600016C  System.Void Reset()
  RVA=0x041E1670  token=0x600016D  System.Void OnValidate()
  RVA=0x09EE9DE4  token=0x600016E  System.Void OnInit()
  RVA=0x09EE9D94  token=0x600016F  System.Void OnDispose()
  RVA=0x041E1670  token=0x6000170  System.Void OnUpdate()
  RVA=0x09EE9D6C  token=0x6000171  System.Void OnActive()
  RVA=0x09EE9DBC  token=0x6000172  System.Void OnInactive()
  RVA=0x04D85B80  token=0x6000173  System.Int32 GetVersion()
  RVA=0x011EC580  token=0x6000174  System.Int32 GetErrorVersion()
  RVA=0x09EE9664  token=0x6000175  System.Void CreateVerifyData()
  RVA=0x09EE9E0C  token=0x6000176  MagicaCloth.Define.Error VerifyData()
  RVA=0x09EE9740  token=0x6000177  System.String GetInformation()
  RVA=0x09EE9738  token=0x6000178  System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject()
  RVA=0x011EC580  token=0x6000179  MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source)
  RVA=0x09EE9E84  token=0x600017A  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.MagicaDirectionalWind
TYPE:  class
TOKEN: 0x200002E
SIZE:  0x58
EXTENDS: MagicaCloth.WindComponent
FIELDS:
METHODS:
  RVA=0x04DA33E0  token=0x600017B  MagicaCloth.ComponentType GetComponentType()
  RVA=0x04D85B80  token=0x600017C  MagicaCloth.PhysicsManagerWindData.WindType GetWindType()
  RVA=0x011EC580  token=0x600017D  MagicaCloth.PhysicsManagerWindData.ShapeType GetShapeType()
  RVA=0x011EC580  token=0x600017E  MagicaCloth.PhysicsManagerWindData.DirectionType GetDirectionType()
  RVA=0x012081B0  token=0x600017F  System.Boolean IsAddition()
  RVA=0x04DA33B0  token=0x6000180  UnityEngine.Vector3 GetAreaSize()
  RVA=0x04DA33D0  token=0x6000181  System.Single GetAreaVolume()
  RVA=0x04DA33A0  token=0x6000182  System.Single GetAreaLength()
  RVA=0x09EE9F64  token=0x6000183  System.Void ResetParams()
  RVA=0x09EE69B4  token=0x6000184  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.MagicaPhysicsManager
TYPE:  class
TOKEN: 0x200002F
SIZE:  0x80
EXTENDS: MagicaCloth.CreateSingleton`1
FIELDS:
  private           MagicaCloth.UpdateTimeManager   updateTime  // 0x18
  private           MagicaCloth.PhysicsManagerParticleDataparticle  // 0x20
  private           MagicaCloth.PhysicsManagerBoneDatabone  // 0x28
  private           MagicaCloth.PhysicsManagerMeshDatamesh  // 0x30
  private           MagicaCloth.PhysicsManagerTeamDatateam  // 0x38
  private           MagicaCloth.PhysicsManagerWindDatawind  // 0x40
  private           MagicaCloth.PhysicsManagerComponentcomponent  // 0x48
  private           MagicaCloth.PhysicsManagerComputecompute  // 0x50
  private           System.Boolean                  useFasterWrite  // 0x58
  private           UnityEngine.ComputeShader       meshWriter  // 0x60
  public            MagicaCloth.PhysicsManagerPreUpdateEventOnPreUpdate  // 0x68
  public            MagicaCloth.PhysicsManagerPostUpdateEventOnPostUpdate  // 0x70
  private           System.Boolean                  useDelay  // 0x78
  private           System.Boolean                  updatePlayerLoop  // 0x79
  private           System.Boolean                  isActive  // 0x7a
PROPERTIES:
  UpdatePerSeccond  get=0x09EEA36C  set=0x09EEA3D4
  UpdateMode  get=0x09EEA350  set=0x09EEA3B8
  FuturePredictionRate  get=0x09EEA1B8  set=0x09EEA388
  UpdateTime  get=0x0385B100
  Particle  get=0x04A379B0
  Bone  get=0x03233960
  Mesh  get=0x03235430
  Team  get=0x032354C0
  Wind  get=0x04A37980
  Component  get=0x03233E50
  Compute  get=0x03235C30
  IsDelay  get=0x04D86F90
  IsActive  get=0x04D88E50  set=0x09EEA3B0
  IsFasterWrite  get=0x09EEA1D8
  MeshWriterShader  get=0x09EEA28C
METHODS:
  RVA=0x09EEA18C  token=0x6000189  System.Void SetGlobalTimeScale(System.Single timeScale)
  RVA=0x09EEA030  token=0x600018A  System.Single GetGlobalTimeScale()
  RVA=0x04D71DC0  token=0x600019A  System.Void Awake()
  RVA=0x04A378A0  token=0x600019B  System.Void InitSingleton()
  RVA=0x09EE9FC4  token=0x600019C  System.Void DuplicateDetection(MagicaCloth.MagicaPhysicsManager duplicate)
  RVA=0x04D84EB0  token=0x600019D  System.Void OnEnable()
  RVA=0x09EEA158  token=0x600019E  System.Void OnDisable()
  RVA=0x03FF1BE0  token=0x600019F  System.Void Update()
  RVA=0x03E626F0  token=0x60001A0  System.Void FixedUpdate()
  RVA=0x09EEA050  token=0x60001A1  System.Void OnDestroy()
  RVA=0x03233B20  token=0x60001A2  System.Void AfterEarlyUpdate()
  RVA=0x03235170  token=0x60001A3  System.Void AfterFixedUpdate()
  RVA=0x03233B60  token=0x60001A4  System.Void AfterUpdate()
  RVA=0x03233990  token=0x60001A5  System.Void BeforeLateUpdate()
  RVA=0x032339C0  token=0x60001A6  System.Void AfterLateUpdate()
  RVA=0x03235550  token=0x60001A7  System.Void DefaultUpdate()
  RVA=0x03233A50  token=0x60001A8  System.Void PostLateUpdate()
  RVA=0x032339F0  token=0x60001A9  System.Void AfterRendering()
  RVA=0x04D7D2E0  token=0x60001AA  System.Void Init()
  RVA=0x03FF1C10  token=0x60001AB  System.Void InitCustomGameLoop()
  RVA=0x03326620  token=0x60001AC  System.Void SetCustomGameLoop(UnityEngine.LowLevel.PlayerLoopSystem& playerLoop)
  RVA=0x03324CC0  token=0x60001AD  System.Boolean CheckRegist(UnityEngine.LowLevel.PlayerLoopSystem& playerLoop)
  RVA=0x046D73A0  token=0x60001AE  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.WindComponent
TYPE:  abstract class
TOKEN: 0x2000032
SIZE:  0x58
EXTENDS: MagicaCloth.BaseComponent
FIELDS:
  protected         System.Single                   main  // 0x18
  protected         System.Single                   turbulence  // 0x1c
  protected         System.Single                   frequency  // 0x20
  protected         UnityEngine.Vector3             areaSize  // 0x24
  protected         System.Single                   areaRadius  // 0x30
  protected         System.Single                   directionAngleX  // 0x34
  protected         System.Single                   directionAngleY  // 0x38
  protected         MagicaCloth.PhysicsManagerWindData.DirectionTypedirectionType  // 0x3c
  protected         MagicaCloth.BezierParam         attenuation  // 0x40
  protected         System.Int32                    windId  // 0x48
  protected         MagicaCloth.RuntimeStatus       status  // 0x50
PROPERTIES:
  Main  get=0x04D86380  set=0x09EEB830
  Turbulence  get=0x04D868F0  set=0x09EEB870
  Frequency  get=0x011EC8A0  set=0x09EEB750
  MainDirection  get=0x09EEB57C  set=0x09EEB77C
  DirectionAngleX  get=0x04D8E050  set=0x09EEB6E8
  DirectionAngleY  get=0x04D88360  set=0x09EEB70C
  AreaSize  get=0x04D905E0  set=0x09EEB634
  AreaRadius  get=0x04D86E20  set=0x09EEB600
  DirectionType  get=0x04D86640  set=0x09EEB730
  Status  get=0x04D86270
METHODS:
  RVA=0x09EEB3EC  token=0x60001D7  System.Void SetAttenuation(System.Single sval, System.Single eval, System.Boolean useEval, System.Single cval, System.Boolean useCval)
  RVA=0x09EEB3DC  token=0x60001D9  System.Void Reset()
  RVA=0x09EEB278  token=0x60001DA  System.Void OnValidate()
  RVA=0x09EEB0D8  token=0x60001DB  System.Void OnDidApplyAnimationProperties()
  RVA=0x09EEB434  token=0x60001DC  System.Void Start()
  RVA=0x09EEB18C  token=0x60001DD  System.Void OnEnable()
  RVA=0x09EEB108  token=0x60001DE  System.Void OnDisable()
  RVA=0x09EEB0AC  token=0x60001DF  System.Void OnDestroy()
  RVA=0x09EEB43C  token=0x60001E0  System.Void Update()
  RVA=0x09EEAF58  token=0x60001E1  System.Void Init()
  RVA=0x09EEB20C  token=0x60001E2  System.Void OnUpdateStatus()
  RVA=0x02FFF600  token=0x60001E3  System.Boolean VerifyData()
  RVA=0x09EEB1CC  token=0x60001E4  System.Void OnInit()
  RVA=0x09EEB140  token=0x60001E5  System.Void OnDispose()
  RVA=0x09EEB248  token=0x60001E6  System.Void OnUpdate()
  RVA=0x09EEB0A4  token=0x60001E7  System.Void OnActive()
  RVA=0x09EEB1C4  token=0x60001E8  System.Void OnInactive()
  RVA=0x09EEAE50  token=0x60001E9  System.Void EnableWind()
  RVA=0x09EEADA4  token=0x60001EA  System.Void DisableWind()
  RVA=0x09EEB354  token=0x60001EB  System.Void RemoveWind()
  RVA=0x09EEAB7C  token=0x60001EC  System.Void CreateWind()
  RVA=0x09EEAEE0  token=0x60001ED  UnityEngine.Vector3 GetLocalDirection()
  RVA=0x09EEA994  token=0x60001EE  System.Void ChangeParameter()
  RVA=-1  // abstract  token=0x60001EF  MagicaCloth.PhysicsManagerWindData.WindType GetWindType()
  RVA=-1  // abstract  token=0x60001F0  MagicaCloth.PhysicsManagerWindData.ShapeType GetShapeType()
  RVA=-1  // abstract  token=0x60001F1  MagicaCloth.PhysicsManagerWindData.DirectionType GetDirectionType()
  RVA=-1  // abstract  token=0x60001F2  System.Boolean IsAddition()
  RVA=-1  // abstract  token=0x60001F3  UnityEngine.Vector3 GetAreaSize()
  RVA=-1  // abstract  token=0x60001F4  System.Single GetAreaVolume()
  RVA=-1  // abstract  token=0x60001F5  System.Single GetAreaLength()
  RVA=-1  // abstract  token=0x60001F6  System.Void ResetParams()
  RVA=0x09EEB4A0  token=0x60001F7  System.Void .ctor()
  RVA=0x0464E630  token=0x60001F8  System.Object <Init>b__49_0()
END_CLASS

CLASS: MagicaCloth.MagicaAvatarAccess
TYPE:  abstract class
TOKEN: 0x2000033
SIZE:  0x18
IMPLEMENTS: System.IDisposable
FIELDS:
  protected         MagicaCloth.MagicaAvatar        owner  // 0x10
PROPERTIES:
  Runtime  get=0x09EE69FC
METHODS:
  RVA=0x053908C0  token=0x60001FA  System.Void SetParent(MagicaCloth.MagicaAvatar avatar)
  RVA=-1  // abstract  token=0x60001FB  System.Void Create()
  RVA=-1  // abstract  token=0x60001FC  System.Void Dispose()
  RVA=-1  // abstract  token=0x60001FD  System.Void Active()
  RVA=-1  // abstract  token=0x60001FE  System.Void Inactive()
  RVA=0x041E1670  token=0x60001FF  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.MagicaAvatarParts
TYPE:  class
TOKEN: 0x2000034
SIZE:  0x30
EXTENDS: MagicaCloth.BaseComponent
IMPLEMENTS: MagicaCloth.IDataVerify
FIELDS:
  private           MagicaCloth.MagicaAvatar        parentAvatar  // 0x18
  private           System.Collections.Generic.Dictionary<System.String,UnityEngine.Transform>boneDict  // 0x20
  private           System.Collections.Generic.List<MagicaCloth.CoreComponent>magicaComponentList  // 0x28
PROPERTIES:
  ParentAvatar  get=0x0385B100  set=0x05392C40
  HasParent  get=0x09EE74D4
  PartsId  get=0x09EE7524
METHODS:
  RVA=0x04DA3380  token=0x6000200  MagicaCloth.ComponentType GetComponentType()
  RVA=0x09EE73E4  token=0x6000205  System.Void OnDestroy()
  RVA=0x09EE6C34  token=0x6000206  System.Void Dispose()
  RVA=0x09EE6A1C  token=0x6000207  System.Collections.Generic.List<UnityEngine.Transform> CheckOverlappingTransform()
  RVA=0x09EE6CC0  token=0x6000208  System.Collections.Generic.Dictionary<System.String,UnityEngine.Transform> GetBoneDict()
  RVA=0x09EE7338  token=0x6000209  System.Collections.Generic.List<MagicaCloth.CoreComponent> GetMagicaComponentList()
  RVA=0x04D85B80  token=0x600020A  System.Int32 GetVersion()
  RVA=0x09EE6BE8  token=0x600020B  System.Void CreateVerifyData()
  RVA=0x09EE73EC  token=0x600020C  MagicaCloth.Define.Error VerifyData()
  RVA=0x09EE6E80  token=0x600020D  System.String GetInformation()
  RVA=0x09EE7454  token=0x600020E  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.MagicaAvatarRuntime
TYPE:  class
TOKEN: 0x2000035
SIZE:  0x38
EXTENDS: MagicaCloth.MagicaAvatarAccess
FIELDS:
  private           System.Collections.Generic.Dictionary<System.String,UnityEngine.Transform>boneDict  // 0x18
  private           System.Collections.Generic.Dictionary<UnityEngine.Transform,System.Int32>boneReferenceDict  // 0x20
  private           System.Collections.Generic.List<MagicaCloth.MagicaAvatarParts>avatarPartsList  // 0x28
  private           System.Collections.Generic.List<MagicaCloth.ColliderComponent>colliderList  // 0x30
PROPERTIES:
  AvatarPartsCount  get=0x09EE955C
METHODS:
  RVA=0x09EE8870  token=0x600020F  System.Void Create()
  RVA=0x041E1670  token=0x6000210  System.Void Dispose()
  RVA=0x041E1670  token=0x6000211  System.Void Active()
  RVA=0x041E1670  token=0x6000212  System.Void Inactive()
  RVA=0x09EE8890  token=0x6000214  MagicaCloth.MagicaAvatarParts GetAvatarParts(System.Int32 index)
  RVA=0x09EE8650  token=0x6000215  System.Void CreateBoneDict()
  RVA=0x09EE87F8  token=0x6000216  System.Void CreateColliderList()
  RVA=0x09EE88E4  token=0x6000217  System.Int32 GetColliderCount()
  RVA=0x09EE8474  token=0x6000218  System.Collections.Generic.List<UnityEngine.Transform> CheckOverlappingTransform()
  RVA=0x09EE752C  token=0x6000219  System.Int32 AddAvatarParts(MagicaCloth.MagicaAvatarParts parts)
  RVA=0x09EE81A8  token=0x600021A  System.Void AddBone(UnityEngine.Transform root, UnityEngine.Transform croot, UnityEngine.Transform bone)
  RVA=0x09EE9310  token=0x600021B  System.Void ReplaceSkinMeshRenderer(UnityEngine.SkinnedMeshRenderer skinRenderer, System.Collections.Generic.Dictionary<UnityEngine.Transform,UnityEngine.Transform> boneReplaceDict)
  RVA=0x09EE92BC  token=0x600021C  System.Void ReplaceMagicaComponent(MagicaCloth.CoreComponent comp, System.Collections.Generic.Dictionary<UnityEngine.Transform,UnityEngine.Transform> boneReplaceDict)
  RVA=0x09EE8950  token=0x600021D  System.Void RemoveAvatarParts(MagicaCloth.MagicaAvatarParts parts)
  RVA=0x09EE91E0  token=0x600021E  System.Void RemoveAvatarParts(System.Int32 partsId)
  RVA=0x09EE9404  token=0x600021F  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.BoneClothTarget
TYPE:  class
TOKEN: 0x2000037
SIZE:  0x28
IMPLEMENTS: MagicaCloth.IDataHash MagicaCloth.IBoneReplace
FIELDS:
  private           System.Collections.Generic.List<UnityEngine.Transform>rootList  // 0x10
  private           MagicaCloth.BoneClothTarget.ConnectionModeconnection  // 0x18
  private           System.Single                   sameSurfaceAngle  // 0x1c
  private           System.Int32[]                  parentIndexList  // 0x20
PROPERTIES:
  RootCount  get=0x09ED893C
  Connection  get=0x011EF5B0
  SameSurfaceAngle  get=0x04D868F0
  IsMeshConnection  get=0x09ED8924
METHODS:
  RVA=0x09ED85F8  token=0x6000222  System.Int32 GetDataHash()
  RVA=0x09ED8688  token=0x6000224  UnityEngine.Transform GetRoot(System.Int32 index)
  RVA=0x09ED8634  token=0x6000225  System.Int32 GetRootIndex(UnityEngine.Transform root)
  RVA=0x09ED81D4  token=0x6000226  System.Void AddParentTransform()
  RVA=0x09ED8768  token=0x6000227  System.Void RemoveParentTransform()
  RVA=0x09ED881C  token=0x6000228  System.Void ResetFuturePredictionParentTransform()
  RVA=0x09ED8538  token=0x6000229  System.Void ChangeUnityPhysicsCount(System.Boolean sw)
  RVA=-1  // generic def  token=0x600022D  System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict)
  RVA=0x09ED86F4  token=0x600022E  System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones()
  RVA=0x09ED88CC  token=0x600022F  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.ClothData
TYPE:  class
TOKEN: 0x2000039
SIZE:  0x1A8
EXTENDS: MagicaCloth.ShareDataObject
FIELDS:
  private   static  System.Int32                    DATA_VERSION  // const
  public    static  System.UInt32                   VertexFlag_End  // const
  public    static  System.UInt32                   VertexFlag_TriangleRotation  // const
  public            MagicaCloth.ClothParams.AlgorithmtriangleBendAlgorithm  // 0x20
  public            MagicaCloth.ClothParams.AlgorithmrestoreRotationAlgorithm  // 0x24
  public            MagicaCloth.ClothParams.AlgorithmclampRotationAlgorithm  // 0x28
  public            System.Collections.Generic.List<System.Int32>useVertexList  // 0x30
  public            System.Collections.Generic.List<System.Int32>selectionData  // 0x38
  public            System.UInt32                   maxLevel  // 0x40
  public            System.Collections.Generic.List<System.UInt32>vertexFlagLevelList  // 0x48
  public            System.Collections.Generic.List<System.Single>vertexDepthList  // 0x50
  public            System.Collections.Generic.List<System.Int32>rootList  // 0x58
  public            System.Collections.Generic.List<System.Int32>parentList  // 0x60
  public            MagicaCloth.RestoreDistanceConstraint.RestoreDistanceData[]structDistanceDataList  // 0x68
  public            MagicaCloth.ReferenceDataIndex[]structDistanceReferenceList  // 0x70
  public            MagicaCloth.RestoreDistanceConstraint.RestoreDistanceData[]bendDistanceDataList  // 0x78
  public            MagicaCloth.ReferenceDataIndex[]bendDistanceReferenceList  // 0x80
  public            MagicaCloth.RestoreDistanceConstraint.RestoreDistanceData[]nearDistanceDataList  // 0x88
  public            MagicaCloth.ReferenceDataIndex[]nearDistanceReferenceList  // 0x90
  public            MagicaCloth.ClampDistanceConstraint.ClampDistanceData[]rootDistanceDataList  // 0x98
  public            MagicaCloth.ReferenceDataIndex[]rootDistanceReferenceList  // 0xa0
  public            MagicaCloth.ClampDistance2Constraint.ClampDistance2Data[]clampDistance2DataList  // 0xa8
  public            MagicaCloth.ClampDistance2Constraint.ClampDistance2RootInfo[]clampDistance2RootInfoList  // 0xb0
  public            MagicaCloth.RestoreRotationConstraint.RotationData[]restoreRotationDataList  // 0xb8
  public            MagicaCloth.ReferenceDataIndex[]restoreRotationReferenceList  // 0xc0
  public            MagicaCloth.ClampRotationConstraint.ClampRotationData[]clampRotationDataList  // 0xc8
  public            MagicaCloth.ClampRotationConstraint.ClampRotationRootInfo[]clampRotationRootInfoList  // 0xd0
  public            MagicaCloth.CompositeRotationConstraint.RotationData[]compositeRotationDataList  // 0xd8
  public            MagicaCloth.CompositeRotationConstraint.RootInfo[]compositeRotationRootInfoList  // 0xe0
  public            MagicaCloth.TwistConstraint.TwistData[]twistDataList  // 0xe8
  public            MagicaCloth.ReferenceDataIndex[]twistReferenceList  // 0xf0
  public            MagicaCloth.AdjustRotationWorker.AdjustRotationData[]adjustRotationDataList  // 0xf8
  public            MagicaCloth.TriangleBendConstraint.TriangleBendData[]triangleBendDataList  // 0x100
  public            MagicaCloth.ReferenceDataIndex[]triangleBendReferenceList  // 0x108
  public            System.Int32                    triangleBendWriteBufferCount  // 0x110
  public            MagicaCloth.VolumeConstraint.VolumeData[]volumeDataList  // 0x118
  public            MagicaCloth.ReferenceDataIndex[]volumeReferenceList  // 0x120
  public            System.Int32                    volumeWriteBufferCount  // 0x128
  public            MagicaCloth.LineWorker.LineRotationData[]lineRotationDataList  // 0x130
  public            MagicaCloth.LineWorker.LineRotationRootInfo[]lineRotationRootInfoList  // 0x138
  public            MagicaCloth.TriangleWorker.TriangleRotationData[]triangleRotationDataList  // 0x140
  public            System.Int32[]                  triangleRotationIndexList  // 0x148
  public            MagicaCloth.EdgeCollisionConstraint.EdgeCollisionData[]edgeCollisionDataList  // 0x150
  public            MagicaCloth.ReferenceDataIndex[]edgeCollisionReferenceList  // 0x158
  public            System.Int32                    edgeCollisionWriteBufferCount  // 0x160
  public            MagicaCloth.PenetrationConstraint.PenetrationData[]penetrationDataList  // 0x168
  public            MagicaCloth.ReferenceDataIndex[]penetrationReferenceList  // 0x170
  public            Unity.Mathematics.float3[]      penetrationDirectionDataList  // 0x178
  public            MagicaCloth.ClothParams.PenetrationModepenetrationMode  // 0x180
  public            MagicaCloth.BaseSkinningWorker.BaseSkinningData[]baseSkinningDataList  // 0x188
  public            Unity.Mathematics.float4x4[]    baseSkinningBindPoseList  // 0x190
  public            UnityEngine.Vector3             initScale  // 0x198
PROPERTIES:
  VertexUseCount  get=0x09EE45A0
  StructDistanceConstraintCount  get=0x09EE4544
  BendDistanceConstraintCount  get=0x09EE4420
  NearDistanceConstraintCount  get=0x09EE44DC
  ClampDistanceConstraintCount  get=0x09EE444C
  ClampDistance2ConstraintCount  get=0x09EE4434
  RestoreRotationConstraintCount  get=0x09EE452C
  ClampRotationConstraintDataCount  get=0x09EE4464
  ClampRotationConstraintRootCount  get=0x09EE447C
  AdjustRotationConstraintCount  get=0x09EE43F0
  CompositeRotationCount  get=0x09EE4494
  TwistConstraintCount  get=0x09EE4588
  TriangleBendConstraintCount  get=0x09EE4558
  EdgeCollisionConstraintCount  get=0x09EE44AC
  VolumeConstraintCount  get=0x09EE45BC
  LineRotationWorkerCount  get=0x09EE44C4
  TriangleRotationWorkerCount  get=0x09EE4570
  PenetrationCount  get=0x09EE44F4
  BaseSkinningCount  get=0x09EE4408
METHODS:
  RVA=0x09EE1C58  token=0x6000230  System.Int32 GetDataHash()
  RVA=0x09EE1C1C  token=0x6000244  System.Int32 GetClampRotationCount()
  RVA=0x09EE2278  token=0x6000245  System.Int32 GetRestoreRotationCount()
  RVA=0x09EE39B4  token=0x6000246  System.Boolean IsInvalidVertex(System.Int32 vindex)
  RVA=0x09EE3958  token=0x6000247  System.Boolean IsFixedVertex(System.Int32 vindex)
  RVA=0x09EE3A1C  token=0x6000248  System.Boolean IsMoveVertex(System.Int32 vindex)
  RVA=0x09EE392C  token=0x6000249  System.Boolean IsExtendVertex(System.Int32 vindex)
  RVA=0x09EE3A0C  token=0x600024A  System.Boolean IsLastLevel(System.Int32 vindex)
  RVA=0x09EE3984  token=0x600024B  System.Boolean IsFlag(System.Int32 vindex, System.UInt32 flag)
  RVA=0x09EE3FEC  token=0x600024C  System.Void SetFlag(System.Int32 vindex, System.UInt32 flag)
  RVA=0x09EE2108  token=0x600024D  System.Int32 GetLevel(System.Int32 vindex)
  RVA=0x04D9EC50  token=0x600024E  System.Int32 GetVersion()
  RVA=0x09EE41F0  token=0x600024F  MagicaCloth.Define.Error VerifyData()
  RVA=0x09EE12FC  token=0x6000250  System.Void CreateData(MagicaCloth.PhysicsTeam team, MagicaCloth.ClothParams clothParams, MagicaCloth.PhysicsTeamData teamData, MagicaCloth.MeshData meshData, MagicaCloth.IEditorMesh editMesh, System.Collections.Generic.List<System.Int32> selData, System.Action<System.Collections.Generic.List<System.Int32>,System.Collections.Generic.List<System.Int32>,System.Collections.Generic.List<UnityEngine.Vector3>,System.Collections.Generic.List<UnityEngine.Vector3>,System.Collections.Generic.List<UnityEngine.Vector3>,System.Collections.Generic.List<System.Int32>,System.Collections.Generic.List<System.Int32>> extensionAction)
  RVA=0x09EE1544  token=0x6000251  System.Void CreateVertexData(System.Int32 vertexCount, System.Collections.Generic.List<System.Int32> lineList, System.Collections.Generic.List<System.Int32> triangleList)
  RVA=0x09ED9088  token=0x6000252  System.Void CreateConstraintData(MagicaCloth.PhysicsTeam team, MagicaCloth.ClothParams clothParams, MagicaCloth.PhysicsTeamData teamData, System.Int32 vertexCount, System.Collections.Generic.List<UnityEngine.Vector3> wposList, System.Collections.Generic.List<UnityEngine.Vector3> wnorList, System.Collections.Generic.List<UnityEngine.Vector3> wtanList, System.Collections.Generic.List<System.Int32> lineList, System.Collections.Generic.List<System.Int32> triangleList)
  RVA=0x09EE3A78  token=0x6000253  System.Void RegistTriangleBend(System.Int32 v0, System.Int32 v1, System.Int32 v2, System.Int32 v3, System.Collections.Generic.List<UnityEngine.Vector3> wposList, MagicaCloth.ClothParams clothParams, System.Collections.Generic.List<MagicaCloth.TriangleBendConstraint.TriangleBendData> triangleBendData)
  RVA=0x09EE4070  token=0x6000254  System.Collections.Generic.List<System.Int32> SortTetra(System.Int32 v0, System.Int32 v1, System.Int32 v2, System.Int32 v3, System.Collections.Generic.List<System.Single> meshVertexDepthList)
  RVA=0x09ED8C44  token=0x6000255  System.Collections.Generic.List<System.Int32> CheckTetraDirection(System.Int32 v0, System.Int32 v1, System.Int32 v2, System.Int32 v3, System.Collections.Generic.HashSet<System.UInt64> trianglePackSet, System.Collections.Generic.List<System.Single> meshVertexDepthList)
  RVA=0x09ED897C  token=0x6000256  System.Boolean CalcTriangleBendRestAngle(UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, UnityEngine.Vector3 p2, UnityEngine.Vector3 p3, System.Single& restAngle)
  RVA=0x09EE2160  token=0x6000257  System.Collections.Generic.List<System.Single> GetMeshVertexDepthList(System.Int32 vertexCount, System.Collections.Generic.List<System.Single> depthList)
  RVA=0x09EE22B4  token=0x6000258  System.Collections.Generic.List<System.Int32> GetUseParentVertexList(System.Int32 vertexCount, System.Collections.Generic.List<System.Collections.Generic.HashSet<System.Int32>> vlink, System.Collections.Generic.List<UnityEngine.Vector3> wposList, System.Collections.Generic.List<System.Single> depthList)
  RVA=0x09EE364C  token=0x6000259  System.Collections.Generic.List<System.Int32> GetUseRootVertexList(System.Collections.Generic.List<System.Int32> parentVertexList)
  RVA=0x09EE3748  token=0x600025A  System.Collections.Generic.List<MagicaCloth.ClothData.VertexInfo> GetUseVertexInfoList(System.Collections.Generic.List<System.Int32> parentVertexList)
  RVA=0x09EE314C  token=0x600025B  System.Collections.Generic.List<System.Collections.Generic.List<System.Int32>> GetUseRootLineList(System.Collections.Generic.List<System.Int32> parentVertexList)
  RVA=0x09EE4298  token=0x600025C  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.ClothParams
TYPE:  class
TOKEN: 0x2000042
SIZE:  0x228
FIELDS:
  private           MagicaCloth.ClothParams.Algorithmalgorithm  // 0x10
  private           MagicaCloth.BezierParam         radius  // 0x18
  private           MagicaCloth.BezierParam         mass  // 0x20
  private           System.Boolean                  useGravity  // 0x28
  private           MagicaCloth.BezierParam         gravity  // 0x30
  private           UnityEngine.Vector3             gravityDirection  // 0x38
  private           System.Boolean                  useDrag  // 0x44
  private           MagicaCloth.BezierParam         drag  // 0x48
  private           System.Boolean                  useMaxVelocity  // 0x50
  private           MagicaCloth.BezierParam         maxVelocity  // 0x58
  private           UnityEngine.Transform           influenceTarget  // 0x60
  private           System.Single                   maxMoveSpeed  // 0x68
  private           System.Single                   maxRotationSpeed  // 0x6c
  private           MagicaCloth.BezierParam         worldMoveInfluence  // 0x70
  private           MagicaCloth.BezierParam         worldRotationInfluence  // 0x78
  private           System.Single                   massInfluence  // 0x80
  private           MagicaCloth.BezierParam         depthInfluence  // 0x88
  private           System.Single                   windInfluence  // 0x90
  private           System.Single                   windRandomScale  // 0x94
  private           System.Single                   windSynchronization  // 0x98
  private           System.Boolean                  useDistanceDisable  // 0x9c
  private           UnityEngine.Transform           disableReferenceObject  // 0xa0
  private           System.Single                   disableDistance  // 0xa8
  private           System.Single                   disableFadeDistance  // 0xac
  private           System.Boolean                  useResetTeleport  // 0xb0
  private           System.Single                   teleportDistance  // 0xb4
  private           System.Single                   teleportRotation  // 0xb8
  private           MagicaCloth.ClothParams.TeleportModeteleportMode  // 0xbc
  private           System.Single                   resetStabilizationTime  // 0xc0
  private           System.Boolean                  useClampDistanceRatio  // 0xc4
  private           System.Single                   clampDistanceMinRatio  // 0xc8
  private           System.Single                   clampDistanceMaxRatio  // 0xcc
  private           System.Single                   clampDistanceVelocityInfluence  // 0xd0
  private           System.Boolean                  useClampPositionLength  // 0xd4
  private           MagicaCloth.BezierParam         clampPositionLength  // 0xd8
  private           System.Single                   clampPositionRatioX  // 0xe0
  private           System.Single                   clampPositionRatioY  // 0xe4
  private           System.Single                   clampPositionRatioZ  // 0xe8
  private           System.Single                   clampPositionVelocityInfluence  // 0xec
  private           System.Boolean                  useClampRotation  // 0xf0
  private           MagicaCloth.BezierParam         clampRotationAngle  // 0xf8
  private           MagicaCloth.BezierParam         clampRotationAngle2  // 0x100
  private           System.Single                   clampRotationVelocityLimit  // 0x108
  private           System.Single                   clampRotationVelocityInfluence  // 0x10c
  private           System.Single                   restoreDistanceVelocityInfluence  // 0x110
  private           MagicaCloth.BezierParam         structDistanceStiffness  // 0x118
  private           System.Boolean                  useBendDistance  // 0x120
  private           System.Int32                    bendDistanceMaxCount  // 0x124
  private           MagicaCloth.BezierParam         bendDistanceStiffness  // 0x128
  private           System.Boolean                  useNearDistance  // 0x130
  private           System.Int32                    nearDistanceMaxCount  // 0x134
  private           System.Single                   nearDistanceMaxDepth  // 0x138
  private           MagicaCloth.BezierParam         nearDistanceLength  // 0x140
  private           MagicaCloth.BezierParam         nearDistanceStiffness  // 0x148
  private           System.Boolean                  useRestoreRotation  // 0x150
  private           MagicaCloth.BezierParam         restoreRotation  // 0x158
  private           MagicaCloth.BezierParam         restoreRotation2  // 0x160
  private           System.Single                   restoreRotationVelocityInfluence  // 0x168
  private           System.Single                   restoreRotationVelocityInfluence2  // 0x16c
  private           System.Boolean                  useSpring  // 0x170
  private           System.Single                   springPower  // 0x174
  private           System.Single                   springRadius  // 0x178
  private           System.Single                   springScaleX  // 0x17c
  private           System.Single                   springScaleY  // 0x180
  private           System.Single                   springScaleZ  // 0x184
  private           System.Single                   springIntensity  // 0x188
  private           MagicaCloth.BezierParam         springDirectionAtten  // 0x190
  private           MagicaCloth.BezierParam         springDistanceAtten  // 0x198
  private           MagicaCloth.ClothParams.AdjustModeadjustMode  // 0x1a0
  private           System.Single                   adjustRotationPower  // 0x1a4
  private           System.Boolean                  useTriangleBend  // 0x1a8
  private           MagicaCloth.BezierParam         triangleBend  // 0x1b0
  private           MagicaCloth.BezierParam         triangleBend2  // 0x1b8
  private           System.Boolean                  useTwistCorrection  // 0x1c0
  private           System.Single                   twistRecoveryPower  // 0x1c4
  private           System.Boolean                  useVolume  // 0x1c8
  private           System.Single                   maxVolumeLength  // 0x1cc
  private           MagicaCloth.BezierParam         volumeStretchStiffness  // 0x1d0
  private           MagicaCloth.BezierParam         volumeShearStiffness  // 0x1d8
  private           System.Boolean                  useCollision  // 0x1e0
  private           System.Single                   friction  // 0x1e4
  private           System.Single                   staticFriction  // 0x1e8
  private           System.Boolean                  usePenetration  // 0x1ec
  private           MagicaCloth.ClothParams.PenetrationModepenetrationMode  // 0x1f0
  private           MagicaCloth.ClothParams.PenetrationAxispenetrationAxis  // 0x1f4
  private           System.Single                   penetrationMaxDepth  // 0x1f8
  private           MagicaCloth.BezierParam         penetrationConnectDistance  // 0x200
  private           MagicaCloth.BezierParam         penetrationDistance  // 0x208
  private           MagicaCloth.BezierParam         penetrationRadius  // 0x210
  private           System.Boolean                  useLineAvarageRotation  // 0x218
  private           System.Boolean                  useFixedNonRotation  // 0x219
  private           System.Collections.Generic.HashSet<MagicaCloth.ClothParams.ParamType>changeSet  // 0x220
PROPERTIES:
  AlgorithmType  get=0x02B2E2D0  set=0x014F51F0
  UseGravity  get=0x011F33C0
  GravityDirection  get=0x04D9E700  set=0x04D9DB00
  UseDrag  get=0x04D86A30
  UseMaxVelocity  get=0x04D86800
  MassInfluence  get=0x04D88E60  set=0x04D8E240
  WindInfluence  get=0x04D8FD30  set=0x04D8FD60
  WindRandomScale  get=0x04D8F070  set=0x04D8F0A0
  WindSynchronization  get=0x04D8E570  set=0x04D8E590
  MaxMoveSpeed  get=0x04D8F060  set=0x04D8F090
  MaxRotationSpeed  get=0x04DA3230  set=0x04DA3330
  UseResetTeleport  get=0x04D88CC0  set=0x04D88D20
  TeleportDistance  get=0x04DA32B0  set=0x04DA3350
  TeleportRotation  get=0x04DA32D0  set=0x04D87E30
  TeleportResetMode  get=0x04DA32C0  set=0x04DA3360
  ResetStabilizationTime  get=0x04D9DF30  set=0x04D9DFF0
  UseDistanceDisable  get=0x04D92ED0  set=0x04D92F60
  DisableReferenceObject  get=0x04D86210  set=0x05391334
  DisableDistance  get=0x04D8E650  set=0x04D8E680
  DisableFadeDistance  get=0x04D8E660  set=0x04D8E690
  UseClampDistanceRatio  get=0x04DA32F0
  ClampDistanceMinRatio  get=0x09EE5BEC
  ClampDistanceMaxRatio  get=0x09EE5BD4
  ClampDistanceVelocityInfluence  get=0x09EE5C04
  UseClampPositionLength  get=0x04DA2990
  ClampPositionAxisRatio  get=0x04DA3210
  ClampPositionVelocityInfluence  get=0x09EE5C20
  UseClampRotation  get=0x04D8E520
  ClampRotationVelocityInfluence  get=0x09EE5C3C
  RestoreDistanceVelocityInfluence  get=0x04D8E170
  UseBendDistance  get=0x04D8FE70
  BendDistanceMaxCount  get=0x04D90A80
  UseNearDistance  get=0x04D92620
  NearDistanceMaxCount  get=0x04DA3240
  NearDistanceMaxDepth  get=0x04DA3250
  UseRestoreRotation  get=0x04DA2F60
  UseSpring  get=0x04D905A0  set=0x04D905B0
  SpringPowr  get=0x04DA3270  set=0x04DA3340
  SpringRadius  get=0x04D902D0
  SpringRadiusScale  get=0x04DA3280
  SpringIntensity  get=0x04DA3260
  AdjustRotationMode  get=0x04DA3200
  AdjustRotationVector  get=0x09EE5B80
  UseTriangleBend  get=0x04DA3310
  TwistRecoveryPower  get=0x04DA32E0
  UseVolume  get=0x04DA3320
  UseCollision  get=0x04D91ED0
  DynamicFriction  get=0x04D90D10
  StaticFriction  get=0x04DA32A0
  UsePenetration  get=0x04D90680  set=0x04D90690
  PenetrationMaxDepth  get=0x04D911D0
  UseLineAvarageRotation  get=0x04D8EDA0
  UseFixedNonRotation  get=0x04DA3300
METHODS:
  RVA=0x09EE4F08  token=0x600026C  System.Void SetChangeParam(MagicaCloth.ClothParams.ParamType ptype)
  RVA=0x09EE45D4  token=0x600026D  System.Boolean ChangedParam(MagicaCloth.ClothParams.ParamType ptype)
  RVA=0x09EE462C  token=0x600026E  System.Void ClearChangeParam()
  RVA=0x09EE4820  token=0x600026F  System.Int32 GetParamHash(MagicaCloth.BaseCloth cloth, MagicaCloth.ClothParams.ParamType ptype)
  RVA=0x09EE514C  token=0x6000272  System.Void SetRadius(System.Single sval, System.Single eval)
  RVA=0x09EE4E30  token=0x6000273  System.Single GetRadius(System.Single depth)
  RVA=0x0385B100  token=0x6000274  MagicaCloth.BezierParam GetRadius()
  RVA=0x09EE50DC  token=0x6000275  System.Void SetMass(System.Single sval, System.Single eval, System.Boolean useEval, System.Single cval, System.Boolean useCval)
  RVA=0x04D862C0  token=0x6000276  MagicaCloth.BezierParam GetMass()
  RVA=0x09EE50A8  token=0x6000277  System.Void SetGravity(System.Boolean sw, System.Single sval, System.Single eval)
  RVA=0x09EE4784  token=0x6000279  MagicaCloth.BezierParam GetGravity()
  RVA=0x09EE5074  token=0x600027C  System.Void SetDrag(System.Boolean sw, System.Single sval, System.Single eval)
  RVA=0x09EE4744  token=0x600027E  MagicaCloth.BezierParam GetDrag()
  RVA=0x09EE5118  token=0x600027F  System.Void SetMaxVelocity(System.Boolean sw, System.Single sval, System.Single eval)
  RVA=0x09EE47C4  token=0x6000281  MagicaCloth.BezierParam GetMaxVelocity()
  RVA=0x04DA3150  token=0x6000282  System.Void SetExternalForce(System.Single massInfluence, System.Single windInfluence, System.Single windRandomScale, System.Single windSynchronization)
  RVA=0x04D861F0  token=0x6000285  MagicaCloth.BezierParam GetDepthInfluence()
  RVA=0x09EE5350  token=0x600028C  System.Void SetWorldInfluence(System.Single maxspeed, System.Single moveval, System.Single rotval)
  RVA=0x04D862B0  token=0x600028D  MagicaCloth.BezierParam GetWorldMoveInfluence()
  RVA=0x04D862A0  token=0x600028E  MagicaCloth.BezierParam GetWorldRotationInfluence()
  RVA=0x04D86280  token=0x600028F  UnityEngine.Transform GetInfluenceTarget()
  RVA=0x063CC164  token=0x6000290  System.Void SetInfluenceTarget(UnityEngine.Transform t)
  RVA=0x04DA31D0  token=0x6000295  System.Void SetTeleport(System.Boolean sw, System.Single distance, System.Single rotation, MagicaCloth.ClothParams.TeleportMode mode)
  RVA=0x09EE5038  token=0x60002A0  System.Void SetDistanceDisable(System.Boolean sw, System.Single distance, System.Single fadeDistance, UnityEngine.Transform referenceObject)
  RVA=0x04DA3100  token=0x60002A9  System.Void SetClampDistanceRatio(System.Boolean sw, System.Single minval, System.Single maxval, System.Single influence)
  RVA=0x09EE4F60  token=0x60002AE  System.Void SetClampPositionLength(System.Boolean sw, System.Single sval, System.Single eval, System.Single ratioX, System.Single ratioY, System.Single ratioZ, System.Single influence)
  RVA=0x04D88EF0  token=0x60002B1  MagicaCloth.BezierParam GetClampPositionLength()
  RVA=0x09EE4FD0  token=0x60002B3  System.Void SetClampRotationAngle(System.Boolean sw, System.Single sval, System.Single eval, System.Single influence)
  RVA=0x09EE4708  token=0x60002B5  MagicaCloth.BezierParam GetClampRotationAngle(MagicaCloth.ClothParams.Algorithm algo)
  RVA=0x09EE4720  token=0x60002B7  System.Single GetClampRotationVelocityLimit(MagicaCloth.ClothParams.Algorithm algo)
  RVA=0x09EE5180  token=0x60002B8  System.Void SetRestoreDistance(System.Single influence, System.Single structStiffness)
  RVA=0x04D85F80  token=0x60002BA  MagicaCloth.BezierParam GetStructDistanceStiffness()
  RVA=0x04D8D910  token=0x60002BD  MagicaCloth.BezierParam GetBendDistanceStiffness()
  RVA=0x04D8DA60  token=0x60002C1  MagicaCloth.BezierParam GetNearDistanceLength()
  RVA=0x04D8DAC0  token=0x60002C2  MagicaCloth.BezierParam GetNearDistanceStiffness()
  RVA=0x09EE51BC  token=0x60002C3  System.Void SetRestoreRotation(System.Boolean sw, System.Single sval, System.Single eval, System.Single influence)
  RVA=0x09EE4E50  token=0x60002C5  MagicaCloth.BezierParam GetRestoreRotationPower(MagicaCloth.ClothParams.Algorithm algo)
  RVA=0x09EE4E68  token=0x60002C6  System.Single GetRestoreRotationVelocityInfluence(MagicaCloth.ClothParams.Algorithm algo)
  RVA=0x04DA3180  token=0x60002C7  System.Void SetSpring(System.Boolean sw, System.Single power, System.Single r, System.Single sclx, System.Single scly, System.Single sclz, System.Single intensity)
  RVA=0x09EE522C  token=0x60002C8  System.Void SetSpringDirectionAtten(System.Single sval, System.Single eval, System.Single cval)
  RVA=0x09EE5260  token=0x60002C9  System.Void SetSpringDistanceAtten(System.Single sval, System.Single eval, System.Single cval)
  RVA=0x09EE4EC8  token=0x60002CC  System.Single GetSpringPower()
  RVA=0x09EE4E80  token=0x60002D2  System.Single GetSpringDirectionAtten(System.Single ratio)
  RVA=0x09EE4EA4  token=0x60002D3  System.Single GetSpringDistanceAtten(System.Single ratio)
  RVA=0x04DA30F0  token=0x60002D4  System.Void SetAdjustRotation(MagicaCloth.ClothParams.AdjustMode amode, System.Single power)
  RVA=0x09EE5294  token=0x60002D7  System.Void SetTriangleBend(System.Boolean sw, System.Single sval, System.Single eval)
  RVA=0x09EE4EE0  token=0x60002D9  MagicaCloth.BezierParam GetTriangleBendStiffness(MagicaCloth.ClothParams.Algorithm algo)
  RVA=0x09EE4EF8  token=0x60002DA  System.Boolean GetUseTwistCorrection(MagicaCloth.ClothParams.Algorithm algo)
  RVA=0x09EE52EC  token=0x60002DC  System.Void SetVolume(System.Boolean sw, System.Single maxLength, System.Single stiffness, System.Single shear)
  RVA=0x09EE4808  token=0x60002DE  System.Single GetMaxVolumeLength()
  RVA=0x04D8D7B0  token=0x60002DF  MagicaCloth.BezierParam GetVolumeStretchStiffness()
  RVA=0x04D87870  token=0x60002E0  MagicaCloth.BezierParam GetVolumeShearStiffness()
  RVA=0x04DA3130  token=0x60002E1  System.Void SetCollision(System.Boolean sw, System.Single dynamicFriction, System.Single staticFriction)
  RVA=0x04DA23C0  token=0x60002E7  MagicaCloth.ClothParams.PenetrationMode GetPenetrationMode()
  RVA=0x04DA30E0  token=0x60002E8  MagicaCloth.ClothParams.PenetrationAxis GetPenetrationAxis()
  RVA=0x04D8DA40  token=0x60002EA  MagicaCloth.BezierParam GetPenetrationConnectDistance()
  RVA=0x04D8D8B0  token=0x60002EB  MagicaCloth.BezierParam GetPenetrationRadius()
  RVA=0x04D8D8C0  token=0x60002EC  MagicaCloth.BezierParam GetPenetrationDistance()
  RVA=0x09EE4674  token=0x60002EF  System.Void ConvertToLatestAlgorithmParameter()
  RVA=0x09EE53A0  token=0x60002F0  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.ClothSetup
TYPE:  class
TOKEN: 0x2000049
SIZE:  0x18
FIELDS:
  private           System.Int32                    teamBoneIndex  // 0x10
  private           System.Single                   distanceBlendRatio  // 0x14
PROPERTIES:
  DistanceBlendRatio  get=0x04D88D40  set=0x04D88D50
METHODS:
  RVA=0x09EECE30  token=0x60002F1  System.Void ClothInit(MagicaCloth.PhysicsTeam team, MagicaCloth.MeshData meshData, MagicaCloth.ClothData clothData, MagicaCloth.ClothParams param, System.Func<System.Int32,System.UInt32> funcUserFlag)
  RVA=0x09EECCE4  token=0x60002F2  System.Void ClothDispose(MagicaCloth.PhysicsTeam team)
  RVA=0x09EECBBC  token=0x60002F3  System.Void ClothActive(MagicaCloth.PhysicsTeam team, MagicaCloth.ClothParams param, MagicaCloth.ClothData clothData)
  RVA=0x09EECD48  token=0x60002F4  System.Void ClothInactive(MagicaCloth.PhysicsTeam team)
  RVA=-1  // generic def  token=0x60002F5  System.Void ReplaceBone(MagicaCloth.PhysicsTeam team, MagicaCloth.ClothParams param, System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict)
  RVA=0x09EEED7C  token=0x60002F6  System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones(MagicaCloth.PhysicsTeam team, MagicaCloth.ClothParams param)
  RVA=0x09EECB50  token=0x60002F7  System.Void ChangeUseUnityPhysics(System.Boolean sw)
  RVA=0x09EEBF38  token=0x60002FA  System.Void ChangeData(MagicaCloth.PhysicsTeam team, MagicaCloth.ClothParams param, MagicaCloth.ClothData clothData)
  RVA=0x04DA33F0  token=0x60002FB  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.MagicaBoneCloth
TYPE:  class
TOKEN: 0x200004B
SIZE:  0x100
EXTENDS: MagicaCloth.BaseCloth
FIELDS:
  private   static  System.Int32                    DATA_VERSION  // const
  private   static  System.Int32                    ERR_DATA_VERSION  // const
  private           MagicaCloth.MeshData            meshData  // 0xc8
  private           System.Int32                    meshDataHash  // 0xd0
  private           System.Int32                    meshDataVersion  // 0xd4
  private           MagicaCloth.BoneClothTarget     clothTarget  // 0xd8
  private           System.Collections.Generic.List<UnityEngine.Transform>useTransformList  // 0xe0
  private           System.Collections.Generic.List<UnityEngine.Vector3>useTransformPositionList  // 0xe8
  private           System.Collections.Generic.List<UnityEngine.Quaternion>useTransformRotationList  // 0xf0
  private           System.Collections.Generic.List<UnityEngine.Vector3>useTransformScaleList  // 0xf8
PROPERTIES:
  ClothTarget  get=0x04D88EF0
  MeshData  get=0x04D8D1D0
  UseTransformCount  get=0x09EF1B1C
METHODS:
  RVA=0x04DA3400  token=0x6000300  MagicaCloth.ComponentType GetComponentType()
  RVA=0x09EEF674  token=0x6000301  System.Int32 GetDataHash()
  RVA=0x09EF162C  token=0x6000304  System.Void Reset()
  RVA=0x09EF12C0  token=0x6000305  System.Void OnValidate()
  RVA=0x09EEF4B8  token=0x6000306  System.Void ClothInit()
  RVA=0x09EEF494  token=0x6000307  System.Void ClothDispose()
  RVA=0x09EEF48C  token=0x6000308  System.Void ClothActive()
  RVA=0x09EF1634  token=0x6000309  System.UInt32 UserFlag(System.Int32 index)
  RVA=0x09EF1798  token=0x600030A  UnityEngine.Transform UserTransform(System.Int32 index)
  RVA=0x09EF1668  token=0x600030B  Unity.Mathematics.float3 UserTransformLocalPosition(System.Int32 vindex)
  RVA=0x09EF1708  token=0x600030C  Unity.Mathematics.quaternion UserTransformLocalRotation(System.Int32 vindex)
  RVA=0x011EC580  token=0x600030D  MagicaCloth.BaseMeshDeformer GetDeformer()
  RVA=0x04D8D1D0  token=0x600030E  MagicaCloth.MeshData GetMeshData()
  RVA=0x041E1670  token=0x600030F  System.Void WorkerInit()
  RVA=0x012081B0  token=0x6000310  System.Boolean IsRequiresDeformer()
  RVA=0x041E1670  token=0x6000311  System.Void SetDeformerUseVertex(System.Boolean sw, MagicaCloth.BaseMeshDeformer deformer)
  RVA=0x09EEF444  token=0x6000312  System.Void ChangeUseUnityPhysics(System.Boolean sw)
  RVA=0x09EF11E4  token=0x6000313  System.Void OnChangeCalculation()
  RVA=0x09EF0E08  token=0x6000314  System.Collections.Generic.List<UnityEngine.Transform> GetTransformList()
  RVA=0x09EF1108  token=0x6000315  UnityEngine.Transform GetUseTransform(System.Int32 index)
  RVA=0x09EF0A64  token=0x6000317  System.Collections.Generic.List<System.Collections.Generic.List<System.Collections.Generic.List<UnityEngine.Transform>>> GetTransformGrid(System.Int32& maxLevel)
  RVA=0x04D9BE00  token=0x6000318  System.Int32 GetVersion()
  RVA=0x04D8C9E0  token=0x6000319  System.Int32 GetErrorVersion()
  RVA=0x09EEF4EC  token=0x600031A  System.Void CreateVerifyData()
  RVA=0x09EF17A0  token=0x600031B  MagicaCloth.Define.Error VerifyData()
  RVA=0x09EEFCE0  token=0x600031C  System.String GetInformation()
  RVA=-1  // generic def  token=0x600031D  System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict)
  RVA=0x09EF1158  token=0x600031E  System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones()
  RVA=0x09EEF870  token=0x600031F  System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList)
  RVA=0x09EEFC00  token=0x6000320  System.Collections.Generic.List<System.Int32> GetEditorTriangleList()
  RVA=0x09EEF790  token=0x6000321  System.Collections.Generic.List<System.Int32> GetEditorLineList()
  RVA=0x09EF09C0  token=0x6000322  System.Collections.Generic.List<System.Int32> GetSelectionList()
  RVA=0x011EC580  token=0x6000323  System.Collections.Generic.List<System.Int32> GetUseList()
  RVA=0x09EEF60C  token=0x6000324  System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject()
  RVA=0x09EEF534  token=0x6000325  MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source)
  RVA=0x09EF12C8  token=0x6000326  System.Void ResetParams()
  RVA=0x09EF19E0  token=0x6000327  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.MagicaBoneSpring
TYPE:  class
TOKEN: 0x200004C
SIZE:  0x100
EXTENDS: MagicaCloth.BaseCloth
FIELDS:
  private   static  System.Int32                    DATA_VERSION  // const
  private   static  System.Int32                    ERR_DATA_VERSION  // const
  private           MagicaCloth.MeshData            meshData  // 0xc8
  private           System.Int32                    meshDataHash  // 0xd0
  private           System.Int32                    meshDataVersion  // 0xd4
  private           MagicaCloth.BoneClothTarget     clothTarget  // 0xd8
  private           System.Collections.Generic.List<UnityEngine.Transform>useTransformList  // 0xe0
  private           System.Collections.Generic.List<UnityEngine.Vector3>useTransformPositionList  // 0xe8
  private           System.Collections.Generic.List<UnityEngine.Quaternion>useTransformRotationList  // 0xf0
  private           System.Collections.Generic.List<UnityEngine.Vector3>useTransformScaleList  // 0xf8
PROPERTIES:
  ClothTarget  get=0x04D88EF0
  MeshData  get=0x04D8D1D0
  UseTransformCount  get=0x09EF3930
METHODS:
  RVA=0x04DA3410  token=0x6000328  MagicaCloth.ComponentType GetComponentType()
  RVA=0x09EF1D04  token=0x6000329  System.Int32 GetDataHash()
  RVA=0x09EF32D0  token=0x600032C  System.Void Reset()
  RVA=0x09EF12C0  token=0x600032D  System.Void OnValidate()
  RVA=0x09EF1B3C  token=0x600032E  System.Void ClothInit()
  RVA=0x09EEF494  token=0x600032F  System.Void ClothDispose()
  RVA=0x09EEF48C  token=0x6000330  System.Void ClothActive()
  RVA=0x09EF1634  token=0x6000331  System.UInt32 UserFlag(System.Int32 index)
  RVA=0x09EF3468  token=0x6000332  UnityEngine.Transform UserTransform(System.Int32 index)
  RVA=0x09EF32D8  token=0x6000333  Unity.Mathematics.float3 UserTransformLocalPosition(System.Int32 vindex)
  RVA=0x09EF33A8  token=0x6000334  Unity.Mathematics.quaternion UserTransformLocalRotation(System.Int32 vindex)
  RVA=0x012081B0  token=0x6000335  System.Boolean IsRequiresDeformer()
  RVA=0x011EC580  token=0x6000336  MagicaCloth.BaseMeshDeformer GetDeformer()
  RVA=0x04D8D1D0  token=0x6000337  MagicaCloth.MeshData GetMeshData()
  RVA=0x041E1670  token=0x6000338  System.Void WorkerInit()
  RVA=0x041E1670  token=0x6000339  System.Void SetDeformerUseVertex(System.Boolean sw, MagicaCloth.BaseMeshDeformer deformer)
  RVA=0x09EEF444  token=0x600033A  System.Void ChangeUseUnityPhysics(System.Boolean sw)
  RVA=0x09EF2E5C  token=0x600033B  System.Void OnChangeCalculation()
  RVA=0x09EF2A54  token=0x600033C  System.Collections.Generic.List<UnityEngine.Transform> GetTransformList()
  RVA=0x09EF2D4C  token=0x600033D  UnityEngine.Transform GetUseTransform(System.Int32 index)
  RVA=0x04D9BE00  token=0x600033F  System.Int32 GetVersion()
  RVA=0x04D8C9E0  token=0x6000340  System.Int32 GetErrorVersion()
  RVA=0x09EEF4EC  token=0x6000341  System.Void CreateVerifyData()
  RVA=0x09EF3470  token=0x6000342  MagicaCloth.Define.Error VerifyData()
  RVA=0x09EF2370  token=0x6000343  System.String GetInformation()
  RVA=-1  // generic def  token=0x6000344  System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict)
  RVA=0x09EF2DD0  token=0x6000345  System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones()
  RVA=0x09EF1F00  token=0x6000346  System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList)
  RVA=0x09EF2290  token=0x6000347  System.Collections.Generic.List<System.Int32> GetEditorTriangleList()
  RVA=0x09EF1E20  token=0x6000348  System.Collections.Generic.List<System.Int32> GetEditorLineList()
  RVA=0x09EF29B0  token=0x6000349  System.Collections.Generic.List<System.Int32> GetSelectionList()
  RVA=0x09EF2B64  token=0x600034A  System.Collections.Generic.List<System.Int32> GetUseList()
  RVA=0x09EF1C9C  token=0x600034B  System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject()
  RVA=0x09EF1BC4  token=0x600034C  MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source)
  RVA=0x09EF2F64  token=0x600034D  System.Void ResetParams()
  RVA=0x09EF3790  token=0x600034E  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.MagicaMeshCloth
TYPE:  class
TOKEN: 0x200004D
SIZE:  0xD8
EXTENDS: MagicaCloth.BaseCloth
FIELDS:
  private   static  System.Int32                    DATA_VERSION  // const
  private   static  System.Int32                    ERR_DATA_VERSION  // const
  private           MagicaCloth.MagicaVirtualDeformervirtualDeformer  // 0xc8
  private           System.Int32                    virtualDeformerHash  // 0xd0
  private           System.Int32                    virtualDeformerVersion  // 0xd4
PROPERTIES:
  Deformer  get=0x09EF51C8
METHODS:
  RVA=0x04DA3420  token=0x600034F  MagicaCloth.ComponentType GetComponentType()
  RVA=0x09EF3B3C  token=0x6000350  System.Int32 GetDataHash()
  RVA=0x09EF4E44  token=0x6000352  System.Void Reset()
  RVA=0x09EF12C0  token=0x6000353  System.Void OnValidate()
  RVA=0x09EF4ADC  token=0x6000354  System.Void OnInit()
  RVA=0x09EF4AC4  token=0x6000355  System.Void OnActive()
  RVA=0x09EF4AD4  token=0x6000356  System.Void OnInactive()
  RVA=0x09EF4ACC  token=0x6000357  System.Void OnDispose()
  RVA=0x011EC580  token=0x6000358  System.UInt32 UserFlag(System.Int32 index)
  RVA=0x011EC580  token=0x6000359  UnityEngine.Transform UserTransform(System.Int32 index)
  RVA=0x04DA3430  token=0x600035A  Unity.Mathematics.float3 UserTransformLocalPosition(System.Int32 vindex)
  RVA=0x09EF4F38  token=0x600035B  Unity.Mathematics.quaternion UserTransformLocalRotation(System.Int32 vindex)
  RVA=0x02FFF600  token=0x600035C  System.Boolean IsRequiresDeformer()
  RVA=0x09EF3B7C  token=0x600035D  MagicaCloth.BaseMeshDeformer GetDeformer()
  RVA=0x09EF4724  token=0x600035E  MagicaCloth.MeshData GetMeshData()
  RVA=0x09EF5068  token=0x600035F  System.Void WorkerInit()
  RVA=0x09EF4E4C  token=0x6000360  System.Void SetDeformerUseVertex(System.Boolean sw, MagicaCloth.BaseMeshDeformer deformer)
  RVA=0x09EF3970  token=0x6000361  System.Void ChangeUseUnityPhysics(System.Boolean sw)
  RVA=0x04D9BE00  token=0x6000362  System.Int32 GetVersion()
  RVA=0x04D8C9E0  token=0x6000363  System.Int32 GetErrorVersion()
  RVA=0x09EEF4EC  token=0x6000364  System.Void CreateVerifyData()
  RVA=0x09EF4F7C  token=0x6000365  MagicaCloth.Define.Error VerifyData()
  RVA=0x09EF3C20  token=0x6000366  System.String GetInformation()
  RVA=0x09EF4A28  token=0x6000367  System.Boolean IsValidPointSelect()
  RVA=0x09EF3BAC  token=0x6000368  System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList)
  RVA=0x09EF3BF8  token=0x6000369  System.Collections.Generic.List<System.Int32> GetEditorTriangleList()
  RVA=0x09EF3B84  token=0x600036A  System.Collections.Generic.List<System.Int32> GetEditorLineList()
  RVA=0x09EF4744  token=0x600036B  System.Collections.Generic.List<System.Int32> GetSelectionList()
  RVA=0x09EF4858  token=0x600036C  System.Collections.Generic.List<System.Int32> GetUseList()
  RVA=0x09EF3ABC  token=0x600036D  System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject()
  RVA=0x09EF39AC  token=0x600036E  MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source)
  RVA=0x09EF4AE4  token=0x600036F  System.Void ResetParams()
  RVA=0x09EF51C0  token=0x6000370  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.MagicaMeshSpring
TYPE:  class
TOKEN: 0x200004E
SIZE:  0xF8
EXTENDS: MagicaCloth.BaseCloth
FIELDS:
  private   static  System.Int32                    DATA_VERSION  // const
  private   static  System.Int32                    ERR_DATA_VERSION  // const
  private           MagicaCloth.MagicaVirtualDeformervirtualDeformer  // 0xc8
  private           System.Int32                    virtualDeformerHash  // 0xd0
  private           System.Int32                    virtualDeformerVersion  // 0xd4
  private           UnityEngine.Transform           centerTransform  // 0xd8
  private           MagicaCloth.MagicaMeshSpring.AxisdirectionAxis  // 0xe0
  private           MagicaCloth.SpringData          springData  // 0xe8
  private           System.Int32                    springDataHash  // 0xf0
  private           System.Int32                    springDataVersion  // 0xf4
PROPERTIES:
  Deformer  get=0x09EF67A0
  SpringData  get=0x04D88590
  UseVertexCount  get=0x09EF6818
  CenterTransform  get=0x04D88EF0  set=0x06902698
  DirectionAxis  get=0x04D9A880  set=0x04DA3470
  CenterTransformDirection  get=0x09EF660C
METHODS:
  RVA=0x04DA3450  token=0x6000371  MagicaCloth.ComponentType GetComponentType()
  RVA=0x09EF55CC  token=0x6000372  System.Int32 GetDataHash()
  RVA=0x09EF564C  token=0x600037B  MagicaCloth.SpringData.DeformerData GetDeformerData()
  RVA=0x09EF61C0  token=0x600037C  System.Void Reset()
  RVA=0x09EF12C0  token=0x600037D  System.Void OnValidate()
  RVA=0x09EF5240  token=0x600037E  System.Void ClothInit()
  RVA=0x09EEF48C  token=0x600037F  System.Void ClothActive()
  RVA=0x04DA3460  token=0x6000380  System.UInt32 UserFlag(System.Int32 index)
  RVA=0x04D88EF0  token=0x6000381  UnityEngine.Transform UserTransform(System.Int32 index)
  RVA=0x09EF626C  token=0x6000382  Unity.Mathematics.float3 UserTransformLocalPosition(System.Int32 vindex)
  RVA=0x09EF62D0  token=0x6000383  Unity.Mathematics.quaternion UserTransformLocalRotation(System.Int32 vindex)
  RVA=0x02FFF600  token=0x6000384  System.Boolean IsRequiresDeformer()
  RVA=0x09EF566C  token=0x6000385  MagicaCloth.BaseMeshDeformer GetDeformer()
  RVA=0x011EC580  token=0x6000386  MagicaCloth.MeshData GetMeshData()
  RVA=0x09EF6490  token=0x6000387  System.Void WorkerInit()
  RVA=0x09EF61C8  token=0x6000388  System.Void SetDeformerUseVertex(System.Boolean sw, MagicaCloth.BaseMeshDeformer deformer)
  RVA=0x09EF3970  token=0x6000389  System.Void ChangeUseUnityPhysics(System.Boolean sw)
  RVA=0x09EF5D74  token=0x600038A  System.Void OnChangeCalculation()
  RVA=0x04D9BE00  token=0x600038B  System.Int32 GetVersion()
  RVA=0x04D8C9E0  token=0x600038C  System.Int32 GetErrorVersion()
  RVA=0x09EF541C  token=0x600038D  System.Void CreateVerifyData()
  RVA=0x09EF6324  token=0x600038E  MagicaCloth.Define.Error VerifyData()
  RVA=0x09EF58E4  token=0x600038F  System.String GetInformation()
  RVA=0x041E1670  token=0x6000390  System.Void VerifyDeformer()
  RVA=-1  // generic def  token=0x6000391  System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict)
  RVA=0x09EF5D0C  token=0x6000392  System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones()
  RVA=0x09EF56E0  token=0x6000393  System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList)
  RVA=0x011EC580  token=0x6000394  System.Collections.Generic.List<System.Int32> GetEditorTriangleList()
  RVA=0x011EC580  token=0x6000395  System.Collections.Generic.List<System.Int32> GetEditorLineList()
  RVA=0x011EC580  token=0x6000396  System.Collections.Generic.List<System.Int32> GetSelectionList()
  RVA=0x011EC580  token=0x6000397  System.Collections.Generic.List<System.Int32> GetUseList()
  RVA=0x09EF5564  token=0x6000398  System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject()
  RVA=0x09EF548C  token=0x6000399  MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source)
  RVA=0x09EF5E08  token=0x600039A  System.Void ResetParams()
  RVA=0x09EF51C0  token=0x600039B  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.SelectionData
TYPE:  class
TOKEN: 0x2000050
SIZE:  0x28
EXTENDS: MagicaCloth.ShareDataObject
FIELDS:
  private   static  System.Int32                    DATA_VERSION  // const
  public    static  System.Int32                    Invalid  // const
  public    static  System.Int32                    Move  // const
  public    static  System.Int32                    Fixed  // const
  public    static  System.Int32                    Extend  // const
  public            System.Collections.Generic.List<MagicaCloth.SelectionData.DeformerSelection>selectionList  // 0x20
PROPERTIES:
  DeformerCount  get=0x09EFC5A4
METHODS:
  RVA=0x09EFB50C  token=0x600039D  System.Int32 GetDataHash()
  RVA=0x02BBE540  token=0x600039E  System.Int32 GetVersion()
  RVA=0x09EFC518  token=0x600039F  MagicaCloth.Define.Error VerifyData()
  RVA=0x09EFB430  token=0x60003A0  System.Boolean Compare(MagicaCloth.SelectionData sel)
  RVA=0x09EFB548  token=0x60003A1  System.Collections.Generic.List<System.Int32> GetSelectionData(MagicaCloth.MeshData meshData, System.Collections.Generic.List<MagicaCloth.MeshData> childMeshDataList)
  RVA=0x09EFB960  token=0x60003A2  System.Int32 GetSelection(MagicaCloth.MeshData meshData, System.Int32 vindex, System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<System.UInt32>> dict, System.Collections.Generic.List<MagicaCloth.MeshData> childMeshDataList, System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.UInt64,System.Int32>> hashList)
  RVA=0x09EFBE2C  token=0x60003A3  System.Void SetSelectionData(MagicaCloth.MeshData meshData, System.Collections.Generic.List<System.Int32> selects, System.Collections.Generic.List<MagicaCloth.MeshData> childMeshDataList)
  RVA=0x09EFB700  token=0x60003A4  System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.UInt64,System.Int32>> GetSelectionVertexHashList()
  RVA=0x09EFC54C  token=0x60003A5  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.SpringData
TYPE:  class
TOKEN: 0x2000052
SIZE:  0x38
EXTENDS: MagicaCloth.ShareDataObject
FIELDS:
  private   static  System.Int32                    DATA_VERSION  // const
  public            MagicaCloth.SpringData.DeformerDatadeformerData  // 0x20
  public            UnityEngine.Vector3             initScale  // 0x28
PROPERTIES:
  UseVertexCount  get=0x09EFC620
METHODS:
  RVA=0x09EFC5E4  token=0x60003A9  System.Int32 GetDataHash()
  RVA=0x02BBE540  token=0x60003AB  System.Int32 GetVersion()
  RVA=0x09EFC604  token=0x60003AC  MagicaCloth.Define.Error VerifyData()
  RVA=0x03573370  token=0x60003AD  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.Define
TYPE:  static class
TOKEN: 0x2000054
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x06911EF8  token=0x60003B1  System.Boolean IsNormal(MagicaCloth.Define.Error err)
  RVA=0x09EEF16C  token=0x60003B2  System.Boolean IsError(MagicaCloth.Define.Error err)
  RVA=0x09EEF180  token=0x60003B3  System.Boolean IsWarning(MagicaCloth.Define.Error err)
  RVA=0x09EEEE0C  token=0x60003B4  System.String GetErrorMessage(MagicaCloth.Define.Error err)
END_CLASS

CLASS: MagicaCloth.BaseMeshDeformer
TYPE:  abstract class
TOKEN: 0x2000059
SIZE:  0x48
IMPLEMENTS: MagicaCloth.IEditorMesh MagicaCloth.IDataVerify MagicaCloth.IDataHash
FIELDS:
  private           MagicaCloth.MeshData            meshData  // 0x10
  private           UnityEngine.GameObject          targetObject  // 0x18
  protected         System.Int32                    dataHash  // 0x20
  protected         System.Int32                    dataVersion  // 0x24
  protected         MagicaCloth.RuntimeStatus       status  // 0x28
  private           MagicaCloth.CoreComponent       parent  // 0x30
  private           System.Int32                    <MeshIndex>k__BackingField  // 0x38
  private           System.Int32                    <VertexCount>k__BackingField  // 0x3c
  private           System.Int32                    <SkinningVertexCount>k__BackingField  // 0x40
  private           System.Int32                    <TriangleCount>k__BackingField  // 0x44
PROPERTIES:
  Parent  get=0x011F36E0  set=0x05396200
  MeshData  get=0x02B2ECC0  set=0x053908C0
  TargetObject  get=0x0385B100  set=0x05392C40
  Status  get=0x04D86240
  MeshIndex  get=0x04D863B0  set=0x04D86430
  VertexCount  get=0x04D86640  set=0x04D86670
  SkinningVertexCount  get=0x04D865E0  set=0x04D86610
  TriangleCount  get=0x04D86530  set=0x04D86580
  IsSkinning  get=0x09EEBE70
  BoneCount  get=0x09EEBDF8
  SaveDataHash  get=0x02B70F90
  SaveDataVersion  get=0x011F0020
METHODS:
  RVA=0x09EEBA2C  token=0x60003C4  System.Void Init()
  RVA=0x09EEBBF0  token=0x60003C5  System.Void OnInit()
  RVA=0x09EEB8CC  token=0x60003C6  System.Void Dispose()
  RVA=0x09EEBBB8  token=0x60003C7  System.Void OnEnable()
  RVA=0x09EEBB80  token=0x60003C8  System.Void OnDisable()
  RVA=0x09EEBC90  token=0x60003C9  System.Void Update()
  RVA=-1  // abstract  token=0x60003CA  System.Void MeshCalculation(System.Int32 bufferIndex)
  RVA=-1  // abstract  token=0x60003CB  System.Void NormalWriting(System.Int32 bufferIndex)
  RVA=0x09EEBC54  token=0x60003CC  System.Void OnUpdateStatus()
  RVA=0x041E1670  token=0x60003CD  System.Void OnActive()
  RVA=0x041E1670  token=0x60003CE  System.Void OnInactive()
  RVA=0x012081B0  token=0x60003CF  System.Boolean IsMeshUse()
  RVA=0x012081B0  token=0x60003D0  System.Boolean IsActiveMesh()
  RVA=0x041E1670  token=0x60003D3  System.Void AddUseMesh(System.Object parent)
  RVA=0x041E1670  token=0x60003D4  System.Void RemoveUseMesh(System.Object parent)
  RVA=0x012081B0  token=0x60003D5  System.Boolean AddUseVertex(System.Int32 vindex, System.Boolean fix)
  RVA=0x012081B0  token=0x60003D6  System.Boolean RemoveUseVertex(System.Int32 vindex, System.Boolean fix)
  RVA=0x041E1670  token=0x60003D7  System.Void ResetFuturePrediction()
  RVA=0x041E1670  token=0x60003D8  System.Void ChangeUseUnityPhysics(System.Boolean sw)
  RVA=0x09EEB954  token=0x60003D9  System.Int32 GetDataHash()
  RVA=-1  // abstract  token=0x60003DC  System.Int32 GetVersion()
  RVA=0x09EEBCD4  token=0x60003DD  MagicaCloth.Define.Error VerifyData()
  RVA=0x09EEB89C  token=0x60003DE  System.Void CreateVerifyData()
  RVA=0x09EEBA00  token=0x60003DF  System.String GetInformation()
  RVA=-1  // abstract  token=0x60003E0  System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList)
  RVA=-1  // abstract  token=0x60003E1  System.Collections.Generic.List<System.Int32> GetEditorTriangleList()
  RVA=-1  // abstract  token=0x60003E2  System.Collections.Generic.List<System.Int32> GetEditorLineList()
  RVA=0x09EEBD8C  token=0x60003E3  System.Void .ctor()
  RVA=0x011F36E0  token=0x60003E4  System.Object <Init>b__33_0()
END_CLASS

CLASS: MagicaCloth.MagicaRenderDeformer
TYPE:  class
TOKEN: 0x200005A
SIZE:  0x50
EXTENDS: MagicaCloth.CoreComponent
FIELDS:
  private   static  System.Int32                    DATA_VERSION  // const
  private   static  System.Int32                    ERR_DATA_VERSION  // const
  private           MagicaCloth.RenderMeshDeformer  deformer  // 0x38
  private           System.Int32                    deformerHash  // 0x40
  private           System.Int32                    deformerVersion  // 0x44
  private           MagicaCloth.PhysicsTeam.TeamCullingMode<cullModeCash>k__BackingField  // 0x48
PROPERTIES:
  cullModeCash  get=0x04D863D0  set=0x04D86450
  Deformer  get=0x09EF7280
METHODS:
  RVA=0x04DA3480  token=0x60003E7  MagicaCloth.ComponentType GetComponentType()
  RVA=0x09EF6A60  token=0x60003E8  System.Int32 GetDataHash()
  RVA=0x09EF6DE0  token=0x60003EA  System.Void OnValidate()
  RVA=0x09EF6D98  token=0x60003EB  System.Void OnInit()
  RVA=0x09EF6D4C  token=0x60003EC  System.Void OnDispose()
  RVA=0x09EF6DBC  token=0x60003ED  System.Void OnUpdate()
  RVA=0x09EF6C0C  token=0x60003EE  System.Void OnActive()
  RVA=0x09EF6D74  token=0x60003EF  System.Void OnInactive()
  RVA=0x09EF6C78  token=0x60003F0  System.Void OnBecameVisible()
  RVA=0x09EF6C30  token=0x60003F1  System.Void OnBecameInvisible()
  RVA=0x09EF6E04  token=0x60003F2  System.Void UpdateCullingMode(MagicaCloth.CoreComponent caller)
  RVA=0x09EF6CC0  token=0x60003F3  System.Void OnChangeCalculation()
  RVA=0x02BBE540  token=0x60003F4  System.Int32 GetVersion()
  RVA=0x011EC580  token=0x60003F5  System.Int32 GetErrorVersion()
  RVA=0x09EF68C4  token=0x60003F6  System.Void CreateVerifyData()
  RVA=0x09EF7170  token=0x60003F7  MagicaCloth.Define.Error VerifyData()
  RVA=0x09EF6B24  token=0x60003F8  System.String GetInformation()
  RVA=-1  // generic def  token=0x60003F9  System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict)
  RVA=0x09EF6B90  token=0x60003FA  System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones()
  RVA=0x09EF6890  token=0x60003FB  System.Void ChangeUseUnityPhysics(System.Boolean sw)
  RVA=0x09EF6AB0  token=0x60003FC  System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList)
  RVA=0x09EF6AFC  token=0x60003FD  System.Collections.Generic.List<System.Int32> GetEditorTriangleList()
  RVA=0x09EF6A88  token=0x60003FE  System.Collections.Generic.List<System.Int32> GetEditorLineList()
  RVA=0x09EF6B6C  token=0x60003FF  System.Collections.Generic.List<System.Int32> GetUseList()
  RVA=0x09EF69EC  token=0x6000400  System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject()
  RVA=0x09EF690C  token=0x6000401  MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source)
  RVA=0x09EF720C  token=0x6000402  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.MagicaVirtualDeformer
TYPE:  class
TOKEN: 0x200005B
SIZE:  0x50
EXTENDS: MagicaCloth.CoreComponent
FIELDS:
  private   static  System.Int32                    DATA_VERSION  // const
  private   static  System.Int32                    ERR_DATA_VERSION  // const
  private           MagicaCloth.VirtualMeshDeformer deformer  // 0x38
  private           System.Int32                    deformerHash  // 0x40
  private           System.Int32                    deformerVersion  // 0x44
  private           MagicaCloth.PhysicsTeam.TeamCullingMode<cullModeCash>k__BackingField  // 0x48
PROPERTIES:
  cullModeCash  get=0x04D863D0  set=0x04D86450
  Deformer  get=0x09EF7280
METHODS:
  RVA=0x04DA3490  token=0x6000405  MagicaCloth.ComponentType GetComponentType()
  RVA=0x09EF6A60  token=0x6000406  System.Int32 GetDataHash()
  RVA=0x041E1670  token=0x6000408  System.Void OnValidate()
  RVA=0x09EF7810  token=0x6000409  System.Void OnInit()
  RVA=0x09EF77D4  token=0x600040A  System.Void OnDispose()
  RVA=0x09EF6DBC  token=0x600040B  System.Void OnUpdate()
  RVA=0x09EF6C0C  token=0x600040C  System.Void OnActive()
  RVA=0x09EF6D74  token=0x600040D  System.Void OnInactive()
  RVA=0x09EF76A4  token=0x600040E  System.Void LinkRenderDeformerStatus(System.Boolean sw)
  RVA=0x09EF72AC  token=0x600040F  System.Void ChangeUseUnityPhysics(System.Boolean sw)
  RVA=0x09EF7848  token=0x6000410  System.Void UpdateCullingMode(MagicaCloth.CoreComponent caller)
  RVA=0x09EF77A0  token=0x6000411  System.Void OnChangeCalculation()
  RVA=0x02BBE540  token=0x6000412  System.Int32 GetVersion()
  RVA=0x011EC580  token=0x6000413  System.Int32 GetErrorVersion()
  RVA=0x09EF68C4  token=0x6000414  System.Void CreateVerifyData()
  RVA=0x09EF7C2C  token=0x6000415  MagicaCloth.Define.Error VerifyData()
  RVA=0x09EF6B24  token=0x6000416  System.String GetInformation()
  RVA=-1  // generic def  token=0x6000417  System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict)
  RVA=0x09EF7628  token=0x6000418  System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones()
  RVA=0x09EF6AB0  token=0x6000419  System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList)
  RVA=0x09EF6AFC  token=0x600041A  System.Collections.Generic.List<System.Int32> GetEditorTriangleList()
  RVA=0x09EF6A88  token=0x600041B  System.Collections.Generic.List<System.Int32> GetEditorLineList()
  RVA=0x09EF749C  token=0x600041C  System.Collections.Generic.List<System.Int32> GetUseList()
  RVA=0x09EF7428  token=0x600041D  System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject()
  RVA=0x09EF7348  token=0x600041E  MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source)
  RVA=0x09EF7C98  token=0x600041F  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.MeshData
TYPE:  class
TOKEN: 0x200005C
SIZE:  0x98
EXTENDS: MagicaCloth.ShareDataObject
FIELDS:
  private   static  System.Int32                    DATA_VERSION  // const
  public            System.Boolean                  isSkinning  // 0x20
  public            System.Int32                    vertexCount  // 0x24
  public            System.UInt32[]                 vertexInfoList  // 0x28
  public            MagicaCloth.MeshData.VertexWeight[]vertexWeightList  // 0x30
  public            System.UInt64[]                 vertexHashList  // 0x38
  public            UnityEngine.Vector2[]           uvList  // 0x40
  public            System.Int32                    lineCount  // 0x48
  public            System.Int32[]                  lineList  // 0x50
  public            System.Int32                    triangleCount  // 0x58
  public            System.Int32[]                  triangleList  // 0x60
  public            System.Int32                    boneCount  // 0x68
  public            System.UInt32[]                 vertexToTriangleInfoList  // 0x70
  public            System.Int32[]                  vertexToTriangleIndexList  // 0x78
  public            System.Collections.Generic.List<MagicaCloth.MeshData.ChildData>childDataList  // 0x80
  public            UnityEngine.Vector3             baseScale  // 0x88
PROPERTIES:
  VertexCount  get=0x011F0020
  VertexHashCount  get=0x09EF88A8
  WeightCount  get=0x09EF88BC
  LineCount  get=0x04D863D0
  TriangleCount  get=0x04D86570
  BoneCount  get=0x04D86CF0
  ChildCount  get=0x09EF8868
METHODS:
  RVA=0x09EF8428  token=0x6000427  System.Int32 GetDataHash()
  RVA=0x02BBE540  token=0x6000428  System.Int32 GetVersion()
  RVA=0x09EF87EC  token=0x6000429  MagicaCloth.Define.Error VerifyData()
  RVA=0x09EF862C  token=0x600042A  System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<System.UInt32>> GetVirtualToChildVertexDict()
  RVA=0x09EF7D0C  token=0x600042B  System.Collections.Generic.List<System.Int32> ExtendSelection(System.Collections.Generic.List<System.Int32> originalSelection, System.Boolean extendNext, System.Boolean extendWeight)
  RVA=0x09EF8808  token=0x600042C  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.RenderMeshDeformer
TYPE:  class
TOKEN: 0x200005F
SIZE:  0xA8
EXTENDS: MagicaCloth.BaseMeshDeformer
IMPLEMENTS: MagicaCloth.IBoneReplace
FIELDS:
  private   static  System.Int32                    DATA_VERSION  // const
  private           MagicaCloth.RenderMeshDeformer.RecalculateModenormalAndTangentUpdateMode  // 0x48
  private           MagicaCloth.RenderMeshDeformer.BoundsModeboundsUpdateMode  // 0x4c
  private           UnityEngine.Mesh                sharedMesh  // 0x50
  private           System.Int32                    meshOptimize  // 0x58
  private           UnityEngine.Renderer            renderer  // 0x60
  private           UnityEngine.MeshFilter          meshFilter  // 0x68
  private           UnityEngine.SkinnedMeshRenderer skinMeshRenderer  // 0x70
  private           UnityEngine.Transform[]         originalBones  // 0x78
  private           UnityEngine.Transform[]         boneList  // 0x80
  private           UnityEngine.Mesh                cloneMesh  // 0x88
  private           UnityEngine.GraphicsBuffer      vertexBuffer  // 0x90
  private           System.Boolean                  <IsChangePosition>k__BackingField  // 0x98
  private           System.Boolean                  <IsChangeNormalTangent>k__BackingField  // 0x99
  private           System.Boolean                  <IsChangeBoneWeights>k__BackingField  // 0x9a
  private           System.Boolean                  oldUse  // 0x9b
  private           System.Boolean                  <IsWriteSkip>k__BackingField  // 0x9c
  private           System.Boolean                  <IsFasterWriteUpdate>k__BackingField  // 0x9d
  private           System.Boolean                  <IsWriteMeshPosition>k__BackingField  // 0x9e
  private           System.Boolean                  <IsWriteMeshBoneWeight>k__BackingField  // 0x9f
  private           System.Boolean                  IsWriteMeshNormal  // 0xa0
  private           System.Boolean                  IsWriteMeshTangent  // 0xa1
  private           System.Boolean                  <IsFasterWrite>k__BackingField  // 0xa2
PROPERTIES:
  IsChangePosition  get=0x04D86C50  set=0x04D86C70
  IsChangeNormalTangent  get=0x04DA34C0  set=0x04DA3510
  IsChangeBoneWeights  get=0x04DA34B0  set=0x04DA3500
  IsWriteSkip  get=0x04D92ED0  set=0x04D92F60
  IsFasterWriteUpdate  get=0x04DA34D0  set=0x04DA3520
  IsWriteMeshPosition  get=0x04DA34F0  set=0x04DA3540
  IsWriteMeshBoneWeight  get=0x04DA34E0  set=0x04DA3530
  SharedMesh  get=0x04D86270
  IsFasterWrite  get=0x04D90800  set=0x04D90810
  IsRendererVisible  get=0x09EFB3C4
  HasNormal  get=0x09EFB3B0
METHODS:
  RVA=0x09EF8F14  token=0x600043E  System.Int32 GetDataHash()
  RVA=0x09EFAE54  token=0x6000442  System.Void OnValidate()
  RVA=0x09EFA570  token=0x6000443  System.Void OnInit()
  RVA=0x09EFA36C  token=0x6000444  System.Void OnActive()
  RVA=0x09EFA47C  token=0x6000445  System.Void OnInactive()
  RVA=0x09EF8CD4  token=0x6000446  System.Void Dispose()
  RVA=0x09EFB184  token=0x6000447  System.Void SetRecalculateNormalAndTangentMode()
  RVA=0x09EF8C50  token=0x6000448  System.Void ChangeUseUnityPhysics(System.Boolean sw)
  RVA=0x09EF8BC0  token=0x6000449  System.Void ChangeCalculation(System.Boolean sw)
  RVA=0x09EF9D88  token=0x600044A  System.Boolean IsMeshUse()
  RVA=0x09EF9D10  token=0x600044B  System.Boolean IsActiveMesh()
  RVA=0x09EF88D0  token=0x600044C  System.Void AddUseMesh(System.Object parent)
  RVA=0x09EFAE94  token=0x600044D  System.Void RemoveUseMesh(System.Object parent)
  RVA=0x09EF9E00  token=0x6000450  System.Void MeshCalculation(System.Int32 bufferIndex)
  RVA=0x09EFA1F4  token=0x6000451  System.Void NormalWriting(System.Int32 bufferIndex)
  RVA=0x09EF8DC8  token=0x6000452  System.Boolean FasterWriting(System.Int32 bufferIndex, UnityEngine.ComputeShader compute, System.Int32 kernel, System.Int32 index, System.Int32& maxVertexCount)
  RVA=0x04DA34A0  token=0x6000453  System.Void ChangeNormalTangentUpdateMode()
  RVA=-1  // generic def  token=0x6000454  System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict)
  RVA=0x09EF9C0C  token=0x6000455  System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones()
  RVA=0x09EF8F98  token=0x6000456  System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList)
  RVA=0x09EF9328  token=0x6000457  System.Collections.Generic.List<System.Int32> GetEditorTriangleList()
  RVA=0x011EC580  token=0x6000458  System.Collections.Generic.List<System.Int32> GetEditorLineList()
  RVA=0x09EF93D8  token=0x6000459  System.Collections.Generic.List<System.Int32> GetEditorUseList()
  RVA=0x02BBE540  token=0x600045A  System.Int32 GetVersion()
  RVA=0x09EFB25C  token=0x600045B  MagicaCloth.Define.Error VerifyData()
  RVA=0x09EF9AD4  token=0x600045C  UnityEngine.Mesh GetTargetSharedMesh()
  RVA=0x09EF945C  token=0x600045D  System.String GetInformation()
  RVA=0x09EFB3A0  token=0x600045E  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.VirtualMeshDeformer
TYPE:  class
TOKEN: 0x2000062
SIZE:  0x88
EXTENDS: MagicaCloth.BaseMeshDeformer
IMPLEMENTS: MagicaCloth.IBoneReplace
FIELDS:
  private   static  System.Int32                    DATA_VERSION  // const
  private           System.Collections.Generic.List<MagicaCloth.MagicaRenderDeformer>renderDeformerList  // 0x48
  private           System.Collections.Generic.List<System.Int32>renderDeformerHashList  // 0x50
  private           System.Int32                    renderDeformerVersion  // 0x58
  private           System.Single                   mergeVertexDistance  // 0x5c
  private           System.Single                   mergeTriangleDistance  // 0x60
  private           System.Single                   sameSurfaceAngle  // 0x64
  private           System.Boolean                  useSkinning  // 0x68
  private           System.Int32                    maxWeightCount  // 0x6c
  private           System.Single                   weightPow  // 0x70
  private           System.Collections.Generic.List<UnityEngine.Transform>boneList  // 0x78
  private           System.Collections.Generic.List<System.Int32>sharedChildMeshIndexList  // 0x80
PROPERTIES:
  MergeVertexDistance  get=0x04D91500
  MergeTriangleDistance  get=0x04D88480
  SameSurfaceAngle  get=0x04D88470
  MaxWeightCount  get=0x09EFEAC4
  WeightPow  get=0x04D90610
  RenderDeformerCount  get=0x09EFEAF0
METHODS:
  RVA=0x09EFCDD0  token=0x600045F  System.Int32 GetDataHash()
  RVA=0x09EFDE7C  token=0x6000460  System.Void OnInit()
  RVA=0x09EFCBA8  token=0x6000461  System.Void Dispose()
  RVA=0x09EFDD10  token=0x6000462  System.Void OnActive()
  RVA=0x09EFDDB8  token=0x6000463  System.Void OnInactive()
  RVA=0x041E1670  token=0x6000464  System.Void MeshCalculation(System.Int32 bufferIndex)
  RVA=0x041E1670  token=0x6000465  System.Void NormalWriting(System.Int32 bufferIndex)
  RVA=-1  // generic def  token=0x6000466  System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict)
  RVA=0x09EFDBAC  token=0x6000467  System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones()
  RVA=0x0464E630  token=0x6000468  MagicaCloth.BaseMeshDeformer GetDeformer()
  RVA=0x09EFDA88  token=0x600046F  MagicaCloth.MagicaRenderDeformer GetRenderDeformer(System.Int32 index)
  RVA=0x09EFDADC  token=0x6000470  System.Int32 GetRenderMeshDeformerIndex(MagicaCloth.RenderMeshDeformer deformer)
  RVA=0x09EFD938  token=0x6000471  System.Collections.Generic.List<MagicaCloth.MeshData> GetRenderDeformerMeshList()
  RVA=0x09EFDC98  token=0x6000472  System.Boolean IsMeshUse()
  RVA=0x09EFDC20  token=0x6000473  System.Boolean IsActiveMesh()
  RVA=0x09EFC794  token=0x6000474  System.Void AddUseMesh(System.Object parent)
  RVA=0x09EFE378  token=0x6000475  System.Void RemoveUseMesh(System.Object parent)
  RVA=0x09EFC878  token=0x6000476  System.Boolean AddUseVertex(System.Int32 vindex, System.Boolean fix)
  RVA=0x09EFE45C  token=0x6000477  System.Boolean RemoveUseVertex(System.Int32 vindex, System.Boolean fix)
  RVA=0x09EFE4F4  token=0x6000478  System.Void ResetFuturePrediction()
  RVA=0x09EFC9A0  token=0x6000479  System.Void ChangeUseUnityPhysics(System.Boolean sw)
  RVA=0x09EFC910  token=0x600047A  System.Void ChangeCalculation(System.Boolean sw)
  RVA=0x09EFCF6C  token=0x600047B  System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList)
  RVA=0x09EFD284  token=0x600047C  System.Collections.Generic.List<System.Int32> GetEditorTriangleList()
  RVA=0x09EFCEB8  token=0x600047D  System.Collections.Generic.List<System.Int32> GetEditorLineList()
  RVA=0x04D85B80  token=0x600047E  System.Int32 GetVersion()
  RVA=0x09EFCA24  token=0x600047F  System.Void CreateVerifyData()
  RVA=0x09EFE554  token=0x6000480  MagicaCloth.Define.Error VerifyData()
  RVA=0x09EFD338  token=0x6000481  System.String GetInformation()
  RVA=0x09EFE954  token=0x6000482  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.ClampDistance2Constraint
TYPE:  class
TOKEN: 0x2000064
SIZE:  0x40
EXTENDS: MagicaCloth.PhysicsManagerConstraint
FIELDS:
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.ClampDistance2Constraint.ClampDistance2Data>dataList  // 0x20
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.ClampDistance2Constraint.ClampDistance2RootInfo>rootInfoList  // 0x28
  public            MagicaCloth.FixedNativeList<MagicaCloth.ClampDistance2Constraint.GroupData>groupList  // 0x30
  private           MagicaCloth.FixedChunkNativeArray<System.Int32>rootTeamList  // 0x38
METHODS:
  RVA=0x09EFF51C  token=0x6000485  System.Void Create()
  RVA=0x09EFF674  token=0x6000486  System.Void Release()
  RVA=0x09EFF0A8  token=0x6000487  System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, System.Single minRatio, System.Single maxRatio, System.Single velocityInfluence, MagicaCloth.ClampDistance2Constraint.ClampDistance2Data[] dataArray, MagicaCloth.ClampDistance2Constraint.ClampDistance2RootInfo[] rootInfoArray)
  RVA=0x09EFF71C  token=0x6000488  System.Void RemoveTeam(System.Int32 teamId)
  RVA=0x09EFF36C  token=0x6000489  System.Void ChangeParam(System.Int32 teamId, System.Boolean active, System.Single minRatio, System.Single maxRatio, System.Single velocityInfluence)
  RVA=0x09EFF91C  token=0x600048A  Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle)
  RVA=0x04D89110  token=0x600048B  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.ClampDistanceConstraint
TYPE:  class
TOKEN: 0x2000069
SIZE:  0x38
EXTENDS: MagicaCloth.PhysicsManagerConstraint
FIELDS:
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.ClampDistanceConstraint.ClampDistanceData>dataList  // 0x20
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.ReferenceDataIndex>refDataList  // 0x28
  public            MagicaCloth.FixedNativeList<MagicaCloth.ClampDistanceConstraint.GroupData>groupList  // 0x30
METHODS:
  RVA=0x0442E0A0  token=0x600048D  System.Void Create()
  RVA=0x09F00430  token=0x600048E  System.Void Release()
  RVA=0x09F000A0  token=0x600048F  System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, System.Single minRatio, System.Single maxRatio, System.Single velocityInfluence, MagicaCloth.ClampDistanceConstraint.ClampDistanceData[] dataArray, MagicaCloth.ReferenceDataIndex[] refDataArray)
  RVA=0x09F004B8  token=0x6000490  System.Void RemoveTeam(System.Int32 teamId)
  RVA=0x09F00280  token=0x6000491  System.Void ChangeParam(System.Int32 teamId, System.Boolean active, System.Single minRatio, System.Single maxRatio, System.Single velocityInfluence)
  RVA=0x09F00628  token=0x6000492  Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle)
  RVA=0x04D89110  token=0x6000493  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.ClampPositionConstraint
TYPE:  class
TOKEN: 0x200006D
SIZE:  0x28
EXTENDS: MagicaCloth.PhysicsManagerConstraint
FIELDS:
  public            MagicaCloth.FixedNativeList<MagicaCloth.ClampPositionConstraint.GroupData>groupList  // 0x20
METHODS:
  RVA=0x04D2FE20  token=0x6000496  System.Void Create()
  RVA=0x09F011D8  token=0x6000497  System.Void Release()
  RVA=0x09F00ED8  token=0x6000498  System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam limitLength, Unity.Mathematics.float3 axisRatio, System.Single velocityInfluence)
  RVA=0x09F01220  token=0x6000499  System.Void RemoveTeam(System.Int32 teamId)
  RVA=0x09F01004  token=0x600049A  System.Void ChangeParam(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam limitLength, Unity.Mathematics.float3 axisRatio, System.Single velocityInfluence)
  RVA=0x09F01304  token=0x600049B  Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle)
  RVA=0x04D89110  token=0x600049C  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.ClampRotationConstraint
TYPE:  class
TOKEN: 0x2000070
SIZE:  0x48
EXTENDS: MagicaCloth.PhysicsManagerConstraint
FIELDS:
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.ClampRotationConstraint.ClampRotationData>dataList  // 0x20
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.ClampRotationConstraint.ClampRotationRootInfo>rootInfoList  // 0x28
  public            MagicaCloth.FixedNativeList<MagicaCloth.ClampRotationConstraint.GroupData>groupList  // 0x30
  private           MagicaCloth.FixedChunkNativeArray<System.Int32>rootTeamList  // 0x38
  private           MagicaCloth.FixedChunkNativeArray<System.Single>lengthBuffer  // 0x40
METHODS:
  RVA=0x0442E1B0  token=0x600049F  System.Void Create()
  RVA=0x09F02118  token=0x60004A0  System.Void Release()
  RVA=0x09F01C70  token=0x60004A1  System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam maxAngle, System.Single velocityInfluence, MagicaCloth.ClampRotationConstraint.ClampRotationData[] dataArray, MagicaCloth.ClampRotationConstraint.ClampRotationRootInfo[] rootInfoArray)
  RVA=0x09F021E4  token=0x60004A2  System.Void RemoveTeam(System.Int32 teamId)
  RVA=0x09F01F38  token=0x60004A3  System.Void ChangeParam(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam maxAngle, System.Single velocityInfluence)
  RVA=0x09F023C0  token=0x60004A4  Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle)
  RVA=0x04D89110  token=0x60004A5  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.ColliderCollisionConstraint
TYPE:  class
TOKEN: 0x2000075
SIZE:  0x20
EXTENDS: MagicaCloth.PhysicsManagerConstraint
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60004A8  System.Void Create()
  RVA=0x041E1670  token=0x60004A9  System.Void RemoveTeam(System.Int32 teamId)
  RVA=0x09F03184  token=0x60004AA  System.Void ChangeParam(System.Int32 teamId, System.Boolean useCollision)
  RVA=0x041E1670  token=0x60004AB  System.Void Release()
  RVA=0x09F031D4  token=0x60004AC  Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle)
  RVA=0x04D89110  token=0x60004AD  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.ColliderExtrusionConstraint
TYPE:  class
TOKEN: 0x2000077
SIZE:  0x20
EXTENDS: MagicaCloth.PhysicsManagerConstraint
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60004B2  System.Void Create()
  RVA=0x041E1670  token=0x60004B3  System.Void RemoveTeam(System.Int32 teamId)
  RVA=0x041E1670  token=0x60004B4  System.Void Release()
  RVA=0x09F036AC  token=0x60004B5  Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle)
  RVA=0x04D89110  token=0x60004B6  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.CompositeRotationConstraint
TYPE:  class
TOKEN: 0x2000079
SIZE:  0x48
EXTENDS: MagicaCloth.PhysicsManagerConstraint
FIELDS:
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.CompositeRotationConstraint.RotationData>dataList  // 0x20
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.CompositeRotationConstraint.RootInfo>rootInfoList  // 0x28
  public            MagicaCloth.FixedNativeList<MagicaCloth.CompositeRotationConstraint.GroupData>groupList  // 0x30
  private           MagicaCloth.FixedChunkNativeArray<System.Int32>rootTeamList  // 0x38
  private           MagicaCloth.FixedChunkNativeArray<System.Single>lengthBuffer  // 0x40
METHODS:
  RVA=0x0442E350  token=0x60004B8  System.Void Create()
  RVA=0x09F04F28  token=0x60004B9  System.Void Release()
  RVA=0x09F04ADC  token=0x60004BA  System.Int32 AddGroup(System.Int32 teamId, System.Boolean useClamp, MagicaCloth.BezierParam maxAngle, System.Boolean useRestore, MagicaCloth.BezierParam restorePower, System.Single velocityInfluence, MagicaCloth.CompositeRotationConstraint.RotationData[] dataArray, MagicaCloth.CompositeRotationConstraint.RootInfo[] rootInfoArray)
  RVA=0x09F04FF4  token=0x60004BB  System.Void RemoveTeam(System.Int32 teamId)
  RVA=0x09F04D24  token=0x60004BC  System.Void ChangeParam(System.Int32 teamId, System.Boolean useClamp, MagicaCloth.BezierParam maxAngle, System.Boolean useRestore, MagicaCloth.BezierParam restorePower, System.Single velocityInfluence)
  RVA=0x09F05194  token=0x60004BD  Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle)
  RVA=0x04D89110  token=0x60004BE  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.EdgeCollisionConstraint
TYPE:  class
TOKEN: 0x200007E
SIZE:  0x48
EXTENDS: MagicaCloth.PhysicsManagerConstraint
FIELDS:
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.EdgeCollisionConstraint.EdgeCollisionData>dataList  // 0x20
  private           MagicaCloth.FixedChunkNativeArray<System.Int16>groupIndexList  // 0x28
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.ReferenceDataIndex>refDataList  // 0x30
  private           MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>writeBuffer  // 0x38
  private           MagicaCloth.FixedNativeList<MagicaCloth.EdgeCollisionConstraint.GroupData>groupList  // 0x40
METHODS:
  RVA=0x09F070D4  token=0x60004C2  System.Void Create()
  RVA=0x09F07278  token=0x60004C3  System.Void Release()
  RVA=0x09F06C0C  token=0x60004C4  System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, System.Single edgeRadius, MagicaCloth.EdgeCollisionConstraint.EdgeCollisionData[] dataArray, MagicaCloth.ReferenceDataIndex[] refDataArray, System.Int32 writeBufferCount)
  RVA=0x09F07344  token=0x60004C5  System.Void RemoveTeam(System.Int32 teamId)
  RVA=0x09F06F40  token=0x60004C6  System.Void ChangeParam(System.Int32 teamId, System.Boolean active, System.Single edgeRadius)
  RVA=0x09F07590  token=0x60004C7  Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle)
  RVA=0x04D89110  token=0x60004C8  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.PenetrationConstraint
TYPE:  class
TOKEN: 0x2000083
SIZE:  0x40
EXTENDS: MagicaCloth.PhysicsManagerConstraint
FIELDS:
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.PenetrationConstraint.PenetrationData>dataList  // 0x20
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.ReferenceDataIndex>refDataList  // 0x28
  private           MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>bonePenetrationDataList  // 0x30
  public            MagicaCloth.FixedNativeList<MagicaCloth.PenetrationConstraint.GroupData>groupList  // 0x38
METHODS:
  RVA=0x0442E4F0  token=0x60004CE  System.Void Create()
  RVA=0x09F084C4  token=0x60004CF  System.Void Release()
  RVA=0x09F07FFC  token=0x60004D0  System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, MagicaCloth.ClothParams.PenetrationMode mode, MagicaCloth.BezierParam distance, MagicaCloth.BezierParam radius, System.Single maxDepth, MagicaCloth.PenetrationConstraint.PenetrationData[] moveLimitDataList, MagicaCloth.ReferenceDataIndex[] refDataArray, Unity.Mathematics.float3[] bonePenetrationDataArray)
  RVA=0x09F0856C  token=0x60004D1  System.Void RemoveTeam(System.Int32 teamId)
  RVA=0x09F082CC  token=0x60004D2  System.Void ChangeParam(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam distance, MagicaCloth.BezierParam radius, System.Single maxDepth)
  RVA=0x09F086F4  token=0x60004D3  Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle)
  RVA=0x04D89110  token=0x60004D4  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.RestoreDistanceConstraint
TYPE:  class
TOKEN: 0x2000087
SIZE:  0x38
EXTENDS: MagicaCloth.PhysicsManagerConstraint
FIELDS:
  public    static  System.Int32                    StructType  // const
  public    static  System.Int32                    BendType  // const
  public    static  System.Int32                    NearType  // const
  public    static  System.Int32                    TypeCount  // const
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.RestoreDistanceConstraint.RestoreDistanceData>[]dataList  // 0x20
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.ReferenceDataIndex>[]refDataList  // 0x28
  public            MagicaCloth.FixedNativeList<MagicaCloth.RestoreDistanceConstraint.RestoreDistanceGroupData>groupList  // 0x30
METHODS:
  RVA=0x0442E870  token=0x60004D9  System.Void Create()
  RVA=0x09F0A2B8  token=0x60004DA  System.Void Release()
  RVA=0x09F09ACC  token=0x60004DB  System.Int32 AddGroup(System.Int32 teamId, MagicaCloth.BezierParam mass, System.Single velocityInfluence, MagicaCloth.BezierParam structStiffness, MagicaCloth.RestoreDistanceConstraint.RestoreDistanceData[] structDataArray, MagicaCloth.ReferenceDataIndex[] structRefDataArray, System.Boolean useBend, MagicaCloth.BezierParam bendStiffness, MagicaCloth.RestoreDistanceConstraint.RestoreDistanceData[] bendDataArray, MagicaCloth.ReferenceDataIndex[] bendRefDataArray, System.Boolean useNear, MagicaCloth.BezierParam nearStiffness, MagicaCloth.RestoreDistanceConstraint.RestoreDistanceData[] nearDataArray, MagicaCloth.ReferenceDataIndex[] nearRefDataArray)
  RVA=0x09F0A39C  token=0x60004DC  System.Void RemoveTeam(System.Int32 teamId)
  RVA=0x09F0A01C  token=0x60004DD  System.Void ChangeParam(System.Int32 teamId, MagicaCloth.BezierParam mass, System.Single velocityInfluence, MagicaCloth.BezierParam structStiffness, System.Boolean useBend, MagicaCloth.BezierParam bendStiffness, System.Boolean useNear, MagicaCloth.BezierParam nearStiffness)
  RVA=0x02B2E2D0  token=0x60004DE  System.Int32 GetIterationCount()
  RVA=0x09F0A5F0  token=0x60004DF  Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle)
  RVA=0x04D89110  token=0x60004E0  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.RestoreRotationConstraint
TYPE:  class
TOKEN: 0x200008B
SIZE:  0x38
EXTENDS: MagicaCloth.PhysicsManagerConstraint
FIELDS:
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.RestoreRotationConstraint.RotationData>dataList  // 0x20
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.ReferenceDataIndex>refDataList  // 0x28
  public            MagicaCloth.FixedNativeList<MagicaCloth.RestoreRotationConstraint.GroupData>groupList  // 0x30
METHODS:
  RVA=0x0442E650  token=0x60004E7  System.Void Create()
  RVA=0x09F0AF58  token=0x60004E8  System.Void Release()
  RVA=0x09F0AAFC  token=0x60004E9  System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam power, System.Single velocityInfluence, MagicaCloth.RestoreRotationConstraint.RotationData[] dataArray, MagicaCloth.ReferenceDataIndex[] refDataArray)
  RVA=0x09F0AFE0  token=0x60004EA  System.Void RemoveTeam(System.Int32 teamId)
  RVA=0x09F0AD78  token=0x60004EB  System.Void ChangeParam(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam power, System.Single velocityInfluence)
  RVA=0x09F0B160  token=0x60004EC  Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle)
  RVA=0x04D89110  token=0x60004ED  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.SpringConstraint
TYPE:  class
TOKEN: 0x200008F
SIZE:  0x28
EXTENDS: MagicaCloth.PhysicsManagerConstraint
FIELDS:
  public            MagicaCloth.FixedNativeList<MagicaCloth.SpringConstraint.GroupData>groupList  // 0x20
METHODS:
  RVA=0x04D2DE20  token=0x60004F0  System.Void Create()
  RVA=0x09F0D550  token=0x60004F1  System.Void Release()
  RVA=0x09F0D324  token=0x60004F2  System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, System.Single spring)
  RVA=0x09F0D598  token=0x60004F3  System.Void RemoveTeam(System.Int32 teamId)
  RVA=0x09F0D3F0  token=0x60004F4  System.Void ChangeParam(System.Int32 teamId, System.Boolean active, System.Single spring)
  RVA=0x09F0D67C  token=0x60004F5  Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle)
  RVA=0x04D89110  token=0x60004F6  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.TriangleBendConstraint
TYPE:  class
TOKEN: 0x2000092
SIZE:  0x48
EXTENDS: MagicaCloth.PhysicsManagerConstraint
FIELDS:
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.TriangleBendConstraint.TriangleBendData>dataList  // 0x20
  private           MagicaCloth.FixedChunkNativeArray<System.Int16>groupIndexList  // 0x28
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.ReferenceDataIndex>refDataList  // 0x30
  private           MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>writeBuffer  // 0x38
  private           MagicaCloth.FixedNativeList<MagicaCloth.TriangleBendConstraint.TriangleBendGroupData>groupList  // 0x40
METHODS:
  RVA=0x0442EA50  token=0x60004F8  System.Void Create()
  RVA=0x09F0ED88  token=0x60004F9  System.Void Release()
  RVA=0x09F0E824  token=0x60004FA  System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, MagicaCloth.ClothParams.Algorithm algorithm, MagicaCloth.BezierParam stiffness, MagicaCloth.TriangleBendConstraint.TriangleBendData[] dataArray, MagicaCloth.ReferenceDataIndex[] refDataArray, System.Int32 writeBufferCount)
  RVA=0x09F0EE54  token=0x60004FB  System.Void RemoveTeam(System.Int32 teamId)
  RVA=0x09F0EB8C  token=0x60004FC  System.Void ChangeParam(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam stiffness)
  RVA=0x09F0F030  token=0x60004FD  Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle)
  RVA=0x04D89110  token=0x60004FE  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.TwistConstraint
TYPE:  class
TOKEN: 0x2000097
SIZE:  0x38
EXTENDS: MagicaCloth.PhysicsManagerConstraint
FIELDS:
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.TwistConstraint.TwistData>dataList  // 0x20
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.ReferenceDataIndex>refDataList  // 0x28
  public            MagicaCloth.FixedNativeList<MagicaCloth.TwistConstraint.GroupData>groupList  // 0x30
METHODS:
  RVA=0x0442E760  token=0x6000503  System.Void Create()
  RVA=0x09F0FB90  token=0x6000504  System.Void Release()
  RVA=0x09F0F828  token=0x6000505  System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, System.Single recoveryPower, MagicaCloth.TwistConstraint.TwistData[] dataArray, MagicaCloth.ReferenceDataIndex[] refArray)
  RVA=0x09F0FC18  token=0x6000506  System.Void RemoveTeam(System.Int32 teamId)
  RVA=0x09F0FA10  token=0x6000507  System.Void ChangeParam(System.Int32 teamId, System.Boolean active, System.Single recoveryPower)
  RVA=0x09F0FD98  token=0x6000508  Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle)
  RVA=0x04D89110  token=0x6000509  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.VolumeConstraint
TYPE:  class
TOKEN: 0x200009B
SIZE:  0x48
EXTENDS: MagicaCloth.PhysicsManagerConstraint
FIELDS:
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.VolumeConstraint.VolumeData>dataList  // 0x20
  private           MagicaCloth.FixedChunkNativeArray<System.Int16>groupIndexList  // 0x28
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.ReferenceDataIndex>refDataList  // 0x30
  private           MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>writeBuffer  // 0x38
  private           MagicaCloth.FixedNativeList<MagicaCloth.VolumeConstraint.GroupData>groupList  // 0x40
METHODS:
  RVA=0x09F11C18  token=0x600050C  System.Void Create()
  RVA=0x09F11DBC  token=0x600050D  System.Void Release()
  RVA=0x09F11690  token=0x600050E  System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam stretchStiffness, MagicaCloth.BezierParam shearStiffness, MagicaCloth.VolumeConstraint.VolumeData[] dataArray, MagicaCloth.ReferenceDataIndex[] refDataArray, System.Int32 writeBufferCount)
  RVA=0x09F11E88  token=0x600050F  System.Void RemoveTeam(System.Int32 teamId)
  RVA=0x09F11A04  token=0x6000510  System.Void ChangeParam(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam stretchStiffness, MagicaCloth.BezierParam shearStiffness)
  RVA=0x02B2E2D0  token=0x6000511  System.Int32 GetIterationCount()
  RVA=0x09F120C4  token=0x6000512  Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle)
  RVA=0x04D89110  token=0x6000513  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.CurveParam
TYPE:  sealed struct
TOKEN: 0x20000A0
SIZE:  0x20
FIELDS:
  public            System.Single                   sval  // 0x10
  public            System.Single                   eval  // 0x14
  public            System.Single                   cval  // 0x18
  public            System.Int32                    useCurve  // 0x1c
METHODS:
  RVA=0x04DA3640  token=0x6000517  System.Void .ctor(System.Single value)
  RVA=0x04DA3650  token=0x6000518  System.Void .ctor(System.Single svalue, System.Single evalue)
  RVA=0x09F12A90  token=0x6000519  System.Void .ctor(MagicaCloth.BezierParam bezier)
  RVA=0x09F129F0  token=0x600051A  System.Void Setup(MagicaCloth.BezierParam bezier)
  RVA=0x09F12988  token=0x600051B  System.Single Evaluate(System.Single t)
END_CLASS

CLASS: MagicaCloth.BaseComponent
TYPE:  abstract class
TOKEN: 0x20000A1
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600051C  MagicaCloth.ComponentType GetComponentType()
  RVA=0x05393520  token=0x600051D  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.ComponentType
TYPE:  sealed struct
TOKEN: 0x20000A2
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MagicaCloth.ComponentType       None  // const
  public    static  MagicaCloth.ComponentType       SphereCollider  // const
  public    static  MagicaCloth.ComponentType       CapsuleCollider  // const
  public    static  MagicaCloth.ComponentType       PlaneCollider  // const
  public    static  MagicaCloth.ComponentType       DirectionalWind  // const
  public    static  MagicaCloth.ComponentType       AreaWind  // const
  public    static  MagicaCloth.ComponentType       RenderDeformer  // const
  public    static  MagicaCloth.ComponentType       VirtualDeformer  // const
  public    static  MagicaCloth.ComponentType       BoneCloth  // const
  public    static  MagicaCloth.ComponentType       BoneSpring  // const
  public    static  MagicaCloth.ComponentType       MeshCloth  // const
  public    static  MagicaCloth.ComponentType       MeshSpring  // const
  public    static  MagicaCloth.ComponentType       Avatar  // const
  public    static  MagicaCloth.ComponentType       AvatarParts  // const
METHODS:
END_CLASS

CLASS: MagicaCloth.PhysicsManagerAccess
TYPE:  abstract class
TOKEN: 0x20000A3
SIZE:  0x18
IMPLEMENTS: System.IDisposable
FIELDS:
  protected         MagicaCloth.MagicaPhysicsManagermanager  // 0x10
PROPERTIES:
  UpdateTime  get=0x0861D7BC
  Particle  get=0x09ED14C0
  Bone  get=0x03233920
  Mesh  get=0x032353B0
  Team  get=0x03235330
  Wind  get=0x09EE69FC
  Component  get=0x09F13E98
  Compute  get=0x09F13EB8
METHODS:
  RVA=0x053908C0  token=0x6000526  System.Void SetParent(MagicaCloth.MagicaPhysicsManager manager)
  RVA=-1  // abstract  token=0x6000527  System.Void Create()
  RVA=-1  // abstract  token=0x6000528  System.Void Dispose()
  RVA=0x041E1670  token=0x6000529  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.PhysicsManagerBoneData
TYPE:  class
TOKEN: 0x20000A4
SIZE:  0xD0
EXTENDS: MagicaCloth.PhysicsManagerAccess
FIELDS:
  public    static  System.Byte                     Flag_Reset  // const
  public    static  System.Byte                     Flag_Restore  // const
  public    static  System.Byte                     Flag_Write  // const
  public            MagicaCloth.FixedTransformAccessArrayboneList  // 0x18
  public            MagicaCloth.FixedNativeList<System.Byte>boneFlagList  // 0x20
  public            MagicaCloth.FixedNativeList<Unity.Mathematics.float3>bonePosList  // 0x28
  public            MagicaCloth.FixedNativeList<Unity.Mathematics.quaternion>boneRotList  // 0x30
  public            MagicaCloth.FixedNativeList<Unity.Mathematics.float3>boneSclList  // 0x38
  public            MagicaCloth.FixedNativeList<System.Int32>boneParentIndexList  // 0x40
  public            MagicaCloth.FixedNativeList<Unity.Mathematics.float3>basePosList  // 0x48
  public            MagicaCloth.FixedNativeList<Unity.Mathematics.quaternion>baseRotList  // 0x50
  public            MagicaCloth.FixedNativeList<System.Int16>boneUnityPhysicsList  // 0x58
  public            MagicaCloth.FixedNativeList<Unity.Mathematics.float3>futurePosList  // 0x60
  public            MagicaCloth.FixedNativeList<Unity.Mathematics.quaternion>futureRotList  // 0x68
  public            MagicaCloth.FixedTransformAccessArrayrestoreBoneList  // 0x70
  public            MagicaCloth.FixedNativeList<Unity.Mathematics.float3>restoreBoneLocalPosList  // 0x78
  public            MagicaCloth.FixedNativeList<Unity.Mathematics.quaternion>restoreBoneLocalRotList  // 0x80
  public            MagicaCloth.FixedNativeList<System.Int32>restoreBoneIndexList  // 0x88
  public            MagicaCloth.FixedTransformAccessArraywriteBoneList  // 0x90
  public            MagicaCloth.FixedNativeList<System.Int32>writeBoneIndexList  // 0x98
  public            MagicaCloth.ExNativeMultiHashMap<System.Int32,System.Int32>writeBoneParticleIndexMap  // 0xa0
  private           System.Collections.Generic.Dictionary<System.Int32,System.Int32>boneToWriteIndexDict  // 0xa8
  public            MagicaCloth.FixedNativeList<Unity.Mathematics.float3>writeBonePosList  // 0xb0
  public            MagicaCloth.FixedNativeList<Unity.Mathematics.quaternion>writeBoneRotList  // 0xb8
  private           System.Boolean                  <hasBoneChanged>k__BackingField  // 0xc0
  private           UnityEngine.Profiling.CustomSampler<SamplerReadBoneScale>k__BackingField  // 0xc8
PROPERTIES:
  hasBoneChanged  get=0x04D91920  set=0x04D91990
  SamplerReadBoneScale  get=0x04D8D1D0  set=0x06AC5B30
  RestoreBoneCount  get=0x09F155E8
  ReadBoneCount  get=0x09F155C8
  WriteBoneCount  get=0x03233870
METHODS:
  RVA=0x03E876D0  token=0x600052E  System.Void Create()
  RVA=0x09F14958  token=0x600052F  System.Void Dispose()
  RVA=0x09F14348  token=0x6000530  System.Int32 AddRestoreBone(UnityEngine.Transform target, Unity.Mathematics.float3 lpos, Unity.Mathematics.quaternion lrot, System.Int32 boneIndex)
  RVA=0x09F1533C  token=0x6000531  System.Void RemoveRestoreBone(System.Int32 restoreBoneIndex)
  RVA=0x09F13ED8  token=0x6000533  System.Int32 AddBone(UnityEngine.Transform target, System.Int32 pindex, System.Boolean addParent)
  RVA=0x09F1508C  token=0x6000534  System.Boolean RemoveBone(System.Int32 boneIndex, System.Int32 pindex)
  RVA=0x09F1443C  token=0x6000535  System.Void ChangeUnityPhysicsCount(System.Int32 boneIndex, System.Boolean sw)
  RVA=0x09F15560  token=0x6000536  System.Void ResetFuturePrediction(System.Int32 boneIndex)
  RVA=0x09F153D8  token=0x6000539  System.Void ResetBoneFromTransform(System.Boolean fixedUpdate)
  RVA=0x09F14BB0  token=0x600053A  System.Void ReadBoneFromTransform()
  RVA=0x09F144B4  token=0x600053B  System.Void ConvertWorldToLocal()
  RVA=0x03233800  token=0x600053C  System.Void WriteBoneToTransform(System.Int32 bufferIndex)
  RVA=0x09F146A4  token=0x600053D  System.Void CopyBoneBuffer()
  RVA=0x046D7630  token=0x600053E  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.PhysicsManagerComponent
TYPE:  class
TOKEN: 0x20000AD
SIZE:  0x28
EXTENDS: MagicaCloth.PhysicsManagerAccess
FIELDS:
  private   readonly System.Collections.Generic.HashSet<MagicaCloth.CoreComponent>componentSet  // 0x18
  private           System.Collections.Generic.HashSet<MagicaCloth.ParticleComponent>dataUpdateParticleSet  // 0x20
PROPERTIES:
  ComponentCount  get=0x09F159FC
METHODS:
  RVA=0x041E1670  token=0x6000547  System.Void Create()
  RVA=0x041E1670  token=0x6000548  System.Void Dispose()
  RVA=0x09F1577C  token=0x600054A  System.Collections.Generic.List<MagicaCloth.CoreComponent> GetComponentList()
  RVA=0x09F1562C  token=0x600054B  System.Void ComponentAction(System.Action<MagicaCloth.CoreComponent> act)
  RVA=0x09F15898  token=0x600054C  System.Void UpdateComponentStatus()
  RVA=0x09F15608  token=0x600054D  System.Void AddComponent(MagicaCloth.CoreComponent comp)
  RVA=0x09F157F0  token=0x600054E  System.Void RemoveComponent(MagicaCloth.CoreComponent comp)
  RVA=0x09F15844  token=0x600054F  System.Void ReserveDataUpdateParticleComponent(MagicaCloth.ParticleComponent comp)
  RVA=0x03233710  token=0x6000550  System.Void DataUpdateParticleComponent()
  RVA=0x046D7730  token=0x6000551  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.PhysicsManagerCompute
TYPE:  class
TOKEN: 0x20000AE
SIZE:  0xF8
EXTENDS: MagicaCloth.PhysicsManagerAccess
FIELDS:
  private           System.Int32                    solverIteration  // 0x18
  private           System.Collections.Generic.List<MagicaCloth.PhysicsManagerConstraint>constraints  // 0x20
  private           MagicaCloth.ClampPositionConstraint<ClampPosition>k__BackingField  // 0x28
  private           MagicaCloth.ClampDistanceConstraint<ClampDistance>k__BackingField  // 0x30
  private           MagicaCloth.ClampRotationConstraint<ClampRotation>k__BackingField  // 0x38
  private           MagicaCloth.SpringConstraint    <Spring>k__BackingField  // 0x40
  private           MagicaCloth.RestoreDistanceConstraint<RestoreDistance>k__BackingField  // 0x48
  private           MagicaCloth.RestoreRotationConstraint<RestoreRotation>k__BackingField  // 0x50
  private           MagicaCloth.TriangleBendConstraint<TriangleBend>k__BackingField  // 0x58
  private           MagicaCloth.ColliderCollisionConstraint<Collision>k__BackingField  // 0x60
  private           MagicaCloth.PenetrationConstraint<Penetration>k__BackingField  // 0x68
  private           MagicaCloth.ColliderExtrusionConstraint<ColliderExtrusion>k__BackingField  // 0x70
  private           MagicaCloth.TwistConstraint     <Twist>k__BackingField  // 0x78
  private           MagicaCloth.CompositeRotationConstraint<CompositeRotation>k__BackingField  // 0x80
  private           System.Collections.Generic.List<MagicaCloth.PhysicsManagerWorker>workers  // 0x88
  private           MagicaCloth.RenderMeshWorker    <RenderMeshWorker>k__BackingField  // 0x90
  private           MagicaCloth.VirtualMeshWorker   <VirtualMeshWorker>k__BackingField  // 0x98
  private           MagicaCloth.MeshParticleWorker  <MeshParticleWorker>k__BackingField  // 0xa0
  private           MagicaCloth.SpringMeshWorker    <SpringMeshWorker>k__BackingField  // 0xa8
  private           MagicaCloth.AdjustRotationWorker<AdjustRotationWorker>k__BackingField  // 0xb0
  private           MagicaCloth.LineWorker          <LineWorker>k__BackingField  // 0xb8
  private           MagicaCloth.TriangleWorker      <TriangleWorker>k__BackingField  // 0xc0
  private           MagicaCloth.BaseSkinningWorker  <BaseSkinningWorker>k__BackingField  // 0xc8
  private           Unity.Jobs.JobHandle            jobHandle  // 0xd0
  private           System.Boolean                  runMasterJob  // 0xe0
  private           System.Int32                    swapIndex  // 0xe4
  private           UnityEngine.Profiling.CustomSampler<SamplerCalcMesh>k__BackingField  // 0xe8
  private           UnityEngine.Profiling.CustomSampler<SamplerWriteMesh>k__BackingField  // 0xf0
PROPERTIES:
  ClampPosition  get=0x04D86240  set=0x02FDB880
  ClampDistance  get=0x011F36E0  set=0x05396200
  ClampRotation  get=0x04D85A50  set=0x046A91B0
  Spring  get=0x04D85A60  set=0x042B4AE0
  RestoreDistance  get=0x04D86200  set=0x035A41A0
  RestoreRotation  get=0x04D86270  set=0x02FDAEA0
  TriangleBend  get=0x02E56440  set=0x036E5590
  Collision  get=0x04D86280  set=0x063CC164
  Penetration  get=0x04D86230  set=0x0485F170
  ColliderExtrusion  get=0x04D862B0  set=0x063AD760
  Twist  get=0x04D862A0  set=0x06402320
  CompositeRotation  get=0x04D86260  set=0x04CD9970
  RenderMeshWorker  get=0x04D86220  set=0x06402330
  VirtualMeshWorker  get=0x04D85EE0  set=0x06352D0C
  MeshParticleWorker  get=0x04D86210  set=0x05391334
  SpringMeshWorker  get=0x04D86290  set=0x04D84CC0
  AdjustRotationWorker  get=0x04D86250  set=0x04D84CE0
  LineWorker  get=0x04D85EF0  set=0x06AC5AF4
  TriangleWorker  get=0x04D892F0  set=0x06AC5ACC
  BaseSkinningWorker  get=0x04D8D1D0  set=0x06AC5B30
  SamplerCalcMesh  get=0x04D88590  set=0x06AC5B1C
  SamplerWriteMesh  get=0x04D8D1A0  set=0x06AC5AE0
  MasterJob  get=0x04D9E7C0  set=0x04DA12C0
  SwapIndex  get=0x04DA3670
METHODS:
  RVA=0x03B92860  token=0x600057E  System.Void Create()
  RVA=0x09F15A3C  token=0x600057F  System.Void Dispose()
  RVA=0x09F16098  token=0x6000580  System.Void RemoveTeam(System.Int32 teamId)
  RVA=0x09F17034  token=0x6000581  System.Void UpdateRestoreBone(MagicaCloth.PhysicsTeam.TeamUpdateMode updateMode)
  RVA=0x03233CB0  token=0x6000582  System.Void UpdateReadBone()
  RVA=0x03233D00  token=0x6000583  System.Void UpdateTeamAlways()
  RVA=0x03233D40  token=0x6000584  System.Void UpdateStartSimulation(MagicaCloth.UpdateTimeManager update)
  RVA=0x03233C10  token=0x6000585  System.Void UpdateCompleteSimulation()
  RVA=0x032338C0  token=0x6000586  System.Void UpdateWriteBone()
  RVA=0x032352A0  token=0x6000587  System.Void MeshCalculation()
  RVA=0x03233BA0  token=0x6000588  System.Void NormalWritingMesh()
  RVA=0x09F16FB4  token=0x6000589  System.Void UpdateReadWriteBone()
  RVA=0x09F17204  token=0x600058A  System.Void UpdateSyncBuffer()
  RVA=0x09F17088  token=0x600058B  System.Void UpdateSwapBuffer()
  RVA=0x04DA3660  token=0x600058E  System.Void InitJob()
  RVA=0x09F162C4  token=0x600058F  System.Void ScheduleJob()
  RVA=0x035BC9F0  token=0x6000590  System.Void CompleteJob()
  RVA=0x09F162CC  token=0x6000592  System.Void UpdatePhysics(System.Int32 updateCount, System.Int32 runCount, System.Single updatePower, System.Single updateDeltaTime)
  RVA=0x09F15C60  token=0x6000593  System.Void PostUpdatePhysics(System.Single updateDeltaTime)
  RVA=0x09F17330  token=0x6000594  System.Void WarmupWorker()
  RVA=0x046D77F0  token=0x6000595  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.PhysicsManagerConstraint
TYPE:  abstract class
TOKEN: 0x20000B2
SIZE:  0x20
FIELDS:
  public            System.Int32                    iteration  // 0x10
  private           MagicaCloth.MagicaPhysicsManager<Manager>k__BackingField  // 0x18
PROPERTIES:
  Manager  get=0x0385B100  set=0x05392C40
METHODS:
  RVA=0x03B93E60  token=0x600059C  System.Void Init(MagicaCloth.MagicaPhysicsManager manager)
  RVA=-1  // abstract  token=0x600059D  System.Void Create()
  RVA=-1  // abstract  token=0x600059E  System.Void RemoveTeam(System.Int32 teamId)
  RVA=-1  // abstract  token=0x600059F  System.Void Release()
  RVA=0x02B2E2D0  token=0x60005A0  System.Int32 GetIterationCount()
  RVA=-1  // abstract  token=0x60005A1  Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle)
  RVA=0x04D89110  token=0x60005A2  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.PhysicsManagerMeshData
TYPE:  class
TOKEN: 0x20000B3
SIZE:  0x188
EXTENDS: MagicaCloth.PhysicsManagerAccess
FIELDS:
  public    static  System.UInt32                   MeshFlag_Active  // const
  public    static  System.UInt32                   MeshFlag_Skinning  // const
  public    static  System.UInt32                   Meshflag_CalcNormal  // const
  public    static  System.UInt32                   Meshflag_CalcTangent  // const
  public    static  System.UInt32                   Meshflag_Pause  // const
  public    static  System.UInt32                   MeshFlag_ExistNormals  // const
  public    static  System.UInt32                   MeshFlag_ExistTangents  // const
  public    static  System.UInt32                   MeshFlag_ExistWeights  // const
  public    static  System.UInt32                   MeshFlag_UpdateUseVertexFront  // const
  public    static  System.UInt32                   MeshFlag_UpdateUseVertexBack  // const
  public    static  System.UInt32                   MeshFlag_FasterWrite  // const
  public    static  System.UInt32                   MeshFlag_MeshLink  // const
  public            MagicaCloth.FixedNativeList<MagicaCloth.PhysicsManagerMeshData.SharedVirtualMeshInfo>sharedVirtualMeshInfoList  // 0x18
  public            System.Collections.Generic.Dictionary<System.Int32,System.Int32>sharedVirtualMeshIdToIndexDict  // 0x20
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float2>sharedVirtualUvList  // 0x28
  public            MagicaCloth.FixedChunkNativeArray<System.UInt32>sharedVirtualVertexInfoList  // 0x30
  public            MagicaCloth.FixedChunkNativeArray<MagicaCloth.MeshData.VertexWeight>sharedVirtualWeightList  // 0x38
  public            MagicaCloth.FixedChunkNativeArray<System.Int32>sharedVirtualTriangleList  // 0x40
  public            MagicaCloth.FixedChunkNativeArray<System.UInt32>sharedVirtualVertexToTriangleInfoList  // 0x48
  public            MagicaCloth.FixedChunkNativeArray<System.Int32>sharedVirtualVertexToTriangleIndexList  // 0x50
  public    static  System.Byte                     VirtualVertexFlag_Use  // const
  public            MagicaCloth.FixedNativeList<MagicaCloth.PhysicsManagerMeshData.VirtualMeshInfo>virtualMeshInfoList  // 0x58
  public            MagicaCloth.FixedChunkNativeArray<System.Int16>virtualVertexMeshIndexList  // 0x60
  public            MagicaCloth.FixedChunkNativeArray<System.Byte>virtualVertexUseList  // 0x68
  public            MagicaCloth.FixedChunkNativeArray<System.Byte>virtualVertexFixList  // 0x70
  public            MagicaCloth.FixedChunkNativeArray<System.Byte>virtualVertexFlagList  // 0x78
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>virtualPosList  // 0x80
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.quaternion>virtualRotList  // 0x88
  public            MagicaCloth.FixedChunkNativeArray<System.Int32>virtualTransformIndexList  // 0x90
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>virtualTriangleNormalList  // 0x98
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>virtualTriangleTangentList  // 0xa0
  public            MagicaCloth.FixedChunkNativeArray<System.UInt16>virtualTriangleMeshIndexList  // 0xa8
  public            MagicaCloth.FixedNativeList<MagicaCloth.PhysicsManagerMeshData.SharedChildMeshInfo>sharedChildMeshInfoList  // 0xb0
  public            System.Collections.Generic.Dictionary<System.Int64,System.Int32>sharedChildMeshIdToSharedVirtualMeshIndexDict  // 0xb8
  public            MagicaCloth.FixedChunkNativeArray<System.UInt32>sharedChildVertexInfoList  // 0xc0
  public            MagicaCloth.FixedChunkNativeArray<MagicaCloth.MeshData.VertexWeight>sharedChildWeightList  // 0xc8
  public            MagicaCloth.FixedNativeList<MagicaCloth.PhysicsManagerMeshData.SharedRenderMeshInfo>sharedRenderMeshInfoList  // 0xd0
  public            System.Collections.Generic.Dictionary<System.Int32,System.Int32>sharedRenderMeshIdToIndexDict  // 0xd8
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>sharedRenderVertices  // 0xe0
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>sharedRenderNormals  // 0xe8
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float4>sharedRenderTangents  // 0xf0
  public            MagicaCloth.FixedChunkNativeArray<System.Byte>sharedBonesPerVertexList  // 0xf8
  public            MagicaCloth.FixedChunkNativeArray<System.Int32>sharedBonesPerVertexStartList  // 0x100
  public            MagicaCloth.FixedChunkNativeArray<UnityEngine.BoneWeight1>sharedBoneWeightList  // 0x108
  public    static  System.UInt32                   RenderVertexFlag_Use  // const
  public    static  System.Int32                    MaxRenderMeshLinkCount  // const
  public            MagicaCloth.FixedNativeList<MagicaCloth.PhysicsManagerMeshData.RenderMeshInfo>renderMeshInfoList  // 0x110
  public    static  System.UInt32                   RenderStateFlag_Use  // const
  public    static  System.UInt32                   RenderStateFlag_ExistNormal  // const
  public    static  System.UInt32                   RenderStateFlag_ExistTangent  // const
  public    static  System.UInt32                   RenderStateFlag_FasterWrite  // const
  public    static  System.UInt32                   RenderStateFlag_DelayedCalculated  // const
  public            System.Collections.Generic.Dictionary<System.Int32,MagicaCloth.PhysicsManagerMeshData.RenderMeshState>renderMeshStateDict  // 0x118
  public            MagicaCloth.FixedChunkNativeArray<System.UInt32>renderVertexFlagList  // 0x120
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>renderPosList  // 0x128
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>renderNormalList  // 0x130
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float4>renderTangentList  // 0x138
  public            MagicaCloth.FixedChunkNativeArray<UnityEngine.BoneWeight1>renderBoneWeightList  // 0x140
  private           MagicaCloth.DoubleComputeBuffer<Unity.Mathematics.float3>renderPosBuffer  // 0x148
  private           MagicaCloth.DoubleComputeBuffer<Unity.Mathematics.float3>renderNormalBuffer  // 0x150
  private           UnityEngine.ComputeBuffer       emptyByteAddressBuffer  // 0x158
  private           System.Boolean                  isBeginWrite  // 0x160
  private           System.Collections.Generic.HashSet<MagicaCloth.BaseMeshDeformer>renderMeshSet  // 0x168
  private           System.Collections.Generic.List<MagicaCloth.RenderMeshDeformer>normalWriteList  // 0x170
  private           System.Collections.Generic.List<MagicaCloth.RenderMeshDeformer>fasterWritePositionList  // 0x178
  private           System.Collections.Generic.List<MagicaCloth.RenderMeshDeformer>fasterWritePositionNormalList  // 0x180
PROPERTIES:
  SharedVirtualMeshCount  get=0x09F1DC8C
  VirtualMeshCount  get=0x03233AC0
  VirtualMeshVertexCount  get=0x09F1DE68
  VirtualMeshTriangleCount  get=0x09F1DD78
  VirtualMeshVertexUseCount  get=0x09F1DEE4
  VirtualMeshUseCount  get=0x09F1DDF4
  VirtualMeshPauseCount  get=0x09F1DCD4
  SharedRenderMeshCount  get=0x09F1DC44
  SharedChildMeshCount  get=0x09F1DBFC
  RenderMeshCount  get=0x09F1D6F4
  RenderMeshVertexCount  get=0x09F1D9E0
  RenderMeshUseCount  get=0x09F1D888
  RenderMeshVertexUseCount  get=0x09F1DAB8
  RenderMeshPauseCount  get=0x09F1D73C
METHODS:
  RVA=0x0442EE60  token=0x60005A3  System.Void Create()
  RVA=0x09F19D4C  token=0x60005A4  System.Void Dispose()
  RVA=0x09F173A0  token=0x60005A5  System.Void AddMesh(MagicaCloth.BaseMeshDeformer bmesh)
  RVA=0x09F1AECC  token=0x60005A6  System.Void RemoveMesh(MagicaCloth.BaseMeshDeformer bmesh)
  RVA=0x09F18438  token=0x60005A7  System.Int32 AddVirtualMesh(System.Int32 uid, System.Int32 vertexCount, System.Int32 weightCount, System.Int32 boneCount, System.Int32 triangleCount, System.Int32 vertexToTriangleIndexCount, UnityEngine.Transform transform)
  RVA=0x09F1A728  token=0x60005A8  System.Boolean IsEmptySharedVirtualMesh(System.Int32 uid)
  RVA=0x09F1CD04  token=0x60005A9  System.Void SetSharedVirtualMeshData(System.Int32 virtualMeshIndex, System.UInt32[] sharedVertexInfoList, MagicaCloth.MeshData.VertexWeight[] sharedWeightList, UnityEngine.Vector2[] sharedUv, System.Int32[] sharedTriangles, System.UInt32[] vertexToTriangleInfoList, System.Int32[] vertexToTriangleIndexList)
  RVA=0x09F1BCC8  token=0x60005AA  System.Void RemoveVirtualMesh(System.Int32 virtualMeshIndex)
  RVA=0x09F1A210  token=0x60005AB  System.Boolean ExistsVirtualMesh(System.Int32 virtualMeshIndex)
  RVA=0x09F1A4D4  token=0x60005AC  MagicaCloth.PhysicsManagerMeshData.VirtualMeshInfo GetVirtualMeshInfo(System.Int32 virtualMeshIndex)
  RVA=0x09F1A8DC  token=0x60005AD  System.Boolean IsUseVirtualMesh(System.Int32 virtualMeshIndex)
  RVA=0x09F1A618  token=0x60005AE  System.Boolean IsActiveVirtualMesh(System.Int32 virtualMeshIndex)
  RVA=0x09F1CFA4  token=0x60005AF  System.Void SetVirtualMeshActive(System.Int32 virtualMeshIndex, System.Boolean sw)
  RVA=0x09F17FF8  token=0x60005B0  System.Void AddUseVirtualMesh(System.Int32 virtualMeshIndex)
  RVA=0x09F1B888  token=0x60005B1  System.Void RemoveUseVirtualMesh(System.Int32 virtualMeshIndex)
  RVA=0x09F180D4  token=0x60005B2  System.Boolean AddUseVirtualVertex(System.Int32 virtualMeshIndex, System.Int32 vindex, System.Boolean fix)
  RVA=0x09F1B964  token=0x60005B3  System.Boolean RemoveUseVirtualVertex(System.Int32 virtualMeshIndex, System.Int32 vindex, System.Boolean fix)
  RVA=0x09F19688  token=0x60005B4  System.Void CopyToVirtualMeshWorldData(System.Int32 virtualMeshIndex, UnityEngine.Vector3[] vertices, UnityEngine.Vector3[] normals, UnityEngine.Vector3[] tangents)
  RVA=0x09F18248  token=0x60005B5  System.Void AddVirtualMeshBone(System.Int32 virtualMeshIndex, System.Collections.Generic.List<UnityEngine.Transform> boneList)
  RVA=0x09F1BAD8  token=0x60005B6  System.Void RemoveVirtualMeshBone(System.Int32 virtualMeshIndex)
  RVA=0x09F1C194  token=0x60005B7  System.Void ResetFuturePredictionVirtualMeshBone(System.Int32 virtualMeshIndex)
  RVA=0x09F18B1C  token=0x60005B8  System.Void ChangeVirtualMeshUseUnityPhysics(System.Int32 virtualMeshIndex, System.Boolean sw)
  RVA=0x09F1D08C  token=0x60005B9  System.Void SetVirtualMeshFlag(System.Int32 virtualMeshIndex, System.UInt32 flag, System.Boolean sw)
  RVA=0x09F17BA8  token=0x60005C1  System.Int32 AddSharedChildMesh(System.Int64 cuid, System.Int32 virtualMeshIndex, System.Int32 vertexCount, System.Int32 weightCount)
  RVA=0x09F1A674  token=0x60005C2  System.Boolean IsEmptySharedChildMesh(System.Int64 cuid)
  RVA=0x09F1CBEC  token=0x60005C3  System.Void SetSharedChildMeshData(System.Int32 sharedMeshIndex, System.UInt32[] sharedVertexInfoList, MagicaCloth.MeshData.VertexWeight[] sharedVertexWeightList)
  RVA=0x09F1B554  token=0x60005C4  System.Void RemoveSharedChildMesh(System.Int32 sharedChildMeshIndex)
  RVA=0x09F17588  token=0x60005C7  System.Int32 AddRenderMesh(System.Int32 uid, System.Boolean isSkinning, System.Boolean isFasterWrite, UnityEngine.Vector3 baseScale, System.Int32 vertexCount, System.Int32 rendererBoneIndex, System.Int32 boneWeightCount)
  RVA=0x09F1D3B0  token=0x60005C8  System.Void UpdateMeshState(System.Int32 renderMeshIndex)
  RVA=0x09F17418  token=0x60005C9  System.Void AddRenderMeshBone(System.Int32 renderMeshIndex, UnityEngine.Transform rendererTransform)
  RVA=0x09F1A6D0  token=0x60005CA  System.Boolean IsEmptySharedRenderMesh(System.Int32 uid)
  RVA=0x09F1C770  token=0x60005CB  System.Void SetRenderSharedMeshData(System.Int32 renderMeshIndex, System.Boolean isSkinning, UnityEngine.Vector3[] sharedVertices, UnityEngine.Vector3[] sharedNormals, UnityEngine.Vector4[] sharedTangents, Unity.Collections.NativeArray<System.Byte> sharedBonesPerVertex, Unity.Collections.NativeArray<UnityEngine.BoneWeight1> sharedBoneWeights)
  RVA=0x09F1B0B4  token=0x60005CC  System.Void RemoveRenderMesh(System.Int32 renderMeshIndex)
  RVA=0x09F1AF50  token=0x60005CD  System.Void RemoveRenderMeshBone(System.Int32 renderMeshIndex)
  RVA=0x09F189E0  token=0x60005CE  System.Void ChangeRenderMeshUseUnityPhysics(System.Int32 renderMeshIndex, System.Boolean sw)
  RVA=0x09F1A878  token=0x60005CF  System.Boolean IsUseRenderMesh(System.Int32 renderMeshIndex)
  RVA=0x09F1A55C  token=0x60005D0  System.Boolean IsActiveRenderMesh(System.Int32 renderMeshIndex)
  RVA=0x09F1C5F0  token=0x60005D1  System.Void SetRenderMeshFlag(System.Int32 renderMeshIndex, System.UInt32 flag, System.Boolean sw)
  RVA=0x09F1A780  token=0x60005D2  System.Boolean IsRenderMeshFlag(System.Int32 renderMeshIndex, System.UInt32 flag)
  RVA=0x09F1C3FC  token=0x60005D3  System.Void SetRenderMeshActive(System.Int32 renderMeshIndex, System.Boolean sw)
  RVA=0x09F17E38  token=0x60005D4  System.Void AddUseRenderMesh(System.Int32 renderMeshIndex)
  RVA=0x09F1B6C8  token=0x60005D5  System.Void RemoveUseRenderMesh(System.Int32 renderMeshIndex)
  RVA=0x09F1A96C  token=0x60005D6  System.Void LinkRenderMesh(System.Int32 renderMeshIndex, System.Int32 childMeshVertexStart, System.Int32 childMeshWeightStart, System.Int32 virtualMeshVertexStart, System.Int32 sharedVirtualMeshVertexStart)
  RVA=0x09F1D180  token=0x60005D7  System.Void UnlinkRenderMesh(System.Int32 renderMeshIndex, System.Int32 childMeshVertexStart, System.Int32 childMeshWeightStart, System.Int32 virtualMeshVertexStart, System.Int32 sharedVirtualMeshVertexStart)
  RVA=0x09F18F80  token=0x60005D8  System.Void CopyToRenderMeshLocalPositionData(System.Int32 renderMeshIndex, UnityEngine.Mesh mesh, System.Int32 bufferIndex)
  RVA=0x09F18DEC  token=0x60005D9  System.Void CopyToRenderMeshLocalNormalTangentData(System.Int32 renderMeshIndex, UnityEngine.Mesh mesh, System.Int32 bufferIndex, System.Boolean normal, System.Boolean tangent)
  RVA=0x09F18C68  token=0x60005DA  System.Void CopyToRenderMeshBoneWeightData(System.Int32 renderMeshIndex, UnityEngine.Mesh mesh, UnityEngine.Mesh sharedMesh, System.Int32 bufferIndex)
  RVA=0x09F19070  token=0x60005DB  System.Void CopyToRenderMeshWorldData(System.Int32 renderMeshIndex, UnityEngine.Transform target, UnityEngine.Vector3[] vertices, UnityEngine.Vector3[] normals, UnityEngine.Vector3[] tangents)
  RVA=0x09F1A308  token=0x60005DC  System.Collections.Generic.List<System.Int32> GetVertexUseList(System.Int32 renderMeshIndex)
  RVA=0x09F1C294  token=0x60005E2  System.Void SetDelayedCalculatedFlag()
  RVA=0x03233470  token=0x60005E3  System.Void ClearWritingList()
  RVA=0x09F1AB70  token=0x60005E4  System.Void MeshCalculation(System.Int32 bufferIndex)
  RVA=0x09F1AD64  token=0x60005E5  System.Void NormalWriting(System.Int32 bufferIndex)
  RVA=0x09F193E0  token=0x60005E6  System.Void CopyToRenderVertexBuffer(System.Int32 renderMeshIndex, System.Int32 bufferIndex, UnityEngine.GraphicsBuffer vertexBuffer, System.Boolean normal, UnityEngine.ComputeShader compute, System.Int32 kernel, System.Int32 index)
  RVA=0x09F1D468  token=0x60005E7  System.Void UpdateVertexBuffer()
  RVA=0x03233A20  token=0x60005E8  System.Void FinishVertexBuffer()
  RVA=0x09F1A264  token=0x60005E9  System.Void FasterWriting(System.Int32 bufferIndex)
  RVA=0x09F198E4  token=0x60005EA  System.Void DispatchWriting(System.Int32 kernel, System.Collections.Generic.List<MagicaCloth.RenderMeshDeformer> rlist, System.Int32 bufferIndex)
  RVA=0x046D78C0  token=0x60005EB  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.PhysicsManagerParticleData
TYPE:  class
TOKEN: 0x20000BC
SIZE:  0x100
EXTENDS: MagicaCloth.PhysicsManagerAccess
FIELDS:
  public    static  System.UInt32                   Flag_Enable  // const
  public    static  System.UInt32                   Flag_Kinematic  // const
  public    static  System.UInt32                   Flag_Hold  // const
  public    static  System.UInt32                   Flag_Collider  // const
  public    static  System.UInt32                   Flag_Plane  // const
  public    static  System.UInt32                   Flag_CapsuleX  // const
  public    static  System.UInt32                   Flag_CapsuleY  // const
  public    static  System.UInt32                   Flag_CapsuleZ  // const
  public    static  System.UInt32                   Flag_Box  // const
  public    static  System.UInt32                   Flag_TriangleRotation  // const
  public    static  System.UInt32                   Flag_Transform_Read_Pos  // const
  public    static  System.UInt32                   Flag_Transform_Read_Rot  // const
  public    static  System.UInt32                   Flag_Transform_Read_Base  // const
  public    static  System.UInt32                   Flag_Transform_Read_Local  // const
  public    static  System.UInt32                   Flag_Transform_Read_Scl  // const
  public    static  System.UInt32                   Flag_Transform_Write  // const
  public    static  System.UInt32                   Flag_Transform_Restore  // const
  public    static  System.UInt32                   Flag_Transform_UnityPhysics  // const
  public    static  System.UInt32                   Flag_Transform_Parent  // const
  public    static  System.UInt32                   Flag_Step_Update  // const
  public    static  System.UInt32                   Flag_Reset_Position  // const
  public            MagicaCloth.FixedChunkNativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag>flagList  // 0x18
  public            MagicaCloth.FixedChunkNativeArray<System.Int32>teamIdList  // 0x20
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>posList  // 0x28
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.quaternion>rotList  // 0x30
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>oldPosList  // 0x38
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.quaternion>oldRotList  // 0x40
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>oldSlowPosList  // 0x48
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>localPosList  // 0x50
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>basePosList  // 0x58
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.quaternion>baseRotList  // 0x60
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>snapBasePosList  // 0x68
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.quaternion>snapBaseRotList  // 0x70
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>oldBasePosList  // 0x78
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.quaternion>oldBaseRotList  // 0x80
  public            MagicaCloth.FixedChunkNativeArray<System.Single>depthList  // 0x88
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>radiusList  // 0x90
  public            MagicaCloth.FixedChunkNativeArray<System.Int32>restoreTransformIndexList  // 0x98
  public            MagicaCloth.FixedChunkNativeArray<System.Int32>transformIndexList  // 0xa0
  public            MagicaCloth.FixedChunkNativeArray<System.Single>frictionList  // 0xa8
  public            MagicaCloth.FixedChunkNativeArray<System.Single>staticFrictionList  // 0xb0
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>velocityList  // 0xb8
  public            MagicaCloth.FixedChunkNativeArray<System.Int32>collisionLinkIdList  // 0xc0
  public            MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>collisionNormalList  // 0xc8
  private           MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>nextPos0List  // 0xd0
  private           MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3>nextPos1List  // 0xd8
  private           System.Int32                    nextPosSwitch  // 0xe0
  private           MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.quaternion>nextRot0List  // 0xe8
  private           MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.quaternion>nextRot1List  // 0xf0
  private           System.Int32                    nextRotSwitch  // 0xf8
  private           System.Int32                    colliderCount  // 0xfc
PROPERTIES:
  Count  get=0x09F1FB70
  Length  get=0x09F1FBD8
  ColliderCount  get=0x04D8F940
  InNextPosList  get=0x09F1FBA0
  OutNextPosList  get=0x09F1FBEC
  InNextRotList  get=0x09F1FBBC
  OutNextRotList  get=0x09F1FC08
METHODS:
  RVA=0x0442F840  token=0x6000605  System.Void Create()
  RVA=0x09F1E87C  token=0x6000606  System.Void Dispose()
  RVA=0x0442FFA0  token=0x6000607  MagicaCloth.ChunkData CreateParticle(System.UInt32 flag, System.Int32 team, Unity.Mathematics.float3 wpos, Unity.Mathematics.quaternion wrot, System.Single depth, Unity.Mathematics.float3 radius, Unity.Mathematics.float3 targetLocalPos)
  RVA=0x09F1DF6C  token=0x6000608  MagicaCloth.ChunkData CreateParticle(System.Int32 team, System.Int32 count, System.Func<System.Int32,System.UInt32> funcFlag, System.Func<System.Int32,Unity.Mathematics.float3> funcWpos, System.Func<System.Int32,Unity.Mathematics.quaternion> funcWrot, System.Func<System.Int32,System.Single> funcDepth, System.Func<System.Int32,Unity.Mathematics.float3> funcRadius, System.Func<System.Int32,Unity.Mathematics.float3> funcTargetLocalPos)
  RVA=0x09F1EC50  token=0x6000609  System.Void RemoveParticle(MagicaCloth.ChunkData c)
  RVA=0x04430570  token=0x600060A  System.Void SetEnable(MagicaCloth.ChunkData c, System.Boolean sw, System.Func<System.Int32,UnityEngine.Transform> funcTarget, System.Func<System.Int32,Unity.Mathematics.float3> funcLpos, System.Func<System.Int32,Unity.Mathematics.quaternion> funcLrot)
  RVA=0x09F1F158  token=0x600060B  System.Void SetRadius(System.Int32 index, Unity.Mathematics.float3 radius)
  RVA=0x09F1F0C4  token=0x600060C  System.Void SetLocalPos(System.Int32 index, UnityEngine.Vector3 lpos)
  RVA=0x09F1EBE8  token=0x6000610  System.Boolean IsValid(System.Int32 index)
  RVA=0x09F1EB98  token=0x6000611  System.Int32 GetTeamId(System.Int32 index)
  RVA=0x09F1F054  token=0x6000612  System.Void ResetFuturePredictionTransform(System.Int32 index)
  RVA=0x09F1F004  token=0x6000613  System.Void ResetFuturePredictionTransform(MagicaCloth.ChunkData c)
  RVA=0x04DA3680  token=0x6000616  System.Void SwitchingNextPosList()
  RVA=0x04DA36A0  token=0x6000619  System.Void SwitchingNextRotList()
  RVA=0x09F1F1C8  token=0x600061A  System.Void UpdateBoneToParticle()
  RVA=0x09F1F878  token=0x600061B  System.Void UpdateResetParticle()
  RVA=0x09F1F570  token=0x600061C  System.Boolean UpdateParticleToBone()
  RVA=0x041E1670  token=0x600061D  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.PhysicsManagerTeamData
TYPE:  class
TOKEN: 0x20000C1
SIZE:  0x80
EXTENDS: MagicaCloth.PhysicsManagerAccess
FIELDS:
  public    static  System.UInt32                   Flag_Enable  // const
  public    static  System.UInt32                   Flag_Interpolate  // const
  public    static  System.UInt32                   Flag_FixedNonRotation  // const
  public    static  System.UInt32                   Flag_AnimatedPose  // const
  public    static  System.UInt32                   Flag_IgnoreClampPositionVelocity  // const
  public    static  System.UInt32                   Flag_Collision  // const
  public    static  System.UInt32                   Flag_AfterCollision  // const
  public    static  System.UInt32                   Flag_UpdatePhysics  // const
  public    static  System.UInt32                   Flag_Pause  // const
  public    static  System.UInt32                   Flag_Reset_WorldInfluence  // const
  public    static  System.UInt32                   Flag_Reset_Position  // const
  public    static  System.UInt32                   Flag_Reset_Keep  // const
  public    static  System.UInt32                   Flag_Wind  // const
  public            MagicaCloth.FixedNativeList<MagicaCloth.PhysicsManagerTeamData.TeamData>teamDataList  // 0x18
  public            MagicaCloth.FixedNativeList<MagicaCloth.CurveParam>teamMassList  // 0x20
  public            MagicaCloth.FixedNativeList<MagicaCloth.CurveParam>teamGravityList  // 0x28
  public            MagicaCloth.FixedNativeList<MagicaCloth.CurveParam>teamDragList  // 0x30
  public            MagicaCloth.FixedNativeList<MagicaCloth.CurveParam>teamMaxVelocityList  // 0x38
  public            MagicaCloth.FixedNativeList<MagicaCloth.CurveParam>teamDepthInfluenceList  // 0x40
  public            MagicaCloth.FixedNativeList<MagicaCloth.PhysicsManagerTeamData.WorldInfluence>teamWorldInfluenceList  // 0x48
  public            MagicaCloth.FixedNativeList<MagicaCloth.PhysicsManagerTeamData.WindInfo>teamWindInfoList  // 0x50
  public            MagicaCloth.FixedMultiNativeList<System.Int32>colliderList  // 0x58
  public            MagicaCloth.FixedMultiNativeList<System.Int32>skinningBoneList  // 0x60
  private           System.Collections.Generic.Dictionary<System.Int32,MagicaCloth.PhysicsTeam>teamComponentDict  // 0x68
  private           System.Int32                    activeTeamCount  // 0x70
  private           System.Int32                    normalUpdateCount  // 0x74
  private           System.Int32                    physicsUpdateCount  // 0x78
PROPERTIES:
  TeamCount  get=0x09F2E5F4
  TeamLength  get=0x09F2E63C
  ActiveTeamCount  get=0x04D868A0
  ColliderCount  get=0x09F2E4B0
  NormalUpdateCount  get=0x04D868B0
  PhysicsUpdateCount  get=0x04D863C0
  PauseCount  get=0x09F2E4FC
METHODS:
  RVA=0x02FC1070  token=0x6000631  System.Void Create()
  RVA=0x09F2B738  token=0x6000632  System.Void Dispose()
  RVA=0x02FC1330  token=0x600063A  System.Int32 CreateTeam(MagicaCloth.PhysicsTeam team, System.UInt32 flag)
  RVA=0x09F2C534  token=0x600063B  System.Void RemoveTeam(System.Int32 teamId)
  RVA=0x09F2D238  token=0x600063C  System.Void SetEnable(System.Int32 teamId, System.Boolean sw)
  RVA=0x09F2BC68  token=0x600063D  System.Boolean IsValid(System.Int32 teamId)
  RVA=0x09F2BC38  token=0x600063E  System.Boolean IsValidData(System.Int32 teamId)
  RVA=0x09F2BAE0  token=0x600063F  System.Boolean IsActive(System.Int32 teamId)
  RVA=0x09F2D38C  token=0x6000640  System.Void SetFlag(System.Int32 teamId, System.UInt32 flag, System.Boolean sw)
  RVA=0x09F2BBA4  token=0x6000641  System.Boolean IsFlag(System.Int32 teamId, System.UInt32 flag)
  RVA=0x09F2DAD4  token=0x6000642  System.Void SetParticleChunk(System.Int32 teamId, MagicaCloth.ChunkData chunk)
  RVA=0x09F2D4CC  token=0x6000643  System.Void SetFriction(System.Int32 teamId, System.Single dynamicFriction, System.Single staticFriction)
  RVA=0x09F2D91C  token=0x6000644  System.Void SetMass(System.Int32 teamId, MagicaCloth.BezierParam mass)
  RVA=0x09F2D764  token=0x6000645  System.Void SetGravity(System.Int32 teamId, MagicaCloth.BezierParam gravity)
  RVA=0x09F2D5CC  token=0x6000646  System.Void SetGravityDirection(System.Int32 teamId, Unity.Mathematics.float3 dir)
  RVA=0x09F2D1E0  token=0x6000647  System.Void SetDrag(System.Int32 teamId, MagicaCloth.BezierParam drag)
  RVA=0x09F2D974  token=0x6000648  System.Void SetMaxVelocity(System.Int32 teamId, MagicaCloth.BezierParam maxVelocity)
  RVA=0x09F2D260  token=0x6000649  System.Void SetExternalForce(System.Int32 teamId, System.Single massInfluence, System.Single windInfluence, System.Single windRandomScale, System.Single windSynchronization)
  RVA=0x09F2D188  token=0x600064A  System.Void SetDepthInfluence(System.Int32 teamId, MagicaCloth.BezierParam depthInfluence)
  RVA=0x09F2E10C  token=0x600064B  System.Void SetWorldInfluence(System.Int32 teamId, System.Single maxSpeed, System.Single maxRotatinSpeed, MagicaCloth.BezierParam moveInfluence, MagicaCloth.BezierParam rotInfluence, System.Boolean resetTeleport, System.Single teleportDistance, System.Single teleportRotation, System.Single resetStabilizationTime, MagicaCloth.ClothParams.TeleportMode teleportMode, System.Boolean useClampRotation, MagicaCloth.BezierParam clampRotation)
  RVA=0x09F2AE20  token=0x600064C  System.Single CalcClampRotationLimit(System.Boolean useClampRotation, MagicaCloth.BezierParam clampRotation)
  RVA=0x09F2E2E8  token=0x600064D  System.Void SetWorldInfluence(System.Int32 teamId, System.Single maxSpeed, System.Single maxRotationSpeed, MagicaCloth.BezierParam moveInfluence, MagicaCloth.BezierParam rotInfluence)
  RVA=0x09F2CC08  token=0x600064E  System.Void SetAfterTeleport(System.Int32 teamId, System.Boolean resetTeleport, System.Single teleportDistance, System.Single teleportRotation, MagicaCloth.ClothParams.TeleportMode teleportMode)
  RVA=0x09F2DD18  token=0x600064F  System.Void SetStabilizationTime(System.Int32 teamId, System.Single resetStabilizationTime)
  RVA=0x09F2CA04  token=0x6000650  System.Void ResetWorldInfluenceTarget(System.Int32 teamId, UnityEngine.Transform target)
  RVA=0x09F2D000  token=0x6000651  System.Void SetClampRotation(System.Int32 teamId, System.Boolean useClampRotation, MagicaCloth.BezierParam clampRotation)
  RVA=0x09F2DBD8  token=0x6000652  System.Void SetSelfCollisionRange(System.Int32 teamId, System.Single range)
  RVA=0x09F2CEA4  token=0x6000653  System.Void SetBoneIndex(System.Int32 teamId, System.Int32 boneIndex, UnityEngine.Vector3 initScale)
  RVA=0x09F2A838  token=0x6000654  System.Void AddColliderParticle(System.Int32 teamId, System.Int32 particleIndex)
  RVA=0x09F2C078  token=0x6000655  System.Void RemoveColliderParticle(System.Int32 teamId, System.Int32 particleIndex)
  RVA=0x09F2C240  token=0x6000656  System.Void RemoveCollider(System.Int32 teamId, MagicaCloth.ColliderComponent collider)
  RVA=0x09F2C63C  token=0x6000657  System.Void ResetFuturePredictionCollidere(System.Int32 teamId)
  RVA=0x09F2AA2C  token=0x6000658  System.Void AddSkinningBoneIndex(System.Int32 teamId, System.Collections.Generic.List<UnityEngine.Transform> boneList)
  RVA=0x09F2C30C  token=0x6000659  System.Void RemoveSkinningBoneIndex(System.Int32 teamId)
  RVA=0x09F2DE78  token=0x600065A  System.Void SetTimeScale(System.Int32 teamId, System.Single timeScale)
  RVA=0x09F2BA24  token=0x600065B  System.Single GetTimeScale(System.Int32 teamId)
  RVA=0x09F2CD9C  token=0x600065C  System.Void SetBlendRatio(System.Int32 teamId, System.Single blendRatio)
  RVA=0x09F2B8AC  token=0x600065D  System.Single GetBlendRatio(System.Int32 teamId)
  RVA=0x09F2D9CC  token=0x600065E  System.Void SetOutPhysicsPoseBlendRatio(System.Int32 teamId, System.Single blendRatio)
  RVA=0x09F2B968  token=0x600065F  System.Single GetOutPhysicsPoseBlendRatio(System.Int32 teamId)
  RVA=0x09F2D7BC  token=0x6000660  System.Void SetImpactForce(System.Int32 teamId, Unity.Mathematics.float3 force, MagicaCloth.PhysicsManagerTeamData.ForceMode mode)
  RVA=0x09F2C810  token=0x6000661  System.Void ResetStabilizationTime(System.Int32 teamId, System.Single resetStabilizationTime)
  RVA=0x09F2DFC0  token=0x6000662  System.Void SetUpdateMode(System.Int32 teamId, MagicaCloth.PhysicsTeam.TeamUpdateMode updateMode)
  RVA=0x09F2B524  token=0x6000663  System.Void ChangeUseUnityPhysics(System.Int32 teamId, System.Boolean sw)
  RVA=0x09F2B408  token=0x6000664  System.Void ChangeParticleUseUnityPhysics(System.Int32 pindex, System.Boolean unityPhysics)
  RVA=0x09F2B1F8  token=0x6000665  System.Void ChangeBoneFlag(System.Int32 teamId, MagicaCloth.PhysicsTeam.TeamCullingMode cullingMode, System.Boolean isCalculation)
  RVA=0x033362D0  token=0x6000666  System.Void EarlyUpdateTeamAlways()
  RVA=0x033364E0  token=0x6000667  System.Void PreUpdateTeamAlways()
  RVA=0x09F2AE7C  token=0x6000668  System.Int32 CalcMaxUpdateCount(System.Int32 ups, System.Single deltaTime, System.Single physicsDeltaTime, System.Single updateDeltaTime)
  RVA=0x09F2BD94  token=0x6000669  System.Void PreUpdateTeamData(System.Single deltaTime, System.Single physicsDeltaTime, System.Single updateDeltaTime, System.Int32 ups, System.Int32 maxUpdateCount)
  RVA=0x09F2BC74  token=0x600066A  System.Void PostUpdateTeamData()
  RVA=0x046D76B0  token=0x600066B  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.PhysicsManagerWindData
TYPE:  class
TOKEN: 0x20000C9
SIZE:  0x20
EXTENDS: MagicaCloth.PhysicsManagerAccess
FIELDS:
  public    static  System.UInt32                   Flag_Enable  // const
  public    static  System.UInt32                   Flag_Addition  // const
  public            MagicaCloth.FixedNativeList<MagicaCloth.PhysicsManagerWindData.WindData>windDataList  // 0x18
PROPERTIES:
  Count  get=0x09F2EEB4
METHODS:
  RVA=0x04D2EDA0  token=0x600067C  System.Void Create()
  RVA=0x09F2E9F4  token=0x600067D  System.Void Dispose()
  RVA=0x09F2E8B8  token=0x600067E  System.Int32 CreateWind(MagicaCloth.PhysicsManagerWindData.WindType windType, MagicaCloth.PhysicsManagerWindData.ShapeType shapeType, Unity.Mathematics.float3 areaSize, System.Boolean addition, System.Single main, System.Single turbulence, System.Single frequency, Unity.Mathematics.float3 direction, MagicaCloth.PhysicsManagerWindData.DirectionType directinType, System.Single areaVolume, System.Single areaLength, MagicaCloth.BezierParam attenuation)
  RVA=0x09F2EA9C  token=0x600067F  System.Void RemoveWind(System.Int32 windId)
  RVA=0x09F2EAF4  token=0x6000680  System.Void SetEnable(System.Int32 windId, System.Boolean sw, UnityEngine.Transform target)
  RVA=0x09F2EA38  token=0x6000681  System.Boolean IsActive(System.Int32 windId)
  RVA=0x09F2EC70  token=0x6000682  System.Void SetFlag(System.Int32 windId, System.UInt32 flag, System.Boolean sw)
  RVA=0x09F2ED44  token=0x6000683  System.Void SetParameter(System.Int32 windId, Unity.Mathematics.float3 areaSize, System.Boolean addition, System.Single main, System.Single turbulence, System.Single frequency, Unity.Mathematics.float3 direction, System.Single areaVolume, System.Single areaLength, MagicaCloth.BezierParam attenuation)
  RVA=0x09F2E67C  token=0x6000685  Unity.Mathematics.float3 CalcWindForce(System.Single time, Unity.Mathematics.float2 noiseBasePos, Unity.Mathematics.float3 mainDir, System.Single main, System.Single turbulence, System.Single frequency, System.Single randomScale)
  RVA=0x041E1670  token=0x6000686  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.PhysicsManagerWorker
TYPE:  abstract class
TOKEN: 0x20000CE
SIZE:  0x18
FIELDS:
  private           MagicaCloth.MagicaPhysicsManager<Manager>k__BackingField  // 0x10
PROPERTIES:
  Manager  get=0x02B2ECC0  set=0x053908C0
METHODS:
  RVA=0x041E1670  token=0x600068D  System.Void Start()
  RVA=0x03B93E90  token=0x600068E  System.Void Init(MagicaCloth.MagicaPhysicsManager manager)
  RVA=-1  // abstract  token=0x600068F  System.Void Create()
  RVA=-1  // abstract  token=0x6000690  System.Void RemoveGroup(System.Int32 group)
  RVA=-1  // abstract  token=0x6000691  System.Void Release()
  RVA=-1  // abstract  token=0x6000692  System.Void Warmup()
  RVA=-1  // abstract  token=0x6000693  Unity.Jobs.JobHandle PreUpdate(Unity.Jobs.JobHandle jobHandle)
  RVA=-1  // abstract  token=0x6000694  Unity.Jobs.JobHandle PostUpdate(Unity.Jobs.JobHandle jobHandle)
  RVA=0x041E1670  token=0x6000695  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.ColliderComponent
TYPE:  abstract class
TOKEN: 0x20000CF
SIZE:  0x38
EXTENDS: MagicaCloth.ParticleComponent
FIELDS:
  protected         System.Boolean                  isGlobal  // 0x28
  private           UnityEngine.Vector3             center  // 0x2c
PROPERTIES:
  Center  get=0x04D90540  set=0x09F25B5C
METHODS:
  RVA=0x04D84ED0  token=0x6000698  System.Void OnInit()
  RVA=0x09F25834  token=0x6000699  System.Void OnDispose()
  RVA=0x09F257E8  token=0x600069A  System.Int32 GetDataHash()
  RVA=-1  // abstract  token=0x600069B  System.Boolean CalcNearPoint(UnityEngine.Vector3 pos, UnityEngine.Vector3& p, UnityEngine.Vector3& dir, UnityEngine.Vector3& d, System.Boolean skinning)
  RVA=0x09F2564C  token=0x600069C  UnityEngine.Vector3 CalcLocalPos(UnityEngine.Vector3 pos)
  RVA=0x09F255E0  token=0x600069D  UnityEngine.Vector3 CalcLocalDir(UnityEngine.Vector3 dir)
  RVA=0x09F2575C  token=0x600069E  MagicaCloth.ChunkData CreateColliderParticle(System.Int32 teamId)
  RVA=0x09F25A70  token=0x600069F  System.Void RemoveColliderParticle(System.Int32 teamId)
  RVA=-1  // abstract  token=0x60006A0  MagicaCloth.ChunkData CreateColliderParticleReal(System.Int32 teamId)
  RVA=0x053982BC  token=0x60006A1  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.MagicaCapsuleCollider
TYPE:  class
TOKEN: 0x20000D0
SIZE:  0x48
EXTENDS: MagicaCloth.ColliderComponent
FIELDS:
  private           MagicaCloth.MagicaCapsuleCollider.Axisaxis  // 0x38
  private           System.Single                   length  // 0x3c
  private           System.Single                   startRadius  // 0x40
  private           System.Single                   endRadius  // 0x44
PROPERTIES:
  AxisMode  get=0x04D863B0  set=0x09F2859C
  Length  get=0x04D863E0  set=0x09F285B4
  StartRadius  get=0x04D863F0  set=0x09F285C0
  EndRadius  get=0x04D86400  set=0x09F285A8
METHODS:
  RVA=0x04DA36C0  token=0x60006A2  MagicaCloth.ComponentType GetComponentType()
  RVA=0x09F28570  token=0x60006A3  System.Void OnValidate()
  RVA=0x09F28040  token=0x60006A4  System.Void DataUpdate()
  RVA=0x09F28390  token=0x60006A5  System.Int32 GetDataHash()
  RVA=0x09F27F0C  token=0x60006AE  MagicaCloth.ChunkData CreateColliderParticleReal(System.Int32 teamId)
  RVA=0x09F28370  token=0x60006AF  System.UInt32 GetCapsuleFlag()
  RVA=0x09F28494  token=0x60006B0  UnityEngine.Vector3 GetLocalDir()
  RVA=0x09F284D8  token=0x60006B1  UnityEngine.Vector3 GetLocalUp()
  RVA=0x09F28514  token=0x60006B2  System.Single GetScale()
  RVA=0x09F27790  token=0x60006B3  System.Boolean CalcNearPoint(UnityEngine.Vector3 pos, UnityEngine.Vector3& p, UnityEngine.Vector3& dir, UnityEngine.Vector3& d, System.Boolean skinning)
  RVA=0x04D778B0  token=0x60006B4  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.MagicaPlaneCollider
TYPE:  class
TOKEN: 0x20000D2
SIZE:  0x38
EXTENDS: MagicaCloth.ColliderComponent
FIELDS:
METHODS:
  RVA=0x04DA36D0  token=0x60006B5  MagicaCloth.ComponentType GetComponentType()
  RVA=0x09F28570  token=0x60006B6  System.Void OnValidate()
  RVA=0x09F288C4  token=0x60006B7  System.Void DataUpdate()
  RVA=0x09F28798  token=0x60006B8  MagicaCloth.ChunkData CreateColliderParticleReal(System.Int32 teamId)
  RVA=0x09F285CC  token=0x60006B9  System.Boolean CalcNearPoint(UnityEngine.Vector3 pos, UnityEngine.Vector3& p, UnityEngine.Vector3& dir, UnityEngine.Vector3& d, System.Boolean skinning)
  RVA=0x053982BC  token=0x60006BA  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.MagicaSphereCollider
TYPE:  class
TOKEN: 0x20000D3
SIZE:  0x40
EXTENDS: MagicaCloth.ColliderComponent
FIELDS:
  private           System.Single                   radius  // 0x38
PROPERTIES:
  Radius  get=0x04D88360  set=0x09F29054
METHODS:
  RVA=0x04D91C30  token=0x60006BB  MagicaCloth.ComponentType GetComponentType()
  RVA=0x09F28570  token=0x60006BC  System.Void OnValidate()
  RVA=0x09F28D70  token=0x60006BD  System.Void DataUpdate()
  RVA=0x09F28FAC  token=0x60006C0  System.Int32 GetDataHash()
  RVA=0x09F28C48  token=0x60006C1  MagicaCloth.ChunkData CreateColliderParticleReal(System.Int32 teamId)
  RVA=0x09F29014  token=0x60006C2  System.Single GetScale()
  RVA=0x09F28A90  token=0x60006C3  System.Boolean CalcNearPoint(UnityEngine.Vector3 pos, UnityEngine.Vector3& p, UnityEngine.Vector3& dir, UnityEngine.Vector3& d, System.Boolean skinning)
  RVA=0x09F29044  token=0x60006C4  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.ParticleComponent
TYPE:  abstract class
TOKEN: 0x20000D4
SIZE:  0x28
EXTENDS: MagicaCloth.BaseComponent
IMPLEMENTS: MagicaCloth.IDataHash
FIELDS:
  protected         System.Collections.Generic.Dictionary<System.Int32,MagicaCloth.ChunkData>particleDict  // 0x18
  protected         MagicaCloth.RuntimeStatus       status  // 0x20
PROPERTIES:
  Status  get=0x04D862C0
  CenterParticleIndex  get=0x09F2A33C
METHODS:
  RVA=-1  // abstract  token=0x60006C6  System.Int32 GetDataHash()
  RVA=0x047FE3A0  token=0x60006C8  System.Void Start()
  RVA=0x047FE350  token=0x60006C9  System.Void OnEnable()
  RVA=0x09EE648C  token=0x60006CA  System.Void OnDisable()
  RVA=0x09F29F90  token=0x60006CB  System.Void OnDestroy()
  RVA=0x047FE3B0  token=0x60006CC  System.Void Init()
  RVA=0x04D396C0  token=0x60006CD  System.Void OnUpdateStatus()
  RVA=0x02FFF600  token=0x60006CE  System.Boolean VerifyData()
  RVA=0x041E1670  token=0x60006CF  System.Void OnInit()
  RVA=0x09F29FC0  token=0x60006D0  System.Void OnDispose()
  RVA=0x041E1670  token=0x60006D1  System.Void OnUpdate()
  RVA=0x0539824C  token=0x60006D2  System.Void OnActive()
  RVA=0x09F29FF0  token=0x60006D3  System.Void OnInactive()
  RVA=0x04CA5CE0  token=0x60006D4  System.Void EnableParticle()
  RVA=0x09F29C18  token=0x60006D5  System.Void DisableParticle()
  RVA=0x09F29E58  token=0x60006D6  System.Void EnableTeamParticle(System.Int32 teamId)
  RVA=0x09F29D20  token=0x60006D7  System.Void DisableTeamParticle(System.Int32 teamId)
  RVA=0x09F2A1D8  token=0x60006D8  System.Void ReserveDataUpdate()
  RVA=0x041E1670  token=0x60006D9  System.Void DataUpdate()
  RVA=0x09F2A254  token=0x60006DA  System.Void UpdateStatus()
  RVA=0x09F299E8  token=0x60006DB  MagicaCloth.ChunkData CreateParticle(System.UInt32 flag, System.Int32 teamId, System.Single depth, Unity.Mathematics.float3 radius, Unity.Mathematics.float3 localPos)
  RVA=0x09F2A128  token=0x60006DC  System.Void RemoveTeamParticle(System.Int32 teamId)
  RVA=0x09F29FF8  token=0x60006DD  System.Void RemoveParticle()
  RVA=0x053953F4  token=0x60006DE  UnityEngine.Transform UserTransform(System.Int32 vindex)
  RVA=0x09F2A274  token=0x60006DF  Unity.Mathematics.float3 UserTransformLocalPosition(System.Int32 vindex)
  RVA=0x09F2A2E0  token=0x60006E0  Unity.Mathematics.quaternion UserTransformLocalRotation(System.Int32 vindex)
  RVA=0x049E11B0  token=0x60006E1  System.Void .ctor()
  RVA=0x0464E630  token=0x60006E2  System.Object <Init>b__11_0()
END_CLASS

CLASS: MagicaCloth.PhysicsTeam
TYPE:  abstract class
TOKEN: 0x20000D5
SIZE:  0x78
EXTENDS: MagicaCloth.CoreComponent
FIELDS:
  protected         MagicaCloth.PhysicsTeamData     teamData  // 0x38
  private           System.Single                   userBlendWeight  // 0x40
  private           System.Single                   userPhysicsBlendWeight  // 0x44
  private           System.Boolean                  syncBlendWeightToPoseBlendWeight  // 0x48
  private           System.Single                   userCloseThreshold  // 0x4c
  private           MagicaCloth.PhysicsTeam.TeamUpdateModeupdateMode  // 0x50
  private           MagicaCloth.PhysicsTeam.TeamCullingModecullingMode  // 0x54
  private           MagicaCloth.PhysicsTeam.TeamSkinningModeskinningMode  // 0x58
  protected         System.Int32                    teamId  // 0x5c
  protected         MagicaCloth.ChunkData           particleChunk  // 0x60
  protected         UnityEngine.Transform           influenceTarget  // 0x70
PROPERTIES:
  TeamId  get=0x04D86560
  TeamData  get=0x04D85A50
  ParticleChunk  get=0x04DA1280
  InfluenceTarget  get=0x04D862B0  set=0x063AD760
  UserBlendWeight  get=0x04D863F0  set=0x09EC1054
  UserPhysicsBlendWeight  get=0x04D86400  set=0x09F2FBE0
  SyncBlendWeightToPoseBlendWeight  get=0x04D867D0  set=0x04D86810
  UserCloseThreshold  get=0x04D87860  set=0x09F2FBC8
  UpdateMode  get=0x04D86550  set=0x04D865A0
  CullingMode  get=0x04D86540  set=0x04D86590
  SkinningMode  get=0x04D86570  set=0x04D865D0
  UseAnimatedPose  get=0x09F2FBC0
METHODS:
  RVA=0x09F2F85C  token=0x60006E3  System.Int32 GetDataHash()
  RVA=0x09F2FA88  token=0x60006F8  System.Void OnInit()
  RVA=0x09F2F9C0  token=0x60006F9  System.Void OnDispose()
  RVA=0x041E1670  token=0x60006FA  System.Void OnUpdate()
  RVA=0x09F2F978  token=0x60006FB  System.Void OnActive()
  RVA=0x09F2FA30  token=0x60006FC  System.Void OnInactive()
  RVA=0x09F2F87C  token=0x60006FD  System.Boolean IsActive()
  RVA=0x09F2F8FC  token=0x60006FE  System.Boolean IsValid()
  RVA=0x09F2F65C  token=0x60006FF  MagicaCloth.ChunkData CreateParticle(System.Int32 team, System.Int32 count, System.Func<System.Int32,System.UInt32> funcFlag, System.Func<System.Int32,Unity.Mathematics.float3> funcWpos, System.Func<System.Int32,Unity.Mathematics.quaternion> funcWrot, System.Func<System.Int32,System.Single> funcDepth, System.Func<System.Int32,Unity.Mathematics.float3> funcRadius, System.Func<System.Int32,Unity.Mathematics.float3> funcTargetLocalPos)
  RVA=0x09F2FAE4  token=0x6000700  System.Void RemoveAllParticle()
  RVA=0x09F2F7D4  token=0x6000701  System.Void EnableParticle(System.Func<System.Int32,UnityEngine.Transform> funcTarget, System.Func<System.Int32,Unity.Mathematics.float3> funcLpos, System.Func<System.Int32,Unity.Mathematics.quaternion> funcLrot)
  RVA=0x09F2F73C  token=0x6000702  System.Void DisableParticle(System.Func<System.Int32,UnityEngine.Transform> funcTarget, System.Func<System.Int32,Unity.Mathematics.float3> funcLpos, System.Func<System.Int32,Unity.Mathematics.quaternion> funcLrot)
  RVA=0x09F2FB44  token=0x6000703  MagicaCloth.Define.Error VerifyData()
  RVA=0x09F2FB4C  token=0x6000704  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.PhysicsTeamData
TYPE:  class
TOKEN: 0x20000D9
SIZE:  0x30
IMPLEMENTS: MagicaCloth.IDataHash
FIELDS:
  private           System.Collections.Generic.List<MagicaCloth.ColliderComponent>colliderList  // 0x10
  private           System.Collections.Generic.List<MagicaCloth.ColliderComponent>penetrationIgnoreColliderList  // 0x18
  private           System.Boolean                  mergeAvatarCollider  // 0x20
  private           System.Collections.Generic.List<MagicaCloth.ColliderComponent>addColliderList  // 0x28
PROPERTIES:
  ColliderCount  get=0x09F2F61C
  ColliderList  get=0x02B2ECC0
  PenetrationIgnoreColliderList  get=0x0385B100
  MergeAvatarCollider  get=0x04D866B0
METHODS:
  RVA=0x09F2F1D8  token=0x6000705  System.Int32 GetDataHash()
  RVA=0x09F2F214  token=0x6000706  System.Void Init(System.Int32 teamId)
  RVA=0x09F2EFA4  token=0x6000707  System.Void Dispose(System.Int32 teamId)
  RVA=0x09F2EF00  token=0x6000708  System.Void AddCollider(MagicaCloth.ColliderComponent collider)
  RVA=0x09F2F31C  token=0x6000709  System.Void RemoveCollider(MagicaCloth.ColliderComponent collider)
  RVA=0x09F2F434  token=0x600070A  System.Void UpdateStatus()
  RVA=0x09F2F528  token=0x600070F  System.Void ValidateColliderList()
  RVA=0x09F2F564  token=0x6000710  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.AdjustRotationWorker
TYPE:  class
TOKEN: 0x20000DA
SIZE:  0x30
EXTENDS: MagicaCloth.PhysicsManagerWorker
FIELDS:
  private   static  System.Int32                    AdjustMode_Fixed  // const
  private   static  System.Int32                    AdjustMode_XYMove  // const
  private   static  System.Int32                    AdjustMode_XZMove  // const
  private   static  System.Int32                    AdjustMode_YZMove  // const
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.AdjustRotationWorker.AdjustRotationData>dataList  // 0x18
  public            MagicaCloth.FixedNativeList<MagicaCloth.AdjustRotationWorker.GroupData>groupList  // 0x20
  private           MagicaCloth.ExNativeMultiHashMap<System.Int32,System.Int32>particleMap  // 0x28
METHODS:
  RVA=0x03E87400  token=0x6000711  System.Void Create()
  RVA=0x09F2340C  token=0x6000712  System.Void Release()
  RVA=0x09F22D08  token=0x6000713  System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, System.Int32 adjustMode, Unity.Mathematics.float3 axisRotationPower, MagicaCloth.AdjustRotationWorker.AdjustRotationData[] dataArray)
  RVA=0x09F23494  token=0x6000714  System.Void RemoveGroup(System.Int32 teamId)
  RVA=0x09F22F94  token=0x6000715  System.Void ChangeParam(System.Int32 teamId, System.Boolean active, System.Int32 adjustMode, Unity.Mathematics.float3 axisRotationPower)
  RVA=0x041E1670  token=0x6000716  System.Void Warmup()
  RVA=0x04D9A8E0  token=0x6000717  Unity.Jobs.JobHandle PreUpdate(Unity.Jobs.JobHandle jobHandle)
  RVA=0x09F23128  token=0x6000718  Unity.Jobs.JobHandle PostUpdate(Unity.Jobs.JobHandle jobHandle)
  RVA=0x041E1670  token=0x6000719  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.BaseSkinningWorker
TYPE:  class
TOKEN: 0x20000DE
SIZE:  0x30
EXTENDS: MagicaCloth.PhysicsManagerWorker
FIELDS:
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.BaseSkinningWorker.BaseSkinningData>dataList  // 0x18
  private           MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float4x4>bindPoseList  // 0x20
  public            MagicaCloth.FixedNativeList<MagicaCloth.BaseSkinningWorker.GroupData>groupList  // 0x28
METHODS:
  RVA=0x04B4E220  token=0x600071C  System.Void Create()
  RVA=0x09F24784  token=0x600071D  System.Void Release()
  RVA=0x09F240F4  token=0x600071E  System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, System.Boolean updateFixed, MagicaCloth.BaseSkinningWorker.BaseSkinningData[] skinningDataList, Unity.Mathematics.float4x4[] skinningBindPoseList)
  RVA=0x09F2480C  token=0x600071F  System.Void RemoveGroup(System.Int32 teamId)
  RVA=0x09F242D8  token=0x6000720  System.Void ChangeParam(System.Int32 teamId, System.Boolean updateFixed)
  RVA=0x041E1670  token=0x6000721  System.Void Warmup()
  RVA=0x09F24430  token=0x6000722  Unity.Jobs.JobHandle PreUpdate(Unity.Jobs.JobHandle jobHandle)
  RVA=0x04D9A8E0  token=0x6000723  Unity.Jobs.JobHandle PostUpdate(Unity.Jobs.JobHandle jobHandle)
  RVA=0x041E1670  token=0x6000724  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.LineWorker
TYPE:  class
TOKEN: 0x20000E2
SIZE:  0x38
EXTENDS: MagicaCloth.PhysicsManagerWorker
FIELDS:
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.LineWorker.LineRotationData>dataList  // 0x18
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.LineWorker.LineRotationRootInfo>rootInfoList  // 0x20
  public            MagicaCloth.FixedNativeList<MagicaCloth.LineWorker.GroupData>groupList  // 0x28
  private           MagicaCloth.FixedChunkNativeArray<System.Int32>rootTeamList  // 0x30
METHODS:
  RVA=0x0442EBF0  token=0x6000727  System.Void Create()
  RVA=0x09F27548  token=0x6000728  System.Void Release()
  RVA=0x09F26FA0  token=0x6000729  System.Int32 AddGroup(System.Int32 teamId, System.Boolean avarage, MagicaCloth.LineWorker.LineRotationData[] dataArray, MagicaCloth.LineWorker.LineRotationRootInfo[] rootInfoArray)
  RVA=0x09F275F0  token=0x600072A  System.Void RemoveGroup(System.Int32 teamId)
  RVA=0x09F27194  token=0x600072B  System.Void ChangeParam(System.Int32 teamId, System.Boolean avarage)
  RVA=0x041E1670  token=0x600072C  System.Void Warmup()
  RVA=0x04D9A8E0  token=0x600072D  Unity.Jobs.JobHandle PreUpdate(Unity.Jobs.JobHandle jobHandle)
  RVA=0x09F272F4  token=0x600072E  Unity.Jobs.JobHandle PostUpdate(Unity.Jobs.JobHandle jobHandle)
  RVA=0x041E1670  token=0x600072F  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.MeshParticleWorker
TYPE:  class
TOKEN: 0x20000E7
SIZE:  0x30
EXTENDS: MagicaCloth.PhysicsManagerWorker
FIELDS:
  private           MagicaCloth.ExNativeMultiHashMap<System.Int32,System.Int32>vertexToParticleMap  // 0x18
  private           MagicaCloth.FixedNativeListWithCount<System.Int32>vertexToParticleList  // 0x20
  private           System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<MagicaCloth.MeshParticleWorker.CreateData>>groupCreateDict  // 0x28
METHODS:
  RVA=0x03E87510  token=0x6000731  System.Void Create()
  RVA=0x09F297E8  token=0x6000732  System.Void Release()
  RVA=0x09F29060  token=0x6000733  System.Void Add(System.Int32 group, System.Int32 vindex, System.Int32 pindex)
  RVA=0x09F29850  token=0x6000734  System.Void RemoveGroup(System.Int32 group)
  RVA=0x041E1670  token=0x6000735  System.Void Warmup()
  RVA=0x09F29560  token=0x6000736  Unity.Jobs.JobHandle PreUpdate(Unity.Jobs.JobHandle jobHandle)
  RVA=0x09F291E8  token=0x6000737  Unity.Jobs.JobHandle PostUpdate(Unity.Jobs.JobHandle jobHandle)
  RVA=0x03B927E0  token=0x6000738  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.RenderMeshWorker
TYPE:  class
TOKEN: 0x20000EB
SIZE:  0x18
EXTENDS: MagicaCloth.PhysicsManagerWorker
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600073B  System.Void Create()
  RVA=0x041E1670  token=0x600073C  System.Void Release()
  RVA=0x041E1670  token=0x600073D  System.Void RemoveGroup(System.Int32 group)
  RVA=0x09F328E8  token=0x600073E  System.Boolean IsPerformMeshProcessForEachParticle()
  RVA=0x09F331C0  token=0x600073F  System.Void Warmup()
  RVA=0x04D9A8E0  token=0x6000740  Unity.Jobs.JobHandle PreUpdate(Unity.Jobs.JobHandle jobHandle)
  RVA=0x09F32970  token=0x6000741  Unity.Jobs.JobHandle PostUpdate(Unity.Jobs.JobHandle jobHandle)
  RVA=0x09F31370  token=0x6000742  System.Void CollectionVertex(MagicaCloth.PhysicsManagerMeshData.RenderMeshInfo& r_minfo, Unity.Collections.NativeArray<System.UInt32>& sharedChildVertexInfoList, Unity.Collections.NativeArray<MagicaCloth.MeshData.VertexWeight>& sharedChildVertexWeightList, Unity.Collections.NativeArray<Unity.Mathematics.float3>& virtualPosList, Unity.Collections.NativeArray<Unity.Mathematics.quaternion>& virtualRotList, Unity.Mathematics.float3& tpos, Unity.Mathematics.quaternion& trot, Unity.Mathematics.float3& tscl, Unity.Mathematics.quaternion& itrot, System.Single scaleRatio, Unity.Mathematics.float3& scaleDirection, System.Boolean calcNormal, System.Boolean calcTangent, System.Int32 vindex, System.Int32 i, System.UInt32 flag, System.Boolean isFasterWrite, Unity.Collections.NativeArray<Unity.Mathematics.float3>& renderPosList, Unity.Collections.NativeArray<Unity.Mathematics.float3>& renderNormalList, Unity.Collections.NativeArray<Unity.Mathematics.float3>& renderPosArray, Unity.Collections.NativeArray<Unity.Mathematics.float3>& renderNomalArray, Unity.Collections.NativeArray<Unity.Mathematics.float4>& renderTangentList)
  RVA=0x041E1670  token=0x6000743  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.SpringMeshWorker
TYPE:  class
TOKEN: 0x20000EF
SIZE:  0x30
EXTENDS: MagicaCloth.PhysicsManagerWorker
FIELDS:
  private           MagicaCloth.ExNativeMultiHashMap<System.Int32,MagicaCloth.SpringMeshWorker.SpringData>springMap  // 0x18
  private           MagicaCloth.FixedNativeListWithCount<System.Int32>springVertexList  // 0x20
  private           System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<System.Int32>>groupIndexDict  // 0x28
METHODS:
  RVA=0x03E875F0  token=0x6000747  System.Void Create()
  RVA=0x09F34230  token=0x6000748  System.Void Release()
  RVA=0x09F33D6C  token=0x6000749  System.Void Add(System.Int32 group, System.Int32 vertexIndex, System.Int32 particleIndex, System.Single weight)
  RVA=0x09F34298  token=0x600074A  System.Void RemoveGroup(System.Int32 group)
  RVA=0x041E1670  token=0x600074B  System.Void Warmup()
  RVA=0x04D9A8E0  token=0x600074C  Unity.Jobs.JobHandle PreUpdate(Unity.Jobs.JobHandle jobHandle)
  RVA=0x09F33EFC  token=0x600074D  Unity.Jobs.JobHandle PostUpdate(Unity.Jobs.JobHandle jobHandle)
  RVA=0x03B92760  token=0x600074E  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.TriangleWorker
TYPE:  class
TOKEN: 0x20000F2
SIZE:  0x30
EXTENDS: MagicaCloth.PhysicsManagerWorker
FIELDS:
  private           MagicaCloth.FixedChunkNativeArray<MagicaCloth.TriangleWorker.TriangleRotationData>triangleDataList  // 0x18
  private           MagicaCloth.FixedChunkNativeArray<System.Int32>triangleIndexList  // 0x20
  public            MagicaCloth.FixedNativeList<MagicaCloth.TriangleWorker.GroupData>groupList  // 0x28
METHODS:
  RVA=0x0442ED50  token=0x6000750  System.Void Create()
  RVA=0x09F35018  token=0x6000751  System.Void Release()
  RVA=0x09F34B74  token=0x6000752  System.Int32 AddGroup(System.Int32 teamId, MagicaCloth.TriangleWorker.TriangleRotationData[] dataArray, System.Int32[] indexArray)
  RVA=0x09F350A0  token=0x6000753  System.Void RemoveGroup(System.Int32 teamId)
  RVA=0x041E1670  token=0x6000754  System.Void Warmup()
  RVA=0x04D9A8E0  token=0x6000755  Unity.Jobs.JobHandle PreUpdate(Unity.Jobs.JobHandle jobHandle)
  RVA=0x09F34D98  token=0x6000756  Unity.Jobs.JobHandle PostUpdate(Unity.Jobs.JobHandle jobHandle)
  RVA=0x041E1670  token=0x6000757  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.VirtualMeshWorker
TYPE:  class
TOKEN: 0x20000F6
SIZE:  0x18
EXTENDS: MagicaCloth.PhysicsManagerWorker
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600075A  System.Void Create()
  RVA=0x041E1670  token=0x600075B  System.Void Release()
  RVA=0x041E1670  token=0x600075C  System.Void RemoveGroup(System.Int32 group)
  RVA=0x09F4A15C  token=0x600075D  System.Boolean IsPerformMeshProcessForEachParticle()
  RVA=0x041E1670  token=0x600075E  System.Void Warmup()
  RVA=0x09F4ABB8  token=0x600075F  Unity.Jobs.JobHandle PreUpdate(Unity.Jobs.JobHandle jobHandle)
  RVA=0x09F4A1E4  token=0x6000760  Unity.Jobs.JobHandle PostUpdate(Unity.Jobs.JobHandle jobHandle)
  RVA=0x041E1670  token=0x6000761  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.AutoMove
TYPE:  class
TOKEN: 0x20000FB
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.Vector3             direction  // 0x18
  private           System.Single                   length  // 0x24
  private           System.Single                   interval  // 0x28
  private           UnityEngine.Vector3             startPosition  // 0x2c
  private           System.Single                   time  // 0x38
METHODS:
  RVA=0x09F35584  token=0x6000766  System.Void Start()
  RVA=0x09F355C4  token=0x6000767  System.Void Update()
  RVA=0x09F3555C  token=0x6000768  System.Void OnMoveButton()
  RVA=0x09F3571C  token=0x6000769  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.AutoRotate
TYPE:  class
TOKEN: 0x20000FC
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.Vector3             rotateAngle  // 0x18
  private           System.Single                   interval  // 0x24
  private           System.Single                   time  // 0x28
METHODS:
  RVA=0x041E1670  token=0x600076A  System.Void Start()
  RVA=0x09F3575C  token=0x600076B  System.Void Update()
  RVA=0x09F3555C  token=0x600076C  System.Void OnMoveButton()
  RVA=0x09F35818  token=0x600076D  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.BlastWaveController
TYPE:  class
TOKEN: 0x20000FD
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            MagicaCloth.MagicaAreaWind      wind  // 0x18
  public            System.Single                   attenuationStartTime  // 0x20
  public            System.Single                   attenuationTime  // 0x24
METHODS:
  RVA=0x09F35B48  token=0x600076E  System.Collections.IEnumerator Start()
  RVA=0x04D848C0  token=0x600076F  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.CameraOrbit
TYPE:  class
TOKEN: 0x20000FF
SIZE:  0x98
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.Transform           cameraTransform  // 0x18
  public            UnityEngine.Transform           cameraTarget  // 0x20
  public            UnityEngine.Vector3             cameraTargetPos  // 0x28
  public            UnityEngine.Vector3             cameraTargetOffset  // 0x34
  private           System.Single                   cameraDist  // 0x40
  private           System.Single                   cameraPitch  // 0x44
  private           System.Single                   cameraYaw  // 0x48
  private           System.Single                   cameraDistHokanTime  // 0x4c
  private           System.Single                   cameraAngleHokanTime  // 0x50
  private           System.Single                   cameraDistSpeed  // 0x54
  private           System.Single                   cameraDistMax  // 0x58
  private           System.Single                   cameraDistMin  // 0x5c
  private           System.Single                   cameraYawSpeed  // 0x60
  private           System.Single                   cameraPitchSpeed  // 0x64
  private           System.Single                   cameraMaxAngleSpeed  // 0x68
  private           System.Single                   cameraPitchMax  // 0x6c
  private           System.Single                   cameraPitchMin  // 0x70
  private           MagicaCloth.CameraOrbit.MoveModemoveMode  // 0x74
  private           System.Single                   moveSpeed  // 0x78
  private           System.Single                   setCameraDist  // 0x7c
  private           System.Single                   setCameraPitch  // 0x80
  private           System.Single                   setCameraYaw  // 0x84
  private           System.Single                   cameraDistVelocity  // 0x88
  private           System.Single                   cameraPitchVelocity  // 0x8c
  private           System.Single                   cameraYawVelocity  // 0x90
METHODS:
  RVA=0x09F37398  token=0x6000776  System.Void Start()
  RVA=0x09F37070  token=0x6000777  System.Void OnEnable()
  RVA=0x09F36DC0  token=0x6000778  System.Void OnDisable()
  RVA=0x09F36DB8  token=0x6000779  System.Void LateUpdate()
  RVA=0x09F374F8  token=0x600077A  System.Void updateCamera()
  RVA=0x09F379B0  token=0x600077B  System.Void updatePitchYaw(UnityEngine.Vector2 speed)
  RVA=0x09F377D4  token=0x600077C  System.Void updateOffset(UnityEngine.Vector2 speed)
  RVA=0x09F37A1C  token=0x600077D  System.Void updateZoom(System.Single speed)
  RVA=0x09F372B0  token=0x600077E  System.Void OnTouchMove(System.Int32 fid, UnityEngine.Vector2 screenPos, UnityEngine.Vector2 screenVelocity, UnityEngine.Vector2 cmVelocity)
  RVA=0x09F37000  token=0x600077F  System.Void OnDoubleTouchMove(System.Int32 fid, UnityEngine.Vector2 screenPos, UnityEngine.Vector2 screenVelocity, UnityEngine.Vector2 cmVelocity)
  RVA=0x09F3732C  token=0x6000780  System.Void OnTouchPinch(System.Single speedscr, System.Single speedcm)
  RVA=0x09F37490  token=0x6000781  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.CopyObject
TYPE:  class
TOKEN: 0x2000101
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Int32                    seed  // 0x18
  public            System.Int32                    count  // 0x1c
  public            System.Single                   radius  // 0x20
  public            UnityEngine.GameObject          prefab  // 0x28
  public            System.Int32                    delayFrame  // 0x30
METHODS:
  RVA=0x041E1670  token=0x6000782  System.Void Awake()
  RVA=0x09F37C8C  token=0x6000783  System.Void Start()
  RVA=0x09F37C30  token=0x6000784  System.Collections.IEnumerator CreateObject()
  RVA=0x09F37CB0  token=0x6000785  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.DressUpControl
TYPE:  class
TOKEN: 0x2000103
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.GameObject          partsItemPrefab  // 0x18
  private           UnityEngine.UI.VerticalLayoutGroupverticalLayoutGroup  // 0x20
  private           MagicaCloth.MagicaAvatar        avatar  // 0x28
  public            System.Collections.Generic.List<MagicaCloth.DressUpControl.AvatarPartsGroup>avatarPartsGroupList  // 0x30
METHODS:
  RVA=0x09F38730  token=0x600078C  System.Void Start()
  RVA=0x041E1670  token=0x600078D  System.Void Update()
  RVA=0x09F386F8  token=0x600078E  System.Void OnDestroy()
  RVA=0x09F38504  token=0x600078F  System.Void Init()
  RVA=0x09F382C4  token=0x6000790  System.Void ChangeParts(System.Int32 id, System.Int32 dir)
  RVA=0x09F383C0  token=0x6000791  System.Void Clear()
  RVA=0x09F38740  token=0x6000792  System.Void .ctor()
  RVA=0x09F38738  token=0x6000793  System.Void <Init>b__8_0(System.Int32 id, System.Int32 dir)
END_CLASS

CLASS: MagicaCloth.ModelController
TYPE:  class
TOKEN: 0x2000105
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Collections.Generic.List<UnityEngine.GameObject>characterList  // 0x18
  private           System.Single                   slowTime  // 0x20
  private           System.Boolean                  slow  // 0x24
METHODS:
  RVA=0x041E1670  token=0x6000795  System.Void Start()
  RVA=0x041E1670  token=0x6000796  System.Void Update()
  RVA=0x09F4569C  token=0x6000797  System.Void AnimatorAction(System.Action<UnityEngine.Animator> act)
  RVA=0x09F45840  token=0x6000798  System.Void ClothAction(System.Action<MagicaCloth.BaseCloth> act)
  RVA=0x09F45BE4  token=0x6000799  System.Void OnNextButton()
  RVA=0x09F45AE8  token=0x600079A  System.Void OnBackButton()
  RVA=0x09F45CE0  token=0x600079B  System.Void OnSlowButton()
  RVA=0x09F459EC  token=0x600079C  System.Void OnActiveButton()
  RVA=0x09F45DFC  token=0x600079D  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.ObjectUtility
TYPE:  static class
TOKEN: 0x2000108
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09F47188  token=0x60007A6  UnityEngine.GameObject[] PlaceRandomCubes(System.Int32 count, UnityEngine.Vector3 center, System.Single radius)
  RVA=0x09F4732C  token=0x60007A7  UnityEngine.GameObject[] PlaceRandomCubes(System.Int32 count)
  RVA=0x09F470DC  token=0x60007A8  UnityEngine.GameObject MakeStrippedCube()
  RVA=0x09F47358  token=0x60007A9  UnityEngine.GameObject[] PlaceRandomGameObject(System.Int32 count, UnityEngine.Vector3 center, System.Single radius, UnityEngine.GameObject prefab)
END_CLASS

CLASS: MagicaCloth.SimpleInputManager
TYPE:  class
TOKEN: 0x2000109
SIZE:  0x70
EXTENDS: MagicaCloth.CreateSingleton`1
FIELDS:
  private   static  System.Int32                    MaxFinger  // const
  public            System.Single                   tapRadiusCm  // 0x18
  public            System.Single                   flickRangeCm  // 0x1c
  public            System.Single                   flickCheckSpeed  // 0x20
  public            System.Single                   mouseWheelSpeed  // 0x24
  private           System.Int32                    mainFingerId  // 0x28
  private           System.Int32                    subFingerId  // 0x2c
  private           UnityEngine.Vector2[]           downPos  // 0x30
  private           UnityEngine.Vector2[]           lastPos  // 0x38
  private           UnityEngine.Vector2[]           flickDownPos  // 0x40
  private           System.Single[]                 flickDownTime  // 0x48
  private           System.Single                   lastTime  // 0x50
  private           System.Boolean                  mobilePlatform  // 0x54
  private           System.Boolean[]                mouseDown  // 0x58
  private           UnityEngine.Vector2[]           mouseOldMovePos  // 0x60
  private           System.Single                   screenDpi  // 0x68
  private           System.Single                   screenDpc  // 0x6c
  public    static  UnityEngine.Events.UnityAction<System.Int32,UnityEngine.Vector2>OnTouchDown  // static @ 0x0
  public    static  UnityEngine.Events.UnityAction<System.Int32,UnityEngine.Vector2,UnityEngine.Vector2,UnityEngine.Vector2>OnTouchMove  // static @ 0x8
  public    static  UnityEngine.Events.UnityAction<System.Int32,UnityEngine.Vector2,UnityEngine.Vector2,UnityEngine.Vector2>OnDoubleTouchMove  // static @ 0x10
  public    static  UnityEngine.Events.UnityAction<System.Int32,UnityEngine.Vector2>OnTouchUp  // static @ 0x18
  public    static  UnityEngine.Events.UnityAction<System.Int32,UnityEngine.Vector2>OnTouchMoveCancel  // static @ 0x20
  public    static  UnityEngine.Events.UnityAction<System.Int32,UnityEngine.Vector2>OnTouchTap  // static @ 0x28
  public    static  UnityEngine.Events.UnityAction<System.Int32,UnityEngine.Vector2,UnityEngine.Vector2,UnityEngine.Vector2>OnTouchFlick  // static @ 0x30
  public    static  UnityEngine.Events.UnityAction<System.Single,System.Single>OnTouchPinch  // static @ 0x38
  public    static  UnityEngine.Events.UnityAction  OnBackButton  // static @ 0x40
PROPERTIES:
  ScreenDpi  get=0x09F496E8
  ScreenDpc  get=0x09F496A4
METHODS:
  RVA=0x04D7CA40  token=0x60007AA  System.Void Init()
  RVA=0x09F4838C  token=0x60007AB  System.Void InitSingleton()
  RVA=0x09F49624  token=0x60007AC  System.Void Update()
  RVA=0x09F48158  token=0x60007AF  System.Void CalcScreenDpi()
  RVA=0x09F48118  token=0x60007B0  System.Void AllResetTouchInfo()
  RVA=0x09F48384  token=0x60007B1  System.Int32 GetTouchCount()
  RVA=0x09F484A0  token=0x60007B2  System.Boolean IsUI()
  RVA=0x09F4853C  token=0x60007B3  System.Void UpdateMobile()
  RVA=0x09F48198  token=0x60007B4  UnityEngine.Vector2 CalcScreenRatioVector(UnityEngine.Vector2 vec)
  RVA=0x09F481E8  token=0x60007B5  System.Boolean CheckFlic(System.Int32 fid, UnityEngine.Vector2 oldpos, UnityEngine.Vector2 nowpos, UnityEngine.Vector2 downpos, System.Single flicktime)
  RVA=0x09F48DC8  token=0x60007B6  System.Void UpdateMouse()
  RVA=0x09F49644  token=0x60007B7  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.SliderStart
TYPE:  class
TOKEN: 0x200010A
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.UI.Text             text  // 0x18
  private           System.String                   lable  // 0x20
  private           System.String                   format  // 0x28
  private           System.String                   formatString  // 0x30
METHODS:
  RVA=0x09F497F8  token=0x60007B8  System.Void Start()
  RVA=0x09F4972C  token=0x60007B9  System.Void OnChangeValue(System.Single value)
  RVA=0x09F49974  token=0x60007BA  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.UIPartsItem
TYPE:  class
TOKEN: 0x200010B
SIZE:  0x38
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.UI.Text             text  // 0x18
  public            UnityEngine.UI.Button           prefButton  // 0x20
  public            UnityEngine.UI.Button           nextButton  // 0x28
  private           System.Int32                    id  // 0x30
METHODS:
  RVA=0x041E1670  token=0x60007BB  System.Void Start()
  RVA=0x09F49FE8  token=0x60007BC  System.Void Init(System.String title, System.Int32 id, System.Action<System.Int32,System.Int32> onClick)
  RVA=0x05393520  token=0x60007BD  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.WindController
TYPE:  class
TOKEN: 0x200010D
SIZE:  0x58
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.WindZone            unityWindZone  // 0x18
  private           System.Single                   unityWindZoneScale  // 0x20
  private           UnityEngine.Renderer            arrowRenderer  // 0x28
  private           UnityEngine.Gradient            arrowGradient  // 0x30
  private           System.Collections.Generic.List<UnityEngine.Transform>rotationTransforms  // 0x38
  private           UnityEngine.GameObject          blastWavePrefab  // 0x40
  private           System.Single                   blastWaveSpawnRadius  // 0x48
  private           System.Single                   angleY  // 0x4c
  private           System.Single                   angleX  // 0x50
PROPERTIES:
  Wind  get=0x09F4B574
METHODS:
  RVA=0x041E1670  token=0x60007C1  System.Void Start()
  RVA=0x09F4B108  token=0x60007C2  System.Void OnDirectionY(System.Single value)
  RVA=0x09F4B0FC  token=0x60007C3  System.Void OnDirectionX(System.Single value)
  RVA=0x09F4B14C  token=0x60007C4  System.Void OnMain(System.Single value)
  RVA=0x09F4B294  token=0x60007C5  System.Void OnTurbulence(System.Single value)
  RVA=0x09F4B114  token=0x60007C6  System.Void OnFrequency(System.Single value)
  RVA=0x09F4AFBC  token=0x60007C7  System.Void OnBlastWave()
  RVA=0x09F4B2CC  token=0x60007C9  System.Void UpdateDirection()
  RVA=0x09F4B4AC  token=0x60007CA  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.BezierParam
TYPE:  class
TOKEN: 0x200010E
SIZE:  0x28
IMPLEMENTS: MagicaCloth.IDataHash
FIELDS:
  private           System.Single                   startValue  // 0x10
  private           System.Single                   endValue  // 0x14
  private           System.Boolean                  useEndValue  // 0x18
  private           System.Single                   curveValue  // 0x1c
  private           System.Boolean                  useCurveValue  // 0x20
PROPERTIES:
  StartValue  get=0x04D85F70
  EndValue  get=0x09F35B24
  CurveValue  get=0x09F35B0C
  UseCurve  get=0x09F35B38
METHODS:
  RVA=0x041E1670  token=0x60007CB  System.Void .ctor()
  RVA=0x04DA3710  token=0x60007CC  System.Void .ctor(System.Single val)
  RVA=0x04DA3730  token=0x60007CD  System.Void .ctor(System.Single sval, System.Single eval)
  RVA=0x04DA36E0  token=0x60007CE  System.Void .ctor(System.Single sval, System.Single eval, System.Boolean useEval, System.Single cval, System.Boolean useCval)
  RVA=0x04DA36E0  token=0x60007CF  System.Void SetParam(System.Single sval, System.Single eval, System.Boolean useEval, System.Single cval, System.Boolean useCval)
  RVA=0x09F35998  token=0x60007D4  System.Single Evaluate(System.Single x)
  RVA=0x09F358C0  token=0x60007D5  MagicaCloth.BezierParam AutoSetup(System.Single startVal, System.Single endVal, System.Single curveVal)
  RVA=0x09F359F0  token=0x60007D6  System.Int32 GetDataHash()
  RVA=0x09F35924  token=0x60007D7  MagicaCloth.BezierParam Clone()
END_CLASS

CLASS: MagicaCloth.IBoneReplace
TYPE:  interface
TOKEN: 0x200010F
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60007D8  System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones()
  RVA=-1  // abstract  token=0x60007D9  System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict)
END_CLASS

CLASS: MagicaCloth.IDataVerify
TYPE:  interface
TOKEN: 0x2000110
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60007DA  System.Int32 GetVersion()
  RVA=-1  // abstract  token=0x60007DB  System.Void CreateVerifyData()
  RVA=-1  // abstract  token=0x60007DC  MagicaCloth.Define.Error VerifyData()
  RVA=-1  // abstract  token=0x60007DD  System.String GetInformation()
END_CLASS

CLASS: MagicaCloth.IEditorCloth
TYPE:  interface
TOKEN: 0x2000111
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60007DE  System.Collections.Generic.List<System.Int32> GetSelectionList()
  RVA=-1  // abstract  token=0x60007DF  System.Collections.Generic.List<System.Int32> GetUseList()
END_CLASS

CLASS: MagicaCloth.IEditorMesh
TYPE:  interface
TOKEN: 0x2000112
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60007E0  System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList)
  RVA=-1  // abstract  token=0x60007E1  System.Collections.Generic.List<System.Int32> GetEditorTriangleList()
  RVA=-1  // abstract  token=0x60007E2  System.Collections.Generic.List<System.Int32> GetEditorLineList()
END_CLASS

CLASS: MagicaCloth.RuntimeStatus
TYPE:  class
TOKEN: 0x2000113
SIZE:  0x48
FIELDS:
  private           System.Boolean                  initStart  // 0x10
  private           System.Boolean                  init  // 0x11
  private           System.Boolean                  initError  // 0x12
  private           System.Boolean                  enable  // 0x13
  private           System.Boolean                  userEnable  // 0x14
  private           System.Boolean                  runtimeError  // 0x15
  private           System.Boolean                  dispose  // 0x16
  private           System.Boolean                  isActive  // 0x17
  private           System.Boolean                  isDirty  // 0x18
  private           System.Collections.Generic.HashSet<MagicaCloth.RuntimeStatus><parentStatusSet>k__BackingField  // 0x20
  private           System.Collections.Generic.HashSet<MagicaCloth.RuntimeStatus><childStatusSet>k__BackingField  // 0x28
  private           System.Action                   UpdateStatusAction  // 0x30
  private           System.Action                   DisconnectedAction  // 0x38
  private           System.Func<System.Object>      OwnerFunc  // 0x40
PROPERTIES:
  parentStatusSet  get=0x04D862C0  set=0x05390F40
  childStatusSet  get=0x04D86240  set=0x02FDB880
  IsActive  get=0x04D39700
  IsInitStart  get=0x015EFCE0
  IsInitComplete  get=0x04D88380
  IsInitSuccess  get=0x09F48104
  IsInitError  get=0x047FE4C0
  IsDispose  get=0x04DA2FA0
  IsDirty  get=0x04D86340
METHODS:
  RVA=0x015EFCD0  token=0x60007EE  System.Void SetInitStart()
  RVA=0x04D8ECC0  token=0x60007EF  System.Void SetInitComplete()
  RVA=0x04D99700  token=0x60007F0  System.Void SetInitError()
  RVA=0x047FE380  token=0x60007F1  System.Boolean SetEnable(System.Boolean sw)
  RVA=0x09F480B8  token=0x60007F2  System.Boolean SetUserEnable(System.Boolean sw)
  RVA=0x09F480A8  token=0x60007F3  System.Boolean SetRuntimeError(System.Boolean sw)
  RVA=0x04DA37F0  token=0x60007F4  System.Void SetDispose()
  RVA=0x04D8EB60  token=0x60007F5  System.Void SetDirty()
  RVA=0x04D91AA0  token=0x60007F6  System.Void ClearDirty()
  RVA=0x047FE4D0  token=0x60007F7  System.Boolean UpdateStatus()
  RVA=0x09F47ED0  token=0x60007F8  System.Void AddParentStatus(MagicaCloth.RuntimeStatus status)
  RVA=0x09F48004  token=0x60007F9  System.Void RemoveParentStatus(MagicaCloth.RuntimeStatus status)
  RVA=0x09F47E7C  token=0x60007FA  System.Void AddChildStatus(MagicaCloth.RuntimeStatus status)
  RVA=0x09F47F60  token=0x60007FB  System.Void RemoveChildStatus(MagicaCloth.RuntimeStatus status)
  RVA=0x09F47F24  token=0x60007FC  System.Void LinkParentStatus(MagicaCloth.RuntimeStatus parent)
  RVA=0x09F480C8  token=0x60007FD  System.Void UnlinkParentStatus(MagicaCloth.RuntimeStatus parent)
  RVA=0x047FE620  token=0x60007FE  System.Boolean IsParentStatusActive()
  RVA=0x049E1240  token=0x60007FF  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.DataHashExtensions
TYPE:  static class
TOKEN: 0x2000114
SIZE:  0x10
FIELDS:
  public    static  System.Int32                    NullHash  // const
  public    static  System.Int32                    NumberHash  // const
METHODS:
  RVA=0x09F37CD4  token=0x6000800  System.Int32 GetDataHash(System.Object data)
  RVA=0x09F37F08  token=0x6000801  System.Int32 GetDataHash(MagicaCloth.IDataHash data)
  RVA=-1  // generic def  token=0x6000802  System.Int32 GetDataHash(T[] data)
  RVA=-1  // generic def  token=0x6000803  System.Int32 GetDataHash(System.Collections.Generic.List<T> data)
  RVA=-1  // generic def  token=0x6000804  System.Int32 GetDataCountHash(T[] data)
  RVA=-1  // generic def  token=0x6000805  System.Int32 GetDataCountHash(System.Collections.Generic.List<T> data)
  RVA=0x09F37F20  token=0x6000806  System.UInt64 GetVectorDataHash(UnityEngine.Vector3 v)
END_CLASS

CLASS: MagicaCloth.GridHash
TYPE:  class
TOKEN: 0x2000115
SIZE:  0x20
FIELDS:
  protected         System.Collections.Generic.Dictionary<System.UInt32,System.Collections.Generic.List<MagicaCloth.GridHash.Point>>gridMap  // 0x10
  protected         System.Single                   gridSize  // 0x18
METHODS:
  RVA=0x09F3AA3C  token=0x6000807  System.Void Create(System.Single gridSize)
  RVA=0x09F3A86C  token=0x6000808  System.Void AddPoint(Unity.Mathematics.float3 pos, System.Int32 id)
  RVA=0x09F3ABAC  token=0x6000809  System.Void Remove(Unity.Mathematics.float3 pos, System.Int32 id)
  RVA=0x09F3A9F4  token=0x600080A  System.Void Clear()
  RVA=0x09F3AB28  token=0x600080B  Unity.Mathematics.int3 GetGridPos(Unity.Mathematics.float3 pos, System.Single gridSize)
  RVA=0x04DA37C0  token=0x600080C  System.UInt32 GetGridHash(Unity.Mathematics.int3 pos)
  RVA=0x09F3AAC4  token=0x600080D  System.UInt32 GetGridHash(Unity.Mathematics.float3 pos, System.Single gridSize)
  RVA=0x04D8D240  token=0x600080E  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.NearPointSearch
TYPE:  class
TOKEN: 0x2000117
SIZE:  0x40
EXTENDS: MagicaCloth.GridHash
FIELDS:
  private           System.Single                   radius  // 0x20
  private           System.Collections.Generic.Dictionary<System.Int32,System.Int32>nearDict  // 0x28
  private           System.Collections.Generic.Dictionary<System.Int32,System.Single>distDict  // 0x30
  private           System.Collections.Generic.HashSet<System.UInt32>lockPairSet  // 0x38
METHODS:
  RVA=0x09F45F1C  token=0x6000810  System.Void Create(Unity.Mathematics.float3[] positionList, System.Single radius)
  RVA=0x09F462D8  token=0x6000811  System.Void SearchNearPointAll()
  RVA=0x09F46558  token=0x6000812  System.Void SearchNearPoint(System.Int32 id, Unity.Mathematics.float3 pos)
  RVA=0x09F46A24  token=0x6000813  System.Void SearchNearPoint(Unity.Mathematics.float3 pos, System.Single r)
  RVA=0x09F45EF4  token=0x6000814  System.Void AddPoint(Unity.Mathematics.float3 pos, System.Int32 id)
  RVA=0x09F46208  token=0x6000815  System.Void Remove(Unity.Mathematics.float3 pos, System.Int32 id)
  RVA=0x09F45E84  token=0x6000816  System.Void AddLockPair(System.Int32 id1, System.Int32 id2)
  RVA=0x09F45FA8  token=0x6000817  System.Boolean GetNearPointPair(System.Int32& id1, System.Int32& id2)
  RVA=0x09F46D80  token=0x6000818  System.String ToString()
  RVA=0x09F46FC8  token=0x6000819  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.ChunkData
TYPE:  sealed struct
TOKEN: 0x2000118
SIZE:  0x20
FIELDS:
  public            System.Int32                    chunkNo  // 0x10
  public            System.Int32                    startIndex  // 0x14
  public            System.Int32                    dataLength  // 0x18
  public            System.Int32                    useLength  // 0x1c
METHODS:
  RVA=0x04DA3750  token=0x600081A  System.Void Clear()
  RVA=0x0423A5F0  token=0x600081B  System.Boolean IsValid()
  RVA=0x09F37A94  token=0x600081C  System.String ToString()
END_CLASS

CLASS: MagicaCloth.DoubleComputeBuffer`1
TYPE:  class
TOKEN: 0x2000119
IMPLEMENTS: System.IDisposable
FIELDS:
  private           UnityEngine.ComputeBuffer       buffer0  // 0x0
  private           UnityEngine.ComputeBuffer       buffer1  // 0x0
  private           Unity.Collections.NativeArray<T>nativeArrya  // 0x0
  public            System.UInt32                   bufferID  // 0x0
  public            System.UInt32                   bufferOffset  // 0x0
  public            System.Int32                    bufferCount  // 0x0
PROPERTIES:
  Count  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600081D  System.Void Dispose()
  RVA=-1  // not resolved  token=0x600081E  System.Void Swap()
  RVA=-1  // not resolved  token=0x600081F  System.Void Create(System.Int32 size, UnityEngine.ComputeBufferType type, UnityEngine.ComputeBufferMode usage)
  RVA=-1  // not resolved  token=0x6000820  System.Void BeginWrite(System.Int32 length)
  RVA=-1  // not resolved  token=0x6000821  System.Void EndWrite(System.Int32 length)
  RVA=-1  // not resolved  token=0x6000822  Unity.Collections.NativeArray<T> GetNativeArray()
  RVA=-1  // not resolved  token=0x6000823  UnityEngine.ComputeBuffer GetBuffer(System.Int32 bufferIndex)
  RVA=-1  // not resolved  token=0x6000825  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.ExNativeHashMap`2
TYPE:  class
TOKEN: 0x200011A
FIELDS:
  private           Unity.Collections.NativeParallelHashMap<TKey,TValue>nativeHashMap  // 0x0
  private           System.Int32                    nativeLength  // 0x0
  private           System.Collections.Generic.HashSet<TKey>useKeySet  // 0x0
PROPERTIES:
  NativeCount  get=-1  // not resolved
  Count  get=-1  // not resolved
  Map  get=-1  // not resolved
  UseKeySet  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000826  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000827  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000829  System.Void Add(TKey key, TValue value)
  RVA=-1  // not resolved  token=0x600082A  TValue Get(TKey key)
  RVA=-1  // not resolved  token=0x600082B  System.Void Remove(System.Func<TKey,TValue,System.Boolean> func)
  RVA=-1  // not resolved  token=0x600082C  System.Void Replace(System.Func<TKey,TValue,System.Boolean> func, System.Func<TValue,TValue> datafunc)
  RVA=-1  // not resolved  token=0x600082D  System.Void Remove(TKey key)
  RVA=-1  // not resolved  token=0x600082F  System.Void Clear()
END_CLASS

CLASS: MagicaCloth.ExNativeMultiHashMap`2
TYPE:  class
TOKEN: 0x200011B
FIELDS:
  private           Unity.Collections.NativeParallelMultiHashMap<TKey,TValue>nativeMultiHashMap  // 0x0
  private           System.Int32                    nativeLength  // 0x0
  private           System.Collections.Generic.Dictionary<TKey,System.Int32>useKeyDict  // 0x0
PROPERTIES:
  NativeCount  get=-1  // not resolved
  IsCreated  get=-1  // not resolved
  Count  get=-1  // not resolved
  Map  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000832  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000833  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000836  System.Void Add(TKey key, TValue value)
  RVA=-1  // not resolved  token=0x6000837  System.Void Remove(TKey key, TValue value)
  RVA=-1  // not resolved  token=0x6000838  System.Void Remove(System.Func<TKey,TValue,System.Boolean> func)
  RVA=-1  // not resolved  token=0x6000839  System.Void Replace(System.Func<TKey,TValue,System.Boolean> func, System.Func<TValue,TValue> datafunc)
  RVA=-1  // not resolved  token=0x600083A  System.Void Process(System.Action<TKey,TValue> act)
  RVA=-1  // not resolved  token=0x600083B  System.Void Process(TKey key, System.Action<TValue> act)
  RVA=-1  // not resolved  token=0x600083C  System.Boolean Contains(TKey key, TValue value)
  RVA=-1  // not resolved  token=0x600083D  System.Boolean Contains(TKey key)
  RVA=-1  // not resolved  token=0x600083E  System.Void Remove(TKey key)
  RVA=-1  // not resolved  token=0x6000840  System.Void Clear()
END_CLASS

CLASS: MagicaCloth.FixedChunkNativeArray`1
TYPE:  class
TOKEN: 0x200011C
IMPLEMENTS: System.IDisposable
FIELDS:
  private           Unity.Collections.NativeArray<T>nativeArray0  // 0x0
  private           Unity.Collections.NativeArray<T>nativeArray1  // 0x0
  private           System.Int32                    nativeLength  // 0x0
  private           System.Collections.Generic.List<MagicaCloth.ChunkData>emptyChunkList  // 0x0
  private           System.Collections.Generic.List<MagicaCloth.ChunkData>useChunkList  // 0x0
  private           System.Int32                    chunkSeed  // 0x0
  private           System.Int32                    initLength  // 0x0
  private           T                               emptyElement  // 0x0
  private           System.Int32                    useLength  // 0x0
PROPERTIES:
  Length  get=-1  // not resolved
  ChunkCount  get=-1  // not resolved
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000842  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000843  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000844  System.Void SetEmptyElement(T empty)
  RVA=-1  // not resolved  token=0x6000845  MagicaCloth.ChunkData AddChunk(System.Int32 length)
  RVA=-1  // not resolved  token=0x6000846  MagicaCloth.ChunkData Add(T data)
  RVA=-1  // not resolved  token=0x6000847  System.Void RemoveChunk(System.Int32 chunkNo)
  RVA=-1  // not resolved  token=0x6000848  System.Void RemoveChunk(MagicaCloth.ChunkData chunk)
  RVA=-1  // not resolved  token=0x6000849  System.Void Fill(MagicaCloth.ChunkData chunk, T data)
  RVA=-1  // not resolved  token=0x600084F  Unity.Collections.NativeArray<T> ToJobArray()
  RVA=-1  // not resolved  token=0x6000850  Unity.Collections.NativeArray<T> ToJobArray(System.Int32 bufferIndex)
  RVA=-1  // not resolved  token=0x6000851  System.Void SwapBuffer()
  RVA=-1  // not resolved  token=0x6000852  System.String ToString()
END_CLASS

CLASS: MagicaCloth.FixedMultiNativeList`1
TYPE:  class
TOKEN: 0x200011D
IMPLEMENTS: System.IDisposable
FIELDS:
  private           Unity.Collections.NativeArray<T>nativeArray  // 0x0
  private           System.Int32                    nativeLength  // 0x0
  private           System.Collections.Generic.List<MagicaCloth.ChunkData>emptyChunkList  // 0x0
  private           System.Collections.Generic.List<MagicaCloth.ChunkData>useChunkList  // 0x0
  private           System.Int32                    chunkSeed  // 0x0
  private           System.Int32                    initLength  // 0x0
  private           T                               emptyElement  // 0x0
  private           System.Int32                    useLength  // 0x0
PROPERTIES:
  Length  get=-1  // not resolved
  ChunkCount  get=-1  // not resolved
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000853  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000854  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000855  System.Void SetEmptyElement(T empty)
  RVA=-1  // not resolved  token=0x6000856  MagicaCloth.ChunkData AddChunk(System.Int32 length)
  RVA=-1  // not resolved  token=0x6000857  System.Void RemoveChunk(System.Int32 chunkNo)
  RVA=-1  // not resolved  token=0x6000858  System.Void RemoveChunk(MagicaCloth.ChunkData chunk)
  RVA=-1  // not resolved  token=0x6000859  MagicaCloth.ChunkData AddData(MagicaCloth.ChunkData chunk, T data)
  RVA=-1  // not resolved  token=0x600085A  MagicaCloth.ChunkData RemoveData(MagicaCloth.ChunkData chunk, T data)
  RVA=-1  // not resolved  token=0x600085B  MagicaCloth.ChunkData ClearData(MagicaCloth.ChunkData chunk)
  RVA=-1  // not resolved  token=0x600085C  System.Void Process(MagicaCloth.ChunkData chunk, System.Action<T> act)
  RVA=-1  // not resolved  token=0x6000861  Unity.Collections.NativeArray<T> ToJobArray()
  RVA=-1  // not resolved  token=0x6000862  System.String ToString()
END_CLASS

CLASS: MagicaCloth.FixedNativeList`1
TYPE:  class
TOKEN: 0x200011E
IMPLEMENTS: System.IDisposable
FIELDS:
  private           Unity.Collections.NativeArray<T>nativeArray0  // 0x0
  private           Unity.Collections.NativeArray<T>nativeArray1  // 0x0
  private           System.Int32                    nativeLength  // 0x0
  private           System.Collections.Generic.Queue<System.Int32>emptyStack  // 0x0
  private           System.Collections.Generic.HashSet<System.Int32>useIndexSet  // 0x0
  private           System.Int32                    useLength  // 0x0
PROPERTIES:
  Length  get=-1  // not resolved
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000863  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000864  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000865  System.Int32 Add(T element)
  RVA=-1  // not resolved  token=0x6000866  System.Void Remove(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000867  System.Boolean Exists(System.Int32 index)
  RVA=-1  // not resolved  token=0x600086C  System.Void Clear()
  RVA=-1  // not resolved  token=0x600086D  Unity.Collections.NativeArray<T> ToJobArray()
  RVA=-1  // not resolved  token=0x600086E  Unity.Collections.NativeArray<T> ToJobArray(System.Int32 bufferIndex)
  RVA=-1  // not resolved  token=0x600086F  System.Void SyncBuffer()
END_CLASS

CLASS: MagicaCloth.FixedNativeListWithCount`1
TYPE:  class
TOKEN: 0x200011F
IMPLEMENTS: System.IDisposable
FIELDS:
  private           Unity.Collections.NativeList<T> nativeList  // 0x0
  private           System.Int32                    nativeLength  // 0x0
  private           System.Collections.Generic.Queue<System.Int32>emptyStack  // 0x0
  private           System.Collections.Generic.HashSet<System.Int32>useIndexSet  // 0x0
  private           System.Collections.Generic.Dictionary<T,System.Int32>indexDict  // 0x0
  private           System.Collections.Generic.Dictionary<T,System.Int32>countDict  // 0x0
  private           T                               emptyElement  // 0x0
PROPERTIES:
  Length  get=-1  // not resolved
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000870  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000871  System.Void .ctor(System.Int32 capacity)
  RVA=-1  // not resolved  token=0x6000872  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000873  System.Void SetEmptyElement(T empty)
  RVA=-1  // not resolved  token=0x6000874  System.Int32 Add(T element)
  RVA=-1  // not resolved  token=0x6000875  System.Void Remove(T element)
  RVA=-1  // not resolved  token=0x6000876  System.Boolean Exist(T element)
  RVA=-1  // not resolved  token=0x6000877  System.Int32 GetUseCount(T element)
  RVA=-1  // not resolved  token=0x600087C  System.Void Clear()
  RVA=-1  // not resolved  token=0x600087D  T[] ToArray()
  RVA=-1  // not resolved  token=0x600087E  Unity.Collections.NativeArray<T> ToJobArray()
END_CLASS

CLASS: MagicaCloth.FixedTransformAccessArray
TYPE:  class
TOKEN: 0x2000120
SIZE:  0x40
IMPLEMENTS: System.IDisposable
FIELDS:
  private           UnityEngine.Jobs.TransformAccessArraytransformArray  // 0x10
  private           System.Int32                    nativeLength  // 0x18
  private           System.Collections.Generic.Queue<System.Int32>emptyStack  // 0x20
  private           System.Collections.Generic.Dictionary<System.Int32,System.Int32>useIndexDict  // 0x28
  private           System.Collections.Generic.Dictionary<System.Int32,System.Int32>indexDict  // 0x30
  private           System.Collections.Generic.Dictionary<System.Int32,System.Int32>referenceDict  // 0x38
PROPERTIES:
  Count  get=0x032338A0
  Length  get=0x011EF5B0
  Item  get=0x09F38F88
METHODS:
  RVA=0x03E87C20  token=0x600087F  System.Void .ctor(System.Int32 desiredJobCount)
  RVA=0x09F38E14  token=0x6000880  System.Void .ctor(System.Int32 capacity, System.Int32 desiredJobCount)
  RVA=0x09F387C0  token=0x6000881  System.Int32 Add(UnityEngine.Transform element)
  RVA=0x09F38CEC  token=0x6000882  System.Void Remove(System.Int32 index)
  RVA=0x09F38C40  token=0x6000883  System.Boolean Exist(System.Int32 index)
  RVA=0x09F38BD4  token=0x6000884  System.Boolean Exist(UnityEngine.Transform element)
  RVA=0x09F38C64  token=0x6000888  System.Int32 GetIndex(UnityEngine.Transform element)
  RVA=0x09F38940  token=0x6000889  System.Void Clear()
  RVA=0x09F38B2C  token=0x600088A  System.Void Dispose()
  RVA=0x02B2ECC0  token=0x600088B  UnityEngine.Jobs.TransformAccessArray GetTransformAccessArray()
END_CLASS

CLASS: MagicaCloth.NativeArrayExtension
TYPE:  static class
TOKEN: 0x2000121
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x600088C  System.Void CopyToFast(Unity.Collections.NativeArray<T> nativeArray, System.Int32 startIndex, T2[] array)
  RVA=-1  // generic def  token=0x600088D  System.Void CopyToFast(Unity.Collections.NativeArray<T> nativeArray, System.Int32 startIndex, Unity.Collections.NativeArray<T> array)
  RVA=-1  // generic def  token=0x600088E  System.Void CopyBlock(Unity.Collections.NativeArray<T> nativeArray, System.Int32 sourceIndex, System.Int32 destinationIndex, System.Int32 count)
  RVA=-1  // generic def  token=0x600088F  System.Void CopyFromFast(Unity.Collections.NativeArray<T> nativeArray, Unity.Collections.NativeArray<T> array)
  RVA=-1  // generic def  token=0x6000890  System.Void CopyFromFast(Unity.Collections.NativeArray<T> nativeArray, System.Int32 startIndex, T2[] array)
  RVA=-1  // generic def  token=0x6000891  System.Void SetValue(Unity.Collections.NativeArray<T> nativeArray, System.Int32 startIndex, System.Int32 count, T value)
END_CLASS

CLASS: MagicaCloth.CreateSingleton`1
TYPE:  abstract class
TOKEN: 0x2000122
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  T                               instance  // static @ 0x0
  private   static  T                               initInstance  // static @ 0x0
  private   static  System.Boolean                  isDestroy  // static @ 0x0
PROPERTIES:
  Instance  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000892  System.Void InitMember()
  RVA=-1  // not resolved  token=0x6000894  System.Void InitInstance()
  RVA=-1  // not resolved  token=0x6000895  System.Boolean IsInstance()
  RVA=-1  // not resolved  token=0x6000896  System.Void Awake()
  RVA=-1  // not resolved  token=0x6000897  System.Void OnDestroy()
  RVA=-1  // not resolved  token=0x6000898  System.Void DuplicateDetection(T duplicate)
  RVA=-1  // abstract  token=0x6000899  System.Void InitSingleton()
  RVA=-1  // not resolved  token=0x600089A  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.DataUtility
TYPE:  static class
TOKEN: 0x2000123
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0677F488  token=0x600089B  System.UInt32 PackPair(System.Int32 v0, System.Int32 v1)
  RVA=0x04DA3080  token=0x600089C  System.Void UnpackPair(System.UInt32 pack, System.Int32& v0, System.Int32& v1)
  RVA=0x04D874D0  token=0x600089D  System.UInt32 Pack16(System.Int32 hi, System.Int32 low)
  RVA=0x04D875B0  token=0x600089E  System.Int32 Unpack16Hi(System.UInt32 pack)
  RVA=0x04D875C0  token=0x600089F  System.Int32 Unpack16Low(System.UInt32 pack)
  RVA=0x04DA3760  token=0x60008A0  System.UInt32 Pack4_28(System.Int32 hi, System.Int32 low)
  RVA=0x04DA3780  token=0x60008A1  System.Int32 Unpack4_28Hi(System.UInt32 pack)
  RVA=0x04DA3790  token=0x60008A2  System.Int32 Unpack4_28Low(System.UInt32 pack)
  RVA=0x04DA3770  token=0x60008A3  System.UInt32 Pack8_24(System.Int32 hi, System.Int32 low)
  RVA=0x04DA37A0  token=0x60008A4  System.Int32 Unpack8_24Hi(System.UInt32 pack)
  RVA=0x04DA37B0  token=0x60008A5  System.Int32 Unpack8_24Low(System.UInt32 pack)
  RVA=0x04D91CF0  token=0x60008A6  System.UInt64 Pack32(System.Int32 hi, System.Int32 low)
  RVA=0x04D9D8A0  token=0x60008A7  System.Int32 Unpack32Hi(System.UInt64 pack)
  RVA=0x04D88BE0  token=0x60008A8  System.Int32 Unpack32Low(System.UInt64 pack)
  RVA=0x09F3813C  token=0x60008A9  System.UInt64 PackTriple(System.Int32 v0, System.Int32 v1, System.Int32 v2)
  RVA=0x04DA30C0  token=0x60008AA  System.Void UnpackTriple(System.UInt64 pack, System.Int32& v0, System.Int32& v1, System.Int32& v2)
  RVA=0x09F37FC4  token=0x60008AB  System.UInt64 PackQuater(System.Int32 v0, System.Int32 v1, System.Int32 v2, System.Int32 v3)
  RVA=0x04DA3090  token=0x60008AC  System.Void UnpackQuater(System.UInt64 pack, System.Int32& v0, System.Int32& v1, System.Int32& v2, System.Int32& v3)
END_CLASS

CLASS: MagicaCloth.Develop
TYPE:  static class
TOKEN: 0x2000124
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09F38278  token=0x60008AD  System.Void Log(System.String str)
END_CLASS

CLASS: MagicaCloth.GizmoUtility
TYPE:  static class
TOKEN: 0x2000125
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Color               ColorDynamic  // static @ 0x0
  public    static readonly UnityEngine.Color               ColorKinematic  // static @ 0x10
  public    static readonly UnityEngine.Color               ColorInvalid  // static @ 0x20
  public    static readonly UnityEngine.Color               ColorCollider  // static @ 0x30
  public    static readonly UnityEngine.Color               ColorNonSelectedCollider  // static @ 0x40
  public    static readonly UnityEngine.Color               ColorTriangle  // static @ 0x50
  public    static readonly UnityEngine.Color               ColorStructLine  // static @ 0x60
  public    static readonly UnityEngine.Color               ColorBendLine  // static @ 0x70
  public    static readonly UnityEngine.Color               ColorNearLine  // static @ 0x80
  public    static readonly UnityEngine.Color               ColorRotationLine  // static @ 0x90
  public    static readonly UnityEngine.Color               ColorAdjustLine  // static @ 0xa0
  public    static readonly UnityEngine.Color               ColorAirLine  // static @ 0xb0
  public    static readonly UnityEngine.Color               ColorBasePosition  // static @ 0xc0
  public    static readonly UnityEngine.Color               ColorDirectionMoveLimit  // static @ 0xd0
  public    static readonly UnityEngine.Color               ColorPenetration  // static @ 0xe0
  public    static readonly UnityEngine.Color               ColorCollisionNormal  // static @ 0xf0
  public    static readonly UnityEngine.Color               ColorVelocity  // static @ 0x100
  public    static readonly UnityEngine.Color               ColorSkinningBone  // static @ 0x110
  public    static readonly UnityEngine.Color               ColorDeformerPoint  // static @ 0x120
  public    static readonly UnityEngine.Color               ColorDeformerPointRange  // static @ 0x130
  public    static readonly UnityEngine.Color               ColorWind  // static @ 0x140
METHODS:
  RVA=0x09F399C4  token=0x60008AE  System.Void DrawWireCapsule(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, UnityEngine.Vector3 scl, UnityEngine.Vector3 ldir, UnityEngine.Vector3 lup, System.Single length, System.Single startRadius, System.Single endRadius, System.Boolean resetMatrix)
  RVA=0x09F3A36C  token=0x60008AF  System.Void DrawWireSphere(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, UnityEngine.Vector3 scl, System.Single radius, System.Boolean drawSphere, System.Boolean drawAxis, System.Boolean resetMatrix)
  RVA=0x09F3A24C  token=0x60008B0  System.Void DrawWireCube(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, UnityEngine.Vector3 size, System.Boolean resetMatrix)
  RVA=0x09F39E38  token=0x60008B1  System.Void DrawWireCone(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, System.Single length, System.Single radius, System.Int32 div)
  RVA=0x09F39640  token=0x60008B2  System.Void DrawWireArrow(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, UnityEngine.Vector3 size, System.Boolean cross)
  RVA=0x09F38F94  token=0x60008B3  System.Void DrawAxis(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, System.Single size, System.Boolean resetMatrix)
  RVA=0x09F391F0  token=0x60008B4  System.Void DrawBone(UnityEngine.Vector3 pos, UnityEngine.Vector3 tpos, System.Single size)
  RVA=0x09F3A608  token=0x60008B5  System.Void .cctor()
END_CLASS

CLASS: MagicaCloth.MathUtility
TYPE:  static class
TOKEN: 0x2000126
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x066AD86C  token=0x60008B6  System.Single Clamp1(System.Single a)
  RVA=0x09F3AE20  token=0x60008B7  System.Single Clamp01(System.Single a)
  RVA=0x09EFEF5C  token=0x60008B8  Unity.Mathematics.float3 Project(Unity.Mathematics.float3 v, Unity.Mathematics.float3 n)
  RVA=0x066A85BC  token=0x60008B9  System.Single Angle(Unity.Mathematics.float3 v1, Unity.Mathematics.float3 v2)
  RVA=0x09EFEB30  token=0x60008BA  Unity.Mathematics.float3 ClampVector(Unity.Mathematics.float3 v, System.Single minlength, System.Single maxlength)
  RVA=0x09F3B148  token=0x60008BB  Unity.Mathematics.float3 ClampVector(Unity.Mathematics.float3 v, System.Single maxlength)
  RVA=0x09F1260C  token=0x60008BC  Unity.Mathematics.float3 ClampDistance(Unity.Mathematics.float3 from, Unity.Mathematics.float3 to, System.Single maxlength)
  RVA=0x09F3AE30  token=0x60008BD  System.Boolean ClampAngle(Unity.Mathematics.float3 dir, Unity.Mathematics.float3 basedir, System.Single maxAngle, Unity.Mathematics.float3& outdir)
  RVA=0x09F3BF44  token=0x60008BE  Unity.Mathematics.quaternion FromToRotation(Unity.Mathematics.float3 from, Unity.Mathematics.float3 to, System.Single t)
  RVA=0x066AEEC4  token=0x60008BF  Unity.Mathematics.quaternion FromToRotation(Unity.Mathematics.quaternion from, Unity.Mathematics.quaternion to)
  RVA=0x09F3ACF4  token=0x60008C0  System.Single Angle(Unity.Mathematics.quaternion a, Unity.Mathematics.quaternion b)
  RVA=0x09F3B0B0  token=0x60008C1  Unity.Mathematics.quaternion ClampAngle(Unity.Mathematics.quaternion from, Unity.Mathematics.quaternion to, System.Single maxAngle)
  RVA=0x09F3AD58  token=0x60008C2  Unity.Mathematics.float3 AxisToEuler(Unity.Mathematics.float3 axis)
  RVA=0x09F21260  token=0x60008C3  Unity.Mathematics.quaternion AxisQuaternion(Unity.Mathematics.float3 dir)
  RVA=0x09EFEC04  token=0x60008C4  System.Single ClosestPtPointSegmentRatio(Unity.Mathematics.float3 c, Unity.Mathematics.float3 a, Unity.Mathematics.float3 b)
  RVA=0x09F3B33C  token=0x60008C5  System.Single ClosestPtPointSegmentRatioNoClamp(Unity.Mathematics.float3 c, Unity.Mathematics.float3 a, Unity.Mathematics.float3 b)
  RVA=0x09ED7F24  token=0x60008C6  Unity.Mathematics.float3 ClosestPtPointSegment(Unity.Mathematics.float3 c, Unity.Mathematics.float3 a, Unity.Mathematics.float3 b)
  RVA=0x09F3B1F4  token=0x60008C7  Unity.Mathematics.float3 ClosestPtPointSegmentNoClamp(Unity.Mathematics.float3 c, Unity.Mathematics.float3 a, Unity.Mathematics.float3 b)
  RVA=0x09EFEE4C  token=0x60008C8  System.Boolean IntersectPointPlane(Unity.Mathematics.float3 planePos, Unity.Mathematics.float3 planeDir, Unity.Mathematics.float3 pos, Unity.Mathematics.float3& outpos)
  RVA=0x09EFECEC  token=0x60008C9  System.Single IntersectPointPlaneDist(Unity.Mathematics.float3 planePos, Unity.Mathematics.float3 planeDir, Unity.Mathematics.float3 pos, Unity.Mathematics.float3& outPos)
  RVA=0x09F3DE04  token=0x60008CA  System.Boolean IntersectSegmentPlane(Unity.Mathematics.float3 a, Unity.Mathematics.float3 b, Unity.Mathematics.float3 p, Unity.Mathematics.float3 pn, Unity.Mathematics.float3& opos)
  RVA=0x09F3D5A0  token=0x60008CB  System.Boolean IntersectPointSphere(Unity.Mathematics.float3 sc, System.Single sr, Unity.Mathematics.float3 pos, Unity.Mathematics.float3& outPos)
  RVA=0x09F354EC  token=0x60008CC  System.Boolean IntersectPointSphere(Unity.Mathematics.float3 p, Unity.Mathematics.float3 sc, System.Single sr)
  RVA=0x09F3DC68  token=0x60008CD  System.Boolean IntersectRaySphere(Unity.Mathematics.float3 p, Unity.Mathematics.float3 d, Unity.Mathematics.float3 sc, System.Single sr, Unity.Mathematics.float3& q, System.Single& t)
  RVA=0x09F3CFE0  token=0x60008CE  System.Boolean IntersectLineSphare(Unity.Mathematics.float3 a, Unity.Mathematics.float3 b, Unity.Mathematics.float3 sc, System.Single sr, Unity.Mathematics.float3& q)
  RVA=0x09F3D6F0  token=0x60008CF  System.Boolean IntersectRayCone(Unity.Mathematics.float3 o, Unity.Mathematics.float3 d, Unity.Mathematics.float3 c, Unity.Mathematics.float3 v, System.Single cost, System.Single& t, Unity.Mathematics.float3& p)
  RVA=0x09F3C53C  token=0x60008D0  System.Boolean IntersectLineConeSurface(Unity.Mathematics.float3 a, Unity.Mathematics.float3 b, Unity.Mathematics.float3 d, System.Single dlen, Unity.Mathematics.float3 c, Unity.Mathematics.float3 v, System.Single cost, Unity.Mathematics.float3 c1, Unity.Mathematics.float3 c2, Unity.Mathematics.float3& p)
  RVA=0x09F3CBC8  token=0x60008D1  System.Boolean IntersectLineCylinderSurface(Unity.Mathematics.float3 sa, Unity.Mathematics.float3 sb, Unity.Mathematics.float3 p, Unity.Mathematics.float3 q, System.Single r, System.Single& t)
  RVA=0x09F3C6F8  token=0x60008D2  System.Boolean IntersectLineCylinderSurface(Unity.Mathematics.float3 a, Unity.Mathematics.float3 b, Unity.Mathematics.float3 c1, Unity.Mathematics.float3 c2, System.Single r1, System.Single r2, Unity.Mathematics.float3& p)
  RVA=0x09F3C1DC  token=0x60008D3  System.Boolean IntersectLineCapsule(Unity.Mathematics.float3 a, Unity.Mathematics.float3 b, Unity.Mathematics.float3 c1, Unity.Mathematics.float3 c2, System.Single r1, System.Single r2, Unity.Mathematics.float3& p)
  RVA=0x09F3F3E8  token=0x60008D4  System.Boolean IntersectTrianglePointDistance(Unity.Mathematics.float3 p, Unity.Mathematics.float3 p0, Unity.Mathematics.float3 p1, Unity.Mathematics.float3 p2, System.Single restDist, System.Single compressionStiffness, System.Single stretchStiffness, Unity.Mathematics.float3& corr, Unity.Mathematics.float3& corr0, Unity.Mathematics.float3& corr1, Unity.Mathematics.float3& corr2)
  RVA=0x09F3E95C  token=0x60008D5  System.Boolean IntersectTrianglePointDistanceSide(Unity.Mathematics.float3 p, Unity.Mathematics.float3 p0, Unity.Mathematics.float3 p1, Unity.Mathematics.float3 p2, System.Single restDist, System.Single compressionStiffness, System.Single stretchStiffness, System.Single side, Unity.Mathematics.float3& corr, Unity.Mathematics.float3& corr0, Unity.Mathematics.float3& corr1, Unity.Mathematics.float3& corr2)
  RVA=0x09F3DF80  token=0x60008D6  System.Boolean IntersectTrianglePointDistanceSide2(Unity.Mathematics.float3 p, Unity.Mathematics.float3 p0, Unity.Mathematics.float3 p1, Unity.Mathematics.float3 p2, System.Single radius, System.Single restDist, System.Single compressionStiffness, System.Single stretchStiffness, System.Single side, Unity.Mathematics.float3& corr, Unity.Mathematics.float3& corr0, Unity.Mathematics.float3& corr1, Unity.Mathematics.float3& corr2)
  RVA=0x09F3B904  token=0x60008D7  System.Single DistanceTrianglePoint(Unity.Mathematics.float3 p, Unity.Mathematics.float3 p0, Unity.Mathematics.float3 p1, Unity.Mathematics.float3 p2)
  RVA=0x066B2454  token=0x60008D8  Unity.Mathematics.float3 TriangleCenter(Unity.Mathematics.float3 p0, Unity.Mathematics.float3 p1, Unity.Mathematics.float3 p2)
  RVA=0x09ED8080  token=0x60008D9  Unity.Mathematics.float3 TriangleNormal(Unity.Mathematics.float3 p0, Unity.Mathematics.float3 p1, Unity.Mathematics.float3 p2)
  RVA=0x09F3FF60  token=0x60008DA  Unity.Mathematics.quaternion TriangleRotation(Unity.Mathematics.float3 p0, Unity.Mathematics.float3 p1, Unity.Mathematics.float3 p2)
  RVA=0x09F3FDBC  token=0x60008DB  Unity.Mathematics.quaternion TriangleCenterRotation(Unity.Mathematics.float3 p0, Unity.Mathematics.float3 p1, Unity.Mathematics.float3 p2, Unity.Mathematics.float3 p3)
  RVA=0x066AEDF8  token=0x60008DC  System.Single DistanceTriangleCenter(Unity.Mathematics.float3 p, Unity.Mathematics.float3 p0, Unity.Mathematics.float3 p1, Unity.Mathematics.float3 p2)
  RVA=0x066AECDC  token=0x60008DD  System.Single DirectionPointTriangle(Unity.Mathematics.float3 p, Unity.Mathematics.float3 a, Unity.Mathematics.float3 b, Unity.Mathematics.float3 c)
  RVA=0x09F3D190  token=0x60008DE  System.Boolean IntersectLineTriangle(Unity.Mathematics.float3 p, Unity.Mathematics.float3 q, Unity.Mathematics.float3 a, Unity.Mathematics.float3 b, Unity.Mathematics.float3 c, Unity.Mathematics.float3& hitpos, System.Single& t, Unity.Mathematics.float3& n)
  RVA=0x09F3B418  token=0x60008DF  System.Single ClosestPtSegmentSegment(Unity.Mathematics.float3 p1, Unity.Mathematics.float3 q1, Unity.Mathematics.float3 p2, Unity.Mathematics.float3 q2, System.Single& s, System.Single& t, Unity.Mathematics.float3& c1, Unity.Mathematics.float3& c2)
  RVA=0x09F3C184  token=0x60008E0  System.Single GetBezierValue(MagicaCloth.BezierParam bparam, System.Single t)
  RVA=0x09F35460  token=0x60008E1  System.Single GetBezierValue(System.Single sval, System.Single eval, System.Single curve, System.Single t)
END_CLASS

CLASS: MagicaCloth.MeshUtility
TYPE:  static class
TOKEN: 0x2000127
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09F45224  token=0x60008E2  UnityEngine.GameObject ReplaceSkinnedMeshRendererToMeshRenderer(UnityEngine.SkinnedMeshRenderer sren, System.Boolean replaceSkinnedMeshRenderer)
  RVA=0x09F40E84  token=0x60008E3  System.Boolean CalcMeshWorldPositionNormalTangent(MagicaCloth.MeshData meshData, System.Collections.Generic.List<UnityEngine.Transform> boneList, System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList)
  RVA=0x09F414C4  token=0x60008E4  System.Boolean CalcMeshWorldPositionNormalTangent(UnityEngine.Renderer ren, UnityEngine.Mesh mesh, System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList)
  RVA=0x09F401DC  token=0x60008E5  System.Boolean CalcMeshLocalNormalTangent(System.Collections.Generic.List<System.Int32> selectList, UnityEngine.Vector3[] vlist, UnityEngine.Vector2[] uvlist, System.Int32[] triangles, System.Collections.Generic.List<UnityEngine.Vector3>& lnorList, System.Collections.Generic.List<UnityEngine.Vector3>& ltanList)
  RVA=0x09F446EC  token=0x60008E6  System.Collections.Generic.List<System.Collections.Generic.HashSet<System.Int32>> GetTriangleToVertexLinkList(System.Int32 vcnt, System.Collections.Generic.List<System.Int32> lineList, System.Collections.Generic.List<System.Int32> triangleList)
  RVA=0x09F44E00  token=0x60008E7  System.Collections.Generic.List<System.Collections.Generic.HashSet<System.Int32>> GetVertexLinkList(System.Int32 vcnt, System.Collections.Generic.HashSet<System.UInt32> lineSet)
  RVA=0x09F45080  token=0x60008E8  System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.HashSet<System.Int32>> GetVertexToTriangles(System.Collections.Generic.List<System.Int32> triangleList)
  RVA=0x09F4560C  token=0x60008E9  System.Int32 RestTriangleVertex(System.Int32 tindex, System.Int32 v0, System.Int32 v1, System.Collections.Generic.List<System.Int32> triangleList)
  RVA=0x09F45508  token=0x60008EA  System.Void RestTriangleVertex(System.Int32 tindex, System.Int32 v0, System.Collections.Generic.List<System.Int32> triangleList, System.Int32& v1, System.Int32& v2)
  RVA=0x09F440DC  token=0x60008EB  System.Boolean CheckAdjacentTriangle(System.Int32 tindex0, System.Int32 tindex1, System.Collections.Generic.List<System.Int32> triangleList)
  RVA=0x09F44444  token=0x60008EC  System.Collections.Generic.Dictionary<System.UInt32,System.Collections.Generic.List<System.Int32>> GetTriangleEdgePair(System.Collections.Generic.List<System.Int32> triangleList)
  RVA=0x09F400BC  token=0x60008ED  System.Void AddTriangleEdge(System.Int32 v0, System.Int32 v1, System.Int32 tindex, System.Collections.Generic.Dictionary<System.UInt32,System.Collections.Generic.List<System.Int32>> triangleEdgeDict)
  RVA=0x09F445B0  token=0x60008EE  System.Collections.Generic.List<System.UInt64> GetTrianglePackList(System.Collections.Generic.List<System.Int32> triangleList)
  RVA=0x09F441A0  token=0x60008EF  System.Single ClosestPtBoneLine(UnityEngine.Vector3 pos, UnityEngine.Transform bone, System.Single lineWidth, UnityEngine.Vector3& d)
  RVA=0x09F44A60  token=0x60008F0  System.Collections.Generic.List<UnityEngine.Transform> GetUseBoneTransformList(UnityEngine.Transform[] bones, UnityEngine.Mesh mesh)
  RVA=0x09F41F40  token=0x60008F1  System.Void CalcTetraMesh(System.Collections.Generic.List<UnityEngine.Vector3> posList, System.Int32& tetraCount, System.Collections.Generic.List<System.Int32>& tetraIndexList, System.Collections.Generic.List<System.Single>& tetraSizeList)
  RVA=-1  // generic def  token=0x60008F2  UnityEngine.Transform GetReplaceBone(UnityEngine.Transform now, System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict)
END_CLASS

CLASS: MagicaCloth.StaticStringBuilder
TYPE:  class
TOKEN: 0x200012A
SIZE:  0x10
FIELDS:
  private   static  System.Text.StringBuilder       stringBuilder  // static @ 0x0
PROPERTIES:
  Instance  get=0x09F4BA0C
METHODS:
  RVA=0x09F4B8D8  token=0x6000900  System.Void Clear()
  RVA=0x09F4B838  token=0x6000901  System.Text.StringBuilder Append(System.Object[] args)
  RVA=0x09F4B60C  token=0x6000902  System.Text.StringBuilder AppendLine(System.Object[] args)
  RVA=0x09F4B6E0  token=0x6000903  System.Text.StringBuilder AppendLine()
  RVA=0x09F4B75C  token=0x6000904  System.String AppendToString(System.Object[] args)
  RVA=0x09F4B930  token=0x6000905  System.String ToString()
  RVA=0x041E1670  token=0x6000906  System.Void .ctor()
  RVA=0x09F4B988  token=0x6000907  System.Void .cctor()
END_CLASS

CLASS: MagicaCloth.UpdateTimeManager
TYPE:  class
TOKEN: 0x200012B
SIZE:  0x30
FIELDS:
  private           MagicaCloth.UpdateTimeManager.UpdateCountupdatePerSeccond  // 0x10
  private           MagicaCloth.UpdateTimeManager.UpdateModeupdateMode  // 0x14
  private           MagicaCloth.UpdateTimeManager.UpdateLocationupdateLocation  // 0x18
  private           System.Single                   timeScale  // 0x1c
  private           System.Single                   futurePredictionRate  // 0x20
  private           System.Boolean                  updateBoneScale  // 0x24
  private           System.Int32                    fixedUpdateCount  // 0x28
PROPERTIES:
  FixedUpdateCount  get=0x011F2A90
  UpdatePerSecond  get=0x02B2E2D0
  UpdateIntervalTime  get=0x04DA3880
  UpdatePower  get=0x04DA38A0
  TimeScale  get=0x04D868F0  set=0x09F4C9E8
  DeltaTime  get=0x05391A8C
  PhysicsDeltaTime  get=0x03233AF0
  AverageDeltaTime  get=0x09F4C9AC
  IsUnscaledUpdate  get=0x09F4C9B4
  IsDelay  get=0x03233A70
  FuturePredictionRate  get=0x011EC8A0  set=0x09F4C9D0
  UpdateBoneScale  get=0x04D86870  set=0x04D86880
  WorkerMaximumCount  get=0x09F4C9C8
METHODS:
  RVA=0x04DA3850  token=0x6000908  System.Void ResetFixedUpdateCount()
  RVA=0x04D9EBD0  token=0x6000909  System.Void AddFixedUpdateCount()
  RVA=0x04D882A0  token=0x600090B  MagicaCloth.UpdateTimeManager.UpdateMode GetUpdateMode()
  RVA=0x04D8F540  token=0x600090C  System.Void SetUpdateMode(MagicaCloth.UpdateTimeManager.UpdateMode mode)
  RVA=0x011EF5B0  token=0x600090D  MagicaCloth.UpdateTimeManager.UpdateLocation GetUpdateLocation()
  RVA=0x011EF9B0  token=0x600090E  System.Void SetUpdateLocation(MagicaCloth.UpdateTimeManager.UpdateLocation location)
  RVA=0x014F51F0  token=0x6000910  System.Void SetUpdatePerSecond(MagicaCloth.UpdateTimeManager.UpdateCount ucount)
  RVA=0x04DA3860  token=0x600091F  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.ReferenceDataBuilder`1
TYPE:  class
TOKEN: 0x200012F
FIELDS:
  private           System.Int32                    indexCount  // 0x0
  private           System.Collections.Generic.List<T>dataList  // 0x0
  private           System.Collections.Generic.List<System.Collections.Generic.List<System.Int32>>indexToDataIndexList  // 0x0
  private           System.Collections.Generic.List<System.Collections.Generic.List<System.Int32>>dataIndexToIndexList  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000920  System.Void Init(System.Int32 indexCount)
  RVA=-1  // not resolved  token=0x6000921  System.Void AddData(T data, System.Int32[] indexes)
  RVA=-1  // not resolved  token=0x6000922  System.ValueTuple<System.Collections.Generic.List<MagicaCloth.ReferenceDataIndex>,System.Collections.Generic.List<T>> GetDirectReferenceData()
  RVA=-1  // not resolved  token=0x6000923  System.ValueTuple<System.Collections.Generic.List<MagicaCloth.ReferenceDataIndex>,System.Collections.Generic.List<System.Int32>,System.Collections.Generic.List<System.Collections.Generic.List<System.Int32>>> GetIndirectReferenceData()
  RVA=-1  // not resolved  token=0x6000924  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.ReferenceDataIndex
TYPE:  sealed struct
TOKEN: 0x2000130
SIZE:  0x18
FIELDS:
  public            System.Int32                    startIndex  // 0x10
  public            System.Int32                    count  // 0x14
METHODS:
END_CLASS

CLASS: MagicaCloth.IDataHash
TYPE:  interface
TOKEN: 0x2000131
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000925  System.Int32 GetDataHash()
END_CLASS

CLASS: MagicaCloth.IShareDataObject
TYPE:  interface
TOKEN: 0x2000132
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000926  System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject()
  RVA=-1  // abstract  token=0x6000927  MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source)
END_CLASS

CLASS: MagicaCloth.ShareDataObject
TYPE:  abstract class
TOKEN: 0x2000133
SIZE:  0x20
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: MagicaCloth.IDataVerify MagicaCloth.IDataHash
FIELDS:
  protected         System.Int32                    dataHash  // 0x18
  protected         System.Int32                    dataVersion  // 0x1c
PROPERTIES:
  SaveDataHash  get=0x011EF5B0  set=0x011EF9B0
  SaveDataVersion  get=0x04D86310  set=0x04D86320
METHODS:
  RVA=-1  // abstract  token=0x6000928  System.Int32 GetDataHash()
  RVA=-1  // abstract  token=0x600092D  System.Int32 GetVersion()
  RVA=-1  // abstract  token=0x600092E  MagicaCloth.Define.Error VerifyData()
  RVA=0x09F4B5B0  token=0x600092F  System.Void CreateVerifyData()
  RVA=0x09F4B5E0  token=0x6000930  System.String GetInformation()
  RVA=-1  // generic def  token=0x6000931  T CreateShareData(System.String dataName)
  RVA=-1  // generic def  token=0x6000932  System.Boolean RemoveNullAndDuplication(System.Collections.Generic.List<T> data)
  RVA=-1  // generic def  token=0x6000933  T Clone(T source)
  RVA=0x03573370  token=0x6000934  System.Void .ctor()
END_CLASS

CLASS: MagicaCloth.StopWatch
TYPE:  class
TOKEN: 0x2000134
SIZE:  0x18
FIELDS:
  private           System.Single                   startTime  // 0x10
  private           System.Single                   endTime  // 0x14
PROPERTIES:
  ElapsedSeconds  get=0x04DA3820
  ElapsedMilliseconds  get=0x04DA3800
METHODS:
  RVA=0x09F4BA5C  token=0x6000935  MagicaCloth.StopWatch Start()
  RVA=0x09F4BA7C  token=0x6000936  MagicaCloth.StopWatch Stop()
  RVA=0x041E1670  token=0x6000939  System.Void .ctor()
END_CLASS

CLASS: MagicaReductionMesh.ReductionMesh
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x30
FIELDS:
  private           MagicaReductionMesh.ReductionMesh.ReductionWeightMode<WeightMode>k__BackingField  // 0x10
  private           MagicaReductionMesh.MeshData    meshData  // 0x18
  private           MagicaReductionMesh.ReductionDatareductionData  // 0x20
  private           MagicaReductionMesh.DebugData   debugData  // 0x28
PROPERTIES:
  WeightMode  get=0x02B2E2D0  set=0x014F51F0
  MeshData  get=0x09ED1A44
  ReductionData  get=0x04A379B0
  DebugData  get=0x09ED1A18
METHODS:
  RVA=0x09ED17D0  token=0x6000008  System.Int32 AddMesh(System.Boolean isSkinning, UnityEngine.Mesh mesh, System.Collections.Generic.List<UnityEngine.Transform> bones, UnityEngine.Matrix4x4[] bindPoseList, UnityEngine.BoneWeight[] boneWeightList)
  RVA=0x09ED1554  token=0x6000009  System.Int32 AddMesh(UnityEngine.Renderer ren)
  RVA=0x09ED14E0  token=0x600000A  System.Int32 AddMesh(UnityEngine.Transform root, System.Collections.Generic.List<UnityEngine.Vector3> posList, System.Collections.Generic.List<UnityEngine.Vector3> norList, System.Collections.Generic.List<UnityEngine.Vector4> tanList, System.Collections.Generic.List<UnityEngine.Vector2> uvList, System.Collections.Generic.List<System.Int32> triangleList)
  RVA=0x09ED1868  token=0x600000B  System.Void Reduction(System.Single zeroRadius, System.Single radius, System.Single polygonLength, System.Boolean createTetra)
  RVA=0x09ED1838  token=0x600000C  MagicaReductionMesh.FinalData GetFinalData(UnityEngine.Transform root)
  RVA=0x09ED1960  token=0x600000D  System.Void .ctor()
END_CLASS

CLASS: MagicaReductionMesh.ReductionMeshAccess
TYPE:  abstract class
TOKEN: 0x2000006
SIZE:  0x18
FIELDS:
  protected         MagicaReductionMesh.ReductionMeshparent  // 0x10
PROPERTIES:
  MeshData  get=0x09ED14A0
  ReductionData  get=0x09ED14C0
  DebugData  get=0x09ED1480
METHODS:
  RVA=0x053908C0  token=0x6000011  System.Void SetParent(MagicaReductionMesh.ReductionMesh parent)
  RVA=0x041E1670  token=0x6000012  System.Void .ctor()
END_CLASS

CLASS: MagicaReductionMesh.DebugData
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x18
EXTENDS: MagicaReductionMesh.ReductionMeshAccess
FIELDS:
METHODS:
  RVA=0x09EC2B90  token=0x6000013  System.Void DispMeshInfo(System.String header)
  RVA=0x09EC1FCC  token=0x6000014  System.Void DebugDrawShared(MagicaReductionMesh.FinalData final, System.Boolean drawTriangle, System.Boolean drawLine, System.Boolean drawTetra, System.Boolean drawVertexNormal, System.Boolean drawVertexTangent, System.Boolean drawNumber, System.Int32 maxPolygonCount, System.Int32 layer, System.Int32 tetraIndex, System.Single tetraSize, System.Collections.Generic.List<System.Int32> drawNumberList, System.Single axisSize)
  RVA=0x09EC2E3C  token=0x6000015  System.Void DrawTetra(MagicaReductionMesh.FinalData final, System.Int32 tetraIndex, System.Collections.Generic.List<UnityEngine.Vector3> wposList, System.Single tetraSize)
  RVA=0x09EC1B24  token=0x6000016  System.Void DebugDrawChild(MagicaReductionMesh.FinalData final, System.Boolean drawPosition, System.Boolean drawNormal, System.Boolean drawTriangle, System.Boolean drawNumber, System.Int32 maxVertexCount, System.Single positionSize, System.Single axisSize)
  RVA=0x041E1670  token=0x6000017  System.Void .ctor()
END_CLASS

CLASS: MagicaReductionMesh.FinalData
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x98
FIELDS:
  public            System.Collections.Generic.List<UnityEngine.Vector3>vertices  // 0x10
  public            System.Collections.Generic.List<UnityEngine.Vector3>normals  // 0x18
  public            System.Collections.Generic.List<UnityEngine.Vector4>tangents  // 0x20
  public            System.Collections.Generic.List<UnityEngine.Vector2>uvs  // 0x28
  public            System.Collections.Generic.List<UnityEngine.BoneWeight>boneWeights  // 0x30
  public            System.Collections.Generic.List<UnityEngine.Matrix4x4>bindPoses  // 0x38
  public            System.Collections.Generic.List<UnityEngine.Transform>bones  // 0x40
  public            System.Collections.Generic.List<System.Int32>lines  // 0x48
  public            System.Collections.Generic.List<System.Int32>triangles  // 0x50
  public            System.Collections.Generic.List<System.Int32>tetras  // 0x58
  public            System.Collections.Generic.List<System.Single>tetraSizes  // 0x60
  public            System.Collections.Generic.List<UnityEngine.Matrix4x4>vertexBindPoses  // 0x68
  public            System.Collections.Generic.List<MagicaReductionMesh.FinalData.MeshIndexData>vertexToMeshIndexList  // 0x70
  public            System.Collections.Generic.List<System.Int32>vertexToTriangleCountList  // 0x78
  public            System.Collections.Generic.List<System.Int32>vertexToTriangleStartList  // 0x80
  public            System.Collections.Generic.List<System.Int32>vertexToTriangleIndexList  // 0x88
  public            System.Collections.Generic.List<MagicaReductionMesh.FinalData.MeshInfo>meshList  // 0x90
PROPERTIES:
  IsValid  get=0x09EC35C4
  VertexCount  get=0x09EC3720
  LineCount  get=0x09EC3608
  TriangleCount  get=0x09EC36D4
  TetraCount  get=0x09EC368C
  BoneCount  get=0x09EC3584
  IsSkinning  get=0x02FFF600
  MeshCount  get=0x09EC364C
METHODS:
  RVA=0x09EC30E4  token=0x6000020  System.Void .ctor()
END_CLASS

CLASS: MagicaReductionMesh.MeshData
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x68
EXTENDS: MagicaReductionMesh.ReductionMeshAccess
FIELDS:
  public            System.Collections.Generic.List<MagicaReductionMesh.MeshData.Vertex>originalVertexList  // 0x18
  public            System.Collections.Generic.List<MagicaReductionMesh.MeshData.MeshInfo>meshInfoList  // 0x20
  public            System.Collections.Generic.List<MagicaReductionMesh.MeshData.ShareVertex>shareVertexList  // 0x28
  private           System.Collections.Generic.Dictionary<System.UInt64,MagicaReductionMesh.MeshData.Triangle>triangleDict  // 0x30
  private           System.Collections.Generic.Dictionary<System.UInt32,MagicaReductionMesh.MeshData.Line>lineDict  // 0x38
  public            System.Collections.Generic.List<UnityEngine.Transform>boneList  // 0x40
  private           System.Collections.Generic.List<MagicaReductionMesh.MeshData.Tetra>tetraList  // 0x48
  private           System.Single                   weightPow  // 0x50
  private           System.Int32                    maxWeightCount  // 0x54
  private           System.Single                   sameSurfaceAngle  // 0x58
  private           System.Boolean                  removeSameTrianglePair  // 0x5c
  private           System.Single                   removeSameTrianglePairAngle  // 0x60
PROPERTIES:
  VertexCount  get=0x09ECE42C
  LineCount  get=0x09ECE32C
  TriangleCount  get=0x09ECE3EC
  TetraCount  get=0x09ECE3AC
  MeshCount  get=0x09ECE36C
  WeightPow  get=0x04D885A0  set=0x04D88FF0
  MaxWeightCount  get=0x04D86540  set=0x04D86590
  SameSurfaceAngle  get=0x04D88400  set=0x04D88430
  RemoveSameTrianglePair  get=0x04D86D90  set=0x04D86DB0
  RemoveSameTrianglePairAngle  get=0x04D88480  set=0x04D884A0
METHODS:
  RVA=0x09EC3F38  token=0x6000033  System.Int32 AddMesh(System.Boolean isSkinning, UnityEngine.Mesh mesh, System.Collections.Generic.List<UnityEngine.Transform> bones, UnityEngine.Matrix4x4[] bindPoseList, UnityEngine.BoneWeight[] boneWeightList)
  RVA=0x09EC37D8  token=0x6000034  System.Int32 AddMesh(UnityEngine.Transform root, System.Collections.Generic.List<UnityEngine.Vector3> posList, System.Collections.Generic.List<UnityEngine.Vector3> norList, System.Collections.Generic.List<UnityEngine.Vector4> tanList, System.Collections.Generic.List<UnityEngine.Vector2> uvList, System.Collections.Generic.List<System.Int32> triangleList)
  RVA=0x09EC6F7C  token=0x6000035  System.Void CombineVertex(MagicaReductionMesh.MeshData.ShareVertex sv0, MagicaReductionMesh.MeshData.ShareVertex sv1)
  RVA=0x09ECE064  token=0x6000036  System.Void UpdateMeshData(System.Boolean createTetra)
  RVA=0x09EC6B7C  token=0x6000037  System.Void CalcVertexIndex()
  RVA=0x09EC67A4  token=0x6000038  System.Void CalcUV(MagicaReductionMesh.MeshData.UvWrapMode wrapMode)
  RVA=0x09EC9C54  token=0x6000039  System.Void CreateTriangleAndLine()
  RVA=0x09EC63C0  token=0x600003A  System.Void CalcShareVertexWeight()
  RVA=0x09EC4C7C  token=0x600003B  System.Void AdjustTriangleNormal()
  RVA=0x09EC6E38  token=0x600003C  System.Boolean CheckTwoTriangleOpen(MagicaReductionMesh.MeshData.Triangle tri1, MagicaReductionMesh.MeshData.Triangle tri2, System.UInt32 edge)
  RVA=0x09EC64F8  token=0x600003D  System.Single CalcTwoTriangleAngle(MagicaReductionMesh.MeshData.Triangle tri1, MagicaReductionMesh.MeshData.Triangle tri2, System.UInt32 edge)
  RVA=0x09ECB04C  token=0x600003E  System.Collections.Generic.Dictionary<System.UInt32,System.Collections.Generic.List<System.UInt64>> GetEdgeToTriangleDict()
  RVA=0x09EC6D24  token=0x600003F  System.Void CalcVertexNormalFromTriangle()
  RVA=0x09ECD6D4  token=0x6000040  System.Void RemoveTriangle(System.UInt64 thash)
  RVA=0x09ECCD54  token=0x6000041  System.Collections.Generic.Dictionary<System.UInt64,System.Collections.Generic.List<MagicaReductionMesh.MeshData.Square>> GetSquareDict()
  RVA=0x09ECD5C4  token=0x6000042  System.Void RemoveOverlappingSquareTriangles()
  RVA=0x09EC83A8  token=0x6000043  System.Void CreateTetraMesh()
  RVA=0x09EC5728  token=0x6000044  UnityEngine.Bounds CalcBounding()
  RVA=0x09ECB574  token=0x6000045  MagicaReductionMesh.FinalData GetFinalData(UnityEngine.Transform root)
  RVA=0x09EC72AC  token=0x6000046  System.Void CreateOriginalMeshInfo(MagicaReductionMesh.FinalData final, UnityEngine.Transform root, System.Single weightLength)
  RVA=0x09EC4BE4  token=0x6000047  System.Void AddVertexToMeshIndexData(MagicaReductionMesh.FinalData final, System.Int32 sindex, System.Int32 meshIndex, System.Int32 meshVertexIndex)
  RVA=0x09ECD8EC  token=0x6000048  System.Collections.Generic.List<MagicaReductionMesh.MeshData.ShareVertex> SearchNearPointList(UnityEngine.Vector3 basePos, MagicaReductionMesh.MeshData.ShareVertex sv, System.Single weightLength, System.Int32 maxCount)
  RVA=0x09EC593C  token=0x6000049  System.Void CalcMeshWorldPositionNormalTangent(System.Boolean isSkinning, UnityEngine.Mesh mesh, System.Collections.Generic.List<UnityEngine.Transform> bones, UnityEngine.Matrix4x4[] bindPoseList, UnityEngine.BoneWeight[] boneWeightList, System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector4>& wtanList)
  RVA=0x09ECE0CC  token=0x600004A  System.Void .ctor()
END_CLASS

CLASS: MagicaReductionMesh.ReductionData
TYPE:  class
TOKEN: 0x200001F
SIZE:  0x18
EXTENDS: MagicaReductionMesh.ReductionMeshAccess
FIELDS:
METHODS:
  RVA=0x09ED13F4  token=0x6000087  System.Void ReductionZeroDistance(System.Single radius)
  RVA=0x09ED1368  token=0x6000088  System.Void ReductionRadius(System.Single radius)
  RVA=0x09ED12DC  token=0x6000089  System.Void ReductionPolygonLink(System.Single length)
  RVA=0x09ED0E10  token=0x600008A  System.Void ReductionBone()
  RVA=0x041E1670  token=0x600008B  System.Void .ctor()
END_CLASS

CLASS: MagicaReductionMesh.NearPointReduction
TYPE:  class
TOKEN: 0x2000020
SIZE:  0x38
FIELDS:
  protected         MagicaReductionMesh.MeshData    meshData  // 0x10
  private           System.Collections.Generic.List<MagicaReductionMesh.NearPointReduction.Point>pointList  // 0x18
  protected         System.Collections.Generic.Dictionary<UnityEngine.Vector3Int,System.Collections.Generic.List<MagicaReductionMesh.NearPointReduction.Point>>gridMap  // 0x20
  protected         System.Single                   gridSize  // 0x28
  private           System.Single                   searchRadius  // 0x2c
  private           System.Collections.Generic.Dictionary<MagicaReductionMesh.NearPointReduction.Point,System.Collections.Generic.List<MagicaReductionMesh.NearPointReduction.Point>>nearPointDict  // 0x30
PROPERTIES:
  PointCount  get=0x09ECFEA8
METHODS:
  RVA=0x09ECFD7C  token=0x600008C  System.Void .ctor(System.Single radius)
  RVA=0x09ECE974  token=0x600008E  System.Void Create(MagicaReductionMesh.MeshData meshData)
  RVA=0x09ECED18  token=0x600008F  System.Void Reduction()
  RVA=0x09ECE8B8  token=0x6000090  MagicaReductionMesh.NearPointReduction.Point AddPoint(MagicaReductionMesh.MeshData.ShareVertex sv, UnityEngine.Vector3 pos)
  RVA=0x09ECE728  token=0x6000091  System.Void AddGrid(MagicaReductionMesh.NearPointReduction.Point p)
  RVA=0x09ECF25C  token=0x6000092  System.Void RemoveGrid(MagicaReductionMesh.NearPointReduction.Point p)
  RVA=0x09ECECC0  token=0x6000093  System.Void Move(MagicaReductionMesh.NearPointReduction.Point p, UnityEngine.Vector3 newpos)
  RVA=0x09ECF3DC  token=0x6000094  System.Void Remove(MagicaReductionMesh.NearPointReduction.Point p)
  RVA=0x09ECEAE8  token=0x6000095  UnityEngine.Vector3Int GetGridPos(UnityEngine.Vector3 pos)
  RVA=0x09ECF440  token=0x6000096  System.Void SearchNearPointAll()
  RVA=0x09ECF6D4  token=0x6000097  System.Void SearchNearPoint(MagicaReductionMesh.NearPointReduction.Point p, System.Single radius, MagicaReductionMesh.NearPointReduction.Point ignorePoint)
  RVA=0x09ECEB60  token=0x6000098  MagicaReductionMesh.NearPointReduction.Point GetNearPointPair()
END_CLASS

CLASS: MagicaReductionMesh.PolygonLinkReduction
TYPE:  class
TOKEN: 0x2000022
SIZE:  0x30
FIELDS:
  protected         MagicaReductionMesh.MeshData    meshData  // 0x10
  private           System.Single                   reductionLength  // 0x18
  private           System.Collections.Generic.List<MagicaReductionMesh.PolygonLinkReduction.Point>pointList  // 0x20
  private           System.Collections.Generic.Dictionary<MagicaReductionMesh.MeshData.ShareVertex,MagicaReductionMesh.PolygonLinkReduction.Point>pointDict  // 0x28
PROPERTIES:
  PointCount  get=0x09ED0DD0
METHODS:
  RVA=0x09ED0D00  token=0x600009A  System.Void .ctor(System.Single length)
  RVA=0x09ECFF9C  token=0x600009C  System.Void Create(MagicaReductionMesh.MeshData meshData)
  RVA=0x09ED02CC  token=0x600009D  System.Void Reduction()
  RVA=0x09ECFEE8  token=0x600009E  System.Void AddPoint(MagicaReductionMesh.MeshData.ShareVertex sv)
  RVA=0x09ED0248  token=0x600009F  MagicaReductionMesh.PolygonLinkReduction.Point GetPoint(MagicaReductionMesh.MeshData.ShareVertex sv)
  RVA=0x09ED0904  token=0x60000A0  System.Void Remove(MagicaReductionMesh.PolygonLinkReduction.Point p)
  RVA=0x09ED0984  token=0x60000A1  System.Void SearchNearPointAll()
  RVA=0x09ED0A98  token=0x60000A2  System.Void SearchNearPoint(MagicaReductionMesh.PolygonLinkReduction.Point p)
  RVA=0x09ED00E8  token=0x60000A3  MagicaReductionMesh.PolygonLinkReduction.Point GetNearPointPair()
END_CLASS

CLASS: MagicaReductionMesh.Utility
TYPE:  class
TOKEN: 0x2000024
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0677F488  token=0x60000A5  System.UInt32 PackPair(System.Int32 v0, System.Int32 v1)
  RVA=0x04DA3080  token=0x60000A6  System.Void UnpackPair(System.UInt32 pack, System.Int32& v0, System.Int32& v1)
  RVA=0x09ED7AFC  token=0x60000A7  System.UInt64 PackTriple(System.Int32 v0, System.Int32 v1, System.Int32 v2)
  RVA=0x04DA30C0  token=0x60000A8  System.Void UnpackTriple(System.UInt64 pack, System.Int32& v0, System.Int32& v1, System.Int32& v2)
  RVA=0x09ED7984  token=0x60000A9  System.UInt64 PackQuater(System.Int32 v0, System.Int32 v1, System.Int32 v2, System.Int32 v3)
  RVA=0x04DA3090  token=0x60000AA  System.Void UnpackQuater(System.UInt64 pack, System.Int32& v0, System.Int32& v1, System.Int32& v2, System.Int32& v3)
  RVA=0x04D874D0  token=0x60000AB  System.UInt32 Pack16(System.Int32 hi, System.Int32 low)
  RVA=0x04D875B0  token=0x60000AC  System.Int32 Unpack16Hi(System.UInt32 pack)
  RVA=0x04D875C0  token=0x60000AD  System.Int32 Unpack16Low(System.UInt32 pack)
  RVA=0x04D91CF0  token=0x60000AE  System.UInt64 Pack32(System.Int32 hi, System.Int32 low)
  RVA=0x04D9D8A0  token=0x60000AF  System.Int32 Unpack32Hi(System.UInt64 pack)
  RVA=0x04D88BE0  token=0x60000B0  System.Int32 Unpack32Low(System.UInt64 pack)
  RVA=0x09ED6D78  token=0x60000B1  System.Void CalcFinalDataWorldPositionNormalTangent(MagicaReductionMesh.FinalData final, System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector4>& wtanList)
  RVA=0x09ED6224  token=0x60000B2  System.Void CalcFinalDataChildWorldPositionNormalTangent(MagicaReductionMesh.FinalData final, System.Int32 meshIndex, System.Collections.Generic.List<UnityEngine.Vector3> sposList, System.Collections.Generic.List<UnityEngine.Vector3> snorList, System.Collections.Generic.List<UnityEngine.Vector4> stanList, System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector4>& wtanList)
  RVA=0x09ED770C  token=0x60000B3  System.Void CalcLocalPositionNormalTangent(UnityEngine.Transform root, System.Collections.Generic.List<UnityEngine.Vector3> wposList, System.Collections.Generic.List<UnityEngine.Vector3> wnorList, System.Collections.Generic.List<UnityEngine.Vector4> wtanList)
  RVA=0x041E1670  token=0x60000B4  System.Void .ctor()
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

CLASS: System.Runtime.CompilerServices.IsUnmanagedAttribute
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000002  System.Void .ctor()
END_CLASS

