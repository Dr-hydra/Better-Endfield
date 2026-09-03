// ========================================================
// Dumped by @desirepro
// Assembly: Unity.VisualScripting.Flow.dll
// Classes:  54
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

CLASS: DebugData
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x30
EXTENDS: Unity.VisualScripting.UnitConnectionDebugData
FIELDS:
  private           System.Object                   <lastValue>k__BackingField  // 0x20
  private           System.Boolean                  <assignedLastValue>k__BackingField  // 0x28
PROPERTIES:
  lastValue  set=0x05390F40
  assignedLastValue  set=0x011F33D0
METHODS:
END_CLASS

CLASS: RecursionNode
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private   readonly Unity.VisualScripting.IUnitPort <port>k__BackingField  // 0x10
  private   readonly Unity.VisualScripting.IGraphParent<context>k__BackingField  // 0x18
PROPERTIES:
  port  get=0x02B76770
  context  get=0x04D88320
METHODS:
  RVA=0x0B386CF8  token=0x6000056  System.Void .ctor(Unity.VisualScripting.IUnitPort port, Unity.VisualScripting.GraphPointer pointer)
  RVA=0x0B386CA0  token=0x6000057  System.Boolean Equals(Unity.VisualScripting.Flow.RecursionNode other)
  RVA=0x0B386C30  token=0x6000058  System.Boolean Equals(System.Object obj)
  RVA=0x0B386CB8  token=0x6000059  System.Int32 GetHashCode()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000012
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.Flow.<>c  <>9  // static @ 0x0
  public    static  System.Func<Unity.VisualScripting.Flow><>9__35_0  // static @ 0x8
METHODS:
  RVA=0x0B386F0C  token=0x600005A  System.Void .cctor()
  RVA=0x041E1670  token=0x600005B  System.Void .ctor()
  RVA=0x0B386D34  token=0x600005C  Unity.VisualScripting.Flow <New>b__35_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200001A
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.ControlInput.<>c<>9  // static @ 0x0
  public    static  System.Func<Unity.VisualScripting.ControlConnection,Unity.VisualScripting.ControlOutput><>9__14_0  // static @ 0x8
  public    static  System.Func<Unity.VisualScripting.InvalidConnection,Unity.VisualScripting.IUnitOutputPort><>9__16_0  // static @ 0x10
METHODS:
  RVA=0x0B386EA8  token=0x6000081  System.Void .cctor()
  RVA=0x041E1670  token=0x6000082  System.Void .ctor()
  RVA=0x0B386DC4  token=0x6000083  Unity.VisualScripting.ControlOutput <get_validConnectedPorts>b__14_0(Unity.VisualScripting.ControlConnection c)
  RVA=0x0B386D84  token=0x6000084  Unity.VisualScripting.IUnitOutputPort <get_invalidConnectedPorts>b__16_0(Unity.VisualScripting.InvalidConnection c)
END_CLASS

CLASS: <>c__DisplayClass25_0
TYPE:  sealed class
TOKEN: 0x200001B
SIZE:  0x18
FIELDS:
  public            Unity.VisualScripting.ControlOutputport  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000085  System.Void .ctor()
  RVA=0x0B386E1C  token=0x6000086  System.Boolean <DisconnectFromValid>b__0(Unity.VisualScripting.ControlConnection c)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200001D
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.ControlOutput.<>c<>9  // static @ 0x0
  public    static  System.Func<Unity.VisualScripting.ControlConnection,Unity.VisualScripting.ControlInput><>9__6_0  // static @ 0x8
  public    static  System.Func<Unity.VisualScripting.InvalidConnection,Unity.VisualScripting.IUnitInputPort><>9__8_0  // static @ 0x10
METHODS:
  RVA=0x0B386F70  token=0x6000093  System.Void .cctor()
  RVA=0x041E1670  token=0x6000094  System.Void .ctor()
  RVA=0x0B386DDC  token=0x6000095  Unity.VisualScripting.ControlInput <get_validConnectedPorts>b__6_0(Unity.VisualScripting.ControlConnection c)
  RVA=0x0B386DA4  token=0x6000096  Unity.VisualScripting.IUnitInputPort <get_invalidConnectedPorts>b__8_0(Unity.VisualScripting.InvalidConnection c)
END_CLASS

CLASS: <>c__DisplayClass21_0
TYPE:  sealed class
TOKEN: 0x200001E
SIZE:  0x18
FIELDS:
  public            Unity.VisualScripting.ControlInputport  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000097  System.Void .ctor()
  RVA=0x0B386DF4  token=0x6000098  System.Boolean <DisconnectFromValid>b__0(Unity.VisualScripting.ControlConnection c)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000020
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.InvalidInput.<>c<>9  // static @ 0x0
  public    static  System.Func<Unity.VisualScripting.InvalidConnection,Unity.VisualScripting.IUnitOutputPort><>9__6_0  // static @ 0x8
  public    static  System.Func<Unity.VisualScripting.InvalidConnection,Unity.VisualScripting.IUnitOutputPort><>9__8_0  // static @ 0x10
METHODS:
  RVA=0x0B386E44  token=0x60000A3  System.Void .cctor()
  RVA=0x041E1670  token=0x60000A4  System.Void .ctor()
  RVA=0x0B386D84  token=0x60000A5  Unity.VisualScripting.IUnitOutputPort <get_validConnectedPorts>b__6_0(Unity.VisualScripting.InvalidConnection c)
  RVA=0x0B386D84  token=0x60000A6  Unity.VisualScripting.IUnitOutputPort <get_invalidConnectedPorts>b__8_0(Unity.VisualScripting.InvalidConnection c)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000022
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.InvalidOutput.<>c<>9  // static @ 0x0
  public    static  System.Func<Unity.VisualScripting.InvalidConnection,Unity.VisualScripting.IUnitInputPort><>9__6_0  // static @ 0x8
  public    static  System.Func<Unity.VisualScripting.InvalidConnection,Unity.VisualScripting.IUnitInputPort><>9__8_0  // static @ 0x10
METHODS:
  RVA=0x0B387038  token=0x60000B1  System.Void .cctor()
  RVA=0x041E1670  token=0x60000B2  System.Void .ctor()
  RVA=0x0B386DA4  token=0x60000B3  Unity.VisualScripting.IUnitInputPort <get_validConnectedPorts>b__6_0(Unity.VisualScripting.InvalidConnection c)
  RVA=0x0B386DA4  token=0x60000B4  Unity.VisualScripting.IUnitInputPort <get_invalidConnectedPorts>b__8_0(Unity.VisualScripting.InvalidConnection c)
END_CLASS

CLASS: <>c__DisplayClass45_0
TYPE:  sealed class
TOKEN: 0x200002A
FIELDS:
  public            Unity.VisualScripting.IUnitOutputPortsource  // 0x0
  public            Unity.VisualScripting.IUnitInputPortdestination  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60000D4  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60000D5  System.Boolean <ConnectInvalid>b__0(Unity.VisualScripting.InvalidConnection c)
END_CLASS

CLASS: <>c__DisplayClass46_0
TYPE:  sealed class
TOKEN: 0x200002B
FIELDS:
  public            Unity.VisualScripting.IUnitOutputPortsource  // 0x0
  public            Unity.VisualScripting.IUnitInputPortdestination  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60000D6  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60000D7  System.Boolean <DisconnectInvalid>b__0(Unity.VisualScripting.InvalidConnection c)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200002E
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.ValueInput.<>c<>9  // static @ 0x0
  public    static  System.Func<Unity.VisualScripting.ValueConnection,Unity.VisualScripting.ValueOutput><>9__11_0  // static @ 0x8
  public    static  System.Func<Unity.VisualScripting.InvalidConnection,Unity.VisualScripting.IUnitOutputPort><>9__13_0  // static @ 0x10
METHODS:
  RVA=0x0B38709C  token=0x60000F6  System.Void .cctor()
  RVA=0x041E1670  token=0x60000F7  System.Void .ctor()
  RVA=0x0B386DC4  token=0x60000F8  Unity.VisualScripting.ValueOutput <get_validConnectedPorts>b__11_0(Unity.VisualScripting.ValueConnection c)
  RVA=0x0B386D84  token=0x60000F9  Unity.VisualScripting.IUnitOutputPort <get_invalidConnectedPorts>b__13_0(Unity.VisualScripting.InvalidConnection c)
END_CLASS

CLASS: <>c__DisplayClass33_0
TYPE:  sealed class
TOKEN: 0x200002F
SIZE:  0x18
FIELDS:
  public            Unity.VisualScripting.ValueOutputport  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60000FA  System.Void .ctor()
  RVA=0x0B386E1C  token=0x60000FB  System.Boolean <DisconnectFromValid>b__0(Unity.VisualScripting.ValueConnection c)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000031
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.ValueOutput.<>c<>9  // static @ 0x0
  public    static  System.Func<Unity.VisualScripting.ValueConnection,Unity.VisualScripting.ValueInput><>9__16_0  // static @ 0x8
  public    static  System.Func<Unity.VisualScripting.InvalidConnection,Unity.VisualScripting.IUnitInputPort><>9__18_0  // static @ 0x10
METHODS:
  RVA=0x0B386FD4  token=0x6000108  System.Void .cctor()
  RVA=0x041E1670  token=0x6000109  System.Void .ctor()
  RVA=0x0B386DDC  token=0x600010A  Unity.VisualScripting.ValueInput <get_validConnectedPorts>b__16_0(Unity.VisualScripting.ValueConnection c)
  RVA=0x0B386DA4  token=0x600010B  Unity.VisualScripting.IUnitInputPort <get_invalidConnectedPorts>b__18_0(Unity.VisualScripting.InvalidConnection c)
END_CLASS

CLASS: <>c__DisplayClass22_0
TYPE:  sealed class
TOKEN: 0x2000032
SIZE:  0x18
FIELDS:
  public            Unity.VisualScripting.ValueInputport  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600010C  System.Void .ctor()
  RVA=0x0B386DF4  token=0x600010D  System.Boolean <DisconnectFromValid>b__0(Unity.VisualScripting.ValueConnection c)
END_CLASS

CLASS: <>c__DisplayClass86_0`1
TYPE:  sealed class
TOKEN: 0x2000034
FIELDS:
  public            System.Func<Unity.VisualScripting.Flow,T>getValue  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000128  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000129  System.Object <ValueOutput>b__0(Unity.VisualScripting.Flow recursion)
END_CLASS

CLASS: Unity.VisualScripting.ControlConnection
TYPE:  sealed class
TOKEN: 0x2000002
SIZE:  0x48
EXTENDS: Unity.VisualScripting.UnitConnection`2
IMPLEMENTS: Unity.VisualScripting.IUnitConnection Unity.VisualScripting.IConnection`2 Unity.VisualScripting.IGraphElementWithDebugData Unity.VisualScripting.IGraphElement System.IDisposable Unity.VisualScripting.IIdentifiable
FIELDS:
PROPERTIES:
  source  get=0x0B383588
  destination  get=0x0B3834AC
  Unity.VisualScripting.IConnection<Unity.VisualScripting.IUnitOutputPort,Unity.VisualScripting.IUnitInputPort>.source  get=0x0B38336C
  Unity.VisualScripting.IConnection<Unity.VisualScripting.IUnitOutputPort,Unity.VisualScripting.IUnitInputPort>.destination  get=0x0B383364
  sourceExists  get=0x0B383514
  destinationExists  get=0x0B383438
METHODS:
  RVA=0x0B383374  token=0x6000001  System.Void .ctor(Unity.VisualScripting.ControlOutput source, Unity.VisualScripting.ControlInput destination)
END_CLASS

CLASS: Unity.VisualScripting.InvalidConnection
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x48
EXTENDS: Unity.VisualScripting.UnitConnection`2
IMPLEMENTS: Unity.VisualScripting.IUnitConnection Unity.VisualScripting.IConnection`2 Unity.VisualScripting.IGraphElementWithDebugData Unity.VisualScripting.IGraphElement System.IDisposable Unity.VisualScripting.IIdentifiable
FIELDS:
PROPERTIES:
  source  get=0x0B385F68
  destination  get=0x0B385E00
  validSource  get=0x0B386238
  validDestination  get=0x0B3860D0
  sourceExists  get=0x0B385EB4
  destinationExists  get=0x0B385D4C
  validSourceExists  get=0x0B386184
  validDestinationExists  get=0x0B38601C
METHODS:
  RVA=0x0B385CF4  token=0x6000008  System.Void .ctor(Unity.VisualScripting.IUnitOutputPort source, Unity.VisualScripting.IUnitInputPort destination)
  RVA=0x0B385858  token=0x6000011  System.Boolean HandleDependencies()
  RVA=0x0B385B3C  token=0x6000012  System.Boolean <get_source>b__4_0(Unity.VisualScripting.IUnitOutputPort p)
  RVA=0x0B385A8C  token=0x6000013  System.Boolean <get_destination>b__6_0(Unity.VisualScripting.IUnitInputPort p)
  RVA=0x0B385C9C  token=0x6000014  System.Boolean <get_validSource>b__8_0(Unity.VisualScripting.IUnitOutputPort p)
  RVA=0x0B385BEC  token=0x6000015  System.Boolean <get_validDestination>b__10_0(Unity.VisualScripting.IUnitInputPort p)
  RVA=0x0B385AE4  token=0x6000016  System.Boolean <get_sourceExists>b__12_0(Unity.VisualScripting.IUnitOutputPort p)
  RVA=0x0B385A34  token=0x6000017  System.Boolean <get_destinationExists>b__14_0(Unity.VisualScripting.IUnitInputPort p)
  RVA=0x0B385C44  token=0x6000018  System.Boolean <get_validSourceExists>b__16_0(Unity.VisualScripting.IUnitOutputPort p)
  RVA=0x0B385B94  token=0x6000019  System.Boolean <get_validDestinationExists>b__18_0(Unity.VisualScripting.IUnitInputPort p)
END_CLASS

CLASS: Unity.VisualScripting.IUnitConnection
TYPE:  interface
TOKEN: 0x2000004
IMPLEMENTS: Unity.VisualScripting.IConnection`2 Unity.VisualScripting.IGraphElementWithDebugData Unity.VisualScripting.IGraphElement System.IDisposable Unity.VisualScripting.IIdentifiable
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.IUnitConnectionDebugData
TYPE:  interface
TOKEN: 0x2000005
IMPLEMENTS: Unity.VisualScripting.IGraphElementDebugData
FIELDS:
PROPERTIES:
  lastInvokeFrame  set=-1  // abstract
  lastInvokeTime  set=-1  // abstract
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.IUnitRelation
TYPE:  interface
TOKEN: 0x2000006
IMPLEMENTS: Unity.VisualScripting.IConnection`2
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.UnitConnection`2
TYPE:  abstract class
TOKEN: 0x2000007
EXTENDS: Unity.VisualScripting.GraphElement`1
IMPLEMENTS: Unity.VisualScripting.IConnection`2
FIELDS:
  private           Unity.VisualScripting.IUnit     <sourceUnit>k__BackingField  // 0x0
  private           System.String                   <sourceKey>k__BackingField  // 0x0
  private           Unity.VisualScripting.IUnit     <destinationUnit>k__BackingField  // 0x0
  private           System.String                   <destinationKey>k__BackingField  // 0x0
PROPERTIES:
  sourceUnit  get=-1  // not resolved  set=-1  // not resolved
  sourceKey  get=-1  // not resolved  set=-1  // not resolved
  destinationUnit  get=-1  // not resolved  set=-1  // not resolved
  destinationKey  get=-1  // not resolved  set=-1  // not resolved
  source  get=-1  // abstract
  destination  get=-1  // abstract
  dependencyOrder  get=-1  // not resolved
  sourceExists  get=-1  // abstract
  destinationExists  get=-1  // abstract
METHODS:
  RVA=-1  // not resolved  token=0x600001C  System.Void .ctor(TSourcePort source, TDestinationPort destination)
  RVA=-1  // not resolved  token=0x600002A  System.Boolean HandleDependencies()
END_CLASS

CLASS: Unity.VisualScripting.UnitConnectionDebugData
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x20
IMPLEMENTS: Unity.VisualScripting.IUnitConnectionDebugData
FIELDS:
  private           System.Int32                    <lastInvokeFrame>k__BackingField  // 0x10
  private           System.Single                   <lastInvokeTime>k__BackingField  // 0x14
  private           System.Exception                <runtimeException>k__BackingField  // 0x18
PROPERTIES:
  lastInvokeFrame  set=0x014F51F0
  lastInvokeTime  set=0x04D88D50
  runtimeException  set=0x05392C40
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.ValueConnection
TYPE:  sealed class
TOKEN: 0x2000009
SIZE:  0x48
EXTENDS: Unity.VisualScripting.UnitConnection`2
IMPLEMENTS: Unity.VisualScripting.IUnitConnection Unity.VisualScripting.IConnection`2 Unity.VisualScripting.IGraphElementWithDebugData Unity.VisualScripting.IGraphElement System.IDisposable Unity.VisualScripting.IIdentifiable
FIELDS:
PROPERTIES:
  source  get=0x0B388308
  destination  get=0x0B38822C
  Unity.VisualScripting.IConnection<Unity.VisualScripting.IUnitOutputPort,Unity.VisualScripting.IUnitInputPort>.source  get=0x0B38336C
  Unity.VisualScripting.IConnection<Unity.VisualScripting.IUnitOutputPort,Unity.VisualScripting.IUnitInputPort>.destination  get=0x0B383364
  sourceExists  get=0x0B388294
  destinationExists  get=0x0B3881B8
METHODS:
  RVA=0x0B388034  token=0x600002E  System.Void .ctor(Unity.VisualScripting.ValueOutput source, Unity.VisualScripting.ValueInput destination)
END_CLASS

CLASS: Unity.VisualScripting.PortLabelHiddenAttribute
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000037  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.SpecialUnitAttribute
TYPE:  sealed class
TOKEN: 0x200000C
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000038  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.UnitHeaderInspectableAttribute
TYPE:  sealed class
TOKEN: 0x200000D
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000039  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.UnitShortTitleAttribute
TYPE:  sealed class
TOKEN: 0x200000E
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private           System.String                   <title>k__BackingField  // 0x10
PROPERTIES:
  title  set=0x053908C0
METHODS:
  RVA=0x053908C0  token=0x600003A  System.Void .ctor(System.String title)
END_CLASS

CLASS: Unity.VisualScripting.UnitTitleAttribute
TYPE:  sealed class
TOKEN: 0x200000F
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private           System.String                   <title>k__BackingField  // 0x10
PROPERTIES:
  title  set=0x053908C0
METHODS:
  RVA=0x053908C0  token=0x600003C  System.Void .ctor(System.String title)
END_CLASS

CLASS: Unity.VisualScripting.Flow
TYPE:  sealed class
TOKEN: 0x2000010
SIZE:  0x68
IMPLEMENTS: Unity.VisualScripting.IPoolable System.IDisposable
FIELDS:
  private           Unity.VisualScripting.GraphStack<stack>k__BackingField  // 0x10
  private           Unity.VisualScripting.Recursion<Unity.VisualScripting.Flow.RecursionNode>recursion  // 0x18
  private   readonly System.Collections.Generic.Dictionary<Unity.VisualScripting.IUnitValuePort,System.Object>locals  // 0x20
  public    readonly Unity.VisualScripting.VariableDeclarationsvariables  // 0x28
  private   readonly System.Collections.Generic.Stack<System.Int32>loops  // 0x30
  private   readonly System.Collections.Generic.HashSet<Unity.VisualScripting.GraphStack>preservedStacks  // 0x38
  private           UnityEngine.MonoBehaviour       <coroutineRunner>k__BackingField  // 0x40
  private           System.Collections.Generic.ICollection<Unity.VisualScripting.Flow>activeCoroutinesRegistry  // 0x48
  private           System.Boolean                  coroutineStopRequested  // 0x50
  private           System.Boolean                  <isCoroutine>k__BackingField  // 0x51
  private           System.Collections.IEnumerator  coroutineEnumerator  // 0x58
  private           System.Boolean                  <isPrediction>k__BackingField  // 0x60
  private           System.Boolean                  disposed  // 0x61
  public            System.Int32                    loopIdentifier  // 0x64
PROPERTIES:
  stack  get=0x02B2ECC0  set=0x053908C0
  coroutineRunner  set=0x042B4AE0
  isCoroutine  set=0x04D86830
  isPrediction  get=0x04D88340  set=0x04D88350
  enableDebug  get=0x0B385828
METHODS:
  RVA=0x0B3856DC  token=0x6000045  System.Void .ctor()
  RVA=0x0B385090  token=0x6000046  Unity.VisualScripting.Flow New(Unity.VisualScripting.GraphReference reference)
  RVA=0x0B385694  token=0x6000047  System.Void Unity.VisualScripting.IPoolable.New()
  RVA=0x0B38452C  token=0x6000048  System.Void Dispose()
  RVA=0x0B3852F0  token=0x6000049  System.Void Unity.VisualScripting.IPoolable.Free()
  RVA=0x0B384E94  token=0x600004A  System.Void Invoke(Unity.VisualScripting.ControlOutput output)
  RVA=0x0B3842DC  token=0x600004B  Unity.VisualScripting.Flow.RecursionNode BeforeInvoke(Unity.VisualScripting.ControlOutput output, Unity.VisualScripting.Flow.RecursionNode recursionNode)
  RVA=0x0B384288  token=0x600004C  System.Void AfterInvoke(Unity.VisualScripting.ControlOutput output, Unity.VisualScripting.Flow.RecursionNode recursionNode)
  RVA=0x0B384D24  token=0x600004D  Unity.VisualScripting.ControlOutput InvokeDelegate(Unity.VisualScripting.ControlInput input)
  RVA=0x0B384AB8  token=0x600004E  System.Object GetValue(Unity.VisualScripting.ValueInput input)
  RVA=0x0B3846C8  token=0x600004F  System.Object GetValue(Unity.VisualScripting.ValueOutput output)
  RVA=0x0B384CB4  token=0x6000050  System.Object GetValue(Unity.VisualScripting.ValueInput input, System.Type type)
  RVA=-1  // generic def  token=0x6000051  T GetValue(Unity.VisualScripting.ValueInput input)
  RVA=0x0B385230  token=0x6000052  System.Boolean TryGetDefaultValue(Unity.VisualScripting.ValueInput input, System.Object& defaultValue)
  RVA=0x0B3845E4  token=0x6000053  System.Object GetValueDelegate(Unity.VisualScripting.ValueOutput output)
END_CLASS

CLASS: Unity.VisualScripting.FlowGraph
TYPE:  sealed class
TOKEN: 0x2000013
SIZE:  0x38
EXTENDS: Unity.VisualScripting.Graph
IMPLEMENTS: Unity.VisualScripting.IGraph System.IDisposable Unity.VisualScripting.ISerializationDepender UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           Unity.VisualScripting.GraphConnectionCollection<Unity.VisualScripting.ControlConnection,Unity.VisualScripting.ControlOutput,Unity.VisualScripting.ControlInput><controlConnections>k__BackingField  // 0x20
  private           Unity.VisualScripting.GraphConnectionCollection<Unity.VisualScripting.ValueConnection,Unity.VisualScripting.ValueOutput,Unity.VisualScripting.ValueInput><valueConnections>k__BackingField  // 0x28
  private           Unity.VisualScripting.GraphConnectionCollection<Unity.VisualScripting.InvalidConnection,Unity.VisualScripting.IUnitOutputPort,Unity.VisualScripting.IUnitInputPort><invalidConnections>k__BackingField  // 0x30
PROPERTIES:
  controlConnections  get=0x04D862C0
  valueConnections  get=0x04D86240
  invalidConnections  get=0x011F36E0
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.EventUnit`1
TYPE:  abstract class
TOKEN: 0x2000014
EXTENDS: Unity.VisualScripting.Unit
IMPLEMENTS: Unity.VisualScripting.IUnit Unity.VisualScripting.IGraphElementWithDebugData Unity.VisualScripting.IGraphElement System.IDisposable Unity.VisualScripting.IIdentifiable
FIELDS:
  private           Unity.VisualScripting.ControlOutput<trigger>k__BackingField  // 0x0
PROPERTIES:
  trigger  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000062  System.Void Definition()
  RVA=-1  // not resolved  token=0x6000063  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000064  Unity.VisualScripting.FlowGraph Unity.VisualScripting.IUnit.get_graph()
END_CLASS

CLASS: Unity.VisualScripting.ManualEventUnit`1
TYPE:  abstract class
TOKEN: 0x2000015
EXTENDS: Unity.VisualScripting.EventUnit`1
FIELDS:
PROPERTIES:
  hookName  get=-1  // abstract
METHODS:
  RVA=-1  // not resolved  token=0x6000066  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.MissingType
TYPE:  sealed class
TOKEN: 0x2000016
SIZE:  0x78
EXTENDS: Unity.VisualScripting.Unit
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000067  System.Void Definition()
END_CLASS

CLASS: Unity.VisualScripting.IUnit
TYPE:  interface
TOKEN: 0x2000017
IMPLEMENTS: Unity.VisualScripting.IGraphElementWithDebugData Unity.VisualScripting.IGraphElement System.IDisposable Unity.VisualScripting.IIdentifiable
FIELDS:
PROPERTIES:
  graph  get=-1  // abstract
  defaultValues  get=-1  // abstract
  controlInputs  get=-1  // abstract
  controlOutputs  get=-1  // abstract
  valueInputs  get=-1  // abstract
  valueOutputs  get=-1  // abstract
  invalidInputs  get=-1  // abstract
  invalidOutputs  get=-1  // abstract
  inputs  get=-1  // abstract
  outputs  get=-1  // abstract
  validInputs  get=-1  // abstract
  validOutputs  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000074  System.Void PortsChanged()
END_CLASS

CLASS: Unity.VisualScripting.IUnitDebugData
TYPE:  interface
TOKEN: 0x2000018
IMPLEMENTS: Unity.VisualScripting.IGraphElementDebugData
FIELDS:
PROPERTIES:
  lastInvokeFrame  set=-1  // abstract
  lastInvokeTime  set=-1  // abstract
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.ControlInput
TYPE:  sealed class
TOKEN: 0x2000019
SIZE:  0x28
EXTENDS: Unity.VisualScripting.UnitPort`3
IMPLEMENTS: Unity.VisualScripting.IUnitPort Unity.VisualScripting.IUnitInputPort
FIELDS:
  private   readonly System.Func<Unity.VisualScripting.Flow,Unity.VisualScripting.ControlOutput>action  // 0x20
PROPERTIES:
  requiresCoroutine  get=0x0A819158
  validConnections  get=0x0B383B4C
  invalidConnections  get=0x0B3839AC
  validConnectedPorts  get=0x0B383A34
  invalidConnectedPorts  get=0x0B383894
METHODS:
  RVA=0x02FFF600  token=0x600007C  System.Boolean CanConnectToValid(Unity.VisualScripting.ControlOutput port)
  RVA=0x0B38363C  token=0x600007D  System.Void ConnectToValid(Unity.VisualScripting.ControlOutput port)
  RVA=0x0B3835F0  token=0x600007E  System.Void ConnectToInvalid(Unity.VisualScripting.IUnitOutputPort port)
  RVA=0x0B383764  token=0x600007F  System.Void DisconnectFromValid(Unity.VisualScripting.ControlOutput port)
  RVA=0x0B383718  token=0x6000080  System.Void DisconnectFromInvalid(Unity.VisualScripting.IUnitOutputPort port)
END_CLASS

CLASS: Unity.VisualScripting.ControlOutput
TYPE:  sealed class
TOKEN: 0x200001C
SIZE:  0x20
EXTENDS: Unity.VisualScripting.UnitPort`3
IMPLEMENTS: Unity.VisualScripting.IUnitPort Unity.VisualScripting.IUnitOutputPort
FIELDS:
PROPERTIES:
  validConnections  get=0x0B384200
  invalidConnections  get=0x0B384060
  validConnectedPorts  get=0x0B3840E8
  invalidConnectedPorts  get=0x0B383F48
  connection  get=0x0B383EC0
  hasValidConnection  get=0x0B383F30
METHODS:
  RVA=0x0B383E78  token=0x6000087  System.Void .ctor(System.String key)
  RVA=0x02FFF600  token=0x600008E  System.Boolean CanConnectToValid(Unity.VisualScripting.ControlInput port)
  RVA=0x0B383C20  token=0x600008F  System.Void ConnectToValid(Unity.VisualScripting.ControlInput port)
  RVA=0x0B383BD4  token=0x6000090  System.Void ConnectToInvalid(Unity.VisualScripting.IUnitInputPort port)
  RVA=0x0B383D48  token=0x6000091  System.Void DisconnectFromValid(Unity.VisualScripting.ControlInput port)
  RVA=0x0B383CFC  token=0x6000092  System.Void DisconnectFromInvalid(Unity.VisualScripting.IUnitInputPort port)
END_CLASS

CLASS: Unity.VisualScripting.InvalidInput
TYPE:  sealed class
TOKEN: 0x200001F
SIZE:  0x20
EXTENDS: Unity.VisualScripting.UnitPort`3
IMPLEMENTS: Unity.VisualScripting.IUnitPort Unity.VisualScripting.IUnitInputPort
FIELDS:
PROPERTIES:
  validConnections  get=0x0B3866C4
  invalidConnections  get=0x0B38657C
  validConnectedPorts  get=0x0B3865AC
  invalidConnectedPorts  get=0x0B386464
METHODS:
  RVA=0x0B38641C  token=0x6000099  System.Void .ctor(System.String key)
  RVA=0x012081B0  token=0x600009E  System.Boolean CanConnectToValid(Unity.VisualScripting.IUnitOutputPort port)
  RVA=0x0B386338  token=0x600009F  System.Void ConnectToValid(Unity.VisualScripting.IUnitOutputPort port)
  RVA=0x0B3862EC  token=0x60000A0  System.Void ConnectToInvalid(Unity.VisualScripting.IUnitOutputPort port)
  RVA=0x0B3863D0  token=0x60000A1  System.Void DisconnectFromValid(Unity.VisualScripting.IUnitOutputPort port)
  RVA=0x0B386384  token=0x60000A2  System.Void DisconnectFromInvalid(Unity.VisualScripting.IUnitOutputPort port)
END_CLASS

CLASS: Unity.VisualScripting.InvalidOutput
TYPE:  sealed class
TOKEN: 0x2000021
SIZE:  0x20
EXTENDS: Unity.VisualScripting.UnitPort`3
IMPLEMENTS: Unity.VisualScripting.IUnitPort Unity.VisualScripting.IUnitOutputPort
FIELDS:
PROPERTIES:
  validConnections  get=0x0B386B24
  invalidConnections  get=0x0B3869DC
  validConnectedPorts  get=0x0B386A0C
  invalidConnectedPorts  get=0x0B3868C4
METHODS:
  RVA=0x0B38687C  token=0x60000A7  System.Void .ctor(System.String key)
  RVA=0x012081B0  token=0x60000AC  System.Boolean CanConnectToValid(Unity.VisualScripting.IUnitInputPort port)
  RVA=0x0B386798  token=0x60000AD  System.Void ConnectToValid(Unity.VisualScripting.IUnitInputPort port)
  RVA=0x0B38674C  token=0x60000AE  System.Void ConnectToInvalid(Unity.VisualScripting.IUnitInputPort port)
  RVA=0x0B386830  token=0x60000AF  System.Void DisconnectFromValid(Unity.VisualScripting.IUnitInputPort port)
  RVA=0x0B3867E4  token=0x60000B0  System.Void DisconnectFromInvalid(Unity.VisualScripting.IUnitInputPort port)
END_CLASS

CLASS: Unity.VisualScripting.IUnitInputPort
TYPE:  interface
TOKEN: 0x2000023
IMPLEMENTS: Unity.VisualScripting.IUnitPort
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.IUnitOutputPort
TYPE:  interface
TOKEN: 0x2000024
IMPLEMENTS: Unity.VisualScripting.IUnitPort
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.IUnitPort
TYPE:  interface
TOKEN: 0x2000025
FIELDS:
PROPERTIES:
  unit  get=-1  // abstract  set=-1  // abstract
  key  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60000B8  System.Boolean CanInvalidlyConnectTo(Unity.VisualScripting.IUnitPort port)
  RVA=-1  // abstract  token=0x60000B9  System.Boolean CanValidlyConnectTo(Unity.VisualScripting.IUnitPort port)
  RVA=-1  // abstract  token=0x60000BA  System.Void InvalidlyConnectTo(Unity.VisualScripting.IUnitPort port)
  RVA=-1  // abstract  token=0x60000BB  System.Void ValidlyConnectTo(Unity.VisualScripting.IUnitPort port)
END_CLASS

CLASS: Unity.VisualScripting.IUnitPortCollection`1
TYPE:  interface
TOKEN: 0x2000026
IMPLEMENTS: Unity.VisualScripting.IKeyedCollection`2 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.IUnitValuePort
TYPE:  interface
TOKEN: 0x2000027
IMPLEMENTS: Unity.VisualScripting.IUnitPort
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.MissingValuePortInputException
TYPE:  sealed class
TOKEN: 0x2000028
SIZE:  0x90
EXTENDS: System.Exception
FIELDS:
METHODS:
  RVA=0x0B386BAC  token=0x60000BC  System.Void .ctor(System.String key)
END_CLASS

CLASS: Unity.VisualScripting.UnitPort`3
TYPE:  abstract class
TOKEN: 0x2000029
IMPLEMENTS: Unity.VisualScripting.IUnitPort
FIELDS:
  private           Unity.VisualScripting.IUnit     <unit>k__BackingField  // 0x0
  private   readonly System.String                   <key>k__BackingField  // 0x0
PROPERTIES:
  unit  get=-1  // not resolved  set=-1  // not resolved
  key  get=-1  // not resolved
  validConnections  get=-1  // abstract
  invalidConnections  get=-1  // abstract
  validConnectedPorts  get=-1  // abstract
  invalidConnectedPorts  get=-1  // abstract
  hasValidConnection  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000BD  System.Void .ctor(System.String key)
  RVA=-1  // not resolved  token=0x60000C6  System.Boolean CanConnectTo(Unity.VisualScripting.IUnitPort port)
  RVA=-1  // not resolved  token=0x60000C7  System.Boolean CanValidlyConnectTo(Unity.VisualScripting.IUnitPort port)
  RVA=-1  // not resolved  token=0x60000C8  System.Boolean CanInvalidlyConnectTo(Unity.VisualScripting.IUnitPort port)
  RVA=-1  // not resolved  token=0x60000C9  System.Void ValidlyConnectTo(Unity.VisualScripting.IUnitPort port)
  RVA=-1  // not resolved  token=0x60000CA  System.Void InvalidlyConnectTo(Unity.VisualScripting.IUnitPort port)
  RVA=-1  // not resolved  token=0x60000CB  System.Void Disconnect()
  RVA=-1  // abstract  token=0x60000CC  System.Boolean CanConnectToValid(TValidOther port)
  RVA=-1  // not resolved  token=0x60000CD  System.Boolean CanConnectToInvalid(TInvalidOther port)
  RVA=-1  // abstract  token=0x60000CE  System.Void ConnectToValid(TValidOther port)
  RVA=-1  // abstract  token=0x60000CF  System.Void ConnectToInvalid(TInvalidOther port)
  RVA=-1  // abstract  token=0x60000D0  System.Void DisconnectFromValid(TValidOther port)
  RVA=-1  // abstract  token=0x60000D1  System.Void DisconnectFromInvalid(TInvalidOther port)
  RVA=-1  // not resolved  token=0x60000D2  System.Void ConnectInvalid(Unity.VisualScripting.IUnitOutputPort source, Unity.VisualScripting.IUnitInputPort destination)
  RVA=-1  // not resolved  token=0x60000D3  System.Void DisconnectInvalid(Unity.VisualScripting.IUnitOutputPort source, Unity.VisualScripting.IUnitInputPort destination)
END_CLASS

CLASS: Unity.VisualScripting.UnitPortCollection`1
TYPE:  sealed class
TOKEN: 0x200002C
EXTENDS: System.Collections.ObjectModel.KeyedCollection`2
IMPLEMENTS: Unity.VisualScripting.IUnitPortCollection`1 Unity.VisualScripting.IKeyedCollection`2 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Unity.VisualScripting.IUnit     <unit>k__BackingField  // 0x0
PROPERTIES:
  unit  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000D9  System.Void .ctor(Unity.VisualScripting.IUnit unit)
  RVA=-1  // not resolved  token=0x60000DA  System.Void BeforeAdd(TPort port)
  RVA=-1  // not resolved  token=0x60000DB  System.Void AfterAdd(TPort port)
  RVA=-1  // not resolved  token=0x60000DC  System.Void BeforeRemove(TPort port)
  RVA=-1  // not resolved  token=0x60000DD  System.Void AfterRemove(TPort port)
  RVA=-1  // not resolved  token=0x60000DE  System.String GetKeyForItem(TPort item)
  RVA=-1  // not resolved  token=0x60000DF  System.Void InsertItem(System.Int32 index, TPort item)
  RVA=-1  // not resolved  token=0x60000E0  System.Void RemoveItem(System.Int32 index)
  RVA=-1  // not resolved  token=0x60000E1  System.Void SetItem(System.Int32 index, TPort item)
  RVA=-1  // not resolved  token=0x60000E2  System.Void ClearItems()
  RVA=-1  // not resolved  token=0x60000E3  TPort Unity.VisualScripting.IKeyedCollection<System.String,TPort>.get_Item(System.String key)
  RVA=-1  // not resolved  token=0x60000E4  System.Boolean Unity.VisualScripting.IKeyedCollection<System.String,TPort>.Contains(System.String key)
END_CLASS

CLASS: Unity.VisualScripting.ValueInput
TYPE:  sealed class
TOKEN: 0x200002D
SIZE:  0x30
EXTENDS: Unity.VisualScripting.UnitPort`3
IMPLEMENTS: Unity.VisualScripting.IUnitValuePort Unity.VisualScripting.IUnitPort Unity.VisualScripting.IUnitInputPort
FIELDS:
  private   readonly System.Type                     <type>k__BackingField  // 0x20
  private           System.Boolean                  <nullMeansSelf>k__BackingField  // 0x28
  private   static readonly System.Collections.Generic.HashSet<System.Type>typesWithDefaultValues  // static @ 0x0
PROPERTIES:
  type  get=0x04D862C0
  validConnections  get=0x0B388FBC
  invalidConnections  get=0x0B388E1C
  validConnectedPorts  get=0x0B388EA4
  invalidConnectedPorts  get=0x0B388D04
  nullMeansSelf  get=0x011F33C0
  connection  get=0x0B388C7C
  hasValidConnection  get=0x0B388CEC
METHODS:
  RVA=0x0B388BBC  token=0x60000E5  System.Void .ctor(System.String key, System.Type type)
  RVA=0x0B38868C  token=0x60000EE  System.Void SetDefaultValue(System.Object value)
  RVA=0x0B388370  token=0x60000EF  System.Boolean CanConnectToValid(Unity.VisualScripting.ValueOutput port)
  RVA=0x0B388434  token=0x60000F0  System.Void ConnectToValid(Unity.VisualScripting.ValueOutput port)
  RVA=0x0B3883E8  token=0x60000F1  System.Void ConnectToInvalid(Unity.VisualScripting.IUnitOutputPort port)
  RVA=0x0B38855C  token=0x60000F2  System.Void DisconnectFromValid(Unity.VisualScripting.ValueOutput port)
  RVA=0x0B388510  token=0x60000F3  System.Void DisconnectFromInvalid(Unity.VisualScripting.IUnitOutputPort port)
  RVA=0x0B388808  token=0x60000F4  System.Boolean SupportsDefaultValue(System.Type type)
  RVA=0x0B388944  token=0x60000F5  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.ValueOutput
TYPE:  sealed class
TOKEN: 0x2000030
SIZE:  0x30
EXTENDS: Unity.VisualScripting.UnitPort`3
IMPLEMENTS: Unity.VisualScripting.IUnitValuePort Unity.VisualScripting.IUnitPort Unity.VisualScripting.IUnitOutputPort
FIELDS:
  private   readonly System.Func<Unity.VisualScripting.Flow,System.Object>getValue  // 0x20
  private   readonly System.Type                     <type>k__BackingField  // 0x28
PROPERTIES:
  supportsFetch  get=0x09B00070
  type  get=0x04D86240
  validConnections  get=0x0B38971C
  invalidConnections  get=0x0B38957C
  validConnectedPorts  get=0x0B389604
  invalidConnectedPorts  get=0x0B389464
METHODS:
  RVA=0x0B389350  token=0x60000FC  System.Void .ctor(System.String key, System.Type type, System.Func<Unity.VisualScripting.Flow,System.Object> getValue)
  RVA=0x0B389044  token=0x6000103  System.Boolean CanConnectToValid(Unity.VisualScripting.ValueInput port)
  RVA=0x0B3890F8  token=0x6000104  System.Void ConnectToValid(Unity.VisualScripting.ValueInput port)
  RVA=0x0B3890AC  token=0x6000105  System.Void ConnectToInvalid(Unity.VisualScripting.IUnitInputPort port)
  RVA=0x0B389220  token=0x6000106  System.Void DisconnectFromValid(Unity.VisualScripting.ValueInput port)
  RVA=0x0B3891D4  token=0x6000107  System.Void DisconnectFromInvalid(Unity.VisualScripting.IUnitInputPort port)
END_CLASS

CLASS: Unity.VisualScripting.Unit
TYPE:  abstract class
TOKEN: 0x2000033
SIZE:  0x78
EXTENDS: Unity.VisualScripting.GraphElement`1
IMPLEMENTS: Unity.VisualScripting.IUnit Unity.VisualScripting.IGraphElementWithDebugData Unity.VisualScripting.IGraphElement System.IDisposable Unity.VisualScripting.IIdentifiable
FIELDS:
  private   readonly Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.ControlInput><controlInputs>k__BackingField  // 0x28
  private   readonly Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.ControlOutput><controlOutputs>k__BackingField  // 0x30
  private   readonly Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.ValueInput><valueInputs>k__BackingField  // 0x38
  private   readonly Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.ValueOutput><valueOutputs>k__BackingField  // 0x40
  private   readonly Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.InvalidInput><invalidInputs>k__BackingField  // 0x48
  private   readonly Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.InvalidOutput><invalidOutputs>k__BackingField  // 0x50
  private           System.Action                   onPortsChanged  // 0x58
  private           System.Collections.Generic.Dictionary<System.String,System.Object><defaultValues>k__BackingField  // 0x60
  private           Unity.VisualScripting.IConnectionCollection<Unity.VisualScripting.IUnitRelation,Unity.VisualScripting.IUnitPort,Unity.VisualScripting.IUnitPort><relations>k__BackingField  // 0x68
  private           System.Boolean                  <isControlRoot>k__BackingField  // 0x70
PROPERTIES:
  controlInputs  get=0x04D86240
  controlOutputs  get=0x011F36E0
  valueInputs  get=0x04D85A50
  valueOutputs  get=0x04D85A60
  invalidInputs  get=0x04D86200
  invalidOutputs  get=0x04D86270
  inputs  get=0x0B387D24
  outputs  get=0x0B387DF8
  validInputs  get=0x0B387ECC
  validOutputs  get=0x0B387F80
  defaultValues  get=0x04D86280  set=0x063CC164
  relations  set=0x0485F170
  isControlRoot  set=0x04D86A80
METHODS:
  RVA=0x0B387A74  token=0x600010E  System.Void .ctor()
  RVA=-1  // abstract  token=0x600010F  System.Void Definition()
  RVA=0x070E89F8  token=0x600011A  System.Void PortsChanged()
  RVA=0x0B387670  token=0x600011F  System.Void EnsureUniqueInput(System.String key)
  RVA=0x0B3877A4  token=0x6000120  System.Void EnsureUniqueOutput(System.String key)
  RVA=0x0B3875D8  token=0x6000121  Unity.VisualScripting.ControlOutput ControlOutput(System.String key)
  RVA=0x0B387910  token=0x6000122  Unity.VisualScripting.ValueInput ValueInput(System.Type type, System.String key)
  RVA=-1  // generic def  token=0x6000123  Unity.VisualScripting.ValueInput ValueInput(System.String key)
  RVA=-1  // generic def  token=0x6000124  Unity.VisualScripting.ValueInput ValueInput(System.String key, T default)
  RVA=0x0B3879B8  token=0x6000125  Unity.VisualScripting.ValueOutput ValueOutput(System.Type type, System.String key, System.Func<Unity.VisualScripting.Flow,System.Object> getValue)
  RVA=-1  // generic def  token=0x6000126  Unity.VisualScripting.ValueOutput ValueOutput(System.String key, System.Func<Unity.VisualScripting.Flow,T> getValue)
  RVA=0x0B3878D8  token=0x6000127  Unity.VisualScripting.FlowGraph Unity.VisualScripting.IUnit.get_graph()
END_CLASS

CLASS: Unity.VisualScripting.UnitCategory
TYPE:  class
TOKEN: 0x2000035
SIZE:  0x38
EXTENDS: System.Attribute
FIELDS:
  private   readonly Unity.VisualScripting.UnitCategory<root>k__BackingField  // 0x10
  private   readonly Unity.VisualScripting.UnitCategory<parent>k__BackingField  // 0x18
  private   readonly System.String                   <fullName>k__BackingField  // 0x20
  private   readonly System.String                   <name>k__BackingField  // 0x28
  private   readonly System.Boolean                  <isRoot>k__BackingField  // 0x30
PROPERTIES:
  fullName  get=0x04D862C0
METHODS:
  RVA=0x0B387414  token=0x600012A  System.Void .ctor(System.String fullName)
  RVA=0x0B387368  token=0x600012C  System.Boolean Equals(System.Object obj)
  RVA=0x0B3873F0  token=0x600012D  System.Int32 GetHashCode()
  RVA=0x04D862C0  token=0x600012E  System.String ToString()
END_CLASS

CLASS: Unity.VisualScripting.UnitCategoryConverter
TYPE:  class
TOKEN: 0x2000036
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsDirectConverter
FIELDS:
PROPERTIES:
  ModelType  get=0x0B387318
METHODS:
  RVA=0x0B387230  token=0x6000130  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0B387100  token=0x6000131  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
END_CLASS

