// ========================================================
// Dumped by @desirepro
// Assembly: Unity.Collections.dll
// Classes:  191
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000008
    public sealed class ExecuteJobFunction : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000008
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000009
        public virtual System.Void Invoke(T& data, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex) { }

    }

    // TypeToken: 0x2000007  // size: 0x11
    public sealed struct JobBurstSchedulableProducer`1
    {
        // Fields
        private static readonly Unity.Burst.SharedStatic<System.IntPtr> jobReflectionData;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000005
        private static System.Void Initialize() { }
        // RVA: -1  // not resolved  token: 0x6000006
        public static System.Void Execute(T& data, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex) { }
        // RVA: -1  // not resolved  token: 0x6000007
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000C
    public sealed class ExecuteJobFunction : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000010
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000011
        public virtual System.Void Invoke(T& jobData, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex) { }

    }

    // TypeToken: 0x200000B  // size: 0x11
    public sealed struct JobParallelForBatchProducer`1
    {
        // Fields
        private static readonly Unity.Burst.SharedStatic<System.IntPtr> jobReflectionData;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600000D
        private static System.Void Initialize() { }
        // RVA: -1  // not resolved  token: 0x600000E
        public static System.Void Execute(T& jobData, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex) { }
        // RVA: -1  // not resolved  token: 0x600000F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000010
    public sealed class ExecuteJobFunction : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000016
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000017
        public virtual System.Void Invoke(T& data, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex) { }

    }

    // TypeToken: 0x200000F  // size: 0x11
    public sealed struct JobParallelForBurstSchedulableProducer`1
    {
        // Fields
        private static readonly Unity.Burst.SharedStatic<System.IntPtr> jobReflectionData;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000013
        private static System.Void Initialize() { }
        // RVA: -1  // not resolved  token: 0x6000014
        public static System.Void Execute(T& jobData, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex) { }
        // RVA: -1  // not resolved  token: 0x6000015
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000014
    public sealed class ExecuteJobFunction : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000021
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000022
        public virtual System.Void Invoke(T& jobData, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex) { }

    }

    // TypeToken: 0x2000013  // size: 0x11
    public sealed struct JobParallelForDeferProducer`1
    {
        // Fields
        private static readonly Unity.Burst.SharedStatic<System.IntPtr> jobReflectionData;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600001E
        private static System.Void Initialize() { }
        // RVA: -1  // not resolved  token: 0x600001F
        public static System.Void Execute(T& jobData, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex) { }
        // RVA: -1  // not resolved  token: 0x6000020
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000018
    public sealed struct JobWrapper
    {
        // Fields
        public Unity.Collections.NativeList<System.Int32> outputIndices;  // 0x0
        public System.Int32 appendCount;  // 0x0
        public T JobData;  // 0x0

    }

    // TypeToken: 0x2000019
    public sealed class ExecuteJobFunction : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000029
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x600002A
        public virtual System.Void Invoke(Unity.Jobs.JobParallelIndexListExtensions.JobParallelForFilterProducer.JobWrapper<T>& jobWrapper, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex) { }

    }

    // TypeToken: 0x2000017  // size: 0x11
    public sealed struct JobParallelForFilterProducer`1
    {
        // Fields
        private static readonly Unity.Burst.SharedStatic<System.IntPtr> jobReflectionData;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000024
        public static System.Void Initialize() { }
        // RVA: -1  // not resolved  token: 0x6000025
        public static System.Void Execute(Unity.Jobs.JobParallelIndexListExtensions.JobParallelForFilterProducer.JobWrapper<T>& jobWrapper, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex) { }
        // RVA: -1  // not resolved  token: 0x6000026
        public static System.Void ExecuteAppend(Unity.Jobs.JobParallelIndexListExtensions.JobParallelForFilterProducer.JobWrapper<T>& jobWrapper, System.IntPtr bufferRangePatchData) { }
        // RVA: -1  // not resolved  token: 0x6000027
        public static System.Void ExecuteFilter(Unity.Jobs.JobParallelIndexListExtensions.JobParallelForFilterProducer.JobWrapper<T>& jobWrapper, System.IntPtr bufferRangePatchData) { }
        // RVA: -1  // not resolved  token: 0x6000028
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001C  // size: 0x80
    public sealed class TryFunction : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B163E34  token: 0x6000042
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x015802E0  token: 0x6000043
        public virtual System.Int32 Invoke(System.IntPtr allocatorState, Unity.Collections.AllocatorManager.Block& block) { }

    }

    // TypeToken: 0x200001D  // size: 0x14
    public sealed struct AllocatorHandle : IAllocator, System.IDisposable
    {
        // Fields
        public System.UInt16 Index;  // 0x10
        public System.UInt16 Version;  // 0x12

        // Properties
        Unity.Collections.AllocatorManager.TableEntry& TableEntry { get; /* RVA: 0x0B15FC48 */ }
        System.Int32 Value { get; /* RVA: 0x04D87110 */ }
        Unity.Collections.AllocatorManager.AllocatorHandle Handle { get; /* RVA: 0x02B77620 */ }
        Unity.Collections.Allocator ToAllocator { get; /* RVA: 0x04DAB3E0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000045
        private System.Void Rewind() { }
        // RVA: 0x04DAB3F0  token: 0x6000046
        public static Unity.Collections.AllocatorManager.AllocatorHandle op_Implicit(Unity.Collections.Allocator a) { }
        // RVA: 0x03347A00  token: 0x6000048
        public virtual System.Int32 Try(Unity.Collections.AllocatorManager.Block& block) { }
        // RVA: 0x041E1670  token: 0x600004B
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x200001E  // size: 0x20
    public sealed struct Range : System.IDisposable
    {
        // Fields
        public System.IntPtr Pointer;  // 0x10
        public System.Int32 Items;  // 0x18
        public Unity.Collections.AllocatorManager.AllocatorHandle Allocator;  // 0x1c

        // Methods
        // RVA: 0x0B163650  token: 0x600004C
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x200001F  // size: 0x30
    public sealed struct Block : System.IDisposable
    {
        // Fields
        public Unity.Collections.AllocatorManager.Range Range;  // 0x10
        public System.Int32 BytesPerItem;  // 0x20
        public System.Int32 AllocatedItems;  // 0x24
        public System.Byte Log2Alignment;  // 0x28
        public System.Byte Padding0;  // 0x29
        public System.UInt16 Padding1;  // 0x2a
        public System.UInt32 Padding2;  // 0x2c

        // Properties
        System.Int64 Bytes { get; /* RVA: 0x04DAB4F0 */ }
        System.Int64 AllocatedBytes { get; /* RVA: 0x04DAB4E0 */ }
        System.Int32 Alignment { get; /* RVA: 0x04DAB4D0 */ set; /* RVA: 0x03347C90 */ }

        // Methods
        // RVA: 0x0B15FF9C  token: 0x6000051
        public virtual System.Void Dispose() { }
        // RVA: 0x0B15FFA4  token: 0x6000052
        public System.Int32 TryFree() { }

    }

    // TypeToken: 0x2000020
    public interface IAllocator : System.IDisposable
    {
        // Properties
        Unity.Collections.AllocatorManager.AllocatorHandle Handle { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000053
        public virtual System.Int32 Try(Unity.Collections.AllocatorManager.Block& block) { }

    }

    // TypeToken: 0x2000022  // size: 0x80
    public sealed class Try_000000A0$PostfixBurstDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B163E34  token: 0x600005A
        public System.Void .ctor(System.Object , System.IntPtr ) { }
        // RVA: 0x015802E0  token: 0x600005B
        public virtual System.Int32 Invoke(System.IntPtr allocatorState, Unity.Collections.AllocatorManager.Block& block) { }

    }

    // TypeToken: 0x2000023  // size: 0x10
    public static class Try_000000A0$BurstDirectCall
    {
        // Fields
        private static System.IntPtr Pointer;  // static @ 0x0
        private static System.IntPtr DeferredCompilation;  // static @ 0x8

        // Methods
        // RVA: 0x0B163EFC  token: 0x600005C
        private static System.Void GetFunctionPointerDiscard(System.IntPtr& ) { }
        // RVA: 0x0B164000  token: 0x600005D
        private static System.IntPtr GetFunctionPointer() { }
        // RVA: 0x03FB0750  token: 0x600005E
        public static System.Void Constructor() { }
        // RVA: 0x041E1670  token: 0x600005F
        public static System.Void Initialize() { }
        // RVA: 0x03FAEA50  token: 0x6000060
        private static System.Void .cctor() { }
        // RVA: 0x0B164054  token: 0x6000061
        public static System.Int32 Invoke(System.IntPtr allocatorState, Unity.Collections.AllocatorManager.Block& block) { }

    }

    // TypeToken: 0x2000021  // size: 0x40
    public sealed struct StackAllocator : IAllocator, System.IDisposable
    {
        // Fields
        private Unity.Collections.AllocatorManager.AllocatorHandle m_handle;  // 0x10
        private Unity.Collections.AllocatorManager.Block m_storage;  // 0x18
        private System.Int64 m_top;  // 0x38

        // Properties
        Unity.Collections.AllocatorManager.AllocatorHandle Handle { get; /* RVA: 0x02B77620 */ }

        // Methods
        // RVA: 0x0B163D48  token: 0x6000056
        public virtual System.Int32 Try(Unity.Collections.AllocatorManager.Block& block) { }
        // RVA: 0x0B163CEC  token: 0x6000057
        public static System.Int32 Try(System.IntPtr allocatorState, Unity.Collections.AllocatorManager.Block& block) { }
        // RVA: 0x041E1670  token: 0x6000058
        public virtual System.Void Dispose() { }
        // RVA: 0x0B163CE4  token: 0x6000059
        public static System.Int32 Try$BurstManaged(System.IntPtr allocatorState, Unity.Collections.AllocatorManager.Block& block) { }

    }

    // TypeToken: 0x2000025  // size: 0x80
    public sealed class Try_000000AE$PostfixBurstDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B163E34  token: 0x6000068
        public System.Void .ctor(System.Object , System.IntPtr ) { }
        // RVA: 0x015802E0  token: 0x6000069
        public virtual System.Int32 Invoke(System.IntPtr allocatorState, Unity.Collections.AllocatorManager.Block& block) { }

    }

    // TypeToken: 0x2000026  // size: 0x10
    public static class Try_000000AE$BurstDirectCall
    {
        // Fields
        private static System.IntPtr Pointer;  // static @ 0x0
        private static System.IntPtr DeferredCompilation;  // static @ 0x8

        // Methods
        // RVA: 0x0B1640E0  token: 0x600006A
        private static System.Void GetFunctionPointerDiscard(System.IntPtr& ) { }
        // RVA: 0x0B1641E4  token: 0x600006B
        private static System.IntPtr GetFunctionPointer() { }
        // RVA: 0x03FB07E0  token: 0x600006C
        public static System.Void Constructor() { }
        // RVA: 0x041E1670  token: 0x600006D
        public static System.Void Initialize() { }
        // RVA: 0x03FAEA60  token: 0x600006E
        private static System.Void .cctor() { }
        // RVA: 0x0B164238  token: 0x600006F
        public static System.Int32 Invoke(System.IntPtr allocatorState, Unity.Collections.AllocatorManager.Block& block) { }

    }

    // TypeToken: 0x2000024  // size: 0x1050
    public sealed struct SlabAllocator : IAllocator, System.IDisposable
    {
        // Fields
        private Unity.Collections.AllocatorManager.AllocatorHandle m_handle;  // 0x10
        private Unity.Collections.AllocatorManager.Block Storage;  // 0x18
        private System.Int32 Log2SlabSizeInBytes;  // 0x38
        private Unity.Collections.FixedList4096Bytes<System.Int32> Occupied;  // 0x3c
        private System.Int64 budgetInBytes;  // 0x1040
        private System.Int64 allocatedBytes;  // 0x1048

        // Properties
        Unity.Collections.AllocatorManager.AllocatorHandle Handle { get; /* RVA: 0x02B77620 */ }
        System.Int32 SlabSizeInBytes { get; /* RVA: 0x04DAB540 */ }

        // Methods
        // RVA: 0x0B163AC8  token: 0x6000064
        public virtual System.Int32 Try(Unity.Collections.AllocatorManager.Block& block) { }
        // RVA: 0x0B163A6C  token: 0x6000065
        public static System.Int32 Try(System.IntPtr allocatorState, Unity.Collections.AllocatorManager.Block& block) { }
        // RVA: 0x041E1670  token: 0x6000066
        public virtual System.Void Dispose() { }
        // RVA: 0x0B163A64  token: 0x6000067
        public static System.Int32 Try$BurstManaged(System.IntPtr allocatorState, Unity.Collections.AllocatorManager.Block& block) { }

    }

    // TypeToken: 0x2000027  // size: 0x20
    public sealed struct TableEntry
    {
        // Fields
        private System.IntPtr function;  // 0x10
        private System.IntPtr state;  // 0x18

    }

    // TypeToken: 0x2000028
    public sealed struct Array16`1
    {
        // Fields
        private T f0;  // 0x0
        private T f1;  // 0x0
        private T f2;  // 0x0
        private T f3;  // 0x0
        private T f4;  // 0x0
        private T f5;  // 0x0
        private T f6;  // 0x0
        private T f7;  // 0x0
        private T f8;  // 0x0
        private T f9;  // 0x0
        private T f10;  // 0x0
        private T f11;  // 0x0
        private T f12;  // 0x0
        private T f13;  // 0x0
        private T f14;  // 0x0
        private T f15;  // 0x0

    }

    // TypeToken: 0x2000029
    public sealed struct Array256`1
    {
        // Fields
        private Unity.Collections.AllocatorManager.Array16<T> f0;  // 0x0
        private Unity.Collections.AllocatorManager.Array16<T> f1;  // 0x0
        private Unity.Collections.AllocatorManager.Array16<T> f2;  // 0x0
        private Unity.Collections.AllocatorManager.Array16<T> f3;  // 0x0
        private Unity.Collections.AllocatorManager.Array16<T> f4;  // 0x0
        private Unity.Collections.AllocatorManager.Array16<T> f5;  // 0x0
        private Unity.Collections.AllocatorManager.Array16<T> f6;  // 0x0
        private Unity.Collections.AllocatorManager.Array16<T> f7;  // 0x0
        private Unity.Collections.AllocatorManager.Array16<T> f8;  // 0x0
        private Unity.Collections.AllocatorManager.Array16<T> f9;  // 0x0
        private Unity.Collections.AllocatorManager.Array16<T> f10;  // 0x0
        private Unity.Collections.AllocatorManager.Array16<T> f11;  // 0x0
        private Unity.Collections.AllocatorManager.Array16<T> f12;  // 0x0
        private Unity.Collections.AllocatorManager.Array16<T> f13;  // 0x0
        private Unity.Collections.AllocatorManager.Array16<T> f14;  // 0x0
        private Unity.Collections.AllocatorManager.Array16<T> f15;  // 0x0

    }

    // TypeToken: 0x200002A
    public sealed struct Array4096`1
    {
        // Fields
        private Unity.Collections.AllocatorManager.Array256<T> f0;  // 0x0
        private Unity.Collections.AllocatorManager.Array256<T> f1;  // 0x0
        private Unity.Collections.AllocatorManager.Array256<T> f2;  // 0x0
        private Unity.Collections.AllocatorManager.Array256<T> f3;  // 0x0
        private Unity.Collections.AllocatorManager.Array256<T> f4;  // 0x0
        private Unity.Collections.AllocatorManager.Array256<T> f5;  // 0x0
        private Unity.Collections.AllocatorManager.Array256<T> f6;  // 0x0
        private Unity.Collections.AllocatorManager.Array256<T> f7;  // 0x0
        private Unity.Collections.AllocatorManager.Array256<T> f8;  // 0x0
        private Unity.Collections.AllocatorManager.Array256<T> f9;  // 0x0
        private Unity.Collections.AllocatorManager.Array256<T> f10;  // 0x0
        private Unity.Collections.AllocatorManager.Array256<T> f11;  // 0x0
        private Unity.Collections.AllocatorManager.Array256<T> f12;  // 0x0
        private Unity.Collections.AllocatorManager.Array256<T> f13;  // 0x0
        private Unity.Collections.AllocatorManager.Array256<T> f14;  // 0x0
        private Unity.Collections.AllocatorManager.Array256<T> f15;  // 0x0

    }

    // TypeToken: 0x200002B
    public sealed struct Array32768`1 : Unity.Collections.IIndexable`1
    {
        // Fields
        private Unity.Collections.AllocatorManager.Array4096<T> f0;  // 0x0
        private Unity.Collections.AllocatorManager.Array4096<T> f1;  // 0x0
        private Unity.Collections.AllocatorManager.Array4096<T> f2;  // 0x0
        private Unity.Collections.AllocatorManager.Array4096<T> f3;  // 0x0
        private Unity.Collections.AllocatorManager.Array4096<T> f4;  // 0x0
        private Unity.Collections.AllocatorManager.Array4096<T> f5;  // 0x0
        private Unity.Collections.AllocatorManager.Array4096<T> f6;  // 0x0
        private Unity.Collections.AllocatorManager.Array4096<T> f7;  // 0x0

        // Properties
        System.Int32 Length { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000071
        public virtual T& ElementAt(System.Int32 index) { }

    }

    // TypeToken: 0x200002D  // size: 0x10
    public sealed class TableEntry
    {
        // Fields
        private static readonly Unity.Burst.SharedStatic<Unity.Collections.AllocatorManager.Array32768<Unity.Collections.AllocatorManager.TableEntry>> Ref;  // static @ 0x0

        // Methods
        // RVA: 0x0B163DD4  token: 0x6000072
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200002C  // size: 0x10
    public sealed class SharedStatics
    {
    }

    // TypeToken: 0x200002E  // size: 0x10
    public static class Managed
    {
        // Fields
        private static Unity.Collections.AllocatorManager.TryFunction[] TryFunctionDelegates;  // static @ 0x0

        // Methods
        // RVA: 0x0B1632EC  token: 0x6000073
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000044
    public sealed struct Enumerator : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private Unity.Collections.FixedList512Bytes<T> m_List;  // 0x0
        private System.Int32 m_Index;  // 0x0

        // Properties
        T Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600010C
        public System.Void .ctor(Unity.Collections.FixedList512Bytes<T>& list) { }
        // RVA: -1  // not resolved  token: 0x600010D
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x600010E
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x600010F
        public virtual System.Void Reset() { }

    }

    // TypeToken: 0x200005A  // size: 0x11
    public sealed struct Array
    {
        // Methods
        // RVA: 0x03347DF0  token: 0x60001C4
        private static System.Boolean IsCustom(Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: 0x0B15FEF8  token: 0x60001C5
        private static System.Void* CustomResize(System.Void* oldPointer, System.Int64 oldCount, System.Int64 newCount, Unity.Collections.AllocatorManager.AllocatorHandle allocator, System.Int64 size, System.Int32 align) { }
        // RVA: 0x03347CE0  token: 0x60001C6
        private static System.Void* Resize(System.Void* oldPointer, System.Int64 oldCount, System.Int64 newCount, Unity.Collections.AllocatorManager.AllocatorHandle allocator, System.Int64 size, System.Int32 align) { }
        // RVA: -1  // generic def  token: 0x60001C7
        private static T* Resize(T* oldPointer, System.Int64 oldCount, System.Int64 newCount, Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }

    }

    // TypeToken: 0x2000059  // size: 0x11
    public sealed struct Unmanaged
    {
        // Methods
        // RVA: 0x03347C10  token: 0x60001C1
        private static System.Void* Allocate(System.Int64 size, System.Int32 align, Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: 0x03CCB260  token: 0x60001C2
        private static System.Void Free(System.Void* pointer, Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // generic def  token: 0x60001C3
        private static System.Void Free(T* pointer, Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }

    }

    // TypeToken: 0x200005F
    public sealed struct ParallelWriter
    {
        // Fields
        public Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* ListData;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001F4
        private System.Void .ctor(Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* listData) { }
        // RVA: -1  // not resolved  token: 0x60001F5
        public System.Void AddNoResize(T value) { }

    }

    // TypeToken: 0x2000064
    public sealed struct ParallelWriter
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap.ParallelWriter<TKey,TValue> m_Writer;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600020F
        public System.Boolean TryAdd(TKey key, TValue item) { }

    }

    // TypeToken: 0x2000065
    public sealed struct Enumerator : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataEnumerator m_Enumerator;  // 0x0

        // Properties
        Unity.Collections.LowLevel.Unsafe.KeyValue<TKey,TValue> Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000210
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000211
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000212
        public virtual System.Void Reset() { }

    }

    // TypeToken: 0x2000069
    public sealed struct Enumerator : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataEnumerator m_Enumerator;  // 0x0

        // Properties
        T Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000223
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000224
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000225
        public virtual System.Void Reset() { }

    }

    // TypeToken: 0x200006D
    public sealed struct ParallelWriter
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap.ParallelWriter<TKey,TValue> m_Writer;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600023F
        public System.Void Add(TKey key, TValue item) { }

    }

    // TypeToken: 0x200006E
    public sealed struct Enumerator : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private Unity.Collections.NativeParallelMultiHashMap<TKey,TValue> hashmap;  // 0x0
        private TKey key;  // 0x0
        private System.Boolean isFirst;  // 0x0
        private TValue value;  // 0x0
        private Unity.Collections.NativeParallelMultiHashMapIterator<TKey> iterator;  // 0x0

        // Properties
        TValue Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000240
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000241
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000242
        public virtual System.Void Reset() { }
        // RVA: -1  // not resolved  token: 0x6000245
        public Unity.Collections.NativeParallelMultiHashMap.Enumerator<TKey,TValue> GetEnumerator() { }

    }

    // TypeToken: 0x2000075
    public sealed struct ParallelWriter
    {
        // Fields
        private Unity.Collections.NativeQueueData* m_Buffer;  // 0x0
        private Unity.Collections.NativeQueueBlockPoolData* m_QueuePool;  // 0x0
        private System.Int32 m_ThreadIndex;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600025C
        public System.Void Enqueue(T value) { }

    }

    // TypeToken: 0x200007C  // size: 0x11
    public sealed struct DefaultComparer`1 : System.Collections.Generic.IComparer`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x600027B
        public virtual System.Int32 Compare(T x, T y) { }

    }

    // TypeToken: 0x200007E
    public sealed struct SegmentSort : Unity.Jobs.IJobParallelFor
    {
        // Fields
        public T* Data;  // 0x0
        public U Comp;  // 0x0
        public System.Int32 Length;  // 0x0
        public System.Int32 SegmentWidth;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600027D
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x200007F
    public sealed struct SegmentSortMerge : Unity.Jobs.IJob
    {
        // Fields
        public T* Data;  // 0x0
        public U Comp;  // 0x0
        public System.Int32 Length;  // 0x0
        public System.Int32 SegmentWidth;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600027E
        public virtual System.Void Execute() { }

    }

    // TypeToken: 0x2000081  // size: 0x28
    public sealed struct ConstructJobList : Unity.Jobs.IJob
    {
        // Fields
        public Unity.Collections.NativeStream Container;  // 0x10
        public Unity.Collections.LowLevel.Unsafe.UntypedUnsafeList* List;  // 0x20

        // Methods
        // RVA: 0x0B160024  token: 0x6000281
        public virtual System.Void Execute() { }

    }

    // TypeToken: 0x2000082  // size: 0x30
    public sealed struct ConstructJob : Unity.Jobs.IJob
    {
        // Fields
        public Unity.Collections.NativeStream Container;  // 0x10
        public Unity.Collections.NativeArray<System.Int32> Length;  // 0x20

        // Methods
        // RVA: 0x0B160048  token: 0x6000282
        public virtual System.Void Execute() { }

    }

    // TypeToken: 0x2000086  // size: 0x30
    public sealed struct MemoryBlock : System.IDisposable
    {
        // Fields
        public System.Byte* m_pointer;  // 0x10
        public System.Int64 m_bytes;  // 0x18
        public System.Int64 m_current;  // 0x20
        public System.Int64 m_allocations;  // 0x28

        // Methods
        // RVA: 0x0B1634A8  token: 0x600028D
        public System.Void .ctor(System.Int64 bytes) { }
        // RVA: 0x04D92200  token: 0x600028E
        public System.Void Rewind() { }
        // RVA: 0x0B16336C  token: 0x600028F
        public virtual System.Void Dispose() { }
        // RVA: 0x0B1633CC  token: 0x6000290
        public System.Int32 TryAllocate(Unity.Collections.AllocatorManager.Block& block) { }
        // RVA: 0x0B163354  token: 0x6000291
        public System.Boolean Contains(System.IntPtr ptr) { }

    }

    // TypeToken: 0x200008D  // size: 0x14
    public sealed struct Rune
    {
        // Fields
        public System.Int32 value;  // 0x10

    }

    // TypeToken: 0x200008F  // size: 0x18
    public sealed struct Comparison
    {
        // Fields
        public System.Boolean terminates;  // 0x10
        public System.Int32 result;  // 0x14

        // Methods
        // RVA: 0x0B15FFF4  token: 0x60002AA
        public System.Void .ctor(Unity.Collections.Unicode.Rune runeA, Unity.Collections.ConversionError errorA, Unity.Collections.Unicode.Rune runeB, Unity.Collections.ConversionError errorB) { }

    }

    // TypeToken: 0x2000091  // size: 0x20
    public sealed struct ulong2
    {
        // Fields
        public System.UInt64 x;  // 0x10
        public System.UInt64 y;  // 0x18

        // Methods
        // RVA: 0x04D861B0  token: 0x60002DB
        public System.Void .ctor(System.UInt64 x, System.UInt64 y) { }

    }

    // TypeToken: 0x200009E
    public sealed struct ParallelReader
    {
        // Fields
        public readonly T* Ptr;  // 0x0
        public readonly System.Int32 Length;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000314
        private System.Void .ctor(T* ptr, System.Int32 length) { }

    }

    // TypeToken: 0x200009F
    public sealed struct ParallelWriter
    {
        // Fields
        public Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* ListData;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000315
        private System.Void .ctor(Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* listData) { }
        // RVA: -1  // not resolved  token: 0x6000316
        public System.Void AddNoResize(T value) { }

    }

    // TypeToken: 0x20000A2  // size: 0x2010
    public sealed struct <firstFreeTLS>e__FixedBuffer
    {
        // Fields
        public System.Int32 FixedElementField;  // 0x10

    }

    // TypeToken: 0x20000A9
    public sealed struct ParallelWriter
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* m_Buffer;  // 0x0
        private System.Int32 m_ThreadIndex;  // 0x0

        // Properties
        System.Int32 Capacity { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000349
        public System.Boolean TryAdd(TKey key, TValue item) { }

    }

    // TypeToken: 0x20000AD
    public sealed struct Enumerator : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataEnumerator m_Enumerator;  // 0x0

        // Properties
        T Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000354
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000355
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000356
        public virtual System.Void Reset() { }

    }

    // TypeToken: 0x20000B1
    public sealed struct ParallelWriter
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* m_Buffer;  // 0x0
        private System.Int32 m_ThreadIndex;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600036C
        public System.Void Add(TKey key, TValue item) { }

    }

    // TypeToken: 0x20000B4  // size: 0x11
    public sealed struct <Data>e__FixedBuffer
    {
        // Fields
        public System.Byte FixedElementField;  // 0x10

    }

    // TypeToken: 0x20000B8  // size: 0x20
    public sealed struct DisposeJob : Unity.Jobs.IJob
    {
        // Fields
        public Unity.Collections.LowLevel.Unsafe.UnsafeStream Container;  // 0x10

        // Methods
        // RVA: 0x0B160058  token: 0x6000370
        public virtual System.Void Execute() { }

    }

    // TypeToken: 0x20000B9  // size: 0x28
    public sealed struct ConstructJobList : Unity.Jobs.IJob
    {
        // Fields
        public Unity.Collections.LowLevel.Unsafe.UnsafeStream Container;  // 0x10
        public Unity.Collections.LowLevel.Unsafe.UntypedUnsafeList* List;  // 0x20

        // Methods
        // RVA: 0x0B160024  token: 0x6000371
        public virtual System.Void Execute() { }

    }

    // TypeToken: 0x20000BA  // size: 0x30
    public sealed struct ConstructJob : Unity.Jobs.IJob
    {
        // Fields
        public Unity.Collections.LowLevel.Unsafe.UnsafeStream Container;  // 0x10
        public Unity.Collections.NativeArray<System.Int32> Length;  // 0x20

        // Methods
        // RVA: 0x0B160048  token: 0x6000372
        public virtual System.Void Execute() { }

    }

    // TypeToken: 0x20000BD  // size: 0xD0
    public sealed struct __StaticArrayInitTypeSize=192
    {
    }

    // TypeToken: 0x20000BC  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=192 2CF2F88BF9B71283059B6DF53E5BCDE20ADBFD9E8D6CE2C1AB106262BB283BED;  // static @ 0x0

    }

    // TypeToken: 0x20000BE  // size: 0x10
    public class __JobReflectionRegistrationOutput__486165037
    {
        // Methods
        // RVA: 0x02EC09F0  token: 0x6000376
        public static System.Void CreateJobReflectionData() { }
        // RVA: 0x02EC2B20  token: 0x6000377
        public static System.Void EarlyInit() { }

    }

    // TypeToken: 0x20000BF  // size: 0x10
    public static class $BurstDirectCallInitializer
    {
        // Methods
        // RVA: 0x04CAF720  token: 0x6000378
        private static System.Void Initialize() { }

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

namespace Unity.Collections
{

    // TypeToken: 0x200001B  // size: 0x10
    public static class AllocatorManager
    {
        // Fields
        public static readonly Unity.Collections.AllocatorManager.AllocatorHandle Invalid;  // static @ 0x0
        public static readonly Unity.Collections.AllocatorManager.AllocatorHandle None;  // static @ 0x4
        public static readonly Unity.Collections.AllocatorManager.AllocatorHandle Temp;  // static @ 0x8
        public static readonly Unity.Collections.AllocatorManager.AllocatorHandle TempJob;  // static @ 0xc
        public static readonly Unity.Collections.AllocatorManager.AllocatorHandle Persistent;  // static @ 0x10
        public static readonly Unity.Collections.AllocatorManager.AllocatorHandle AudioKernel;  // static @ 0x14

        // Methods
        // RVA: -1  // generic def  token: 0x600002C
        private static Unity.Collections.AllocatorManager.Block AllocateBlock(T& t, System.Int32 sizeOf, System.Int32 alignOf, System.Int32 items) { }
        // RVA: -1  // generic def  token: 0x600002D
        private static System.Void* Allocate(T& t, System.Int32 sizeOf, System.Int32 alignOf, System.Int32 items) { }
        // RVA: -1  // generic def  token: 0x600002E
        private static U* Allocate(T& t, U u, System.Int32 items) { }
        // RVA: -1  // generic def  token: 0x600002F
        private static System.Void* AllocateStruct(T& t, U u, System.Int32 items) { }
        // RVA: -1  // generic def  token: 0x6000030
        private static System.Void FreeBlock(T& t, Unity.Collections.AllocatorManager.Block& block) { }
        // RVA: -1  // generic def  token: 0x6000031
        private static System.Void Free(T& t, System.Void* pointer, System.Int32 sizeOf, System.Int32 alignOf, System.Int32 items) { }
        // RVA: -1  // generic def  token: 0x6000032
        private static System.Void Free(T& t, U* pointer, System.Int32 items) { }
        // RVA: 0x03345120  token: 0x6000033
        public static System.Void* Allocate(Unity.Collections.AllocatorManager.AllocatorHandle handle, System.Int32 itemSizeInBytes, System.Int32 alignmentInBytes, System.Int32 items) { }
        // RVA: -1  // generic def  token: 0x6000034
        public static T* Allocate(Unity.Collections.AllocatorManager.AllocatorHandle handle, System.Int32 items) { }
        // RVA: 0x0B15FCC4  token: 0x6000035
        public static System.Void Free(Unity.Collections.AllocatorManager.AllocatorHandle handle, System.Void* pointer) { }
        // RVA: -1  // generic def  token: 0x6000036
        public static System.Void Free(Unity.Collections.AllocatorManager.AllocatorHandle handle, T* pointer, System.Int32 items) { }
        // RVA: 0x04D910F0  token: 0x6000037
        private static System.Void CheckDelegate(System.Boolean& useDelegate) { }
        // RVA: 0x0B15FD24  token: 0x6000038
        private static System.Boolean UseDelegate() { }
        // RVA: 0x0B15FD60  token: 0x6000039
        private static System.Int32 allocate_block(Unity.Collections.AllocatorManager.Block& block) { }
        // RVA: 0x0B15FDF0  token: 0x600003A
        private static System.Void forward_mono_allocate_block(Unity.Collections.AllocatorManager.Block& block, System.Int32& error) { }
        // RVA: 0x03347C00  token: 0x600003B
        private static Unity.Collections.Allocator LegacyOf(Unity.Collections.AllocatorManager.AllocatorHandle handle) { }
        // RVA: 0x03347AA0  token: 0x600003C
        private static System.Int32 TryLegacy(Unity.Collections.AllocatorManager.Block& block) { }
        // RVA: 0x03347A60  token: 0x600003D
        public static System.Int32 Try(Unity.Collections.AllocatorManager.Block& block) { }
        // RVA: 0x03347DF0  token: 0x600003E
        private static System.Boolean IsCustomAllocator(Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: 0x04CA1340  token: 0x600003F
        private static System.Void .cctor() { }
        // RVA: 0x04CAF810  token: 0x6000040
        public static System.Void Initialize$StackAllocator_Try_000000A0$BurstDirectCall() { }
        // RVA: 0x04CAF7D0  token: 0x6000041
        public static System.Void Initialize$SlabAllocator_Try_000000AE$BurstDirectCall() { }

    }

    // TypeToken: 0x200002F  // size: 0x10
    public class CreatePropertyAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000074
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000030  // size: 0x11
    public sealed struct Bitwise
    {
        // Methods
        // RVA: 0x038C4530  token: 0x6000075
        private static System.Int32 FromBool(System.Boolean value) { }
        // RVA: 0x04DAB470  token: 0x6000076
        private static System.UInt32 ExtractBits(System.UInt32 input, System.Int32 pos, System.UInt32 mask) { }
        // RVA: 0x04DAB4B0  token: 0x6000077
        private static System.UInt32 ReplaceBits(System.UInt32 input, System.Int32 pos, System.UInt32 mask, System.UInt32 value) { }
        // RVA: 0x038C44E0  token: 0x6000078
        private static System.UInt32 SetBits(System.UInt32 input, System.Int32 pos, System.UInt32 mask, System.Boolean value) { }
        // RVA: 0x04DAB460  token: 0x6000079
        private static System.UInt64 ExtractBits(System.UInt64 input, System.Int32 pos, System.UInt64 mask) { }
        // RVA: 0x04DAB480  token: 0x600007A
        private static System.UInt64 ReplaceBits(System.UInt64 input, System.Int32 pos, System.UInt64 mask, System.UInt64 value) { }
        // RVA: 0x038C4500  token: 0x600007B
        private static System.UInt64 SetBits(System.UInt64 input, System.Int32 pos, System.UInt64 mask, System.Boolean value) { }

    }

    // TypeToken: 0x2000031  // size: 0x14
    public sealed struct BitField32
    {
        // Fields
        public System.UInt32 Value;  // 0x10

        // Methods
        // RVA: 0x05390AE8  token: 0x600007C
        public System.Void SetBits(System.Int32 pos, System.Boolean value) { }
        // RVA: 0x04DAB410  token: 0x600007D
        public System.UInt32 GetBits(System.Int32 pos, System.Int32 numBits) { }
        // RVA: 0x02F95910  token: 0x600007E
        public System.Boolean IsSet(System.Int32 pos) { }

    }

    // TypeToken: 0x2000032  // size: 0x10
    public sealed class BitField32DebugView
    {
    }

    // TypeToken: 0x2000033  // size: 0x18
    public sealed struct BitField64
    {
        // Fields
        public System.UInt64 Value;  // 0x10

        // Methods
        // RVA: 0x05390AB4  token: 0x600007F
        public System.Void SetBits(System.Int32 pos, System.Boolean value) { }
        // RVA: 0x04DAB430  token: 0x6000080
        public System.UInt64 GetBits(System.Int32 pos, System.Int32 numBits) { }
        // RVA: 0x037EBDF0  token: 0x6000081
        public System.Boolean IsSet(System.Int32 pos) { }
        // RVA: 0x037EDE40  token: 0x6000082
        public System.Boolean TestAny(System.Int32 pos, System.Int32 numBits) { }

    }

    // TypeToken: 0x2000034  // size: 0x10
    public sealed class BitField64DebugView
    {
    }

    // TypeToken: 0x2000035  // size: 0x20
    public class BurstCompatibleAttribute : System.Attribute
    {
        // Fields
        private System.Type[] <GenericTypeArguments>k__BackingField;  // 0x10
        public System.String RequiredUnityDefine;  // 0x18

        // Properties
        System.Type[] GenericTypeArguments { set; /* RVA: 0x053908C0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000084
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000036  // size: 0x10
    public class NotBurstCompatibleAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000085
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000037
    public interface INativeDisposable : System.IDisposable
    {
    }

    // TypeToken: 0x2000038  // size: 0x10
    public static class CollectionHelper
    {
        // Methods
        // RVA: 0x05F0E0A0  token: 0x6000086
        public static System.Int32 Log2Floor(System.Int32 value) { }
        // RVA: 0x037F5AD0  token: 0x6000087
        public static System.Int32 Align(System.Int32 size, System.Int32 alignmentPowerOfTwo) { }
        // RVA: 0x043CED80  token: 0x6000088
        public static System.UInt32 Hash(System.Void* ptr, System.Int32 bytes) { }
        // RVA: 0x03CCB240  token: 0x6000089
        private static System.Boolean ShouldDeallocate(Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: 0x04D88BE0  token: 0x600008A
        private static System.Int32 AssumePositive(System.Int32 value) { }
        // RVA: -1  // generic def  token: 0x600008B
        public static Unity.Collections.NativeArray<T> CreateNativeArray(System.Int32 length, Unity.Collections.AllocatorManager.AllocatorHandle allocator, Unity.Collections.NativeArrayOptions options) { }
        // RVA: -1  // generic def  token: 0x600008C
        public static Unity.Collections.NativeArray<T> CreateNativeArray(T[] array, Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }

    }

    // TypeToken: 0x2000039  // size: 0x11
    public sealed struct FixedList
    {
        // Methods
        // RVA: -1  // generic def  token: 0x600008D
        private static System.Int32 PaddingBytes() { }
        // RVA: -1  // generic def  token: 0x600008E
        private static System.Int32 StorageBytes() { }
        // RVA: -1  // generic def  token: 0x600008F
        private static System.Int32 Capacity() { }

    }

    // TypeToken: 0x200003A
    public sealed struct FixedList32Bytes`1 : Unity.Collections.INativeList`1, Unity.Collections.IIndexable`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1
    {
        // Fields
        private System.UInt16 length;  // 0x0
        private Unity.Collections.FixedBytes30 buffer;  // 0x0

        // Properties
        System.Int32 Length { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Int32 LengthInBytes { get; /* RVA: -1  // not resolved */ }
        System.Byte* Buffer { get; /* RVA: -1  // not resolved */ }
        System.Int32 Capacity { get; /* RVA: -1  // not resolved */ }
        T Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000097
        public virtual T& ElementAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000098
        public virtual System.Int32 GetHashCode() { }
        // RVA: -1  // not resolved  token: 0x6000099
        public System.Void Add(T& item) { }
        // RVA: -1  // not resolved  token: 0x600009A
        public System.Void RemoveAtSwapBack(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x600009B
        public System.Void RemoveRangeSwapBack(System.Int32 index, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x600009C
        public System.Void RemoveAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x600009D
        public System.Void RemoveRange(System.Int32 index, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x600009E
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList32Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x600009F
        public virtual System.Boolean Equals(Unity.Collections.FixedList32Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000A0
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList64Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000A1
        public virtual System.Boolean Equals(Unity.Collections.FixedList64Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000A2
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList128Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000A3
        public virtual System.Boolean Equals(Unity.Collections.FixedList128Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000A4
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList512Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000A5
        public virtual System.Boolean Equals(Unity.Collections.FixedList512Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000A6
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList4096Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000A7
        public virtual System.Boolean Equals(Unity.Collections.FixedList4096Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000A8
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: -1  // not resolved  token: 0x60000A9
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60000AA
        private virtual System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator() { }

    }

    // TypeToken: 0x200003B  // size: 0x10
    public static class FixedList32BytesExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x60000AB
        public static System.Int32 IndexOf(Unity.Collections.FixedList32Bytes<T>& list, U value) { }
        // RVA: -1  // generic def  token: 0x60000AC
        public static System.Boolean Contains(Unity.Collections.FixedList32Bytes<T>& list, U value) { }

    }

    // TypeToken: 0x200003C
    public sealed class FixedList32BytesDebugView`1
    {
    }

    // TypeToken: 0x200003D
    public sealed struct FixedList64Bytes`1 : Unity.Collections.INativeList`1, Unity.Collections.IIndexable`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1
    {
        // Fields
        private System.UInt16 length;  // 0x0
        private Unity.Collections.FixedBytes62 buffer;  // 0x0

        // Properties
        System.Int32 Length { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Int32 LengthInBytes { get; /* RVA: -1  // not resolved */ }
        System.Byte* Buffer { get; /* RVA: -1  // not resolved */ }
        System.Int32 Capacity { get; /* RVA: -1  // not resolved */ }
        T Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60000B4
        public virtual T& ElementAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60000B5
        public virtual System.Int32 GetHashCode() { }
        // RVA: -1  // not resolved  token: 0x60000B6
        public System.Void Add(T& item) { }
        // RVA: -1  // not resolved  token: 0x60000B7
        public System.Void RemoveAtSwapBack(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60000B8
        public System.Void RemoveRangeSwapBack(System.Int32 index, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x60000B9
        public System.Void RemoveAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60000BA
        public System.Void RemoveRange(System.Int32 index, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x60000BB
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList32Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000BC
        public virtual System.Boolean Equals(Unity.Collections.FixedList32Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000BD
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList64Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000BE
        public virtual System.Boolean Equals(Unity.Collections.FixedList64Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000BF
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList128Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000C0
        public virtual System.Boolean Equals(Unity.Collections.FixedList128Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000C1
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList512Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000C2
        public virtual System.Boolean Equals(Unity.Collections.FixedList512Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000C3
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList4096Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000C4
        public virtual System.Boolean Equals(Unity.Collections.FixedList4096Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000C5
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: -1  // not resolved  token: 0x60000C6
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60000C7
        private virtual System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator() { }

    }

    // TypeToken: 0x200003E  // size: 0x10
    public static class FixedList64BytesExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x60000C8
        public static System.Int32 IndexOf(Unity.Collections.FixedList64Bytes<T>& list, U value) { }
        // RVA: -1  // generic def  token: 0x60000C9
        public static System.Boolean Contains(Unity.Collections.FixedList64Bytes<T>& list, U value) { }

    }

    // TypeToken: 0x200003F
    public sealed class FixedList64BytesDebugView`1
    {
    }

    // TypeToken: 0x2000040
    public sealed struct FixedList128Bytes`1 : Unity.Collections.INativeList`1, Unity.Collections.IIndexable`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1
    {
        // Fields
        private System.UInt16 length;  // 0x0
        private Unity.Collections.FixedBytes126 buffer;  // 0x0

        // Properties
        System.Int32 Length { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Boolean IsEmpty { get; /* RVA: -1  // not resolved */ }
        System.Int32 LengthInBytes { get; /* RVA: -1  // not resolved */ }
        System.Byte* Buffer { get; /* RVA: -1  // not resolved */ }
        System.Int32 Capacity { get; /* RVA: -1  // not resolved */ }
        T Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60000D2
        public virtual T& ElementAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60000D3
        public virtual System.Int32 GetHashCode() { }
        // RVA: -1  // not resolved  token: 0x60000D4
        public System.Void Add(T& item) { }
        // RVA: -1  // not resolved  token: 0x60000D5
        public System.Void AddNoResize(T& item) { }
        // RVA: -1  // not resolved  token: 0x60000D6
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x60000D7
        public System.Void RemoveAtSwapBack(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60000D8
        public System.Void RemoveRangeSwapBack(System.Int32 index, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x60000D9
        public System.Void RemoveAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60000DA
        public System.Void RemoveRange(System.Int32 index, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x60000DB
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList32Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000DC
        public virtual System.Boolean Equals(Unity.Collections.FixedList32Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000DD
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList64Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000DE
        public virtual System.Boolean Equals(Unity.Collections.FixedList64Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000DF
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList128Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000E0
        public virtual System.Boolean Equals(Unity.Collections.FixedList128Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000E1
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList512Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000E2
        public virtual System.Boolean Equals(Unity.Collections.FixedList512Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000E3
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList4096Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000E4
        public virtual System.Boolean Equals(Unity.Collections.FixedList4096Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000E5
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: -1  // not resolved  token: 0x60000E6
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60000E7
        private virtual System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator() { }

    }

    // TypeToken: 0x2000041  // size: 0x10
    public static class FixedList128BytesExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x60000E8
        public static System.Int32 IndexOf(Unity.Collections.FixedList128Bytes<T>& list, U value) { }
        // RVA: -1  // generic def  token: 0x60000E9
        public static System.Boolean Contains(Unity.Collections.FixedList128Bytes<T>& list, U value) { }
        // RVA: -1  // generic def  token: 0x60000EA
        public static System.Boolean Remove(Unity.Collections.FixedList128Bytes<T>& list, U value) { }

    }

    // TypeToken: 0x2000042
    public sealed class FixedList128BytesDebugView`1
    {
    }

    // TypeToken: 0x2000043
    public sealed struct FixedList512Bytes`1 : Unity.Collections.INativeList`1, Unity.Collections.IIndexable`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1
    {
        // Fields
        private System.UInt16 length;  // 0x0
        private Unity.Collections.FixedBytes510 buffer;  // 0x0

        // Properties
        System.Int32 Length { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Boolean IsEmpty { get; /* RVA: -1  // not resolved */ }
        System.Int32 LengthInBytes { get; /* RVA: -1  // not resolved */ }
        System.Byte* Buffer { get; /* RVA: -1  // not resolved */ }
        System.Int32 Capacity { get; /* RVA: -1  // not resolved */ }
        T Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60000F3
        public virtual T& ElementAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60000F4
        public virtual System.Int32 GetHashCode() { }
        // RVA: -1  // not resolved  token: 0x60000F5
        public System.Void Add(T& item) { }
        // RVA: -1  // not resolved  token: 0x60000F6
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x60000F7
        public System.Void RemoveAtSwapBack(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60000F8
        public System.Void RemoveRangeSwapBack(System.Int32 index, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x60000F9
        public System.Void RemoveAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60000FA
        public System.Void RemoveRange(System.Int32 index, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x60000FB
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList32Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000FC
        public virtual System.Boolean Equals(Unity.Collections.FixedList32Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000FD
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList64Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000FE
        public virtual System.Boolean Equals(Unity.Collections.FixedList64Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x60000FF
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList128Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000100
        public virtual System.Boolean Equals(Unity.Collections.FixedList128Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000101
        public System.Void .ctor(Unity.Collections.FixedList128Bytes<T>& other) { }
        // RVA: -1  // not resolved  token: 0x6000102
        private System.Int32 Initialize(Unity.Collections.FixedList128Bytes<T>& other) { }
        // RVA: -1  // not resolved  token: 0x6000103
        public static Unity.Collections.FixedList512Bytes<T> op_Implicit(Unity.Collections.FixedList128Bytes<T>& other) { }
        // RVA: -1  // not resolved  token: 0x6000104
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList512Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000105
        public virtual System.Boolean Equals(Unity.Collections.FixedList512Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000106
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList4096Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000107
        public virtual System.Boolean Equals(Unity.Collections.FixedList4096Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000108
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: -1  // not resolved  token: 0x6000109
        public Unity.Collections.FixedList512Bytes.Enumerator<T> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600010A
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600010B
        private virtual System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator() { }

    }

    // TypeToken: 0x2000045  // size: 0x10
    public static class FixedList512BytesExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000112
        public static System.Int32 IndexOf(Unity.Collections.FixedList512Bytes<T>& list, U value) { }
        // RVA: -1  // generic def  token: 0x6000113
        public static System.Boolean Contains(Unity.Collections.FixedList512Bytes<T>& list, U value) { }
        // RVA: -1  // generic def  token: 0x6000114
        public static System.Boolean Remove(Unity.Collections.FixedList512Bytes<T>& list, U value) { }

    }

    // TypeToken: 0x2000046
    public sealed class FixedList512BytesDebugView`1
    {
    }

    // TypeToken: 0x2000047
    public sealed struct FixedList4096Bytes`1 : Unity.Collections.INativeList`1, Unity.Collections.IIndexable`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1
    {
        // Fields
        private System.UInt16 length;  // 0x0
        private Unity.Collections.FixedBytes4094 buffer;  // 0x0

        // Properties
        System.Int32 Length { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Boolean IsEmpty { get; /* RVA: -1  // not resolved */ }
        System.Int32 LengthInBytes { get; /* RVA: -1  // not resolved */ }
        System.Byte* Buffer { get; /* RVA: -1  // not resolved */ }
        System.Int32 Capacity { get; /* RVA: -1  // not resolved */ }
        T Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600011D
        public virtual T& ElementAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x600011E
        public virtual System.Int32 GetHashCode() { }
        // RVA: -1  // not resolved  token: 0x600011F
        public System.Void Add(T& item) { }
        // RVA: -1  // not resolved  token: 0x6000120
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000121
        public System.Void RemoveAtSwapBack(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000122
        public System.Void RemoveRangeSwapBack(System.Int32 index, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x6000123
        public System.Void RemoveAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000124
        public System.Void RemoveRange(System.Int32 index, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x6000125
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList32Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000126
        public virtual System.Boolean Equals(Unity.Collections.FixedList32Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000127
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList64Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000128
        public virtual System.Boolean Equals(Unity.Collections.FixedList64Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000129
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList128Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x600012A
        public virtual System.Boolean Equals(Unity.Collections.FixedList128Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x600012B
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList512Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x600012C
        public virtual System.Boolean Equals(Unity.Collections.FixedList512Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x600012D
        public virtual System.Int32 CompareTo(Unity.Collections.FixedList4096Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x600012E
        public virtual System.Boolean Equals(Unity.Collections.FixedList4096Bytes<T> other) { }
        // RVA: -1  // not resolved  token: 0x600012F
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: -1  // not resolved  token: 0x6000130
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000131
        private virtual System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator() { }

    }

    // TypeToken: 0x2000048  // size: 0x10
    public static class FixedList4096BytesExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000132
        public static System.Int32 IndexOf(Unity.Collections.FixedList4096Bytes<T>& list, U value) { }
        // RVA: -1  // generic def  token: 0x6000133
        public static System.Boolean Contains(Unity.Collections.FixedList4096Bytes<T>& list, U value) { }

    }

    // TypeToken: 0x2000049
    public sealed class FixedList4096BytesDebugView`1
    {
    }

    // TypeToken: 0x200004A  // size: 0x20
    public sealed struct FixedBytes16
    {
        // Fields
        public System.Byte byte0000;  // 0x10
        public System.Byte byte0001;  // 0x11
        public System.Byte byte0002;  // 0x12
        public System.Byte byte0003;  // 0x13
        public System.Byte byte0004;  // 0x14
        public System.Byte byte0005;  // 0x15
        public System.Byte byte0006;  // 0x16
        public System.Byte byte0007;  // 0x17
        public System.Byte byte0008;  // 0x18
        public System.Byte byte0009;  // 0x19
        public System.Byte byte0010;  // 0x1a
        public System.Byte byte0011;  // 0x1b
        public System.Byte byte0012;  // 0x1c
        public System.Byte byte0013;  // 0x1d
        public System.Byte byte0014;  // 0x1e
        public System.Byte byte0015;  // 0x1f

    }

    // TypeToken: 0x200004B  // size: 0x2E
    public sealed struct FixedBytes30
    {
        // Fields
        public Unity.Collections.FixedBytes16 offset0000;  // 0x10
        public System.Byte byte0016;  // 0x20
        public System.Byte byte0017;  // 0x21
        public System.Byte byte0018;  // 0x22
        public System.Byte byte0019;  // 0x23
        public System.Byte byte0020;  // 0x24
        public System.Byte byte0021;  // 0x25
        public System.Byte byte0022;  // 0x26
        public System.Byte byte0023;  // 0x27
        public System.Byte byte0024;  // 0x28
        public System.Byte byte0025;  // 0x29
        public System.Byte byte0026;  // 0x2a
        public System.Byte byte0027;  // 0x2b
        public System.Byte byte0028;  // 0x2c
        public System.Byte byte0029;  // 0x2d

    }

    // TypeToken: 0x200004C  // size: 0x30
    public sealed struct FixedString32Bytes : Unity.Collections.INativeList`1, Unity.Collections.IIndexable`1, Unity.Collections.IUTF8Bytes, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1
    {
        // Fields
        private static System.UInt16 utf8MaxLengthInBytes;  // const
        private System.UInt16 utf8LengthInBytes;  // 0x10
        private Unity.Collections.FixedBytes30 bytes;  // 0x12

        // Properties
        System.String Value { get; /* RVA: 0x05395BE4 */ }
        System.Int32 Length { get; /* RVA: 0x04D87110 */ set; /* RVA: 0x04DAB510 */ }
        System.Boolean IsEmpty { get; /* RVA: 0x0B160F5C */ }

        // Methods
        // RVA: 0x04DAB500  token: 0x6000135
        public virtual System.Byte* GetUnsafePtr() { }
        // RVA: 0x0B160EF0  token: 0x6000138
        public virtual System.Boolean TryResize(System.Int32 newLength, Unity.Collections.NativeArrayOptions clearOptions) { }
        // RVA: 0x0B160A44  token: 0x600013A
        public virtual System.Int32 CompareTo(System.String other) { }
        // RVA: 0x0B160B60  token: 0x600013B
        public virtual System.Boolean Equals(System.String other) { }
        // RVA: 0x038759B0  token: 0x600013C
        public System.Void .ctor(System.String source) { }
        // RVA: 0x038759D0  token: 0x600013D
        private System.Int32 Initialize(System.String source) { }
        // RVA: 0x0B160AC8  token: 0x600013E
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString32Bytes other) { }
        // RVA: 0x0B160FF0  token: 0x600013F
        public static System.Boolean op_Equality(Unity.Collections.FixedString32Bytes& a, Unity.Collections.FixedString32Bytes& b) { }
        // RVA: 0x0B160C0C  token: 0x6000140
        public virtual System.Boolean Equals(Unity.Collections.FixedString32Bytes other) { }
        // RVA: 0x0B160B14  token: 0x6000141
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString64Bytes other) { }
        // RVA: 0x0B160F64  token: 0x6000142
        public static System.Boolean op_Equality(Unity.Collections.FixedString32Bytes& a, Unity.Collections.FixedString64Bytes& b) { }
        // RVA: 0x0B160BF8  token: 0x6000143
        public virtual System.Boolean Equals(Unity.Collections.FixedString64Bytes other) { }
        // RVA: 0x0B1609F8  token: 0x6000144
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString128Bytes other) { }
        // RVA: 0x0B1610FC  token: 0x6000145
        public static System.Boolean op_Equality(Unity.Collections.FixedString32Bytes& a, Unity.Collections.FixedString128Bytes& b) { }
        // RVA: 0x0B160C20  token: 0x6000146
        public virtual System.Boolean Equals(Unity.Collections.FixedString128Bytes other) { }
        // RVA: 0x0B1609AC  token: 0x6000147
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString512Bytes other) { }
        // RVA: 0x0B161070  token: 0x6000148
        public static System.Boolean op_Equality(Unity.Collections.FixedString32Bytes& a, Unity.Collections.FixedString512Bytes& b) { }
        // RVA: 0x0B160BE4  token: 0x6000149
        public virtual System.Boolean Equals(Unity.Collections.FixedString512Bytes other) { }
        // RVA: 0x0B160A7C  token: 0x600014A
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString4096Bytes other) { }
        // RVA: 0x0B161188  token: 0x600014B
        public static System.Boolean op_Equality(Unity.Collections.FixedString32Bytes& a, Unity.Collections.FixedString4096Bytes& b) { }
        // RVA: 0x0B160EDC  token: 0x600014C
        public virtual System.Boolean Equals(Unity.Collections.FixedString4096Bytes other) { }
        // RVA: 0x03875980  token: 0x600014D
        public static Unity.Collections.FixedString32Bytes op_Implicit(System.String b) { }
        // RVA: 0x05395BE4  token: 0x600014E
        public virtual System.String ToString() { }
        // RVA: 0x034D7A80  token: 0x600014F
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B160C34  token: 0x6000150
        public virtual System.Boolean Equals(System.Object obj) { }

    }

    // TypeToken: 0x200004D  // size: 0x4E
    public sealed struct FixedBytes62
    {
        // Fields
        public Unity.Collections.FixedBytes16 offset0000;  // 0x10
        public Unity.Collections.FixedBytes16 offset0016;  // 0x20
        public Unity.Collections.FixedBytes16 offset0032;  // 0x30
        public System.Byte byte0048;  // 0x40
        public System.Byte byte0049;  // 0x41
        public System.Byte byte0050;  // 0x42
        public System.Byte byte0051;  // 0x43
        public System.Byte byte0052;  // 0x44
        public System.Byte byte0053;  // 0x45
        public System.Byte byte0054;  // 0x46
        public System.Byte byte0055;  // 0x47
        public System.Byte byte0056;  // 0x48
        public System.Byte byte0057;  // 0x49
        public System.Byte byte0058;  // 0x4a
        public System.Byte byte0059;  // 0x4b
        public System.Byte byte0060;  // 0x4c
        public System.Byte byte0061;  // 0x4d

    }

    // TypeToken: 0x200004E  // size: 0x50
    public sealed struct FixedString64Bytes : Unity.Collections.INativeList`1, Unity.Collections.IIndexable`1, Unity.Collections.IUTF8Bytes, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1
    {
        // Fields
        private static System.UInt16 utf8MaxLengthInBytes;  // const
        private System.UInt16 utf8LengthInBytes;  // 0x10
        private Unity.Collections.FixedBytes62 bytes;  // 0x12

        // Properties
        System.String Value { get; /* RVA: 0x053945DC */ }
        System.Int32 Length { get; /* RVA: 0x04D87110 */ set; /* RVA: 0x04DAB510 */ }

        // Methods
        // RVA: 0x04DAB500  token: 0x6000152
        public virtual System.Byte* GetUnsafePtr() { }
        // RVA: 0x0B1629BC  token: 0x6000155
        public virtual System.Boolean TryResize(System.Int32 newLength, Unity.Collections.NativeArrayOptions clearOptions) { }
        // RVA: 0x0B1624A8  token: 0x6000156
        public virtual System.Int32 CompareTo(System.String other) { }
        // RVA: 0x0B16285C  token: 0x6000157
        public virtual System.Boolean Equals(System.String other) { }
        // RVA: 0x0B162A28  token: 0x6000158
        public System.Void .ctor(System.String source) { }
        // RVA: 0x0B162944  token: 0x6000159
        private System.Int32 Initialize(System.String source) { }
        // RVA: 0x0B1624E0  token: 0x600015A
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString32Bytes other) { }
        // RVA: 0x0B162B5C  token: 0x600015B
        public static System.Boolean op_Equality(Unity.Collections.FixedString64Bytes& a, Unity.Collections.FixedString32Bytes& b) { }
        // RVA: 0x0B162820  token: 0x600015C
        public virtual System.Boolean Equals(Unity.Collections.FixedString32Bytes other) { }
        // RVA: 0x0B16252C  token: 0x600015D
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString64Bytes other) { }
        // RVA: 0x0B162BE8  token: 0x600015E
        public static System.Boolean op_Equality(Unity.Collections.FixedString64Bytes& a, Unity.Collections.FixedString64Bytes& b) { }
        // RVA: 0x0B162848  token: 0x600015F
        public virtual System.Boolean Equals(Unity.Collections.FixedString64Bytes other) { }
        // RVA: 0x0B162410  token: 0x6000160
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString128Bytes other) { }
        // RVA: 0x0B162AD0  token: 0x6000161
        public static System.Boolean op_Equality(Unity.Collections.FixedString64Bytes& a, Unity.Collections.FixedString128Bytes& b) { }
        // RVA: 0x0B162834  token: 0x6000162
        public virtual System.Boolean Equals(Unity.Collections.FixedString128Bytes other) { }
        // RVA: 0x0B1623C4  token: 0x6000163
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString512Bytes other) { }
        // RVA: 0x0B162A44  token: 0x6000164
        public static System.Boolean op_Equality(Unity.Collections.FixedString64Bytes& a, Unity.Collections.FixedString512Bytes& b) { }
        // RVA: 0x0B1628E0  token: 0x6000165
        public virtual System.Boolean Equals(Unity.Collections.FixedString512Bytes other) { }
        // RVA: 0x0B16245C  token: 0x6000166
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString4096Bytes other) { }
        // RVA: 0x0B162C68  token: 0x6000167
        public static System.Boolean op_Equality(Unity.Collections.FixedString64Bytes& a, Unity.Collections.FixedString4096Bytes& b) { }
        // RVA: 0x0B1628F4  token: 0x6000168
        public virtual System.Boolean Equals(Unity.Collections.FixedString4096Bytes other) { }
        // RVA: 0x0B162CF4  token: 0x6000169
        public static Unity.Collections.FixedString64Bytes op_Implicit(System.String b) { }
        // RVA: 0x053945DC  token: 0x600016A
        public virtual System.String ToString() { }
        // RVA: 0x0B162908  token: 0x600016B
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B162578  token: 0x600016C
        public virtual System.Boolean Equals(System.Object obj) { }

    }

    // TypeToken: 0x200004F  // size: 0x8E
    public sealed struct FixedBytes126
    {
        // Fields
        public Unity.Collections.FixedBytes16 offset0000;  // 0x10
        public Unity.Collections.FixedBytes16 offset0016;  // 0x20
        public Unity.Collections.FixedBytes16 offset0032;  // 0x30
        public Unity.Collections.FixedBytes16 offset0048;  // 0x40
        public Unity.Collections.FixedBytes16 offset0064;  // 0x50
        public Unity.Collections.FixedBytes16 offset0080;  // 0x60
        public Unity.Collections.FixedBytes16 offset0096;  // 0x70
        public System.Byte byte0112;  // 0x80
        public System.Byte byte0113;  // 0x81
        public System.Byte byte0114;  // 0x82
        public System.Byte byte0115;  // 0x83
        public System.Byte byte0116;  // 0x84
        public System.Byte byte0117;  // 0x85
        public System.Byte byte0118;  // 0x86
        public System.Byte byte0119;  // 0x87
        public System.Byte byte0120;  // 0x88
        public System.Byte byte0121;  // 0x89
        public System.Byte byte0122;  // 0x8a
        public System.Byte byte0123;  // 0x8b
        public System.Byte byte0124;  // 0x8c
        public System.Byte byte0125;  // 0x8d

    }

    // TypeToken: 0x2000050  // size: 0x90
    public sealed struct FixedString128Bytes : Unity.Collections.INativeList`1, Unity.Collections.IIndexable`1, Unity.Collections.IUTF8Bytes, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1
    {
        // Fields
        private static System.UInt16 utf8MaxLengthInBytes;  // const
        private System.UInt16 utf8LengthInBytes;  // 0x10
        private Unity.Collections.FixedBytes126 bytes;  // 0x12

        // Properties
        System.Int32 Length { get; /* RVA: 0x04D87110 */ set; /* RVA: 0x04DAB510 */ }

        // Methods
        // RVA: 0x04DAB500  token: 0x600016D
        public virtual System.Byte* GetUnsafePtr() { }
        // RVA: 0x0B160690  token: 0x6000170
        public virtual System.Boolean TryResize(System.Int32 newLength, Unity.Collections.NativeArrayOptions clearOptions) { }
        // RVA: 0x0B1601BC  token: 0x6000171
        public virtual System.Int32 CompareTo(System.String other) { }
        // RVA: 0x0B1602D8  token: 0x6000172
        public virtual System.Boolean Equals(System.String other) { }
        // RVA: 0x03875060  token: 0x6000173
        public System.Void .ctor(System.String source) { }
        // RVA: 0x03875090  token: 0x6000174
        private System.Int32 Initialize(System.String source) { }
        // RVA: 0x0B16023C  token: 0x6000175
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString32Bytes other) { }
        // RVA: 0x0B160788  token: 0x6000176
        public static System.Boolean op_Equality(Unity.Collections.FixedString128Bytes& a, Unity.Collections.FixedString32Bytes& b) { }
        // RVA: 0x0B1602C4  token: 0x6000177
        public virtual System.Boolean Equals(Unity.Collections.FixedString32Bytes other) { }
        // RVA: 0x0B1601F0  token: 0x6000178
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString64Bytes other) { }
        // RVA: 0x0B160894  token: 0x6000179
        public static System.Boolean op_Equality(Unity.Collections.FixedString128Bytes& a, Unity.Collections.FixedString64Bytes& b) { }
        // RVA: 0x0B16035C  token: 0x600017A
        public virtual System.Boolean Equals(Unity.Collections.FixedString64Bytes other) { }
        // RVA: 0x0B160124  token: 0x600017B
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString128Bytes other) { }
        // RVA: 0x0B160814  token: 0x600017C
        public static System.Boolean op_Equality(Unity.Collections.FixedString128Bytes& a, Unity.Collections.FixedString128Bytes& b) { }
        // RVA: 0x0B1602B0  token: 0x600017D
        public virtual System.Boolean Equals(Unity.Collections.FixedString128Bytes other) { }
        // RVA: 0x0B160170  token: 0x600017E
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString512Bytes other) { }
        // RVA: 0x0B1606FC  token: 0x600017F
        public static System.Boolean op_Equality(Unity.Collections.FixedString128Bytes& a, Unity.Collections.FixedString512Bytes& b) { }
        // RVA: 0x0B160288  token: 0x6000180
        public virtual System.Boolean Equals(Unity.Collections.FixedString512Bytes other) { }
        // RVA: 0x0B1600D8  token: 0x6000181
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString4096Bytes other) { }
        // RVA: 0x0B160920  token: 0x6000182
        public static System.Boolean op_Equality(Unity.Collections.FixedString128Bytes& a, Unity.Collections.FixedString4096Bytes& b) { }
        // RVA: 0x0B16029C  token: 0x6000183
        public virtual System.Boolean Equals(Unity.Collections.FixedString4096Bytes other) { }
        // RVA: 0x03875020  token: 0x6000184
        public static Unity.Collections.FixedString128Bytes op_Implicit(System.String b) { }
        // RVA: 0x0B160654  token: 0x6000185
        public virtual System.String ToString() { }
        // RVA: 0x0B160618  token: 0x6000186
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B160370  token: 0x6000187
        public virtual System.Boolean Equals(System.Object obj) { }

    }

    // TypeToken: 0x2000051  // size: 0x20E
    public sealed struct FixedBytes510
    {
        // Fields
        public Unity.Collections.FixedBytes16 offset0000;  // 0x10
        public Unity.Collections.FixedBytes16 offset0016;  // 0x20
        public Unity.Collections.FixedBytes16 offset0032;  // 0x30
        public Unity.Collections.FixedBytes16 offset0048;  // 0x40
        public Unity.Collections.FixedBytes16 offset0064;  // 0x50
        public Unity.Collections.FixedBytes16 offset0080;  // 0x60
        public Unity.Collections.FixedBytes16 offset0096;  // 0x70
        public Unity.Collections.FixedBytes16 offset0112;  // 0x80
        public Unity.Collections.FixedBytes16 offset0128;  // 0x90
        public Unity.Collections.FixedBytes16 offset0144;  // 0xa0
        public Unity.Collections.FixedBytes16 offset0160;  // 0xb0
        public Unity.Collections.FixedBytes16 offset0176;  // 0xc0
        public Unity.Collections.FixedBytes16 offset0192;  // 0xd0
        public Unity.Collections.FixedBytes16 offset0208;  // 0xe0
        public Unity.Collections.FixedBytes16 offset0224;  // 0xf0
        public Unity.Collections.FixedBytes16 offset0240;  // 0x100
        public Unity.Collections.FixedBytes16 offset0256;  // 0x110
        public Unity.Collections.FixedBytes16 offset0272;  // 0x120
        public Unity.Collections.FixedBytes16 offset0288;  // 0x130
        public Unity.Collections.FixedBytes16 offset0304;  // 0x140
        public Unity.Collections.FixedBytes16 offset0320;  // 0x150
        public Unity.Collections.FixedBytes16 offset0336;  // 0x160
        public Unity.Collections.FixedBytes16 offset0352;  // 0x170
        public Unity.Collections.FixedBytes16 offset0368;  // 0x180
        public Unity.Collections.FixedBytes16 offset0384;  // 0x190
        public Unity.Collections.FixedBytes16 offset0400;  // 0x1a0
        public Unity.Collections.FixedBytes16 offset0416;  // 0x1b0
        public Unity.Collections.FixedBytes16 offset0432;  // 0x1c0
        public Unity.Collections.FixedBytes16 offset0448;  // 0x1d0
        public Unity.Collections.FixedBytes16 offset0464;  // 0x1e0
        public Unity.Collections.FixedBytes16 offset0480;  // 0x1f0
        public System.Byte byte0496;  // 0x200
        public System.Byte byte0497;  // 0x201
        public System.Byte byte0498;  // 0x202
        public System.Byte byte0499;  // 0x203
        public System.Byte byte0500;  // 0x204
        public System.Byte byte0501;  // 0x205
        public System.Byte byte0502;  // 0x206
        public System.Byte byte0503;  // 0x207
        public System.Byte byte0504;  // 0x208
        public System.Byte byte0505;  // 0x209
        public System.Byte byte0506;  // 0x20a
        public System.Byte byte0507;  // 0x20b
        public System.Byte byte0508;  // 0x20c
        public System.Byte byte0509;  // 0x20d

    }

    // TypeToken: 0x2000052  // size: 0x210
    public sealed struct FixedString512Bytes : Unity.Collections.INativeList`1, Unity.Collections.IIndexable`1, Unity.Collections.IUTF8Bytes, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1
    {
        // Fields
        private System.UInt16 utf8LengthInBytes;  // 0x10
        private Unity.Collections.FixedBytes510 bytes;  // 0x12

        // Properties
        System.Int32 Length { get; /* RVA: 0x04D87110 */ }

        // Methods
        // RVA: 0x04DAB500  token: 0x6000188
        public virtual System.Byte* GetUnsafePtr() { }
        // RVA: 0x0B1620A4  token: 0x600018A
        public virtual System.Boolean TryResize(System.Int32 newLength, Unity.Collections.NativeArrayOptions clearOptions) { }
        // RVA: 0x0B161C1C  token: 0x600018B
        public virtual System.Int32 CompareTo(System.String other) { }
        // RVA: 0x0B161FA8  token: 0x600018C
        public virtual System.Boolean Equals(System.String other) { }
        // RVA: 0x0B161C50  token: 0x600018D
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString32Bytes other) { }
        // RVA: 0x0B162338  token: 0x600018E
        public static System.Boolean op_Equality(Unity.Collections.FixedString512Bytes& a, Unity.Collections.FixedString32Bytes& b) { }
        // RVA: 0x0B161CB0  token: 0x600018F
        public virtual System.Boolean Equals(Unity.Collections.FixedString32Bytes other) { }
        // RVA: 0x0B161B84  token: 0x6000190
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString64Bytes other) { }
        // RVA: 0x0B1622AC  token: 0x6000191
        public static System.Boolean op_Equality(Unity.Collections.FixedString512Bytes& a, Unity.Collections.FixedString64Bytes& b) { }
        // RVA: 0x0B161C9C  token: 0x6000192
        public virtual System.Boolean Equals(Unity.Collections.FixedString64Bytes other) { }
        // RVA: 0x0B161B38  token: 0x6000193
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString128Bytes other) { }
        // RVA: 0x0B162194  token: 0x6000194
        public static System.Boolean op_Equality(Unity.Collections.FixedString512Bytes& a, Unity.Collections.FixedString128Bytes& b) { }
        // RVA: 0x0B161F80  token: 0x6000195
        public virtual System.Boolean Equals(Unity.Collections.FixedString128Bytes other) { }
        // RVA: 0x0B161AEC  token: 0x6000196
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString512Bytes other) { }
        // RVA: 0x0B162114  token: 0x6000197
        public static System.Boolean op_Equality(Unity.Collections.FixedString512Bytes& a, Unity.Collections.FixedString512Bytes& b) { }
        // RVA: 0x0B161F6C  token: 0x6000198
        public virtual System.Boolean Equals(Unity.Collections.FixedString512Bytes other) { }
        // RVA: 0x0B161BD0  token: 0x6000199
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString4096Bytes other) { }
        // RVA: 0x0B162220  token: 0x600019A
        public static System.Boolean op_Equality(Unity.Collections.FixedString512Bytes& a, Unity.Collections.FixedString4096Bytes& b) { }
        // RVA: 0x0B161F94  token: 0x600019B
        public virtual System.Boolean Equals(Unity.Collections.FixedString4096Bytes other) { }
        // RVA: 0x0B162068  token: 0x600019C
        public virtual System.String ToString() { }
        // RVA: 0x0B16202C  token: 0x600019D
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B161CC4  token: 0x600019E
        public virtual System.Boolean Equals(System.Object obj) { }

    }

    // TypeToken: 0x2000053  // size: 0x100E
    public sealed struct FixedBytes4094
    {
        // Fields
        public Unity.Collections.FixedBytes16 offset0000;  // 0x10
        public Unity.Collections.FixedBytes16 offset0016;  // 0x20
        public Unity.Collections.FixedBytes16 offset0032;  // 0x30
        public Unity.Collections.FixedBytes16 offset0048;  // 0x40
        public Unity.Collections.FixedBytes16 offset0064;  // 0x50
        public Unity.Collections.FixedBytes16 offset0080;  // 0x60
        public Unity.Collections.FixedBytes16 offset0096;  // 0x70
        public Unity.Collections.FixedBytes16 offset0112;  // 0x80
        public Unity.Collections.FixedBytes16 offset0128;  // 0x90
        public Unity.Collections.FixedBytes16 offset0144;  // 0xa0
        public Unity.Collections.FixedBytes16 offset0160;  // 0xb0
        public Unity.Collections.FixedBytes16 offset0176;  // 0xc0
        public Unity.Collections.FixedBytes16 offset0192;  // 0xd0
        public Unity.Collections.FixedBytes16 offset0208;  // 0xe0
        public Unity.Collections.FixedBytes16 offset0224;  // 0xf0
        public Unity.Collections.FixedBytes16 offset0240;  // 0x100
        public Unity.Collections.FixedBytes16 offset0256;  // 0x110
        public Unity.Collections.FixedBytes16 offset0272;  // 0x120
        public Unity.Collections.FixedBytes16 offset0288;  // 0x130
        public Unity.Collections.FixedBytes16 offset0304;  // 0x140
        public Unity.Collections.FixedBytes16 offset0320;  // 0x150
        public Unity.Collections.FixedBytes16 offset0336;  // 0x160
        public Unity.Collections.FixedBytes16 offset0352;  // 0x170
        public Unity.Collections.FixedBytes16 offset0368;  // 0x180
        public Unity.Collections.FixedBytes16 offset0384;  // 0x190
        public Unity.Collections.FixedBytes16 offset0400;  // 0x1a0
        public Unity.Collections.FixedBytes16 offset0416;  // 0x1b0
        public Unity.Collections.FixedBytes16 offset0432;  // 0x1c0
        public Unity.Collections.FixedBytes16 offset0448;  // 0x1d0
        public Unity.Collections.FixedBytes16 offset0464;  // 0x1e0
        public Unity.Collections.FixedBytes16 offset0480;  // 0x1f0
        public Unity.Collections.FixedBytes16 offset0496;  // 0x200
        public Unity.Collections.FixedBytes16 offset0512;  // 0x210
        public Unity.Collections.FixedBytes16 offset0528;  // 0x220
        public Unity.Collections.FixedBytes16 offset0544;  // 0x230
        public Unity.Collections.FixedBytes16 offset0560;  // 0x240
        public Unity.Collections.FixedBytes16 offset0576;  // 0x250
        public Unity.Collections.FixedBytes16 offset0592;  // 0x260
        public Unity.Collections.FixedBytes16 offset0608;  // 0x270
        public Unity.Collections.FixedBytes16 offset0624;  // 0x280
        public Unity.Collections.FixedBytes16 offset0640;  // 0x290
        public Unity.Collections.FixedBytes16 offset0656;  // 0x2a0
        public Unity.Collections.FixedBytes16 offset0672;  // 0x2b0
        public Unity.Collections.FixedBytes16 offset0688;  // 0x2c0
        public Unity.Collections.FixedBytes16 offset0704;  // 0x2d0
        public Unity.Collections.FixedBytes16 offset0720;  // 0x2e0
        public Unity.Collections.FixedBytes16 offset0736;  // 0x2f0
        public Unity.Collections.FixedBytes16 offset0752;  // 0x300
        public Unity.Collections.FixedBytes16 offset0768;  // 0x310
        public Unity.Collections.FixedBytes16 offset0784;  // 0x320
        public Unity.Collections.FixedBytes16 offset0800;  // 0x330
        public Unity.Collections.FixedBytes16 offset0816;  // 0x340
        public Unity.Collections.FixedBytes16 offset0832;  // 0x350
        public Unity.Collections.FixedBytes16 offset0848;  // 0x360
        public Unity.Collections.FixedBytes16 offset0864;  // 0x370
        public Unity.Collections.FixedBytes16 offset0880;  // 0x380
        public Unity.Collections.FixedBytes16 offset0896;  // 0x390
        public Unity.Collections.FixedBytes16 offset0912;  // 0x3a0
        public Unity.Collections.FixedBytes16 offset0928;  // 0x3b0
        public Unity.Collections.FixedBytes16 offset0944;  // 0x3c0
        public Unity.Collections.FixedBytes16 offset0960;  // 0x3d0
        public Unity.Collections.FixedBytes16 offset0976;  // 0x3e0
        public Unity.Collections.FixedBytes16 offset0992;  // 0x3f0
        public Unity.Collections.FixedBytes16 offset1008;  // 0x400
        public Unity.Collections.FixedBytes16 offset1024;  // 0x410
        public Unity.Collections.FixedBytes16 offset1040;  // 0x420
        public Unity.Collections.FixedBytes16 offset1056;  // 0x430
        public Unity.Collections.FixedBytes16 offset1072;  // 0x440
        public Unity.Collections.FixedBytes16 offset1088;  // 0x450
        public Unity.Collections.FixedBytes16 offset1104;  // 0x460
        public Unity.Collections.FixedBytes16 offset1120;  // 0x470
        public Unity.Collections.FixedBytes16 offset1136;  // 0x480
        public Unity.Collections.FixedBytes16 offset1152;  // 0x490
        public Unity.Collections.FixedBytes16 offset1168;  // 0x4a0
        public Unity.Collections.FixedBytes16 offset1184;  // 0x4b0
        public Unity.Collections.FixedBytes16 offset1200;  // 0x4c0
        public Unity.Collections.FixedBytes16 offset1216;  // 0x4d0
        public Unity.Collections.FixedBytes16 offset1232;  // 0x4e0
        public Unity.Collections.FixedBytes16 offset1248;  // 0x4f0
        public Unity.Collections.FixedBytes16 offset1264;  // 0x500
        public Unity.Collections.FixedBytes16 offset1280;  // 0x510
        public Unity.Collections.FixedBytes16 offset1296;  // 0x520
        public Unity.Collections.FixedBytes16 offset1312;  // 0x530
        public Unity.Collections.FixedBytes16 offset1328;  // 0x540
        public Unity.Collections.FixedBytes16 offset1344;  // 0x550
        public Unity.Collections.FixedBytes16 offset1360;  // 0x560
        public Unity.Collections.FixedBytes16 offset1376;  // 0x570
        public Unity.Collections.FixedBytes16 offset1392;  // 0x580
        public Unity.Collections.FixedBytes16 offset1408;  // 0x590
        public Unity.Collections.FixedBytes16 offset1424;  // 0x5a0
        public Unity.Collections.FixedBytes16 offset1440;  // 0x5b0
        public Unity.Collections.FixedBytes16 offset1456;  // 0x5c0
        public Unity.Collections.FixedBytes16 offset1472;  // 0x5d0
        public Unity.Collections.FixedBytes16 offset1488;  // 0x5e0
        public Unity.Collections.FixedBytes16 offset1504;  // 0x5f0
        public Unity.Collections.FixedBytes16 offset1520;  // 0x600
        public Unity.Collections.FixedBytes16 offset1536;  // 0x610
        public Unity.Collections.FixedBytes16 offset1552;  // 0x620
        public Unity.Collections.FixedBytes16 offset1568;  // 0x630
        public Unity.Collections.FixedBytes16 offset1584;  // 0x640
        public Unity.Collections.FixedBytes16 offset1600;  // 0x650
        public Unity.Collections.FixedBytes16 offset1616;  // 0x660
        public Unity.Collections.FixedBytes16 offset1632;  // 0x670
        public Unity.Collections.FixedBytes16 offset1648;  // 0x680
        public Unity.Collections.FixedBytes16 offset1664;  // 0x690
        public Unity.Collections.FixedBytes16 offset1680;  // 0x6a0
        public Unity.Collections.FixedBytes16 offset1696;  // 0x6b0
        public Unity.Collections.FixedBytes16 offset1712;  // 0x6c0
        public Unity.Collections.FixedBytes16 offset1728;  // 0x6d0
        public Unity.Collections.FixedBytes16 offset1744;  // 0x6e0
        public Unity.Collections.FixedBytes16 offset1760;  // 0x6f0
        public Unity.Collections.FixedBytes16 offset1776;  // 0x700
        public Unity.Collections.FixedBytes16 offset1792;  // 0x710
        public Unity.Collections.FixedBytes16 offset1808;  // 0x720
        public Unity.Collections.FixedBytes16 offset1824;  // 0x730
        public Unity.Collections.FixedBytes16 offset1840;  // 0x740
        public Unity.Collections.FixedBytes16 offset1856;  // 0x750
        public Unity.Collections.FixedBytes16 offset1872;  // 0x760
        public Unity.Collections.FixedBytes16 offset1888;  // 0x770
        public Unity.Collections.FixedBytes16 offset1904;  // 0x780
        public Unity.Collections.FixedBytes16 offset1920;  // 0x790
        public Unity.Collections.FixedBytes16 offset1936;  // 0x7a0
        public Unity.Collections.FixedBytes16 offset1952;  // 0x7b0
        public Unity.Collections.FixedBytes16 offset1968;  // 0x7c0
        public Unity.Collections.FixedBytes16 offset1984;  // 0x7d0
        public Unity.Collections.FixedBytes16 offset2000;  // 0x7e0
        public Unity.Collections.FixedBytes16 offset2016;  // 0x7f0
        public Unity.Collections.FixedBytes16 offset2032;  // 0x800
        public Unity.Collections.FixedBytes16 offset2048;  // 0x810
        public Unity.Collections.FixedBytes16 offset2064;  // 0x820
        public Unity.Collections.FixedBytes16 offset2080;  // 0x830
        public Unity.Collections.FixedBytes16 offset2096;  // 0x840
        public Unity.Collections.FixedBytes16 offset2112;  // 0x850
        public Unity.Collections.FixedBytes16 offset2128;  // 0x860
        public Unity.Collections.FixedBytes16 offset2144;  // 0x870
        public Unity.Collections.FixedBytes16 offset2160;  // 0x880
        public Unity.Collections.FixedBytes16 offset2176;  // 0x890
        public Unity.Collections.FixedBytes16 offset2192;  // 0x8a0
        public Unity.Collections.FixedBytes16 offset2208;  // 0x8b0
        public Unity.Collections.FixedBytes16 offset2224;  // 0x8c0
        public Unity.Collections.FixedBytes16 offset2240;  // 0x8d0
        public Unity.Collections.FixedBytes16 offset2256;  // 0x8e0
        public Unity.Collections.FixedBytes16 offset2272;  // 0x8f0
        public Unity.Collections.FixedBytes16 offset2288;  // 0x900
        public Unity.Collections.FixedBytes16 offset2304;  // 0x910
        public Unity.Collections.FixedBytes16 offset2320;  // 0x920
        public Unity.Collections.FixedBytes16 offset2336;  // 0x930
        public Unity.Collections.FixedBytes16 offset2352;  // 0x940
        public Unity.Collections.FixedBytes16 offset2368;  // 0x950
        public Unity.Collections.FixedBytes16 offset2384;  // 0x960
        public Unity.Collections.FixedBytes16 offset2400;  // 0x970
        public Unity.Collections.FixedBytes16 offset2416;  // 0x980
        public Unity.Collections.FixedBytes16 offset2432;  // 0x990
        public Unity.Collections.FixedBytes16 offset2448;  // 0x9a0
        public Unity.Collections.FixedBytes16 offset2464;  // 0x9b0
        public Unity.Collections.FixedBytes16 offset2480;  // 0x9c0
        public Unity.Collections.FixedBytes16 offset2496;  // 0x9d0
        public Unity.Collections.FixedBytes16 offset2512;  // 0x9e0
        public Unity.Collections.FixedBytes16 offset2528;  // 0x9f0
        public Unity.Collections.FixedBytes16 offset2544;  // 0xa00
        public Unity.Collections.FixedBytes16 offset2560;  // 0xa10
        public Unity.Collections.FixedBytes16 offset2576;  // 0xa20
        public Unity.Collections.FixedBytes16 offset2592;  // 0xa30
        public Unity.Collections.FixedBytes16 offset2608;  // 0xa40
        public Unity.Collections.FixedBytes16 offset2624;  // 0xa50
        public Unity.Collections.FixedBytes16 offset2640;  // 0xa60
        public Unity.Collections.FixedBytes16 offset2656;  // 0xa70
        public Unity.Collections.FixedBytes16 offset2672;  // 0xa80
        public Unity.Collections.FixedBytes16 offset2688;  // 0xa90
        public Unity.Collections.FixedBytes16 offset2704;  // 0xaa0
        public Unity.Collections.FixedBytes16 offset2720;  // 0xab0
        public Unity.Collections.FixedBytes16 offset2736;  // 0xac0
        public Unity.Collections.FixedBytes16 offset2752;  // 0xad0
        public Unity.Collections.FixedBytes16 offset2768;  // 0xae0
        public Unity.Collections.FixedBytes16 offset2784;  // 0xaf0
        public Unity.Collections.FixedBytes16 offset2800;  // 0xb00
        public Unity.Collections.FixedBytes16 offset2816;  // 0xb10
        public Unity.Collections.FixedBytes16 offset2832;  // 0xb20
        public Unity.Collections.FixedBytes16 offset2848;  // 0xb30
        public Unity.Collections.FixedBytes16 offset2864;  // 0xb40
        public Unity.Collections.FixedBytes16 offset2880;  // 0xb50
        public Unity.Collections.FixedBytes16 offset2896;  // 0xb60
        public Unity.Collections.FixedBytes16 offset2912;  // 0xb70
        public Unity.Collections.FixedBytes16 offset2928;  // 0xb80
        public Unity.Collections.FixedBytes16 offset2944;  // 0xb90
        public Unity.Collections.FixedBytes16 offset2960;  // 0xba0
        public Unity.Collections.FixedBytes16 offset2976;  // 0xbb0
        public Unity.Collections.FixedBytes16 offset2992;  // 0xbc0
        public Unity.Collections.FixedBytes16 offset3008;  // 0xbd0
        public Unity.Collections.FixedBytes16 offset3024;  // 0xbe0
        public Unity.Collections.FixedBytes16 offset3040;  // 0xbf0
        public Unity.Collections.FixedBytes16 offset3056;  // 0xc00
        public Unity.Collections.FixedBytes16 offset3072;  // 0xc10
        public Unity.Collections.FixedBytes16 offset3088;  // 0xc20
        public Unity.Collections.FixedBytes16 offset3104;  // 0xc30
        public Unity.Collections.FixedBytes16 offset3120;  // 0xc40
        public Unity.Collections.FixedBytes16 offset3136;  // 0xc50
        public Unity.Collections.FixedBytes16 offset3152;  // 0xc60
        public Unity.Collections.FixedBytes16 offset3168;  // 0xc70
        public Unity.Collections.FixedBytes16 offset3184;  // 0xc80
        public Unity.Collections.FixedBytes16 offset3200;  // 0xc90
        public Unity.Collections.FixedBytes16 offset3216;  // 0xca0
        public Unity.Collections.FixedBytes16 offset3232;  // 0xcb0
        public Unity.Collections.FixedBytes16 offset3248;  // 0xcc0
        public Unity.Collections.FixedBytes16 offset3264;  // 0xcd0
        public Unity.Collections.FixedBytes16 offset3280;  // 0xce0
        public Unity.Collections.FixedBytes16 offset3296;  // 0xcf0
        public Unity.Collections.FixedBytes16 offset3312;  // 0xd00
        public Unity.Collections.FixedBytes16 offset3328;  // 0xd10
        public Unity.Collections.FixedBytes16 offset3344;  // 0xd20
        public Unity.Collections.FixedBytes16 offset3360;  // 0xd30
        public Unity.Collections.FixedBytes16 offset3376;  // 0xd40
        public Unity.Collections.FixedBytes16 offset3392;  // 0xd50
        public Unity.Collections.FixedBytes16 offset3408;  // 0xd60
        public Unity.Collections.FixedBytes16 offset3424;  // 0xd70
        public Unity.Collections.FixedBytes16 offset3440;  // 0xd80
        public Unity.Collections.FixedBytes16 offset3456;  // 0xd90
        public Unity.Collections.FixedBytes16 offset3472;  // 0xda0
        public Unity.Collections.FixedBytes16 offset3488;  // 0xdb0
        public Unity.Collections.FixedBytes16 offset3504;  // 0xdc0
        public Unity.Collections.FixedBytes16 offset3520;  // 0xdd0
        public Unity.Collections.FixedBytes16 offset3536;  // 0xde0
        public Unity.Collections.FixedBytes16 offset3552;  // 0xdf0
        public Unity.Collections.FixedBytes16 offset3568;  // 0xe00
        public Unity.Collections.FixedBytes16 offset3584;  // 0xe10
        public Unity.Collections.FixedBytes16 offset3600;  // 0xe20
        public Unity.Collections.FixedBytes16 offset3616;  // 0xe30
        public Unity.Collections.FixedBytes16 offset3632;  // 0xe40
        public Unity.Collections.FixedBytes16 offset3648;  // 0xe50
        public Unity.Collections.FixedBytes16 offset3664;  // 0xe60
        public Unity.Collections.FixedBytes16 offset3680;  // 0xe70
        public Unity.Collections.FixedBytes16 offset3696;  // 0xe80
        public Unity.Collections.FixedBytes16 offset3712;  // 0xe90
        public Unity.Collections.FixedBytes16 offset3728;  // 0xea0
        public Unity.Collections.FixedBytes16 offset3744;  // 0xeb0
        public Unity.Collections.FixedBytes16 offset3760;  // 0xec0
        public Unity.Collections.FixedBytes16 offset3776;  // 0xed0
        public Unity.Collections.FixedBytes16 offset3792;  // 0xee0
        public Unity.Collections.FixedBytes16 offset3808;  // 0xef0
        public Unity.Collections.FixedBytes16 offset3824;  // 0xf00
        public Unity.Collections.FixedBytes16 offset3840;  // 0xf10
        public Unity.Collections.FixedBytes16 offset3856;  // 0xf20
        public Unity.Collections.FixedBytes16 offset3872;  // 0xf30
        public Unity.Collections.FixedBytes16 offset3888;  // 0xf40
        public Unity.Collections.FixedBytes16 offset3904;  // 0xf50
        public Unity.Collections.FixedBytes16 offset3920;  // 0xf60
        public Unity.Collections.FixedBytes16 offset3936;  // 0xf70
        public Unity.Collections.FixedBytes16 offset3952;  // 0xf80
        public Unity.Collections.FixedBytes16 offset3968;  // 0xf90
        public Unity.Collections.FixedBytes16 offset3984;  // 0xfa0
        public Unity.Collections.FixedBytes16 offset4000;  // 0xfb0
        public Unity.Collections.FixedBytes16 offset4016;  // 0xfc0
        public Unity.Collections.FixedBytes16 offset4032;  // 0xfd0
        public Unity.Collections.FixedBytes16 offset4048;  // 0xfe0
        public Unity.Collections.FixedBytes16 offset4064;  // 0xff0
        public System.Byte byte4080;  // 0x1000
        public System.Byte byte4081;  // 0x1001
        public System.Byte byte4082;  // 0x1002
        public System.Byte byte4083;  // 0x1003
        public System.Byte byte4084;  // 0x1004
        public System.Byte byte4085;  // 0x1005
        public System.Byte byte4086;  // 0x1006
        public System.Byte byte4087;  // 0x1007
        public System.Byte byte4088;  // 0x1008
        public System.Byte byte4089;  // 0x1009
        public System.Byte byte4090;  // 0x100a
        public System.Byte byte4091;  // 0x100b
        public System.Byte byte4092;  // 0x100c
        public System.Byte byte4093;  // 0x100d

    }

    // TypeToken: 0x2000054  // size: 0x1010
    public sealed struct FixedString4096Bytes : Unity.Collections.INativeList`1, Unity.Collections.IIndexable`1, Unity.Collections.IUTF8Bytes, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1, System.IComparable`1, System.IEquatable`1
    {
        // Fields
        private System.UInt16 utf8LengthInBytes;  // 0x10
        private Unity.Collections.FixedBytes4094 bytes;  // 0x12

        // Properties
        System.Int32 Length { get; /* RVA: 0x04D87110 */ }

        // Methods
        // RVA: 0x04DAB500  token: 0x600019F
        public virtual System.Byte* GetUnsafePtr() { }
        // RVA: 0x0B1617CC  token: 0x60001A1
        public virtual System.Boolean TryResize(System.Int32 newLength, Unity.Collections.NativeArrayOptions clearOptions) { }
        // RVA: 0x0B161390  token: 0x60001A2
        public virtual System.Int32 CompareTo(System.String other) { }
        // RVA: 0x0B1613D8  token: 0x60001A3
        public virtual System.Boolean Equals(System.String other) { }
        // RVA: 0x0B1612F8  token: 0x60001A4
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString32Bytes other) { }
        // RVA: 0x0B161948  token: 0x60001A5
        public static System.Boolean op_Equality(Unity.Collections.FixedString4096Bytes& a, Unity.Collections.FixedString32Bytes& b) { }
        // RVA: 0x0B16172C  token: 0x60001A6
        public virtual System.Boolean Equals(Unity.Collections.FixedString32Bytes other) { }
        // RVA: 0x0B161344  token: 0x60001A7
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString64Bytes other) { }
        // RVA: 0x0B1618BC  token: 0x60001A8
        public static System.Boolean op_Equality(Unity.Collections.FixedString4096Bytes& a, Unity.Collections.FixedString64Bytes& b) { }
        // RVA: 0x0B161740  token: 0x60001A9
        public virtual System.Boolean Equals(Unity.Collections.FixedString64Bytes other) { }
        // RVA: 0x0B1612AC  token: 0x60001AA
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString128Bytes other) { }
        // RVA: 0x0B1619D4  token: 0x60001AB
        public static System.Boolean op_Equality(Unity.Collections.FixedString4096Bytes& a, Unity.Collections.FixedString128Bytes& b) { }
        // RVA: 0x0B16145C  token: 0x60001AC
        public virtual System.Boolean Equals(Unity.Collections.FixedString128Bytes other) { }
        // RVA: 0x0B161214  token: 0x60001AD
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString512Bytes other) { }
        // RVA: 0x0B161A60  token: 0x60001AE
        public static System.Boolean op_Equality(Unity.Collections.FixedString4096Bytes& a, Unity.Collections.FixedString512Bytes& b) { }
        // RVA: 0x0B161470  token: 0x60001AF
        public virtual System.Boolean Equals(Unity.Collections.FixedString512Bytes other) { }
        // RVA: 0x0B161260  token: 0x60001B0
        public virtual System.Int32 CompareTo(Unity.Collections.FixedString4096Bytes other) { }
        // RVA: 0x0B16183C  token: 0x60001B1
        public static System.Boolean op_Equality(Unity.Collections.FixedString4096Bytes& a, Unity.Collections.FixedString4096Bytes& b) { }
        // RVA: 0x0B1613C4  token: 0x60001B2
        public virtual System.Boolean Equals(Unity.Collections.FixedString4096Bytes other) { }
        // RVA: 0x0B161790  token: 0x60001B3
        public virtual System.String ToString() { }
        // RVA: 0x0B161754  token: 0x60001B4
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B161484  token: 0x60001B5
        public virtual System.Boolean Equals(System.Object obj) { }

    }

    // TypeToken: 0x2000055  // size: 0x10
    public static class FixedStringMethods
    {
        // Methods
        // RVA: -1  // generic def  token: 0x60001B6
        public static Unity.Collections.FormatError Append(T& fs, System.Byte* utf8Bytes, System.Int32 utf8BytesLength) { }
        // RVA: -1  // generic def  token: 0x60001B7
        public static Unity.Collections.FormatError Append(T& fs, System.String s) { }
        // RVA: -1  // generic def  token: 0x60001B8
        public static System.Int32 CompareTo(T& fs, System.Byte* bytes, System.Int32 bytesLen) { }
        // RVA: -1  // generic def  token: 0x60001B9
        public static System.Int32 CompareTo(T& fs, T2& other) { }
        // RVA: -1  // generic def  token: 0x60001BA
        public static System.String ConvertToString(T& fs) { }
        // RVA: -1  // generic def  token: 0x60001BB
        public static System.Int32 ComputeHashCode(T& fs) { }

    }

    // TypeToken: 0x2000056
    public interface IUTF8Bytes
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001BC
        public virtual System.Byte* GetUnsafePtr() { }
        // RVA: -1  // abstract  token: 0x60001BD
        public virtual System.Boolean TryResize(System.Int32 newLength, Unity.Collections.NativeArrayOptions clearOptions) { }

    }

    // TypeToken: 0x2000057  // size: 0x10
    public static class ListExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x60001BE
        public static System.Boolean RemoveSwapBack(System.Collections.Generic.List<T> list, T value) { }
        // RVA: -1  // generic def  token: 0x60001BF
        public static System.Void RemoveAtSwapBack(System.Collections.Generic.List<T> list, System.Int32 index) { }
        // RVA: -1  // generic def  token: 0x60001C0
        public static Unity.Collections.NativeArray<T> ToNativeArray(System.Collections.Generic.List<T> list, Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }

    }

    // TypeToken: 0x2000058  // size: 0x11
    public sealed struct Memory
    {
    }

    // TypeToken: 0x200005B  // size: 0x10
    public static class NativeArrayExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x60001C8
        public static System.Boolean Contains(Unity.Collections.NativeList<T> list, U value) { }
        // RVA: -1  // generic def  token: 0x60001C9
        public static System.Span<T> AsSpan(Unity.Collections.NativeArray<T> array) { }
        // RVA: -1  // generic def  token: 0x60001CA
        public static System.Span<T> AsSpanThread(Unity.Collections.NativeArray<T> array) { }
        // RVA: -1  // generic def  token: 0x60001CB
        public static System.Int32 IndexOf(System.Void* ptr, System.Int32 length, U value) { }
        // RVA: -1  // generic def  token: 0x60001CC
        public static Unity.Collections.NativeArray<U> Reinterpret(Unity.Collections.NativeArray<T> array) { }
        // RVA: -1  // generic def  token: 0x60001CD
        private static System.Void Initialize(Unity.Collections.NativeArray<T>& array, System.Int32 length, Unity.Collections.AllocatorManager.AllocatorHandle allocator, Unity.Collections.NativeArrayOptions options) { }

    }

    // TypeToken: 0x200005C
    public interface IIndexable`1
    {
        // Properties
        System.Int32 Length { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x200005D
    public interface INativeList`1 : Unity.Collections.IIndexable`1
    {
    }

    // TypeToken: 0x200005E
    public sealed struct NativeList`1 : Unity.Collections.INativeDisposable, System.IDisposable, Unity.Collections.INativeList`1, Unity.Collections.IIndexable`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* m_ListData;  // 0x0
        private Unity.Collections.AllocatorManager.AllocatorHandle m_DeprecatedAllocator;  // 0x0

        // Properties
        T Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Int32 Length { get; /* RVA: -1  // not resolved */ }
        System.Int32 Capacity { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Boolean IsEmpty { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsCreated { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60001CF
        public System.Void .ctor(Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // not resolved  token: 0x60001D0
        public System.Void .ctor(System.Int32 initialCapacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // generic def  token: 0x60001D1
        private System.Void Initialize(System.Int32 initialCapacity, U& allocator, System.Int32 disposeSentinelStackDepth) { }
        // RVA: -1  // not resolved  token: 0x60001D2
        private System.Void .ctor(System.Int32 initialCapacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator, System.Int32 disposeSentinelStackDepth) { }
        // RVA: -1  // not resolved  token: 0x60001D5
        public virtual T& ElementAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60001D9
        public Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* GetUnsafeList() { }
        // RVA: -1  // not resolved  token: 0x60001DA
        public System.Void AddNoResize(T value) { }
        // RVA: -1  // not resolved  token: 0x60001DB
        public System.Void AddRangeNoResize(System.Void* ptr, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x60001DC
        public System.Void Add(T& value) { }
        // RVA: -1  // not resolved  token: 0x60001DD
        public System.Void AddRange(Unity.Collections.NativeArray<T> array) { }
        // RVA: -1  // not resolved  token: 0x60001DE
        public System.Void AddRange(System.Void* ptr, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x60001DF
        public System.Void RemoveAtSwapBack(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60001E0
        public System.Void RemoveAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60001E1
        public System.Void RemoveRange(System.Int32 index, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x60001E4
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x60001E5
        public virtual Unity.Jobs.JobHandle Dispose(Unity.Jobs.JobHandle inputDeps) { }
        // RVA: -1  // not resolved  token: 0x60001E6
        public Unity.Jobs.JobHandle CrossFrameDispose(Unity.Jobs.JobHandle inputDeps, Unity.Jobs.LowLevel.Unsafe.JobQueuePriority priority) { }
        // RVA: -1  // not resolved  token: 0x60001E7
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x60001E8
        public static Unity.Collections.NativeArray<T> op_Implicit(Unity.Collections.NativeList<T> nativeList) { }
        // RVA: -1  // not resolved  token: 0x60001E9
        public Unity.Collections.NativeArray<T> AsArray() { }
        // RVA: -1  // not resolved  token: 0x60001EA
        public Unity.Collections.NativeArray<T> AsDeferredJobArray() { }
        // RVA: -1  // not resolved  token: 0x60001EB
        public T[] ToArray() { }
        // RVA: -1  // not resolved  token: 0x60001EC
        public Unity.Collections.NativeArray.Enumerator<T> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60001ED
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60001EE
        private virtual System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60001EF
        public System.Void CopyFrom(Unity.Collections.NativeArray<T> array) { }
        // RVA: -1  // not resolved  token: 0x60001F0
        public System.Void Resize(System.Int32 length, Unity.Collections.NativeArrayOptions options) { }
        // RVA: -1  // not resolved  token: 0x60001F1
        public System.Void ResizeUninitialized(System.Int32 length) { }
        // RVA: -1  // not resolved  token: 0x60001F2
        public Unity.Collections.NativeArray.ReadOnly<T> AsParallelReader() { }
        // RVA: -1  // not resolved  token: 0x60001F3
        public Unity.Collections.NativeList.ParallelWriter<T> AsParallelWriter() { }

    }

    // TypeToken: 0x2000060  // size: 0x18
    public sealed struct NativeListDispose
    {
        // Fields
        public Unity.Collections.LowLevel.Unsafe.UntypedUnsafeList* m_ListData;  // 0x10

        // Methods
        // RVA: 0x0B163504  token: 0x60001F6
        public System.Void Dispose() { }

    }

    // TypeToken: 0x2000061  // size: 0x18
    public sealed struct NativeListDisposeJob : Unity.Jobs.IJob
    {
        // Fields
        private Unity.Collections.NativeListDispose Data;  // 0x10

        // Methods
        // RVA: 0x0B1634FC  token: 0x60001F7
        public virtual System.Void Execute() { }

    }

    // TypeToken: 0x2000062
    public sealed class NativeListDebugView`1
    {
    }

    // TypeToken: 0x2000063
    public sealed struct NativeParallelHashMap`2 : Unity.Collections.INativeDisposable, System.IDisposable, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<TKey,TValue> m_HashMapData;  // 0x0

        // Properties
        System.Boolean IsEmpty { get; /* RVA: -1  // not resolved */ }
        System.Int32 Capacity { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        TValue Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Boolean IsCreated { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60001F8
        public System.Void .ctor(System.Int32 capacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // not resolved  token: 0x60001F9
        private System.Void .ctor(System.Int32 capacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator, System.Int32 disposeSentinelStackDepth) { }
        // RVA: -1  // not resolved  token: 0x60001FB
        public System.Int32 Count() { }
        // RVA: -1  // not resolved  token: 0x60001FE
        public System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x60001FF
        public System.Boolean TryAdd(TKey key, TValue& item) { }
        // RVA: -1  // not resolved  token: 0x6000200
        public System.Void Add(TKey key, TValue item) { }
        // RVA: -1  // not resolved  token: 0x6000201
        public System.Boolean Remove(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000202
        public System.Boolean TryGetValue(TKey key, TValue& item) { }
        // RVA: -1  // not resolved  token: 0x6000203
        public System.Boolean ContainsKey(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000207
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000208
        public virtual Unity.Jobs.JobHandle Dispose(Unity.Jobs.JobHandle inputDeps) { }
        // RVA: -1  // not resolved  token: 0x6000209
        public Unity.Collections.NativeArray<TKey> GetKeyArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // not resolved  token: 0x600020A
        public Unity.Collections.NativeArray<TValue> GetValueArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // not resolved  token: 0x600020B
        public Unity.Collections.NativeParallelHashMap.ParallelWriter<TKey,TValue> AsParallelWriter() { }
        // RVA: -1  // not resolved  token: 0x600020C
        public Unity.Collections.NativeParallelHashMap.Enumerator<TKey,TValue> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600020D
        private virtual System.Collections.Generic.IEnumerator<Unity.Collections.LowLevel.Unsafe.KeyValue<TKey,TValue>> System.Collections.Generic.IEnumerable<Unity.Collections.LowLevel.Unsafe.KeyValue<TKey,TValue>>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600020E
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000066
    public sealed class NativeParallelHashMapDebuggerTypeProxy`2
    {
    }

    // TypeToken: 0x2000067  // size: 0x10
    public static class NativeParallelHashMapExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000215
        public static Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<TKey,TValue> GetUnsafeMap(Unity.Collections.NativeParallelHashMap<TKey,TValue> container) { }

    }

    // TypeToken: 0x2000068
    public sealed struct NativeParallelHashSet`1 : Unity.Collections.INativeDisposable, System.IDisposable, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private Unity.Collections.NativeParallelHashMap<T,System.Boolean> m_Data;  // 0x0

        // Properties
        System.Boolean IsEmpty { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsCreated { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000216
        public System.Void .ctor(System.Int32 capacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // not resolved  token: 0x6000218
        public System.Int32 Count() { }
        // RVA: -1  // not resolved  token: 0x600021A
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x600021B
        public System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x600021C
        public System.Boolean Add(T item) { }
        // RVA: -1  // not resolved  token: 0x600021D
        public System.Boolean Remove(T item) { }
        // RVA: -1  // not resolved  token: 0x600021E
        public System.Boolean Contains(T item) { }
        // RVA: -1  // not resolved  token: 0x600021F
        public Unity.Collections.NativeArray<T> ToNativeArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // not resolved  token: 0x6000220
        public Unity.Collections.NativeParallelHashSet.Enumerator<T> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000221
        private virtual System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000222
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200006A
    public sealed class NativeHashSetDebuggerTypeProxy`1
    {
    }

    // TypeToken: 0x200006B
    public sealed struct NativeParallelMultiHashMapIterator`1
    {
        // Fields
        private TKey key;  // 0x0
        private System.Int32 NextEntryIndex;  // 0x0
        private System.Int32 EntryIndex;  // 0x0

    }

    // TypeToken: 0x200006C
    public sealed struct NativeParallelMultiHashMap`2 : Unity.Collections.INativeDisposable, System.IDisposable, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey,TValue> m_MultiHashMapData;  // 0x0

        // Properties
        System.Boolean IsEmpty { get; /* RVA: -1  // not resolved */ }
        System.Int32 Capacity { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsCreated { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000228
        public System.Void .ctor(System.Int32 capacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // generic def  token: 0x6000229
        private System.Void Initialize(System.Int32 capacity, U& allocator, System.Int32 disposeSentinelStackDepth) { }
        // RVA: -1  // not resolved  token: 0x600022A
        private System.Void .ctor(System.Int32 capacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator, System.Int32 disposeSentinelStackDepth) { }
        // RVA: -1  // not resolved  token: 0x600022C
        public System.Int32 Count() { }
        // RVA: -1  // not resolved  token: 0x600022E
        public System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x600022F
        public System.Void Add(TKey key, TValue item) { }
        // RVA: -1  // not resolved  token: 0x6000230
        public System.Int32 Remove(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000231
        public System.Void Remove(Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it) { }
        // RVA: -1  // not resolved  token: 0x6000232
        public System.Boolean TryGetFirstValue(TKey key, TValue& item, Unity.Collections.NativeParallelMultiHashMapIterator<TKey>& it) { }
        // RVA: -1  // not resolved  token: 0x6000233
        public System.Boolean TryGetNextValue(TValue& item, Unity.Collections.NativeParallelMultiHashMapIterator<TKey>& it) { }
        // RVA: -1  // not resolved  token: 0x6000234
        public System.Boolean ContainsKey(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000235
        public System.Int32 CountValuesForKey(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000236
        public System.Boolean SetValue(TValue item, Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it) { }
        // RVA: -1  // not resolved  token: 0x6000238
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000239
        public Unity.Collections.NativeArray<TKey> GetKeyArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // not resolved  token: 0x600023A
        public Unity.Collections.NativeArray<TValue> GetValueArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // not resolved  token: 0x600023B
        public Unity.Collections.NativeParallelMultiHashMap.ParallelWriter<TKey,TValue> AsParallelWriter() { }
        // RVA: -1  // not resolved  token: 0x600023C
        public Unity.Collections.NativeParallelMultiHashMap.Enumerator<TKey,TValue> GetValuesForKey(TKey key) { }
        // RVA: -1  // not resolved  token: 0x600023D
        private virtual System.Collections.Generic.IEnumerator<Unity.Collections.LowLevel.Unsafe.KeyValue<TKey,TValue>> System.Collections.Generic.IEnumerable<Unity.Collections.LowLevel.Unsafe.KeyValue<TKey,TValue>>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600023E
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200006F
    public sealed class NativeParallelMultiHashMapDebuggerTypeProxy`2
    {
    }

    // TypeToken: 0x2000070  // size: 0x20
    public sealed struct NativeQueueBlockHeader
    {
        // Fields
        public Unity.Collections.NativeQueueBlockHeader* m_NextBlock;  // 0x10
        public System.Int32 m_NumItems;  // 0x18

    }

    // TypeToken: 0x2000071  // size: 0x28
    public sealed struct NativeQueueBlockPoolData
    {
        // Fields
        private System.IntPtr m_FirstBlock;  // 0x10
        private System.Int32 m_NumBlocks;  // 0x18
        private System.Int32 m_MaxBlocks;  // 0x1c
        private System.Int32 m_AllocLock;  // 0x20

        // Methods
        // RVA: 0x03FA3AD0  token: 0x6000246
        public Unity.Collections.NativeQueueBlockHeader* AllocateBlock() { }
        // RVA: 0x03EE1D30  token: 0x6000247
        public System.Void FreeBlock(Unity.Collections.NativeQueueBlockHeader* block) { }

    }

    // TypeToken: 0x2000072  // size: 0x10
    public class NativeQueueBlockPool
    {
        // Fields
        private static readonly Unity.Burst.SharedStatic<System.IntPtr> Data;  // static @ 0x0

        // Methods
        // RVA: 0x03FE6EC0  token: 0x6000248
        private static Unity.Collections.NativeQueueBlockPoolData* GetQueueBlockPool() { }
        // RVA: 0x03B0D570  token: 0x6000249
        private static System.Void AppDomainOnDomainUnload() { }
        // RVA: 0x0B163540  token: 0x600024A
        private static System.Void OnDomainUnload(System.Object sender, System.EventArgs e) { }
        // RVA: 0x0452F380  token: 0x600024B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000073  // size: 0x30
    public sealed struct NativeQueueData
    {
        // Fields
        public System.IntPtr m_FirstBlock;  // 0x10
        public System.IntPtr m_LastBlock;  // 0x18
        public System.Int32 m_MaxItems;  // 0x20
        public System.Int32 m_CurrentRead;  // 0x24
        public System.Byte* m_CurrentWriteBlockTLS;  // 0x28

        // Methods
        // RVA: 0x04DAB520  token: 0x600024C
        private Unity.Collections.NativeQueueBlockHeader* GetCurrentWriteBlockTLS(System.Int32 threadIndex) { }
        // RVA: 0x04DAB530  token: 0x600024D
        private System.Void SetCurrentWriteBlockTLS(System.Int32 threadIndex, Unity.Collections.NativeQueueBlockHeader* currentWriteBlock) { }
        // RVA: -1  // generic def  token: 0x600024E
        public static Unity.Collections.NativeQueueBlockHeader* AllocateWriteBlockMT(Unity.Collections.NativeQueueData* data, Unity.Collections.NativeQueueBlockPoolData* pool, System.Int32 threadIndex) { }
        // RVA: -1  // generic def  token: 0x600024F
        public static System.Void AllocateQueue(Unity.Collections.AllocatorManager.AllocatorHandle label, Unity.Collections.NativeQueueData*& outBuf) { }
        // RVA: 0x041649C0  token: 0x6000250
        public static System.Void DeallocateQueue(Unity.Collections.NativeQueueData* data, Unity.Collections.NativeQueueBlockPoolData* pool, Unity.Collections.AllocatorManager.AllocatorHandle allocation) { }

    }

    // TypeToken: 0x2000074
    public sealed struct NativeQueue`1 : Unity.Collections.INativeDisposable, System.IDisposable
    {
        // Fields
        private Unity.Collections.NativeQueueData* m_Buffer;  // 0x0
        private Unity.Collections.NativeQueueBlockPoolData* m_QueuePool;  // 0x0
        private Unity.Collections.AllocatorManager.AllocatorHandle m_AllocatorLabel;  // 0x0

        // Properties
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsCreated { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000251
        public System.Void .ctor(Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // not resolved  token: 0x6000252
        public System.Boolean IsEmpty() { }
        // RVA: -1  // not resolved  token: 0x6000254
        public System.Void Enqueue(T value) { }
        // RVA: -1  // not resolved  token: 0x6000255
        public System.Boolean TryDequeue(T& item) { }
        // RVA: -1  // not resolved  token: 0x6000256
        public Unity.Collections.NativeArray<T> ToArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // not resolved  token: 0x6000257
        public System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000259
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x600025A
        public virtual Unity.Jobs.JobHandle Dispose(Unity.Jobs.JobHandle inputDeps) { }
        // RVA: -1  // not resolved  token: 0x600025B
        public Unity.Collections.NativeQueue.ParallelWriter<T> AsParallelWriter() { }

    }

    // TypeToken: 0x2000076  // size: 0x28
    public sealed struct NativeQueueDispose
    {
        // Fields
        private Unity.Collections.NativeQueueData* m_Buffer;  // 0x10
        private Unity.Collections.NativeQueueBlockPoolData* m_QueuePool;  // 0x18
        private Unity.Collections.AllocatorManager.AllocatorHandle m_AllocatorLabel;  // 0x20

        // Methods
        // RVA: 0x0B16361C  token: 0x600025D
        public System.Void Dispose() { }

    }

    // TypeToken: 0x2000077  // size: 0x28
    public sealed struct NativeQueueDisposeJob : Unity.Jobs.IJob
    {
        // Fields
        public Unity.Collections.NativeQueueDispose Data;  // 0x10

        // Methods
        // RVA: 0x0B163614  token: 0x600025E
        public virtual System.Void Execute() { }

    }

    // TypeToken: 0x2000078
    public sealed struct NativeReference`1 : Unity.Collections.INativeDisposable, System.IDisposable, System.IEquatable`1
    {
        // Fields
        private System.Void* m_Data;  // 0x0
        private Unity.Collections.AllocatorManager.AllocatorHandle m_AllocatorLabel;  // 0x0

        // Properties
        T Value { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Boolean IsCreated { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600025F
        public System.Void .ctor(Unity.Collections.AllocatorManager.AllocatorHandle allocator, Unity.Collections.NativeArrayOptions options) { }
        // RVA: -1  // not resolved  token: 0x6000260
        public System.Void .ctor(T value, Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // not resolved  token: 0x6000261
        private static System.Void Allocate(Unity.Collections.AllocatorManager.AllocatorHandle allocator, Unity.Collections.NativeReference<T>& reference) { }
        // RVA: -1  // not resolved  token: 0x6000265
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000266
        public virtual System.Boolean Equals(Unity.Collections.NativeReference<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000267
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: -1  // not resolved  token: 0x6000268
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000079  // size: 0x20
    public sealed struct NativeReferenceDispose
    {
        // Fields
        private System.Void* m_Data;  // 0x10
        private Unity.Collections.AllocatorManager.AllocatorHandle m_AllocatorLabel;  // 0x18

        // Methods
        // RVA: 0x0B163638  token: 0x6000269
        public System.Void Dispose() { }

    }

    // TypeToken: 0x200007A  // size: 0x20
    public sealed struct NativeReferenceDisposeJob : Unity.Jobs.IJob
    {
        // Fields
        private Unity.Collections.NativeReferenceDispose Data;  // 0x10

        // Methods
        // RVA: 0x0B163630  token: 0x600026A
        public virtual System.Void Execute() { }

    }

    // TypeToken: 0x200007B  // size: 0x10
    public static class NativeSortExtension
    {
        // Methods
        // RVA: -1  // generic def  token: 0x600026B
        public static System.Void Sort(T* array, System.Int32 length) { }
        // RVA: -1  // generic def  token: 0x600026C
        public static System.Void Sort(T* array, System.Int32 length, U comp) { }
        // RVA: -1  // generic def  token: 0x600026D
        public static Unity.Collections.SortJob<T,U> SortJob(T* array, System.Int32 length, U comp) { }
        // RVA: -1  // generic def  token: 0x600026E
        public static System.Int32 BinarySearch(T* ptr, System.Int32 length, T value) { }
        // RVA: -1  // generic def  token: 0x600026F
        public static System.Int32 BinarySearch(T* ptr, System.Int32 length, T value, U comp) { }
        // RVA: -1  // generic def  token: 0x6000270
        public static Unity.Collections.SortJob<T,Unity.Collections.NativeSortExtension.DefaultComparer<T>> SortJob(Unity.Collections.NativeArray<T> array) { }
        // RVA: -1  // generic def  token: 0x6000271
        public static System.Void Sort(Unity.Collections.NativeList<T> list) { }
        // RVA: -1  // generic def  token: 0x6000272
        public static System.Void Sort(Unity.Collections.NativeList<T> list, U comp) { }
        // RVA: -1  // generic def  token: 0x6000273
        private static System.Void IntroSort(System.Void* array, System.Int32 length, U comp) { }
        // RVA: -1  // generic def  token: 0x6000274
        private static System.Void IntroSort(System.Void* array, System.Int32 lo, System.Int32 hi, System.Int32 depth, U comp) { }
        // RVA: -1  // generic def  token: 0x6000275
        private static System.Void InsertionSort(System.Void* array, System.Int32 lo, System.Int32 hi, U comp) { }
        // RVA: -1  // generic def  token: 0x6000276
        private static System.Int32 Partition(System.Void* array, System.Int32 lo, System.Int32 hi, U comp) { }
        // RVA: -1  // generic def  token: 0x6000277
        private static System.Void HeapSort(System.Void* array, System.Int32 lo, System.Int32 hi, U comp) { }
        // RVA: -1  // generic def  token: 0x6000278
        private static System.Void Heapify(System.Void* array, System.Int32 i, System.Int32 n, System.Int32 lo, U comp) { }
        // RVA: -1  // generic def  token: 0x6000279
        private static System.Void Swap(System.Void* array, System.Int32 lhs, System.Int32 rhs) { }
        // RVA: -1  // generic def  token: 0x600027A
        private static System.Void SwapIfGreaterWithItems(System.Void* array, System.Int32 lhs, System.Int32 rhs, U comp) { }

    }

    // TypeToken: 0x200007D
    public sealed struct SortJob`2
    {
        // Fields
        public T* Data;  // 0x0
        public U Comp;  // 0x0
        public System.Int32 Length;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600027C
        public Unity.Jobs.JobHandle Schedule(Unity.Jobs.JobHandle inputDeps) { }

    }

    // TypeToken: 0x2000080  // size: 0x20
    public sealed struct NativeStream : System.IDisposable
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeStream m_Stream;  // 0x10

        // Methods
        // RVA: 0x0B160058  token: 0x600027F
        public virtual System.Void Dispose() { }
        // RVA: 0x0B163648  token: 0x6000280
        private System.Void AllocateForEach(System.Int32 forEachCount) { }

    }

    // TypeToken: 0x2000083  // size: 0x14
    public sealed struct Spinner
    {
        // Fields
        private System.Int32 m_value;  // 0x10

        // Methods
        // RVA: 0x0B163CB4  token: 0x6000283
        public System.Void Lock() { }
        // RVA: 0x0B163CC8  token: 0x6000284
        public System.Void Unlock() { }

    }

    // TypeToken: 0x2000084
    public sealed struct UnmanagedArray`1 : System.IDisposable
    {
        // Fields
        private System.IntPtr m_pointer;  // 0x0
        private System.Int32 m_length;  // 0x0
        private Unity.Collections.AllocatorManager.AllocatorHandle m_allocator;  // 0x0

        // Properties
        T& Item { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000285
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x2000087  // size: 0x80
    public sealed class Try_000008B6$PostfixBurstDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B163E34  token: 0x6000292
        public System.Void .ctor(System.Object , System.IntPtr ) { }
        // RVA: 0x015802E0  token: 0x6000293
        public virtual System.Int32 Invoke(System.IntPtr state, Unity.Collections.AllocatorManager.Block& block) { }

    }

    // TypeToken: 0x2000088  // size: 0x10
    public static class Try_000008B6$BurstDirectCall
    {
        // Fields
        private static System.IntPtr Pointer;  // static @ 0x0
        private static System.IntPtr DeferredCompilation;  // static @ 0x8

        // Methods
        // RVA: 0x0B1642C4  token: 0x6000294
        private static System.Void GetFunctionPointerDiscard(System.IntPtr& ) { }
        // RVA: 0x0B1643C8  token: 0x6000295
        private static System.IntPtr GetFunctionPointer() { }
        // RVA: 0x03FB0870  token: 0x6000296
        public static System.Void Constructor() { }
        // RVA: 0x041E1670  token: 0x6000297
        public static System.Void Initialize() { }
        // RVA: 0x03FAEA70  token: 0x6000298
        private static System.Void .cctor() { }
        // RVA: 0x0B16441C  token: 0x6000299
        public static System.Int32 Invoke(System.IntPtr state, Unity.Collections.AllocatorManager.Block& block) { }

    }

    // TypeToken: 0x2000085  // size: 0x38
    public sealed struct RewindableAllocator : IAllocator, System.IDisposable
    {
        // Fields
        private Unity.Collections.Spinner m_spinner;  // 0x10
        private Unity.Collections.AllocatorManager.AllocatorHandle m_handle;  // 0x14
        private Unity.Collections.UnmanagedArray<Unity.Collections.RewindableAllocator.MemoryBlock> m_block;  // 0x18
        private System.Int32 m_best;  // 0x28
        private System.Int32 m_last;  // 0x2c
        private System.Int32 m_used;  // 0x30
        private System.Boolean m_enableBlockFree;  // 0x34

        // Properties
        Unity.Collections.AllocatorManager.AllocatorHandle Handle { get; /* RVA: 0x04D88B20 */ }

        // Methods
        // RVA: 0x0B163754  token: 0x6000287
        public System.Void Rewind() { }
        // RVA: 0x0B163688  token: 0x6000288
        public virtual System.Void Dispose() { }
        // RVA: 0x0B163840  token: 0x6000289
        public virtual System.Int32 Try(Unity.Collections.AllocatorManager.Block& block) { }
        // RVA: 0x0B163A08  token: 0x600028A
        private static System.Int32 Try(System.IntPtr state, Unity.Collections.AllocatorManager.Block& block) { }
        // RVA: 0x0B163838  token: 0x600028C
        public static System.Int32 Try$BurstManaged(System.IntPtr state, Unity.Collections.AllocatorManager.Block& block) { }

    }

    // TypeToken: 0x2000089  // size: 0x14
    public sealed struct FormatError
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Unity.Collections.FormatError None;  // const
        public static Unity.Collections.FormatError Overflow;  // const

    }

    // TypeToken: 0x200008A  // size: 0x14
    public sealed struct CopyError
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Unity.Collections.CopyError None;  // const
        public static Unity.Collections.CopyError Truncation;  // const

    }

    // TypeToken: 0x200008B  // size: 0x14
    public sealed struct ConversionError
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Unity.Collections.ConversionError None;  // const
        public static Unity.Collections.ConversionError Overflow;  // const
        public static Unity.Collections.ConversionError Encoding;  // const
        public static Unity.Collections.ConversionError CodePoint;  // const

    }

    // TypeToken: 0x200008C  // size: 0x11
    public sealed struct Unicode
    {
        // Properties
        Unity.Collections.Unicode.Rune ReplacementCharacter { get; /* RVA: 0x04DAB550 */ }

        // Methods
        // RVA: 0x03875EB0  token: 0x600029A
        public static System.Boolean IsValidCodePoint(System.Int32 codepoint) { }
        // RVA: 0x0B164628  token: 0x600029B
        public static System.Boolean NotTrailer(System.Byte b) { }
        // RVA: 0x03875E20  token: 0x600029D
        public static Unity.Collections.ConversionError Utf8ToUcs(Unity.Collections.Unicode.Rune& rune, System.Byte* buffer, System.Int32& index, System.Int32 capacity) { }
        // RVA: 0x03875EC0  token: 0x600029E
        private static System.Boolean IsLeadingSurrogate(System.Char c) { }
        // RVA: 0x0A7E1B0C  token: 0x600029F
        private static System.Boolean IsTrailingSurrogate(System.Char c) { }
        // RVA: 0x03875CE0  token: 0x60002A0
        public static Unity.Collections.ConversionError Utf16ToUcs(Unity.Collections.Unicode.Rune& rune, System.Char* buffer, System.Int32& index, System.Int32 capacity) { }
        // RVA: 0x03875CA0  token: 0x60002A1
        public static Unity.Collections.ConversionError UcsToUtf8(System.Byte* buffer, System.Int32& index, System.Int32 capacity, Unity.Collections.Unicode.Rune rune) { }
        // RVA: 0x03875E60  token: 0x60002A2
        public static Unity.Collections.ConversionError UcsToUtf16(System.Char* buffer, System.Int32& index, System.Int32 capacity, Unity.Collections.Unicode.Rune rune) { }
        // RVA: 0x03875BE0  token: 0x60002A3
        public static Unity.Collections.ConversionError Utf16ToUtf8(System.Char* utf16Buffer, System.Int32 utf16Length, System.Byte* utf8Buffer, System.Int32& utf8Length, System.Int32 utf8Capacity) { }
        // RVA: 0x03875D60  token: 0x60002A4
        public static Unity.Collections.ConversionError Utf8ToUtf16(System.Byte* utf8Buffer, System.Int32 utf8Length, System.Char* utf16Buffer, System.Int32& utf16Length, System.Int32 utf16Capacity) { }

    }

    // TypeToken: 0x200008E  // size: 0x10
    public static class UTF8ArrayUnsafeUtility
    {
        // Methods
        // RVA: 0x0B1644A8  token: 0x60002A5
        public static Unity.Collections.CopyError Copy(System.Byte* dest, System.Int32& destLength, System.Int32 destUTF8MaxLengthInBytes, System.Char* src, System.Int32 srcLength) { }
        // RVA: 0x03875B10  token: 0x60002A6
        public static Unity.Collections.CopyError Copy(System.Byte* dest, System.UInt16& destLength, System.UInt16 destUTF8MaxLengthInBytes, System.Char* src, System.Int32 srcLength) { }
        // RVA: 0x0B164590  token: 0x60002A7
        public static System.Int32 StrCmp(System.Byte* utf8BufferA, System.Int32 utf8LengthInBytesA, System.Byte* utf8BufferB, System.Int32 utf8LengthInBytesB) { }
        // RVA: 0x0B1644DC  token: 0x60002A8
        public static System.Boolean EqualsUTF8Bytes(System.Byte* aBytes, System.Int32 aLength, System.Byte* bBytes, System.Int32 bLength) { }
        // RVA: 0x0B1644F8  token: 0x60002A9
        public static System.Int32 StrCmp(System.Byte* utf8Buffer, System.Int32 utf8LengthInBytes, System.Char* utf16Buffer, System.Int32 utf16LengthInChars) { }

    }

    // TypeToken: 0x2000092  // size: 0x80
    public sealed class Hash64Long_000008F7$PostfixBurstDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B163218  token: 0x60002DC
        public System.Void .ctor(System.Object , System.IntPtr ) { }
        // RVA: 0x014D0D00  token: 0x60002DD
        public virtual System.UInt64 Invoke(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret) { }

    }

    // TypeToken: 0x2000093  // size: 0x10
    public static class Hash64Long_000008F7$BurstDirectCall
    {
        // Fields
        private static System.IntPtr Pointer;  // static @ 0x0
        private static System.IntPtr DeferredCompilation;  // static @ 0x8

        // Methods
        // RVA: 0x0B163010  token: 0x60002DE
        private static System.Void GetFunctionPointerDiscard(System.IntPtr& ) { }
        // RVA: 0x0B163114  token: 0x60002DF
        private static System.IntPtr GetFunctionPointer() { }
        // RVA: 0x03FB0900  token: 0x60002E0
        public static System.Void Constructor() { }
        // RVA: 0x041E1670  token: 0x60002E1
        public static System.Void Initialize() { }
        // RVA: 0x03FAEA80  token: 0x60002E2
        private static System.Void .cctor() { }
        // RVA: 0x0B163168  token: 0x60002E3
        public static System.UInt64 Invoke(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret) { }

    }

    // TypeToken: 0x2000094  // size: 0x80
    public sealed class Hash128Long_000008FE$PostfixBurstDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B162F3C  token: 0x60002E4
        public System.Void .ctor(System.Object , System.IntPtr ) { }
        // RVA: 0x014DD080  token: 0x60002E5
        public virtual System.Void Invoke(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret, Unity.Mathematics.uint4& result) { }

    }

    // TypeToken: 0x2000095  // size: 0x10
    public static class Hash128Long_000008FE$BurstDirectCall
    {
        // Fields
        private static System.IntPtr Pointer;  // static @ 0x0
        private static System.IntPtr DeferredCompilation;  // static @ 0x8

        // Methods
        // RVA: 0x0B162D20  token: 0x60002E6
        private static System.Void GetFunctionPointerDiscard(System.IntPtr& ) { }
        // RVA: 0x0B162E24  token: 0x60002E7
        private static System.IntPtr GetFunctionPointer() { }
        // RVA: 0x03FB0990  token: 0x60002E8
        public static System.Void Constructor() { }
        // RVA: 0x041E1670  token: 0x60002E9
        public static System.Void Initialize() { }
        // RVA: 0x03FAEA90  token: 0x60002EA
        private static System.Void .cctor() { }
        // RVA: 0x0B162E78  token: 0x60002EB
        public static System.Void Invoke(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret, Unity.Mathematics.uint4& result) { }

    }

    // TypeToken: 0x2000090  // size: 0x10
    public static class xxHash3
    {
        // Methods
        // RVA: 0x04D83230  token: 0x60002AB
        private static System.Void Avx2HashLongInternalLoop(System.UInt64* acc, System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret, System.Int32 isHash64) { }
        // RVA: 0x04D83230  token: 0x60002AC
        private static System.Void Avx2ScrambleAcc(System.UInt64* acc, System.Byte* secret) { }
        // RVA: 0x04D83230  token: 0x60002AD
        private static System.Void Avx2Accumulate(System.UInt64* acc, System.Byte* input, System.Byte* dest, System.Byte* secret, System.Int64 nbStripes, System.Int32 isHash64) { }
        // RVA: 0x04D83230  token: 0x60002AE
        private static System.Void Avx2Accumulate512(System.UInt64* acc, System.Byte* input, System.Byte* dest, System.Byte* secret) { }
        // RVA: 0x02F835D0  token: 0x60002AF
        public static Unity.Mathematics.uint2 Hash64(System.Void* input, System.Int64 length) { }
        // RVA: 0x0B1653CC  token: 0x60002B0
        public static Unity.Mathematics.uint4 Hash128(System.Void* input, System.Int64 length) { }
        // RVA: 0x02F838B0  token: 0x60002B1
        private static System.UInt64 Hash64Internal(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret, System.UInt64 seed) { }
        // RVA: 0x0B164B90  token: 0x60002B2
        private static System.Void Hash128Internal(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret, System.UInt64 seed, Unity.Mathematics.uint4& result) { }
        // RVA: 0x040F2920  token: 0x60002B3
        private static System.UInt64 Hash64Len1To3(System.Byte* input, System.Int64 len, System.Byte* secret, System.UInt64 seed) { }
        // RVA: 0x02F84BC0  token: 0x60002B4
        private static System.UInt64 Hash64Len4To8(System.Byte* input, System.Int64 length, System.Byte* secret, System.UInt64 seed) { }
        // RVA: 0x03F6B640  token: 0x60002B5
        private static System.UInt64 Hash64Len9To16(System.Byte* input, System.Int64 length, System.Byte* secret, System.UInt64 seed) { }
        // RVA: 0x02F84B10  token: 0x60002B6
        private static System.UInt64 Hash64Len0To16(System.Byte* input, System.Int64 length, System.Byte* secret, System.UInt64 seed) { }
        // RVA: 0x02F84CB0  token: 0x60002B7
        private static System.UInt64 Hash64Len17To128(System.Byte* input, System.Int64 length, System.Byte* secret, System.UInt64 seed) { }
        // RVA: 0x0461E140  token: 0x60002B8
        private static System.UInt64 Hash64Len129To240(System.Byte* input, System.Int64 length, System.Byte* secret, System.UInt64 seed) { }
        // RVA: 0x0B165470  token: 0x60002B9
        private static System.UInt64 Hash64Long(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret) { }
        // RVA: 0x0B165114  token: 0x60002BA
        private static System.Void Hash128Len1To3(System.Byte* input, System.Int64 length, System.Byte* secret, System.UInt64 seed, Unity.Mathematics.uint4& result) { }
        // RVA: 0x0B1651AC  token: 0x60002BB
        private static System.Void Hash128Len4To8(System.Byte* input, System.Int64 len, System.Byte* secret, System.UInt64 seed, Unity.Mathematics.uint4& result) { }
        // RVA: 0x0B16525C  token: 0x60002BC
        private static System.Void Hash128Len9To16(System.Byte* input, System.Int64 len, System.Byte* secret, System.UInt64 seed, Unity.Mathematics.uint4& result) { }
        // RVA: 0x0B164CD8  token: 0x60002BD
        private static System.Void Hash128Len0To16(System.Byte* input, System.Int64 length, System.Byte* secret, System.UInt64 seed, Unity.Mathematics.uint4& result) { }
        // RVA: 0x0B164F84  token: 0x60002BE
        private static System.Void Hash128Len17To128(System.Byte* input, System.Int64 length, System.Byte* secret, System.UInt64 seed, Unity.Mathematics.uint4& result) { }
        // RVA: 0x0B164D7C  token: 0x60002BF
        private static System.Void Hash128Len129To240(System.Byte* input, System.Int64 length, System.Byte* secret, System.UInt64 seed, Unity.Mathematics.uint4& result) { }
        // RVA: 0x0B16534C  token: 0x60002C0
        private static System.Void Hash128Long(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret, Unity.Mathematics.uint4& result) { }
        // RVA: 0x04DAB5F0  token: 0x60002C1
        private static Unity.Mathematics.uint2 ToUint2(System.UInt64 u) { }
        // RVA: 0x04DAB610  token: 0x60002C2
        private static Unity.Mathematics.uint4 ToUint4(System.UInt64 ul0, System.UInt64 ul1) { }
        // RVA: 0x0B164B60  token: 0x60002C3
        private static System.Void EncodeSecretKey(System.Byte* dst, System.Byte* secret, System.UInt64 seed) { }
        // RVA: 0x02B76770  token: 0x60002C4
        private static System.UInt64 Read64LE(System.Void* addr) { }
        // RVA: 0x02B77620  token: 0x60002C5
        private static System.UInt32 Read32LE(System.Void* addr) { }
        // RVA: 0x04D88B60  token: 0x60002C6
        private static System.Void Write64LE(System.Void* addr, System.UInt64 value) { }
        // RVA: 0x04DA5160  token: 0x60002C7
        private static System.UInt64 Mul32To64(System.UInt32 x, System.UInt32 y) { }
        // RVA: 0x04DAB5E0  token: 0x60002C8
        private static System.UInt64 Swap64(System.UInt64 x) { }
        // RVA: 0x04DA4CD0  token: 0x60002C9
        private static System.UInt32 Swap32(System.UInt32 x) { }
        // RVA: 0x04D882E0  token: 0x60002CA
        private static System.UInt32 RotL32(System.UInt32 x, System.Int32 r) { }
        // RVA: 0x04DAB5C0  token: 0x60002CB
        private static System.UInt64 RotL64(System.UInt64 x, System.Int32 r) { }
        // RVA: 0x04DAB630  token: 0x60002CC
        private static System.UInt64 XorShift64(System.UInt64 v64, System.Int32 shift) { }
        // RVA: 0x0B16563C  token: 0x60002CD
        private static System.UInt64 Mul128Fold64(System.UInt64 lhs, System.UInt64 rhs) { }
        // RVA: 0x0B165570  token: 0x60002CE
        private static System.UInt64 Mix16(System.Byte* input, System.Byte* secret, System.UInt64 seed) { }
        // RVA: 0x0B16559C  token: 0x60002CF
        private static Unity.Collections.xxHash3.ulong2 Mix32(Unity.Collections.xxHash3.ulong2 acc, System.Byte* input_1, System.Byte* input_2, System.Byte* secret, System.UInt64 seed) { }
        // RVA: 0x04DAB590  token: 0x60002D0
        private static System.UInt64 Avalanche(System.UInt64 h64) { }
        // RVA: 0x040F2960  token: 0x60002D1
        private static System.UInt64 AvalancheH64(System.UInt64 h64) { }
        // RVA: 0x04DAB650  token: 0x60002D2
        private static System.UInt64 rrmxmx(System.UInt64 h64, System.UInt64 length) { }
        // RVA: 0x0B165590  token: 0x60002D3
        private static System.UInt64 Mix2Acc(System.UInt64 acc0, System.UInt64 acc1, System.Byte* secret) { }
        // RVA: 0x0B1654E4  token: 0x60002D4
        private static System.UInt64 MergeAcc(System.UInt64* acc, System.Byte* secret, System.UInt64 start) { }
        // RVA: 0x0B1649BC  token: 0x60002D5
        private static System.Void DefaultHashLongInternalLoop(System.UInt64* acc, System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret, System.Int32 isHash64) { }
        // RVA: 0x0B164918  token: 0x60002D6
        private static System.Void DefaultAccumulate(System.UInt64* acc, System.Byte* input, System.Byte* dest, System.Byte* secret, System.Int64 nbStripes, System.Int32 isHash64) { }
        // RVA: 0x0B164898  token: 0x60002D7
        private static System.Void DefaultAccumulate512(System.UInt64* acc, System.Byte* input, System.Byte* dest, System.Byte* secret, System.Int32 isHash64) { }
        // RVA: 0x0B164B30  token: 0x60002D8
        private static System.Void DefaultScrambleAcc(System.UInt64* acc, System.Byte* secret) { }
        // RVA: 0x0B15FA9C  token: 0x60002D9
        public static System.UInt64 Hash64Long$BurstManaged(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret) { }
        // RVA: 0x0B15F960  token: 0x60002DA
        public static System.Void Hash128Long$BurstManaged(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret, Unity.Mathematics.uint4& result) { }

    }

    // TypeToken: 0x2000096  // size: 0x10
    public static class xxHashDefaultKey
    {
        // Fields
        public static readonly System.Byte[] kSecret;  // static @ 0x0

        // Methods
        // RVA: 0x04D164D0  token: 0x60002EC
        private static System.Void .cctor() { }

    }

}

namespace Unity.Collections.LowLevel.Unsafe
{

    // TypeToken: 0x2000098  // size: 0x10
    public static class NativeListUnsafeUtility
    {
        // Methods
        // RVA: -1  // generic def  token: 0x60002EE
        public static System.Void* GetUnsafePtr(Unity.Collections.NativeList<T> list) { }
        // RVA: -1  // generic def  token: 0x60002EF
        public static System.Void* GetUnsafeReadOnlyPtr(Unity.Collections.NativeList<T> list) { }
        // RVA: -1  // generic def  token: 0x60002F0
        public static System.Void* GetInternalListDataPtrUnchecked(Unity.Collections.NativeList<T>& list) { }

    }

    // TypeToken: 0x2000099  // size: 0x10
    public static class NativeReferenceUnsafeUtility
    {
        // Methods
        // RVA: -1  // generic def  token: 0x60002F1
        public static System.Void* GetUnsafePtr(Unity.Collections.NativeReference<T> reference) { }
        // RVA: -1  // generic def  token: 0x60002F2
        public static System.Void* GetUnsafePtrWithoutChecks(Unity.Collections.NativeReference<T> reference) { }

    }

    // TypeToken: 0x200009A  // size: 0x28
    public sealed struct UnsafeAppendBuffer : Unity.Collections.INativeDisposable, System.IDisposable
    {
        // Fields
        public System.Byte* Ptr;  // 0x10
        public System.Int32 Length;  // 0x18
        public System.Int32 Capacity;  // 0x1c
        public Unity.Collections.AllocatorManager.AllocatorHandle Allocator;  // 0x20
        public readonly System.Int32 Alignment;  // 0x24

        // Methods
        // RVA: 0x0B164634  token: 0x60002F3
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x200009B  // size: 0x20
    public sealed struct UnsafeDisposeJob : Unity.Jobs.IJob
    {
        // Fields
        public System.Void* Ptr;  // 0x10
        public Unity.Collections.AllocatorManager.AllocatorHandle Allocator;  // 0x18

        // Methods
        // RVA: 0x0B1646B8  token: 0x60002F4
        public virtual System.Void Execute() { }

    }

    // TypeToken: 0x200009C  // size: 0x30
    public sealed struct UntypedUnsafeList
    {
        // Fields
        public System.Void* Ptr;  // 0x10
        public System.Int32 m_length;  // 0x18
        public System.Int32 m_capacity;  // 0x1c
        public Unity.Collections.AllocatorManager.AllocatorHandle Allocator;  // 0x20
        private System.Int32 obsolete_length;  // 0x24
        private System.Int32 obsolete_capacity;  // 0x28

    }

    // TypeToken: 0x200009D
    public sealed struct UnsafeList`1 : Unity.Collections.INativeDisposable, System.IDisposable, Unity.Collections.INativeList`1, Unity.Collections.IIndexable`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        public T* Ptr;  // 0x0
        public System.Int32 m_length;  // 0x0
        public System.Int32 m_capacity;  // 0x0
        public Unity.Collections.AllocatorManager.AllocatorHandle Allocator;  // 0x0
        public System.Int32 length;  // 0x0
        public System.Int32 capacity;  // 0x0

        // Properties
        System.Int32 Length { get; /* RVA: -1  // not resolved */ }
        System.Int32 Capacity { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        T Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Boolean IsCreated { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60002FA
        public virtual T& ElementAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60002FB
        public System.Void .ctor(System.Int32 initialCapacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator, Unity.Collections.NativeArrayOptions options) { }
        // RVA: -1  // generic def  token: 0x60002FC
        private static Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* Create(System.Int32 initialCapacity, U& allocator, Unity.Collections.NativeArrayOptions options) { }
        // RVA: -1  // not resolved  token: 0x60002FD
        public static Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* Create(System.Int32 initialCapacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator, Unity.Collections.NativeArrayOptions options) { }
        // RVA: -1  // not resolved  token: 0x60002FE
        public static System.Void Destroy(Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* listData) { }
        // RVA: -1  // not resolved  token: 0x6000300
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000301
        public virtual Unity.Jobs.JobHandle Dispose(Unity.Jobs.JobHandle inputDeps) { }
        // RVA: -1  // not resolved  token: 0x6000302
        public Unity.Jobs.JobHandle CrossFrameDispose(Unity.Jobs.JobHandle inputDeps, Unity.Jobs.LowLevel.Unsafe.JobQueuePriority priority) { }
        // RVA: -1  // not resolved  token: 0x6000303
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000304
        public System.Void Resize(System.Int32 length, Unity.Collections.NativeArrayOptions options) { }
        // RVA: -1  // generic def  token: 0x6000305
        private System.Void Realloc(U& allocator, System.Int32 newCapacity) { }
        // RVA: -1  // generic def  token: 0x6000306
        private System.Void SetCapacity(U& allocator, System.Int32 capacity) { }
        // RVA: -1  // not resolved  token: 0x6000307
        public System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: -1  // not resolved  token: 0x6000308
        public System.Void AddNoResize(T value) { }
        // RVA: -1  // not resolved  token: 0x6000309
        public System.Void AddRangeNoResize(System.Void* ptr, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x600030A
        public System.Void Add(T& value) { }
        // RVA: -1  // not resolved  token: 0x600030B
        public System.Void AddRange(System.Void* ptr, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x600030C
        public System.Void RemoveAtSwapBack(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x600030D
        public System.Void RemoveRangeSwapBack(System.Int32 index, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x600030E
        public System.Void RemoveAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x600030F
        public System.Void RemoveRange(System.Int32 index, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x6000310
        public Unity.Collections.LowLevel.Unsafe.UnsafeList.ParallelReader<T> AsParallelReader() { }
        // RVA: -1  // not resolved  token: 0x6000311
        public Unity.Collections.LowLevel.Unsafe.UnsafeList.ParallelWriter<T> AsParallelWriter() { }
        // RVA: -1  // not resolved  token: 0x6000312
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000313
        private virtual System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator() { }

    }

    // TypeToken: 0x20000A0
    public sealed class UnsafeListTDebugView`1
    {
    }

    // TypeToken: 0x20000A1  // size: 0x2050
    public sealed struct UnsafeParallelHashMapData
    {
        // Fields
        private System.Byte* values;  // 0x10
        private System.Byte* keys;  // 0x18
        private System.Byte* next;  // 0x20
        private System.Byte* buckets;  // 0x28
        private System.Int32 keyCapacity;  // 0x30
        private System.Int32 bucketCapacityMask;  // 0x34
        private System.Int32 allocatedIndexLength;  // 0x38
        private Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.<firstFreeTLS>e__FixedBuffer firstFreeTLS;  // 0x50
        private static System.Int32 IntsPerCacheLine;  // const

        // Methods
        // RVA: 0x04DAB580  token: 0x6000317
        private static System.Int32 GetBucketSize(System.Int32 capacity) { }
        // RVA: 0x0352EE60  token: 0x6000318
        private static System.Int32 GrowCapacity(System.Int32 capacity) { }
        // RVA: -1  // generic def  token: 0x6000319
        private static System.Void AllocateHashMap(System.Int32 length, System.Int32 bucketLength, Unity.Collections.AllocatorManager.AllocatorHandle label, Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData*& outBuf) { }
        // RVA: -1  // generic def  token: 0x600031A
        private static System.Void ReallocateHashMap(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, System.Int32 newCapacity, System.Int32 newBucketCapacity, Unity.Collections.AllocatorManager.AllocatorHandle label) { }
        // RVA: 0x037F6580  token: 0x600031B
        private static System.Void DeallocateHashMap(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // generic def  token: 0x600031C
        private static System.Int32 CalculateDataSize(System.Int32 length, System.Int32 bucketLength, System.Int32& keyOffset, System.Int32& nextOffset, System.Int32& bucketOffset) { }
        // RVA: 0x0394D400  token: 0x600031D
        private static System.Boolean IsEmpty(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data) { }
        // RVA: 0x03A09AA0  token: 0x600031E
        private static System.Int32 GetCount(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data) { }
        // RVA: 0x034ACF90  token: 0x600031F
        private static System.Boolean MoveNext(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, System.Int32& bucketIndex, System.Int32& nextIndex, System.Int32& index) { }
        // RVA: -1  // generic def  token: 0x6000320
        private static System.Void GetKeyArray(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, Unity.Collections.NativeArray<TKey> result) { }
        // RVA: -1  // generic def  token: 0x6000321
        private static System.Void GetValueArray(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, Unity.Collections.NativeArray<TValue> result) { }

    }

    // TypeToken: 0x20000A3  // size: 0x20
    public sealed struct UnsafeParallelHashMapDataDispose
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* m_Buffer;  // 0x10
        private Unity.Collections.AllocatorManager.AllocatorHandle m_AllocatorLabel;  // 0x18

        // Methods
        // RVA: 0x0B164714  token: 0x6000322
        public System.Void Dispose() { }

    }

    // TypeToken: 0x20000A4  // size: 0x20
    public sealed struct UnsafeParallelHashMapDataDisposeJob : Unity.Jobs.IJob
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataDispose Data;  // 0x10

        // Methods
        // RVA: 0x0B16470C  token: 0x6000323
        public virtual System.Void Execute() { }

    }

    // TypeToken: 0x20000A5
    public sealed struct UnsafeParallelHashMapBase`2
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000324
        private static System.Void Clear(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data) { }
        // RVA: -1  // not resolved  token: 0x6000325
        private static System.Int32 AllocEntry(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, System.Int32 threadIndex) { }
        // RVA: -1  // not resolved  token: 0x6000326
        private static System.Void FreeEntry(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, System.Int32 idx, System.Int32 threadIndex) { }
        // RVA: -1  // not resolved  token: 0x6000327
        private static System.Boolean TryAddAtomic(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, TKey key, TValue item, System.Int32 threadIndex) { }
        // RVA: -1  // not resolved  token: 0x6000328
        private static System.Void AddAtomicMulti(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, TKey key, TValue item, System.Int32 threadIndex) { }
        // RVA: -1  // not resolved  token: 0x6000329
        private static System.Boolean TryAdd(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, TKey key, TValue& item, System.Boolean isMultiHashMap, Unity.Collections.AllocatorManager.AllocatorHandle allocation) { }
        // RVA: -1  // not resolved  token: 0x600032A
        private static System.Int32 Remove(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, TKey key, System.Boolean isMultiHashMap) { }
        // RVA: -1  // not resolved  token: 0x600032B
        private static System.Void Remove(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it) { }
        // RVA: -1  // not resolved  token: 0x600032C
        private static System.Boolean TryGetFirstValueAtomic(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, TKey key, TValue& item, Unity.Collections.NativeParallelMultiHashMapIterator<TKey>& it) { }
        // RVA: -1  // not resolved  token: 0x600032D
        private static System.Boolean TryGetNextValueAtomic(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, TValue& item, Unity.Collections.NativeParallelMultiHashMapIterator<TKey>& it) { }
        // RVA: -1  // not resolved  token: 0x600032E
        private static System.Boolean SetValue(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, Unity.Collections.NativeParallelMultiHashMapIterator<TKey>& it, TValue& item) { }

    }

    // TypeToken: 0x20000A6
    public sealed struct KeyValue`2
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* m_Buffer;  // 0x0
        private System.Int32 m_Index;  // 0x0
        private System.Int32 m_Next;  // 0x0

        // Properties
        TKey Key { get; /* RVA: -1  // not resolved */ }
        TValue& Value { get; /* RVA: -1  // not resolved */ }

    }

    // TypeToken: 0x20000A7  // size: 0x28
    public sealed struct UnsafeParallelHashMapDataEnumerator
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* m_Buffer;  // 0x10
        private System.Int32 m_Index;  // 0x18
        private System.Int32 m_BucketIndex;  // 0x1c
        private System.Int32 m_NextIndex;  // 0x20

        // Methods
        // RVA: 0x04DAB560  token: 0x6000331
        private System.Void .ctor(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data) { }
        // RVA: 0x03824360  token: 0x6000332
        private System.Boolean MoveNext() { }
        // RVA: 0x04D95D70  token: 0x6000333
        private System.Void Reset() { }
        // RVA: -1  // generic def  token: 0x6000334
        private Unity.Collections.LowLevel.Unsafe.KeyValue<TKey,TValue> GetCurrent() { }
        // RVA: -1  // generic def  token: 0x6000335
        private TKey GetCurrentKey() { }

    }

    // TypeToken: 0x20000A8
    public sealed struct UnsafeParallelHashMap`2 : Unity.Collections.INativeDisposable, System.IDisposable, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* m_Buffer;  // 0x0
        private Unity.Collections.AllocatorManager.AllocatorHandle m_AllocatorLabel;  // 0x0

        // Properties
        System.Boolean IsEmpty { get; /* RVA: -1  // not resolved */ }
        System.Int32 Capacity { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        TKey Item { set; /* RVA: -1  // not resolved */ }
        System.Boolean IsCreated { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000336
        public System.Void .ctor(System.Int32 capacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // not resolved  token: 0x6000338
        public System.Int32 Count() { }
        // RVA: -1  // not resolved  token: 0x600033B
        public System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x600033C
        public System.Boolean TryAdd(TKey key, TValue& item) { }
        // RVA: -1  // not resolved  token: 0x600033D
        public System.Boolean Remove(TKey key) { }
        // RVA: -1  // not resolved  token: 0x600033E
        public System.Boolean TryGetValue(TKey key, TValue& item) { }
        // RVA: -1  // not resolved  token: 0x600033F
        public System.Boolean ContainsKey(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000342
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000343
        public Unity.Collections.NativeArray<TKey> GetKeyArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // not resolved  token: 0x6000344
        public Unity.Collections.NativeArray<TValue> GetValueArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // not resolved  token: 0x6000345
        public Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap.ParallelWriter<TKey,TValue> AsParallelWriter() { }
        // RVA: -1  // not resolved  token: 0x6000346
        private virtual System.Collections.Generic.IEnumerator<Unity.Collections.LowLevel.Unsafe.KeyValue<TKey,TValue>> System.Collections.Generic.IEnumerable<Unity.Collections.LowLevel.Unsafe.KeyValue<TKey,TValue>>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000347
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x20000AA  // size: 0x20
    public sealed struct UnsafeParallelHashMapDisposeJob : Unity.Jobs.IJob
    {
        // Fields
        public Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* Data;  // 0x10
        public Unity.Collections.AllocatorManager.AllocatorHandle Allocator;  // 0x18

        // Methods
        // RVA: 0x0B164714  token: 0x600034A
        public virtual System.Void Execute() { }

    }

    // TypeToken: 0x20000AB
    public sealed class UnsafeParallelHashMapDebuggerTypeProxy`2
    {
    }

    // TypeToken: 0x20000AC
    public sealed struct UnsafeParallelHashSet`1 : Unity.Collections.INativeDisposable, System.IDisposable, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<T,System.Boolean> m_Data;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600034B
        public System.Void .ctor(System.Int32 capacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // not resolved  token: 0x600034C
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x600034D
        public System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x600034E
        public System.Boolean Add(T item) { }
        // RVA: -1  // not resolved  token: 0x600034F
        public System.Boolean Remove(T item) { }
        // RVA: -1  // not resolved  token: 0x6000350
        public System.Boolean Contains(T item) { }
        // RVA: -1  // not resolved  token: 0x6000351
        public Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSet.Enumerator<T> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000352
        private virtual System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000353
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x20000AE
    public sealed class UnsafeHashSetDebuggerTypeProxy`1
    {
    }

    // TypeToken: 0x20000AF  // size: 0x10
    public static class HashSetExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000359
        public static System.Void UnionWith(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSet<T> container, Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSet<T> other) { }

    }

    // TypeToken: 0x20000B0
    public sealed struct UnsafeParallelMultiHashMap`2 : Unity.Collections.INativeDisposable, System.IDisposable, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* m_Buffer;  // 0x0
        private Unity.Collections.AllocatorManager.AllocatorHandle m_AllocatorLabel;  // 0x0

        // Properties
        System.Boolean IsEmpty { get; /* RVA: -1  // not resolved */ }
        System.Int32 Capacity { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsCreated { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600035A
        public System.Void .ctor(System.Int32 capacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // not resolved  token: 0x600035C
        public System.Int32 Count() { }
        // RVA: -1  // not resolved  token: 0x600035E
        public System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x600035F
        public System.Void Add(TKey key, TValue item) { }
        // RVA: -1  // not resolved  token: 0x6000360
        public System.Int32 Remove(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000361
        public System.Void Remove(Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it) { }
        // RVA: -1  // not resolved  token: 0x6000362
        public System.Boolean TryGetFirstValue(TKey key, TValue& item, Unity.Collections.NativeParallelMultiHashMapIterator<TKey>& it) { }
        // RVA: -1  // not resolved  token: 0x6000363
        public System.Boolean TryGetNextValue(TValue& item, Unity.Collections.NativeParallelMultiHashMapIterator<TKey>& it) { }
        // RVA: -1  // not resolved  token: 0x6000364
        public System.Boolean SetValue(TValue item, Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it) { }
        // RVA: -1  // not resolved  token: 0x6000366
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000367
        public Unity.Collections.NativeArray<TKey> GetKeyArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // not resolved  token: 0x6000368
        public Unity.Collections.NativeArray<TValue> GetValueArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator) { }
        // RVA: -1  // not resolved  token: 0x6000369
        public Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap.ParallelWriter<TKey,TValue> AsParallelWriter() { }
        // RVA: -1  // not resolved  token: 0x600036A
        private virtual System.Collections.Generic.IEnumerator<Unity.Collections.LowLevel.Unsafe.KeyValue<TKey,TValue>> System.Collections.Generic.IEnumerable<Unity.Collections.LowLevel.Unsafe.KeyValue<TKey,TValue>>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600036B
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x20000B2
    public sealed class UnsafeParallelMultiHashMapDebuggerTypeProxy`2
    {
    }

    // TypeToken: 0x20000B3  // size: 0x20
    public sealed struct UnsafeStreamBlock
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock* Next;  // 0x10
        private Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock.<Data>e__FixedBuffer Data;  // 0x18

    }

    // TypeToken: 0x20000B5  // size: 0x28
    public sealed struct UnsafeStreamRange
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock* Block;  // 0x10
        private System.Int32 OffsetInFirstBlock;  // 0x18
        private System.Int32 ElementCount;  // 0x1c
        private System.Int32 LastOffset;  // 0x20
        private System.Int32 NumberOfBlocks;  // 0x24

    }

    // TypeToken: 0x20000B6  // size: 0x40
    public sealed struct UnsafeStreamBlockData
    {
        // Fields
        private Unity.Collections.AllocatorManager.AllocatorHandle Allocator;  // 0x10
        private Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock** Blocks;  // 0x18
        private System.Int32 BlockCount;  // 0x20
        private Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock* Free;  // 0x28
        private Unity.Collections.LowLevel.Unsafe.UnsafeStreamRange* Ranges;  // 0x30
        private System.Int32 RangeCount;  // 0x38

    }

    // TypeToken: 0x20000B7  // size: 0x20
    public sealed struct UnsafeStream : Unity.Collections.INativeDisposable, System.IDisposable
    {
        // Fields
        private Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData* m_Block;  // 0x10
        private Unity.Collections.AllocatorManager.AllocatorHandle m_Allocator;  // 0x18

        // Methods
        // RVA: 0x0B164724  token: 0x600036D
        private System.Void AllocateForEach(System.Int32 forEachCount) { }
        // RVA: 0x0B1647A4  token: 0x600036E
        private System.Void Deallocate() { }
        // RVA: 0x0B160058  token: 0x600036F
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x20000BB  // size: 0x10
    public static class UnsafeUtilityExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000373
        public static System.Void* AddressOf(T& value) { }
        // RVA: -1  // generic def  token: 0x6000374
        public static T& AsRef(T& value) { }
        // RVA: 0x03116130  token: 0x6000375
        public static System.IntPtr Malloc(System.Int64 size, System.Int32 alignment, Unity.Collections.Allocator allocator) { }

    }

}

namespace Unity.Collections.NotBurstCompatible
{

    // TypeToken: 0x2000097  // size: 0x10
    public static class Extensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x60002ED
        public static T[] ToArrayNBC(Unity.Collections.NativeList<T> list) { }

    }

}

namespace Unity.Jobs
{

    // TypeToken: 0x2000004  // size: 0x10
    public class EarlyInitHelpers
    {
        // Methods
        // RVA: 0x0B160060  token: 0x6000003
        public static System.Void JobReflectionDataCreationFailed(System.Exception ex, System.Type jobType) { }

    }

    // TypeToken: 0x2000005
    public interface IJobBurstSchedulable
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000004
        public virtual System.Void Execute() { }

    }

    // TypeToken: 0x2000006  // size: 0x10
    public static class IJobBurstSchedulableExtensions
    {
    }

    // TypeToken: 0x2000009
    public interface IJobParallelForBatch
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600000A
        public virtual System.Void Execute(System.Int32 startIndex, System.Int32 count) { }

    }

    // TypeToken: 0x200000A  // size: 0x10
    public static class IJobParallelForBatchExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x600000B
        public static System.Void EarlyJobInit() { }
        // RVA: -1  // generic def  token: 0x600000C
        public static Unity.Jobs.JobHandle ScheduleBatch(T jobData, System.Int32 arrayLength, System.Int32 minIndicesPerJobCount, Unity.Jobs.JobHandle dependsOn) { }

    }

    // TypeToken: 0x200000D
    public interface IJobParallelForBurstSchedulable
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000012
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x200000E  // size: 0x10
    public static class IJobParallelForExtensionsBurstSchedulable
    {
    }

    // TypeToken: 0x2000011
    public interface IJobParallelForDefer
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000018
        public virtual System.Void Execute(System.Int32 index) { }

    }

    // TypeToken: 0x2000012  // size: 0x10
    public static class IJobParallelForDeferExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000019
        public static System.Void EarlyJobInit() { }
        // RVA: -1  // generic def  token: 0x600001A
        public static Unity.Jobs.JobHandle Schedule(T jobData, Unity.Collections.NativeList<U> list, System.Int32 innerloopBatchCount, Unity.Jobs.JobHandle dependsOn) { }
        // RVA: -1  // generic def  token: 0x600001B
        public static Unity.Jobs.JobHandle Schedule(T jobData, System.Int32* forEachCount, System.Int32 innerloopBatchCount, Unity.Jobs.JobHandle dependsOn) { }
        // RVA: -1  // generic def  token: 0x600001C
        public static Unity.Jobs.JobHandle ScheduleByRef(T& jobData, System.Int32* forEachCount, System.Int32 innerloopBatchCount, Unity.Jobs.JobHandle dependsOn) { }
        // RVA: -1  // generic def  token: 0x600001D
        private static Unity.Jobs.JobHandle ScheduleInternal(T& jobData, System.Int32 innerloopBatchCount, System.Void* forEachListPtr, System.Void* atomicSafetyHandlePtr, Unity.Jobs.JobHandle dependsOn) { }

    }

    // TypeToken: 0x2000015
    public interface IJobParallelForFilter
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000023
        public virtual System.Boolean Execute(System.Int32 index) { }

    }

    // TypeToken: 0x2000016  // size: 0x10
    public static class JobParallelIndexListExtensions
    {
    }

    // TypeToken: 0x200001A  // size: 0x10
    public class DOTSCompilerGeneratedAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600002B
        public System.Void .ctor() { }

    }

}

