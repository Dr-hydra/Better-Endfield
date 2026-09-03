// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.GridModule.dll
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

    // TypeToken: 0x2000002  // size: 0x18
    public sealed class Grid : UnityEngine.GridLayout
    {
        // Properties
        UnityEngine.Vector3 cellSize { get; /* RVA: 0x0B3F723C */ }

        // Methods
        // RVA: 0x05393520  token: 0x6000002
        public System.Void .ctor() { }
        // RVA: 0x0B3F71F8  token: 0x6000003
        private System.Void get_cellSize_Injected(UnityEngine.Vector3& ret) { }

    }

    // TypeToken: 0x2000003  // size: 0x18
    public class GridLayout : UnityEngine.Behaviour
    {
        // Methods
        // RVA: 0x0B3F71C4  token: 0x6000004
        public UnityEngine.Vector3 CellToLocal(UnityEngine.Vector3Int cellPosition) { }
        // RVA: 0x041E1670  token: 0x6000005
        private System.Void DoNothing() { }
        // RVA: 0x05393520  token: 0x6000006
        public System.Void .ctor() { }
        // RVA: 0x0B3F7170  token: 0x6000007
        private System.Void CellToLocal_Injected(UnityEngine.Vector3Int& cellPosition, UnityEngine.Vector3& ret) { }

    }

}

