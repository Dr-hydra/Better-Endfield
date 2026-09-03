// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.TilemapModule.dll
// Classes:  12
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

CLASS: ColliderType
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Tilemaps.Tile.ColliderTypeNone  // const
  public    static  UnityEngine.Tilemaps.Tile.ColliderTypeSprite  // const
  public    static  UnityEngine.Tilemaps.Tile.ColliderTypeGrid  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Tilemaps.ITilemap
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x30
FIELDS:
  private   static  UnityEngine.Tilemaps.ITilemap   s_Instance  // static @ 0x0
  private           UnityEngine.Tilemaps.Tilemap    m_Tilemap  // 0x10
  private           System.Boolean                  m_AddToList  // 0x18
  private           System.Int32                    m_RefreshCount  // 0x1c
  private           Unity.Collections.NativeArray<UnityEngine.Vector3Int>m_RefreshPos  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
  RVA=0x0B45EF30  token=0x6000002  System.Void RefreshTile(UnityEngine.Vector3Int position)
  RVA=0x0B45EA08  token=0x6000003  UnityEngine.Tilemaps.ITilemap CreateInstance()
  RVA=0x0B45EA7C  token=0x6000004  System.Void FindAllRefreshPositions(UnityEngine.Tilemaps.ITilemap tilemap, System.Int32 count, System.IntPtr oldTilesIntPtr, System.IntPtr newTilesIntPtr, System.IntPtr positionsIntPtr)
  RVA=0x0B45ED18  token=0x6000005  System.Void GetAllTileData(UnityEngine.Tilemaps.ITilemap tilemap, System.Int32 count, System.IntPtr tilesIntPtr, System.IntPtr positionsIntPtr, System.IntPtr outTileDataIntPtr)
END_CLASS

CLASS: UnityEngine.Tilemaps.Tile
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x80
EXTENDS: UnityEngine.Tilemaps.TileBase
FIELDS:
  private           UnityEngine.Sprite              m_Sprite  // 0x18
  private           UnityEngine.Color               m_Color  // 0x20
  private           UnityEngine.Matrix4x4           m_Transform  // 0x30
  private           UnityEngine.GameObject          m_InstancedGameObject  // 0x70
  private           UnityEngine.Tilemaps.TileFlags  m_Flags  // 0x78
  private           UnityEngine.Tilemaps.Tile.ColliderTypem_ColliderType  // 0x7c
PROPERTIES:
  sprite  get=0x04D96330  set=0x05392C40
  color  get=0x04DBD320  set=0x04D8C210
  transform  get=0x04DBD730  set=0x04DBD760
  gameObject  get=0x04DBD0F0  set=0x063AD760
  flags  get=0x04DBD140  set=0x04D86440
  colliderType  get=0x04DBD720  set=0x04D86490
METHODS:
  RVA=0x0B45F3E8  token=0x6000012  System.Void GetTileData(UnityEngine.Vector3Int position, UnityEngine.Tilemaps.ITilemap tilemap, UnityEngine.Tilemaps.TileData& tileData)
  RVA=0x0B45F48C  token=0x6000013  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Tilemaps.TileBase
TYPE:  abstract class
TOKEN: 0x2000005
SIZE:  0x18
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
METHODS:
  RVA=0x0B45F158  token=0x6000014  System.Void RefreshTile(UnityEngine.Vector3Int position, UnityEngine.Tilemaps.ITilemap tilemap)
  RVA=0x041E1670  token=0x6000015  System.Void GetTileData(UnityEngine.Vector3Int position, UnityEngine.Tilemaps.ITilemap tilemap, UnityEngine.Tilemaps.TileData& tileData)
  RVA=0x0B45F094  token=0x6000016  UnityEngine.Tilemaps.TileData GetTileDataNoRef(UnityEngine.Vector3Int position, UnityEngine.Tilemaps.ITilemap tilemap)
  RVA=0x04D85B20  token=0x6000017  System.Boolean GetTileAnimationData(UnityEngine.Vector3Int position, UnityEngine.Tilemaps.ITilemap tilemap, UnityEngine.Tilemaps.TileAnimationData& tileAnimationData)
  RVA=0x0302E290  token=0x6000018  UnityEngine.Tilemaps.TileAnimationData GetTileAnimationDataNoRef(UnityEngine.Vector3Int position, UnityEngine.Tilemaps.ITilemap tilemap)
  RVA=0x04DBD6F0  token=0x6000019  System.Void GetTileAnimationDataRef(UnityEngine.Vector3Int position, UnityEngine.Tilemaps.ITilemap tilemap, UnityEngine.Tilemaps.TileAnimationData& tileAnimationData, System.Boolean& hasAnimation)
  RVA=0x04D85B20  token=0x600001A  System.Boolean StartUp(UnityEngine.Vector3Int position, UnityEngine.Tilemaps.ITilemap tilemap, UnityEngine.GameObject go)
  RVA=0x04DBD6F0  token=0x600001B  System.Void StartUpRef(UnityEngine.Vector3Int position, UnityEngine.Tilemaps.ITilemap tilemap, UnityEngine.GameObject go, System.Boolean& startUpInvokedByUser)
  RVA=0x03573370  token=0x600001C  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Tilemaps.Tilemap
TYPE:  sealed class
TOKEN: 0x2000006
SIZE:  0x18
EXTENDS: UnityEngine.GridLayout
FIELDS:
PROPERTIES:
  layoutGrid  get=0x0B45FA4C
  cellBounds  get=0x0B45F9B8
  origin  get=0x0B45FB40
  size  get=0x0B45FBB0
  tileAnchor  get=0x0B45FC20
  orientationMatrix  get=0x0B45FAC4
METHODS:
  RVA=0x0B45F66C  token=0x6000023  UnityEngine.Object GetTileAsset(UnityEngine.Vector3Int position)
  RVA=-1  // generic def  token=0x6000024  T GetTile(UnityEngine.Vector3Int position)
  RVA=0x0B45F6D4  token=0x6000025  UnityEngine.Object[] GetTileAssetsBlock(UnityEngine.Vector3Int position, UnityEngine.Vector3Int blockDimensions)
  RVA=0x0B45F6E8  token=0x6000026  UnityEngine.Tilemaps.TileBase[] GetTilesBlock(UnityEngine.BoundsInt bounds)
  RVA=0x0B45F854  token=0x6000027  System.Boolean HasTile(UnityEngine.Vector3Int position)
  RVA=0x0B45F950  token=0x6000028  System.Void RefreshTile(UnityEngine.Vector3Int position)
  RVA=0x0B45F964  token=0x6000029  System.Void RefreshTilesNative(System.Void* positions, System.Int32 count)
  RVA=0x0B45F814  token=0x600002A  System.Int32 GetUsedTilesCount()
  RVA=0x0B45F848  token=0x600002B  System.Int32 GetUsedTilesNonAlloc(UnityEngine.Tilemaps.TileBase[] usedTiles)
  RVA=0x0B45F8C8  token=0x600002C  System.Int32 Internal_GetUsedTilesNonAlloc(UnityEngine.Object[] usedTiles)
  RVA=0x0B45FAFC  token=0x600002D  System.Void get_origin_Injected(UnityEngine.Vector3Int& ret)
  RVA=0x0B45FB6C  token=0x600002E  System.Void get_size_Injected(UnityEngine.Vector3Int& ret)
  RVA=0x0B45FBDC  token=0x600002F  System.Void get_tileAnchor_Injected(UnityEngine.Vector3& ret)
  RVA=0x0B45FA80  token=0x6000030  System.Void get_orientationMatrix_Injected(UnityEngine.Matrix4x4& ret)
  RVA=0x0B45F628  token=0x6000031  UnityEngine.Object GetTileAsset_Injected(UnityEngine.Vector3Int& position)
  RVA=0x0B45F680  token=0x6000032  UnityEngine.Object[] GetTileAssetsBlock_Injected(UnityEngine.Vector3Int& position, UnityEngine.Vector3Int& blockDimensions)
  RVA=0x0B45F90C  token=0x6000033  System.Void RefreshTile_Injected(UnityEngine.Vector3Int& position)
END_CLASS

CLASS: UnityEngine.Tilemaps.TileFlags
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Tilemaps.TileFlags  None  // const
  public    static  UnityEngine.Tilemaps.TileFlags  LockColor  // const
  public    static  UnityEngine.Tilemaps.TileFlags  LockTransform  // const
  public    static  UnityEngine.Tilemaps.TileFlags  InstantiateGameObjectRuntimeOnly  // const
  public    static  UnityEngine.Tilemaps.TileFlags  KeepGameObjectRuntimeOnly  // const
  public    static  UnityEngine.Tilemaps.TileFlags  LockAll  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Tilemaps.TilemapRenderer
TYPE:  sealed class
TOKEN: 0x2000008
SIZE:  0x18
EXTENDS: UnityEngine.Renderer
FIELDS:
METHODS:
  RVA=0x0B45F538  token=0x6000034  System.Void RegisterSpriteAtlasRegistered()
  RVA=0x0B45F5B0  token=0x6000035  System.Void UnregisterSpriteAtlasRegistered()
  RVA=0x0B45F4F4  token=0x6000036  System.Void OnSpriteAtlasRegistered(UnityEngine.U2D.SpriteAtlas atlas)
END_CLASS

CLASS: UnityEngine.Tilemaps.TileData
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x70
FIELDS:
  private           System.Int32                    m_Sprite  // 0x10
  private           UnityEngine.Color               m_Color  // 0x14
  private           UnityEngine.Matrix4x4           m_Transform  // 0x24
  private           System.Int32                    m_GameObject  // 0x64
  private           UnityEngine.Tilemaps.TileFlags  m_Flags  // 0x68
  private           UnityEngine.Tilemaps.Tile.ColliderTypem_ColliderType  // 0x6c
  private   static readonly UnityEngine.Tilemaps.TileData   Default  // static @ 0x0
PROPERTIES:
  sprite  set=0x0B45F370
  color  set=0x04DB9CC0
  transform  set=0x04DBD700
  gameObject  set=0x0B45F2F4
  flags  set=0x04D865D0
  colliderType  set=0x04D865C0
METHODS:
  RVA=0x0B45F190  token=0x600003D  UnityEngine.Tilemaps.TileData CreateDefault()
  RVA=0x0B45F280  token=0x600003E  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Tilemaps.TileDataNative
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x70
FIELDS:
  private           System.Int32                    m_Sprite  // 0x10
  private           UnityEngine.Color               m_Color  // 0x14
  private           UnityEngine.Matrix4x4           m_Transform  // 0x24
  private           System.Int32                    m_GameObject  // 0x64
  private           UnityEngine.Tilemaps.TileFlags  m_Flags  // 0x68
  private           UnityEngine.Tilemaps.Tile.ColliderTypem_ColliderType  // 0x6c
METHODS:
END_CLASS

CLASS: UnityEngine.Tilemaps.TileChangeData
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x78
FIELDS:
  private           UnityEngine.Vector3Int          m_Position  // 0x10
  private           UnityEngine.Object              m_TileAsset  // 0x20
  private           UnityEngine.Color               m_Color  // 0x28
  private           UnityEngine.Matrix4x4           m_Transform  // 0x38
METHODS:
END_CLASS

CLASS: UnityEngine.Tilemaps.TileAnimationData
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x20
FIELDS:
  private           UnityEngine.Sprite[]            m_AnimatedSprites  // 0x10
  private           System.Single                   m_AnimationSpeed  // 0x18
  private           System.Single                   m_AnimationStartTime  // 0x1c
METHODS:
END_CLASS

