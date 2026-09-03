// ========================================================
// Dumped by @desirepro
// Assembly: HoudiniEngineUnity.VisualScripting.dll
// Classes:  7
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

CLASS: <>c__DisplayClass13_0
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x28
FIELDS:
  public            Unity.VisualScripting.Flow      flow  // 0x10
  public            HoudiniEngineUnity.HEU_InstantiateHDA<>4__this  // 0x18
  public            System.Action                   ContinueFlow  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000008  System.Void .ctor()
  RVA=0x09D554E8  token=0x6000009  System.Void <TriggerButton>b__0()
  RVA=0x09D55538  token=0x600000A  System.Void <TriggerButton>b__1(HoudiniEngineUnity.HEU_ReloadEventData data)
END_CLASS

CLASS: <>c__DisplayClass7_0
TYPE:  sealed class
TOKEN: 0x2000005
SIZE:  0x20
FIELDS:
  public            Unity.VisualScripting.Flow      flow  // 0x10
  public            HoudiniEngineUnity.HEU_ModifyHDA<>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000010  System.Void .ctor()
  RVA=0x09D55570  token=0x6000011  System.Void <TriggerButton>b__0()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_InstantiateHDA
TYPE:  class
TOKEN: 0x2000002
SIZE:  0xD0
EXTENDS: Unity.VisualScripting.ManualEventUnit`1
FIELDS:
  public            HoudiniEngineUnity.HEU_UnitButtontriggerButton  // 0x80
  public            Unity.VisualScripting.ValueInputinputPath  // 0x88
  public            Unity.VisualScripting.ValueInputinputAsync  // 0x90
  public            Unity.VisualScripting.ValueInputinputPosition  // 0x98
  public            Unity.VisualScripting.ValueOutputoutputHDARoot  // 0xa0
  public            Unity.VisualScripting.ValueOutputoutputHDAAsset  // 0xa8
  public            Unity.VisualScripting.ValueOutputoutputSuccess  // 0xb0
  private           HoudiniEngineUnity.HEU_HoudiniAssetRoothdaRoot  // 0xb8
  private           HoudiniEngineUnity.HEU_HoudiniAssethdaAsset  // 0xc0
  private           System.Boolean                  bSuccess  // 0xc8
PROPERTIES:
  hookName  get=0x09D551A0
METHODS:
  RVA=0x09D54A94  token=0x6000002  System.Void Definition()
  RVA=0x09D54D70  token=0x6000003  System.Void TriggerButton(Unity.VisualScripting.GraphReference reference)
  RVA=0x09D55164  token=0x6000004  System.Void .ctor()
  RVA=0x04D85EF0  token=0x6000005  HoudiniEngineUnity.HEU_HoudiniAssetRoot <Definition>b__12_0(Unity.VisualScripting.Flow flow)
  RVA=0x04D892F0  token=0x6000006  HoudiniEngineUnity.HEU_HoudiniAsset <Definition>b__12_1(Unity.VisualScripting.Flow flow)
  RVA=0x04D8E270  token=0x6000007  System.Boolean <Definition>b__12_2(Unity.VisualScripting.Flow flow)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_ModifyHDA
TYPE:  class
TOKEN: 0x2000004
SIZE:  0xA0
EXTENDS: Unity.VisualScripting.ManualEventUnit`1
FIELDS:
  public            HoudiniEngineUnity.HEU_UnitButtontriggerButton  // 0x80
  public            Unity.VisualScripting.ValueInputinputHDA  // 0x88
  public            Unity.VisualScripting.ValueOutputoutputHDAAsset  // 0x90
  private           HoudiniEngineUnity.HEU_HoudiniAssethdaAsset  // 0x98
PROPERTIES:
  hookName  get=0x09D554BC
METHODS:
  RVA=0x09D551CC  token=0x600000C  System.Void Definition()
  RVA=0x09D552E0  token=0x600000D  System.Void TriggerButton(Unity.VisualScripting.GraphReference reference)
  RVA=0x09D55480  token=0x600000E  System.Void .ctor()
  RVA=0x04D85EE0  token=0x600000F  HoudiniEngineUnity.HEU_HoudiniAsset <Definition>b__6_0(Unity.VisualScripting.Flow flow)
END_CLASS

CLASS: HoudiniEngineUnity.HEU_UnitButton
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x18
FIELDS:
  public            System.Action                   action  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000012  System.Void .ctor()
END_CLASS

CLASS: HoudiniEngineUnity.HEU_UnitButtonAttribute
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x28
EXTENDS: System.Attribute
FIELDS:
  public            System.String                   functionName  // 0x10
  public            System.String                   buttonLabel  // 0x18
  public            System.Int32                    buttonWidth  // 0x20
METHODS:
  RVA=0x047E1B40  token=0x6000013  System.Void .ctor(System.String fnName, System.String btnLabel, System.Int32 btnWidth)
END_CLASS

