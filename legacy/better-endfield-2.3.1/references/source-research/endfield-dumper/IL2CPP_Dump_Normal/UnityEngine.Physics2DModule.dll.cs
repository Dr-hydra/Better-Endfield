// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.Physics2DModule.dll
// Classes:  16
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

namespace UnityEngine
{

    // TypeToken: 0x2000002  // size: 0x14
    public sealed struct PhysicsScene2D : System.IEquatable`1
    {
        // Fields
        private System.Int32 m_Handle;  // 0x10

        // Methods
        // RVA: 0x0B41EFF8  token: 0x6000001
        public virtual System.String ToString() { }
        // RVA: 0x04DBA750  token: 0x6000002
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B41E80C  token: 0x6000003
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x02EED260  token: 0x6000004
        public virtual System.Boolean Equals(UnityEngine.PhysicsScene2D other) { }
        // RVA: 0x0B41EB54  token: 0x6000005
        public UnityEngine.RaycastHit2D Linecast(UnityEngine.Vector2 start, UnityEngine.Vector2 end, UnityEngine.ContactFilter2D contactFilter) { }
        // RVA: 0x0B41EB04  token: 0x6000006
        private static UnityEngine.RaycastHit2D Linecast_Internal(UnityEngine.PhysicsScene2D physicsScene, UnityEngine.Vector2 start, UnityEngine.Vector2 end, UnityEngine.ContactFilter2D contactFilter) { }
        // RVA: 0x0B41EE90  token: 0x6000007
        public UnityEngine.RaycastHit2D Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, System.Int32 layerMask) { }
        // RVA: 0x0B41EF70  token: 0x6000008
        public UnityEngine.RaycastHit2D Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, UnityEngine.ContactFilter2D contactFilter) { }
        // RVA: 0x0B41ED90  token: 0x6000009
        private static UnityEngine.RaycastHit2D Raycast_Internal(UnityEngine.PhysicsScene2D physicsScene, UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, UnityEngine.ContactFilter2D contactFilter) { }
        // RVA: 0x0B41EDF0  token: 0x600000A
        public System.Int32 Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, UnityEngine.ContactFilter2D contactFilter, UnityEngine.RaycastHit2D[] results) { }
        // RVA: 0x0B41EC30  token: 0x600000B
        private static System.Int32 RaycastArray_Internal(UnityEngine.PhysicsScene2D physicsScene, UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, UnityEngine.ContactFilter2D contactFilter, UnityEngine.RaycastHit2D[] results) { }
        // RVA: 0x0B41EE40  token: 0x600000C
        public System.Int32 Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, UnityEngine.ContactFilter2D contactFilter, System.Collections.Generic.List<UnityEngine.RaycastHit2D> results) { }
        // RVA: 0x0B41ECE0  token: 0x600000D
        private static System.Int32 RaycastList_Internal(UnityEngine.PhysicsScene2D physicsScene, UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, UnityEngine.ContactFilter2D contactFilter, System.Collections.Generic.List<UnityEngine.RaycastHit2D> results) { }
        // RVA: 0x0B41E9B8  token: 0x600000E
        public UnityEngine.RaycastHit2D GetRayIntersection(UnityEngine.Ray ray, System.Single distance, System.Int32 layerMask) { }
        // RVA: 0x0B41E96C  token: 0x600000F
        private static UnityEngine.RaycastHit2D GetRayIntersection_Internal(UnityEngine.PhysicsScene2D physicsScene, UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single distance, System.Int32 layerMask) { }
        // RVA: 0x0B41EA48  token: 0x6000010
        public System.Int32 GetRayIntersection(UnityEngine.Ray ray, System.Single distance, UnityEngine.RaycastHit2D[] results, System.Int32 layerMask) { }
        // RVA: 0x0B41E8D4  token: 0x6000011
        private static System.Int32 GetRayIntersectionArray_Internal(UnityEngine.PhysicsScene2D physicsScene, UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single distance, System.Int32 layerMask, UnityEngine.RaycastHit2D[] results) { }
        // RVA: 0x0B41EAA0  token: 0x6000012
        private static System.Void Linecast_Internal_Injected(UnityEngine.PhysicsScene2D& physicsScene, UnityEngine.Vector2& start, UnityEngine.Vector2& end, UnityEngine.ContactFilter2D& contactFilter, UnityEngine.RaycastHit2D& ret) { }
        // RVA: 0x0B41ED2C  token: 0x6000013
        private static System.Void Raycast_Internal_Injected(UnityEngine.PhysicsScene2D& physicsScene, UnityEngine.Vector2& origin, UnityEngine.Vector2& direction, System.Single distance, UnityEngine.ContactFilter2D& contactFilter, UnityEngine.RaycastHit2D& ret) { }
        // RVA: 0x0B41EBCC  token: 0x6000014
        private static System.Int32 RaycastArray_Internal_Injected(UnityEngine.PhysicsScene2D& physicsScene, UnityEngine.Vector2& origin, UnityEngine.Vector2& direction, System.Single distance, UnityEngine.ContactFilter2D& contactFilter, UnityEngine.RaycastHit2D[] results) { }
        // RVA: 0x0B41EC7C  token: 0x6000015
        private static System.Int32 RaycastList_Internal_Injected(UnityEngine.PhysicsScene2D& physicsScene, UnityEngine.Vector2& origin, UnityEngine.Vector2& direction, System.Single distance, UnityEngine.ContactFilter2D& contactFilter, System.Collections.Generic.List<UnityEngine.RaycastHit2D> results) { }
        // RVA: 0x0B41E908  token: 0x6000016
        private static System.Void GetRayIntersection_Internal_Injected(UnityEngine.PhysicsScene2D& physicsScene, UnityEngine.Vector3& origin, UnityEngine.Vector3& direction, System.Single distance, System.Int32 layerMask, UnityEngine.RaycastHit2D& ret) { }
        // RVA: 0x0B41E870  token: 0x6000017
        private static System.Int32 GetRayIntersectionArray_Internal_Injected(UnityEngine.PhysicsScene2D& physicsScene, UnityEngine.Vector3& origin, UnityEngine.Vector3& direction, System.Single distance, System.Int32 layerMask, UnityEngine.RaycastHit2D[] results) { }

    }

    // TypeToken: 0x2000003  // size: 0x10
    public class Physics2D
    {
        // Fields
        private static System.Collections.Generic.List<UnityEngine.Rigidbody2D> m_LastDisabledRigidbody2D;  // static @ 0x0

        // Properties
        UnityEngine.PhysicsScene2D defaultPhysicsScene { get; /* RVA: 0x04DA17C0 */ }
        UnityEngine.Vector2 gravity { set; /* RVA: 0x0B41E7C0 */ }
        System.Boolean queriesHitTriggers { get; /* RVA: 0x0B41E760 */ }

        // Methods
        // RVA: 0x0B41DE64  token: 0x600001B
        public static UnityEngine.RaycastHit2D Linecast(UnityEngine.Vector2 start, UnityEngine.Vector2 end, System.Int32 layerMask) { }
        // RVA: 0x0B41DD98  token: 0x600001C
        public static UnityEngine.RaycastHit2D[] LinecastAll(UnityEngine.Vector2 start, UnityEngine.Vector2 end, System.Int32 layerMask) { }
        // RVA: 0x0B41DD34  token: 0x600001D
        private static UnityEngine.RaycastHit2D[] LinecastAll_Internal(UnityEngine.PhysicsScene2D physicsScene, UnityEngine.Vector2 start, UnityEngine.Vector2 end, UnityEngine.ContactFilter2D contactFilter) { }
        // RVA: 0x0B41E610  token: 0x600001E
        public static UnityEngine.RaycastHit2D Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction) { }
        // RVA: 0x0B41E354  token: 0x600001F
        public static UnityEngine.RaycastHit2D Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance) { }
        // RVA: 0x0B41E164  token: 0x6000020
        public static UnityEngine.RaycastHit2D Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, System.Int32 layerMask) { }
        // RVA: 0x0B41E4E4  token: 0x6000021
        public static UnityEngine.RaycastHit2D Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, System.Int32 layerMask, System.Single minDepth) { }
        // RVA: 0x0B41DF78  token: 0x6000022
        public static UnityEngine.RaycastHit2D Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, System.Single distance, System.Int32 layerMask, System.Single minDepth, System.Single maxDepth) { }
        // RVA: 0x0B41E0A4  token: 0x6000023
        public static System.Int32 Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, UnityEngine.ContactFilter2D contactFilter, UnityEngine.RaycastHit2D[] results) { }
        // RVA: 0x0B41E290  token: 0x6000024
        public static System.Int32 Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, UnityEngine.ContactFilter2D contactFilter, UnityEngine.RaycastHit2D[] results, System.Single distance) { }
        // RVA: 0x0B41E420  token: 0x6000025
        public static System.Int32 Raycast(UnityEngine.Vector2 origin, UnityEngine.Vector2 direction, UnityEngine.ContactFilter2D contactFilter, System.Collections.Generic.List<UnityEngine.RaycastHit2D> results, System.Single distance) { }
        // RVA: 0x0B41DC04  token: 0x6000026
        public static UnityEngine.RaycastHit2D GetRayIntersection(UnityEngine.Ray ray, System.Single distance, System.Int32 layerMask) { }
        // RVA: 0x0B41D9CC  token: 0x6000027
        public static UnityEngine.RaycastHit2D[] GetRayIntersectionAll(UnityEngine.Ray ray) { }
        // RVA: 0x0B41D940  token: 0x6000028
        public static UnityEngine.RaycastHit2D[] GetRayIntersectionAll(UnityEngine.Ray ray, System.Single distance) { }
        // RVA: 0x0B41D8AC  token: 0x6000029
        public static UnityEngine.RaycastHit2D[] GetRayIntersectionAll(UnityEngine.Ray ray, System.Single distance, System.Int32 layerMask) { }
        // RVA: 0x0B41D834  token: 0x600002A
        private static UnityEngine.RaycastHit2D[] GetRayIntersectionAll_Internal(UnityEngine.PhysicsScene2D physicsScene, UnityEngine.Vector3 origin, UnityEngine.Vector3 direction, System.Single distance, System.Int32 layerMask) { }
        // RVA: 0x0B41DB80  token: 0x600002B
        public static System.Int32 GetRayIntersectionNonAlloc(UnityEngine.Ray ray, UnityEngine.RaycastHit2D[] results) { }
        // RVA: 0x0B41DA50  token: 0x600002C
        public static System.Int32 GetRayIntersectionNonAlloc(UnityEngine.Ray ray, UnityEngine.RaycastHit2D[] results, System.Single distance) { }
        // RVA: 0x0B41DAE4  token: 0x600002D
        public static System.Int32 GetRayIntersectionNonAlloc(UnityEngine.Ray ray, UnityEngine.RaycastHit2D[] results, System.Single distance, System.Int32 layerMask) { }
        // RVA: 0x0B41E6D4  token: 0x600002E
        private static System.Void .cctor() { }
        // RVA: 0x0B41E78C  token: 0x600002F
        private static System.Void set_gravity_Injected(UnityEngine.Vector2& value) { }
        // RVA: 0x0B41DCD0  token: 0x6000030
        private static UnityEngine.RaycastHit2D[] LinecastAll_Internal_Injected(UnityEngine.PhysicsScene2D& physicsScene, UnityEngine.Vector2& start, UnityEngine.Vector2& end, UnityEngine.ContactFilter2D& contactFilter) { }
        // RVA: 0x0B41D7D0  token: 0x6000031
        private static UnityEngine.RaycastHit2D[] GetRayIntersectionAll_Internal_Injected(UnityEngine.PhysicsScene2D& physicsScene, UnityEngine.Vector3& origin, UnityEngine.Vector3& direction, System.Single distance, System.Int32 layerMask) { }

    }

    // TypeToken: 0x2000004  // size: 0x14
    public sealed struct RigidbodyInterpolation2D
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.RigidbodyInterpolation2D None;  // const
        public static UnityEngine.RigidbodyInterpolation2D Interpolate;  // const
        public static UnityEngine.RigidbodyInterpolation2D Extrapolate;  // const

    }

    // TypeToken: 0x2000005  // size: 0x14
    public sealed struct RigidbodySleepMode2D
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.RigidbodySleepMode2D NeverSleep;  // const
        public static UnityEngine.RigidbodySleepMode2D StartAwake;  // const
        public static UnityEngine.RigidbodySleepMode2D StartAsleep;  // const

    }

    // TypeToken: 0x2000006  // size: 0x14
    public sealed struct CollisionDetectionMode2D
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.CollisionDetectionMode2D None;  // const
        public static UnityEngine.CollisionDetectionMode2D Discrete;  // const
        public static UnityEngine.CollisionDetectionMode2D Continuous;  // const

    }

    // TypeToken: 0x2000007  // size: 0x2C
    public sealed struct ContactFilter2D
    {
        // Fields
        public System.Boolean useTriggers;  // 0x10
        public System.Boolean useLayerMask;  // 0x11
        public System.Boolean useDepth;  // 0x12
        public System.Boolean useOutsideDepth;  // 0x13
        public System.Boolean useNormalAngle;  // 0x14
        public System.Boolean useOutsideNormalAngle;  // 0x15
        public UnityEngine.LayerMask layerMask;  // 0x18
        public System.Single minDepth;  // 0x1c
        public System.Single maxDepth;  // 0x20
        public System.Single minNormalAngle;  // 0x24
        public System.Single maxNormalAngle;  // 0x28

        // Methods
        // RVA: 0x0B41D630  token: 0x6000032
        private System.Void CheckConsistency() { }
        // RVA: 0x04DBD250  token: 0x6000033
        public System.Void SetLayerMask(UnityEngine.LayerMask layerMask) { }
        // RVA: 0x0B41D6F8  token: 0x6000034
        public System.Void SetDepth(System.Single minDepth, System.Single maxDepth) { }
        // RVA: 0x0B41D638  token: 0x6000035
        private static UnityEngine.ContactFilter2D CreateLegacyFilter(System.Int32 layerMask, System.Single minDepth, System.Single maxDepth) { }
        // RVA: 0x0B41D5FC  token: 0x6000036
        private static System.Void CheckConsistency_Injected(UnityEngine.ContactFilter2D& _unity_self) { }

    }

    // TypeToken: 0x2000008  // size: 0x40
    public class Collision2D
    {
        // Fields
        private System.Int32 m_Collider;  // 0x10
        private System.Int32 m_OtherCollider;  // 0x14
        private System.Int32 m_Rigidbody;  // 0x18
        private System.Int32 m_OtherRigidbody;  // 0x1c
        private UnityEngine.Vector2 m_RelativeVelocity;  // 0x20
        private System.Int32 m_Enabled;  // 0x28
        private System.Int32 m_ContactCount;  // 0x2c
        private UnityEngine.ContactPoint2D[] m_ReusedContacts;  // 0x30
        private UnityEngine.ContactPoint2D[] m_LegacyContacts;  // 0x38

        // Properties
        UnityEngine.Collider2D collider { get; /* RVA: 0x0B41D224 */ }
        UnityEngine.Rigidbody2D rigidbody { get; /* RVA: 0x0B41D370 */ }
        UnityEngine.GameObject gameObject { get; /* RVA: 0x0B41D2EC */ }
        UnityEngine.Vector2 relativeVelocity { get; /* RVA: 0x04DBD230 */ }
        UnityEngine.ContactPoint2D[] contacts { get; /* RVA: 0x0B41D284 */ }

    }

    // TypeToken: 0x2000009  // size: 0x48
    public sealed struct ContactPoint2D
    {
        // Fields
        private UnityEngine.Vector2 m_Point;  // 0x10
        private UnityEngine.Vector2 m_Normal;  // 0x18
        private UnityEngine.Vector2 m_RelativeVelocity;  // 0x20
        private System.Single m_Separation;  // 0x28
        private System.Single m_NormalImpulse;  // 0x2c
        private System.Single m_TangentImpulse;  // 0x30
        private System.Int32 m_Collider;  // 0x34
        private System.Int32 m_OtherCollider;  // 0x38
        private System.Int32 m_Rigidbody;  // 0x3c
        private System.Int32 m_OtherRigidbody;  // 0x40
        private System.Int32 m_Enabled;  // 0x44

        // Properties
        UnityEngine.Vector2 point { get; /* RVA: 0x02FB0860 */ }
        UnityEngine.Vector2 normal { get; /* RVA: 0x04DBAC30 */ }
        UnityEngine.Collider2D collider { get; /* RVA: 0x0B41D710 */ }
        UnityEngine.Collider2D otherCollider { get; /* RVA: 0x0B41D770 */ }

    }

    // TypeToken: 0x200000A  // size: 0x34
    public sealed struct RaycastHit2D
    {
        // Fields
        private UnityEngine.Vector2 m_Centroid;  // 0x10
        private UnityEngine.Vector2 m_Point;  // 0x18
        private UnityEngine.Vector2 m_Normal;  // 0x20
        private System.Single m_Distance;  // 0x28
        private System.Single m_Fraction;  // 0x2c
        private System.Int32 m_Collider;  // 0x30

        // Properties
        UnityEngine.Vector2 point { get; /* RVA: 0x04DBAC30 */ }
        UnityEngine.Vector2 normal { get; /* RVA: 0x04DBD260 */ }
        System.Single distance { get; /* RVA: 0x04DBAB50 */ }
        System.Single fraction { get; /* RVA: 0x04DBD180 */ }
        UnityEngine.Collider2D collider { get; /* RVA: 0x0B41F290 */ }

    }

    // TypeToken: 0x200000B  // size: 0x18
    public sealed class Rigidbody2D : UnityEngine.Component
    {
        // Properties
        UnityEngine.Vector2 velocity { get; /* RVA: 0x0B41F368 */ }
        System.Single mass { get; /* RVA: 0x0B41F2F0 */ }

        // Methods
        // RVA: 0x05393520  token: 0x6000047
        public System.Void .ctor() { }
        // RVA: 0x0B41F324  token: 0x6000048
        private System.Void get_velocity_Injected(UnityEngine.Vector2& ret) { }

    }

    // TypeToken: 0x200000C  // size: 0x18
    public class Collider2D : UnityEngine.Behaviour
    {
        // Properties
        UnityEngine.Vector2 offset { get; /* RVA: 0x0B41D200 */ }
        UnityEngine.Rigidbody2D attachedRigidbody { get; /* RVA: 0x0B41D188 */ }

        // Methods
        // RVA: 0x0B41D16C  token: 0x600004B
        public System.Boolean OverlapPoint(UnityEngine.Vector2 point) { }
        // RVA: 0x0B41D1BC  token: 0x600004C
        private System.Void get_offset_Injected(UnityEngine.Vector2& ret) { }
        // RVA: 0x0B41D128  token: 0x600004D
        private System.Boolean OverlapPoint_Injected(UnityEngine.Vector2& point) { }

    }

    // TypeToken: 0x200000D  // size: 0x18
    public sealed class CircleCollider2D : UnityEngine.Collider2D
    {
    }

    // TypeToken: 0x200000E  // size: 0x18
    public sealed class PolygonCollider2D : UnityEngine.Collider2D
    {
        // Properties
        System.Int32 pathCount { get; /* RVA: 0x0B41F25C */ }

        // Methods
        // RVA: 0x0B41F228  token: 0x600004E
        public System.Int32 GetTotalPointCount() { }
        // RVA: 0x0B41F0E8  token: 0x6000050
        public UnityEngine.Vector2[] GetPath(System.Int32 index) { }
        // RVA: 0x0B41F0A8  token: 0x6000051
        private UnityEngine.Vector2[] GetPath_Internal(System.Int32 index) { }

    }

    // TypeToken: 0x200000F  // size: 0x18
    public sealed class CompositeCollider2D : UnityEngine.Collider2D
    {
        // Properties
        System.Int32 pathCount { get; /* RVA: 0x0B41D594 */ }
        System.Int32 pointCount { get; /* RVA: 0x0B41D5C8 */ }

        // Methods
        // RVA: 0x0B41D430  token: 0x6000054
        public System.Int32 GetPath(System.Int32 index, UnityEngine.Vector2[] points) { }
        // RVA: 0x0B41D3E0  token: 0x6000055
        private System.Int32 GetPathArray_Internal(System.Int32 index, UnityEngine.Vector2[] points) { }

    }

    // TypeToken: 0x2000010  // size: 0x18
    public sealed class PhysicsMaterial2D : UnityEngine.Object
    {
    }

}

