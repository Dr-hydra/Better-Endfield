// ========================================================
// Dumped by @desirepro
// Assembly: Base.Beyond.dll
// Classes:  20
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

CLASS: $BurstDirectCallInitializer
TYPE:  static class
TOKEN: 0x2000010
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04D84E70  token=0x600006B  System.Void Initialize()
END_CLASS

CLASS: Beyond.CollectionUtils
TYPE:  static class
TOKEN: 0x2000006
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000008  System.Void AddRange(System.Collections.Generic.HashSet<T> hashSet, System.Collections.Generic.IEnumerable<T> range)
  RVA=-1  // generic def  token=0x6000009  System.Collections.Generic.HashSet<T> DeepClone(System.Collections.Generic.HashSet<T> hashSet)
END_CLASS

CLASS: Beyond.DebugAssert
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x10
FIELDS:
  private   static  System.String                   NODE_NAME  // const
METHODS:
  RVA=0x041E1670  token=0x600000A  System.Void IsTrue(System.Boolean condition)
  RVA=0x041E1670  token=0x600000B  System.Void IsTrue(System.Boolean condition, System.String message)
  RVA=0x041E1670  token=0x600000C  System.Void IsFalse(System.Boolean condition)
  RVA=0x041E1670  token=0x600000D  System.Void IsFalse(System.Boolean condition, System.String message)
  RVA=-1  // generic def  token=0x600000E  System.Void IsNotNull(T value)
  RVA=-1  // generic def  token=0x600000F  System.Void IsNull(T value)
  RVA=0x041E1670  token=0x6000010  System.Void .ctor()
END_CLASS

CLASS: Beyond.EnumUtils
TYPE:  static class
TOKEN: 0x2000008
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Generic.Dictionary<System.Type,System.Int32>_enumCountCache  // static @ 0x0
METHODS:
  RVA=-1  // generic def  token=0x6000011  System.Int32 GetEnumCount()
  RVA=-1  // generic def  token=0x6000012  TEnum FromIntNoGC(System.Int32 value)
  RVA=-1  // generic def  token=0x6000013  TEnum IntToEnumNoGC(System.Int32 value)
  RVA=0x06366450  token=0x6000014  System.Void .cctor()
END_CLASS

CLASS: Beyond.StaticFastLookupCollection`2
TYPE:  class
TOKEN: 0x2000009
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<TKey,TValue>m_dict  // 0x0
  private   readonly System.Collections.Generic.List<TValue>m_list  // 0x0
PROPERTIES:
  items  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000015  System.Boolean TryAdd(TKey key, TValue value)
  RVA=-1  // not resolved  token=0x6000016  System.Boolean TryGetValue(TKey key, TValue& value)
  RVA=-1  // not resolved  token=0x6000017  System.Boolean ContainsKey(TKey key)
  RVA=-1  // not resolved  token=0x6000018  System.Void Clear()
  RVA=-1  // not resolved  token=0x600001A  System.Void .ctor()
END_CLASS

CLASS: Beyond.DynamicFastLookupCollection`2
TYPE:  class
TOKEN: 0x200000A
FIELDS:
  private           System.Collections.Generic.Dictionary<TKey,TValue>m_dict  // 0x0
  private           System.Collections.Generic.List<System.ValueTuple<TKey,TValue>>m_list  // 0x0
  private   readonly System.Collections.Generic.IEqualityComparer<TKey>m_keyComparer  // 0x0
PROPERTIES:
  items  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600001B  System.Boolean TryAdd(TKey key, TValue value)
  RVA=-1  // not resolved  token=0x600001C  System.Void Add(TKey key, TValue value)
  RVA=-1  // not resolved  token=0x600001D  System.Int32 Count()
  RVA=-1  // not resolved  token=0x600001E  System.Boolean TryGetValue(TKey key, TValue& value)
  RVA=-1  // not resolved  token=0x600001F  System.Boolean ContainsKey(TKey key)
  RVA=-1  // not resolved  token=0x6000020  System.Boolean Remove(TKey key, TValue& value)
  RVA=-1  // not resolved  token=0x6000021  System.Boolean Remove(TKey key)
  RVA=-1  // not resolved  token=0x6000022  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000024  System.Void .ctor()
END_CLASS

CLASS: Beyond._EqualsIgnoreCase_0000002C$PostfixBurstDelegate
TYPE:  sealed class
TOKEN: 0x200000C
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x06367DDC  token=0x6000056  System.Void .ctor(System.Object , System.IntPtr )
  RVA=0x014F3120  token=0x6000057  System.Boolean Invoke(System.Int16* str1, System.Int16* str2, System.Int32 len)
  RVA=0x06367D8C  token=0x6000058  System.IAsyncResult BeginInvoke(System.Int16* str1, System.Int16* str2, System.Int32 len, System.AsyncCallback , System.Object )
  RVA=0x053DE668  token=0x6000059  System.Boolean EndInvoke(System.IAsyncResult )
END_CLASS

CLASS: Beyond._EqualsIgnoreCase_0000002C$BurstDirectCall
TYPE:  static class
TOKEN: 0x200000D
SIZE:  0x10
FIELDS:
  private   static  System.IntPtr                   Pointer  // static @ 0x0
  private   static  System.IntPtr                   DeferredCompilation  // static @ 0x8
METHODS:
  RVA=0x0307BA90  token=0x600005A  System.Void GetFunctionPointerDiscard(System.IntPtr& )
  RVA=0x0307B390  token=0x600005B  System.IntPtr GetFunctionPointer()
  RVA=0x03FAEBF0  token=0x600005C  System.Void Constructor()
  RVA=0x041E1670  token=0x600005D  System.Void Initialize()
  RVA=0x03FAEB20  token=0x600005E  System.Void .cctor()
  RVA=0x0307B310  token=0x600005F  System.Boolean Invoke(System.Int16* str1, System.Int16* str2, System.Int32 len)
END_CLASS

CLASS: Beyond.StringUtils
TYPE:  static class
TOKEN: 0x200000B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x063676BC  token=0x6000025  System.String LowerFirst(System.String str, System.Boolean includeAllStartUppers)
  RVA=0x06367A28  token=0x6000026  System.String TrimStartLength(System.String str, System.String lengthString)
  RVA=0x063679B0  token=0x6000027  System.String TrimStartLength(System.String str, System.Int32 length)
  RVA=0x042BAC90  token=0x6000028  System.String TrimEndLength(System.String str, System.String lengthString)
  RVA=0x06367938  token=0x6000029  System.String TrimEndLength(System.String str, System.Int32 length)
  RVA=0x06367AAC  token=0x600002A  System.String Trim(System.String str, System.String startLengthString, System.String endLengthString)
  RVA=0x0307B110  token=0x600002B  System.Boolean EqualsIgnoreCase(System.String strA, System.String strB)
  RVA=0x0307B270  token=0x600002C  System.Boolean _EqualsIgnoreCase(System.Int16* str1, System.Int16* str2, System.Int32 len)
  RVA=0x033B8840  token=0x600002D  System.String SafeValue(System.String str)
  RVA=0x0636753C  token=0x600002E  System.Int32 HexToInt(System.Char hex)
  RVA=0x06367884  token=0x600002F  System.Int32 StringHexToInt(System.String s)
  RVA=0x02F271D0  token=0x6000030  Cysharp.Text.Utf16ValueStringBuilder CreateZStringBuilder()
  RVA=-1  // generic def  token=0x6000031  System.String ZConcat(T1 arg1, T2 arg2)
  RVA=-1  // generic def  token=0x6000032  System.String ZConcat(T1 arg1, T2 arg2, T3 arg3)
  RVA=-1  // generic def  token=0x6000033  System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
  RVA=-1  // generic def  token=0x6000034  System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
  RVA=-1  // generic def  token=0x6000035  System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
  RVA=-1  // generic def  token=0x6000036  System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
  RVA=-1  // generic def  token=0x6000037  System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
  RVA=-1  // generic def  token=0x6000038  System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
  RVA=-1  // generic def  token=0x6000039  System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
  RVA=-1  // generic def  token=0x600003A  System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
  RVA=-1  // generic def  token=0x600003B  System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
  RVA=-1  // generic def  token=0x600003C  System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
  RVA=-1  // generic def  token=0x600003D  System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
  RVA=-1  // generic def  token=0x600003E  System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
  RVA=-1  // generic def  token=0x600003F  System.String ZConcat(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
  RVA=-1  // generic def  token=0x6000040  System.String ZFormat(System.String format, T1 arg1)
  RVA=-1  // generic def  token=0x6000041  System.String ZFormat(System.String format, T1 arg1, T2 arg2)
  RVA=-1  // generic def  token=0x6000042  System.String ZFormat(System.String format, T1 arg1, T2 arg2, T3 arg3)
  RVA=-1  // generic def  token=0x6000043  System.String ZFormat(System.String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
  RVA=-1  // generic def  token=0x6000044  System.String ZFormat(System.String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
  RVA=-1  // generic def  token=0x6000045  System.String ZFormat(System.String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
  RVA=-1  // generic def  token=0x6000046  System.String ZFormat(System.String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
  RVA=-1  // generic def  token=0x6000047  System.String ZFormat(System.String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
  RVA=-1  // generic def  token=0x6000048  System.String ZJoin(System.Char separator, T[] values)
  RVA=-1  // generic def  token=0x6000049  System.String ZJoin(System.Char separator, System.Collections.Generic.List<T> values)
  RVA=-1  // generic def  token=0x600004A  System.String ZJoin(System.Char separator, System.Collections.Generic.IEnumerable<T> values)
  RVA=-1  // generic def  token=0x600004B  System.String ZJoin(System.Char separator, System.Collections.Generic.ICollection<T> values)
  RVA=-1  // generic def  token=0x600004C  System.String ZJoin(System.Char separator, System.Collections.Generic.IList<T> values)
  RVA=-1  // generic def  token=0x600004D  System.String ZJoin(System.Char separator, System.Collections.Generic.IReadOnlyList<T> values)
  RVA=-1  // generic def  token=0x600004E  System.String ZJoin(System.Char separator, System.Collections.Generic.IReadOnlyCollection<T> values)
  RVA=-1  // generic def  token=0x600004F  System.String ZJoin(System.String separator, System.Collections.Generic.List<T> values)
  RVA=-1  // generic def  token=0x6000050  System.String ZJoin(System.String separator, System.Collections.Generic.IEnumerable<T> values)
  RVA=-1  // generic def  token=0x6000051  System.String ZJoin(System.String separator, System.Collections.Generic.ICollection<T> values)
  RVA=-1  // generic def  token=0x6000052  System.String ZJoin(System.String separator, System.Collections.Generic.IList<T> values)
  RVA=-1  // generic def  token=0x6000053  System.String ZJoin(System.String separator, System.Collections.Generic.IReadOnlyList<T> values)
  RVA=-1  // generic def  token=0x6000054  System.String ZJoin(System.String separator, System.Collections.Generic.IReadOnlyCollection<T> values)
  RVA=0x063663F0  token=0x6000055  System.Boolean _EqualsIgnoreCase$BurstManaged(System.Int16* str1, System.Int16* str2, System.Int32 len)
END_CLASS

CLASS: Beyond.Base.IDisposeEnumerator
TYPE:  interface
TOKEN: 0x200000F
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600006A  System.Void Dispose()
END_CLASS

CLASS: Beyond.Security.IOOO000iiI
TYPE:  class
TOKEN: 0x200000E
SIZE:  0x10
FIELDS:
  public    static  System.String                   FONT_PATH  // const
METHODS:
  RVA=0x063671E0  token=0x6000060  System.String Encrypt(System.String text, System.String key)
  RVA=0x0636725C  token=0x6000061  System.String IOoOIiII(System.String encryptText, System.String key)
  RVA=0x06367154  token=0x6000062  System.String EncryptBytes(System.Byte[] bytes, System.String key)
  RVA=0x063674B4  token=0x6000063  System.Byte[] _IooiIIO(System.String encryptText, System.String key)
  RVA=0x0307C380  token=0x6000064  System.Int32 IooiIIO(System.String encryptText, System.Span<System.Byte> result, System.String key)
  RVA=0x063672E0  token=0x6000065  System.Byte[] _Encrypt(System.Byte[] data, System.String key)
  RVA=0x02F800B0  token=0x6000066  System.Void _IIiiIIIoO(System.ReadOnlySpan<System.Byte> data, System.String key, System.Span<System.Byte> result)
  RVA=0x063673C0  token=0x6000067  System.Byte[] _IIiiIIIoO(System.Byte[] data, System.String key)
  RVA=0x0307BC20  token=0x6000068  System.Void FromBase64String(System.String text, System.Span<System.Byte> buffer, System.Int32& count)
  RVA=0x041E1670  token=0x6000069  System.Void .ctor()
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x2000011
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x600006C  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x063664A8  token=0x600006D  System.String __Gen_Wrap_0(System.Object P0, System.Boolean P1)
  RVA=0x06366900  token=0x600006E  System.String __Gen_Wrap_1(System.Object P0, System.Object P1)
  RVA=0x063669F0  token=0x600006F  System.String __Gen_Wrap_2(System.Object P0, System.Int32 P1)
  RVA=0x06366ACC  token=0x6000070  System.String __Gen_Wrap_3(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x06366BC4  token=0x6000071  System.Boolean __Gen_Wrap_4(System.Object P0, System.Object P1)
  RVA=0x06366CB0  token=0x6000072  System.String __Gen_Wrap_5(System.Object P0)
  RVA=0x06366D84  token=0x6000073  System.Int32 __Gen_Wrap_6(System.Char P0)
  RVA=0x06366E40  token=0x6000074  System.Int32 __Gen_Wrap_7(System.Object P0)
  RVA=0x06366EFC  token=0x6000075  Cysharp.Text.Utf16ValueStringBuilder __Gen_Wrap_8()
  RVA=0x06366FD0  token=0x6000076  System.Byte[] __Gen_Wrap_9(System.Object P0, System.Object P1)
  RVA=0x06366584  token=0x6000077  System.Int32 __Gen_Wrap_10(System.Object P0, System.Span<System.Byte> P1, System.Object P2)
  RVA=0x063666A4  token=0x6000078  System.Void __Gen_Wrap_11(System.ReadOnlySpan<System.Byte> P0, System.Object P1, System.Span<System.Byte> P2)
  RVA=0x063667D0  token=0x6000079  System.Void __Gen_Wrap_12(System.Object P0, System.Span<System.Byte> P1, System.Int32& P2)
  RVA=0x04D7A410  token=0x600007A  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x40
EXTENDS: IFix.Core.AnonymousStorey
FIELDS:
METHODS:
  RVA=0x063670C0  token=0x600007B  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x600007C  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x2000013
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x600007D  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x06367CD8  token=0x600007E  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x0307B220  token=0x600007F  System.Boolean IsPatched(System.Int32 id)
  RVA=0x06367BE8  token=0x6000080  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x06367C7C  token=0x6000081  System.Object CreateWrapper(System.Int32 id)
  RVA=0x06367D2C  token=0x6000082  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x06367B60  token=0x6000083  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: IFix.IDMAP0
TYPE:  sealed struct
TOKEN: 0x2000014
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  IFix.IDMAP0                     Beyond-StringUtils-LowerFirst0  // const
  public    static  IFix.IDMAP0                     Beyond-StringUtils-TrimStartLength0  // const
  public    static  IFix.IDMAP0                     Beyond-StringUtils-TrimStartLength1  // const
  public    static  IFix.IDMAP0                     Beyond-StringUtils-TrimEndLength0  // const
  public    static  IFix.IDMAP0                     Beyond-StringUtils-TrimEndLength1  // const
  public    static  IFix.IDMAP0                     Beyond-StringUtils-Trim0  // const
  public    static  IFix.IDMAP0                     Beyond-StringUtils-EqualsIgnoreCase0  // const
  public    static  IFix.IDMAP0                     Beyond-StringUtils-SafeValue0  // const
  public    static  IFix.IDMAP0                     Beyond-StringUtils-HexToInt0  // const
  public    static  IFix.IDMAP0                     Beyond-StringUtils-StringHexToInt0  // const
  public    static  IFix.IDMAP0                     Beyond-StringUtils-CreateZStringBuilder0  // const
  public    static  IFix.IDMAP0                     Beyond-Security-IOOO000iiI-_Encrypt0  // const
  public    static  IFix.IDMAP0                     Beyond-Security-IOOO000iiI-EncryptBytes0  // const
  public    static  IFix.IDMAP0                     Beyond-Security-IOOO000iiI-Encrypt0  // const
  public    static  IFix.IDMAP0                     Beyond-Security-IOOO000iiI-_IIiiIIIoO0  // const
  public    static  IFix.IDMAP0                     Beyond-Security-IOOO000iiI-_IooiIIO0  // const
  public    static  IFix.IDMAP0                     Beyond-Security-IOOO000iiI-IOoOIiII0  // const
  public    static  IFix.IDMAP0                     Beyond-Security-IOOO000iiI-IooiIIO0  // const
  public    static  IFix.IDMAP0                     Beyond-Security-IOOO000iiI-_IIiiIIIoO1  // const
  public    static  IFix.IDMAP0                     Beyond-Security-IOOO000iiI-FromBase64String0  // const
METHODS:
END_CLASS

CLASS: Microsoft.CodeAnalysis.EmbeddedAttribute
TYPE:  sealed class
TOKEN: 0x2000002
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
END_CLASS

CLASS: System.Runtime.CompilerServices.IsUnmanagedAttribute
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000002  System.Void .ctor()
END_CLASS

CLASS: Unity.IL2CPP.CompilerServices.Option
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Unity.IL2CPP.CompilerServices.OptionNullChecks  // const
  public    static  Unity.IL2CPP.CompilerServices.OptionArrayBoundsChecks  // const
  public    static  Unity.IL2CPP.CompilerServices.OptionDivideByZeroChecks  // const
METHODS:
END_CLASS

CLASS: Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  private           Unity.IL2CPP.CompilerServices.Option<Option>k__BackingField  // 0x10
  private           System.Object                   <Value>k__BackingField  // 0x18
PROPERTIES:
  Option  get=0x02B2E2D0  set=0x014F51F0
  Value  get=0x0385B100  set=0x05392C40
METHODS:
  RVA=0x04A41B50  token=0x6000007  System.Void .ctor(Unity.IL2CPP.CompilerServices.Option option, System.Object value)
END_CLASS

