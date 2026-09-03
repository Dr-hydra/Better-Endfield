// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.TerrainModule.dll
// Classes:  21
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000006  // size: 0x80
    public sealed class HeightmapChangedCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B42C6F0  token: 0x6000018
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x0621CA6C  token: 0x6000019
        public virtual System.Void Invoke(UnityEngine.Terrain terrain, UnityEngine.RectInt heightRegion, System.Boolean synched) { }

    }

    // TypeToken: 0x2000007  // size: 0x80
    public sealed class TextureChangedCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B4307A0  token: 0x600001A
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x0622E128  token: 0x600001B
        public virtual System.Void Invoke(UnityEngine.Terrain terrain, System.String textureName, UnityEngine.RectInt texelRegion, System.Boolean synched) { }

    }

    // TypeToken: 0x200000D  // size: 0x14
    public sealed struct BoundaryValueType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TerrainData.BoundaryValueType MaxHeightmapRes;  // const
        public static UnityEngine.TerrainData.BoundaryValueType MinDetailResPerPatch;  // const
        public static UnityEngine.TerrainData.BoundaryValueType MaxDetailResPerPatch;  // const
        public static UnityEngine.TerrainData.BoundaryValueType MaxDetailPatchCount;  // const
        public static UnityEngine.TerrainData.BoundaryValueType MaxDetailsPerRes;  // const
        public static UnityEngine.TerrainData.BoundaryValueType MinAlphamapRes;  // const
        public static UnityEngine.TerrainData.BoundaryValueType MaxAlphamapRes;  // const
        public static UnityEngine.TerrainData.BoundaryValueType MinCliffIndexMapRes;  // const
        public static UnityEngine.TerrainData.BoundaryValueType MaxCliffIndexMapRes;  // const
        public static UnityEngine.TerrainData.BoundaryValueType MinBaseMapRes;  // const
        public static UnityEngine.TerrainData.BoundaryValueType MaxBaseMapRes;  // const

    }

    // TypeToken: 0x2000012  // size: 0x18
    public sealed class <>c__DisplayClass3_0
    {
        // Fields
        public System.Int32 groupID;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60000B9
        public System.Void .ctor() { }
        // RVA: 0x0B430A1C  token: 0x60000BA
        private System.Boolean <CreateFromPlacement>b__0(UnityEngine.Terrain x) { }

    }

    // TypeToken: 0x2000014  // size: 0x18
    public sealed class <>c__DisplayClass2_0
    {
        // Fields
        public System.Boolean onlyAutoConnectedTerrains;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60000BF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000015  // size: 0x20
    public sealed class <>c__DisplayClass2_1
    {
        // Fields
        public UnityEngine.Terrain t;  // 0x10
        public UnityEngine.TerrainUtils.TerrainUtility.<>c__DisplayClass2_0 CS$<>8__locals1;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60000C0
        public System.Void .ctor() { }
        // RVA: 0x0B4309A0  token: 0x60000C1
        private System.Boolean <CollectTerrains>b__0(UnityEngine.Terrain x) { }

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
    public sealed class IsReadOnlyAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000002
        public System.Void .ctor() { }

    }

}

namespace UnityEngine
{

    // TypeToken: 0x2000004  // size: 0x18
    public sealed class Terrain : UnityEngine.Behaviour
    {
        // Properties
        UnityEngine.TerrainData terrainData { get; /* RVA: 0x0B43053C */ set; /* RVA: 0x0B43075C */ }
        System.Single detailObjectDistance { set; /* RVA: 0x0B4305F8 */ }
        System.Single detailObjectDensity { set; /* RVA: 0x0B4305B4 */ }
        System.Int32 lightmapIndex { set; /* RVA: 0x0B430680 */ }
        UnityEngine.Vector4 lightmapScaleOffset { set; /* RVA: 0x0B430704 */ }
        UnityEngine.Material materialTemplate { get; /* RVA: 0x0B430508 */ set; /* RVA: 0x0B430718 */ }
        System.Boolean allowAutoConnect { get; /* RVA: 0x0B4304A0 */ set; /* RVA: 0x0B430570 */ }
        System.Int32 groupingID { get; /* RVA: 0x0B4304D4 */ }
        System.Boolean drawInstanced { set; /* RVA: 0x0B43063C */ }
        UnityEngine.Terrain[] activeTerrains { get; /* RVA: 0x0B430474 */ }

        // Methods
        // RVA: 0x0B430410  token: 0x600000F
        public System.Void SetNeighbors(UnityEngine.Terrain left, UnityEngine.Terrain top, UnityEngine.Terrain right, UnityEngine.Terrain bottom) { }
        // RVA: 0x0B4303D4  token: 0x6000010
        public System.Void Flush() { }
        // RVA: 0x0B430408  token: 0x6000012
        public static System.Void GetActiveTerrains(System.Collections.Generic.List<UnityEngine.Terrain> terrainList) { }
        // RVA: 0x034AD340  token: 0x6000013
        private static System.Void Internal_FillActiveTerrainList(System.Object terrainList) { }
        // RVA: 0x05393520  token: 0x6000014
        public System.Void .ctor() { }
        // RVA: 0x0B4306C0  token: 0x6000015
        private System.Void set_lightmapScaleOffset_Injected(UnityEngine.Vector4& value) { }

    }

    // TypeToken: 0x2000005  // size: 0x10
    public static class TerrainCallbacks
    {
        // Fields
        private static UnityEngine.TerrainCallbacks.HeightmapChangedCallback heightmapChanged;  // static @ 0x0
        private static UnityEngine.TerrainCallbacks.TextureChangedCallback textureChanged;  // static @ 0x8

        // Methods
        // RVA: 0x0B42C7C4  token: 0x6000016
        private static System.Void InvokeHeightmapChangedCallback(UnityEngine.TerrainData terrainData, UnityEngine.RectInt heightRegion, System.Boolean synched) { }
        // RVA: 0x0B42C898  token: 0x6000017
        private static System.Void InvokeTextureChangedCallback(UnityEngine.TerrainData terrainData, System.String textureName, UnityEngine.RectInt texelRegion, System.Boolean synched) { }

    }

    // TypeToken: 0x2000008  // size: 0x20
    public sealed class TreePrototype
    {
        // Fields
        private UnityEngine.GameObject m_Prefab;  // 0x10
        private System.Single m_BendFactor;  // 0x18
        private System.Int32 m_NavMeshLod;  // 0x1c

        // Properties
        UnityEngine.GameObject prefab { get; /* RVA: 0x04D9B600 */ set; /* RVA: 0x053908C0 */ }
        System.Single bendFactor { get; /* RVA: 0x04DBAB50 */ set; /* RVA: 0x04D86390 */ }
        System.Int32 navMeshLod { get; /* RVA: 0x04D9B610 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000021
        public System.Void .ctor() { }
        // RVA: 0x0B430874  token: 0x6000022
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x04DBBEA0  token: 0x6000023
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B4308D8  token: 0x6000024
        private System.Boolean Equals(UnityEngine.TreePrototype other) { }

    }

    // TypeToken: 0x2000009  // size: 0x14
    public sealed struct DetailRenderMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.DetailRenderMode GrassBillboard;  // const
        public static UnityEngine.DetailRenderMode VertexLit;  // const
        public static UnityEngine.DetailRenderMode Grass;  // const

    }

    // TypeToken: 0x200000A  // size: 0x68
    public sealed class DetailPrototype
    {
        // Fields
        private static readonly UnityEngine.Color DefaultHealthColor;  // static @ 0x0
        private static readonly UnityEngine.Color DefaultDryColor;  // static @ 0x10
        private UnityEngine.GameObject m_Prototype;  // 0x10
        private UnityEngine.Texture2D m_PrototypeTexture;  // 0x18
        private UnityEngine.Color m_HealthyColor;  // 0x20
        private UnityEngine.Color m_DryColor;  // 0x30
        private System.Single m_MinWidth;  // 0x40
        private System.Single m_MaxWidth;  // 0x44
        private System.Single m_MinHeight;  // 0x48
        private System.Single m_MaxHeight;  // 0x4c
        private System.Int32 m_NoiseSeed;  // 0x50
        private System.Single m_NoiseSpread;  // 0x54
        private System.Single m_HoleEdgePadding;  // 0x58
        private System.Int32 m_RenderMode;  // 0x5c
        private System.Int32 m_UsePrototypeMesh;  // 0x60
        private System.Int32 m_UseInstancing;  // 0x64

        // Properties
        UnityEngine.GameObject prototype { get; /* RVA: 0x04D9B600 */ set; /* RVA: 0x053908C0 */ }
        UnityEngine.Texture2D prototypeTexture { get; /* RVA: 0x04D96330 */ set; /* RVA: 0x05392C40 */ }
        System.Single minWidth { get; /* RVA: 0x05391640 */ set; /* RVA: 0x04D86470 */ }
        System.Single maxWidth { get; /* RVA: 0x05391650 */ set; /* RVA: 0x04D86480 */ }
        System.Single minHeight { get; /* RVA: 0x05391660 */ set; /* RVA: 0x04D8D380 */ }
        System.Single maxHeight { get; /* RVA: 0x04DBD330 */ set; /* RVA: 0x04D91480 */ }
        System.Single noiseSpread { get; /* RVA: 0x04DBD340 */ set; /* RVA: 0x04D86E10 */ }
        UnityEngine.Color healthyColor { get; /* RVA: 0x04DBD320 */ set; /* RVA: 0x04D8C210 */ }
        UnityEngine.Color dryColor { get; /* RVA: 0x04DBCB30 */ set; /* RVA: 0x04D87490 */ }
        UnityEngine.DetailRenderMode renderMode { get; /* RVA: 0x04DBD350 */ set; /* RVA: 0x04D865C0 */ }
        System.Boolean usePrototypeMesh { get; /* RVA: 0x0B42C6B0 */ set; /* RVA: 0x0B42C6D4 */ }
        System.Boolean useInstancing { set; /* RVA: 0x0B42C6B8 */ }

        // Methods
        // RVA: 0x0B42C4B8  token: 0x600003C
        public System.Void .ctor() { }
        // RVA: 0x0B42C570  token: 0x600003D
        public System.Void .ctor(UnityEngine.DetailPrototype other) { }
        // RVA: 0x0B42C250  token: 0x600003E
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x04DBBEA0  token: 0x600003F
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B42C2B4  token: 0x6000040
        private System.Boolean Equals(UnityEngine.DetailPrototype other) { }
        // RVA: 0x0B42C460  token: 0x6000041
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000B  // size: 0x38
    public sealed struct TreeInstance
    {
        // Fields
        public UnityEngine.Vector3 position;  // 0x10
        public System.Single widthScale;  // 0x1c
        public System.Single heightScale;  // 0x20
        public System.Single rotation;  // 0x24
        public UnityEngine.Color32 color;  // 0x28
        public UnityEngine.Color32 lightmapColor;  // 0x2c
        public System.Int32 prototypeIndex;  // 0x30
        private System.Single temporaryDistance;  // 0x34

    }

    // TypeToken: 0x200000C  // size: 0x18
    public sealed class TerrainData : UnityEngine.Object
    {
        // Fields
        private static System.String k_ScriptingInterfaceName;  // const
        private static System.String k_ScriptingInterfacePrefix;  // const
        private static System.String k_HeightmapPrefix;  // const
        private static System.String k_DetailDatabasePrefix;  // const
        private static System.String k_TreeDatabasePrefix;  // const
        private static System.String k_SplatDatabasePrefix;  // const
        private static readonly System.Int32 k_MaximumResolution;  // static @ 0x0
        private static readonly System.Int32 k_MinimumDetailResolutionPerPatch;  // static @ 0x4
        private static readonly System.Int32 k_MaximumDetailResolutionPerPatch;  // static @ 0x8
        private static readonly System.Int32 k_MaximumDetailPatchCount;  // static @ 0xc
        private static readonly System.Int32 k_MaximumDetailsPerRes;  // static @ 0x10
        private static readonly System.Int32 k_MinimumAlphamapResolution;  // static @ 0x14
        private static readonly System.Int32 k_MaximumAlphamapResolution;  // static @ 0x18
        private static readonly System.Int32 k_MinimumCliffIndexMapResolution;  // static @ 0x1c
        private static readonly System.Int32 k_MaximumCliffIndexMapResolution;  // static @ 0x20
        private static readonly System.Int32 k_MinimumBaseMapResolution;  // static @ 0x24
        private static readonly System.Int32 k_MaximumBaseMapResolution;  // static @ 0x28

        // Properties
        UnityEngine.RenderTexture heightmapTexture { get; /* RVA: 0x0B42DDFC */ }
        System.Int32 heightmapResolution { get; /* RVA: 0x0B42DDF0 */ set; /* RVA: 0x0B42E278 */ }
        System.Int32 internalHeightmapResolution { get; /* RVA: 0x0B42DE74 */ set; /* RVA: 0x0B42E3A0 */ }
        UnityEngine.Texture holesTexture { get; /* RVA: 0x0B42DE44 */ }
        System.Boolean enableHolesTextureCompression { get; /* RVA: 0x0B42DDBC */ set; /* RVA: 0x0B42E234 */ }
        System.Int32 holesResolution { get; /* RVA: 0x0B42DE30 */ }
        UnityEngine.Vector3 size { get; /* RVA: 0x0B42DEEC */ set; /* RVA: 0x0B42E424 */ }
        System.Int32 detailWidth { get; /* RVA: 0x0B42DD88 */ }
        System.Int32 detailHeight { get; /* RVA: 0x0B42DCB8 */ }
        System.Int32 detailResolution { get; /* RVA: 0x0B42DD54 */ }
        System.Int32 detailResolutionPerPatch { get; /* RVA: 0x0B42DD20 */ }
        UnityEngine.DetailPrototype[] detailPrototypes { get; /* RVA: 0x0B42DCEC */ set; /* RVA: 0x0B42E1F0 */ }
        UnityEngine.TreeInstance[] treeInstances { get; /* RVA: 0x0B42DF4C */ }
        UnityEngine.TreePrototype[] treePrototypes { get; /* RVA: 0x0B42DF58 */ set; /* RVA: 0x0B42E47C */ }
        System.Int32 alphamapLayers { get; /* RVA: 0x0B42DBB8 */ }
        System.Int32 alphamapResolution { get; /* RVA: 0x0B42DBAC */ set; /* RVA: 0x0B42E000 */ }
        System.Int32 Internal_alphamapResolution { get; /* RVA: 0x0B42DB44 */ set; /* RVA: 0x0B42DFC0 */ }
        System.Int32 alphamapWidth { get; /* RVA: 0x0B42DBAC */ }
        System.Int32 alphamapHeight { get; /* RVA: 0x0B42DBAC */ }
        System.Int32 baseMapResolution { get; /* RVA: 0x0B42DCAC */ }
        System.Int32 Internal_baseMapResolution { get; /* RVA: 0x0B42DB78 */ }
        System.Int32 alphamapTextureCount { get; /* RVA: 0x0B42DBEC */ }
        UnityEngine.Texture2D[] alphamapTextures { get; /* RVA: 0x0B42DC20 */ }
        UnityEngine.TerrainLayer[] terrainLayers { get; /* RVA: 0x0B42DF18 */ set; /* RVA: 0x0B42E438 */ }
        UnityEngine.Terrain[] users { get; /* RVA: 0x0B42DF8C */ }

        // Methods
        // RVA: 0x0B42CA7C  token: 0x6000042
        private static System.Int32 GetBoundaryValue(UnityEngine.TerrainData.BoundaryValueType type) { }
        // RVA: 0x0B42DAEC  token: 0x6000043
        public System.Void .ctor() { }
        // RVA: 0x0B42CE90  token: 0x6000044
        private static System.Void Internal_Create(UnityEngine.TerrainData terrainData) { }
        // RVA: 0x0B42D188  token: 0x600004D
        private System.Boolean IsHolesTextureCompressed() { }
        // RVA: 0x0B42CC2C  token: 0x600004E
        private UnityEngine.RenderTexture GetHolesTexture() { }
        // RVA: 0x0B42CAB0  token: 0x600004F
        private UnityEngine.Texture2D GetCompressedHolesTexture() { }
        // RVA: 0x0B42CD48  token: 0x6000053
        public System.Single GetInterpolatedHeight(System.Single x, System.Single y) { }
        // RVA: 0x0B42CB44  token: 0x6000054
        public System.Single[,] GetHeights(System.Int32 xBase, System.Int32 yBase, System.Int32 width, System.Int32 height) { }
        // RVA: 0x0B42CF24  token: 0x6000055
        private System.Single[,] Internal_GetHeights(System.Int32 xBase, System.Int32 yBase, System.Int32 width, System.Int32 height) { }
        // RVA: 0x0B42D6BC  token: 0x6000056
        public System.Void SetHeights(System.Int32 xBase, System.Int32 yBase, System.Single[,] heights) { }
        // RVA: 0x0B42D128  token: 0x6000057
        private System.Void Internal_SetHeights(System.Int32 xBase, System.Int32 yBase, System.Int32 width, System.Int32 height, System.Single[,] heights) { }
        // RVA: 0x0B42CC60  token: 0x6000058
        public System.Boolean[,] GetHoles(System.Int32 xBase, System.Int32 yBase, System.Int32 width, System.Int32 height) { }
        // RVA: 0x0B42CF84  token: 0x6000059
        private System.Boolean[,] Internal_GetHoles(System.Int32 xBase, System.Int32 yBase, System.Int32 width, System.Int32 height) { }
        // RVA: 0x0B42CE00  token: 0x600005A
        public UnityEngine.Vector3 GetInterpolatedNormal(System.Single x, System.Single y) { }
        // RVA: 0x0B42D3D8  token: 0x600005D
        public System.Void SetDetailResolution(System.Int32 detailResolution, System.Int32 resolutionPerPatch) { }
        // RVA: 0x0B42D0D8  token: 0x600005E
        private System.Void Internal_SetDetailResolution(System.Int32 patchCount, System.Int32 resolutionPerPatch) { }
        // RVA: 0x0B42D1BC  token: 0x6000061
        public System.Void RefreshPrototypes() { }
        // RVA: 0x0B42CE30  token: 0x6000064
        public System.Int32[] GetSupportedLayers(System.Int32 xBase, System.Int32 yBase, System.Int32 totalWidth, System.Int32 totalHeight) { }
        // RVA: 0x0B42CAE4  token: 0x6000065
        public System.Int32[,] GetDetailLayer(System.Int32 xBase, System.Int32 yBase, System.Int32 width, System.Int32 height, System.Int32 layer) { }
        // RVA: 0x0B42D348  token: 0x6000066
        public System.Void SetDetailLayer(System.Int32 xBase, System.Int32 yBase, System.Int32 layer, System.Int32[,] details) { }
        // RVA: 0x0B42D078  token: 0x6000067
        private System.Void Internal_SetDetailLayer(System.Int32 xBase, System.Int32 yBase, System.Int32 totalWidth, System.Int32 totalHeight, System.Int32 detailIndex, System.Int32[,] data) { }
        // RVA: 0x0B42CFE4  token: 0x6000069
        private UnityEngine.TreeInstance[] Internal_GetTreeInstances() { }
        // RVA: 0x0B42D948  token: 0x600006A
        public System.Void SetTreeInstances(UnityEngine.TreeInstance[] instances, System.Boolean snapToHeightmap) { }
        // RVA: 0x0B42C9F4  token: 0x600006E
        public System.Single[,,] GetAlphamaps(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height) { }
        // RVA: 0x0B42CEC4  token: 0x600006F
        private System.Single[,,] Internal_GetAlphamaps(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height) { }
        // RVA: 0x0B42C980  token: 0x6000072
        private System.Single GetAlphamapResolutionInternal() { }
        // RVA: 0x0B42D1F0  token: 0x6000079
        public System.Void SetAlphamaps(System.Int32 x, System.Int32 y, System.Single[,,] map) { }
        // RVA: 0x0B42D018  token: 0x600007A
        private System.Void Internal_SetAlphamaps(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Single[,,] map) { }
        // RVA: 0x0B42C9B4  token: 0x600007B
        public UnityEngine.Texture2D GetAlphamapTexture(System.Int32 index) { }
        // RVA: 0x0B42D99C  token: 0x6000081
        private static System.Void .cctor() { }
        // RVA: 0x0B42DEA8  token: 0x6000082
        private System.Void get_size_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x0B42E3E0  token: 0x6000083
        private System.Void set_size_Injected(UnityEngine.Vector3& value) { }
        // RVA: 0x0B42CD9C  token: 0x6000084
        private System.Void GetInterpolatedNormal_Injected(System.Single x, System.Single y, UnityEngine.Vector3& ret) { }

    }

    // TypeToken: 0x200000E  // size: 0x18
    public sealed class TerrainLayer : UnityEngine.Object
    {
        // Properties
        UnityEngine.Texture2D diffuseTexture { get; /* RVA: 0x0B42E620 */ set; /* RVA: 0x0B42EA28 */ }
        UnityEngine.Texture2D normalMapTexture { get; /* RVA: 0x0B42E79C */ set; /* RVA: 0x0B42EBA4 */ }
        UnityEngine.Texture2D maskMapTexture { get; /* RVA: 0x0B42E734 */ set; /* RVA: 0x0B42EB1C */ }
        UnityEngine.Vector2 tileSize { get; /* RVA: 0x0B42E954 */ set; /* RVA: 0x0B42ED6C */ }
        UnityEngine.Vector2 tileOffset { get; /* RVA: 0x0B42E8EC */ set; /* RVA: 0x0B42ED0C */ }
        UnityEngine.Color specular { get; /* RVA: 0x0B42E87C */ set; /* RVA: 0x0B42ECB4 */ }
        System.Single metallic { get; /* RVA: 0x0B42E768 */ set; /* RVA: 0x0B42EB60 */ }
        System.Single smoothness { get; /* RVA: 0x0B42E804 */ set; /* RVA: 0x0B42EC2C */ }
        System.Single normalScale { get; /* RVA: 0x0B42E7D0 */ set; /* RVA: 0x0B42EBE8 */ }
        UnityEngine.Vector4 diffuseRemapMin { get; /* RVA: 0x0B42E5F4 */ set; /* RVA: 0x0B42EA14 */ }
        UnityEngine.Vector4 diffuseRemapMax { get; /* RVA: 0x0B42E584 */ set; /* RVA: 0x0B42E9BC */ }
        UnityEngine.Vector4 maskMapRemapMin { get; /* RVA: 0x0B42E708 */ set; /* RVA: 0x0B42EB08 */ }
        UnityEngine.Vector4 maskMapRemapMax { get; /* RVA: 0x0B42E698 */ set; /* RVA: 0x0B42EAB0 */ }

        // Methods
        // RVA: 0x0B42E4F4  token: 0x6000085
        public System.Void .ctor() { }
        // RVA: 0x0B42E4C0  token: 0x6000086
        private static System.Void Internal_Create(UnityEngine.TerrainLayer layer) { }
        // RVA: 0x0B42E910  token: 0x60000A1
        private System.Void get_tileSize_Injected(UnityEngine.Vector2& ret) { }
        // RVA: 0x0B42ED28  token: 0x60000A2
        private System.Void set_tileSize_Injected(UnityEngine.Vector2& value) { }
        // RVA: 0x0B42E8A8  token: 0x60000A3
        private System.Void get_tileOffset_Injected(UnityEngine.Vector2& ret) { }
        // RVA: 0x0B42ECC8  token: 0x60000A4
        private System.Void set_tileOffset_Injected(UnityEngine.Vector2& value) { }
        // RVA: 0x0B42E838  token: 0x60000A5
        private System.Void get_specular_Injected(UnityEngine.Color& ret) { }
        // RVA: 0x0B42EC70  token: 0x60000A6
        private System.Void set_specular_Injected(UnityEngine.Color& value) { }
        // RVA: 0x0B42E5B0  token: 0x60000A7
        private System.Void get_diffuseRemapMin_Injected(UnityEngine.Vector4& ret) { }
        // RVA: 0x0B42E9D0  token: 0x60000A8
        private System.Void set_diffuseRemapMin_Injected(UnityEngine.Vector4& value) { }
        // RVA: 0x0B42E540  token: 0x60000A9
        private System.Void get_diffuseRemapMax_Injected(UnityEngine.Vector4& ret) { }
        // RVA: 0x0B42E978  token: 0x60000AA
        private System.Void set_diffuseRemapMax_Injected(UnityEngine.Vector4& value) { }
        // RVA: 0x0B42E6C4  token: 0x60000AB
        private System.Void get_maskMapRemapMin_Injected(UnityEngine.Vector4& ret) { }
        // RVA: 0x0B42EAC4  token: 0x60000AC
        private System.Void set_maskMapRemapMin_Injected(UnityEngine.Vector4& value) { }
        // RVA: 0x0B42E654  token: 0x60000AD
        private System.Void get_maskMapRemapMax_Injected(UnityEngine.Vector4& ret) { }
        // RVA: 0x0B42EA6C  token: 0x60000AE
        private System.Void set_maskMapRemapMax_Injected(UnityEngine.Vector4& value) { }

    }

}

namespace UnityEngine.TerrainUtils
{

    // TypeToken: 0x200000F  // size: 0x14
    public sealed struct TerrainMapStatusCode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TerrainUtils.TerrainMapStatusCode OK;  // const
        public static UnityEngine.TerrainUtils.TerrainMapStatusCode Overlapping;  // const
        public static UnityEngine.TerrainUtils.TerrainMapStatusCode SizeMismatch;  // const
        public static UnityEngine.TerrainUtils.TerrainMapStatusCode EdgeAlignmentMismatch;  // const

    }

    // TypeToken: 0x2000010  // size: 0x18
    public sealed struct TerrainTileCoord
    {
        // Fields
        public readonly System.Int32 tileX;  // 0x10
        public readonly System.Int32 tileZ;  // 0x14

        // Methods
        // RVA: 0x04D85C40  token: 0x60000AF
        public System.Void .ctor(System.Int32 tileX, System.Int32 tileZ) { }

    }

    // TypeToken: 0x2000011  // size: 0x28
    public class TerrainMap
    {
        // Fields
        private UnityEngine.Vector3 m_patchSize;  // 0x10
        private UnityEngine.TerrainUtils.TerrainMapStatusCode m_errorCode;  // 0x1c
        private System.Collections.Generic.Dictionary<UnityEngine.TerrainUtils.TerrainTileCoord,UnityEngine.Terrain> m_terrainTiles;  // 0x20

        // Properties
        System.Collections.Generic.Dictionary<UnityEngine.TerrainUtils.TerrainTileCoord,UnityEngine.Terrain> terrainTiles { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x0B42F34C  token: 0x60000B0
        public UnityEngine.Terrain GetTerrain(System.Int32 tileX, System.Int32 tileZ) { }
        // RVA: 0x0B42F100  token: 0x60000B1
        public static UnityEngine.TerrainUtils.TerrainMap CreateFromPlacement(UnityEngine.Terrain originTerrain, System.Predicate<UnityEngine.Terrain> filter, System.Boolean fullValidation) { }
        // RVA: 0x0B42EEE4  token: 0x60000B2
        public static UnityEngine.TerrainUtils.TerrainMap CreateFromPlacement(UnityEngine.Vector2 gridOrigin, UnityEngine.Vector2 gridSize, System.Predicate<UnityEngine.Terrain> filter, System.Boolean fullValidation) { }
        // RVA: 0x0B42FD08  token: 0x60000B4
        public System.Void .ctor() { }
        // RVA: 0x0B42ED88  token: 0x60000B5
        private System.Void AddTerrainInternal(System.Int32 x, System.Int32 z, UnityEngine.Terrain terrain) { }
        // RVA: 0x0B42F3C8  token: 0x60000B6
        private System.Boolean TryToAddTerrain(System.Int32 tileX, System.Int32 tileZ, UnityEngine.Terrain terrain) { }
        // RVA: 0x0B42F4B0  token: 0x60000B7
        private System.Void ValidateTerrain(System.Int32 tileX, System.Int32 tileZ) { }
        // RVA: 0x0B42FBB4  token: 0x60000B8
        private UnityEngine.TerrainUtils.TerrainMapStatusCode Validate() { }

    }

    // TypeToken: 0x2000013  // size: 0x10
    public static class TerrainUtility
    {
        // Methods
        // RVA: 0x0B4303A0  token: 0x60000BB
        private static System.Boolean ValidTerrainsExist() { }
        // RVA: 0x0B4300C8  token: 0x60000BC
        private static System.Void ClearConnectivity() { }
        // RVA: 0x0B430148  token: 0x60000BD
        private static System.Collections.Generic.Dictionary<System.Int32,UnityEngine.TerrainUtils.TerrainMap> CollectTerrains(System.Boolean onlyAutoConnectedTerrains) { }
        // RVA: 0x0B42FD84  token: 0x60000BE
        public static System.Void AutoConnect() { }

    }

}

