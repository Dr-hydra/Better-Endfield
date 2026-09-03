// ========================================================
// Dumped by @desirepro
// Assembly: ManifestDataBinary.dll
// Classes:  16
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

CLASS: RefEnumerator`1
TYPE:  sealed struct
TOKEN: 0x2000007
FIELDS:
  private   readonly System.Byte*                    m_address  // 0x0
  private   readonly System.Byte*                    m_slotAddress  // 0x0
  private           System.Int32                    m_currentIndex  // 0x0
  private           System.Int32                    m_slotIndex  // 0x0
  private   readonly System.Int32                    m_capacity  // 0x0
PROPERTIES:
  slotIndex  get=-1  // not resolved
  ptrCurrent  get=-1  // not resolved
  current  get=-1  // not resolved
  GetCurrent  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000015  System.Void .ctor(Beyond.ManifestBinary.RefMultiHashTable<K,V> table)
  RVA=-1  // not resolved  token=0x6000016  System.Boolean MoveNext()
END_CLASS

CLASS: TestValue
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x20
FIELDS:
  public            System.Int64                    hashKey  // 0x10
  public            System.Int32                    payload  // 0x18
  public            System.Int32                    extra  // 0x1c
METHODS:
  RVA=0x09F50540  token=0x6000027  System.String ToString()
  RVA=0x09F50634  token=0x6000028  System.String <>iFixBaseProxy_ToString()
END_CLASS

CLASS: TestComparerLong
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x10
IMPLEMENTS: Beyond.ManifestBinary.IRefComparer`1
FIELDS:
METHODS:
  RVA=0x09F504EC  token=0x6000029  System.Int64 GetHashCode(System.Int64 obj)
  RVA=0x041E1670  token=0x600002A  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200000B
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.ManifestBinary.RefHashTableRuntimeTest.<>c<>9  // static @ 0x0
  public    static  System.Func<Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue,System.Int32,System.Int64><>9__4_0  // static @ 0x8
  public    static  System.Func<Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue,System.Int32,System.Int64><>9__5_0  // static @ 0x10
  public    static  System.Func<Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue,System.Int32,System.Int64><>9__6_0  // static @ 0x18
  public    static  System.Func<Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue,System.Int32,System.Int64><>9__7_0  // static @ 0x20
  public    static  System.Func<Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue,System.Int32,System.Int64><>9__8_0  // static @ 0x28
  public    static  System.Func<Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue,System.Int32,System.Int64><>9__9_0  // static @ 0x30
  public    static  System.Func<Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue,System.Int32,System.Int64><>9__10_0  // static @ 0x38
  public    static  System.Func<Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue,System.Int32,System.Int64><>9__11_0  // static @ 0x40
  public    static  System.Func<Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue,System.Int32,System.Int64><>9__12_0  // static @ 0x48
  public    static  System.Func<Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue,System.Int32,System.Int64><>9__13_0  // static @ 0x50
  public    static  System.Func<Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue,System.Int32,System.Int64><>9__13_1  // static @ 0x58
  public    static  System.Func<Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue,System.Int32,System.Int64><>9__14_0  // static @ 0x60
METHODS:
  RVA=0x09F50684  token=0x600002B  System.Void .cctor()
  RVA=0x041E1670  token=0x600002C  System.Void .ctor()
  RVA=0x02A55C30  token=0x600002D  System.Int64 <Test_EmptyList>b__4_0(Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue v, System.Int32 i)
  RVA=0x02A55C30  token=0x600002E  System.Int64 <Test_SingleElement>b__5_0(Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue v, System.Int32 i)
  RVA=0x02A55C30  token=0x600002F  System.Int64 <Test_MultipleElements_NoDuplicate>b__6_0(Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue v, System.Int32 i)
  RVA=0x02A55C30  token=0x6000030  System.Int64 <Test_HashCollision_SameSlot>b__7_0(Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue v, System.Int32 i)
  RVA=0x02A55C30  token=0x6000031  System.Int64 <Test_ContainsKey>b__8_0(Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue v, System.Int32 i)
  RVA=0x02A55C30  token=0x6000032  System.Int64 <Test_NotFoundKey>b__9_0(Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue v, System.Int32 i)
  RVA=0x02A55C30  token=0x6000033  System.Int64 <Test_TryGetValueList_MultiResult>b__10_0(Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue v, System.Int32 i)
  RVA=0x02A55C30  token=0x6000034  System.Int64 <Test_Enumerator_CountMatch>b__11_0(Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue v, System.Int32 i)
  RVA=0x02A55C30  token=0x6000035  System.Int64 <Test_LargeScale>b__12_0(Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue v, System.Int32 i)
  RVA=0x02A55C30  token=0x6000036  System.Int64 <Test_MassiveHashCollision>b__13_0(Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue v, System.Int32 i)
  RVA=0x02A55C30  token=0x6000037  System.Int64 <Test_MassiveHashCollision>b__13_1(Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue v, System.Int32 i)
  RVA=0x04DA38C0  token=0x6000038  System.Int64 <Test_IndexAwareGetHash>b__14_0(Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue v, System.Int32 i)
END_CLASS

CLASS: Beyond.ManifestBinary.RefHashSlot
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x18
FIELDS:
  public            System.Int32                    offset  // 0x10
  public            System.Int32                    bucketsSize  // 0x14
METHODS:
  RVA=0x09F4CF14  token=0x6000003  System.Int32 GetSize()
END_CLASS

CLASS: Beyond.ManifestBinary.IRefComparer`1
TYPE:  interface
TOKEN: 0x2000005
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000004  System.Int64 GetHashCode(T obj)
END_CLASS

CLASS: Beyond.ManifestBinary.RefMultiHashTable`2
TYPE:  sealed struct
TOKEN: 0x2000006
IMPLEMENTS: Beyond.ManifestBinary.IRefStructInterface
FIELDS:
  public            System.Int32                    capacity  // 0x0
  private           System.Byte*                    m_head  // 0x0
  private           System.Byte*                    m_slotAddress  // 0x0
  public            Beyond.ManifestBinary.IRefComparer<K>m_iComparer  // 0x0
PROPERTIES:
  slots  get=-1  // not resolved
  isCreated  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000007  System.Void .ctor(Beyond.ManifestBinary.IRefComparer<K> iComparer)
  RVA=-1  // not resolved  token=0x6000008  System.Void Init(System.Byte* data)
  RVA=-1  // not resolved  token=0x6000009  System.Void Init(System.IntPtr data)
  RVA=-1  // not resolved  token=0x600000A  System.Boolean ContainsKey(K key)
  RVA=-1  // not resolved  token=0x600000B  System.Boolean ContainsKey(System.Int64 hash)
  RVA=-1  // not resolved  token=0x600000C  Beyond.ManifestBinary.RefHashSlot* GetHashSlots(K key, System.Int64& hash)
  RVA=-1  // not resolved  token=0x600000D  Beyond.ManifestBinary.RefHashSlot* GetHashSlots(System.Int64 hash)
  RVA=-1  // not resolved  token=0x600000E  System.Boolean TryGetValue(K key, V*& item, System.Int32 valueKeyOffset)
  RVA=-1  // not resolved  token=0x600000F  System.Boolean TryGetValue(K key, V& item, System.Int32 valueKeyOffset)
  RVA=-1  // not resolved  token=0x6000010  System.Boolean TryGetValue(K key, System.Collections.Generic.List<V>& array)
  RVA=-1  // not resolved  token=0x6000011  Beyond.ManifestBinary.RefMultiHashTable.RefEnumerator<K,V,V> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000012  Beyond.ManifestBinary.RefMultiHashTable<K,V> CreateDynamicTable(Unity.Collections.NativeArray<V> items, System.Func<V,System.Int32,System.Int64> getHash, Beyond.ManifestBinary.IRefComparer<K> comparer, System.Func<System.String,System.IntPtr,System.Int32,System.Int32,System.IntPtr> createFunc)
  RVA=-1  // not resolved  token=0x6000013  System.Void DestroyRuntimeTable(System.Func<System.IntPtr,System.Boolean> freeMmapFunc)
END_CLASS

CLASS: Beyond.ManifestBinary.RefHashTableRuntimeTest
TYPE:  static class
TOKEN: 0x2000008
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09F4D060  token=0x600001A  System.Void RunAllTests()
  RVA=0x09F4CF54  token=0x600001B  System.Boolean Assert(System.Boolean condition, System.String testName, System.String msg)
  RVA=0x09F4D4D0  token=0x600001C  System.Boolean Test_EmptyList()
  RVA=0x09F4FB78  token=0x600001D  System.Boolean Test_SingleElement()
  RVA=0x09F4F4C4  token=0x600001E  System.Boolean Test_MultipleElements_NoDuplicate()
  RVA=0x09F4D9F0  token=0x600001F  System.Boolean Test_HashCollision_SameSlot()
  RVA=0x09F4D1B0  token=0x6000020  System.Boolean Test_ContainsKey()
  RVA=0x09F4F8A8  token=0x6000021  System.Boolean Test_NotFoundKey()
  RVA=0x09F4FF30  token=0x6000022  System.Boolean Test_TryGetValueList_MultiResult()
  RVA=0x09F4D704  token=0x6000023  System.Boolean Test_Enumerator_CountMatch()
  RVA=0x09F4E28C  token=0x6000024  System.Boolean Test_LargeScale()
  RVA=0x09F4E750  token=0x6000025  System.Boolean Test_MassiveHashCollision()
  RVA=0x09F4DEDC  token=0x6000026  System.Boolean Test_IndexAwareGetHash()
END_CLASS

CLASS: Beyond.ManifestBinary.IRefStructInterface
TYPE:  interface
TOKEN: 0x200000C
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000039  System.Void Init(System.Byte* data)
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x200000D
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x600003A  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x06377214  token=0x600003B  System.Int32 __Gen_Wrap_0()
  RVA=0x09F4CA00  token=0x600003C  System.Int64 __Gen_Wrap_1(Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue P0, System.Int32 P1)
  RVA=0x09F4CB0C  token=0x600003D  System.Boolean __Gen_Wrap_2(System.Boolean P0, System.Object P1, System.Object P2)
  RVA=0x063770B0  token=0x600003E  System.Boolean __Gen_Wrap_3()
  RVA=0x063761FC  token=0x600003F  System.Void __Gen_Wrap_4()
  RVA=0x09F4CC00  token=0x6000040  System.String __Gen_Wrap_5(Beyond.ManifestBinary.RefHashTableRuntimeTest.TestValue& P0)
  RVA=0x09F4CD44  token=0x6000041  System.Int64 __Gen_Wrap_6(System.Object P0, System.Int64 P1)
  RVA=0x09F4CE1C  token=0x6000042  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x200000E
SIZE:  0x40
EXTENDS: IFix.Core.AnonymousStorey
FIELDS:
METHODS:
  RVA=0x09F4CE80  token=0x6000043  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x6000044  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x200000F
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000045  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x09F508D0  token=0x6000046  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x09F509C0  token=0x6000047  System.Boolean IsPatched(System.Int32 id)
  RVA=0x09F5078C  token=0x6000048  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x09F50858  token=0x6000049  System.Object CreateWrapper(System.Int32 id)
  RVA=0x09F50934  token=0x600004A  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x09F506E8  token=0x600004B  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: IFix.IDMAP0
TYPE:  sealed struct
TOKEN: 0x2000010
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  IFix.IDMAP0                     Beyond-ManifestBinary-RefHashSlot-GetSize0  // const
  public    static  IFix.IDMAP0                     Beyond-ManifestBinary-RefHashTableRuntimeTest-Assert0  // const
  public    static  IFix.IDMAP0                     Beyond-ManifestBinary-RefHashTableRuntimeTest-Test_EmptyList0  // const
  public    static  IFix.IDMAP0                     Beyond-ManifestBinary-RefHashTableRuntimeTest-Test_SingleElement0  // const
  public    static  IFix.IDMAP0                     Beyond-ManifestBinary-RefHashTableRuntimeTest-Test_MultipleElements_NoDuplicate0  // const
  public    static  IFix.IDMAP0                     Beyond-ManifestBinary-RefHashTableRuntimeTest-Test_HashCollision_SameSlot0  // const
  public    static  IFix.IDMAP0                     Beyond-ManifestBinary-RefHashTableRuntimeTest-Test_ContainsKey0  // const
  public    static  IFix.IDMAP0                     Beyond-ManifestBinary-RefHashTableRuntimeTest-Test_NotFoundKey0  // const
  public    static  IFix.IDMAP0                     Beyond-ManifestBinary-RefHashTableRuntimeTest-Test_TryGetValueList_MultiResult0  // const
  public    static  IFix.IDMAP0                     Beyond-ManifestBinary-RefHashTableRuntimeTest-Test_Enumerator_CountMatch0  // const
  public    static  IFix.IDMAP0                     Beyond-ManifestBinary-RefHashTableRuntimeTest-Test_LargeScale0  // const
  public    static  IFix.IDMAP0                     Beyond-ManifestBinary-RefHashTableRuntimeTest-Test_MassiveHashCollision0  // const
  public    static  IFix.IDMAP0                     Beyond-ManifestBinary-RefHashTableRuntimeTest-Test_IndexAwareGetHash0  // const
  public    static  IFix.IDMAP0                     Beyond-ManifestBinary-RefHashTableRuntimeTest-RunAllTests0  // const
  public    static  IFix.IDMAP0                     Beyond-ManifestBinary-RefHashTableRuntimeTest-TestValue-ToString0  // const
  public    static  IFix.IDMAP0                     Beyond-ManifestBinary-RefHashTableRuntimeTest-TestComparerLong-GetHashCode0  // const
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

