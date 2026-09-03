// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.TerrainPhysicsModule.dll
// Classes:  3
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

    // TypeToken: 0x2000002  // size: 0x44
    public sealed struct HGTerrainRaycastHit
    {
        // Fields
        private UnityEngine.Vector3 m_Position;  // 0x10
        private UnityEngine.Vector3 m_Normal;  // 0x1c
        private System.Single m_Distance;  // 0x28
        private UnityEngine.Vector3 m_Edge01;  // 0x2c
        private UnityEngine.Vector3 m_Edge02;  // 0x38

        // Properties
        UnityEngine.Vector3 position { get; /* RVA: 0x04DBA810 */ }
        UnityEngine.Vector3 normal { get; /* RVA: 0x04DBA7F0 */ }
        UnityEngine.Vector3 edge01 { get; /* RVA: 0x04DBA870 */ }
        UnityEngine.Vector3 edge02 { get; /* RVA: 0x04DBA850 */ }
        System.Single distance { get; /* RVA: 0x04DBAB50 */ }

    }

    // TypeToken: 0x2000003  // size: 0x18
    public class TerrainCollider : UnityEngine.Collider
    {
        // Properties
        UnityEngine.TerrainData terrainData { set; /* RVA: 0x0B430B80 */ }

        // Methods
        // RVA: 0x0B430AB0  token: 0x6000007
        private UnityEngine.HGTerrainRaycastHit HGTerrainRaycast(UnityEngine.Ray ray, System.Single maxDistance, System.Boolean hitHoles, System.Boolean& hasHit) { }
        // RVA: 0x0B430B00  token: 0x6000008
        public System.Boolean HGTerrainRaycast(UnityEngine.Ray ray, UnityEngine.HGTerrainRaycastHit& hitInfo, System.Single maxDistance, System.Boolean hitHoles) { }
        // RVA: 0x0B430A4C  token: 0x6000009
        private System.Void HGTerrainRaycast_Injected(UnityEngine.Ray& ray, System.Single maxDistance, System.Boolean hitHoles, System.Boolean& hasHit, UnityEngine.HGTerrainRaycastHit& ret) { }

    }

}

