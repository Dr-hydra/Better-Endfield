// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.PhysicsModule.dll
// Classes:  36
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000017  // size: 0x80
    public sealed class CollisionCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B41FF0C  token: 0x6000267
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x0B41FEC0  token: 0x6000268
        public virtual System.Void Invoke(UnityEngine.ECSColliderResultProxy thisCollider, UnityEngine.ECSColliderResultProxy otherCollider) { }

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

    // TypeToken: 0x2000004  // size: 0x14
    public sealed struct RigidbodyConstraints
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.RigidbodyConstraints None;  // const
        public static UnityEngine.RigidbodyConstraints FreezePositionX;  // const
        public static UnityEngine.RigidbodyConstraints FreezePositionY;  // const
        public static UnityEngine.RigidbodyConstraints FreezePositionZ;  // const
        public static UnityEngine.RigidbodyConstraints FreezeRotationX;  // const
        public static UnityEngine.RigidbodyConstraints FreezeRotationY;  // const
        public static UnityEngine.RigidbodyConstraints FreezeRotationZ;  // const
        public static UnityEngine.RigidbodyConstraints FreezePosition;  // const
        public static UnityEngine.RigidbodyConstraints FreezeRotation;  // const
        public static UnityEngine.RigidbodyConstraints FreezeAll;  // const

    }

    // TypeToken: 0x2000005  // size: 0x14
    public sealed struct ForceMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ForceMode Force;  // const
        public static UnityEngine.ForceMode Acceleration;  // const
        public static UnityEngine.ForceMode Impulse;  // const
        public static UnityEngine.ForceMode VelocityChange;  // const

    }

    // TypeToken: 0x2000006  // size: 0x14
    public sealed struct MeshColliderCookingOptions
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.MeshColliderCookingOptions None;  // const
        public static UnityEngine.MeshColliderCookingOptions InflateConvexMesh;  // const
        public static UnityEngine.MeshColliderCookingOptions CookForFasterSimulation;  // const
        public static UnityEngine.MeshColliderCookingOptions EnableMeshCleaning;  // const
        public static UnityEngine.MeshColliderCookingOptions WeldColocatedVertices;  // const
        public static UnityEngine.MeshColliderCookingOptions UseFastMidphase;  // const

    }

    // TypeToken: 0x2000007  // size: 0x14
    public sealed struct ColliderSurfaceType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ColliderSurfaceType Default;  // const
        public static UnityEngine.ColliderSurfaceType Concrete;  // const
        public static UnityEngine.ColliderSurfaceType Wood;  // const
        public static UnityEngine.ColliderSurfaceType Dirt;  // const
        public static UnityEngine.ColliderSurfaceType Grass;  // const
        public static UnityEngine.ColliderSurfaceType Stone;  // const
        public static UnityEngine.ColliderSurfaceType Water;  // const
        public static UnityEngine.ColliderSurfaceType Flesh;  // const
        public static UnityEngine.ColliderSurfaceType Metal;  // const
        public static UnityEngine.ColliderSurfaceType Crystal;  // const
        public static UnityEngine.ColliderSurfaceType Rubber;  // const
        public static UnityEngine.ColliderSurfaceType Sand;  // const
        public static UnityEngine.ColliderSurfaceType Gravel;  // const
        public static UnityEngine.ColliderSurfaceType Moss;  // const
        public static UnityEngine.ColliderSurfaceType Custom;  // const
        public static UnityEngine.ColliderSurfaceType Snow;  // const
        public static UnityEngine.ColliderSurfaceType Mud;  // const
        public static UnityEngine.ColliderSurfaceType Cardboard;  // const
        public static UnityEngine.ColliderSurfaceType Plastic;  // const
        public static UnityEngine.ColliderSurfaceType Ice;  // const
        public static UnityEngine.ColliderSurfaceType Wet;  // const
        public static UnityEngine.ColliderSurfaceType WaterV2;  // const

    }

    // TypeToken: 0x2000008  // size: 0x14
    public sealed struct RigidbodyInterpolation
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.RigidbodyInterpolation None;  // const
        public static UnityEngine.RigidbodyInterpolation Interpolate;  // const
        public static UnityEngine.RigidbodyInterpolation Extrapolate;  // const

    }

    // TypeToken: 0x2000009  // size: 0x50
    public class ControllerColliderHit
    {
        // Fields
        private UnityEngine.CharacterController m_Controller;  // 0x10
        private UnityEngine.Collider m_Collider;  // 0x18
        private UnityEngine.Vector3 m_Point;  // 0x20
        private UnityEngine.Vector3 m_Normal;  // 0x2c
        private UnityEngine.Vector3 m_MoveDirection;  // 0x38
        private System.Single m_MoveLength;  // 0x44
        private System.Int32 m_Push;  // 0x48

        // Properties
        UnityEngine.GameObject gameObject { get; /* RVA: 0x0B4200FC */ }
        UnityEngine.Vector3 point { get; /* RVA: 0x04DBD280 */ }

    }

    // TypeToken: 0x200000A  // size: 0x50
    public class Collision
    {
        // Fields
        private UnityEngine.Vector3 m_Impulse;  // 0x10
        private UnityEngine.Vector3 m_RelativeVelocity;  // 0x1c
        private UnityEngine.Component m_Body;  // 0x28
        private UnityEngine.Collider m_Collider;  // 0x30
        private System.Int32 m_ContactCount;  // 0x38
        private UnityEngine.ContactPoint[] m_ReusedContacts;  // 0x40
        private UnityEngine.ContactPoint[] m_LegacyContacts;  // 0x48

        // Properties
        UnityEngine.Vector3 relativeVelocity { get; /* RVA: 0x04DBA870 */ }
        UnityEngine.Collider collider { get; /* RVA: 0x04DBBEB0 */ }
        UnityEngine.GameObject gameObject { get; /* RVA: 0x0B42003C */ }
        UnityEngine.ContactPoint[] contacts { get; /* RVA: 0x0B41FFD4 */ }
        UnityEngine.Vector3 impulse { get; /* RVA: 0x04DBA8C0 */ }

    }

    // TypeToken: 0x200000B  // size: 0x14
    public sealed struct QueryTriggerInteraction
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.QueryTriggerInteraction UseGlobal;  // const
        public static UnityEngine.QueryTriggerInteraction Ignore;  // const
        public static UnityEngine.QueryTriggerInteraction Collide;  // const

    }

    // TypeToken: 0x200000C  // size: 0x14
    public sealed struct CollisionDetectionMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.CollisionDetectionMode Discrete;  // const
        public static UnityEngine.CollisionDetectionMode Continuous;  // const
        public static UnityEngine.CollisionDetectionMode ContinuousDynamic;  // const
        public static UnityEngine.CollisionDetectionMode ContinuousSpeculative;  // const

    }

    // TypeToken: 0x200000D  // size: 0x18
    public class ArticulationBody : UnityEngine.Behaviour
    {
        // Methods
        // RVA: 0x05393520  token: 0x600000A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000E  // size: 0x10
    public class Physics
    {
        // Fields
        private static System.Action<UnityEngine.PhysicsScene,Unity.Collections.NativeArray<UnityEngine.ModifiableContactPair>> ContactModifyEvent;  // static @ 0x0
        private static System.Action<UnityEngine.PhysicsScene,Unity.Collections.NativeArray<UnityEngine.ModifiableContactPair>> ContactModifyEventCCD;  // static @ 0x8
        private static System.Single k_MaxFloatMinusEpsilon;  // const
        public static System.Int32 IgnoreRaycastLayer;  // const
        public static System.Int32 DefaultRaycastLayers;  // const
        public static System.Int32 AllLayers;  // const
        public static System.Int32 kIgnoreRaycastLayer;  // const
        public static System.Int32 kDefaultRaycastLayers;  // const
        public static System.Int32 kAllLayers;  // const

        // Properties
        UnityEngine.Vector3 gravity { get; /* RVA: 0x045B55E0 */ set; /* RVA: 0x0B428FB0 */ }
        System.Single defaultContactOffset { get; /* RVA: 0x02F62830 */ set; /* RVA: 0x0B428E60 */ }
        System.Single sleepThreshold { get; /* RVA: 0x0B428C20 */ set; /* RVA: 0x0B42913C */ }
        System.Boolean queriesHitTriggers { get; /* RVA: 0x0B428BC8 */ set; /* RVA: 0x0B4290D4 */ }
        System.Boolean queriesHitBackfaces { get; /* RVA: 0x0B428B9C */ set; /* RVA: 0x0B4290A0 */ }
        System.Single bounceThreshold { get; /* RVA: 0x0B428980 */ set; /* RVA: 0x0B428DD8 */ }
        System.Single defaultMaxDepenetrationVelocity { get; /* RVA: 0x0B428A3C */ set; /* RVA: 0x0B428ED8 */ }
        System.Int32 defaultSolverIterations { get; /* RVA: 0x0B428A68 */ set; /* RVA: 0x0B428F14 */ }
        System.Int32 defaultSolverVelocityIterations { get; /* RVA: 0x0B428A94 */ set; /* RVA: 0x0B428F48 */ }
        System.Single defaultMaxAngularSpeed { get; /* RVA: 0x0B428A10 */ set; /* RVA: 0x0B428E9C */ }
        System.Boolean improvedPatchFriction { get; /* RVA: 0x0B428AC0 */ set; /* RVA: 0x0B428FC0 */ }
        UnityEngine.PhysicsScene defaultPhysicsScene { get; /* RVA: 0x03207570 */ }
        System.Boolean logPhysXCheckError { get; /* RVA: 0x0B428B70 */ set; /* RVA: 0x040E4B70 */ }
        System.Boolean autoSimulation { get; /* RVA: 0x03C330B0 */ set; /* RVA: 0x040E4BB0 */ }
        System.Boolean autoSyncTransforms { get; /* RVA: 0x0B428954 */ set; /* RVA: 0x0B428DA4 */ }
        System.Boolean reuseCollisionCallbacks { get; /* RVA: 0x0B428BF4 */ set; /* RVA: 0x0B429108 */ }
        System.Single interCollisionDistance { get; /* RVA: 0x0B428AEC */ set; /* RVA: 0x0B428FF4 */ }
        System.Single interCollisionStiffness { get; /* RVA: 0x0B428B44 */ set; /* RVA: 0x0B429064 */ }
        System.Boolean interCollisionSettingsToggle { get; /* RVA: 0x0B428B18 */ set; /* RVA: 0x0B429030 */ }
        UnityEngine.Vector3 clothGravity { get; /* RVA: 0x0B4289EC */ set; /* RVA: 0x0B428E50 */ }
        System.Single minPenetrationForPenalty { get; /* RVA: 0x04DBAEC0 */ set; /* RVA: 0x041E1670 */ }
        System.Single bounceTreshold { get; /* RVA: 0x0B4289AC */ set; /* RVA: 0x0B428E14 */ }
        System.Single sleepVelocity { get; /* RVA: 0x04DBAEC0 */ set; /* RVA: 0x041E1670 */ }
        System.Single sleepAngularVelocity { get; /* RVA: 0x04DBAEC0 */ set; /* RVA: 0x041E1670 */ }
        System.Single maxAngularVelocity { get; /* RVA: 0x04DBAEC0 */ set; /* RVA: 0x041E1670 */ }
        System.Int32 solverIterationCount { get; /* RVA: 0x0B428C4C */ set; /* RVA: 0x0B429178 */ }
        System.Int32 solverVelocityIterationCount { get; /* RVA: 0x0B428C58 */ set; /* RVA: 0x0B429180 */ }
        System.Single penetrationPenaltyForce { get; /* RVA: 0x04DBAEC0 */ set; /* RVA: 0x041E1670 */ }

        // Events
        event System.Action<UnityEngine.PhysicsScene,Unity.Collections.NativeArray<UnityEngine.ModifiableContactPair>> ContactModifyEvent;
        event System.Action<UnityEngine.PhysicsScene,Unity.Collections.NativeArray<UnityEngine.ModifiableContactPair>> ContactModifyEventCCD;

        // Methods
        // RVA: 0x0B424F74  token: 0x600000F
        private static System.Void OnSceneContactModify(UnityEngine.PhysicsScene scene, System.IntPtr buffer, System.Int32 count, System.Boolean isCCD) { }
        // RVA: 0x0427A2A0  token: 0x6000029
        public static System.Void ClearUnusedMemory() { }
        // RVA: 0x0B424940  token: 0x600002A
        public static System.Void IgnoreCollision(UnityEngine.Collider collider1, UnityEngine.Collider collider2, System.Boolean ignore) { }
        // RVA: 0x0B424994  token: 0x600002B
        public static System.Void IgnoreCollision(UnityEngine.Collider collider1, UnityEngine.Collider collider2) { }
        // RVA: 0x0B4249AC  token: 0x600002C
        public static System.Void IgnoreLayerCollision(System.Int32 layer1, System.Int32 layer2, System.Boolean ignore) { }
        // RVA: 0x0B424878  token: 0x600002D
        public static System.Void ForceRebuildDynamicTree(System.Boolean rebuildStatic, System.Boolean rebuildDynamic) { }
        // RVA: 0x0B4249A0  token: 0x600002E
        public static System.Void IgnoreLayerCollision(System.Int32 layer1, System.Int32 layer2) { }
        // RVA: 0x0B424900  token: 0x600002F
        public static System.Boolean GetIgnoreLayerCollision(System.Int32 layer1, System.Int32 layer2) { }
        // RVA: 0x0B4248BC  token: 0x6000030
        public static System.Boolean GetIgnoreCollision(UnityEngine.Collider collider1, UnityEngine.Collider collider2) { }
        // RVA: 0x0B427888  token: 0x6000031
        public static System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B427718  token: 0x6000032
        public static System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B427C40  token: 0x6000033
        public static System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single maxDistance) { }
        // RVA: 0x0B427B44  token: 0x6000034
        public static System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction) { }
        // RVA: 0x02F61D50  token: 0x6000035
        public static System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x046D9BD0  token: 0x6000036
        public static System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x03CF1320  token: 0x6000037
        public static System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance) { }
        // RVA: 0x0B4277A8  token: 0x6000038
        public static System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo) { }
        // RVA: 0x0B4275F0  token: 0x6000039
        public static System.Boolean RaycastV2(UnityEngine.ECSColliderResultProxy proxy, UnityEngine.Ray ray, System.Single maxDistance, UnityEngine.RaycastHit& hit) { }
        // RVA: 0x0B427684  token: 0x600003A
        public static System.Boolean RaycastV2(UnityEngine.ECSColliderResultProxy proxy, UnityEngine.Ray ray, System.Single maxDistance, System.Boolean hitHoles, UnityEngine.RaycastHit& hit) { }
        // RVA: 0x0B424BA4  token: 0x600003B
        private static UnityEngine.RaycastHit Internal_RaycastTestV2(UnityEngine.ECSColliderResultProxy collider, UnityEngine.Ray ray, System.Single maxDistance, System.Boolean hitHoles, System.Boolean& hasHit) { }
        // RVA: 0x0B427AB4  token: 0x600003C
        public static System.Boolean Raycast(UnityEngine.Ray ray, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B427920  token: 0x600003D
        public static System.Boolean Raycast(UnityEngine.Ray ray, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B4279A8  token: 0x600003E
        public static System.Boolean Raycast(UnityEngine.Ray ray, System.Single maxDistance) { }
        // RVA: 0x0B427CC8  token: 0x600003F
        public static System.Boolean Raycast(UnityEngine.Ray ray) { }
        // RVA: 0x03028220  token: 0x6000040
        public static System.Boolean Raycast(UnityEngine.Ray ray, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B427838  token: 0x6000041
        public static System.Boolean Raycast(UnityEngine.Ray ray, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B427A24  token: 0x6000042
        public static System.Boolean Raycast(UnityEngine.Ray ray, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance) { }
        // RVA: 0x0B427BBC  token: 0x6000043
        public static System.Boolean Raycast(UnityEngine.Ray ray, UnityEngine.RaycastHit& hitInfo) { }
        // RVA: 0x0B424E14  token: 0x6000044
        public static System.Boolean Linecast(UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B424EE4  token: 0x6000045
        public static System.Boolean Linecast(UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Int32 layerMask) { }
        // RVA: 0x0B424DC8  token: 0x6000046
        public static System.Boolean Linecast(UnityEngine.Vector3 start, UnityEngine.Vector3 end) { }
        // RVA: 0x0B424CA4  token: 0x6000047
        public static System.Boolean Linecast(UnityEngine.Vector3 start, UnityEngine.Vector3 end, UnityEngine.RaycastHit& hitInfo, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B424F2C  token: 0x6000048
        public static System.Boolean Linecast(UnityEngine.Vector3 start, UnityEngine.Vector3 end, UnityEngine.RaycastHit& hitInfo, System.Int32 layerMask) { }
        // RVA: 0x0B424D7C  token: 0x6000049
        public static System.Boolean Linecast(UnityEngine.Vector3 start, UnityEngine.Vector3 end, UnityEngine.RaycastHit& hitInfo) { }
        // RVA: 0x0B423ACC  token: 0x600004A
        public static System.Boolean CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B423D74  token: 0x600004B
        public static System.Boolean CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B423C24  token: 0x600004C
        public static System.Boolean CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance) { }
        // RVA: 0x0B4239D0  token: 0x600004D
        public static System.Boolean CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction) { }
        // RVA: 0x0B423C98  token: 0x600004E
        public static System.Boolean CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B423A44  token: 0x600004F
        public static System.Boolean CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B423948  token: 0x6000050
        public static System.Boolean CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance) { }
        // RVA: 0x0B423BA0  token: 0x6000051
        public static System.Boolean CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo) { }
        // RVA: 0x03730420  token: 0x6000052
        public static System.Boolean SphereCast(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B4283EC  token: 0x6000053
        public static System.Boolean SphereCast(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B428640  token: 0x6000054
        public static System.Boolean SphereCast(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance) { }
        // RVA: 0x0B4284C4  token: 0x6000055
        public static System.Boolean SphereCast(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo) { }
        // RVA: 0x0B428524  token: 0x6000056
        public static System.Boolean SphereCast(UnityEngine.Ray ray, System.Single radius, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B42860C  token: 0x6000057
        public static System.Boolean SphereCast(UnityEngine.Ray ray, System.Single radius, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B42848C  token: 0x6000058
        public static System.Boolean SphereCast(UnityEngine.Ray ray, System.Single radius, System.Single maxDistance) { }
        // RVA: 0x0B428700  token: 0x6000059
        public static System.Boolean SphereCast(UnityEngine.Ray ray, System.Single radius) { }
        // RVA: 0x0B4286A0  token: 0x600005A
        public static System.Boolean SphereCast(UnityEngine.Ray ray, System.Single radius, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B428740  token: 0x600005B
        public static System.Boolean SphereCast(UnityEngine.Ray ray, System.Single radius, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B428450  token: 0x600005C
        public static System.Boolean SphereCast(UnityEngine.Ray ray, System.Single radius, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance) { }
        // RVA: 0x0B4285C8  token: 0x600005D
        public static System.Boolean SphereCast(UnityEngine.Ray ray, System.Single radius, UnityEngine.RaycastHit& hitInfo) { }
        // RVA: 0x0B422C34  token: 0x600005E
        public static System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B422F84  token: 0x600005F
        public static System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B422E38  token: 0x6000060
        public static System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation, System.Single maxDistance) { }
        // RVA: 0x0B4230A4  token: 0x6000061
        public static System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation) { }
        // RVA: 0x0B422BA4  token: 0x6000062
        public static System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction) { }
        // RVA: 0x0B422EC0  token: 0x6000063
        public static System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B422D08  token: 0x6000064
        public static System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B42312C  token: 0x6000065
        public static System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, UnityEngine.Quaternion orientation, System.Single maxDistance) { }
        // RVA: 0x0B423010  token: 0x6000066
        public static System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, UnityEngine.Quaternion orientation) { }
        // RVA: 0x0B422DA0  token: 0x6000067
        public static System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo) { }
        // RVA: 0x0B424B18  token: 0x6000068
        private static UnityEngine.RaycastHit[] Internal_RaycastAll(UnityEngine.PhysicsScene physicsScene, UnityEngine.Ray ray, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B4270AC  token: 0x6000069
        public static UnityEngine.RaycastHit[] RaycastAll(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B427064  token: 0x600006A
        public static UnityEngine.RaycastHit[] RaycastAll(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B42721C  token: 0x600006B
        public static UnityEngine.RaycastHit[] RaycastAll(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single maxDistance) { }
        // RVA: 0x0B427268  token: 0x600006C
        public static UnityEngine.RaycastHit[] RaycastAll(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction) { }
        // RVA: 0x0B4271D0  token: 0x600006D
        public static UnityEngine.RaycastHit[] RaycastAll(UnityEngine.Ray ray, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B427018  token: 0x600006E
        public static UnityEngine.RaycastHit[] RaycastAll(UnityEngine.Ray ray, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B426FC8  token: 0x600006F
        public static UnityEngine.RaycastHit[] RaycastAll(UnityEngine.Ray ray, System.Single maxDistance) { }
        // RVA: 0x0B4272BC  token: 0x6000070
        public static UnityEngine.RaycastHit[] RaycastAll(UnityEngine.Ray ray) { }
        // RVA: 0x03B1D970  token: 0x6000071
        public static System.Int32 RaycastNonAlloc(UnityEngine.Ray ray, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x03906800  token: 0x6000072
        public static System.Int32 RaycastNonAlloc(UnityEngine.Ray ray, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B427310  token: 0x6000073
        public static System.Int32 RaycastNonAlloc(UnityEngine.Ray ray, UnityEngine.RaycastHit[] results, System.Single maxDistance) { }
        // RVA: 0x0B4273A0  token: 0x6000074
        public static System.Int32 RaycastNonAlloc(UnityEngine.Ray ray, UnityEngine.RaycastHit[] results) { }
        // RVA: 0x03206500  token: 0x6000075
        public static System.Int32 RaycastNonAlloc(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B4274C0  token: 0x6000076
        public static System.Int32 RaycastNonAlloc(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B427424  token: 0x6000077
        public static System.Int32 RaycastNonAlloc(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance) { }
        // RVA: 0x0B427560  token: 0x6000078
        public static System.Int32 RaycastNonAlloc(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results) { }
        // RVA: 0x0B426974  token: 0x6000079
        private static UnityEngine.RaycastHit[] Query_CapsuleCastAll(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B4231C0  token: 0x600007A
        public static UnityEngine.RaycastHit[] CapsuleCastAll(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B423374  token: 0x600007B
        public static UnityEngine.RaycastHit[] CapsuleCastAll(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B423300  token: 0x600007C
        public static UnityEngine.RaycastHit[] CapsuleCastAll(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance) { }
        // RVA: 0x0B4233EC  token: 0x600007D
        public static UnityEngine.RaycastHit[] CapsuleCastAll(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction) { }
        // RVA: 0x0B426ED0  token: 0x600007E
        private static UnityEngine.RaycastHit[] Query_SphereCastAll(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B428080  token: 0x600007F
        public static UnityEngine.RaycastHit[] SphereCastAll(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B427EB8  token: 0x6000080
        public static UnityEngine.RaycastHit[] SphereCastAll(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B427FF8  token: 0x6000081
        public static UnityEngine.RaycastHit[] SphereCastAll(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance) { }
        // RVA: 0x0B427FA0  token: 0x6000082
        public static UnityEngine.RaycastHit[] SphereCastAll(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction) { }
        // RVA: 0x0B427F4C  token: 0x6000083
        public static UnityEngine.RaycastHit[] SphereCastAll(UnityEngine.Ray ray, System.Single radius, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B427E84  token: 0x6000084
        public static UnityEngine.RaycastHit[] SphereCastAll(UnityEngine.Ray ray, System.Single radius, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B428048  token: 0x6000085
        public static UnityEngine.RaycastHit[] SphereCastAll(UnityEngine.Ray ray, System.Single radius, System.Single maxDistance) { }
        // RVA: 0x0B427F0C  token: 0x6000086
        public static UnityEngine.RaycastHit[] SphereCastAll(UnityEngine.Ray ray, System.Single radius) { }
        // RVA: 0x0B426024  token: 0x6000087
        private static UnityEngine.Collider[] OverlapCapsule_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B426118  token: 0x6000088
        public static UnityEngine.Collider[] OverlapCapsule(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B426054  token: 0x6000089
        public static UnityEngine.Collider[] OverlapCapsule(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, System.Int32 layerMask) { }
        // RVA: 0x0B4261F0  token: 0x600008A
        public static UnityEngine.Collider[] OverlapCapsule(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius) { }
        // RVA: 0x0B425E70  token: 0x600008B
        private static UnityEngine.ECSColliderResultProxy[] OverlapCapsuleV2_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B425F34  token: 0x600008C
        public static UnityEngine.ECSColliderResultProxy[] OverlapCapsuleV2(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B425EEC  token: 0x600008D
        public static UnityEngine.ECSColliderResultProxy[] OverlapCapsuleV2(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, System.Int32 layerMask) { }
        // RVA: 0x0B425EA0  token: 0x600008E
        public static UnityEngine.ECSColliderResultProxy[] OverlapCapsuleV2(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius) { }
        // RVA: 0x0B4266FC  token: 0x600008F
        private static UnityEngine.Collider[] OverlapSphere_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B4267C4  token: 0x6000090
        public static UnityEngine.Collider[] OverlapSphere(UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x03679460  token: 0x6000091
        public static UnityEngine.ECSColliderResultProxy CreateECSProxy(UnityEngine.Collider collider) { }
        // RVA: 0x0B426724  token: 0x6000092
        public static UnityEngine.Collider[] OverlapSphere(UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask) { }
        // RVA: 0x0B42687C  token: 0x6000093
        public static UnityEngine.Collider[] OverlapSphere(UnityEngine.Vector3 position, System.Single radius) { }
        // RVA: 0x0B42659C  token: 0x6000094
        private static UnityEngine.ECSColliderResultProxy[] OverlapSphereV2_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B4265FC  token: 0x6000095
        public static UnityEngine.ECSColliderResultProxy[] OverlapSphereV2(UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B426668  token: 0x6000096
        public static UnityEngine.ECSColliderResultProxy[] OverlapSphereV2(UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask) { }
        // RVA: 0x0B4265C4  token: 0x6000097
        public static UnityEngine.ECSColliderResultProxy[] OverlapSphereV2(UnityEngine.Vector3 position, System.Single radius) { }
        // RVA: 0x05395788  token: 0x6000098
        private static System.Void Simulate_Internal(UnityEngine.PhysicsScene physicsScene, System.Single step) { }
        // RVA: 0x03C33040  token: 0x6000099
        public static System.Void Simulate(System.Single step) { }
        // RVA: 0x032CA830  token: 0x600009C
        public static System.Void SyncTransforms() { }
        // RVA: 0x0B426D84  token: 0x60000A1
        private static System.Boolean Query_ComputePenetration(UnityEngine.Collider colliderA, UnityEngine.Vector3 positionA, UnityEngine.Quaternion rotationA, UnityEngine.Collider colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance) { }
        // RVA: 0x0B424690  token: 0x60000A2
        public static System.Boolean ComputePenetration(UnityEngine.Collider colliderA, UnityEngine.Vector3 positionA, UnityEngine.Quaternion rotationA, UnityEngine.Collider colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance) { }
        // RVA: 0x05393470  token: 0x60000A3
        private static System.Boolean Query_ComputePenetrationV2(UnityEngine.ECSColliderResultProxy colliderA, UnityEngine.Vector3 positionA, UnityEngine.Quaternion rotationA, UnityEngine.ECSColliderResultProxy colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance) { }
        // RVA: 0x03679BB0  token: 0x60000A4
        public static System.Boolean ComputePenetrationV2(UnityEngine.ECSColliderResultProxy colliderA, UnityEngine.Vector3 positionA, UnityEngine.Quaternion rotationA, UnityEngine.ECSColliderResultProxy colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance) { }
        // RVA: 0x0B426CD4  token: 0x60000A5
        private static System.Boolean Query_ComputeCapsulePenetration(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Collider colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance) { }
        // RVA: 0x0B424564  token: 0x60000A6
        public static System.Boolean ComputeCapsulePenetration(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Collider colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance) { }
        // RVA: 0x0B426C24  token: 0x60000A7
        private static System.Boolean Query_ComputeCapsulePenetrationV2(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.ECSColliderResultProxy colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance) { }
        // RVA: 0x0B424498  token: 0x60000A8
        public static System.Boolean ComputeCapsulePenetrationV2(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.ECSColliderResultProxy colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance) { }
        // RVA: 0x0B426E34  token: 0x60000A9
        private static System.Boolean Query_ComputeSpherePenetrationV2(UnityEngine.Vector3 center, System.Single radius, UnityEngine.ECSColliderResultProxy colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance) { }
        // RVA: 0x0B4247C8  token: 0x60000AA
        public static System.Boolean ComputeSpherePenetrationV2(UnityEngine.Vector3 center, System.Single radius, UnityEngine.ECSColliderResultProxy colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance) { }
        // RVA: 0x0B426B74  token: 0x60000AB
        private static System.Boolean Query_ComputeBoxPenetrationV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtent, UnityEngine.Quaternion rotationA, UnityEngine.ECSColliderResultProxy colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance) { }
        // RVA: 0x0B4243BC  token: 0x60000AC
        public static System.Boolean ComputeBoxPenetrationV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtent, UnityEngine.Quaternion rotationA, UnityEngine.ECSColliderResultProxy colliderB, UnityEngine.Vector3 positionB, UnityEngine.Quaternion rotationB, UnityEngine.Vector3& direction, System.Single& distance) { }
        // RVA: 0x0B424C6C  token: 0x60000AD
        private static System.Boolean IsSimulatedThisFrame_Internal() { }
        // RVA: 0x0B424C98  token: 0x60000AE
        public static System.Boolean IsSimulatedThisFrame() { }
        // RVA: 0x0B427E48  token: 0x60000AF
        private static System.Void SetForceSimulateThisFrame_Internal(System.Boolean force) { }
        // RVA: 0x0B427E7C  token: 0x60000B0
        public static System.Void SetForceSimulateThisFrame(System.Boolean force) { }
        // RVA: 0x04D51170  token: 0x60000B1
        private static System.Void SetFastSimulate_Internal(System.Boolean fast) { }
        // RVA: 0x05396EC0  token: 0x60000B2
        public static System.Void SetFastSimulate(System.Boolean fast) { }
        // RVA: 0x0B426AD0  token: 0x60000B3
        private static UnityEngine.Vector3 Query_ClosestPoint(UnityEngine.Collider collider, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 point) { }
        // RVA: 0x0B4242C4  token: 0x60000B4
        public static UnityEngine.Vector3 ClosestPoint(UnityEngine.Vector3 point, UnityEngine.Collider collider, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0B426A2C  token: 0x60000B5
        private static UnityEngine.Vector3 Query_ClosestPointV2(UnityEngine.ECSColliderResultProxy colliderProxy, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 point) { }
        // RVA: 0x0B424220  token: 0x60000B6
        public static UnityEngine.Vector3 ClosestPointV2(UnityEngine.Vector3 point, UnityEngine.ECSColliderResultProxy colliderProxy, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0B426F74  token: 0x60000B7
        private static UnityEngine.Vector3 Query_TryGetClosestPoint(UnityEngine.ECSColliderResultProxy colliderProxy, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 point, System.Boolean& resultIsValid) { }
        // RVA: 0x0B42877C  token: 0x60000B8
        public static System.Boolean TryGetClosestPoint(UnityEngine.ECSColliderResultProxy colliderProxy, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Vector3 point, UnityEngine.Vector3& result) { }
        // RVA: 0x0B42646C  token: 0x60000C1
        public static System.Int32 OverlapSphereNonAlloc(UnityEngine.Vector3 position, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B4263B8  token: 0x60000C2
        public static System.Int32 OverlapSphereNonAlloc(UnityEngine.Vector3 position, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask) { }
        // RVA: 0x0B426310  token: 0x60000C3
        public static System.Int32 OverlapSphereNonAlloc(UnityEngine.Vector3 position, System.Single radius, UnityEngine.Collider[] results) { }
        // RVA: 0x03D7C060  token: 0x60000C4
        public static System.Int32 OverlapSphereNonAllocV2(UnityEngine.Vector3 position, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B4262E0  token: 0x60000C5
        public static System.Int32 OverlapSphereNonAllocV2(UnityEngine.Vector3 position, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask) { }
        // RVA: 0x0B4262A8  token: 0x60000C6
        public static System.Int32 OverlapSphereNonAllocV2(UnityEngine.Vector3 position, System.Single radius, UnityEngine.ECSColliderResultProxy[] results) { }
        // RVA: 0x0B424124  token: 0x60000C7
        private static System.Boolean CheckSphere_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B424184  token: 0x60000C8
        public static System.Boolean CheckSphere(UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B4241F0  token: 0x60000C9
        public static System.Boolean CheckSphere(UnityEngine.Vector3 position, System.Single radius, System.Int32 layerMask) { }
        // RVA: 0x0B42414C  token: 0x60000CA
        public static System.Boolean CheckSphere(UnityEngine.Vector3 position, System.Single radius) { }
        // RVA: 0x030F1F60  token: 0x60000CB
        public static System.Int32 CapsuleCastNonAlloc(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B423604  token: 0x60000CC
        public static System.Int32 CapsuleCastNonAllocV2(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.ECSColliderResultProxy[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B4237B4  token: 0x60000CD
        public static System.Int32 CapsuleCastNonAlloc(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B4236E0  token: 0x60000CE
        public static System.Int32 CapsuleCastNonAllocV2(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.ECSColliderResultProxy[] results, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B4238C0  token: 0x60000CF
        public static System.Int32 CapsuleCastNonAlloc(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance) { }
        // RVA: 0x0B423530  token: 0x60000D0
        public static System.Int32 CapsuleCastNonAllocV2(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.ECSColliderResultProxy[] results, System.Single maxDistance) { }
        // RVA: 0x0B42383C  token: 0x60000D1
        public static System.Int32 CapsuleCastNonAlloc(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results) { }
        // RVA: 0x0B423460  token: 0x60000D2
        public static System.Int32 CapsuleCastNonAllocV2(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.ECSColliderResultProxy[] results) { }
        // RVA: 0x030F58C0  token: 0x60000D3
        public static System.Int32 SphereCastNonAlloc(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B428328  token: 0x60000D4
        public static System.Int32 SphereCastNonAlloc(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B428268  token: 0x60000D5
        public static System.Int32 SphereCastNonAlloc(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance) { }
        // RVA: 0x0B42838C  token: 0x60000D6
        public static System.Int32 SphereCastNonAlloc(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results) { }
        // RVA: 0x0B4282C8  token: 0x60000D7
        public static System.Int32 SphereCastNonAlloc(UnityEngine.Ray ray, System.Single radius, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B4281F0  token: 0x60000D8
        public static System.Int32 SphereCastNonAlloc(UnityEngine.Ray ray, System.Single radius, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B42822C  token: 0x60000D9
        public static System.Int32 SphereCastNonAlloc(UnityEngine.Ray ray, System.Single radius, UnityEngine.RaycastHit[] results, System.Single maxDistance) { }
        // RVA: 0x0B4281AC  token: 0x60000DA
        public static System.Int32 SphereCastNonAlloc(UnityEngine.Ray ray, System.Single radius, UnityEngine.RaycastHit[] results) { }
        // RVA: 0x053909E0  token: 0x60000DB
        private static System.Boolean CheckCapsule_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x02F65310  token: 0x60000DC
        public static System.Boolean CheckCapsule(UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B42402C  token: 0x60000DD
        public static System.Boolean CheckCapsule(UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Single radius, System.Int32 layerMask) { }
        // RVA: 0x0B424074  token: 0x60000DE
        public static System.Boolean CheckCapsule(UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Single radius) { }
        // RVA: 0x0B423E50  token: 0x60000DF
        private static System.Boolean CheckBox_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, System.Int32 layermask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B423F44  token: 0x60000E0
        public static System.Boolean CheckBox(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, System.Int32 layermask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B423FD8  token: 0x60000E1
        public static System.Boolean CheckBox(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, System.Int32 layerMask) { }
        // RVA: 0x0B423E80  token: 0x60000E2
        public static System.Boolean CheckBox(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation) { }
        // RVA: 0x0B423EDC  token: 0x60000E3
        public static System.Boolean CheckBox(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents) { }
        // RVA: 0x0B425774  token: 0x60000E4
        private static UnityEngine.Collider[] OverlapBox_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B425928  token: 0x60000E5
        public static UnityEngine.Collider[] OverlapBox(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B425A0C  token: 0x60000E6
        public static UnityEngine.Collider[] OverlapBox(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, System.Int32 layerMask) { }
        // RVA: 0x0B4257A4  token: 0x60000E7
        public static UnityEngine.Collider[] OverlapBox(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation) { }
        // RVA: 0x0B425868  token: 0x60000E8
        public static UnityEngine.Collider[] OverlapBox(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents) { }
        // RVA: 0x0B425150  token: 0x60000E9
        public static System.Int32 OverlapBoxNonAlloc(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Collider[] results, UnityEngine.Quaternion orientation, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B42524C  token: 0x60000EA
        public static System.Int32 OverlapBoxNonAlloc(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Collider[] results, UnityEngine.Quaternion orientation, System.Int32 mask) { }
        // RVA: 0x0B425328  token: 0x60000EB
        public static System.Int32 OverlapBoxNonAlloc(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Collider[] results, UnityEngine.Quaternion orientation) { }
        // RVA: 0x0B425400  token: 0x60000EC
        public static System.Int32 OverlapBoxNonAlloc(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Collider[] results) { }
        // RVA: 0x0B425538  token: 0x60000ED
        private static UnityEngine.ECSColliderResultProxy[] OverlapBoxV2_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B42567C  token: 0x60000EE
        public static UnityEngine.ECSColliderResultProxy[] OverlapBoxV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B425628  token: 0x60000EF
        public static UnityEngine.ECSColliderResultProxy[] OverlapBoxV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, System.Int32 layerMask) { }
        // RVA: 0x0B425568  token: 0x60000F0
        public static UnityEngine.ECSColliderResultProxy[] OverlapBoxV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation) { }
        // RVA: 0x0B4255C0  token: 0x60000F1
        public static UnityEngine.ECSColliderResultProxy[] OverlapBoxV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents) { }
        // RVA: 0x045ADD40  token: 0x60000F2
        public static System.Int32 OverlapBoxNonAllocV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.ECSColliderResultProxy[] results, UnityEngine.Quaternion orientation, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B425098  token: 0x60000F3
        public static System.Int32 OverlapBoxNonAllocV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.ECSColliderResultProxy[] results, UnityEngine.Quaternion orientation, System.Int32 mask) { }
        // RVA: 0x0B4250F8  token: 0x60000F4
        public static System.Int32 OverlapBoxNonAllocV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.ECSColliderResultProxy[] results, UnityEngine.Quaternion orientation) { }
        // RVA: 0x0B425030  token: 0x60000F5
        public static System.Int32 OverlapBoxNonAllocV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.ECSColliderResultProxy[] results) { }
        // RVA: 0x03027790  token: 0x60000F6
        public static System.Int32 BoxCastNonAlloc(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B422A7C  token: 0x60000F7
        public static System.Int32 BoxCastNonAlloc(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, UnityEngine.Quaternion orientation) { }
        // RVA: 0x0B422B10  token: 0x60000F8
        public static System.Int32 BoxCastNonAlloc(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, UnityEngine.Quaternion orientation, System.Single maxDistance) { }
        // RVA: 0x0B42294C  token: 0x60000F9
        public static System.Int32 BoxCastNonAlloc(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B4229E4  token: 0x60000FA
        public static System.Int32 BoxCastNonAlloc(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results) { }
        // RVA: 0x0B424A60  token: 0x60000FB
        private static UnityEngine.RaycastHit[] Internal_BoxCastAll(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B4225D8  token: 0x60000FC
        public static UnityEngine.RaycastHit[] BoxCastAll(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B422830  token: 0x60000FD
        public static UnityEngine.RaycastHit[] BoxCastAll(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask) { }
        // RVA: 0x0B422720  token: 0x60000FE
        public static UnityEngine.RaycastHit[] BoxCastAll(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation, System.Single maxDistance) { }
        // RVA: 0x0B4227A8  token: 0x60000FF
        public static UnityEngine.RaycastHit[] BoxCastAll(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation) { }
        // RVA: 0x0B4228BC  token: 0x6000100
        public static UnityEngine.RaycastHit[] BoxCastAll(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction) { }
        // RVA: 0x0B425D1C  token: 0x6000101
        public static System.Int32 OverlapCapsuleNonAlloc(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B425B7C  token: 0x6000102
        public static System.Int32 OverlapCapsuleNonAlloc(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask) { }
        // RVA: 0x0B425C50  token: 0x6000103
        public static System.Int32 OverlapCapsuleNonAlloc(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.Collider[] results) { }
        // RVA: 0x031FFA70  token: 0x6000104
        public static System.Int32 OverlapCapsuleNonAllocV2(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B425B2C  token: 0x6000105
        public static System.Int32 OverlapCapsuleNonAllocV2(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask) { }
        // RVA: 0x0B425AE0  token: 0x6000106
        public static System.Int32 OverlapCapsuleNonAllocV2(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.ECSColliderResultProxy[] results) { }
        // RVA: 0x0B424C58  token: 0x6000107
        private static System.Void Internal_RebuildBroadphaseRegions(UnityEngine.Bounds bounds, System.Int32 subdivisions) { }
        // RVA: 0x0B427D34  token: 0x6000108
        public static System.Void RebuildBroadphaseRegions(UnityEngine.Bounds worldBounds, System.Int32 subdivisions) { }
        // RVA: 0x0B422598  token: 0x6000109
        public static System.Void BakeMesh(System.Int32 meshID, System.Boolean convex) { }
        // RVA: 0x041E1670  token: 0x600011A
        public System.Void .ctor() { }
        // RVA: 0x045B5610  token: 0x600011B
        private static System.Void get_gravity_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x0B428F7C  token: 0x600011C
        private static System.Void set_gravity_Injected(UnityEngine.Vector3& value) { }
        // RVA: 0x032075C0  token: 0x600011D
        private static System.Void get_defaultPhysicsScene_Injected(UnityEngine.PhysicsScene& ret) { }
        // RVA: 0x0B424B40  token: 0x600011E
        private static System.Void Internal_RaycastTestV2_Injected(UnityEngine.ECSColliderResultProxy& collider, UnityEngine.Ray& ray, System.Single maxDistance, System.Boolean hitHoles, System.Boolean& hasHit, UnityEngine.RaycastHit& ret) { }
        // RVA: 0x0B424AB4  token: 0x600011F
        private static UnityEngine.RaycastHit[] Internal_RaycastAll_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Ray& ray, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B426910  token: 0x6000120
        private static UnityEngine.RaycastHit[] Query_CapsuleCastAll_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& p0, UnityEngine.Vector3& p1, System.Single radius, UnityEngine.Vector3& direction, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B426E6C  token: 0x6000121
        private static UnityEngine.RaycastHit[] Query_SphereCastAll_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& origin, System.Single radius, UnityEngine.Vector3& direction, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B425FC0  token: 0x6000122
        private static UnityEngine.Collider[] OverlapCapsule_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& point0, UnityEngine.Vector3& point1, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B425E0C  token: 0x6000123
        private static UnityEngine.ECSColliderResultProxy[] OverlapCapsuleV2_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& point0, UnityEngine.Vector3& point1, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B426698  token: 0x6000124
        private static UnityEngine.Collider[] OverlapSphere_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& position, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B426538  token: 0x6000125
        private static UnityEngine.ECSColliderResultProxy[] OverlapSphereV2_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& position, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x03C32E90  token: 0x6000126
        private static System.Void Simulate_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, System.Single step) { }
        // RVA: 0x0B426D20  token: 0x6000127
        private static System.Boolean Query_ComputePenetration_Injected(UnityEngine.Collider colliderA, UnityEngine.Vector3& positionA, UnityEngine.Quaternion& rotationA, UnityEngine.Collider colliderB, UnityEngine.Vector3& positionB, UnityEngine.Quaternion& rotationB, UnityEngine.Vector3& direction, System.Single& distance) { }
        // RVA: 0x04500B90  token: 0x6000128
        private static System.Boolean Query_ComputePenetrationV2_Injected(UnityEngine.ECSColliderResultProxy& colliderA, UnityEngine.Vector3& positionA, UnityEngine.Quaternion& rotationA, UnityEngine.ECSColliderResultProxy& colliderB, UnityEngine.Vector3& positionB, UnityEngine.Quaternion& rotationB, UnityEngine.Vector3& direction, System.Single& distance) { }
        // RVA: 0x0B426C70  token: 0x6000129
        private static System.Boolean Query_ComputeCapsulePenetration_Injected(UnityEngine.Vector3& point1, UnityEngine.Vector3& point2, System.Single radius, UnityEngine.Collider colliderB, UnityEngine.Vector3& positionB, UnityEngine.Quaternion& rotationB, UnityEngine.Vector3& direction, System.Single& distance) { }
        // RVA: 0x0B426BC0  token: 0x600012A
        private static System.Boolean Query_ComputeCapsulePenetrationV2_Injected(UnityEngine.Vector3& point1, UnityEngine.Vector3& point2, System.Single radius, UnityEngine.ECSColliderResultProxy& colliderB, UnityEngine.Vector3& positionB, UnityEngine.Quaternion& rotationB, UnityEngine.Vector3& direction, System.Single& distance) { }
        // RVA: 0x0B426DD0  token: 0x600012B
        private static System.Boolean Query_ComputeSpherePenetrationV2_Injected(UnityEngine.Vector3& center, System.Single radius, UnityEngine.ECSColliderResultProxy& colliderB, UnityEngine.Vector3& positionB, UnityEngine.Quaternion& rotationB, UnityEngine.Vector3& direction, System.Single& distance) { }
        // RVA: 0x0B426B10  token: 0x600012C
        private static System.Boolean Query_ComputeBoxPenetrationV2_Injected(UnityEngine.Vector3& center, UnityEngine.Vector3& halfExtent, UnityEngine.Quaternion& rotationA, UnityEngine.ECSColliderResultProxy& colliderB, UnityEngine.Vector3& positionB, UnityEngine.Quaternion& rotationB, UnityEngine.Vector3& direction, System.Single& distance) { }
        // RVA: 0x0B426A6C  token: 0x600012D
        private static System.Void Query_ClosestPoint_Injected(UnityEngine.Collider collider, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& point, UnityEngine.Vector3& ret) { }
        // RVA: 0x0B4269C8  token: 0x600012E
        private static System.Void Query_ClosestPointV2_Injected(UnityEngine.ECSColliderResultProxy& colliderProxy, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& point, UnityEngine.Vector3& ret) { }
        // RVA: 0x0B426F10  token: 0x600012F
        private static System.Void Query_TryGetClosestPoint_Injected(UnityEngine.ECSColliderResultProxy& colliderProxy, UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, UnityEngine.Vector3& point, System.Boolean& resultIsValid, UnityEngine.Vector3& ret) { }
        // RVA: 0x0B4289B8  token: 0x6000130
        private static System.Void get_clothGravity_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x0B428E1C  token: 0x6000131
        private static System.Void set_clothGravity_Injected(UnityEngine.Vector3& value) { }
        // RVA: 0x0B4240C0  token: 0x6000132
        private static System.Boolean CheckSphere_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& position, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x02F653B0  token: 0x6000133
        private static System.Boolean CheckCapsule_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& start, UnityEngine.Vector3& end, System.Single radius, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B423DEC  token: 0x6000134
        private static System.Boolean CheckBox_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& center, UnityEngine.Vector3& halfExtents, UnityEngine.Quaternion& orientation, System.Int32 layermask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B425710  token: 0x6000135
        private static UnityEngine.Collider[] OverlapBox_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& center, UnityEngine.Vector3& halfExtents, UnityEngine.Quaternion& orientation, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B4254D4  token: 0x6000136
        private static UnityEngine.ECSColliderResultProxy[] OverlapBoxV2_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& center, UnityEngine.Vector3& halfExtents, UnityEngine.Quaternion& orientation, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B4249FC  token: 0x6000137
        private static UnityEngine.RaycastHit[] Internal_BoxCastAll_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& center, UnityEngine.Vector3& halfExtents, UnityEngine.Vector3& direction, UnityEngine.Quaternion& orientation, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B424C18  token: 0x6000138
        private static System.Void Internal_RebuildBroadphaseRegions_Injected(UnityEngine.Bounds& bounds, System.Int32 subdivisions) { }

    }

    // TypeToken: 0x200000F  // size: 0x78
    public sealed struct ModifiableContactPair
    {
        // Fields
        private System.IntPtr actor;  // 0x10
        private System.IntPtr otherActor;  // 0x18
        private System.IntPtr shape;  // 0x20
        private System.IntPtr otherShape;  // 0x28
        public UnityEngine.Quaternion rotation;  // 0x30
        public UnityEngine.Vector3 position;  // 0x40
        public UnityEngine.Quaternion otherRotation;  // 0x4c
        public UnityEngine.Vector3 otherPosition;  // 0x5c
        private System.Int32 numContacts;  // 0x68
        private System.IntPtr contacts;  // 0x70

    }

    // TypeToken: 0x2000010  // size: 0x18
    public class PhysicMaterial : UnityEngine.Object
    {
        // Methods
        // RVA: 0x0B421764  token: 0x6000139
        public System.Void .ctor() { }
        // RVA: 0x0B421720  token: 0x600013A
        private static System.Void Internal_CreateDynamicsMaterial(UnityEngine.PhysicMaterial mat, System.String name) { }

    }

    // TypeToken: 0x2000011  // size: 0x28
    public sealed struct ECSColliderResultProxy : System.IEquatable`1
    {
        // Fields
        private System.IntPtr m_Actor;  // 0x10
        private System.Int32 m_EcsId;  // 0x18
        private System.UInt32 m_EcsVersion;  // 0x1c
        private System.Int32 m_Collider;  // 0x20
        private System.Int32 m_ComponentIndex;  // 0x24
        private static readonly UnityEngine.ECSColliderResultProxy INVALID_PROXY;  // static @ 0x0

        // Properties
        System.Int32 componentIndex { get; /* RVA: 0x04D872D0 */ }
        UnityEngine.HyperGryph.ECS.Entity colliderEntity { get; /* RVA: 0x0444B190 */ }
        System.Int32 ecsId { get; /* RVA: 0x04D9CF30 */ }
        System.UInt32 ecsVersion { get; /* RVA: 0x04D9D1B0 */ }
        UnityEngine.ECSColliderResultProxy invalid { get; /* RVA: 0x0B420590 */ }
        UnityEngine.Collider collider { get; /* RVA: 0x03204840 */ }
        System.Int32 colliderInstanceId { get; /* RVA: 0x04DBA9F0 */ }
        System.Boolean bIsECS { get; /* RVA: 0x03205320 */ }
        System.Int32 tagConfig { get; /* RVA: 0x034E7180 */ }
        System.Boolean isTrigger { get; /* RVA: 0x03731A40 */ }
        System.Int32 layer { get; /* RVA: 0x030F5140 */ }
        UnityEngine.Transform transform { get; /* RVA: 0x032023B0 */ }
        UnityEngine.Vector3 transformPosition { get; /* RVA: 0x03677460 */ }
        UnityEngine.Quaternion transformRotation { get; /* RVA: 0x03679660 */ }
        UnityEngine.Vector3 transformLossyScale { get; /* RVA: 0x0B4205F4 */ }
        UnityEngine.Vector3 shapePosition { get; /* RVA: 0x031FA240 */ }
        UnityEngine.Bounds bounds { get; /* RVA: 0x03678230 */ }
        System.Int32 colliderOptions { get; /* RVA: 0x03205340 */ }
        UnityEngine.ColliderSurfaceType surfaceType { get; /* RVA: 0x032884F0 */ }
        UnityEngine.ECSColliderType colliderType { get; /* RVA: 0x0B420434 */ }
        UnityEngine.GameObject gameObject { get; /* RVA: 0x03F1E2A0 */ }
        System.Boolean hasComplexMesh { get; /* RVA: 0x03203C00 */ }

        // Methods
        // RVA: 0x03204A70  token: 0x600013B
        public System.Void SetColliderAndSetIndex(System.Int32 colliderInstanceID) { }
        // RVA: 0x04D8F540  token: 0x600013C
        public System.Void SetComponentIndex(System.Int32 index) { }
        // RVA: 0x04D88B60  token: 0x600013D
        public System.Void SetActor(System.IntPtr actor) { }
        // RVA: 0x04D88B40  token: 0x600013E
        public System.Void SetEcsId(System.Int32 id) { }
        // RVA: 0x04D8CAE0  token: 0x600013F
        public System.Void SetEcsVersion(System.UInt32 version) { }
        // RVA: 0x03205140  token: 0x6000149
        public System.Boolean IsValid() { }
        // RVA: 0x03676B00  token: 0x6000154
        public System.String GetColliderGoName() { }
        // RVA: 0x0406A3A0  token: 0x6000157
        public System.Int32 GetFactoryBuildingIdentifier() { }
        // RVA: 0x0B420320  token: 0x6000159
        public System.Boolean TryGetClosestPoint(UnityEngine.Vector3 point, UnityEngine.Vector3& result) { }
        // RVA: 0x03677BC0  token: 0x600015A
        public UnityEngine.Vector3 ClosestPoint(UnityEngine.Vector3 position) { }
        // RVA: 0x0B4201C0  token: 0x600015B
        public System.Boolean Raycast(UnityEngine.Ray ray, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance) { }
        // RVA: 0x0B4206FC  token: 0x600015C
        public static System.Boolean op_Equality(UnityEngine.ECSColliderResultProxy left, UnityEngine.ECSColliderResultProxy right) { }
        // RVA: 0x02F5B750  token: 0x600015D
        public static System.Boolean op_Inequality(UnityEngine.ECSColliderResultProxy left, UnityEngine.ECSColliderResultProxy right) { }
        // RVA: 0x043285A0  token: 0x600015E
        public virtual System.Boolean Equals(UnityEngine.ECSColliderResultProxy other) { }
        // RVA: 0x0B42011C  token: 0x600015F
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x03F70140  token: 0x6000160
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x036778A0  token: 0x6000161
        public System.Boolean CompareTag(System.Int32 tagValue) { }
        // RVA: 0x03202310  token: 0x6000162
        public System.Boolean IsSameCollider(UnityEngine.Collider otherCollider) { }
        // RVA: 0x04D5EB60  token: 0x6000163
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000012  // size: 0x50
    public sealed struct RaycastHit
    {
        // Fields
        private UnityEngine.Vector3 m_Point;  // 0x10
        private UnityEngine.Vector3 m_Normal;  // 0x1c
        private System.UInt32 m_FaceID;  // 0x28
        private System.Single m_Distance;  // 0x2c
        private UnityEngine.Vector2 m_UV;  // 0x30
        private System.Int32 m_Collider;  // 0x38
        private System.Int32 m_Index;  // 0x3c
        private System.Int32 m_EcsId;  // 0x40
        private System.UInt32 m_EcsVersion;  // 0x44
        private System.IntPtr m_Actor;  // 0x48

        // Properties
        System.Int32 ecsId { get; /* RVA: 0x04DBA970 */ }
        System.UInt32 ecsVersion { get; /* RVA: 0x04DBA8F0 */ }
        System.Boolean IsECSCollider { get; /* RVA: 0x0406A430 */ }
        UnityEngine.Collider collider { get; /* RVA: 0x03BF0940 */ }
        System.Int32 colliderInstanceID { get; /* RVA: 0x04DBBEC0 */ }
        UnityEngine.Collider editorCollider { get; /* RVA: 0x0B4292C4 */ }
        UnityEngine.ECSColliderResultProxy colliderProxy { get; /* RVA: 0x032048B0 */ }
        UnityEngine.Vector3 point { get; /* RVA: 0x04DBA810 */ set; /* RVA: 0x04DABF80 */ }
        UnityEngine.Vector3 normal { get; /* RVA: 0x04DBA7F0 */ set; /* RVA: 0x04DBAA60 */ }
        UnityEngine.Vector3 barycentricCoordinate { get; /* RVA: 0x04DBD2E0 */ set; /* RVA: 0x0B42971C */ }
        System.Single distance { get; /* RVA: 0x04DBD180 */ set; /* RVA: 0x04D86900 */ }
        System.Int32 triangleIndex { get; /* RVA: 0x04D9D860 */ }
        System.Int32 componentIndex { get; /* RVA: 0x04DBD310 */ }
        UnityEngine.Vector2 textureCoord { get; /* RVA: 0x0B4296C0 */ }
        UnityEngine.Vector2 textureCoord2 { get; /* RVA: 0x0B429660 */ }
        UnityEngine.Transform transform { get; /* RVA: 0x03BF06A0 */ }
        UnityEngine.Vector3 shapePosition { get; /* RVA: 0x0B4295D8 */ }
        UnityEngine.Quaternion shapeRotation { get; /* RVA: 0x0B429618 */ }
        UnityEngine.Vector3 position { get; /* RVA: 0x0B4294C0 */ }
        UnityEngine.Quaternion rotation { get; /* RVA: 0x0B4294F4 */ }
        UnityEngine.Vector3 scale { get; /* RVA: 0x0B429520 */ }
        UnityEngine.Rigidbody rigidbody { get; /* RVA: 0x03BF0600 */ }
        UnityEngine.ArticulationBody articulationBody { get; /* RVA: 0x0B429234 */ }
        UnityEngine.Vector2 lightmapCoord { get; /* RVA: 0x0B429354 */ }
        UnityEngine.Vector2 textureCoord1 { get; /* RVA: 0x0B429650 */ }

        // Methods
        // RVA: 0x0B4291E8  token: 0x6000175
        private static UnityEngine.Vector2 CalculateRaycastTexCoord(UnityEngine.Collider collider, System.Int32 m_Index, UnityEngine.Vector2 uv, UnityEngine.Vector3 pos, System.UInt32 face, System.Int32 textcoord) { }
        // RVA: 0x0B429188  token: 0x6000182
        private static System.Void CalculateRaycastTexCoord_Injected(UnityEngine.Collider collider, System.Int32 m_Index, UnityEngine.Vector2& uv, UnityEngine.Vector3& pos, System.UInt32 face, System.Int32 textcoord, UnityEngine.Vector2& ret) { }

    }

    // TypeToken: 0x2000013  // size: 0x18
    public class Rigidbody : UnityEngine.Component
    {
        // Properties
        UnityEngine.Vector3 velocity { get; /* RVA: 0x0319C6B0 */ set; /* RVA: 0x0B42AF9C */ }
        UnityEngine.Vector3 angularVelocity { get; /* RVA: 0x0319D840 */ set; /* RVA: 0x0B42AA74 */ }
        System.Single drag { get; /* RVA: 0x0319C730 */ set; /* RVA: 0x0B42ABA4 */ }
        System.Single angularDrag { get; /* RVA: 0x0B42A45C */ set; /* RVA: 0x0B42A9EC */ }
        System.Single mass { get; /* RVA: 0x0B42A718 */ set; /* RVA: 0x0B42AD1C */ }
        System.Boolean useGravity { get; /* RVA: 0x0B42A948 */ set; /* RVA: 0x0B42AF14 */ }
        System.Single maxDepenetrationVelocity { get; /* RVA: 0x0B42A780 */ set; /* RVA: 0x0B42ADA4 */ }
        System.Boolean isKinematic { get; /* RVA: 0x0B42A6E4 */ set; /* RVA: 0x0319DBB0 */ }
        System.Boolean freezeRotation { get; /* RVA: 0x0B42A59C */ set; /* RVA: 0x0B42ABE8 */ }
        UnityEngine.RigidbodyConstraints constraints { get; /* RVA: 0x0B42A534 */ set; /* RVA: 0x0B42AB20 */ }
        UnityEngine.CollisionDetectionMode collisionDetectionMode { get; /* RVA: 0x0B42A500 */ set; /* RVA: 0x0B42AAE0 */ }
        UnityEngine.Vector3 centerOfMass { get; /* RVA: 0x0B42A4D4 */ set; /* RVA: 0x0B42AACC */ }
        UnityEngine.Vector3 worldCenterOfMass { get; /* RVA: 0x0B42A9C0 */ }
        UnityEngine.Quaternion inertiaTensorRotation { get; /* RVA: 0x0B42A614 */ set; /* RVA: 0x0B42AC70 */ }
        UnityEngine.Vector3 inertiaTensor { get; /* RVA: 0x0B42A684 */ set; /* RVA: 0x0B42ACC8 */ }
        System.Boolean detectCollisions { get; /* RVA: 0x0B42A568 */ set; /* RVA: 0x0B42AB60 */ }
        UnityEngine.Vector3 position { get; /* RVA: 0x0B42A7F8 */ set; /* RVA: 0x0539183C */ }
        UnityEngine.Quaternion rotation { get; /* RVA: 0x0B42A868 */ set; /* RVA: 0x0B42AE2C */ }
        UnityEngine.RigidbodyInterpolation interpolation { get; /* RVA: 0x0B42A6B0 */ set; /* RVA: 0x0B42ACDC */ }
        System.Int32 solverIterations { get; /* RVA: 0x0B42A8D4 */ set; /* RVA: 0x0B42AE8C */ }
        System.Single sleepThreshold { get; /* RVA: 0x0B42A894 */ set; /* RVA: 0x0B42AE40 */ }
        System.Single maxAngularVelocity { get; /* RVA: 0x0B42A74C */ set; /* RVA: 0x0B42AD60 */ }
        System.Int32 solverVelocityIterations { get; /* RVA: 0x0B42A914 */ set; /* RVA: 0x0B42AED4 */ }
        System.Single sleepVelocity { get; /* RVA: 0x04DBAEC0 */ set; /* RVA: 0x041E1670 */ }
        System.Single sleepAngularVelocity { get; /* RVA: 0x04DBAEC0 */ set; /* RVA: 0x041E1670 */ }
        System.Boolean useConeFriction { get; /* RVA: 0x04D85B20 */ set; /* RVA: 0x041E1670 */ }
        System.Int32 solverIterationCount { get; /* RVA: 0x0B42A8C8 */ set; /* RVA: 0x0B42AE84 */ }
        System.Int32 solverVelocityIterationCount { get; /* RVA: 0x0B42A908 */ set; /* RVA: 0x0B42AECC */ }

        // Methods
        // RVA: 0x0B42A048  token: 0x600018D
        public System.Void SetDensity(System.Single density) { }
        // RVA: 0x0B429F74  token: 0x60001AF
        public System.Void MovePosition(UnityEngine.Vector3 position) { }
        // RVA: 0x0B429FCC  token: 0x60001B0
        public System.Void MoveRotation(UnityEngine.Quaternion rot) { }
        // RVA: 0x0B42A094  token: 0x60001B1
        public System.Void Sleep() { }
        // RVA: 0x0319BF90  token: 0x60001B2
        public System.Boolean IsSleeping() { }
        // RVA: 0x0B42A428  token: 0x60001B3
        public System.Void WakeUp() { }
        // RVA: 0x0B429FE0  token: 0x60001B4
        public System.Void ResetCenterOfMass() { }
        // RVA: 0x0B42A014  token: 0x60001B5
        public System.Void ResetInertiaTensor() { }
        // RVA: 0x0B429E04  token: 0x60001B6
        public UnityEngine.Vector3 GetRelativePointVelocity(UnityEngine.Vector3 relativePoint) { }
        // RVA: 0x0B429D7C  token: 0x60001B7
        public UnityEngine.Vector3 GetPointVelocity(UnityEngine.Vector3 worldPoint) { }
        // RVA: 0x0B4299B8  token: 0x60001BA
        public System.Void AddForce(UnityEngine.Vector3 force, UnityEngine.ForceMode mode) { }
        // RVA: 0x0B42998C  token: 0x60001BB
        public System.Void AddForce(UnityEngine.Vector3 force) { }
        // RVA: 0x0B429960  token: 0x60001BC
        public System.Void AddForce(System.Single x, System.Single y, System.Single z, UnityEngine.ForceMode mode) { }
        // RVA: 0x0B4299CC  token: 0x60001BD
        public System.Void AddForce(System.Single x, System.Single y, System.Single z) { }
        // RVA: 0x0B429A9C  token: 0x60001BE
        public System.Void AddRelativeForce(UnityEngine.Vector3 force, UnityEngine.ForceMode mode) { }
        // RVA: 0x0B429A48  token: 0x60001BF
        public System.Void AddRelativeForce(UnityEngine.Vector3 force) { }
        // RVA: 0x0B429AB0  token: 0x60001C0
        public System.Void AddRelativeForce(System.Single x, System.Single y, System.Single z, UnityEngine.ForceMode mode) { }
        // RVA: 0x0B429A74  token: 0x60001C1
        public System.Void AddRelativeForce(System.Single x, System.Single y, System.Single z) { }
        // RVA: 0x0B429C38  token: 0x60001C2
        public System.Void AddTorque(UnityEngine.Vector3 torque, UnityEngine.ForceMode mode) { }
        // RVA: 0x0B429C0C  token: 0x60001C3
        public System.Void AddTorque(UnityEngine.Vector3 torque) { }
        // RVA: 0x0B429C74  token: 0x60001C4
        public System.Void AddTorque(System.Single x, System.Single y, System.Single z, UnityEngine.ForceMode mode) { }
        // RVA: 0x0B429C4C  token: 0x60001C5
        public System.Void AddTorque(System.Single x, System.Single y, System.Single z) { }
        // RVA: 0x0B429B88  token: 0x60001C6
        public System.Void AddRelativeTorque(UnityEngine.Vector3 torque, UnityEngine.ForceMode mode) { }
        // RVA: 0x0B429B5C  token: 0x60001C7
        public System.Void AddRelativeTorque(UnityEngine.Vector3 torque) { }
        // RVA: 0x0B429B30  token: 0x60001C8
        public System.Void AddRelativeTorque(System.Single x, System.Single y, System.Single z, UnityEngine.ForceMode mode) { }
        // RVA: 0x0B429B9C  token: 0x60001C9
        public System.Void AddRelativeTorque(System.Single x, System.Single y, System.Single z) { }
        // RVA: 0x0B4298AC  token: 0x60001CA
        public System.Void AddForceAtPosition(UnityEngine.Vector3 force, UnityEngine.Vector3 position, UnityEngine.ForceMode mode) { }
        // RVA: 0x0B4298C4  token: 0x60001CB
        public System.Void AddForceAtPosition(UnityEngine.Vector3 force, UnityEngine.Vector3 position) { }
        // RVA: 0x0B42981C  token: 0x60001CC
        public System.Void AddExplosionForce(System.Single explosionForce, UnityEngine.Vector3 explosionPosition, System.Single explosionRadius, System.Single upwardsModifier, UnityEngine.ForceMode mode) { }
        // RVA: 0x0B4297DC  token: 0x60001CD
        public System.Void AddExplosionForce(System.Single explosionForce, UnityEngine.Vector3 explosionPosition, System.Single explosionRadius, System.Single upwardsModifier) { }
        // RVA: 0x0B4297A0  token: 0x60001CE
        public System.Void AddExplosionForce(System.Single explosionForce, UnityEngine.Vector3 explosionPosition, System.Single explosionRadius) { }
        // RVA: 0x0B429E9C  token: 0x60001CF
        private System.Void Internal_ClosestPointOnBounds(UnityEngine.Vector3 point, UnityEngine.Vector3& outPos, System.Single& distance) { }
        // RVA: 0x0B429CA0  token: 0x60001D0
        public UnityEngine.Vector3 ClosestPointOnBounds(UnityEngine.Vector3 position) { }
        // RVA: 0x0B42A288  token: 0x60001D1
        private UnityEngine.RaycastHit SweepTest(UnityEngine.Vector3 direction, System.Single maxDistance, UnityEngine.QueryTriggerInteraction queryTriggerInteraction, System.Boolean& hasHit) { }
        // RVA: 0x0B42A334  token: 0x60001D2
        public System.Boolean SweepTest(UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B42A258  token: 0x60001D3
        public System.Boolean SweepTest(UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance) { }
        // RVA: 0x0B42A2FC  token: 0x60001D4
        public System.Boolean SweepTest(UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo) { }
        // RVA: 0x0B429F18  token: 0x60001D5
        private UnityEngine.RaycastHit[] Internal_SweepTestAll(UnityEngine.Vector3 direction, System.Single maxDistance, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B42A0F8  token: 0x60001D6
        public UnityEngine.RaycastHit[] SweepTestAll(UnityEngine.Vector3 direction, System.Single maxDistance, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B42A0C8  token: 0x60001D7
        public UnityEngine.RaycastHit[] SweepTestAll(UnityEngine.Vector3 direction, System.Single maxDistance) { }
        // RVA: 0x0B42A1BC  token: 0x60001D8
        public UnityEngine.RaycastHit[] SweepTestAll(UnityEngine.Vector3 direction) { }
        // RVA: 0x0B42A08C  token: 0x60001DD
        public System.Void SetMaxAngularVelocity(System.Single a) { }
        // RVA: 0x05393520  token: 0x60001E4
        public System.Void .ctor() { }
        // RVA: 0x0319C6E0  token: 0x60001E5
        private System.Void get_velocity_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x0B42AF58  token: 0x60001E6
        private System.Void set_velocity_Injected(UnityEngine.Vector3& value) { }
        // RVA: 0x0319DC00  token: 0x60001E7
        private System.Void get_angularVelocity_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x0B42AA30  token: 0x60001E8
        private System.Void set_angularVelocity_Injected(UnityEngine.Vector3& value) { }
        // RVA: 0x0B42A490  token: 0x60001E9
        private System.Void get_centerOfMass_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x0B42AA88  token: 0x60001EA
        private System.Void set_centerOfMass_Injected(UnityEngine.Vector3& value) { }
        // RVA: 0x0B42A97C  token: 0x60001EB
        private System.Void get_worldCenterOfMass_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x0B42A5D0  token: 0x60001EC
        private System.Void get_inertiaTensorRotation_Injected(UnityEngine.Quaternion& ret) { }
        // RVA: 0x0B42AC2C  token: 0x60001ED
        private System.Void set_inertiaTensorRotation_Injected(UnityEngine.Quaternion& value) { }
        // RVA: 0x0B42A640  token: 0x60001EE
        private System.Void get_inertiaTensor_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x0B42AC84  token: 0x60001EF
        private System.Void set_inertiaTensor_Injected(UnityEngine.Vector3& value) { }
        // RVA: 0x0B42A7B4  token: 0x60001F0
        private System.Void get_position_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x040487C0  token: 0x60001F1
        private System.Void set_position_Injected(UnityEngine.Vector3& value) { }
        // RVA: 0x0B42A824  token: 0x60001F2
        private System.Void get_rotation_Injected(UnityEngine.Quaternion& ret) { }
        // RVA: 0x0B42ADE8  token: 0x60001F3
        private System.Void set_rotation_Injected(UnityEngine.Quaternion& value) { }
        // RVA: 0x0B429F30  token: 0x60001F4
        private System.Void MovePosition_Injected(UnityEngine.Vector3& position) { }
        // RVA: 0x0B429F88  token: 0x60001F5
        private System.Void MoveRotation_Injected(UnityEngine.Quaternion& rot) { }
        // RVA: 0x0B429DB0  token: 0x60001F6
        private System.Void GetRelativePointVelocity_Injected(UnityEngine.Vector3& relativePoint, UnityEngine.Vector3& ret) { }
        // RVA: 0x0B429D28  token: 0x60001F7
        private System.Void GetPointVelocity_Injected(UnityEngine.Vector3& worldPoint, UnityEngine.Vector3& ret) { }
        // RVA: 0x0B42990C  token: 0x60001F8
        private System.Void AddForce_Injected(UnityEngine.Vector3& force, UnityEngine.ForceMode mode) { }
        // RVA: 0x0B4299F4  token: 0x60001F9
        private System.Void AddRelativeForce_Injected(UnityEngine.Vector3& force, UnityEngine.ForceMode mode) { }
        // RVA: 0x0B429BB8  token: 0x60001FA
        private System.Void AddTorque_Injected(UnityEngine.Vector3& torque, UnityEngine.ForceMode mode) { }
        // RVA: 0x0B429ADC  token: 0x60001FB
        private System.Void AddRelativeTorque_Injected(UnityEngine.Vector3& torque, UnityEngine.ForceMode mode) { }
        // RVA: 0x0B429848  token: 0x60001FC
        private System.Void AddForceAtPosition_Injected(UnityEngine.Vector3& force, UnityEngine.Vector3& position, UnityEngine.ForceMode mode) { }
        // RVA: 0x0B42973C  token: 0x60001FD
        private System.Void AddExplosionForce_Injected(System.Single explosionForce, UnityEngine.Vector3& explosionPosition, System.Single explosionRadius, System.Single upwardsModifier, UnityEngine.ForceMode mode) { }
        // RVA: 0x0B429E38  token: 0x60001FE
        private System.Void Internal_ClosestPointOnBounds_Injected(UnityEngine.Vector3& point, UnityEngine.Vector3& outPos, System.Single& distance) { }
        // RVA: 0x0B42A1F4  token: 0x60001FF
        private System.Void SweepTest_Injected(UnityEngine.Vector3& direction, System.Single maxDistance, UnityEngine.QueryTriggerInteraction queryTriggerInteraction, System.Boolean& hasHit, UnityEngine.RaycastHit& ret) { }
        // RVA: 0x0B429EB4  token: 0x6000200
        private UnityEngine.RaycastHit[] Internal_SweepTestAll_Injected(UnityEngine.Vector3& direction, System.Single maxDistance, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }

    }

    // TypeToken: 0x2000014  // size: 0x14
    public sealed struct ECSColliderType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ECSColliderType Default;  // const
        public static UnityEngine.ECSColliderType BoxCollider;  // const
        public static UnityEngine.ECSColliderType CapsuleCollider;  // const
        public static UnityEngine.ECSColliderType SphereCollider;  // const
        public static UnityEngine.ECSColliderType MeshCollider;  // const
        public static UnityEngine.ECSColliderType MultiCollider;  // const
        public static UnityEngine.ECSColliderType TerrainCollider;  // const
        public static UnityEngine.ECSColliderType ECSCollider;  // const
        public static UnityEngine.ECSColliderType Invalid;  // const

    }

    // TypeToken: 0x2000015  // size: 0x18
    public class Collider : UnityEngine.Component
    {
        // Properties
        System.Boolean enabled { get; /* RVA: 0x036797D0 */ set; /* RVA: 0x034361B0 */ }
        UnityEngine.Rigidbody attachedRigidbody { get; /* RVA: 0x0B41FBD4 */ }
        UnityEngine.ArticulationBody attachedArticulationBody { get; /* RVA: 0x0B41FBA0 */ }
        System.Boolean isTrigger { get; /* RVA: 0x037318A0 */ set; /* RVA: 0x0375FD00 */ }
        System.Boolean enableDither { get; /* RVA: 0x0B41FC3C */ }
        System.Single contactOffset { get; /* RVA: 0x0B41FC08 */ set; /* RVA: 0x0B41FD74 */ }
        UnityEngine.Bounds bounds { get; /* RVA: 0x036783C0 */ }
        System.Boolean hasModifiableContacts { get; /* RVA: 0x0B41FC70 */ set; /* RVA: 0x0B41FDB8 */ }
        UnityEngine.ColliderSurfaceType surfaceType { get; /* RVA: 0x0B41FD0C */ set; /* RVA: 0x0B41FE40 */ }
        System.Int32 tagConfig { get; /* RVA: 0x0B41FD40 */ set; /* RVA: 0x0B41FE80 */ }
        System.Int32 colliderOptions { get; /* RVA: 0x0375FD90 */ set; /* RVA: 0x0375FEE0 */ }
        UnityEngine.PhysicMaterial sharedMaterial { get; /* RVA: 0x0B41FCD8 */ set; /* RVA: 0x0B41FDFC */ }
        UnityEngine.PhysicMaterial material { get; /* RVA: 0x0B41FCA4 */ set; /* RVA: 0x03679410 */ }

        // Methods
        // RVA: 0x03677D00  token: 0x600020A
        public UnityEngine.Vector3 ClosestPoint(UnityEngine.Vector3 position) { }
        // RVA: 0x03679620  token: 0x6000214
        public System.IntPtr GetActor() { }
        // RVA: 0x03F51100  token: 0x6000215
        public System.Int32 GetComponentIndex() { }
        // RVA: 0x0B41F880  token: 0x6000216
        public System.Int32 GetFactoryBuildingIdentifier() { }
        // RVA: 0x0B41FA6C  token: 0x6000217
        public System.Void SetFactoryBuildingIdentifier(System.Int32 value) { }
        // RVA: 0x0B41F994  token: 0x600021C
        private UnityEngine.RaycastHit Raycast(UnityEngine.Ray ray, System.Single maxDistance, System.Boolean& hasHit) { }
        // RVA: 0x0B41F9F8  token: 0x600021D
        public System.Boolean Raycast(UnityEngine.Ray ray, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance) { }
        // RVA: 0x0B41F918  token: 0x600021E
        private System.Void Internal_ClosestPointOnBounds(UnityEngine.Vector3 point, UnityEngine.Vector3& outPos, System.Single& distance) { }
        // RVA: 0x0B41F7F8  token: 0x600021F
        public UnityEngine.Vector3 ClosestPointOnBounds(UnityEngine.Vector3 position) { }
        // RVA: 0x0B41F7C4  token: 0x6000220
        public System.Boolean CanColliderComputeClosestPoint() { }
        // RVA: 0x0B41FB10  token: 0x6000221
        private UnityEngine.Vector3 TryGetClosestPoint_Internal(UnityEngine.Vector3 point, System.Boolean& resultIsValid) { }
        // RVA: 0x0B41FB4C  token: 0x6000222
        public System.Boolean TryGetClosestPoint(UnityEngine.Vector3 point, UnityEngine.Vector3& result) { }
        // RVA: 0x05393520  token: 0x6000223
        public System.Void .ctor() { }
        // RVA: 0x03677D40  token: 0x6000224
        private System.Void ClosestPoint_Injected(UnityEngine.Vector3& position, UnityEngine.Vector3& ret) { }
        // RVA: 0x036783F0  token: 0x6000225
        private System.Void get_bounds_Injected(UnityEngine.Bounds& ret) { }
        // RVA: 0x0B41F930  token: 0x6000226
        private System.Void Raycast_Injected(UnityEngine.Ray& ray, System.Single maxDistance, System.Boolean& hasHit, UnityEngine.RaycastHit& ret) { }
        // RVA: 0x0B41F8B4  token: 0x6000227
        private System.Void Internal_ClosestPointOnBounds_Injected(UnityEngine.Vector3& point, UnityEngine.Vector3& outPos, System.Single& distance) { }
        // RVA: 0x0B41FAAC  token: 0x6000228
        private System.Void TryGetClosestPoint_Internal_Injected(UnityEngine.Vector3& point, System.Boolean& resultIsValid, UnityEngine.Vector3& ret) { }

    }

    // TypeToken: 0x2000016  // size: 0x18
    public class ECSCollider
    {
        // Fields
        private System.IntPtr self;  // 0x10
        public static UnityEngine.ECSCollider.CollisionCallback collisionEnterCallback;  // static @ 0x0
        public static UnityEngine.ECSCollider.CollisionCallback collisionStayCallback;  // static @ 0x8
        public static UnityEngine.ECSCollider.CollisionCallback collisionExitCallback;  // static @ 0x10
        public static UnityEngine.ECSCollider.CollisionCallback triggerEnterCallback;  // static @ 0x18
        public static UnityEngine.ECSCollider.CollisionCallback triggerStayCallback;  // static @ 0x20
        public static UnityEngine.ECSCollider.CollisionCallback triggerExitCallback;  // static @ 0x28

        // Methods
        // RVA: 0x0B42098C  token: 0x6000229
        public static System.IntPtr CreateActor(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, System.UInt32 layer, System.Int32 tagInt, System.Boolean movable) { }
        // RVA: 0x0B4209AC  token: 0x600022A
        public static System.Void DestroyActor(System.Int32 id, System.UInt32 version) { }
        // RVA: 0x034C79D0  token: 0x600022B
        public static System.IntPtr Decode(System.IntPtr data, System.Int32 length, System.IntPtr meshes, System.Int32 meshesCount, UnityEngine.Texture2D heightmaps) { }
        // RVA: 0x034C7940  token: 0x600022C
        public static System.IntPtr Decode(System.IntPtr data, System.Int32 length, Unity.Collections.NativeArray<System.Int32> meshInstanceIds, UnityEngine.Texture2D heightmap) { }
        // RVA: 0x05392C94  token: 0x600022D
        public static System.Void RecordEntity(System.IntPtr actor, UnityEngine.HyperGryph.ECS.Entity entity) { }
        // RVA: 0x034C7A40  token: 0x600022E
        public static System.Int32 GetId(System.IntPtr actor) { }
        // RVA: 0x034C7A80  token: 0x600022F
        public static System.UInt32 GetVersion(System.IntPtr actor) { }
        // RVA: 0x032052C0  token: 0x6000230
        public static System.Boolean IsValid(System.Int32 id, System.UInt32 version, System.IntPtr actor) { }
        // RVA: 0x0444B1A0  token: 0x6000231
        public static UnityEngine.HyperGryph.ECS.Entity GetEntity(System.Int32 id, System.UInt32 version) { }
        // RVA: 0x03678E80  token: 0x6000232
        public static UnityEngine.Vector3 GetPosition(System.Int32 ecsId, System.UInt32 ecsVersion) { }
        // RVA: 0x044C6EF0  token: 0x6000233
        public static UnityEngine.Vector3 GetShapePosition(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 index) { }
        // RVA: 0x05392CB0  token: 0x6000234
        public static System.Void SetPosition(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3 position) { }
        // RVA: 0x0B420A4C  token: 0x6000235
        public static UnityEngine.Quaternion GetShapeRotation(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 index) { }
        // RVA: 0x048150F0  token: 0x6000236
        public static UnityEngine.Quaternion GetRotation(System.Int32 ecsId, System.UInt32 ecsVersion) { }
        // RVA: 0x05392CC4  token: 0x6000237
        public static System.Void SetRotation(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Quaternion rotation) { }
        // RVA: 0x0B4207EC  token: 0x6000238
        public static System.Void AttachBox(System.Int32 id, System.UInt32 version, UnityEngine.Vector3 halfExtent, UnityEngine.Vector3 localPosition, UnityEngine.Quaternion localRotation, System.Int32 surfaceType, System.Int32 colliderOption, System.Int32 tag, System.Boolean isTrigger, System.Boolean isPrimitive) { }
        // RVA: 0x03203C80  token: 0x6000239
        public static System.Boolean HasComplexMesh(System.Int32 id, System.UInt32 version) { }
        // RVA: 0x0B420D28  token: 0x600023A
        private static UnityEngine.RaycastHit Raycast_Internal(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Ray ray, System.Single maxDistance, System.Boolean hitHoles, System.Boolean& hasHit) { }
        // RVA: 0x0B420D9C  token: 0x600023B
        public static System.Boolean Raycast(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Ray ray, System.Single maxDistance, System.Boolean hitHoles, UnityEngine.RaycastHit& hitInfo) { }
        // RVA: 0x041183B0  token: 0x600023C
        private static UnityEngine.RaycastHit RaycastComplexMesh_Internal(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Ray ray, System.Single maxDistance, System.Boolean hitHoles, System.Boolean& hasHit) { }
        // RVA: 0x041182B0  token: 0x600023D
        public static System.Boolean RaycastComplexMesh(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Ray ray, System.Single maxDistance, System.Boolean hitHoles, UnityEngine.RaycastHit& hitInfo) { }
        // RVA: 0x030F0360  token: 0x600023E
        private static UnityEngine.RaycastHit CapsuleCastComplexMesh_Internal(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Boolean& hasHit) { }
        // RVA: 0x030F0210  token: 0x600023F
        public static System.Boolean CapsuleCastComplexMesh(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, UnityEngine.RaycastHit& hitInfo) { }
        // RVA: 0x030EF280  token: 0x6000240
        private static UnityEngine.RaycastHit SphereCastComplexMesh_Internal(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3 center, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Boolean& hasHit) { }
        // RVA: 0x030EF160  token: 0x6000241
        public static System.Boolean SphereCastComplexMesh(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3 center, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, UnityEngine.RaycastHit& hitInfo) { }
        // RVA: 0x0B42084C  token: 0x6000242
        public static System.Boolean CanColliderComputeClosestPoint(System.Int32 ecsId, System.UInt32 ecsVersion) { }
        // RVA: 0x03288490  token: 0x6000243
        public static UnityEngine.ColliderSurfaceType GetSurfaceTypeAt(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 index) { }
        // RVA: 0x03205480  token: 0x6000244
        public static System.Int32 GetColliderOptionAt(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 index) { }
        // RVA: 0x034E7200  token: 0x6000245
        public static System.Int32 GetTagConfigAt(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 index) { }
        // RVA: 0x043F3060  token: 0x6000246
        public static System.Boolean GetIsTrigger(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 index) { }
        // RVA: 0x03678440  token: 0x6000247
        public static UnityEngine.Bounds GetBounds(System.Int32 ecsId, System.UInt32 ecsVersion) { }
        // RVA: 0x0B420E88  token: 0x6000248
        private static UnityEngine.Vector3 TryGetClosestPoint_Internal(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3 point, System.Boolean& resultIsValid) { }
        // RVA: 0x0B420EC8  token: 0x6000249
        public static System.Boolean TryGetClosestPoint(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3 point, UnityEngine.Vector3& result) { }
        // RVA: 0x0B4208EC  token: 0x600024A
        public static UnityEngine.Vector3 ClosestPoint(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3 position) { }
        // RVA: 0x03675AF0  token: 0x600024B
        public static System.Boolean CompareTag(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 tagValue) { }
        // RVA: 0x030F5240  token: 0x600024C
        public static System.Int32 GetLayer(System.Int32 ecsId, System.UInt32 ecsVersion) { }
        // RVA: 0x0406A3E0  token: 0x600024D
        public static System.Int32 GetFactoryBuildingIdentifier(System.Int32 ecsId, System.UInt32 ecsVersion) { }
        // RVA: 0x034C4BF0  token: 0x600024E
        public static System.Void SetFactoryBuildingIdentifier(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 identifier) { }
        // RVA: 0x0B420A88  token: 0x600024F
        private static System.Void OnCollisionEnterV2(UnityEngine.ECSColliderResultProxy thisCollider, UnityEngine.ECSColliderResultProxy otherCollider) { }
        // RVA: 0x0B420BA8  token: 0x6000250
        private static System.Void OnCollisionStayV2(UnityEngine.ECSColliderResultProxy thisCollider, UnityEngine.ECSColliderResultProxy otherCollider) { }
        // RVA: 0x0B420B18  token: 0x6000251
        private static System.Void OnCollisionExitV2(UnityEngine.ECSColliderResultProxy thisCollider, UnityEngine.ECSColliderResultProxy otherCollider) { }
        // RVA: 0x0484F800  token: 0x6000252
        private static System.Void OnTriggerEnterV2(UnityEngine.ECSColliderResultProxy thisCollider, UnityEngine.ECSColliderResultProxy otherCollider) { }
        // RVA: 0x0B420C38  token: 0x6000253
        private static System.Void OnTriggerStayV2(UnityEngine.ECSColliderResultProxy thisCollider, UnityEngine.ECSColliderResultProxy otherCollider) { }
        // RVA: 0x04CA3010  token: 0x6000254
        private static System.Void OnTriggerExitV2(UnityEngine.ECSColliderResultProxy thisCollider, UnityEngine.ECSColliderResultProxy otherCollider) { }
        // RVA: 0x034A2210  token: 0x6000255
        public static System.Void ToggleCollision(System.Int32 id, System.UInt32 version, System.Boolean enableCollision) { }
        // RVA: 0x0B420928  token: 0x6000256
        private static System.IntPtr CreateActor_Injected(UnityEngine.Vector3& position, UnityEngine.Quaternion& rotation, System.UInt32 layer, System.Int32 tagInt, System.Boolean movable) { }
        // RVA: 0x04258D10  token: 0x6000257
        private static System.Void RecordEntity_Injected(System.IntPtr actor, UnityEngine.HyperGryph.ECS.Entity& entity) { }
        // RVA: 0x0444B200  token: 0x6000258
        private static System.Void GetEntity_Injected(System.Int32 id, System.UInt32 version, UnityEngine.HyperGryph.ECS.Entity& ret) { }
        // RVA: 0x03678EB0  token: 0x6000259
        private static System.Void GetPosition_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3& ret) { }
        // RVA: 0x044C6F30  token: 0x600025A
        private static System.Void GetShapePosition_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 index, UnityEngine.Vector3& ret) { }
        // RVA: 0x04191270  token: 0x600025B
        private static System.Void SetPosition_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3& position) { }
        // RVA: 0x0B4209EC  token: 0x600025C
        private static System.Void GetShapeRotation_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, System.Int32 index, UnityEngine.Quaternion& ret) { }
        // RVA: 0x04815120  token: 0x600025D
        private static System.Void GetRotation_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Quaternion& ret) { }
        // RVA: 0x04191210  token: 0x600025E
        private static System.Void SetRotation_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Quaternion& rotation) { }
        // RVA: 0x0B42078C  token: 0x600025F
        private static System.Void AttachBox_Injected(System.Int32 id, System.UInt32 version, UnityEngine.Vector3& halfExtent, UnityEngine.Vector3& localPosition, UnityEngine.Quaternion& localRotation, System.Int32 surfaceType, System.Int32 colliderOption, System.Int32 tag, System.Boolean isTrigger, System.Boolean isPrimitive) { }
        // RVA: 0x0B420CC8  token: 0x6000260
        private static System.Void Raycast_Internal_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Ray& ray, System.Single maxDistance, System.Boolean hitHoles, System.Boolean& hasHit, UnityEngine.RaycastHit& ret) { }
        // RVA: 0x04118420  token: 0x6000261
        private static System.Void RaycastComplexMesh_Internal_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Ray& ray, System.Single maxDistance, System.Boolean hitHoles, System.Boolean& hasHit, UnityEngine.RaycastHit& ret) { }
        // RVA: 0x030F03F0  token: 0x6000262
        private static System.Void CapsuleCastComplexMesh_Internal_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3& p0, UnityEngine.Vector3& p1, System.Single radius, UnityEngine.Vector3& direction, System.Single maxDistance, System.Boolean& hasHit, UnityEngine.RaycastHit& ret) { }
        // RVA: 0x030EF300  token: 0x6000263
        private static System.Void SphereCastComplexMesh_Internal_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3& center, System.Single radius, UnityEngine.Vector3& direction, System.Single maxDistance, System.Boolean& hasHit, UnityEngine.RaycastHit& ret) { }
        // RVA: 0x03678480  token: 0x6000264
        private static System.Void GetBounds_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Bounds& ret) { }
        // RVA: 0x0B420E28  token: 0x6000265
        private static System.Void TryGetClosestPoint_Internal_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3& point, System.Boolean& resultIsValid, UnityEngine.Vector3& ret) { }
        // RVA: 0x0B42088C  token: 0x6000266
        private static System.Void ClosestPoint_Injected(System.Int32 ecsId, System.UInt32 ecsVersion, UnityEngine.Vector3& position, UnityEngine.Vector3& ret) { }

    }

    // TypeToken: 0x2000018  // size: 0x18
    public class CharacterController : UnityEngine.Collider
    {
        // Properties
        System.Boolean isGrounded { get; /* RVA: 0x0B41F790 */ }

    }

    // TypeToken: 0x2000019  // size: 0x18
    public class MultiCollider : UnityEngine.Collider
    {
        // Properties
        UnityEngine.Mesh ComplexMesh { get; /* RVA: 0x0B4216EC */ }

        // Methods
        // RVA: 0x0B421104  token: 0x600026A
        public System.Void AppendBoxCollider(UnityEngine.BoxCollider boxCollider) { }
        // RVA: 0x0B421148  token: 0x600026B
        public System.Void AppendCapsuleCollider(UnityEngine.CapsuleCollider collider) { }
        // RVA: 0x0B42118C  token: 0x600026C
        public System.Void AppendSphereCollider(UnityEngine.SphereCollider collider) { }
        // RVA: 0x0B42141C  token: 0x600026D
        public UnityEngine.ColliderSurfaceType GetSurfaceTypeAt(System.Int32 index) { }
        // RVA: 0x0B42145C  token: 0x600026E
        public System.Int32 GetTagConfigAt(System.Int32 index) { }
        // RVA: 0x03C828D0  token: 0x600026F
        public System.Int32 GetGameplayOptionAt(System.Int32 index) { }
        // RVA: 0x0B4213DC  token: 0x6000270
        public System.Boolean GetIsTriggerAt(System.Int32 index) { }
        // RVA: 0x0B421518  token: 0x6000271
        public System.Void SetGameplayOptionAt(System.Int32 index, System.Int32 value) { }
        // RVA: 0x0B421374  token: 0x6000272
        public System.Void Flush() { }
        // RVA: 0x0B421500  token: 0x6000274
        public System.Boolean RaycastComplexMesh(UnityEngine.Ray ray, System.Single maxDistance, UnityEngine.RaycastHit& hit) { }
        // RVA: 0x0B421234  token: 0x6000275
        private UnityEngine.RaycastHit CapsuleCastComplexMesh_Internal(UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Boolean& hasHit) { }
        // RVA: 0x0B4212C4  token: 0x6000276
        public System.Boolean CapsuleCastComplexMesh(UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, UnityEngine.RaycastHit& hit) { }
        // RVA: 0x0B4215CC  token: 0x6000277
        private UnityEngine.RaycastHit SphereCastComplexMesh_Internal(UnityEngine.Vector3 center, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, System.Boolean& hasHit) { }
        // RVA: 0x0B42164C  token: 0x6000278
        public System.Boolean SphereCastComplexMesh(UnityEngine.Vector3 center, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, UnityEngine.RaycastHit& hit) { }
        // RVA: 0x0B4213A8  token: 0x6000279
        public System.Int32 GetCountWithoutComplexMesh() { }
        // RVA: 0x0B42149C  token: 0x600027A
        private System.Boolean RaycastComplexMesh_Injected(UnityEngine.Ray& ray, System.Single maxDistance, UnityEngine.RaycastHit& hit) { }
        // RVA: 0x0B4211D0  token: 0x600027B
        private System.Void CapsuleCastComplexMesh_Internal_Injected(UnityEngine.Vector3& p0, UnityEngine.Vector3& p1, System.Single radius, UnityEngine.Vector3& direction, System.Single maxDistance, System.Boolean& hasHit, UnityEngine.RaycastHit& ret) { }
        // RVA: 0x0B421568  token: 0x600027C
        private System.Void SphereCastComplexMesh_Internal_Injected(UnityEngine.Vector3& center, System.Single radius, UnityEngine.Vector3& direction, System.Single maxDistance, System.Boolean& hasHit, UnityEngine.RaycastHit& ret) { }

    }

    // TypeToken: 0x200001A  // size: 0x18
    public class MeshCollider : UnityEngine.Collider
    {
        // Properties
        UnityEngine.Mesh sharedMesh { get; /* RVA: 0x0B42104C */ set; /* RVA: 0x03C57D60 */ }
        System.Boolean convex { get; /* RVA: 0x0B420FE4 */ set; /* RVA: 0x0B421080 */ }
        UnityEngine.MeshColliderCookingOptions cookingOptions { get; /* RVA: 0x0B421018 */ set; /* RVA: 0x0B4210C4 */ }
        System.Boolean smoothSphereCollisions { get; /* RVA: 0x04D9AE10 */ set; /* RVA: 0x041E1670 */ }
        System.Single skinWidth { get; /* RVA: 0x04DBAEC0 */ set; /* RVA: 0x041E1670 */ }
        System.Boolean inflateMesh { get; /* RVA: 0x04D85B20 */ set; /* RVA: 0x041E1670 */ }

        // Methods
        // RVA: 0x0B420FB0  token: 0x6000283
        public System.Boolean NeedsReadWriteEnabled() { }
        // RVA: 0x05393520  token: 0x600028A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001B  // size: 0x18
    public class CapsuleCollider : UnityEngine.Collider
    {
        // Properties
        UnityEngine.Vector3 center { get; /* RVA: 0x031FC060 */ set; /* RVA: 0x05393430 */ }
        System.Single radius { get; /* RVA: 0x031FC020 */ set; /* RVA: 0x03679320 */ }
        System.Single height { get; /* RVA: 0x031FBFE0 */ set; /* RVA: 0x03679370 */ }
        System.Int32 direction { get; /* RVA: 0x031FBFA0 */ set; /* RVA: 0x036793C0 */ }

        // Methods
        // RVA: 0x031FC0C0  token: 0x6000293
        private System.Void get_center_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x04486C50  token: 0x6000294
        private System.Void set_center_Injected(UnityEngine.Vector3& value) { }

    }

    // TypeToken: 0x200001C  // size: 0x18
    public class BoxCollider : UnityEngine.Collider
    {
        // Properties
        UnityEngine.Vector3 center { get; /* RVA: 0x0B41F41C */ set; /* RVA: 0x05396B24 */ }
        UnityEngine.Vector3 size { get; /* RVA: 0x03FB9410 */ set; /* RVA: 0x05396B10 */ }

        // Methods
        // RVA: 0x0B41F3D8  token: 0x6000299
        private System.Void get_center_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x036CCA20  token: 0x600029A
        private System.Void set_center_Injected(UnityEngine.Vector3& value) { }
        // RVA: 0x03FB9440  token: 0x600029B
        private System.Void get_size_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x036CC9D0  token: 0x600029C
        private System.Void set_size_Injected(UnityEngine.Vector3& value) { }

    }

    // TypeToken: 0x200001D  // size: 0x18
    public class SphereCollider : UnityEngine.Collider
    {
        // Properties
        UnityEngine.Vector3 center { get; /* RVA: 0x0B42AFF4 */ set; /* RVA: 0x0B42B054 */ }
        System.Single radius { get; /* RVA: 0x0B42B020 */ set; /* RVA: 0x0375FE40 */ }

        // Methods
        // RVA: 0x0B42AFB0  token: 0x60002A1
        private System.Void get_center_Injected(UnityEngine.Vector3& ret) { }
        // RVA: 0x04D30C40  token: 0x60002A2
        private System.Void set_center_Injected(UnityEngine.Vector3& value) { }

    }

    // TypeToken: 0x200001E  // size: 0x18
    public class Joint : UnityEngine.Component
    {
        // Properties
        UnityEngine.Rigidbody connectedBody { get; /* RVA: 0x0B420F24 */ }
        UnityEngine.Vector3 connectedAnchor { set; /* RVA: 0x0B420F9C */ }

        // Methods
        // RVA: 0x0B420F58  token: 0x60002A5
        private System.Void set_connectedAnchor_Injected(UnityEngine.Vector3& value) { }

    }

    // TypeToken: 0x200001F  // size: 0x34
    public sealed struct ContactPoint
    {
        // Fields
        private UnityEngine.Vector3 m_Point;  // 0x10
        private UnityEngine.Vector3 m_Normal;  // 0x1c
        private System.Int32 m_ThisColliderInstanceID;  // 0x28
        private System.Int32 m_OtherColliderInstanceID;  // 0x2c
        private System.Single m_Separation;  // 0x30

        // Properties
        UnityEngine.Vector3 point { get; /* RVA: 0x04DBA810 */ }
        UnityEngine.Vector3 normal { get; /* RVA: 0x04DBA7F0 */ }
        UnityEngine.Collider thisCollider { get; /* RVA: 0x0B4200F0 */ }
        UnityEngine.Collider otherCollider { get; /* RVA: 0x0B4200E4 */ }

        // Methods
        // RVA: 0x0B4200B0  token: 0x60002AA
        private static UnityEngine.Collider GetColliderByInstanceID(System.Int32 instanceID) { }

    }

    // TypeToken: 0x2000020  // size: 0x14
    public sealed struct PhysicsScene : System.IEquatable`1
    {
        // Fields
        private System.Int32 m_Handle;  // 0x10

        // Methods
        // RVA: 0x0B4224E8  token: 0x60002AB
        public virtual System.String ToString() { }
        // RVA: 0x04DBA750  token: 0x60002AC
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B4219F4  token: 0x60002AD
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x02EED260  token: 0x60002AE
        public virtual System.Boolean Equals(UnityEngine.PhysicsScene other) { }
        // RVA: 0x0B4223F0  token: 0x60002AF
        public System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B421DC8  token: 0x60002B0
        private static System.Boolean Internal_RaycastTest(UnityEngine.PhysicsScene physicsScene, UnityEngine.Ray ray, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x03206EF0  token: 0x60002B1
        public System.Boolean Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x053918B8  token: 0x60002B2
        private static System.Boolean Internal_Raycast(UnityEngine.PhysicsScene physicsScene, UnityEngine.Ray ray, System.Single maxDistance, UnityEngine.RaycastHit& hit, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x03206BF0  token: 0x60002B3
        public System.Int32 Raycast(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] raycastHits, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x05391924  token: 0x60002B4
        private static System.Int32 Internal_RaycastNonAlloc(UnityEngine.PhysicsScene physicsScene, UnityEngine.Ray ray, UnityEngine.RaycastHit[] raycastHits, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B422390  token: 0x60002B5
        private static System.Boolean Query_CapsuleCast(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, UnityEngine.RaycastHit& hitInfo, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B421C44  token: 0x60002B6
        private static System.Boolean Internal_CapsuleCast(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B421958  token: 0x60002B7
        public System.Boolean CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x053910C8  token: 0x60002B8
        private static System.Int32 Internal_CapsuleCastNonAlloc(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] raycastHits, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x030F20F0  token: 0x60002B9
        public System.Int32 CapsuleCast(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B421BE4  token: 0x60002BA
        private static System.Int32 Internal_CapsuleCastNonAllocV2(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 p0, UnityEngine.Vector3 p1, System.Single radius, UnityEngine.Vector3 diretion, UnityEngine.ECSColliderResultProxy[] proxies, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B421864  token: 0x60002BB
        public System.Int32 CapsuleCastV2(UnityEngine.Vector3 point1, UnityEngine.Vector3 point2, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.ECSColliderResultProxy[] proxies, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B421FEC  token: 0x60002BC
        private static System.Int32 OverlapCapsuleNonAlloc_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B42202C  token: 0x60002BD
        public System.Int32 OverlapCapsule(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x05391870  token: 0x60002BE
        private static System.Int32 OverlapCapsuleNonAlloc_InternalV2(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x031FFB20  token: 0x60002BF
        public System.Int32 OverlapCapsuleV2(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x053936F0  token: 0x60002C0
        private static System.Boolean Query_SphereCast(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, System.Single maxDistance, UnityEngine.RaycastHit& hitInfo, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x03730780  token: 0x60002C1
        private static System.Boolean Internal_SphereCast(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x037305E0  token: 0x60002C2
        public System.Boolean SphereCast(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x05391128  token: 0x60002C3
        private static System.Int32 Internal_SphereCastNonAlloc(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] raycastHits, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x030F5A30  token: 0x60002C4
        public System.Int32 SphereCast(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B422174  token: 0x60002C5
        private static System.Int32 OverlapSphereNonAlloc_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 position, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B4221A4  token: 0x60002C6
        public System.Int32 OverlapSphere(UnityEngine.Vector3 position, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x05395C38  token: 0x60002C7
        private static System.Int32 OverlapSphereNonAllocV2_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 position, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x03D7C100  token: 0x60002C8
        public System.Int32 OverlapSphereV2(UnityEngine.Vector3 position, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B4222CC  token: 0x60002C9
        private static System.Boolean Query_BoxCast(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.Quaternion orientation, System.Single maxDistance, UnityEngine.RaycastHit& outHit, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B421A58  token: 0x60002CA
        private static System.Boolean Internal_BoxCast(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B4217B8  token: 0x60002CB
        public System.Boolean BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit& hitInfo, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B421E54  token: 0x60002CC
        private static System.Int32 OverlapBoxNonAlloc_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Collider[] results, UnityEngine.Quaternion orientation, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B421E94  token: 0x60002CD
        public System.Int32 OverlapBox(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Collider[] results, UnityEngine.Quaternion orientation, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x05397930  token: 0x60002CE
        private static System.Int32 OverlapBoxNonAllocV2_Internal(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.ECSColliderResultProxy[] results, UnityEngine.Quaternion orientation, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x045ADDF0  token: 0x60002CF
        public System.Int32 OverlapBoxV2(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.ECSColliderResultProxy[] results, UnityEngine.Quaternion orientation, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x05390DC0  token: 0x60002D0
        private static System.Int32 Internal_BoxCastNonAlloc(UnityEngine.PhysicsScene physicsScene, UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] raycastHits, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x03027940  token: 0x60002D1
        public System.Int32 BoxCast(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Vector3 direction, UnityEngine.RaycastHit[] results, UnityEngine.Quaternion orientation, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B421D64  token: 0x60002D2
        private static System.Boolean Internal_RaycastTest_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Ray& ray, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x03C5DB50  token: 0x60002D3
        private static System.Boolean Internal_Raycast_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Ray& ray, System.Single maxDistance, UnityEngine.RaycastHit& hit, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x03C6CD70  token: 0x60002D4
        private static System.Int32 Internal_RaycastNonAlloc_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Ray& ray, UnityEngine.RaycastHit[] raycastHits, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B42232C  token: 0x60002D5
        private static System.Boolean Query_CapsuleCast_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& point1, UnityEngine.Vector3& point2, System.Single radius, UnityEngine.Vector3& direction, System.Single maxDistance, UnityEngine.RaycastHit& hitInfo, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x03EE4C10  token: 0x60002D6
        private static System.Int32 Internal_CapsuleCastNonAlloc_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& p0, UnityEngine.Vector3& p1, System.Single radius, UnityEngine.Vector3& direction, UnityEngine.RaycastHit[] raycastHits, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B421B80  token: 0x60002D7
        private static System.Int32 Internal_CapsuleCastNonAllocV2_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& p0, UnityEngine.Vector3& p1, System.Single radius, UnityEngine.Vector3& diretion, UnityEngine.ECSColliderResultProxy[] proxies, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B421F88  token: 0x60002D8
        private static System.Int32 OverlapCapsuleNonAlloc_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& point0, UnityEngine.Vector3& point1, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x03DEFB30  token: 0x60002D9
        private static System.Int32 OverlapCapsuleNonAlloc_InternalV2_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& point0, UnityEngine.Vector3& point1, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x03F08EB0  token: 0x60002DA
        private static System.Boolean Query_SphereCast_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& origin, System.Single radius, UnityEngine.Vector3& direction, System.Single maxDistance, UnityEngine.RaycastHit& hitInfo, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x03D6D910  token: 0x60002DB
        private static System.Int32 Internal_SphereCastNonAlloc_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& origin, System.Single radius, UnityEngine.Vector3& direction, UnityEngine.RaycastHit[] raycastHits, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B422110  token: 0x60002DC
        private static System.Int32 OverlapSphereNonAlloc_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& position, System.Single radius, UnityEngine.Collider[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0438EDE0  token: 0x60002DD
        private static System.Int32 OverlapSphereNonAllocV2_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& position, System.Single radius, UnityEngine.ECSColliderResultProxy[] results, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B422268  token: 0x60002DE
        private static System.Boolean Query_BoxCast_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& center, UnityEngine.Vector3& halfExtents, UnityEngine.Vector3& direction, UnityEngine.Quaternion& orientation, System.Single maxDistance, UnityEngine.RaycastHit& outHit, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x0B421DF0  token: 0x60002DF
        private static System.Int32 OverlapBoxNonAlloc_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& center, UnityEngine.Vector3& halfExtents, UnityEngine.Collider[] results, UnityEngine.Quaternion& orientation, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x045ADEA0  token: 0x60002E0
        private static System.Int32 OverlapBoxNonAllocV2_Internal_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& center, UnityEngine.Vector3& halfExtents, UnityEngine.ECSColliderResultProxy[] results, UnityEngine.Quaternion& orientation, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }
        // RVA: 0x03CE61C0  token: 0x60002E1
        private static System.Int32 Internal_BoxCastNonAlloc_Injected(UnityEngine.PhysicsScene& physicsScene, UnityEngine.Vector3& center, UnityEngine.Vector3& halfExtents, UnityEngine.Vector3& direction, UnityEngine.RaycastHit[] raycastHits, UnityEngine.Quaternion& orientation, System.Single maxDistance, System.Int32 mask, UnityEngine.QueryTriggerInteraction queryTriggerInteraction) { }

    }

    // TypeToken: 0x2000021  // size: 0x1C
    public sealed struct QueryParameters
    {
        // Fields
        public System.Int32 layerMask;  // 0x10
        public System.Boolean hitMultipleFaces;  // 0x14
        public System.Boolean hitBackfaces;  // 0x15
        public UnityEngine.QueryTriggerInteraction hitTriggers;  // 0x18

        // Methods
        // RVA: 0x04DBD2A0  token: 0x60002E2
        public System.Void .ctor(System.Int32 layerMask, System.Boolean hitMultipleFaces, UnityEngine.QueryTriggerInteraction hitTriggers, System.Boolean hitBackfaces) { }

    }

    // TypeToken: 0x2000022  // size: 0x3C
    public sealed struct RaycastCommand
    {
        // Fields
        private UnityEngine.Vector3 <from>k__BackingField;  // 0x10
        private UnityEngine.Vector3 <direction>k__BackingField;  // 0x1c
        private System.Single <distance>k__BackingField;  // 0x28
        private UnityEngine.PhysicsScene <physicsScene>k__BackingField;  // 0x2c
        public UnityEngine.QueryParameters queryParameters;  // 0x30

        // Properties
        UnityEngine.Vector3 from { get; /* RVA: 0x04D93250 */ set; /* RVA: 0x04DABF80 */ }
        UnityEngine.Vector3 direction { get; /* RVA: 0x04DBD2C0 */ set; /* RVA: 0x04DBAA60 */ }
        System.Single distance { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x04D86390 */ }
        UnityEngine.PhysicsScene physicsScene { set; /* RVA: 0x04D86320 */ }

        // Methods
        // RVA: 0x0309E080  token: 0x60002E3
        public System.Void .ctor(UnityEngine.Vector3 from, UnityEngine.Vector3 direction, UnityEngine.QueryParameters queryParameters, System.Single distance) { }
        // RVA: 0x0309DDB0  token: 0x60002EB
        public static Unity.Jobs.JobHandle ScheduleBatch(Unity.Collections.NativeArray<UnityEngine.RaycastCommand> commands, Unity.Collections.NativeArray<UnityEngine.RaycastHit> results, System.Int32 minCommandsPerJob, System.Int32 maxHits, Unity.Jobs.JobHandle dependsOn) { }
        // RVA: 0x0309DF90  token: 0x60002EC
        public static Unity.Jobs.JobHandle ScheduleBatch(Unity.Collections.NativeArray<UnityEngine.RaycastCommand> commands, Unity.Collections.NativeArray<UnityEngine.RaycastHit> results, System.Int32 minCommandsPerJob, Unity.Jobs.JobHandle dependsOn) { }
        // RVA: 0x0309DEF0  token: 0x60002ED
        private static Unity.Jobs.JobHandle ScheduleRaycastBatch(Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters& parameters, System.Void* commands, System.Int32 commandLen, System.Void* result, System.Int32 resultLen, System.Int32 minCommandsPerJob, System.Int32 maxHits) { }
        // RVA: 0x0309DD40  token: 0x60002EE
        private static System.Void ScheduleRaycastBatch_Injected(Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters& parameters, System.Void* commands, System.Int32 commandLen, System.Void* result, System.Int32 resultLen, System.Int32 minCommandsPerJob, System.Int32 maxHits, Unity.Jobs.JobHandle& ret) { }

    }

    // TypeToken: 0x2000023  // size: 0x40
    public sealed struct SpherecastCommand
    {
        // Fields
        private UnityEngine.Vector3 <origin>k__BackingField;  // 0x10
        private System.Single <radius>k__BackingField;  // 0x1c
        private UnityEngine.Vector3 <direction>k__BackingField;  // 0x20
        private System.Single <distance>k__BackingField;  // 0x2c
        private UnityEngine.PhysicsScene <physicsScene>k__BackingField;  // 0x30
        public UnityEngine.QueryParameters queryParameters;  // 0x34

        // Properties
        UnityEngine.Vector3 origin { set; /* RVA: 0x04DABF80 */ }
        System.Single radius { set; /* RVA: 0x04DACD70 */ }
        UnityEngine.Vector3 direction { set; /* RVA: 0x04D8C970 */ }
        System.Single distance { set; /* RVA: 0x04D86900 */ }
        UnityEngine.PhysicsScene physicsScene { set; /* RVA: 0x04D86330 */ }

        // Methods
        // RVA: 0x0B42B33C  token: 0x60002EF
        public System.Void .ctor(UnityEngine.Vector3 origin, System.Single radius, UnityEngine.Vector3 direction, UnityEngine.QueryParameters queryParameters, System.Single distance) { }
        // RVA: 0x0B42B0DC  token: 0x60002F5
        public static Unity.Jobs.JobHandle ScheduleBatch(Unity.Collections.NativeArray<UnityEngine.SpherecastCommand> commands, Unity.Collections.NativeArray<UnityEngine.RaycastHit> results, System.Int32 minCommandsPerJob, System.Int32 maxHits, Unity.Jobs.JobHandle dependsOn) { }
        // RVA: 0x0B42B068  token: 0x60002F6
        public static Unity.Jobs.JobHandle ScheduleBatch(Unity.Collections.NativeArray<UnityEngine.SpherecastCommand> commands, Unity.Collections.NativeArray<UnityEngine.RaycastHit> results, System.Int32 minCommandsPerJob, Unity.Jobs.JobHandle dependsOn) { }
        // RVA: 0x0B42B2D0  token: 0x60002F7
        private static Unity.Jobs.JobHandle ScheduleSpherecastBatch(Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters& parameters, System.Void* commands, System.Int32 commandLen, System.Void* result, System.Int32 resultLen, System.Int32 minCommandsPerJob, System.Int32 maxHits) { }
        // RVA: 0x0B42B26C  token: 0x60002F8
        private static System.Void ScheduleSpherecastBatch_Injected(Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters& parameters, System.Void* commands, System.Int32 commandLen, System.Void* result, System.Int32 resultLen, System.Int32 minCommandsPerJob, System.Int32 maxHits, Unity.Jobs.JobHandle& ret) { }

    }

    // TypeToken: 0x2000024  // size: 0x58
    public sealed struct BoxcastCommand
    {
        // Fields
        private UnityEngine.Vector3 <center>k__BackingField;  // 0x10
        private UnityEngine.Vector3 <halfExtents>k__BackingField;  // 0x1c
        private UnityEngine.Quaternion <orientation>k__BackingField;  // 0x28
        private UnityEngine.Vector3 <direction>k__BackingField;  // 0x38
        private System.Single <distance>k__BackingField;  // 0x44
        private UnityEngine.PhysicsScene <physicsScene>k__BackingField;  // 0x48
        public UnityEngine.QueryParameters queryParameters;  // 0x4c

        // Properties
        UnityEngine.Vector3 center { set; /* RVA: 0x04DABF80 */ }
        UnityEngine.Vector3 halfExtents { set; /* RVA: 0x04DBAA60 */ }
        UnityEngine.Quaternion orientation { set; /* RVA: 0x04D890D0 */ }
        UnityEngine.Vector3 direction { set; /* RVA: 0x04D90C20 */ }
        System.Single distance { set; /* RVA: 0x04D8E060 */ }
        UnityEngine.PhysicsScene physicsScene { set; /* RVA: 0x04D86430 */ }

        // Methods
        // RVA: 0x0B41F71C  token: 0x60002F9
        public System.Void .ctor(UnityEngine.Vector3 center, UnityEngine.Vector3 halfExtents, UnityEngine.Quaternion orientation, UnityEngine.Vector3 direction, UnityEngine.QueryParameters queryParameters, System.Single distance) { }
        // RVA: 0x0B41F4BC  token: 0x6000300
        public static Unity.Jobs.JobHandle ScheduleBatch(Unity.Collections.NativeArray<UnityEngine.BoxcastCommand> commands, Unity.Collections.NativeArray<UnityEngine.RaycastHit> results, System.Int32 minCommandsPerJob, System.Int32 maxHits, Unity.Jobs.JobHandle dependsOn) { }
        // RVA: 0x0B41F448  token: 0x6000301
        public static Unity.Jobs.JobHandle ScheduleBatch(Unity.Collections.NativeArray<UnityEngine.BoxcastCommand> commands, Unity.Collections.NativeArray<UnityEngine.RaycastHit> results, System.Int32 minCommandsPerJob, Unity.Jobs.JobHandle dependsOn) { }
        // RVA: 0x0B41F6B0  token: 0x6000302
        private static Unity.Jobs.JobHandle ScheduleBoxcastBatch(Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters& parameters, System.Void* commands, System.Int32 commandLen, System.Void* result, System.Int32 resultLen, System.Int32 minCommandsPerJob, System.Int32 maxHits) { }
        // RVA: 0x0B41F64C  token: 0x6000303
        private static System.Void ScheduleBoxcastBatch_Injected(Unity.Jobs.LowLevel.Unsafe.JobsUtility.JobScheduleParameters& parameters, System.Void* commands, System.Int32 commandLen, System.Void* result, System.Int32 resultLen, System.Int32 minCommandsPerJob, System.Int32 maxHits, Unity.Jobs.JobHandle& ret) { }

    }

}

