// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.PhysicsModule.dll
// Classes:  36
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

CLASS: CollisionCallback
TYPE:  sealed class
TOKEN: 0x2000017
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B41FF0C  token=0x6000267  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x0B41FEC0  token=0x6000268  System.Void Invoke(UnityEngine.ECSColliderResultProxy thisCollider, UnityEngine.ECSColliderResultProxy otherCollider)
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

CLASS: UnityEngine.RigidbodyConstraints
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.RigidbodyConstraintsNone  // const
  public    static  UnityEngine.RigidbodyConstraintsFreezePositionX  // const
  public    static  UnityEngine.RigidbodyConstraintsFreezePositionY  // const
  public    static  UnityEngine.RigidbodyConstraintsFreezePositionZ  // const
  public    static  UnityEngine.RigidbodyConstraintsFreezeRotationX  // const
  public    static  UnityEngine.RigidbodyConstraintsFreezeRotationY  // const
  public    static  UnityEngine.RigidbodyConstraintsFreezeRotationZ  // const
  public    static  UnityEngine.RigidbodyConstraintsFreezePosition  // const
  public    static  UnityEngine.RigidbodyConstraintsFreezeRotation  // const
  public    static  UnityEngine.RigidbodyConstraintsFreezeAll  // const
METHODS:
END_CLASS

CLASS: UnityEngine.ForceMode
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.ForceMode           Force  // const
  public    static  UnityEngine.ForceMode           Acceleration  // const
  public    static  UnityEngine.ForceMode           Impulse  // const
  public    static  UnityEngine.ForceMode           VelocityChange  // const
METHODS:
END_CLASS

CLASS: UnityEngine.MeshColliderCookingOptions
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.MeshColliderCookingOptionsNone  // const
  public    static  UnityEngine.MeshColliderCookingOptionsInflateConvexMesh  // const
  public    static  UnityEngine.MeshColliderCookingOptionsCookForFasterSimulation  // const
  public    static  UnityEngine.MeshColliderCookingOptionsEnableMeshCleaning  // const
  public    static  UnityEngine.MeshColliderCookingOptionsWeldColocatedVertices  // const
  public    static  UnityEngine.MeshColliderCookingOptionsUseFastMidphase  // const
METHODS:
END_CLASS

CLASS: UnityEngine.ColliderSurfaceType
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.ColliderSurfaceType Default  // const
  public    static  UnityEngine.ColliderSurfaceType Concrete  // const
  public    static  UnityEngine.ColliderSurfaceType Wood  // const
  public    static  UnityEngine.ColliderSurfaceType Dirt  // const
  public    static  UnityEngine.ColliderSurfaceType Grass  // const
  public    static  UnityEngine.ColliderSurfaceType Stone  // const
  public    static  UnityEngine.ColliderSurfaceType Water  // const
  public    static  UnityEngine.ColliderSurfaceType Flesh  // const
  public    static  UnityEngine.ColliderSurfaceType Metal  // const
  public    static  UnityEngine.ColliderSurfaceType Crystal  // const
  public    static  UnityEngine.ColliderSurfaceType Rubber  // const
  public    static  UnityEngine.ColliderSurfaceType Sand  // const
  public    static  UnityEngine.ColliderSurfaceType Gravel  // const
  public    static  UnityEngine.ColliderSurfaceType Moss  // const
  public    static  UnityEngine.ColliderSurfaceType Custom  // const
  public    static  UnityEngine.ColliderSurfaceType Snow  // const
  public    static  UnityEngine.ColliderSurfaceType Mud  // const
  public    static  UnityEngine.ColliderSurfaceType Cardboard  // const
  public    static  UnityEngine.ColliderSurfaceType Plastic  // const
  public    static  UnityEngine.ColliderSurfaceType Ice  // const
  public    static  UnityEngine.ColliderSurfaceType Wet  // const
  public    static  UnityEngine.ColliderSurfaceType WaterV2  // const
METHODS:
END_CLASS

CLASS: UnityEngine.RigidbodyInterpolation
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.RigidbodyInterpolationNone  // const
  public    static  UnityEngine.RigidbodyInterpolationInterpolate  // const
  public    static  UnityEngine.RigidbodyInterpolationExtrapolate  // const
METHODS:
END_CLASS

CLASS: UnityEngine.ControllerColliderHit
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x50
FIELDS:
  private           UnityEngine.CharacterController m_Controller  // 0x10
  private           UnityEngine.Collider            m_Collider  // 0x18
  private           UnityEngine.Vector3             m_Point  // 0x20
  private           UnityEngine.Vector3             m_Normal  // 0x2c
  private           UnityEngine.Vector3             m_MoveDirection  // 0x38
  private           System.Single                   m_MoveLength  // 0x44
  private           System.Int32                    m_Push  // 0x48
PROPERTIES:
  gameObject  get=0x0B4200FC
  point  get=0x04DBD280
METHODS:
END_CLASS

CLASS: UnityEngine.Collision
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x50
FIELDS:
  private           UnityEngine.Vector3             m_Impulse  // 0x10
  private           UnityEngine.Vector3             m_RelativeVelocity  // 0x1c
  private           UnityEngine.Component           m_Body  // 0x28
  private           UnityEngine.Collider            m_Collider  // 0x30
  private           System.Int32                    m_ContactCount  // 0x38
  private           UnityEngine.ContactPoint[]      m_ReusedContacts  // 0x40
  private           UnityEngine.ContactPoint[]      m_LegacyContacts  // 0x48
PROPERTIES:
  relativeVelocity  get=0x04DBA870
  collider  get=0x04DBBEB0
  gameObject  get=0x0B42003C
  contacts  get=0x0B41FFD4
  impulse  get=0x04DBA8C0
METHODS:
END_CLASS

CLASS: UnityEngine.QueryTriggerInteraction
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.QueryTriggerInteractionUseGlobal  // const
  public    static  UnityEngine.QueryTriggerInteractionIgnore  // const
  public    static  UnityEngine.QueryTriggerInteractionCollide  // const
METHODS:
END_CLASS

CLASS: UnityEngine.CollisionDetectionMode
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.CollisionDetectionModeDiscrete  // const
  public    static  UnityEngine.CollisionDetectionModeContinuous  // const
  public    static  UnityEngine.CollisionDetectionModeContinuousDynamic  // const
  public    static  UnityEngine.CollisionDetectionModeContinuousSpeculative  // const
METHODS:
END_CLASS

CLASS: UnityEngine.ArticulationBody
TYPE:  class
TOKEN: 0x200000D
SIZE:  0x18
EXTENDS: UnityEngine.Behaviour
FIELDS:
METHODS:
  RVA=0x05393520  token=0x600000A  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Physics
TYPE:  class
TOKEN: 0x200000E
SIZE:  0x10
FIELDS:
  private   static  System.Action<UnityEngine.PhysicsScene,Unity.Collections.NativeArray<UnityEngine.ModifiableContactPair>>ContactModifyEvent  // static @ 0x0
  private   static  System.Action<UnityEngine.PhysicsScene,Unity.Collections.NativeArray<UnityEngine.ModifiableContactPair>>ContactModifyEventCCD  // static @ 0x8
  private   static  System.Single                   k_MaxFloatMinusEpsilon  // const
  public    static  System.Int32                    IgnoreRaycastLayer  // const
  public    static  System.Int32                    DefaultRaycastLayers  // const
  public    static  System.Int32                    AllLayers  // const
  public    static  System.Int32                    kIgnoreRaycastLayer  // const
  public    static  System.Int32                    kDefaultRaycastLayers  // const
  public    static  System.Int32                    kAllLayers  // const
PROPERTIES:
  gravity  get=0x045B55E0  set=0x0B428FB0
  defaultContactOffset  get=0x02F62830  set=0x0B428E60
  sleepThreshold  get=0x0B428C20  set=0x0B42913C
  queriesHitTriggers  get=0x0B428BC8  set=0x0B4290D4
  queriesHitBackfaces  get=0x0B428B9C  set=0x0B4290A0
  bounceThreshold  get=0x0B428980  set=0x0B428DD8
  defaultMaxDepenetrationVelocity  get=0x0B428A3C  set=0x0B428ED8
  defaultSolverIterations  get=0x0B428A68  set=0x0B428F14
  defaultSolverVelocityIterations  get=0x0B428A94  set=0x0B428F48
  defaultMaxAngularSpeed  get=0x0B428A10  set=0x0B428E9C
  improvedPatchFriction  get=0x0B428AC0  set=0x0B428FC0
  defaultPhysicsScene  get=0x03207570
  logPhysXCheckError  get=0x0B428B70  set=0x040E4B70
  autoSimulation  get=0x03C330B0  set=0x040E4BB0
  autoSyncTransforms  get=0x0B428954  set=0x0B428DA4
  reuseCollisionCallbacks  get=0x0B428BF4  set=0x0B429108
  interCollisionDistance  get=0x0B428AEC  set=0x0B428FF4
  interCollisionStiffness  get=0x0B428B44  set=0x0B429064
  interCollisionSettingsToggle  get=0x0B428B18  set=0x0B429030
  clothGravity  get=0x0B4289EC  set=0x0B428E50
  minPenetrationForPenalty  get=0x04DBAEC0  set=0x041E1670
  bounceTreshold  get=0x0B4289AC  set=0x0B428E14
  sleepVelocity  get=0x04DBAEC0  set=0x041E1670
  sleepAngularVelocity  get=0x04DBAEC0  set=0x041E1670
  maxAngularVelocity  get=0x04DBAEC0  set=0x041E1670
  solverIterationCount  get=0x0B428C4C  set=0x0B429178
  solverVelocityIterationCount  get=0x0B428C58  set=0x0B429180
  penetrationPenaltyForce  get=0x04DBAEC0  set=0x041E1670
EVENTS:
  ContactModifyEvent  add=add_ContactModifyEvent  remove=remove_ContactModifyEvent
  ContactModifyEventCCD  add=add_ContactModifyEventCCD  remove=remove_ContactModifyEventCCD
METHODS:
  RVA=0x0B424F74  token=0x600000F  System.Void OnSceneContactModify(UnityEngine.PhysicsScene scene, System.IntPtr buffer, System.Int32 count, System.Boolean isCCD)
  RVA=0x0427A2A0  token=0x6000029  System.Void ClearUnusedMemory()
  RVA=0x0B424940  token=0x600002A  System.Void IgnoreCollision(UnityEngine.Collider collider1, UnityEngine.Collider collider2, System.Boolean ignore)
  RVA=0x0B424994  token=0x600002B  System.Void IgnoreCollision(UnityEngine.Collider collider1, UnityEngine.Collider collider2)
  RVA=0x0B4249AC  token=0x600002C  System.Void IgnoreLayerCollision(System.Int32 layer1, System.Int32 layer2, System.Boolean ignore)
  RVA=0x0B424878  token=0x600002D  System.Void ForceRebuildDynamicTree(System.Boolean rebuildStatic, System.Boolean rebuildDynamic)
  RVA=0x0B4249A0  token=0x600002E  System.Void IgnoreLayerCollision(System.Int32 layer1, System.Int32 layer2)
  RVA=0x0B424900  token=0x600002F  System.Boolean GetIgnoreLayerCollision(System.Int32 layer1, System.Int32 layer2)
  RVA=0x0B4248BC  token=0x6000030  System.Boolean GetIgnoreCollision(UnityEngine.Collider collider1, UnityEngine.Collider collider2)
  RVA=0x0B427888  token=0x6000031  System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B427718  token=0x6000032  System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B427C40  token=0x6000033  System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single maxDistance)
  RVA=0x0B427B44  token=0x6000034  System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction)
  RVA=0x02F61D50  token=0x6000035  System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x046D9BD0  token=0x6000036  System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x03CF1320  token=0x6000037  System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance)
  RVA=0x0B4277A8  token=0x6000038  System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo)
  RVA=0x0B4275F0  token=0x6000039  System.Boolean RaycastV2(UnityEngine.ECSColliderResultProxy proxy, UnityEngine.Ray ray, System.Single maxDistance, UnityEngine.RaycastHit& hit)
  RVA=0x0B427684  token=0x600003A  System.Boolean RaycastV2(UnityEngine.ECSColliderResultProxy proxy, UnityEngine.Ray ray, System.Single maxDistance, System.Boolean hitHoles, UnityEngine.RaycastHit& hit)
  RVA=0x0B424BA4  token=0x600003B  UnityEngine.RaycastHit Internal_RaycastTestV2(UnityEngine.ECSColliderResultProxy collider, UnityEngine.Ray ray, System.Single maxDistance, System.Boolean hitHoles, System.Boolean& hasHit)
  RVA=0x0B427AB4  token=0x600003C  System.Boolean Raycast(UnityEngine.Ray ray, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B427920  token=0x600003D  System.Boolean Raycast(UnityEngine.Ray ray, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B4279A8  token=0x600003E  System.Boolean Raycast(UnityEngine.Ray ray, System.Single maxDistance)
  RVA=0x0B427CC8  token=0x600003F  System.Boolean Raycast(UnityEngine.Ray ray)
  RVA=0x03028220  token=0x6000040  System.Boolean Raycast(UnityEngine.Ray ray, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B427838  token=0x6000041  System.Boolean Raycast(UnityEngine.Ray ray, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B427A24  token=0x6000042  System.Boolean Raycast(UnityEngine.Ray ray, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance)
  RVA=0x0B427BBC  token=0x6000043  System.Boolean Raycast(UnityEngine.Ray ray, UnityEngine.RaycastHit& hitInfo)
  RVA=0x0B424E14  token=0x6000044  System.Boolean Linecast(UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B424EE4  token=0x6000045  System.Boolean Linecast(UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Int32 layerMask)
  RVA=0x0B424DC8  token=0x6000046  System.Boolean Linecast(UnityEngine.Vector3 start, UnityEngine.Vector3 end)
  RVA=0x0B424CA4  token=0x6000047  System.Boolean Linecast(UnityEngine.Vector3 start, UnityEngine.Vector3 end, UnityEngine.RaycastHit& hitInfo, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B424F2C  token=0x6000048  System.Boolean Linecast(UnityEngine.Vector3 start, UnityEngine.Vector3 end, UnityEngine.RaycastHit& hitInfo, System.Int32 layerMask)
  RVA=0x0B424D7C  token=0x6000049  System.Boolean Linecast(UnityEngine.Vector3 start, UnityEngine.Vector3 end, UnityEngine.RaycastHit& hitInfo)
  RVA=0x0B423ACC  token=0x600004A  System.Boolean CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B423D74  token=0x600004B  System.Boolean CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B423C24  token=0x600004C  System.Boolean CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance)
  RVA=0x0B4239D0  token=0x600004D  System.Boolean CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction)
  RVA=0x0B423C98  token=0x600004E  System.Boolean CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B423A44  token=0x600004F  System.Boolean CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B423948  token=0x6000050  System.Boolean CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance)
  RVA=0x0B423BA0  token=0x6000051  System.Boolean CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo)
  RVA=0x03730420  token=0x6000052  System.Boolean SphereCast(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B4283EC  token=0x6000053  System.Boolean SphereCast(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B428640  token=0x6000054  System.Boolean SphereCast(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance)
  RVA=0x0B4284C4  token=0x6000055  System.Boolean SphereCast(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo)
  RVA=0x0B428524  token=0x6000056  System.Boolean SphereCast(UnityEngine.Ray ray, System.Single radius, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B42860C  token=0x6000057  System.Boolean SphereCast(UnityEngine.Ray ray, System.Single radius, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B42848C  token=0x6000058  System.Boolean SphereCast(UnityEngine.Ray ray, System.Single radius, System.Single maxDistance)
  RVA=0x0B428700  token=0x6000059  System.Boolean SphereCast(UnityEngine.Ray ray, System.Single radius)
  RVA=0x0B4286A0  token=0x600005A  System.Boolean SphereCast(UnityEngine.Ray ray, System.Single radius, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B428740  token=0x600005B  System.Boolean SphereCast(UnityEngine.Ray ray, System.Single radius, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B428450  token=0x600005C  System.Boolean SphereCast(UnityEngine.Ray ray, System.Single radius, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance)
  RVA=0x0B4285C8  token=0x600005D  System.Boolean SphereCast(UnityEngine.Ray ray, System.Single radius, UnityEngine.RaycastHit& hitInfo)
  RVA=0x0B422C34  token=0x600005E  System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B422F84  token=0x600005F  System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B422E38  token=0x6000060  System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation, System.Single maxDistance)
  RVA=0x0B4230A4  token=0x6000061  System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation)
  RVA=0x0B422BA4  token=0x6000062  System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction)
  RVA=0x0B422EC0  token=0x6000063  System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B422D08  token=0x6000064  System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B42312C  token=0x6000065  System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, UnityEngine.Quaternion orientation, System.Single maxDistance)
  RVA=0x0B423010  token=0x6000066  System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, UnityEngine.Quaternion orientation)
  RVA=0x0B422DA0  token=0x6000067  System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo)
  RVA=0x0B424B18  token=0x6000068  UnityEngine.RaycastHit[] Internal_RaycastAll(UnityEngine.PhysicsScene physicsScene, UnityEngine.Ray ray, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B4270AC  token=0x6000069  UnityEngine.RaycastHit[] RaycastAll(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B427064  token=0x600006A  UnityEngine.RaycastHit[] RaycastAll(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B42721C  token=0x600006B  UnityEngine.RaycastHit[] RaycastAll(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single maxDistance)
  RVA=0x0B427268  token=0x600006C  UnityEngine.RaycastHit[] RaycastAll(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction)
  RVA=0x0B4271D0  token=0x600006D  UnityEngine.RaycastHit[] RaycastAll(UnityEngine.Ray ray, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B427018  token=0x600006E  UnityEngine.RaycastHit[] RaycastAll(UnityEngine.Ray ray, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B426FC8  token=0x600006F  UnityEngine.RaycastHit[] RaycastAll(UnityEngine.Ray ray, System.Single maxDistance)
  RVA=0x0B4272BC  token=0x6000070  UnityEngine.RaycastHit[] RaycastAll(UnityEngine.Ray ray)
  RVA=0x03B1D970  token=0x6000071  System.Int32 RaycastNonAlloc(UnityEngine.Ray ray, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x03906800  token=0x6000072  System.Int32 RaycastNonAlloc(UnityEngine.Ray ray, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B427310  token=0x6000073  System.Int32 RaycastNonAlloc(UnityEngine.Ray ray, UnityEngine.RaycastHit[] results, System.Single maxDistance)
  RVA=0x0B4273A0  token=0x6000074  System.Int32 RaycastNonAlloc(UnityEngine.Ray ray, UnityEngine.RaycastHit[] results)
  RVA=0x03206500  token=0x6000075  System.Int32 RaycastNonAlloc(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B4274C0  token=0x6000076  System.Int32 RaycastNonAlloc(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B427424  token=0x6000077  System.Int32 RaycastNonAlloc(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance)
  RVA=0x0B427560  token=0x6000078  System.Int32 RaycastNonAlloc(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results)
  RVA=0x0B426974  token=0x6000079  UnityEngine.RaycastHit[] Query_CapsuleCastAll(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B4231C0  token=0x600007A  UnityEngine.RaycastHit[] CapsuleCastAll(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B423374  token=0x600007B  UnityEngine.RaycastHit[] CapsuleCastAll(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B423300  token=0x600007C  UnityEngine.RaycastHit[] CapsuleCastAll(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance)
  RVA=0x0B4233EC  token=0x600007D  UnityEngine.RaycastHit[] CapsuleCastAll(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction)
  RVA=0x0B426ED0  token=0x600007E  UnityEngine.RaycastHit[] Query_SphereCastAll(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B428080  token=0x600007F  UnityEngine.RaycastHit[] SphereCastAll(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B427EB8  token=0x6000080  UnityEngine.RaycastHit[] SphereCastAll(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B427FF8  token=0x6000081  UnityEngine.RaycastHit[] SphereCastAll(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance)
  RVA=0x0B427FA0  token=0x6000082  UnityEngine.RaycastHit[] SphereCastAll(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction)
  RVA=0x0B427F4C  token=0x6000083  UnityEngine.RaycastHit[] SphereCastAll(UnityEngine.Ray ray, System.Single radius, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B427E84  token=0x6000084  UnityEngine.RaycastHit[] SphereCastAll(UnityEngine.Ray ray, System.Single radius, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B428048  token=0x6000085  UnityEngine.RaycastHit[] SphereCastAll(UnityEngine.Ray ray, System.Single radius, System.Single maxDistance)
  RVA=0x0B427F0C  token=0x6000086  UnityEngine.RaycastHit[] SphereCastAll(UnityEngine.Ray ray, System.Single radius)
  RVA=0x0B426024  token=0x6000087  UnityEngine.Collider[] OverlapCapsule_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B426118  token=0x6000088  UnityEngine.Collider[] OverlapCapsule(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B426054  token=0x6000089  UnityEngine.Collider[] OverlapCapsule(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, System.Int32 layerMask)
  RVA=0x0B4261F0  token=0x600008A  UnityEngine.Collider[] OverlapCapsule(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius)
  RVA=0x0B425E70  token=0x600008B  UnityEngine.ECSColliderResultProxy[] OverlapCapsuleV2_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B425F34  token=0x600008C  UnityEngine.ECSColliderResultProxy[] OverlapCapsuleV2(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B425EEC  token=0x600008D  UnityEngine.ECSColliderResultProxy[] OverlapCapsuleV2(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, System.Int32 layerMask)
  RVA=0x0B425EA0  token=0x600008E  UnityEngine.ECSColliderResultProxy[] OverlapCapsuleV2(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius)
  RVA=0x0B4266FC  token=0x600008F  UnityEngine.Collider[] OverlapSphere_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B4267C4  token=0x6000090  UnityEngine.Collider[] OverlapSphere(UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x03679460  token=0x6000091  UnityEngine.ECSColliderResultProxy CreateECSProxy(UnityEngine.Collider collider)
  RVA=0x0B426724  token=0x6000092  UnityEngine.Collider[] OverlapSphere(UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask)
  RVA=0x0B42687C  token=0x6000093  UnityEngine.Collider[] OverlapSphere(UnityEngine.Vector3 position, System.Single radius)
  RVA=0x0B42659C  token=0x6000094  UnityEngine.ECSColliderResultProxy[] OverlapSphereV2_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B4265FC  token=0x6000095  UnityEngine.ECSColliderResultProxy[] OverlapSphereV2(UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B426668  token=0x6000096  UnityEngine.ECSColliderResultProxy[] OverlapSphereV2(UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask)
  RVA=0x0B4265C4  token=0x6000097  UnityEngine.ECSColliderResultProxy[] OverlapSphereV2(UnityEngine.Vector3 position, System.Single radius)
  RVA=0x05395788  token=0x6000098  System.Void Simulate_Internal(UnityEngine.PhysicsScene physicsScene, System.Single step)
  RVA=0x03C33040  token=0x6000099  System.Void Simulate(System.Single step)
  RVA=0x032CA830  token=0x600009C  System.Void SyncTransforms()
  RVA=0x0B426D84  token=0x60000A1  System.Boolean Query_ComputePenetration(UnityEngine.Collider colliderA, UnityEngine.Vector3 positionA, UnityEngine.Quaternion rotationA, UnityEngine.Collider colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance)
  RVA=0x0B424690  token=0x60000A2  System.Boolean ComputePenetration(UnityEngine.Collider colliderA, UnityEngine.Vector3 positionA, UnityEngine.Quaternion rotationA, UnityEngine.Collider colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance)
  RVA=0x05393470  token=0x60000A3  System.Boolean Query_ComputePenetrationV2(UnityEngine.ECSColliderResultProxy colliderA, UnityEngine.Vector3 positionA, UnityEngine.Quaternion rotationA, UnityEngine.ECSColliderResultProxy colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance)
  RVA=0x03679BB0  token=0x60000A4  System.Boolean ComputePenetrationV2(UnityEngine.ECSColliderResultProxy colliderA, UnityEngine.Vector3 positionA, UnityEngine.Quaternion rotationA, UnityEngine.ECSColliderResultProxy colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance)
  RVA=0x0B426CD4  token=0x60000A5  System.Boolean Query_ComputeCapsulePenetration(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Collider colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance)
  RVA=0x0B424564  token=0x60000A6  System.Boolean ComputeCapsulePenetration(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Collider colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance)
  RVA=0x0B426C24  token=0x60000A7  System.Boolean Query_ComputeCapsulePenetrationV2(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.ECSColliderResultProxy colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance)
  RVA=0x0B424498  token=0x60000A8  System.Boolean ComputeCapsulePenetrationV2(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.ECSColliderResultProxy colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance)
  RVA=0x0B426E34  token=0x60000A9  System.Boolean Query_ComputeSpherePenetrationV2(UnityEngine.Vector3 center, System.Single radius, UnityEngine.ECSColliderResultProxy colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance)
  RVA=0x0B4247C8  token=0x60000AA  System.Boolean ComputeSpherePenetrationV2(UnityEngine.Vector3 center, System.Single radius, UnityEngine.ECSColliderResultProxy colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance)
  RVA=0x0B426B74  token=0x60000AB  System.Boolean Query_ComputeBoxPenetrationV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtent, UnityEngine.Quaternion rotationA, UnityEngine.ECSColliderResultProxy colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance)
  RVA=0x0B4243BC  token=0x60000AC  System.Boolean ComputeBoxPenetrationV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtent, UnityEngine.Quaternion rotationA, UnityEngine.ECSColliderResultProxy colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance)
  RVA=0x0B424C6C  token=0x60000AD  System.Boolean IsSimulatedThisFrame_Internal()
  RVA=0x0B424C98  token=0x60000AE  System.Boolean IsSimulatedThisFrame()
  RVA=0x0B427E48  token=0x60000AF  System.Void SetForceSimulateThisFrame_Internal(System.Boolean force)
  RVA=0x0B427E7C  token=0x60000B0  System.Void SetForceSimulateThisFrame(System.Boolean force)
  RVA=0x04D51170  token=0x60000B1  System.Void SetFastSimulate_Internal(System.Boolean fast)
  RVA=0x05396EC0  token=0x60000B2  System.Void SetFastSimulate(System.Boolean fast)
  RVA=0x0B426AD0  token=0x60000B3  UnityEngine.Vector3 Query_ClosestPoint(UnityEngine.Collider collider, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 point)
  RVA=0x0B4242C4  token=0x60000B4  UnityEngine.Vector3 ClosestPoint(UnityEngine.Vector3 point, UnityEngine.Collider collider, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
  RVA=0x0B426A2C  token=0x60000B5  UnityEngine.Vector3 Query_ClosestPointV2(UnityEngine.ECSColliderResultProxy colliderProxy, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 point)
  RVA=0x0B424220  token=0x60000B6  UnityEngine.Vector3 ClosestPointV2(UnityEngine.Vector3 point, UnityEngine.ECSColliderResultProxy colliderProxy, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
  RVA=0x0B426F74  token=0x60000B7  UnityEngine.Vector3 Query_TryGetClosestPoint(UnityEngine.ECSColliderResultProxy colliderProxy, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 point, System.Boolean& resultIsValid)
  RVA=0x0B42877C  token=0x60000B8  System.Boolean TryGetClosestPoint(UnityEngine.ECSColliderResultProxy colliderProxy, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 point, UnityEngine.Vector3& result)
  RVA=0x0B42646C  token=0x60000C1  System.Int32 OverlapSphereNonAlloc(UnityEngine.Vector3 position, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B4263B8  token=0x60000C2  System.Int32 OverlapSphereNonAlloc(UnityEngine.Vector3 position, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask)
  RVA=0x0B426310  token=0x60000C3  System.Int32 OverlapSphereNonAlloc(UnityEngine.Vector3 position, System.Single radius, UnityEngine.Collider[] results)
  RVA=0x03D7C060  token=0x60000C4  System.Int32 OverlapSphereNonAllocV2(UnityEngine.Vector3 position, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B4262E0  token=0x60000C5  System.Int32 OverlapSphereNonAllocV2(UnityEngine.Vector3 position, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask)
  RVA=0x0B4262A8  token=0x60000C6  System.Int32 OverlapSphereNonAllocV2(UnityEngine.Vector3 position, System.Single radius, UnityEngine.ECSColliderResultProxy[] results)
  RVA=0x0B424124  token=0x60000C7  System.Boolean CheckSphere_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B424184  token=0x60000C8  System.Boolean CheckSphere(UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B4241F0  token=0x60000C9  System.Boolean CheckSphere(UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask)
  RVA=0x0B42414C  token=0x60000CA  System.Boolean CheckSphere(UnityEngine.Vector3 position, System.Single radius)
  RVA=0x030F1F60  token=0x60000CB  System.Int32 CapsuleCastNonAlloc(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B423604  token=0x60000CC  System.Int32 CapsuleCastNonAllocV2(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.ECSColliderResultProxy[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B4237B4  token=0x60000CD  System.Int32 CapsuleCastNonAlloc(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B4236E0  token=0x60000CE  System.Int32 CapsuleCastNonAllocV2(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.ECSColliderResultProxy[] results, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B4238C0  token=0x60000CF  System.Int32 CapsuleCastNonAlloc(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance)
  RVA=0x0B423530  token=0x60000D0  System.Int32 CapsuleCastNonAllocV2(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.ECSColliderResultProxy[] results, System.Single maxDistance)
  RVA=0x0B42383C  token=0x60000D1  System.Int32 CapsuleCastNonAlloc(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results)
  RVA=0x0B423460  token=0x60000D2  System.Int32 CapsuleCastNonAllocV2(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.ECSColliderResultProxy[] results)
  RVA=0x030F58C0  token=0x60000D3  System.Int32 SphereCastNonAlloc(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B428328  token=0x60000D4  System.Int32 SphereCastNonAlloc(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B428268  token=0x60000D5  System.Int32 SphereCastNonAlloc(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance)
  RVA=0x0B42838C  token=0x60000D6  System.Int32 SphereCastNonAlloc(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results)
  RVA=0x0B4282C8  token=0x60000D7  System.Int32 SphereCastNonAlloc(UnityEngine.Ray ray, System.Single radius, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B4281F0  token=0x60000D8  System.Int32 SphereCastNonAlloc(UnityEngine.Ray ray, System.Single radius, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B42822C  token=0x60000D9  System.Int32 SphereCastNonAlloc(UnityEngine.Ray ray, System.Single radius, UnityEngine.RaycastHit[] results, System.Single maxDistance)
  RVA=0x0B4281AC  token=0x60000DA  System.Int32 SphereCastNonAlloc(UnityEngine.Ray ray, System.Single radius, UnityEngine.RaycastHit[] results)
  RVA=0x053909E0  token=0x60000DB  System.Boolean CheckCapsule_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x02F65310  token=0x60000DC  System.Boolean CheckCapsule(UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B42402C  token=0x60000DD  System.Boolean CheckCapsule(UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Single radius, System.Int32 layerMask)
  RVA=0x0B424074  token=0x60000DE  System.Boolean CheckCapsule(UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Single radius)
  RVA=0x0B423E50  token=0x60000DF  System.Boolean CheckBox_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, System.Int32 layermask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B423F44  token=0x60000E0  System.Boolean CheckBox(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, System.Int32 layermask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B423FD8  token=0x60000E1  System.Boolean CheckBox(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, System.Int32 layerMask)
  RVA=0x0B423E80  token=0x60000E2  System.Boolean CheckBox(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation)
  RVA=0x0B423EDC  token=0x60000E3  System.Boolean CheckBox(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents)
  RVA=0x0B425774  token=0x60000E4  UnityEngine.Collider[] OverlapBox_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B425928  token=0x60000E5  UnityEngine.Collider[] OverlapBox(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B425A0C  token=0x60000E6  UnityEngine.Collider[] OverlapBox(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, System.Int32 layerMask)
  RVA=0x0B4257A4  token=0x60000E7  UnityEngine.Collider[] OverlapBox(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation)
  RVA=0x0B425868  token=0x60000E8  UnityEngine.Collider[] OverlapBox(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents)
  RVA=0x0B425150  token=0x60000E9  System.Int32 OverlapBoxNonAlloc(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Collider[] results, UnityEngine.Quaternion orientation, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B42524C  token=0x60000EA  System.Int32 OverlapBoxNonAlloc(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Collider[] results, UnityEngine.Quaternion orientation, System.Int32 mask)
  RVA=0x0B425328  token=0x60000EB  System.Int32 OverlapBoxNonAlloc(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Collider[] results, UnityEngine.Quaternion orientation)
  RVA=0x0B425400  token=0x60000EC  System.Int32 OverlapBoxNonAlloc(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Collider[] results)
  RVA=0x0B425538  token=0x60000ED  UnityEngine.ECSColliderResultProxy[] OverlapBoxV2_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B42567C  token=0x60000EE  UnityEngine.ECSColliderResultProxy[] OverlapBoxV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B425628  token=0x60000EF  UnityEngine.ECSColliderResultProxy[] OverlapBoxV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, System.Int32 layerMask)
  RVA=0x0B425568  token=0x60000F0  UnityEngine.ECSColliderResultProxy[] OverlapBoxV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation)
  RVA=0x0B4255C0  token=0x60000F1  UnityEngine.ECSColliderResultProxy[] OverlapBoxV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents)
  RVA=0x045ADD40  token=0x60000F2  System.Int32 OverlapBoxNonAllocV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.ECSColliderResultProxy[] results, UnityEngine.Quaternion orientation, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B425098  token=0x60000F3  System.Int32 OverlapBoxNonAllocV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.ECSColliderResultProxy[] results, UnityEngine.Quaternion orientation, System.Int32 mask)
  RVA=0x0B4250F8  token=0x60000F4  System.Int32 OverlapBoxNonAllocV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.ECSColliderResultProxy[] results, UnityEngine.Quaternion orientation)
  RVA=0x0B425030  token=0x60000F5  System.Int32 OverlapBoxNonAllocV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.ECSColliderResultProxy[] results)
  RVA=0x03027790  token=0x60000F6  System.Int32 BoxCastNonAlloc(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B422A7C  token=0x60000F7  System.Int32 BoxCastNonAlloc(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, UnityEngine.Quaternion orientation)
  RVA=0x0B422B10  token=0x60000F8  System.Int32 BoxCastNonAlloc(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, UnityEngine.Quaternion orientation, System.Single maxDistance)
  RVA=0x0B42294C  token=0x60000F9  System.Int32 BoxCastNonAlloc(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B4229E4  token=0x60000FA  System.Int32 BoxCastNonAlloc(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results)
  RVA=0x0B424A60  token=0x60000FB  UnityEngine.RaycastHit[] Internal_BoxCastAll(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B4225D8  token=0x60000FC  UnityEngine.RaycastHit[] BoxCastAll(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B422830  token=0x60000FD  UnityEngine.RaycastHit[] BoxCastAll(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask)
  RVA=0x0B422720  token=0x60000FE  UnityEngine.RaycastHit[] BoxCastAll(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation, System.Single maxDistance)
  RVA=0x0B4227A8  token=0x60000FF  UnityEngine.RaycastHit[] BoxCastAll(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation)
  RVA=0x0B4228BC  token=0x6000100  UnityEngine.RaycastHit[] BoxCastAll(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction)
  RVA=0x0B425D1C  token=0x6000101  System.Int32 OverlapCapsuleNonAlloc(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B425B7C  token=0x6000102  System.Int32 OverlapCapsuleNonAlloc(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask)
  RVA=0x0B425C50  token=0x6000103  System.Int32 OverlapCapsuleNonAlloc(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.Collider[] results)
  RVA=0x031FFA70  token=0x6000104  System.Int32 OverlapCapsuleNonAllocV2(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B425B2C  token=0x6000105  System.Int32 OverlapCapsuleNonAllocV2(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask)
  RVA=0x0B425AE0  token=0x6000106  System.Int32 OverlapCapsuleNonAllocV2(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.ECSColliderResultProxy[] results)
  RVA=0x0B424C58  token=0x6000107  System.Void Internal_RebuildBroadphaseRegions(UnityEngine.Bounds bounds, System.Int32 subdivisions)
  RVA=0x0B427D34  token=0x6000108  System.Void RebuildBroadphaseRegions(UnityEngine.Bounds worldBounds, System.Int32 subdivisions)
  RVA=0x0B422598  token=0x6000109  System.Void BakeMesh(System.Int32 meshID, System.Boolean convex)
  RVA=0x041E1670  token=0x600011A  System.Void .ctor()
  RVA=0x045B5610  token=0x600011B  System.Void get_gravity_Injected(UnityEngine.Vector3& ret)
  RVA=0x0B428F7C  token=0x600011C  System.Void set_gravity_Injected(UnityEngine.Vector3& value)
  RVA=0x032075C0  token=0x600011D  System.Void get_defaultPhysicsScene_Injected(UnityEngine.PhysicsScene& ret)
  RVA=0x0B424B40  token=0x600011E  System.Void Internal_RaycastTestV2_Injected(UnityEngine.ECSColliderResultProxy& collider, UnityEngine.Ray& ray, System.Single maxDistance, System.Boolean hitHoles, System.Boolean& hasHit, UnityEngine.RaycastHit& ret)
  RVA=0x0B424AB4  token=0x600011F  UnityEngine.RaycastHit[] Internal_RaycastAll_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Ray& ray, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B426910  token=0x6000120  UnityEngine.RaycastHit[] Query_CapsuleCastAll_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& p0, UnityEngine.Vector3& p1, System.Single radius, UnityEngine.Vector3& direction, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B426E6C  token=0x6000121  UnityEngine.RaycastHit[] Query_SphereCastAll_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& origin, System.Single radius, UnityEngine.Vector3& direction, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B425FC0  token=0x6000122  UnityEngine.Collider[] OverlapCapsule_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& point0, UnityEngine.Vector3& point1, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B425E0C  token=0x6000123  UnityEngine.ECSColliderResultProxy[] OverlapCapsuleV2_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& point0, UnityEngine.Vector3& point1, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B426698  token=0x6000124  UnityEngine.Collider[] OverlapSphere_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& position, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B426538  token=0x6000125  UnityEngine.ECSColliderResultProxy[] OverlapSphereV2_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& position, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x03C32E90  token=0x6000126  System.Void Simulate_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, System.Single step)
  RVA=0x0B426D20  token=0x6000127  System.Boolean Query_ComputePenetration_Injected(UnityEngine.Collider colliderA, UnityEngine.Vector3& positionA, UnityEngine.Quaternion& rotationA, UnityEngine.Collider colliderB, UnityEngine.Vector3& positionB, UnityEngine.Quaternion& rotationB, UnityEngine.Vector3& direction, System.Single& distance)
  RVA=0x04500B90  token=0x6000128  System.Boolean Query_ComputePenetrationV2_Injected(UnityEngine.ECSColliderResultProxy& colliderA, UnityEngine.Vector3& positionA, UnityEngine.Quaternion& rotationA, UnityEngine.ECSColliderResultProxy& colliderB, UnityEngine.Vector3& positionB, UnityEngine.Quaternion& rotationB, UnityEngine.Vector3& direction, System.Single& distance)
  RVA=0x0B426C70  token=0x6000129  System.Boolean Query_ComputeCapsulePenetration_Injected(UnityEngine.Vector3& point1, UnityEngine.Vector3& point2, System.Single radius, UnityEngine.Collider colliderB, UnityEngine.Vector3& positionB, UnityEngine.Quaternion& rotationB, UnityEngine.Vector3& direction, System.Single& distance)
  RVA=0x0B426BC0  token=0x600012A  System.Boolean Query_ComputeCapsulePenetrationV2_Injected(UnityEngine.Vector3& point1, UnityEngine.Vector3& point2, System.Single radius, UnityEngine.ECSColliderResultProxy& colliderB, UnityEngine.Vector3& positionB, UnityEngine.Quaternion& rotationB, UnityEngine.Vector3& direction, System.Single& distance)
  RVA=0x0B426DD0  token=0x600012B  System.Boolean Query_ComputeSpherePenetrationV2_Injected(UnityEngine.Vector3& center, System.Single radius, UnityEngine.ECSColliderResultProxy& colliderB, UnityEngine.Vector3& positionB, UnityEngine.Quaternion& rotationB, UnityEngine.Vector3& direction, System.Single& distance)
  RVA=0x0B426B10  token=0x600012C  System.Boolean Query_ComputeBoxPenetrationV2_Injected(UnityEngine.Vector3& center, UnityEngine.Vector3& halfExtent, UnityEngine.Quaternion& rotationA, UnityEngine.ECSColliderResultProxy& colliderB, UnityEngine.Vector3& positionB, UnityEngine.Quaternion& rotationB, UnityEngine.Vector3& direction, System.Single& distance)
  RVA=0x0B426A6C  token=0x600012D  System.Void Query_ClosestPoint_Injected(UnityEngine.Collider collider, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& point, UnityEngine.Vector3& ret)
  RVA=0x0B4269C8  token=0x600012E  System.Void Query_ClosestPointV2_Injected(UnityEngine.ECSColliderResultProxy& colliderProxy, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& point, UnityEngine.Vector3& ret)
  RVA=0x0B426F10  token=0x600012F  System.Void Query_TryGetClosestPoint_Injected(UnityEngine.ECSColliderResultProxy& colliderProxy, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& point, System.Boolean& resultIsValid, UnityEngine.Vector3& ret)
  RVA=0x0B4289B8  token=0x6000130  System.Void get_clothGravity_Injected(UnityEngine.Vector3& ret)
  RVA=0x0B428E1C  token=0x6000131  System.Void set_clothGravity_Injected(UnityEngine.Vector3& value)
  RVA=0x0B4240C0  token=0x6000132  System.Boolean CheckSphere_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& position, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x02F653B0  token=0x6000133  System.Boolean CheckCapsule_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& start, UnityEngine.Vector3& end, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B423DEC  token=0x6000134  System.Boolean CheckBox_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& center, UnityEngine.Vector3& halfExtents, UnityEngine.Quaternion& orientation, System.Int32 layermask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B425710  token=0x6000135  UnityEngine.Collider[] OverlapBox_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& center, UnityEngine.Vector3& halfExtents, UnityEngine.Quaternion& orientation, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B4254D4  token=0x6000136  UnityEngine.ECSColliderResultProxy[] OverlapBoxV2_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& center, UnityEngine.Vector3& halfExtents, UnityEngine.Quaternion& orientation, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B4249FC  token=0x6000137  UnityEngine.RaycastHit[] Internal_BoxCastAll_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& center, UnityEngine.Vector3& halfExtents, UnityEngine.Vector3& direction, UnityEngine.Quaternion& orientation, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B424C18  token=0x6000138  System.Void Internal_RebuildBroadphaseRegions_Injected(UnityEngine.Bounds& bounds, System.Int32 subdivisions)
END_CLASS

CLASS: UnityEngine.ModifiableContactPair
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x78
FIELDS:
  private           System.IntPtr                   actor  // 0x10
  private           System.IntPtr                   otherActor  // 0x18
  private           System.IntPtr                   shape  // 0x20
  private           System.IntPtr                   otherShape  // 0x28
  public            UnityEngine.Quaternion          rotation  // 0x30
  public            UnityEngine.Vector3             position  // 0x40
  public            UnityEngine.Quaternion          otherRotation  // 0x4c
  public            UnityEngine.Vector3             otherPosition  // 0x5c
  private           System.Int32                    numContacts  // 0x68
  private           System.IntPtr                   contacts  // 0x70
METHODS:
END_CLASS

CLASS: UnityEngine.PhysicMaterial
TYPE:  class
TOKEN: 0x2000010
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
METHODS:
  RVA=0x0B421764  token=0x6000139  System.Void .ctor()
  RVA=0x0B421720  token=0x600013A  System.Void Internal_CreateDynamicsMaterial(UnityEngine.PhysicMaterial mat, System.String name)
END_CLASS

CLASS: UnityEngine.ECSColliderResultProxy
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x28
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private           System.IntPtr                   m_Actor  // 0x10
  private           System.Int32                    m_EcsId  // 0x18
  private           System.UInt32                   m_EcsVersion  // 0x1c
  private           System.Int32                    m_Collider  // 0x20
  private           System.Int32                    m_ComponentIndex  // 0x24
  private   static readonly UnityEngine.ECSColliderResultProxyINVALID_PROXY  // static @ 0x0
PROPERTIES:
  componentIndex  get=0x04D872D0
  colliderEntity  get=0x0444B190
  ecsId  get=0x04D9CF30
  ecsVersion  get=0x04D9D1B0
  invalid  get=0x0B420590
  collider  get=0x03204840
  colliderInstanceId  get=0x04DBA9F0
  bIsECS  get=0x03205320
  tagConfig  get=0x034E7180
  isTrigger  get=0x03731A40
  layer  get=0x030F5140
  transform  get=0x032023B0
  transformPosition  get=0x03677460
  transformRotation  get=0x03679660
  transformLossyScale  get=0x0B4205F4
  shapePosition  get=0x031FA240
  bounds  get=0x03678230
  colliderOptions  get=0x03205340
  surfaceType  get=0x032884F0
  colliderType  get=0x0B420434
  gameObject  get=0x03F1E2A0
  hasComplexMesh  get=0x03203C00
METHODS:
  RVA=0x03204A70  token=0x600013B  System.Void SetColliderAndSetIndex(System.Int32 colliderInstanceID)
  RVA=0x04D8F540  token=0x600013C  System.Void SetComponentIndex(System.Int32 index)
  RVA=0x04D88B60  token=0x600013D  System.Void SetActor(System.IntPtr actor)
  RVA=0x04D88B40  token=0x600013E  System.Void SetEcsId(System.Int32 id)
  RVA=0x04D8CAE0  token=0x600013F  System.Void SetEcsVersion(System.UInt32 version)
  RVA=0x03205140  token=0x6000149  System.Boolean IsValid()
  RVA=0x03676B00  token=0x6000154  System.String GetColliderGoName()
  RVA=0x0406A3A0  token=0x6000157  System.Int32 GetFactoryBuildingIdentifier()
  RVA=0x0B420320  token=0x6000159  System.Boolean TryGetClosestPoint(UnityEngine.Vector3 point, UnityEngine.Vector3& result)
  RVA=0x03677BC0  token=0x600015A  UnityEngine.Vector3 ClosestPoint(UnityEngine.Vector3 position)
  RVA=0x0B4201C0  token=0x600015B  System.Boolean Raycast(UnityEngine.Ray ray, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance)
  RVA=0x0B4206FC  token=0x600015C  System.Boolean op_Equality(UnityEngine.ECSColliderResultProxy left, UnityEngine.ECSColliderResultProxy right)
  RVA=0x02F5B750  token=0x600015D  System.Boolean op_Inequality(UnityEngine.ECSColliderResultProxy left, UnityEngine.ECSColliderResultProxy right)
  RVA=0x043285A0  token=0x600015E  System.Boolean Equals(UnityEngine.ECSColliderResultProxy other)
  RVA=0x0B42011C  token=0x600015F  System.Boolean Equals(System.Object obj)
  RVA=0x03F70140  token=0x6000160  System.Int32 GetHashCode()
  RVA=0x036778A0  token=0x6000161  System.Boolean CompareTag(System.Int32 tagValue)
  RVA=0x03202310  token=0x6000162  System.Boolean IsSameCollider(UnityEngine.Collider otherCollider)
  RVA=0x04D5EB60  token=0x6000163  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.RaycastHit
TYPE:  sealed struct
TOKEN: 0x2000012
SIZE:  0x50
FIELDS:
  private           UnityEngine.Vector3             m_Point  // 0x10
  private           UnityEngine.Vector3             m_Normal  // 0x1c
  private           System.UInt32                   m_FaceID  // 0x28
  private           System.Single                   m_Distance  // 0x2c
  private           UnityEngine.Vector2             m_UV  // 0x30
  private           System.Int32                    m_Collider  // 0x38
  private           System.Int32                    m_Index  // 0x3c
  private           System.Int32                    m_EcsId  // 0x40
  private           System.UInt32                   m_EcsVersion  // 0x44
  private           System.IntPtr                   m_Actor  // 0x48
PROPERTIES:
  ecsId  get=0x04DBA970
  ecsVersion  get=0x04DBA8F0
  IsECSCollider  get=0x0406A430
  collider  get=0x03BF0940
  colliderInstanceID  get=0x04DBBEC0
  editorCollider  get=0x0B4292C4
  colliderProxy  get=0x032048B0
  point  get=0x04DBA810  set=0x04DABF80
  normal  get=0x04DBA7F0  set=0x04DBAA60
  barycentricCoordinate  get=0x04DBD2E0  set=0x0B42971C
  distance  get=0x04DBD180  set=0x04D86900
  triangleIndex  get=0x04D9D860
  componentIndex  get=0x04DBD310
  textureCoord  get=0x0B4296C0
  textureCoord2  get=0x0B429660
  transform  get=0x03BF06A0
  shapePosition  get=0x0B4295D8
  shapeRotation  get=0x0B429618
  position  get=0x0B4294C0
  rotation  get=0x0B4294F4
  scale  get=0x0B429520
  rigidbody  get=0x03BF0600
  articulationBody  get=0x0B429234
  lightmapCoord  get=0x0B429354
  textureCoord1  get=0x0B429650
METHODS:
  RVA=0x0B4291E8  token=0x6000175  UnityEngine.Vector2 CalculateRaycastTexCoord(UnityEngine.Collider collider, System.Int32 m_Index, UnityEngine.Vector2 uv, UnityEngine.Vector3 pos, System.UInt32 face, System.Int32 textcoord)
  RVA=0x0B429188  token=0x6000182  System.Void CalculateRaycastTexCoord_Injected(UnityEngine.Collider collider, System.Int32 m_Index, UnityEngine.Vector2& uv, UnityEngine.Vector3& pos, System.UInt32 face, System.Int32 textcoord, UnityEngine.Vector2& ret)
END_CLASS

CLASS: UnityEngine.Rigidbody
TYPE:  class
TOKEN: 0x2000013
SIZE:  0x18
EXTENDS: UnityEngine.Component
FIELDS:
PROPERTIES:
  velocity  get=0x0319C6B0  set=0x0B42AF9C
  angularVelocity  get=0x0319D840  set=0x0B42AA74
  drag  get=0x0319C730  set=0x0B42ABA4
  angularDrag  get=0x0B42A45C  set=0x0B42A9EC
  mass  get=0x0B42A718  set=0x0B42AD1C
  useGravity  get=0x0B42A948  set=0x0B42AF14
  maxDepenetrationVelocity  get=0x0B42A780  set=0x0B42ADA4
  isKinematic  get=0x0B42A6E4  set=0x0319DBB0
  freezeRotation  get=0x0B42A59C  set=0x0B42ABE8
  constraints  get=0x0B42A534  set=0x0B42AB20
  collisionDetectionMode  get=0x0B42A500  set=0x0B42AAE0
  centerOfMass  get=0x0B42A4D4  set=0x0B42AACC
  worldCenterOfMass  get=0x0B42A9C0
  inertiaTensorRotation  get=0x0B42A614  set=0x0B42AC70
  inertiaTensor  get=0x0B42A684  set=0x0B42ACC8
  detectCollisions  get=0x0B42A568  set=0x0B42AB60
  position  get=0x0B42A7F8  set=0x0539183C
  rotation  get=0x0B42A868  set=0x0B42AE2C
  interpolation  get=0x0B42A6B0  set=0x0B42ACDC
  solverIterations  get=0x0B42A8D4  set=0x0B42AE8C
  sleepThreshold  get=0x0B42A894  set=0x0B42AE40
  maxAngularVelocity  get=0x0B42A74C  set=0x0B42AD60
  solverVelocityIterations  get=0x0B42A914  set=0x0B42AED4
  sleepVelocity  get=0x04DBAEC0  set=0x041E1670
  sleepAngularVelocity  get=0x04DBAEC0  set=0x041E1670
  useConeFriction  get=0x04D85B20  set=0x041E1670
  solverIterationCount  get=0x0B42A8C8  set=0x0B42AE84
  solverVelocityIterationCount  get=0x0B42A908  set=0x0B42AECC
METHODS:
  RVA=0x0B42A048  token=0x600018D  System.Void SetDensity(System.Single density)
  RVA=0x0B429F74  token=0x60001AF  System.Void MovePosition(UnityEngine.Vector3 position)
  RVA=0x0B429FCC  token=0x60001B0  System.Void MoveRotation(UnityEngine.Quaternion rot)
  RVA=0x0B42A094  token=0x60001B1  System.Void Sleep()
  RVA=0x0319BF90  token=0x60001B2  System.Boolean IsSleeping()
  RVA=0x0B42A428  token=0x60001B3  System.Void WakeUp()
  RVA=0x0B429FE0  token=0x60001B4  System.Void ResetCenterOfMass()
  RVA=0x0B42A014  token=0x60001B5  System.Void ResetInertiaTensor()
  RVA=0x0B429E04  token=0x60001B6  UnityEngine.Vector3 GetRelativePointVelocity(UnityEngine.Vector3 relativePoint)
  RVA=0x0B429D7C  token=0x60001B7  UnityEngine.Vector3 GetPointVelocity(UnityEngine.Vector3 worldPoint)
  RVA=0x0B4299B8  token=0x60001BA  System.Void AddForce(UnityEngine.Vector3 force, UnityEngine.ForceMode mode)
  RVA=0x0B42998C  token=0x60001BB  System.Void AddForce(UnityEngine.Vector3 force)
  RVA=0x0B429960  token=0x60001BC  System.Void AddForce(System.Single x, System.Single y, System.Single z, UnityEngine.ForceMode mode)
  RVA=0x0B4299CC  token=0x60001BD  System.Void AddForce(System.Single x, System.Single y, System.Single z)
  RVA=0x0B429A9C  token=0x60001BE  System.Void AddRelativeForce(UnityEngine.Vector3 force, UnityEngine.ForceMode mode)
  RVA=0x0B429A48  token=0x60001BF  System.Void AddRelativeForce(UnityEngine.Vector3 force)
  RVA=0x0B429AB0  token=0x60001C0  System.Void AddRelativeForce(System.Single x, System.Single y, System.Single z, UnityEngine.ForceMode mode)
  RVA=0x0B429A74  token=0x60001C1  System.Void AddRelativeForce(System.Single x, System.Single y, System.Single z)
  RVA=0x0B429C38  token=0x60001C2  System.Void AddTorque(UnityEngine.Vector3 torque, UnityEngine.ForceMode mode)
  RVA=0x0B429C0C  token=0x60001C3  System.Void AddTorque(UnityEngine.Vector3 torque)
  RVA=0x0B429C74  token=0x60001C4  System.Void AddTorque(System.Single x, System.Single y, System.Single z, UnityEngine.ForceMode mode)
  RVA=0x0B429C4C  token=0x60001C5  System.Void AddTorque(System.Single x, System.Single y, System.Single z)
  RVA=0x0B429B88  token=0x60001C6  System.Void AddRelativeTorque(UnityEngine.Vector3 torque, UnityEngine.ForceMode mode)
  RVA=0x0B429B5C  token=0x60001C7  System.Void AddRelativeTorque(UnityEngine.Vector3 torque)
  RVA=0x0B429B30  token=0x60001C8  System.Void AddRelativeTorque(System.Single x, System.Single y, System.Single z, UnityEngine.ForceMode mode)
  RVA=0x0B429B9C  token=0x60001C9  System.Void AddRelativeTorque(System.Single x, System.Single y, System.Single z)
  RVA=0x0B4298AC  token=0x60001CA  System.Void AddForceAtPosition(UnityEngine.Vector3 force, UnityEngine.Vector3 position, UnityEngine.ForceMode mode)
  RVA=0x0B4298C4  token=0x60001CB  System.Void AddForceAtPosition(UnityEngine.Vector3 force, UnityEngine.Vector3 position)
  RVA=0x0B42981C  token=0x60001CC  System.Void AddExplosionForce(System.Single explosionForce, UnityEngine.Vector3 explosionPosition, System.Single explosionRadius, System.Single upwardsModifier, UnityEngine.ForceMode mode)
  RVA=0x0B4297DC  token=0x60001CD  System.Void AddExplosionForce(System.Single explosionForce, UnityEngine.Vector3 explosionPosition, System.Single explosionRadius, System.Single upwardsModifier)
  RVA=0x0B4297A0  token=0x60001CE  System.Void AddExplosionForce(System.Single explosionForce, UnityEngine.Vector3 explosionPosition, System.Single explosionRadius)
  RVA=0x0B429E9C  token=0x60001CF  System.Void Internal_ClosestPointOnBounds(UnityEngine.Vector3 point, UnityEngine.Vector3& outPos, System.Single& distance)
  RVA=0x0B429CA0  token=0x60001D0  UnityEngine.Vector3 ClosestPointOnBounds(UnityEngine.Vector3 position)
  RVA=0x0B42A288  token=0x60001D1  UnityEngine.RaycastHit SweepTest(UnityEngine.Vector3 direction, System.Single maxDistance, UnityEngine.QueryTriggerInteraction queryTriggerInteraction, System.Boolean& hasHit)
  RVA=0x0B42A334  token=0x60001D2  System.Boolean SweepTest(UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B42A258  token=0x60001D3  System.Boolean SweepTest(UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance)
  RVA=0x0B42A2FC  token=0x60001D4  System.Boolean SweepTest(UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo)
  RVA=0x0B429F18  token=0x60001D5  UnityEngine.RaycastHit[] Internal_SweepTestAll(UnityEngine.Vector3 direction, System.Single maxDistance, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B42A0F8  token=0x60001D6  UnityEngine.RaycastHit[] SweepTestAll(UnityEngine.Vector3 direction, System.Single maxDistance, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B42A0C8  token=0x60001D7  UnityEngine.RaycastHit[] SweepTestAll(UnityEngine.Vector3 direction, System.Single maxDistance)
  RVA=0x0B42A1BC  token=0x60001D8  UnityEngine.RaycastHit[] SweepTestAll(UnityEngine.Vector3 direction)
  RVA=0x0B42A08C  token=0x60001DD  System.Void SetMaxAngularVelocity(System.Single a)
  RVA=0x05393520  token=0x60001E4  System.Void .ctor()
  RVA=0x0319C6E0  token=0x60001E5  System.Void get_velocity_Injected(UnityEngine.Vector3& ret)
  RVA=0x0B42AF58  token=0x60001E6  System.Void set_velocity_Injected(UnityEngine.Vector3& value)
  RVA=0x0319DC00  token=0x60001E7  System.Void get_angularVelocity_Injected(UnityEngine.Vector3& ret)
  RVA=0x0B42AA30  token=0x60001E8  System.Void set_angularVelocity_Injected(UnityEngine.Vector3& value)
  RVA=0x0B42A490  token=0x60001E9  System.Void get_centerOfMass_Injected(UnityEngine.Vector3& ret)
  RVA=0x0B42AA88  token=0x60001EA  System.Void set_centerOfMass_Injected(UnityEngine.Vector3& value)
  RVA=0x0B42A97C  token=0x60001EB  System.Void get_worldCenterOfMass_Injected(UnityEngine.Vector3& ret)
  RVA=0x0B42A5D0  token=0x60001EC  System.Void get_inertiaTensorRotation_Injected(UnityEngine.Quaternion& ret)
  RVA=0x0B42AC2C  token=0x60001ED  System.Void set_inertiaTensorRotation_Injected(UnityEngine.Quaternion& value)
  RVA=0x0B42A640  token=0x60001EE  System.Void get_inertiaTensor_Injected(UnityEngine.Vector3& ret)
  RVA=0x0B42AC84  token=0x60001EF  System.Void set_inertiaTensor_Injected(UnityEngine.Vector3& value)
  RVA=0x0B42A7B4  token=0x60001F0  System.Void get_position_Injected(UnityEngine.Vector3& ret)
  RVA=0x040487C0  token=0x60001F1  System.Void set_position_Injected(UnityEngine.Vector3& value)
  RVA=0x0B42A824  token=0x60001F2  System.Void get_rotation_Injected(UnityEngine.Quaternion& ret)
  RVA=0x0B42ADE8  token=0x60001F3  System.Void set_rotation_Injected(UnityEngine.Quaternion& value)
  RVA=0x0B429F30  token=0x60001F4  System.Void MovePosition_Injected(UnityEngine.Vector3& position)
  RVA=0x0B429F88  token=0x60001F5  System.Void MoveRotation_Injected(UnityEngine.Quaternion& rot)
  RVA=0x0B429DB0  token=0x60001F6  System.Void GetRelativePointVelocity_Injected(UnityEngine.Vector3& relativePoint, UnityEngine.Vector3& ret)
  RVA=0x0B429D28  token=0x60001F7  System.Void GetPointVelocity_Injected(UnityEngine.Vector3& worldPoint, UnityEngine.Vector3& ret)
  RVA=0x0B42990C  token=0x60001F8  System.Void AddForce_Injected(UnityEngine.Vector3& force, UnityEngine.ForceMode mode)
  RVA=0x0B4299F4  token=0x60001F9  System.Void AddRelativeForce_Injected(UnityEngine.Vector3& force, UnityEngine.ForceMode mode)
  RVA=0x0B429BB8  token=0x60001FA  System.Void AddTorque_Injected(UnityEngine.Vector3& torque, UnityEngine.ForceMode mode)
  RVA=0x0B429ADC  token=0x60001FB  System.Void AddRelativeTorque_Injected(UnityEngine.Vector3& torque, UnityEngine.ForceMode mode)
  RVA=0x0B429848  token=0x60001FC  System.Void AddForceAtPosition_Injected(UnityEngine.Vector3& force, UnityEngine.Vector3& position, UnityEngine.ForceMode mode)
  RVA=0x0B42973C  token=0x60001FD  System.Void AddExplosionForce_Injected(System.Single explosionForce, UnityEngine.Vector3& explosionPosition, System.Single explosionRadius, System.Single upwardsModifier, UnityEngine.ForceMode mode)
  RVA=0x0B429E38  token=0x60001FE  System.Void Internal_ClosestPointOnBounds_Injected(UnityEngine.Vector3& point, UnityEngine.Vector3& outPos, System.Single& distance)
  RVA=0x0B42A1F4  token=0x60001FF  System.Void SweepTest_Injected(UnityEngine.Vector3& direction, System.Single maxDistance, UnityEngine.QueryTriggerInteraction queryTriggerInteraction, System.Boolean& hasHit, UnityEngine.RaycastHit& ret)
  RVA=0x0B429EB4  token=0x6000200  UnityEngine.RaycastHit[] Internal_SweepTestAll_Injected(UnityEngine.Vector3& direction, System.Single maxDistance, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
END_CLASS

CLASS: UnityEngine.ECSColliderType
TYPE:  sealed struct
TOKEN: 0x2000014
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.ECSColliderType     Default  // const
  public    static  UnityEngine.ECSColliderType     BoxCollider  // const
  public    static  UnityEngine.ECSColliderType     CapsuleCollider  // const
  public    static  UnityEngine.ECSColliderType     SphereCollider  // const
  public    static  UnityEngine.ECSColliderType     MeshCollider  // const
  public    static  UnityEngine.ECSColliderType     MultiCollider  // const
  public    static  UnityEngine.ECSColliderType     TerrainCollider  // const
  public    static  UnityEngine.ECSColliderType     ECSCollider  // const
  public    static  UnityEngine.ECSColliderType     Invalid  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Collider
TYPE:  class
TOKEN: 0x2000015
SIZE:  0x18
EXTENDS: UnityEngine.Component
FIELDS:
PROPERTIES:
  enabled  get=0x036797D0  set=0x034361B0
  attachedRigidbody  get=0x0B41FBD4
  attachedArticulationBody  get=0x0B41FBA0
  isTrigger  get=0x037318A0  set=0x0375FD00
  enableDither  get=0x0B41FC3C
  contactOffset  get=0x0B41FC08  set=0x0B41FD74
  bounds  get=0x036783C0
  hasModifiableContacts  get=0x0B41FC70  set=0x0B41FDB8
  surfaceType  get=0x0B41FD0C  set=0x0B41FE40
  tagConfig  get=0x0B41FD40  set=0x0B41FE80
  colliderOptions  get=0x0375FD90  set=0x0375FEE0
  sharedMaterial  get=0x0B41FCD8  set=0x0B41FDFC
  material  get=0x0B41FCA4  set=0x03679410
METHODS:
  RVA=0x03677D00  token=0x600020A  UnityEngine.Vector3 ClosestPoint(UnityEngine.Vector3 position)
  RVA=0x03679620  token=0x6000214  System.IntPtr GetActor()
  RVA=0x03F51100  token=0x6000215  System.Int32 GetComponentIndex()
  RVA=0x0B41F880  token=0x6000216  System.Int32 GetFactoryBuildingIdentifier()
  RVA=0x0B41FA6C  token=0x6000217  System.Void SetFactoryBuildingIdentifier(System.Int32 value)
  RVA=0x0B41F994  token=0x600021C  UnityEngine.RaycastHit Raycast(UnityEngine.Ray ray, System.Single maxDistance, System.Boolean& hasHit)
  RVA=0x0B41F9F8  token=0x600021D  System.Boolean Raycast(UnityEngine.Ray ray, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance)
  RVA=0x0B41F918  token=0x600021E  System.Void Internal_ClosestPointOnBounds(UnityEngine.Vector3 point, UnityEngine.Vector3& outPos, System.Single& distance)
  RVA=0x0B41F7F8  token=0x600021F  UnityEngine.Vector3 ClosestPointOnBounds(UnityEngine.Vector3 position)
  RVA=0x0B41F7C4  token=0x6000220  System.Boolean CanColliderComputeClosestPoint()
  RVA=0x0B41FB10  token=0x6000221  UnityEngine.Vector3 TryGetClosestPoint_Internal(UnityEngine.Vector3 point, System.Boolean& resultIsValid)
  RVA=0x0B41FB4C  token=0x6000222  System.Boolean TryGetClosestPoint(UnityEngine.Vector3 point, UnityEngine.Vector3& result)
  RVA=0x05393520  token=0x6000223  System.Void .ctor()
  RVA=0x03677D40  token=0x6000224  System.Void ClosestPoint_Injected(UnityEngine.Vector3& position, UnityEngine.Vector3& ret)
  RVA=0x036783F0  token=0x6000225  System.Void get_bounds_Injected(UnityEngine.Bounds& ret)
  RVA=0x0B41F930  token=0x6000226  System.Void Raycast_Injected(UnityEngine.Ray& ray, System.Single maxDistance, System.Boolean& hasHit, UnityEngine.RaycastHit& ret)
  RVA=0x0B41F8B4  token=0x6000227  System.Void Internal_ClosestPointOnBounds_Injected(UnityEngine.Vector3& point, UnityEngine.Vector3& outPos, System.Single& distance)
  RVA=0x0B41FAAC  token=0x6000228  System.Void TryGetClosestPoint_Internal_Injected(UnityEngine.Vector3& point, System.Boolean& resultIsValid, UnityEngine.Vector3& ret)
END_CLASS

CLASS: UnityEngine.ECSCollider
TYPE:  class
TOKEN: 0x2000016
SIZE:  0x18
FIELDS:
  private           System.IntPtr                   self  // 0x10
  public    static  UnityEngine.ECSCollider.CollisionCallbackcollisionEnterCallback  // static @ 0x0
  public    static  UnityEngine.ECSCollider.CollisionCallbackcollisionStayCallback  // static @ 0x8
  public    static  UnityEngine.ECSCollider.CollisionCallbackcollisionExitCallback  // static @ 0x10
  public    static  UnityEngine.ECSCollider.CollisionCallbacktriggerEnterCallback  // static @ 0x18
  public    static  UnityEngine.ECSCollider.CollisionCallbacktriggerStayCallback  // static @ 0x20
  public    static  UnityEngine.ECSCollider.CollisionCallbacktriggerExitCallback  // static @ 0x28
METHODS:
  RVA=0x0B42098C  token=0x6000229  System.IntPtr CreateActor(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, System.UInt32 layer, System.Int32 tagInt, System.Boolean movable)
  RVA=0x0B4209AC  token=0x600022A  System.Void DestroyActor(System.Int32 id, System.UInt32 version)
  RVA=0x034C79D0  token=0x600022B  System.IntPtr Decode(System.IntPtr data, System.Int32 length, System.IntPtr meshes, System.Int32 meshesCount, UnityEngine.Texture2D heightmaps)
  RVA=0x034C7940  token=0x600022C  System.IntPtr Decode(System.IntPtr data, System.Int32 length, Unity.Collections.NativeArray<System.Int32> meshInstanceIds, UnityEngine.Texture2D heightmap)
  RVA=0x05392C94  token=0x600022D  System.Void RecordEntity(System.IntPtr actor, UnityEngine.HyperGryph.ECS.Entity entity)
  RVA=0x034C7A40  token=0x600022E  System.Int32 GetId(System.IntPtr actor)
  RVA=0x034C7A80  token=0x600022F  System.UInt32 GetVersion(System.IntPtr actor)
  RVA=0x032052C0  token=0x6000230  System.Boolean IsValid(System.Int32 id, System.UInt32 version, System.IntPtr actor)
  RVA=0x0444B1A0  token=0x6000231  UnityEngine.HyperGryph.ECS.Entity GetEntity(System.Int32 id, System.UInt32 version)
  RVA=0x03678E80  token=0x6000232  UnityEngine.Vector3 GetPosition(System.Int32 ecsId, System.UInt32 ecsVersion)
  RVA=0x044C6EF0  token=0x6000233  UnityEngine.Vector3 GetShapePosition(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 index)
  RVA=0x05392CB0  token=0x6000234  System.Void SetPosition(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3 position)
  RVA=0x0B420A4C  token=0x6000235  UnityEngine.Quaternion GetShapeRotation(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 index)
  RVA=0x048150F0  token=0x6000236  UnityEngine.Quaternion GetRotation(System.Int32 ecsId, System.UInt32 ecsVersion)
  RVA=0x05392CC4  token=0x6000237  System.Void SetRotation(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Quaternion rotation)
  RVA=0x0B4207EC  token=0x6000238  System.Void AttachBox(System.Int32 id, System.UInt32 version, UnityEngine.Vector3 halfExtent, UnityEngine.Vector3 localPosition, UnityEngine.Quaternion localRotation, System.Int32 surfaceType, System.Int32 colliderOption, System.Int32 tag, System.Boolean isTrigger, System.Boolean isPrimitive)
  RVA=0x03203C80  token=0x6000239  System.Boolean HasComplexMesh(System.Int32 id, System.UInt32 version)
  RVA=0x0B420D28  token=0x600023A  UnityEngine.RaycastHit Raycast_Internal(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Ray ray, System.Single maxDistance, System.Boolean hitHoles, System.Boolean& hasHit)
  RVA=0x0B420D9C  token=0x600023B  System.Boolean Raycast(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Ray ray, System.Single maxDistance, System.Boolean hitHoles, UnityEngine.RaycastHit& hitInfo)
  RVA=0x041183B0  token=0x600023C  UnityEngine.RaycastHit RaycastComplexMesh_Internal(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Ray ray, System.Single maxDistance, System.Boolean hitHoles, System.Boolean& hasHit)
  RVA=0x041182B0  token=0x600023D  System.Boolean RaycastComplexMesh(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Ray ray, System.Single maxDistance, System.Boolean hitHoles, UnityEngine.RaycastHit& hitInfo)
  RVA=0x030F0360  token=0x600023E  UnityEngine.RaycastHit CapsuleCastComplexMesh_Internal(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Boolean& hasHit)
  RVA=0x030F0210  token=0x600023F  System.Boolean CapsuleCastComplexMesh(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, UnityEngine.RaycastHit& hitInfo)
  RVA=0x030EF280  token=0x6000240  UnityEngine.RaycastHit SphereCastComplexMesh_Internal(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3 center, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Boolean& hasHit)
  RVA=0x030EF160  token=0x6000241  System.Boolean SphereCastComplexMesh(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3 center, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, UnityEngine.RaycastHit& hitInfo)
  RVA=0x0B42084C  token=0x6000242  System.Boolean CanColliderComputeClosestPoint(System.Int32 ecsId, System.UInt32 ecsVersion)
  RVA=0x03288490  token=0x6000243  UnityEngine.ColliderSurfaceType GetSurfaceTypeAt(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 index)
  RVA=0x03205480  token=0x6000244  System.Int32 GetColliderOptionAt(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 index)
  RVA=0x034E7200  token=0x6000245  System.Int32 GetTagConfigAt(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 index)
  RVA=0x043F3060  token=0x6000246  System.Boolean GetIsTrigger(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 index)
  RVA=0x03678440  token=0x6000247  UnityEngine.Bounds GetBounds(System.Int32 ecsId, System.UInt32 ecsVersion)
  RVA=0x0B420E88  token=0x6000248  UnityEngine.Vector3 TryGetClosestPoint_Internal(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3 point, System.Boolean& resultIsValid)
  RVA=0x0B420EC8  token=0x6000249  System.Boolean TryGetClosestPoint(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3 point, UnityEngine.Vector3& result)
  RVA=0x0B4208EC  token=0x600024A  UnityEngine.Vector3 ClosestPoint(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3 position)
  RVA=0x03675AF0  token=0x600024B  System.Boolean CompareTag(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 tagValue)
  RVA=0x030F5240  token=0x600024C  System.Int32 GetLayer(System.Int32 ecsId, System.UInt32 ecsVersion)
  RVA=0x0406A3E0  token=0x600024D  System.Int32 GetFactoryBuildingIdentifier(System.Int32 ecsId, System.UInt32 ecsVersion)
  RVA=0x034C4BF0  token=0x600024E  System.Void SetFactoryBuildingIdentifier(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 identifier)
  RVA=0x0B420A88  token=0x600024F  System.Void OnCollisionEnterV2(UnityEngine.ECSColliderResultProxy thisCollider, UnityEngine.ECSColliderResultProxy otherCollider)
  RVA=0x0B420BA8  token=0x6000250  System.Void OnCollisionStayV2(UnityEngine.ECSColliderResultProxy thisCollider, UnityEngine.ECSColliderResultProxy otherCollider)
  RVA=0x0B420B18  token=0x6000251  System.Void OnCollisionExitV2(UnityEngine.ECSColliderResultProxy thisCollider, UnityEngine.ECSColliderResultProxy otherCollider)
  RVA=0x0484F800  token=0x6000252  System.Void OnTriggerEnterV2(UnityEngine.ECSColliderResultProxy thisCollider, UnityEngine.ECSColliderResultProxy otherCollider)
  RVA=0x0B420C38  token=0x6000253  System.Void OnTriggerStayV2(UnityEngine.ECSColliderResultProxy thisCollider, UnityEngine.ECSColliderResultProxy otherCollider)
  RVA=0x04CA3010  token=0x6000254  System.Void OnTriggerExitV2(UnityEngine.ECSColliderResultProxy thisCollider, UnityEngine.ECSColliderResultProxy otherCollider)
  RVA=0x034A2210  token=0x6000255  System.Void ToggleCollision(System.Int32 id, System.UInt32 version, System.Boolean enableCollision)
  RVA=0x0B420928  token=0x6000256  System.IntPtr CreateActor_Injected(UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, System.UInt32 layer, System.Int32 tagInt, System.Boolean movable)
  RVA=0x04258D10  token=0x6000257  System.Void RecordEntity_Injected(System.IntPtr actor, UnityEngine.HyperGryph.ECS.Entity& entity)
  RVA=0x0444B200  token=0x6000258  System.Void GetEntity_Injected(System.Int32 id, System.UInt32 version, UnityEngine.HyperGryph.ECS.Entity& ret)
  RVA=0x03678EB0  token=0x6000259  System.Void GetPosition_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3& ret)
  RVA=0x044C6F30  token=0x600025A  System.Void GetShapePosition_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 index, UnityEngine.Vector3& ret)
  RVA=0x04191270  token=0x600025B  System.Void SetPosition_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3& position)
  RVA=0x0B4209EC  token=0x600025C  System.Void GetShapeRotation_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 index, UnityEngine.Quaternion& ret)
  RVA=0x04815120  token=0x600025D  System.Void GetRotation_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Quaternion& ret)
  RVA=0x04191210  token=0x600025E  System.Void SetRotation_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Quaternion& rotation)
  RVA=0x0B42078C  token=0x600025F  System.Void AttachBox_Injected(System.Int32 id, System.UInt32 version, UnityEngine.Vector3& halfExtent, UnityEngine.Vector3& localPosition, UnityEngine.Quaternion& localRotation, System.Int32 surfaceType, System.Int32 colliderOption, System.Int32 tag, System.Boolean isTrigger, System.Boolean isPrimitive)
  RVA=0x0B420CC8  token=0x6000260  System.Void Raycast_Internal_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Ray& ray, System.Single maxDistance, System.Boolean hitHoles, System.Boolean& hasHit, UnityEngine.RaycastHit& ret)
  RVA=0x04118420  token=0x6000261  System.Void RaycastComplexMesh_Internal_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Ray& ray, System.Single maxDistance, System.Boolean hitHoles, System.Boolean& hasHit, UnityEngine.RaycastHit& ret)
  RVA=0x030F03F0  token=0x6000262  System.Void CapsuleCastComplexMesh_Internal_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3& p0, UnityEngine.Vector3& p1, System.Single radius, UnityEngine.Vector3& direction, System.Single maxDistance, System.Boolean& hasHit, UnityEngine.RaycastHit& ret)
  RVA=0x030EF300  token=0x6000263  System.Void SphereCastComplexMesh_Internal_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3& center, System.Single radius, UnityEngine.Vector3& direction, System.Single maxDistance, System.Boolean& hasHit, UnityEngine.RaycastHit& ret)
  RVA=0x03678480  token=0x6000264  System.Void GetBounds_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Bounds& ret)
  RVA=0x0B420E28  token=0x6000265  System.Void TryGetClosestPoint_Internal_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3& point, System.Boolean& resultIsValid, UnityEngine.Vector3& ret)
  RVA=0x0B42088C  token=0x6000266  System.Void ClosestPoint_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3& position, UnityEngine.Vector3& ret)
END_CLASS

CLASS: UnityEngine.CharacterController
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x18
EXTENDS: UnityEngine.Collider
FIELDS:
PROPERTIES:
  isGrounded  get=0x0B41F790
METHODS:
END_CLASS

CLASS: UnityEngine.MultiCollider
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x18
EXTENDS: UnityEngine.Collider
FIELDS:
PROPERTIES:
  ComplexMesh  get=0x0B4216EC
METHODS:
  RVA=0x0B421104  token=0x600026A  System.Void AppendBoxCollider(UnityEngine.BoxCollider boxCollider)
  RVA=0x0B421148  token=0x600026B  System.Void AppendCapsuleCollider(UnityEngine.CapsuleCollider collider)
  RVA=0x0B42118C  token=0x600026C  System.Void AppendSphereCollider(UnityEngine.SphereCollider collider)
  RVA=0x0B42141C  token=0x600026D  UnityEngine.ColliderSurfaceType GetSurfaceTypeAt(System.Int32 index)
  RVA=0x0B42145C  token=0x600026E  System.Int32 GetTagConfigAt(System.Int32 index)
  RVA=0x03C828D0  token=0x600026F  System.Int32 GetGameplayOptionAt(System.Int32 index)
  RVA=0x0B4213DC  token=0x6000270  System.Boolean GetIsTriggerAt(System.Int32 index)
  RVA=0x0B421518  token=0x6000271  System.Void SetGameplayOptionAt(System.Int32 index, System.Int32 value)
  RVA=0x0B421374  token=0x6000272  System.Void Flush()
  RVA=0x0B421500  token=0x6000274  System.Boolean RaycastComplexMesh(UnityEngine.Ray ray, System.Single maxDistance, UnityEngine.RaycastHit& hit)
  RVA=0x0B421234  token=0x6000275  UnityEngine.RaycastHit CapsuleCastComplexMesh_Internal(UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Boolean& hasHit)
  RVA=0x0B4212C4  token=0x6000276  System.Boolean CapsuleCastComplexMesh(UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, UnityEngine.RaycastHit& hit)
  RVA=0x0B4215CC  token=0x6000277  UnityEngine.RaycastHit SphereCastComplexMesh_Internal(UnityEngine.Vector3 center, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Boolean& hasHit)
  RVA=0x0B42164C  token=0x6000278  System.Boolean SphereCastComplexMesh(UnityEngine.Vector3 center, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, UnityEngine.RaycastHit& hit)
  RVA=0x0B4213A8  token=0x6000279  System.Int32 GetCountWithoutComplexMesh()
  RVA=0x0B42149C  token=0x600027A  System.Boolean RaycastComplexMesh_Injected(UnityEngine.Ray& ray, System.Single maxDistance, UnityEngine.RaycastHit& hit)
  RVA=0x0B4211D0  token=0x600027B  System.Void CapsuleCastComplexMesh_Internal_Injected(UnityEngine.Vector3& p0, UnityEngine.Vector3& p1, System.Single radius, UnityEngine.Vector3& direction, System.Single maxDistance, System.Boolean& hasHit, UnityEngine.RaycastHit& ret)
  RVA=0x0B421568  token=0x600027C  System.Void SphereCastComplexMesh_Internal_Injected(UnityEngine.Vector3& center, System.Single radius, UnityEngine.Vector3& direction, System.Single maxDistance, System.Boolean& hasHit, UnityEngine.RaycastHit& ret)
END_CLASS

CLASS: UnityEngine.MeshCollider
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x18
EXTENDS: UnityEngine.Collider
FIELDS:
PROPERTIES:
  sharedMesh  get=0x0B42104C  set=0x03C57D60
  convex  get=0x0B420FE4  set=0x0B421080
  cookingOptions  get=0x0B421018  set=0x0B4210C4
  smoothSphereCollisions  get=0x04D9AE10  set=0x041E1670
  skinWidth  get=0x04DBAEC0  set=0x041E1670
  inflateMesh  get=0x04D85B20  set=0x041E1670
METHODS:
  RVA=0x0B420FB0  token=0x6000283  System.Boolean NeedsReadWriteEnabled()
  RVA=0x05393520  token=0x600028A  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.CapsuleCollider
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x18
EXTENDS: UnityEngine.Collider
FIELDS:
PROPERTIES:
  center  get=0x031FC060  set=0x05393430
  radius  get=0x031FC020  set=0x03679320
  height  get=0x031FBFE0  set=0x03679370
  direction  get=0x031FBFA0  set=0x036793C0
METHODS:
  RVA=0x031FC0C0  token=0x6000293  System.Void get_center_Injected(UnityEngine.Vector3& ret)
  RVA=0x04486C50  token=0x6000294  System.Void set_center_Injected(UnityEngine.Vector3& value)
END_CLASS

CLASS: UnityEngine.BoxCollider
TYPE:  class
TOKEN: 0x200001C
SIZE:  0x18
EXTENDS: UnityEngine.Collider
FIELDS:
PROPERTIES:
  center  get=0x0B41F41C  set=0x05396B24
  size  get=0x03FB9410  set=0x05396B10
METHODS:
  RVA=0x0B41F3D8  token=0x6000299  System.Void get_center_Injected(UnityEngine.Vector3& ret)
  RVA=0x036CCA20  token=0x600029A  System.Void set_center_Injected(UnityEngine.Vector3& value)
  RVA=0x03FB9440  token=0x600029B  System.Void get_size_Injected(UnityEngine.Vector3& ret)
  RVA=0x036CC9D0  token=0x600029C  System.Void set_size_Injected(UnityEngine.Vector3& value)
END_CLASS

CLASS: UnityEngine.SphereCollider
TYPE:  class
TOKEN: 0x200001D
SIZE:  0x18
EXTENDS: UnityEngine.Collider
FIELDS:
PROPERTIES:
  center  get=0x0B42AFF4  set=0x0B42B054
  radius  get=0x0B42B020  set=0x0375FE40
METHODS:
  RVA=0x0B42AFB0  token=0x60002A1  System.Void get_center_Injected(UnityEngine.Vector3& ret)
  RVA=0x04D30C40  token=0x60002A2  System.Void set_center_Injected(UnityEngine.Vector3& value)
END_CLASS

CLASS: UnityEngine.Joint
TYPE:  class
TOKEN: 0x200001E
SIZE:  0x18
EXTENDS: UnityEngine.Component
FIELDS:
PROPERTIES:
  connectedBody  get=0x0B420F24
  connectedAnchor  set=0x0B420F9C
METHODS:
  RVA=0x0B420F58  token=0x60002A5  System.Void set_connectedAnchor_Injected(UnityEngine.Vector3& value)
END_CLASS

CLASS: UnityEngine.ContactPoint
TYPE:  sealed struct
TOKEN: 0x200001F
SIZE:  0x34
FIELDS:
  private           UnityEngine.Vector3             m_Point  // 0x10
  private           UnityEngine.Vector3             m_Normal  // 0x1c
  private           System.Int32                    m_ThisColliderInstanceID  // 0x28
  private           System.Int32                    m_OtherColliderInstanceID  // 0x2c
  private           System.Single                   m_Separation  // 0x30
PROPERTIES:
  point  get=0x04DBA810
  normal  get=0x04DBA7F0
  thisCollider  get=0x0B4200F0
  otherCollider  get=0x0B4200E4
METHODS:
  RVA=0x0B4200B0  token=0x60002AA  UnityEngine.Collider GetColliderByInstanceID(System.Int32 instanceID)
END_CLASS

CLASS: UnityEngine.PhysicsScene
TYPE:  sealed struct
TOKEN: 0x2000020
SIZE:  0x14
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private           System.Int32                    m_Handle  // 0x10
METHODS:
  RVA=0x0B4224E8  token=0x60002AB  System.String ToString()
  RVA=0x04DBA750  token=0x60002AC  System.Int32 GetHashCode()
  RVA=0x0B4219F4  token=0x60002AD  System.Boolean Equals(System.Object other)
  RVA=0x02EED260  token=0x60002AE  System.Boolean Equals(UnityEngine.PhysicsScene other)
  RVA=0x0B4223F0  token=0x60002AF  System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B421DC8  token=0x60002B0  System.Boolean Internal_RaycastTest(UnityEngine.PhysicsScene physicsScene, UnityEngine.Ray ray, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x03206EF0  token=0x60002B1  System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x053918B8  token=0x60002B2  System.Boolean Internal_Raycast(UnityEngine.PhysicsScene physicsScene, UnityEngine.Ray ray, System.Single maxDistance, UnityEngine.RaycastHit& hit, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x03206BF0  token=0x60002B3  System.Int32 Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] raycastHits, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x05391924  token=0x60002B4  System.Int32 Internal_RaycastNonAlloc(UnityEngine.PhysicsScene physicsScene, UnityEngine.Ray ray, UnityEngine.RaycastHit[] raycastHits, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B422390  token=0x60002B5  System.Boolean Query_CapsuleCast(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, UnityEngine.RaycastHit& hitInfo, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B421C44  token=0x60002B6  System.Boolean Internal_CapsuleCast(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B421958  token=0x60002B7  System.Boolean CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x053910C8  token=0x60002B8  System.Int32 Internal_CapsuleCastNonAlloc(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] raycastHits, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x030F20F0  token=0x60002B9  System.Int32 CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B421BE4  token=0x60002BA  System.Int32 Internal_CapsuleCastNonAllocV2(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, System.Single radius, UnityEngine.Vector3 diretion, UnityEngine.ECSColliderResultProxy[] proxies, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B421864  token=0x60002BB  System.Int32 CapsuleCastV2(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.ECSColliderResultProxy[] proxies, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B421FEC  token=0x60002BC  System.Int32 OverlapCapsuleNonAlloc_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B42202C  token=0x60002BD  System.Int32 OverlapCapsule(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x05391870  token=0x60002BE  System.Int32 OverlapCapsuleNonAlloc_InternalV2(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x031FFB20  token=0x60002BF  System.Int32 OverlapCapsuleV2(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x053936F0  token=0x60002C0  System.Boolean Query_SphereCast(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, UnityEngine.RaycastHit& hitInfo, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x03730780  token=0x60002C1  System.Boolean Internal_SphereCast(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x037305E0  token=0x60002C2  System.Boolean SphereCast(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x05391128  token=0x60002C3  System.Int32 Internal_SphereCastNonAlloc(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] raycastHits, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x030F5A30  token=0x60002C4  System.Int32 SphereCast(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B422174  token=0x60002C5  System.Int32 OverlapSphereNonAlloc_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 position, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B4221A4  token=0x60002C6  System.Int32 OverlapSphere(UnityEngine.Vector3 position, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x05395C38  token=0x60002C7  System.Int32 OverlapSphereNonAllocV2_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 position, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x03D7C100  token=0x60002C8  System.Int32 OverlapSphereV2(UnityEngine.Vector3 position, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B4222CC  token=0x60002C9  System.Boolean Query_BoxCast(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation, System.Single maxDistance, UnityEngine.RaycastHit& outHit, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B421A58  token=0x60002CA  System.Boolean Internal_BoxCast(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B4217B8  token=0x60002CB  System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B421E54  token=0x60002CC  System.Int32 OverlapBoxNonAlloc_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Collider[] results, UnityEngine.Quaternion orientation, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B421E94  token=0x60002CD  System.Int32 OverlapBox(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Collider[] results, UnityEngine.Quaternion orientation, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x05397930  token=0x60002CE  System.Int32 OverlapBoxNonAllocV2_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.ECSColliderResultProxy[] results, UnityEngine.Quaternion orientation, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x045ADDF0  token=0x60002CF  System.Int32 OverlapBoxV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.ECSColliderResultProxy[] results, UnityEngine.Quaternion orientation, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x05390DC0  token=0x60002D0  System.Int32 Internal_BoxCastNonAlloc(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] raycastHits, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x03027940  token=0x60002D1  System.Int32 BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B421D64  token=0x60002D2  System.Boolean Internal_RaycastTest_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Ray& ray, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x03C5DB50  token=0x60002D3  System.Boolean Internal_Raycast_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Ray& ray, System.Single maxDistance, UnityEngine.RaycastHit& hit, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x03C6CD70  token=0x60002D4  System.Int32 Internal_RaycastNonAlloc_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Ray& ray, UnityEngine.RaycastHit[] raycastHits, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B42232C  token=0x60002D5  System.Boolean Query_CapsuleCast_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& point1, UnityEngine.Vector3& point2, System.Single radius, UnityEngine.Vector3& direction, System.Single maxDistance, UnityEngine.RaycastHit& hitInfo, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x03EE4C10  token=0x60002D6  System.Int32 Internal_CapsuleCastNonAlloc_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& p0, UnityEngine.Vector3& p1, System.Single radius, UnityEngine.Vector3& direction, UnityEngine.RaycastHit[] raycastHits, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B421B80  token=0x60002D7  System.Int32 Internal_CapsuleCastNonAllocV2_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& p0, UnityEngine.Vector3& p1, System.Single radius, UnityEngine.Vector3& diretion, UnityEngine.ECSColliderResultProxy[] proxies, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B421F88  token=0x60002D8  System.Int32 OverlapCapsuleNonAlloc_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& point0, UnityEngine.Vector3& point1, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x03DEFB30  token=0x60002D9  System.Int32 OverlapCapsuleNonAlloc_InternalV2_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& point0, UnityEngine.Vector3& point1, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x03F08EB0  token=0x60002DA  System.Boolean Query_SphereCast_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& origin, System.Single radius, UnityEngine.Vector3& direction, System.Single maxDistance, UnityEngine.RaycastHit& hitInfo, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x03D6D910  token=0x60002DB  System.Int32 Internal_SphereCastNonAlloc_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& origin, System.Single radius, UnityEngine.Vector3& direction, UnityEngine.RaycastHit[] raycastHits, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B422110  token=0x60002DC  System.Int32 OverlapSphereNonAlloc_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& position, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0438EDE0  token=0x60002DD  System.Int32 OverlapSphereNonAllocV2_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& position, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B422268  token=0x60002DE  System.Boolean Query_BoxCast_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& center, UnityEngine.Vector3& halfExtents, UnityEngine.Vector3& direction, UnityEngine.Quaternion& orientation, System.Single maxDistance, UnityEngine.RaycastHit& outHit, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x0B421DF0  token=0x60002DF  System.Int32 OverlapBoxNonAlloc_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& center, UnityEngine.Vector3& halfExtents, UnityEngine.Collider[] results, UnityEngine.Quaternion& orientation, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x045ADEA0  token=0x60002E0  System.Int32 OverlapBoxNonAllocV2_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& center, UnityEngine.Vector3& halfExtents, UnityEngine.ECSColliderResultProxy[] results, UnityEngine.Quaternion& orientation, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
  RVA=0x03CE61C0  token=0x60002E1  System.Int32 Internal_BoxCastNonAlloc_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& center, UnityEngine.Vector3& halfExtents, UnityEngine.Vector3& direction, UnityEngine.RaycastHit[] raycastHits, UnityEngine.Quaternion& orientation, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction)
END_CLASS

CLASS: UnityEngine.QueryParameters
TYPE:  sealed struct
TOKEN: 0x2000021
SIZE:  0x1C
FIELDS:
  public            System.Int32                    layerMask  // 0x10
  public            System.Boolean                  hitMultipleFaces  // 0x14
  public            System.Boolean                  hitBackfaces  // 0x15
  public            UnityEngine.QueryTriggerInteractionhitTriggers  // 0x18
METHODS:
  RVA=0x04DBD2A0  token=0x60002E2  System.Void .ctor(System.Int32 layerMask, System.Boolean hitMultipleFaces, UnityEngine.QueryTriggerInteraction hitTriggers, System.Boolean hitBackfaces)
END_CLASS

CLASS: UnityEngine.RaycastCommand
TYPE:  sealed struct
TOKEN: 0x2000022
SIZE:  0x3C
FIELDS:
  private           UnityEngine.Vector3             <from>k__BackingField  // 0x10
  private           UnityEngine.Vector3             <direction>k__BackingField  // 0x1c
  private           System.Single                   <distance>k__BackingField  // 0x28
  private           UnityEngine.PhysicsScene        <physicsScene>k__BackingField  // 0x2c
  public            UnityEngine.QueryParameters     queryParameters  // 0x30
PROPERTIES:
  from  get=0x04D93250  set=0x04DABF80
  direction  get=0x04DBD2C0  set=0x04DBAA60
  distance  get=0x04D86380  set=0x04D86390
  physicsScene  set=0x04D86320
METHODS:
  RVA=0x0309E080  token=0x60002E3  System.Void .ctor(UnityEngine.Vector3 from, UnityEngine.Vector3 direction, UnityEngine.QueryParameters queryParameters, System.Single distance)
  RVA=0x0309DDB0  token=0x60002EB  Unity.Jobs.JobHandle ScheduleBatch(Unity.Collections.NativeArray<UnityEngine.RaycastCommand> commands, Unity.Collections.NativeArray<UnityEngine.RaycastHit> results, System.Int32 minCommandsPerJob, System.Int32 maxHits, Unity.Jobs.JobHandle dependsOn)
  RVA=0x0309DF90  token=0x60002EC  Unity.Jobs.JobHandle ScheduleBatch(Unity.Collections.NativeArray<UnityEngine.RaycastCommand> commands, Unity.Collections.NativeArray<UnityEngine.RaycastHit> results, System.Int32 minCommandsPerJob, Unity.Jobs.JobHandle dependsOn)
  RVA=0x0309DEF0  token=0x60002ED  Unity.Jobs.JobHandle ScheduleRaycastBatch(Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters& parameters, System.Void* commands, System.Int32 commandLen, System.Void* result, System.Int32 resultLen, System.Int32 minCommandsPerJob, System.Int32 maxHits)
  RVA=0x0309DD40  token=0x60002EE  System.Void ScheduleRaycastBatch_Injected(Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters& parameters, System.Void* commands, System.Int32 commandLen, System.Void* result, System.Int32 resultLen, System.Int32 minCommandsPerJob, System.Int32 maxHits, Unity.Jobs.JobHandle& ret)
END_CLASS

CLASS: UnityEngine.SpherecastCommand
TYPE:  sealed struct
TOKEN: 0x2000023
SIZE:  0x40
FIELDS:
  private           UnityEngine.Vector3             <origin>k__BackingField  // 0x10
  private           System.Single                   <radius>k__BackingField  // 0x1c
  private           UnityEngine.Vector3             <direction>k__BackingField  // 0x20
  private           System.Single                   <distance>k__BackingField  // 0x2c
  private           UnityEngine.PhysicsScene        <physicsScene>k__BackingField  // 0x30
  public            UnityEngine.QueryParameters     queryParameters  // 0x34
PROPERTIES:
  origin  set=0x04DABF80
  radius  set=0x04DACD70
  direction  set=0x04D8C970
  distance  set=0x04D86900
  physicsScene  set=0x04D86330
METHODS:
  RVA=0x0B42B33C  token=0x60002EF  System.Void .ctor(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.QueryParameters queryParameters, System.Single distance)
  RVA=0x0B42B0DC  token=0x60002F5  Unity.Jobs.JobHandle ScheduleBatch(Unity.Collections.NativeArray<UnityEngine.SpherecastCommand> commands, Unity.Collections.NativeArray<UnityEngine.RaycastHit> results, System.Int32 minCommandsPerJob, System.Int32 maxHits, Unity.Jobs.JobHandle dependsOn)
  RVA=0x0B42B068  token=0x60002F6  Unity.Jobs.JobHandle ScheduleBatch(Unity.Collections.NativeArray<UnityEngine.SpherecastCommand> commands, Unity.Collections.NativeArray<UnityEngine.RaycastHit> results, System.Int32 minCommandsPerJob, Unity.Jobs.JobHandle dependsOn)
  RVA=0x0B42B2D0  token=0x60002F7  Unity.Jobs.JobHandle ScheduleSpherecastBatch(Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters& parameters, System.Void* commands, System.Int32 commandLen, System.Void* result, System.Int32 resultLen, System.Int32 minCommandsPerJob, System.Int32 maxHits)
  RVA=0x0B42B26C  token=0x60002F8  System.Void ScheduleSpherecastBatch_Injected(Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters& parameters, System.Void* commands, System.Int32 commandLen, System.Void* result, System.Int32 resultLen, System.Int32 minCommandsPerJob, System.Int32 maxHits, Unity.Jobs.JobHandle& ret)
END_CLASS

CLASS: UnityEngine.BoxcastCommand
TYPE:  sealed struct
TOKEN: 0x2000024
SIZE:  0x58
FIELDS:
  private           UnityEngine.Vector3             <center>k__BackingField  // 0x10
  private           UnityEngine.Vector3             <halfExtents>k__BackingField  // 0x1c
  private           UnityEngine.Quaternion          <orientation>k__BackingField  // 0x28
  private           UnityEngine.Vector3             <direction>k__BackingField  // 0x38
  private           System.Single                   <distance>k__BackingField  // 0x44
  private           UnityEngine.PhysicsScene        <physicsScene>k__BackingField  // 0x48
  public            UnityEngine.QueryParameters     queryParameters  // 0x4c
PROPERTIES:
  center  set=0x04DABF80
  halfExtents  set=0x04DBAA60
  orientation  set=0x04D890D0
  direction  set=0x04D90C20
  distance  set=0x04D8E060
  physicsScene  set=0x04D86430
METHODS:
  RVA=0x0B41F71C  token=0x60002F9  System.Void .ctor(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, UnityEngine.Vector3 direction, UnityEngine.QueryParameters queryParameters, System.Single distance)
  RVA=0x0B41F4BC  token=0x6000300  Unity.Jobs.JobHandle ScheduleBatch(Unity.Collections.NativeArray<UnityEngine.BoxcastCommand> commands, Unity.Collections.NativeArray<UnityEngine.RaycastHit> results, System.Int32 minCommandsPerJob, System.Int32 maxHits, Unity.Jobs.JobHandle dependsOn)
  RVA=0x0B41F448  token=0x6000301  Unity.Jobs.JobHandle ScheduleBatch(Unity.Collections.NativeArray<UnityEngine.BoxcastCommand> commands, Unity.Collections.NativeArray<UnityEngine.RaycastHit> results, System.Int32 minCommandsPerJob, Unity.Jobs.JobHandle dependsOn)
  RVA=0x0B41F6B0  token=0x6000302  Unity.Jobs.JobHandle ScheduleBoxcastBatch(Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters& parameters, System.Void* commands, System.Int32 commandLen, System.Void* result, System.Int32 resultLen, System.Int32 minCommandsPerJob, System.Int32 maxHits)
  RVA=0x0B41F64C  token=0x6000303  System.Void ScheduleBoxcastBatch_Injected(Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters& parameters, System.Void* commands, System.Int32 commandLen, System.Void* result, System.Int32 resultLen, System.Int32 minCommandsPerJob, System.Int32 maxHits, Unity.Jobs.JobHandle& ret)
END_CLASS

