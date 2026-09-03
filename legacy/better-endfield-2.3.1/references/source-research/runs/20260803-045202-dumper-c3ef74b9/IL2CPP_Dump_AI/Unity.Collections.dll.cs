// ========================================================
// Dumped by @desirepro
// Assembly: Unity.Collections.dll
// Classes:  191
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

CLASS: ExecuteJobFunction
TYPE:  sealed class
TOKEN: 0x2000008
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000008  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000009  System.Void Invoke(T& data, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex)
END_CLASS

CLASS: JobBurstSchedulableProducer`1
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x11
FIELDS:
  private   static readonly Unity.Burst.SharedStatic<System.IntPtr>jobReflectionData  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000005  System.Void Initialize()
  RVA=-1  // not resolved  token=0x6000006  System.Void Execute(T& data, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex)
  RVA=-1  // not resolved  token=0x6000007  System.Void .cctor()
END_CLASS

CLASS: ExecuteJobFunction
TYPE:  sealed class
TOKEN: 0x200000C
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000010  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000011  System.Void Invoke(T& jobData, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex)
END_CLASS

CLASS: JobParallelForBatchProducer`1
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x11
FIELDS:
  private   static readonly Unity.Burst.SharedStatic<System.IntPtr>jobReflectionData  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600000D  System.Void Initialize()
  RVA=-1  // not resolved  token=0x600000E  System.Void Execute(T& jobData, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex)
  RVA=-1  // not resolved  token=0x600000F  System.Void .cctor()
END_CLASS

CLASS: ExecuteJobFunction
TYPE:  sealed class
TOKEN: 0x2000010
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000016  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000017  System.Void Invoke(T& data, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex)
END_CLASS

CLASS: JobParallelForBurstSchedulableProducer`1
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x11
FIELDS:
  private   static readonly Unity.Burst.SharedStatic<System.IntPtr>jobReflectionData  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000013  System.Void Initialize()
  RVA=-1  // not resolved  token=0x6000014  System.Void Execute(T& jobData, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex)
  RVA=-1  // not resolved  token=0x6000015  System.Void .cctor()
END_CLASS

CLASS: ExecuteJobFunction
TYPE:  sealed class
TOKEN: 0x2000014
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000021  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000022  System.Void Invoke(T& jobData, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex)
END_CLASS

CLASS: JobParallelForDeferProducer`1
TYPE:  sealed struct
TOKEN: 0x2000013
SIZE:  0x11
FIELDS:
  private   static readonly Unity.Burst.SharedStatic<System.IntPtr>jobReflectionData  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600001E  System.Void Initialize()
  RVA=-1  // not resolved  token=0x600001F  System.Void Execute(T& jobData, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex)
  RVA=-1  // not resolved  token=0x6000020  System.Void .cctor()
END_CLASS

CLASS: JobWrapper
TYPE:  sealed struct
TOKEN: 0x2000018
FIELDS:
  public            Unity.Collections.NativeList<System.Int32>outputIndices  // 0x0
  public            System.Int32                    appendCount  // 0x0
  public            T                               JobData  // 0x0
METHODS:
END_CLASS

CLASS: ExecuteJobFunction
TYPE:  sealed class
TOKEN: 0x2000019
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000029  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600002A  System.Void Invoke(Unity.Jobs.JobParallelIndexListExtensions.JobParallelForFilterProducer.JobWrapper<T>& jobWrapper, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex)
END_CLASS

CLASS: JobParallelForFilterProducer`1
TYPE:  sealed struct
TOKEN: 0x2000017
SIZE:  0x11
FIELDS:
  private   static readonly Unity.Burst.SharedStatic<System.IntPtr>jobReflectionData  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000024  System.Void Initialize()
  RVA=-1  // not resolved  token=0x6000025  System.Void Execute(Unity.Jobs.JobParallelIndexListExtensions.JobParallelForFilterProducer.JobWrapper<T>& jobWrapper, System.IntPtr additionalPtr, System.IntPtr bufferRangePatchData, Unity.Jobs.LowLevel.Unsafe.JobRanges& ranges, System.Int32 jobIndex)
  RVA=-1  // not resolved  token=0x6000026  System.Void ExecuteAppend(Unity.Jobs.JobParallelIndexListExtensions.JobParallelForFilterProducer.JobWrapper<T>& jobWrapper, System.IntPtr bufferRangePatchData)
  RVA=-1  // not resolved  token=0x6000027  System.Void ExecuteFilter(Unity.Jobs.JobParallelIndexListExtensions.JobParallelForFilterProducer.JobWrapper<T>& jobWrapper, System.IntPtr bufferRangePatchData)
  RVA=-1  // not resolved  token=0x6000028  System.Void .cctor()
END_CLASS

CLASS: TryFunction
TYPE:  sealed class
TOKEN: 0x200001C
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B163E34  token=0x6000042  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x015802E0  token=0x6000043  System.Int32 Invoke(System.IntPtr allocatorState, Unity.Collections.AllocatorManager.Block& block)
END_CLASS

CLASS: AllocatorHandle
TYPE:  sealed struct
TOKEN: 0x200001D
SIZE:  0x14
IMPLEMENTS: IAllocator System.IDisposable
FIELDS:
  public            System.UInt16                   Index  // 0x10
  public            System.UInt16                   Version  // 0x12
PROPERTIES:
  TableEntry  get=0x0B15FC48
  Value  get=0x04D87110
  Handle  get=0x02B77620
  ToAllocator  get=0x04DAB3E0
METHODS:
  RVA=0x041E1670  token=0x6000045  System.Void Rewind()
  RVA=0x04DAB3F0  token=0x6000046  Unity.Collections.AllocatorManager.AllocatorHandle op_Implicit(Unity.Collections.Allocator a)
  RVA=0x03347A00  token=0x6000048  System.Int32 Try(Unity.Collections.AllocatorManager.Block& block)
  RVA=0x041E1670  token=0x600004B  System.Void Dispose()
END_CLASS

CLASS: Range
TYPE:  sealed struct
TOKEN: 0x200001E
SIZE:  0x20
IMPLEMENTS: System.IDisposable
FIELDS:
  public            System.IntPtr                   Pointer  // 0x10
  public            System.Int32                    Items  // 0x18
  public            Unity.Collections.AllocatorManager.AllocatorHandleAllocator  // 0x1c
METHODS:
  RVA=0x0B163650  token=0x600004C  System.Void Dispose()
END_CLASS

CLASS: Block
TYPE:  sealed struct
TOKEN: 0x200001F
SIZE:  0x30
IMPLEMENTS: System.IDisposable
FIELDS:
  public            Unity.Collections.AllocatorManager.RangeRange  // 0x10
  public            System.Int32                    BytesPerItem  // 0x20
  public            System.Int32                    AllocatedItems  // 0x24
  public            System.Byte                     Log2Alignment  // 0x28
  public            System.Byte                     Padding0  // 0x29
  public            System.UInt16                   Padding1  // 0x2a
  public            System.UInt32                   Padding2  // 0x2c
PROPERTIES:
  Bytes  get=0x04DAB4F0
  AllocatedBytes  get=0x04DAB4E0
  Alignment  get=0x04DAB4D0  set=0x03347C90
METHODS:
  RVA=0x0B15FF9C  token=0x6000051  System.Void Dispose()
  RVA=0x0B15FFA4  token=0x6000052  System.Int32 TryFree()
END_CLASS

CLASS: IAllocator
TYPE:  interface
TOKEN: 0x2000020
IMPLEMENTS: System.IDisposable
FIELDS:
PROPERTIES:
  Handle  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000053  System.Int32 Try(Unity.Collections.AllocatorManager.Block& block)
END_CLASS

CLASS: Try_000000A0$PostfixBurstDelegate
TYPE:  sealed class
TOKEN: 0x2000022
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B163E34  token=0x600005A  System.Void .ctor(System.Object , System.IntPtr )
  RVA=0x015802E0  token=0x600005B  System.Int32 Invoke(System.IntPtr allocatorState, Unity.Collections.AllocatorManager.Block& block)
END_CLASS

CLASS: Try_000000A0$BurstDirectCall
TYPE:  static class
TOKEN: 0x2000023
SIZE:  0x10
FIELDS:
  private   static  System.IntPtr                   Pointer  // static @ 0x0
  private   static  System.IntPtr                   DeferredCompilation  // static @ 0x8
METHODS:
  RVA=0x0B163EFC  token=0x600005C  System.Void GetFunctionPointerDiscard(System.IntPtr& )
  RVA=0x0B164000  token=0x600005D  System.IntPtr GetFunctionPointer()
  RVA=0x03FB0750  token=0x600005E  System.Void Constructor()
  RVA=0x041E1670  token=0x600005F  System.Void Initialize()
  RVA=0x03FAEA50  token=0x6000060  System.Void .cctor()
  RVA=0x0B164054  token=0x6000061  System.Int32 Invoke(System.IntPtr allocatorState, Unity.Collections.AllocatorManager.Block& block)
END_CLASS

CLASS: StackAllocator
TYPE:  sealed struct
TOKEN: 0x2000021
SIZE:  0x40
IMPLEMENTS: IAllocator System.IDisposable
FIELDS:
  private           Unity.Collections.AllocatorManager.AllocatorHandlem_handle  // 0x10
  private           Unity.Collections.AllocatorManager.Blockm_storage  // 0x18
  private           System.Int64                    m_top  // 0x38
PROPERTIES:
  Handle  get=0x02B77620
METHODS:
  RVA=0x0B163D48  token=0x6000056  System.Int32 Try(Unity.Collections.AllocatorManager.Block& block)
  RVA=0x0B163CEC  token=0x6000057  System.Int32 Try(System.IntPtr allocatorState, Unity.Collections.AllocatorManager.Block& block)
  RVA=0x041E1670  token=0x6000058  System.Void Dispose()
  RVA=0x0B163CE4  token=0x6000059  System.Int32 Try$BurstManaged(System.IntPtr allocatorState, Unity.Collections.AllocatorManager.Block& block)
END_CLASS

CLASS: Try_000000AE$PostfixBurstDelegate
TYPE:  sealed class
TOKEN: 0x2000025
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B163E34  token=0x6000068  System.Void .ctor(System.Object , System.IntPtr )
  RVA=0x015802E0  token=0x6000069  System.Int32 Invoke(System.IntPtr allocatorState, Unity.Collections.AllocatorManager.Block& block)
END_CLASS

CLASS: Try_000000AE$BurstDirectCall
TYPE:  static class
TOKEN: 0x2000026
SIZE:  0x10
FIELDS:
  private   static  System.IntPtr                   Pointer  // static @ 0x0
  private   static  System.IntPtr                   DeferredCompilation  // static @ 0x8
METHODS:
  RVA=0x0B1640E0  token=0x600006A  System.Void GetFunctionPointerDiscard(System.IntPtr& )
  RVA=0x0B1641E4  token=0x600006B  System.IntPtr GetFunctionPointer()
  RVA=0x03FB07E0  token=0x600006C  System.Void Constructor()
  RVA=0x041E1670  token=0x600006D  System.Void Initialize()
  RVA=0x03FAEA60  token=0x600006E  System.Void .cctor()
  RVA=0x0B164238  token=0x600006F  System.Int32 Invoke(System.IntPtr allocatorState, Unity.Collections.AllocatorManager.Block& block)
END_CLASS

CLASS: SlabAllocator
TYPE:  sealed struct
TOKEN: 0x2000024
SIZE:  0x1050
IMPLEMENTS: IAllocator System.IDisposable
FIELDS:
  private           Unity.Collections.AllocatorManager.AllocatorHandlem_handle  // 0x10
  private           Unity.Collections.AllocatorManager.BlockStorage  // 0x18
  private           System.Int32                    Log2SlabSizeInBytes  // 0x38
  private           Unity.Collections.FixedList4096Bytes<System.Int32>Occupied  // 0x3c
  private           System.Int64                    budgetInBytes  // 0x1040
  private           System.Int64                    allocatedBytes  // 0x1048
PROPERTIES:
  Handle  get=0x02B77620
  SlabSizeInBytes  get=0x04DAB540
METHODS:
  RVA=0x0B163AC8  token=0x6000064  System.Int32 Try(Unity.Collections.AllocatorManager.Block& block)
  RVA=0x0B163A6C  token=0x6000065  System.Int32 Try(System.IntPtr allocatorState, Unity.Collections.AllocatorManager.Block& block)
  RVA=0x041E1670  token=0x6000066  System.Void Dispose()
  RVA=0x0B163A64  token=0x6000067  System.Int32 Try$BurstManaged(System.IntPtr allocatorState, Unity.Collections.AllocatorManager.Block& block)
END_CLASS

CLASS: TableEntry
TYPE:  sealed struct
TOKEN: 0x2000027
SIZE:  0x20
FIELDS:
  private           System.IntPtr                   function  // 0x10
  private           System.IntPtr                   state  // 0x18
METHODS:
END_CLASS

CLASS: Array16`1
TYPE:  sealed struct
TOKEN: 0x2000028
FIELDS:
  private           T                               f0  // 0x0
  private           T                               f1  // 0x0
  private           T                               f2  // 0x0
  private           T                               f3  // 0x0
  private           T                               f4  // 0x0
  private           T                               f5  // 0x0
  private           T                               f6  // 0x0
  private           T                               f7  // 0x0
  private           T                               f8  // 0x0
  private           T                               f9  // 0x0
  private           T                               f10  // 0x0
  private           T                               f11  // 0x0
  private           T                               f12  // 0x0
  private           T                               f13  // 0x0
  private           T                               f14  // 0x0
  private           T                               f15  // 0x0
METHODS:
END_CLASS

CLASS: Array256`1
TYPE:  sealed struct
TOKEN: 0x2000029
FIELDS:
  private           Unity.Collections.AllocatorManager.Array16<T>f0  // 0x0
  private           Unity.Collections.AllocatorManager.Array16<T>f1  // 0x0
  private           Unity.Collections.AllocatorManager.Array16<T>f2  // 0x0
  private           Unity.Collections.AllocatorManager.Array16<T>f3  // 0x0
  private           Unity.Collections.AllocatorManager.Array16<T>f4  // 0x0
  private           Unity.Collections.AllocatorManager.Array16<T>f5  // 0x0
  private           Unity.Collections.AllocatorManager.Array16<T>f6  // 0x0
  private           Unity.Collections.AllocatorManager.Array16<T>f7  // 0x0
  private           Unity.Collections.AllocatorManager.Array16<T>f8  // 0x0
  private           Unity.Collections.AllocatorManager.Array16<T>f9  // 0x0
  private           Unity.Collections.AllocatorManager.Array16<T>f10  // 0x0
  private           Unity.Collections.AllocatorManager.Array16<T>f11  // 0x0
  private           Unity.Collections.AllocatorManager.Array16<T>f12  // 0x0
  private           Unity.Collections.AllocatorManager.Array16<T>f13  // 0x0
  private           Unity.Collections.AllocatorManager.Array16<T>f14  // 0x0
  private           Unity.Collections.AllocatorManager.Array16<T>f15  // 0x0
METHODS:
END_CLASS

CLASS: Array4096`1
TYPE:  sealed struct
TOKEN: 0x200002A
FIELDS:
  private           Unity.Collections.AllocatorManager.Array256<T>f0  // 0x0
  private           Unity.Collections.AllocatorManager.Array256<T>f1  // 0x0
  private           Unity.Collections.AllocatorManager.Array256<T>f2  // 0x0
  private           Unity.Collections.AllocatorManager.Array256<T>f3  // 0x0
  private           Unity.Collections.AllocatorManager.Array256<T>f4  // 0x0
  private           Unity.Collections.AllocatorManager.Array256<T>f5  // 0x0
  private           Unity.Collections.AllocatorManager.Array256<T>f6  // 0x0
  private           Unity.Collections.AllocatorManager.Array256<T>f7  // 0x0
  private           Unity.Collections.AllocatorManager.Array256<T>f8  // 0x0
  private           Unity.Collections.AllocatorManager.Array256<T>f9  // 0x0
  private           Unity.Collections.AllocatorManager.Array256<T>f10  // 0x0
  private           Unity.Collections.AllocatorManager.Array256<T>f11  // 0x0
  private           Unity.Collections.AllocatorManager.Array256<T>f12  // 0x0
  private           Unity.Collections.AllocatorManager.Array256<T>f13  // 0x0
  private           Unity.Collections.AllocatorManager.Array256<T>f14  // 0x0
  private           Unity.Collections.AllocatorManager.Array256<T>f15  // 0x0
METHODS:
END_CLASS

CLASS: Array32768`1
TYPE:  sealed struct
TOKEN: 0x200002B
IMPLEMENTS: Unity.Collections.IIndexable`1
FIELDS:
  private           Unity.Collections.AllocatorManager.Array4096<T>f0  // 0x0
  private           Unity.Collections.AllocatorManager.Array4096<T>f1  // 0x0
  private           Unity.Collections.AllocatorManager.Array4096<T>f2  // 0x0
  private           Unity.Collections.AllocatorManager.Array4096<T>f3  // 0x0
  private           Unity.Collections.AllocatorManager.Array4096<T>f4  // 0x0
  private           Unity.Collections.AllocatorManager.Array4096<T>f5  // 0x0
  private           Unity.Collections.AllocatorManager.Array4096<T>f6  // 0x0
  private           Unity.Collections.AllocatorManager.Array4096<T>f7  // 0x0
PROPERTIES:
  Length  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000071  T& ElementAt(System.Int32 index)
END_CLASS

CLASS: TableEntry
TYPE:  sealed class
TOKEN: 0x200002D
SIZE:  0x10
FIELDS:
  private   static readonly Unity.Burst.SharedStatic<Unity.Collections.AllocatorManager.Array32768<Unity.Collections.AllocatorManager.TableEntry>>Ref  // static @ 0x0
METHODS:
  RVA=0x0B163DD4  token=0x6000072  System.Void .cctor()
END_CLASS

CLASS: SharedStatics
TYPE:  sealed class
TOKEN: 0x200002C
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: Managed
TYPE:  static class
TOKEN: 0x200002E
SIZE:  0x10
FIELDS:
  private   static  Unity.Collections.AllocatorManager.TryFunction[]TryFunctionDelegates  // static @ 0x0
METHODS:
  RVA=0x0B1632EC  token=0x6000073  System.Void .cctor()
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x2000044
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           Unity.Collections.FixedList512Bytes<T>m_List  // 0x0
  private           System.Int32                    m_Index  // 0x0
PROPERTIES:
  Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600010C  System.Void .ctor(Unity.Collections.FixedList512Bytes<T>& list)
  RVA=-1  // not resolved  token=0x600010D  System.Void Dispose()
  RVA=-1  // not resolved  token=0x600010E  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x600010F  System.Void Reset()
END_CLASS

CLASS: Array
TYPE:  sealed struct
TOKEN: 0x200005A
SIZE:  0x11
FIELDS:
METHODS:
  RVA=0x03347DF0  token=0x60001C4  System.Boolean IsCustom(Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=0x0B15FEF8  token=0x60001C5  System.Void* CustomResize(System.Void* oldPointer, System.Int64 oldCount, System.Int64 newCount, Unity.Collections.AllocatorManager.AllocatorHandle allocator, System.Int64 size, System.Int32 align)
  RVA=0x03347CE0  token=0x60001C6  System.Void* Resize(System.Void* oldPointer, System.Int64 oldCount, System.Int64 newCount, Unity.Collections.AllocatorManager.AllocatorHandle allocator, System.Int64 size, System.Int32 align)
  RVA=-1  // generic def  token=0x60001C7  T* Resize(T* oldPointer, System.Int64 oldCount, System.Int64 newCount, Unity.Collections.AllocatorManager.AllocatorHandle allocator)
END_CLASS

CLASS: Unmanaged
TYPE:  sealed struct
TOKEN: 0x2000059
SIZE:  0x11
FIELDS:
METHODS:
  RVA=0x03347C10  token=0x60001C1  System.Void* Allocate(System.Int64 size, System.Int32 align, Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=0x03CCB260  token=0x60001C2  System.Void Free(System.Void* pointer, Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // generic def  token=0x60001C3  System.Void Free(T* pointer, Unity.Collections.AllocatorManager.AllocatorHandle allocator)
END_CLASS

CLASS: ParallelWriter
TYPE:  sealed struct
TOKEN: 0x200005F
FIELDS:
  public            Unity.Collections.LowLevel.Unsafe.UnsafeList<T>*ListData  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001F4  System.Void .ctor(Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* listData)
  RVA=-1  // not resolved  token=0x60001F5  System.Void AddNoResize(T value)
END_CLASS

CLASS: ParallelWriter
TYPE:  sealed struct
TOKEN: 0x2000064
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap.ParallelWriter<TKey,TValue>m_Writer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600020F  System.Boolean TryAdd(TKey key, TValue item)
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x2000065
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataEnumeratorm_Enumerator  // 0x0
PROPERTIES:
  Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000210  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000211  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000212  System.Void Reset()
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x2000069
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataEnumeratorm_Enumerator  // 0x0
PROPERTIES:
  Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000223  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000224  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000225  System.Void Reset()
END_CLASS

CLASS: ParallelWriter
TYPE:  sealed struct
TOKEN: 0x200006D
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap.ParallelWriter<TKey,TValue>m_Writer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600023F  System.Void Add(TKey key, TValue item)
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x200006E
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           Unity.Collections.NativeParallelMultiHashMap<TKey,TValue>hashmap  // 0x0
  private           TKey                            key  // 0x0
  private           System.Boolean                  isFirst  // 0x0
  private           TValue                          value  // 0x0
  private           Unity.Collections.NativeParallelMultiHashMapIterator<TKey>iterator  // 0x0
PROPERTIES:
  Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000240  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000241  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000242  System.Void Reset()
  RVA=-1  // not resolved  token=0x6000245  Unity.Collections.NativeParallelMultiHashMap.Enumerator<TKey,TValue> GetEnumerator()
END_CLASS

CLASS: ParallelWriter
TYPE:  sealed struct
TOKEN: 0x2000075
FIELDS:
  private           Unity.Collections.NativeQueueData*m_Buffer  // 0x0
  private           Unity.Collections.NativeQueueBlockPoolData*m_QueuePool  // 0x0
  private           System.Int32                    m_ThreadIndex  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600025C  System.Void Enqueue(T value)
END_CLASS

CLASS: DefaultComparer`1
TYPE:  sealed struct
TOKEN: 0x200007C
SIZE:  0x11
IMPLEMENTS: System.Collections.Generic.IComparer`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600027B  System.Int32 Compare(T x, T y)
END_CLASS

CLASS: SegmentSort
TYPE:  sealed struct
TOKEN: 0x200007E
IMPLEMENTS: Unity.Jobs.IJobParallelFor
FIELDS:
  public            T*                              Data  // 0x0
  public            U                               Comp  // 0x0
  public            System.Int32                    Length  // 0x0
  public            System.Int32                    SegmentWidth  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600027D  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: SegmentSortMerge
TYPE:  sealed struct
TOKEN: 0x200007F
IMPLEMENTS: Unity.Jobs.IJob
FIELDS:
  public            T*                              Data  // 0x0
  public            U                               Comp  // 0x0
  public            System.Int32                    Length  // 0x0
  public            System.Int32                    SegmentWidth  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600027E  System.Void Execute()
END_CLASS

CLASS: ConstructJobList
TYPE:  sealed struct
TOKEN: 0x2000081
SIZE:  0x28
IMPLEMENTS: Unity.Jobs.IJob
FIELDS:
  public            Unity.Collections.NativeStream  Container  // 0x10
  public            Unity.Collections.LowLevel.Unsafe.UntypedUnsafeList*List  // 0x20
METHODS:
  RVA=0x0B160024  token=0x6000281  System.Void Execute()
END_CLASS

CLASS: ConstructJob
TYPE:  sealed struct
TOKEN: 0x2000082
SIZE:  0x30
IMPLEMENTS: Unity.Jobs.IJob
FIELDS:
  public            Unity.Collections.NativeStream  Container  // 0x10
  public            Unity.Collections.NativeArray<System.Int32>Length  // 0x20
METHODS:
  RVA=0x0B160048  token=0x6000282  System.Void Execute()
END_CLASS

CLASS: MemoryBlock
TYPE:  sealed struct
TOKEN: 0x2000086
SIZE:  0x30
IMPLEMENTS: System.IDisposable
FIELDS:
  public            System.Byte*                    m_pointer  // 0x10
  public            System.Int64                    m_bytes  // 0x18
  public            System.Int64                    m_current  // 0x20
  public            System.Int64                    m_allocations  // 0x28
METHODS:
  RVA=0x0B1634A8  token=0x600028D  System.Void .ctor(System.Int64 bytes)
  RVA=0x04D92200  token=0x600028E  System.Void Rewind()
  RVA=0x0B16336C  token=0x600028F  System.Void Dispose()
  RVA=0x0B1633CC  token=0x6000290  System.Int32 TryAllocate(Unity.Collections.AllocatorManager.Block& block)
  RVA=0x0B163354  token=0x6000291  System.Boolean Contains(System.IntPtr ptr)
END_CLASS

CLASS: Rune
TYPE:  sealed struct
TOKEN: 0x200008D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value  // 0x10
METHODS:
END_CLASS

CLASS: Comparison
TYPE:  sealed struct
TOKEN: 0x200008F
SIZE:  0x18
FIELDS:
  public            System.Boolean                  terminates  // 0x10
  public            System.Int32                    result  // 0x14
METHODS:
  RVA=0x0B15FFF4  token=0x60002AA  System.Void .ctor(Unity.Collections.Unicode.Rune runeA, Unity.Collections.ConversionError errorA, Unity.Collections.Unicode.Rune runeB, Unity.Collections.ConversionError errorB)
END_CLASS

CLASS: ulong2
TYPE:  sealed struct
TOKEN: 0x2000091
SIZE:  0x20
FIELDS:
  public            System.UInt64                   x  // 0x10
  public            System.UInt64                   y  // 0x18
METHODS:
  RVA=0x04D861B0  token=0x60002DB  System.Void .ctor(System.UInt64 x, System.UInt64 y)
END_CLASS

CLASS: ParallelReader
TYPE:  sealed struct
TOKEN: 0x200009E
FIELDS:
  public    readonly T*                              Ptr  // 0x0
  public    readonly System.Int32                    Length  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000314  System.Void .ctor(T* ptr, System.Int32 length)
END_CLASS

CLASS: ParallelWriter
TYPE:  sealed struct
TOKEN: 0x200009F
FIELDS:
  public            Unity.Collections.LowLevel.Unsafe.UnsafeList<T>*ListData  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000315  System.Void .ctor(Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* listData)
  RVA=-1  // not resolved  token=0x6000316  System.Void AddNoResize(T value)
END_CLASS

CLASS: <firstFreeTLS>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x20000A2
SIZE:  0x2010
FIELDS:
  public            System.Int32                    FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: ParallelWriter
TYPE:  sealed struct
TOKEN: 0x20000A9
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData*m_Buffer  // 0x0
  private           System.Int32                    m_ThreadIndex  // 0x0
PROPERTIES:
  Capacity  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000349  System.Boolean TryAdd(TKey key, TValue item)
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x20000AD
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataEnumeratorm_Enumerator  // 0x0
PROPERTIES:
  Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000354  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000355  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000356  System.Void Reset()
END_CLASS

CLASS: ParallelWriter
TYPE:  sealed struct
TOKEN: 0x20000B1
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData*m_Buffer  // 0x0
  private           System.Int32                    m_ThreadIndex  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600036C  System.Void Add(TKey key, TValue item)
END_CLASS

CLASS: <Data>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x20000B4
SIZE:  0x11
FIELDS:
  public            System.Byte                     FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: DisposeJob
TYPE:  sealed struct
TOKEN: 0x20000B8
SIZE:  0x20
IMPLEMENTS: Unity.Jobs.IJob
FIELDS:
  public            Unity.Collections.LowLevel.Unsafe.UnsafeStreamContainer  // 0x10
METHODS:
  RVA=0x0B160058  token=0x6000370  System.Void Execute()
END_CLASS

CLASS: ConstructJobList
TYPE:  sealed struct
TOKEN: 0x20000B9
SIZE:  0x28
IMPLEMENTS: Unity.Jobs.IJob
FIELDS:
  public            Unity.Collections.LowLevel.Unsafe.UnsafeStreamContainer  // 0x10
  public            Unity.Collections.LowLevel.Unsafe.UntypedUnsafeList*List  // 0x20
METHODS:
  RVA=0x0B160024  token=0x6000371  System.Void Execute()
END_CLASS

CLASS: ConstructJob
TYPE:  sealed struct
TOKEN: 0x20000BA
SIZE:  0x30
IMPLEMENTS: Unity.Jobs.IJob
FIELDS:
  public            Unity.Collections.LowLevel.Unsafe.UnsafeStreamContainer  // 0x10
  public            Unity.Collections.NativeArray<System.Int32>Length  // 0x20
METHODS:
  RVA=0x0B160048  token=0x6000372  System.Void Execute()
END_CLASS

CLASS: __StaticArrayInitTypeSize=192
TYPE:  sealed struct
TOKEN: 0x20000BD
SIZE:  0xD0
FIELDS:
METHODS:
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x20000BC
SIZE:  0x10
FIELDS:
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=1922CF2F88BF9B71283059B6DF53E5BCDE20ADBFD9E8D6CE2C1AB106262BB283BED  // static @ 0x0
METHODS:
END_CLASS

CLASS: __JobReflectionRegistrationOutput__486165037
TYPE:  class
TOKEN: 0x20000BE
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x02EC09F0  token=0x6000376  System.Void CreateJobReflectionData()
  RVA=0x02EC2B20  token=0x6000377  System.Void EarlyInit()
END_CLASS

CLASS: $BurstDirectCallInitializer
TYPE:  static class
TOKEN: 0x20000BF
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04CAF720  token=0x6000378  System.Void Initialize()
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

CLASS: Unity.Collections.AllocatorManager
TYPE:  static class
TOKEN: 0x200001B
SIZE:  0x10
FIELDS:
  public    static readonly Unity.Collections.AllocatorManager.AllocatorHandleInvalid  // static @ 0x0
  public    static readonly Unity.Collections.AllocatorManager.AllocatorHandleNone  // static @ 0x4
  public    static readonly Unity.Collections.AllocatorManager.AllocatorHandleTemp  // static @ 0x8
  public    static readonly Unity.Collections.AllocatorManager.AllocatorHandleTempJob  // static @ 0xc
  public    static readonly Unity.Collections.AllocatorManager.AllocatorHandlePersistent  // static @ 0x10
  public    static readonly Unity.Collections.AllocatorManager.AllocatorHandleAudioKernel  // static @ 0x14
METHODS:
  RVA=-1  // generic def  token=0x600002C  Unity.Collections.AllocatorManager.Block AllocateBlock(T& t, System.Int32 sizeOf, System.Int32 alignOf, System.Int32 items)
  RVA=-1  // generic def  token=0x600002D  System.Void* Allocate(T& t, System.Int32 sizeOf, System.Int32 alignOf, System.Int32 items)
  RVA=-1  // generic def  token=0x600002E  U* Allocate(T& t, U u, System.Int32 items)
  RVA=-1  // generic def  token=0x600002F  System.Void* AllocateStruct(T& t, U u, System.Int32 items)
  RVA=-1  // generic def  token=0x6000030  System.Void FreeBlock(T& t, Unity.Collections.AllocatorManager.Block& block)
  RVA=-1  // generic def  token=0x6000031  System.Void Free(T& t, System.Void* pointer, System.Int32 sizeOf, System.Int32 alignOf, System.Int32 items)
  RVA=-1  // generic def  token=0x6000032  System.Void Free(T& t, U* pointer, System.Int32 items)
  RVA=0x03345120  token=0x6000033  System.Void* Allocate(Unity.Collections.AllocatorManager.AllocatorHandle handle, System.Int32 itemSizeInBytes, System.Int32 alignmentInBytes, System.Int32 items)
  RVA=-1  // generic def  token=0x6000034  T* Allocate(Unity.Collections.AllocatorManager.AllocatorHandle handle, System.Int32 items)
  RVA=0x0B15FCC4  token=0x6000035  System.Void Free(Unity.Collections.AllocatorManager.AllocatorHandle handle, System.Void* pointer)
  RVA=-1  // generic def  token=0x6000036  System.Void Free(Unity.Collections.AllocatorManager.AllocatorHandle handle, T* pointer, System.Int32 items)
  RVA=0x04D910F0  token=0x6000037  System.Void CheckDelegate(System.Boolean& useDelegate)
  RVA=0x0B15FD24  token=0x6000038  System.Boolean UseDelegate()
  RVA=0x0B15FD60  token=0x6000039  System.Int32 allocate_block(Unity.Collections.AllocatorManager.Block& block)
  RVA=0x0B15FDF0  token=0x600003A  System.Void forward_mono_allocate_block(Unity.Collections.AllocatorManager.Block& block, System.Int32& error)
  RVA=0x03347C00  token=0x600003B  Unity.Collections.Allocator LegacyOf(Unity.Collections.AllocatorManager.AllocatorHandle handle)
  RVA=0x03347AA0  token=0x600003C  System.Int32 TryLegacy(Unity.Collections.AllocatorManager.Block& block)
  RVA=0x03347A60  token=0x600003D  System.Int32 Try(Unity.Collections.AllocatorManager.Block& block)
  RVA=0x03347DF0  token=0x600003E  System.Boolean IsCustomAllocator(Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=0x04CA1340  token=0x600003F  System.Void .cctor()
  RVA=0x04CAF810  token=0x6000040  System.Void Initialize$StackAllocator_Try_000000A0$BurstDirectCall()
  RVA=0x04CAF7D0  token=0x6000041  System.Void Initialize$SlabAllocator_Try_000000AE$BurstDirectCall()
END_CLASS

CLASS: Unity.Collections.CreatePropertyAttribute
TYPE:  class
TOKEN: 0x200002F
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000074  System.Void .ctor()
END_CLASS

CLASS: Unity.Collections.Bitwise
TYPE:  sealed struct
TOKEN: 0x2000030
SIZE:  0x11
FIELDS:
METHODS:
  RVA=0x038C4530  token=0x6000075  System.Int32 FromBool(System.Boolean value)
  RVA=0x04DAB470  token=0x6000076  System.UInt32 ExtractBits(System.UInt32 input, System.Int32 pos, System.UInt32 mask)
  RVA=0x04DAB4B0  token=0x6000077  System.UInt32 ReplaceBits(System.UInt32 input, System.Int32 pos, System.UInt32 mask, System.UInt32 value)
  RVA=0x038C44E0  token=0x6000078  System.UInt32 SetBits(System.UInt32 input, System.Int32 pos, System.UInt32 mask, System.Boolean value)
  RVA=0x04DAB460  token=0x6000079  System.UInt64 ExtractBits(System.UInt64 input, System.Int32 pos, System.UInt64 mask)
  RVA=0x04DAB480  token=0x600007A  System.UInt64 ReplaceBits(System.UInt64 input, System.Int32 pos, System.UInt64 mask, System.UInt64 value)
  RVA=0x038C4500  token=0x600007B  System.UInt64 SetBits(System.UInt64 input, System.Int32 pos, System.UInt64 mask, System.Boolean value)
END_CLASS

CLASS: Unity.Collections.BitField32
TYPE:  sealed struct
TOKEN: 0x2000031
SIZE:  0x14
FIELDS:
  public            System.UInt32                   Value  // 0x10
METHODS:
  RVA=0x05390AE8  token=0x600007C  System.Void SetBits(System.Int32 pos, System.Boolean value)
  RVA=0x04DAB410  token=0x600007D  System.UInt32 GetBits(System.Int32 pos, System.Int32 numBits)
  RVA=0x02F95910  token=0x600007E  System.Boolean IsSet(System.Int32 pos)
END_CLASS

CLASS: Unity.Collections.BitField32DebugView
TYPE:  sealed class
TOKEN: 0x2000032
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Collections.BitField64
TYPE:  sealed struct
TOKEN: 0x2000033
SIZE:  0x18
FIELDS:
  public            System.UInt64                   Value  // 0x10
METHODS:
  RVA=0x05390AB4  token=0x600007F  System.Void SetBits(System.Int32 pos, System.Boolean value)
  RVA=0x04DAB430  token=0x6000080  System.UInt64 GetBits(System.Int32 pos, System.Int32 numBits)
  RVA=0x037EBDF0  token=0x6000081  System.Boolean IsSet(System.Int32 pos)
  RVA=0x037EDE40  token=0x6000082  System.Boolean TestAny(System.Int32 pos, System.Int32 numBits)
END_CLASS

CLASS: Unity.Collections.BitField64DebugView
TYPE:  sealed class
TOKEN: 0x2000034
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Collections.BurstCompatibleAttribute
TYPE:  class
TOKEN: 0x2000035
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  private           System.Type[]                   <GenericTypeArguments>k__BackingField  // 0x10
  public            System.String                   RequiredUnityDefine  // 0x18
PROPERTIES:
  GenericTypeArguments  set=0x053908C0
METHODS:
  RVA=0x041E1670  token=0x6000084  System.Void .ctor()
END_CLASS

CLASS: Unity.Collections.NotBurstCompatibleAttribute
TYPE:  class
TOKEN: 0x2000036
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000085  System.Void .ctor()
END_CLASS

CLASS: Unity.Collections.INativeDisposable
TYPE:  interface
TOKEN: 0x2000037
IMPLEMENTS: System.IDisposable
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Collections.CollectionHelper
TYPE:  static class
TOKEN: 0x2000038
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x05F0E0A0  token=0x6000086  System.Int32 Log2Floor(System.Int32 value)
  RVA=0x037F5AD0  token=0x6000087  System.Int32 Align(System.Int32 size, System.Int32 alignmentPowerOfTwo)
  RVA=0x043CED80  token=0x6000088  System.UInt32 Hash(System.Void* ptr, System.Int32 bytes)
  RVA=0x03CCB240  token=0x6000089  System.Boolean ShouldDeallocate(Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=0x04D88BE0  token=0x600008A  System.Int32 AssumePositive(System.Int32 value)
  RVA=-1  // generic def  token=0x600008B  Unity.Collections.NativeArray<T> CreateNativeArray(System.Int32 length, Unity.Collections.AllocatorManager.AllocatorHandle allocator, Unity.Collections.NativeArrayOptions options)
  RVA=-1  // generic def  token=0x600008C  Unity.Collections.NativeArray<T> CreateNativeArray(T[] array, Unity.Collections.AllocatorManager.AllocatorHandle allocator)
END_CLASS

CLASS: Unity.Collections.FixedList
TYPE:  sealed struct
TOKEN: 0x2000039
SIZE:  0x11
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x600008D  System.Int32 PaddingBytes()
  RVA=-1  // generic def  token=0x600008E  System.Int32 StorageBytes()
  RVA=-1  // generic def  token=0x600008F  System.Int32 Capacity()
END_CLASS

CLASS: Unity.Collections.FixedList32Bytes`1
TYPE:  sealed struct
TOKEN: 0x200003A
IMPLEMENTS: Unity.Collections.INativeList`1 Unity.Collections.IIndexable`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1
FIELDS:
  private           System.UInt16                   length  // 0x0
  private           Unity.Collections.FixedBytes30  buffer  // 0x0
PROPERTIES:
  Length  get=-1  // not resolved  set=-1  // not resolved
  LengthInBytes  get=-1  // not resolved
  Buffer  get=-1  // not resolved
  Capacity  get=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000097  T& ElementAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000098  System.Int32 GetHashCode()
  RVA=-1  // not resolved  token=0x6000099  System.Void Add(T& item)
  RVA=-1  // not resolved  token=0x600009A  System.Void RemoveAtSwapBack(System.Int32 index)
  RVA=-1  // not resolved  token=0x600009B  System.Void RemoveRangeSwapBack(System.Int32 index, System.Int32 count)
  RVA=-1  // not resolved  token=0x600009C  System.Void RemoveAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x600009D  System.Void RemoveRange(System.Int32 index, System.Int32 count)
  RVA=-1  // not resolved  token=0x600009E  System.Int32 CompareTo(Unity.Collections.FixedList32Bytes<T> other)
  RVA=-1  // not resolved  token=0x600009F  System.Boolean Equals(Unity.Collections.FixedList32Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000A0  System.Int32 CompareTo(Unity.Collections.FixedList64Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000A1  System.Boolean Equals(Unity.Collections.FixedList64Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000A2  System.Int32 CompareTo(Unity.Collections.FixedList128Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000A3  System.Boolean Equals(Unity.Collections.FixedList128Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000A4  System.Int32 CompareTo(Unity.Collections.FixedList512Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000A5  System.Boolean Equals(Unity.Collections.FixedList512Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000A6  System.Int32 CompareTo(Unity.Collections.FixedList4096Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000A7  System.Boolean Equals(Unity.Collections.FixedList4096Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000A8  System.Boolean Equals(System.Object obj)
  RVA=-1  // not resolved  token=0x60000A9  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x60000AA  System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
END_CLASS

CLASS: Unity.Collections.FixedList32BytesExtensions
TYPE:  static class
TOKEN: 0x200003B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60000AB  System.Int32 IndexOf(Unity.Collections.FixedList32Bytes<T>& list, U value)
  RVA=-1  // generic def  token=0x60000AC  System.Boolean Contains(Unity.Collections.FixedList32Bytes<T>& list, U value)
END_CLASS

CLASS: Unity.Collections.FixedList32BytesDebugView`1
TYPE:  sealed class
TOKEN: 0x200003C
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Collections.FixedList64Bytes`1
TYPE:  sealed struct
TOKEN: 0x200003D
IMPLEMENTS: Unity.Collections.INativeList`1 Unity.Collections.IIndexable`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1
FIELDS:
  private           System.UInt16                   length  // 0x0
  private           Unity.Collections.FixedBytes62  buffer  // 0x0
PROPERTIES:
  Length  get=-1  // not resolved  set=-1  // not resolved
  LengthInBytes  get=-1  // not resolved
  Buffer  get=-1  // not resolved
  Capacity  get=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000B4  T& ElementAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x60000B5  System.Int32 GetHashCode()
  RVA=-1  // not resolved  token=0x60000B6  System.Void Add(T& item)
  RVA=-1  // not resolved  token=0x60000B7  System.Void RemoveAtSwapBack(System.Int32 index)
  RVA=-1  // not resolved  token=0x60000B8  System.Void RemoveRangeSwapBack(System.Int32 index, System.Int32 count)
  RVA=-1  // not resolved  token=0x60000B9  System.Void RemoveAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x60000BA  System.Void RemoveRange(System.Int32 index, System.Int32 count)
  RVA=-1  // not resolved  token=0x60000BB  System.Int32 CompareTo(Unity.Collections.FixedList32Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000BC  System.Boolean Equals(Unity.Collections.FixedList32Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000BD  System.Int32 CompareTo(Unity.Collections.FixedList64Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000BE  System.Boolean Equals(Unity.Collections.FixedList64Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000BF  System.Int32 CompareTo(Unity.Collections.FixedList128Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000C0  System.Boolean Equals(Unity.Collections.FixedList128Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000C1  System.Int32 CompareTo(Unity.Collections.FixedList512Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000C2  System.Boolean Equals(Unity.Collections.FixedList512Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000C3  System.Int32 CompareTo(Unity.Collections.FixedList4096Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000C4  System.Boolean Equals(Unity.Collections.FixedList4096Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000C5  System.Boolean Equals(System.Object obj)
  RVA=-1  // not resolved  token=0x60000C6  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x60000C7  System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
END_CLASS

CLASS: Unity.Collections.FixedList64BytesExtensions
TYPE:  static class
TOKEN: 0x200003E
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60000C8  System.Int32 IndexOf(Unity.Collections.FixedList64Bytes<T>& list, U value)
  RVA=-1  // generic def  token=0x60000C9  System.Boolean Contains(Unity.Collections.FixedList64Bytes<T>& list, U value)
END_CLASS

CLASS: Unity.Collections.FixedList64BytesDebugView`1
TYPE:  sealed class
TOKEN: 0x200003F
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Collections.FixedList128Bytes`1
TYPE:  sealed struct
TOKEN: 0x2000040
IMPLEMENTS: Unity.Collections.INativeList`1 Unity.Collections.IIndexable`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1
FIELDS:
  private           System.UInt16                   length  // 0x0
  private           Unity.Collections.FixedBytes126 buffer  // 0x0
PROPERTIES:
  Length  get=-1  // not resolved  set=-1  // not resolved
  IsEmpty  get=-1  // not resolved
  LengthInBytes  get=-1  // not resolved
  Buffer  get=-1  // not resolved
  Capacity  get=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000D2  T& ElementAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x60000D3  System.Int32 GetHashCode()
  RVA=-1  // not resolved  token=0x60000D4  System.Void Add(T& item)
  RVA=-1  // not resolved  token=0x60000D5  System.Void AddNoResize(T& item)
  RVA=-1  // not resolved  token=0x60000D6  System.Void Clear()
  RVA=-1  // not resolved  token=0x60000D7  System.Void RemoveAtSwapBack(System.Int32 index)
  RVA=-1  // not resolved  token=0x60000D8  System.Void RemoveRangeSwapBack(System.Int32 index, System.Int32 count)
  RVA=-1  // not resolved  token=0x60000D9  System.Void RemoveAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x60000DA  System.Void RemoveRange(System.Int32 index, System.Int32 count)
  RVA=-1  // not resolved  token=0x60000DB  System.Int32 CompareTo(Unity.Collections.FixedList32Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000DC  System.Boolean Equals(Unity.Collections.FixedList32Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000DD  System.Int32 CompareTo(Unity.Collections.FixedList64Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000DE  System.Boolean Equals(Unity.Collections.FixedList64Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000DF  System.Int32 CompareTo(Unity.Collections.FixedList128Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000E0  System.Boolean Equals(Unity.Collections.FixedList128Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000E1  System.Int32 CompareTo(Unity.Collections.FixedList512Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000E2  System.Boolean Equals(Unity.Collections.FixedList512Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000E3  System.Int32 CompareTo(Unity.Collections.FixedList4096Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000E4  System.Boolean Equals(Unity.Collections.FixedList4096Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000E5  System.Boolean Equals(System.Object obj)
  RVA=-1  // not resolved  token=0x60000E6  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x60000E7  System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
END_CLASS

CLASS: Unity.Collections.FixedList128BytesExtensions
TYPE:  static class
TOKEN: 0x2000041
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60000E8  System.Int32 IndexOf(Unity.Collections.FixedList128Bytes<T>& list, U value)
  RVA=-1  // generic def  token=0x60000E9  System.Boolean Contains(Unity.Collections.FixedList128Bytes<T>& list, U value)
  RVA=-1  // generic def  token=0x60000EA  System.Boolean Remove(Unity.Collections.FixedList128Bytes<T>& list, U value)
END_CLASS

CLASS: Unity.Collections.FixedList128BytesDebugView`1
TYPE:  sealed class
TOKEN: 0x2000042
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Collections.FixedList512Bytes`1
TYPE:  sealed struct
TOKEN: 0x2000043
IMPLEMENTS: Unity.Collections.INativeList`1 Unity.Collections.IIndexable`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1
FIELDS:
  private           System.UInt16                   length  // 0x0
  private           Unity.Collections.FixedBytes510 buffer  // 0x0
PROPERTIES:
  Length  get=-1  // not resolved  set=-1  // not resolved
  IsEmpty  get=-1  // not resolved
  LengthInBytes  get=-1  // not resolved
  Buffer  get=-1  // not resolved
  Capacity  get=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000F3  T& ElementAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x60000F4  System.Int32 GetHashCode()
  RVA=-1  // not resolved  token=0x60000F5  System.Void Add(T& item)
  RVA=-1  // not resolved  token=0x60000F6  System.Void Clear()
  RVA=-1  // not resolved  token=0x60000F7  System.Void RemoveAtSwapBack(System.Int32 index)
  RVA=-1  // not resolved  token=0x60000F8  System.Void RemoveRangeSwapBack(System.Int32 index, System.Int32 count)
  RVA=-1  // not resolved  token=0x60000F9  System.Void RemoveAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x60000FA  System.Void RemoveRange(System.Int32 index, System.Int32 count)
  RVA=-1  // not resolved  token=0x60000FB  System.Int32 CompareTo(Unity.Collections.FixedList32Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000FC  System.Boolean Equals(Unity.Collections.FixedList32Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000FD  System.Int32 CompareTo(Unity.Collections.FixedList64Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000FE  System.Boolean Equals(Unity.Collections.FixedList64Bytes<T> other)
  RVA=-1  // not resolved  token=0x60000FF  System.Int32 CompareTo(Unity.Collections.FixedList128Bytes<T> other)
  RVA=-1  // not resolved  token=0x6000100  System.Boolean Equals(Unity.Collections.FixedList128Bytes<T> other)
  RVA=-1  // not resolved  token=0x6000101  System.Void .ctor(Unity.Collections.FixedList128Bytes<T>& other)
  RVA=-1  // not resolved  token=0x6000102  System.Int32 Initialize(Unity.Collections.FixedList128Bytes<T>& other)
  RVA=-1  // not resolved  token=0x6000103  Unity.Collections.FixedList512Bytes<T> op_Implicit(Unity.Collections.FixedList128Bytes<T>& other)
  RVA=-1  // not resolved  token=0x6000104  System.Int32 CompareTo(Unity.Collections.FixedList512Bytes<T> other)
  RVA=-1  // not resolved  token=0x6000105  System.Boolean Equals(Unity.Collections.FixedList512Bytes<T> other)
  RVA=-1  // not resolved  token=0x6000106  System.Int32 CompareTo(Unity.Collections.FixedList4096Bytes<T> other)
  RVA=-1  // not resolved  token=0x6000107  System.Boolean Equals(Unity.Collections.FixedList4096Bytes<T> other)
  RVA=-1  // not resolved  token=0x6000108  System.Boolean Equals(System.Object obj)
  RVA=-1  // not resolved  token=0x6000109  Unity.Collections.FixedList512Bytes.Enumerator<T> GetEnumerator()
  RVA=-1  // not resolved  token=0x600010A  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x600010B  System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
END_CLASS

CLASS: Unity.Collections.FixedList512BytesExtensions
TYPE:  static class
TOKEN: 0x2000045
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000112  System.Int32 IndexOf(Unity.Collections.FixedList512Bytes<T>& list, U value)
  RVA=-1  // generic def  token=0x6000113  System.Boolean Contains(Unity.Collections.FixedList512Bytes<T>& list, U value)
  RVA=-1  // generic def  token=0x6000114  System.Boolean Remove(Unity.Collections.FixedList512Bytes<T>& list, U value)
END_CLASS

CLASS: Unity.Collections.FixedList512BytesDebugView`1
TYPE:  sealed class
TOKEN: 0x2000046
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Collections.FixedList4096Bytes`1
TYPE:  sealed struct
TOKEN: 0x2000047
IMPLEMENTS: Unity.Collections.INativeList`1 Unity.Collections.IIndexable`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1
FIELDS:
  private           System.UInt16                   length  // 0x0
  private           Unity.Collections.FixedBytes4094buffer  // 0x0
PROPERTIES:
  Length  get=-1  // not resolved  set=-1  // not resolved
  IsEmpty  get=-1  // not resolved
  LengthInBytes  get=-1  // not resolved
  Buffer  get=-1  // not resolved
  Capacity  get=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600011D  T& ElementAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x600011E  System.Int32 GetHashCode()
  RVA=-1  // not resolved  token=0x600011F  System.Void Add(T& item)
  RVA=-1  // not resolved  token=0x6000120  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000121  System.Void RemoveAtSwapBack(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000122  System.Void RemoveRangeSwapBack(System.Int32 index, System.Int32 count)
  RVA=-1  // not resolved  token=0x6000123  System.Void RemoveAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000124  System.Void RemoveRange(System.Int32 index, System.Int32 count)
  RVA=-1  // not resolved  token=0x6000125  System.Int32 CompareTo(Unity.Collections.FixedList32Bytes<T> other)
  RVA=-1  // not resolved  token=0x6000126  System.Boolean Equals(Unity.Collections.FixedList32Bytes<T> other)
  RVA=-1  // not resolved  token=0x6000127  System.Int32 CompareTo(Unity.Collections.FixedList64Bytes<T> other)
  RVA=-1  // not resolved  token=0x6000128  System.Boolean Equals(Unity.Collections.FixedList64Bytes<T> other)
  RVA=-1  // not resolved  token=0x6000129  System.Int32 CompareTo(Unity.Collections.FixedList128Bytes<T> other)
  RVA=-1  // not resolved  token=0x600012A  System.Boolean Equals(Unity.Collections.FixedList128Bytes<T> other)
  RVA=-1  // not resolved  token=0x600012B  System.Int32 CompareTo(Unity.Collections.FixedList512Bytes<T> other)
  RVA=-1  // not resolved  token=0x600012C  System.Boolean Equals(Unity.Collections.FixedList512Bytes<T> other)
  RVA=-1  // not resolved  token=0x600012D  System.Int32 CompareTo(Unity.Collections.FixedList4096Bytes<T> other)
  RVA=-1  // not resolved  token=0x600012E  System.Boolean Equals(Unity.Collections.FixedList4096Bytes<T> other)
  RVA=-1  // not resolved  token=0x600012F  System.Boolean Equals(System.Object obj)
  RVA=-1  // not resolved  token=0x6000130  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000131  System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
END_CLASS

CLASS: Unity.Collections.FixedList4096BytesExtensions
TYPE:  static class
TOKEN: 0x2000048
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000132  System.Int32 IndexOf(Unity.Collections.FixedList4096Bytes<T>& list, U value)
  RVA=-1  // generic def  token=0x6000133  System.Boolean Contains(Unity.Collections.FixedList4096Bytes<T>& list, U value)
END_CLASS

CLASS: Unity.Collections.FixedList4096BytesDebugView`1
TYPE:  sealed class
TOKEN: 0x2000049
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Collections.FixedBytes16
TYPE:  sealed struct
TOKEN: 0x200004A
SIZE:  0x20
FIELDS:
  public            System.Byte                     byte0000  // 0x10
  public            System.Byte                     byte0001  // 0x11
  public            System.Byte                     byte0002  // 0x12
  public            System.Byte                     byte0003  // 0x13
  public            System.Byte                     byte0004  // 0x14
  public            System.Byte                     byte0005  // 0x15
  public            System.Byte                     byte0006  // 0x16
  public            System.Byte                     byte0007  // 0x17
  public            System.Byte                     byte0008  // 0x18
  public            System.Byte                     byte0009  // 0x19
  public            System.Byte                     byte0010  // 0x1a
  public            System.Byte                     byte0011  // 0x1b
  public            System.Byte                     byte0012  // 0x1c
  public            System.Byte                     byte0013  // 0x1d
  public            System.Byte                     byte0014  // 0x1e
  public            System.Byte                     byte0015  // 0x1f
METHODS:
END_CLASS

CLASS: Unity.Collections.FixedBytes30
TYPE:  sealed struct
TOKEN: 0x200004B
SIZE:  0x2E
FIELDS:
  public            Unity.Collections.FixedBytes16  offset0000  // 0x10
  public            System.Byte                     byte0016  // 0x20
  public            System.Byte                     byte0017  // 0x21
  public            System.Byte                     byte0018  // 0x22
  public            System.Byte                     byte0019  // 0x23
  public            System.Byte                     byte0020  // 0x24
  public            System.Byte                     byte0021  // 0x25
  public            System.Byte                     byte0022  // 0x26
  public            System.Byte                     byte0023  // 0x27
  public            System.Byte                     byte0024  // 0x28
  public            System.Byte                     byte0025  // 0x29
  public            System.Byte                     byte0026  // 0x2a
  public            System.Byte                     byte0027  // 0x2b
  public            System.Byte                     byte0028  // 0x2c
  public            System.Byte                     byte0029  // 0x2d
METHODS:
END_CLASS

CLASS: Unity.Collections.FixedString32Bytes
TYPE:  sealed struct
TOKEN: 0x200004C
SIZE:  0x30
IMPLEMENTS: Unity.Collections.INativeList`1 Unity.Collections.IIndexable`1 Unity.Collections.IUTF8Bytes System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1
FIELDS:
  private   static  System.UInt16                   utf8MaxLengthInBytes  // const
  private           System.UInt16                   utf8LengthInBytes  // 0x10
  private           Unity.Collections.FixedBytes30  bytes  // 0x12
PROPERTIES:
  Value  get=0x05395BE4
  Length  get=0x04D87110  set=0x04DAB510
  IsEmpty  get=0x0B160F5C
METHODS:
  RVA=0x04DAB500  token=0x6000135  System.Byte* GetUnsafePtr()
  RVA=0x0B160EF0  token=0x6000138  System.Boolean TryResize(System.Int32 newLength, Unity.Collections.NativeArrayOptions clearOptions)
  RVA=0x0B160A44  token=0x600013A  System.Int32 CompareTo(System.String other)
  RVA=0x0B160B60  token=0x600013B  System.Boolean Equals(System.String other)
  RVA=0x038759B0  token=0x600013C  System.Void .ctor(System.String source)
  RVA=0x038759D0  token=0x600013D  System.Int32 Initialize(System.String source)
  RVA=0x0B160AC8  token=0x600013E  System.Int32 CompareTo(Unity.Collections.FixedString32Bytes other)
  RVA=0x0B160FF0  token=0x600013F  System.Boolean op_Equality(Unity.Collections.FixedString32Bytes& a, Unity.Collections.FixedString32Bytes& b)
  RVA=0x0B160C0C  token=0x6000140  System.Boolean Equals(Unity.Collections.FixedString32Bytes other)
  RVA=0x0B160B14  token=0x6000141  System.Int32 CompareTo(Unity.Collections.FixedString64Bytes other)
  RVA=0x0B160F64  token=0x6000142  System.Boolean op_Equality(Unity.Collections.FixedString32Bytes& a, Unity.Collections.FixedString64Bytes& b)
  RVA=0x0B160BF8  token=0x6000143  System.Boolean Equals(Unity.Collections.FixedString64Bytes other)
  RVA=0x0B1609F8  token=0x6000144  System.Int32 CompareTo(Unity.Collections.FixedString128Bytes other)
  RVA=0x0B1610FC  token=0x6000145  System.Boolean op_Equality(Unity.Collections.FixedString32Bytes& a, Unity.Collections.FixedString128Bytes& b)
  RVA=0x0B160C20  token=0x6000146  System.Boolean Equals(Unity.Collections.FixedString128Bytes other)
  RVA=0x0B1609AC  token=0x6000147  System.Int32 CompareTo(Unity.Collections.FixedString512Bytes other)
  RVA=0x0B161070  token=0x6000148  System.Boolean op_Equality(Unity.Collections.FixedString32Bytes& a, Unity.Collections.FixedString512Bytes& b)
  RVA=0x0B160BE4  token=0x6000149  System.Boolean Equals(Unity.Collections.FixedString512Bytes other)
  RVA=0x0B160A7C  token=0x600014A  System.Int32 CompareTo(Unity.Collections.FixedString4096Bytes other)
  RVA=0x0B161188  token=0x600014B  System.Boolean op_Equality(Unity.Collections.FixedString32Bytes& a, Unity.Collections.FixedString4096Bytes& b)
  RVA=0x0B160EDC  token=0x600014C  System.Boolean Equals(Unity.Collections.FixedString4096Bytes other)
  RVA=0x03875980  token=0x600014D  Unity.Collections.FixedString32Bytes op_Implicit(System.String b)
  RVA=0x05395BE4  token=0x600014E  System.String ToString()
  RVA=0x034D7A80  token=0x600014F  System.Int32 GetHashCode()
  RVA=0x0B160C34  token=0x6000150  System.Boolean Equals(System.Object obj)
END_CLASS

CLASS: Unity.Collections.FixedBytes62
TYPE:  sealed struct
TOKEN: 0x200004D
SIZE:  0x4E
FIELDS:
  public            Unity.Collections.FixedBytes16  offset0000  // 0x10
  public            Unity.Collections.FixedBytes16  offset0016  // 0x20
  public            Unity.Collections.FixedBytes16  offset0032  // 0x30
  public            System.Byte                     byte0048  // 0x40
  public            System.Byte                     byte0049  // 0x41
  public            System.Byte                     byte0050  // 0x42
  public            System.Byte                     byte0051  // 0x43
  public            System.Byte                     byte0052  // 0x44
  public            System.Byte                     byte0053  // 0x45
  public            System.Byte                     byte0054  // 0x46
  public            System.Byte                     byte0055  // 0x47
  public            System.Byte                     byte0056  // 0x48
  public            System.Byte                     byte0057  // 0x49
  public            System.Byte                     byte0058  // 0x4a
  public            System.Byte                     byte0059  // 0x4b
  public            System.Byte                     byte0060  // 0x4c
  public            System.Byte                     byte0061  // 0x4d
METHODS:
END_CLASS

CLASS: Unity.Collections.FixedString64Bytes
TYPE:  sealed struct
TOKEN: 0x200004E
SIZE:  0x50
IMPLEMENTS: Unity.Collections.INativeList`1 Unity.Collections.IIndexable`1 Unity.Collections.IUTF8Bytes System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1
FIELDS:
  private   static  System.UInt16                   utf8MaxLengthInBytes  // const
  private           System.UInt16                   utf8LengthInBytes  // 0x10
  private           Unity.Collections.FixedBytes62  bytes  // 0x12
PROPERTIES:
  Value  get=0x053945DC
  Length  get=0x04D87110  set=0x04DAB510
METHODS:
  RVA=0x04DAB500  token=0x6000152  System.Byte* GetUnsafePtr()
  RVA=0x0B1629BC  token=0x6000155  System.Boolean TryResize(System.Int32 newLength, Unity.Collections.NativeArrayOptions clearOptions)
  RVA=0x0B1624A8  token=0x6000156  System.Int32 CompareTo(System.String other)
  RVA=0x0B16285C  token=0x6000157  System.Boolean Equals(System.String other)
  RVA=0x0B162A28  token=0x6000158  System.Void .ctor(System.String source)
  RVA=0x0B162944  token=0x6000159  System.Int32 Initialize(System.String source)
  RVA=0x0B1624E0  token=0x600015A  System.Int32 CompareTo(Unity.Collections.FixedString32Bytes other)
  RVA=0x0B162B5C  token=0x600015B  System.Boolean op_Equality(Unity.Collections.FixedString64Bytes& a, Unity.Collections.FixedString32Bytes& b)
  RVA=0x0B162820  token=0x600015C  System.Boolean Equals(Unity.Collections.FixedString32Bytes other)
  RVA=0x0B16252C  token=0x600015D  System.Int32 CompareTo(Unity.Collections.FixedString64Bytes other)
  RVA=0x0B162BE8  token=0x600015E  System.Boolean op_Equality(Unity.Collections.FixedString64Bytes& a, Unity.Collections.FixedString64Bytes& b)
  RVA=0x0B162848  token=0x600015F  System.Boolean Equals(Unity.Collections.FixedString64Bytes other)
  RVA=0x0B162410  token=0x6000160  System.Int32 CompareTo(Unity.Collections.FixedString128Bytes other)
  RVA=0x0B162AD0  token=0x6000161  System.Boolean op_Equality(Unity.Collections.FixedString64Bytes& a, Unity.Collections.FixedString128Bytes& b)
  RVA=0x0B162834  token=0x6000162  System.Boolean Equals(Unity.Collections.FixedString128Bytes other)
  RVA=0x0B1623C4  token=0x6000163  System.Int32 CompareTo(Unity.Collections.FixedString512Bytes other)
  RVA=0x0B162A44  token=0x6000164  System.Boolean op_Equality(Unity.Collections.FixedString64Bytes& a, Unity.Collections.FixedString512Bytes& b)
  RVA=0x0B1628E0  token=0x6000165  System.Boolean Equals(Unity.Collections.FixedString512Bytes other)
  RVA=0x0B16245C  token=0x6000166  System.Int32 CompareTo(Unity.Collections.FixedString4096Bytes other)
  RVA=0x0B162C68  token=0x6000167  System.Boolean op_Equality(Unity.Collections.FixedString64Bytes& a, Unity.Collections.FixedString4096Bytes& b)
  RVA=0x0B1628F4  token=0x6000168  System.Boolean Equals(Unity.Collections.FixedString4096Bytes other)
  RVA=0x0B162CF4  token=0x6000169  Unity.Collections.FixedString64Bytes op_Implicit(System.String b)
  RVA=0x053945DC  token=0x600016A  System.String ToString()
  RVA=0x0B162908  token=0x600016B  System.Int32 GetHashCode()
  RVA=0x0B162578  token=0x600016C  System.Boolean Equals(System.Object obj)
END_CLASS

CLASS: Unity.Collections.FixedBytes126
TYPE:  sealed struct
TOKEN: 0x200004F
SIZE:  0x8E
FIELDS:
  public            Unity.Collections.FixedBytes16  offset0000  // 0x10
  public            Unity.Collections.FixedBytes16  offset0016  // 0x20
  public            Unity.Collections.FixedBytes16  offset0032  // 0x30
  public            Unity.Collections.FixedBytes16  offset0048  // 0x40
  public            Unity.Collections.FixedBytes16  offset0064  // 0x50
  public            Unity.Collections.FixedBytes16  offset0080  // 0x60
  public            Unity.Collections.FixedBytes16  offset0096  // 0x70
  public            System.Byte                     byte0112  // 0x80
  public            System.Byte                     byte0113  // 0x81
  public            System.Byte                     byte0114  // 0x82
  public            System.Byte                     byte0115  // 0x83
  public            System.Byte                     byte0116  // 0x84
  public            System.Byte                     byte0117  // 0x85
  public            System.Byte                     byte0118  // 0x86
  public            System.Byte                     byte0119  // 0x87
  public            System.Byte                     byte0120  // 0x88
  public            System.Byte                     byte0121  // 0x89
  public            System.Byte                     byte0122  // 0x8a
  public            System.Byte                     byte0123  // 0x8b
  public            System.Byte                     byte0124  // 0x8c
  public            System.Byte                     byte0125  // 0x8d
METHODS:
END_CLASS

CLASS: Unity.Collections.FixedString128Bytes
TYPE:  sealed struct
TOKEN: 0x2000050
SIZE:  0x90
IMPLEMENTS: Unity.Collections.INativeList`1 Unity.Collections.IIndexable`1 Unity.Collections.IUTF8Bytes System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1
FIELDS:
  private   static  System.UInt16                   utf8MaxLengthInBytes  // const
  private           System.UInt16                   utf8LengthInBytes  // 0x10
  private           Unity.Collections.FixedBytes126 bytes  // 0x12
PROPERTIES:
  Length  get=0x04D87110  set=0x04DAB510
METHODS:
  RVA=0x04DAB500  token=0x600016D  System.Byte* GetUnsafePtr()
  RVA=0x0B160690  token=0x6000170  System.Boolean TryResize(System.Int32 newLength, Unity.Collections.NativeArrayOptions clearOptions)
  RVA=0x0B1601BC  token=0x6000171  System.Int32 CompareTo(System.String other)
  RVA=0x0B1602D8  token=0x6000172  System.Boolean Equals(System.String other)
  RVA=0x03875060  token=0x6000173  System.Void .ctor(System.String source)
  RVA=0x03875090  token=0x6000174  System.Int32 Initialize(System.String source)
  RVA=0x0B16023C  token=0x6000175  System.Int32 CompareTo(Unity.Collections.FixedString32Bytes other)
  RVA=0x0B160788  token=0x6000176  System.Boolean op_Equality(Unity.Collections.FixedString128Bytes& a, Unity.Collections.FixedString32Bytes& b)
  RVA=0x0B1602C4  token=0x6000177  System.Boolean Equals(Unity.Collections.FixedString32Bytes other)
  RVA=0x0B1601F0  token=0x6000178  System.Int32 CompareTo(Unity.Collections.FixedString64Bytes other)
  RVA=0x0B160894  token=0x6000179  System.Boolean op_Equality(Unity.Collections.FixedString128Bytes& a, Unity.Collections.FixedString64Bytes& b)
  RVA=0x0B16035C  token=0x600017A  System.Boolean Equals(Unity.Collections.FixedString64Bytes other)
  RVA=0x0B160124  token=0x600017B  System.Int32 CompareTo(Unity.Collections.FixedString128Bytes other)
  RVA=0x0B160814  token=0x600017C  System.Boolean op_Equality(Unity.Collections.FixedString128Bytes& a, Unity.Collections.FixedString128Bytes& b)
  RVA=0x0B1602B0  token=0x600017D  System.Boolean Equals(Unity.Collections.FixedString128Bytes other)
  RVA=0x0B160170  token=0x600017E  System.Int32 CompareTo(Unity.Collections.FixedString512Bytes other)
  RVA=0x0B1606FC  token=0x600017F  System.Boolean op_Equality(Unity.Collections.FixedString128Bytes& a, Unity.Collections.FixedString512Bytes& b)
  RVA=0x0B160288  token=0x6000180  System.Boolean Equals(Unity.Collections.FixedString512Bytes other)
  RVA=0x0B1600D8  token=0x6000181  System.Int32 CompareTo(Unity.Collections.FixedString4096Bytes other)
  RVA=0x0B160920  token=0x6000182  System.Boolean op_Equality(Unity.Collections.FixedString128Bytes& a, Unity.Collections.FixedString4096Bytes& b)
  RVA=0x0B16029C  token=0x6000183  System.Boolean Equals(Unity.Collections.FixedString4096Bytes other)
  RVA=0x03875020  token=0x6000184  Unity.Collections.FixedString128Bytes op_Implicit(System.String b)
  RVA=0x0B160654  token=0x6000185  System.String ToString()
  RVA=0x0B160618  token=0x6000186  System.Int32 GetHashCode()
  RVA=0x0B160370  token=0x6000187  System.Boolean Equals(System.Object obj)
END_CLASS

CLASS: Unity.Collections.FixedBytes510
TYPE:  sealed struct
TOKEN: 0x2000051
SIZE:  0x20E
FIELDS:
  public            Unity.Collections.FixedBytes16  offset0000  // 0x10
  public            Unity.Collections.FixedBytes16  offset0016  // 0x20
  public            Unity.Collections.FixedBytes16  offset0032  // 0x30
  public            Unity.Collections.FixedBytes16  offset0048  // 0x40
  public            Unity.Collections.FixedBytes16  offset0064  // 0x50
  public            Unity.Collections.FixedBytes16  offset0080  // 0x60
  public            Unity.Collections.FixedBytes16  offset0096  // 0x70
  public            Unity.Collections.FixedBytes16  offset0112  // 0x80
  public            Unity.Collections.FixedBytes16  offset0128  // 0x90
  public            Unity.Collections.FixedBytes16  offset0144  // 0xa0
  public            Unity.Collections.FixedBytes16  offset0160  // 0xb0
  public            Unity.Collections.FixedBytes16  offset0176  // 0xc0
  public            Unity.Collections.FixedBytes16  offset0192  // 0xd0
  public            Unity.Collections.FixedBytes16  offset0208  // 0xe0
  public            Unity.Collections.FixedBytes16  offset0224  // 0xf0
  public            Unity.Collections.FixedBytes16  offset0240  // 0x100
  public            Unity.Collections.FixedBytes16  offset0256  // 0x110
  public            Unity.Collections.FixedBytes16  offset0272  // 0x120
  public            Unity.Collections.FixedBytes16  offset0288  // 0x130
  public            Unity.Collections.FixedBytes16  offset0304  // 0x140
  public            Unity.Collections.FixedBytes16  offset0320  // 0x150
  public            Unity.Collections.FixedBytes16  offset0336  // 0x160
  public            Unity.Collections.FixedBytes16  offset0352  // 0x170
  public            Unity.Collections.FixedBytes16  offset0368  // 0x180
  public            Unity.Collections.FixedBytes16  offset0384  // 0x190
  public            Unity.Collections.FixedBytes16  offset0400  // 0x1a0
  public            Unity.Collections.FixedBytes16  offset0416  // 0x1b0
  public            Unity.Collections.FixedBytes16  offset0432  // 0x1c0
  public            Unity.Collections.FixedBytes16  offset0448  // 0x1d0
  public            Unity.Collections.FixedBytes16  offset0464  // 0x1e0
  public            Unity.Collections.FixedBytes16  offset0480  // 0x1f0
  public            System.Byte                     byte0496  // 0x200
  public            System.Byte                     byte0497  // 0x201
  public            System.Byte                     byte0498  // 0x202
  public            System.Byte                     byte0499  // 0x203
  public            System.Byte                     byte0500  // 0x204
  public            System.Byte                     byte0501  // 0x205
  public            System.Byte                     byte0502  // 0x206
  public            System.Byte                     byte0503  // 0x207
  public            System.Byte                     byte0504  // 0x208
  public            System.Byte                     byte0505  // 0x209
  public            System.Byte                     byte0506  // 0x20a
  public            System.Byte                     byte0507  // 0x20b
  public            System.Byte                     byte0508  // 0x20c
  public            System.Byte                     byte0509  // 0x20d
METHODS:
END_CLASS

CLASS: Unity.Collections.FixedString512Bytes
TYPE:  sealed struct
TOKEN: 0x2000052
SIZE:  0x210
IMPLEMENTS: Unity.Collections.INativeList`1 Unity.Collections.IIndexable`1 Unity.Collections.IUTF8Bytes System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1
FIELDS:
  private           System.UInt16                   utf8LengthInBytes  // 0x10
  private           Unity.Collections.FixedBytes510 bytes  // 0x12
PROPERTIES:
  Length  get=0x04D87110
METHODS:
  RVA=0x04DAB500  token=0x6000188  System.Byte* GetUnsafePtr()
  RVA=0x0B1620A4  token=0x600018A  System.Boolean TryResize(System.Int32 newLength, Unity.Collections.NativeArrayOptions clearOptions)
  RVA=0x0B161C1C  token=0x600018B  System.Int32 CompareTo(System.String other)
  RVA=0x0B161FA8  token=0x600018C  System.Boolean Equals(System.String other)
  RVA=0x0B161C50  token=0x600018D  System.Int32 CompareTo(Unity.Collections.FixedString32Bytes other)
  RVA=0x0B162338  token=0x600018E  System.Boolean op_Equality(Unity.Collections.FixedString512Bytes& a, Unity.Collections.FixedString32Bytes& b)
  RVA=0x0B161CB0  token=0x600018F  System.Boolean Equals(Unity.Collections.FixedString32Bytes other)
  RVA=0x0B161B84  token=0x6000190  System.Int32 CompareTo(Unity.Collections.FixedString64Bytes other)
  RVA=0x0B1622AC  token=0x6000191  System.Boolean op_Equality(Unity.Collections.FixedString512Bytes& a, Unity.Collections.FixedString64Bytes& b)
  RVA=0x0B161C9C  token=0x6000192  System.Boolean Equals(Unity.Collections.FixedString64Bytes other)
  RVA=0x0B161B38  token=0x6000193  System.Int32 CompareTo(Unity.Collections.FixedString128Bytes other)
  RVA=0x0B162194  token=0x6000194  System.Boolean op_Equality(Unity.Collections.FixedString512Bytes& a, Unity.Collections.FixedString128Bytes& b)
  RVA=0x0B161F80  token=0x6000195  System.Boolean Equals(Unity.Collections.FixedString128Bytes other)
  RVA=0x0B161AEC  token=0x6000196  System.Int32 CompareTo(Unity.Collections.FixedString512Bytes other)
  RVA=0x0B162114  token=0x6000197  System.Boolean op_Equality(Unity.Collections.FixedString512Bytes& a, Unity.Collections.FixedString512Bytes& b)
  RVA=0x0B161F6C  token=0x6000198  System.Boolean Equals(Unity.Collections.FixedString512Bytes other)
  RVA=0x0B161BD0  token=0x6000199  System.Int32 CompareTo(Unity.Collections.FixedString4096Bytes other)
  RVA=0x0B162220  token=0x600019A  System.Boolean op_Equality(Unity.Collections.FixedString512Bytes& a, Unity.Collections.FixedString4096Bytes& b)
  RVA=0x0B161F94  token=0x600019B  System.Boolean Equals(Unity.Collections.FixedString4096Bytes other)
  RVA=0x0B162068  token=0x600019C  System.String ToString()
  RVA=0x0B16202C  token=0x600019D  System.Int32 GetHashCode()
  RVA=0x0B161CC4  token=0x600019E  System.Boolean Equals(System.Object obj)
END_CLASS

CLASS: Unity.Collections.FixedBytes4094
TYPE:  sealed struct
TOKEN: 0x2000053
SIZE:  0x100E
FIELDS:
  public            Unity.Collections.FixedBytes16  offset0000  // 0x10
  public            Unity.Collections.FixedBytes16  offset0016  // 0x20
  public            Unity.Collections.FixedBytes16  offset0032  // 0x30
  public            Unity.Collections.FixedBytes16  offset0048  // 0x40
  public            Unity.Collections.FixedBytes16  offset0064  // 0x50
  public            Unity.Collections.FixedBytes16  offset0080  // 0x60
  public            Unity.Collections.FixedBytes16  offset0096  // 0x70
  public            Unity.Collections.FixedBytes16  offset0112  // 0x80
  public            Unity.Collections.FixedBytes16  offset0128  // 0x90
  public            Unity.Collections.FixedBytes16  offset0144  // 0xa0
  public            Unity.Collections.FixedBytes16  offset0160  // 0xb0
  public            Unity.Collections.FixedBytes16  offset0176  // 0xc0
  public            Unity.Collections.FixedBytes16  offset0192  // 0xd0
  public            Unity.Collections.FixedBytes16  offset0208  // 0xe0
  public            Unity.Collections.FixedBytes16  offset0224  // 0xf0
  public            Unity.Collections.FixedBytes16  offset0240  // 0x100
  public            Unity.Collections.FixedBytes16  offset0256  // 0x110
  public            Unity.Collections.FixedBytes16  offset0272  // 0x120
  public            Unity.Collections.FixedBytes16  offset0288  // 0x130
  public            Unity.Collections.FixedBytes16  offset0304  // 0x140
  public            Unity.Collections.FixedBytes16  offset0320  // 0x150
  public            Unity.Collections.FixedBytes16  offset0336  // 0x160
  public            Unity.Collections.FixedBytes16  offset0352  // 0x170
  public            Unity.Collections.FixedBytes16  offset0368  // 0x180
  public            Unity.Collections.FixedBytes16  offset0384  // 0x190
  public            Unity.Collections.FixedBytes16  offset0400  // 0x1a0
  public            Unity.Collections.FixedBytes16  offset0416  // 0x1b0
  public            Unity.Collections.FixedBytes16  offset0432  // 0x1c0
  public            Unity.Collections.FixedBytes16  offset0448  // 0x1d0
  public            Unity.Collections.FixedBytes16  offset0464  // 0x1e0
  public            Unity.Collections.FixedBytes16  offset0480  // 0x1f0
  public            Unity.Collections.FixedBytes16  offset0496  // 0x200
  public            Unity.Collections.FixedBytes16  offset0512  // 0x210
  public            Unity.Collections.FixedBytes16  offset0528  // 0x220
  public            Unity.Collections.FixedBytes16  offset0544  // 0x230
  public            Unity.Collections.FixedBytes16  offset0560  // 0x240
  public            Unity.Collections.FixedBytes16  offset0576  // 0x250
  public            Unity.Collections.FixedBytes16  offset0592  // 0x260
  public            Unity.Collections.FixedBytes16  offset0608  // 0x270
  public            Unity.Collections.FixedBytes16  offset0624  // 0x280
  public            Unity.Collections.FixedBytes16  offset0640  // 0x290
  public            Unity.Collections.FixedBytes16  offset0656  // 0x2a0
  public            Unity.Collections.FixedBytes16  offset0672  // 0x2b0
  public            Unity.Collections.FixedBytes16  offset0688  // 0x2c0
  public            Unity.Collections.FixedBytes16  offset0704  // 0x2d0
  public            Unity.Collections.FixedBytes16  offset0720  // 0x2e0
  public            Unity.Collections.FixedBytes16  offset0736  // 0x2f0
  public            Unity.Collections.FixedBytes16  offset0752  // 0x300
  public            Unity.Collections.FixedBytes16  offset0768  // 0x310
  public            Unity.Collections.FixedBytes16  offset0784  // 0x320
  public            Unity.Collections.FixedBytes16  offset0800  // 0x330
  public            Unity.Collections.FixedBytes16  offset0816  // 0x340
  public            Unity.Collections.FixedBytes16  offset0832  // 0x350
  public            Unity.Collections.FixedBytes16  offset0848  // 0x360
  public            Unity.Collections.FixedBytes16  offset0864  // 0x370
  public            Unity.Collections.FixedBytes16  offset0880  // 0x380
  public            Unity.Collections.FixedBytes16  offset0896  // 0x390
  public            Unity.Collections.FixedBytes16  offset0912  // 0x3a0
  public            Unity.Collections.FixedBytes16  offset0928  // 0x3b0
  public            Unity.Collections.FixedBytes16  offset0944  // 0x3c0
  public            Unity.Collections.FixedBytes16  offset0960  // 0x3d0
  public            Unity.Collections.FixedBytes16  offset0976  // 0x3e0
  public            Unity.Collections.FixedBytes16  offset0992  // 0x3f0
  public            Unity.Collections.FixedBytes16  offset1008  // 0x400
  public            Unity.Collections.FixedBytes16  offset1024  // 0x410
  public            Unity.Collections.FixedBytes16  offset1040  // 0x420
  public            Unity.Collections.FixedBytes16  offset1056  // 0x430
  public            Unity.Collections.FixedBytes16  offset1072  // 0x440
  public            Unity.Collections.FixedBytes16  offset1088  // 0x450
  public            Unity.Collections.FixedBytes16  offset1104  // 0x460
  public            Unity.Collections.FixedBytes16  offset1120  // 0x470
  public            Unity.Collections.FixedBytes16  offset1136  // 0x480
  public            Unity.Collections.FixedBytes16  offset1152  // 0x490
  public            Unity.Collections.FixedBytes16  offset1168  // 0x4a0
  public            Unity.Collections.FixedBytes16  offset1184  // 0x4b0
  public            Unity.Collections.FixedBytes16  offset1200  // 0x4c0
  public            Unity.Collections.FixedBytes16  offset1216  // 0x4d0
  public            Unity.Collections.FixedBytes16  offset1232  // 0x4e0
  public            Unity.Collections.FixedBytes16  offset1248  // 0x4f0
  public            Unity.Collections.FixedBytes16  offset1264  // 0x500
  public            Unity.Collections.FixedBytes16  offset1280  // 0x510
  public            Unity.Collections.FixedBytes16  offset1296  // 0x520
  public            Unity.Collections.FixedBytes16  offset1312  // 0x530
  public            Unity.Collections.FixedBytes16  offset1328  // 0x540
  public            Unity.Collections.FixedBytes16  offset1344  // 0x550
  public            Unity.Collections.FixedBytes16  offset1360  // 0x560
  public            Unity.Collections.FixedBytes16  offset1376  // 0x570
  public            Unity.Collections.FixedBytes16  offset1392  // 0x580
  public            Unity.Collections.FixedBytes16  offset1408  // 0x590
  public            Unity.Collections.FixedBytes16  offset1424  // 0x5a0
  public            Unity.Collections.FixedBytes16  offset1440  // 0x5b0
  public            Unity.Collections.FixedBytes16  offset1456  // 0x5c0
  public            Unity.Collections.FixedBytes16  offset1472  // 0x5d0
  public            Unity.Collections.FixedBytes16  offset1488  // 0x5e0
  public            Unity.Collections.FixedBytes16  offset1504  // 0x5f0
  public            Unity.Collections.FixedBytes16  offset1520  // 0x600
  public            Unity.Collections.FixedBytes16  offset1536  // 0x610
  public            Unity.Collections.FixedBytes16  offset1552  // 0x620
  public            Unity.Collections.FixedBytes16  offset1568  // 0x630
  public            Unity.Collections.FixedBytes16  offset1584  // 0x640
  public            Unity.Collections.FixedBytes16  offset1600  // 0x650
  public            Unity.Collections.FixedBytes16  offset1616  // 0x660
  public            Unity.Collections.FixedBytes16  offset1632  // 0x670
  public            Unity.Collections.FixedBytes16  offset1648  // 0x680
  public            Unity.Collections.FixedBytes16  offset1664  // 0x690
  public            Unity.Collections.FixedBytes16  offset1680  // 0x6a0
  public            Unity.Collections.FixedBytes16  offset1696  // 0x6b0
  public            Unity.Collections.FixedBytes16  offset1712  // 0x6c0
  public            Unity.Collections.FixedBytes16  offset1728  // 0x6d0
  public            Unity.Collections.FixedBytes16  offset1744  // 0x6e0
  public            Unity.Collections.FixedBytes16  offset1760  // 0x6f0
  public            Unity.Collections.FixedBytes16  offset1776  // 0x700
  public            Unity.Collections.FixedBytes16  offset1792  // 0x710
  public            Unity.Collections.FixedBytes16  offset1808  // 0x720
  public            Unity.Collections.FixedBytes16  offset1824  // 0x730
  public            Unity.Collections.FixedBytes16  offset1840  // 0x740
  public            Unity.Collections.FixedBytes16  offset1856  // 0x750
  public            Unity.Collections.FixedBytes16  offset1872  // 0x760
  public            Unity.Collections.FixedBytes16  offset1888  // 0x770
  public            Unity.Collections.FixedBytes16  offset1904  // 0x780
  public            Unity.Collections.FixedBytes16  offset1920  // 0x790
  public            Unity.Collections.FixedBytes16  offset1936  // 0x7a0
  public            Unity.Collections.FixedBytes16  offset1952  // 0x7b0
  public            Unity.Collections.FixedBytes16  offset1968  // 0x7c0
  public            Unity.Collections.FixedBytes16  offset1984  // 0x7d0
  public            Unity.Collections.FixedBytes16  offset2000  // 0x7e0
  public            Unity.Collections.FixedBytes16  offset2016  // 0x7f0
  public            Unity.Collections.FixedBytes16  offset2032  // 0x800
  public            Unity.Collections.FixedBytes16  offset2048  // 0x810
  public            Unity.Collections.FixedBytes16  offset2064  // 0x820
  public            Unity.Collections.FixedBytes16  offset2080  // 0x830
  public            Unity.Collections.FixedBytes16  offset2096  // 0x840
  public            Unity.Collections.FixedBytes16  offset2112  // 0x850
  public            Unity.Collections.FixedBytes16  offset2128  // 0x860
  public            Unity.Collections.FixedBytes16  offset2144  // 0x870
  public            Unity.Collections.FixedBytes16  offset2160  // 0x880
  public            Unity.Collections.FixedBytes16  offset2176  // 0x890
  public            Unity.Collections.FixedBytes16  offset2192  // 0x8a0
  public            Unity.Collections.FixedBytes16  offset2208  // 0x8b0
  public            Unity.Collections.FixedBytes16  offset2224  // 0x8c0
  public            Unity.Collections.FixedBytes16  offset2240  // 0x8d0
  public            Unity.Collections.FixedBytes16  offset2256  // 0x8e0
  public            Unity.Collections.FixedBytes16  offset2272  // 0x8f0
  public            Unity.Collections.FixedBytes16  offset2288  // 0x900
  public            Unity.Collections.FixedBytes16  offset2304  // 0x910
  public            Unity.Collections.FixedBytes16  offset2320  // 0x920
  public            Unity.Collections.FixedBytes16  offset2336  // 0x930
  public            Unity.Collections.FixedBytes16  offset2352  // 0x940
  public            Unity.Collections.FixedBytes16  offset2368  // 0x950
  public            Unity.Collections.FixedBytes16  offset2384  // 0x960
  public            Unity.Collections.FixedBytes16  offset2400  // 0x970
  public            Unity.Collections.FixedBytes16  offset2416  // 0x980
  public            Unity.Collections.FixedBytes16  offset2432  // 0x990
  public            Unity.Collections.FixedBytes16  offset2448  // 0x9a0
  public            Unity.Collections.FixedBytes16  offset2464  // 0x9b0
  public            Unity.Collections.FixedBytes16  offset2480  // 0x9c0
  public            Unity.Collections.FixedBytes16  offset2496  // 0x9d0
  public            Unity.Collections.FixedBytes16  offset2512  // 0x9e0
  public            Unity.Collections.FixedBytes16  offset2528  // 0x9f0
  public            Unity.Collections.FixedBytes16  offset2544  // 0xa00
  public            Unity.Collections.FixedBytes16  offset2560  // 0xa10
  public            Unity.Collections.FixedBytes16  offset2576  // 0xa20
  public            Unity.Collections.FixedBytes16  offset2592  // 0xa30
  public            Unity.Collections.FixedBytes16  offset2608  // 0xa40
  public            Unity.Collections.FixedBytes16  offset2624  // 0xa50
  public            Unity.Collections.FixedBytes16  offset2640  // 0xa60
  public            Unity.Collections.FixedBytes16  offset2656  // 0xa70
  public            Unity.Collections.FixedBytes16  offset2672  // 0xa80
  public            Unity.Collections.FixedBytes16  offset2688  // 0xa90
  public            Unity.Collections.FixedBytes16  offset2704  // 0xaa0
  public            Unity.Collections.FixedBytes16  offset2720  // 0xab0
  public            Unity.Collections.FixedBytes16  offset2736  // 0xac0
  public            Unity.Collections.FixedBytes16  offset2752  // 0xad0
  public            Unity.Collections.FixedBytes16  offset2768  // 0xae0
  public            Unity.Collections.FixedBytes16  offset2784  // 0xaf0
  public            Unity.Collections.FixedBytes16  offset2800  // 0xb00
  public            Unity.Collections.FixedBytes16  offset2816  // 0xb10
  public            Unity.Collections.FixedBytes16  offset2832  // 0xb20
  public            Unity.Collections.FixedBytes16  offset2848  // 0xb30
  public            Unity.Collections.FixedBytes16  offset2864  // 0xb40
  public            Unity.Collections.FixedBytes16  offset2880  // 0xb50
  public            Unity.Collections.FixedBytes16  offset2896  // 0xb60
  public            Unity.Collections.FixedBytes16  offset2912  // 0xb70
  public            Unity.Collections.FixedBytes16  offset2928  // 0xb80
  public            Unity.Collections.FixedBytes16  offset2944  // 0xb90
  public            Unity.Collections.FixedBytes16  offset2960  // 0xba0
  public            Unity.Collections.FixedBytes16  offset2976  // 0xbb0
  public            Unity.Collections.FixedBytes16  offset2992  // 0xbc0
  public            Unity.Collections.FixedBytes16  offset3008  // 0xbd0
  public            Unity.Collections.FixedBytes16  offset3024  // 0xbe0
  public            Unity.Collections.FixedBytes16  offset3040  // 0xbf0
  public            Unity.Collections.FixedBytes16  offset3056  // 0xc00
  public            Unity.Collections.FixedBytes16  offset3072  // 0xc10
  public            Unity.Collections.FixedBytes16  offset3088  // 0xc20
  public            Unity.Collections.FixedBytes16  offset3104  // 0xc30
  public            Unity.Collections.FixedBytes16  offset3120  // 0xc40
  public            Unity.Collections.FixedBytes16  offset3136  // 0xc50
  public            Unity.Collections.FixedBytes16  offset3152  // 0xc60
  public            Unity.Collections.FixedBytes16  offset3168  // 0xc70
  public            Unity.Collections.FixedBytes16  offset3184  // 0xc80
  public            Unity.Collections.FixedBytes16  offset3200  // 0xc90
  public            Unity.Collections.FixedBytes16  offset3216  // 0xca0
  public            Unity.Collections.FixedBytes16  offset3232  // 0xcb0
  public            Unity.Collections.FixedBytes16  offset3248  // 0xcc0
  public            Unity.Collections.FixedBytes16  offset3264  // 0xcd0
  public            Unity.Collections.FixedBytes16  offset3280  // 0xce0
  public            Unity.Collections.FixedBytes16  offset3296  // 0xcf0
  public            Unity.Collections.FixedBytes16  offset3312  // 0xd00
  public            Unity.Collections.FixedBytes16  offset3328  // 0xd10
  public            Unity.Collections.FixedBytes16  offset3344  // 0xd20
  public            Unity.Collections.FixedBytes16  offset3360  // 0xd30
  public            Unity.Collections.FixedBytes16  offset3376  // 0xd40
  public            Unity.Collections.FixedBytes16  offset3392  // 0xd50
  public            Unity.Collections.FixedBytes16  offset3408  // 0xd60
  public            Unity.Collections.FixedBytes16  offset3424  // 0xd70
  public            Unity.Collections.FixedBytes16  offset3440  // 0xd80
  public            Unity.Collections.FixedBytes16  offset3456  // 0xd90
  public            Unity.Collections.FixedBytes16  offset3472  // 0xda0
  public            Unity.Collections.FixedBytes16  offset3488  // 0xdb0
  public            Unity.Collections.FixedBytes16  offset3504  // 0xdc0
  public            Unity.Collections.FixedBytes16  offset3520  // 0xdd0
  public            Unity.Collections.FixedBytes16  offset3536  // 0xde0
  public            Unity.Collections.FixedBytes16  offset3552  // 0xdf0
  public            Unity.Collections.FixedBytes16  offset3568  // 0xe00
  public            Unity.Collections.FixedBytes16  offset3584  // 0xe10
  public            Unity.Collections.FixedBytes16  offset3600  // 0xe20
  public            Unity.Collections.FixedBytes16  offset3616  // 0xe30
  public            Unity.Collections.FixedBytes16  offset3632  // 0xe40
  public            Unity.Collections.FixedBytes16  offset3648  // 0xe50
  public            Unity.Collections.FixedBytes16  offset3664  // 0xe60
  public            Unity.Collections.FixedBytes16  offset3680  // 0xe70
  public            Unity.Collections.FixedBytes16  offset3696  // 0xe80
  public            Unity.Collections.FixedBytes16  offset3712  // 0xe90
  public            Unity.Collections.FixedBytes16  offset3728  // 0xea0
  public            Unity.Collections.FixedBytes16  offset3744  // 0xeb0
  public            Unity.Collections.FixedBytes16  offset3760  // 0xec0
  public            Unity.Collections.FixedBytes16  offset3776  // 0xed0
  public            Unity.Collections.FixedBytes16  offset3792  // 0xee0
  public            Unity.Collections.FixedBytes16  offset3808  // 0xef0
  public            Unity.Collections.FixedBytes16  offset3824  // 0xf00
  public            Unity.Collections.FixedBytes16  offset3840  // 0xf10
  public            Unity.Collections.FixedBytes16  offset3856  // 0xf20
  public            Unity.Collections.FixedBytes16  offset3872  // 0xf30
  public            Unity.Collections.FixedBytes16  offset3888  // 0xf40
  public            Unity.Collections.FixedBytes16  offset3904  // 0xf50
  public            Unity.Collections.FixedBytes16  offset3920  // 0xf60
  public            Unity.Collections.FixedBytes16  offset3936  // 0xf70
  public            Unity.Collections.FixedBytes16  offset3952  // 0xf80
  public            Unity.Collections.FixedBytes16  offset3968  // 0xf90
  public            Unity.Collections.FixedBytes16  offset3984  // 0xfa0
  public            Unity.Collections.FixedBytes16  offset4000  // 0xfb0
  public            Unity.Collections.FixedBytes16  offset4016  // 0xfc0
  public            Unity.Collections.FixedBytes16  offset4032  // 0xfd0
  public            Unity.Collections.FixedBytes16  offset4048  // 0xfe0
  public            Unity.Collections.FixedBytes16  offset4064  // 0xff0
  public            System.Byte                     byte4080  // 0x1000
  public            System.Byte                     byte4081  // 0x1001
  public            System.Byte                     byte4082  // 0x1002
  public            System.Byte                     byte4083  // 0x1003
  public            System.Byte                     byte4084  // 0x1004
  public            System.Byte                     byte4085  // 0x1005
  public            System.Byte                     byte4086  // 0x1006
  public            System.Byte                     byte4087  // 0x1007
  public            System.Byte                     byte4088  // 0x1008
  public            System.Byte                     byte4089  // 0x1009
  public            System.Byte                     byte4090  // 0x100a
  public            System.Byte                     byte4091  // 0x100b
  public            System.Byte                     byte4092  // 0x100c
  public            System.Byte                     byte4093  // 0x100d
METHODS:
END_CLASS

CLASS: Unity.Collections.FixedString4096Bytes
TYPE:  sealed struct
TOKEN: 0x2000054
SIZE:  0x1010
IMPLEMENTS: Unity.Collections.INativeList`1 Unity.Collections.IIndexable`1 Unity.Collections.IUTF8Bytes System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1 System.IComparable`1 System.IEquatable`1
FIELDS:
  private           System.UInt16                   utf8LengthInBytes  // 0x10
  private           Unity.Collections.FixedBytes4094bytes  // 0x12
PROPERTIES:
  Length  get=0x04D87110
METHODS:
  RVA=0x04DAB500  token=0x600019F  System.Byte* GetUnsafePtr()
  RVA=0x0B1617CC  token=0x60001A1  System.Boolean TryResize(System.Int32 newLength, Unity.Collections.NativeArrayOptions clearOptions)
  RVA=0x0B161390  token=0x60001A2  System.Int32 CompareTo(System.String other)
  RVA=0x0B1613D8  token=0x60001A3  System.Boolean Equals(System.String other)
  RVA=0x0B1612F8  token=0x60001A4  System.Int32 CompareTo(Unity.Collections.FixedString32Bytes other)
  RVA=0x0B161948  token=0x60001A5  System.Boolean op_Equality(Unity.Collections.FixedString4096Bytes& a, Unity.Collections.FixedString32Bytes& b)
  RVA=0x0B16172C  token=0x60001A6  System.Boolean Equals(Unity.Collections.FixedString32Bytes other)
  RVA=0x0B161344  token=0x60001A7  System.Int32 CompareTo(Unity.Collections.FixedString64Bytes other)
  RVA=0x0B1618BC  token=0x60001A8  System.Boolean op_Equality(Unity.Collections.FixedString4096Bytes& a, Unity.Collections.FixedString64Bytes& b)
  RVA=0x0B161740  token=0x60001A9  System.Boolean Equals(Unity.Collections.FixedString64Bytes other)
  RVA=0x0B1612AC  token=0x60001AA  System.Int32 CompareTo(Unity.Collections.FixedString128Bytes other)
  RVA=0x0B1619D4  token=0x60001AB  System.Boolean op_Equality(Unity.Collections.FixedString4096Bytes& a, Unity.Collections.FixedString128Bytes& b)
  RVA=0x0B16145C  token=0x60001AC  System.Boolean Equals(Unity.Collections.FixedString128Bytes other)
  RVA=0x0B161214  token=0x60001AD  System.Int32 CompareTo(Unity.Collections.FixedString512Bytes other)
  RVA=0x0B161A60  token=0x60001AE  System.Boolean op_Equality(Unity.Collections.FixedString4096Bytes& a, Unity.Collections.FixedString512Bytes& b)
  RVA=0x0B161470  token=0x60001AF  System.Boolean Equals(Unity.Collections.FixedString512Bytes other)
  RVA=0x0B161260  token=0x60001B0  System.Int32 CompareTo(Unity.Collections.FixedString4096Bytes other)
  RVA=0x0B16183C  token=0x60001B1  System.Boolean op_Equality(Unity.Collections.FixedString4096Bytes& a, Unity.Collections.FixedString4096Bytes& b)
  RVA=0x0B1613C4  token=0x60001B2  System.Boolean Equals(Unity.Collections.FixedString4096Bytes other)
  RVA=0x0B161790  token=0x60001B3  System.String ToString()
  RVA=0x0B161754  token=0x60001B4  System.Int32 GetHashCode()
  RVA=0x0B161484  token=0x60001B5  System.Boolean Equals(System.Object obj)
END_CLASS

CLASS: Unity.Collections.FixedStringMethods
TYPE:  static class
TOKEN: 0x2000055
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60001B6  Unity.Collections.FormatError Append(T& fs, System.Byte* utf8Bytes, System.Int32 utf8BytesLength)
  RVA=-1  // generic def  token=0x60001B7  Unity.Collections.FormatError Append(T& fs, System.String s)
  RVA=-1  // generic def  token=0x60001B8  System.Int32 CompareTo(T& fs, System.Byte* bytes, System.Int32 bytesLen)
  RVA=-1  // generic def  token=0x60001B9  System.Int32 CompareTo(T& fs, T2& other)
  RVA=-1  // generic def  token=0x60001BA  System.String ConvertToString(T& fs)
  RVA=-1  // generic def  token=0x60001BB  System.Int32 ComputeHashCode(T& fs)
END_CLASS

CLASS: Unity.Collections.IUTF8Bytes
TYPE:  interface
TOKEN: 0x2000056
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001BC  System.Byte* GetUnsafePtr()
  RVA=-1  // abstract  token=0x60001BD  System.Boolean TryResize(System.Int32 newLength, Unity.Collections.NativeArrayOptions clearOptions)
END_CLASS

CLASS: Unity.Collections.ListExtensions
TYPE:  static class
TOKEN: 0x2000057
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60001BE  System.Boolean RemoveSwapBack(System.Collections.Generic.List<T> list, T value)
  RVA=-1  // generic def  token=0x60001BF  System.Void RemoveAtSwapBack(System.Collections.Generic.List<T> list, System.Int32 index)
  RVA=-1  // generic def  token=0x60001C0  Unity.Collections.NativeArray<T> ToNativeArray(System.Collections.Generic.List<T> list, Unity.Collections.AllocatorManager.AllocatorHandle allocator)
END_CLASS

CLASS: Unity.Collections.Memory
TYPE:  sealed struct
TOKEN: 0x2000058
SIZE:  0x11
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Collections.NativeArrayExtensions
TYPE:  static class
TOKEN: 0x200005B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60001C8  System.Boolean Contains(Unity.Collections.NativeList<T> list, U value)
  RVA=-1  // generic def  token=0x60001C9  System.Span<T> AsSpan(Unity.Collections.NativeArray<T> array)
  RVA=-1  // generic def  token=0x60001CA  System.Span<T> AsSpanThread(Unity.Collections.NativeArray<T> array)
  RVA=-1  // generic def  token=0x60001CB  System.Int32 IndexOf(System.Void* ptr, System.Int32 length, U value)
  RVA=-1  // generic def  token=0x60001CC  Unity.Collections.NativeArray<U> Reinterpret(Unity.Collections.NativeArray<T> array)
  RVA=-1  // generic def  token=0x60001CD  System.Void Initialize(Unity.Collections.NativeArray<T>& array, System.Int32 length, Unity.Collections.AllocatorManager.AllocatorHandle allocator, Unity.Collections.NativeArrayOptions options)
END_CLASS

CLASS: Unity.Collections.IIndexable`1
TYPE:  interface
TOKEN: 0x200005C
FIELDS:
PROPERTIES:
  Length  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Unity.Collections.INativeList`1
TYPE:  interface
TOKEN: 0x200005D
IMPLEMENTS: Unity.Collections.IIndexable`1
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Collections.NativeList`1
TYPE:  sealed struct
TOKEN: 0x200005E
IMPLEMENTS: Unity.Collections.INativeDisposable System.IDisposable Unity.Collections.INativeList`1 Unity.Collections.IIndexable`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeList<T>*m_ListData  // 0x0
  private           Unity.Collections.AllocatorManager.AllocatorHandlem_DeprecatedAllocator  // 0x0
PROPERTIES:
  Item  get=-1  // not resolved  set=-1  // not resolved
  Length  get=-1  // not resolved
  Capacity  get=-1  // not resolved  set=-1  // not resolved
  IsEmpty  get=-1  // not resolved
  IsCreated  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60001CF  System.Void .ctor(Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // not resolved  token=0x60001D0  System.Void .ctor(System.Int32 initialCapacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // generic def  token=0x60001D1  System.Void Initialize(System.Int32 initialCapacity, U& allocator, System.Int32 disposeSentinelStackDepth)
  RVA=-1  // not resolved  token=0x60001D2  System.Void .ctor(System.Int32 initialCapacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator, System.Int32 disposeSentinelStackDepth)
  RVA=-1  // not resolved  token=0x60001D5  T& ElementAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x60001D9  Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* GetUnsafeList()
  RVA=-1  // not resolved  token=0x60001DA  System.Void AddNoResize(T value)
  RVA=-1  // not resolved  token=0x60001DB  System.Void AddRangeNoResize(System.Void* ptr, System.Int32 count)
  RVA=-1  // not resolved  token=0x60001DC  System.Void Add(T& value)
  RVA=-1  // not resolved  token=0x60001DD  System.Void AddRange(Unity.Collections.NativeArray<T> array)
  RVA=-1  // not resolved  token=0x60001DE  System.Void AddRange(System.Void* ptr, System.Int32 count)
  RVA=-1  // not resolved  token=0x60001DF  System.Void RemoveAtSwapBack(System.Int32 index)
  RVA=-1  // not resolved  token=0x60001E0  System.Void RemoveAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x60001E1  System.Void RemoveRange(System.Int32 index, System.Int32 count)
  RVA=-1  // not resolved  token=0x60001E4  System.Void Dispose()
  RVA=-1  // not resolved  token=0x60001E5  Unity.Jobs.JobHandle Dispose(Unity.Jobs.JobHandle inputDeps)
  RVA=-1  // not resolved  token=0x60001E6  Unity.Jobs.JobHandle CrossFrameDispose(Unity.Jobs.JobHandle inputDeps, Unity.Jobs.LowLevel.Unsafe.JobQueuePriority priority)
  RVA=-1  // not resolved  token=0x60001E7  System.Void Clear()
  RVA=-1  // not resolved  token=0x60001E8  Unity.Collections.NativeArray<T> op_Implicit(Unity.Collections.NativeList<T> nativeList)
  RVA=-1  // not resolved  token=0x60001E9  Unity.Collections.NativeArray<T> AsArray()
  RVA=-1  // not resolved  token=0x60001EA  Unity.Collections.NativeArray<T> AsDeferredJobArray()
  RVA=-1  // not resolved  token=0x60001EB  T[] ToArray()
  RVA=-1  // not resolved  token=0x60001EC  Unity.Collections.NativeArray.Enumerator<T> GetEnumerator()
  RVA=-1  // not resolved  token=0x60001ED  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x60001EE  System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
  RVA=-1  // not resolved  token=0x60001EF  System.Void CopyFrom(Unity.Collections.NativeArray<T> array)
  RVA=-1  // not resolved  token=0x60001F0  System.Void Resize(System.Int32 length, Unity.Collections.NativeArrayOptions options)
  RVA=-1  // not resolved  token=0x60001F1  System.Void ResizeUninitialized(System.Int32 length)
  RVA=-1  // not resolved  token=0x60001F2  Unity.Collections.NativeArray.ReadOnly<T> AsParallelReader()
  RVA=-1  // not resolved  token=0x60001F3  Unity.Collections.NativeList.ParallelWriter<T> AsParallelWriter()
END_CLASS

CLASS: Unity.Collections.NativeListDispose
TYPE:  sealed struct
TOKEN: 0x2000060
SIZE:  0x18
FIELDS:
  public            Unity.Collections.LowLevel.Unsafe.UntypedUnsafeList*m_ListData  // 0x10
METHODS:
  RVA=0x0B163504  token=0x60001F6  System.Void Dispose()
END_CLASS

CLASS: Unity.Collections.NativeListDisposeJob
TYPE:  sealed struct
TOKEN: 0x2000061
SIZE:  0x18
IMPLEMENTS: Unity.Jobs.IJob
FIELDS:
  private           Unity.Collections.NativeListDisposeData  // 0x10
METHODS:
  RVA=0x0B1634FC  token=0x60001F7  System.Void Execute()
END_CLASS

CLASS: Unity.Collections.NativeListDebugView`1
TYPE:  sealed class
TOKEN: 0x2000062
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Collections.NativeParallelHashMap`2
TYPE:  sealed struct
TOKEN: 0x2000063
IMPLEMENTS: Unity.Collections.INativeDisposable System.IDisposable System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<TKey,TValue>m_HashMapData  // 0x0
PROPERTIES:
  IsEmpty  get=-1  // not resolved
  Capacity  get=-1  // not resolved  set=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
  IsCreated  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60001F8  System.Void .ctor(System.Int32 capacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // not resolved  token=0x60001F9  System.Void .ctor(System.Int32 capacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator, System.Int32 disposeSentinelStackDepth)
  RVA=-1  // not resolved  token=0x60001FB  System.Int32 Count()
  RVA=-1  // not resolved  token=0x60001FE  System.Void Clear()
  RVA=-1  // not resolved  token=0x60001FF  System.Boolean TryAdd(TKey key, TValue& item)
  RVA=-1  // not resolved  token=0x6000200  System.Void Add(TKey key, TValue item)
  RVA=-1  // not resolved  token=0x6000201  System.Boolean Remove(TKey key)
  RVA=-1  // not resolved  token=0x6000202  System.Boolean TryGetValue(TKey key, TValue& item)
  RVA=-1  // not resolved  token=0x6000203  System.Boolean ContainsKey(TKey key)
  RVA=-1  // not resolved  token=0x6000207  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000208  Unity.Jobs.JobHandle Dispose(Unity.Jobs.JobHandle inputDeps)
  RVA=-1  // not resolved  token=0x6000209  Unity.Collections.NativeArray<TKey> GetKeyArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // not resolved  token=0x600020A  Unity.Collections.NativeArray<TValue> GetValueArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // not resolved  token=0x600020B  Unity.Collections.NativeParallelHashMap.ParallelWriter<TKey,TValue> AsParallelWriter()
  RVA=-1  // not resolved  token=0x600020C  Unity.Collections.NativeParallelHashMap.Enumerator<TKey,TValue> GetEnumerator()
  RVA=-1  // not resolved  token=0x600020D  System.Collections.Generic.IEnumerator<Unity.Collections.LowLevel.Unsafe.KeyValue<TKey,TValue>> System.Collections.Generic.IEnumerable<Unity.Collections.LowLevel.Unsafe.KeyValue<TKey,TValue>>.GetEnumerator()
  RVA=-1  // not resolved  token=0x600020E  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Unity.Collections.NativeParallelHashMapDebuggerTypeProxy`2
TYPE:  sealed class
TOKEN: 0x2000066
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Collections.NativeParallelHashMapExtensions
TYPE:  static class
TOKEN: 0x2000067
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000215  Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<TKey,TValue> GetUnsafeMap(Unity.Collections.NativeParallelHashMap<TKey,TValue> container)
END_CLASS

CLASS: Unity.Collections.NativeParallelHashSet`1
TYPE:  sealed struct
TOKEN: 0x2000068
IMPLEMENTS: Unity.Collections.INativeDisposable System.IDisposable System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           Unity.Collections.NativeParallelHashMap<T,System.Boolean>m_Data  // 0x0
PROPERTIES:
  IsEmpty  get=-1  // not resolved
  IsCreated  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000216  System.Void .ctor(System.Int32 capacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // not resolved  token=0x6000218  System.Int32 Count()
  RVA=-1  // not resolved  token=0x600021A  System.Void Dispose()
  RVA=-1  // not resolved  token=0x600021B  System.Void Clear()
  RVA=-1  // not resolved  token=0x600021C  System.Boolean Add(T item)
  RVA=-1  // not resolved  token=0x600021D  System.Boolean Remove(T item)
  RVA=-1  // not resolved  token=0x600021E  System.Boolean Contains(T item)
  RVA=-1  // not resolved  token=0x600021F  Unity.Collections.NativeArray<T> ToNativeArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // not resolved  token=0x6000220  Unity.Collections.NativeParallelHashSet.Enumerator<T> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000221  System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000222  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Unity.Collections.NativeHashSetDebuggerTypeProxy`1
TYPE:  sealed class
TOKEN: 0x200006A
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Collections.NativeParallelMultiHashMapIterator`1
TYPE:  sealed struct
TOKEN: 0x200006B
FIELDS:
  private           TKey                            key  // 0x0
  private           System.Int32                    NextEntryIndex  // 0x0
  private           System.Int32                    EntryIndex  // 0x0
METHODS:
END_CLASS

CLASS: Unity.Collections.NativeParallelMultiHashMap`2
TYPE:  sealed struct
TOKEN: 0x200006C
IMPLEMENTS: Unity.Collections.INativeDisposable System.IDisposable System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap<TKey,TValue>m_MultiHashMapData  // 0x0
PROPERTIES:
  IsEmpty  get=-1  // not resolved
  Capacity  get=-1  // not resolved
  IsCreated  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000228  System.Void .ctor(System.Int32 capacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // generic def  token=0x6000229  System.Void Initialize(System.Int32 capacity, U& allocator, System.Int32 disposeSentinelStackDepth)
  RVA=-1  // not resolved  token=0x600022A  System.Void .ctor(System.Int32 capacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator, System.Int32 disposeSentinelStackDepth)
  RVA=-1  // not resolved  token=0x600022C  System.Int32 Count()
  RVA=-1  // not resolved  token=0x600022E  System.Void Clear()
  RVA=-1  // not resolved  token=0x600022F  System.Void Add(TKey key, TValue item)
  RVA=-1  // not resolved  token=0x6000230  System.Int32 Remove(TKey key)
  RVA=-1  // not resolved  token=0x6000231  System.Void Remove(Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it)
  RVA=-1  // not resolved  token=0x6000232  System.Boolean TryGetFirstValue(TKey key, TValue& item, Unity.Collections.NativeParallelMultiHashMapIterator<TKey>& it)
  RVA=-1  // not resolved  token=0x6000233  System.Boolean TryGetNextValue(TValue& item, Unity.Collections.NativeParallelMultiHashMapIterator<TKey>& it)
  RVA=-1  // not resolved  token=0x6000234  System.Boolean ContainsKey(TKey key)
  RVA=-1  // not resolved  token=0x6000235  System.Int32 CountValuesForKey(TKey key)
  RVA=-1  // not resolved  token=0x6000236  System.Boolean SetValue(TValue item, Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it)
  RVA=-1  // not resolved  token=0x6000238  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000239  Unity.Collections.NativeArray<TKey> GetKeyArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // not resolved  token=0x600023A  Unity.Collections.NativeArray<TValue> GetValueArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // not resolved  token=0x600023B  Unity.Collections.NativeParallelMultiHashMap.ParallelWriter<TKey,TValue> AsParallelWriter()
  RVA=-1  // not resolved  token=0x600023C  Unity.Collections.NativeParallelMultiHashMap.Enumerator<TKey,TValue> GetValuesForKey(TKey key)
  RVA=-1  // not resolved  token=0x600023D  System.Collections.Generic.IEnumerator<Unity.Collections.LowLevel.Unsafe.KeyValue<TKey,TValue>> System.Collections.Generic.IEnumerable<Unity.Collections.LowLevel.Unsafe.KeyValue<TKey,TValue>>.GetEnumerator()
  RVA=-1  // not resolved  token=0x600023E  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Unity.Collections.NativeParallelMultiHashMapDebuggerTypeProxy`2
TYPE:  sealed class
TOKEN: 0x200006F
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Collections.NativeQueueBlockHeader
TYPE:  sealed struct
TOKEN: 0x2000070
SIZE:  0x20
FIELDS:
  public            Unity.Collections.NativeQueueBlockHeader*m_NextBlock  // 0x10
  public            System.Int32                    m_NumItems  // 0x18
METHODS:
END_CLASS

CLASS: Unity.Collections.NativeQueueBlockPoolData
TYPE:  sealed struct
TOKEN: 0x2000071
SIZE:  0x28
FIELDS:
  private           System.IntPtr                   m_FirstBlock  // 0x10
  private           System.Int32                    m_NumBlocks  // 0x18
  private           System.Int32                    m_MaxBlocks  // 0x1c
  private           System.Int32                    m_AllocLock  // 0x20
METHODS:
  RVA=0x03FA3AD0  token=0x6000246  Unity.Collections.NativeQueueBlockHeader* AllocateBlock()
  RVA=0x03EE1D30  token=0x6000247  System.Void FreeBlock(Unity.Collections.NativeQueueBlockHeader* block)
END_CLASS

CLASS: Unity.Collections.NativeQueueBlockPool
TYPE:  class
TOKEN: 0x2000072
SIZE:  0x10
FIELDS:
  private   static readonly Unity.Burst.SharedStatic<System.IntPtr>Data  // static @ 0x0
METHODS:
  RVA=0x03FE6EC0  token=0x6000248  Unity.Collections.NativeQueueBlockPoolData* GetQueueBlockPool()
  RVA=0x03B0D570  token=0x6000249  System.Void AppDomainOnDomainUnload()
  RVA=0x0B163540  token=0x600024A  System.Void OnDomainUnload(System.Object sender, System.EventArgs e)
  RVA=0x0452F380  token=0x600024B  System.Void .cctor()
END_CLASS

CLASS: Unity.Collections.NativeQueueData
TYPE:  sealed struct
TOKEN: 0x2000073
SIZE:  0x30
FIELDS:
  public            System.IntPtr                   m_FirstBlock  // 0x10
  public            System.IntPtr                   m_LastBlock  // 0x18
  public            System.Int32                    m_MaxItems  // 0x20
  public            System.Int32                    m_CurrentRead  // 0x24
  public            System.Byte*                    m_CurrentWriteBlockTLS  // 0x28
METHODS:
  RVA=0x04DAB520  token=0x600024C  Unity.Collections.NativeQueueBlockHeader* GetCurrentWriteBlockTLS(System.Int32 threadIndex)
  RVA=0x04DAB530  token=0x600024D  System.Void SetCurrentWriteBlockTLS(System.Int32 threadIndex, Unity.Collections.NativeQueueBlockHeader* currentWriteBlock)
  RVA=-1  // generic def  token=0x600024E  Unity.Collections.NativeQueueBlockHeader* AllocateWriteBlockMT(Unity.Collections.NativeQueueData* data, Unity.Collections.NativeQueueBlockPoolData* pool, System.Int32 threadIndex)
  RVA=-1  // generic def  token=0x600024F  System.Void AllocateQueue(Unity.Collections.AllocatorManager.AllocatorHandle label, Unity.Collections.NativeQueueData*& outBuf)
  RVA=0x041649C0  token=0x6000250  System.Void DeallocateQueue(Unity.Collections.NativeQueueData* data, Unity.Collections.NativeQueueBlockPoolData* pool, Unity.Collections.AllocatorManager.AllocatorHandle allocation)
END_CLASS

CLASS: Unity.Collections.NativeQueue`1
TYPE:  sealed struct
TOKEN: 0x2000074
IMPLEMENTS: Unity.Collections.INativeDisposable System.IDisposable
FIELDS:
  private           Unity.Collections.NativeQueueData*m_Buffer  // 0x0
  private           Unity.Collections.NativeQueueBlockPoolData*m_QueuePool  // 0x0
  private           Unity.Collections.AllocatorManager.AllocatorHandlem_AllocatorLabel  // 0x0
PROPERTIES:
  Count  get=-1  // not resolved
  IsCreated  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000251  System.Void .ctor(Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // not resolved  token=0x6000252  System.Boolean IsEmpty()
  RVA=-1  // not resolved  token=0x6000254  System.Void Enqueue(T value)
  RVA=-1  // not resolved  token=0x6000255  System.Boolean TryDequeue(T& item)
  RVA=-1  // not resolved  token=0x6000256  Unity.Collections.NativeArray<T> ToArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // not resolved  token=0x6000257  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000259  System.Void Dispose()
  RVA=-1  // not resolved  token=0x600025A  Unity.Jobs.JobHandle Dispose(Unity.Jobs.JobHandle inputDeps)
  RVA=-1  // not resolved  token=0x600025B  Unity.Collections.NativeQueue.ParallelWriter<T> AsParallelWriter()
END_CLASS

CLASS: Unity.Collections.NativeQueueDispose
TYPE:  sealed struct
TOKEN: 0x2000076
SIZE:  0x28
FIELDS:
  private           Unity.Collections.NativeQueueData*m_Buffer  // 0x10
  private           Unity.Collections.NativeQueueBlockPoolData*m_QueuePool  // 0x18
  private           Unity.Collections.AllocatorManager.AllocatorHandlem_AllocatorLabel  // 0x20
METHODS:
  RVA=0x0B16361C  token=0x600025D  System.Void Dispose()
END_CLASS

CLASS: Unity.Collections.NativeQueueDisposeJob
TYPE:  sealed struct
TOKEN: 0x2000077
SIZE:  0x28
IMPLEMENTS: Unity.Jobs.IJob
FIELDS:
  public            Unity.Collections.NativeQueueDisposeData  // 0x10
METHODS:
  RVA=0x0B163614  token=0x600025E  System.Void Execute()
END_CLASS

CLASS: Unity.Collections.NativeReference`1
TYPE:  sealed struct
TOKEN: 0x2000078
IMPLEMENTS: Unity.Collections.INativeDisposable System.IDisposable System.IEquatable`1
FIELDS:
  private           System.Void*                    m_Data  // 0x0
  private           Unity.Collections.AllocatorManager.AllocatorHandlem_AllocatorLabel  // 0x0
PROPERTIES:
  Value  get=-1  // not resolved  set=-1  // not resolved
  IsCreated  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600025F  System.Void .ctor(Unity.Collections.AllocatorManager.AllocatorHandle allocator, Unity.Collections.NativeArrayOptions options)
  RVA=-1  // not resolved  token=0x6000260  System.Void .ctor(T value, Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // not resolved  token=0x6000261  System.Void Allocate(Unity.Collections.AllocatorManager.AllocatorHandle allocator, Unity.Collections.NativeReference<T>& reference)
  RVA=-1  // not resolved  token=0x6000265  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000266  System.Boolean Equals(Unity.Collections.NativeReference<T> other)
  RVA=-1  // not resolved  token=0x6000267  System.Boolean Equals(System.Object obj)
  RVA=-1  // not resolved  token=0x6000268  System.Int32 GetHashCode()
END_CLASS

CLASS: Unity.Collections.NativeReferenceDispose
TYPE:  sealed struct
TOKEN: 0x2000079
SIZE:  0x20
FIELDS:
  private           System.Void*                    m_Data  // 0x10
  private           Unity.Collections.AllocatorManager.AllocatorHandlem_AllocatorLabel  // 0x18
METHODS:
  RVA=0x0B163638  token=0x6000269  System.Void Dispose()
END_CLASS

CLASS: Unity.Collections.NativeReferenceDisposeJob
TYPE:  sealed struct
TOKEN: 0x200007A
SIZE:  0x20
IMPLEMENTS: Unity.Jobs.IJob
FIELDS:
  private           Unity.Collections.NativeReferenceDisposeData  // 0x10
METHODS:
  RVA=0x0B163630  token=0x600026A  System.Void Execute()
END_CLASS

CLASS: Unity.Collections.NativeSortExtension
TYPE:  static class
TOKEN: 0x200007B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x600026B  System.Void Sort(T* array, System.Int32 length)
  RVA=-1  // generic def  token=0x600026C  System.Void Sort(T* array, System.Int32 length, U comp)
  RVA=-1  // generic def  token=0x600026D  Unity.Collections.SortJob<T,U> SortJob(T* array, System.Int32 length, U comp)
  RVA=-1  // generic def  token=0x600026E  System.Int32 BinarySearch(T* ptr, System.Int32 length, T value)
  RVA=-1  // generic def  token=0x600026F  System.Int32 BinarySearch(T* ptr, System.Int32 length, T value, U comp)
  RVA=-1  // generic def  token=0x6000270  Unity.Collections.SortJob<T,Unity.Collections.NativeSortExtension.DefaultComparer<T>> SortJob(Unity.Collections.NativeArray<T> array)
  RVA=-1  // generic def  token=0x6000271  System.Void Sort(Unity.Collections.NativeList<T> list)
  RVA=-1  // generic def  token=0x6000272  System.Void Sort(Unity.Collections.NativeList<T> list, U comp)
  RVA=-1  // generic def  token=0x6000273  System.Void IntroSort(System.Void* array, System.Int32 length, U comp)
  RVA=-1  // generic def  token=0x6000274  System.Void IntroSort(System.Void* array, System.Int32 lo, System.Int32 hi, System.Int32 depth, U comp)
  RVA=-1  // generic def  token=0x6000275  System.Void InsertionSort(System.Void* array, System.Int32 lo, System.Int32 hi, U comp)
  RVA=-1  // generic def  token=0x6000276  System.Int32 Partition(System.Void* array, System.Int32 lo, System.Int32 hi, U comp)
  RVA=-1  // generic def  token=0x6000277  System.Void HeapSort(System.Void* array, System.Int32 lo, System.Int32 hi, U comp)
  RVA=-1  // generic def  token=0x6000278  System.Void Heapify(System.Void* array, System.Int32 i, System.Int32 n, System.Int32 lo, U comp)
  RVA=-1  // generic def  token=0x6000279  System.Void Swap(System.Void* array, System.Int32 lhs, System.Int32 rhs)
  RVA=-1  // generic def  token=0x600027A  System.Void SwapIfGreaterWithItems(System.Void* array, System.Int32 lhs, System.Int32 rhs, U comp)
END_CLASS

CLASS: Unity.Collections.SortJob`2
TYPE:  sealed struct
TOKEN: 0x200007D
FIELDS:
  public            T*                              Data  // 0x0
  public            U                               Comp  // 0x0
  public            System.Int32                    Length  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600027C  Unity.Jobs.JobHandle Schedule(Unity.Jobs.JobHandle inputDeps)
END_CLASS

CLASS: Unity.Collections.NativeStream
TYPE:  sealed struct
TOKEN: 0x2000080
SIZE:  0x20
IMPLEMENTS: System.IDisposable
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeStreamm_Stream  // 0x10
METHODS:
  RVA=0x0B160058  token=0x600027F  System.Void Dispose()
  RVA=0x0B163648  token=0x6000280  System.Void AllocateForEach(System.Int32 forEachCount)
END_CLASS

CLASS: Unity.Collections.Spinner
TYPE:  sealed struct
TOKEN: 0x2000083
SIZE:  0x14
FIELDS:
  private           System.Int32                    m_value  // 0x10
METHODS:
  RVA=0x0B163CB4  token=0x6000283  System.Void Lock()
  RVA=0x0B163CC8  token=0x6000284  System.Void Unlock()
END_CLASS

CLASS: Unity.Collections.UnmanagedArray`1
TYPE:  sealed struct
TOKEN: 0x2000084
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.IntPtr                   m_pointer  // 0x0
  private           System.Int32                    m_length  // 0x0
  private           Unity.Collections.AllocatorManager.AllocatorHandlem_allocator  // 0x0
PROPERTIES:
  Item  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000285  System.Void Dispose()
END_CLASS

CLASS: Unity.Collections.Try_000008B6$PostfixBurstDelegate
TYPE:  sealed class
TOKEN: 0x2000087
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B163E34  token=0x6000292  System.Void .ctor(System.Object , System.IntPtr )
  RVA=0x015802E0  token=0x6000293  System.Int32 Invoke(System.IntPtr state, Unity.Collections.AllocatorManager.Block& block)
END_CLASS

CLASS: Unity.Collections.Try_000008B6$BurstDirectCall
TYPE:  static class
TOKEN: 0x2000088
SIZE:  0x10
FIELDS:
  private   static  System.IntPtr                   Pointer  // static @ 0x0
  private   static  System.IntPtr                   DeferredCompilation  // static @ 0x8
METHODS:
  RVA=0x0B1642C4  token=0x6000294  System.Void GetFunctionPointerDiscard(System.IntPtr& )
  RVA=0x0B1643C8  token=0x6000295  System.IntPtr GetFunctionPointer()
  RVA=0x03FB0870  token=0x6000296  System.Void Constructor()
  RVA=0x041E1670  token=0x6000297  System.Void Initialize()
  RVA=0x03FAEA70  token=0x6000298  System.Void .cctor()
  RVA=0x0B16441C  token=0x6000299  System.Int32 Invoke(System.IntPtr state, Unity.Collections.AllocatorManager.Block& block)
END_CLASS

CLASS: Unity.Collections.RewindableAllocator
TYPE:  sealed struct
TOKEN: 0x2000085
SIZE:  0x38
IMPLEMENTS: IAllocator System.IDisposable
FIELDS:
  private           Unity.Collections.Spinner       m_spinner  // 0x10
  private           Unity.Collections.AllocatorManager.AllocatorHandlem_handle  // 0x14
  private           Unity.Collections.UnmanagedArray<Unity.Collections.RewindableAllocator.MemoryBlock>m_block  // 0x18
  private           System.Int32                    m_best  // 0x28
  private           System.Int32                    m_last  // 0x2c
  private           System.Int32                    m_used  // 0x30
  private           System.Boolean                  m_enableBlockFree  // 0x34
PROPERTIES:
  Handle  get=0x04D88B20
METHODS:
  RVA=0x0B163754  token=0x6000287  System.Void Rewind()
  RVA=0x0B163688  token=0x6000288  System.Void Dispose()
  RVA=0x0B163840  token=0x6000289  System.Int32 Try(Unity.Collections.AllocatorManager.Block& block)
  RVA=0x0B163A08  token=0x600028A  System.Int32 Try(System.IntPtr state, Unity.Collections.AllocatorManager.Block& block)
  RVA=0x0B163838  token=0x600028C  System.Int32 Try$BurstManaged(System.IntPtr state, Unity.Collections.AllocatorManager.Block& block)
END_CLASS

CLASS: Unity.Collections.FormatError
TYPE:  sealed struct
TOKEN: 0x2000089
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Unity.Collections.FormatError   None  // const
  public    static  Unity.Collections.FormatError   Overflow  // const
METHODS:
END_CLASS

CLASS: Unity.Collections.CopyError
TYPE:  sealed struct
TOKEN: 0x200008A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Unity.Collections.CopyError     None  // const
  public    static  Unity.Collections.CopyError     Truncation  // const
METHODS:
END_CLASS

CLASS: Unity.Collections.ConversionError
TYPE:  sealed struct
TOKEN: 0x200008B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Unity.Collections.ConversionErrorNone  // const
  public    static  Unity.Collections.ConversionErrorOverflow  // const
  public    static  Unity.Collections.ConversionErrorEncoding  // const
  public    static  Unity.Collections.ConversionErrorCodePoint  // const
METHODS:
END_CLASS

CLASS: Unity.Collections.Unicode
TYPE:  sealed struct
TOKEN: 0x200008C
SIZE:  0x11
FIELDS:
PROPERTIES:
  ReplacementCharacter  get=0x04DAB550
METHODS:
  RVA=0x03875EB0  token=0x600029A  System.Boolean IsValidCodePoint(System.Int32 codepoint)
  RVA=0x0B164628  token=0x600029B  System.Boolean NotTrailer(System.Byte b)
  RVA=0x03875E20  token=0x600029D  Unity.Collections.ConversionError Utf8ToUcs(Unity.Collections.Unicode.Rune& rune, System.Byte* buffer, System.Int32& index, System.Int32 capacity)
  RVA=0x03875EC0  token=0x600029E  System.Boolean IsLeadingSurrogate(System.Char c)
  RVA=0x0A7E1B0C  token=0x600029F  System.Boolean IsTrailingSurrogate(System.Char c)
  RVA=0x03875CE0  token=0x60002A0  Unity.Collections.ConversionError Utf16ToUcs(Unity.Collections.Unicode.Rune& rune, System.Char* buffer, System.Int32& index, System.Int32 capacity)
  RVA=0x03875CA0  token=0x60002A1  Unity.Collections.ConversionError UcsToUtf8(System.Byte* buffer, System.Int32& index, System.Int32 capacity, Unity.Collections.Unicode.Rune rune)
  RVA=0x03875E60  token=0x60002A2  Unity.Collections.ConversionError UcsToUtf16(System.Char* buffer, System.Int32& index, System.Int32 capacity, Unity.Collections.Unicode.Rune rune)
  RVA=0x03875BE0  token=0x60002A3  Unity.Collections.ConversionError Utf16ToUtf8(System.Char* utf16Buffer, System.Int32 utf16Length, System.Byte* utf8Buffer, System.Int32& utf8Length, System.Int32 utf8Capacity)
  RVA=0x03875D60  token=0x60002A4  Unity.Collections.ConversionError Utf8ToUtf16(System.Byte* utf8Buffer, System.Int32 utf8Length, System.Char* utf16Buffer, System.Int32& utf16Length, System.Int32 utf16Capacity)
END_CLASS

CLASS: Unity.Collections.UTF8ArrayUnsafeUtility
TYPE:  static class
TOKEN: 0x200008E
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B1644A8  token=0x60002A5  Unity.Collections.CopyError Copy(System.Byte* dest, System.Int32& destLength, System.Int32 destUTF8MaxLengthInBytes, System.Char* src, System.Int32 srcLength)
  RVA=0x03875B10  token=0x60002A6  Unity.Collections.CopyError Copy(System.Byte* dest, System.UInt16& destLength, System.UInt16 destUTF8MaxLengthInBytes, System.Char* src, System.Int32 srcLength)
  RVA=0x0B164590  token=0x60002A7  System.Int32 StrCmp(System.Byte* utf8BufferA, System.Int32 utf8LengthInBytesA, System.Byte* utf8BufferB, System.Int32 utf8LengthInBytesB)
  RVA=0x0B1644DC  token=0x60002A8  System.Boolean EqualsUTF8Bytes(System.Byte* aBytes, System.Int32 aLength, System.Byte* bBytes, System.Int32 bLength)
  RVA=0x0B1644F8  token=0x60002A9  System.Int32 StrCmp(System.Byte* utf8Buffer, System.Int32 utf8LengthInBytes, System.Char* utf16Buffer, System.Int32 utf16LengthInChars)
END_CLASS

CLASS: Unity.Collections.Hash64Long_000008F7$PostfixBurstDelegate
TYPE:  sealed class
TOKEN: 0x2000092
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B163218  token=0x60002DC  System.Void .ctor(System.Object , System.IntPtr )
  RVA=0x014D0D00  token=0x60002DD  System.UInt64 Invoke(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret)
END_CLASS

CLASS: Unity.Collections.Hash64Long_000008F7$BurstDirectCall
TYPE:  static class
TOKEN: 0x2000093
SIZE:  0x10
FIELDS:
  private   static  System.IntPtr                   Pointer  // static @ 0x0
  private   static  System.IntPtr                   DeferredCompilation  // static @ 0x8
METHODS:
  RVA=0x0B163010  token=0x60002DE  System.Void GetFunctionPointerDiscard(System.IntPtr& )
  RVA=0x0B163114  token=0x60002DF  System.IntPtr GetFunctionPointer()
  RVA=0x03FB0900  token=0x60002E0  System.Void Constructor()
  RVA=0x041E1670  token=0x60002E1  System.Void Initialize()
  RVA=0x03FAEA80  token=0x60002E2  System.Void .cctor()
  RVA=0x0B163168  token=0x60002E3  System.UInt64 Invoke(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret)
END_CLASS

CLASS: Unity.Collections.Hash128Long_000008FE$PostfixBurstDelegate
TYPE:  sealed class
TOKEN: 0x2000094
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B162F3C  token=0x60002E4  System.Void .ctor(System.Object , System.IntPtr )
  RVA=0x014DD080  token=0x60002E5  System.Void Invoke(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret, Unity.Mathematics.uint4& result)
END_CLASS

CLASS: Unity.Collections.Hash128Long_000008FE$BurstDirectCall
TYPE:  static class
TOKEN: 0x2000095
SIZE:  0x10
FIELDS:
  private   static  System.IntPtr                   Pointer  // static @ 0x0
  private   static  System.IntPtr                   DeferredCompilation  // static @ 0x8
METHODS:
  RVA=0x0B162D20  token=0x60002E6  System.Void GetFunctionPointerDiscard(System.IntPtr& )
  RVA=0x0B162E24  token=0x60002E7  System.IntPtr GetFunctionPointer()
  RVA=0x03FB0990  token=0x60002E8  System.Void Constructor()
  RVA=0x041E1670  token=0x60002E9  System.Void Initialize()
  RVA=0x03FAEA90  token=0x60002EA  System.Void .cctor()
  RVA=0x0B162E78  token=0x60002EB  System.Void Invoke(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret, Unity.Mathematics.uint4& result)
END_CLASS

CLASS: Unity.Collections.xxHash3
TYPE:  static class
TOKEN: 0x2000090
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04D83230  token=0x60002AB  System.Void Avx2HashLongInternalLoop(System.UInt64* acc, System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret, System.Int32 isHash64)
  RVA=0x04D83230  token=0x60002AC  System.Void Avx2ScrambleAcc(System.UInt64* acc, System.Byte* secret)
  RVA=0x04D83230  token=0x60002AD  System.Void Avx2Accumulate(System.UInt64* acc, System.Byte* input, System.Byte* dest, System.Byte* secret, System.Int64 nbStripes, System.Int32 isHash64)
  RVA=0x04D83230  token=0x60002AE  System.Void Avx2Accumulate512(System.UInt64* acc, System.Byte* input, System.Byte* dest, System.Byte* secret)
  RVA=0x02F835D0  token=0x60002AF  Unity.Mathematics.uint2 Hash64(System.Void* input, System.Int64 length)
  RVA=0x0B1653CC  token=0x60002B0  Unity.Mathematics.uint4 Hash128(System.Void* input, System.Int64 length)
  RVA=0x02F838B0  token=0x60002B1  System.UInt64 Hash64Internal(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret, System.UInt64 seed)
  RVA=0x0B164B90  token=0x60002B2  System.Void Hash128Internal(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret, System.UInt64 seed, Unity.Mathematics.uint4& result)
  RVA=0x040F2920  token=0x60002B3  System.UInt64 Hash64Len1To3(System.Byte* input, System.Int64 len, System.Byte* secret, System.UInt64 seed)
  RVA=0x02F84BC0  token=0x60002B4  System.UInt64 Hash64Len4To8(System.Byte* input, System.Int64 length, System.Byte* secret, System.UInt64 seed)
  RVA=0x03F6B640  token=0x60002B5  System.UInt64 Hash64Len9To16(System.Byte* input, System.Int64 length, System.Byte* secret, System.UInt64 seed)
  RVA=0x02F84B10  token=0x60002B6  System.UInt64 Hash64Len0To16(System.Byte* input, System.Int64 length, System.Byte* secret, System.UInt64 seed)
  RVA=0x02F84CB0  token=0x60002B7  System.UInt64 Hash64Len17To128(System.Byte* input, System.Int64 length, System.Byte* secret, System.UInt64 seed)
  RVA=0x0461E140  token=0x60002B8  System.UInt64 Hash64Len129To240(System.Byte* input, System.Int64 length, System.Byte* secret, System.UInt64 seed)
  RVA=0x0B165470  token=0x60002B9  System.UInt64 Hash64Long(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret)
  RVA=0x0B165114  token=0x60002BA  System.Void Hash128Len1To3(System.Byte* input, System.Int64 length, System.Byte* secret, System.UInt64 seed, Unity.Mathematics.uint4& result)
  RVA=0x0B1651AC  token=0x60002BB  System.Void Hash128Len4To8(System.Byte* input, System.Int64 len, System.Byte* secret, System.UInt64 seed, Unity.Mathematics.uint4& result)
  RVA=0x0B16525C  token=0x60002BC  System.Void Hash128Len9To16(System.Byte* input, System.Int64 len, System.Byte* secret, System.UInt64 seed, Unity.Mathematics.uint4& result)
  RVA=0x0B164CD8  token=0x60002BD  System.Void Hash128Len0To16(System.Byte* input, System.Int64 length, System.Byte* secret, System.UInt64 seed, Unity.Mathematics.uint4& result)
  RVA=0x0B164F84  token=0x60002BE  System.Void Hash128Len17To128(System.Byte* input, System.Int64 length, System.Byte* secret, System.UInt64 seed, Unity.Mathematics.uint4& result)
  RVA=0x0B164D7C  token=0x60002BF  System.Void Hash128Len129To240(System.Byte* input, System.Int64 length, System.Byte* secret, System.UInt64 seed, Unity.Mathematics.uint4& result)
  RVA=0x0B16534C  token=0x60002C0  System.Void Hash128Long(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret, Unity.Mathematics.uint4& result)
  RVA=0x04DAB5F0  token=0x60002C1  Unity.Mathematics.uint2 ToUint2(System.UInt64 u)
  RVA=0x04DAB610  token=0x60002C2  Unity.Mathematics.uint4 ToUint4(System.UInt64 ul0, System.UInt64 ul1)
  RVA=0x0B164B60  token=0x60002C3  System.Void EncodeSecretKey(System.Byte* dst, System.Byte* secret, System.UInt64 seed)
  RVA=0x02B76770  token=0x60002C4  System.UInt64 Read64LE(System.Void* addr)
  RVA=0x02B77620  token=0x60002C5  System.UInt32 Read32LE(System.Void* addr)
  RVA=0x04D88B60  token=0x60002C6  System.Void Write64LE(System.Void* addr, System.UInt64 value)
  RVA=0x04DA5160  token=0x60002C7  System.UInt64 Mul32To64(System.UInt32 x, System.UInt32 y)
  RVA=0x04DAB5E0  token=0x60002C8  System.UInt64 Swap64(System.UInt64 x)
  RVA=0x04DA4CD0  token=0x60002C9  System.UInt32 Swap32(System.UInt32 x)
  RVA=0x04D882E0  token=0x60002CA  System.UInt32 RotL32(System.UInt32 x, System.Int32 r)
  RVA=0x04DAB5C0  token=0x60002CB  System.UInt64 RotL64(System.UInt64 x, System.Int32 r)
  RVA=0x04DAB630  token=0x60002CC  System.UInt64 XorShift64(System.UInt64 v64, System.Int32 shift)
  RVA=0x0B16563C  token=0x60002CD  System.UInt64 Mul128Fold64(System.UInt64 lhs, System.UInt64 rhs)
  RVA=0x0B165570  token=0x60002CE  System.UInt64 Mix16(System.Byte* input, System.Byte* secret, System.UInt64 seed)
  RVA=0x0B16559C  token=0x60002CF  Unity.Collections.xxHash3.ulong2 Mix32(Unity.Collections.xxHash3.ulong2 acc, System.Byte* input_1, System.Byte* input_2, System.Byte* secret, System.UInt64 seed)
  RVA=0x04DAB590  token=0x60002D0  System.UInt64 Avalanche(System.UInt64 h64)
  RVA=0x040F2960  token=0x60002D1  System.UInt64 AvalancheH64(System.UInt64 h64)
  RVA=0x04DAB650  token=0x60002D2  System.UInt64 rrmxmx(System.UInt64 h64, System.UInt64 length)
  RVA=0x0B165590  token=0x60002D3  System.UInt64 Mix2Acc(System.UInt64 acc0, System.UInt64 acc1, System.Byte* secret)
  RVA=0x0B1654E4  token=0x60002D4  System.UInt64 MergeAcc(System.UInt64* acc, System.Byte* secret, System.UInt64 start)
  RVA=0x0B1649BC  token=0x60002D5  System.Void DefaultHashLongInternalLoop(System.UInt64* acc, System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret, System.Int32 isHash64)
  RVA=0x0B164918  token=0x60002D6  System.Void DefaultAccumulate(System.UInt64* acc, System.Byte* input, System.Byte* dest, System.Byte* secret, System.Int64 nbStripes, System.Int32 isHash64)
  RVA=0x0B164898  token=0x60002D7  System.Void DefaultAccumulate512(System.UInt64* acc, System.Byte* input, System.Byte* dest, System.Byte* secret, System.Int32 isHash64)
  RVA=0x0B164B30  token=0x60002D8  System.Void DefaultScrambleAcc(System.UInt64* acc, System.Byte* secret)
  RVA=0x0B15FA9C  token=0x60002D9  System.UInt64 Hash64Long$BurstManaged(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret)
  RVA=0x0B15F960  token=0x60002DA  System.Void Hash128Long$BurstManaged(System.Byte* input, System.Byte* dest, System.Int64 length, System.Byte* secret, Unity.Mathematics.uint4& result)
END_CLASS

CLASS: Unity.Collections.xxHashDefaultKey
TYPE:  static class
TOKEN: 0x2000096
SIZE:  0x10
FIELDS:
  public    static readonly System.Byte[]                   kSecret  // static @ 0x0
METHODS:
  RVA=0x04D164D0  token=0x60002EC  System.Void .cctor()
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.NativeListUnsafeUtility
TYPE:  static class
TOKEN: 0x2000098
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60002EE  System.Void* GetUnsafePtr(Unity.Collections.NativeList<T> list)
  RVA=-1  // generic def  token=0x60002EF  System.Void* GetUnsafeReadOnlyPtr(Unity.Collections.NativeList<T> list)
  RVA=-1  // generic def  token=0x60002F0  System.Void* GetInternalListDataPtrUnchecked(Unity.Collections.NativeList<T>& list)
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.NativeReferenceUnsafeUtility
TYPE:  static class
TOKEN: 0x2000099
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60002F1  System.Void* GetUnsafePtr(Unity.Collections.NativeReference<T> reference)
  RVA=-1  // generic def  token=0x60002F2  System.Void* GetUnsafePtrWithoutChecks(Unity.Collections.NativeReference<T> reference)
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeAppendBuffer
TYPE:  sealed struct
TOKEN: 0x200009A
SIZE:  0x28
IMPLEMENTS: Unity.Collections.INativeDisposable System.IDisposable
FIELDS:
  public            System.Byte*                    Ptr  // 0x10
  public            System.Int32                    Length  // 0x18
  public            System.Int32                    Capacity  // 0x1c
  public            Unity.Collections.AllocatorManager.AllocatorHandleAllocator  // 0x20
  public    readonly System.Int32                    Alignment  // 0x24
METHODS:
  RVA=0x0B164634  token=0x60002F3  System.Void Dispose()
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeDisposeJob
TYPE:  sealed struct
TOKEN: 0x200009B
SIZE:  0x20
IMPLEMENTS: Unity.Jobs.IJob
FIELDS:
  public            System.Void*                    Ptr  // 0x10
  public            Unity.Collections.AllocatorManager.AllocatorHandleAllocator  // 0x18
METHODS:
  RVA=0x0B1646B8  token=0x60002F4  System.Void Execute()
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UntypedUnsafeList
TYPE:  sealed struct
TOKEN: 0x200009C
SIZE:  0x30
FIELDS:
  public            System.Void*                    Ptr  // 0x10
  public            System.Int32                    m_length  // 0x18
  public            System.Int32                    m_capacity  // 0x1c
  public            Unity.Collections.AllocatorManager.AllocatorHandleAllocator  // 0x20
  private           System.Int32                    obsolete_length  // 0x24
  private           System.Int32                    obsolete_capacity  // 0x28
METHODS:
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeList`1
TYPE:  sealed struct
TOKEN: 0x200009D
IMPLEMENTS: Unity.Collections.INativeDisposable System.IDisposable Unity.Collections.INativeList`1 Unity.Collections.IIndexable`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  public            T*                              Ptr  // 0x0
  public            System.Int32                    m_length  // 0x0
  public            System.Int32                    m_capacity  // 0x0
  public            Unity.Collections.AllocatorManager.AllocatorHandleAllocator  // 0x0
  public            System.Int32                    length  // 0x0
  public            System.Int32                    capacity  // 0x0
PROPERTIES:
  Length  get=-1  // not resolved
  Capacity  get=-1  // not resolved  set=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
  IsCreated  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60002FA  T& ElementAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x60002FB  System.Void .ctor(System.Int32 initialCapacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator, Unity.Collections.NativeArrayOptions options)
  RVA=-1  // generic def  token=0x60002FC  Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* Create(System.Int32 initialCapacity, U& allocator, Unity.Collections.NativeArrayOptions options)
  RVA=-1  // not resolved  token=0x60002FD  Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* Create(System.Int32 initialCapacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator, Unity.Collections.NativeArrayOptions options)
  RVA=-1  // not resolved  token=0x60002FE  System.Void Destroy(Unity.Collections.LowLevel.Unsafe.UnsafeList<T>* listData)
  RVA=-1  // not resolved  token=0x6000300  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000301  Unity.Jobs.JobHandle Dispose(Unity.Jobs.JobHandle inputDeps)
  RVA=-1  // not resolved  token=0x6000302  Unity.Jobs.JobHandle CrossFrameDispose(Unity.Jobs.JobHandle inputDeps, Unity.Jobs.LowLevel.Unsafe.JobQueuePriority priority)
  RVA=-1  // not resolved  token=0x6000303  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000304  System.Void Resize(System.Int32 length, Unity.Collections.NativeArrayOptions options)
  RVA=-1  // generic def  token=0x6000305  System.Void Realloc(U& allocator, System.Int32 newCapacity)
  RVA=-1  // generic def  token=0x6000306  System.Void SetCapacity(U& allocator, System.Int32 capacity)
  RVA=-1  // not resolved  token=0x6000307  System.Void SetCapacity(System.Int32 capacity)
  RVA=-1  // not resolved  token=0x6000308  System.Void AddNoResize(T value)
  RVA=-1  // not resolved  token=0x6000309  System.Void AddRangeNoResize(System.Void* ptr, System.Int32 count)
  RVA=-1  // not resolved  token=0x600030A  System.Void Add(T& value)
  RVA=-1  // not resolved  token=0x600030B  System.Void AddRange(System.Void* ptr, System.Int32 count)
  RVA=-1  // not resolved  token=0x600030C  System.Void RemoveAtSwapBack(System.Int32 index)
  RVA=-1  // not resolved  token=0x600030D  System.Void RemoveRangeSwapBack(System.Int32 index, System.Int32 count)
  RVA=-1  // not resolved  token=0x600030E  System.Void RemoveAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x600030F  System.Void RemoveRange(System.Int32 index, System.Int32 count)
  RVA=-1  // not resolved  token=0x6000310  Unity.Collections.LowLevel.Unsafe.UnsafeList.ParallelReader<T> AsParallelReader()
  RVA=-1  // not resolved  token=0x6000311  Unity.Collections.LowLevel.Unsafe.UnsafeList.ParallelWriter<T> AsParallelWriter()
  RVA=-1  // not resolved  token=0x6000312  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000313  System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeListTDebugView`1
TYPE:  sealed class
TOKEN: 0x20000A0
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData
TYPE:  sealed struct
TOKEN: 0x20000A1
SIZE:  0x2050
FIELDS:
  private           System.Byte*                    values  // 0x10
  private           System.Byte*                    keys  // 0x18
  private           System.Byte*                    next  // 0x20
  private           System.Byte*                    buckets  // 0x28
  private           System.Int32                    keyCapacity  // 0x30
  private           System.Int32                    bucketCapacityMask  // 0x34
  private           System.Int32                    allocatedIndexLength  // 0x38
  private           Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData.<firstFreeTLS>e__FixedBufferfirstFreeTLS  // 0x50
  private   static  System.Int32                    IntsPerCacheLine  // const
METHODS:
  RVA=0x04DAB580  token=0x6000317  System.Int32 GetBucketSize(System.Int32 capacity)
  RVA=0x0352EE60  token=0x6000318  System.Int32 GrowCapacity(System.Int32 capacity)
  RVA=-1  // generic def  token=0x6000319  System.Void AllocateHashMap(System.Int32 length, System.Int32 bucketLength, Unity.Collections.AllocatorManager.AllocatorHandle label, Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData*& outBuf)
  RVA=-1  // generic def  token=0x600031A  System.Void ReallocateHashMap(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, System.Int32 newCapacity, System.Int32 newBucketCapacity, Unity.Collections.AllocatorManager.AllocatorHandle label)
  RVA=0x037F6580  token=0x600031B  System.Void DeallocateHashMap(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // generic def  token=0x600031C  System.Int32 CalculateDataSize(System.Int32 length, System.Int32 bucketLength, System.Int32& keyOffset, System.Int32& nextOffset, System.Int32& bucketOffset)
  RVA=0x0394D400  token=0x600031D  System.Boolean IsEmpty(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data)
  RVA=0x03A09AA0  token=0x600031E  System.Int32 GetCount(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data)
  RVA=0x034ACF90  token=0x600031F  System.Boolean MoveNext(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, System.Int32& bucketIndex, System.Int32& nextIndex, System.Int32& index)
  RVA=-1  // generic def  token=0x6000320  System.Void GetKeyArray(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, Unity.Collections.NativeArray<TKey> result)
  RVA=-1  // generic def  token=0x6000321  System.Void GetValueArray(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, Unity.Collections.NativeArray<TValue> result)
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataDispose
TYPE:  sealed struct
TOKEN: 0x20000A3
SIZE:  0x20
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData*m_Buffer  // 0x10
  private           Unity.Collections.AllocatorManager.AllocatorHandlem_AllocatorLabel  // 0x18
METHODS:
  RVA=0x0B164714  token=0x6000322  System.Void Dispose()
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataDisposeJob
TYPE:  sealed struct
TOKEN: 0x20000A4
SIZE:  0x20
IMPLEMENTS: Unity.Jobs.IJob
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataDisposeData  // 0x10
METHODS:
  RVA=0x0B16470C  token=0x6000323  System.Void Execute()
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapBase`2
TYPE:  sealed struct
TOKEN: 0x20000A5
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000324  System.Void Clear(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data)
  RVA=-1  // not resolved  token=0x6000325  System.Int32 AllocEntry(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, System.Int32 threadIndex)
  RVA=-1  // not resolved  token=0x6000326  System.Void FreeEntry(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, System.Int32 idx, System.Int32 threadIndex)
  RVA=-1  // not resolved  token=0x6000327  System.Boolean TryAddAtomic(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, TKey key, TValue item, System.Int32 threadIndex)
  RVA=-1  // not resolved  token=0x6000328  System.Void AddAtomicMulti(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, TKey key, TValue item, System.Int32 threadIndex)
  RVA=-1  // not resolved  token=0x6000329  System.Boolean TryAdd(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, TKey key, TValue& item, System.Boolean isMultiHashMap, Unity.Collections.AllocatorManager.AllocatorHandle allocation)
  RVA=-1  // not resolved  token=0x600032A  System.Int32 Remove(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, TKey key, System.Boolean isMultiHashMap)
  RVA=-1  // not resolved  token=0x600032B  System.Void Remove(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it)
  RVA=-1  // not resolved  token=0x600032C  System.Boolean TryGetFirstValueAtomic(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, TKey key, TValue& item, Unity.Collections.NativeParallelMultiHashMapIterator<TKey>& it)
  RVA=-1  // not resolved  token=0x600032D  System.Boolean TryGetNextValueAtomic(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, TValue& item, Unity.Collections.NativeParallelMultiHashMapIterator<TKey>& it)
  RVA=-1  // not resolved  token=0x600032E  System.Boolean SetValue(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data, Unity.Collections.NativeParallelMultiHashMapIterator<TKey>& it, TValue& item)
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.KeyValue`2
TYPE:  sealed struct
TOKEN: 0x20000A6
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData*m_Buffer  // 0x0
  private           System.Int32                    m_Index  // 0x0
  private           System.Int32                    m_Next  // 0x0
PROPERTIES:
  Key  get=-1  // not resolved
  Value  get=-1  // not resolved
METHODS:
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDataEnumerator
TYPE:  sealed struct
TOKEN: 0x20000A7
SIZE:  0x28
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData*m_Buffer  // 0x10
  private           System.Int32                    m_Index  // 0x18
  private           System.Int32                    m_BucketIndex  // 0x1c
  private           System.Int32                    m_NextIndex  // 0x20
METHODS:
  RVA=0x04DAB560  token=0x6000331  System.Void .ctor(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData* data)
  RVA=0x03824360  token=0x6000332  System.Boolean MoveNext()
  RVA=0x04D95D70  token=0x6000333  System.Void Reset()
  RVA=-1  // generic def  token=0x6000334  Unity.Collections.LowLevel.Unsafe.KeyValue<TKey,TValue> GetCurrent()
  RVA=-1  // generic def  token=0x6000335  TKey GetCurrentKey()
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap`2
TYPE:  sealed struct
TOKEN: 0x20000A8
IMPLEMENTS: Unity.Collections.INativeDisposable System.IDisposable System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData*m_Buffer  // 0x0
  private           Unity.Collections.AllocatorManager.AllocatorHandlem_AllocatorLabel  // 0x0
PROPERTIES:
  IsEmpty  get=-1  // not resolved
  Capacity  get=-1  // not resolved  set=-1  // not resolved
  Item  set=-1  // not resolved
  IsCreated  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000336  System.Void .ctor(System.Int32 capacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // not resolved  token=0x6000338  System.Int32 Count()
  RVA=-1  // not resolved  token=0x600033B  System.Void Clear()
  RVA=-1  // not resolved  token=0x600033C  System.Boolean TryAdd(TKey key, TValue& item)
  RVA=-1  // not resolved  token=0x600033D  System.Boolean Remove(TKey key)
  RVA=-1  // not resolved  token=0x600033E  System.Boolean TryGetValue(TKey key, TValue& item)
  RVA=-1  // not resolved  token=0x600033F  System.Boolean ContainsKey(TKey key)
  RVA=-1  // not resolved  token=0x6000342  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000343  Unity.Collections.NativeArray<TKey> GetKeyArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // not resolved  token=0x6000344  Unity.Collections.NativeArray<TValue> GetValueArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // not resolved  token=0x6000345  Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap.ParallelWriter<TKey,TValue> AsParallelWriter()
  RVA=-1  // not resolved  token=0x6000346  System.Collections.Generic.IEnumerator<Unity.Collections.LowLevel.Unsafe.KeyValue<TKey,TValue>> System.Collections.Generic.IEnumerable<Unity.Collections.LowLevel.Unsafe.KeyValue<TKey,TValue>>.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000347  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDisposeJob
TYPE:  sealed struct
TOKEN: 0x20000AA
SIZE:  0x20
IMPLEMENTS: Unity.Jobs.IJob
FIELDS:
  public            Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData*Data  // 0x10
  public            Unity.Collections.AllocatorManager.AllocatorHandleAllocator  // 0x18
METHODS:
  RVA=0x0B164714  token=0x600034A  System.Void Execute()
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapDebuggerTypeProxy`2
TYPE:  sealed class
TOKEN: 0x20000AB
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSet`1
TYPE:  sealed struct
TOKEN: 0x20000AC
IMPLEMENTS: Unity.Collections.INativeDisposable System.IDisposable System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMap<T,System.Boolean>m_Data  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600034B  System.Void .ctor(System.Int32 capacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // not resolved  token=0x600034C  System.Void Dispose()
  RVA=-1  // not resolved  token=0x600034D  System.Void Clear()
  RVA=-1  // not resolved  token=0x600034E  System.Boolean Add(T item)
  RVA=-1  // not resolved  token=0x600034F  System.Boolean Remove(T item)
  RVA=-1  // not resolved  token=0x6000350  System.Boolean Contains(T item)
  RVA=-1  // not resolved  token=0x6000351  Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSet.Enumerator<T> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000352  System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000353  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeHashSetDebuggerTypeProxy`1
TYPE:  sealed class
TOKEN: 0x20000AE
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.HashSetExtensions
TYPE:  static class
TOKEN: 0x20000AF
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000359  System.Void UnionWith(Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSet<T> container, Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashSet<T> other)
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap`2
TYPE:  sealed struct
TOKEN: 0x20000B0
IMPLEMENTS: Unity.Collections.INativeDisposable System.IDisposable System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeParallelHashMapData*m_Buffer  // 0x0
  private           Unity.Collections.AllocatorManager.AllocatorHandlem_AllocatorLabel  // 0x0
PROPERTIES:
  IsEmpty  get=-1  // not resolved
  Capacity  get=-1  // not resolved
  IsCreated  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600035A  System.Void .ctor(System.Int32 capacity, Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // not resolved  token=0x600035C  System.Int32 Count()
  RVA=-1  // not resolved  token=0x600035E  System.Void Clear()
  RVA=-1  // not resolved  token=0x600035F  System.Void Add(TKey key, TValue item)
  RVA=-1  // not resolved  token=0x6000360  System.Int32 Remove(TKey key)
  RVA=-1  // not resolved  token=0x6000361  System.Void Remove(Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it)
  RVA=-1  // not resolved  token=0x6000362  System.Boolean TryGetFirstValue(TKey key, TValue& item, Unity.Collections.NativeParallelMultiHashMapIterator<TKey>& it)
  RVA=-1  // not resolved  token=0x6000363  System.Boolean TryGetNextValue(TValue& item, Unity.Collections.NativeParallelMultiHashMapIterator<TKey>& it)
  RVA=-1  // not resolved  token=0x6000364  System.Boolean SetValue(TValue item, Unity.Collections.NativeParallelMultiHashMapIterator<TKey> it)
  RVA=-1  // not resolved  token=0x6000366  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000367  Unity.Collections.NativeArray<TKey> GetKeyArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // not resolved  token=0x6000368  Unity.Collections.NativeArray<TValue> GetValueArray(Unity.Collections.AllocatorManager.AllocatorHandle allocator)
  RVA=-1  // not resolved  token=0x6000369  Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMap.ParallelWriter<TKey,TValue> AsParallelWriter()
  RVA=-1  // not resolved  token=0x600036A  System.Collections.Generic.IEnumerator<Unity.Collections.LowLevel.Unsafe.KeyValue<TKey,TValue>> System.Collections.Generic.IEnumerable<Unity.Collections.LowLevel.Unsafe.KeyValue<TKey,TValue>>.GetEnumerator()
  RVA=-1  // not resolved  token=0x600036B  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeParallelMultiHashMapDebuggerTypeProxy`2
TYPE:  sealed class
TOKEN: 0x20000B2
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock
TYPE:  sealed struct
TOKEN: 0x20000B3
SIZE:  0x20
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock*Next  // 0x10
  private           Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock.<Data>e__FixedBufferData  // 0x18
METHODS:
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeStreamRange
TYPE:  sealed struct
TOKEN: 0x20000B5
SIZE:  0x28
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock*Block  // 0x10
  private           System.Int32                    OffsetInFirstBlock  // 0x18
  private           System.Int32                    ElementCount  // 0x1c
  private           System.Int32                    LastOffset  // 0x20
  private           System.Int32                    NumberOfBlocks  // 0x24
METHODS:
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData
TYPE:  sealed struct
TOKEN: 0x20000B6
SIZE:  0x40
FIELDS:
  private           Unity.Collections.AllocatorManager.AllocatorHandleAllocator  // 0x10
  private           Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock**Blocks  // 0x18
  private           System.Int32                    BlockCount  // 0x20
  private           Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlock*Free  // 0x28
  private           Unity.Collections.LowLevel.Unsafe.UnsafeStreamRange*Ranges  // 0x30
  private           System.Int32                    RangeCount  // 0x38
METHODS:
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeStream
TYPE:  sealed struct
TOKEN: 0x20000B7
SIZE:  0x20
IMPLEMENTS: Unity.Collections.INativeDisposable System.IDisposable
FIELDS:
  private           Unity.Collections.LowLevel.Unsafe.UnsafeStreamBlockData*m_Block  // 0x10
  private           Unity.Collections.AllocatorManager.AllocatorHandlem_Allocator  // 0x18
METHODS:
  RVA=0x0B164724  token=0x600036D  System.Void AllocateForEach(System.Int32 forEachCount)
  RVA=0x0B1647A4  token=0x600036E  System.Void Deallocate()
  RVA=0x0B160058  token=0x600036F  System.Void Dispose()
END_CLASS

CLASS: Unity.Collections.LowLevel.Unsafe.UnsafeUtilityExtensions
TYPE:  static class
TOKEN: 0x20000BB
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000373  System.Void* AddressOf(T& value)
  RVA=-1  // generic def  token=0x6000374  T& AsRef(T& value)
  RVA=0x03116130  token=0x6000375  System.IntPtr Malloc(System.Int64 size, System.Int32 alignment, Unity.Collections.Allocator allocator)
END_CLASS

CLASS: Unity.Collections.NotBurstCompatible.Extensions
TYPE:  static class
TOKEN: 0x2000097
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60002ED  T[] ToArrayNBC(Unity.Collections.NativeList<T> list)
END_CLASS

CLASS: Unity.Jobs.EarlyInitHelpers
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B160060  token=0x6000003  System.Void JobReflectionDataCreationFailed(System.Exception ex, System.Type jobType)
END_CLASS

CLASS: Unity.Jobs.IJobBurstSchedulable
TYPE:  interface
TOKEN: 0x2000005
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000004  System.Void Execute()
END_CLASS

CLASS: Unity.Jobs.IJobBurstSchedulableExtensions
TYPE:  static class
TOKEN: 0x2000006
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Jobs.IJobParallelForBatch
TYPE:  interface
TOKEN: 0x2000009
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600000A  System.Void Execute(System.Int32 startIndex, System.Int32 count)
END_CLASS

CLASS: Unity.Jobs.IJobParallelForBatchExtensions
TYPE:  static class
TOKEN: 0x200000A
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x600000B  System.Void EarlyJobInit()
  RVA=-1  // generic def  token=0x600000C  Unity.Jobs.JobHandle ScheduleBatch(T jobData, System.Int32 arrayLength, System.Int32 minIndicesPerJobCount, Unity.Jobs.JobHandle dependsOn)
END_CLASS

CLASS: Unity.Jobs.IJobParallelForBurstSchedulable
TYPE:  interface
TOKEN: 0x200000D
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000012  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: Unity.Jobs.IJobParallelForExtensionsBurstSchedulable
TYPE:  static class
TOKEN: 0x200000E
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Jobs.IJobParallelForDefer
TYPE:  interface
TOKEN: 0x2000011
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000018  System.Void Execute(System.Int32 index)
END_CLASS

CLASS: Unity.Jobs.IJobParallelForDeferExtensions
TYPE:  static class
TOKEN: 0x2000012
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000019  System.Void EarlyJobInit()
  RVA=-1  // generic def  token=0x600001A  Unity.Jobs.JobHandle Schedule(T jobData, Unity.Collections.NativeList<U> list, System.Int32 innerloopBatchCount, Unity.Jobs.JobHandle dependsOn)
  RVA=-1  // generic def  token=0x600001B  Unity.Jobs.JobHandle Schedule(T jobData, System.Int32* forEachCount, System.Int32 innerloopBatchCount, Unity.Jobs.JobHandle dependsOn)
  RVA=-1  // generic def  token=0x600001C  Unity.Jobs.JobHandle ScheduleByRef(T& jobData, System.Int32* forEachCount, System.Int32 innerloopBatchCount, Unity.Jobs.JobHandle dependsOn)
  RVA=-1  // generic def  token=0x600001D  Unity.Jobs.JobHandle ScheduleInternal(T& jobData, System.Int32 innerloopBatchCount, System.Void* forEachListPtr, System.Void* atomicSafetyHandlePtr, Unity.Jobs.JobHandle dependsOn)
END_CLASS

CLASS: Unity.Jobs.IJobParallelForFilter
TYPE:  interface
TOKEN: 0x2000015
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000023  System.Boolean Execute(System.Int32 index)
END_CLASS

CLASS: Unity.Jobs.JobParallelIndexListExtensions
TYPE:  static class
TOKEN: 0x2000016
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.Jobs.DOTSCompilerGeneratedAttribute
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600002B  System.Void .ctor()
END_CLASS

