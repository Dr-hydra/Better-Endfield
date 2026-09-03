// ========================================================
// Dumped by @desirepro
// Assembly: Base.Beyond.dll
// Classes:  20
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000010  // size: 0x10
    public static class $BurstDirectCallInitializer
    {
        // Methods
        // RVA: 0x04D84E70  token: 0x600006B
        private static System.Void Initialize() { }

    }

namespace Beyond
{

    // TypeToken: 0x2000006  // size: 0x10
    public static class CollectionUtils
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000008
        public static System.Void AddRange(System.Collections.Generic.HashSet<T> hashSet, System.Collections.Generic.IEnumerable<T> range) { }
        // RVA: -1  // generic def  token: 0x6000009
        public static System.Collections.Generic.HashSet<T> DeepClone(System.Collections.Generic.HashSet<T> hashSet) { }

    }

    // TypeToken: 0x2000007  // size: 0x10
    public class DebugAssert
    {
        // Fields
        private static System.String NODE_NAME;  // const

        // Methods
        // RVA: 0x041E1670  token: 0x600000A
        public static System.Void IsTrue(System.Boolean condition) { }
        // RVA: 0x041E1670  token: 0x600000B
        public static System.Void IsTrue(System.Boolean condition, System.String message) { }
        // RVA: 0x041E1670  token: 0x600000C
        public static System.Void IsFalse(System.Boolean condition) { }
        // RVA: 0x041E1670  token: 0x600000D
        public static System.Void IsFalse(System.Boolean condition, System.String message) { }
        // RVA: -1  // generic def  token: 0x600000E
        public static System.Void IsNotNull(T value) { }
        // RVA: -1  // generic def  token: 0x600000F
        public static System.Void IsNull(T value) { }
        // RVA: 0x041E1670  token: 0x6000010
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000008  // size: 0x10
    public static class EnumUtils
    {
        // Fields
        private static readonly System.Collections.Generic.Dictionary<System.Type,System.Int32> _enumCountCache;  // static @ 0x0

        // Methods
        // RVA: -1  // generic def  token: 0x6000011
        public static System.Int32 GetEnumCount() { }
        // RVA: -1  // generic def  token: 0x6000012
        public static TEnum FromIntNoGC(System.Int32 value) { }
        // RVA: -1  // generic def  token: 0x6000013
        public static TEnum IntToEnumNoGC(System.Int32 value) { }
        // RVA: 0x06366450  token: 0x6000014
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000009
    public class StaticFastLookupCollection`2
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<TKey,TValue> m_dict;  // 0x0
        private readonly System.Collections.Generic.List<TValue> m_list;  // 0x0

        // Properties
        System.Collections.Generic.IReadOnlyList<TValue> items { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000015
        public System.Boolean TryAdd(TKey key, TValue value) { }
        // RVA: -1  // not resolved  token: 0x6000016
        public System.Boolean TryGetValue(TKey key, TValue& value) { }
        // RVA: -1  // not resolved  token: 0x6000017
        public System.Boolean ContainsKey(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000018
        public System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x600001A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000A
    public class DynamicFastLookupCollection`2
    {
        // Fields
        private System.Collections.Generic.Dictionary<TKey,TValue> m_dict;  // 0x0
        private System.Collections.Generic.List<System.ValueTuple<TKey,TValue>> m_list;  // 0x0
        private readonly System.Collections.Generic.IEqualityComparer<TKey> m_keyComparer;  // 0x0

        // Properties
        System.Collections.Generic.IReadOnlyList<System.ValueTuple<TKey,TValue>> items { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600001B
        public System.Boolean TryAdd(TKey key, TValue value) { }
        // RVA: -1  // not resolved  token: 0x600001C
        public System.Void Add(TKey key, TValue value) { }
        // RVA: -1  // not resolved  token: 0x600001D
        public System.Int32 Count() { }
        // RVA: -1  // not resolved  token: 0x600001E
        public System.Boolean TryGetValue(TKey key, TValue& value) { }
        // RVA: -1  // not resolved  token: 0x600001F
        public System.Boolean ContainsKey(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000020
        public System.Boolean Remove(TKey key, TValue& value) { }
        // RVA: -1  // not resolved  token: 0x6000021
        public System.Boolean Remove(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000022
        public System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000024
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000C  // size: 0x80
    public sealed class _EqualsIgnoreCase_0000002C$PostfixBurstDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x06367DDC  token: 0x6000056
        public System.Void .ctor(System.Object , System.IntPtr ) { }
        // RVA: 0x014F3120  token: 0x6000057
        public virtual System.Boolean Invoke(System.Int16* str1, System.Int16* str2, System.Int32 len) { }
        // RVA: 0x06367D8C  token: 0x6000058
        public virtual System.IAsyncResult BeginInvoke(System.Int16* str1, System.Int16* str2, System.Int32 len, System.AsyncCallback , System.Object ) { }
        // RVA: 0x053DE668  token: 0x6000059
        public virtual System.Boolean EndInvoke(System.IAsyncResult ) { }

    }

    // TypeToken: 0x200000D  // size: 0x10
    public static class _EqualsIgnoreCase_0000002C$BurstDirectCall
    {
        // Fields
        private static System.IntPtr Pointer;  // static @ 0x0
        private static System.IntPtr DeferredCompilation;  // static @ 0x8

        // Methods
        // RVA: 0x0307BA90  token: 0x600005A
        private static System.Void GetFunctionPointerDiscard(System.IntPtr& ) { }
        // RVA: 0x0307B390  token: 0x600005B
        private static System.IntPtr GetFunctionPointer() { }
        // RVA: 0x03FAEBF0  token: 0x600005C
        public static System.Void Constructor() { }
        // RVA: 0x041E1670  token: 0x600005D
        public static System.Void Initialize() { }
        // RVA: 0x03FAEB20  token: 0x600005E
        private static System.Void .cctor() { }
        // RVA: 0x0307B310  token: 0x600005F
        public static System.Boolean Invoke(System.Int16* str1, System.Int16* str2, System.Int32 len) { }

    }

    // TypeToken: 0x200000B  // size: 0x10
    public static class StringUtils
    {
        // Methods
        // RVA: 0x063676BC  token: 0x6000025
        public static System.String LowerFirst(System.String str, System.Boolean includeAllStartUppers) { }
        // RVA: 0x06367A28  token: 0x6000026
        public static System.String TrimStartLength(System.String str, System.String lengthString) { }
        // RVA: 0x063679B0  token: 0x6000027
        public static System.String TrimStartLength(System.String str, System.Int32 length) { }
        // RVA: 0x042BAC90  token: 0x6000028
        public static System.String TrimEndLength(System.String str, System.String lengthString) { }
        // RVA: 0x06367938  token: 0x6000029
        public static System.String TrimEndLength(System.String str, System.Int32 length) { }
        // RVA: 0x06367AAC  token: 0x600002A
        public static System.String Trim(System.String str, System.String startLengthString, System.String endLengthString) { }
        // RVA: 0x0307B110  token: 0x600002B
        public static System.Boolean EqualsIgnoreCase(System.String strA, System.String strB) { }
        // RVA: 0x0307B270  token: 0x600002C
        private static System.Boolean _EqualsIgnoreCase(System.Int16* str1, System.Int16* str2, System.Int32 len) { }
        // RVA: 0x033B8840  token: 0x600002D
        public static System.String SafeValue(System.String str) { }
        // RVA: 0x0636753C  token: 0x600002E
        public static System.Int32 HexToInt(System.Char hex) { }
        // RVA: 0x06367884  token: 0x600002F
        public static System.Int32 StringHexToInt(System.String s) { }
        // RVA: 0x02F271D0  token: 0x6000030
        public static Cysharp.Text.Utf16ValueStringBuilder CreateZStringBuilder() { }
        // RVA: -1  // generic def  token: 0x6000031
        public static System.String ZConcat(T1 arg1, T2 arg2) { }
        // RVA: -1  // generic def  token: 0x6000032
        public static System.String ZConcat(T1 arg1, T2 arg2, T3 arg3) { }
        // RVA: -1  // generic def  token: 0x6000033
        public static System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4) { }
        // RVA: -1  // generic def  token: 0x6000034
        public static System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) { }
        // RVA: -1  // generic def  token: 0x6000035
        public static System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) { }
        // RVA: -1  // generic def  token: 0x6000036
        public static System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) { }
        // RVA: -1  // generic def  token: 0x6000037
        public static System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8) { }
        // RVA: -1  // generic def  token: 0x6000038
        public static System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9) { }
        // RVA: -1  // generic def  token: 0x6000039
        public static System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10) { }
        // RVA: -1  // generic def  token: 0x600003A
        public static System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11) { }
        // RVA: -1  // generic def  token: 0x600003B
        public static System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12) { }
        // RVA: -1  // generic def  token: 0x600003C
        public static System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13) { }
        // RVA: -1  // generic def  token: 0x600003D
        public static System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14) { }
        // RVA: -1  // generic def  token: 0x600003E
        public static System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15) { }
        // RVA: -1  // generic def  token: 0x600003F
        public static System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16) { }
        // RVA: -1  // generic def  token: 0x6000040
        public static System.String ZFormat(System.String format, T1 arg1) { }
        // RVA: -1  // generic def  token: 0x6000041
        public static System.String ZFormat(System.String format, T1 arg1, T2 arg2) { }
        // RVA: -1  // generic def  token: 0x6000042
        public static System.String ZFormat(System.String format, T1 arg1, T2 arg2, T3 arg3) { }
        // RVA: -1  // generic def  token: 0x6000043
        public static System.String ZFormat(System.String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4) { }
        // RVA: -1  // generic def  token: 0x6000044
        public static System.String ZFormat(System.String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) { }
        // RVA: -1  // generic def  token: 0x6000045
        public static System.String ZFormat(System.String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) { }
        // RVA: -1  // generic def  token: 0x6000046
        public static System.String ZFormat(System.String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) { }
        // RVA: -1  // generic def  token: 0x6000047
        public static System.String ZFormat(System.String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8) { }
        // RVA: -1  // generic def  token: 0x6000048
        public static System.String ZJoin(System.Char separator, T[] values) { }
        // RVA: -1  // generic def  token: 0x6000049
        public static System.String ZJoin(System.Char separator, System.Collections.Generic.List<T> values) { }
        // RVA: -1  // generic def  token: 0x600004A
        public static System.String ZJoin(System.Char separator, System.Collections.Generic.IEnumerable<T> values) { }
        // RVA: -1  // generic def  token: 0x600004B
        public static System.String ZJoin(System.Char separator, System.Collections.Generic.ICollection<T> values) { }
        // RVA: -1  // generic def  token: 0x600004C
        public static System.String ZJoin(System.Char separator, System.Collections.Generic.IList<T> values) { }
        // RVA: -1  // generic def  token: 0x600004D
        public static System.String ZJoin(System.Char separator, System.Collections.Generic.IReadOnlyList<T> values) { }
        // RVA: -1  // generic def  token: 0x600004E
        public static System.String ZJoin(System.Char separator, System.Collections.Generic.IReadOnlyCollection<T> values) { }
        // RVA: -1  // generic def  token: 0x600004F
        public static System.String ZJoin(System.String separator, System.Collections.Generic.List<T> values) { }
        // RVA: -1  // generic def  token: 0x6000050
        public static System.String ZJoin(System.String separator, System.Collections.Generic.IEnumerable<T> values) { }
        // RVA: -1  // generic def  token: 0x6000051
        public static System.String ZJoin(System.String separator, System.Collections.Generic.ICollection<T> values) { }
        // RVA: -1  // generic def  token: 0x6000052
        public static System.String ZJoin(System.String separator, System.Collections.Generic.IList<T> values) { }
        // RVA: -1  // generic def  token: 0x6000053
        public static System.String ZJoin(System.String separator, System.Collections.Generic.IReadOnlyList<T> values) { }
        // RVA: -1  // generic def  token: 0x6000054
        public static System.String ZJoin(System.String separator, System.Collections.Generic.IReadOnlyCollection<T> values) { }
        // RVA: 0x063663F0  token: 0x6000055
        public static System.Boolean _EqualsIgnoreCase$BurstManaged(System.Int16* str1, System.Int16* str2, System.Int32 len) { }

    }

}

namespace Beyond.Base
{

    // TypeToken: 0x200000F
    public interface IDisposeEnumerator
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600006A
        public virtual System.Void Dispose() { }

    }

}

namespace Beyond.Security
{

    // TypeToken: 0x200000E  // size: 0x10
    public class IOOO000iiI
    {
        // Fields
        public static System.String FONT_PATH;  // const

        // Methods
        // RVA: 0x063671E0  token: 0x6000060
        public static System.String Encrypt(System.String text, System.String key) { }
        // RVA: 0x0636725C  token: 0x6000061
        public static System.String IOoOIiII(System.String encryptText, System.String key) { }
        // RVA: 0x06367154  token: 0x6000062
        public static System.String EncryptBytes(System.Byte[] bytes, System.String key) { }
        // RVA: 0x063674B4  token: 0x6000063
        private static System.Byte[] _IooiIIO(System.String encryptText, System.String key) { }
        // RVA: 0x0307C380  token: 0x6000064
        public static System.Int32 IooiIIO(System.String encryptText, System.Span<System.Byte> result, System.String key) { }
        // RVA: 0x063672E0  token: 0x6000065
        private static System.Byte[] _Encrypt(System.Byte[] data, System.String key) { }
        // RVA: 0x02F800B0  token: 0x6000066
        private static System.Void _IIiiIIIoO(System.ReadOnlySpan<System.Byte> data, System.String key, System.Span<System.Byte> result) { }
        // RVA: 0x063673C0  token: 0x6000067
        private static System.Byte[] _IIiiIIIoO(System.Byte[] data, System.String key) { }
        // RVA: 0x0307BC20  token: 0x6000068
        public static System.Void FromBase64String(System.String text, System.Span<System.Byte> buffer, System.Int32& count) { }
        // RVA: 0x041E1670  token: 0x6000069
        public System.Void .ctor() { }

    }

}

namespace IFix
{

    // TypeToken: 0x2000011  // size: 0x28
    public class ILFixDynamicMethodWrapper
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10
        private System.Int32 methodId;  // 0x18
        private System.Object anonObj;  // 0x20
        public static IFix.ILFixDynamicMethodWrapper[] wrapperArray;  // static @ 0x0

        // Methods
        // RVA: 0x032B2C30  token: 0x600006C
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj) { }
        // RVA: 0x063664A8  token: 0x600006D
        public System.String __Gen_Wrap_0(System.Object P0, System.Boolean P1) { }
        // RVA: 0x06366900  token: 0x600006E
        public System.String __Gen_Wrap_1(System.Object P0, System.Object P1) { }
        // RVA: 0x063669F0  token: 0x600006F
        public System.String __Gen_Wrap_2(System.Object P0, System.Int32 P1) { }
        // RVA: 0x06366ACC  token: 0x6000070
        public System.String __Gen_Wrap_3(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x06366BC4  token: 0x6000071
        public System.Boolean __Gen_Wrap_4(System.Object P0, System.Object P1) { }
        // RVA: 0x06366CB0  token: 0x6000072
        public System.String __Gen_Wrap_5(System.Object P0) { }
        // RVA: 0x06366D84  token: 0x6000073
        public System.Int32 __Gen_Wrap_6(System.Char P0) { }
        // RVA: 0x06366E40  token: 0x6000074
        public System.Int32 __Gen_Wrap_7(System.Object P0) { }
        // RVA: 0x06366EFC  token: 0x6000075
        public Cysharp.Text.Utf16ValueStringBuilder __Gen_Wrap_8() { }
        // RVA: 0x06366FD0  token: 0x6000076
        public System.Byte[] __Gen_Wrap_9(System.Object P0, System.Object P1) { }
        // RVA: 0x06366584  token: 0x6000077
        public System.Int32 __Gen_Wrap_10(System.Object P0, System.Span<System.Byte> P1, System.Object P2) { }
        // RVA: 0x063666A4  token: 0x6000078
        public System.Void __Gen_Wrap_11(System.ReadOnlySpan<System.Byte> P0, System.Object P1, System.Span<System.Byte> P2) { }
        // RVA: 0x063667D0  token: 0x6000079
        public System.Void __Gen_Wrap_12(System.Object P0, System.Span<System.Byte> P1, System.Int32& P2) { }
        // RVA: 0x04D7A410  token: 0x600007A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000012  // size: 0x40
    public class ILFixInterfaceBridge : IFix.Core.AnonymousStorey
    {
        // Methods
        // RVA: 0x063670C0  token: 0x600007B
        public System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x041E1670  token: 0x600007C
        public System.Void RefAsyncBuilderStartMethod() { }

    }

    // TypeToken: 0x2000013  // size: 0x18
    public class WrappersManagerImpl : IFix.Core.WrappersManager
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x600007D
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x06367CD8  token: 0x600007E
        public static IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id) { }
        // RVA: 0x0307B220  token: 0x600007F
        public static System.Boolean IsPatched(System.Int32 id) { }
        // RVA: 0x06367BE8  token: 0x6000080
        public virtual System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon) { }
        // RVA: 0x06367C7C  token: 0x6000081
        public virtual System.Object CreateWrapper(System.Int32 id) { }
        // RVA: 0x06367D2C  token: 0x6000082
        public virtual System.Object InitWrapperArray(System.Int32 len) { }
        // RVA: 0x06367B60  token: 0x6000083
        public virtual IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine) { }

    }

    // TypeToken: 0x2000014  // size: 0x14
    public sealed struct IDMAP0
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static IFix.IDMAP0 Beyond-StringUtils-LowerFirst0;  // const
        public static IFix.IDMAP0 Beyond-StringUtils-TrimStartLength0;  // const
        public static IFix.IDMAP0 Beyond-StringUtils-TrimStartLength1;  // const
        public static IFix.IDMAP0 Beyond-StringUtils-TrimEndLength0;  // const
        public static IFix.IDMAP0 Beyond-StringUtils-TrimEndLength1;  // const
        public static IFix.IDMAP0 Beyond-StringUtils-Trim0;  // const
        public static IFix.IDMAP0 Beyond-StringUtils-EqualsIgnoreCase0;  // const
        public static IFix.IDMAP0 Beyond-StringUtils-SafeValue0;  // const
        public static IFix.IDMAP0 Beyond-StringUtils-HexToInt0;  // const
        public static IFix.IDMAP0 Beyond-StringUtils-StringHexToInt0;  // const
        public static IFix.IDMAP0 Beyond-StringUtils-CreateZStringBuilder0;  // const
        public static IFix.IDMAP0 Beyond-Security-IOOO000iiI-_Encrypt0;  // const
        public static IFix.IDMAP0 Beyond-Security-IOOO000iiI-EncryptBytes0;  // const
        public static IFix.IDMAP0 Beyond-Security-IOOO000iiI-Encrypt0;  // const
        public static IFix.IDMAP0 Beyond-Security-IOOO000iiI-_IIiiIIIoO0;  // const
        public static IFix.IDMAP0 Beyond-Security-IOOO000iiI-_IooiIIO0;  // const
        public static IFix.IDMAP0 Beyond-Security-IOOO000iiI-IOoOIiII0;  // const
        public static IFix.IDMAP0 Beyond-Security-IOOO000iiI-IooiIIO0;  // const
        public static IFix.IDMAP0 Beyond-Security-IOOO000iiI-_IIiiIIIoO1;  // const
        public static IFix.IDMAP0 Beyond-Security-IOOO000iiI-FromBase64String0;  // const

    }

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
    public sealed class IsUnmanagedAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000002
        public System.Void .ctor() { }

    }

}

namespace Unity.IL2CPP.CompilerServices
{

    // TypeToken: 0x2000004  // size: 0x14
    public sealed struct Option
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Unity.IL2CPP.CompilerServices.Option NullChecks;  // const
        public static Unity.IL2CPP.CompilerServices.Option ArrayBoundsChecks;  // const
        public static Unity.IL2CPP.CompilerServices.Option DivideByZeroChecks;  // const

    }

    // TypeToken: 0x2000005  // size: 0x20
    public class Il2CppSetOptionAttribute : System.Attribute
    {
        // Fields
        private Unity.IL2CPP.CompilerServices.Option <Option>k__BackingField;  // 0x10
        private System.Object <Value>k__BackingField;  // 0x18

        // Properties
        Unity.IL2CPP.CompilerServices.Option Option { get; /* RVA: 0x02B2E2D0 */ set; /* RVA: 0x014F51F0 */ }
        System.Object Value { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x04A41B50  token: 0x6000007
        public System.Void .ctor(Unity.IL2CPP.CompilerServices.Option option, System.Object value) { }

    }

}

