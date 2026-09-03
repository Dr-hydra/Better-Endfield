// ========================================================
// Dumped by @desirepro
// Assembly: Unsafe.SceneGrid.dll
// Classes:  25
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

CLASS: FDictionaryEnumerator
TYPE:  sealed struct
TOKEN: 0x200000B
FIELDS:
  public            Beyond.Gameplay.Core.FDictionary4<Tk,Tv>*ptr  // 0x0
  private           System.Int32                    m_pos  // 0x0
PROPERTIES:
  Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000013  System.Void .ctor(Beyond.Gameplay.Core.FDictionary4<Tk,Tv>* initPtr)
  RVA=-1  // not resolved  token=0x6000014  System.Boolean MoveNext()
END_CLASS

CLASS: FDictionaryEnumerator
TYPE:  sealed struct
TOKEN: 0x200000D
FIELDS:
  public            Beyond.Gameplay.Core.FDictionary16<Tk,Tv>*ptr  // 0x0
  private           System.Int32                    m_pos  // 0x0
PROPERTIES:
  Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000020  System.Void .ctor(Beyond.Gameplay.Core.FDictionary16<Tk,Tv>* initPtr)
  RVA=-1  // not resolved  token=0x6000021  System.Boolean MoveNext()
END_CLASS

CLASS: Beyond.Gameplay.Core.FDictionary4`2
TYPE:  sealed struct
TOKEN: 0x200000A
FIELDS:
  public            Beyond.FArray4<Tk>              kArray  // 0x0
  public            Beyond.FArray4<Tv>              vArray  // 0x0
  public            System.UInt32                   bitArray  // 0x0
  public    static readonly System.Int32                    CAPACITY  // static @ 0x0
PROPERTIES:
  Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000009  Beyond.Gameplay.Core.FDictionary4.FDictionaryEnumerator<Tk,Tv> GetEnumerator()
  RVA=-1  // not resolved  token=0x600000A  System.Void .ctor(System.Int32 len)
  RVA=-1  // not resolved  token=0x600000B  System.Void SetBitEnable(System.Int32 bit)
  RVA=-1  // not resolved  token=0x600000C  System.Void SetBitDisable(System.Int32 bit)
  RVA=-1  // not resolved  token=0x600000D  System.Boolean IsBitEnable(System.Int32 bit)
  RVA=-1  // not resolved  token=0x600000E  System.Boolean TryGetValue(Tk k, Tv& v)
  RVA=-1  // not resolved  token=0x600000F  System.Boolean TryRemove(Tk k)
  RVA=-1  // not resolved  token=0x6000012  System.Void .cctor()
END_CLASS

CLASS: Beyond.Gameplay.Core.FDictionary16`2
TYPE:  sealed struct
TOKEN: 0x200000C
FIELDS:
  public            Beyond.FArray16<Tk>             kArray  // 0x0
  public            Beyond.FArray16<Tv>             vArray  // 0x0
  public            System.UInt32                   bitArray  // 0x0
  public    static readonly System.Int32                    CAPACITY  // static @ 0x0
PROPERTIES:
  Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000016  Beyond.Gameplay.Core.FDictionary16.FDictionaryEnumerator<Tk,Tv> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000017  System.Void .ctor(System.Int32 len)
  RVA=-1  // not resolved  token=0x6000018  System.Void SetBitEnable(System.Int32 bit)
  RVA=-1  // not resolved  token=0x6000019  System.Void SetBitDisable(System.Int32 bit)
  RVA=-1  // not resolved  token=0x600001A  System.Boolean IsBitEnable(System.Int32 bit)
  RVA=-1  // not resolved  token=0x600001B  System.Boolean TryGetValue(Tk k, Tv& v)
  RVA=-1  // not resolved  token=0x600001C  System.Boolean TryRemove(Tk k)
  RVA=-1  // not resolved  token=0x600001F  System.Void .cctor()
END_CLASS

CLASS: Beyond.Gameplay.Core.MemFriendSetContextManager`1
TYPE:  class
TOKEN: 0x200000E
FIELDS:
  private   static  System.Int32                    ARRAY_CACHE_SIZE  // const
  private   static  System.Int32                    SEQ_SET_MAX  // const
  private   static  System.Int32                    INIT_SET_CAP  // const
  private   static  System.Int32                    MAX_SET_CAP  // const
  private   static  System.Int32                    INIT_MIN_CAP  // const
  public    static  System.Int32                    MAX_CONTENT_SIZE  // const
  public    readonly System.Int32                    minCapSize  // 0x0
  public    readonly System.Int32                    initContentSize  // 0x0
  public    readonly System.Int32                    freeListPointerSize  // 0x0
  private   readonly Beyond.Gameplay.Core.MemFriendSetContextBase<T>[]m_setContexts  // 0x0
  private   readonly System.Collections.Generic.Dictionary<System.Int32,Beyond.Gameplay.Core.MemFriendSetContextBase<T>>m_setContextsMap  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000023  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000024  Beyond.Gameplay.Core.MemFriendSetContextBase<T> _GetContext(System.Int32 cap)
  RVA=-1  // not resolved  token=0x6000025  Beyond.Gameplay.Core.MemFriendSetContextBase<T> _CreateContext(System.Int32 cap)
  RVA=-1  // not resolved  token=0x6000026  System.Boolean Insert(Beyond.Gameplay.Core.MemFriendHashSet& set, T item)
  RVA=-1  // not resolved  token=0x6000027  System.Boolean Remove(Beyond.Gameplay.Core.MemFriendHashSet& set, T item)
  RVA=-1  // not resolved  token=0x6000028  System.Boolean Contains(Beyond.Gameplay.Core.MemFriendHashSet& set, T item)
  RVA=-1  // not resolved  token=0x6000029  Beyond.Gameplay.Core.PureEnumerator<T> GetEnumerator(Beyond.Gameplay.Core.MemFriendHashSet& set)
  RVA=-1  // not resolved  token=0x600002A  System.Void AddToMonoSet(Beyond.Gameplay.Core.MemFriendHashSet& set, System.Collections.Generic.HashSet<T> monoSet)
  RVA=-1  // not resolved  token=0x600002B  System.Void AddToMonoList(Beyond.Gameplay.Core.MemFriendHashSet& set, System.Collections.Generic.List<T> monoList)
  RVA=-1  // not resolved  token=0x600002C  Beyond.Gameplay.Core.MemFriendHashSet AllocateSet(System.Int32 cap)
  RVA=-1  // not resolved  token=0x600002D  System.Void Recycle(Beyond.Gameplay.Core.MemFriendHashSet& set)
  RVA=-1  // not resolved  token=0x600002E  System.Int32 GetContextSize(System.Int32 cap)
  RVA=-1  // not resolved  token=0x600002F  System.Void OnRelease()
END_CLASS

CLASS: Beyond.Gameplay.Core.PureEnumerator`1
TYPE:  sealed struct
TOKEN: 0x200000F
FIELDS:
  private           System.Int32                    m_index  // 0x0
  private           System.Int32                    m_validCnt  // 0x0
  private           Beyond.Gameplay.Core.MemFriendHashSetm_set  // 0x0
  private   readonly Beyond.Gameplay.Core.MemFriendSetContextBase<T>m_context  // 0x0
PROPERTIES:
  current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000031  System.Void .ctor(Beyond.Gameplay.Core.MemFriendHashSet set, Beyond.Gameplay.Core.MemFriendSetContextBase<T> context)
  RVA=-1  // not resolved  token=0x6000032  System.Boolean MoveNext()
END_CLASS

CLASS: Beyond.Gameplay.Core.MemFriendHashSet
TYPE:  sealed struct
TOKEN: 0x2000010
SIZE:  0x20
FIELDS:
  public            System.Int32                    start  // 0x10
  public            System.Int32                    cap  // 0x14
  public            System.Int32                    count  // 0x18
  public            System.Int32                    bitStart  // 0x1c
METHODS:
  RVA=0x036B5F60  token=0x6000033  System.Void CopyFrom(Beyond.Gameplay.Core.MemFriendHashSet& other)
  RVA=0x036B6220  token=0x6000034  System.Void Clear()
END_CLASS

CLASS: Beyond.Gameplay.Core.MemFriendSetContextBase`1
TYPE:  abstract class
TOKEN: 0x2000011
IMPLEMENTS: System.IDisposable
FIELDS:
  protected         Unity.Collections.NativeArray<T>m_content  // 0x0
  protected readonly System.Int32                    m_singleElementSize  // 0x0
  public    static  System.Int32                    INVALID_CONTENT_INDEX  // const
  public    readonly System.Int32                    typeSize  // 0x0
  public    readonly System.Int32                    stride  // 0x0
  public    readonly Beyond.Gameplay.Core.MemFriendSetContextManager<T>contextManager  // 0x0
  private           System.Int32                    m_freeListHead  // 0x0
  private           System.Int32                    m_freeListTail  // 0x0
  private           System.Byte*                    m_ptr  // 0x0
  public            System.Int32                    currentCount  // 0x0
PROPERTIES:
  singleElementSize  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000036  System.Void .ctor(Beyond.Gameplay.Core.MemFriendSetContextManager<T> contextManager, System.Int32 initSize, System.Int32 singleElementSize)
  RVA=-1  // not resolved  token=0x6000037  System.Void InitArrayFreeList(System.Int32 baseIndex, System.Int32 size)
  RVA=-1  // not resolved  token=0x6000038  System.Void _ReplaceWithNewCap(Beyond.Gameplay.Core.MemFriendHashSet& set, System.Int32 newCap)
  RVA=-1  // not resolved  token=0x6000039  System.Void _TryShrink(Beyond.Gameplay.Core.MemFriendHashSet& set)
  RVA=-1  // not resolved  token=0x600003A  System.Boolean _TryEnLarge(Beyond.Gameplay.Core.MemFriendHashSet& set, T item)
  RVA=-1  // not resolved  token=0x600003B  System.Void EnLarge()
  RVA=-1  // not resolved  token=0x600003C  Beyond.Gameplay.Core.MemFriendHashSet Allocate()
  RVA=-1  // not resolved  token=0x600003D  System.Void Free(Beyond.Gameplay.Core.MemFriendHashSet& set)
  RVA=-1  // not resolved  token=0x600003E  System.Void CopyFromAnother(Beyond.Gameplay.Core.MemFriendHashSet& desSet, Beyond.Gameplay.Core.MemFriendHashSet& srcSet)
  RVA=-1  // not resolved  token=0x600003F  T GetElement(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000040  System.Boolean IsEqual(System.Int32 index, T item)
  RVA=-1  // not resolved  token=0x6000041  System.Int32 GetHashCodeOfElement(T item)
  RVA=-1  // not resolved  token=0x6000042  Beyond.Gameplay.Core.PureEnumerator<T> GetEnumerator(Beyond.Gameplay.Core.MemFriendHashSet& set)
  RVA=-1  // abstract  token=0x6000043  System.Int32 GetNextValidIndex(Beyond.Gameplay.Core.MemFriendHashSet& set, System.Int32 index, System.Int32 validCnt)
  RVA=-1  // abstract  token=0x6000044  System.Boolean Insert(Beyond.Gameplay.Core.MemFriendHashSet& set, T item)
  RVA=-1  // abstract  token=0x6000045  System.Boolean Remove(Beyond.Gameplay.Core.MemFriendHashSet& set, T item)
  RVA=-1  // abstract  token=0x6000046  System.Boolean Contains(Beyond.Gameplay.Core.MemFriendHashSet& set, T item)
  RVA=-1  // abstract  token=0x6000047  System.Boolean ShouldEnLarge(System.Int32 count)
  RVA=-1  // not resolved  token=0x6000048  System.Void Dispose()
END_CLASS

CLASS: Beyond.Gameplay.Core.BitContext
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x30
IMPLEMENTS: System.IDisposable
FIELDS:
  public            Unity.Collections.NativeArray<System.Byte>bits  // 0x10
  public    static  System.Int32                    ONE_SLOT_BIT_NUM  // const
  public    static  System.Int32                    ONE_STATE_BIT_NUM  // const
  public    static  System.Int32                    ONE_SLOT_ELEMENT_NUM  // const
  public    static  System.Byte                     MASK  // const
  public    static  System.Byte                     FREE  // const
  public    static  System.Byte                     OCCUPIED  // const
  public    static  System.Byte                     USED  // const
  private   readonly System.Int32                    m_elementBitSlot  // 0x20
  private           System.Byte*                    m_ptr  // 0x28
METHODS:
  RVA=0x036B52A0  token=0x6000049  System.Void .ctor(System.Int32 initSize, System.Int32 singleElementSize)
  RVA=0x036B5350  token=0x600004A  System.Int32 _ComputePropBitArrayLength(System.Int32 contentLength)
  RVA=0x0B5A9F4C  token=0x600004B  System.Void EnLarge(System.Int32 contentSize)
  RVA=0x036B5830  token=0x600004C  System.Void Recycle(System.Int32 bitIndex)
  RVA=0x0B5AA040  token=0x600004D  System.Int32 GetBitStartIndex(System.Int32 index)
  RVA=0x04DBF4B0  token=0x600004E  System.Void _CalculateBitIndexAndMask(System.Int32 bitStateStartIndex, System.Int32 index, System.Int32& finalIndex, System.Int32& offsetInternal)
  RVA=0x036B6590  token=0x600004F  System.Byte GetState(System.Int32 bitStateStartIndex, System.Int32 index)
  RVA=0x036B6250  token=0x6000050  System.Void SetState(System.Int32 bitStateStartIndex, System.Int32 index, System.Byte state)
  RVA=0x0B5A9EC0  token=0x6000051  System.Void Dispose()
END_CLASS

CLASS: Beyond.Gameplay.Core.MemFriendNormalSetContext`1
TYPE:  class
TOKEN: 0x2000013
EXTENDS: Beyond.Gameplay.Core.MemFriendSetContextBase`1
FIELDS:
  private   readonly Beyond.Gameplay.Core.BitContext m_bitContext  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000052  System.Void .ctor(Beyond.Gameplay.Core.MemFriendSetContextManager<T> contextManager, System.Int32 initSize, System.Int32 singleElementSize)
  RVA=-1  // not resolved  token=0x6000053  System.Void EnLarge()
  RVA=-1  // not resolved  token=0x6000054  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000055  System.Int32 GetNextValidIndex(Beyond.Gameplay.Core.MemFriendHashSet& set, System.Int32 index, System.Int32 validCnt)
  RVA=-1  // not resolved  token=0x6000056  Beyond.Gameplay.Core.MemFriendHashSet Allocate()
  RVA=-1  // not resolved  token=0x6000057  System.Void Free(Beyond.Gameplay.Core.MemFriendHashSet& set)
  RVA=-1  // not resolved  token=0x6000058  System.Boolean ShouldEnLarge(System.Int32 count)
  RVA=-1  // not resolved  token=0x6000059  System.Boolean Insert(Beyond.Gameplay.Core.MemFriendHashSet& set, T item)
  RVA=-1  // not resolved  token=0x600005A  System.Boolean Remove(Beyond.Gameplay.Core.MemFriendHashSet& set, T item)
  RVA=-1  // not resolved  token=0x600005B  System.Boolean Contains(Beyond.Gameplay.Core.MemFriendHashSet& set, T item)
END_CLASS

CLASS: Beyond.Gameplay.Core.MemFriendSeqSetContext`1
TYPE:  class
TOKEN: 0x2000014
EXTENDS: Beyond.Gameplay.Core.MemFriendSetContextBase`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600005C  System.Void .ctor(Beyond.Gameplay.Core.MemFriendSetContextManager<T> contextManager, System.Int32 initSize, System.Int32 singleElementSize)
  RVA=-1  // not resolved  token=0x600005D  System.Int32 GetNextValidIndex(Beyond.Gameplay.Core.MemFriendHashSet& set, System.Int32 index, System.Int32 validCnt)
  RVA=-1  // not resolved  token=0x600005E  System.Boolean ShouldEnLarge(System.Int32 count)
  RVA=-1  // not resolved  token=0x600005F  System.Boolean Insert(Beyond.Gameplay.Core.MemFriendHashSet& set, T item)
  RVA=-1  // not resolved  token=0x6000060  System.Boolean Remove(Beyond.Gameplay.Core.MemFriendHashSet& set, T item)
  RVA=-1  // not resolved  token=0x6000061  System.Boolean Contains(Beyond.Gameplay.Core.MemFriendHashSet& set, T item)
END_CLASS

CLASS: Beyond.Gameplay.Core.GameplayUnsafeUtil
TYPE:  static class
TOKEN: 0x2000015
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000062  System.Byte[] NativeArrayToBytes(Unity.Collections.NativeArray<T> nativeArray)
  RVA=0x0B5AA04C  token=0x6000063  Unity.Collections.NativeArray<System.Byte> GetNativeArrayFromStream(System.IO.Stream stream)
END_CLASS

CLASS: Beyond.SourceGenerator.CameraControlConfigAttribute
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000004  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.DataNAttribute
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public            System.Int32                    capacity  // 0x10
  public            System.Int32                    tSize  // 0x14
  public            System.Boolean                  ring  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000005  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardUtility
TYPE:  static class
TOKEN: 0x2000007
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_enableFastPathBuffer  // static @ 0x0
METHODS:
  RVA=-1  // generic def  token=0x6000006  System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity)
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardAttribute
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000007  System.Void .ctor()
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
  RVA=0x041E1670  token=0x6000008  System.Void .ctor()
END_CLASS

CLASS: HG.Rendering.Runtime.HGConstantBufferLayoutAttribute
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000003  System.Void .ctor()
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x2000016
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x6000064  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x0B5AA17C  token=0x6000065  System.Void __Gen_Wrap_0(Beyond.Gameplay.Core.MemFriendHashSet& P0, Beyond.Gameplay.Core.MemFriendHashSet& P1)
  RVA=0x0B5AA314  token=0x6000066  System.Void __Gen_Wrap_1(Beyond.Gameplay.Core.MemFriendHashSet& P0)
  RVA=0x066C13F0  token=0x6000067  System.Int32 __Gen_Wrap_2(System.Object P0, System.Int32 P1)
  RVA=0x066F3838  token=0x6000068  System.Void __Gen_Wrap_3(System.Object P0, System.Int32 P1)
  RVA=0x0698CC40  token=0x6000069  System.Byte __Gen_Wrap_4(System.Object P0, System.Int32 P1, System.Int32 P2)
  RVA=0x0B5AA448  token=0x600006A  System.Void __Gen_Wrap_5(System.Object P0, System.Int32 P1, System.Int32 P2, System.Byte P3)
  RVA=0x066C296C  token=0x600006B  System.Void __Gen_Wrap_6(System.Object P0)
  RVA=0x0B5AA550  token=0x600006C  Unity.Collections.NativeArray<System.Byte> __Gen_Wrap_7(System.Object P0)
  RVA=0x04D371E0  token=0x600006D  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x40
EXTENDS: IFix.Core.AnonymousStorey
FIELDS:
METHODS:
  RVA=0x0B5AA648  token=0x600006E  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x600006F  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000070  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x0B5AA8C4  token=0x6000071  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x036B6630  token=0x6000072  System.Boolean IsPatched(System.Int32 id)
  RVA=0x0B5AA780  token=0x6000073  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x0B5AA84C  token=0x6000074  System.Object CreateWrapper(System.Int32 id)
  RVA=0x0B5AA928  token=0x6000075  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x0B5AA6DC  token=0x6000076  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: IFix.IDMAP0
TYPE:  sealed struct
TOKEN: 0x2000019
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-MemFriendHashSet-CopyFrom0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-MemFriendHashSet-Clear0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-BitContext-_ComputePropBitArrayLength0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-BitContext-EnLarge0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-BitContext-Recycle0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-BitContext-GetState0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-BitContext-SetState0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-BitContext-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-GameplayUnsafeUtil-GetNativeArrayFromStream0  // const
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

