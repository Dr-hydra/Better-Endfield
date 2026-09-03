// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.Physics2DModule.dll
// Classes:  16
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

CLASS: UnityEngine.PhysicsScene2D
TYPE:  sealed struct
TOKEN: 0x2000002
SIZE:  0x14
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private           System.Int32                    m_Handle  // 0x10
METHODS:
  RVA=0x0B41EFF8  token=0x6000001  System.String ToString()
  RVA=0x04DBA750  token=0x6000002  System.Int32 GetHashCode()
  RVA=0x0B41E80C  token=0x6000003  System.Boolean Equals(System.Object other)
  RVA=0x02EED260  token=0x6000004  System.Boolean Equals(UnityEngine.PhysicsScene2D other)
  RVA=0x0B41EB54  token=0x6000005  UnityEngine.RaycastHit2D Linecast(UnityEngine.Vector2 start, UnityEngine.Vector2 end, UnityEngine.ContactFilter2D contactFilter)
  RVA=0x0B41EB04  token=0x6000006  UnityEngine.RaycastHit2D Linecast_Internal(UnityEngine.PhysicsScene2D physicsScene, UnityEngine.Vector2 start, UnityEngine.Vector2 end, UnityEngine.ContactFilter2D contactFilter)
  RVA=0x0B41EE90  token=0x6000007  UnityEngine.RaycastHit2D Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, System.Int32 layerMask)
  RVA=0x0B41EF70  token=0x6000008  UnityEngine.RaycastHit2D Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, UnityEngine.ContactFilter2D contactFilter)
  RVA=0x0B41ED90  token=0x6000009  UnityEngine.RaycastHit2D Raycast_Internal(UnityEngine.PhysicsScene2D physicsScene, UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, UnityEngine.ContactFilter2D contactFilter)
  RVA=0x0B41EDF0  token=0x600000A  System.Int32 Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, UnityEngine.ContactFilter2D contactFilter, UnityEngine.RaycastHit2D[] results)
  RVA=0x0B41EC30  token=0x600000B  System.Int32 RaycastArray_Internal(UnityEngine.PhysicsScene2D physicsScene, UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, UnityEngine.ContactFilter2D contactFilter, UnityEngine.RaycastHit2D[] results)
  RVA=0x0B41EE40  token=0x600000C  System.Int32 Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, UnityEngine.ContactFilter2D contactFilter, System.Collections.Generic.List<UnityEngine.RaycastHit2D> results)
  RVA=0x0B41ECE0  token=0x600000D  System.Int32 RaycastList_Internal(UnityEngine.PhysicsScene2D physicsScene, UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, UnityEngine.ContactFilter2D contactFilter, System.Collections.Generic.List<UnityEngine.RaycastHit2D> results)
  RVA=0x0B41E9B8  token=0x600000E  UnityEngine.RaycastHit2D GetRayIntersection(UnityEngine.Ray ray, System.Single distance, System.Int32 layerMask)
  RVA=0x0B41E96C  token=0x600000F  UnityEngine.RaycastHit2D GetRayIntersection_Internal(UnityEngine.PhysicsScene2D physicsScene, UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single distance, System.Int32 layerMask)
  RVA=0x0B41EA48  token=0x6000010  System.Int32 GetRayIntersection(UnityEngine.Ray ray, System.Single distance, UnityEngine.RaycastHit2D[] results, System.Int32 layerMask)
  RVA=0x0B41E8D4  token=0x6000011  System.Int32 GetRayIntersectionArray_Internal(UnityEngine.PhysicsScene2D physicsScene, UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single distance, System.Int32 layerMask, UnityEngine.RaycastHit2D[] results)
  RVA=0x0B41EAA0  token=0x6000012  System.Void Linecast_Internal_Injected(UnityEngine.PhysicsScene2D& physicsScene, UnityEngine.Vector2& start, UnityEngine.Vector2& end, UnityEngine.ContactFilter2D& contactFilter, UnityEngine.RaycastHit2D& ret)
  RVA=0x0B41ED2C  token=0x6000013  System.Void Raycast_Internal_Injected(UnityEngine.PhysicsScene2D& physicsScene, UnityEngine.Vector2& origin, UnityEngine.Vector2& direction, System.Single distance, UnityEngine.ContactFilter2D& contactFilter, UnityEngine.RaycastHit2D& ret)
  RVA=0x0B41EBCC  token=0x6000014  System.Int32 RaycastArray_Internal_Injected(UnityEngine.PhysicsScene2D& physicsScene, UnityEngine.Vector2& origin, UnityEngine.Vector2& direction, System.Single distance, UnityEngine.ContactFilter2D& contactFilter, UnityEngine.RaycastHit2D[] results)
  RVA=0x0B41EC7C  token=0x6000015  System.Int32 RaycastList_Internal_Injected(UnityEngine.PhysicsScene2D& physicsScene, UnityEngine.Vector2& origin, UnityEngine.Vector2& direction, System.Single distance, UnityEngine.ContactFilter2D& contactFilter, System.Collections.Generic.List<UnityEngine.RaycastHit2D> results)
  RVA=0x0B41E908  token=0x6000016  System.Void GetRayIntersection_Internal_Injected(UnityEngine.PhysicsScene2D& physicsScene, UnityEngine.Vector3& origin, UnityEngine.Vector3& direction, System.Single distance, System.Int32 layerMask, UnityEngine.RaycastHit2D& ret)
  RVA=0x0B41E870  token=0x6000017  System.Int32 GetRayIntersectionArray_Internal_Injected(UnityEngine.PhysicsScene2D& physicsScene, UnityEngine.Vector3& origin, UnityEngine.Vector3& direction, System.Single distance, System.Int32 layerMask, UnityEngine.RaycastHit2D[] results)
END_CLASS

CLASS: UnityEngine.Physics2D
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.List<UnityEngine.Rigidbody2D>m_LastDisabledRigidbody2D  // static @ 0x0
PROPERTIES:
  defaultPhysicsScene  get=0x04DA17C0
  gravity  set=0x0B41E7C0
  queriesHitTriggers  get=0x0B41E760
METHODS:
  RVA=0x0B41DE64  token=0x600001B  UnityEngine.RaycastHit2D Linecast(UnityEngine.Vector2 start, UnityEngine.Vector2 end, System.Int32 layerMask)
  RVA=0x0B41DD98  token=0x600001C  UnityEngine.RaycastHit2D[] LinecastAll(UnityEngine.Vector2 start, UnityEngine.Vector2 end, System.Int32 layerMask)
  RVA=0x0B41DD34  token=0x600001D  UnityEngine.RaycastHit2D[] LinecastAll_Internal(UnityEngine.PhysicsScene2D physicsScene, UnityEngine.Vector2 start, UnityEngine.Vector2 end, UnityEngine.ContactFilter2D contactFilter)
  RVA=0x0B41E610  token=0x600001E  UnityEngine.RaycastHit2D Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction)
  RVA=0x0B41E354  token=0x600001F  UnityEngine.RaycastHit2D Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance)
  RVA=0x0B41E164  token=0x6000020  UnityEngine.RaycastHit2D Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, System.Int32 layerMask)
  RVA=0x0B41E4E4  token=0x6000021  UnityEngine.RaycastHit2D Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, System.Int32 layerMask, System.Single minDepth)
  RVA=0x0B41DF78  token=0x6000022  UnityEngine.RaycastHit2D Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, System.Int32 layerMask, System.Single minDepth, System.Single maxDepth)
  RVA=0x0B41E0A4  token=0x6000023  System.Int32 Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, UnityEngine.ContactFilter2D contactFilter, UnityEngine.RaycastHit2D[] results)
  RVA=0x0B41E290  token=0x6000024  System.Int32 Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, UnityEngine.ContactFilter2D contactFilter, UnityEngine.RaycastHit2D[] results, System.Single distance)
  RVA=0x0B41E420  token=0x6000025  System.Int32 Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, UnityEngine.ContactFilter2D contactFilter, System.Collections.Generic.List<UnityEngine.RaycastHit2D> results, System.Single distance)
  RVA=0x0B41DC04  token=0x6000026  UnityEngine.RaycastHit2D GetRayIntersection(UnityEngine.Ray ray, System.Single distance, System.Int32 layerMask)
  RVA=0x0B41D9CC  token=0x6000027  UnityEngine.RaycastHit2D[] GetRayIntersectionAll(UnityEngine.Ray ray)
  RVA=0x0B41D940  token=0x6000028  UnityEngine.RaycastHit2D[] GetRayIntersectionAll(UnityEngine.Ray ray, System.Single distance)
  RVA=0x0B41D8AC  token=0x6000029  UnityEngine.RaycastHit2D[] GetRayIntersectionAll(UnityEngine.Ray ray, System.Single distance, System.Int32 layerMask)
  RVA=0x0B41D834  token=0x600002A  UnityEngine.RaycastHit2D[] GetRayIntersectionAll_Internal(UnityEngine.PhysicsScene2D physicsScene, UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single distance, System.Int32 layerMask)
  RVA=0x0B41DB80  token=0x600002B  System.Int32 GetRayIntersectionNonAlloc(UnityEngine.Ray ray, UnityEngine.RaycastHit2D[] results)
  RVA=0x0B41DA50  token=0x600002C  System.Int32 GetRayIntersectionNonAlloc(UnityEngine.Ray ray, UnityEngine.RaycastHit2D[] results, System.Single distance)
  RVA=0x0B41DAE4  token=0x600002D  System.Int32 GetRayIntersectionNonAlloc(UnityEngine.Ray ray, UnityEngine.RaycastHit2D[] results, System.Single distance, System.Int32 layerMask)
  RVA=0x0B41E6D4  token=0x600002E  System.Void .cctor()
  RVA=0x0B41E78C  token=0x600002F  System.Void set_gravity_Injected(UnityEngine.Vector2& value)
  RVA=0x0B41DCD0  token=0x6000030  UnityEngine.RaycastHit2D[] LinecastAll_Internal_Injected(UnityEngine.PhysicsScene2D& physicsScene, UnityEngine.Vector2& start, UnityEngine.Vector2& end, UnityEngine.ContactFilter2D& contactFilter)
  RVA=0x0B41D7D0  token=0x6000031  UnityEngine.RaycastHit2D[] GetRayIntersectionAll_Internal_Injected(UnityEngine.PhysicsScene2D& physicsScene, UnityEngine.Vector3& origin, UnityEngine.Vector3& direction, System.Single distance, System.Int32 layerMask)
END_CLASS

CLASS: UnityEngine.RigidbodyInterpolation2D
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.RigidbodyInterpolation2DNone  // const
  public    static  UnityEngine.RigidbodyInterpolation2DInterpolate  // const
  public    static  UnityEngine.RigidbodyInterpolation2DExtrapolate  // const
METHODS:
END_CLASS

CLASS: UnityEngine.RigidbodySleepMode2D
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.RigidbodySleepMode2DNeverSleep  // const
  public    static  UnityEngine.RigidbodySleepMode2DStartAwake  // const
  public    static  UnityEngine.RigidbodySleepMode2DStartAsleep  // const
METHODS:
END_CLASS

CLASS: UnityEngine.CollisionDetectionMode2D
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.CollisionDetectionMode2DNone  // const
  public    static  UnityEngine.CollisionDetectionMode2DDiscrete  // const
  public    static  UnityEngine.CollisionDetectionMode2DContinuous  // const
METHODS:
END_CLASS

CLASS: UnityEngine.ContactFilter2D
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x2C
FIELDS:
  public            System.Boolean                  useTriggers  // 0x10
  public            System.Boolean                  useLayerMask  // 0x11
  public            System.Boolean                  useDepth  // 0x12
  public            System.Boolean                  useOutsideDepth  // 0x13
  public            System.Boolean                  useNormalAngle  // 0x14
  public            System.Boolean                  useOutsideNormalAngle  // 0x15
  public            UnityEngine.LayerMask           layerMask  // 0x18
  public            System.Single                   minDepth  // 0x1c
  public            System.Single                   maxDepth  // 0x20
  public            System.Single                   minNormalAngle  // 0x24
  public            System.Single                   maxNormalAngle  // 0x28
METHODS:
  RVA=0x0B41D630  token=0x6000032  System.Void CheckConsistency()
  RVA=0x04DBD250  token=0x6000033  System.Void SetLayerMask(UnityEngine.LayerMask layerMask)
  RVA=0x0B41D6F8  token=0x6000034  System.Void SetDepth(System.Single minDepth, System.Single maxDepth)
  RVA=0x0B41D638  token=0x6000035  UnityEngine.ContactFilter2D CreateLegacyFilter(System.Int32 layerMask, System.Single minDepth, System.Single maxDepth)
  RVA=0x0B41D5FC  token=0x6000036  System.Void CheckConsistency_Injected(UnityEngine.ContactFilter2D& _unity_self)
END_CLASS

CLASS: UnityEngine.Collision2D
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x40
FIELDS:
  private           System.Int32                    m_Collider  // 0x10
  private           System.Int32                    m_OtherCollider  // 0x14
  private           System.Int32                    m_Rigidbody  // 0x18
  private           System.Int32                    m_OtherRigidbody  // 0x1c
  private           UnityEngine.Vector2             m_RelativeVelocity  // 0x20
  private           System.Int32                    m_Enabled  // 0x28
  private           System.Int32                    m_ContactCount  // 0x2c
  private           UnityEngine.ContactPoint2D[]    m_ReusedContacts  // 0x30
  private           UnityEngine.ContactPoint2D[]    m_LegacyContacts  // 0x38
PROPERTIES:
  collider  get=0x0B41D224
  rigidbody  get=0x0B41D370
  gameObject  get=0x0B41D2EC
  relativeVelocity  get=0x04DBD230
  contacts  get=0x0B41D284
METHODS:
END_CLASS

CLASS: UnityEngine.ContactPoint2D
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x48
FIELDS:
  private           UnityEngine.Vector2             m_Point  // 0x10
  private           UnityEngine.Vector2             m_Normal  // 0x18
  private           UnityEngine.Vector2             m_RelativeVelocity  // 0x20
  private           System.Single                   m_Separation  // 0x28
  private           System.Single                   m_NormalImpulse  // 0x2c
  private           System.Single                   m_TangentImpulse  // 0x30
  private           System.Int32                    m_Collider  // 0x34
  private           System.Int32                    m_OtherCollider  // 0x38
  private           System.Int32                    m_Rigidbody  // 0x3c
  private           System.Int32                    m_OtherRigidbody  // 0x40
  private           System.Int32                    m_Enabled  // 0x44
PROPERTIES:
  point  get=0x02FB0860
  normal  get=0x04DBAC30
  collider  get=0x0B41D710
  otherCollider  get=0x0B41D770
METHODS:
END_CLASS

CLASS: UnityEngine.RaycastHit2D
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x34
FIELDS:
  private           UnityEngine.Vector2             m_Centroid  // 0x10
  private           UnityEngine.Vector2             m_Point  // 0x18
  private           UnityEngine.Vector2             m_Normal  // 0x20
  private           System.Single                   m_Distance  // 0x28
  private           System.Single                   m_Fraction  // 0x2c
  private           System.Int32                    m_Collider  // 0x30
PROPERTIES:
  point  get=0x04DBAC30
  normal  get=0x04DBD260
  distance  get=0x04DBAB50
  fraction  get=0x04DBD180
  collider  get=0x0B41F290
METHODS:
END_CLASS

CLASS: UnityEngine.Rigidbody2D
TYPE:  sealed class
TOKEN: 0x200000B
SIZE:  0x18
EXTENDS: UnityEngine.Component
FIELDS:
PROPERTIES:
  velocity  get=0x0B41F368
  mass  get=0x0B41F2F0
METHODS:
  RVA=0x05393520  token=0x6000047  System.Void .ctor()
  RVA=0x0B41F324  token=0x6000048  System.Void get_velocity_Injected(UnityEngine.Vector2& ret)
END_CLASS

CLASS: UnityEngine.Collider2D
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x18
EXTENDS: UnityEngine.Behaviour
FIELDS:
PROPERTIES:
  offset  get=0x0B41D200
  attachedRigidbody  get=0x0B41D188
METHODS:
  RVA=0x0B41D16C  token=0x600004B  System.Boolean OverlapPoint(UnityEngine.Vector2 point)
  RVA=0x0B41D1BC  token=0x600004C  System.Void get_offset_Injected(UnityEngine.Vector2& ret)
  RVA=0x0B41D128  token=0x600004D  System.Boolean OverlapPoint_Injected(UnityEngine.Vector2& point)
END_CLASS

CLASS: UnityEngine.CircleCollider2D
TYPE:  sealed class
TOKEN: 0x200000D
SIZE:  0x18
EXTENDS: UnityEngine.Collider2D
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.PolygonCollider2D
TYPE:  sealed class
TOKEN: 0x200000E
SIZE:  0x18
EXTENDS: UnityEngine.Collider2D
FIELDS:
PROPERTIES:
  pathCount  get=0x0B41F25C
METHODS:
  RVA=0x0B41F228  token=0x600004E  System.Int32 GetTotalPointCount()
  RVA=0x0B41F0E8  token=0x6000050  UnityEngine.Vector2[] GetPath(System.Int32 index)
  RVA=0x0B41F0A8  token=0x6000051  UnityEngine.Vector2[] GetPath_Internal(System.Int32 index)
END_CLASS

CLASS: UnityEngine.CompositeCollider2D
TYPE:  sealed class
TOKEN: 0x200000F
SIZE:  0x18
EXTENDS: UnityEngine.Collider2D
FIELDS:
PROPERTIES:
  pathCount  get=0x0B41D594
  pointCount  get=0x0B41D5C8
METHODS:
  RVA=0x0B41D430  token=0x6000054  System.Int32 GetPath(System.Int32 index, UnityEngine.Vector2[] points)
  RVA=0x0B41D3E0  token=0x6000055  System.Int32 GetPathArray_Internal(System.Int32 index, UnityEngine.Vector2[] points)
END_CLASS

CLASS: UnityEngine.PhysicsMaterial2D
TYPE:  sealed class
TOKEN: 0x2000010
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
METHODS:
END_CLASS

