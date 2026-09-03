// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.AIModule.dll
// Classes:  29
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

CLASS: OnNavMeshPreUpdate
TYPE:  sealed class
TOKEN: 0x2000011
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x053DEDC8  token=0x60000AD  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x053DEDB8  token=0x60000AE  System.Void Invoke()
END_CLASS

CLASS: OnUnloadFinishDelegate
TYPE:  sealed class
TOKEN: 0x2000012
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0349C210  token=0x60000AF  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x60000B0  System.Void Invoke(System.UInt64 surfTileID)
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

CLASS: System.Runtime.CompilerServices.IsReadOnlyAttribute
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000002  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.AI.NavMeshPathStatus
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AI.NavMeshPathStatusPathComplete  // const
  public    static  UnityEngine.AI.NavMeshPathStatusPathPartial  // const
  public    static  UnityEngine.AI.NavMeshPathStatusPathInvalid  // const
  public    static  UnityEngine.AI.NavMeshPathStatusPathProcessing  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AI.NavMeshPath
TYPE:  sealed class
TOKEN: 0x2000005
SIZE:  0x30
FIELDS:
  private           System.IntPtr                   m_Ptr  // 0x10
  private           UnityEngine.Vector3[]           m_Corners  // 0x18
  private           System.Int32[]                  m_Areas  // 0x20
  private           UnityEngine.Vector3[]           m_DividingPos  // 0x28
PROPERTIES:
  corners  get=0x053969FC
  status  get=0x03F7E9A0
  needRepath  get=0x034ED480  set=0x032C21F0
  calcAutoRepath  set=0x032C21A0
METHODS:
  RVA=0x034847A0  token=0x6000003  System.Void .ctor()
  RVA=0x04B21C40  token=0x6000004  System.Void Finalize()
  RVA=0x034847C0  token=0x6000005  System.IntPtr InitializeNavMeshPath()
  RVA=0x04B21CD0  token=0x6000006  System.Void DestroyNavMeshPath(System.IntPtr ptr)
  RVA=0x0457B2E0  token=0x6000007  System.Int32 GetCornersNonAlloc(UnityEngine.Vector3[] corners, System.Int32[] areas, UnityEngine.Vector3[] dividingPos)
  RVA=0x04693A20  token=0x6000008  System.Int32 GetPolyCount()
  RVA=0x04992840  token=0x6000009  UnityEngine.Vector3[] CalculateCornersInternal()
  RVA=0x03F7FC10  token=0x600000A  System.Void ClearCornersInternal()
  RVA=0x03F7FBE0  token=0x600000B  System.Void ClearCorners()
  RVA=0x04992810  token=0x600000C  System.Void CalculateCorners()
END_CLASS

CLASS: UnityEngine.AI.NavMeshBuilder
TYPE:  static class
TOKEN: 0x2000006
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B389C7C  token=0x6000012  System.Void CollectSources(UnityEngine.Bounds includedWorldBounds, System.Int32 includedLayerMask, UnityEngine.AI.NavMeshCollectGeometry geometry, System.Int32 defaultArea, System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildMarkup> markups, System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildSource> results)
  RVA=0x0B389ACC  token=0x6000013  System.Void CollectSources(UnityEngine.Transform root, System.Int32 includedLayerMask, UnityEngine.AI.NavMeshCollectGeometry geometry, System.Int32 defaultArea, System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildMarkup> markups, System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildSource> results)
  RVA=0x0B389A98  token=0x6000014  UnityEngine.AI.NavMeshBuildSource[] CollectSourcesInternal(System.Int32 includedLayerMask, UnityEngine.Bounds includedWorldBounds, UnityEngine.Transform root, System.Boolean useBounds, UnityEngine.AI.NavMeshCollectGeometry geometry, System.Int32 defaultArea, UnityEngine.AI.NavMeshBuildMarkup[] markups)
  RVA=0x04415270  token=0x6000015  System.Void CollectSourcesV2(UnityEngine.Bounds includedWorldBounds, System.Int32 includedLayerMask, System.Int32 defaultArea, System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildMarkup> markups, System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildSource> results)
  RVA=0x05397650  token=0x6000016  UnityEngine.AI.NavMeshBuildSource[] CollectSourcesV2Internal(System.Int32 includedLayerMask, UnityEngine.Bounds includedWorldBounds, System.Int32 defaultArea, UnityEngine.AI.NavMeshBuildMarkup[] markups)
  RVA=0x0B3898A8  token=0x6000017  UnityEngine.AI.NavMeshData BuildNavMeshData(UnityEngine.AI.NavMeshBuildSettings buildSettings, System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildSource> sources, UnityEngine.Bounds localBounds, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, System.Boolean useSubJob)
  RVA=0x0B389F0C  token=0x6000018  System.Boolean UpdateNavMeshData(UnityEngine.AI.NavMeshData data, UnityEngine.AI.NavMeshBuildSettings buildSettings, System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildSource> sources, UnityEngine.Bounds localBounds, System.Boolean useSubJob)
  RVA=0x0B389EEC  token=0x6000019  System.Boolean UpdateNavMeshDataListInternal(UnityEngine.AI.NavMeshData data, UnityEngine.AI.NavMeshBuildSettings buildSettings, System.Object sources, UnityEngine.Bounds localBounds, System.Boolean useSubJob)
  RVA=0x04972100  token=0x600001A  UnityEngine.AsyncOperation UpdateNavMeshDataAsync(UnityEngine.AI.NavMeshData data, UnityEngine.AI.NavMeshBuildSettings buildSettings, System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildSource> sources, UnityEngine.Bounds localBounds, System.Boolean useSubJob)
  RVA=0x05397DFC  token=0x600001B  UnityEngine.AsyncOperation UpdateNavMeshDataAsyncListInternal(UnityEngine.AI.NavMeshData data, UnityEngine.AI.NavMeshBuildSettings buildSettings, System.Object sources, UnityEngine.Bounds localBounds, System.Boolean useSubJob)
  RVA=0x0B389A38  token=0x600001C  UnityEngine.AI.NavMeshBuildSource[] CollectSourcesInternal_Injected(System.Int32 includedLayerMask, UnityEngine.Bounds& includedWorldBounds, UnityEngine.Transform root, System.Boolean useBounds, UnityEngine.AI.NavMeshCollectGeometry geometry, System.Int32 defaultArea, UnityEngine.AI.NavMeshBuildMarkup[] markups)
  RVA=0x04D109D0  token=0x600001D  UnityEngine.AI.NavMeshBuildSource[] CollectSourcesV2Internal_Injected(System.Int32 includedLayerMask, UnityEngine.Bounds& includedWorldBounds, System.Int32 defaultArea, UnityEngine.AI.NavMeshBuildMarkup[] markups)
  RVA=0x0B389E88  token=0x600001E  System.Boolean UpdateNavMeshDataListInternal_Injected(UnityEngine.AI.NavMeshData data, UnityEngine.AI.NavMeshBuildSettings& buildSettings, System.Object sources, UnityEngine.Bounds& localBounds, System.Boolean useSubJob)
  RVA=0x04972270  token=0x600001F  UnityEngine.AsyncOperation UpdateNavMeshDataAsyncListInternal_Injected(UnityEngine.AI.NavMeshData data, UnityEngine.AI.NavMeshBuildSettings& buildSettings, System.Object sources, UnityEngine.Bounds& localBounds, System.Boolean useSubJob)
END_CLASS

CLASS: UnityEngine.AI.HGNavMeshAgent
TYPE:  sealed class
TOKEN: 0x2000007
SIZE:  0x18
EXTENDS: UnityEngine.Behaviour
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.AI.NavMeshAgent
TYPE:  sealed class
TOKEN: 0x2000008
SIZE:  0x18
EXTENDS: UnityEngine.Behaviour
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.AI.NavMeshObstacleShape
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AI.NavMeshObstacleShapeCapsule  // const
  public    static  UnityEngine.AI.NavMeshObstacleShapeBox  // const
  public    static  UnityEngine.AI.NavMeshObstacleShapePolygon  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AI.NavMeshObstacle
TYPE:  sealed class
TOKEN: 0x200000A
SIZE:  0x18
EXTENDS: UnityEngine.Behaviour
FIELDS:
PROPERTIES:
  height  get=0x0B38A2D4  set=0x0B38A3AC
  radius  get=0x0B38A308  set=0x0B38A3F0
  carving  set=0x033E0200
  carveOnlyStationary  set=0x033E0110
  shape  set=0x033E00C0
  center  get=0x0B38A2A8  set=0x05392350
  size  get=0x0B38A380  set=0x05392364
  ignoredSurfaceID  set=0x033E0160
  carveArea  set=0x033E01B0
METHODS:
  RVA=0x0B38A264  token=0x600002D  System.Void get_center_Injected(UnityEngine.Vector3& ret)
  RVA=0x033DF040  token=0x600002E  System.Void set_center_Injected(UnityEngine.Vector3& value)
  RVA=0x0B38A33C  token=0x600002F  System.Void get_size_Injected(UnityEngine.Vector3& ret)
  RVA=0x033DF090  token=0x6000030  System.Void set_size_Injected(UnityEngine.Vector3& value)
END_CLASS

CLASS: UnityEngine.AI.NavMeshHit
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x34
FIELDS:
  private           UnityEngine.Vector3             m_Position  // 0x10
  private           UnityEngine.Vector3             m_Normal  // 0x1c
  private           System.Single                   m_Distance  // 0x28
  private           System.Int32                    m_Mask  // 0x2c
  private           System.Int32                    m_Hit  // 0x30
PROPERTIES:
  position  get=0x04DBA810  set=0x04DABF80
  normal  get=0x04DBA7F0
  hit  get=0x0B38A18C
METHODS:
END_CLASS

CLASS: UnityEngine.AI.NavMeshData
TYPE:  sealed class
TOKEN: 0x200000C
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
PROPERTIES:
  sourceBounds  get=0x0349CBE0
  position  get=0x03FD1B50  set=0x0B38A164
  rotation  get=0x03FD1B80  set=0x0B38A178
METHODS:
  RVA=0x04CAD9C0  token=0x6000035  System.Void .ctor(System.Int32 agentTypeID)
  RVA=0x04CADA20  token=0x6000036  System.Void Internal_Create(UnityEngine.AI.NavMeshData mono, System.Int32 agentTypeID)
  RVA=0x03FD1870  token=0x600003C  System.Void AppendExtOffMeshLink(UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Single width, System.Boolean bidirectional, System.Int32 area, System.Single costModifier)
  RVA=0x05396C90  token=0x600003D  System.Void Internal_AppendExtOffMeshLink(UnityEngine.Vector3 startLeft, UnityEngine.Vector3 startRight, UnityEngine.Vector3 endRight, UnityEngine.Vector3 endLeft, System.Boolean bidirectional, System.Int32 area, System.Single costModifier)
  RVA=0x0349CC10  token=0x600003E  System.Void get_sourceBounds_Injected(UnityEngine.Bounds& ret)
  RVA=0x03FD1BB0  token=0x600003F  System.Void get_position_Injected(UnityEngine.Vector3& ret)
  RVA=0x04D30CE0  token=0x6000040  System.Void set_position_Injected(UnityEngine.Vector3& value)
  RVA=0x03FD1C00  token=0x6000041  System.Void get_rotation_Injected(UnityEngine.Quaternion& ret)
  RVA=0x04D30C90  token=0x6000042  System.Void set_rotation_Injected(UnityEngine.Quaternion& value)
  RVA=0x03FD1ED0  token=0x6000043  System.Void Internal_AppendExtOffMeshLink_Injected(UnityEngine.Vector3& startLeft, UnityEngine.Vector3& startRight, UnityEngine.Vector3& endRight, UnityEngine.Vector3& endLeft, System.Boolean bidirectional, System.Int32 area, System.Single costModifier)
END_CLASS

CLASS: UnityEngine.AI.NavMeshDataInstance
TYPE:  sealed struct
TOKEN: 0x200000D
SIZE:  0x14
FIELDS:
  private           System.Int32                    <id>k__BackingField  // 0x10
PROPERTIES:
  valid  get=0x0349D810
  id  get=0x02B77620  set=0x04D86140
  handleId  get=0x04DBA750  set=0x04D86140
  owner  set=0x0B38A0B4
METHODS:
  RVA=0x0349A640  token=0x6000049  System.Void Remove()
END_CLASS

CLASS: UnityEngine.AI.NavMeshLinkData
TYPE:  sealed struct
TOKEN: 0x200000E
SIZE:  0x54
FIELDS:
  private           System.Boolean                  m_IsExt  // 0x10
  private           UnityEngine.Vector3             m_StartLeft  // 0x14
  private           UnityEngine.Vector3             m_StartRight  // 0x20
  private           UnityEngine.Vector3             m_EndRight  // 0x2c
  private           UnityEngine.Vector3             m_EndLeft  // 0x38
  private           System.Single                   m_CostModifier  // 0x44
  private           System.Int32                    m_Bidirectional  // 0x48
  private           System.Int32                    m_Area  // 0x4c
  private           System.Int32                    m_AgentTypeID  // 0x50
PROPERTIES:
  isExt  get=0x04DBA890  set=0x04D87180
  startLeft  get=0x04DBA8A0  set=0x04DABF50
  startRight  get=0x04DBA8C0  set=0x04D8C970
  endRight  get=0x04DBA870  set=0x04D90500
  endLeft  get=0x04DBA850  set=0x04D90C20
  costModifier  get=0x053957B0  set=0x04D8E060
  bidirectional  get=0x0A75F924  set=0x043A5630
  area  get=0x04DBA840  set=0x04D86670
  agentTypeID  get=0x04DBA830  set=0x04D86610
METHODS:
END_CLASS

CLASS: UnityEngine.AI.NavMeshLinkInstance
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x14
FIELDS:
  private           System.Int32                    <id>k__BackingField  // 0x10
PROPERTIES:
  valid  get=0x0B38A1A0
  id  get=0x02B77620  set=0x04D86140
  owner  set=0x0B38A1B4
METHODS:
  RVA=0x0B38A194  token=0x6000060  System.Void Remove()
END_CLASS

CLASS: UnityEngine.AI.NavMesh
TYPE:  static class
TOKEN: 0x2000010
SIZE:  0x10
FIELDS:
  public    static  UnityEngine.AI.NavMesh.OnNavMeshPreUpdateonPreUpdate  // static @ 0x0
  public    static  UnityEngine.AI.NavMesh.OnUnloadFinishDelegateonUnloadFinish  // static @ 0x8
METHODS:
  RVA=0x03F2EF40  token=0x6000062  System.Void Internal_CallOnNavMeshPreUpdate()
  RVA=0x05392CE8  token=0x6000063  System.Boolean Raycast(UnityEngine.Vector3 sourcePosition, UnityEngine.Vector3 targetPosition, UnityEngine.AI.NavMeshHit& hit, System.Int32 areaMask)
  RVA=0x03F7FA20  token=0x6000064  System.Boolean CalculatePath(UnityEngine.Vector3 sourcePosition, UnityEngine.Vector3 targetPosition, System.Int32 areaMask, UnityEngine.AI.NavMeshPath path, System.Boolean isReserveEnd, System.Int32 agentTypeID, System.Single radius)
  RVA=0x053969C4  token=0x6000065  System.Boolean CalculatePathInternal(UnityEngine.Vector3 sourcePosition, UnityEngine.Vector3 targetPosition, System.Int32 areaMask, UnityEngine.AI.NavMeshPath path, System.Boolean isReserveEnd, System.Int32 agentTypeID, System.Single radius)
  RVA=0x0B38A758  token=0x6000066  System.Boolean FindClosestEdge(UnityEngine.Vector3 sourcePosition, UnityEngine.AI.NavMeshHit& hit, System.Int32 areaMask)
  RVA=0x053907C0  token=0x6000067  System.Boolean SamplePosition(UnityEngine.Vector3 sourcePosition, UnityEngine.AI.NavMeshHit& hit, System.Single maxDistance, System.Int32 areaMask)
  RVA=0x047C8BB0  token=0x6000068  System.Int32 GetAreaFromName(System.String areaName)
  RVA=0x0349A470  token=0x6000069  UnityEngine.AI.NavMeshDataInstance AddNavMeshData(UnityEngine.AI.NavMeshData navMeshData)
  RVA=0x0349CF50  token=0x600006A  UnityEngine.AI.NavMeshDataInstance AddNavMeshData(UnityEngine.AI.NavMeshData navMeshData, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
  RVA=0x0B38A9F4  token=0x600006B  System.Void UpdateSurfaceTransform(UnityEngine.AI.NavMeshDataInstance handle, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
  RVA=0x0349C470  token=0x600006C  System.Void RemoveNavMeshData(UnityEngine.AI.NavMeshDataInstance handle)
  RVA=0x0349D830  token=0x600006D  System.Boolean IsValidNavMeshDataHandle(System.Int32 handle)
  RVA=0x0B38A8BC  token=0x600006E  System.Boolean IsValidLinkHandle(System.Int32 handle)
  RVA=0x0B38A7B4  token=0x600006F  System.Boolean InternalSetOwner(System.Int32 dataID, System.Int32 ownerID)
  RVA=0x0B38A774  token=0x6000070  System.Boolean InternalSetLinkOwner(System.Int32 linkID, System.Int32 ownerID)
  RVA=0x0349A500  token=0x6000071  System.Int32 AddNavMeshDataInternal(UnityEngine.AI.NavMeshData navMeshData)
  RVA=0x05392C60  token=0x6000072  System.Int32 AddNavMeshDataTransformedInternal(UnityEngine.AI.NavMeshData navMeshData, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
  RVA=0x0349C380  token=0x6000073  System.Void RemoveNavMeshDataInternal(System.Int32 handle)
  RVA=0x043A5590  token=0x6000074  UnityEngine.AI.NavMeshLinkInstance AddLink(UnityEngine.AI.NavMeshLinkData link, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
  RVA=0x0349C2C0  token=0x6000075  System.Void RemoveLink(UnityEngine.AI.NavMeshLinkInstance handle)
  RVA=0x05397578  token=0x6000076  System.Int32 AddLinkInternal(UnityEngine.AI.NavMeshLinkData link, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
  RVA=0x0349C280  token=0x6000077  System.Void RemoveLinkInternal(System.Int32 handle)
  RVA=0x03BAA6A0  token=0x6000078  UnityEngine.AI.NavMeshBuildSettings GetSettingsByID(System.Int32 agentTypeID)
  RVA=0x04228B50  token=0x6000079  UnityEngine.AI.NavMeshBuildSettings GetSettingsByIndex(System.Int32 index)
  RVA=0x0B38A9E0  token=0x600007A  System.Void UpdateSurfaceTransformInternal(System.Int32 surfaceID, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
  RVA=0x0349D2E0  token=0x600007B  System.Boolean DetachFromSurface(System.Int32 chunkSurfaceId, System.Int32 builderSurfaceId, System.Int32 indexX, System.Int32 indexZ)
  RVA=0x03BAA730  token=0x600007C  System.Boolean SetBuilderSurfaceParams(System.Int32 surfID, System.Single chunkSize, System.Single tileSize, System.Single centerX, System.Single centerZ, System.Single sizeX, System.Single sizeZ)
  RVA=0x0349D270  token=0x600007D  System.Boolean Internal_DetachFromSurface(System.Int32 chunkSurfaceId, System.Int32 builderSurfaceId, System.Int32 indexX, System.Int32 indexZ)
  RVA=0x03BAA900  token=0x600007E  System.Boolean Internal_SetBuilderSurfaceParams(System.Int32 surfID, System.Single chunkSize, System.Single tileSize, System.Single centerX, System.Single centerZ, System.Single sizeX, System.Single sizeZ)
  RVA=0x0B38A8F0  token=0x600007F  System.Int32 LoadDynamicBakeTile(UnityEngine.AI.NavMeshData navMeshData)
  RVA=0x0B38A988  token=0x6000080  System.Void UnloadDynamicBakeTile(System.Int32 dynamicTileID)
  RVA=0x0B38A854  token=0x6000081  System.Int32 Internal_LoadDynamicBakeTile(UnityEngine.AI.NavMeshData navMeshData)
  RVA=0x0B38A888  token=0x6000082  System.Void Internal_UnloadDynamicBakeTile(System.Int32 dynamicTileID)
  RVA=0x0B38A960  token=0x6000083  System.Int32 ModifyVolumeArea(System.Collections.Generic.List<UnityEngine.Vector3> center, System.Collections.Generic.List<UnityEngine.Vector3> ext, System.Collections.Generic.List<UnityEngine.Quaternion> rotation, System.Int32 area)
  RVA=0x0B38A8FC  token=0x6000084  System.Int32 ModifyBoxAreaInternal(System.Collections.Generic.List<UnityEngine.Vector3> center, System.Collections.Generic.List<UnityEngine.Vector3> ext, System.Collections.Generic.List<UnityEngine.Quaternion> rotation, System.Int32 area)
  RVA=0x045F73E0  token=0x6000085  System.Int32 ModifyVolumeArea(System.Collections.Generic.List<UnityEngine.Vector3> verts, System.Single hMin, System.Single hMax, System.Int32 area)
  RVA=0x045F73F0  token=0x6000086  System.Int32 ModifyPolygonAreaInternal(System.Collections.Generic.List<UnityEngine.Vector3> verts, System.Single hMin, System.Single hMax, System.Int32 area)
  RVA=0x05396CDC  token=0x6000087  System.Void LoadExtOffMeshLinks(System.Int32 surfaceID)
  RVA=0x04D51230  token=0x6000088  System.Void Internal_LoadExtOffMeshLinks(System.Int32 surfaceID)
  RVA=0x04025080  token=0x6000089  System.Void BeginLoadingBatch()
  RVA=0x04024F90  token=0x600008A  System.Void EndLoadingBatch()
  RVA=0x04024F50  token=0x600008B  System.Boolean IsLoadingBatch()
  RVA=0x0349D8D0  token=0x600008C  UnityEngine.AsyncOperation AddNavMeshDataAsync(System.Int32& surfaceID, UnityEngine.AI.NavMeshData navMeshData, System.UInt64 surfTileID)
  RVA=0x0349D970  token=0x600008D  UnityEngine.AsyncOperation AddNavMeshDataAsyncInternal(System.Int32& surfaceID, UnityEngine.AI.NavMeshData navMeshData, System.UInt64 surfTileID)
  RVA=0x0B38A6FC  token=0x600008E  System.Void CancelAsyncAdd(System.UInt64 surfTileID)
  RVA=0x0B38A7F4  token=0x600008F  System.Void Internal_CancelAsyncAdd(System.UInt64 surfaceID)
  RVA=0x05392C74  token=0x6000090  System.Void UnloadDataAsync(System.Int32 surfaceID, System.UInt64 surfTileID)
  RVA=0x045B0C50  token=0x6000091  System.Void Internal_UnloadDataAsync(System.Int32 surfaceID, System.UInt64 surfTileID)
  RVA=0x04480240  token=0x6000092  System.Void Internal_InvokeOnUnloadFinish(System.UInt64 surfTileID)
  RVA=0x05392C7C  token=0x6000093  System.Boolean IsSurfaceUnloading(System.UInt64 surfTileID)
  RVA=0x045BBC20  token=0x6000094  System.Boolean Internal_IsSurfaceUnloading(System.UInt64 surfTileID)
  RVA=0x0B38A76C  token=0x6000095  System.Void ForceClearUnloadDataRequest()
  RVA=0x0B38A828  token=0x6000096  System.Void Internal_ForceClearUnloadDataRequest()
  RVA=0x03F7FB00  token=0x6000097  System.Void CalculatePathAsync(UnityEngine.Vector3 sourcePosition, UnityEngine.Vector3 targetPosition, System.Int32 areaMask, UnityEngine.AI.NavMeshPath path, System.Boolean isReserveEnd, System.Int32 agentTypeID, System.Single radius)
  RVA=0x0539698C  token=0x6000098  System.Void CalculatePathAsyncInternal(UnityEngine.Vector3 sourcePosition, UnityEngine.Vector3 targetPosition, System.Int32 areaMask, UnityEngine.AI.NavMeshPath path, System.Boolean isReserveEnd, System.Int32 agentTypeID, System.Single radius)
  RVA=0x05395578  token=0x6000099  System.Void SetFindPathMaxIterPerFrame(System.Int32 maxIterPerFrame)
  RVA=0x04D71780  token=0x600009A  System.Void Internal_SetFindPathMaxIterPerFrame(System.Int32 maxIterPerFrame)
  RVA=0x05391E78  token=0x600009B  System.Void CancelAsyncCalculatePath(UnityEngine.AI.NavMeshPath path)
  RVA=0x04627810  token=0x600009C  System.Void Internal_CancelAsyncCalculatePath(UnityEngine.AI.NavMeshPath path)
  RVA=0x05391E80  token=0x600009D  System.Void RegisterAutoRepath(UnityEngine.AI.NavMeshPath path)
  RVA=0x04970E80  token=0x600009E  System.Void Internal_RegisterAutoRepath(UnityEngine.AI.NavMeshPath path)
  RVA=0x05391E70  token=0x600009F  System.Void UnregisterAutoRepath(UnityEngine.AI.NavMeshPath path)
  RVA=0x04D116D0  token=0x60000A0  System.Void Internal_UnregisterAutoRepath(UnityEngine.AI.NavMeshPath path)
  RVA=0x0B38A970  token=0x60000A1  System.Boolean RaycastNearestPoint(UnityEngine.Vector3 sourcePosition, UnityEngine.Vector3 targetPosition, UnityEngine.AI.NavMeshHit& hit, System.Int32 areaMask)
  RVA=0x041CC6E0  token=0x60000A2  System.Boolean Raycast_Injected(UnityEngine.Vector3& sourcePosition, UnityEngine.Vector3& targetPosition, UnityEngine.AI.NavMeshHit& hit, System.Int32 areaMask)
  RVA=0x03F80440  token=0x60000A3  System.Boolean CalculatePathInternal_Injected(UnityEngine.Vector3& sourcePosition, UnityEngine.Vector3& targetPosition, System.Int32 areaMask, UnityEngine.AI.NavMeshPath path, System.Boolean isReserveEnd, System.Int32 agentTypeID, System.Single radius)
  RVA=0x0B38A704  token=0x60000A4  System.Boolean FindClosestEdge_Injected(UnityEngine.Vector3& sourcePosition, UnityEngine.AI.NavMeshHit& hit, System.Int32 areaMask)
  RVA=0x03AD65B0  token=0x60000A5  System.Boolean SamplePosition_Injected(UnityEngine.Vector3& sourcePosition, UnityEngine.AI.NavMeshHit& hit, System.Single maxDistance, System.Int32 areaMask)
  RVA=0x049BE650  token=0x60000A6  System.Int32 AddNavMeshDataTransformedInternal_Injected(UnityEngine.AI.NavMeshData navMeshData, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation)
  RVA=0x043A5650  token=0x60000A7  System.Int32 AddLinkInternal_Injected(UnityEngine.AI.NavMeshLinkData& link, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation)
  RVA=0x03BAA6E0  token=0x60000A8  System.Void GetSettingsByID_Injected(System.Int32 agentTypeID, UnityEngine.AI.NavMeshBuildSettings& ret)
  RVA=0x04228B90  token=0x60000A9  System.Void GetSettingsByIndex_Injected(System.Int32 index, UnityEngine.AI.NavMeshBuildSettings& ret)
  RVA=0x0B38A990  token=0x60000AA  System.Void UpdateSurfaceTransformInternal_Injected(System.Int32 surfaceID, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation)
  RVA=0x03F804C0  token=0x60000AB  System.Void CalculatePathAsyncInternal_Injected(UnityEngine.Vector3& sourcePosition, UnityEngine.Vector3& targetPosition, System.Int32 areaMask, UnityEngine.AI.NavMeshPath path, System.Boolean isReserveEnd, System.Int32 agentTypeID, System.Single radius)
  RVA=0x03D37610  token=0x60000AC  System.Boolean RaycastNearestPoint_Injected(UnityEngine.Vector3& sourcePosition, UnityEngine.Vector3& targetPosition, UnityEngine.AI.NavMeshHit& hit, System.Int32 areaMask)
END_CLASS

CLASS: UnityEngine.AI.NavMeshBuildSourceShape
TYPE:  sealed struct
TOKEN: 0x2000013
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AI.NavMeshBuildSourceShapeMesh  // const
  public    static  UnityEngine.AI.NavMeshBuildSourceShapeTerrain  // const
  public    static  UnityEngine.AI.NavMeshBuildSourceShapeBox  // const
  public    static  UnityEngine.AI.NavMeshBuildSourceShapeSphere  // const
  public    static  UnityEngine.AI.NavMeshBuildSourceShapeCapsule  // const
  public    static  UnityEngine.AI.NavMeshBuildSourceShapeModifierBox  // const
  public    static  UnityEngine.AI.NavMeshBuildSourceShapePhysicsTriangleMesh  // const
  public    static  UnityEngine.AI.NavMeshBuildSourceShapePhysicsConvexMesh  // const
  public    static  UnityEngine.AI.NavMeshBuildSourceShapePhysicsTerrain  // const
  public    static  UnityEngine.AI.NavMeshBuildSourceShapeModifierPoly  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AI.NavMeshCollectGeometry
TYPE:  sealed struct
TOKEN: 0x2000014
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AI.NavMeshCollectGeometryRenderMeshes  // const
  public    static  UnityEngine.AI.NavMeshCollectGeometryPhysicsColliders  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AI.NavMeshBuildSource
TYPE:  sealed struct
TOKEN: 0x2000015
SIZE:  0x88
FIELDS:
  private           UnityEngine.Matrix4x4           m_Transform  // 0x10
  private           UnityEngine.Vector3             m_Size  // 0x50
  private           UnityEngine.AI.NavMeshBuildSourceShapem_Shape  // 0x5c
  private           System.Int32                    m_Area  // 0x60
  private           System.Int32                    m_InstanceID  // 0x64
  private           System.Int32                    m_ComponentID  // 0x68
  private           System.Boolean                  m_IsConvex  // 0x6c
  private           System.IntPtr                   m_EcsShape  // 0x70
  public            System.Collections.Generic.List<UnityEngine.Vector3>convexVerts  // 0x78
  public            System.Single                   convexHmin  // 0x80
  public            System.Single                   convexHmax  // 0x84
PROPERTIES:
  transform  get=0x04DBA7A0  set=0x04DBA7D0
  size  get=0x04DBA780  set=0x04D89690
  shape  get=0x04DBA770  set=0x04D86630
  area  get=0x04DBA760  set=0x04D865A0
  sourceObject  get=0x0B38989C
  component  get=0x0B389890
METHODS:
  RVA=0x0B389828  token=0x60000BB  UnityEngine.Component InternalGetComponent(System.Int32 instanceID)
  RVA=0x0B38985C  token=0x60000BC  UnityEngine.Object InternalGetObject(System.Int32 instanceID)
END_CLASS

CLASS: UnityEngine.AI.NavMeshBuildMarkup
TYPE:  sealed struct
TOKEN: 0x2000016
SIZE:  0x20
FIELDS:
  private           System.Int32                    m_OverrideArea  // 0x10
  private           System.Int32                    m_Area  // 0x14
  private           System.Int32                    m_IgnoreFromBuild  // 0x18
  private           System.Int32                    m_InstanceID  // 0x1c
PROPERTIES:
  overrideArea  set=0x0B3897A4
  area  set=0x04D88B50
  ignoreFromBuild  set=0x02F129D0
  root  set=0x0B3897AC
METHODS:
END_CLASS

CLASS: UnityEngine.AI.NavMeshBuildSettings
TYPE:  sealed struct
TOKEN: 0x2000017
SIZE:  0x6C
FIELDS:
  private           System.Int32                    m_AgentTypeID  // 0x10
  private           System.Single                   m_AgentRadius  // 0x14
  private           System.Single                   m_AgentHeight  // 0x18
  private           System.Single                   m_AgentSlope  // 0x1c
  private           System.Single                   m_AgentClimb  // 0x20
  private           System.Single                   m_LedgeDropHeight  // 0x24
  private           System.Single                   m_MaxJumpAcrossDistance  // 0x28
  private           System.Single                   m_MinRegionArea  // 0x2c
  private           System.Int32                    m_OverrideVoxelSize  // 0x30
  private           System.Single                   m_VoxelSize  // 0x34
  private           System.Int32                    m_OverrideTileSize  // 0x38
  private           System.Int32                    m_TileSize  // 0x3c
  private           System.Int32                    m_AccuratePlacement  // 0x40
  private           System.UInt32                   m_MaxJobWorkers  // 0x44
  private           System.Int32                    m_PreserveTilesOutsideBounds  // 0x48
  private           System.Int32                    m_PartitioningMethod  // 0x4c
  private           System.Int32                    m_FilterLowHangingObstacles  // 0x50
  private           System.Int32                    m_FilterLedgeSpans  // 0x54
  private           System.Int32                    m_FilterWalkableLowHeightSpans  // 0x58
  private           System.Int32                    m_erodeAgainstWallSpans  // 0x5c
  private           System.Int32                    m_cliffIndent  // 0x60
  private           System.Int32                    m_DynamicNavmeshSurfaceType  // 0x64
  private           UnityEngine.AI.NavMeshBuildDebugSettingsm_Debug  // 0x68
PROPERTIES:
  agentTypeID  get=0x04DBA750  set=0x04D86140
  agentRadius  get=0x04DBA740
  agentHeight  get=0x04DBA730
  agentClimb  get=0x04DBA720
  minRegionArea  set=0x04D86900
  overrideVoxelSize  set=0x03BAA7E0
  voxelSize  set=0x04D89860
  overrideTileSize  set=0x03BAA7C0
  tileSize  set=0x04D86720
  partitioningMethod  set=0x04D86670
  filterLowHangingObstacles  set=0x03BAA740
  filterLedgeSpans  set=0x03BAA760
  filterWalkableLowHeightSpans  set=0x03BAA780
  erodeAgainstWallSpans  set=0x03BAA7A0
  cliffIndent  set=0x04D865A0
METHODS:
END_CLASS

CLASS: UnityEngine.AI.NavMeshBuildDebugSettings
TYPE:  sealed struct
TOKEN: 0x2000018
SIZE:  0x11
FIELDS:
  private           System.Byte                     m_Flags  // 0x10
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.AI.PolygonId
TYPE:  sealed struct
TOKEN: 0x2000019
SIZE:  0x18
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private           System.UInt64                   polyRef  // 0x10
METHODS:
  RVA=0x0314CD60  token=0x60000D1  System.Boolean IsNull()
  RVA=0x02FB1840  token=0x60000D2  System.Boolean op_Equality(UnityEngine.Experimental.AI.PolygonId x, UnityEngine.Experimental.AI.PolygonId y)
  RVA=0x0B38AB58  token=0x60000D3  System.Boolean op_Inequality(UnityEngine.Experimental.AI.PolygonId x, UnityEngine.Experimental.AI.PolygonId y)
  RVA=0x04DBA8E0  token=0x60000D4  System.Int32 GetHashCode()
  RVA=0x0B38AB50  token=0x60000D5  System.Boolean Equals(UnityEngine.Experimental.AI.PolygonId rhs)
  RVA=0x0B38AAE4  token=0x60000D6  System.Boolean Equals(System.Object obj)
END_CLASS

CLASS: UnityEngine.Experimental.AI.NavMeshLocation
TYPE:  sealed struct
TOKEN: 0x200001A
SIZE:  0x28
FIELDS:
  private   readonly UnityEngine.Experimental.AI.PolygonId<polygon>k__BackingField  // 0x10
  private   readonly UnityEngine.Vector3             <position>k__BackingField  // 0x18
PROPERTIES:
  polygon  get=0x02B76770
  position  get=0x04D9C4D0
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.AI.PathQueryStatus
TYPE:  sealed struct
TOKEN: 0x200001B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Experimental.AI.PathQueryStatusFailure  // const
  public    static  UnityEngine.Experimental.AI.PathQueryStatusSuccess  // const
  public    static  UnityEngine.Experimental.AI.PathQueryStatusInProgress  // const
  public    static  UnityEngine.Experimental.AI.PathQueryStatusStatusDetailMask  // const
  public    static  UnityEngine.Experimental.AI.PathQueryStatusWrongMagic  // const
  public    static  UnityEngine.Experimental.AI.PathQueryStatusWrongVersion  // const
  public    static  UnityEngine.Experimental.AI.PathQueryStatusOutOfMemory  // const
  public    static  UnityEngine.Experimental.AI.PathQueryStatusInvalidParam  // const
  public    static  UnityEngine.Experimental.AI.PathQueryStatusBufferTooSmall  // const
  public    static  UnityEngine.Experimental.AI.PathQueryStatusOutOfNodes  // const
  public    static  UnityEngine.Experimental.AI.PathQueryStatusPartialResult  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.AI.NavMeshWorld
TYPE:  sealed struct
TOKEN: 0x200001C
SIZE:  0x18
FIELDS:
  private           System.IntPtr                   world  // 0x10
METHODS:
  RVA=0x03BAA610  token=0x60000D9  UnityEngine.Experimental.AI.NavMeshWorld GetDefaultWorld()
  RVA=0x03BAA660  token=0x60000DA  System.Void GetDefaultWorld_Injected(UnityEngine.Experimental.AI.NavMeshWorld& ret)
END_CLASS

CLASS: UnityEngine.Experimental.AI.NavMeshQuery
TYPE:  sealed struct
TOKEN: 0x200001D
SIZE:  0x18
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.IntPtr                   m_NavMeshQuery  // 0x10
METHODS:
  RVA=0x03BAA860  token=0x60000DB  System.Void .ctor(UnityEngine.Experimental.AI.NavMeshWorld world, Unity.Collections.Allocator allocator, System.Int32 pathNodePoolSize)
  RVA=0x040F38C0  token=0x60000DC  System.Void Dispose()
  RVA=0x05395580  token=0x60000DD  System.IntPtr Create(UnityEngine.Experimental.AI.NavMeshWorld world, System.Int32 nodePoolSize)
  RVA=0x040F3940  token=0x60000DE  System.Void Destroy(System.IntPtr navMeshQuery)
  RVA=0x034ED8A0  token=0x60000DF  UnityEngine.Experimental.AI.NavMeshLocation MapLocation(System.IntPtr navMeshQuery, UnityEngine.Vector3 position, UnityEngine.Vector3 extents, System.Int32 agentTypeID, System.Int32 areaMask)
  RVA=0x034ED7D0  token=0x60000E0  UnityEngine.Experimental.AI.NavMeshLocation MapLocation(UnityEngine.Vector3 position, UnityEngine.Vector3 extents, System.Int32 agentTypeID, System.Int32 areaMask)
  RVA=0x0B38A498  token=0x60000E1  UnityEngine.Experimental.AI.NavMeshLocation MoveLocation(System.IntPtr navMeshQuery, UnityEngine.Experimental.AI.NavMeshLocation location, UnityEngine.Vector3 target, System.Int32 areaMask)
  RVA=0x0B38A4DC  token=0x60000E2  UnityEngine.Experimental.AI.NavMeshLocation MoveLocation(UnityEngine.Experimental.AI.NavMeshLocation location, UnityEngine.Vector3 target, System.Int32 areaMask)
  RVA=0x0B38A5C4  token=0x60000E3  UnityEngine.Experimental.AI.PathQueryStatus Raycast(System.IntPtr navMeshQuery, UnityEngine.Experimental.AI.NavMeshLocation start, UnityEngine.Vector3 targetPosition, System.Int32 areaMask, System.Void* costs, UnityEngine.AI.NavMeshHit& hit, System.Void* path, System.Int32& pathCount, System.Int32 maxPath)
  RVA=0x0B38A61C  token=0x60000E4  UnityEngine.Experimental.AI.PathQueryStatus Raycast(UnityEngine.AI.NavMeshHit& hit, UnityEngine.Experimental.AI.NavMeshLocation start, UnityEngine.Vector3 targetPosition, System.Int32 areaMask, Unity.Collections.NativeArray<System.Single> costs)
  RVA=0x0426FE80  token=0x60000E5  System.IntPtr Create_Injected(UnityEngine.Experimental.AI.NavMeshWorld& world, System.Int32 nodePoolSize)
  RVA=0x034ED930  token=0x60000E6  System.Void MapLocation_Injected(System.IntPtr navMeshQuery, UnityEngine.Vector3& position, UnityEngine.Vector3& extents, System.Int32 agentTypeID, System.Int32 areaMask, UnityEngine.Experimental.AI.NavMeshLocation& ret)
  RVA=0x0B38A434  token=0x60000E7  System.Void MoveLocation_Injected(System.IntPtr navMeshQuery, UnityEngine.Experimental.AI.NavMeshLocation& location, UnityEngine.Vector3& target, System.Int32 areaMask, UnityEngine.Experimental.AI.NavMeshLocation& ret)
  RVA=0x0B38A560  token=0x60000E8  UnityEngine.Experimental.AI.PathQueryStatus Raycast_Injected(System.IntPtr navMeshQuery, UnityEngine.Experimental.AI.NavMeshLocation& start, UnityEngine.Vector3& targetPosition, System.Int32 areaMask, System.Void* costs, UnityEngine.AI.NavMeshHit& hit, System.Void* path, System.Int32& pathCount, System.Int32 maxPath)
END_CLASS

