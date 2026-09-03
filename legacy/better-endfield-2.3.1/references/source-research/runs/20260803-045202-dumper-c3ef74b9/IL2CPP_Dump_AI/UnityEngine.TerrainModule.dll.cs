// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.TerrainModule.dll
// Classes:  21
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

CLASS: HeightmapChangedCallback
TYPE:  sealed class
TOKEN: 0x2000006
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B42C6F0  token=0x6000018  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x0621CA6C  token=0x6000019  System.Void Invoke(UnityEngine.Terrain terrain, UnityEngine.RectInt heightRegion, System.Boolean synched)
END_CLASS

CLASS: TextureChangedCallback
TYPE:  sealed class
TOKEN: 0x2000007
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B4307A0  token=0x600001A  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x0622E128  token=0x600001B  System.Void Invoke(UnityEngine.Terrain terrain, System.String textureName, UnityEngine.RectInt texelRegion, System.Boolean synched)
END_CLASS

CLASS: BoundaryValueType
TYPE:  sealed struct
TOKEN: 0x200000D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.TerrainData.BoundaryValueTypeMaxHeightmapRes  // const
  public    static  UnityEngine.TerrainData.BoundaryValueTypeMinDetailResPerPatch  // const
  public    static  UnityEngine.TerrainData.BoundaryValueTypeMaxDetailResPerPatch  // const
  public    static  UnityEngine.TerrainData.BoundaryValueTypeMaxDetailPatchCount  // const
  public    static  UnityEngine.TerrainData.BoundaryValueTypeMaxDetailsPerRes  // const
  public    static  UnityEngine.TerrainData.BoundaryValueTypeMinAlphamapRes  // const
  public    static  UnityEngine.TerrainData.BoundaryValueTypeMaxAlphamapRes  // const
  public    static  UnityEngine.TerrainData.BoundaryValueTypeMinCliffIndexMapRes  // const
  public    static  UnityEngine.TerrainData.BoundaryValueTypeMaxCliffIndexMapRes  // const
  public    static  UnityEngine.TerrainData.BoundaryValueTypeMinBaseMapRes  // const
  public    static  UnityEngine.TerrainData.BoundaryValueTypeMaxBaseMapRes  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x2000012
SIZE:  0x18
FIELDS:
  public            System.Int32                    groupID  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60000B9  System.Void .ctor()
  RVA=0x0B430A1C  token=0x60000BA  System.Boolean <CreateFromPlacement>b__0(UnityEngine.Terrain x)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x2000014
SIZE:  0x18
FIELDS:
  public            System.Boolean                  onlyAutoConnectedTerrains  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60000BF  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass2_1
TYPE:  sealed class
TOKEN: 0x2000015
SIZE:  0x20
FIELDS:
  public            UnityEngine.Terrain             t  // 0x10
  public            UnityEngine.TerrainUtils.TerrainUtility.<>c__DisplayClass2_0CS$<>8__locals1  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60000C0  System.Void .ctor()
  RVA=0x0B4309A0  token=0x60000C1  System.Boolean <CollectTerrains>b__0(UnityEngine.Terrain x)
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

CLASS: UnityEngine.Terrain
TYPE:  sealed class
TOKEN: 0x2000004
SIZE:  0x18
EXTENDS: UnityEngine.Behaviour
FIELDS:
PROPERTIES:
  terrainData  get=0x0B43053C  set=0x0B43075C
  detailObjectDistance  set=0x0B4305F8
  detailObjectDensity  set=0x0B4305B4
  lightmapIndex  set=0x0B430680
  lightmapScaleOffset  set=0x0B430704
  materialTemplate  get=0x0B430508  set=0x0B430718
  allowAutoConnect  get=0x0B4304A0  set=0x0B430570
  groupingID  get=0x0B4304D4
  drawInstanced  set=0x0B43063C
  activeTerrains  get=0x0B430474
METHODS:
  RVA=0x0B430410  token=0x600000F  System.Void SetNeighbors(UnityEngine.Terrain left, UnityEngine.Terrain top, UnityEngine.Terrain right, UnityEngine.Terrain bottom)
  RVA=0x0B4303D4  token=0x6000010  System.Void Flush()
  RVA=0x0B430408  token=0x6000012  System.Void GetActiveTerrains(System.Collections.Generic.List<UnityEngine.Terrain> terrainList)
  RVA=0x034AD340  token=0x6000013  System.Void Internal_FillActiveTerrainList(System.Object terrainList)
  RVA=0x05393520  token=0x6000014  System.Void .ctor()
  RVA=0x0B4306C0  token=0x6000015  System.Void set_lightmapScaleOffset_Injected(UnityEngine.Vector4& value)
END_CLASS

CLASS: UnityEngine.TerrainCallbacks
TYPE:  static class
TOKEN: 0x2000005
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.TerrainCallbacks.HeightmapChangedCallbackheightmapChanged  // static @ 0x0
  private   static  UnityEngine.TerrainCallbacks.TextureChangedCallbacktextureChanged  // static @ 0x8
METHODS:
  RVA=0x0B42C7C4  token=0x6000016  System.Void InvokeHeightmapChangedCallback(UnityEngine.TerrainData terrainData, UnityEngine.RectInt heightRegion, System.Boolean synched)
  RVA=0x0B42C898  token=0x6000017  System.Void InvokeTextureChangedCallback(UnityEngine.TerrainData terrainData, System.String textureName, UnityEngine.RectInt texelRegion, System.Boolean synched)
END_CLASS

CLASS: UnityEngine.TreePrototype
TYPE:  sealed class
TOKEN: 0x2000008
SIZE:  0x20
FIELDS:
  private           UnityEngine.GameObject          m_Prefab  // 0x10
  private           System.Single                   m_BendFactor  // 0x18
  private           System.Int32                    m_NavMeshLod  // 0x1c
PROPERTIES:
  prefab  get=0x04D9B600  set=0x053908C0
  bendFactor  get=0x04DBAB50  set=0x04D86390
  navMeshLod  get=0x04D9B610
METHODS:
  RVA=0x041E1670  token=0x6000021  System.Void .ctor()
  RVA=0x0B430874  token=0x6000022  System.Boolean Equals(System.Object obj)
  RVA=0x04DBBEA0  token=0x6000023  System.Int32 GetHashCode()
  RVA=0x0B4308D8  token=0x6000024  System.Boolean Equals(UnityEngine.TreePrototype other)
END_CLASS

CLASS: UnityEngine.DetailRenderMode
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.DetailRenderMode    GrassBillboard  // const
  public    static  UnityEngine.DetailRenderMode    VertexLit  // const
  public    static  UnityEngine.DetailRenderMode    Grass  // const
METHODS:
END_CLASS

CLASS: UnityEngine.DetailPrototype
TYPE:  sealed class
TOKEN: 0x200000A
SIZE:  0x68
FIELDS:
  private   static readonly UnityEngine.Color               DefaultHealthColor  // static @ 0x0
  private   static readonly UnityEngine.Color               DefaultDryColor  // static @ 0x10
  private           UnityEngine.GameObject          m_Prototype  // 0x10
  private           UnityEngine.Texture2D           m_PrototypeTexture  // 0x18
  private           UnityEngine.Color               m_HealthyColor  // 0x20
  private           UnityEngine.Color               m_DryColor  // 0x30
  private           System.Single                   m_MinWidth  // 0x40
  private           System.Single                   m_MaxWidth  // 0x44
  private           System.Single                   m_MinHeight  // 0x48
  private           System.Single                   m_MaxHeight  // 0x4c
  private           System.Int32                    m_NoiseSeed  // 0x50
  private           System.Single                   m_NoiseSpread  // 0x54
  private           System.Single                   m_HoleEdgePadding  // 0x58
  private           System.Int32                    m_RenderMode  // 0x5c
  private           System.Int32                    m_UsePrototypeMesh  // 0x60
  private           System.Int32                    m_UseInstancing  // 0x64
PROPERTIES:
  prototype  get=0x04D9B600  set=0x053908C0
  prototypeTexture  get=0x04D96330  set=0x05392C40
  minWidth  get=0x05391640  set=0x04D86470
  maxWidth  get=0x05391650  set=0x04D86480
  minHeight  get=0x05391660  set=0x04D8D380
  maxHeight  get=0x04DBD330  set=0x04D91480
  noiseSpread  get=0x04DBD340  set=0x04D86E10
  healthyColor  get=0x04DBD320  set=0x04D8C210
  dryColor  get=0x04DBCB30  set=0x04D87490
  renderMode  get=0x04DBD350  set=0x04D865C0
  usePrototypeMesh  get=0x0B42C6B0  set=0x0B42C6D4
  useInstancing  set=0x0B42C6B8
METHODS:
  RVA=0x0B42C4B8  token=0x600003C  System.Void .ctor()
  RVA=0x0B42C570  token=0x600003D  System.Void .ctor(UnityEngine.DetailPrototype other)
  RVA=0x0B42C250  token=0x600003E  System.Boolean Equals(System.Object obj)
  RVA=0x04DBBEA0  token=0x600003F  System.Int32 GetHashCode()
  RVA=0x0B42C2B4  token=0x6000040  System.Boolean Equals(UnityEngine.DetailPrototype other)
  RVA=0x0B42C460  token=0x6000041  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.TreeInstance
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x38
FIELDS:
  public            UnityEngine.Vector3             position  // 0x10
  public            System.Single                   widthScale  // 0x1c
  public            System.Single                   heightScale  // 0x20
  public            System.Single                   rotation  // 0x24
  public            UnityEngine.Color32             color  // 0x28
  public            UnityEngine.Color32             lightmapColor  // 0x2c
  public            System.Int32                    prototypeIndex  // 0x30
  private           System.Single                   temporaryDistance  // 0x34
METHODS:
END_CLASS

CLASS: UnityEngine.TerrainData
TYPE:  sealed class
TOKEN: 0x200000C
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
  private   static  System.String                   k_ScriptingInterfaceName  // const
  private   static  System.String                   k_ScriptingInterfacePrefix  // const
  private   static  System.String                   k_HeightmapPrefix  // const
  private   static  System.String                   k_DetailDatabasePrefix  // const
  private   static  System.String                   k_TreeDatabasePrefix  // const
  private   static  System.String                   k_SplatDatabasePrefix  // const
  private   static readonly System.Int32                    k_MaximumResolution  // static @ 0x0
  private   static readonly System.Int32                    k_MinimumDetailResolutionPerPatch  // static @ 0x4
  private   static readonly System.Int32                    k_MaximumDetailResolutionPerPatch  // static @ 0x8
  private   static readonly System.Int32                    k_MaximumDetailPatchCount  // static @ 0xc
  private   static readonly System.Int32                    k_MaximumDetailsPerRes  // static @ 0x10
  private   static readonly System.Int32                    k_MinimumAlphamapResolution  // static @ 0x14
  private   static readonly System.Int32                    k_MaximumAlphamapResolution  // static @ 0x18
  private   static readonly System.Int32                    k_MinimumCliffIndexMapResolution  // static @ 0x1c
  private   static readonly System.Int32                    k_MaximumCliffIndexMapResolution  // static @ 0x20
  private   static readonly System.Int32                    k_MinimumBaseMapResolution  // static @ 0x24
  private   static readonly System.Int32                    k_MaximumBaseMapResolution  // static @ 0x28
PROPERTIES:
  heightmapTexture  get=0x0B42DDFC
  heightmapResolution  get=0x0B42DDF0  set=0x0B42E278
  internalHeightmapResolution  get=0x0B42DE74  set=0x0B42E3A0
  holesTexture  get=0x0B42DE44
  enableHolesTextureCompression  get=0x0B42DDBC  set=0x0B42E234
  holesResolution  get=0x0B42DE30
  size  get=0x0B42DEEC  set=0x0B42E424
  detailWidth  get=0x0B42DD88
  detailHeight  get=0x0B42DCB8
  detailResolution  get=0x0B42DD54
  detailResolutionPerPatch  get=0x0B42DD20
  detailPrototypes  get=0x0B42DCEC  set=0x0B42E1F0
  treeInstances  get=0x0B42DF4C
  treePrototypes  get=0x0B42DF58  set=0x0B42E47C
  alphamapLayers  get=0x0B42DBB8
  alphamapResolution  get=0x0B42DBAC  set=0x0B42E000
  Internal_alphamapResolution  get=0x0B42DB44  set=0x0B42DFC0
  alphamapWidth  get=0x0B42DBAC
  alphamapHeight  get=0x0B42DBAC
  baseMapResolution  get=0x0B42DCAC
  Internal_baseMapResolution  get=0x0B42DB78
  alphamapTextureCount  get=0x0B42DBEC
  alphamapTextures  get=0x0B42DC20
  terrainLayers  get=0x0B42DF18  set=0x0B42E438
  users  get=0x0B42DF8C
METHODS:
  RVA=0x0B42CA7C  token=0x6000042  System.Int32 GetBoundaryValue(UnityEngine.TerrainData.BoundaryValueType type)
  RVA=0x0B42DAEC  token=0x6000043  System.Void .ctor()
  RVA=0x0B42CE90  token=0x6000044  System.Void Internal_Create(UnityEngine.TerrainData terrainData)
  RVA=0x0B42D188  token=0x600004D  System.Boolean IsHolesTextureCompressed()
  RVA=0x0B42CC2C  token=0x600004E  UnityEngine.RenderTexture GetHolesTexture()
  RVA=0x0B42CAB0  token=0x600004F  UnityEngine.Texture2D GetCompressedHolesTexture()
  RVA=0x0B42CD48  token=0x6000053  System.Single GetInterpolatedHeight(System.Single x, System.Single y)
  RVA=0x0B42CB44  token=0x6000054  System.Single[,] GetHeights(System.Int32 xBase, System.Int32 yBase, System.Int32 width, System.Int32 height)
  RVA=0x0B42CF24  token=0x6000055  System.Single[,] Internal_GetHeights(System.Int32 xBase, System.Int32 yBase, System.Int32 width, System.Int32 height)
  RVA=0x0B42D6BC  token=0x6000056  System.Void SetHeights(System.Int32 xBase, System.Int32 yBase, System.Single[,] heights)
  RVA=0x0B42D128  token=0x6000057  System.Void Internal_SetHeights(System.Int32 xBase, System.Int32 yBase, System.Int32 width, System.Int32 height, System.Single[,] heights)
  RVA=0x0B42CC60  token=0x6000058  System.Boolean[,] GetHoles(System.Int32 xBase, System.Int32 yBase, System.Int32 width, System.Int32 height)
  RVA=0x0B42CF84  token=0x6000059  System.Boolean[,] Internal_GetHoles(System.Int32 xBase, System.Int32 yBase, System.Int32 width, System.Int32 height)
  RVA=0x0B42CE00  token=0x600005A  UnityEngine.Vector3 GetInterpolatedNormal(System.Single x, System.Single y)
  RVA=0x0B42D3D8  token=0x600005D  System.Void SetDetailResolution(System.Int32 detailResolution, System.Int32 resolutionPerPatch)
  RVA=0x0B42D0D8  token=0x600005E  System.Void Internal_SetDetailResolution(System.Int32 patchCount, System.Int32 resolutionPerPatch)
  RVA=0x0B42D1BC  token=0x6000061  System.Void RefreshPrototypes()
  RVA=0x0B42CE30  token=0x6000064  System.Int32[] GetSupportedLayers(System.Int32 xBase, System.Int32 yBase, System.Int32 totalWidth, System.Int32 totalHeight)
  RVA=0x0B42CAE4  token=0x6000065  System.Int32[,] GetDetailLayer(System.Int32 xBase, System.Int32 yBase, System.Int32 width, System.Int32 height, System.Int32 layer)
  RVA=0x0B42D348  token=0x6000066  System.Void SetDetailLayer(System.Int32 xBase, System.Int32 yBase, System.Int32 layer, System.Int32[,] details)
  RVA=0x0B42D078  token=0x6000067  System.Void Internal_SetDetailLayer(System.Int32 xBase, System.Int32 yBase, System.Int32 totalWidth, System.Int32 totalHeight, System.Int32 detailIndex, System.Int32[,] data)
  RVA=0x0B42CFE4  token=0x6000069  UnityEngine.TreeInstance[] Internal_GetTreeInstances()
  RVA=0x0B42D948  token=0x600006A  System.Void SetTreeInstances(UnityEngine.TreeInstance[] instances, System.Boolean snapToHeightmap)
  RVA=0x0B42C9F4  token=0x600006E  System.Single[,,] GetAlphamaps(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)
  RVA=0x0B42CEC4  token=0x600006F  System.Single[,,] Internal_GetAlphamaps(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height)
  RVA=0x0B42C980  token=0x6000072  System.Single GetAlphamapResolutionInternal()
  RVA=0x0B42D1F0  token=0x6000079  System.Void SetAlphamaps(System.Int32 x, System.Int32 y, System.Single[,,] map)
  RVA=0x0B42D018  token=0x600007A  System.Void Internal_SetAlphamaps(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height, System.Single[,,] map)
  RVA=0x0B42C9B4  token=0x600007B  UnityEngine.Texture2D GetAlphamapTexture(System.Int32 index)
  RVA=0x0B42D99C  token=0x6000081  System.Void .cctor()
  RVA=0x0B42DEA8  token=0x6000082  System.Void get_size_Injected(UnityEngine.Vector3& ret)
  RVA=0x0B42E3E0  token=0x6000083  System.Void set_size_Injected(UnityEngine.Vector3& value)
  RVA=0x0B42CD9C  token=0x6000084  System.Void GetInterpolatedNormal_Injected(System.Single x, System.Single y, UnityEngine.Vector3& ret)
END_CLASS

CLASS: UnityEngine.TerrainLayer
TYPE:  sealed class
TOKEN: 0x200000E
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
PROPERTIES:
  diffuseTexture  get=0x0B42E620  set=0x0B42EA28
  normalMapTexture  get=0x0B42E79C  set=0x0B42EBA4
  maskMapTexture  get=0x0B42E734  set=0x0B42EB1C
  tileSize  get=0x0B42E954  set=0x0B42ED6C
  tileOffset  get=0x0B42E8EC  set=0x0B42ED0C
  specular  get=0x0B42E87C  set=0x0B42ECB4
  metallic  get=0x0B42E768  set=0x0B42EB60
  smoothness  get=0x0B42E804  set=0x0B42EC2C
  normalScale  get=0x0B42E7D0  set=0x0B42EBE8
  diffuseRemapMin  get=0x0B42E5F4  set=0x0B42EA14
  diffuseRemapMax  get=0x0B42E584  set=0x0B42E9BC
  maskMapRemapMin  get=0x0B42E708  set=0x0B42EB08
  maskMapRemapMax  get=0x0B42E698  set=0x0B42EAB0
METHODS:
  RVA=0x0B42E4F4  token=0x6000085  System.Void .ctor()
  RVA=0x0B42E4C0  token=0x6000086  System.Void Internal_Create(UnityEngine.TerrainLayer layer)
  RVA=0x0B42E910  token=0x60000A1  System.Void get_tileSize_Injected(UnityEngine.Vector2& ret)
  RVA=0x0B42ED28  token=0x60000A2  System.Void set_tileSize_Injected(UnityEngine.Vector2& value)
  RVA=0x0B42E8A8  token=0x60000A3  System.Void get_tileOffset_Injected(UnityEngine.Vector2& ret)
  RVA=0x0B42ECC8  token=0x60000A4  System.Void set_tileOffset_Injected(UnityEngine.Vector2& value)
  RVA=0x0B42E838  token=0x60000A5  System.Void get_specular_Injected(UnityEngine.Color& ret)
  RVA=0x0B42EC70  token=0x60000A6  System.Void set_specular_Injected(UnityEngine.Color& value)
  RVA=0x0B42E5B0  token=0x60000A7  System.Void get_diffuseRemapMin_Injected(UnityEngine.Vector4& ret)
  RVA=0x0B42E9D0  token=0x60000A8  System.Void set_diffuseRemapMin_Injected(UnityEngine.Vector4& value)
  RVA=0x0B42E540  token=0x60000A9  System.Void get_diffuseRemapMax_Injected(UnityEngine.Vector4& ret)
  RVA=0x0B42E978  token=0x60000AA  System.Void set_diffuseRemapMax_Injected(UnityEngine.Vector4& value)
  RVA=0x0B42E6C4  token=0x60000AB  System.Void get_maskMapRemapMin_Injected(UnityEngine.Vector4& ret)
  RVA=0x0B42EAC4  token=0x60000AC  System.Void set_maskMapRemapMin_Injected(UnityEngine.Vector4& value)
  RVA=0x0B42E654  token=0x60000AD  System.Void get_maskMapRemapMax_Injected(UnityEngine.Vector4& ret)
  RVA=0x0B42EA6C  token=0x60000AE  System.Void set_maskMapRemapMax_Injected(UnityEngine.Vector4& value)
END_CLASS

CLASS: UnityEngine.TerrainUtils.TerrainMapStatusCode
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.TerrainUtils.TerrainMapStatusCodeOK  // const
  public    static  UnityEngine.TerrainUtils.TerrainMapStatusCodeOverlapping  // const
  public    static  UnityEngine.TerrainUtils.TerrainMapStatusCodeSizeMismatch  // const
  public    static  UnityEngine.TerrainUtils.TerrainMapStatusCodeEdgeAlignmentMismatch  // const
METHODS:
END_CLASS

CLASS: UnityEngine.TerrainUtils.TerrainTileCoord
TYPE:  sealed struct
TOKEN: 0x2000010
SIZE:  0x18
FIELDS:
  public    readonly System.Int32                    tileX  // 0x10
  public    readonly System.Int32                    tileZ  // 0x14
METHODS:
  RVA=0x04D85C40  token=0x60000AF  System.Void .ctor(System.Int32 tileX, System.Int32 tileZ)
END_CLASS

CLASS: UnityEngine.TerrainUtils.TerrainMap
TYPE:  class
TOKEN: 0x2000011
SIZE:  0x28
FIELDS:
  private           UnityEngine.Vector3             m_patchSize  // 0x10
  private           UnityEngine.TerrainUtils.TerrainMapStatusCodem_errorCode  // 0x1c
  private           System.Collections.Generic.Dictionary<UnityEngine.TerrainUtils.TerrainTileCoord,UnityEngine.Terrain>m_terrainTiles  // 0x20
PROPERTIES:
  terrainTiles  get=0x04D862C0
METHODS:
  RVA=0x0B42F34C  token=0x60000B0  UnityEngine.Terrain GetTerrain(System.Int32 tileX, System.Int32 tileZ)
  RVA=0x0B42F100  token=0x60000B1  UnityEngine.TerrainUtils.TerrainMap CreateFromPlacement(UnityEngine.Terrain originTerrain, System.Predicate<UnityEngine.Terrain> filter, System.Boolean fullValidation)
  RVA=0x0B42EEE4  token=0x60000B2  UnityEngine.TerrainUtils.TerrainMap CreateFromPlacement(UnityEngine.Vector2 gridOrigin, UnityEngine.Vector2 gridSize, System.Predicate<UnityEngine.Terrain> filter, System.Boolean fullValidation)
  RVA=0x0B42FD08  token=0x60000B4  System.Void .ctor()
  RVA=0x0B42ED88  token=0x60000B5  System.Void AddTerrainInternal(System.Int32 x, System.Int32 z, UnityEngine.Terrain terrain)
  RVA=0x0B42F3C8  token=0x60000B6  System.Boolean TryToAddTerrain(System.Int32 tileX, System.Int32 tileZ, UnityEngine.Terrain terrain)
  RVA=0x0B42F4B0  token=0x60000B7  System.Void ValidateTerrain(System.Int32 tileX, System.Int32 tileZ)
  RVA=0x0B42FBB4  token=0x60000B8  UnityEngine.TerrainUtils.TerrainMapStatusCode Validate()
END_CLASS

CLASS: UnityEngine.TerrainUtils.TerrainUtility
TYPE:  static class
TOKEN: 0x2000013
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B4303A0  token=0x60000BB  System.Boolean ValidTerrainsExist()
  RVA=0x0B4300C8  token=0x60000BC  System.Void ClearConnectivity()
  RVA=0x0B430148  token=0x60000BD  System.Collections.Generic.Dictionary<System.Int32,UnityEngine.TerrainUtils.TerrainMap> CollectTerrains(System.Boolean onlyAutoConnectedTerrains)
  RVA=0x0B42FD84  token=0x60000BE  System.Void AutoConnect()
END_CLASS

