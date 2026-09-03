// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.AIModule.dll
// Classes:  29
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000011  // size: 0x80
    public sealed class OnNavMeshPreUpdate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x053DEDC8  token: 0x60000AD
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x053DEDB8  token: 0x60000AE
        public virtual System.Void Invoke() { }

    }

    // TypeToken: 0x2000012  // size: 0x80
    public sealed class OnUnloadFinishDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0349C210  token: 0x60000AF
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x60000B0
        public virtual System.Void Invoke(System.UInt64 surfTileID) { }

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

namespace UnityEngine.AI
{

    // TypeToken: 0x2000004  // size: 0x14
    public sealed struct NavMeshPathStatus
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AI.NavMeshPathStatus PathComplete;  // const
        public static UnityEngine.AI.NavMeshPathStatus PathPartial;  // const
        public static UnityEngine.AI.NavMeshPathStatus PathInvalid;  // const
        public static UnityEngine.AI.NavMeshPathStatus PathProcessing;  // const

    }

    // TypeToken: 0x2000005  // size: 0x30
    public sealed class NavMeshPath
    {
        // Fields
        private System.IntPtr m_Ptr;  // 0x10
        private UnityEngine.Vector3[] m_Corners;  // 0x18
        private System.Int32[] m_Areas;  // 0x20
        private UnityEngine.Vector3[] m_DividingPos;  // 0x28

        // Properties
        UnityEngine.Vector3[] corners { get; /* RVA: 0x053969FC */ }
        UnityEngine.AI.NavMeshPathStatus status { get; /* RVA: 0x03F7E9A0 */ }
        System.Boolean needRepath { get; /* RVA: 0x034ED480 */ set; /* RVA: 0x032C21F0 */ }
        System.Boolean calcAutoRepath { set; /* RVA: 0x032C21A0 */ }

        // Methods
        // RVA: 0x034847A0  token: 0x6000003
        public System.Void .ctor() { }
        // RVA: 0x04B21C40  token: 0x6000004
        protected virtual System.Void Finalize() { }
        // RVA: 0x034847C0  token: 0x6000005
        private static System.IntPtr InitializeNavMeshPath() { }
        // RVA: 0x04B21CD0  token: 0x6000006
        private static System.Void DestroyNavMeshPath(System.IntPtr ptr) { }
        // RVA: 0x0457B2E0  token: 0x6000007
        public System.Int32 GetCornersNonAlloc(UnityEngine.Vector3[] corners, System.Int32[] areas, UnityEngine.Vector3[] dividingPos) { }
        // RVA: 0x04693A20  token: 0x6000008
        public System.Int32 GetPolyCount() { }
        // RVA: 0x04992840  token: 0x6000009
        private UnityEngine.Vector3[] CalculateCornersInternal() { }
        // RVA: 0x03F7FC10  token: 0x600000A
        private System.Void ClearCornersInternal() { }
        // RVA: 0x03F7FBE0  token: 0x600000B
        public System.Void ClearCorners() { }
        // RVA: 0x04992810  token: 0x600000C
        private System.Void CalculateCorners() { }

    }

    // TypeToken: 0x2000006  // size: 0x10
    public static class NavMeshBuilder
    {
        // Methods
        // RVA: 0x0B389C7C  token: 0x6000012
        public static System.Void CollectSources(UnityEngine.Bounds includedWorldBounds, System.Int32 includedLayerMask, UnityEngine.AI.NavMeshCollectGeometry geometry, System.Int32 defaultArea, System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildMarkup> markups, System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildSource> results) { }
        // RVA: 0x0B389ACC  token: 0x6000013
        public static System.Void CollectSources(UnityEngine.Transform root, System.Int32 includedLayerMask, UnityEngine.AI.NavMeshCollectGeometry geometry, System.Int32 defaultArea, System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildMarkup> markups, System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildSource> results) { }
        // RVA: 0x0B389A98  token: 0x6000014
        private static UnityEngine.AI.NavMeshBuildSource[] CollectSourcesInternal(System.Int32 includedLayerMask, UnityEngine.Bounds includedWorldBounds, UnityEngine.Transform root, System.Boolean useBounds, UnityEngine.AI.NavMeshCollectGeometry geometry, System.Int32 defaultArea, UnityEngine.AI.NavMeshBuildMarkup[] markups) { }
        // RVA: 0x04415270  token: 0x6000015
        public static System.Void CollectSourcesV2(UnityEngine.Bounds includedWorldBounds, System.Int32 includedLayerMask, System.Int32 defaultArea, System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildMarkup> markups, System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildSource> results) { }
        // RVA: 0x05397650  token: 0x6000016
        private static UnityEngine.AI.NavMeshBuildSource[] CollectSourcesV2Internal(System.Int32 includedLayerMask, UnityEngine.Bounds includedWorldBounds, System.Int32 defaultArea, UnityEngine.AI.NavMeshBuildMarkup[] markups) { }
        // RVA: 0x0B3898A8  token: 0x6000017
        public static UnityEngine.AI.NavMeshData BuildNavMeshData(UnityEngine.AI.NavMeshBuildSettings buildSettings, System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildSource> sources, UnityEngine.Bounds localBounds, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, System.Boolean useSubJob) { }
        // RVA: 0x0B389F0C  token: 0x6000018
        public static System.Boolean UpdateNavMeshData(UnityEngine.AI.NavMeshData data, UnityEngine.AI.NavMeshBuildSettings buildSettings, System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildSource> sources, UnityEngine.Bounds localBounds, System.Boolean useSubJob) { }
        // RVA: 0x0B389EEC  token: 0x6000019
        private static System.Boolean UpdateNavMeshDataListInternal(UnityEngine.AI.NavMeshData data, UnityEngine.AI.NavMeshBuildSettings buildSettings, System.Object sources, UnityEngine.Bounds localBounds, System.Boolean useSubJob) { }
        // RVA: 0x04972100  token: 0x600001A
        public static UnityEngine.AsyncOperation UpdateNavMeshDataAsync(UnityEngine.AI.NavMeshData data, UnityEngine.AI.NavMeshBuildSettings buildSettings, System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildSource> sources, UnityEngine.Bounds localBounds, System.Boolean useSubJob) { }
        // RVA: 0x05397DFC  token: 0x600001B
        private static UnityEngine.AsyncOperation UpdateNavMeshDataAsyncListInternal(UnityEngine.AI.NavMeshData data, UnityEngine.AI.NavMeshBuildSettings buildSettings, System.Object sources, UnityEngine.Bounds localBounds, System.Boolean useSubJob) { }
        // RVA: 0x0B389A38  token: 0x600001C
        private static UnityEngine.AI.NavMeshBuildSource[] CollectSourcesInternal_Injected(System.Int32 includedLayerMask, UnityEngine.Bounds& includedWorldBounds, UnityEngine.Transform root, System.Boolean useBounds, UnityEngine.AI.NavMeshCollectGeometry geometry, System.Int32 defaultArea, UnityEngine.AI.NavMeshBuildMarkup[] markups) { }
        // RVA: 0x04D109D0  token: 0x600001D
        private static UnityEngine.AI.NavMeshBuildSource[] CollectSourcesV2Internal_Injected(System.Int32 includedLayerMask, UnityEngine.Bounds& includedWorldBounds, System.Int32 defaultArea, UnityEngine.AI.NavMeshBuildMarkup[] markups) { }
        // RVA: 0x0B389E88  token: 0x600001E
        private static System.Boolean UpdateNavMeshDataListInternal_Injected(UnityEngine.AI.NavMeshData data, UnityEngine.AI.NavMeshBuildSettings& buildSettings, System.Object sources, UnityEngine.Bounds& localBounds, System.Boolean useSubJob) { }
        // RVA: 0x04972270  token: 0x600001F
        private static UnityEngine.AsyncOperation UpdateNavMeshDataAsyncListInternal_Injected(UnityEngine.AI.NavMeshData data, UnityEngine.AI.NavMeshBuildSettings& buildSettings, System.Object sources, UnityEngine.Bounds& localBounds, System.Boolean useSubJob) { }

    }

    // TypeToken: 0x2000007  // size: 0x18
    public sealed class HGNavMeshAgent : UnityEngine.Behaviour
    {
    }

    // TypeToken: 0x2000008  // size: 0x18
    public sealed class NavMeshAgent : UnityEngine.Behaviour
    {
    }

    // TypeToken: 0x2000009  // size: 0x14
    public sealed struct NavMeshObstacleShape
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AI.NavMeshObstacleShape Capsule;  // const
        public static UnityEngine.AI.NavMeshObstacleShape Box;  // const
        public static UnityEngine.AI.NavMeshObstacleShape Polygon;  // const

    }

    // TypeToken: 0x200000A  // size: 0x18
    public sealed class NavMeshObstacle : UnityEngine.Behaviour
    {
        // Properties
        System.Single height { get; /* RVA: 0x0B38A2D4 */ set; /* RVA: 0x0B38A3AC */ }
        System.Single radius { get; /* RVA: 0x0B38A308 */ set; /* RVA: 0x0B38A3F0 */ }
        System.Boolean carving { set; /* RVA: 0x033E0200 */ }
        System.Boolean carveOnlyStationary { set; /* RVA: 0x033E0110 */ }
        UnityEngine.AI.NavMeshObstacleShape shape { set; /* RVA: 0x033E00C0 */ }
        UnityEngine.Vector3 center { get; /* RVA: 0x0B38A2A8 */ set; /* RVA: 0x05392350 */ }
        UnityEngine.Vector3 size { get; /* RVA: 0x0B38A380 */ set; /* RVA: 0x05392364 */ }
        System.Int32 ignoredSurfaceID { set; /* RVA: 0x033E0160 */ }
        System.Int32 carveArea { set; /* RVA: 0x033E01B0 */ }

        // Methods
        // RVA: 0x0B38A264  token: 0x600002D
        private System.Void get_center_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x033DF040  token: 0x600002E
        private System.Void set_center_Injected(UnityEngine.Vector3& value) { }
        // RVA: 0x0B38A33C  token: 0x600002F
        private System.Void get_size_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x033DF090  token: 0x6000030
        private System.Void set_size_Injected(UnityEngine.Vector3& value) { }

    }

    // TypeToken: 0x200000B  // size: 0x34
    public sealed struct NavMeshHit
    {
        // Fields
        private UnityEngine.Vector3 m_Position;  // 0x10
        private UnityEngine.Vector3 m_Normal;  // 0x1c
        private System.Single m_Distance;  // 0x28
        private System.Int32 m_Mask;  // 0x2c
        private System.Int32 m_Hit;  // 0x30

        // Properties
        UnityEngine.Vector3 position { get; /* RVA: 0x04DBA810 */ set; /* RVA: 0x04DABF80 */ }
        UnityEngine.Vector3 normal { get; /* RVA: 0x04DBA7F0 */ }
        System.Boolean hit { get; /* RVA: 0x0B38A18C */ }

    }

    // TypeToken: 0x200000C  // size: 0x18
    public sealed class NavMeshData : UnityEngine.Object
    {
        // Properties
        UnityEngine.Bounds sourceBounds { get; /* RVA: 0x0349CBE0 */ }
        UnityEngine.Vector3 position { get; /* RVA: 0x03FD1B50 */ set; /* RVA: 0x0B38A164 */ }
        UnityEngine.Quaternion rotation { get; /* RVA: 0x03FD1B80 */ set; /* RVA: 0x0B38A178 */ }

        // Methods
        // RVA: 0x04CAD9C0  token: 0x6000035
        public System.Void .ctor(System.Int32 agentTypeID) { }
        // RVA: 0x04CADA20  token: 0x6000036
        private static System.Void Internal_Create(UnityEngine.AI.NavMeshData mono, System.Int32 agentTypeID) { }
        // RVA: 0x03FD1870  token: 0x600003C
        public System.Void AppendExtOffMeshLink(UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Single width, System.Boolean bidirectional, System.Int32 area, System.Single costModifier) { }
        // RVA: 0x05396C90  token: 0x600003D
        private System.Void Internal_AppendExtOffMeshLink(UnityEngine.Vector3 startLeft, UnityEngine.Vector3 startRight, UnityEngine.Vector3 endRight, UnityEngine.Vector3 endLeft, System.Boolean bidirectional, System.Int32 area, System.Single costModifier) { }
        // RVA: 0x0349CC10  token: 0x600003E
        private System.Void get_sourceBounds_Injected(UnityEngine.Bounds& ret) { }
        // RVA: 0x03FD1BB0  token: 0x600003F
        private System.Void get_position_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x04D30CE0  token: 0x6000040
        private System.Void set_position_Injected(UnityEngine.Vector3& value) { }
        // RVA: 0x03FD1C00  token: 0x6000041
        private System.Void get_rotation_Injected(UnityEngine.Quaternion& ret) { }
        // RVA: 0x04D30C90  token: 0x6000042
        private System.Void set_rotation_Injected(UnityEngine.Quaternion& value) { }
        // RVA: 0x03FD1ED0  token: 0x6000043
        private System.Void Internal_AppendExtOffMeshLink_Injected(UnityEngine.Vector3& startLeft, UnityEngine.Vector3& startRight, UnityEngine.Vector3& endRight, UnityEngine.Vector3& endLeft, System.Boolean bidirectional, System.Int32 area, System.Single costModifier) { }

    }

    // TypeToken: 0x200000D  // size: 0x14
    public sealed struct NavMeshDataInstance
    {
        // Fields
        private System.Int32 <id>k__BackingField;  // 0x10

        // Properties
        System.Boolean valid { get; /* RVA: 0x0349D810 */ }
        System.Int32 id { get; /* RVA: 0x02B77620 */ set; /* RVA: 0x04D86140 */ }
        System.Int32 handleId { get; /* RVA: 0x04DBA750 */ set; /* RVA: 0x04D86140 */ }
        UnityEngine.Object owner { set; /* RVA: 0x0B38A0B4 */ }

        // Methods
        // RVA: 0x0349A640  token: 0x6000049
        public System.Void Remove() { }

    }

    // TypeToken: 0x200000E  // size: 0x54
    public sealed struct NavMeshLinkData
    {
        // Fields
        private System.Boolean m_IsExt;  // 0x10
        private UnityEngine.Vector3 m_StartLeft;  // 0x14
        private UnityEngine.Vector3 m_StartRight;  // 0x20
        private UnityEngine.Vector3 m_EndRight;  // 0x2c
        private UnityEngine.Vector3 m_EndLeft;  // 0x38
        private System.Single m_CostModifier;  // 0x44
        private System.Int32 m_Bidirectional;  // 0x48
        private System.Int32 m_Area;  // 0x4c
        private System.Int32 m_AgentTypeID;  // 0x50

        // Properties
        System.Boolean isExt { get; /* RVA: 0x04DBA890 */ set; /* RVA: 0x04D87180 */ }
        UnityEngine.Vector3 startLeft { get; /* RVA: 0x04DBA8A0 */ set; /* RVA: 0x04DABF50 */ }
        UnityEngine.Vector3 startRight { get; /* RVA: 0x04DBA8C0 */ set; /* RVA: 0x04D8C970 */ }
        UnityEngine.Vector3 endRight { get; /* RVA: 0x04DBA870 */ set; /* RVA: 0x04D90500 */ }
        UnityEngine.Vector3 endLeft { get; /* RVA: 0x04DBA850 */ set; /* RVA: 0x04D90C20 */ }
        System.Single costModifier { get; /* RVA: 0x053957B0 */ set; /* RVA: 0x04D8E060 */ }
        System.Boolean bidirectional { get; /* RVA: 0x0A75F924 */ set; /* RVA: 0x043A5630 */ }
        System.Int32 area { get; /* RVA: 0x04DBA840 */ set; /* RVA: 0x04D86670 */ }
        System.Int32 agentTypeID { get; /* RVA: 0x04DBA830 */ set; /* RVA: 0x04D86610 */ }

    }

    // TypeToken: 0x200000F  // size: 0x14
    public sealed struct NavMeshLinkInstance
    {
        // Fields
        private System.Int32 <id>k__BackingField;  // 0x10

        // Properties
        System.Boolean valid { get; /* RVA: 0x0B38A1A0 */ }
        System.Int32 id { get; /* RVA: 0x02B77620 */ set; /* RVA: 0x04D86140 */ }
        UnityEngine.Object owner { set; /* RVA: 0x0B38A1B4 */ }

        // Methods
        // RVA: 0x0B38A194  token: 0x6000060
        public System.Void Remove() { }

    }

    // TypeToken: 0x2000010  // size: 0x10
    public static class NavMesh
    {
        // Fields
        public static UnityEngine.AI.NavMesh.OnNavMeshPreUpdate onPreUpdate;  // static @ 0x0
        public static UnityEngine.AI.NavMesh.OnUnloadFinishDelegate onUnloadFinish;  // static @ 0x8

        // Methods
        // RVA: 0x03F2EF40  token: 0x6000062
        private static System.Void Internal_CallOnNavMeshPreUpdate() { }
        // RVA: 0x05392CE8  token: 0x6000063
        public static System.Boolean Raycast(UnityEngine.Vector3 sourcePosition, UnityEngine.Vector3 targetPosition, UnityEngine.AI.NavMeshHit& hit, System.Int32 areaMask) { }
        // RVA: 0x03F7FA20  token: 0x6000064
        public static System.Boolean CalculatePath(UnityEngine.Vector3 sourcePosition, UnityEngine.Vector3 targetPosition, System.Int32 areaMask, UnityEngine.AI.NavMeshPath path, System.Boolean isReserveEnd, System.Int32 agentTypeID, System.Single radius) { }
        // RVA: 0x053969C4  token: 0x6000065
        private static System.Boolean CalculatePathInternal(UnityEngine.Vector3 sourcePosition, UnityEngine.Vector3 targetPosition, System.Int32 areaMask, UnityEngine.AI.NavMeshPath path, System.Boolean isReserveEnd, System.Int32 agentTypeID, System.Single radius) { }
        // RVA: 0x0B38A758  token: 0x6000066
        public static System.Boolean FindClosestEdge(UnityEngine.Vector3 sourcePosition, UnityEngine.AI.NavMeshHit& hit, System.Int32 areaMask) { }
        // RVA: 0x053907C0  token: 0x6000067
        public static System.Boolean SamplePosition(UnityEngine.Vector3 sourcePosition, UnityEngine.AI.NavMeshHit& hit, System.Single maxDistance, System.Int32 areaMask) { }
        // RVA: 0x047C8BB0  token: 0x6000068
        public static System.Int32 GetAreaFromName(System.String areaName) { }
        // RVA: 0x0349A470  token: 0x6000069
        public static UnityEngine.AI.NavMeshDataInstance AddNavMeshData(UnityEngine.AI.NavMeshData navMeshData) { }
        // RVA: 0x0349CF50  token: 0x600006A
        public static UnityEngine.AI.NavMeshDataInstance AddNavMeshData(UnityEngine.AI.NavMeshData navMeshData, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0B38A9F4  token: 0x600006B
        public static System.Void UpdateSurfaceTransform(UnityEngine.AI.NavMeshDataInstance handle, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0349C470  token: 0x600006C
        public static System.Void RemoveNavMeshData(UnityEngine.AI.NavMeshDataInstance handle) { }
        // RVA: 0x0349D830  token: 0x600006D
        private static System.Boolean IsValidNavMeshDataHandle(System.Int32 handle) { }
        // RVA: 0x0B38A8BC  token: 0x600006E
        private static System.Boolean IsValidLinkHandle(System.Int32 handle) { }
        // RVA: 0x0B38A7B4  token: 0x600006F
        private static System.Boolean InternalSetOwner(System.Int32 dataID, System.Int32 ownerID) { }
        // RVA: 0x0B38A774  token: 0x6000070
        private static System.Boolean InternalSetLinkOwner(System.Int32 linkID, System.Int32 ownerID) { }
        // RVA: 0x0349A500  token: 0x6000071
        private static System.Int32 AddNavMeshDataInternal(UnityEngine.AI.NavMeshData navMeshData) { }
        // RVA: 0x05392C60  token: 0x6000072
        private static System.Int32 AddNavMeshDataTransformedInternal(UnityEngine.AI.NavMeshData navMeshData, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0349C380  token: 0x6000073
        private static System.Void RemoveNavMeshDataInternal(System.Int32 handle) { }
        // RVA: 0x043A5590  token: 0x6000074
        public static UnityEngine.AI.NavMeshLinkInstance AddLink(UnityEngine.AI.NavMeshLinkData link, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0349C2C0  token: 0x6000075
        public static System.Void RemoveLink(UnityEngine.AI.NavMeshLinkInstance handle) { }
        // RVA: 0x05397578  token: 0x6000076
        private static System.Int32 AddLinkInternal(UnityEngine.AI.NavMeshLinkData link, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0349C280  token: 0x6000077
        private static System.Void RemoveLinkInternal(System.Int32 handle) { }
        // RVA: 0x03BAA6A0  token: 0x6000078
        public static UnityEngine.AI.NavMeshBuildSettings GetSettingsByID(System.Int32 agentTypeID) { }
        // RVA: 0x04228B50  token: 0x6000079
        public static UnityEngine.AI.NavMeshBuildSettings GetSettingsByIndex(System.Int32 index) { }
        // RVA: 0x0B38A9E0  token: 0x600007A
        private static System.Void UpdateSurfaceTransformInternal(System.Int32 surfaceID, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0349D2E0  token: 0x600007B
        public static System.Boolean DetachFromSurface(System.Int32 chunkSurfaceId, System.Int32 builderSurfaceId, System.Int32 indexX, System.Int32 indexZ) { }
        // RVA: 0x03BAA730  token: 0x600007C
        public static System.Boolean SetBuilderSurfaceParams(System.Int32 surfID, System.Single chunkSize, System.Single tileSize, System.Single centerX, System.Single centerZ, System.Single sizeX, System.Single sizeZ) { }
        // RVA: 0x0349D270  token: 0x600007D
        private static System.Boolean Internal_DetachFromSurface(System.Int32 chunkSurfaceId, System.Int32 builderSurfaceId, System.Int32 indexX, System.Int32 indexZ) { }
        // RVA: 0x03BAA900  token: 0x600007E
        private static System.Boolean Internal_SetBuilderSurfaceParams(System.Int32 surfID, System.Single chunkSize, System.Single tileSize, System.Single centerX, System.Single centerZ, System.Single sizeX, System.Single sizeZ) { }
        // RVA: 0x0B38A8F0  token: 0x600007F
        public static System.Int32 LoadDynamicBakeTile(UnityEngine.AI.NavMeshData navMeshData) { }
        // RVA: 0x0B38A988  token: 0x6000080
        public static System.Void UnloadDynamicBakeTile(System.Int32 dynamicTileID) { }
        // RVA: 0x0B38A854  token: 0x6000081
        private static System.Int32 Internal_LoadDynamicBakeTile(UnityEngine.AI.NavMeshData navMeshData) { }
        // RVA: 0x0B38A888  token: 0x6000082
        private static System.Void Internal_UnloadDynamicBakeTile(System.Int32 dynamicTileID) { }
        // RVA: 0x0B38A960  token: 0x6000083
        public static System.Int32 ModifyVolumeArea(System.Collections.Generic.List<UnityEngine.Vector3> center, System.Collections.Generic.List<UnityEngine.Vector3> ext, System.Collections.Generic.List<UnityEngine.Quaternion> rotation, System.Int32 area) { }
        // RVA: 0x0B38A8FC  token: 0x6000084
        private static System.Int32 ModifyBoxAreaInternal(System.Collections.Generic.List<UnityEngine.Vector3> center, System.Collections.Generic.List<UnityEngine.Vector3> ext, System.Collections.Generic.List<UnityEngine.Quaternion> rotation, System.Int32 area) { }
        // RVA: 0x045F73E0  token: 0x6000085
        public static System.Int32 ModifyVolumeArea(System.Collections.Generic.List<UnityEngine.Vector3> verts, System.Single hMin, System.Single hMax, System.Int32 area) { }
        // RVA: 0x045F73F0  token: 0x6000086
        private static System.Int32 ModifyPolygonAreaInternal(System.Collections.Generic.List<UnityEngine.Vector3> verts, System.Single hMin, System.Single hMax, System.Int32 area) { }
        // RVA: 0x05396CDC  token: 0x6000087
        public static System.Void LoadExtOffMeshLinks(System.Int32 surfaceID) { }
        // RVA: 0x04D51230  token: 0x6000088
        private static System.Void Internal_LoadExtOffMeshLinks(System.Int32 surfaceID) { }
        // RVA: 0x04025080  token: 0x6000089
        public static System.Void BeginLoadingBatch() { }
        // RVA: 0x04024F90  token: 0x600008A
        public static System.Void EndLoadingBatch() { }
        // RVA: 0x04024F50  token: 0x600008B
        public static System.Boolean IsLoadingBatch() { }
        // RVA: 0x0349D8D0  token: 0x600008C
        public static UnityEngine.AsyncOperation AddNavMeshDataAsync(System.Int32& surfaceID, UnityEngine.AI.NavMeshData navMeshData, System.UInt64 surfTileID) { }
        // RVA: 0x0349D970  token: 0x600008D
        private static UnityEngine.AsyncOperation AddNavMeshDataAsyncInternal(System.Int32& surfaceID, UnityEngine.AI.NavMeshData navMeshData, System.UInt64 surfTileID) { }
        // RVA: 0x0B38A6FC  token: 0x600008E
        public static System.Void CancelAsyncAdd(System.UInt64 surfTileID) { }
        // RVA: 0x0B38A7F4  token: 0x600008F
        private static System.Void Internal_CancelAsyncAdd(System.UInt64 surfaceID) { }
        // RVA: 0x05392C74  token: 0x6000090
        public static System.Void UnloadDataAsync(System.Int32 surfaceID, System.UInt64 surfTileID) { }
        // RVA: 0x045B0C50  token: 0x6000091
        private static System.Void Internal_UnloadDataAsync(System.Int32 surfaceID, System.UInt64 surfTileID) { }
        // RVA: 0x04480240  token: 0x6000092
        private static System.Void Internal_InvokeOnUnloadFinish(System.UInt64 surfTileID) { }
        // RVA: 0x05392C7C  token: 0x6000093
        public static System.Boolean IsSurfaceUnloading(System.UInt64 surfTileID) { }
        // RVA: 0x045BBC20  token: 0x6000094
        private static System.Boolean Internal_IsSurfaceUnloading(System.UInt64 surfTileID) { }
        // RVA: 0x0B38A76C  token: 0x6000095
        public static System.Void ForceClearUnloadDataRequest() { }
        // RVA: 0x0B38A828  token: 0x6000096
        private static System.Void Internal_ForceClearUnloadDataRequest() { }
        // RVA: 0x03F7FB00  token: 0x6000097
        public static System.Void CalculatePathAsync(UnityEngine.Vector3 sourcePosition, UnityEngine.Vector3 targetPosition, System.Int32 areaMask, UnityEngine.AI.NavMeshPath path, System.Boolean isReserveEnd, System.Int32 agentTypeID, System.Single radius) { }
        // RVA: 0x0539698C  token: 0x6000098
        private static System.Void CalculatePathAsyncInternal(UnityEngine.Vector3 sourcePosition, UnityEngine.Vector3 targetPosition, System.Int32 areaMask, UnityEngine.AI.NavMeshPath path, System.Boolean isReserveEnd, System.Int32 agentTypeID, System.Single radius) { }
        // RVA: 0x05395578  token: 0x6000099
        public static System.Void SetFindPathMaxIterPerFrame(System.Int32 maxIterPerFrame) { }
        // RVA: 0x04D71780  token: 0x600009A
        private static System.Void Internal_SetFindPathMaxIterPerFrame(System.Int32 maxIterPerFrame) { }
        // RVA: 0x05391E78  token: 0x600009B
        public static System.Void CancelAsyncCalculatePath(UnityEngine.AI.NavMeshPath path) { }
        // RVA: 0x04627810  token: 0x600009C
        private static System.Void Internal_CancelAsyncCalculatePath(UnityEngine.AI.NavMeshPath path) { }
        // RVA: 0x05391E80  token: 0x600009D
        public static System.Void RegisterAutoRepath(UnityEngine.AI.NavMeshPath path) { }
        // RVA: 0x04970E80  token: 0x600009E
        private static System.Void Internal_RegisterAutoRepath(UnityEngine.AI.NavMeshPath path) { }
        // RVA: 0x05391E70  token: 0x600009F
        public static System.Void UnregisterAutoRepath(UnityEngine.AI.NavMeshPath path) { }
        // RVA: 0x04D116D0  token: 0x60000A0
        private static System.Void Internal_UnregisterAutoRepath(UnityEngine.AI.NavMeshPath path) { }
        // RVA: 0x0B38A970  token: 0x60000A1
        public static System.Boolean RaycastNearestPoint(UnityEngine.Vector3 sourcePosition, UnityEngine.Vector3 targetPosition, UnityEngine.AI.NavMeshHit& hit, System.Int32 areaMask) { }
        // RVA: 0x041CC6E0  token: 0x60000A2
        private static System.Boolean Raycast_Injected(UnityEngine.Vector3& sourcePosition, UnityEngine.Vector3& targetPosition, UnityEngine.AI.NavMeshHit& hit, System.Int32 areaMask) { }
        // RVA: 0x03F80440  token: 0x60000A3
        private static System.Boolean CalculatePathInternal_Injected(UnityEngine.Vector3& sourcePosition, UnityEngine.Vector3& targetPosition, System.Int32 areaMask, UnityEngine.AI.NavMeshPath path, System.Boolean isReserveEnd, System.Int32 agentTypeID, System.Single radius) { }
        // RVA: 0x0B38A704  token: 0x60000A4
        private static System.Boolean FindClosestEdge_Injected(UnityEngine.Vector3& sourcePosition, UnityEngine.AI.NavMeshHit& hit, System.Int32 areaMask) { }
        // RVA: 0x03AD65B0  token: 0x60000A5
        private static System.Boolean SamplePosition_Injected(UnityEngine.Vector3& sourcePosition, UnityEngine.AI.NavMeshHit& hit, System.Single maxDistance, System.Int32 areaMask) { }
        // RVA: 0x049BE650  token: 0x60000A6
        private static System.Int32 AddNavMeshDataTransformedInternal_Injected(UnityEngine.AI.NavMeshData navMeshData, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation) { }
        // RVA: 0x043A5650  token: 0x60000A7
        private static System.Int32 AddLinkInternal_Injected(UnityEngine.AI.NavMeshLinkData& link, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation) { }
        // RVA: 0x03BAA6E0  token: 0x60000A8
        private static System.Void GetSettingsByID_Injected(System.Int32 agentTypeID, UnityEngine.AI.NavMeshBuildSettings& ret) { }
        // RVA: 0x04228B90  token: 0x60000A9
        private static System.Void GetSettingsByIndex_Injected(System.Int32 index, UnityEngine.AI.NavMeshBuildSettings& ret) { }
        // RVA: 0x0B38A990  token: 0x60000AA
        private static System.Void UpdateSurfaceTransformInternal_Injected(System.Int32 surfaceID, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation) { }
        // RVA: 0x03F804C0  token: 0x60000AB
        private static System.Void CalculatePathAsyncInternal_Injected(UnityEngine.Vector3& sourcePosition, UnityEngine.Vector3& targetPosition, System.Int32 areaMask, UnityEngine.AI.NavMeshPath path, System.Boolean isReserveEnd, System.Int32 agentTypeID, System.Single radius) { }
        // RVA: 0x03D37610  token: 0x60000AC
        private static System.Boolean RaycastNearestPoint_Injected(UnityEngine.Vector3& sourcePosition, UnityEngine.Vector3& targetPosition, UnityEngine.AI.NavMeshHit& hit, System.Int32 areaMask) { }

    }

    // TypeToken: 0x2000013  // size: 0x14
    public sealed struct NavMeshBuildSourceShape
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AI.NavMeshBuildSourceShape Mesh;  // const
        public static UnityEngine.AI.NavMeshBuildSourceShape Terrain;  // const
        public static UnityEngine.AI.NavMeshBuildSourceShape Box;  // const
        public static UnityEngine.AI.NavMeshBuildSourceShape Sphere;  // const
        public static UnityEngine.AI.NavMeshBuildSourceShape Capsule;  // const
        public static UnityEngine.AI.NavMeshBuildSourceShape ModifierBox;  // const
        public static UnityEngine.AI.NavMeshBuildSourceShape PhysicsTriangleMesh;  // const
        public static UnityEngine.AI.NavMeshBuildSourceShape PhysicsConvexMesh;  // const
        public static UnityEngine.AI.NavMeshBuildSourceShape PhysicsTerrain;  // const
        public static UnityEngine.AI.NavMeshBuildSourceShape ModifierPoly;  // const

    }

    // TypeToken: 0x2000014  // size: 0x14
    public sealed struct NavMeshCollectGeometry
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AI.NavMeshCollectGeometry RenderMeshes;  // const
        public static UnityEngine.AI.NavMeshCollectGeometry PhysicsColliders;  // const

    }

    // TypeToken: 0x2000015  // size: 0x88
    public sealed struct NavMeshBuildSource
    {
        // Fields
        private UnityEngine.Matrix4x4 m_Transform;  // 0x10
        private UnityEngine.Vector3 m_Size;  // 0x50
        private UnityEngine.AI.NavMeshBuildSourceShape m_Shape;  // 0x5c
        private System.Int32 m_Area;  // 0x60
        private System.Int32 m_InstanceID;  // 0x64
        private System.Int32 m_ComponentID;  // 0x68
        private System.Boolean m_IsConvex;  // 0x6c
        private System.IntPtr m_EcsShape;  // 0x70
        public System.Collections.Generic.List<UnityEngine.Vector3> convexVerts;  // 0x78
        public System.Single convexHmin;  // 0x80
        public System.Single convexHmax;  // 0x84

        // Properties
        UnityEngine.Matrix4x4 transform { get; /* RVA: 0x04DBA7A0 */ set; /* RVA: 0x04DBA7D0 */ }
        UnityEngine.Vector3 size { get; /* RVA: 0x04DBA780 */ set; /* RVA: 0x04D89690 */ }
        UnityEngine.AI.NavMeshBuildSourceShape shape { get; /* RVA: 0x04DBA770 */ set; /* RVA: 0x04D86630 */ }
        System.Int32 area { get; /* RVA: 0x04DBA760 */ set; /* RVA: 0x04D865A0 */ }
        UnityEngine.Object sourceObject { get; /* RVA: 0x0B38989C */ }
        UnityEngine.Component component { get; /* RVA: 0x0B389890 */ }

        // Methods
        // RVA: 0x0B389828  token: 0x60000BB
        private static UnityEngine.Component InternalGetComponent(System.Int32 instanceID) { }
        // RVA: 0x0B38985C  token: 0x60000BC
        private static UnityEngine.Object InternalGetObject(System.Int32 instanceID) { }

    }

    // TypeToken: 0x2000016  // size: 0x20
    public sealed struct NavMeshBuildMarkup
    {
        // Fields
        private System.Int32 m_OverrideArea;  // 0x10
        private System.Int32 m_Area;  // 0x14
        private System.Int32 m_IgnoreFromBuild;  // 0x18
        private System.Int32 m_InstanceID;  // 0x1c

        // Properties
        System.Boolean overrideArea { set; /* RVA: 0x0B3897A4 */ }
        System.Int32 area { set; /* RVA: 0x04D88B50 */ }
        System.Boolean ignoreFromBuild { set; /* RVA: 0x02F129D0 */ }
        UnityEngine.Transform root { set; /* RVA: 0x0B3897AC */ }

    }

    // TypeToken: 0x2000017  // size: 0x6C
    public sealed struct NavMeshBuildSettings
    {
        // Fields
        private System.Int32 m_AgentTypeID;  // 0x10
        private System.Single m_AgentRadius;  // 0x14
        private System.Single m_AgentHeight;  // 0x18
        private System.Single m_AgentSlope;  // 0x1c
        private System.Single m_AgentClimb;  // 0x20
        private System.Single m_LedgeDropHeight;  // 0x24
        private System.Single m_MaxJumpAcrossDistance;  // 0x28
        private System.Single m_MinRegionArea;  // 0x2c
        private System.Int32 m_OverrideVoxelSize;  // 0x30
        private System.Single m_VoxelSize;  // 0x34
        private System.Int32 m_OverrideTileSize;  // 0x38
        private System.Int32 m_TileSize;  // 0x3c
        private System.Int32 m_AccuratePlacement;  // 0x40
        private System.UInt32 m_MaxJobWorkers;  // 0x44
        private System.Int32 m_PreserveTilesOutsideBounds;  // 0x48
        private System.Int32 m_PartitioningMethod;  // 0x4c
        private System.Int32 m_FilterLowHangingObstacles;  // 0x50
        private System.Int32 m_FilterLedgeSpans;  // 0x54
        private System.Int32 m_FilterWalkableLowHeightSpans;  // 0x58
        private System.Int32 m_erodeAgainstWallSpans;  // 0x5c
        private System.Int32 m_cliffIndent;  // 0x60
        private System.Int32 m_DynamicNavmeshSurfaceType;  // 0x64
        private UnityEngine.AI.NavMeshBuildDebugSettings m_Debug;  // 0x68

        // Properties
        System.Int32 agentTypeID { get; /* RVA: 0x04DBA750 */ set; /* RVA: 0x04D86140 */ }
        System.Single agentRadius { get; /* RVA: 0x04DBA740 */ }
        System.Single agentHeight { get; /* RVA: 0x04DBA730 */ }
        System.Single agentClimb { get; /* RVA: 0x04DBA720 */ }
        System.Single minRegionArea { set; /* RVA: 0x04D86900 */ }
        System.Boolean overrideVoxelSize { set; /* RVA: 0x03BAA7E0 */ }
        System.Single voxelSize { set; /* RVA: 0x04D89860 */ }
        System.Boolean overrideTileSize { set; /* RVA: 0x03BAA7C0 */ }
        System.Int32 tileSize { set; /* RVA: 0x04D86720 */ }
        System.Int32 partitioningMethod { set; /* RVA: 0x04D86670 */ }
        System.Boolean filterLowHangingObstacles { set; /* RVA: 0x03BAA740 */ }
        System.Boolean filterLedgeSpans { set; /* RVA: 0x03BAA760 */ }
        System.Boolean filterWalkableLowHeightSpans { set; /* RVA: 0x03BAA780 */ }
        System.Boolean erodeAgainstWallSpans { set; /* RVA: 0x03BAA7A0 */ }
        System.Int32 cliffIndent { set; /* RVA: 0x04D865A0 */ }

    }

    // TypeToken: 0x2000018  // size: 0x11
    public sealed struct NavMeshBuildDebugSettings
    {
        // Fields
        private System.Byte m_Flags;  // 0x10

    }

}

namespace UnityEngine.Experimental.AI
{

    // TypeToken: 0x2000019  // size: 0x18
    public sealed struct PolygonId : System.IEquatable`1
    {
        // Fields
        private System.UInt64 polyRef;  // 0x10

        // Methods
        // RVA: 0x0314CD60  token: 0x60000D1
        public System.Boolean IsNull() { }
        // RVA: 0x02FB1840  token: 0x60000D2
        public static System.Boolean op_Equality(UnityEngine.Experimental.AI.PolygonId x, UnityEngine.Experimental.AI.PolygonId y) { }
        // RVA: 0x0B38AB58  token: 0x60000D3
        public static System.Boolean op_Inequality(UnityEngine.Experimental.AI.PolygonId x, UnityEngine.Experimental.AI.PolygonId y) { }
        // RVA: 0x04DBA8E0  token: 0x60000D4
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B38AB50  token: 0x60000D5
        public virtual System.Boolean Equals(UnityEngine.Experimental.AI.PolygonId rhs) { }
        // RVA: 0x0B38AAE4  token: 0x60000D6
        public virtual System.Boolean Equals(System.Object obj) { }

    }

    // TypeToken: 0x200001A  // size: 0x28
    public sealed struct NavMeshLocation
    {
        // Fields
        private readonly UnityEngine.Experimental.AI.PolygonId <polygon>k__BackingField;  // 0x10
        private readonly UnityEngine.Vector3 <position>k__BackingField;  // 0x18

        // Properties
        UnityEngine.Experimental.AI.PolygonId polygon { get; /* RVA: 0x02B76770 */ }
        UnityEngine.Vector3 position { get; /* RVA: 0x04D9C4D0 */ }

    }

    // TypeToken: 0x200001B  // size: 0x14
    public sealed struct PathQueryStatus
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Experimental.AI.PathQueryStatus Failure;  // const
        public static UnityEngine.Experimental.AI.PathQueryStatus Success;  // const
        public static UnityEngine.Experimental.AI.PathQueryStatus InProgress;  // const
        public static UnityEngine.Experimental.AI.PathQueryStatus StatusDetailMask;  // const
        public static UnityEngine.Experimental.AI.PathQueryStatus WrongMagic;  // const
        public static UnityEngine.Experimental.AI.PathQueryStatus WrongVersion;  // const
        public static UnityEngine.Experimental.AI.PathQueryStatus OutOfMemory;  // const
        public static UnityEngine.Experimental.AI.PathQueryStatus InvalidParam;  // const
        public static UnityEngine.Experimental.AI.PathQueryStatus BufferTooSmall;  // const
        public static UnityEngine.Experimental.AI.PathQueryStatus OutOfNodes;  // const
        public static UnityEngine.Experimental.AI.PathQueryStatus PartialResult;  // const

    }

    // TypeToken: 0x200001C  // size: 0x18
    public sealed struct NavMeshWorld
    {
        // Fields
        private System.IntPtr world;  // 0x10

        // Methods
        // RVA: 0x03BAA610  token: 0x60000D9
        public static UnityEngine.Experimental.AI.NavMeshWorld GetDefaultWorld() { }
        // RVA: 0x03BAA660  token: 0x60000DA
        private static System.Void GetDefaultWorld_Injected(UnityEngine.Experimental.AI.NavMeshWorld& ret) { }

    }

    // TypeToken: 0x200001D  // size: 0x18
    public sealed struct NavMeshQuery : System.IDisposable
    {
        // Fields
        private System.IntPtr m_NavMeshQuery;  // 0x10

        // Methods
        // RVA: 0x03BAA860  token: 0x60000DB
        public System.Void .ctor(UnityEngine.Experimental.AI.NavMeshWorld world, Unity.Collections.Allocator allocator, System.Int32 pathNodePoolSize) { }
        // RVA: 0x040F38C0  token: 0x60000DC
        public virtual System.Void Dispose() { }
        // RVA: 0x05395580  token: 0x60000DD
        private static System.IntPtr Create(UnityEngine.Experimental.AI.NavMeshWorld world, System.Int32 nodePoolSize) { }
        // RVA: 0x040F3940  token: 0x60000DE
        private static System.Void Destroy(System.IntPtr navMeshQuery) { }
        // RVA: 0x034ED8A0  token: 0x60000DF
        private static UnityEngine.Experimental.AI.NavMeshLocation MapLocation(System.IntPtr navMeshQuery, UnityEngine.Vector3 position, UnityEngine.Vector3 extents, System.Int32 agentTypeID, System.Int32 areaMask) { }
        // RVA: 0x034ED7D0  token: 0x60000E0
        public UnityEngine.Experimental.AI.NavMeshLocation MapLocation(UnityEngine.Vector3 position, UnityEngine.Vector3 extents, System.Int32 agentTypeID, System.Int32 areaMask) { }
        // RVA: 0x0B38A498  token: 0x60000E1
        private static UnityEngine.Experimental.AI.NavMeshLocation MoveLocation(System.IntPtr navMeshQuery, UnityEngine.Experimental.AI.NavMeshLocation location, UnityEngine.Vector3 target, System.Int32 areaMask) { }
        // RVA: 0x0B38A4DC  token: 0x60000E2
        public UnityEngine.Experimental.AI.NavMeshLocation MoveLocation(UnityEngine.Experimental.AI.NavMeshLocation location, UnityEngine.Vector3 target, System.Int32 areaMask) { }
        // RVA: 0x0B38A5C4  token: 0x60000E3
        private static UnityEngine.Experimental.AI.PathQueryStatus Raycast(System.IntPtr navMeshQuery, UnityEngine.Experimental.AI.NavMeshLocation start, UnityEngine.Vector3 targetPosition, System.Int32 areaMask, System.Void* costs, UnityEngine.AI.NavMeshHit& hit, System.Void* path, System.Int32& pathCount, System.Int32 maxPath) { }
        // RVA: 0x0B38A61C  token: 0x60000E4
        public UnityEngine.Experimental.AI.PathQueryStatus Raycast(UnityEngine.AI.NavMeshHit& hit, UnityEngine.Experimental.AI.NavMeshLocation start, UnityEngine.Vector3 targetPosition, System.Int32 areaMask, Unity.Collections.NativeArray<System.Single> costs) { }
        // RVA: 0x0426FE80  token: 0x60000E5
        private static System.IntPtr Create_Injected(UnityEngine.Experimental.AI.NavMeshWorld& world, System.Int32 nodePoolSize) { }
        // RVA: 0x034ED930  token: 0x60000E6
        private static System.Void MapLocation_Injected(System.IntPtr navMeshQuery, UnityEngine.Vector3& position, UnityEngine.Vector3& extents, System.Int32 agentTypeID, System.Int32 areaMask, UnityEngine.Experimental.AI.NavMeshLocation& ret) { }
        // RVA: 0x0B38A434  token: 0x60000E7
        private static System.Void MoveLocation_Injected(System.IntPtr navMeshQuery, UnityEngine.Experimental.AI.NavMeshLocation& location, UnityEngine.Vector3& target, System.Int32 areaMask, UnityEngine.Experimental.AI.NavMeshLocation& ret) { }
        // RVA: 0x0B38A560  token: 0x60000E8
        private static UnityEngine.Experimental.AI.PathQueryStatus Raycast_Injected(System.IntPtr navMeshQuery, UnityEngine.Experimental.AI.NavMeshLocation& start, UnityEngine.Vector3& targetPosition, System.Int32 areaMask, System.Void* costs, UnityEngine.AI.NavMeshHit& hit, System.Void* path, System.Int32& pathCount, System.Int32 maxPath) { }

    }

}

