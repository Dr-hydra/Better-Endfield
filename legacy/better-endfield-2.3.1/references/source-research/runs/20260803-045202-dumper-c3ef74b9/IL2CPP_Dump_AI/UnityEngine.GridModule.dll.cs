// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.GridModule.dll
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

CLASS: UnityEngine.Grid
TYPE:  sealed class
TOKEN: 0x2000002
SIZE:  0x18
EXTENDS: UnityEngine.GridLayout
FIELDS:
PROPERTIES:
  cellSize  get=0x0B3F723C
METHODS:
  RVA=0x05393520  token=0x6000002  System.Void .ctor()
  RVA=0x0B3F71F8  token=0x6000003  System.Void get_cellSize_Injected(UnityEngine.Vector3& ret)
END_CLASS

CLASS: UnityEngine.GridLayout
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x18
EXTENDS: UnityEngine.Behaviour
FIELDS:
METHODS:
  RVA=0x0B3F71C4  token=0x6000004  UnityEngine.Vector3 CellToLocal(UnityEngine.Vector3Int cellPosition)
  RVA=0x041E1670  token=0x6000005  System.Void DoNothing()
  RVA=0x05393520  token=0x6000006  System.Void .ctor()
  RVA=0x0B3F7170  token=0x6000007  System.Void CellToLocal_Injected(UnityEngine.Vector3Int& cellPosition, UnityEngine.Vector3& ret)
END_CLASS

