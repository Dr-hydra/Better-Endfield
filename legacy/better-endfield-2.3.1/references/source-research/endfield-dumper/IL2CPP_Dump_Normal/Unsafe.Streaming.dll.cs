// ========================================================
// Dumped by @desirepro
// Assembly: Unsafe.Streaming.dll
// Classes:  16
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

namespace Beyond.Gameplay.Streaming
{

    // TypeToken: 0x200000A  // size: 0x10
    public static class StreamingUnsafeUtilsV2
    {
        // Fields
        public static System.Int32 s_defaultSceneStateId;  // static @ 0x0
        public static System.Int32 s_defaultAreaId;  // static @ 0x4

        // Methods
        // RVA: 0x02ED52D0  token: 0x6000009
        public static UnityEngine.HyperGryph.Streaming.StreamingSourceData CreateStreamingSourceData(UnityEngine.Vector2 streamingPos, System.Single chunkLoadRadius, System.Single defaultLayerLoadRadius, System.Single hlod0LayerLoadRadius, System.Single hlod1LayerLoadRadius, System.Single hlod2LayerLoadRadius, System.Single colliderLayerLoadRadius, System.Single tinyLayerLoadRadius, System.Single waterLayerLoadRadius, System.Single lightingLoadRadius, System.Single audioLoadRadius, System.Single rendererWithColliderLoadRadius, Unity.Collections.NativeArray<System.Byte> layerEnabled) { }
        // RVA: -1  // generic def  token: 0x600000A
        public static System.Void MemSet(Unity.Collections.NativeArray<T> array, System.Byte value, System.Int32 count) { }
        // RVA: -1  // generic def  token: 0x600000B
        public static System.Span<T> AsSpan(Unity.Collections.NativeArray<T> nativeArray) { }
        // RVA: -1  // generic def  token: 0x600000C
        public static System.ReadOnlySpan<T> AsReadOnlySpan(Unity.Collections.NativeArray<T> nativeArray) { }
        // RVA: 0x0B5AB1A0  token: 0x600000D
        public static System.Void MemCpyUnsafe(System.IntPtr destination, System.IntPtr source, System.Int64 size) { }
        // RVA: 0x0B5AB12C  token: 0x600000E
        public static System.IntPtr MallocUnsafe(System.Int64 size, System.Int32 alignment, Unity.Collections.Allocator allocator) { }
        // RVA: 0x0B5AB0CC  token: 0x600000F
        public static System.Void FreeUnsafe(System.IntPtr memory, Unity.Collections.Allocator allocator) { }
        // RVA: -1  // generic def  token: 0x6000010
        public static System.Byte[] ConvertToBytes(T val) { }
        // RVA: -1  // generic def  token: 0x6000011
        public static System.IntPtr GetIntPtr(Unity.Collections.NativeArray<T> array) { }
        // RVA: -1  // generic def  token: 0x6000012
        public static System.IntPtr GetReadonlyIntPtr(Unity.Collections.NativeArray<T> array) { }
        // RVA: -1  // generic def  token: 0x6000013
        public static System.UIntPtr GetReadonlyUIntPtr(Unity.Collections.NativeArray<T> array) { }
        // RVA: -1  // generic def  token: 0x6000014
        public static System.IntPtr GetRendererInfos(T& kComponent) { }
        // RVA: 0x04DB3AC0  token: 0x6000015
        public static UnityEngine.HyperGryph.ECS.RendererInfo& GetRendererInfo(System.IntPtr rendererInfos, System.Int32 index) { }
        // RVA: -1  // generic def  token: 0x6000016
        public static TTo ReinterpretAs(TFrom val) { }

    }

    // TypeToken: 0x200000B
    public sealed struct UnsafeArray`1 : System.IDisposable
    {
        // Fields
        public System.IntPtr ptr;  // 0x0
        public System.Int32 count;  // 0x0
        public Unity.Collections.Allocator allocator;  // 0x0

        // Properties
        System.Boolean IsCreated { get; /* RVA: -1  // not resolved */ }
        T Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000018
        public System.Void .ctor(System.Int32 count, Unity.Collections.Allocator allocator, Unity.Collections.NativeArrayOptions options) { }
        // RVA: -1  // not resolved  token: 0x600001B
        public T GetValue(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x600001C
        public T& GetRef(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x600001D
        public System.Void SetValue(System.Int32 index, T value) { }
        // RVA: -1  // not resolved  token: 0x600001E
        public System.Void Resize(System.Int32 newCount, Unity.Collections.NativeArrayOptions options) { }
        // RVA: -1  // not resolved  token: 0x600001F
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x200000C  // size: 0x28
    public sealed struct VFSReadOperation : System.IDisposable
    {
        // Fields
        public Unity.IO.LowLevel.Unsafe.ReadCommand* readCommand;  // 0x10
        public Unity.IO.LowLevel.Unsafe.ReadHandle readHandle;  // 0x18

        // Methods
        // RVA: 0x0B5AB3AC  token: 0x6000020
        public System.Void .ctor(Beyond.VFS.FVFBlockFileInfo info, System.Boolean async) { }
        // RVA: 0x0B5AB218  token: 0x6000021
        public virtual System.Void Dispose() { }
        // RVA: 0x0B5AB35C  token: 0x6000022
        public System.Boolean IsValid() { }
        // RVA: 0x0B5AB29C  token: 0x6000023
        public Unity.Collections.NativeArray<System.Byte> GetData() { }

    }

}

namespace Beyond.SourceGenerator
{

    // TypeToken: 0x2000005  // size: 0x10
    public static class AnimatorBlackboardUtility
    {
        // Fields
        private static System.Boolean s_enableFastPathBuffer;  // static @ 0x0

        // Methods
        // RVA: -1  // generic def  token: 0x6000004
        private static System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity) { }

    }

    // TypeToken: 0x2000006  // size: 0x10
    public class AnimatorBlackboardAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000005
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x18
    public class ECSComponentAttribute : System.Attribute
    {
        // Fields
        public System.Runtime.InteropServices.LayoutKind layoutKind;  // 0x10
        public System.Boolean isTag;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x6000006
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000008  // size: 0x10
    public class CameraControlConfigAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000007
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x20
    public class DataNAttribute : System.Attribute
    {
        // Fields
        public System.Int32 capacity;  // 0x10
        public System.Int32 tSize;  // 0x14
        public System.Boolean ring;  // 0x18

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

    // TypeToken: 0x200000D  // size: 0x28
    public class ILFixDynamicMethodWrapper
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10
        private System.Int32 methodId;  // 0x18
        private System.Object anonObj;  // 0x20
        public static IFix.ILFixDynamicMethodWrapper[] wrapperArray;  // static @ 0x0

        // Methods
        // RVA: 0x032B2C30  token: 0x6000024
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj) { }
        // RVA: 0x02ED4350  token: 0x6000025
        public UnityEngine.HyperGryph.Streaming.StreamingSourceData __Gen_Wrap_0(UnityEngine.Vector2 P0, System.Single P1, System.Single P2, System.Single P3, System.Single P4, System.Single P5, System.Single P6, System.Single P7, System.Single P8, System.Single P9, System.Single P10, System.Single P11, Unity.Collections.NativeArray<System.Byte> P12) { }
        // RVA: 0x0B5AA9B4  token: 0x6000026
        public System.Void __Gen_Wrap_1(System.IntPtr P0, System.IntPtr P1, System.Int64 P2) { }
        // RVA: 0x0B5AAA98  token: 0x6000027
        public System.IntPtr __Gen_Wrap_2(System.Int64 P0, System.Int32 P1, Unity.Collections.Allocator P2) { }
        // RVA: 0x0B5AAB88  token: 0x6000028
        public System.Void __Gen_Wrap_3(System.IntPtr P0, Unity.Collections.Allocator P1) { }
        // RVA: 0x0B5AAC50  token: 0x6000029
        public System.Void __Gen_Wrap_4(Beyond.Gameplay.Streaming.VFSReadOperation& P0) { }
        // RVA: 0x0B5AAD84  token: 0x600002A
        public System.Boolean __Gen_Wrap_5(Beyond.Gameplay.Streaming.VFSReadOperation& P0) { }
        // RVA: 0x0B5AAEC8  token: 0x600002B
        public Unity.Collections.NativeArray<System.Byte> __Gen_Wrap_6(Beyond.Gameplay.Streaming.VFSReadOperation& P0) { }
        // RVA: 0x04D37560  token: 0x600002C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000E  // size: 0x40
    public class ILFixInterfaceBridge : IFix.Core.AnonymousStorey
    {
        // Methods
        // RVA: 0x0B5AB038  token: 0x600002D
        public System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x041E1670  token: 0x600002E
        public System.Void RefAsyncBuilderStartMethod() { }

    }

    // TypeToken: 0x200000F  // size: 0x18
    public class WrappersManagerImpl : IFix.Core.WrappersManager
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x600002F
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x0B5AB6FC  token: 0x6000030
        public static IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id) { }
        // RVA: 0x03EE5230  token: 0x6000031
        public static System.Boolean IsPatched(System.Int32 id) { }
        // RVA: 0x0B5AB5B8  token: 0x6000032
        public virtual System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon) { }
        // RVA: 0x0B5AB684  token: 0x6000033
        public virtual System.Object CreateWrapper(System.Int32 id) { }
        // RVA: 0x0B5AB760  token: 0x6000034
        public virtual System.Object InitWrapperArray(System.Int32 len) { }
        // RVA: 0x0B5AB514  token: 0x6000035
        public virtual IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine) { }

    }

    // TypeToken: 0x2000010  // size: 0x14
    public sealed struct IDMAP0
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static IFix.IDMAP0 Beyond-Gameplay-Streaming-StreamingUnsafeUtilsV2-CreateStreamingSourceData0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Streaming-StreamingUnsafeUtilsV2-MemCpyUnsafe0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Streaming-StreamingUnsafeUtilsV2-MallocUnsafe0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Streaming-StreamingUnsafeUtilsV2-FreeUnsafe0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Streaming-VFSReadOperation-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Streaming-VFSReadOperation-IsValid0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Streaming-VFSReadOperation-GetData0;  // const

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

