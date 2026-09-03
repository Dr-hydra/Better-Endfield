// ========================================================
// Dumped by @desirepro
// Assembly: NavMeshComponents.dll
// Classes:  9
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

CLASS: NavMeshPrefabInstance
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.AI.NavMeshData      m_NavMesh  // 0x18
  private           System.Boolean                  m_FollowTransform  // 0x20
  private           UnityEngine.AI.NavMeshDataInstancem_Instance  // 0x24
  private   static readonly System.Collections.Generic.List<NavMeshPrefabInstance>s_TrackedInstances  // static @ 0x0
  private           UnityEngine.Vector3             m_Position  // 0x28
  private           UnityEngine.Quaternion          m_Rotation  // 0x34
PROPERTIES:
  navMeshData  get=0x0385B100  set=0x05392C40
  followTransform  get=0x04D866B0  set=0x0A8904B4
  trackedInstances  get=0x0A890464
METHODS:
  RVA=0x0A890080  token=0x6000006  System.Void OnEnable()
  RVA=0x0A89005C  token=0x6000007  System.Void OnDisable()
  RVA=0x0A890264  token=0x6000008  System.Void UpdateInstance()
  RVA=0x0A88FC94  token=0x6000009  System.Void AddInstance()
  RVA=0x0A88FDD4  token=0x600000A  System.Void AddTracking()
  RVA=0x0A8900BC  token=0x600000B  System.Void RemoveTracking()
  RVA=0x0A890238  token=0x600000C  System.Void SetFollowTransform(System.Boolean value)
  RVA=0x0A88FF68  token=0x600000D  System.Boolean HasMoved()
  RVA=0x0A890288  token=0x600000E  System.Void UpdateTrackedInstances()
  RVA=0x05393520  token=0x600000F  System.Void .ctor()
  RVA=0x0A8903D8  token=0x6000010  System.Void .cctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000009
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.AI.NavMeshSurface.<>c<>9  // static @ 0x0
  public    static  System.Predicate<UnityEngine.AI.NavMeshModifierVolume><>9__105_0  // static @ 0x8
  public    static  System.Predicate<UnityEngine.AI.NavMeshModifier><>9__106_0  // static @ 0x10
  public    static  System.Predicate<UnityEngine.AI.NavMeshBuildSource><>9__106_1  // static @ 0x18
  public    static  System.Predicate<UnityEngine.AI.NavMeshBuildSource><>9__106_2  // static @ 0x20
METHODS:
  RVA=0x0A893158  token=0x600008D  System.Void .cctor()
  RVA=0x041E1670  token=0x600008E  System.Void .ctor()
  RVA=0x0A892FBC  token=0x600008F  System.Boolean <AppendModifierVolumes>b__105_0(UnityEngine.AI.NavMeshModifierVolume x)
  RVA=0x0A892FBC  token=0x6000090  System.Boolean <CollectSources>b__106_0(UnityEngine.AI.NavMeshModifier x)
  RVA=0x0A892FE0  token=0x6000091  System.Boolean <CollectSources>b__106_1(UnityEngine.AI.NavMeshBuildSource x)
  RVA=0x0A89309C  token=0x6000092  System.Boolean <CollectSources>b__106_2(UnityEngine.AI.NavMeshBuildSource x)
END_CLASS

CLASS: UnityEngine.AI.NavMeshLink
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x90
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Int32                    m_AgentTypeID  // 0x18
  private           UnityEngine.Vector3             m_StartRight  // 0x1c
  private           UnityEngine.Vector3             m_StartLeft  // 0x28
  private           UnityEngine.Vector3             m_EndRight  // 0x34
  private           UnityEngine.Vector3             m_EndLeft  // 0x40
  private           System.Boolean                  m_IsExt  // 0x4c
  private           System.Single                   m_CostModifier  // 0x50
  private           System.Boolean                  m_Bidirectional  // 0x54
  private           System.Boolean                  m_AutoUpdatePosition  // 0x55
  private           System.Int32                    m_Area  // 0x58
  public            System.Boolean                  autoSnapExtent  // 0x5c
  public            UnityEngine.Vector3             snapExtent  // 0x60
  private           UnityEngine.AI.NavMeshLinkInstancem_LinkInstance  // 0x6c
  private           UnityEngine.Vector3             m_LastPosition  // 0x70
  private           UnityEngine.Quaternion          m_LastRotation  // 0x7c
  private   static readonly System.Collections.Generic.List<UnityEngine.AI.NavMeshLink>s_Tracked  // static @ 0x0
PROPERTIES:
  agentTypeID  get=0x011EF5B0  set=0x0A88F358
  startRight  get=0x04D90520  set=0x0A88F528
  startLeft  get=0x04D90C00  set=0x0A88F4B4
  startMid  get=0x0A88F2D8
  endRight  get=0x04D89640  set=0x0A88F430
  endLeft  get=0x04D89660  set=0x0A88F3BC
  endMid  get=0x0A88F258
  isExt  get=0x04D86910  set=0x0A88F4A4
  costModifier  get=0x04D885A0  set=0x0A88F390
  bidirectional  get=0x04D86C20  set=0x0A88F380
  autoUpdate  get=0x04D86C10  set=0x0A88F378
  area  get=0x04D86570  set=0x0A88F368
METHODS:
  RVA=0x0A88ECF0  token=0x6000027  System.Void OnEnable()
  RVA=0x0A88ECA4  token=0x6000028  System.Void OnDisable()
  RVA=0x0A88EF94  token=0x6000029  System.Void UpdateLink()
  RVA=0x0A88EA14  token=0x600002A  System.Void AddTracking(UnityEngine.AI.NavMeshLink link)
  RVA=0x0A88ED50  token=0x600002B  System.Void RemoveTracking(UnityEngine.AI.NavMeshLink link)
  RVA=0x0A88EECC  token=0x600002C  System.Void SetAutoUpdate(System.Boolean value)
  RVA=0x0A88E850  token=0x600002D  System.Void AddLink()
  RVA=0x0A88EF30  token=0x600002E  System.Void SetByLinkData(UnityEngine.AI.NavMeshLinkData data)
  RVA=0x0A88EBA8  token=0x600002F  System.Boolean HasTransformChanged()
  RVA=0x0A88EC9C  token=0x6000030  System.Void OnDidApplyAnimationProperties()
  RVA=0x0A88EFB8  token=0x6000031  System.Void UpdateTrackedInstances()
  RVA=0x0A88F194  token=0x6000032  System.Void .ctor()
  RVA=0x0A88F108  token=0x6000033  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.AI.NavMeshModifier
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Boolean                  m_OverrideArea  // 0x18
  private           System.Int32                    m_Area  // 0x1c
  private           System.Boolean                  m_IgnoreFromBuild  // 0x20
  private           System.Collections.Generic.List<System.Int32>m_AffectedAgents  // 0x28
  private   static readonly System.Collections.Generic.List<UnityEngine.AI.NavMeshModifier>s_NavMeshModifiers  // static @ 0x0
PROPERTIES:
  overrideArea  get=0x04D86340  set=0x04D86350
  area  get=0x04D86310  set=0x04D86320
  ignoreFromBuild  get=0x04D866B0  set=0x04D866D0
  activeModifiers  get=0x0A88FC44
METHODS:
  RVA=0x0A88FA44  token=0x600003B  System.Void OnEnable()
  RVA=0x0A88F9D4  token=0x600003C  System.Void OnDisable()
  RVA=0x0A88F934  token=0x600003D  System.Boolean AffectsAgentType(System.Int32 agentTypeID)
  RVA=0x0A88FB80  token=0x600003E  System.Void .ctor()
  RVA=0x0A88FAF4  token=0x600003F  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.AI.NavMeshModifierVolume
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.Vector3             m_Size  // 0x18
  private           UnityEngine.Vector3             m_Center  // 0x24
  private           System.Int32                    m_Area  // 0x30
  private           System.Collections.Generic.List<System.Int32>m_AffectedAgents  // 0x38
  private   static readonly System.Collections.Generic.List<UnityEngine.AI.NavMeshModifierVolume>s_NavMeshModifiers  // static @ 0x0
PROPERTIES:
  size  get=0x04D89870  set=0x04D89890
  center  get=0x04D905E0  set=0x04D905D0
  area  get=0x04D864F0  set=0x04D86740
  activeModifiers  get=0x0A88F8E4
METHODS:
  RVA=0x0A88F6AC  token=0x6000047  System.Void OnEnable()
  RVA=0x0A88F63C  token=0x6000048  System.Void OnDisable()
  RVA=0x0A88F59C  token=0x6000049  System.Boolean AffectsAgentType(System.Int32 agentTypeID)
  RVA=0x0A88F7E8  token=0x600004A  System.Void .ctor()
  RVA=0x0A88F75C  token=0x600004B  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.AI.CollectObjects
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AI.CollectObjects   All  // const
  public    static  UnityEngine.AI.CollectObjects   Volume  // const
  public    static  UnityEngine.AI.CollectObjects   Children  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AI.PartitioningMethod
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AI.PartitioningMethodLayers  // const
  public    static  UnityEngine.AI.PartitioningMethodMonotone  // const
  public    static  UnityEngine.AI.PartitioningMethodWatershed  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AI.NavMeshSurface
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x98
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Int32                    m_AgentTypeID  // 0x18
  private           UnityEngine.AI.CollectObjects   m_CollectObjects  // 0x1c
  private           UnityEngine.Vector3             m_Size  // 0x20
  private           UnityEngine.Vector3             m_Center  // 0x2c
  private           UnityEngine.LayerMask           m_LayerMask  // 0x38
  private           System.String                   m_BlockTag  // 0x40
  private           UnityEngine.AI.PartitioningMethodm_PartitioningMethod  // 0x48
  private           System.Boolean                  m_FilterLowHangingObstacles  // 0x4c
  private           System.Boolean                  m_FilterLedgeSpans  // 0x4d
  private           System.Boolean                  m_FilterWalkableLowHeightSpans  // 0x4e
  private           System.Boolean                  m_OverrideMinRegionArea  // 0x4f
  private           System.Single                   m_MinRegionArea  // 0x50
  private           UnityEngine.AI.NavMeshCollectGeometrym_UseGeometry  // 0x54
  private           System.Int32                    m_DefaultArea  // 0x58
  private           System.Boolean                  m_IgnoreNavMeshAgent  // 0x5c
  private           System.Boolean                  m_IgnoreNavMeshObstacle  // 0x5d
  private           System.Boolean                  m_OverrideTileSize  // 0x5e
  private           System.Int32                    m_TileSize  // 0x60
  private           System.Boolean                  m_OverrideVoxelSize  // 0x64
  private           System.Single                   m_VoxelSize  // 0x68
  private           System.Boolean                  m_BuildHeightMesh  // 0x6c
  private           UnityEngine.AI.NavMeshData      m_NavMeshData  // 0x70
  private           UnityEngine.AI.NavMeshDataInstancem_NavMeshDataInstance  // 0x78
  private           UnityEngine.Vector3             m_LastPosition  // 0x7c
  private           UnityEngine.Quaternion          m_LastRotation  // 0x88
  private   static readonly System.Collections.Generic.List<UnityEngine.AI.NavMeshSurface>s_NavMeshSurfaces  // static @ 0x0
PROPERTIES:
  agentTypeID  get=0x011EF5B0  set=0x011EF9B0
  collectObjects  get=0x04D86310  set=0x04D86320
  size  get=0x04D90830  set=0x04D90820
  center  get=0x04D90540  set=0x04D90510
  layerMask  get=0x04D863B0  set=0x04D86430
  BlockTag  get=0x04D85A60  set=0x042B4AE0
  partitioningMethod  get=0x04D863D0  set=0x04D86450
  filterLowHangingObstacles  get=0x04D86910  set=0x04D86920
  filterLedgeSpans  get=0x04D926B0  set=0x04D926C0
  filterWalkableLowHeightSpans  get=0x04DA5B30  set=0x04DA5B70
  overrideMinRegionArea  get=0x04DA5B50  set=0x04DA5B90
  minRegionArea  get=0x04D885A0  set=0x04D88FF0
  useGeometry  get=0x04D86540  set=0x04D86590
  defaultArea  get=0x04D86570  set=0x04D865D0
  ignoreNavMeshAgent  get=0x04D86D90  set=0x04D86DB0
  ignoreNavMeshObstacle  get=0x04DA5B40  set=0x04DA5B80
  overrideTileSize  get=0x04DA5B60  set=0x04DA5BA0
  tileSize  get=0x04D86790  set=0x04D86D50
  overrideVoxelSize  get=0x04D8F050  set=0x04D8F080
  voxelSize  get=0x04D8F060  set=0x04D8F090
  buildHeightMesh  get=0x04D8F040  set=0x04D87F30
  navMeshData  get=0x04D862B0  set=0x063AD760
  activeSurfaces  get=0x0A892F6C
METHODS:
  RVA=0x0A89261C  token=0x6000079  System.Void OnEnable()
  RVA=0x0A8925D0  token=0x600007A  System.Void OnDisable()
  RVA=0x0A8904BC  token=0x600007B  System.Void AddData()
  RVA=0x0A89283C  token=0x600007C  System.Void RemoveData()
  RVA=0x0A892098  token=0x600007D  UnityEngine.AI.NavMeshBuildSettings GetBuildSettings()
  RVA=0x0A890BE0  token=0x600007E  System.Void BuildNavMesh()
  RVA=0x0A892C68  token=0x600007F  UnityEngine.AsyncOperation UpdateNavMesh(UnityEngine.AI.NavMeshData data)
  RVA=0x0A892AB8  token=0x6000080  System.Boolean UpdateNavMeshSync(UnityEngine.AI.NavMeshData data)
  RVA=0x0A892668  token=0x6000081  System.Void Register(UnityEngine.AI.NavMeshSurface surface)
  RVA=0x0A89285C  token=0x6000082  System.Void Unregister(UnityEngine.AI.NavMeshSurface surface)
  RVA=0x0A8929D8  token=0x6000083  System.Void UpdateActive()
  RVA=0x0A89062C  token=0x6000084  System.Void AppendModifierVolumes(System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildSource>& sources)
  RVA=0x0A8917A8  token=0x6000085  System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildSource> CollectSources()
  RVA=0x04D92AA0  token=0x6000086  UnityEngine.Vector3 Abs(UnityEngine.Vector3 v)
  RVA=0x0A8921F8  token=0x6000087  UnityEngine.Bounds GetWorldBounds(UnityEngine.Matrix4x4 mat, UnityEngine.Bounds bounds)
  RVA=0x0A890ED8  token=0x6000088  UnityEngine.Bounds CalculateWorldBounds(System.Collections.Generic.List<UnityEngine.AI.NavMeshBuildSource> sources)
  RVA=0x0A8924D4  token=0x6000089  System.Boolean HasTransformChanged()
  RVA=0x0A892A84  token=0x600008A  System.Void UpdateDataIfTransformChanged()
  RVA=0x0A892EA4  token=0x600008B  System.Void .ctor()
  RVA=0x0A892E18  token=0x600008C  System.Void .cctor()
END_CLASS

