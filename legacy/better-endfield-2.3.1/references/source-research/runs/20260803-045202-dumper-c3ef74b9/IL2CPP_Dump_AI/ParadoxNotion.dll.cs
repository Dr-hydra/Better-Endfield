// ========================================================
// Dumped by @desirepro
// Assembly: ParadoxNotion.dll
// Classes:  1335
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

CLASS: SetEnumFlag
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public    readonly NodeCanvas.Framework.Internal.BBObjectParameterVariable  // 0x68
  public    readonly NodeCanvas.Framework.Internal.BBObjectParameterFlag  // 0x70
  public    readonly NodeCanvas.Framework.BBParameter<System.Boolean>Clear  // 0x78
PROPERTIES:
  info  get=0x0A921FEC
METHODS:
  RVA=0x0A921D4C  token=0x6000002  System.Void OnExecute()
  RVA=0x0A921EC0  token=0x6000003  System.Void .ctor()
END_CLASS

CLASS: CheckEnumFlag
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public    readonly NodeCanvas.Framework.Internal.BBObjectParameterVariable  // 0x68
  public    readonly NodeCanvas.Framework.Internal.BBObjectParameterFlag  // 0x70
PROPERTIES:
  info  get=0x0A914B88
METHODS:
  RVA=0x0A9149E0  token=0x6000005  System.Boolean OnCheck()
  RVA=0x0A914A98  token=0x6000006  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200000C
SIZE:  0x10
FIELDS:
  public    static readonly FlowCanvas.BinderConnection.<>c <>9  // static @ 0x0
  public    static  FlowCanvas.ValueHandlerObject   <>9__19_0  // static @ 0x8
  public    static  FlowCanvas.FlowHandler          <>9__20_0  // static @ 0x10
METHODS:
  RVA=0x0A925804  token=0x6000024  System.Void .cctor()
  RVA=0x041E1670  token=0x6000025  System.Void .ctor()
  RVA=0x0A925108  token=0x6000026  System.Object <GatherAndValidateSourcePort>b__19_0()
  RVA=0x0A925164  token=0x6000027  System.Void <GatherAndValidateTargetPort>b__20_0(FlowCanvas.Flow f)
END_CLASS

CLASS: ReturnData
TYPE:  sealed struct
TOKEN: 0x2000013
SIZE:  0x20
FIELDS:
  private           FlowCanvas.FlowReturn           <returnCall>k__BackingField  // 0x10
  private           System.Type                     <returnType>k__BackingField  // 0x18
PROPERTIES:
  returnCall  get=0x02B76770  set=0x05392320
  returnType  get=0x04D88320  set=0x067D5E9C
METHODS:
  RVA=0x039CF630  token=0x600004B  System.Void .ctor(FlowCanvas.FlowReturn call, System.Type type)
END_CLASS

CLASS: ContextDefinedInputsAttribute
TYPE:  class
TOKEN: 0x2000016
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.Type[]                   types  // 0x10
METHODS:
  RVA=0x053908C0  token=0x600009E  System.Void .ctor(System.Type[] types)
END_CLASS

CLASS: ContextDefinedOutputsAttribute
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.Type[]                   types  // 0x10
METHODS:
  RVA=0x053908C0  token=0x600009F  System.Void .ctor(System.Type[] types)
END_CLASS

CLASS: HasRefreshButtonAttribute
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60000A0  System.Void .ctor()
END_CLASS

CLASS: GatherPortsCallbackAttribute
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x18
EXTENDS: ParadoxNotion.Design.CallbackAttribute
FIELDS:
METHODS:
  RVA=0x0A91C868  token=0x60000A1  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass43_0
TYPE:  sealed class
TOKEN: 0x200001A
SIZE:  0x20
FIELDS:
  public            FlowCanvas.FlowNode             <>4__this  // 0x10
  public            System.String                   ID  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60000A2  System.Void .ctor()
  RVA=0x0A925564  token=0x60000A3  System.Boolean <GetInputPort>b__0(FlowCanvas.Port p)
END_CLASS

CLASS: <>c__DisplayClass44_0
TYPE:  sealed class
TOKEN: 0x200001B
SIZE:  0x20
FIELDS:
  public            FlowCanvas.FlowNode             <>4__this  // 0x10
  public            System.String                   ID  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60000A4  System.Void .ctor()
  RVA=0x0A925564  token=0x60000A5  System.Boolean <GetOutputPort>b__0(FlowCanvas.Port p)
END_CLASS

CLASS: <>c__DisplayClass50_0
TYPE:  sealed class
TOKEN: 0x200001C
SIZE:  0x18
FIELDS:
  public            System.Type                     type  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60000A6  System.Void .ctor()
  RVA=0x0A9255C8  token=0x60000A7  System.Boolean <GetFirstInputOfType>b__1(FlowCanvas.Port p)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200001D
SIZE:  0x10
FIELDS:
  public    static readonly FlowCanvas.FlowNode.<>c         <>9  // static @ 0x0
  public    static  System.Func<FlowCanvas.Port,System.Int32><>9__50_0  // static @ 0x8
  public    static  System.Func<FlowCanvas.Port,System.Int32><>9__51_0  // static @ 0x10
  public    static  System.Func<FlowCanvas.ValueInput,System.Boolean><>9__52_0  // static @ 0x18
  public    static  System.Func<System.Collections.Generic.KeyValuePair<System.String,System.Object>,System.String><>9__78_0  // static @ 0x20
  public    static  System.Func<System.Collections.Generic.KeyValuePair<System.String,System.Object>,System.Object><>9__78_1  // static @ 0x28
METHODS:
  RVA=0x0A9257A0  token=0x60000A8  System.Void .cctor()
  RVA=0x041E1670  token=0x60000A9  System.Void .ctor()
  RVA=0x0A9251C0  token=0x60000AA  System.Int32 <GetFirstInputOfType>b__50_0(FlowCanvas.Port p)
  RVA=0x0A92520C  token=0x60000AB  System.Int32 <GetFirstOutputOfType>b__51_0(FlowCanvas.Port p)
  RVA=0x0A9250D4  token=0x60000AC  System.Boolean <AssignSelfInstancePort>b__52_0(FlowCanvas.ValueInput p)
  RVA=0x0A925258  token=0x60000AD  System.String <ReplaceWith>b__78_0(System.Collections.Generic.KeyValuePair<System.String,System.Object> k)
  RVA=0x0A92528C  token=0x60000AE  System.Object <ReplaceWith>b__78_1(System.Collections.Generic.KeyValuePair<System.String,System.Object> v)
END_CLASS

CLASS: <>c__DisplayClass51_0
TYPE:  sealed class
TOKEN: 0x200001E
SIZE:  0x18
FIELDS:
  public            System.Type                     type  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60000AF  System.Void .ctor()
  RVA=0x0A925644  token=0x60000B0  System.Boolean <GetFirstOutputOfType>b__1(FlowCanvas.Port p)
END_CLASS

CLASS: <>c__DisplayClass56_0
TYPE:  sealed class
TOKEN: 0x200001F
SIZE:  0x28
FIELDS:
  public            System.Collections.Generic.KeyValuePair<System.String,System.Object>pair  // 0x10
  public            FlowCanvas.FlowNode             <>4__this  // 0x20
METHODS:
  RVA=0x041E1670  token=0x60000B1  System.Void .ctor()
  RVA=0x0A9256B4  token=0x60000B2  System.Boolean <DeserializeInputPortValues>b__0(FlowCanvas.Port p)
END_CLASS

CLASS: BindStatus
TYPE:  sealed struct
TOKEN: 0x2000027
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  FlowCanvas.Port.BindStatus      Valid  // const
  public    static  FlowCanvas.Port.BindStatus      Missing  // const
  public    static  FlowCanvas.Port.BindStatus      InvalidCast  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x200002E
FIELDS:
  public            FlowCanvas.ValueHandlerObject   getter  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000132  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000133  T <.ctor>b__0()
END_CLASS

CLASS: CustomConverter
TYPE:  sealed class
TOKEN: 0x2000030
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0679AFA0  token=0x600013C  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x014F3120  token=0x600013D  FlowCanvas.ValueHandler<System.Object> Invoke(System.Type sourceType, System.Type targetType, FlowCanvas.ValueHandler<System.Object> func)
  RVA=0x05FD2238  token=0x600013E  System.IAsyncResult BeginInvoke(System.Type sourceType, System.Type targetType, FlowCanvas.ValueHandler<System.Object> func, System.AsyncCallback callback, System.Object object)
  RVA=0x05397E84  token=0x600013F  FlowCanvas.ValueHandler<System.Object> EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: CustomCanConvert
TYPE:  sealed class
TOKEN: 0x2000031
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0A916C70  token=0x6000140  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x015802E0  token=0x6000141  System.Nullable<System.Boolean> Invoke(System.Type sourceType, System.Type targetType)
  RVA=0x05E43E3C  token=0x6000142  System.IAsyncResult BeginInvoke(System.Type sourceType, System.Type targetType, System.AsyncCallback callback, System.Object object)
  RVA=0x09612FF8  token=0x6000143  System.Nullable<System.Boolean> EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: <>c__DisplayClass8_0`1
TYPE:  sealed class
TOKEN: 0x2000032
FIELDS:
  public            FlowCanvas.ValueHandler<System.Object>func  // 0x0
  public            System.Type                     targetType  // 0x0
  public            System.Linq.Expressions.UnaryExpressionexp  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000144  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000145  T <GetConverterFuncFromTo>b__0()
  RVA=-1  // not resolved  token=0x6000146  T <GetConverterFuncFromTo>b__1()
  RVA=-1  // not resolved  token=0x6000147  T <GetConverterFuncFromTo>b__2()
  RVA=-1  // not resolved  token=0x6000148  T <GetConverterFuncFromTo>b__3()
  RVA=-1  // not resolved  token=0x6000149  T <GetConverterFuncFromTo>b__4()
  RVA=-1  // not resolved  token=0x600014A  T <GetConverterFuncFromTo>b__5()
  RVA=-1  // not resolved  token=0x600014B  T <GetConverterFuncFromTo>b__6()
  RVA=-1  // not resolved  token=0x600014C  T <GetConverterFuncFromTo>b__7()
  RVA=-1  // not resolved  token=0x600014D  T <GetConverterFuncFromTo>b__8()
  RVA=-1  // not resolved  token=0x600014E  T <GetConverterFuncFromTo>b__9()
  RVA=-1  // not resolved  token=0x600014F  T <GetConverterFuncFromTo>b__10()
  RVA=-1  // not resolved  token=0x6000150  T <GetConverterFuncFromTo>b__11()
  RVA=-1  // not resolved  token=0x6000151  T <GetConverterFuncFromTo>b__12()
  RVA=-1  // not resolved  token=0x6000152  T <GetConverterFuncFromTo>b__13()
  RVA=-1  // not resolved  token=0x6000153  T <GetConverterFuncFromTo>b__14()
END_CLASS

CLASS: <>c__DisplayClass8_1`1
TYPE:  sealed class
TOKEN: 0x2000033
FIELDS:
  public            FlowCanvas.ValueHandler<System.Object>converter  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000154  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000155  T <GetConverterFuncFromTo>b__15()
END_CLASS

CLASS: <>c__DisplayClass8_2`1
TYPE:  sealed class
TOKEN: 0x2000034
FIELDS:
  public            System.Type                     listType  // 0x0
  public            FlowCanvas.TypeConverter.<>c__DisplayClass8_0<T>CS$<>8__locals1  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000156  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000157  T <GetConverterFuncFromTo>b__16()
END_CLASS

CLASS: <>c__DisplayClass11_0
TYPE:  sealed class
TOKEN: 0x2000035
SIZE:  0x18
FIELDS:
  public            System.Object                   obj  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000158  System.Void .ctor()
  RVA=0x02B2ECC0  token=0x6000159  System.Object <QuickConvert>b__0()
END_CLASS

CLASS: <>c__DisplayClass14_0
TYPE:  sealed class
TOKEN: 0x200003D
SIZE:  0x20
FIELDS:
  public            System.Int32                    i  // 0x10
  public            FlowCanvas.Nodes.CSharpAutoCallbackEvent<>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000179  System.Void .ctor()
  RVA=0x0A9254BC  token=0x600017A  System.Object <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass14_0
TYPE:  sealed class
TOKEN: 0x200003F
SIZE:  0x20
FIELDS:
  public            System.Int32                    i  // 0x10
  public            FlowCanvas.Nodes.CSharpEventCallback<>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000188  System.Void .ctor()
  RVA=0x0A9254F4  token=0x6000189  System.Object <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass9_0
TYPE:  sealed class
TOKEN: 0x2000043
SIZE:  0x20
FIELDS:
  public            System.Int32                    i  // 0x10
  public            FlowCanvas.Nodes.DelegateCallbackEvent<>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001A4  System.Void .ctor()
  RVA=0x0A925768  token=0x60001A5  System.Object <RegisterPorts>b__1()
END_CLASS

CLASS: <>c__DisplayClass8_0
TYPE:  sealed class
TOKEN: 0x2000045
SIZE:  0x20
FIELDS:
  public            FlowCanvas.Nodes.SharpEvent     wrapper  // 0x10
  public            FlowCanvas.Nodes.GetSharpEvent  <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001AC  System.Void .ctor()
  RVA=0x0A925708  token=0x60001AD  System.Object <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass10_0
TYPE:  sealed class
TOKEN: 0x2000047
SIZE:  0x20
FIELDS:
  public            FlowCanvas.Nodes.InvokeSignal   <>4__this  // 0x10
  public            FlowCanvas.FlowOutput           exit  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001B4  System.Void .ctor()
  RVA=0x0A9252C4  token=0x60001B5  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass15_0
TYPE:  sealed class
TOKEN: 0x2000049
SIZE:  0x20
FIELDS:
  public            System.Int32                    i  // 0x10
  public            FlowCanvas.Nodes.SignalCallback <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001C3  System.Void .ctor()
  RVA=0x0A92552C  token=0x60001C4  System.Object <RegisterPorts>b__2()
END_CLASS

CLASS: <>c__DisplayClass20_0
TYPE:  sealed class
TOKEN: 0x200004B
SIZE:  0x20
FIELDS:
  public            System.Int32                    i  // 0x10
  public            FlowCanvas.Nodes.UnityEventAutoCallbackEvent<>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001D2  System.Void .ctor()
  RVA=0x0A9254BC  token=0x60001D3  System.Object <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass14_0
TYPE:  sealed class
TOKEN: 0x200004D
SIZE:  0x20
FIELDS:
  public            System.Int32                    i  // 0x10
  public            FlowCanvas.Nodes.UnityEventCallbackEvent<>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001E1  System.Void .ctor()
  RVA=0x0A9254F4  token=0x60001E2  System.Object <RegisterPorts>b__0()
END_CLASS

CLASS: TargetMode
TYPE:  sealed struct
TOKEN: 0x2000051
FIELDS:
  public            System.Int32                    value__  // 0x0
  public    static  FlowCanvas.Nodes.RouterEventNode.TargetMode<T>SingleTarget  // const
  public    static  FlowCanvas.Nodes.RouterEventNode.TargetMode<T>MultipleTargets  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x200005C
SIZE:  0x20
FIELDS:
  public            System.Int32                    i  // 0x10
  public            FlowCanvas.Nodes.InputCustomAxisEvent<>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000217  System.Void .ctor()
  RVA=0x0A92558C  token=0x6000218  System.Single <RegisterPorts>b__0()
END_CLASS

CLASS: ButtonKeys
TYPE:  sealed struct
TOKEN: 0x200005F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  FlowCanvas.Nodes.MouseEvents.ButtonKeysLeft  // const
  public    static  FlowCanvas.Nodes.MouseEvents.ButtonKeysRight  // const
  public    static  FlowCanvas.Nodes.MouseEvents.ButtonKeysMiddle  // const
METHODS:
END_CLASS

CLASS: ButtonKeys
TYPE:  sealed struct
TOKEN: 0x2000061
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  FlowCanvas.Nodes.MousePickEvent.ButtonKeysLeft  // const
  public    static  FlowCanvas.Nodes.MousePickEvent.ButtonKeysRight  // const
  public    static  FlowCanvas.Nodes.MousePickEvent.ButtonKeysMiddle  // const
METHODS:
END_CLASS

CLASS: <UpdatePressed>d__17
TYPE:  sealed class
TOKEN: 0x2000077
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            FlowCanvas.Nodes.UIPointerEvents<>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60002F2  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60002F3  System.Void System.IDisposable.Dispose()
  RVA=0x0A93AA98  token=0x60002F4  System.Boolean MoveNext()
  RVA=0x0A93AB24  token=0x60002F6  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass0_0
TYPE:  sealed class
TOKEN: 0x2000081
SIZE:  0x38
FIELDS:
  public            FlowCanvas.ValueInput<System.Single>min  // 0x10
  public            FlowCanvas.ValueInput<System.Single>max  // 0x18
  public            FlowCanvas.ValueInput<System.Single>chance  // 0x20
  public            FlowCanvas.FlowOutput           success  // 0x28
  public            FlowCanvas.FlowOutput           failure  // 0x30
METHODS:
  RVA=0x041E1670  token=0x6000328  System.Void .ctor()
  RVA=0x0A938C74  token=0x6000329  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <CountDown>d__14
TYPE:  sealed class
TOKEN: 0x2000083
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            FlowCanvas.Nodes.Cooldown       <>4__this  // 0x20
  public            FlowCanvas.Flow                 f  // 0x28
  private           System.Single                   <total>5__2  // 0x50
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000334  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000335  System.Void System.IDisposable.Dispose()
  RVA=0x0A936FB8  token=0x6000336  System.Boolean MoveNext()
  RVA=0x0A9371C8  token=0x6000338  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x2000085
SIZE:  0x20
FIELDS:
  public            FlowCanvas.Nodes.DoOnce         <>4__this  // 0x10
  public            FlowCanvas.FlowOutput           o  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600033D  System.Void .ctor()
  RVA=0x0A9398E0  token=0x600033E  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
  RVA=0x0A939A40  token=0x600033F  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x2000087
FIELDS:
  public            FlowCanvas.Nodes.ReadFlowParameter<T><>4__this  // 0x0
  public            FlowCanvas.ValueInput<System.String>pName  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000342  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000343  T <RegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x6000344  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass0_0
TYPE:  sealed class
TOKEN: 0x2000089
FIELDS:
  public            FlowCanvas.ValueInput<System.String>pName  // 0x0
  public            FlowCanvas.ValueInput<T>        pValue  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000347  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000348  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x200008B
SIZE:  0x20
FIELDS:
  public            FlowCanvas.Nodes.LatchBool      <>4__this  // 0x10
  public            FlowCanvas.FlowOutput           o  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600034B  System.Void .ctor()
  RVA=0x0A939400  token=0x600034C  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
  RVA=0x0A9397F4  token=0x600034D  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
  RVA=0x07C376F8  token=0x600034E  System.Boolean <RegisterPorts>b__2()
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x200008D
SIZE:  0x20
FIELDS:
  public            FlowCanvas.Nodes.LatchInt       <>4__this  // 0x10
  public            FlowCanvas.FlowOutput           o  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000351  System.Void .ctor()
  RVA=0x0861D818  token=0x6000352  System.Int32 <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass2_1
TYPE:  sealed class
TOKEN: 0x200008E
SIZE:  0x20
FIELDS:
  public            System.Int32                    i  // 0x10
  public            FlowCanvas.Nodes.LatchInt.<>c__DisplayClass2_0CS$<>8__locals1  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000353  System.Void .ctor()
  RVA=0x0A939A60  token=0x6000354  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass6_0
TYPE:  sealed class
TOKEN: 0x2000090
SIZE:  0x20
FIELDS:
  public            System.Int32                    i  // 0x10
  public            FlowCanvas.Nodes.ANDMerge       <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600035A  System.Void .ctor()
  RVA=0x0A93A27C  token=0x600035B  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass6_0
TYPE:  sealed class
TOKEN: 0x2000092
SIZE:  0x20
FIELDS:
  public            System.Int32                    i  // 0x10
  public            FlowCanvas.Nodes.AwaitAll       <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000361  System.Void .ctor()
  RVA=0x0A93A27C  token=0x6000362  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x2000094
SIZE:  0x20
FIELDS:
  public            System.Int32                    i  // 0x10
  public            FlowCanvas.Nodes.ORMerge        <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000366  System.Void .ctor()
  RVA=0x0A939BB0  token=0x6000367  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x2000096
SIZE:  0x20
FIELDS:
  public            System.Int32                    i  // 0x10
  public            FlowCanvas.Nodes.XORMerge       <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600036B  System.Void .ctor()
  RVA=0x0A939DF4  token=0x600036C  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x200009A
SIZE:  0x30
FIELDS:
  public            FlowCanvas.Nodes.ForEach        <>4__this  // 0x10
  public            FlowCanvas.FlowOutput           fFinish  // 0x18
  public            FlowCanvas.FlowOutput           fCurrent  // 0x20
  public            FlowCanvas.FlowBreak            <>9__4  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000375  System.Void .ctor()
  RVA=0x08107360  token=0x6000376  System.Object <RegisterPorts>b__0()
  RVA=0x0861D898  token=0x6000377  System.Int32 <RegisterPorts>b__1()
  RVA=0x0A939E40  token=0x6000378  System.Void <RegisterPorts>b__2(FlowCanvas.Flow f)
  RVA=0x08622480  token=0x6000379  System.Void <RegisterPorts>b__4()
  RVA=0x08622480  token=0x600037A  System.Void <RegisterPorts>b__3(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x200009C
FIELDS:
  public            FlowCanvas.Nodes.ForEach<T>     <>4__this  // 0x0
  public            FlowCanvas.ValueInput<System.Collections.Generic.IEnumerable<T>>list  // 0x0
  public            FlowCanvas.FlowOutput           fFinish  // 0x0
  public            FlowCanvas.FlowOutput           fCurrent  // 0x0
  public            FlowCanvas.FlowBreak            <>9__4  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600037D  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600037E  T <RegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x600037F  System.Int32 <RegisterPorts>b__1()
  RVA=-1  // not resolved  token=0x6000380  System.Void <RegisterPorts>b__2(FlowCanvas.Flow f)
  RVA=-1  // not resolved  token=0x6000381  System.Void <RegisterPorts>b__4()
  RVA=-1  // not resolved  token=0x6000382  System.Void <RegisterPorts>b__3(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass6_0
TYPE:  sealed class
TOKEN: 0x200009E
SIZE:  0x30
FIELDS:
  public            FlowCanvas.Nodes.ForLoop        <>4__this  // 0x10
  public            FlowCanvas.FlowOutput           fCurrent  // 0x18
  public            FlowCanvas.FlowOutput           fFinish  // 0x20
  public            FlowCanvas.FlowBreak            <>9__3  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000385  System.Void .ctor()
  RVA=0x0861D818  token=0x6000386  System.Int32 <RegisterPorts>b__0()
  RVA=0x0A93A324  token=0x6000387  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
  RVA=0x08622540  token=0x6000388  System.Void <RegisterPorts>b__3()
  RVA=0x08622540  token=0x6000389  System.Void <RegisterPorts>b__2(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x20000A0
SIZE:  0x18
FIELDS:
  public            FlowCanvas.FlowOutput           fOut  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600038D  System.Void .ctor()
  RVA=0x0A939940  token=0x600038E  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass0_0
TYPE:  sealed class
TOKEN: 0x20000A2
SIZE:  0x20
FIELDS:
  public            FlowCanvas.Nodes.Finish         <>4__this  // 0x10
  public            FlowCanvas.ValueInput<System.Boolean>c  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000391  System.Void .ctor()
  RVA=0x0A938C04  token=0x6000392  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20000A7
SIZE:  0x20
FIELDS:
  public            System.Collections.Generic.List<FlowCanvas.FlowOutput>outs  // 0x10
  public            FlowCanvas.Nodes.Split          <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60003A3  System.Void .ctor()
  RVA=0x0A939564  token=0x60003A4  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass0_0
TYPE:  sealed class
TOKEN: 0x20000A9
SIZE:  0x28
FIELDS:
  public            FlowCanvas.FlowOutput           fTry  // 0x10
  public            FlowCanvas.FlowOutput           fCatch  // 0x18
  public            FlowCanvas.FlowOutput           fFinally  // 0x20
METHODS:
  RVA=0x041E1670  token=0x60003A7  System.Void .ctor()
  RVA=0x0A938E88  token=0x60003A8  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x20000AB
SIZE:  0x30
FIELDS:
  public            FlowCanvas.Nodes.CoroutineState <>4__this  // 0x10
  public            FlowCanvas.FlowOutput           fStart  // 0x18
  public            FlowCanvas.FlowOutput           fUpdate  // 0x20
  public            FlowCanvas.FlowOutput           fFinish  // 0x28
METHODS:
  RVA=0x041E1670  token=0x60003AE  System.Void .ctor()
  RVA=0x0A939BFC  token=0x60003AF  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
  RVA=0x0A939A40  token=0x60003B0  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <DoRepeat>d__4
TYPE:  sealed class
TOKEN: 0x20000AC
SIZE:  0x68
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            FlowCanvas.FlowOutput           fStart  // 0x20
  public            FlowCanvas.Flow                 f  // 0x28
  public            FlowCanvas.Nodes.CoroutineState <>4__this  // 0x50
  public            FlowCanvas.FlowOutput           fUpdate  // 0x58
  public            FlowCanvas.FlowOutput           fFinish  // 0x60
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003B1  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003B2  System.Void System.IDisposable.Dispose()
  RVA=0x0A937214  token=0x60003B3  System.Boolean MoveNext()
  RVA=0x0A9373E0  token=0x60003B5  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x20000AE
SIZE:  0x30
FIELDS:
  public            FlowCanvas.Nodes.While          <>4__this  // 0x10
  public            FlowCanvas.FlowOutput           fUpdate  // 0x18
  public            FlowCanvas.FlowOutput           fFinish  // 0x20
  public            FlowCanvas.ValueInput<System.Boolean>c  // 0x28
METHODS:
  RVA=0x041E1670  token=0x60003BC  System.Void .ctor()
  RVA=0x0A939B18  token=0x60003BD  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x20000AF
SIZE:  0x18
FIELDS:
  public            System.Boolean                  active  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60003BE  System.Void .ctor()
  RVA=0x04D8CCB0  token=0x60003BF  System.Void <DoWhile>b__0()
END_CLASS

CLASS: <DoWhile>d__4
TYPE:  sealed class
TOKEN: 0x20000B0
SIZE:  0x70
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            FlowCanvas.Flow                 f  // 0x20
  public            FlowCanvas.Nodes.While          <>4__this  // 0x48
  public            FlowCanvas.FlowOutput           fUpdate  // 0x50
  private           FlowCanvas.Nodes.While.<>c__DisplayClass4_0<>8__1  // 0x58
  public            FlowCanvas.ValueInput<System.Boolean>condition  // 0x60
  public            FlowCanvas.FlowOutput           fFinish  // 0x68
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60003C0  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60003C1  System.Void System.IDisposable.Dispose()
  RVA=0x0A93742C  token=0x60003C2  System.Boolean MoveNext()
  RVA=0x0A93765C  token=0x60003C4  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x20000B3
FIELDS:
  public            FlowCanvas.ValueInput           selector  // 0x0
  public            FlowCanvas.ValueInput<T>[]      cases  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60003CF  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60003D0  T <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20000B5
FIELDS:
  public            FlowCanvas.ValueInput<System.Int32>selector  // 0x0
  public            FlowCanvas.ValueInput<T>[]      cases  // 0x0
  public            FlowCanvas.ValueInput<T>        defaultCase  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60003D3  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60003D4  T <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20000B7
FIELDS:
  public            FlowCanvas.ValueInput<System.String>selector  // 0x0
  public            FlowCanvas.Nodes.SelectOnString<T><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T>[]      cases  // 0x0
  public            FlowCanvas.ValueInput<T>        defaultCase  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60003D7  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60003D8  T <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20000B9
FIELDS:
  public            FlowCanvas.ValueInput<UnityEngine.GameObject>selector  // 0x0
  public            FlowCanvas.Nodes.SelectOnTag<T> <>4__this  // 0x0
  public            FlowCanvas.ValueInput<T>[]      cases  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60003DB  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60003DC  T <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x20000BB
SIZE:  0x20
FIELDS:
  public            FlowCanvas.Nodes.Random         <>4__this  // 0x10
  public            System.Collections.Generic.List<FlowCanvas.FlowOutput>outs  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60003DF  System.Void .ctor()
  RVA=0x0A939984  token=0x60003E0  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
  RVA=0x0861D818  token=0x60003E1  System.Int32 <RegisterPorts>b__1()
END_CLASS

CLASS: <>c__DisplayClass0_0
TYPE:  sealed class
TOKEN: 0x20000BD
SIZE:  0x30
FIELDS:
  public            FlowCanvas.ValueInput<System.Boolean>selector  // 0x10
  public            FlowCanvas.FlowOutput           caseTrue  // 0x18
  public            FlowCanvas.FlowOutput           caseFalse  // 0x20
  public            FlowCanvas.FlowOutput           caseThen  // 0x28
METHODS:
  RVA=0x041E1670  token=0x60003E4  System.Void .ctor()
  RVA=0x0A938B88  token=0x60003E5  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass0_0
TYPE:  sealed class
TOKEN: 0x20000BF
SIZE:  0x40
FIELDS:
  public            FlowCanvas.ValueInput<System.IComparable>a  // 0x10
  public            FlowCanvas.ValueInput<System.IComparable>b  // 0x18
  public            FlowCanvas.FlowOutput           equal  // 0x20
  public            FlowCanvas.FlowOutput           notEqual  // 0x28
  public            FlowCanvas.FlowOutput           greater  // 0x30
  public            FlowCanvas.FlowOutput           less  // 0x38
METHODS:
  RVA=0x041E1670  token=0x60003E8  System.Void .ctor()
  RVA=0x0A938D28  token=0x60003E9  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x20000C1
SIZE:  0x20
FIELDS:
  public            FlowCanvas.ValueInput           selector  // 0x10
  public            System.Collections.Generic.Dictionary<System.String,FlowCanvas.FlowOutput>cases  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60003F0  System.Void .ctor()
  RVA=0x0A939D3C  token=0x60003F1  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20000C3
SIZE:  0x28
FIELDS:
  public            FlowCanvas.ValueInput<System.Int32>selector  // 0x10
  public            FlowCanvas.FlowOutput[]         cases  // 0x18
  public            FlowCanvas.FlowOutput           defaultCase  // 0x20
METHODS:
  RVA=0x041E1670  token=0x60003F4  System.Void .ctor()
  RVA=0x0A939770  token=0x60003F5  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20000C5
SIZE:  0x30
FIELDS:
  public            FlowCanvas.ValueInput<System.Int32>selector  // 0x10
  public            FlowCanvas.Nodes.SwitchIntCustom<>4__this  // 0x18
  public            FlowCanvas.FlowOutput[]         cases  // 0x20
  public            FlowCanvas.FlowOutput           defaultCase  // 0x28
METHODS:
  RVA=0x041E1670  token=0x60003F8  System.Void .ctor()
  RVA=0x0A93960C  token=0x60003F9  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20000C8
SIZE:  0x30
FIELDS:
  public            FlowCanvas.ValueInput<System.String>selector  // 0x10
  public            FlowCanvas.Nodes.SwitchString   <>4__this  // 0x18
  public            FlowCanvas.FlowOutput[]         cases  // 0x20
  public            FlowCanvas.FlowOutput           defaultCase  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000400  System.Void .ctor()
  RVA=0x0A93927C  token=0x6000401  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20000CA
SIZE:  0x28
FIELDS:
  public            FlowCanvas.Nodes.SwitchTag      <>4__this  // 0x10
  public            FlowCanvas.ValueInput<UnityEngine.GameObject>selector  // 0x18
  public            FlowCanvas.FlowOutput[]         cases  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000404  System.Void .ctor()
  RVA=0x0A939454  token=0x6000405  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass6_0
TYPE:  sealed class
TOKEN: 0x20000CC
SIZE:  0x28
FIELDS:
  public            FlowCanvas.Nodes.FlipFlop       <>4__this  // 0x10
  public            FlowCanvas.FlowOutput           fFlip  // 0x18
  public            FlowCanvas.FlowOutput           fFlop  // 0x20
METHODS:
  RVA=0x041E1670  token=0x600040B  System.Void .ctor()
  RVA=0x0A93A224  token=0x600040C  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
  RVA=0x0A939A40  token=0x600040D  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
  RVA=0x07C376F8  token=0x600040E  System.Boolean <RegisterPorts>b__2()
END_CLASS

CLASS: <>c__DisplayClass7_0
TYPE:  sealed class
TOKEN: 0x20000CE
SIZE:  0x20
FIELDS:
  public            System.Collections.Generic.List<FlowCanvas.FlowOutput>outs  // 0x10
  public            FlowCanvas.Nodes.MultiToggle    <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000414  System.Void .ctor()
  RVA=0x0A93A624  token=0x6000415  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
  RVA=0x0A93A7A8  token=0x6000416  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
  RVA=0x0A93A7EC  token=0x6000417  System.Void <RegisterPorts>b__2(FlowCanvas.Flow f)
  RVA=0x0A93A894  token=0x6000418  System.Int32 <RegisterPorts>b__3()
END_CLASS

CLASS: <>c__DisplayClass7_0
TYPE:  sealed class
TOKEN: 0x20000D0
SIZE:  0x20
FIELDS:
  public            System.Collections.Generic.List<FlowCanvas.FlowOutput>outs  // 0x10
  public            FlowCanvas.Nodes.Sequence       <>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600041E  System.Void .ctor()
  RVA=0x0A93A6B8  token=0x600041F  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
  RVA=0x0A93A784  token=0x6000420  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
  RVA=0x0A93A894  token=0x6000421  System.Int32 <RegisterPorts>b__2()
END_CLASS

CLASS: <>c__DisplayClass6_0
TYPE:  sealed class
TOKEN: 0x20000D2
SIZE:  0x28
FIELDS:
  public            FlowCanvas.Nodes.Toggle         <>4__this  // 0x10
  public            FlowCanvas.FlowOutput           fOn  // 0x18
  public            FlowCanvas.FlowOutput           fOff  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000427  System.Void .ctor()
  RVA=0x0A93A2C8  token=0x6000428  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
  RVA=0x0A93A304  token=0x6000429  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
  RVA=0x0A939A40  token=0x600042A  System.Void <RegisterPorts>b__2(FlowCanvas.Flow f)
  RVA=0x0A93A5FC  token=0x600042B  System.Void <RegisterPorts>b__3(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000D5
SIZE:  0x10
FIELDS:
  public    static readonly FlowCanvas.Nodes.CustomFunctionEvent.<>c<>9  // static @ 0x0
  public    static  System.Func<ParadoxNotion.DynamicParameterDefinition,System.Type><>9__15_0  // static @ 0x8
METHODS:
  RVA=0x0A93A96C  token=0x600044A  System.Void .cctor()
  RVA=0x041E1670  token=0x600044B  System.Void .ctor()
  RVA=0x0A938B70  token=0x600044C  System.Type <get_parameterTypes>b__15_0(ParadoxNotion.DynamicParameterDefinition p)
END_CLASS

CLASS: <>c__DisplayClass19_0
TYPE:  sealed class
TOKEN: 0x20000D6
SIZE:  0x20
FIELDS:
  public            System.Int32                    i  // 0x10
  public            FlowCanvas.Nodes.CustomFunctionEvent<>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600044D  System.Void .ctor()
  RVA=0x0A9391DC  token=0x600044E  System.Object <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass22_0
TYPE:  sealed class
TOKEN: 0x20000D7
SIZE:  0x20
FIELDS:
  public            System.Action<System.Object>    callback  // 0x10
  public            FlowCanvas.Nodes.CustomFunctionEvent<>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600044F  System.Void .ctor()
  RVA=0x0A939848  token=0x6000450  System.Void <NodeCanvas.Framework.IInvokable.InvokeAsync>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass24_0
TYPE:  sealed class
TOKEN: 0x20000D8
SIZE:  0x48
FIELDS:
  public            FlowCanvas.Nodes.CustomFunctionEvent<>4__this  // 0x10
  public            FlowCanvas.FlowHandler          flowCallback  // 0x18
  public            FlowCanvas.Flow                 f  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000451  System.Void .ctor()
  RVA=0x0A93987C  token=0x6000452  System.Void <InvokeAsync>b__0(System.Object o)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20000DC
SIZE:  0x20
FIELDS:
  public            FlowCanvas.Nodes.CallableActionNode<>4__this  // 0x10
  public            FlowCanvas.FlowOutput           o  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600045A  System.Void .ctor()
  RVA=0x0A939214  token=0x600045B  System.Void <OnRegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20000DE
FIELDS:
  public            FlowCanvas.Nodes.CallableActionNode<T1><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600045F  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000460  System.Void <OnRegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20000E0
FIELDS:
  public            FlowCanvas.Nodes.CallableActionNode<T1,T2><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000464  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000465  System.Void <OnRegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20000E2
FIELDS:
  public            FlowCanvas.Nodes.CallableActionNode<T1,T2,T3><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000469  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600046A  System.Void <OnRegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20000E4
FIELDS:
  public            FlowCanvas.Nodes.CallableActionNode<T1,T2,T3,T4><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600046E  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600046F  System.Void <OnRegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20000E6
FIELDS:
  public            FlowCanvas.Nodes.CallableActionNode<T1,T2,T3,T4,T5><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000473  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000474  System.Void <OnRegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20000E8
FIELDS:
  public            FlowCanvas.Nodes.CallableActionNode<T1,T2,T3,T4,T5,T6><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000478  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000479  System.Void <OnRegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20000EA
FIELDS:
  public            FlowCanvas.Nodes.CallableActionNode<T1,T2,T3,T4,T5,T6,T7><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600047D  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600047E  System.Void <OnRegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20000EC
FIELDS:
  public            FlowCanvas.Nodes.CallableActionNode<T1,T2,T3,T4,T5,T6,T7,T8><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
  public            FlowCanvas.ValueInput<T8>       p8  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000482  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000483  System.Void <OnRegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20000EE
FIELDS:
  public            FlowCanvas.Nodes.CallableActionNode<T1,T2,T3,T4,T5,T6,T7,T8,T9><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
  public            FlowCanvas.ValueInput<T8>       p8  // 0x0
  public            FlowCanvas.ValueInput<T9>       p9  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000487  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000488  System.Void <OnRegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20000F0
FIELDS:
  public            FlowCanvas.Nodes.CallableActionNode<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
  public            FlowCanvas.ValueInput<T8>       p8  // 0x0
  public            FlowCanvas.ValueInput<T9>       p9  // 0x0
  public            FlowCanvas.ValueInput<T10>      p10  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600048C  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600048D  System.Void <OnRegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x20000F3
FIELDS:
  public            FlowCanvas.Nodes.CallableFunctionNode<TResult><>4__this  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000492  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000493  TResult <OnRegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x6000494  System.Void <OnRegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x20000F5
FIELDS:
  public            FlowCanvas.Nodes.CallableFunctionNode<TResult,T1><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000498  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000499  TResult <OnRegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x600049A  System.Void <OnRegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x20000F7
FIELDS:
  public            FlowCanvas.Nodes.CallableFunctionNode<TResult,T1,T2><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600049E  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600049F  TResult <OnRegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x60004A0  System.Void <OnRegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x20000F9
FIELDS:
  public            FlowCanvas.Nodes.CallableFunctionNode<TResult,T1,T2,T3><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60004A4  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60004A5  TResult <OnRegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x60004A6  System.Void <OnRegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x20000FB
FIELDS:
  public            FlowCanvas.Nodes.CallableFunctionNode<TResult,T1,T2,T3,T4><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60004AA  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60004AB  TResult <OnRegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x60004AC  System.Void <OnRegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x20000FD
FIELDS:
  public            FlowCanvas.Nodes.CallableFunctionNode<TResult,T1,T2,T3,T4,T5><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60004B0  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60004B1  TResult <OnRegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x60004B2  System.Void <OnRegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x20000FF
FIELDS:
  public            FlowCanvas.Nodes.CallableFunctionNode<TResult,T1,T2,T3,T4,T5,T6><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60004B6  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60004B7  TResult <OnRegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x60004B8  System.Void <OnRegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x2000101
FIELDS:
  public            FlowCanvas.Nodes.CallableFunctionNode<TResult,T1,T2,T3,T4,T5,T6,T7><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60004BC  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60004BD  TResult <OnRegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x60004BE  System.Void <OnRegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x2000103
FIELDS:
  public            FlowCanvas.Nodes.CallableFunctionNode<TResult,T1,T2,T3,T4,T5,T6,T7,T8><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
  public            FlowCanvas.ValueInput<T8>       p8  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60004C2  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60004C3  TResult <OnRegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x60004C4  System.Void <OnRegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x2000105
FIELDS:
  public            FlowCanvas.Nodes.CallableFunctionNode<TResult,T1,T2,T3,T4,T5,T6,T7,T8,T9><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
  public            FlowCanvas.ValueInput<T8>       p8  // 0x0
  public            FlowCanvas.ValueInput<T9>       p9  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60004C8  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60004C9  TResult <OnRegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x60004CA  System.Void <OnRegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x2000107
FIELDS:
  public            FlowCanvas.Nodes.CallableFunctionNode<TResult,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
  public            FlowCanvas.ValueInput<T8>       p8  // 0x0
  public            FlowCanvas.ValueInput<T9>       p9  // 0x0
  public            FlowCanvas.ValueInput<T10>      p10  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60004CE  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60004CF  TResult <OnRegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x60004D0  System.Void <OnRegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000109
SIZE:  0x10
FIELDS:
  public    static readonly FlowCanvas.Nodes.ExtractorNode.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Type,System.Boolean><>9__1_0  // static @ 0x8
METHODS:
  RVA=0x0A93A908  token=0x60004D3  System.Void .cctor()
  RVA=0x041E1670  token=0x60004D4  System.Void .ctor()
  RVA=0x0A938B50  token=0x60004D5  System.Boolean <GetExtractorType>b__1_0(System.Type t)
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x200010B
FIELDS:
  public            FlowCanvas.Nodes.ExtractorNode<TInstance,T1,T2><>4__this  // 0x0
  public            FlowCanvas.ValueInput<TInstance>i  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60004D9  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60004DA  T1 <OnRegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x60004DB  T2 <OnRegisterPorts>b__1()
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x200010D
FIELDS:
  public            FlowCanvas.Nodes.ExtractorNode<TInstance,T1,T2,T3><>4__this  // 0x0
  public            FlowCanvas.ValueInput<TInstance>i  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60004DF  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60004E0  T1 <OnRegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x60004E1  T2 <OnRegisterPorts>b__1()
  RVA=-1  // not resolved  token=0x60004E2  T3 <OnRegisterPorts>b__2()
END_CLASS

CLASS: <>c__DisplayClass5_0
TYPE:  sealed class
TOKEN: 0x200010F
FIELDS:
  public            FlowCanvas.Nodes.ExtractorNode<TInstance,T1,T2,T3,T4><>4__this  // 0x0
  public            FlowCanvas.ValueInput<TInstance>i  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60004E6  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60004E7  T1 <OnRegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x60004E8  T2 <OnRegisterPorts>b__1()
  RVA=-1  // not resolved  token=0x60004E9  T3 <OnRegisterPorts>b__2()
  RVA=-1  // not resolved  token=0x60004EA  T4 <OnRegisterPorts>b__3()
END_CLASS

CLASS: <>c__DisplayClass6_0
TYPE:  sealed class
TOKEN: 0x2000111
FIELDS:
  public            FlowCanvas.Nodes.ExtractorNode<TInstance,T1,T2,T3,T4,T5><>4__this  // 0x0
  public            FlowCanvas.ValueInput<TInstance>i  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60004EE  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60004EF  T1 <OnRegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x60004F0  T2 <OnRegisterPorts>b__1()
  RVA=-1  // not resolved  token=0x60004F1  T3 <OnRegisterPorts>b__2()
  RVA=-1  // not resolved  token=0x60004F2  T4 <OnRegisterPorts>b__3()
  RVA=-1  // not resolved  token=0x60004F3  T5 <OnRegisterPorts>b__4()
END_CLASS

CLASS: <>c__DisplayClass7_0
TYPE:  sealed class
TOKEN: 0x2000113
FIELDS:
  public            FlowCanvas.Nodes.ExtractorNode<TInstance,T1,T2,T3,T4,T5,T6><>4__this  // 0x0
  public            FlowCanvas.ValueInput<TInstance>i  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60004F7  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60004F8  T1 <OnRegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x60004F9  T2 <OnRegisterPorts>b__1()
  RVA=-1  // not resolved  token=0x60004FA  T3 <OnRegisterPorts>b__2()
  RVA=-1  // not resolved  token=0x60004FB  T4 <OnRegisterPorts>b__3()
  RVA=-1  // not resolved  token=0x60004FC  T5 <OnRegisterPorts>b__4()
  RVA=-1  // not resolved  token=0x60004FD  T6 <OnRegisterPorts>b__5()
END_CLASS

CLASS: <Invoke>d__8
TYPE:  sealed class
TOKEN: 0x2000159
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            FlowCanvas.Nodes.Wait           <>4__this  // 0x20
  public            System.Single                   time  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000593  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000594  System.Void System.IDisposable.Dispose()
  RVA=0x0A938A54  token=0x6000595  System.Boolean MoveNext()
  RVA=0x0A938B04  token=0x6000597  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <Invoke>d__2
TYPE:  sealed class
TOKEN: 0x200015B
SIZE:  0x20
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600059C  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600059D  System.Void System.IDisposable.Dispose()
  RVA=0x0A9381F4  token=0x600059E  System.Boolean MoveNext()
  RVA=0x0A9382A0  token=0x60005A0  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <Invoke>d__2
TYPE:  sealed class
TOKEN: 0x200015D
SIZE:  0x20
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60005A5  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60005A6  System.Void System.IDisposable.Dispose()
  RVA=0x0A938184  token=0x60005A7  System.Boolean MoveNext()
  RVA=0x0A9382EC  token=0x60005A9  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <Invoke>d__2
TYPE:  sealed class
TOKEN: 0x200015F
SIZE:  0x20
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60005AE  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60005AF  System.Void System.IDisposable.Dispose()
  RVA=0x0A938230  token=0x60005B0  System.Boolean MoveNext()
  RVA=0x0A938338  token=0x60005B2  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <Invoke>d__3
TYPE:  sealed class
TOKEN: 0x2000161
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            FlowCanvas.Nodes.WaitUntil      <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60005B8  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60005B9  System.Void System.IDisposable.Dispose()
  RVA=0x0A938384  token=0x60005BA  System.Boolean MoveNext()
  RVA=0x0A93854C  token=0x60005BC  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <Invoke>d__3
TYPE:  sealed class
TOKEN: 0x2000163
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            FlowCanvas.Nodes.WaitWhile      <>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60005C2  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60005C3  System.Void System.IDisposable.Dispose()
  RVA=0x0A938468  token=0x60005C4  System.Boolean MoveNext()
  RVA=0x0A938598  token=0x60005C6  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <Invoke>d__4
TYPE:  sealed class
TOKEN: 0x200016E
SIZE:  0x40
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            FlowCanvas.Nodes.TweenFloat     <>4__this  // 0x20
  public            ParadoxNotion.Animation.EaseTypeinterpolation  // 0x28
  public            System.Single                   from  // 0x2c
  public            System.Single                   to  // 0x30
  public            System.Single                   duration  // 0x34
  private           System.Single                   <t>5__2  // 0x38
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60005DE  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60005DF  System.Void System.IDisposable.Dispose()
  RVA=0x0A9385E4  token=0x60005E0  System.Boolean MoveNext()
  RVA=0x0A9389BC  token=0x60005E2  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <Invoke>d__4
TYPE:  sealed class
TOKEN: 0x2000170
SIZE:  0x50
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            FlowCanvas.Nodes.TweenVector3   <>4__this  // 0x20
  public            ParadoxNotion.Animation.EaseTypeinterpolation  // 0x28
  public            UnityEngine.Vector3             from  // 0x2c
  public            UnityEngine.Vector3             to  // 0x38
  public            System.Single                   duration  // 0x44
  private           System.Single                   <t>5__2  // 0x48
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60005E8  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60005E9  System.Void System.IDisposable.Dispose()
  RVA=0x0A938834  token=0x60005EA  System.Boolean MoveNext()
  RVA=0x0A938924  token=0x60005EC  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <Invoke>d__4
TYPE:  sealed class
TOKEN: 0x2000172
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            FlowCanvas.Nodes.TweenQuaternion<>4__this  // 0x20
  public            ParadoxNotion.Animation.EaseTypeinterpolation  // 0x28
  public            UnityEngine.Quaternion          from  // 0x2c
  public            UnityEngine.Quaternion          to  // 0x3c
  public            System.Single                   duration  // 0x4c
  private           System.Single                   <t>5__2  // 0x50
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60005F2  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60005F3  System.Void System.IDisposable.Dispose()
  RVA=0x0A93868C  token=0x60005F4  System.Boolean MoveNext()
  RVA=0x0A938A08  token=0x60005F6  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <Invoke>d__4
TYPE:  sealed class
TOKEN: 0x2000174
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            FlowCanvas.Nodes.TweenColor     <>4__this  // 0x20
  public            ParadoxNotion.Animation.EaseTypeinterpolation  // 0x28
  public            UnityEngine.Color               from  // 0x2c
  public            UnityEngine.Color               to  // 0x3c
  public            System.Single                   duration  // 0x4c
  private           System.Single                   <t>5__2  // 0x50
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60005FC  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60005FD  System.Void System.IDisposable.Dispose()
  RVA=0x0A938760  token=0x60005FE  System.Boolean MoveNext()
  RVA=0x0A938970  token=0x6000600  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <Invoke>d__0
TYPE:  sealed class
TOKEN: 0x2000176
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            UnityEngine.Vector3             position  // 0x20
  public            System.Boolean                  relative  // 0x2c
  public            UnityEngine.Transform           transform  // 0x30
  public            FlowCanvas.Nodes.TweenPosition  <>4__this  // 0x38
  public            ParadoxNotion.Animation.EaseTypeinterpolation  // 0x40
  public            System.Single                   duration  // 0x44
  private           System.Single                   <t>5__2  // 0x48
  private           UnityEngine.Vector3             <from>5__3  // 0x4c
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000604  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000605  System.Void System.IDisposable.Dispose()
  RVA=0x0A937C48  token=0x6000606  System.Boolean MoveNext()
  RVA=0x0A9380EC  token=0x6000608  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <Invoke>d__0
TYPE:  sealed class
TOKEN: 0x2000178
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            UnityEngine.Vector3             rotation  // 0x20
  public            System.Boolean                  relative  // 0x2c
  public            UnityEngine.Transform           transform  // 0x30
  public            FlowCanvas.Nodes.TweenRotation  <>4__this  // 0x38
  public            ParadoxNotion.Animation.EaseTypeinterpolation  // 0x40
  public            System.Single                   duration  // 0x44
  private           System.Single                   <t>5__2  // 0x48
  private           UnityEngine.Vector3             <from>5__3  // 0x4c
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600060C  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600060D  System.Void System.IDisposable.Dispose()
  RVA=0x0A937A24  token=0x600060E  System.Boolean MoveNext()
  RVA=0x0A938138  token=0x6000610  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <Invoke>d__0
TYPE:  sealed class
TOKEN: 0x200017A
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            UnityEngine.Vector3             scale  // 0x20
  public            System.Boolean                  relative  // 0x2c
  public            UnityEngine.Transform           transform  // 0x30
  public            FlowCanvas.Nodes.TweenScale     <>4__this  // 0x38
  public            ParadoxNotion.Animation.EaseTypeinterpolation  // 0x40
  public            System.Single                   duration  // 0x44
  private           System.Single                   <t>5__2  // 0x48
  private           UnityEngine.Vector3             <from>5__3  // 0x4c
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000614  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000615  System.Void System.IDisposable.Dispose()
  RVA=0x0A937E74  token=0x6000616  System.Boolean MoveNext()
  RVA=0x0A9380A0  token=0x6000618  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: InvocationMode
TYPE:  sealed struct
TOKEN: 0x200018C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  FlowCanvas.Nodes.LatentActionNodeBase.InvocationModeQueueCalls  // const
  public    static  FlowCanvas.Nodes.LatentActionNodeBase.InvocationModeFilterCalls  // const
METHODS:
END_CLASS

CLASS: RoutineData
TYPE:  sealed struct
TOKEN: 0x200018D
SIZE:  0x40
FIELDS:
  public            System.Collections.IEnumerator  enumerator  // 0x10
  public            FlowCanvas.Flow                 flow  // 0x18
METHODS:
  RVA=0x047D81D0  token=0x6000649  System.Void .ctor(System.Collections.IEnumerator enumerator, FlowCanvas.Flow flow)
END_CLASS

CLASS: <InternalCoroutine>d__15
TYPE:  sealed class
TOKEN: 0x200018E
SIZE:  0x80
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            FlowCanvas.Nodes.LatentActionNodeBase.RoutineDatadata  // 0x20
  public            FlowCanvas.Nodes.LatentActionNodeBase<>4__this  // 0x50
  private           FlowCanvas.Flow                 <f>5__2  // 0x58
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600064A  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600064B  System.Void System.IDisposable.Dispose()
  RVA=0x0A9376A8  token=0x600064C  System.Boolean MoveNext()
  RVA=0x0A9379D8  token=0x600064E  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x2000191
FIELDS:
  public            FlowCanvas.Nodes.LatentActionNode<T1><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000657  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000658  System.Void <OnRegisterDerivedPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x2000193
FIELDS:
  public            FlowCanvas.Nodes.LatentActionNode<T1,T2><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600065C  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600065D  System.Void <OnRegisterDerivedPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x2000195
FIELDS:
  public            FlowCanvas.Nodes.LatentActionNode<T1,T2,T3><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000661  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000662  System.Void <OnRegisterDerivedPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x2000197
FIELDS:
  public            FlowCanvas.Nodes.LatentActionNode<T1,T2,T3,T4><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000666  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000667  System.Void <OnRegisterDerivedPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x2000199
FIELDS:
  public            FlowCanvas.Nodes.LatentActionNode<T1,T2,T3,T4,T5><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600066B  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600066C  System.Void <OnRegisterDerivedPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x200019B
FIELDS:
  public            FlowCanvas.Nodes.LatentActionNode<T1,T2,T3,T4,T5,T6><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000670  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000671  System.Void <OnRegisterDerivedPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x200019D
FIELDS:
  public            FlowCanvas.Nodes.LatentActionNode<T1,T2,T3,T4,T5,T6,T7><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000675  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000676  System.Void <OnRegisterDerivedPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x200019F
FIELDS:
  public            FlowCanvas.Nodes.LatentActionNode<T1,T2,T3,T4,T5,T6,T7,T8><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
  public            FlowCanvas.ValueInput<T8>       p8  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600067A  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600067B  System.Void <OnRegisterDerivedPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20001A1
FIELDS:
  public            FlowCanvas.Nodes.LatentActionNode<T1,T2,T3,T4,T5,T6,T7,T8,T9><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
  public            FlowCanvas.ValueInput<T8>       p8  // 0x0
  public            FlowCanvas.ValueInput<T9>       p9  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600067F  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000680  System.Void <OnRegisterDerivedPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20001A3
FIELDS:
  public            FlowCanvas.Nodes.LatentActionNode<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
  public            FlowCanvas.ValueInput<T8>       p8  // 0x0
  public            FlowCanvas.ValueInput<T9>       p9  // 0x0
  public            FlowCanvas.ValueInput<T10>      p10  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000684  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000685  System.Void <OnRegisterDerivedPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20001A7
FIELDS:
  public            FlowCanvas.Nodes.PureFunctionNode<TResult,T1><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600068E  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600068F  TResult <OnRegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20001A9
FIELDS:
  public            FlowCanvas.Nodes.PureFunctionNode<TResult,T1,T2><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000693  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000694  TResult <OnRegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20001AB
FIELDS:
  public            FlowCanvas.Nodes.PureFunctionNode<TResult,T1,T2,T3><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000698  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000699  TResult <OnRegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20001AD
FIELDS:
  public            FlowCanvas.Nodes.PureFunctionNode<TResult,T1,T2,T3,T4><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600069D  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600069E  TResult <OnRegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20001AF
FIELDS:
  public            FlowCanvas.Nodes.PureFunctionNode<TResult,T1,T2,T3,T4,T5><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60006A2  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60006A3  TResult <OnRegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20001B1
FIELDS:
  public            FlowCanvas.Nodes.PureFunctionNode<TResult,T1,T2,T3,T4,T5,T6><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60006A7  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60006A8  TResult <OnRegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20001B3
FIELDS:
  public            FlowCanvas.Nodes.PureFunctionNode<TResult,T1,T2,T3,T4,T5,T6,T7><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60006AC  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60006AD  TResult <OnRegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20001B5
FIELDS:
  public            FlowCanvas.Nodes.PureFunctionNode<TResult,T1,T2,T3,T4,T5,T6,T7,T8><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
  public            FlowCanvas.ValueInput<T8>       p8  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60006B1  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60006B2  TResult <OnRegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20001B7
FIELDS:
  public            FlowCanvas.Nodes.PureFunctionNode<TResult,T1,T2,T3,T4,T5,T6,T7,T8,T9><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
  public            FlowCanvas.ValueInput<T8>       p8  // 0x0
  public            FlowCanvas.ValueInput<T9>       p9  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60006B6  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60006B7  TResult <OnRegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20001B9
FIELDS:
  public            FlowCanvas.Nodes.PureFunctionNode<TResult,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
  public            FlowCanvas.ValueInput<T8>       p8  // 0x0
  public            FlowCanvas.ValueInput<T9>       p9  // 0x0
  public            FlowCanvas.ValueInput<T10>      p10  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60006BB  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60006BC  TResult <OnRegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass6_0
TYPE:  sealed class
TOKEN: 0x20001D0
FIELDS:
  public            System.Delegate                 beforeReturn  // 0x0
  public            FlowCanvas.Nodes.UniversalDelegateParam<T><>4__this  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000724  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000725  T <RegisterAsOutputInternal>b__0()
END_CLASS

CLASS: <>c__DisplayClass8_0
TYPE:  sealed class
TOKEN: 0x20001D4
SIZE:  0x28
FIELDS:
  public            System.Boolean                  callable  // 0x10
  public            FlowCanvas.Nodes.PureReflectionConstructorNode<>4__this  // 0x18
  public            System.Int32                    idx  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000736  System.Void .ctor()
  RVA=0x0A93A8B4  token=0x6000737  System.Object <RegisterOutput>b__0()
END_CLASS

CLASS: <>c__DisplayClass10_0
TYPE:  sealed class
TOKEN: 0x20001D5
SIZE:  0x20
FIELDS:
  public            FlowCanvas.Nodes.PureReflectionConstructorNode<>4__this  // 0x10
  public            FlowCanvas.Nodes.ReflectedMethodRegistrationOptionsoptions  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000738  System.Void .ctor()
  RVA=0x0A93906C  token=0x6000739  System.Object <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass10_1
TYPE:  sealed class
TOKEN: 0x20001D6
SIZE:  0x20
FIELDS:
  public            FlowCanvas.FlowOutput           output  // 0x10
  public            FlowCanvas.Nodes.PureReflectionConstructorNode.<>c__DisplayClass10_0CS$<>8__locals1  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600073A  System.Void .ctor()
  RVA=0x0A9390A8  token=0x600073B  System.Void <RegisterPorts>b__1(FlowCanvas.Flow flow)
END_CLASS

CLASS: DelegateEventCallback
TYPE:  sealed class
TOKEN: 0x20001D8
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x074FF394  token=0x6000750  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000751  System.Void Invoke(System.Object[] args)
  RVA=0x047FDD70  token=0x6000752  System.IAsyncResult BeginInvoke(System.Object[] args, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000753  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20001D9
SIZE:  0x10
FIELDS:
  public    static readonly FlowCanvas.Nodes.ReflectedDelegateEvent.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Reflection.ParameterInfo,System.Type><>9__10_0  // static @ 0x8
METHODS:
  RVA=0x0A93A9D0  token=0x6000754  System.Void .cctor()
  RVA=0x041E1670  token=0x6000755  System.Void .ctor()
  RVA=0x06C08908  token=0x6000756  System.Type <GetMethodForDelegateType>b__10_0(System.Reflection.ParameterInfo p)
END_CLASS

CLASS: UnityEventCallback
TYPE:  sealed class
TOKEN: 0x20001DB
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x074FF394  token=0x6000765  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000766  System.Void Invoke(System.Object[] args)
  RVA=0x047FDD70  token=0x6000767  System.IAsyncResult BeginInvoke(System.Object[] args, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000768  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20001DC
SIZE:  0x10
FIELDS:
  public    static readonly FlowCanvas.Nodes.ReflectedUnityEvent.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Reflection.ParameterInfo,System.Type><>9__15_0  // static @ 0x8
METHODS:
  RVA=0x0A93AA34  token=0x6000769  System.Void .cctor()
  RVA=0x041E1670  token=0x600076A  System.Void .ctor()
  RVA=0x06C08908  token=0x600076B  System.Type <InitForEventType>b__15_0(System.Reflection.ParameterInfo p)
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x20001E1
SIZE:  0x20
FIELDS:
  public            FlowCanvas.Nodes.PureReflectedExtractorNode<>4__this  // 0x10
  public            System.Reflection.FieldInfo     info  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000787  System.Void .ctor()
  RVA=0x0A939AC8  token=0x6000788  System.Object <GetPortHandler>b__0()
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x20001E2
SIZE:  0x20
FIELDS:
  public            FlowCanvas.Nodes.PureReflectedExtractorNode<>4__this  // 0x10
  public            System.Reflection.MethodInfo    info  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000789  System.Void .ctor()
  RVA=0x0A939CA4  token=0x600078A  System.Object <GetPortHandler>b__0()
END_CLASS

CLASS: <>c__DisplayClass7_0
TYPE:  sealed class
TOKEN: 0x20001E5
SIZE:  0x20
FIELDS:
  public            FlowCanvas.FlowOutput           output  // 0x10
  public            FlowCanvas.Nodes.PureReflectedFieldNode<>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000799  System.Void .ctor()
  RVA=0x0A93A830  token=0x600079A  System.Void <RegisterPorts>b__2(FlowCanvas.Flow flow)
END_CLASS

CLASS: <>c__DisplayClass10_0
TYPE:  sealed class
TOKEN: 0x20001E8
SIZE:  0x28
FIELDS:
  public            System.Boolean                  callable  // 0x10
  public            FlowCanvas.Nodes.PureReflectedMethodNode<>4__this  // 0x18
  public            System.Int32                    idx  // 0x20
METHODS:
  RVA=0x041E1670  token=0x60007A8  System.Void .ctor()
  RVA=0x0A939018  token=0x60007A9  System.Object <RegisterOutput>b__0()
END_CLASS

CLASS: <>c__DisplayClass12_0
TYPE:  sealed class
TOKEN: 0x20001E9
SIZE:  0x20
FIELDS:
  public            FlowCanvas.Nodes.PureReflectedMethodNode<>4__this  // 0x10
  public            FlowCanvas.Nodes.ReflectedMethodRegistrationOptionsoptions  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60007AA  System.Void .ctor()
  RVA=0x0A939114  token=0x60007AB  System.Object <RegisterPorts>b__0()
  RVA=0x0A939134  token=0x60007AC  System.Object <RegisterPorts>b__1()
END_CLASS

CLASS: <>c__DisplayClass12_1
TYPE:  sealed class
TOKEN: 0x20001EA
SIZE:  0x20
FIELDS:
  public            FlowCanvas.FlowOutput           output  // 0x10
  public            FlowCanvas.Nodes.PureReflectedMethodNode.<>c__DisplayClass12_0CS$<>8__locals1  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60007AD  System.Void .ctor()
  RVA=0x0A939170  token=0x60007AE  System.Void <RegisterPorts>b__2(FlowCanvas.Flow flow)
END_CLASS

CLASS: AccessMode
TYPE:  sealed struct
TOKEN: 0x20001EF
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  FlowCanvas.Nodes.ReflectedFieldNodeWrapper.AccessModeGetField  // const
  public    static  FlowCanvas.Nodes.ReflectedFieldNodeWrapper.AccessModeSetField  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass16_0
TYPE:  sealed class
TOKEN: 0x20001F4
FIELDS:
  public            FlowCanvas.Nodes.FlowNestedBase<T><>4__this  // 0x0
  public            FlowCanvas.Flow                 f  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60007F0  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60007F1  System.Void <Start>b__0(System.Boolean result)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20001F7
FIELDS:
  public            System.Collections.Generic.List<FlowCanvas.ValueInput<T>>ins  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60007F5  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60007F6  T[] <RegisterPorts>b__0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20001F8
FIELDS:
  public    static readonly FlowCanvas.Nodes.CreateCollection.<>c<T><>9  // static @ 0x0
  public    static  System.Func<FlowCanvas.ValueInput<T>,T><>9__1_1  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60007F7  System.Void .cctor()
  RVA=-1  // not resolved  token=0x60007F8  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60007F9  T <RegisterPorts>b__1_1(FlowCanvas.ValueInput<T> p)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x20001FA
FIELDS:
  public            System.Collections.Generic.List<FlowCanvas.ValueInput<System.String>>keys  // 0x0
  public            System.Collections.Generic.List<FlowCanvas.ValueInput<T>>values  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60007FC  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60007FD  System.Collections.Generic.IDictionary<System.String,T> <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass1_1
TYPE:  sealed class
TOKEN: 0x20001FB
FIELDS:
  public            System.Collections.Generic.List<T>v  // 0x0
  public            System.Collections.Generic.List<System.String>k  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60007FE  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60007FF  T <RegisterPorts>b__4(System.String x)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20001FC
FIELDS:
  public    static readonly FlowCanvas.Nodes.CreateDictionary.<>c<T><>9  // static @ 0x0
  public    static  System.Func<FlowCanvas.ValueInput<System.String>,System.String><>9__1_1  // static @ 0x0
  public    static  System.Func<FlowCanvas.ValueInput<T>,T><>9__1_2  // static @ 0x0
  public    static  System.Func<System.String,System.String><>9__1_3  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000800  System.Void .cctor()
  RVA=-1  // not resolved  token=0x6000801  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000802  System.String <RegisterPorts>b__1_1(FlowCanvas.ValueInput<System.String> x)
  RVA=-1  // not resolved  token=0x6000803  T <RegisterPorts>b__1_2(FlowCanvas.ValueInput<T> x)
  RVA=-1  // not resolved  token=0x6000804  System.String <RegisterPorts>b__1_3(System.String x)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x20001FE
FIELDS:
  public            FlowCanvas.ValueInput<NodeCanvas.Framework.IBlackboard>bb  // 0x0
  public            FlowCanvas.ValueInput<System.String>varName  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000808  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000809  T <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass17_0
TYPE:  sealed class
TOKEN: 0x2000202
FIELDS:
  public            FlowCanvas.Nodes.RelayValueInput<T><>4__this  // 0x0
  public            FlowCanvas.FlowOutput           fOut  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600081A  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600081B  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000206
SIZE:  0x10
FIELDS:
  public    static readonly FlowCanvas.Nodes.NullObject.<>c <>9  // static @ 0x0
  public    static  FlowCanvas.ValueHandler<System.Object><>9__0_0  // static @ 0x8
METHODS:
  RVA=0x0A952DC4  token=0x6000829  System.Void .cctor()
  RVA=0x041E1670  token=0x600082A  System.Void .ctor()
  RVA=0x011EC580  token=0x600082B  System.Object <RegisterPorts>b__0_0()
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x200020A
FIELDS:
  public            FlowCanvas.ValueInput<NodeCanvas.Framework.Blackboard>bb  // 0x0
  public            FlowCanvas.Nodes.SetOtherVariable<T><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T>        v  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000836  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000837  T <RegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x6000838  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass7_0
TYPE:  sealed class
TOKEN: 0x200020C
FIELDS:
  public            FlowCanvas.Nodes.SetVariable<T> <>4__this  // 0x0
  public            FlowCanvas.ValueInput<T>        v  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600083E  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600083F  T <RegisterPorts>b__0()
  RVA=-1  // not resolved  token=0x6000840  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <DoUpdate>d__15
TYPE:  sealed class
TOKEN: 0x2000238
SIZE:  0x50
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            FlowCanvas.Nodes.TaskAction     <>4__this  // 0x20
  public            FlowCanvas.Flow                 f  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60008BA  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60008BB  System.Void System.IDisposable.Dispose()
  RVA=0x0A952554  token=0x60008BC  System.Boolean MoveNext()
  RVA=0x0A952690  token=0x60008BE  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass0_0
TYPE:  sealed class
TOKEN: 0x200023C
SIZE:  0x18
FIELDS:
  public            System.Reflection.FieldInfo     field  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60008CF  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass0_1
TYPE:  sealed class
TOKEN: 0x200023D
SIZE:  0x18
FIELDS:
  public            System.Object                   constantValue  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60008D0  System.Void .ctor()
  RVA=0x02B2ECC0  token=0x60008D1  System.Object <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass0_2
TYPE:  sealed class
TOKEN: 0x200023E
SIZE:  0x20
FIELDS:
  public            FlowCanvas.ValueInput           instanceInput  // 0x10
  public            FlowCanvas.Nodes.Legacy.PureReflectedFieldNode.<>c__DisplayClass0_0CS$<>8__locals1  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60008D2  System.Void .ctor()
  RVA=0x0A9526F0  token=0x60008D3  System.Object <RegisterPorts>b__1()
END_CLASS

CLASS: <>c__DisplayClass0_3
TYPE:  sealed class
TOKEN: 0x200023F
SIZE:  0x38
FIELDS:
  public            System.Object                   instance  // 0x10
  public            FlowCanvas.ValueInput           instanceInput  // 0x18
  public            FlowCanvas.ValueInput           valueInput  // 0x20
  public            FlowCanvas.FlowOutput           flowOut  // 0x28
  public            FlowCanvas.Nodes.Legacy.PureReflectedFieldNode.<>c__DisplayClass0_0CS$<>8__locals2  // 0x30
METHODS:
  RVA=0x041E1670  token=0x60008D4  System.Void .ctor()
  RVA=0x02B2ECC0  token=0x60008D5  System.Object <RegisterPorts>b__2()
  RVA=0x0A952734  token=0x60008D6  System.Void <RegisterPorts>b__3(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x2000241
SIZE:  0x18
FIELDS:
  public            System.Reflection.ParameterInfo[]parameters  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60008DC  System.Void .ctor()
  RVA=0x0A9527F4  token=0x60008DD  System.Boolean <Create>b__0(System.Reflection.ParameterInfo p)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000242
SIZE:  0x10
FIELDS:
  public    static readonly FlowCanvas.Nodes.Legacy.ReflectedMethodNode.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Reflection.ParameterInfo,System.Type><>9__2_0  // static @ 0x8
  public    static  System.Func<System.Reflection.ParameterInfo,System.Type><>9__2_1  // static @ 0x10
METHODS:
  RVA=0x0A952D60  token=0x60008DE  System.Void .cctor()
  RVA=0x041E1670  token=0x60008DF  System.Void .ctor()
  RVA=0x06C08908  token=0x60008E0  System.Type <TryCreateJit>b__2_0(System.Reflection.ParameterInfo p)
  RVA=0x06C08908  token=0x60008E1  System.Type <TryCreateJit>b__2_1(System.Reflection.ParameterInfo p)
END_CLASS

CLASS: <>c__DisplayClass8_0
TYPE:  sealed class
TOKEN: 0x2000244
SIZE:  0x20
FIELDS:
  public            FlowCanvas.Nodes.Legacy.PureReflectedMethodNode<>4__this  // 0x10
  public            FlowCanvas.Nodes.ReflectedMethodRegistrationOptionsoptions  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60008E5  System.Void .ctor()
  RVA=0x0A952C3C  token=0x60008E6  System.Object <RegisterPorts>b__0()
  RVA=0x0A952C58  token=0x60008E7  System.Object <RegisterPorts>b__1()
END_CLASS

CLASS: <>c__DisplayClass8_1
TYPE:  sealed class
TOKEN: 0x2000245
SIZE:  0x20
FIELDS:
  public            FlowCanvas.FlowOutput           o  // 0x10
  public            FlowCanvas.Nodes.Legacy.PureReflectedMethodNode.<>c__DisplayClass8_0CS$<>8__locals1  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60008E8  System.Void .ctor()
  RVA=0x0A952C90  token=0x60008E9  System.Void <RegisterPorts>b__2(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass8_2
TYPE:  sealed class
TOKEN: 0x2000246
SIZE:  0x20
FIELDS:
  public            System.Int32                    i  // 0x10
  public            FlowCanvas.Nodes.Legacy.PureReflectedMethodNode.<>c__DisplayClass8_0CS$<>8__locals2  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60008EA  System.Void .ctor()
  RVA=0x0A952CFC  token=0x60008EB  System.Object <RegisterPorts>b__3()
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x2000248
SIZE:  0x20
FIELDS:
  public            FlowCanvas.Nodes.Legacy.ReflectedActionNode<>4__this  // 0x10
  public            FlowCanvas.FlowOutput           o  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60008EF  System.Void .ctor()
  RVA=0x0A952A90  token=0x60008F0  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x200024A
FIELDS:
  public            FlowCanvas.Nodes.Legacy.ReflectedActionNode<T1><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60008F4  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60008F5  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
  RVA=-1  // not resolved  token=0x60008F6  T1 <RegisterPorts>b__1()
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x200024C
FIELDS:
  public            FlowCanvas.Nodes.Legacy.ReflectedActionNode<T1,T2><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60008FA  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60008FB  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
  RVA=-1  // not resolved  token=0x60008FC  T1 <RegisterPorts>b__1()
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x200024E
FIELDS:
  public            FlowCanvas.Nodes.Legacy.ReflectedActionNode<T1,T2,T3><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000900  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000901  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
  RVA=-1  // not resolved  token=0x6000902  T1 <RegisterPorts>b__1()
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x2000250
FIELDS:
  public            FlowCanvas.Nodes.Legacy.ReflectedActionNode<T1,T2,T3,T4><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000906  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000907  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
  RVA=-1  // not resolved  token=0x6000908  T1 <RegisterPorts>b__1()
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x2000252
FIELDS:
  public            FlowCanvas.Nodes.Legacy.ReflectedActionNode<T1,T2,T3,T4,T5><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600090C  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600090D  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
  RVA=-1  // not resolved  token=0x600090E  T1 <RegisterPorts>b__1()
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x2000254
FIELDS:
  public            FlowCanvas.Nodes.Legacy.ReflectedActionNode<T1,T2,T3,T4,T5,T6><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000912  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000913  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
  RVA=-1  // not resolved  token=0x6000914  T1 <RegisterPorts>b__1()
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x2000256
FIELDS:
  public            FlowCanvas.Nodes.Legacy.ReflectedActionNode<T1,T2,T3,T4,T5,T6,T7><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000918  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000919  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
  RVA=-1  // not resolved  token=0x600091A  T1 <RegisterPorts>b__1()
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x2000258
FIELDS:
  public            FlowCanvas.Nodes.Legacy.ReflectedActionNode<T1,T2,T3,T4,T5,T6,T7,T8><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
  public            FlowCanvas.ValueInput<T8>       p8  // 0x0
  public            FlowCanvas.FlowOutput           o  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600091E  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600091F  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
  RVA=-1  // not resolved  token=0x6000920  T1 <RegisterPorts>b__1()
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x200025A
FIELDS:
  public            FlowCanvas.Nodes.Legacy.ReflectedFunctionNode<TResult><>4__this  // 0x0
  public            FlowCanvas.Nodes.ReflectedMethodRegistrationOptionsoptions  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000924  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000925  TResult <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass3_1
TYPE:  sealed class
TOKEN: 0x200025B
FIELDS:
  public            FlowCanvas.FlowOutput           o  // 0x0
  public            FlowCanvas.Nodes.Legacy.ReflectedFunctionNode.<>c__DisplayClass3_0<TResult>CS$<>8__locals1  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000926  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000927  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x200025D
FIELDS:
  public            FlowCanvas.Nodes.Legacy.ReflectedFunctionNode<T1,TResult><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.Nodes.ReflectedMethodRegistrationOptionsoptions  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600092B  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600092C  T1 <RegisterPorts>b__2()
  RVA=-1  // not resolved  token=0x600092D  TResult <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass4_1
TYPE:  sealed class
TOKEN: 0x200025E
FIELDS:
  public            FlowCanvas.FlowOutput           o  // 0x0
  public            FlowCanvas.Nodes.Legacy.ReflectedFunctionNode.<>c__DisplayClass4_0<T1,TResult>CS$<>8__locals1  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600092E  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600092F  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x2000260
FIELDS:
  public            FlowCanvas.Nodes.Legacy.ReflectedFunctionNode<T1,T2,TResult><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.Nodes.ReflectedMethodRegistrationOptionsoptions  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000933  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000934  T1 <RegisterPorts>b__2()
  RVA=-1  // not resolved  token=0x6000935  TResult <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass4_1
TYPE:  sealed class
TOKEN: 0x2000261
FIELDS:
  public            FlowCanvas.FlowOutput           o  // 0x0
  public            FlowCanvas.Nodes.Legacy.ReflectedFunctionNode.<>c__DisplayClass4_0<T1,T2,TResult>CS$<>8__locals1  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000936  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000937  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x2000263
FIELDS:
  public            FlowCanvas.Nodes.Legacy.ReflectedFunctionNode<T1,T2,T3,TResult><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.Nodes.ReflectedMethodRegistrationOptionsoptions  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600093B  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600093C  T1 <RegisterPorts>b__2()
  RVA=-1  // not resolved  token=0x600093D  TResult <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass4_1
TYPE:  sealed class
TOKEN: 0x2000264
FIELDS:
  public            FlowCanvas.FlowOutput           o  // 0x0
  public            FlowCanvas.Nodes.Legacy.ReflectedFunctionNode.<>c__DisplayClass4_0<T1,T2,T3,TResult>CS$<>8__locals1  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600093E  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600093F  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x2000266
FIELDS:
  public            FlowCanvas.Nodes.Legacy.ReflectedFunctionNode<T1,T2,T3,T4,TResult><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.Nodes.ReflectedMethodRegistrationOptionsoptions  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000943  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000944  T1 <RegisterPorts>b__2()
  RVA=-1  // not resolved  token=0x6000945  TResult <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass4_1
TYPE:  sealed class
TOKEN: 0x2000267
FIELDS:
  public            FlowCanvas.FlowOutput           o  // 0x0
  public            FlowCanvas.Nodes.Legacy.ReflectedFunctionNode.<>c__DisplayClass4_0<T1,T2,T3,T4,TResult>CS$<>8__locals1  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000946  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000947  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x2000269
FIELDS:
  public            FlowCanvas.Nodes.Legacy.ReflectedFunctionNode<T1,T2,T3,T4,T5,TResult><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.Nodes.ReflectedMethodRegistrationOptionsoptions  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600094B  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600094C  T1 <RegisterPorts>b__2()
  RVA=-1  // not resolved  token=0x600094D  TResult <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass4_1
TYPE:  sealed class
TOKEN: 0x200026A
FIELDS:
  public            FlowCanvas.FlowOutput           o  // 0x0
  public            FlowCanvas.Nodes.Legacy.ReflectedFunctionNode.<>c__DisplayClass4_0<T1,T2,T3,T4,T5,TResult>CS$<>8__locals1  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600094E  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600094F  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x200026C
FIELDS:
  public            FlowCanvas.Nodes.Legacy.ReflectedFunctionNode<T1,T2,T3,T4,T5,T6,TResult><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.Nodes.ReflectedMethodRegistrationOptionsoptions  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000953  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000954  T1 <RegisterPorts>b__2()
  RVA=-1  // not resolved  token=0x6000955  TResult <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass4_1
TYPE:  sealed class
TOKEN: 0x200026D
FIELDS:
  public            FlowCanvas.FlowOutput           o  // 0x0
  public            FlowCanvas.Nodes.Legacy.ReflectedFunctionNode.<>c__DisplayClass4_0<T1,T2,T3,T4,T5,T6,TResult>CS$<>8__locals1  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000956  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000957  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x200026F
FIELDS:
  public            FlowCanvas.Nodes.Legacy.ReflectedFunctionNode<T1,T2,T3,T4,T5,T6,T7,TResult><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
  public            FlowCanvas.Nodes.ReflectedMethodRegistrationOptionsoptions  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600095B  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600095C  T1 <RegisterPorts>b__2()
  RVA=-1  // not resolved  token=0x600095D  TResult <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass4_1
TYPE:  sealed class
TOKEN: 0x2000270
FIELDS:
  public            FlowCanvas.FlowOutput           o  // 0x0
  public            FlowCanvas.Nodes.Legacy.ReflectedFunctionNode.<>c__DisplayClass4_0<T1,T2,T3,T4,T5,T6,T7,TResult>CS$<>8__locals1  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600095E  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600095F  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x2000272
FIELDS:
  public            FlowCanvas.Nodes.Legacy.ReflectedFunctionNode<T1,T2,T3,T4,T5,T6,T7,T8,TResult><>4__this  // 0x0
  public            FlowCanvas.ValueInput<T1>       p1  // 0x0
  public            FlowCanvas.ValueInput<T2>       p2  // 0x0
  public            FlowCanvas.ValueInput<T3>       p3  // 0x0
  public            FlowCanvas.ValueInput<T4>       p4  // 0x0
  public            FlowCanvas.ValueInput<T5>       p5  // 0x0
  public            FlowCanvas.ValueInput<T6>       p6  // 0x0
  public            FlowCanvas.ValueInput<T7>       p7  // 0x0
  public            FlowCanvas.ValueInput<T8>       p8  // 0x0
  public            FlowCanvas.Nodes.ReflectedMethodRegistrationOptionsoptions  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000963  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000964  T1 <RegisterPorts>b__2()
  RVA=-1  // not resolved  token=0x6000965  TResult <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass4_1
TYPE:  sealed class
TOKEN: 0x2000273
FIELDS:
  public            FlowCanvas.FlowOutput           o  // 0x0
  public            FlowCanvas.Nodes.Legacy.ReflectedFunctionNode.<>c__DisplayClass4_0<T1,T2,T3,T4,T5,T6,T7,T8,TResult>CS$<>8__locals1  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000966  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000967  System.Void <RegisterPorts>b__1(FlowCanvas.Flow f)
END_CLASS

CLASS: DerivedSerializationData
TYPE:  class
TOKEN: 0x2000275
SIZE:  0x20
FIELDS:
  public            System.Collections.Generic.List<ParadoxNotion.DynamicParameterDefinition>inputDefinitions  // 0x10
  public            System.Collections.Generic.List<ParadoxNotion.DynamicParameterDefinition>outputDefinitions  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000976  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass20_0
TYPE:  sealed class
TOKEN: 0x2000276
SIZE:  0x18
FIELDS:
  public            ParadoxNotion.DynamicParameterDefinitiondef  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000977  System.Void .ctor()
  RVA=0x0A95286C  token=0x6000978  System.Boolean <AddInputDefinition>b__0(ParadoxNotion.DynamicParameterDefinition d)
END_CLASS

CLASS: <>c__DisplayClass21_0
TYPE:  sealed class
TOKEN: 0x2000277
SIZE:  0x18
FIELDS:
  public            ParadoxNotion.DynamicParameterDefinitiondef  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000979  System.Void .ctor()
  RVA=0x0A95286C  token=0x600097A  System.Boolean <AddOutputDefinition>b__0(ParadoxNotion.DynamicParameterDefinition d)
END_CLASS

CLASS: <>c__DisplayClass23_0`1
TYPE:  sealed class
TOKEN: 0x2000278
FIELDS:
  public            System.String                   name  // 0x0
  public            T                               value  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600097B  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600097C  System.Boolean <SetValueInput>b__0(ParadoxNotion.DynamicParameterDefinition d)
  RVA=-1  // not resolved  token=0x600097D  System.Object <SetValueInput>b__1()
END_CLASS

CLASS: <>c__DisplayClass24_0
TYPE:  sealed class
TOKEN: 0x2000279
SIZE:  0x18
FIELDS:
  public            System.String                   name  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600097E  System.Void .ctor()
  RVA=0x0A9528BC  token=0x600097F  System.Boolean <CallFlowInput>b__0(ParadoxNotion.DynamicParameterDefinition d)
END_CLASS

CLASS: <>c__DisplayClass25_0`1
TYPE:  sealed class
TOKEN: 0x200027A
FIELDS:
  public            System.String                   name  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000980  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000981  System.Boolean <GetValueOutput>b__0(ParadoxNotion.DynamicParameterDefinition d)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x200027C
SIZE:  0x20
FIELDS:
  public            ParadoxNotion.DynamicParameterDefinitiondef  // 0x10
  public            FlowCanvas.Macros.MacroInputNode<>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000986  System.Void .ctor()
  RVA=0x0A952AF4  token=0x6000987  System.Object <RegisterPorts>b__0()
END_CLASS

CLASS: <>c__DisplayClass25_0
TYPE:  sealed class
TOKEN: 0x200027E
SIZE:  0x18
FIELDS:
  public            FlowCanvas.Macros.Macro         target  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000999  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass25_1
TYPE:  sealed class
TOKEN: 0x200027F
SIZE:  0x20
FIELDS:
  public            ParadoxNotion.DynamicParameterDefinitiondefIn  // 0x10
  public            FlowCanvas.Macros.MacroNodeWrapper.<>c__DisplayClass25_0CS$<>8__locals1  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600099A  System.Void .ctor()
  RVA=0x0A952948  token=0x600099B  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c__DisplayClass25_2
TYPE:  sealed class
TOKEN: 0x2000280
SIZE:  0x20
FIELDS:
  public            ParadoxNotion.DynamicParameterDefinitiondefOut  // 0x10
  public            FlowCanvas.Macros.MacroNodeWrapper.<>c__DisplayClass25_0CS$<>8__locals2  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600099C  System.Void .ctor()
  RVA=0x0A952A00  token=0x600099D  System.Object <RegisterPorts>b__1()
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x2000282
SIZE:  0x20
FIELDS:
  public            ParadoxNotion.DynamicParameterDefinitiondef  // 0x10
  public            FlowCanvas.Macros.MacroOutputNode<>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60009A2  System.Void .ctor()
  RVA=0x0A952B84  token=0x60009A3  System.Void <RegisterPorts>b__0(FlowCanvas.Flow f)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20002A3
SIZE:  0x10
FIELDS:
  public    static readonly NodeCanvas.Tasks.Conditions.CheckLOS2D.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.RaycastHit2D,UnityEngine.Collider2D><>9__7_0  // static @ 0x8
METHODS:
  RVA=0x0A952E28  token=0x6000A25  System.Void .cctor()
  RVA=0x041E1670  token=0x6000A26  System.Void .ctor()
  RVA=0x0A9526DC  token=0x6000A27  UnityEngine.Collider2D <OnCheck>b__7_0(UnityEngine.RaycastHit2D h)
END_CLASS

CLASS: BoolSetModes
TYPE:  sealed struct
TOKEN: 0x20002FC
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Tasks.Actions.SetBoolean.BoolSetModesFalse  // const
  public    static  NodeCanvas.Tasks.Actions.SetBoolean.BoolSetModesTrue  // const
  public    static  NodeCanvas.Tasks.Actions.SetBoolean.BoolSetModesToggle  // const
METHODS:
END_CLASS

CLASS: <Flip>d__4
TYPE:  sealed class
TOKEN: 0x2000307
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            NodeCanvas.Tasks.Actions.TriggerBoolean<>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000BFA  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000BFB  System.Void System.IDisposable.Dispose()
  RVA=0x0A967E5C  token=0x6000BFC  System.Boolean MoveNext()
  RVA=0x0A967EDC  token=0x6000BFE  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000317
FIELDS:
  public    static readonly NodeCanvas.Tasks.Actions.FindObjectsOfType.<>c<T><>9  // static @ 0x0
  public    static  System.Func<T,UnityEngine.GameObject><>9__2_0  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000C25  System.Void .cctor()
  RVA=-1  // not resolved  token=0x6000C26  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000C27  UnityEngine.GameObject <OnExecute>b__2_0(T o)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200031B
SIZE:  0x10
FIELDS:
  public    static readonly NodeCanvas.Tasks.Actions.GetAllChildGameObjects.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.Transform,UnityEngine.GameObject><>9__4_0  // static @ 0x8
METHODS:
  RVA=0x0A9684BC  token=0x6000C32  System.Void .cctor()
  RVA=0x041E1670  token=0x6000C33  System.Void .ctor()
  RVA=0x07C183B0  token=0x6000C34  UnityEngine.GameObject <OnExecute>b__4_0(UnityEngine.Transform t)
END_CLASS

CLASS: SetActiveMode
TYPE:  sealed struct
TOKEN: 0x2000323
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Tasks.Actions.SetObjectActive.SetActiveModeDeactivate  // const
  public    static  NodeCanvas.Tasks.Actions.SetObjectActive.SetActiveModeActivate  // const
  public    static  NodeCanvas.Tasks.Actions.SetObjectActive.SetActiveModeToggle  // const
METHODS:
END_CLASS

CLASS: SetEnableMode
TYPE:  sealed struct
TOKEN: 0x2000325
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Tasks.Actions.SetObjectEnabled.SetEnableModeDisable  // const
  public    static  NodeCanvas.Tasks.Actions.SetObjectEnabled.SetEnableModeEnable  // const
  public    static  NodeCanvas.Tasks.Actions.SetObjectEnabled.SetEnableModeToggle  // const
METHODS:
END_CLASS

CLASS: SetVisibleMode
TYPE:  sealed struct
TOKEN: 0x2000327
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Tasks.Actions.SetObjectVisibility.SetVisibleModeHide  // const
  public    static  NodeCanvas.Tasks.Actions.SetObjectVisibility.SetVisibleModeShow  // const
  public    static  NodeCanvas.Tasks.Actions.SetObjectVisibility.SetVisibleModeToggle  // const
METHODS:
END_CLASS

CLASS: ButtonKeys
TYPE:  sealed struct
TOKEN: 0x200032C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Tasks.Actions.WaitMousePick.ButtonKeysLeft  // const
  public    static  NodeCanvas.Tasks.Actions.WaitMousePick.ButtonKeysRight  // const
  public    static  NodeCanvas.Tasks.Actions.WaitMousePick.ButtonKeysMiddle  // const
METHODS:
END_CLASS

CLASS: ButtonKeys
TYPE:  sealed struct
TOKEN: 0x200032E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Tasks.Actions.WaitMousePick2D.ButtonKeysLeft  // const
  public    static  NodeCanvas.Tasks.Actions.WaitMousePick2D.ButtonKeysRight  // const
  public    static  NodeCanvas.Tasks.Actions.WaitMousePick2D.ButtonKeysMiddle  // const
METHODS:
END_CLASS

CLASS: TransformMode
TYPE:  sealed struct
TOKEN: 0x2000330
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Tasks.Actions.CurveTransformTween.TransformModePosition  // const
  public    static  NodeCanvas.Tasks.Actions.CurveTransformTween.TransformModeRotation  // const
  public    static  NodeCanvas.Tasks.Actions.CurveTransformTween.TransformModeScale  // const
METHODS:
END_CLASS

CLASS: TweenMode
TYPE:  sealed struct
TOKEN: 0x2000331
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Tasks.Actions.CurveTransformTween.TweenModeAbsolute  // const
  public    static  NodeCanvas.Tasks.Actions.CurveTransformTween.TweenModeAdditive  // const
METHODS:
END_CLASS

CLASS: PlayMode
TYPE:  sealed struct
TOKEN: 0x2000332
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Tasks.Actions.CurveTransformTween.PlayModeNormal  // const
  public    static  NodeCanvas.Tasks.Actions.CurveTransformTween.PlayModePingPong  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200033C
SIZE:  0x10
FIELDS:
  public    static readonly NodeCanvas.Tasks.Actions.GetLinecastInfo2DAll.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.RaycastHit2D,UnityEngine.GameObject><>9__7_0  // static @ 0x8
  public    static  System.Func<UnityEngine.RaycastHit2D,System.Single><>9__7_1  // static @ 0x10
  public    static  System.Func<UnityEngine.RaycastHit2D,UnityEngine.Vector3><>9__7_3  // static @ 0x18
METHODS:
  RVA=0x0A968458  token=0x6000C7E  System.Void .cctor()
  RVA=0x041E1670  token=0x6000C7F  System.Void .ctor()
  RVA=0x0A968228  token=0x6000C80  UnityEngine.GameObject <OnExecute>b__7_0(UnityEngine.RaycastHit2D h)
  RVA=0x04DA6740  token=0x6000C81  System.Single <OnExecute>b__7_1(UnityEngine.RaycastHit2D h)
  RVA=0x04DA6750  token=0x6000C82  UnityEngine.Vector3 <OnExecute>b__7_3(UnityEngine.RaycastHit2D h)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200033E
SIZE:  0x10
FIELDS:
  public    static readonly NodeCanvas.Tasks.Actions.GetOverlapSphereObjects.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.ECSColliderResultProxy,UnityEngine.GameObject><>9__3_0  // static @ 0x8
METHODS:
  RVA=0x0A96832C  token=0x6000C86  System.Void .cctor()
  RVA=0x041E1670  token=0x6000C87  System.Void .ctor()
  RVA=0x0A9681DC  token=0x6000C88  UnityEngine.GameObject <OnExecute>b__3_0(UnityEngine.ECSColliderResultProxy c)
END_CLASS

CLASS: <InternalCoroutine>d__17
TYPE:  sealed class
TOKEN: 0x2000340
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            NodeCanvas.Tasks.Actions.ExecuteFunction_Multiplatform<>4__this  // 0x20
  public            System.Collections.IEnumerator  routine  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000C94  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000C95  System.Void System.IDisposable.Dispose()
  RVA=0x0A968048  token=0x6000C96  System.Boolean MoveNext()
  RVA=0x0A968130  token=0x6000C98  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <InternalCoroutine>d__13
TYPE:  sealed class
TOKEN: 0x200034C
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            NodeCanvas.Tasks.Actions.ExecuteFunction<>4__this  // 0x20
  public            System.Collections.IEnumerator  routine  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000CDE  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000CDF  System.Void System.IDisposable.Dispose()
  RVA=0x0A967F28  token=0x6000CE0  System.Boolean MoveNext()
  RVA=0x0A967FFC  token=0x6000CE2  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: LogMode
TYPE:  sealed struct
TOKEN: 0x2000353
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Tasks.Actions.DebugLogText.LogModeLog  // const
  public    static  NodeCanvas.Tasks.Actions.DebugLogText.LogModeWarning  // const
  public    static  NodeCanvas.Tasks.Actions.DebugLogText.LogModeError  // const
METHODS:
END_CLASS

CLASS: VerboseMode
TYPE:  sealed struct
TOKEN: 0x2000354
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Tasks.Actions.DebugLogText.VerboseModeLogAndDisplayLabel  // const
  public    static  NodeCanvas.Tasks.Actions.DebugLogText.VerboseModeLogOnly  // const
  public    static  NodeCanvas.Tasks.Actions.DebugLogText.VerboseModeDisplayLabelOnly  // const
METHODS:
END_CLASS

CLASS: Control
TYPE:  sealed struct
TOKEN: 0x2000358
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Tasks.Actions.GraphOwnerControl.ControlStartBehaviour  // const
  public    static  NodeCanvas.Tasks.Actions.GraphOwnerControl.ControlStopBehaviour  // const
  public    static  NodeCanvas.Tasks.Actions.GraphOwnerControl.ControlPauseBehaviour  // const
METHODS:
END_CLASS

CLASS: <YieldDo>d__6
TYPE:  sealed class
TOKEN: 0x2000359
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            NodeCanvas.Tasks.Actions.GraphOwnerControl<>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000D17  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000D18  System.Void System.IDisposable.Dispose()
  RVA=0x0A968520  token=0x6000D19  System.Boolean MoveNext()
  RVA=0x0A968578  token=0x6000D1B  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000366
SIZE:  0x10
FIELDS:
  public    static readonly NodeCanvas.Tasks.Actions.ExecuteStaticFunction_Multiplatform.<>c<>9  // static @ 0x0
  public    static  System.Func<NodeCanvas.Framework.Internal.BBObjectParameter,System.Object><>9__9_0  // static @ 0x8
METHODS:
  RVA=0x0A9683F4  token=0x6000D4A  System.Void .cctor()
  RVA=0x041E1670  token=0x6000D4B  System.Void .ctor()
  RVA=0x0A968250  token=0x6000D4C  System.Object <OnExecute>b__9_0(NodeCanvas.Framework.Internal.BBObjectParameter p)
END_CLASS

CLASS: TransitionCallMode
TYPE:  sealed struct
TOKEN: 0x2000368
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.StateMachines.FSM.TransitionCallModeNormal  // const
  public    static  NodeCanvas.StateMachines.FSM.TransitionCallModeStacked  // const
  public    static  NodeCanvas.StateMachines.FSM.TransitionCallModeClean  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000369
SIZE:  0x10
FIELDS:
  public    static readonly NodeCanvas.StateMachines.FSM.<>c<>9  // static @ 0x0
  public    static  System.Func<NodeCanvas.Framework.IUpdatable,System.Boolean><>9__45_0  // static @ 0x8
  public    static  System.Func<NodeCanvas.Framework.Node,System.Boolean><>9__49_0  // static @ 0x10
  public    static  System.Func<NodeCanvas.Framework.Node,System.String><>9__49_1  // static @ 0x18
METHODS:
  RVA=0x0A968390  token=0x6000D70  System.Void .cctor()
  RVA=0x041E1670  token=0x6000D71  System.Void .ctor()
  RVA=0x0A968294  token=0x6000D72  System.Boolean <OnGraphUpdate>b__45_0(NodeCanvas.Framework.IUpdatable n)
  RVA=0x0A96817C  token=0x6000D73  System.Boolean <GetStateNames>b__49_0(NodeCanvas.Framework.Node n)
  RVA=0x0A9681BC  token=0x6000D74  System.String <GetStateNames>b__49_1(NodeCanvas.Framework.Node n)
END_CLASS

CLASS: <>c__DisplayClass50_0
TYPE:  sealed class
TOKEN: 0x200036A
SIZE:  0x18
FIELDS:
  public            System.String                   name  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000D75  System.Void .ctor()
  RVA=0x0A9682B8  token=0x6000D76  System.Boolean <GetStateWithName>b__0(NodeCanvas.Framework.Node n)
END_CLASS

CLASS: TransitionEvaluationMode
TYPE:  sealed struct
TOKEN: 0x2000370
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.StateMachines.FSMState.TransitionEvaluationModeCheckContinuously  // const
  public    static  NodeCanvas.StateMachines.FSMState.TransitionEvaluationModeCheckAfterStateFinished  // const
  public    static  NodeCanvas.StateMachines.FSMState.TransitionEvaluationModeCheckManually  // const
METHODS:
END_CLASS

CLASS: BTExecutionMode
TYPE:  sealed struct
TOKEN: 0x200037A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.StateMachines.NestedBTState.BTExecutionModeOnce  // const
  public    static  NodeCanvas.StateMachines.NestedBTState.BTExecutionModeRepeat  // const
METHODS:
END_CLASS

CLASS: BTExitMode
TYPE:  sealed struct
TOKEN: 0x200037B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.StateMachines.NestedBTState.BTExitModeStopAndRestart  // const
  public    static  NodeCanvas.StateMachines.NestedBTState.BTExitModePauseAndResume  // const
METHODS:
END_CLASS

CLASS: FSMExitMode
TYPE:  sealed struct
TOKEN: 0x200037E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.StateMachines.NestedFSMState.FSMExitModeStopAndRestart  // const
  public    static  NodeCanvas.StateMachines.NestedFSMState.FSMExitModePauseAndResume  // const
METHODS:
END_CLASS

CLASS: DerivedSerializationData
TYPE:  class
TOKEN: 0x200038B
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.List<NodeCanvas.DialogueTrees.DialogueTree.ActorParameter>actorParameters  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000E98  System.Void .ctor()
END_CLASS

CLASS: ActorParameter
TYPE:  class
TOKEN: 0x200038C
SIZE:  0x30
FIELDS:
  private           System.String                   _keyName  // 0x10
  private           System.String                   _id  // 0x18
  private           UnityEngine.Object              _actorObject  // 0x20
  private           NodeCanvas.DialogueTrees.IDialogueActor_actor  // 0x28
PROPERTIES:
  name  get=0x02B2ECC0  set=0x053908C0
  ID  get=0x0A9692B0
  actor  get=0x0A96930C  set=0x0A96937C
METHODS:
  RVA=0x041E1670  token=0x6000E9E  System.Void .ctor()
  RVA=0x053908C0  token=0x6000E9F  System.Void .ctor(System.String name)
  RVA=0x0A969284  token=0x6000EA0  System.Void .ctor(System.String name, NodeCanvas.DialogueTrees.IDialogueActor actor)
  RVA=0x02B2ECC0  token=0x6000EA1  System.String ToString()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200038D
SIZE:  0x10
FIELDS:
  public    static readonly NodeCanvas.DialogueTrees.DialogueTree.<>c<>9  // static @ 0x0
  public    static  System.Func<NodeCanvas.DialogueTrees.DialogueTree.ActorParameter,System.String><>9__48_0  // static @ 0x8
METHODS:
  RVA=0x0A97C894  token=0x6000EA2  System.Void .cctor()
  RVA=0x041E1670  token=0x6000EA3  System.Void .ctor()
  RVA=0x06C08920  token=0x6000EA4  System.String <get_definedActorParameterNames>b__48_0(NodeCanvas.DialogueTrees.DialogueTree.ActorParameter r)
END_CLASS

CLASS: <>c__DisplayClass49_0
TYPE:  sealed class
TOKEN: 0x200038E
SIZE:  0x18
FIELDS:
  public            System.String                   id  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000EA5  System.Void .ctor()
  RVA=0x0A97C7D0  token=0x6000EA6  System.Boolean <GetParameterByID>b__0(NodeCanvas.DialogueTrees.DialogueTree.ActorParameter p)
END_CLASS

CLASS: <>c__DisplayClass50_0
TYPE:  sealed class
TOKEN: 0x200038F
SIZE:  0x18
FIELDS:
  public            System.String                   paramName  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000EA7  System.Void .ctor()
  RVA=0x06CC7538  token=0x6000EA8  System.Boolean <GetParameterByName>b__0(NodeCanvas.DialogueTrees.DialogueTree.ActorParameter p)
END_CLASS

CLASS: <>c__DisplayClass52_0
TYPE:  sealed class
TOKEN: 0x2000390
SIZE:  0x18
FIELDS:
  public            System.String                   paramName  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000EA9  System.Void .ctor()
  RVA=0x06CC7538  token=0x6000EAA  System.Boolean <GetActorReferenceByName>b__0(NodeCanvas.DialogueTrees.DialogueTree.ActorParameter r)
END_CLASS

CLASS: <>c__DisplayClass53_0
TYPE:  sealed class
TOKEN: 0x2000391
SIZE:  0x18
FIELDS:
  public            System.String                   paramName  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000EAB  System.Void .ctor()
  RVA=0x06CC7538  token=0x6000EAC  System.Boolean <SetActorReference>b__0(NodeCanvas.DialogueTrees.DialogueTree.ActorParameter p)
END_CLASS

CLASS: <>c__DisplayClass54_0
TYPE:  sealed class
TOKEN: 0x2000392
SIZE:  0x20
FIELDS:
  public            System.Collections.Generic.KeyValuePair<System.String,NodeCanvas.DialogueTrees.IDialogueActor>pair  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000EAD  System.Void .ctor()
  RVA=0x0A97C808  token=0x6000EAE  System.Boolean <SetActorReferences>b__0(NodeCanvas.DialogueTrees.DialogueTree.ActorParameter p)
END_CLASS

CLASS: <>c__DisplayClass16_0
TYPE:  sealed class
TOKEN: 0x200039B
SIZE:  0x18
FIELDS:
  public            NodeCanvas.Framework.IBlackboardbb  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000EF9  System.Void .ctor()
  RVA=0x0A97C558  token=0x6000EFA  System.String <BlackboardReplace>b__0(System.String input)
END_CLASS

CLASS: <UpdateAction>d__10
TYPE:  sealed class
TOKEN: 0x200039D
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            NodeCanvas.DialogueTrees.ActionNode<>4__this  // 0x20
  public            UnityEngine.Component           actionAgent  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000F06  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000F07  System.Void System.IDisposable.Dispose()
  RVA=0x0A97C8F8  token=0x6000F08  System.Boolean MoveNext()
  RVA=0x0A97C9A0  token=0x6000F0A  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: Choice
TYPE:  class
TOKEN: 0x20003A2
SIZE:  0x28
FIELDS:
  public            System.Boolean                  isUnfolded  // 0x10
  public            NodeCanvas.DialogueTrees.Statementstatement  // 0x18
  public            NodeCanvas.Framework.ConditionTaskcondition  // 0x20
METHODS:
  RVA=0x015EFCD0  token=0x6000F23  System.Void .ctor()
  RVA=0x0952DB04  token=0x6000F24  System.Void .ctor(NodeCanvas.DialogueTrees.Statement statement)
END_CLASS

CLASS: <>c__DisplayClass9_0
TYPE:  sealed class
TOKEN: 0x20003A3
SIZE:  0x20
FIELDS:
  public            NodeCanvas.DialogueTrees.MultipleChoiceNode<>4__this  // 0x10
  public            System.Int32                    index  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000F25  System.Void .ctor()
  RVA=0x0A97C85C  token=0x6000F26  System.Void <OnOptionSelected>b__0()
END_CLASS

CLASS: Option
TYPE:  class
TOKEN: 0x20003A6
SIZE:  0x20
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Single>weight  // 0x10
  public            NodeCanvas.Framework.ConditionTaskcondition  // 0x18
METHODS:
  RVA=0x0A978430  token=0x6000F33  System.Void .ctor(System.Single weightValue, NodeCanvas.Framework.IBlackboard bbValue)
END_CLASS

CLASS: SubtitleDelays
TYPE:  class
TOKEN: 0x20003AC
SIZE:  0x20
FIELDS:
  public            System.Single                   characterDelay  // 0x10
  public            System.Single                   sentenceDelay  // 0x14
  public            System.Single                   commaDelay  // 0x18
  public            System.Single                   finalDelay  // 0x1c
METHODS:
  RVA=0x04DA67F0  token=0x6000F62  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass31_0
TYPE:  sealed class
TOKEN: 0x20003AD
SIZE:  0x18
FIELDS:
  public            System.Boolean                  inputDown  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000F63  System.Void .ctor()
  RVA=0x015EFCD0  token=0x6000F64  System.Void <Internal_OnSubtitlesRequestInfo>b__0()
END_CLASS

CLASS: <Internal_OnSubtitlesRequestInfo>d__31
TYPE:  sealed class
TOKEN: 0x20003AE
SIZE:  0x60
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            NodeCanvas.DialogueTrees.SubtitlesRequestInfoinfo  // 0x20
  public            NodeCanvas.DialogueTrees.UI.Examples.DialogueUGUI<>4__this  // 0x28
  private           NodeCanvas.DialogueTrees.UI.Examples.DialogueUGUI.<>c__DisplayClass31_0<>8__1  // 0x30
  private           System.String                   <text>5__2  // 0x38
  private           UnityEngine.AudioClip           <audio>5__3  // 0x40
  private           System.Single                   <timer>5__4  // 0x48
  private           System.String                   <tempText>5__5  // 0x50
  private           System.Int32                    <i>5__6  // 0x58
  private           System.Char                     <c>5__7  // 0x5c
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000F65  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000F66  System.Void System.IDisposable.Dispose()
  RVA=0x0A97BC90  token=0x6000F67  System.Boolean MoveNext()
  RVA=0x0A97C50C  token=0x6000F69  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <CheckInput>d__33
TYPE:  sealed class
TOKEN: 0x20003AF
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            NodeCanvas.DialogueTrees.UI.Examples.DialogueUGUI<>4__this  // 0x20
  public            System.Action                   Do  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000F6B  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000F6C  System.Void System.IDisposable.Dispose()
  RVA=0x0A97B86C  token=0x6000F6D  System.Boolean MoveNext()
  RVA=0x0A97B8C8  token=0x6000F6F  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <DelayPrint>d__34
TYPE:  sealed class
TOKEN: 0x20003B0
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.Single                   time  // 0x20
  private           System.Single                   <timer>5__2  // 0x24
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000F71  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000F72  System.Void System.IDisposable.Dispose()
  RVA=0x0A97BBD8  token=0x6000F73  System.Boolean MoveNext()
  RVA=0x0A97BC44  token=0x6000F75  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass35_0
TYPE:  sealed class
TOKEN: 0x20003B1
SIZE:  0x20
FIELDS:
  public            NodeCanvas.DialogueTrees.UI.Examples.DialogueUGUI<>4__this  // 0x10
  public            NodeCanvas.DialogueTrees.MultipleChoiceRequestInfoinfo  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000F77  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass35_1
TYPE:  sealed class
TOKEN: 0x20003B2
SIZE:  0x20
FIELDS:
  public            UnityEngine.UI.Button           btn  // 0x10
  public            NodeCanvas.DialogueTrees.UI.Examples.DialogueUGUI.<>c__DisplayClass35_0CS$<>8__locals1  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000F78  System.Void .ctor()
  RVA=0x0A97C74C  token=0x6000F79  System.Void <OnMultipleChoiceRequest>b__0()
END_CLASS

CLASS: <CountDown>d__36
TYPE:  sealed class
TOKEN: 0x20003B3
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            NodeCanvas.DialogueTrees.UI.Examples.DialogueUGUI<>4__this  // 0x20
  public            NodeCanvas.DialogueTrees.MultipleChoiceRequestInfoinfo  // 0x28
  private           System.Single                   <timer>5__2  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000F7A  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000F7B  System.Void System.IDisposable.Dispose()
  RVA=0x0A97BA24  token=0x6000F7C  System.Boolean MoveNext()
  RVA=0x0A97BB8C  token=0x6000F7E  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: DerivedSerializationData
TYPE:  class
TOKEN: 0x20003B5
SIZE:  0x18
FIELDS:
  public            System.Boolean                  repeat  // 0x10
  public            System.Single                   updateInterval  // 0x14
METHODS:
  RVA=0x041E1670  token=0x6000F91  System.Void .ctor()
END_CLASS

CLASS: ParallelPolicy
TYPE:  sealed struct
TOKEN: 0x20003BF
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.BehaviourTrees.Parallel.ParallelPolicyFirstFailure  // const
  public    static  NodeCanvas.BehaviourTrees.Parallel.ParallelPolicyFirstSuccess  // const
  public    static  NodeCanvas.BehaviourTrees.Parallel.ParallelPolicyFirstSuccessOrFailure  // const
METHODS:
END_CLASS

CLASS: Desire
TYPE:  class
TOKEN: 0x20003C2
SIZE:  0x28
FIELDS:
  public            System.String                   name  // 0x10
  public            System.Boolean                  foldout  // 0x18
  public            System.Collections.Generic.List<NodeCanvas.BehaviourTrees.PrioritySelector.Consideration>considerations  // 0x20
METHODS:
  RVA=0x0A96C1E8  token=0x6000FD6  NodeCanvas.BehaviourTrees.PrioritySelector.Consideration AddConsideration(NodeCanvas.Framework.IBlackboard bb)
  RVA=0x0A96C318  token=0x6000FD7  System.Void RemoveConsideration(NodeCanvas.BehaviourTrees.PrioritySelector.Consideration consideration)
  RVA=0x0A96C278  token=0x6000FD8  System.Single GetCompoundUtility()
  RVA=0x0A96C36C  token=0x6000FD9  System.Void .ctor()
END_CLASS

CLASS: Consideration
TYPE:  class
TOKEN: 0x20003C3
SIZE:  0x20
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Single>input  // 0x10
  public            NodeCanvas.Framework.BBParameter<UnityEngine.AnimationCurve>function  // 0x18
PROPERTIES:
  utility  get=0x0A96BB74
METHODS:
  RVA=0x0A96BA88  token=0x6000FDB  System.Void .ctor(NodeCanvas.Framework.IBlackboard blackboard)
END_CLASS

CLASS: CaseSelectionMode
TYPE:  sealed struct
TOKEN: 0x20003C9
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.BehaviourTrees.Switch.CaseSelectionModeIndexBased  // const
  public    static  NodeCanvas.BehaviourTrees.Switch.CaseSelectionModeEnumBased  // const
METHODS:
END_CLASS

CLASS: OutOfRangeMode
TYPE:  sealed struct
TOKEN: 0x20003CA
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.BehaviourTrees.Switch.OutOfRangeModeReturnFailure  // const
  public    static  NodeCanvas.BehaviourTrees.Switch.OutOfRangeModeLoopIndex  // const
METHODS:
END_CLASS

CLASS: FilterMode
TYPE:  sealed struct
TOKEN: 0x20003CD
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.BehaviourTrees.Filter.FilterModeLimitNumberOfTimes  // const
  public    static  NodeCanvas.BehaviourTrees.Filter.FilterModeCoolDown  // const
METHODS:
END_CLASS

CLASS: Policy
TYPE:  sealed struct
TOKEN: 0x20003CE
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.BehaviourTrees.Filter.PolicySuccessOrFailure  // const
  public    static  NodeCanvas.BehaviourTrees.Filter.PolicySuccessOnly  // const
  public    static  NodeCanvas.BehaviourTrees.Filter.PolicyFailureOnly  // const
METHODS:
END_CLASS

CLASS: <Cooldown>d__13
TYPE:  sealed class
TOKEN: 0x20003CF
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            NodeCanvas.BehaviourTrees.Filter<>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000FFF  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6001000  System.Void System.IDisposable.Dispose()
  RVA=0x0A97B914  token=0x6001001  System.Boolean MoveNext()
  RVA=0x0A97B9D8  token=0x6001003  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: GuardMode
TYPE:  sealed struct
TOKEN: 0x20003D1
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.BehaviourTrees.Guard.GuardModeReturnFailure  // const
  public    static  NodeCanvas.BehaviourTrees.Guard.GuardModeWaitUntilReleased  // const
METHODS:
END_CLASS

CLASS: TerminationConditions
TYPE:  sealed struct
TOKEN: 0x20003D5
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.BehaviourTrees.Iterator.TerminationConditionsNone  // const
  public    static  NodeCanvas.BehaviourTrees.Iterator.TerminationConditionsFirstSuccess  // const
  public    static  NodeCanvas.BehaviourTrees.Iterator.TerminationConditionsFirstFailure  // const
METHODS:
END_CLASS

CLASS: MonitorMode
TYPE:  sealed struct
TOKEN: 0x20003D8
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.BehaviourTrees.Monitor.MonitorModeFailure  // const
  public    static  NodeCanvas.BehaviourTrees.Monitor.MonitorModeSuccess  // const
  public    static  NodeCanvas.BehaviourTrees.Monitor.MonitorModeAnyStatus  // const
METHODS:
END_CLASS

CLASS: ReturnStatusMode
TYPE:  sealed struct
TOKEN: 0x20003D9
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.BehaviourTrees.Monitor.ReturnStatusModeOriginalDecoratedChildStatus  // const
  public    static  NodeCanvas.BehaviourTrees.Monitor.ReturnStatusModeNewDecoratorActionStatus  // const
METHODS:
END_CLASS

CLASS: RemapStatus
TYPE:  sealed struct
TOKEN: 0x20003DC
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.BehaviourTrees.Remapper.RemapStatusFailure  // const
  public    static  NodeCanvas.BehaviourTrees.Remapper.RemapStatusSuccess  // const
METHODS:
END_CLASS

CLASS: RepeaterMode
TYPE:  sealed struct
TOKEN: 0x20003DE
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.BehaviourTrees.Repeater.RepeaterModeRepeatTimes  // const
  public    static  NodeCanvas.BehaviourTrees.Repeater.RepeaterModeRepeatUntil  // const
  public    static  NodeCanvas.BehaviourTrees.Repeater.RepeaterModeRepeatForever  // const
METHODS:
END_CLASS

CLASS: RepeatUntilStatus
TYPE:  sealed struct
TOKEN: 0x20003DF
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.BehaviourTrees.Repeater.RepeatUntilStatusFailure  // const
  public    static  NodeCanvas.BehaviourTrees.Repeater.RepeatUntilStatusSuccess  // const
METHODS:
END_CLASS

CLASS: ToggleMode
TYPE:  sealed struct
TOKEN: 0x20003E9
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.BehaviourTrees.NodeToggler.ToggleModeEnable  // const
  public    static  NodeCanvas.BehaviourTrees.NodeToggler.ToggleModeDisable  // const
  public    static  NodeCanvas.BehaviourTrees.NodeToggler.ToggleModeToggle  // const
METHODS:
END_CLASS

CLASS: InvokeArguments
TYPE:  sealed class
TOKEN: 0x20003F1
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0A975840  token=0x6001073  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x015547C0  token=0x6001074  System.Void Invoke(UnityEngine.Transform sender, UnityEngine.Transform receiver, System.Boolean isGlobal, System.Object[] args)
  RVA=0x0A9757A4  token=0x6001075  System.IAsyncResult BeginInvoke(UnityEngine.Transform sender, UnityEngine.Transform receiver, System.Boolean isGlobal, System.Object[] args, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6001076  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: <>c__DisplayClass10_0
TYPE:  sealed class
TOKEN: 0x20003F2
SIZE:  0x18
FIELDS:
  public            System.String                   name  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6001077  System.Void .ctor()
  RVA=0x0A8D9104  token=0x6001078  System.Boolean <RemoveParameter>b__0(ParadoxNotion.DynamicParameterDefinition p)
END_CLASS

CLASS: UpdateMode
TYPE:  sealed struct
TOKEN: 0x20003F8
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Framework.Graph.UpdateModeNormalUpdate  // const
  public    static  NodeCanvas.Framework.Graph.UpdateModeLateUpdate  // const
  public    static  NodeCanvas.Framework.Graph.UpdateModeFixedUpdate  // const
  public    static  NodeCanvas.Framework.Graph.UpdateModeManual  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20003F9
SIZE:  0x10
FIELDS:
  public    static readonly NodeCanvas.Framework.Graph.<>c  <>9  // static @ 0x0
  public    static  System.Func<NodeCanvas.Framework.Node,System.Int32><>9__174_0  // static @ 0x8
  public    static  System.Func<NodeCanvas.Framework.Node,System.Boolean><>9__218_0  // static @ 0x10
  public    static  System.Func<NodeCanvas.Framework.Node,System.Boolean><>9__219_0  // static @ 0x18
  public    static  System.Func<NodeCanvas.Framework.BBParameter,System.Boolean><>9__222_0  // static @ 0x20
METHODS:
  RVA=0x0A98D1AC  token=0x600115F  System.Void .cctor()
  RVA=0x041E1670  token=0x6001160  System.Void .ctor()
  RVA=0x0A98CA98  token=0x6001161  System.Int32 <UpdateNodeIDs>b__174_0(NodeCanvas.Framework.Node n)
  RVA=0x0A98CA50  token=0x6001162  System.Boolean <GetRootNodes>b__218_0(NodeCanvas.Framework.Node n)
  RVA=0x0A98CA08  token=0x6001163  System.Boolean <GetLeafNodes>b__219_0(NodeCanvas.Framework.Node n)
  RVA=0x0A98C9F0  token=0x6001164  System.Boolean <GetDefinedParameters>b__222_0(NodeCanvas.Framework.BBParameter p)
END_CLASS

CLASS: <>c__DisplayClass182_0
TYPE:  sealed class
TOKEN: 0x20003FA
SIZE:  0x48
FIELDS:
  public            NodeCanvas.Framework.Graph      <>4__this  // 0x10
  public            NodeCanvas.Framework.Internal.GraphLoadDatadata  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6001165  System.Void .ctor()
  RVA=0x0A98CC14  token=0x6001166  System.Void <LoadOverwriteAsync>b__0()
END_CLASS

CLASS: <LoadOverwriteAsync>d__182
TYPE:  sealed struct
TOKEN: 0x20003FB
SIZE:  0x80
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncVoidMethodBuilder<>t__builder  // 0x18
  public            NodeCanvas.Framework.Graph      <>4__this  // 0x38
  public            NodeCanvas.Framework.Internal.GraphLoadDatadata  // 0x40
  public            System.Action                   callback  // 0x70
  private           System.Runtime.CompilerServices.TaskAwaiter<>u__1  // 0x78
METHODS:
  RVA=0x0A98C794  token=0x6001167  System.Void MoveNext()
  RVA=0x053982D4  token=0x6001168  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <>c__DisplayClass183_0
TYPE:  sealed class
TOKEN: 0x20003FC
SIZE:  0x48
FIELDS:
  public            NodeCanvas.Framework.Graph      <>4__this  // 0x10
  public            NodeCanvas.Framework.Internal.GraphLoadDatadata  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6001169  System.Void .ctor()
  RVA=0x0A98CC58  token=0x600116A  System.Void <LoadOverwrite>b__0()
END_CLASS

CLASS: <>c__DisplayClass213_0
TYPE:  sealed class
TOKEN: 0x20003FD
SIZE:  0x18
FIELDS:
  public            System.Int32                    searchID  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600116B  System.Void .ctor()
  RVA=0x0A98CCE8  token=0x600116C  System.Boolean <GetNodeWithID>b__0(NodeCanvas.Framework.Node n)
END_CLASS

CLASS: <>c__DisplayClass215_0`1
TYPE:  sealed class
TOKEN: 0x20003FE
FIELDS:
  public            System.String                   tagName  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600116D  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600116E  System.Boolean <GetNodeWithTag>b__0(T n)
END_CLASS

CLASS: <>c__DisplayClass216_0`1
TYPE:  sealed class
TOKEN: 0x20003FF
FIELDS:
  public            System.String                   tagName  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600116F  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6001170  System.Boolean <GetNodesWithTag>b__0(T n)
END_CLASS

CLASS: <>c__217`1
TYPE:  sealed class
TOKEN: 0x2000400
FIELDS:
  public    static readonly NodeCanvas.Framework.Graph.<>c__217<T><>9  // static @ 0x0
  public    static  System.Func<T,System.Boolean>   <>9__217_0  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001171  System.Void .cctor()
  RVA=-1  // not resolved  token=0x6001172  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6001173  System.Boolean <GetAllTagedNodes>b__217_0(T n)
END_CLASS

CLASS: <>c__DisplayClass230_0
TYPE:  sealed class
TOKEN: 0x2000401
SIZE:  0x20
FIELDS:
  public            System.Collections.Generic.Stack<ParadoxNotion.HierarchyTree.Element>stack  // 0x10
  public            ParadoxNotion.HierarchyTree.ElementparentElement  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6001174  System.Void .ctor()
  RVA=0x0A98CD08  token=0x6001175  System.Void <CollectSubElements>b__0(System.Object o)
  RVA=0x0A98CE00  token=0x6001176  System.Void <CollectSubElements>b__1(System.Object o, ParadoxNotion.Serialization.FullSerializer.fsData d)
END_CLASS

CLASS: <>c__DisplayClass233_0
TYPE:  sealed class
TOKEN: 0x2000402
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.List<NodeCanvas.Framework.Task>result  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6001177  System.Void .ctor()
  RVA=0x0A98CE80  token=0x6001178  System.Void <GetTasksInElement>b__0(System.Object o, ParadoxNotion.Serialization.FullSerializer.fsData d)
END_CLASS

CLASS: <>c__DisplayClass234_0
TYPE:  sealed class
TOKEN: 0x2000403
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.List<NodeCanvas.Framework.BBParameter>result  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6001179  System.Void .ctor()
  RVA=0x0A98CF14  token=0x600117A  System.Void <GetParametersInElement>b__0(System.Object o, ParadoxNotion.Serialization.FullSerializer.fsData d)
END_CLASS

CLASS: <>c__DisplayClass239_0
TYPE:  sealed class
TOKEN: 0x2000404
SIZE:  0x18
FIELDS:
  public            NodeCanvas.Framework.Node       node  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600117B  System.Void .ctor()
  RVA=0x0A98CFA8  token=0x600117C  System.Boolean <RemoveNode>b__0(NodeCanvas.Framework.Node n)
END_CLASS

CLASS: <>c__DisplayClass242_0
TYPE:  sealed class
TOKEN: 0x2000405
SIZE:  0x18
FIELDS:
  public            NodeCanvas.Framework.Graph      targetGraph  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600117D  System.Void .ctor()
  RVA=0x0A98D028  token=0x600117E  System.Boolean <CloneNodes>b__0(NodeCanvas.Framework.Node n)
END_CLASS

CLASS: EnableAction
TYPE:  sealed struct
TOKEN: 0x2000407
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Framework.GraphOwner.EnableActionEnableBehaviour  // const
  public    static  NodeCanvas.Framework.GraphOwner.EnableActionDoNothing  // const
METHODS:
END_CLASS

CLASS: DisableAction
TYPE:  sealed struct
TOKEN: 0x2000408
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Framework.GraphOwner.DisableActionDisableBehaviour  // const
  public    static  NodeCanvas.Framework.GraphOwner.DisableActionPauseBehaviour  // const
  public    static  NodeCanvas.Framework.GraphOwner.DisableActionDoNothing  // const
METHODS:
END_CLASS

CLASS: FirstActivation
TYPE:  sealed struct
TOKEN: 0x2000409
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Framework.GraphOwner.FirstActivationOnEnable  // const
  public    static  NodeCanvas.Framework.GraphOwner.FirstActivationOnStart  // const
  public    static  NodeCanvas.Framework.GraphOwner.FirstActivationAsync  // const
METHODS:
END_CLASS

CLASS: SubInfo
TYPE:  class
TOKEN: 0x200040A
SIZE:  0x40
FIELDS:
  public            NodeCanvas.Framework.Graph      graph  // 0x10
  public            NodeCanvas.Framework.Graph      originalGraph  // 0x18
  public            NodeCanvas.Framework.Graph      parentGraph  // 0x20
  public            System.String                   name  // 0x28
  public            NodeCanvas.Framework.IGraphAssignableassignable  // 0x30
  public            System.Boolean                  isSuc  // 0x38
METHODS:
  RVA=0x041E1670  token=0x60011C9  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass95_0
TYPE:  sealed class
TOKEN: 0x200040B
SIZE:  0x28
FIELDS:
  public            NodeCanvas.Framework.GraphOwner <>4__this  // 0x10
  public            System.Action                   callBack  // 0x18
  public            System.Action                   <>9__1  // 0x20
METHODS:
  RVA=0x041E1670  token=0x60011CA  System.Void .ctor()
  RVA=0x03073770  token=0x60011CB  System.Void <StartBehaviourAsync>b__0(NodeCanvas.Framework.Graph instance)
  RVA=0x0306AF00  token=0x60011CC  System.Void <StartBehaviourAsync>b__1()
END_CLASS

CLASS: <>c__DisplayClass96_0
TYPE:  sealed class
TOKEN: 0x200040C
SIZE:  0x30
FIELDS:
  public            NodeCanvas.Framework.GraphOwner <>4__this  // 0x10
  public            NodeCanvas.Framework.Graph      originalGraph  // 0x18
  public            System.Boolean                  isSuc  // 0x20
  public            NodeCanvas.Framework.Graph      clone  // 0x28
METHODS:
  RVA=0x041E1670  token=0x60011CD  System.Void .ctor()
  RVA=0x031136D0  token=0x60011CE  System.Void <GetInstanceAsync>b__0()
END_CLASS

CLASS: <GetInstanceAsync>d__96
TYPE:  sealed struct
TOKEN: 0x200040D
SIZE:  0x68
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncVoidMethodBuilder<>t__builder  // 0x18
  public            NodeCanvas.Framework.GraphOwner <>4__this  // 0x38
  public            NodeCanvas.Framework.Graph      originalGraph  // 0x40
  public            System.Action<NodeCanvas.Framework.Graph>callBack  // 0x48
  private           NodeCanvas.Framework.GraphOwner.<>c__DisplayClass96_0<>8__1  // 0x50
  private           System.Threading.Tasks.Task     <task>5__2  // 0x58
  private           System.Single                   <start>5__3  // 0x60
  private           System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter<>u__1  // 0x64
METHODS:
  RVA=0x02FC3A70  token=0x60011CF  System.Void MoveNext()
  RVA=0x053982D4  token=0x60011D0  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <>c__DisplayClass97_0
TYPE:  sealed class
TOKEN: 0x200040E
SIZE:  0x20
FIELDS:
  public            NodeCanvas.Framework.GraphOwner <>4__this  // 0x10
  public            System.Collections.Generic.List<NodeCanvas.Framework.GraphOwner.SubInfo>subInfos  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60011D1  System.Void .ctor()
  RVA=0x02FC1D80  token=0x60011D2  System.Void <CreateSubInstanceAsync>b__0()
END_CLASS

CLASS: <CreateSubInstanceAsync>d__97
TYPE:  sealed struct
TOKEN: 0x200040F
SIZE:  0x60
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncVoidMethodBuilder<>t__builder  // 0x18
  public            NodeCanvas.Framework.GraphOwner <>4__this  // 0x38
  public            System.Action                   callBack  // 0x40
  private           NodeCanvas.Framework.GraphOwner.<>c__DisplayClass97_0<>8__1  // 0x48
  private           System.Threading.Tasks.Task     <task>5__2  // 0x50
  private           System.Single                   <start>5__3  // 0x58
  private           System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter<>u__1  // 0x5c
METHODS:
  RVA=0x02FC1E80  token=0x60011D3  System.Void MoveNext()
  RVA=0x053982D4  token=0x60011D4  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <>c__DisplayClass105_0`1
TYPE:  sealed class
TOKEN: 0x2000410
FIELDS:
  public            System.String                   name  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60011D5  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60011D6  System.Boolean <GetExposedParameterValue>b__0(NodeCanvas.Framework.ExposedParameter x)
END_CLASS

CLASS: <>c__DisplayClass106_0`1
TYPE:  sealed class
TOKEN: 0x2000411
FIELDS:
  public            System.String                   name  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60011D7  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60011D8  System.Boolean <SetExposedParameterValue>b__0(NodeCanvas.Framework.ExposedParameter x)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x2000414
SIZE:  0x20
FIELDS:
  public            NodeCanvas.Framework.IGraphAssignableassignable  // 0x10
  public            System.Action<System.Boolean>   callback  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60011F1  System.Void .ctor()
  RVA=0x0A98CC90  token=0x60011F2  System.Void <TryStartSubGraph>b__0(System.Boolean result)
END_CLASS

CLASS: AutoSortWithChildrenConnections
TYPE:  class
TOKEN: 0x2000421
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600127B  System.Void .ctor()
END_CLASS

CLASS: <YieldBreak>d__137
TYPE:  sealed class
TOKEN: 0x2000422
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.Action                   resume  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600127C  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600127D  System.Void System.IDisposable.Dispose()
  RVA=0x0A98D210  token=0x600127E  System.Boolean MoveNext()
  RVA=0x0A98D294  token=0x6001280  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass147_0
TYPE:  sealed class
TOKEN: 0x2000423
SIZE:  0x20
FIELDS:
  public            NodeCanvas.Framework.Node       b  // 0x10
  public            NodeCanvas.Framework.Node       a  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6001282  System.Void .ctor()
  RVA=0x0A98CB8C  token=0x6001283  System.Boolean <AreNodesConnected>b__0(NodeCanvas.Framework.Connection c)
  RVA=0x0A98CBAC  token=0x6001284  System.Boolean <AreNodesConnected>b__1(NodeCanvas.Framework.Connection c)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000424
SIZE:  0x10
FIELDS:
  public    static readonly NodeCanvas.Framework.Node.<>c   <>9  // static @ 0x0
  public    static  System.Func<NodeCanvas.Framework.Connection,NodeCanvas.Framework.Node><>9__152_0  // static @ 0x8
  public    static  System.Func<NodeCanvas.Framework.Connection,NodeCanvas.Framework.Node><>9__153_0  // static @ 0x10
METHODS:
  RVA=0x0A98D148  token=0x6001285  System.Void .cctor()
  RVA=0x041E1670  token=0x6001286  System.Void .ctor()
  RVA=0x06C08920  token=0x6001287  NodeCanvas.Framework.Node <GetParentNodes>b__152_0(NodeCanvas.Framework.Connection c)
  RVA=0x06C08908  token=0x6001288  NodeCanvas.Framework.Node <GetChildNodes>b__153_0(NodeCanvas.Framework.Connection c)
END_CLASS

CLASS: <>c__DisplayClass154_0
TYPE:  sealed class
TOKEN: 0x2000425
SIZE:  0x18
FIELDS:
  public            NodeCanvas.Framework.Node       parentNode  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6001289  System.Void .ctor()
  RVA=0x0A98CBF4  token=0x600128A  System.Boolean <IsChildOf>b__0(NodeCanvas.Framework.Connection c)
END_CLASS

CLASS: <>c__DisplayClass155_0
TYPE:  sealed class
TOKEN: 0x2000426
SIZE:  0x18
FIELDS:
  public            NodeCanvas.Framework.Node       childNode  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600128B  System.Void .ctor()
  RVA=0x0A98CB8C  token=0x600128C  System.Boolean <IsParentOf>b__0(NodeCanvas.Framework.Connection c)
END_CLASS

CLASS: <>c__DisplayClass8_0
TYPE:  sealed class
TOKEN: 0x2000429
FIELDS:
  public            NodeCanvas.Framework.NodeReference<T>_this  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001297  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6001298  System.Boolean <Get>b__0(T x)
END_CLASS

CLASS: ActionsExecutionMode
TYPE:  sealed struct
TOKEN: 0x200042C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Framework.ActionList.ActionsExecutionModeActionsRunInSequence  // const
  public    static  NodeCanvas.Framework.ActionList.ActionsExecutionModeActionsRunInParallel  // const
METHODS:
END_CLASS

CLASS: <IndependentActionUpdater>d__12
TYPE:  sealed class
TOKEN: 0x200042F
SIZE:  0x40
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            NodeCanvas.Framework.ActionTask <>4__this  // 0x20
  public            UnityEngine.Component           agent  // 0x28
  public            NodeCanvas.Framework.IBlackboardblackboard  // 0x30
  public            System.Action<NodeCanvas.Framework.Status>callback  // 0x38
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60012BA  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60012BB  System.Void System.IDisposable.Dispose()
  RVA=0x0A98C6CC  token=0x60012BC  System.Boolean MoveNext()
  RVA=0x0A98C748  token=0x60012BE  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: ConditionsCheckMode
TYPE:  sealed struct
TOKEN: 0x2000431
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Framework.ConditionList.ConditionsCheckModeAllTrueRequired  // const
  public    static  NodeCanvas.Framework.ConditionList.ConditionsCheckModeAnyTrueSuffice  // const
METHODS:
END_CLASS

CLASS: <Flip>d__13
TYPE:  sealed class
TOKEN: 0x2000434
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            NodeCanvas.Framework.ConditionTask<>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60012DA  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60012DB  System.Void System.IDisposable.Dispose()
  RVA=0x0A98C3F0  token=0x60012DC  System.Boolean MoveNext()
  RVA=0x0A98C450  token=0x60012DE  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: GetFromAgentAttribute
TYPE:  class
TOKEN: 0x2000438
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6001318  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass11_0
TYPE:  sealed class
TOKEN: 0x200043B
SIZE:  0x18
FIELDS:
  public            NodeCanvas.Framework.IBlackboardbb  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6001352  System.Void .ctor()
  RVA=0x0A98CB04  token=0x6001353  System.Void <SetBBFields>b__0(System.Object o, ParadoxNotion.Serialization.FullSerializer.fsData d)
END_CLASS

CLASS: <>c__DisplayClass20_0
TYPE:  sealed class
TOKEN: 0x200043D
FIELDS:
  public            System.Func<System.Object>      convertFunc  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001366  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6001367  T <BindGetter>b__0()
END_CLASS

CLASS: <>c__DisplayClass21_0
TYPE:  sealed class
TOKEN: 0x200043E
FIELDS:
  public            System.Action<System.Object>    convertFunc  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001368  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6001369  System.Void <BindSetter>b__0(T value)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200043F
FIELDS:
  public    static readonly NodeCanvas.Framework.BBParameter.<>c<T><>9  // static @ 0x0
  public    static  System.Action<T>                <>9__21_1  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600136A  System.Void .cctor()
  RVA=-1  // not resolved  token=0x600136B  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600136C  System.Void <BindSetter>b__21_1(T value)
END_CLASS

CLASS: SingletonMode
TYPE:  sealed struct
TOKEN: 0x2000442
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Framework.GlobalBlackboard.SingletonModeDestroyComponentOnly  // const
  public    static  NodeCanvas.Framework.GlobalBlackboard.SingletonModeDestroyEntireGameObject  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass14_0
TYPE:  sealed class
TOKEN: 0x2000443
SIZE:  0x18
FIELDS:
  public            System.String                   name  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60013A3  System.Void .ctor()
  RVA=0x0A98CBCC  token=0x60013A4  System.Boolean <Find>b__0(NodeCanvas.Framework.GlobalBlackboard b)
END_CLASS

CLASS: <GetAllParents>d__1
TYPE:  sealed class
TOKEN: 0x2000447
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           NodeCanvas.Framework.IBlackboard<>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  private           NodeCanvas.Framework.IBlackboardblackboard  // 0x28
  public            NodeCanvas.Framework.IBlackboard<>3__blackboard  // 0x30
  private           System.Boolean                  includeSelf  // 0x38
  public            System.Boolean                  <>3__includeSelf  // 0x39
  private           NodeCanvas.Framework.IBlackboard<current>5__2  // 0x40
PROPERTIES:
  System.Collections.Generic.IEnumerator<NodeCanvas.Framework.IBlackboard>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x60013C4  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60013C5  System.Void System.IDisposable.Dispose()
  RVA=0x0A98C49C  token=0x60013C6  System.Boolean MoveNext()
  RVA=0x0A98C5E4  token=0x60013C8  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0A98C540  token=0x60013CA  System.Collections.Generic.IEnumerator<NodeCanvas.Framework.IBlackboard> System.Collections.Generic.IEnumerable<NodeCanvas.Framework.IBlackboard>.GetEnumerator()
  RVA=0x0A98C5DC  token=0x60013CB  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <GetVariables>d__14
TYPE:  sealed class
TOKEN: 0x2000448
SIZE:  0x78
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           NodeCanvas.Framework.Variable   <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  private           NodeCanvas.Framework.IBlackboardblackboard  // 0x28
  public            NodeCanvas.Framework.IBlackboard<>3__blackboard  // 0x30
  private           System.Type                     ofType  // 0x38
  public            System.Type                     <>3__ofType  // 0x40
  private           System.Collections.Generic.IEnumerator<NodeCanvas.Framework.Variable><>7__wrap1  // 0x48
  private           System.Collections.Generic.Dictionary.Enumerator<System.String,NodeCanvas.Framework.Variable><>7__wrap2  // 0x50
PROPERTIES:
  System.Collections.Generic.IEnumerator<NodeCanvas.Framework.Variable>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x60013CC  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x04CC4380  token=0x60013CD  System.Void System.IDisposable.Dispose()
  RVA=0x0306E380  token=0x60013CE  System.Boolean MoveNext()
  RVA=0x0A98C684  token=0x60013CF  System.Void <>m__Finally1()
  RVA=0x04CD8F80  token=0x60013D0  System.Void <>m__Finally2()
  RVA=0x0A98C638  token=0x60013D2  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x03A112D0  token=0x60013D4  System.Collections.Generic.IEnumerator<NodeCanvas.Framework.Variable> System.Collections.Generic.IEnumerable<NodeCanvas.Framework.Variable>.GetEnumerator()
  RVA=0x0A98C630  token=0x60013D5  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200044A
SIZE:  0x10
FIELDS:
  public    static readonly NodeCanvas.Framework.BlackboardHelpers.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Type,System.Type><>9__1_0  // static @ 0x8
METHODS:
  RVA=0x04D4D170  token=0x60013D8  System.Void .cctor()
  RVA=0x041E1670  token=0x60013D9  System.Void .ctor()
  RVA=0x049AF370  token=0x60013DA  System.Type <CreateVariableType>b__1_0(System.Type t)
END_CLASS

CLASS: <>c__DisplayClass50_0
TYPE:  sealed class
TOKEN: 0x200044C
SIZE:  0x20
FIELDS:
  public            NodeCanvas.Framework.Variable   <>4__this  // 0x10
  public            System.Func<System.Object,System.Object>converter  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6001401  System.Void .ctor()
  RVA=0x0A98D088  token=0x6001402  System.Object <GetGetConverter>b__0()
  RVA=0x0A98D0AC  token=0x6001403  System.Object <GetGetConverter>b__1()
END_CLASS

CLASS: <>c__DisplayClass52_0
TYPE:  sealed class
TOKEN: 0x200044D
SIZE:  0x20
FIELDS:
  public            NodeCanvas.Framework.Variable   <>4__this  // 0x10
  public            System.Func<System.Object,System.Object>converter  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6001404  System.Void .ctor()
  RVA=0x0A98D0E8  token=0x6001405  System.Void <GetSetConverter>b__0(System.Object x)
  RVA=0x0A98D10C  token=0x6001406  System.Void <GetSetConverter>b__1(System.Object x)
END_CLASS

CLASS: <>c__DisplayClass31_0
TYPE:  sealed class
TOKEN: 0x200044F
FIELDS:
  public            System.Reflection.FieldInfo     field  // 0x0
  public            UnityEngine.Component           instance  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600141D  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600141E  T <InitializePropertyBinding>b__0()
  RVA=-1  // not resolved  token=0x600141F  System.Void <InitializePropertyBinding>b__1(T o)
END_CLASS

CLASS: <>c__DisplayClass31_1
TYPE:  sealed class
TOKEN: 0x2000450
FIELDS:
  public            T                               value  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001420  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6001421  T <InitializePropertyBinding>b__2()
END_CLASS

CLASS: <>c__DisplayClass31_2
TYPE:  sealed class
TOKEN: 0x2000451
FIELDS:
  public            System.Reflection.MethodInfo    getMethod  // 0x0
  public            UnityEngine.Component           instance  // 0x0
  public            System.Reflection.MethodInfo    setMethod  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001422  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6001423  T <InitializePropertyBinding>b__3()
  RVA=-1  // not resolved  token=0x6001424  System.Void <InitializePropertyBinding>b__5(T o)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000452
FIELDS:
  public    static readonly NodeCanvas.Framework.Variable.<>c<T><>9  // static @ 0x0
  public    static  System.Func<T>                  <>9__31_4  // static @ 0x0
  public    static  System.Action<T>                <>9__31_6  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001425  System.Void .cctor()
  RVA=-1  // not resolved  token=0x6001426  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6001427  T <InitializePropertyBinding>b__31_4()
  RVA=-1  // not resolved  token=0x6001428  System.Void <InitializePropertyBinding>b__31_6(T o)
END_CLASS

CLASS: <GetAllChildrenReferencesOfType>d__15`1
TYPE:  sealed class
TOKEN: 0x2000494
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           T                               <>2__current  // 0x0
  private           System.Int32                    <>l__initialThreadId  // 0x0
  public            ParadoxNotion.HierarchyTree.Element<>4__this  // 0x0
  private           System.Int32                    <i>5__2  // 0x0
  private           ParadoxNotion.HierarchyTree.Element<element>5__3  // 0x0
  private           System.Collections.Generic.IEnumerator<T><>7__wrap3  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<T>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6001547  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x6001548  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x6001549  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x600154A  System.Void <>m__Finally1()
  RVA=-1  // not resolved  token=0x600154C  System.Void System.Collections.IEnumerator.Reset()
  RVA=-1  // not resolved  token=0x600154E  System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
  RVA=-1  // not resolved  token=0x600154F  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Element
TYPE:  class
TOKEN: 0x2000493
SIZE:  0x28
FIELDS:
  private           System.Object                   _reference  // 0x10
  private           ParadoxNotion.HierarchyTree.Element_parent  // 0x18
  private           System.Collections.Generic.List<ParadoxNotion.HierarchyTree.Element>_children  // 0x20
PROPERTIES:
  reference  get=0x02B2ECC0
  parent  get=0x0385B100
  children  get=0x04D862C0
METHODS:
  RVA=0x053908C0  token=0x6001540  System.Void .ctor(System.Object reference)
  RVA=0x0A9806D8  token=0x6001541  ParadoxNotion.HierarchyTree.Element AddChild(ParadoxNotion.HierarchyTree.Element child)
  RVA=0x0A980854  token=0x6001542  System.Void RemoveChild(ParadoxNotion.HierarchyTree.Element child)
  RVA=0x0A980840  token=0x6001543  ParadoxNotion.HierarchyTree.Element GetRoot()
  RVA=0x0A98079C  token=0x6001544  ParadoxNotion.HierarchyTree.Element FindReferenceElement(System.Object target)
  RVA=-1  // generic def  token=0x6001545  T GetFirstParentReferenceOfType()
  RVA=-1  // generic def  token=0x6001546  System.Collections.Generic.IEnumerable<T> GetAllChildrenReferencesOfType()
END_CLASS

CLASS: MethodType
TYPE:  sealed struct
TOKEN: 0x200049A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ParadoxNotion.ReflectionTools.MethodTypeNormal  // const
  public    static  ParadoxNotion.ReflectionTools.MethodTypePropertyAccessor  // const
  public    static  ParadoxNotion.ReflectionTools.MethodTypeEvent  // const
  public    static  ParadoxNotion.ReflectionTools.MethodTypeOperator  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass33_0
TYPE:  sealed class
TOKEN: 0x200049B
SIZE:  0x20
FIELDS:
  public            System.Boolean                  includeObsolete  // 0x10
  public            System.Func<System.Type,System.Boolean><>9__2  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60015CD  System.Void .ctor()
  RVA=0x0A99A7FC  token=0x60015CE  System.Boolean <GetAllTypes>b__2(System.Type t)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200049C
SIZE:  0x10
FIELDS:
  public    static readonly ParadoxNotion.ReflectionTools.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Type,System.String><>9__33_0  // static @ 0x8
  public    static  System.Func<System.Type,System.String><>9__33_1  // static @ 0x10
  public    static  System.Func<System.Reflection.ParameterInfo,System.Type><>9__102_0  // static @ 0x18
  public    static  System.Func<System.Reflection.ParameterInfo,System.Type><>9__110_0  // static @ 0x20
METHODS:
  RVA=0x0A99AECC  token=0x60015CF  System.Void .cctor()
  RVA=0x041E1670  token=0x60015D0  System.Void .ctor()
  RVA=0x0A999FA0  token=0x60015D1  System.String <GetAllTypes>b__33_0(System.Type t)
  RVA=0x0A999FC0  token=0x60015D2  System.String <GetAllTypes>b__33_1(System.Type t)
  RVA=0x06C08908  token=0x60015D3  System.Type <RTCreateDelegate>b__102_0(System.Reflection.ParameterInfo p)
  RVA=0x06C08908  token=0x60015D4  System.Type <GetBaseDefinition>b__110_0(System.Reflection.ParameterInfo p)
END_CLASS

CLASS: <RTGetAttributesRecursive>d__99`1
TYPE:  sealed class
TOKEN: 0x200049D
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           T                               <>2__current  // 0x0
  private           System.Int32                    <>l__initialThreadId  // 0x0
  private           System.Type                     type  // 0x0
  public            System.Type                     <>3__type  // 0x0
  private           System.Type                     <current>5__2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<T>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60015D5  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x60015D6  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x60015D7  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x60015D9  System.Void System.Collections.IEnumerator.Reset()
  RVA=-1  // not resolved  token=0x60015DB  System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
  RVA=-1  // not resolved  token=0x60015DC  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <>c__DisplayClass128_0`2
TYPE:  sealed class
TOKEN: 0x200049E
FIELDS:
  public            System.Reflection.FieldInfo     info  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60015DD  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60015DE  TResult <GetFieldGetter>b__0(T instance)
END_CLASS

CLASS: <>c__DisplayClass129_0`2
TYPE:  sealed class
TOKEN: 0x200049F
FIELDS:
  public            System.Reflection.FieldInfo     info  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60015DF  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60015E0  System.Void <GetFieldSetter>b__0(T instance, TValue value)
END_CLASS

CLASS: CustomConverter
TYPE:  sealed class
TOKEN: 0x20004A1
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x096EB080  token=0x60015E5  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x015802E0  token=0x60015E6  System.Func<System.Object,System.Object> Invoke(System.Type fromType, System.Type toType)
  RVA=0x05E43E3C  token=0x60015E7  System.IAsyncResult BeginInvoke(System.Type fromType, System.Type toType, System.AsyncCallback callback, System.Object object)
  RVA=0x05397E84  token=0x60015E8  System.Func<System.Object,System.Object> EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x20004A2
SIZE:  0x18
FIELDS:
  public            System.Type                     toType  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60015E9  System.Void .ctor()
  RVA=0x0A99A868  token=0x60015EA  System.Object <Get>b__2(System.Object value)
  RVA=0x0A99A96C  token=0x60015EB  System.Object <Get>b__4(System.Object value)
  RVA=0x0A99AA18  token=0x60015EC  System.Object <Get>b__6(System.Object value)
  RVA=0x0A99AAD4  token=0x60015ED  System.Object <Get>b__7(System.Object value)
  RVA=0x0A99AB80  token=0x60015EE  System.Object <Get>b__8(System.Object value)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20004A3
SIZE:  0x10
FIELDS:
  public    static readonly ParadoxNotion.TypeConverter.<>c <>9  // static @ 0x0
  public    static  System.Func<System.Object,System.Object><>9__4_0  // static @ 0x8
  public    static  System.Func<System.Object,System.Object><>9__4_1  // static @ 0x10
  public    static  System.Func<System.Object,System.Object><>9__4_3  // static @ 0x18
  public    static  System.Func<System.Object,System.Object><>9__4_5  // static @ 0x20
  public    static  System.Func<System.Object,System.Object><>9__4_9  // static @ 0x28
  public    static  System.Func<System.Object,System.Object><>9__4_10  // static @ 0x30
  public    static  System.Func<System.Object,System.Object><>9__4_11  // static @ 0x38
  public    static  System.Func<System.Object,System.Object><>9__4_12  // static @ 0x40
  public    static  System.Func<System.Object,System.Object><>9__4_13  // static @ 0x48
  public    static  System.Func<System.Object,System.Object><>9__4_14  // static @ 0x50
  public    static  System.Func<System.Object,System.Object><>9__4_15  // static @ 0x58
  public    static  System.Func<System.Object,System.Object><>9__4_16  // static @ 0x60
METHODS:
  RVA=0x04D4C6F0  token=0x60015EF  System.Void .cctor()
  RVA=0x041E1670  token=0x60015F0  System.Void .ctor()
  RVA=0x04D88C30  token=0x60015F1  System.Object <Get>b__4_0(System.Object value)
  RVA=0x0A99A4D0  token=0x60015F2  System.Object <Get>b__4_1(System.Object value)
  RVA=0x0A99A51C  token=0x60015F3  System.Object <Get>b__4_3(System.Object value)
  RVA=0x0A99A560  token=0x60015F4  System.Object <Get>b__4_5(System.Object value)
  RVA=0x0A99A600  token=0x60015F5  System.Object <Get>b__4_9(System.Object value)
  RVA=0x0A99A044  token=0x60015F6  System.Object <Get>b__4_10(System.Object value)
  RVA=0x0A99A12C  token=0x60015F7  System.Object <Get>b__4_11(System.Object value)
  RVA=0x0A99A200  token=0x60015F8  System.Object <Get>b__4_12(System.Object value)
  RVA=0x0A99A2E0  token=0x60015F9  System.Object <Get>b__4_13(System.Object value)
  RVA=0x0A99A360  token=0x60015FA  System.Object <Get>b__4_14(System.Object value)
  RVA=0x0A99A3E0  token=0x60015FB  System.Object <Get>b__4_15(System.Object value)
  RVA=0x0A99A458  token=0x60015FC  System.Object <Get>b__4_16(System.Object value)
END_CLASS

CLASS: <>c__DisplayClass5_0
TYPE:  sealed class
TOKEN: 0x20004A9
SIZE:  0x20
FIELDS:
  public            UnityEngine.GameObject          exclude  // 0x10
  public            UnityEngine.LayerMask           mask  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6001624  System.Void .ctor()
  RVA=0x0A99AC3C  token=0x6001625  System.Boolean <FindGameObjectsWithinLayerMask>b__0(UnityEngine.GameObject x)
END_CLASS

CLASS: EventDelegate
TYPE:  sealed class
TOKEN: 0x20004AF
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x07C3F9AC  token=0x60016D6  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02ABE1A0  token=0x60016D7  System.Void Invoke(ParadoxNotion.EventData msg)
  RVA=0x0A98E898  token=0x60016D8  System.IAsyncResult BeginInvoke(ParadoxNotion.EventData msg, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x60016D9  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: EventDelegate`1
TYPE:  sealed class
TOKEN: 0x20004B0
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60016DA  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x60016DB  System.Void Invoke(ParadoxNotion.EventData<T> msg)
  RVA=-1  // runtime  token=0x60016DC  System.IAsyncResult BeginInvoke(ParadoxNotion.EventData<T> msg, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x60016DD  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: CustomEventDelegate
TYPE:  sealed class
TOKEN: 0x20004B1
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x08469FD0  token=0x60016DE  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x60016DF  System.Void Invoke(System.String name, ParadoxNotion.IEventData data)
  RVA=0x05E43E3C  token=0x60016E0  System.IAsyncResult BeginInvoke(System.String name, ParadoxNotion.IEventData data, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x60016E1  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: Message
TYPE:  sealed struct
TOKEN: 0x20004B4
SIZE:  0x30
FIELDS:
  private           System.WeakReference<System.Object>_contextRef  // 0x10
  public            UnityEngine.LogType             type  // 0x18
  public            System.String                   text  // 0x20
  public            System.String                   tag  // 0x28
PROPERTIES:
  context  get=0x0A995764  set=0x0A9957C0
METHODS:
  RVA=0x0A97EA08  token=0x60016F1  System.Boolean IsValid()
END_CLASS

CLASS: LogHandler
TYPE:  sealed class
TOKEN: 0x20004B5
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x08EFA0A4  token=0x60016F2  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B99160  token=0x60016F3  System.Boolean Invoke(ParadoxNotion.Services.Logger.Message message)
  RVA=0x0A994C9C  token=0x60016F4  System.IAsyncResult BeginInvoke(ParadoxNotion.Services.Logger.Message message, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE668  token=0x60016F5  System.Boolean EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: UpdateMode
TYPE:  sealed struct
TOKEN: 0x20004B7
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ParadoxNotion.Services.MonoManager.UpdateModeNormalUpdate  // const
  public    static  ParadoxNotion.Services.MonoManager.UpdateModeLateUpdate  // const
  public    static  ParadoxNotion.Services.MonoManager.UpdateModeFixedUpdate  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass8_0
TYPE:  sealed class
TOKEN: 0x20004B9
SIZE:  0x18
FIELDS:
  public            System.Action                   function  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600171D  System.Void .ctor()
  RVA=0x06DFA030  token=0x600171E  System.Void <StartAction>b__0()
END_CLASS

CLASS: <>c__DisplayClass9_0`1
TYPE:  sealed class
TOKEN: 0x20004BA
FIELDS:
  public            System.Action<T1>               function  // 0x0
  public            T1                              parameter1  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600171F  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6001720  System.Void <StartAction>b__0()
END_CLASS

CLASS: <>c__DisplayClass10_0`2
TYPE:  sealed class
TOKEN: 0x20004BB
FIELDS:
  public            System.Action<T1,T2>            function  // 0x0
  public            T1                              parameter1  // 0x0
  public            T2                              parameter2  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001721  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6001722  System.Void <StartAction>b__0()
END_CLASS

CLASS: <>c__DisplayClass11_0`3
TYPE:  sealed class
TOKEN: 0x20004BC
FIELDS:
  public            System.Action<T1,T2,T3>         function  // 0x0
  public            T1                              parameter1  // 0x0
  public            T2                              parameter2  // 0x0
  public            T3                              parameter3  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001723  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6001724  System.Void <StartAction>b__0()
END_CLASS

CLASS: <>c__DisplayClass12_0`1
TYPE:  sealed class
TOKEN: 0x20004BD
FIELDS:
  public            TResult                         result  // 0x0
  public            System.Func<TResult>            function  // 0x0
  public            System.Action<TResult>          callback  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001725  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6001726  System.Void <StartFunction>b__0()
  RVA=-1  // not resolved  token=0x6001727  System.Void <StartFunction>b__1()
END_CLASS

CLASS: <>c__DisplayClass13_0`2
TYPE:  sealed class
TOKEN: 0x20004BE
FIELDS:
  public            TResult                         result  // 0x0
  public            System.Func<T1,TResult>         function  // 0x0
  public            T1                              parameter1  // 0x0
  public            System.Action<TResult>          callback  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001728  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6001729  System.Void <StartFunction>b__0()
  RVA=-1  // not resolved  token=0x600172A  System.Void <StartFunction>b__1()
END_CLASS

CLASS: <>c__DisplayClass14_0`3
TYPE:  sealed class
TOKEN: 0x20004BF
FIELDS:
  public            TResult                         result  // 0x0
  public            System.Func<T1,T2,TResult>      function  // 0x0
  public            T1                              parameter1  // 0x0
  public            T2                              parameter2  // 0x0
  public            System.Action<TResult>          callback  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600172B  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600172C  System.Void <StartFunction>b__0()
  RVA=-1  // not resolved  token=0x600172D  System.Void <StartFunction>b__1()
END_CLASS

CLASS: <>c__DisplayClass15_0`4
TYPE:  sealed class
TOKEN: 0x20004C0
FIELDS:
  public            TResult                         result  // 0x0
  public            System.Func<T1,T2,T3,TResult>   function  // 0x0
  public            T1                              parameter1  // 0x0
  public            T2                              parameter2  // 0x0
  public            T3                              parameter3  // 0x0
  public            System.Action<TResult>          callback  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600172E  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600172F  System.Void <StartFunction>b__0()
  RVA=-1  // not resolved  token=0x6001730  System.Void <StartFunction>b__1()
END_CLASS

CLASS: <ThreadMonitor>d__17
TYPE:  sealed class
TOKEN: 0x20004C1
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.Threading.Thread         thread  // 0x20
  public            System.Action                   callback  // 0x28
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6001731  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6001732  System.Void System.IDisposable.Dispose()
  RVA=0x0A999E60  token=0x6001733  System.Boolean MoveNext()
  RVA=0x0A999F04  token=0x6001735  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x20004C4
FIELDS:
  public            System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData>json  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600173B  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600173C  System.Boolean <OnBeforeDeserialize>b__0(System.Collections.Generic.KeyValuePair<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> kvp)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20004C5
FIELDS:
  public    static readonly ParadoxNotion.Serialization.fsRecoveryProcessor.<>c<TCanProcess,TMissing><>9  // static @ 0x0
  public    static  System.Func<System.Collections.Generic.KeyValuePair<System.String,ParadoxNotion.Serialization.FullSerializer.fsData>,System.String><>9__3_1  // static @ 0x0
  public    static  System.Func<System.Collections.Generic.KeyValuePair<System.String,ParadoxNotion.Serialization.FullSerializer.fsData>,ParadoxNotion.Serialization.FullSerializer.fsData><>9__3_2  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600173D  System.Void .cctor()
  RVA=-1  // not resolved  token=0x600173E  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600173F  System.String <OnBeforeDeserialize>b__3_1(System.Collections.Generic.KeyValuePair<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> c)
  RVA=-1  // not resolved  token=0x6001740  ParadoxNotion.Serialization.FullSerializer.fsData <OnBeforeDeserialize>b__3_2(System.Collections.Generic.KeyValuePair<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> c)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20004CD
SIZE:  0x10
FIELDS:
  public    static readonly ParadoxNotion.Serialization.SerializedConstructorInfo.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Reflection.ParameterInfo,System.String><>9__4_0  // static @ 0x8
METHODS:
  RVA=0x0A99AE68  token=0x600176B  System.Void .cctor()
  RVA=0x041E1670  token=0x600176C  System.Void .ctor()
  RVA=0x0A99A6DC  token=0x600176D  System.String <UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize>b__4_0(System.Reflection.ParameterInfo p)
END_CLASS

CLASS: <>c__DisplayClass5_0
TYPE:  sealed class
TOKEN: 0x20004CE
SIZE:  0x18
FIELDS:
  public            System.Type[]                   parameterTypes  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600176E  System.Void .ctor()
  RVA=0x0A99ACB0  token=0x600176F  System.Boolean <UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize>b__0(System.Reflection.ConstructorInfo c)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20004D2
SIZE:  0x10
FIELDS:
  public    static readonly ParadoxNotion.Serialization.SerializedMethodInfo.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Reflection.ParameterInfo,System.String><>9__5_0  // static @ 0x8
  public    static  System.Func<System.Type,System.String><>9__5_1  // static @ 0x10
METHODS:
  RVA=0x0A99ADA0  token=0x600178A  System.Void .cctor()
  RVA=0x041E1670  token=0x600178B  System.Void .ctor()
  RVA=0x0A99A6DC  token=0x600178C  System.String <UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize>b__5_0(System.Reflection.ParameterInfo p)
  RVA=0x0A99A704  token=0x600178D  System.String <UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize>b__5_1(System.Type a)
END_CLASS

CLASS: <>c__DisplayClass6_0
TYPE:  sealed class
TOKEN: 0x20004D3
SIZE:  0x28
FIELDS:
  public            System.String                   name  // 0x10
  public            System.Type[]                   parameterTypes  // 0x18
  public            System.Boolean                  isSerializedGeneric  // 0x20
METHODS:
  RVA=0x041E1670  token=0x600178E  System.Void .ctor()
  RVA=0x0A99ACEC  token=0x600178F  System.Boolean <UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize>b__0(System.Reflection.MethodInfo m)
  RVA=0x0A99AD6C  token=0x6001790  System.Boolean <UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize>b__1(System.Reflection.MethodInfo m)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20004E4
SIZE:  0x10
FIELDS:
  public    static readonly ParadoxNotion.Serialization.FullSerializer.fsBaseConverter.<>c<>9  // static @ 0x0
  public    static  System.Func<ParadoxNotion.Serialization.FullSerializer.fsDataType,System.String><>9__6_0  // static @ 0x8
METHODS:
  RVA=0x0A99AE04  token=0x60017C0  System.Void .cctor()
  RVA=0x041E1670  token=0x60017C1  System.Void .ctor()
  RVA=0x0A999F50  token=0x60017C2  System.String <FailExpectedType>b__6_0(ParadoxNotion.Serialization.FullSerializer.fsDataType t)
END_CLASS

CLASS: ObjectReferenceEqualityComparator
TYPE:  class
TOKEN: 0x20004E7
SIZE:  0x10
IMPLEMENTS: System.Collections.Generic.IEqualityComparer`1
FIELDS:
  public    static readonly System.Collections.Generic.IEqualityComparer<System.Object>Instance  // static @ 0x0
METHODS:
  RVA=0x0829C278  token=0x60017CE  System.Boolean System.Collections.Generic.IEqualityComparer<System.Object>.Equals(System.Object x, System.Object y)
  RVA=0x04DA3AC0  token=0x60017CF  System.Int32 System.Collections.Generic.IEqualityComparer<System.Object>.GetHashCode(System.Object obj)
  RVA=0x041E1670  token=0x60017D0  System.Void .ctor()
  RVA=0x04D4DEF0  token=0x60017D1  System.Void .cctor()
END_CLASS

CLASS: ObjectGenerator
TYPE:  sealed class
TOKEN: 0x20004F1
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x03604CC0  token=0x6001840  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x07378308  token=0x6001841  System.Object Invoke()
  RVA=0x053DED94  token=0x6001842  System.IAsyncResult BeginInvoke(System.AsyncCallback callback, System.Object object)
  RVA=0x05397E84  token=0x6001843  System.Object EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: <>c__DisplayClass21_0
TYPE:  sealed class
TOKEN: 0x20004F2
SIZE:  0x18
FIELDS:
  public            System.Type                     reflectedType  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6001844  System.Void .ctor()
  RVA=0x0A99A724  token=0x6001845  System.Object <GetGenerator>b__0()
  RVA=0x0A99A7BC  token=0x6001846  System.Object <GetGenerator>b__1()
  RVA=0x0A99A7CC  token=0x6001847  System.Object <GetGenerator>b__2()
  RVA=0x040E3BB0  token=0x6001848  System.Object <GetGenerator>b__4()
  RVA=0x036050C0  token=0x6001849  System.Object <GetGenerator>b__5()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20004F3
SIZE:  0x10
FIELDS:
  public    static readonly ParadoxNotion.Serialization.FullSerializer.fsMetaType.<>c<>9  // static @ 0x0
  public    static  ParadoxNotion.Serialization.FullSerializer.fsMetaType.ObjectGenerator<>9__21_3  // static @ 0x8
METHODS:
  RVA=0x0A99AF30  token=0x600184A  System.Void .cctor()
  RVA=0x041E1670  token=0x600184B  System.Void .ctor()
  RVA=0x0A99A00C  token=0x600184C  System.Object <GetGenerator>b__21_3()
END_CLASS

CLASS: fsLazyCycleDefinitionWriter
TYPE:  class
TOKEN: 0x20004F7
SIZE:  0x20
FIELDS:
  private           System.Collections.Generic.Dictionary<System.Int32,ParadoxNotion.Serialization.FullSerializer.fsData>_pendingDefinitions  // 0x10
  private           System.Collections.Generic.HashSet<System.Int32>_references  // 0x18
METHODS:
  RVA=0x0A9A6628  token=0x6001889  System.Void WriteDefinition(System.Int32 id, ParadoxNotion.Serialization.FullSerializer.fsData data)
  RVA=0x0A9A675C  token=0x600188A  System.Void WriteReference(System.Int32 id, System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> dict)
  RVA=0x031147D0  token=0x600188B  System.Void Clear()
  RVA=0x044D8750  token=0x600188C  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20004F8
SIZE:  0x10
FIELDS:
  public    static readonly ParadoxNotion.Serialization.FullSerializer.fsSerializer.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Reflection.MethodInfo,System.Boolean><>9__58_0  // static @ 0x8
METHODS:
  RVA=0x0A9A4500  token=0x600188D  System.Void .cctor()
  RVA=0x041E1670  token=0x600188E  System.Void .ctor()
  RVA=0x0A9A44AC  token=0x600188F  System.Boolean <TryDeserializeMigration>b__58_0(System.Reflection.MethodInfo m)
END_CLASS

CLASS: Beyond.SourceGenerator.CameraControlConfigAttribute
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000008  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardUtility
TYPE:  static class
TOKEN: 0x2000006
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_enableFastPathBuffer  // static @ 0x0
METHODS:
  RVA=-1  // generic def  token=0x6000009  System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity)
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardAttribute
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600000A  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.DataNAttribute
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public            System.Int32                    capacity  // 0x10
  public            System.Int32                    tSize  // 0x14
  public            System.Boolean                  ring  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600000B  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.ECSComponentAttribute
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public            System.Runtime.InteropServices.LayoutKindlayoutKind  // 0x10
  public            System.Boolean                  isTag  // 0x14
METHODS:
  RVA=0x041E1670  token=0x600000C  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.BinderConnection`1
TYPE:  class
TOKEN: 0x200000A
EXTENDS: FlowCanvas.BinderConnection
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600000D  System.Void Bind()
  RVA=-1  // not resolved  token=0x600000E  System.Void UnBind()
  RVA=-1  // not resolved  token=0x600000F  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.BinderConnection
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x50
EXTENDS: NodeCanvas.Framework.Connection
FIELDS:
  private           System.String                   _sourcePortID  // 0x30
  private           System.String                   _targetPortID  // 0x38
  private           FlowCanvas.Port                 _sourcePort  // 0x40
  private           FlowCanvas.Port                 _targetPort  // 0x48
PROPERTIES:
  sourcePortID  get=0x0A912E04  set=0x05396200
  targetPortID  get=0x0A912EEC  set=0x046A91B0
  sourcePort  get=0x0A912E40
  targetPort  get=0x0A912F28
  bindingType  get=0x0A912D34
METHODS:
  RVA=0x0A911ACC  token=0x6000017  FlowCanvas.BinderConnection Create(FlowCanvas.Port source, FlowCanvas.Port target, System.Boolean recodeUndo, System.Boolean invokeGraphChanged)
  RVA=0x0A9129F0  token=0x6000018  System.Void SetSourcePort(FlowCanvas.Port newSourcePort)
  RVA=0x0A912B4C  token=0x6000019  System.Void SetTargetPort(FlowCanvas.Port newTargetPort)
  RVA=0x0A911FE8  token=0x600001A  System.Void GatherAndValidateSourcePort()
  RVA=0x0A9123D0  token=0x600001B  System.Void GatherAndValidateTargetPort()
  RVA=0x0A911AAC  token=0x600001C  System.Boolean CanBeBound(FlowCanvas.Port source, FlowCanvas.Port target, FlowCanvas.BinderConnection refConnection)
  RVA=0x0A911A80  token=0x600001D  System.Boolean CanBeBoundVerbosed(FlowCanvas.Port source, FlowCanvas.Port target, FlowCanvas.BinderConnection refConnection, System.String& verbose)
  RVA=0x0A911808  token=0x600001E  System.String CanBeBoundVerbosed_Internal(FlowCanvas.Port source, FlowCanvas.Port target, FlowCanvas.BinderConnection refConnection)
  RVA=0x0A9128E8  token=0x600001F  System.Void OnDestroy()
  RVA=0x0A912854  token=0x6000020  System.Void OnDestroyAfterConnectionRemoved()
  RVA=0x0A9116E8  token=0x6000021  System.Void Bind()
  RVA=0x0A912CA8  token=0x6000022  System.Void UnBind()
  RVA=0x04D92320  token=0x6000023  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.FlowHandler
TYPE:  sealed class
TOKEN: 0x200000D
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x07C3FC04  token=0x6000028  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x0622185C  token=0x6000029  System.Void Invoke(FlowCanvas.Flow f)
  RVA=0x0A918CD0  token=0x600002A  System.IAsyncResult BeginInvoke(FlowCanvas.Flow f, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x600002B  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: FlowCanvas.ValueHandler`1
TYPE:  sealed class
TOKEN: 0x200000E
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x600002C  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600002D  T Invoke()
  RVA=-1  // runtime  token=0x600002E  System.IAsyncResult BeginInvoke(System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x600002F  T EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: FlowCanvas.ValueHandlerObject
TYPE:  sealed class
TOKEN: 0x200000F
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x07378318  token=0x6000030  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x07378308  token=0x6000031  System.Object Invoke()
  RVA=0x053DED94  token=0x6000032  System.IAsyncResult BeginInvoke(System.AsyncCallback callback, System.Object object)
  RVA=0x05397E84  token=0x6000033  System.Object EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: FlowCanvas.FlowBreak
TYPE:  sealed class
TOKEN: 0x2000010
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x053DEDC8  token=0x6000034  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x053DEDB8  token=0x6000035  System.Void Invoke()
  RVA=0x053DED94  token=0x6000036  System.IAsyncResult BeginInvoke(System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000037  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: FlowCanvas.FlowReturn
TYPE:  sealed class
TOKEN: 0x2000011
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x074FF394  token=0x6000038  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000039  System.Void Invoke(System.Object value)
  RVA=0x047FDD70  token=0x600003A  System.IAsyncResult BeginInvoke(System.Object value, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x600003B  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: FlowCanvas.Flow
TYPE:  sealed struct
TOKEN: 0x2000012
SIZE:  0x38
FIELDS:
  private           System.Int32                    <ticks>k__BackingField  // 0x10
  private           System.Collections.Generic.Dictionary<System.String,System.Object>parameters  // 0x18
  private           FlowCanvas.Flow.ReturnData      returnData  // 0x20
  private           FlowCanvas.FlowBreak            breakCall  // 0x30
PROPERTIES:
  ticks  get=0x02B77620  set=0x04D86140
  New  get=0x04D9D920
METHODS:
  RVA=0x0A91C648  token=0x600003F  System.Void Call(FlowCanvas.FlowOutput port)
  RVA=-1  // generic def  token=0x6000040  T ReadParameter(System.String name)
  RVA=-1  // generic def  token=0x6000041  System.Void WriteParameter(System.String name, T value)
  RVA=0x0A91C830  token=0x6000042  System.Void SetReturnData(FlowCanvas.FlowReturn call, System.Type expectedType)
  RVA=0x0A91C6A8  token=0x6000043  System.Void Return(System.Object value, FlowCanvas.FlowNode context)
  RVA=0x05390F40  token=0x6000044  System.Void BeginBreakBlock(FlowCanvas.FlowBreak callback)
  RVA=0x0A91C68C  token=0x6000045  System.Void EndBreakBlock()
  RVA=0x0A91C5F0  token=0x6000046  System.Void Break(FlowCanvas.FlowNode context)
END_CLASS

CLASS: FlowCanvas.FlowGraph
TYPE:  abstract class
TOKEN: 0x2000014
SIZE:  0xC0
EXTENDS: NodeCanvas.Framework.Graph
FIELDS:
  private           System.Collections.Generic.List<NodeCanvas.Framework.IUpdatable>updatableNodes  // 0xa0
  private           System.Collections.Generic.List<FlowCanvas.Macros.MacroNodeWrapper>macroWrappers  // 0xa8
  private           System.Collections.Generic.Dictionary<System.String,NodeCanvas.Framework.IInvokable>functions  // 0xb0
  private           System.Collections.Generic.Dictionary<System.Type,UnityEngine.Component>cachedAgentComponents  // 0xb8
PROPERTIES:
  baseNodeType  get=0x0A918C80
  allowBlackboardOverrides  get=0x02FFF600
  requiresAgent  get=0x012081B0
  requiresPrimeNode  get=0x012081B0
  isTree  get=0x012081B0
  canAcceptVariableDrops  get=0x02FFF600
METHODS:
  RVA=-1  // generic def  token=0x6000052  T CallFunction(System.String name, System.Object[] args)
  RVA=0x0A918120  token=0x6000053  System.Object CallFunction(System.String name, System.Object[] args)
  RVA=0x0A918064  token=0x6000054  System.Void CallFunctionAsync(System.String name, System.Action<System.Object> callback, System.Object[] args)
  RVA=0x0A9181D8  token=0x6000055  UnityEngine.Object GetAgentComponent(System.Type type)
  RVA=0x0A918578  token=0x6000056  System.Void OnGraphInitialize()
  RVA=0x0A918478  token=0x6000057  System.Void InitSecondPass()
  RVA=0x0A91896C  token=0x6000058  System.Void OnGraphStarted()
  RVA=0x0A918BA0  token=0x6000059  System.Void OnGraphUpdate()
  RVA=0x0A918A7C  token=0x600005A  System.Void OnGraphStoped()
  RVA=0x0A918C34  token=0x600005B  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.FlowNode
TYPE:  abstract class
TOKEN: 0x2000015
SIZE:  0xB0
EXTENDS: NodeCanvas.Framework.Node
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           System.Collections.Generic.Dictionary<System.String,System.Object>_inputPortValues  // 0x98
  protected         System.Collections.Generic.Dictionary<System.String,FlowCanvas.Port>inputPorts  // 0xa0
  protected         System.Collections.Generic.Dictionary<System.String,FlowCanvas.Port>outputPorts  // 0xa8
PROPERTIES:
  allowDragInContentRect  get=0x012081B0
  overrideNodeWidth  get=0x0A91C200
  maxInConnections  get=0x04DA2BB0
  maxOutConnections  get=0x04DA2BB0
  allowAsPrime  get=0x012081B0
  canSelfConnect  get=0x012081B0
  outConnectionType  get=0x0A91C1B0
  commentsAlignment  get=0x04D882B0
  iconAlignment  get=0x04D85B80
  flowGraph  get=0x0A91C174
  SPACE  get=0x0A91C148
  ignoreSelfInstancePortAssignment  get=0x012081B0
METHODS:
  RVA=0x0A91BD78  token=0x6000068  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x041E1670  token=0x6000069  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x0A91A7C4  token=0x600006A  System.Void OnValidate(NodeCanvas.Framework.Graph flowGraph)
  RVA=0x041E1670  token=0x600006B  System.Void OnParentConnected(System.Int32 i)
  RVA=0x041E1670  token=0x600006C  System.Void OnChildConnected(System.Int32 i)
  RVA=0x041E1670  token=0x600006D  System.Void OnParentDisconnected(System.Int32 i)
  RVA=0x041E1670  token=0x600006E  System.Void OnChildDisconnected(System.Int32 i)
  RVA=0x0A91A75C  token=0x600006F  System.Void OnPortConnected(FlowCanvas.Port port, FlowCanvas.Port otherPort)
  RVA=0x0A91A7BC  token=0x6000070  System.Void OnPortDisconnected(FlowCanvas.Port port, FlowCanvas.Port otherPort)
  RVA=0x041E1670  token=0x6000071  System.Void OnPortDisconnectedLate(FlowCanvas.Port port, FlowCanvas.Port otherPort)
  RVA=0x0A91970C  token=0x6000072  System.Void BindPorts()
  RVA=0x0A91BCC4  token=0x6000073  System.Void UnBindPorts()
  RVA=0x0A91A324  token=0x6000074  FlowCanvas.Port GetInputPort(System.String ID)
  RVA=0x0A91A59C  token=0x6000075  FlowCanvas.Port GetOutputPort(System.String ID)
  RVA=0x0A919E74  token=0x6000076  System.Collections.Generic.IEnumerable<FlowCanvas.Port> GetAllPorts()
  RVA=0x0A91A534  token=0x6000077  System.Collections.Generic.IEnumerable<FlowCanvas.FlowOutput> GetOutputFlowPorts()
  RVA=0x0A91A6F4  token=0x6000078  System.Collections.Generic.IEnumerable<FlowCanvas.ValueOutput> GetOutputValuePorts()
  RVA=0x0A91A2BC  token=0x6000079  System.Collections.Generic.IEnumerable<FlowCanvas.FlowInput> GetInputFlowPorts()
  RVA=0x0A91A47C  token=0x600007A  System.Collections.Generic.IEnumerable<FlowCanvas.ValueInput> GetInputValuePorts()
  RVA=0x0A919F04  token=0x600007B  FlowCanvas.Port GetFirstInputOfType(System.Type type)
  RVA=0x0A91A0E0  token=0x600007C  FlowCanvas.Port GetFirstOutputOfType(System.Type type)
  RVA=0x0A919528  token=0x600007D  System.Void AssignSelfInstancePort()
  RVA=0x0A919CF0  token=0x600007E  System.Void GatherPorts()
  RVA=-1  // abstract  token=0x600007F  System.Void RegisterPorts()
  RVA=0x041E1670  token=0x6000080  System.Void ValidateConnections()
  RVA=0x0A9198D0  token=0x6000081  System.Void DeserializeInputPortValues()
  RVA=0x041E1670  token=0x6000082  System.Void RefreshEditorPorts()
  RVA=0x0A918DD0  token=0x6000083  FlowCanvas.FlowInput AddFlowInput(System.String name, System.String ID, FlowCanvas.FlowHandler pointer)
  RVA=0x0A918DE8  token=0x6000084  FlowCanvas.FlowInput AddFlowInput(System.String name, FlowCanvas.FlowHandler pointer, System.String ID)
  RVA=0x0A918ED0  token=0x6000085  FlowCanvas.FlowOutput AddFlowOutput(System.String name, System.String ID)
  RVA=-1  // generic def  token=0x6000086  FlowCanvas.ValueInput<T> AddValueInput(System.String name, System.String ID)
  RVA=-1  // generic def  token=0x6000087  FlowCanvas.ValueOutput<T> AddValueOutput(System.String name, System.String ID, FlowCanvas.ValueHandler<T> getter)
  RVA=-1  // generic def  token=0x6000088  FlowCanvas.ValueOutput<T> AddValueOutput(System.String name, FlowCanvas.ValueHandler<T> getter, System.String ID)
  RVA=0x0A919070  token=0x6000089  System.Void AddValueInput(System.Type valueType, System.String name, System.String ID)
  RVA=-1  // generic def  token=0x600008A  FlowCanvas.ValueOutput<T> AddValueOutput(System.String name, System.String ID)
  RVA=0x0A919354  token=0x600008B  System.Void AddValueOutput(System.Type valueType, System.String name, System.String ID)
  RVA=0x0A919244  token=0x600008C  FlowCanvas.ValueInput AddValueInput(System.String name, System.String ID, System.Type type)
  RVA=0x0A918FA8  token=0x600008D  FlowCanvas.ValueInput AddValueInput(System.String name, System.Type type, System.String ID)
  RVA=0x0A91925C  token=0x600008E  FlowCanvas.ValueOutput AddValueOutput(System.String name, System.String ID, System.Type type, FlowCanvas.ValueHandlerObject getter)
  RVA=0x0A91927C  token=0x600008F  FlowCanvas.ValueOutput AddValueOutput(System.String name, System.Type type, FlowCanvas.ValueHandlerObject getter, System.String ID)
  RVA=0x0A91A7CC  token=0x6000090  System.Void QualifyPortNameAndID(System.String& name, System.String& ID, System.Collections.IDictionary dict)
  RVA=0x0A9197C0  token=0x6000091  System.Boolean CheckReverseIDEquality(FlowCanvas.Port port, System.String requestedID)
  RVA=0x0A91B5D0  token=0x6000092  System.Void TryAddReflectionBasedRegistrationForObject(System.Object instance)
  RVA=0x0A91B124  token=0x6000093  FlowCanvas.FlowInput TryAddMethodFlowInput(System.Reflection.MethodInfo method, System.Object instance)
  RVA=0x0A91ABBC  token=0x6000094  FlowCanvas.FlowOutput TryAddFieldDelegateFlowOutput(System.Reflection.FieldInfo field, System.Object instance)
  RVA=0x0A91AD30  token=0x6000095  FlowCanvas.ValueInput TryAddFieldDelegateValueInput(System.Reflection.FieldInfo field, System.Object instance)
  RVA=0x0A91B2D8  token=0x6000096  FlowCanvas.ValueOutput TryAddPropertyValueOutput(System.Reflection.PropertyInfo prop, System.Object instance)
  RVA=0x0A91A884  token=0x6000097  FlowCanvas.FlowNode ReplaceWith(System.Type t)
  RVA=0x0A91BC10  token=0x6000098  System.Void TryRemovePortIfMissingAndClean(FlowCanvas.Port port)
  RVA=0x0A91A4E4  token=0x6000099  System.Type GetNodeWildDefinitionType()
  RVA=0x0A91BB68  token=0x600009A  System.Void TryHandleWildPortConnection(System.Type currentType, System.Type targetType)
  RVA=0x0A91B944  token=0x600009B  System.Type TryGetNewGenericTypeForWild(System.Type wildType, System.Type currentType, System.Type targetType, System.Type content, System.Type context)
  RVA=0x0A91B7A4  token=0x600009C  System.Reflection.MethodInfo TryGetNewGenericMethodForWild(System.Type wildType, System.Type currentType, System.Type targetType, System.Reflection.MethodInfo content)
  RVA=0x0489BC70  token=0x600009D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.FlowNodeNested`1
TYPE:  abstract class
TOKEN: 0x2000020
EXTENDS: FlowCanvas.FlowNode
IMPLEMENTS: NodeCanvas.Framework.IGraphAssignable`1 NodeCanvas.Framework.IGraphAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           System.Collections.Generic.List<NodeCanvas.Framework.Internal.BBMappingParameter>_variablesMap  // 0x0
  private           T                               <currentInstance>k__BackingField  // 0x0
  private           System.Collections.Generic.Dictionary<NodeCanvas.Framework.Graph,NodeCanvas.Framework.Graph><instances>k__BackingField  // 0x0
PROPERTIES:
  subGraph  get=-1  // abstract  set=-1  // abstract
  subGraphParameter  get=-1  // abstract
  currentInstance  get=-1  // not resolved  set=-1  // not resolved
  instances  get=-1  // not resolved  set=-1  // not resolved
  variablesMap  get=-1  // not resolved  set=-1  // not resolved
  NodeCanvas.Framework.IGraphAssignable.subGraph  get=-1  // not resolved  set=-1  // not resolved
  NodeCanvas.Framework.IGraphAssignable.currentInstance  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000C0  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.FlowScript
TYPE:  class
TOKEN: 0x2000021
SIZE:  0xC0
EXTENDS: FlowCanvas.FlowScriptBase
FIELDS:
PROPERTIES:
  canDelayDeserializeAndOptimizeClone  get=0x02FFF600
METHODS:
  RVA=0x0A91C444  token=0x60000C2  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.FlowScriptBase
TYPE:  abstract class
TOKEN: 0x2000022
SIZE:  0xC0
EXTENDS: FlowCanvas.FlowGraph
FIELDS:
METHODS:
  RVA=0x0A91C444  token=0x60000C3  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.FlowScriptController
TYPE:  class
TOKEN: 0x2000023
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.GraphOwner`1
FIELDS:
METHODS:
  RVA=0x0A91C4D0  token=0x60000C4  System.Void CallFunction(System.String name)
  RVA=0x0A91C54C  token=0x60000C5  System.Object CallFunction(System.String name, System.Object[] args)
  RVA=0x0A91C44C  token=0x60000C6  System.Void CallFunctionAsync(System.String name, System.Action<System.Object> callback, System.Object[] args)
  RVA=0x0A91C5BC  token=0x60000C7  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.IEditorMenuCallbackReceiver
TYPE:  interface
TOKEN: 0x2000024
FIELDS:
METHODS:
END_CLASS

CLASS: FlowCanvas.RenderImageEvent
TYPE:  class
TOKEN: 0x2000025
SIZE:  0xE0
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           fOut  // 0xc8
  private           UnityEngine.RenderTexture       t1  // 0xd0
  private           UnityEngine.RenderTexture       t2  // 0xd8
METHODS:
  RVA=0x0A921BF8  token=0x60000C8  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A921C88  token=0x60000C9  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A921A20  token=0x60000CA  System.Void OnRenderImage(UnityEngine.RenderTexture source, UnityEngine.RenderTexture dest)
  RVA=0x0A921A9C  token=0x60000CB  System.Void RegisterPorts()
  RVA=0x0A921D18  token=0x60000CC  System.Void .ctor()
  RVA=0x04D876B0  token=0x60000CD  UnityEngine.RenderTexture <RegisterPorts>b__6_0()
  RVA=0x04D88EF0  token=0x60000CE  UnityEngine.RenderTexture <RegisterPorts>b__6_1()
END_CLASS

CLASS: FlowCanvas.Port
TYPE:  abstract class
TOKEN: 0x2000026
SIZE:  0x40
FIELDS:
  private           System.Boolean                  <prettifyName>k__BackingField  // 0x10
  private           System.Boolean                  <forceNotDraw>k__BackingField  // 0x11
  private           FlowCanvas.FlowNode             <parent>k__BackingField  // 0x18
  private           System.String                   <ID>k__BackingField  // 0x20
  private           System.String                   <name>k__BackingField  // 0x28
  private           System.Int32                    <connections>k__BackingField  // 0x30
  private           FlowCanvas.Port.BindStatus      <bindStatus>k__BackingField  // 0x34
  private           UnityEngine.GUIContent          <displayContent>k__BackingField  // 0x38
PROPERTIES:
  prettifyName  get=0x015EFCE0  set=0x04D86130
  forceNotDraw  get=0x04D88380  set=0x04D88460
  parent  get=0x0385B100  set=0x05392C40
  ID  get=0x04D862C0  set=0x05390F40
  name  get=0x04D86240  set=0x02FDB880
  connections  get=0x04D864F0  set=0x04D86740
  isConnected  get=0x0A921A18
  bindStatus  get=0x04D86730  set=0x04D86760
  displayContent  get=0x04D85A50  set=0x046A91B0
  type  get=-1  // abstract
METHODS:
  RVA=0x015EFCD0  token=0x60000CF  System.Void .ctor()
  RVA=0x0A9219DC  token=0x60000D4  System.Void .ctor(FlowCanvas.FlowNode parent, System.String name, System.String ID)
  RVA=0x0A9211D0  token=0x60000E3  FlowCanvas.Port FlagMissing()
  RVA=0x0A9211A8  token=0x60000E4  FlowCanvas.Port FlagInvalidCast()
  RVA=0x0A9211F8  token=0x60000E5  FlowCanvas.Port FlagValid()
  RVA=0x0A9210F4  token=0x60000E6  System.Boolean CanAcceptConnections()
  RVA=0x0A92139C  token=0x60000E7  System.Collections.Generic.IEnumerable<FlowCanvas.BinderConnection> GetPortConnections()
  RVA=0x0A92121C  token=0x60000E8  FlowCanvas.BinderConnection GetFirstInputConnection()
  RVA=0x0A9212DC  token=0x60000E9  FlowCanvas.BinderConnection GetFirstOutputConnection()
  RVA=0x0A9215A0  token=0x60000EA  System.Boolean IsFlowPort()
  RVA=0x0A921854  token=0x60000EB  System.Boolean IsValuePort()
  RVA=0x0A921604  token=0x60000EC  System.Boolean IsInputPort()
  RVA=0x0A921668  token=0x60000ED  System.Boolean IsOutputPort()
  RVA=0x0A9216CC  token=0x60000EE  System.Boolean IsUnityObject()
  RVA=0x0A92176C  token=0x60000EF  System.Boolean IsUnitySceneObject()
  RVA=0x0A9214B0  token=0x60000F0  System.Boolean IsDelegate()
  RVA=0x0A921550  token=0x60000F1  System.Boolean IsEnumerableCollection()
  RVA=0x0A9218B8  token=0x60000F2  System.Boolean IsWild()
  RVA=0x0A92192C  token=0x60000F3  System.String ToString()
  RVA=0x0A92197C  token=0x60000F4  System.Boolean <GetPortConnections>b__43_0(FlowCanvas.BinderConnection c)
  RVA=0x0A9219AC  token=0x60000F5  System.Boolean <GetPortConnections>b__43_1(FlowCanvas.BinderConnection c)
  RVA=0x0A92197C  token=0x60000F6  System.Boolean <GetFirstInputConnection>b__44_0(FlowCanvas.BinderConnection c)
  RVA=0x0A9219AC  token=0x60000F7  System.Boolean <GetFirstOutputConnection>b__45_0(FlowCanvas.BinderConnection c)
END_CLASS

CLASS: FlowCanvas.FlowInput
TYPE:  class
TOKEN: 0x2000028
SIZE:  0x48
EXTENDS: FlowCanvas.Port
FIELDS:
  private           FlowCanvas.FlowHandler          <pointer>k__BackingField  // 0x40
PROPERTIES:
  pointer  get=0x04D85A60  set=0x042B4AE0
  type  get=0x0A918D80
METHODS:
  RVA=0x0A918D50  token=0x60000F8  System.Void .ctor(FlowCanvas.FlowNode parent, System.String name, System.String ID, FlowCanvas.FlowHandler pointer)
END_CLASS

CLASS: FlowCanvas.FlowOutput
TYPE:  class
TOKEN: 0x2000029
SIZE:  0x48
EXTENDS: FlowCanvas.Port
FIELDS:
  private           FlowCanvas.FlowHandler          pointer  // 0x40
PROPERTIES:
  type  get=0x0A91C360
EVENTS:
  pointer  add=add_pointer  remove=remove_pointer
METHODS:
  RVA=0x09447C84  token=0x60000FC  System.Void .ctor(FlowCanvas.FlowNode parent, System.String name, System.String ID)
  RVA=0x0A91C26C  token=0x6000100  System.Void Call(FlowCanvas.Flow f)
  RVA=0x0A91C248  token=0x6000101  System.Void BindTo(FlowCanvas.FlowInput target)
  RVA=0x0A91C240  token=0x6000102  System.Void Append(FlowCanvas.FlowHandler callback)
  RVA=0x09AFF570  token=0x6000103  System.Void UnBind()
END_CLASS

CLASS: FlowCanvas.ValueInput
TYPE:  abstract class
TOKEN: 0x200002A
SIZE:  0x48
EXTENDS: FlowCanvas.Port
FIELDS:
  private           System.Boolean                  <skipSelfInstanceAssignment>k__BackingField  // 0x40
PROPERTIES:
  value  get=0x0A928BA0
  skipSelfInstanceAssignment  get=0x02D480A0  set=0x02D480B0
  defaultValue  get=-1  // abstract  set=-1  // abstract
  serializedValue  get=-1  // abstract  set=-1  // abstract
  isDefaultValue  get=-1  // abstract
  type  get=-1  // abstract
METHODS:
  RVA=0x09447C84  token=0x6000104  System.Void .ctor(FlowCanvas.FlowNode parent, System.String name, System.String ID)
  RVA=-1  // generic def  token=0x6000105  FlowCanvas.ValueInput<T> CreateInstance(FlowCanvas.FlowNode parent, System.String name, System.String ID)
  RVA=0x0A9289CC  token=0x6000106  FlowCanvas.ValueInput CreateInstance(System.Type t, FlowCanvas.FlowNode parent, System.String name, System.String ID)
  RVA=0x0A928B60  token=0x6000108  FlowCanvas.ValueInput SetDefaultAndSerializedValue(System.Object v)
  RVA=0x04DA6450  token=0x6000109  FlowCanvas.ValueInput SkipSelfInstanceAssignment(System.Boolean skip)
  RVA=-1  // abstract  token=0x6000112  System.Void BindTo(FlowCanvas.ValueOutput target)
  RVA=-1  // abstract  token=0x6000113  System.Void UnBind()
  RVA=-1  // abstract  token=0x6000114  System.Object GetObjectValue()
END_CLASS

CLASS: FlowCanvas.ValueInput`1
TYPE:  class
TOKEN: 0x200002B
EXTENDS: FlowCanvas.ValueInput
FIELDS:
  private           FlowCanvas.ValueHandler<T>      getter  // 0x0
  private           System.Action<T>                callback  // 0x0
  private           T                               _value  // 0x0
  private           T                               _defaultValue  // 0x0
PROPERTIES:
  value  get=-1  // not resolved
  defaultValue  get=-1  // not resolved  set=-1  // not resolved
  serializedValue  get=-1  // not resolved  set=-1  // not resolved
  isDefaultValue  get=-1  // not resolved
  type  get=-1  // not resolved
EVENTS:
  getter  add=add_getter  remove=remove_getter
METHODS:
  RVA=-1  // not resolved  token=0x6000115  System.Void .ctor(FlowCanvas.FlowNode parent, System.String name, System.String ID)
  RVA=-1  // not resolved  token=0x600011F  FlowCanvas.ValueInput<T> SetDefaultAndSerializedValue(T v)
  RVA=-1  // not resolved  token=0x6000120  T GetValue()
  RVA=-1  // not resolved  token=0x6000121  System.Object GetObjectValue()
  RVA=-1  // not resolved  token=0x6000122  System.Void BindTo(FlowCanvas.ValueOutput source)
  RVA=-1  // not resolved  token=0x6000123  System.Void Append(System.Action<T> callback)
  RVA=-1  // not resolved  token=0x6000124  System.Void UnBind()
  RVA=-1  // not resolved  token=0x6000125  T op_Explicit(FlowCanvas.ValueInput<T> port)
END_CLASS

CLASS: FlowCanvas.ValueOutput
TYPE:  abstract class
TOKEN: 0x200002C
SIZE:  0x40
EXTENDS: FlowCanvas.Port
FIELDS:
METHODS:
  RVA=0x09447C84  token=0x6000126  System.Void .ctor(FlowCanvas.FlowNode parent, System.String name, System.String ID)
  RVA=-1  // generic def  token=0x6000127  FlowCanvas.ValueOutput<T> CreateInstance(FlowCanvas.FlowNode parent, System.String name, System.String ID, FlowCanvas.ValueHandler<T> getter)
  RVA=0x0A928BB0  token=0x6000128  FlowCanvas.ValueOutput CreateInstance(System.Type t, FlowCanvas.FlowNode parent, System.String name, System.String ID, FlowCanvas.ValueHandlerObject getter)
  RVA=-1  // abstract  token=0x6000129  System.Object GetObjectValue()
END_CLASS

CLASS: FlowCanvas.ValueOutput`1
TYPE:  class
TOKEN: 0x200002D
EXTENDS: FlowCanvas.ValueOutput
FIELDS:
  private           FlowCanvas.ValueHandler<T>      <getter>k__BackingField  // 0x0
PROPERTIES:
  getter  get=-1  // not resolved  set=-1  // not resolved
  type  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600012A  System.Void .ctor(FlowCanvas.FlowNode parent, System.String name, System.String ID, FlowCanvas.ValueHandler<T> getter)
  RVA=-1  // not resolved  token=0x600012B  System.Void .ctor(FlowCanvas.FlowNode parent, System.String name, System.String ID, FlowCanvas.ValueHandlerObject getter)
  RVA=-1  // not resolved  token=0x600012C  System.Void .ctor(FlowCanvas.FlowNode parent, System.String name, System.String ID)
  RVA=-1  // not resolved  token=0x600012F  System.Object GetObjectValue()
  RVA=-1  // not resolved  token=0x6000131  T op_Explicit(FlowCanvas.ValueOutput<T> port)
END_CLASS

CLASS: FlowCanvas.TypeConverter
TYPE:  static class
TOKEN: 0x200002F
SIZE:  0x10
FIELDS:
  private   static  FlowCanvas.TypeConverter.CustomConvertercustomConverter  // static @ 0x0
  private   static  FlowCanvas.TypeConverter.CustomCanConvertcustomCanConvert  // static @ 0x8
EVENTS:
  customConverter  add=add_customConverter  remove=remove_customConverter
  customCanConvert  add=add_customCanConvert  remove=remove_customCanConvert
METHODS:
  RVA=-1  // generic def  token=0x6000138  FlowCanvas.ValueHandler<T> GetConverterFuncFromTo(System.Type sourceType, System.Type targetType, FlowCanvas.ValueHandler<System.Object> func)
  RVA=0x0A924C4C  token=0x6000139  System.Boolean HasConvertion(System.Type sourceType, System.Type targetType)
  RVA=-1  // generic def  token=0x600013A  T QuickConvert(System.Object obj)
  RVA=0x0A924CF8  token=0x600013B  System.Object QuickConvert(System.Object obj, System.Type type)
END_CLASS

CLASS: FlowCanvas.Wild
TYPE:  class
TOKEN: 0x2000036
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600015A  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Macros.Macro
TYPE:  class
TOKEN: 0x2000274
SIZE:  0x100
EXTENDS: FlowCanvas.FlowScriptBase
FIELDS:
  public            System.Collections.Generic.List<ParadoxNotion.DynamicParameterDefinition>inputDefinitions  // 0xc0
  public            System.Collections.Generic.List<ParadoxNotion.DynamicParameterDefinition>outputDefinitions  // 0xc8
  public            System.Collections.Generic.Dictionary<System.String,FlowCanvas.FlowHandler>entryActionMap  // 0xd0
  public            System.Collections.Generic.Dictionary<System.String,FlowCanvas.FlowHandler>exitActionMap  // 0xd8
  public            System.Collections.Generic.Dictionary<System.String,FlowCanvas.ValueHandlerObject>entryFunctionMap  // 0xe0
  public            System.Collections.Generic.Dictionary<System.String,FlowCanvas.ValueHandlerObject>exitFunctionMap  // 0xe8
  private           FlowCanvas.Macros.MacroInputNode_entry  // 0xf0
  private           FlowCanvas.Macros.MacroOutputNode_exit  // 0xf8
PROPERTIES:
  allowBlackboardOverrides  get=0x012081B0
  canDelayDeserializeAndOptimizeClone  get=0x012081B0
  entry  get=0x0A94E4F8
  exit  get=0x0A94E608
METHODS:
  RVA=0x0A94E1F4  token=0x6000968  System.Object OnDerivedDataSerialization()
  RVA=0x0A94E12C  token=0x6000969  System.Void OnDerivedDataDeserialization(System.Object data)
  RVA=0x0A94E264  token=0x600096E  System.Void OnGraphValidate()
  RVA=0x0A94DD10  token=0x600096F  FlowCanvas.Port AddInputDefinition(ParadoxNotion.DynamicParameterDefinition def)
  RVA=0x0A94DE74  token=0x6000970  FlowCanvas.Port AddOutputDefinition(ParadoxNotion.DynamicParameterDefinition def)
  RVA=0x0A94DB88  token=0x6000971  System.Void AddExamplePorts()
  RVA=-1  // generic def  token=0x6000972  System.Void SetValueInput(System.String name, T value)
  RVA=0x0A94DFD8  token=0x6000973  System.Void CallFlowInput(System.String name)
  RVA=-1  // generic def  token=0x6000974  T GetValueOutput(System.String name)
  RVA=0x0A94E2D4  token=0x6000975  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Macros.MacroInputNode
TYPE:  class
TOKEN: 0x200027B
SIZE:  0xB0
EXTENDS: FlowCanvas.FlowNode
FIELDS:
PROPERTIES:
  iconAlignment  get=0x011EC580
  macro  get=0x0A94CD54
METHODS:
  RVA=0x0A94CA4C  token=0x6000984  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x6000985  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Macros.MacroNodeWrapper
TYPE:  class
TOKEN: 0x200027D
SIZE:  0xC0
EXTENDS: FlowCanvas.FlowNode
IMPLEMENTS: NodeCanvas.Framework.IGraphAssignable NodeCanvas.Framework.IGraphElement NodeCanvas.Framework.IUpdatable
FIELDS:
  private           FlowCanvas.Macros.Macro         _macro  // 0xb0
  private           FlowCanvas.Macros.Macro         _currentInstance  // 0xb8
PROPERTIES:
  name  get=0x0A94D714
  description  get=0x0A94D678
  macro  get=0x04D86250  set=0x0A94D79C
  NodeCanvas.Framework.IGraphAssignable.subGraph  get=0x04D86250  set=0x0A94CFEC
  NodeCanvas.Framework.IGraphAssignable.currentInstance  get=0x04D85EF0  set=0x0A94CF28
  NodeCanvas.Framework.IGraphAssignable.variablesMap  get=0x011EC580  set=0x041E1670
  NodeCanvas.Framework.IGraphAssignable.subGraphParameter  get=0x011EC580
  NodeCanvas.Framework.IGraphAssignable.instances  get=0x0A94CE94  set=0x0A94CF90
METHODS:
  RVA=0x0A94CD90  token=0x6000995  System.Void MakeInstance()
  RVA=0x0A94D040  token=0x6000996  System.Void NodeCanvas.Framework.IUpdatable.Update()
  RVA=0x0A94D0C0  token=0x6000997  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x6000998  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Macros.MacroOutputNode
TYPE:  class
TOKEN: 0x2000281
SIZE:  0xB0
EXTENDS: FlowCanvas.FlowNode
FIELDS:
PROPERTIES:
  iconAlignment  get=0x011EC580
  macro  get=0x0A94DB4C
METHODS:
  RVA=0x0A94D83C  token=0x60009A0  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x60009A1  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExternalImplementedNodeWrapper
TYPE:  class
TOKEN: 0x2000037
SIZE:  0xB8
EXTENDS: FlowCanvas.FlowNode
FIELDS:
  private           UnityEngine.Object              _target  // 0xb0
PROPERTIES:
  target  get=0x0A917E14  set=0x0A917E54
  name  get=0x0A917D94
METHODS:
  RVA=0x0A917CE0  token=0x600015E  System.Type GetRuntimeIconType()
  RVA=0x0A917D08  token=0x600015F  System.Void RegisterPorts()
  RVA=0x0A917D8C  token=0x6000160  System.Void SetTarget(FlowCanvas.Nodes.IExternalImplementedNode target)
  RVA=0x0489BC70  token=0x6000161  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.IDropedReferenceNode
TYPE:  interface
TOKEN: 0x2000038
IMPLEMENTS: NodeCanvas.Framework.IGraphElement
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000162  System.Void SetTarget(UnityEngine.Object target)
END_CLASS

CLASS: FlowCanvas.Nodes.IExternalImplementedNode
TYPE:  interface
TOKEN: 0x2000039
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000163  System.Void RegisterPorts(FlowCanvas.FlowNode parent)
END_CLASS

CLASS: FlowCanvas.Nodes.ApplicationPauseEvent
TYPE:  class
TOKEN: 0x200003A
SIZE:  0xC0
EXTENDS: FlowCanvas.Nodes.EventNode
FIELDS:
  private           FlowCanvas.FlowOutput           pause  // 0xb0
  private           System.Boolean                  isPause  // 0xb8
METHODS:
  RVA=0x0A9112E4  token=0x6000164  System.Void OnGraphStarted()
  RVA=0x0A91137C  token=0x6000165  System.Void OnGraphStoped()
  RVA=0x0A911290  token=0x6000166  System.Void ApplicationPause(System.Boolean isPause)
  RVA=0x0A911414  token=0x6000167  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x6000168  System.Void .ctor()
  RVA=0x04D892C0  token=0x6000169  System.Boolean <RegisterPorts>b__5_0()
END_CLASS

CLASS: FlowCanvas.Nodes.ApplicationQuitEvent
TYPE:  class
TOKEN: 0x200003B
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.EventNode
FIELDS:
  private           FlowCanvas.FlowOutput           quit  // 0xb0
METHODS:
  RVA=0x0A911554  token=0x600016A  System.Void OnGraphStarted()
  RVA=0x0A9115EC  token=0x600016B  System.Void OnGraphStoped()
  RVA=0x0A911504  token=0x600016C  System.Void ApplicationQuit()
  RVA=0x0A911684  token=0x600016D  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x600016E  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CSharpAutoCallbackEvent
TYPE:  class
TOKEN: 0x200003C
SIZE:  0xE0
EXTENDS: FlowCanvas.Nodes.EventNode
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper
FIELDS:
  private           ParadoxNotion.Serialization.SerializedEventInfo_event  // 0xb0
  private           FlowCanvas.Nodes.ReflectedDelegateEventreflectedEvent  // 0xb8
  private           FlowCanvas.ValueInput           instancePort  // 0xc0
  private           FlowCanvas.FlowOutput           callback  // 0xc8
  private           System.Object                   instance  // 0xd0
  private           System.Object[]                 args  // 0xd8
PROPERTIES:
  eventInfo  get=0x091C020C
  isStaticEvent  get=0x0A913640
  name  get=0x0A9136B0
METHODS:
  RVA=0x04D86250  token=0x6000172  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A9135B8  token=0x6000173  System.Void SetEvent(System.Reflection.EventInfo info, System.Object instance)
  RVA=0x0A9132A0  token=0x6000174  System.Void RegisterPorts()
  RVA=0x0A913038  token=0x6000175  System.Void OnGraphStarted()
  RVA=0x0A913190  token=0x6000176  System.Void OnGraphStoped()
  RVA=0x0A912FD4  token=0x6000177  System.Void OnEventRaised(System.Object[] args)
  RVA=0x0489BC70  token=0x6000178  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CSharpEventCallback
TYPE:  class
TOKEN: 0x200003E
SIZE:  0xE0
EXTENDS: FlowCanvas.Nodes.EventNode
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper
FIELDS:
  protected         System.Boolean                  _autoHandleRegistration  // 0xb0
  private           ParadoxNotion.Serialization.SerializedTypeInfo_type  // 0xb8
  private           System.Object[]                 argValues  // 0xc0
  private           FlowCanvas.ValueInput           eventInput  // 0xc8
  private           FlowCanvas.FlowOutput           flowCallback  // 0xd0
  private           FlowCanvas.Nodes.ReflectedDelegateEventreflectedEvent  // 0xd8
PROPERTIES:
  type  get=0x0A91404C  set=0x0A914060
  autoHandleRegistration  get=0x04D88CC0
METHODS:
  RVA=0x04D85EF0  token=0x600017E  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A91388C  token=0x600017F  System.Void OnGraphStarted()
  RVA=0x0A9138D4  token=0x6000180  System.Void OnGraphStoped()
  RVA=0x0A913A24  token=0x6000181  System.Void RegisterPorts()
  RVA=0x0A913E5C  token=0x6000182  System.Void Register(FlowCanvas.Flow f)
  RVA=0x0A913F78  token=0x6000183  System.Void Unregister(FlowCanvas.Flow f)
  RVA=0x0A9137D8  token=0x6000184  System.Void Callback(System.Object[] args)
  RVA=0x0A91383C  token=0x6000185  System.Type GetNodeWildDefinitionType()
  RVA=0x0A913934  token=0x6000186  System.Void OnPortConnected(FlowCanvas.Port port, FlowCanvas.Port otherPort)
  RVA=0x0489BC70  token=0x6000187  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CustomEvent
TYPE:  class
TOKEN: 0x2000040
SIZE:  0xE8
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>eventName  // 0xc8
  private           FlowCanvas.FlowOutput           onReceived  // 0xd0
  private           NodeCanvas.Framework.GraphOwner sender  // 0xd8
  private           NodeCanvas.Framework.GraphOwner receiver  // 0xe0
PROPERTIES:
  name  get=0x0A91722C
METHODS:
  RVA=0x0A917098  token=0x600018B  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A917128  token=0x600018C  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A916F3C  token=0x600018D  System.Void RegisterPorts()
  RVA=0x0A916D98  token=0x600018E  System.Void OnCustomEvent(System.String eventName, ParadoxNotion.IEventData msg)
  RVA=0x0A9171B8  token=0x600018F  System.Void .ctor()
  RVA=0x04D8D1C0  token=0x6000190  NodeCanvas.Framework.GraphOwner <RegisterPorts>b__8_0()
  RVA=0x04D88EF0  token=0x6000191  NodeCanvas.Framework.GraphOwner <RegisterPorts>b__8_1()
END_CLASS

CLASS: FlowCanvas.Nodes.CustomEvent`1
TYPE:  class
TOKEN: 0x2000041
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>eventName  // 0x0
  private           FlowCanvas.FlowOutput           onReceived  // 0x0
  private           NodeCanvas.Framework.GraphOwner sender  // 0x0
  private           NodeCanvas.Framework.GraphOwner receiver  // 0x0
  private           T                               receivedValue  // 0x0
PROPERTIES:
  name  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000193  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=-1  // not resolved  token=0x6000194  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=-1  // not resolved  token=0x6000195  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x6000196  System.Void OnCustomEvent(System.String eventName, ParadoxNotion.IEventData msg)
  RVA=-1  // not resolved  token=0x6000197  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000198  NodeCanvas.Framework.GraphOwner <RegisterPorts>b__9_0()
  RVA=-1  // not resolved  token=0x6000199  NodeCanvas.Framework.GraphOwner <RegisterPorts>b__9_1()
  RVA=-1  // not resolved  token=0x600019A  T <RegisterPorts>b__9_2()
END_CLASS

CLASS: FlowCanvas.Nodes.DelegateCallbackEvent
TYPE:  class
TOKEN: 0x2000042
SIZE:  0xD8
EXTENDS: FlowCanvas.Nodes.EventNode
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper
FIELDS:
  private           ParadoxNotion.Serialization.SerializedTypeInfo_type  // 0xb0
  private           FlowCanvas.Nodes.ReflectedDelegateEventreflectedEvent  // 0xb8
  private           FlowCanvas.ValueOutput          delegatePort  // 0xc0
  private           FlowCanvas.FlowOutput           callbackPort  // 0xc8
  private           System.Object[]                 args  // 0xd0
PROPERTIES:
  delegateType  get=0x091C020C  set=0x0A917814
METHODS:
  RVA=0x04D86250  token=0x600019D  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A917410  token=0x600019E  System.Void RegisterPorts()
  RVA=0x0A9172A4  token=0x600019F  System.Void Callback(System.Object[] args)
  RVA=0x0A917308  token=0x60001A0  System.Void OnGraphStoped()
  RVA=0x0A917320  token=0x60001A1  System.Void OnPortConnected(FlowCanvas.Port port, FlowCanvas.Port otherPort)
  RVA=0x0489BC70  token=0x60001A2  System.Void .ctor()
  RVA=0x0A9177F4  token=0x60001A3  System.Object <RegisterPorts>b__9_0()
END_CLASS

CLASS: FlowCanvas.Nodes.GetSharpEvent
TYPE:  class
TOKEN: 0x2000044
SIZE:  0xC0
EXTENDS: FlowCanvas.FlowNode
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper
FIELDS:
  private           ParadoxNotion.Serialization.SerializedEventInfo_event  // 0xb0
  private           FlowCanvas.ValueInput           instancePort  // 0xb8
PROPERTIES:
  eventInfo  get=0x091C020C
  name  get=0x0A91CB2C
METHODS:
  RVA=0x04D86250  token=0x60001A8  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A91CAA4  token=0x60001A9  System.Void SetEvent(System.Reflection.EventInfo info, System.Object instance)
  RVA=0x0A91C8A8  token=0x60001AA  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x60001AB  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.InvokeSignal
TYPE:  class
TOKEN: 0x2000046
SIZE:  0xD0
EXTENDS: FlowCanvas.FlowNode
IMPLEMENTS: FlowCanvas.Nodes.IDropedReferenceNode NodeCanvas.Framework.IGraphElement
FIELDS:
  public            NodeCanvas.Framework.BBParameter<NodeCanvas.Framework.SignalDefinition>_signalDefinition  // 0xb0
  public            System.Boolean                  global  // 0xb8
  private           FlowCanvas.ValueInput<UnityEngine.Transform>target  // 0xc0
  private           FlowCanvas.ValueInput[]         inputArgs  // 0xc8
PROPERTIES:
  signalDefinition  get=0x0A91DB08  set=0x0A91DB50
  name  get=0x0A91DA30
METHODS:
  RVA=0x0A91D9D0  token=0x60001B1  System.Void SetTarget(UnityEngine.Object target)
  RVA=0x0A91D700  token=0x60001B2  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x60001B3  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SignalCallback
TYPE:  class
TOKEN: 0x2000048
SIZE:  0xF0
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
IMPLEMENTS: FlowCanvas.Nodes.IDropedReferenceNode NodeCanvas.Framework.IGraphElement
FIELDS:
  public            NodeCanvas.Framework.BBParameter<NodeCanvas.Framework.SignalDefinition>_signalDefinition  // 0xc8
  private           FlowCanvas.FlowOutput           onReceived  // 0xd0
  private           UnityEngine.Transform           receiver  // 0xd8
  private           UnityEngine.Transform           sender  // 0xe0
  private           System.Object[]                 args  // 0xe8
PROPERTIES:
  signalDefinition  get=0x0A922868  set=0x0A9228B0
  name  get=0x0A922780
METHODS:
  RVA=0x0A9226EC  token=0x60001B9  System.Void SetTarget(UnityEngine.Object target)
  RVA=0x041E1670  token=0x60001BA  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x041E1670  token=0x60001BB  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A9222C8  token=0x60001BC  System.Void OnPostGraphStarted()
  RVA=0x0A922084  token=0x60001BD  System.Void OnGraphStoped()
  RVA=0x0A922404  token=0x60001BE  System.Void RegisterPorts()
  RVA=0x0A922170  token=0x60001BF  System.Void OnInvoked(UnityEngine.Transform sender, UnityEngine.Transform receiver, System.Boolean isGlobal, System.Object[] args)
  RVA=0x0A92274C  token=0x60001C0  System.Void .ctor()
  RVA=0x04D88EF0  token=0x60001C1  UnityEngine.Transform <RegisterPorts>b__15_0()
  RVA=0x04D8D1C0  token=0x60001C2  UnityEngine.Transform <RegisterPorts>b__15_1()
END_CLASS

CLASS: FlowCanvas.Nodes.UnityEventAutoCallbackEvent
TYPE:  class
TOKEN: 0x200004A
SIZE:  0xE0
EXTENDS: FlowCanvas.Nodes.EventNode
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper
FIELDS:
  private           ParadoxNotion.Serialization.SerializedUnityEventInfo_eventMember  // 0xb0
  private           FlowCanvas.Nodes.ReflectedUnityEventreflectedEvent  // 0xb8
  private           UnityEngine.Events.UnityEventBaseunityEvent  // 0xc0
  private           FlowCanvas.ValueInput           instancePort  // 0xc8
  private           FlowCanvas.FlowOutput           callback  // 0xd0
  private           System.Object[]                 args  // 0xd8
PROPERTIES:
  member  get=0x0A927D58
  isStatic  get=0x0A927D30
  eventType  get=0x0A927CF8
  field  get=0x0A927D20
  prop  get=0x0A927E9C
  name  get=0x0A927D74
METHODS:
  RVA=0x04D86250  token=0x60001CB  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A927C70  token=0x60001CC  System.Void SetEvent(System.Reflection.MemberInfo newMember, System.Object instance)
  RVA=0x0A927934  token=0x60001CD  System.Void RegisterPorts()
  RVA=0x0A927670  token=0x60001CE  System.Void OnGraphStarted()
  RVA=0x0A927880  token=0x60001CF  System.Void OnGraphStoped()
  RVA=0x0A92760C  token=0x60001D0  System.Void OnEventRaised(System.Object[] args)
  RVA=0x0489BC70  token=0x60001D1  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.UnityEventCallbackEvent
TYPE:  class
TOKEN: 0x200004C
SIZE:  0xE0
EXTENDS: FlowCanvas.Nodes.EventNode
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper
FIELDS:
  protected         System.Boolean                  _autoHandleRegistration  // 0xb0
  private           ParadoxNotion.Serialization.SerializedTypeInfo_type  // 0xb8
  private           System.Object[]                 argValues  // 0xc0
  private           FlowCanvas.ValueInput           eventInput  // 0xc8
  private           FlowCanvas.FlowOutput           flowCallback  // 0xd0
  private           FlowCanvas.Nodes.ReflectedUnityEventreflectedEvent  // 0xd8
PROPERTIES:
  eventType  get=0x0A91404C  set=0x0A928738
  autoHandleRegistration  get=0x04D88CC0
METHODS:
  RVA=0x04D85EF0  token=0x60001D7  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A927EFC  token=0x60001D8  System.Void OnGraphStarted()
  RVA=0x0A927F44  token=0x60001D9  System.Void OnGraphStoped()
  RVA=0x0A92807C  token=0x60001DA  System.Void RegisterPorts()
  RVA=0x0A928538  token=0x60001DB  System.Void Register(FlowCanvas.Flow f)
  RVA=0x0A928660  token=0x60001DC  System.Void Unregister(FlowCanvas.Flow f)
  RVA=0x0A9137D8  token=0x60001DD  System.Void OnEventRaised(System.Object[] args)
  RVA=0x0A927EAC  token=0x60001DE  System.Type GetNodeWildDefinitionType()
  RVA=0x0A927F8C  token=0x60001DF  System.Void OnPortConnected(FlowCanvas.Port port, FlowCanvas.Port otherPort)
  RVA=0x0489BC70  token=0x60001E0  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.EventNode
TYPE:  abstract class
TOKEN: 0x200004E
SIZE:  0xB0
EXTENDS: FlowCanvas.FlowNode
FIELDS:
PROPERTIES:
  name  get=0x0A917C84
METHODS:
  RVA=0x0489BC70  token=0x60001E4  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.EventNode`1
TYPE:  abstract class
TOKEN: 0x200004F
EXTENDS: FlowCanvas.Nodes.EventNode
FIELDS:
  public            NodeCanvas.Framework.BBParameter<T>target  // 0x0
PROPERTIES:
  name  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60001E6  System.Void OnPostGraphStarted()
  RVA=-1  // not resolved  token=0x60001E7  System.Void ResolveSelf()
  RVA=-1  // not resolved  token=0x60001E8  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.RouterEventNode`1
TYPE:  abstract class
TOKEN: 0x2000050
EXTENDS: FlowCanvas.Nodes.EventNode
FIELDS:
  public            FlowCanvas.Nodes.RouterEventNode.TargetMode<T>targetMode  // 0x0
  public            NodeCanvas.Framework.BBParameter<T>target  // 0x0
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<T>>targets  // 0x0
PROPERTIES:
  name  get=-1  // not resolved
METHODS:
  RVA=-1  // abstract  token=0x60001EA  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=-1  // abstract  token=0x60001EB  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=-1  // not resolved  token=0x60001EC  System.Void OnPostGraphStarted()
  RVA=-1  // not resolved  token=0x60001ED  System.Void OnGraphStoped()
  RVA=-1  // not resolved  token=0x60001EE  T ResolveReceiver(UnityEngine.GameObject receiver)
  RVA=-1  // not resolved  token=0x60001EF  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ConstructionEvent
TYPE:  class
TOKEN: 0x2000052
SIZE:  0xC0
EXTENDS: FlowCanvas.Nodes.EventNode
FIELDS:
  private           FlowCanvas.FlowOutput           awake  // 0xb0
  private           System.Boolean                  called  // 0xb8
METHODS:
  RVA=0x0A916BAC  token=0x60001F0  System.Void OnPostGraphStarted()
  RVA=0x0A916C0C  token=0x60001F1  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x60001F2  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.DisableEvent
TYPE:  class
TOKEN: 0x2000053
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.EventNode
FIELDS:
  private           FlowCanvas.FlowOutput           disable  // 0xb0
METHODS:
  RVA=0x0A911504  token=0x60001F3  System.Void OnGraphStoped()
  RVA=0x0A9178C4  token=0x60001F4  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x60001F5  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.EnableEvent
TYPE:  class
TOKEN: 0x2000054
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.EventNode
FIELDS:
  private           FlowCanvas.FlowOutput           enable  // 0xb0
METHODS:
  RVA=0x0A911504  token=0x60001F6  System.Void OnPostGraphStarted()
  RVA=0x0A917C20  token=0x60001F7  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x60001F8  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.FixedUpdateEvent
TYPE:  class
TOKEN: 0x2000055
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.EventNode
FIELDS:
  private           FlowCanvas.FlowOutput           fixedUpdate  // 0xb0
METHODS:
  RVA=0x0A918000  token=0x60001F9  System.Void RegisterPorts()
  RVA=0x0A917ED0  token=0x60001FA  System.Void OnGraphStarted()
  RVA=0x0A917F68  token=0x60001FB  System.Void OnGraphStoped()
  RVA=0x0A911504  token=0x60001FC  System.Void FixedUpdate()
  RVA=0x0489BC70  token=0x60001FD  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.LateUpdateEvent
TYPE:  class
TOKEN: 0x2000056
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.EventNode
FIELDS:
  private           FlowCanvas.FlowOutput           lateUpdate  // 0xb0
METHODS:
  RVA=0x0A91DF88  token=0x60001FE  System.Void RegisterPorts()
  RVA=0x0A91DE58  token=0x60001FF  System.Void OnGraphStarted()
  RVA=0x0A91DEF0  token=0x6000200  System.Void OnGraphStoped()
  RVA=0x0A911504  token=0x6000201  System.Void LateUpdate()
  RVA=0x0489BC70  token=0x6000202  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.StartEvent
TYPE:  class
TOKEN: 0x2000057
SIZE:  0xC0
EXTENDS: FlowCanvas.Nodes.EventNode
FIELDS:
  private           FlowCanvas.FlowOutput           start  // 0xb0
  private           System.Boolean                  called  // 0xb8
METHODS:
  RVA=0x0A922908  token=0x6000203  System.Void OnPostGraphStarted()
  RVA=0x0A9229EC  token=0x6000204  System.Void OnStartCallback()
  RVA=0x0A922A64  token=0x6000205  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x6000206  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.UpdateEvent
TYPE:  class
TOKEN: 0x2000058
SIZE:  0xC8
EXTENDS: FlowCanvas.Nodes.EventNode
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Single>updateInterval  // 0xb0
  private           FlowCanvas.FlowOutput           update  // 0xb8
  private           System.Single                   lastUpdatedTime  // 0xc0
METHODS:
  RVA=0x0A9287E8  token=0x6000207  System.Void RegisterPorts()
  RVA=0x04DA6440  token=0x6000208  System.Void OnGraphStarted()
  RVA=0x0A92884C  token=0x6000209  System.Void Update()
  RVA=0x0A928974  token=0x600020A  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.InputAxisEvent
TYPE:  class
TOKEN: 0x2000059
SIZE:  0xC8
EXTENDS: FlowCanvas.Nodes.EventNode
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           FlowCanvas.FlowOutput           o  // 0xb0
  private           System.Single                   horizontal  // 0xb8
  private           System.Single                   vertical  // 0xbc
  private           System.Boolean                  calledLastFrame  // 0xc0
METHODS:
  RVA=0x0A91CCA4  token=0x600020B  System.Void RegisterPorts()
  RVA=0x0A91CE00  token=0x600020C  System.Void Update()
  RVA=0x0489BC70  token=0x600020D  System.Void .ctor()
  RVA=0x04DA32D0  token=0x600020E  System.Single <RegisterPorts>b__4_0()
  RVA=0x04DA62F0  token=0x600020F  System.Single <RegisterPorts>b__4_1()
END_CLASS

CLASS: FlowCanvas.Nodes.InputButtonEvents
TYPE:  class
TOKEN: 0x200005A
SIZE:  0xD0
EXTENDS: FlowCanvas.Nodes.EventNode
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>buttonName  // 0xb0
  private           FlowCanvas.FlowOutput           down  // 0xb8
  private           FlowCanvas.FlowOutput           up  // 0xc0
  private           FlowCanvas.FlowOutput           pressed  // 0xc8
PROPERTIES:
  name  get=0x0A91D1B8
METHODS:
  RVA=0x0A91CF44  token=0x6000211  System.Void RegisterPorts()
  RVA=0x0A91D018  token=0x6000212  System.Void Update()
  RVA=0x0A91D150  token=0x6000213  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.InputCustomAxisEvent
TYPE:  class
TOKEN: 0x200005B
SIZE:  0xD0
EXTENDS: FlowCanvas.Nodes.EventNode
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<System.String>>axis  // 0xb0
  private           System.Single[]                 axisValues  // 0xb8
  private           System.Boolean                  calledLastFrame  // 0xc0
  private           FlowCanvas.FlowOutput           o  // 0xc8
METHODS:
  RVA=0x0A91D208  token=0x6000214  System.Void RegisterPorts()
  RVA=0x0A91D470  token=0x6000215  System.Void Update()
  RVA=0x0A91D610  token=0x6000216  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.KeyboardEvents
TYPE:  class
TOKEN: 0x200005D
SIZE:  0xD0
EXTENDS: FlowCanvas.Nodes.EventNode
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.KeyCode>keyCode  // 0xb0
  private           FlowCanvas.FlowOutput           down  // 0xb8
  private           FlowCanvas.FlowOutput           up  // 0xc0
  private           FlowCanvas.FlowOutput           pressed  // 0xc8
PROPERTIES:
  name  get=0x0A91DE08
METHODS:
  RVA=0x0A91DBA8  token=0x600021A  System.Void RegisterPorts()
  RVA=0x0A91DC7C  token=0x600021B  System.Void Update()
  RVA=0x0A91DDB0  token=0x600021C  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.MouseEvents
TYPE:  class
TOKEN: 0x200005E
SIZE:  0xD0
EXTENDS: FlowCanvas.Nodes.EventNode
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  public            NodeCanvas.Framework.BBParameter<FlowCanvas.Nodes.MouseEvents.ButtonKeys>buttonKey  // 0xb0
  private           FlowCanvas.FlowOutput           down  // 0xb8
  private           FlowCanvas.FlowOutput           pressed  // 0xc0
  private           FlowCanvas.FlowOutput           up  // 0xc8
PROPERTIES:
  name  get=0x0A91F990
METHODS:
  RVA=0x0A91F788  token=0x600021E  System.Void RegisterPorts()
  RVA=0x0A91F85C  token=0x600021F  System.Void Update()
  RVA=0x0489BC70  token=0x6000220  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.MousePickEvent
TYPE:  class
TOKEN: 0x2000060
SIZE:  0x108
EXTENDS: FlowCanvas.Nodes.EventNode
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  public            NodeCanvas.Framework.BBParameter<FlowCanvas.Nodes.MousePickEvent.ButtonKeys>buttonKey  // 0xb0
  public            NodeCanvas.Framework.BBParameter<UnityEngine.LayerMask>mask  // 0xb8
  private           FlowCanvas.FlowOutput           o  // 0xc0
  private           UnityEngine.RaycastHit          hit  // 0xc8
PROPERTIES:
  name  get=0x0A91FCA4
METHODS:
  RVA=0x0A91F9E0  token=0x6000222  System.Void RegisterPorts()
  RVA=0x0A91FAD0  token=0x6000223  System.Void Update()
  RVA=0x0A91FC34  token=0x6000224  System.Void .ctor()
  RVA=0x04D9E330  token=0x6000225  UnityEngine.RaycastHit <RegisterPorts>b__7_0()
END_CLASS

CLASS: FlowCanvas.Nodes.AnimatorEvents
TYPE:  class
TOKEN: 0x2000062
SIZE:  0xE8
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           onAnimatorMove  // 0xc8
  private           FlowCanvas.FlowOutput           onAnimatorIK  // 0xd0
  private           UnityEngine.Animator            receiver  // 0xd8
  private           System.Int32                    layerIndex  // 0xe0
METHODS:
  RVA=0x0A910EEC  token=0x6000226  System.Void RegisterPorts()
  RVA=0x0A91109C  token=0x6000227  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A91117C  token=0x6000228  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A910E48  token=0x6000229  System.Void OnAnimatorMove(ParadoxNotion.EventData msg)
  RVA=0x0A910D80  token=0x600022A  System.Void OnAnimatorIK(ParadoxNotion.EventData<System.Int32> msg)
  RVA=0x0A91125C  token=0x600022B  System.Void .ctor()
  RVA=0x04D88EF0  token=0x600022C  UnityEngine.Animator <RegisterPorts>b__4_0()
  RVA=0x04D9A880  token=0x600022D  System.Int32 <RegisterPorts>b__4_1()
END_CLASS

CLASS: FlowCanvas.Nodes.CharacterControllerEvents
TYPE:  class
TOKEN: 0x2000063
SIZE:  0x100
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           FlowCanvas.FlowOutput           onHit  // 0xc8
  private           UnityEngine.CharacterController receiver  // 0xd0
  private           UnityEngine.ControllerColliderHithitInfo  // 0xd8
  private           FlowCanvas.FlowOutput           onGrounded  // 0xe0
  private           FlowCanvas.FlowOutput           onUnGrounded  // 0xe8
  private           System.Boolean                  wasGrounded  // 0xf0
  private           System.Boolean[]                wasGroundedMulti  // 0xf8
METHODS:
  RVA=0x0A91445C  token=0x600022E  System.Void OnPostGraphStarted()
  RVA=0x0A914110  token=0x600022F  System.Void NodeCanvas.Framework.IUpdatable.Update()
  RVA=0x0A91454C  token=0x6000230  System.Void RegisterPorts()
  RVA=0x0A914838  token=0x6000231  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A91491C  token=0x6000232  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A914388  token=0x6000233  System.Void OnControllerColliderHit(ParadoxNotion.EventData<UnityEngine.ControllerColliderHit> msg)
  RVA=0x0A9149AC  token=0x6000234  System.Void .ctor()
  RVA=0x04D876B0  token=0x6000235  UnityEngine.CharacterController <RegisterPorts>b__9_0()
  RVA=0x0A9148C8  token=0x6000236  UnityEngine.GameObject <RegisterPorts>b__9_1()
  RVA=0x0A9148EC  token=0x6000237  UnityEngine.Vector3 <RegisterPorts>b__9_2()
  RVA=0x04D88EF0  token=0x6000238  UnityEngine.ControllerColliderHit <RegisterPorts>b__9_3()
END_CLASS

CLASS: FlowCanvas.Nodes.Collision2DEvents_Rigidbody
TYPE:  class
TOKEN: 0x2000064
SIZE:  0xF0
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           onEnter  // 0xc8
  private           FlowCanvas.FlowOutput           onStay  // 0xd0
  private           FlowCanvas.FlowOutput           onExit  // 0xd8
  private           UnityEngine.Rigidbody2D         receiver  // 0xe0
  private           UnityEngine.Collision2D         collision  // 0xe8
METHODS:
  RVA=0x0A915394  token=0x6000239  System.Void RegisterPorts()
  RVA=0x0A915680  token=0x600023A  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A915800  token=0x600023B  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A915118  token=0x600023C  System.Void OnCollisionEnter2D(ParadoxNotion.EventData<UnityEngine.Collision2D> msg)
  RVA=0x0A9152C0  token=0x600023D  System.Void OnCollisionStay2D(ParadoxNotion.EventData<UnityEngine.Collision2D> msg)
  RVA=0x0A9151EC  token=0x600023E  System.Void OnCollisionExit2D(ParadoxNotion.EventData<UnityEngine.Collision2D> msg)
  RVA=0x0A91591C  token=0x600023F  System.Void .ctor()
  RVA=0x04D8D1C0  token=0x6000240  UnityEngine.Rigidbody2D <RegisterPorts>b__5_0()
  RVA=0x0A91579C  token=0x6000241  UnityEngine.GameObject <RegisterPorts>b__5_1()
  RVA=0x0A9157C0  token=0x6000242  UnityEngine.ContactPoint2D <RegisterPorts>b__5_2()
  RVA=0x04D88590  token=0x6000243  UnityEngine.Collision2D <RegisterPorts>b__5_3()
END_CLASS

CLASS: FlowCanvas.Nodes.Collision2DEvents
TYPE:  class
TOKEN: 0x2000065
SIZE:  0xF0
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           onEnter  // 0xc8
  private           FlowCanvas.FlowOutput           onStay  // 0xd0
  private           FlowCanvas.FlowOutput           onExit  // 0xd8
  private           UnityEngine.Collider2D          receiver  // 0xe0
  private           UnityEngine.Collision2D         collision  // 0xe8
METHODS:
  RVA=0x0A914E48  token=0x6000244  System.Void RegisterPorts()
  RVA=0x0A915950  token=0x6000245  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A915A6C  token=0x6000246  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A914BCC  token=0x6000247  System.Void OnCollisionEnter2D(ParadoxNotion.EventData<UnityEngine.Collision2D> msg)
  RVA=0x0A914D74  token=0x6000248  System.Void OnCollisionStay2D(ParadoxNotion.EventData<UnityEngine.Collision2D> msg)
  RVA=0x0A914CA0  token=0x6000249  System.Void OnCollisionExit2D(ParadoxNotion.EventData<UnityEngine.Collision2D> msg)
  RVA=0x0A915B88  token=0x600024A  System.Void .ctor()
  RVA=0x04D8D1C0  token=0x600024B  UnityEngine.Collider2D <RegisterPorts>b__5_0()
  RVA=0x0A91579C  token=0x600024C  UnityEngine.GameObject <RegisterPorts>b__5_1()
  RVA=0x0A9157C0  token=0x600024D  UnityEngine.ContactPoint2D <RegisterPorts>b__5_2()
  RVA=0x04D88590  token=0x600024E  UnityEngine.Collision2D <RegisterPorts>b__5_3()
END_CLASS

CLASS: FlowCanvas.Nodes.CollisionEvents_Rigidbody
TYPE:  class
TOKEN: 0x2000066
SIZE:  0xF0
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           onEnter  // 0xc8
  private           FlowCanvas.FlowOutput           onStay  // 0xd0
  private           FlowCanvas.FlowOutput           onExit  // 0xd8
  private           UnityEngine.Rigidbody           receiver  // 0xe0
  private           UnityEngine.Collision           collision  // 0xe8
METHODS:
  RVA=0x0A916384  token=0x600024F  System.Void RegisterPorts()
  RVA=0x0A916670  token=0x6000250  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A9167F0  token=0x6000251  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A916108  token=0x6000252  System.Void OnCollisionEnter(ParadoxNotion.EventData<UnityEngine.Collision> msg)
  RVA=0x0A9162B0  token=0x6000253  System.Void OnCollisionStay(ParadoxNotion.EventData<UnityEngine.Collision> msg)
  RVA=0x0A9161DC  token=0x6000254  System.Void OnCollisionExit(ParadoxNotion.EventData<UnityEngine.Collision> msg)
  RVA=0x0A91690C  token=0x6000255  System.Void .ctor()
  RVA=0x04D8D1C0  token=0x6000256  UnityEngine.Rigidbody <RegisterPorts>b__5_0()
  RVA=0x0A91678C  token=0x6000257  UnityEngine.GameObject <RegisterPorts>b__5_1()
  RVA=0x0A9167B0  token=0x6000258  UnityEngine.ContactPoint <RegisterPorts>b__5_2()
  RVA=0x04D88590  token=0x6000259  UnityEngine.Collision <RegisterPorts>b__5_3()
END_CLASS

CLASS: FlowCanvas.Nodes.CollisionEvents
TYPE:  class
TOKEN: 0x2000067
SIZE:  0xF0
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           onEnter  // 0xc8
  private           FlowCanvas.FlowOutput           onStay  // 0xd0
  private           FlowCanvas.FlowOutput           onExit  // 0xd8
  private           UnityEngine.Collider            receiver  // 0xe0
  private           UnityEngine.Collision           collision  // 0xe8
METHODS:
  RVA=0x0A915E38  token=0x600025A  System.Void RegisterPorts()
  RVA=0x0A916940  token=0x600025B  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A916A5C  token=0x600025C  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A915BBC  token=0x600025D  System.Void OnCollisionEnter(ParadoxNotion.EventData<UnityEngine.Collision> msg)
  RVA=0x0A915D64  token=0x600025E  System.Void OnCollisionStay(ParadoxNotion.EventData<UnityEngine.Collision> msg)
  RVA=0x0A915C90  token=0x600025F  System.Void OnCollisionExit(ParadoxNotion.EventData<UnityEngine.Collision> msg)
  RVA=0x0A916B78  token=0x6000260  System.Void .ctor()
  RVA=0x04D8D1C0  token=0x6000261  UnityEngine.Collider <RegisterPorts>b__5_0()
  RVA=0x0A91678C  token=0x6000262  UnityEngine.GameObject <RegisterPorts>b__5_1()
  RVA=0x0A9167B0  token=0x6000263  UnityEngine.ContactPoint <RegisterPorts>b__5_2()
  RVA=0x04D88590  token=0x6000264  UnityEngine.Collision <RegisterPorts>b__5_3()
END_CLASS

CLASS: FlowCanvas.Nodes.MouseAgent2DEvents
TYPE:  class
TOKEN: 0x2000068
SIZE:  0x128
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           onEnter  // 0xc8
  private           FlowCanvas.FlowOutput           onOver  // 0xd0
  private           FlowCanvas.FlowOutput           onExit  // 0xd8
  private           FlowCanvas.FlowOutput           onDown  // 0xe0
  private           FlowCanvas.FlowOutput           onUp  // 0xe8
  private           FlowCanvas.FlowOutput           onDrag  // 0xf0
  private           UnityEngine.Collider2D          receiver  // 0xf8
  private           UnityEngine.RaycastHit2D        hit  // 0x100
METHODS:
  RVA=0x0A91E40C  token=0x6000265  System.Void RegisterPorts()
  RVA=0x0A91E7EC  token=0x6000266  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A91E9E0  token=0x6000267  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A91E14C  token=0x6000268  System.Void OnMouseEnter(ParadoxNotion.EventData msg)
  RVA=0x0A91E2AC  token=0x6000269  System.Void OnMouseOver(ParadoxNotion.EventData msg)
  RVA=0x0A91E1FC  token=0x600026A  System.Void OnMouseExit(ParadoxNotion.EventData msg)
  RVA=0x0A91DFEC  token=0x600026B  System.Void OnMouseDown(ParadoxNotion.EventData msg)
  RVA=0x0A91E35C  token=0x600026C  System.Void OnMouseUp(ParadoxNotion.EventData msg)
  RVA=0x0A91E09C  token=0x600026D  System.Void OnMouseDrag(ParadoxNotion.EventData msg)
  RVA=0x0A91E67C  token=0x600026E  System.Void StoreHit()
  RVA=0x0A91EBD4  token=0x600026F  System.Void .ctor()
  RVA=0x04D877C0  token=0x6000270  UnityEngine.Collider2D <RegisterPorts>b__8_0()
  RVA=0x04DA63E0  token=0x6000271  UnityEngine.RaycastHit2D <RegisterPorts>b__8_1()
END_CLASS

CLASS: FlowCanvas.Nodes.MouseAgentEvents
TYPE:  class
TOKEN: 0x2000069
SIZE:  0x140
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           onEnter  // 0xc8
  private           FlowCanvas.FlowOutput           onOver  // 0xd0
  private           FlowCanvas.FlowOutput           onExit  // 0xd8
  private           FlowCanvas.FlowOutput           onDown  // 0xe0
  private           FlowCanvas.FlowOutput           onUp  // 0xe8
  private           FlowCanvas.FlowOutput           onDrag  // 0xf0
  private           UnityEngine.Collider            receiver  // 0xf8
  private           UnityEngine.RaycastHit          hit  // 0x100
METHODS:
  RVA=0x0A91F028  token=0x6000272  System.Void RegisterPorts()
  RVA=0x0A91F36C  token=0x6000273  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A91F560  token=0x6000274  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A91ED68  token=0x6000275  System.Void OnMouseEnter(ParadoxNotion.EventData msg)
  RVA=0x0A91EEC8  token=0x6000276  System.Void OnMouseOver(ParadoxNotion.EventData msg)
  RVA=0x0A91EE18  token=0x6000277  System.Void OnMouseExit(ParadoxNotion.EventData msg)
  RVA=0x0A91EC08  token=0x6000278  System.Void OnMouseDown(ParadoxNotion.EventData msg)
  RVA=0x0A91EF78  token=0x6000279  System.Void OnMouseUp(ParadoxNotion.EventData msg)
  RVA=0x0A91ECB8  token=0x600027A  System.Void OnMouseDrag(ParadoxNotion.EventData msg)
  RVA=0x0A91F298  token=0x600027B  System.Void StoreHit()
  RVA=0x0A91F754  token=0x600027C  System.Void .ctor()
  RVA=0x04D877C0  token=0x600027D  UnityEngine.Collider <RegisterPorts>b__8_0()
  RVA=0x04DA6410  token=0x600027E  UnityEngine.RaycastHit <RegisterPorts>b__8_1()
END_CLASS

CLASS: FlowCanvas.Nodes.ObjectStateEvents
TYPE:  class
TOKEN: 0x200006A
SIZE:  0xE8
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           onEnable  // 0xc8
  private           FlowCanvas.FlowOutput           onDisable  // 0xd0
  private           FlowCanvas.FlowOutput           onDestroy  // 0xd8
  private           UnityEngine.GameObject          receiver  // 0xe0
METHODS:
  RVA=0x0A91FF10  token=0x600027F  System.Void RegisterPorts()
  RVA=0x0A920070  token=0x6000280  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A92018C  token=0x6000281  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A91FE5C  token=0x6000282  System.Void OnEnable(ParadoxNotion.EventData msg)
  RVA=0x0A91FDA8  token=0x6000283  System.Void OnDisable(ParadoxNotion.EventData msg)
  RVA=0x0A91FCF4  token=0x6000284  System.Void OnDestroy(ParadoxNotion.EventData msg)
  RVA=0x0A9202A8  token=0x6000285  System.Void .ctor()
  RVA=0x04D8D1C0  token=0x6000286  UnityEngine.GameObject <RegisterPorts>b__4_0()
END_CLASS

CLASS: FlowCanvas.Nodes.DrawGizmosEvents
TYPE:  class
TOKEN: 0x200006B
SIZE:  0xD8
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           onDrawGizmos  // 0xc8
  private           UnityEngine.GameObject          receiver  // 0xd0
METHODS:
  RVA=0x0A9179DC  token=0x6000287  System.Void RegisterPorts()
  RVA=0x0A917ACC  token=0x6000288  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A917B5C  token=0x6000289  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A917928  token=0x600028A  System.Void OnDrawGizmos(ParadoxNotion.EventData msg)
  RVA=0x0A917BEC  token=0x600028B  System.Void .ctor()
  RVA=0x04D876B0  token=0x600028C  UnityEngine.GameObject <RegisterPorts>b__2_0()
END_CLASS

CLASS: FlowCanvas.Nodes.ParticleCollision2DEvents
TYPE:  class
TOKEN: 0x200006C
SIZE:  0xE8
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           onCollision  // 0xc8
  private           UnityEngine.Collider2D          receiver  // 0xd0
  private           UnityEngine.ParticleSystem      particle  // 0xd8
  private           System.Collections.Generic.List<UnityEngine.ParticleCollisionEvent>collisionEvents  // 0xe0
METHODS:
  RVA=0x0A920484  token=0x600028D  System.Void RegisterPorts()
  RVA=0x0A920738  token=0x600028E  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A920924  token=0x600028F  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A9202DC  token=0x6000290  System.Void OnParticleCollision(ParadoxNotion.EventData<UnityEngine.GameObject> msg)
  RVA=0x0A9209B4  token=0x6000291  System.Void .ctor()
  RVA=0x04D876B0  token=0x6000292  UnityEngine.Collider2D <RegisterPorts>b__4_0()
  RVA=0x04D88EF0  token=0x6000293  UnityEngine.ParticleSystem <RegisterPorts>b__4_1()
  RVA=0x0A9207C8  token=0x6000294  UnityEngine.Vector3 <RegisterPorts>b__4_2()
  RVA=0x0A92083C  token=0x6000295  UnityEngine.Vector3 <RegisterPorts>b__4_3()
  RVA=0x0A9208B0  token=0x6000296  UnityEngine.Vector3 <RegisterPorts>b__4_4()
END_CLASS

CLASS: FlowCanvas.Nodes.ParticleCollisionEvents
TYPE:  class
TOKEN: 0x200006D
SIZE:  0xE8
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           onCollision  // 0xc8
  private           UnityEngine.Collider            receiver  // 0xd0
  private           UnityEngine.ParticleSystem      particle  // 0xd8
  private           System.Collections.Generic.List<UnityEngine.ParticleCollisionEvent>collisionEvents  // 0xe0
METHODS:
  RVA=0x0A920B90  token=0x6000297  System.Void RegisterPorts()
  RVA=0x0A920E44  token=0x6000298  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A921030  token=0x6000299  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A9209E8  token=0x600029A  System.Void OnParticleCollision(ParadoxNotion.EventData<UnityEngine.GameObject> msg)
  RVA=0x0A9210C0  token=0x600029B  System.Void .ctor()
  RVA=0x04D876B0  token=0x600029C  UnityEngine.Collider <RegisterPorts>b__4_0()
  RVA=0x04D88EF0  token=0x600029D  UnityEngine.ParticleSystem <RegisterPorts>b__4_1()
  RVA=0x0A920ED4  token=0x600029E  UnityEngine.Vector3 <RegisterPorts>b__4_2()
  RVA=0x0A920F48  token=0x600029F  UnityEngine.Vector3 <RegisterPorts>b__4_3()
  RVA=0x0A920FBC  token=0x60002A0  UnityEngine.Vector3 <RegisterPorts>b__4_4()
END_CLASS

CLASS: FlowCanvas.Nodes.TransformEvents
TYPE:  class
TOKEN: 0x200006E
SIZE:  0xF0
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           onParentChanged  // 0xc8
  private           FlowCanvas.FlowOutput           onChildrenChanged  // 0xd0
  private           UnityEngine.Transform           receiver  // 0xd8
  private           UnityEngine.Transform           parent  // 0xe0
  private           System.Collections.Generic.IEnumerable<UnityEngine.Transform>children  // 0xe8
METHODS:
  RVA=0x0A922CC8  token=0x60002A1  System.Void RegisterPorts()
  RVA=0x0A922EE0  token=0x60002A2  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A922FB4  token=0x60002A3  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A922BC8  token=0x60002A4  System.Void OnTransformParentChanged(ParadoxNotion.EventData msg)
  RVA=0x0A922AC8  token=0x60002A5  System.Void OnTransformChildrenChanged(ParadoxNotion.EventData msg)
  RVA=0x0A923088  token=0x60002A6  System.Void .ctor()
  RVA=0x04D88EF0  token=0x60002A7  UnityEngine.Transform <RegisterPorts>b__5_0()
  RVA=0x04D8D1C0  token=0x60002A8  UnityEngine.Transform <RegisterPorts>b__5_1()
  RVA=0x04D88590  token=0x60002A9  System.Collections.Generic.IEnumerable<UnityEngine.Transform> <RegisterPorts>b__5_2()
END_CLASS

CLASS: FlowCanvas.Nodes.Trigger2DEvents_Transform
TYPE:  class
TOKEN: 0x200006F
SIZE:  0xF0
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           onEnter  // 0xc8
  private           FlowCanvas.FlowOutput           onStay  // 0xd0
  private           FlowCanvas.FlowOutput           onExit  // 0xd8
  private           UnityEngine.Transform           receiver  // 0xe0
  private           UnityEngine.GameObject          other  // 0xe8
METHODS:
  RVA=0x0A9238E0  token=0x60002AA  System.Void RegisterPorts()
  RVA=0x0A923AC8  token=0x60002AB  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A923BE4  token=0x60002AC  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A923640  token=0x60002AD  System.Void OnTriggerEnter2D(ParadoxNotion.EventData<UnityEngine.Collider2D> msg)
  RVA=0x0A923800  token=0x60002AE  System.Void OnTriggerStay2D(ParadoxNotion.EventData<UnityEngine.Collider2D> msg)
  RVA=0x0A923720  token=0x60002AF  System.Void OnTriggerExit2D(ParadoxNotion.EventData<UnityEngine.Collider2D> msg)
  RVA=0x0A923D00  token=0x60002B0  System.Void .ctor()
  RVA=0x04D8D1C0  token=0x60002B1  UnityEngine.Transform <RegisterPorts>b__5_0()
  RVA=0x04D88590  token=0x60002B2  UnityEngine.GameObject <RegisterPorts>b__5_1()
END_CLASS

CLASS: FlowCanvas.Nodes.Trigger2DEvents
TYPE:  class
TOKEN: 0x2000070
SIZE:  0xF0
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           onEnter  // 0xc8
  private           FlowCanvas.FlowOutput           onStay  // 0xd0
  private           FlowCanvas.FlowOutput           onExit  // 0xd8
  private           UnityEngine.Collider2D          receiver  // 0xe0
  private           UnityEngine.GameObject          other  // 0xe8
METHODS:
  RVA=0x0A92335C  token=0x60002B3  System.Void RegisterPorts()
  RVA=0x0A923524  token=0x60002B4  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A923D34  token=0x60002B5  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A9230BC  token=0x60002B6  System.Void OnTriggerEnter2D(ParadoxNotion.EventData<UnityEngine.Collider2D> msg)
  RVA=0x0A92327C  token=0x60002B7  System.Void OnTriggerStay2D(ParadoxNotion.EventData<UnityEngine.Collider2D> msg)
  RVA=0x0A92319C  token=0x60002B8  System.Void OnTriggerExit2D(ParadoxNotion.EventData<UnityEngine.Collider2D> msg)
  RVA=0x0A923E50  token=0x60002B9  System.Void .ctor()
  RVA=0x04D8D1C0  token=0x60002BA  UnityEngine.Collider2D <RegisterPorts>b__5_0()
  RVA=0x04D88590  token=0x60002BB  UnityEngine.GameObject <RegisterPorts>b__5_1()
END_CLASS

CLASS: FlowCanvas.Nodes.TriggerEvents_Transform
TYPE:  class
TOKEN: 0x2000071
SIZE:  0xF0
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           onEnter  // 0xc8
  private           FlowCanvas.FlowOutput           onStay  // 0xd0
  private           FlowCanvas.FlowOutput           onExit  // 0xd8
  private           UnityEngine.Transform           receiver  // 0xe0
  private           UnityEngine.GameObject          other  // 0xe8
METHODS:
  RVA=0x0A9246A8  token=0x60002BC  System.Void RegisterPorts()
  RVA=0x0A924890  token=0x60002BD  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A9249AC  token=0x60002BE  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A924408  token=0x60002BF  System.Void OnTriggerEnter(ParadoxNotion.EventData<UnityEngine.Collider> msg)
  RVA=0x0A9245C8  token=0x60002C0  System.Void OnTriggerStay(ParadoxNotion.EventData<UnityEngine.Collider> msg)
  RVA=0x0A9244E8  token=0x60002C1  System.Void OnTriggerExit(ParadoxNotion.EventData<UnityEngine.Collider> msg)
  RVA=0x0A924AC8  token=0x60002C2  System.Void .ctor()
  RVA=0x04D8D1C0  token=0x60002C3  UnityEngine.Transform <RegisterPorts>b__5_0()
  RVA=0x04D88590  token=0x60002C4  UnityEngine.GameObject <RegisterPorts>b__5_1()
END_CLASS

CLASS: FlowCanvas.Nodes.TriggerEvents
TYPE:  class
TOKEN: 0x2000072
SIZE:  0xF0
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           onEnter  // 0xc8
  private           FlowCanvas.FlowOutput           onStay  // 0xd0
  private           FlowCanvas.FlowOutput           onExit  // 0xd8
  private           UnityEngine.Collider            receiver  // 0xe0
  private           UnityEngine.GameObject          other  // 0xe8
METHODS:
  RVA=0x0A924124  token=0x60002C5  System.Void RegisterPorts()
  RVA=0x0A9242EC  token=0x60002C6  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A924AFC  token=0x60002C7  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A923E84  token=0x60002C8  System.Void OnTriggerEnter(ParadoxNotion.EventData<UnityEngine.Collider> msg)
  RVA=0x0A924044  token=0x60002C9  System.Void OnTriggerStay(ParadoxNotion.EventData<UnityEngine.Collider> msg)
  RVA=0x0A923F64  token=0x60002CA  System.Void OnTriggerExit(ParadoxNotion.EventData<UnityEngine.Collider> msg)
  RVA=0x0A924C18  token=0x60002CB  System.Void .ctor()
  RVA=0x04D8D1C0  token=0x60002CC  UnityEngine.Collider <RegisterPorts>b__5_0()
  RVA=0x04D88590  token=0x60002CD  UnityEngine.GameObject <RegisterPorts>b__5_1()
END_CLASS

CLASS: FlowCanvas.Nodes.UIButtonEvent
TYPE:  class
TOKEN: 0x2000073
SIZE:  0xC0
EXTENDS: FlowCanvas.Nodes.EventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           o  // 0xb8
METHODS:
  RVA=0x0A92598C  token=0x60002CE  System.Void OnPostGraphStarted()
  RVA=0x0A9258B8  token=0x60002CF  System.Void OnGraphStoped()
  RVA=0x0A925A7C  token=0x60002D0  System.Void RegisterPorts()
  RVA=0x0A925868  token=0x60002D1  System.Void OnClick()
  RVA=0x0A925BB4  token=0x60002D2  System.Void .ctor()
  RVA=0x0A925B6C  token=0x60002D3  UnityEngine.UI.Button <RegisterPorts>b__3_0()
END_CLASS

CLASS: FlowCanvas.Nodes.UIDropdownEvent
TYPE:  class
TOKEN: 0x2000074
SIZE:  0xC8
EXTENDS: FlowCanvas.Nodes.EventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           o  // 0xb8
  private           System.Int32                    value  // 0xc0
METHODS:
  RVA=0x0A925CCC  token=0x60002D4  System.Void OnPostGraphStarted()
  RVA=0x0A925BE8  token=0x60002D5  System.Void OnGraphStoped()
  RVA=0x0A925E20  token=0x60002D6  System.Void RegisterPorts()
  RVA=0x0A925DCC  token=0x60002D7  System.Void OnValueChanged(System.Int32 value)
  RVA=0x0A925FDC  token=0x60002D8  System.Void .ctor()
  RVA=0x0A925F94  token=0x60002D9  UnityEngine.UI.Dropdown <RegisterPorts>b__4_0()
  RVA=0x04DA4B70  token=0x60002DA  System.Int32 <RegisterPorts>b__4_1()
END_CLASS

CLASS: FlowCanvas.Nodes.UIInputFieldEvent
TYPE:  class
TOKEN: 0x2000075
SIZE:  0xD0
EXTENDS: FlowCanvas.Nodes.EventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           onValueChanged  // 0xb8
  private           FlowCanvas.FlowOutput           onEndEdit  // 0xc0
  private           System.String                   value  // 0xc8
METHODS:
  RVA=0x0A9261E0  token=0x60002DB  System.Void OnPostGraphStarted()
  RVA=0x0A926074  token=0x60002DC  System.Void OnGraphStoped()
  RVA=0x0A9263CC  token=0x60002DD  System.Void RegisterPorts()
  RVA=0x0A926368  token=0x60002DE  System.Void OnValueChanged(System.String value)
  RVA=0x0A926010  token=0x60002DF  System.Void OnEndEdit(System.String value)
  RVA=0x0A9265C4  token=0x60002E0  System.Void .ctor()
  RVA=0x0A92657C  token=0x60002E1  UnityEngine.UI.InputField <RegisterPorts>b__5_0()
  RVA=0x04D8D1D0  token=0x60002E2  System.String <RegisterPorts>b__5_1()
END_CLASS

CLASS: FlowCanvas.Nodes.UIPointerEvents
TYPE:  class
TOKEN: 0x2000076
SIZE:  0x128
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           onPointerDown  // 0xc8
  private           FlowCanvas.FlowOutput           onPointerPressed  // 0xd0
  private           FlowCanvas.FlowOutput           onPointerUp  // 0xd8
  private           FlowCanvas.FlowOutput           onPointerEnter  // 0xe0
  private           FlowCanvas.FlowOutput           onPointerExit  // 0xe8
  private           FlowCanvas.FlowOutput           onPointerClick  // 0xf0
  private           FlowCanvas.FlowOutput           onPointerDrag  // 0xf8
  private           FlowCanvas.FlowOutput           onPointerDrop  // 0x100
  private           FlowCanvas.FlowOutput           onPointerScroll  // 0x108
  private           UnityEngine.GameObject          receiver  // 0x110
  private           UnityEngine.EventSystems.PointerEventDataeventData  // 0x118
  private           System.Boolean                  updatePressed  // 0x120
METHODS:
  RVA=0x0A926D44  token=0x60002E3  System.Void RegisterPorts()
  RVA=0x0A92707C  token=0x60002E4  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A9272FC  token=0x60002E5  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A9266DC  token=0x60002E6  System.Void OnPointerDown(ParadoxNotion.EventData<UnityEngine.EventSystems.PointerEventData> msg)
  RVA=0x0A926C54  token=0x60002E7  System.Void OnPointerUp(ParadoxNotion.EventData<UnityEngine.EventSystems.PointerEventData> msg)
  RVA=0x0A92757C  token=0x60002E8  System.Collections.IEnumerator UpdatePressed()
  RVA=0x0A9269A8  token=0x60002E9  System.Void OnPointerEnter(ParadoxNotion.EventData<UnityEngine.EventSystems.PointerEventData> msg)
  RVA=0x0A926A8C  token=0x60002EA  System.Void OnPointerExit(ParadoxNotion.EventData<UnityEngine.EventSystems.PointerEventData> msg)
  RVA=0x0A9265F8  token=0x60002EB  System.Void OnPointerClick(ParadoxNotion.EventData<UnityEngine.EventSystems.PointerEventData> msg)
  RVA=0x0A9267E0  token=0x60002EC  System.Void OnPointerDrag(ParadoxNotion.EventData<UnityEngine.EventSystems.PointerEventData> msg)
  RVA=0x0A9268C4  token=0x60002ED  System.Void OnPointerDrop(ParadoxNotion.EventData<UnityEngine.EventSystems.PointerEventData> msg)
  RVA=0x0A926B70  token=0x60002EE  System.Void OnPointerScroll(ParadoxNotion.EventData<UnityEngine.EventSystems.PointerEventData> msg)
  RVA=0x0A9275D8  token=0x60002EF  System.Void .ctor()
  RVA=0x04D8D1B0  token=0x60002F0  UnityEngine.GameObject <RegisterPorts>b__12_0()
  RVA=0x04D85F80  token=0x60002F1  UnityEngine.EventSystems.PointerEventData <RegisterPorts>b__12_1()
END_CLASS

CLASS: FlowCanvas.Nodes.UIScrollbarEvent
TYPE:  class
TOKEN: 0x2000078
SIZE:  0xC8
EXTENDS: FlowCanvas.Nodes.EventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           o  // 0xb8
  private           System.Single                   value  // 0xc0
METHODS:
  RVA=0x0A93AC54  token=0x60002F8  System.Void OnPostGraphStarted()
  RVA=0x0A93AB70  token=0x60002F9  System.Void OnGraphStoped()
  RVA=0x0A93ADAC  token=0x60002FA  System.Void RegisterPorts()
  RVA=0x0A93AD54  token=0x60002FB  System.Void OnValueChanged(System.Single value)
  RVA=0x0A93AF68  token=0x60002FC  System.Void .ctor()
  RVA=0x0A93AF20  token=0x60002FD  UnityEngine.UI.Scrollbar <RegisterPorts>b__4_0()
  RVA=0x04D9DF30  token=0x60002FE  System.Single <RegisterPorts>b__4_1()
END_CLASS

CLASS: FlowCanvas.Nodes.UISliderEvent
TYPE:  class
TOKEN: 0x2000079
SIZE:  0xC8
EXTENDS: FlowCanvas.Nodes.EventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           o  // 0xb8
  private           System.Single                   value  // 0xc0
METHODS:
  RVA=0x0A93B080  token=0x60002FF  System.Void OnPostGraphStarted()
  RVA=0x0A93AF9C  token=0x6000300  System.Void OnGraphStoped()
  RVA=0x0A93B180  token=0x6000301  System.Void RegisterPorts()
  RVA=0x0A93AD54  token=0x6000302  System.Void OnValueChanged(System.Single value)
  RVA=0x0A93B33C  token=0x6000303  System.Void .ctor()
  RVA=0x0A93B2F4  token=0x6000304  UnityEngine.UI.Slider <RegisterPorts>b__4_0()
  RVA=0x04D9DF30  token=0x6000305  System.Single <RegisterPorts>b__4_1()
END_CLASS

CLASS: FlowCanvas.Nodes.UIToggleEvent
TYPE:  class
TOKEN: 0x200007A
SIZE:  0xC8
EXTENDS: FlowCanvas.Nodes.EventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           o  // 0xb8
  private           System.Boolean                  state  // 0xc0
METHODS:
  RVA=0x0A93B454  token=0x6000306  System.Void OnPostGraphStarted()
  RVA=0x0A93B370  token=0x6000307  System.Void OnGraphStoped()
  RVA=0x0A93B5A8  token=0x6000308  System.Void RegisterPorts()
  RVA=0x0A93B554  token=0x6000309  System.Void OnValueChanged(System.Boolean state)
  RVA=0x0A93B764  token=0x600030A  System.Void .ctor()
  RVA=0x0A93B71C  token=0x600030B  UnityEngine.UI.Toggle <RegisterPorts>b__4_0()
  RVA=0x04D91920  token=0x600030C  System.Boolean <RegisterPorts>b__4_1()
END_CLASS

CLASS: FlowCanvas.Nodes.VisibilityEvents
TYPE:  class
TOKEN: 0x200007B
SIZE:  0xE0
EXTENDS: FlowCanvas.Nodes.RouterEventNode`1
FIELDS:
  private           FlowCanvas.FlowOutput           onVisible  // 0xc8
  private           FlowCanvas.FlowOutput           onInvisible  // 0xd0
  private           UnityEngine.GameObject          receiver  // 0xd8
METHODS:
  RVA=0x0A93BFFC  token=0x600030D  System.Void RegisterPorts()
  RVA=0x0A93C124  token=0x600030E  System.Void Subscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A93C1F8  token=0x600030F  System.Void UnSubscribe(ParadoxNotion.Services.EventRouter router)
  RVA=0x0A93BF48  token=0x6000310  System.Void OnBecameVisible(ParadoxNotion.EventData msg)
  RVA=0x0A93BE94  token=0x6000311  System.Void OnBecameInvisible(ParadoxNotion.EventData msg)
  RVA=0x0A93C2CC  token=0x6000312  System.Void .ctor()
  RVA=0x04D88EF0  token=0x6000313  UnityEngine.GameObject <RegisterPorts>b__3_0()
END_CLASS

CLASS: FlowCanvas.Nodes.ConditionalUpdateEvent
TYPE:  class
TOKEN: 0x200007C
SIZE:  0xD0
EXTENDS: FlowCanvas.Nodes.EventNode
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           FlowCanvas.FlowOutput           becameTrue  // 0xb0
  private           FlowCanvas.FlowOutput           becameFalse  // 0xb8
  private           FlowCanvas.ValueInput<System.Boolean>condition  // 0xc0
  private           System.Boolean                  lastState  // 0xc8
METHODS:
  RVA=0x0A92AB9C  token=0x6000314  System.Void RegisterPorts()
  RVA=0x0A92AC80  token=0x6000315  System.Void Update()
  RVA=0x0489BC70  token=0x6000316  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.DebugEvent
TYPE:  class
TOKEN: 0x200007D
SIZE:  0xB0
EXTENDS: FlowCanvas.Nodes.EventNode
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000317  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x6000318  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.OscillatorEvent
TYPE:  class
TOKEN: 0x200007E
SIZE:  0xD0
EXTENDS: FlowCanvas.Nodes.EventNode
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.AnimationCurve>curve  // 0xb0
  private           System.Single                   time  // 0xb8
  private           System.Single                   value  // 0xbc
  private           FlowCanvas.FlowOutput           hi  // 0xc0
  private           FlowCanvas.FlowOutput           low  // 0xc8
METHODS:
  RVA=0x0A92FE3C  token=0x6000319  System.Void .ctor()
  RVA=0x0A92FC1C  token=0x600031A  System.Void RegisterPorts()
  RVA=0x04DA6520  token=0x600031B  System.Void OnGraphStarted()
  RVA=0x0A92FD44  token=0x600031C  System.Void Update()
  RVA=0x04DA62F0  token=0x600031D  System.Single <RegisterPorts>b__6_0()
END_CLASS

CLASS: FlowCanvas.Nodes.VariableChangedEvent
TYPE:  class
TOKEN: 0x200007F
SIZE:  0xC8
EXTENDS: FlowCanvas.Nodes.EventNode
FIELDS:
  public            NodeCanvas.Framework.Internal.BBObjectParametertargetVariable  // 0xb0
  private           FlowCanvas.FlowOutput           fOut  // 0xb8
  private           System.Object                   newValue  // 0xc0
PROPERTIES:
  name  get=0x0A93BB04
METHODS:
  RVA=0x0A93B798  token=0x600031F  System.Void OnGraphStarted()
  RVA=0x0A93B88C  token=0x6000320  System.Void OnGraphStoped()
  RVA=0x0A93B9F8  token=0x6000321  System.Void RegisterPorts()
  RVA=0x0A93B940  token=0x6000322  System.Void OnValueChanged(System.Object value)
  RVA=0x0A93B9A4  token=0x6000323  System.Void OnVariableRefChange(NodeCanvas.Framework.Variable newVarRef)
  RVA=0x0489BC70  token=0x6000324  System.Void .ctor()
  RVA=0x04D892F0  token=0x6000325  System.Object <RegisterPorts>b__7_0()
END_CLASS

CLASS: FlowCanvas.Nodes.Chance
TYPE:  class
TOKEN: 0x2000080
SIZE:  0xB0
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
METHODS:
  RVA=0x0A92A8E8  token=0x6000326  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x6000327  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Cooldown
TYPE:  class
TOKEN: 0x2000082
SIZE:  0xE0
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           UnityEngine.Coroutine           coroutine  // 0xb0
  private           System.Single                   remaining  // 0xb8
  private           System.Single                   remainingNormalized  // 0xbc
  private           FlowCanvas.FlowOutput           start  // 0xc0
  private           FlowCanvas.FlowOutput           finish  // 0xc8
  private           FlowCanvas.FlowOutput           update  // 0xd0
  private           FlowCanvas.ValueInput<System.Single>time  // 0xd8
PROPERTIES:
  name  get=0x0A92B22C
METHODS:
  RVA=0x0A92AEB0  token=0x600032B  System.Void OnGraphStarted()
  RVA=0x0A92ADDC  token=0x600032C  System.Void OnGraphStoped()
  RVA=0x0A92AECC  token=0x600032D  System.Void RegisterPorts()
  RVA=0x0A92AD6C  token=0x600032E  System.Void Begin(FlowCanvas.Flow f)
  RVA=0x0A92ADDC  token=0x600032F  System.Void Cancel(FlowCanvas.Flow f)
  RVA=0x0A92AE24  token=0x6000330  System.Collections.IEnumerator CountDown(FlowCanvas.Flow f)
  RVA=0x0489BC70  token=0x6000331  System.Void .ctor()
  RVA=0x0A92B208  token=0x6000332  System.Single <RegisterPorts>b__11_0()
  RVA=0x0A92B21C  token=0x6000333  System.Single <RegisterPorts>b__11_1()
END_CLASS

CLASS: FlowCanvas.Nodes.DoOnce
TYPE:  class
TOKEN: 0x2000084
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.Boolean                  called  // 0xb0
METHODS:
  RVA=0x04DA6460  token=0x600033A  System.Void OnGraphStarted()
  RVA=0x0A92CAB8  token=0x600033B  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x600033C  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ReadFlowParameter`1
TYPE:  class
TOKEN: 0x2000086
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           T                               flowValue  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000340  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x6000341  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.WriteFlowParameter`1
TYPE:  class
TOKEN: 0x2000088
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000345  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x6000346  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.LatchBool
TYPE:  class
TOKEN: 0x200008A
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.Boolean                  latched  // 0xb0
METHODS:
  RVA=0x0A92E744  token=0x6000349  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x600034A  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.LatchInt
TYPE:  class
TOKEN: 0x200008C
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.Int32                    _portCount  // 0xb0
  private           System.Int32                    latched  // 0xb4
METHODS:
  RVA=0x0A92E944  token=0x600034F  System.Void RegisterPorts()
  RVA=0x091C2030  token=0x6000350  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ANDMerge
TYPE:  class
TOKEN: 0x200008F
SIZE:  0xD0
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.Int32                    _portCount  // 0xb0
  public            System.Single                   maxAwaitTime  // 0xb4
  private           FlowCanvas.FlowOutput           fOut  // 0xb8
  private           System.Single[]                 calls  // 0xc0
  private           System.Int32                    lastFrameCall  // 0xc8
METHODS:
  RVA=0x0A928E78  token=0x6000355  System.Void OnGraphStarted()
  RVA=0x0A928E80  token=0x6000356  System.Void RegisterPorts()
  RVA=0x0A928FFC  token=0x6000357  System.Void Reset()
  RVA=0x0A928D64  token=0x6000358  System.Void Check(System.Int32 index, FlowCanvas.Flow f)
  RVA=0x0A929040  token=0x6000359  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.AwaitAll
TYPE:  class
TOKEN: 0x2000091
SIZE:  0xD0
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.Int32                    _portCount  // 0xb0
  public            System.Single                   maxAwaitTime  // 0xb4
  private           FlowCanvas.FlowOutput           fOut  // 0xb8
  private           System.Single[]                 calls  // 0xc0
  private           System.Int32                    lastFrameCall  // 0xc8
METHODS:
  RVA=0x0A928E78  token=0x600035C  System.Void OnGraphStarted()
  RVA=0x0A9293B4  token=0x600035D  System.Void RegisterPorts()
  RVA=0x0A928FFC  token=0x600035E  System.Void Reset()
  RVA=0x0A928D64  token=0x600035F  System.Void Check(System.Int32 index, FlowCanvas.Flow f)
  RVA=0x0A929530  token=0x6000360  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ORMerge
TYPE:  class
TOKEN: 0x2000093
SIZE:  0xC8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.Int32                    _portCount  // 0xb0
  private           FlowCanvas.FlowOutput           fOut  // 0xb8
  private           System.Int32                    lastFrameCall  // 0xc0
METHODS:
  RVA=0x0A92F984  token=0x6000363  System.Void RegisterPorts()
  RVA=0x0A92F910  token=0x6000364  System.Void Check(System.Int32 index, FlowCanvas.Flow f)
  RVA=0x0A929040  token=0x6000365  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.XORMerge
TYPE:  class
TOKEN: 0x2000095
SIZE:  0xD0
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.Int32                    _portCount  // 0xb0
  private           FlowCanvas.FlowOutput           fOut  // 0xb8
  private           System.Int32[]                  calls  // 0xc0
  private           System.Int32                    lastFrameCall  // 0xc8
METHODS:
  RVA=0x0A93C9B4  token=0x6000368  System.Void RegisterPorts()
  RVA=0x0A93C8D4  token=0x6000369  System.Void Check(System.Int32 index, FlowCanvas.Flow f)
  RVA=0x0A929040  token=0x600036A  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.FlowControlNode
TYPE:  abstract class
TOKEN: 0x2000097
SIZE:  0xB0
EXTENDS: FlowCanvas.FlowNode
FIELDS:
METHODS:
  RVA=0x0489BC70  token=0x600036D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Break
TYPE:  class
TOKEN: 0x2000098
SIZE:  0xB0
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
METHODS:
  RVA=0x0A92A714  token=0x600036E  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x600036F  System.Void .ctor()
  RVA=0x0A92A7BC  token=0x6000370  System.Void <RegisterPorts>b__0_0(FlowCanvas.Flow f)
END_CLASS

CLASS: FlowCanvas.Nodes.ForEach
TYPE:  class
TOKEN: 0x2000099
SIZE:  0xC8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.Object                   currentObject  // 0xb0
  private           System.Int32                    currentIndex  // 0xb8
  private           System.Boolean                  broken  // 0xbc
  private           FlowCanvas.ValueInput<System.Collections.IEnumerable>enumerableInput  // 0xc0
METHODS:
  RVA=0x0A92D97C  token=0x6000371  System.Void RegisterPorts()
  RVA=0x0A92D7E8  token=0x6000372  System.Type GetNodeWildDefinitionType()
  RVA=0x0A92D838  token=0x6000373  System.Void OnPortConnected(FlowCanvas.Port port, FlowCanvas.Port otherPort)
  RVA=0x0489BC70  token=0x6000374  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ForEach`1
TYPE:  class
TOKEN: 0x200009B
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           T                               currentObject  // 0x0
  private           System.Int32                    currentIndex  // 0x0
  private           System.Boolean                  broken  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600037B  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x600037C  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ForLoop
TYPE:  class
TOKEN: 0x200009D
SIZE:  0xD8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  public            System.Boolean                  reverse  // 0xb0
  private           System.Int32                    current  // 0xb4
  private           System.Boolean                  broken  // 0xb8
  private           FlowCanvas.ValueInput<System.Int32>first  // 0xc0
  private           FlowCanvas.ValueInput<System.Int32>last  // 0xc8
  private           FlowCanvas.ValueInput<System.Int32>step  // 0xd0
METHODS:
  RVA=0x0A92DC78  token=0x6000383  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x6000384  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Dummy
TYPE:  class
TOKEN: 0x200009F
SIZE:  0xB0
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
PROPERTIES:
  name  get=0x011EC580
METHODS:
  RVA=0x0A92CC30  token=0x600038B  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x600038C  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Finish
TYPE:  class
TOKEN: 0x20000A1
SIZE:  0xB0
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
METHODS:
  RVA=0x0A92D1A8  token=0x600038F  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x6000390  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.GoToLabel
TYPE:  class
TOKEN: 0x20000A3
SIZE:  0xC0
EXTENDS: FlowCanvas.Nodes.FlowControlNode
IMPLEMENTS: FlowCanvas.IEditorMenuCallbackReceiver
FIELDS:
  public            System.String                   identifier  // 0xb0
  private           FlowCanvas.FlowOutput           <port>k__BackingField  // 0xb8
PROPERTIES:
  port  get=0x04D85EF0  set=0x06AC5AF4
  name  get=0x0A92E0FC
METHODS:
  RVA=0x0A92E048  token=0x6000396  System.Void RegisterPorts()
  RVA=0x0A92E0AC  token=0x6000397  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.GoToStatement
TYPE:  class
TOKEN: 0x20000A4
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
IMPLEMENTS: NodeCanvas.Framework.IHaveNodeReference NodeCanvas.Framework.IGraphElement
FIELDS:
  public            NodeCanvas.Framework.NodeReference<FlowCanvas.Nodes.GoToLabel>_targetLabel  // 0xb0
PROPERTIES:
  NodeCanvas.Framework.IHaveNodeReference.targetReference  get=0x04D86250
  target  get=0x0A92E38C
  name  get=0x0A92E2FC
METHODS:
  RVA=0x0A92E1FC  token=0x600039B  System.Void SetTarget(FlowCanvas.Nodes.GoToLabel newTarget)
  RVA=0x0A92E154  token=0x600039C  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x600039D  System.Void .ctor()
  RVA=0x0A92E288  token=0x600039E  System.Void <RegisterPorts>b__8_0(FlowCanvas.Flow f)
END_CLASS

CLASS: FlowCanvas.Nodes.Merge
TYPE:  class
TOKEN: 0x20000A5
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.Int32                    _portCount  // 0xb0
METHODS:
  RVA=0x0A92F434  token=0x600039F  System.Void RegisterPorts()
  RVA=0x091C2030  token=0x60003A0  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Split
TYPE:  class
TOKEN: 0x20000A6
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.Int32                    _portCount  // 0xb0
METHODS:
  RVA=0x0A934CD8  token=0x60003A1  System.Void RegisterPorts()
  RVA=0x091C2030  token=0x60003A2  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.TryCatch
TYPE:  class
TOKEN: 0x20000A8
SIZE:  0xB0
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
METHODS:
  RVA=0x0A936888  token=0x60003A5  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x60003A6  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CoroutineState
TYPE:  class
TOKEN: 0x20000AA
SIZE:  0xC0
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.Boolean                  active  // 0xb0
  private           UnityEngine.Coroutine           coroutine  // 0xb8
METHODS:
  RVA=0x0A92B380  token=0x60003A9  System.Void OnGraphStoped()
  RVA=0x0A92B3B0  token=0x60003AA  System.Void RegisterPorts()
  RVA=0x0A92B2B0  token=0x60003AB  System.Collections.IEnumerator DoRepeat(FlowCanvas.FlowOutput fStart, FlowCanvas.FlowOutput fUpdate, FlowCanvas.FlowOutput fFinish, FlowCanvas.Flow f)
  RVA=0x0489BC70  token=0x60003AC  System.Void .ctor()
  RVA=0x04DA6460  token=0x60003AD  System.Void <DoRepeat>b__4_0()
END_CLASS

CLASS: FlowCanvas.Nodes.While
TYPE:  class
TOKEN: 0x20000AD
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           UnityEngine.Coroutine           coroutine  // 0xb0
METHODS:
  RVA=0x0A93C6F0  token=0x60003B7  System.Void OnGraphStarted()
  RVA=0x0A93C708  token=0x60003B8  System.Void OnGraphStoped()
  RVA=0x0A93C74C  token=0x60003B9  System.Void RegisterPorts()
  RVA=0x0A93C620  token=0x60003BA  System.Collections.IEnumerator DoWhile(FlowCanvas.FlowOutput fUpdate, FlowCanvas.FlowOutput fFinish, FlowCanvas.Flow f, FlowCanvas.ValueInput<System.Boolean> condition)
  RVA=0x0489BC70  token=0x60003BB  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SelectOnBool`1
TYPE:  class
TOKEN: 0x20000B1
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           FlowCanvas.ValueInput<System.Boolean>condition  // 0x0
  private           FlowCanvas.ValueInput<T>        isTrue  // 0x0
  private           FlowCanvas.ValueInput<T>        isFalse  // 0x0
  private           FlowCanvas.ValueOutput<T>       result  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60003C6  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x60003C7  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60003C8  T <RegisterPorts>b__4_0()
END_CLASS

CLASS: FlowCanvas.Nodes.SelectOnEnum`1
TYPE:  class
TOKEN: 0x20000B2
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           ParadoxNotion.Serialization.SerializedTypeInfo_type  // 0x0
PROPERTIES:
  type  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60003CB  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x60003CC  System.Type GetNodeWildDefinitionType()
  RVA=-1  // not resolved  token=0x60003CD  System.Void OnPortConnected(FlowCanvas.Port port, FlowCanvas.Port otherPort)
  RVA=-1  // not resolved  token=0x60003CE  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SelectOnInt`1
TYPE:  class
TOKEN: 0x20000B4
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.Int32                    _portCount  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60003D1  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x60003D2  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SelectOnString`1
TYPE:  class
TOKEN: 0x20000B6
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  public            System.Collections.Generic.List<System.String>stringCases  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60003D5  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x60003D6  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SelectOnTag`1
TYPE:  class
TOKEN: 0x20000B8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.String[]                 _tagNames  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60003D9  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x60003DA  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Random
TYPE:  class
TOKEN: 0x20000BA
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.Int32                    _portCount  // 0xb0
  private           System.Int32                    current  // 0xb4
METHODS:
  RVA=0x0A931BAC  token=0x60003DD  System.Void RegisterPorts()
  RVA=0x091C2030  token=0x60003DE  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SwitchBool
TYPE:  class
TOKEN: 0x20000BC
SIZE:  0xB0
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
METHODS:
  RVA=0x0A934E74  token=0x60003E2  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x60003E3  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SwitchComparison
TYPE:  class
TOKEN: 0x20000BE
SIZE:  0xB0
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
METHODS:
  RVA=0x0A935020  token=0x60003E6  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x60003E7  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SwitchEnum
TYPE:  class
TOKEN: 0x20000C0
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           ParadoxNotion.Serialization.SerializedTypeInfo_type  // 0xb0
PROPERTIES:
  type  get=0x091C020C  set=0x0A935694
METHODS:
  RVA=0x0A9353C8  token=0x60003EC  System.Void RegisterPorts()
  RVA=0x0A935268  token=0x60003ED  System.Type GetNodeWildDefinitionType()
  RVA=0x0A9352B8  token=0x60003EE  System.Void OnPortConnected(FlowCanvas.Port port, FlowCanvas.Port otherPort)
  RVA=0x0489BC70  token=0x60003EF  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SwitchInt
TYPE:  class
TOKEN: 0x20000C2
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.Int32                    _portCount  // 0xb0
METHODS:
  RVA=0x0A935A14  token=0x60003F2  System.Void RegisterPorts()
  RVA=0x091C2030  token=0x60003F3  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SwitchIntCustom
TYPE:  class
TOKEN: 0x20000C4
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  public            System.Collections.Generic.List<System.Int32>intCases  // 0xb0
METHODS:
  RVA=0x0A935744  token=0x60003F6  System.Void RegisterPorts()
  RVA=0x0A93598C  token=0x60003F7  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SwitchProbability
TYPE:  class
TOKEN: 0x20000C6
SIZE:  0xD8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.Int32                    _portCount  // 0xb0
  private           System.Collections.Generic.List<FlowCanvas.ValueInput<System.Single>>probabilityValues  // 0xb8
  private           System.Collections.Generic.List<FlowCanvas.FlowOutput>probabilityOuts  // 0xc0
  private           System.Single[]                 cacheValues  // 0xc8
  private           System.Int32                    current  // 0xd0
METHODS:
  RVA=0x0A935DB8  token=0x60003FA  System.Void RegisterPorts()
  RVA=0x0A935BFC  token=0x60003FB  System.Void Enter(FlowCanvas.Flow f)
  RVA=0x0A929040  token=0x60003FC  System.Void .ctor()
  RVA=0x04D9E810  token=0x60003FD  System.Int32 <RegisterPorts>b__5_0()
END_CLASS

CLASS: FlowCanvas.Nodes.SwitchString
TYPE:  class
TOKEN: 0x20000C7
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  public            System.Collections.Generic.List<System.String>comparisonOutputs  // 0xb0
METHODS:
  RVA=0x0A9360EC  token=0x60003FE  System.Void RegisterPorts()
  RVA=0x0A936338  token=0x60003FF  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SwitchTag
TYPE:  class
TOKEN: 0x20000C9
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.String[]                 _tagNames  // 0xb0
METHODS:
  RVA=0x0A9363C0  token=0x6000402  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x6000403  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.FlipFlop
TYPE:  class
TOKEN: 0x20000CB
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  public            System.Boolean                  isFlip  // 0xb0
  private           System.Boolean                  original  // 0xb1
PROPERTIES:
  name  get=0x0A92D50C
METHODS:
  RVA=0x04DA6470  token=0x6000407  System.Void OnGraphStarted()
  RVA=0x04DA6480  token=0x6000408  System.Void OnGraphStoped()
  RVA=0x0A92D2CC  token=0x6000409  System.Void RegisterPorts()
  RVA=0x0A92D4FC  token=0x600040A  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.MultiToggle
TYPE:  class
TOKEN: 0x20000CD
SIZE:  0xC0
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.Int32                    _portCount  // 0xb0
  public            System.Int32                    current  // 0xb4
  private           System.Int32                    original  // 0xb8
PROPERTIES:
  name  get=0x0A92F85C
METHODS:
  RVA=0x0A92F530  token=0x6000410  System.Void OnGraphStarted()
  RVA=0x04DA6510  token=0x6000411  System.Void OnGraphStoped()
  RVA=0x0A92F56C  token=0x6000412  System.Void RegisterPorts()
  RVA=0x091C2030  token=0x6000413  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Sequence
TYPE:  class
TOKEN: 0x20000CF
SIZE:  0xC0
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.Int32                    _portCount  // 0xb0
  public            System.Int32                    current  // 0xb4
  private           System.Int32                    original  // 0xb8
PROPERTIES:
  name  get=0x0A9345F0
METHODS:
  RVA=0x0A92F530  token=0x6000419  System.Void OnGraphStarted()
  RVA=0x04DA6510  token=0x600041A  System.Void OnGraphStoped()
  RVA=0x0A934364  token=0x600041C  System.Void RegisterPorts()
  RVA=0x091C2030  token=0x600041D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Toggle
TYPE:  class
TOKEN: 0x20000D1
SIZE:  0xB8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  public            System.Boolean                  open  // 0xb0
  private           System.Boolean                  original  // 0xb1
PROPERTIES:
  name  get=0x0A936810
METHODS:
  RVA=0x04DA6470  token=0x6000423  System.Void OnGraphStarted()
  RVA=0x04DA6480  token=0x6000424  System.Void OnGraphStoped()
  RVA=0x0A936598  token=0x6000425  System.Void RegisterPorts()
  RVA=0x0A92D4FC  token=0x6000426  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CustomFunctionCall
TYPE:  class
TOKEN: 0x20000D3
SIZE:  0xD8
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  private           System.String                   _sourceOutputUID  // 0xb0
  private           FlowCanvas.ValueInput[]         portArgs  // 0xb8
  private           System.Object[]                 objectArgs  // 0xc0
  private           FlowCanvas.FlowOutput           fOut  // 0xc8
  private           System.WeakReference<FlowCanvas.Nodes.CustomFunctionEvent>_sourceFunctionRef  // 0xd0
PROPERTIES:
  sourceFunctionUID  get=0x04D86250  set=0x04D84CE0
  sourceFunction  get=0x0A92BC2C
  name  get=0x0A92BBA4
  description  get=0x0A92BB40
METHODS:
  RVA=0x0A92BA48  token=0x6000431  System.Void SetFunction(FlowCanvas.Nodes.CustomFunctionEvent func)
  RVA=0x0A92B72C  token=0x6000432  System.Void RegisterPorts()
  RVA=0x0A92B580  token=0x6000433  System.Void Invoke(FlowCanvas.Flow f)
  RVA=0x0489BC70  token=0x6000434  System.Void .ctor()
  RVA=0x0A92BB08  token=0x6000435  System.Boolean <get_sourceFunction>b__9_0(FlowCanvas.Nodes.CustomFunctionEvent i)
END_CLASS

CLASS: FlowCanvas.Nodes.CustomFunctionEvent
TYPE:  class
TOKEN: 0x20000D4
SIZE:  0xE0
EXTENDS: FlowCanvas.Nodes.EventNode
IMPLEMENTS: NodeCanvas.Framework.IInvokable NodeCanvas.Framework.IGraphElement FlowCanvas.IEditorMenuCallbackReceiver
FIELDS:
  public            System.String                   identifier  // 0xb0
  private           System.Collections.Generic.List<ParadoxNotion.DynamicParameterDefinition>_parameters  // 0xb8
  private           ParadoxNotion.DynamicParameterDefinition_returns  // 0xc0
  private           System.Object[]                 args  // 0xc8
  private           System.Object                   returnValue  // 0xd0
  private           FlowCanvas.FlowOutput           onInvoke  // 0xd8
PROPERTIES:
  parameters  get=0x04D85EF0  set=0x06AC5AF4
  returns  get=0x04D892F0  set=0x06AC5ACC
  returnType  get=0x0A92C884
  parameterTypes  get=0x0A92C75C
  name  get=0x0A92C71C
METHODS:
  RVA=0x0A92C41C  token=0x600043D  System.Void OnGraphStoped()
  RVA=0x0A92C450  token=0x600043E  System.Void RegisterPorts()
  RVA=0x04D86250  token=0x600043F  System.String NodeCanvas.Framework.IInvokable.GetInvocationID()
  RVA=0x0A92C3DC  token=0x6000440  System.Object NodeCanvas.Framework.IInvokable.Invoke(System.Object[] args)
  RVA=0x0A92C2CC  token=0x6000441  System.Void NodeCanvas.Framework.IInvokable.InvokeAsync(System.Action<System.Object> callback, System.Object[] args)
  RVA=0x0A92C1C4  token=0x6000442  System.Object Invoke(FlowCanvas.Flow f, System.Object[] args)
  RVA=0x0A92C040  token=0x6000443  System.Void InvokeAsync(FlowCanvas.Flow f, FlowCanvas.FlowHandler flowCallback, System.Object[] args)
  RVA=0x04D876B0  token=0x6000444  System.Object GetReturnValue()
  RVA=0x0A92BD88  token=0x6000445  System.Void AddParameter(System.Type type)
  RVA=0x0A92BE64  token=0x6000446  System.Void GatherPortsUpdateRefs()
  RVA=0x0A92C61C  token=0x6000447  System.Void .ctor()
  RVA=0x0678D7BC  token=0x6000448  System.Void <Invoke>b__23_0(System.Object o)
  RVA=0x0A92C5EC  token=0x6000449  System.Boolean <GatherPortsUpdateRefs>b__27_0(FlowCanvas.Nodes.CustomFunctionCall n)
END_CLASS

CLASS: FlowCanvas.Nodes.Return
TYPE:  class
TOKEN: 0x20000D9
SIZE:  0xC0
EXTENDS: FlowCanvas.Nodes.FlowControlNode
FIELDS:
  public            System.Boolean                  useReturnValue  // 0xb0
  private           FlowCanvas.ValueInput<System.Object>returnPort  // 0xb8
METHODS:
  RVA=0x0A934110  token=0x6000453  System.Void RegisterPorts()
  RVA=0x0A92D4FC  token=0x6000454  System.Void .ctor()
  RVA=0x0A934208  token=0x6000455  System.Void <RegisterPorts>b__2_0(FlowCanvas.Flow f)
END_CLASS

CLASS: FlowCanvas.Nodes.CallableActionNodeBase
TYPE:  abstract class
TOKEN: 0x20000DA
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.SimplexNode
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000456  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableActionNode
TYPE:  abstract class
TOKEN: 0x20000DB
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.CallableActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000457  System.Void Invoke()
  RVA=0x0A92A7D8  token=0x6000458  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=0x041E1670  token=0x6000459  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableActionNode`1
TYPE:  abstract class
TOKEN: 0x20000DD
EXTENDS: FlowCanvas.Nodes.CallableActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600045C  System.Void Invoke(T1 a)
  RVA=-1  // not resolved  token=0x600045D  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x600045E  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableActionNode`2
TYPE:  abstract class
TOKEN: 0x20000DF
EXTENDS: FlowCanvas.Nodes.CallableActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000461  System.Void Invoke(T1 a, T2 b)
  RVA=-1  // not resolved  token=0x6000462  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x6000463  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableActionNode`3
TYPE:  abstract class
TOKEN: 0x20000E1
EXTENDS: FlowCanvas.Nodes.CallableActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000466  System.Void Invoke(T1 a, T2 b, T3 c)
  RVA=-1  // not resolved  token=0x6000467  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x6000468  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableActionNode`4
TYPE:  abstract class
TOKEN: 0x20000E3
EXTENDS: FlowCanvas.Nodes.CallableActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600046B  System.Void Invoke(T1 a, T2 b, T3 c, T4 d)
  RVA=-1  // not resolved  token=0x600046C  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x600046D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableActionNode`5
TYPE:  abstract class
TOKEN: 0x20000E5
EXTENDS: FlowCanvas.Nodes.CallableActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000470  System.Void Invoke(T1 a, T2 b, T3 c, T4 d, T5 e)
  RVA=-1  // not resolved  token=0x6000471  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x6000472  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableActionNode`6
TYPE:  abstract class
TOKEN: 0x20000E7
EXTENDS: FlowCanvas.Nodes.CallableActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000475  System.Void Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f)
  RVA=-1  // not resolved  token=0x6000476  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x6000477  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableActionNode`7
TYPE:  abstract class
TOKEN: 0x20000E9
EXTENDS: FlowCanvas.Nodes.CallableActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600047A  System.Void Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g)
  RVA=-1  // not resolved  token=0x600047B  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x600047C  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableActionNode`8
TYPE:  abstract class
TOKEN: 0x20000EB
EXTENDS: FlowCanvas.Nodes.CallableActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600047F  System.Void Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h)
  RVA=-1  // not resolved  token=0x6000480  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x6000481  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableActionNode`9
TYPE:  abstract class
TOKEN: 0x20000ED
EXTENDS: FlowCanvas.Nodes.CallableActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000484  System.Void Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i)
  RVA=-1  // not resolved  token=0x6000485  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x6000486  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableActionNode`10
TYPE:  abstract class
TOKEN: 0x20000EF
EXTENDS: FlowCanvas.Nodes.CallableActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000489  System.Void Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j)
  RVA=-1  // not resolved  token=0x600048A  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x600048B  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableFunctionNodeBase
TYPE:  abstract class
TOKEN: 0x20000F1
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.SimplexNode
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600048E  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableFunctionNode`1
TYPE:  abstract class
TOKEN: 0x20000F2
EXTENDS: FlowCanvas.Nodes.CallableFunctionNodeBase
FIELDS:
  private           TResult                         result  // 0x0
METHODS:
  RVA=-1  // abstract  token=0x600048F  TResult Invoke()
  RVA=-1  // not resolved  token=0x6000490  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x6000491  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableFunctionNode`2
TYPE:  abstract class
TOKEN: 0x20000F4
EXTENDS: FlowCanvas.Nodes.CallableFunctionNodeBase
FIELDS:
  private           TResult                         result  // 0x0
METHODS:
  RVA=-1  // abstract  token=0x6000495  TResult Invoke(T1 a)
  RVA=-1  // not resolved  token=0x6000496  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x6000497  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableFunctionNode`3
TYPE:  abstract class
TOKEN: 0x20000F6
EXTENDS: FlowCanvas.Nodes.CallableFunctionNodeBase
FIELDS:
  private           TResult                         result  // 0x0
METHODS:
  RVA=-1  // abstract  token=0x600049B  TResult Invoke(T1 a, T2 b)
  RVA=-1  // not resolved  token=0x600049C  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x600049D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableFunctionNode`4
TYPE:  abstract class
TOKEN: 0x20000F8
EXTENDS: FlowCanvas.Nodes.CallableFunctionNodeBase
FIELDS:
  private           TResult                         result  // 0x0
METHODS:
  RVA=-1  // abstract  token=0x60004A1  TResult Invoke(T1 a, T2 b, T3 c)
  RVA=-1  // not resolved  token=0x60004A2  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x60004A3  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableFunctionNode`5
TYPE:  abstract class
TOKEN: 0x20000FA
EXTENDS: FlowCanvas.Nodes.CallableFunctionNodeBase
FIELDS:
  private           TResult                         result  // 0x0
METHODS:
  RVA=-1  // abstract  token=0x60004A7  TResult Invoke(T1 a, T2 b, T3 c, T4 d)
  RVA=-1  // not resolved  token=0x60004A8  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x60004A9  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableFunctionNode`6
TYPE:  abstract class
TOKEN: 0x20000FC
EXTENDS: FlowCanvas.Nodes.CallableFunctionNodeBase
FIELDS:
  private           TResult                         result  // 0x0
METHODS:
  RVA=-1  // abstract  token=0x60004AD  TResult Invoke(T1 a, T2 b, T3 c, T4 d, T5 e)
  RVA=-1  // not resolved  token=0x60004AE  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x60004AF  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableFunctionNode`7
TYPE:  abstract class
TOKEN: 0x20000FE
EXTENDS: FlowCanvas.Nodes.CallableFunctionNodeBase
FIELDS:
  private           TResult                         result  // 0x0
METHODS:
  RVA=-1  // abstract  token=0x60004B3  TResult Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f)
  RVA=-1  // not resolved  token=0x60004B4  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x60004B5  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableFunctionNode`8
TYPE:  abstract class
TOKEN: 0x2000100
EXTENDS: FlowCanvas.Nodes.CallableFunctionNodeBase
FIELDS:
  private           TResult                         result  // 0x0
METHODS:
  RVA=-1  // abstract  token=0x60004B9  TResult Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g)
  RVA=-1  // not resolved  token=0x60004BA  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x60004BB  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableFunctionNode`9
TYPE:  abstract class
TOKEN: 0x2000102
EXTENDS: FlowCanvas.Nodes.CallableFunctionNodeBase
FIELDS:
  private           TResult                         result  // 0x0
METHODS:
  RVA=-1  // abstract  token=0x60004BF  TResult Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h)
  RVA=-1  // not resolved  token=0x60004C0  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x60004C1  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableFunctionNode`10
TYPE:  abstract class
TOKEN: 0x2000104
EXTENDS: FlowCanvas.Nodes.CallableFunctionNodeBase
FIELDS:
  private           TResult                         result  // 0x0
METHODS:
  RVA=-1  // abstract  token=0x60004C5  TResult Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i)
  RVA=-1  // not resolved  token=0x60004C6  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x60004C7  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CallableFunctionNode`11
TYPE:  abstract class
TOKEN: 0x2000106
EXTENDS: FlowCanvas.Nodes.CallableFunctionNodeBase
FIELDS:
  private           TResult                         result  // 0x0
METHODS:
  RVA=-1  // abstract  token=0x60004CB  TResult Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j)
  RVA=-1  // not resolved  token=0x60004CC  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x60004CD  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractorNode
TYPE:  abstract class
TOKEN: 0x2000108
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.SimplexNode
FIELDS:
  private   static  System.Collections.Generic.Dictionary<System.Type,System.Type>_extractors  // static @ 0x0
METHODS:
  RVA=0x0A92CD34  token=0x60004D1  System.Type GetExtractorType(System.Type type)
  RVA=0x041E1670  token=0x60004D2  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractorNode`3
TYPE:  abstract class
TOKEN: 0x200010A
EXTENDS: FlowCanvas.Nodes.ExtractorNode
FIELDS:
  private           T1                              a  // 0x0
  private           T2                              b  // 0x0
METHODS:
  RVA=-1  // abstract  token=0x60004D6  System.Void Invoke(TInstance instance, T1& a, T2& b)
  RVA=-1  // not resolved  token=0x60004D7  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x60004D8  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractorNode`4
TYPE:  abstract class
TOKEN: 0x200010C
EXTENDS: FlowCanvas.Nodes.ExtractorNode
FIELDS:
  private           T1                              a  // 0x0
  private           T2                              b  // 0x0
  private           T3                              c  // 0x0
METHODS:
  RVA=-1  // abstract  token=0x60004DC  System.Void Invoke(TInstance instance, T1& a, T2& b, T3& c)
  RVA=-1  // not resolved  token=0x60004DD  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x60004DE  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractorNode`5
TYPE:  abstract class
TOKEN: 0x200010E
EXTENDS: FlowCanvas.Nodes.ExtractorNode
FIELDS:
  private           T1                              a  // 0x0
  private           T2                              b  // 0x0
  private           T3                              c  // 0x0
  private           T4                              d  // 0x0
METHODS:
  RVA=-1  // abstract  token=0x60004E3  System.Void Invoke(TInstance instance, T1& a, T2& b, T3& c, T4& d)
  RVA=-1  // not resolved  token=0x60004E4  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x60004E5  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractorNode`6
TYPE:  abstract class
TOKEN: 0x2000110
EXTENDS: FlowCanvas.Nodes.ExtractorNode
FIELDS:
  private           T1                              a  // 0x0
  private           T2                              b  // 0x0
  private           T3                              c  // 0x0
  private           T4                              d  // 0x0
  private           T5                              e  // 0x0
METHODS:
  RVA=-1  // abstract  token=0x60004EB  System.Void Invoke(TInstance instance, T1& a, T2& b, T3& c, T4& d, T5& e)
  RVA=-1  // not resolved  token=0x60004EC  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x60004ED  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractorNode`7
TYPE:  abstract class
TOKEN: 0x2000112
EXTENDS: FlowCanvas.Nodes.ExtractorNode
FIELDS:
  private           T1                              a  // 0x0
  private           T2                              b  // 0x0
  private           T3                              c  // 0x0
  private           T4                              d  // 0x0
  private           T5                              e  // 0x0
  private           T6                              f  // 0x0
METHODS:
  RVA=-1  // abstract  token=0x60004F4  System.Void Invoke(TInstance instance, T1& a, T2& b, T3& c, T4& d, T5& e, T6& f)
  RVA=-1  // not resolved  token=0x60004F5  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x60004F6  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.AddDictionaryItem`1
TYPE:  class
TOKEN: 0x2000114
EXTENDS: FlowCanvas.Nodes.CallableFunctionNode`4
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60004FE  System.Collections.Generic.IDictionary<System.String,T> Invoke(System.Collections.Generic.IDictionary<System.String,T> dict, System.String key, T item)
  RVA=-1  // not resolved  token=0x60004FF  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.GetDictionaryItem`1
TYPE:  class
TOKEN: 0x2000115
EXTENDS: FlowCanvas.Nodes.CallableFunctionNode`3
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000500  T Invoke(System.Collections.Generic.IDictionary<System.String,T> dict, System.String key)
  RVA=-1  // not resolved  token=0x6000501  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.RemoveDictionaryKey`1
TYPE:  class
TOKEN: 0x2000116
EXTENDS: FlowCanvas.Nodes.CallableFunctionNode`3
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000502  System.Collections.Generic.IDictionary<System.String,T> Invoke(System.Collections.Generic.IDictionary<System.String,T> dict, System.String key)
  RVA=-1  // not resolved  token=0x6000503  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ClearDictionary
TYPE:  class
TOKEN: 0x2000117
SIZE:  0x38
EXTENDS: FlowCanvas.Nodes.CallableFunctionNode`2
FIELDS:
METHODS:
  RVA=0x0A92AAFC  token=0x6000504  System.Collections.IDictionary Invoke(System.Collections.IDictionary dict)
  RVA=0x0A92AB24  token=0x6000505  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.TryGetValue`1
TYPE:  class
TOKEN: 0x2000118
EXTENDS: FlowCanvas.Nodes.CallableFunctionNode`3
FIELDS:
  private           System.Boolean                  <exists>k__BackingField  // 0x0
PROPERTIES:
  exists  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000508  T Invoke(System.Collections.Generic.IDictionary<System.String,T> dict, System.String key)
  RVA=-1  // not resolved  token=0x6000509  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.DictionaryContainsKey`1
TYPE:  class
TOKEN: 0x2000119
EXTENDS: FlowCanvas.Nodes.CallableFunctionNode`3
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600050A  System.Boolean Invoke(System.Collections.Generic.IDictionary<System.String,T> dict, System.String key)
  RVA=-1  // not resolved  token=0x600050B  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ClearList
TYPE:  class
TOKEN: 0x200011A
SIZE:  0x38
EXTENDS: FlowCanvas.Nodes.CallableFunctionNode`2
FIELDS:
METHODS:
  RVA=0x0A92AB4C  token=0x600050C  System.Collections.IList Invoke(System.Collections.IList list)
  RVA=0x0A92AB74  token=0x600050D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.AddListItem`1
TYPE:  class
TOKEN: 0x200011B
EXTENDS: FlowCanvas.Nodes.CallableFunctionNode`3
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600050E  System.Collections.Generic.IList<T> Invoke(System.Collections.Generic.List<T> list, T item)
  RVA=-1  // not resolved  token=0x600050F  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.InsertListItem`1
TYPE:  class
TOKEN: 0x200011C
EXTENDS: FlowCanvas.Nodes.CallableFunctionNode`4
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000510  System.Collections.Generic.IList<T> Invoke(System.Collections.Generic.List<T> list, System.Int32 index, T item)
  RVA=-1  // not resolved  token=0x6000511  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.RemoveListItem`1
TYPE:  class
TOKEN: 0x200011D
EXTENDS: FlowCanvas.Nodes.CallableFunctionNode`3
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000512  System.Collections.Generic.IList<T> Invoke(System.Collections.Generic.List<T> list, T item)
  RVA=-1  // not resolved  token=0x6000513  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.RemoveListItemAt`1
TYPE:  class
TOKEN: 0x200011E
EXTENDS: FlowCanvas.Nodes.CallableFunctionNode`3
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000514  System.Collections.Generic.IList<T> Invoke(System.Collections.Generic.List<T> list, System.Int32 index)
  RVA=-1  // not resolved  token=0x6000515  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SetListItem`1
TYPE:  class
TOKEN: 0x200011F
EXTENDS: FlowCanvas.Nodes.CallableFunctionNode`4
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000516  System.Collections.Generic.IList<T> Invoke(System.Collections.Generic.IList<T> list, System.Int32 index, T item)
  RVA=-1  // not resolved  token=0x6000517  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ShuffleList`1
TYPE:  class
TOKEN: 0x2000120
EXTENDS: FlowCanvas.Nodes.CallableFunctionNode`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000518  System.Collections.Generic.IList<T> Invoke(System.Collections.Generic.IList<T> list)
  RVA=-1  // not resolved  token=0x6000519  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.GetListItem`1
TYPE:  class
TOKEN: 0x2000121
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600051A  T Invoke(System.Collections.Generic.IList<T> list, System.Int32 index)
  RVA=-1  // not resolved  token=0x600051B  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.GetRandomListItem`1
TYPE:  class
TOKEN: 0x2000122
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600051C  T Invoke(System.Collections.Generic.IList<T> list)
  RVA=-1  // not resolved  token=0x600051D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.GetLastListItem`1
TYPE:  class
TOKEN: 0x2000123
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600051E  T Invoke(System.Collections.Generic.IList<T> list)
  RVA=-1  // not resolved  token=0x600051F  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.GetFirstListItem`1
TYPE:  class
TOKEN: 0x2000124
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000520  T Invoke(System.Collections.Generic.IList<T> list)
  RVA=-1  // not resolved  token=0x6000521  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.GetListItemIndex
TYPE:  class
TOKEN: 0x2000125
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92DFF8  token=0x6000522  System.Int32 Invoke(System.Collections.IList list, System.Object item)
  RVA=0x0A92E020  token=0x6000523  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.IsNotNull
TYPE:  class
TOKEN: 0x2000126
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`2
FIELDS:
METHODS:
  RVA=0x0A92E650  token=0x6000524  System.Boolean Invoke(System.Object OBJECT)
  RVA=0x0A92E670  token=0x6000525  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.IsOfType
TYPE:  class
TOKEN: 0x2000127
SIZE:  0x38
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
  private           System.Object                   <OBJECT>k__BackingField  // 0x30
PROPERTIES:
  OBJECT  get=0x011F36E0  set=0x05396200
METHODS:
  RVA=0x0A92E698  token=0x6000528  System.Boolean Invoke(System.Object OBJECT, System.Type type)
  RVA=0x0A92E71C  token=0x6000529  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.AnyGreaterThan
TYPE:  class
TOKEN: 0x2000128
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92924C  token=0x600052A  System.Boolean Invoke(System.IComparable a, System.IComparable b)
  RVA=0x0A92927C  token=0x600052B  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.AnyGreaterEqualThan
TYPE:  class
TOKEN: 0x2000129
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A9291D8  token=0x600052C  System.Boolean Invoke(System.IComparable a, System.IComparable b)
  RVA=0x0A929224  token=0x600052D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.AnyLessThan
TYPE:  class
TOKEN: 0x200012A
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A929318  token=0x600052E  System.Boolean Invoke(System.IComparable a, System.IComparable b)
  RVA=0x0A929348  token=0x600052F  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.AnyLessEqualThan
TYPE:  class
TOKEN: 0x200012B
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A9292A4  token=0x6000530  System.Boolean Invoke(System.IComparable a, System.IComparable b)
  RVA=0x0A9292F0  token=0x6000531  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.AnyEqual
TYPE:  class
TOKEN: 0x200012C
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92919C  token=0x6000532  System.Boolean Invoke(System.Object a, System.Object b)
  RVA=0x0A9291B0  token=0x6000533  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.AnyNotEqual
TYPE:  class
TOKEN: 0x200012D
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A929370  token=0x6000534  System.Boolean Invoke(System.Object a, System.Object b)
  RVA=0x0A92938C  token=0x6000535  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.FloatAdd
TYPE:  class
TOKEN: 0x200012E
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x04DA6490  token=0x6000536  System.Single Invoke(System.Single a, System.Single b)
  RVA=0x0A92D584  token=0x6000537  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.FloatSubtract
TYPE:  class
TOKEN: 0x200012F
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x04DA64C0  token=0x6000538  System.Single Invoke(System.Single a, System.Single b)
  RVA=0x0A92D7C0  token=0x6000539  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.FloatMultiply
TYPE:  class
TOKEN: 0x2000130
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x04DA1D40  token=0x600053A  System.Single Invoke(System.Single a, System.Single b)
  RVA=0x0A92D70C  token=0x600053B  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.FloatDivide
TYPE:  class
TOKEN: 0x2000131
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x06989EC8  token=0x600053C  System.Single Invoke(System.Single a, System.Single b)
  RVA=0x0A92D5AC  token=0x600053D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.FloatModulo
TYPE:  class
TOKEN: 0x2000132
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x04DA64B0  token=0x600053E  System.Single Invoke(System.Single value, System.Single mod)
  RVA=0x0A92D6E4  token=0x600053F  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.FloatGreaterThan
TYPE:  class
TOKEN: 0x2000133
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92D62C  token=0x6000540  System.Boolean Invoke(System.Single a, System.Single b)
  RVA=0x0A92D634  token=0x6000541  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.FloatGreaterEqualThan
TYPE:  class
TOKEN: 0x2000134
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92D5FC  token=0x6000542  System.Boolean Invoke(System.Single a, System.Single b)
  RVA=0x0A92D604  token=0x6000543  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.FloatLessThan
TYPE:  class
TOKEN: 0x2000135
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92D6B4  token=0x6000544  System.Boolean Invoke(System.Single a, System.Single b)
  RVA=0x0A92D6BC  token=0x6000545  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.FloatLessEqualThan
TYPE:  class
TOKEN: 0x2000136
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92D684  token=0x6000546  System.Boolean Invoke(System.Single a, System.Single b)
  RVA=0x0A92D68C  token=0x6000547  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.FloatEqual
TYPE:  class
TOKEN: 0x2000137
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x09617CE8  token=0x6000548  System.Boolean Invoke(System.Single a, System.Single b)
  RVA=0x0A92D5D4  token=0x6000549  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.FloatNotEqual
TYPE:  class
TOKEN: 0x2000138
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92D734  token=0x600054A  System.Boolean Invoke(System.Single a, System.Single b)
  RVA=0x0A92D744  token=0x600054B  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.FloatInvert
TYPE:  class
TOKEN: 0x2000139
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`2
FIELDS:
METHODS:
  RVA=0x04DA64A0  token=0x600054C  System.Single Invoke(System.Single value)
  RVA=0x0A92D65C  token=0x600054D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.FloatSnap
TYPE:  class
TOKEN: 0x200013A
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92D76C  token=0x600054E  System.Int32 Invoke(System.Single value, System.Int32 interval)
  RVA=0x0A92D798  token=0x600054F  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.IntegerAdd
TYPE:  class
TOKEN: 0x200013B
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x04DA64D0  token=0x6000550  System.Int32 Invoke(System.Int32 a, System.Int32 b)
  RVA=0x0A92E3DC  token=0x6000551  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.IntegerSubtract
TYPE:  class
TOKEN: 0x200013C
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x04DA64F0  token=0x6000552  System.Int32 Invoke(System.Int32 a, System.Int32 b)
  RVA=0x0A92E628  token=0x6000553  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.IntegerMultiply
TYPE:  class
TOKEN: 0x200013D
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x04DA1D30  token=0x6000554  System.Int32 Invoke(System.Int32 a, System.Int32 b)
  RVA=0x0A92E580  token=0x6000555  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.IntegerDivide
TYPE:  class
TOKEN: 0x200013E
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92E404  token=0x6000556  System.Int32 Invoke(System.Int32 a, System.Int32 b)
  RVA=0x0A92E414  token=0x6000557  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.IntegerModulo
TYPE:  class
TOKEN: 0x200013F
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92E54C  token=0x6000558  System.Int32 Invoke(System.Int32 value, System.Int32 mod)
  RVA=0x0A92E558  token=0x6000559  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.IntegerGreaterThan
TYPE:  class
TOKEN: 0x2000140
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92E494  token=0x600055A  System.Boolean Invoke(System.Int32 a, System.Int32 b)
  RVA=0x0A92E49C  token=0x600055B  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.IntegerGreaterEqualThan
TYPE:  class
TOKEN: 0x2000141
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92E464  token=0x600055C  System.Boolean Invoke(System.Int32 a, System.Int32 b)
  RVA=0x0A92E46C  token=0x600055D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.IntegerLessThan
TYPE:  class
TOKEN: 0x2000142
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92E51C  token=0x600055E  System.Boolean Invoke(System.Int32 a, System.Int32 b)
  RVA=0x0A92E524  token=0x600055F  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.IntegerLessEqualThan
TYPE:  class
TOKEN: 0x2000143
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92E4EC  token=0x6000560  System.Boolean Invoke(System.Int32 a, System.Int32 b)
  RVA=0x0A92E4F4  token=0x6000561  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.IntegerEqual
TYPE:  class
TOKEN: 0x2000144
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x09619760  token=0x6000562  System.Boolean Invoke(System.Int32 a, System.Int32 b)
  RVA=0x0A92E43C  token=0x6000563  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.IntegerNotEqual
TYPE:  class
TOKEN: 0x2000145
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92E5A8  token=0x6000564  System.Boolean Invoke(System.Int32 a, System.Int32 b)
  RVA=0x0A92E5B0  token=0x6000565  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.IntegerInvert
TYPE:  class
TOKEN: 0x2000146
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`2
FIELDS:
METHODS:
  RVA=0x04DA64E0  token=0x6000566  System.Int32 Invoke(System.Int32 value)
  RVA=0x0A92E4C4  token=0x6000567  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.IntegerSnap
TYPE:  class
TOKEN: 0x2000147
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92E5D8  token=0x6000568  System.Int32 Invoke(System.Int32 value, System.Int32 interval)
  RVA=0x0A92E600  token=0x6000569  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.BooleanEqual
TYPE:  class
TOKEN: 0x2000148
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x092FCD58  token=0x600056A  System.Boolean Invoke(System.Boolean a, System.Boolean b)
  RVA=0x0A92A6BC  token=0x600056B  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.BooleanNotEqual
TYPE:  class
TOKEN: 0x2000149
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92A6E4  token=0x600056C  System.Boolean Invoke(System.Boolean a, System.Boolean b)
  RVA=0x0A92A6EC  token=0x600056D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.AND
TYPE:  class
TOKEN: 0x200014A
SIZE:  0x40
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`1
FIELDS:
  private           FlowCanvas.ValueInput<System.Boolean>a  // 0x30
  private           FlowCanvas.ValueInput<System.Boolean>b  // 0x38
METHODS:
  RVA=0x0A929054  token=0x600056E  System.Boolean Invoke()
  RVA=0x0A9290B8  token=0x600056F  System.Void OnRegisterExtraPorts(FlowCanvas.FlowNode node)
  RVA=0x0A929174  token=0x6000570  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.OR
TYPE:  class
TOKEN: 0x200014B
SIZE:  0x40
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`1
FIELDS:
  private           FlowCanvas.ValueInput<System.Boolean>a  // 0x30
  private           FlowCanvas.ValueInput<System.Boolean>b  // 0x38
METHODS:
  RVA=0x0A92FAD0  token=0x6000571  System.Boolean Invoke()
  RVA=0x0A92FB38  token=0x6000572  System.Void OnRegisterExtraPorts(FlowCanvas.FlowNode node)
  RVA=0x0A92FBF4  token=0x6000573  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.XOR
TYPE:  class
TOKEN: 0x200014C
SIZE:  0x40
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`1
FIELDS:
  private           FlowCanvas.ValueInput<System.Boolean>a  // 0x30
  private           FlowCanvas.ValueInput<System.Boolean>b  // 0x38
METHODS:
  RVA=0x0A93CB30  token=0x6000574  System.Boolean Invoke()
  RVA=0x0A93CBA8  token=0x6000575  System.Void OnRegisterExtraPorts(FlowCanvas.FlowNode node)
  RVA=0x0A93CC64  token=0x6000576  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.NOT
TYPE:  class
TOKEN: 0x200014D
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`2
FIELDS:
METHODS:
  RVA=0x0A92F8E0  token=0x6000577  System.Boolean Invoke(System.Boolean value)
  RVA=0x0A92F8E8  token=0x6000578  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Vector3Equal
TYPE:  class
TOKEN: 0x200014E
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A93BC38  token=0x6000579  System.Boolean Invoke(UnityEngine.Vector3 a, UnityEngine.Vector3 b)
  RVA=0x0A93BC8C  token=0x600057A  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Vector3NotEqual
TYPE:  class
TOKEN: 0x200014F
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A93BD94  token=0x600057B  System.Boolean Invoke(UnityEngine.Vector3 a, UnityEngine.Vector3 b)
  RVA=0x0A93BDE8  token=0x600057C  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Vector3Add
TYPE:  class
TOKEN: 0x2000150
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A93BB54  token=0x600057D  UnityEngine.Vector3 Invoke(UnityEngine.Vector3 a, UnityEngine.Vector3 b)
  RVA=0x0A93BBB0  token=0x600057E  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Vector3Subtract
TYPE:  class
TOKEN: 0x2000151
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A93BE10  token=0x600057F  UnityEngine.Vector3 Invoke(UnityEngine.Vector3 a, UnityEngine.Vector3 b)
  RVA=0x0A93BE6C  token=0x6000580  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Vector3Multiply
TYPE:  class
TOKEN: 0x2000152
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A93BD28  token=0x6000581  UnityEngine.Vector3 Invoke(UnityEngine.Vector3 a, System.Single b)
  RVA=0x0A93BD6C  token=0x6000582  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Vector3Divide
TYPE:  class
TOKEN: 0x2000153
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A93BBD8  token=0x6000583  UnityEngine.Vector3 Invoke(UnityEngine.Vector3 a, System.Single b)
  RVA=0x0A93BC10  token=0x6000584  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Vector3Invert
TYPE:  class
TOKEN: 0x2000154
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`2
FIELDS:
METHODS:
  RVA=0x0A93BCB4  token=0x6000585  UnityEngine.Vector3 Invoke(UnityEngine.Vector3 value)
  RVA=0x0A93BD00  token=0x6000586  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.BitwiseAnd`1
TYPE:  class
TOKEN: 0x2000155
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000587  T Invoke(T a, T b)
  RVA=-1  // not resolved  token=0x6000588  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.BitwiseOr`1
TYPE:  class
TOKEN: 0x2000156
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000589  T Invoke(T a, T b)
  RVA=-1  // not resolved  token=0x600058A  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.BitwiseInvert`1
TYPE:  class
TOKEN: 0x2000157
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600058B  T Invoke(T a)
  RVA=-1  // not resolved  token=0x600058C  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Wait
TYPE:  class
TOKEN: 0x2000158
SIZE:  0x70
EXTENDS: FlowCanvas.Nodes.LatentActionNode`1
FIELDS:
  private           System.Single                   <timeLeft>k__BackingField  // 0x68
  private           System.Single                   <normalized>k__BackingField  // 0x6c
PROPERTIES:
  timeLeft  get=0x04D8F060  set=0x04D8F090
  normalized  get=0x04DA3230  set=0x04DA3330
METHODS:
  RVA=0x0A93C58C  token=0x6000591  System.Collections.IEnumerator Invoke(System.Single time)
  RVA=0x0A93C5F8  token=0x6000592  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.WaitForOneFrame
TYPE:  class
TOKEN: 0x200015A
SIZE:  0x68
EXTENDS: FlowCanvas.Nodes.LatentActionNode
FIELDS:
PROPERTIES:
  allowRoutineQueueing  get=0x012081B0
METHODS:
  RVA=0x0A93C344  token=0x600059A  System.Collections.IEnumerator Invoke()
  RVA=0x041E1670  token=0x600059B  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.WaitForPhysicsFrame
TYPE:  class
TOKEN: 0x200015C
SIZE:  0x68
EXTENDS: FlowCanvas.Nodes.LatentActionNode
FIELDS:
PROPERTIES:
  allowRoutineQueueing  get=0x012081B0
METHODS:
  RVA=0x0A93C388  token=0x60005A3  System.Collections.IEnumerator Invoke()
  RVA=0x041E1670  token=0x60005A4  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.WaitForEndOfFrame
TYPE:  class
TOKEN: 0x200015E
SIZE:  0x68
EXTENDS: FlowCanvas.Nodes.LatentActionNode
FIELDS:
PROPERTIES:
  allowRoutineQueueing  get=0x012081B0
METHODS:
  RVA=0x0A93C300  token=0x60005AC  System.Collections.IEnumerator Invoke()
  RVA=0x041E1670  token=0x60005AD  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.WaitUntil
TYPE:  class
TOKEN: 0x2000160
SIZE:  0x70
EXTENDS: FlowCanvas.Nodes.LatentActionNode
FIELDS:
  private           FlowCanvas.ValueInput<System.Boolean>condition  // 0x68
PROPERTIES:
  allowRoutineQueueing  get=0x012081B0
METHODS:
  RVA=0x0A93C3CC  token=0x60005B5  System.Collections.IEnumerator Invoke()
  RVA=0x0A93C428  token=0x60005B6  System.Void OnRegisterExtraPorts(FlowCanvas.FlowNode node)
  RVA=0x041E1670  token=0x60005B7  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.WaitWhile
TYPE:  class
TOKEN: 0x2000162
SIZE:  0x70
EXTENDS: FlowCanvas.Nodes.LatentActionNode
FIELDS:
  private           FlowCanvas.ValueInput<System.Boolean>condition  // 0x68
PROPERTIES:
  allowRoutineQueueing  get=0x012081B0
METHODS:
  RVA=0x0A93C4AC  token=0x60005BF  System.Collections.IEnumerator Invoke()
  RVA=0x0A93C508  token=0x60005C0  System.Void OnRegisterExtraPorts(FlowCanvas.FlowNode node)
  RVA=0x041E1670  token=0x60005C1  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.DeltaTimed
TYPE:  class
TOKEN: 0x2000164
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92CA5C  token=0x60005C8  System.Single Invoke(System.Single value, System.Single multiplier)
  RVA=0x0A92CA90  token=0x60005C9  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.DeltaTimedVector3
TYPE:  class
TOKEN: 0x2000165
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A92C9B0  token=0x60005CA  UnityEngine.Vector3 Invoke(UnityEngine.Vector3 value, System.Single multiplier)
  RVA=0x0A92CA34  token=0x60005CB  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Buffer`1
TYPE:  class
TOKEN: 0x2000166
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
  private           System.Int32                    lastFrame  // 0x0
  private           System.Collections.Generic.Queue<T>q  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60005CC  T Invoke(T value, System.Int32 size)
  RVA=-1  // not resolved  token=0x60005CD  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.DampFloat
TYPE:  class
TOKEN: 0x2000167
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`4
FIELDS:
METHODS:
  RVA=0x0A92C8A4  token=0x60005CE  System.Single Invoke(System.Single current, System.Single target, System.Single damp)
  RVA=0x0A92C8F0  token=0x60005CF  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.DampVector3
TYPE:  class
TOKEN: 0x2000168
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`4
FIELDS:
METHODS:
  RVA=0x0A92C918  token=0x60005D0  UnityEngine.Vector3 Invoke(UnityEngine.Vector3 current, UnityEngine.Vector3 target, System.Single damp)
  RVA=0x0A92C988  token=0x60005D1  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.LerpFloat
TYPE:  class
TOKEN: 0x2000169
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`5
FIELDS:
METHODS:
  RVA=0x0A92F28C  token=0x60005D2  System.Single Invoke(System.Single a, System.Single b, System.Single t, ParadoxNotion.Animation.EaseType interpolation)
  RVA=0x0A92F2A0  token=0x60005D3  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.LerpVector3
TYPE:  class
TOKEN: 0x200016A
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`5
FIELDS:
METHODS:
  RVA=0x0A92F348  token=0x60005D4  UnityEngine.Vector3 Invoke(UnityEngine.Vector3 a, UnityEngine.Vector3 b, System.Single t, ParadoxNotion.Animation.EaseType interpolation)
  RVA=0x0A92F3BC  token=0x60005D5  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.LerpQuaternion
TYPE:  class
TOKEN: 0x200016B
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`5
FIELDS:
METHODS:
  RVA=0x0A92F2C8  token=0x60005D6  UnityEngine.Quaternion Invoke(UnityEngine.Quaternion a, UnityEngine.Quaternion b, System.Single t, ParadoxNotion.Animation.EaseType interpolation)
  RVA=0x0A92F320  token=0x60005D7  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.LerpColor
TYPE:  class
TOKEN: 0x200016C
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`5
FIELDS:
METHODS:
  RVA=0x0A92F20C  token=0x60005D8  UnityEngine.Color Invoke(UnityEngine.Color a, UnityEngine.Color b, System.Single t, ParadoxNotion.Animation.EaseType interpolation)
  RVA=0x0A92F264  token=0x60005D9  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.TweenFloat
TYPE:  class
TOKEN: 0x200016D
SIZE:  0x70
EXTENDS: FlowCanvas.Nodes.LatentActionNode`4
FIELDS:
  private           System.Single                   <value>k__BackingField  // 0x68
PROPERTIES:
  value  get=0x04D8F060  set=0x04D8F090
METHODS:
  RVA=0x0A936ABC  token=0x60005DC  System.Collections.IEnumerator Invoke(System.Single from, System.Single to, System.Single duration, ParadoxNotion.Animation.EaseType interpolation)
  RVA=0x0A936B5C  token=0x60005DD  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.TweenVector3
TYPE:  class
TOKEN: 0x200016F
SIZE:  0x78
EXTENDS: FlowCanvas.Nodes.LatentActionNode`4
FIELDS:
  private           UnityEngine.Vector3             <value>k__BackingField  // 0x68
PROPERTIES:
  value  get=0x04D91E20  set=0x04D91E40
METHODS:
  RVA=0x0A936EE0  token=0x60005E6  System.Collections.IEnumerator Invoke(UnityEngine.Vector3 from, UnityEngine.Vector3 to, System.Single duration, ParadoxNotion.Animation.EaseType interpolation)
  RVA=0x0A936F90  token=0x60005E7  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.TweenQuaternion
TYPE:  class
TOKEN: 0x2000171
SIZE:  0x78
EXTENDS: FlowCanvas.Nodes.LatentActionNode`4
FIELDS:
  private           UnityEngine.Quaternion          <value>k__BackingField  // 0x68
PROPERTIES:
  value  get=0x04DA63C0  set=0x04DA63D0
METHODS:
  RVA=0x0A936C60  token=0x60005F0  System.Collections.IEnumerator Invoke(UnityEngine.Quaternion from, UnityEngine.Quaternion to, System.Single duration, ParadoxNotion.Animation.EaseType interpolation)
  RVA=0x0A936D00  token=0x60005F1  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.TweenColor
TYPE:  class
TOKEN: 0x2000173
SIZE:  0x78
EXTENDS: FlowCanvas.Nodes.LatentActionNode`4
FIELDS:
  private           UnityEngine.Color               <value>k__BackingField  // 0x68
PROPERTIES:
  value  get=0x04DA63C0  set=0x04DA63D0
METHODS:
  RVA=0x0A9369F4  token=0x60005FA  System.Collections.IEnumerator Invoke(UnityEngine.Color from, UnityEngine.Color to, System.Single duration, ParadoxNotion.Animation.EaseType interpolation)
  RVA=0x0A936A94  token=0x60005FB  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.TweenPosition
TYPE:  class
TOKEN: 0x2000175
SIZE:  0x68
EXTENDS: FlowCanvas.Nodes.LatentActionNode`5
FIELDS:
METHODS:
  RVA=0x0A936B84  token=0x6000602  System.Collections.IEnumerator Invoke(UnityEngine.Transform transform, UnityEngine.Vector3 position, System.Single duration, ParadoxNotion.Animation.EaseType interpolation, System.Boolean relative)
  RVA=0x0A936C38  token=0x6000603  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.TweenRotation
TYPE:  class
TOKEN: 0x2000177
SIZE:  0x68
EXTENDS: FlowCanvas.Nodes.LatentActionNode`5
FIELDS:
METHODS:
  RVA=0x0A936D28  token=0x600060A  System.Collections.IEnumerator Invoke(UnityEngine.Transform transform, UnityEngine.Vector3 rotation, System.Single duration, ParadoxNotion.Animation.EaseType interpolation, System.Boolean relative)
  RVA=0x0A936DDC  token=0x600060B  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.TweenScale
TYPE:  class
TOKEN: 0x2000179
SIZE:  0x68
EXTENDS: FlowCanvas.Nodes.LatentActionNode`5
FIELDS:
METHODS:
  RVA=0x0A936E04  token=0x6000612  System.Collections.IEnumerator Invoke(UnityEngine.Transform transform, UnityEngine.Vector3 scale, System.Single duration, ParadoxNotion.Animation.EaseType interpolation, System.Boolean relative)
  RVA=0x0A936EB8  token=0x6000613  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.AddComponent`1
TYPE:  class
TOKEN: 0x200017B
EXTENDS: FlowCanvas.Nodes.CallableFunctionNode`3
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600061A  T Invoke(UnityEngine.GameObject gameObject, System.Boolean tryGetExisting)
  RVA=-1  // not resolved  token=0x600061B  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.GetComponent`1
TYPE:  class
TOKEN: 0x200017C
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`2
FIELDS:
  private           T                               _component  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600061C  T Invoke(UnityEngine.GameObject gameObject)
  RVA=-1  // not resolved  token=0x600061D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Instantiate`1
TYPE:  class
TOKEN: 0x200017D
EXTENDS: FlowCanvas.Nodes.CallableFunctionNode`5
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600061E  T Invoke(T original, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Transform parent)
  RVA=-1  // not resolved  token=0x600061F  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.NewScriptableObject`1
TYPE:  class
TOKEN: 0x200017E
EXTENDS: FlowCanvas.Nodes.CallableFunctionNode`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000620  T Invoke()
  RVA=-1  // not resolved  token=0x6000621  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.GetChildTransforms
TYPE:  class
TOKEN: 0x200017F
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`2
FIELDS:
METHODS:
  RVA=0x0A92DF94  token=0x6000622  System.Collections.Generic.IEnumerable<UnityEngine.Transform> Invoke(UnityEngine.Transform parent)
  RVA=0x0A92DFD0  token=0x6000623  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Cast`1
TYPE:  class
TOKEN: 0x2000180
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000624  T Invoke(System.Object obj)
  RVA=-1  // not resolved  token=0x6000625  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Identity`1
TYPE:  class
TOKEN: 0x2000181
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`2
FIELDS:
PROPERTIES:
  name  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000627  T Invoke(T value)
  RVA=-1  // not resolved  token=0x6000628  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Cache`1
TYPE:  class
TOKEN: 0x2000182
EXTENDS: FlowCanvas.Nodes.CallableFunctionNode`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000629  T Invoke(T value)
  RVA=-1  // not resolved  token=0x600062A  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.RemapFloat
TYPE:  class
TOKEN: 0x2000183
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`6
FIELDS:
METHODS:
  RVA=0x0A934018  token=0x600062B  System.Single Invoke(System.Single current, System.Single iMin, System.Single iMax, System.Single oMin, System.Single oMax)
  RVA=0x0A934044  token=0x600062C  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.RemapVector3
TYPE:  class
TOKEN: 0x2000184
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`6
FIELDS:
METHODS:
  RVA=0x0A93406C  token=0x600062D  UnityEngine.Vector3 Invoke(System.Single current, System.Single iMin, System.Single iMax, UnityEngine.Vector3 oMin, UnityEngine.Vector3 oMax)
  RVA=0x0A9340E8  token=0x600062E  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.LogValue
TYPE:  class
TOKEN: 0x2000185
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.CallableActionNode`1
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600062F  System.Void Invoke(System.Object obj)
  RVA=0x0A92F40C  token=0x6000630  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.LogText
TYPE:  class
TOKEN: 0x2000186
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.CallableActionNode`1
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000631  System.Void Invoke(System.String text)
  RVA=0x0A92F3E4  token=0x6000632  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SendEvent
TYPE:  class
TOKEN: 0x2000187
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.CallableActionNode`2
FIELDS:
METHODS:
  RVA=0x0A934284  token=0x6000633  System.Void Invoke(NodeCanvas.Framework.GraphOwner target, System.String eventName)
  RVA=0x0A9342BC  token=0x6000634  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SendEvent`1
TYPE:  class
TOKEN: 0x2000188
EXTENDS: FlowCanvas.Nodes.CallableActionNode`3
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000635  System.Void Invoke(NodeCanvas.Framework.GraphOwner target, System.String eventName, T eventValue)
  RVA=-1  // not resolved  token=0x6000636  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SendGlobalEvent
TYPE:  class
TOKEN: 0x2000189
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.CallableActionNode`1
FIELDS:
METHODS:
  RVA=0x0A9342E4  token=0x6000637  System.Void Invoke(System.String eventName)
  RVA=0x0A93433C  token=0x6000638  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SendGlobalEvent`1
TYPE:  class
TOKEN: 0x200018A
EXTENDS: FlowCanvas.Nodes.CallableActionNode`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000639  System.Void Invoke(System.String eventName, T eventValue)
  RVA=-1  // not resolved  token=0x600063A  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.LatentActionNodeBase
TYPE:  abstract class
TOKEN: 0x200018B
SIZE:  0x68
EXTENDS: FlowCanvas.Nodes.SimplexNode
FIELDS:
  public            FlowCanvas.Nodes.LatentActionNodeBase.InvocationModeinvocationMode  // 0x30
  private           FlowCanvas.FlowOutput           onStart  // 0x38
  private           FlowCanvas.FlowOutput           onUpdate  // 0x40
  private           FlowCanvas.FlowOutput           onFinish  // 0x48
  private           System.Collections.Generic.Queue<FlowCanvas.Nodes.LatentActionNodeBase.RoutineData>routineQueue  // 0x50
  private           UnityEngine.Coroutine           currentCoroutine  // 0x58
  private           System.Boolean                  graphStoped  // 0x60
PROPERTIES:
  name  get=0x0A92F058
  allowRoutineQueueing  get=0x02FFF600
METHODS:
  RVA=0x04DA6500  token=0x600063C  System.Void OnGraphStarted()
  RVA=0x0A92EE1C  token=0x600063D  System.Void OnGraphStoped()
  RVA=0x0A92EB3C  token=0x600063E  System.Void Begin(System.Collections.IEnumerator enumerator, FlowCanvas.Flow f)
  RVA=0x0A92ECD0  token=0x600063F  System.Void BreakAll(System.Boolean callFinish)
  RVA=0x0A92ED94  token=0x6000640  System.Collections.IEnumerator InternalCoroutine(FlowCanvas.Nodes.LatentActionNodeBase.RoutineData data)
  RVA=0x0A92EE2C  token=0x6000641  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // abstract  token=0x6000642  System.Void OnRegisterDerivedPorts(FlowCanvas.FlowNode node)
  RVA=0x041E1670  token=0x6000643  System.Void OnBreak()
  RVA=0x041E1670  token=0x6000645  System.Void .ctor()
  RVA=0x0A92F040  token=0x6000646  System.Void <InternalCoroutine>b__15_0()
  RVA=0x0A92F040  token=0x6000647  System.Void <OnRegisterPorts>b__16_0(FlowCanvas.Flow f)
  RVA=0x0A92F04C  token=0x6000648  System.Void <OnRegisterPorts>b__16_1(FlowCanvas.Flow f)
END_CLASS

CLASS: FlowCanvas.Nodes.LatentActionNode
TYPE:  abstract class
TOKEN: 0x200018F
SIZE:  0x68
EXTENDS: FlowCanvas.Nodes.LatentActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000650  System.Collections.IEnumerator Invoke()
  RVA=0x0A92F0FC  token=0x6000651  System.Void OnRegisterDerivedPorts(FlowCanvas.FlowNode node)
  RVA=0x041E1670  token=0x6000652  System.Void .ctor()
  RVA=0x0A92F1B4  token=0x6000653  System.Void <OnRegisterDerivedPorts>b__1_0(FlowCanvas.Flow f)
END_CLASS

CLASS: FlowCanvas.Nodes.LatentActionNode`1
TYPE:  abstract class
TOKEN: 0x2000190
EXTENDS: FlowCanvas.Nodes.LatentActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000654  System.Collections.IEnumerator Invoke(T1 a)
  RVA=-1  // not resolved  token=0x6000655  System.Void OnRegisterDerivedPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x6000656  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.LatentActionNode`2
TYPE:  abstract class
TOKEN: 0x2000192
EXTENDS: FlowCanvas.Nodes.LatentActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000659  System.Collections.IEnumerator Invoke(T1 a, T2 b)
  RVA=-1  // not resolved  token=0x600065A  System.Void OnRegisterDerivedPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x600065B  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.LatentActionNode`3
TYPE:  abstract class
TOKEN: 0x2000194
EXTENDS: FlowCanvas.Nodes.LatentActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600065E  System.Collections.IEnumerator Invoke(T1 a, T2 b, T3 c)
  RVA=-1  // not resolved  token=0x600065F  System.Void OnRegisterDerivedPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x6000660  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.LatentActionNode`4
TYPE:  abstract class
TOKEN: 0x2000196
EXTENDS: FlowCanvas.Nodes.LatentActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000663  System.Collections.IEnumerator Invoke(T1 a, T2 b, T3 c, T4 d)
  RVA=-1  // not resolved  token=0x6000664  System.Void OnRegisterDerivedPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x6000665  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.LatentActionNode`5
TYPE:  abstract class
TOKEN: 0x2000198
EXTENDS: FlowCanvas.Nodes.LatentActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000668  System.Collections.IEnumerator Invoke(T1 a, T2 b, T3 c, T4 d, T5 e)
  RVA=-1  // not resolved  token=0x6000669  System.Void OnRegisterDerivedPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x600066A  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.LatentActionNode`6
TYPE:  abstract class
TOKEN: 0x200019A
EXTENDS: FlowCanvas.Nodes.LatentActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600066D  System.Collections.IEnumerator Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f)
  RVA=-1  // not resolved  token=0x600066E  System.Void OnRegisterDerivedPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x600066F  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.LatentActionNode`7
TYPE:  abstract class
TOKEN: 0x200019C
EXTENDS: FlowCanvas.Nodes.LatentActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000672  System.Collections.IEnumerator Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g)
  RVA=-1  // not resolved  token=0x6000673  System.Void OnRegisterDerivedPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x6000674  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.LatentActionNode`8
TYPE:  abstract class
TOKEN: 0x200019E
EXTENDS: FlowCanvas.Nodes.LatentActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000677  System.Collections.IEnumerator Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h)
  RVA=-1  // not resolved  token=0x6000678  System.Void OnRegisterDerivedPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x6000679  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.LatentActionNode`9
TYPE:  abstract class
TOKEN: 0x20001A0
EXTENDS: FlowCanvas.Nodes.LatentActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600067C  System.Collections.IEnumerator Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i)
  RVA=-1  // not resolved  token=0x600067D  System.Void OnRegisterDerivedPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x600067E  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.LatentActionNode`10
TYPE:  abstract class
TOKEN: 0x20001A2
EXTENDS: FlowCanvas.Nodes.LatentActionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000681  System.Collections.IEnumerator Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j)
  RVA=-1  // not resolved  token=0x6000682  System.Void OnRegisterDerivedPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x6000683  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.PureFunctionNodeBase
TYPE:  abstract class
TOKEN: 0x20001A4
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.SimplexNode
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000686  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.PureFunctionNode`1
TYPE:  abstract class
TOKEN: 0x20001A5
EXTENDS: FlowCanvas.Nodes.PureFunctionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000687  TResult Invoke()
  RVA=-1  // not resolved  token=0x6000688  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x6000689  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600068A  TResult <OnRegisterPorts>b__1_0()
END_CLASS

CLASS: FlowCanvas.Nodes.PureFunctionNode`2
TYPE:  abstract class
TOKEN: 0x20001A6
EXTENDS: FlowCanvas.Nodes.PureFunctionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600068B  TResult Invoke(T1 a)
  RVA=-1  // not resolved  token=0x600068C  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x600068D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.PureFunctionNode`3
TYPE:  abstract class
TOKEN: 0x20001A8
EXTENDS: FlowCanvas.Nodes.PureFunctionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000690  TResult Invoke(T1 a, T2 b)
  RVA=-1  // not resolved  token=0x6000691  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x6000692  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.PureFunctionNode`4
TYPE:  abstract class
TOKEN: 0x20001AA
EXTENDS: FlowCanvas.Nodes.PureFunctionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000695  TResult Invoke(T1 a, T2 b, T3 c)
  RVA=-1  // not resolved  token=0x6000696  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x6000697  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.PureFunctionNode`5
TYPE:  abstract class
TOKEN: 0x20001AC
EXTENDS: FlowCanvas.Nodes.PureFunctionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600069A  TResult Invoke(T1 a, T2 b, T3 c, T4 d)
  RVA=-1  // not resolved  token=0x600069B  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x600069C  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.PureFunctionNode`6
TYPE:  abstract class
TOKEN: 0x20001AE
EXTENDS: FlowCanvas.Nodes.PureFunctionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600069F  TResult Invoke(T1 a, T2 b, T3 c, T4 d, T5 e)
  RVA=-1  // not resolved  token=0x60006A0  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x60006A1  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.PureFunctionNode`7
TYPE:  abstract class
TOKEN: 0x20001B0
EXTENDS: FlowCanvas.Nodes.PureFunctionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006A4  TResult Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f)
  RVA=-1  // not resolved  token=0x60006A5  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x60006A6  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.PureFunctionNode`8
TYPE:  abstract class
TOKEN: 0x20001B2
EXTENDS: FlowCanvas.Nodes.PureFunctionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006A9  TResult Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g)
  RVA=-1  // not resolved  token=0x60006AA  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x60006AB  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.PureFunctionNode`9
TYPE:  abstract class
TOKEN: 0x20001B4
EXTENDS: FlowCanvas.Nodes.PureFunctionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006AE  TResult Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h)
  RVA=-1  // not resolved  token=0x60006AF  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x60006B0  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.PureFunctionNode`10
TYPE:  abstract class
TOKEN: 0x20001B6
EXTENDS: FlowCanvas.Nodes.PureFunctionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006B3  TResult Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i)
  RVA=-1  // not resolved  token=0x60006B4  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x60006B5  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.PureFunctionNode`11
TYPE:  abstract class
TOKEN: 0x20001B8
EXTENDS: FlowCanvas.Nodes.PureFunctionNodeBase
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60006B8  TResult Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, T9 i, T10 j)
  RVA=-1  // not resolved  token=0x60006B9  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x60006BA  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SimplexNode
TYPE:  abstract class
TOKEN: 0x20001BA
SIZE:  0x30
FIELDS:
  private           System.String                   _name  // 0x10
  private           System.String                   _description  // 0x18
  private           FlowCanvas.FlowNode             <parentNode>k__BackingField  // 0x20
  private           System.Reflection.ParameterInfo[]_parameters  // 0x28
PROPERTIES:
  parentNode  get=0x04D862C0  set=0x05390F40
  name  get=0x0A934B30
  description  get=0x0A934A88
  parameters  get=0x0A934C14
METHODS:
  RVA=0x0A93486C  token=0x60006C2  System.Void RegisterPorts(FlowCanvas.FlowNode node)
  RVA=0x0A93496C  token=0x60006C3  System.Void SetDefaultParameters(FlowCanvas.FlowNode node)
  RVA=-1  // abstract  token=0x60006C4  System.Void OnRegisterPorts(FlowCanvas.FlowNode node)
  RVA=0x041E1670  token=0x60006C5  System.Void OnRegisterExtraPorts(FlowCanvas.FlowNode node)
  RVA=0x041E1670  token=0x60006C6  System.Void OnGraphStarted()
  RVA=0x041E1670  token=0x60006C7  System.Void OnGraphPaused()
  RVA=0x041E1670  token=0x60006C8  System.Void OnGraphUnpaused()
  RVA=0x041E1670  token=0x60006C9  System.Void OnGraphStoped()
  RVA=0x041E1670  token=0x60006CA  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SimplexNodeWrapper
TYPE:  abstract class
TOKEN: 0x20001BB
SIZE:  0xB0
EXTENDS: FlowCanvas.FlowNode
FIELDS:
METHODS:
  RVA=0x0489BC70  token=0x60006CB  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SimplexNodeWrapper`1
TYPE:  class
TOKEN: 0x20001BC
EXTENDS: FlowCanvas.Nodes.SimplexNodeWrapper
FIELDS:
  private           T                               _simplexNode  // 0x0
PROPERTIES:
  simplexNode  get=-1  // not resolved
  name  get=-1  // not resolved
  description  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60006CF  System.Type GetNodeWildDefinitionType()
  RVA=-1  // not resolved  token=0x60006D0  System.Void OnCreate(NodeCanvas.Framework.Graph assignedGraph)
  RVA=-1  // not resolved  token=0x60006D1  System.Void OnGraphStarted()
  RVA=-1  // not resolved  token=0x60006D2  System.Void OnGraphPaused()
  RVA=-1  // not resolved  token=0x60006D3  System.Void OnGraphUnpaused()
  RVA=-1  // not resolved  token=0x60006D4  System.Void OnGraphStoped()
  RVA=-1  // not resolved  token=0x60006D5  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x60006D6  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.EventUniversalDelegate
TYPE:  sealed class
TOKEN: 0x20001BD
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x053DEDC8  token=0x60006D7  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x053DEDB8  token=0x60006D8  System.Void Invoke()
  RVA=0x053DED94  token=0x60006D9  System.IAsyncResult BeginInvoke(System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x60006DA  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: FlowCanvas.Nodes.EventUniversalDelegate`1
TYPE:  sealed class
TOKEN: 0x20001BE
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60006DB  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x60006DC  System.Void Invoke(T1 arg1)
  RVA=-1  // runtime  token=0x60006DD  System.IAsyncResult BeginInvoke(T1 arg1, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x60006DE  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: FlowCanvas.Nodes.EventUniversalDelegate`2
TYPE:  sealed class
TOKEN: 0x20001BF
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60006DF  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x60006E0  System.Void Invoke(T1 arg1, T2 arg2)
  RVA=-1  // runtime  token=0x60006E1  System.IAsyncResult BeginInvoke(T1 arg1, T2 arg2, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x60006E2  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: FlowCanvas.Nodes.EventUniversalDelegate`3
TYPE:  sealed class
TOKEN: 0x20001C0
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60006E3  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x60006E4  System.Void Invoke(T1 arg1, T2 arg2, T3 arg3)
  RVA=-1  // runtime  token=0x60006E5  System.IAsyncResult BeginInvoke(T1 arg1, T2 arg2, T3 arg3, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x60006E6  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: FlowCanvas.Nodes.EventUniversalDelegate`4
TYPE:  sealed class
TOKEN: 0x20001C1
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60006E7  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x60006E8  System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
  RVA=-1  // runtime  token=0x60006E9  System.IAsyncResult BeginInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x60006EA  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: FlowCanvas.Nodes.EventUniversalDelegate`5
TYPE:  sealed class
TOKEN: 0x20001C2
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60006EB  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x60006EC  System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
  RVA=-1  // runtime  token=0x60006ED  System.IAsyncResult BeginInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x60006EE  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: FlowCanvas.Nodes.EventUniversalDelegate`6
TYPE:  sealed class
TOKEN: 0x20001C3
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60006EF  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x60006F0  System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
  RVA=-1  // runtime  token=0x60006F1  System.IAsyncResult BeginInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x60006F2  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: FlowCanvas.Nodes.EventUniversalDelegate`7
TYPE:  sealed class
TOKEN: 0x20001C4
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60006F3  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x60006F4  System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
  RVA=-1  // runtime  token=0x60006F5  System.IAsyncResult BeginInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x60006F6  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: FlowCanvas.Nodes.EventUniversalDelegate`8
TYPE:  sealed class
TOKEN: 0x20001C5
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60006F7  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x60006F8  System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
  RVA=-1  // runtime  token=0x60006F9  System.IAsyncResult BeginInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x60006FA  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: FlowCanvas.Nodes.EventUniversalDelegate`9
TYPE:  sealed class
TOKEN: 0x20001C6
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60006FB  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x60006FC  System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
  RVA=-1  // runtime  token=0x60006FD  System.IAsyncResult BeginInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x60006FE  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: FlowCanvas.Nodes.EventUniversalDelegate`10
TYPE:  sealed class
TOKEN: 0x20001C7
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60006FF  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000700  System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
  RVA=-1  // runtime  token=0x6000701  System.IAsyncResult BeginInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6000702  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: FlowCanvas.Nodes.GeneratedKeyAttribute
TYPE:  class
TOKEN: 0x20001C8
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private           System.String                   memberString  // 0x10
PROPERTIES:
  MemberName  get=0x02B2ECC0
METHODS:
  RVA=0x053908C0  token=0x6000703  System.Void .ctor(System.String memberName)
END_CLASS

CLASS: FlowCanvas.Nodes.ParamDef
TYPE:  sealed struct
TOKEN: 0x20001C9
SIZE:  0x48
FIELDS:
  public            System.Type                     paramType  // 0x10
  public            System.Type                     arrayType  // 0x18
  public            FlowCanvas.Nodes.ParamMode      paramMode  // 0x20
  public            System.String                   portName  // 0x28
  public            System.String                   portId  // 0x30
  public            System.Boolean                  isParamsArray  // 0x38
  public            System.Reflection.MemberInfo    presentedInfo  // 0x40
METHODS:
END_CLASS

CLASS: FlowCanvas.Nodes.ParametresDef
TYPE:  sealed struct
TOKEN: 0x20001CA
SIZE:  0x88
FIELDS:
  public            System.Collections.Generic.List<FlowCanvas.Nodes.ParamDef>paramDefinitions  // 0x10
  public            FlowCanvas.Nodes.ParamDef       instanceDef  // 0x18
  public            FlowCanvas.Nodes.ParamDef       resultDef  // 0x50
METHODS:
END_CLASS

CLASS: FlowCanvas.Nodes.ParamMode
TYPE:  sealed struct
TOKEN: 0x20001CB
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  FlowCanvas.Nodes.ParamMode      Undefined  // const
  public    static  FlowCanvas.Nodes.ParamMode      In  // const
  public    static  FlowCanvas.Nodes.ParamMode      Out  // const
  public    static  FlowCanvas.Nodes.ParamMode      Ref  // const
  public    static  FlowCanvas.Nodes.ParamMode      Instance  // const
  public    static  FlowCanvas.Nodes.ParamMode      Result  // const
METHODS:
END_CLASS

CLASS: FlowCanvas.Nodes.ReflectedNodesHelper
TYPE:  static class
TOKEN: 0x20001CC
SIZE:  0x10
FIELDS:
  private   static  System.String                   RETURN_VALUE_NAME  // const
METHODS:
  RVA=0x0A9329F4  token=0x6000705  FlowCanvas.Nodes.ParamDef GetGetterDefFromInfo(System.Reflection.MemberInfo info)
  RVA=0x0A932740  token=0x6000706  FlowCanvas.Nodes.ParamDef GetDefFromInfo(System.Reflection.ParameterInfo info, System.Boolean last)
  RVA=0x0A932C34  token=0x6000707  System.Boolean InitParams(System.Type targetType, System.Boolean isStatic, System.Reflection.MemberInfo[] infos, FlowCanvas.Nodes.ParametresDef& parametres)
  RVA=0x0A932F00  token=0x6000708  System.Boolean InitParams(System.Reflection.ParameterInfo[] prms, System.Type returnType, FlowCanvas.Nodes.ParametresDef& parametres)
  RVA=0x0A9331B8  token=0x6000709  System.Boolean InitParams(System.Reflection.ConstructorInfo constructor, FlowCanvas.Nodes.ParametresDef& parametres)
  RVA=0x0A9335AC  token=0x600070A  System.Boolean InitParams(System.Reflection.MethodInfo method, FlowCanvas.Nodes.ParametresDef& parametres)
  RVA=0x0A933388  token=0x600070B  System.Boolean InitParams(System.Reflection.FieldInfo field, FlowCanvas.Nodes.ParametresDef& parametres)
  RVA=0x0A932918  token=0x600070C  System.String GetGeneratedKey(System.Reflection.MemberInfo memberInfo)
END_CLASS

CLASS: FlowCanvas.Nodes.UniversalDelegate
TYPE:  sealed class
TOKEN: 0x20001CD
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x068FE0AC  token=0x600070D  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x600070E  System.Void Invoke(FlowCanvas.Nodes.UniversalDelegateParam[] delegateParams)
  RVA=0x047FDD70  token=0x600070F  System.IAsyncResult BeginInvoke(FlowCanvas.Nodes.UniversalDelegateParam[] delegateParams, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000710  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: FlowCanvas.Nodes.UniversalDelegateParam
TYPE:  abstract class
TOKEN: 0x20001CE
SIZE:  0x60
FIELDS:
  public            FlowCanvas.Nodes.ParamDef       paramDef  // 0x10
  public            System.Boolean                  paramsArrayNeeded  // 0x48
  public            System.Int32                    paramsArrayCount  // 0x4c
  public            FlowCanvas.Nodes.UniversalDelegatereferencedDelegate  // 0x50
  public            FlowCanvas.Nodes.UniversalDelegateParam[]referencedParams  // 0x58
PROPERTIES:
  ValueField  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000711  System.Type GetCurrentType()
  RVA=-1  // abstract  token=0x6000712  System.Void RegisterAsInput(FlowCanvas.FlowNode node)
  RVA=-1  // abstract  token=0x6000713  System.Void RegisterAsOutput(FlowCanvas.FlowNode node)
  RVA=-1  // abstract  token=0x6000714  System.Void RegisterAsOutput(FlowCanvas.FlowNode node, System.Action beforeReturn)
  RVA=-1  // abstract  token=0x6000715  System.Void RegisterAsOutput(FlowCanvas.FlowNode node, System.Action<FlowCanvas.Nodes.UniversalDelegateParam> beforeReturn)
  RVA=-1  // abstract  token=0x6000716  System.Void SetFromInput()
  RVA=-1  // abstract  token=0x6000717  System.Void SetFromValue(System.Object value)
  RVA=0x041E1670  token=0x6000719  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.UniversalDelegateParam`1
TYPE:  class
TOKEN: 0x20001CF
EXTENDS: FlowCanvas.Nodes.UniversalDelegateParam
FIELDS:
  public            T                               value  // 0x0
  private           FlowCanvas.ValueInput<T>        valueInput  // 0x0
  private   static  System.Reflection.FieldInfo     _fieldInfo  // static @ 0x0
PROPERTIES:
  ValueField  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600071A  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600071B  System.Type GetCurrentType()
  RVA=-1  // not resolved  token=0x600071C  System.Void RegisterAsInput(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x600071D  System.Void RegisterAsOutputInternal(FlowCanvas.FlowNode node, System.Delegate beforeReturn)
  RVA=-1  // not resolved  token=0x600071E  System.Void RegisterAsOutput(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x600071F  System.Void RegisterAsOutput(FlowCanvas.FlowNode node, System.Action beforeReturn)
  RVA=-1  // not resolved  token=0x6000720  System.Void RegisterAsOutput(FlowCanvas.FlowNode node, System.Action<FlowCanvas.Nodes.UniversalDelegateParam> beforeReturn)
  RVA=-1  // not resolved  token=0x6000721  System.Void SetFromInput()
  RVA=-1  // not resolved  token=0x6000722  System.Void SetFromValue(System.Object newValue)
END_CLASS

CLASS: FlowCanvas.Nodes.UniversalDelegateParam`2
TYPE:  class
TOKEN: 0x20001D1
EXTENDS: FlowCanvas.Nodes.UniversalDelegateParam`1
FIELDS:
  private           FlowCanvas.ValueInput<TValue>[] valueInputs  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000726  System.Void RegisterAsInput(FlowCanvas.FlowNode node)
  RVA=-1  // not resolved  token=0x6000727  System.Void SetFromInput()
  RVA=-1  // not resolved  token=0x6000728  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.BaseReflectedConstructorNode
TYPE:  abstract class
TOKEN: 0x20001D2
SIZE:  0x98
FIELDS:
  private   static  System.Func<System.Reflection.ConstructorInfo,FlowCanvas.Nodes.BaseReflectedConstructorNode>OnGetAotReflectedConstructorNode  // static @ 0x0
  protected         System.Reflection.ConstructorInfoconstructorInfo  // 0x10
  protected         System.Collections.Generic.List<FlowCanvas.Nodes.ParamDef>paramDefinitions  // 0x18
  protected         FlowCanvas.Nodes.ParamDef       instanceDef  // 0x20
  protected         FlowCanvas.Nodes.ParamDef       resultDef  // 0x58
  protected         FlowCanvas.Nodes.ReflectedMethodRegistrationOptionsoptions  // 0x90
EVENTS:
  OnGetAotReflectedConstructorNode  add=add_OnGetAotReflectedConstructorNode  remove=remove_OnGetAotReflectedConstructorNode
METHODS:
  RVA=0x0A92954C  token=0x600072B  FlowCanvas.Nodes.BaseReflectedConstructorNode GetConstructorNode(System.Reflection.ConstructorInfo targetConstructor, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=0x0A929700  token=0x600072C  System.Boolean Init(System.Reflection.ConstructorInfo constructor, FlowCanvas.Nodes.ParametresDef parametres)
  RVA=-1  // abstract  token=0x600072D  System.Boolean InitInternal(System.Reflection.ConstructorInfo constructor)
  RVA=-1  // abstract  token=0x600072E  System.Void RegisterPorts(FlowCanvas.FlowNode node, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=0x041E1670  token=0x600072F  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.PureReflectionConstructorNode
TYPE:  class
TOKEN: 0x20001D3
SIZE:  0xC8
EXTENDS: FlowCanvas.Nodes.BaseReflectedConstructorNode
FIELDS:
  private           System.Object                   resultObject  // 0x98
  private           System.Object[]                 callParams  // 0xa0
  private           FlowCanvas.ValueInput[]         inputs  // 0xa8
  private           FlowCanvas.ValueInput[]         arrayInputs  // 0xb0
  private           System.Int32                    arrayParamsInput  // 0xb8
  private           System.Type                     arrayParamsType  // 0xc0
METHODS:
  RVA=0x0A93142C  token=0x6000730  System.Boolean InitInternal(System.Reflection.ConstructorInfo constructor)
  RVA=0x0A9312A0  token=0x6000731  System.Void Call()
  RVA=0x0A931758  token=0x6000732  System.Void RegisterOutput(FlowCanvas.FlowNode node, System.Boolean callable, FlowCanvas.Nodes.ParamDef def, System.Int32 idx)
  RVA=0x0A9315A0  token=0x6000733  System.Void RegisterInput(FlowCanvas.FlowNode node, FlowCanvas.Nodes.ParamDef def, System.Int32 idx)
  RVA=0x0A931858  token=0x6000734  System.Void RegisterPorts(FlowCanvas.FlowNode node, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=0x04DA2320  token=0x6000735  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ReflectedDelegateEvent
TYPE:  class
TOKEN: 0x20001D7
SIZE:  0x20
FIELDS:
  private           FlowCanvas.Nodes.ReflectedDelegateEvent.DelegateEventCallbackonCallback  // 0x10
  private           System.Delegate                 theDelegate  // 0x18
EVENTS:
  onCallback  add=add_onCallback  remove=remove_onCallback
METHODS:
  RVA=0x041E1670  token=0x600073E  System.Void .ctor()
  RVA=0x0A93259C  token=0x600073F  System.Void .ctor(System.Type delegateType)
  RVA=0x0A9320D0  token=0x6000740  System.Void Add(FlowCanvas.Nodes.ReflectedDelegateEvent.DelegateEventCallback callback)
  RVA=0x0A932594  token=0x6000741  System.Void Remove(FlowCanvas.Nodes.ReflectedDelegateEvent.DelegateEventCallback callback)
  RVA=0x0385B100  token=0x6000742  System.Delegate AsDelegate()
  RVA=0x0A932130  token=0x6000743  System.Reflection.MethodInfo GetMethodForDelegateType(System.Type delegateType)
  RVA=0x0A9320D8  token=0x6000744  System.Void Callback0()
  RVA=-1  // generic def  token=0x6000745  System.Void Callback1(T0 arg0)
  RVA=-1  // generic def  token=0x6000746  System.Void Callback2(T0 arg0, T1 arg1)
  RVA=-1  // generic def  token=0x6000747  System.Void Callback3(T0 arg0, T1 arg1, T2 arg2)
  RVA=-1  // generic def  token=0x6000748  System.Void Callback4(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
  RVA=-1  // generic def  token=0x6000749  System.Void Callback5(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
  RVA=-1  // generic def  token=0x600074A  System.Void Callback6(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
  RVA=-1  // generic def  token=0x600074B  System.Void Callback7(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
  RVA=-1  // generic def  token=0x600074C  System.Void Callback8(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
  RVA=-1  // generic def  token=0x600074D  System.Void Callback9(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
  RVA=-1  // generic def  token=0x600074E  System.Void Callback10(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
  RVA=0x053E4DA0  token=0x600074F  System.Delegate op_Explicit(FlowCanvas.Nodes.ReflectedDelegateEvent that)
END_CLASS

CLASS: FlowCanvas.Nodes.ReflectedUnityEvent
TYPE:  class
TOKEN: 0x20001DA
SIZE:  0x40
FIELDS:
  private           FlowCanvas.Nodes.ReflectedUnityEvent.UnityEventCallback_callback  // 0x10
  private           System.Type                     _eventType  // 0x18
  private           System.Reflection.MethodInfo    _addListenerMethod  // 0x20
  private           System.Reflection.MethodInfo    _removeListenerMethod  // 0x28
  private           System.Reflection.ParameterInfo[]_parameters  // 0x30
  private           System.Reflection.MethodInfo    _callMethod  // 0x38
PROPERTIES:
  parameters  get=0x011F36E0
  eventType  get=0x0385B100
EVENTS:
  _callback  add=add__callback  remove=remove__callback
METHODS:
  RVA=0x041E1670  token=0x600075B  System.Void .ctor()
  RVA=0x0A933EE8  token=0x600075C  System.Void .ctor(System.Type eventType)
  RVA=0x0A933818  token=0x600075D  System.Void InitForEventType(System.Type eventType)
  RVA=0x0A933D50  token=0x600075E  System.Void StartListening(UnityEngine.Events.UnityEventBase targetEvent, FlowCanvas.Nodes.ReflectedUnityEvent.UnityEventCallback callback)
  RVA=0x0A933E1C  token=0x600075F  System.Void StopListening(UnityEngine.Events.UnityEventBase targetEvent, FlowCanvas.Nodes.ReflectedUnityEvent.UnityEventCallback callback)
  RVA=0x0A9337C0  token=0x6000760  System.Void CallbackMethod0()
  RVA=-1  // generic def  token=0x6000761  System.Void CallbackMethod1(T0 arg0)
  RVA=-1  // generic def  token=0x6000762  System.Void CallbackMethod2(T0 arg0, T1 arg1)
  RVA=-1  // generic def  token=0x6000763  System.Void CallbackMethod3(T0 arg0, T1 arg1, T2 arg2)
  RVA=-1  // generic def  token=0x6000764  System.Void CallbackMethod4(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
END_CLASS

CLASS: FlowCanvas.Nodes.SharpEvent
TYPE:  abstract class
TOKEN: 0x20001DD
SIZE:  0x20
FIELDS:
  private           System.Object                   <instance>k__BackingField  // 0x10
  private           System.Reflection.EventInfo     <eventInfo>k__BackingField  // 0x18
PROPERTIES:
  instance  get=0x02B2ECC0  set=0x053908C0
  eventInfo  get=0x0385B100  set=0x05392C40
METHODS:
  RVA=0x0A934674  token=0x6000770  FlowCanvas.Nodes.SharpEvent Create(System.Reflection.EventInfo eventInfo)
  RVA=0x053908C0  token=0x6000771  System.Void SetInstance(System.Object instance)
  RVA=0x0A9347BC  token=0x6000772  System.Void StartListening(FlowCanvas.Nodes.ReflectedDelegateEvent reflectedEvent, FlowCanvas.Nodes.ReflectedDelegateEvent.DelegateEventCallback callback)
  RVA=0x0A934814  token=0x6000773  System.Void StopListening(FlowCanvas.Nodes.ReflectedDelegateEvent reflectedEvent, FlowCanvas.Nodes.ReflectedDelegateEvent.DelegateEventCallback callback)
  RVA=0x041E1670  token=0x6000774  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SharpEvent`1
TYPE:  class
TOKEN: 0x20001DE
EXTENDS: FlowCanvas.Nodes.SharpEvent
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000775  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.BaseReflectedExtractorNode
TYPE:  abstract class
TOKEN: 0x20001DF
SIZE:  0x90
FIELDS:
  private   static  System.Func<System.Type,System.Boolean,System.Reflection.MemberInfo[],FlowCanvas.Nodes.BaseReflectedExtractorNode>OnGetAotExtractorNode  // static @ 0x0
  private           FlowCanvas.Nodes.ParametresDef  <Params>k__BackingField  // 0x10
  private           System.Type                     <TargetType>k__BackingField  // 0x88
PROPERTIES:
  Params  get=0x04D97610  set=0x0A929D98
  TargetType  get=0x04D861F0  set=0x05395D30
EVENTS:
  OnGetAotExtractorNode  add=add_OnGetAotExtractorNode  remove=remove_OnGetAotExtractorNode
METHODS:
  RVA=0x0A929A30  token=0x6000778  FlowCanvas.Nodes.BaseReflectedExtractorNode GetExtractorNode(System.Type targetType, System.Boolean isStatic, System.Reflection.MemberInfo[] infos)
  RVA=0x0A929BE4  token=0x600077D  System.Boolean Init(FlowCanvas.Nodes.ParametresDef paramsDef, System.Type targetType)
  RVA=-1  // abstract  token=0x600077E  System.Boolean InitInternal()
  RVA=-1  // abstract  token=0x600077F  System.Void RegisterPorts(FlowCanvas.FlowNode node)
  RVA=0x041E1670  token=0x6000780  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.PureReflectedExtractorNode
TYPE:  class
TOKEN: 0x20001E0
SIZE:  0x98
EXTENDS: FlowCanvas.Nodes.BaseReflectedExtractorNode
FIELDS:
  private   static readonly System.Object[]                 EmptyParams  // static @ 0x0
  private           FlowCanvas.ValueInput           instanceInput  // 0x90
METHODS:
  RVA=0x0A93018C  token=0x6000781  System.Boolean InitInternal()
  RVA=0x0A9300BC  token=0x6000782  FlowCanvas.ValueHandlerObject GetPortHandler(System.Reflection.FieldInfo info)
  RVA=0x0A92FFEC  token=0x6000783  FlowCanvas.ValueHandlerObject GetPortHandler(System.Reflection.MethodInfo info)
  RVA=0x0A9301B0  token=0x6000784  System.Void RegisterPorts(FlowCanvas.FlowNode node)
  RVA=0x041E1670  token=0x6000785  System.Void .ctor()
  RVA=0x0A930388  token=0x6000786  System.Void .cctor()
END_CLASS

CLASS: FlowCanvas.Nodes.BaseReflectedFieldNode
TYPE:  abstract class
TOKEN: 0x20001E3
SIZE:  0x88
FIELDS:
  private   static  System.Func<System.Reflection.FieldInfo,FlowCanvas.Nodes.BaseReflectedFieldNode>OnGetAotReflectedFieldNode  // static @ 0x0
  protected         System.Reflection.FieldInfo     fieldInfo  // 0x10
  protected         FlowCanvas.Nodes.ParamDef       instanceDef  // 0x18
  protected         FlowCanvas.Nodes.ParamDef       resultDef  // 0x50
EVENTS:
  OnGetAotReflectedFieldNode  add=add_OnGetAotReflectedFieldNode  remove=remove_OnGetAotReflectedFieldNode
METHODS:
  RVA=0x0A929DE8  token=0x600078D  FlowCanvas.Nodes.BaseReflectedFieldNode GetFieldNode(System.Reflection.FieldInfo targetField)
  RVA=0x0A929F70  token=0x600078E  System.Boolean Init(System.Reflection.FieldInfo field, FlowCanvas.Nodes.ParametresDef parametres)
  RVA=-1  // abstract  token=0x600078F  System.Boolean InitInternal(System.Reflection.FieldInfo field)
  RVA=-1  // abstract  token=0x6000790  System.Void RegisterPorts(FlowCanvas.FlowNode node, FlowCanvas.Nodes.ReflectedFieldNodeWrapper.AccessMode accessMode)
  RVA=0x041E1670  token=0x6000791  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.PureReflectedFieldNode
TYPE:  class
TOKEN: 0x20001E4
SIZE:  0xA8
EXTENDS: FlowCanvas.Nodes.BaseReflectedFieldNode
FIELDS:
  private           FlowCanvas.ValueInput           instanceInput  // 0x88
  private           FlowCanvas.ValueInput           valueInput  // 0x90
  private           System.Object                   instanceObject  // 0x98
  private           System.Object                   valueObject  // 0xa0
METHODS:
  RVA=0x0A93045C  token=0x6000792  System.Boolean InitInternal(System.Reflection.FieldInfo method)
  RVA=0x0A9307EC  token=0x6000793  System.Void SetValue()
  RVA=0x0A9303EC  token=0x6000794  System.Void GetValue()
  RVA=0x0A9304A8  token=0x6000795  System.Void RegisterPorts(FlowCanvas.FlowNode node, FlowCanvas.Nodes.ReflectedFieldNodeWrapper.AccessMode accessMode)
  RVA=0x041E1670  token=0x6000796  System.Void .ctor()
  RVA=0x04D85EE0  token=0x6000797  System.Object <RegisterPorts>b__7_0()
  RVA=0x0A930880  token=0x6000798  System.Object <RegisterPorts>b__7_1()
END_CLASS

CLASS: FlowCanvas.Nodes.BaseReflectedMethodNode
TYPE:  abstract class
TOKEN: 0x20001E6
SIZE:  0x98
FIELDS:
  private   static  System.Func<System.Reflection.MethodInfo,FlowCanvas.Nodes.BaseReflectedMethodNode>OnGetAotReflectedMethodNode  // static @ 0x0
  protected         System.Reflection.MethodInfo    methodInfo  // 0x10
  protected         System.Collections.Generic.List<FlowCanvas.Nodes.ParamDef>paramDefinitions  // 0x18
  protected         FlowCanvas.Nodes.ParamDef       instanceDef  // 0x20
  protected         FlowCanvas.Nodes.ParamDef       resultDef  // 0x58
  protected         FlowCanvas.Nodes.ReflectedMethodRegistrationOptionsoptions  // 0x90
EVENTS:
  OnGetAotReflectedMethodNode  add=add_OnGetAotReflectedMethodNode  remove=remove_OnGetAotReflectedMethodNode
METHODS:
  RVA=0x0A92A20C  token=0x600079D  FlowCanvas.Nodes.BaseReflectedMethodNode GetMethodNode(System.Reflection.MethodInfo targetMethod, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=0x0A92A3C0  token=0x600079E  System.Boolean Init(System.Reflection.MethodInfo method, FlowCanvas.Nodes.ParametresDef parametres)
  RVA=-1  // abstract  token=0x600079F  System.Boolean InitInternal(System.Reflection.MethodInfo method)
  RVA=-1  // abstract  token=0x60007A0  System.Void RegisterPorts(FlowCanvas.FlowNode node, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=0x041E1670  token=0x60007A1  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.PureReflectedMethodNode
TYPE:  class
TOKEN: 0x20001E7
SIZE:  0xD8
EXTENDS: FlowCanvas.Nodes.BaseReflectedMethodNode
FIELDS:
  private           FlowCanvas.ValueInput           instanceInput  // 0x98
  private           System.Object                   instanceObject  // 0xa0
  private           System.Object                   resultObject  // 0xa8
  private           System.Object[]                 callParams  // 0xb0
  private           FlowCanvas.ValueInput[]         inputs  // 0xb8
  private           FlowCanvas.ValueInput[]         arrayInputs  // 0xc0
  private           System.Int32                    arrayParamsInput  // 0xc8
  private           System.Type                     arrayParamsType  // 0xd0
METHODS:
  RVA=0x0A930A68  token=0x60007A2  System.Boolean InitInternal(System.Reflection.MethodInfo method)
  RVA=0x0A9308A0  token=0x60007A3  System.Void Call()
  RVA=0x0A930DA8  token=0x60007A4  System.Void RegisterOutput(FlowCanvas.FlowNode node, System.Boolean callable, FlowCanvas.Nodes.ParamDef def, System.Int32 idx)
  RVA=0x0A930BF0  token=0x60007A5  System.Void RegisterInput(FlowCanvas.FlowNode node, FlowCanvas.Nodes.ParamDef def, System.Int32 idx)
  RVA=0x0A930EA4  token=0x60007A6  System.Void RegisterPorts(FlowCanvas.FlowNode node, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=0x04DA6530  token=0x60007A7  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ReflectedConstructorNodeWrapper
TYPE:  class
TOKEN: 0x20001EB
SIZE:  0xC8
EXTENDS: FlowCanvas.Nodes.ReflectedMethodBaseNodeWrapper
FIELDS:
  private           ParadoxNotion.Serialization.SerializedConstructorInfo_constructor  // 0xb8
  private           FlowCanvas.Nodes.BaseReflectedConstructorNode<reflectedConstructorNode>k__BackingField  // 0xc0
PROPERTIES:
  reflectedConstructorNode  get=0x04D892F0  set=0x06AC5ACC
  constructor  get=0x0A931FA0
  serializedMethodBase  get=0x04D85EF0
  name  get=0x0A931FB4
METHODS:
  RVA=0x0A931F38  token=0x60007B4  System.Void SetMethodBase(System.Reflection.MethodBase newMethod, System.Object instance)
  RVA=0x0A931EA4  token=0x60007B5  System.Void SetConstructor(System.Reflection.ConstructorInfo newConstructor)
  RVA=0x0A931DD4  token=0x60007B6  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x60007B7  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ReflectedExtractorNodeWrapper`1
TYPE:  class
TOKEN: 0x20001EC
EXTENDS: FlowCanvas.FlowNode
FIELDS:
  private   static  System.Collections.Generic.Dictionary<System.String,System.Reflection.MemberInfo>_memberInfos  // static @ 0x0
  private   static  System.Collections.Generic.List<System.String>_instanceMemberNames  // static @ 0x0
  private   static  System.Collections.Generic.List<System.String>_staticMemberNames  // static @ 0x0
  private           System.Collections.Generic.List<System.String>_selectedInstanceMembers  // 0x0
  private           FlowCanvas.Nodes.BaseReflectedExtractorNodeextractorNode  // 0x0
PROPERTIES:
  name  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60007B8  System.Type GetRuntimeIconType()
  RVA=-1  // not resolved  token=0x60007B9  System.Void PopulateInfos()
  RVA=-1  // not resolved  token=0x60007BB  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x60007BC  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ReflectedFieldNodeWrapper_0
TYPE:  class
TOKEN: 0x20001ED
SIZE:  0x20
FIELDS:
  public            System.String                   fieldName  // 0x10
  public            System.Type                     targetType  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60007BD  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ReflectedFieldNodeWrapper
TYPE:  class
TOKEN: 0x20001EE
SIZE:  0xC8
EXTENDS: FlowCanvas.FlowNode
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper ParadoxNotion.Serialization.FullSerializer.IMigratable`1 ParadoxNotion.Serialization.FullSerializer.IMigratable
FIELDS:
  private           ParadoxNotion.Serialization.SerializedFieldInfo_field  // 0xb0
  private           FlowCanvas.Nodes.ReflectedFieldNodeWrapper.AccessModeaccessMode  // 0xb8
  private           FlowCanvas.Nodes.BaseReflectedFieldNode<reflectedFieldNode>k__BackingField  // 0xc0
PROPERTIES:
  reflectedFieldNode  get=0x04D892F0  set=0x06AC5ACC
  field  get=0x091C020C
  name  get=0x0A950524
METHODS:
  RVA=0x0A950280  token=0x60007BE  System.Void ParadoxNotion.Serialization.FullSerializer.IMigratable<FlowCanvas.Nodes.ReflectedFieldNodeWrapper_0>.Migrate(FlowCanvas.Nodes.ReflectedFieldNodeWrapper_0 model)
  RVA=0x04D86250  token=0x60007BF  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A950230  token=0x60007C0  System.Type GetRuntimeIconType()
  RVA=0x0A9503DC  token=0x60007C5  System.Void SetField(System.Reflection.FieldInfo newField, FlowCanvas.Nodes.ReflectedFieldNodeWrapper.AccessMode mode, System.Object instance)
  RVA=0x0A950340  token=0x60007C6  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x60007C7  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ReflectedMethodBaseNodeWrapper
TYPE:  abstract class
TOKEN: 0x20001F0
SIZE:  0xB8
EXTENDS: FlowCanvas.FlowNode
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper
FIELDS:
  protected         System.Boolean                  _callable  // 0xb0
  protected         System.Boolean                  _exposeParams  // 0xb1
  protected         System.Int32                    _exposedParamsCount  // 0xb4
PROPERTIES:
  serializedMethodBase  get=-1  // abstract
  method  get=0x0A9509EC
  callable  get=0x04D88CC0  set=0x0A950A58
  exposeParams  get=0x04D88CB0  set=0x0A950A70
  exposedParamsCount  get=0x04D892E0  set=0x0A950A9C
METHODS:
  RVA=0x0A950824  token=0x60007C8  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A9507E0  token=0x60007C9  System.Type GetRuntimeIconType()
  RVA=-1  // abstract  token=0x60007D2  System.Void SetMethodBase(System.Reflection.MethodBase newMethod, System.Object instance)
  RVA=0x0A950834  token=0x60007D3  System.Void SetDefaultParameterValues(System.Reflection.MethodBase newMethod)
  RVA=0x0A95094C  token=0x60007D4  System.Void SetDropInstanceReference(System.Reflection.MethodBase newMethod, System.Object instance)
  RVA=0x0489BC70  token=0x60007D5  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ReflectedMethodNodeWrapper
TYPE:  class
TOKEN: 0x20001F1
SIZE:  0xC8
EXTENDS: FlowCanvas.Nodes.ReflectedMethodBaseNodeWrapper
FIELDS:
  private           ParadoxNotion.Serialization.SerializedMethodInfo_method  // 0xb8
  private           FlowCanvas.Nodes.BaseReflectedMethodNode<reflectedMethodNode>k__BackingField  // 0xc0
PROPERTIES:
  reflectedMethodNode  get=0x04D892F0  set=0x06AC5ACC
  method  get=0x0A950F34
  serializedMethodBase  get=0x04D85EF0
  name  get=0x0A950F48
METHODS:
  RVA=0x0A950CFC  token=0x60007DB  System.Void SetMethodBase(System.Reflection.MethodBase newMethod, System.Object instance)
  RVA=0x0A950D74  token=0x60007DC  System.Void SetMethod(System.Reflection.MethodInfo newMethod, System.Object instance)
  RVA=0x0A950B10  token=0x60007DD  System.Void OnPortConnected(FlowCanvas.Port port, FlowCanvas.Port otherPort)
  RVA=0x0A950AC0  token=0x60007DE  System.Type GetNodeWildDefinitionType()
  RVA=0x0A950C70  token=0x60007DF  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x60007E0  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ReflectedMethodRegistrationOptions
TYPE:  sealed struct
TOKEN: 0x20001F2
SIZE:  0x18
FIELDS:
  public            System.Boolean                  callable  // 0x10
  public            System.Boolean                  exposeParams  // 0x11
  public            System.Int32                    exposedParamsCount  // 0x14
METHODS:
END_CLASS

CLASS: FlowCanvas.Nodes.FlowNestedBase`1
TYPE:  abstract class
TOKEN: 0x20001F3
EXTENDS: FlowCanvas.FlowNodeNested`1
IMPLEMENTS: FlowCanvas.Nodes.IDropedReferenceNode NodeCanvas.Framework.IGraphElement NodeCanvas.Framework.IUpdatable
FIELDS:
  public            NodeCanvas.Framework.BBParameter<T>_subGraph  // 0x0
  private           FlowCanvas.ValueInput<UnityEngine.Component>targetAgent  // 0x0
  private           FlowCanvas.FlowOutput           onStart  // 0x0
  private           FlowCanvas.FlowOutput           onUpdate  // 0x0
  private           FlowCanvas.FlowOutput           onFinish  // 0x0
  private           System.Boolean                  paused  // 0x0
  private           System.Boolean                  endResult  // 0x0
PROPERTIES:
  subGraph  get=-1  // not resolved  set=-1  // not resolved
  subGraphParameter  get=-1  // not resolved
  iconAlignment  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60007E5  System.Void FlowCanvas.Nodes.IDropedReferenceNode.SetTarget(UnityEngine.Object target)
  RVA=-1  // not resolved  token=0x60007E6  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x60007E7  System.Void Start(FlowCanvas.Flow f)
  RVA=-1  // not resolved  token=0x60007E8  System.Void Stop(FlowCanvas.Flow f)
  RVA=-1  // not resolved  token=0x60007E9  System.Void OnStop(FlowCanvas.Flow f)
  RVA=-1  // not resolved  token=0x60007EA  System.Void Pause(FlowCanvas.Flow f)
  RVA=-1  // not resolved  token=0x60007EB  System.Void Resume(FlowCanvas.Flow f)
  RVA=-1  // not resolved  token=0x60007EC  System.Void NodeCanvas.Framework.IUpdatable.Update()
  RVA=-1  // not resolved  token=0x60007ED  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60007EE  System.Single <RegisterPorts>b__15_0()
  RVA=-1  // not resolved  token=0x60007EF  System.Boolean <RegisterPorts>b__15_1()
END_CLASS

CLASS: FlowCanvas.Nodes.FlowNestedFlow
TYPE:  class
TOKEN: 0x20001F5
SIZE:  0xF8
EXTENDS: FlowCanvas.Nodes.FlowNestedBase`1
FIELDS:
METHODS:
  RVA=0x0A94BCD4  token=0x60007F2  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CreateCollection`1
TYPE:  class
TOKEN: 0x20001F6
EXTENDS: FlowCanvas.FlowNode
FIELDS:
  private           System.Int32                    _portCount  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60007F3  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x60007F4  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CreateDictionary`1
TYPE:  class
TOKEN: 0x20001F9
EXTENDS: FlowCanvas.FlowNode
FIELDS:
  private           System.Int32                    _portCount  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60007FA  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x60007FB  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.GetOtherVariable`1
TYPE:  class
TOKEN: 0x20001FD
EXTENDS: FlowCanvas.FlowNode
FIELDS:
PROPERTIES:
  name  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000806  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x6000807  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.GetVariable`1
TYPE:  class
TOKEN: 0x20001FF
EXTENDS: FlowCanvas.Nodes.ParameterVariableNode
FIELDS:
  public            NodeCanvas.Framework.BBParameter<T>value  // 0x0
PROPERTIES:
  parameter  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600080B  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x600080C  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600080D  T <RegisterPorts>b__3_0()
END_CLASS

CLASS: FlowCanvas.Nodes.RelayValueInputBase
TYPE:  abstract class
TOKEN: 0x2000200
SIZE:  0xB0
EXTENDS: FlowCanvas.FlowNode
FIELDS:
PROPERTIES:
  relayType  get=-1  // abstract
METHODS:
  RVA=0x0489BC70  token=0x600080F  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.RelayValueInput`1
TYPE:  class
TOKEN: 0x2000201
EXTENDS: FlowCanvas.Nodes.RelayValueInputBase
IMPLEMENTS: FlowCanvas.IEditorMenuCallbackReceiver
FIELDS:
  public            System.String                   identifier  // 0x0
  private           FlowCanvas.ValueInput<T>        <port>k__BackingField  // 0x0
  private           System.Boolean                  <cached>k__BackingField  // 0x0
  private           T                               <cachedValue>k__BackingField  // 0x0
PROPERTIES:
  port  get=-1  // not resolved  set=-1  // not resolved
  cached  get=-1  // not resolved  set=-1  // not resolved
  cachedValue  get=-1  // not resolved  set=-1  // not resolved
  relayType  get=-1  // not resolved
  name  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000818  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x6000819  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.RelayValueOutputBase
TYPE:  abstract class
TOKEN: 0x2000203
SIZE:  0xB0
EXTENDS: FlowCanvas.FlowNode
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600081C  System.Void SetSource(FlowCanvas.Nodes.RelayValueInputBase source)
  RVA=0x0489BC70  token=0x600081D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.RelayValueOutput`1
TYPE:  class
TOKEN: 0x2000204
EXTENDS: FlowCanvas.Nodes.RelayValueOutputBase
FIELDS:
  private           System.String                   _sourceInputUID  // 0x0
  private           System.WeakReference<FlowCanvas.Nodes.RelayValueInputBase>_sourceInputRef  // 0x0
PROPERTIES:
  sourceInputUID  get=-1  // not resolved  set=-1  // not resolved
  sourceInput  get=-1  // not resolved
  name  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000822  System.Void SetSource(FlowCanvas.Nodes.RelayValueInputBase source)
  RVA=-1  // not resolved  token=0x6000823  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x6000824  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000825  System.Boolean <get_sourceInput>b__6_0(FlowCanvas.Nodes.RelayValueInput<T> i)
  RVA=-1  // not resolved  token=0x6000826  T <RegisterPorts>b__10_0()
END_CLASS

CLASS: FlowCanvas.Nodes.NullObject
TYPE:  class
TOKEN: 0x2000205
SIZE:  0xB0
EXTENDS: FlowCanvas.FlowNode
FIELDS:
METHODS:
  RVA=0x0A94EFA8  token=0x6000827  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x6000828  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.OwnerVariable
TYPE:  class
TOKEN: 0x2000207
SIZE:  0xB0
EXTENDS: FlowCanvas.FlowNode
FIELDS:
PROPERTIES:
  name  get=0x0A94F224
METHODS:
  RVA=0x0A94F0EC  token=0x600082D  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x600082E  System.Void .ctor()
  RVA=0x0A94F1A4  token=0x600082F  UnityEngine.GameObject <RegisterPorts>b__2_0()
END_CLASS

CLASS: FlowCanvas.Nodes.ParameterVariableNode
TYPE:  abstract class
TOKEN: 0x2000208
SIZE:  0xB0
EXTENDS: FlowCanvas.FlowNode
FIELDS:
PROPERTIES:
  parameter  get=-1  // abstract
METHODS:
  RVA=0x0489BC70  token=0x6000831  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SetOtherVariable`1
TYPE:  class
TOKEN: 0x2000209
EXTENDS: FlowCanvas.FlowNode
FIELDS:
  public            ParadoxNotion.OperationMethod   operation  // 0x0
  private           FlowCanvas.ValueInput<System.String>varName  // 0x0
PROPERTIES:
  name  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000833  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x6000834  System.Void DoSet(NodeCanvas.Framework.Blackboard bb, System.String name, T value)
  RVA=-1  // not resolved  token=0x6000835  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SetVariable`1
TYPE:  class
TOKEN: 0x200020B
EXTENDS: FlowCanvas.Nodes.ParameterVariableNode
FIELDS:
  public            NodeCanvas.Framework.BBParameter<T>targetVariable  // 0x0
  public            ParadoxNotion.OperationMethod   operation  // 0x0
  public            System.Boolean                  perSecond  // 0x0
PROPERTIES:
  parameter  get=-1  // not resolved
  name  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600083B  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x600083C  System.Void DoSet(T value)
  RVA=-1  // not resolved  token=0x600083D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CodeEventBase
TYPE:  abstract class
TOKEN: 0x200020D
SIZE:  0xD0
EXTENDS: FlowCanvas.Nodes.EventNode`1
FIELDS:
  protected         System.String                   eventName  // 0xb8
  protected         System.Type                     targetType  // 0xc0
  protected         UnityEngine.Component           targetComponent  // 0xc8
PROPERTIES:
  eventInfo  get=0x0A94ADB8
METHODS:
  RVA=0x0A94ACF0  token=0x6000842  System.Void SetEvent(System.Reflection.EventInfo e, System.Object instace)
  RVA=0x0A94AB60  token=0x6000843  System.Void OnPostGraphStarted()
  RVA=0x0A94AD84  token=0x6000844  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CodeEvent
TYPE:  class
TOKEN: 0x200020E
SIZE:  0xE0
EXTENDS: FlowCanvas.Nodes.CodeEventBase
FIELDS:
  private           FlowCanvas.FlowOutput           o  // 0xd0
  private           System.Action                   pointer  // 0xd8
METHODS:
  RVA=0x0A94AF58  token=0x6000845  System.Void OnPostGraphStarted()
  RVA=0x0A94AE98  token=0x6000846  System.Void OnGraphStoped()
  RVA=0x0A94B00C  token=0x6000847  System.Void RegisterPorts()
  RVA=0x0A94AE48  token=0x6000848  System.Void Call()
  RVA=0x093DAC58  token=0x6000849  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CodeEvent`1
TYPE:  class
TOKEN: 0x200020F
EXTENDS: FlowCanvas.Nodes.CodeEventBase
FIELDS:
  private           FlowCanvas.FlowOutput           o  // 0x0
  private           System.Action<T>                pointer  // 0x0
  private           T                               eventValue  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600084A  System.Void OnPostGraphStarted()
  RVA=-1  // not resolved  token=0x600084B  System.Void OnGraphStoped()
  RVA=-1  // not resolved  token=0x600084C  System.Void Call(T eventValue)
  RVA=-1  // not resolved  token=0x600084D  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x600084E  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600084F  T <RegisterPorts>b__6_0()
END_CLASS

CLASS: FlowCanvas.Nodes.NewGameObject
TYPE:  class
TOKEN: 0x2000210
SIZE:  0x38
EXTENDS: FlowCanvas.Nodes.CallableFunctionNode`4
FIELDS:
METHODS:
  RVA=0x0A94ED64  token=0x6000850  UnityEngine.GameObject Invoke(System.String name, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
  RVA=0x0A94EE38  token=0x6000851  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.NewVector2
TYPE:  class
TOKEN: 0x2000211
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x04DA6680  token=0x6000852  UnityEngine.Vector2 Invoke(System.Single x, System.Single y)
  RVA=0x0A94EF30  token=0x6000853  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.NewVector3
TYPE:  class
TOKEN: 0x2000212
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`4
FIELDS:
METHODS:
  RVA=0x04DA6690  token=0x6000854  UnityEngine.Vector3 Invoke(System.Single x, System.Single y, System.Single z)
  RVA=0x0A94EF58  token=0x6000855  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.NewVector4
TYPE:  class
TOKEN: 0x2000213
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`5
FIELDS:
METHODS:
  RVA=0x04DA6650  token=0x6000856  UnityEngine.Vector4 Invoke(System.Single x, System.Single y, System.Single z, System.Single w)
  RVA=0x0A94EF80  token=0x6000857  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.NewQuaternion
TYPE:  class
TOKEN: 0x2000214
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`5
FIELDS:
METHODS:
  RVA=0x04DA6650  token=0x6000858  UnityEngine.Quaternion Invoke(System.Single x, System.Single y, System.Single z, System.Single w)
  RVA=0x0A94EE60  token=0x6000859  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.NewColor
TYPE:  class
TOKEN: 0x2000215
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`5
FIELDS:
METHODS:
  RVA=0x04DA6650  token=0x600085A  UnityEngine.Color Invoke(System.Single r, System.Single g, System.Single b, System.Single a)
  RVA=0x0A94ED3C  token=0x600085B  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.NewBounds
TYPE:  class
TOKEN: 0x2000216
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A94ECBC  token=0x600085C  UnityEngine.Bounds Invoke(UnityEngine.Vector3 center, UnityEngine.Vector3 size)
  RVA=0x0A94ED14  token=0x600085D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.NewRect
TYPE:  class
TOKEN: 0x2000217
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`5
FIELDS:
METHODS:
  RVA=0x04DA6650  token=0x600085E  UnityEngine.Rect Invoke(System.Single left, System.Single top, System.Single width, System.Single height)
  RVA=0x0A94EF08  token=0x600085F  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.NewRay
TYPE:  class
TOKEN: 0x2000218
SIZE:  0x30
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=0x0A94EE88  token=0x6000860  UnityEngine.Ray Invoke(UnityEngine.Vector3 origin, UnityEngine.Vector3 direction)
  RVA=0x0A94EEE0  token=0x6000861  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ConvertTo`1
TYPE:  class
TOKEN: 0x2000219
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000862  T Invoke(System.IConvertible obj)
  RVA=-1  // not resolved  token=0x6000863  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CastTo`1
TYPE:  class
TOKEN: 0x200021A
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000864  T Invoke(System.Object obj)
  RVA=-1  // not resolved  token=0x6000865  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ToArray`1
TYPE:  class
TOKEN: 0x200021B
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000866  T[] Invoke(System.Collections.Generic.IList<T> list)
  RVA=-1  // not resolved  token=0x6000867  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ToList`1
TYPE:  class
TOKEN: 0x200021C
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000868  System.Collections.Generic.List<T> Invoke(System.Collections.Generic.IList<T> list)
  RVA=-1  // not resolved  token=0x6000869  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CustomObjectWrapper
TYPE:  abstract class
TOKEN: 0x200021D
SIZE:  0xB0
EXTENDS: FlowCanvas.FlowNode
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600086A  System.Void SetTarget(UnityEngine.Object target)
  RVA=0x0489BC70  token=0x600086B  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.CustomObjectWrapper`1
TYPE:  abstract class
TOKEN: 0x200021E
EXTENDS: FlowCanvas.Nodes.CustomObjectWrapper
FIELDS:
  private           T                               _target  // 0x0
PROPERTIES:
  target  get=-1  // not resolved  set=-1  // not resolved
  name  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600086F  System.Void SetTarget(UnityEngine.Object target)
  RVA=-1  // not resolved  token=0x6000870  System.Type GetRuntimeIconType()
  RVA=-1  // not resolved  token=0x6000871  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractVector2
TYPE:  class
TOKEN: 0x200021F
SIZE:  0x38
EXTENDS: FlowCanvas.Nodes.ExtractorNode`3
FIELDS:
METHODS:
  RVA=0x04DA6610  token=0x6000872  System.Void Invoke(UnityEngine.Vector2 vector, System.Single& x, System.Single& y)
  RVA=0x0A94B84C  token=0x6000873  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractVector3
TYPE:  class
TOKEN: 0x2000220
SIZE:  0x40
EXTENDS: FlowCanvas.Nodes.ExtractorNode`4
FIELDS:
METHODS:
  RVA=0x04DA6630  token=0x6000874  System.Void Invoke(UnityEngine.Vector3 vector, System.Single& x, System.Single& y, System.Single& z)
  RVA=0x0A94B874  token=0x6000875  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractVector4
TYPE:  class
TOKEN: 0x2000221
SIZE:  0x40
EXTENDS: FlowCanvas.Nodes.ExtractorNode`5
FIELDS:
METHODS:
  RVA=0x04DA6590  token=0x6000876  System.Void Invoke(UnityEngine.Vector4 vector, System.Single& x, System.Single& y, System.Single& z, System.Single& w)
  RVA=0x0A94B89C  token=0x6000877  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractQuaternion
TYPE:  class
TOKEN: 0x2000222
SIZE:  0x50
EXTENDS: FlowCanvas.Nodes.ExtractorNode`6
FIELDS:
METHODS:
  RVA=0x0A94B4FC  token=0x6000878  System.Void Invoke(UnityEngine.Quaternion quaternion, System.Single& x, System.Single& y, System.Single& z, System.Single& w, UnityEngine.Vector3& eulerAngles)
  RVA=0x0A94B544  token=0x6000879  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractRect
TYPE:  class
TOKEN: 0x2000223
SIZE:  0x48
EXTENDS: FlowCanvas.Nodes.ExtractorNode`6
FIELDS:
METHODS:
  RVA=0x0A94B7C0  token=0x600087A  System.Void Invoke(UnityEngine.Rect rect, UnityEngine.Vector2& center, System.Single& xMin, System.Single& xMax, System.Single& yMin, System.Single& yMax)
  RVA=0x0A94B824  token=0x600087B  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractColor
TYPE:  class
TOKEN: 0x2000224
SIZE:  0x40
EXTENDS: FlowCanvas.Nodes.ExtractorNode`5
FIELDS:
METHODS:
  RVA=0x04DA6590  token=0x600087C  System.Void Invoke(UnityEngine.Color color, System.Single& r, System.Single& g, System.Single& b, System.Single& a)
  RVA=0x0A94B3A4  token=0x600087D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractRaycastHit
TYPE:  class
TOKEN: 0x2000225
SIZE:  0x58
EXTENDS: FlowCanvas.Nodes.ExtractorNode`5
FIELDS:
METHODS:
  RVA=0x0A94B6DC  token=0x600087E  System.Void Invoke(UnityEngine.RaycastHit hit, UnityEngine.GameObject& gameObject, System.Single& distance, UnityEngine.Vector3& normal, UnityEngine.Vector3& point)
  RVA=0x0A94B798  token=0x600087F  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractRaycastHit2D
TYPE:  class
TOKEN: 0x2000226
SIZE:  0x58
EXTENDS: FlowCanvas.Nodes.ExtractorNode`6
FIELDS:
METHODS:
  RVA=0x0A94B594  token=0x6000880  System.Void Invoke(UnityEngine.RaycastHit2D hit, UnityEngine.GameObject& gameObject, System.Single& distance, System.Single& fraction, UnityEngine.Vector3& normal, UnityEngine.Vector3& point)
  RVA=0x0A94B6B4  token=0x6000881  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractRay
TYPE:  class
TOKEN: 0x2000227
SIZE:  0x48
EXTENDS: FlowCanvas.Nodes.ExtractorNode`3
FIELDS:
METHODS:
  RVA=0x04DA65E0  token=0x6000882  System.Void Invoke(UnityEngine.Ray ray, UnityEngine.Vector3& origin, UnityEngine.Vector3& direction)
  RVA=0x0A94B56C  token=0x6000883  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractBounds
TYPE:  class
TOKEN: 0x2000228
SIZE:  0x70
EXTENDS: FlowCanvas.Nodes.ExtractorNode`6
FIELDS:
METHODS:
  RVA=0x0A94B11C  token=0x6000884  System.Void Invoke(UnityEngine.Bounds bounds, UnityEngine.Vector3& center, UnityEngine.Vector3& extents, UnityEngine.Vector3& max, UnityEngine.Vector3& min, UnityEngine.Vector3& size)
  RVA=0x0A94B1B4  token=0x6000885  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractCollision
TYPE:  class
TOKEN: 0x2000229
SIZE:  0x78
EXTENDS: FlowCanvas.Nodes.ExtractorNode`5
FIELDS:
METHODS:
  RVA=0x0A94B2C8  token=0x6000886  System.Void Invoke(UnityEngine.Collision collision, UnityEngine.ContactPoint[]& contacts, UnityEngine.ContactPoint& firstContact, UnityEngine.GameObject& gameObject, UnityEngine.Vector3& velocity)
  RVA=0x0A94B37C  token=0x6000887  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractCollision2D
TYPE:  class
TOKEN: 0x200022A
SIZE:  0x80
EXTENDS: FlowCanvas.Nodes.ExtractorNode`5
FIELDS:
METHODS:
  RVA=0x0A94B1DC  token=0x6000888  System.Void Invoke(UnityEngine.Collision2D collision, UnityEngine.ContactPoint2D[]& contacts, UnityEngine.ContactPoint2D& firstContact, UnityEngine.GameObject& gameObject, UnityEngine.Vector2& velocity)
  RVA=0x0A94B2A0  token=0x6000889  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractContactPoint
TYPE:  class
TOKEN: 0x200022B
SIZE:  0x58
EXTENDS: FlowCanvas.Nodes.ExtractorNode`5
FIELDS:
METHODS:
  RVA=0x0A94B44C  token=0x600088A  System.Void Invoke(UnityEngine.ContactPoint contactPoint, UnityEngine.Vector3& normal, UnityEngine.Vector3& point, UnityEngine.Collider& colliderA, UnityEngine.Collider& colliderB)
  RVA=0x0A94B4AC  token=0x600088B  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractContactPoint2D
TYPE:  class
TOKEN: 0x200022C
SIZE:  0x50
EXTENDS: FlowCanvas.Nodes.ExtractorNode`5
FIELDS:
METHODS:
  RVA=0x0A94B3CC  token=0x600088C  System.Void Invoke(UnityEngine.ContactPoint2D contactPoint, UnityEngine.Vector2& normal, UnityEngine.Vector2& point, UnityEngine.Collider2D& colliderA, UnityEngine.Collider2D& colliderB)
  RVA=0x0A94B424  token=0x600088D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractAnimationCurve
TYPE:  class
TOKEN: 0x200022D
SIZE:  0x48
EXTENDS: FlowCanvas.Nodes.ExtractorNode`5
FIELDS:
METHODS:
  RVA=0x0A94B074  token=0x600088E  System.Void Invoke(UnityEngine.AnimationCurve curve, UnityEngine.Keyframe[]& keys, System.Single& length, UnityEngine.WrapMode& postWrapMode, UnityEngine.WrapMode& preWrapMode)
  RVA=0x0A94B0F4  token=0x600088F  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.ExtractKeyFrame
TYPE:  class
TOKEN: 0x200022E
SIZE:  0x40
EXTENDS: FlowCanvas.Nodes.ExtractorNode`5
FIELDS:
METHODS:
  RVA=0x04DA65B0  token=0x6000890  System.Void Invoke(UnityEngine.Keyframe key, System.Single& inTangent, System.Single& outTangent, System.Single& time, System.Single& value)
  RVA=0x0A94B4D4  token=0x6000891  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.SwitchValue`1
TYPE:  class
TOKEN: 0x200022F
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`4
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000892  T Invoke(System.Boolean condition, T isTrue, T isFalse)
  RVA=-1  // not resolved  token=0x6000893  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.PickValue`1
TYPE:  class
TOKEN: 0x2000230
EXTENDS: FlowCanvas.Nodes.PureFunctionNode`3
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000894  T Invoke(System.Int32 index, System.Collections.Generic.IList<T> values)
  RVA=-1  // not resolved  token=0x6000895  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.StaticCodeEventBase
TYPE:  abstract class
TOKEN: 0x2000231
SIZE:  0xC0
EXTENDS: FlowCanvas.Nodes.EventNode
FIELDS:
  protected         System.String                   eventName  // 0xb0
  protected         System.Type                     targetType  // 0xb8
PROPERTIES:
  eventInfo  get=0x0A951CE0
METHODS:
  RVA=0x0A951C4C  token=0x6000897  System.Void SetEvent(System.Reflection.EventInfo e)
  RVA=0x0A951B98  token=0x6000898  System.Void OnGraphStarted()
  RVA=0x0489BC70  token=0x6000899  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.StaticCodeEvent
TYPE:  class
TOKEN: 0x2000232
SIZE:  0xD0
EXTENDS: FlowCanvas.Nodes.StaticCodeEventBase
FIELDS:
  private           FlowCanvas.FlowOutput           o  // 0xc0
  private           System.Action                   pointer  // 0xc8
METHODS:
  RVA=0x0A951DC0  token=0x600089A  System.Void OnGraphStarted()
  RVA=0x092E9C34  token=0x600089B  System.Void OnGraphStoped()
  RVA=0x0A951D70  token=0x600089C  System.Void Call()
  RVA=0x0A951E70  token=0x600089D  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x600089E  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.StaticCodeEvent`1
TYPE:  class
TOKEN: 0x2000233
EXTENDS: FlowCanvas.Nodes.StaticCodeEventBase
FIELDS:
  private           FlowCanvas.FlowOutput           o  // 0x0
  private           System.Action<T>                pointer  // 0x0
  private           T                               eventValue  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600089F  System.Void OnGraphStarted()
  RVA=-1  // not resolved  token=0x60008A0  System.Void OnGraphStoped()
  RVA=-1  // not resolved  token=0x60008A1  System.Void Call(T value)
  RVA=-1  // not resolved  token=0x60008A2  System.Void RegisterPorts()
  RVA=-1  // not resolved  token=0x60008A3  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60008A4  T <RegisterPorts>b__6_0()
END_CLASS

CLASS: FlowCanvas.Nodes.FlowNestedBT
TYPE:  class
TOKEN: 0x2000234
SIZE:  0xF8
EXTENDS: FlowCanvas.Nodes.FlowNestedBase`1
FIELDS:
METHODS:
  RVA=0x0A94B8C4  token=0x60008A5  System.Void RegisterPorts()
  RVA=0x0A94B9DC  token=0x60008A6  System.Void .ctor()
  RVA=0x0A94B998  token=0x60008A7  NodeCanvas.Framework.Status <RegisterPorts>b__0_0()
END_CLASS

CLASS: FlowCanvas.Nodes.FlowNestedFSM
TYPE:  class
TOKEN: 0x2000235
SIZE:  0xF8
EXTENDS: FlowCanvas.Nodes.FlowNestedBase`1
FIELDS:
METHODS:
  RVA=0x0A94BB80  token=0x60008A8  System.Void RegisterPorts()
  RVA=0x0A94BC98  token=0x60008A9  System.Void .ctor()
  RVA=0x0A94BC54  token=0x60008AA  NodeCanvas.StateMachines.IState <RegisterPorts>b__0_0()
END_CLASS

CLASS: FlowCanvas.Nodes.FlowNestedDT
TYPE:  class
TOKEN: 0x2000236
SIZE:  0xF8
EXTENDS: FlowCanvas.Nodes.FlowNestedBase`1
FIELDS:
METHODS:
  RVA=0x0A94BA18  token=0x60008AB  System.Void RegisterPorts()
  RVA=0x0A94BB44  token=0x60008AC  System.Void .ctor()
  RVA=0x0A94BAEC  token=0x60008AD  NodeCanvas.DialogueTrees.IDialogueActor <RegisterPorts>b__0_0()
END_CLASS

CLASS: FlowCanvas.Nodes.TaskAction
TYPE:  class
TOKEN: 0x2000237
SIZE:  0xC8
EXTENDS: FlowCanvas.FlowNode
IMPLEMENTS: NodeCanvas.Framework.ITaskAssignable`1 NodeCanvas.Framework.ITaskAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           NodeCanvas.Framework.ActionTask _action  // 0xb0
  private           FlowCanvas.FlowOutput           onFinish  // 0xb8
  private           UnityEngine.Coroutine           coroutine  // 0xc0
PROPERTIES:
  name  get=0x0A95229C
  action  get=0x04D86250  set=0x0A9522EC
  NodeCanvas.Framework.ITaskAssignable.task  get=0x04D86250  set=0x0A952024
METHODS:
  RVA=0x0A952094  token=0x60008B3  System.Void OnGraphStarted()
  RVA=0x0A9520AC  token=0x60008B4  System.Void OnGraphStoped()
  RVA=0x0A952078  token=0x60008B5  System.Void OnGraphPaused()
  RVA=0x0A95210C  token=0x60008B6  System.Void RegisterPorts()
  RVA=0x0A951F98  token=0x60008B7  System.Collections.IEnumerator DoUpdate(FlowCanvas.Flow f)
  RVA=0x0489BC70  token=0x60008B8  System.Void .ctor()
  RVA=0x0A9521EC  token=0x60008B9  System.Void <RegisterPorts>b__14_0(FlowCanvas.Flow f)
END_CLASS

CLASS: FlowCanvas.Nodes.TaskCondition
TYPE:  class
TOKEN: 0x2000239
SIZE:  0xB8
EXTENDS: FlowCanvas.FlowNode
IMPLEMENTS: NodeCanvas.Framework.ITaskAssignable`1 NodeCanvas.Framework.ITaskAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           NodeCanvas.Framework.ConditionTask_condition  // 0xb0
PROPERTIES:
  name  get=0x0A952504
  condition  get=0x04D86250  set=0x04D84CE0
  NodeCanvas.Framework.ITaskAssignable.task  get=0x04D86250  set=0x0A952324
METHODS:
  RVA=0x0A95237C  token=0x60008C5  System.Void OnGraphStarted()
  RVA=0x0A9523D4  token=0x60008C6  System.Void OnGraphStoped()
  RVA=0x0A9523F0  token=0x60008C7  System.Void RegisterPorts()
  RVA=0x0489BC70  token=0x60008C8  System.Void .ctor()
  RVA=0x0A9524A8  token=0x60008C9  System.Boolean <RegisterPorts>b__11_0()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedFieldNode
TYPE:  abstract class
TOKEN: 0x200023A
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0A9507A4  token=0x60008CA  FlowCanvas.Nodes.Legacy.ReflectedFieldNode Create(System.Reflection.FieldInfo field)
  RVA=-1  // abstract  token=0x60008CB  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.FieldInfo field, FlowCanvas.Nodes.ReflectedFieldNodeWrapper.AccessMode accessMode)
  RVA=0x041E1670  token=0x60008CC  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.PureReflectedFieldNode
TYPE:  sealed class
TOKEN: 0x200023B
SIZE:  0x10
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedFieldNode
FIELDS:
METHODS:
  RVA=0x0A94F250  token=0x60008CD  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.FieldInfo field, FlowCanvas.Nodes.ReflectedFieldNodeWrapper.AccessMode accessMode)
  RVA=0x041E1670  token=0x60008CE  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
TYPE:  abstract class
TOKEN: 0x2000240
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60008D7  System.Void .ctor()
  RVA=0x0A951220  token=0x60008D8  FlowCanvas.Nodes.Legacy.ReflectedMethodNode Create(System.Reflection.MethodInfo method)
  RVA=0x0A9514FC  token=0x60008D9  FlowCanvas.Nodes.Legacy.ReflectedMethodNode TryCreateJit(System.Reflection.MethodInfo method)
  RVA=0x0A9513EC  token=0x60008DA  System.String GetName(System.Reflection.MethodInfo method, System.Int32 i)
  RVA=-1  // abstract  token=0x60008DB  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.PureReflectedMethodNode
TYPE:  sealed class
TOKEN: 0x2000243
SIZE:  0x50
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
FIELDS:
  private           System.Reflection.MethodInfo    method  // 0x10
  private           FlowCanvas.ValueInput           instanceInput  // 0x18
  private           System.Collections.Generic.List<FlowCanvas.ValueInput>inputs  // 0x20
  private           System.Collections.Generic.List<FlowCanvas.ValueInput>paramsInputs  // 0x28
  private           System.Type                     paramsArrayType  // 0x30
  private           System.Object[]                 args  // 0x38
  private           System.Object                   instance  // 0x40
  private           System.Object                   returnValue  // 0x48
METHODS:
  RVA=0x0A94F930  token=0x60008E2  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=0x0A94F6C8  token=0x60008E3  System.Object CallMethod()
  RVA=0x041E1670  token=0x60008E4  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedActionNode
TYPE:  sealed class
TOKEN: 0x2000247
SIZE:  0x18
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
FIELDS:
  private           ParadoxNotion.ActionCall        call  // 0x10
METHODS:
  RVA=0x0A9500A8  token=0x60008EC  System.Void Call()
  RVA=0x0A9500CC  token=0x60008ED  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=0x041E1670  token=0x60008EE  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedActionNode`1
TYPE:  sealed class
TOKEN: 0x2000249
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
FIELDS:
  private           ParadoxNotion.ActionCall<T1>    call  // 0x0
  private           T1                              instance  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60008F1  System.Void Call(T1 a)
  RVA=-1  // not resolved  token=0x60008F2  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=-1  // not resolved  token=0x60008F3  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedActionNode`2
TYPE:  sealed class
TOKEN: 0x200024B
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
FIELDS:
  private           ParadoxNotion.ActionCall<T1,T2> call  // 0x0
  private           T1                              instance  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60008F7  System.Void Call(T1 a, T2 b)
  RVA=-1  // not resolved  token=0x60008F8  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=-1  // not resolved  token=0x60008F9  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedActionNode`3
TYPE:  sealed class
TOKEN: 0x200024D
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
FIELDS:
  private           ParadoxNotion.ActionCall<T1,T2,T3>call  // 0x0
  private           T1                              instance  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60008FD  System.Void Call(T1 a, T2 b, T3 c)
  RVA=-1  // not resolved  token=0x60008FE  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=-1  // not resolved  token=0x60008FF  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedActionNode`4
TYPE:  sealed class
TOKEN: 0x200024F
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
FIELDS:
  private           ParadoxNotion.ActionCall<T1,T2,T3,T4>call  // 0x0
  private           T1                              instance  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000903  System.Void Call(T1 a, T2 b, T3 c, T4 d)
  RVA=-1  // not resolved  token=0x6000904  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=-1  // not resolved  token=0x6000905  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedActionNode`5
TYPE:  sealed class
TOKEN: 0x2000251
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
FIELDS:
  private           ParadoxNotion.ActionCall<T1,T2,T3,T4,T5>call  // 0x0
  private           T1                              instance  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000909  System.Void Call(T1 a, T2 b, T3 c, T4 d, T5 e)
  RVA=-1  // not resolved  token=0x600090A  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=-1  // not resolved  token=0x600090B  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedActionNode`6
TYPE:  sealed class
TOKEN: 0x2000253
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
FIELDS:
  private           ParadoxNotion.ActionCall<T1,T2,T3,T4,T5,T6>call  // 0x0
  private           T1                              instance  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600090F  System.Void Call(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f)
  RVA=-1  // not resolved  token=0x6000910  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=-1  // not resolved  token=0x6000911  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedActionNode`7
TYPE:  sealed class
TOKEN: 0x2000255
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
FIELDS:
  private           ParadoxNotion.ActionCall<T1,T2,T3,T4,T5,T6,T7>call  // 0x0
  private           T1                              instance  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000915  System.Void Call(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g)
  RVA=-1  // not resolved  token=0x6000916  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=-1  // not resolved  token=0x6000917  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedActionNode`8
TYPE:  sealed class
TOKEN: 0x2000257
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
FIELDS:
  private           ParadoxNotion.ActionCall<T1,T2,T3,T4,T5,T6,T7,T8>call  // 0x0
  private           T1                              instance  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600091B  System.Void Call(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h)
  RVA=-1  // not resolved  token=0x600091C  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=-1  // not resolved  token=0x600091D  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedFunctionNode`1
TYPE:  sealed class
TOKEN: 0x2000259
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
FIELDS:
  private           ParadoxNotion.FunctionCall<TResult>call  // 0x0
  private           TResult                         returnValue  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000921  TResult Call()
  RVA=-1  // not resolved  token=0x6000922  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=-1  // not resolved  token=0x6000923  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedFunctionNode`2
TYPE:  sealed class
TOKEN: 0x200025C
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
FIELDS:
  private           ParadoxNotion.FunctionCall<T1,TResult>call  // 0x0
  private           TResult                         returnValue  // 0x0
  private           T1                              instance  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000928  TResult Call(T1 a)
  RVA=-1  // not resolved  token=0x6000929  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=-1  // not resolved  token=0x600092A  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedFunctionNode`3
TYPE:  sealed class
TOKEN: 0x200025F
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
FIELDS:
  private           ParadoxNotion.FunctionCall<T1,T2,TResult>call  // 0x0
  private           TResult                         returnValue  // 0x0
  private           T1                              instance  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000930  TResult Call(T1 a, T2 b)
  RVA=-1  // not resolved  token=0x6000931  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=-1  // not resolved  token=0x6000932  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedFunctionNode`4
TYPE:  sealed class
TOKEN: 0x2000262
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
FIELDS:
  private           ParadoxNotion.FunctionCall<T1,T2,T3,TResult>call  // 0x0
  private           TResult                         returnValue  // 0x0
  private           T1                              instance  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000938  TResult Call(T1 a, T2 b, T3 c)
  RVA=-1  // not resolved  token=0x6000939  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=-1  // not resolved  token=0x600093A  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedFunctionNode`5
TYPE:  sealed class
TOKEN: 0x2000265
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
FIELDS:
  private           ParadoxNotion.FunctionCall<T1,T2,T3,T4,TResult>call  // 0x0
  private           TResult                         returnValue  // 0x0
  private           T1                              instance  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000940  TResult Call(T1 a, T2 b, T3 c, T4 d)
  RVA=-1  // not resolved  token=0x6000941  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=-1  // not resolved  token=0x6000942  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedFunctionNode`6
TYPE:  sealed class
TOKEN: 0x2000268
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
FIELDS:
  private           ParadoxNotion.FunctionCall<T1,T2,T3,T4,T5,TResult>call  // 0x0
  private           TResult                         returnValue  // 0x0
  private           T1                              instance  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000948  TResult Call(T1 a, T2 b, T3 c, T4 d, T5 e)
  RVA=-1  // not resolved  token=0x6000949  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=-1  // not resolved  token=0x600094A  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedFunctionNode`7
TYPE:  sealed class
TOKEN: 0x200026B
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
FIELDS:
  private           ParadoxNotion.FunctionCall<T1,T2,T3,T4,T5,T6,TResult>call  // 0x0
  private           TResult                         returnValue  // 0x0
  private           T1                              instance  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000950  TResult Call(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f)
  RVA=-1  // not resolved  token=0x6000951  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=-1  // not resolved  token=0x6000952  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedFunctionNode`8
TYPE:  sealed class
TOKEN: 0x200026E
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
FIELDS:
  private           ParadoxNotion.FunctionCall<T1,T2,T3,T4,T5,T6,T7,TResult>call  // 0x0
  private           TResult                         returnValue  // 0x0
  private           T1                              instance  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000958  TResult Call(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g)
  RVA=-1  // not resolved  token=0x6000959  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=-1  // not resolved  token=0x600095A  System.Void .ctor()
END_CLASS

CLASS: FlowCanvas.Nodes.Legacy.ReflectedFunctionNode`9
TYPE:  sealed class
TOKEN: 0x2000271
EXTENDS: FlowCanvas.Nodes.Legacy.ReflectedMethodNode
FIELDS:
  private           ParadoxNotion.FunctionCall<T1,T2,T3,T4,T5,T6,T7,T8,TResult>call  // 0x0
  private           TResult                         returnValue  // 0x0
  private           T1                              instance  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000960  TResult Call(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h)
  RVA=-1  // not resolved  token=0x6000961  System.Void RegisterPorts(FlowCanvas.FlowNode node, System.Reflection.MethodInfo method, FlowCanvas.Nodes.ReflectedMethodRegistrationOptions options)
  RVA=-1  // not resolved  token=0x6000962  System.Void .ctor()
END_CLASS

CLASS: HG.Rendering.Runtime.HGConstantBufferLayoutAttribute
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000007  System.Void .ctor()
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x2000535
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x600195F  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x0A9A30D8  token=0x6001960  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x2000536
SIZE:  0x40
EXTENDS: IFix.Core.AnonymousStorey
FIELDS:
METHODS:
  RVA=0x0A9A3180  token=0x6001961  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x0A9A313C  token=0x6001962  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x2000537
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6001963  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x0A9A5038  token=0x6001964  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x0A9A5128  token=0x6001965  System.Boolean IsPatched(System.Int32 id)
  RVA=0x0A9A4EF4  token=0x6001966  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x0A9A4FC0  token=0x6001967  System.Object CreateWrapper(System.Int32 id)
  RVA=0x0A9A509C  token=0x6001968  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x0A9A4E50  token=0x6001969  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: NodeCanvas.AssemblyWrapUtils
TYPE:  static class
TOKEN: 0x2000283
SIZE:  0x10
FIELDS:
  public    static  System.Boolean                  disableProjectPanelSuffixValidate  // static @ 0x0
  public    static  System.Func<System.Boolean>     autoLockOpenBehaviourTree  // static @ 0x8
  public    static  System.Func<System.Boolean>     autoLockOpenStateMachine  // static @ 0x10
  public    static  System.Func<System.Boolean>     autoLockOpenDialogTree  // static @ 0x18
  public    static  System.Func<System.Boolean>     autoLockOpenSAGraph  // static @ 0x20
  public    static  System.Func<System.Boolean>     autoLockOpenLevelScript  // static @ 0x28
  public    static  System.Func<System.Boolean>     autoLockOpenLevelScriptModule  // static @ 0x30
  public    static  System.Func<System.Boolean>     autoLockOpenInteractiveConfig  // static @ 0x38
  public    static  System.Func<System.Boolean>     autoLockOpenMissionGraph  // static @ 0x40
  public    static  System.Func<System.Boolean>     autoLockOpenGuideGraph  // static @ 0x48
METHODS:
END_CLASS

CLASS: NodeCanvas.ActionListPlayer
TYPE:  class
TOKEN: 0x2000284
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: NodeCanvas.Framework.ITaskSystem UnityEngine.ISerializationCallbackReceiver
FIELDS:
  public            System.Boolean                  playOnAwake  // 0x18
  private           System.String                   _serializedList  // 0x20
  private           System.Collections.Generic.List<UnityEngine.Object>_objectReferences  // 0x28
  private           NodeCanvas.Framework.Blackboard _blackboard  // 0x30
  private           NodeCanvas.Framework.ActionList _actionList  // 0x38
  private           System.Single                   timeStarted  // 0x40
PROPERTIES:
  actionList  get=0x04D85A50
  elapsedTime  get=0x0A93D20C
  deltaTime  get=0x05391A8C
  NodeCanvas.Framework.ITaskSystem.contextObject  get=0x0464E630
  NodeCanvas.Framework.ITaskSystem.agent  get=0x0464E630
  blackboard  get=0x011F36E0  set=0x0A93D228
METHODS:
  RVA=0x0A93CF44  token=0x60009A4  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x0A93CE40  token=0x60009A5  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x0A93CCB8  token=0x60009AD  NodeCanvas.ActionListPlayer Create()
  RVA=0x0A93CC8C  token=0x60009AE  System.Void Awake()
  RVA=0x0A93D070  token=0x60009AF  System.Void UpdateTasksOwner()
  RVA=0x041E1670  token=0x60009B0  System.Void NodeCanvas.Framework.ITaskSystem.SendEvent(System.String name, System.Object value, System.Object sender)
  RVA=-1  // generic def  token=0x60009B1  System.Void NodeCanvas.Framework.ITaskSystem.SendEvent(System.String name, T value, System.Object sender)
  RVA=0x0A93CE1C  token=0x60009B2  System.Void Play()
  RVA=0x0A93CDF8  token=0x60009B3  System.Void Play(System.Action<NodeCanvas.Framework.Status> OnFinish)
  RVA=0x0A93CD88  token=0x60009B4  System.Void Play(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard, System.Action<NodeCanvas.Framework.Status> OnFinish)
  RVA=0x0A93CD5C  token=0x60009B5  NodeCanvas.Framework.Status Execute()
  RVA=0x0A93CD34  token=0x60009B6  NodeCanvas.Framework.Status Execute(UnityEngine.Component agent)
  RVA=0x05393520  token=0x60009B7  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.BehaviourTree
TYPE:  class
TOKEN: 0x20003B4
SIZE:  0xB0
EXTENDS: NodeCanvas.Framework.Graph
FIELDS:
  public            System.Boolean                  repeat  // 0xa0
  public            System.Single                   updateInterval  // 0xa4
  private   static  System.Action<NodeCanvas.BehaviourTrees.BehaviourTree,NodeCanvas.Framework.Status>onRootStatusChanged  // static @ 0x0
  private           System.Single                   intervalCounter  // 0xa8
  private           NodeCanvas.Framework.Status     _rootStatus  // 0xac
PROPERTIES:
  rootStatus  get=0x04D88CA0  set=0x04B2D320
  baseNodeType  get=0x0A96A080
  requiresAgent  get=0x02FFF600
  requiresPrimeNode  get=0x02FFF600
  isTree  get=0x02FFF600
  allowBlackboardOverrides  get=0x02FFF600
  canAcceptVariableDrops  get=0x012081B0
  canDelayDeserializeAndOptimizeClone  get=0x02FFF600
EVENTS:
  onRootStatusChanged  add=add_onRootStatusChanged  remove=remove_onRootStatusChanged
METHODS:
  RVA=0x0A969E58  token=0x6000F80  System.Object OnDerivedDataSerialization()
  RVA=0x048C05F0  token=0x6000F81  System.Void OnDerivedDataDeserialization(System.Object data)
  RVA=0x04B2D2E0  token=0x6000F8D  System.Void OnGraphStarted()
  RVA=0x0A969EB8  token=0x6000F8E  System.Void OnGraphUpdate()
  RVA=0x0A969F60  token=0x6000F8F  NodeCanvas.Framework.Status Tick(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x03CBE920  token=0x6000F90  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.BehaviourTreeOwner
TYPE:  class
TOKEN: 0x20003B6
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.GraphOwner`1
FIELDS:
PROPERTIES:
  repeat  get=0x0A969B54  set=0x0A969D18
  updateInterval  get=0x0A969C80  set=0x0A969DB8
  rootStatus  get=0x0A969BE8
METHODS:
  RVA=0x0A969AB4  token=0x6000F97  NodeCanvas.Framework.Status Tick()
  RVA=0x04D11AD0  token=0x6000F98  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.BTConnection
TYPE:  class
TOKEN: 0x20003B7
SIZE:  0x30
EXTENDS: NodeCanvas.Framework.Connection
FIELDS:
METHODS:
  RVA=0x04D92320  token=0x6000F99  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.BTNode
TYPE:  abstract class
TOKEN: 0x20003B8
SIZE:  0x98
EXTENDS: NodeCanvas.Framework.Node
FIELDS:
PROPERTIES:
  outConnectionType  get=0x0A969A64
  allowAsPrime  get=0x02FFF600
  canSelfConnect  get=0x012081B0
  commentsAlignment  get=0x04D882B0
  iconAlignment  get=0x011EC580
  maxInConnections  get=0x04D85B80
  maxOutConnections  get=0x011EC580
METHODS:
  RVA=-1  // generic def  token=0x6000FA1  T AddChild(System.Int32 childIndex)
  RVA=-1  // generic def  token=0x6000FA2  T AddChild()
  RVA=0x0489BC70  token=0x6000FA3  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.BTNodeNested`1
TYPE:  abstract class
TOKEN: 0x20003B9
EXTENDS: NodeCanvas.BehaviourTrees.BTNode
IMPLEMENTS: NodeCanvas.Framework.IGraphAssignable`1 NodeCanvas.Framework.IGraphAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           System.Collections.Generic.List<NodeCanvas.Framework.Internal.BBMappingParameter>_variablesMap  // 0x0
  private           T                               <currentInstance>k__BackingField  // 0x0
  private           System.Collections.Generic.Dictionary<NodeCanvas.Framework.Graph,NodeCanvas.Framework.Graph><instances>k__BackingField  // 0x0
PROPERTIES:
  subGraph  get=-1  // abstract  set=-1  // abstract
  subGraphParameter  get=-1  // abstract
  currentInstance  get=-1  // not resolved  set=-1  // not resolved
  instances  get=-1  // not resolved  set=-1  // not resolved
  variablesMap  get=-1  // not resolved  set=-1  // not resolved
  NodeCanvas.Framework.IGraphAssignable.subGraph  get=-1  // not resolved  set=-1  // not resolved
  NodeCanvas.Framework.IGraphAssignable.currentInstance  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000FB1  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.BTComposite
TYPE:  abstract class
TOKEN: 0x20003BA
SIZE:  0x98
EXTENDS: NodeCanvas.BehaviourTrees.BTNode
FIELDS:
PROPERTIES:
  name  get=0x0A968FC0
  maxOutConnections  get=0x04DA2BB0
  commentsAlignment  get=0x02BBE540
METHODS:
  RVA=0x0489BC70  token=0x6000FB5  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.BTDecorator
TYPE:  abstract class
TOKEN: 0x20003BB
SIZE:  0x98
EXTENDS: NodeCanvas.BehaviourTrees.BTNode
FIELDS:
PROPERTIES:
  maxOutConnections  get=0x04D85B80
  commentsAlignment  get=0x02BBE540
  decoratedConnection  get=0x0A969788
  decoratedNode  get=0x0A9697C0
METHODS:
  RVA=0x0489BC70  token=0x6000FBA  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.BinarySelector
TYPE:  class
TOKEN: 0x20003BC
SIZE:  0xB0
EXTENDS: NodeCanvas.BehaviourTrees.BTNode
IMPLEMENTS: NodeCanvas.Framework.ITaskAssignable`1 NodeCanvas.Framework.ITaskAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
  public            System.Boolean                  dynamic  // 0x98
  private           NodeCanvas.Framework.ConditionTask_condition  // 0xa0
  private           System.Int32                    succeedIndex  // 0xa8
PROPERTIES:
  maxOutConnections  get=0x02BBE540
  commentsAlignment  get=0x02BBE540
  name  get=0x0A968FC0
  task  get=0x04D86210  set=0x0A96A29C
  condition  get=0x04D86210  set=0x05391334
METHODS:
  RVA=0x0A96A16C  token=0x6000FC2  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A96A280  token=0x6000FC3  System.Void OnReset()
  RVA=0x0489BC70  token=0x6000FC4  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.FlipSelector
TYPE:  class
TOKEN: 0x20003BD
SIZE:  0xA0
EXTENDS: NodeCanvas.BehaviourTrees.BTComposite
FIELDS:
  private           System.Int32                    current  // 0x98
METHODS:
  RVA=0x0A96FF18  token=0x6000FC5  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A96FFF8  token=0x6000FC6  System.Void SendToBack(System.Int32 i)
  RVA=0x04DA67A0  token=0x6000FC7  System.Void OnReset()
  RVA=0x0489BC70  token=0x6000FC8  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.Parallel
TYPE:  class
TOKEN: 0x20003BE
SIZE:  0xB0
EXTENDS: NodeCanvas.BehaviourTrees.BTComposite
FIELDS:
  public            NodeCanvas.BehaviourTrees.Parallel.ParallelPolicypolicy  // 0x98
  public            System.Boolean                  dynamic  // 0x9c
  private           System.Boolean[]                finishedConnections  // 0xa0
  private           System.Int32                    finishedConnectionsCount  // 0xa8
METHODS:
  RVA=0x0A978768  token=0x6000FC9  System.Void OnGraphStarted()
  RVA=0x0A978584  token=0x6000FCA  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A9787D8  token=0x6000FCB  System.Void OnReset()
  RVA=0x0A97881C  token=0x6000FCC  System.Void ResetRunning()
  RVA=0x0489BC70  token=0x6000FCD  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.PrioritySelector_0
TYPE:  class
TOKEN: 0x20003C0
SIZE:  0xA0
EXTENDS: NodeCanvas.BehaviourTrees.BTComposite
FIELDS:
  public            System.Collections.Generic.List<NodeCanvas.Framework.BBParameter<System.Single>>priorities  // 0x98
METHODS:
  RVA=0x0489BC70  token=0x6000FCE  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.PrioritySelector
TYPE:  class
TOKEN: 0x20003C1
SIZE:  0xB0
EXTENDS: NodeCanvas.BehaviourTrees.BTComposite
IMPLEMENTS: ParadoxNotion.Serialization.FullSerializer.IMigratable`1 ParadoxNotion.Serialization.FullSerializer.IMigratable
FIELDS:
  public            System.Collections.Generic.List<NodeCanvas.BehaviourTrees.PrioritySelector.Desire>desires  // 0x98
  private           NodeCanvas.Framework.Connection[]orderedConnections  // 0xa0
  private           System.Int32                    current  // 0xa8
METHODS:
  RVA=0x0A978B9C  token=0x6000FCF  System.Void ParadoxNotion.Serialization.FullSerializer.IMigratable<NodeCanvas.BehaviourTrees.PrioritySelector_0>.Migrate(NodeCanvas.BehaviourTrees.PrioritySelector_0 model)
  RVA=0x0A9788BC  token=0x6000FD0  System.Void OnChildConnected(System.Int32 index)
  RVA=0x0A9789D8  token=0x6000FD1  System.Void OnChildDisconnected(System.Int32 index)
  RVA=0x0A978A30  token=0x6000FD2  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x04DA67C0  token=0x6000FD3  System.Void OnReset()
  RVA=0x0489BC70  token=0x6000FD4  System.Void .ctor()
  RVA=0x0A978DF4  token=0x6000FD5  System.Single <OnExecute>b__8_0(NodeCanvas.Framework.Connection c)
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.ProbabilitySelector
TYPE:  class
TOKEN: 0x20003C4
SIZE:  0xC8
EXTENDS: NodeCanvas.BehaviourTrees.BTComposite
FIELDS:
  public            System.Collections.Generic.List<NodeCanvas.Framework.BBParameter<System.Single>>childWeights  // 0x98
  public            NodeCanvas.Framework.BBParameter<System.Single>failChance  // 0xa0
  private           System.Boolean[]                indexFailed  // 0xa8
  private           System.Single[]                 tmpWeights  // 0xb0
  private           System.Single                   tmpFailWeight  // 0xb8
  private           System.Single                   tmpTotal  // 0xbc
  private           System.Single                   tmpDice  // 0xc0
METHODS:
  RVA=0x0A979054  token=0x6000FDC  System.Void OnChildConnected(System.Int32 index)
  RVA=0x0A979214  token=0x6000FDD  System.Void OnChildDisconnected(System.Int32 index)
  RVA=0x04CC8C70  token=0x6000FDE  System.Void OnGraphStarted()
  RVA=0x0A97926C  token=0x6000FDF  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A979720  token=0x6000FE0  System.Void OnReset()
  RVA=0x0489BC70  token=0x6000FE1  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.Selector
TYPE:  class
TOKEN: 0x20003C5
SIZE:  0xA0
EXTENDS: NodeCanvas.BehaviourTrees.BTComposite
FIELDS:
  public            System.Boolean                  dynamic  // 0x98
  public            System.Boolean                  random  // 0x99
  private           System.Int32                    lastRunningNodeIndex  // 0x9c
METHODS:
  RVA=0x0A979B48  token=0x6000FE2  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x04B6CCE0  token=0x6000FE3  System.Void OnReset()
  RVA=0x0A979B34  token=0x6000FE4  System.Void OnChildDisconnected(System.Int32 index)
  RVA=0x04CC8C70  token=0x6000FE5  System.Void OnGraphStarted()
  RVA=0x0489BC70  token=0x6000FE6  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.Sequencer
TYPE:  class
TOKEN: 0x20003C6
SIZE:  0xA0
EXTENDS: NodeCanvas.BehaviourTrees.BTComposite
FIELDS:
  public            System.Boolean                  dynamic  // 0x98
  public            System.Boolean                  random  // 0x99
  private           System.Int32                    lastRunningNodeIndex  // 0x9c
METHODS:
  RVA=0x0A979CA0  token=0x6000FE7  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x04B6CCE0  token=0x6000FE8  System.Void OnReset()
  RVA=0x0A979B34  token=0x6000FE9  System.Void OnChildDisconnected(System.Int32 index)
  RVA=0x04CC8C70  token=0x6000FEA  System.Void OnGraphStarted()
  RVA=0x0489BC70  token=0x6000FEB  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.StepIterator
TYPE:  class
TOKEN: 0x20003C7
SIZE:  0xA0
EXTENDS: NodeCanvas.BehaviourTrees.BTComposite
FIELDS:
  private           System.Int32                    current  // 0x98
METHODS:
  RVA=0x04DA67A0  token=0x6000FEC  System.Void OnGraphStarted()
  RVA=0x0A97A760  token=0x6000FED  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x04DA67E0  token=0x6000FEE  System.Void OnReset()
  RVA=0x0489BC70  token=0x6000FEF  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.Switch
TYPE:  class
TOKEN: 0x20003C8
SIZE:  0xC8
EXTENDS: NodeCanvas.BehaviourTrees.BTComposite
FIELDS:
  public            System.Boolean                  dynamic  // 0x98
  public            NodeCanvas.BehaviourTrees.Switch.CaseSelectionModeselectionMode  // 0x9c
  public            NodeCanvas.Framework.BBParameter<System.Int32>intCase  // 0xa0
  public            NodeCanvas.BehaviourTrees.Switch.OutOfRangeModeoutOfRangeMode  // 0xa8
  public            NodeCanvas.Framework.Internal.BBObjectParameterenumCase  // 0xb0
  private           System.Collections.Generic.Dictionary<System.Int32,System.Int32>enumCasePairing  // 0xb8
  private           System.Int32                    current  // 0xc0
  private           System.Int32                    runningIndex  // 0xc4
METHODS:
  RVA=0x0A97B4D8  token=0x6000FF0  System.Void OnGraphStarted()
  RVA=0x0A97B2A8  token=0x6000FF1  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A97B65C  token=0x6000FF2  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.ConditionalEvaluator
TYPE:  class
TOKEN: 0x20003CB
SIZE:  0xB0
EXTENDS: NodeCanvas.BehaviourTrees.BTDecorator
IMPLEMENTS: NodeCanvas.Framework.ITaskAssignable`1 NodeCanvas.Framework.ITaskAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
  public            System.Boolean                  isDynamic  // 0x98
  public            ParadoxNotion.CompactStatus     conditionFailReturn  // 0x9c
  private           NodeCanvas.Framework.ConditionTask_condition  // 0xa0
  private           System.Boolean                  accessed  // 0xa8
PROPERTIES:
  task  get=0x04D86210  set=0x0A96AED4
  condition  get=0x04D86210  set=0x05391334
METHODS:
  RVA=0x0A96AD94  token=0x6000FF7  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A96AE9C  token=0x6000FF8  System.Void OnReset()
  RVA=0x0489BC70  token=0x6000FF9  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.Filter
TYPE:  class
TOKEN: 0x20003CC
SIZE:  0xC8
EXTENDS: NodeCanvas.BehaviourTrees.BTDecorator
FIELDS:
  public            NodeCanvas.BehaviourTrees.Filter.FilterModefilterMode  // 0x98
  public            NodeCanvas.Framework.BBParameter<System.Int32>maxCount  // 0xa0
  public            NodeCanvas.BehaviourTrees.Filter.Policypolicy  // 0xa8
  public            NodeCanvas.Framework.BBParameter<System.Single>coolDownTime  // 0xb0
  public            System.Boolean                  inactiveWhenLimited  // 0xb8
  private           System.Int32                    executedCount  // 0xbc
  private           System.Single                   currentTime  // 0xc0
  private           System.Single                   targetTime  // 0xc4
METHODS:
  RVA=0x041E1670  token=0x6000FFA  System.Void OnGraphStoped()
  RVA=0x0A96FCC8  token=0x6000FFB  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x04DA6790  token=0x6000FFC  System.Void OnClearNodeData()
  RVA=0x0A96FC6C  token=0x6000FFD  System.Collections.IEnumerator Cooldown()
  RVA=0x0489BD50  token=0x6000FFE  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.Guard
TYPE:  class
TOKEN: 0x20003D0
SIZE:  0xA8
EXTENDS: NodeCanvas.BehaviourTrees.BTDecorator
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>token  // 0x98
  public            NodeCanvas.BehaviourTrees.Guard.GuardModeifGuarded  // 0xa0
  private           System.Boolean                  isGuarding  // 0xa4
  private   static readonly System.Collections.Generic.Dictionary<UnityEngine.GameObject,System.Collections.Generic.List<NodeCanvas.BehaviourTrees.Guard>>guards  // static @ 0x0
METHODS:
  RVA=0x0A974E04  token=0x6001005  System.Collections.Generic.List<NodeCanvas.BehaviourTrees.Guard> AgentGuards(UnityEngine.Component agent)
  RVA=0x0A975090  token=0x6001006  System.Void OnGraphStarted()
  RVA=0x0A9750B4  token=0x6001007  System.Void OnGraphStoped()
  RVA=0x0A974E8C  token=0x6001008  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x04DA67B0  token=0x6001009  System.Void OnReset()
  RVA=0x0A975360  token=0x600100A  System.Void SetGuards(UnityEngine.Component guardAgent)
  RVA=0x0489BC70  token=0x600100B  System.Void .ctor()
  RVA=0x0A975554  token=0x600100C  System.Void .cctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.Interruptor
TYPE:  class
TOKEN: 0x20003D2
SIZE:  0xA0
EXTENDS: NodeCanvas.BehaviourTrees.BTDecorator
IMPLEMENTS: NodeCanvas.Framework.ITaskAssignable`1 NodeCanvas.Framework.ITaskAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           NodeCanvas.Framework.ConditionTask_condition  // 0x98
PROPERTIES:
  condition  get=0x04D85EE0  set=0x06352D0C
  task  get=0x04D85EE0  set=0x0A9756C4
METHODS:
  RVA=0x0A9755E0  token=0x6001011  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A96ACC8  token=0x6001012  System.Void OnReset()
  RVA=0x0489BC70  token=0x6001013  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.Inverter
TYPE:  class
TOKEN: 0x20003D3
SIZE:  0x98
EXTENDS: NodeCanvas.BehaviourTrees.BTDecorator
FIELDS:
METHODS:
  RVA=0x0A97571C  token=0x6001014  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0489BC70  token=0x6001015  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.Iterator
TYPE:  class
TOKEN: 0x20003D4
SIZE:  0xC8
EXTENDS: NodeCanvas.BehaviourTrees.BTDecorator
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.IList>targetList  // 0x98
  public            NodeCanvas.Framework.Internal.BBObjectParametercurrent  // 0xa0
  public            NodeCanvas.Framework.BBParameter<System.Int32>storeIndex  // 0xa8
  public            NodeCanvas.BehaviourTrees.Iterator.TerminationConditionsterminationCondition  // 0xb0
  public            NodeCanvas.Framework.BBParameter<System.Int32>maxIteration  // 0xb8
  public            System.Boolean                  resetIndex  // 0xc0
  private           System.Int32                    currentIndex  // 0xc4
PROPERTIES:
  list  get=0x0A975C34
METHODS:
  RVA=0x0A975968  token=0x6001017  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A975BC0  token=0x6001018  System.Void OnReset()
  RVA=0x0A975BD4  token=0x6001019  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.Merge
TYPE:  class
TOKEN: 0x20003D6
SIZE:  0x98
EXTENDS: NodeCanvas.BehaviourTrees.BTDecorator
FIELDS:
PROPERTIES:
  maxInConnections  get=0x04DA2BB0
METHODS:
  RVA=0x0A975D60  token=0x600101B  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0489BC70  token=0x600101C  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.Monitor
TYPE:  class
TOKEN: 0x20003D7
SIZE:  0xB0
EXTENDS: NodeCanvas.BehaviourTrees.BTDecorator
IMPLEMENTS: NodeCanvas.Framework.ITaskAssignable`1 NodeCanvas.Framework.ITaskAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
  public            NodeCanvas.BehaviourTrees.Monitor.MonitorModemonitorMode  // 0x98
  public            NodeCanvas.BehaviourTrees.Monitor.ReturnStatusModereturnMode  // 0x9c
  private           NodeCanvas.Framework.Status     decoratorActionStatus  // 0xa0
  private           NodeCanvas.Framework.ActionTask _action  // 0xa8
PROPERTIES:
  action  get=0x04D86290  set=0x04D84CC0
  task  get=0x04D86290  set=0x0A975F00
METHODS:
  RVA=0x0A975DCC  token=0x6001021  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A975ECC  token=0x6001022  System.Void OnReset()
  RVA=0x0489BC70  token=0x6001023  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.Optional
TYPE:  class
TOKEN: 0x20003DA
SIZE:  0x98
EXTENDS: NodeCanvas.BehaviourTrees.BTDecorator
FIELDS:
METHODS:
  RVA=0x0A9784EC  token=0x6001024  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0489BC70  token=0x6001025  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.Remapper
TYPE:  class
TOKEN: 0x20003DB
SIZE:  0xA0
EXTENDS: NodeCanvas.BehaviourTrees.BTDecorator
FIELDS:
  public            NodeCanvas.BehaviourTrees.Remapper.RemapStatussuccessRemap  // 0x98
  public            NodeCanvas.BehaviourTrees.Remapper.RemapStatusfailureRemap  // 0x9c
METHODS:
  RVA=0x0A979848  token=0x6001026  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0489BE70  token=0x6001027  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.Repeater
TYPE:  class
TOKEN: 0x20003DD
SIZE:  0xB0
EXTENDS: NodeCanvas.BehaviourTrees.BTDecorator
FIELDS:
  public            NodeCanvas.BehaviourTrees.Repeater.RepeaterModerepeaterMode  // 0x98
  public            NodeCanvas.Framework.BBParameter<System.Int32>repeatTimes  // 0xa0
  public            NodeCanvas.BehaviourTrees.Repeater.RepeatUntilStatusrepeatUntilStatus  // 0xa8
  private           System.Int32                    currentIteration  // 0xac
METHODS:
  RVA=0x0A9798D8  token=0x6001028  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x04DA67D0  token=0x6001029  System.Void OnReset()
  RVA=0x0A9799FC  token=0x600102A  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.Setter
TYPE:  class
TOKEN: 0x20003E0
SIZE:  0xA8
EXTENDS: NodeCanvas.BehaviourTrees.BTDecorator
FIELDS:
  public            System.Boolean                  revertToOriginal  // 0x98
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>newAgent  // 0xa0
METHODS:
  RVA=0x0A979DF8  token=0x600102B  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0489BC70  token=0x600102C  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.Timeout
TYPE:  class
TOKEN: 0x20003E1
SIZE:  0xA0
EXTENDS: NodeCanvas.BehaviourTrees.BTDecorator
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Single>timeout  // 0x98
METHODS:
  RVA=0x0A97B720  token=0x600102D  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A97B810  token=0x600102E  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.WaitUntil
TYPE:  class
TOKEN: 0x20003E2
SIZE:  0xA8
EXTENDS: NodeCanvas.BehaviourTrees.BTDecorator
IMPLEMENTS: NodeCanvas.Framework.ITaskAssignable`1 NodeCanvas.Framework.ITaskAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           NodeCanvas.Framework.ConditionTask_condition  // 0x98
  private           System.Boolean                  accessed  // 0xa0
PROPERTIES:
  task  get=0x04D85EE0  set=0x0A97CB20
  condition  get=0x04D85EE0  set=0x06352D0C
METHODS:
  RVA=0x0A97C9EC  token=0x6001033  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A97CAE8  token=0x6001034  System.Void OnReset()
  RVA=0x0489BC70  token=0x6001035  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.ActionNode
TYPE:  class
TOKEN: 0x20003E3
SIZE:  0xA0
EXTENDS: NodeCanvas.BehaviourTrees.BTNode
IMPLEMENTS: NodeCanvas.Framework.ITaskAssignable`1 NodeCanvas.Framework.ITaskAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           NodeCanvas.Framework.ActionTask _action  // 0x98
PROPERTIES:
  task  get=0x04D85EE0  set=0x0A968FE4
  action  get=0x04D85EE0  set=0x06352D0C
  name  get=0x0A968FC0
METHODS:
  RVA=0x0A968E1C  token=0x600103B  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A968F30  token=0x600103C  System.Void OnReset()
  RVA=0x0A968EF8  token=0x600103D  System.Void OnGraphPaused()
  RVA=0x0489BC70  token=0x600103E  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.ConditionNode
TYPE:  class
TOKEN: 0x20003E4
SIZE:  0xA0
EXTENDS: NodeCanvas.BehaviourTrees.BTNode
IMPLEMENTS: NodeCanvas.Framework.ITaskAssignable`1 NodeCanvas.Framework.ITaskAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           NodeCanvas.Framework.ConditionTask_condition  // 0x98
PROPERTIES:
  task  get=0x04D85EE0  set=0x0A96ACE4
  condition  get=0x04D85EE0  set=0x06352D0C
  name  get=0x0A968FC0
METHODS:
  RVA=0x0A96AB38  token=0x6001044  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A96ACC8  token=0x6001045  System.Void OnReset()
  RVA=0x0489BC70  token=0x6001046  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.NestedDT
TYPE:  class
TOKEN: 0x20003E5
SIZE:  0xB8
EXTENDS: NodeCanvas.BehaviourTrees.BTNodeNested`1
FIELDS:
  private           NodeCanvas.Framework.BBParameter<NodeCanvas.DialogueTrees.DialogueTree>_nestedDialogueTree  // 0xb0
PROPERTIES:
  subGraph  get=0x0A977124  set=0x0A97716C
  subGraphParameter  get=0x04D86250
METHODS:
  RVA=0x0A976F28  token=0x600104A  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A9698FC  token=0x600104B  System.Void OnDLGFinished(System.Boolean success)
  RVA=0x0A977070  token=0x600104C  System.Void OnReset()
  RVA=0x0A9770F0  token=0x600104D  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.NestedFSM
TYPE:  class
TOKEN: 0x20003E6
SIZE:  0xC8
EXTENDS: NodeCanvas.BehaviourTrees.BTNodeNested`1
FIELDS:
  private           NodeCanvas.Framework.BBParameter<NodeCanvas.StateMachines.FSM>_nestedFSM  // 0xb0
  public            System.String                   successState  // 0xb8
  public            System.String                   failureState  // 0xc0
PROPERTIES:
  subGraph  get=0x0A97770C  set=0x0A977754
  subGraphParameter  get=0x04D86250
METHODS:
  RVA=0x0A97747C  token=0x6001051  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A9698FC  token=0x6001052  System.Void OnFSMFinish(System.Boolean success)
  RVA=0x0A977658  token=0x6001053  System.Void OnReset()
  RVA=0x0A9776D8  token=0x6001054  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.SubTree
TYPE:  class
TOKEN: 0x20003E7
SIZE:  0xB8
EXTENDS: NodeCanvas.BehaviourTrees.BTNodeNested`1
FIELDS:
  private           NodeCanvas.Framework.BBParameter<NodeCanvas.BehaviourTrees.BehaviourTree>_subTree  // 0xb0
PROPERTIES:
  subGraph  get=0x04CD94E0  set=0x04D19410
  subGraphParameter  get=0x04D86250
METHODS:
  RVA=0x0A97AD90  token=0x6001058  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A97AE64  token=0x6001059  System.Void OnReset()
  RVA=0x04D50FB0  token=0x600105A  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.NodeToggler
TYPE:  class
TOKEN: 0x20003E8
SIZE:  0xB0
EXTENDS: NodeCanvas.BehaviourTrees.BTNode
FIELDS:
  public            NodeCanvas.BehaviourTrees.NodeToggler.ToggleModetoggleMode  // 0x98
  public            System.String                   targetNodeTag  // 0xa0
  private           System.Collections.Generic.List<NodeCanvas.Framework.Node>targetNodes  // 0xa8
METHODS:
  RVA=0x0A977BD0  token=0x600105B  System.Void OnGraphStarted()
  RVA=0x0A9777AC  token=0x600105C  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A977C4C  token=0x600105D  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.RootSwitcher
TYPE:  class
TOKEN: 0x20003EA
SIZE:  0xA8
EXTENDS: NodeCanvas.BehaviourTrees.BTNode
FIELDS:
  public            System.String                   targetNodeTag  // 0x98
  private           NodeCanvas.Framework.Node       targetNode  // 0xa0
METHODS:
  RVA=0x0A979AD4  token=0x600105E  System.Void OnGraphStarted()
  RVA=0x0A979A68  token=0x600105F  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0489BC70  token=0x6001060  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.BehaviourTrees.BTNestedFlowScript
TYPE:  class
TOKEN: 0x20003EB
SIZE:  0xB8
EXTENDS: NodeCanvas.BehaviourTrees.BTNodeNested`1
FIELDS:
  private           NodeCanvas.Framework.BBParameter<FlowCanvas.FlowScript>_flowScript  // 0xb0
PROPERTIES:
  subGraph  get=0x0A9699C4  set=0x0A969A0C
  subGraphParameter  get=0x04D86250
METHODS:
  RVA=0x0A9697DC  token=0x6001064  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A9698FC  token=0x6001065  System.Void OnFlowScriptFinished(System.Boolean success)
  RVA=0x0A969910  token=0x6001066  System.Void OnReset()
  RVA=0x0A969990  token=0x6001067  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.DialogueTrees.DialogueActor
TYPE:  class
TOKEN: 0x2000387
SIZE:  0x50
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: NodeCanvas.DialogueTrees.IDialogueActor
FIELDS:
  protected         System.String                   _name  // 0x18
  protected         UnityEngine.Texture2D           _portrait  // 0x20
  protected         UnityEngine.Color               _dialogueColor  // 0x28
  protected         UnityEngine.Vector3             _dialogueOffset  // 0x38
  private           UnityEngine.Sprite              _portraitSprite  // 0x48
PROPERTIES:
  name  get=0x0385B100
  portrait  get=0x04D862C0
  portraitSprite  get=0x0A96C44C
  dialogueColor  get=0x04D8FAD0
  dialoguePosition  get=0x0A96C3E4
METHODS:
  RVA=0x0A90E334  token=0x6000E69  System.Void .ctor()
  RVA=0x053953F4  token=0x6000E6A  UnityEngine.Transform NodeCanvas.DialogueTrees.IDialogueActor.get_transform()
END_CLASS

CLASS: NodeCanvas.DialogueTrees.SubtitlesRequestInfo
TYPE:  class
TOKEN: 0x2000388
SIZE:  0x28
FIELDS:
  public            NodeCanvas.DialogueTrees.IDialogueActoractor  // 0x10
  public            NodeCanvas.DialogueTrees.IStatementstatement  // 0x18
  public            System.Action                   Continue  // 0x20
METHODS:
  RVA=0x031D14F0  token=0x6000E6B  System.Void .ctor(NodeCanvas.DialogueTrees.IDialogueActor actor, NodeCanvas.DialogueTrees.IStatement statement, System.Action callback)
END_CLASS

CLASS: NodeCanvas.DialogueTrees.MultipleChoiceRequestInfo
TYPE:  class
TOKEN: 0x2000389
SIZE:  0x30
FIELDS:
  public            NodeCanvas.DialogueTrees.IDialogueActoractor  // 0x10
  public            System.Collections.Generic.Dictionary<NodeCanvas.DialogueTrees.IStatement,System.Int32>options  // 0x18
  public            System.Single                   availableTime  // 0x20
  public            System.Boolean                  showLastStatement  // 0x24
  public            System.Action<System.Int32>     SelectOption  // 0x28
METHODS:
  RVA=0x0A9764E0  token=0x6000E6C  System.Void .ctor(NodeCanvas.DialogueTrees.IDialogueActor actor, System.Collections.Generic.Dictionary<NodeCanvas.DialogueTrees.IStatement,System.Int32> options, System.Single availableTime, System.Boolean showLastStatement, System.Action<System.Int32> callback)
  RVA=0x0A9764A0  token=0x6000E6D  System.Void .ctor(NodeCanvas.DialogueTrees.IDialogueActor actor, System.Collections.Generic.Dictionary<NodeCanvas.DialogueTrees.IStatement,System.Int32> options, System.Single availableTime, System.Action<System.Int32> callback)
END_CLASS

CLASS: NodeCanvas.DialogueTrees.DialogueTree
TYPE:  class
TOKEN: 0x200038A
SIZE:  0xB0
EXTENDS: NodeCanvas.Framework.Graph
FIELDS:
  public    static  System.String                   INSTIGATOR_NAME  // const
  public            System.Collections.Generic.List<NodeCanvas.DialogueTrees.DialogueTree.ActorParameter>actorParameters  // 0xa0
  private   static  System.Action<NodeCanvas.DialogueTrees.DialogueTree>OnDialogueStarted  // static @ 0x0
  private   static  System.Action<NodeCanvas.DialogueTrees.DialogueTree>OnDialoguePaused  // static @ 0x8
  private   static  System.Action<NodeCanvas.DialogueTrees.DialogueTree>OnDialogueFinished  // static @ 0x10
  private   static  System.Action<NodeCanvas.DialogueTrees.SubtitlesRequestInfo>OnSubtitlesRequest  // static @ 0x18
  private   static  System.Action<NodeCanvas.DialogueTrees.MultipleChoiceRequestInfo>OnMultipleChoiceRequest  // static @ 0x20
  private   static  NodeCanvas.DialogueTrees.DialogueTree<currentDialogue>k__BackingField  // static @ 0x28
  private   static  NodeCanvas.DialogueTrees.DialogueTree<previousDialogue>k__BackingField  // static @ 0x30
  private           NodeCanvas.DialogueTrees.DTNode <currentNode>k__BackingField  // 0xa8
PROPERTIES:
  currentDialogue  get=0x0A96DB28  set=0x0A96E02C
  previousDialogue  get=0x0A96DCC8  set=0x0A96E080
  currentNode  get=0x04D86290  set=0x04D84CC0
  baseNodeType  get=0x0A96DAD8
  requiresAgent  get=0x012081B0
  requiresPrimeNode  get=0x02FFF600
  isTree  get=0x02FFF600
  allowBlackboardOverrides  get=0x02FFF600
  canAcceptVariableDrops  get=0x012081B0
  canDelayDeserializeAndOptimizeClone  get=0x012081B0
  definedActorParameterNames  get=0x0A96DB60
EVENTS:
  OnDialogueStarted  add=add_OnDialogueStarted  remove=remove_OnDialogueStarted
  OnDialoguePaused  add=add_OnDialoguePaused  remove=remove_OnDialoguePaused
  OnDialogueFinished  add=add_OnDialogueFinished  remove=remove_OnDialogueFinished
  OnSubtitlesRequest  add=add_OnSubtitlesRequest  remove=remove_OnSubtitlesRequest
  OnMultipleChoiceRequest  add=add_OnMultipleChoiceRequest  remove=remove_OnMultipleChoiceRequest
METHODS:
  RVA=0x0A96CFD0  token=0x6000E6E  System.Object OnDerivedDataSerialization()
  RVA=0x0A96CF44  token=0x6000E6F  System.Void OnDerivedDataDeserialization(System.Object data)
  RVA=0x0A96CD9C  token=0x6000E88  NodeCanvas.DialogueTrees.DialogueTree.ActorParameter GetParameterByID(System.String id)
  RVA=0x0A96CE70  token=0x6000E89  NodeCanvas.DialogueTrees.DialogueTree.ActorParameter GetParameterByName(System.String paramName)
  RVA=0x0A96CB08  token=0x6000E8A  NodeCanvas.DialogueTrees.IDialogueActor GetActorReferenceByID(System.String id)
  RVA=0x0A96CB38  token=0x6000E8B  NodeCanvas.DialogueTrees.IDialogueActor GetActorReferenceByName(System.String paramName)
  RVA=0x0A96D3D4  token=0x6000E8C  System.Void SetActorReference(System.String paramName, NodeCanvas.DialogueTrees.IDialogueActor actor)
  RVA=0x0A96D4C8  token=0x6000E8D  System.Void SetActorReferences(System.Collections.Generic.Dictionary<System.String,NodeCanvas.DialogueTrees.IDialogueActor> actors)
  RVA=0x0A96C980  token=0x6000E8E  System.Void Continue(System.Int32 index)
  RVA=0x0A96CA74  token=0x6000E8F  System.Void EnterNode(NodeCanvas.DialogueTrees.DTNode node)
  RVA=0x0A96D384  token=0x6000E90  System.Void RequestSubtitles(NodeCanvas.DialogueTrees.SubtitlesRequestInfo info)
  RVA=0x0A96D334  token=0x6000E91  System.Void RequestMultipleChoices(NodeCanvas.DialogueTrees.MultipleChoiceRequestInfo info)
  RVA=0x0A96D07C  token=0x6000E92  System.Void OnGraphStarted()
  RVA=0x0A96D2B8  token=0x6000E93  System.Void OnGraphUpdate()
  RVA=0x0A96D16C  token=0x6000E94  System.Void OnGraphStoped()
  RVA=0x0A96D02C  token=0x6000E95  System.Void OnGraphPaused()
  RVA=0x0A96D20C  token=0x6000E96  System.Void OnGraphUnpaused()
  RVA=0x0A96D70C  token=0x6000E97  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.DialogueTrees.DialogueTreeController
TYPE:  class
TOKEN: 0x2000393
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.GraphOwner`1
IMPLEMENTS: NodeCanvas.DialogueTrees.IDialogueActor
FIELDS:
PROPERTIES:
  NodeCanvas.DialogueTrees.IDialogueActor.name  get=0x05396F58
  NodeCanvas.DialogueTrees.IDialogueActor.portrait  get=0x011EC580
  NodeCanvas.DialogueTrees.IDialogueActor.portraitSprite  get=0x011EC580
  NodeCanvas.DialogueTrees.IDialogueActor.dialogueColor  get=0x0A96C614
  NodeCanvas.DialogueTrees.IDialogueActor.dialoguePosition  get=0x09D954F8
  NodeCanvas.DialogueTrees.IDialogueActor.transform  get=0x053953F4
METHODS:
  RVA=0x0A96C7B8  token=0x6000EB5  System.Void StartDialogue()
  RVA=0x0A96C900  token=0x6000EB6  System.Void StartDialogue(System.Action<System.Boolean> callback)
  RVA=0x0A96C910  token=0x6000EB7  System.Void StartDialogue(NodeCanvas.DialogueTrees.IDialogueActor instigator)
  RVA=0x0A96C7C8  token=0x6000EB8  System.Void StartDialogue(NodeCanvas.DialogueTrees.DialogueTree newTree, NodeCanvas.DialogueTrees.IDialogueActor instigator, System.Action<System.Boolean> callback)
  RVA=0x0A96C810  token=0x6000EB9  System.Void StartDialogue(NodeCanvas.DialogueTrees.IDialogueActor instigator, System.Action<System.Boolean> callback)
  RVA=0x0A96C634  token=0x6000EBA  System.Void PauseDialogue()
  RVA=0x0A96C91C  token=0x6000EBB  System.Void StopDialogue()
  RVA=0x0A96C660  token=0x6000EBC  System.Void SetActorReference(System.String paramName, NodeCanvas.DialogueTrees.IDialogueActor actor)
  RVA=0x0A96C714  token=0x6000EBD  System.Void SetActorReferences(System.Collections.Generic.Dictionary<System.String,NodeCanvas.DialogueTrees.IDialogueActor> actors)
  RVA=0x0A96C56C  token=0x6000EBE  NodeCanvas.DialogueTrees.IDialogueActor GetActorReferenceByName(System.String paramName)
  RVA=0x0A96C94C  token=0x6000EBF  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.DialogueTrees.DTConnection
TYPE:  class
TOKEN: 0x2000394
SIZE:  0x30
EXTENDS: NodeCanvas.Framework.Connection
FIELDS:
METHODS:
  RVA=0x04D92320  token=0x6000EC0  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.DialogueTrees.DTNode
TYPE:  abstract class
TOKEN: 0x2000395
SIZE:  0xA8
EXTENDS: NodeCanvas.Framework.Node
FIELDS:
  private           System.String                   _actorName  // 0x98
  private           System.String                   _actorParameterID  // 0xa0
PROPERTIES:
  name  get=0x0A96C020
  requireActorSelection  get=0x02FFF600
  maxInConnections  get=0x04DA2BB0
  maxOutConnections  get=0x04D85B80
  outConnectionType  get=0x0A96C0F8
  allowAsPrime  get=0x02FFF600
  canSelfConnect  get=0x012081B0
  commentsAlignment  get=0x02BBE540
  iconAlignment  get=0x04D882B0
  DLGTree  get=0x0A96BF40
  actorName  get=0x0A96BF7C  set=0x0A96C148
  finalActor  get=0x0A96BFC4
METHODS:
  RVA=0x0A96BEF0  token=0x6000ECE  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.DialogueTrees.DTNodeNested`1
TYPE:  abstract class
TOKEN: 0x2000396
EXTENDS: NodeCanvas.DialogueTrees.DTNode
IMPLEMENTS: NodeCanvas.Framework.IGraphAssignable`1 NodeCanvas.Framework.IGraphAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           System.Collections.Generic.List<NodeCanvas.Framework.Internal.BBMappingParameter>_variablesMap  // 0x0
  private           T                               <currentInstance>k__BackingField  // 0x0
  private           System.Collections.Generic.Dictionary<NodeCanvas.Framework.Graph,NodeCanvas.Framework.Graph><instances>k__BackingField  // 0x0
PROPERTIES:
  subGraph  get=-1  // abstract  set=-1  // abstract
  subGraphParameter  get=-1  // abstract
  currentInstance  get=-1  // not resolved  set=-1  // not resolved
  instances  get=-1  // not resolved  set=-1  // not resolved
  variablesMap  get=-1  // not resolved  set=-1  // not resolved
  NodeCanvas.Framework.IGraphAssignable.subGraph  get=-1  // not resolved  set=-1  // not resolved
  NodeCanvas.Framework.IGraphAssignable.currentInstance  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000EDC  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.DialogueTrees.IDialogueActor
TYPE:  interface
TOKEN: 0x2000397
FIELDS:
PROPERTIES:
  name  get=-1  // abstract
  portrait  get=-1  // abstract
  portraitSprite  get=-1  // abstract
  dialogueColor  get=-1  // abstract
  dialoguePosition  get=-1  // abstract
  transform  get=-1  // abstract
METHODS:
END_CLASS

CLASS: NodeCanvas.DialogueTrees.ProxyDialogueActor
TYPE:  class
TOKEN: 0x2000398
SIZE:  0x20
IMPLEMENTS: NodeCanvas.DialogueTrees.IDialogueActor
FIELDS:
  private           System.String                   _name  // 0x10
  private           UnityEngine.Transform           _transform  // 0x18
PROPERTIES:
  name  get=0x02B2ECC0
  portrait  get=0x011EC580
  portraitSprite  get=0x011EC580
  dialogueColor  get=0x0A96C614
  dialoguePosition  get=0x09D954F8
  transform  get=0x0385B100
METHODS:
  RVA=0x032AF420  token=0x6000EE9  System.Void .ctor(System.String name, UnityEngine.Transform transform)
END_CLASS

CLASS: NodeCanvas.DialogueTrees.IStatement
TYPE:  interface
TOKEN: 0x2000399
FIELDS:
PROPERTIES:
  text  get=-1  // abstract
  audio  get=-1  // abstract
  meta  get=-1  // abstract
METHODS:
END_CLASS

CLASS: NodeCanvas.DialogueTrees.Statement
TYPE:  class
TOKEN: 0x200039A
SIZE:  0x28
IMPLEMENTS: NodeCanvas.DialogueTrees.IStatement
FIELDS:
  private           System.String                   _text  // 0x10
  private           UnityEngine.AudioClip           _audio  // 0x18
  private           System.String                   _meta  // 0x20
PROPERTIES:
  text  get=0x02B2ECC0  set=0x053908C0
  audio  get=0x0385B100  set=0x05392C40
  meta  get=0x04D862C0  set=0x05390F40
METHODS:
  RVA=0x0A97A6F8  token=0x6000EF3  System.Void .ctor()
  RVA=0x0A97A528  token=0x6000EF4  System.Void .ctor(System.String text)
  RVA=0x0A97A65C  token=0x6000EF5  System.Void .ctor(System.String text, UnityEngine.AudioClip audio)
  RVA=0x0A97A5A8  token=0x6000EF6  System.Void .ctor(System.String text, UnityEngine.AudioClip audio, System.String meta)
  RVA=0x0A97A3F0  token=0x6000EF7  NodeCanvas.DialogueTrees.IStatement BlackboardReplace(NodeCanvas.Framework.IBlackboard bb)
  RVA=0x02B2ECC0  token=0x6000EF8  System.String ToString()
END_CLASS

CLASS: NodeCanvas.DialogueTrees.ActionNode
TYPE:  class
TOKEN: 0x200039C
SIZE:  0xB0
EXTENDS: NodeCanvas.DialogueTrees.DTNode
IMPLEMENTS: NodeCanvas.Framework.ITaskAssignable`1 NodeCanvas.Framework.ITaskAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           NodeCanvas.Framework.ActionTask _action  // 0xa8
PROPERTIES:
  action  get=0x04D86290  set=0x04D84CC0
  task  get=0x04D86290  set=0x0A96903C
  requireActorSelection  get=0x02FFF600
METHODS:
  RVA=0x0A968E4C  token=0x6000F00  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard bb)
  RVA=0x0A968F4C  token=0x6000F01  System.Collections.IEnumerator UpdateAction(UnityEngine.Component actionAgent)
  RVA=0x0A968DB0  token=0x6000F02  System.Void OnActionEnd(System.Boolean success)
  RVA=0x0A968F14  token=0x6000F03  System.Void OnReset()
  RVA=0x0A968EDC  token=0x6000F04  System.Void OnGraphPaused()
  RVA=0x09876A0C  token=0x6000F05  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.DialogueTrees.ConditionNode
TYPE:  class
TOKEN: 0x200039E
SIZE:  0xB0
EXTENDS: NodeCanvas.DialogueTrees.DTNode
IMPLEMENTS: NodeCanvas.Framework.ITaskAssignable`1 NodeCanvas.Framework.ITaskAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           NodeCanvas.Framework.ConditionTask_condition  // 0xa8
PROPERTIES:
  condition  get=0x04D86290  set=0x04D84CC0
  task  get=0x04D86290  set=0x0A96AD3C
  maxOutConnections  get=0x02BBE540
  requireActorSelection  get=0x02FFF600
METHODS:
  RVA=0x0A96ABBC  token=0x6000F12  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard bb)
  RVA=0x09876A0C  token=0x6000F13  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.DialogueTrees.FinishNode
TYPE:  class
TOKEN: 0x200039F
SIZE:  0xB0
EXTENDS: NodeCanvas.DialogueTrees.DTNode
FIELDS:
  public            ParadoxNotion.CompactStatus     finishState  // 0xa8
PROPERTIES:
  maxOutConnections  get=0x011EC580
  requireActorSelection  get=0x012081B0
METHODS:
  RVA=0x0A96FEB8  token=0x6000F16  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard bb)
  RVA=0x0A96FF04  token=0x6000F17  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.DialogueTrees.Jumper
TYPE:  class
TOKEN: 0x20003A0
SIZE:  0xB0
EXTENDS: NodeCanvas.DialogueTrees.DTNode
IMPLEMENTS: NodeCanvas.Framework.IHaveNodeReference NodeCanvas.Framework.IGraphElement
FIELDS:
  public            NodeCanvas.Framework.NodeReference<NodeCanvas.DialogueTrees.DTNode>_targetNode  // 0xa8
PROPERTIES:
  NodeCanvas.Framework.IHaveNodeReference.targetReference  get=0x04D86290
  target  get=0x0A975D10
  maxOutConnections  get=0x011EC580
  requireActorSelection  get=0x012081B0
METHODS:
  RVA=0x0A975C84  token=0x6000F1C  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard bb)
  RVA=0x09876A0C  token=0x6000F1D  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.DialogueTrees.MultipleChoiceNode
TYPE:  class
TOKEN: 0x20003A1
SIZE:  0xB8
EXTENDS: NodeCanvas.DialogueTrees.DTNode
FIELDS:
  public            System.Single                   availableTime  // 0xa8
  public            System.Boolean                  saySelection  // 0xac
  private           System.Collections.Generic.List<NodeCanvas.DialogueTrees.MultipleChoiceNode.Choice>availableChoices  // 0xb0
PROPERTIES:
  maxOutConnections  get=0x0A976460
  requireActorSelection  get=0x02FFF600
METHODS:
  RVA=0x0A975F58  token=0x6000F20  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard bb)
  RVA=0x0A976258  token=0x6000F21  System.Void OnOptionSelected(System.Int32 index)
  RVA=0x0A9763D8  token=0x6000F22  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.DialogueTrees.MultipleConditionNode
TYPE:  class
TOKEN: 0x20003A4
SIZE:  0xB0
EXTENDS: NodeCanvas.DialogueTrees.DTNode
FIELDS:
  private           System.Collections.Generic.List<NodeCanvas.Framework.ConditionTask>conditions  // 0xa8
PROPERTIES:
  maxOutConnections  get=0x04DA2BB0
METHODS:
  RVA=0x0A976528  token=0x6000F28  System.Void OnChildConnected(System.Int32 index)
  RVA=0x0A9765B4  token=0x6000F29  System.Void OnChildDisconnected(System.Int32 index)
  RVA=0x0A97660C  token=0x6000F2A  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard bb)
  RVA=0x0A976788  token=0x6000F2B  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.DialogueTrees.ProbabilitySelector
TYPE:  class
TOKEN: 0x20003A5
SIZE:  0xB8
EXTENDS: NodeCanvas.DialogueTrees.DTNode
FIELDS:
  private           System.Collections.Generic.List<NodeCanvas.DialogueTrees.ProbabilitySelector.Option>childOptions  // 0xa8
  private           System.Collections.Generic.List<System.Int32>successIndeces  // 0xb0
PROPERTIES:
  maxOutConnections  get=0x04DA2BB0
METHODS:
  RVA=0x0A978F70  token=0x6000F2D  System.Void OnChildConnected(System.Int32 index)
  RVA=0x0A9791BC  token=0x6000F2E  System.Void OnChildDisconnected(System.Int32 index)
  RVA=0x0A9794A8  token=0x6000F2F  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A978E8C  token=0x6000F30  System.Single GetTotal()
  RVA=0x0A93C6F0  token=0x6000F31  System.Void OnReset()
  RVA=0x0A9797C0  token=0x6000F32  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.DialogueTrees.StatementNode
TYPE:  class
TOKEN: 0x20003A7
SIZE:  0xB0
EXTENDS: NodeCanvas.DialogueTrees.DTNode
FIELDS:
  public            NodeCanvas.DialogueTrees.Statementstatement  // 0xa8
PROPERTIES:
  requireActorSelection  get=0x02FFF600
METHODS:
  RVA=0x0A97A230  token=0x6000F35  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard bb)
  RVA=0x0A97A328  token=0x6000F36  System.Void OnStatementFinish()
  RVA=0x0A97A368  token=0x6000F37  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.DialogueTrees.SubDialogueTree
TYPE:  class
TOKEN: 0x20003A8
SIZE:  0xD0
EXTENDS: NodeCanvas.DialogueTrees.DTNodeNested`1
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           NodeCanvas.Framework.BBParameter<NodeCanvas.DialogueTrees.DialogueTree>_subTree  // 0xc0
  private           System.Collections.Generic.Dictionary<System.String,System.String>_actorParametersMap  // 0xc8
PROPERTIES:
  maxOutConnections  get=0x02BBE540
  subGraph  get=0x0A97ACF0  set=0x0A97AD38
  subGraphParameter  get=0x04D892F0
METHODS:
  RVA=0x0A97A88C  token=0x6000F3C  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard bb)
  RVA=0x0A97AA78  token=0x6000F3D  System.Void OnSubDialogueFinish(System.Boolean success)
  RVA=0x0A97A7F8  token=0x6000F3E  System.Void NodeCanvas.Framework.IUpdatable.Update()
  RVA=0x0A97AAD0  token=0x6000F3F  System.Void TryWriteMappedActorParameters()
  RVA=0x0A97ACBC  token=0x6000F40  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.DialogueTrees.GoToNode
TYPE:  class
TOKEN: 0x20003A9
SIZE:  0xB0
EXTENDS: NodeCanvas.DialogueTrees.DTNode
FIELDS:
  private           NodeCanvas.DialogueTrees.DTNode _targetNode  // 0xa8
PROPERTIES:
  maxOutConnections  get=0x011EC580
  requireActorSelection  get=0x012081B0
METHODS:
  RVA=0x0A9703AC  token=0x6000F43  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard bb)
  RVA=0x09876A0C  token=0x6000F44  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.DialogueTrees.DTNestedFlowScript
TYPE:  class
TOKEN: 0x20003AA
SIZE:  0xC8
EXTENDS: NodeCanvas.DialogueTrees.DTNodeNested`1
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           NodeCanvas.Framework.BBParameter<FlowCanvas.FlowScript>_flowScript  // 0xc0
PROPERTIES:
  maxOutConnections  get=0x02BBE540
  subGraph  get=0x0A96BE50  set=0x0A96BE98
  subGraphParameter  get=0x04D892F0
METHODS:
  RVA=0x0A96BCC0  token=0x6000F49  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard bb)
  RVA=0x0A96BC2C  token=0x6000F4A  System.Void NodeCanvas.Framework.IUpdatable.Update()
  RVA=0x0A96BDCC  token=0x6000F4B  System.Void OnFlowScriptFinish(System.Boolean success)
  RVA=0x0A96BE1C  token=0x6000F4C  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.DialogueTrees.UI.Examples.DialogueUGUI
TYPE:  class
TOKEN: 0x20003AB
SIZE:  0x90
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Boolean                  skipOnInput  // 0x18
  public            System.Boolean                  waitForInput  // 0x19
  public            UnityEngine.RectTransform       subtitlesGroup  // 0x20
  public            UnityEngine.UI.Text             actorSpeech  // 0x28
  public            UnityEngine.UI.Text             actorName  // 0x30
  public            UnityEngine.UI.Image            actorPortrait  // 0x38
  public            UnityEngine.RectTransform       waitInputIndicator  // 0x40
  public            NodeCanvas.DialogueTrees.UI.Examples.DialogueUGUI.SubtitleDelayssubtitleDelays  // 0x48
  public            System.Collections.Generic.List<UnityEngine.AudioClip>typingSounds  // 0x50
  private           UnityEngine.AudioSource         playSource  // 0x58
  public            UnityEngine.RectTransform       optionsGroup  // 0x60
  public            UnityEngine.UI.Button           optionButton  // 0x68
  private           System.Collections.Generic.Dictionary<UnityEngine.UI.Button,System.Int32>cachedButtons  // 0x70
  private           UnityEngine.Vector2             originalSubsPosition  // 0x78
  private           System.Boolean                  isWaitingChoice  // 0x80
  private           UnityEngine.AudioSource         _localSource  // 0x88
PROPERTIES:
  localSource  get=0x0A96F7C8
  anyKeyDown  get=0x0A96F7C0
METHODS:
  RVA=0x0A96E0D4  token=0x6000F4F  System.Void Awake()
  RVA=0x0A96EA08  token=0x6000F50  System.Void OnEnable()
  RVA=0x0A96EA00  token=0x6000F51  System.Void OnDisable()
  RVA=0x0A96F41C  token=0x6000F52  System.Void Subscribe()
  RVA=0x0A96F5B4  token=0x6000F53  System.Void UnSubscribe()
  RVA=0x0A96E51C  token=0x6000F54  System.Void Hide()
  RVA=0x041E1670  token=0x6000F55  System.Void OnDialogueStarted(NodeCanvas.DialogueTrees.DialogueTree dlg)
  RVA=0x0A96E940  token=0x6000F56  System.Void OnDialoguePaused(NodeCanvas.DialogueTrees.DialogueTree dlg)
  RVA=0x0A96E6A0  token=0x6000F57  System.Void OnDialogueFinished(NodeCanvas.DialogueTrees.DialogueTree dlg)
  RVA=0x0A96F27C  token=0x6000F58  System.Void OnSubtitlesRequest(NodeCanvas.DialogueTrees.SubtitlesRequestInfo info)
  RVA=0x0A96E62C  token=0x6000F59  System.Collections.IEnumerator Internal_OnSubtitlesRequestInfo(NodeCanvas.DialogueTrees.SubtitlesRequestInfo info)
  RVA=0x0A96F2A0  token=0x6000F5A  System.Void PlayTypeSound()
  RVA=0x0A96E0F4  token=0x6000F5B  System.Collections.IEnumerator CheckInput(System.Action Do)
  RVA=0x0A96E1DC  token=0x6000F5C  System.Collections.IEnumerator DelayPrint(System.Single time)
  RVA=0x0A96EA28  token=0x6000F5D  System.Void OnMultipleChoiceRequest(NodeCanvas.DialogueTrees.MultipleChoiceRequestInfo info)
  RVA=0x0A96E168  token=0x6000F5E  System.Collections.IEnumerator CountDown(NodeCanvas.DialogueTrees.MultipleChoiceRequestInfo info)
  RVA=0x0A96E230  token=0x6000F5F  System.Void Finalize(NodeCanvas.DialogueTrees.MultipleChoiceRequestInfo info, System.Int32 index)
  RVA=0x0A96F38C  token=0x6000F60  System.Void SetMassAlpha(UnityEngine.RectTransform root, System.Single alpha)
  RVA=0x0A96F74C  token=0x6000F61  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.GraphInfoAttribute
TYPE:  class
TOKEN: 0x20003EC
SIZE:  0x30
EXTENDS: System.Attribute
FIELDS:
  public            System.String                   packageName  // 0x10
  public            System.String                   docsURL  // 0x18
  public            System.String                   resourcesURL  // 0x20
  public            System.String                   forumsURL  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6001068  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.DropReferenceType
TYPE:  class
TOKEN: 0x20003ED
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.Type                     type  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6001069  System.Void .ctor(System.Type type)
END_CLASS

CLASS: NodeCanvas.Framework.BlackboardOnlyAttribute
TYPE:  class
TOKEN: 0x20003EE
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600106A  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.LogTag
TYPE:  static class
TOKEN: 0x20003EF
SIZE:  0x10
FIELDS:
  public    static  System.String                   EDITOR  // const
  public    static  System.String                   EVENT  // const
  public    static  System.String                   EXECUTION  // const
  public    static  System.String                   VARIABLE  // const
  public    static  System.String                   BLACKBOARD  // const
  public    static  System.String                   GRAPH  // const
  public    static  System.String                   INIT  // const
  public    static  System.String                   VALIDATION  // const
  public    static  System.String                   SERIALIZATION  // const
METHODS:
END_CLASS

CLASS: NodeCanvas.Framework.SignalDefinition
TYPE:  class
TOKEN: 0x20003F0
SIZE:  0x28
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  private           NodeCanvas.Framework.SignalDefinition.InvokeArgumentsonInvoke  // 0x18
  private           System.Collections.Generic.List<ParadoxNotion.DynamicParameterDefinition>_parameters  // 0x20
PROPERTIES:
  parameters  get=0x04D862C0  set=0x05390F40
EVENTS:
  onInvoke  add=add_onInvoke  remove=remove_onInvoke
METHODS:
  RVA=0x0A979F50  token=0x600106F  System.Void Invoke(UnityEngine.Transform sender, UnityEngine.Transform receiver, System.Boolean isGlobal, System.Object[] args)
  RVA=0x0A979EB4  token=0x6001070  System.Void AddParameter(System.String name, System.Type type)
  RVA=0x0A979F80  token=0x6001071  System.Void RemoveParameter(System.String name)
  RVA=0x0A97A088  token=0x6001072  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.CanvasGroup
TYPE:  class
TOKEN: 0x20003F3
SIZE:  0x40
FIELDS:
  public            System.String                   name  // 0x10
  public            UnityEngine.Rect                rect  // 0x18
  public            UnityEngine.Color               color  // 0x28
  public            System.Boolean                  autoGroup  // 0x38
METHODS:
  RVA=0x041E1670  token=0x6001079  System.Void .ctor()
  RVA=0x0A96A2F4  token=0x600107A  System.Void .ctor(UnityEngine.Rect rect, System.String name)
END_CLASS

CLASS: NodeCanvas.Framework.Connection
TYPE:  abstract class
TOKEN: 0x20003F4
SIZE:  0x30
IMPLEMENTS: NodeCanvas.Framework.IGraphElement ParadoxNotion.Serialization.FullSerializer.ISerializationCollectable
FIELDS:
  private           NodeCanvas.Framework.Node       _sourceNode  // 0x10
  private           NodeCanvas.Framework.Node       _targetNode  // 0x18
  private           System.String                   _UID  // 0x20
  private           System.Boolean                  _isDisabled  // 0x28
  private           NodeCanvas.Framework.Status     _status  // 0x2c
PROPERTIES:
  UID  get=0x0A96B9DC
  sourceNode  get=0x02B2ECC0  set=0x053908C0
  targetNode  get=0x0385B100  set=0x05392C40
  NodeCanvas.Framework.IGraphElement.name  get=0x0A96B318
  isActive  get=0x0A96BA4C  set=0x0A96BA54
  status  get=0x04D86710  set=0x04D86720
  graph  get=0x0A96BA38
  drawInspector  get=0x02FFF600
METHODS:
  RVA=0x04D92320  token=0x6001087  System.Void .ctor()
  RVA=0x0A96AF2C  token=0x6001088  NodeCanvas.Framework.Connection Create(NodeCanvas.Framework.Node source, NodeCanvas.Framework.Node target, System.Int32 sourceIndex, System.Int32 targetIndex)
  RVA=0x0A96B01C  token=0x6001089  NodeCanvas.Framework.Connection Duplicate(NodeCanvas.Framework.Node newSource, NodeCanvas.Framework.Node newTarget)
  RVA=0x0A96B56C  token=0x600108A  System.Int32 SetSourceNode(NodeCanvas.Framework.Node newSource, System.Int32 index, System.Boolean recodeUndo)
  RVA=0x0A96B70C  token=0x600108B  System.Int32 SetTargetNode(NodeCanvas.Framework.Node newTarget, System.Int32 index, System.Boolean recodeUndo)
  RVA=0x0A96B37C  token=0x600108C  System.Int32 SetRawSourceNode(NodeCanvas.Framework.Node newSource)
  RVA=0x0A96B474  token=0x600108D  System.Int32 SetRawTargetNode(NodeCanvas.Framework.Node newTarget)
  RVA=0x0A96B2E0  token=0x600108E  NodeCanvas.Framework.Status Execute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A96B344  token=0x600108F  System.Void Reset(System.Boolean recursively)
  RVA=0x041E1670  token=0x6001090  System.Void OnCreate(System.Int32 sourceIndex, System.Int32 targetIndex)
  RVA=0x041E1670  token=0x6001091  System.Void OnValidate(System.Int32 sourceIndex, System.Int32 targetIndex)
  RVA=0x041E1670  token=0x6001092  System.Void OnDestroy()
  RVA=0x041E1670  token=0x6001093  System.Void OnDestroyAfterConnectionRemoved()
  RVA=0x0A96B98C  token=0x6001094  System.String ToString()
END_CLASS

CLASS: NodeCanvas.Framework.ExposedParameter
TYPE:  abstract class
TOKEN: 0x20003F5
SIZE:  0x10
FIELDS:
PROPERTIES:
  targetVariableID  get=-1  // abstract
  type  get=-1  // abstract
  valueBoxed  get=-1  // abstract  set=-1  // abstract
  varRefBoxed  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6001099  System.Void Bind(NodeCanvas.Framework.IBlackboard blackboard)
  RVA=-1  // abstract  token=0x600109A  System.Void UnBind()
  RVA=0x0A96FB3C  token=0x600109C  NodeCanvas.Framework.ExposedParameter CreateInstance(NodeCanvas.Framework.Variable target)
  RVA=0x041E1670  token=0x600109D  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.ExposedParameter`1
TYPE:  sealed class
TOKEN: 0x20003F6
EXTENDS: NodeCanvas.Framework.ExposedParameter
FIELDS:
  private           System.String                   _targetVariableID  // 0x0
  private           T                               _value  // 0x0
  private           NodeCanvas.Framework.Variable<T><varRef>k__BackingField  // 0x0
PROPERTIES:
  varRef  get=-1  // not resolved  set=-1  // not resolved
  targetVariableID  get=-1  // not resolved
  type  get=-1  // not resolved
  valueBoxed  get=-1  // not resolved  set=-1  // not resolved
  varRefBoxed  get=-1  // not resolved
  value  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60010A0  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60010A1  System.Void .ctor(NodeCanvas.Framework.Variable target)
  RVA=-1  // not resolved  token=0x60010A9  System.Void Bind(NodeCanvas.Framework.IBlackboard blackboard)
  RVA=-1  // not resolved  token=0x60010AA  System.Void UnBind()
  RVA=-1  // not resolved  token=0x60010AB  T GetRawValue()
  RVA=-1  // not resolved  token=0x60010AC  System.Void SetRawValue(T value)
END_CLASS

CLASS: NodeCanvas.Framework.Graph
TYPE:  abstract class
TOKEN: 0x20003F7
SIZE:  0xA0
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: NodeCanvas.Framework.ITaskSystem UnityEngine.ISerializationCallbackReceiver Beyond.IStringCompressProcessor
FIELDS:
  private           System.String                   _serializedGraph  // 0x18
  protected         System.Int32                    _serializedGraphStringIndex  // 0x20
  private           System.Boolean                  _enableGraphStringCompress  // 0x24
  private           System.Collections.Generic.List<UnityEngine.Object>_objectReferences  // 0x28
  private           NodeCanvas.Framework.Internal.GraphSource_graphSource  // 0x30
  private           System.Boolean                  _haltSerialization  // 0x38
  private           System.Boolean                  haltForUndo  // 0x39
  private   static  System.Action<NodeCanvas.Framework.Graph>onGraphSerialized  // static @ 0x0
  public    static  System.Action<NodeCanvas.Framework.Graph>onGraphDeserialized  // static @ 0x8
  private           System.Boolean                  <isDeserialized>k__BackingField  // 0x3a
  private   static  System.Collections.Generic.List<NodeCanvas.Framework.Node>s_nodeBuffer  // static @ 0x10
  private           System.Boolean                  <graphValidateDirty>k__BackingField  // 0x3b
  private           System.Action<System.Boolean>   onFinish  // 0x40
  private   static  System.Collections.Generic.List<NodeCanvas.Framework.Graph>_runningGraphs  // static @ 0x18
  private           System.Boolean                  <hasInitialized>k__BackingField  // 0x48
  private           ParadoxNotion.HierarchyTree.Element<flatMetaGraph>k__BackingField  // 0x50
  private           ParadoxNotion.HierarchyTree.Element<fullMetaGraph>k__BackingField  // 0x58
  private           ParadoxNotion.HierarchyTree.Element<nestedMetaGraph>k__BackingField  // 0x60
  private           NodeCanvas.Framework.Graph      <parentGraph>k__BackingField  // 0x68
  private           System.Single                   <elapsedTime>k__BackingField  // 0x70
  private           System.Single                   <deltaTime>k__BackingField  // 0x74
  private           System.Int32                    <lastUpdateFrame>k__BackingField  // 0x78
  private           System.Boolean                  <isRunning>k__BackingField  // 0x7c
  private           System.Boolean                  <isPaused>k__BackingField  // 0x7d
  private           NodeCanvas.Framework.Graph.UpdateMode<updateMode>k__BackingField  // 0x80
  private           UnityEngine.Component           <agent>k__BackingField  // 0x88
  private           NodeCanvas.Framework.IBlackboard<parentBlackboard>k__BackingField  // 0x90
  private           System.Action                   delayedInitCalls  // 0x98
PROPERTIES:
  allowRuntimeSerialization  get=0x02FFF600
  dontSerializeBinderConnectionGenericType  get=0x012081B0
  isDeserialized  get=0x04D86660  set=0x04D86690
  canDelayDeserializeAndOptimizeClone  get=-1  // abstract
  graphValidateDirty  get=0x04D86D10  set=0x04D86D20
  hasInitialized  get=0x04D867D0  set=0x04D86810
  flatMetaGraph  get=0x04D86270  set=0x02FDAEA0
  fullMetaGraph  get=0x02E56440  set=0x036E5590
  nestedMetaGraph  get=0x04D86280  set=0x063CC164
  baseNodeType  get=-1  // abstract
  requiresAgent  get=-1  // abstract
  requiresPrimeNode  get=-1  // abstract
  isTree  get=-1  // abstract
  allowBlackboardOverrides  get=-1  // abstract
  canAcceptVariableDrops  get=-1  // abstract
  graphSource  get=0x0306F4B0  set=0x05396200
  category  get=0x0A974814  set=0x0A974BC8
  comments  get=0x0A974834  set=0x0A974BF8
  translation  get=0x0A974930  set=0x0A974D9C
  zoomFactor  get=0x0A974960  set=0x0A974DD4
  allNodes  get=0x0306F380  set=0x0A974B68
  canvasGroups  get=0x0A9747F4  set=0x0A974B98
  localBlackboard  get=0x0306F3A0  set=0x0A974C28
  allParameters  get=0x0306F490
  allTasks  get=0x0306F360
  allConnections  get=0x0A9747D4
  rootGraph  get=0x0A974874
  serializationHalted  get=0x011F2A70
  runningGraphs  get=0x0A9748E0
  parentGraph  get=0x04D86230  set=0x0485F170
  elapsedTime  get=0x04D90610  set=0x04D90600
  deltaTime  get=0x04DA60B0  set=0x04DA60C0
  lastUpdateFrame  get=0x04D863C0  set=0x04D86440
  didUpdateLastFrame  get=0x0A974854
  isRunning  get=0x04D92E50  set=0x04D92E60
  isPaused  get=0x04DA26A0  set=0x04DA26B0
  updateMode  get=0x04D863A0  set=0x04D86420
  primeNode  get=0x0306D980  set=0x0A974C58
  agent  get=0x04D861F0  set=0x05395D30
  blackboard  get=0x05390F2C
  parentBlackboard  get=0x04D86220  set=0x06402330
  NodeCanvas.Framework.ITaskSystem.contextObject  get=0x0464E630
EVENTS:
  onGraphSerialized  add=add_onGraphSerialized  remove=remove_onGraphSerialized
  onFinish  add=add_onFinish  remove=remove_onFinish
  delayedInitCalls  add=add_delayedInitCalls  remove=remove_delayedInitCalls
METHODS:
  RVA=0x04D94BD0  token=0x60010B4  UnityEngine.Rect DrawCustomBlackboardPanel(UnityEngine.Event e, UnityEngine.Rect canvasRect, UnityEngine.Vector2 canvasMousePos)
  RVA=0x04D94BD0  token=0x60010B5  UnityEngine.Rect DrawCustomPanel(UnityEngine.Event e, UnityEngine.Rect canvasRect, UnityEngine.Vector2 canvasMousePos)
  RVA=0x04D94BD0  token=0x60010B6  UnityEngine.Rect DrawCustomPanel2(UnityEngine.Event e, UnityEngine.Rect canvasRect, UnityEngine.Vector2 canvasMousePos)
  RVA=0x0A974120  token=0x60010B7  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x03113CD0  token=0x60010B8  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x0306F4D0  token=0x60010B9  System.Void TryDeserializeIfNot()
  RVA=0x04D1ABF0  token=0x60010BA  System.Void OnEnable()
  RVA=0x041E1670  token=0x60010BB  System.Void OnDisable()
  RVA=0x0A972A6C  token=0x60010BC  System.Void OnDestroy()
  RVA=0x041E1670  token=0x60010BD  System.Void OnValidate()
  RVA=0x0A973704  token=0x60010BE  System.Void Reset()
  RVA=0x0A9739EC  token=0x60010BF  System.Boolean SelfSerialize(System.Boolean ignoreScene)
  RVA=0x041E1670  token=0x60010C0  System.Void OnAfterGraphSerialized()
  RVA=0x0A97394C  token=0x60010C1  System.Boolean SelfDeserialize()
  RVA=0x0A973F28  token=0x60010C2  System.String Serialize(System.Collections.Generic.List<UnityEngine.Object> references)
  RVA=0x03114AB0  token=0x60010C3  System.Boolean Deserialize(System.String serializedGraph, System.Collections.Generic.List<UnityEngine.Object> references, System.Boolean validate)
  RVA=0x03115400  token=0x60010C4  System.Boolean DeserializeSelf(System.Collections.Generic.List<UnityEngine.Object> references, System.Boolean validate)
  RVA=0x0A971F14  token=0x60010C5  NodeCanvas.Framework.Internal.GraphSource GetGraphSource()
  RVA=0x0A9724E0  token=0x60010C6  System.String GetSerializedJsonData()
  RVA=0x03115350  token=0x60010C7  System.Int32 GetSerializedJsonDataStringIndex()
  RVA=0x031151C0  token=0x60010C8  System.Boolean DeserializeFromGraph(NodeCanvas.Framework.Graph other, System.Boolean validate)
  RVA=0x0A972494  token=0x60010C9  Beyond.DataCompressManager.StringCompressUnsafeString GetSerializedJsonDataUnsafeString()
  RVA=0x05390C40  token=0x60010CA  System.Void ClearSerializedJsonData()
  RVA=0x03115300  token=0x60010CB  System.Collections.Generic.List<UnityEngine.Object> GetSerializedReferencesData()
  RVA=0x0A971EA4  token=0x60010CC  NodeCanvas.Framework.Internal.GraphSource GetGraphSourceMetaDataCopy()
  RVA=0x0A974044  token=0x60010CD  System.Void SetGraphSourceMetaData(NodeCanvas.Framework.Internal.GraphSource source)
  RVA=0x0A973E4C  token=0x60010CE  System.String SerializeLocalBlackboard(System.Collections.Generic.List<UnityEngine.Object>& references)
  RVA=0x0A971348  token=0x60010CF  System.Boolean DeserializeLocalBlackboard(System.String json, System.Collections.Generic.List<UnityEngine.Object> references)
  RVA=-1  // generic def  token=0x60010D0  T Clone(T graph, NodeCanvas.Framework.Graph parentGraph)
  RVA=0x02FC1CC0  token=0x60010D1  System.Void Validate()
  RVA=0x0A974550  token=0x6001111  System.Void UpdateReferencesFromOwner(NodeCanvas.Framework.GraphOwner owner, System.Boolean force)
  RVA=0x0306C5A0  token=0x6001112  System.Void UpdateReferences(UnityEngine.Component newAgent, NodeCanvas.Framework.IBlackboard newParentBlackboard, System.Boolean force)
  RVA=0x0306F3C0  token=0x6001113  System.Void UpdateNodeBBFields()
  RVA=0x0306F2C0  token=0x6001114  System.Void NodeCanvas.Framework.ITaskSystem.UpdateTasksOwner()
  RVA=0x0A9741C8  token=0x6001115  System.Void UpdateNodeIDs(System.Boolean alsoReorderList)
  RVA=0x041E1670  token=0x6001116  System.Void OnGraphStructureChange()
  RVA=0x041E1670  token=0x6001117  System.Void OnAfterUpdateNodeIDs()
  RVA=0x0A970594  token=0x6001118  System.Int32 AssignNodeID(NodeCanvas.Framework.Node node, System.Int32 lastID, NodeCanvas.Framework.Node[]& parsed)
  RVA=0x0A9740AC  token=0x600111B  System.Void ThreadSafeInitCall(System.Action call)
  RVA=0x0A97280C  token=0x600111C  System.Void LoadOverwriteAsync(NodeCanvas.Framework.Internal.GraphLoadData data, System.Action callback)
  RVA=0x0A9728EC  token=0x600111D  System.Void LoadOverwrite(NodeCanvas.Framework.Internal.GraphLoadData data)
  RVA=0x03113660  token=0x600111E  System.Void Initialize(UnityEngine.Component newAgent, NodeCanvas.Framework.IBlackboard newParentBlackboard, System.Boolean preInitializeSubGraphs)
  RVA=0x0A972C40  token=0x600111F  System.Void PreInitializeSubGraphs()
  RVA=0x0306EDB0  token=0x6001120  System.Void StartGraph(UnityEngine.Component newAgent, NodeCanvas.Framework.IBlackboard newParentBlackboard, NodeCanvas.Framework.Graph.UpdateMode newUpdateMode, System.Action<System.Boolean> callback)
  RVA=0x0306FD40  token=0x6001121  System.Void ClearGraphData()
  RVA=0x0306F100  token=0x6001122  System.Void Stop(System.Boolean success)
  RVA=0x0A972ABC  token=0x6001123  System.Void Pause()
  RVA=0x0A97376C  token=0x6001124  System.Void Resume()
  RVA=0x0A973714  token=0x6001125  System.Void Restart()
  RVA=0x0A974134  token=0x6001126  System.Void UpdateGraph()
  RVA=0x0A974158  token=0x6001127  System.Void UpdateGraph(System.Single deltaTime)
  RVA=0x011EC580  token=0x6001128  System.Object OnDerivedDataSerialization()
  RVA=0x041E1670  token=0x6001129  System.Void OnDerivedDataDeserialization(System.Object data)
  RVA=0x041E1670  token=0x600112A  System.Void OnGraphInitialize()
  RVA=0x041E1670  token=0x600112B  System.Void OnGraphStarted()
  RVA=0x041E1670  token=0x600112C  System.Void OnGraphUpdate()
  RVA=0x041E1670  token=0x600112D  System.Void OnGraphStoped()
  RVA=0x041E1670  token=0x600112E  System.Void OnGraphPaused()
  RVA=0x041E1670  token=0x600112F  System.Void OnGraphUnpaused()
  RVA=0x041E1670  token=0x6001130  System.Void OnGraphObjectEnable()
  RVA=0x041E1670  token=0x6001131  System.Void OnGraphObjectDisable()
  RVA=0x041E1670  token=0x6001132  System.Void OnGraphObjectDestroy()
  RVA=0x041E1670  token=0x6001133  System.Void OnGraphValidate()
  RVA=0x0A973B70  token=0x6001134  System.Void SendEvent(System.String name, System.Object value, System.Object sender)
  RVA=-1  // generic def  token=0x6001135  System.Void SendEvent(System.String name, T value, System.Object sender)
  RVA=0x0A973C5C  token=0x6001136  System.Void SendGlobalEvent(System.String name, System.Object value, System.Object sender)
  RVA=-1  // generic def  token=0x6001137  System.Void SendGlobalEvent(System.String name, T value, System.Object sender)
  RVA=0x0A9718C8  token=0x6001138  System.Collections.Generic.IEnumerable<NodeCanvas.Framework.BBParameter> GetAllParameters()
  RVA=0x0A971568  token=0x6001139  System.Collections.Generic.IEnumerable<NodeCanvas.Framework.Connection> GetAllConnections()
  RVA=-1  // generic def  token=0x600113A  System.Collections.Generic.IEnumerable<T> GetAllTasksOfType()
  RVA=0x0A9720E4  token=0x600113B  NodeCanvas.Framework.Node GetNodeWithID(System.Int32 searchID)
  RVA=-1  // generic def  token=0x600113C  System.Collections.Generic.IEnumerable<T> GetAllNodesOfType()
  RVA=-1  // generic def  token=0x600113D  T GetNodeWithTag(System.String tagName)
  RVA=-1  // generic def  token=0x600113E  System.Collections.Generic.IEnumerable<T> GetNodesWithTag(System.String tagName)
  RVA=-1  // generic def  token=0x600113F  System.Collections.Generic.IEnumerable<T> GetAllTagedNodes()
  RVA=0x0A972380  token=0x6001140  System.Collections.Generic.IEnumerable<NodeCanvas.Framework.Node> GetRootNodes()
  RVA=0x0A971F30  token=0x6001141  System.Collections.Generic.IEnumerable<NodeCanvas.Framework.Node> GetLeafNodes()
  RVA=-1  // generic def  token=0x6001142  System.Collections.Generic.IEnumerable<T> GetAllNestedGraphs(System.Boolean recursive)
  RVA=0x0A971570  token=0x6001143  System.Collections.Generic.IEnumerable<NodeCanvas.Framework.Graph> GetAllInstancedNestedGraphs()
  RVA=0x0A9718D0  token=0x6001144  System.Collections.Generic.IEnumerable<NodeCanvas.Framework.BBParameter> GetDefinedParameters()
  RVA=0x0A972FB4  token=0x6001145  System.Void PromoteMissingParametersToVariables(NodeCanvas.Framework.IBlackboard bb)
  RVA=0x0A9719E4  token=0x6001146  NodeCanvas.Framework.Graph GetElementGraph(System.Object obj)
  RVA=0x0A971BDC  token=0x6001147  ParadoxNotion.HierarchyTree.Element GetFlatMetaGraph()
  RVA=0x0A971D04  token=0x6001148  ParadoxNotion.HierarchyTree.Element GetFullMetaGraph()
  RVA=0x0A972044  token=0x6001149  ParadoxNotion.HierarchyTree.Element GetNestedMetaGraph()
  RVA=0x0A9713E0  token=0x600114A  System.Void DigNestedGraphs(NodeCanvas.Framework.Graph currentGraph, ParadoxNotion.HierarchyTree.Element currentElement)
  RVA=0x0A97269C  token=0x600114B  ParadoxNotion.HierarchyTree.Element GetTreeNodeElement(NodeCanvas.Framework.Node node, System.Boolean recurse, System.Int32& lastID)
  RVA=0x0A971130  token=0x600114C  ParadoxNotion.HierarchyTree.Element CollectSubElements(NodeCanvas.Framework.IGraphElement obj)
  RVA=0x0A9724F8  token=0x600114D  NodeCanvas.Framework.IGraphElement GetTaskParentElement(NodeCanvas.Framework.Task targetTask)
  RVA=0x0A9721DC  token=0x600114E  NodeCanvas.Framework.IGraphElement GetParameterParentElement(NodeCanvas.Framework.BBParameter targetParameter)
  RVA=0x0A972568  token=0x600114F  System.Collections.Generic.IEnumerable<NodeCanvas.Framework.Task> GetTasksInElement(NodeCanvas.Framework.IGraphElement target)
  RVA=0x0A97224C  token=0x6001150  System.Collections.Generic.IEnumerable<NodeCanvas.Framework.BBParameter> GetParametersInElement(NodeCanvas.Framework.IGraphElement target)
  RVA=-1  // generic def  token=0x6001151  T AddNode()
  RVA=-1  // generic def  token=0x6001152  T AddNode(UnityEngine.Vector2 pos)
  RVA=0x0A97051C  token=0x6001153  NodeCanvas.Framework.Node AddNode(System.Type nodeType)
  RVA=0x0A970424  token=0x6001154  NodeCanvas.Framework.Node AddNode(System.Type nodeType, UnityEngine.Vector2 pos)
  RVA=0x0A9732C0  token=0x6001155  System.Void RemoveNode(NodeCanvas.Framework.Node node, System.Boolean recordUndo, System.Boolean force)
  RVA=0x0A9712CC  token=0x6001156  NodeCanvas.Framework.Connection ConnectNodes(NodeCanvas.Framework.Node sourceNode, NodeCanvas.Framework.Node targetNode, System.Int32 sourceIndex, System.Int32 targetIndex)
  RVA=0x0A973108  token=0x6001157  System.Void RemoveConnection(NodeCanvas.Framework.Connection connection, System.Boolean recordUndo, System.Boolean invokeGraphChanged)
  RVA=0x0A970838  token=0x6001158  System.Collections.Generic.List<NodeCanvas.Framework.Node> CloneNodes(System.Collections.Generic.List<NodeCanvas.Framework.Node> originalNodes, NodeCanvas.Framework.Graph targetGraph, UnityEngine.Vector2 originPosition)
  RVA=0x0A970790  token=0x6001159  System.Void ClearGraph()
  RVA=0x0A974074  token=0x600115A  System.Void StartGraph(UnityEngine.Component newAgent, NodeCanvas.Framework.IBlackboard newBlackboard, System.Boolean autoUpdate, System.Action<System.Boolean> callback)
  RVA=0x0A972F54  token=0x600115B  System.Void ProcessStringCompress(Beyond.StringCompressVisitor visitor)
  RVA=0x0A9706AC  token=0x600115C  System.Boolean CheckStringCompress()
  RVA=0x03CBE980  token=0x600115D  System.Void .ctor()
  RVA=0x04D12810  token=0x600115E  System.Void .cctor()
END_CLASS

CLASS: NodeCanvas.Framework.GraphOwner
TYPE:  abstract class
TOKEN: 0x2000406
SIZE:  0x78
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver
FIELDS:
  protected         System.Boolean                  isLazyLoadTriggered  // 0x18
  private           System.Boolean                  m_deserializeFinish  // 0x19
  private           ParadoxNotion.Serialization.SerializationPair[]_serializedExposedParameters  // 0x20
  private           System.Collections.Generic.List<NodeCanvas.Framework.ExposedParameter><exposedParameters>k__BackingField  // 0x28
  public    static  System.Action<NodeCanvas.Framework.GraphOwner>onOwnerBehaviourStateChange  // static @ 0x0
  private           System.Action                   onMonoBehaviourStart  // 0x30
  private           System.String                   _boundGraphSerialization  // 0x38
  private           System.Collections.Generic.List<UnityEngine.Object>_boundGraphObjectReferences  // 0x40
  private           NodeCanvas.Framework.Internal.GraphSource_boundGraphSource  // 0x48
  private           NodeCanvas.Framework.GraphOwner.FirstActivation_firstActivation  // 0x50
  private           NodeCanvas.Framework.GraphOwner.EnableAction_enableAction  // 0x54
  private           NodeCanvas.Framework.GraphOwner.DisableAction_disableAction  // 0x58
  private           System.Boolean                  _lockBoundGraphPrefabOverrides  // 0x5c
  private           System.Boolean                  _lockBoundGraphFromInstance  // 0x5d
  private           System.Boolean                  _preInitializeSubGraphs  // 0x5e
  private           NodeCanvas.Framework.Graph.UpdateMode_updateMode  // 0x60
  protected         System.Collections.Generic.Dictionary<NodeCanvas.Framework.Graph,NodeCanvas.Framework.Graph>instances  // 0x68
  private           System.Boolean                  <initialized>k__BackingField  // 0x70
  private           System.Boolean                  <enableCalled>k__BackingField  // 0x71
  private           System.Boolean                  <startCalled>k__BackingField  // 0x72
PROPERTIES:
  exposedParameters  get=0x04D86240  set=0x02FDB880
  graph  get=-1  // abstract  set=-1  // abstract
  blackboard  get=-1  // abstract  set=-1  // abstract
  graphType  get=-1  // abstract
  initialized  get=0x04D86A50  set=0x04D86A80
  enableCalled  get=0x04D89040  set=0x04D89050
  startCalled  get=0x0157A7C0  set=0x0157A800
  boundGraphSource  get=0x04D86200  set=0x035A41A0
  boundGraphSerialization  get=0x04D85A50  set=0x046A91B0
  boundGraphObjectReferences  get=0x04D85A60  set=0x042B4AE0
  lockBoundGraphPrefabOverrides  get=0x0A981C64  set=0x04D86DB0
  lockBoundGraphFromInstance  get=0x04DA5B40  set=0x04DA5B80
  preInitializeSubGraphs  get=0x04DA5B60  set=0x04DA5BA0
  firstActivation  get=0x04D86550  set=0x04D865A0
  enableAction  get=0x04D86540  set=0x04D86590
  disableAction  get=0x04D86570  set=0x04D865D0
  updateMode  get=0x04D86790  set=0x04D86D50
  graphIsBound  get=0x04B88270
  isRunning  get=0x0306BA60
  isPaused  get=0x0A981C00
  elapsedTime  get=0x0A981B7C
  canEditNonPersistantBoundGraph  get=0x02FFF600
EVENTS:
  onMonoBehaviourStart  add=add_onMonoBehaviourStart  remove=remove_onMonoBehaviourStart
METHODS:
  RVA=0x0A9818C0  token=0x6001181  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x0A98176C  token=0x6001182  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x041E1670  token=0x6001183  System.Void OnBeforeSerializeDoExtra()
  RVA=0x041E1670  token=0x6001184  System.Void OnAfterDeserializeDoExtra()
  RVA=0x04B30270  token=0x60011AB  NodeCanvas.Framework.Graph GetInstance(NodeCanvas.Framework.Graph originalGraph)
  RVA=0x0A9810D8  token=0x60011AC  NodeCanvas.Framework.Graph MakeRuntimeGraphInstance()
  RVA=0x03074AD0  token=0x60011AD  System.Void StartBehaviour()
  RVA=0x0A981670  token=0x60011AE  System.Void StartBehaviour(System.Action<System.Boolean> callback)
  RVA=0x0306B330  token=0x60011AF  System.Void StartBehaviour(NodeCanvas.Framework.Graph.UpdateMode updateMode, System.Action<System.Boolean> callback)
  RVA=0x0A981438  token=0x60011B0  System.Void PauseBehaviour()
  RVA=0x0306ADD0  token=0x60011B1  System.Void StartBehaviourAsync(System.Action callBack)
  RVA=0x030738B0  token=0x60011B2  System.Void GetInstanceAsync(NodeCanvas.Framework.Graph originalGraph, System.Action<NodeCanvas.Framework.Graph> callBack)
  RVA=0x030739A0  token=0x60011B3  System.Void CreateSubInstanceAsync(System.Action callBack)
  RVA=0x0306B770  token=0x60011B4  System.Void StopBehaviour(System.Boolean success)
  RVA=0x0A981A64  token=0x60011B5  System.Void UpdateBehaviour()
  RVA=0x0A9814EC  token=0x60011B6  System.Void RestartBehaviour()
  RVA=0x0A9815D0  token=0x60011B7  System.Void SendEvent(System.String eventName)
  RVA=0x0A981518  token=0x60011B8  System.Void SendEvent(System.String eventName, System.Object value, System.Object sender)
  RVA=-1  // generic def  token=0x60011B9  System.Void SendEvent(System.String eventName, T eventValue, System.Object sender)
  RVA=-1  // generic def  token=0x60011BA  T GetExposedParameterValue(System.String name)
  RVA=-1  // generic def  token=0x60011BB  System.Void SetExposedParameterValue(System.String name, T value)
  RVA=-1  // generic def  token=0x60011BC  NodeCanvas.Framework.ExposedParameter MakeNewExposedParameter(System.String name)
  RVA=0x04B88160  token=0x60011BD  System.Void Awake()
  RVA=0x04B88170  token=0x60011BE  System.Void Initialize()
  RVA=0x0A980F98  token=0x60011BF  System.Void BindExposedParameters()
  RVA=0x0A9816E8  token=0x60011C0  System.Void UnBindExposedParameters()
  RVA=0x03074A80  token=0x60011C1  System.Void OnEnable()
  RVA=0x04D776E0  token=0x60011C2  System.Void Start()
  RVA=0x04D77710  token=0x60011C3  System.Void InvokeStartEvent()
  RVA=0x0A981404  token=0x60011C4  System.Void OnDisable()
  RVA=0x0A981088  token=0x60011C5  System.Void DestroyInstance()
  RVA=0x0A981124  token=0x60011C6  System.Void OnDestroy()
  RVA=0x03CBD990  token=0x60011C7  System.Void .ctor()
  RVA=0x0A981680  token=0x60011C8  System.Void <Initialize>b__109_0()
END_CLASS

CLASS: NodeCanvas.Framework.GraphOwner`1
TYPE:  abstract class
TOKEN: 0x2000412
EXTENDS: NodeCanvas.Framework.GraphOwner
FIELDS:
  private           T                               _graph  // 0x0
  private           UnityEngine.Object              _blackboard  // 0x0
PROPERTIES:
  graph  get=-1  // not resolved  set=-1  // not resolved
  behaviour  get=-1  // not resolved  set=-1  // not resolved
  blackboard  get=-1  // not resolved  set=-1  // not resolved
  graphType  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60011E0  System.Void StartBehaviour(T newGraph)
  RVA=-1  // not resolved  token=0x60011E1  System.Void StartBehaviour(T newGraph, System.Action<System.Boolean> callback)
  RVA=-1  // not resolved  token=0x60011E2  System.Void StartBehaviour(T newGraph, NodeCanvas.Framework.Graph.UpdateMode updateMode, System.Action<System.Boolean> callback)
  RVA=-1  // not resolved  token=0x60011E3  System.Void SwitchBehaviour(T newGraph)
  RVA=-1  // not resolved  token=0x60011E4  System.Void SwitchBehaviour(T newGraph, System.Action<System.Boolean> callback)
  RVA=-1  // not resolved  token=0x60011E5  System.Void SwitchBehaviour(T newGraph, NodeCanvas.Framework.Graph.UpdateMode updateMode, System.Action<System.Boolean> callback)
  RVA=-1  // not resolved  token=0x60011E6  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.IGraphAssignableExtensions
TYPE:  static class
TOKEN: 0x2000413
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0A9828E8  token=0x60011E7  NodeCanvas.Framework.Graph CheckInstance(NodeCanvas.Framework.IGraphAssignable assignable)
  RVA=0x0A982D9C  token=0x60011E8  System.Boolean TryStartSubGraph(NodeCanvas.Framework.IGraphAssignable assignable, UnityEngine.Component agent, System.Action<System.Boolean> callback)
  RVA=0x02FC18F0  token=0x60011E9  System.Boolean TryStopSubGraph(NodeCanvas.Framework.IGraphAssignable assignable)
  RVA=0x02FC1980  token=0x60011EA  System.Boolean TryClearSubGraphData(NodeCanvas.Framework.IGraphAssignable assignable)
  RVA=0x0A982AC0  token=0x60011EB  System.Boolean TryPauseSubGraph(NodeCanvas.Framework.IGraphAssignable assignable)
  RVA=0x0A982D18  token=0x60011EC  System.Boolean TryResumeSubGraph(NodeCanvas.Framework.IGraphAssignable assignable)
  RVA=0x0A982F48  token=0x60011ED  System.Boolean TryUpdateSubGraph(NodeCanvas.Framework.IGraphAssignable assignable)
  RVA=0x0A982FF8  token=0x60011EE  System.Void TryWriteAndBindMappedVariables(NodeCanvas.Framework.IGraphAssignable assignable)
  RVA=0x0A982B44  token=0x60011EF  System.Void TryReadAndUnbindMappedVariables(NodeCanvas.Framework.IGraphAssignable assignable)
  RVA=0x0A983214  token=0x60011F0  System.Void ValidateSubGraphAndParameters(NodeCanvas.Framework.IGraphAssignable assignable)
END_CLASS

CLASS: NodeCanvas.Framework.IGraphElement
TYPE:  interface
TOKEN: 0x2000415
FIELDS:
PROPERTIES:
  name  get=-1  // abstract
  UID  get=-1  // abstract
  graph  get=-1  // abstract
  status  get=-1  // abstract
METHODS:
END_CLASS

CLASS: NodeCanvas.Framework.IUpdatable
TYPE:  interface
TOKEN: 0x2000416
IMPLEMENTS: NodeCanvas.Framework.IGraphElement
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60011F7  System.Void Update()
END_CLASS

CLASS: NodeCanvas.Framework.IInvokable
TYPE:  interface
TOKEN: 0x2000417
IMPLEMENTS: NodeCanvas.Framework.IGraphElement
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60011F8  System.String GetInvocationID()
  RVA=-1  // abstract  token=0x60011F9  System.Object Invoke(System.Object[] args)
  RVA=-1  // abstract  token=0x60011FA  System.Void InvokeAsync(System.Action<System.Object> callback, System.Object[] args)
END_CLASS

CLASS: NodeCanvas.Framework.IGraphAssignable
TYPE:  interface
TOKEN: 0x2000418
IMPLEMENTS: NodeCanvas.Framework.IGraphElement
FIELDS:
PROPERTIES:
  subGraph  get=-1  // abstract  set=-1  // abstract
  currentInstance  get=-1  // abstract  set=-1  // abstract
  instances  get=-1  // abstract  set=-1  // abstract
  subGraphParameter  get=-1  // abstract
  variablesMap  get=-1  // abstract  set=-1  // abstract
METHODS:
END_CLASS

CLASS: NodeCanvas.Framework.IGraphAssignable`1
TYPE:  interface
TOKEN: 0x2000419
IMPLEMENTS: NodeCanvas.Framework.IGraphAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
PROPERTIES:
  subGraph  get=-1  // abstract  set=-1  // abstract
  currentInstance  get=-1  // abstract  set=-1  // abstract
METHODS:
END_CLASS

CLASS: NodeCanvas.Framework.ITaskAssignable
TYPE:  interface
TOKEN: 0x200041A
IMPLEMENTS: NodeCanvas.Framework.IGraphElement
FIELDS:
PROPERTIES:
  task  get=-1  // abstract  set=-1  // abstract
METHODS:
END_CLASS

CLASS: NodeCanvas.Framework.ITaskAssignable`1
TYPE:  interface
TOKEN: 0x200041B
IMPLEMENTS: NodeCanvas.Framework.ITaskAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
METHODS:
END_CLASS

CLASS: NodeCanvas.Framework.IHaveNodeReference
TYPE:  interface
TOKEN: 0x200041C
IMPLEMENTS: NodeCanvas.Framework.IGraphElement
FIELDS:
PROPERTIES:
  targetReference  get=-1  // abstract
METHODS:
END_CLASS

CLASS: NodeCanvas.Framework.IReflectedWrapper
TYPE:  interface
TOKEN: 0x200041D
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600120B  ParadoxNotion.Serialization.ISerializedReflectedInfo GetSerializedInfo()
END_CLASS

CLASS: NodeCanvas.Framework.ISubTasksContainer
TYPE:  interface
TOKEN: 0x200041E
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600120C  NodeCanvas.Framework.Task[] GetSubTasks()
END_CLASS

CLASS: NodeCanvas.Framework.ISubParametersContainer
TYPE:  interface
TOKEN: 0x200041F
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600120D  NodeCanvas.Framework.BBParameter[] GetSubParameters()
END_CLASS

CLASS: NodeCanvas.Framework.Node
TYPE:  abstract class
TOKEN: 0x2000420
SIZE:  0x98
IMPLEMENTS: NodeCanvas.Framework.IGraphElement ParadoxNotion.Serialization.FullSerializer.ISerializationCollectable
FIELDS:
  public            System.Single                   additionalWidth  // 0x10
  private           System.String                   _UID  // 0x18
  private           System.String                   _name  // 0x20
  private           System.String                   _tag  // 0x28
  private           UnityEngine.Vector2             _position  // 0x30
  private           System.String                   _comment  // 0x38
  private           System.Boolean                  _isBreakpoint  // 0x40
  private           NodeCanvas.Framework.Graph      _graph  // 0x48
  private           System.Int32                    _ID  // 0x50
  private           System.Collections.Generic.List<NodeCanvas.Framework.Connection>_inConnections  // 0x58
  private           System.Collections.Generic.List<NodeCanvas.Framework.Connection>_outConnections  // 0x60
  private           NodeCanvas.Framework.Status     _status  // 0x68
  private           System.String                   _nameCache  // 0x70
  private           System.String                   _descriptionCache  // 0x78
  private           System.Int32                    _priorityCache  // 0x80
  private   readonly System.Boolean                  <allowResizeHorizontal>k__BackingField  // 0x84
  private           System.Boolean                  <canResizeHorizontal>k__BackingField  // 0x85
  private           System.Boolean                  <resizingHorizontal>k__BackingField  // 0x86
  private           System.Boolean                  <isHighlight>k__BackingField  // 0x87
  public            System.Single                   highlightColorFade  // 0x88
  private           System.Single                   <timeStarted>k__BackingField  // 0x8c
  private           System.Boolean                  <isChecked>k__BackingField  // 0x90
  private           System.Boolean                  <breakPointReached>k__BackingField  // 0x91
PROPERTIES:
  overrideNodeWidth  get=0x011EC580
  graph  get=0x04D86200  set=0x035A41A0
  ID  get=0x04D86550  set=0x04D865A0
  UID  get=0x0A984784
  inConnections  get=0x02E56440  set=0x036E5590
  outConnections  get=0x04D86280  set=0x063CC164
  position  get=0x04D9E6C0  set=0x011F36F0
  customName  get=0x04D862C0  set=0x05390F40
  tag  get=0x04D86240  set=0x02FDB880
  comments  get=0x04D85A50  set=0x046A91B0
  commentForShow  get=0x04D85A50
  rawComment  get=0x04D85A50
  modifyComment  get=0x012081B0
  modifiedComment  get=0x0A984974
  isBreakpoint  get=0x02D480A0  set=0x02D480B0
  name  get=0x0A9849AC  set=0x05390F40
  description  get=0x0A9847BC
  priority  get=0x0A984B4C
  maxInConnections  get=-1  // abstract
  maxOutConnections  get=-1  // abstract
  outConnectionType  get=-1  // abstract
  allowAsPrime  get=-1  // abstract
  canSelfConnect  get=-1  // abstract
  commentsAlignment  get=-1  // abstract
  iconAlignment  get=-1  // abstract
  isBeyondGraph  get=0x012081B0
  drawInspector  get=0x02FFF600
  inspectorShowTag  get=0x02FFF600
  allowResizeHorizontal  get=0x02A80FA0
  canResizeHorizontal  get=0x04D89370  set=0x04DA6840
  resizingHorizontal  get=0x04D88E70  set=0x04DA6860
  allowDragInContentRect  get=0x02FFF600
  nodeIconKey  get=0x0A984B14
  nodeIconKey2  get=0x0A984AA4
  nodeIconKey3  get=0x0A984ADC
  isHighlight  get=0x04DA6830  set=0x04DA6850
  drawNameTagOrComment  get=0x02FFF600
  status  get=0x04D86CF0  set=0x0A984BE4
  graphAgent  get=0x0A984894
  graphBlackboard  get=0x0A984904
  elapsedTime  get=0x0A984864
  timeStarted  get=0x04D8E210  set=0x04D8E230
  isChecked  get=0x02EC6590  set=0x02EC65A0
  breakPointReached  get=0x04D88160  set=0x04D88180
METHODS:
  RVA=0x0A983494  token=0x6001214  System.String AllocateUID()
  RVA=0x05392C40  token=0x6001215  System.Void SetUID(System.String value)
  RVA=0x043DB830  token=0x600124D  System.Void .ctor()
  RVA=0x0A983624  token=0x600124E  NodeCanvas.Framework.Node Create(NodeCanvas.Framework.Graph targetGraph, System.Type nodeType, UnityEngine.Vector2 pos)
  RVA=0x0A9837C0  token=0x600124F  NodeCanvas.Framework.Node Duplicate(NodeCanvas.Framework.Graph targetGraph)
  RVA=0x0A984628  token=0x6001250  System.Void Validate(NodeCanvas.Framework.Graph assignedGraph)
  RVA=0x0A983BC0  token=0x6001251  NodeCanvas.Framework.Status Execute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0306EAF0  token=0x6001252  System.Void Reset(System.Boolean recursively)
  RVA=0x0A984728  token=0x6001253  System.Collections.IEnumerator YieldBreak(System.Action resume)
  RVA=0x0A983B0C  token=0x6001254  NodeCanvas.Framework.Status Error(System.Object msg)
  RVA=0x0A983C38  token=0x6001255  NodeCanvas.Framework.Status Fail(System.String msg)
  RVA=0x041E1670  token=0x6001256  System.Void Warn(System.String msg)
  RVA=0x0A958184  token=0x6001257  System.Void SetStatus(NodeCanvas.Framework.Status status)
  RVA=0x0A984310  token=0x6001258  System.Void SendEvent(System.String eventName)
  RVA=-1  // generic def  token=0x6001259  System.Void SendEvent(System.String eventName, T value)
  RVA=0x0A984178  token=0x600125A  System.Boolean IsNewConnectionAllowed(NodeCanvas.Framework.Node sourceNode, NodeCanvas.Framework.Node targetNode, NodeCanvas.Framework.Connection refConnection)
  RVA=0x02FFF600  token=0x600125B  System.Boolean CanConnectToTarget(NodeCanvas.Framework.Node targetNode)
  RVA=0x02FFF600  token=0x600125C  System.Boolean CanConnectFromSource(NodeCanvas.Framework.Node sourceNode)
  RVA=0x0A9834C8  token=0x600125D  System.Boolean AreNodesConnected(NodeCanvas.Framework.Node a, NodeCanvas.Framework.Node b)
  RVA=0x0A984340  token=0x600125E  UnityEngine.Coroutine StartCoroutine(System.Collections.IEnumerator routine)
  RVA=0x0A9843C0  token=0x600125F  System.Void StopCoroutine(UnityEngine.Coroutine routine)
  RVA=0x041E1670  token=0x6001260  System.Void StartParallelTask(System.Action action)
  RVA=0x041E1670  token=0x6001261  System.Void StopParallelTask(System.Action action)
  RVA=0x0A983EB4  token=0x6001262  System.Collections.Generic.IEnumerable<NodeCanvas.Framework.Node> GetParentNodes()
  RVA=0x0A983C50  token=0x6001263  System.Collections.Generic.IEnumerable<NodeCanvas.Framework.Node> GetChildNodes()
  RVA=0x0A9840AC  token=0x6001264  System.Boolean IsChildOf(NodeCanvas.Framework.Node parentNode)
  RVA=0x0A984244  token=0x6001265  System.Boolean IsParentOf(NodeCanvas.Framework.Node childNode)
  RVA=0x0A983FFC  token=0x6001266  System.String GetWarningOrError()
  RVA=0x0A983D98  token=0x6001267  System.String GetHardError()
  RVA=0x04D86CF0  token=0x6001268  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x041E1670  token=0x6001269  System.Void OnReset()
  RVA=0x041E1670  token=0x600126A  System.Void OnCreate(NodeCanvas.Framework.Graph assignedGraph)
  RVA=0x041E1670  token=0x600126B  System.Void OnDuplicate(NodeCanvas.Framework.Graph assignedGraph)
  RVA=0x041E1670  token=0x600126C  System.Void OnValidate(NodeCanvas.Framework.Graph assignedGraph)
  RVA=0x041E1670  token=0x600126D  System.Void OnDestroy()
  RVA=0x041E1670  token=0x600126E  System.Void OnParentConnected(System.Int32 connectionIndex)
  RVA=0x041E1670  token=0x600126F  System.Void OnParentDisconnected(System.Int32 connectionIndex)
  RVA=0x041E1670  token=0x6001270  System.Void OnChildConnected(System.Int32 connectionIndex)
  RVA=0x041E1670  token=0x6001271  System.Void OnChildDisconnected(System.Int32 connectionIndex)
  RVA=0x041E1670  token=0x6001272  System.Void OnChildrenConnectionsSorted(System.Int32[] oldIndeces)
  RVA=0x041E1670  token=0x6001273  System.Void OnGraphStarted()
  RVA=0x041E1670  token=0x6001274  System.Void OnPostGraphStarted()
  RVA=0x041E1670  token=0x6001275  System.Void OnGraphStoped()
  RVA=0x041E1670  token=0x6001276  System.Void OnPostGraphStoped()
  RVA=0x041E1670  token=0x6001277  System.Void OnGraphPaused()
  RVA=0x041E1670  token=0x6001278  System.Void OnGraphUnpaused()
  RVA=0x041E1670  token=0x6001279  System.Void OnClearNodeData()
  RVA=0x0A98443C  token=0x600127A  System.String ToString()
END_CLASS

CLASS: NodeCanvas.Framework.INodeReference
TYPE:  interface
TOKEN: 0x2000427
FIELDS:
PROPERTIES:
  type  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x600128E  NodeCanvas.Framework.Node Get(NodeCanvas.Framework.Graph graph)
  RVA=-1  // abstract  token=0x600128F  System.Void Set(NodeCanvas.Framework.Node target)
END_CLASS

CLASS: NodeCanvas.Framework.NodeReference`1
TYPE:  class
TOKEN: 0x2000428
IMPLEMENTS: NodeCanvas.Framework.INodeReference
FIELDS:
  private           System.String                   _targetNodeUID  // 0x0
  private           System.WeakReference<T>         _targetNodeRef  // 0x0
PROPERTIES:
  NodeCanvas.Framework.INodeReference.type  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6001291  NodeCanvas.Framework.Node NodeCanvas.Framework.INodeReference.Get(NodeCanvas.Framework.Graph graph)
  RVA=-1  // not resolved  token=0x6001292  System.Void NodeCanvas.Framework.INodeReference.Set(NodeCanvas.Framework.Node target)
  RVA=-1  // not resolved  token=0x6001293  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6001294  System.Void .ctor(T target)
  RVA=-1  // not resolved  token=0x6001295  T Get(NodeCanvas.Framework.Graph graph)
  RVA=-1  // not resolved  token=0x6001296  System.Void Set(T target)
END_CLASS

CLASS: NodeCanvas.Framework.Status
TYPE:  sealed struct
TOKEN: 0x200042A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  NodeCanvas.Framework.Status     Failure  // const
  public    static  NodeCanvas.Framework.Status     Success  // const
  public    static  NodeCanvas.Framework.Status     Running  // const
  public    static  NodeCanvas.Framework.Status     Resting  // const
  public    static  NodeCanvas.Framework.Status     Error  // const
  public    static  NodeCanvas.Framework.Status     Optional  // const
METHODS:
END_CLASS

CLASS: NodeCanvas.Framework.ActionList
TYPE:  class
TOKEN: 0x200042B
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.ActionList.ActionsExecutionModeexecutionMode  // 0x68
  public            System.Collections.Generic.List<NodeCanvas.Framework.ActionTask>actions  // 0x70
  private           System.Int32                    currentActionIndex  // 0x78
  private           System.Boolean[]                finishedIndeces  // 0x80
PROPERTIES:
  info  get=0x0A97D7E0
METHODS:
  RVA=0x0A97CFF0  token=0x600129A  NodeCanvas.Framework.Task Duplicate(NodeCanvas.Framework.ITaskSystem newOwnerSystem)
  RVA=0x0A97D35C  token=0x600129B  System.String OnInit()
  RVA=0x0A97D2F4  token=0x600129C  System.Void OnExecute()
  RVA=0x0A97D510  token=0x600129D  System.Void OnUpdate()
  RVA=0x0A97D464  token=0x600129E  System.Void OnStop()
  RVA=0x0A97D3C8  token=0x600129F  System.Void OnPause()
  RVA=0x0A97D254  token=0x60012A0  System.Void OnDrawGizmosSelected()
  RVA=0x0A97CE08  token=0x60012A1  System.Void AddAction(NodeCanvas.Framework.ActionTask action)
  RVA=0x0A97D1D0  token=0x60012A2  System.String GetWarningOrError()
  RVA=0x0A97D760  token=0x60012A3  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.ActionTask`1
TYPE:  abstract class
TOKEN: 0x200042D
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
PROPERTIES:
  agentType  get=-1  // not resolved
  agent  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60012A6  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.ActionTask
TYPE:  abstract class
TOKEN: 0x200042E
SIZE:  0x68
EXTENDS: NodeCanvas.Framework.Task
FIELDS:
  private           NodeCanvas.Framework.Status     status  // 0x58
  private           System.Single                   timeStarted  // 0x5c
  private           System.Boolean                  latch  // 0x60
  private           System.Boolean                  <isPaused>k__BackingField  // 0x61
PROPERTIES:
  elapsedTime  get=0x0A97DBFC
  isRunning  get=0x0A97DC30
  isPaused  get=0x04D892D0  set=0x04D8FF40
METHODS:
  RVA=0x0A97DA2C  token=0x60012AB  System.Void ExecuteIndependent(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard, System.Action<NodeCanvas.Framework.Status> callback)
  RVA=0x0A97DB3C  token=0x60012AC  System.Collections.IEnumerator IndependentActionUpdater(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard, System.Action<NodeCanvas.Framework.Status> callback)
  RVA=0x0A97DA24  token=0x60012AD  NodeCanvas.Framework.Status ExecuteAction(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A97DAAC  token=0x60012AE  NodeCanvas.Framework.Status Execute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A954718  token=0x60012AF  System.Void EndAction()
  RVA=0x0A97DA0C  token=0x60012B0  System.Void EndAction(System.Boolean success)
  RVA=0x0A97D9B8  token=0x60012B1  System.Void EndAction(System.Nullable<System.Boolean> success)
  RVA=0x0A97DBE4  token=0x60012B2  System.Void Pause()
  RVA=0x041E1670  token=0x60012B3  System.Void OnExecute()
  RVA=0x041E1670  token=0x60012B4  System.Void OnUpdate()
  RVA=0x033C4B40  token=0x60012B5  System.Void OnStop(System.Boolean interrupted)
  RVA=0x041E1670  token=0x60012B6  System.Void OnStop()
  RVA=0x041E1670  token=0x60012B7  System.Void OnPause()
  RVA=0x041E1670  token=0x60012B8  System.Void OnResume()
  RVA=0x04D8E030  token=0x60012B9  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.ConditionList
TYPE:  class
TOKEN: 0x2000430
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            NodeCanvas.Framework.ConditionList.ConditionsCheckModecheckMode  // 0x68
  public            System.Collections.Generic.List<NodeCanvas.Framework.ConditionTask>conditions  // 0x70
PROPERTIES:
  allTrueRequired  get=0x0A9801C8
  info  get=0x0A9801D0
METHODS:
  RVA=0x0A97FCE8  token=0x60012C2  NodeCanvas.Framework.Task Duplicate(NodeCanvas.Framework.ITaskSystem newOwnerSystem)
  RVA=0x041E1670  token=0x60012C3  System.Void OnEnable()
  RVA=0x0A9800AC  token=0x60012C4  System.Void OnDisable()
  RVA=0x0A97FF4C  token=0x60012C5  System.Boolean OnCheck()
  RVA=0x0A980128  token=0x60012C6  System.Void OnDrawGizmosSelected()
  RVA=0x0A97FB00  token=0x60012C7  System.Void AddCondition(NodeCanvas.Framework.ConditionTask condition)
  RVA=0x0A97FEC8  token=0x60012C8  System.String GetWarningOrError()
  RVA=0x047CF4E0  token=0x60012C9  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.ConditionTask`1
TYPE:  abstract class
TOKEN: 0x2000432
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
PROPERTIES:
  agentType  get=-1  // not resolved
  agent  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60012CC  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.ConditionTask
TYPE:  abstract class
TOKEN: 0x2000433
SIZE:  0x68
EXTENDS: NodeCanvas.Framework.Task
FIELDS:
  private           System.Boolean                  _invert  // 0x58
  private           System.Int32                    yieldReturn  // 0x5c
  private           System.Int32                    yields  // 0x60
  private           System.Boolean                  isRuntimeEnabled  // 0x64
PROPERTIES:
  invert  get=0x04D86F70  set=0x04D86F80
METHODS:
  RVA=0x0A980494  token=0x60012CF  System.Void Enable(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard bb)
  RVA=0x043111A0  token=0x60012D0  System.Void Disable()
  RVA=0x0A9803CC  token=0x60012D1  System.Boolean CheckCondition(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A980424  token=0x60012D2  System.Boolean Check(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A9803D4  token=0x60012D3  System.Boolean CheckOnce(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A980528  token=0x60012D4  System.Void YieldReturn(System.Boolean value, System.Boolean keepValueUntilUsed)
  RVA=0x0A9804CC  token=0x60012D5  System.Collections.IEnumerator Flip()
  RVA=0x041E1670  token=0x60012D6  System.Void OnEnable()
  RVA=0x041E1670  token=0x60012D7  System.Void OnDisable()
  RVA=0x02FFF600  token=0x60012D8  System.Boolean OnCheck()
  RVA=0x04D8E020  token=0x60012D9  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.EventReceiverAttribute
TYPE:  class
TOKEN: 0x2000435
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.String[]                 eventMessages  // 0x10
METHODS:
  RVA=0x053908C0  token=0x60012E0  System.Void .ctor(System.String[] args)
END_CLASS

CLASS: NodeCanvas.Framework.ITaskSystem
TYPE:  interface
TOKEN: 0x2000436
FIELDS:
PROPERTIES:
  agent  get=-1  // abstract
  blackboard  get=-1  // abstract
  contextObject  get=-1  // abstract
  elapsedTime  get=-1  // abstract
  deltaTime  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60012E6  System.Void UpdateTasksOwner()
  RVA=-1  // abstract  token=0x60012E7  System.Void SendEvent(System.String name, System.Object value, System.Object sender)
  RVA=-1  // abstract  token=0x60012E8  System.Void SendEvent(System.String name, T value, System.Object sender)
END_CLASS

CLASS: NodeCanvas.Framework.Task
TYPE:  abstract class
TOKEN: 0x2000437
SIZE:  0x58
IMPLEMENTS: ParadoxNotion.Serialization.FullSerializer.ISerializationCollectable UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           System.Boolean                  _isUserDisabled  // 0x10
  protected internal        NodeCanvas.Framework.Internal.TaskAgentParameter_agentParameter  // 0x18
  private           NodeCanvas.Framework.ITaskSystem_ownerSystem  // 0x20
  private           UnityEngine.Component           _currentAgent  // 0x28
  private           System.String                   _taskName  // 0x30
  private           System.String                   _taskDescription  // 0x38
  private           System.String                   _obsoleteInfo  // 0x40
  private           System.Boolean                  _isRuntimeActive  // 0x48
  private           System.Boolean                  _isInitSuccess  // 0x49
  private           ParadoxNotion.Services.EventRouter_eventRouter  // 0x50
PROPERTIES:
  ownerSystem  get=0x04D862C0  set=0x05390F40
  ownerSystemAgent  get=0x0A98C070
  ownerSystemBlackboard  get=0x0A98C0C4
  ownerSystemElapsedTime  get=0x0A98C0E4
  isUserEnabled  get=0x0A98BEE0  set=0x0A98C3E8
  obsolete  get=0x0A98BFC4
  name  get=0x0A98BEE8
  description  get=0x0A98BE34
  summaryInfo  get=0x0A98C1E4
  info  get=0x06BAE448
  agentType  get=0x011EC580
  agentInfo  get=0x0A98BD44
  agentParameterName  get=0x0A98BD90
  agentIsOverride  get=0x067B9E50  set=0x0A98C33C
  agent  get=0x0A98BDA4
  blackboard  get=0x0A98BE2C
  router  get=0x0A98C104
METHODS:
  RVA=0x0A98BC84  token=0x60012E9  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x041E1670  token=0x60012EA  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x041E1670  token=0x60012EB  System.Void .ctor()
  RVA=-1  // generic def  token=0x60012EC  T Create(NodeCanvas.Framework.ITaskSystem newOwnerSystem)
  RVA=0x0A98AFBC  token=0x60012ED  NodeCanvas.Framework.Task Create(System.Type type, NodeCanvas.Framework.ITaskSystem newOwnerSystem)
  RVA=0x0A98B0F0  token=0x60012EE  NodeCanvas.Framework.Task Duplicate(NodeCanvas.Framework.ITaskSystem newOwnerSystem)
  RVA=0x0A98BD0C  token=0x60012EF  System.Void Validate(NodeCanvas.Framework.ITaskSystem ownerSystem)
  RVA=0x05390F40  token=0x60012F0  System.Void SetOwnerSystem(NodeCanvas.Framework.ITaskSystem newOwnerSystem)
  RVA=0x0A98BA90  token=0x6001305  System.Boolean Set(UnityEngine.Component newAgent, NodeCanvas.Framework.IBlackboard newBB)
  RVA=0x0A98B928  token=0x6001306  System.Boolean Initialize(UnityEngine.Component newAgent)
  RVA=0x0A98B72C  token=0x6001307  System.Boolean InitializeFieldAttributes(UnityEngine.Component newAgent)
  RVA=0x012081B0  token=0x6001308  System.Boolean Error(System.String error, System.String tag)
  RVA=0x0A98BB80  token=0x6001309  UnityEngine.Coroutine StartCoroutine(System.Collections.IEnumerator routine)
  RVA=0x0A98BC00  token=0x600130A  System.Void StopCoroutine(UnityEngine.Coroutine routine)
  RVA=0x0A98BA38  token=0x600130B  System.Void SendEvent(System.String name)
  RVA=-1  // generic def  token=0x600130C  System.Void SendEvent(System.String name, T value)
  RVA=0x0A98B2A4  token=0x600130D  System.String GetWarningOrError()
  RVA=0x011EC580  token=0x600130E  System.String OnErrorCheck()
  RVA=0x0A98B188  token=0x600130F  System.String GetHardError()
  RVA=0x011EC580  token=0x6001310  System.String OnInit()
  RVA=0x041E1670  token=0x6001311  System.Void OnCreate(NodeCanvas.Framework.ITaskSystem ownerSystem)
  RVA=0x041E1670  token=0x6001312  System.Void OnValidate(NodeCanvas.Framework.ITaskSystem ownerSystem)
  RVA=0x0A98BA28  token=0x6001313  System.Void OnDrawGizmos()
  RVA=0x041E1670  token=0x6001314  System.Void OnDrawGizmosSelected()
  RVA=0x05390F50  token=0x6001315  System.Void ClearTaskData()
  RVA=0x041E1670  token=0x6001316  System.Void OnClearTaskData()
  RVA=0x0A98BC7C  token=0x6001317  System.String ToString()
END_CLASS

CLASS: NodeCanvas.Framework.AssetBlackboard
TYPE:  class
TOKEN: 0x2000439
SIZE:  0x50
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver NodeCanvas.Framework.IGlobalBlackboard NodeCanvas.Framework.IBlackboard
FIELDS:
  private           System.Action<NodeCanvas.Framework.Variable>onVariableAdded  // 0x18
  private           System.Action<NodeCanvas.Framework.Variable>onVariableRemoved  // 0x20
  private           System.String                   _serializedBlackboard  // 0x28
  private           System.Collections.Generic.List<UnityEngine.Object>_objectReferences  // 0x30
  private           System.String                   _UID  // 0x38
  private           System.String                   _identifier  // 0x40
  private           NodeCanvas.Framework.Internal.BlackboardSource_blackboard  // 0x48
PROPERTIES:
  NodeCanvas.Framework.IBlackboard.variables  get=0x0306FCE0  set=0x0A97DC4C
  NodeCanvas.Framework.IBlackboard.unityContextObject  get=0x0464E630
  NodeCanvas.Framework.IBlackboard.parent  get=0x011EC580
  NodeCanvas.Framework.IBlackboard.propertiesBindTarget  get=0x011EC580
  NodeCanvas.Framework.IBlackboard.independantVariablesFieldName  get=0x011EC580
  identifier  get=0x04D85A60
  UID  get=0x04D85A50
EVENTS:
  onVariableAdded  add=add_onVariableAdded  remove=remove_onVariableAdded
  onVariableRemoved  add=add_onVariableRemoved  remove=remove_onVariableRemoved
METHODS:
  RVA=0x0A97DDFC  token=0x600131D  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x049E1A90  token=0x600131E  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x0A97DC94  token=0x600131F  System.Void SelfSerialize()
  RVA=0x049E1AA0  token=0x6001320  System.Void SelfDeserialize()
  RVA=0x0306DA80  token=0x6001327  System.Void NodeCanvas.Framework.IBlackboard.TryInvokeOnVariableAdded(NodeCanvas.Framework.Variable variable)
  RVA=0x0A97DC38  token=0x6001328  System.Void NodeCanvas.Framework.IBlackboard.TryInvokeOnVariableRemoved(NodeCanvas.Framework.Variable variable)
  RVA=0x0A97DD9C  token=0x600132B  System.Void ShowJson()
  RVA=0x04D85A60  token=0x600132C  System.String ToString()
  RVA=0x0A97DC70  token=0x600132D  System.Void OnValidate()
  RVA=0x03113D40  token=0x600132E  System.Void OnEnable()
  RVA=0x03CBDE90  token=0x600132F  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.BBParameter
TYPE:  abstract class
TOKEN: 0x200043A
SIZE:  0x38
IMPLEMENTS: ParadoxNotion.Serialization.FullSerializer.ISerializationCollectable UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           System.String                   _name  // 0x10
  private           System.String                   _targetVariableID  // 0x18
  private           NodeCanvas.Framework.IBlackboard_bb  // 0x20
  private           NodeCanvas.Framework.Variable   _varRef  // 0x28
  private           System.Action<NodeCanvas.Framework.Variable>onVariableReferenceChanged  // 0x30
PROPERTIES:
  targetVariableID  get=0x0385B100  set=0x05392C40
  varRef  get=0x04D86240  set=0x0306F630
  name  get=0x02B2ECC0  set=0x0A97EBE0
  bb  get=0x04D862C0  set=0x0306F4F0
  useBlackboard  get=0x0A77DD74  set=0x0A97EC60
  isPresumedDynamic  get=0x0A97EA74
  isNone  get=0x044FE540
  isNull  get=0x0A97EA50
  isNoneOrNull  get=0x0A97EA20
  isDefined  get=0x0A97EA08
  refType  get=0x0A97EB1C
  value  get=0x0A97EB44  set=0x0A97ECEC
  varType  get=-1  // abstract
EVENTS:
  onVariableReferenceChanged  add=add_onVariableReferenceChanged  remove=remove_onVariableReferenceChanged
METHODS:
  RVA=0x0A97E964  token=0x6001330  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x041E1670  token=0x6001331  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x041E1670  token=0x6001334  System.Void .ctor()
  RVA=0x0A97E364  token=0x6001335  NodeCanvas.Framework.BBParameter CreateInstance(System.Type t, NodeCanvas.Framework.IBlackboard bb)
  RVA=0x0A97E5C4  token=0x6001336  System.Void SetBBFields(System.Object target, NodeCanvas.Framework.IBlackboard bb)
  RVA=-1  // abstract  token=0x600134A  System.Void SetDefaultValue()
  RVA=-1  // abstract  token=0x600134B  System.Void Bind(NodeCanvas.Framework.Variable data)
  RVA=-1  // abstract  token=0x600134C  System.Object GetValueBoxed()
  RVA=-1  // abstract  token=0x600134D  System.Void SetValueBoxed(System.Object value)
  RVA=0x0A97E6B4  token=0x600134E  System.Void SetTargetVariable(NodeCanvas.Framework.IBlackboard targetBB, NodeCanvas.Framework.Variable targetVariable)
  RVA=0x0306F540  token=0x600134F  NodeCanvas.Framework.Variable ResolveReference(NodeCanvas.Framework.IBlackboard targetBlackboard, System.Boolean useID)
  RVA=0x0A97E49C  token=0x6001350  NodeCanvas.Framework.Variable PromoteToVariable(NodeCanvas.Framework.IBlackboard targetBB)
  RVA=0x0A97E7F8  token=0x6001351  System.String ToString()
END_CLASS

CLASS: NodeCanvas.Framework.BBParameter`1
TYPE:  class
TOKEN: 0x200043C
EXTENDS: NodeCanvas.Framework.BBParameter
FIELDS:
  protected         T                               _value  // 0x0
  private           System.Func<T>                  getter  // 0x0
  private           System.Action<T>                setter  // 0x0
PROPERTIES:
  value  get=-1  // not resolved  set=-1  // not resolved
  varType  get=-1  // not resolved
EVENTS:
  getter  add=add_getter  remove=remove_getter
  setter  add=add_setter  remove=remove_setter
METHODS:
  RVA=-1  // not resolved  token=0x600135B  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600135C  System.Void .ctor(T value)
  RVA=-1  // not resolved  token=0x600135D  System.Object GetValueBoxed()
  RVA=-1  // not resolved  token=0x600135E  System.Void SetValueBoxed(System.Object newValue)
  RVA=-1  // not resolved  token=0x600135F  T GetValue()
  RVA=-1  // not resolved  token=0x6001360  System.Void SetValue(T value)
  RVA=-1  // not resolved  token=0x6001361  System.Void SetDefaultValue()
  RVA=-1  // not resolved  token=0x6001362  System.Void Bind(NodeCanvas.Framework.Variable variable)
  RVA=-1  // not resolved  token=0x6001363  System.Boolean BindGetter(NodeCanvas.Framework.Variable variable)
  RVA=-1  // not resolved  token=0x6001364  System.Boolean BindSetter(NodeCanvas.Framework.Variable variable)
  RVA=-1  // not resolved  token=0x6001365  NodeCanvas.Framework.BBParameter<T> op_Implicit(T value)
END_CLASS

CLASS: NodeCanvas.Framework.Blackboard
TYPE:  class
TOKEN: 0x2000440
SIZE:  0x60
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver NodeCanvas.Framework.IBlackboard
FIELDS:
  private           NodeCanvas.Framework.AssetBlackboard_parentBlackboard  // 0x18
  private           System.String                   _serializedBlackboard  // 0x20
  private           System.Collections.Generic.List<UnityEngine.Object>_objectReferences  // 0x28
  private           ParadoxNotion.Serialization.SerializationPair[]_serializedVariables  // 0x30
  private           NodeCanvas.Framework.Internal.BlackboardSource_blackboard  // 0x38
  private           System.Boolean                  haltForUndo  // 0x40
  private           System.String                   _identifier  // 0x48
  private           System.Action<NodeCanvas.Framework.Variable>onVariableAdded  // 0x50
  private           System.Action<NodeCanvas.Framework.Variable>onVariableRemoved  // 0x58
PROPERTIES:
  blackboard  get=0x04D85A50
  parentBlackboard  get=0x0385B100
  NodeCanvas.Framework.IBlackboard.identifier  get=0x04D86200
  NodeCanvas.Framework.IBlackboard.variables  get=0x0A97F198  set=0x0A97F1B4
  NodeCanvas.Framework.IBlackboard.propertiesBindTarget  get=0x0464E630
  NodeCanvas.Framework.IBlackboard.unityContextObject  get=0x0464E630
  NodeCanvas.Framework.IBlackboard.parent  get=0x0385B100
  NodeCanvas.Framework.IBlackboard.independantVariablesFieldName  get=0x0A97F16C
EVENTS:
  onVariableAdded  add=add_onVariableAdded  remove=remove_onVariableAdded
  onVariableRemoved  add=add_onVariableRemoved  remove=remove_onVariableRemoved
METHODS:
  RVA=0x05392C40  token=0x600136F  System.Void SetParentBlackboard(NodeCanvas.Framework.AssetBlackboard bb)
  RVA=0x0A97F858  token=0x6001370  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x0A97F850  token=0x6001371  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x0A97F434  token=0x6001372  System.Void SelfSerialize()
  RVA=0x0A97F278  token=0x6001373  System.Void SelfDeserialize()
  RVA=0x0A97F730  token=0x6001374  System.String Serialize(System.Collections.Generic.List<UnityEngine.Object> references, System.Boolean pretyJson)
  RVA=0x0A97EFC4  token=0x6001375  System.Boolean Deserialize(System.String json, System.Collections.Generic.List<UnityEngine.Object> references, System.Boolean removeMissingVariables)
  RVA=0x0A97F144  token=0x6001381  System.Void NodeCanvas.Framework.IBlackboard.TryInvokeOnVariableAdded(NodeCanvas.Framework.Variable variable)
  RVA=0x0A97F158  token=0x6001382  System.Void NodeCanvas.Framework.IBlackboard.TryInvokeOnVariableRemoved(NodeCanvas.Framework.Variable variable)
  RVA=0x0A97EF74  token=0x6001383  System.Void Awake()
  RVA=0x0A97EF6C  token=0x6001384  NodeCanvas.Framework.Variable AddVariable(System.String name, System.Type type)
  RVA=0x0A97EF64  token=0x6001385  NodeCanvas.Framework.Variable AddVariable(System.String name, System.Object value)
  RVA=0x0A97F210  token=0x6001386  NodeCanvas.Framework.Variable RemoveVariable(System.String name)
  RVA=0x0A97F078  token=0x6001387  NodeCanvas.Framework.Variable GetVariable(System.String name, System.Type ofType)
  RVA=0x0A97F070  token=0x6001388  NodeCanvas.Framework.Variable GetVariableByID(System.String ID)
  RVA=-1  // generic def  token=0x6001389  NodeCanvas.Framework.Variable<T> GetVariable(System.String name)
  RVA=-1  // generic def  token=0x600138A  T GetVariableValue(System.String name)
  RVA=0x0A97F7E8  token=0x600138B  NodeCanvas.Framework.Variable SetVariableValue(System.String name, System.Object value)
  RVA=-1  // generic def  token=0x600138C  T GetValue(System.String name)
  RVA=0x0A97F7E8  token=0x600138D  NodeCanvas.Framework.Variable SetValue(System.String name, System.Object value)
  RVA=0x0A97F7F0  token=0x600138E  System.Void ShowJson()
  RVA=0x0A97F254  token=0x600138F  System.String Save()
  RVA=0x0A97F218  token=0x6001390  System.String Save(System.String saveKey)
  RVA=0x0A97F120  token=0x6001391  System.Boolean Load()
  RVA=0x0A97F080  token=0x6001392  System.Boolean Load(System.String saveKey)
  RVA=0x0A97F1D8  token=0x6001393  System.Void OnValidate()
  RVA=0x04D86200  token=0x6001394  System.String ToString()
  RVA=0x0A97F860  token=0x6001395  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.GlobalBlackboard
TYPE:  class
TOKEN: 0x2000441
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.Blackboard
IMPLEMENTS: NodeCanvas.Framework.IGlobalBlackboard NodeCanvas.Framework.IBlackboard
FIELDS:
  private           System.String                   _UID  // 0x60
  private           System.String                   _identifier  // 0x68
  private           NodeCanvas.Framework.GlobalBlackboard.SingletonMode_singletonMode  // 0x70
  private           System.Boolean                  _dontDestroyOnLoad  // 0x74
  private   static  System.Collections.Generic.List<NodeCanvas.Framework.GlobalBlackboard>_allGlobals  // static @ 0x0
PROPERTIES:
  identifier  get=0x04D86230
  UID  get=0x04D86280
  name  get=0x04D86230
METHODS:
  RVA=0x0A980AB4  token=0x6001399  System.Collections.Generic.IEnumerable<NodeCanvas.Framework.GlobalBlackboard> GetAll()
  RVA=0x0A980918  token=0x600139A  NodeCanvas.Framework.GlobalBlackboard Create()
  RVA=0x0A9809C0  token=0x600139B  NodeCanvas.Framework.GlobalBlackboard Find(System.String name)
  RVA=0x0A980B74  token=0x600139C  System.Void OnEnable()
  RVA=0x0A980B04  token=0x600139D  System.Void OnDisable()
  RVA=0x0A980D48  token=0x600139E  System.Void OnValidate()
  RVA=0x04D86230  token=0x600139F  System.String ToString()
  RVA=0x012081B0  token=0x60013A0  System.Boolean IsPrefabAsset()
  RVA=0x0A980F44  token=0x60013A1  System.Void .ctor()
  RVA=0x0A980EB8  token=0x60013A2  System.Void .cctor()
END_CLASS

CLASS: NodeCanvas.Framework.IBlackboard
TYPE:  interface
TOKEN: 0x2000444
FIELDS:
PROPERTIES:
  identifier  get=-1  // abstract
  parent  get=-1  // abstract
  variables  get=-1  // abstract  set=-1  // abstract
  propertiesBindTarget  get=-1  // abstract
  unityContextObject  get=-1  // abstract
  independantVariablesFieldName  get=-1  // abstract
EVENTS:
  onVariableAdded  add=add_onVariableAdded  remove=remove_onVariableAdded
  onVariableRemoved  add=add_onVariableRemoved  remove=remove_onVariableRemoved
METHODS:
  RVA=-1  // abstract  token=0x60013B0  System.Void TryInvokeOnVariableAdded(NodeCanvas.Framework.Variable variable)
  RVA=-1  // abstract  token=0x60013B1  System.Void TryInvokeOnVariableRemoved(NodeCanvas.Framework.Variable variable)
END_CLASS

CLASS: NodeCanvas.Framework.IGlobalBlackboard
TYPE:  interface
TOKEN: 0x2000445
IMPLEMENTS: NodeCanvas.Framework.IBlackboard
FIELDS:
PROPERTIES:
  UID  get=-1  // abstract
METHODS:
END_CLASS

CLASS: NodeCanvas.Framework.IBlackboardExtensions
TYPE:  static class
TOKEN: 0x2000446
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0A9821F8  token=0x60013B3  NodeCanvas.Framework.IBlackboard GetRoot(NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A982178  token=0x60013B4  System.Collections.Generic.IEnumerable<NodeCanvas.Framework.IBlackboard> GetAllParents(NodeCanvas.Framework.IBlackboard blackboard, System.Boolean includeSelf)
  RVA=0x0A982238  token=0x60013B5  System.Boolean IsPartOf(NodeCanvas.Framework.IBlackboard blackboard, NodeCanvas.Framework.IBlackboard child)
  RVA=-1  // generic def  token=0x60013B6  NodeCanvas.Framework.Variable<T> AddVariable(NodeCanvas.Framework.IBlackboard blackboard, System.String varName, T value)
  RVA=-1  // generic def  token=0x60013B7  NodeCanvas.Framework.Variable<T> AddVariable(NodeCanvas.Framework.IBlackboard blackboard, System.String varName)
  RVA=0x0306E310  token=0x60013B8  NodeCanvas.Framework.Variable AddVariable(NodeCanvas.Framework.IBlackboard blackboard, System.String varName, System.Object value)
  RVA=0x0306E9F0  token=0x60013B9  NodeCanvas.Framework.Variable AddVariable(NodeCanvas.Framework.IBlackboard blackboard, System.String varName, System.Type type)
  RVA=0x0A9826A4  token=0x60013BA  NodeCanvas.Framework.Variable RemoveVariable(NodeCanvas.Framework.IBlackboard blackboard, System.String varName)
  RVA=-1  // generic def  token=0x60013BB  T GetVariableValue(NodeCanvas.Framework.IBlackboard blackboard, System.String varName)
  RVA=0x0A9827D0  token=0x60013BC  NodeCanvas.Framework.Variable SetVariableValue(NodeCanvas.Framework.IBlackboard blackboard, System.String varName, System.Object value)
  RVA=0x03114EC0  token=0x60013BD  System.Void InitializePropertiesBinding(NodeCanvas.Framework.IBlackboard blackboard, UnityEngine.Component target, System.Boolean callSetter)
  RVA=-1  // generic def  token=0x60013BE  NodeCanvas.Framework.Variable<T> GetVariable(NodeCanvas.Framework.IBlackboard blackboard, System.String varName)
  RVA=0x0306F680  token=0x60013BF  NodeCanvas.Framework.Variable GetVariable(NodeCanvas.Framework.IBlackboard blackboard, System.String varName, System.Type ofType)
  RVA=0x0306F7D0  token=0x60013C0  NodeCanvas.Framework.Variable GetVariableByID(NodeCanvas.Framework.IBlackboard blackboard, System.String ID)
  RVA=0x0306C510  token=0x60013C1  System.Collections.Generic.IEnumerable<NodeCanvas.Framework.Variable> GetVariables(NodeCanvas.Framework.IBlackboard blackboard, System.Type ofType)
  RVA=0x0A981F9C  token=0x60013C2  NodeCanvas.Framework.Variable ChangeVariableType(NodeCanvas.Framework.IBlackboard blackboard, NodeCanvas.Framework.Variable target, System.Type newType)
  RVA=0x0A982278  token=0x60013C3  System.Void OverwriteFrom(NodeCanvas.Framework.IBlackboard blackboard, NodeCanvas.Framework.IBlackboard sourceBlackboard, System.Boolean removeMissingVariables)
END_CLASS

CLASS: NodeCanvas.Framework.BlackboardHelpers
TYPE:  static class
TOKEN: 0x2000449
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Concurrent.ConcurrentDictionary<System.Type,System.Type>s_variableTypeCache  // static @ 0x0
METHODS:
  RVA=0x0306DAA0  token=0x60013D6  System.Type CreateVariableType(System.Type valueType)
  RVA=0x039D9230  token=0x60013D7  System.Void .cctor()
END_CLASS

CLASS: NodeCanvas.Framework.Variable
TYPE:  abstract class
TOKEN: 0x200044B
SIZE:  0x40
FIELDS:
  private           System.String                   _name  // 0x10
  private           System.String                   _id  // 0x18
  private           System.Boolean                  _isPublic  // 0x20
  private           System.Boolean                  _debugBoundValue  // 0x21
  private           System.Action<System.String>    onNameChanged  // 0x28
  private           System.Action<System.Object>    onValueChanged  // 0x30
  private           System.Action                   onDestroy  // 0x38
PROPERTIES:
  name  get=0x02B2ECC0  set=0x0306D9F0
  ID  get=0x0306FC40
  value  get=0x05390F1C  set=0x05390F08
  isExposedPublic  get=0x0A98D5D8  set=0x04D866D0
  debugBoundValue  get=0x04D86850  set=0x04D86860
  isPropertyBound  get=0x03115190
  isDataBound  get=-1  // abstract
  varType  get=-1  // abstract
  propertyPath  get=-1  // abstract  set=-1  // abstract
EVENTS:
  onNameChanged  add=add_onNameChanged  remove=remove_onNameChanged
  onValueChanged  add=add_onValueChanged  remove=remove_onValueChanged
  onDestroy  add=add_onDestroy  remove=remove_onDestroy
METHODS:
  RVA=-1  // abstract  token=0x60013EF  System.Void BindProperty(System.Reflection.MemberInfo prop, UnityEngine.GameObject target)
  RVA=-1  // abstract  token=0x60013F0  System.Void UnBind()
  RVA=-1  // abstract  token=0x60013F1  System.Void InitializePropertyBinding(UnityEngine.GameObject go, System.Boolean callSetter)
  RVA=-1  // abstract  token=0x60013F2  System.Object GetValueBoxed()
  RVA=-1  // abstract  token=0x60013F3  System.Void SetValueBoxed(System.Object value)
  RVA=-1  // abstract  token=0x60013F4  System.Void ResetDefaultValue()
  RVA=-1  // abstract  token=0x60013F5  System.Void RecordDefaultValue()
  RVA=0x03CBE700  token=0x60013F6  System.Void .ctor()
  RVA=0x032AF420  token=0x60013F7  System.Void .ctor(System.String name, System.String ID)
  RVA=0x0A98D404  token=0x60013F8  System.Void OnDestroy()
  RVA=0x0A98D2F8  token=0x60013F9  NodeCanvas.Framework.Variable Duplicate(NodeCanvas.Framework.IBlackboard targetBB)
  RVA=0x04486EE0  token=0x60013FA  System.Boolean HasValueChangeEvent()
  RVA=0x0A98D418  token=0x60013FB  System.Void TryInvokeValueChangeEvent(System.Object value)
  RVA=0x0306E9D0  token=0x60013FC  System.Boolean CanConvertTo(System.Type toType)
  RVA=0x035CD980  token=0x60013FD  System.Func<System.Object> GetGetConverter(System.Type toType)
  RVA=0x0A98D2E0  token=0x60013FE  System.Boolean CanConvertFrom(System.Type fromType)
  RVA=0x0321C5F0  token=0x60013FF  System.Action<System.Object> GetSetConverter(System.Type fromType)
  RVA=0x02B2ECC0  token=0x6001400  System.String ToString()
END_CLASS

CLASS: NodeCanvas.Framework.Variable`1
TYPE:  class
TOKEN: 0x200044E
EXTENDS: NodeCanvas.Framework.Variable
FIELDS:
  private           T                               _value  // 0x0
  private           System.String                   _propertyPath  // 0x0
  private           System.Func<T>                  getter  // 0x0
  private           System.Action<T>                setter  // 0x0
  private           T                               m_originalValue  // 0x0
  private           System.Boolean                  m_hasOriginalValue  // 0x0
PROPERTIES:
  varType  get=-1  // not resolved
  isDataBound  get=-1  // not resolved
  propertyPath  get=-1  // not resolved  set=-1  // not resolved
  value  get=-1  // not resolved  set=-1  // not resolved
EVENTS:
  getter  add=add_getter  remove=remove_getter
  setter  add=add_setter  remove=remove_setter
METHODS:
  RVA=-1  // not resolved  token=0x6001411  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6001412  System.Void .ctor(System.String name, System.String ID)
  RVA=-1  // not resolved  token=0x6001413  System.Object GetValueBoxed()
  RVA=-1  // not resolved  token=0x6001414  System.Void SetValueBoxed(System.Object newValue)
  RVA=-1  // not resolved  token=0x6001415  T GetValue()
  RVA=-1  // not resolved  token=0x6001416  System.Void SetValue(T newValue)
  RVA=-1  // not resolved  token=0x6001417  System.Void ResetDefaultValue()
  RVA=-1  // not resolved  token=0x6001418  System.Void RecordDefaultValue()
  RVA=-1  // not resolved  token=0x6001419  System.Void BindProperty(System.Reflection.MemberInfo prop, UnityEngine.GameObject target)
  RVA=-1  // not resolved  token=0x600141A  System.Void BindGetSet(System.Func<T> _get, System.Action<T> _set)
  RVA=-1  // not resolved  token=0x600141B  System.Void UnBind()
  RVA=-1  // not resolved  token=0x600141C  System.Void InitializePropertyBinding(UnityEngine.GameObject go, System.Boolean callSetter)
END_CLASS

CLASS: NodeCanvas.Framework.VariableSeperator
TYPE:  class
TOKEN: 0x2000453
SIZE:  0x18
FIELDS:
  private           System.Boolean                  <isEditingName>k__BackingField  // 0x10
PROPERTIES:
  isEditingName  get=0x015EFCE0  set=0x04D86130
METHODS:
  RVA=0x041E1670  token=0x6001429  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.GraphLoadData
TYPE:  sealed struct
TOKEN: 0x2000454
SIZE:  0x40
FIELDS:
  public            NodeCanvas.Framework.Internal.GraphSourcesource  // 0x10
  public            System.String                   json  // 0x18
  public            System.Collections.Generic.List<UnityEngine.Object>references  // 0x20
  public            UnityEngine.Component           agent  // 0x28
  public            NodeCanvas.Framework.IBlackboardparentBlackboard  // 0x30
  public            System.Boolean                  preInitializeSubGraphs  // 0x38
METHODS:
END_CLASS

CLASS: NodeCanvas.Framework.Internal.GraphSource
TYPE:  class
TOKEN: 0x2000455
SIZE:  0x78
IMPLEMENTS: ParadoxNotion.Serialization.FullSerializer.ISerializationCollector ParadoxNotion.Serialization.FullSerializer.ISerializationCollectable
FIELDS:
  private           System.Collections.Generic.List<NodeCanvas.Framework.Task>m_allTasks  // 0x10
  public            System.Collections.Generic.List<NodeCanvas.Framework.BBParameter>m_allParameters  // 0x18
  public    static  System.Single                   FRAMEWORK_VERSION  // const
  private           System.Single                   _version  // 0x20
  private           System.String                   _category  // 0x28
  private           System.String                   _comments  // 0x30
  private           UnityEngine.Vector2             _translation  // 0x38
  private           System.Single                   _zoomFactor  // 0x40
  private           System.String                   _type  // 0x48
  private           System.Collections.Generic.List<NodeCanvas.Framework.Node>_nodes  // 0x50
  private           System.Collections.Generic.List<NodeCanvas.Framework.Connection>_connections  // 0x58
  private           System.Collections.Generic.List<NodeCanvas.Framework.CanvasGroup>_canvasGroups  // 0x60
  private           NodeCanvas.Framework.Internal.BlackboardSource_localBlackboard  // 0x68
  private           System.Object                   _derivedData  // 0x70
PROPERTIES:
  allTasks  get=0x02B2ECC0  set=0x053908C0
  allParameters  get=0x0385B100  set=0x05392C40
  version  get=0x011EC8A0  set=0x011EC9E0
  category  get=0x04D86240  set=0x02FDB880
  comments  get=0x011F36E0  set=0x05396200
  translation  get=0x04DA6810  set=0x04D865B0
  zoomFactor  get=0x04D863F0  set=0x04D86470
  type  get=0x04D86200  set=0x035A41A0
  nodes  get=0x04D86270  set=0x02FDAEA0
  connections  get=0x02E56440  set=0x036E5590
  canvasGroups  get=0x04D86280  set=0x063CC164
  localBlackboard  get=0x04D86230  set=0x0485F170
  derivedData  get=0x04D862B0  set=0x063AD760
METHODS:
  RVA=0x03CBDA80  token=0x6001430  System.Void ParadoxNotion.Serialization.FullSerializer.ISerializationCollector.OnPush(ParadoxNotion.Serialization.FullSerializer.ISerializationCollector parent)
  RVA=0x0360FBE0  token=0x6001431  System.Void ParadoxNotion.Serialization.FullSerializer.ISerializationCollector.OnCollect(ParadoxNotion.Serialization.FullSerializer.ISerializationCollectable child, System.Int32 depth)
  RVA=0x041E1670  token=0x6001432  System.Void ParadoxNotion.Serialization.FullSerializer.ISerializationCollector.OnPop(ParadoxNotion.Serialization.FullSerializer.ISerializationCollector parent)
  RVA=0x03CBE9F0  token=0x6001449  System.Void .ctor()
  RVA=0x0A981D0C  token=0x600144A  NodeCanvas.Framework.Internal.GraphSource Pack(NodeCanvas.Framework.Graph graph)
  RVA=0x0306EB20  token=0x600144B  NodeCanvas.Framework.Internal.GraphSource Unpack(NodeCanvas.Framework.Graph graph)
  RVA=0x0A981F34  token=0x600144C  NodeCanvas.Framework.Internal.GraphSource SetMetaData(NodeCanvas.Framework.Internal.GraphSource source)
  RVA=0x0A981EEC  token=0x600144D  System.Void PurgeRedundantReferences()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.MissingConnection
TYPE:  sealed class
TOKEN: 0x2000456
SIZE:  0x40
EXTENDS: NodeCanvas.Framework.Connection
IMPLEMENTS: ParadoxNotion.Serialization.IMissingRecoverable
FIELDS:
  private           System.String                   _missingType  // 0x30
  private           System.String                   _recoveryState  // 0x38
PROPERTIES:
  ParadoxNotion.Serialization.IMissingRecoverable.missingType  get=0x011F36E0  set=0x05396200
  ParadoxNotion.Serialization.IMissingRecoverable.recoveryState  get=0x04D85A50  set=0x046A91B0
METHODS:
  RVA=0x04D92320  token=0x6001452  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.MissingNode
TYPE:  sealed class
TOKEN: 0x2000457
SIZE:  0xA8
EXTENDS: NodeCanvas.Framework.Node
IMPLEMENTS: ParadoxNotion.Serialization.IMissingRecoverable
FIELDS:
  private           System.String                   _missingType  // 0x98
  private           System.String                   _recoveryState  // 0xa0
PROPERTIES:
  ParadoxNotion.Serialization.IMissingRecoverable.missingType  get=0x04D85EE0  set=0x06352D0C
  ParadoxNotion.Serialization.IMissingRecoverable.recoveryState  get=0x04D86210  set=0x05391334
  name  get=0x0A98340C
  outConnectionType  get=0x011EC580
  maxInConnections  get=0x011EC580
  maxOutConnections  get=0x011EC580
  allowAsPrime  get=0x012081B0
  canSelfConnect  get=0x012081B0
  commentsAlignment  get=0x02BBE540
  iconAlignment  get=0x011EC580
METHODS:
  RVA=0x0489BC70  token=0x600145F  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.ReflectedAction
TYPE:  class
TOKEN: 0x2000458
SIZE:  0x20
EXTENDS: NodeCanvas.Framework.Internal.ReflectedActionWrapper
FIELDS:
  private           ParadoxNotion.ActionCall        call  // 0x18
METHODS:
  RVA=0x0A98543C  token=0x6001460  NodeCanvas.Framework.BBParameter[] GetVariables()
  RVA=0x0A985470  token=0x6001461  System.Void Init(System.Object instance)
  RVA=0x0A985418  token=0x6001462  System.Void Call()
  RVA=0x041E1670  token=0x6001463  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.ReflectedAction`1
TYPE:  class
TOKEN: 0x2000459
EXTENDS: NodeCanvas.Framework.Internal.ReflectedActionWrapper
FIELDS:
  private           ParadoxNotion.ActionCall<T1>    call  // 0x0
  public            NodeCanvas.Framework.BBParameter<T1>p1  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001464  NodeCanvas.Framework.BBParameter[] GetVariables()
  RVA=-1  // not resolved  token=0x6001465  System.Void Init(System.Object instance)
  RVA=-1  // not resolved  token=0x6001466  System.Void Call()
  RVA=-1  // not resolved  token=0x6001467  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.ReflectedAction`2
TYPE:  class
TOKEN: 0x200045A
EXTENDS: NodeCanvas.Framework.Internal.ReflectedActionWrapper
FIELDS:
  private           ParadoxNotion.ActionCall<T1,T2> call  // 0x0
  public            NodeCanvas.Framework.BBParameter<T1>p1  // 0x0
  public            NodeCanvas.Framework.BBParameter<T2>p2  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001468  NodeCanvas.Framework.BBParameter[] GetVariables()
  RVA=-1  // not resolved  token=0x6001469  System.Void Init(System.Object instance)
  RVA=-1  // not resolved  token=0x600146A  System.Void Call()
  RVA=-1  // not resolved  token=0x600146B  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.ReflectedAction`3
TYPE:  class
TOKEN: 0x200045B
EXTENDS: NodeCanvas.Framework.Internal.ReflectedActionWrapper
FIELDS:
  private           ParadoxNotion.ActionCall<T1,T2,T3>call  // 0x0
  public            NodeCanvas.Framework.BBParameter<T1>p1  // 0x0
  public            NodeCanvas.Framework.BBParameter<T2>p2  // 0x0
  public            NodeCanvas.Framework.BBParameter<T3>p3  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600146C  NodeCanvas.Framework.BBParameter[] GetVariables()
  RVA=-1  // not resolved  token=0x600146D  System.Void Init(System.Object instance)
  RVA=-1  // not resolved  token=0x600146E  System.Void Call()
  RVA=-1  // not resolved  token=0x600146F  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.ReflectedAction`4
TYPE:  class
TOKEN: 0x200045C
EXTENDS: NodeCanvas.Framework.Internal.ReflectedActionWrapper
FIELDS:
  private           ParadoxNotion.ActionCall<T1,T2,T3,T4>call  // 0x0
  public            NodeCanvas.Framework.BBParameter<T1>p1  // 0x0
  public            NodeCanvas.Framework.BBParameter<T2>p2  // 0x0
  public            NodeCanvas.Framework.BBParameter<T3>p3  // 0x0
  public            NodeCanvas.Framework.BBParameter<T4>p4  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001470  NodeCanvas.Framework.BBParameter[] GetVariables()
  RVA=-1  // not resolved  token=0x6001471  System.Void Init(System.Object instance)
  RVA=-1  // not resolved  token=0x6001472  System.Void Call()
  RVA=-1  // not resolved  token=0x6001473  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.ReflectedAction`5
TYPE:  class
TOKEN: 0x200045D
EXTENDS: NodeCanvas.Framework.Internal.ReflectedActionWrapper
FIELDS:
  private           ParadoxNotion.ActionCall<T1,T2,T3,T4,T5>call  // 0x0
  public            NodeCanvas.Framework.BBParameter<T1>p1  // 0x0
  public            NodeCanvas.Framework.BBParameter<T2>p2  // 0x0
  public            NodeCanvas.Framework.BBParameter<T3>p3  // 0x0
  public            NodeCanvas.Framework.BBParameter<T4>p4  // 0x0
  public            NodeCanvas.Framework.BBParameter<T5>p5  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001474  NodeCanvas.Framework.BBParameter[] GetVariables()
  RVA=-1  // not resolved  token=0x6001475  System.Void Init(System.Object instance)
  RVA=-1  // not resolved  token=0x6001476  System.Void Call()
  RVA=-1  // not resolved  token=0x6001477  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.ReflectedAction`6
TYPE:  class
TOKEN: 0x200045E
EXTENDS: NodeCanvas.Framework.Internal.ReflectedActionWrapper
FIELDS:
  private           ParadoxNotion.ActionCall<T1,T2,T3,T4,T5,T6>call  // 0x0
  public            NodeCanvas.Framework.BBParameter<T1>p1  // 0x0
  public            NodeCanvas.Framework.BBParameter<T2>p2  // 0x0
  public            NodeCanvas.Framework.BBParameter<T3>p3  // 0x0
  public            NodeCanvas.Framework.BBParameter<T4>p4  // 0x0
  public            NodeCanvas.Framework.BBParameter<T5>p5  // 0x0
  public            NodeCanvas.Framework.BBParameter<T6>p6  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001478  NodeCanvas.Framework.BBParameter[] GetVariables()
  RVA=-1  // not resolved  token=0x6001479  System.Void Init(System.Object instance)
  RVA=-1  // not resolved  token=0x600147A  System.Void Call()
  RVA=-1  // not resolved  token=0x600147B  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.ReflectedFunction`1
TYPE:  class
TOKEN: 0x200045F
EXTENDS: NodeCanvas.Framework.Internal.ReflectedFunctionWrapper
FIELDS:
  private           ParadoxNotion.FunctionCall<TResult>call  // 0x0
  public            NodeCanvas.Framework.BBParameter<TResult>result  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600147C  NodeCanvas.Framework.BBParameter[] GetVariables()
  RVA=-1  // not resolved  token=0x600147D  System.Void Init(System.Object instance)
  RVA=-1  // not resolved  token=0x600147E  System.Object Call()
  RVA=-1  // not resolved  token=0x600147F  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.ReflectedFunction`2
TYPE:  class
TOKEN: 0x2000460
EXTENDS: NodeCanvas.Framework.Internal.ReflectedFunctionWrapper
FIELDS:
  private           ParadoxNotion.FunctionCall<T1,TResult>call  // 0x0
  public            NodeCanvas.Framework.BBParameter<T1>p1  // 0x0
  public            NodeCanvas.Framework.BBParameter<TResult>result  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001480  NodeCanvas.Framework.BBParameter[] GetVariables()
  RVA=-1  // not resolved  token=0x6001481  System.Void Init(System.Object instance)
  RVA=-1  // not resolved  token=0x6001482  System.Object Call()
  RVA=-1  // not resolved  token=0x6001483  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.ReflectedFunction`3
TYPE:  class
TOKEN: 0x2000461
EXTENDS: NodeCanvas.Framework.Internal.ReflectedFunctionWrapper
FIELDS:
  private           ParadoxNotion.FunctionCall<T1,T2,TResult>call  // 0x0
  public            NodeCanvas.Framework.BBParameter<T1>p1  // 0x0
  public            NodeCanvas.Framework.BBParameter<T2>p2  // 0x0
  public            NodeCanvas.Framework.BBParameter<TResult>result  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001484  NodeCanvas.Framework.BBParameter[] GetVariables()
  RVA=-1  // not resolved  token=0x6001485  System.Void Init(System.Object instance)
  RVA=-1  // not resolved  token=0x6001486  System.Object Call()
  RVA=-1  // not resolved  token=0x6001487  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.ReflectedFunction`4
TYPE:  class
TOKEN: 0x2000462
EXTENDS: NodeCanvas.Framework.Internal.ReflectedFunctionWrapper
FIELDS:
  private           ParadoxNotion.FunctionCall<T1,T2,T3,TResult>call  // 0x0
  public            NodeCanvas.Framework.BBParameter<T1>p1  // 0x0
  public            NodeCanvas.Framework.BBParameter<T2>p2  // 0x0
  public            NodeCanvas.Framework.BBParameter<T3>p3  // 0x0
  public            NodeCanvas.Framework.BBParameter<TResult>result  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001488  NodeCanvas.Framework.BBParameter[] GetVariables()
  RVA=-1  // not resolved  token=0x6001489  System.Void Init(System.Object instance)
  RVA=-1  // not resolved  token=0x600148A  System.Object Call()
  RVA=-1  // not resolved  token=0x600148B  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.ReflectedFunction`5
TYPE:  class
TOKEN: 0x2000463
EXTENDS: NodeCanvas.Framework.Internal.ReflectedFunctionWrapper
FIELDS:
  private           ParadoxNotion.FunctionCall<T1,T2,T3,T4,TResult>call  // 0x0
  public            NodeCanvas.Framework.BBParameter<T1>p1  // 0x0
  public            NodeCanvas.Framework.BBParameter<T2>p2  // 0x0
  public            NodeCanvas.Framework.BBParameter<T3>p3  // 0x0
  public            NodeCanvas.Framework.BBParameter<T4>p4  // 0x0
  public            NodeCanvas.Framework.BBParameter<TResult>result  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600148C  NodeCanvas.Framework.BBParameter[] GetVariables()
  RVA=-1  // not resolved  token=0x600148D  System.Void Init(System.Object instance)
  RVA=-1  // not resolved  token=0x600148E  System.Object Call()
  RVA=-1  // not resolved  token=0x600148F  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.ReflectedFunction`6
TYPE:  class
TOKEN: 0x2000464
EXTENDS: NodeCanvas.Framework.Internal.ReflectedFunctionWrapper
FIELDS:
  private           ParadoxNotion.FunctionCall<T1,T2,T3,T4,T5,TResult>call  // 0x0
  public            NodeCanvas.Framework.BBParameter<T1>p1  // 0x0
  public            NodeCanvas.Framework.BBParameter<T2>p2  // 0x0
  public            NodeCanvas.Framework.BBParameter<T3>p3  // 0x0
  public            NodeCanvas.Framework.BBParameter<T4>p4  // 0x0
  public            NodeCanvas.Framework.BBParameter<T5>p5  // 0x0
  public            NodeCanvas.Framework.BBParameter<TResult>result  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001490  NodeCanvas.Framework.BBParameter[] GetVariables()
  RVA=-1  // not resolved  token=0x6001491  System.Void Init(System.Object instance)
  RVA=-1  // not resolved  token=0x6001492  System.Object Call()
  RVA=-1  // not resolved  token=0x6001493  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.ReflectedFunction`7
TYPE:  class
TOKEN: 0x2000465
EXTENDS: NodeCanvas.Framework.Internal.ReflectedFunctionWrapper
FIELDS:
  private           ParadoxNotion.FunctionCall<T1,T2,T3,T4,T5,T6,TResult>call  // 0x0
  public            NodeCanvas.Framework.BBParameter<T1>p1  // 0x0
  public            NodeCanvas.Framework.BBParameter<T2>p2  // 0x0
  public            NodeCanvas.Framework.BBParameter<T3>p3  // 0x0
  public            NodeCanvas.Framework.BBParameter<T4>p4  // 0x0
  public            NodeCanvas.Framework.BBParameter<T5>p5  // 0x0
  public            NodeCanvas.Framework.BBParameter<T6>p6  // 0x0
  public            NodeCanvas.Framework.BBParameter<TResult>result  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6001494  NodeCanvas.Framework.BBParameter[] GetVariables()
  RVA=-1  // not resolved  token=0x6001495  System.Void Init(System.Object instance)
  RVA=-1  // not resolved  token=0x6001496  System.Object Call()
  RVA=-1  // not resolved  token=0x6001497  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.ReflectedWrapper
TYPE:  abstract class
TOKEN: 0x2000466
SIZE:  0x18
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper
FIELDS:
  protected         ParadoxNotion.Serialization.SerializedMethodInfo_targetMethod  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6001498  System.Void .ctor()
  RVA=0x0A9858EC  token=0x6001499  NodeCanvas.Framework.Internal.ReflectedWrapper Create(System.Reflection.MethodInfo method, NodeCanvas.Framework.IBlackboard bb)
  RVA=0x02B2ECC0  token=0x600149A  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A9859B8  token=0x600149B  System.Void SetVariablesBB(NodeCanvas.Framework.IBlackboard bb)
  RVA=0x02B2ECC0  token=0x600149C  ParadoxNotion.Serialization.SerializedMethodInfo GetSerializedMethod()
  RVA=0x0A985994  token=0x600149D  System.Reflection.MethodInfo GetMethod()
  RVA=0x0A9859A4  token=0x600149E  System.Boolean HasChanged()
  RVA=0x0A9858CC  token=0x600149F  System.String AsString()
  RVA=0x0A985A1C  token=0x60014A0  System.String ToString()
  RVA=-1  // abstract  token=0x60014A1  NodeCanvas.Framework.BBParameter[] GetVariables()
  RVA=-1  // abstract  token=0x60014A2  System.Void Init(System.Object instance)
END_CLASS

CLASS: NodeCanvas.Framework.Internal.ReflectedActionWrapper
TYPE:  abstract class
TOKEN: 0x2000467
SIZE:  0x18
EXTENDS: NodeCanvas.Framework.Internal.ReflectedWrapper
FIELDS:
METHODS:
  RVA=0x0A985078  token=0x60014A3  NodeCanvas.Framework.Internal.ReflectedActionWrapper Create(System.Reflection.MethodInfo method, NodeCanvas.Framework.IBlackboard bb)
  RVA=-1  // abstract  token=0x60014A4  System.Void Call()
  RVA=0x041E1670  token=0x60014A5  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.ReflectedFunctionWrapper
TYPE:  abstract class
TOKEN: 0x2000468
SIZE:  0x18
EXTENDS: NodeCanvas.Framework.Internal.ReflectedWrapper
FIELDS:
METHODS:
  RVA=0x0A9854F4  token=0x60014A6  NodeCanvas.Framework.Internal.ReflectedFunctionWrapper Create(System.Reflection.MethodInfo method, NodeCanvas.Framework.IBlackboard bb)
  RVA=-1  // abstract  token=0x60014A7  System.Object Call()
  RVA=0x041E1670  token=0x60014A8  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.MissingAction
TYPE:  class
TOKEN: 0x2000469
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
IMPLEMENTS: ParadoxNotion.Serialization.IMissingRecoverable
FIELDS:
  private           System.String                   _missingType  // 0x68
  private           System.String                   _recoveryState  // 0x70
PROPERTIES:
  ParadoxNotion.Serialization.IMissingRecoverable.missingType  get=0x04D86230  set=0x0485F170
  ParadoxNotion.Serialization.IMissingRecoverable.recoveryState  get=0x04D862B0  set=0x063AD760
  info  get=0x0A98330C
METHODS:
  RVA=0x04D8E030  token=0x60014AE  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.MissingCondition
TYPE:  class
TOKEN: 0x200046A
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ConditionTask
IMPLEMENTS: ParadoxNotion.Serialization.IMissingRecoverable
FIELDS:
  private           System.String                   _missingType  // 0x68
  private           System.String                   _recoveryState  // 0x70
PROPERTIES:
  ParadoxNotion.Serialization.IMissingRecoverable.missingType  get=0x04D86230  set=0x0485F170
  ParadoxNotion.Serialization.IMissingRecoverable.recoveryState  get=0x04D862B0  set=0x063AD760
  info  get=0x0A9833A0
METHODS:
  RVA=0x04D8E020  token=0x60014B4  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.TaskAgentParameter
TYPE:  sealed class
TOKEN: 0x200046B
SIZE:  0x58
EXTENDS: NodeCanvas.Framework.BBParameter`1
FIELDS:
  private           System.Type                     _type  // 0x50
PROPERTIES:
  varType  get=0x0A98AF5C
  value  get=0x0A98AEB4  set=0x046A91B0
METHODS:
  RVA=0x0A98AD98  token=0x60014B8  System.Object GetValueBoxed()
  RVA=0x0A98AE38  token=0x60014B9  System.Void SetValueBoxed(System.Object newValue)
  RVA=0x0A98ADA0  token=0x60014BA  System.Void SetType(System.Type newType)
  RVA=0x0A98AE8C  token=0x60014BB  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.BBMappingParameter
TYPE:  class
TOKEN: 0x200046C
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.Internal.BBObjectParameter
FIELDS:
  private           System.String                   _targetSubGraphVariableID  // 0x60
  private           System.Boolean                  _canRead  // 0x68
  private           System.Boolean                  _canWrite  // 0x69
PROPERTIES:
  targetSubGraphVariableID  get=0x04D86280
  canRead  get=0x04D86B40  set=0x04D86BA0
  canWrite  get=0x04D86B10  set=0x04D86B70
METHODS:
  RVA=0x0A97E034  token=0x60014C1  System.Void .ctor()
  RVA=0x0A97E03C  token=0x60014C2  System.Void .ctor(NodeCanvas.Framework.Variable subVariable)
END_CLASS

CLASS: NodeCanvas.Framework.Internal.BBObjectParameter
TYPE:  class
TOKEN: 0x200046D
SIZE:  0x60
EXTENDS: NodeCanvas.Framework.BBParameter`1
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           System.String                   _type  // 0x50
  private           System.Type                     <type>k__BackingField  // 0x58
PROPERTIES:
  type  get=0x02E56440  set=0x036E5590
  varType  get=0x045E60D0
METHODS:
  RVA=0x0A97E1A8  token=0x60014C3  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x038F16C0  token=0x60014C4  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x0A97E2EC  token=0x60014C8  System.Void .ctor()
  RVA=0x0A97E2A8  token=0x60014C9  System.Void .ctor(System.Type t)
  RVA=0x0A97E218  token=0x60014CA  System.Void .ctor(NodeCanvas.Framework.BBParameter source)
  RVA=0x0A97E09C  token=0x60014CB  System.Void SetType(System.Type t)
END_CLASS

CLASS: NodeCanvas.Framework.Internal.BlackboardSource
TYPE:  class
TOKEN: 0x200046E
SIZE:  0x40
IMPLEMENTS: NodeCanvas.Framework.IBlackboard
FIELDS:
  private           System.Collections.Generic.Dictionary<System.String,NodeCanvas.Framework.Variable>_variables  // 0x10
  private           System.Action<NodeCanvas.Framework.Variable>onVariableAdded  // 0x18
  private           System.Action<NodeCanvas.Framework.Variable>onVariableRemoved  // 0x20
  private           NodeCanvas.Framework.IBlackboard<parent>k__BackingField  // 0x28
  private           UnityEngine.Object              <unityContextObject>k__BackingField  // 0x30
  private           UnityEngine.Component           <propertiesBindTarget>k__BackingField  // 0x38
PROPERTIES:
  identifier  get=0x0A97EE20
  variables  get=0x02B2ECC0  set=0x053908C0
  parent  get=0x04D86240  set=0x02FDB880
  unityContextObject  get=0x011F36E0  set=0x05396200
  propertiesBindTarget  get=0x04D85A50  set=0x046A91B0
  NodeCanvas.Framework.IBlackboard.independantVariablesFieldName  get=0x011EC580
EVENTS:
  onVariableAdded  add=add_onVariableAdded  remove=remove_onVariableAdded
  onVariableRemoved  add=add_onVariableRemoved  remove=remove_onVariableRemoved
METHODS:
  RVA=0x091A9FF4  token=0x60014DA  System.Void NodeCanvas.Framework.IBlackboard.TryInvokeOnVariableAdded(NodeCanvas.Framework.Variable variable)
  RVA=0x0A97DC38  token=0x60014DB  System.Void NodeCanvas.Framework.IBlackboard.TryInvokeOnVariableRemoved(NodeCanvas.Framework.Variable variable)
  RVA=0x03CBEB90  token=0x60014DC  System.Void .ctor()
  RVA=0x0A97ED00  token=0x60014DD  System.String ToString()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.fsBBParameterProcessor
TYPE:  class
TOKEN: 0x200046F
SIZE:  0x10
EXTENDS: ParadoxNotion.Serialization.fsRecoveryProcessor`2
FIELDS:
METHODS:
  RVA=0x0A98D7A0  token=0x60014DE  System.Void OnBeforeDeserializeAfterInstanceCreation(System.Type storageType, System.Object instance, ParadoxNotion.Serialization.FullSerializer.fsData& data)
  RVA=0x0A98DA20  token=0x60014DF  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.MissingBBParameterType
TYPE:  class
TOKEN: 0x2000470
SIZE:  0x60
EXTENDS: NodeCanvas.Framework.BBParameter`1
IMPLEMENTS: ParadoxNotion.Serialization.IMissingRecoverable
FIELDS:
  private           System.String                   _missingType  // 0x50
  private           System.String                   _recoveryState  // 0x58
PROPERTIES:
  ParadoxNotion.Serialization.IMissingRecoverable.missingType  get=0x04D86270  set=0x02FDAEA0
  ParadoxNotion.Serialization.IMissingRecoverable.recoveryState  get=0x02E56440  set=0x036E5590
METHODS:
  RVA=0x0A983378  token=0x60014E4  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Framework.Internal.MissingVariableType
TYPE:  class
TOKEN: 0x2000471
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.Variable`1
IMPLEMENTS: ParadoxNotion.Serialization.IMissingRecoverable
FIELDS:
  private           System.String                   _missingType  // 0x70
  private           System.String                   _recoveryState  // 0x78
PROPERTIES:
  missingType  get=0x04D862B0  set=0x063AD760
  recoveryState  get=0x04D862A0  set=0x06402320
METHODS:
  RVA=0x0A983460  token=0x60014E9  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.StateMachines.FSM
TYPE:  class
TOKEN: 0x2000367
SIZE:  0xF0
EXTENDS: NodeCanvas.Framework.Graph
FIELDS:
  private           System.Collections.Generic.List<NodeCanvas.Framework.IUpdatable>updatableNodes  // 0xa0
  private           NodeCanvas.StateMachines.IStateCallbackReceiver[]callbackReceivers  // 0xa8
  private           System.Collections.Generic.Stack<NodeCanvas.StateMachines.FSMState>stateStack  // 0xb0
  private           System.Boolean                  enterStartStateFlag  // 0xb8
  private           System.Action<NodeCanvas.StateMachines.IState>onStateEnter  // 0xc0
  private           System.Action<NodeCanvas.StateMachines.IState>onStateUpdate  // 0xc8
  private           System.Action<NodeCanvas.StateMachines.IState>onStateExit  // 0xd0
  private           System.Action<NodeCanvas.StateMachines.IState>onStateTransition  // 0xd8
  private           NodeCanvas.StateMachines.FSMState<currentState>k__BackingField  // 0xe0
  private           NodeCanvas.StateMachines.FSMState<previousState>k__BackingField  // 0xe8
PROPERTIES:
  currentState  get=0x04D8D1C0  set=0x06AC5B08
  previousState  get=0x04D88590  set=0x06AC5B1C
  currentStateName  get=0x0A9595E4
  previousStateName  get=0x0A959610
  baseNodeType  get=0x0A959594
  requiresAgent  get=0x02FFF600
  requiresPrimeNode  get=0x02FFF600
  isTree  get=0x012081B0
  allowBlackboardOverrides  get=0x02FFF600
  canAcceptVariableDrops  get=0x012081B0
  canDelayDeserializeAndOptimizeClone  get=0x012081B0
EVENTS:
  onStateEnter  add=add_onStateEnter  remove=remove_onStateEnter
  onStateUpdate  add=add_onStateUpdate  remove=remove_onStateUpdate
  onStateExit  add=add_onStateExit  remove=remove_onStateExit
  onStateTransition  add=add_onStateTransition  remove=remove_onStateTransition
METHODS:
  RVA=0x0A958B28  token=0x6000D62  System.Void OnGraphInitialize()
  RVA=0x0A958CE8  token=0x6000D63  System.Void OnGraphStarted()
  RVA=0x0A958DE0  token=0x6000D64  System.Void OnGraphUpdate()
  RVA=0x0A958D6C  token=0x6000D65  System.Void OnGraphStoped()
  RVA=0x0A9584E0  token=0x6000D66  System.Boolean EnterState(NodeCanvas.StateMachines.FSMState newState, NodeCanvas.StateMachines.FSM.TransitionCallMode callMode)
  RVA=0x0A959128  token=0x6000D67  NodeCanvas.StateMachines.FSMState TriggerState(System.String stateName, NodeCanvas.StateMachines.FSM.TransitionCallMode callMode)
  RVA=0x0A958838  token=0x6000D68  System.String[] GetStateNames()
  RVA=0x0A958A34  token=0x6000D69  NodeCanvas.StateMachines.FSMState GetStateWithName(System.String name)
  RVA=0x0A958694  token=0x6000D6A  System.Void GatherCallbackReceivers()
  RVA=0x0A9590C4  token=0x6000D6B  NodeCanvas.StateMachines.FSMState PeekStack()
  RVA=0x0A959308  token=0x6000D6C  System.Void .ctor()
  RVA=0x0A959174  token=0x6000D6D  System.Void <GatherCallbackReceivers>b__51_0(NodeCanvas.StateMachines.IState x)
  RVA=0x0A9591F8  token=0x6000D6E  System.Void <GatherCallbackReceivers>b__51_1(NodeCanvas.StateMachines.IState x)
  RVA=0x0A959280  token=0x6000D6F  System.Void <GatherCallbackReceivers>b__51_2(NodeCanvas.StateMachines.IState x)
END_CLASS

CLASS: NodeCanvas.StateMachines.FSMConnection
TYPE:  class
TOKEN: 0x200036B
SIZE:  0x40
EXTENDS: NodeCanvas.Framework.Connection
IMPLEMENTS: NodeCanvas.Framework.ITaskAssignable`1 NodeCanvas.Framework.ITaskAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           NodeCanvas.StateMachines.FSM.TransitionCallMode_transitionCallMode  // 0x30
  private           NodeCanvas.Framework.ConditionTask_condition  // 0x38
PROPERTIES:
  condition  get=0x04D85A50  set=0x046A91B0
  task  get=0x04D85A50  set=0x0A957948
  transitionCallMode  get=0x04D864F0  set=0x04D86740
METHODS:
  RVA=0x0A957880  token=0x6000D7D  System.Void EnableCondition(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard blackboard)
  RVA=0x0A95786C  token=0x6000D7E  System.Void DisableCondition()
  RVA=0x0A957894  token=0x6000D7F  System.Void PerformTransition()
  RVA=0x04D92320  token=0x6000D80  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.StateMachines.FSMNode
TYPE:  abstract class
TOKEN: 0x200036C
SIZE:  0x98
EXTENDS: NodeCanvas.Framework.Node
FIELDS:
PROPERTIES:
  allowAsPrime  get=0x012081B0
  canSelfConnect  get=0x012081B0
  maxInConnections  get=0x04DA2BB0
  maxOutConnections  get=0x04DA2BB0
  outConnectionType  get=0x0A9579D8
  commentsAlignment  get=0x04D882B0
  iconAlignment  get=0x04D882B0
  FSM  get=0x0A95799C
METHODS:
  RVA=0x0489BC70  token=0x6000D89  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.StateMachines.FSMNodeNested`1
TYPE:  abstract class
TOKEN: 0x200036D
EXTENDS: NodeCanvas.StateMachines.FSMNode
IMPLEMENTS: NodeCanvas.Framework.IGraphAssignable`1 NodeCanvas.Framework.IGraphAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           System.Collections.Generic.List<NodeCanvas.Framework.Internal.BBMappingParameter>_variablesMap  // 0x0
  private           T                               <currentInstance>k__BackingField  // 0x0
  private           System.Collections.Generic.Dictionary<NodeCanvas.Framework.Graph,NodeCanvas.Framework.Graph><instances>k__BackingField  // 0x0
PROPERTIES:
  subGraph  get=-1  // abstract  set=-1  // abstract
  subGraphParameter  get=-1  // abstract
  currentInstance  get=-1  // not resolved  set=-1  // not resolved
  instances  get=-1  // not resolved  set=-1  // not resolved
  variablesMap  get=-1  // not resolved  set=-1  // not resolved
  NodeCanvas.Framework.IGraphAssignable.subGraph  get=-1  // not resolved  set=-1  // not resolved
  NodeCanvas.Framework.IGraphAssignable.currentInstance  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000D97  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.StateMachines.FSMOwner
TYPE:  class
TOKEN: 0x200036E
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.GraphOwner`1
FIELDS:
PROPERTIES:
  currentRootStateName  get=0x0A957EB0
  previousRootStateName  get=0x0A957F6C
  currentDeepStateName  get=0x0A957E8C
  previousDeepStateName  get=0x0A957F48
METHODS:
  RVA=0x0A957A28  token=0x6000D9C  NodeCanvas.StateMachines.IState GetCurrentState(System.Boolean includeSubFSMs)
  RVA=0x0A957B58  token=0x6000D9D  NodeCanvas.StateMachines.IState GetPreviousState(System.Boolean includeSubFSMs)
  RVA=0x0A957D94  token=0x6000D9E  NodeCanvas.StateMachines.IState TriggerState(System.String stateName)
  RVA=0x0A957DA0  token=0x6000D9F  NodeCanvas.StateMachines.IState TriggerState(System.String stateName, NodeCanvas.StateMachines.FSM.TransitionCallMode callMode)
  RVA=0x0A957CFC  token=0x6000DA0  System.String[] GetStateNames()
  RVA=0x0A957E58  token=0x6000DA1  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.StateMachines.FSMState
TYPE:  abstract class
TOKEN: 0x200036F
SIZE:  0xA0
EXTENDS: NodeCanvas.StateMachines.FSMNode
IMPLEMENTS: NodeCanvas.StateMachines.IState
FIELDS:
  private           NodeCanvas.StateMachines.FSMState.TransitionEvaluationMode_transitionEvaluation  // 0x98
  private           System.Boolean                  _hasInit  // 0x9c
PROPERTIES:
  allowAsPrime  get=0x02FFF600
  canSelfConnect  get=0x02FFF600
  maxInConnections  get=0x04DA2BB0
  maxOutConnections  get=0x04DA2BB0
  transitionEvaluation  get=0x04D862D0  set=0x04D86300
METHODS:
  RVA=0x0A9581BC  token=0x6000DA8  NodeCanvas.StateMachines.FSMConnection[] GetTransitions()
  RVA=0x0A95818C  token=0x6000DA9  System.Void Finish()
  RVA=0x0A95819C  token=0x6000DAA  System.Void Finish(System.Boolean inSuccess)
  RVA=0x0A958184  token=0x6000DAB  System.Void Finish(NodeCanvas.Framework.Status status)
  RVA=0x0A95840C  token=0x6000DAC  System.Void OnGraphPaused()
  RVA=0x0A958004  token=0x6000DAD  System.Boolean CanConnectFromSource(NodeCanvas.Framework.Node sourceNode)
  RVA=0x0A95801C  token=0x6000DAE  System.Boolean CanConnectToTarget(NodeCanvas.Framework.Node targetNode)
  RVA=0x0A9582CC  token=0x6000DAF  NodeCanvas.Framework.Status OnExecute(UnityEngine.Component agent, NodeCanvas.Framework.IBlackboard bb)
  RVA=0x0A958034  token=0x6000DB0  System.Boolean CheckTransitions()
  RVA=0x0A958420  token=0x6000DB1  System.Void OnReset()
  RVA=0x041E1670  token=0x6000DB2  System.Void OnInit()
  RVA=0x041E1670  token=0x6000DB3  System.Void OnEnter()
  RVA=0x041E1670  token=0x6000DB4  System.Void OnUpdate()
  RVA=0x041E1670  token=0x6000DB5  System.Void OnExit()
  RVA=0x041E1670  token=0x6000DB6  System.Void OnPause()
  RVA=0x0489BC70  token=0x6000DB7  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.StateMachines.FSMStateNested`1
TYPE:  abstract class
TOKEN: 0x2000371
EXTENDS: NodeCanvas.StateMachines.FSMState
IMPLEMENTS: NodeCanvas.Framework.IGraphAssignable`1 NodeCanvas.Framework.IGraphAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           System.Collections.Generic.List<NodeCanvas.Framework.Internal.BBMappingParameter>_variablesMap  // 0x0
  private           T                               <currentInstance>k__BackingField  // 0x0
  private           System.Collections.Generic.Dictionary<NodeCanvas.Framework.Graph,NodeCanvas.Framework.Graph><instances>k__BackingField  // 0x0
PROPERTIES:
  subGraph  get=-1  // abstract  set=-1  // abstract
  subGraphParameter  get=-1  // abstract
  currentInstance  get=-1  // not resolved  set=-1  // not resolved
  instances  get=-1  // not resolved  set=-1  // not resolved
  variablesMap  get=-1  // not resolved  set=-1  // not resolved
  NodeCanvas.Framework.IGraphAssignable.subGraph  get=-1  // not resolved  set=-1  // not resolved
  NodeCanvas.Framework.IGraphAssignable.currentInstance  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000DC5  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.StateMachines.IState
TYPE:  interface
TOKEN: 0x2000372
FIELDS:
PROPERTIES:
  name  get=-1  // abstract
  tag  get=-1  // abstract
  elapsedTime  get=-1  // abstract
  FSM  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000DCA  NodeCanvas.StateMachines.FSMConnection[] GetTransitions()
  RVA=-1  // abstract  token=0x6000DCB  System.Boolean CheckTransitions()
  RVA=-1  // abstract  token=0x6000DCC  System.Void Finish(System.Boolean success)
END_CLASS

CLASS: NodeCanvas.StateMachines.IStateCallbackReceiver
TYPE:  interface
TOKEN: 0x2000373
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000DCD  System.Void OnStateEnter(NodeCanvas.StateMachines.IState state)
  RVA=-1  // abstract  token=0x6000DCE  System.Void OnStateUpdate(NodeCanvas.StateMachines.IState state)
  RVA=-1  // abstract  token=0x6000DCF  System.Void OnStateExit(NodeCanvas.StateMachines.IState state)
END_CLASS

CLASS: NodeCanvas.StateMachines.ActionState
TYPE:  class
TOKEN: 0x2000374
SIZE:  0xB0
EXTENDS: NodeCanvas.StateMachines.FSMState
IMPLEMENTS: NodeCanvas.Framework.ITaskAssignable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           NodeCanvas.Framework.ActionList _actionList  // 0xa0
  private           System.Boolean                  _repeatStateActions  // 0xa8
PROPERTIES:
  task  get=0x04D86210  set=0x0A96922C
  actionList  get=0x04D86210  set=0x05391334
  repeatStateActions  get=0x04D90720  set=0x04D90750
METHODS:
  RVA=0x0A969160  token=0x6000DD6  System.Void OnValidate(NodeCanvas.Framework.Graph assignedGraph)
  RVA=0x0A969094  token=0x6000DD7  System.Void OnEnter()
  RVA=0x0A9690EC  token=0x6000DD8  System.Void OnUpdate()
  RVA=0x0A9690A4  token=0x6000DD9  System.Void OnExit()
  RVA=0x0A9690C8  token=0x6000DDA  System.Void OnPause()
  RVA=0x0489BC70  token=0x6000DDB  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.StateMachines.AnyState
TYPE:  class
TOKEN: 0x2000375
SIZE:  0xA0
EXTENDS: NodeCanvas.StateMachines.FSMNode
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  public            System.Boolean                  dontRetriggerStates  // 0x98
PROPERTIES:
  name  get=0x0A96975C
  maxInConnections  get=0x011EC580
  maxOutConnections  get=0x04DA2BB0
  allowAsPrime  get=0x012081B0
METHODS:
  RVA=0x0A9695C0  token=0x6000DE0  System.Void OnGraphStarted()
  RVA=0x0A9696A8  token=0x6000DE1  System.Void OnGraphStoped()
  RVA=0x0A9693EC  token=0x6000DE2  System.Void NodeCanvas.Framework.IUpdatable.Update()
  RVA=0x0489BC70  token=0x6000DE3  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.StateMachines.ConcurrentSubFSM
TYPE:  class
TOKEN: 0x2000376
SIZE:  0xB8
EXTENDS: NodeCanvas.StateMachines.FSMNodeNested`1
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  protected         NodeCanvas.Framework.BBParameter<NodeCanvas.StateMachines.FSM>_subFSM  // 0xb0
PROPERTIES:
  name  get=0x0A968FC0
  maxInConnections  get=0x011EC580
  maxOutConnections  get=0x011EC580
  allowAsPrime  get=0x012081B0
  subGraph  get=0x0A96A730  set=0x0A96A778
  subGraphParameter  get=0x04D86250
METHODS:
  RVA=0x0A96A61C  token=0x6000DEB  System.Void OnGraphStarted()
  RVA=0x0A96A614  token=0x6000DEC  System.Void NodeCanvas.Framework.IUpdatable.Update()
  RVA=0x0A96A6FC  token=0x6000DED  System.Void .ctor()
  RVA=0x0A95819C  token=0x6000DEE  System.Void <OnGraphStarted>b__14_0(System.Boolean result)
END_CLASS

CLASS: NodeCanvas.StateMachines.ConcurrentSubTree
TYPE:  class
TOKEN: 0x2000377
SIZE:  0xB8
EXTENDS: NodeCanvas.StateMachines.FSMNodeNested`1
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  protected         NodeCanvas.Framework.BBParameter<NodeCanvas.BehaviourTrees.BehaviourTree>_subTree  // 0xb0
PROPERTIES:
  name  get=0x0A968FC0
  maxInConnections  get=0x011EC580
  maxOutConnections  get=0x011EC580
  allowAsPrime  get=0x012081B0
  subGraph  get=0x0A96AA98  set=0x0A96AAE0
  subGraphParameter  get=0x04D86250
METHODS:
  RVA=0x0A96A984  token=0x6000DF6  System.Void OnGraphStarted()
  RVA=0x0A96A614  token=0x6000DF7  System.Void NodeCanvas.Framework.IUpdatable.Update()
  RVA=0x0A96AA64  token=0x6000DF8  System.Void .ctor()
  RVA=0x0A95819C  token=0x6000DF9  System.Void <OnGraphStarted>b__14_0(System.Boolean result)
END_CLASS

CLASS: NodeCanvas.StateMachines.EmptyState
TYPE:  class
TOKEN: 0x2000378
SIZE:  0xA0
EXTENDS: NodeCanvas.StateMachines.FSMState
FIELDS:
PROPERTIES:
  name  get=0x0A968FC0
METHODS:
  RVA=0x0A96F874  token=0x6000DFB  System.Void OnEnter()
  RVA=0x0489BC70  token=0x6000DFC  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.StateMachines.NestedBTState
TYPE:  class
TOKEN: 0x2000379
SIZE:  0xD8
EXTENDS: NodeCanvas.StateMachines.FSMStateNested`1
FIELDS:
  private           NodeCanvas.Framework.BBParameter<NodeCanvas.BehaviourTrees.BehaviourTree>_nestedBT  // 0xb8
  public            NodeCanvas.StateMachines.NestedBTState.BTExitModeexitMode  // 0xc0
  public            NodeCanvas.StateMachines.NestedBTState.BTExecutionModeexecutionMode  // 0xc4
  public            System.String                   successEvent  // 0xc8
  public            System.String                   failureEvent  // 0xd0
PROPERTIES:
  subGraph  get=0x0A976C04  set=0x0A976C4C
  subGraphParameter  get=0x04D85EF0
METHODS:
  RVA=0x0A976810  token=0x6000E00  System.Void OnEnter()
  RVA=0x0A976B04  token=0x6000E01  System.Void OnUpdate()
  RVA=0x0A976A84  token=0x6000E02  System.Void OnFinish(System.Boolean success)
  RVA=0x0A9769DC  token=0x6000E03  System.Void OnExit()
  RVA=0x0A976BC4  token=0x6000E04  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.StateMachines.NestedDTState
TYPE:  class
TOKEN: 0x200037C
SIZE:  0xD0
EXTENDS: NodeCanvas.StateMachines.FSMStateNested`1
FIELDS:
  private           NodeCanvas.Framework.BBParameter<NodeCanvas.DialogueTrees.DialogueTree>_nestedDLG  // 0xb8
  public            System.String                   successEvent  // 0xc0
  public            System.String                   failureEvent  // 0xc8
PROPERTIES:
  subGraph  get=0x0A976E88  set=0x0A976ED0
  subGraphParameter  get=0x04D85EF0
METHODS:
  RVA=0x0A976CA4  token=0x6000E08  System.Void OnEnter()
  RVA=0x0A976E00  token=0x6000E09  System.Void OnUpdate()
  RVA=0x0A976D80  token=0x6000E0A  System.Void OnExit()
  RVA=0x0A97020C  token=0x6000E0B  System.Void OnDialogueFinished(System.Boolean success)
  RVA=0x0A976E54  token=0x6000E0C  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.StateMachines.NestedFSMState
TYPE:  class
TOKEN: 0x200037D
SIZE:  0xC8
EXTENDS: NodeCanvas.StateMachines.FSMStateNested`1
FIELDS:
  private           NodeCanvas.Framework.BBParameter<NodeCanvas.StateMachines.FSM>_nestedFSM  // 0xb8
  public            NodeCanvas.StateMachines.NestedFSMState.FSMExitModeexitMode  // 0xc0
PROPERTIES:
  subGraph  get=0x0A9773DC  set=0x0A977424
  subGraphParameter  get=0x04D85EF0
METHODS:
  RVA=0x0A9771C4  token=0x6000E10  System.Void OnEnter()
  RVA=0x0A977354  token=0x6000E11  System.Void OnUpdate()
  RVA=0x0A9772AC  token=0x6000E12  System.Void OnExit()
  RVA=0x0A9773A8  token=0x6000E13  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.StateMachines.OnFSMEnter
TYPE:  class
TOKEN: 0x200037F
SIZE:  0xA8
EXTENDS: NodeCanvas.StateMachines.FSMNode
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           NodeCanvas.Framework.ConditionList_conditionList  // 0x98
  private           NodeCanvas.Framework.ActionList _actionList  // 0xa0
PROPERTIES:
  name  get=0x0A968FC0
  maxInConnections  get=0x011EC580
  maxOutConnections  get=0x011EC580
  allowAsPrime  get=0x012081B0
METHODS:
  RVA=0x0A977DF4  token=0x6000E18  System.Void OnValidate(NodeCanvas.Framework.Graph assignedGraph)
  RVA=0x0A977CCC  token=0x6000E19  System.Void OnGraphStarted()
  RVA=0x0A977DA8  token=0x6000E1A  System.Void OnGraphStoped()
  RVA=0x0A977C60  token=0x6000E1B  System.Void NodeCanvas.Framework.IUpdatable.Update()
  RVA=0x0489BC70  token=0x6000E1C  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.StateMachines.OnFSMExit
TYPE:  class
TOKEN: 0x2000380
SIZE:  0xA8
EXTENDS: NodeCanvas.StateMachines.FSMNode
FIELDS:
  private           NodeCanvas.Framework.ConditionList_conditionList  // 0x98
  private           NodeCanvas.Framework.ActionList _actionList  // 0xa0
PROPERTIES:
  name  get=0x0A968FC0
  maxInConnections  get=0x011EC580
  maxOutConnections  get=0x011EC580
  allowAsPrime  get=0x012081B0
METHODS:
  RVA=0x0A978088  token=0x6000E21  System.Void OnValidate(NodeCanvas.Framework.Graph assignedGraph)
  RVA=0x0A977F64  token=0x6000E22  System.Void OnGraphStarted()
  RVA=0x0A977FBC  token=0x6000E23  System.Void OnGraphStoped()
  RVA=0x0489BC70  token=0x6000E24  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.StateMachines.OnFSMUpdate
TYPE:  class
TOKEN: 0x2000381
SIZE:  0xA8
EXTENDS: NodeCanvas.StateMachines.FSMNode
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           NodeCanvas.Framework.ConditionList_conditionList  // 0x98
  private           NodeCanvas.Framework.ActionList _actionList  // 0xa0
PROPERTIES:
  name  get=0x0A968FC0
  maxInConnections  get=0x011EC580
  maxOutConnections  get=0x011EC580
  allowAsPrime  get=0x012081B0
METHODS:
  RVA=0x0A9782C0  token=0x6000E29  System.Void OnValidate(NodeCanvas.Framework.Graph assignedGraph)
  RVA=0x0A977F64  token=0x6000E2A  System.Void OnGraphStarted()
  RVA=0x0A977DA8  token=0x6000E2B  System.Void OnGraphStoped()
  RVA=0x0A9781F8  token=0x6000E2C  System.Void NodeCanvas.Framework.IUpdatable.Update()
  RVA=0x0489BC70  token=0x6000E2D  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.StateMachines.SuperActionState
TYPE:  class
TOKEN: 0x2000382
SIZE:  0xC0
EXTENDS: NodeCanvas.StateMachines.FSMState
FIELDS:
  private           NodeCanvas.Framework.ActionList _onEnterList  // 0xa0
  private           NodeCanvas.Framework.ActionList _onUpdateList  // 0xa8
  private           NodeCanvas.Framework.ActionList _onExitList  // 0xb0
  private           System.Boolean                  enterListFinished  // 0xb8
METHODS:
  RVA=0x0A97B0CC  token=0x6000E2E  System.Void OnValidate(NodeCanvas.Framework.Graph assignedGraph)
  RVA=0x0A97AEE4  token=0x6000E2F  System.Void OnEnter()
  RVA=0x0A97AFE4  token=0x6000E30  System.Void OnUpdate()
  RVA=0x0A97AEF8  token=0x6000E31  System.Void OnExit()
  RVA=0x0A97AFA8  token=0x6000E32  System.Void OnPause()
  RVA=0x0489BC70  token=0x6000E33  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.StateMachines.ConcurrentState
TYPE:  class
TOKEN: 0x2000383
SIZE:  0xB0
EXTENDS: NodeCanvas.StateMachines.FSMNode
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           NodeCanvas.Framework.ConditionList_conditionList  // 0x98
  private           NodeCanvas.Framework.ActionList _actionList  // 0xa0
  private           System.Boolean                  _repeatStateActions  // 0xa8
  private           System.Boolean                  done  // 0xa9
PROPERTIES:
  conditionList  get=0x04D85EE0  set=0x06352D0C
  actionList  get=0x04D86210  set=0x05391334
  repeatStateActions  get=0x04D90720  set=0x04D90750
  name  get=0x0A968FC0
  maxInConnections  get=0x011EC580
  maxOutConnections  get=0x011EC580
  allowAsPrime  get=0x012081B0
METHODS:
  RVA=0x0A96A4C8  token=0x6000E3E  System.Void OnValidate(NodeCanvas.Framework.Graph assignedGraph)
  RVA=0x0A96A414  token=0x6000E3F  System.Void OnGraphStarted()
  RVA=0x0A96A474  token=0x6000E40  System.Void OnGraphStoped()
  RVA=0x0A9690C8  token=0x6000E41  System.Void OnGraphPaused()
  RVA=0x0A96A308  token=0x6000E42  System.Void NodeCanvas.Framework.IUpdatable.Update()
  RVA=0x0489BC70  token=0x6000E43  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.StateMachines.EnterExitState
TYPE:  class
TOKEN: 0x2000384
SIZE:  0xA8
EXTENDS: NodeCanvas.StateMachines.FSMNode
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  private           NodeCanvas.Framework.ActionList _actionListEnter  // 0x98
  private           NodeCanvas.Framework.ActionList _actionListExit  // 0xa0
PROPERTIES:
  actionListEnter  get=0x04D85EE0  set=0x06352D0C
  actionListExit  get=0x04D86210  set=0x05391334
  name  get=0x0A968FC0
  maxInConnections  get=0x011EC580
  maxOutConnections  get=0x011EC580
  allowAsPrime  get=0x012081B0
METHODS:
  RVA=0x0A96FA04  token=0x6000E4C  System.Void OnValidate(NodeCanvas.Framework.Graph assignedGraph)
  RVA=0x0A96F908  token=0x6000E4D  System.Void OnGraphStarted()
  RVA=0x0A96F970  token=0x6000E4E  System.Void OnGraphStoped()
  RVA=0x0A96F89C  token=0x6000E4F  System.Void NodeCanvas.Framework.IUpdatable.Update()
  RVA=0x0489BC70  token=0x6000E50  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.StateMachines.ConcurrentSubFlowScript
TYPE:  class
TOKEN: 0x2000385
SIZE:  0xB8
EXTENDS: NodeCanvas.StateMachines.FSMNodeNested`1
IMPLEMENTS: NodeCanvas.Framework.IUpdatable NodeCanvas.Framework.IGraphElement
FIELDS:
  protected         NodeCanvas.Framework.BBParameter<FlowCanvas.FlowScript>_subFlowScript  // 0xb0
PROPERTIES:
  name  get=0x0A968FC0
  maxInConnections  get=0x011EC580
  maxOutConnections  get=0x011EC580
  allowAsPrime  get=0x012081B0
  subGraph  get=0x0A96A8E4  set=0x0A96A92C
  subGraphParameter  get=0x04D86250
METHODS:
  RVA=0x0A96A7D0  token=0x6000E58  System.Void OnGraphStarted()
  RVA=0x0A96A614  token=0x6000E59  System.Void NodeCanvas.Framework.IUpdatable.Update()
  RVA=0x0A96A8B0  token=0x6000E5A  System.Void .ctor()
  RVA=0x0A95819C  token=0x6000E5B  System.Void <OnGraphStarted>b__14_0(System.Boolean result)
END_CLASS

CLASS: NodeCanvas.StateMachines.FlowScriptState
TYPE:  class
TOKEN: 0x2000386
SIZE:  0xD0
EXTENDS: NodeCanvas.StateMachines.FSMStateNested`1
FIELDS:
  private           NodeCanvas.Framework.BBParameter<FlowCanvas.FlowScript>_flowScript  // 0xb8
  public            System.String                   successEvent  // 0xc0
  public            System.String                   failureEvent  // 0xc8
PROPERTIES:
  subGraph  get=0x0A97030C  set=0x0A970354
  subGraphParameter  get=0x04D85EF0
METHODS:
  RVA=0x0A9700A0  token=0x6000E5F  System.Void OnEnter()
  RVA=0x0A970284  token=0x6000E60  System.Void OnUpdate()
  RVA=0x0A97020C  token=0x6000E61  System.Void OnFlowScriptFinished(System.Boolean success)
  RVA=0x0A97017C  token=0x6000E62  System.Void OnExit()
  RVA=0x0A9702D8  token=0x6000E63  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.PlayAnimationAdvanced
TYPE:  class
TOKEN: 0x20002D7
SIZE:  0xB8
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.AnimationClip>animationClip  // 0x68
  public            UnityEngine.WrapMode            animationWrap  // 0x70
  public            UnityEngine.AnimationBlendMode  blendMode  // 0x74
  public            System.Single                   playbackSpeed  // 0x78
  public            System.Single                   crossFadeTime  // 0x7c
  public            ParadoxNotion.PlayDirections    playDirection  // 0x80
  public            NodeCanvas.Framework.BBParameter<System.String>mixTransformName  // 0x88
  public            NodeCanvas.Framework.BBParameter<System.Int32>animationLayer  // 0x90
  public            System.Boolean                  queueAnimation  // 0x98
  public            System.Boolean                  waitActionFinish  // 0x99
  private           System.String                   animationToPlay  // 0xa0
  private           System.Int32                    dir  // 0xa8
  private           UnityEngine.Transform           mixTransform  // 0xb0
PROPERTIES:
  info  get=0x0A962D4C
METHODS:
  RVA=0x0A962B4C  token=0x6000B63  System.String OnInit()
  RVA=0x0A962688  token=0x6000B64  System.Void OnExecute()
  RVA=0x0A962C30  token=0x6000B65  System.Void OnUpdate()
  RVA=0x0A9625B0  token=0x6000B66  UnityEngine.Transform FindTransform(UnityEngine.Transform parent, System.String name)
  RVA=0x0A962CCC  token=0x6000B67  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.PlayAnimationSimple
TYPE:  class
TOKEN: 0x20002D8
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.AnimationClip>animationClip  // 0x68
  public            System.Single                   crossFadeTime  // 0x70
  public            UnityEngine.WrapMode            animationWrap  // 0x74
  public            System.Boolean                  waitActionFinish  // 0x78
  private   static  System.Collections.Generic.Dictionary<UnityEngine.Animation,UnityEngine.AnimationClip>lastPlayedClips  // static @ 0x0
PROPERTIES:
  info  get=0x0A963224
METHODS:
  RVA=0x0A962FEC  token=0x6000B69  System.String OnInit()
  RVA=0x0A962DA4  token=0x6000B6A  System.Void OnExecute()
  RVA=0x0A9630D0  token=0x6000B6B  System.Void OnUpdate()
  RVA=0x0A9631E0  token=0x6000B6C  System.Void .ctor()
  RVA=0x0A963154  token=0x6000B6D  System.Void .cctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.MecanimPlayAnimation
TYPE:  class
TOKEN: 0x20002D9
SIZE:  0xB8
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Int32>layerIndex  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.String>stateName  // 0x70
  public            System.Single                   transitTime  // 0x78
  public            System.Boolean                  waitUntilFinish  // 0x7c
  private           UnityEngine.AnimatorStateInfo   stateInfo  // 0x80
  private           System.Boolean                  played  // 0xb4
PROPERTIES:
  info  get=0x0A960AA4
METHODS:
  RVA=0x0A960764  token=0x6000B6F  System.Void OnExecute()
  RVA=0x0A9608CC  token=0x6000B70  System.Void OnUpdate()
  RVA=0x0A960A58  token=0x6000B71  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.MecanimSetBool
TYPE:  class
TOKEN: 0x20002DA
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>parameter  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Int32>parameterHashID  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Boolean>setTo  // 0x78
PROPERTIES:
  info  get=0x0A960C98
METHODS:
  RVA=0x0A960B0C  token=0x6000B73  System.Void OnExecute()
  RVA=0x0A960C5C  token=0x6000B74  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.MecanimSetFloat
TYPE:  class
TOKEN: 0x20002DB
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>parameter  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Int32>parameterHashID  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>setTo  // 0x78
  public            System.Single                   transitTime  // 0x80
  private           System.Single                   currentValue  // 0x84
PROPERTIES:
  info  get=0x0A9610BC
METHODS:
  RVA=0x0A960E34  token=0x6000B76  System.Void OnExecute()
  RVA=0x0A960EB8  token=0x6000B77  System.Void OnUpdate()
  RVA=0x0A960D48  token=0x6000B78  System.Single Get()
  RVA=0x0A960F6C  token=0x6000B79  System.Void Set(System.Single newValue)
  RVA=0x0A96106C  token=0x6000B7A  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.MecanimSetIK
TYPE:  class
TOKEN: 0x20002DC
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            UnityEngine.AvatarIKGoal        IKGoal  // 0x68
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>goal  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>weight  // 0x78
PROPERTIES:
  info  get=0x0A961400
METHODS:
  RVA=0x0A961294  token=0x6000B7C  System.Void OnExecute()
  RVA=0x0A96132C  token=0x6000B7D  System.Void OnStop()
  RVA=0x0A96116C  token=0x6000B7E  System.Void OnAnimatorIK(ParadoxNotion.EventData<System.Int32> msg)
  RVA=0x0A9613C4  token=0x6000B7F  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.MecanimSetInt
TYPE:  class
TOKEN: 0x20002DD
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>parameter  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Int32>parameterHashID  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Int32>setTo  // 0x78
PROPERTIES:
  info  get=0x0A96163C
METHODS:
  RVA=0x0A9614BC  token=0x6000B81  System.Void OnExecute()
  RVA=0x0A961600  token=0x6000B82  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.MecanimSetLayerWeight
TYPE:  class
TOKEN: 0x20002DE
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Int32>layerIndex  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>layerWeight  // 0x70
  public            System.Single                   transitTime  // 0x78
  private           System.Single                   currentValue  // 0x7c
PROPERTIES:
  info  get=0x0A9618D0
METHODS:
  RVA=0x0A9616EC  token=0x6000B84  System.Void OnExecute()
  RVA=0x0A961770  token=0x6000B85  System.Void OnUpdate()
  RVA=0x0A961894  token=0x6000B86  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.MecanimSetLookAt
TYPE:  class
TOKEN: 0x20002DF
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>targetPosition  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>targetWeight  // 0x70
PROPERTIES:
  info  get=0x0A961BF4
METHODS:
  RVA=0x0A961A88  token=0x6000B88  System.Void OnExecute()
  RVA=0x0A961B20  token=0x6000B89  System.Void OnStop()
  RVA=0x0A961970  token=0x6000B8A  System.Void OnAnimatorIK(ParadoxNotion.EventData<System.Int32> msg)
  RVA=0x0A961BB8  token=0x6000B8B  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.MecanimSetTrigger
TYPE:  class
TOKEN: 0x20002E0
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>parameter  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Int32>parameterHashID  // 0x70
PROPERTIES:
  info  get=0x0A961D88
METHODS:
  RVA=0x0A961C54  token=0x6000B8D  System.Void OnExecute()
  RVA=0x0A961D4C  token=0x6000B8E  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.LoadScene
TYPE:  class
TOKEN: 0x20002E1
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>sceneName  // 0x68
  public            NodeCanvas.Framework.BBParameter<UnityEngine.SceneManagement.LoadSceneMode>mode  // 0x70
PROPERTIES:
  info  get=0x0A960570
METHODS:
  RVA=0x0A9604C0  token=0x6000B90  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000B91  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.PlayAudioAtPosition
TYPE:  class
TOKEN: 0x20002E2
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.AudioClip>audioClip  // 0x68
  public            System.Single                   volume  // 0x70
  public            System.Boolean                  waitActionFinish  // 0x74
PROPERTIES:
  info  get=0x0A963404
METHODS:
  RVA=0x0A96327C  token=0x6000B93  System.Void OnExecute()
  RVA=0x0A963338  token=0x6000B94  System.Void OnUpdate()
  RVA=0x0A9633B8  token=0x6000B95  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.ComposeVector
TYPE:  class
TOKEN: 0x20002E3
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Single>x  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>y  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>z  // 0x78
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>saveAs  // 0x80
PROPERTIES:
  info  get=0x0A953E98
METHODS:
  RVA=0x0A953DB8  token=0x6000B97  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000B98  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.DecomposeVector
TYPE:  class
TOKEN: 0x20002E4
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>targetVector  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>x  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>y  // 0x78
  public            NodeCanvas.Framework.BBParameter<System.Single>z  // 0x80
PROPERTIES:
  info  get=0x0A955128
METHODS:
  RVA=0x0A955020  token=0x6000B9A  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000B9B  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.AddElementToDictionary`1
TYPE:  class
TOKEN: 0x20002E5
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.Dictionary<System.String,T>>dictionary  // 0x0
  public            NodeCanvas.Framework.BBParameter<System.String>key  // 0x0
  public            NodeCanvas.Framework.BBParameter<T>value  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000B9D  System.Void OnExecute()
  RVA=-1  // not resolved  token=0x6000B9E  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetDictionaryElement`1
TYPE:  class
TOKEN: 0x20002E6
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.Dictionary<System.String,T>>dictionary  // 0x0
  public            NodeCanvas.Framework.BBParameter<System.String>key  // 0x0
  public            NodeCanvas.Framework.BBParameter<T>saveAs  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000BA0  System.Void OnExecute()
  RVA=-1  // not resolved  token=0x6000BA1  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.EvaluateCurve
TYPE:  class
TOKEN: 0x20002E7
SIZE:  0x90
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.AnimationCurve>curve  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>from  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>to  // 0x78
  public            NodeCanvas.Framework.BBParameter<System.Single>time  // 0x80
  public            NodeCanvas.Framework.BBParameter<System.Single>saveAs  // 0x88
METHODS:
  RVA=0x0A9552CC  token=0x6000BA2  System.Void OnUpdate()
  RVA=0x0A955440  token=0x6000BA3  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetSelf
TYPE:  class
TOKEN: 0x20002E8
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveAs  // 0x68
METHODS:
  RVA=0x0A95DDA0  token=0x6000BA4  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000BA5  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetOtherBlackboardVariable
TYPE:  class
TOKEN: 0x20002E9
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>targetVariableName  // 0x68
  public            NodeCanvas.Framework.Internal.BBObjectParametersaveAs  // 0x70
PROPERTIES:
  info  get=0x0A95D0EC
METHODS:
  RVA=0x0A95CFE4  token=0x6000BA7  System.Void OnExecute()
  RVA=0x0A95D0B0  token=0x6000BA8  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetToString
TYPE:  class
TOKEN: 0x20002EA
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Object>variable  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.String>toString  // 0x70
PROPERTIES:
  info  get=0x0A95DEDC
METHODS:
  RVA=0x0A95DE1C  token=0x6000BAA  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000BAB  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.AddElementToList`1
TYPE:  class
TOKEN: 0x20002EB
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<T>>targetList  // 0x0
  public            NodeCanvas.Framework.BBParameter<T>targetElement  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000BAD  System.Void OnExecute()
  RVA=-1  // not resolved  token=0x6000BAE  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.ClearList
TYPE:  class
TOKEN: 0x20002EC
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.IList>targetList  // 0x68
PROPERTIES:
  info  get=0x0A953D78
METHODS:
  RVA=0x0A953D14  token=0x6000BB0  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000BB1  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetCloserGameObjectInList
TYPE:  class
TOKEN: 0x20002ED
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>list  // 0x68
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveAs  // 0x70
PROPERTIES:
  info  get=0x0A95B0D4
METHODS:
  RVA=0x0A95ADC8  token=0x6000BB3  System.Void OnExecute()
  RVA=0x0A95B098  token=0x6000BB4  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetIndexOfElement`1
TYPE:  class
TOKEN: 0x20002EE
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<T>>targetList  // 0x0
  public            NodeCanvas.Framework.BBParameter<T>targetElement  // 0x0
  public            NodeCanvas.Framework.BBParameter<System.Int32>saveIndexAs  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000BB5  System.Void OnExecute()
  RVA=-1  // not resolved  token=0x6000BB6  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetListCount
TYPE:  class
TOKEN: 0x20002EF
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.IList>targetList  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Int32>saveAs  // 0x70
PROPERTIES:
  info  get=0x0A95CE24
METHODS:
  RVA=0x0A95CD94  token=0x6000BB8  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000BB9  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.InsertElementToList`1
TYPE:  class
TOKEN: 0x20002F0
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<T>>targetList  // 0x0
  public            NodeCanvas.Framework.BBParameter<T>targetElement  // 0x0
  public            NodeCanvas.Framework.BBParameter<System.Int32>targetIndex  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000BBB  System.Void OnExecute()
  RVA=-1  // not resolved  token=0x6000BBC  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.PickListElement`1
TYPE:  class
TOKEN: 0x20002F1
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<T>>targetList  // 0x0
  public            NodeCanvas.Framework.BBParameter<System.Int32>index  // 0x0
  public            NodeCanvas.Framework.BBParameter<T>saveAs  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000BBE  System.Void OnExecute()
  RVA=-1  // not resolved  token=0x6000BBF  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.PickRandomListElement`1
TYPE:  class
TOKEN: 0x20002F2
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<T>>targetList  // 0x0
  public            NodeCanvas.Framework.BBParameter<T>saveAs  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000BC1  System.Void OnExecute()
  RVA=-1  // not resolved  token=0x6000BC2  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.RemoveElementFromList`1
TYPE:  class
TOKEN: 0x20002F3
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<T>>targetList  // 0x0
  public            NodeCanvas.Framework.BBParameter<T>targetElement  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000BC4  System.Void OnExecute()
  RVA=-1  // not resolved  token=0x6000BC5  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SetListElement`1
TYPE:  class
TOKEN: 0x20002F4
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<T>>targetList  // 0x0
  public            NodeCanvas.Framework.BBParameter<System.Int32>index  // 0x0
  public            NodeCanvas.Framework.BBParameter<T>newValue  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000BC6  System.Void OnExecute()
  RVA=-1  // not resolved  token=0x6000BC7  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.ShuffleList
TYPE:  class
TOKEN: 0x20002F5
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.IList>targetList  // 0x68
METHODS:
  RVA=0x0A967108  token=0x6000BC8  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000BC9  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SortGameObjectListByDistance
TYPE:  class
TOKEN: 0x20002F6
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>targetList  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>saveAs  // 0x70
  public            System.Boolean                  reverse  // 0x78
PROPERTIES:
  info  get=0x0A967460
METHODS:
  RVA=0x0A967208  token=0x6000BCB  System.Void OnExecute()
  RVA=0x0A967424  token=0x6000BCC  System.Void .ctor()
  RVA=0x0A967360  token=0x6000BCD  System.Single <OnExecute>b__5_0(UnityEngine.GameObject go)
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.LoadBlackboard
TYPE:  class
TOKEN: 0x20002F7
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>saveKey  // 0x68
PROPERTIES:
  info  get=0x0A960468
METHODS:
  RVA=0x0A9603A0  token=0x6000BCF  System.Void OnExecute()
  RVA=0x0A96042C  token=0x6000BD0  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.NormalizeVector
TYPE:  class
TOKEN: 0x20002F8
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>targetVector  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>multiply  // 0x70
METHODS:
  RVA=0x0A962448  token=0x6000BD1  System.Void OnExecute()
  RVA=0x0A96255C  token=0x6000BD2  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SampleCurve
TYPE:  class
TOKEN: 0x20002F9
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.AnimationCurve>curve  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>sampleAt  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>saveAs  // 0x78
METHODS:
  RVA=0x0A963E24  token=0x6000BD3  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000BD4  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SaveBlackboard
TYPE:  class
TOKEN: 0x20002FA
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>saveKey  // 0x68
PROPERTIES:
  info  get=0x0A963FB0
METHODS:
  RVA=0x0A963EE8  token=0x6000BD6  System.Void OnExecute()
  RVA=0x0A963F74  token=0x6000BD7  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SetBoolean
TYPE:  class
TOKEN: 0x20002FB
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Boolean>boolVariable  // 0x68
  public            NodeCanvas.Tasks.Actions.SetBoolean.BoolSetModessetTo  // 0x70
PROPERTIES:
  info  get=0x0A964BD4
METHODS:
  RVA=0x0A964B3C  token=0x6000BD9  System.Void OnExecute()
  RVA=0x04DA6730  token=0x6000BDA  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SetBooleanRandom
TYPE:  class
TOKEN: 0x20002FD
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Boolean>boolVariable  // 0x68
PROPERTIES:
  info  get=0x0A964AC8
METHODS:
  RVA=0x0A964A54  token=0x6000BDC  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000BDD  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SetEnum
TYPE:  class
TOKEN: 0x20002FE
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.Internal.BBObjectParametervalueA  // 0x68
  public            NodeCanvas.Framework.Internal.BBObjectParametervalueB  // 0x70
PROPERTIES:
  info  get=0x0A964E30
METHODS:
  RVA=0x0A964CB8  token=0x6000BDF  System.Void OnExecute()
  RVA=0x0A964D3C  token=0x6000BE0  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SetFloat
TYPE:  class
TOKEN: 0x20002FF
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Single>valueA  // 0x68
  public            ParadoxNotion.OperationMethod   Operation  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>valueB  // 0x78
  public            System.Boolean                  perSecond  // 0x80
PROPERTIES:
  info  get=0x0A96568C
METHODS:
  RVA=0x0A9655A0  token=0x6000BE2  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000BE3  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SetFloatRandom
TYPE:  class
TOKEN: 0x2000300
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Single>minValue  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>maxValue  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>floatVariable  // 0x78
PROPERTIES:
  info  get=0x0A965444
METHODS:
  RVA=0x0A965390  token=0x6000BE5  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000BE6  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SetInt
TYPE:  class
TOKEN: 0x2000301
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Int32>valueA  // 0x68
  public            ParadoxNotion.OperationMethod   Operation  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Int32>valueB  // 0x78
PROPERTIES:
  info  get=0x0A965A70
METHODS:
  RVA=0x0A9659C4  token=0x6000BE8  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000BE9  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SetIntRandom
TYPE:  class
TOKEN: 0x2000302
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Int32>minValue  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Int32>maxValue  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Int32>intVariable  // 0x78
PROPERTIES:
  info  get=0x0A965868
METHODS:
  RVA=0x0A9657B8  token=0x6000BEB  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000BEC  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SetOtherBlackboardVariable
TYPE:  class
TOKEN: 0x2000303
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>targetVariableName  // 0x68
  public            NodeCanvas.Framework.Internal.BBObjectParameternewValue  // 0x70
PROPERTIES:
  info  get=0x0A966028
METHODS:
  RVA=0x0A965F2C  token=0x6000BEE  System.Void OnExecute()
  RVA=0x0A965FEC  token=0x6000BEF  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SetVariable`1
TYPE:  class
TOKEN: 0x2000304
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<T>valueA  // 0x0
  public            NodeCanvas.Framework.BBParameter<T>valueB  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000BF1  System.Void OnExecute()
  RVA=-1  // not resolved  token=0x6000BF2  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SetVector3
TYPE:  class
TOKEN: 0x2000305
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>valueA  // 0x68
  public            ParadoxNotion.OperationMethod   operation  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>valueB  // 0x78
  public            System.Boolean                  perSecond  // 0x80
PROPERTIES:
  info  get=0x0A966AAC
METHODS:
  RVA=0x0A966980  token=0x6000BF4  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000BF5  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.TriggerBoolean
TYPE:  class
TOKEN: 0x2000306
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Boolean>variable  // 0x68
PROPERTIES:
  info  get=0x0A967E1C
METHODS:
  RVA=0x0A967D88  token=0x6000BF7  System.Void OnExecute()
  RVA=0x0A967D2C  token=0x6000BF8  System.Collections.IEnumerator Flip()
  RVA=0x04D8E030  token=0x6000BF9  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.FadeIn
TYPE:  class
TOKEN: 0x2000308
SIZE:  0x68
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
METHODS:
  RVA=0x04D8E030  token=0x6000C00  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.FadeOut
TYPE:  class
TOKEN: 0x2000309
SIZE:  0x68
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
METHODS:
  RVA=0x04D8E030  token=0x6000C01  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.Say
TYPE:  class
TOKEN: 0x200030A
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.DialogueTrees.Statementstatement  // 0x68
PROPERTIES:
  info  get=0x0A964384
METHODS:
  RVA=0x0A9641EC  token=0x6000C03  System.Void OnExecute()
  RVA=0x0A9642F8  token=0x6000C04  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SayRandom
TYPE:  class
TOKEN: 0x200030B
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            System.Collections.Generic.List<NodeCanvas.DialogueTrees.Statement>statements  // 0x68
METHODS:
  RVA=0x0A964008  token=0x6000C05  System.Void OnExecute()
  RVA=0x0A964160  token=0x6000C06  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.StartDialogueTree
TYPE:  class
TOKEN: 0x200030C
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<NodeCanvas.DialogueTrees.DialogueTreeController>dialogueTreeController  // 0x68
  public            System.Boolean                  waitActionFinish  // 0x70
  public            System.Boolean                  isPrefab  // 0x71
  private           NodeCanvas.DialogueTrees.DialogueTreeControllerinstance  // 0x78
PROPERTIES:
  info  get=0x0A9677D4
METHODS:
  RVA=0x0A967500  token=0x6000C08  System.Void OnExecute()
  RVA=0x0A967790  token=0x6000C09  System.Void .ctor()
  RVA=0x0A9676AC  token=0x6000C0A  System.Void <OnExecute>b__6_0(System.Boolean success)
  RVA=0x0A967730  token=0x6000C0B  System.Void <OnExecute>b__6_1(System.Boolean success)
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.CreateGameObject
TYPE:  class
TOKEN: 0x200030D
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>objectName  // 0x68
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>position  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>rotation  // 0x78
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveAs  // 0x80
METHODS:
  RVA=0x0A953EFC  token=0x6000C0C  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000C0D  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.CreatePrimitive
TYPE:  class
TOKEN: 0x200030E
SIZE:  0x90
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>objectName  // 0x68
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>position  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>rotation  // 0x78
  public            NodeCanvas.Framework.BBParameter<UnityEngine.PrimitiveType>type  // 0x80
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveAs  // 0x88
METHODS:
  RVA=0x0A95407C  token=0x6000C0E  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000C0F  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.DestroyGameObject
TYPE:  class
TOKEN: 0x200030F
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            System.Boolean                  immediately  // 0x68
PROPERTIES:
  info  get=0x0A955284
METHODS:
  RVA=0x0A955188  token=0x6000C11  System.Void OnUpdate()
  RVA=0x0A955248  token=0x6000C12  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.FindAllWithLayer
TYPE:  class
TOKEN: 0x2000310
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.LayerMask>targetLayers  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>saveAs  // 0x70
PROPERTIES:
  info  get=0x0A95995C
METHODS:
  RVA=0x0A95987C  token=0x6000C14  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000C15  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.FindAllWithName
TYPE:  class
TOKEN: 0x2000311
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>searchName  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>saveAs  // 0x70
PROPERTIES:
  info  get=0x0A959BE4
METHODS:
  RVA=0x0A9599FC  token=0x6000C17  System.Void OnExecute()
  RVA=0x0A959B84  token=0x6000C18  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.FindAllWithTag
TYPE:  class
TOKEN: 0x2000312
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>searchTag  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>saveAs  // 0x70
PROPERTIES:
  info  get=0x0A959DC4
METHODS:
  RVA=0x0A959C84  token=0x6000C1A  System.Void OnExecute()
  RVA=0x0A959D64  token=0x6000C1B  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.FindChildByName
TYPE:  class
TOKEN: 0x2000313
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>childName  // 0x68
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Transform>saveAs  // 0x70
PROPERTIES:
  info  get=0x0A959FDC
METHODS:
  RVA=0x0A959E64  token=0x6000C1D  System.Void OnExecute()
  RVA=0x0A959FA0  token=0x6000C1E  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.FindClosestWithTag
TYPE:  class
TOKEN: 0x2000314
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>searchTag  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Boolean>ignoreChildren  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveObjectAs  // 0x78
  public            NodeCanvas.Framework.BBParameter<System.Single>saveDistanceAs  // 0x80
METHODS:
  RVA=0x0A95A108  token=0x6000C1F  System.Void OnExecute()
  RVA=0x0A95A3D4  token=0x6000C20  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.FindObjectOfType`1
TYPE:  class
TOKEN: 0x2000315
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<T>saveComponentAs  // 0x0
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveGameObjectAs  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000C21  System.Void OnExecute()
  RVA=-1  // not resolved  token=0x6000C22  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.FindObjectsOfType`1
TYPE:  class
TOKEN: 0x2000316
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>saveGameObjects  // 0x0
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<T>>saveComponents  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000C23  System.Void OnExecute()
  RVA=-1  // not resolved  token=0x6000C24  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.FindWithName
TYPE:  class
TOKEN: 0x2000318
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>gameObjectName  // 0x68
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveAs  // 0x70
PROPERTIES:
  info  get=0x0A95A49C
METHODS:
  RVA=0x0A95A410  token=0x6000C29  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000C2A  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.FindWithTag
TYPE:  class
TOKEN: 0x2000319
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            System.String                   searchTag  // 0x68
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveAs  // 0x70
PROPERTIES:
  info  get=0x0A95A5F0
METHODS:
  RVA=0x0A95A53C  token=0x6000C2C  System.Void OnExecute()
  RVA=0x0A95A5A8  token=0x6000C2D  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetAllChildGameObjects
TYPE:  class
TOKEN: 0x200031A
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>saveAs  // 0x68
  public            System.Boolean                  recursive  // 0x70
PROPERTIES:
  info  get=0x0A95AD28
METHODS:
  RVA=0x0A95A92C  token=0x6000C2F  System.Void OnExecute()
  RVA=0x0A95A71C  token=0x6000C30  System.Collections.Generic.List<UnityEngine.Transform> Get(UnityEngine.Transform parent)
  RVA=0x0A95ACEC  token=0x6000C31  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetComponent`1
TYPE:  class
TOKEN: 0x200031C
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<T>saveAs  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000C36  System.Void OnExecute()
  RVA=-1  // not resolved  token=0x6000C37  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetDistance
TYPE:  class
TOKEN: 0x200031D
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>target  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>saveAs  // 0x70
PROPERTIES:
  info  get=0x0A95B2D8
METHODS:
  RVA=0x0A95B174  token=0x6000C39  System.Void OnExecute()
  RVA=0x0A95B29C  token=0x6000C3A  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetGameObjectPosition
TYPE:  class
TOKEN: 0x200031E
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>saveAs  // 0x68
PROPERTIES:
  info  get=0x0A95B908
METHODS:
  RVA=0x0A95B828  token=0x6000C3C  System.Void OnExecute()
  RVA=0x0A95B8CC  token=0x6000C3D  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.InstantiateGameObject
TYPE:  class
TOKEN: 0x200031F
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Transform>parent  // 0x68
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>clonePosition  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>cloneRotation  // 0x78
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveCloneAs  // 0x80
PROPERTIES:
  info  get=0x0A95F558
METHODS:
  RVA=0x0A95F358  token=0x6000C3F  System.Void OnExecute()
  RVA=0x0A95F51C  token=0x6000C40  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.LookAt
TYPE:  class
TOKEN: 0x2000320
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>lookTarget  // 0x68
  public            System.Boolean                  repeat  // 0x70
PROPERTIES:
  info  get=0x0A960704
METHODS:
  RVA=0x0A9606C0  token=0x6000C42  System.Void OnExecute()
  RVA=0x0A9606C0  token=0x6000C43  System.Void OnUpdate()
  RVA=0x0A9605B0  token=0x6000C44  System.Void DoLook()
  RVA=0x0A9606C8  token=0x6000C45  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.RemoveComponent`1
TYPE:  class
TOKEN: 0x2000321
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            System.Boolean                  immediately  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000C47  System.Void OnExecute()
  RVA=-1  // not resolved  token=0x6000C48  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SetObjectActive
TYPE:  class
TOKEN: 0x2000322
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Tasks.Actions.SetObjectActive.SetActiveModesetTo  // 0x68
PROPERTIES:
  info  get=0x0A965BF0
METHODS:
  RVA=0x0A965AEC  token=0x6000C4A  System.Void OnExecute()
  RVA=0x0A965BA4  token=0x6000C4B  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SetObjectEnabled
TYPE:  class
TOKEN: 0x2000324
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Tasks.Actions.SetObjectEnabled.SetEnableModesetTo  // 0x68
PROPERTIES:
  info  get=0x0A965D50
METHODS:
  RVA=0x0A965C6C  token=0x6000C4D  System.Void OnExecute()
  RVA=0x0A965D04  token=0x6000C4E  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SetObjectVisibility
TYPE:  class
TOKEN: 0x2000326
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Tasks.Actions.SetObjectVisibility.SetVisibleModesetTo  // 0x68
PROPERTIES:
  info  get=0x0A965EB0
METHODS:
  RVA=0x0A965DCC  token=0x6000C50  System.Void OnExecute()
  RVA=0x0A965E64  token=0x6000C51  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetInputAxis
TYPE:  class
TOKEN: 0x2000328
SIZE:  0xB0
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>xAxisName  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.String>yAxisName  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.String>zAxisName  // 0x78
  public            NodeCanvas.Framework.BBParameter<System.Single>multiplier  // 0x80
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>saveAs  // 0x88
  public            NodeCanvas.Framework.BBParameter<System.Single>saveXAs  // 0x90
  public            NodeCanvas.Framework.BBParameter<System.Single>saveYAs  // 0x98
  public            NodeCanvas.Framework.BBParameter<System.Single>saveZAs  // 0xa0
  public            System.Boolean                  repeat  // 0xa8
METHODS:
  RVA=0x0A95BC58  token=0x6000C52  System.Void OnExecute()
  RVA=0x0A95BC58  token=0x6000C53  System.Void OnUpdate()
  RVA=0x0A95B99C  token=0x6000C54  System.Void Do()
  RVA=0x0A95BC60  token=0x6000C55  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetMousePosition
TYPE:  class
TOKEN: 0x2000329
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>saveAs  // 0x68
  public            System.Boolean                  repeat  // 0x70
METHODS:
  RVA=0x0A95CEF0  token=0x6000C56  System.Void OnExecute()
  RVA=0x0A95CEF0  token=0x6000C57  System.Void OnUpdate()
  RVA=0x0A95CE68  token=0x6000C58  System.Void Do()
  RVA=0x04D8E030  token=0x6000C59  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetMouseScrollDelta
TYPE:  class
TOKEN: 0x200032A
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Single>saveAs  // 0x68
  public            System.Boolean                  repeat  // 0x70
PROPERTIES:
  info  get=0x0A95CF84
METHODS:
  RVA=0x0A95CF7C  token=0x6000C5B  System.Void OnExecute()
  RVA=0x0A95CF7C  token=0x6000C5C  System.Void OnUpdate()
  RVA=0x0A95CEF8  token=0x6000C5D  System.Void Do()
  RVA=0x04D8E030  token=0x6000C5E  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.WaitMousePick
TYPE:  class
TOKEN: 0x200032B
SIZE:  0xD0
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Tasks.Actions.WaitMousePick.ButtonKeysbuttonKey  // 0x68
  public            UnityEngine.LayerMask           mask  // 0x6c
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveObjectAs  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>saveDistanceAs  // 0x78
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>savePositionAs  // 0x80
  private           System.Int32                    buttonID  // 0x88
  private           UnityEngine.RaycastHit          hit  // 0x90
PROPERTIES:
  info  get=0x0A968A54
METHODS:
  RVA=0x0A968898  token=0x6000C60  System.Void OnUpdate()
  RVA=0x04DA6770  token=0x6000C61  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.WaitMousePick2D
TYPE:  class
TOKEN: 0x200032D
SIZE:  0xB0
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Tasks.Actions.WaitMousePick2D.ButtonKeysbuttonKey  // 0x68
  public            UnityEngine.LayerMask           mask  // 0x6c
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveObjectAs  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>saveDistanceAs  // 0x78
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>savePositionAs  // 0x80
  private           System.Int32                    buttonID  // 0x88
  private           UnityEngine.RaycastHit2D        hit  // 0x8c
PROPERTIES:
  info  get=0x0A968834
METHODS:
  RVA=0x0A9685C4  token=0x6000C63  System.Void OnUpdate()
  RVA=0x04DA6770  token=0x6000C64  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.CurveTransformTween
TYPE:  class
TOKEN: 0x200032F
SIZE:  0xB0
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Tasks.Actions.CurveTransformTween.TransformModetransformMode  // 0x68
  public            NodeCanvas.Tasks.Actions.CurveTransformTween.TweenModemode  // 0x6c
  public            NodeCanvas.Tasks.Actions.CurveTransformTween.PlayModeplayMode  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>targetPosition  // 0x78
  public            NodeCanvas.Framework.BBParameter<UnityEngine.AnimationCurve>curve  // 0x80
  public            NodeCanvas.Framework.BBParameter<System.Single>time  // 0x88
  private           UnityEngine.Vector3             original  // 0x90
  private           UnityEngine.Vector3             final  // 0x9c
  private           System.Boolean                  ponging  // 0xa8
METHODS:
  RVA=0x0A954214  token=0x6000C65  System.Void OnExecute()
  RVA=0x0A954448  token=0x6000C66  System.Void OnUpdate()
  RVA=0x0A954668  token=0x6000C67  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.InputMove
TYPE:  class
TOKEN: 0x2000333
SIZE:  0xA0
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Single>strafe  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>turn  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>forward  // 0x78
  public            NodeCanvas.Framework.BBParameter<System.Single>up  // 0x80
  public            NodeCanvas.Framework.BBParameter<System.Single>moveSpeed  // 0x88
  public            NodeCanvas.Framework.BBParameter<System.Single>rotationSpeed  // 0x90
  public            System.Boolean                  repeat  // 0x98
METHODS:
  RVA=0x0A95ED88  token=0x6000C68  System.Void OnUpdate()
  RVA=0x0A95F2CC  token=0x6000C69  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.MoveAway
TYPE:  class
TOKEN: 0x2000334
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>target  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>speed  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>stopDistance  // 0x78
  public            System.Boolean                  waitActionFinish  // 0x80
METHODS:
  RVA=0x0A961E34  token=0x6000C6A  System.Void OnUpdate()
  RVA=0x0A9620C0  token=0x6000C6B  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.MoveTowards
TYPE:  class
TOKEN: 0x2000335
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>target  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>speed  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>stopDistance  // 0x78
  public            System.Boolean                  waitActionFinish  // 0x80
METHODS:
  RVA=0x0A962140  token=0x6000C6C  System.Void OnUpdate()
  RVA=0x0A9623C8  token=0x6000C6D  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.RotateAway
TYPE:  class
TOKEN: 0x2000336
SIZE:  0x90
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>target  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>speed  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>angleDifference  // 0x78
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>upVector  // 0x80
  public            System.Boolean                  waitActionFinish  // 0x88
METHODS:
  RVA=0x0A963580  token=0x6000C6E  System.Void OnUpdate()
  RVA=0x0A963920  token=0x6000C6F  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.RotateTowards
TYPE:  class
TOKEN: 0x2000337
SIZE:  0x90
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>target  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>speed  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>angleDifference  // 0x78
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>upVector  // 0x80
  public            System.Boolean                  waitActionFinish  // 0x88
METHODS:
  RVA=0x0A9639E4  token=0x6000C70  System.Void OnUpdate()
  RVA=0x0A963D60  token=0x6000C71  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.FindClosestEdge
TYPE:  class
TOKEN: 0x2000338
SIZE:  0xA0
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>targetPosition  // 0x68
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>saveFoundPosition  // 0x70
  private           UnityEngine.AI.NavMeshHit       hit  // 0x78
METHODS:
  RVA=0x0A95A03C  token=0x6000C72  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000C73  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetLinecastInfo
TYPE:  class
TOKEN: 0x2000339
SIZE:  0xD8
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>target  // 0x68
  public            NodeCanvas.Framework.BBParameter<UnityEngine.LayerMask>layerMask  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveHitGameObjectAs  // 0x78
  public            NodeCanvas.Framework.BBParameter<System.Single>saveDistanceAs  // 0x80
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>savePointAs  // 0x88
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>saveNormalAs  // 0x90
  private           UnityEngine.RaycastHit          hit  // 0x98
METHODS:
  RVA=0x0A95CACC  token=0x6000C74  System.Void OnExecute()
  RVA=0x0A95C96C  token=0x6000C75  System.Void OnDrawGizmosSelected()
  RVA=0x0A95CD38  token=0x6000C76  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetLinecastInfo2D
TYPE:  class
TOKEN: 0x200033A
SIZE:  0xC0
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>target  // 0x68
  public            UnityEngine.LayerMask           mask  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveHitGameObjectAs  // 0x78
  public            NodeCanvas.Framework.BBParameter<System.Single>saveDistanceAs  // 0x80
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>savePointAs  // 0x88
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>saveNormalAs  // 0x90
  private           UnityEngine.RaycastHit2D        hit  // 0x98
METHODS:
  RVA=0x0A95C640  token=0x6000C77  System.Void OnExecute()
  RVA=0x0A95C4E0  token=0x6000C78  System.Void OnDrawGizmosSelected()
  RVA=0x0A95C920  token=0x6000C79  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetLinecastInfo2DAll
TYPE:  class
TOKEN: 0x200033B
SIZE:  0xA0
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>target  // 0x68
  public            UnityEngine.LayerMask           mask  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>saveHitGameObjectsAs  // 0x78
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<System.Single>>saveDistancesAs  // 0x80
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.Vector3>>savePointsAs  // 0x88
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.Vector3>>saveNormalsAs  // 0x90
  private           UnityEngine.RaycastHit2D[]      hits  // 0x98
METHODS:
  RVA=0x0A95BE80  token=0x6000C7A  System.Void OnExecute()
  RVA=0x0A95BD20  token=0x6000C7B  System.Void OnDrawGizmosSelected()
  RVA=0x0A95C494  token=0x6000C7C  System.Void .ctor()
  RVA=0x0A95C3DC  token=0x6000C7D  UnityEngine.Vector3 <OnExecute>b__7_2(UnityEngine.RaycastHit2D h)
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetOverlapSphereObjects
TYPE:  class
TOKEN: 0x200033D
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            UnityEngine.LayerMask           layerMask  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>radius  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>saveObjectsAs  // 0x78
METHODS:
  RVA=0x0A95D230  token=0x6000C83  System.Void OnExecute()
  RVA=0x0A95D130  token=0x6000C84  System.Void OnDrawGizmosSelected()
  RVA=0x0A95D4CC  token=0x6000C85  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.ExecuteFunction_Multiplatform
TYPE:  class
TOKEN: 0x200033F
SIZE:  0x98
EXTENDS: NodeCanvas.Framework.ActionTask
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper
FIELDS:
  protected         ParadoxNotion.Serialization.SerializedMethodInfomethod  // 0x68
  protected         System.Collections.Generic.List<NodeCanvas.Framework.Internal.BBObjectParameter>parameters  // 0x70
  protected         NodeCanvas.Framework.Internal.BBObjectParameterreturnValue  // 0x78
  private           System.Object[]                 args  // 0x80
  private           System.Boolean                  routineRunning  // 0x88
  private           System.Boolean[]                parameterIsByRef  // 0x90
PROPERTIES:
  targetMethod  get=0x0A946100
  agentType  get=0x0A955D48
  info  get=0x0A955E0C
METHODS:
  RVA=0x04D86230  token=0x6000C8C  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A9559C4  token=0x6000C8D  System.Void OnValidate(NodeCanvas.Framework.ITaskSystem ownerSystem)
  RVA=0x0A95583C  token=0x6000C8E  System.String OnInit()
  RVA=0x0A9555A4  token=0x6000C8F  System.Void OnExecute()
  RVA=0x04DA66E0  token=0x6000C90  System.Void OnStop()
  RVA=0x0A955530  token=0x6000C91  System.Collections.IEnumerator InternalCoroutine(System.Collections.IEnumerator routine)
  RVA=0x0A9559EC  token=0x6000C92  System.Void SetMethod(System.Reflection.MethodInfo method)
  RVA=0x0A955CC8  token=0x6000C93  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetField_0
TYPE:  class
TOKEN: 0x2000341
SIZE:  0x20
FIELDS:
  public            System.Type                     targetType  // 0x10
  public            System.String                   fieldName  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000C9A  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetField
TYPE:  class
TOKEN: 0x2000342
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper ParadoxNotion.Serialization.FullSerializer.IMigratable`1 ParadoxNotion.Serialization.FullSerializer.IMigratable
FIELDS:
  protected         ParadoxNotion.Serialization.SerializedFieldInfofield  // 0x68
  protected         NodeCanvas.Framework.Internal.BBObjectParametersaveAs  // 0x70
PROPERTIES:
  targetField  get=0x092C37DC
  agentType  get=0x0A95B5E8
  info  get=0x0A95B6B8
METHODS:
  RVA=0x0A95B480  token=0x6000C9B  System.Void ParadoxNotion.Serialization.FullSerializer.IMigratable<NodeCanvas.Tasks.Actions.GetField_0>.Migrate(NodeCanvas.Tasks.Actions.GetField_0 model)
  RVA=0x04D86230  token=0x6000C9F  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A95B3E8  token=0x6000CA0  System.String OnInit()
  RVA=0x0A95B330  token=0x6000CA1  System.Void OnExecute()
  RVA=0x0A95B538  token=0x6000CA2  System.Void SetTargetField(System.Reflection.FieldInfo newField)
  RVA=0x04D8E030  token=0x6000CA3  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetProperty_Multiplatform
TYPE:  class
TOKEN: 0x2000343
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper
FIELDS:
  protected         ParadoxNotion.Serialization.SerializedMethodInfomethod  // 0x68
  protected         NodeCanvas.Framework.Internal.BBObjectParameterreturnValue  // 0x70
PROPERTIES:
  targetMethod  get=0x0A946100
  agentType  get=0x0A95D74C
  info  get=0x0A95D810
METHODS:
  RVA=0x04D86230  token=0x6000CA7  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A95D674  token=0x6000CA8  System.Void OnValidate(NodeCanvas.Framework.ITaskSystem ownerSystem)
  RVA=0x0A95D5EC  token=0x6000CA9  System.String OnInit()
  RVA=0x0A95D530  token=0x6000CAA  System.Void OnExecute()
  RVA=0x0A95D69C  token=0x6000CAB  System.Void SetMethod(System.Reflection.MethodInfo method)
  RVA=0x04D8E030  token=0x6000CAC  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.ImplementedAction_Multiplatform
TYPE:  class
TOKEN: 0x2000344
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ActionTask
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper
FIELDS:
  private           ParadoxNotion.Serialization.SerializedMethodInfomethod  // 0x68
  private           System.Collections.Generic.List<NodeCanvas.Framework.Internal.BBObjectParameter>parameters  // 0x70
  private           NodeCanvas.Framework.Status     actionStatus  // 0x78
  private           System.Object[]                 args  // 0x80
PROPERTIES:
  targetMethod  get=0x0A946100
  agentType  get=0x0A95E6FC
  info  get=0x0A95E7C0
METHODS:
  RVA=0x04D86230  token=0x6000CB0  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A95E4D8  token=0x6000CB1  System.Void OnValidate(NodeCanvas.Framework.ITaskSystem ownerSystem)
  RVA=0x0A95E290  token=0x6000CB2  System.String OnInit()
  RVA=0x0A95E374  token=0x6000CB3  System.Void OnUpdate()
  RVA=0x04DA6700  token=0x6000CB4  System.Void OnStop()
  RVA=0x0A95E500  token=0x6000CB5  System.Void SetMethod(System.Reflection.MethodInfo method)
  RVA=0x0A95E678  token=0x6000CB6  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SendMessage
TYPE:  class
TOKEN: 0x2000345
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>methodName  // 0x68
PROPERTIES:
  info  get=0x0A964A14
METHODS:
  RVA=0x0A96494C  token=0x6000CB8  System.Void OnExecute()
  RVA=0x0A9649D8  token=0x6000CB9  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SendMessage`1
TYPE:  class
TOKEN: 0x2000346
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>methodName  // 0x0
  public            NodeCanvas.Framework.BBParameter<T>argument  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000CBB  System.Void OnExecute()
  RVA=-1  // not resolved  token=0x6000CBC  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SendMessageToType`1
TYPE:  class
TOKEN: 0x2000347
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>message  // 0x0
  public            NodeCanvas.Framework.BBParameter<System.Object>argument  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000CBE  System.Void OnExecute()
  RVA=-1  // not resolved  token=0x6000CBF  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SetField_0
TYPE:  class
TOKEN: 0x2000348
SIZE:  0x20
FIELDS:
  public            System.Type                     targetType  // 0x10
  public            System.String                   fieldName  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000CC0  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SetField
TYPE:  class
TOKEN: 0x2000349
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper ParadoxNotion.Serialization.FullSerializer.IMigratable`1 ParadoxNotion.Serialization.FullSerializer.IMigratable
FIELDS:
  protected         ParadoxNotion.Serialization.SerializedFieldInfofield  // 0x68
  protected         NodeCanvas.Framework.Internal.BBObjectParametersetValue  // 0x70
PROPERTIES:
  targetField  get=0x092C37DC
  agentType  get=0x0A965170
  info  get=0x0A965240
METHODS:
  RVA=0x0A965008  token=0x6000CC1  System.Void ParadoxNotion.Serialization.FullSerializer.IMigratable<NodeCanvas.Tasks.Actions.SetField_0>.Migrate(NodeCanvas.Tasks.Actions.SetField_0 model)
  RVA=0x04D86230  token=0x6000CC5  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A964F70  token=0x6000CC6  System.String OnInit()
  RVA=0x0A964EB8  token=0x6000CC7  System.Void OnExecute()
  RVA=0x0A9650C0  token=0x6000CC8  System.Void SetTargetField(System.Reflection.FieldInfo newField)
  RVA=0x04D8E030  token=0x6000CC9  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SetProperty_Multiplatform
TYPE:  class
TOKEN: 0x200034A
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper
FIELDS:
  protected         ParadoxNotion.Serialization.SerializedMethodInfomethod  // 0x68
  protected         NodeCanvas.Framework.Internal.BBObjectParameterparameter  // 0x70
PROPERTIES:
  targetMethod  get=0x0A946100
  agentType  get=0x0A96632C
  info  get=0x0A9663F0
METHODS:
  RVA=0x04D86230  token=0x6000CCD  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A966238  token=0x6000CCE  System.Void OnValidate(NodeCanvas.Framework.ITaskSystem ownerSystem)
  RVA=0x0A9661B0  token=0x6000CCF  System.String OnInit()
  RVA=0x0A9660C4  token=0x6000CD0  System.Void OnExecute()
  RVA=0x0A966260  token=0x6000CD1  System.Void SetMethod(System.Reflection.MethodInfo method)
  RVA=0x04D8E030  token=0x6000CD2  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.ExecuteFunction
TYPE:  class
TOKEN: 0x200034B
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper
FIELDS:
  protected         NodeCanvas.Framework.Internal.ReflectedWrapperfunctionWrapper  // 0x68
  private           System.Boolean                  routineRunning  // 0x70
PROPERTIES:
  targetMethod  get=0x0A9468AC
  agentType  get=0x0A956598
  info  get=0x0A95665C
METHODS:
  RVA=0x0A95618C  token=0x6000CD3  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A9564F8  token=0x6000CD7  System.Void OnValidate(NodeCanvas.Framework.ITaskSystem ownerSystem)
  RVA=0x0A95639C  token=0x6000CD8  System.String OnInit()
  RVA=0x0A95619C  token=0x6000CD9  System.Void OnExecute()
  RVA=0x04DA61D0  token=0x6000CDA  System.Void OnStop()
  RVA=0x0A9554BC  token=0x6000CDB  System.Collections.IEnumerator InternalCoroutine(System.Collections.IEnumerator routine)
  RVA=0x0A956544  token=0x6000CDC  System.Void SetMethod(System.Reflection.MethodInfo method)
  RVA=0x04D8E030  token=0x6000CDD  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GetProperty
TYPE:  class
TOKEN: 0x200034D
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper
FIELDS:
  protected         NodeCanvas.Framework.Internal.ReflectedFunctionWrapperfunctionWrapper  // 0x68
PROPERTIES:
  targetMethod  get=0x0A9468AC
  agentType  get=0x0A95DB6C
  info  get=0x0A95DC30
METHODS:
  RVA=0x0A95618C  token=0x6000CE7  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A95DACC  token=0x6000CE8  System.Void OnValidate(NodeCanvas.Framework.ITaskSystem ownerSystem)
  RVA=0x0A95D9B0  token=0x6000CE9  System.String OnInit()
  RVA=0x0A95D974  token=0x6000CEA  System.Void OnExecute()
  RVA=0x0A95DB18  token=0x6000CEB  System.Void SetMethod(System.Reflection.MethodInfo method)
  RVA=0x04D8E030  token=0x6000CEC  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.ImplementedAction
TYPE:  class
TOKEN: 0x200034E
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper
FIELDS:
  protected         NodeCanvas.Framework.Internal.ReflectedFunctionWrapperfunctionWrapper  // 0x68
  private           NodeCanvas.Framework.Status     actionStatus  // 0x70
PROPERTIES:
  targetMethod  get=0x0A9468AC
  agentType  get=0x0A95EB04
  info  get=0x0A95EBC8
METHODS:
  RVA=0x0A95618C  token=0x6000CF0  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A95DACC  token=0x6000CF1  System.Void OnValidate(NodeCanvas.Framework.ITaskSystem ownerSystem)
  RVA=0x0A95E978  token=0x6000CF2  System.String OnInit()
  RVA=0x0A95EA94  token=0x6000CF3  System.Void OnUpdate()
  RVA=0x04DA6710  token=0x6000CF4  System.Void OnStop()
  RVA=0x0A95DB18  token=0x6000CF5  System.Void SetMethod(System.Reflection.MethodInfo method)
  RVA=0x04DA6720  token=0x6000CF6  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SetProperty
TYPE:  class
TOKEN: 0x200034F
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper
FIELDS:
  protected         NodeCanvas.Framework.Internal.ReflectedActionWrapperfunctionWrapper  // 0x68
PROPERTIES:
  targetMethod  get=0x0A9468AC
  agentType  get=0x0A96674C
  info  get=0x0A966810
METHODS:
  RVA=0x0A95618C  token=0x6000CFA  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A9666AC  token=0x6000CFB  System.Void OnValidate(NodeCanvas.Framework.ITaskSystem ownerSystem)
  RVA=0x0A966590  token=0x6000CFC  System.String OnInit()
  RVA=0x0A966554  token=0x6000CFD  System.Void OnExecute()
  RVA=0x0A9666F8  token=0x6000CFE  System.Void SetMethod(System.Reflection.MethodInfo method)
  RVA=0x04D8E030  token=0x6000CFF  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.DebugBeep
TYPE:  class
TOKEN: 0x2000350
SIZE:  0x68
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
METHODS:
  RVA=0x0A954718  token=0x6000D00  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000D01  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.DebugDrawLine
TYPE:  class
TOKEN: 0x2000351
SIZE:  0x90
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>from  // 0x68
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>to  // 0x70
  public            UnityEngine.Color               color  // 0x78
  public            System.Single                   timeToShow  // 0x88
METHODS:
  RVA=0x0A954724  token=0x6000D02  System.Void OnExecute()
  RVA=0x0A95482C  token=0x6000D03  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.DebugLogText
TYPE:  class
TOKEN: 0x2000352
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>log  // 0x68
  public            System.Single                   labelYOffset  // 0x70
  public            System.Single                   secondsToRun  // 0x74
  public            NodeCanvas.Tasks.Actions.DebugLogText.VerboseModeverboseMode  // 0x78
  public            NodeCanvas.Tasks.Actions.DebugLogText.LogModelogMode  // 0x7c
  public            ParadoxNotion.CompactStatus     finishStatus  // 0x80
PROPERTIES:
  info  get=0x0A954E2C
METHODS:
  RVA=0x0A95485C  token=0x6000D05  System.Void OnExecute()
  RVA=0x0A954CCC  token=0x6000D06  System.Void OnStop()
  RVA=0x0A954D7C  token=0x6000D07  System.Void OnUpdate()
  RVA=0x0A9549A4  token=0x6000D08  System.Void OnGUI()
  RVA=0x0A954DB0  token=0x6000D09  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.DebugLogVariable
TYPE:  class
TOKEN: 0x2000355
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Object>log  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.String>prefix  // 0x70
  public            System.Single                   secondsToRun  // 0x78
  public            ParadoxNotion.CompactStatus     finishStatus  // 0x7c
PROPERTIES:
  info  get=0x0A954F2C
METHODS:
  RVA=0x041E1670  token=0x6000D0B  System.Void OnExecute()
  RVA=0x0A954EFC  token=0x6000D0C  System.Void OnUpdate()
  RVA=0x04DA66C0  token=0x6000D0D  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.ForceFinishGraph
TYPE:  class
TOKEN: 0x2000356
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            ParadoxNotion.CompactStatus     finishStatus  // 0x68
METHODS:
  RVA=0x0A95A67C  token=0x6000D0E  System.Void OnExecute()
  RVA=0x04DA66F0  token=0x6000D0F  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.GraphOwnerControl
TYPE:  class
TOKEN: 0x2000357
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Tasks.Actions.GraphOwnerControl.Controlcontrol  // 0x68
  public            System.Boolean                  waitActionFinish  // 0x6c
PROPERTIES:
  info  get=0x0A95E208
METHODS:
  RVA=0x0A95DFC4  token=0x6000D11  System.Void OnExecute()
  RVA=0x0A95E168  token=0x6000D12  System.Collections.IEnumerator YieldDo()
  RVA=0x0A95DF20  token=0x6000D13  System.Void Do()
  RVA=0x0A95E100  token=0x6000D14  System.Void OnStop()
  RVA=0x0A95E1C4  token=0x6000D15  System.Void .ctor()
  RVA=0x0A95E160  token=0x6000D16  System.Void <OnExecute>b__5_0(System.Boolean s)
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.RunForever
TYPE:  class
TOKEN: 0x200035A
SIZE:  0x68
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
METHODS:
  RVA=0x04D8E030  token=0x6000D1D  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SendEvent
TYPE:  class
TOKEN: 0x200035B
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>eventName  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>delay  // 0x70
  public            System.Boolean                  sendGlobal  // 0x78
PROPERTIES:
  info  get=0x0A9647B8
METHODS:
  RVA=0x0A964658  token=0x6000D1F  System.Void OnUpdate()
  RVA=0x0A96477C  token=0x6000D20  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SendEvent`1
TYPE:  class
TOKEN: 0x200035C
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>eventName  // 0x0
  public            NodeCanvas.Framework.BBParameter<T>eventValue  // 0x0
  public            NodeCanvas.Framework.BBParameter<System.Single>delay  // 0x0
  public            System.Boolean                  sendGlobal  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000D22  System.Void OnUpdate()
  RVA=-1  // not resolved  token=0x6000D23  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SendEventToObjects
TYPE:  class
TOKEN: 0x200035D
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>targetObjects  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.String>eventName  // 0x70
PROPERTIES:
  info  get=0x0A964614
METHODS:
  RVA=0x0A9643FC  token=0x6000D25  System.Void OnExecute()
  RVA=0x04D8E030  token=0x6000D26  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SendEventToObjects`1
TYPE:  class
TOKEN: 0x200035E
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>targetObjects  // 0x0
  public            NodeCanvas.Framework.BBParameter<System.String>eventName  // 0x0
  public            NodeCanvas.Framework.BBParameter<T>eventValue  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000D28  System.Void OnExecute()
  RVA=-1  // not resolved  token=0x6000D29  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.ShoutEvent
TYPE:  class
TOKEN: 0x200035F
SIZE:  0x98
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>eventName  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>shoutRange  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>completionTime  // 0x78
  private           NodeCanvas.Framework.GraphOwner[]owners  // 0x80
  private           System.Boolean[]                receivedOwners  // 0x88
  private           System.Single                   traveledDistance  // 0x90
PROPERTIES:
  info  get=0x0A9670B0
METHODS:
  RVA=0x0A966D28  token=0x6000D2B  System.Void OnExecute()
  RVA=0x0A966DCC  token=0x6000D2C  System.Void OnUpdate()
  RVA=0x0A966BD8  token=0x6000D2D  System.Void OnDrawGizmosSelected()
  RVA=0x0A967030  token=0x6000D2E  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SwitchBehaviourTree
TYPE:  class
TOKEN: 0x2000360
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<NodeCanvas.BehaviourTrees.BehaviourTree>behaviourTree  // 0x68
PROPERTIES:
  info  get=0x0A9678EC
METHODS:
  RVA=0x0A967814  token=0x6000D30  System.Void OnExecute()
  RVA=0x0A9678B0  token=0x6000D31  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.SwitchFSM
TYPE:  class
TOKEN: 0x2000361
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<NodeCanvas.StateMachines.FSM>fsm  // 0x68
PROPERTIES:
  info  get=0x0A967A04
METHODS:
  RVA=0x0A96792C  token=0x6000D33  System.Void OnExecute()
  RVA=0x0A9679C8  token=0x6000D34  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.Wait
TYPE:  class
TOKEN: 0x2000362
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Single>waitTime  // 0x68
  public            ParadoxNotion.CompactStatus     finishStatus  // 0x70
PROPERTIES:
  info  get=0x0A968B88
METHODS:
  RVA=0x0A968AB8  token=0x6000D36  System.Void OnUpdate()
  RVA=0x0A968B2C  token=0x6000D37  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.CheckSpeed
TYPE:  class
TOKEN: 0x2000363
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            ParadoxNotion.CompareMethod     checkType  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>value  // 0x70
  public            System.Single                   differenceThreshold  // 0x78
PROPERTIES:
  info  get=0x0A953718
METHODS:
  RVA=0x0A953614  token=0x6000D39  System.Boolean OnCheck()
  RVA=0x0A9536CC  token=0x6000D3A  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.ExecuteStaticFunction
TYPE:  class
TOKEN: 0x2000364
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ActionTask
IMPLEMENTS: NodeCanvas.Framework.ISubParametersContainer
FIELDS:
  protected         NodeCanvas.Framework.Internal.ReflectedWrapperfunctionWrapper  // 0x68
PROPERTIES:
  targetMethod  get=0x0A9468AC
  info  get=0x0A9574D4
METHODS:
  RVA=0x0A9572F0  token=0x6000D3B  NodeCanvas.Framework.BBParameter[] NodeCanvas.Framework.ISubParametersContainer.GetSubParameters()
  RVA=0x0A9564F8  token=0x6000D3E  System.Void OnValidate(NodeCanvas.Framework.ITaskSystem ownerSystem)
  RVA=0x0A9573F4  token=0x6000D3F  System.String OnInit()
  RVA=0x0A957310  token=0x6000D40  System.Void OnExecute()
  RVA=0x0A956544  token=0x6000D41  System.Void SetMethod(System.Reflection.MethodInfo method)
  RVA=0x04D8E030  token=0x6000D42  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Actions.ExecuteStaticFunction_Multiplatform
TYPE:  class
TOKEN: 0x2000365
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ActionTask
FIELDS:
  protected         ParadoxNotion.Serialization.SerializedMethodInfomethod  // 0x68
  protected         System.Collections.Generic.List<NodeCanvas.Framework.Internal.BBObjectParameter>parameters  // 0x70
  protected         NodeCanvas.Framework.Internal.BBObjectParameterreturnValue  // 0x78
PROPERTIES:
  targetMethod  get=0x0A946100
  info  get=0x0A956FDC
METHODS:
  RVA=0x0A956CD4  token=0x6000D45  System.Void OnValidate(NodeCanvas.Framework.ITaskSystem ownerSystem)
  RVA=0x0A956C4C  token=0x6000D46  System.String OnInit()
  RVA=0x0A956AB4  token=0x6000D47  System.Void OnExecute()
  RVA=0x0A956CFC  token=0x6000D48  System.Void SetMethod(System.Reflection.MethodInfo method)
  RVA=0x0A956F5C  token=0x6000D49  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.InvokeSignal
TYPE:  class
TOKEN: 0x2000285
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ActionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<NodeCanvas.Framework.SignalDefinition>signalDefinition  // 0x68
  public            System.Boolean                  global  // 0x70
  private           System.Collections.Generic.Dictionary<System.String,NodeCanvas.Framework.Internal.BBObjectParameter>argumentsMap  // 0x78
  private           System.Object[]                 args  // 0x80
PROPERTIES:
  info  get=0x0A94BFE8
METHODS:
  RVA=0x0A94BEC4  token=0x60009B9  System.String OnInit()
  RVA=0x0A94BD10  token=0x60009BA  System.Void OnExecute()
  RVA=0x0A94BF5C  token=0x60009BB  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.MecanimCheckBool
TYPE:  class
TOKEN: 0x2000286
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>parameter  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Boolean>value  // 0x70
PROPERTIES:
  info  get=0x0A94E800
METHODS:
  RVA=0x0A94E718  token=0x60009BD  System.Boolean OnCheck()
  RVA=0x0A94E7C4  token=0x60009BE  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.MecanimCheckFloat
TYPE:  class
TOKEN: 0x2000287
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>parameter  // 0x68
  public            ParadoxNotion.CompareMethod     comparison  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>value  // 0x78
PROPERTIES:
  info  get=0x0A94E9AC
METHODS:
  RVA=0x0A94E8A4  token=0x60009C0  System.Boolean OnCheck()
  RVA=0x0A94E970  token=0x60009C1  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.MecanimCheckInt
TYPE:  class
TOKEN: 0x2000288
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>parameter  // 0x68
  public            ParadoxNotion.CompareMethod     comparison  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Int32>value  // 0x78
PROPERTIES:
  info  get=0x0A94EB38
METHODS:
  RVA=0x0A94EA48  token=0x60009C3  System.Boolean OnCheck()
  RVA=0x0A94EAFC  token=0x60009C4  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.MecanimIsInTransition
TYPE:  class
TOKEN: 0x2000289
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Int32>layerIndex  // 0x68
PROPERTIES:
  info  get=0x0A94EC90
METHODS:
  RVA=0x0A94EBD4  token=0x60009C6  System.Boolean OnCheck()
  RVA=0x0A94EC54  token=0x60009C7  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckBoolean
TYPE:  class
TOKEN: 0x200028A
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Boolean>valueA  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Boolean>valueB  // 0x70
PROPERTIES:
  info  get=0x0A94147C
METHODS:
  RVA=0x0A9413C0  token=0x60009C9  System.Boolean OnCheck()
  RVA=0x0A94142C  token=0x60009CA  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckBooleanTrigger
TYPE:  class
TOKEN: 0x200028B
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Boolean>trigger  // 0x68
PROPERTIES:
  info  get=0x0A941380
METHODS:
  RVA=0x0A94130C  token=0x60009CC  System.Boolean OnCheck()
  RVA=0x04D8E020  token=0x60009CD  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckEnum
TYPE:  class
TOKEN: 0x200028C
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            NodeCanvas.Framework.Internal.BBObjectParametervalueA  // 0x68
  public            NodeCanvas.Framework.Internal.BBObjectParametervalueB  // 0x70
PROPERTIES:
  info  get=0x0A944BA4
METHODS:
  RVA=0x0A944A40  token=0x60009CF  System.Boolean OnCheck()
  RVA=0x0A944AB4  token=0x60009D0  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckFloat
TYPE:  class
TOKEN: 0x200028D
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Single>valueA  // 0x68
  public            ParadoxNotion.CompareMethod     checkType  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>valueB  // 0x78
  public            System.Single                   differenceThreshold  // 0x80
PROPERTIES:
  info  get=0x0A9454E0
METHODS:
  RVA=0x0A945458  token=0x60009D2  System.Boolean OnCheck()
  RVA=0x04DA6540  token=0x60009D3  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckInt
TYPE:  class
TOKEN: 0x200028E
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Int32>valueA  // 0x68
  public            ParadoxNotion.CompareMethod     checkType  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Int32>valueB  // 0x78
PROPERTIES:
  info  get=0x0A9454E0
METHODS:
  RVA=0x0A9468CC  token=0x60009D5  System.Boolean OnCheck()
  RVA=0x04D8E020  token=0x60009D6  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckNull
TYPE:  class
TOKEN: 0x200028F
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Object>variable  // 0x68
PROPERTIES:
  info  get=0x0A948830
METHODS:
  RVA=0x0A9487E4  token=0x60009D8  System.Boolean OnCheck()
  RVA=0x04D8E020  token=0x60009D9  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckString
TYPE:  class
TOKEN: 0x2000290
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>valueA  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.String>valueB  // 0x70
PROPERTIES:
  info  get=0x0A949490
METHODS:
  RVA=0x0A94941C  token=0x60009DB  System.Boolean OnCheck()
  RVA=0x04D8E020  token=0x60009DC  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckUnityObject
TYPE:  class
TOKEN: 0x2000291
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Object>valueA  // 0x68
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Object>valueB  // 0x70
PROPERTIES:
  info  get=0x0A94A8E8
METHODS:
  RVA=0x0A94A858  token=0x60009DE  System.Boolean OnCheck()
  RVA=0x04D8E020  token=0x60009DF  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckVariable`1
TYPE:  class
TOKEN: 0x2000292
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<T>valueA  // 0x0
  public            NodeCanvas.Framework.BBParameter<T>valueB  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60009E1  System.Boolean OnCheck()
  RVA=-1  // not resolved  token=0x60009E2  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckVectorDistance
TYPE:  class
TOKEN: 0x2000293
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>vectorA  // 0x68
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>vectorB  // 0x70
  public            ParadoxNotion.CompareMethod     comparison  // 0x78
  public            NodeCanvas.Framework.BBParameter<System.Single>distance  // 0x80
PROPERTIES:
  info  get=0x0A94AA60
METHODS:
  RVA=0x0A94A970  token=0x60009E4  System.Boolean OnCheck()
  RVA=0x04D8E020  token=0x60009E5  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.TryGetValue`1
TYPE:  class
TOKEN: 0x2000294
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.Dictionary<System.String,T>>targetDictionary  // 0x0
  public            NodeCanvas.Framework.BBParameter<System.String>key  // 0x0
  public            NodeCanvas.Framework.BBParameter<T>saveValueAs  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60009E7  System.Boolean OnCheck()
  RVA=-1  // not resolved  token=0x60009E8  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.ListContainsElement`1
TYPE:  class
TOKEN: 0x2000295
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<T>>targetList  // 0x0
  public            NodeCanvas.Framework.BBParameter<T>checkElement  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60009EA  System.Boolean OnCheck()
  RVA=-1  // not resolved  token=0x60009EB  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.ListIsEmpty
TYPE:  class
TOKEN: 0x2000296
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.IList>targetList  // 0x68
PROPERTIES:
  info  get=0x0A94CA0C
METHODS:
  RVA=0x0A94C9B0  token=0x60009ED  System.Boolean OnCheck()
  RVA=0x04D8E020  token=0x60009EE  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.BBVariableChanged
TYPE:  class
TOKEN: 0x2000297
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            NodeCanvas.Framework.Internal.BBObjectParametertargetVariable  // 0x68
PROPERTIES:
  info  get=0x0A93D42C
METHODS:
  RVA=0x0A93D3CC  token=0x60009F0  System.String OnInit()
  RVA=0x0A93D334  token=0x60009F1  System.Void OnEnable()
  RVA=0x0A93D29C  token=0x60009F2  System.Void OnDisable()
  RVA=0x012081B0  token=0x60009F3  System.Boolean OnCheck()
  RVA=0x0A93D41C  token=0x60009F4  System.Void OnValueChanged(System.Object varValue)
  RVA=0x04D8E020  token=0x60009F5  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.StringContains
TYPE:  class
TOKEN: 0x2000298
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>targetString  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.String>checkString  // 0x70
PROPERTIES:
  info  get=0x0A951F54
METHODS:
  RVA=0x0A951ED8  token=0x60009F7  System.Boolean OnCheck()
  RVA=0x04D8E020  token=0x60009F8  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CanSeeTarget
TYPE:  class
TOKEN: 0x2000299
SIZE:  0xE0
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>target  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>maxDistance  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.LayerMask>layerMask  // 0x78
  public            NodeCanvas.Framework.BBParameter<System.Single>awarnessDistance  // 0x80
  public            NodeCanvas.Framework.BBParameter<System.Single>viewAngle  // 0x88
  public            UnityEngine.Vector3             offset  // 0x90
  private           UnityEngine.RaycastHit          hit  // 0xa0
PROPERTIES:
  info  get=0x0A9412AC
METHODS:
  RVA=0x0A9406AC  token=0x60009FA  System.Boolean OnCheck()
  RVA=0x0A940BBC  token=0x60009FB  System.Void OnDrawGizmosSelected()
  RVA=0x0A9411DC  token=0x60009FC  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CanSeeTarget2D
TYPE:  class
TOKEN: 0x200029A
SIZE:  0xC0
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>target  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>maxDistance  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.LayerMask>layerMask  // 0x78
  public            NodeCanvas.Framework.BBParameter<System.Single>awarnessDistance  // 0x80
  public            NodeCanvas.Framework.BBParameter<System.Single>viewAngle  // 0x88
  public            UnityEngine.Vector2             offset  // 0x90
  private           UnityEngine.RaycastHit2D        hit  // 0x98
PROPERTIES:
  info  get=0x0A93E068
METHODS:
  RVA=0x0A93D488  token=0x60009FE  System.Boolean OnCheck()
  RVA=0x0A93D914  token=0x60009FF  System.Void OnDrawGizmosSelected()
  RVA=0x0A93DF98  token=0x6000A00  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CanSeeTargetAny
TYPE:  class
TOKEN: 0x200029B
SIZE:  0xF0
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>targetObjects  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>maxDistance  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.LayerMask>layerMask  // 0x78
  public            NodeCanvas.Framework.BBParameter<System.Single>awarnessDistance  // 0x80
  public            NodeCanvas.Framework.BBParameter<System.Single>viewAngle  // 0x88
  public            UnityEngine.Vector3             offset  // 0x90
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>allResults  // 0xa0
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>closerResult  // 0xa8
  private           UnityEngine.RaycastHit          hit  // 0xb0
PROPERTIES:
  info  get=0x0A94064C
METHODS:
  RVA=0x0A93F350  token=0x6000A02  System.Boolean OnCheck()
  RVA=0x0A93FE9C  token=0x6000A03  System.Void OnDrawGizmosSelected()
  RVA=0x0A94057C  token=0x6000A04  System.Void .ctor()
  RVA=0x0A9404BC  token=0x6000A05  System.Single <OnCheck>b__11_0(UnityEngine.GameObject x)
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CanSeeTargetAny2D
TYPE:  class
TOKEN: 0x200029C
SIZE:  0xA8
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>targetObjects  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>maxDistance  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.LayerMask>layerMask  // 0x78
  public            NodeCanvas.Framework.BBParameter<System.Single>awarnessDistance  // 0x80
  public            NodeCanvas.Framework.BBParameter<System.Single>viewAngle  // 0x88
  public            UnityEngine.Vector2             offset  // 0x90
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>allResults  // 0x98
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>closerResult  // 0xa0
PROPERTIES:
  info  get=0x0A93F2F0
METHODS:
  RVA=0x0A93E0C8  token=0x6000A07  System.Boolean OnCheck()
  RVA=0x0A93EB00  token=0x6000A08  System.Void OnDrawGizmosSelected()
  RVA=0x0A93F220  token=0x6000A09  System.Void .ctor()
  RVA=0x0A93F160  token=0x6000A0A  System.Single <OnCheck>b__10_0(UnityEngine.GameObject x)
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckDistanceToGameObject
TYPE:  class
TOKEN: 0x200029D
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>checkTarget  // 0x68
  public            ParadoxNotion.CompareMethod     checkType  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>distance  // 0x78
  public            System.Single                   floatingPoint  // 0x80
PROPERTIES:
  info  get=0x0A944934
METHODS:
  RVA=0x0A9446B0  token=0x6000A0C  System.Boolean OnCheck()
  RVA=0x0A9447DC  token=0x6000A0D  System.Void OnDrawGizmosSelected()
  RVA=0x0A9448C4  token=0x6000A0E  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckDistanceToGameObject2D
TYPE:  class
TOKEN: 0x200029E
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>checkTarget  // 0x68
  public            ParadoxNotion.CompareMethod     checkType  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>distance  // 0x78
  public            System.Single                   floatingPoint  // 0x80
PROPERTIES:
  info  get=0x0A943584
METHODS:
  RVA=0x0A9432C4  token=0x6000A10  System.Boolean OnCheck()
  RVA=0x0A943400  token=0x6000A11  System.Void OnDrawGizmosSelected()
  RVA=0x0A943514  token=0x6000A12  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckDistanceToGameObjectAny
TYPE:  class
TOKEN: 0x200029F
SIZE:  0x98
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>targetObjects  // 0x68
  public            ParadoxNotion.CompareMethod     checkType  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>distance  // 0x78
  public            System.Single                   floatingPoint  // 0x80
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>allResults  // 0x88
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>closerResult  // 0x90
PROPERTIES:
  info  get=0x0A9445A4
METHODS:
  RVA=0x0A943ECC  token=0x6000A14  System.Boolean OnCheck()
  RVA=0x0A94438C  token=0x6000A15  System.Void OnDrawGizmosSelected()
  RVA=0x0A944534  token=0x6000A16  System.Void .ctor()
  RVA=0x0A944474  token=0x6000A17  System.Single <OnCheck>b__8_0(UnityEngine.GameObject x)
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckDistanceToGameObjectAny2D
TYPE:  class
TOKEN: 0x20002A0
SIZE:  0x98
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>targetObjects  // 0x68
  public            ParadoxNotion.CompareMethod     checkType  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>distance  // 0x78
  public            System.Single                   floatingPoint  // 0x80
  public            NodeCanvas.Framework.BBParameter<System.Collections.Generic.List<UnityEngine.GameObject>>allResults  // 0x88
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>closerResult  // 0x90
PROPERTIES:
  info  get=0x0A943DC0
METHODS:
  RVA=0x0A943690  token=0x6000A19  System.Boolean OnCheck()
  RVA=0x0A943B60  token=0x6000A1A  System.Void OnDrawGizmosSelected()
  RVA=0x0A943D50  token=0x6000A1B  System.Void .ctor()
  RVA=0x0A943C74  token=0x6000A1C  System.Single <OnCheck>b__8_0(UnityEngine.GameObject x)
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckLOS
TYPE:  class
TOKEN: 0x20002A1
SIZE:  0xD0
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>LOSTarget  // 0x68
  public            NodeCanvas.Framework.BBParameter<UnityEngine.LayerMask>layerMask  // 0x70
  public            UnityEngine.Vector3             offset  // 0x78
  public            NodeCanvas.Framework.BBParameter<System.Single>saveDistanceAs  // 0x88
  private           UnityEngine.RaycastHit          hit  // 0x90
PROPERTIES:
  info  get=0x0A947590
METHODS:
  RVA=0x0A9470B8  token=0x6000A1E  System.Boolean OnCheck()
  RVA=0x0A947368  token=0x6000A1F  System.Void OnDrawGizmosSelected()
  RVA=0x0A947538  token=0x6000A20  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckLOS2D
TYPE:  class
TOKEN: 0x20002A2
SIZE:  0x90
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>LOSTarget  // 0x68
  public            NodeCanvas.Framework.BBParameter<UnityEngine.LayerMask>layerMask  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>saveDistanceAs  // 0x78
  protected         UnityEngine.Collider2D          agentCollider  // 0x80
  private           UnityEngine.RaycastHit2D[]      hits  // 0x88
PROPERTIES:
  info  get=0x0A947060
METHODS:
  RVA=0x0A946A28  token=0x6000A22  System.Boolean OnCheck()
  RVA=0x0A946E5C  token=0x6000A23  System.Void OnDrawGizmosSelected()
  RVA=0x0A947008  token=0x6000A24  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.HasComponent`1
TYPE:  class
TOKEN: 0x20002A4
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000A28  System.Boolean OnCheck()
  RVA=-1  // not resolved  token=0x6000A29  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.IsActive
TYPE:  class
TOKEN: 0x20002A5
SIZE:  0x68
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
PROPERTIES:
  info  get=0x0A94C0A8
METHODS:
  RVA=0x0A94C00C  token=0x6000A2B  System.Boolean OnCheck()
  RVA=0x0A94C06C  token=0x6000A2C  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.IsInFront
TYPE:  class
TOKEN: 0x20002A6
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>checkTarget  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>viewAngle  // 0x70
PROPERTIES:
  info  get=0x0A94C890
METHODS:
  RVA=0x0A94C500  token=0x6000A2E  System.Boolean OnCheck()
  RVA=0x0A94C670  token=0x6000A2F  System.Void OnDrawGizmosSelected()
  RVA=0x0A94C834  token=0x6000A30  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.IsInFront2D
TYPE:  class
TOKEN: 0x20002A7
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>checkTarget  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>viewAngle  // 0x70
PROPERTIES:
  info  get=0x0A94C4A4
METHODS:
  RVA=0x0A94C0F0  token=0x6000A32  System.Boolean OnCheck()
  RVA=0x0A94C268  token=0x6000A33  System.Void OnDrawGizmosSelected()
  RVA=0x0A94C448  token=0x6000A34  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.IsWithinLayerMask
TYPE:  class
TOKEN: 0x20002A8
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.LayerMask>targetLayers  // 0x68
METHODS:
  RVA=0x0A94C8EC  token=0x6000A35  System.Boolean OnCheck()
  RVA=0x0A94C974  token=0x6000A36  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckButtonInput
TYPE:  class
TOKEN: 0x20002A9
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            ParadoxNotion.PressTypes        pressType  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.String>buttonName  // 0x70
PROPERTIES:
  info  get=0x0A941614
METHODS:
  RVA=0x0A941504  token=0x6000A38  System.Boolean OnCheck()
  RVA=0x0A9415B8  token=0x6000A39  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckKeyboardInput
TYPE:  class
TOKEN: 0x20002AA
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            ParadoxNotion.PressTypes        pressType  // 0x68
  public            UnityEngine.KeyCode             key  // 0x6c
PROPERTIES:
  info  get=0x0A946978
METHODS:
  RVA=0x0A946944  token=0x6000A3B  System.Boolean OnCheck()
  RVA=0x04DA6560  token=0x6000A3C  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckMousePick
TYPE:  class
TOKEN: 0x20002AB
SIZE:  0xC8
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            ParadoxNotion.ButtonKeys        buttonKey  // 0x68
  public            System.Int32                    layer  // 0x6c
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveGoAs  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>saveDistanceAs  // 0x78
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>savePosAs  // 0x80
  private           UnityEngine.RaycastHit          hit  // 0x88
PROPERTIES:
  info  get=0x0A9483A0
METHODS:
  RVA=0x0A9481E8  token=0x6000A3E  System.Boolean OnCheck()
  RVA=0x04D8E020  token=0x6000A3F  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckMousePick2D
TYPE:  class
TOKEN: 0x20002AC
SIZE:  0xB0
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            ParadoxNotion.ButtonKeys        buttonKey  // 0x68
  public            UnityEngine.LayerMask           mask  // 0x6c
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveGoAs  // 0x70
  public            NodeCanvas.Framework.BBParameter<System.Single>saveDistanceAs  // 0x78
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>savePosAs  // 0x80
  private           System.Int32                    buttonID  // 0x88
  private           UnityEngine.RaycastHit2D        hit  // 0x8c
PROPERTIES:
  info  get=0x0A9480A0
METHODS:
  RVA=0x0A947E38  token=0x6000A41  System.Boolean OnCheck()
  RVA=0x04DA6570  token=0x6000A42  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckCSharpEvent_0
TYPE:  class
TOKEN: 0x20002AD
SIZE:  0x20
FIELDS:
  public            System.Type                     targetType  // 0x10
  public            System.String                   eventName  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000A43  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckCSharpEvent_0`1
TYPE:  class
TOKEN: 0x20002AE
FIELDS:
  public            System.Type                     targetType  // 0x0
  public            System.String                   eventName  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000A44  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckCSharpEventValue_0`1
TYPE:  class
TOKEN: 0x20002AF
FIELDS:
  public            System.Type                     targetType  // 0x0
  public            System.String                   eventName  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000A45  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckStaticCSharpEvent
TYPE:  class
TOKEN: 0x20002B0
SIZE:  0x20
FIELDS:
  public            System.Type                     targetType  // 0x10
  public            System.String                   eventName  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000A46  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckStaticCSharpEvent`1
TYPE:  class
TOKEN: 0x20002B1
FIELDS:
  public            System.Type                     targetType  // 0x0
  public            System.String                   eventName  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000A47  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckCSharpEvent
TYPE:  class
TOKEN: 0x20002B2
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ConditionTask
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper ParadoxNotion.Serialization.FullSerializer.IMigratable`1 ParadoxNotion.Serialization.FullSerializer.IMigratable ParadoxNotion.Serialization.FullSerializer.IMigratable`1
FIELDS:
  private           ParadoxNotion.Serialization.SerializedEventInfoeventInfo  // 0x68
  private           System.Delegate                 handler  // 0x70
PROPERTIES:
  targetEvent  get=0x092C37DC
  agentType  get=0x0A941B24
  info  get=0x0A941BF8
METHODS:
  RVA=0x0A941914  token=0x6000A48  System.Void ParadoxNotion.Serialization.FullSerializer.IMigratable<NodeCanvas.Tasks.Conditions.CheckCSharpEvent_0>.Migrate(NodeCanvas.Tasks.Conditions.CheckCSharpEvent_0 model)
  RVA=0x0A9419D8  token=0x6000A49  System.Void ParadoxNotion.Serialization.FullSerializer.IMigratable<NodeCanvas.Tasks.Conditions.CheckStaticCSharpEvent>.Migrate(NodeCanvas.Tasks.Conditions.CheckStaticCSharpEvent model)
  RVA=0x04D86230  token=0x6000A4D  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A9417F4  token=0x6000A4E  System.String OnInit()
  RVA=0x0A941750  token=0x6000A4F  System.Void OnEnable()
  RVA=0x0A9416AC  token=0x6000A50  System.Void OnDisable()
  RVA=0x0A93D41C  token=0x6000A51  System.Void Raised()
  RVA=0x012081B0  token=0x6000A52  System.Boolean OnCheck()
  RVA=0x0A941A9C  token=0x6000A53  System.Void SetTargetEvent(System.Reflection.EventInfo info)
  RVA=0x04D8E020  token=0x6000A54  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckCSharpEvent`1
TYPE:  class
TOKEN: 0x20002B3
EXTENDS: NodeCanvas.Framework.ConditionTask
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper ParadoxNotion.Serialization.FullSerializer.IMigratable`1 ParadoxNotion.Serialization.FullSerializer.IMigratable ParadoxNotion.Serialization.FullSerializer.IMigratable`1
FIELDS:
  private           ParadoxNotion.Serialization.SerializedEventInfoeventInfo  // 0x0
  private           NodeCanvas.Framework.BBParameter<T>saveAs  // 0x0
  private           System.Delegate                 handler  // 0x0
PROPERTIES:
  targetEvent  get=-1  // not resolved
  agentType  get=-1  // not resolved
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000A55  System.Void ParadoxNotion.Serialization.FullSerializer.IMigratable<NodeCanvas.Tasks.Conditions.CheckCSharpEvent_0<T>>.Migrate(NodeCanvas.Tasks.Conditions.CheckCSharpEvent_0<T> model)
  RVA=-1  // not resolved  token=0x6000A56  System.Void ParadoxNotion.Serialization.FullSerializer.IMigratable<NodeCanvas.Tasks.Conditions.CheckStaticCSharpEvent<T>>.Migrate(NodeCanvas.Tasks.Conditions.CheckStaticCSharpEvent<T> model)
  RVA=-1  // not resolved  token=0x6000A5A  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=-1  // not resolved  token=0x6000A5B  System.String OnInit()
  RVA=-1  // not resolved  token=0x6000A5C  System.Void OnEnable()
  RVA=-1  // not resolved  token=0x6000A5D  System.Void OnDisable()
  RVA=-1  // not resolved  token=0x6000A5E  System.Void Raised(T eventValue)
  RVA=-1  // not resolved  token=0x6000A5F  System.Boolean OnCheck()
  RVA=-1  // not resolved  token=0x6000A60  System.Void SetTargetEvent(System.Reflection.EventInfo info)
  RVA=-1  // not resolved  token=0x6000A61  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckCSharpEventValue`1
TYPE:  class
TOKEN: 0x20002B4
EXTENDS: NodeCanvas.Framework.ConditionTask
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper ParadoxNotion.Serialization.FullSerializer.IMigratable`1 ParadoxNotion.Serialization.FullSerializer.IMigratable
FIELDS:
  private           ParadoxNotion.Serialization.SerializedEventInfoeventInfo  // 0x0
  private           NodeCanvas.Framework.BBParameter<T>checkValue  // 0x0
  private           System.Delegate                 handler  // 0x0
PROPERTIES:
  targetEvent  get=-1  // not resolved
  agentType  get=-1  // not resolved
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000A62  System.Void ParadoxNotion.Serialization.FullSerializer.IMigratable<NodeCanvas.Tasks.Conditions.CheckCSharpEventValue_0<T>>.Migrate(NodeCanvas.Tasks.Conditions.CheckCSharpEventValue_0<T> model)
  RVA=-1  // not resolved  token=0x6000A66  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=-1  // not resolved  token=0x6000A67  System.String OnInit()
  RVA=-1  // not resolved  token=0x6000A68  System.Void OnEnable()
  RVA=-1  // not resolved  token=0x6000A69  System.Void OnDisable()
  RVA=-1  // not resolved  token=0x6000A6A  System.Void Raised(T eventValue)
  RVA=-1  // not resolved  token=0x6000A6B  System.Boolean OnCheck()
  RVA=-1  // not resolved  token=0x6000A6C  System.Void SetTargetEvent(System.Reflection.EventInfo info)
  RVA=-1  // not resolved  token=0x6000A6D  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckField_0
TYPE:  class
TOKEN: 0x20002B5
SIZE:  0x28
FIELDS:
  public            NodeCanvas.Framework.BBParametercheckValue  // 0x10
  public            System.Type                     targetType  // 0x18
  public            System.String                   fieldName  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000A6E  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckField
TYPE:  class
TOKEN: 0x20002B6
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ConditionTask
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper ParadoxNotion.Serialization.FullSerializer.IMigratable`1 ParadoxNotion.Serialization.FullSerializer.IMigratable
FIELDS:
  protected         NodeCanvas.Framework.Internal.BBObjectParametercheckValue  // 0x68
  protected         ParadoxNotion.CompareMethod     comparison  // 0x70
  protected         ParadoxNotion.Serialization.SerializedFieldInfofield  // 0x78
PROPERTIES:
  targetField  get=0x0A945448
  agentType  get=0x0A94517C
  info  get=0x0A94524C
METHODS:
  RVA=0x0A944F34  token=0x6000A6F  System.Void ParadoxNotion.Serialization.FullSerializer.IMigratable<NodeCanvas.Tasks.Conditions.CheckField_0>.Migrate(NodeCanvas.Tasks.Conditions.CheckField_0 model)
  RVA=0x04D862A0  token=0x6000A73  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A944E9C  token=0x6000A74  System.String OnInit()
  RVA=0x0A944C2C  token=0x6000A75  System.Boolean OnCheck()
  RVA=0x0A9450C8  token=0x6000A76  System.Void SetTargetField(System.Reflection.FieldInfo newField)
  RVA=0x04D8E020  token=0x6000A77  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckFunction_Multiplatform
TYPE:  class
TOKEN: 0x20002B7
SIZE:  0x98
EXTENDS: NodeCanvas.Framework.ConditionTask
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper
FIELDS:
  protected         ParadoxNotion.Serialization.SerializedMethodInfomethod  // 0x68
  protected         System.Collections.Generic.List<NodeCanvas.Framework.Internal.BBObjectParameter>parameters  // 0x70
  protected         ParadoxNotion.CompareMethod     comparison  // 0x78
  protected         NodeCanvas.Framework.Internal.BBObjectParametercheckValue  // 0x80
  private           System.Object[]                 args  // 0x88
  private           System.Boolean[]                parameterIsByRef  // 0x90
PROPERTIES:
  targetMethod  get=0x0A946100
  agentType  get=0x0A945D70
  info  get=0x0A945E34
METHODS:
  RVA=0x04D86230  token=0x6000A7B  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A945A8C  token=0x6000A7C  System.Void OnValidate(NodeCanvas.Framework.ITaskSystem ownerSystem)
  RVA=0x0A94590C  token=0x6000A7D  System.String OnInit()
  RVA=0x0A94555C  token=0x6000A7E  System.Boolean OnCheck()
  RVA=0x0A945AB4  token=0x6000A7F  System.Void SetMethod(System.Reflection.MethodInfo method)
  RVA=0x0A945CF4  token=0x6000A80  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckProperty_Multiplatform
TYPE:  class
TOKEN: 0x20002B8
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ConditionTask
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper
FIELDS:
  protected         ParadoxNotion.Serialization.SerializedMethodInfomethod  // 0x68
  protected         NodeCanvas.Framework.Internal.BBObjectParametercheckValue  // 0x70
  protected         ParadoxNotion.CompareMethod     comparison  // 0x78
PROPERTIES:
  targetMethod  get=0x0A946100
  agentType  get=0x0A948C50
  info  get=0x0A948D14
METHODS:
  RVA=0x04D86230  token=0x6000A84  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A948B74  token=0x6000A85  System.Void OnValidate(NodeCanvas.Framework.ITaskSystem ownerSystem)
  RVA=0x0A948B0C  token=0x6000A86  System.String OnInit()
  RVA=0x0A94888C  token=0x6000A87  System.Boolean OnCheck()
  RVA=0x0A948B9C  token=0x6000A88  System.Void SetMethod(System.Reflection.MethodInfo method)
  RVA=0x04D8E020  token=0x6000A89  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckUnityEvent_0
TYPE:  class
TOKEN: 0x20002B9
SIZE:  0x20
FIELDS:
  public            System.Type                     targetType  // 0x10
  public            System.String                   eventName  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000A8A  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckUnityEvent_0`1
TYPE:  class
TOKEN: 0x20002BA
FIELDS:
  public            System.Type                     targetType  // 0x0
  public            System.String                   eventName  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000A8B  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckUnityEventValue_0`1
TYPE:  class
TOKEN: 0x20002BB
FIELDS:
  public            System.Type                     targetType  // 0x0
  public            System.String                   eventName  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000A8C  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckUnityEvent
TYPE:  class
TOKEN: 0x20002BC
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ConditionTask
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper ParadoxNotion.Serialization.FullSerializer.IMigratable`1 ParadoxNotion.Serialization.FullSerializer.IMigratable
FIELDS:
  private           ParadoxNotion.Serialization.SerializedUnityEventInfo_eventInfo  // 0x68
  private           UnityEngine.Events.UnityEvent   unityEvent  // 0x70
PROPERTIES:
  targetMember  get=0x0936B978
  isStatic  get=0x0936B940
  eventType  get=0x09369E8C
  targetEventField  get=0x0936B960
  targetEventProp  get=0x0936B96C
  agentType  get=0x0A94A6C8
  info  get=0x0A94A780
METHODS:
  RVA=0x0A94A588  token=0x6000A8D  System.Void ParadoxNotion.Serialization.FullSerializer.IMigratable<NodeCanvas.Tasks.Conditions.CheckUnityEvent_0>.Migrate(NodeCanvas.Tasks.Conditions.CheckUnityEvent_0 model)
  RVA=0x04D86230  token=0x6000A95  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=0x0A94A40C  token=0x6000A96  System.String OnInit()
  RVA=0x0A94A378  token=0x6000A97  System.Void OnEnable()
  RVA=0x0A94A2E4  token=0x6000A98  System.Void OnDisable()
  RVA=0x0A93D41C  token=0x6000A99  System.Void Raised()
  RVA=0x012081B0  token=0x6000A9A  System.Boolean OnCheck()
  RVA=0x0A94A640  token=0x6000A9B  System.Void SetTargetEvent(System.Reflection.MemberInfo newMember)
  RVA=0x04D8E020  token=0x6000A9C  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckUnityEvent`1
TYPE:  class
TOKEN: 0x20002BD
EXTENDS: NodeCanvas.Framework.ConditionTask
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper ParadoxNotion.Serialization.FullSerializer.IMigratable`1 ParadoxNotion.Serialization.FullSerializer.IMigratable
FIELDS:
  private           ParadoxNotion.Serialization.SerializedUnityEventInfo_eventInfo  // 0x0
  private           NodeCanvas.Framework.BBParameter<T>saveAs  // 0x0
  private           UnityEngine.Events.UnityEvent<T>unityEvent  // 0x0
PROPERTIES:
  targetMember  get=-1  // not resolved
  isStatic  get=-1  // not resolved
  eventType  get=-1  // not resolved
  targetEventField  get=-1  // not resolved
  targetEventProp  get=-1  // not resolved
  agentType  get=-1  // not resolved
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000A9D  System.Void ParadoxNotion.Serialization.FullSerializer.IMigratable<NodeCanvas.Tasks.Conditions.CheckUnityEvent_0<T>>.Migrate(NodeCanvas.Tasks.Conditions.CheckUnityEvent_0<T> model)
  RVA=-1  // not resolved  token=0x6000AA5  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=-1  // not resolved  token=0x6000AA6  System.String OnInit()
  RVA=-1  // not resolved  token=0x6000AA7  System.Void OnEnable()
  RVA=-1  // not resolved  token=0x6000AA8  System.Void OnDisable()
  RVA=-1  // not resolved  token=0x6000AA9  System.Void Raised(T eventValue)
  RVA=-1  // not resolved  token=0x6000AAA  System.Boolean OnCheck()
  RVA=-1  // not resolved  token=0x6000AAB  System.Void SetTargetEvent(System.Reflection.MemberInfo newMember)
  RVA=-1  // not resolved  token=0x6000AAC  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckUnityEventValue`1
TYPE:  class
TOKEN: 0x20002BE
EXTENDS: NodeCanvas.Framework.ConditionTask
IMPLEMENTS: NodeCanvas.Framework.IReflectedWrapper ParadoxNotion.Serialization.FullSerializer.IMigratable`1 ParadoxNotion.Serialization.FullSerializer.IMigratable
FIELDS:
  private           ParadoxNotion.Serialization.SerializedUnityEventInfo_eventInfo  // 0x0
  private           NodeCanvas.Framework.BBParameter<T>checkValue  // 0x0
  private           UnityEngine.Events.UnityEvent<T>unityEvent  // 0x0
PROPERTIES:
  targetMember  get=-1  // not resolved
  isStatic  get=-1  // not resolved
  eventType  get=-1  // not resolved
  targetEventField  get=-1  // not resolved
  targetEventProp  get=-1  // not resolved
  agentType  get=-1  // not resolved
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000AAD  System.Void ParadoxNotion.Serialization.FullSerializer.IMigratable<NodeCanvas.Tasks.Conditions.CheckUnityEventValue_0<T>>.Migrate(NodeCanvas.Tasks.Conditions.CheckUnityEventValue_0<T> model)
  RVA=-1  // not resolved  token=0x6000AB5  ParadoxNotion.Serialization.ISerializedReflectedInfo NodeCanvas.Framework.IReflectedWrapper.GetSerializedInfo()
  RVA=-1  // not resolved  token=0x6000AB6  System.String OnInit()
  RVA=-1  // not resolved  token=0x6000AB7  System.Void OnEnable()
  RVA=-1  // not resolved  token=0x6000AB8  System.Void OnDisable()
  RVA=-1  // not resolved  token=0x6000AB9  System.Void Raised(T eventValue)
  RVA=-1  // not resolved  token=0x6000ABA  System.Boolean OnCheck()
  RVA=-1  // not resolved  token=0x6000ABB  System.Void SetTargetEvent(System.Reflection.MemberInfo newMember)
  RVA=-1  // not resolved  token=0x6000ABC  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckFunction
TYPE:  class
TOKEN: 0x20002BF
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  protected         NodeCanvas.Framework.Internal.ReflectedFunctionWrapperfunctionWrapper  // 0x68
  protected         NodeCanvas.Framework.BBParametercheckValue  // 0x70
  protected         ParadoxNotion.CompareMethod     comparison  // 0x78
PROPERTIES:
  targetMethod  get=0x0A9468AC
  agentType  get=0x0A946528
  info  get=0x0A9465EC
METHODS:
  RVA=0x0A946444  token=0x6000AC0  System.Void OnValidate(NodeCanvas.Framework.ITaskSystem ownerSystem)
  RVA=0x0A946328  token=0x6000AC1  System.String OnInit()
  RVA=0x0A946110  token=0x6000AC2  System.Boolean OnCheck()
  RVA=0x0A946490  token=0x6000AC3  System.Void SetMethod(System.Reflection.MethodInfo method)
  RVA=0x04D8E020  token=0x6000AC4  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckProperty
TYPE:  class
TOKEN: 0x20002C0
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  protected         NodeCanvas.Framework.Internal.ReflectedFunctionWrapperfunctionWrapper  // 0x68
  protected         NodeCanvas.Framework.BBParametercheckValue  // 0x70
  protected         ParadoxNotion.CompareMethod     comparison  // 0x78
PROPERTIES:
  targetMethod  get=0x0A9468AC
  agentType  get=0x0A9491D0
  info  get=0x0A949294
METHODS:
  RVA=0x0A946444  token=0x6000AC8  System.Void OnValidate(NodeCanvas.Framework.ITaskSystem ownerSystem)
  RVA=0x0A9490B4  token=0x6000AC9  System.String OnInit()
  RVA=0x0A948E9C  token=0x6000ACA  System.Boolean OnCheck()
  RVA=0x0A946490  token=0x6000ACB  System.Void SetMethod(System.Reflection.MethodInfo method)
  RVA=0x04D8E020  token=0x6000ACC  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckCollision_Rigidbody
TYPE:  class
TOKEN: 0x20002C1
SIZE:  0x98
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            ParadoxNotion.CollisionTypes    checkType  // 0x68
  public            System.Boolean                  specifiedTagOnly  // 0x6c
  public            System.String                   objectTag  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveGameObjectAs  // 0x78
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>saveContactPoint  // 0x80
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>saveContactNormal  // 0x88
  private           System.Boolean                  stay  // 0x90
PROPERTIES:
  info  get=0x0A943104
METHODS:
  RVA=0x0A942FC0  token=0x6000ACE  System.Void OnEnable()
  RVA=0x0A942ECC  token=0x6000ACF  System.Void OnDisable()
  RVA=0x0A941CD0  token=0x6000AD0  System.Boolean OnCheck()
  RVA=0x0A942C60  token=0x6000AD1  System.Void OnCollisionEnter(ParadoxNotion.EventData<UnityEngine.Collision> data)
  RVA=0x0A942E00  token=0x6000AD2  System.Void OnCollisionExit(ParadoxNotion.EventData<UnityEngine.Collision> data)
  RVA=0x0A9430B4  token=0x6000AD3  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckCollision
TYPE:  class
TOKEN: 0x20002C2
SIZE:  0x98
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            ParadoxNotion.CollisionTypes    checkType  // 0x68
  public            System.Boolean                  specifiedTagOnly  // 0x6c
  public            System.String                   objectTag  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveGameObjectAs  // 0x78
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>saveContactPoint  // 0x80
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>saveContactNormal  // 0x88
  private           System.Boolean                  stay  // 0x90
PROPERTIES:
  info  get=0x0A94320C
METHODS:
  RVA=0x0A942B6C  token=0x6000AD5  System.Void OnEnable()
  RVA=0x0A942A78  token=0x6000AD6  System.Void OnDisable()
  RVA=0x0A941CD0  token=0x6000AD7  System.Boolean OnCheck()
  RVA=0x0A94280C  token=0x6000AD8  System.Void OnCollisionEnter(ParadoxNotion.EventData<UnityEngine.Collision> data)
  RVA=0x0A9429AC  token=0x6000AD9  System.Void OnCollisionExit(ParadoxNotion.EventData<UnityEngine.Collision> data)
  RVA=0x0A9431BC  token=0x6000ADA  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckCollision2D_Rigidbody
TYPE:  class
TOKEN: 0x20002C3
SIZE:  0x98
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            ParadoxNotion.CollisionTypes    checkType  // 0x68
  public            System.Boolean                  specifiedTagOnly  // 0x6c
  public            System.String                   objectTag  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveGameObjectAs  // 0x78
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>saveContactPoint  // 0x80
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>saveContactNormal  // 0x88
  private           System.Boolean                  stay  // 0x90
PROPERTIES:
  info  get=0x0A94264C
METHODS:
  RVA=0x0A941CD0  token=0x6000ADC  System.Boolean OnCheck()
  RVA=0x0A942508  token=0x6000ADD  System.Void OnEnable()
  RVA=0x0A942414  token=0x6000ADE  System.Void OnDisable()
  RVA=0x0A942170  token=0x6000ADF  System.Void OnCollisionEnter2D(ParadoxNotion.EventData<UnityEngine.Collision2D> data)
  RVA=0x0A942348  token=0x6000AE0  System.Void OnCollisionExit2D(ParadoxNotion.EventData<UnityEngine.Collision2D> data)
  RVA=0x0A9425FC  token=0x6000AE1  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckCollision2D
TYPE:  class
TOKEN: 0x20002C4
SIZE:  0x98
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            ParadoxNotion.CollisionTypes    checkType  // 0x68
  public            System.Boolean                  specifiedTagOnly  // 0x6c
  public            System.String                   objectTag  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveGameObjectAs  // 0x78
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>saveContactPoint  // 0x80
  public            NodeCanvas.Framework.BBParameter<UnityEngine.Vector3>saveContactNormal  // 0x88
  private           System.Boolean                  stay  // 0x90
PROPERTIES:
  info  get=0x0A942754
METHODS:
  RVA=0x0A941CD0  token=0x6000AE3  System.Boolean OnCheck()
  RVA=0x0A94207C  token=0x6000AE4  System.Void OnEnable()
  RVA=0x0A941F88  token=0x6000AE5  System.Void OnDisable()
  RVA=0x0A941CE4  token=0x6000AE6  System.Void OnCollisionEnter2D(ParadoxNotion.EventData<UnityEngine.Collision2D> data)
  RVA=0x0A941EBC  token=0x6000AE7  System.Void OnCollisionExit2D(ParadoxNotion.EventData<UnityEngine.Collision2D> data)
  RVA=0x0A942704  token=0x6000AE8  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckMouse
TYPE:  class
TOKEN: 0x20002C5
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            ParadoxNotion.MouseInteractionTypescheckType  // 0x68
PROPERTIES:
  info  get=0x0A948794
METHODS:
  RVA=0x012081B0  token=0x6000AEA  System.Boolean OnCheck()
  RVA=0x0A948608  token=0x6000AEB  System.Void OnEnable()
  RVA=0x0A9484B8  token=0x6000AEC  System.Void OnDisable()
  RVA=0x0A947888  token=0x6000AED  System.Void OnMouseEnter(ParadoxNotion.EventData msg)
  RVA=0x0A94789C  token=0x6000AEE  System.Void OnMouseExit(ParadoxNotion.EventData msg)
  RVA=0x0A9478B0  token=0x6000AEF  System.Void OnMouseOver(ParadoxNotion.EventData msg)
  RVA=0x0A948758  token=0x6000AF0  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckMouse2D
TYPE:  class
TOKEN: 0x20002C6
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            ParadoxNotion.MouseInteractionTypescheckType  // 0x68
PROPERTIES:
  info  get=0x0A947900
METHODS:
  RVA=0x0A947738  token=0x6000AF2  System.Void OnEnable()
  RVA=0x0A9475E8  token=0x6000AF3  System.Void OnDisable()
  RVA=0x012081B0  token=0x6000AF4  System.Boolean OnCheck()
  RVA=0x0A947888  token=0x6000AF5  System.Void OnMouseEnter(ParadoxNotion.EventData msg)
  RVA=0x0A94789C  token=0x6000AF6  System.Void OnMouseExit(ParadoxNotion.EventData msg)
  RVA=0x0A9478B0  token=0x6000AF7  System.Void OnMouseOver(ParadoxNotion.EventData msg)
  RVA=0x0A9478C4  token=0x6000AF8  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckMouseClick
TYPE:  class
TOKEN: 0x20002C7
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            ParadoxNotion.MouseClickEvent   checkType  // 0x68
PROPERTIES:
  info  get=0x0A947DE8
METHODS:
  RVA=0x012081B0  token=0x6000AFA  System.Boolean OnCheck()
  RVA=0x0A947CB8  token=0x6000AFB  System.Void OnEnable()
  RVA=0x0A947BC4  token=0x6000AFC  System.Void OnDisable()
  RVA=0x0A947888  token=0x6000AFD  System.Void OnMouseDown(ParadoxNotion.EventData msg)
  RVA=0x0A94789C  token=0x6000AFE  System.Void OnMouseUp(ParadoxNotion.EventData msg)
  RVA=0x0A947DAC  token=0x6000AFF  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckMouseClick2D
TYPE:  class
TOKEN: 0x20002C8
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            ParadoxNotion.MouseClickEvent   checkType  // 0x68
PROPERTIES:
  info  get=0x0A947B74
METHODS:
  RVA=0x012081B0  token=0x6000B01  System.Boolean OnCheck()
  RVA=0x0A947A44  token=0x6000B02  System.Void OnEnable()
  RVA=0x0A947950  token=0x6000B03  System.Void OnDisable()
  RVA=0x0A947888  token=0x6000B04  System.Void OnMouseDown(ParadoxNotion.EventData msg)
  RVA=0x0A94789C  token=0x6000B05  System.Void OnMouseUp(ParadoxNotion.EventData msg)
  RVA=0x0A947B38  token=0x6000B06  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckTrigger_Transform
TYPE:  class
TOKEN: 0x20002C9
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            ParadoxNotion.TriggerTypes      checkType  // 0x68
  public            System.Boolean                  specifiedTagOnly  // 0x6c
  public            System.String                   objectTag  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveGameObjectAs  // 0x78
  private           System.Boolean                  stay  // 0x80
PROPERTIES:
  info  get=0x0A94A124
METHODS:
  RVA=0x0A9499B8  token=0x6000B08  System.Boolean OnCheck()
  RVA=0x0A949E44  token=0x6000B09  System.Void OnEnable()
  RVA=0x0A949D50  token=0x6000B0A  System.Void OnDisable()
  RVA=0x0A949F38  token=0x6000B0B  System.Void OnTriggerEnter(ParadoxNotion.EventData<UnityEngine.Collider> data)
  RVA=0x0A94A008  token=0x6000B0C  System.Void OnTriggerExit(ParadoxNotion.EventData<UnityEngine.Collider> data)
  RVA=0x0A94A0D4  token=0x6000B0D  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckTrigger
TYPE:  class
TOKEN: 0x20002CA
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            ParadoxNotion.TriggerTypes      checkType  // 0x68
  public            System.Boolean                  specifiedTagOnly  // 0x6c
  public            System.String                   objectTag  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveGameObjectAs  // 0x78
  private           System.Boolean                  stay  // 0x80
PROPERTIES:
  info  get=0x0A94A22C
METHODS:
  RVA=0x0A9499B8  token=0x6000B0F  System.Boolean OnCheck()
  RVA=0x0A949AC0  token=0x6000B10  System.Void OnEnable()
  RVA=0x0A9499CC  token=0x6000B11  System.Void OnDisable()
  RVA=0x0A949BB4  token=0x6000B12  System.Void OnTriggerEnter(ParadoxNotion.EventData<UnityEngine.Collider> data)
  RVA=0x0A949C84  token=0x6000B13  System.Void OnTriggerExit(ParadoxNotion.EventData<UnityEngine.Collider> data)
  RVA=0x0A94A1DC  token=0x6000B14  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckTrigger2D_Transform
TYPE:  class
TOKEN: 0x20002CB
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            ParadoxNotion.TriggerTypes      CheckType  // 0x68
  public            System.Boolean                  specifiedTagOnly  // 0x6c
  public            System.String                   objectTag  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveGameObjectAs  // 0x78
  private           System.Boolean                  stay  // 0x80
PROPERTIES:
  info  get=0x0A949900
METHODS:
  RVA=0x0A949518  token=0x6000B16  System.Boolean OnCheck()
  RVA=0x0A949620  token=0x6000B17  System.Void OnEnable()
  RVA=0x0A94952C  token=0x6000B18  System.Void OnDisable()
  RVA=0x0A949714  token=0x6000B19  System.Void OnTriggerEnter2D(ParadoxNotion.EventData<UnityEngine.Collider2D> data)
  RVA=0x0A9497E4  token=0x6000B1A  System.Void OnTriggerExit2D(ParadoxNotion.EventData<UnityEngine.Collider2D> data)
  RVA=0x0A9498B0  token=0x6000B1B  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckTrigger2D
TYPE:  class
TOKEN: 0x20002CC
SIZE:  0x88
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            ParadoxNotion.TriggerTypes      CheckType  // 0x68
  public            System.Boolean                  specifiedTagOnly  // 0x6c
  public            System.String                   objectTag  // 0x70
  public            NodeCanvas.Framework.BBParameter<UnityEngine.GameObject>saveGameObjectAs  // 0x78
  private           System.Boolean                  stay  // 0x80
PROPERTIES:
  info  get=0x0A953C5C
METHODS:
  RVA=0x0A949518  token=0x6000B1D  System.Boolean OnCheck()
  RVA=0x0A95397C  token=0x6000B1E  System.Void OnEnable()
  RVA=0x0A953888  token=0x6000B1F  System.Void OnDisable()
  RVA=0x0A953A70  token=0x6000B20  System.Void OnTriggerEnter2D(ParadoxNotion.EventData<UnityEngine.Collider2D> data)
  RVA=0x0A953B40  token=0x6000B21  System.Void OnTriggerExit2D(ParadoxNotion.EventData<UnityEngine.Collider2D> data)
  RVA=0x0A953C0C  token=0x6000B22  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.ButtonClicked
TYPE:  class
TOKEN: 0x20002CD
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<UnityEngine.UI.Button>button  // 0x68
PROPERTIES:
  info  get=0x0A952F48
METHODS:
  RVA=0x0A952E8C  token=0x6000B24  System.String OnInit()
  RVA=0x012081B0  token=0x6000B25  System.Boolean OnCheck()
  RVA=0x0A93D41C  token=0x6000B26  System.Void OnClick()
  RVA=0x04D8E020  token=0x6000B27  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.InterceptEvent
TYPE:  class
TOKEN: 0x20002CE
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            UnityEngine.EventSystems.EventTriggerTypeeventType  // 0x68
PROPERTIES:
  info  get=0x0A960318
METHODS:
  RVA=0x0A95FD0C  token=0x6000B29  System.Void OnEnable()
  RVA=0x0A95F73C  token=0x6000B2A  System.Void OnDisable()
  RVA=0x012081B0  token=0x6000B2B  System.Boolean OnCheck()
  RVA=0x0A93D41C  token=0x6000B2C  System.Void OnPointerEnter(ParadoxNotion.EventData<UnityEngine.EventSystems.PointerEventData> data)
  RVA=0x0A93D41C  token=0x6000B2D  System.Void OnPointerExit(ParadoxNotion.EventData<UnityEngine.EventSystems.PointerEventData> data)
  RVA=0x0A93D41C  token=0x6000B2E  System.Void OnPointerDown(ParadoxNotion.EventData<UnityEngine.EventSystems.PointerEventData> data)
  RVA=0x0A93D41C  token=0x6000B2F  System.Void OnPointerUp(ParadoxNotion.EventData<UnityEngine.EventSystems.PointerEventData> data)
  RVA=0x0A93D41C  token=0x6000B30  System.Void OnPointerClick(ParadoxNotion.EventData<UnityEngine.EventSystems.PointerEventData> data)
  RVA=0x0A93D41C  token=0x6000B31  System.Void OnDrag(ParadoxNotion.EventData<UnityEngine.EventSystems.PointerEventData> data)
  RVA=0x0A93D41C  token=0x6000B32  System.Void OnDrop(ParadoxNotion.EventData<UnityEngine.EventSystems.PointerEventData> eventData)
  RVA=0x0A93D41C  token=0x6000B33  System.Void OnScroll(ParadoxNotion.EventData<UnityEngine.EventSystems.PointerEventData> data)
  RVA=0x0A93D41C  token=0x6000B34  System.Void OnUpdateSelected(ParadoxNotion.EventData<UnityEngine.EventSystems.BaseEventData> eventData)
  RVA=0x0A93D41C  token=0x6000B35  System.Void OnSelect(ParadoxNotion.EventData<UnityEngine.EventSystems.BaseEventData> eventData)
  RVA=0x0A93D41C  token=0x6000B36  System.Void OnDeselect(ParadoxNotion.EventData<UnityEngine.EventSystems.BaseEventData> eventData)
  RVA=0x0A93D41C  token=0x6000B37  System.Void OnMove(ParadoxNotion.EventData<UnityEngine.EventSystems.AxisEventData> eventData)
  RVA=0x0A93D41C  token=0x6000B38  System.Void OnSubmit(ParadoxNotion.EventData<UnityEngine.EventSystems.BaseEventData> eventData)
  RVA=0x0A9602DC  token=0x6000B39  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckEvent
TYPE:  class
TOKEN: 0x20002CF
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>eventName  // 0x68
PROPERTIES:
  info  get=0x0A95318C
METHODS:
  RVA=0x0A9530B8  token=0x6000B3B  System.Void OnEnable()
  RVA=0x0A953020  token=0x6000B3C  System.Void OnDisable()
  RVA=0x012081B0  token=0x6000B3D  System.Boolean OnCheck()
  RVA=0x0A952FA0  token=0x6000B3E  System.Void OnCustomEvent(System.String eventName, ParadoxNotion.IEventData data)
  RVA=0x0A953150  token=0x6000B3F  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckEvent`1
TYPE:  class
TOKEN: 0x20002D0
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>eventName  // 0x0
  public            NodeCanvas.Framework.BBParameter<T>saveEventValue  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000B41  System.Void OnEnable()
  RVA=-1  // not resolved  token=0x6000B42  System.Void OnDisable()
  RVA=-1  // not resolved  token=0x6000B43  System.Boolean OnCheck()
  RVA=-1  // not resolved  token=0x6000B44  System.Void OnCustomEvent(System.String eventName, ParadoxNotion.IEventData data)
  RVA=-1  // not resolved  token=0x6000B45  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckEventValue`1
TYPE:  class
TOKEN: 0x20002D1
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.String>eventName  // 0x0
  public            NodeCanvas.Framework.BBParameter<T>value  // 0x0
PROPERTIES:
  info  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000B47  System.Void OnEnable()
  RVA=-1  // not resolved  token=0x6000B48  System.Void OnDisable()
  RVA=-1  // not resolved  token=0x6000B49  System.Boolean OnCheck()
  RVA=-1  // not resolved  token=0x6000B4A  System.Void OnCustomEvent(System.String eventName, ParadoxNotion.IEventData msg)
  RVA=-1  // not resolved  token=0x6000B4B  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckSignal
TYPE:  class
TOKEN: 0x20002D2
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ConditionTask`1
FIELDS:
  public            NodeCanvas.Framework.BBParameter<NodeCanvas.Framework.SignalDefinition>signalDefinition  // 0x68
  private           System.Collections.Generic.Dictionary<System.String,NodeCanvas.Framework.Internal.BBObjectParameter>argumentsMap  // 0x70
PROPERTIES:
  info  get=0x0A94BFE8
METHODS:
  RVA=0x0A9533B0  token=0x6000B4D  System.String OnInit()
  RVA=0x0A9532A4  token=0x6000B4E  System.Void OnEnable()
  RVA=0x0A9531F4  token=0x6000B4F  System.Void OnDisable()
  RVA=0x0A953400  token=0x6000B50  System.Void OnSignalInvoke(UnityEngine.Transform sender, UnityEngine.Transform receiver, System.Boolean isGlobal, System.Object[] args)
  RVA=0x012081B0  token=0x6000B51  System.Boolean OnCheck()
  RVA=0x0A95358C  token=0x6000B52  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.CheckStateStatus
TYPE:  class
TOKEN: 0x20002D3
SIZE:  0x70
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            ParadoxNotion.CompactStatus     status  // 0x68
PROPERTIES:
  info  get=0x0A953828
METHODS:
  RVA=0x0A953794  token=0x6000B54  System.Boolean OnCheck()
  RVA=0x04DA66B0  token=0x6000B55  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.DebugCondition
TYPE:  class
TOKEN: 0x20002D4
SIZE:  0x68
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
METHODS:
  RVA=0x012081B0  token=0x6000B56  System.Boolean OnCheck()
  RVA=0x04D8E020  token=0x6000B57  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.Probability
TYPE:  class
TOKEN: 0x20002D5
SIZE:  0x80
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Single>probability  // 0x68
  public            NodeCanvas.Framework.BBParameter<System.Single>maxValue  // 0x70
  private           System.Boolean                  success  // 0x78
PROPERTIES:
  info  get=0x0A9634DC
METHODS:
  RVA=0x0A96345C  token=0x6000B59  System.Void OnEnable()
  RVA=0x04D86F90  token=0x6000B5A  System.Boolean OnCheck()
  RVA=0x0489BDF0  token=0x6000B5B  System.Void .ctor()
END_CLASS

CLASS: NodeCanvas.Tasks.Conditions.Timeout
TYPE:  class
TOKEN: 0x20002D6
SIZE:  0x78
EXTENDS: NodeCanvas.Framework.ConditionTask
FIELDS:
  public            NodeCanvas.Framework.BBParameter<System.Single>timeout  // 0x68
  private           System.Single                   currentTime  // 0x70
PROPERTIES:
  info  get=0x0A967CA8
METHODS:
  RVA=0x0A967BBC  token=0x6000B5D  System.Void OnEnable()
  RVA=0x0A967B24  token=0x6000B5E  System.Void OnDisable()
  RVA=0x0A967A44  token=0x6000B5F  System.Void MoveNext()
  RVA=0x0A967AC0  token=0x6000B60  System.Boolean OnCheck()
  RVA=0x0A967C54  token=0x6000B61  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.ActionCall
TYPE:  sealed class
TOKEN: 0x2000472
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x053DEDC8  token=0x60014EA  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x053DEDB8  token=0x60014EB  System.Void Invoke()
  RVA=0x053DED94  token=0x60014EC  System.IAsyncResult BeginInvoke(System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x60014ED  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ParadoxNotion.ActionCall`1
TYPE:  sealed class
TOKEN: 0x2000473
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60014EE  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x60014EF  System.Void Invoke(T1 a)
  RVA=-1  // runtime  token=0x60014F0  System.IAsyncResult BeginInvoke(T1 a, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x60014F1  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ParadoxNotion.ActionCall`2
TYPE:  sealed class
TOKEN: 0x2000474
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60014F2  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x60014F3  System.Void Invoke(T1 a, T2 b)
  RVA=-1  // runtime  token=0x60014F4  System.IAsyncResult BeginInvoke(T1 a, T2 b, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x60014F5  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ParadoxNotion.ActionCall`3
TYPE:  sealed class
TOKEN: 0x2000475
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60014F6  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x60014F7  System.Void Invoke(T1 a, T2 b, T3 c)
  RVA=-1  // runtime  token=0x60014F8  System.IAsyncResult BeginInvoke(T1 a, T2 b, T3 c, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x60014F9  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ParadoxNotion.ActionCall`4
TYPE:  sealed class
TOKEN: 0x2000476
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60014FA  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x60014FB  System.Void Invoke(T1 a, T2 b, T3 c, T4 d)
  RVA=-1  // runtime  token=0x60014FC  System.IAsyncResult BeginInvoke(T1 a, T2 b, T3 c, T4 d, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x60014FD  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ParadoxNotion.ActionCall`5
TYPE:  sealed class
TOKEN: 0x2000477
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60014FE  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x60014FF  System.Void Invoke(T1 a, T2 b, T3 c, T4 d, T5 e)
  RVA=-1  // runtime  token=0x6001500  System.IAsyncResult BeginInvoke(T1 a, T2 b, T3 c, T4 d, T5 e, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6001501  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ParadoxNotion.ActionCall`6
TYPE:  sealed class
TOKEN: 0x2000478
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6001502  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6001503  System.Void Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f)
  RVA=-1  // runtime  token=0x6001504  System.IAsyncResult BeginInvoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6001505  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ParadoxNotion.ActionCall`7
TYPE:  sealed class
TOKEN: 0x2000479
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6001506  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6001507  System.Void Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g)
  RVA=-1  // runtime  token=0x6001508  System.IAsyncResult BeginInvoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6001509  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ParadoxNotion.ActionCall`8
TYPE:  sealed class
TOKEN: 0x200047A
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x600150A  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600150B  System.Void Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h)
  RVA=-1  // runtime  token=0x600150C  System.IAsyncResult BeginInvoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x600150D  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ParadoxNotion.FunctionCall`1
TYPE:  sealed class
TOKEN: 0x200047B
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x600150E  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600150F  TResult Invoke()
  RVA=-1  // runtime  token=0x6001510  System.IAsyncResult BeginInvoke(System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6001511  TResult EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ParadoxNotion.FunctionCall`2
TYPE:  sealed class
TOKEN: 0x200047C
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6001512  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6001513  TResult Invoke(T1 a)
  RVA=-1  // runtime  token=0x6001514  System.IAsyncResult BeginInvoke(T1 a, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6001515  TResult EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ParadoxNotion.FunctionCall`3
TYPE:  sealed class
TOKEN: 0x200047D
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6001516  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6001517  TResult Invoke(T1 a, T2 b)
  RVA=-1  // runtime  token=0x6001518  System.IAsyncResult BeginInvoke(T1 a, T2 b, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6001519  TResult EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ParadoxNotion.FunctionCall`4
TYPE:  sealed class
TOKEN: 0x200047E
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x600151A  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600151B  TResult Invoke(T1 a, T2 b, T3 c)
  RVA=-1  // runtime  token=0x600151C  System.IAsyncResult BeginInvoke(T1 a, T2 b, T3 c, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x600151D  TResult EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ParadoxNotion.FunctionCall`5
TYPE:  sealed class
TOKEN: 0x200047F
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x600151E  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600151F  TResult Invoke(T1 a, T2 b, T3 c, T4 d)
  RVA=-1  // runtime  token=0x6001520  System.IAsyncResult BeginInvoke(T1 a, T2 b, T3 c, T4 d, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6001521  TResult EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ParadoxNotion.FunctionCall`6
TYPE:  sealed class
TOKEN: 0x2000480
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6001522  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6001523  TResult Invoke(T1 a, T2 b, T3 c, T4 d, T5 e)
  RVA=-1  // runtime  token=0x6001524  System.IAsyncResult BeginInvoke(T1 a, T2 b, T3 c, T4 d, T5 e, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6001525  TResult EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ParadoxNotion.FunctionCall`7
TYPE:  sealed class
TOKEN: 0x2000481
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6001526  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6001527  TResult Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f)
  RVA=-1  // runtime  token=0x6001528  System.IAsyncResult BeginInvoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6001529  TResult EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ParadoxNotion.FunctionCall`8
TYPE:  sealed class
TOKEN: 0x2000482
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x600152A  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600152B  TResult Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g)
  RVA=-1  // runtime  token=0x600152C  System.IAsyncResult BeginInvoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x600152D  TResult EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ParadoxNotion.FunctionCall`9
TYPE:  sealed class
TOKEN: 0x2000483
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x600152E  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600152F  TResult Invoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h)
  RVA=-1  // runtime  token=0x6001530  System.IAsyncResult BeginInvoke(T1 a, T2 b, T3 c, T4 d, T5 e, T6 f, T7 g, T8 h, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6001531  TResult EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: ParadoxNotion.DynamicParameterDefinition
TYPE:  sealed class
TOKEN: 0x2000484
SIZE:  0x30
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           System.String                   _ID  // 0x10
  private           System.String                   _name  // 0x18
  private           System.String                   _type  // 0x20
  private           System.Type                     <type>k__BackingField  // 0x28
PROPERTIES:
  ID  get=0x0A9806A8  set=0x053908C0
  name  get=0x0385B100  set=0x05392C40
  type  get=0x04D86240  set=0x02FDB880
METHODS:
  RVA=0x0A9805C0  token=0x6001532  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x0A980560  token=0x6001533  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x041E1670  token=0x600153A  System.Void .ctor()
  RVA=0x0A980630  token=0x600153B  System.Void .ctor(System.String name, System.Type type)
END_CLASS

CLASS: ParadoxNotion.CompareMethod
TYPE:  sealed struct
TOKEN: 0x2000485
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ParadoxNotion.CompareMethod     EqualTo  // const
  public    static  ParadoxNotion.CompareMethod     GreaterThan  // const
  public    static  ParadoxNotion.CompareMethod     LessThan  // const
  public    static  ParadoxNotion.CompareMethod     GreaterOrEqualTo  // const
  public    static  ParadoxNotion.CompareMethod     LessOrEqualTo  // const
METHODS:
END_CLASS

CLASS: ParadoxNotion.OperationMethod
TYPE:  sealed struct
TOKEN: 0x2000486
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ParadoxNotion.OperationMethod   Set  // const
  public    static  ParadoxNotion.OperationMethod   Add  // const
  public    static  ParadoxNotion.OperationMethod   Subtract  // const
  public    static  ParadoxNotion.OperationMethod   Multiply  // const
  public    static  ParadoxNotion.OperationMethod   Divide  // const
METHODS:
END_CLASS

CLASS: ParadoxNotion.ButtonKeys
TYPE:  sealed struct
TOKEN: 0x2000487
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ParadoxNotion.ButtonKeys        Left  // const
  public    static  ParadoxNotion.ButtonKeys        Right  // const
  public    static  ParadoxNotion.ButtonKeys        Middle  // const
METHODS:
END_CLASS

CLASS: ParadoxNotion.PressTypes
TYPE:  sealed struct
TOKEN: 0x2000488
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ParadoxNotion.PressTypes        Down  // const
  public    static  ParadoxNotion.PressTypes        Up  // const
  public    static  ParadoxNotion.PressTypes        Pressed  // const
METHODS:
END_CLASS

CLASS: ParadoxNotion.MouseClickEvent
TYPE:  sealed struct
TOKEN: 0x2000489
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ParadoxNotion.MouseClickEvent   MouseDown  // const
  public    static  ParadoxNotion.MouseClickEvent   MouseUp  // const
METHODS:
END_CLASS

CLASS: ParadoxNotion.TriggerTypes
TYPE:  sealed struct
TOKEN: 0x200048A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ParadoxNotion.TriggerTypes      TriggerEnter  // const
  public    static  ParadoxNotion.TriggerTypes      TriggerExit  // const
  public    static  ParadoxNotion.TriggerTypes      TriggerStay  // const
METHODS:
END_CLASS

CLASS: ParadoxNotion.CollisionTypes
TYPE:  sealed struct
TOKEN: 0x200048B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ParadoxNotion.CollisionTypes    CollisionEnter  // const
  public    static  ParadoxNotion.CollisionTypes    CollisionExit  // const
  public    static  ParadoxNotion.CollisionTypes    CollisionStay  // const
METHODS:
END_CLASS

CLASS: ParadoxNotion.MouseInteractionTypes
TYPE:  sealed struct
TOKEN: 0x200048C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ParadoxNotion.MouseInteractionTypesMouseEnter  // const
  public    static  ParadoxNotion.MouseInteractionTypesMouseExit  // const
  public    static  ParadoxNotion.MouseInteractionTypesMouseOver  // const
METHODS:
END_CLASS

CLASS: ParadoxNotion.CompactStatus
TYPE:  sealed struct
TOKEN: 0x200048D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ParadoxNotion.CompactStatus     Failure  // const
  public    static  ParadoxNotion.CompactStatus     Success  // const
METHODS:
END_CLASS

CLASS: ParadoxNotion.PlayDirections
TYPE:  sealed struct
TOKEN: 0x200048E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ParadoxNotion.PlayDirections    Forward  // const
  public    static  ParadoxNotion.PlayDirections    Backward  // const
  public    static  ParadoxNotion.PlayDirections    Toggle  // const
METHODS:
END_CLASS

CLASS: ParadoxNotion.PlanarDirection
TYPE:  sealed struct
TOKEN: 0x200048F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ParadoxNotion.PlanarDirection   Horizontal  // const
  public    static  ParadoxNotion.PlanarDirection   Vertical  // const
  public    static  ParadoxNotion.PlanarDirection   Auto  // const
METHODS:
END_CLASS

CLASS: ParadoxNotion.Alignment2x2
TYPE:  sealed struct
TOKEN: 0x2000490
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ParadoxNotion.Alignment2x2      Default  // const
  public    static  ParadoxNotion.Alignment2x2      Left  // const
  public    static  ParadoxNotion.Alignment2x2      Right  // const
  public    static  ParadoxNotion.Alignment2x2      Top  // const
  public    static  ParadoxNotion.Alignment2x2      Bottom  // const
METHODS:
END_CLASS

CLASS: ParadoxNotion.Alignment3x3
TYPE:  sealed struct
TOKEN: 0x2000491
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ParadoxNotion.Alignment3x3      TopLeft  // const
  public    static  ParadoxNotion.Alignment3x3      TopCenter  // const
  public    static  ParadoxNotion.Alignment3x3      TopRight  // const
  public    static  ParadoxNotion.Alignment3x3      MiddleLeft  // const
  public    static  ParadoxNotion.Alignment3x3      MiddleCenter  // const
  public    static  ParadoxNotion.Alignment3x3      MiddleRight  // const
  public    static  ParadoxNotion.Alignment3x3      BottomLeft  // const
  public    static  ParadoxNotion.Alignment3x3      BottomCenter  // const
  public    static  ParadoxNotion.Alignment3x3      BottomRight  // const
METHODS:
END_CLASS

CLASS: ParadoxNotion.HierarchyTree
TYPE:  class
TOKEN: 0x2000492
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600153C  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.IEventData
TYPE:  interface
TOKEN: 0x2000495
FIELDS:
PROPERTIES:
  receiver  get=-1  // abstract
  sender  get=-1  // abstract
  valueBoxed  get=-1  // abstract
METHODS:
END_CLASS

CLASS: ParadoxNotion.EventData
TYPE:  sealed struct
TOKEN: 0x2000496
SIZE:  0x28
IMPLEMENTS: ParadoxNotion.IEventData
FIELDS:
  private           UnityEngine.GameObject          <receiver>k__BackingField  // 0x10
  private           System.Object                   <sender>k__BackingField  // 0x18
  private           System.Object                   <value>k__BackingField  // 0x20
PROPERTIES:
  receiver  get=0x02B76770  set=0x05392320
  sender  get=0x04D88320  set=0x067D5E9C
  value  get=0x02B2ECC0  set=0x053908C0
  valueBoxed  get=0x02B2ECC0
METHODS:
  RVA=0x0A9808E4  token=0x600155A  System.Void .ctor(System.Object value, UnityEngine.GameObject receiver, System.Object sender)
  RVA=0x0A9808A8  token=0x600155B  System.Void .ctor(UnityEngine.GameObject receiver, System.Object sender)
END_CLASS

CLASS: ParadoxNotion.EventData`1
TYPE:  sealed struct
TOKEN: 0x2000497
IMPLEMENTS: ParadoxNotion.IEventData
FIELDS:
  private           UnityEngine.GameObject          <receiver>k__BackingField  // 0x0
  private           System.Object                   <sender>k__BackingField  // 0x0
  private           T                               <value>k__BackingField  // 0x0
PROPERTIES:
  receiver  get=-1  // not resolved  set=-1  // not resolved
  sender  get=-1  // not resolved  set=-1  // not resolved
  value  get=-1  // not resolved  set=-1  // not resolved
  valueBoxed  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6001563  System.Void .ctor(T value, UnityEngine.GameObject receiver, System.Object sender)
END_CLASS

CLASS: ParadoxNotion.OperationTools
TYPE:  static class
TOKEN: 0x2000498
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0A984D58  token=0x6001564  System.String GetOperationString(ParadoxNotion.OperationMethod om)
  RVA=0x0A985030  token=0x6001565  System.Single Operate(System.Single a, System.Single b, ParadoxNotion.OperationMethod om, System.Single delta)
  RVA=0x0A984FF4  token=0x6001566  System.Int32 Operate(System.Int32 a, System.Int32 b, ParadoxNotion.OperationMethod om)
  RVA=0x0A984E14  token=0x6001567  UnityEngine.Vector3 Operate(UnityEngine.Vector3 a, UnityEngine.Vector3 b, ParadoxNotion.OperationMethod om, System.Single delta)
  RVA=0x0A984C9C  token=0x6001568  System.String GetCompareString(ParadoxNotion.CompareMethod cm)
  RVA=0x0A984C54  token=0x6001569  System.Boolean Compare(System.Single a, System.Single b, ParadoxNotion.CompareMethod cm, System.Single floatingPoint)
  RVA=0x0A984C14  token=0x600156A  System.Boolean Compare(System.Int32 a, System.Int32 b, ParadoxNotion.CompareMethod cm)
END_CLASS

CLASS: ParadoxNotion.ReflectionTools
TYPE:  static class
TOKEN: 0x2000499
SIZE:  0x10
FIELDS:
  public    static  System.Reflection.BindingFlags  FLAGS_ALL  // const
  public    static  System.Reflection.BindingFlags  FLAGS_ALL_DECLARED  // const
  private   static  System.Reflection.Assembly[]    _loadedAssemblies  // static @ 0x0
  private   static  System.Type[]                   _allTypes  // static @ 0x8
  private   static  System.Object[]                 _tempArgs  // static @ 0x10
  private   static  System.Collections.Generic.Dictionary<System.String,System.Type>_typesMap  // static @ 0x18
  private   static  System.Collections.Generic.Dictionary<System.Type,System.Type[]>_subTypesMap  // static @ 0x20
  private   static  System.Collections.Generic.Dictionary<System.Reflection.MethodBase,ParadoxNotion.ReflectionTools.MethodType>_methodSpecialType  // static @ 0x28
  private   static  System.Collections.Generic.Dictionary<System.Type,System.String>_typeFriendlyName  // static @ 0x30
  private   static  System.Collections.Generic.Dictionary<System.Type,System.String>_typeFriendlyNameCompileSafe  // static @ 0x38
  private   static  System.Collections.Generic.Dictionary<System.Reflection.MethodBase,System.String>_methodSignatures  // static @ 0x40
  private   static  System.Collections.Generic.Dictionary<System.Type,System.Reflection.ConstructorInfo[]>_typeConstructors  // static @ 0x48
  private   static  System.Collections.Generic.Dictionary<System.Type,System.Reflection.MethodInfo[]>_typeMethods  // static @ 0x50
  private   static  System.Collections.Generic.Dictionary<System.Type,System.Reflection.FieldInfo[]>_typeFields  // static @ 0x58
  private   static  System.Collections.Generic.Dictionary<System.Type,System.Reflection.PropertyInfo[]>_typeProperties  // static @ 0x60
  private   static  System.Collections.Generic.Dictionary<System.Type,System.Reflection.EventInfo[]>_typeEvents  // static @ 0x68
  private   static  System.Collections.Generic.Dictionary<System.Type,System.Object[]>_typeAttributes  // static @ 0x70
  private   static  System.Collections.Generic.Dictionary<System.Reflection.MemberInfo,System.Object[]>_memberAttributes  // static @ 0x78
  private   static  System.Collections.Generic.Dictionary<System.Reflection.MemberInfo,System.Boolean>_obsoleteCache  // static @ 0x80
  private   static  System.Collections.Generic.Dictionary<System.Type,System.Reflection.MethodInfo[]>_typeExtensions  // static @ 0x88
  private   static  System.Collections.Generic.Dictionary<System.Type,System.Type[]>_genericArgsTypeCache  // static @ 0x90
  private   static  System.Collections.Generic.Dictionary<System.Reflection.MethodInfo,System.Type[]>_genericArgsMathodCache  // static @ 0x98
  public    static readonly System.Collections.Generic.Dictionary<System.String,System.String>op_FriendlyNamesLong  // static @ 0xa0
  public    static readonly System.Collections.Generic.Dictionary<System.String,System.String>op_FriendlyNamesShort  // static @ 0xa8
  public    static readonly System.Collections.Generic.Dictionary<System.String,System.String>op_CSharpAliases  // static @ 0xb0
  public    static  System.String                   METHOD_SPECIAL_NAME_GET  // const
  public    static  System.String                   METHOD_SPECIAL_NAME_SET  // const
  public    static  System.String                   METHOD_SPECIAL_NAME_ADD  // const
  public    static  System.String                   METHOD_SPECIAL_NAME_REMOVE  // const
  public    static  System.String                   METHOD_SPECIAL_NAME_OP  // const
PROPERTIES:
  loadedAssemblies  get=0x0A98ACE8
METHODS:
  RVA=0x03A32DC0  token=0x600156B  System.Void .cctor()
  RVA=0x045B12D0  token=0x600156C  System.Void FlushMem()
  RVA=0x0A988334  token=0x600156E  System.Type GetType(System.String typeFullName)
  RVA=0x038F1470  token=0x600156F  System.Type GetType(System.String typeFullName, System.Type fallbackAssignable)
  RVA=0x038F14C0  token=0x6001570  System.Type GetType(System.String typeFullName, System.Boolean fallbackNoNamespace, System.Type fallbackAssignable)
  RVA=0x038F1620  token=0x6001571  System.Type GetTypeDirect(System.String typeFullName)
  RVA=0x0A98A628  token=0x6001572  System.Type TryResolveGenericType(System.String typeFullName, System.Boolean fallbackNoNamespace, System.Type fallbackAssignable)
  RVA=0x0A98A50C  token=0x6001573  System.Type TryResolveDeserializeFromAttribute(System.String typeName)
  RVA=0x0A98AB34  token=0x6001574  System.Type TryResolveWithoutNamespace(System.String typeName, System.Type fallbackAssignable)
  RVA=0x0A986DE4  token=0x6001575  System.Type[] GetAllTypes(System.Boolean includeObsolete)
  RVA=0x0A987C80  token=0x6001576  System.Type[] GetImplementationsOf(System.Type baseType)
  RVA=0x0A98A084  token=0x6001577  System.Object[] SingleTempArgsArray(System.Object arg)
  RVA=0x0A987E5C  token=0x6001578  ParadoxNotion.ReflectionTools.MethodType GetMethodSpecialType(System.Reflection.MethodBase method)
  RVA=0x0A986730  token=0x6001579  System.String FriendlyName(System.Type t, System.Boolean compileSafe)
  RVA=0x0A986634  token=0x600157A  System.String FriendlyName(System.Reflection.MemberInfo info)
  RVA=0x0A986108  token=0x600157B  System.String FriendlyName(System.Reflection.MethodBase method)
  RVA=0x0A986160  token=0x600157C  System.String FriendlyName(System.Reflection.MethodBase method, ParadoxNotion.ReflectionTools.MethodType& specialNameType)
  RVA=0x0A989CA4  token=0x600157D  System.String SignatureName(System.Reflection.MethodBase method)
  RVA=0x0A986C40  token=0x600157E  System.String FriendlyTypeName(System.String fullName)
  RVA=0x0A989A2C  token=0x600157F  System.Type RTReflectedOrDeclaredType(System.Reflection.MemberInfo member)
  RVA=0x0321E250  token=0x6001580  System.Boolean RTIsAssignableFrom(System.Type type, System.Type other)
  RVA=0x0321E200  token=0x6001581  System.Boolean RTIsAssignableTo(System.Type type, System.Type other)
  RVA=0x0A989920  token=0x6001582  System.Boolean RTIsAbstract(System.Type type)
  RVA=0x0A989A0C  token=0x6001583  System.Boolean RTIsValueType(System.Type type)
  RVA=0x0A98993C  token=0x6001584  System.Boolean RTIsArray(System.Type type)
  RVA=0x0A9899CC  token=0x6001585  System.Boolean RTIsInterface(System.Type type)
  RVA=0x0A9899E8  token=0x6001586  System.Boolean RTIsSubclassOf(System.Type type, System.Type other)
  RVA=0x0A9899AC  token=0x6001587  System.Boolean RTIsGenericParameter(System.Type type)
  RVA=0x04D83A60  token=0x6001588  System.Boolean RTIsGenericType(System.Type type)
  RVA=0x0A989524  token=0x6001589  System.Reflection.MethodInfo RTGetGetMethod(System.Reflection.PropertyInfo prop)
  RVA=0x0A989904  token=0x600158A  System.Reflection.MethodInfo RTGetSetMethod(System.Reflection.PropertyInfo prop)
  RVA=0x0A988CE8  token=0x600158B  System.Reflection.MethodInfo RTGetDelegateMethodInfo(System.Delegate del)
  RVA=0x049AF470  token=0x600158C  System.Type RTMakeGenericType(System.Type type, System.Type[] typeArgs)
  RVA=0x03F85540  token=0x600158D  System.Type[] RTGetGenericArguments(System.Type type)
  RVA=0x0A989420  token=0x600158E  System.Type[] RTGetGenericArguments(System.Reflection.MethodInfo method)
  RVA=0x0A988E54  token=0x600158F  System.Type[] RTGetEmptyTypes()
  RVA=0x0A988E04  token=0x6001590  System.Type RTGetElementType(System.Type type)
  RVA=0x0A98995C  token=0x6001591  System.Boolean RTIsByRef(System.Type type)
  RVA=0x0A985ACC  token=0x6001592  System.Object CreateObject(System.Type type)
  RVA=0x0A985A64  token=0x6001593  System.Object CreateObjectUninitialized(System.Type type)
  RVA=0x036042F0  token=0x6001594  System.Reflection.ConstructorInfo RTGetDefaultConstructor(System.Type type)
  RVA=0x0A988BC0  token=0x6001595  System.Reflection.ConstructorInfo RTGetConstructor(System.Type type, System.Type[] paramTypes)
  RVA=0x0A988940  token=0x6001596  System.Boolean MemberResolvedFromDeserializeAttribute(System.Reflection.MemberInfo member, System.String targetName)
  RVA=0x0A989540  token=0x6001597  System.Reflection.MethodInfo RTGetMethod(System.Type type, System.String name)
  RVA=0x0A989614  token=0x6001598  System.Reflection.MethodInfo RTGetMethod(System.Type type, System.String name, System.Type[] paramTypes, System.Type returnType, System.Type[] genericArgumentTypes)
  RVA=0x0A989304  token=0x6001599  System.Reflection.FieldInfo RTGetField(System.Type type, System.String name, System.Boolean includePrivateBase)
  RVA=0x03F85730  token=0x600159A  System.Reflection.PropertyInfo RTGetProperty(System.Type type, System.String name)
  RVA=0x0A989198  token=0x600159B  System.Reflection.MemberInfo RTGetFieldOrProp(System.Type type, System.String name)
  RVA=0x0A988EA4  token=0x600159C  System.Reflection.EventInfo RTGetEvent(System.Type type, System.String name)
  RVA=0x0A989058  token=0x600159D  System.Object RTGetFieldOrPropValue(System.Reflection.MemberInfo member, System.Object instance, System.Int32 index)
  RVA=0x0A989AA0  token=0x600159E  System.Void RTSetFieldOrPropValue(System.Reflection.MemberInfo member, System.Object instance, System.Object value, System.Int32 index)
  RVA=0x036043A0  token=0x600159F  System.Reflection.ConstructorInfo[] RTGetConstructors(System.Type type)
  RVA=0x0A989824  token=0x60015A0  System.Reflection.MethodInfo[] RTGetMethods(System.Type type)
  RVA=0x0321D560  token=0x60015A1  System.Reflection.FieldInfo[] RTGetFields(System.Type type)
  RVA=0x03F85650  token=0x60015A2  System.Reflection.PropertyInfo[] RTGetProperties(System.Type type)
  RVA=0x0A988F78  token=0x60015A3  System.Reflection.EventInfo[] RTGetEvents(System.Type type)
  RVA=0x0321E280  token=0x60015A4  System.Object[] RTGetAllAttributes(System.Type type)
  RVA=-1  // generic def  token=0x60015A5  System.Boolean RTIsDefined(System.Type type, System.Boolean inherited)
  RVA=0x0321CA70  token=0x60015A6  System.Boolean RTIsDefined(System.Type type, System.Type attributeType, System.Boolean inherited)
  RVA=-1  // generic def  token=0x60015A7  T RTGetAttribute(System.Type type, System.Boolean inherited)
  RVA=0x0321E0D0  token=0x60015A8  System.Attribute RTGetAttribute(System.Type type, System.Type attributeType, System.Boolean inherited)
  RVA=0x0321DF80  token=0x60015A9  System.Object[] RTGetAllAttributes(System.Reflection.MemberInfo member)
  RVA=-1  // generic def  token=0x60015AA  System.Boolean RTIsDefined(System.Reflection.MemberInfo member, System.Boolean inherited)
  RVA=0x0321DDD0  token=0x60015AB  System.Boolean RTIsDefined(System.Reflection.MemberInfo member, System.Type attributeType, System.Boolean inherited)
  RVA=-1  // generic def  token=0x60015AC  T RTGetAttribute(System.Reflection.MemberInfo member, System.Boolean inherited)
  RVA=0x0321DE40  token=0x60015AD  System.Attribute RTGetAttribute(System.Reflection.MemberInfo member, System.Type attributeType, System.Boolean inherited)
  RVA=-1  // generic def  token=0x60015AE  System.Collections.Generic.IEnumerable<T> RTGetAttributesRecursive(System.Type type)
  RVA=0x0A988D08  token=0x60015AF  System.Reflection.ParameterInfo[] RTGetDelegateTypeParameters(System.Type delegateType)
  RVA=-1  // generic def  token=0x60015B0  T RTCreateDelegate(System.Reflection.MethodInfo method, System.Object instance)
  RVA=0x0A9889B8  token=0x60015B1  System.Delegate RTCreateDelegate(System.Reflection.MethodInfo method, System.Type type, System.Object instance)
  RVA=0x0A985A24  token=0x60015B2  System.Delegate ConvertDelegate(System.Delegate originalDelegate, System.Type targetDelegateType)
  RVA=0x0A988878  token=0x60015B3  System.Boolean IsReadOnly(System.Reflection.FieldInfo field)
  RVA=0x0A988464  token=0x60015B4  System.Boolean IsConstant(System.Reflection.FieldInfo field)
  RVA=0x0A9888F8  token=0x60015B5  System.Boolean IsStatic(System.Reflection.EventInfo info)
  RVA=0x0A9888B0  token=0x60015B6  System.Boolean IsStatic(System.Reflection.PropertyInfo info)
  RVA=0x0A98879C  token=0x60015B7  System.Boolean IsParams(System.Reflection.ParameterInfo parameter, System.Reflection.ParameterInfo[] parameters)
  RVA=0x0A98861C  token=0x60015B8  System.Boolean IsObsolete(System.Reflection.MemberInfo member, System.Boolean inherited)
  RVA=0x0A9872C8  token=0x60015B9  System.Reflection.PropertyInfo GetBaseDefinition(System.Reflection.PropertyInfo propertyInfo)
  RVA=0x0A98724C  token=0x60015BA  System.Reflection.FieldInfo GetBaseDefinition(System.Reflection.FieldInfo fieldInfo)
  RVA=0x0A987768  token=0x60015BB  System.Reflection.MethodInfo[] GetExtensionMethods(System.Type targetType)
  RVA=0x0A988598  token=0x60015BC  System.Boolean IsExtensionMethod(System.Reflection.MethodInfo method)
  RVA=0x0A988830  token=0x60015BD  System.Boolean IsPropertyAccessor(System.Reflection.MethodInfo method)
  RVA=0x0A9885EC  token=0x60015BE  System.Boolean IsIndexerProperty(System.Reflection.PropertyInfo property)
  RVA=0x0A988384  token=0x60015BF  System.Boolean IsAutoProperty(System.Reflection.PropertyInfo property)
  RVA=0x0A986D3C  token=0x60015C0  System.Reflection.PropertyInfo GetAccessorProperty(System.Reflection.MethodInfo method)
  RVA=0x0A9884C4  token=0x60015C1  System.Boolean IsEnumerableCollection(System.Type type)
  RVA=0x0A9875C8  token=0x60015C2  System.Type GetEnumerableElementType(System.Type type)
  RVA=0x0A9882B4  token=0x60015C3  System.Type GetSingleGenericArgument(System.Type type)
  RVA=0x0A987B58  token=0x60015C4  System.Type GetFirstGenericParameterConstraintType(System.Type type)
  RVA=0x0A987A44  token=0x60015C5  System.Type GetFirstGenericParameterConstraintType(System.Reflection.MethodInfo method)
  RVA=0x0A98A210  token=0x60015C6  System.Boolean TryMakeGeneric(System.Type def, System.Type argType, System.Type& result)
  RVA=0x0A98A394  token=0x60015C7  System.Boolean TryMakeGeneric(System.Reflection.MethodInfo def, System.Type argType, System.Reflection.MethodInfo& result)
  RVA=0x0A989BE8  token=0x60015C8  System.Array Resize(System.Array array, System.Int32 newSize)
  RVA=0x0A98A10C  token=0x60015C9  System.Boolean TryConvert(System.Type fromType, System.Type toType, System.Linq.Expressions.UnaryExpression& exp)
  RVA=0x0A985B1C  token=0x60015CA  System.Void DigFields(System.Object root, System.Predicate<System.Reflection.FieldInfo> move, System.Action<System.Object> push, System.Action<System.Object> pop)
  RVA=-1  // generic def  token=0x60015CB  System.Func<T,TResult> GetFieldGetter(System.Reflection.FieldInfo info)
  RVA=-1  // generic def  token=0x60015CC  System.Action<T,TValue> GetFieldSetter(System.Reflection.FieldInfo info)
END_CLASS

CLASS: ParadoxNotion.TypeConverter
TYPE:  static class
TOKEN: 0x20004A0
SIZE:  0x10
FIELDS:
  private   static  ParadoxNotion.TypeConverter.CustomConvertercustomConverter  // static @ 0x0
EVENTS:
  customConverter  add=add_customConverter  remove=remove_customConverter
METHODS:
  RVA=0x04115330  token=0x60015E3  System.Func<System.Object,System.Object> Get(System.Type fromType, System.Type toType)
  RVA=0x0A999CF8  token=0x60015E4  System.Boolean CanConvert(System.Type fromType, System.Type toType)
END_CLASS

CLASS: ParadoxNotion.ColorUtils
TYPE:  static class
TOKEN: 0x20004A4
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.Dictionary<UnityEngine.Color32,System.String>colorHexCache  // static @ 0x0
  private   static  System.Collections.Generic.Dictionary<System.String,UnityEngine.Color>hexColorCache  // static @ 0x8
  private   static  System.Collections.Generic.Dictionary<System.ValueTuple<System.Int32,System.Int32,UnityEngine.Gradient,System.Single>,UnityEngine.Texture2D>gradientTextureCache  // static @ 0x10
  private   static  System.Collections.Generic.Dictionary<System.ValueTuple<UnityEngine.Color,System.Single,UnityEngine.Color,System.Single>,UnityEngine.Gradient>gradientCache  // static @ 0x18
  private   static  System.Collections.Generic.Dictionary<System.String,UnityEngine.Gradient>menuItemRibbonGradientCache  // static @ 0x20
METHODS:
  RVA=0x04DA6890  token=0x60015FD  UnityEngine.Color WithAlpha(UnityEngine.Color color, System.Single alpha)
  RVA=0x04DA6870  token=0x60015FE  UnityEngine.Color Grey(System.Single value)
  RVA=0x0A98DAB8  token=0x60015FF  System.String ColorToHex(UnityEngine.Color32 color)
  RVA=0x0331D300  token=0x6001600  UnityEngine.Color HexToColor(System.String hex)
  RVA=0x0A98DC2C  token=0x6001601  UnityEngine.Texture2D CreateGradientTexture(System.Int32 width, System.Int32 height, UnityEngine.Gradient gradient, System.Single rotateDegree)
  RVA=0x0A98DED4  token=0x6001602  UnityEngine.Gradient CreateGradient(UnityEngine.Color colorLeft, System.Single colorLeftRatio, UnityEngine.Color colorRight, System.Single colorRightRatio)
  RVA=0x0A98E160  token=0x6001603  UnityEngine.Gradient GetMenuItemRibbonGradientOfNodeFullPath(System.String fullPath)
  RVA=0x0A98E1E4  token=0x6001604  System.Void RegisterMenuItemRibbonGradientOfNodeFullPath(System.String fullPath, UnityEngine.Color colorLeft, System.Single colorLeftRatio, UnityEngine.Color colorRight, System.Single colorRightRatio)
  RVA=0x03A32B50  token=0x6001605  System.Void .cctor()
END_CLASS

CLASS: ParadoxNotion.CurveUtils
TYPE:  static class
TOKEN: 0x20004A5
SIZE:  0x10
FIELDS:
  private   static  System.Single                   POS_CHECK_RES  // const
  private   static  System.Single                   POS_CHECK_DISTANCE  // const
METHODS:
  RVA=0x0A98E2BC  token=0x6001606  UnityEngine.Vector2 GetPosAlongCurve(UnityEngine.Vector2 from, UnityEngine.Vector2 to, UnityEngine.Vector2 fromTangent, UnityEngine.Vector2 toTangent, System.Single t)
  RVA=0x0A98E3D4  token=0x6001607  System.Boolean IsPosAlongCurve(UnityEngine.Vector2 from, UnityEngine.Vector2 to, UnityEngine.Vector2 fromTangent, UnityEngine.Vector2 toTangent, UnityEngine.Vector2 targetPosition)
  RVA=0x0A98E40C  token=0x6001608  System.Boolean IsPosAlongCurve(UnityEngine.Vector2 from, UnityEngine.Vector2 to, UnityEngine.Vector2 fromTangent, UnityEngine.Vector2 toTangent, UnityEngine.Vector2 targetPosition, System.Single& norm)
  RVA=0x0A98E7E0  token=0x6001609  System.Void ResolveTangents(UnityEngine.Vector2 from, UnityEngine.Vector2 to, System.Single rigidMlt, ParadoxNotion.PlanarDirection direction, UnityEngine.Vector2& fromTangent, UnityEngine.Vector2& toTangent)
  RVA=0x0A98E5EC  token=0x600160A  System.Void ResolveTangents(UnityEngine.Vector2 from, UnityEngine.Vector2 to, UnityEngine.Rect fromRect, UnityEngine.Rect toRect, System.Single rigidMlt, ParadoxNotion.PlanarDirection direction, UnityEngine.Vector2& fromTangent, UnityEngine.Vector2& toTangent)
END_CLASS

CLASS: ParadoxNotion.GUIStyleUtils
TYPE:  static class
TOKEN: 0x20004A6
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0A993AE0  token=0x600160B  UnityEngine.GUIStyle Margin(UnityEngine.GUIStyle style, System.Int32 left, System.Int32 right, System.Int32 top, System.Int32 bottom)
  RVA=0x0A993C38  token=0x600160C  UnityEngine.GUIStyle Padding(UnityEngine.GUIStyle style, System.Int32 left, System.Int32 right, System.Int32 top, System.Int32 bottom)
  RVA=0x0A993A34  token=0x600160D  UnityEngine.GUIStyle Border(UnityEngine.GUIStyle style, System.Int32 left, System.Int32 right, System.Int32 top, System.Int32 bottom)
  RVA=0x0A993B8C  token=0x600160E  UnityEngine.GUIStyle Overflow(UnityEngine.GUIStyle style, System.Int32 left, System.Int32 right, System.Int32 top, System.Int32 bottom)
  RVA=0x0A993D0C  token=0x600160F  UnityEngine.GUIStyle TextAlignment(UnityEngine.GUIStyle style, UnityEngine.TextAnchor anchor)
  RVA=0x0A993CE4  token=0x6001610  UnityEngine.GUIStyle RichText(UnityEngine.GUIStyle style, System.Boolean rich)
END_CLASS

CLASS: ParadoxNotion.LayerUtils
TYPE:  static class
TOKEN: 0x20004A7
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0A994A28  token=0x6001611  UnityEngine.LayerMask CreateFromNames(System.String[] layerNames)
  RVA=0x0A994A30  token=0x6001612  UnityEngine.LayerMask CreateFromNumbers(System.Int32[] layerNumbers)
  RVA=0x0A994A38  token=0x6001613  UnityEngine.LayerMask LayerNamesToMask(System.String[] layerNames)
  RVA=0x0A994A94  token=0x6001614  UnityEngine.LayerMask LayerNumbersToMask(System.Int32[] layerNumbers)
  RVA=0x04DA68B0  token=0x6001615  UnityEngine.LayerMask Inverse(UnityEngine.LayerMask mask)
  RVA=0x0A994948  token=0x6001616  UnityEngine.LayerMask AddToMask(UnityEngine.LayerMask mask, System.String[] layerNames)
  RVA=0x0A994C78  token=0x6001617  UnityEngine.LayerMask RemoveFromMask(UnityEngine.LayerMask mask, System.String[] layerNames)
  RVA=0x0A9949C8  token=0x6001618  System.Boolean ContainsAnyLayer(UnityEngine.LayerMask mask, System.String[] layerNames)
  RVA=0x0A994968  token=0x6001619  System.Boolean ContainsAllLayers(UnityEngine.LayerMask mask, System.String[] layerNames)
  RVA=0x0A994AD4  token=0x600161A  System.String[] MaskToNames(UnityEngine.LayerMask mask)
  RVA=0x0A994C3C  token=0x600161B  System.String MaskToString(UnityEngine.LayerMask mask)
  RVA=0x0A994BB0  token=0x600161C  System.String MaskToString(UnityEngine.LayerMask mask, System.String delimiter)
END_CLASS

CLASS: ParadoxNotion.ObjectUtils
TYPE:  static class
TOKEN: 0x20004A8
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0A996344  token=0x600161D  System.Boolean AnyEquals(System.Object a, System.Object b)
  RVA=-1  // generic def  token=0x600161E  System.Collections.Generic.List<T> Shuffle(System.Collections.Generic.List<T> list)
  RVA=-1  // generic def  token=0x600161F  System.Boolean Is(System.Object o, T& result)
  RVA=-1  // generic def  token=0x6001620  T GetAddComponent(UnityEngine.GameObject gameObject)
  RVA=0x0A996534  token=0x6001621  UnityEngine.Component TransformToType(UnityEngine.Component current, System.Type type)
  RVA=0x0A996408  token=0x6001622  System.Collections.Generic.IEnumerable<UnityEngine.GameObject> FindGameObjectsWithinLayerMask(UnityEngine.LayerMask mask, UnityEngine.GameObject exclude)
  RVA=0x0A996504  token=0x6001623  System.Boolean IsInLayerMask(UnityEngine.GameObject gameObject, UnityEngine.LayerMask mask)
END_CLASS

CLASS: ParadoxNotion.RectUtils
TYPE:  static class
TOKEN: 0x20004AA
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0A996768  token=0x6001626  UnityEngine.Rect GetBoundRect(UnityEngine.Rect[] rects)
  RVA=0x0A996888  token=0x6001627  UnityEngine.Rect GetBoundRect(UnityEngine.Vector2[] positions)
  RVA=0x0A996634  token=0x6001628  System.Boolean Encapsulates(UnityEngine.Rect a, UnityEngine.Rect b)
  RVA=0x0A9966E8  token=0x6001629  UnityEngine.Rect ExpandBy(UnityEngine.Rect rect, System.Single margin)
  RVA=0x0A996724  token=0x600162A  UnityEngine.Rect ExpandBy(UnityEngine.Rect rect, System.Single xMargin, System.Single yMargin)
  RVA=0x0A996680  token=0x600162B  UnityEngine.Rect ExpandBy(UnityEngine.Rect rect, System.Single left, System.Single top, System.Single right, System.Single bottom)
  RVA=0x0A996A18  token=0x600162C  UnityEngine.Rect TransformSpace(UnityEngine.Rect rect, UnityEngine.Rect oldContainer, UnityEngine.Rect newContainer)
  RVA=0x0A996998  token=0x600162D  UnityEngine.Vector2 TransformSpace(UnityEngine.Vector2 vector, UnityEngine.Rect oldContainer, UnityEngine.Rect newContainer)
END_CLASS

CLASS: ParadoxNotion.StringUtils
TYPE:  static class
TOKEN: 0x20004AB
SIZE:  0x10
FIELDS:
  public    static  System.String                   SPACE  // const
  public    static  System.String                   ALPHABET  // const
  public    static readonly System.Char[]                   CHAR_SPACE_ARRAY  // static @ 0x0
  private   static  System.Collections.Generic.Dictionary<System.String,System.String>splitCaseCache  // static @ 0x8
METHODS:
  RVA=0x0A999480  token=0x600162E  System.String SplitCamelCase(System.String s)
  RVA=0x0A99892C  token=0x600162F  System.String CapitalizeFirst(System.String s)
  RVA=0x0A9988B8  token=0x6001630  System.String CapLength(System.String s, System.Int32 max)
  RVA=0x0A998AAC  token=0x6001631  System.String GetCapitals(System.String s)
  RVA=0x0A9989E0  token=0x6001632  System.String FormatError(System.String input)
  RVA=0x0A998A1C  token=0x6001633  System.String GetAlphabetLetter(System.Int32 index)
  RVA=0x0A998C58  token=0x6001634  System.String GetStringWithinOuter(System.String input, System.Char from, System.Char to)
  RVA=0x0A998BB4  token=0x6001635  System.String GetStringWithinInner(System.String input, System.Char from, System.Char to)
  RVA=0x0A998CC0  token=0x6001636  System.String ReplaceWithin(System.String text, System.Char startChar, System.Char endChar, System.Func<System.String,System.String> Process)
  RVA=0x0A998DBC  token=0x6001637  System.Single ScoreSearchMatch(System.String input, System.String leafName, System.String categoryName)
  RVA=0x0A999154  token=0x6001638  System.Boolean SearchMatch(System.String input, System.String leafName, System.String categoryName)
  RVA=0x0A999624  token=0x6001639  System.String ToStringAdvanced(System.Object o)
  RVA=0x0A999888  token=0x600163A  System.Void .cctor()
END_CLASS

CLASS: ParadoxNotion.WeakReferenceList`1
TYPE:  class
TOKEN: 0x20004AC
FIELDS:
  private           System.Collections.Generic.List<System.WeakReference<T>>list  // 0x0
PROPERTIES:
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600163C  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600163D  System.Void .ctor(System.Int32 capacity)
  RVA=-1  // not resolved  token=0x6001640  System.Void Add(T item)
  RVA=-1  // not resolved  token=0x6001641  System.Void Remove(T item)
  RVA=-1  // not resolved  token=0x6001642  System.Boolean Contains(T item, System.Int32& index)
  RVA=-1  // not resolved  token=0x6001643  System.Void Clear()
  RVA=-1  // not resolved  token=0x6001644  System.Collections.Generic.List<T> ToReferenceList()
  RVA=-1  // not resolved  token=0x6001645  ParadoxNotion.WeakReferenceList<T> op_Implicit(System.Collections.Generic.List<T> value)
END_CLASS

CLASS: ParadoxNotion.WeakReferenceTable`2
TYPE:  class
TOKEN: 0x20004AD
FIELDS:
  private           System.Collections.Generic.List<System.WeakReference<TKey>>keys  // 0x0
  private           System.Collections.Generic.List<TValue>values  // 0x0
PROPERTIES:
  Count  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6001647  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6001648  System.Void Clear()
  RVA=-1  // not resolved  token=0x6001649  System.Void Add(TKey key, TValue value)
  RVA=-1  // not resolved  token=0x600164A  System.Void Remove(TKey key)
  RVA=-1  // not resolved  token=0x600164B  System.Boolean TryGetValueWithRefCheck(TKey key, TValue& value)
  RVA=-1  // not resolved  token=0x600164C  System.Void RemoveMissingReferences()
  RVA=-1  // not resolved  token=0x600164D  System.Void CheckCount()
END_CLASS

CLASS: ParadoxNotion.Animation.EaseType
TYPE:  sealed struct
TOKEN: 0x2000511
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ParadoxNotion.Animation.EaseTypeLinear  // const
  public    static  ParadoxNotion.Animation.EaseTypeQuadraticIn  // const
  public    static  ParadoxNotion.Animation.EaseTypeQuadraticOut  // const
  public    static  ParadoxNotion.Animation.EaseTypeQuadraticInOut  // const
  public    static  ParadoxNotion.Animation.EaseTypeQuarticIn  // const
  public    static  ParadoxNotion.Animation.EaseTypeQuarticOut  // const
  public    static  ParadoxNotion.Animation.EaseTypeQuarticInOut  // const
  public    static  ParadoxNotion.Animation.EaseTypeQuinticIn  // const
  public    static  ParadoxNotion.Animation.EaseTypeQuinticOut  // const
  public    static  ParadoxNotion.Animation.EaseTypeQuinticInOut  // const
  public    static  ParadoxNotion.Animation.EaseTypeCubicIn  // const
  public    static  ParadoxNotion.Animation.EaseTypeCubicOut  // const
  public    static  ParadoxNotion.Animation.EaseTypeCubicInOut  // const
  public    static  ParadoxNotion.Animation.EaseTypeExponentialIn  // const
  public    static  ParadoxNotion.Animation.EaseTypeExponentialOut  // const
  public    static  ParadoxNotion.Animation.EaseTypeExponentialInOut  // const
  public    static  ParadoxNotion.Animation.EaseTypeCircularIn  // const
  public    static  ParadoxNotion.Animation.EaseTypeCircularOut  // const
  public    static  ParadoxNotion.Animation.EaseTypeCircularInOut  // const
  public    static  ParadoxNotion.Animation.EaseTypeSinusoidalIn  // const
  public    static  ParadoxNotion.Animation.EaseTypeSinusoidalOut  // const
  public    static  ParadoxNotion.Animation.EaseTypeSinusoidalInOut  // const
  public    static  ParadoxNotion.Animation.EaseTypeElasticIn  // const
  public    static  ParadoxNotion.Animation.EaseTypeElasticOut  // const
  public    static  ParadoxNotion.Animation.EaseTypeElasticInOut  // const
  public    static  ParadoxNotion.Animation.EaseTypeBounceIn  // const
  public    static  ParadoxNotion.Animation.EaseTypeBounceOut  // const
  public    static  ParadoxNotion.Animation.EaseTypeBounceInOut  // const
  public    static  ParadoxNotion.Animation.EaseTypeBackIn  // const
  public    static  ParadoxNotion.Animation.EaseTypeBackOut  // const
  public    static  ParadoxNotion.Animation.EaseTypeBackInOut  // const
METHODS:
END_CLASS

CLASS: ParadoxNotion.Animation.Easing
TYPE:  static class
TOKEN: 0x2000512
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0A9A03F0  token=0x60018F9  System.Single Ease(ParadoxNotion.Animation.EaseType type, System.Single from, System.Single to, System.Single t)
  RVA=0x0A9A030C  token=0x60018FA  UnityEngine.Vector3 Ease(ParadoxNotion.Animation.EaseType type, UnityEngine.Vector3 from, UnityEngine.Vector3 to, System.Single t)
  RVA=0x0A9A0248  token=0x60018FB  UnityEngine.Quaternion Ease(ParadoxNotion.Animation.EaseType type, UnityEngine.Quaternion from, UnityEngine.Quaternion to, System.Single t)
  RVA=0x0A9A0474  token=0x60018FC  UnityEngine.Color Ease(ParadoxNotion.Animation.EaseType type, UnityEngine.Color from, UnityEngine.Color to, System.Single t)
  RVA=0x0A9A0700  token=0x60018FD  System.Func<System.Single,System.Single> Function(ParadoxNotion.Animation.EaseType type)
  RVA=0x041E1670  token=0x60018FE  System.Single Linear(System.Single t)
  RVA=0x04D886E0  token=0x60018FF  System.Single QuadraticIn(System.Single t)
  RVA=0x04DA6AB0  token=0x6001900  System.Single QuadraticOut(System.Single t)
  RVA=0x0A9A0E30  token=0x6001901  System.Single QuadraticInOut(System.Single t)
  RVA=0x04DA6AD0  token=0x6001902  System.Single QuarticIn(System.Single t)
  RVA=0x04DA6AE0  token=0x6001903  System.Single QuarticOut(System.Single t)
  RVA=0x0A9A0E8C  token=0x6001904  System.Single QuarticInOut(System.Single t)
  RVA=0x04DA6B10  token=0x6001905  System.Single QuinticIn(System.Single t)
  RVA=0x04DA6B30  token=0x6001906  System.Single QuinticOut(System.Single t)
  RVA=0x0A9A0EE0  token=0x6001907  System.Single QuinticInOut(System.Single t)
  RVA=0x04D89930  token=0x6001908  System.Single CubicIn(System.Single t)
  RVA=0x04DA69A0  token=0x6001909  System.Single CubicOut(System.Single t)
  RVA=0x0A9A01E0  token=0x600190A  System.Single CubicInOut(System.Single t)
  RVA=0x0A9A0F3C  token=0x600190B  System.Single SinusoidalIn(System.Single t)
  RVA=0x0A9A0F6C  token=0x600190C  System.Single SinusoidalOut(System.Single t)
  RVA=0x04DA6B60  token=0x600190D  System.Single SinusoidalInOut(System.Single t)
  RVA=0x0A9A0690  token=0x600190E  System.Single ExponentialIn(System.Single t)
  RVA=0x0A9A06BC  token=0x600190F  System.Single ExponentialOut(System.Single t)
  RVA=0x0A9A0610  token=0x6001910  System.Single ExponentialInOut(System.Single t)
  RVA=0x04DA6910  token=0x6001911  System.Single CircularIn(System.Single t)
  RVA=0x04DA6970  token=0x6001912  System.Single CircularOut(System.Single t)
  RVA=0x0A9A017C  token=0x6001913  System.Single CircularInOut(System.Single t)
  RVA=0x04DA69C0  token=0x6001914  System.Single ElasticIn(System.Single t)
  RVA=0x04DA6A40  token=0x6001915  System.Single ElasticOut(System.Single t)
  RVA=0x0A9A0530  token=0x6001916  System.Single ElasticInOut(System.Single t)
  RVA=0x0A9A00B0  token=0x6001917  System.Single BounceIn(System.Single t)
  RVA=0x0A9A00E4  token=0x6001918  System.Single BounceOut(System.Single t)
  RVA=0x0A9A0044  token=0x6001919  System.Single BounceInOut(System.Single t)
  RVA=0x04DA68C0  token=0x600191A  System.Single BackIn(System.Single t)
  RVA=0x04DA68E0  token=0x600191B  System.Single BackOut(System.Single t)
  RVA=0x0A99FFDC  token=0x600191C  System.Single BackInOut(System.Single t)
END_CLASS

CLASS: ParadoxNotion.Design.SpoofAOTAttribute
TYPE:  class
TOKEN: 0x2000513
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600191D  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Design.CancelSpoofAOTAttribute
TYPE:  class
TOKEN: 0x2000514
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600191E  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Design.DoNotListAttribute
TYPE:  class
TOKEN: 0x2000515
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600191F  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Design.ProtectedSingletonAttribute
TYPE:  class
TOKEN: 0x2000516
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6001920  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Design.ExecutionPriorityAttribute
TYPE:  class
TOKEN: 0x2000517
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.Int32                    priority  // 0x10
METHODS:
  RVA=0x014F51F0  token=0x6001921  System.Void .ctor(System.Int32 priority)
END_CLASS

CLASS: ParadoxNotion.Design.ExposeAsDefinitionAttribute
TYPE:  class
TOKEN: 0x2000518
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6001922  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Design.ExposeFieldAttribute
TYPE:  class
TOKEN: 0x2000519
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6001923  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Design.NodeCanvasOdinOnlyAttribute
TYPE:  class
TOKEN: 0x200051A
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6001924  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Design.ListInspectorOptionAttribute
TYPE:  class
TOKEN: 0x200051B
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.Boolean                  allowAdd  // 0x10
  public    readonly System.Boolean                  allowRemove  // 0x11
  public    readonly System.Boolean                  showFoldout  // 0x12
METHODS:
  RVA=0x04DA6B90  token=0x6001925  System.Void .ctor(System.Boolean allowAdd, System.Boolean allowRemove, System.Boolean alwaysExpanded)
END_CLASS

CLASS: ParadoxNotion.Design.NameAttribute
TYPE:  class
TOKEN: 0x200051C
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.String                   name  // 0x10
  public    readonly System.Int32                    priority  // 0x18
METHODS:
  RVA=0x0476C890  token=0x6001926  System.Void .ctor(System.String name, System.Int32 priority)
END_CLASS

CLASS: ParadoxNotion.Design.CategoryAttribute
TYPE:  class
TOKEN: 0x200051D
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.String                   category  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6001927  System.Void .ctor(System.String category)
END_CLASS

CLASS: ParadoxNotion.Design.DescriptionAttribute
TYPE:  class
TOKEN: 0x200051E
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.String                   description  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6001928  System.Void .ctor(System.String description)
END_CLASS

CLASS: ParadoxNotion.Design.IconAttribute
TYPE:  class
TOKEN: 0x200051F
SIZE:  0x30
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.String                   iconName  // 0x10
  public    readonly System.Boolean                  fixedColor  // 0x18
  public    readonly System.String                   runtimeIconTypeCallback  // 0x20
  public    readonly System.Type                     fromType  // 0x28
METHODS:
  RVA=0x048B7260  token=0x6001929  System.Void .ctor(System.String iconName, System.Boolean fixedColor, System.String runtimeIconTypeCallback)
  RVA=0x02FDB880  token=0x600192A  System.Void .ctor(System.Type fromType)
END_CLASS

CLASS: ParadoxNotion.Design.ColorAttribute
TYPE:  class
TOKEN: 0x2000520
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.String                   hexColor  // 0x10
METHODS:
  RVA=0x053908C0  token=0x600192B  System.Void .ctor(System.String hexColor)
END_CLASS

CLASS: ParadoxNotion.Design.ColorGradientAttribute
TYPE:  class
TOKEN: 0x2000521
SIZE:  0x30
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.String                   hexColorLeft  // 0x10
  public    readonly System.Single                   colorLeftRatio  // 0x18
  public    readonly System.String                   hexColorRight  // 0x20
  public    readonly System.Single                   colorRightRatio  // 0x28
  public    readonly System.Single                   rotateDegree  // 0x2c
PROPERTIES:
  colorLeft  get=0x0A99FF14
  colorRight  get=0x0A99FF78
METHODS:
  RVA=0x0A99FED0  token=0x600192E  System.Void .ctor(System.String hexColorLeft, System.Single colorLeftRatio, System.String hexColorRight, System.Single colorRightRatio, System.Single rotateDegree)
END_CLASS

CLASS: ParadoxNotion.Design.NodeMenuItemColorGradientAttribute
TYPE:  class
TOKEN: 0x2000522
SIZE:  0x30
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.String                   hexColorLeft  // 0x10
  public    readonly System.Single                   colorLeftRatio  // 0x18
  public    readonly System.String                   hexColorRight  // 0x20
  public    readonly System.Single                   colorRightRatio  // 0x28
  public    readonly System.Single                   rotateDegree  // 0x2c
PROPERTIES:
  colorLeft  get=0x0A9A3A4C
  colorRight  get=0x0A9A3AB0
METHODS:
  RVA=0x0A99FED0  token=0x6001931  System.Void .ctor(System.String hexColorLeft, System.Single colorLeftRatio, System.String hexColorRight, System.Single colorRightRatio, System.Single rotateDegree)
END_CLASS

CLASS: ParadoxNotion.Design.NodeCategoryMenuItemColorGradientAttribute
TYPE:  class
TOKEN: 0x2000523
SIZE:  0x30
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.String                   hexColorLeft  // 0x10
  public    readonly System.Single                   colorLeftRatio  // 0x18
  public    readonly System.String                   hexColorRight  // 0x20
  public    readonly System.Single                   colorRightRatio  // 0x28
  public    readonly System.Single                   rotateDegree  // 0x2c
PROPERTIES:
  colorLeft  get=0x0A9A3984
  colorRight  get=0x0A9A39E8
METHODS:
  RVA=0x0A99FED0  token=0x6001934  System.Void .ctor(System.String hexColorLeft, System.Single colorLeftRatio, System.String hexColorRight, System.Single colorRightRatio, System.Single rotateDegree)
END_CLASS

CLASS: ParadoxNotion.Design.HideAttribute
TYPE:  class
TOKEN: 0x2000524
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6001935  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Design.DrawerAttribute
TYPE:  abstract class
TOKEN: 0x2000525
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
PROPERTIES:
  priority  get=0x04DA5AE0
  isDecorator  get=0x012081B0
METHODS:
  RVA=0x041E1670  token=0x6001938  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Design.HeaderAttribute
TYPE:  class
TOKEN: 0x2000526
SIZE:  0x18
EXTENDS: ParadoxNotion.Design.DrawerAttribute
FIELDS:
  public    readonly System.String                   title  // 0x10
PROPERTIES:
  isDecorator  get=0x02FFF600
METHODS:
  RVA=0x053908C0  token=0x600193A  System.Void .ctor(System.String title)
END_CLASS

CLASS: ParadoxNotion.Design.DimIfDefaultAttribute
TYPE:  class
TOKEN: 0x2000527
SIZE:  0x10
EXTENDS: ParadoxNotion.Design.DrawerAttribute
FIELDS:
PROPERTIES:
  isDecorator  get=0x02FFF600
  priority  get=0x011EC580
METHODS:
  RVA=0x041E1670  token=0x600193D  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Design.ShowIfAttribute
TYPE:  class
TOKEN: 0x2000528
SIZE:  0x20
EXTENDS: ParadoxNotion.Design.DrawerAttribute
FIELDS:
  public    readonly System.String                   fieldName  // 0x10
  public    readonly System.Int32                    checkValue  // 0x18
PROPERTIES:
  isDecorator  get=0x02FFF600
  priority  get=0x04D85B80
METHODS:
  RVA=0x048B52D0  token=0x6001940  System.Void .ctor(System.String fieldName, System.Int32 checkValue)
END_CLASS

CLASS: ParadoxNotion.Design.RequiredFieldAttribute
TYPE:  class
TOKEN: 0x2000529
SIZE:  0x10
EXTENDS: ParadoxNotion.Design.DrawerAttribute
FIELDS:
PROPERTIES:
  isDecorator  get=0x012081B0
  priority  get=0x02BBE540
METHODS:
  RVA=0x041E1670  token=0x6001943  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Design.ShowButtonAttribute
TYPE:  class
TOKEN: 0x200052A
SIZE:  0x20
EXTENDS: ParadoxNotion.Design.DrawerAttribute
FIELDS:
  public    readonly System.String                   buttonTitle  // 0x10
  public    readonly System.String                   methodName  // 0x18
PROPERTIES:
  isDecorator  get=0x02FFF600
  priority  get=0x04D8C9E0
METHODS:
  RVA=0x032AF420  token=0x6001946  System.Void .ctor(System.String buttonTitle, System.String methodnameCallback)
END_CLASS

CLASS: ParadoxNotion.Design.CallbackAttribute
TYPE:  class
TOKEN: 0x200052B
SIZE:  0x18
EXTENDS: ParadoxNotion.Design.DrawerAttribute
FIELDS:
  public    readonly System.String                   methodName  // 0x10
PROPERTIES:
  isDecorator  get=0x02FFF600
  priority  get=0x04D882B0
METHODS:
  RVA=0x053908C0  token=0x6001949  System.Void .ctor(System.String methodName)
END_CLASS

CLASS: ParadoxNotion.Design.MinValueAttribute
TYPE:  class
TOKEN: 0x200052C
SIZE:  0x18
EXTENDS: ParadoxNotion.Design.DrawerAttribute
FIELDS:
  public    readonly System.Single                   min  // 0x10
PROPERTIES:
  priority  get=0x04D9EC50
METHODS:
  RVA=0x04D88280  token=0x600194B  System.Void .ctor(System.Single min)
  RVA=0x04DA6BA0  token=0x600194C  System.Void .ctor(System.Int32 min)
END_CLASS

CLASS: ParadoxNotion.Design.DelayedFieldAttribute
TYPE:  class
TOKEN: 0x200052D
SIZE:  0x10
EXTENDS: ParadoxNotion.Design.DrawerAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600194D  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Design.LayerFieldAttribute
TYPE:  class
TOKEN: 0x200052E
SIZE:  0x10
EXTENDS: ParadoxNotion.Design.DrawerAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600194E  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Design.TagFieldAttribute
TYPE:  class
TOKEN: 0x200052F
SIZE:  0x10
EXTENDS: ParadoxNotion.Design.DrawerAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600194F  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Design.TextAreaFieldAttribute
TYPE:  class
TOKEN: 0x2000530
SIZE:  0x18
EXTENDS: ParadoxNotion.Design.DrawerAttribute
FIELDS:
  public    readonly System.Int32                    numberOfLines  // 0x10
METHODS:
  RVA=0x014F51F0  token=0x6001950  System.Void .ctor(System.Int32 numberOfLines)
END_CLASS

CLASS: ParadoxNotion.Design.PopupFieldAttribute
TYPE:  class
TOKEN: 0x2000531
SIZE:  0x18
EXTENDS: ParadoxNotion.Design.DrawerAttribute
FIELDS:
  public    readonly System.Object[]                 options  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6001951  System.Void .ctor(System.Object[] options)
END_CLASS

CLASS: ParadoxNotion.Design.SliderFieldAttribute
TYPE:  class
TOKEN: 0x2000532
SIZE:  0x18
EXTENDS: ParadoxNotion.Design.DrawerAttribute
FIELDS:
  public    readonly System.Single                   min  // 0x10
  public    readonly System.Single                   max  // 0x14
METHODS:
  RVA=0x04D8D120  token=0x6001952  System.Void .ctor(System.Single min, System.Single max)
  RVA=0x04DA6BB0  token=0x6001953  System.Void .ctor(System.Int32 min, System.Int32 max)
END_CLASS

CLASS: ParadoxNotion.Design.ForceObjectFieldAttribute
TYPE:  class
TOKEN: 0x2000533
SIZE:  0x10
EXTENDS: ParadoxNotion.Design.DrawerAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6001954  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Design.UndoUtility
TYPE:  static class
TOKEN: 0x2000534
SIZE:  0x10
FIELDS:
  private   static  System.String                   <lastOperationName>k__BackingField  // static @ 0x0
PROPERTIES:
  lastOperationName  get=0x0A9A469C  set=0x0A9A46D4
METHODS:
  RVA=0x041E1670  token=0x6001957  System.Void RecordObject(UnityEngine.Object target, System.String name, System.Boolean forceIgnoreScene)
  RVA=0x041E1670  token=0x6001958  System.Void RecordObjectComplete(UnityEngine.Object target, System.String name, System.Boolean forceIgnore)
  RVA=0x041E1670  token=0x6001959  System.Void SetDirty(UnityEngine.Object target)
  RVA=0x0A9A4680  token=0x600195A  System.Void RecordObject(UnityEngine.Object target, System.String name, System.Action operation)
  RVA=0x0A9A4680  token=0x600195B  System.Void RecordObjectComplete(UnityEngine.Object target, System.String name, System.Action operation)
  RVA=0x0A9A4654  token=0x600195C  System.String GetLastOperationNameOr(System.String operation)
  RVA=0x0A9A45A4  token=0x600195D  System.Void CheckUndo(UnityEngine.Object target, System.String name)
  RVA=0x0A9A4564  token=0x600195E  System.Void CheckDirty(UnityEngine.Object target)
END_CLASS

CLASS: ParadoxNotion.Serialization.DeserializeFromAttribute
TYPE:  class
TOKEN: 0x20004C2
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.String                   previousTypeFullName  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6001737  System.Void .ctor(System.String previousTypeFullName)
END_CLASS

CLASS: ParadoxNotion.Serialization.fsRecoveryProcessor`2
TYPE:  class
TOKEN: 0x20004C3
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsObjectProcessor
FIELDS:
  private   static  System.String                   FIELD_NAME_TYPE  // const
  private   static  System.String                   FIELD_NAME_STATE  // const
METHODS:
  RVA=-1  // not resolved  token=0x6001738  System.Boolean CanProcess(System.Type type)
  RVA=-1  // not resolved  token=0x6001739  System.Void OnBeforeDeserialize(System.Type storageType, ParadoxNotion.Serialization.FullSerializer.fsData& data)
  RVA=-1  // not resolved  token=0x600173A  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.fsUnityObjectConverter
TYPE:  class
TOKEN: 0x20004C6
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsConverter
FIELDS:
METHODS:
  RVA=0x0321CAF0  token=0x6001741  System.Boolean CanProcess(System.Type type)
  RVA=0x012081B0  token=0x6001742  System.Boolean RequestCycleSupport(System.Type storageType)
  RVA=0x012081B0  token=0x6001743  System.Boolean RequestInheritanceSupport(System.Type storageType)
  RVA=0x0A99F51C  token=0x6001744  ParadoxNotion.Serialization.FullSerializer.fsResult TrySerialize(System.Object instance, ParadoxNotion.Serialization.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x04250BD0  token=0x6001745  ParadoxNotion.Serialization.FullSerializer.fsResult TryDeserialize(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=0x011EC580  token=0x6001746  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x041E1670  token=0x6001747  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.IMissingRecoverable
TYPE:  interface
TOKEN: 0x20004C7
FIELDS:
PROPERTIES:
  missingType  get=-1  // abstract  set=-1  // abstract
  recoveryState  get=-1  // abstract  set=-1  // abstract
METHODS:
END_CLASS

CLASS: ParadoxNotion.Serialization.ISerializedMethodBaseInfo
TYPE:  interface
TOKEN: 0x20004C8
IMPLEMENTS: ParadoxNotion.Serialization.ISerializedReflectedInfo UnityEngine.ISerializationCallbackReceiver
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600174C  System.Reflection.MethodBase GetMethodBase()
  RVA=-1  // abstract  token=0x600174D  System.Boolean HasChanged()
END_CLASS

CLASS: ParadoxNotion.Serialization.ISerializedReflectedInfo
TYPE:  interface
TOKEN: 0x20004C9
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600174E  System.Reflection.MemberInfo AsMemberInfo()
  RVA=-1  // abstract  token=0x600174F  System.String AsString()
END_CLASS

CLASS: ParadoxNotion.Serialization.JSONSerializer
TYPE:  static class
TOKEN: 0x20004CA
SIZE:  0x10
FIELDS:
  public    static  System.Boolean                  enableCache  // static @ 0x0
  private   static  System.Object                   serializerLock  // static @ 0x8
  private   static  ParadoxNotion.Serialization.FullSerializer.fsSerializerserializer  // static @ 0x10
  private   static  System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData>dataCache  // static @ 0x18
METHODS:
  RVA=0x044D7850  token=0x6001750  System.Void .cctor()
  RVA=0x044D78D0  token=0x6001751  System.Void FlushMem()
  RVA=0x044D7A00  token=0x6001752  System.Void __FlushDataCache()
  RVA=0x0A99436C  token=0x6001753  System.String Serialize(System.Type type, System.Object instance, System.Collections.Generic.List<UnityEngine.Object> references, System.Boolean pretyJson)
  RVA=-1  // generic def  token=0x6001754  T Deserialize(System.String json, System.Collections.Generic.List<UnityEngine.Object> references)
  RVA=0x0A993ECC  token=0x6001755  System.Object Deserialize(System.Type type, System.String json, System.Collections.Generic.List<UnityEngine.Object> references)
  RVA=-1  // generic def  token=0x6001756  T TryDeserializeOverwrite(T instance, System.String json, System.Collections.Generic.List<UnityEngine.Object> references)
  RVA=0x0A9948BC  token=0x6001757  System.Object TryDeserializeOverwrite(System.Object instance, System.String json, System.Collections.Generic.List<UnityEngine.Object> references)
  RVA=0x03113DB0  token=0x6001758  System.Object Internal_Deserialize(System.Type type, System.String json, System.Collections.Generic.List<UnityEngine.Object> references, System.Object instance)
  RVA=0x0A993F50  token=0x6001759  System.Void SerializeAndExecuteNoCycles(System.Type type, System.Object instance, System.Action<System.Object,ParadoxNotion.Serialization.FullSerializer.fsData> call)
  RVA=0x0A994134  token=0x600175A  System.Void SerializeAndExecuteNoCycles(System.Type type, System.Object instance, System.Action<System.Object> beforeCall, System.Action<System.Object,ParadoxNotion.Serialization.FullSerializer.fsData> afterCall)
  RVA=-1  // generic def  token=0x600175B  T Clone(T original)
  RVA=0x0A993D34  token=0x600175C  System.Object Clone(System.Object original)
  RVA=0x0A993DF8  token=0x600175D  System.Void CopySerialized(System.Object source, System.Object target)
  RVA=0x0A9947CC  token=0x600175E  System.Void ShowData(System.String json, System.String fileName)
  RVA=0x0A993F34  token=0x600175F  System.String PrettifyJson(System.String json)
END_CLASS

CLASS: ParadoxNotion.Serialization.SerializationPair
TYPE:  sealed class
TOKEN: 0x20004CB
SIZE:  0x20
FIELDS:
  public            System.String                   _json  // 0x10
  public            System.Collections.Generic.List<UnityEngine.Object>_references  // 0x18
METHODS:
  RVA=0x0A996B50  token=0x6001760  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.SerializedConstructorInfo
TYPE:  class
TOKEN: 0x20004CC
SIZE:  0x30
IMPLEMENTS: ParadoxNotion.Serialization.ISerializedMethodBaseInfo ParadoxNotion.Serialization.ISerializedReflectedInfo UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           System.String                   _baseInfo  // 0x10
  private           System.String                   _paramsInfo  // 0x18
  private           System.Reflection.ConstructorInfo_constructor  // 0x20
  private           System.Boolean                  _hasChanged  // 0x28
METHODS:
  RVA=0x0A996F94  token=0x6001761  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x0A996C84  token=0x6001762  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x041E1670  token=0x6001763  System.Void .ctor()
  RVA=0x0A997214  token=0x6001764  System.Void .ctor(System.Reflection.ConstructorInfo constructor)
  RVA=0x04D862C0  token=0x6001765  System.Reflection.MemberInfo AsMemberInfo()
  RVA=0x04D862C0  token=0x6001766  System.Reflection.MethodBase GetMethodBase()
  RVA=0x011F33C0  token=0x6001767  System.Boolean HasChanged()
  RVA=0x0A996BC8  token=0x6001768  System.String AsString()
  RVA=0x0A996C7C  token=0x6001769  System.String ToString()
  RVA=0x0A997228  token=0x600176A  System.Reflection.ConstructorInfo op_Implicit(ParadoxNotion.Serialization.SerializedConstructorInfo value)
END_CLASS

CLASS: ParadoxNotion.Serialization.SerializedEventInfo
TYPE:  class
TOKEN: 0x20004CF
SIZE:  0x20
IMPLEMENTS: ParadoxNotion.Serialization.ISerializedReflectedInfo UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           System.String                   _baseInfo  // 0x10
  private           System.Reflection.EventInfo     _event  // 0x18
METHODS:
  RVA=0x0A9973A0  token=0x6001770  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x0A9972A0  token=0x6001771  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x041E1670  token=0x6001772  System.Void .ctor()
  RVA=0x05392C40  token=0x6001773  System.Void .ctor(System.Reflection.EventInfo info)
  RVA=0x0385B100  token=0x6001774  System.Reflection.MemberInfo AsMemberInfo()
  RVA=0x0A997238  token=0x6001775  System.String AsString()
  RVA=0x0A997298  token=0x6001776  System.String ToString()
  RVA=0x0A99745C  token=0x6001777  System.Reflection.EventInfo op_Implicit(ParadoxNotion.Serialization.SerializedEventInfo value)
END_CLASS

CLASS: ParadoxNotion.Serialization.SerializedFieldInfo
TYPE:  class
TOKEN: 0x20004D0
SIZE:  0x20
IMPLEMENTS: ParadoxNotion.Serialization.ISerializedReflectedInfo UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           System.String                   _baseInfo  // 0x10
  private           System.Reflection.FieldInfo     _field  // 0x18
METHODS:
  RVA=0x0A9975E4  token=0x6001778  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x0A9974E4  token=0x6001779  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x041E1670  token=0x600177A  System.Void .ctor()
  RVA=0x05392C40  token=0x600177B  System.Void .ctor(System.Reflection.FieldInfo info)
  RVA=0x0385B100  token=0x600177C  System.Reflection.MemberInfo AsMemberInfo()
  RVA=0x0A99746C  token=0x600177D  System.String AsString()
  RVA=0x0A9974DC  token=0x600177E  System.String ToString()
  RVA=0x0A99745C  token=0x600177F  System.Reflection.FieldInfo op_Implicit(ParadoxNotion.Serialization.SerializedFieldInfo value)
END_CLASS

CLASS: ParadoxNotion.Serialization.SerializedMethodInfo
TYPE:  class
TOKEN: 0x20004D1
SIZE:  0x38
IMPLEMENTS: ParadoxNotion.Serialization.ISerializedMethodBaseInfo ParadoxNotion.Serialization.ISerializedReflectedInfo UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           System.String                   _baseInfo  // 0x10
  private           System.String                   _paramsInfo  // 0x18
  private           System.String                   _genericArgumentsInfo  // 0x20
  private           System.Reflection.MethodInfo    _method  // 0x28
  private           System.Boolean                  _hasChanged  // 0x30
METHODS:
  RVA=0x0A997CE0  token=0x6001780  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x0A99775C  token=0x6001781  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x041E1670  token=0x6001782  System.Void .ctor()
  RVA=0x0A998134  token=0x6001783  System.Void .ctor(System.Reflection.MethodInfo method)
  RVA=0x04D86240  token=0x6001784  System.Reflection.MemberInfo AsMemberInfo()
  RVA=0x04D86240  token=0x6001785  System.Reflection.MethodBase GetMethodBase()
  RVA=0x04D865F0  token=0x6001786  System.Boolean HasChanged()
  RVA=0x0A9976A0  token=0x6001787  System.String AsString()
  RVA=0x0A997754  token=0x6001788  System.String ToString()
  RVA=0x0A998148  token=0x6001789  System.Reflection.MethodInfo op_Implicit(ParadoxNotion.Serialization.SerializedMethodInfo value)
END_CLASS

CLASS: ParadoxNotion.Serialization.SerializedTypeInfo
TYPE:  class
TOKEN: 0x20004D4
SIZE:  0x20
IMPLEMENTS: ParadoxNotion.Serialization.ISerializedReflectedInfo UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           System.String                   _baseInfo  // 0x10
  private           System.Type                     _type  // 0x18
METHODS:
  RVA=0x0A9981C0  token=0x6001791  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x0A998158  token=0x6001792  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x041E1670  token=0x6001793  System.Void .ctor()
  RVA=0x0A8DA46C  token=0x6001794  System.Void .ctor(System.Type info)
  RVA=0x0385B100  token=0x6001795  System.Reflection.MemberInfo AsMemberInfo()
  RVA=0x02B2ECC0  token=0x6001796  System.String AsString()
  RVA=0x02B2ECC0  token=0x6001797  System.String ToString()
  RVA=0x0A99745C  token=0x6001798  System.Type op_Implicit(ParadoxNotion.Serialization.SerializedTypeInfo value)
END_CLASS

CLASS: ParadoxNotion.Serialization.SerializedUnityEventInfo
TYPE:  class
TOKEN: 0x20004D5
SIZE:  0x20
IMPLEMENTS: ParadoxNotion.Serialization.ISerializedReflectedInfo UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           System.String                   _baseInfo  // 0x10
  private           System.Reflection.MemberInfo    _memberInfo  // 0x18
PROPERTIES:
  isStatic  get=0x0A998680
  memberType  get=0x0A998758
METHODS:
  RVA=0x0A9984F8  token=0x600179B  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x0A9982A8  token=0x600179C  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x041E1670  token=0x600179D  System.Void .ctor()
  RVA=0x05392C40  token=0x600179E  System.Void .ctor(System.Reflection.FieldInfo info)
  RVA=0x05392C40  token=0x600179F  System.Void .ctor(System.Reflection.PropertyInfo info)
  RVA=0x0A9985B4  token=0x60017A0  System.Void .ctor(System.Reflection.MemberInfo info)
  RVA=0x0385B100  token=0x60017A1  System.Reflection.MemberInfo AsMemberInfo()
  RVA=0x0A998230  token=0x60017A2  System.String AsString()
  RVA=0x0A9982A0  token=0x60017A3  System.String ToString()
  RVA=0x0A998870  token=0x60017A4  System.Reflection.FieldInfo op_Implicit(ParadoxNotion.Serialization.SerializedUnityEventInfo value)
  RVA=0x0A998828  token=0x60017A5  System.Reflection.PropertyInfo op_Implicit(ParadoxNotion.Serialization.SerializedUnityEventInfo value)
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsForwardAttribute
TYPE:  sealed class
TOKEN: 0x20004D6
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public            System.String                   MemberName  // 0x10
METHODS:
  RVA=0x053908C0  token=0x60017A6  System.Void .ctor(System.String memberName)
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsWriteOnlyAttribute
TYPE:  sealed class
TOKEN: 0x20004D7
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60017A7  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsReadOnlyAttribute
TYPE:  sealed class
TOKEN: 0x20004D8
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60017A8  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsIgnoreAttribute
TYPE:  sealed class
TOKEN: 0x20004D9
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60017A9  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsIgnoreInBuildAttribute
TYPE:  sealed class
TOKEN: 0x20004DA
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60017AA  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsSerializeAsAttribute
TYPE:  sealed class
TOKEN: 0x20004DB
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.String                   Name  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60017AB  System.Void .ctor()
  RVA=0x053908C0  token=0x60017AC  System.Void .ctor(System.String name)
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsMigrateToAttribute
TYPE:  class
TOKEN: 0x20004DC
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.Type                     targetType  // 0x10
METHODS:
  RVA=0x053908C0  token=0x60017AD  System.Void .ctor(System.Type targetType)
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsMigrateVersionsAttribute
TYPE:  class
TOKEN: 0x20004DD
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.Type[]                   previousTypes  // 0x10
METHODS:
  RVA=0x053908C0  token=0x60017AE  System.Void .ctor(System.Type[] previousTypes)
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsSerializeAsReference
TYPE:  sealed class
TOKEN: 0x20004DE
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60017AF  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsDeserializeOverwrite
TYPE:  sealed class
TOKEN: 0x20004DF
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60017B0  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsUninitialized
TYPE:  class
TOKEN: 0x20004E0
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60017B1  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsAutoInstance
TYPE:  sealed class
TOKEN: 0x20004E1
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.Boolean                  makeInstance  // 0x10
METHODS:
  RVA=0x04D86130  token=0x60017B2  System.Void .ctor(System.Boolean makeInstance)
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsObjectAttribute
TYPE:  sealed class
TOKEN: 0x20004E2
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public            System.Type                     Converter  // 0x10
  public            System.Type                     Processor  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60017B3  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsBaseConverter
TYPE:  abstract class
TOKEN: 0x20004E3
SIZE:  0x18
FIELDS:
  public            ParadoxNotion.Serialization.FullSerializer.fsSerializerSerializer  // 0x10
METHODS:
  RVA=0x0360F660  token=0x60017B4  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x0A99B4EC  token=0x60017B5  System.Boolean RequestCycleSupport(System.Type storageType)
  RVA=0x0A99B57C  token=0x60017B6  System.Boolean RequestInheritanceSupport(System.Type storageType)
  RVA=-1  // abstract  token=0x60017B7  ParadoxNotion.Serialization.FullSerializer.fsResult TrySerialize(System.Object instance, ParadoxNotion.Serialization.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=-1  // abstract  token=0x60017B8  ParadoxNotion.Serialization.FullSerializer.fsResult TryDeserialize(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=0x0A99B1A8  token=0x60017B9  ParadoxNotion.Serialization.FullSerializer.fsResult FailExpectedType(ParadoxNotion.Serialization.FullSerializer.fsData data, ParadoxNotion.Serialization.FullSerializer.fsDataType[] types)
  RVA=0x0360FCA0  token=0x60017BA  ParadoxNotion.Serialization.FullSerializer.fsResult CheckType(ParadoxNotion.Serialization.FullSerializer.fsData data, ParadoxNotion.Serialization.FullSerializer.fsDataType type)
  RVA=0x0A99AF94  token=0x60017BB  ParadoxNotion.Serialization.FullSerializer.fsResult CheckKey(ParadoxNotion.Serialization.FullSerializer.fsData data, System.String key, ParadoxNotion.Serialization.FullSerializer.fsData& subitem)
  RVA=0x0A99B000  token=0x60017BC  ParadoxNotion.Serialization.FullSerializer.fsResult CheckKey(System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> data, System.String key, ParadoxNotion.Serialization.FullSerializer.fsData& subitem)
  RVA=-1  // generic def  token=0x60017BD  ParadoxNotion.Serialization.FullSerializer.fsResult SerializeMember(System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> data, System.Type overrideConverterType, System.String name, T value)
  RVA=-1  // generic def  token=0x60017BE  ParadoxNotion.Serialization.FullSerializer.fsResult DeserializeMember(System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> data, System.Type overrideConverterType, System.String name, T& value)
  RVA=0x041E1670  token=0x60017BF  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsConverter
TYPE:  abstract class
TOKEN: 0x20004E5
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsBaseConverter
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60017C3  System.Boolean CanProcess(System.Type type)
  RVA=0x041E1670  token=0x60017C4  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsCyclicReferenceManager
TYPE:  class
TOKEN: 0x20004E6
SIZE:  0x30
FIELDS:
  private           System.Collections.Generic.Dictionary<System.Object,System.Int32>_objectIds  // 0x10
  private           System.Int32                    _nextId  // 0x18
  private           System.Collections.Generic.Dictionary<System.Int32,System.Object>_marked  // 0x20
  private           System.Int32                    _depth  // 0x28
METHODS:
  RVA=0x044D8810  token=0x60017C5  System.Void .ctor()
  RVA=0x03114840  token=0x60017C6  System.Void Clear()
  RVA=0x0360ED40  token=0x60017C7  System.Boolean Enter()
  RVA=0x031148B0  token=0x60017C8  System.Boolean Exit()
  RVA=0x036FD5D0  token=0x60017C9  System.Object GetReferenceObject(System.Int32 id)
  RVA=0x04521F10  token=0x60017CA  System.Void AddReferenceWithId(System.Int32 id, System.Object reference)
  RVA=0x0A99B5A0  token=0x60017CB  System.Int32 GetReferenceId(System.Object item)
  RVA=0x0A99B648  token=0x60017CC  System.Boolean IsReference(System.Object item)
  RVA=0x0A99B6B8  token=0x60017CD  System.Void MarkSerialized(System.Object item)
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsDataType
TYPE:  sealed struct
TOKEN: 0x20004E8
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ParadoxNotion.Serialization.FullSerializer.fsDataTypeArray  // const
  public    static  ParadoxNotion.Serialization.FullSerializer.fsDataTypeObject  // const
  public    static  ParadoxNotion.Serialization.FullSerializer.fsDataTypeDouble  // const
  public    static  ParadoxNotion.Serialization.FullSerializer.fsDataTypeInt64  // const
  public    static  ParadoxNotion.Serialization.FullSerializer.fsDataTypeBoolean  // const
  public    static  ParadoxNotion.Serialization.FullSerializer.fsDataTypeString  // const
  public    static  ParadoxNotion.Serialization.FullSerializer.fsDataTypeNull  // const
METHODS:
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsData
TYPE:  sealed class
TOKEN: 0x20004E9
SIZE:  0x18
FIELDS:
  private           System.Object                   _value  // 0x10
  public    static readonly ParadoxNotion.Serialization.FullSerializer.fsDataTrue  // static @ 0x0
  public    static readonly ParadoxNotion.Serialization.FullSerializer.fsDataFalse  // static @ 0x8
  public    static readonly ParadoxNotion.Serialization.FullSerializer.fsDataNull  // static @ 0x10
PROPERTIES:
  Type  get=0x0360FD30
  IsNull  get=0x0360ED30
  IsDouble  get=0x0446EC20
  IsInt64  get=0x04250E60
  IsBool  get=0x0A99C0FC
  IsString  get=0x04250E40
  IsDictionary  get=0x0360EDC0
  IsList  get=0x0A99C148
  AsDouble  get=0x0A99C0F0
  AsInt64  get=0x05397344
  AsBool  get=0x0A99C0E4
  AsString  get=0x05393358
  AsDictionary  get=0x0539334C
  AsList  get=0x05396A3C
METHODS:
  RVA=0x040E20A0  token=0x60017D3  System.Void .ctor()
  RVA=0x040E2070  token=0x60017D4  System.Void .ctor(System.Boolean boolean)
  RVA=0x043977A0  token=0x60017D5  System.Void .ctor(System.Double f)
  RVA=0x0454C230  token=0x60017D6  System.Void .ctor(System.Int64 i)
  RVA=0x053908C0  token=0x60017D7  System.Void .ctor(System.String str)
  RVA=0x053908C0  token=0x60017D8  System.Void .ctor(System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> dict)
  RVA=0x053908C0  token=0x60017D9  System.Void .ctor(System.Collections.Generic.List<ParadoxNotion.Serialization.FullSerializer.fsData> list)
  RVA=0x0A99B8F8  token=0x60017DA  ParadoxNotion.Serialization.FullSerializer.fsData CreateDictionary()
  RVA=0x0A99B9EC  token=0x60017DB  ParadoxNotion.Serialization.FullSerializer.fsData CreateList()
  RVA=0x0A99BA84  token=0x60017DC  ParadoxNotion.Serialization.FullSerializer.fsData CreateList(System.Int32 capacity)
  RVA=0x0A99B7E8  token=0x60017DD  System.Void BecomeDictionary()
  RVA=0x0A99B88C  token=0x60017DE  ParadoxNotion.Serialization.FullSerializer.fsData Clone()
  RVA=-1  // generic def  token=0x60017EC  T Cast()
  RVA=0x0A99C0DC  token=0x60017ED  System.String ToString()
  RVA=0x0A99BB20  token=0x60017EE  System.Boolean Equals(System.Object obj)
  RVA=0x0A99BB84  token=0x60017EF  System.Boolean Equals(ParadoxNotion.Serialization.FullSerializer.fsData other)
  RVA=0x0A99C18C  token=0x60017F0  System.Boolean op_Equality(ParadoxNotion.Serialization.FullSerializer.fsData a, ParadoxNotion.Serialization.FullSerializer.fsData b)
  RVA=0x0A99C25C  token=0x60017F1  System.Boolean op_Inequality(ParadoxNotion.Serialization.FullSerializer.fsData a, ParadoxNotion.Serialization.FullSerializer.fsData b)
  RVA=0x08479504  token=0x60017F2  System.Int32 GetHashCode()
  RVA=0x040E1CB0  token=0x60017F3  System.Void .cctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsDirectConverter
TYPE:  abstract class
TOKEN: 0x20004EA
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsBaseConverter
FIELDS:
PROPERTIES:
  ModelType  get=-1  // abstract
METHODS:
  RVA=0x041E1670  token=0x60017F5  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsDirectConverter`1
TYPE:  abstract class
TOKEN: 0x20004EB
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsDirectConverter
FIELDS:
PROPERTIES:
  ModelType  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60017F7  ParadoxNotion.Serialization.FullSerializer.fsResult TrySerialize(System.Object instance, ParadoxNotion.Serialization.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=-1  // not resolved  token=0x60017F8  ParadoxNotion.Serialization.FullSerializer.fsResult TryDeserialize(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=-1  // abstract  token=0x60017F9  ParadoxNotion.Serialization.FullSerializer.fsResult DoSerialize(TModel model, System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> serialized)
  RVA=-1  // abstract  token=0x60017FA  ParadoxNotion.Serialization.FullSerializer.fsResult DoDeserialize(System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> data, TModel& model)
  RVA=-1  // not resolved  token=0x60017FB  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsGlobalConfig
TYPE:  static class
TOKEN: 0x20004EC
SIZE:  0x10
FIELDS:
  public    static  System.Boolean                  SerializeDefaultValues  // static @ 0x0
  public    static  System.Boolean                  IsCaseSensitive  // static @ 0x1
  public    static  System.Type[]                   IgnoreSerializeAttributes  // static @ 0x8
  public    static  System.Type[]                   SerializeAttributes  // static @ 0x10
  public    static  System.String                   CustomDateTimeFormatString  // static @ 0x18
  public    static  System.Boolean                  Serialize64BitIntegerAsString  // static @ 0x20
  public    static  System.Boolean                  SerializeEnumsAsInteger  // static @ 0x21
METHODS:
  RVA=0x049AFAC0  token=0x60017FC  System.Void .cctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsJsonParser
TYPE:  class
TOKEN: 0x20004ED
SIZE:  0x28
FIELDS:
  private           System.Int32                    _start  // 0x10
  private           System.String                   _input  // 0x18
  private   readonly System.Text.StringBuilder       _cachedStringBuilder  // 0x20
METHODS:
  RVA=0x0A99C2B0  token=0x60017FD  ParadoxNotion.Serialization.FullSerializer.fsResult MakeFailure(System.String message)
  RVA=0x0360D650  token=0x60017FE  System.Boolean TryMoveNext()
  RVA=0x05393340  token=0x60017FF  System.Boolean HasValue()
  RVA=0x0360D680  token=0x6001800  System.Boolean HasValue(System.Int32 offset)
  RVA=0x05393334  token=0x6001801  System.Char Character()
  RVA=0x0360D6C0  token=0x6001802  System.Char Character(System.Int32 offset)
  RVA=0x0360D090  token=0x6001803  System.Void SkipSpace()
  RVA=0x03FA24C0  token=0x6001804  System.Boolean IsHex(System.Char c)
  RVA=0x03FA2570  token=0x6001805  System.UInt32 ParseSingleChar(System.Char c1, System.UInt32 multipliyer)
  RVA=0x03FA24E0  token=0x6001806  System.UInt32 ParseUnicode(System.Char c1, System.Char c2, System.Char c3, System.Char c4)
  RVA=0x03FA22A0  token=0x6001807  ParadoxNotion.Serialization.FullSerializer.fsResult TryUnescapeChar(System.Char& escaped)
  RVA=0x040E1FB0  token=0x6001808  ParadoxNotion.Serialization.FullSerializer.fsResult TryParseExact(System.String content)
  RVA=0x040E1EE0  token=0x6001809  ParadoxNotion.Serialization.FullSerializer.fsResult TryParseTrue(ParadoxNotion.Serialization.FullSerializer.fsData& data)
  RVA=0x040E1DC0  token=0x600180A  ParadoxNotion.Serialization.FullSerializer.fsResult TryParseFalse(ParadoxNotion.Serialization.FullSerializer.fsData& data)
  RVA=0x0A99C43C  token=0x600180B  ParadoxNotion.Serialization.FullSerializer.fsResult TryParseNull(ParadoxNotion.Serialization.FullSerializer.fsData& data)
  RVA=0x0360E020  token=0x600180C  System.Boolean IsSeparator(System.Char c)
  RVA=0x0360DD70  token=0x600180D  ParadoxNotion.Serialization.FullSerializer.fsResult TryParseNumber(ParadoxNotion.Serialization.FullSerializer.fsData& data)
  RVA=0x0360D330  token=0x600180E  ParadoxNotion.Serialization.FullSerializer.fsResult TryParseString(System.String& str)
  RVA=0x04030FC0  token=0x600180F  ParadoxNotion.Serialization.FullSerializer.fsResult TryParseArray(ParadoxNotion.Serialization.FullSerializer.fsData& arr)
  RVA=0x0360CCB0  token=0x6001810  ParadoxNotion.Serialization.FullSerializer.fsResult TryParseObject(ParadoxNotion.Serialization.FullSerializer.fsData& obj)
  RVA=0x0360D700  token=0x6001811  ParadoxNotion.Serialization.FullSerializer.fsResult RunParse(ParadoxNotion.Serialization.FullSerializer.fsData& data)
  RVA=0x031145F0  token=0x6001812  ParadoxNotion.Serialization.FullSerializer.fsResult Parse(System.String input, ParadoxNotion.Serialization.FullSerializer.fsData& data)
  RVA=0x03114500  token=0x6001813  ParadoxNotion.Serialization.FullSerializer.fsData Parse(System.String input)
  RVA=0x0374B2F0  token=0x6001814  System.Void .ctor(System.String input)
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsJsonPrinter
TYPE:  static class
TOKEN: 0x20004EE
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0A99D7BC  token=0x6001815  System.Void InsertSpacing(System.IO.TextWriter stream, System.Int32 count)
  RVA=0x0A99D500  token=0x6001816  System.String EscapeString(System.String str)
  RVA=0x0A99C538  token=0x6001817  System.Void BuildCompressedString(ParadoxNotion.Serialization.FullSerializer.fsData data, System.IO.TextWriter stream)
  RVA=0x0A99CB14  token=0x6001818  System.Void BuildPrettyString(ParadoxNotion.Serialization.FullSerializer.fsData data, System.IO.TextWriter stream, System.Int32 depth)
  RVA=0x0A99D994  token=0x6001819  System.String ToJson(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Boolean pretty)
  RVA=0x0A99D828  token=0x600181A  System.Void PrettyJson(ParadoxNotion.Serialization.FullSerializer.fsData data, System.IO.TextWriter outputStream)
  RVA=0x0A99D834  token=0x600181B  System.String PrettyJson(ParadoxNotion.Serialization.FullSerializer.fsData data)
  RVA=0x0A99D3BC  token=0x600181C  System.Void CompressedJson(ParadoxNotion.Serialization.FullSerializer.fsData data, System.IO.StreamWriter outputStream)
  RVA=0x0A99D260  token=0x600181D  System.String CompressedJson(ParadoxNotion.Serialization.FullSerializer.fsData data)
  RVA=0x0A99D3C4  token=0x600181E  System.String ConvertDoubleToString(System.Double d)
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsMetaProperty
TYPE:  class
TOKEN: 0x20004EF
SIZE:  0x28
FIELDS:
  private           System.Reflection.FieldInfo     <Field>k__BackingField  // 0x10
  private           System.String                   <JsonName>k__BackingField  // 0x18
  private           System.Boolean                  <ReadOnly>k__BackingField  // 0x20
  private           System.Boolean                  <WriteOnly>k__BackingField  // 0x21
  private           System.Boolean                  <AutoInstance>k__BackingField  // 0x22
  private           System.Boolean                  <AsReference>k__BackingField  // 0x23
PROPERTIES:
  Field  get=0x02B2ECC0  set=0x053908C0
  JsonName  get=0x0385B100  set=0x05392C40
  StorageType  get=0x0360E450
  MemberName  get=0x0A99D9B8
  ReadOnly  get=0x04D866B0  set=0x04D866D0
  WriteOnly  get=0x04D86850  set=0x04D86860
  AutoInstance  get=0x04D86AD0  set=0x04D86AE0
  AsReference  get=0x04D8E070  set=0x04D8E080
METHODS:
  RVA=0x0321D640  token=0x600182D  System.Void .ctor(System.Reflection.FieldInfo field)
  RVA=0x08B984D4  token=0x600182E  System.Object Read(System.Object context)
  RVA=0x0360F610  token=0x600182F  System.Void Write(System.Object context, System.Object value)
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsMetaType
TYPE:  class
TOKEN: 0x20004F0
SIZE:  0x30
FIELDS:
  private   static  System.Collections.Generic.Dictionary<System.Type,ParadoxNotion.Serialization.FullSerializer.fsMetaType>_metaTypes  // static @ 0x0
  private   static  System.Collections.Generic.Dictionary<System.Type,System.Object>_defaultInstances  // static @ 0x8
  private           ParadoxNotion.Serialization.FullSerializer.fsMetaType.ObjectGeneratorgenerator  // 0x10
  private           System.Type                     <reflectedType>k__BackingField  // 0x18
  private           ParadoxNotion.Serialization.FullSerializer.fsMetaProperty[]<Properties>k__BackingField  // 0x20
  private           System.Boolean                  <DeserializeOverwriteRequest>k__BackingField  // 0x28
PROPERTIES:
  reflectedType  get=0x0385B100  set=0x05392C40
  Properties  get=0x04D862C0  set=0x05390F40
  DeserializeOverwriteRequest  get=0x011F33C0  set=0x011F33D0
METHODS:
  RVA=0x03604FD0  token=0x6001830  ParadoxNotion.Serialization.FullSerializer.fsMetaType Get(System.Type type)
  RVA=0x044D7AB0  token=0x6001831  System.Void FlushMem()
  RVA=0x036048E0  token=0x6001838  System.Void .ctor(System.Type reflectedType)
  RVA=0x0321E390  token=0x6001839  System.Void CollectProperties(System.Collections.Generic.List<ParadoxNotion.Serialization.FullSerializer.fsMetaProperty> properties, System.Type reflectedType)
  RVA=0x0321DA20  token=0x600183A  System.Boolean CanSerializeField(System.Reflection.FieldInfo field)
  RVA=0x036049F0  token=0x600183B  ParadoxNotion.Serialization.FullSerializer.fsMetaType.ObjectGenerator GetGenerator(System.Type reflectedType)
  RVA=0x03604220  token=0x600183C  System.Boolean HasDefaultConstructor(System.Type reflectedType)
  RVA=0x0A99D9DC  token=0x600183D  System.Object GetDefaultInstance()
  RVA=0x03604F50  token=0x600183E  System.Object CreateInstance()
  RVA=0x044D7BC0  token=0x600183F  System.Void .cctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsObjectProcessor
TYPE:  abstract class
TOKEN: 0x20004F4
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0A99DAD0  token=0x600184D  System.Boolean CanProcess(System.Type type)
  RVA=0x041E1670  token=0x600184E  System.Void OnBeforeSerialize(System.Type storageType, System.Object instance)
  RVA=0x041E1670  token=0x600184F  System.Void OnAfterSerialize(System.Type storageType, System.Object instance, ParadoxNotion.Serialization.FullSerializer.fsData& data)
  RVA=0x041E1670  token=0x6001850  System.Void OnBeforeDeserialize(System.Type storageType, ParadoxNotion.Serialization.FullSerializer.fsData& data)
  RVA=0x041E1670  token=0x6001851  System.Void OnBeforeDeserializeAfterInstanceCreation(System.Type storageType, System.Object instance, ParadoxNotion.Serialization.FullSerializer.fsData& data)
  RVA=0x041E1670  token=0x6001852  System.Void OnAfterDeserialize(System.Type storageType, System.Object instance)
  RVA=0x041E1670  token=0x6001853  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsResult
TYPE:  sealed struct
TOKEN: 0x20004F5
SIZE:  0x20
FIELDS:
  private   static readonly System.String[]                 EmptyStringArray  // static @ 0x0
  private           System.Boolean                  _success  // 0x10
  private           System.Collections.Generic.List<System.String>_messages  // 0x18
  public    static  ParadoxNotion.Serialization.FullSerializer.fsResultSuccess  // static @ 0x8
PROPERTIES:
  Failed  get=0x035291C0
  Succeeded  get=0x04D883E0
  HasWarnings  get=0x031146C0
  AsException  get=0x0A99DE34
  RawMessages  get=0x0A99E02C
  FormattedMessages  get=0x0A99DF4C
METHODS:
  RVA=0x0A99DD84  token=0x6001854  ParadoxNotion.Serialization.FullSerializer.fsResult Warn(System.String warning)
  RVA=0x0A99DC8C  token=0x6001855  ParadoxNotion.Serialization.FullSerializer.fsResult Fail(System.String warning)
  RVA=0x0A99DB1C  token=0x6001856  System.Void AddMessage(System.String message)
  RVA=0x0360F630  token=0x6001857  System.Void AddMessages(ParadoxNotion.Serialization.FullSerializer.fsResult result)
  RVA=0x0360EE80  token=0x6001858  ParadoxNotion.Serialization.FullSerializer.fsResult Merge(ParadoxNotion.Serialization.FullSerializer.fsResult other)
  RVA=0x0360EE10  token=0x6001859  ParadoxNotion.Serialization.FullSerializer.fsResult op_Addition(ParadoxNotion.Serialization.FullSerializer.fsResult a, ParadoxNotion.Serialization.FullSerializer.fsResult b)
  RVA=0x031145A0  token=0x600185D  ParadoxNotion.Serialization.FullSerializer.fsResult AssertSuccess()
  RVA=0x0A99DBCC  token=0x600185E  ParadoxNotion.Serialization.FullSerializer.fsResult AssertSuccessWithoutWarnings()
  RVA=0x0A99DD38  token=0x6001862  System.String ToString()
  RVA=0x04CECC30  token=0x6001863  System.Void .cctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.fsSerializer
TYPE:  class
TOKEN: 0x20004F6
SIZE:  0x80
FIELDS:
  public    static  System.String                   KEY_OBJECT_REFERENCE  // const
  public    static  System.String                   KEY_OBJECT_DEFINITION  // const
  public    static  System.String                   KEY_INSTANCE_TYPE  // const
  public    static  System.String                   KEY_VERSION  // const
  public    static  System.String                   KEY_CONTENT  // const
  private           System.Collections.Generic.Dictionary<System.Type,ParadoxNotion.Serialization.FullSerializer.fsBaseConverter>_cachedOverrideConverterInstances  // 0x10
  private           System.Collections.Generic.Dictionary<System.Type,ParadoxNotion.Serialization.FullSerializer.fsBaseConverter>_cachedConverters  // 0x18
  private   readonly System.Collections.Generic.List<ParadoxNotion.Serialization.FullSerializer.fsConverter>_availableConverters  // 0x20
  private   readonly System.Collections.Generic.Dictionary<System.Type,ParadoxNotion.Serialization.FullSerializer.fsDirectConverter>_availableDirectConverters  // 0x28
  private   readonly System.Collections.Generic.List<ParadoxNotion.Serialization.FullSerializer.fsObjectProcessor>_processors  // 0x30
  private           System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.List<ParadoxNotion.Serialization.FullSerializer.fsObjectProcessor>>_cachedProcessors  // 0x38
  private           ParadoxNotion.Serialization.FullSerializer.fsCyclicReferenceManager_references  // 0x40
  private           ParadoxNotion.Serialization.FullSerializer.fsSerializer.fsLazyCycleDefinitionWriter_lazyReferenceWriter  // 0x48
  private           System.Collections.Generic.Stack<ParadoxNotion.Serialization.FullSerializer.ISerializationCollector>_collectors  // 0x50
  private           System.Int32                    _collectableDepth  // 0x58
  private           System.Collections.Generic.List<UnityEngine.Object><ReferencesDatabase>k__BackingField  // 0x60
  private           System.Boolean                  <IgnoreSerializeCycleReferences>k__BackingField  // 0x68
  private           System.Action<System.Object>    onBeforeObjectSerialized  // 0x70
  private           System.Action<System.Object,ParadoxNotion.Serialization.FullSerializer.fsData>onAfterObjectSerialized  // 0x78
PROPERTIES:
  ReferencesDatabase  get=0x04D86280  set=0x063CC164
  IgnoreSerializeCycleReferences  get=0x04D86B40  set=0x04D86BA0
EVENTS:
  onBeforeObjectSerialized  add=add_onBeforeObjectSerialized  remove=remove_onBeforeObjectSerialized
  onAfterObjectSerialized  add=add_onAfterObjectSerialized  remove=remove_onAfterObjectSerialized
METHODS:
  RVA=0x03F86AB0  token=0x6001864  System.Boolean IsReservedKeyword(System.String key)
  RVA=0x0A99E678  token=0x6001865  System.Void RemoveMetaData(ParadoxNotion.Serialization.FullSerializer.fsData& data)
  RVA=0x0A99E084  token=0x6001866  System.Void EnsureDictionary(ParadoxNotion.Serialization.FullSerializer.fsData& data)
  RVA=0x0360ED50  token=0x6001867  System.Boolean IsObjectReference(ParadoxNotion.Serialization.FullSerializer.fsData data)
  RVA=0x0360E480  token=0x6001868  System.Boolean IsObjectDefinition(ParadoxNotion.Serialization.FullSerializer.fsData data)
  RVA=0x0A99E5FC  token=0x6001869  System.Boolean IsVersioned(ParadoxNotion.Serialization.FullSerializer.fsData data)
  RVA=0x0360E4F0  token=0x600186A  System.Boolean IsTypeSpecified(ParadoxNotion.Serialization.FullSerializer.fsData data)
  RVA=0x0360E560  token=0x600186B  System.Boolean IsWrappedData(ParadoxNotion.Serialization.FullSerializer.fsData data)
  RVA=0x0A99E514  token=0x600186C  System.Void Invoke_OnBeforeSerialize(System.Collections.Generic.List<ParadoxNotion.Serialization.FullSerializer.fsObjectProcessor> processors, System.Type storageType, System.Object instance)
  RVA=0x0A99E4A8  token=0x600186D  System.Void Invoke_OnAfterSerialize(System.Collections.Generic.List<ParadoxNotion.Serialization.FullSerializer.fsObjectProcessor> processors, System.Type storageType, System.Object instance, ParadoxNotion.Serialization.FullSerializer.fsData& data)
  RVA=0x0360F200  token=0x600186E  System.Void Invoke_OnBeforeDeserialize(System.Collections.Generic.List<ParadoxNotion.Serialization.FullSerializer.fsObjectProcessor> processors, System.Type storageType, ParadoxNotion.Serialization.FullSerializer.fsData& data)
  RVA=0x0360F1A0  token=0x600186F  System.Void Invoke_OnBeforeDeserializeAfterInstanceCreation(System.Collections.Generic.List<ParadoxNotion.Serialization.FullSerializer.fsObjectProcessor> processors, System.Type storageType, System.Object instance, ParadoxNotion.Serialization.FullSerializer.fsData& data)
  RVA=0x0360F2E0  token=0x6001870  System.Void Invoke_OnAfterDeserialize(System.Collections.Generic.List<ParadoxNotion.Serialization.FullSerializer.fsObjectProcessor> processors, System.Type storageType, System.Object instance)
  RVA=0x044D7CB0  token=0x6001879  System.Void .ctor()
  RVA=0x03114760  token=0x600187A  System.Void PurgeTemporaryData()
  RVA=0x0360EEE0  token=0x600187B  System.Collections.Generic.List<ParadoxNotion.Serialization.FullSerializer.fsObjectProcessor> GetProcessors(System.Type type)
  RVA=0x044D8400  token=0x600187C  System.Void AddConverter(ParadoxNotion.Serialization.FullSerializer.fsBaseConverter converter)
  RVA=0x0360EFF0  token=0x600187D  ParadoxNotion.Serialization.FullSerializer.fsBaseConverter GetConverter(System.Type type, System.Type overrideConverterType)
  RVA=0x0A99F00C  token=0x600187E  ParadoxNotion.Serialization.FullSerializer.fsResult TrySerialize(System.Type storageType, System.Object instance, ParadoxNotion.Serialization.FullSerializer.fsData& data)
  RVA=0x0A99F048  token=0x600187F  ParadoxNotion.Serialization.FullSerializer.fsResult TrySerialize(System.Type storageType, System.Object instance, ParadoxNotion.Serialization.FullSerializer.fsData& data, System.Type overrideConverterType)
  RVA=0x0A99E12C  token=0x6001880  ParadoxNotion.Serialization.FullSerializer.fsResult Internal_Serialize(System.Type storageType, System.Object instance, ParadoxNotion.Serialization.FullSerializer.fsData& data, System.Type overrideConverterType)
  RVA=0x05396A48  token=0x6001881  ParadoxNotion.Serialization.FullSerializer.fsResult TryDeserialize(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType, System.Object& result)
  RVA=0x0360E5D0  token=0x6001882  ParadoxNotion.Serialization.FullSerializer.fsResult TryDeserialize(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType, System.Object& result, System.Type overrideConverterType)
  RVA=0x0360E760  token=0x6001883  ParadoxNotion.Serialization.FullSerializer.fsResult Internal_Deserialize(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType, System.Object& result, System.Type overrideConverterType)
  RVA=0x0360F470  token=0x6001884  System.Void TryPush(System.Object o)
  RVA=0x0360F390  token=0x6001885  System.Void TryPop(System.Object o)
  RVA=0x0A99EEAC  token=0x6001886  System.Void TrySerializeVersioning(System.Object currentInstance, ParadoxNotion.Serialization.FullSerializer.fsData& data)
  RVA=0x0360F260  token=0x6001887  System.Void TryDeserializeVersioning(System.Object& currentInstance, ParadoxNotion.Serialization.FullSerializer.fsData& currentData)
  RVA=0x0A99E7E4  token=0x6001888  System.Void TryDeserializeMigration(System.Object& currentInstance, ParadoxNotion.Serialization.FullSerializer.fsData& currentData, System.Type previousType, System.Object previousInstance)
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.IMigratable
TYPE:  interface
TOKEN: 0x20004F9
FIELDS:
METHODS:
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.IMigratable`1
TYPE:  interface
TOKEN: 0x20004FA
IMPLEMENTS: ParadoxNotion.Serialization.FullSerializer.IMigratable
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6001890  System.Void Migrate(T model)
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.ISerializationCollector
TYPE:  interface
TOKEN: 0x20004FB
IMPLEMENTS: ParadoxNotion.Serialization.FullSerializer.ISerializationCollectable
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6001891  System.Void OnPush(ParadoxNotion.Serialization.FullSerializer.ISerializationCollector parent)
  RVA=-1  // abstract  token=0x6001892  System.Void OnCollect(ParadoxNotion.Serialization.FullSerializer.ISerializationCollectable child, System.Int32 depth)
  RVA=-1  // abstract  token=0x6001893  System.Void OnPop(ParadoxNotion.Serialization.FullSerializer.ISerializationCollector parent)
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.ISerializationCollectable
TYPE:  interface
TOKEN: 0x20004FC
FIELDS:
METHODS:
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.fsArrayConverter
TYPE:  class
TOKEN: 0x20004FD
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsConverter
FIELDS:
METHODS:
  RVA=0x04707080  token=0x6001894  System.Boolean CanProcess(System.Type type)
  RVA=0x012081B0  token=0x6001895  System.Boolean RequestCycleSupport(System.Type storageType)
  RVA=0x012081B0  token=0x6001896  System.Boolean RequestInheritanceSupport(System.Type storageType)
  RVA=0x0A9A5480  token=0x6001897  ParadoxNotion.Serialization.FullSerializer.fsResult TrySerialize(System.Object instance, ParadoxNotion.Serialization.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0A9A5218  token=0x6001898  ParadoxNotion.Serialization.FullSerializer.fsResult TryDeserialize(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=0x0A9A51C0  token=0x6001899  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x041E1670  token=0x600189A  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.fsDictionaryConverter
TYPE:  class
TOKEN: 0x20004FE
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsConverter
FIELDS:
METHODS:
  RVA=0x0454B410  token=0x600189B  System.Boolean CanProcess(System.Type type)
  RVA=0x048F6920  token=0x600189C  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x0A9A565C  token=0x600189D  ParadoxNotion.Serialization.FullSerializer.fsResult TrySerialize(System.Object instance_, ParadoxNotion.Serialization.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x03F857E0  token=0x600189E  ParadoxNotion.Serialization.FullSerializer.fsResult TryDeserialize(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Object& instance_, System.Type storageType)
  RVA=0x041E1670  token=0x600189F  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.fsEnumConverter
TYPE:  class
TOKEN: 0x20004FF
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsConverter
FIELDS:
METHODS:
  RVA=0x046929D0  token=0x60018A0  System.Boolean CanProcess(System.Type type)
  RVA=0x012081B0  token=0x60018A1  System.Boolean RequestCycleSupport(System.Type storageType)
  RVA=0x012081B0  token=0x60018A2  System.Boolean RequestInheritanceSupport(System.Type storageType)
  RVA=0x04598ED0  token=0x60018A3  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x0A9A5BC4  token=0x60018A4  ParadoxNotion.Serialization.FullSerializer.fsResult TrySerialize(System.Object instance, ParadoxNotion.Serialization.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x04250D30  token=0x60018A5  ParadoxNotion.Serialization.FullSerializer.fsResult TryDeserialize(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=-1  // generic def  token=0x60018A6  System.Boolean ArrayContains(T[] values, T value)
  RVA=0x041E1670  token=0x60018A7  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.fsForwardConverter
TYPE:  class
TOKEN: 0x2000500
SIZE:  0x20
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsConverter
FIELDS:
  private           System.String                   _memberName  // 0x18
METHODS:
  RVA=0x0A9A6604  token=0x60018A8  System.Void .ctor(ParadoxNotion.Serialization.FullSerializer.fsForwardAttribute attribute)
  RVA=0x0A9A6084  token=0x60018A9  System.Boolean CanProcess(System.Type type)
  RVA=0x0A9A6138  token=0x60018AA  ParadoxNotion.Serialization.FullSerializer.fsResult GetProperty(System.Object instance, ParadoxNotion.Serialization.FullSerializer.fsMetaProperty& property)
  RVA=0x0A9A6498  token=0x60018AB  ParadoxNotion.Serialization.FullSerializer.fsResult TrySerialize(System.Object instance, ParadoxNotion.Serialization.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0A9A630C  token=0x60018AC  ParadoxNotion.Serialization.FullSerializer.fsResult TryDeserialize(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=0x0A9A60E0  token=0x60018AD  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.fsListConverter
TYPE:  class
TOKEN: 0x2000501
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsConverter
FIELDS:
METHODS:
  RVA=0x04550680  token=0x60018AE  System.Boolean CanProcess(System.Type type)
  RVA=0x03604EB0  token=0x60018AF  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x0A9A6920  token=0x60018B0  ParadoxNotion.Serialization.FullSerializer.fsResult TrySerialize(System.Object instance_, ParadoxNotion.Serialization.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x03F86750  token=0x60018B1  ParadoxNotion.Serialization.FullSerializer.fsResult TryDeserialize(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Object& instance_, System.Type storageType)
  RVA=0x041E1670  token=0x60018B2  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.fsPrimitiveConverter
TYPE:  class
TOKEN: 0x2000502
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsConverter
FIELDS:
METHODS:
  RVA=0x04450B30  token=0x60018B3  System.Boolean CanProcess(System.Type type)
  RVA=0x012081B0  token=0x60018B4  System.Boolean RequestCycleSupport(System.Type storageType)
  RVA=0x012081B0  token=0x60018B5  System.Boolean RequestInheritanceSupport(System.Type storageType)
  RVA=0x0360FAE0  token=0x60018B6  System.Boolean UseBool(System.Type type)
  RVA=0x0360FDF0  token=0x60018B7  System.Boolean UseInt64(System.Type type)
  RVA=0x0360FB30  token=0x60018B8  System.Boolean UseDouble(System.Type type)
  RVA=0x0420D110  token=0x60018B9  System.Boolean UseString(System.Type type)
  RVA=0x0A9A6B9C  token=0x60018BA  ParadoxNotion.Serialization.FullSerializer.fsResult TrySerialize(System.Object instance, ParadoxNotion.Serialization.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0360F6A0  token=0x60018BB  ParadoxNotion.Serialization.FullSerializer.fsResult TryDeserialize(ParadoxNotion.Serialization.FullSerializer.fsData storage, System.Object& instance, System.Type storageType)
  RVA=0x041E1670  token=0x60018BC  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.fsReflectedConverter
TYPE:  class
TOKEN: 0x2000503
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsConverter
FIELDS:
METHODS:
  RVA=0x044E5BC0  token=0x60018BD  System.Boolean CanProcess(System.Type type)
  RVA=0x0A9A7150  token=0x60018BE  ParadoxNotion.Serialization.FullSerializer.fsResult TrySerialize(System.Object instance, ParadoxNotion.Serialization.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0360DA00  token=0x60018BF  ParadoxNotion.Serialization.FullSerializer.fsResult TryDeserialize(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=0x03604F00  token=0x60018C0  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x041E1670  token=0x60018C1  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.fsTypeConverter
TYPE:  class
TOKEN: 0x2000504
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsConverter
FIELDS:
METHODS:
  RVA=0x04510A20  token=0x60018C2  System.Boolean CanProcess(System.Type type)
  RVA=0x012081B0  token=0x60018C3  System.Boolean RequestCycleSupport(System.Type type)
  RVA=0x012081B0  token=0x60018C4  System.Boolean RequestInheritanceSupport(System.Type type)
  RVA=0x0A9A75D8  token=0x60018C5  ParadoxNotion.Serialization.FullSerializer.fsResult TrySerialize(System.Object instance, ParadoxNotion.Serialization.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0A9A7490  token=0x60018C6  ParadoxNotion.Serialization.FullSerializer.fsResult TryDeserialize(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=0x04D8C7E0  token=0x60018C7  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x041E1670  token=0x60018C8  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.DirectConverters.AnimationCurve_DirectConverter
TYPE:  class
TOKEN: 0x2000505
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsDirectConverter`1
FIELDS:
METHODS:
  RVA=0x0A99F990  token=0x60018C9  ParadoxNotion.Serialization.FullSerializer.fsResult DoSerialize(UnityEngine.AnimationCurve model, System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> serialized)
  RVA=0x0A99F758  token=0x60018CA  ParadoxNotion.Serialization.FullSerializer.fsResult DoDeserialize(System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> data, UnityEngine.AnimationCurve& model)
  RVA=0x0A99F708  token=0x60018CB  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x044D8540  token=0x60018CC  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.DirectConverters.Bounds_DirectConverter
TYPE:  class
TOKEN: 0x2000506
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsDirectConverter`1
FIELDS:
METHODS:
  RVA=0x0A99FD5C  token=0x60018CD  ParadoxNotion.Serialization.FullSerializer.fsResult DoSerialize(UnityEngine.Bounds model, System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> serialized)
  RVA=0x0A99FBB8  token=0x60018CE  ParadoxNotion.Serialization.FullSerializer.fsResult DoDeserialize(System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> data, UnityEngine.Bounds& model)
  RVA=0x0A99FB64  token=0x60018CF  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x044D8570  token=0x60018D0  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.DirectConverters.Gradient_DirectConverter
TYPE:  class
TOKEN: 0x2000507
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsDirectConverter`1
FIELDS:
METHODS:
  RVA=0x0A9A2F4C  token=0x60018D1  ParadoxNotion.Serialization.FullSerializer.fsResult DoSerialize(UnityEngine.Gradient model, System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> serialized)
  RVA=0x0A9A2D84  token=0x60018D2  ParadoxNotion.Serialization.FullSerializer.fsResult DoDeserialize(System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> data, UnityEngine.Gradient& model)
  RVA=0x0A9A2D34  token=0x60018D3  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x044D8600  token=0x60018D4  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.DirectConverters.GUIStyleState_DirectConverter
TYPE:  class
TOKEN: 0x2000508
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsDirectConverter`1
FIELDS:
METHODS:
  RVA=0x0A9A11B4  token=0x60018D5  ParadoxNotion.Serialization.FullSerializer.fsResult DoSerialize(UnityEngine.GUIStyleState model, System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> serialized)
  RVA=0x0A9A0FD4  token=0x60018D6  ParadoxNotion.Serialization.FullSerializer.fsResult DoDeserialize(System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> data, UnityEngine.GUIStyleState& model)
  RVA=0x0A9A0F84  token=0x60018D7  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x044D85A0  token=0x60018D8  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.DirectConverters.GUIStyle_DirectConverter
TYPE:  class
TOKEN: 0x2000509
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsDirectConverter`1
FIELDS:
METHODS:
  RVA=0x0A9A2270  token=0x60018D9  ParadoxNotion.Serialization.FullSerializer.fsResult DoSerialize(UnityEngine.GUIStyle model, System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> serialized)
  RVA=0x0A9A13A0  token=0x60018DA  ParadoxNotion.Serialization.FullSerializer.fsResult DoDeserialize(System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> data, UnityEngine.GUIStyle& model)
  RVA=0x0A9A1350  token=0x60018DB  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x044D85D0  token=0x60018DC  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.DirectConverters.Keyframe_DirectConverter
TYPE:  class
TOKEN: 0x200050A
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsDirectConverter`1
FIELDS:
METHODS:
  RVA=0x0A9A3504  token=0x60018DD  ParadoxNotion.Serialization.FullSerializer.fsResult DoSerialize(UnityEngine.Keyframe model, System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> serialized)
  RVA=0x0A9A326C  token=0x60018DE  ParadoxNotion.Serialization.FullSerializer.fsResult DoDeserialize(System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> data, UnityEngine.Keyframe& model)
  RVA=0x0A9A3214  token=0x60018DF  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x044D8630  token=0x60018E0  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.DirectConverters.LayerMask_DirectConverter
TYPE:  class
TOKEN: 0x200050B
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsDirectConverter`1
FIELDS:
METHODS:
  RVA=0x0A9A389C  token=0x60018E1  ParadoxNotion.Serialization.FullSerializer.fsResult DoSerialize(UnityEngine.LayerMask model, System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> serialized)
  RVA=0x0A9A3798  token=0x60018E2  ParadoxNotion.Serialization.FullSerializer.fsResult DoDeserialize(System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> data, UnityEngine.LayerMask& model)
  RVA=0x0A9A3758  token=0x60018E3  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x044D8660  token=0x60018E4  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.DirectConverters.RectOffset_DirectConverter
TYPE:  class
TOKEN: 0x200050C
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsDirectConverter`1
FIELDS:
METHODS:
  RVA=0x0A9A3E00  token=0x60018E5  ParadoxNotion.Serialization.FullSerializer.fsResult DoSerialize(UnityEngine.RectOffset model, System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> serialized)
  RVA=0x0A9A3B64  token=0x60018E6  ParadoxNotion.Serialization.FullSerializer.fsResult DoDeserialize(System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> data, UnityEngine.RectOffset& model)
  RVA=0x0A9A3B14  token=0x60018E7  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x044D8690  token=0x60018E8  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.DirectConverters.Rect_DirectConverter
TYPE:  class
TOKEN: 0x200050D
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsDirectConverter`1
FIELDS:
METHODS:
  RVA=0x0A9A42AC  token=0x60018E9  ParadoxNotion.Serialization.FullSerializer.fsResult DoSerialize(UnityEngine.Rect model, System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> serialized)
  RVA=0x0A9A4050  token=0x60018EA  ParadoxNotion.Serialization.FullSerializer.fsResult DoDeserialize(System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> data, UnityEngine.Rect& model)
  RVA=0x0A9A4010  token=0x60018EB  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x044D86C0  token=0x60018EC  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.DirectConverters.Vector2Int_DirectConverter
TYPE:  class
TOKEN: 0x200050E
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsDirectConverter`1
FIELDS:
METHODS:
  RVA=0x0A9A4868  token=0x60018ED  ParadoxNotion.Serialization.FullSerializer.fsResult DoSerialize(UnityEngine.Vector2Int model, System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> serialized)
  RVA=0x0A9A4764  token=0x60018EE  ParadoxNotion.Serialization.FullSerializer.fsResult DoDeserialize(System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> data, UnityEngine.Vector2Int& model)
  RVA=0x0A9A4724  token=0x60018EF  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x044D86F0  token=0x60018F0  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.DirectConverters.Vector2_DirectConverter
TYPE:  class
TOKEN: 0x200050F
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsDirectConverter`1
FIELDS:
METHODS:
  RVA=0x0A9A4AAC  token=0x60018F1  ParadoxNotion.Serialization.FullSerializer.fsResult DoSerialize(UnityEngine.Vector2 model, System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> serialized)
  RVA=0x0A9A4998  token=0x60018F2  ParadoxNotion.Serialization.FullSerializer.fsResult DoDeserialize(System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> data, UnityEngine.Vector2& model)
  RVA=0x0A9A494C  token=0x60018F3  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x0A9A4B90  token=0x60018F4  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Serialization.FullSerializer.Internal.DirectConverters.Vector3Int_DirectConverter
TYPE:  class
TOKEN: 0x2000510
SIZE:  0x18
EXTENDS: ParadoxNotion.Serialization.FullSerializer.fsDirectConverter`1
FIELDS:
METHODS:
  RVA=0x0A9A4D40  token=0x60018F5  ParadoxNotion.Serialization.FullSerializer.fsResult DoSerialize(UnityEngine.Vector3Int model, System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> serialized)
  RVA=0x0A9A4C08  token=0x60018F6  ParadoxNotion.Serialization.FullSerializer.fsResult DoDeserialize(System.Collections.Generic.Dictionary<System.String,ParadoxNotion.Serialization.FullSerializer.fsData> data, UnityEngine.Vector3Int& model)
  RVA=0x0A9A4BB8  token=0x60018F7  System.Object CreateInstance(ParadoxNotion.Serialization.FullSerializer.fsData data, System.Type storageType)
  RVA=0x044D8720  token=0x60018F8  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Services.EventRouter
TYPE:  class
TOKEN: 0x20004AE
SIZE:  0x180
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IPointerEnterHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IPointerExitHandler UnityEngine.EventSystems.IPointerDownHandler UnityEngine.EventSystems.IPointerUpHandler UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IScrollHandler UnityEngine.EventSystems.IUpdateSelectedHandler UnityEngine.EventSystems.ISelectHandler UnityEngine.EventSystems.IDeselectHandler UnityEngine.EventSystems.IMoveHandler UnityEngine.EventSystems.ISubmitHandler UnityEngine.EventSystems.IDropHandler
FIELDS:
  private           ParadoxNotion.Services.EventRouterAnimatorMove_routerAnimatorMove  // 0x18
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.EventSystems.PointerEventData>onPointerEnter  // 0x20
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.EventSystems.PointerEventData>onPointerExit  // 0x28
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.EventSystems.PointerEventData>onPointerDown  // 0x30
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.EventSystems.PointerEventData>onPointerUp  // 0x38
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.EventSystems.PointerEventData>onPointerClick  // 0x40
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.EventSystems.PointerEventData>onDrag  // 0x48
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.EventSystems.PointerEventData>onDrop  // 0x50
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.EventSystems.PointerEventData>onScroll  // 0x58
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.EventSystems.BaseEventData>onUpdateSelected  // 0x60
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.EventSystems.BaseEventData>onSelect  // 0x68
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.EventSystems.BaseEventData>onDeselect  // 0x70
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.EventSystems.AxisEventData>onMove  // 0x78
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.EventSystems.BaseEventData>onSubmit  // 0x80
  private           ParadoxNotion.Services.EventRouter.EventDelegateonMouseDown  // 0x88
  private           ParadoxNotion.Services.EventRouter.EventDelegateonMouseDrag  // 0x90
  private           ParadoxNotion.Services.EventRouter.EventDelegateonMouseEnter  // 0x98
  private           ParadoxNotion.Services.EventRouter.EventDelegateonMouseExit  // 0xa0
  private           ParadoxNotion.Services.EventRouter.EventDelegateonMouseOver  // 0xa8
  private           ParadoxNotion.Services.EventRouter.EventDelegateonMouseUp  // 0xb0
  private           ParadoxNotion.Services.EventRouter.EventDelegateonEnable  // 0xb8
  private           ParadoxNotion.Services.EventRouter.EventDelegateonDisable  // 0xc0
  private           ParadoxNotion.Services.EventRouter.EventDelegateonDestroy  // 0xc8
  private           ParadoxNotion.Services.EventRouter.EventDelegateonTransformChildrenChanged  // 0xd0
  private           ParadoxNotion.Services.EventRouter.EventDelegateonTransformParentChanged  // 0xd8
  private           ParadoxNotion.Services.EventRouter.EventDelegate<System.Int32>onAnimatorIK  // 0xe0
  private           ParadoxNotion.Services.EventRouter.EventDelegateonBecameInvisible  // 0xe8
  private           ParadoxNotion.Services.EventRouter.EventDelegateonBecameVisible  // 0xf0
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.ControllerColliderHit>onControllerColliderHit  // 0xf8
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.GameObject>onParticleCollision  // 0x100
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.Collision>onCollisionEnter  // 0x108
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.Collision>onCollisionExit  // 0x110
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.Collision>onCollisionStay  // 0x118
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.Collision2D>onCollisionEnter2D  // 0x120
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.Collision2D>onCollisionExit2D  // 0x128
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.Collision2D>onCollisionStay2D  // 0x130
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.Collider>onTriggerEnter  // 0x138
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.Collider>onTriggerExit  // 0x140
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.Collider>onTriggerStay  // 0x148
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.Collider2D>onTriggerEnter2D  // 0x150
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.Collider2D>onTriggerExit2D  // 0x158
  private           ParadoxNotion.Services.EventRouter.EventDelegate<UnityEngine.Collider2D>onTriggerStay2D  // 0x160
  private           System.Action<UnityEngine.RenderTexture,UnityEngine.RenderTexture>onRenderImage  // 0x168
  private           ParadoxNotion.Services.EventRouter.EventDelegateonDrawGizmos  // 0x170
  private           ParadoxNotion.Services.EventRouter.CustomEventDelegateonCustomEvent  // 0x178
EVENTS:
  onPointerEnter  add=add_onPointerEnter  remove=remove_onPointerEnter
  onPointerExit  add=add_onPointerExit  remove=remove_onPointerExit
  onPointerDown  add=add_onPointerDown  remove=remove_onPointerDown
  onPointerUp  add=add_onPointerUp  remove=remove_onPointerUp
  onPointerClick  add=add_onPointerClick  remove=remove_onPointerClick
  onDrag  add=add_onDrag  remove=remove_onDrag
  onDrop  add=add_onDrop  remove=remove_onDrop
  onScroll  add=add_onScroll  remove=remove_onScroll
  onUpdateSelected  add=add_onUpdateSelected  remove=remove_onUpdateSelected
  onSelect  add=add_onSelect  remove=remove_onSelect
  onDeselect  add=add_onDeselect  remove=remove_onDeselect
  onMove  add=add_onMove  remove=remove_onMove
  onSubmit  add=add_onSubmit  remove=remove_onSubmit
  onMouseDown  add=add_onMouseDown  remove=remove_onMouseDown
  onMouseDrag  add=add_onMouseDrag  remove=remove_onMouseDrag
  onMouseEnter  add=add_onMouseEnter  remove=remove_onMouseEnter
  onMouseExit  add=add_onMouseExit  remove=remove_onMouseExit
  onMouseOver  add=add_onMouseOver  remove=remove_onMouseOver
  onMouseUp  add=add_onMouseUp  remove=remove_onMouseUp
  onEnable  add=add_onEnable  remove=remove_onEnable
  onDisable  add=add_onDisable  remove=remove_onDisable
  onDestroy  add=add_onDestroy  remove=remove_onDestroy
  onTransformChildrenChanged  add=add_onTransformChildrenChanged  remove=remove_onTransformChildrenChanged
  onTransformParentChanged  add=add_onTransformParentChanged  remove=remove_onTransformParentChanged
  onAnimatorIK  add=add_onAnimatorIK  remove=remove_onAnimatorIK
  onAnimatorMove  add=add_onAnimatorMove  remove=remove_onAnimatorMove
  onBecameInvisible  add=add_onBecameInvisible  remove=remove_onBecameInvisible
  onBecameVisible  add=add_onBecameVisible  remove=remove_onBecameVisible
  onControllerColliderHit  add=add_onControllerColliderHit  remove=remove_onControllerColliderHit
  onParticleCollision  add=add_onParticleCollision  remove=remove_onParticleCollision
  onCollisionEnter  add=add_onCollisionEnter  remove=remove_onCollisionEnter
  onCollisionExit  add=add_onCollisionExit  remove=remove_onCollisionExit
  onCollisionStay  add=add_onCollisionStay  remove=remove_onCollisionStay
  onCollisionEnter2D  add=add_onCollisionEnter2D  remove=remove_onCollisionEnter2D
  onCollisionExit2D  add=add_onCollisionExit2D  remove=remove_onCollisionExit2D
  onCollisionStay2D  add=add_onCollisionStay2D  remove=remove_onCollisionStay2D
  onTriggerEnter  add=add_onTriggerEnter  remove=remove_onTriggerEnter
  onTriggerExit  add=add_onTriggerExit  remove=remove_onTriggerExit
  onTriggerStay  add=add_onTriggerStay  remove=remove_onTriggerStay
  onTriggerEnter2D  add=add_onTriggerEnter2D  remove=remove_onTriggerEnter2D
  onTriggerExit2D  add=add_onTriggerExit2D  remove=remove_onTriggerExit2D
  onTriggerStay2D  add=add_onTriggerStay2D  remove=remove_onTriggerStay2D
  onRenderImage  add=add_onRenderImage  remove=remove_onRenderImage
  onDrawGizmos  add=add_onDrawGizmos  remove=remove_onDrawGizmos
  onCustomEvent  add=add_onCustomEvent  remove=remove_onCustomEvent
METHODS:
  RVA=0x0A990204  token=0x6001668  System.Void UnityEngine.EventSystems.IPointerEnterHandler.OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0A9902B8  token=0x6001669  System.Void UnityEngine.EventSystems.IPointerExitHandler.OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0A990150  token=0x600166A  System.Void UnityEngine.EventSystems.IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0A99036C  token=0x600166B  System.Void UnityEngine.EventSystems.IPointerUpHandler.OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0A99009C  token=0x600166C  System.Void UnityEngine.EventSystems.IPointerClickHandler.OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0A98FE7C  token=0x600166D  System.Void UnityEngine.EventSystems.IDragHandler.OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0A98FF30  token=0x600166E  System.Void UnityEngine.EventSystems.IDropHandler.OnDrop(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0A990420  token=0x600166F  System.Void UnityEngine.EventSystems.IScrollHandler.OnScroll(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0A990644  token=0x6001670  System.Void UnityEngine.EventSystems.IUpdateSelectedHandler.OnUpdateSelected(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0A9904D4  token=0x6001671  System.Void UnityEngine.EventSystems.ISelectHandler.OnSelect(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0A98FDC8  token=0x6001672  System.Void UnityEngine.EventSystems.IDeselectHandler.OnDeselect(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0A98FFE4  token=0x6001673  System.Void UnityEngine.EventSystems.IMoveHandler.OnMove(UnityEngine.EventSystems.AxisEventData eventData)
  RVA=0x0A990588  token=0x6001674  System.Void UnityEngine.EventSystems.ISubmitHandler.OnSubmit(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0A98F484  token=0x6001681  System.Void OnMouseDown()
  RVA=0x0A98F504  token=0x6001682  System.Void OnMouseDrag()
  RVA=0x0A98F584  token=0x6001683  System.Void OnMouseEnter()
  RVA=0x0A98F604  token=0x6001684  System.Void OnMouseExit()
  RVA=0x0A98F684  token=0x6001685  System.Void OnMouseOver()
  RVA=0x0A98F704  token=0x6001686  System.Void OnMouseUp()
  RVA=0x0A98F404  token=0x600168D  System.Void OnEnable()
  RVA=0x0A98F304  token=0x600168E  System.Void OnDisable()
  RVA=0x0A98F284  token=0x600168F  System.Void OnDestroy()
  RVA=0x0A98F860  token=0x6001694  System.Void OnTransformChildrenChanged()
  RVA=0x0A98F8E0  token=0x6001695  System.Void OnTransformParentChanged()
  RVA=0x0A98EBA0  token=0x600169A  System.Void OnAnimatorIK(System.Int32 layerIndex)
  RVA=0x0A98EC5C  token=0x600169F  System.Void OnBecameInvisible()
  RVA=0x0A98ECDC  token=0x60016A0  System.Void OnBecameVisible()
  RVA=0x0A98F1C4  token=0x60016A5  System.Void OnControllerColliderHit(UnityEngine.ControllerColliderHit hit)
  RVA=0x0A98F784  token=0x60016A6  System.Void OnParticleCollision(UnityEngine.GameObject other)
  RVA=0x0A98EE18  token=0x60016AD  System.Void OnCollisionEnter(UnityEngine.Collision collisionInfo)
  RVA=0x0A98EF90  token=0x60016AE  System.Void OnCollisionExit(UnityEngine.Collision collisionInfo)
  RVA=0x0A98F108  token=0x60016AF  System.Void OnCollisionStay(UnityEngine.Collision collisionInfo)
  RVA=0x0A98ED5C  token=0x60016B6  System.Void OnCollisionEnter2D(UnityEngine.Collision2D collisionInfo)
  RVA=0x0A98EED4  token=0x60016B7  System.Void OnCollisionExit2D(UnityEngine.Collision2D collisionInfo)
  RVA=0x0A98F04C  token=0x60016B8  System.Void OnCollisionStay2D(UnityEngine.Collision2D collisionInfo)
  RVA=0x0A98FA1C  token=0x60016BF  System.Void OnTriggerEnter(UnityEngine.Collider other)
  RVA=0x0A98FB94  token=0x60016C0  System.Void OnTriggerExit(UnityEngine.Collider other)
  RVA=0x0A98FD0C  token=0x60016C1  System.Void OnTriggerStay(UnityEngine.Collider other)
  RVA=0x0A98F960  token=0x60016C8  System.Void OnTriggerEnter2D(UnityEngine.Collider2D other)
  RVA=0x0A98FAD8  token=0x60016C9  System.Void OnTriggerExit2D(UnityEngine.Collider2D other)
  RVA=0x0A98FC50  token=0x60016CA  System.Void OnTriggerStay2D(UnityEngine.Collider2D other)
  RVA=0x0A98F844  token=0x60016CD  System.Void OnRenderImage(UnityEngine.RenderTexture source, UnityEngine.RenderTexture destination)
  RVA=0x0A98F384  token=0x60016D0  System.Void OnDrawGizmos()
  RVA=0x0A98EABC  token=0x60016D3  System.Void InvokeCustomEvent(System.String name, System.Object value, System.Object sender)
  RVA=-1  // generic def  token=0x60016D4  System.Void InvokeCustomEvent(System.String name, T value, System.Object sender)
  RVA=0x05393520  token=0x60016D5  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Services.EventRouterAnimatorMove
TYPE:  class
TOKEN: 0x20004B2
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           ParadoxNotion.Services.EventRouter.EventDelegateonAnimatorMove  // 0x18
EVENTS:
  onAnimatorMove  add=add_onAnimatorMove  remove=remove_onAnimatorMove
METHODS:
  RVA=0x0A98E918  token=0x60016E4  System.Void OnAnimatorMove()
  RVA=0x05393520  token=0x60016E5  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Services.Logger
TYPE:  static class
TOKEN: 0x20004B3
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.List<ParadoxNotion.Services.Logger.LogHandler>subscribers  // static @ 0x0
METHODS:
  RVA=0x0A994D1C  token=0x60016E6  System.Void AddListener(ParadoxNotion.Services.Logger.LogHandler callback)
  RVA=0x0A995668  token=0x60016E7  System.Void RemoveListener(ParadoxNotion.Services.Logger.LogHandler callback)
  RVA=0x0A9955FC  token=0x60016E8  System.Void Log(System.Object message, System.String tag, System.Object context)
  RVA=0x0A995590  token=0x60016E9  System.Void LogWarning(System.Object message, System.String tag, System.Object context)
  RVA=0x0A9954B8  token=0x60016EA  System.Void LogError(System.Object message, System.String tag, System.Object context)
  RVA=0x0A995524  token=0x60016EB  System.Void LogException(System.Exception exception, System.String tag, System.Object context)
  RVA=0x0A994EB8  token=0x60016EC  System.Void Internal_Log(UnityEngine.LogType type, System.Object message, System.String tag, System.Object context)
  RVA=0x0A994D8C  token=0x60016ED  System.Void ForwardToUnity(UnityEngine.LogType type, System.Object message, System.String tag, System.Object context)
  RVA=0x0A9956D8  token=0x60016EE  System.Void .cctor()
END_CLASS

CLASS: ParadoxNotion.Services.MonoManager
TYPE:  class
TOKEN: 0x20004B6
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Action                   onUpdate  // 0x18
  private           System.Action                   onLateUpdate  // 0x20
  private           System.Action                   onFixedUpdate  // 0x28
  private           System.Action                   onApplicationQuit  // 0x30
  private           System.Action<System.Boolean>   onApplicationPause  // 0x38
  private           System.Action                   onGUI  // 0x40
  private   static  System.Boolean                  isQuiting  // static @ 0x0
  private   static  ParadoxNotion.Services.MonoManager_current  // static @ 0x8
PROPERTIES:
  current  get=0x0A995E28
EVENTS:
  onUpdate  add=add_onUpdate  remove=remove_onUpdate
  onLateUpdate  add=add_onLateUpdate  remove=remove_onLateUpdate
  onFixedUpdate  add=add_onFixedUpdate  remove=remove_onFixedUpdate
  onApplicationQuit  add=add_onApplicationQuit  remove=remove_onApplicationQuit
  onApplicationPause  add=add_onApplicationPause  remove=remove_onApplicationPause
  onGUI  add=add_onGUI  remove=remove_onGUI
METHODS:
  RVA=0x04D7A090  token=0x6001703  System.Void Purge()
  RVA=0x0A995998  token=0x6001704  System.Void Create()
  RVA=0x0A995844  token=0x6001705  System.Void AddUpdateCall(ParadoxNotion.Services.MonoManager.UpdateMode mode, System.Action call)
  RVA=0x0A995A74  token=0x6001706  System.Void RemoveUpdateCall(ParadoxNotion.Services.MonoManager.UpdateMode mode, System.Action call)
  RVA=0x0A995888  token=0x6001707  System.Void Awake()
  RVA=0x0A995A28  token=0x6001708  System.Void OnApplicationQuit()
  RVA=0x0A995A14  token=0x6001709  System.Void OnApplicationPause(System.Boolean isPause)
  RVA=0x03EFA060  token=0x600170A  System.Void Update()
  RVA=0x0A995A00  token=0x600170B  System.Void LateUpdate()
  RVA=0x0A9959EC  token=0x600170C  System.Void FixedUpdate()
  RVA=0x05393520  token=0x600170D  System.Void .ctor()
END_CLASS

CLASS: ParadoxNotion.Services.Threader
TYPE:  static class
TOKEN: 0x20004B8
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  <applicationIsPlaying>k__BackingField  // static @ 0x0
PROPERTIES:
  applicationIsPlaying  get=0x0A999C28  set=0x0A999CA4
  isMainThread  get=0x0A999C78
METHODS:
  RVA=0x04D577F0  token=0x600170E  System.Void .cctor()
  RVA=0x0A999A30  token=0x6001712  System.Void ManuallySyncIsPlaying()
  RVA=0x0A999A80  token=0x6001713  System.Threading.Thread StartAction(System.Threading.Thread thread, System.Action function, System.Action callback)
  RVA=-1  // generic def  token=0x6001714  System.Threading.Thread StartAction(System.Threading.Thread thread, System.Action<T1> function, T1 parameter1, System.Action callback)
  RVA=-1  // generic def  token=0x6001715  System.Threading.Thread StartAction(System.Threading.Thread thread, System.Action<T1,T2> function, T1 parameter1, T2 parameter2, System.Action callback)
  RVA=-1  // generic def  token=0x6001716  System.Threading.Thread StartAction(System.Threading.Thread thread, System.Action<T1,T2,T3> function, T1 parameter1, T2 parameter2, T3 parameter3, System.Action callback)
  RVA=-1  // generic def  token=0x6001717  System.Threading.Thread StartFunction(System.Threading.Thread thread, System.Func<TResult> function, System.Action<TResult> callback)
  RVA=-1  // generic def  token=0x6001718  System.Threading.Thread StartFunction(System.Threading.Thread thread, System.Func<T1,TResult> function, T1 parameter1, System.Action<TResult> callback)
  RVA=-1  // generic def  token=0x6001719  System.Threading.Thread StartFunction(System.Threading.Thread thread, System.Func<T1,T2,TResult> function, T1 parameter1, T2 parameter2, System.Action<TResult> callback)
  RVA=-1  // generic def  token=0x600171A  System.Threading.Thread StartFunction(System.Threading.Thread thread, System.Func<T1,T2,T3,TResult> function, T1 parameter1, T2 parameter2, T3 parameter3, System.Action<TResult> callback)
  RVA=0x0A9999A4  token=0x600171B  System.Void Begin(System.Threading.Thread thread, System.Action callback)
  RVA=0x0A999BB4  token=0x600171C  System.Collections.IEnumerator ThreadMonitor(System.Threading.Thread thread, System.Action callback)
END_CLASS

