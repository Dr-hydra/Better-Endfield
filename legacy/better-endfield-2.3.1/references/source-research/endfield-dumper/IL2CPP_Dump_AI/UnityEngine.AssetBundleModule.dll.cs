// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.AssetBundleModule.dll
// Classes:  9
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

CLASS: UnityEngine.AssetBundle
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
  public    static  UnityEngine.AssetBundle[]       BatchAssetBundleBuffer  // static @ 0x0
  public    static  System.Int64[]                  BatchHashBuffer  // static @ 0x8
  public    static  System.String[]                 BatchPathBuffer  // static @ 0x10
  public    static  System.UInt32[]                 BatchCrcBuffer  // static @ 0x18
  public    static  System.UInt64[]                 BatchOffsetBuffer  // static @ 0x20
  private   static  System.Action                   onMissReferencedScript  // static @ 0x28
  public    static  System.Func<System.Int64,System.String>hashToStringPath  // static @ 0x30
  public    static  System.Func<System.Int64,System.Boolean>checkHashValid  // static @ 0x38
  public    static  System.Action<System.String>    addPathSting  // static @ 0x40
PROPERTIES:
  enableLazyCloseFile  set=0x04D14940
  memoryBudgetKB  get=0x05395C80  set=0x0B3A2A28
METHODS:
  RVA=0x0B3A29E8  token=0x6000001  System.Void .ctor()
  RVA=0x0B3A2810  token=0x6000002  System.Void UnloadAllAssetBundles(System.Boolean unloadAllObjects)
  RVA=0x0B3A2054  token=0x6000003  UnityEngine.AssetBundle[] GetAllLoadedAssetBundles_Native()
  RVA=0x0B3A2080  token=0x6000004  System.Collections.Generic.IEnumerable<UnityEngine.AssetBundle> GetAllLoadedAssetBundles()
  RVA=0x0380AC00  token=0x6000005  System.Void WaitForAllAsyncOperationsToComplete()
  RVA=0x0380ABC0  token=0x6000006  System.Void WaitForAllAsyncOperationsToComplete_Internal()
  RVA=0x049925D0  token=0x6000007  System.Int32 GetBatchLoadFromFileIntegrateMaxCount_Internal()
  RVA=-1  // generic def  token=0x6000008  T[] CreateBatchLoadFromFileBuffer()
  RVA=0x0B3A2150  token=0x6000009  System.Void InvokeOnMissReferencedScriptOnBehaviour()
  RVA=0x0B3A2790  token=0x600000B  System.Boolean TryCloseLazyCloseFile_Internal(System.Boolean force)
  RVA=0x0B3A27C4  token=0x600000C  System.Boolean TryCloseLazyCloseFile(System.Boolean force)
  RVA=0x0B3A20C0  token=0x600000D  System.String HashToStringPath(System.Int64 hash)
  RVA=0x0B3A1FCC  token=0x600000E  System.Boolean CheckHashValid(System.Int64 hash)
  RVA=0x0B3A1F70  token=0x600000F  System.Void AddPathStringToCSharp(System.String path)
  RVA=0x02F73240  token=0x6000010  UnityEngine.AssetBundleCreateRequest LoadFromFileAsync_Internal(System.String path, System.UInt32 crc, System.UInt64 offset)
  RVA=0x0B3A2484  token=0x6000011  UnityEngine.AssetBundleCreateRequest LoadFromFileAsync(System.String path)
  RVA=0x02F731C0  token=0x6000012  UnityEngine.AssetBundleCreateRequest LoadFromFileAsync(System.String path, System.UInt32 crc, System.UInt64 offset)
  RVA=0x03BB36F0  token=0x6000013  UnityEngine.AssetBundle LoadFromFile_Internal(System.String path, System.UInt32 crc, System.UInt64 offset)
  RVA=0x0B3A24D4  token=0x6000014  UnityEngine.AssetBundle LoadFromFile(System.String path)
  RVA=0x03BB3670  token=0x6000015  UnityEngine.AssetBundle LoadFromFile(System.String path, System.UInt32 crc, System.UInt64 offset)
  RVA=0x0B3A2524  token=0x6000016  UnityEngine.AssetBundleCreateRequest LoadFromMemoryAsync_Internal(System.Byte[] binary, System.UInt32 crc)
  RVA=0x0B3A2564  token=0x6000017  UnityEngine.AssetBundleCreateRequest LoadFromMemoryAsync(System.Byte[] binary)
  RVA=-1  // generic def  token=0x6000018  T LoadAsset(System.String name)
  RVA=0x05393960  token=0x6000019  UnityEngine.Object LoadAsset(System.Int64 hash, System.Type type)
  RVA=0x0B3A2368  token=0x600001A  UnityEngine.Object LoadAsset(System.String name, System.Type type)
  RVA=0x0B3A2314  token=0x600001B  UnityEngine.Object LoadAsset_Internal(System.String name, System.Type type)
  RVA=0x0380DA20  token=0x600001C  UnityEngine.Object LoadAssetByHash_Internal(System.Int64 hash, System.Type type)
  RVA=-1  // generic def  token=0x600001D  UnityEngine.AssetBundleRequest LoadAssetAsync(System.String name)
  RVA=0x0B3A21F8  token=0x600001E  UnityEngine.AssetBundleRequest LoadAssetAsync(System.String name, System.Type type)
  RVA=0x0380D250  token=0x600001F  UnityEngine.AssetBundleRequest LoadAssetAsync(System.Int64 hash, System.Type type)
  RVA=0x0B3A2614  token=0x6000020  UnityEngine.AssetBundleRequest LoadSubAssetAsync(System.String name, System.Int64 subAssetInstanceID, System.Type type)
  RVA=0x0B3A21A4  token=0x6000021  UnityEngine.AssetBundleRequest LoadAssetAsync_Internal(System.String name, System.Type type)
  RVA=0x0380D2C0  token=0x6000022  UnityEngine.AssetBundleRequest LoadAssetAsyncByHash_Internal(System.Int64 hash, System.Type type)
  RVA=0x02F66030  token=0x6000023  System.Void Unload(System.Boolean unloadAllLoadedObjects)
  RVA=0x0B3A29A4  token=0x6000024  UnityEngine.AsyncOperation UnloadAsync(System.Boolean unloadAllLoadedObjects)
  RVA=0x0B3A2940  token=0x6000025  System.Void UnloadAsset(UnityEngine.Object asset)
  RVA=0x0B3A2888  token=0x6000026  UnityEngine.AsyncOperation UnloadAssetAsync(UnityEngine.Object asset)
  RVA=0x0B3A28FC  token=0x6000027  System.Void UnloadAsset_Internal(UnityEngine.Object asset)
  RVA=0x0B3A2844  token=0x6000028  UnityEngine.AsyncOperation UnloadAssetAsync_Internal(UnityEngine.Object asset)
  RVA=0x0B3A25B0  token=0x6000029  UnityEngine.AssetBundleRequest LoadSubAssetAsync_Internal(System.String name, System.Int64 instanceID, System.Type type)
  RVA=0x04B266A0  token=0x600002C  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.AssetBundleBatchCreateRequest
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x20
EXTENDS: UnityEngine.AsyncOperation
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.AssetBundleBatchRequest
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x20
EXTENDS: UnityEngine.AsyncOperation
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.AssetBundleCreateRequest
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x20
EXTENDS: UnityEngine.AsyncOperation
FIELDS:
PROPERTIES:
  assetBundle  get=0x0380BF40
METHODS:
  RVA=0x041E1670  token=0x600002E  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.AssetBundleLoadingCache
TYPE:  static class
TOKEN: 0x2000006
SIZE:  0x10
FIELDS:
PROPERTIES:
  maxBlocksPerFile  get=0x0B3A1F44  set=0x049BFB50
  blockCount  get=0x049BFAD0  set=0x049BFB10
  blockSize  get=0x049BF9C0
  memoryBudgetKB  get=0x049BF9A0  set=0x049BFA00
METHODS:
END_CLASS

CLASS: UnityEngine.AssetBundleRecompressOperation
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x20
EXTENDS: UnityEngine.AsyncOperation
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.AssetBundleRequest
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x30
EXTENDS: UnityEngine.ResourceRequest
FIELDS:
PROPERTIES:
  asset  get=0x0539396C
METHODS:
  RVA=0x0380D9E0  token=0x6000036  UnityEngine.Object GetResult()
  RVA=0x041E1670  token=0x6000038  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Experimental.AssetBundlePatching.AssetBundleUtility
TYPE:  static class
TOKEN: 0x2000009
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x03A18E70  token=0x6000039  System.Int32 LZ4Decompress(System.Void* src, System.Int32 srcSize, System.Void* dst, System.Int32 dstSize)
  RVA=0x03A18DF0  token=0x600003A  System.Int32 LZ4Decompress(System.Byte[] src, System.Int32 srcOffset, System.Int32 srcSize, System.Byte[] dst, System.Int32 dstOffset, System.Int32 dstSize)
END_CLASS

