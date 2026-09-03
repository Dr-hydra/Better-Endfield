// ========================================================
// Dumped by @desirepro
// Assembly: Unity.VisualScripting.Flow.dll
// Classes:  54
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x200000A  // size: 0x30
    public class DebugData : Unity.VisualScripting.UnitConnectionDebugData
    {
        // Fields
        private System.Object <lastValue>k__BackingField;  // 0x20
        private System.Boolean <assignedLastValue>k__BackingField;  // 0x28

        // Properties
        System.Object lastValue { set; /* RVA: 0x05390F40 */ }
        System.Boolean assignedLastValue { set; /* RVA: 0x011F33D0 */ }

    }

    // TypeToken: 0x2000011  // size: 0x20
    public sealed struct RecursionNode : System.IEquatable`1
    {
        // Fields
        private readonly Unity.VisualScripting.IUnitPort <port>k__BackingField;  // 0x10
        private readonly Unity.VisualScripting.IGraphParent <context>k__BackingField;  // 0x18

        // Properties
        Unity.VisualScripting.IUnitPort port { get; /* RVA: 0x02B76770 */ }
        Unity.VisualScripting.IGraphParent context { get; /* RVA: 0x04D88320 */ }

        // Methods
        // RVA: 0x0B386CF8  token: 0x6000056
        public System.Void .ctor(Unity.VisualScripting.IUnitPort port, Unity.VisualScripting.GraphPointer pointer) { }
        // RVA: 0x0B386CA0  token: 0x6000057
        public virtual System.Boolean Equals(Unity.VisualScripting.Flow.RecursionNode other) { }
        // RVA: 0x0B386C30  token: 0x6000058
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B386CB8  token: 0x6000059
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000012  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.Flow.<>c <>9;  // static @ 0x0
        public static System.Func<Unity.VisualScripting.Flow> <>9__35_0;  // static @ 0x8

        // Methods
        // RVA: 0x0B386F0C  token: 0x600005A
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600005B
        public System.Void .ctor() { }
        // RVA: 0x0B386D34  token: 0x600005C
        private Unity.VisualScripting.Flow <New>b__35_0() { }

    }

    // TypeToken: 0x200001A  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.ControlInput.<>c <>9;  // static @ 0x0
        public static System.Func<Unity.VisualScripting.ControlConnection,Unity.VisualScripting.ControlOutput> <>9__14_0;  // static @ 0x8
        public static System.Func<Unity.VisualScripting.InvalidConnection,Unity.VisualScripting.IUnitOutputPort> <>9__16_0;  // static @ 0x10

        // Methods
        // RVA: 0x0B386EA8  token: 0x6000081
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000082
        public System.Void .ctor() { }
        // RVA: 0x0B386DC4  token: 0x6000083
        private Unity.VisualScripting.ControlOutput <get_validConnectedPorts>b__14_0(Unity.VisualScripting.ControlConnection c) { }
        // RVA: 0x0B386D84  token: 0x6000084
        private Unity.VisualScripting.IUnitOutputPort <get_invalidConnectedPorts>b__16_0(Unity.VisualScripting.InvalidConnection c) { }

    }

    // TypeToken: 0x200001B  // size: 0x18
    public sealed class <>c__DisplayClass25_0
    {
        // Fields
        public Unity.VisualScripting.ControlOutput port;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000085
        public System.Void .ctor() { }
        // RVA: 0x0B386E1C  token: 0x6000086
        private System.Boolean <DisconnectFromValid>b__0(Unity.VisualScripting.ControlConnection c) { }

    }

    // TypeToken: 0x200001D  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.ControlOutput.<>c <>9;  // static @ 0x0
        public static System.Func<Unity.VisualScripting.ControlConnection,Unity.VisualScripting.ControlInput> <>9__6_0;  // static @ 0x8
        public static System.Func<Unity.VisualScripting.InvalidConnection,Unity.VisualScripting.IUnitInputPort> <>9__8_0;  // static @ 0x10

        // Methods
        // RVA: 0x0B386F70  token: 0x6000093
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000094
        public System.Void .ctor() { }
        // RVA: 0x0B386DDC  token: 0x6000095
        private Unity.VisualScripting.ControlInput <get_validConnectedPorts>b__6_0(Unity.VisualScripting.ControlConnection c) { }
        // RVA: 0x0B386DA4  token: 0x6000096
        private Unity.VisualScripting.IUnitInputPort <get_invalidConnectedPorts>b__8_0(Unity.VisualScripting.InvalidConnection c) { }

    }

    // TypeToken: 0x200001E  // size: 0x18
    public sealed class <>c__DisplayClass21_0
    {
        // Fields
        public Unity.VisualScripting.ControlInput port;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000097
        public System.Void .ctor() { }
        // RVA: 0x0B386DF4  token: 0x6000098
        private System.Boolean <DisconnectFromValid>b__0(Unity.VisualScripting.ControlConnection c) { }

    }

    // TypeToken: 0x2000020  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.InvalidInput.<>c <>9;  // static @ 0x0
        public static System.Func<Unity.VisualScripting.InvalidConnection,Unity.VisualScripting.IUnitOutputPort> <>9__6_0;  // static @ 0x8
        public static System.Func<Unity.VisualScripting.InvalidConnection,Unity.VisualScripting.IUnitOutputPort> <>9__8_0;  // static @ 0x10

        // Methods
        // RVA: 0x0B386E44  token: 0x60000A3
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60000A4
        public System.Void .ctor() { }
        // RVA: 0x0B386D84  token: 0x60000A5
        private Unity.VisualScripting.IUnitOutputPort <get_validConnectedPorts>b__6_0(Unity.VisualScripting.InvalidConnection c) { }
        // RVA: 0x0B386D84  token: 0x60000A6
        private Unity.VisualScripting.IUnitOutputPort <get_invalidConnectedPorts>b__8_0(Unity.VisualScripting.InvalidConnection c) { }

    }

    // TypeToken: 0x2000022  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.InvalidOutput.<>c <>9;  // static @ 0x0
        public static System.Func<Unity.VisualScripting.InvalidConnection,Unity.VisualScripting.IUnitInputPort> <>9__6_0;  // static @ 0x8
        public static System.Func<Unity.VisualScripting.InvalidConnection,Unity.VisualScripting.IUnitInputPort> <>9__8_0;  // static @ 0x10

        // Methods
        // RVA: 0x0B387038  token: 0x60000B1
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60000B2
        public System.Void .ctor() { }
        // RVA: 0x0B386DA4  token: 0x60000B3
        private Unity.VisualScripting.IUnitInputPort <get_validConnectedPorts>b__6_0(Unity.VisualScripting.InvalidConnection c) { }
        // RVA: 0x0B386DA4  token: 0x60000B4
        private Unity.VisualScripting.IUnitInputPort <get_invalidConnectedPorts>b__8_0(Unity.VisualScripting.InvalidConnection c) { }

    }

    // TypeToken: 0x200002A
    public sealed class <>c__DisplayClass45_0
    {
        // Fields
        public Unity.VisualScripting.IUnitOutputPort source;  // 0x0
        public Unity.VisualScripting.IUnitInputPort destination;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60000D4
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60000D5
        private System.Boolean <ConnectInvalid>b__0(Unity.VisualScripting.InvalidConnection c) { }

    }

    // TypeToken: 0x200002B
    public sealed class <>c__DisplayClass46_0
    {
        // Fields
        public Unity.VisualScripting.IUnitOutputPort source;  // 0x0
        public Unity.VisualScripting.IUnitInputPort destination;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60000D6
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60000D7
        private System.Boolean <DisconnectInvalid>b__0(Unity.VisualScripting.InvalidConnection c) { }

    }

    // TypeToken: 0x200002E  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.ValueInput.<>c <>9;  // static @ 0x0
        public static System.Func<Unity.VisualScripting.ValueConnection,Unity.VisualScripting.ValueOutput> <>9__11_0;  // static @ 0x8
        public static System.Func<Unity.VisualScripting.InvalidConnection,Unity.VisualScripting.IUnitOutputPort> <>9__13_0;  // static @ 0x10

        // Methods
        // RVA: 0x0B38709C  token: 0x60000F6
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60000F7
        public System.Void .ctor() { }
        // RVA: 0x0B386DC4  token: 0x60000F8
        private Unity.VisualScripting.ValueOutput <get_validConnectedPorts>b__11_0(Unity.VisualScripting.ValueConnection c) { }
        // RVA: 0x0B386D84  token: 0x60000F9
        private Unity.VisualScripting.IUnitOutputPort <get_invalidConnectedPorts>b__13_0(Unity.VisualScripting.InvalidConnection c) { }

    }

    // TypeToken: 0x200002F  // size: 0x18
    public sealed class <>c__DisplayClass33_0
    {
        // Fields
        public Unity.VisualScripting.ValueOutput port;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60000FA
        public System.Void .ctor() { }
        // RVA: 0x0B386E1C  token: 0x60000FB
        private System.Boolean <DisconnectFromValid>b__0(Unity.VisualScripting.ValueConnection c) { }

    }

    // TypeToken: 0x2000031  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.ValueOutput.<>c <>9;  // static @ 0x0
        public static System.Func<Unity.VisualScripting.ValueConnection,Unity.VisualScripting.ValueInput> <>9__16_0;  // static @ 0x8
        public static System.Func<Unity.VisualScripting.InvalidConnection,Unity.VisualScripting.IUnitInputPort> <>9__18_0;  // static @ 0x10

        // Methods
        // RVA: 0x0B386FD4  token: 0x6000108
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000109
        public System.Void .ctor() { }
        // RVA: 0x0B386DDC  token: 0x600010A
        private Unity.VisualScripting.ValueInput <get_validConnectedPorts>b__16_0(Unity.VisualScripting.ValueConnection c) { }
        // RVA: 0x0B386DA4  token: 0x600010B
        private Unity.VisualScripting.IUnitInputPort <get_invalidConnectedPorts>b__18_0(Unity.VisualScripting.InvalidConnection c) { }

    }

    // TypeToken: 0x2000032  // size: 0x18
    public sealed class <>c__DisplayClass22_0
    {
        // Fields
        public Unity.VisualScripting.ValueInput port;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600010C
        public System.Void .ctor() { }
        // RVA: 0x0B386DF4  token: 0x600010D
        private System.Boolean <DisconnectFromValid>b__0(Unity.VisualScripting.ValueConnection c) { }

    }

    // TypeToken: 0x2000034
    public sealed class <>c__DisplayClass86_0`1
    {
        // Fields
        public System.Func<Unity.VisualScripting.Flow,T> getValue;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000128
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000129
        private System.Object <ValueOutput>b__0(Unity.VisualScripting.Flow recursion) { }

    }

namespace Unity.VisualScripting
{

    // TypeToken: 0x2000002  // size: 0x48
    public sealed class ControlConnection : Unity.VisualScripting.UnitConnection`2, Unity.VisualScripting.IUnitConnection, Unity.VisualScripting.IConnection`2, Unity.VisualScripting.IGraphElementWithDebugData, Unity.VisualScripting.IGraphElement, System.IDisposable, Unity.VisualScripting.IIdentifiable
    {
        // Properties
        Unity.VisualScripting.ControlOutput source { get; /* RVA: 0x0B383588 */ }
        Unity.VisualScripting.ControlInput destination { get; /* RVA: 0x0B3834AC */ }
        Unity.VisualScripting.IUnitOutputPort Unity.VisualScripting.IConnection<Unity.VisualScripting.IUnitOutputPort,Unity.VisualScripting.IUnitInputPort>.source { get; /* RVA: 0x0B38336C */ }
        Unity.VisualScripting.IUnitInputPort Unity.VisualScripting.IConnection<Unity.VisualScripting.IUnitOutputPort,Unity.VisualScripting.IUnitInputPort>.destination { get; /* RVA: 0x0B383364 */ }
        System.Boolean sourceExists { get; /* RVA: 0x0B383514 */ }
        System.Boolean destinationExists { get; /* RVA: 0x0B383438 */ }

        // Methods
        // RVA: 0x0B383374  token: 0x6000001
        public System.Void .ctor(Unity.VisualScripting.ControlOutput source, Unity.VisualScripting.ControlInput destination) { }

    }

    // TypeToken: 0x2000003  // size: 0x48
    public sealed class InvalidConnection : Unity.VisualScripting.UnitConnection`2, Unity.VisualScripting.IUnitConnection, Unity.VisualScripting.IConnection`2, Unity.VisualScripting.IGraphElementWithDebugData, Unity.VisualScripting.IGraphElement, System.IDisposable, Unity.VisualScripting.IIdentifiable
    {
        // Properties
        Unity.VisualScripting.IUnitOutputPort source { get; /* RVA: 0x0B385F68 */ }
        Unity.VisualScripting.IUnitInputPort destination { get; /* RVA: 0x0B385E00 */ }
        Unity.VisualScripting.IUnitOutputPort validSource { get; /* RVA: 0x0B386238 */ }
        Unity.VisualScripting.IUnitInputPort validDestination { get; /* RVA: 0x0B3860D0 */ }
        System.Boolean sourceExists { get; /* RVA: 0x0B385EB4 */ }
        System.Boolean destinationExists { get; /* RVA: 0x0B385D4C */ }
        System.Boolean validSourceExists { get; /* RVA: 0x0B386184 */ }
        System.Boolean validDestinationExists { get; /* RVA: 0x0B38601C */ }

        // Methods
        // RVA: 0x0B385CF4  token: 0x6000008
        public System.Void .ctor(Unity.VisualScripting.IUnitOutputPort source, Unity.VisualScripting.IUnitInputPort destination) { }
        // RVA: 0x0B385858  token: 0x6000011
        public virtual System.Boolean HandleDependencies() { }
        // RVA: 0x0B385B3C  token: 0x6000012
        private System.Boolean <get_source>b__4_0(Unity.VisualScripting.IUnitOutputPort p) { }
        // RVA: 0x0B385A8C  token: 0x6000013
        private System.Boolean <get_destination>b__6_0(Unity.VisualScripting.IUnitInputPort p) { }
        // RVA: 0x0B385C9C  token: 0x6000014
        private System.Boolean <get_validSource>b__8_0(Unity.VisualScripting.IUnitOutputPort p) { }
        // RVA: 0x0B385BEC  token: 0x6000015
        private System.Boolean <get_validDestination>b__10_0(Unity.VisualScripting.IUnitInputPort p) { }
        // RVA: 0x0B385AE4  token: 0x6000016
        private System.Boolean <get_sourceExists>b__12_0(Unity.VisualScripting.IUnitOutputPort p) { }
        // RVA: 0x0B385A34  token: 0x6000017
        private System.Boolean <get_destinationExists>b__14_0(Unity.VisualScripting.IUnitInputPort p) { }
        // RVA: 0x0B385C44  token: 0x6000018
        private System.Boolean <get_validSourceExists>b__16_0(Unity.VisualScripting.IUnitOutputPort p) { }
        // RVA: 0x0B385B94  token: 0x6000019
        private System.Boolean <get_validDestinationExists>b__18_0(Unity.VisualScripting.IUnitInputPort p) { }

    }

    // TypeToken: 0x2000004
    public interface IUnitConnection : Unity.VisualScripting.IConnection`2, Unity.VisualScripting.IGraphElementWithDebugData, Unity.VisualScripting.IGraphElement, System.IDisposable, Unity.VisualScripting.IIdentifiable
    {
    }

    // TypeToken: 0x2000005
    public interface IUnitConnectionDebugData : Unity.VisualScripting.IGraphElementDebugData
    {
        // Properties
        System.Int32 lastInvokeFrame { set; /* RVA: -1  // abstract */ }
        System.Single lastInvokeTime { set; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000006
    public interface IUnitRelation : Unity.VisualScripting.IConnection`2
    {
    }

    // TypeToken: 0x2000007
    public abstract class UnitConnection`2 : Unity.VisualScripting.GraphElement`1, Unity.VisualScripting.IConnection`2
    {
        // Fields
        private Unity.VisualScripting.IUnit <sourceUnit>k__BackingField;  // 0x0
        private System.String <sourceKey>k__BackingField;  // 0x0
        private Unity.VisualScripting.IUnit <destinationUnit>k__BackingField;  // 0x0
        private System.String <destinationKey>k__BackingField;  // 0x0

        // Properties
        Unity.VisualScripting.IUnit sourceUnit { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.String sourceKey { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        Unity.VisualScripting.IUnit destinationUnit { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.String destinationKey { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        TSourcePort source { get; /* RVA: -1  // abstract */ }
        TDestinationPort destination { get; /* RVA: -1  // abstract */ }
        System.Int32 dependencyOrder { get; /* RVA: -1  // not resolved */ }
        System.Boolean sourceExists { get; /* RVA: -1  // abstract */ }
        System.Boolean destinationExists { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600001C
        protected System.Void .ctor(TSourcePort source, TDestinationPort destination) { }
        // RVA: -1  // not resolved  token: 0x600002A
        public virtual System.Boolean HandleDependencies() { }

    }

    // TypeToken: 0x2000008  // size: 0x20
    public class UnitConnectionDebugData : Unity.VisualScripting.IUnitConnectionDebugData
    {
        // Fields
        private System.Int32 <lastInvokeFrame>k__BackingField;  // 0x10
        private System.Single <lastInvokeTime>k__BackingField;  // 0x14
        private System.Exception <runtimeException>k__BackingField;  // 0x18

        // Properties
        System.Int32 lastInvokeFrame { set; /* RVA: 0x014F51F0 */ }
        System.Single lastInvokeTime { set; /* RVA: 0x04D88D50 */ }
        System.Exception runtimeException { set; /* RVA: 0x05392C40 */ }

    }

    // TypeToken: 0x2000009  // size: 0x48
    public sealed class ValueConnection : Unity.VisualScripting.UnitConnection`2, Unity.VisualScripting.IUnitConnection, Unity.VisualScripting.IConnection`2, Unity.VisualScripting.IGraphElementWithDebugData, Unity.VisualScripting.IGraphElement, System.IDisposable, Unity.VisualScripting.IIdentifiable
    {
        // Properties
        Unity.VisualScripting.ValueOutput source { get; /* RVA: 0x0B388308 */ }
        Unity.VisualScripting.ValueInput destination { get; /* RVA: 0x0B38822C */ }
        Unity.VisualScripting.IUnitOutputPort Unity.VisualScripting.IConnection<Unity.VisualScripting.IUnitOutputPort,Unity.VisualScripting.IUnitInputPort>.source { get; /* RVA: 0x0B38336C */ }
        Unity.VisualScripting.IUnitInputPort Unity.VisualScripting.IConnection<Unity.VisualScripting.IUnitOutputPort,Unity.VisualScripting.IUnitInputPort>.destination { get; /* RVA: 0x0B383364 */ }
        System.Boolean sourceExists { get; /* RVA: 0x0B388294 */ }
        System.Boolean destinationExists { get; /* RVA: 0x0B3881B8 */ }

        // Methods
        // RVA: 0x0B388034  token: 0x600002E
        public System.Void .ctor(Unity.VisualScripting.ValueOutput source, Unity.VisualScripting.ValueInput destination) { }

    }

    // TypeToken: 0x200000B  // size: 0x10
    public class PortLabelHiddenAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000037
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000C  // size: 0x10
    public sealed class SpecialUnitAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000038
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000D  // size: 0x10
    public sealed class UnitHeaderInspectableAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000039
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000E  // size: 0x18
    public sealed class UnitShortTitleAttribute : System.Attribute
    {
        // Fields
        private System.String <title>k__BackingField;  // 0x10

        // Properties
        System.String title { set; /* RVA: 0x053908C0 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x600003A
        public System.Void .ctor(System.String title) { }

    }

    // TypeToken: 0x200000F  // size: 0x18
    public sealed class UnitTitleAttribute : System.Attribute
    {
        // Fields
        private System.String <title>k__BackingField;  // 0x10

        // Properties
        System.String title { set; /* RVA: 0x053908C0 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x600003C
        public System.Void .ctor(System.String title) { }

    }

    // TypeToken: 0x2000010  // size: 0x68
    public sealed class Flow : Unity.VisualScripting.IPoolable, System.IDisposable
    {
        // Fields
        private Unity.VisualScripting.GraphStack <stack>k__BackingField;  // 0x10
        private Unity.VisualScripting.Recursion<Unity.VisualScripting.Flow.RecursionNode> recursion;  // 0x18
        private readonly System.Collections.Generic.Dictionary<Unity.VisualScripting.IUnitValuePort,System.Object> locals;  // 0x20
        public readonly Unity.VisualScripting.VariableDeclarations variables;  // 0x28
        private readonly System.Collections.Generic.Stack<System.Int32> loops;  // 0x30
        private readonly System.Collections.Generic.HashSet<Unity.VisualScripting.GraphStack> preservedStacks;  // 0x38
        private UnityEngine.MonoBehaviour <coroutineRunner>k__BackingField;  // 0x40
        private System.Collections.Generic.ICollection<Unity.VisualScripting.Flow> activeCoroutinesRegistry;  // 0x48
        private System.Boolean coroutineStopRequested;  // 0x50
        private System.Boolean <isCoroutine>k__BackingField;  // 0x51
        private System.Collections.IEnumerator coroutineEnumerator;  // 0x58
        private System.Boolean <isPrediction>k__BackingField;  // 0x60
        private System.Boolean disposed;  // 0x61
        public System.Int32 loopIdentifier;  // 0x64

        // Properties
        Unity.VisualScripting.GraphStack stack { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        UnityEngine.MonoBehaviour coroutineRunner { set; /* RVA: 0x042B4AE0 */ }
        System.Boolean isCoroutine { set; /* RVA: 0x04D86830 */ }
        System.Boolean isPrediction { get; /* RVA: 0x04D88340 */ set; /* RVA: 0x04D88350 */ }
        System.Boolean enableDebug { get; /* RVA: 0x0B385828 */ }

        // Methods
        // RVA: 0x0B3856DC  token: 0x6000045
        private System.Void .ctor() { }
        // RVA: 0x0B385090  token: 0x6000046
        public static Unity.VisualScripting.Flow New(Unity.VisualScripting.GraphReference reference) { }
        // RVA: 0x0B385694  token: 0x6000047
        private virtual System.Void Unity.VisualScripting.IPoolable.New() { }
        // RVA: 0x0B38452C  token: 0x6000048
        public virtual System.Void Dispose() { }
        // RVA: 0x0B3852F0  token: 0x6000049
        private virtual System.Void Unity.VisualScripting.IPoolable.Free() { }
        // RVA: 0x0B384E94  token: 0x600004A
        public System.Void Invoke(Unity.VisualScripting.ControlOutput output) { }
        // RVA: 0x0B3842DC  token: 0x600004B
        private Unity.VisualScripting.Flow.RecursionNode BeforeInvoke(Unity.VisualScripting.ControlOutput output, Unity.VisualScripting.Flow.RecursionNode recursionNode) { }
        // RVA: 0x0B384288  token: 0x600004C
        private System.Void AfterInvoke(Unity.VisualScripting.ControlOutput output, Unity.VisualScripting.Flow.RecursionNode recursionNode) { }
        // RVA: 0x0B384D24  token: 0x600004D
        private Unity.VisualScripting.ControlOutput InvokeDelegate(Unity.VisualScripting.ControlInput input) { }
        // RVA: 0x0B384AB8  token: 0x600004E
        public System.Object GetValue(Unity.VisualScripting.ValueInput input) { }
        // RVA: 0x0B3846C8  token: 0x600004F
        private System.Object GetValue(Unity.VisualScripting.ValueOutput output) { }
        // RVA: 0x0B384CB4  token: 0x6000050
        public System.Object GetValue(Unity.VisualScripting.ValueInput input, System.Type type) { }
        // RVA: -1  // generic def  token: 0x6000051
        public T GetValue(Unity.VisualScripting.ValueInput input) { }
        // RVA: 0x0B385230  token: 0x6000052
        public System.Boolean TryGetDefaultValue(Unity.VisualScripting.ValueInput input, System.Object& defaultValue) { }
        // RVA: 0x0B3845E4  token: 0x6000053
        private System.Object GetValueDelegate(Unity.VisualScripting.ValueOutput output) { }

    }

    // TypeToken: 0x2000013  // size: 0x38
    public sealed class FlowGraph : Unity.VisualScripting.Graph, Unity.VisualScripting.IGraph, System.IDisposable, Unity.VisualScripting.ISerializationDepender, UnityEngine.ISerializationCallbackReceiver
    {
        // Fields
        private Unity.VisualScripting.GraphConnectionCollection<Unity.VisualScripting.ControlConnection,Unity.VisualScripting.ControlOutput,Unity.VisualScripting.ControlInput> <controlConnections>k__BackingField;  // 0x20
        private Unity.VisualScripting.GraphConnectionCollection<Unity.VisualScripting.ValueConnection,Unity.VisualScripting.ValueOutput,Unity.VisualScripting.ValueInput> <valueConnections>k__BackingField;  // 0x28
        private Unity.VisualScripting.GraphConnectionCollection<Unity.VisualScripting.InvalidConnection,Unity.VisualScripting.IUnitOutputPort,Unity.VisualScripting.IUnitInputPort> <invalidConnections>k__BackingField;  // 0x30

        // Properties
        Unity.VisualScripting.GraphConnectionCollection<Unity.VisualScripting.ControlConnection,Unity.VisualScripting.ControlOutput,Unity.VisualScripting.ControlInput> controlConnections { get; /* RVA: 0x04D862C0 */ }
        Unity.VisualScripting.GraphConnectionCollection<Unity.VisualScripting.ValueConnection,Unity.VisualScripting.ValueOutput,Unity.VisualScripting.ValueInput> valueConnections { get; /* RVA: 0x04D86240 */ }
        Unity.VisualScripting.GraphConnectionCollection<Unity.VisualScripting.InvalidConnection,Unity.VisualScripting.IUnitOutputPort,Unity.VisualScripting.IUnitInputPort> invalidConnections { get; /* RVA: 0x011F36E0 */ }

    }

    // TypeToken: 0x2000014
    public abstract class EventUnit`1 : Unity.VisualScripting.Unit, Unity.VisualScripting.IUnit, Unity.VisualScripting.IGraphElementWithDebugData, Unity.VisualScripting.IGraphElement, System.IDisposable, Unity.VisualScripting.IIdentifiable
    {
        // Fields
        private Unity.VisualScripting.ControlOutput <trigger>k__BackingField;  // 0x0

        // Properties
        Unity.VisualScripting.ControlOutput trigger { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000062
        protected virtual System.Void Definition() { }
        // RVA: -1  // not resolved  token: 0x6000063
        protected System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000064
        private virtual Unity.VisualScripting.FlowGraph Unity.VisualScripting.IUnit.get_graph() { }

    }

    // TypeToken: 0x2000015
    public abstract class ManualEventUnit`1 : Unity.VisualScripting.EventUnit`1
    {
        // Properties
        System.String hookName { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000066
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000016  // size: 0x78
    public sealed class MissingType : Unity.VisualScripting.Unit
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000067
        protected virtual System.Void Definition() { }

    }

    // TypeToken: 0x2000017
    public interface IUnit : Unity.VisualScripting.IGraphElementWithDebugData, Unity.VisualScripting.IGraphElement, System.IDisposable, Unity.VisualScripting.IIdentifiable
    {
        // Properties
        Unity.VisualScripting.FlowGraph graph { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.Dictionary<System.String,System.Object> defaultValues { get; /* RVA: -1  // abstract */ }
        Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.ControlInput> controlInputs { get; /* RVA: -1  // abstract */ }
        Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.ControlOutput> controlOutputs { get; /* RVA: -1  // abstract */ }
        Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.ValueInput> valueInputs { get; /* RVA: -1  // abstract */ }
        Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.ValueOutput> valueOutputs { get; /* RVA: -1  // abstract */ }
        Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.InvalidInput> invalidInputs { get; /* RVA: -1  // abstract */ }
        Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.InvalidOutput> invalidOutputs { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.IUnitInputPort> inputs { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.IUnitOutputPort> outputs { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.IUnitInputPort> validInputs { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.IUnitOutputPort> validOutputs { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000074
        public virtual System.Void PortsChanged() { }

    }

    // TypeToken: 0x2000018
    public interface IUnitDebugData : Unity.VisualScripting.IGraphElementDebugData
    {
        // Properties
        System.Int32 lastInvokeFrame { set; /* RVA: -1  // abstract */ }
        System.Single lastInvokeTime { set; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000019  // size: 0x28
    public sealed class ControlInput : Unity.VisualScripting.UnitPort`3, Unity.VisualScripting.IUnitPort, Unity.VisualScripting.IUnitInputPort
    {
        // Fields
        private readonly System.Func<Unity.VisualScripting.Flow,Unity.VisualScripting.ControlOutput> action;  // 0x20

        // Properties
        System.Boolean requiresCoroutine { get; /* RVA: 0x0A819158 */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.ControlConnection> validConnections { get; /* RVA: 0x0B383B4C */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.InvalidConnection> invalidConnections { get; /* RVA: 0x0B3839AC */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.ControlOutput> validConnectedPorts { get; /* RVA: 0x0B383A34 */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.IUnitOutputPort> invalidConnectedPorts { get; /* RVA: 0x0B383894 */ }

        // Methods
        // RVA: 0x02FFF600  token: 0x600007C
        public virtual System.Boolean CanConnectToValid(Unity.VisualScripting.ControlOutput port) { }
        // RVA: 0x0B38363C  token: 0x600007D
        public virtual System.Void ConnectToValid(Unity.VisualScripting.ControlOutput port) { }
        // RVA: 0x0B3835F0  token: 0x600007E
        public virtual System.Void ConnectToInvalid(Unity.VisualScripting.IUnitOutputPort port) { }
        // RVA: 0x0B383764  token: 0x600007F
        public virtual System.Void DisconnectFromValid(Unity.VisualScripting.ControlOutput port) { }
        // RVA: 0x0B383718  token: 0x6000080
        public virtual System.Void DisconnectFromInvalid(Unity.VisualScripting.IUnitOutputPort port) { }

    }

    // TypeToken: 0x200001C  // size: 0x20
    public sealed class ControlOutput : Unity.VisualScripting.UnitPort`3, Unity.VisualScripting.IUnitPort, Unity.VisualScripting.IUnitOutputPort
    {
        // Properties
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.ControlConnection> validConnections { get; /* RVA: 0x0B384200 */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.InvalidConnection> invalidConnections { get; /* RVA: 0x0B384060 */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.ControlInput> validConnectedPorts { get; /* RVA: 0x0B3840E8 */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.IUnitInputPort> invalidConnectedPorts { get; /* RVA: 0x0B383F48 */ }
        Unity.VisualScripting.ControlConnection connection { get; /* RVA: 0x0B383EC0 */ }
        System.Boolean hasValidConnection { get; /* RVA: 0x0B383F30 */ }

        // Methods
        // RVA: 0x0B383E78  token: 0x6000087
        public System.Void .ctor(System.String key) { }
        // RVA: 0x02FFF600  token: 0x600008E
        public virtual System.Boolean CanConnectToValid(Unity.VisualScripting.ControlInput port) { }
        // RVA: 0x0B383C20  token: 0x600008F
        public virtual System.Void ConnectToValid(Unity.VisualScripting.ControlInput port) { }
        // RVA: 0x0B383BD4  token: 0x6000090
        public virtual System.Void ConnectToInvalid(Unity.VisualScripting.IUnitInputPort port) { }
        // RVA: 0x0B383D48  token: 0x6000091
        public virtual System.Void DisconnectFromValid(Unity.VisualScripting.ControlInput port) { }
        // RVA: 0x0B383CFC  token: 0x6000092
        public virtual System.Void DisconnectFromInvalid(Unity.VisualScripting.IUnitInputPort port) { }

    }

    // TypeToken: 0x200001F  // size: 0x20
    public sealed class InvalidInput : Unity.VisualScripting.UnitPort`3, Unity.VisualScripting.IUnitPort, Unity.VisualScripting.IUnitInputPort
    {
        // Properties
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.InvalidConnection> validConnections { get; /* RVA: 0x0B3866C4 */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.InvalidConnection> invalidConnections { get; /* RVA: 0x0B38657C */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.IUnitOutputPort> validConnectedPorts { get; /* RVA: 0x0B3865AC */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.IUnitOutputPort> invalidConnectedPorts { get; /* RVA: 0x0B386464 */ }

        // Methods
        // RVA: 0x0B38641C  token: 0x6000099
        public System.Void .ctor(System.String key) { }
        // RVA: 0x012081B0  token: 0x600009E
        public virtual System.Boolean CanConnectToValid(Unity.VisualScripting.IUnitOutputPort port) { }
        // RVA: 0x0B386338  token: 0x600009F
        public virtual System.Void ConnectToValid(Unity.VisualScripting.IUnitOutputPort port) { }
        // RVA: 0x0B3862EC  token: 0x60000A0
        public virtual System.Void ConnectToInvalid(Unity.VisualScripting.IUnitOutputPort port) { }
        // RVA: 0x0B3863D0  token: 0x60000A1
        public virtual System.Void DisconnectFromValid(Unity.VisualScripting.IUnitOutputPort port) { }
        // RVA: 0x0B386384  token: 0x60000A2
        public virtual System.Void DisconnectFromInvalid(Unity.VisualScripting.IUnitOutputPort port) { }

    }

    // TypeToken: 0x2000021  // size: 0x20
    public sealed class InvalidOutput : Unity.VisualScripting.UnitPort`3, Unity.VisualScripting.IUnitPort, Unity.VisualScripting.IUnitOutputPort
    {
        // Properties
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.InvalidConnection> validConnections { get; /* RVA: 0x0B386B24 */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.InvalidConnection> invalidConnections { get; /* RVA: 0x0B3869DC */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.IUnitInputPort> validConnectedPorts { get; /* RVA: 0x0B386A0C */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.IUnitInputPort> invalidConnectedPorts { get; /* RVA: 0x0B3868C4 */ }

        // Methods
        // RVA: 0x0B38687C  token: 0x60000A7
        public System.Void .ctor(System.String key) { }
        // RVA: 0x012081B0  token: 0x60000AC
        public virtual System.Boolean CanConnectToValid(Unity.VisualScripting.IUnitInputPort port) { }
        // RVA: 0x0B386798  token: 0x60000AD
        public virtual System.Void ConnectToValid(Unity.VisualScripting.IUnitInputPort port) { }
        // RVA: 0x0B38674C  token: 0x60000AE
        public virtual System.Void ConnectToInvalid(Unity.VisualScripting.IUnitInputPort port) { }
        // RVA: 0x0B386830  token: 0x60000AF
        public virtual System.Void DisconnectFromValid(Unity.VisualScripting.IUnitInputPort port) { }
        // RVA: 0x0B3867E4  token: 0x60000B0
        public virtual System.Void DisconnectFromInvalid(Unity.VisualScripting.IUnitInputPort port) { }

    }

    // TypeToken: 0x2000023
    public interface IUnitInputPort : Unity.VisualScripting.IUnitPort
    {
    }

    // TypeToken: 0x2000024
    public interface IUnitOutputPort : Unity.VisualScripting.IUnitPort
    {
    }

    // TypeToken: 0x2000025
    public interface IUnitPort
    {
        // Properties
        Unity.VisualScripting.IUnit unit { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.String key { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60000B8
        public virtual System.Boolean CanInvalidlyConnectTo(Unity.VisualScripting.IUnitPort port) { }
        // RVA: -1  // abstract  token: 0x60000B9
        public virtual System.Boolean CanValidlyConnectTo(Unity.VisualScripting.IUnitPort port) { }
        // RVA: -1  // abstract  token: 0x60000BA
        public virtual System.Void InvalidlyConnectTo(Unity.VisualScripting.IUnitPort port) { }
        // RVA: -1  // abstract  token: 0x60000BB
        public virtual System.Void ValidlyConnectTo(Unity.VisualScripting.IUnitPort port) { }

    }

    // TypeToken: 0x2000026
    public interface IUnitPortCollection`1 : Unity.VisualScripting.IKeyedCollection`2, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
    }

    // TypeToken: 0x2000027
    public interface IUnitValuePort : Unity.VisualScripting.IUnitPort
    {
    }

    // TypeToken: 0x2000028  // size: 0x90
    public sealed class MissingValuePortInputException : System.Exception
    {
        // Methods
        // RVA: 0x0B386BAC  token: 0x60000BC
        public System.Void .ctor(System.String key) { }

    }

    // TypeToken: 0x2000029
    public abstract class UnitPort`3 : Unity.VisualScripting.IUnitPort
    {
        // Fields
        private Unity.VisualScripting.IUnit <unit>k__BackingField;  // 0x0
        private readonly System.String <key>k__BackingField;  // 0x0

        // Properties
        Unity.VisualScripting.IUnit unit { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.String key { get; /* RVA: -1  // not resolved */ }
        System.Collections.Generic.IEnumerable<TExternalConnection> validConnections { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.InvalidConnection> invalidConnections { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.IEnumerable<TValidOther> validConnectedPorts { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.IEnumerable<TInvalidOther> invalidConnectedPorts { get; /* RVA: -1  // abstract */ }
        System.Boolean hasValidConnection { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60000BD
        protected System.Void .ctor(System.String key) { }
        // RVA: -1  // not resolved  token: 0x60000C6
        private System.Boolean CanConnectTo(Unity.VisualScripting.IUnitPort port) { }
        // RVA: -1  // not resolved  token: 0x60000C7
        public virtual System.Boolean CanValidlyConnectTo(Unity.VisualScripting.IUnitPort port) { }
        // RVA: -1  // not resolved  token: 0x60000C8
        public virtual System.Boolean CanInvalidlyConnectTo(Unity.VisualScripting.IUnitPort port) { }
        // RVA: -1  // not resolved  token: 0x60000C9
        public virtual System.Void ValidlyConnectTo(Unity.VisualScripting.IUnitPort port) { }
        // RVA: -1  // not resolved  token: 0x60000CA
        public virtual System.Void InvalidlyConnectTo(Unity.VisualScripting.IUnitPort port) { }
        // RVA: -1  // not resolved  token: 0x60000CB
        public virtual System.Void Disconnect() { }
        // RVA: -1  // abstract  token: 0x60000CC
        public virtual System.Boolean CanConnectToValid(TValidOther port) { }
        // RVA: -1  // not resolved  token: 0x60000CD
        public System.Boolean CanConnectToInvalid(TInvalidOther port) { }
        // RVA: -1  // abstract  token: 0x60000CE
        public virtual System.Void ConnectToValid(TValidOther port) { }
        // RVA: -1  // abstract  token: 0x60000CF
        public virtual System.Void ConnectToInvalid(TInvalidOther port) { }
        // RVA: -1  // abstract  token: 0x60000D0
        public virtual System.Void DisconnectFromValid(TValidOther port) { }
        // RVA: -1  // abstract  token: 0x60000D1
        public virtual System.Void DisconnectFromInvalid(TInvalidOther port) { }
        // RVA: -1  // not resolved  token: 0x60000D2
        protected System.Void ConnectInvalid(Unity.VisualScripting.IUnitOutputPort source, Unity.VisualScripting.IUnitInputPort destination) { }
        // RVA: -1  // not resolved  token: 0x60000D3
        protected System.Void DisconnectInvalid(Unity.VisualScripting.IUnitOutputPort source, Unity.VisualScripting.IUnitInputPort destination) { }

    }

    // TypeToken: 0x200002C
    public sealed class UnitPortCollection`1 : System.Collections.ObjectModel.KeyedCollection`2, Unity.VisualScripting.IUnitPortCollection`1, Unity.VisualScripting.IKeyedCollection`2, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Unity.VisualScripting.IUnit <unit>k__BackingField;  // 0x0

        // Properties
        Unity.VisualScripting.IUnit unit { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60000D9
        public System.Void .ctor(Unity.VisualScripting.IUnit unit) { }
        // RVA: -1  // not resolved  token: 0x60000DA
        private System.Void BeforeAdd(TPort port) { }
        // RVA: -1  // not resolved  token: 0x60000DB
        private System.Void AfterAdd(TPort port) { }
        // RVA: -1  // not resolved  token: 0x60000DC
        private System.Void BeforeRemove(TPort port) { }
        // RVA: -1  // not resolved  token: 0x60000DD
        private System.Void AfterRemove(TPort port) { }
        // RVA: -1  // not resolved  token: 0x60000DE
        protected virtual System.String GetKeyForItem(TPort item) { }
        // RVA: -1  // not resolved  token: 0x60000DF
        protected virtual System.Void InsertItem(System.Int32 index, TPort item) { }
        // RVA: -1  // not resolved  token: 0x60000E0
        protected virtual System.Void RemoveItem(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60000E1
        protected virtual System.Void SetItem(System.Int32 index, TPort item) { }
        // RVA: -1  // not resolved  token: 0x60000E2
        protected virtual System.Void ClearItems() { }
        // RVA: -1  // not resolved  token: 0x60000E3
        private virtual TPort Unity.VisualScripting.IKeyedCollection<System.String,TPort>.get_Item(System.String key) { }
        // RVA: -1  // not resolved  token: 0x60000E4
        private virtual System.Boolean Unity.VisualScripting.IKeyedCollection<System.String,TPort>.Contains(System.String key) { }

    }

    // TypeToken: 0x200002D  // size: 0x30
    public sealed class ValueInput : Unity.VisualScripting.UnitPort`3, Unity.VisualScripting.IUnitValuePort, Unity.VisualScripting.IUnitPort, Unity.VisualScripting.IUnitInputPort
    {
        // Fields
        private readonly System.Type <type>k__BackingField;  // 0x20
        private System.Boolean <nullMeansSelf>k__BackingField;  // 0x28
        private static readonly System.Collections.Generic.HashSet<System.Type> typesWithDefaultValues;  // static @ 0x0

        // Properties
        System.Type type { get; /* RVA: 0x04D862C0 */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.ValueConnection> validConnections { get; /* RVA: 0x0B388FBC */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.InvalidConnection> invalidConnections { get; /* RVA: 0x0B388E1C */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.ValueOutput> validConnectedPorts { get; /* RVA: 0x0B388EA4 */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.IUnitOutputPort> invalidConnectedPorts { get; /* RVA: 0x0B388D04 */ }
        System.Boolean nullMeansSelf { get; /* RVA: 0x011F33C0 */ }
        Unity.VisualScripting.ValueConnection connection { get; /* RVA: 0x0B388C7C */ }
        System.Boolean hasValidConnection { get; /* RVA: 0x0B388CEC */ }

        // Methods
        // RVA: 0x0B388BBC  token: 0x60000E5
        public System.Void .ctor(System.String key, System.Type type) { }
        // RVA: 0x0B38868C  token: 0x60000EE
        public System.Void SetDefaultValue(System.Object value) { }
        // RVA: 0x0B388370  token: 0x60000EF
        public virtual System.Boolean CanConnectToValid(Unity.VisualScripting.ValueOutput port) { }
        // RVA: 0x0B388434  token: 0x60000F0
        public virtual System.Void ConnectToValid(Unity.VisualScripting.ValueOutput port) { }
        // RVA: 0x0B3883E8  token: 0x60000F1
        public virtual System.Void ConnectToInvalid(Unity.VisualScripting.IUnitOutputPort port) { }
        // RVA: 0x0B38855C  token: 0x60000F2
        public virtual System.Void DisconnectFromValid(Unity.VisualScripting.ValueOutput port) { }
        // RVA: 0x0B388510  token: 0x60000F3
        public virtual System.Void DisconnectFromInvalid(Unity.VisualScripting.IUnitOutputPort port) { }
        // RVA: 0x0B388808  token: 0x60000F4
        public static System.Boolean SupportsDefaultValue(System.Type type) { }
        // RVA: 0x0B388944  token: 0x60000F5
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000030  // size: 0x30
    public sealed class ValueOutput : Unity.VisualScripting.UnitPort`3, Unity.VisualScripting.IUnitValuePort, Unity.VisualScripting.IUnitPort, Unity.VisualScripting.IUnitOutputPort
    {
        // Fields
        private readonly System.Func<Unity.VisualScripting.Flow,System.Object> getValue;  // 0x20
        private readonly System.Type <type>k__BackingField;  // 0x28

        // Properties
        System.Boolean supportsFetch { get; /* RVA: 0x09B00070 */ }
        System.Type type { get; /* RVA: 0x04D86240 */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.ValueConnection> validConnections { get; /* RVA: 0x0B38971C */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.InvalidConnection> invalidConnections { get; /* RVA: 0x0B38957C */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.ValueInput> validConnectedPorts { get; /* RVA: 0x0B389604 */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.IUnitInputPort> invalidConnectedPorts { get; /* RVA: 0x0B389464 */ }

        // Methods
        // RVA: 0x0B389350  token: 0x60000FC
        public System.Void .ctor(System.String key, System.Type type, System.Func<Unity.VisualScripting.Flow,System.Object> getValue) { }
        // RVA: 0x0B389044  token: 0x6000103
        public virtual System.Boolean CanConnectToValid(Unity.VisualScripting.ValueInput port) { }
        // RVA: 0x0B3890F8  token: 0x6000104
        public virtual System.Void ConnectToValid(Unity.VisualScripting.ValueInput port) { }
        // RVA: 0x0B3890AC  token: 0x6000105
        public virtual System.Void ConnectToInvalid(Unity.VisualScripting.IUnitInputPort port) { }
        // RVA: 0x0B389220  token: 0x6000106
        public virtual System.Void DisconnectFromValid(Unity.VisualScripting.ValueInput port) { }
        // RVA: 0x0B3891D4  token: 0x6000107
        public virtual System.Void DisconnectFromInvalid(Unity.VisualScripting.IUnitInputPort port) { }

    }

    // TypeToken: 0x2000033  // size: 0x78
    public abstract class Unit : Unity.VisualScripting.GraphElement`1, Unity.VisualScripting.IUnit, Unity.VisualScripting.IGraphElementWithDebugData, Unity.VisualScripting.IGraphElement, System.IDisposable, Unity.VisualScripting.IIdentifiable
    {
        // Fields
        private readonly Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.ControlInput> <controlInputs>k__BackingField;  // 0x28
        private readonly Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.ControlOutput> <controlOutputs>k__BackingField;  // 0x30
        private readonly Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.ValueInput> <valueInputs>k__BackingField;  // 0x38
        private readonly Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.ValueOutput> <valueOutputs>k__BackingField;  // 0x40
        private readonly Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.InvalidInput> <invalidInputs>k__BackingField;  // 0x48
        private readonly Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.InvalidOutput> <invalidOutputs>k__BackingField;  // 0x50
        private System.Action onPortsChanged;  // 0x58
        private System.Collections.Generic.Dictionary<System.String,System.Object> <defaultValues>k__BackingField;  // 0x60
        private Unity.VisualScripting.IConnectionCollection<Unity.VisualScripting.IUnitRelation,Unity.VisualScripting.IUnitPort,Unity.VisualScripting.IUnitPort> <relations>k__BackingField;  // 0x68
        private System.Boolean <isControlRoot>k__BackingField;  // 0x70

        // Properties
        Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.ControlInput> controlInputs { get; /* RVA: 0x04D86240 */ }
        Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.ControlOutput> controlOutputs { get; /* RVA: 0x011F36E0 */ }
        Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.ValueInput> valueInputs { get; /* RVA: 0x04D85A50 */ }
        Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.ValueOutput> valueOutputs { get; /* RVA: 0x04D85A60 */ }
        Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.InvalidInput> invalidInputs { get; /* RVA: 0x04D86200 */ }
        Unity.VisualScripting.IUnitPortCollection<Unity.VisualScripting.InvalidOutput> invalidOutputs { get; /* RVA: 0x04D86270 */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.IUnitInputPort> inputs { get; /* RVA: 0x0B387D24 */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.IUnitOutputPort> outputs { get; /* RVA: 0x0B387DF8 */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.IUnitInputPort> validInputs { get; /* RVA: 0x0B387ECC */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.IUnitOutputPort> validOutputs { get; /* RVA: 0x0B387F80 */ }
        System.Collections.Generic.Dictionary<System.String,System.Object> defaultValues { get; /* RVA: 0x04D86280 */ set; /* RVA: 0x063CC164 */ }
        Unity.VisualScripting.IConnectionCollection<Unity.VisualScripting.IUnitRelation,Unity.VisualScripting.IUnitPort,Unity.VisualScripting.IUnitPort> relations { set; /* RVA: 0x0485F170 */ }
        System.Boolean isControlRoot { set; /* RVA: 0x04D86A80 */ }

        // Methods
        // RVA: 0x0B387A74  token: 0x600010E
        protected System.Void .ctor() { }
        // RVA: -1  // abstract  token: 0x600010F
        protected virtual System.Void Definition() { }
        // RVA: 0x070E89F8  token: 0x600011A
        public virtual System.Void PortsChanged() { }
        // RVA: 0x0B387670  token: 0x600011F
        protected System.Void EnsureUniqueInput(System.String key) { }
        // RVA: 0x0B3877A4  token: 0x6000120
        protected System.Void EnsureUniqueOutput(System.String key) { }
        // RVA: 0x0B3875D8  token: 0x6000121
        protected Unity.VisualScripting.ControlOutput ControlOutput(System.String key) { }
        // RVA: 0x0B387910  token: 0x6000122
        protected Unity.VisualScripting.ValueInput ValueInput(System.Type type, System.String key) { }
        // RVA: -1  // generic def  token: 0x6000123
        protected Unity.VisualScripting.ValueInput ValueInput(System.String key) { }
        // RVA: -1  // generic def  token: 0x6000124
        protected Unity.VisualScripting.ValueInput ValueInput(System.String key, T default) { }
        // RVA: 0x0B3879B8  token: 0x6000125
        protected Unity.VisualScripting.ValueOutput ValueOutput(System.Type type, System.String key, System.Func<Unity.VisualScripting.Flow,System.Object> getValue) { }
        // RVA: -1  // generic def  token: 0x6000126
        protected Unity.VisualScripting.ValueOutput ValueOutput(System.String key, System.Func<Unity.VisualScripting.Flow,T> getValue) { }
        // RVA: 0x0B3878D8  token: 0x6000127
        private virtual Unity.VisualScripting.FlowGraph Unity.VisualScripting.IUnit.get_graph() { }

    }

    // TypeToken: 0x2000035  // size: 0x38
    public class UnitCategory : System.Attribute
    {
        // Fields
        private readonly Unity.VisualScripting.UnitCategory <root>k__BackingField;  // 0x10
        private readonly Unity.VisualScripting.UnitCategory <parent>k__BackingField;  // 0x18
        private readonly System.String <fullName>k__BackingField;  // 0x20
        private readonly System.String <name>k__BackingField;  // 0x28
        private readonly System.Boolean <isRoot>k__BackingField;  // 0x30

        // Properties
        System.String fullName { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x0B387414  token: 0x600012A
        public System.Void .ctor(System.String fullName) { }
        // RVA: 0x0B387368  token: 0x600012C
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B3873F0  token: 0x600012D
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x04D862C0  token: 0x600012E
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000036  // size: 0x18
    public class UnitCategoryConverter : Unity.VisualScripting.FullSerializer.fsDirectConverter
    {
        // Properties
        System.Type ModelType { get; /* RVA: 0x0B387318 */ }

        // Methods
        // RVA: 0x0B387230  token: 0x6000130
        public virtual Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType) { }
        // RVA: 0x0B387100  token: 0x6000131
        public virtual Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType) { }

    }

}

