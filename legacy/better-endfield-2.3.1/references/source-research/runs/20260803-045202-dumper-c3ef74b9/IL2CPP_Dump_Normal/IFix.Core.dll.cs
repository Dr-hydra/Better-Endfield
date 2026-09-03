// ========================================================
// Dumped by @desirepro
// Assembly: IFix.Core.dll
// Classes:  37
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000004  // size: 0x18
    public sealed class <readMethod>c__AnonStorey0
    {
        // Fields
        private System.Type[] genericArgInfos;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000009
        public System.Void .ctor() { }
        // RVA: 0x09E11FB4  token: 0x600000A
        private System.String <>m__0(System.Text.RegularExpressions.Match m) { }

    }

    // TypeToken: 0x2000005  // size: 0x18
    public sealed class <Load>c__AnonStorey1
    {
        // Fields
        private System.Collections.Generic.List<System.IntPtr> nativePointers;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600000B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x20
    public sealed class <Load>c__AnonStorey2
    {
        // Fields
        private IFix.Core.WrappersManager wrapperManager;  // 0x10
        private IFix.Core.PatchManager.<Load>c__AnonStorey1 <>f__ref$1;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600000C
        public System.Void .ctor() { }
        // RVA: 0x09E11EF4  token: 0x600000D
        private System.Void <>m__0() { }
        // RVA: 0x09E11F94  token: 0x600000E
        private System.Void <>m__1() { }

    }

    // TypeToken: 0x2000010  // size: 0x18
    public sealed class <Create>c__AnonStorey0
    {
        // Fields
        private System.Type delegateType;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000027
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000011  // size: 0x20
    public sealed class <Create>c__AnonStorey1
    {
        // Fields
        private System.Reflection.MethodInfo methodInfo;  // 0x10
        private IFix.Core.GenericDelegateFactory.<Create>c__AnonStorey0 <>f__ref$0;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000028
        public System.Void .ctor() { }
        // RVA: 0x09E11ECC  token: 0x6000029
        private System.Delegate <>m__0(IFix.Core.GenericDelegate o) { }

    }

    // TypeToken: 0x2000012  // size: 0x20
    public sealed class <Create>c__AnonStorey2
    {
        // Fields
        private System.Reflection.MethodInfo methodInfo;  // 0x10
        private IFix.Core.GenericDelegateFactory.<Create>c__AnonStorey0 <>f__ref$0;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600002A
        public System.Void .ctor() { }
        // RVA: 0x09E11ECC  token: 0x600002B
        private System.Delegate <>m__0(IFix.Core.GenericDelegate o) { }

    }

    // TypeToken: 0x2000024  // size: 0x20
    public class CalliInvokerInfo
    {
        // Fields
        public IFix.Core.ExternInvoker Invoker;  // 0x10
        public System.Int32 ArgumentCount;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60000A6
        public System.Void .ctor() { }

    }

namespace IFix
{

    // TypeToken: 0x200001C  // size: 0x10
    public class CustomBridgeAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000069
        public System.Void .ctor() { }

    }

}

namespace IFix.Core
{

    // TypeToken: 0x2000002  // size: 0x18
    public class IDTagAttribute : System.Attribute
    {
        // Fields
        public System.Int32 ID;  // 0x10

        // Methods
        // RVA: 0x014F51F0  token: 0x6000001
        public System.Void .ctor(System.Int32 id) { }

    }

    // TypeToken: 0x2000003  // size: 0x10
    public static class PatchManager
    {
        // Fields
        private static System.Collections.Generic.Dictionary<System.Reflection.Assembly,System.Action> removers;  // static @ 0x0
        private static System.Func<System.Reflection.ParameterInfo,System.Type> <>f__am$cache0;  // static @ 0x8

        // Methods
        // RVA: 0x09E0FD0C  token: 0x6000002
        private static System.Reflection.MethodBase readMethod(System.IO.BinaryReader reader, System.Type[] externTypes) { }
        // RVA: 0x09E0FA90  token: 0x6000003
        private static System.Int32 getMapId(System.Collections.Generic.List<System.Type> idMapArray, System.Reflection.MethodBase method) { }
        // RVA: 0x09E106C8  token: 0x6000004
        private static System.Int32[] readSlotInfo(System.IO.BinaryReader reader, System.Collections.Generic.Dictionary<System.Reflection.MethodInfo,System.Int32> itfMethodToId, System.Type[] externTypes, System.Int32 maxId) { }
        // RVA: 0x09E0B1AC  token: 0x6000005
        public static IFix.Core.VirtualMachine Load(System.IO.Stream stream, System.Boolean checkNew) { }
        // RVA: 0x09E0F870  token: 0x6000006
        public static System.Void Unload(System.Reflection.Assembly assembly) { }
        // RVA: 0x09E0FA04  token: 0x6000007
        private static System.Void .cctor() { }
        // RVA: 0x053E4DA0  token: 0x6000008
        private static System.Type <readMethod>m__0(System.Reflection.ParameterInfo p) { }

    }

    // TypeToken: 0x2000007  // size: 0x14
    public sealed struct Code
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static IFix.Core.Code Blt_Un;  // const
        public static IFix.Core.Code Ldind_U1;  // const
        public static IFix.Core.Code Stelem_Any;  // const
        public static IFix.Core.Code Ldelem_I;  // const
        public static IFix.Core.Code Endfinally;  // const
        public static IFix.Core.Code Arglist;  // const
        public static IFix.Core.Code Shl;  // const
        public static IFix.Core.Code Conv_Ovf_I4;  // const
        public static IFix.Core.Code Stloc;  // const
        public static IFix.Core.Code Refanytype;  // const
        public static IFix.Core.Code Callvirt;  // const
        public static IFix.Core.Code Ldelem_R8;  // const
        public static IFix.Core.Code Unaligned;  // const
        public static IFix.Core.Code Conv_I2;  // const
        public static IFix.Core.Code Ldfld;  // const
        public static IFix.Core.Code Sizeof;  // const
        public static IFix.Core.Code Newanon;  // const
        public static IFix.Core.Code Sub;  // const
        public static IFix.Core.Code Conv_Ovf_U2;  // const
        public static IFix.Core.Code Cgt_Un;  // const
        public static IFix.Core.Code Call;  // const
        public static IFix.Core.Code Stind_R8;  // const
        public static IFix.Core.Code Conv_Ovf_I8;  // const
        public static IFix.Core.Code Readonly;  // const
        public static IFix.Core.Code Newobj;  // const
        public static IFix.Core.Code Shr_Un;  // const
        public static IFix.Core.Code Ldvirtftn;  // const
        public static IFix.Core.Code Jmp;  // const
        public static IFix.Core.Code Cgt;  // const
        public static IFix.Core.Code Ldtoken;  // const
        public static IFix.Core.Code Conv_Ovf_U;  // const
        public static IFix.Core.Code Ldind_Ref;  // const
        public static IFix.Core.Code Conv_Ovf_I_Un;  // const
        public static IFix.Core.Code Conv_I1;  // const
        public static IFix.Core.Code Ldloc;  // const
        public static IFix.Core.Code Ldind_R8;  // const
        public static IFix.Core.Code Ldind_U4;  // const
        public static IFix.Core.Code Ldlen;  // const
        public static IFix.Core.Code Rem_Un;  // const
        public static IFix.Core.Code Starg;  // const
        public static IFix.Core.Code Calli;  // const
        public static IFix.Core.Code Conv_Ovf_U1_Un;  // const
        public static IFix.Core.Code Add;  // const
        public static IFix.Core.Code Switch;  // const
        public static IFix.Core.Code CallExtern;  // const
        public static IFix.Core.Code Ldelem_I8;  // const
        public static IFix.Core.Code Ldelema;  // const
        public static IFix.Core.Code Div;  // const
        public static IFix.Core.Code Br;  // const
        public static IFix.Core.Code Ret;  // const
        public static IFix.Core.Code Ldind_I4;  // const
        public static IFix.Core.Code Clt;  // const
        public static IFix.Core.Code Conv_I4;  // const
        public static IFix.Core.Code Ceq;  // const
        public static IFix.Core.Code Conv_U;  // const
        public static IFix.Core.Code Initobj;  // const
        public static IFix.Core.Code Stelem_Ref;  // const
        public static IFix.Core.Code Mul_Ovf_Un;  // const
        public static IFix.Core.Code Ldstr;  // const
        public static IFix.Core.Code Mul_Ovf;  // const
        public static IFix.Core.Code Sub_Ovf;  // const
        public static IFix.Core.Code Ldelem_U1;  // const
        public static IFix.Core.Code Conv_I;  // const
        public static IFix.Core.Code Conv_U4;  // const
        public static IFix.Core.Code Blt;  // const
        public static IFix.Core.Code Conv_Ovf_I;  // const
        public static IFix.Core.Code No;  // const
        public static IFix.Core.Code Brtrue;  // const
        public static IFix.Core.Code Unbox_Any;  // const
        public static IFix.Core.Code Not;  // const
        public static IFix.Core.Code Neg;  // const
        public static IFix.Core.Code Ldind_I8;  // const
        public static IFix.Core.Code Stind_Ref;  // const
        public static IFix.Core.Code Conv_R4;  // const
        public static IFix.Core.Code Conv_Ovf_U4;  // const
        public static IFix.Core.Code Conv_Ovf_U8;  // const
        public static IFix.Core.Code Ldind_I;  // const
        public static IFix.Core.Code Tail;  // const
        public static IFix.Core.Code Initblk;  // const
        public static IFix.Core.Code Conv_Ovf_U8_Un;  // const
        public static IFix.Core.Code Shr;  // const
        public static IFix.Core.Code Callvirtvirt;  // const
        public static IFix.Core.Code StackSpace;  // const
        public static IFix.Core.Code Ldsflda;  // const
        public static IFix.Core.Code Ble;  // const
        public static IFix.Core.Code Ldloca;  // const
        public static IFix.Core.Code Ldtype;  // const
        public static IFix.Core.Code Conv_I8;  // const
        public static IFix.Core.Code Conv_Ovf_U2_Un;  // const
        public static IFix.Core.Code Add_Ovf;  // const
        public static IFix.Core.Code Isinst;  // const
        public static IFix.Core.Code Stobj;  // const
        public static IFix.Core.Code Conv_Ovf_I1_Un;  // const
        public static IFix.Core.Code Cpblk;  // const
        public static IFix.Core.Code Pop;  // const
        public static IFix.Core.Code And;  // const
        public static IFix.Core.Code Box;  // const
        public static IFix.Core.Code Mul;  // const
        public static IFix.Core.Code Conv_U1;  // const
        public static IFix.Core.Code Nop;  // const
        public static IFix.Core.Code Conv_R_Un;  // const
        public static IFix.Core.Code Ldarga;  // const
        public static IFix.Core.Code Bgt;  // const
        public static IFix.Core.Code Break;  // const
        public static IFix.Core.Code Div_Un;  // const
        public static IFix.Core.Code Ldc_I4;  // const
        public static IFix.Core.Code Refanyval;  // const
        public static IFix.Core.Code Conv_U8;  // const
        public static IFix.Core.Code Bgt_Un;  // const
        public static IFix.Core.Code Stelem_R8;  // const
        public static IFix.Core.Code Bge;  // const
        public static IFix.Core.Code Ldflda;  // const
        public static IFix.Core.Code Ldelem_I1;  // const
        public static IFix.Core.Code Localloc;  // const
        public static IFix.Core.Code Ldind_I1;  // const
        public static IFix.Core.Code Ldelem_U4;  // const
        public static IFix.Core.Code Volatile;  // const
        public static IFix.Core.Code Ldarg;  // const
        public static IFix.Core.Code Ldftn;  // const
        public static IFix.Core.Code Xor;  // const
        public static IFix.Core.Code Conv_Ovf_I2;  // const
        public static IFix.Core.Code Conv_Ovf_U_Un;  // const
        public static IFix.Core.Code Stind_I2;  // const
        public static IFix.Core.Code Ldc_R4;  // const
        public static IFix.Core.Code Stelem_I2;  // const
        public static IFix.Core.Code Or;  // const
        public static IFix.Core.Code Castclass;  // const
        public static IFix.Core.Code Newarr;  // const
        public static IFix.Core.Code Throw;  // const
        public static IFix.Core.Code Conv_U2;  // const
        public static IFix.Core.Code Clt_Un;  // const
        public static IFix.Core.Code Conv_Ovf_U4_Un;  // const
        public static IFix.Core.Code Dup;  // const
        public static IFix.Core.Code Ldvirtftn2;  // const
        public static IFix.Core.Code Add_Ovf_Un;  // const
        public static IFix.Core.Code Ldc_R8;  // const
        public static IFix.Core.Code Ldelem_Any;  // const
        public static IFix.Core.Code Sub_Ovf_Un;  // const
        public static IFix.Core.Code Brfalse;  // const
        public static IFix.Core.Code Conv_R8;  // const
        public static IFix.Core.Code Unbox;  // const
        public static IFix.Core.Code Endfilter;  // const
        public static IFix.Core.Code Rethrow;  // const
        public static IFix.Core.Code Rem;  // const
        public static IFix.Core.Code Ldnull;  // const
        public static IFix.Core.Code Conv_Ovf_U1;  // const
        public static IFix.Core.Code Stfld;  // const
        public static IFix.Core.Code Stelem_I;  // const
        public static IFix.Core.Code Mkrefany;  // const
        public static IFix.Core.Code Stelem_R4;  // const
        public static IFix.Core.Code Ldc_I8;  // const
        public static IFix.Core.Code Cpobj;  // const
        public static IFix.Core.Code Stind_I8;  // const
        public static IFix.Core.Code Bne_Un;  // const
        public static IFix.Core.Code Constrained;  // const
        public static IFix.Core.Code Stelem_I1;  // const
        public static IFix.Core.Code Beq;  // const
        public static IFix.Core.Code Bge_Un;  // const
        public static IFix.Core.Code Ldind_I2;  // const
        public static IFix.Core.Code Ldelem_I2;  // const
        public static IFix.Core.Code Conv_Ovf_I8_Un;  // const
        public static IFix.Core.Code Conv_Ovf_I1;  // const
        public static IFix.Core.Code Ldobj;  // const
        public static IFix.Core.Code Leave;  // const
        public static IFix.Core.Code Stind_I;  // const
        public static IFix.Core.Code Stind_I1;  // const
        public static IFix.Core.Code Ldelem_R4;  // const
        public static IFix.Core.Code Conv_Ovf_I4_Un;  // const
        public static IFix.Core.Code Ldelem_I4;  // const
        public static IFix.Core.Code Ldind_R4;  // const
        public static IFix.Core.Code Ckfinite;  // const
        public static IFix.Core.Code Ldelem_Ref;  // const
        public static IFix.Core.Code Stsfld;  // const
        public static IFix.Core.Code Ldsfld;  // const
        public static IFix.Core.Code Stelem_I8;  // const
        public static IFix.Core.Code Stelem_I4;  // const
        public static IFix.Core.Code Ldelem_U2;  // const
        public static IFix.Core.Code Conv_Ovf_I2_Un;  // const
        public static IFix.Core.Code Stind_R4;  // const
        public static IFix.Core.Code Ldind_U2;  // const
        public static IFix.Core.Code Ble_Un;  // const
        public static IFix.Core.Code Stind_I4;  // const

    }

    // TypeToken: 0x2000008  // size: 0x18
    public sealed struct Instruction
    {
        // Fields
        public IFix.Core.Code Code;  // 0x10
        public System.Int32 Operand;  // 0x14

    }

    // TypeToken: 0x2000009  // size: 0x14
    public sealed struct ExceptionHandlerType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static IFix.Core.ExceptionHandlerType Catch;  // const
        public static IFix.Core.ExceptionHandlerType Filter;  // const
        public static IFix.Core.ExceptionHandlerType Finally;  // const
        public static IFix.Core.ExceptionHandlerType Fault;  // const

    }

    // TypeToken: 0x200000A  // size: 0x30
    public sealed class ExceptionHandler
    {
        // Fields
        public System.Type CatchType;  // 0x10
        public System.Int32 CatchTypeId;  // 0x18
        public System.Int32 HandlerEnd;  // 0x1c
        public System.Int32 HandlerStart;  // 0x20
        public IFix.Core.ExceptionHandlerType HandlerType;  // 0x24
        public System.Int32 TryEnd;  // 0x28
        public System.Int32 TryStart;  // 0x2c

        // Methods
        // RVA: 0x041E1670  token: 0x600000F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000B  // size: 0x40
    public class AnonymousStorey
    {
        // Fields
        private IFix.Core.Value[] unmanagedFields;  // 0x10
        private System.Object[] managedFields;  // 0x18
        private System.Int32 typeId;  // 0x20
        protected IFix.Core.VirtualMachine virtualMachine;  // 0x28
        private System.Int32 equalMethodId;  // 0x30
        private System.Int32 finalizeMethodId;  // 0x34
        private System.Int32 getHashCodeMethodId;  // 0x38
        private System.Int32 toStringMethodId;  // 0x3c

        // Methods
        // RVA: 0x09E071B0  token: 0x6000010
        public System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeID, System.Int32[] vTable, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x09E06EDC  token: 0x6000011
        private System.Void Ldfld(System.Int32 fieldIndex, IFix.Core.Value* evaluationStackBase, IFix.Core.Value* evaluationStackPointer, System.Object[] managedStack) { }
        // RVA: 0x09E0702C  token: 0x6000012
        private System.Void Stfld(System.Int32 fieldIndex, IFix.Core.Value* evaluationStackBase, IFix.Core.Value* evaluationStackPointer, System.Object[] managedStack) { }
        // RVA: 0x09E06E60  token: 0x6000013
        private System.Object Get(System.Int32 fieldIndex, System.Type type, IFix.Core.VirtualMachine virtualMachine, System.Boolean valueTypeClone) { }
        // RVA: 0x09E06FB8  token: 0x6000014
        private System.Void Set(System.Int32 fieldIndex, System.Object obj, System.Type type, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x02FB1840  token: 0x6000015
        public System.Boolean ObjectEquals(System.Object obj) { }
        // RVA: 0x09E06C20  token: 0x6000016
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x04D87100  token: 0x6000017
        public System.Int32 ObjectGetHashCode() { }
        // RVA: 0x09E06DC4  token: 0x6000018
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0669AD6C  token: 0x6000019
        public System.String ObjectToString() { }
        // RVA: 0x09E070F0  token: 0x600001A
        public virtual System.String ToString() { }
        // RVA: 0x09E06CDC  token: 0x600001B
        protected virtual System.Void Finalize() { }

    }

    // TypeToken: 0x200000C  // size: 0x38
    public class AnonymousStoreyInfo
    {
        // Fields
        public System.Int32 FieldNum;  // 0x10
        public System.Int32[] FieldTypes;  // 0x18
        public System.Int32 CtorId;  // 0x20
        public System.Int32 CtorParamNum;  // 0x24
        public System.Int32[] Slots;  // 0x28
        public System.Int32[] VTable;  // 0x30

        // Methods
        // RVA: 0x041E1670  token: 0x600001C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000D  // size: 0x14
    public sealed struct ValueType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static IFix.Core.ValueType Integer;  // const
        public static IFix.Core.ValueType Long;  // const
        public static IFix.Core.ValueType Float;  // const
        public static IFix.Core.ValueType Double;  // const
        public static IFix.Core.ValueType StackReference;  // const
        public static IFix.Core.ValueType StaticFieldReference;  // const
        public static IFix.Core.ValueType FieldReference;  // const
        public static IFix.Core.ValueType ChainFieldReference;  // const
        public static IFix.Core.ValueType Object;  // const
        public static IFix.Core.ValueType ValueType;  // const
        public static IFix.Core.ValueType ArrayReference;  // const

    }

    // TypeToken: 0x200000E  // size: 0x1C
    public sealed struct Value
    {
        // Fields
        public IFix.Core.ValueType Type;  // 0x10
        public System.Int32 Value1;  // 0x14
        public System.Int32 Value2;  // 0x18

    }

    // TypeToken: 0x200000F  // size: 0x10
    public class GenericDelegateFactory
    {
        // Fields
        private static System.Reflection.MethodInfo[] genericAction;  // static @ 0x0
        private static System.Reflection.MethodInfo[] genericFunc;  // static @ 0x8
        private static System.Collections.Generic.Dictionary<System.Type,System.Func<IFix.Core.GenericDelegate,System.Delegate>> genericDelegateCreatorCache;  // static @ 0x10
        private static System.Func<System.Reflection.MethodInfo,System.Boolean> <>f__am$cache0;  // static @ 0x18
        private static System.Func<System.Reflection.MethodInfo,System.Int32> <>f__am$cache1;  // static @ 0x20
        private static System.Func<System.Reflection.MethodInfo,System.Boolean> <>f__am$cache2;  // static @ 0x28
        private static System.Func<System.Reflection.MethodInfo,System.Int32> <>f__am$cache3;  // static @ 0x30
        private static System.Func<System.Reflection.ParameterInfo,System.Boolean> <>f__am$cache4;  // static @ 0x38
        private static System.Func<IFix.Core.GenericDelegate,System.Delegate> <>f__am$cache5;  // static @ 0x40
        private static System.Func<System.Reflection.ParameterInfo,System.Type> <>f__am$cache6;  // static @ 0x48

        // Methods
        // RVA: 0x09E0A2F8  token: 0x600001D
        private static System.Void PreventStripping(System.Object obj) { }
        // RVA: 0x09E09798  token: 0x600001E
        private static System.Delegate Create(System.Type delegateType, IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj) { }
        // RVA: 0x09E0A5D4  token: 0x600001F
        private static System.Void .cctor() { }
        // RVA: 0x09E0A4B8  token: 0x6000020
        private static System.Boolean <Create>m__0(System.Reflection.MethodInfo m) { }
        // RVA: 0x09E0A50C  token: 0x6000021
        private static System.Int32 <Create>m__1(System.Reflection.MethodInfo m) { }
        // RVA: 0x09E0A538  token: 0x6000022
        private static System.Boolean <Create>m__2(System.Reflection.MethodInfo m) { }
        // RVA: 0x09E0A50C  token: 0x6000023
        private static System.Int32 <Create>m__3(System.Reflection.MethodInfo m) { }
        // RVA: 0x09E0A58C  token: 0x6000024
        private static System.Boolean <Create>m__4(System.Reflection.ParameterInfo p) { }
        // RVA: 0x011EC580  token: 0x6000025
        private static System.Delegate <Create>m__5(IFix.Core.GenericDelegate x) { }
        // RVA: 0x053E4DA0  token: 0x6000026
        private static System.Type <Create>m__6(System.Reflection.ParameterInfo pinfo) { }

    }

    // TypeToken: 0x2000013  // size: 0x30
    public class GenericDelegate
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10
        private System.Int32 methodId;  // 0x18
        private System.Object anonObj;  // 0x20
        private System.Boolean pushSelf;  // 0x28
        private System.Int32 extraArgNum;  // 0x2c

        // Methods
        // RVA: 0x09E0A748  token: 0x600002C
        private System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj) { }
        // RVA: 0x09E0A6B4  token: 0x600002D
        public System.Void Action() { }
        // RVA: -1  // generic def  token: 0x600002E
        public System.Void Action(T1 p1) { }
        // RVA: -1  // generic def  token: 0x600002F
        public System.Void Action(T1 p1, T2 p2) { }
        // RVA: -1  // generic def  token: 0x6000030
        public System.Void Action(T1 p1, T2 p2, T3 p3) { }
        // RVA: -1  // generic def  token: 0x6000031
        public System.Void Action(T1 p1, T2 p2, T3 p3, T4 p4) { }
        // RVA: -1  // generic def  token: 0x6000032
        public TResult Func() { }
        // RVA: -1  // generic def  token: 0x6000033
        public TResult Func(T1 p1) { }
        // RVA: -1  // generic def  token: 0x6000034
        public TResult Func(T1 p1, T2 p2) { }
        // RVA: -1  // generic def  token: 0x6000035
        public TResult Func(T1 p1, T2 p2, T3 p3) { }
        // RVA: -1  // generic def  token: 0x6000036
        public TResult Func(T1 p1, T2 p2, T3 p3, T4 p4) { }

    }

    // TypeToken: 0x2000016  // size: 0x18
    public class ObjectClone
    {
        // Fields
        private System.Reflection.MethodInfo memberwiseClone;  // 0x10

        // Methods
        // RVA: 0x09E0B114  token: 0x600003A
        public System.Void .ctor() { }
        // RVA: 0x09E0B0F0  token: 0x600003B
        public System.Object Clone(System.Object obj) { }

    }

    // TypeToken: 0x2000017  // size: 0x50
    public class ReflectionMethodInvoker
    {
        // Fields
        private System.Int32 paramCount;  // 0x10
        private System.Boolean hasThis;  // 0x14
        private System.Boolean hasReturn;  // 0x15
        private System.Boolean[] refFlags;  // 0x18
        private System.Boolean[] outFlags;  // 0x20
        private System.Type[] rawTypes;  // 0x28
        private System.Reflection.MethodBase method;  // 0x30
        private System.Reflection.ConstructorInfo ctor;  // 0x38
        private System.Type returnType;  // 0x40
        private System.Boolean isNullableHasValue;  // 0x48
        private System.Boolean isNullableValue;  // 0x49

        // Methods
        // RVA: 0x09E11880  token: 0x600003C
        public System.Void .ctor(System.Reflection.MethodBase method) { }
        // RVA: 0x09E1098C  token: 0x600003D
        public System.Void Invoke(IFix.Core.VirtualMachine virtualMachine, IFix.Core.Call& call, System.Boolean isInstantiate) { }

    }

    // TypeToken: 0x2000018  // size: 0x20
    public sealed struct UnmanagedStack
    {
        // Fields
        public IFix.Core.Value* Base;  // 0x10
        public IFix.Core.Value* Top;  // 0x18

    }

    // TypeToken: 0x2000019  // size: 0x30
    public class ThreadStackInfo
    {
        // Fields
        public IFix.Core.UnmanagedStack* UnmanagedStack;  // 0x10
        public System.Object[] ManagedStack;  // 0x18
        private System.IntPtr evaluationStackHandler;  // 0x20
        private System.IntPtr unmanagedStackHandler;  // 0x28
        private static System.LocalDataStoreSlot localSlot;  // static @ 0x0

        // Properties
        IFix.Core.ThreadStackInfo Stack { get; /* RVA: 0x09E11DE8 */ }

        // Methods
        // RVA: 0x09E11D50  token: 0x600003E
        public System.Void .ctor() { }
        // RVA: 0x09E11D00  token: 0x6000040
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001A  // size: 0x10
    public static class EvaluationStackOperation
    {
        // Methods
        // RVA: 0x09E08AD8  token: 0x6000041
        private static System.Void UnboxPrimitive(IFix.Core.Value* evaluationStackPointer, System.Object obj, System.Type type) { }
        // RVA: 0x09E09354  token: 0x6000042
        private static System.Object mGet(System.Boolean isArray, System.Object root, System.Int32 layer, System.Int32[] fieldIdList, System.Reflection.FieldInfo[] fieldInfos, System.Collections.Generic.Dictionary<System.Int32,IFix.Core.NewFieldInfo> newFieldInfos) { }
        // RVA: 0x09E09570  token: 0x6000043
        private static System.Void mSet(System.Boolean isArray, System.Object root, System.Object val, System.Int32 layer, System.Int32[] fieldIdList, System.Reflection.FieldInfo[] fieldInfos, System.Collections.Generic.Dictionary<System.Int32,IFix.Core.NewFieldInfo> newFieldInfos) { }
        // RVA: 0x09E07EDC  token: 0x6000044
        private static System.Object ToObject(IFix.Core.Value* evaluationStackBase, IFix.Core.Value* evaluationStackPointer, System.Object[] managedStack, System.Type type, IFix.Core.VirtualMachine virtualMachine, System.Boolean valueTypeClone) { }
        // RVA: 0x09E07B74  token: 0x6000045
        public static System.Void PushObject(IFix.Core.Value* evaluationStackBase, IFix.Core.Value* evaluationStackPointer, System.Object[] managedStack, System.Object obj, System.Type type) { }
        // RVA: 0x09E08FAC  token: 0x6000046
        public static System.Void UpdateReference(IFix.Core.Value* evaluationStackBase, IFix.Core.Value* evaluationStackPointer, System.Object[] managedStack, System.Object obj, IFix.Core.VirtualMachine virtualMachine, System.Type type) { }

    }

    // TypeToken: 0x200001B  // size: 0x38
    public sealed struct Call
    {
        // Fields
        private IFix.Core.Value* argumentBase;  // 0x10
        private IFix.Core.Value* evaluationStackBase;  // 0x18
        private System.Object[] managedStack;  // 0x20
        private IFix.Core.Value* currentTop;  // 0x28
        private IFix.Core.Value** topWriteBack;  // 0x30

        // Methods
        // RVA: 0x09E07448  token: 0x6000047
        public static IFix.Core.Call Begin() { }
        // RVA: 0x09E073C8  token: 0x6000048
        private static IFix.Core.Call BeginForStack(IFix.Core.ThreadStackInfo stack) { }
        // RVA: 0x09E076A8  token: 0x6000049
        public System.Void PushBoolean(System.Boolean b) { }
        // RVA: 0x09E0750C  token: 0x600004A
        public System.Boolean GetBoolean(System.Int32 offset) { }
        // RVA: 0x09E076D8  token: 0x600004B
        public System.Void PushByte(System.Byte b) { }
        // RVA: 0x09E07540  token: 0x600004C
        public System.Byte GetByte(System.Int32 offset) { }
        // RVA: 0x09E07540  token: 0x600004D
        public System.SByte GetSByte(System.Int32 offset) { }
        // RVA: 0x09E07718  token: 0x600004E
        public System.Void PushInt16(System.Int16 s) { }
        // RVA: 0x09E07540  token: 0x600004F
        public System.Int16 GetInt16(System.Int32 offset) { }
        // RVA: 0x09E076E4  token: 0x6000050
        public System.Void PushChar(System.Char c) { }
        // RVA: 0x09E07540  token: 0x6000051
        public System.Char GetChar(System.Int32 offset) { }
        // RVA: 0x09E076E4  token: 0x6000052
        public System.Void PushUInt16(System.UInt16 us) { }
        // RVA: 0x09E07540  token: 0x6000053
        public System.UInt16 GetUInt16(System.Int32 offset) { }
        // RVA: 0x09E07724  token: 0x6000054
        public System.Void PushInt32(System.Int32 i) { }
        // RVA: 0x09E07578  token: 0x6000055
        public System.Int32 GetInt32(System.Int32 offset) { }
        // RVA: 0x09E078D0  token: 0x6000056
        public System.Void PushUInt32(System.UInt32 ui) { }
        // RVA: 0x09E07540  token: 0x6000057
        public System.UInt32 GetUInt32(System.Int32 offset) { }
        // RVA: 0x09E07754  token: 0x6000058
        public System.Void PushInt64(System.Int64 i) { }
        // RVA: 0x09E075A8  token: 0x6000059
        public System.Int64 GetInt64(System.Int32 offset) { }
        // RVA: 0x09E078D8  token: 0x600005A
        public System.Void PushUInt64(System.UInt64 i) { }
        // RVA: 0x09E075D8  token: 0x600005B
        public System.UInt64 GetUInt64(System.Int32 offset) { }
        // RVA: 0x09E078A8  token: 0x600005C
        public System.Void PushSingle(System.Single f) { }
        // RVA: 0x09E07678  token: 0x600005D
        public System.Single GetSingle(System.Int32 offset) { }
        // RVA: 0x09E076F0  token: 0x600005E
        public System.Void PushDouble(System.Double d) { }
        // RVA: 0x09E07548  token: 0x600005F
        public System.Double GetDouble(System.Int32 offset) { }
        // RVA: 0x09E07784  token: 0x6000060
        public System.Void PushIntPtr(System.IntPtr i) { }
        // RVA: 0x09E075D8  token: 0x6000061
        public System.IntPtr GetIntPtr(System.Int32 offset) { }
        // RVA: 0x09E077CC  token: 0x6000062
        public System.Void PushObject(System.Object o) { }
        // RVA: 0x09E078E0  token: 0x6000063
        public System.Void PushValueType(System.Object o) { }
        // RVA: 0x09E075E0  token: 0x6000064
        public System.Object GetObject(System.Int32 offset) { }
        // RVA: -1  // generic def  token: 0x6000065
        public T GetAsType(System.Int32 offset) { }
        // RVA: 0x09E07790  token: 0x6000066
        public System.Void PushObjectAsResult(System.Object obj, System.Type type) { }
        // RVA: 0x09E07868  token: 0x6000067
        public System.Void PushRef(System.Int32 offset) { }
        // RVA: 0x09E0797C  token: 0x6000068
        public System.Void UpdateReference(System.Int32 offset, System.Object obj, IFix.Core.VirtualMachine virtualMachine, System.Type type) { }

    }

    // TypeToken: 0x200001D  // size: 0x10
    public static class Utils
    {
        // Fields
        private static System.Collections.Generic.Dictionary<System.Type,System.Reflection.MethodInfo> delegateAdptCache;  // static @ 0x0

        // Methods
        // RVA: 0x09E12020  token: 0x600006A
        public static System.Boolean IsAssignable(System.Reflection.MethodInfo delegateMethod, System.Reflection.MethodInfo method) { }
        // RVA: 0x09E1218C  token: 0x600006B
        public static System.Delegate TryAdapterToDelegate(System.Object obj, System.Type delegateType, System.String perfix) { }
        // RVA: 0x09E123F0  token: 0x600006C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001E  // size: 0x98
    public class RuntimeException : System.Exception
    {
        // Fields
        private System.Exception <Real>k__BackingField;  // 0x90

        // Properties
        System.Exception Real { get; /* RVA: 0x04D86220 */ set; /* RVA: 0x06402330 */ }

        // Methods
        // RVA: 0x09E11CB4  token: 0x600006D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001F  // size: 0x80
    public sealed class ExternInvoker : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x09725BB4  token: 0x6000070
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x01546280  token: 0x6000071
        public virtual System.Void Invoke(IFix.Core.VirtualMachine vm, IFix.Core.Call& call, System.Boolean isInstantiate) { }

    }

    // TypeToken: 0x2000020  // size: 0x20
    public class FieldAddr
    {
        // Fields
        public System.Object Object;  // 0x10
        public System.Int32[] FieldIdList;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000072
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000021  // size: 0x10
    public class Cleanner
    {
        // Fields
        private static System.Boolean start;  // static @ 0x0

        // Methods
        // RVA: 0x041E1670  token: 0x6000073
        public System.Void .ctor() { }
        // RVA: 0x09E07AE8  token: 0x6000074
        public static System.Void Start() { }
        // RVA: 0x09E07B3C  token: 0x6000075
        public static System.Void Stop() { }
        // RVA: 0x09E07A2C  token: 0x6000076
        protected virtual System.Void Finalize() { }

    }

    // TypeToken: 0x2000022  // size: 0x30
    public class NewFieldInfo
    {
        // Fields
        public System.String Name;  // 0x10
        public System.Type DeclaringType;  // 0x18
        public System.Type FieldType;  // 0x20
        public System.Int32 MethodId;  // 0x28
        private static readonly System.Int32 staticObjectKey;  // static @ 0x0
        private static readonly IFix.Core.ThreadStackInfo stack;  // static @ 0x8
        private static readonly System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.Dictionary<System.String,System.Object>> newFieldValues;  // static @ 0x10
        private static readonly System.Collections.Generic.Dictionary<System.Int32,System.WeakReference> objList;  // static @ 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000077
        public System.Void .ctor() { }
        // RVA: 0x09E0AAEC  token: 0x6000078
        private System.Object SetDefaultValue(System.Object obj) { }
        // RVA: 0x09E0AD80  token: 0x6000079
        public static System.Void Sweep() { }
        // RVA: 0x09E0A788  token: 0x600007A
        public System.Void CheckInit(IFix.Core.VirtualMachine virtualMachine, System.Object obj) { }
        // RVA: 0x09E0AA8C  token: 0x600007B
        public System.Int32 ObjectToIndex(System.Object obj) { }
        // RVA: 0x09E0A984  token: 0x600007C
        public System.Boolean HasInitialize(System.Object obj) { }
        // RVA: 0x09E0A898  token: 0x600007D
        public System.Object GetValue(System.Object obj) { }
        // RVA: 0x09E0AB64  token: 0x600007E
        public System.Void SetValue(System.Object obj, System.Object value) { }
        // RVA: 0x09E0AF8C  token: 0x600007F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000023  // size: 0x98
    public class VirtualMachine
    {
        // Fields
        private IFix.Core.ObjectClone objectClone;  // 0x10
        private IFix.Core.Instruction** unmanagedCodes;  // 0x18
        private IFix.Core.ExceptionHandler[][] exceptionHandlers;  // 0x20
        private System.Action onDispose;  // 0x28
        private IFix.Core.ExternInvoker[] externInvokers;  // 0x30
        private System.Collections.Generic.Dictionary<System.Reflection.MethodBase,IFix.Core.VirtualMachine.CalliInvokerInfo> calliInvokerCache;  // 0x38
        private System.Reflection.MethodBase[] externMethods;  // 0x40
        private System.Type[] externTypes;  // 0x48
        private System.String[] internStrings;  // 0x50
        private System.Reflection.FieldInfo[] fieldInfos;  // 0x58
        private System.Collections.Generic.Dictionary<System.Int32,IFix.Core.NewFieldInfo> newFieldInfos;  // 0x60
        private IFix.Core.AnonymousStoreyInfo[] anonymousStoreyInfos;  // 0x68
        private System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.Dictionary<System.Reflection.MethodInfo,System.Reflection.MethodInfo>> overrideCache;  // 0x70
        private System.Type[] staticFieldTypes;  // 0x78
        private System.Object[] staticFields;  // 0x80
        private System.Int32[] cctors;  // 0x88
        private IFix.Core.WrappersManager wrappersManager;  // 0x90
        public static System.Action<System.String> Info;  // static @ 0x0
        private static System.Action<System.String> <>f__mg$cache0;  // static @ 0x8

        // Properties
        IFix.Core.ExceptionHandler[][] ExceptionHandlers { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.Type[] ExternTypes { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }
        System.Reflection.MethodBase[] ExternMethods { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x09E13534 */ }
        System.String[] InternStrings { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x02FDAEA0 */ }
        System.Reflection.FieldInfo[] FieldInfos { set; /* RVA: 0x036E5590 */ }
        System.Collections.Generic.Dictionary<System.Int32,IFix.Core.NewFieldInfo> NewFieldInfos { set; /* RVA: 0x063CC164 */ }
        IFix.Core.AnonymousStoreyInfo[] AnonymousStoreyInfos { set; /* RVA: 0x0485F170 */ }
        System.Type[] StaticFieldTypes { set; /* RVA: 0x09E135A4 */ }
        System.Int32[] Cctors { set; /* RVA: 0x05395D30 */ }
        IFix.Core.WrappersManager WrappersManager { get; /* RVA: 0x04D86220 */ set; /* RVA: 0x06402330 */ }

        // Methods
        // RVA: 0x09E12634  token: 0x6000080
        private System.Void .ctor(IFix.Core.Instruction** unmanaged_codes, System.Action on_dispose) { }
        // RVA: 0x09E1252C  token: 0x6000090
        protected virtual System.Void Finalize() { }
        // RVA: 0x09E12DFC  token: 0x6000091
        private System.Void checkCctorExecute(System.Int32 fieldId, IFix.Core.Value* argumentBase, System.Object[] managedStack, IFix.Core.Value* evaluationStackBase) { }
        // RVA: 0x09E1384C  token: 0x6000092
        private System.Void store(IFix.Core.Value* stackBase, IFix.Core.Value* dst, IFix.Core.Value* src, System.Object[] managedStack) { }
        // RVA: 0x09E12E80  token: 0x6000093
        private System.Void copy(IFix.Core.Value* stackBase, IFix.Core.Value* dst, IFix.Core.Value* src, System.Object[] managedStack) { }
        // RVA: 0x09E133D8  token: 0x6000094
        private System.Void resolveStackReference(IFix.Core.Value* value, IFix.Core.Value* stackBase, System.Object[] managedStack) { }
        // RVA: 0x09E13620  token: 0x6000095
        private System.Byte* stackReferenceToPointer(IFix.Core.Value* value) { }
        // RVA: 0x09E13A7C  token: 0x6000096
        private System.Boolean tryConvertStackReferenceToPointer(IFix.Core.Value* value) { }
        // RVA: 0x09E13258  token: 0x6000097
        private System.Byte* pointerValueToAddress(IFix.Core.Value* value, System.Object[] managedStack) { }
        // RVA: 0x09E13CB4  token: 0x6000098
        private System.Int64 valueToInt64(IFix.Core.Value* value) { }
        // RVA: 0x09E13BAC  token: 0x6000099
        private System.Single valueToFloat(IFix.Core.Value* value) { }
        // RVA: 0x09E13AA4  token: 0x600009A
        private System.Double valueToDouble(IFix.Core.Value* value) { }
        // RVA: 0x09E13634  token: 0x600009B
        private System.Void storeToRawPointer(System.Byte* destination, IFix.Core.Value* src, IFix.Core.Code code) { }
        // RVA: 0x09E13078  token: 0x600009C
        private System.Void loadFromRawPointer(System.Byte* source, IFix.Core.Value* dest, IFix.Core.Code code) { }
        // RVA: 0x09E124C4  token: 0x600009D
        public System.Void Execute(System.Int32 methodIndex, IFix.Core.Call& call, System.Int32 argsCount, System.Int32 refCount) { }
        // RVA: 0x09E1247C  token: 0x600009E
        public IFix.Core.Value* Execute(System.Int32 methodIndex, IFix.Core.Value* argumentBase, System.Object[] managedStack, IFix.Core.Value* evaluationStackBase, System.Int32 argsCount) { }
        // RVA: 0x09E139F8  token: 0x600009F
        private System.Void throwRuntimeException(System.Exception e, System.Boolean bWrap) { }
        // RVA: 0x09E12FC0  token: 0x60000A0
        private IFix.Core.ExceptionHandler getExceptionHandler(System.Int32 methodIndex, System.Type exceptionType, System.Int32 pc) { }
        // RVA: 0x09E12760  token: 0x60000A1
        private System.Void arrayGet(System.Object obj, System.Int32 idx, IFix.Core.Value* val, System.Object[] managedStack, IFix.Core.Value* evaluationStackBase) { }
        // RVA: 0x09E12ACC  token: 0x60000A2
        private System.Void arraySet(System.Object obj, System.Int32 idx, IFix.Core.Value* val, System.Object[] managedStack, IFix.Core.Value* evaluationStackBase) { }
        // RVA: 0x09E125BC  token: 0x60000A3
        public static System.Void _Info(System.String a) { }
        // RVA: 0x09DFAAD8  token: 0x60000A4
        public IFix.Core.Value* Execute(IFix.Core.Instruction* pc, IFix.Core.Value* argumentBase, System.Object[] managedStack, IFix.Core.Value* evaluationStackBase, System.Int32 argsCount, System.Int32 methodIndex, System.Int32 refCount, IFix.Core.Value** topWriteBack) { }
        // RVA: 0x03AF9D30  token: 0x60000A5
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000025
    public interface WrappersManager
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60000A7
        public virtual System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon) { }
        // RVA: -1  // abstract  token: 0x60000A8
        public virtual IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: -1  // abstract  token: 0x60000A9
        public virtual System.Object CreateWrapper(System.Int32 id) { }
        // RVA: -1  // abstract  token: 0x60000AA
        public virtual System.Object InitWrapperArray(System.Int32 len) { }

    }

}

namespace Unity.IL2CPP.CompilerServices
{

    // TypeToken: 0x2000014  // size: 0x14
    public sealed struct Option
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Unity.IL2CPP.CompilerServices.Option NullChecks;  // const
        public static Unity.IL2CPP.CompilerServices.Option ArrayBoundsChecks;  // const
        public static Unity.IL2CPP.CompilerServices.Option DivideByZeroChecks;  // const

    }

    // TypeToken: 0x2000015  // size: 0x20
    public class Il2CppSetOptionAttribute : System.Attribute
    {
        // Fields
        private Unity.IL2CPP.CompilerServices.Option <Option>k__BackingField;  // 0x10
        private System.Object <Value>k__BackingField;  // 0x18

        // Properties
        Unity.IL2CPP.CompilerServices.Option Option { set; /* RVA: 0x014F51F0 */ }
        System.Object Value { set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x05391D70  token: 0x6000037
        private System.Void .ctor(Unity.IL2CPP.CompilerServices.Option option, System.Object value) { }

    }

}

