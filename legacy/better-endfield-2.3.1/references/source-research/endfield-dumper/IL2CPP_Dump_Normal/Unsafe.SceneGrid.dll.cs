// ========================================================
// Dumped by @desirepro
// Assembly: Unsafe.SceneGrid.dll
// Classes:  25
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x200000B
    public sealed struct FDictionaryEnumerator
    {
        // Fields
        public Beyond.Gameplay.Core.FDictionary4<Tk,Tv>* ptr;  // 0x0
        private System.Int32 m_pos;  // 0x0

        // Properties
        System.ValueTuple<Tk,Tv> Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000013
        public System.Void .ctor(Beyond.Gameplay.Core.FDictionary4<Tk,Tv>* initPtr) { }
        // RVA: -1  // not resolved  token: 0x6000014
        public System.Boolean MoveNext() { }

    }

    // TypeToken: 0x200000D
    public sealed struct FDictionaryEnumerator
    {
        // Fields
        public Beyond.Gameplay.Core.FDictionary16<Tk,Tv>* ptr;  // 0x0
        private System.Int32 m_pos;  // 0x0

        // Properties
        System.ValueTuple<Tk,Tv> Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000020
        public System.Void .ctor(Beyond.Gameplay.Core.FDictionary16<Tk,Tv>* initPtr) { }
        // RVA: -1  // not resolved  token: 0x6000021
        public System.Boolean MoveNext() { }

    }

namespace Beyond.Gameplay.Core
{

    // TypeToken: 0x200000A
    public sealed struct FDictionary4`2
    {
        // Fields
        public Beyond.FArray4<Tk> kArray;  // 0x0
        public Beyond.FArray4<Tv> vArray;  // 0x0
        public System.UInt32 bitArray;  // 0x0
        public static readonly System.Int32 CAPACITY;  // static @ 0x0

        // Properties
        Tv Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000009
        public Beyond.Gameplay.Core.FDictionary4.FDictionaryEnumerator<Tk,Tv> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600000A
        public System.Void .ctor(System.Int32 len) { }
        // RVA: -1  // not resolved  token: 0x600000B
        public System.Void SetBitEnable(System.Int32 bit) { }
        // RVA: -1  // not resolved  token: 0x600000C
        public System.Void SetBitDisable(System.Int32 bit) { }
        // RVA: -1  // not resolved  token: 0x600000D
        public System.Boolean IsBitEnable(System.Int32 bit) { }
        // RVA: -1  // not resolved  token: 0x600000E
        public System.Boolean TryGetValue(Tk k, Tv& v) { }
        // RVA: -1  // not resolved  token: 0x600000F
        public System.Boolean TryRemove(Tk k) { }
        // RVA: -1  // not resolved  token: 0x6000012
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000C
    public sealed struct FDictionary16`2
    {
        // Fields
        public Beyond.FArray16<Tk> kArray;  // 0x0
        public Beyond.FArray16<Tv> vArray;  // 0x0
        public System.UInt32 bitArray;  // 0x0
        public static readonly System.Int32 CAPACITY;  // static @ 0x0

        // Properties
        Tv Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000016
        public Beyond.Gameplay.Core.FDictionary16.FDictionaryEnumerator<Tk,Tv> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000017
        public System.Void .ctor(System.Int32 len) { }
        // RVA: -1  // not resolved  token: 0x6000018
        public System.Void SetBitEnable(System.Int32 bit) { }
        // RVA: -1  // not resolved  token: 0x6000019
        public System.Void SetBitDisable(System.Int32 bit) { }
        // RVA: -1  // not resolved  token: 0x600001A
        public System.Boolean IsBitEnable(System.Int32 bit) { }
        // RVA: -1  // not resolved  token: 0x600001B
        public System.Boolean TryGetValue(Tk k, Tv& v) { }
        // RVA: -1  // not resolved  token: 0x600001C
        public System.Boolean TryRemove(Tk k) { }
        // RVA: -1  // not resolved  token: 0x600001F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000E
    public class MemFriendSetContextManager`1
    {
        // Fields
        private static System.Int32 ARRAY_CACHE_SIZE;  // const
        private static System.Int32 SEQ_SET_MAX;  // const
        private static System.Int32 INIT_SET_CAP;  // const
        private static System.Int32 MAX_SET_CAP;  // const
        private static System.Int32 INIT_MIN_CAP;  // const
        public static System.Int32 MAX_CONTENT_SIZE;  // const
        public readonly System.Int32 minCapSize;  // 0x0
        public readonly System.Int32 initContentSize;  // 0x0
        public readonly System.Int32 freeListPointerSize;  // 0x0
        private readonly Beyond.Gameplay.Core.MemFriendSetContextBase<T>[] m_setContexts;  // 0x0
        private readonly System.Collections.Generic.Dictionary<System.Int32,Beyond.Gameplay.Core.MemFriendSetContextBase<T>> m_setContextsMap;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000023
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000024
        private Beyond.Gameplay.Core.MemFriendSetContextBase<T> _GetContext(System.Int32 cap) { }
        // RVA: -1  // not resolved  token: 0x6000025
        private Beyond.Gameplay.Core.MemFriendSetContextBase<T> _CreateContext(System.Int32 cap) { }
        // RVA: -1  // not resolved  token: 0x6000026
        public System.Boolean Insert(Beyond.Gameplay.Core.MemFriendHashSet& set, T item) { }
        // RVA: -1  // not resolved  token: 0x6000027
        public System.Boolean Remove(Beyond.Gameplay.Core.MemFriendHashSet& set, T item) { }
        // RVA: -1  // not resolved  token: 0x6000028
        public System.Boolean Contains(Beyond.Gameplay.Core.MemFriendHashSet& set, T item) { }
        // RVA: -1  // not resolved  token: 0x6000029
        public Beyond.Gameplay.Core.PureEnumerator<T> GetEnumerator(Beyond.Gameplay.Core.MemFriendHashSet& set) { }
        // RVA: -1  // not resolved  token: 0x600002A
        public System.Void AddToMonoSet(Beyond.Gameplay.Core.MemFriendHashSet& set, System.Collections.Generic.HashSet<T> monoSet) { }
        // RVA: -1  // not resolved  token: 0x600002B
        public System.Void AddToMonoList(Beyond.Gameplay.Core.MemFriendHashSet& set, System.Collections.Generic.List<T> monoList) { }
        // RVA: -1  // not resolved  token: 0x600002C
        public Beyond.Gameplay.Core.MemFriendHashSet AllocateSet(System.Int32 cap) { }
        // RVA: -1  // not resolved  token: 0x600002D
        public System.Void Recycle(Beyond.Gameplay.Core.MemFriendHashSet& set) { }
        // RVA: -1  // not resolved  token: 0x600002E
        public System.Int32 GetContextSize(System.Int32 cap) { }
        // RVA: -1  // not resolved  token: 0x600002F
        public System.Void OnRelease() { }

    }

    // TypeToken: 0x200000F
    public sealed struct PureEnumerator`1
    {
        // Fields
        private System.Int32 m_index;  // 0x0
        private System.Int32 m_validCnt;  // 0x0
        private Beyond.Gameplay.Core.MemFriendHashSet m_set;  // 0x0
        private readonly Beyond.Gameplay.Core.MemFriendSetContextBase<T> m_context;  // 0x0

        // Properties
        T current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000031
        public System.Void .ctor(Beyond.Gameplay.Core.MemFriendHashSet set, Beyond.Gameplay.Core.MemFriendSetContextBase<T> context) { }
        // RVA: -1  // not resolved  token: 0x6000032
        public System.Boolean MoveNext() { }

    }

    // TypeToken: 0x2000010  // size: 0x20
    public sealed struct MemFriendHashSet
    {
        // Fields
        public System.Int32 start;  // 0x10
        public System.Int32 cap;  // 0x14
        public System.Int32 count;  // 0x18
        public System.Int32 bitStart;  // 0x1c

        // Methods
        // RVA: 0x036B5F60  token: 0x6000033
        public System.Void CopyFrom(Beyond.Gameplay.Core.MemFriendHashSet& other) { }
        // RVA: 0x036B6220  token: 0x6000034
        public System.Void Clear() { }

    }

    // TypeToken: 0x2000011
    public abstract class MemFriendSetContextBase`1 : System.IDisposable
    {
        // Fields
        protected Unity.Collections.NativeArray<T> m_content;  // 0x0
        protected readonly System.Int32 m_singleElementSize;  // 0x0
        public static System.Int32 INVALID_CONTENT_INDEX;  // const
        public readonly System.Int32 typeSize;  // 0x0
        public readonly System.Int32 stride;  // 0x0
        public readonly Beyond.Gameplay.Core.MemFriendSetContextManager<T> contextManager;  // 0x0
        private System.Int32 m_freeListHead;  // 0x0
        private System.Int32 m_freeListTail;  // 0x0
        private System.Byte* m_ptr;  // 0x0
        public System.Int32 currentCount;  // 0x0

        // Properties
        System.Int32 singleElementSize { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000036
        protected System.Void .ctor(Beyond.Gameplay.Core.MemFriendSetContextManager<T> contextManager, System.Int32 initSize, System.Int32 singleElementSize) { }
        // RVA: -1  // not resolved  token: 0x6000037
        public System.Void InitArrayFreeList(System.Int32 baseIndex, System.Int32 size) { }
        // RVA: -1  // not resolved  token: 0x6000038
        protected System.Void _ReplaceWithNewCap(Beyond.Gameplay.Core.MemFriendHashSet& set, System.Int32 newCap) { }
        // RVA: -1  // not resolved  token: 0x6000039
        protected System.Void _TryShrink(Beyond.Gameplay.Core.MemFriendHashSet& set) { }
        // RVA: -1  // not resolved  token: 0x600003A
        protected System.Boolean _TryEnLarge(Beyond.Gameplay.Core.MemFriendHashSet& set, T item) { }
        // RVA: -1  // not resolved  token: 0x600003B
        protected virtual System.Void EnLarge() { }
        // RVA: -1  // not resolved  token: 0x600003C
        public virtual Beyond.Gameplay.Core.MemFriendHashSet Allocate() { }
        // RVA: -1  // not resolved  token: 0x600003D
        public virtual System.Void Free(Beyond.Gameplay.Core.MemFriendHashSet& set) { }
        // RVA: -1  // not resolved  token: 0x600003E
        public System.Void CopyFromAnother(Beyond.Gameplay.Core.MemFriendHashSet& desSet, Beyond.Gameplay.Core.MemFriendHashSet& srcSet) { }
        // RVA: -1  // not resolved  token: 0x600003F
        public T GetElement(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000040
        protected System.Boolean IsEqual(System.Int32 index, T item) { }
        // RVA: -1  // not resolved  token: 0x6000041
        protected System.Int32 GetHashCodeOfElement(T item) { }
        // RVA: -1  // not resolved  token: 0x6000042
        public Beyond.Gameplay.Core.PureEnumerator<T> GetEnumerator(Beyond.Gameplay.Core.MemFriendHashSet& set) { }
        // RVA: -1  // abstract  token: 0x6000043
        public virtual System.Int32 GetNextValidIndex(Beyond.Gameplay.Core.MemFriendHashSet& set, System.Int32 index, System.Int32 validCnt) { }
        // RVA: -1  // abstract  token: 0x6000044
        public virtual System.Boolean Insert(Beyond.Gameplay.Core.MemFriendHashSet& set, T item) { }
        // RVA: -1  // abstract  token: 0x6000045
        public virtual System.Boolean Remove(Beyond.Gameplay.Core.MemFriendHashSet& set, T item) { }
        // RVA: -1  // abstract  token: 0x6000046
        public virtual System.Boolean Contains(Beyond.Gameplay.Core.MemFriendHashSet& set, T item) { }
        // RVA: -1  // abstract  token: 0x6000047
        public virtual System.Boolean ShouldEnLarge(System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x6000048
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x2000012  // size: 0x30
    public class BitContext : System.IDisposable
    {
        // Fields
        public Unity.Collections.NativeArray<System.Byte> bits;  // 0x10
        public static System.Int32 ONE_SLOT_BIT_NUM;  // const
        public static System.Int32 ONE_STATE_BIT_NUM;  // const
        public static System.Int32 ONE_SLOT_ELEMENT_NUM;  // const
        public static System.Byte MASK;  // const
        public static System.Byte FREE;  // const
        public static System.Byte OCCUPIED;  // const
        public static System.Byte USED;  // const
        private readonly System.Int32 m_elementBitSlot;  // 0x20
        private System.Byte* m_ptr;  // 0x28

        // Methods
        // RVA: 0x036B52A0  token: 0x6000049
        public System.Void .ctor(System.Int32 initSize, System.Int32 singleElementSize) { }
        // RVA: 0x036B5350  token: 0x600004A
        private System.Int32 _ComputePropBitArrayLength(System.Int32 contentLength) { }
        // RVA: 0x0B5A9F4C  token: 0x600004B
        public System.Void EnLarge(System.Int32 contentSize) { }
        // RVA: 0x036B5830  token: 0x600004C
        public System.Void Recycle(System.Int32 bitIndex) { }
        // RVA: 0x0B5AA040  token: 0x600004D
        public System.Int32 GetBitStartIndex(System.Int32 index) { }
        // RVA: 0x04DBF4B0  token: 0x600004E
        private System.Void _CalculateBitIndexAndMask(System.Int32 bitStateStartIndex, System.Int32 index, System.Int32& finalIndex, System.Int32& offsetInternal) { }
        // RVA: 0x036B6590  token: 0x600004F
        public System.Byte GetState(System.Int32 bitStateStartIndex, System.Int32 index) { }
        // RVA: 0x036B6250  token: 0x6000050
        public System.Void SetState(System.Int32 bitStateStartIndex, System.Int32 index, System.Byte state) { }
        // RVA: 0x0B5A9EC0  token: 0x6000051
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x2000013
    public class MemFriendNormalSetContext`1 : Beyond.Gameplay.Core.MemFriendSetContextBase`1
    {
        // Fields
        private readonly Beyond.Gameplay.Core.BitContext m_bitContext;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000052
        public System.Void .ctor(Beyond.Gameplay.Core.MemFriendSetContextManager<T> contextManager, System.Int32 initSize, System.Int32 singleElementSize) { }
        // RVA: -1  // not resolved  token: 0x6000053
        protected virtual System.Void EnLarge() { }
        // RVA: -1  // not resolved  token: 0x6000054
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000055
        public virtual System.Int32 GetNextValidIndex(Beyond.Gameplay.Core.MemFriendHashSet& set, System.Int32 index, System.Int32 validCnt) { }
        // RVA: -1  // not resolved  token: 0x6000056
        public virtual Beyond.Gameplay.Core.MemFriendHashSet Allocate() { }
        // RVA: -1  // not resolved  token: 0x6000057
        public virtual System.Void Free(Beyond.Gameplay.Core.MemFriendHashSet& set) { }
        // RVA: -1  // not resolved  token: 0x6000058
        public virtual System.Boolean ShouldEnLarge(System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x6000059
        public virtual System.Boolean Insert(Beyond.Gameplay.Core.MemFriendHashSet& set, T item) { }
        // RVA: -1  // not resolved  token: 0x600005A
        public virtual System.Boolean Remove(Beyond.Gameplay.Core.MemFriendHashSet& set, T item) { }
        // RVA: -1  // not resolved  token: 0x600005B
        public virtual System.Boolean Contains(Beyond.Gameplay.Core.MemFriendHashSet& set, T item) { }

    }

    // TypeToken: 0x2000014
    public class MemFriendSeqSetContext`1 : Beyond.Gameplay.Core.MemFriendSetContextBase`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x600005C
        public System.Void .ctor(Beyond.Gameplay.Core.MemFriendSetContextManager<T> contextManager, System.Int32 initSize, System.Int32 singleElementSize) { }
        // RVA: -1  // not resolved  token: 0x600005D
        public virtual System.Int32 GetNextValidIndex(Beyond.Gameplay.Core.MemFriendHashSet& set, System.Int32 index, System.Int32 validCnt) { }
        // RVA: -1  // not resolved  token: 0x600005E
        public virtual System.Boolean ShouldEnLarge(System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x600005F
        public virtual System.Boolean Insert(Beyond.Gameplay.Core.MemFriendHashSet& set, T item) { }
        // RVA: -1  // not resolved  token: 0x6000060
        public virtual System.Boolean Remove(Beyond.Gameplay.Core.MemFriendHashSet& set, T item) { }
        // RVA: -1  // not resolved  token: 0x6000061
        public virtual System.Boolean Contains(Beyond.Gameplay.Core.MemFriendHashSet& set, T item) { }

    }

    // TypeToken: 0x2000015  // size: 0x10
    public static class GameplayUnsafeUtil
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000062
        public static System.Byte[] NativeArrayToBytes(Unity.Collections.NativeArray<T> nativeArray) { }
        // RVA: 0x0B5AA04C  token: 0x6000063
        public static Unity.Collections.NativeArray<System.Byte> GetNativeArrayFromStream(System.IO.Stream stream) { }

    }

}

namespace Beyond.SourceGenerator
{

    // TypeToken: 0x2000005  // size: 0x10
    public class CameraControlConfigAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000004
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x20
    public class DataNAttribute : System.Attribute
    {
        // Fields
        public System.Int32 capacity;  // 0x10
        public System.Int32 tSize;  // 0x14
        public System.Boolean ring;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000005
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x10
    public static class AnimatorBlackboardUtility
    {
        // Fields
        private static System.Boolean s_enableFastPathBuffer;  // static @ 0x0

        // Methods
        // RVA: -1  // generic def  token: 0x6000006
        private static System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity) { }

    }

    // TypeToken: 0x2000008  // size: 0x10
    public class AnimatorBlackboardAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000007
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x18
    public class ECSComponentAttribute : System.Attribute
    {
        // Fields
        public System.Runtime.InteropServices.LayoutKind layoutKind;  // 0x10
        public System.Boolean isTag;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x6000008
        public System.Void .ctor() { }

    }

}

namespace HG.Rendering.Runtime
{

    // TypeToken: 0x2000004  // size: 0x10
    public class HGConstantBufferLayoutAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000003
        public System.Void .ctor() { }

    }

}

namespace IFix
{

    // TypeToken: 0x2000016  // size: 0x28
    public class ILFixDynamicMethodWrapper
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10
        private System.Int32 methodId;  // 0x18
        private System.Object anonObj;  // 0x20
        public static IFix.ILFixDynamicMethodWrapper[] wrapperArray;  // static @ 0x0

        // Methods
        // RVA: 0x032B2C30  token: 0x6000064
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj) { }
        // RVA: 0x0B5AA17C  token: 0x6000065
        public System.Void __Gen_Wrap_0(Beyond.Gameplay.Core.MemFriendHashSet& P0, Beyond.Gameplay.Core.MemFriendHashSet& P1) { }
        // RVA: 0x0B5AA314  token: 0x6000066
        public System.Void __Gen_Wrap_1(Beyond.Gameplay.Core.MemFriendHashSet& P0) { }
        // RVA: 0x066C13F0  token: 0x6000067
        public System.Int32 __Gen_Wrap_2(System.Object P0, System.Int32 P1) { }
        // RVA: 0x066F3838  token: 0x6000068
        public System.Void __Gen_Wrap_3(System.Object P0, System.Int32 P1) { }
        // RVA: 0x0698CC40  token: 0x6000069
        public System.Byte __Gen_Wrap_4(System.Object P0, System.Int32 P1, System.Int32 P2) { }
        // RVA: 0x0B5AA448  token: 0x600006A
        public System.Void __Gen_Wrap_5(System.Object P0, System.Int32 P1, System.Int32 P2, System.Byte P3) { }
        // RVA: 0x066C296C  token: 0x600006B
        public System.Void __Gen_Wrap_6(System.Object P0) { }
        // RVA: 0x0B5AA550  token: 0x600006C
        public Unity.Collections.NativeArray<System.Byte> __Gen_Wrap_7(System.Object P0) { }
        // RVA: 0x04D371E0  token: 0x600006D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000017  // size: 0x40
    public class ILFixInterfaceBridge : IFix.Core.AnonymousStorey
    {
        // Methods
        // RVA: 0x0B5AA648  token: 0x600006E
        public System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x041E1670  token: 0x600006F
        public System.Void RefAsyncBuilderStartMethod() { }

    }

    // TypeToken: 0x2000018  // size: 0x18
    public class WrappersManagerImpl : IFix.Core.WrappersManager
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000070
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x0B5AA8C4  token: 0x6000071
        public static IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id) { }
        // RVA: 0x036B6630  token: 0x6000072
        public static System.Boolean IsPatched(System.Int32 id) { }
        // RVA: 0x0B5AA780  token: 0x6000073
        public virtual System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon) { }
        // RVA: 0x0B5AA84C  token: 0x6000074
        public virtual System.Object CreateWrapper(System.Int32 id) { }
        // RVA: 0x0B5AA928  token: 0x6000075
        public virtual System.Object InitWrapperArray(System.Int32 len) { }
        // RVA: 0x0B5AA6DC  token: 0x6000076
        public virtual IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine) { }

    }

    // TypeToken: 0x2000019  // size: 0x14
    public sealed struct IDMAP0
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static IFix.IDMAP0 Beyond-Gameplay-Core-MemFriendHashSet-CopyFrom0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-MemFriendHashSet-Clear0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-BitContext-_ComputePropBitArrayLength0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-BitContext-EnLarge0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-BitContext-Recycle0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-BitContext-GetState0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-BitContext-SetState0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-BitContext-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-GameplayUnsafeUtil-GetNativeArrayFromStream0;  // const

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

