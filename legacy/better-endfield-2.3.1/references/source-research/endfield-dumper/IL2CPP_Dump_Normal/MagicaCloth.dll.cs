// ========================================================
// Dumped by @desirepro
// Assembly: MagicaCloth.dll
// Classes:  308
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000005  // size: 0x14
    public sealed struct ReductionWeightMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaReductionMesh.ReductionMesh.ReductionWeightMode Distance;  // const
        public static MagicaReductionMesh.ReductionMesh.ReductionWeightMode Average;  // const
        public static MagicaReductionMesh.ReductionMesh.ReductionWeightMode DistanceAverage;  // const

    }

    // TypeToken: 0x2000009  // size: 0x18
    public class MeshIndexData
    {
        // Fields
        public System.Collections.Generic.List<System.UInt32> meshIndexPackList;  // 0x10

        // Methods
        // RVA: 0x09ECE46C  token: 0x6000021
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x48
    public class MeshInfo
    {
        // Fields
        public System.Int32 meshIndex;  // 0x10
        public UnityEngine.Mesh mesh;  // 0x18
        public System.Collections.Generic.List<UnityEngine.Vector3> vertices;  // 0x20
        public System.Collections.Generic.List<UnityEngine.Vector3> normals;  // 0x28
        public System.Collections.Generic.List<UnityEngine.Vector4> tangents;  // 0x30
        public System.Collections.Generic.List<UnityEngine.BoneWeight> boneWeights;  // 0x38
        public System.Collections.Generic.List<System.Int32> parents;  // 0x40

        // Properties
        System.Int32 VertexCount { get; /* RVA: 0x09ECE6E8 */ }

        // Methods
        // RVA: 0x09ECE4E4  token: 0x6000023
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000C  // size: 0x18
    public class WeightData
    {
        // Fields
        public System.Int32 boneIndex;  // 0x10
        public System.Single boneWeight;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x600004B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000D  // size: 0x58
    public class Vertex
    {
        // Fields
        public System.Int32 meshIndex;  // 0x10
        public System.Int32 vertexIndex;  // 0x14
        public UnityEngine.Vector3 wpos;  // 0x18
        public UnityEngine.Vector3 wnor;  // 0x24
        public UnityEngine.Vector3 wtan;  // 0x30
        public System.Single tanw;  // 0x3c
        public UnityEngine.Vector2 uv;  // 0x40
        public System.Int32 parentIndex;  // 0x48
        public System.Collections.Generic.List<MagicaReductionMesh.MeshData.WeightData> boneWeightList;  // 0x50

        // Methods
        // RVA: 0x09ED7C38  token: 0x600004C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000E  // size: 0x30
    public class MeshInfo
    {
        // Fields
        public System.Int32 index;  // 0x10
        public UnityEngine.Mesh mesh;  // 0x18
        public System.Int32 vertexCount;  // 0x20
        public System.Collections.Generic.List<MagicaReductionMesh.MeshData.Vertex> vertexList;  // 0x28

        // Methods
        // RVA: 0x09ECE670  token: 0x600004D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000010  // size: 0x18
    public sealed class <>c__DisplayClass20_0
    {
        // Fields
        public MagicaReductionMesh.MeshData.WeightData w;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600005C
        public System.Void .ctor() { }
        // RVA: 0x09ED609C  token: 0x600005D
        private System.Boolean <CalcBoneWeight_DistanceAverage>b__1(MagicaReductionMesh.MeshData.WeightData wdata) { }

    }

    // TypeToken: 0x2000011  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly MagicaReductionMesh.MeshData.ShareVertex.<>c <>9;  // static @ 0x0
        public static System.Comparison<MagicaReductionMesh.MeshData.WeightData> <>9__20_0;  // static @ 0x8
        public static System.Comparison<MagicaReductionMesh.MeshData.WeightData> <>9__21_0;  // static @ 0x10
        public static System.Comparison<MagicaReductionMesh.MeshData.WeightData> <>9__23_0;  // static @ 0x18

        // Methods
        // RVA: 0x09ED61C0  token: 0x600005E
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600005F
        public System.Void .ctor() { }
        // RVA: 0x09ED6034  token: 0x6000060
        private System.Int32 <CalcBoneWeight_DistanceAverage>b__20_0(MagicaReductionMesh.MeshData.WeightData a, MagicaReductionMesh.MeshData.WeightData b) { }
        // RVA: 0x09ED6034  token: 0x6000061
        private System.Int32 <CalcBoneWeight_Average>b__21_0(MagicaReductionMesh.MeshData.WeightData a, MagicaReductionMesh.MeshData.WeightData b) { }
        // RVA: 0x09ED6034  token: 0x6000062
        private System.Int32 <CalcBoneWeight_Distance>b__23_0(MagicaReductionMesh.MeshData.WeightData a, MagicaReductionMesh.MeshData.WeightData b) { }

    }

    // TypeToken: 0x2000012  // size: 0x18
    public sealed class <>c__DisplayClass21_0
    {
        // Fields
        public MagicaReductionMesh.MeshData.WeightData w;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000063
        public System.Void .ctor() { }
        // RVA: 0x09ED609C  token: 0x6000064
        private System.Boolean <CalcBoneWeight_Average>b__1(MagicaReductionMesh.MeshData.WeightData wdata) { }

    }

    // TypeToken: 0x2000013  // size: 0x18
    public sealed class <>c__DisplayClass23_0
    {
        // Fields
        public MagicaReductionMesh.MeshData.WeightData w;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000065
        public System.Void .ctor() { }
        // RVA: 0x09ED609C  token: 0x6000066
        private System.Boolean <CalcBoneWeight_Distance>b__1(MagicaReductionMesh.MeshData.WeightData wdata) { }

    }

    // TypeToken: 0x200000F  // size: 0xE8
    public class ShareVertex
    {
        // Fields
        public System.Int32 sindex;  // 0x10
        public UnityEngine.Vector3 wpos;  // 0x14
        public UnityEngine.Vector3 wnor;  // 0x20
        public UnityEngine.Vector3 wtan;  // 0x2c
        public System.Single tanw;  // 0x38
        public UnityEngine.Vector2 uv;  // 0x3c
        public UnityEngine.Matrix4x4 worldToLocalMatrix;  // 0x44
        public UnityEngine.Matrix4x4 bindpose;  // 0x84
        public System.Collections.Generic.List<MagicaReductionMesh.MeshData.WeightData> boneWeightList;  // 0xc8
        public System.Collections.Generic.List<MagicaReductionMesh.MeshData.Vertex> vertexList;  // 0xd0
        public System.Collections.Generic.HashSet<MagicaReductionMesh.MeshData.ShareVertex> linkShareVertexSet;  // 0xd8
        public System.Collections.Generic.HashSet<MagicaReductionMesh.MeshData.Triangle> linkTriangleSet;  // 0xe0

        // Methods
        // RVA: 0x09ED1A70  token: 0x600004E
        public System.Void AddLink(MagicaReductionMesh.MeshData.ShareVertex mv) { }
        // RVA: 0x09ED3E50  token: 0x600004F
        public System.Void ReplaseLink(MagicaReductionMesh.MeshData.ShareVertex old, MagicaReductionMesh.MeshData.ShareVertex mv) { }
        // RVA: 0x09ED3B68  token: 0x6000050
        public System.Void RecalcCoordinate() { }
        // RVA: 0x09ED3728  token: 0x6000051
        public System.Void CalcNormalTangentFromTriangle() { }
        // RVA: 0x09ED3634  token: 0x6000052
        public UnityEngine.Vector3 CalcLocalPos(UnityEngine.Vector3 pos) { }
        // RVA: 0x09ED3584  token: 0x6000053
        public UnityEngine.Vector3 CalcLocalDir(UnityEngine.Vector3 dir) { }
        // RVA: 0x09ED3964  token: 0x6000054
        public UnityEngine.Matrix4x4 CalcWorldToLocalMatrix() { }
        // RVA: 0x09ED3548  token: 0x6000055
        public System.Void CalcBoneWeight(MagicaReductionMesh.ReductionMesh.ReductionWeightMode weightMode, System.Single weightPow) { }
        // RVA: 0x09ED236C  token: 0x6000056
        private System.Void CalcBoneWeight_DistanceAverage(System.Single weightPow) { }
        // RVA: 0x09ED1CF8  token: 0x6000057
        private System.Void CalcBoneWeight_Average() { }
        // RVA: 0x09ED1AC8  token: 0x6000058
        private System.Void AdjustWeight(System.Collections.Generic.List<MagicaReductionMesh.MeshData.WeightData> sumlist) { }
        // RVA: 0x09ED2D54  token: 0x6000059
        private System.Void CalcBoneWeight_Distance(System.Single weightPow) { }
        // RVA: 0x09ED3A78  token: 0x600005A
        public UnityEngine.BoneWeight GetBoneWeight() { }
        // RVA: 0x09ED3F08  token: 0x600005B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000015  // size: 0x18
    public sealed class <>c__DisplayClass9_0
    {
        // Fields
        public System.Int32 edgev0;  // 0x10
        public System.Int32 edgev1;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x6000070
        public System.Void .ctor() { }
        // RVA: 0x09ED6130  token: 0x6000071
        private System.Boolean <GetNonEdgeVertex>b__0(MagicaReductionMesh.MeshData.ShareVertex sv) { }

    }

    // TypeToken: 0x2000014  // size: 0x40
    public class Triangle
    {
        // Fields
        public System.Int32 tindex;  // 0x10
        public System.Collections.Generic.List<MagicaReductionMesh.MeshData.ShareVertex> shareVertexList;  // 0x18
        public UnityEngine.Vector3 wnor;  // 0x20
        public UnityEngine.Vector3 wtan;  // 0x2c
        public System.Boolean flipLock;  // 0x38

        // Methods
        // RVA: 0x09ED5A0C  token: 0x6000067
        public System.Void GetEdge(System.UInt32& edge0, System.UInt32& edge1, System.UInt32& edge2) { }
        // RVA: 0x09ED5254  token: 0x6000068
        public UnityEngine.Vector3 CalcTriangleNormal() { }
        // RVA: 0x09ED5914  token: 0x6000069
        public System.Void Flip() { }
        // RVA: 0x09ED55A0  token: 0x600006A
        public UnityEngine.Vector3 CalcTriangleTangent() { }
        // RVA: 0x09ED5B78  token: 0x600006B
        public MagicaReductionMesh.MeshData.ShareVertex GetNonEdgeVertex(System.Int32 edgev0, System.Int32 edgev1) { }
        // RVA: 0x09ED5D34  token: 0x600006C
        public System.UInt64 GetTriangleHash() { }
        // RVA: 0x09ED5C4C  token: 0x600006D
        public static System.Single GetTriangleArea(MagicaReductionMesh.MeshData.ShareVertex sv0, MagicaReductionMesh.MeshData.ShareVertex sv1, MagicaReductionMesh.MeshData.ShareVertex sv2) { }
        // RVA: 0x09ED5DE8  token: 0x600006E
        public virtual System.String ToString() { }
        // RVA: 0x09ED5FBC  token: 0x600006F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000016  // size: 0x18
    public class Line
    {
        // Fields
        public System.Collections.Generic.List<MagicaReductionMesh.MeshData.ShareVertex> shareVertexList;  // 0x10

        // Methods
        // RVA: 0x09EC3760  token: 0x6000072
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000017  // size: 0x14
    public sealed struct UvWrapMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaReductionMesh.MeshData.UvWrapMode None;  // const
        public static MagicaReductionMesh.MeshData.UvWrapMode Sphere;  // const

    }

    // TypeToken: 0x2000018  // size: 0x28
    public class Square
    {
        // Fields
        public System.UInt64 shash;  // 0x10
        public System.Collections.Generic.List<MagicaReductionMesh.MeshData.Triangle> triangleList;  // 0x18
        public System.Single angle;  // 0x20

        // Methods
        // RVA: 0x09ED4078  token: 0x6000073
        public virtual System.String ToString() { }
        // RVA: 0x09ED4200  token: 0x6000074
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000019  // size: 0x38
    public class Tetra
    {
        // Fields
        public System.Collections.Generic.List<MagicaReductionMesh.MeshData.ShareVertex> shareVertexList;  // 0x10
        public UnityEngine.Vector3 circumCenter;  // 0x18
        public System.Single circumRadius;  // 0x24
        public UnityEngine.Vector3 tetraCenter;  // 0x28
        public System.Single tetraSize;  // 0x34

        // Methods
        // RVA: 0x09ED50C0  token: 0x6000075
        public System.Void .ctor() { }
        // RVA: 0x09ED5138  token: 0x6000076
        public System.Void .ctor(MagicaReductionMesh.MeshData.ShareVertex a, MagicaReductionMesh.MeshData.ShareVertex b, MagicaReductionMesh.MeshData.ShareVertex c, MagicaReductionMesh.MeshData.ShareVertex d) { }
        // RVA: 0x09ED4D28  token: 0x6000077
        public System.UInt64 GetTetraHash() { }
        // RVA: 0x09ED4278  token: 0x6000078
        public System.Void CalcCircumcircle() { }
        // RVA: 0x09ED4E1C  token: 0x6000079
        public System.Boolean IntersectCircumcircle(UnityEngine.Vector3 pos) { }
        // RVA: 0x09ED4B88  token: 0x600007A
        public System.Boolean CheckSame(MagicaReductionMesh.MeshData.Tetra tri) { }
        // RVA: 0x09ED4C08  token: 0x600007B
        public System.Boolean ContainsPoint(MagicaReductionMesh.MeshData.ShareVertex p1) { }
        // RVA: 0x09ED4C5C  token: 0x600007C
        public System.Boolean ContainsPoint(MagicaReductionMesh.MeshData.ShareVertex p1, MagicaReductionMesh.MeshData.ShareVertex p2, MagicaReductionMesh.MeshData.ShareVertex p3, MagicaReductionMesh.MeshData.ShareVertex p4) { }
        // RVA: 0x09ED48E0  token: 0x600007D
        public System.Void CalcSize() { }
        // RVA: 0x09ED4E68  token: 0x600007E
        public System.Boolean Verification() { }

    }

    // TypeToken: 0x200001A  // size: 0x20
    public class LinkInfo
    {
        // Fields
        public MagicaReductionMesh.MeshData.ShareVertex sv;  // 0x10
        public System.Single length;  // 0x18
        public System.Int32 count;  // 0x1c

        // Methods
        // RVA: 0x041E1670  token: 0x600007F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001B  // size: 0x20
    public class VertexLengthInfo
    {
        // Fields
        public MagicaReductionMesh.MeshData.ShareVertex sv;  // 0x10
        public System.Single length;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000080
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001C  // size: 0x18
    public sealed class <>c__DisplayClass65_0
    {
        // Fields
        public System.Single weightLength;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000081
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001D  // size: 0x20
    public sealed class <>c__DisplayClass65_1
    {
        // Fields
        public MagicaReductionMesh.MeshData.Vertex vt;  // 0x10
        public MagicaReductionMesh.MeshData.<>c__DisplayClass65_0 CS$<>8__locals1;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000082
        public System.Void .ctor() { }
        // RVA: 0x09ED60C4  token: 0x6000083
        private System.Boolean <CreateOriginalMeshInfo>b__0(MagicaReductionMesh.MeshData.ShareVertex sv) { }

    }

    // TypeToken: 0x200001E  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly MagicaReductionMesh.MeshData.<>c <>9;  // static @ 0x0
        public static System.Comparison<MagicaReductionMesh.MeshData.VertexLengthInfo> <>9__69_0;  // static @ 0x8

        // Methods
        // RVA: 0x09ED615C  token: 0x6000084
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000085
        public System.Void .ctor() { }
        // RVA: 0x09ED606C  token: 0x6000086
        private System.Int32 <SearchNearPointList>b__69_0(MagicaReductionMesh.MeshData.VertexLengthInfo a, MagicaReductionMesh.MeshData.VertexLengthInfo b) { }

    }

    // TypeToken: 0x2000021  // size: 0x40
    public class Point
    {
        // Fields
        public MagicaReductionMesh.MeshData.ShareVertex shareVertex;  // 0x10
        public UnityEngine.Vector3 pos;  // 0x18
        public UnityEngine.Vector3Int grid;  // 0x24
        public MagicaReductionMesh.NearPointReduction.Point nearPoint;  // 0x30
        public System.Single nearDist;  // 0x38

        // Methods
        // RVA: 0x041E1670  token: 0x6000099
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000023  // size: 0x28
    public class Point
    {
        // Fields
        public MagicaReductionMesh.MeshData.ShareVertex shareVertex;  // 0x10
        public MagicaReductionMesh.PolygonLinkReduction.Point nearPoint;  // 0x18
        public System.Single nearDist;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x60000A4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000026  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly MagicaCloth.BaseCloth.<>c <>9;  // static @ 0x0
        public static System.Func<UnityEngine.Renderer,System.Boolean> <>9__106_0;  // static @ 0x8

        // Methods
        // RVA: 0x09EEA808  token: 0x600011B
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600011C
        public System.Void .ctor() { }
        // RVA: 0x09EEA438  token: 0x600011D
        private System.Boolean <GetCullRenderListCount>b__106_0(UnityEngine.Renderer x) { }

    }

    // TypeToken: 0x2000030  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly MagicaCloth.MagicaPhysicsManager.<>c <>9;  // static @ 0x0
        public static UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction <>9__69_11;  // static @ 0x8
        public static UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction <>9__69_12;  // static @ 0x10
        public static UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction <>9__69_13;  // static @ 0x18
        public static UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction <>9__69_14;  // static @ 0x20
        public static UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction <>9__69_15;  // static @ 0x28
        public static UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction <>9__69_16;  // static @ 0x30
        public static UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction <>9__69_17;  // static @ 0x38
        public static System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem> <>9__69_0;  // static @ 0x40
        public static System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem> <>9__69_1;  // static @ 0x48
        public static System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem> <>9__69_2;  // static @ 0x50
        public static System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem> <>9__69_3;  // static @ 0x58
        public static System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem> <>9__69_4;  // static @ 0x60
        public static System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem> <>9__69_5;  // static @ 0x68
        public static System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem> <>9__69_6;  // static @ 0x70
        public static System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem> <>9__69_7;  // static @ 0x78
        public static System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem> <>9__69_8;  // static @ 0x80
        public static System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem> <>9__69_9;  // static @ 0x88
        public static System.Predicate<UnityEngine.LowLevel.PlayerLoopSystem> <>9__69_10;  // static @ 0x90

        // Methods
        // RVA: 0x04D46B10  token: 0x60001AF
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60001B0
        public System.Void .ctor() { }
        // RVA: 0x03234FB0  token: 0x60001B1
        private System.Void <SetCustomGameLoop>b__69_11() { }
        // RVA: 0x03235010  token: 0x60001B2
        private System.Void <SetCustomGameLoop>b__69_12() { }
        // RVA: 0x03234DF0  token: 0x60001B3
        private System.Void <SetCustomGameLoop>b__69_13() { }
        // RVA: 0x03234E60  token: 0x60001B4
        private System.Void <SetCustomGameLoop>b__69_14() { }
        // RVA: 0x03234EB0  token: 0x60001B5
        private System.Void <SetCustomGameLoop>b__69_15() { }
        // RVA: 0x03234F00  token: 0x60001B6
        private System.Void <SetCustomGameLoop>b__69_16() { }
        // RVA: 0x03234F50  token: 0x60001B7
        private System.Void <SetCustomGameLoop>b__69_17() { }
        // RVA: 0x04294250  token: 0x60001B8
        private System.Boolean <SetCustomGameLoop>b__69_0(UnityEngine.LowLevel.PlayerLoopSystem s) { }
        // RVA: 0x04294140  token: 0x60001B9
        private System.Boolean <SetCustomGameLoop>b__69_1(UnityEngine.LowLevel.PlayerLoopSystem s) { }
        // RVA: 0x042940D0  token: 0x60001BA
        private System.Boolean <SetCustomGameLoop>b__69_2(UnityEngine.LowLevel.PlayerLoopSystem h) { }
        // RVA: 0x04294070  token: 0x60001BB
        private System.Boolean <SetCustomGameLoop>b__69_3(UnityEngine.LowLevel.PlayerLoopSystem s) { }
        // RVA: 0x042942B0  token: 0x60001BC
        private System.Boolean <SetCustomGameLoop>b__69_4(UnityEngine.LowLevel.PlayerLoopSystem h) { }
        // RVA: 0x04294010  token: 0x60001BD
        private System.Boolean <SetCustomGameLoop>b__69_5(UnityEngine.LowLevel.PlayerLoopSystem s) { }
        // RVA: 0x04293E70  token: 0x60001BE
        private System.Boolean <SetCustomGameLoop>b__69_6(UnityEngine.LowLevel.PlayerLoopSystem h) { }
        // RVA: 0x04293FB0  token: 0x60001BF
        private System.Boolean <SetCustomGameLoop>b__69_7(UnityEngine.LowLevel.PlayerLoopSystem s) { }
        // RVA: 0x042941A0  token: 0x60001C0
        private System.Boolean <SetCustomGameLoop>b__69_8(UnityEngine.LowLevel.PlayerLoopSystem h) { }
        // RVA: 0x04293F50  token: 0x60001C1
        private System.Boolean <SetCustomGameLoop>b__69_9(UnityEngine.LowLevel.PlayerLoopSystem s) { }
        // RVA: 0x045F40F0  token: 0x60001C2
        private System.Boolean <SetCustomGameLoop>b__69_10(UnityEngine.LowLevel.PlayerLoopSystem h) { }

    }

    // TypeToken: 0x2000031  // size: 0x20
    public sealed class <>c__DisplayClass70_0
    {
        // Fields
        public System.Type t;  // 0x10
        public System.Func<UnityEngine.LowLevel.PlayerLoopSystem,System.Boolean> <>9__0;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60001C3
        public System.Void .ctor() { }
        // RVA: 0x044E7120  token: 0x60001C4
        private System.Boolean <CheckRegist>b__0(UnityEngine.LowLevel.PlayerLoopSystem x) { }

    }

    // TypeToken: 0x2000036  // size: 0x18
    public sealed class <>c__DisplayClass20_0
    {
        // Fields
        public System.Int32 partsId;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000220
        public System.Void .ctor() { }
        // RVA: 0x09EEA7D8  token: 0x6000221
        private System.Boolean <RemoveAvatarParts>b__0(MagicaCloth.MagicaAvatarParts p) { }

    }

    // TypeToken: 0x2000038  // size: 0x14
    public sealed struct ConnectionMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.BoneClothTarget.ConnectionMode Line;  // const
        public static MagicaCloth.BoneClothTarget.ConnectionMode MeshAutomatic;  // const
        public static MagicaCloth.BoneClothTarget.ConnectionMode MeshSequentialLoop;  // const
        public static MagicaCloth.BoneClothTarget.ConnectionMode MeshSequentialNoLoop;  // const

    }

    // TypeToken: 0x200003A  // size: 0x18
    public class RestoreDistanceWork
    {
        // Fields
        public System.UInt32 vertexPair;  // 0x10
        public System.Single dist;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x600025D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003B  // size: 0x20
    public class PenetrationBone
    {
        // Fields
        public UnityEngine.Transform bone;  // 0x10
        public UnityEngine.Transform childBone;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600025E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003C  // size: 0x30
    public class PenetrationWork
    {
        // Fields
        public UnityEngine.Transform bone;  // 0x10
        public UnityEngine.Transform childBone;  // 0x18
        public System.Int32 boneIndex;  // 0x20
        public System.Single distance;  // 0x24
        public System.Single weight;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x600025F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003D  // size: 0x30
    public class VertexInfo
    {
        // Fields
        public System.Int32 vertexIndex;  // 0x10
        public System.Int32 parentVertexIndex;  // 0x14
        public System.Collections.Generic.List<System.Int32> childVertexList;  // 0x18
        public MagicaCloth.ClothData.VertexInfo parentInfo;  // 0x20
        public System.Collections.Generic.List<MagicaCloth.ClothData.VertexInfo> childInfoList;  // 0x28

        // Methods
        // RVA: 0x09EEA8D0  token: 0x6000260
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003E  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly MagicaCloth.ClothData.<>c <>9;  // static @ 0x0
        public static System.Comparison<MagicaCloth.ClothData.RestoreDistanceWork> <>9__108_0;  // static @ 0x8
        public static System.Comparison<MagicaCloth.ClothData.RestoreDistanceWork> <>9__108_1;  // static @ 0x10
        public static System.Comparison<MagicaCloth.PenetrationConstraint.PenetrationData> <>9__108_2;  // static @ 0x18

        // Methods
        // RVA: 0x09EEA86C  token: 0x6000261
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000262
        public System.Void .ctor() { }
        // RVA: 0x09EEA3F0  token: 0x6000263
        private System.Int32 <CreateConstraintData>b__108_0(MagicaCloth.ClothData.RestoreDistanceWork a, MagicaCloth.ClothData.RestoreDistanceWork b) { }
        // RVA: 0x09EEA3F0  token: 0x6000264
        private System.Int32 <CreateConstraintData>b__108_1(MagicaCloth.ClothData.RestoreDistanceWork a, MagicaCloth.ClothData.RestoreDistanceWork b) { }
        // RVA: 0x09EEA420  token: 0x6000265
        private System.Int32 <CreateConstraintData>b__108_2(MagicaCloth.PenetrationConstraint.PenetrationData a, MagicaCloth.PenetrationConstraint.PenetrationData b) { }

    }

    // TypeToken: 0x200003F  // size: 0x18
    public sealed class <>c__DisplayClass110_0
    {
        // Fields
        public System.Collections.Generic.List<System.Single> meshVertexDepthList;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000266
        public System.Void .ctor() { }
        // RVA: 0x09EEA484  token: 0x6000267
        private System.Int32 <SortTetra>b__0(System.Int32 a, System.Int32 b) { }

    }

    // TypeToken: 0x2000040  // size: 0x18
    public sealed class <>c__DisplayClass111_0
    {
        // Fields
        public System.Collections.Generic.List<System.Single> meshVertexDepthList;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000268
        public System.Void .ctor() { }
        // RVA: 0x09EEA520  token: 0x6000269
        private System.Int32 <CheckTetraDirection>b__0(System.UInt64 a, System.UInt64 b) { }

    }

    // TypeToken: 0x2000041  // size: 0x20
    public sealed class <>c__DisplayClass114_0
    {
        // Fields
        public System.Collections.Generic.List<System.Single> depthList;  // 0x10
        public System.Collections.Generic.List<System.Single> nearFixedDistList;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600026A
        public System.Void .ctor() { }
        // RVA: 0x09EEA6CC  token: 0x600026B
        private System.Int32 <GetUseParentVertexList>b__0(System.Int32 a, System.Int32 b) { }

    }

    // TypeToken: 0x2000043  // size: 0x14
    public sealed struct Algorithm
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.ClothParams.Algorithm Algorithm_1;  // const
        public static MagicaCloth.ClothParams.Algorithm Algorithm_2;  // const

    }

    // TypeToken: 0x2000044  // size: 0x14
    public sealed struct TeleportMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.ClothParams.TeleportMode Reset;  // const
        public static MagicaCloth.ClothParams.TeleportMode Keep;  // const

    }

    // TypeToken: 0x2000045  // size: 0x14
    public sealed struct AdjustMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.ClothParams.AdjustMode Fixed;  // const
        public static MagicaCloth.ClothParams.AdjustMode XYMove;  // const
        public static MagicaCloth.ClothParams.AdjustMode XZMove;  // const
        public static MagicaCloth.ClothParams.AdjustMode YZMove;  // const

    }

    // TypeToken: 0x2000046  // size: 0x14
    public sealed struct PenetrationMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.ClothParams.PenetrationMode SurfacePenetration;  // const
        public static MagicaCloth.ClothParams.PenetrationMode ColliderPenetration;  // const

    }

    // TypeToken: 0x2000047  // size: 0x14
    public sealed struct PenetrationAxis
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.ClothParams.PenetrationAxis X;  // const
        public static MagicaCloth.ClothParams.PenetrationAxis Y;  // const
        public static MagicaCloth.ClothParams.PenetrationAxis Z;  // const
        public static MagicaCloth.ClothParams.PenetrationAxis InverseX;  // const
        public static MagicaCloth.ClothParams.PenetrationAxis InverseY;  // const
        public static MagicaCloth.ClothParams.PenetrationAxis InverseZ;  // const

    }

    // TypeToken: 0x2000048  // size: 0x14
    public sealed struct ParamType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.ClothParams.ParamType Radius;  // const
        public static MagicaCloth.ClothParams.ParamType Mass;  // const
        public static MagicaCloth.ClothParams.ParamType Gravity;  // const
        public static MagicaCloth.ClothParams.ParamType Drag;  // const
        public static MagicaCloth.ClothParams.ParamType MaxVelocity;  // const
        public static MagicaCloth.ClothParams.ParamType WorldInfluence;  // const
        public static MagicaCloth.ClothParams.ParamType ClampDistance;  // const
        public static MagicaCloth.ClothParams.ParamType ClampPosition;  // const
        public static MagicaCloth.ClothParams.ParamType ClampRotation;  // const
        public static MagicaCloth.ClothParams.ParamType RestoreDistance;  // const
        public static MagicaCloth.ClothParams.ParamType RestoreRotation;  // const
        public static MagicaCloth.ClothParams.ParamType Spring;  // const
        public static MagicaCloth.ClothParams.ParamType AdjustRotation;  // const
        public static MagicaCloth.ClothParams.ParamType AirLine;  // const
        public static MagicaCloth.ClothParams.ParamType TriangleBend;  // const
        public static MagicaCloth.ClothParams.ParamType Volume;  // const
        public static MagicaCloth.ClothParams.ParamType ColliderCollision;  // const
        public static MagicaCloth.ClothParams.ParamType RotationInterpolation;  // const
        public static MagicaCloth.ClothParams.ParamType DistanceDisable;  // const
        public static MagicaCloth.ClothParams.ParamType ExternalForce;  // const
        public static MagicaCloth.ClothParams.ParamType Penetration;  // const
        public static MagicaCloth.ClothParams.ParamType Algorithm;  // const
        public static MagicaCloth.ClothParams.ParamType BaseSkinning;  // const
        public static MagicaCloth.ClothParams.ParamType Max;  // const

    }

    // TypeToken: 0x200004A  // size: 0x28
    public sealed class <>c__DisplayClass2_0
    {
        // Fields
        public MagicaCloth.ClothData clothData;  // 0x10
        public System.Func<System.Int32,System.UInt32> funcUserFlag;  // 0x18
        public MagicaCloth.ClothParams param;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x60002FC
        public System.Void .ctor() { }
        // RVA: 0x09EFC63C  token: 0x60002FD
        private System.UInt32 <ClothInit>b__0(System.Int32 i) { }
        // RVA: 0x09EFC6FC  token: 0x60002FE
        private System.Single <ClothInit>b__1(System.Int32 i) { }
        // RVA: 0x09EFC728  token: 0x60002FF
        private Unity.Mathematics.float3 <ClothInit>b__2(System.Int32 i) { }

    }

    // TypeToken: 0x200004F  // size: 0x14
    public sealed struct Axis
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.MagicaMeshSpring.Axis X;  // const
        public static MagicaCloth.MagicaMeshSpring.Axis Y;  // const
        public static MagicaCloth.MagicaMeshSpring.Axis Z;  // const
        public static MagicaCloth.MagicaMeshSpring.Axis InverseX;  // const
        public static MagicaCloth.MagicaMeshSpring.Axis InverseY;  // const
        public static MagicaCloth.MagicaMeshSpring.Axis InverseZ;  // const

    }

    // TypeToken: 0x2000051  // size: 0x20
    public class DeformerSelection : MagicaCloth.IDataHash
    {
        // Fields
        public System.Collections.Generic.List<System.Int32> selectData;  // 0x10
        public System.Collections.Generic.List<System.UInt64> vertexHashList;  // 0x18

        // Methods
        // RVA: 0x09EEF388  token: 0x60003A6
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x09EEF234  token: 0x60003A7
        public System.Boolean Compare(MagicaCloth.SelectionData.DeformerSelection data) { }
        // RVA: 0x09EEF3C4  token: 0x60003A8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000053  // size: 0x28
    public class DeformerData : MagicaCloth.IDataHash
    {
        // Fields
        public System.Int32 deformerDataHash;  // 0x10
        public System.Int32 vertexCount;  // 0x14
        public System.Int32[] useVertexIndexList;  // 0x18
        public System.Single[] weightList;  // 0x20

        // Properties
        System.Int32 UseVertexCount { get; /* RVA: 0x09EEF220 */ }

        // Methods
        // RVA: 0x09EEF18C  token: 0x60003AF
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x041E1670  token: 0x60003B0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000055  // size: 0x10
    public static class Compute
    {
        // Fields
        public static System.Single Epsilon;  // const
        public static System.Single CollisionFrictionRange;  // const
        public static System.Single FrictionDampingRate;  // const
        public static System.Single FrictionMoveRatio;  // const
        public static System.Single FrictionPower;  // const
        public static System.Single ClampPositionMaxVelocity;  // const
        public static System.Single GlobalColliderMaxMoveDistance;  // const
        public static System.Single GlobalColliderMaxRotationAngle;  // const
        public static System.Single ColliderExtrusionMaxPower;  // const
        public static System.Single ColliderExtrusionDirectionPower;  // const
        public static System.Single ColliderExtrusionDistPower;  // const
        public static System.Single ColliderExtrusionVelocityInfluence;  // const
        public static System.Single MaxWindMain;  // const
        public static System.Single ClampRotationMaxVelocity;  // const
        public static System.Single ClampRotationMaxVelocity2;  // const
        public static System.Single TriangleBendVelocityInfluence;  // const

    }

    // TypeToken: 0x2000056  // size: 0x14
    public sealed struct Error
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.Define.Error None;  // const
        public static MagicaCloth.Define.Error Cancel;  // const
        public static MagicaCloth.Define.Error EmptyData;  // const
        public static MagicaCloth.Define.Error InvalidDataHash;  // const
        public static MagicaCloth.Define.Error TooOldDataVersion;  // const
        public static MagicaCloth.Define.Error HigherDataVersion;  // const
        public static MagicaCloth.Define.Error MeshDataNull;  // const
        public static MagicaCloth.Define.Error MeshDataHashMismatch;  // const
        public static MagicaCloth.Define.Error MeshDataVersionMismatch;  // const
        public static MagicaCloth.Define.Error ClothDataNull;  // const
        public static MagicaCloth.Define.Error ClothDataHashMismatch;  // const
        public static MagicaCloth.Define.Error ClothDataVersionMismatch;  // const
        public static MagicaCloth.Define.Error ClothSelectionHashMismatch;  // const
        public static MagicaCloth.Define.Error ClothSelectionVersionMismatch;  // const
        public static MagicaCloth.Define.Error ClothTargetRootCountMismatch;  // const
        public static MagicaCloth.Define.Error UseTransformNull;  // const
        public static MagicaCloth.Define.Error UseTransformCountZero;  // const
        public static MagicaCloth.Define.Error UseTransformCountMismatch;  // const
        public static MagicaCloth.Define.Error DeformerNull;  // const
        public static MagicaCloth.Define.Error DeformerHashMismatch;  // const
        public static MagicaCloth.Define.Error DeformerVersionMismatch;  // const
        public static MagicaCloth.Define.Error DeformerCountZero;  // const
        public static MagicaCloth.Define.Error DeformerCountMismatch;  // const
        public static MagicaCloth.Define.Error VertexCountZero;  // const
        public static MagicaCloth.Define.Error VertexUseCountZero;  // const
        public static MagicaCloth.Define.Error VertexCountMismatch;  // const
        public static MagicaCloth.Define.Error RootListCountMismatch;  // const
        public static MagicaCloth.Define.Error SelectionDataCountMismatch;  // const
        public static MagicaCloth.Define.Error SelectionCountZero;  // const
        public static MagicaCloth.Define.Error CenterTransformNull;  // const
        public static MagicaCloth.Define.Error SpringDataNull;  // const
        public static MagicaCloth.Define.Error SpringDataHashMismatch;  // const
        public static MagicaCloth.Define.Error SpringDataVersionMismatch;  // const
        public static MagicaCloth.Define.Error TargetObjectNull;  // const
        public static MagicaCloth.Define.Error SharedMeshNull;  // const
        public static MagicaCloth.Define.Error SharedMeshCannotRead;  // const
        public static MagicaCloth.Define.Error SharedMeshDifferent;  // const
        public static MagicaCloth.Define.Error SharedMeshDifferentVertexCount;  // const
        public static MagicaCloth.Define.Error MeshOptimizeMismatch;  // const
        public static MagicaCloth.Define.Error MeshVertexCount65535Over;  // const
        public static MagicaCloth.Define.Error MeshKeepQuads;  // const
        public static MagicaCloth.Define.Error BoneListZero;  // const
        public static MagicaCloth.Define.Error BoneListNull;  // const
        public static MagicaCloth.Define.Error RendererNotFound;  // const
        public static MagicaCloth.Define.Error MeshFilterNotFound;  // const
        public static MagicaCloth.Define.Error BuildNoTransformList;  // const
        public static MagicaCloth.Define.Error BuildReadOnlyPrefab;  // const
        public static MagicaCloth.Define.Error BuildFailedSaveAssets;  // const
        public static MagicaCloth.Define.Error BuildPrefabCannotSaved;  // const
        public static MagicaCloth.Define.Error BuildNotSceneObject;  // const
        public static MagicaCloth.Define.Error BuildInvalidComponent;  // const
        public static MagicaCloth.Define.Error BuildInvalidData;  // const
        public static MagicaCloth.Define.Error BuildInvalidMeshData;  // const
        public static MagicaCloth.Define.Error BuildInvalidGameObject;  // const
        public static MagicaCloth.Define.Error BuildInvalidPrefab;  // const
        public static MagicaCloth.Define.Error BuildInvalidRenderDeformer;  // const
        public static MagicaCloth.Define.Error BuildInvalidScene;  // const
        public static MagicaCloth.Define.Error BuildInvalidSelection;  // const
        public static MagicaCloth.Define.Error BuildMissingDeformer;  // const
        public static MagicaCloth.Define.Error BuildMissingSelection;  // const
        public static MagicaCloth.Define.Error BuildMissingMesh;  // const
        public static MagicaCloth.Define.Error BuildMissingScriptOnPrefab;  // const
        public static MagicaCloth.Define.Error OverlappingTransform;  // const
        public static MagicaCloth.Define.Error AddOverlappingTransform;  // const
        public static MagicaCloth.Define.Error OldDataVersion;  // const
        public static MagicaCloth.Define.Error OldAlgorithm;  // const

    }

    // TypeToken: 0x2000057  // size: 0x10
    public static class OptimizeMesh
    {
        // Fields
        public static System.Int32 Unknown;  // const
        public static System.Int32 Nothing;  // const
        public static System.Int32 Unity2018_On;  // const
        public static System.Int32 Unity2019_PolygonOrder;  // const
        public static System.Int32 Unity2019_VertexOrder;  // const

    }

    // TypeToken: 0x2000058  // size: 0x10
    public static class RenderMesh
    {
        // Fields
        public static System.Int32 WorkerMultiplesOfVertexCollection;  // const

    }

    // TypeToken: 0x200005D  // size: 0x3C
    public sealed struct VertexWeight
    {
        // Fields
        public UnityEngine.Vector3 localPos;  // 0x10
        public UnityEngine.Vector3 localNor;  // 0x1c
        public UnityEngine.Vector3 localTan;  // 0x28
        public System.Int32 parentIndex;  // 0x34
        public System.Single weight;  // 0x38

    }

    // TypeToken: 0x200005E  // size: 0x30
    public class ChildData : MagicaCloth.IDataHash
    {
        // Fields
        public System.Int32 childDataHash;  // 0x10
        public System.Int32 vertexCount;  // 0x14
        public System.UInt32[] vertexInfoList;  // 0x18
        public MagicaCloth.MeshData.VertexWeight[] vertexWeightList;  // 0x20
        public System.Int32[] parentIndexList;  // 0x28

        // Properties
        System.Int32 VertexCount { get; /* RVA: 0x04D882A0 */ }

        // Methods
        // RVA: 0x09EEBEDC  token: 0x600042E
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x041E1670  token: 0x600042F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000060  // size: 0x14
    public sealed struct RecalculateMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.RenderMeshDeformer.RecalculateMode None;  // const
        public static MagicaCloth.RenderMeshDeformer.RecalculateMode UpdateNormalPerFrame;  // const
        public static MagicaCloth.RenderMeshDeformer.RecalculateMode UpdateNormalAndTangentPerFrame;  // const

    }

    // TypeToken: 0x2000061  // size: 0x14
    public sealed struct BoundsMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.RenderMeshDeformer.BoundsMode None;  // const
        public static MagicaCloth.RenderMeshDeformer.BoundsMode ExpandedAtInitialization;  // const

    }

    // TypeToken: 0x2000063  // size: 0x18
    public sealed class <>c__DisplayClass35_0
    {
        // Fields
        public MagicaCloth.RenderMeshDeformer deformer;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000483
        public System.Void .ctor() { }
        // RVA: 0x09F10814  token: 0x6000484
        private System.Boolean <GetRenderMeshDeformerIndex>b__0(MagicaCloth.MagicaRenderDeformer d) { }

    }

    // TypeToken: 0x2000065  // size: 0x1C
    public sealed struct ClampDistance2Data
    {
        // Fields
        public System.Int32 vertexIndex;  // 0x10
        public System.Int32 parentVertexIndex;  // 0x14
        public System.Single length;  // 0x18

    }

    // TypeToken: 0x2000066  // size: 0x14
    public sealed struct ClampDistance2RootInfo
    {
        // Fields
        public System.UInt16 startIndex;  // 0x10
        public System.UInt16 dataLength;  // 0x12

    }

    // TypeToken: 0x2000067  // size: 0x44
    public sealed struct GroupData
    {
        // Fields
        public System.Int32 teamId;  // 0x10
        public System.Int32 active;  // 0x14
        public System.Single minRatio;  // 0x18
        public System.Single maxRatio;  // 0x1c
        public System.Single velocityInfluence;  // 0x20
        public MagicaCloth.ChunkData dataChunk;  // 0x24
        public MagicaCloth.ChunkData rootInfoChunk;  // 0x34

    }

    // TypeToken: 0x2000068  // size: 0xA8
    public sealed struct ClampDistance2Job : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Int32 runCount;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.ClampDistance2Constraint.ClampDistance2Data> dataList;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.ClampDistance2Constraint.ClampDistance2RootInfo> rootInfoList;  // 0x28
        public Unity.Collections.NativeArray<System.Int32> rootTeamList;  // 0x38
        public Unity.Collections.NativeArray<MagicaCloth.ClampDistance2Constraint.GroupData> groupList;  // 0x48
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x58
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x68
        public Unity.Collections.NativeArray<System.Single> frictionList;  // 0x78
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0x88
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0x98

        // Methods
        // RVA: 0x09EFFC40  token: 0x600048C
        public virtual System.Void Execute(System.Int32 rootIndex) { }

    }

    // TypeToken: 0x200006A  // size: 0x18
    public sealed struct ClampDistanceData
    {
        // Fields
        public System.UInt16 vertexIndex;  // 0x10
        public System.UInt16 targetVertexIndex;  // 0x12
        public System.Single length;  // 0x14

        // Methods
        // RVA: 0x09F00928  token: 0x6000494
        public System.Boolean IsValid() { }

    }

    // TypeToken: 0x200006B  // size: 0x44
    public sealed struct GroupData
    {
        // Fields
        public System.Int32 teamId;  // 0x10
        public System.Int32 active;  // 0x14
        public System.Single minRatio;  // 0x18
        public System.Single maxRatio;  // 0x1c
        public System.Single velocityInfluence;  // 0x20
        public MagicaCloth.ChunkData dataChunk;  // 0x24
        public MagicaCloth.ChunkData refChunk;  // 0x34

    }

    // TypeToken: 0x200006C  // size: 0xB8
    public sealed struct ClampDistanceJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Int32 runCount;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.ClampDistanceConstraint.ClampDistanceData> clampDistanceList;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.ClampDistanceConstraint.GroupData> groupList;  // 0x28
        public Unity.Collections.NativeArray<MagicaCloth.ReferenceDataIndex> refDataList;  // 0x38
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x48
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x58
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x68
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0x78
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> basePosList;  // 0x88
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0x98
        public Unity.Collections.NativeArray<System.Single> frictionList;  // 0xa8

        // Methods
        // RVA: 0x09F0093C  token: 0x6000495
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x200006E  // size: 0x38
    public sealed struct GroupData
    {
        // Fields
        public System.Int32 teamId;  // 0x10
        public System.Int32 active;  // 0x14
        public MagicaCloth.CurveParam limitLength;  // 0x18
        public Unity.Mathematics.float3 axisRatio;  // 0x28
        public System.Single velocityInfluence;  // 0x34

        // Methods
        // RVA: 0x09F07FDC  token: 0x600049D
        public System.Boolean IsAxisCheck() { }

    }

    // TypeToken: 0x200006F  // size: 0xB8
    public sealed struct ClampPositionJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Int32 runCount;  // 0x10
        public System.Single maxMoveLength;  // 0x14
        public Unity.Collections.NativeArray<MagicaCloth.ClampPositionConstraint.GroupData> clampPositionGroupList;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x28
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x38
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x48
        public Unity.Collections.NativeArray<System.Single> depthList;  // 0x58
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> basePosList;  // 0x68
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> baseRotList;  // 0x78
        public Unity.Collections.NativeArray<System.Single> frictionList;  // 0x88
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0x98
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0xa8

        // Methods
        // RVA: 0x09F01650  token: 0x600049E
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x2000071  // size: 0x34
    public sealed struct ClampRotationData
    {
        // Fields
        public System.Int32 vertexIndex;  // 0x10
        public System.Int32 parentVertexIndex;  // 0x14
        public Unity.Mathematics.float3 localPos;  // 0x18
        public Unity.Mathematics.quaternion localRot;  // 0x24

        // Methods
        // RVA: 0x09F02704  token: 0x60004A6
        public System.Boolean IsValid() { }

    }

    // TypeToken: 0x2000072  // size: 0x14
    public sealed struct ClampRotationRootInfo
    {
        // Fields
        public System.UInt16 startIndex;  // 0x10
        public System.UInt16 dataLength;  // 0x12

    }

    // TypeToken: 0x2000073  // size: 0x4C
    public sealed struct GroupData
    {
        // Fields
        public System.Int32 teamId;  // 0x10
        public System.Int32 active;  // 0x14
        public MagicaCloth.CurveParam maxAngle;  // 0x18
        public System.Single velocityInfluence;  // 0x28
        public MagicaCloth.ChunkData dataChunk;  // 0x2c
        public MagicaCloth.ChunkData rootInfoChunk;  // 0x3c

    }

    // TypeToken: 0x2000074  // size: 0xD8
    public sealed struct ClampRotationJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Int32 runCount;  // 0x10
        public System.Single maxMoveLength;  // 0x14
        public Unity.Collections.NativeArray<MagicaCloth.ClampRotationConstraint.ClampRotationData> dataList;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.ClampRotationConstraint.ClampRotationRootInfo> rootInfoList;  // 0x28
        public Unity.Collections.NativeArray<System.Int32> rootTeamList;  // 0x38
        public Unity.Collections.NativeArray<MagicaCloth.ClampRotationConstraint.GroupData> groupList;  // 0x48
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x58
        public Unity.Collections.NativeArray<System.Single> depthList;  // 0x68
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x78
        public Unity.Collections.NativeArray<System.Single> frictionList;  // 0x88
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0x98
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> nextRotList;  // 0xa8
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0xb8
        public Unity.Collections.NativeArray<System.Single> lengthBuffer;  // 0xc8

        // Methods
        // RVA: 0x09F02714  token: 0x60004A7
        public virtual System.Void Execute(System.Int32 rootIndex) { }

    }

    // TypeToken: 0x2000076  // size: 0x128
    public sealed struct CollisionJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Int32 runCount;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x18
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x28
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> radiusList;  // 0x38
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0x48
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> nextRotList;  // 0x58
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0x68
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> rotList;  // 0x78
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> localPosList;  // 0x88
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> basePosList;  // 0x98
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> baseRotList;  // 0xa8
        public Unity.Collections.NativeArray<System.Int32> transformIndexList;  // 0xb8
        public Unity.Collections.NativeArray<System.Int32> colliderList;  // 0xc8
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> boneSclList;  // 0xd8
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0xe8
        public Unity.Collections.NativeArray<System.Single> frictionList;  // 0xf8
        public Unity.Collections.NativeArray<System.Int32> collisionLinkIdList;  // 0x108
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> collisionNormalList;  // 0x118

        // Methods
        // RVA: 0x09F04348  token: 0x60004AE
        public virtual System.Void Execute(System.Int32 index) { }
        // RVA: 0x09F0492C  token: 0x60004AF
        private System.Single SphereColliderDetection(Unity.Mathematics.float3& nextpos, System.Single radius, System.Int32 cindex, Unity.Mathematics.float3& normal) { }
        // RVA: 0x09F03E94  token: 0x60004B0
        private System.Single CapsuleColliderDetection(Unity.Mathematics.float3& nextpos, System.Single radius, System.Int32 cindex, Unity.Mathematics.float3 dir, Unity.Mathematics.float3& normal) { }
        // RVA: 0x09F04808  token: 0x60004B1
        private System.Single PlaneColliderDetection(Unity.Mathematics.float3& nextpos, System.Single radius, System.Int32 cindex, Unity.Mathematics.float3& normal) { }

    }

    // TypeToken: 0x2000078  // size: 0xB8
    public sealed struct CollisionExtrusionJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Int32 runCount;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x18
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x28
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0x38
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> nextRotList;  // 0x48
        public Unity.Collections.NativeArray<System.Int32> collisionLinkIdList;  // 0x58
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0x68
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> rotList;  // 0x78
        public Unity.Collections.NativeArray<System.Single> frictionList;  // 0x88
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> collisionNormalList;  // 0x98
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0xa8

        // Methods
        // RVA: 0x09F03A18  token: 0x60004B7
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x200007A  // size: 0x34
    public sealed struct RotationData
    {
        // Fields
        public System.Int32 vertexIndex;  // 0x10
        public System.Int32 parentVertexIndex;  // 0x14
        public Unity.Mathematics.float3 localPos;  // 0x18
        public Unity.Mathematics.quaternion localRot;  // 0x24

        // Methods
        // RVA: 0x09F02704  token: 0x60004BF
        public System.Boolean IsValid() { }

    }

    // TypeToken: 0x200007B  // size: 0x14
    public sealed struct RootInfo
    {
        // Fields
        public System.UInt16 startIndex;  // 0x10
        public System.UInt16 dataLength;  // 0x12

    }

    // TypeToken: 0x200007C  // size: 0x60
    public sealed struct GroupData
    {
        // Fields
        public System.Int32 teamId;  // 0x10
        public System.Int32 useClamp;  // 0x14
        public System.Int32 useRestore;  // 0x18
        public MagicaCloth.CurveParam maxAngle;  // 0x1c
        public MagicaCloth.CurveParam restorePower;  // 0x2c
        public System.Single restoreVelocityInfluence;  // 0x3c
        public MagicaCloth.ChunkData dataChunk;  // 0x40
        public MagicaCloth.ChunkData rootInfoChunk;  // 0x50

    }

    // TypeToken: 0x200007D  // size: 0x110
    public sealed struct RotationRootLineJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Single updatePower;  // 0x10
        public System.Int32 runCount;  // 0x14
        public System.Single maxMoveSpeed;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.CompositeRotationConstraint.RotationData> dataList;  // 0x20
        public Unity.Collections.NativeArray<MagicaCloth.CompositeRotationConstraint.RootInfo> rootInfoList;  // 0x30
        public Unity.Collections.NativeArray<System.Int32> rootTeamList;  // 0x40
        public Unity.Collections.NativeArray<MagicaCloth.CompositeRotationConstraint.GroupData> groupList;  // 0x50
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x60
        public Unity.Collections.NativeArray<MagicaCloth.CurveParam> teamGravityList;  // 0x70
        public Unity.Collections.NativeArray<System.Single> depthList;  // 0x80
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x90
        public Unity.Collections.NativeArray<System.Single> frictionList;  // 0xa0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> basePosList;  // 0xb0
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> baseRotList;  // 0xc0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0xd0
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> nextRotList;  // 0xe0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0xf0
        public Unity.Collections.NativeArray<System.Single> lengthBuffer;  // 0x100

        // Methods
        // RVA: 0x09F0BC5C  token: 0x60004C0
        public virtual System.Void Execute(System.Int32 rootIndex) { }
        // RVA: 0x09EFEFD4  token: 0x60004C1
        private System.Single GetRotRatio(Unity.Mathematics.float3 tv, Unity.Mathematics.float3 gravityVector, System.Single gravity, System.Single minRatio, System.Single maxRatio) { }

    }

    // TypeToken: 0x200007F  // size: 0x1C
    public sealed struct EdgeCollisionData
    {
        // Fields
        public System.UInt16 vindex0;  // 0x10
        public System.UInt16 vindex1;  // 0x12
        public System.Int32 writeIndex0;  // 0x14
        public System.Int32 writeIndex1;  // 0x18

        // Methods
        // RVA: 0x09F07C84  token: 0x60004C9
        public System.Boolean IsValid() { }

    }

    // TypeToken: 0x2000080  // size: 0x5C
    public sealed struct GroupData
    {
        // Fields
        public System.Int32 teamId;  // 0x10
        public System.Int32 active;  // 0x14
        public System.Single edgeRadius;  // 0x18
        public MagicaCloth.ChunkData dataChunk;  // 0x1c
        public MagicaCloth.ChunkData groupIndexChunk;  // 0x2c
        public MagicaCloth.ChunkData refDataChunk;  // 0x3c
        public MagicaCloth.ChunkData writeDataChunk;  // 0x4c

    }

    // TypeToken: 0x2000081  // size: 0x108
    public sealed struct EdgeCollisionCalcJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Single updatePower;  // 0x10
        public System.Int32 runCount;  // 0x14
        public Unity.Collections.NativeArray<MagicaCloth.EdgeCollisionConstraint.GroupData> groupDataList;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.EdgeCollisionConstraint.EdgeCollisionData> dataList;  // 0x28
        public Unity.Collections.NativeArray<System.Int16> groupIndexList;  // 0x38
        public Unity.Collections.NativeArray<System.Int32> colliderList;  // 0x48
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x58
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x68
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> radiusList;  // 0x78
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0x88
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> rotList;  // 0x98
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0xa8
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> nextRotList;  // 0xb8
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> localPosList;  // 0xc8
        public Unity.Collections.NativeArray<System.Int32> transformIndexList;  // 0xd8
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> boneSclList;  // 0xe8
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> writeBuffer;  // 0xf8

        // Methods
        // RVA: 0x09F063F0  token: 0x60004CA
        public virtual System.Void Execute(System.Int32 index) { }
        // RVA: 0x09F068C4  token: 0x60004CB
        private System.Boolean SphereColliderDetection(Unity.Mathematics.float3 nextpos0, Unity.Mathematics.float3 nextpos1, Unity.Mathematics.float3& corr0, Unity.Mathematics.float3& corr1, System.Single radius, System.Int32 cindex) { }
        // RVA: 0x09F05D5C  token: 0x60004CC
        private System.Boolean CapsuleColliderDetection(Unity.Mathematics.float3 nextpos0, Unity.Mathematics.float3 nextpos1, Unity.Mathematics.float3& corr0, Unity.Mathematics.float3& corr1, System.Single radius, System.Int32 cindex, Unity.Mathematics.float3 dir) { }

    }

    // TypeToken: 0x2000082  // size: 0x98
    public sealed struct EdgeCollisionSumJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Int32 runCount;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.EdgeCollisionConstraint.GroupData> groupDataList;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.ReferenceDataIndex> refDataList;  // 0x28
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> writeBuffer;  // 0x38
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x48
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x58
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x68
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> inoutNextPosList;  // 0x78
        public Unity.Collections.NativeArray<System.Single> frictionList;  // 0x88

        // Methods
        // RVA: 0x09F07C98  token: 0x60004CD
        public virtual System.Void Execute(System.Int32 pindex) { }

    }

    // TypeToken: 0x2000084  // size: 0x30
    public sealed struct PenetrationData
    {
        // Fields
        public System.Int16 vertexIndex;  // 0x10
        public System.Int16 colliderIndex;  // 0x12
        public Unity.Mathematics.float3 localPos;  // 0x14
        public Unity.Mathematics.float3 localDir;  // 0x20
        public System.Single distance;  // 0x2c

        // Methods
        // RVA: 0x09F08C44  token: 0x60004D5
        public System.Boolean IsValid() { }

    }

    // TypeToken: 0x2000085  // size: 0x70
    public sealed struct GroupData
    {
        // Fields
        public System.Int32 teamId;  // 0x10
        public System.Int32 active;  // 0x14
        public System.Int32 mode;  // 0x18
        public System.Single maxDepth;  // 0x1c
        public MagicaCloth.CurveParam radius;  // 0x20
        public MagicaCloth.CurveParam distance;  // 0x30
        public MagicaCloth.ChunkData dataChunk;  // 0x40
        public MagicaCloth.ChunkData refDataChunk;  // 0x50
        public MagicaCloth.ChunkData bonePenetrationDataChunk;  // 0x60

    }

    // TypeToken: 0x2000086  // size: 0x158
    public sealed struct PenetrationJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Int32 runCount;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.PenetrationConstraint.GroupData> groupList;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.PenetrationConstraint.PenetrationData> dataList;  // 0x28
        public Unity.Collections.NativeArray<MagicaCloth.ReferenceDataIndex> refDataList;  // 0x38
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> bonePenetrationDataList;  // 0x48
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x58
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x68
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0x78
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> nextRotList;  // 0x88
        public Unity.Collections.NativeArray<System.Int32> transformIndexList;  // 0x98
        public Unity.Collections.NativeArray<System.Single> depthList;  // 0xa8
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> basePosList;  // 0xb8
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> baseRotList;  // 0xc8
        public Unity.Collections.NativeArray<System.Int32> colliderList;  // 0xd8
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> bonePosList;  // 0xe8
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> boneRotList;  // 0xf8
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> boneSclList;  // 0x108
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x118
        public Unity.Collections.NativeArray<System.Int32> skinningBoneList;  // 0x128
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> outNextPosList;  // 0x138
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0x148

        // Methods
        // RVA: 0x09F08C50  token: 0x60004D6
        public virtual System.Void Execute(System.Int32 index) { }
        // RVA: 0x09F098F0  token: 0x60004D7
        private Unity.Mathematics.float3 InverseSpherePosition(MagicaCloth.PenetrationConstraint.PenetrationData& data, System.Single teamScale, Unity.Mathematics.float3 scaleDirection, System.Single distance, System.Int32 cindex, System.Single cr) { }
        // RVA: 0x09F097D0  token: 0x60004D8
        private Unity.Mathematics.float3 InverseSpherePenetration(Unity.Mathematics.float3 c, System.Single cr, Unity.Mathematics.float3 nextpos) { }

    }

    // TypeToken: 0x2000088  // size: 0x18
    public sealed struct RestoreDistanceData
    {
        // Fields
        public System.UInt16 vertexIndex;  // 0x10
        public System.UInt16 targetVertexIndex;  // 0x12
        public System.Single length;  // 0x14

        // Methods
        // RVA: 0x09F00928  token: 0x60004E1
        public System.Boolean IsValid() { }

    }

    // TypeToken: 0x2000089  // size: 0xC0
    public sealed struct RestoreDistanceGroupData
    {
        // Fields
        public System.Int32 teamId;  // 0x10
        public MagicaCloth.CurveParam mass;  // 0x14
        public System.Single velocityInfluence;  // 0x24
        public MagicaCloth.CurveParam structStiffness;  // 0x28
        public MagicaCloth.ChunkData structDataChunk;  // 0x38
        public MagicaCloth.ChunkData structRefChunk;  // 0x48
        public System.Int32 useBend;  // 0x58
        public MagicaCloth.CurveParam bendStiffness;  // 0x5c
        public MagicaCloth.ChunkData bendDataChunk;  // 0x6c
        public MagicaCloth.ChunkData bendRefChunk;  // 0x7c
        public System.Int32 useNear;  // 0x8c
        public MagicaCloth.CurveParam nearStiffness;  // 0x90
        public MagicaCloth.ChunkData nearDataChunk;  // 0xa0
        public MagicaCloth.ChunkData nearRefChunk;  // 0xb0

        // Methods
        // RVA: 0x09F0AAE0  token: 0x60004E2
        public System.Boolean IsValid(System.Int32 type) { }
        // RVA: 0x09F0AAB8  token: 0x60004E3
        public MagicaCloth.CurveParam GetStiffness(System.Int32 type) { }
        // RVA: 0x09F0AA68  token: 0x60004E4
        public MagicaCloth.ChunkData GetDataChunk(System.Int32 type) { }
        // RVA: 0x09F0AA90  token: 0x60004E5
        public MagicaCloth.ChunkData GetRefChunk(System.Int32 type) { }

    }

    // TypeToken: 0x200008A  // size: 0xE0
    public sealed struct DistanceJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Single updatePower;  // 0x10
        public System.Int32 runCount;  // 0x14
        public System.Int32 type;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.RestoreDistanceConstraint.RestoreDistanceData> restoreDistanceDataList;  // 0x20
        public Unity.Collections.NativeArray<MagicaCloth.RestoreDistanceConstraint.RestoreDistanceGroupData> restoreDistanceGroupDataList;  // 0x30
        public Unity.Collections.NativeArray<MagicaCloth.ReferenceDataIndex> refDataList;  // 0x40
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x50
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x60
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x70
        public Unity.Collections.NativeArray<System.Single> depthList;  // 0x80
        public Unity.Collections.NativeArray<System.Single> frictionList;  // 0x90
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> basePosList;  // 0xa0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0xb0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> outNextPosList;  // 0xc0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0xd0

        // Methods
        // RVA: 0x09F05564  token: 0x60004E6
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x200008C  // size: 0x20
    public sealed struct RotationData
    {
        // Fields
        public System.UInt16 vertexIndex;  // 0x10
        public System.UInt16 targetVertexIndex;  // 0x12
        public Unity.Mathematics.float3 localPos;  // 0x14

        // Methods
        // RVA: 0x09F00928  token: 0x60004EE
        public System.Boolean IsValid() { }

    }

    // TypeToken: 0x200008D  // size: 0x4C
    public sealed struct GroupData
    {
        // Fields
        public System.Int32 teamId;  // 0x10
        public System.Int32 active;  // 0x14
        public MagicaCloth.CurveParam restorePower;  // 0x18
        public System.Single velocityInfluence;  // 0x28
        public MagicaCloth.ChunkData dataChunk;  // 0x2c
        public MagicaCloth.ChunkData refChunk;  // 0x3c

    }

    // TypeToken: 0x200008E  // size: 0xD8
    public sealed struct RotationJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Single updatePower;  // 0x10
        public System.Int32 runCount;  // 0x14
        public Unity.Collections.NativeArray<MagicaCloth.RestoreRotationConstraint.RotationData> dataList;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.RestoreRotationConstraint.GroupData> groupList;  // 0x28
        public Unity.Collections.NativeArray<MagicaCloth.ReferenceDataIndex> refDataList;  // 0x38
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x48
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x58
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x68
        public Unity.Collections.NativeArray<System.Single> depthList;  // 0x78
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> baseRotList;  // 0x88
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0x98
        public Unity.Collections.NativeArray<System.Single> frictionList;  // 0xa8
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> outNextPosList;  // 0xb8
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0xc8

        // Methods
        // RVA: 0x09F0B524  token: 0x60004EF
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x2000090  // size: 0x1C
    public sealed struct GroupData
    {
        // Fields
        public System.Int32 teamId;  // 0x10
        public System.Int32 active;  // 0x14
        public System.Single spring;  // 0x18

    }

    // TypeToken: 0x2000091  // size: 0x78
    public sealed struct SpringJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Single updatePower;  // 0x10
        public System.Int32 runCount;  // 0x14
        public Unity.Collections.NativeArray<MagicaCloth.SpringConstraint.GroupData> groupList;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x28
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x38
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x48
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> basePosList;  // 0x58
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0x68

        // Methods
        // RVA: 0x09F0D8C8  token: 0x60004F7
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x2000093  // size: 0x3C
    public sealed struct TriangleBendData
    {
        // Fields
        public System.Int32 vindex0;  // 0x10
        public System.Int32 vindex1;  // 0x14
        public System.Int32 vindex2;  // 0x18
        public System.Int32 vindex3;  // 0x1c
        public System.Single restAngle;  // 0x20
        public System.Single direction;  // 0x24
        public System.Single depth;  // 0x28
        public System.Int32 writeIndex0;  // 0x2c
        public System.Int32 writeIndex1;  // 0x30
        public System.Int32 writeIndex2;  // 0x34
        public System.Int32 writeIndex3;  // 0x38

        // Methods
        // RVA: 0x09F0F454  token: 0x60004FF
        public System.Boolean IsValid() { }
        // RVA: 0x09F0F44C  token: 0x6000500
        public System.Boolean IsPositionBend() { }

    }

    // TypeToken: 0x2000094  // size: 0x6C
    public sealed struct TriangleBendGroupData
    {
        // Fields
        public System.Int32 teamId;  // 0x10
        public System.Int32 active;  // 0x14
        public System.Int32 algorithm;  // 0x18
        public MagicaCloth.CurveParam stiffness;  // 0x1c
        public MagicaCloth.ChunkData dataChunk;  // 0x2c
        public MagicaCloth.ChunkData groupIndexChunk;  // 0x3c
        public MagicaCloth.ChunkData refDataChunk;  // 0x4c
        public MagicaCloth.ChunkData writeDataChunk;  // 0x5c

    }

    // TypeToken: 0x2000095  // size: 0x88
    public sealed struct TriangleBendCalcJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Single updatePower;  // 0x10
        public System.Int32 runCount;  // 0x14
        public Unity.Collections.NativeArray<MagicaCloth.TriangleBendConstraint.TriangleBendGroupData> triangleBendGroupDataList;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.TriangleBendConstraint.TriangleBendData> triangleBendList;  // 0x28
        public Unity.Collections.NativeArray<System.Int16> groupIndexList;  // 0x38
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x48
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0x58
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> basePosList;  // 0x68
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> writeBuffer;  // 0x78

        // Methods
        // RVA: 0x09F0DB30  token: 0x6000501
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x2000096  // size: 0x98
    public sealed struct TriangleBendSumJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Int32 runCount;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.TriangleBendConstraint.TriangleBendGroupData> triangleBendGroupDataList;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.ReferenceDataIndex> refDataList;  // 0x28
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> writeBuffer;  // 0x38
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x48
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x58
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x68
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> inoutNextPosList;  // 0x78
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0x88

        // Methods
        // RVA: 0x09F0F464  token: 0x6000502
        public virtual System.Void Execute(System.Int32 pindex) { }

    }

    // TypeToken: 0x2000098  // size: 0x14
    public sealed struct TwistData
    {
        // Fields
        public System.UInt16 vertexIndex0;  // 0x10
        public System.UInt16 vertexIndex1;  // 0x12

        // Methods
        // RVA: 0x09F00928  token: 0x600050A
        public System.Boolean IsValid() { }

    }

    // TypeToken: 0x2000099  // size: 0x3C
    public sealed struct GroupData
    {
        // Fields
        public System.Int32 teamId;  // 0x10
        public System.Int32 active;  // 0x14
        public System.Single recoveryPower;  // 0x18
        public MagicaCloth.ChunkData dataChunk;  // 0x1c
        public MagicaCloth.ChunkData refChunk;  // 0x2c

    }

    // TypeToken: 0x200009A  // size: 0xC8
    public sealed struct TwistJob2 : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Int32 runCount;  // 0x10
        public System.Single updatePower;  // 0x14
        public Unity.Collections.NativeArray<MagicaCloth.TwistConstraint.TwistData> dataList;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.ReferenceDataIndex> refDataList;  // 0x28
        public Unity.Collections.NativeArray<MagicaCloth.TwistConstraint.GroupData> groupList;  // 0x38
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x48
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x58
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x68
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> basePosList;  // 0x78
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0x88
        public Unity.Collections.NativeArray<System.Single> frictionList;  // 0x98
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> outNextPosList;  // 0xa8
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0xb8

        // Methods
        // RVA: 0x09F10120  token: 0x600050B
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x200009C  // size: 0x5C
    public sealed struct VolumeData
    {
        // Fields
        public System.Int32 vindex0;  // 0x10
        public System.Int32 vindex1;  // 0x14
        public System.Int32 vindex2;  // 0x18
        public System.Int32 vindex3;  // 0x1c
        public Unity.Mathematics.float3x3 ivMat;  // 0x20
        public System.Single depth;  // 0x44
        public System.Int32 direction;  // 0x48
        public System.Int32 writeIndex0;  // 0x4c
        public System.Int32 writeIndex1;  // 0x50
        public System.Int32 writeIndex2;  // 0x54
        public System.Int32 writeIndex3;  // 0x58

        // Methods
        // RVA: 0x09F0F454  token: 0x6000514
        public System.Boolean IsValid() { }

    }

    // TypeToken: 0x200009D  // size: 0x78
    public sealed struct GroupData
    {
        // Fields
        public System.Int32 teamId;  // 0x10
        public System.Int32 active;  // 0x14
        public MagicaCloth.CurveParam stretchStiffness;  // 0x18
        public MagicaCloth.CurveParam shearStiffness;  // 0x28
        public MagicaCloth.ChunkData dataChunk;  // 0x38
        public MagicaCloth.ChunkData groupIndexChunk;  // 0x48
        public MagicaCloth.ChunkData refDataChunk;  // 0x58
        public MagicaCloth.ChunkData writeDataChunk;  // 0x68

    }

    // TypeToken: 0x200009E  // size: 0x78
    public sealed struct VolumeCalcJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Single updatePower;  // 0x10
        public System.Int32 runCount;  // 0x14
        public Unity.Collections.NativeArray<MagicaCloth.VolumeConstraint.GroupData> groupDataList;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.VolumeConstraint.VolumeData> dataList;  // 0x28
        public Unity.Collections.NativeArray<System.Int16> groupIndexList;  // 0x38
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x48
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0x58
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> writeBuffer;  // 0x68

        // Methods
        // RVA: 0x09F10844  token: 0x6000515
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x200009F  // size: 0x88
    public sealed struct VolumeSumJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Int32 runCount;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.VolumeConstraint.GroupData> groupDataList;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.ReferenceDataIndex> refDataList;  // 0x28
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> writeBuffer;  // 0x38
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x48
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x58
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x68
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> inoutNextPosList;  // 0x78

        // Methods
        // RVA: 0x09F20DCC  token: 0x6000516
        public virtual System.Void Execute(System.Int32 pindex) { }

    }

    // TypeToken: 0x20000A5  // size: 0x68
    public sealed struct RestoreBoneJob : UnityEngine.Jobs.IJobParallelForTransform
    {
        // Fields
        public System.Boolean fixedUpdate;  // 0x10
        public Unity.Collections.NativeArray<System.Int16> boneUnityPhysicsList;  // 0x18
        public Unity.Collections.NativeArray<System.Byte> boneFlagList;  // 0x28
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> restoreBoneLocalPosList;  // 0x38
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> restoreBoneLocalRotList;  // 0x48
        public Unity.Collections.NativeArray<System.Int32> restoreBoneIndexList;  // 0x58

        // Methods
        // RVA: 0x09F20C10  token: 0x600053F
        public virtual System.Void Execute(System.Int32 index, UnityEngine.Jobs.TransformAccess transform) { }

    }

    // TypeToken: 0x20000A6  // size: 0xA8
    public sealed struct ReadBoneJob0 : UnityEngine.Jobs.IJobParallelForTransform
    {
        // Fields
        public System.Int32 fixedUpdateCount;  // 0x10
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> bonePosList;  // 0x18
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> boneRotList;  // 0x28
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> boneSclList;  // 0x38
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> basePosList;  // 0x48
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> baseRotList;  // 0x58
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> futurePosList;  // 0x68
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> futureRotList;  // 0x78
        public Unity.Collections.NativeArray<System.Int16> boneUnityPhysicsList;  // 0x88
        public Unity.Collections.NativeArray<System.Byte> boneFlagList;  // 0x98

        // Methods
        // RVA: 0x09F201C0  token: 0x6000540
        public virtual System.Void Execute(System.Int32 index, UnityEngine.Jobs.TransformAccess transform) { }

    }

    // TypeToken: 0x20000A7  // size: 0xB8
    public sealed struct ReadBoneJob1 : UnityEngine.Jobs.IJobParallelForTransform
    {
        // Fields
        public System.Int32 fixedUpdateCount;  // 0x10
        public System.Single normalFutureRatio;  // 0x14
        public System.Single fixedFutureRatio;  // 0x18
        public System.Single normalDeltaTime;  // 0x1c
        public System.Single fixedDeltaTime;  // 0x20
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> bonePosList;  // 0x28
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> boneRotList;  // 0x38
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> boneSclList;  // 0x48
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> basePosList;  // 0x58
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> baseRotList;  // 0x68
        public Unity.Collections.NativeArray<System.Int16> boneUnityPhysicsList;  // 0x78
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> futurePosList;  // 0x88
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> futureRotList;  // 0x98
        public Unity.Collections.NativeArray<System.Byte> boneFlagList;  // 0xa8

        // Methods
        // RVA: 0x09F2051C  token: 0x6000541
        public virtual System.Void Execute(System.Int32 index, UnityEngine.Jobs.TransformAccess transform) { }

    }

    // TypeToken: 0x20000A8  // size: 0x90
    public sealed struct ConvertWorldToLocalJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<System.Int32> writeBoneIndexList;  // 0x10
        public Unity.Collections.NativeArray<System.Byte> boneFlagList;  // 0x20
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> bonePosList;  // 0x30
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> boneRotList;  // 0x40
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> boneSclList;  // 0x50
        public Unity.Collections.NativeArray<System.Int32> boneParentIndexList;  // 0x60
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> writeBonePosList;  // 0x70
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> writeBoneRotList;  // 0x80

        // Methods
        // RVA: 0x09F126D0  token: 0x6000542
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x20000A9  // size: 0x78
    public sealed struct WriteBontToTransformJob2 : UnityEngine.Jobs.IJobParallelForTransform
    {
        // Fields
        public System.Int32 fixedUpdateCount;  // 0x10
        public Unity.Collections.NativeArray<System.Byte> boneFlagList;  // 0x18
        public Unity.Collections.NativeArray<System.Int32> writeBoneIndexList;  // 0x28
        public Unity.Collections.NativeArray<System.Int32> boneParentIndexList;  // 0x38
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> writeBonePosList;  // 0x48
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> writeBoneRotList;  // 0x58
        public Unity.Collections.NativeArray<System.Int16> boneUnityPhysicsList;  // 0x68

        // Methods
        // RVA: 0x09F21104  token: 0x6000543
        public virtual System.Void Execute(System.Int32 index, UnityEngine.Jobs.TransformAccess transform) { }

    }

    // TypeToken: 0x20000AA  // size: 0x50
    public sealed struct CopyBoneJob0 : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> bonePosList;  // 0x10
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> boneRotList;  // 0x20
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> backBonePosList;  // 0x30
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> backBoneRotList;  // 0x40

        // Methods
        // RVA: 0x04DA3550  token: 0x6000544
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x20000AB  // size: 0x30
    public sealed struct CopyBoneJob1 : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<System.Int32> writeBoneIndexList;  // 0x10
        public Unity.Collections.NativeArray<System.Int32> backWriteBoneIndexList;  // 0x20

        // Methods
        // RVA: 0x04DA35A0  token: 0x6000545
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x20000AC  // size: 0x30
    public sealed struct CopyBoneJob2 : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<System.Byte> boneFlagList;  // 0x10
        public Unity.Collections.NativeArray<System.Byte> backBoneFlagList;  // 0x20

        // Methods
        // RVA: 0x04DA35C0  token: 0x6000546
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x20000AF  // size: 0x1C0
    public sealed struct ForceAndVelocityJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Single updateDeltaTime;  // 0x10
        public System.Single updatePower;  // 0x14
        public System.Int32 runCount;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x20
        public Unity.Collections.NativeArray<MagicaCloth.CurveParam> teamMassList;  // 0x30
        public Unity.Collections.NativeArray<MagicaCloth.CurveParam> teamGravityList;  // 0x40
        public Unity.Collections.NativeArray<MagicaCloth.CurveParam> teamDragList;  // 0x50
        public Unity.Collections.NativeArray<MagicaCloth.CurveParam> teamDepthInfluenceList;  // 0x60
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.WindInfo> teamWindInfoList;  // 0x70
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x80
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x90
        public Unity.Collections.NativeArray<System.Single> depthList;  // 0xa0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> snapBasePosList;  // 0xb0
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> snapBaseRotList;  // 0xc0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> basePosList;  // 0xd0
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> baseRotList;  // 0xe0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> oldBasePosList;  // 0xf0
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> oldBaseRotList;  // 0x100
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0x110
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> nextRotList;  // 0x120
        public Unity.Collections.NativeArray<System.Single> frictionList;  // 0x130
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0x140
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> rotList;  // 0x150
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> oldPosList;  // 0x160
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> oldRotList;  // 0x170
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> velocityList;  // 0x180
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerWindData.WindData> windDataList;  // 0x190
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> bonePosList;  // 0x1a0
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> boneRotList;  // 0x1b0

        // Methods
        // RVA: 0x09F132A0  token: 0x6000596
        public virtual System.Void Execute(System.Int32 index) { }
        // RVA: 0x09F13CA8  token: 0x6000597
        private Unity.Mathematics.float3 Wind(System.Int32 teamId, MagicaCloth.PhysicsManagerTeamData.TeamData& teamData, Unity.Mathematics.float3& pos) { }

    }

    // TypeToken: 0x20000B0  // size: 0x120
    public sealed struct FixPositionJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Single updatePower;  // 0x10
        public System.Single updateDeltaTime;  // 0x14
        public System.Int32 runCount;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x20
        public Unity.Collections.NativeArray<MagicaCloth.CurveParam> teamMaxVelocityList;  // 0x30
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x40
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x50
        public Unity.Collections.NativeArray<System.Single> depthList;  // 0x60
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0x70
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> nextRotList;  // 0x80
        public Unity.Collections.NativeArray<System.Single> frictionList;  // 0x90
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> velocityList;  // 0xa0
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> rotList;  // 0xb0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> oldPosList;  // 0xc0
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> oldRotList;  // 0xd0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0xe0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> localPosList;  // 0xf0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> collisionNormalList;  // 0x100
        public Unity.Collections.NativeArray<System.Single> staticFrictionList;  // 0x110

        // Methods
        // RVA: 0x09F12AA4  token: 0x6000598
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x20000B1  // size: 0x138
    public sealed struct PostUpdatePhysicsJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Single updateDeltaTime;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x28
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x38
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> snapBasePosList;  // 0x48
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> snapBaseRotList;  // 0x58
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> basePosList;  // 0x68
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> baseRotList;  // 0x78
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> oldBasePosList;  // 0x88
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> oldBaseRotList;  // 0x98
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> velocityList;  // 0xa8
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> localPosList;  // 0xb8
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> oldPosList;  // 0xc8
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> oldRotList;  // 0xd8
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0xe8
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> rotList;  // 0xf8
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0x108
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> nextRotList;  // 0x118
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> oldSlowPosList;  // 0x128

        // Methods
        // RVA: 0x09F1FC24  token: 0x6000599
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x20000B4  // size: 0x60
    public sealed struct SharedVirtualMeshInfo
    {
        // Fields
        public System.Int32 uid;  // 0x10
        public System.Int32 useCount;  // 0x14
        public System.Int32 sharedChildMeshStartIndex;  // 0x18
        public System.Int32 sharedChildMeshCount;  // 0x1c
        public MagicaCloth.ChunkData vertexChunk;  // 0x20
        public MagicaCloth.ChunkData weightChunk;  // 0x30
        public MagicaCloth.ChunkData triangleChunk;  // 0x40
        public MagicaCloth.ChunkData vertexToTriangleChunk;  // 0x50

    }

    // TypeToken: 0x20000B5  // size: 0x54
    public sealed struct VirtualMeshInfo
    {
        // Fields
        public System.UInt32 flag;  // 0x10
        public System.Int32 sharedVirtualMeshIndex;  // 0x14
        public System.Int32 meshUseCount;  // 0x18
        public System.Int32 vertexUseCount;  // 0x1c
        public MagicaCloth.ChunkData vertexChunk;  // 0x20
        public MagicaCloth.ChunkData boneChunk;  // 0x30
        public MagicaCloth.ChunkData triangleChunk;  // 0x40
        public System.Int32 transformIndex;  // 0x50

        // Methods
        // RVA: 0x09F20B04  token: 0x60005EC
        public System.Boolean IsFlag(System.UInt32 flag) { }
        // RVA: 0x09F20BE4  token: 0x60005ED
        public System.Void SetFlag(System.UInt32 flag, System.Boolean sw) { }
        // RVA: 0x09F20AF0  token: 0x60005EE
        public System.Boolean IsActive() { }
        // RVA: 0x09F20DB4  token: 0x60005EF
        public System.Boolean IsUse() { }
        // RVA: 0x09F20B24  token: 0x60005F0
        public System.Boolean IsPause() { }

    }

    // TypeToken: 0x20000B6  // size: 0x48
    public sealed struct SharedChildMeshInfo
    {
        // Fields
        public System.Int64 cuid;  // 0x10
        public System.Int32 sharedVirtualMeshIndex;  // 0x18
        public System.Int32 virtualMeshIndex;  // 0x1c
        public System.Int32 meshUseCount;  // 0x20
        public MagicaCloth.ChunkData vertexChunk;  // 0x24
        public MagicaCloth.ChunkData weightChunk;  // 0x34

    }

    // TypeToken: 0x20000B7  // size: 0x50
    public sealed struct SharedRenderMeshInfo
    {
        // Fields
        public System.Int32 uid;  // 0x10
        public System.Int32 useCount;  // 0x14
        public System.UInt32 flag;  // 0x18
        public MagicaCloth.ChunkData vertexChunk;  // 0x1c
        public MagicaCloth.ChunkData bonePerVertexChunk;  // 0x2c
        public MagicaCloth.ChunkData boneWeightsChunk;  // 0x3c
        public System.Int32 rendererBoneIndex;  // 0x4c

        // Methods
        // RVA: 0x09F20CF8  token: 0x60005F1
        public System.Boolean IsFlag(System.UInt32 flag) { }
        // RVA: 0x09F20D08  token: 0x60005F2
        public System.Void SetFlag(System.UInt32 flag, System.Boolean sw) { }
        // RVA: 0x09F20D00  token: 0x60005F3
        public System.Boolean IsSkinning() { }

    }

    // TypeToken: 0x20000B8  // size: 0x98
    public sealed struct RenderMeshInfo
    {
        // Fields
        public System.UInt32 flag;  // 0x10
        public System.Int32 renderSharedMeshIndex;  // 0x14
        public System.Int32 sharedRenderMeshVertexStartIndex;  // 0x18
        public System.Int32 meshUseCount;  // 0x1c
        public Unity.Mathematics.int4 childMeshVertexStartIndex;  // 0x20
        public Unity.Mathematics.int4 childMeshWeightStartIndex;  // 0x30
        public Unity.Mathematics.int4 virtualMeshVertexStartIndex;  // 0x40
        public Unity.Mathematics.int4 sharedVirtualMeshVertexStartIndex;  // 0x50
        public Unity.Mathematics.int4 linkMeshCount;  // 0x60
        public MagicaCloth.ChunkData vertexChunk;  // 0x70
        public MagicaCloth.ChunkData boneWeightsChunk;  // 0x80
        public System.Int32 transformIndex;  // 0x90
        public System.Single baseScale;  // 0x94

        // Methods
        // RVA: 0x09F20B04  token: 0x60005F4
        public System.Boolean IsFlag(System.UInt32 flag) { }
        // RVA: 0x09F20BE4  token: 0x60005F5
        public System.Void SetFlag(System.UInt32 flag, System.Boolean sw) { }
        // RVA: 0x09F20AF0  token: 0x60005F6
        public System.Boolean IsActive() { }
        // RVA: 0x09F20B34  token: 0x60005F7
        public System.Boolean IsUse() { }
        // RVA: 0x09F20B0C  token: 0x60005F8
        public System.Boolean IsLinkMesh(System.Int32 index) { }
        // RVA: 0x09F20B24  token: 0x60005F9
        public System.Boolean IsPause() { }
        // RVA: 0x09F20AF8  token: 0x60005FA
        public System.Boolean IsFasterWrite() { }
        // RVA: 0x09F20B2C  token: 0x60005FB
        public System.Boolean IsSkinning() { }
        // RVA: 0x09F20A30  token: 0x60005FC
        public System.Boolean AddLinkMesh(System.Int32 renderMeshIndex, System.Int32 childMeshVertexStart, System.Int32 childMeshWeightStart, System.Int32 virtualMeshVertexStart, System.Int32 sharedVirtualMeshVertexStart) { }
        // RVA: 0x09F20B44  token: 0x60005FD
        public System.Boolean RemoveLinkMesh(System.Int32 renderMeshIndex, System.Int32 childMeshVertexStart, System.Int32 childMeshWeightStart, System.Int32 virtualMeshVertexStart, System.Int32 sharedVirtualMeshVertexStart) { }

    }

    // TypeToken: 0x20000B9  // size: 0x30
    public class RenderMeshState
    {
        // Fields
        public System.UInt32 flag;  // 0x10
        public System.Int32 RenderSharedMeshIndex;  // 0x14
        public System.Int32 RenderSharedMeshId;  // 0x18
        public System.Int32 VertexChunkStart;  // 0x1c
        public System.Int32 VertexChunkLength;  // 0x20
        public System.Int32 BoneWeightChunkStart;  // 0x24
        public System.Int32 BoneWeightChunkLength;  // 0x28

        // Methods
        // RVA: 0x0709EC80  token: 0x60005FE
        public System.Boolean IsFlag(System.UInt32 flag) { }
        // RVA: 0x09F20BF8  token: 0x60005FF
        public System.Void SetFlag(System.UInt32 flag, System.Boolean sw) { }
        // RVA: 0x041E1670  token: 0x6000600
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BA  // size: 0x50
    public sealed struct CopyRenderBuffer : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> renderPosList;  // 0x10
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> renderNormalList;  // 0x20
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> renderPosArray;  // 0x30
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> renderNormalArray;  // 0x40

        // Methods
        // RVA: 0x04DA35E0  token: 0x6000601
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x20000BB  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly MagicaCloth.PhysicsManagerMeshData.<>c <>9;  // static @ 0x0
        public static System.Comparison<MagicaCloth.RenderMeshDeformer> <>9__158_0;  // static @ 0x8

        // Methods
        // RVA: 0x09F20D50  token: 0x6000602
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000603
        public System.Void .ctor() { }
        // RVA: 0x09F20D20  token: 0x6000604
        private System.Int32 <DispatchWriting>b__158_0(MagicaCloth.RenderMeshDeformer a, MagicaCloth.RenderMeshDeformer b) { }

    }

    // TypeToken: 0x20000BD  // size: 0x14
    public sealed struct ParticleFlag
    {
        // Fields
        public System.UInt32 flag;  // 0x10

        // Methods
        // RVA: 0x04430530  token: 0x600061E
        public System.Void .ctor(System.UInt32[] flags) { }
        // RVA: 0x09F20B04  token: 0x600061F
        public System.Boolean IsFlag(System.UInt32 flag) { }
        // RVA: 0x09F20BE4  token: 0x6000620
        public System.Void SetFlag(System.UInt32 flag, System.Boolean sw) { }
        // RVA: 0x09F2A3F0  token: 0x6000621
        public System.Boolean IsValid() { }
        // RVA: 0x04430680  token: 0x6000622
        public System.Void SetEnable(System.Boolean sw) { }
        // RVA: 0x09F2A3B4  token: 0x6000623
        public System.Boolean IsFixed() { }
        // RVA: 0x09F2A3C4  token: 0x6000624
        public System.Boolean IsMove() { }
        // RVA: 0x09F2A3BC  token: 0x6000625
        public System.Boolean IsKinematic() { }
        // RVA: 0x09F20B2C  token: 0x6000626
        public System.Boolean IsHold() { }
        // RVA: 0x04430520  token: 0x6000627
        public System.Boolean IsCollider() { }
        // RVA: 0x04430660  token: 0x6000628
        public System.Boolean IsReadTransform() { }
        // RVA: 0x09F2A3F8  token: 0x6000629
        public System.Boolean IsWriteTransform() { }
        // RVA: 0x04430670  token: 0x600062A
        public System.Boolean IsRestoreTransform() { }
        // RVA: 0x09F2A3E4  token: 0x600062B
        public System.Boolean IsUnityPhysics() { }
        // RVA: 0x09F2A3D8  token: 0x600062C
        public System.Boolean IsReadSclTransform() { }
        // RVA: 0x09F2A3CC  token: 0x600062D
        public System.Boolean IsParentTransform() { }

    }

    // TypeToken: 0x20000BE  // size: 0x160
    public sealed struct CopyBoneToParticleJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamData;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.WorldInfluence> teamWorldInfluenceList;  // 0x20
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x30
        public Unity.Collections.NativeArray<System.Single> depthList;  // 0x40
        public Unity.Collections.NativeArray<System.Int32> transformIndexList;  // 0x50
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> localPosList;  // 0x60
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x70
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> velocityList;  // 0x80
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> bonePosList;  // 0x90
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> boneRotList;  // 0xa0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> boneSclList;  // 0xb0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0xc0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> oldPosList;  // 0xd0
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> oldRotList;  // 0xe0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> oldSlowPosList;  // 0xf0
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> rotList;  // 0x100
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> snapBasePosList;  // 0x110
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> snapBaseRotList;  // 0x120
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> oldBasePosList;  // 0x130
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> oldBaseRotList;  // 0x140
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0x150

        // Methods
        // RVA: 0x09F25B74  token: 0x600062E
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x20000BF  // size: 0x130
    public sealed struct ResetParticleJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamData;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x20
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x30
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> snapBasePosList;  // 0x40
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> snapBaseRotList;  // 0x50
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> basePosList;  // 0x60
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> baseRotList;  // 0x70
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> oldBasePosList;  // 0x80
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> oldBaseRotList;  // 0x90
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0xa0
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> rotList;  // 0xb0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> oldPosList;  // 0xc0
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> oldRotList;  // 0xd0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> oldSlowPosList;  // 0xe0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> velocityList;  // 0xf0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> nextPosList;  // 0x100
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> nextRotList;  // 0x110
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> localPosList;  // 0x120

        // Methods
        // RVA: 0x09F337CC  token: 0x600062F
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x20000C0  // size: 0xC0
    public sealed struct CopyParticleToBoneJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x10
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0x20
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> rotList;  // 0x30
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> boneBasePosList;  // 0x40
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> boneBaseRotList;  // 0x50
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x60
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x70
        public Unity.Collections.NativeParallelMultiHashMap<System.Int32,System.Int32> transformParticleIndexMap;  // 0x80
        public Unity.Collections.NativeArray<System.Int32> writeBoneIndexList;  // 0x90
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> bonePosList;  // 0xa0
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> boneRotList;  // 0xb0

        // Methods
        // RVA: 0x09F26484  token: 0x6000630
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x20000C2  // size: 0x14
    public sealed struct ForceMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.PhysicsManagerTeamData.ForceMode None;  // const
        public static MagicaCloth.PhysicsManagerTeamData.ForceMode VelocityAdd;  // const
        public static MagicaCloth.PhysicsManagerTeamData.ForceMode VelocityChange;  // const
        public static MagicaCloth.PhysicsManagerTeamData.ForceMode VelocityAddWithoutMass;  // const
        public static MagicaCloth.PhysicsManagerTeamData.ForceMode VelocityChangeWithoutMass;  // const

    }

    // TypeToken: 0x20000C3  // size: 0x110
    public sealed struct TeamData
    {
        // Fields
        public MagicaCloth.ChunkData particleChunk;  // 0x10
        public MagicaCloth.ChunkData colliderChunk;  // 0x20
        public MagicaCloth.ChunkData skinningBoneChunk;  // 0x30
        public System.UInt32 flag;  // 0x40
        public System.Single dynamicFriction;  // 0x44
        public System.Single staticFriction;  // 0x48
        public System.Single selfCollisionRange;  // 0x4c
        public System.Int32 boneIndex;  // 0x50
        public Unity.Mathematics.float3 initScale;  // 0x54
        public System.Single scaleRatio;  // 0x60
        public Unity.Mathematics.float3 scaleDirection;  // 0x64
        public Unity.Mathematics.float4 quaternionScale;  // 0x70
        public System.Single time;  // 0x80
        public System.Single oldTime;  // 0x84
        public System.Single addTime;  // 0x88
        public System.Single timeScale;  // 0x8c
        public System.Single nowTime;  // 0x90
        public System.Single startTime;  // 0x94
        public System.Int32 updateCount;  // 0x98
        public System.Single blendRatio;  // 0x9c
        public System.Single outPhysicsPoseBlendRatio;  // 0xa0
        public Unity.Mathematics.float3 externalForce;  // 0xa4
        public System.Single forceMassInfluence;  // 0xb0
        public System.Single forceWindInfluence;  // 0xb4
        public System.Single forceWindRandomScale;  // 0xb8
        public System.Single forceWindSynchronization;  // 0xbc
        public System.Single velocityWeight;  // 0xc0
        public System.Single velocityRecoverySpeed;  // 0xc4
        public Unity.Mathematics.float3 gravityDirection;  // 0xc8
        public MagicaCloth.PhysicsManagerTeamData.ForceMode forceMode;  // 0xd4
        public Unity.Mathematics.float3 impactForce;  // 0xd8
        public System.Int32 calcCount;  // 0xe4
        public System.Int16 restoreDistanceGroupIndex;  // 0xe8
        public System.Int16 triangleBendGroupIndex;  // 0xea
        public System.Int16 clampDistanceGroupIndex;  // 0xec
        public System.Int16 clampDistance2GroupIndex;  // 0xee
        public System.Int16 clampPositionGroupIndex;  // 0xf0
        public System.Int16 clampRotationGroupIndex;  // 0xf2
        public System.Int16 restoreRotationGroupIndex;  // 0xf4
        public System.Int16 adjustRotationGroupIndex;  // 0xf6
        public System.Int16 springGroupIndex;  // 0xf8
        public System.Int16 volumeGroupIndex;  // 0xfa
        public System.Int16 airLineGroupIndex;  // 0xfc
        public System.Int16 lineWorkerGroupIndex;  // 0xfe
        public System.Int16 triangleWorkerGroupIndex;  // 0x100
        public System.Int16 selfCollisionGroupIndex;  // 0x102
        public System.Int16 edgeCollisionGroupIndex;  // 0x104
        public System.Int16 penetrationGroupIndex;  // 0x106
        public System.Int16 baseSkinningGroupIndex;  // 0x108
        public System.Int16 twistGroupIndex;  // 0x10a
        public System.Int16 compositeRotationGroupIndex;  // 0x10c

        // Methods
        // RVA: 0x09F34424  token: 0x600066C
        public System.Boolean IsActive() { }
        // RVA: 0x09F34460  token: 0x600066D
        public System.Boolean IsRunning() { }
        // RVA: 0x09F3446C  token: 0x600066E
        public System.Boolean IsUpdate(System.Int32 runCount) { }
        // RVA: 0x09F34434  token: 0x600066F
        public System.Boolean IsInterpolate() { }
        // RVA: 0x09F34448  token: 0x6000670
        public System.Boolean IsPhysicsUpdate() { }
        // RVA: 0x09F3442C  token: 0x6000671
        public System.Boolean IsFlag(System.UInt32 flag) { }
        // RVA: 0x09F34478  token: 0x6000672
        public System.Void SetFlag(System.UInt32 flag, System.Boolean sw) { }
        // RVA: 0x09F34454  token: 0x6000673
        public System.Boolean IsReset() { }
        // RVA: 0x09F3443C  token: 0x6000674
        public System.Boolean IsPause() { }

    }

    // TypeToken: 0x20000C4  // size: 0xAC
    public sealed struct WorldInfluence
    {
        // Fields
        public MagicaCloth.CurveParam moveInfluence;  // 0x10
        public MagicaCloth.CurveParam rotInfluence;  // 0x20
        public System.Single maxMoveSpeed;  // 0x30
        public System.Single maxRotationSpeed;  // 0x34
        public Unity.Mathematics.float3 nowPosition;  // 0x38
        public Unity.Mathematics.float3 oldPosition;  // 0x44
        public Unity.Mathematics.float3 moveOffset;  // 0x50
        public System.Single moveIgnoreRatio;  // 0x5c
        public Unity.Mathematics.quaternion nowRotation;  // 0x60
        public Unity.Mathematics.quaternion oldRotation;  // 0x70
        public Unity.Mathematics.quaternion rotationOffset;  // 0x80
        public System.Single rotationIgnoreRatio;  // 0x90
        public System.Int32 resetTeleport;  // 0x94
        public System.Single teleportDistance;  // 0x98
        public System.Single teleportRotation;  // 0x9c
        public MagicaCloth.ClothParams.TeleportMode teleportMode;  // 0xa0
        public System.Single stabilizationTime;  // 0xa4
        public System.Single clampRotationLimit;  // 0xa8

    }

    // TypeToken: 0x20000C5  // size: 0x64
    public sealed struct WindInfo
    {
        // Fields
        public System.Int32 windCount;  // 0x10
        public Unity.Mathematics.int4 windDataIndexList;  // 0x14
        public Unity.Mathematics.float3x4 windDirectionList;  // 0x24
        public Unity.Mathematics.float4 windMainList;  // 0x54

    }

    // TypeToken: 0x20000C6  // size: 0xA8
    public sealed struct PreProcessTeamDataJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Single deltaTime;  // 0x10
        public System.Single physicsDeltaTime;  // 0x14
        public System.Single updateDeltaTime;  // 0x18
        public System.Single globalTimeScale;  // 0x1c
        public System.Int32 maxUpdateCount;  // 0x20
        public System.Single unityTimeScale;  // 0x24
        public System.Single elapsedTime;  // 0x28
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamData;  // 0x30
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.WorldInfluence> teamWorldInfluenceList;  // 0x40
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.WindInfo> teamWindInfoList;  // 0x50
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> bonePosList;  // 0x60
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> boneRotList;  // 0x70
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> boneSclList;  // 0x80
        public System.Int32 windCount;  // 0x90
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerWindData.WindData> windData;  // 0x98

        // Methods
        // RVA: 0x09F2FEFC  token: 0x6000675
        public virtual System.Void Execute(System.Int32 teamId) { }
        // RVA: 0x09F30DA0  token: 0x6000676
        private System.Void UpdateTime(MagicaCloth.PhysicsManagerTeamData.TeamData& tdata, System.Boolean reset, System.Single dtime) { }
        // RVA: 0x09F30E94  token: 0x6000677
        private System.Void Wind(MagicaCloth.PhysicsManagerTeamData.TeamData& tdata, Unity.Mathematics.float3 pos, System.Int32 teamId) { }

    }

    // TypeToken: 0x20000C7  // size: 0x38
    public sealed struct PostProcessTeamDataJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Int32 fixedUpdateCount;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamData;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.WorldInfluence> teamWorldInfluenceList;  // 0x28

        // Methods
        // RVA: 0x09F2FBF8  token: 0x6000678
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x20000C8  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly MagicaCloth.PhysicsManagerTeamData.<>c <>9;  // static @ 0x0
        public static System.Action<System.Int32> <>9__76_0;  // static @ 0x8

        // Methods
        // RVA: 0x09F3526C  token: 0x6000679
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600067A
        public System.Void .ctor() { }
        // RVA: 0x09F35210  token: 0x600067B
        private System.Void <ResetFuturePredictionCollidere>b__76_0(System.Int32 pindex) { }

    }

    // TypeToken: 0x20000CA  // size: 0x14
    public sealed struct WindType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.PhysicsManagerWindData.WindType None;  // const
        public static MagicaCloth.PhysicsManagerWindData.WindType Direction;  // const
        public static MagicaCloth.PhysicsManagerWindData.WindType Area;  // const

    }

    // TypeToken: 0x20000CB  // size: 0x14
    public sealed struct ShapeType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.PhysicsManagerWindData.ShapeType Box;  // const
        public static MagicaCloth.PhysicsManagerWindData.ShapeType Sphere;  // const

    }

    // TypeToken: 0x20000CC  // size: 0x14
    public sealed struct DirectionType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.PhysicsManagerWindData.DirectionType OneDirection;  // const
        public static MagicaCloth.PhysicsManagerWindData.DirectionType Radial;  // const

    }

    // TypeToken: 0x20000CD  // size: 0x60
    public sealed struct WindData
    {
        // Fields
        public System.UInt32 flag;  // 0x10
        public MagicaCloth.PhysicsManagerWindData.WindType windType;  // 0x14
        public MagicaCloth.PhysicsManagerWindData.ShapeType shapeType;  // 0x18
        public System.Int32 transformIndex;  // 0x1c
        public Unity.Mathematics.float3 areaSize;  // 0x20
        public System.Single main;  // 0x2c
        public System.Single turbulence;  // 0x30
        public System.Single frequency;  // 0x34
        public Unity.Mathematics.float3 direction;  // 0x38
        public MagicaCloth.PhysicsManagerWindData.DirectionType directionType;  // 0x44
        public System.Single areaVolume;  // 0x48
        public System.Single areaLength;  // 0x4c
        public MagicaCloth.CurveParam attenuation;  // 0x50

        // Methods
        // RVA: 0x09F20B04  token: 0x6000687
        public System.Boolean IsFlag(System.UInt32 flag) { }
        // RVA: 0x09F20BE4  token: 0x6000688
        public System.Void SetFlag(System.UInt32 flag, System.Boolean sw) { }
        // RVA: 0x09F35448  token: 0x6000689
        public System.Void SetEnable(System.Boolean sw) { }
        // RVA: 0x09F2A3F0  token: 0x600068A
        public System.Boolean IsActive() { }

    }

    // TypeToken: 0x20000D1  // size: 0x14
    public sealed struct Axis
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.MagicaCapsuleCollider.Axis X;  // const
        public static MagicaCloth.MagicaCapsuleCollider.Axis Y;  // const
        public static MagicaCloth.MagicaCapsuleCollider.Axis Z;  // const

    }

    // TypeToken: 0x20000D6  // size: 0x14
    public sealed struct TeamUpdateMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.PhysicsTeam.TeamUpdateMode Normal;  // const
        public static MagicaCloth.PhysicsTeam.TeamUpdateMode UnityPhysics;  // const

    }

    // TypeToken: 0x20000D7  // size: 0x14
    public sealed struct TeamCullingMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.PhysicsTeam.TeamCullingMode Reset;  // const
        public static MagicaCloth.PhysicsTeam.TeamCullingMode Pause;  // const
        public static MagicaCloth.PhysicsTeam.TeamCullingMode Off;  // const

    }

    // TypeToken: 0x20000D8  // size: 0x14
    public sealed struct TeamSkinningMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.PhysicsTeam.TeamSkinningMode None;  // const
        public static MagicaCloth.PhysicsTeam.TeamSkinningMode UserAnimation;  // const

    }

    // TypeToken: 0x20000DB  // size: 0x24
    public sealed struct AdjustRotationData
    {
        // Fields
        public System.Int32 keyIndex;  // 0x10
        public System.Int32 targetIndex;  // 0x14
        public Unity.Mathematics.float3 localPos;  // 0x18

        // Methods
        // RVA: 0x09F228AC  token: 0x600071A
        public System.Boolean IsValid() { }

    }

    // TypeToken: 0x20000DC  // size: 0x38
    public sealed struct GroupData
    {
        // Fields
        public System.Int32 teamId;  // 0x10
        public System.Int32 active;  // 0x14
        public System.Int32 adjustMode;  // 0x18
        public Unity.Mathematics.float3 axisRotationPower;  // 0x1c
        public MagicaCloth.ChunkData chunk;  // 0x28

    }

    // TypeToken: 0x20000DD  // size: 0xB0
    public sealed struct AdjustRotationJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<MagicaCloth.AdjustRotationWorker.AdjustRotationData> dataList;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.AdjustRotationWorker.GroupData> groupList;  // 0x20
        public Unity.Collections.NativeParallelMultiHashMap<System.Int32,System.Int32> particleMap;  // 0x30
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x40
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x50
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x60
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> snapBasePosList;  // 0x70
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> snapBaseRotList;  // 0x80
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0x90
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> rotList;  // 0xa0

        // Methods
        // RVA: 0x09F228BC  token: 0x600071B
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x20000DF  // size: 0x58
    public sealed struct BaseSkinningData
    {
        // Fields
        public Unity.Mathematics.float3 localPos;  // 0x10
        public Unity.Mathematics.float3 localNor;  // 0x1c
        public Unity.Mathematics.float3 localTan;  // 0x28
        public Unity.Mathematics.int4 boneIndices;  // 0x34
        public Unity.Mathematics.float4 weights;  // 0x44
        public System.Int16 weightCount;  // 0x54

        // Methods
        // RVA: 0x09F2371C  token: 0x6000725
        public System.Boolean IsValid() { }

    }

    // TypeToken: 0x20000E0  // size: 0x3C
    public sealed struct GroupData
    {
        // Fields
        public System.Int32 teamId;  // 0x10
        public System.Int32 active;  // 0x14
        public System.Int32 updateFixed;  // 0x18
        public MagicaCloth.ChunkData dataChunk;  // 0x1c
        public MagicaCloth.ChunkData bindPoseChunk;  // 0x2c

    }

    // TypeToken: 0x20000E1  // size: 0xD0
    public sealed struct BaseSkinningJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<MagicaCloth.BaseSkinningWorker.GroupData> groupList;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.BaseSkinningWorker.BaseSkinningData> dataList;  // 0x20
        public Unity.Collections.NativeArray<Unity.Mathematics.float4x4> bindPoseList;  // 0x30
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x40
        public Unity.Collections.NativeArray<System.Int32> skinningBoneList;  // 0x50
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> bonePosList;  // 0x60
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> boneRotList;  // 0x70
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> boneSclList;  // 0x80
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x90
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0xa0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> snapBasePosList;  // 0xb0
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> snapBaseRotList;  // 0xc0

        // Methods
        // RVA: 0x09F23728  token: 0x6000726
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x20000E3  // size: 0x38
    public sealed struct LineRotationData
    {
        // Fields
        public System.Int32 vertexIndex;  // 0x10
        public System.Int32 childCount;  // 0x14
        public System.Int32 childStartDataIndex;  // 0x18
        public Unity.Mathematics.float3 localPos;  // 0x1c
        public Unity.Mathematics.quaternion localRot;  // 0x28

    }

    // TypeToken: 0x20000E4  // size: 0x14
    public sealed struct LineRotationRootInfo
    {
        // Fields
        public System.UInt16 startIndex;  // 0x10
        public System.UInt16 dataLength;  // 0x12

    }

    // TypeToken: 0x20000E5  // size: 0x38
    public sealed struct GroupData
    {
        // Fields
        public System.Int32 teamId;  // 0x10
        public System.Int32 avarage;  // 0x14
        public MagicaCloth.ChunkData dataChunk;  // 0x18
        public MagicaCloth.ChunkData rootInfoChunk;  // 0x28

    }

    // TypeToken: 0x20000E6  // size: 0x98
    public sealed struct LineRotationJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.Int32 fixedUpdateCount;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.LineWorker.LineRotationData> dataList;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.LineWorker.LineRotationRootInfo> rootInfoList;  // 0x28
        public Unity.Collections.NativeArray<System.Int32> rootTeamList;  // 0x38
        public Unity.Collections.NativeArray<MagicaCloth.LineWorker.GroupData> groupList;  // 0x48
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x58
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x68
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0x78
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> rotList;  // 0x88

        // Methods
        // RVA: 0x09F267FC  token: 0x6000730
        public virtual System.Void Execute(System.Int32 rootIndex) { }

    }

    // TypeToken: 0x20000E8  // size: 0x18
    public sealed struct CreateData
    {
        // Fields
        public System.Int32 vertexIndex;  // 0x10
        public System.Int32 particleIndex;  // 0x14

    }

    // TypeToken: 0x20000E9  // size: 0xA0
    public sealed struct VertexToParticleJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.VirtualMeshInfo> virtualMeshInfoList;  // 0x10
        public Unity.Collections.NativeArray<System.Int32> vertexToParticleList;  // 0x20
        public Unity.Collections.NativeParallelMultiHashMap<System.Int32,System.Int32> vertexToParticleMap;  // 0x30
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0x40
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> rotList;  // 0x50
        public Unity.Collections.NativeArray<System.Int16> virtualVertexMeshIndexList;  // 0x60
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> snapBasePosList;  // 0x70
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> snapBaseRotList;  // 0x80
        private Unity.Collections.NativeParallelMultiHashMapIterator<System.Int32> iterator;  // 0x90

        // Methods
        // RVA: 0x09F352D0  token: 0x6000739
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x20000EA  // size: 0xE0
    public sealed struct ParticleToVertexJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<System.Int32> vertexToParticleList;  // 0x10
        public Unity.Collections.NativeParallelMultiHashMap<System.Int32,System.Int32> vertexToParticleMap;  // 0x20
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> virtualPosList;  // 0x30
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> virtualRotList;  // 0x40
        public Unity.Collections.NativeArray<System.Byte> virtualVertexFlagList;  // 0x50
        public Unity.Collections.NativeArray<System.Int16> virtualVertexMeshIndexList;  // 0x60
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.VirtualMeshInfo> virtualMeshInfoList;  // 0x70
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x80
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x90
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> particleFlagList;  // 0xa0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> particlePosList;  // 0xb0
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> particleRotList;  // 0xc0
        private Unity.Collections.NativeParallelMultiHashMapIterator<System.Int32> iterator;  // 0xd0

        // Methods
        // RVA: 0x09F2A404  token: 0x600073A
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x20000EC  // size: 0x148
    public sealed struct CalcVertexUseFlagJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public System.UInt32 updateFlag;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.RenderMeshInfo> renderMeshInfoList;  // 0x18
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.SharedRenderMeshInfo> sharedRenderMeshInfoList;  // 0x28
        public Unity.Collections.NativeArray<System.Byte> virtualVertexUseList;  // 0x38
        public Unity.Collections.NativeArray<System.Byte> virtualVertexFixList;  // 0x48
        public Unity.Collections.NativeArray<System.UInt32> sharedChildVertexInfoList;  // 0x58
        public Unity.Collections.NativeArray<MagicaCloth.MeshData.VertexWeight> sharedChildVertexWeightList;  // 0x68
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> sharedRenderVertices;  // 0x78
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> sharedRenderNormals;  // 0x88
        public Unity.Collections.NativeArray<Unity.Mathematics.float4> sharedRenderTangents;  // 0x98
        public Unity.Collections.NativeArray<System.Byte> sharedBonesPerVertexList;  // 0xa8
        public Unity.Collections.NativeArray<System.Int32> sharedBonesPerVertexStartList;  // 0xb8
        public Unity.Collections.NativeArray<UnityEngine.BoneWeight1> sharedBoneWeightList;  // 0xc8
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> renderPosList;  // 0xd8
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> renderNormalList;  // 0xe8
        public Unity.Collections.NativeArray<Unity.Mathematics.float4> renderTangentList;  // 0xf8
        public Unity.Collections.NativeArray<UnityEngine.BoneWeight1> renderBoneWeightList;  // 0x108
        public Unity.Collections.NativeArray<System.UInt32> renderVertexFlagList;  // 0x118
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> renderPosArray;  // 0x128
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> renderNormalArray;  // 0x138

        // Methods
        // RVA: 0x09F24994  token: 0x6000744
        public virtual System.Void Execute(System.Int32 rmindex) { }

    }

    // TypeToken: 0x20000ED  // size: 0xF0
    public sealed struct CollectLocalPositionNormalTangentForEachMeshJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.RenderMeshInfo> renderMeshInfoList;  // 0x10
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> transformPosList;  // 0x20
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> transformRotList;  // 0x30
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> transformSclList;  // 0x40
        public Unity.Collections.NativeArray<System.UInt32> sharedChildVertexInfoList;  // 0x50
        public Unity.Collections.NativeArray<MagicaCloth.MeshData.VertexWeight> sharedChildVertexWeightList;  // 0x60
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> virtualPosList;  // 0x70
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> virtualRotList;  // 0x80
        public Unity.Collections.NativeArray<System.UInt32> renderVertexFlagList;  // 0x90
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> renderPosList;  // 0xa0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> renderNormalList;  // 0xb0
        public Unity.Collections.NativeArray<Unity.Mathematics.float4> renderTangentList;  // 0xc0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> renderPosArray;  // 0xd0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> renderNormalArray;  // 0xe0

        // Methods
        // RVA: 0x09F24F30  token: 0x6000745
        public virtual System.Void Execute(System.Int32 rmindex) { }

    }

    // TypeToken: 0x20000EE  // size: 0xF0
    public sealed struct CollectLocalPositionNormalTangentForEachVertexJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.RenderMeshInfo> renderMeshInfoList;  // 0x10
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> transformPosList;  // 0x20
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> transformRotList;  // 0x30
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> transformSclList;  // 0x40
        public Unity.Collections.NativeArray<System.UInt32> sharedChildVertexInfoList;  // 0x50
        public Unity.Collections.NativeArray<MagicaCloth.MeshData.VertexWeight> sharedChildVertexWeightList;  // 0x60
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> virtualPosList;  // 0x70
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> virtualRotList;  // 0x80
        public Unity.Collections.NativeArray<System.UInt32> renderVertexFlagList;  // 0x90
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> renderPosList;  // 0xa0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> renderNormalList;  // 0xb0
        public Unity.Collections.NativeArray<Unity.Mathematics.float4> renderTangentList;  // 0xc0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> renderPosArray;  // 0xd0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> renderNormalArray;  // 0xe0

        // Methods
        // RVA: 0x09F2528C  token: 0x6000746
        public virtual System.Void Execute(System.Int32 vindex) { }

    }

    // TypeToken: 0x20000F0  // size: 0x18
    public sealed struct SpringData
    {
        // Fields
        public System.Int32 particleIndex;  // 0x10
        public System.Single weight;  // 0x14

    }

    // TypeToken: 0x20000F1  // size: 0xD0
    public sealed struct SpringJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<System.Int32> springVertexList;  // 0x10
        public Unity.Collections.NativeParallelMultiHashMap<System.Int32,MagicaCloth.SpringMeshWorker.SpringData> springMap;  // 0x20
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x30
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> particlePosList;  // 0x40
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> particleRotList;  // 0x50
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> snapBasePosList;  // 0x60
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> snapBaseRotList;  // 0x70
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> virtualPosList;  // 0x80
        public Unity.Collections.NativeArray<System.Byte> virtualVertexFlagList;  // 0x90
        public Unity.Collections.NativeArray<System.Int16> virtualVertexMeshIndexList;  // 0xa0
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.VirtualMeshInfo> virtualMeshInfoList;  // 0xb0
        private Unity.Collections.NativeParallelMultiHashMapIterator<System.Int32> iterator;  // 0xc0

        // Methods
        // RVA: 0x09F339B0  token: 0x600074F
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x20000F3  // size: 0x2C
    public sealed struct TriangleRotationData
    {
        // Fields
        public System.Int32 targetIndex;  // 0x10
        public System.Int32 triangleCount;  // 0x14
        public System.Int32 triangleStartIndex;  // 0x18
        public Unity.Mathematics.quaternion localRot;  // 0x1c

        // Methods
        // RVA: 0x09F34490  token: 0x6000758
        public System.Boolean IsValid() { }

    }

    // TypeToken: 0x20000F4  // size: 0x34
    public sealed struct GroupData
    {
        // Fields
        public System.Int32 teamId;  // 0x10
        public MagicaCloth.ChunkData triangleDataChunk;  // 0x14
        public MagicaCloth.ChunkData triangleIndexChunk;  // 0x24

    }

    // TypeToken: 0x20000F5  // size: 0x90
    public sealed struct TriangleRotationJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<MagicaCloth.TriangleWorker.TriangleRotationData> triangleDataList;  // 0x10
        public Unity.Collections.NativeArray<System.Int32> triangleIndexList;  // 0x20
        public Unity.Collections.NativeArray<MagicaCloth.TriangleWorker.GroupData> groupList;  // 0x30
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x40
        public Unity.Collections.NativeArray<System.Int32> teamIdList;  // 0x50
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x60
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> posList;  // 0x70
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> rotList;  // 0x80

        // Methods
        // RVA: 0x09F34498  token: 0x6000759
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x20000F7  // size: 0xE0
    public sealed struct ReadMeshPositionJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.VirtualMeshInfo> virtualMeshInfoList;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.SharedVirtualMeshInfo> sharedVirtualMeshInfoList;  // 0x20
        public Unity.Collections.NativeArray<System.Int16> virtualVertexMeshIndexList;  // 0x30
        public Unity.Collections.NativeArray<System.Byte> virtualVertexUseList;  // 0x40
        public Unity.Collections.NativeArray<System.Int32> virtualTransformIndexList;  // 0x50
        public Unity.Collections.NativeArray<System.UInt32> sharedVirtualVertexInfoList;  // 0x60
        public Unity.Collections.NativeArray<MagicaCloth.MeshData.VertexWeight> sharedVirtualWeightList;  // 0x70
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> transformPosList;  // 0x80
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> transformRotList;  // 0x90
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> transformSclList;  // 0xa0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> virtualPosList;  // 0xb0
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> virtualRotList;  // 0xc0
        public Unity.Collections.NativeArray<System.Byte> virtualVertexFlagList;  // 0xd0

        // Methods
        // RVA: 0x09F474E0  token: 0x6000762
        public virtual System.Void Execute(System.Int32 vindex) { }

    }

    // TypeToken: 0x20000F8  // size: 0xB0
    public sealed struct CalcMeshTriangleNormalTangentJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.VirtualMeshInfo> virtualMeshInfoList;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.SharedVirtualMeshInfo> sharedVirtualMeshInfoList;  // 0x20
        public Unity.Collections.NativeArray<System.UInt16> virtualTriangleMeshIndexList;  // 0x30
        public Unity.Collections.NativeArray<System.Byte> virtualVertexUseList;  // 0x40
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> virtualPosList;  // 0x50
        public Unity.Collections.NativeArray<System.Int32> sharedTriangles;  // 0x60
        public Unity.Collections.NativeArray<Unity.Mathematics.float2> sharedMeshUv;  // 0x70
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> virtualTriangleNormalList;  // 0x80
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> virtualTriangleTangentList;  // 0x90
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> transformSclList;  // 0xa0

        // Methods
        // RVA: 0x09F364E0  token: 0x6000763
        public virtual System.Void Execute(System.Int32 tindex) { }

    }

    // TypeToken: 0x20000F9  // size: 0xB0
    public sealed struct CalcVertexNormalTangentFromTriangleJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.VirtualMeshInfo> virtualMeshInfoList;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.SharedVirtualMeshInfo> sharedVirtualMeshInfoList;  // 0x20
        public Unity.Collections.NativeArray<System.Int16> virtualVertexMeshIndexList;  // 0x30
        public Unity.Collections.NativeArray<System.Byte> virtualVertexUseList;  // 0x40
        public Unity.Collections.NativeArray<System.Byte> virtualVertexFlagList;  // 0x50
        public Unity.Collections.NativeArray<System.UInt32> sharedVirtualVertexToTriangleInfoList;  // 0x60
        public Unity.Collections.NativeArray<System.Int32> sharedVirtualVertexToTriangleIndexList;  // 0x70
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> virtualTriangleNormalList;  // 0x80
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> virtualTriangleTangentList;  // 0x90
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> virtualRotList;  // 0xa0

        // Methods
        // RVA: 0x09F36ACC  token: 0x6000764
        public virtual System.Void Execute(System.Int32 vindex) { }

    }

    // TypeToken: 0x20000FA  // size: 0xE0
    public sealed struct CalcMeshTriangleNormalTangentForEachMeshJob : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.VirtualMeshInfo> virtualMeshInfoList;  // 0x10
        public Unity.Collections.NativeArray<MagicaCloth.PhysicsManagerMeshData.SharedVirtualMeshInfo> sharedVirtualMeshInfoList;  // 0x20
        public Unity.Collections.NativeArray<System.Byte> virtualVertexUseList;  // 0x30
        public Unity.Collections.NativeArray<System.Byte> virtualVertexFlagList;  // 0x40
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> virtualPosList;  // 0x50
        public Unity.Collections.NativeArray<System.Int32> sharedTriangles;  // 0x60
        public Unity.Collections.NativeArray<Unity.Mathematics.float2> sharedMeshUv;  // 0x70
        public Unity.Collections.NativeArray<System.UInt32> sharedVirtualVertexToTriangleInfoList;  // 0x80
        public Unity.Collections.NativeArray<System.Int32> sharedVirtualVertexToTriangleIndexList;  // 0x90
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> transformSclList;  // 0xa0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> virtualTriangleNormalList;  // 0xb0
        public Unity.Collections.NativeArray<Unity.Mathematics.float3> virtualTriangleTangentList;  // 0xc0
        public Unity.Collections.NativeArray<Unity.Mathematics.quaternion> virtualRotList;  // 0xd0

        // Methods
        // RVA: 0x09F35BA4  token: 0x6000765
        public virtual System.Void Execute(System.Int32 mindex) { }

    }

    // TypeToken: 0x20000FE  // size: 0x30
    public sealed class <Start>d__3 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public MagicaCloth.BlastWaveController <>4__this;  // 0x20
        private System.Single <main>5__2;  // 0x28
        private System.Single <time>5__3;  // 0x2c

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000770
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000771
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09F49C0C  token: 0x6000772
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09F49D9C  token: 0x6000774
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000100  // size: 0x14
    public sealed struct MoveMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.CameraOrbit.MoveMode None;  // const
        public static MagicaCloth.CameraOrbit.MoveMode UpDown;  // const
        public static MagicaCloth.CameraOrbit.MoveMode Free;  // const

    }

    // TypeToken: 0x2000102  // size: 0x30
    public sealed class <CreateObject>d__7 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public MagicaCloth.CopyObject <>4__this;  // 0x20
        private System.Int32 <i>5__2;  // 0x28
        private System.Int32 <j>5__3;  // 0x2c

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000786
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000787
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09F499DC  token: 0x6000788
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09F49BC0  token: 0x600078A
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000104  // size: 0x30
    public class AvatarPartsGroup
    {
        // Fields
        public System.String groupName;  // 0x10
        public System.Collections.Generic.List<UnityEngine.GameObject> partsPrefabList;  // 0x18
        public System.Int32 id;  // 0x20
        public System.Int32 handle;  // 0x24
        public System.Int32 index;  // 0x28

        // Methods
        // RVA: 0x09F35848  token: 0x6000794
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000106  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly MagicaCloth.ModelController.<>c <>9;  // static @ 0x0
        public static System.Action<UnityEngine.Animator> <>9__7_0;  // static @ 0x8
        public static System.Action<UnityEngine.Animator> <>9__8_0;  // static @ 0x10
        public static System.Action<MagicaCloth.BaseCloth> <>9__10_0;  // static @ 0x18

        // Methods
        // RVA: 0x09F49F84  token: 0x600079E
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600079F
        public System.Void .ctor() { }
        // RVA: 0x09F49E8C  token: 0x60007A0
        private System.Void <OnNextButton>b__7_0(UnityEngine.Animator ani) { }
        // RVA: 0x09F49E44  token: 0x60007A1
        private System.Void <OnBackButton>b__8_0(UnityEngine.Animator ani) { }
        // RVA: 0x09F49DE8  token: 0x60007A2
        private System.Void <OnActiveButton>b__10_0(MagicaCloth.BaseCloth cloth) { }

    }

    // TypeToken: 0x2000107  // size: 0x18
    public sealed class <>c__DisplayClass9_0
    {
        // Fields
        public System.Single timeScale;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60007A3
        public System.Void .ctor() { }
        // RVA: 0x09F49F3C  token: 0x60007A4
        private System.Void <OnSlowButton>b__0(UnityEngine.Animator ani) { }
        // RVA: 0x09F49F60  token: 0x60007A5
        private System.Void <OnSlowButton>b__1(MagicaCloth.BaseCloth cloth) { }

    }

    // TypeToken: 0x200010C  // size: 0x20
    public sealed class <>c__DisplayClass5_0
    {
        // Fields
        public System.Action<System.Int32,System.Int32> onClick;  // 0x10
        public MagicaCloth.UIPartsItem <>4__this;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60007BE
        public System.Void .ctor() { }
        // RVA: 0x09F49ED4  token: 0x60007BF
        private System.Void <Init>b__0() { }
        // RVA: 0x09F49F08  token: 0x60007C0
        private System.Void <Init>b__1() { }

    }

    // TypeToken: 0x2000116  // size: 0x20
    public class Point
    {
        // Fields
        public System.Int32 id;  // 0x10
        public Unity.Mathematics.float3 pos;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x600080F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000128  // size: 0x20
    public class TetraVertex
    {
        // Fields
        public System.Int32 index;  // 0x10
        public UnityEngine.Vector3 pos;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x60008F3
        public System.Void .ctor() { }
        // RVA: 0x04DA3830  token: 0x60008F4
        public System.Void .ctor(UnityEngine.Vector3 pos, System.Int32 index) { }

    }

    // TypeToken: 0x2000129  // size: 0x38
    public class Tetra
    {
        // Fields
        public System.Collections.Generic.List<MagicaCloth.MeshUtility.TetraVertex> vertexList;  // 0x10
        public UnityEngine.Vector3 circumCenter;  // 0x18
        public System.Single circumRadius;  // 0x24
        public UnityEngine.Vector3 tetraCenter;  // 0x28
        public System.Single tetraSize;  // 0x34

        // Methods
        // RVA: 0x09F4C934  token: 0x60008F5
        public System.Void .ctor() { }
        // RVA: 0x09F4C818  token: 0x60008F6
        public System.Void .ctor(MagicaCloth.MeshUtility.TetraVertex a, MagicaCloth.MeshUtility.TetraVertex b, MagicaCloth.MeshUtility.TetraVertex c, MagicaCloth.MeshUtility.TetraVertex d) { }
        // RVA: 0x09F4C4CC  token: 0x60008F7
        public System.UInt64 GetTetraHash() { }
        // RVA: 0x09F4BA9C  token: 0x60008F8
        public System.Void CalcCircumcircle() { }
        // RVA: 0x09ED4E1C  token: 0x60008F9
        public System.Boolean IntersectCircumcircle(UnityEngine.Vector3 pos) { }
        // RVA: 0x09ED4B88  token: 0x60008FA
        public System.Boolean CheckSame(MagicaCloth.MeshUtility.Tetra tri) { }
        // RVA: 0x09F4C478  token: 0x60008FB
        public System.Boolean ContainsPoint(MagicaCloth.MeshUtility.TetraVertex p1) { }
        // RVA: 0x09F4C3AC  token: 0x60008FC
        public System.Boolean ContainsPoint(MagicaCloth.MeshUtility.TetraVertex p1, MagicaCloth.MeshUtility.TetraVertex p2, MagicaCloth.MeshUtility.TetraVertex p3, MagicaCloth.MeshUtility.TetraVertex p4) { }
        // RVA: 0x09F4C104  token: 0x60008FD
        public System.Void CalcSize() { }
        // RVA: 0x09F4C5C0  token: 0x60008FE
        public System.Boolean Verification() { }

    }

    // TypeToken: 0x200012C  // size: 0x14
    public sealed struct UpdateCount
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.UpdateTimeManager.UpdateCount _60;  // const
        public static MagicaCloth.UpdateTimeManager.UpdateCount _90_Default;  // const
        public static MagicaCloth.UpdateTimeManager.UpdateCount _120;  // const
        public static MagicaCloth.UpdateTimeManager.UpdateCount _150;  // const
        public static MagicaCloth.UpdateTimeManager.UpdateCount _180;  // const

    }

    // TypeToken: 0x200012D  // size: 0x14
    public sealed struct UpdateMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.UpdateTimeManager.UpdateMode UnscaledTime;  // const
        public static MagicaCloth.UpdateTimeManager.UpdateMode OncePerFrame;  // const
        public static MagicaCloth.UpdateTimeManager.UpdateMode DelayUnscaledTime;  // const

    }

    // TypeToken: 0x200012E  // size: 0x14
    public sealed struct UpdateLocation
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.UpdateTimeManager.UpdateLocation AfterLateUpdate;  // const
        public static MagicaCloth.UpdateTimeManager.UpdateLocation BeforeLateUpdate;  // const

    }

namespace MagicaCloth
{

    // TypeToken: 0x2000025  // size: 0xC8
    public abstract class BaseCloth : MagicaCloth.PhysicsTeam
    {
        // Fields
        protected MagicaCloth.ClothParams clothParams;  // 0x78
        protected System.Collections.Generic.List<System.Int32> clothParamDataHashList;  // 0x80
        private MagicaCloth.ClothData clothData;  // 0x88
        protected System.Int32 clothDataHash;  // 0x90
        protected System.Int32 clothDataVersion;  // 0x94
        private MagicaCloth.SelectionData clothSelection;  // 0x98
        private System.Int32 clothSelectionHash;  // 0xa0
        private System.Int32 clothSelectionVersion;  // 0xa4
        private System.Collections.Generic.List<UnityEngine.Renderer> cullRendererList;  // 0xa8
        protected MagicaCloth.ClothSetup setup;  // 0xb0
        private System.Single oldBlendRatio;  // 0xb8
        private MagicaCloth.PhysicsTeam.TeamUpdateMode oldUpdateMode;  // 0xbc
        private MagicaCloth.PhysicsTeam.TeamCullingMode oldCullingMode;  // 0xc0
        private System.Boolean oldUseAnimatedDistance;  // 0xc4

        // Properties
        System.Single BlendWeight { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x09EC1054 */ }
        UnityEngine.Vector3 Gravity_GravityDirection { get; /* RVA: 0x09EC0F08 */ set; /* RVA: 0x09EC1374 */ }
        System.Boolean DistanceDisable_Active { get; /* RVA: 0x09EC0E48 */ set; /* RVA: 0x09EC1130 */ }
        UnityEngine.Transform DistanceDisable_ReferenceObject { get; /* RVA: 0x09EC0EA8 */ set; /* RVA: 0x09EC11A0 */ }
        System.Single DistanceDisable_Distance { get; /* RVA: 0x09EC0E68 */ set; /* RVA: 0x09EC1150 */ }
        System.Single DistanceDisable_FadeDistance { get; /* RVA: 0x09EC0E88 */ set; /* RVA: 0x09EC1178 */ }
        System.Single ExternalForce_WindInfluence { get; /* RVA: 0x09EC0EC8 */ set; /* RVA: 0x09EC11CC */ }
        System.Single ExternalForce_WindRandomScale { get; /* RVA: 0x09EC0EE8 */ set; /* RVA: 0x09EC12A0 */ }
        System.Single WorldInfluence_MaxMoveSpeed { get; /* RVA: 0x09EC0F94 */ set; /* RVA: 0x09EC16AC */ }
        System.Boolean WorldInfluence_ResetAfterTeleport { get; /* RVA: 0x09EC0FB4 */ set; /* RVA: 0x09EC1774 */ }
        System.Single WorldInfluence_TeleportDistance { get; /* RVA: 0x09EC0FF4 */ set; /* RVA: 0x09EC18DC */ }
        System.Single WorldInfluence_TeleportRotation { get; /* RVA: 0x09EC1034 */ set; /* RVA: 0x09EC1A60 */ }
        MagicaCloth.ClothParams.TeleportMode WorldInfluence_TeleportMode { get; /* RVA: 0x09EC1014 */ set; /* RVA: 0x09EC19A0 */ }
        System.Single WorldInfluence_StabilizationTime { get; /* RVA: 0x09EC0FD4 */ set; /* RVA: 0x09EC1838 */ }
        System.Boolean ColliderCollision_Active { get; /* RVA: 0x09EC0E28 */ set; /* RVA: 0x09EC106C */ }
        System.Boolean Penetration_Active { get; /* RVA: 0x09EC0F34 */ set; /* RVA: 0x09EC1454 */ }
        System.Boolean Spring_Active { get; /* RVA: 0x09EC0F54 */ set; /* RVA: 0x09EC151C */ }
        System.Single Spring_Power { get; /* RVA: 0x09EC0F74 */ set; /* RVA: 0x09EC15DC */ }
        MagicaCloth.ClothParams Params { get; /* RVA: 0x04D862A0 */ }
        MagicaCloth.ClothData ClothData { get; /* RVA: 0x04D861F0 */ set; /* RVA: 0x05395D30 */ }
        MagicaCloth.SelectionData ClothSelection { get; /* RVA: 0x04D85EE0 */ }
        MagicaCloth.ClothSetup Setup { get; /* RVA: 0x04D86250 */ }

        // Methods
        // RVA: 0x09EC01C0  token: 0x60000B5
        public System.Void ResetCloth() { }
        // RVA: 0x09EC0190  token: 0x60000B6
        public System.Void ResetCloth(System.Single resetStabilizationTime) { }
        // RVA: 0x09EC01F0  token: 0x60000B7
        public System.Void ResetCloth(MagicaCloth.ClothParams.TeleportMode teleportMode, System.Single resetStabilizationTime) { }
        // RVA: 0x09EC01F8  token: 0x60000B8
        public System.Void SetTimeScale(System.Single timeScale) { }
        // RVA: 0x09EBF494  token: 0x60000B9
        public System.Single GetTimeScale() { }
        // RVA: 0x09EBE780  token: 0x60000BA
        public System.Void AddForce(UnityEngine.Vector3 force, MagicaCloth.PhysicsManagerTeamData.ForceMode mode) { }
        // RVA: 0x09EBE6DC  token: 0x60000BD
        public System.Void AddCollider(MagicaCloth.ColliderComponent collider) { }
        // RVA: 0x09EBFEE0  token: 0x60000BE
        public System.Void RemoveCollider(MagicaCloth.ColliderComponent collider) { }
        // RVA: 0x04D865A0  token: 0x60000BF
        public System.Void SetUpdateMode(MagicaCloth.PhysicsTeam.TeamUpdateMode updateMode) { }
        // RVA: 0x04D86590  token: 0x60000C0
        public System.Void SetCullingMode(MagicaCloth.PhysicsTeam.TeamCullingMode cullingMode) { }
        // RVA: 0x09EBFD58  token: 0x60000C1
        public System.Void Radius_SetRadius(System.Single startVal, System.Single endVal, System.Single curveVal) { }
        // RVA: 0x09EBF714  token: 0x60000C2
        public System.Void Mass_SetMass(System.Single startVal, System.Single endVal, System.Single curveVal) { }
        // RVA: 0x09EBEA80  token: 0x60000C3
        public System.Void ClampPosition_SetPositionLength(System.Single startVal, System.Single endVal, System.Single curveVal) { }
        // RVA: 0x09EBF58C  token: 0x60000C4
        public System.Void Gravity_SetGravity(System.Single startVal, System.Single endVal, System.Single curveVal) { }
        // RVA: 0x09EBEF80  token: 0x60000C7
        public System.Void Drag_SetDrag(System.Single startVal, System.Single endVal, System.Single curveVal) { }
        // RVA: 0x09EBF15C  token: 0x60000D0
        public System.Void ExternalForce_DepthInfluence(System.Single startVal, System.Single endVal, System.Single curveVal) { }
        // RVA: 0x09EC0B30  token: 0x60000D5
        public System.Void WorldInfluence_SetMovementInfluence(System.Single startVal, System.Single endVal, System.Single curveVal) { }
        // RVA: 0x09EC0C2C  token: 0x60000D6
        public System.Void WorldInfluence_SetRotationInfluence(System.Single startVal, System.Single endVal, System.Single curveVal) { }
        // RVA: 0x09EC09E8  token: 0x60000E3
        public System.Void WorldInfluence_ReplaceInfluenceTarget(UnityEngine.Transform target) { }
        // RVA: 0x09EBFC48  token: 0x60000E8
        public System.Void Penetration_SetMovingRadius(System.Single startVal, System.Single endVal, System.Single curveVal) { }
        // RVA: 0x09EBF3C8  token: 0x60000ED
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x041E1670  token: 0x60000F3
        protected virtual System.Void Reset() { }
        // RVA: 0x09EBFC00  token: 0x60000F4
        protected virtual System.Void OnValidate() { }
        // RVA: 0x09EBFBE0  token: 0x60000F5
        protected virtual System.Void OnInit() { }
        // RVA: 0x09EBF8AC  token: 0x60000F6
        protected virtual System.Void OnActive() { }
        // RVA: 0x09EBFADC  token: 0x60000F7
        protected virtual System.Void OnInactive() { }
        // RVA: 0x09EBFABC  token: 0x60000F8
        protected virtual System.Void OnDispose() { }
        // RVA: 0x09EC0528  token: 0x60000F9
        private virtual System.Void UpdateCullingMode(MagicaCloth.CoreComponent caller) { }
        // RVA: 0x09EBF9C4  token: 0x60000FA
        protected virtual System.Void OnChangeCalculation() { }
        // RVA: 0x09EBF2A8  token: 0x60000FB
        public System.Int32 GetCullRenderListCount() { }
        // RVA: 0x09EBE8F8  token: 0x60000FC
        private System.Void BaseClothInit() { }
        // RVA: 0x09EBE860  token: 0x60000FD
        private System.Void BaseClothDispose() { }
        // RVA: 0x09EBECD0  token: 0x60000FE
        protected virtual System.Void ClothInit() { }
        // RVA: 0x09EBEC2C  token: 0x60000FF
        protected virtual System.Void ClothActive() { }
        // RVA: 0x09EBECA8  token: 0x6000100
        protected virtual System.Void ClothInactive() { }
        // RVA: 0x041E1670  token: 0x6000101
        protected virtual System.Void ClothDispose() { }
        // RVA: -1  // abstract  token: 0x6000102
        protected virtual System.UInt32 UserFlag(System.Int32 vindex) { }
        // RVA: -1  // abstract  token: 0x6000103
        protected virtual UnityEngine.Transform UserTransform(System.Int32 vindex) { }
        // RVA: -1  // abstract  token: 0x6000104
        protected virtual Unity.Mathematics.float3 UserTransformLocalPosition(System.Int32 vindex) { }
        // RVA: -1  // abstract  token: 0x6000105
        protected virtual Unity.Mathematics.quaternion UserTransformLocalRotation(System.Int32 vindex) { }
        // RVA: -1  // abstract  token: 0x6000106
        public virtual System.Boolean IsRequiresDeformer() { }
        // RVA: -1  // abstract  token: 0x6000107
        public virtual MagicaCloth.BaseMeshDeformer GetDeformer() { }
        // RVA: -1  // abstract  token: 0x6000108
        protected virtual MagicaCloth.MeshData GetMeshData() { }
        // RVA: -1  // abstract  token: 0x6000109
        protected virtual System.Void WorkerInit() { }
        // RVA: 0x09EC0280  token: 0x600010A
        private System.Void SetUseMesh(System.Boolean sw) { }
        // RVA: 0x09EC02EC  token: 0x600010B
        private System.Void SetUseVertex(System.Boolean sw) { }
        // RVA: -1  // abstract  token: 0x600010C
        protected virtual System.Void SetDeformerUseVertex(System.Boolean sw, MagicaCloth.BaseMeshDeformer deformer) { }
        // RVA: 0x09EBEF44  token: 0x600010D
        private System.Void DeformerForEach(System.Action<MagicaCloth.BaseMeshDeformer> act) { }
        // RVA: 0x09EC0354  token: 0x600010E
        public System.Void UpdateBlend() { }
        // RVA: -1  // generic def  token: 0x600010F
        public virtual System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict) { }
        // RVA: 0x09EBF510  token: 0x6000110
        public virtual System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones() { }
        // RVA: 0x09EBE9F4  token: 0x6000111
        protected virtual System.Void ChangeUseUnityPhysics(System.Boolean sw) { }
        // RVA: 0x09EBED98  token: 0x6000112
        public virtual System.Void CreateVerifyData() { }
        // RVA: 0x09EC08CC  token: 0x6000113
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: 0x09EBF664  token: 0x6000114
        public System.Boolean HasChangedParam(MagicaCloth.ClothParams.ParamType ptype) { }
        // RVA: 0x09EC0844  token: 0x6000115
        public MagicaCloth.Define.Error VerifyAlgorithmVersion() { }
        // RVA: 0x09EC080C  token: 0x6000116
        public virtual System.Boolean UpgradeFormat() { }
        // RVA: 0x09EBF22C  token: 0x6000117
        public virtual System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject() { }
        // RVA: 0x09EBF058  token: 0x6000118
        public virtual MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source) { }
        // RVA: 0x09EBFF6C  token: 0x6000119
        private System.Void ResetClothInternal(MagicaCloth.ClothParams.TeleportMode teleportMode, System.Single resetStabilizationTime) { }
        // RVA: 0x09EC0D28  token: 0x600011A
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000027  // size: 0x38
    public abstract class CoreComponent : MagicaCloth.BaseComponent, MagicaCloth.IShareDataObject, MagicaCloth.IDataVerify, MagicaCloth.IEditorMesh, MagicaCloth.IEditorCloth, MagicaCloth.IDataHash, MagicaCloth.IBoneReplace
    {
        // Fields
        protected System.Int32 dataHash;  // 0x18
        protected System.Int32 dataVersion;  // 0x1c
        protected MagicaCloth.RuntimeStatus status;  // 0x20
        private System.Int32 useUnityPhysicsCount;  // 0x28
        private System.Boolean nowUseUnityPhysics;  // 0x2c
        private System.Boolean <IsVisible>k__BackingField;  // 0x2d
        protected System.Int32 calculateValue;  // 0x30

        // Properties
        MagicaCloth.RuntimeStatus Status { get; /* RVA: 0x04D862C0 */ }
        System.Boolean IsVisible { get; /* RVA: 0x04D86CB0 */ set; /* RVA: 0x04D86CE0 */ }
        System.Boolean IsCalculate { get; /* RVA: 0x068D2DA0 */ }
        System.Int32 SaveDataHash { get; /* RVA: 0x011EF5B0 */ }
        System.Int32 SaveDataVersion { get; /* RVA: 0x04D86310 */ }

        // Methods
        // RVA: 0x09EE5F4C  token: 0x600011E
        public System.Collections.Generic.Dictionary<System.String,UnityEngine.Transform> GetUsedComponentBones() { }
        // RVA: 0x09EE5EB0  token: 0x600011F
        public System.Collections.Generic.List<System.String> GetUsedComponentBoneNames() { }
        // RVA: 0x09EE65B8  token: 0x6000120
        public System.Void ReplaceComponentBone(System.Collections.Generic.Dictionary<UnityEngine.Transform,UnityEngine.Transform> boneReplaceDict) { }
        // RVA: 0x09EE6570  token: 0x6000121
        public System.Void ReplaceComponentBone(System.Collections.Generic.Dictionary<System.String,UnityEngine.Transform> boneReplaceDict) { }
        // RVA: -1  // abstract  token: 0x6000126
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x09EE6698  token: 0x6000129
        protected virtual System.Void Start() { }
        // RVA: 0x09EE64CC  token: 0x600012A
        public virtual System.Void OnEnable() { }
        // RVA: 0x09EE648C  token: 0x600012B
        public virtual System.Void OnDisable() { }
        // RVA: 0x09EE6418  token: 0x600012C
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x09EE622C  token: 0x600012D
        public System.Void Init() { }
        // RVA: -1  // abstract  token: 0x600012E
        protected virtual System.Void OnInit() { }
        // RVA: -1  // abstract  token: 0x600012F
        protected virtual System.Void OnDispose() { }
        // RVA: -1  // abstract  token: 0x6000130
        protected virtual System.Void OnUpdate() { }
        // RVA: -1  // abstract  token: 0x6000131
        protected virtual System.Void OnActive() { }
        // RVA: -1  // abstract  token: 0x6000132
        protected virtual System.Void OnInactive() { }
        // RVA: 0x09EE6504  token: 0x6000133
        protected virtual System.Void OnUpdateStatus() { }
        // RVA: 0x09EE64C4  token: 0x6000134
        protected virtual System.Void OnDisconnectedStatus() { }
        // RVA: 0x09EE6600  token: 0x6000135
        public System.Void SetUseUnityPhysics(System.Boolean sw) { }
        // RVA: 0x09EE5C58  token: 0x6000136
        private System.Void ActiveUseUnityPhysics() { }
        // RVA: 0x09EE6210  token: 0x6000137
        private System.Void InactiveUseUnityPhysics() { }
        // RVA: 0x041E1670  token: 0x6000138
        protected virtual System.Void ChangeUseUnityPhysics(System.Boolean sw) { }
        // RVA: 0x0931FCE4  token: 0x6000139
        protected System.Boolean IsUseUnityPhysics() { }
        // RVA: 0x09EE5CAC  token: 0x600013A
        public virtual System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject() { }
        // RVA: -1  // abstract  token: 0x600013B
        public virtual MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source) { }
        // RVA: 0x09EE6664  token: 0x600013C
        protected System.Void SetUserEnable(System.Boolean sw) { }
        // RVA: 0x041E1670  token: 0x600013D
        private virtual System.Void UpdateCullingMode(MagicaCloth.CoreComponent Caller) { }
        // RVA: 0x041E1670  token: 0x600013E
        protected virtual System.Void OnChangeCalculation() { }
        // RVA: -1  // abstract  token: 0x600013F
        public virtual System.Int32 GetVersion() { }
        // RVA: -1  // abstract  token: 0x6000140
        public virtual System.Int32 GetErrorVersion() { }
        // RVA: 0x09EE66DC  token: 0x6000141
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: 0x09EE66A0  token: 0x6000142
        public MagicaCloth.Define.Error VerifyDataVersion() { }
        // RVA: 0x09EE63F4  token: 0x6000143
        public System.Boolean IsOldDataVertion() { }
        // RVA: 0x09EE5C7C  token: 0x6000144
        public virtual System.Void CreateVerifyData() { }
        // RVA: 0x09EE5DF0  token: 0x6000145
        public virtual System.String GetInformation() { }
        // RVA: 0x012081B0  token: 0x6000146
        public virtual System.Boolean UpgradeFormat() { }
        // RVA: -1  // generic def  token: 0x6000147
        public System.Void ChangeAvatar(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict) { }
        // RVA: -1  // generic def  token: 0x6000148
        public virtual System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict) { }
        // RVA: 0x09EE5E1C  token: 0x6000149
        public virtual System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones() { }
        // RVA: 0x09EE5D58  token: 0x600014A
        public virtual System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList) { }
        // RVA: 0x09EE5DA4  token: 0x600014B
        public virtual System.Collections.Generic.List<System.Int32> GetEditorTriangleList() { }
        // RVA: 0x09EE5D0C  token: 0x600014C
        public virtual System.Collections.Generic.List<System.Int32> GetEditorLineList() { }
        // RVA: 0x011EC580  token: 0x600014D
        public virtual System.Collections.Generic.List<System.Int32> GetSelectionList() { }
        // RVA: 0x011EC580  token: 0x600014E
        public virtual System.Collections.Generic.List<System.Int32> GetUseList() { }
        // RVA: 0x09EE6760  token: 0x600014F
        protected System.Void .ctor() { }
        // RVA: 0x0464E630  token: 0x6000150
        private System.Object <Init>b__27_0() { }

    }

    // TypeToken: 0x2000028  // size: 0x30
    public class AvatarPartsAttachEvent : UnityEngine.Events.UnityEvent`2
    {
        // Methods
        // RVA: 0x09ED815C  token: 0x6000151
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000029  // size: 0x30
    public class AvatarPartsDetachEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x09ED8198  token: 0x6000152
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002A  // size: 0x30
    public class PhysicsManagerPreUpdateEvent : UnityEngine.Events.UnityEvent
    {
        // Methods
        // RVA: 0x05393538  token: 0x6000153
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002B  // size: 0x30
    public class PhysicsManagerPostUpdateEvent : UnityEngine.Events.UnityEvent
    {
        // Methods
        // RVA: 0x05393538  token: 0x6000154
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002C  // size: 0x60
    public class MagicaAreaWind : MagicaCloth.WindComponent
    {
        // Fields
        private MagicaCloth.PhysicsManagerWindData.ShapeType shapeType;  // 0x58
        private System.Boolean isAddition;  // 0x5c

        // Properties
        MagicaCloth.PhysicsManagerWindData.ShapeType ShapeType { get; /* RVA: 0x04D86570 */ set; /* RVA: 0x09EE69DC */ }
        System.Boolean Addition { get; /* RVA: 0x04D86D90 */ set; /* RVA: 0x09EE69BC */ }

        // Methods
        // RVA: 0x04DA3370  token: 0x6000159
        public virtual MagicaCloth.ComponentType GetComponentType() { }
        // RVA: 0x02BBE540  token: 0x600015A
        public virtual MagicaCloth.PhysicsManagerWindData.WindType GetWindType() { }
        // RVA: 0x04D86570  token: 0x600015B
        public virtual MagicaCloth.PhysicsManagerWindData.ShapeType GetShapeType() { }
        // RVA: 0x09EE6944  token: 0x600015C
        public virtual MagicaCloth.PhysicsManagerWindData.DirectionType GetDirectionType() { }
        // RVA: 0x04D86D90  token: 0x600015D
        public virtual System.Boolean IsAddition() { }
        // RVA: 0x09EE6804  token: 0x600015E
        public virtual UnityEngine.Vector3 GetAreaSize() { }
        // RVA: 0x09EE689C  token: 0x600015F
        public virtual System.Single GetAreaVolume() { }
        // RVA: 0x09EE67B8  token: 0x6000160
        public virtual System.Single GetAreaLength() { }
        // RVA: 0x09EE6954  token: 0x6000161
        protected virtual System.Void ResetParams() { }
        // RVA: 0x09EE69B4  token: 0x6000162
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002D  // size: 0x58
    public class MagicaAvatar : MagicaCloth.CoreComponent
    {
        // Fields
        private static System.Int32 DATA_VERSION;  // const
        private System.Boolean dataReset;  // 0x38
        private MagicaCloth.MagicaAvatarRuntime runtime;  // 0x40
        public MagicaCloth.AvatarPartsAttachEvent OnAttachParts;  // 0x48
        public MagicaCloth.AvatarPartsDetachEvent OnDetachParts;  // 0x50

        // Properties
        System.Boolean DataReset { get; /* RVA: 0x011F2A70 */ set; /* RVA: 0x011F2A80 */ }
        MagicaCloth.MagicaAvatarRuntime Runtime { get; /* RVA: 0x04A37980 */ }

        // Methods
        // RVA: 0x09EE959C  token: 0x6000163
        public System.Int32 AttachAvatarParts(UnityEngine.GameObject avatarPartsPrefab, System.Action<UnityEngine.GameObject> instanceAction) { }
        // RVA: 0x09EE969C  token: 0x6000164
        public System.Void DetachAvatarParts(System.Int32 partsId) { }
        // RVA: 0x09EE96C8  token: 0x6000165
        public System.Void DetachAvatarParts(UnityEngine.GameObject avatarPartsObject) { }
        // RVA: 0x09EE966C  token: 0x6000166
        public System.Void DetachAvatarParts(MagicaCloth.MagicaAvatarParts parts) { }
        // RVA: 0x04DA3390  token: 0x6000167
        public virtual MagicaCloth.ComponentType GetComponentType() { }
        // RVA: 0x011EC580  token: 0x6000168
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x04D919E0  token: 0x600016C
        private System.Void Reset() { }
        // RVA: 0x041E1670  token: 0x600016D
        private System.Void OnValidate() { }
        // RVA: 0x09EE9DE4  token: 0x600016E
        protected virtual System.Void OnInit() { }
        // RVA: 0x09EE9D94  token: 0x600016F
        protected virtual System.Void OnDispose() { }
        // RVA: 0x041E1670  token: 0x6000170
        protected virtual System.Void OnUpdate() { }
        // RVA: 0x09EE9D6C  token: 0x6000171
        protected virtual System.Void OnActive() { }
        // RVA: 0x09EE9DBC  token: 0x6000172
        protected virtual System.Void OnInactive() { }
        // RVA: 0x04D85B80  token: 0x6000173
        public virtual System.Int32 GetVersion() { }
        // RVA: 0x011EC580  token: 0x6000174
        public virtual System.Int32 GetErrorVersion() { }
        // RVA: 0x09EE9664  token: 0x6000175
        public virtual System.Void CreateVerifyData() { }
        // RVA: 0x09EE9E0C  token: 0x6000176
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: 0x09EE9740  token: 0x6000177
        public virtual System.String GetInformation() { }
        // RVA: 0x09EE9738  token: 0x6000178
        public virtual System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject() { }
        // RVA: 0x011EC580  token: 0x6000179
        public virtual MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source) { }
        // RVA: 0x09EE9E84  token: 0x600017A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002E  // size: 0x58
    public class MagicaDirectionalWind : MagicaCloth.WindComponent
    {
        // Methods
        // RVA: 0x04DA33E0  token: 0x600017B
        public virtual MagicaCloth.ComponentType GetComponentType() { }
        // RVA: 0x04D85B80  token: 0x600017C
        public virtual MagicaCloth.PhysicsManagerWindData.WindType GetWindType() { }
        // RVA: 0x011EC580  token: 0x600017D
        public virtual MagicaCloth.PhysicsManagerWindData.ShapeType GetShapeType() { }
        // RVA: 0x011EC580  token: 0x600017E
        public virtual MagicaCloth.PhysicsManagerWindData.DirectionType GetDirectionType() { }
        // RVA: 0x012081B0  token: 0x600017F
        public virtual System.Boolean IsAddition() { }
        // RVA: 0x04DA33B0  token: 0x6000180
        public virtual UnityEngine.Vector3 GetAreaSize() { }
        // RVA: 0x04DA33D0  token: 0x6000181
        public virtual System.Single GetAreaVolume() { }
        // RVA: 0x04DA33A0  token: 0x6000182
        public virtual System.Single GetAreaLength() { }
        // RVA: 0x09EE9F64  token: 0x6000183
        protected virtual System.Void ResetParams() { }
        // RVA: 0x09EE69B4  token: 0x6000184
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002F  // size: 0x80
    public class MagicaPhysicsManager : MagicaCloth.CreateSingleton`1
    {
        // Fields
        private MagicaCloth.UpdateTimeManager updateTime;  // 0x18
        private MagicaCloth.PhysicsManagerParticleData particle;  // 0x20
        private MagicaCloth.PhysicsManagerBoneData bone;  // 0x28
        private MagicaCloth.PhysicsManagerMeshData mesh;  // 0x30
        private MagicaCloth.PhysicsManagerTeamData team;  // 0x38
        private MagicaCloth.PhysicsManagerWindData wind;  // 0x40
        private MagicaCloth.PhysicsManagerComponent component;  // 0x48
        private MagicaCloth.PhysicsManagerCompute compute;  // 0x50
        private System.Boolean useFasterWrite;  // 0x58
        private UnityEngine.ComputeShader meshWriter;  // 0x60
        public MagicaCloth.PhysicsManagerPreUpdateEvent OnPreUpdate;  // 0x68
        public MagicaCloth.PhysicsManagerPostUpdateEvent OnPostUpdate;  // 0x70
        private System.Boolean useDelay;  // 0x78
        private System.Boolean updatePlayerLoop;  // 0x79
        private System.Boolean isActive;  // 0x7a

        // Properties
        MagicaCloth.UpdateTimeManager.UpdateCount UpdatePerSeccond { get; /* RVA: 0x09EEA36C */ set; /* RVA: 0x09EEA3D4 */ }
        MagicaCloth.UpdateTimeManager.UpdateMode UpdateMode { get; /* RVA: 0x09EEA350 */ set; /* RVA: 0x09EEA3B8 */ }
        System.Single FuturePredictionRate { get; /* RVA: 0x09EEA1B8 */ set; /* RVA: 0x09EEA388 */ }
        MagicaCloth.UpdateTimeManager UpdateTime { get; /* RVA: 0x0385B100 */ }
        MagicaCloth.PhysicsManagerParticleData Particle { get; /* RVA: 0x04A379B0 */ }
        MagicaCloth.PhysicsManagerBoneData Bone { get; /* RVA: 0x03233960 */ }
        MagicaCloth.PhysicsManagerMeshData Mesh { get; /* RVA: 0x03235430 */ }
        MagicaCloth.PhysicsManagerTeamData Team { get; /* RVA: 0x032354C0 */ }
        MagicaCloth.PhysicsManagerWindData Wind { get; /* RVA: 0x04A37980 */ }
        MagicaCloth.PhysicsManagerComponent Component { get; /* RVA: 0x03233E50 */ }
        MagicaCloth.PhysicsManagerCompute Compute { get; /* RVA: 0x03235C30 */ }
        System.Boolean IsDelay { get; /* RVA: 0x04D86F90 */ }
        System.Boolean IsActive { get; /* RVA: 0x04D88E50 */ set; /* RVA: 0x09EEA3B0 */ }
        System.Boolean IsFasterWrite { get; /* RVA: 0x09EEA1D8 */ }
        UnityEngine.ComputeShader MeshWriterShader { get; /* RVA: 0x09EEA28C */ }

        // Methods
        // RVA: 0x09EEA18C  token: 0x6000189
        public System.Void SetGlobalTimeScale(System.Single timeScale) { }
        // RVA: 0x09EEA030  token: 0x600018A
        public System.Single GetGlobalTimeScale() { }
        // RVA: 0x04D71DC0  token: 0x600019A
        protected virtual System.Void Awake() { }
        // RVA: 0x04A378A0  token: 0x600019B
        protected virtual System.Void InitSingleton() { }
        // RVA: 0x09EE9FC4  token: 0x600019C
        protected virtual System.Void DuplicateDetection(MagicaCloth.MagicaPhysicsManager duplicate) { }
        // RVA: 0x04D84EB0  token: 0x600019D
        protected System.Void OnEnable() { }
        // RVA: 0x09EEA158  token: 0x600019E
        protected System.Void OnDisable() { }
        // RVA: 0x03FF1BE0  token: 0x600019F
        private System.Void Update() { }
        // RVA: 0x03E626F0  token: 0x60001A0
        private System.Void FixedUpdate() { }
        // RVA: 0x09EEA050  token: 0x60001A1
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x03233B20  token: 0x60001A2
        private System.Void AfterEarlyUpdate() { }
        // RVA: 0x03235170  token: 0x60001A3
        private System.Void AfterFixedUpdate() { }
        // RVA: 0x03233B60  token: 0x60001A4
        private System.Void AfterUpdate() { }
        // RVA: 0x03233990  token: 0x60001A5
        private System.Void BeforeLateUpdate() { }
        // RVA: 0x032339C0  token: 0x60001A6
        private System.Void AfterLateUpdate() { }
        // RVA: 0x03235550  token: 0x60001A7
        private System.Void DefaultUpdate() { }
        // RVA: 0x03233A50  token: 0x60001A8
        private System.Void PostLateUpdate() { }
        // RVA: 0x032339F0  token: 0x60001A9
        private System.Void AfterRendering() { }
        // RVA: 0x04D7D2E0  token: 0x60001AA
        private static System.Void Init() { }
        // RVA: 0x03FF1C10  token: 0x60001AB
        public static System.Void InitCustomGameLoop() { }
        // RVA: 0x03326620  token: 0x60001AC
        public static System.Void SetCustomGameLoop(UnityEngine.LowLevel.PlayerLoopSystem& playerLoop) { }
        // RVA: 0x03324CC0  token: 0x60001AD
        private static System.Boolean CheckRegist(UnityEngine.LowLevel.PlayerLoopSystem& playerLoop) { }
        // RVA: 0x046D73A0  token: 0x60001AE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000032  // size: 0x58
    public abstract class WindComponent : MagicaCloth.BaseComponent
    {
        // Fields
        protected System.Single main;  // 0x18
        protected System.Single turbulence;  // 0x1c
        protected System.Single frequency;  // 0x20
        protected UnityEngine.Vector3 areaSize;  // 0x24
        protected System.Single areaRadius;  // 0x30
        protected System.Single directionAngleX;  // 0x34
        protected System.Single directionAngleY;  // 0x38
        protected MagicaCloth.PhysicsManagerWindData.DirectionType directionType;  // 0x3c
        protected MagicaCloth.BezierParam attenuation;  // 0x40
        protected System.Int32 windId;  // 0x48
        protected MagicaCloth.RuntimeStatus status;  // 0x50

        // Properties
        System.Single Main { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x09EEB830 */ }
        System.Single Turbulence { get; /* RVA: 0x04D868F0 */ set; /* RVA: 0x09EEB870 */ }
        System.Single Frequency { get; /* RVA: 0x011EC8A0 */ set; /* RVA: 0x09EEB750 */ }
        UnityEngine.Vector3 MainDirection { get; /* RVA: 0x09EEB57C */ set; /* RVA: 0x09EEB77C */ }
        System.Single DirectionAngleX { get; /* RVA: 0x04D8E050 */ set; /* RVA: 0x09EEB6E8 */ }
        System.Single DirectionAngleY { get; /* RVA: 0x04D88360 */ set; /* RVA: 0x09EEB70C */ }
        UnityEngine.Vector3 AreaSize { get; /* RVA: 0x04D905E0 */ set; /* RVA: 0x09EEB634 */ }
        System.Single AreaRadius { get; /* RVA: 0x04D86E20 */ set; /* RVA: 0x09EEB600 */ }
        MagicaCloth.PhysicsManagerWindData.DirectionType DirectionType { get; /* RVA: 0x04D86640 */ set; /* RVA: 0x09EEB730 */ }
        MagicaCloth.RuntimeStatus Status { get; /* RVA: 0x04D86270 */ }

        // Methods
        // RVA: 0x09EEB3EC  token: 0x60001D7
        public System.Void SetAttenuation(System.Single sval, System.Single eval, System.Boolean useEval, System.Single cval, System.Boolean useCval) { }
        // RVA: 0x09EEB3DC  token: 0x60001D9
        protected virtual System.Void Reset() { }
        // RVA: 0x09EEB278  token: 0x60001DA
        protected virtual System.Void OnValidate() { }
        // RVA: 0x09EEB0D8  token: 0x60001DB
        private System.Void OnDidApplyAnimationProperties() { }
        // RVA: 0x09EEB434  token: 0x60001DC
        protected virtual System.Void Start() { }
        // RVA: 0x09EEB18C  token: 0x60001DD
        private virtual System.Void OnEnable() { }
        // RVA: 0x09EEB108  token: 0x60001DE
        private virtual System.Void OnDisable() { }
        // RVA: 0x09EEB0AC  token: 0x60001DF
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x09EEB43C  token: 0x60001E0
        protected virtual System.Void Update() { }
        // RVA: 0x09EEAF58  token: 0x60001E1
        private System.Void Init() { }
        // RVA: 0x09EEB20C  token: 0x60001E2
        protected System.Void OnUpdateStatus() { }
        // RVA: 0x02FFF600  token: 0x60001E3
        private virtual System.Boolean VerifyData() { }
        // RVA: 0x09EEB1CC  token: 0x60001E4
        protected virtual System.Void OnInit() { }
        // RVA: 0x09EEB140  token: 0x60001E5
        protected virtual System.Void OnDispose() { }
        // RVA: 0x09EEB248  token: 0x60001E6
        protected virtual System.Void OnUpdate() { }
        // RVA: 0x09EEB0A4  token: 0x60001E7
        protected virtual System.Void OnActive() { }
        // RVA: 0x09EEB1C4  token: 0x60001E8
        protected virtual System.Void OnInactive() { }
        // RVA: 0x09EEAE50  token: 0x60001E9
        protected System.Void EnableWind() { }
        // RVA: 0x09EEADA4  token: 0x60001EA
        protected System.Void DisableWind() { }
        // RVA: 0x09EEB354  token: 0x60001EB
        private System.Void RemoveWind() { }
        // RVA: 0x09EEAB7C  token: 0x60001EC
        private System.Void CreateWind() { }
        // RVA: 0x09EEAEE0  token: 0x60001ED
        private UnityEngine.Vector3 GetLocalDirection() { }
        // RVA: 0x09EEA994  token: 0x60001EE
        private System.Void ChangeParameter() { }
        // RVA: -1  // abstract  token: 0x60001EF
        public virtual MagicaCloth.PhysicsManagerWindData.WindType GetWindType() { }
        // RVA: -1  // abstract  token: 0x60001F0
        public virtual MagicaCloth.PhysicsManagerWindData.ShapeType GetShapeType() { }
        // RVA: -1  // abstract  token: 0x60001F1
        public virtual MagicaCloth.PhysicsManagerWindData.DirectionType GetDirectionType() { }
        // RVA: -1  // abstract  token: 0x60001F2
        public virtual System.Boolean IsAddition() { }
        // RVA: -1  // abstract  token: 0x60001F3
        public virtual UnityEngine.Vector3 GetAreaSize() { }
        // RVA: -1  // abstract  token: 0x60001F4
        public virtual System.Single GetAreaVolume() { }
        // RVA: -1  // abstract  token: 0x60001F5
        public virtual System.Single GetAreaLength() { }
        // RVA: -1  // abstract  token: 0x60001F6
        protected virtual System.Void ResetParams() { }
        // RVA: 0x09EEB4A0  token: 0x60001F7
        protected System.Void .ctor() { }
        // RVA: 0x0464E630  token: 0x60001F8
        private System.Object <Init>b__49_0() { }

    }

    // TypeToken: 0x2000033  // size: 0x18
    public abstract class MagicaAvatarAccess : System.IDisposable
    {
        // Fields
        protected MagicaCloth.MagicaAvatar owner;  // 0x10

        // Properties
        MagicaCloth.MagicaAvatarRuntime Runtime { get; /* RVA: 0x09EE69FC */ }

        // Methods
        // RVA: 0x053908C0  token: 0x60001FA
        public System.Void SetParent(MagicaCloth.MagicaAvatar avatar) { }
        // RVA: -1  // abstract  token: 0x60001FB
        public virtual System.Void Create() { }
        // RVA: -1  // abstract  token: 0x60001FC
        public virtual System.Void Dispose() { }
        // RVA: -1  // abstract  token: 0x60001FD
        public virtual System.Void Active() { }
        // RVA: -1  // abstract  token: 0x60001FE
        public virtual System.Void Inactive() { }
        // RVA: 0x041E1670  token: 0x60001FF
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000034  // size: 0x30
    public class MagicaAvatarParts : MagicaCloth.BaseComponent, MagicaCloth.IDataVerify
    {
        // Fields
        private MagicaCloth.MagicaAvatar parentAvatar;  // 0x18
        private System.Collections.Generic.Dictionary<System.String,UnityEngine.Transform> boneDict;  // 0x20
        private System.Collections.Generic.List<MagicaCloth.CoreComponent> magicaComponentList;  // 0x28

        // Properties
        MagicaCloth.MagicaAvatar ParentAvatar { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Boolean HasParent { get; /* RVA: 0x09EE74D4 */ }
        System.Int32 PartsId { get; /* RVA: 0x09EE7524 */ }

        // Methods
        // RVA: 0x04DA3380  token: 0x6000200
        public virtual MagicaCloth.ComponentType GetComponentType() { }
        // RVA: 0x09EE73E4  token: 0x6000205
        private System.Void OnDestroy() { }
        // RVA: 0x09EE6C34  token: 0x6000206
        public System.Void Dispose() { }
        // RVA: 0x09EE6A1C  token: 0x6000207
        public System.Collections.Generic.List<UnityEngine.Transform> CheckOverlappingTransform() { }
        // RVA: 0x09EE6CC0  token: 0x6000208
        public System.Collections.Generic.Dictionary<System.String,UnityEngine.Transform> GetBoneDict() { }
        // RVA: 0x09EE7338  token: 0x6000209
        public System.Collections.Generic.List<MagicaCloth.CoreComponent> GetMagicaComponentList() { }
        // RVA: 0x04D85B80  token: 0x600020A
        public virtual System.Int32 GetVersion() { }
        // RVA: 0x09EE6BE8  token: 0x600020B
        public virtual System.Void CreateVerifyData() { }
        // RVA: 0x09EE73EC  token: 0x600020C
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: 0x09EE6E80  token: 0x600020D
        public virtual System.String GetInformation() { }
        // RVA: 0x09EE7454  token: 0x600020E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000035  // size: 0x38
    public class MagicaAvatarRuntime : MagicaCloth.MagicaAvatarAccess
    {
        // Fields
        private System.Collections.Generic.Dictionary<System.String,UnityEngine.Transform> boneDict;  // 0x18
        private System.Collections.Generic.Dictionary<UnityEngine.Transform,System.Int32> boneReferenceDict;  // 0x20
        private System.Collections.Generic.List<MagicaCloth.MagicaAvatarParts> avatarPartsList;  // 0x28
        private System.Collections.Generic.List<MagicaCloth.ColliderComponent> colliderList;  // 0x30

        // Properties
        System.Int32 AvatarPartsCount { get; /* RVA: 0x09EE955C */ }

        // Methods
        // RVA: 0x09EE8870  token: 0x600020F
        public virtual System.Void Create() { }
        // RVA: 0x041E1670  token: 0x6000210
        public virtual System.Void Dispose() { }
        // RVA: 0x041E1670  token: 0x6000211
        public virtual System.Void Active() { }
        // RVA: 0x041E1670  token: 0x6000212
        public virtual System.Void Inactive() { }
        // RVA: 0x09EE8890  token: 0x6000214
        public MagicaCloth.MagicaAvatarParts GetAvatarParts(System.Int32 index) { }
        // RVA: 0x09EE8650  token: 0x6000215
        private System.Void CreateBoneDict() { }
        // RVA: 0x09EE87F8  token: 0x6000216
        private System.Void CreateColliderList() { }
        // RVA: 0x09EE88E4  token: 0x6000217
        public System.Int32 GetColliderCount() { }
        // RVA: 0x09EE8474  token: 0x6000218
        public System.Collections.Generic.List<UnityEngine.Transform> CheckOverlappingTransform() { }
        // RVA: 0x09EE752C  token: 0x6000219
        public System.Int32 AddAvatarParts(MagicaCloth.MagicaAvatarParts parts) { }
        // RVA: 0x09EE81A8  token: 0x600021A
        private System.Void AddBone(UnityEngine.Transform root, UnityEngine.Transform croot, UnityEngine.Transform bone) { }
        // RVA: 0x09EE9310  token: 0x600021B
        private System.Void ReplaceSkinMeshRenderer(UnityEngine.SkinnedMeshRenderer skinRenderer, System.Collections.Generic.Dictionary<UnityEngine.Transform,UnityEngine.Transform> boneReplaceDict) { }
        // RVA: 0x09EE92BC  token: 0x600021C
        private System.Void ReplaceMagicaComponent(MagicaCloth.CoreComponent comp, System.Collections.Generic.Dictionary<UnityEngine.Transform,UnityEngine.Transform> boneReplaceDict) { }
        // RVA: 0x09EE8950  token: 0x600021D
        public System.Void RemoveAvatarParts(MagicaCloth.MagicaAvatarParts parts) { }
        // RVA: 0x09EE91E0  token: 0x600021E
        public System.Void RemoveAvatarParts(System.Int32 partsId) { }
        // RVA: 0x09EE9404  token: 0x600021F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000037  // size: 0x28
    public class BoneClothTarget : MagicaCloth.IDataHash, MagicaCloth.IBoneReplace
    {
        // Fields
        private System.Collections.Generic.List<UnityEngine.Transform> rootList;  // 0x10
        private MagicaCloth.BoneClothTarget.ConnectionMode connection;  // 0x18
        private System.Single sameSurfaceAngle;  // 0x1c
        private System.Int32[] parentIndexList;  // 0x20

        // Properties
        System.Int32 RootCount { get; /* RVA: 0x09ED893C */ }
        MagicaCloth.BoneClothTarget.ConnectionMode Connection { get; /* RVA: 0x011EF5B0 */ }
        System.Single SameSurfaceAngle { get; /* RVA: 0x04D868F0 */ }
        System.Boolean IsMeshConnection { get; /* RVA: 0x09ED8924 */ }

        // Methods
        // RVA: 0x09ED85F8  token: 0x6000222
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x09ED8688  token: 0x6000224
        public UnityEngine.Transform GetRoot(System.Int32 index) { }
        // RVA: 0x09ED8634  token: 0x6000225
        public System.Int32 GetRootIndex(UnityEngine.Transform root) { }
        // RVA: 0x09ED81D4  token: 0x6000226
        public System.Void AddParentTransform() { }
        // RVA: 0x09ED8768  token: 0x6000227
        public System.Void RemoveParentTransform() { }
        // RVA: 0x09ED881C  token: 0x6000228
        public System.Void ResetFuturePredictionParentTransform() { }
        // RVA: 0x09ED8538  token: 0x6000229
        public System.Void ChangeUnityPhysicsCount(System.Boolean sw) { }
        // RVA: -1  // generic def  token: 0x600022D
        public virtual System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict) { }
        // RVA: 0x09ED86F4  token: 0x600022E
        public virtual System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones() { }
        // RVA: 0x09ED88CC  token: 0x600022F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000039  // size: 0x1A8
    public class ClothData : MagicaCloth.ShareDataObject
    {
        // Fields
        private static System.Int32 DATA_VERSION;  // const
        public static System.UInt32 VertexFlag_End;  // const
        public static System.UInt32 VertexFlag_TriangleRotation;  // const
        public MagicaCloth.ClothParams.Algorithm triangleBendAlgorithm;  // 0x20
        public MagicaCloth.ClothParams.Algorithm restoreRotationAlgorithm;  // 0x24
        public MagicaCloth.ClothParams.Algorithm clampRotationAlgorithm;  // 0x28
        public System.Collections.Generic.List<System.Int32> useVertexList;  // 0x30
        public System.Collections.Generic.List<System.Int32> selectionData;  // 0x38
        public System.UInt32 maxLevel;  // 0x40
        public System.Collections.Generic.List<System.UInt32> vertexFlagLevelList;  // 0x48
        public System.Collections.Generic.List<System.Single> vertexDepthList;  // 0x50
        public System.Collections.Generic.List<System.Int32> rootList;  // 0x58
        public System.Collections.Generic.List<System.Int32> parentList;  // 0x60
        public MagicaCloth.RestoreDistanceConstraint.RestoreDistanceData[] structDistanceDataList;  // 0x68
        public MagicaCloth.ReferenceDataIndex[] structDistanceReferenceList;  // 0x70
        public MagicaCloth.RestoreDistanceConstraint.RestoreDistanceData[] bendDistanceDataList;  // 0x78
        public MagicaCloth.ReferenceDataIndex[] bendDistanceReferenceList;  // 0x80
        public MagicaCloth.RestoreDistanceConstraint.RestoreDistanceData[] nearDistanceDataList;  // 0x88
        public MagicaCloth.ReferenceDataIndex[] nearDistanceReferenceList;  // 0x90
        public MagicaCloth.ClampDistanceConstraint.ClampDistanceData[] rootDistanceDataList;  // 0x98
        public MagicaCloth.ReferenceDataIndex[] rootDistanceReferenceList;  // 0xa0
        public MagicaCloth.ClampDistance2Constraint.ClampDistance2Data[] clampDistance2DataList;  // 0xa8
        public MagicaCloth.ClampDistance2Constraint.ClampDistance2RootInfo[] clampDistance2RootInfoList;  // 0xb0
        public MagicaCloth.RestoreRotationConstraint.RotationData[] restoreRotationDataList;  // 0xb8
        public MagicaCloth.ReferenceDataIndex[] restoreRotationReferenceList;  // 0xc0
        public MagicaCloth.ClampRotationConstraint.ClampRotationData[] clampRotationDataList;  // 0xc8
        public MagicaCloth.ClampRotationConstraint.ClampRotationRootInfo[] clampRotationRootInfoList;  // 0xd0
        public MagicaCloth.CompositeRotationConstraint.RotationData[] compositeRotationDataList;  // 0xd8
        public MagicaCloth.CompositeRotationConstraint.RootInfo[] compositeRotationRootInfoList;  // 0xe0
        public MagicaCloth.TwistConstraint.TwistData[] twistDataList;  // 0xe8
        public MagicaCloth.ReferenceDataIndex[] twistReferenceList;  // 0xf0
        public MagicaCloth.AdjustRotationWorker.AdjustRotationData[] adjustRotationDataList;  // 0xf8
        public MagicaCloth.TriangleBendConstraint.TriangleBendData[] triangleBendDataList;  // 0x100
        public MagicaCloth.ReferenceDataIndex[] triangleBendReferenceList;  // 0x108
        public System.Int32 triangleBendWriteBufferCount;  // 0x110
        public MagicaCloth.VolumeConstraint.VolumeData[] volumeDataList;  // 0x118
        public MagicaCloth.ReferenceDataIndex[] volumeReferenceList;  // 0x120
        public System.Int32 volumeWriteBufferCount;  // 0x128
        public MagicaCloth.LineWorker.LineRotationData[] lineRotationDataList;  // 0x130
        public MagicaCloth.LineWorker.LineRotationRootInfo[] lineRotationRootInfoList;  // 0x138
        public MagicaCloth.TriangleWorker.TriangleRotationData[] triangleRotationDataList;  // 0x140
        public System.Int32[] triangleRotationIndexList;  // 0x148
        public MagicaCloth.EdgeCollisionConstraint.EdgeCollisionData[] edgeCollisionDataList;  // 0x150
        public MagicaCloth.ReferenceDataIndex[] edgeCollisionReferenceList;  // 0x158
        public System.Int32 edgeCollisionWriteBufferCount;  // 0x160
        public MagicaCloth.PenetrationConstraint.PenetrationData[] penetrationDataList;  // 0x168
        public MagicaCloth.ReferenceDataIndex[] penetrationReferenceList;  // 0x170
        public Unity.Mathematics.float3[] penetrationDirectionDataList;  // 0x178
        public MagicaCloth.ClothParams.PenetrationMode penetrationMode;  // 0x180
        public MagicaCloth.BaseSkinningWorker.BaseSkinningData[] baseSkinningDataList;  // 0x188
        public Unity.Mathematics.float4x4[] baseSkinningBindPoseList;  // 0x190
        public UnityEngine.Vector3 initScale;  // 0x198

        // Properties
        System.Int32 VertexUseCount { get; /* RVA: 0x09EE45A0 */ }
        System.Int32 StructDistanceConstraintCount { get; /* RVA: 0x09EE4544 */ }
        System.Int32 BendDistanceConstraintCount { get; /* RVA: 0x09EE4420 */ }
        System.Int32 NearDistanceConstraintCount { get; /* RVA: 0x09EE44DC */ }
        System.Int32 ClampDistanceConstraintCount { get; /* RVA: 0x09EE444C */ }
        System.Int32 ClampDistance2ConstraintCount { get; /* RVA: 0x09EE4434 */ }
        System.Int32 RestoreRotationConstraintCount { get; /* RVA: 0x09EE452C */ }
        System.Int32 ClampRotationConstraintDataCount { get; /* RVA: 0x09EE4464 */ }
        System.Int32 ClampRotationConstraintRootCount { get; /* RVA: 0x09EE447C */ }
        System.Int32 AdjustRotationConstraintCount { get; /* RVA: 0x09EE43F0 */ }
        System.Int32 CompositeRotationCount { get; /* RVA: 0x09EE4494 */ }
        System.Int32 TwistConstraintCount { get; /* RVA: 0x09EE4588 */ }
        System.Int32 TriangleBendConstraintCount { get; /* RVA: 0x09EE4558 */ }
        System.Int32 EdgeCollisionConstraintCount { get; /* RVA: 0x09EE44AC */ }
        System.Int32 VolumeConstraintCount { get; /* RVA: 0x09EE45BC */ }
        System.Int32 LineRotationWorkerCount { get; /* RVA: 0x09EE44C4 */ }
        System.Int32 TriangleRotationWorkerCount { get; /* RVA: 0x09EE4570 */ }
        System.Int32 PenetrationCount { get; /* RVA: 0x09EE44F4 */ }
        System.Int32 BaseSkinningCount { get; /* RVA: 0x09EE4408 */ }

        // Methods
        // RVA: 0x09EE1C58  token: 0x6000230
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x09EE1C1C  token: 0x6000244
        public System.Int32 GetClampRotationCount() { }
        // RVA: 0x09EE2278  token: 0x6000245
        public System.Int32 GetRestoreRotationCount() { }
        // RVA: 0x09EE39B4  token: 0x6000246
        public System.Boolean IsInvalidVertex(System.Int32 vindex) { }
        // RVA: 0x09EE3958  token: 0x6000247
        public System.Boolean IsFixedVertex(System.Int32 vindex) { }
        // RVA: 0x09EE3A1C  token: 0x6000248
        public System.Boolean IsMoveVertex(System.Int32 vindex) { }
        // RVA: 0x09EE392C  token: 0x6000249
        public System.Boolean IsExtendVertex(System.Int32 vindex) { }
        // RVA: 0x09EE3A0C  token: 0x600024A
        public System.Boolean IsLastLevel(System.Int32 vindex) { }
        // RVA: 0x09EE3984  token: 0x600024B
        public System.Boolean IsFlag(System.Int32 vindex, System.UInt32 flag) { }
        // RVA: 0x09EE3FEC  token: 0x600024C
        public System.Void SetFlag(System.Int32 vindex, System.UInt32 flag) { }
        // RVA: 0x09EE2108  token: 0x600024D
        public System.Int32 GetLevel(System.Int32 vindex) { }
        // RVA: 0x04D9EC50  token: 0x600024E
        public virtual System.Int32 GetVersion() { }
        // RVA: 0x09EE41F0  token: 0x600024F
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: 0x09EE12FC  token: 0x6000250
        public System.Void CreateData(MagicaCloth.PhysicsTeam team, MagicaCloth.ClothParams clothParams, MagicaCloth.PhysicsTeamData teamData, MagicaCloth.MeshData meshData, MagicaCloth.IEditorMesh editMesh, System.Collections.Generic.List<System.Int32> selData, System.Action<System.Collections.Generic.List<System.Int32>,System.Collections.Generic.List<System.Int32>,System.Collections.Generic.List<UnityEngine.Vector3>,System.Collections.Generic.List<UnityEngine.Vector3>,System.Collections.Generic.List<UnityEngine.Vector3>,System.Collections.Generic.List<System.Int32>,System.Collections.Generic.List<System.Int32>> extensionAction) { }
        // RVA: 0x09EE1544  token: 0x6000251
        private System.Void CreateVertexData(System.Int32 vertexCount, System.Collections.Generic.List<System.Int32> lineList, System.Collections.Generic.List<System.Int32> triangleList) { }
        // RVA: 0x09ED9088  token: 0x6000252
        private System.Void CreateConstraintData(MagicaCloth.PhysicsTeam team, MagicaCloth.ClothParams clothParams, MagicaCloth.PhysicsTeamData teamData, System.Int32 vertexCount, System.Collections.Generic.List<UnityEngine.Vector3> wposList, System.Collections.Generic.List<UnityEngine.Vector3> wnorList, System.Collections.Generic.List<UnityEngine.Vector3> wtanList, System.Collections.Generic.List<System.Int32> lineList, System.Collections.Generic.List<System.Int32> triangleList) { }
        // RVA: 0x09EE3A78  token: 0x6000253
        private System.Void RegistTriangleBend(System.Int32 v0, System.Int32 v1, System.Int32 v2, System.Int32 v3, System.Collections.Generic.List<UnityEngine.Vector3> wposList, MagicaCloth.ClothParams clothParams, System.Collections.Generic.List<MagicaCloth.TriangleBendConstraint.TriangleBendData> triangleBendData) { }
        // RVA: 0x09EE4070  token: 0x6000254
        private System.Collections.Generic.List<System.Int32> SortTetra(System.Int32 v0, System.Int32 v1, System.Int32 v2, System.Int32 v3, System.Collections.Generic.List<System.Single> meshVertexDepthList) { }
        // RVA: 0x09ED8C44  token: 0x6000255
        private System.Collections.Generic.List<System.Int32> CheckTetraDirection(System.Int32 v0, System.Int32 v1, System.Int32 v2, System.Int32 v3, System.Collections.Generic.HashSet<System.UInt64> trianglePackSet, System.Collections.Generic.List<System.Single> meshVertexDepthList) { }
        // RVA: 0x09ED897C  token: 0x6000256
        private System.Boolean CalcTriangleBendRestAngle(UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, UnityEngine.Vector3 p2, UnityEngine.Vector3 p3, System.Single& restAngle) { }
        // RVA: 0x09EE2160  token: 0x6000257
        private System.Collections.Generic.List<System.Single> GetMeshVertexDepthList(System.Int32 vertexCount, System.Collections.Generic.List<System.Single> depthList) { }
        // RVA: 0x09EE22B4  token: 0x6000258
        private System.Collections.Generic.List<System.Int32> GetUseParentVertexList(System.Int32 vertexCount, System.Collections.Generic.List<System.Collections.Generic.HashSet<System.Int32>> vlink, System.Collections.Generic.List<UnityEngine.Vector3> wposList, System.Collections.Generic.List<System.Single> depthList) { }
        // RVA: 0x09EE364C  token: 0x6000259
        private System.Collections.Generic.List<System.Int32> GetUseRootVertexList(System.Collections.Generic.List<System.Int32> parentVertexList) { }
        // RVA: 0x09EE3748  token: 0x600025A
        private System.Collections.Generic.List<MagicaCloth.ClothData.VertexInfo> GetUseVertexInfoList(System.Collections.Generic.List<System.Int32> parentVertexList) { }
        // RVA: 0x09EE314C  token: 0x600025B
        private System.Collections.Generic.List<System.Collections.Generic.List<System.Int32>> GetUseRootLineList(System.Collections.Generic.List<System.Int32> parentVertexList) { }
        // RVA: 0x09EE4298  token: 0x600025C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000042  // size: 0x228
    public class ClothParams
    {
        // Fields
        private MagicaCloth.ClothParams.Algorithm algorithm;  // 0x10
        private MagicaCloth.BezierParam radius;  // 0x18
        private MagicaCloth.BezierParam mass;  // 0x20
        private System.Boolean useGravity;  // 0x28
        private MagicaCloth.BezierParam gravity;  // 0x30
        private UnityEngine.Vector3 gravityDirection;  // 0x38
        private System.Boolean useDrag;  // 0x44
        private MagicaCloth.BezierParam drag;  // 0x48
        private System.Boolean useMaxVelocity;  // 0x50
        private MagicaCloth.BezierParam maxVelocity;  // 0x58
        private UnityEngine.Transform influenceTarget;  // 0x60
        private System.Single maxMoveSpeed;  // 0x68
        private System.Single maxRotationSpeed;  // 0x6c
        private MagicaCloth.BezierParam worldMoveInfluence;  // 0x70
        private MagicaCloth.BezierParam worldRotationInfluence;  // 0x78
        private System.Single massInfluence;  // 0x80
        private MagicaCloth.BezierParam depthInfluence;  // 0x88
        private System.Single windInfluence;  // 0x90
        private System.Single windRandomScale;  // 0x94
        private System.Single windSynchronization;  // 0x98
        private System.Boolean useDistanceDisable;  // 0x9c
        private UnityEngine.Transform disableReferenceObject;  // 0xa0
        private System.Single disableDistance;  // 0xa8
        private System.Single disableFadeDistance;  // 0xac
        private System.Boolean useResetTeleport;  // 0xb0
        private System.Single teleportDistance;  // 0xb4
        private System.Single teleportRotation;  // 0xb8
        private MagicaCloth.ClothParams.TeleportMode teleportMode;  // 0xbc
        private System.Single resetStabilizationTime;  // 0xc0
        private System.Boolean useClampDistanceRatio;  // 0xc4
        private System.Single clampDistanceMinRatio;  // 0xc8
        private System.Single clampDistanceMaxRatio;  // 0xcc
        private System.Single clampDistanceVelocityInfluence;  // 0xd0
        private System.Boolean useClampPositionLength;  // 0xd4
        private MagicaCloth.BezierParam clampPositionLength;  // 0xd8
        private System.Single clampPositionRatioX;  // 0xe0
        private System.Single clampPositionRatioY;  // 0xe4
        private System.Single clampPositionRatioZ;  // 0xe8
        private System.Single clampPositionVelocityInfluence;  // 0xec
        private System.Boolean useClampRotation;  // 0xf0
        private MagicaCloth.BezierParam clampRotationAngle;  // 0xf8
        private MagicaCloth.BezierParam clampRotationAngle2;  // 0x100
        private System.Single clampRotationVelocityLimit;  // 0x108
        private System.Single clampRotationVelocityInfluence;  // 0x10c
        private System.Single restoreDistanceVelocityInfluence;  // 0x110
        private MagicaCloth.BezierParam structDistanceStiffness;  // 0x118
        private System.Boolean useBendDistance;  // 0x120
        private System.Int32 bendDistanceMaxCount;  // 0x124
        private MagicaCloth.BezierParam bendDistanceStiffness;  // 0x128
        private System.Boolean useNearDistance;  // 0x130
        private System.Int32 nearDistanceMaxCount;  // 0x134
        private System.Single nearDistanceMaxDepth;  // 0x138
        private MagicaCloth.BezierParam nearDistanceLength;  // 0x140
        private MagicaCloth.BezierParam nearDistanceStiffness;  // 0x148
        private System.Boolean useRestoreRotation;  // 0x150
        private MagicaCloth.BezierParam restoreRotation;  // 0x158
        private MagicaCloth.BezierParam restoreRotation2;  // 0x160
        private System.Single restoreRotationVelocityInfluence;  // 0x168
        private System.Single restoreRotationVelocityInfluence2;  // 0x16c
        private System.Boolean useSpring;  // 0x170
        private System.Single springPower;  // 0x174
        private System.Single springRadius;  // 0x178
        private System.Single springScaleX;  // 0x17c
        private System.Single springScaleY;  // 0x180
        private System.Single springScaleZ;  // 0x184
        private System.Single springIntensity;  // 0x188
        private MagicaCloth.BezierParam springDirectionAtten;  // 0x190
        private MagicaCloth.BezierParam springDistanceAtten;  // 0x198
        private MagicaCloth.ClothParams.AdjustMode adjustMode;  // 0x1a0
        private System.Single adjustRotationPower;  // 0x1a4
        private System.Boolean useTriangleBend;  // 0x1a8
        private MagicaCloth.BezierParam triangleBend;  // 0x1b0
        private MagicaCloth.BezierParam triangleBend2;  // 0x1b8
        private System.Boolean useTwistCorrection;  // 0x1c0
        private System.Single twistRecoveryPower;  // 0x1c4
        private System.Boolean useVolume;  // 0x1c8
        private System.Single maxVolumeLength;  // 0x1cc
        private MagicaCloth.BezierParam volumeStretchStiffness;  // 0x1d0
        private MagicaCloth.BezierParam volumeShearStiffness;  // 0x1d8
        private System.Boolean useCollision;  // 0x1e0
        private System.Single friction;  // 0x1e4
        private System.Single staticFriction;  // 0x1e8
        private System.Boolean usePenetration;  // 0x1ec
        private MagicaCloth.ClothParams.PenetrationMode penetrationMode;  // 0x1f0
        private MagicaCloth.ClothParams.PenetrationAxis penetrationAxis;  // 0x1f4
        private System.Single penetrationMaxDepth;  // 0x1f8
        private MagicaCloth.BezierParam penetrationConnectDistance;  // 0x200
        private MagicaCloth.BezierParam penetrationDistance;  // 0x208
        private MagicaCloth.BezierParam penetrationRadius;  // 0x210
        private System.Boolean useLineAvarageRotation;  // 0x218
        private System.Boolean useFixedNonRotation;  // 0x219
        private System.Collections.Generic.HashSet<MagicaCloth.ClothParams.ParamType> changeSet;  // 0x220

        // Properties
        MagicaCloth.ClothParams.Algorithm AlgorithmType { get; /* RVA: 0x02B2E2D0 */ set; /* RVA: 0x014F51F0 */ }
        System.Boolean UseGravity { get; /* RVA: 0x011F33C0 */ }
        UnityEngine.Vector3 GravityDirection { get; /* RVA: 0x04D9E700 */ set; /* RVA: 0x04D9DB00 */ }
        System.Boolean UseDrag { get; /* RVA: 0x04D86A30 */ }
        System.Boolean UseMaxVelocity { get; /* RVA: 0x04D86800 */ }
        System.Single MassInfluence { get; /* RVA: 0x04D88E60 */ set; /* RVA: 0x04D8E240 */ }
        System.Single WindInfluence { get; /* RVA: 0x04D8FD30 */ set; /* RVA: 0x04D8FD60 */ }
        System.Single WindRandomScale { get; /* RVA: 0x04D8F070 */ set; /* RVA: 0x04D8F0A0 */ }
        System.Single WindSynchronization { get; /* RVA: 0x04D8E570 */ set; /* RVA: 0x04D8E590 */ }
        System.Single MaxMoveSpeed { get; /* RVA: 0x04D8F060 */ set; /* RVA: 0x04D8F090 */ }
        System.Single MaxRotationSpeed { get; /* RVA: 0x04DA3230 */ set; /* RVA: 0x04DA3330 */ }
        System.Boolean UseResetTeleport { get; /* RVA: 0x04D88CC0 */ set; /* RVA: 0x04D88D20 */ }
        System.Single TeleportDistance { get; /* RVA: 0x04DA32B0 */ set; /* RVA: 0x04DA3350 */ }
        System.Single TeleportRotation { get; /* RVA: 0x04DA32D0 */ set; /* RVA: 0x04D87E30 */ }
        MagicaCloth.ClothParams.TeleportMode TeleportResetMode { get; /* RVA: 0x04DA32C0 */ set; /* RVA: 0x04DA3360 */ }
        System.Single ResetStabilizationTime { get; /* RVA: 0x04D9DF30 */ set; /* RVA: 0x04D9DFF0 */ }
        System.Boolean UseDistanceDisable { get; /* RVA: 0x04D92ED0 */ set; /* RVA: 0x04D92F60 */ }
        UnityEngine.Transform DisableReferenceObject { get; /* RVA: 0x04D86210 */ set; /* RVA: 0x05391334 */ }
        System.Single DisableDistance { get; /* RVA: 0x04D8E650 */ set; /* RVA: 0x04D8E680 */ }
        System.Single DisableFadeDistance { get; /* RVA: 0x04D8E660 */ set; /* RVA: 0x04D8E690 */ }
        System.Boolean UseClampDistanceRatio { get; /* RVA: 0x04DA32F0 */ }
        System.Single ClampDistanceMinRatio { get; /* RVA: 0x09EE5BEC */ }
        System.Single ClampDistanceMaxRatio { get; /* RVA: 0x09EE5BD4 */ }
        System.Single ClampDistanceVelocityInfluence { get; /* RVA: 0x09EE5C04 */ }
        System.Boolean UseClampPositionLength { get; /* RVA: 0x04DA2990 */ }
        UnityEngine.Vector3 ClampPositionAxisRatio { get; /* RVA: 0x04DA3210 */ }
        System.Single ClampPositionVelocityInfluence { get; /* RVA: 0x09EE5C20 */ }
        System.Boolean UseClampRotation { get; /* RVA: 0x04D8E520 */ }
        System.Single ClampRotationVelocityInfluence { get; /* RVA: 0x09EE5C3C */ }
        System.Single RestoreDistanceVelocityInfluence { get; /* RVA: 0x04D8E170 */ }
        System.Boolean UseBendDistance { get; /* RVA: 0x04D8FE70 */ }
        System.Int32 BendDistanceMaxCount { get; /* RVA: 0x04D90A80 */ }
        System.Boolean UseNearDistance { get; /* RVA: 0x04D92620 */ }
        System.Int32 NearDistanceMaxCount { get; /* RVA: 0x04DA3240 */ }
        System.Single NearDistanceMaxDepth { get; /* RVA: 0x04DA3250 */ }
        System.Boolean UseRestoreRotation { get; /* RVA: 0x04DA2F60 */ }
        System.Boolean UseSpring { get; /* RVA: 0x04D905A0 */ set; /* RVA: 0x04D905B0 */ }
        System.Single SpringPowr { get; /* RVA: 0x04DA3270 */ set; /* RVA: 0x04DA3340 */ }
        System.Single SpringRadius { get; /* RVA: 0x04D902D0 */ }
        UnityEngine.Vector3 SpringRadiusScale { get; /* RVA: 0x04DA3280 */ }
        System.Single SpringIntensity { get; /* RVA: 0x04DA3260 */ }
        MagicaCloth.ClothParams.AdjustMode AdjustRotationMode { get; /* RVA: 0x04DA3200 */ }
        UnityEngine.Vector3 AdjustRotationVector { get; /* RVA: 0x09EE5B80 */ }
        System.Boolean UseTriangleBend { get; /* RVA: 0x04DA3310 */ }
        System.Single TwistRecoveryPower { get; /* RVA: 0x04DA32E0 */ }
        System.Boolean UseVolume { get; /* RVA: 0x04DA3320 */ }
        System.Boolean UseCollision { get; /* RVA: 0x04D91ED0 */ }
        System.Single DynamicFriction { get; /* RVA: 0x04D90D10 */ }
        System.Single StaticFriction { get; /* RVA: 0x04DA32A0 */ }
        System.Boolean UsePenetration { get; /* RVA: 0x04D90680 */ set; /* RVA: 0x04D90690 */ }
        System.Single PenetrationMaxDepth { get; /* RVA: 0x04D911D0 */ }
        System.Boolean UseLineAvarageRotation { get; /* RVA: 0x04D8EDA0 */ }
        System.Boolean UseFixedNonRotation { get; /* RVA: 0x04DA3300 */ }

        // Methods
        // RVA: 0x09EE4F08  token: 0x600026C
        public System.Void SetChangeParam(MagicaCloth.ClothParams.ParamType ptype) { }
        // RVA: 0x09EE45D4  token: 0x600026D
        public System.Boolean ChangedParam(MagicaCloth.ClothParams.ParamType ptype) { }
        // RVA: 0x09EE462C  token: 0x600026E
        public System.Void ClearChangeParam() { }
        // RVA: 0x09EE4820  token: 0x600026F
        public System.Int32 GetParamHash(MagicaCloth.BaseCloth cloth, MagicaCloth.ClothParams.ParamType ptype) { }
        // RVA: 0x09EE514C  token: 0x6000272
        public System.Void SetRadius(System.Single sval, System.Single eval) { }
        // RVA: 0x09EE4E30  token: 0x6000273
        public System.Single GetRadius(System.Single depth) { }
        // RVA: 0x0385B100  token: 0x6000274
        public MagicaCloth.BezierParam GetRadius() { }
        // RVA: 0x09EE50DC  token: 0x6000275
        public System.Void SetMass(System.Single sval, System.Single eval, System.Boolean useEval, System.Single cval, System.Boolean useCval) { }
        // RVA: 0x04D862C0  token: 0x6000276
        public MagicaCloth.BezierParam GetMass() { }
        // RVA: 0x09EE50A8  token: 0x6000277
        public System.Void SetGravity(System.Boolean sw, System.Single sval, System.Single eval) { }
        // RVA: 0x09EE4784  token: 0x6000279
        public MagicaCloth.BezierParam GetGravity() { }
        // RVA: 0x09EE5074  token: 0x600027C
        public System.Void SetDrag(System.Boolean sw, System.Single sval, System.Single eval) { }
        // RVA: 0x09EE4744  token: 0x600027E
        public MagicaCloth.BezierParam GetDrag() { }
        // RVA: 0x09EE5118  token: 0x600027F
        public System.Void SetMaxVelocity(System.Boolean sw, System.Single sval, System.Single eval) { }
        // RVA: 0x09EE47C4  token: 0x6000281
        public MagicaCloth.BezierParam GetMaxVelocity() { }
        // RVA: 0x04DA3150  token: 0x6000282
        public System.Void SetExternalForce(System.Single massInfluence, System.Single windInfluence, System.Single windRandomScale, System.Single windSynchronization) { }
        // RVA: 0x04D861F0  token: 0x6000285
        public MagicaCloth.BezierParam GetDepthInfluence() { }
        // RVA: 0x09EE5350  token: 0x600028C
        public System.Void SetWorldInfluence(System.Single maxspeed, System.Single moveval, System.Single rotval) { }
        // RVA: 0x04D862B0  token: 0x600028D
        public MagicaCloth.BezierParam GetWorldMoveInfluence() { }
        // RVA: 0x04D862A0  token: 0x600028E
        public MagicaCloth.BezierParam GetWorldRotationInfluence() { }
        // RVA: 0x04D86280  token: 0x600028F
        public UnityEngine.Transform GetInfluenceTarget() { }
        // RVA: 0x063CC164  token: 0x6000290
        public System.Void SetInfluenceTarget(UnityEngine.Transform t) { }
        // RVA: 0x04DA31D0  token: 0x6000295
        public System.Void SetTeleport(System.Boolean sw, System.Single distance, System.Single rotation, MagicaCloth.ClothParams.TeleportMode mode) { }
        // RVA: 0x09EE5038  token: 0x60002A0
        public System.Void SetDistanceDisable(System.Boolean sw, System.Single distance, System.Single fadeDistance, UnityEngine.Transform referenceObject) { }
        // RVA: 0x04DA3100  token: 0x60002A9
        public System.Void SetClampDistanceRatio(System.Boolean sw, System.Single minval, System.Single maxval, System.Single influence) { }
        // RVA: 0x09EE4F60  token: 0x60002AE
        public System.Void SetClampPositionLength(System.Boolean sw, System.Single sval, System.Single eval, System.Single ratioX, System.Single ratioY, System.Single ratioZ, System.Single influence) { }
        // RVA: 0x04D88EF0  token: 0x60002B1
        public MagicaCloth.BezierParam GetClampPositionLength() { }
        // RVA: 0x09EE4FD0  token: 0x60002B3
        public System.Void SetClampRotationAngle(System.Boolean sw, System.Single sval, System.Single eval, System.Single influence) { }
        // RVA: 0x09EE4708  token: 0x60002B5
        public MagicaCloth.BezierParam GetClampRotationAngle(MagicaCloth.ClothParams.Algorithm algo) { }
        // RVA: 0x09EE4720  token: 0x60002B7
        public System.Single GetClampRotationVelocityLimit(MagicaCloth.ClothParams.Algorithm algo) { }
        // RVA: 0x09EE5180  token: 0x60002B8
        public System.Void SetRestoreDistance(System.Single influence, System.Single structStiffness) { }
        // RVA: 0x04D85F80  token: 0x60002BA
        public MagicaCloth.BezierParam GetStructDistanceStiffness() { }
        // RVA: 0x04D8D910  token: 0x60002BD
        public MagicaCloth.BezierParam GetBendDistanceStiffness() { }
        // RVA: 0x04D8DA60  token: 0x60002C1
        public MagicaCloth.BezierParam GetNearDistanceLength() { }
        // RVA: 0x04D8DAC0  token: 0x60002C2
        public MagicaCloth.BezierParam GetNearDistanceStiffness() { }
        // RVA: 0x09EE51BC  token: 0x60002C3
        public System.Void SetRestoreRotation(System.Boolean sw, System.Single sval, System.Single eval, System.Single influence) { }
        // RVA: 0x09EE4E50  token: 0x60002C5
        public MagicaCloth.BezierParam GetRestoreRotationPower(MagicaCloth.ClothParams.Algorithm algo) { }
        // RVA: 0x09EE4E68  token: 0x60002C6
        public System.Single GetRestoreRotationVelocityInfluence(MagicaCloth.ClothParams.Algorithm algo) { }
        // RVA: 0x04DA3180  token: 0x60002C7
        public System.Void SetSpring(System.Boolean sw, System.Single power, System.Single r, System.Single sclx, System.Single scly, System.Single sclz, System.Single intensity) { }
        // RVA: 0x09EE522C  token: 0x60002C8
        public System.Void SetSpringDirectionAtten(System.Single sval, System.Single eval, System.Single cval) { }
        // RVA: 0x09EE5260  token: 0x60002C9
        public System.Void SetSpringDistanceAtten(System.Single sval, System.Single eval, System.Single cval) { }
        // RVA: 0x09EE4EC8  token: 0x60002CC
        public System.Single GetSpringPower() { }
        // RVA: 0x09EE4E80  token: 0x60002D2
        public System.Single GetSpringDirectionAtten(System.Single ratio) { }
        // RVA: 0x09EE4EA4  token: 0x60002D3
        public System.Single GetSpringDistanceAtten(System.Single ratio) { }
        // RVA: 0x04DA30F0  token: 0x60002D4
        public System.Void SetAdjustRotation(MagicaCloth.ClothParams.AdjustMode amode, System.Single power) { }
        // RVA: 0x09EE5294  token: 0x60002D7
        public System.Void SetTriangleBend(System.Boolean sw, System.Single sval, System.Single eval) { }
        // RVA: 0x09EE4EE0  token: 0x60002D9
        public MagicaCloth.BezierParam GetTriangleBendStiffness(MagicaCloth.ClothParams.Algorithm algo) { }
        // RVA: 0x09EE4EF8  token: 0x60002DA
        private System.Boolean GetUseTwistCorrection(MagicaCloth.ClothParams.Algorithm algo) { }
        // RVA: 0x09EE52EC  token: 0x60002DC
        public System.Void SetVolume(System.Boolean sw, System.Single maxLength, System.Single stiffness, System.Single shear) { }
        // RVA: 0x09EE4808  token: 0x60002DE
        public System.Single GetMaxVolumeLength() { }
        // RVA: 0x04D8D7B0  token: 0x60002DF
        public MagicaCloth.BezierParam GetVolumeStretchStiffness() { }
        // RVA: 0x04D87870  token: 0x60002E0
        public MagicaCloth.BezierParam GetVolumeShearStiffness() { }
        // RVA: 0x04DA3130  token: 0x60002E1
        public System.Void SetCollision(System.Boolean sw, System.Single dynamicFriction, System.Single staticFriction) { }
        // RVA: 0x04DA23C0  token: 0x60002E7
        public MagicaCloth.ClothParams.PenetrationMode GetPenetrationMode() { }
        // RVA: 0x04DA30E0  token: 0x60002E8
        public MagicaCloth.ClothParams.PenetrationAxis GetPenetrationAxis() { }
        // RVA: 0x04D8DA40  token: 0x60002EA
        public MagicaCloth.BezierParam GetPenetrationConnectDistance() { }
        // RVA: 0x04D8D8B0  token: 0x60002EB
        public MagicaCloth.BezierParam GetPenetrationRadius() { }
        // RVA: 0x04D8D8C0  token: 0x60002EC
        public MagicaCloth.BezierParam GetPenetrationDistance() { }
        // RVA: 0x09EE4674  token: 0x60002EF
        public System.Void ConvertToLatestAlgorithmParameter() { }
        // RVA: 0x09EE53A0  token: 0x60002F0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000049  // size: 0x18
    public class ClothSetup
    {
        // Fields
        private System.Int32 teamBoneIndex;  // 0x10
        private System.Single distanceBlendRatio;  // 0x14

        // Properties
        System.Single DistanceBlendRatio { get; /* RVA: 0x04D88D40 */ set; /* RVA: 0x04D88D50 */ }

        // Methods
        // RVA: 0x09EECE30  token: 0x60002F1
        public System.Void ClothInit(MagicaCloth.PhysicsTeam team, MagicaCloth.MeshData meshData, MagicaCloth.ClothData clothData, MagicaCloth.ClothParams param, System.Func<System.Int32,System.UInt32> funcUserFlag) { }
        // RVA: 0x09EECCE4  token: 0x60002F2
        public System.Void ClothDispose(MagicaCloth.PhysicsTeam team) { }
        // RVA: 0x09EECBBC  token: 0x60002F3
        public System.Void ClothActive(MagicaCloth.PhysicsTeam team, MagicaCloth.ClothParams param, MagicaCloth.ClothData clothData) { }
        // RVA: 0x09EECD48  token: 0x60002F4
        public System.Void ClothInactive(MagicaCloth.PhysicsTeam team) { }
        // RVA: -1  // generic def  token: 0x60002F5
        private System.Void ReplaceBone(MagicaCloth.PhysicsTeam team, MagicaCloth.ClothParams param, System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict) { }
        // RVA: 0x09EEED7C  token: 0x60002F6
        private System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones(MagicaCloth.PhysicsTeam team, MagicaCloth.ClothParams param) { }
        // RVA: 0x09EECB50  token: 0x60002F7
        public System.Void ChangeUseUnityPhysics(System.Boolean sw) { }
        // RVA: 0x09EEBF38  token: 0x60002FA
        public System.Void ChangeData(MagicaCloth.PhysicsTeam team, MagicaCloth.ClothParams param, MagicaCloth.ClothData clothData) { }
        // RVA: 0x04DA33F0  token: 0x60002FB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004B  // size: 0x100
    public class MagicaBoneCloth : MagicaCloth.BaseCloth
    {
        // Fields
        private static System.Int32 DATA_VERSION;  // const
        private static System.Int32 ERR_DATA_VERSION;  // const
        private MagicaCloth.MeshData meshData;  // 0xc8
        private System.Int32 meshDataHash;  // 0xd0
        private System.Int32 meshDataVersion;  // 0xd4
        private MagicaCloth.BoneClothTarget clothTarget;  // 0xd8
        private System.Collections.Generic.List<UnityEngine.Transform> useTransformList;  // 0xe0
        private System.Collections.Generic.List<UnityEngine.Vector3> useTransformPositionList;  // 0xe8
        private System.Collections.Generic.List<UnityEngine.Quaternion> useTransformRotationList;  // 0xf0
        private System.Collections.Generic.List<UnityEngine.Vector3> useTransformScaleList;  // 0xf8

        // Properties
        MagicaCloth.BoneClothTarget ClothTarget { get; /* RVA: 0x04D88EF0 */ }
        MagicaCloth.MeshData MeshData { get; /* RVA: 0x04D8D1D0 */ }
        System.Int32 UseTransformCount { get; /* RVA: 0x09EF1B1C */ }

        // Methods
        // RVA: 0x04DA3400  token: 0x6000300
        public virtual MagicaCloth.ComponentType GetComponentType() { }
        // RVA: 0x09EEF674  token: 0x6000301
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x09EF162C  token: 0x6000304
        protected virtual System.Void Reset() { }
        // RVA: 0x09EF12C0  token: 0x6000305
        protected virtual System.Void OnValidate() { }
        // RVA: 0x09EEF4B8  token: 0x6000306
        protected virtual System.Void ClothInit() { }
        // RVA: 0x09EEF494  token: 0x6000307
        protected virtual System.Void ClothDispose() { }
        // RVA: 0x09EEF48C  token: 0x6000308
        protected virtual System.Void ClothActive() { }
        // RVA: 0x09EF1634  token: 0x6000309
        protected virtual System.UInt32 UserFlag(System.Int32 index) { }
        // RVA: 0x09EF1798  token: 0x600030A
        protected virtual UnityEngine.Transform UserTransform(System.Int32 index) { }
        // RVA: 0x09EF1668  token: 0x600030B
        protected virtual Unity.Mathematics.float3 UserTransformLocalPosition(System.Int32 vindex) { }
        // RVA: 0x09EF1708  token: 0x600030C
        protected virtual Unity.Mathematics.quaternion UserTransformLocalRotation(System.Int32 vindex) { }
        // RVA: 0x011EC580  token: 0x600030D
        public virtual MagicaCloth.BaseMeshDeformer GetDeformer() { }
        // RVA: 0x04D8D1D0  token: 0x600030E
        protected virtual MagicaCloth.MeshData GetMeshData() { }
        // RVA: 0x041E1670  token: 0x600030F
        protected virtual System.Void WorkerInit() { }
        // RVA: 0x012081B0  token: 0x6000310
        public virtual System.Boolean IsRequiresDeformer() { }
        // RVA: 0x041E1670  token: 0x6000311
        protected virtual System.Void SetDeformerUseVertex(System.Boolean sw, MagicaCloth.BaseMeshDeformer deformer) { }
        // RVA: 0x09EEF444  token: 0x6000312
        protected virtual System.Void ChangeUseUnityPhysics(System.Boolean sw) { }
        // RVA: 0x09EF11E4  token: 0x6000313
        protected virtual System.Void OnChangeCalculation() { }
        // RVA: 0x09EF0E08  token: 0x6000314
        public System.Collections.Generic.List<UnityEngine.Transform> GetTransformList() { }
        // RVA: 0x09EF1108  token: 0x6000315
        private UnityEngine.Transform GetUseTransform(System.Int32 index) { }
        // RVA: 0x09EF0A64  token: 0x6000317
        public System.Collections.Generic.List<System.Collections.Generic.List<System.Collections.Generic.List<UnityEngine.Transform>>> GetTransformGrid(System.Int32& maxLevel) { }
        // RVA: 0x04D9BE00  token: 0x6000318
        public virtual System.Int32 GetVersion() { }
        // RVA: 0x04D8C9E0  token: 0x6000319
        public virtual System.Int32 GetErrorVersion() { }
        // RVA: 0x09EEF4EC  token: 0x600031A
        public virtual System.Void CreateVerifyData() { }
        // RVA: 0x09EF17A0  token: 0x600031B
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: 0x09EEFCE0  token: 0x600031C
        public virtual System.String GetInformation() { }
        // RVA: -1  // generic def  token: 0x600031D
        public virtual System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict) { }
        // RVA: 0x09EF1158  token: 0x600031E
        public virtual System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones() { }
        // RVA: 0x09EEF870  token: 0x600031F
        public virtual System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList) { }
        // RVA: 0x09EEFC00  token: 0x6000320
        public virtual System.Collections.Generic.List<System.Int32> GetEditorTriangleList() { }
        // RVA: 0x09EEF790  token: 0x6000321
        public virtual System.Collections.Generic.List<System.Int32> GetEditorLineList() { }
        // RVA: 0x09EF09C0  token: 0x6000322
        public virtual System.Collections.Generic.List<System.Int32> GetSelectionList() { }
        // RVA: 0x011EC580  token: 0x6000323
        public virtual System.Collections.Generic.List<System.Int32> GetUseList() { }
        // RVA: 0x09EEF60C  token: 0x6000324
        public virtual System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject() { }
        // RVA: 0x09EEF534  token: 0x6000325
        public virtual MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source) { }
        // RVA: 0x09EF12C8  token: 0x6000326
        private System.Void ResetParams() { }
        // RVA: 0x09EF19E0  token: 0x6000327
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004C  // size: 0x100
    public class MagicaBoneSpring : MagicaCloth.BaseCloth
    {
        // Fields
        private static System.Int32 DATA_VERSION;  // const
        private static System.Int32 ERR_DATA_VERSION;  // const
        private MagicaCloth.MeshData meshData;  // 0xc8
        private System.Int32 meshDataHash;  // 0xd0
        private System.Int32 meshDataVersion;  // 0xd4
        private MagicaCloth.BoneClothTarget clothTarget;  // 0xd8
        private System.Collections.Generic.List<UnityEngine.Transform> useTransformList;  // 0xe0
        private System.Collections.Generic.List<UnityEngine.Vector3> useTransformPositionList;  // 0xe8
        private System.Collections.Generic.List<UnityEngine.Quaternion> useTransformRotationList;  // 0xf0
        private System.Collections.Generic.List<UnityEngine.Vector3> useTransformScaleList;  // 0xf8

        // Properties
        MagicaCloth.BoneClothTarget ClothTarget { get; /* RVA: 0x04D88EF0 */ }
        MagicaCloth.MeshData MeshData { get; /* RVA: 0x04D8D1D0 */ }
        System.Int32 UseTransformCount { get; /* RVA: 0x09EF3930 */ }

        // Methods
        // RVA: 0x04DA3410  token: 0x6000328
        public virtual MagicaCloth.ComponentType GetComponentType() { }
        // RVA: 0x09EF1D04  token: 0x6000329
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x09EF32D0  token: 0x600032C
        protected virtual System.Void Reset() { }
        // RVA: 0x09EF12C0  token: 0x600032D
        protected virtual System.Void OnValidate() { }
        // RVA: 0x09EF1B3C  token: 0x600032E
        protected virtual System.Void ClothInit() { }
        // RVA: 0x09EEF494  token: 0x600032F
        protected virtual System.Void ClothDispose() { }
        // RVA: 0x09EEF48C  token: 0x6000330
        protected virtual System.Void ClothActive() { }
        // RVA: 0x09EF1634  token: 0x6000331
        protected virtual System.UInt32 UserFlag(System.Int32 index) { }
        // RVA: 0x09EF3468  token: 0x6000332
        protected virtual UnityEngine.Transform UserTransform(System.Int32 index) { }
        // RVA: 0x09EF32D8  token: 0x6000333
        protected virtual Unity.Mathematics.float3 UserTransformLocalPosition(System.Int32 vindex) { }
        // RVA: 0x09EF33A8  token: 0x6000334
        protected virtual Unity.Mathematics.quaternion UserTransformLocalRotation(System.Int32 vindex) { }
        // RVA: 0x012081B0  token: 0x6000335
        public virtual System.Boolean IsRequiresDeformer() { }
        // RVA: 0x011EC580  token: 0x6000336
        public virtual MagicaCloth.BaseMeshDeformer GetDeformer() { }
        // RVA: 0x04D8D1D0  token: 0x6000337
        protected virtual MagicaCloth.MeshData GetMeshData() { }
        // RVA: 0x041E1670  token: 0x6000338
        protected virtual System.Void WorkerInit() { }
        // RVA: 0x041E1670  token: 0x6000339
        protected virtual System.Void SetDeformerUseVertex(System.Boolean sw, MagicaCloth.BaseMeshDeformer deformer) { }
        // RVA: 0x09EEF444  token: 0x600033A
        protected virtual System.Void ChangeUseUnityPhysics(System.Boolean sw) { }
        // RVA: 0x09EF2E5C  token: 0x600033B
        protected virtual System.Void OnChangeCalculation() { }
        // RVA: 0x09EF2A54  token: 0x600033C
        public System.Collections.Generic.List<UnityEngine.Transform> GetTransformList() { }
        // RVA: 0x09EF2D4C  token: 0x600033D
        private UnityEngine.Transform GetUseTransform(System.Int32 index) { }
        // RVA: 0x04D9BE00  token: 0x600033F
        public virtual System.Int32 GetVersion() { }
        // RVA: 0x04D8C9E0  token: 0x6000340
        public virtual System.Int32 GetErrorVersion() { }
        // RVA: 0x09EEF4EC  token: 0x6000341
        public virtual System.Void CreateVerifyData() { }
        // RVA: 0x09EF3470  token: 0x6000342
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: 0x09EF2370  token: 0x6000343
        public virtual System.String GetInformation() { }
        // RVA: -1  // generic def  token: 0x6000344
        public virtual System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict) { }
        // RVA: 0x09EF2DD0  token: 0x6000345
        public virtual System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones() { }
        // RVA: 0x09EF1F00  token: 0x6000346
        public virtual System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList) { }
        // RVA: 0x09EF2290  token: 0x6000347
        public virtual System.Collections.Generic.List<System.Int32> GetEditorTriangleList() { }
        // RVA: 0x09EF1E20  token: 0x6000348
        public virtual System.Collections.Generic.List<System.Int32> GetEditorLineList() { }
        // RVA: 0x09EF29B0  token: 0x6000349
        public virtual System.Collections.Generic.List<System.Int32> GetSelectionList() { }
        // RVA: 0x09EF2B64  token: 0x600034A
        public virtual System.Collections.Generic.List<System.Int32> GetUseList() { }
        // RVA: 0x09EF1C9C  token: 0x600034B
        public virtual System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject() { }
        // RVA: 0x09EF1BC4  token: 0x600034C
        public virtual MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source) { }
        // RVA: 0x09EF2F64  token: 0x600034D
        private System.Void ResetParams() { }
        // RVA: 0x09EF3790  token: 0x600034E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004D  // size: 0xD8
    public class MagicaMeshCloth : MagicaCloth.BaseCloth
    {
        // Fields
        private static System.Int32 DATA_VERSION;  // const
        private static System.Int32 ERR_DATA_VERSION;  // const
        private MagicaCloth.MagicaVirtualDeformer virtualDeformer;  // 0xc8
        private System.Int32 virtualDeformerHash;  // 0xd0
        private System.Int32 virtualDeformerVersion;  // 0xd4

        // Properties
        MagicaCloth.VirtualMeshDeformer Deformer { get; /* RVA: 0x09EF51C8 */ }

        // Methods
        // RVA: 0x04DA3420  token: 0x600034F
        public virtual MagicaCloth.ComponentType GetComponentType() { }
        // RVA: 0x09EF3B3C  token: 0x6000350
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x09EF4E44  token: 0x6000352
        protected virtual System.Void Reset() { }
        // RVA: 0x09EF12C0  token: 0x6000353
        protected virtual System.Void OnValidate() { }
        // RVA: 0x09EF4ADC  token: 0x6000354
        protected virtual System.Void OnInit() { }
        // RVA: 0x09EF4AC4  token: 0x6000355
        protected virtual System.Void OnActive() { }
        // RVA: 0x09EF4AD4  token: 0x6000356
        protected virtual System.Void OnInactive() { }
        // RVA: 0x09EF4ACC  token: 0x6000357
        protected virtual System.Void OnDispose() { }
        // RVA: 0x011EC580  token: 0x6000358
        protected virtual System.UInt32 UserFlag(System.Int32 index) { }
        // RVA: 0x011EC580  token: 0x6000359
        protected virtual UnityEngine.Transform UserTransform(System.Int32 index) { }
        // RVA: 0x04DA3430  token: 0x600035A
        protected virtual Unity.Mathematics.float3 UserTransformLocalPosition(System.Int32 vindex) { }
        // RVA: 0x09EF4F38  token: 0x600035B
        protected virtual Unity.Mathematics.quaternion UserTransformLocalRotation(System.Int32 vindex) { }
        // RVA: 0x02FFF600  token: 0x600035C
        public virtual System.Boolean IsRequiresDeformer() { }
        // RVA: 0x09EF3B7C  token: 0x600035D
        public virtual MagicaCloth.BaseMeshDeformer GetDeformer() { }
        // RVA: 0x09EF4724  token: 0x600035E
        protected virtual MagicaCloth.MeshData GetMeshData() { }
        // RVA: 0x09EF5068  token: 0x600035F
        protected virtual System.Void WorkerInit() { }
        // RVA: 0x09EF4E4C  token: 0x6000360
        protected virtual System.Void SetDeformerUseVertex(System.Boolean sw, MagicaCloth.BaseMeshDeformer deformer) { }
        // RVA: 0x09EF3970  token: 0x6000361
        protected virtual System.Void ChangeUseUnityPhysics(System.Boolean sw) { }
        // RVA: 0x04D9BE00  token: 0x6000362
        public virtual System.Int32 GetVersion() { }
        // RVA: 0x04D8C9E0  token: 0x6000363
        public virtual System.Int32 GetErrorVersion() { }
        // RVA: 0x09EEF4EC  token: 0x6000364
        public virtual System.Void CreateVerifyData() { }
        // RVA: 0x09EF4F7C  token: 0x6000365
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: 0x09EF3C20  token: 0x6000366
        public virtual System.String GetInformation() { }
        // RVA: 0x09EF4A28  token: 0x6000367
        public System.Boolean IsValidPointSelect() { }
        // RVA: 0x09EF3BAC  token: 0x6000368
        public virtual System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList) { }
        // RVA: 0x09EF3BF8  token: 0x6000369
        public virtual System.Collections.Generic.List<System.Int32> GetEditorTriangleList() { }
        // RVA: 0x09EF3B84  token: 0x600036A
        public virtual System.Collections.Generic.List<System.Int32> GetEditorLineList() { }
        // RVA: 0x09EF4744  token: 0x600036B
        public virtual System.Collections.Generic.List<System.Int32> GetSelectionList() { }
        // RVA: 0x09EF4858  token: 0x600036C
        public virtual System.Collections.Generic.List<System.Int32> GetUseList() { }
        // RVA: 0x09EF3ABC  token: 0x600036D
        public virtual System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject() { }
        // RVA: 0x09EF39AC  token: 0x600036E
        public virtual MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source) { }
        // RVA: 0x09EF4AE4  token: 0x600036F
        private System.Void ResetParams() { }
        // RVA: 0x09EF51C0  token: 0x6000370
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004E  // size: 0xF8
    public class MagicaMeshSpring : MagicaCloth.BaseCloth
    {
        // Fields
        private static System.Int32 DATA_VERSION;  // const
        private static System.Int32 ERR_DATA_VERSION;  // const
        private MagicaCloth.MagicaVirtualDeformer virtualDeformer;  // 0xc8
        private System.Int32 virtualDeformerHash;  // 0xd0
        private System.Int32 virtualDeformerVersion;  // 0xd4
        private UnityEngine.Transform centerTransform;  // 0xd8
        private MagicaCloth.MagicaMeshSpring.Axis directionAxis;  // 0xe0
        private MagicaCloth.SpringData springData;  // 0xe8
        private System.Int32 springDataHash;  // 0xf0
        private System.Int32 springDataVersion;  // 0xf4

        // Properties
        MagicaCloth.VirtualMeshDeformer Deformer { get; /* RVA: 0x09EF67A0 */ }
        MagicaCloth.SpringData SpringData { get; /* RVA: 0x04D88590 */ }
        System.Int32 UseVertexCount { get; /* RVA: 0x09EF6818 */ }
        UnityEngine.Transform CenterTransform { get; /* RVA: 0x04D88EF0 */ set; /* RVA: 0x06902698 */ }
        MagicaCloth.MagicaMeshSpring.Axis DirectionAxis { get; /* RVA: 0x04D9A880 */ set; /* RVA: 0x04DA3470 */ }
        UnityEngine.Vector3 CenterTransformDirection { get; /* RVA: 0x09EF660C */ }

        // Methods
        // RVA: 0x04DA3450  token: 0x6000371
        public virtual MagicaCloth.ComponentType GetComponentType() { }
        // RVA: 0x09EF55CC  token: 0x6000372
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x09EF564C  token: 0x600037B
        public MagicaCloth.SpringData.DeformerData GetDeformerData() { }
        // RVA: 0x09EF61C0  token: 0x600037C
        protected virtual System.Void Reset() { }
        // RVA: 0x09EF12C0  token: 0x600037D
        protected virtual System.Void OnValidate() { }
        // RVA: 0x09EF5240  token: 0x600037E
        protected virtual System.Void ClothInit() { }
        // RVA: 0x09EEF48C  token: 0x600037F
        protected virtual System.Void ClothActive() { }
        // RVA: 0x04DA3460  token: 0x6000380
        protected virtual System.UInt32 UserFlag(System.Int32 index) { }
        // RVA: 0x04D88EF0  token: 0x6000381
        protected virtual UnityEngine.Transform UserTransform(System.Int32 index) { }
        // RVA: 0x09EF626C  token: 0x6000382
        protected virtual Unity.Mathematics.float3 UserTransformLocalPosition(System.Int32 vindex) { }
        // RVA: 0x09EF62D0  token: 0x6000383
        protected virtual Unity.Mathematics.quaternion UserTransformLocalRotation(System.Int32 vindex) { }
        // RVA: 0x02FFF600  token: 0x6000384
        public virtual System.Boolean IsRequiresDeformer() { }
        // RVA: 0x09EF566C  token: 0x6000385
        public virtual MagicaCloth.BaseMeshDeformer GetDeformer() { }
        // RVA: 0x011EC580  token: 0x6000386
        protected virtual MagicaCloth.MeshData GetMeshData() { }
        // RVA: 0x09EF6490  token: 0x6000387
        protected virtual System.Void WorkerInit() { }
        // RVA: 0x09EF61C8  token: 0x6000388
        protected virtual System.Void SetDeformerUseVertex(System.Boolean sw, MagicaCloth.BaseMeshDeformer deformer) { }
        // RVA: 0x09EF3970  token: 0x6000389
        protected virtual System.Void ChangeUseUnityPhysics(System.Boolean sw) { }
        // RVA: 0x09EF5D74  token: 0x600038A
        protected virtual System.Void OnChangeCalculation() { }
        // RVA: 0x04D9BE00  token: 0x600038B
        public virtual System.Int32 GetVersion() { }
        // RVA: 0x04D8C9E0  token: 0x600038C
        public virtual System.Int32 GetErrorVersion() { }
        // RVA: 0x09EF541C  token: 0x600038D
        public virtual System.Void CreateVerifyData() { }
        // RVA: 0x09EF6324  token: 0x600038E
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: 0x09EF58E4  token: 0x600038F
        public virtual System.String GetInformation() { }
        // RVA: 0x041E1670  token: 0x6000390
        public System.Void VerifyDeformer() { }
        // RVA: -1  // generic def  token: 0x6000391
        public virtual System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict) { }
        // RVA: 0x09EF5D0C  token: 0x6000392
        public virtual System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones() { }
        // RVA: 0x09EF56E0  token: 0x6000393
        public virtual System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList) { }
        // RVA: 0x011EC580  token: 0x6000394
        public virtual System.Collections.Generic.List<System.Int32> GetEditorTriangleList() { }
        // RVA: 0x011EC580  token: 0x6000395
        public virtual System.Collections.Generic.List<System.Int32> GetEditorLineList() { }
        // RVA: 0x011EC580  token: 0x6000396
        public virtual System.Collections.Generic.List<System.Int32> GetSelectionList() { }
        // RVA: 0x011EC580  token: 0x6000397
        public virtual System.Collections.Generic.List<System.Int32> GetUseList() { }
        // RVA: 0x09EF5564  token: 0x6000398
        public virtual System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject() { }
        // RVA: 0x09EF548C  token: 0x6000399
        public virtual MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source) { }
        // RVA: 0x09EF5E08  token: 0x600039A
        private System.Void ResetParams() { }
        // RVA: 0x09EF51C0  token: 0x600039B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000050  // size: 0x28
    public class SelectionData : MagicaCloth.ShareDataObject
    {
        // Fields
        private static System.Int32 DATA_VERSION;  // const
        public static System.Int32 Invalid;  // const
        public static System.Int32 Move;  // const
        public static System.Int32 Fixed;  // const
        public static System.Int32 Extend;  // const
        public System.Collections.Generic.List<MagicaCloth.SelectionData.DeformerSelection> selectionList;  // 0x20

        // Properties
        System.Int32 DeformerCount { get; /* RVA: 0x09EFC5A4 */ }

        // Methods
        // RVA: 0x09EFB50C  token: 0x600039D
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x02BBE540  token: 0x600039E
        public virtual System.Int32 GetVersion() { }
        // RVA: 0x09EFC518  token: 0x600039F
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: 0x09EFB430  token: 0x60003A0
        public System.Boolean Compare(MagicaCloth.SelectionData sel) { }
        // RVA: 0x09EFB548  token: 0x60003A1
        public System.Collections.Generic.List<System.Int32> GetSelectionData(MagicaCloth.MeshData meshData, System.Collections.Generic.List<MagicaCloth.MeshData> childMeshDataList) { }
        // RVA: 0x09EFB960  token: 0x60003A2
        private System.Int32 GetSelection(MagicaCloth.MeshData meshData, System.Int32 vindex, System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<System.UInt32>> dict, System.Collections.Generic.List<MagicaCloth.MeshData> childMeshDataList, System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.UInt64,System.Int32>> hashList) { }
        // RVA: 0x09EFBE2C  token: 0x60003A3
        public System.Void SetSelectionData(MagicaCloth.MeshData meshData, System.Collections.Generic.List<System.Int32> selects, System.Collections.Generic.List<MagicaCloth.MeshData> childMeshDataList) { }
        // RVA: 0x09EFB700  token: 0x60003A4
        private System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.UInt64,System.Int32>> GetSelectionVertexHashList() { }
        // RVA: 0x09EFC54C  token: 0x60003A5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000052  // size: 0x38
    public class SpringData : MagicaCloth.ShareDataObject
    {
        // Fields
        private static System.Int32 DATA_VERSION;  // const
        public MagicaCloth.SpringData.DeformerData deformerData;  // 0x20
        public UnityEngine.Vector3 initScale;  // 0x28

        // Properties
        System.Int32 UseVertexCount { get; /* RVA: 0x09EFC620 */ }

        // Methods
        // RVA: 0x09EFC5E4  token: 0x60003A9
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x02BBE540  token: 0x60003AB
        public virtual System.Int32 GetVersion() { }
        // RVA: 0x09EFC604  token: 0x60003AC
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: 0x03573370  token: 0x60003AD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000054  // size: 0x10
    public static class Define
    {
        // Methods
        // RVA: 0x06911EF8  token: 0x60003B1
        public static System.Boolean IsNormal(MagicaCloth.Define.Error err) { }
        // RVA: 0x09EEF16C  token: 0x60003B2
        public static System.Boolean IsError(MagicaCloth.Define.Error err) { }
        // RVA: 0x09EEF180  token: 0x60003B3
        public static System.Boolean IsWarning(MagicaCloth.Define.Error err) { }
        // RVA: 0x09EEEE0C  token: 0x60003B4
        public static System.String GetErrorMessage(MagicaCloth.Define.Error err) { }

    }

    // TypeToken: 0x2000059  // size: 0x48
    public abstract class BaseMeshDeformer : MagicaCloth.IEditorMesh, MagicaCloth.IDataVerify, MagicaCloth.IDataHash
    {
        // Fields
        private MagicaCloth.MeshData meshData;  // 0x10
        private UnityEngine.GameObject targetObject;  // 0x18
        protected System.Int32 dataHash;  // 0x20
        protected System.Int32 dataVersion;  // 0x24
        protected MagicaCloth.RuntimeStatus status;  // 0x28
        private MagicaCloth.CoreComponent parent;  // 0x30
        private System.Int32 <MeshIndex>k__BackingField;  // 0x38
        private System.Int32 <VertexCount>k__BackingField;  // 0x3c
        private System.Int32 <SkinningVertexCount>k__BackingField;  // 0x40
        private System.Int32 <TriangleCount>k__BackingField;  // 0x44

        // Properties
        MagicaCloth.CoreComponent Parent { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        MagicaCloth.MeshData MeshData { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        UnityEngine.GameObject TargetObject { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        MagicaCloth.RuntimeStatus Status { get; /* RVA: 0x04D86240 */ }
        System.Int32 MeshIndex { get; /* RVA: 0x04D863B0 */ set; /* RVA: 0x04D86430 */ }
        System.Int32 VertexCount { get; /* RVA: 0x04D86640 */ set; /* RVA: 0x04D86670 */ }
        System.Int32 SkinningVertexCount { get; /* RVA: 0x04D865E0 */ set; /* RVA: 0x04D86610 */ }
        System.Int32 TriangleCount { get; /* RVA: 0x04D86530 */ set; /* RVA: 0x04D86580 */ }
        System.Boolean IsSkinning { get; /* RVA: 0x09EEBE70 */ }
        System.Int32 BoneCount { get; /* RVA: 0x09EEBDF8 */ }
        System.Int32 SaveDataHash { get; /* RVA: 0x02B70F90 */ }
        System.Int32 SaveDataVersion { get; /* RVA: 0x011F0020 */ }

        // Methods
        // RVA: 0x09EEBA2C  token: 0x60003C4
        public virtual System.Void Init() { }
        // RVA: 0x09EEBBF0  token: 0x60003C5
        protected virtual System.Void OnInit() { }
        // RVA: 0x09EEB8CC  token: 0x60003C6
        public virtual System.Void Dispose() { }
        // RVA: 0x09EEBBB8  token: 0x60003C7
        public virtual System.Void OnEnable() { }
        // RVA: 0x09EEBB80  token: 0x60003C8
        public virtual System.Void OnDisable() { }
        // RVA: 0x09EEBC90  token: 0x60003C9
        public virtual System.Void Update() { }
        // RVA: -1  // abstract  token: 0x60003CA
        private virtual System.Void MeshCalculation(System.Int32 bufferIndex) { }
        // RVA: -1  // abstract  token: 0x60003CB
        private virtual System.Void NormalWriting(System.Int32 bufferIndex) { }
        // RVA: 0x09EEBC54  token: 0x60003CC
        protected System.Void OnUpdateStatus() { }
        // RVA: 0x041E1670  token: 0x60003CD
        protected virtual System.Void OnActive() { }
        // RVA: 0x041E1670  token: 0x60003CE
        protected virtual System.Void OnInactive() { }
        // RVA: 0x012081B0  token: 0x60003CF
        public virtual System.Boolean IsMeshUse() { }
        // RVA: 0x012081B0  token: 0x60003D0
        public virtual System.Boolean IsActiveMesh() { }
        // RVA: 0x041E1670  token: 0x60003D3
        public virtual System.Void AddUseMesh(System.Object parent) { }
        // RVA: 0x041E1670  token: 0x60003D4
        public virtual System.Void RemoveUseMesh(System.Object parent) { }
        // RVA: 0x012081B0  token: 0x60003D5
        public virtual System.Boolean AddUseVertex(System.Int32 vindex, System.Boolean fix) { }
        // RVA: 0x012081B0  token: 0x60003D6
        public virtual System.Boolean RemoveUseVertex(System.Int32 vindex, System.Boolean fix) { }
        // RVA: 0x041E1670  token: 0x60003D7
        public virtual System.Void ResetFuturePrediction() { }
        // RVA: 0x041E1670  token: 0x60003D8
        public virtual System.Void ChangeUseUnityPhysics(System.Boolean sw) { }
        // RVA: 0x09EEB954  token: 0x60003D9
        public virtual System.Int32 GetDataHash() { }
        // RVA: -1  // abstract  token: 0x60003DC
        public virtual System.Int32 GetVersion() { }
        // RVA: 0x09EEBCD4  token: 0x60003DD
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: 0x09EEB89C  token: 0x60003DE
        public virtual System.Void CreateVerifyData() { }
        // RVA: 0x09EEBA00  token: 0x60003DF
        public virtual System.String GetInformation() { }
        // RVA: -1  // abstract  token: 0x60003E0
        public virtual System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList) { }
        // RVA: -1  // abstract  token: 0x60003E1
        public virtual System.Collections.Generic.List<System.Int32> GetEditorTriangleList() { }
        // RVA: -1  // abstract  token: 0x60003E2
        public virtual System.Collections.Generic.List<System.Int32> GetEditorLineList() { }
        // RVA: 0x09EEBD8C  token: 0x60003E3
        protected System.Void .ctor() { }
        // RVA: 0x011F36E0  token: 0x60003E4
        private System.Object <Init>b__33_0() { }

    }

    // TypeToken: 0x200005A  // size: 0x50
    public class MagicaRenderDeformer : MagicaCloth.CoreComponent
    {
        // Fields
        private static System.Int32 DATA_VERSION;  // const
        private static System.Int32 ERR_DATA_VERSION;  // const
        private MagicaCloth.RenderMeshDeformer deformer;  // 0x38
        private System.Int32 deformerHash;  // 0x40
        private System.Int32 deformerVersion;  // 0x44
        private MagicaCloth.PhysicsTeam.TeamCullingMode <cullModeCash>k__BackingField;  // 0x48

        // Properties
        MagicaCloth.PhysicsTeam.TeamCullingMode cullModeCash { get; /* RVA: 0x04D863D0 */ set; /* RVA: 0x04D86450 */ }
        MagicaCloth.RenderMeshDeformer Deformer { get; /* RVA: 0x09EF7280 */ }

        // Methods
        // RVA: 0x04DA3480  token: 0x60003E7
        public virtual MagicaCloth.ComponentType GetComponentType() { }
        // RVA: 0x09EF6A60  token: 0x60003E8
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x09EF6DE0  token: 0x60003EA
        private System.Void OnValidate() { }
        // RVA: 0x09EF6D98  token: 0x60003EB
        protected virtual System.Void OnInit() { }
        // RVA: 0x09EF6D4C  token: 0x60003EC
        protected virtual System.Void OnDispose() { }
        // RVA: 0x09EF6DBC  token: 0x60003ED
        protected virtual System.Void OnUpdate() { }
        // RVA: 0x09EF6C0C  token: 0x60003EE
        protected virtual System.Void OnActive() { }
        // RVA: 0x09EF6D74  token: 0x60003EF
        protected virtual System.Void OnInactive() { }
        // RVA: 0x09EF6C78  token: 0x60003F0
        protected System.Void OnBecameVisible() { }
        // RVA: 0x09EF6C30  token: 0x60003F1
        protected System.Void OnBecameInvisible() { }
        // RVA: 0x09EF6E04  token: 0x60003F2
        private virtual System.Void UpdateCullingMode(MagicaCloth.CoreComponent caller) { }
        // RVA: 0x09EF6CC0  token: 0x60003F3
        protected virtual System.Void OnChangeCalculation() { }
        // RVA: 0x02BBE540  token: 0x60003F4
        public virtual System.Int32 GetVersion() { }
        // RVA: 0x011EC580  token: 0x60003F5
        public virtual System.Int32 GetErrorVersion() { }
        // RVA: 0x09EF68C4  token: 0x60003F6
        public virtual System.Void CreateVerifyData() { }
        // RVA: 0x09EF7170  token: 0x60003F7
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: 0x09EF6B24  token: 0x60003F8
        public virtual System.String GetInformation() { }
        // RVA: -1  // generic def  token: 0x60003F9
        public virtual System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict) { }
        // RVA: 0x09EF6B90  token: 0x60003FA
        public virtual System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones() { }
        // RVA: 0x09EF6890  token: 0x60003FB
        protected virtual System.Void ChangeUseUnityPhysics(System.Boolean sw) { }
        // RVA: 0x09EF6AB0  token: 0x60003FC
        public virtual System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList) { }
        // RVA: 0x09EF6AFC  token: 0x60003FD
        public virtual System.Collections.Generic.List<System.Int32> GetEditorTriangleList() { }
        // RVA: 0x09EF6A88  token: 0x60003FE
        public virtual System.Collections.Generic.List<System.Int32> GetEditorLineList() { }
        // RVA: 0x09EF6B6C  token: 0x60003FF
        public virtual System.Collections.Generic.List<System.Int32> GetUseList() { }
        // RVA: 0x09EF69EC  token: 0x6000400
        public virtual System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject() { }
        // RVA: 0x09EF690C  token: 0x6000401
        public virtual MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source) { }
        // RVA: 0x09EF720C  token: 0x6000402
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005B  // size: 0x50
    public class MagicaVirtualDeformer : MagicaCloth.CoreComponent
    {
        // Fields
        private static System.Int32 DATA_VERSION;  // const
        private static System.Int32 ERR_DATA_VERSION;  // const
        private MagicaCloth.VirtualMeshDeformer deformer;  // 0x38
        private System.Int32 deformerHash;  // 0x40
        private System.Int32 deformerVersion;  // 0x44
        private MagicaCloth.PhysicsTeam.TeamCullingMode <cullModeCash>k__BackingField;  // 0x48

        // Properties
        MagicaCloth.PhysicsTeam.TeamCullingMode cullModeCash { get; /* RVA: 0x04D863D0 */ set; /* RVA: 0x04D86450 */ }
        MagicaCloth.VirtualMeshDeformer Deformer { get; /* RVA: 0x09EF7280 */ }

        // Methods
        // RVA: 0x04DA3490  token: 0x6000405
        public virtual MagicaCloth.ComponentType GetComponentType() { }
        // RVA: 0x09EF6A60  token: 0x6000406
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x041E1670  token: 0x6000408
        private System.Void OnValidate() { }
        // RVA: 0x09EF7810  token: 0x6000409
        protected virtual System.Void OnInit() { }
        // RVA: 0x09EF77D4  token: 0x600040A
        protected virtual System.Void OnDispose() { }
        // RVA: 0x09EF6DBC  token: 0x600040B
        protected virtual System.Void OnUpdate() { }
        // RVA: 0x09EF6C0C  token: 0x600040C
        protected virtual System.Void OnActive() { }
        // RVA: 0x09EF6D74  token: 0x600040D
        protected virtual System.Void OnInactive() { }
        // RVA: 0x09EF76A4  token: 0x600040E
        private System.Void LinkRenderDeformerStatus(System.Boolean sw) { }
        // RVA: 0x09EF72AC  token: 0x600040F
        protected virtual System.Void ChangeUseUnityPhysics(System.Boolean sw) { }
        // RVA: 0x09EF7848  token: 0x6000410
        private virtual System.Void UpdateCullingMode(MagicaCloth.CoreComponent caller) { }
        // RVA: 0x09EF77A0  token: 0x6000411
        protected virtual System.Void OnChangeCalculation() { }
        // RVA: 0x02BBE540  token: 0x6000412
        public virtual System.Int32 GetVersion() { }
        // RVA: 0x011EC580  token: 0x6000413
        public virtual System.Int32 GetErrorVersion() { }
        // RVA: 0x09EF68C4  token: 0x6000414
        public virtual System.Void CreateVerifyData() { }
        // RVA: 0x09EF7C2C  token: 0x6000415
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: 0x09EF6B24  token: 0x6000416
        public virtual System.String GetInformation() { }
        // RVA: -1  // generic def  token: 0x6000417
        public virtual System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict) { }
        // RVA: 0x09EF7628  token: 0x6000418
        public virtual System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones() { }
        // RVA: 0x09EF6AB0  token: 0x6000419
        public virtual System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList) { }
        // RVA: 0x09EF6AFC  token: 0x600041A
        public virtual System.Collections.Generic.List<System.Int32> GetEditorTriangleList() { }
        // RVA: 0x09EF6A88  token: 0x600041B
        public virtual System.Collections.Generic.List<System.Int32> GetEditorLineList() { }
        // RVA: 0x09EF749C  token: 0x600041C
        public virtual System.Collections.Generic.List<System.Int32> GetUseList() { }
        // RVA: 0x09EF7428  token: 0x600041D
        public virtual System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject() { }
        // RVA: 0x09EF7348  token: 0x600041E
        public virtual MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source) { }
        // RVA: 0x09EF7C98  token: 0x600041F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005C  // size: 0x98
    public class MeshData : MagicaCloth.ShareDataObject
    {
        // Fields
        private static System.Int32 DATA_VERSION;  // const
        public System.Boolean isSkinning;  // 0x20
        public System.Int32 vertexCount;  // 0x24
        public System.UInt32[] vertexInfoList;  // 0x28
        public MagicaCloth.MeshData.VertexWeight[] vertexWeightList;  // 0x30
        public System.UInt64[] vertexHashList;  // 0x38
        public UnityEngine.Vector2[] uvList;  // 0x40
        public System.Int32 lineCount;  // 0x48
        public System.Int32[] lineList;  // 0x50
        public System.Int32 triangleCount;  // 0x58
        public System.Int32[] triangleList;  // 0x60
        public System.Int32 boneCount;  // 0x68
        public System.UInt32[] vertexToTriangleInfoList;  // 0x70
        public System.Int32[] vertexToTriangleIndexList;  // 0x78
        public System.Collections.Generic.List<MagicaCloth.MeshData.ChildData> childDataList;  // 0x80
        public UnityEngine.Vector3 baseScale;  // 0x88

        // Properties
        System.Int32 VertexCount { get; /* RVA: 0x011F0020 */ }
        System.Int32 VertexHashCount { get; /* RVA: 0x09EF88A8 */ }
        System.Int32 WeightCount { get; /* RVA: 0x09EF88BC */ }
        System.Int32 LineCount { get; /* RVA: 0x04D863D0 */ }
        System.Int32 TriangleCount { get; /* RVA: 0x04D86570 */ }
        System.Int32 BoneCount { get; /* RVA: 0x04D86CF0 */ }
        System.Int32 ChildCount { get; /* RVA: 0x09EF8868 */ }

        // Methods
        // RVA: 0x09EF8428  token: 0x6000427
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x02BBE540  token: 0x6000428
        public virtual System.Int32 GetVersion() { }
        // RVA: 0x09EF87EC  token: 0x6000429
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: 0x09EF862C  token: 0x600042A
        public System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<System.UInt32>> GetVirtualToChildVertexDict() { }
        // RVA: 0x09EF7D0C  token: 0x600042B
        public System.Collections.Generic.List<System.Int32> ExtendSelection(System.Collections.Generic.List<System.Int32> originalSelection, System.Boolean extendNext, System.Boolean extendWeight) { }
        // RVA: 0x09EF8808  token: 0x600042C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005F  // size: 0xA8
    public class RenderMeshDeformer : MagicaCloth.BaseMeshDeformer, MagicaCloth.IBoneReplace
    {
        // Fields
        private static System.Int32 DATA_VERSION;  // const
        private MagicaCloth.RenderMeshDeformer.RecalculateMode normalAndTangentUpdateMode;  // 0x48
        private MagicaCloth.RenderMeshDeformer.BoundsMode boundsUpdateMode;  // 0x4c
        private UnityEngine.Mesh sharedMesh;  // 0x50
        private System.Int32 meshOptimize;  // 0x58
        private UnityEngine.Renderer renderer;  // 0x60
        private UnityEngine.MeshFilter meshFilter;  // 0x68
        private UnityEngine.SkinnedMeshRenderer skinMeshRenderer;  // 0x70
        private UnityEngine.Transform[] originalBones;  // 0x78
        private UnityEngine.Transform[] boneList;  // 0x80
        private UnityEngine.Mesh cloneMesh;  // 0x88
        private UnityEngine.GraphicsBuffer vertexBuffer;  // 0x90
        private System.Boolean <IsChangePosition>k__BackingField;  // 0x98
        private System.Boolean <IsChangeNormalTangent>k__BackingField;  // 0x99
        private System.Boolean <IsChangeBoneWeights>k__BackingField;  // 0x9a
        private System.Boolean oldUse;  // 0x9b
        private System.Boolean <IsWriteSkip>k__BackingField;  // 0x9c
        private System.Boolean <IsFasterWriteUpdate>k__BackingField;  // 0x9d
        private System.Boolean <IsWriteMeshPosition>k__BackingField;  // 0x9e
        private System.Boolean <IsWriteMeshBoneWeight>k__BackingField;  // 0x9f
        private System.Boolean IsWriteMeshNormal;  // 0xa0
        private System.Boolean IsWriteMeshTangent;  // 0xa1
        private System.Boolean <IsFasterWrite>k__BackingField;  // 0xa2

        // Properties
        System.Boolean IsChangePosition { get; /* RVA: 0x04D86C50 */ set; /* RVA: 0x04D86C70 */ }
        System.Boolean IsChangeNormalTangent { get; /* RVA: 0x04DA34C0 */ set; /* RVA: 0x04DA3510 */ }
        System.Boolean IsChangeBoneWeights { get; /* RVA: 0x04DA34B0 */ set; /* RVA: 0x04DA3500 */ }
        System.Boolean IsWriteSkip { get; /* RVA: 0x04D92ED0 */ set; /* RVA: 0x04D92F60 */ }
        System.Boolean IsFasterWriteUpdate { get; /* RVA: 0x04DA34D0 */ set; /* RVA: 0x04DA3520 */ }
        System.Boolean IsWriteMeshPosition { get; /* RVA: 0x04DA34F0 */ set; /* RVA: 0x04DA3540 */ }
        System.Boolean IsWriteMeshBoneWeight { get; /* RVA: 0x04DA34E0 */ set; /* RVA: 0x04DA3530 */ }
        UnityEngine.Mesh SharedMesh { get; /* RVA: 0x04D86270 */ }
        System.Boolean IsFasterWrite { get; /* RVA: 0x04D90800 */ set; /* RVA: 0x04D90810 */ }
        System.Boolean IsRendererVisible { get; /* RVA: 0x09EFB3C4 */ }
        System.Boolean HasNormal { get; /* RVA: 0x09EFB3B0 */ }

        // Methods
        // RVA: 0x09EF8F14  token: 0x600043E
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x09EFAE54  token: 0x6000442
        public System.Void OnValidate() { }
        // RVA: 0x09EFA570  token: 0x6000443
        protected virtual System.Void OnInit() { }
        // RVA: 0x09EFA36C  token: 0x6000444
        protected virtual System.Void OnActive() { }
        // RVA: 0x09EFA47C  token: 0x6000445
        protected virtual System.Void OnInactive() { }
        // RVA: 0x09EF8CD4  token: 0x6000446
        public virtual System.Void Dispose() { }
        // RVA: 0x09EFB184  token: 0x6000447
        private System.Void SetRecalculateNormalAndTangentMode() { }
        // RVA: 0x09EF8C50  token: 0x6000448
        public virtual System.Void ChangeUseUnityPhysics(System.Boolean sw) { }
        // RVA: 0x09EF8BC0  token: 0x6000449
        public System.Void ChangeCalculation(System.Boolean sw) { }
        // RVA: 0x09EF9D88  token: 0x600044A
        public virtual System.Boolean IsMeshUse() { }
        // RVA: 0x09EF9D10  token: 0x600044B
        public virtual System.Boolean IsActiveMesh() { }
        // RVA: 0x09EF88D0  token: 0x600044C
        public virtual System.Void AddUseMesh(System.Object parent) { }
        // RVA: 0x09EFAE94  token: 0x600044D
        public virtual System.Void RemoveUseMesh(System.Object parent) { }
        // RVA: 0x09EF9E00  token: 0x6000450
        private virtual System.Void MeshCalculation(System.Int32 bufferIndex) { }
        // RVA: 0x09EFA1F4  token: 0x6000451
        private virtual System.Void NormalWriting(System.Int32 bufferIndex) { }
        // RVA: 0x09EF8DC8  token: 0x6000452
        private System.Boolean FasterWriting(System.Int32 bufferIndex, UnityEngine.ComputeShader compute, System.Int32 kernel, System.Int32 index, System.Int32& maxVertexCount) { }
        // RVA: 0x04DA34A0  token: 0x6000453
        public System.Void ChangeNormalTangentUpdateMode() { }
        // RVA: -1  // generic def  token: 0x6000454
        public virtual System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict) { }
        // RVA: 0x09EF9C0C  token: 0x6000455
        public virtual System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones() { }
        // RVA: 0x09EF8F98  token: 0x6000456
        public virtual System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList) { }
        // RVA: 0x09EF9328  token: 0x6000457
        public virtual System.Collections.Generic.List<System.Int32> GetEditorTriangleList() { }
        // RVA: 0x011EC580  token: 0x6000458
        public virtual System.Collections.Generic.List<System.Int32> GetEditorLineList() { }
        // RVA: 0x09EF93D8  token: 0x6000459
        public System.Collections.Generic.List<System.Int32> GetEditorUseList() { }
        // RVA: 0x02BBE540  token: 0x600045A
        public virtual System.Int32 GetVersion() { }
        // RVA: 0x09EFB25C  token: 0x600045B
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: 0x09EF9AD4  token: 0x600045C
        private UnityEngine.Mesh GetTargetSharedMesh() { }
        // RVA: 0x09EF945C  token: 0x600045D
        public virtual System.String GetInformation() { }
        // RVA: 0x09EFB3A0  token: 0x600045E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000062  // size: 0x88
    public class VirtualMeshDeformer : MagicaCloth.BaseMeshDeformer, MagicaCloth.IBoneReplace
    {
        // Fields
        private static System.Int32 DATA_VERSION;  // const
        private System.Collections.Generic.List<MagicaCloth.MagicaRenderDeformer> renderDeformerList;  // 0x48
        private System.Collections.Generic.List<System.Int32> renderDeformerHashList;  // 0x50
        private System.Int32 renderDeformerVersion;  // 0x58
        private System.Single mergeVertexDistance;  // 0x5c
        private System.Single mergeTriangleDistance;  // 0x60
        private System.Single sameSurfaceAngle;  // 0x64
        private System.Boolean useSkinning;  // 0x68
        private System.Int32 maxWeightCount;  // 0x6c
        private System.Single weightPow;  // 0x70
        private System.Collections.Generic.List<UnityEngine.Transform> boneList;  // 0x78
        private System.Collections.Generic.List<System.Int32> sharedChildMeshIndexList;  // 0x80

        // Properties
        System.Single MergeVertexDistance { get; /* RVA: 0x04D91500 */ }
        System.Single MergeTriangleDistance { get; /* RVA: 0x04D88480 */ }
        System.Single SameSurfaceAngle { get; /* RVA: 0x04D88470 */ }
        System.Int32 MaxWeightCount { get; /* RVA: 0x09EFEAC4 */ }
        System.Single WeightPow { get; /* RVA: 0x04D90610 */ }
        System.Int32 RenderDeformerCount { get; /* RVA: 0x09EFEAF0 */ }

        // Methods
        // RVA: 0x09EFCDD0  token: 0x600045F
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x09EFDE7C  token: 0x6000460
        protected virtual System.Void OnInit() { }
        // RVA: 0x09EFCBA8  token: 0x6000461
        public virtual System.Void Dispose() { }
        // RVA: 0x09EFDD10  token: 0x6000462
        protected virtual System.Void OnActive() { }
        // RVA: 0x09EFDDB8  token: 0x6000463
        protected virtual System.Void OnInactive() { }
        // RVA: 0x041E1670  token: 0x6000464
        private virtual System.Void MeshCalculation(System.Int32 bufferIndex) { }
        // RVA: 0x041E1670  token: 0x6000465
        private virtual System.Void NormalWriting(System.Int32 bufferIndex) { }
        // RVA: -1  // generic def  token: 0x6000466
        public virtual System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict) { }
        // RVA: 0x09EFDBAC  token: 0x6000467
        public virtual System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones() { }
        // RVA: 0x0464E630  token: 0x6000468
        public MagicaCloth.BaseMeshDeformer GetDeformer() { }
        // RVA: 0x09EFDA88  token: 0x600046F
        public MagicaCloth.MagicaRenderDeformer GetRenderDeformer(System.Int32 index) { }
        // RVA: 0x09EFDADC  token: 0x6000470
        public System.Int32 GetRenderMeshDeformerIndex(MagicaCloth.RenderMeshDeformer deformer) { }
        // RVA: 0x09EFD938  token: 0x6000471
        public System.Collections.Generic.List<MagicaCloth.MeshData> GetRenderDeformerMeshList() { }
        // RVA: 0x09EFDC98  token: 0x6000472
        public virtual System.Boolean IsMeshUse() { }
        // RVA: 0x09EFDC20  token: 0x6000473
        public virtual System.Boolean IsActiveMesh() { }
        // RVA: 0x09EFC794  token: 0x6000474
        public virtual System.Void AddUseMesh(System.Object parent) { }
        // RVA: 0x09EFE378  token: 0x6000475
        public virtual System.Void RemoveUseMesh(System.Object parent) { }
        // RVA: 0x09EFC878  token: 0x6000476
        public virtual System.Boolean AddUseVertex(System.Int32 vindex, System.Boolean fix) { }
        // RVA: 0x09EFE45C  token: 0x6000477
        public virtual System.Boolean RemoveUseVertex(System.Int32 vindex, System.Boolean fix) { }
        // RVA: 0x09EFE4F4  token: 0x6000478
        public virtual System.Void ResetFuturePrediction() { }
        // RVA: 0x09EFC9A0  token: 0x6000479
        public virtual System.Void ChangeUseUnityPhysics(System.Boolean sw) { }
        // RVA: 0x09EFC910  token: 0x600047A
        public System.Void ChangeCalculation(System.Boolean sw) { }
        // RVA: 0x09EFCF6C  token: 0x600047B
        public virtual System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList) { }
        // RVA: 0x09EFD284  token: 0x600047C
        public virtual System.Collections.Generic.List<System.Int32> GetEditorTriangleList() { }
        // RVA: 0x09EFCEB8  token: 0x600047D
        public virtual System.Collections.Generic.List<System.Int32> GetEditorLineList() { }
        // RVA: 0x04D85B80  token: 0x600047E
        public virtual System.Int32 GetVersion() { }
        // RVA: 0x09EFCA24  token: 0x600047F
        public virtual System.Void CreateVerifyData() { }
        // RVA: 0x09EFE554  token: 0x6000480
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: 0x09EFD338  token: 0x6000481
        public virtual System.String GetInformation() { }
        // RVA: 0x09EFE954  token: 0x6000482
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000064  // size: 0x40
    public class ClampDistance2Constraint : MagicaCloth.PhysicsManagerConstraint
    {
        // Fields
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.ClampDistance2Constraint.ClampDistance2Data> dataList;  // 0x20
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.ClampDistance2Constraint.ClampDistance2RootInfo> rootInfoList;  // 0x28
        public MagicaCloth.FixedNativeList<MagicaCloth.ClampDistance2Constraint.GroupData> groupList;  // 0x30
        private MagicaCloth.FixedChunkNativeArray<System.Int32> rootTeamList;  // 0x38

        // Methods
        // RVA: 0x09EFF51C  token: 0x6000485
        public virtual System.Void Create() { }
        // RVA: 0x09EFF674  token: 0x6000486
        public virtual System.Void Release() { }
        // RVA: 0x09EFF0A8  token: 0x6000487
        public System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, System.Single minRatio, System.Single maxRatio, System.Single velocityInfluence, MagicaCloth.ClampDistance2Constraint.ClampDistance2Data[] dataArray, MagicaCloth.ClampDistance2Constraint.ClampDistance2RootInfo[] rootInfoArray) { }
        // RVA: 0x09EFF71C  token: 0x6000488
        public virtual System.Void RemoveTeam(System.Int32 teamId) { }
        // RVA: 0x09EFF36C  token: 0x6000489
        public System.Void ChangeParam(System.Int32 teamId, System.Boolean active, System.Single minRatio, System.Single maxRatio, System.Single velocityInfluence) { }
        // RVA: 0x09EFF91C  token: 0x600048A
        public virtual Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x04D89110  token: 0x600048B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000069  // size: 0x38
    public class ClampDistanceConstraint : MagicaCloth.PhysicsManagerConstraint
    {
        // Fields
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.ClampDistanceConstraint.ClampDistanceData> dataList;  // 0x20
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.ReferenceDataIndex> refDataList;  // 0x28
        public MagicaCloth.FixedNativeList<MagicaCloth.ClampDistanceConstraint.GroupData> groupList;  // 0x30

        // Methods
        // RVA: 0x0442E0A0  token: 0x600048D
        public virtual System.Void Create() { }
        // RVA: 0x09F00430  token: 0x600048E
        public virtual System.Void Release() { }
        // RVA: 0x09F000A0  token: 0x600048F
        public System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, System.Single minRatio, System.Single maxRatio, System.Single velocityInfluence, MagicaCloth.ClampDistanceConstraint.ClampDistanceData[] dataArray, MagicaCloth.ReferenceDataIndex[] refDataArray) { }
        // RVA: 0x09F004B8  token: 0x6000490
        public virtual System.Void RemoveTeam(System.Int32 teamId) { }
        // RVA: 0x09F00280  token: 0x6000491
        public System.Void ChangeParam(System.Int32 teamId, System.Boolean active, System.Single minRatio, System.Single maxRatio, System.Single velocityInfluence) { }
        // RVA: 0x09F00628  token: 0x6000492
        public virtual Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x04D89110  token: 0x6000493
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200006D  // size: 0x28
    public class ClampPositionConstraint : MagicaCloth.PhysicsManagerConstraint
    {
        // Fields
        public MagicaCloth.FixedNativeList<MagicaCloth.ClampPositionConstraint.GroupData> groupList;  // 0x20

        // Methods
        // RVA: 0x04D2FE20  token: 0x6000496
        public virtual System.Void Create() { }
        // RVA: 0x09F011D8  token: 0x6000497
        public virtual System.Void Release() { }
        // RVA: 0x09F00ED8  token: 0x6000498
        public System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam limitLength, Unity.Mathematics.float3 axisRatio, System.Single velocityInfluence) { }
        // RVA: 0x09F01220  token: 0x6000499
        public virtual System.Void RemoveTeam(System.Int32 teamId) { }
        // RVA: 0x09F01004  token: 0x600049A
        public System.Void ChangeParam(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam limitLength, Unity.Mathematics.float3 axisRatio, System.Single velocityInfluence) { }
        // RVA: 0x09F01304  token: 0x600049B
        public virtual Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x04D89110  token: 0x600049C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000070  // size: 0x48
    public class ClampRotationConstraint : MagicaCloth.PhysicsManagerConstraint
    {
        // Fields
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.ClampRotationConstraint.ClampRotationData> dataList;  // 0x20
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.ClampRotationConstraint.ClampRotationRootInfo> rootInfoList;  // 0x28
        public MagicaCloth.FixedNativeList<MagicaCloth.ClampRotationConstraint.GroupData> groupList;  // 0x30
        private MagicaCloth.FixedChunkNativeArray<System.Int32> rootTeamList;  // 0x38
        private MagicaCloth.FixedChunkNativeArray<System.Single> lengthBuffer;  // 0x40

        // Methods
        // RVA: 0x0442E1B0  token: 0x600049F
        public virtual System.Void Create() { }
        // RVA: 0x09F02118  token: 0x60004A0
        public virtual System.Void Release() { }
        // RVA: 0x09F01C70  token: 0x60004A1
        public System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam maxAngle, System.Single velocityInfluence, MagicaCloth.ClampRotationConstraint.ClampRotationData[] dataArray, MagicaCloth.ClampRotationConstraint.ClampRotationRootInfo[] rootInfoArray) { }
        // RVA: 0x09F021E4  token: 0x60004A2
        public virtual System.Void RemoveTeam(System.Int32 teamId) { }
        // RVA: 0x09F01F38  token: 0x60004A3
        public System.Void ChangeParam(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam maxAngle, System.Single velocityInfluence) { }
        // RVA: 0x09F023C0  token: 0x60004A4
        public virtual Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x04D89110  token: 0x60004A5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000075  // size: 0x20
    public class ColliderCollisionConstraint : MagicaCloth.PhysicsManagerConstraint
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60004A8
        public virtual System.Void Create() { }
        // RVA: 0x041E1670  token: 0x60004A9
        public virtual System.Void RemoveTeam(System.Int32 teamId) { }
        // RVA: 0x09F03184  token: 0x60004AA
        public System.Void ChangeParam(System.Int32 teamId, System.Boolean useCollision) { }
        // RVA: 0x041E1670  token: 0x60004AB
        public virtual System.Void Release() { }
        // RVA: 0x09F031D4  token: 0x60004AC
        public virtual Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x04D89110  token: 0x60004AD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000077  // size: 0x20
    public class ColliderExtrusionConstraint : MagicaCloth.PhysicsManagerConstraint
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60004B2
        public virtual System.Void Create() { }
        // RVA: 0x041E1670  token: 0x60004B3
        public virtual System.Void RemoveTeam(System.Int32 teamId) { }
        // RVA: 0x041E1670  token: 0x60004B4
        public virtual System.Void Release() { }
        // RVA: 0x09F036AC  token: 0x60004B5
        public virtual Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x04D89110  token: 0x60004B6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000079  // size: 0x48
    public class CompositeRotationConstraint : MagicaCloth.PhysicsManagerConstraint
    {
        // Fields
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.CompositeRotationConstraint.RotationData> dataList;  // 0x20
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.CompositeRotationConstraint.RootInfo> rootInfoList;  // 0x28
        public MagicaCloth.FixedNativeList<MagicaCloth.CompositeRotationConstraint.GroupData> groupList;  // 0x30
        private MagicaCloth.FixedChunkNativeArray<System.Int32> rootTeamList;  // 0x38
        private MagicaCloth.FixedChunkNativeArray<System.Single> lengthBuffer;  // 0x40

        // Methods
        // RVA: 0x0442E350  token: 0x60004B8
        public virtual System.Void Create() { }
        // RVA: 0x09F04F28  token: 0x60004B9
        public virtual System.Void Release() { }
        // RVA: 0x09F04ADC  token: 0x60004BA
        public System.Int32 AddGroup(System.Int32 teamId, System.Boolean useClamp, MagicaCloth.BezierParam maxAngle, System.Boolean useRestore, MagicaCloth.BezierParam restorePower, System.Single velocityInfluence, MagicaCloth.CompositeRotationConstraint.RotationData[] dataArray, MagicaCloth.CompositeRotationConstraint.RootInfo[] rootInfoArray) { }
        // RVA: 0x09F04FF4  token: 0x60004BB
        public virtual System.Void RemoveTeam(System.Int32 teamId) { }
        // RVA: 0x09F04D24  token: 0x60004BC
        public System.Void ChangeParam(System.Int32 teamId, System.Boolean useClamp, MagicaCloth.BezierParam maxAngle, System.Boolean useRestore, MagicaCloth.BezierParam restorePower, System.Single velocityInfluence) { }
        // RVA: 0x09F05194  token: 0x60004BD
        public virtual Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x04D89110  token: 0x60004BE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007E  // size: 0x48
    public class EdgeCollisionConstraint : MagicaCloth.PhysicsManagerConstraint
    {
        // Fields
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.EdgeCollisionConstraint.EdgeCollisionData> dataList;  // 0x20
        private MagicaCloth.FixedChunkNativeArray<System.Int16> groupIndexList;  // 0x28
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.ReferenceDataIndex> refDataList;  // 0x30
        private MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> writeBuffer;  // 0x38
        private MagicaCloth.FixedNativeList<MagicaCloth.EdgeCollisionConstraint.GroupData> groupList;  // 0x40

        // Methods
        // RVA: 0x09F070D4  token: 0x60004C2
        public virtual System.Void Create() { }
        // RVA: 0x09F07278  token: 0x60004C3
        public virtual System.Void Release() { }
        // RVA: 0x09F06C0C  token: 0x60004C4
        public System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, System.Single edgeRadius, MagicaCloth.EdgeCollisionConstraint.EdgeCollisionData[] dataArray, MagicaCloth.ReferenceDataIndex[] refDataArray, System.Int32 writeBufferCount) { }
        // RVA: 0x09F07344  token: 0x60004C5
        public virtual System.Void RemoveTeam(System.Int32 teamId) { }
        // RVA: 0x09F06F40  token: 0x60004C6
        public System.Void ChangeParam(System.Int32 teamId, System.Boolean active, System.Single edgeRadius) { }
        // RVA: 0x09F07590  token: 0x60004C7
        public virtual Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x04D89110  token: 0x60004C8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000083  // size: 0x40
    public class PenetrationConstraint : MagicaCloth.PhysicsManagerConstraint
    {
        // Fields
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.PenetrationConstraint.PenetrationData> dataList;  // 0x20
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.ReferenceDataIndex> refDataList;  // 0x28
        private MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> bonePenetrationDataList;  // 0x30
        public MagicaCloth.FixedNativeList<MagicaCloth.PenetrationConstraint.GroupData> groupList;  // 0x38

        // Methods
        // RVA: 0x0442E4F0  token: 0x60004CE
        public virtual System.Void Create() { }
        // RVA: 0x09F084C4  token: 0x60004CF
        public virtual System.Void Release() { }
        // RVA: 0x09F07FFC  token: 0x60004D0
        public System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, MagicaCloth.ClothParams.PenetrationMode mode, MagicaCloth.BezierParam distance, MagicaCloth.BezierParam radius, System.Single maxDepth, MagicaCloth.PenetrationConstraint.PenetrationData[] moveLimitDataList, MagicaCloth.ReferenceDataIndex[] refDataArray, Unity.Mathematics.float3[] bonePenetrationDataArray) { }
        // RVA: 0x09F0856C  token: 0x60004D1
        public virtual System.Void RemoveTeam(System.Int32 teamId) { }
        // RVA: 0x09F082CC  token: 0x60004D2
        public System.Void ChangeParam(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam distance, MagicaCloth.BezierParam radius, System.Single maxDepth) { }
        // RVA: 0x09F086F4  token: 0x60004D3
        public virtual Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x04D89110  token: 0x60004D4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000087  // size: 0x38
    public class RestoreDistanceConstraint : MagicaCloth.PhysicsManagerConstraint
    {
        // Fields
        public static System.Int32 StructType;  // const
        public static System.Int32 BendType;  // const
        public static System.Int32 NearType;  // const
        public static System.Int32 TypeCount;  // const
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.RestoreDistanceConstraint.RestoreDistanceData>[] dataList;  // 0x20
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.ReferenceDataIndex>[] refDataList;  // 0x28
        public MagicaCloth.FixedNativeList<MagicaCloth.RestoreDistanceConstraint.RestoreDistanceGroupData> groupList;  // 0x30

        // Methods
        // RVA: 0x0442E870  token: 0x60004D9
        public virtual System.Void Create() { }
        // RVA: 0x09F0A2B8  token: 0x60004DA
        public virtual System.Void Release() { }
        // RVA: 0x09F09ACC  token: 0x60004DB
        public System.Int32 AddGroup(System.Int32 teamId, MagicaCloth.BezierParam mass, System.Single velocityInfluence, MagicaCloth.BezierParam structStiffness, MagicaCloth.RestoreDistanceConstraint.RestoreDistanceData[] structDataArray, MagicaCloth.ReferenceDataIndex[] structRefDataArray, System.Boolean useBend, MagicaCloth.BezierParam bendStiffness, MagicaCloth.RestoreDistanceConstraint.RestoreDistanceData[] bendDataArray, MagicaCloth.ReferenceDataIndex[] bendRefDataArray, System.Boolean useNear, MagicaCloth.BezierParam nearStiffness, MagicaCloth.RestoreDistanceConstraint.RestoreDistanceData[] nearDataArray, MagicaCloth.ReferenceDataIndex[] nearRefDataArray) { }
        // RVA: 0x09F0A39C  token: 0x60004DC
        public virtual System.Void RemoveTeam(System.Int32 teamId) { }
        // RVA: 0x09F0A01C  token: 0x60004DD
        public System.Void ChangeParam(System.Int32 teamId, MagicaCloth.BezierParam mass, System.Single velocityInfluence, MagicaCloth.BezierParam structStiffness, System.Boolean useBend, MagicaCloth.BezierParam bendStiffness, System.Boolean useNear, MagicaCloth.BezierParam nearStiffness) { }
        // RVA: 0x02B2E2D0  token: 0x60004DE
        public virtual System.Int32 GetIterationCount() { }
        // RVA: 0x09F0A5F0  token: 0x60004DF
        public virtual Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x04D89110  token: 0x60004E0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008B  // size: 0x38
    public class RestoreRotationConstraint : MagicaCloth.PhysicsManagerConstraint
    {
        // Fields
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.RestoreRotationConstraint.RotationData> dataList;  // 0x20
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.ReferenceDataIndex> refDataList;  // 0x28
        public MagicaCloth.FixedNativeList<MagicaCloth.RestoreRotationConstraint.GroupData> groupList;  // 0x30

        // Methods
        // RVA: 0x0442E650  token: 0x60004E7
        public virtual System.Void Create() { }
        // RVA: 0x09F0AF58  token: 0x60004E8
        public virtual System.Void Release() { }
        // RVA: 0x09F0AAFC  token: 0x60004E9
        public System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam power, System.Single velocityInfluence, MagicaCloth.RestoreRotationConstraint.RotationData[] dataArray, MagicaCloth.ReferenceDataIndex[] refDataArray) { }
        // RVA: 0x09F0AFE0  token: 0x60004EA
        public virtual System.Void RemoveTeam(System.Int32 teamId) { }
        // RVA: 0x09F0AD78  token: 0x60004EB
        public System.Void ChangeParam(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam power, System.Single velocityInfluence) { }
        // RVA: 0x09F0B160  token: 0x60004EC
        public virtual Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x04D89110  token: 0x60004ED
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008F  // size: 0x28
    public class SpringConstraint : MagicaCloth.PhysicsManagerConstraint
    {
        // Fields
        public MagicaCloth.FixedNativeList<MagicaCloth.SpringConstraint.GroupData> groupList;  // 0x20

        // Methods
        // RVA: 0x04D2DE20  token: 0x60004F0
        public virtual System.Void Create() { }
        // RVA: 0x09F0D550  token: 0x60004F1
        public virtual System.Void Release() { }
        // RVA: 0x09F0D324  token: 0x60004F2
        public System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, System.Single spring) { }
        // RVA: 0x09F0D598  token: 0x60004F3
        public virtual System.Void RemoveTeam(System.Int32 teamId) { }
        // RVA: 0x09F0D3F0  token: 0x60004F4
        public System.Void ChangeParam(System.Int32 teamId, System.Boolean active, System.Single spring) { }
        // RVA: 0x09F0D67C  token: 0x60004F5
        public virtual Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x04D89110  token: 0x60004F6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000092  // size: 0x48
    public class TriangleBendConstraint : MagicaCloth.PhysicsManagerConstraint
    {
        // Fields
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.TriangleBendConstraint.TriangleBendData> dataList;  // 0x20
        private MagicaCloth.FixedChunkNativeArray<System.Int16> groupIndexList;  // 0x28
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.ReferenceDataIndex> refDataList;  // 0x30
        private MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> writeBuffer;  // 0x38
        private MagicaCloth.FixedNativeList<MagicaCloth.TriangleBendConstraint.TriangleBendGroupData> groupList;  // 0x40

        // Methods
        // RVA: 0x0442EA50  token: 0x60004F8
        public virtual System.Void Create() { }
        // RVA: 0x09F0ED88  token: 0x60004F9
        public virtual System.Void Release() { }
        // RVA: 0x09F0E824  token: 0x60004FA
        public System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, MagicaCloth.ClothParams.Algorithm algorithm, MagicaCloth.BezierParam stiffness, MagicaCloth.TriangleBendConstraint.TriangleBendData[] dataArray, MagicaCloth.ReferenceDataIndex[] refDataArray, System.Int32 writeBufferCount) { }
        // RVA: 0x09F0EE54  token: 0x60004FB
        public virtual System.Void RemoveTeam(System.Int32 teamId) { }
        // RVA: 0x09F0EB8C  token: 0x60004FC
        public System.Void ChangeParam(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam stiffness) { }
        // RVA: 0x09F0F030  token: 0x60004FD
        public virtual Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x04D89110  token: 0x60004FE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000097  // size: 0x38
    public class TwistConstraint : MagicaCloth.PhysicsManagerConstraint
    {
        // Fields
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.TwistConstraint.TwistData> dataList;  // 0x20
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.ReferenceDataIndex> refDataList;  // 0x28
        public MagicaCloth.FixedNativeList<MagicaCloth.TwistConstraint.GroupData> groupList;  // 0x30

        // Methods
        // RVA: 0x0442E760  token: 0x6000503
        public virtual System.Void Create() { }
        // RVA: 0x09F0FB90  token: 0x6000504
        public virtual System.Void Release() { }
        // RVA: 0x09F0F828  token: 0x6000505
        public System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, System.Single recoveryPower, MagicaCloth.TwistConstraint.TwistData[] dataArray, MagicaCloth.ReferenceDataIndex[] refArray) { }
        // RVA: 0x09F0FC18  token: 0x6000506
        public virtual System.Void RemoveTeam(System.Int32 teamId) { }
        // RVA: 0x09F0FA10  token: 0x6000507
        public System.Void ChangeParam(System.Int32 teamId, System.Boolean active, System.Single recoveryPower) { }
        // RVA: 0x09F0FD98  token: 0x6000508
        public virtual Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x04D89110  token: 0x6000509
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200009B  // size: 0x48
    public class VolumeConstraint : MagicaCloth.PhysicsManagerConstraint
    {
        // Fields
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.VolumeConstraint.VolumeData> dataList;  // 0x20
        private MagicaCloth.FixedChunkNativeArray<System.Int16> groupIndexList;  // 0x28
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.ReferenceDataIndex> refDataList;  // 0x30
        private MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> writeBuffer;  // 0x38
        private MagicaCloth.FixedNativeList<MagicaCloth.VolumeConstraint.GroupData> groupList;  // 0x40

        // Methods
        // RVA: 0x09F11C18  token: 0x600050C
        public virtual System.Void Create() { }
        // RVA: 0x09F11DBC  token: 0x600050D
        public virtual System.Void Release() { }
        // RVA: 0x09F11690  token: 0x600050E
        public System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam stretchStiffness, MagicaCloth.BezierParam shearStiffness, MagicaCloth.VolumeConstraint.VolumeData[] dataArray, MagicaCloth.ReferenceDataIndex[] refDataArray, System.Int32 writeBufferCount) { }
        // RVA: 0x09F11E88  token: 0x600050F
        public virtual System.Void RemoveTeam(System.Int32 teamId) { }
        // RVA: 0x09F11A04  token: 0x6000510
        public System.Void ChangeParam(System.Int32 teamId, System.Boolean active, MagicaCloth.BezierParam stretchStiffness, MagicaCloth.BezierParam shearStiffness) { }
        // RVA: 0x02B2E2D0  token: 0x6000511
        public virtual System.Int32 GetIterationCount() { }
        // RVA: 0x09F120C4  token: 0x6000512
        public virtual Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x04D89110  token: 0x6000513
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A0  // size: 0x20
    public sealed struct CurveParam
    {
        // Fields
        public System.Single sval;  // 0x10
        public System.Single eval;  // 0x14
        public System.Single cval;  // 0x18
        public System.Int32 useCurve;  // 0x1c

        // Methods
        // RVA: 0x04DA3640  token: 0x6000517
        public System.Void .ctor(System.Single value) { }
        // RVA: 0x04DA3650  token: 0x6000518
        public System.Void .ctor(System.Single svalue, System.Single evalue) { }
        // RVA: 0x09F12A90  token: 0x6000519
        public System.Void .ctor(MagicaCloth.BezierParam bezier) { }
        // RVA: 0x09F129F0  token: 0x600051A
        public System.Void Setup(MagicaCloth.BezierParam bezier) { }
        // RVA: 0x09F12988  token: 0x600051B
        public System.Single Evaluate(System.Single t) { }

    }

    // TypeToken: 0x20000A1  // size: 0x18
    public abstract class BaseComponent : UnityEngine.MonoBehaviour
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600051C
        public virtual MagicaCloth.ComponentType GetComponentType() { }
        // RVA: 0x05393520  token: 0x600051D
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000A2  // size: 0x14
    public sealed struct ComponentType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MagicaCloth.ComponentType None;  // const
        public static MagicaCloth.ComponentType SphereCollider;  // const
        public static MagicaCloth.ComponentType CapsuleCollider;  // const
        public static MagicaCloth.ComponentType PlaneCollider;  // const
        public static MagicaCloth.ComponentType DirectionalWind;  // const
        public static MagicaCloth.ComponentType AreaWind;  // const
        public static MagicaCloth.ComponentType RenderDeformer;  // const
        public static MagicaCloth.ComponentType VirtualDeformer;  // const
        public static MagicaCloth.ComponentType BoneCloth;  // const
        public static MagicaCloth.ComponentType BoneSpring;  // const
        public static MagicaCloth.ComponentType MeshCloth;  // const
        public static MagicaCloth.ComponentType MeshSpring;  // const
        public static MagicaCloth.ComponentType Avatar;  // const
        public static MagicaCloth.ComponentType AvatarParts;  // const

    }

    // TypeToken: 0x20000A3  // size: 0x18
    public abstract class PhysicsManagerAccess : System.IDisposable
    {
        // Fields
        protected MagicaCloth.MagicaPhysicsManager manager;  // 0x10

        // Properties
        MagicaCloth.UpdateTimeManager UpdateTime { get; /* RVA: 0x0861D7BC */ }
        MagicaCloth.PhysicsManagerParticleData Particle { get; /* RVA: 0x09ED14C0 */ }
        MagicaCloth.PhysicsManagerBoneData Bone { get; /* RVA: 0x03233920 */ }
        MagicaCloth.PhysicsManagerMeshData Mesh { get; /* RVA: 0x032353B0 */ }
        MagicaCloth.PhysicsManagerTeamData Team { get; /* RVA: 0x03235330 */ }
        MagicaCloth.PhysicsManagerWindData Wind { get; /* RVA: 0x09EE69FC */ }
        MagicaCloth.PhysicsManagerComponent Component { get; /* RVA: 0x09F13E98 */ }
        MagicaCloth.PhysicsManagerCompute Compute { get; /* RVA: 0x09F13EB8 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000526
        public System.Void SetParent(MagicaCloth.MagicaPhysicsManager manager) { }
        // RVA: -1  // abstract  token: 0x6000527
        public virtual System.Void Create() { }
        // RVA: -1  // abstract  token: 0x6000528
        public virtual System.Void Dispose() { }
        // RVA: 0x041E1670  token: 0x6000529
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000A4  // size: 0xD0
    public class PhysicsManagerBoneData : MagicaCloth.PhysicsManagerAccess
    {
        // Fields
        public static System.Byte Flag_Reset;  // const
        public static System.Byte Flag_Restore;  // const
        public static System.Byte Flag_Write;  // const
        public MagicaCloth.FixedTransformAccessArray boneList;  // 0x18
        public MagicaCloth.FixedNativeList<System.Byte> boneFlagList;  // 0x20
        public MagicaCloth.FixedNativeList<Unity.Mathematics.float3> bonePosList;  // 0x28
        public MagicaCloth.FixedNativeList<Unity.Mathematics.quaternion> boneRotList;  // 0x30
        public MagicaCloth.FixedNativeList<Unity.Mathematics.float3> boneSclList;  // 0x38
        public MagicaCloth.FixedNativeList<System.Int32> boneParentIndexList;  // 0x40
        public MagicaCloth.FixedNativeList<Unity.Mathematics.float3> basePosList;  // 0x48
        public MagicaCloth.FixedNativeList<Unity.Mathematics.quaternion> baseRotList;  // 0x50
        public MagicaCloth.FixedNativeList<System.Int16> boneUnityPhysicsList;  // 0x58
        public MagicaCloth.FixedNativeList<Unity.Mathematics.float3> futurePosList;  // 0x60
        public MagicaCloth.FixedNativeList<Unity.Mathematics.quaternion> futureRotList;  // 0x68
        public MagicaCloth.FixedTransformAccessArray restoreBoneList;  // 0x70
        public MagicaCloth.FixedNativeList<Unity.Mathematics.float3> restoreBoneLocalPosList;  // 0x78
        public MagicaCloth.FixedNativeList<Unity.Mathematics.quaternion> restoreBoneLocalRotList;  // 0x80
        public MagicaCloth.FixedNativeList<System.Int32> restoreBoneIndexList;  // 0x88
        public MagicaCloth.FixedTransformAccessArray writeBoneList;  // 0x90
        public MagicaCloth.FixedNativeList<System.Int32> writeBoneIndexList;  // 0x98
        public MagicaCloth.ExNativeMultiHashMap<System.Int32,System.Int32> writeBoneParticleIndexMap;  // 0xa0
        private System.Collections.Generic.Dictionary<System.Int32,System.Int32> boneToWriteIndexDict;  // 0xa8
        public MagicaCloth.FixedNativeList<Unity.Mathematics.float3> writeBonePosList;  // 0xb0
        public MagicaCloth.FixedNativeList<Unity.Mathematics.quaternion> writeBoneRotList;  // 0xb8
        private System.Boolean <hasBoneChanged>k__BackingField;  // 0xc0
        private UnityEngine.Profiling.CustomSampler <SamplerReadBoneScale>k__BackingField;  // 0xc8

        // Properties
        System.Boolean hasBoneChanged { get; /* RVA: 0x04D91920 */ set; /* RVA: 0x04D91990 */ }
        UnityEngine.Profiling.CustomSampler SamplerReadBoneScale { get; /* RVA: 0x04D8D1D0 */ set; /* RVA: 0x06AC5B30 */ }
        System.Int32 RestoreBoneCount { get; /* RVA: 0x09F155E8 */ }
        System.Int32 ReadBoneCount { get; /* RVA: 0x09F155C8 */ }
        System.Int32 WriteBoneCount { get; /* RVA: 0x03233870 */ }

        // Methods
        // RVA: 0x03E876D0  token: 0x600052E
        public virtual System.Void Create() { }
        // RVA: 0x09F14958  token: 0x600052F
        public virtual System.Void Dispose() { }
        // RVA: 0x09F14348  token: 0x6000530
        public System.Int32 AddRestoreBone(UnityEngine.Transform target, Unity.Mathematics.float3 lpos, Unity.Mathematics.quaternion lrot, System.Int32 boneIndex) { }
        // RVA: 0x09F1533C  token: 0x6000531
        public System.Void RemoveRestoreBone(System.Int32 restoreBoneIndex) { }
        // RVA: 0x09F13ED8  token: 0x6000533
        public System.Int32 AddBone(UnityEngine.Transform target, System.Int32 pindex, System.Boolean addParent) { }
        // RVA: 0x09F1508C  token: 0x6000534
        public System.Boolean RemoveBone(System.Int32 boneIndex, System.Int32 pindex) { }
        // RVA: 0x09F1443C  token: 0x6000535
        public System.Void ChangeUnityPhysicsCount(System.Int32 boneIndex, System.Boolean sw) { }
        // RVA: 0x09F15560  token: 0x6000536
        public System.Void ResetFuturePrediction(System.Int32 boneIndex) { }
        // RVA: 0x09F153D8  token: 0x6000539
        public System.Void ResetBoneFromTransform(System.Boolean fixedUpdate) { }
        // RVA: 0x09F14BB0  token: 0x600053A
        public System.Void ReadBoneFromTransform() { }
        // RVA: 0x09F144B4  token: 0x600053B
        public System.Void ConvertWorldToLocal() { }
        // RVA: 0x03233800  token: 0x600053C
        public System.Void WriteBoneToTransform(System.Int32 bufferIndex) { }
        // RVA: 0x09F146A4  token: 0x600053D
        public System.Void CopyBoneBuffer() { }
        // RVA: 0x046D7630  token: 0x600053E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000AD  // size: 0x28
    public class PhysicsManagerComponent : MagicaCloth.PhysicsManagerAccess
    {
        // Fields
        private readonly System.Collections.Generic.HashSet<MagicaCloth.CoreComponent> componentSet;  // 0x18
        private System.Collections.Generic.HashSet<MagicaCloth.ParticleComponent> dataUpdateParticleSet;  // 0x20

        // Properties
        System.Int32 ComponentCount { get; /* RVA: 0x09F159FC */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000547
        public virtual System.Void Create() { }
        // RVA: 0x041E1670  token: 0x6000548
        public virtual System.Void Dispose() { }
        // RVA: 0x09F1577C  token: 0x600054A
        public System.Collections.Generic.List<MagicaCloth.CoreComponent> GetComponentList() { }
        // RVA: 0x09F1562C  token: 0x600054B
        public System.Void ComponentAction(System.Action<MagicaCloth.CoreComponent> act) { }
        // RVA: 0x09F15898  token: 0x600054C
        public System.Void UpdateComponentStatus() { }
        // RVA: 0x09F15608  token: 0x600054D
        public System.Void AddComponent(MagicaCloth.CoreComponent comp) { }
        // RVA: 0x09F157F0  token: 0x600054E
        public System.Void RemoveComponent(MagicaCloth.CoreComponent comp) { }
        // RVA: 0x09F15844  token: 0x600054F
        private System.Void ReserveDataUpdateParticleComponent(MagicaCloth.ParticleComponent comp) { }
        // RVA: 0x03233710  token: 0x6000550
        private System.Void DataUpdateParticleComponent() { }
        // RVA: 0x046D7730  token: 0x6000551
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000AE  // size: 0xF8
    public class PhysicsManagerCompute : MagicaCloth.PhysicsManagerAccess
    {
        // Fields
        private System.Int32 solverIteration;  // 0x18
        private System.Collections.Generic.List<MagicaCloth.PhysicsManagerConstraint> constraints;  // 0x20
        private MagicaCloth.ClampPositionConstraint <ClampPosition>k__BackingField;  // 0x28
        private MagicaCloth.ClampDistanceConstraint <ClampDistance>k__BackingField;  // 0x30
        private MagicaCloth.ClampRotationConstraint <ClampRotation>k__BackingField;  // 0x38
        private MagicaCloth.SpringConstraint <Spring>k__BackingField;  // 0x40
        private MagicaCloth.RestoreDistanceConstraint <RestoreDistance>k__BackingField;  // 0x48
        private MagicaCloth.RestoreRotationConstraint <RestoreRotation>k__BackingField;  // 0x50
        private MagicaCloth.TriangleBendConstraint <TriangleBend>k__BackingField;  // 0x58
        private MagicaCloth.ColliderCollisionConstraint <Collision>k__BackingField;  // 0x60
        private MagicaCloth.PenetrationConstraint <Penetration>k__BackingField;  // 0x68
        private MagicaCloth.ColliderExtrusionConstraint <ColliderExtrusion>k__BackingField;  // 0x70
        private MagicaCloth.TwistConstraint <Twist>k__BackingField;  // 0x78
        private MagicaCloth.CompositeRotationConstraint <CompositeRotation>k__BackingField;  // 0x80
        private System.Collections.Generic.List<MagicaCloth.PhysicsManagerWorker> workers;  // 0x88
        private MagicaCloth.RenderMeshWorker <RenderMeshWorker>k__BackingField;  // 0x90
        private MagicaCloth.VirtualMeshWorker <VirtualMeshWorker>k__BackingField;  // 0x98
        private MagicaCloth.MeshParticleWorker <MeshParticleWorker>k__BackingField;  // 0xa0
        private MagicaCloth.SpringMeshWorker <SpringMeshWorker>k__BackingField;  // 0xa8
        private MagicaCloth.AdjustRotationWorker <AdjustRotationWorker>k__BackingField;  // 0xb0
        private MagicaCloth.LineWorker <LineWorker>k__BackingField;  // 0xb8
        private MagicaCloth.TriangleWorker <TriangleWorker>k__BackingField;  // 0xc0
        private MagicaCloth.BaseSkinningWorker <BaseSkinningWorker>k__BackingField;  // 0xc8
        private Unity.Jobs.JobHandle jobHandle;  // 0xd0
        private System.Boolean runMasterJob;  // 0xe0
        private System.Int32 swapIndex;  // 0xe4
        private UnityEngine.Profiling.CustomSampler <SamplerCalcMesh>k__BackingField;  // 0xe8
        private UnityEngine.Profiling.CustomSampler <SamplerWriteMesh>k__BackingField;  // 0xf0

        // Properties
        MagicaCloth.ClampPositionConstraint ClampPosition { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        MagicaCloth.ClampDistanceConstraint ClampDistance { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        MagicaCloth.ClampRotationConstraint ClampRotation { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }
        MagicaCloth.SpringConstraint Spring { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        MagicaCloth.RestoreDistanceConstraint RestoreDistance { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }
        MagicaCloth.RestoreRotationConstraint RestoreRotation { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x02FDAEA0 */ }
        MagicaCloth.TriangleBendConstraint TriangleBend { get; /* RVA: 0x02E56440 */ set; /* RVA: 0x036E5590 */ }
        MagicaCloth.ColliderCollisionConstraint Collision { get; /* RVA: 0x04D86280 */ set; /* RVA: 0x063CC164 */ }
        MagicaCloth.PenetrationConstraint Penetration { get; /* RVA: 0x04D86230 */ set; /* RVA: 0x0485F170 */ }
        MagicaCloth.ColliderExtrusionConstraint ColliderExtrusion { get; /* RVA: 0x04D862B0 */ set; /* RVA: 0x063AD760 */ }
        MagicaCloth.TwistConstraint Twist { get; /* RVA: 0x04D862A0 */ set; /* RVA: 0x06402320 */ }
        MagicaCloth.CompositeRotationConstraint CompositeRotation { get; /* RVA: 0x04D86260 */ set; /* RVA: 0x04CD9970 */ }
        MagicaCloth.RenderMeshWorker RenderMeshWorker { get; /* RVA: 0x04D86220 */ set; /* RVA: 0x06402330 */ }
        MagicaCloth.VirtualMeshWorker VirtualMeshWorker { get; /* RVA: 0x04D85EE0 */ set; /* RVA: 0x06352D0C */ }
        MagicaCloth.MeshParticleWorker MeshParticleWorker { get; /* RVA: 0x04D86210 */ set; /* RVA: 0x05391334 */ }
        MagicaCloth.SpringMeshWorker SpringMeshWorker { get; /* RVA: 0x04D86290 */ set; /* RVA: 0x04D84CC0 */ }
        MagicaCloth.AdjustRotationWorker AdjustRotationWorker { get; /* RVA: 0x04D86250 */ set; /* RVA: 0x04D84CE0 */ }
        MagicaCloth.LineWorker LineWorker { get; /* RVA: 0x04D85EF0 */ set; /* RVA: 0x06AC5AF4 */ }
        MagicaCloth.TriangleWorker TriangleWorker { get; /* RVA: 0x04D892F0 */ set; /* RVA: 0x06AC5ACC */ }
        MagicaCloth.BaseSkinningWorker BaseSkinningWorker { get; /* RVA: 0x04D8D1D0 */ set; /* RVA: 0x06AC5B30 */ }
        UnityEngine.Profiling.CustomSampler SamplerCalcMesh { get; /* RVA: 0x04D88590 */ set; /* RVA: 0x06AC5B1C */ }
        UnityEngine.Profiling.CustomSampler SamplerWriteMesh { get; /* RVA: 0x04D8D1A0 */ set; /* RVA: 0x06AC5AE0 */ }
        Unity.Jobs.JobHandle MasterJob { get; /* RVA: 0x04D9E7C0 */ set; /* RVA: 0x04DA12C0 */ }
        System.Int32 SwapIndex { get; /* RVA: 0x04DA3670 */ }

        // Methods
        // RVA: 0x03B92860  token: 0x600057E
        public virtual System.Void Create() { }
        // RVA: 0x09F15A3C  token: 0x600057F
        public virtual System.Void Dispose() { }
        // RVA: 0x09F16098  token: 0x6000580
        public System.Void RemoveTeam(System.Int32 teamId) { }
        // RVA: 0x09F17034  token: 0x6000581
        private System.Void UpdateRestoreBone(MagicaCloth.PhysicsTeam.TeamUpdateMode updateMode) { }
        // RVA: 0x03233CB0  token: 0x6000582
        private System.Void UpdateReadBone() { }
        // RVA: 0x03233D00  token: 0x6000583
        private System.Void UpdateTeamAlways() { }
        // RVA: 0x03233D40  token: 0x6000584
        private System.Void UpdateStartSimulation(MagicaCloth.UpdateTimeManager update) { }
        // RVA: 0x03233C10  token: 0x6000585
        private System.Void UpdateCompleteSimulation() { }
        // RVA: 0x032338C0  token: 0x6000586
        private System.Void UpdateWriteBone() { }
        // RVA: 0x032352A0  token: 0x6000587
        private System.Void MeshCalculation() { }
        // RVA: 0x03233BA0  token: 0x6000588
        private System.Void NormalWritingMesh() { }
        // RVA: 0x09F16FB4  token: 0x6000589
        private System.Void UpdateReadWriteBone() { }
        // RVA: 0x09F17204  token: 0x600058A
        private System.Void UpdateSyncBuffer() { }
        // RVA: 0x09F17088  token: 0x600058B
        private System.Void UpdateSwapBuffer() { }
        // RVA: 0x04DA3660  token: 0x600058E
        public System.Void InitJob() { }
        // RVA: 0x09F162C4  token: 0x600058F
        public System.Void ScheduleJob() { }
        // RVA: 0x035BC9F0  token: 0x6000590
        public System.Void CompleteJob() { }
        // RVA: 0x09F162CC  token: 0x6000592
        private System.Void UpdatePhysics(System.Int32 updateCount, System.Int32 runCount, System.Single updatePower, System.Single updateDeltaTime) { }
        // RVA: 0x09F15C60  token: 0x6000593
        private System.Void PostUpdatePhysics(System.Single updateDeltaTime) { }
        // RVA: 0x09F17330  token: 0x6000594
        private System.Void WarmupWorker() { }
        // RVA: 0x046D77F0  token: 0x6000595
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000B2  // size: 0x20
    public abstract class PhysicsManagerConstraint
    {
        // Fields
        public System.Int32 iteration;  // 0x10
        private MagicaCloth.MagicaPhysicsManager <Manager>k__BackingField;  // 0x18

        // Properties
        MagicaCloth.MagicaPhysicsManager Manager { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x03B93E60  token: 0x600059C
        public System.Void Init(MagicaCloth.MagicaPhysicsManager manager) { }
        // RVA: -1  // abstract  token: 0x600059D
        public virtual System.Void Create() { }
        // RVA: -1  // abstract  token: 0x600059E
        public virtual System.Void RemoveTeam(System.Int32 teamId) { }
        // RVA: -1  // abstract  token: 0x600059F
        public virtual System.Void Release() { }
        // RVA: 0x02B2E2D0  token: 0x60005A0
        public virtual System.Int32 GetIterationCount() { }
        // RVA: -1  // abstract  token: 0x60005A1
        public virtual Unity.Jobs.JobHandle SolverConstraint(System.Int32 runCount, System.Single dtime, System.Single updatePower, System.Int32 iteration, Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x04D89110  token: 0x60005A2
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000B3  // size: 0x188
    public class PhysicsManagerMeshData : MagicaCloth.PhysicsManagerAccess
    {
        // Fields
        public static System.UInt32 MeshFlag_Active;  // const
        public static System.UInt32 MeshFlag_Skinning;  // const
        public static System.UInt32 Meshflag_CalcNormal;  // const
        public static System.UInt32 Meshflag_CalcTangent;  // const
        public static System.UInt32 Meshflag_Pause;  // const
        public static System.UInt32 MeshFlag_ExistNormals;  // const
        public static System.UInt32 MeshFlag_ExistTangents;  // const
        public static System.UInt32 MeshFlag_ExistWeights;  // const
        public static System.UInt32 MeshFlag_UpdateUseVertexFront;  // const
        public static System.UInt32 MeshFlag_UpdateUseVertexBack;  // const
        public static System.UInt32 MeshFlag_FasterWrite;  // const
        public static System.UInt32 MeshFlag_MeshLink;  // const
        public MagicaCloth.FixedNativeList<MagicaCloth.PhysicsManagerMeshData.SharedVirtualMeshInfo> sharedVirtualMeshInfoList;  // 0x18
        public System.Collections.Generic.Dictionary<System.Int32,System.Int32> sharedVirtualMeshIdToIndexDict;  // 0x20
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float2> sharedVirtualUvList;  // 0x28
        public MagicaCloth.FixedChunkNativeArray<System.UInt32> sharedVirtualVertexInfoList;  // 0x30
        public MagicaCloth.FixedChunkNativeArray<MagicaCloth.MeshData.VertexWeight> sharedVirtualWeightList;  // 0x38
        public MagicaCloth.FixedChunkNativeArray<System.Int32> sharedVirtualTriangleList;  // 0x40
        public MagicaCloth.FixedChunkNativeArray<System.UInt32> sharedVirtualVertexToTriangleInfoList;  // 0x48
        public MagicaCloth.FixedChunkNativeArray<System.Int32> sharedVirtualVertexToTriangleIndexList;  // 0x50
        public static System.Byte VirtualVertexFlag_Use;  // const
        public MagicaCloth.FixedNativeList<MagicaCloth.PhysicsManagerMeshData.VirtualMeshInfo> virtualMeshInfoList;  // 0x58
        public MagicaCloth.FixedChunkNativeArray<System.Int16> virtualVertexMeshIndexList;  // 0x60
        public MagicaCloth.FixedChunkNativeArray<System.Byte> virtualVertexUseList;  // 0x68
        public MagicaCloth.FixedChunkNativeArray<System.Byte> virtualVertexFixList;  // 0x70
        public MagicaCloth.FixedChunkNativeArray<System.Byte> virtualVertexFlagList;  // 0x78
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> virtualPosList;  // 0x80
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.quaternion> virtualRotList;  // 0x88
        public MagicaCloth.FixedChunkNativeArray<System.Int32> virtualTransformIndexList;  // 0x90
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> virtualTriangleNormalList;  // 0x98
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> virtualTriangleTangentList;  // 0xa0
        public MagicaCloth.FixedChunkNativeArray<System.UInt16> virtualTriangleMeshIndexList;  // 0xa8
        public MagicaCloth.FixedNativeList<MagicaCloth.PhysicsManagerMeshData.SharedChildMeshInfo> sharedChildMeshInfoList;  // 0xb0
        public System.Collections.Generic.Dictionary<System.Int64,System.Int32> sharedChildMeshIdToSharedVirtualMeshIndexDict;  // 0xb8
        public MagicaCloth.FixedChunkNativeArray<System.UInt32> sharedChildVertexInfoList;  // 0xc0
        public MagicaCloth.FixedChunkNativeArray<MagicaCloth.MeshData.VertexWeight> sharedChildWeightList;  // 0xc8
        public MagicaCloth.FixedNativeList<MagicaCloth.PhysicsManagerMeshData.SharedRenderMeshInfo> sharedRenderMeshInfoList;  // 0xd0
        public System.Collections.Generic.Dictionary<System.Int32,System.Int32> sharedRenderMeshIdToIndexDict;  // 0xd8
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> sharedRenderVertices;  // 0xe0
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> sharedRenderNormals;  // 0xe8
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float4> sharedRenderTangents;  // 0xf0
        public MagicaCloth.FixedChunkNativeArray<System.Byte> sharedBonesPerVertexList;  // 0xf8
        public MagicaCloth.FixedChunkNativeArray<System.Int32> sharedBonesPerVertexStartList;  // 0x100
        public MagicaCloth.FixedChunkNativeArray<UnityEngine.BoneWeight1> sharedBoneWeightList;  // 0x108
        public static System.UInt32 RenderVertexFlag_Use;  // const
        public static System.Int32 MaxRenderMeshLinkCount;  // const
        public MagicaCloth.FixedNativeList<MagicaCloth.PhysicsManagerMeshData.RenderMeshInfo> renderMeshInfoList;  // 0x110
        public static System.UInt32 RenderStateFlag_Use;  // const
        public static System.UInt32 RenderStateFlag_ExistNormal;  // const
        public static System.UInt32 RenderStateFlag_ExistTangent;  // const
        public static System.UInt32 RenderStateFlag_FasterWrite;  // const
        public static System.UInt32 RenderStateFlag_DelayedCalculated;  // const
        public System.Collections.Generic.Dictionary<System.Int32,MagicaCloth.PhysicsManagerMeshData.RenderMeshState> renderMeshStateDict;  // 0x118
        public MagicaCloth.FixedChunkNativeArray<System.UInt32> renderVertexFlagList;  // 0x120
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> renderPosList;  // 0x128
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> renderNormalList;  // 0x130
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float4> renderTangentList;  // 0x138
        public MagicaCloth.FixedChunkNativeArray<UnityEngine.BoneWeight1> renderBoneWeightList;  // 0x140
        private MagicaCloth.DoubleComputeBuffer<Unity.Mathematics.float3> renderPosBuffer;  // 0x148
        private MagicaCloth.DoubleComputeBuffer<Unity.Mathematics.float3> renderNormalBuffer;  // 0x150
        private UnityEngine.ComputeBuffer emptyByteAddressBuffer;  // 0x158
        private System.Boolean isBeginWrite;  // 0x160
        private System.Collections.Generic.HashSet<MagicaCloth.BaseMeshDeformer> renderMeshSet;  // 0x168
        private System.Collections.Generic.List<MagicaCloth.RenderMeshDeformer> normalWriteList;  // 0x170
        private System.Collections.Generic.List<MagicaCloth.RenderMeshDeformer> fasterWritePositionList;  // 0x178
        private System.Collections.Generic.List<MagicaCloth.RenderMeshDeformer> fasterWritePositionNormalList;  // 0x180

        // Properties
        System.Int32 SharedVirtualMeshCount { get; /* RVA: 0x09F1DC8C */ }
        System.Int32 VirtualMeshCount { get; /* RVA: 0x03233AC0 */ }
        System.Int32 VirtualMeshVertexCount { get; /* RVA: 0x09F1DE68 */ }
        System.Int32 VirtualMeshTriangleCount { get; /* RVA: 0x09F1DD78 */ }
        System.Int32 VirtualMeshVertexUseCount { get; /* RVA: 0x09F1DEE4 */ }
        System.Int32 VirtualMeshUseCount { get; /* RVA: 0x09F1DDF4 */ }
        System.Int32 VirtualMeshPauseCount { get; /* RVA: 0x09F1DCD4 */ }
        System.Int32 SharedRenderMeshCount { get; /* RVA: 0x09F1DC44 */ }
        System.Int32 SharedChildMeshCount { get; /* RVA: 0x09F1DBFC */ }
        System.Int32 RenderMeshCount { get; /* RVA: 0x09F1D6F4 */ }
        System.Int32 RenderMeshVertexCount { get; /* RVA: 0x09F1D9E0 */ }
        System.Int32 RenderMeshUseCount { get; /* RVA: 0x09F1D888 */ }
        System.Int32 RenderMeshVertexUseCount { get; /* RVA: 0x09F1DAB8 */ }
        System.Int32 RenderMeshPauseCount { get; /* RVA: 0x09F1D73C */ }

        // Methods
        // RVA: 0x0442EE60  token: 0x60005A3
        public virtual System.Void Create() { }
        // RVA: 0x09F19D4C  token: 0x60005A4
        public virtual System.Void Dispose() { }
        // RVA: 0x09F173A0  token: 0x60005A5
        public System.Void AddMesh(MagicaCloth.BaseMeshDeformer bmesh) { }
        // RVA: 0x09F1AECC  token: 0x60005A6
        public System.Void RemoveMesh(MagicaCloth.BaseMeshDeformer bmesh) { }
        // RVA: 0x09F18438  token: 0x60005A7
        public System.Int32 AddVirtualMesh(System.Int32 uid, System.Int32 vertexCount, System.Int32 weightCount, System.Int32 boneCount, System.Int32 triangleCount, System.Int32 vertexToTriangleIndexCount, UnityEngine.Transform transform) { }
        // RVA: 0x09F1A728  token: 0x60005A8
        public System.Boolean IsEmptySharedVirtualMesh(System.Int32 uid) { }
        // RVA: 0x09F1CD04  token: 0x60005A9
        public System.Void SetSharedVirtualMeshData(System.Int32 virtualMeshIndex, System.UInt32[] sharedVertexInfoList, MagicaCloth.MeshData.VertexWeight[] sharedWeightList, UnityEngine.Vector2[] sharedUv, System.Int32[] sharedTriangles, System.UInt32[] vertexToTriangleInfoList, System.Int32[] vertexToTriangleIndexList) { }
        // RVA: 0x09F1BCC8  token: 0x60005AA
        public System.Void RemoveVirtualMesh(System.Int32 virtualMeshIndex) { }
        // RVA: 0x09F1A210  token: 0x60005AB
        public System.Boolean ExistsVirtualMesh(System.Int32 virtualMeshIndex) { }
        // RVA: 0x09F1A4D4  token: 0x60005AC
        public MagicaCloth.PhysicsManagerMeshData.VirtualMeshInfo GetVirtualMeshInfo(System.Int32 virtualMeshIndex) { }
        // RVA: 0x09F1A8DC  token: 0x60005AD
        public System.Boolean IsUseVirtualMesh(System.Int32 virtualMeshIndex) { }
        // RVA: 0x09F1A618  token: 0x60005AE
        public System.Boolean IsActiveVirtualMesh(System.Int32 virtualMeshIndex) { }
        // RVA: 0x09F1CFA4  token: 0x60005AF
        public System.Void SetVirtualMeshActive(System.Int32 virtualMeshIndex, System.Boolean sw) { }
        // RVA: 0x09F17FF8  token: 0x60005B0
        public System.Void AddUseVirtualMesh(System.Int32 virtualMeshIndex) { }
        // RVA: 0x09F1B888  token: 0x60005B1
        public System.Void RemoveUseVirtualMesh(System.Int32 virtualMeshIndex) { }
        // RVA: 0x09F180D4  token: 0x60005B2
        public System.Boolean AddUseVirtualVertex(System.Int32 virtualMeshIndex, System.Int32 vindex, System.Boolean fix) { }
        // RVA: 0x09F1B964  token: 0x60005B3
        public System.Boolean RemoveUseVirtualVertex(System.Int32 virtualMeshIndex, System.Int32 vindex, System.Boolean fix) { }
        // RVA: 0x09F19688  token: 0x60005B4
        public System.Void CopyToVirtualMeshWorldData(System.Int32 virtualMeshIndex, UnityEngine.Vector3[] vertices, UnityEngine.Vector3[] normals, UnityEngine.Vector3[] tangents) { }
        // RVA: 0x09F18248  token: 0x60005B5
        public System.Void AddVirtualMeshBone(System.Int32 virtualMeshIndex, System.Collections.Generic.List<UnityEngine.Transform> boneList) { }
        // RVA: 0x09F1BAD8  token: 0x60005B6
        public System.Void RemoveVirtualMeshBone(System.Int32 virtualMeshIndex) { }
        // RVA: 0x09F1C194  token: 0x60005B7
        public System.Void ResetFuturePredictionVirtualMeshBone(System.Int32 virtualMeshIndex) { }
        // RVA: 0x09F18B1C  token: 0x60005B8
        public System.Void ChangeVirtualMeshUseUnityPhysics(System.Int32 virtualMeshIndex, System.Boolean sw) { }
        // RVA: 0x09F1D08C  token: 0x60005B9
        public System.Void SetVirtualMeshFlag(System.Int32 virtualMeshIndex, System.UInt32 flag, System.Boolean sw) { }
        // RVA: 0x09F17BA8  token: 0x60005C1
        public System.Int32 AddSharedChildMesh(System.Int64 cuid, System.Int32 virtualMeshIndex, System.Int32 vertexCount, System.Int32 weightCount) { }
        // RVA: 0x09F1A674  token: 0x60005C2
        public System.Boolean IsEmptySharedChildMesh(System.Int64 cuid) { }
        // RVA: 0x09F1CBEC  token: 0x60005C3
        public System.Void SetSharedChildMeshData(System.Int32 sharedMeshIndex, System.UInt32[] sharedVertexInfoList, MagicaCloth.MeshData.VertexWeight[] sharedVertexWeightList) { }
        // RVA: 0x09F1B554  token: 0x60005C4
        public System.Void RemoveSharedChildMesh(System.Int32 sharedChildMeshIndex) { }
        // RVA: 0x09F17588  token: 0x60005C7
        public System.Int32 AddRenderMesh(System.Int32 uid, System.Boolean isSkinning, System.Boolean isFasterWrite, UnityEngine.Vector3 baseScale, System.Int32 vertexCount, System.Int32 rendererBoneIndex, System.Int32 boneWeightCount) { }
        // RVA: 0x09F1D3B0  token: 0x60005C8
        public System.Void UpdateMeshState(System.Int32 renderMeshIndex) { }
        // RVA: 0x09F17418  token: 0x60005C9
        public System.Void AddRenderMeshBone(System.Int32 renderMeshIndex, UnityEngine.Transform rendererTransform) { }
        // RVA: 0x09F1A6D0  token: 0x60005CA
        public System.Boolean IsEmptySharedRenderMesh(System.Int32 uid) { }
        // RVA: 0x09F1C770  token: 0x60005CB
        public System.Void SetRenderSharedMeshData(System.Int32 renderMeshIndex, System.Boolean isSkinning, UnityEngine.Vector3[] sharedVertices, UnityEngine.Vector3[] sharedNormals, UnityEngine.Vector4[] sharedTangents, Unity.Collections.NativeArray<System.Byte> sharedBonesPerVertex, Unity.Collections.NativeArray<UnityEngine.BoneWeight1> sharedBoneWeights) { }
        // RVA: 0x09F1B0B4  token: 0x60005CC
        public System.Void RemoveRenderMesh(System.Int32 renderMeshIndex) { }
        // RVA: 0x09F1AF50  token: 0x60005CD
        public System.Void RemoveRenderMeshBone(System.Int32 renderMeshIndex) { }
        // RVA: 0x09F189E0  token: 0x60005CE
        public System.Void ChangeRenderMeshUseUnityPhysics(System.Int32 renderMeshIndex, System.Boolean sw) { }
        // RVA: 0x09F1A878  token: 0x60005CF
        public System.Boolean IsUseRenderMesh(System.Int32 renderMeshIndex) { }
        // RVA: 0x09F1A55C  token: 0x60005D0
        public System.Boolean IsActiveRenderMesh(System.Int32 renderMeshIndex) { }
        // RVA: 0x09F1C5F0  token: 0x60005D1
        public System.Void SetRenderMeshFlag(System.Int32 renderMeshIndex, System.UInt32 flag, System.Boolean sw) { }
        // RVA: 0x09F1A780  token: 0x60005D2
        public System.Boolean IsRenderMeshFlag(System.Int32 renderMeshIndex, System.UInt32 flag) { }
        // RVA: 0x09F1C3FC  token: 0x60005D3
        public System.Void SetRenderMeshActive(System.Int32 renderMeshIndex, System.Boolean sw) { }
        // RVA: 0x09F17E38  token: 0x60005D4
        public System.Void AddUseRenderMesh(System.Int32 renderMeshIndex) { }
        // RVA: 0x09F1B6C8  token: 0x60005D5
        public System.Void RemoveUseRenderMesh(System.Int32 renderMeshIndex) { }
        // RVA: 0x09F1A96C  token: 0x60005D6
        public System.Void LinkRenderMesh(System.Int32 renderMeshIndex, System.Int32 childMeshVertexStart, System.Int32 childMeshWeightStart, System.Int32 virtualMeshVertexStart, System.Int32 sharedVirtualMeshVertexStart) { }
        // RVA: 0x09F1D180  token: 0x60005D7
        public System.Void UnlinkRenderMesh(System.Int32 renderMeshIndex, System.Int32 childMeshVertexStart, System.Int32 childMeshWeightStart, System.Int32 virtualMeshVertexStart, System.Int32 sharedVirtualMeshVertexStart) { }
        // RVA: 0x09F18F80  token: 0x60005D8
        private System.Void CopyToRenderMeshLocalPositionData(System.Int32 renderMeshIndex, UnityEngine.Mesh mesh, System.Int32 bufferIndex) { }
        // RVA: 0x09F18DEC  token: 0x60005D9
        private System.Void CopyToRenderMeshLocalNormalTangentData(System.Int32 renderMeshIndex, UnityEngine.Mesh mesh, System.Int32 bufferIndex, System.Boolean normal, System.Boolean tangent) { }
        // RVA: 0x09F18C68  token: 0x60005DA
        private System.Void CopyToRenderMeshBoneWeightData(System.Int32 renderMeshIndex, UnityEngine.Mesh mesh, UnityEngine.Mesh sharedMesh, System.Int32 bufferIndex) { }
        // RVA: 0x09F19070  token: 0x60005DB
        private System.Void CopyToRenderMeshWorldData(System.Int32 renderMeshIndex, UnityEngine.Transform target, UnityEngine.Vector3[] vertices, UnityEngine.Vector3[] normals, UnityEngine.Vector3[] tangents) { }
        // RVA: 0x09F1A308  token: 0x60005DC
        private System.Collections.Generic.List<System.Int32> GetVertexUseList(System.Int32 renderMeshIndex) { }
        // RVA: 0x09F1C294  token: 0x60005E2
        private System.Void SetDelayedCalculatedFlag() { }
        // RVA: 0x03233470  token: 0x60005E3
        private System.Void ClearWritingList() { }
        // RVA: 0x09F1AB70  token: 0x60005E4
        private System.Void MeshCalculation(System.Int32 bufferIndex) { }
        // RVA: 0x09F1AD64  token: 0x60005E5
        private System.Void NormalWriting(System.Int32 bufferIndex) { }
        // RVA: 0x09F193E0  token: 0x60005E6
        private System.Void CopyToRenderVertexBuffer(System.Int32 renderMeshIndex, System.Int32 bufferIndex, UnityEngine.GraphicsBuffer vertexBuffer, System.Boolean normal, UnityEngine.ComputeShader compute, System.Int32 kernel, System.Int32 index) { }
        // RVA: 0x09F1D468  token: 0x60005E7
        private System.Void UpdateVertexBuffer() { }
        // RVA: 0x03233A20  token: 0x60005E8
        private System.Void FinishVertexBuffer() { }
        // RVA: 0x09F1A264  token: 0x60005E9
        private System.Void FasterWriting(System.Int32 bufferIndex) { }
        // RVA: 0x09F198E4  token: 0x60005EA
        private System.Void DispatchWriting(System.Int32 kernel, System.Collections.Generic.List<MagicaCloth.RenderMeshDeformer> rlist, System.Int32 bufferIndex) { }
        // RVA: 0x046D78C0  token: 0x60005EB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BC  // size: 0x100
    public class PhysicsManagerParticleData : MagicaCloth.PhysicsManagerAccess
    {
        // Fields
        public static System.UInt32 Flag_Enable;  // const
        public static System.UInt32 Flag_Kinematic;  // const
        public static System.UInt32 Flag_Hold;  // const
        public static System.UInt32 Flag_Collider;  // const
        public static System.UInt32 Flag_Plane;  // const
        public static System.UInt32 Flag_CapsuleX;  // const
        public static System.UInt32 Flag_CapsuleY;  // const
        public static System.UInt32 Flag_CapsuleZ;  // const
        public static System.UInt32 Flag_Box;  // const
        public static System.UInt32 Flag_TriangleRotation;  // const
        public static System.UInt32 Flag_Transform_Read_Pos;  // const
        public static System.UInt32 Flag_Transform_Read_Rot;  // const
        public static System.UInt32 Flag_Transform_Read_Base;  // const
        public static System.UInt32 Flag_Transform_Read_Local;  // const
        public static System.UInt32 Flag_Transform_Read_Scl;  // const
        public static System.UInt32 Flag_Transform_Write;  // const
        public static System.UInt32 Flag_Transform_Restore;  // const
        public static System.UInt32 Flag_Transform_UnityPhysics;  // const
        public static System.UInt32 Flag_Transform_Parent;  // const
        public static System.UInt32 Flag_Step_Update;  // const
        public static System.UInt32 Flag_Reset_Position;  // const
        public MagicaCloth.FixedChunkNativeArray<MagicaCloth.PhysicsManagerParticleData.ParticleFlag> flagList;  // 0x18
        public MagicaCloth.FixedChunkNativeArray<System.Int32> teamIdList;  // 0x20
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> posList;  // 0x28
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.quaternion> rotList;  // 0x30
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> oldPosList;  // 0x38
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.quaternion> oldRotList;  // 0x40
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> oldSlowPosList;  // 0x48
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> localPosList;  // 0x50
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> basePosList;  // 0x58
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.quaternion> baseRotList;  // 0x60
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> snapBasePosList;  // 0x68
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.quaternion> snapBaseRotList;  // 0x70
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> oldBasePosList;  // 0x78
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.quaternion> oldBaseRotList;  // 0x80
        public MagicaCloth.FixedChunkNativeArray<System.Single> depthList;  // 0x88
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> radiusList;  // 0x90
        public MagicaCloth.FixedChunkNativeArray<System.Int32> restoreTransformIndexList;  // 0x98
        public MagicaCloth.FixedChunkNativeArray<System.Int32> transformIndexList;  // 0xa0
        public MagicaCloth.FixedChunkNativeArray<System.Single> frictionList;  // 0xa8
        public MagicaCloth.FixedChunkNativeArray<System.Single> staticFrictionList;  // 0xb0
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> velocityList;  // 0xb8
        public MagicaCloth.FixedChunkNativeArray<System.Int32> collisionLinkIdList;  // 0xc0
        public MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> collisionNormalList;  // 0xc8
        private MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> nextPos0List;  // 0xd0
        private MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> nextPos1List;  // 0xd8
        private System.Int32 nextPosSwitch;  // 0xe0
        private MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.quaternion> nextRot0List;  // 0xe8
        private MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.quaternion> nextRot1List;  // 0xf0
        private System.Int32 nextRotSwitch;  // 0xf8
        private System.Int32 colliderCount;  // 0xfc

        // Properties
        System.Int32 Count { get; /* RVA: 0x09F1FB70 */ }
        System.Int32 Length { get; /* RVA: 0x09F1FBD8 */ }
        System.Int32 ColliderCount { get; /* RVA: 0x04D8F940 */ }
        MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> InNextPosList { get; /* RVA: 0x09F1FBA0 */ }
        MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float3> OutNextPosList { get; /* RVA: 0x09F1FBEC */ }
        MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.quaternion> InNextRotList { get; /* RVA: 0x09F1FBBC */ }
        MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.quaternion> OutNextRotList { get; /* RVA: 0x09F1FC08 */ }

        // Methods
        // RVA: 0x0442F840  token: 0x6000605
        public virtual System.Void Create() { }
        // RVA: 0x09F1E87C  token: 0x6000606
        public virtual System.Void Dispose() { }
        // RVA: 0x0442FFA0  token: 0x6000607
        public MagicaCloth.ChunkData CreateParticle(System.UInt32 flag, System.Int32 team, Unity.Mathematics.float3 wpos, Unity.Mathematics.quaternion wrot, System.Single depth, Unity.Mathematics.float3 radius, Unity.Mathematics.float3 targetLocalPos) { }
        // RVA: 0x09F1DF6C  token: 0x6000608
        public MagicaCloth.ChunkData CreateParticle(System.Int32 team, System.Int32 count, System.Func<System.Int32,System.UInt32> funcFlag, System.Func<System.Int32,Unity.Mathematics.float3> funcWpos, System.Func<System.Int32,Unity.Mathematics.quaternion> funcWrot, System.Func<System.Int32,System.Single> funcDepth, System.Func<System.Int32,Unity.Mathematics.float3> funcRadius, System.Func<System.Int32,Unity.Mathematics.float3> funcTargetLocalPos) { }
        // RVA: 0x09F1EC50  token: 0x6000609
        public System.Void RemoveParticle(MagicaCloth.ChunkData c) { }
        // RVA: 0x04430570  token: 0x600060A
        public System.Void SetEnable(MagicaCloth.ChunkData c, System.Boolean sw, System.Func<System.Int32,UnityEngine.Transform> funcTarget, System.Func<System.Int32,Unity.Mathematics.float3> funcLpos, System.Func<System.Int32,Unity.Mathematics.quaternion> funcLrot) { }
        // RVA: 0x09F1F158  token: 0x600060B
        public System.Void SetRadius(System.Int32 index, Unity.Mathematics.float3 radius) { }
        // RVA: 0x09F1F0C4  token: 0x600060C
        public System.Void SetLocalPos(System.Int32 index, UnityEngine.Vector3 lpos) { }
        // RVA: 0x09F1EBE8  token: 0x6000610
        public System.Boolean IsValid(System.Int32 index) { }
        // RVA: 0x09F1EB98  token: 0x6000611
        public System.Int32 GetTeamId(System.Int32 index) { }
        // RVA: 0x09F1F054  token: 0x6000612
        public System.Void ResetFuturePredictionTransform(System.Int32 index) { }
        // RVA: 0x09F1F004  token: 0x6000613
        public System.Void ResetFuturePredictionTransform(MagicaCloth.ChunkData c) { }
        // RVA: 0x04DA3680  token: 0x6000616
        public System.Void SwitchingNextPosList() { }
        // RVA: 0x04DA36A0  token: 0x6000619
        public System.Void SwitchingNextRotList() { }
        // RVA: 0x09F1F1C8  token: 0x600061A
        public System.Void UpdateBoneToParticle() { }
        // RVA: 0x09F1F878  token: 0x600061B
        public System.Void UpdateResetParticle() { }
        // RVA: 0x09F1F570  token: 0x600061C
        public System.Boolean UpdateParticleToBone() { }
        // RVA: 0x041E1670  token: 0x600061D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C1  // size: 0x80
    public class PhysicsManagerTeamData : MagicaCloth.PhysicsManagerAccess
    {
        // Fields
        public static System.UInt32 Flag_Enable;  // const
        public static System.UInt32 Flag_Interpolate;  // const
        public static System.UInt32 Flag_FixedNonRotation;  // const
        public static System.UInt32 Flag_AnimatedPose;  // const
        public static System.UInt32 Flag_IgnoreClampPositionVelocity;  // const
        public static System.UInt32 Flag_Collision;  // const
        public static System.UInt32 Flag_AfterCollision;  // const
        public static System.UInt32 Flag_UpdatePhysics;  // const
        public static System.UInt32 Flag_Pause;  // const
        public static System.UInt32 Flag_Reset_WorldInfluence;  // const
        public static System.UInt32 Flag_Reset_Position;  // const
        public static System.UInt32 Flag_Reset_Keep;  // const
        public static System.UInt32 Flag_Wind;  // const
        public MagicaCloth.FixedNativeList<MagicaCloth.PhysicsManagerTeamData.TeamData> teamDataList;  // 0x18
        public MagicaCloth.FixedNativeList<MagicaCloth.CurveParam> teamMassList;  // 0x20
        public MagicaCloth.FixedNativeList<MagicaCloth.CurveParam> teamGravityList;  // 0x28
        public MagicaCloth.FixedNativeList<MagicaCloth.CurveParam> teamDragList;  // 0x30
        public MagicaCloth.FixedNativeList<MagicaCloth.CurveParam> teamMaxVelocityList;  // 0x38
        public MagicaCloth.FixedNativeList<MagicaCloth.CurveParam> teamDepthInfluenceList;  // 0x40
        public MagicaCloth.FixedNativeList<MagicaCloth.PhysicsManagerTeamData.WorldInfluence> teamWorldInfluenceList;  // 0x48
        public MagicaCloth.FixedNativeList<MagicaCloth.PhysicsManagerTeamData.WindInfo> teamWindInfoList;  // 0x50
        public MagicaCloth.FixedMultiNativeList<System.Int32> colliderList;  // 0x58
        public MagicaCloth.FixedMultiNativeList<System.Int32> skinningBoneList;  // 0x60
        private System.Collections.Generic.Dictionary<System.Int32,MagicaCloth.PhysicsTeam> teamComponentDict;  // 0x68
        private System.Int32 activeTeamCount;  // 0x70
        private System.Int32 normalUpdateCount;  // 0x74
        private System.Int32 physicsUpdateCount;  // 0x78

        // Properties
        System.Int32 TeamCount { get; /* RVA: 0x09F2E5F4 */ }
        System.Int32 TeamLength { get; /* RVA: 0x09F2E63C */ }
        System.Int32 ActiveTeamCount { get; /* RVA: 0x04D868A0 */ }
        System.Int32 ColliderCount { get; /* RVA: 0x09F2E4B0 */ }
        System.Int32 NormalUpdateCount { get; /* RVA: 0x04D868B0 */ }
        System.Int32 PhysicsUpdateCount { get; /* RVA: 0x04D863C0 */ }
        System.Int32 PauseCount { get; /* RVA: 0x09F2E4FC */ }

        // Methods
        // RVA: 0x02FC1070  token: 0x6000631
        public virtual System.Void Create() { }
        // RVA: 0x09F2B738  token: 0x6000632
        public virtual System.Void Dispose() { }
        // RVA: 0x02FC1330  token: 0x600063A
        public System.Int32 CreateTeam(MagicaCloth.PhysicsTeam team, System.UInt32 flag) { }
        // RVA: 0x09F2C534  token: 0x600063B
        public System.Void RemoveTeam(System.Int32 teamId) { }
        // RVA: 0x09F2D238  token: 0x600063C
        public System.Void SetEnable(System.Int32 teamId, System.Boolean sw) { }
        // RVA: 0x09F2BC68  token: 0x600063D
        public System.Boolean IsValid(System.Int32 teamId) { }
        // RVA: 0x09F2BC38  token: 0x600063E
        public System.Boolean IsValidData(System.Int32 teamId) { }
        // RVA: 0x09F2BAE0  token: 0x600063F
        public System.Boolean IsActive(System.Int32 teamId) { }
        // RVA: 0x09F2D38C  token: 0x6000640
        public System.Void SetFlag(System.Int32 teamId, System.UInt32 flag, System.Boolean sw) { }
        // RVA: 0x09F2BBA4  token: 0x6000641
        public System.Boolean IsFlag(System.Int32 teamId, System.UInt32 flag) { }
        // RVA: 0x09F2DAD4  token: 0x6000642
        public System.Void SetParticleChunk(System.Int32 teamId, MagicaCloth.ChunkData chunk) { }
        // RVA: 0x09F2D4CC  token: 0x6000643
        public System.Void SetFriction(System.Int32 teamId, System.Single dynamicFriction, System.Single staticFriction) { }
        // RVA: 0x09F2D91C  token: 0x6000644
        public System.Void SetMass(System.Int32 teamId, MagicaCloth.BezierParam mass) { }
        // RVA: 0x09F2D764  token: 0x6000645
        public System.Void SetGravity(System.Int32 teamId, MagicaCloth.BezierParam gravity) { }
        // RVA: 0x09F2D5CC  token: 0x6000646
        public System.Void SetGravityDirection(System.Int32 teamId, Unity.Mathematics.float3 dir) { }
        // RVA: 0x09F2D1E0  token: 0x6000647
        public System.Void SetDrag(System.Int32 teamId, MagicaCloth.BezierParam drag) { }
        // RVA: 0x09F2D974  token: 0x6000648
        public System.Void SetMaxVelocity(System.Int32 teamId, MagicaCloth.BezierParam maxVelocity) { }
        // RVA: 0x09F2D260  token: 0x6000649
        public System.Void SetExternalForce(System.Int32 teamId, System.Single massInfluence, System.Single windInfluence, System.Single windRandomScale, System.Single windSynchronization) { }
        // RVA: 0x09F2D188  token: 0x600064A
        public System.Void SetDepthInfluence(System.Int32 teamId, MagicaCloth.BezierParam depthInfluence) { }
        // RVA: 0x09F2E10C  token: 0x600064B
        public System.Void SetWorldInfluence(System.Int32 teamId, System.Single maxSpeed, System.Single maxRotatinSpeed, MagicaCloth.BezierParam moveInfluence, MagicaCloth.BezierParam rotInfluence, System.Boolean resetTeleport, System.Single teleportDistance, System.Single teleportRotation, System.Single resetStabilizationTime, MagicaCloth.ClothParams.TeleportMode teleportMode, System.Boolean useClampRotation, MagicaCloth.BezierParam clampRotation) { }
        // RVA: 0x09F2AE20  token: 0x600064C
        private System.Single CalcClampRotationLimit(System.Boolean useClampRotation, MagicaCloth.BezierParam clampRotation) { }
        // RVA: 0x09F2E2E8  token: 0x600064D
        public System.Void SetWorldInfluence(System.Int32 teamId, System.Single maxSpeed, System.Single maxRotationSpeed, MagicaCloth.BezierParam moveInfluence, MagicaCloth.BezierParam rotInfluence) { }
        // RVA: 0x09F2CC08  token: 0x600064E
        public System.Void SetAfterTeleport(System.Int32 teamId, System.Boolean resetTeleport, System.Single teleportDistance, System.Single teleportRotation, MagicaCloth.ClothParams.TeleportMode teleportMode) { }
        // RVA: 0x09F2DD18  token: 0x600064F
        public System.Void SetStabilizationTime(System.Int32 teamId, System.Single resetStabilizationTime) { }
        // RVA: 0x09F2CA04  token: 0x6000650
        public System.Void ResetWorldInfluenceTarget(System.Int32 teamId, UnityEngine.Transform target) { }
        // RVA: 0x09F2D000  token: 0x6000651
        public System.Void SetClampRotation(System.Int32 teamId, System.Boolean useClampRotation, MagicaCloth.BezierParam clampRotation) { }
        // RVA: 0x09F2DBD8  token: 0x6000652
        public System.Void SetSelfCollisionRange(System.Int32 teamId, System.Single range) { }
        // RVA: 0x09F2CEA4  token: 0x6000653
        public System.Void SetBoneIndex(System.Int32 teamId, System.Int32 boneIndex, UnityEngine.Vector3 initScale) { }
        // RVA: 0x09F2A838  token: 0x6000654
        private System.Void AddColliderParticle(System.Int32 teamId, System.Int32 particleIndex) { }
        // RVA: 0x09F2C078  token: 0x6000655
        private System.Void RemoveColliderParticle(System.Int32 teamId, System.Int32 particleIndex) { }
        // RVA: 0x09F2C240  token: 0x6000656
        private System.Void RemoveCollider(System.Int32 teamId, MagicaCloth.ColliderComponent collider) { }
        // RVA: 0x09F2C63C  token: 0x6000657
        public System.Void ResetFuturePredictionCollidere(System.Int32 teamId) { }
        // RVA: 0x09F2AA2C  token: 0x6000658
        public System.Void AddSkinningBoneIndex(System.Int32 teamId, System.Collections.Generic.List<UnityEngine.Transform> boneList) { }
        // RVA: 0x09F2C30C  token: 0x6000659
        public System.Void RemoveSkinningBoneIndex(System.Int32 teamId) { }
        // RVA: 0x09F2DE78  token: 0x600065A
        public System.Void SetTimeScale(System.Int32 teamId, System.Single timeScale) { }
        // RVA: 0x09F2BA24  token: 0x600065B
        public System.Single GetTimeScale(System.Int32 teamId) { }
        // RVA: 0x09F2CD9C  token: 0x600065C
        public System.Void SetBlendRatio(System.Int32 teamId, System.Single blendRatio) { }
        // RVA: 0x09F2B8AC  token: 0x600065D
        public System.Single GetBlendRatio(System.Int32 teamId) { }
        // RVA: 0x09F2D9CC  token: 0x600065E
        public System.Void SetOutPhysicsPoseBlendRatio(System.Int32 teamId, System.Single blendRatio) { }
        // RVA: 0x09F2B968  token: 0x600065F
        public System.Single GetOutPhysicsPoseBlendRatio(System.Int32 teamId) { }
        // RVA: 0x09F2D7BC  token: 0x6000660
        public System.Void SetImpactForce(System.Int32 teamId, Unity.Mathematics.float3 force, MagicaCloth.PhysicsManagerTeamData.ForceMode mode) { }
        // RVA: 0x09F2C810  token: 0x6000661
        public System.Void ResetStabilizationTime(System.Int32 teamId, System.Single resetStabilizationTime) { }
        // RVA: 0x09F2DFC0  token: 0x6000662
        public System.Void SetUpdateMode(System.Int32 teamId, MagicaCloth.PhysicsTeam.TeamUpdateMode updateMode) { }
        // RVA: 0x09F2B524  token: 0x6000663
        public System.Void ChangeUseUnityPhysics(System.Int32 teamId, System.Boolean sw) { }
        // RVA: 0x09F2B408  token: 0x6000664
        private System.Void ChangeParticleUseUnityPhysics(System.Int32 pindex, System.Boolean unityPhysics) { }
        // RVA: 0x09F2B1F8  token: 0x6000665
        private System.Void ChangeBoneFlag(System.Int32 teamId, MagicaCloth.PhysicsTeam.TeamCullingMode cullingMode, System.Boolean isCalculation) { }
        // RVA: 0x033362D0  token: 0x6000666
        private System.Void EarlyUpdateTeamAlways() { }
        // RVA: 0x033364E0  token: 0x6000667
        private System.Void PreUpdateTeamAlways() { }
        // RVA: 0x09F2AE7C  token: 0x6000668
        private System.Int32 CalcMaxUpdateCount(System.Int32 ups, System.Single deltaTime, System.Single physicsDeltaTime, System.Single updateDeltaTime) { }
        // RVA: 0x09F2BD94  token: 0x6000669
        private System.Void PreUpdateTeamData(System.Single deltaTime, System.Single physicsDeltaTime, System.Single updateDeltaTime, System.Int32 ups, System.Int32 maxUpdateCount) { }
        // RVA: 0x09F2BC74  token: 0x600066A
        private System.Void PostUpdateTeamData() { }
        // RVA: 0x046D76B0  token: 0x600066B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C9  // size: 0x20
    public class PhysicsManagerWindData : MagicaCloth.PhysicsManagerAccess
    {
        // Fields
        public static System.UInt32 Flag_Enable;  // const
        public static System.UInt32 Flag_Addition;  // const
        public MagicaCloth.FixedNativeList<MagicaCloth.PhysicsManagerWindData.WindData> windDataList;  // 0x18

        // Properties
        System.Int32 Count { get; /* RVA: 0x09F2EEB4 */ }

        // Methods
        // RVA: 0x04D2EDA0  token: 0x600067C
        public virtual System.Void Create() { }
        // RVA: 0x09F2E9F4  token: 0x600067D
        public virtual System.Void Dispose() { }
        // RVA: 0x09F2E8B8  token: 0x600067E
        public System.Int32 CreateWind(MagicaCloth.PhysicsManagerWindData.WindType windType, MagicaCloth.PhysicsManagerWindData.ShapeType shapeType, Unity.Mathematics.float3 areaSize, System.Boolean addition, System.Single main, System.Single turbulence, System.Single frequency, Unity.Mathematics.float3 direction, MagicaCloth.PhysicsManagerWindData.DirectionType directinType, System.Single areaVolume, System.Single areaLength, MagicaCloth.BezierParam attenuation) { }
        // RVA: 0x09F2EA9C  token: 0x600067F
        public System.Void RemoveWind(System.Int32 windId) { }
        // RVA: 0x09F2EAF4  token: 0x6000680
        public System.Void SetEnable(System.Int32 windId, System.Boolean sw, UnityEngine.Transform target) { }
        // RVA: 0x09F2EA38  token: 0x6000681
        public System.Boolean IsActive(System.Int32 windId) { }
        // RVA: 0x09F2EC70  token: 0x6000682
        public System.Void SetFlag(System.Int32 windId, System.UInt32 flag, System.Boolean sw) { }
        // RVA: 0x09F2ED44  token: 0x6000683
        public System.Void SetParameter(System.Int32 windId, Unity.Mathematics.float3 areaSize, System.Boolean addition, System.Single main, System.Single turbulence, System.Single frequency, Unity.Mathematics.float3 direction, System.Single areaVolume, System.Single areaLength, MagicaCloth.BezierParam attenuation) { }
        // RVA: 0x09F2E67C  token: 0x6000685
        private static Unity.Mathematics.float3 CalcWindForce(System.Single time, Unity.Mathematics.float2 noiseBasePos, Unity.Mathematics.float3 mainDir, System.Single main, System.Single turbulence, System.Single frequency, System.Single randomScale) { }
        // RVA: 0x041E1670  token: 0x6000686
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CE  // size: 0x18
    public abstract class PhysicsManagerWorker
    {
        // Fields
        private MagicaCloth.MagicaPhysicsManager <Manager>k__BackingField;  // 0x10

        // Properties
        MagicaCloth.MagicaPhysicsManager Manager { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600068D
        protected virtual System.Void Start() { }
        // RVA: 0x03B93E90  token: 0x600068E
        public System.Void Init(MagicaCloth.MagicaPhysicsManager manager) { }
        // RVA: -1  // abstract  token: 0x600068F
        public virtual System.Void Create() { }
        // RVA: -1  // abstract  token: 0x6000690
        public virtual System.Void RemoveGroup(System.Int32 group) { }
        // RVA: -1  // abstract  token: 0x6000691
        public virtual System.Void Release() { }
        // RVA: -1  // abstract  token: 0x6000692
        public virtual System.Void Warmup() { }
        // RVA: -1  // abstract  token: 0x6000693
        public virtual Unity.Jobs.JobHandle PreUpdate(Unity.Jobs.JobHandle jobHandle) { }
        // RVA: -1  // abstract  token: 0x6000694
        public virtual Unity.Jobs.JobHandle PostUpdate(Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x041E1670  token: 0x6000695
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000CF  // size: 0x38
    public abstract class ColliderComponent : MagicaCloth.ParticleComponent
    {
        // Fields
        protected System.Boolean isGlobal;  // 0x28
        private UnityEngine.Vector3 center;  // 0x2c

        // Properties
        UnityEngine.Vector3 Center { get; /* RVA: 0x04D90540 */ set; /* RVA: 0x09F25B5C */ }

        // Methods
        // RVA: 0x04D84ED0  token: 0x6000698
        protected virtual System.Void OnInit() { }
        // RVA: 0x09F25834  token: 0x6000699
        protected virtual System.Void OnDispose() { }
        // RVA: 0x09F257E8  token: 0x600069A
        public virtual System.Int32 GetDataHash() { }
        // RVA: -1  // abstract  token: 0x600069B
        public virtual System.Boolean CalcNearPoint(UnityEngine.Vector3 pos, UnityEngine.Vector3& p, UnityEngine.Vector3& dir, UnityEngine.Vector3& d, System.Boolean skinning) { }
        // RVA: 0x09F2564C  token: 0x600069C
        public UnityEngine.Vector3 CalcLocalPos(UnityEngine.Vector3 pos) { }
        // RVA: 0x09F255E0  token: 0x600069D
        public UnityEngine.Vector3 CalcLocalDir(UnityEngine.Vector3 dir) { }
        // RVA: 0x09F2575C  token: 0x600069E
        public MagicaCloth.ChunkData CreateColliderParticle(System.Int32 teamId) { }
        // RVA: 0x09F25A70  token: 0x600069F
        public System.Void RemoveColliderParticle(System.Int32 teamId) { }
        // RVA: -1  // abstract  token: 0x60006A0
        protected virtual MagicaCloth.ChunkData CreateColliderParticleReal(System.Int32 teamId) { }
        // RVA: 0x053982BC  token: 0x60006A1
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000D0  // size: 0x48
    public class MagicaCapsuleCollider : MagicaCloth.ColliderComponent
    {
        // Fields
        private MagicaCloth.MagicaCapsuleCollider.Axis axis;  // 0x38
        private System.Single length;  // 0x3c
        private System.Single startRadius;  // 0x40
        private System.Single endRadius;  // 0x44

        // Properties
        MagicaCloth.MagicaCapsuleCollider.Axis AxisMode { get; /* RVA: 0x04D863B0 */ set; /* RVA: 0x09F2859C */ }
        System.Single Length { get; /* RVA: 0x04D863E0 */ set; /* RVA: 0x09F285B4 */ }
        System.Single StartRadius { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x09F285C0 */ }
        System.Single EndRadius { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x09F285A8 */ }

        // Methods
        // RVA: 0x04DA36C0  token: 0x60006A2
        public virtual MagicaCloth.ComponentType GetComponentType() { }
        // RVA: 0x09F28570  token: 0x60006A3
        private System.Void OnValidate() { }
        // RVA: 0x09F28040  token: 0x60006A4
        private virtual System.Void DataUpdate() { }
        // RVA: 0x09F28390  token: 0x60006A5
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x09F27F0C  token: 0x60006AE
        protected virtual MagicaCloth.ChunkData CreateColliderParticleReal(System.Int32 teamId) { }
        // RVA: 0x09F28370  token: 0x60006AF
        private System.UInt32 GetCapsuleFlag() { }
        // RVA: 0x09F28494  token: 0x60006B0
        public UnityEngine.Vector3 GetLocalDir() { }
        // RVA: 0x09F284D8  token: 0x60006B1
        public UnityEngine.Vector3 GetLocalUp() { }
        // RVA: 0x09F28514  token: 0x60006B2
        public System.Single GetScale() { }
        // RVA: 0x09F27790  token: 0x60006B3
        public virtual System.Boolean CalcNearPoint(UnityEngine.Vector3 pos, UnityEngine.Vector3& p, UnityEngine.Vector3& dir, UnityEngine.Vector3& d, System.Boolean skinning) { }
        // RVA: 0x04D778B0  token: 0x60006B4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D2  // size: 0x38
    public class MagicaPlaneCollider : MagicaCloth.ColliderComponent
    {
        // Methods
        // RVA: 0x04DA36D0  token: 0x60006B5
        public virtual MagicaCloth.ComponentType GetComponentType() { }
        // RVA: 0x09F28570  token: 0x60006B6
        private System.Void OnValidate() { }
        // RVA: 0x09F288C4  token: 0x60006B7
        private virtual System.Void DataUpdate() { }
        // RVA: 0x09F28798  token: 0x60006B8
        protected virtual MagicaCloth.ChunkData CreateColliderParticleReal(System.Int32 teamId) { }
        // RVA: 0x09F285CC  token: 0x60006B9
        public virtual System.Boolean CalcNearPoint(UnityEngine.Vector3 pos, UnityEngine.Vector3& p, UnityEngine.Vector3& dir, UnityEngine.Vector3& d, System.Boolean skinning) { }
        // RVA: 0x053982BC  token: 0x60006BA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D3  // size: 0x40
    public class MagicaSphereCollider : MagicaCloth.ColliderComponent
    {
        // Fields
        private System.Single radius;  // 0x38

        // Properties
        System.Single Radius { get; /* RVA: 0x04D88360 */ set; /* RVA: 0x09F29054 */ }

        // Methods
        // RVA: 0x04D91C30  token: 0x60006BB
        public virtual MagicaCloth.ComponentType GetComponentType() { }
        // RVA: 0x09F28570  token: 0x60006BC
        private System.Void OnValidate() { }
        // RVA: 0x09F28D70  token: 0x60006BD
        private virtual System.Void DataUpdate() { }
        // RVA: 0x09F28FAC  token: 0x60006C0
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x09F28C48  token: 0x60006C1
        protected virtual MagicaCloth.ChunkData CreateColliderParticleReal(System.Int32 teamId) { }
        // RVA: 0x09F29014  token: 0x60006C2
        public System.Single GetScale() { }
        // RVA: 0x09F28A90  token: 0x60006C3
        public virtual System.Boolean CalcNearPoint(UnityEngine.Vector3 pos, UnityEngine.Vector3& p, UnityEngine.Vector3& dir, UnityEngine.Vector3& d, System.Boolean skinning) { }
        // RVA: 0x09F29044  token: 0x60006C4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D4  // size: 0x28
    public abstract class ParticleComponent : MagicaCloth.BaseComponent, MagicaCloth.IDataHash
    {
        // Fields
        protected System.Collections.Generic.Dictionary<System.Int32,MagicaCloth.ChunkData> particleDict;  // 0x18
        protected MagicaCloth.RuntimeStatus status;  // 0x20

        // Properties
        MagicaCloth.RuntimeStatus Status { get; /* RVA: 0x04D862C0 */ }
        System.Int32 CenterParticleIndex { get; /* RVA: 0x09F2A33C */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60006C6
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x047FE3A0  token: 0x60006C8
        protected virtual System.Void Start() { }
        // RVA: 0x047FE350  token: 0x60006C9
        public virtual System.Void OnEnable() { }
        // RVA: 0x09EE648C  token: 0x60006CA
        public virtual System.Void OnDisable() { }
        // RVA: 0x09F29F90  token: 0x60006CB
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x047FE3B0  token: 0x60006CC
        private System.Void Init() { }
        // RVA: 0x04D396C0  token: 0x60006CD
        protected System.Void OnUpdateStatus() { }
        // RVA: 0x02FFF600  token: 0x60006CE
        public virtual System.Boolean VerifyData() { }
        // RVA: 0x041E1670  token: 0x60006CF
        protected virtual System.Void OnInit() { }
        // RVA: 0x09F29FC0  token: 0x60006D0
        protected virtual System.Void OnDispose() { }
        // RVA: 0x041E1670  token: 0x60006D1
        protected virtual System.Void OnUpdate() { }
        // RVA: 0x0539824C  token: 0x60006D2
        protected virtual System.Void OnActive() { }
        // RVA: 0x09F29FF0  token: 0x60006D3
        protected virtual System.Void OnInactive() { }
        // RVA: 0x04CA5CE0  token: 0x60006D4
        protected System.Void EnableParticle() { }
        // RVA: 0x09F29C18  token: 0x60006D5
        protected System.Void DisableParticle() { }
        // RVA: 0x09F29E58  token: 0x60006D6
        protected System.Void EnableTeamParticle(System.Int32 teamId) { }
        // RVA: 0x09F29D20  token: 0x60006D7
        protected System.Void DisableTeamParticle(System.Int32 teamId) { }
        // RVA: 0x09F2A1D8  token: 0x60006D8
        protected System.Void ReserveDataUpdate() { }
        // RVA: 0x041E1670  token: 0x60006D9
        private virtual System.Void DataUpdate() { }
        // RVA: 0x09F2A254  token: 0x60006DA
        private System.Void UpdateStatus() { }
        // RVA: 0x09F299E8  token: 0x60006DB
        protected MagicaCloth.ChunkData CreateParticle(System.UInt32 flag, System.Int32 teamId, System.Single depth, Unity.Mathematics.float3 radius, Unity.Mathematics.float3 localPos) { }
        // RVA: 0x09F2A128  token: 0x60006DC
        protected System.Void RemoveTeamParticle(System.Int32 teamId) { }
        // RVA: 0x09F29FF8  token: 0x60006DD
        protected System.Void RemoveParticle() { }
        // RVA: 0x053953F4  token: 0x60006DE
        protected UnityEngine.Transform UserTransform(System.Int32 vindex) { }
        // RVA: 0x09F2A274  token: 0x60006DF
        protected Unity.Mathematics.float3 UserTransformLocalPosition(System.Int32 vindex) { }
        // RVA: 0x09F2A2E0  token: 0x60006E0
        protected Unity.Mathematics.quaternion UserTransformLocalRotation(System.Int32 vindex) { }
        // RVA: 0x049E11B0  token: 0x60006E1
        protected System.Void .ctor() { }
        // RVA: 0x0464E630  token: 0x60006E2
        private System.Object <Init>b__11_0() { }

    }

    // TypeToken: 0x20000D5  // size: 0x78
    public abstract class PhysicsTeam : MagicaCloth.CoreComponent
    {
        // Fields
        protected MagicaCloth.PhysicsTeamData teamData;  // 0x38
        private System.Single userBlendWeight;  // 0x40
        private System.Single userPhysicsBlendWeight;  // 0x44
        private System.Boolean syncBlendWeightToPoseBlendWeight;  // 0x48
        private System.Single userCloseThreshold;  // 0x4c
        private MagicaCloth.PhysicsTeam.TeamUpdateMode updateMode;  // 0x50
        private MagicaCloth.PhysicsTeam.TeamCullingMode cullingMode;  // 0x54
        private MagicaCloth.PhysicsTeam.TeamSkinningMode skinningMode;  // 0x58
        protected System.Int32 teamId;  // 0x5c
        protected MagicaCloth.ChunkData particleChunk;  // 0x60
        protected UnityEngine.Transform influenceTarget;  // 0x70

        // Properties
        System.Int32 TeamId { get; /* RVA: 0x04D86560 */ }
        MagicaCloth.PhysicsTeamData TeamData { get; /* RVA: 0x04D85A50 */ }
        MagicaCloth.ChunkData ParticleChunk { get; /* RVA: 0x04DA1280 */ }
        UnityEngine.Transform InfluenceTarget { get; /* RVA: 0x04D862B0 */ set; /* RVA: 0x063AD760 */ }
        System.Single UserBlendWeight { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x09EC1054 */ }
        System.Single UserPhysicsBlendWeight { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x09F2FBE0 */ }
        System.Boolean SyncBlendWeightToPoseBlendWeight { get; /* RVA: 0x04D867D0 */ set; /* RVA: 0x04D86810 */ }
        System.Single UserCloseThreshold { get; /* RVA: 0x04D87860 */ set; /* RVA: 0x09F2FBC8 */ }
        MagicaCloth.PhysicsTeam.TeamUpdateMode UpdateMode { get; /* RVA: 0x04D86550 */ set; /* RVA: 0x04D865A0 */ }
        MagicaCloth.PhysicsTeam.TeamCullingMode CullingMode { get; /* RVA: 0x04D86540 */ set; /* RVA: 0x04D86590 */ }
        MagicaCloth.PhysicsTeam.TeamSkinningMode SkinningMode { get; /* RVA: 0x04D86570 */ set; /* RVA: 0x04D865D0 */ }
        System.Boolean UseAnimatedPose { get; /* RVA: 0x09F2FBC0 */ }

        // Methods
        // RVA: 0x09F2F85C  token: 0x60006E3
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x09F2FA88  token: 0x60006F8
        protected virtual System.Void OnInit() { }
        // RVA: 0x09F2F9C0  token: 0x60006F9
        protected virtual System.Void OnDispose() { }
        // RVA: 0x041E1670  token: 0x60006FA
        protected virtual System.Void OnUpdate() { }
        // RVA: 0x09F2F978  token: 0x60006FB
        protected virtual System.Void OnActive() { }
        // RVA: 0x09F2FA30  token: 0x60006FC
        protected virtual System.Void OnInactive() { }
        // RVA: 0x09F2F87C  token: 0x60006FD
        public System.Boolean IsActive() { }
        // RVA: 0x09F2F8FC  token: 0x60006FE
        public System.Boolean IsValid() { }
        // RVA: 0x09F2F65C  token: 0x60006FF
        public MagicaCloth.ChunkData CreateParticle(System.Int32 team, System.Int32 count, System.Func<System.Int32,System.UInt32> funcFlag, System.Func<System.Int32,Unity.Mathematics.float3> funcWpos, System.Func<System.Int32,Unity.Mathematics.quaternion> funcWrot, System.Func<System.Int32,System.Single> funcDepth, System.Func<System.Int32,Unity.Mathematics.float3> funcRadius, System.Func<System.Int32,Unity.Mathematics.float3> funcTargetLocalPos) { }
        // RVA: 0x09F2FAE4  token: 0x6000700
        public System.Void RemoveAllParticle() { }
        // RVA: 0x09F2F7D4  token: 0x6000701
        public System.Void EnableParticle(System.Func<System.Int32,UnityEngine.Transform> funcTarget, System.Func<System.Int32,Unity.Mathematics.float3> funcLpos, System.Func<System.Int32,Unity.Mathematics.quaternion> funcLrot) { }
        // RVA: 0x09F2F73C  token: 0x6000702
        public System.Void DisableParticle(System.Func<System.Int32,UnityEngine.Transform> funcTarget, System.Func<System.Int32,Unity.Mathematics.float3> funcLpos, System.Func<System.Int32,Unity.Mathematics.quaternion> funcLrot) { }
        // RVA: 0x09F2FB44  token: 0x6000703
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: 0x09F2FB4C  token: 0x6000704
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000D9  // size: 0x30
    public class PhysicsTeamData : MagicaCloth.IDataHash
    {
        // Fields
        private System.Collections.Generic.List<MagicaCloth.ColliderComponent> colliderList;  // 0x10
        private System.Collections.Generic.List<MagicaCloth.ColliderComponent> penetrationIgnoreColliderList;  // 0x18
        private System.Boolean mergeAvatarCollider;  // 0x20
        private System.Collections.Generic.List<MagicaCloth.ColliderComponent> addColliderList;  // 0x28

        // Properties
        System.Int32 ColliderCount { get; /* RVA: 0x09F2F61C */ }
        System.Collections.Generic.List<MagicaCloth.ColliderComponent> ColliderList { get; /* RVA: 0x02B2ECC0 */ }
        System.Collections.Generic.List<MagicaCloth.ColliderComponent> PenetrationIgnoreColliderList { get; /* RVA: 0x0385B100 */ }
        System.Boolean MergeAvatarCollider { get; /* RVA: 0x04D866B0 */ }

        // Methods
        // RVA: 0x09F2F1D8  token: 0x6000705
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x09F2F214  token: 0x6000706
        public System.Void Init(System.Int32 teamId) { }
        // RVA: 0x09F2EFA4  token: 0x6000707
        public System.Void Dispose(System.Int32 teamId) { }
        // RVA: 0x09F2EF00  token: 0x6000708
        public System.Void AddCollider(MagicaCloth.ColliderComponent collider) { }
        // RVA: 0x09F2F31C  token: 0x6000709
        public System.Void RemoveCollider(MagicaCloth.ColliderComponent collider) { }
        // RVA: 0x09F2F434  token: 0x600070A
        private System.Void UpdateStatus() { }
        // RVA: 0x09F2F528  token: 0x600070F
        public System.Void ValidateColliderList() { }
        // RVA: 0x09F2F564  token: 0x6000710
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000DA  // size: 0x30
    public class AdjustRotationWorker : MagicaCloth.PhysicsManagerWorker
    {
        // Fields
        private static System.Int32 AdjustMode_Fixed;  // const
        private static System.Int32 AdjustMode_XYMove;  // const
        private static System.Int32 AdjustMode_XZMove;  // const
        private static System.Int32 AdjustMode_YZMove;  // const
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.AdjustRotationWorker.AdjustRotationData> dataList;  // 0x18
        public MagicaCloth.FixedNativeList<MagicaCloth.AdjustRotationWorker.GroupData> groupList;  // 0x20
        private MagicaCloth.ExNativeMultiHashMap<System.Int32,System.Int32> particleMap;  // 0x28

        // Methods
        // RVA: 0x03E87400  token: 0x6000711
        public virtual System.Void Create() { }
        // RVA: 0x09F2340C  token: 0x6000712
        public virtual System.Void Release() { }
        // RVA: 0x09F22D08  token: 0x6000713
        public System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, System.Int32 adjustMode, Unity.Mathematics.float3 axisRotationPower, MagicaCloth.AdjustRotationWorker.AdjustRotationData[] dataArray) { }
        // RVA: 0x09F23494  token: 0x6000714
        public virtual System.Void RemoveGroup(System.Int32 teamId) { }
        // RVA: 0x09F22F94  token: 0x6000715
        public System.Void ChangeParam(System.Int32 teamId, System.Boolean active, System.Int32 adjustMode, Unity.Mathematics.float3 axisRotationPower) { }
        // RVA: 0x041E1670  token: 0x6000716
        public virtual System.Void Warmup() { }
        // RVA: 0x04D9A8E0  token: 0x6000717
        public virtual Unity.Jobs.JobHandle PreUpdate(Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x09F23128  token: 0x6000718
        public virtual Unity.Jobs.JobHandle PostUpdate(Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x041E1670  token: 0x6000719
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000DE  // size: 0x30
    public class BaseSkinningWorker : MagicaCloth.PhysicsManagerWorker
    {
        // Fields
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.BaseSkinningWorker.BaseSkinningData> dataList;  // 0x18
        private MagicaCloth.FixedChunkNativeArray<Unity.Mathematics.float4x4> bindPoseList;  // 0x20
        public MagicaCloth.FixedNativeList<MagicaCloth.BaseSkinningWorker.GroupData> groupList;  // 0x28

        // Methods
        // RVA: 0x04B4E220  token: 0x600071C
        public virtual System.Void Create() { }
        // RVA: 0x09F24784  token: 0x600071D
        public virtual System.Void Release() { }
        // RVA: 0x09F240F4  token: 0x600071E
        public System.Int32 AddGroup(System.Int32 teamId, System.Boolean active, System.Boolean updateFixed, MagicaCloth.BaseSkinningWorker.BaseSkinningData[] skinningDataList, Unity.Mathematics.float4x4[] skinningBindPoseList) { }
        // RVA: 0x09F2480C  token: 0x600071F
        public virtual System.Void RemoveGroup(System.Int32 teamId) { }
        // RVA: 0x09F242D8  token: 0x6000720
        public System.Void ChangeParam(System.Int32 teamId, System.Boolean updateFixed) { }
        // RVA: 0x041E1670  token: 0x6000721
        public virtual System.Void Warmup() { }
        // RVA: 0x09F24430  token: 0x6000722
        public virtual Unity.Jobs.JobHandle PreUpdate(Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x04D9A8E0  token: 0x6000723
        public virtual Unity.Jobs.JobHandle PostUpdate(Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x041E1670  token: 0x6000724
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E2  // size: 0x38
    public class LineWorker : MagicaCloth.PhysicsManagerWorker
    {
        // Fields
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.LineWorker.LineRotationData> dataList;  // 0x18
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.LineWorker.LineRotationRootInfo> rootInfoList;  // 0x20
        public MagicaCloth.FixedNativeList<MagicaCloth.LineWorker.GroupData> groupList;  // 0x28
        private MagicaCloth.FixedChunkNativeArray<System.Int32> rootTeamList;  // 0x30

        // Methods
        // RVA: 0x0442EBF0  token: 0x6000727
        public virtual System.Void Create() { }
        // RVA: 0x09F27548  token: 0x6000728
        public virtual System.Void Release() { }
        // RVA: 0x09F26FA0  token: 0x6000729
        public System.Int32 AddGroup(System.Int32 teamId, System.Boolean avarage, MagicaCloth.LineWorker.LineRotationData[] dataArray, MagicaCloth.LineWorker.LineRotationRootInfo[] rootInfoArray) { }
        // RVA: 0x09F275F0  token: 0x600072A
        public virtual System.Void RemoveGroup(System.Int32 teamId) { }
        // RVA: 0x09F27194  token: 0x600072B
        public System.Void ChangeParam(System.Int32 teamId, System.Boolean avarage) { }
        // RVA: 0x041E1670  token: 0x600072C
        public virtual System.Void Warmup() { }
        // RVA: 0x04D9A8E0  token: 0x600072D
        public virtual Unity.Jobs.JobHandle PreUpdate(Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x09F272F4  token: 0x600072E
        public virtual Unity.Jobs.JobHandle PostUpdate(Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x041E1670  token: 0x600072F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E7  // size: 0x30
    public class MeshParticleWorker : MagicaCloth.PhysicsManagerWorker
    {
        // Fields
        private MagicaCloth.ExNativeMultiHashMap<System.Int32,System.Int32> vertexToParticleMap;  // 0x18
        private MagicaCloth.FixedNativeListWithCount<System.Int32> vertexToParticleList;  // 0x20
        private System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<MagicaCloth.MeshParticleWorker.CreateData>> groupCreateDict;  // 0x28

        // Methods
        // RVA: 0x03E87510  token: 0x6000731
        public virtual System.Void Create() { }
        // RVA: 0x09F297E8  token: 0x6000732
        public virtual System.Void Release() { }
        // RVA: 0x09F29060  token: 0x6000733
        public System.Void Add(System.Int32 group, System.Int32 vindex, System.Int32 pindex) { }
        // RVA: 0x09F29850  token: 0x6000734
        public virtual System.Void RemoveGroup(System.Int32 group) { }
        // RVA: 0x041E1670  token: 0x6000735
        public virtual System.Void Warmup() { }
        // RVA: 0x09F29560  token: 0x6000736
        public virtual Unity.Jobs.JobHandle PreUpdate(Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x09F291E8  token: 0x6000737
        public virtual Unity.Jobs.JobHandle PostUpdate(Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x03B927E0  token: 0x6000738
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000EB  // size: 0x18
    public class RenderMeshWorker : MagicaCloth.PhysicsManagerWorker
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600073B
        public virtual System.Void Create() { }
        // RVA: 0x041E1670  token: 0x600073C
        public virtual System.Void Release() { }
        // RVA: 0x041E1670  token: 0x600073D
        public virtual System.Void RemoveGroup(System.Int32 group) { }
        // RVA: 0x09F328E8  token: 0x600073E
        private System.Boolean IsPerformMeshProcessForEachParticle() { }
        // RVA: 0x09F331C0  token: 0x600073F
        public virtual System.Void Warmup() { }
        // RVA: 0x04D9A8E0  token: 0x6000740
        public virtual Unity.Jobs.JobHandle PreUpdate(Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x09F32970  token: 0x6000741
        public virtual Unity.Jobs.JobHandle PostUpdate(Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x09F31370  token: 0x6000742
        private static System.Void CollectionVertex(MagicaCloth.PhysicsManagerMeshData.RenderMeshInfo& r_minfo, Unity.Collections.NativeArray<System.UInt32>& sharedChildVertexInfoList, Unity.Collections.NativeArray<MagicaCloth.MeshData.VertexWeight>& sharedChildVertexWeightList, Unity.Collections.NativeArray<Unity.Mathematics.float3>& virtualPosList, Unity.Collections.NativeArray<Unity.Mathematics.quaternion>& virtualRotList, Unity.Mathematics.float3& tpos, Unity.Mathematics.quaternion& trot, Unity.Mathematics.float3& tscl, Unity.Mathematics.quaternion& itrot, System.Single scaleRatio, Unity.Mathematics.float3& scaleDirection, System.Boolean calcNormal, System.Boolean calcTangent, System.Int32 vindex, System.Int32 i, System.UInt32 flag, System.Boolean isFasterWrite, Unity.Collections.NativeArray<Unity.Mathematics.float3>& renderPosList, Unity.Collections.NativeArray<Unity.Mathematics.float3>& renderNormalList, Unity.Collections.NativeArray<Unity.Mathematics.float3>& renderPosArray, Unity.Collections.NativeArray<Unity.Mathematics.float3>& renderNomalArray, Unity.Collections.NativeArray<Unity.Mathematics.float4>& renderTangentList) { }
        // RVA: 0x041E1670  token: 0x6000743
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000EF  // size: 0x30
    public class SpringMeshWorker : MagicaCloth.PhysicsManagerWorker
    {
        // Fields
        private MagicaCloth.ExNativeMultiHashMap<System.Int32,MagicaCloth.SpringMeshWorker.SpringData> springMap;  // 0x18
        private MagicaCloth.FixedNativeListWithCount<System.Int32> springVertexList;  // 0x20
        private System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<System.Int32>> groupIndexDict;  // 0x28

        // Methods
        // RVA: 0x03E875F0  token: 0x6000747
        public virtual System.Void Create() { }
        // RVA: 0x09F34230  token: 0x6000748
        public virtual System.Void Release() { }
        // RVA: 0x09F33D6C  token: 0x6000749
        public System.Void Add(System.Int32 group, System.Int32 vertexIndex, System.Int32 particleIndex, System.Single weight) { }
        // RVA: 0x09F34298  token: 0x600074A
        public virtual System.Void RemoveGroup(System.Int32 group) { }
        // RVA: 0x041E1670  token: 0x600074B
        public virtual System.Void Warmup() { }
        // RVA: 0x04D9A8E0  token: 0x600074C
        public virtual Unity.Jobs.JobHandle PreUpdate(Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x09F33EFC  token: 0x600074D
        public virtual Unity.Jobs.JobHandle PostUpdate(Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x03B92760  token: 0x600074E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F2  // size: 0x30
    public class TriangleWorker : MagicaCloth.PhysicsManagerWorker
    {
        // Fields
        private MagicaCloth.FixedChunkNativeArray<MagicaCloth.TriangleWorker.TriangleRotationData> triangleDataList;  // 0x18
        private MagicaCloth.FixedChunkNativeArray<System.Int32> triangleIndexList;  // 0x20
        public MagicaCloth.FixedNativeList<MagicaCloth.TriangleWorker.GroupData> groupList;  // 0x28

        // Methods
        // RVA: 0x0442ED50  token: 0x6000750
        public virtual System.Void Create() { }
        // RVA: 0x09F35018  token: 0x6000751
        public virtual System.Void Release() { }
        // RVA: 0x09F34B74  token: 0x6000752
        public System.Int32 AddGroup(System.Int32 teamId, MagicaCloth.TriangleWorker.TriangleRotationData[] dataArray, System.Int32[] indexArray) { }
        // RVA: 0x09F350A0  token: 0x6000753
        public virtual System.Void RemoveGroup(System.Int32 teamId) { }
        // RVA: 0x041E1670  token: 0x6000754
        public virtual System.Void Warmup() { }
        // RVA: 0x04D9A8E0  token: 0x6000755
        public virtual Unity.Jobs.JobHandle PreUpdate(Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x09F34D98  token: 0x6000756
        public virtual Unity.Jobs.JobHandle PostUpdate(Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x041E1670  token: 0x6000757
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F6  // size: 0x18
    public class VirtualMeshWorker : MagicaCloth.PhysicsManagerWorker
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600075A
        public virtual System.Void Create() { }
        // RVA: 0x041E1670  token: 0x600075B
        public virtual System.Void Release() { }
        // RVA: 0x041E1670  token: 0x600075C
        public virtual System.Void RemoveGroup(System.Int32 group) { }
        // RVA: 0x09F4A15C  token: 0x600075D
        private System.Boolean IsPerformMeshProcessForEachParticle() { }
        // RVA: 0x041E1670  token: 0x600075E
        public virtual System.Void Warmup() { }
        // RVA: 0x09F4ABB8  token: 0x600075F
        public virtual Unity.Jobs.JobHandle PreUpdate(Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x09F4A1E4  token: 0x6000760
        public virtual Unity.Jobs.JobHandle PostUpdate(Unity.Jobs.JobHandle jobHandle) { }
        // RVA: 0x041E1670  token: 0x6000761
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000FB  // size: 0x40
    public class AutoMove : UnityEngine.MonoBehaviour
    {
        // Fields
        private UnityEngine.Vector3 direction;  // 0x18
        private System.Single length;  // 0x24
        private System.Single interval;  // 0x28
        private UnityEngine.Vector3 startPosition;  // 0x2c
        private System.Single time;  // 0x38

        // Methods
        // RVA: 0x09F35584  token: 0x6000766
        private System.Void Start() { }
        // RVA: 0x09F355C4  token: 0x6000767
        private System.Void Update() { }
        // RVA: 0x09F3555C  token: 0x6000768
        public System.Void OnMoveButton() { }
        // RVA: 0x09F3571C  token: 0x6000769
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000FC  // size: 0x30
    public class AutoRotate : UnityEngine.MonoBehaviour
    {
        // Fields
        private UnityEngine.Vector3 rotateAngle;  // 0x18
        private System.Single interval;  // 0x24
        private System.Single time;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x600076A
        private System.Void Start() { }
        // RVA: 0x09F3575C  token: 0x600076B
        private System.Void Update() { }
        // RVA: 0x09F3555C  token: 0x600076C
        public System.Void OnMoveButton() { }
        // RVA: 0x09F35818  token: 0x600076D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000FD  // size: 0x28
    public class BlastWaveController : UnityEngine.MonoBehaviour
    {
        // Fields
        public MagicaCloth.MagicaAreaWind wind;  // 0x18
        public System.Single attenuationStartTime;  // 0x20
        public System.Single attenuationTime;  // 0x24

        // Methods
        // RVA: 0x09F35B48  token: 0x600076E
        private System.Collections.IEnumerator Start() { }
        // RVA: 0x04D848C0  token: 0x600076F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000FF  // size: 0x98
    public class CameraOrbit : UnityEngine.MonoBehaviour
    {
        // Fields
        private UnityEngine.Transform cameraTransform;  // 0x18
        public UnityEngine.Transform cameraTarget;  // 0x20
        public UnityEngine.Vector3 cameraTargetPos;  // 0x28
        public UnityEngine.Vector3 cameraTargetOffset;  // 0x34
        private System.Single cameraDist;  // 0x40
        private System.Single cameraPitch;  // 0x44
        private System.Single cameraYaw;  // 0x48
        private System.Single cameraDistHokanTime;  // 0x4c
        private System.Single cameraAngleHokanTime;  // 0x50
        private System.Single cameraDistSpeed;  // 0x54
        private System.Single cameraDistMax;  // 0x58
        private System.Single cameraDistMin;  // 0x5c
        private System.Single cameraYawSpeed;  // 0x60
        private System.Single cameraPitchSpeed;  // 0x64
        private System.Single cameraMaxAngleSpeed;  // 0x68
        private System.Single cameraPitchMax;  // 0x6c
        private System.Single cameraPitchMin;  // 0x70
        private MagicaCloth.CameraOrbit.MoveMode moveMode;  // 0x74
        private System.Single moveSpeed;  // 0x78
        private System.Single setCameraDist;  // 0x7c
        private System.Single setCameraPitch;  // 0x80
        private System.Single setCameraYaw;  // 0x84
        private System.Single cameraDistVelocity;  // 0x88
        private System.Single cameraPitchVelocity;  // 0x8c
        private System.Single cameraYawVelocity;  // 0x90

        // Methods
        // RVA: 0x09F37398  token: 0x6000776
        private System.Void Start() { }
        // RVA: 0x09F37070  token: 0x6000777
        private System.Void OnEnable() { }
        // RVA: 0x09F36DC0  token: 0x6000778
        private System.Void OnDisable() { }
        // RVA: 0x09F36DB8  token: 0x6000779
        private System.Void LateUpdate() { }
        // RVA: 0x09F374F8  token: 0x600077A
        private System.Void updateCamera() { }
        // RVA: 0x09F379B0  token: 0x600077B
        private System.Void updatePitchYaw(UnityEngine.Vector2 speed) { }
        // RVA: 0x09F377D4  token: 0x600077C
        private System.Void updateOffset(UnityEngine.Vector2 speed) { }
        // RVA: 0x09F37A1C  token: 0x600077D
        private System.Void updateZoom(System.Single speed) { }
        // RVA: 0x09F372B0  token: 0x600077E
        private System.Void OnTouchMove(System.Int32 fid, UnityEngine.Vector2 screenPos, UnityEngine.Vector2 screenVelocity, UnityEngine.Vector2 cmVelocity) { }
        // RVA: 0x09F37000  token: 0x600077F
        private System.Void OnDoubleTouchMove(System.Int32 fid, UnityEngine.Vector2 screenPos, UnityEngine.Vector2 screenVelocity, UnityEngine.Vector2 cmVelocity) { }
        // RVA: 0x09F3732C  token: 0x6000780
        private System.Void OnTouchPinch(System.Single speedscr, System.Single speedcm) { }
        // RVA: 0x09F37490  token: 0x6000781
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000101  // size: 0x38
    public class CopyObject : UnityEngine.MonoBehaviour
    {
        // Fields
        public System.Int32 seed;  // 0x18
        public System.Int32 count;  // 0x1c
        public System.Single radius;  // 0x20
        public UnityEngine.GameObject prefab;  // 0x28
        public System.Int32 delayFrame;  // 0x30

        // Methods
        // RVA: 0x041E1670  token: 0x6000782
        private System.Void Awake() { }
        // RVA: 0x09F37C8C  token: 0x6000783
        private System.Void Start() { }
        // RVA: 0x09F37C30  token: 0x6000784
        private System.Collections.IEnumerator CreateObject() { }
        // RVA: 0x09F37CB0  token: 0x6000785
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000103  // size: 0x38
    public class DressUpControl : UnityEngine.MonoBehaviour
    {
        // Fields
        private UnityEngine.GameObject partsItemPrefab;  // 0x18
        private UnityEngine.UI.VerticalLayoutGroup verticalLayoutGroup;  // 0x20
        private MagicaCloth.MagicaAvatar avatar;  // 0x28
        public System.Collections.Generic.List<MagicaCloth.DressUpControl.AvatarPartsGroup> avatarPartsGroupList;  // 0x30

        // Methods
        // RVA: 0x09F38730  token: 0x600078C
        private System.Void Start() { }
        // RVA: 0x041E1670  token: 0x600078D
        private System.Void Update() { }
        // RVA: 0x09F386F8  token: 0x600078E
        private System.Void OnDestroy() { }
        // RVA: 0x09F38504  token: 0x600078F
        private System.Void Init() { }
        // RVA: 0x09F382C4  token: 0x6000790
        private System.Void ChangeParts(System.Int32 id, System.Int32 dir) { }
        // RVA: 0x09F383C0  token: 0x6000791
        public System.Void Clear() { }
        // RVA: 0x09F38740  token: 0x6000792
        public System.Void .ctor() { }
        // RVA: 0x09F38738  token: 0x6000793
        private System.Void <Init>b__8_0(System.Int32 id, System.Int32 dir) { }

    }

    // TypeToken: 0x2000105  // size: 0x28
    public class ModelController : UnityEngine.MonoBehaviour
    {
        // Fields
        private System.Collections.Generic.List<UnityEngine.GameObject> characterList;  // 0x18
        private System.Single slowTime;  // 0x20
        private System.Boolean slow;  // 0x24

        // Methods
        // RVA: 0x041E1670  token: 0x6000795
        private System.Void Start() { }
        // RVA: 0x041E1670  token: 0x6000796
        private System.Void Update() { }
        // RVA: 0x09F4569C  token: 0x6000797
        private System.Void AnimatorAction(System.Action<UnityEngine.Animator> act) { }
        // RVA: 0x09F45840  token: 0x6000798
        private System.Void ClothAction(System.Action<MagicaCloth.BaseCloth> act) { }
        // RVA: 0x09F45BE4  token: 0x6000799
        public System.Void OnNextButton() { }
        // RVA: 0x09F45AE8  token: 0x600079A
        public System.Void OnBackButton() { }
        // RVA: 0x09F45CE0  token: 0x600079B
        public System.Void OnSlowButton() { }
        // RVA: 0x09F459EC  token: 0x600079C
        public System.Void OnActiveButton() { }
        // RVA: 0x09F45DFC  token: 0x600079D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000108  // size: 0x10
    public static class ObjectUtility
    {
        // Methods
        // RVA: 0x09F47188  token: 0x60007A6
        public static UnityEngine.GameObject[] PlaceRandomCubes(System.Int32 count, UnityEngine.Vector3 center, System.Single radius) { }
        // RVA: 0x09F4732C  token: 0x60007A7
        public static UnityEngine.GameObject[] PlaceRandomCubes(System.Int32 count) { }
        // RVA: 0x09F470DC  token: 0x60007A8
        public static UnityEngine.GameObject MakeStrippedCube() { }
        // RVA: 0x09F47358  token: 0x60007A9
        public static UnityEngine.GameObject[] PlaceRandomGameObject(System.Int32 count, UnityEngine.Vector3 center, System.Single radius, UnityEngine.GameObject prefab) { }

    }

    // TypeToken: 0x2000109  // size: 0x70
    public class SimpleInputManager : MagicaCloth.CreateSingleton`1
    {
        // Fields
        private static System.Int32 MaxFinger;  // const
        public System.Single tapRadiusCm;  // 0x18
        public System.Single flickRangeCm;  // 0x1c
        public System.Single flickCheckSpeed;  // 0x20
        public System.Single mouseWheelSpeed;  // 0x24
        private System.Int32 mainFingerId;  // 0x28
        private System.Int32 subFingerId;  // 0x2c
        private UnityEngine.Vector2[] downPos;  // 0x30
        private UnityEngine.Vector2[] lastPos;  // 0x38
        private UnityEngine.Vector2[] flickDownPos;  // 0x40
        private System.Single[] flickDownTime;  // 0x48
        private System.Single lastTime;  // 0x50
        private System.Boolean mobilePlatform;  // 0x54
        private System.Boolean[] mouseDown;  // 0x58
        private UnityEngine.Vector2[] mouseOldMovePos;  // 0x60
        private System.Single screenDpi;  // 0x68
        private System.Single screenDpc;  // 0x6c
        public static UnityEngine.Events.UnityAction<System.Int32,UnityEngine.Vector2> OnTouchDown;  // static @ 0x0
        public static UnityEngine.Events.UnityAction<System.Int32,UnityEngine.Vector2,UnityEngine.Vector2,UnityEngine.Vector2> OnTouchMove;  // static @ 0x8
        public static UnityEngine.Events.UnityAction<System.Int32,UnityEngine.Vector2,UnityEngine.Vector2,UnityEngine.Vector2> OnDoubleTouchMove;  // static @ 0x10
        public static UnityEngine.Events.UnityAction<System.Int32,UnityEngine.Vector2> OnTouchUp;  // static @ 0x18
        public static UnityEngine.Events.UnityAction<System.Int32,UnityEngine.Vector2> OnTouchMoveCancel;  // static @ 0x20
        public static UnityEngine.Events.UnityAction<System.Int32,UnityEngine.Vector2> OnTouchTap;  // static @ 0x28
        public static UnityEngine.Events.UnityAction<System.Int32,UnityEngine.Vector2,UnityEngine.Vector2,UnityEngine.Vector2> OnTouchFlick;  // static @ 0x30
        public static UnityEngine.Events.UnityAction<System.Single,System.Single> OnTouchPinch;  // static @ 0x38
        public static UnityEngine.Events.UnityAction OnBackButton;  // static @ 0x40

        // Properties
        System.Single ScreenDpi { get; /* RVA: 0x09F496E8 */ }
        System.Single ScreenDpc { get; /* RVA: 0x09F496A4 */ }

        // Methods
        // RVA: 0x04D7CA40  token: 0x60007AA
        private static System.Void Init() { }
        // RVA: 0x09F4838C  token: 0x60007AB
        protected virtual System.Void InitSingleton() { }
        // RVA: 0x09F49624  token: 0x60007AC
        private System.Void Update() { }
        // RVA: 0x09F48158  token: 0x60007AF
        private System.Void CalcScreenDpi() { }
        // RVA: 0x09F48118  token: 0x60007B0
        private System.Void AllResetTouchInfo() { }
        // RVA: 0x09F48384  token: 0x60007B1
        public System.Int32 GetTouchCount() { }
        // RVA: 0x09F484A0  token: 0x60007B2
        public System.Boolean IsUI() { }
        // RVA: 0x09F4853C  token: 0x60007B3
        private System.Void UpdateMobile() { }
        // RVA: 0x09F48198  token: 0x60007B4
        private UnityEngine.Vector2 CalcScreenRatioVector(UnityEngine.Vector2 vec) { }
        // RVA: 0x09F481E8  token: 0x60007B5
        private System.Boolean CheckFlic(System.Int32 fid, UnityEngine.Vector2 oldpos, UnityEngine.Vector2 nowpos, UnityEngine.Vector2 downpos, System.Single flicktime) { }
        // RVA: 0x09F48DC8  token: 0x60007B6
        private System.Void UpdateMouse() { }
        // RVA: 0x09F49644  token: 0x60007B7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200010A  // size: 0x38
    public class SliderStart : UnityEngine.MonoBehaviour
    {
        // Fields
        private UnityEngine.UI.Text text;  // 0x18
        private System.String lable;  // 0x20
        private System.String format;  // 0x28
        private System.String formatString;  // 0x30

        // Methods
        // RVA: 0x09F497F8  token: 0x60007B8
        private System.Void Start() { }
        // RVA: 0x09F4972C  token: 0x60007B9
        private System.Void OnChangeValue(System.Single value) { }
        // RVA: 0x09F49974  token: 0x60007BA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200010B  // size: 0x38
    public class UIPartsItem : UnityEngine.MonoBehaviour
    {
        // Fields
        public UnityEngine.UI.Text text;  // 0x18
        public UnityEngine.UI.Button prefButton;  // 0x20
        public UnityEngine.UI.Button nextButton;  // 0x28
        private System.Int32 id;  // 0x30

        // Methods
        // RVA: 0x041E1670  token: 0x60007BB
        private System.Void Start() { }
        // RVA: 0x09F49FE8  token: 0x60007BC
        public System.Void Init(System.String title, System.Int32 id, System.Action<System.Int32,System.Int32> onClick) { }
        // RVA: 0x05393520  token: 0x60007BD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200010D  // size: 0x58
    public class WindController : UnityEngine.MonoBehaviour
    {
        // Fields
        private UnityEngine.WindZone unityWindZone;  // 0x18
        private System.Single unityWindZoneScale;  // 0x20
        private UnityEngine.Renderer arrowRenderer;  // 0x28
        private UnityEngine.Gradient arrowGradient;  // 0x30
        private System.Collections.Generic.List<UnityEngine.Transform> rotationTransforms;  // 0x38
        private UnityEngine.GameObject blastWavePrefab;  // 0x40
        private System.Single blastWaveSpawnRadius;  // 0x48
        private System.Single angleY;  // 0x4c
        private System.Single angleX;  // 0x50

        // Properties
        MagicaCloth.MagicaDirectionalWind Wind { get; /* RVA: 0x09F4B574 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60007C1
        private System.Void Start() { }
        // RVA: 0x09F4B108  token: 0x60007C2
        public System.Void OnDirectionY(System.Single value) { }
        // RVA: 0x09F4B0FC  token: 0x60007C3
        public System.Void OnDirectionX(System.Single value) { }
        // RVA: 0x09F4B14C  token: 0x60007C4
        public System.Void OnMain(System.Single value) { }
        // RVA: 0x09F4B294  token: 0x60007C5
        public System.Void OnTurbulence(System.Single value) { }
        // RVA: 0x09F4B114  token: 0x60007C6
        public System.Void OnFrequency(System.Single value) { }
        // RVA: 0x09F4AFBC  token: 0x60007C7
        public System.Void OnBlastWave() { }
        // RVA: 0x09F4B2CC  token: 0x60007C9
        private System.Void UpdateDirection() { }
        // RVA: 0x09F4B4AC  token: 0x60007CA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200010E  // size: 0x28
    public class BezierParam : MagicaCloth.IDataHash
    {
        // Fields
        private System.Single startValue;  // 0x10
        private System.Single endValue;  // 0x14
        private System.Boolean useEndValue;  // 0x18
        private System.Single curveValue;  // 0x1c
        private System.Boolean useCurveValue;  // 0x20

        // Properties
        System.Single StartValue { get; /* RVA: 0x04D85F70 */ }
        System.Single EndValue { get; /* RVA: 0x09F35B24 */ }
        System.Single CurveValue { get; /* RVA: 0x09F35B0C */ }
        System.Boolean UseCurve { get; /* RVA: 0x09F35B38 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60007CB
        public System.Void .ctor() { }
        // RVA: 0x04DA3710  token: 0x60007CC
        public System.Void .ctor(System.Single val) { }
        // RVA: 0x04DA3730  token: 0x60007CD
        public System.Void .ctor(System.Single sval, System.Single eval) { }
        // RVA: 0x04DA36E0  token: 0x60007CE
        public System.Void .ctor(System.Single sval, System.Single eval, System.Boolean useEval, System.Single cval, System.Boolean useCval) { }
        // RVA: 0x04DA36E0  token: 0x60007CF
        public System.Void SetParam(System.Single sval, System.Single eval, System.Boolean useEval, System.Single cval, System.Boolean useCval) { }
        // RVA: 0x09F35998  token: 0x60007D4
        public System.Single Evaluate(System.Single x) { }
        // RVA: 0x09F358C0  token: 0x60007D5
        public MagicaCloth.BezierParam AutoSetup(System.Single startVal, System.Single endVal, System.Single curveVal) { }
        // RVA: 0x09F359F0  token: 0x60007D6
        public virtual System.Int32 GetDataHash() { }
        // RVA: 0x09F35924  token: 0x60007D7
        public MagicaCloth.BezierParam Clone() { }

    }

    // TypeToken: 0x200010F
    public interface IBoneReplace
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60007D8
        public virtual System.Collections.Generic.HashSet<UnityEngine.Transform> GetUsedBones() { }
        // RVA: -1  // abstract  token: 0x60007D9
        public virtual System.Void ReplaceBone(System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict) { }

    }

    // TypeToken: 0x2000110
    public interface IDataVerify
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60007DA
        public virtual System.Int32 GetVersion() { }
        // RVA: -1  // abstract  token: 0x60007DB
        public virtual System.Void CreateVerifyData() { }
        // RVA: -1  // abstract  token: 0x60007DC
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: -1  // abstract  token: 0x60007DD
        public virtual System.String GetInformation() { }

    }

    // TypeToken: 0x2000111
    public interface IEditorCloth
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60007DE
        public virtual System.Collections.Generic.List<System.Int32> GetSelectionList() { }
        // RVA: -1  // abstract  token: 0x60007DF
        public virtual System.Collections.Generic.List<System.Int32> GetUseList() { }

    }

    // TypeToken: 0x2000112
    public interface IEditorMesh
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60007E0
        public virtual System.Int32 GetEditorPositionNormalTangent(System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList) { }
        // RVA: -1  // abstract  token: 0x60007E1
        public virtual System.Collections.Generic.List<System.Int32> GetEditorTriangleList() { }
        // RVA: -1  // abstract  token: 0x60007E2
        public virtual System.Collections.Generic.List<System.Int32> GetEditorLineList() { }

    }

    // TypeToken: 0x2000113  // size: 0x48
    public class RuntimeStatus
    {
        // Fields
        private System.Boolean initStart;  // 0x10
        private System.Boolean init;  // 0x11
        private System.Boolean initError;  // 0x12
        private System.Boolean enable;  // 0x13
        private System.Boolean userEnable;  // 0x14
        private System.Boolean runtimeError;  // 0x15
        private System.Boolean dispose;  // 0x16
        private System.Boolean isActive;  // 0x17
        private System.Boolean isDirty;  // 0x18
        private System.Collections.Generic.HashSet<MagicaCloth.RuntimeStatus> <parentStatusSet>k__BackingField;  // 0x20
        private System.Collections.Generic.HashSet<MagicaCloth.RuntimeStatus> <childStatusSet>k__BackingField;  // 0x28
        private System.Action UpdateStatusAction;  // 0x30
        private System.Action DisconnectedAction;  // 0x38
        private System.Func<System.Object> OwnerFunc;  // 0x40

        // Properties
        System.Collections.Generic.HashSet<MagicaCloth.RuntimeStatus> parentStatusSet { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.Collections.Generic.HashSet<MagicaCloth.RuntimeStatus> childStatusSet { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        System.Boolean IsActive { get; /* RVA: 0x04D39700 */ }
        System.Boolean IsInitStart { get; /* RVA: 0x015EFCE0 */ }
        System.Boolean IsInitComplete { get; /* RVA: 0x04D88380 */ }
        System.Boolean IsInitSuccess { get; /* RVA: 0x09F48104 */ }
        System.Boolean IsInitError { get; /* RVA: 0x047FE4C0 */ }
        System.Boolean IsDispose { get; /* RVA: 0x04DA2FA0 */ }
        System.Boolean IsDirty { get; /* RVA: 0x04D86340 */ }

        // Methods
        // RVA: 0x015EFCD0  token: 0x60007EE
        public System.Void SetInitStart() { }
        // RVA: 0x04D8ECC0  token: 0x60007EF
        public System.Void SetInitComplete() { }
        // RVA: 0x04D99700  token: 0x60007F0
        public System.Void SetInitError() { }
        // RVA: 0x047FE380  token: 0x60007F1
        public System.Boolean SetEnable(System.Boolean sw) { }
        // RVA: 0x09F480B8  token: 0x60007F2
        public System.Boolean SetUserEnable(System.Boolean sw) { }
        // RVA: 0x09F480A8  token: 0x60007F3
        public System.Boolean SetRuntimeError(System.Boolean sw) { }
        // RVA: 0x04DA37F0  token: 0x60007F4
        public System.Void SetDispose() { }
        // RVA: 0x04D8EB60  token: 0x60007F5
        public System.Void SetDirty() { }
        // RVA: 0x04D91AA0  token: 0x60007F6
        public System.Void ClearDirty() { }
        // RVA: 0x047FE4D0  token: 0x60007F7
        public System.Boolean UpdateStatus() { }
        // RVA: 0x09F47ED0  token: 0x60007F8
        public System.Void AddParentStatus(MagicaCloth.RuntimeStatus status) { }
        // RVA: 0x09F48004  token: 0x60007F9
        public System.Void RemoveParentStatus(MagicaCloth.RuntimeStatus status) { }
        // RVA: 0x09F47E7C  token: 0x60007FA
        public System.Void AddChildStatus(MagicaCloth.RuntimeStatus status) { }
        // RVA: 0x09F47F60  token: 0x60007FB
        public System.Void RemoveChildStatus(MagicaCloth.RuntimeStatus status) { }
        // RVA: 0x09F47F24  token: 0x60007FC
        public System.Void LinkParentStatus(MagicaCloth.RuntimeStatus parent) { }
        // RVA: 0x09F480C8  token: 0x60007FD
        public System.Void UnlinkParentStatus(MagicaCloth.RuntimeStatus parent) { }
        // RVA: 0x047FE620  token: 0x60007FE
        private System.Boolean IsParentStatusActive() { }
        // RVA: 0x049E1240  token: 0x60007FF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000114  // size: 0x10
    public static class DataHashExtensions
    {
        // Fields
        public static System.Int32 NullHash;  // const
        public static System.Int32 NumberHash;  // const

        // Methods
        // RVA: 0x09F37CD4  token: 0x6000800
        public static System.Int32 GetDataHash(System.Object data) { }
        // RVA: 0x09F37F08  token: 0x6000801
        public static System.Int32 GetDataHash(MagicaCloth.IDataHash data) { }
        // RVA: -1  // generic def  token: 0x6000802
        public static System.Int32 GetDataHash(T[] data) { }
        // RVA: -1  // generic def  token: 0x6000803
        public static System.Int32 GetDataHash(System.Collections.Generic.List<T> data) { }
        // RVA: -1  // generic def  token: 0x6000804
        public static System.Int32 GetDataCountHash(T[] data) { }
        // RVA: -1  // generic def  token: 0x6000805
        public static System.Int32 GetDataCountHash(System.Collections.Generic.List<T> data) { }
        // RVA: 0x09F37F20  token: 0x6000806
        public static System.UInt64 GetVectorDataHash(UnityEngine.Vector3 v) { }

    }

    // TypeToken: 0x2000115  // size: 0x20
    public class GridHash
    {
        // Fields
        protected System.Collections.Generic.Dictionary<System.UInt32,System.Collections.Generic.List<MagicaCloth.GridHash.Point>> gridMap;  // 0x10
        protected System.Single gridSize;  // 0x18

        // Methods
        // RVA: 0x09F3AA3C  token: 0x6000807
        public virtual System.Void Create(System.Single gridSize) { }
        // RVA: 0x09F3A86C  token: 0x6000808
        public virtual System.Void AddPoint(Unity.Mathematics.float3 pos, System.Int32 id) { }
        // RVA: 0x09F3ABAC  token: 0x6000809
        public virtual System.Void Remove(Unity.Mathematics.float3 pos, System.Int32 id) { }
        // RVA: 0x09F3A9F4  token: 0x600080A
        public System.Void Clear() { }
        // RVA: 0x09F3AB28  token: 0x600080B
        public static Unity.Mathematics.int3 GetGridPos(Unity.Mathematics.float3 pos, System.Single gridSize) { }
        // RVA: 0x04DA37C0  token: 0x600080C
        public static System.UInt32 GetGridHash(Unity.Mathematics.int3 pos) { }
        // RVA: 0x09F3AAC4  token: 0x600080D
        public static System.UInt32 GetGridHash(Unity.Mathematics.float3 pos, System.Single gridSize) { }
        // RVA: 0x04D8D240  token: 0x600080E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000117  // size: 0x40
    public class NearPointSearch : MagicaCloth.GridHash
    {
        // Fields
        private System.Single radius;  // 0x20
        private System.Collections.Generic.Dictionary<System.Int32,System.Int32> nearDict;  // 0x28
        private System.Collections.Generic.Dictionary<System.Int32,System.Single> distDict;  // 0x30
        private System.Collections.Generic.HashSet<System.UInt32> lockPairSet;  // 0x38

        // Methods
        // RVA: 0x09F45F1C  token: 0x6000810
        public System.Void Create(Unity.Mathematics.float3[] positionList, System.Single radius) { }
        // RVA: 0x09F462D8  token: 0x6000811
        public System.Void SearchNearPointAll() { }
        // RVA: 0x09F46558  token: 0x6000812
        public System.Void SearchNearPoint(System.Int32 id, Unity.Mathematics.float3 pos) { }
        // RVA: 0x09F46A24  token: 0x6000813
        public System.Void SearchNearPoint(Unity.Mathematics.float3 pos, System.Single r) { }
        // RVA: 0x09F45EF4  token: 0x6000814
        public virtual System.Void AddPoint(Unity.Mathematics.float3 pos, System.Int32 id) { }
        // RVA: 0x09F46208  token: 0x6000815
        public virtual System.Void Remove(Unity.Mathematics.float3 pos, System.Int32 id) { }
        // RVA: 0x09F45E84  token: 0x6000816
        public System.Void AddLockPair(System.Int32 id1, System.Int32 id2) { }
        // RVA: 0x09F45FA8  token: 0x6000817
        public System.Boolean GetNearPointPair(System.Int32& id1, System.Int32& id2) { }
        // RVA: 0x09F46D80  token: 0x6000818
        public virtual System.String ToString() { }
        // RVA: 0x09F46FC8  token: 0x6000819
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000118  // size: 0x20
    public sealed struct ChunkData
    {
        // Fields
        public System.Int32 chunkNo;  // 0x10
        public System.Int32 startIndex;  // 0x14
        public System.Int32 dataLength;  // 0x18
        public System.Int32 useLength;  // 0x1c

        // Methods
        // RVA: 0x04DA3750  token: 0x600081A
        public System.Void Clear() { }
        // RVA: 0x0423A5F0  token: 0x600081B
        public System.Boolean IsValid() { }
        // RVA: 0x09F37A94  token: 0x600081C
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000119
    public class DoubleComputeBuffer`1 : System.IDisposable
    {
        // Fields
        private UnityEngine.ComputeBuffer buffer0;  // 0x0
        private UnityEngine.ComputeBuffer buffer1;  // 0x0
        private Unity.Collections.NativeArray<T> nativeArrya;  // 0x0
        public System.UInt32 bufferID;  // 0x0
        public System.UInt32 bufferOffset;  // 0x0
        public System.Int32 bufferCount;  // 0x0

        // Properties
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600081D
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x600081E
        public System.Void Swap() { }
        // RVA: -1  // not resolved  token: 0x600081F
        public System.Void Create(System.Int32 size, UnityEngine.ComputeBufferType type, UnityEngine.ComputeBufferMode usage) { }
        // RVA: -1  // not resolved  token: 0x6000820
        public System.Void BeginWrite(System.Int32 length) { }
        // RVA: -1  // not resolved  token: 0x6000821
        public System.Void EndWrite(System.Int32 length) { }
        // RVA: -1  // not resolved  token: 0x6000822
        public Unity.Collections.NativeArray<T> GetNativeArray() { }
        // RVA: -1  // not resolved  token: 0x6000823
        public UnityEngine.ComputeBuffer GetBuffer(System.Int32 bufferIndex) { }
        // RVA: -1  // not resolved  token: 0x6000825
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200011A
    public class ExNativeHashMap`2
    {
        // Fields
        private Unity.Collections.NativeParallelHashMap<TKey,TValue> nativeHashMap;  // 0x0
        private System.Int32 nativeLength;  // 0x0
        private System.Collections.Generic.HashSet<TKey> useKeySet;  // 0x0

        // Properties
        System.Int32 NativeCount { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        Unity.Collections.NativeParallelHashMap<TKey,TValue> Map { get; /* RVA: -1  // not resolved */ }
        System.Collections.Generic.HashSet<TKey> UseKeySet { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000826
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000827
        public System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000829
        public System.Void Add(TKey key, TValue value) { }
        // RVA: -1  // not resolved  token: 0x600082A
        public TValue Get(TKey key) { }
        // RVA: -1  // not resolved  token: 0x600082B
        public System.Void Remove(System.Func<TKey,TValue,System.Boolean> func) { }
        // RVA: -1  // not resolved  token: 0x600082C
        public System.Void Replace(System.Func<TKey,TValue,System.Boolean> func, System.Func<TValue,TValue> datafunc) { }
        // RVA: -1  // not resolved  token: 0x600082D
        public System.Void Remove(TKey key) { }
        // RVA: -1  // not resolved  token: 0x600082F
        public System.Void Clear() { }

    }

    // TypeToken: 0x200011B
    public class ExNativeMultiHashMap`2
    {
        // Fields
        private Unity.Collections.NativeParallelMultiHashMap<TKey,TValue> nativeMultiHashMap;  // 0x0
        private System.Int32 nativeLength;  // 0x0
        private System.Collections.Generic.Dictionary<TKey,System.Int32> useKeyDict;  // 0x0

        // Properties
        System.Int32 NativeCount { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsCreated { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        Unity.Collections.NativeParallelMultiHashMap<TKey,TValue> Map { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000832
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000833
        public System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000836
        public System.Void Add(TKey key, TValue value) { }
        // RVA: -1  // not resolved  token: 0x6000837
        public System.Void Remove(TKey key, TValue value) { }
        // RVA: -1  // not resolved  token: 0x6000838
        public System.Void Remove(System.Func<TKey,TValue,System.Boolean> func) { }
        // RVA: -1  // not resolved  token: 0x6000839
        public System.Void Replace(System.Func<TKey,TValue,System.Boolean> func, System.Func<TValue,TValue> datafunc) { }
        // RVA: -1  // not resolved  token: 0x600083A
        public System.Void Process(System.Action<TKey,TValue> act) { }
        // RVA: -1  // not resolved  token: 0x600083B
        public System.Void Process(TKey key, System.Action<TValue> act) { }
        // RVA: -1  // not resolved  token: 0x600083C
        public System.Boolean Contains(TKey key, TValue value) { }
        // RVA: -1  // not resolved  token: 0x600083D
        public System.Boolean Contains(TKey key) { }
        // RVA: -1  // not resolved  token: 0x600083E
        public System.Void Remove(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000840
        public System.Void Clear() { }

    }

    // TypeToken: 0x200011C
    public class FixedChunkNativeArray`1 : System.IDisposable
    {
        // Fields
        private Unity.Collections.NativeArray<T> nativeArray0;  // 0x0
        private Unity.Collections.NativeArray<T> nativeArray1;  // 0x0
        private System.Int32 nativeLength;  // 0x0
        private System.Collections.Generic.List<MagicaCloth.ChunkData> emptyChunkList;  // 0x0
        private System.Collections.Generic.List<MagicaCloth.ChunkData> useChunkList;  // 0x0
        private System.Int32 chunkSeed;  // 0x0
        private System.Int32 initLength;  // 0x0
        private T emptyElement;  // 0x0
        private System.Int32 useLength;  // 0x0

        // Properties
        System.Int32 Length { get; /* RVA: -1  // not resolved */ }
        System.Int32 ChunkCount { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        T Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000842
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000843
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000844
        public System.Void SetEmptyElement(T empty) { }
        // RVA: -1  // not resolved  token: 0x6000845
        public MagicaCloth.ChunkData AddChunk(System.Int32 length) { }
        // RVA: -1  // not resolved  token: 0x6000846
        public MagicaCloth.ChunkData Add(T data) { }
        // RVA: -1  // not resolved  token: 0x6000847
        public System.Void RemoveChunk(System.Int32 chunkNo) { }
        // RVA: -1  // not resolved  token: 0x6000848
        public System.Void RemoveChunk(MagicaCloth.ChunkData chunk) { }
        // RVA: -1  // not resolved  token: 0x6000849
        public System.Void Fill(MagicaCloth.ChunkData chunk, T data) { }
        // RVA: -1  // not resolved  token: 0x600084F
        public Unity.Collections.NativeArray<T> ToJobArray() { }
        // RVA: -1  // not resolved  token: 0x6000850
        public Unity.Collections.NativeArray<T> ToJobArray(System.Int32 bufferIndex) { }
        // RVA: -1  // not resolved  token: 0x6000851
        public System.Void SwapBuffer() { }
        // RVA: -1  // not resolved  token: 0x6000852
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200011D
    public class FixedMultiNativeList`1 : System.IDisposable
    {
        // Fields
        private Unity.Collections.NativeArray<T> nativeArray;  // 0x0
        private System.Int32 nativeLength;  // 0x0
        private System.Collections.Generic.List<MagicaCloth.ChunkData> emptyChunkList;  // 0x0
        private System.Collections.Generic.List<MagicaCloth.ChunkData> useChunkList;  // 0x0
        private System.Int32 chunkSeed;  // 0x0
        private System.Int32 initLength;  // 0x0
        private T emptyElement;  // 0x0
        private System.Int32 useLength;  // 0x0

        // Properties
        System.Int32 Length { get; /* RVA: -1  // not resolved */ }
        System.Int32 ChunkCount { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        T Item { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000853
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000854
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000855
        public System.Void SetEmptyElement(T empty) { }
        // RVA: -1  // not resolved  token: 0x6000856
        public MagicaCloth.ChunkData AddChunk(System.Int32 length) { }
        // RVA: -1  // not resolved  token: 0x6000857
        public System.Void RemoveChunk(System.Int32 chunkNo) { }
        // RVA: -1  // not resolved  token: 0x6000858
        public System.Void RemoveChunk(MagicaCloth.ChunkData chunk) { }
        // RVA: -1  // not resolved  token: 0x6000859
        public MagicaCloth.ChunkData AddData(MagicaCloth.ChunkData chunk, T data) { }
        // RVA: -1  // not resolved  token: 0x600085A
        public MagicaCloth.ChunkData RemoveData(MagicaCloth.ChunkData chunk, T data) { }
        // RVA: -1  // not resolved  token: 0x600085B
        public MagicaCloth.ChunkData ClearData(MagicaCloth.ChunkData chunk) { }
        // RVA: -1  // not resolved  token: 0x600085C
        public System.Void Process(MagicaCloth.ChunkData chunk, System.Action<T> act) { }
        // RVA: -1  // not resolved  token: 0x6000861
        public Unity.Collections.NativeArray<T> ToJobArray() { }
        // RVA: -1  // not resolved  token: 0x6000862
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200011E
    public class FixedNativeList`1 : System.IDisposable
    {
        // Fields
        private Unity.Collections.NativeArray<T> nativeArray0;  // 0x0
        private Unity.Collections.NativeArray<T> nativeArray1;  // 0x0
        private System.Int32 nativeLength;  // 0x0
        private System.Collections.Generic.Queue<System.Int32> emptyStack;  // 0x0
        private System.Collections.Generic.HashSet<System.Int32> useIndexSet;  // 0x0
        private System.Int32 useLength;  // 0x0

        // Properties
        System.Int32 Length { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        T Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000863
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000864
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000865
        public System.Int32 Add(T element) { }
        // RVA: -1  // not resolved  token: 0x6000866
        public System.Void Remove(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000867
        public System.Boolean Exists(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x600086C
        public System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x600086D
        public Unity.Collections.NativeArray<T> ToJobArray() { }
        // RVA: -1  // not resolved  token: 0x600086E
        public Unity.Collections.NativeArray<T> ToJobArray(System.Int32 bufferIndex) { }
        // RVA: -1  // not resolved  token: 0x600086F
        public System.Void SyncBuffer() { }

    }

    // TypeToken: 0x200011F
    public class FixedNativeListWithCount`1 : System.IDisposable
    {
        // Fields
        private Unity.Collections.NativeList<T> nativeList;  // 0x0
        private System.Int32 nativeLength;  // 0x0
        private System.Collections.Generic.Queue<System.Int32> emptyStack;  // 0x0
        private System.Collections.Generic.HashSet<System.Int32> useIndexSet;  // 0x0
        private System.Collections.Generic.Dictionary<T,System.Int32> indexDict;  // 0x0
        private System.Collections.Generic.Dictionary<T,System.Int32> countDict;  // 0x0
        private T emptyElement;  // 0x0

        // Properties
        System.Int32 Length { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        T Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000870
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000871
        public System.Void .ctor(System.Int32 capacity) { }
        // RVA: -1  // not resolved  token: 0x6000872
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000873
        public System.Void SetEmptyElement(T empty) { }
        // RVA: -1  // not resolved  token: 0x6000874
        public System.Int32 Add(T element) { }
        // RVA: -1  // not resolved  token: 0x6000875
        public System.Void Remove(T element) { }
        // RVA: -1  // not resolved  token: 0x6000876
        public System.Boolean Exist(T element) { }
        // RVA: -1  // not resolved  token: 0x6000877
        public System.Int32 GetUseCount(T element) { }
        // RVA: -1  // not resolved  token: 0x600087C
        public System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x600087D
        public T[] ToArray() { }
        // RVA: -1  // not resolved  token: 0x600087E
        public Unity.Collections.NativeArray<T> ToJobArray() { }

    }

    // TypeToken: 0x2000120  // size: 0x40
    public class FixedTransformAccessArray : System.IDisposable
    {
        // Fields
        private UnityEngine.Jobs.TransformAccessArray transformArray;  // 0x10
        private System.Int32 nativeLength;  // 0x18
        private System.Collections.Generic.Queue<System.Int32> emptyStack;  // 0x20
        private System.Collections.Generic.Dictionary<System.Int32,System.Int32> useIndexDict;  // 0x28
        private System.Collections.Generic.Dictionary<System.Int32,System.Int32> indexDict;  // 0x30
        private System.Collections.Generic.Dictionary<System.Int32,System.Int32> referenceDict;  // 0x38

        // Properties
        System.Int32 Count { get; /* RVA: 0x032338A0 */ }
        System.Int32 Length { get; /* RVA: 0x011EF5B0 */ }
        UnityEngine.Transform Item { get; /* RVA: 0x09F38F88 */ }

        // Methods
        // RVA: 0x03E87C20  token: 0x600087F
        public System.Void .ctor(System.Int32 desiredJobCount) { }
        // RVA: 0x09F38E14  token: 0x6000880
        public System.Void .ctor(System.Int32 capacity, System.Int32 desiredJobCount) { }
        // RVA: 0x09F387C0  token: 0x6000881
        public System.Int32 Add(UnityEngine.Transform element) { }
        // RVA: 0x09F38CEC  token: 0x6000882
        public System.Void Remove(System.Int32 index) { }
        // RVA: 0x09F38C40  token: 0x6000883
        public System.Boolean Exist(System.Int32 index) { }
        // RVA: 0x09F38BD4  token: 0x6000884
        public System.Boolean Exist(UnityEngine.Transform element) { }
        // RVA: 0x09F38C64  token: 0x6000888
        public System.Int32 GetIndex(UnityEngine.Transform element) { }
        // RVA: 0x09F38940  token: 0x6000889
        public System.Void Clear() { }
        // RVA: 0x09F38B2C  token: 0x600088A
        public virtual System.Void Dispose() { }
        // RVA: 0x02B2ECC0  token: 0x600088B
        public UnityEngine.Jobs.TransformAccessArray GetTransformAccessArray() { }

    }

    // TypeToken: 0x2000121  // size: 0x10
    public static class NativeArrayExtension
    {
        // Methods
        // RVA: -1  // generic def  token: 0x600088C
        public static System.Void CopyToFast(Unity.Collections.NativeArray<T> nativeArray, System.Int32 startIndex, T2[] array) { }
        // RVA: -1  // generic def  token: 0x600088D
        public static System.Void CopyToFast(Unity.Collections.NativeArray<T> nativeArray, System.Int32 startIndex, Unity.Collections.NativeArray<T> array) { }
        // RVA: -1  // generic def  token: 0x600088E
        public static System.Void CopyBlock(Unity.Collections.NativeArray<T> nativeArray, System.Int32 sourceIndex, System.Int32 destinationIndex, System.Int32 count) { }
        // RVA: -1  // generic def  token: 0x600088F
        public static System.Void CopyFromFast(Unity.Collections.NativeArray<T> nativeArray, Unity.Collections.NativeArray<T> array) { }
        // RVA: -1  // generic def  token: 0x6000890
        public static System.Void CopyFromFast(Unity.Collections.NativeArray<T> nativeArray, System.Int32 startIndex, T2[] array) { }
        // RVA: -1  // generic def  token: 0x6000891
        public static System.Void SetValue(Unity.Collections.NativeArray<T> nativeArray, System.Int32 startIndex, System.Int32 count, T value) { }

    }

    // TypeToken: 0x2000122
    public abstract class CreateSingleton`1 : UnityEngine.MonoBehaviour
    {
        // Fields
        private static T instance;  // static @ 0x0
        private static T initInstance;  // static @ 0x0
        private static System.Boolean isDestroy;  // static @ 0x0

        // Properties
        T Instance { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000892
        protected static System.Void InitMember() { }
        // RVA: -1  // not resolved  token: 0x6000894
        private static System.Void InitInstance() { }
        // RVA: -1  // not resolved  token: 0x6000895
        public static System.Boolean IsInstance() { }
        // RVA: -1  // not resolved  token: 0x6000896
        protected virtual System.Void Awake() { }
        // RVA: -1  // not resolved  token: 0x6000897
        protected virtual System.Void OnDestroy() { }
        // RVA: -1  // not resolved  token: 0x6000898
        protected virtual System.Void DuplicateDetection(T duplicate) { }
        // RVA: -1  // abstract  token: 0x6000899
        protected virtual System.Void InitSingleton() { }
        // RVA: -1  // not resolved  token: 0x600089A
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000123  // size: 0x10
    public static class DataUtility
    {
        // Methods
        // RVA: 0x0677F488  token: 0x600089B
        public static System.UInt32 PackPair(System.Int32 v0, System.Int32 v1) { }
        // RVA: 0x04DA3080  token: 0x600089C
        public static System.Void UnpackPair(System.UInt32 pack, System.Int32& v0, System.Int32& v1) { }
        // RVA: 0x04D874D0  token: 0x600089D
        public static System.UInt32 Pack16(System.Int32 hi, System.Int32 low) { }
        // RVA: 0x04D875B0  token: 0x600089E
        public static System.Int32 Unpack16Hi(System.UInt32 pack) { }
        // RVA: 0x04D875C0  token: 0x600089F
        public static System.Int32 Unpack16Low(System.UInt32 pack) { }
        // RVA: 0x04DA3760  token: 0x60008A0
        public static System.UInt32 Pack4_28(System.Int32 hi, System.Int32 low) { }
        // RVA: 0x04DA3780  token: 0x60008A1
        public static System.Int32 Unpack4_28Hi(System.UInt32 pack) { }
        // RVA: 0x04DA3790  token: 0x60008A2
        public static System.Int32 Unpack4_28Low(System.UInt32 pack) { }
        // RVA: 0x04DA3770  token: 0x60008A3
        public static System.UInt32 Pack8_24(System.Int32 hi, System.Int32 low) { }
        // RVA: 0x04DA37A0  token: 0x60008A4
        public static System.Int32 Unpack8_24Hi(System.UInt32 pack) { }
        // RVA: 0x04DA37B0  token: 0x60008A5
        public static System.Int32 Unpack8_24Low(System.UInt32 pack) { }
        // RVA: 0x04D91CF0  token: 0x60008A6
        public static System.UInt64 Pack32(System.Int32 hi, System.Int32 low) { }
        // RVA: 0x04D9D8A0  token: 0x60008A7
        public static System.Int32 Unpack32Hi(System.UInt64 pack) { }
        // RVA: 0x04D88BE0  token: 0x60008A8
        public static System.Int32 Unpack32Low(System.UInt64 pack) { }
        // RVA: 0x09F3813C  token: 0x60008A9
        public static System.UInt64 PackTriple(System.Int32 v0, System.Int32 v1, System.Int32 v2) { }
        // RVA: 0x04DA30C0  token: 0x60008AA
        public static System.Void UnpackTriple(System.UInt64 pack, System.Int32& v0, System.Int32& v1, System.Int32& v2) { }
        // RVA: 0x09F37FC4  token: 0x60008AB
        public static System.UInt64 PackQuater(System.Int32 v0, System.Int32 v1, System.Int32 v2, System.Int32 v3) { }
        // RVA: 0x04DA3090  token: 0x60008AC
        public static System.Void UnpackQuater(System.UInt64 pack, System.Int32& v0, System.Int32& v1, System.Int32& v2, System.Int32& v3) { }

    }

    // TypeToken: 0x2000124  // size: 0x10
    public static class Develop
    {
        // Methods
        // RVA: 0x09F38278  token: 0x60008AD
        public static System.Void Log(System.String str) { }

    }

    // TypeToken: 0x2000125  // size: 0x10
    public static class GizmoUtility
    {
        // Fields
        public static readonly UnityEngine.Color ColorDynamic;  // static @ 0x0
        public static readonly UnityEngine.Color ColorKinematic;  // static @ 0x10
        public static readonly UnityEngine.Color ColorInvalid;  // static @ 0x20
        public static readonly UnityEngine.Color ColorCollider;  // static @ 0x30
        public static readonly UnityEngine.Color ColorNonSelectedCollider;  // static @ 0x40
        public static readonly UnityEngine.Color ColorTriangle;  // static @ 0x50
        public static readonly UnityEngine.Color ColorStructLine;  // static @ 0x60
        public static readonly UnityEngine.Color ColorBendLine;  // static @ 0x70
        public static readonly UnityEngine.Color ColorNearLine;  // static @ 0x80
        public static readonly UnityEngine.Color ColorRotationLine;  // static @ 0x90
        public static readonly UnityEngine.Color ColorAdjustLine;  // static @ 0xa0
        public static readonly UnityEngine.Color ColorAirLine;  // static @ 0xb0
        public static readonly UnityEngine.Color ColorBasePosition;  // static @ 0xc0
        public static readonly UnityEngine.Color ColorDirectionMoveLimit;  // static @ 0xd0
        public static readonly UnityEngine.Color ColorPenetration;  // static @ 0xe0
        public static readonly UnityEngine.Color ColorCollisionNormal;  // static @ 0xf0
        public static readonly UnityEngine.Color ColorVelocity;  // static @ 0x100
        public static readonly UnityEngine.Color ColorSkinningBone;  // static @ 0x110
        public static readonly UnityEngine.Color ColorDeformerPoint;  // static @ 0x120
        public static readonly UnityEngine.Color ColorDeformerPointRange;  // static @ 0x130
        public static readonly UnityEngine.Color ColorWind;  // static @ 0x140

        // Methods
        // RVA: 0x09F399C4  token: 0x60008AE
        public static System.Void DrawWireCapsule(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, UnityEngine.Vector3 scl, UnityEngine.Vector3 ldir, UnityEngine.Vector3 lup, System.Single length, System.Single startRadius, System.Single endRadius, System.Boolean resetMatrix) { }
        // RVA: 0x09F3A36C  token: 0x60008AF
        public static System.Void DrawWireSphere(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, UnityEngine.Vector3 scl, System.Single radius, System.Boolean drawSphere, System.Boolean drawAxis, System.Boolean resetMatrix) { }
        // RVA: 0x09F3A24C  token: 0x60008B0
        public static System.Void DrawWireCube(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, UnityEngine.Vector3 size, System.Boolean resetMatrix) { }
        // RVA: 0x09F39E38  token: 0x60008B1
        public static System.Void DrawWireCone(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, System.Single length, System.Single radius, System.Int32 div) { }
        // RVA: 0x09F39640  token: 0x60008B2
        public static System.Void DrawWireArrow(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, UnityEngine.Vector3 size, System.Boolean cross) { }
        // RVA: 0x09F38F94  token: 0x60008B3
        public static System.Void DrawAxis(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, System.Single size, System.Boolean resetMatrix) { }
        // RVA: 0x09F391F0  token: 0x60008B4
        public static System.Void DrawBone(UnityEngine.Vector3 pos, UnityEngine.Vector3 tpos, System.Single size) { }
        // RVA: 0x09F3A608  token: 0x60008B5
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000126  // size: 0x10
    public static class MathUtility
    {
        // Methods
        // RVA: 0x066AD86C  token: 0x60008B6
        public static System.Single Clamp1(System.Single a) { }
        // RVA: 0x09F3AE20  token: 0x60008B7
        public static System.Single Clamp01(System.Single a) { }
        // RVA: 0x09EFEF5C  token: 0x60008B8
        public static Unity.Mathematics.float3 Project(Unity.Mathematics.float3 v, Unity.Mathematics.float3 n) { }
        // RVA: 0x066A85BC  token: 0x60008B9
        public static System.Single Angle(Unity.Mathematics.float3 v1, Unity.Mathematics.float3 v2) { }
        // RVA: 0x09EFEB30  token: 0x60008BA
        public static Unity.Mathematics.float3 ClampVector(Unity.Mathematics.float3 v, System.Single minlength, System.Single maxlength) { }
        // RVA: 0x09F3B148  token: 0x60008BB
        public static Unity.Mathematics.float3 ClampVector(Unity.Mathematics.float3 v, System.Single maxlength) { }
        // RVA: 0x09F1260C  token: 0x60008BC
        public static Unity.Mathematics.float3 ClampDistance(Unity.Mathematics.float3 from, Unity.Mathematics.float3 to, System.Single maxlength) { }
        // RVA: 0x09F3AE30  token: 0x60008BD
        public static System.Boolean ClampAngle(Unity.Mathematics.float3 dir, Unity.Mathematics.float3 basedir, System.Single maxAngle, Unity.Mathematics.float3& outdir) { }
        // RVA: 0x09F3BF44  token: 0x60008BE
        public static Unity.Mathematics.quaternion FromToRotation(Unity.Mathematics.float3 from, Unity.Mathematics.float3 to, System.Single t) { }
        // RVA: 0x066AEEC4  token: 0x60008BF
        public static Unity.Mathematics.quaternion FromToRotation(Unity.Mathematics.quaternion from, Unity.Mathematics.quaternion to) { }
        // RVA: 0x09F3ACF4  token: 0x60008C0
        public static System.Single Angle(Unity.Mathematics.quaternion a, Unity.Mathematics.quaternion b) { }
        // RVA: 0x09F3B0B0  token: 0x60008C1
        public static Unity.Mathematics.quaternion ClampAngle(Unity.Mathematics.quaternion from, Unity.Mathematics.quaternion to, System.Single maxAngle) { }
        // RVA: 0x09F3AD58  token: 0x60008C2
        public static Unity.Mathematics.float3 AxisToEuler(Unity.Mathematics.float3 axis) { }
        // RVA: 0x09F21260  token: 0x60008C3
        public static Unity.Mathematics.quaternion AxisQuaternion(Unity.Mathematics.float3 dir) { }
        // RVA: 0x09EFEC04  token: 0x60008C4
        public static System.Single ClosestPtPointSegmentRatio(Unity.Mathematics.float3 c, Unity.Mathematics.float3 a, Unity.Mathematics.float3 b) { }
        // RVA: 0x09F3B33C  token: 0x60008C5
        public static System.Single ClosestPtPointSegmentRatioNoClamp(Unity.Mathematics.float3 c, Unity.Mathematics.float3 a, Unity.Mathematics.float3 b) { }
        // RVA: 0x09ED7F24  token: 0x60008C6
        public static Unity.Mathematics.float3 ClosestPtPointSegment(Unity.Mathematics.float3 c, Unity.Mathematics.float3 a, Unity.Mathematics.float3 b) { }
        // RVA: 0x09F3B1F4  token: 0x60008C7
        public static Unity.Mathematics.float3 ClosestPtPointSegmentNoClamp(Unity.Mathematics.float3 c, Unity.Mathematics.float3 a, Unity.Mathematics.float3 b) { }
        // RVA: 0x09EFEE4C  token: 0x60008C8
        public static System.Boolean IntersectPointPlane(Unity.Mathematics.float3 planePos, Unity.Mathematics.float3 planeDir, Unity.Mathematics.float3 pos, Unity.Mathematics.float3& outpos) { }
        // RVA: 0x09EFECEC  token: 0x60008C9
        public static System.Single IntersectPointPlaneDist(Unity.Mathematics.float3 planePos, Unity.Mathematics.float3 planeDir, Unity.Mathematics.float3 pos, Unity.Mathematics.float3& outPos) { }
        // RVA: 0x09F3DE04  token: 0x60008CA
        public static System.Boolean IntersectSegmentPlane(Unity.Mathematics.float3 a, Unity.Mathematics.float3 b, Unity.Mathematics.float3 p, Unity.Mathematics.float3 pn, Unity.Mathematics.float3& opos) { }
        // RVA: 0x09F3D5A0  token: 0x60008CB
        public static System.Boolean IntersectPointSphere(Unity.Mathematics.float3 sc, System.Single sr, Unity.Mathematics.float3 pos, Unity.Mathematics.float3& outPos) { }
        // RVA: 0x09F354EC  token: 0x60008CC
        public static System.Boolean IntersectPointSphere(Unity.Mathematics.float3 p, Unity.Mathematics.float3 sc, System.Single sr) { }
        // RVA: 0x09F3DC68  token: 0x60008CD
        public static System.Boolean IntersectRaySphere(Unity.Mathematics.float3 p, Unity.Mathematics.float3 d, Unity.Mathematics.float3 sc, System.Single sr, Unity.Mathematics.float3& q, System.Single& t) { }
        // RVA: 0x09F3CFE0  token: 0x60008CE
        public static System.Boolean IntersectLineSphare(Unity.Mathematics.float3 a, Unity.Mathematics.float3 b, Unity.Mathematics.float3 sc, System.Single sr, Unity.Mathematics.float3& q) { }
        // RVA: 0x09F3D6F0  token: 0x60008CF
        public static System.Boolean IntersectRayCone(Unity.Mathematics.float3 o, Unity.Mathematics.float3 d, Unity.Mathematics.float3 c, Unity.Mathematics.float3 v, System.Single cost, System.Single& t, Unity.Mathematics.float3& p) { }
        // RVA: 0x09F3C53C  token: 0x60008D0
        public static System.Boolean IntersectLineConeSurface(Unity.Mathematics.float3 a, Unity.Mathematics.float3 b, Unity.Mathematics.float3 d, System.Single dlen, Unity.Mathematics.float3 c, Unity.Mathematics.float3 v, System.Single cost, Unity.Mathematics.float3 c1, Unity.Mathematics.float3 c2, Unity.Mathematics.float3& p) { }
        // RVA: 0x09F3CBC8  token: 0x60008D1
        public static System.Boolean IntersectLineCylinderSurface(Unity.Mathematics.float3 sa, Unity.Mathematics.float3 sb, Unity.Mathematics.float3 p, Unity.Mathematics.float3 q, System.Single r, System.Single& t) { }
        // RVA: 0x09F3C6F8  token: 0x60008D2
        public static System.Boolean IntersectLineCylinderSurface(Unity.Mathematics.float3 a, Unity.Mathematics.float3 b, Unity.Mathematics.float3 c1, Unity.Mathematics.float3 c2, System.Single r1, System.Single r2, Unity.Mathematics.float3& p) { }
        // RVA: 0x09F3C1DC  token: 0x60008D3
        public static System.Boolean IntersectLineCapsule(Unity.Mathematics.float3 a, Unity.Mathematics.float3 b, Unity.Mathematics.float3 c1, Unity.Mathematics.float3 c2, System.Single r1, System.Single r2, Unity.Mathematics.float3& p) { }
        // RVA: 0x09F3F3E8  token: 0x60008D4
        public static System.Boolean IntersectTrianglePointDistance(Unity.Mathematics.float3 p, Unity.Mathematics.float3 p0, Unity.Mathematics.float3 p1, Unity.Mathematics.float3 p2, System.Single restDist, System.Single compressionStiffness, System.Single stretchStiffness, Unity.Mathematics.float3& corr, Unity.Mathematics.float3& corr0, Unity.Mathematics.float3& corr1, Unity.Mathematics.float3& corr2) { }
        // RVA: 0x09F3E95C  token: 0x60008D5
        public static System.Boolean IntersectTrianglePointDistanceSide(Unity.Mathematics.float3 p, Unity.Mathematics.float3 p0, Unity.Mathematics.float3 p1, Unity.Mathematics.float3 p2, System.Single restDist, System.Single compressionStiffness, System.Single stretchStiffness, System.Single side, Unity.Mathematics.float3& corr, Unity.Mathematics.float3& corr0, Unity.Mathematics.float3& corr1, Unity.Mathematics.float3& corr2) { }
        // RVA: 0x09F3DF80  token: 0x60008D6
        public static System.Boolean IntersectTrianglePointDistanceSide2(Unity.Mathematics.float3 p, Unity.Mathematics.float3 p0, Unity.Mathematics.float3 p1, Unity.Mathematics.float3 p2, System.Single radius, System.Single restDist, System.Single compressionStiffness, System.Single stretchStiffness, System.Single side, Unity.Mathematics.float3& corr, Unity.Mathematics.float3& corr0, Unity.Mathematics.float3& corr1, Unity.Mathematics.float3& corr2) { }
        // RVA: 0x09F3B904  token: 0x60008D7
        public static System.Single DistanceTrianglePoint(Unity.Mathematics.float3 p, Unity.Mathematics.float3 p0, Unity.Mathematics.float3 p1, Unity.Mathematics.float3 p2) { }
        // RVA: 0x066B2454  token: 0x60008D8
        public static Unity.Mathematics.float3 TriangleCenter(Unity.Mathematics.float3 p0, Unity.Mathematics.float3 p1, Unity.Mathematics.float3 p2) { }
        // RVA: 0x09ED8080  token: 0x60008D9
        public static Unity.Mathematics.float3 TriangleNormal(Unity.Mathematics.float3 p0, Unity.Mathematics.float3 p1, Unity.Mathematics.float3 p2) { }
        // RVA: 0x09F3FF60  token: 0x60008DA
        public static Unity.Mathematics.quaternion TriangleRotation(Unity.Mathematics.float3 p0, Unity.Mathematics.float3 p1, Unity.Mathematics.float3 p2) { }
        // RVA: 0x09F3FDBC  token: 0x60008DB
        public static Unity.Mathematics.quaternion TriangleCenterRotation(Unity.Mathematics.float3 p0, Unity.Mathematics.float3 p1, Unity.Mathematics.float3 p2, Unity.Mathematics.float3 p3) { }
        // RVA: 0x066AEDF8  token: 0x60008DC
        public static System.Single DistanceTriangleCenter(Unity.Mathematics.float3 p, Unity.Mathematics.float3 p0, Unity.Mathematics.float3 p1, Unity.Mathematics.float3 p2) { }
        // RVA: 0x066AECDC  token: 0x60008DD
        public static System.Single DirectionPointTriangle(Unity.Mathematics.float3 p, Unity.Mathematics.float3 a, Unity.Mathematics.float3 b, Unity.Mathematics.float3 c) { }
        // RVA: 0x09F3D190  token: 0x60008DE
        public static System.Boolean IntersectLineTriangle(Unity.Mathematics.float3 p, Unity.Mathematics.float3 q, Unity.Mathematics.float3 a, Unity.Mathematics.float3 b, Unity.Mathematics.float3 c, Unity.Mathematics.float3& hitpos, System.Single& t, Unity.Mathematics.float3& n) { }
        // RVA: 0x09F3B418  token: 0x60008DF
        public static System.Single ClosestPtSegmentSegment(Unity.Mathematics.float3 p1, Unity.Mathematics.float3 q1, Unity.Mathematics.float3 p2, Unity.Mathematics.float3 q2, System.Single& s, System.Single& t, Unity.Mathematics.float3& c1, Unity.Mathematics.float3& c2) { }
        // RVA: 0x09F3C184  token: 0x60008E0
        public static System.Single GetBezierValue(MagicaCloth.BezierParam bparam, System.Single t) { }
        // RVA: 0x09F35460  token: 0x60008E1
        public static System.Single GetBezierValue(System.Single sval, System.Single eval, System.Single curve, System.Single t) { }

    }

    // TypeToken: 0x2000127  // size: 0x10
    public static class MeshUtility
    {
        // Methods
        // RVA: 0x09F45224  token: 0x60008E2
        public static UnityEngine.GameObject ReplaceSkinnedMeshRendererToMeshRenderer(UnityEngine.SkinnedMeshRenderer sren, System.Boolean replaceSkinnedMeshRenderer) { }
        // RVA: 0x09F40E84  token: 0x60008E3
        public static System.Boolean CalcMeshWorldPositionNormalTangent(MagicaCloth.MeshData meshData, System.Collections.Generic.List<UnityEngine.Transform> boneList, System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList) { }
        // RVA: 0x09F414C4  token: 0x60008E4
        public static System.Boolean CalcMeshWorldPositionNormalTangent(UnityEngine.Renderer ren, UnityEngine.Mesh mesh, System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector3>& wtanList) { }
        // RVA: 0x09F401DC  token: 0x60008E5
        public static System.Boolean CalcMeshLocalNormalTangent(System.Collections.Generic.List<System.Int32> selectList, UnityEngine.Vector3[] vlist, UnityEngine.Vector2[] uvlist, System.Int32[] triangles, System.Collections.Generic.List<UnityEngine.Vector3>& lnorList, System.Collections.Generic.List<UnityEngine.Vector3>& ltanList) { }
        // RVA: 0x09F446EC  token: 0x60008E6
        public static System.Collections.Generic.List<System.Collections.Generic.HashSet<System.Int32>> GetTriangleToVertexLinkList(System.Int32 vcnt, System.Collections.Generic.List<System.Int32> lineList, System.Collections.Generic.List<System.Int32> triangleList) { }
        // RVA: 0x09F44E00  token: 0x60008E7
        public static System.Collections.Generic.List<System.Collections.Generic.HashSet<System.Int32>> GetVertexLinkList(System.Int32 vcnt, System.Collections.Generic.HashSet<System.UInt32> lineSet) { }
        // RVA: 0x09F45080  token: 0x60008E8
        public static System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.HashSet<System.Int32>> GetVertexToTriangles(System.Collections.Generic.List<System.Int32> triangleList) { }
        // RVA: 0x09F4560C  token: 0x60008E9
        public static System.Int32 RestTriangleVertex(System.Int32 tindex, System.Int32 v0, System.Int32 v1, System.Collections.Generic.List<System.Int32> triangleList) { }
        // RVA: 0x09F45508  token: 0x60008EA
        public static System.Void RestTriangleVertex(System.Int32 tindex, System.Int32 v0, System.Collections.Generic.List<System.Int32> triangleList, System.Int32& v1, System.Int32& v2) { }
        // RVA: 0x09F440DC  token: 0x60008EB
        public static System.Boolean CheckAdjacentTriangle(System.Int32 tindex0, System.Int32 tindex1, System.Collections.Generic.List<System.Int32> triangleList) { }
        // RVA: 0x09F44444  token: 0x60008EC
        public static System.Collections.Generic.Dictionary<System.UInt32,System.Collections.Generic.List<System.Int32>> GetTriangleEdgePair(System.Collections.Generic.List<System.Int32> triangleList) { }
        // RVA: 0x09F400BC  token: 0x60008ED
        private static System.Void AddTriangleEdge(System.Int32 v0, System.Int32 v1, System.Int32 tindex, System.Collections.Generic.Dictionary<System.UInt32,System.Collections.Generic.List<System.Int32>> triangleEdgeDict) { }
        // RVA: 0x09F445B0  token: 0x60008EE
        public static System.Collections.Generic.List<System.UInt64> GetTrianglePackList(System.Collections.Generic.List<System.Int32> triangleList) { }
        // RVA: 0x09F441A0  token: 0x60008EF
        public static System.Single ClosestPtBoneLine(UnityEngine.Vector3 pos, UnityEngine.Transform bone, System.Single lineWidth, UnityEngine.Vector3& d) { }
        // RVA: 0x09F44A60  token: 0x60008F0
        public static System.Collections.Generic.List<UnityEngine.Transform> GetUseBoneTransformList(UnityEngine.Transform[] bones, UnityEngine.Mesh mesh) { }
        // RVA: 0x09F41F40  token: 0x60008F1
        public static System.Void CalcTetraMesh(System.Collections.Generic.List<UnityEngine.Vector3> posList, System.Int32& tetraCount, System.Collections.Generic.List<System.Int32>& tetraIndexList, System.Collections.Generic.List<System.Single>& tetraSizeList) { }
        // RVA: -1  // generic def  token: 0x60008F2
        public static UnityEngine.Transform GetReplaceBone(UnityEngine.Transform now, System.Collections.Generic.Dictionary<T,UnityEngine.Transform> boneReplaceDict) { }

    }

    // TypeToken: 0x200012A  // size: 0x10
    public class StaticStringBuilder
    {
        // Fields
        private static System.Text.StringBuilder stringBuilder;  // static @ 0x0

        // Properties
        System.Text.StringBuilder Instance { get; /* RVA: 0x09F4BA0C */ }

        // Methods
        // RVA: 0x09F4B8D8  token: 0x6000900
        public static System.Void Clear() { }
        // RVA: 0x09F4B838  token: 0x6000901
        public static System.Text.StringBuilder Append(System.Object[] args) { }
        // RVA: 0x09F4B60C  token: 0x6000902
        public static System.Text.StringBuilder AppendLine(System.Object[] args) { }
        // RVA: 0x09F4B6E0  token: 0x6000903
        public static System.Text.StringBuilder AppendLine() { }
        // RVA: 0x09F4B75C  token: 0x6000904
        public static System.String AppendToString(System.Object[] args) { }
        // RVA: 0x09F4B930  token: 0x6000905
        public static System.String ToString() { }
        // RVA: 0x041E1670  token: 0x6000906
        public System.Void .ctor() { }
        // RVA: 0x09F4B988  token: 0x6000907
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200012B  // size: 0x30
    public class UpdateTimeManager
    {
        // Fields
        private MagicaCloth.UpdateTimeManager.UpdateCount updatePerSeccond;  // 0x10
        private MagicaCloth.UpdateTimeManager.UpdateMode updateMode;  // 0x14
        private MagicaCloth.UpdateTimeManager.UpdateLocation updateLocation;  // 0x18
        private System.Single timeScale;  // 0x1c
        private System.Single futurePredictionRate;  // 0x20
        private System.Boolean updateBoneScale;  // 0x24
        private System.Int32 fixedUpdateCount;  // 0x28

        // Properties
        System.Int32 FixedUpdateCount { get; /* RVA: 0x011F2A90 */ }
        System.Int32 UpdatePerSecond { get; /* RVA: 0x02B2E2D0 */ }
        System.Single UpdateIntervalTime { get; /* RVA: 0x04DA3880 */ }
        System.Single UpdatePower { get; /* RVA: 0x04DA38A0 */ }
        System.Single TimeScale { get; /* RVA: 0x04D868F0 */ set; /* RVA: 0x09F4C9E8 */ }
        System.Single DeltaTime { get; /* RVA: 0x05391A8C */ }
        System.Single PhysicsDeltaTime { get; /* RVA: 0x03233AF0 */ }
        System.Single AverageDeltaTime { get; /* RVA: 0x09F4C9AC */ }
        System.Boolean IsUnscaledUpdate { get; /* RVA: 0x09F4C9B4 */ }
        System.Boolean IsDelay { get; /* RVA: 0x03233A70 */ }
        System.Single FuturePredictionRate { get; /* RVA: 0x011EC8A0 */ set; /* RVA: 0x09F4C9D0 */ }
        System.Boolean UpdateBoneScale { get; /* RVA: 0x04D86870 */ set; /* RVA: 0x04D86880 */ }
        System.Int32 WorkerMaximumCount { get; /* RVA: 0x09F4C9C8 */ }

        // Methods
        // RVA: 0x04DA3850  token: 0x6000908
        public System.Void ResetFixedUpdateCount() { }
        // RVA: 0x04D9EBD0  token: 0x6000909
        public System.Void AddFixedUpdateCount() { }
        // RVA: 0x04D882A0  token: 0x600090B
        public MagicaCloth.UpdateTimeManager.UpdateMode GetUpdateMode() { }
        // RVA: 0x04D8F540  token: 0x600090C
        public System.Void SetUpdateMode(MagicaCloth.UpdateTimeManager.UpdateMode mode) { }
        // RVA: 0x011EF5B0  token: 0x600090D
        public MagicaCloth.UpdateTimeManager.UpdateLocation GetUpdateLocation() { }
        // RVA: 0x011EF9B0  token: 0x600090E
        public System.Void SetUpdateLocation(MagicaCloth.UpdateTimeManager.UpdateLocation location) { }
        // RVA: 0x014F51F0  token: 0x6000910
        public System.Void SetUpdatePerSecond(MagicaCloth.UpdateTimeManager.UpdateCount ucount) { }
        // RVA: 0x04DA3860  token: 0x600091F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200012F
    public class ReferenceDataBuilder`1
    {
        // Fields
        private System.Int32 indexCount;  // 0x0
        private System.Collections.Generic.List<T> dataList;  // 0x0
        private System.Collections.Generic.List<System.Collections.Generic.List<System.Int32>> indexToDataIndexList;  // 0x0
        private System.Collections.Generic.List<System.Collections.Generic.List<System.Int32>> dataIndexToIndexList;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000920
        public System.Void Init(System.Int32 indexCount) { }
        // RVA: -1  // not resolved  token: 0x6000921
        public System.Void AddData(T data, System.Int32[] indexes) { }
        // RVA: -1  // not resolved  token: 0x6000922
        public System.ValueTuple<System.Collections.Generic.List<MagicaCloth.ReferenceDataIndex>,System.Collections.Generic.List<T>> GetDirectReferenceData() { }
        // RVA: -1  // not resolved  token: 0x6000923
        public System.ValueTuple<System.Collections.Generic.List<MagicaCloth.ReferenceDataIndex>,System.Collections.Generic.List<System.Int32>,System.Collections.Generic.List<System.Collections.Generic.List<System.Int32>>> GetIndirectReferenceData() { }
        // RVA: -1  // not resolved  token: 0x6000924
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000130  // size: 0x18
    public sealed struct ReferenceDataIndex
    {
        // Fields
        public System.Int32 startIndex;  // 0x10
        public System.Int32 count;  // 0x14

    }

    // TypeToken: 0x2000131
    public interface IDataHash
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000925
        public virtual System.Int32 GetDataHash() { }

    }

    // TypeToken: 0x2000132
    public interface IShareDataObject
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000926
        public virtual System.Collections.Generic.List<MagicaCloth.ShareDataObject> GetAllShareDataObject() { }
        // RVA: -1  // abstract  token: 0x6000927
        public virtual MagicaCloth.ShareDataObject DuplicateShareDataObject(MagicaCloth.ShareDataObject source) { }

    }

    // TypeToken: 0x2000133  // size: 0x20
    public abstract class ShareDataObject : UnityEngine.ScriptableObject, MagicaCloth.IDataVerify, MagicaCloth.IDataHash
    {
        // Fields
        protected System.Int32 dataHash;  // 0x18
        protected System.Int32 dataVersion;  // 0x1c

        // Properties
        System.Int32 SaveDataHash { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        System.Int32 SaveDataVersion { get; /* RVA: 0x04D86310 */ set; /* RVA: 0x04D86320 */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000928
        public virtual System.Int32 GetDataHash() { }
        // RVA: -1  // abstract  token: 0x600092D
        public virtual System.Int32 GetVersion() { }
        // RVA: -1  // abstract  token: 0x600092E
        public virtual MagicaCloth.Define.Error VerifyData() { }
        // RVA: 0x09F4B5B0  token: 0x600092F
        public virtual System.Void CreateVerifyData() { }
        // RVA: 0x09F4B5E0  token: 0x6000930
        public virtual System.String GetInformation() { }
        // RVA: -1  // generic def  token: 0x6000931
        public static T CreateShareData(System.String dataName) { }
        // RVA: -1  // generic def  token: 0x6000932
        public static System.Boolean RemoveNullAndDuplication(System.Collections.Generic.List<T> data) { }
        // RVA: -1  // generic def  token: 0x6000933
        public static T Clone(T source) { }
        // RVA: 0x03573370  token: 0x6000934
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000134  // size: 0x18
    public class StopWatch
    {
        // Fields
        private System.Single startTime;  // 0x10
        private System.Single endTime;  // 0x14

        // Properties
        System.Single ElapsedSeconds { get; /* RVA: 0x04DA3820 */ }
        System.Single ElapsedMilliseconds { get; /* RVA: 0x04DA3800 */ }

        // Methods
        // RVA: 0x09F4BA5C  token: 0x6000935
        public MagicaCloth.StopWatch Start() { }
        // RVA: 0x09F4BA7C  token: 0x6000936
        public MagicaCloth.StopWatch Stop() { }
        // RVA: 0x041E1670  token: 0x6000939
        public System.Void .ctor() { }

    }

}

namespace MagicaReductionMesh
{

    // TypeToken: 0x2000004  // size: 0x30
    public class ReductionMesh
    {
        // Fields
        private MagicaReductionMesh.ReductionMesh.ReductionWeightMode <WeightMode>k__BackingField;  // 0x10
        private MagicaReductionMesh.MeshData meshData;  // 0x18
        private MagicaReductionMesh.ReductionData reductionData;  // 0x20
        private MagicaReductionMesh.DebugData debugData;  // 0x28

        // Properties
        MagicaReductionMesh.ReductionMesh.ReductionWeightMode WeightMode { get; /* RVA: 0x02B2E2D0 */ set; /* RVA: 0x014F51F0 */ }
        MagicaReductionMesh.MeshData MeshData { get; /* RVA: 0x09ED1A44 */ }
        MagicaReductionMesh.ReductionData ReductionData { get; /* RVA: 0x04A379B0 */ }
        MagicaReductionMesh.DebugData DebugData { get; /* RVA: 0x09ED1A18 */ }

        // Methods
        // RVA: 0x09ED17D0  token: 0x6000008
        public System.Int32 AddMesh(System.Boolean isSkinning, UnityEngine.Mesh mesh, System.Collections.Generic.List<UnityEngine.Transform> bones, UnityEngine.Matrix4x4[] bindPoseList, UnityEngine.BoneWeight[] boneWeightList) { }
        // RVA: 0x09ED1554  token: 0x6000009
        public System.Int32 AddMesh(UnityEngine.Renderer ren) { }
        // RVA: 0x09ED14E0  token: 0x600000A
        public System.Int32 AddMesh(UnityEngine.Transform root, System.Collections.Generic.List<UnityEngine.Vector3> posList, System.Collections.Generic.List<UnityEngine.Vector3> norList, System.Collections.Generic.List<UnityEngine.Vector4> tanList, System.Collections.Generic.List<UnityEngine.Vector2> uvList, System.Collections.Generic.List<System.Int32> triangleList) { }
        // RVA: 0x09ED1868  token: 0x600000B
        public System.Void Reduction(System.Single zeroRadius, System.Single radius, System.Single polygonLength, System.Boolean createTetra) { }
        // RVA: 0x09ED1838  token: 0x600000C
        public MagicaReductionMesh.FinalData GetFinalData(UnityEngine.Transform root) { }
        // RVA: 0x09ED1960  token: 0x600000D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x18
    public abstract class ReductionMeshAccess
    {
        // Fields
        protected MagicaReductionMesh.ReductionMesh parent;  // 0x10

        // Properties
        MagicaReductionMesh.MeshData MeshData { get; /* RVA: 0x09ED14A0 */ }
        MagicaReductionMesh.ReductionData ReductionData { get; /* RVA: 0x09ED14C0 */ }
        MagicaReductionMesh.DebugData DebugData { get; /* RVA: 0x09ED1480 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000011
        public virtual System.Void SetParent(MagicaReductionMesh.ReductionMesh parent) { }
        // RVA: 0x041E1670  token: 0x6000012
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x18
    public class DebugData : MagicaReductionMesh.ReductionMeshAccess
    {
        // Methods
        // RVA: 0x09EC2B90  token: 0x6000013
        public System.Void DispMeshInfo(System.String header) { }
        // RVA: 0x09EC1FCC  token: 0x6000014
        public static System.Void DebugDrawShared(MagicaReductionMesh.FinalData final, System.Boolean drawTriangle, System.Boolean drawLine, System.Boolean drawTetra, System.Boolean drawVertexNormal, System.Boolean drawVertexTangent, System.Boolean drawNumber, System.Int32 maxPolygonCount, System.Int32 layer, System.Int32 tetraIndex, System.Single tetraSize, System.Collections.Generic.List<System.Int32> drawNumberList, System.Single axisSize) { }
        // RVA: 0x09EC2E3C  token: 0x6000015
        private static System.Void DrawTetra(MagicaReductionMesh.FinalData final, System.Int32 tetraIndex, System.Collections.Generic.List<UnityEngine.Vector3> wposList, System.Single tetraSize) { }
        // RVA: 0x09EC1B24  token: 0x6000016
        public static System.Void DebugDrawChild(MagicaReductionMesh.FinalData final, System.Boolean drawPosition, System.Boolean drawNormal, System.Boolean drawTriangle, System.Boolean drawNumber, System.Int32 maxVertexCount, System.Single positionSize, System.Single axisSize) { }
        // RVA: 0x041E1670  token: 0x6000017
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000008  // size: 0x98
    public class FinalData
    {
        // Fields
        public System.Collections.Generic.List<UnityEngine.Vector3> vertices;  // 0x10
        public System.Collections.Generic.List<UnityEngine.Vector3> normals;  // 0x18
        public System.Collections.Generic.List<UnityEngine.Vector4> tangents;  // 0x20
        public System.Collections.Generic.List<UnityEngine.Vector2> uvs;  // 0x28
        public System.Collections.Generic.List<UnityEngine.BoneWeight> boneWeights;  // 0x30
        public System.Collections.Generic.List<UnityEngine.Matrix4x4> bindPoses;  // 0x38
        public System.Collections.Generic.List<UnityEngine.Transform> bones;  // 0x40
        public System.Collections.Generic.List<System.Int32> lines;  // 0x48
        public System.Collections.Generic.List<System.Int32> triangles;  // 0x50
        public System.Collections.Generic.List<System.Int32> tetras;  // 0x58
        public System.Collections.Generic.List<System.Single> tetraSizes;  // 0x60
        public System.Collections.Generic.List<UnityEngine.Matrix4x4> vertexBindPoses;  // 0x68
        public System.Collections.Generic.List<MagicaReductionMesh.FinalData.MeshIndexData> vertexToMeshIndexList;  // 0x70
        public System.Collections.Generic.List<System.Int32> vertexToTriangleCountList;  // 0x78
        public System.Collections.Generic.List<System.Int32> vertexToTriangleStartList;  // 0x80
        public System.Collections.Generic.List<System.Int32> vertexToTriangleIndexList;  // 0x88
        public System.Collections.Generic.List<MagicaReductionMesh.FinalData.MeshInfo> meshList;  // 0x90

        // Properties
        System.Boolean IsValid { get; /* RVA: 0x09EC35C4 */ }
        System.Int32 VertexCount { get; /* RVA: 0x09EC3720 */ }
        System.Int32 LineCount { get; /* RVA: 0x09EC3608 */ }
        System.Int32 TriangleCount { get; /* RVA: 0x09EC36D4 */ }
        System.Int32 TetraCount { get; /* RVA: 0x09EC368C */ }
        System.Int32 BoneCount { get; /* RVA: 0x09EC3584 */ }
        System.Boolean IsSkinning { get; /* RVA: 0x02FFF600 */ }
        System.Int32 MeshCount { get; /* RVA: 0x09EC364C */ }

        // Methods
        // RVA: 0x09EC30E4  token: 0x6000020
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000B  // size: 0x68
    public class MeshData : MagicaReductionMesh.ReductionMeshAccess
    {
        // Fields
        public System.Collections.Generic.List<MagicaReductionMesh.MeshData.Vertex> originalVertexList;  // 0x18
        public System.Collections.Generic.List<MagicaReductionMesh.MeshData.MeshInfo> meshInfoList;  // 0x20
        public System.Collections.Generic.List<MagicaReductionMesh.MeshData.ShareVertex> shareVertexList;  // 0x28
        private System.Collections.Generic.Dictionary<System.UInt64,MagicaReductionMesh.MeshData.Triangle> triangleDict;  // 0x30
        private System.Collections.Generic.Dictionary<System.UInt32,MagicaReductionMesh.MeshData.Line> lineDict;  // 0x38
        public System.Collections.Generic.List<UnityEngine.Transform> boneList;  // 0x40
        private System.Collections.Generic.List<MagicaReductionMesh.MeshData.Tetra> tetraList;  // 0x48
        private System.Single weightPow;  // 0x50
        private System.Int32 maxWeightCount;  // 0x54
        private System.Single sameSurfaceAngle;  // 0x58
        private System.Boolean removeSameTrianglePair;  // 0x5c
        private System.Single removeSameTrianglePairAngle;  // 0x60

        // Properties
        System.Int32 VertexCount { get; /* RVA: 0x09ECE42C */ }
        System.Int32 LineCount { get; /* RVA: 0x09ECE32C */ }
        System.Int32 TriangleCount { get; /* RVA: 0x09ECE3EC */ }
        System.Int32 TetraCount { get; /* RVA: 0x09ECE3AC */ }
        System.Int32 MeshCount { get; /* RVA: 0x09ECE36C */ }
        System.Single WeightPow { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }
        System.Int32 MaxWeightCount { get; /* RVA: 0x04D86540 */ set; /* RVA: 0x04D86590 */ }
        System.Single SameSurfaceAngle { get; /* RVA: 0x04D88400 */ set; /* RVA: 0x04D88430 */ }
        System.Boolean RemoveSameTrianglePair { get; /* RVA: 0x04D86D90 */ set; /* RVA: 0x04D86DB0 */ }
        System.Single RemoveSameTrianglePairAngle { get; /* RVA: 0x04D88480 */ set; /* RVA: 0x04D884A0 */ }

        // Methods
        // RVA: 0x09EC3F38  token: 0x6000033
        public System.Int32 AddMesh(System.Boolean isSkinning, UnityEngine.Mesh mesh, System.Collections.Generic.List<UnityEngine.Transform> bones, UnityEngine.Matrix4x4[] bindPoseList, UnityEngine.BoneWeight[] boneWeightList) { }
        // RVA: 0x09EC37D8  token: 0x6000034
        public System.Int32 AddMesh(UnityEngine.Transform root, System.Collections.Generic.List<UnityEngine.Vector3> posList, System.Collections.Generic.List<UnityEngine.Vector3> norList, System.Collections.Generic.List<UnityEngine.Vector4> tanList, System.Collections.Generic.List<UnityEngine.Vector2> uvList, System.Collections.Generic.List<System.Int32> triangleList) { }
        // RVA: 0x09EC6F7C  token: 0x6000035
        public System.Void CombineVertex(MagicaReductionMesh.MeshData.ShareVertex sv0, MagicaReductionMesh.MeshData.ShareVertex sv1) { }
        // RVA: 0x09ECE064  token: 0x6000036
        public System.Void UpdateMeshData(System.Boolean createTetra) { }
        // RVA: 0x09EC6B7C  token: 0x6000037
        private System.Void CalcVertexIndex() { }
        // RVA: 0x09EC67A4  token: 0x6000038
        private System.Void CalcUV(MagicaReductionMesh.MeshData.UvWrapMode wrapMode) { }
        // RVA: 0x09EC9C54  token: 0x6000039
        private System.Void CreateTriangleAndLine() { }
        // RVA: 0x09EC63C0  token: 0x600003A
        private System.Void CalcShareVertexWeight() { }
        // RVA: 0x09EC4C7C  token: 0x600003B
        private System.Void AdjustTriangleNormal() { }
        // RVA: 0x09EC6E38  token: 0x600003C
        private System.Boolean CheckTwoTriangleOpen(MagicaReductionMesh.MeshData.Triangle tri1, MagicaReductionMesh.MeshData.Triangle tri2, System.UInt32 edge) { }
        // RVA: 0x09EC64F8  token: 0x600003D
        private System.Single CalcTwoTriangleAngle(MagicaReductionMesh.MeshData.Triangle tri1, MagicaReductionMesh.MeshData.Triangle tri2, System.UInt32 edge) { }
        // RVA: 0x09ECB04C  token: 0x600003E
        private System.Collections.Generic.Dictionary<System.UInt32,System.Collections.Generic.List<System.UInt64>> GetEdgeToTriangleDict() { }
        // RVA: 0x09EC6D24  token: 0x600003F
        private System.Void CalcVertexNormalFromTriangle() { }
        // RVA: 0x09ECD6D4  token: 0x6000040
        private System.Void RemoveTriangle(System.UInt64 thash) { }
        // RVA: 0x09ECCD54  token: 0x6000041
        private System.Collections.Generic.Dictionary<System.UInt64,System.Collections.Generic.List<MagicaReductionMesh.MeshData.Square>> GetSquareDict() { }
        // RVA: 0x09ECD5C4  token: 0x6000042
        private System.Void RemoveOverlappingSquareTriangles() { }
        // RVA: 0x09EC83A8  token: 0x6000043
        private System.Void CreateTetraMesh() { }
        // RVA: 0x09EC5728  token: 0x6000044
        private UnityEngine.Bounds CalcBounding() { }
        // RVA: 0x09ECB574  token: 0x6000045
        public MagicaReductionMesh.FinalData GetFinalData(UnityEngine.Transform root) { }
        // RVA: 0x09EC72AC  token: 0x6000046
        private System.Void CreateOriginalMeshInfo(MagicaReductionMesh.FinalData final, UnityEngine.Transform root, System.Single weightLength) { }
        // RVA: 0x09EC4BE4  token: 0x6000047
        private System.Void AddVertexToMeshIndexData(MagicaReductionMesh.FinalData final, System.Int32 sindex, System.Int32 meshIndex, System.Int32 meshVertexIndex) { }
        // RVA: 0x09ECD8EC  token: 0x6000048
        private System.Collections.Generic.List<MagicaReductionMesh.MeshData.ShareVertex> SearchNearPointList(UnityEngine.Vector3 basePos, MagicaReductionMesh.MeshData.ShareVertex sv, System.Single weightLength, System.Int32 maxCount) { }
        // RVA: 0x09EC593C  token: 0x6000049
        public System.Void CalcMeshWorldPositionNormalTangent(System.Boolean isSkinning, UnityEngine.Mesh mesh, System.Collections.Generic.List<UnityEngine.Transform> bones, UnityEngine.Matrix4x4[] bindPoseList, UnityEngine.BoneWeight[] boneWeightList, System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector4>& wtanList) { }
        // RVA: 0x09ECE0CC  token: 0x600004A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001F  // size: 0x18
    public class ReductionData : MagicaReductionMesh.ReductionMeshAccess
    {
        // Methods
        // RVA: 0x09ED13F4  token: 0x6000087
        public System.Void ReductionZeroDistance(System.Single radius) { }
        // RVA: 0x09ED1368  token: 0x6000088
        public System.Void ReductionRadius(System.Single radius) { }
        // RVA: 0x09ED12DC  token: 0x6000089
        public System.Void ReductionPolygonLink(System.Single length) { }
        // RVA: 0x09ED0E10  token: 0x600008A
        public System.Void ReductionBone() { }
        // RVA: 0x041E1670  token: 0x600008B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000020  // size: 0x38
    public class NearPointReduction
    {
        // Fields
        protected MagicaReductionMesh.MeshData meshData;  // 0x10
        private System.Collections.Generic.List<MagicaReductionMesh.NearPointReduction.Point> pointList;  // 0x18
        protected System.Collections.Generic.Dictionary<UnityEngine.Vector3Int,System.Collections.Generic.List<MagicaReductionMesh.NearPointReduction.Point>> gridMap;  // 0x20
        protected System.Single gridSize;  // 0x28
        private System.Single searchRadius;  // 0x2c
        private System.Collections.Generic.Dictionary<MagicaReductionMesh.NearPointReduction.Point,System.Collections.Generic.List<MagicaReductionMesh.NearPointReduction.Point>> nearPointDict;  // 0x30

        // Properties
        System.Int32 PointCount { get; /* RVA: 0x09ECFEA8 */ }

        // Methods
        // RVA: 0x09ECFD7C  token: 0x600008C
        public System.Void .ctor(System.Single radius) { }
        // RVA: 0x09ECE974  token: 0x600008E
        public System.Void Create(MagicaReductionMesh.MeshData meshData) { }
        // RVA: 0x09ECED18  token: 0x600008F
        public System.Void Reduction() { }
        // RVA: 0x09ECE8B8  token: 0x6000090
        private MagicaReductionMesh.NearPointReduction.Point AddPoint(MagicaReductionMesh.MeshData.ShareVertex sv, UnityEngine.Vector3 pos) { }
        // RVA: 0x09ECE728  token: 0x6000091
        private System.Void AddGrid(MagicaReductionMesh.NearPointReduction.Point p) { }
        // RVA: 0x09ECF25C  token: 0x6000092
        private System.Void RemoveGrid(MagicaReductionMesh.NearPointReduction.Point p) { }
        // RVA: 0x09ECECC0  token: 0x6000093
        private System.Void Move(MagicaReductionMesh.NearPointReduction.Point p, UnityEngine.Vector3 newpos) { }
        // RVA: 0x09ECF3DC  token: 0x6000094
        private System.Void Remove(MagicaReductionMesh.NearPointReduction.Point p) { }
        // RVA: 0x09ECEAE8  token: 0x6000095
        protected UnityEngine.Vector3Int GetGridPos(UnityEngine.Vector3 pos) { }
        // RVA: 0x09ECF440  token: 0x6000096
        private System.Void SearchNearPointAll() { }
        // RVA: 0x09ECF6D4  token: 0x6000097
        private System.Void SearchNearPoint(MagicaReductionMesh.NearPointReduction.Point p, System.Single radius, MagicaReductionMesh.NearPointReduction.Point ignorePoint) { }
        // RVA: 0x09ECEB60  token: 0x6000098
        private MagicaReductionMesh.NearPointReduction.Point GetNearPointPair() { }

    }

    // TypeToken: 0x2000022  // size: 0x30
    public class PolygonLinkReduction
    {
        // Fields
        protected MagicaReductionMesh.MeshData meshData;  // 0x10
        private System.Single reductionLength;  // 0x18
        private System.Collections.Generic.List<MagicaReductionMesh.PolygonLinkReduction.Point> pointList;  // 0x20
        private System.Collections.Generic.Dictionary<MagicaReductionMesh.MeshData.ShareVertex,MagicaReductionMesh.PolygonLinkReduction.Point> pointDict;  // 0x28

        // Properties
        System.Int32 PointCount { get; /* RVA: 0x09ED0DD0 */ }

        // Methods
        // RVA: 0x09ED0D00  token: 0x600009A
        public System.Void .ctor(System.Single length) { }
        // RVA: 0x09ECFF9C  token: 0x600009C
        public System.Void Create(MagicaReductionMesh.MeshData meshData) { }
        // RVA: 0x09ED02CC  token: 0x600009D
        public System.Void Reduction() { }
        // RVA: 0x09ECFEE8  token: 0x600009E
        private System.Void AddPoint(MagicaReductionMesh.MeshData.ShareVertex sv) { }
        // RVA: 0x09ED0248  token: 0x600009F
        private MagicaReductionMesh.PolygonLinkReduction.Point GetPoint(MagicaReductionMesh.MeshData.ShareVertex sv) { }
        // RVA: 0x09ED0904  token: 0x60000A0
        private System.Void Remove(MagicaReductionMesh.PolygonLinkReduction.Point p) { }
        // RVA: 0x09ED0984  token: 0x60000A1
        private System.Void SearchNearPointAll() { }
        // RVA: 0x09ED0A98  token: 0x60000A2
        private System.Void SearchNearPoint(MagicaReductionMesh.PolygonLinkReduction.Point p) { }
        // RVA: 0x09ED00E8  token: 0x60000A3
        private MagicaReductionMesh.PolygonLinkReduction.Point GetNearPointPair() { }

    }

    // TypeToken: 0x2000024  // size: 0x10
    public class Utility
    {
        // Methods
        // RVA: 0x0677F488  token: 0x60000A5
        public static System.UInt32 PackPair(System.Int32 v0, System.Int32 v1) { }
        // RVA: 0x04DA3080  token: 0x60000A6
        public static System.Void UnpackPair(System.UInt32 pack, System.Int32& v0, System.Int32& v1) { }
        // RVA: 0x09ED7AFC  token: 0x60000A7
        public static System.UInt64 PackTriple(System.Int32 v0, System.Int32 v1, System.Int32 v2) { }
        // RVA: 0x04DA30C0  token: 0x60000A8
        public static System.Void UnpackTriple(System.UInt64 pack, System.Int32& v0, System.Int32& v1, System.Int32& v2) { }
        // RVA: 0x09ED7984  token: 0x60000A9
        public static System.UInt64 PackQuater(System.Int32 v0, System.Int32 v1, System.Int32 v2, System.Int32 v3) { }
        // RVA: 0x04DA3090  token: 0x60000AA
        public static System.Void UnpackQuater(System.UInt64 pack, System.Int32& v0, System.Int32& v1, System.Int32& v2, System.Int32& v3) { }
        // RVA: 0x04D874D0  token: 0x60000AB
        public static System.UInt32 Pack16(System.Int32 hi, System.Int32 low) { }
        // RVA: 0x04D875B0  token: 0x60000AC
        public static System.Int32 Unpack16Hi(System.UInt32 pack) { }
        // RVA: 0x04D875C0  token: 0x60000AD
        public static System.Int32 Unpack16Low(System.UInt32 pack) { }
        // RVA: 0x04D91CF0  token: 0x60000AE
        public static System.UInt64 Pack32(System.Int32 hi, System.Int32 low) { }
        // RVA: 0x04D9D8A0  token: 0x60000AF
        public static System.Int32 Unpack32Hi(System.UInt64 pack) { }
        // RVA: 0x04D88BE0  token: 0x60000B0
        public static System.Int32 Unpack32Low(System.UInt64 pack) { }
        // RVA: 0x09ED6D78  token: 0x60000B1
        public static System.Void CalcFinalDataWorldPositionNormalTangent(MagicaReductionMesh.FinalData final, System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector4>& wtanList) { }
        // RVA: 0x09ED6224  token: 0x60000B2
        public static System.Void CalcFinalDataChildWorldPositionNormalTangent(MagicaReductionMesh.FinalData final, System.Int32 meshIndex, System.Collections.Generic.List<UnityEngine.Vector3> sposList, System.Collections.Generic.List<UnityEngine.Vector3> snorList, System.Collections.Generic.List<UnityEngine.Vector4> stanList, System.Collections.Generic.List<UnityEngine.Vector3>& wposList, System.Collections.Generic.List<UnityEngine.Vector3>& wnorList, System.Collections.Generic.List<UnityEngine.Vector4>& wtanList) { }
        // RVA: 0x09ED770C  token: 0x60000B3
        public static System.Void CalcLocalPositionNormalTangent(UnityEngine.Transform root, System.Collections.Generic.List<UnityEngine.Vector3> wposList, System.Collections.Generic.List<UnityEngine.Vector3> wnorList, System.Collections.Generic.List<UnityEngine.Vector4> wtanList) { }
        // RVA: 0x041E1670  token: 0x60000B4
        public System.Void .ctor() { }

    }

}

namespace Microsoft.CodeAnalysis
{

    // TypeToken: 0x2000002  // size: 0x10
    public sealed class EmbeddedAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000001
        public System.Void .ctor() { }

    }

}

namespace System.Runtime.CompilerServices
{

    // TypeToken: 0x2000003  // size: 0x10
    public sealed class IsUnmanagedAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000002
        public System.Void .ctor() { }

    }

}

