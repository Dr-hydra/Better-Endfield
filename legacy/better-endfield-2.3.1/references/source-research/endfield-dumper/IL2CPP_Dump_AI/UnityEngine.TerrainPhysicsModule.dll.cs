// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.TerrainPhysicsModule.dll
// Classes:  3
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

CLASS: UnityEngine.HGTerrainRaycastHit
TYPE:  sealed struct
TOKEN: 0x2000002
SIZE:  0x44
FIELDS:
  private           UnityEngine.Vector3             m_Position  // 0x10
  private           UnityEngine.Vector3             m_Normal  // 0x1c
  private           System.Single                   m_Distance  // 0x28
  private           UnityEngine.Vector3             m_Edge01  // 0x2c
  private           UnityEngine.Vector3             m_Edge02  // 0x38
PROPERTIES:
  position  get=0x04DBA810
  normal  get=0x04DBA7F0
  edge01  get=0x04DBA870
  edge02  get=0x04DBA850
  distance  get=0x04DBAB50
METHODS:
END_CLASS

CLASS: UnityEngine.TerrainCollider
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x18
EXTENDS: UnityEngine.Collider
FIELDS:
PROPERTIES:
  terrainData  set=0x0B430B80
METHODS:
  RVA=0x0B430AB0  token=0x6000007  UnityEngine.HGTerrainRaycastHit HGTerrainRaycast(UnityEngine.Ray ray, System.Single maxDistance, System.Boolean hitHoles, System.Boolean& hasHit)
  RVA=0x0B430B00  token=0x6000008  System.Boolean HGTerrainRaycast(UnityEngine.Ray ray, UnityEngine.HGTerrainRaycastHit& hitInfo, System.Single maxDistance, System.Boolean hitHoles)
  RVA=0x0B430A4C  token=0x6000009  System.Void HGTerrainRaycast_Injected(UnityEngine.Ray& ray, System.Single maxDistance, System.Boolean hitHoles, System.Boolean& hasHit, UnityEngine.HGTerrainRaycastHit& ret)
END_CLASS

