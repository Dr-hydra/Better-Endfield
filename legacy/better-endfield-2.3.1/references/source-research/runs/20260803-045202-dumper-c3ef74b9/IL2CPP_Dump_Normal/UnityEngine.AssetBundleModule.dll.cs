// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.AssetBundleModule.dll
// Classes:  9
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

namespace UnityEngine
{

    // TypeToken: 0x2000002  // size: 0x18
    public class AssetBundle : UnityEngine.Object
    {
        // Fields
        public static UnityEngine.AssetBundle[] BatchAssetBundleBuffer;  // static @ 0x0
        public static System.Int64[] BatchHashBuffer;  // static @ 0x8
        public static System.String[] BatchPathBuffer;  // static @ 0x10
        public static System.UInt32[] BatchCrcBuffer;  // static @ 0x18
        public static System.UInt64[] BatchOffsetBuffer;  // static @ 0x20
        private static System.Action onMissReferencedScript;  // static @ 0x28
        public static System.Func<System.Int64,System.String> hashToStringPath;  // static @ 0x30
        public static System.Func<System.Int64,System.Boolean> checkHashValid;  // static @ 0x38
        public static System.Action<System.String> addPathSting;  // static @ 0x40

        // Properties
        System.Boolean enableLazyCloseFile { set; /* RVA: 0x04D14940 */ }
        System.UInt32 memoryBudgetKB { get; /* RVA: 0x05395C80 */ set; /* RVA: 0x0B3A2A28 */ }

        // Methods
        // RVA: 0x0B3A29E8  token: 0x6000001
        private System.Void .ctor() { }
        // RVA: 0x0B3A2810  token: 0x6000002
        public static System.Void UnloadAllAssetBundles(System.Boolean unloadAllObjects) { }
        // RVA: 0x0B3A2054  token: 0x6000003
        private static UnityEngine.AssetBundle[] GetAllLoadedAssetBundles_Native() { }
        // RVA: 0x0B3A2080  token: 0x6000004
        public static System.Collections.Generic.IEnumerable<UnityEngine.AssetBundle> GetAllLoadedAssetBundles() { }
        // RVA: 0x0380AC00  token: 0x6000005
        public static System.Void WaitForAllAsyncOperationsToComplete() { }
        // RVA: 0x0380ABC0  token: 0x6000006
        private static System.Void WaitForAllAsyncOperationsToComplete_Internal() { }
        // RVA: 0x049925D0  token: 0x6000007
        private static System.Int32 GetBatchLoadFromFileIntegrateMaxCount_Internal() { }
        // RVA: -1  // generic def  token: 0x6000008
        private static T[] CreateBatchLoadFromFileBuffer() { }
        // RVA: 0x0B3A2150  token: 0x6000009
        private static System.Void InvokeOnMissReferencedScriptOnBehaviour() { }
        // RVA: 0x0B3A2790  token: 0x600000B
        private static System.Boolean TryCloseLazyCloseFile_Internal(System.Boolean force) { }
        // RVA: 0x0B3A27C4  token: 0x600000C
        public static System.Boolean TryCloseLazyCloseFile(System.Boolean force) { }
        // RVA: 0x0B3A20C0  token: 0x600000D
        private static System.String HashToStringPath(System.Int64 hash) { }
        // RVA: 0x0B3A1FCC  token: 0x600000E
        private static System.Boolean CheckHashValid(System.Int64 hash) { }
        // RVA: 0x0B3A1F70  token: 0x600000F
        private static System.Void AddPathStringToCSharp(System.String path) { }
        // RVA: 0x02F73240  token: 0x6000010
        private static UnityEngine.AssetBundleCreateRequest LoadFromFileAsync_Internal(System.String path, System.UInt32 crc, System.UInt64 offset) { }
        // RVA: 0x0B3A2484  token: 0x6000011
        public static UnityEngine.AssetBundleCreateRequest LoadFromFileAsync(System.String path) { }
        // RVA: 0x02F731C0  token: 0x6000012
        public static UnityEngine.AssetBundleCreateRequest LoadFromFileAsync(System.String path, System.UInt32 crc, System.UInt64 offset) { }
        // RVA: 0x03BB36F0  token: 0x6000013
        private static UnityEngine.AssetBundle LoadFromFile_Internal(System.String path, System.UInt32 crc, System.UInt64 offset) { }
        // RVA: 0x0B3A24D4  token: 0x6000014
        public static UnityEngine.AssetBundle LoadFromFile(System.String path) { }
        // RVA: 0x03BB3670  token: 0x6000015
        public static UnityEngine.AssetBundle LoadFromFile(System.String path, System.UInt32 crc, System.UInt64 offset) { }
        // RVA: 0x0B3A2524  token: 0x6000016
        private static UnityEngine.AssetBundleCreateRequest LoadFromMemoryAsync_Internal(System.Byte[] binary, System.UInt32 crc) { }
        // RVA: 0x0B3A2564  token: 0x6000017
        public static UnityEngine.AssetBundleCreateRequest LoadFromMemoryAsync(System.Byte[] binary) { }
        // RVA: -1  // generic def  token: 0x6000018
        public T LoadAsset(System.String name) { }
        // RVA: 0x05393960  token: 0x6000019
        public UnityEngine.Object LoadAsset(System.Int64 hash, System.Type type) { }
        // RVA: 0x0B3A2368  token: 0x600001A
        public UnityEngine.Object LoadAsset(System.String name, System.Type type) { }
        // RVA: 0x0B3A2314  token: 0x600001B
        private UnityEngine.Object LoadAsset_Internal(System.String name, System.Type type) { }
        // RVA: 0x0380DA20  token: 0x600001C
        private UnityEngine.Object LoadAssetByHash_Internal(System.Int64 hash, System.Type type) { }
        // RVA: -1  // generic def  token: 0x600001D
        public UnityEngine.AssetBundleRequest LoadAssetAsync(System.String name) { }
        // RVA: 0x0B3A21F8  token: 0x600001E
        public UnityEngine.AssetBundleRequest LoadAssetAsync(System.String name, System.Type type) { }
        // RVA: 0x0380D250  token: 0x600001F
        public UnityEngine.AssetBundleRequest LoadAssetAsync(System.Int64 hash, System.Type type) { }
        // RVA: 0x0B3A2614  token: 0x6000020
        public UnityEngine.AssetBundleRequest LoadSubAssetAsync(System.String name, System.Int64 subAssetInstanceID, System.Type type) { }
        // RVA: 0x0B3A21A4  token: 0x6000021
        private UnityEngine.AssetBundleRequest LoadAssetAsync_Internal(System.String name, System.Type type) { }
        // RVA: 0x0380D2C0  token: 0x6000022
        private UnityEngine.AssetBundleRequest LoadAssetAsyncByHash_Internal(System.Int64 hash, System.Type type) { }
        // RVA: 0x02F66030  token: 0x6000023
        public System.Void Unload(System.Boolean unloadAllLoadedObjects) { }
        // RVA: 0x0B3A29A4  token: 0x6000024
        public UnityEngine.AsyncOperation UnloadAsync(System.Boolean unloadAllLoadedObjects) { }
        // RVA: 0x0B3A2940  token: 0x6000025
        public System.Void UnloadAsset(UnityEngine.Object asset) { }
        // RVA: 0x0B3A2888  token: 0x6000026
        public UnityEngine.AsyncOperation UnloadAssetAsync(UnityEngine.Object asset) { }
        // RVA: 0x0B3A28FC  token: 0x6000027
        private System.Void UnloadAsset_Internal(UnityEngine.Object asset) { }
        // RVA: 0x0B3A2844  token: 0x6000028
        private UnityEngine.AsyncOperation UnloadAssetAsync_Internal(UnityEngine.Object asset) { }
        // RVA: 0x0B3A25B0  token: 0x6000029
        private UnityEngine.AssetBundleRequest LoadSubAssetAsync_Internal(System.String name, System.Int64 instanceID, System.Type type) { }
        // RVA: 0x04B266A0  token: 0x600002C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000003  // size: 0x20
    public class AssetBundleBatchCreateRequest : UnityEngine.AsyncOperation
    {
    }

    // TypeToken: 0x2000004  // size: 0x20
    public class AssetBundleBatchRequest : UnityEngine.AsyncOperation
    {
    }

    // TypeToken: 0x2000005  // size: 0x20
    public class AssetBundleCreateRequest : UnityEngine.AsyncOperation
    {
        // Properties
        UnityEngine.AssetBundle assetBundle { get; /* RVA: 0x0380BF40 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600002E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x10
    public static class AssetBundleLoadingCache
    {
        // Properties
        System.UInt32 maxBlocksPerFile { get; /* RVA: 0x0B3A1F44 */ set; /* RVA: 0x049BFB50 */ }
        System.UInt32 blockCount { get; /* RVA: 0x049BFAD0 */ set; /* RVA: 0x049BFB10 */ }
        System.UInt32 blockSize { get; /* RVA: 0x049BF9C0 */ }
        System.UInt32 memoryBudgetKB { get; /* RVA: 0x049BF9A0 */ set; /* RVA: 0x049BFA00 */ }

    }

    // TypeToken: 0x2000007  // size: 0x20
    public class AssetBundleRecompressOperation : UnityEngine.AsyncOperation
    {
    }

    // TypeToken: 0x2000008  // size: 0x30
    public class AssetBundleRequest : UnityEngine.ResourceRequest
    {
        // Properties
        UnityEngine.Object asset { get; /* RVA: 0x0539396C */ }

        // Methods
        // RVA: 0x0380D9E0  token: 0x6000036
        protected virtual UnityEngine.Object GetResult() { }
        // RVA: 0x041E1670  token: 0x6000038
        public System.Void .ctor() { }

    }

}

namespace UnityEngine.Experimental.AssetBundlePatching
{

    // TypeToken: 0x2000009  // size: 0x10
    public static class AssetBundleUtility
    {
        // Methods
        // RVA: 0x03A18E70  token: 0x6000039
        public static System.Int32 LZ4Decompress(System.Void* src, System.Int32 srcSize, System.Void* dst, System.Int32 dstSize) { }
        // RVA: 0x03A18DF0  token: 0x600003A
        public static System.Int32 LZ4Decompress(System.Byte[] src, System.Int32 srcOffset, System.Int32 srcSize, System.Byte[] dst, System.Int32 dstOffset, System.Int32 dstSize) { }

    }

}

