// ========================================================
// Dumped by @desirepro
// Assembly: Unsafe.Streaming.dll
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

CLASS: Beyond.Gameplay.Streaming.StreamingUnsafeUtilsV2
TYPE:  static class
TOKEN: 0x200000A
SIZE:  0x10
FIELDS:
  public    static  System.Int32                    s_defaultSceneStateId  // static @ 0x0
  public    static  System.Int32                    s_defaultAreaId  // static @ 0x4
METHODS:
  RVA=0x02ED52D0  token=0x6000009  UnityEngine.HyperGryph.Streaming.StreamingSourceData CreateStreamingSourceData(UnityEngine.Vector2 streamingPos, System.Single chunkLoadRadius, System.Single defaultLayerLoadRadius, System.Single hlod0LayerLoadRadius, System.Single hlod1LayerLoadRadius, System.Single hlod2LayerLoadRadius, System.Single colliderLayerLoadRadius, System.Single tinyLayerLoadRadius, System.Single waterLayerLoadRadius, System.Single lightingLoadRadius, System.Single audioLoadRadius, System.Single rendererWithColliderLoadRadius, Unity.Collections.NativeArray<System.Byte> layerEnabled)
  RVA=-1  // generic def  token=0x600000A  System.Void MemSet(Unity.Collections.NativeArray<T> array, System.Byte value, System.Int32 count)
  RVA=-1  // generic def  token=0x600000B  System.Span<T> AsSpan(Unity.Collections.NativeArray<T> nativeArray)
  RVA=-1  // generic def  token=0x600000C  System.ReadOnlySpan<T> AsReadOnlySpan(Unity.Collections.NativeArray<T> nativeArray)
  RVA=0x0B5AB1A0  token=0x600000D  System.Void MemCpyUnsafe(System.IntPtr destination, System.IntPtr source, System.Int64 size)
  RVA=0x0B5AB12C  token=0x600000E  System.IntPtr MallocUnsafe(System.Int64 size, System.Int32 alignment, Unity.Collections.Allocator allocator)
  RVA=0x0B5AB0CC  token=0x600000F  System.Void FreeUnsafe(System.IntPtr memory, Unity.Collections.Allocator allocator)
  RVA=-1  // generic def  token=0x6000010  System.Byte[] ConvertToBytes(T val)
  RVA=-1  // generic def  token=0x6000011  System.IntPtr GetIntPtr(Unity.Collections.NativeArray<T> array)
  RVA=-1  // generic def  token=0x6000012  System.IntPtr GetReadonlyIntPtr(Unity.Collections.NativeArray<T> array)
  RVA=-1  // generic def  token=0x6000013  System.UIntPtr GetReadonlyUIntPtr(Unity.Collections.NativeArray<T> array)
  RVA=-1  // generic def  token=0x6000014  System.IntPtr GetRendererInfos(T& kComponent)
  RVA=0x04DB3AC0  token=0x6000015  UnityEngine.HyperGryph.ECS.RendererInfo& GetRendererInfo(System.IntPtr rendererInfos, System.Int32 index)
  RVA=-1  // generic def  token=0x6000016  TTo ReinterpretAs(TFrom val)
END_CLASS

CLASS: Beyond.Gameplay.Streaming.UnsafeArray`1
TYPE:  sealed struct
TOKEN: 0x200000B
IMPLEMENTS: System.IDisposable
FIELDS:
  public            System.IntPtr                   ptr  // 0x0
  public            System.Int32                    count  // 0x0
  public            Unity.Collections.Allocator     allocator  // 0x0
PROPERTIES:
  IsCreated  get=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000018  System.Void .ctor(System.Int32 count, Unity.Collections.Allocator allocator, Unity.Collections.NativeArrayOptions options)
  RVA=-1  // not resolved  token=0x600001B  T GetValue(System.Int32 index)
  RVA=-1  // not resolved  token=0x600001C  T& GetRef(System.Int32 index)
  RVA=-1  // not resolved  token=0x600001D  System.Void SetValue(System.Int32 index, T value)
  RVA=-1  // not resolved  token=0x600001E  System.Void Resize(System.Int32 newCount, Unity.Collections.NativeArrayOptions options)
  RVA=-1  // not resolved  token=0x600001F  System.Void Dispose()
END_CLASS

CLASS: Beyond.Gameplay.Streaming.VFSReadOperation
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x28
IMPLEMENTS: System.IDisposable
FIELDS:
  public            Unity.IO.LowLevel.Unsafe.ReadCommand*readCommand  // 0x10
  public            Unity.IO.LowLevel.Unsafe.ReadHandlereadHandle  // 0x18
METHODS:
  RVA=0x0B5AB3AC  token=0x6000020  System.Void .ctor(Beyond.VFS.FVFBlockFileInfo info, System.Boolean async)
  RVA=0x0B5AB218  token=0x6000021  System.Void Dispose()
  RVA=0x0B5AB35C  token=0x6000022  System.Boolean IsValid()
  RVA=0x0B5AB29C  token=0x6000023  Unity.Collections.NativeArray<System.Byte> GetData()
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardUtility
TYPE:  static class
TOKEN: 0x2000005
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_enableFastPathBuffer  // static @ 0x0
METHODS:
  RVA=-1  // generic def  token=0x6000004  System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity)
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardAttribute
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000005  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.ECSComponentAttribute
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public            System.Runtime.InteropServices.LayoutKindlayoutKind  // 0x10
  public            System.Boolean                  isTag  // 0x14
METHODS:
  RVA=0x041E1670  token=0x6000006  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.CameraControlConfigAttribute
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000007  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.DataNAttribute
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public            System.Int32                    capacity  // 0x10
  public            System.Int32                    tSize  // 0x14
  public            System.Boolean                  ring  // 0x18
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
TOKEN: 0x200000D
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x6000024  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x02ED4350  token=0x6000025  UnityEngine.HyperGryph.Streaming.StreamingSourceData __Gen_Wrap_0(UnityEngine.Vector2 P0, System.Single P1, System.Single P2, System.Single P3, System.Single P4, System.Single P5, System.Single P6, System.Single P7, System.Single P8, System.Single P9, System.Single P10, System.Single P11, Unity.Collections.NativeArray<System.Byte> P12)
  RVA=0x0B5AA9B4  token=0x6000026  System.Void __Gen_Wrap_1(System.IntPtr P0, System.IntPtr P1, System.Int64 P2)
  RVA=0x0B5AAA98  token=0x6000027  System.IntPtr __Gen_Wrap_2(System.Int64 P0, System.Int32 P1, Unity.Collections.Allocator P2)
  RVA=0x0B5AAB88  token=0x6000028  System.Void __Gen_Wrap_3(System.IntPtr P0, Unity.Collections.Allocator P1)
  RVA=0x0B5AAC50  token=0x6000029  System.Void __Gen_Wrap_4(Beyond.Gameplay.Streaming.VFSReadOperation& P0)
  RVA=0x0B5AAD84  token=0x600002A  System.Boolean __Gen_Wrap_5(Beyond.Gameplay.Streaming.VFSReadOperation& P0)
  RVA=0x0B5AAEC8  token=0x600002B  Unity.Collections.NativeArray<System.Byte> __Gen_Wrap_6(Beyond.Gameplay.Streaming.VFSReadOperation& P0)
  RVA=0x04D37560  token=0x600002C  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x200000E
SIZE:  0x40
EXTENDS: IFix.Core.AnonymousStorey
FIELDS:
METHODS:
  RVA=0x0B5AB038  token=0x600002D  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x600002E  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x200000F
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x600002F  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x0B5AB6FC  token=0x6000030  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x03EE5230  token=0x6000031  System.Boolean IsPatched(System.Int32 id)
  RVA=0x0B5AB5B8  token=0x6000032  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x0B5AB684  token=0x6000033  System.Object CreateWrapper(System.Int32 id)
  RVA=0x0B5AB760  token=0x6000034  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x0B5AB514  token=0x6000035  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: IFix.IDMAP0
TYPE:  sealed struct
TOKEN: 0x2000010
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Streaming-StreamingUnsafeUtilsV2-CreateStreamingSourceData0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Streaming-StreamingUnsafeUtilsV2-MemCpyUnsafe0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Streaming-StreamingUnsafeUtilsV2-MallocUnsafe0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Streaming-StreamingUnsafeUtilsV2-FreeUnsafe0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Streaming-VFSReadOperation-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Streaming-VFSReadOperation-IsValid0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Streaming-VFSReadOperation-GetData0  // const
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

