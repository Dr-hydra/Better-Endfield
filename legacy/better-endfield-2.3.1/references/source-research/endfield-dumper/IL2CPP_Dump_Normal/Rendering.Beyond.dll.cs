// ========================================================
// Dumped by @desirepro
// Assembly: Rendering.Beyond.dll
// Classes:  153
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000002  // size: 0x38
    public class LDstaticObjectLightmappingDataLoader : UnityEngine.MonoBehaviour
    {
        // Fields
        public System.Boolean test;  // 0x18
        public System.Boolean recover;  // 0x19
        public UnityEngine.Renderer[] ldLightmappedRenderer;  // 0x20
        public System.Int32[] ldLightmappedRendererLightmapIndex;  // 0x28
        public UnityEngine.Vector4[] ldLightmappedRendererLightmapUvScale;  // 0x30

        // Methods
        // RVA: 0x0A9AD448  token: 0x6000001
        public System.Void RecoverLightmapdata() { }
        // RVA: 0x05393520  token: 0x6000002
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000F  // size: 0x14
    public sealed struct WarmUpReason
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.ShaderWarmupManager.WarmUpReason NoReason;  // const
        public static Beyond.Rendering.ShaderWarmupManager.WarmUpReason FirstEntryGame;  // const
        public static Beyond.Rendering.ShaderWarmupManager.WarmUpReason LodChange;  // const
        public static Beyond.Rendering.ShaderWarmupManager.WarmUpReason AssetUpdate;  // const
        public static Beyond.Rendering.ShaderWarmupManager.WarmUpReason AppUpdate;  // const
        public static Beyond.Rendering.ShaderWarmupManager.WarmUpReason GraphicsAPIChange;  // const
        public static Beyond.Rendering.ShaderWarmupManager.WarmUpReason ShaderCacheSmall;  // const
        public static Beyond.Rendering.ShaderWarmupManager.WarmUpReason ShaderCacheIOInvalid;  // const
        public static Beyond.Rendering.ShaderWarmupManager.WarmUpReason DevOverride;  // const
        public static Beyond.Rendering.ShaderWarmupManager.WarmUpReason CrashRetry;  // const
        public static Beyond.Rendering.ShaderWarmupManager.WarmUpReason PsoCacheCorrupted;  // const
        public static Beyond.Rendering.ShaderWarmupManager.WarmUpReason ShaderCacheApiInvalid;  // const
        public static Beyond.Rendering.ShaderWarmupManager.WarmUpReason DriverVersionUpdate;  // const
        public static Beyond.Rendering.ShaderWarmupManager.WarmUpReason RemoteCtrlRetry;  // const

    }

    // TypeToken: 0x2000010  // size: 0x14
    public sealed struct WarmupRunType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.ShaderWarmupManager.WarmupRunType None;  // const
        public static Beyond.Rendering.ShaderWarmupManager.WarmupRunType Login;  // const
        public static Beyond.Rendering.ShaderWarmupManager.WarmupRunType Download;  // const

    }

    // TypeToken: 0x2000011  // size: 0x20
    public class TimeCostCounter
    {
        // Fields
        private static System.Single MAX_DELTA_TIME;  // const
        private static System.Single MAX_DELTA_TIME_FIX;  // const
        private System.Single <duration>k__BackingField;  // 0x10
        private System.Single m_lastRealTime;  // 0x14
        private System.Single m_maxDurationSecond;  // 0x18

        // Properties
        System.Single duration { get; /* RVA: 0x04D85F70 */ set; /* RVA: 0x04D88280 */ }
        System.Boolean isTimeOut { get; /* RVA: 0x0A9AF2B8 */ }

        // Methods
        // RVA: 0x0A9AF2A4  token: 0x6000063
        public System.Void .ctor(System.Single maxDurationMilliSecond) { }
        // RVA: 0x0A9AF1D4  token: 0x6000064
        public System.Void Begin() { }
        // RVA: 0x0A9AF230  token: 0x6000065
        public System.Void Tick() { }

    }

    // TypeToken: 0x2000012  // size: 0x50
    public sealed class <WarmupShadersLogin>d__63 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.Boolean supportsRaytracing;  // 0x20
        private System.Collections.Generic.List<Beyond.Resource.FAssetProxyHandle> <assetProxyHandles>5__2;  // 0x28
        private System.Collections.Generic.List<UnityEngine.HGPsoWarmupCollection> <loadedAssets>5__3;  // 0x30
        private System.Int32 <unfinishedCount>5__4;  // 0x38
        private Beyond.Rendering.ShaderWarmupManager.TimeCostCounter <timeCostCounter>5__5;  // 0x40
        private System.Int32 <i>5__6;  // 0x48

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000066
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x02E5AD40  token: 0x6000067
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x02E58FD0  token: 0x6000068
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x02E5ADC0  token: 0x6000069
        private System.Void <>m__Finally1() { }
        // RVA: 0x02E5ACF0  token: 0x600006B
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000013  // size: 0x40
    public sealed class <WarmupShadersDownloading>d__64 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.Boolean supportsRaytracing;  // 0x20
        private System.Collections.Generic.List<Beyond.Resource.FAssetProxyHandle> <assetProxyHandles>5__2;  // 0x28
        private System.Collections.Generic.List<UnityEngine.HGPsoWarmupCollection> <loadedAssets>5__3;  // 0x30
        private System.Int32 <i>5__4;  // 0x38

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600006D
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x02E58B60  token: 0x600006E
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x02E57FD0  token: 0x600006F
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x02E58BE0  token: 0x6000070
        private System.Void <>m__Finally1() { }
        // RVA: 0x02E58B10  token: 0x6000072
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000014  // size: 0x20
    public sealed class <_StopDownloadingWarmupAndWait>d__66 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000074
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000075
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x02E5CBC0  token: 0x6000076
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x02E5CDC0  token: 0x6000078
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000015  // size: 0x38
    public sealed class <_LoadPsoAssets>d__71 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.Collections.Generic.List<Beyond.Resource.StringPathHash> resPathList;  // 0x20
        public System.Collections.Generic.List<Beyond.Resource.FAssetProxyHandle> assetProxyHandles;  // 0x28
        public System.Collections.Generic.List<UnityEngine.HGPsoWarmupCollection> loadedAssets;  // 0x30

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600007A
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600007B
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x02E5C460  token: 0x600007C
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x02E5CB70  token: 0x600007E
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000016  // size: 0x48
    public sealed class <WarmupShadersScene>d__72 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.String scene;  // 0x20
        private System.String <path>5__2;  // 0x28
        private Beyond.Resource.FAssetProxyHandle <assetHandle>5__3;  // 0x30

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000080
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000081
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x02E5AFD0  token: 0x6000082
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x02E5C410  token: 0x6000084
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000017  // size: 0x30
    public sealed class <WarmupOnePso>d__73 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public UnityEngine.HGPsoWarmupCollection psoC;  // 0x20
        public System.Int32 startIndex;  // 0x28
        public System.Int32 warmupCount;  // 0x2c

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000086
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000087
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x02E57AB0  token: 0x6000088
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x02E57F80  token: 0x600008A
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000018  // size: 0x58
    public sealed class <_WarmupOnePsoTimeLimited>d__74 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public UnityEngine.HGPsoWarmupCollection psoC;  // 0x20
        public System.Int32 startIndex;  // 0x28
        public System.Int32 desiredWarmupCount;  // 0x2c
        public Beyond.Rendering.ShaderWarmupManager.TimeCostCounter timeStart;  // 0x30
        private System.Int32 <totalCount>5__2;  // 0x38
        private System.Int32 <computeShaderCount>5__3;  // 0x3c
        private System.Int32 <warmupCountPerStepMainJobCompile>5__4;  // 0x40
        private System.Int32 <compileTotalCount>5__5;  // 0x44
        private System.Int32 <countToCleanUp>5__6;  // 0x48
        private System.Int32 <backJobLastLeftCount>5__7;  // 0x4c
        private System.Int32 <leftBackJobCount>5__8;  // 0x50

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600008C
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600008D
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x02E5D750  token: 0x600008E
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x02E5E2C0  token: 0x6000090
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000019  // size: 0x48
    public sealed class <_WarmupOnePsoBackgroundOnly>d__75 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public UnityEngine.HGPsoWarmupCollection psoC;  // 0x20
        public System.Int32 startIndex;  // 0x28
        public System.Int32 desiredWarmupCount;  // 0x2c
        private System.Int32 <totalCount>5__2;  // 0x30
        private System.Int32 <compileTotalCount>5__3;  // 0x34
        private System.Int32 <countToCleanUp>5__4;  // 0x38
        private System.Int32 <backJobLastLeftCount>5__5;  // 0x3c
        private System.Int32 <currentIndex>5__6;  // 0x40
        private System.Int32 <leftBackJobCount>5__7;  // 0x44

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000092
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000093
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x02E5CE10  token: 0x6000094
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x02E5D700  token: 0x6000096
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200001B  // size: 0x40
    public class ShaderWarmupRemoteBlackDevice
    {
        // Fields
        public System.String deviceModel;  // 0x10
        public System.String graphicsDeviceName;  // 0x18
        public System.String graphicsDeviceVersion;  // 0x20
        public System.String os;  // 0x28
        public System.String cpuName;  // 0x30
        public System.String disableFeature;  // 0x38

        // Methods
        // RVA: 0x041E1670  token: 0x600009D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001C  // size: 0x18
    public class ShaderWarmupGlobalConfig
    {
        // Fields
        public System.Boolean enableDriverVerDetect;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600009E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001D  // size: 0x20
    public class ShaderWarmupRemoteBlacklist
    {
        // Fields
        public System.Collections.Generic.List<Beyond.Rendering.ShaderWarmupManagerRemoteCfg.ShaderWarmupRemoteBlackDevice> devices;  // 0x10
        public Beyond.Rendering.ShaderWarmupManagerRemoteCfg.ShaderWarmupGlobalConfig config;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600009F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001F  // size: 0x48
    public sealed class Result
    {
        // Fields
        public System.String driverVersion;  // 0x10
        public System.String deviceName;  // 0x18
        public System.String deviceDesc;  // 0x20
        public System.String mfg;  // 0x28
        public System.Int32 score;  // 0x30
        public System.String driverKey;  // 0x38
        public System.String keyId;  // 0x40

        // Methods
        // RVA: 0x0A9AD784  token: 0x60000B4
        public virtual System.String ToString() { }
        // RVA: 0x041E1670  token: 0x60000B5
        public System.Void .ctor() { }
        // RVA: 0x0669AD6C  token: 0x60000B6
        public System.String <>iFixBaseProxy_ToString() { }

    }

    // TypeToken: 0x2000020  // size: 0x20
    public sealed class SafeRegistryHandle : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid
    {
        // Methods
        // RVA: 0x053948D4  token: 0x60000B7
        private System.Void .ctor() { }
        // RVA: 0x039B85E0  token: 0x60000B8
        private System.Void .ctor(System.IntPtr preexistingHandle, System.Boolean ownsHandle) { }
        // RVA: 0x0A9AD91C  token: 0x60000B9
        protected virtual System.Boolean ReleaseHandle() { }

    }

    // TypeToken: 0x2000021  // size: 0x14
    public sealed struct RegistryRights
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.WindowsGpuDriverVersion.RegistryRights KEY_QUERY_VALUE;  // const

    }

    // TypeToken: 0x2000022  // size: 0x30
    public sealed struct SP_DEVINFO_DATA
    {
        // Fields
        public System.UInt32 cbSize;  // 0x10
        public System.Guid classGuid;  // 0x14
        public System.UInt32 devInst;  // 0x24
        public System.IntPtr reserved;  // 0x28

    }

    // TypeToken: 0x2000023  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Beyond.Rendering.WindowsGpuDriverVersion.<>c <>9;  // static @ 0x0
        public static System.Comparison<Beyond.Rendering.WindowsGpuDriverVersion.Result> <>9__18_0;  // static @ 0x8

        // Methods
        // RVA: 0x0A9AF334  token: 0x60000BA
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60000BB
        public System.Void .ctor() { }
        // RVA: 0x0A9AF30C  token: 0x60000BC
        private System.Int32 <GetAllDisplayDriverVersions>b__18_0(Beyond.Rendering.WindowsGpuDriverVersion.Result a, Beyond.Rendering.WindowsGpuDriverVersion.Result b) { }

    }

    // TypeToken: 0x2000027  // size: 0x30
    public sealed struct ChunkData
    {
        // Fields
        public UnityEngine.Vector3[] centers;  // 0x10
        public UnityEngine.Vector3[] extents;  // 0x18
        public UnityEngine.Vector3[] vertexData;  // 0x20
        public System.Single[] areas;  // 0x28

    }

    // TypeToken: 0x2000028  // size: 0x28
    public sealed struct SubSceneDataPerSceneStateArea
    {
        // Fields
        public UnityEngine.Vector2Int[] chunkCoords;  // 0x10
        public Beyond.Rendering.HGSOCSubSceneData.ChunkData[] chunkDataArray;  // 0x18
        public System.Int32[] explicitLayers;  // 0x20

    }

    // TypeToken: 0x200002D  // size: 0x30
    public sealed struct MeshRendererToSync
    {
        // Fields
        public UnityEngine.Transform captureTransform;  // 0x10
        public UnityEngine.Renderer captureRenderer;  // 0x18
        public UnityEngine.Transform followTransform;  // 0x20
        public UnityEngine.Renderer followRenderer;  // 0x28

    }

    // TypeToken: 0x200002E  // size: 0x28
    public class CaptureRenderGameObject
    {
        // Fields
        public UnityEngine.GameObject gameObject;  // 0x10
        public UnityEngine.MeshRenderer meshRenderer;  // 0x18
        public UnityEngine.MeshFilter meshFilter;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x60000DF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000031  // size: 0x58
    public class RendererInfo
    {
        // Fields
        private UnityEngine.Renderer m_renderer;  // 0x10
        private System.String m_rendererName;  // 0x18
        public Beyond.Rendering.EntityVFXRendererMask typeMask;  // 0x20
        public System.Int32 subMeshCount;  // 0x24
        public UnityEngine.Rendering.ShadowCastingMode oriShadowCastingMode;  // 0x28
        public UnityEngine.Experimental.Rendering.RayTracingMode oriRayTracingMode;  // 0x2c
        public UnityEngine.Material[] sourceMaterials;  // 0x30
        public System.Boolean materialReplacing;  // 0x38
        public UnityEngine.Material[] replacingMaterials;  // 0x40
        public UnityEngine.Vector4 customPerDrawData0;  // 0x48

        // Properties
        UnityEngine.Material[] currentMeshMaterials { get; /* RVA: 0x0302F140 */ }
        System.Boolean rendererValid { get; /* RVA: 0x02E61B10 */ }
        System.String rendererName { get; /* RVA: 0x02E61800 */ }

        // Methods
        // RVA: 0x02E5E700  token: 0x6000103
        public System.Boolean IsRendererValid() { }
        // RVA: 0x02E5E910  token: 0x6000104
        public System.Boolean TryBeginMaterialUpdate() { }
        // RVA: 0x02E5F1C0  token: 0x6000105
        public System.Boolean TryInit(UnityEngine.Renderer renderer, System.Collections.Generic.List<Beyond.Rendering.EntityRendererTypeConfig> configs) { }
        // RVA: 0x02E60FB0  token: 0x6000106
        private System.Void _Init(UnityEngine.Renderer renderer, System.Collections.Generic.List<Beyond.Rendering.EntityRendererTypeConfig> configs) { }
        // RVA: 0x02E60840  token: 0x6000107
        public System.Boolean TryStopShadowCasting(System.Boolean stopRayTracing) { }
        // RVA: 0x02E5F7F0  token: 0x6000108
        public System.Boolean TryRevertShadowCastingMode() { }
        // RVA: 0x02E5EB90  token: 0x6000109
        public System.Boolean TryFillRendererTypeMask(System.Collections.Generic.Dictionary<UnityEngine.Renderer,Beyond.Rendering.EntityVFXRendererMask> target) { }
        // RVA: 0x02E5FE60  token: 0x600010A
        public System.Boolean TrySetManualDitherAlphaValue(System.Single value) { }
        // RVA: 0x02E5FFE0  token: 0x600010B
        public System.Boolean TrySetManualDitherEnable(System.Boolean enable, System.Boolean& allRendererEnabled) { }
        // RVA: 0x02E60260  token: 0x600010C
        public System.Boolean TrySetSharedMaterial(UnityEngine.Material material) { }
        // RVA: 0x02E603D0  token: 0x600010D
        public System.Boolean TrySetSharedMaterials(UnityEngine.Material[] materials) { }
        // RVA: 0x02E5F360  token: 0x600010E
        public System.Boolean TryReplaceSharedMaterials(UnityEngine.Material[] newMaterials) { }
        // RVA: 0x02E5EE60  token: 0x600010F
        public System.Boolean TryGetRendererPosition(UnityEngine.Vector3& position) { }
        // RVA: 0x02E5ECC0  token: 0x6000110
        public System.Boolean TryGetRendererBounds(UnityEngine.Bounds& bounds) { }
        // RVA: 0x02E5E9F0  token: 0x6000111
        public System.Boolean TryEncapsulateBounds(UnityEngine.Bounds& bounds) { }
        // RVA: 0x02E5F980  token: 0x6000112
        public System.Boolean TrySetCharacterPerDrawData(UnityEngine.Vector4 value) { }
        // RVA: 0x02E5FBF0  token: 0x6000113
        public System.Boolean TrySetEmissiveColor(UnityEngine.Vector4 value) { }
        // RVA: 0x02E609F0  token: 0x6000114
        private static System.Int32 _GetSubMeshCount(UnityEngine.Renderer renderer) { }
        // RVA: 0x041E1670  token: 0x6000115
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000032  // size: 0x40
    public sealed struct AddedMaterialInfo
    {
        // Fields
        public System.Int32 handle;  // 0x10
        public UnityEngine.Material material;  // 0x18
        public Beyond.Rendering.EntityVFXRendererMask mask;  // 0x20
        public Beyond.Rendering.AddMaterialConfig config;  // 0x28
        public System.Collections.Generic.Dictionary<UnityEngine.Material,UnityEngine.Material> customizeMaterialDict;  // 0x38

    }

    // TypeToken: 0x2000033  // size: 0x18
    public sealed struct SourceMaterialCustomizeData
    {
        // Fields
        public UnityEngine.Texture colorTexture;  // 0x10

    }

    // TypeToken: 0x2000034  // size: 0x58
    public class AddedMaterialController
    {
        // Fields
        public static System.Int32 MAX_ACTIVE_ADDED_MATERIAL;  // const
        public System.Collections.Generic.List<Beyond.Rendering.EntityRenderHelperMaterialController.AddedMaterialInfo> addedMaterialInfos;  // 0x10
        private System.Collections.Generic.Dictionary<System.Int32,System.Single> m_addictiveFloatProperties;  // 0x18
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Vector4> m_addictiveVectorProperties;  // 0x20
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Texture> m_addictiveTextureProperties;  // 0x28
        private System.Boolean m_useCustomizeRendererProperty;  // 0x30
        private Beyond.Rendering.EntityCustomizeRendererPropertyConfig m_customizeRendererPropertyConfig;  // 0x38
        private System.Boolean m_initedCustomizeRendererPropertyConfig;  // 0x40
        private System.Collections.Generic.Dictionary<UnityEngine.Material,Beyond.Rendering.EntityRenderHelperMaterialController.SourceMaterialCustomizeData> m_sourceMaterialCustomizeDatas;  // 0x48
        private System.Collections.Generic.List<Beyond.Rendering.EntityRenderHelperMaterialController.RendererInfo> m_rendererInfos;  // 0x50
        private static System.Int32 s_handleCount;  // static @ 0x0

        // Methods
        // RVA: 0x046B5900  token: 0x6000116
        public System.Void Init(System.Collections.Generic.List<Beyond.Rendering.EntityRenderHelperMaterialController.RendererInfo> rendererInfos, Beyond.Rendering.EntityCustomizeRendererPropertyConfig customizeRendererPropertyConfig) { }
        // RVA: 0x0A9B1F7C  token: 0x6000117
        private System.Void _InitCustomizeRendererProperty() { }
        // RVA: 0x046E8E80  token: 0x6000118
        public System.Void Reset() { }
        // RVA: 0x03578AF0  token: 0x6000119
        public System.Int32 RequireAddMaterial(UnityEngine.Material material, Beyond.Rendering.EntityVFXRendererMask mask, Beyond.Rendering.AddMaterialConfig config) { }
        // RVA: 0x03577020  token: 0x600011A
        public Beyond.Rendering.EntityVFXRendererMask RequireRemoveMaterial(System.Int32 handle) { }
        // RVA: 0x0A9B1DF8  token: 0x600011B
        private UnityEngine.Material _GetCustomizePropertyMaterial(Beyond.Rendering.EntityRenderHelperMaterialController.AddedMaterialInfo addedMaterialInfo, UnityEngine.Material material) { }
        // RVA: 0x0302EBF0  token: 0x600011C
        public System.Void ApplySharedMaterialArray(Beyond.Rendering.EntityRenderHelperMaterialController.RendererInfo rendererInfo) { }
        // RVA: 0x0A9B16B0  token: 0x600011D
        public System.Boolean GetAddedMaterialsOfCustomizeProperties(System.Int32 handle, System.Collections.Generic.List<UnityEngine.Material> result) { }
        // RVA: 0x0A9B1B98  token: 0x600011E
        public System.Void SetAddictiveFloatProperty(System.Int32 propertyId, System.Single value) { }
        // RVA: 0x0A9B1994  token: 0x600011F
        public System.Single GetAddictiveFloatProperty(System.Int32 propertyId, System.Single defaultValue) { }
        // RVA: 0x0A9B1D20  token: 0x6000120
        public System.Void SetAddictiveVectorProperty(System.Int32 propertyId, UnityEngine.Vector4 value) { }
        // RVA: 0x0A9B1ACC  token: 0x6000121
        public UnityEngine.Vector4 GetAddictiveVectorProperty(System.Int32 propertyId, UnityEngine.Vector4 defaultValue) { }
        // RVA: 0x0A9B1C5C  token: 0x6000122
        public System.Void SetAddictiveTextureProperty(System.Int32 propertyId, UnityEngine.Texture value) { }
        // RVA: 0x0A9B1A30  token: 0x6000123
        public UnityEngine.Texture GetAddictiveTextureProperty(System.Int32 propertyId, UnityEngine.Texture defaultValue) { }
        // RVA: 0x041E1670  token: 0x6000124
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000035  // size: 0x20
    public class CustomPerDrawDataController
    {
        // Fields
        private System.Collections.Generic.List<Beyond.Rendering.EntityRenderHelperMaterialController.RendererInfo> m_rendererInfos;  // 0x10
        private System.Single m_characterEnvironmentEffectPackedValue;  // 0x18

        // Methods
        // RVA: 0x03522FF0  token: 0x6000125
        private System.Single _PackUNorm8ToFloat(System.Single packed, System.Single value, System.Int32 bitShift) { }
        // RVA: 0x04825740  token: 0x6000126
        public System.Void Init(System.Collections.Generic.List<Beyond.Rendering.EntityRenderHelperMaterialController.RendererInfo> allRendererInfos) { }
        // RVA: 0x0415F9E0  token: 0x6000127
        public System.Void Reset() { }
        // RVA: 0x0323AF90  token: 0x6000128
        public System.Void SetFloatToCustomPerDraw(Beyond.Rendering.EntityRenderHelperCustomPerDrawType type, System.Single value, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: 0x0A9B2B98  token: 0x6000129
        public System.Void SetVectorToCustomPerDraw(Beyond.Rendering.EntityRenderHelperVectorCustomPerDrawType type, UnityEngine.Vector4 value, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: 0x041E1670  token: 0x600012A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000036  // size: 0x50
    public class MeshMaterialGroupData
    {
        // Fields
        public Beyond.Rendering.EntityVFXRendererMask mask;  // 0x10
        public System.Collections.Generic.List<Beyond.Rendering.EntityRenderHelperMaterialController.RendererInfo> rendererInfos;  // 0x18
        public System.Collections.Generic.Dictionary<System.Int32,System.Single> floatProperties;  // 0x20
        public System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Vector4> vectorProperties;  // 0x28
        public System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Texture> textureProperties;  // 0x30
        public System.Collections.Generic.Dictionary<System.String,System.Boolean> keywordStates;  // 0x38
        public System.Boolean depthOnlyPassControlling;  // 0x40
        public System.Collections.Generic.Dictionary<UnityEngine.Material,UnityEngine.Material> src2dstMaterialDict;  // 0x48

        // Methods
        // RVA: 0x04770870  token: 0x600012B
        public System.Void CopyFrom(Beyond.Rendering.EntityRenderHelperMaterialController.MeshMaterialGroupData src) { }
        // RVA: 0x0369B780  token: 0x600012C
        public System.Void Clear() { }
        // RVA: 0x041E1670  token: 0x600012D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000037  // size: 0x18
    public class MeshMaterialController
    {
        // Fields
        private System.Collections.Generic.List<Beyond.Rendering.EntityRenderHelperMaterialController.MeshMaterialGroupData> m_meshMaterialDatas;  // 0x10
        private static System.Collections.Generic.List<Beyond.Rendering.EntityRenderHelperMaterialController.MeshMaterialGroupData> s_cacheMeshMaterialDatas;  // static @ 0x0
        private static readonly System.String VFX_MATERIAL_POSTFIX;  // static @ 0x8

        // Methods
        // RVA: 0x03699A70  token: 0x600012E
        public System.Void Init(System.Collections.Generic.List<Beyond.Rendering.EntityRenderHelperMaterialController.RendererInfo> allRendererInfos) { }
        // RVA: 0x0369AED0  token: 0x600012F
        public System.Void Reset() { }
        // RVA: 0x035758F0  token: 0x6000130
        public System.Void SetKeywordEnableToAllMaterial(System.String keyword, System.Boolean enable, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: 0x0A9B70F4  token: 0x6000131
        public System.Void SetDepthOnlyPassEnableToAllMaterial(System.Boolean enable, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: 0x034DFDA0  token: 0x6000132
        public System.Void SetFloatToAllMaterial(System.Int32 nameId, System.Single value, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: 0x0A9B6C5C  token: 0x6000133
        public System.Void ReleaseFloatToAllMaterial(System.Int32 nameId, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: 0x0357B570  token: 0x6000134
        public System.Void SetVectorToAllMaterial(System.Int32 nameId, UnityEngine.Vector4 value, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: 0x0A9B6F6C  token: 0x6000135
        public System.Void ReleaseVectorToAllMaterial(System.Int32 nameId, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: 0x039411C0  token: 0x6000136
        public System.Void SetTextureToAllMaterial(System.Int32 nameId, UnityEngine.Texture value, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: 0x0A9B6DE4  token: 0x6000137
        public System.Void ReleaseTextureToAllMaterial(System.Int32 nameId, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: 0x03574C40  token: 0x6000138
        private System.Void _CreateReplaceMaterial(Beyond.Rendering.EntityRenderHelperMaterialController.RendererInfo rendererInfo, Beyond.Rendering.EntityRenderHelperMaterialController.MeshMaterialGroupData groupData) { }
        // RVA: 0x02E5E5A0  token: 0x6000139
        private System.Boolean _TryEnsureReplaceMaterial(Beyond.Rendering.EntityRenderHelperMaterialController.RendererInfo rendererInfo, Beyond.Rendering.EntityRenderHelperMaterialController.MeshMaterialGroupData groupData) { }
        // RVA: 0x04656720  token: 0x600013A
        private System.Void _CopyReplaceMaterial(Beyond.Rendering.EntityRenderHelperMaterialController.RendererInfo rendererInfo, Beyond.Rendering.EntityRenderHelperMaterialController.MeshMaterialGroupData groupData) { }
        // RVA: 0x0369B370  token: 0x600013B
        private System.Void _ReleaseAllReplaceMaterial(Beyond.Rendering.EntityRenderHelperMaterialController.MeshMaterialGroupData groupData) { }
        // RVA: 0x035751C0  token: 0x600013C
        private System.Void _ReplaceSharedMaterial(Beyond.Rendering.EntityRenderHelperMaterialController.RendererInfo rendererInfo, UnityEngine.Material[] newMaterials) { }
        // RVA: 0x0369B2A0  token: 0x600013D
        private System.Void _GetAffectGroupData(Beyond.Rendering.EntityVFXRendererMask rendererMask, System.Collections.Generic.List<Beyond.Rendering.EntityRenderHelperMaterialController.MeshMaterialGroupData> result) { }
        // RVA: 0x032C5570  token: 0x600013E
        private System.Void _SplitRenderers(System.Int32 oldStateIndex, Beyond.Rendering.EntityVFXRendererMask newMask) { }
        // RVA: 0x0A9B79D4  token: 0x600013F
        public System.Void SetZTestInMaterial(System.String shaderName, System.Boolean enable) { }
        // RVA: 0x041E1670  token: 0x6000140
        public System.Void .ctor() { }
        // RVA: 0x04D27FD0  token: 0x6000141
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200003B  // size: 0x28
    public class RendererInfo
    {
        // Fields
        private UnityEngine.Renderer m_renderer;  // 0x10
        private System.String <rendererName>k__BackingField;  // 0x18
        public System.Boolean oriVisible;  // 0x20
        public System.Boolean curVisible;  // 0x21

        // Properties
        System.String rendererName { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x02E61C50 */ }
        System.Boolean rendererValid { get; /* RVA: 0x02E619D0 */ }

        // Methods
        // RVA: 0x02E5F050  token: 0x6000150
        public System.Boolean TryInit(UnityEngine.Renderer renderer) { }
        // RVA: 0x02E60D40  token: 0x6000151
        private System.Void _Init(UnityEngine.Renderer renderer) { }
        // RVA: 0x02E5E7E0  token: 0x6000152
        public System.Boolean TryAddToDictionary(System.Collections.Generic.Dictionary<UnityEngine.Renderer,Beyond.Rendering.EntityRenderHelperVisibleController.RendererInfo> dictionary) { }
        // RVA: 0x02E606B0  token: 0x6000153
        public System.Boolean TrySetVisible(System.Boolean visible) { }
        // RVA: 0x02E60540  token: 0x6000154
        public System.Boolean TrySetVisibleByRenderer(System.Boolean visible) { }
        // RVA: 0x02E5F6D0  token: 0x6000155
        public System.Boolean TryResetVisible() { }
        // RVA: 0x041E1670  token: 0x6000156
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000044  // size: 0x14
    public sealed struct TimeMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.EntityVFXAfterImageData.TimeMode Interval;  // const
        public static Beyond.Rendering.EntityVFXAfterImageData.TimeMode Burst;  // const

    }

    // TypeToken: 0x2000052  // size: 0x14
    public sealed struct State
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.EntityVFXControllerStateBase.State End;  // const
        public static Beyond.Rendering.EntityVFXControllerStateBase.State Start;  // const
        public static Beyond.Rendering.EntityVFXControllerStateBase.State Loop;  // const
        public static Beyond.Rendering.EntityVFXControllerStateBase.State Ending;  // const

    }

    // TypeToken: 0x2000058  // size: 0x14
    public sealed struct UVSet
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.EntityVFXDissolveData.UVSet UV;  // const
        public static Beyond.Rendering.EntityVFXDissolveData.UVSet UV2;  // const

    }

    // TypeToken: 0x200005C  // size: 0x28
    public class VATData
    {
        // Fields
        public UnityEngine.Texture factoryVATMap;  // 0x10
        public UnityEngine.Vector4 factoryVATMapParams;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60001C0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005D  // size: 0x30
    public class AddedMaterialData
    {
        // Fields
        public UnityEngine.Material srcMaterial;  // 0x10
        public UnityEngine.Material[] materialInstancesOfEachVAT;  // 0x18
        public System.Collections.Generic.Dictionary<System.Int32,System.Single> savedFloatProperties;  // 0x20
        public System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Vector4> savedVectorProperties;  // 0x28

        // Methods
        // RVA: 0x0A9B2218  token: 0x60001C1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005E  // size: 0x18
    public sealed class <>c__DisplayClass12_0
    {
        // Fields
        public UnityEngine.Material src;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60001C2
        public System.Void .ctor() { }
        // RVA: 0x06C8C288  token: 0x60001C3
        private System.Boolean <_GetAddedMaterialIndex>b__0(Beyond.Rendering.EntityVFXFactoryAddedMaterialManager.AddedMaterialData data) { }

    }

    // TypeToken: 0x200005F  // size: 0x18
    public sealed class <>c__DisplayClass13_0
    {
        // Fields
        public UnityEngine.Material src;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60001C4
        public System.Void .ctor() { }
        // RVA: 0x06C8C288  token: 0x60001C5
        private System.Boolean <_RemoveVATMaterial>b__0(Beyond.Rendering.EntityVFXFactoryAddedMaterialManager.AddedMaterialData data) { }

    }

    // TypeToken: 0x2000061  // size: 0x20
    public class VATMaterialData
    {
        // Fields
        public System.Int32 refCount;  // 0x10
        public UnityEngine.Material vatMaterial;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60001C9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000063  // size: 0x14
    public sealed struct Type
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.EntityVFXSetParamsData.Type Character;  // const
        public static Beyond.Rendering.EntityVFXSetParamsData.Type VFX;  // const
        public static Beyond.Rendering.EntityVFXSetParamsData.Type Lit;  // const

    }

    // TypeToken: 0x2000072  // size: 0x30
    public class PsoInfoWrapperItem
    {
        // Fields
        public System.String infoJson;  // 0x10
        public System.Single time;  // 0x18
        public System.Int32 codeSize;  // 0x1c
        public System.Boolean isMissing;  // 0x20
        public System.String levelId;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x600023F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000073  // size: 0x58
    public class PsoInfoWrapper
    {
        // Fields
        public UnityEngine.RuntimePlatform platform;  // 0x10
        public System.String lastCL;  // 0x18
        public System.String lastEngineCl;  // 0x20
        public System.String deviceModel;  // 0x28
        public System.String devVersion;  // 0x30
        public System.String branch;  // 0x38
        public UnityEngine.Rendering.GraphicsDeviceType graphicsDeviceType;  // 0x40
        public System.Int32 shaderLod;  // 0x44
        public System.Int32 infoVersion;  // 0x48
        public System.Boolean isDebug;  // 0x4c
        public System.Collections.Generic.List<Beyond.Rendering.PsoCreateUploadHelper.PsoInfoWrapperItem> psoInfoWrapperItems;  // 0x50

        // Methods
        // RVA: 0x0A9B85F0  token: 0x6000240
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000074  // size: 0x28
    public sealed class <>c__DisplayClass36_0
    {
        // Fields
        public System.String path;  // 0x10
        public System.String serverFileName;  // 0x18
        public Beyond.Rendering.PsoCreateUploadHelper.PsoInfoWrapper psoInfoWrapper;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000241
        public System.Void .ctor() { }
        // RVA: 0x02E61CB0  token: 0x6000242
        private System.Void <_CreateAndUploadFinalJson>b__0(System.Object param) { }

    }

    // TypeToken: 0x2000075  // size: 0x30
    public sealed class <>c__DisplayClass42_0
    {
        // Fields
        public System.String urlPath;  // 0x10
        public System.String rawFilePath;  // 0x18
        public System.String serverFileNameOverride;  // 0x20
        public System.Action<System.Boolean,System.String,System.String> callback;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000243
        public System.Void .ctor() { }
        // RVA: 0x02E61D00  token: 0x6000244
        private System.Void <_UploadFile>b__0(System.Object param) { }

    }

    // TypeToken: 0x2000076  // size: 0x28
    public sealed class <>c__DisplayClass43_0
    {
        // Fields
        public System.Action<System.Boolean,System.String,System.String> callback;  // 0x10
        public System.String fileServerUrl;  // 0x18
        public System.String errorMessage;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000245
        public System.Void .ctor() { }
        // RVA: 0x02E61D70  token: 0x6000246
        private System.Void <_UploadFileToServer>b__1(System.Object _) { }
        // RVA: 0x02E61D70  token: 0x6000247
        private System.Void <_UploadFileToServer>b__2(System.Object _) { }

    }

    // TypeToken: 0x2000077  // size: 0x20
    public sealed class <>c__DisplayClass43_1
    {
        // Fields
        public System.String result;  // 0x10
        public Beyond.Rendering.PsoCreateUploadHelper.<>c__DisplayClass43_0 CS$<>8__locals1;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000248
        public System.Void .ctor() { }
        // RVA: 0x02E61DB0  token: 0x6000249
        private System.Void <_UploadFileToServer>b__0(System.Object _) { }

    }

    // TypeToken: 0x2000078  // size: 0xA0
    public sealed struct <_UploadFileToServer>d__43 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncVoidMethodBuilder <>t__builder;  // 0x18
        public System.Action<System.Boolean,System.String,System.String> callback;  // 0x38
        public System.String urlPath;  // 0x40
        public System.String serverFileNameOverride;  // 0x48
        public System.String rawFilePath;  // 0x50
        private Beyond.Rendering.PsoCreateUploadHelper.<>c__DisplayClass43_1 <>8__1;  // 0x58
        private Beyond.Rendering.PsoCreateUploadHelper.<>c__DisplayClass43_0 <>8__2;  // 0x60
        private System.String <filePathServerUrl>5__2;  // 0x68
        private System.IO.Stream <fileStream>5__3;  // 0x70
        private System.Net.Http.HttpClient <httpClient>5__4;  // 0x78
        private System.Net.Http.MultipartFormDataContent <content>5__5;  // 0x80
        private System.Net.Http.HttpResponseMessage <response>5__6;  // 0x88
        private System.Runtime.CompilerServices.TaskAwaiter<System.Net.Http.HttpResponseMessage> <>u__1;  // 0x90
        private System.Runtime.CompilerServices.TaskAwaiter<System.String> <>u__2;  // 0x98

        // Methods
        // RVA: 0x02E61DF0  token: 0x600024A
        private virtual System.Void MoveNext() { }
        // RVA: 0x02A58B10  token: 0x600024B
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x200007A  // size: 0x38
    public sealed class <>c__DisplayClass41_0
    {
        // Fields
        public System.Single[,] sdf;  // 0x10
        public System.Collections.Generic.HashSet<System.Int32> hashList;  // 0x18
        public System.Int32 xCount;  // 0x20
        public System.Collections.Generic.Queue<UnityEngine.Vector2Int> pendingList;  // 0x28
        public System.Action<System.Int32,System.Int32> <>9__0;  // 0x30

        // Methods
        // RVA: 0x041E1670  token: 0x6000261
        public System.Void .ctor() { }
        // RVA: 0x0A9C5BE8  token: 0x6000262
        private System.Void <_CalculateEdgeSignedDistance>b__0(System.Int32 tx, System.Int32 tz) { }

    }

    // TypeToken: 0x200007B  // size: 0x28
    public sealed class <>c__DisplayClass41_1
    {
        // Fields
        public System.Int32 ix;  // 0x10
        public System.Int32 iz;  // 0x14
        public System.Single minDistance;  // 0x18
        public Beyond.Rendering.VFXSludgeBaker.<>c__DisplayClass41_0 CS$<>8__locals1;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000263
        public System.Void .ctor() { }
        // RVA: 0x0A9C5C8C  token: 0x6000264
        private System.Void <_CalculateEdgeSignedDistance>b__1(System.Int32 tx, System.Int32 tz) { }

    }

    // TypeToken: 0x200007D  // size: 0x10
    public static class BoundsUtil
    {
        // Methods
        // RVA: 0x0A9BBBE4  token: 0x6000266
        public static System.Void GetTopBottomPoints(UnityEngine.Bounds bounds, UnityEngine.Transform t, UnityEngine.Vector3& topPoint, UnityEngine.Vector3& bottomPoint, System.Nullable<UnityEngine.Vector3> upDir) { }

    }

    // TypeToken: 0x200007F  // size: 0x2C
    public sealed struct Frame
    {
        // Fields
        public UnityEngine.Vector3 origin;  // 0x10
        public UnityEngine.Quaternion rot;  // 0x1c

        // Methods
        // RVA: 0x04D91FA0  token: 0x6000272
        public System.Void .ctor(UnityEngine.Vector3 origin, UnityEngine.Quaternion rot) { }
        // RVA: 0x0A9BC02C  token: 0x6000273
        public UnityEngine.Vector3 WorldToFrame(UnityEngine.Vector3 pWorld) { }
        // RVA: 0x0A9BBF38  token: 0x6000274
        public UnityEngine.Vector3 FrameToWorld(UnityEngine.Vector3 pFrame) { }

    }

    // TypeToken: 0x2000080  // size: 0x14
    public sealed struct EdgeMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.VFXSludgeNavUtils.Spline2DProjectAndSimplify.EdgeMode UseSplineOrder;  // const
        public static Beyond.Rendering.VFXSludgeNavUtils.Spline2DProjectAndSimplify.EdgeMode ConvexHull;  // const

    }

    // TypeToken: 0x2000081  // size: 0x40
    public class ProcessResult
    {
        // Fields
        public System.Collections.Generic.List<UnityEngine.Vector3> samples3D;  // 0x10
        public System.Collections.Generic.List<UnityEngine.Vector2> projected2D;  // 0x18
        public System.Collections.Generic.List<System.Int32> edgeIndices;  // 0x20
        public System.Collections.Generic.List<System.Int32> simplifiedIndices;  // 0x28
        public System.Single worldMinY;  // 0x30
        public System.Single worldMaxY;  // 0x34
        public System.Single frameMinY;  // 0x38
        public System.Single frameMaxY;  // 0x3c

        // Methods
        // RVA: 0x041E1670  token: 0x6000275
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000082  // size: 0x18
    public sealed class <>c__DisplayClass6_0
    {
        // Fields
        public System.Collections.Generic.List<UnityEngine.Vector2> pts;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000276
        public System.Void .ctor() { }
        // RVA: 0x0A9C5DCC  token: 0x6000277
        private System.Int32 <ConvexHullIndices>b__0(System.Int32 i, System.Int32 j) { }

    }

    // TypeToken: 0x200007E  // size: 0x10
    public static class Spline2DProjectAndSimplify
    {
        // Methods
        // RVA: 0x0A9C3A88  token: 0x6000267
        public static Beyond.Rendering.VFXSludgeNavUtils.Spline2DProjectAndSimplify.Frame MakeFrameFromPlaneNormal(UnityEngine.Vector3 planeOrigin, UnityEngine.Vector3 planeNormal) { }
        // RVA: 0x0A9C39C4  token: 0x6000268
        public static Beyond.Rendering.VFXSludgeNavUtils.Spline2DProjectAndSimplify.Frame MakeFrameByYaw(UnityEngine.Vector3 origin, System.Single yawDeg) { }
        // RVA: 0x0A9C42FC  token: 0x6000269
        public static System.Collections.Generic.List<UnityEngine.Vector3> SampleUniformByArcLength(UnityEngine.Splines.SplineContainer container, System.Single stepMeters, System.Int32 splineIndex, System.Boolean closed, System.Single oversample) { }
        // RVA: 0x0A9C4088  token: 0x600026A
        public static System.Void ProjectWorldToFrameXZ(System.Collections.Generic.List<UnityEngine.Vector3> worldPts, Beyond.Rendering.VFXSludgeNavUtils.Spline2DProjectAndSimplify.Frame frame, System.Collections.Generic.List<UnityEngine.Vector2>& projected2D, System.Collections.Generic.List<System.Single>& frameYs, System.Single& worldMinY, System.Single& worldMaxY, System.Single& frameMinY, System.Single& frameMaxY) { }
        // RVA: 0x0A9C347C  token: 0x600026B
        public static System.Collections.Generic.List<System.Int32> ConvexHullIndices(System.Collections.Generic.List<UnityEngine.Vector2> pts) { }
        // RVA: 0x0A9C4C64  token: 0x600026C
        private static System.Single _AngleDegAt(System.Collections.Generic.List<UnityEngine.Vector2> p, System.Int32 i, System.Boolean closed) { }
        // RVA: 0x0A9C4E98  token: 0x600026D
        private static System.Single _CurvatureAt(System.Collections.Generic.List<UnityEngine.Vector2> p, System.Int32 i, System.Boolean closed) { }
        // RVA: 0x0A9C5178  token: 0x600026E
        private static System.Single _PointToSegmentDistance2D(UnityEngine.Vector2 a, UnityEngine.Vector2 b, UnityEngine.Vector2 p) { }
        // RVA: 0x0A9C4834  token: 0x600026F
        public static System.Collections.Generic.List<System.Int32> Simplify2D_ByCurvatureAngleDeviation(System.Collections.Generic.List<UnityEngine.Vector2> poly, System.Boolean closed, System.Single angleDegThresh, System.Single deltaKappaThresh, System.Single devEps) { }
        // RVA: 0x0A9C3BB0  token: 0x6000270
        public static Beyond.Rendering.VFXSludgeNavUtils.Spline2DProjectAndSimplify.ProcessResult Process(UnityEngine.Splines.SplineContainer container, Beyond.Rendering.VFXSludgeNavUtils.Spline2DProjectAndSimplify.Frame frame, System.Single stepMeters, System.Int32 splineIndex, System.Boolean splineClosed, Beyond.Rendering.VFXSludgeNavUtils.Spline2DProjectAndSimplify.EdgeMode edgeMode, System.Single angleDegThresh, System.Single deltaKappaThresh, System.Single devEpsFactor) { }
        // RVA: 0x0A9C4C0C  token: 0x6000271
        private static System.Single <ConvexHullIndices>g__Cross|6_1(UnityEngine.Vector2 a, UnityEngine.Vector2 b, UnityEngine.Vector2 c) { }

    }

    // TypeToken: 0x2000084  // size: 0x30
    public sealed struct <>c__DisplayClass0_0
    {
        // Fields
        public System.Int32 lutResolution;  // 0x10
        public UnityEngine.Splines.SplineContainer container;  // 0x18
        public System.Int32 splineIndex;  // 0x20
        public System.Boolean worldSpace;  // 0x24
        public System.Boolean closed;  // 0x25
        public System.Single totalLen;  // 0x28

    }

    // TypeToken: 0x2000083  // size: 0x10
    public static class SplineSamplingUtils
    {
        // Methods
        // RVA: 0x0A9C52A8  token: 0x6000278
        public static System.Collections.Generic.List<UnityEngine.Vector2> SampleByArcLength(UnityEngine.Splines.SplineContainer container, System.Single step, System.Int32 splineIndex, System.Int32 lutResolution, System.Boolean closed, System.Boolean worldSpace) { }
        // RVA: 0x0A9C5AA0  token: 0x6000279
        private static UnityEngine.Vector3 <SampleByArcLength>g__P|0_0(System.Int32 i, Beyond.Rendering.VFXSludgeNavUtils.SplineSamplingUtils.<>c__DisplayClass0_0& ) { }
        // RVA: 0x0A9C5BB8  token: 0x600027A
        private static System.Single <SampleByArcLength>g__WrapLen|0_1(System.Single s, Beyond.Rendering.VFXSludgeNavUtils.SplineSamplingUtils.<>c__DisplayClass0_0& ) { }

    }

    // TypeToken: 0x2000085  // size: 0x10
    public static class PolySimplify
    {
        // Methods
        // RVA: 0x0A9C2ECC  token: 0x600027B
        public static System.Collections.Generic.List<UnityEngine.Vector3> SimplifyByAngle(System.Collections.Generic.List<UnityEngine.Vector3> pts, System.Single maxTurnDeg, System.Single minEdgeLen, System.Boolean closed) { }

    }

    // TypeToken: 0x200008F  // size: 0x40
    public class OverlayLayer
    {
        // Fields
        public System.Int32 handle;  // 0x10
        public UnityEngine.GameObject overlayGO;  // 0x18
        public UnityEngine.HGMeshRenderer overlayRenderer;  // 0x20
        public UnityEngine.HGMeshRendererData clonedData;  // 0x28
        public UnityEngine.Material currentMaterial;  // 0x30
        public UnityEngine.Material[] materialsScratch;  // 0x38

        // Methods
        // RVA: 0x041E1670  token: 0x60002AA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000091  // size: 0x80
    public sealed class MaterialsProvider : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x083D0A80  token: 0x60002B5
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x07378308  token: 0x60002B6
        public virtual UnityEngine.Material[] Invoke() { }
        // RVA: 0x053DED94  token: 0x60002B7
        public virtual System.IAsyncResult BeginInvoke(System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x05397E84  token: 0x60002B8
        public virtual UnityEngine.Material[] EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000095  // size: 0x24
    public sealed struct __StaticArrayInitTypeSize=20
    {
    }

    // TypeToken: 0x2000094  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=20 7848C87B1819709EC2A0D678BCD530B3C49CA3BB2F61B8FCEF827B64DE251E15;  // static @ 0x0

    }

namespace Beyond.Rendering
{

    // TypeToken: 0x200000B  // size: 0x10
    public class CustomPerDrawDataChannelUtils
    {
        // Fields
        public static System.Int32 HOUDINI_VAT_INDEX;  // const
        public static System.Int32 HOUDINI_VAT_CHANNEL;  // const
        public static System.Int32 LIT_EMISSIVE_INDEX;  // const
        public static System.Int32 LIT_EMISSIVE_ANIMATION_SWEEP_INDEX;  // const
        public static System.Int32 LIT_EMISSIVE_ALBEDO_INDEX;  // const
        public static System.Int32 UV_ANIMATION_INDEX;  // const
        public static System.Int32 MOVING_BAMBOO_CUR_PARAM_INDEX;  // const
        public static System.Int32 MOVING_BAMBOO_LAST_PARAM_INDEX;  // const
        public static System.Int32 LIT_DISSOLVE_INDEX;  // const
        public static System.Int32 LIT_DISSOLVE_CHANNEL;  // const
        public static System.Int32 CHARACTER_PARAMS_INDEX;  // const
        public static System.Int32 CHARACTER_ENVIRONMENT_EFFECT_INTENSITY_CHANNEL;  // const
        public static System.Int32 CHARACTER_WET_EFFECT_WORLD_SPACE_HEIGHT_CHANNEL;  // const
        public static System.Int32 CHARACTER_DISSOLVE_SCHEDULE_OFFSET_CHANNEL;  // const
        public static System.Int32 CHARACTER_RAIN_EFFECT_INTENSITY_CHANNEL_BIT;  // const
        public static System.Int32 CHARACTER_WET_EFFECT_INTENSITY_CHANNEL_BIT;  // const
        public static System.Int32 CHARACTER_WET_EFFECT_GLOBAL_INTENSITY_CHANNEL_BIT;  // const
        public static System.Int32 CHARACTER_SNOW_EFFECT_INTENSITY_CHANNEL_BIT;  // const
        public static System.Int32 CHARACTER_PARAMS_CHANNEL_BIT_BIAS;  // const
        public static System.Int32 CHANNELS_PER_PARAM;  // const
        public static System.Int32 VFX_ALPHA_INDEX;  // const
        public static System.Int32 VFX_ALPHA_DEFAULT_CHANNEL;  // const
        public static System.Int32 TRAIL_EFFECT_DISPLAY_INDEX;  // const
        public static System.Int32 TRAIL_EFFECT_SCAN_INDEX;  // const

        // Methods
        // RVA: 0x0A9AB688  token: 0x600001B
        public static System.Void SetPerDrawData_LitDissolve(UnityEngine.Renderer affectRenderer, System.Single dissolve) { }
        // RVA: 0x0A9AB5EC  token: 0x600001C
        public static System.Void SetPerDrawData_HoudiniVAT(UnityEngine.Renderer affectRenderer, System.Int32 displayFrame) { }
        // RVA: 0x0A9AB720  token: 0x600001D
        public static System.Void SetPerDrawData_MovingBamboo(UnityEngine.Renderer affectRenderer, UnityEngine.Vector4 curParam, UnityEngine.Vector4 lastParam) { }
        // RVA: 0x03F26D20  token: 0x600001E
        public static System.Void SetPerDrawData_EmissiveColor(UnityEngine.Renderer affectRenderer, UnityEngine.Vector4 emissive) { }
        // RVA: 0x03F26D90  token: 0x600001F
        public static System.Void SetPerDrawData_EmissiveAlbedoColor(UnityEngine.Renderer affectRenderer, UnityEngine.Vector4 emissiveAlbedo) { }
        // RVA: 0x03F26F90  token: 0x6000020
        public static System.Void SetPerDrawData_SweepLightIntensity(UnityEngine.Renderer affectRenderer, System.Single intensity) { }
        // RVA: 0x03F26E00  token: 0x6000021
        public static System.Void SetPerDrawData_SweepBreathing(UnityEngine.Renderer affectRenderer, System.Single frequency, System.Single intensity, System.Single timeOffset) { }
        // RVA: 0x0A9AB95C  token: 0x6000022
        public static System.Void SetPerDrawData_UVAnimation(UnityEngine.Renderer affectRenderer, System.Single animationEnable, System.Single customAddTime) { }
        // RVA: 0x0323AF00  token: 0x6000023
        public static System.Void SetPerDrawData_CharacterParams(UnityEngine.Renderer affectRenderer, UnityEngine.Vector4 param0) { }
        // RVA: 0x0A9ABA0C  token: 0x6000024
        public static System.Void SetPerDrawData_VFXAlpha0(UnityEngine.Renderer affectRenderer, System.Single alpha) { }
        // RVA: 0x0A9AB7E0  token: 0x6000025
        public static System.Void SetPerDrawData_TrailDisplayArea(UnityEngine.Renderer affectRenderer, System.Single displayDistanceStart1, System.Single displayDistanceEnd1, System.Single displayDistanceStart2, System.Single displayDistanceEnd2) { }
        // RVA: 0x0A9AB8BC  token: 0x6000026
        public static System.Void SetPerDrawData_TrailScanCircle(UnityEngine.Renderer affectRenderer, System.Single scanRadiusStart, System.Single scanRadiusEnd) { }
        // RVA: 0x041E1670  token: 0x6000027
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000C  // size: 0x10
    public static class PipelineSettingHelper
    {
        // Fields
        public static System.String SETTING_FOLDER;  // const
        public static System.String SETTING_FILE_LIST;  // const

        // Methods
        // RVA: 0x03583870  token: 0x6000028
        public static System.Void LoadPipelineSettings() { }
        // RVA: 0x035850E0  token: 0x6000029
        private static System.String _LoadSetting(System.String settingName) { }
        // RVA: 0x03584E30  token: 0x600002A
        private static System.String _GetFullSettingPath(System.String settingName) { }

    }

    // TypeToken: 0x200000D  // size: 0x10
    public static class ResourceRouter
    {
        // Fields
        private static System.String DYNAMIC_ENVIRONMENTS_FOLDER;  // const

        // Methods
        // RVA: 0x0A9AD718  token: 0x600002B
        private static System.String _GetCategoryFolder(Beyond.Resource.RootCategory category) { }
        // RVA: 0x0A9AD69C  token: 0x600002C
        public static System.String GetEnvironmentsFullPath(Beyond.Resource.RootCategory category, System.Boolean isDevOnly) { }
        // RVA: 0x0A9AD658  token: 0x600002D
        public static System.String GetDecalResourcesPath() { }
        // RVA: 0x0A9AD5DC  token: 0x600002E
        public static System.String GetDecalResourcesFullPath(Beyond.Resource.RootCategory category, System.Boolean isDevOnly) { }

    }

    // TypeToken: 0x200000E  // size: 0x10
    public class ShaderWarmupManager
    {
        // Fields
        public static readonly System.String RUNTIME_PSO_COLLECTION_FOLDER;  // static @ 0x0
        private static System.String WARM_UP_KEY_WORD;  // const
        private static System.String WARM_UP_API_KEY_WORD;  // const
        private static System.String WARM_UP_DRIVER_KEY_WORD;  // const
        private static System.String WARM_UP_REASON_KEY_WORD;  // const
        private static System.String WARMUP_RETRY_COUNT;  // const
        private static System.String WARMUP_UNFINISHED_COUNT;  // const
        private static System.String DOWNLOADING_WARMUP_UNFINISHED_COUNT;  // const
        private static System.String FEATURE_RAYTRACING;  // const
        private static System.String FEATURE_ONE_PASS;  // const
        private static System.Int32 SHADER_COMPILE_WEIGHT;  // const
        public static readonly System.Int32 UNFINISHED_RETRY_COUNT_LIMIT;  // static @ 0x8
        public static readonly System.Int32 DOWNLOADING_UNFINISHED_RETRY_COUNT_LIMIT;  // static @ 0xc
        public static readonly System.Int32 CACHE_FAILED_RETRY_COUNT_LIMIT;  // static @ 0x10
        public static System.Boolean s_shouldUseOverride;  // static @ 0x14
        public static System.Boolean s_needWarmupOverride;  // static @ 0x15
        public static System.Int32 s_forceLoginWarmupStartIndex;  // static @ 0x18
        public static System.Int32 s_forceLoginWarmupCount;  // static @ 0x1c
        public static System.Int32 s_forceSceneWarmupStartIndex;  // static @ 0x20
        public static System.Int32 s_forceSceneWarmupCount;  // static @ 0x24
        private static System.String s_disabledFeature;  // static @ 0x28
        private static System.Boolean s_warmupByCacheLoadChecked;  // static @ 0x30
        private static System.Boolean s_isDeviceInBlackList;  // static @ 0x31
        private static System.Boolean s_isDx11DriverCheck;  // static @ 0x32
        public static System.Int32 s_warmupTotalCount;  // static @ 0x34
        public static System.Int32 s_warmupTotalCountWeighted;  // static @ 0x38
        public static System.Int32 s_warmupRemainCountWeighted;  // static @ 0x3c
        private static System.Int32 s_warmupFinishedCount;  // static @ 0x40
        private static System.Int32 s_cacheFileSizeBefore;  // static @ 0x44
        private static System.Int32 s_compileTotalCount;  // static @ 0x48
        public static System.Single s_loginWarmUpTime;  // static @ 0x4c
        public static Beyond.Rendering.ShaderWarmupManager.WarmUpReason s_loginWarmUpReason;  // static @ 0x50
        public static System.Single s_downloadWarmupPercent;  // static @ 0x54
        private static Beyond.Rendering.ShaderWarmupManager.WarmupRunType s_lastWarmupRunType;  // static @ 0x58
        private static System.Boolean s_isDownloadingWarmupRunning;  // static @ 0x5c
        private static System.Boolean s_shouldStopDownloadingWarmup;  // static @ 0x5d
        public static System.Int64 s_cacheSizeRecompileThreshold;  // static @ 0x60
        public static System.Int32 s_warmupCountPerWorkerMainJobCompile;  // static @ 0x68
        public static System.Int32 s_warmupCountPerStepBackJobCompile;  // static @ 0x6c
        public static System.Int32 s_warmupCountPerStepMainJob;  // static @ 0x70
        public static System.Int32 s_warmupCountPerStepBackJob;  // static @ 0x74
        public static System.Int32 s_warmupCountPerStepBackDispatchLimit;  // static @ 0x78
        public static System.Int32 s_warmupCountToClean;  // static @ 0x7c
        public static System.Int32 s_warmupTimeLimitMobile;  // static @ 0x80
        public static System.Int32 s_warmupTimeLimitPC;  // static @ 0x84
        public static System.String s_vulkanCacheFileName;  // static @ 0x88
        public static System.String s_iosCacheFileName;  // static @ 0x90

        // Properties
        System.Boolean isWarming { get; /* RVA: 0x03EBBA10 */ }

        // Methods
        // RVA: 0x0A9ADE68  token: 0x6000030
        public static System.String GetPsoPathName(UnityEngine.RuntimePlatform platform, UnityEngine.Rendering.GraphicsDeviceType graphicsDeviceType, System.Int32 shaderLod, System.String levelId) { }
        // RVA: 0x0A9ADF3C  token: 0x6000031
        public static System.String GetPsoPathName(UnityEngine.RuntimePlatform platform, UnityEngine.Rendering.GraphicsDeviceType graphicsDeviceType, System.Int32 shaderLod, System.String levelId, System.String feature) { }
        // RVA: 0x04D537E0  token: 0x6000032
        public static System.String GetLoginWarmupDetail() { }
        // RVA: 0x0A9AE6EC  token: 0x6000033
        private static System.Int64 _GetCacheFileSize() { }
        // RVA: 0x03C8F850  token: 0x6000034
        public static System.Void ProcessRemoteConfig(System.String remoteConfigStr) { }
        // RVA: 0x0A9AE024  token: 0x6000035
        public static System.Void MarkNeedShaderWarmUp(Beyond.Rendering.ShaderWarmupManager.WarmUpReason reason) { }
        // RVA: 0x0A9AE08C  token: 0x6000036
        public static System.Void MarkWarmUpLoginFinish() { }
        // RVA: 0x0A9ADE0C  token: 0x6000037
        public static System.Void CleanLoadedPso() { }
        // RVA: 0x0A9AEAFC  token: 0x6000038
        private static System.Void _MarkWarmUpLoginFinish() { }
        // RVA: 0x0A9AE0E8  token: 0x6000039
        public static System.Void TryRepairPsoCacheAfterCorruptCheck() { }
        // RVA: 0x0A9AEEF4  token: 0x600003A
        private static System.Boolean _TryDeleteVulkanPsoCacheFile() { }
        // RVA: 0x03C90DA0  token: 0x600003B
        public static System.Boolean CheckNeedWarmUpLogin(System.Boolean stateless) { }
        // RVA: 0x0A9AE244  token: 0x600003C
        public static System.Collections.IEnumerator WarmupShadersLogin(System.Boolean supportsRaytracing) { }
        // RVA: 0x0A9AE1E8  token: 0x600003D
        public static System.Collections.IEnumerator WarmupShadersDownloading(System.Boolean supportsRaytracing) { }
        // RVA: 0x046DD340  token: 0x600003E
        public static System.Void StopWarmupShadersDownloading() { }
        // RVA: 0x0A9AEEA4  token: 0x600003F
        private static System.Collections.IEnumerator _StopDownloadingWarmupAndWait() { }
        // RVA: 0x0A9AE314  token: 0x6000040
        private static System.Collections.Generic.List<Beyond.Resource.StringPathHash> _BuildLoginWarmupPaths(System.Boolean supportsRaytracing) { }
        // RVA: 0x02E574D0  token: 0x6000041
        private static System.Void _InitWarmupStats(System.Collections.Generic.List<UnityEngine.HGPsoWarmupCollection> loadedAssets, System.Boolean isBackgroundWarmup, System.Boolean updateCacheFileSizeBefore) { }
        // RVA: 0x02E578F0  token: 0x6000042
        private static System.Void _UpdateDownloadWarmupPercent() { }
        // RVA: 0x0A9AE514  token: 0x6000043
        private static System.Void _DisposeWarmupAssetHandles(System.Collections.Generic.List<Beyond.Resource.FAssetProxyHandle> assetProxyHandles) { }
        // RVA: 0x0A9AEA54  token: 0x6000044
        private static System.Collections.IEnumerator _LoadPsoAssets(System.Collections.Generic.List<Beyond.Resource.StringPathHash> resPathList, System.Collections.Generic.List<Beyond.Resource.FAssetProxyHandle> assetProxyHandles, System.Collections.Generic.List<UnityEngine.HGPsoWarmupCollection> loadedAssets) { }
        // RVA: 0x0A9AE2A0  token: 0x6000045
        public static System.Collections.IEnumerator WarmupShadersScene(System.String scene) { }
        // RVA: 0x0A9AE150  token: 0x6000046
        public static System.Collections.IEnumerator WarmupOnePso(UnityEngine.HGPsoWarmupCollection psoC, System.Int32 startIndex, System.Int32 warmupCount) { }
        // RVA: 0x0A9AF120  token: 0x6000047
        private static System.Collections.IEnumerator _WarmupOnePsoTimeLimited(UnityEngine.HGPsoWarmupCollection psoC, System.Int32 startIndex, System.Int32 desiredWarmupCount, Beyond.Rendering.ShaderWarmupManager.TimeCostCounter timeStart) { }
        // RVA: 0x0A9AF088  token: 0x6000048
        private static System.Collections.IEnumerator _WarmupOnePsoBackgroundOnly(UnityEngine.HGPsoWarmupCollection psoC, System.Int32 startIndex, System.Int32 desiredWarmupCount) { }
        // RVA: 0x0A9AEDE8  token: 0x6000049
        private static System.Void _StepProgress(System.Int32 newFinishedCount, System.Int32& compileTotalCount) { }
        // RVA: 0x03811960  token: 0x600004A
        private static System.Boolean _CheckNeedWarmUpByFirstEntry() { }
        // RVA: 0x03C91110  token: 0x600004B
        private static System.Boolean _CheckNeedWarmUpByDeviceState(System.Boolean stateless) { }
        // RVA: 0x03C905A0  token: 0x600004C
        private static System.Boolean _IsNeedWarmupByGraphicsChange() { }
        // RVA: 0x03C91030  token: 0x600004D
        private static System.Boolean _IsNeedWarmupByCacheOutdated() { }
        // RVA: 0x0A9AED30  token: 0x600004E
        private static System.Void _SetWarmupRetryCount(System.Int32 count) { }
        // RVA: 0x0A9AED8C  token: 0x600004F
        private static System.Void _SetWarmupUnfinishedCount(System.Int32 count) { }
        // RVA: 0x0A9AE89C  token: 0x6000050
        private static System.Int32 _GetDownloadingWarmupUnfinishedCount() { }
        // RVA: 0x0A9AECD4  token: 0x6000051
        private static System.Void _SetDownloadingWarmupUnfinishedCount(System.Int32 count) { }
        // RVA: 0x0A9AEC48  token: 0x6000052
        private static System.Void _MarkWarmupFinishAPI() { }
        // RVA: 0x03C92470  token: 0x6000053
        private static System.Boolean _IsNeedWarmupDx11Driver() { }
        // RVA: 0x0A9AEB78  token: 0x6000054
        private static System.Void _MarkWarmupDx11Driver() { }
        // RVA: 0x03C90F40  token: 0x6000055
        private static System.Void _ResetWarmUpStatsIfNeed() { }
        // RVA: 0x03C90EF0  token: 0x6000056
        private static System.Boolean _IsDeviceSupportWarmUp() { }
        // RVA: 0x02E57820  token: 0x6000057
        private static System.Boolean _IsDeviceSupportDownloadWarmUp() { }
        // RVA: 0x0A9AE9E8  token: 0x6000058
        private static System.Boolean _IsFeatureEnabled(System.String featureKeyword) { }
        // RVA: 0x03C90FA0  token: 0x6000059
        private static System.Boolean _CheckWarmUpUnfinishedOften() { }
        // RVA: 0x0A9AE8EC  token: 0x600005A
        private static System.Int32 _GetLoginTimeLimit() { }
        // RVA: 0x0A9AE948  token: 0x600005B
        private static System.Int32 _GetWarmupEndIndexExclusive(UnityEngine.HGPsoWarmupCollection psoC, System.Int32 startIndex, System.Int32 desiredWarmupCount) { }
        // RVA: 0x0A9AE7F4  token: 0x600005C
        private static System.Int32 _GetCompileCountForRange(UnityEngine.HGPsoWarmupCollection psoC, System.Int32 startIndex, System.Int32 desiredWarmupCount) { }
        // RVA: 0x0A9AE660  token: 0x600005D
        private static System.Int32 _GetBackgroundWarmupStartIndex(UnityEngine.HGPsoWarmupCollection psoC, System.Int32 startIndex, System.Int32 endIndexExclusive) { }
        // RVA: 0x041E1670  token: 0x600005E
        public System.Void .ctor() { }
        // RVA: 0x049916A0  token: 0x600005F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001A  // size: 0x18
    public class ShaderWarmupManagerRemoteCfg
    {
        // Fields
        private Beyond.Rendering.ShaderWarmupManagerRemoteCfg.ShaderWarmupRemoteBlacklist m_blacklist;  // 0x10

        // Properties
        System.String cpuNameForWarmUp { get; /* RVA: 0x0A9ADDCC */ }

        // Methods
        // RVA: 0x03C8F940  token: 0x6000098
        public System.Void .ctor(System.String blackListStr) { }
        // RVA: 0x03C8F910  token: 0x6000099
        public System.Boolean IsEnableDx11DriverVersionDetect() { }
        // RVA: 0x03C8F3C0  token: 0x600009A
        public System.Boolean IsDeviceInBlackList(System.String& disabledFeature) { }
        // RVA: 0x0A9ADB54  token: 0x600009C
        private static System.Boolean _CheckDeviceMatch(Beyond.Rendering.ShaderWarmupManagerRemoteCfg.ShaderWarmupRemoteBlackDevice blacklistDevice, System.String currentDeviceModel, System.String currentGraphicsDeviceName, System.String currentGraphicsDeviceVersion, System.String currentProcessorType, System.String currentOS) { }

    }

    // TypeToken: 0x200001E  // size: 0x10
    public static class WindowsGpuDriverVersion
    {
        // Fields
        private static readonly System.Guid GUID_DEVCLASS_DISPLAY;  // static @ 0x0
        private static System.UInt32 DIGCF_PRESENT;  // const
        private static System.UInt32 SPDRP_DEVICEDESC;  // const
        private static System.UInt32 SPDRP_MFG;  // const
        private static System.UInt32 SPDRP_FRIENDLYNAME;  // const
        private static System.UInt32 SPDRP_DRIVER;  // const
        private static System.UInt32 DICS_FLAG_GLOBAL;  // const
        private static System.UInt32 DIREG_DRV;  // const
        private static readonly System.IntPtr INVALID_HANDLE_VALUE;  // static @ 0x10
        private static System.Int32 ERROR_SUCCESS;  // const
        private static System.Int32 ERROR_NO_MORE_ITEMS;  // const
        private static System.Int32 ERROR_INSUFFICIENT_BUFFER;  // const
        private static System.Int32 ERROR_MORE_DATA;  // const
        private static System.UInt32 REG_SZ;  // const
        private static System.UInt32 REG_EXPAND_SZ;  // const

        // Methods
        // RVA: 0x0A9B0168  token: 0x60000A0
        public static System.String GetCurrentDriverVersion() { }
        // RVA: 0x0A9AFBAC  token: 0x60000A1
        public static Beyond.Rendering.WindowsGpuDriverVersion.Result GetBestMatch() { }
        // RVA: 0x0A9AF37C  token: 0x60000A2
        public static System.Collections.Generic.List<Beyond.Rendering.WindowsGpuDriverVersion.Result> GetAllDisplayDriverVersions() { }
        // RVA: 0x0A9B0470  token: 0x60000A3
        public static System.String SerializeAllResults(System.Collections.Generic.List<Beyond.Rendering.WindowsGpuDriverVersion.Result> results) { }
        // RVA: 0x0A9B0414  token: 0x60000A4
        public static System.String SerializeAllCurrentResults() { }
        // RVA: 0x0A9B01DC  token: 0x60000A5
        public static System.Boolean IsBestMatchDriverVersionChanged(System.String serialized) { }
        // RVA: 0x0A9B116C  token: 0x60000A6
        private static System.Boolean _TryGetDriverVersionByKeyId(System.String serialized, System.String keyId, System.String& driverVersion) { }
        // RVA: 0x0A9B0ED8  token: 0x60000A7
        private static System.Int32 _ScoreAgainstUnityCurrentGpu(System.String friendlyName, System.String deviceDesc, System.String mfg) { }
        // RVA: 0x0A9B0954  token: 0x60000A8
        private static System.Boolean _ContainsIgnoreCase(System.String a, System.String b) { }
        // RVA: 0x0A9B09D8  token: 0x60000A9
        private static System.String _GetDeviceRegistryPropertyString(System.IntPtr infoSet, Beyond.Rendering.WindowsGpuDriverVersion.SP_DEVINFO_DATA& devInfo, System.UInt32 prop) { }
        // RVA: 0x0A9B0C20  token: 0x60000AA
        private static System.String _GetDriverKeySuffix(System.String driverKey) { }
        // RVA: 0x0A9B0CB4  token: 0x60000AB
        private static System.String _ReadRegistrySz(Beyond.Rendering.WindowsGpuDriverVersion.SafeRegistryHandle hKey, System.String valueName) { }
        // RVA: 0x0A9B02C4  token: 0x60000AC
        private static System.Int32 RegCloseKey(System.IntPtr hKey) { }
        // RVA: 0x0A9B06F4  token: 0x60000AD
        private static System.IntPtr SetupDiGetClassDevs(System.Guid& classGuid, System.IntPtr enumerator, System.IntPtr hwndParent, System.UInt32 flags) { }
        // RVA: 0x0A9B0658  token: 0x60000AE
        private static System.Boolean SetupDiEnumDeviceInfo(System.IntPtr deviceInfoSet, System.UInt32 memberIndex, Beyond.Rendering.WindowsGpuDriverVersion.SP_DEVINFO_DATA& deviceInfoData) { }
        // RVA: 0x0A9B07A0  token: 0x60000AF
        private static System.Boolean SetupDiGetDeviceRegistryProperty(System.IntPtr deviceInfoSet, Beyond.Rendering.WindowsGpuDriverVersion.SP_DEVINFO_DATA& deviceInfoData, System.UInt32 property, System.UInt32& propertyRegDataType, System.IntPtr propertyBuffer, System.UInt32 propertyBufferSize, System.UInt32& requiredSize) { }
        // RVA: 0x0A9B0874  token: 0x60000B0
        private static Beyond.Rendering.WindowsGpuDriverVersion.SafeRegistryHandle SetupDiOpenDevRegKey(System.IntPtr deviceInfoSet, Beyond.Rendering.WindowsGpuDriverVersion.SP_DEVINFO_DATA& deviceInfoData, System.UInt32 scope, System.UInt32 hwProfile, System.UInt32 keyType, System.Int32 samDesired) { }
        // RVA: 0x0A9B0620  token: 0x60000B1
        private static System.Boolean SetupDiDestroyDeviceInfoList(System.IntPtr deviceInfoSet) { }
        // RVA: 0x0A9B0308  token: 0x60000B2
        private static System.Int32 RegQueryValueEx(Beyond.Rendering.WindowsGpuDriverVersion.SafeRegistryHandle hKey, System.String lpValueName, System.IntPtr lpReserved, System.UInt32& lpType, System.IntPtr lpData, System.UInt32& lpcbData) { }
        // RVA: 0x0A9B12D4  token: 0x60000B3
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000024  // size: 0x10
    public static class SOCHelper
    {
        // Fields
        public static readonly System.Int32 CHUNK_SIZE;  // static @ 0x0
        public static System.String SOC_FOLDER_NAME;  // const
        public static System.String SOC_INTERMEDIATE_FILE_NAME;  // const

        // Methods
        // RVA: 0x0A9AD8FC  token: 0x60000BD
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000025  // size: 0x18
    public sealed struct SceneStateAreaKey : System.IEquatable`1
    {
        // Fields
        public System.UInt32 sceneStateMask;  // 0x10
        public System.Int32 areaId;  // 0x14
        public static readonly Beyond.Rendering.SceneStateAreaKey DEFAULT;  // static @ 0x0

        // Methods
        // RVA: 0x04D85C40  token: 0x60000BE
        public System.Void .ctor(System.UInt32 sceneStateMask, System.Int32 areaId) { }
        // RVA: 0x0A9AD984  token: 0x60000BF
        public virtual System.Boolean Equals(Beyond.Rendering.SceneStateAreaKey other) { }
        // RVA: 0x0A9AD9F0  token: 0x60000C0
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0A9ADA80  token: 0x60000C1
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x04DA6BD0  token: 0x60000C2
        private static System.Void .cctor() { }
        // RVA: 0x0A9ADAF0  token: 0x60000C3
        public System.Boolean <>iFixBaseProxy_Equals(System.Object P0) { }
        // RVA: 0x0A9ADB28  token: 0x60000C4
        public System.Int32 <>iFixBaseProxy_GetHashCode() { }

    }

    // TypeToken: 0x2000026  // size: 0x28
    public class HGSOCSubSceneData : UnityEngine.ScriptableObject
    {
        // Fields
        public Beyond.Rendering.SceneStateAreaKey[] keyArray;  // 0x18
        public Beyond.Rendering.HGSOCSubSceneData.SubSceneDataPerSceneStateArea[] dataArray;  // 0x20

        // Methods
        // RVA: 0x03573370  token: 0x60000C5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000029  // size: 0x38
    public class HGSOCDataHelper : UnityEngine.MonoBehaviour
    {
        // Fields
        public System.Single fallOffStrength;  // 0x18
        public System.Single innerBlendWeight;  // 0x1c
        private System.Boolean <displayQuadOccluders>k__BackingField;  // 0x20
        private System.Boolean <displayHGOccluders>k__BackingField;  // 0x21
        public UnityEngine.Material material;  // 0x28
        public UnityEngine.HGOccluder[] occluders;  // 0x30

        // Properties
        System.Boolean displayQuadOccluders { get; /* RVA: 0x04D866B0 */ set; /* RVA: 0x04D866D0 */ }
        System.Boolean displayHGOccluders { get; /* RVA: 0x04D86850 */ set; /* RVA: 0x04D86860 */ }

        // Methods
        // RVA: 0x0A9AD424  token: 0x60000CA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002A  // size: 0x30
    public class EntityRenderAlphaDitherController
    {
        // Fields
        private Beyond.Rendering.IRendererHelper m_owner;  // 0x10
        private System.Single m_ditherAlpha;  // 0x18
        private static System.Int32 s_CharDitherFade;  // static @ 0x0
        private static System.Int32 s_DitherTransparentAlpha;  // static @ 0x4
        private System.Collections.Generic.Dictionary<System.Int32,System.Single> m_alphaPerHandles;  // 0x20
        private System.Boolean m_lastSetAllRendererEnabled;  // 0x28

        // Methods
        // RVA: 0x03699720  token: 0x60000CB
        public System.Void .ctor(Beyond.Rendering.IRendererHelper owner) { }
        // RVA: 0x0369BA90  token: 0x60000CC
        public System.Void ResetDitherAlpha() { }
        // RVA: 0x0302E760  token: 0x60000CD
        public System.Boolean SetDitherAlpha(System.Single alpha, System.Int32 handle) { }
        // RVA: 0x0302EB90  token: 0x60000CE
        public System.Single GetDitherAlpha() { }
        // RVA: 0x03699CD0  token: 0x60000CF
        public System.Void Reset() { }
        // RVA: 0x0302E370  token: 0x60000D0
        private System.Boolean _ApplyNewAlpha() { }
        // RVA: 0x0302E480  token: 0x60000D1
        private System.Single _GetCurMinAlpha() { }
        // RVA: 0x03B970B0  token: 0x60000D2
        public System.Collections.Generic.Dictionary<System.Int32,System.Single> GetAlphaDictionary() { }
        // RVA: 0x03F2FF40  token: 0x60000D3
        private System.Void _SetMaterialDitherAlpha(System.Single value) { }
        // RVA: 0x03F2F9D0  token: 0x60000D4
        private System.Boolean _SetMaterialDitherEnabled(System.Boolean enabled) { }
        // RVA: 0x04D5E130  token: 0x60000D5
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200002B  // size: 0x28
    public class EntityRenderCapturedEntity
    {
        // Fields
        public UnityEngine.GameObject rootGameObject;  // 0x10
        public System.Collections.Generic.List<UnityEngine.MeshRenderer> meshRenderers;  // 0x18
        public System.Collections.Generic.List<UnityEngine.MeshRenderer> capturedRenderers;  // 0x20

        // Methods
        // RVA: 0x0A9ACCC8  token: 0x60000D6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002C  // size: 0x48
    public class EntityRenderCaptureMeshController
    {
        // Fields
        private Beyond.Rendering.IRendererHelper m_owner;  // 0x10
        private UnityEngine.GameObject m_gameObject;  // 0x18
        private UnityEngine.LODGroup m_lodGroup;  // 0x20
        private System.Collections.Generic.List<Beyond.Rendering.EntityRenderCapturedEntity> m_capturedEntities;  // 0x28
        private System.Int32 m_meshRendererToSyncFrame;  // 0x30
        private System.Collections.Generic.List<Beyond.Rendering.EntityRenderCaptureMeshController.MeshRendererToSync> m_meshRendererToSync;  // 0x38
        private System.Collections.Generic.Dictionary<UnityEngine.Renderer,Beyond.Rendering.EntityVFXRendererMask> m_rendererTypeMasks;  // 0x40

        // Methods
        // RVA: 0x03699570  token: 0x60000D7
        public System.Void .ctor(Beyond.Rendering.IRendererHelper owner, UnityEngine.GameObject gameObject) { }
        // RVA: 0x03699D50  token: 0x60000D8
        public System.Void Reset() { }
        // RVA: 0x040FB920  token: 0x60000D9
        public System.Void LateTick() { }
        // RVA: 0x0A9AC9B4  token: 0x60000DA
        public System.Void InitRendererTypeMasks(System.Collections.Generic.Dictionary<UnityEngine.Renderer,Beyond.Rendering.EntityVFXRendererMask> masks) { }
        // RVA: 0x0A9ABAB0  token: 0x60000DB
        public Beyond.Rendering.EntityRenderCapturedEntity Capture(UnityEngine.GameObject root, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: 0x0A9A76BC  token: 0x60000DC
        private System.Boolean _PassRendererMaskFilter(UnityEngine.Renderer renderer, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: 0x040FB960  token: 0x60000DD
        private System.Void _SyncTransformInRenderPipeline() { }
        // RVA: 0x0A9ACA18  token: 0x60000DE
        public System.Void Release(Beyond.Rendering.EntityRenderCapturedEntity mesh) { }

    }

    // TypeToken: 0x200002F  // size: 0x30
    public class EntityRendererTypeConfig
    {
        // Fields
        public System.Boolean useRegex;  // 0x10
        public System.String regexString;  // 0x18
        public System.Text.RegularExpressions.Regex regex;  // 0x20
        public Beyond.Rendering.EntityVFXRendererType type;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x60000E0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000030  // size: 0x40
    public class EntityRenderHelperMaterialController
    {
        // Fields
        private static System.Collections.Generic.List<UnityEngine.Material> s_cachedMaterialList;  // static @ 0x0
        private System.Collections.Generic.List<Beyond.Rendering.EntityRenderHelperMaterialController.RendererInfo> m_rendererInfos;  // 0x10
        private Beyond.Rendering.EntityRenderHelperMaterialController.AddedMaterialController m_addedMaterialController;  // 0x18
        private Beyond.Rendering.EntityRenderHelperMaterialController.MeshMaterialController m_meshMaterialController;  // 0x20
        private System.Collections.Generic.Dictionary<Beyond.Rendering.EntityVFXRendererMask,System.Collections.Generic.List<Beyond.Rendering.EntityRenderHelperMaterialController.RendererInfo>> m_renderInfoIndexPerMask;  // 0x28
        private Beyond.Rendering.EntityRenderHelperMaterialController.CustomPerDrawDataController m_customPerDrawDataController;  // 0x30
        private System.Boolean m_calculateBoundsWithTransform;  // 0x38
        private static Beyond.PoolCore.ObjectPool<Beyond.Rendering.EntityRenderHelperMaterialController.RendererInfo> s_rendererInfoPool;  // static @ 0x8
        private static Beyond.PoolCore.ObjectPool<System.Collections.Generic.List<Beyond.Rendering.EntityRenderHelperMaterialController.RendererInfo>> s_rendererInfoListPool;  // static @ 0x10
        private static System.Int32 s_ZTestGBuffer;  // static @ 0x18
        private static System.Int32 s_OutlineZTest;  // static @ 0x1c
        private static System.String s_AlphaBlendOnKeyword;  // static @ 0x20

        // Methods
        // RVA: 0x0302D300  token: 0x60000E1
        public System.Void Init(System.Collections.Generic.List<UnityEngine.Renderer> allRenderers, System.Collections.Generic.List<Beyond.Rendering.EntityRendererTypeConfig> configs, Beyond.Rendering.EntityCustomizeRendererPropertyConfig customRendererPropertyConfig, System.Boolean calculateBoundsWithTransform) { }
        // RVA: 0x031DB150  token: 0x60000E2
        public System.Void OnRelease() { }
        // RVA: 0x03699DB0  token: 0x60000E3
        public System.Void Reset() { }
        // RVA: 0x0369ABF0  token: 0x60000E4
        public System.Boolean IsAllRendererValid() { }
        // RVA: 0x032C5D00  token: 0x60000E5
        public System.Void StopShadowCasting(System.Boolean stopRayTracing) { }
        // RVA: 0x032C5FF0  token: 0x60000E6
        public virtual System.Void RevertShadowCastingMode() { }
        // RVA: 0x03578690  token: 0x60000E7
        public System.Int32 AddMaterialToAllRenderer(UnityEngine.Material material, Beyond.Rendering.EntityVFXRendererMask rendererMask, Beyond.Rendering.AddMaterialConfig config) { }
        // RVA: 0x0A9AD0AC  token: 0x60000E8
        public System.Void SetAddictiveFloatProperty(System.Int32 propertyId, System.Single value) { }
        // RVA: 0x0A9ACEEC  token: 0x60000E9
        public System.Single GetAddictiveFloatProperty(System.Int32 propertyId, System.Single defaultValue) { }
        // RVA: 0x0A9AD1AC  token: 0x60000EA
        public System.Void SetAddictiveVectorProperty(System.Int32 propertyId, UnityEngine.Vector4 value) { }
        // RVA: 0x0A9ACFEC  token: 0x60000EB
        public UnityEngine.Vector4 GetAddictiveVectorProperty(System.Int32 propertyId, UnityEngine.Vector4 defaultValue) { }
        // RVA: 0x0A9AD12C  token: 0x60000EC
        public System.Void SetAddictiveTextureProperty(System.Int32 propertyId, UnityEngine.Texture value) { }
        // RVA: 0x0A9ACF6C  token: 0x60000ED
        public UnityEngine.Texture GetAddictiveTextureProperty(System.Int32 propertyId, UnityEngine.Texture defaultValue) { }
        // RVA: 0x035773C0  token: 0x60000EE
        public System.Void RemoveMaterialFromAllRenderer(System.Int32 handle) { }
        // RVA: 0x0A9ACE68  token: 0x60000EF
        public System.Boolean GetAddedMaterialsOfCustomizeProperties(System.Int32 handle, System.Collections.Generic.List<UnityEngine.Material> result) { }
        // RVA: 0x04502C00  token: 0x60000F0
        public System.Void SetKeywordEnableToAllMaterial(System.String keyword, System.Boolean enable, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: 0x0A9AD240  token: 0x60000F1
        public System.Void SetDepthOnlyPassEnableToAllMaterial(System.Boolean enable, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: 0x0357A6A0  token: 0x60000F2
        public System.Void SetFloatToCustomPerDraw(Beyond.Rendering.EntityRenderHelperCustomPerDrawType type, System.Single value, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: 0x0A9AD2C0  token: 0x60000F3
        public System.Void SetVectorToCustomPerDraw(Beyond.Rendering.EntityRenderHelperVectorCustomPerDrawType type, UnityEngine.Vector4 value, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: 0x0357A800  token: 0x60000F4
        public System.Void SetFloatToAllMaterial(System.Int32 nameId, System.Single value, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: 0x0357B4F0  token: 0x60000F5
        public System.Void SetVectorToAllMaterial(System.Int32 nameId, UnityEngine.Vector4 value, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: 0x03941150  token: 0x60000F6
        public System.Void SetTextureToAllMaterial(System.Int32 nameId, UnityEngine.Texture value, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: 0x0A9ACD48  token: 0x60000F7
        public System.Void FillRendererTypeMasks(System.Collections.Generic.Dictionary<UnityEngine.Renderer,Beyond.Rendering.EntityVFXRendererMask> target) { }
        // RVA: 0x03578850  token: 0x60000F8
        private System.Collections.Generic.List<Beyond.Rendering.EntityRenderHelperMaterialController.RendererInfo> _GetRenderersOfRendererMask(Beyond.Rendering.EntityVFXRendererMask rendererMask, System.Boolean affectAll) { }
        // RVA: 0x03103EB0  token: 0x60000F9
        public System.Void SetManualDitherAlphaValue(System.Single value) { }
        // RVA: 0x03104230  token: 0x60000FA
        public System.Boolean SetManualDitherEnable(System.Boolean enable) { }
        // RVA: 0x032C56C0  token: 0x60000FB
        public UnityEngine.Bounds GetRendererBounds() { }
        // RVA: 0x0A9AD3A4  token: 0x60000FC
        public System.Void SetZTestInMaterial(System.String shaderName, System.Boolean enable) { }
        // RVA: 0x0411B870  token: 0x60000FD
        public Beyond.Rendering.RenderHelperType GetRenderHelperType() { }
        // RVA: 0x04D919E0  token: 0x60000FE
        public System.Void .ctor() { }
        // RVA: 0x04A3A560  token: 0x60000FF
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000038  // size: 0x14
    public sealed struct EntityRenderHelperCustomPerDrawType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.EntityRenderHelperCustomPerDrawType RainEffectIntensity;  // const
        public static Beyond.Rendering.EntityRenderHelperCustomPerDrawType WetEffectIntensity;  // const
        public static Beyond.Rendering.EntityRenderHelperCustomPerDrawType WetEffectGlobalIntensity;  // const
        public static Beyond.Rendering.EntityRenderHelperCustomPerDrawType SnowEffectIntensity;  // const
        public static Beyond.Rendering.EntityRenderHelperCustomPerDrawType WetEffectWorldSpaceHeight;  // const
        public static Beyond.Rendering.EntityRenderHelperCustomPerDrawType CharacterDissolveScheduleOffset;  // const

    }

    // TypeToken: 0x2000039  // size: 0x14
    public sealed struct EntityRenderHelperVectorCustomPerDrawType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.EntityRenderHelperVectorCustomPerDrawType Emissive;  // const

    }

    // TypeToken: 0x200003A  // size: 0x20
    public class EntityRenderHelperVisibleController
    {
        // Fields
        private System.Collections.Generic.List<Beyond.Rendering.EntityRenderHelperVisibleController.RendererInfo> m_rendererInfos;  // 0x10
        private System.Collections.Generic.Dictionary<UnityEngine.Renderer,Beyond.Rendering.EntityRenderHelperVisibleController.RendererInfo> m_dictionary;  // 0x18
        private static Beyond.PoolCore.ObjectPool<System.Collections.Generic.List<Beyond.Rendering.EntityRenderHelperVisibleController.RendererInfo>> s_rendererInfoListPool;  // static @ 0x0
        private static Beyond.PoolCore.ObjectPool<System.Collections.Generic.Dictionary<UnityEngine.Renderer,Beyond.Rendering.EntityRenderHelperVisibleController.RendererInfo>> s_rendererInfoDictPool;  // static @ 0x8

        // Methods
        // RVA: 0x0302B800  token: 0x6000142
        public System.Void Init(System.Collections.Generic.List<UnityEngine.Renderer> allRenderers) { }
        // RVA: 0x0A9B2EE4  token: 0x6000143
        private System.Void _InitDictionary() { }
        // RVA: 0x02E5E3A0  token: 0x6000144
        private System.Boolean _TryGetRendererInfo(UnityEngine.Renderer renderer, System.Boolean initDictionary, Beyond.Rendering.EntityRenderHelperVisibleController.RendererInfo& rendererInfo) { }
        // RVA: 0x031DB0B0  token: 0x6000145
        public System.Void Release() { }
        // RVA: 0x0385AEA0  token: 0x6000146
        public System.Void SetVisibleByNameContainsStr(System.String name, System.Boolean visible) { }
        // RVA: 0x0A9B2E44  token: 0x6000147
        public System.Void SetVisibleByRenderer(UnityEngine.Renderer renderer, System.Boolean visible) { }
        // RVA: 0x0385AC60  token: 0x6000148
        public System.Void ResetVisibleByNameContainsStr(System.String name) { }
        // RVA: 0x0A9B2DB8  token: 0x6000149
        public System.Void ResetVisibleByRenderer(UnityEngine.Renderer renderer) { }
        // RVA: 0x03699F00  token: 0x600014A
        public System.Void Reset() { }
        // RVA: 0x041E1670  token: 0x600014B
        public System.Void .ctor() { }
        // RVA: 0x04CB0FC0  token: 0x600014C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200003C  // size: 0x50
    public class EntityRenderMaterialManager
    {
        // Fields
        private System.Collections.Generic.Dictionary<System.Int32,System.Single> m_floatProperties;  // 0x10
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Texture> m_textureProperties;  // 0x18
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Vector4> m_vectorProperties;  // 0x20
        private System.Collections.Generic.HashSet<System.String> m_EnabledKeyWords;  // 0x28
        private System.Collections.Generic.HashSet<System.String> m_DisabledKeyWords;  // 0x30
        private System.Boolean m_shaderPassEnabled;  // 0x38
        private System.Collections.Generic.Dictionary<UnityEngine.Material,System.Boolean> m_oriMaterialShaderPassEnabled;  // 0x40
        private static System.Int32 s_ZTestGBuffer;  // static @ 0x0
        private System.Collections.Generic.List<UnityEngine.Material> m_materials;  // 0x48

        // Methods
        // RVA: 0x0A9B3AE8  token: 0x6000157
        public System.Void SetFloat(System.Int32 nameID, System.Single value) { }
        // RVA: 0x0A9B3D6C  token: 0x6000158
        public System.Void SetTexture(System.Int32 nameID, UnityEngine.Texture value) { }
        // RVA: 0x0A9B3E30  token: 0x6000159
        public System.Void SetVector(System.Int32 nameID, UnityEngine.Vector4 value) { }
        // RVA: 0x0A9B3098  token: 0x600015A
        public System.Void AddMaterial(UnityEngine.Material material) { }
        // RVA: 0x0A9B3754  token: 0x600015B
        public System.Void RemoveMaterial(UnityEngine.Material material) { }
        // RVA: 0x0A9B35FC  token: 0x600015C
        public System.Void ClearMaterials() { }
        // RVA: 0x0A9B3BAC  token: 0x600015D
        public System.Void SetKeywordEnable(System.String keyword, System.Boolean enable) { }
        // RVA: 0x0A9B37D0  token: 0x600015E
        public System.Void SetDepthOnlyEnabled(System.Boolean enable) { }
        // RVA: 0x0A9B3F14  token: 0x600015F
        public System.Void SetZTestInMaterial(System.String shaderName, System.Boolean enable) { }
        // RVA: 0x0A9B4060  token: 0x6000160
        private System.Void _RevertMaterial(UnityEngine.Material material) { }
        // RVA: 0x0A9B4294  token: 0x6000161
        public System.Void .ctor() { }
        // RVA: 0x0A9B426C  token: 0x6000162
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200003D  // size: 0x28
    public class EntityVFXCurve
    {
        // Fields
        public UnityEngine.AnimationCurve curve;  // 0x10
        public UnityEngine.Vector2 rangeEdit;  // 0x18
        public UnityEngine.Vector2 rangeLimit;  // 0x20

        // Methods
        // RVA: 0x053908C0  token: 0x6000163
        public System.Void .ctor(UnityEngine.AnimationCurve curve) { }

    }

    // TypeToken: 0x200003E  // size: 0x48
    public sealed struct EntityVFXCurveEvaluateResult
    {
        // Fields
        public UnityEngine.Rendering.ShaderPropertyType propertyType;  // 0x10
        public System.String propertyName;  // 0x18
        public System.Int32 propertyId;  // 0x20
        public System.Single floatValue;  // 0x24
        public UnityEngine.Vector4 vectorValue;  // 0x28
        public UnityEngine.Vector4 textureScaleOffset;  // 0x38

    }

    // TypeToken: 0x200003F  // size: 0x20
    public sealed struct AddMaterialConfig
    {
        // Fields
        public System.Boolean useColorTexture;  // 0x10
        public System.String colorTextureName;  // 0x18

        // Properties
        System.Boolean useCustomizeProperty { get; /* RVA: 0x03578C80 */ }

    }

    // TypeToken: 0x2000040  // size: 0x58
    public class EntityVFXCurveData
    {
        // Fields
        public System.String propertyName;  // 0x10
        public UnityEngine.Rendering.ShaderPropertyType propertyType;  // 0x18
        public Beyond.Rendering.EntityVFXCurve curve;  // 0x20
        public Beyond.Rendering.EntityVFXCurve curveX;  // 0x28
        public Beyond.Rendering.EntityVFXCurve curveY;  // 0x30
        public Beyond.Rendering.EntityVFXCurve curveZ;  // 0x38
        public Beyond.Rendering.EntityVFXCurve curveW;  // 0x40
        public UnityEngine.Gradient gradient;  // 0x48
        public System.Single colorIntensity;  // 0x50

        // Methods
        // RVA: 0x04CAE4C0  token: 0x6000165
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000041  // size: 0xD8
    public class EntityVFXAddictiveMaterialData
    {
        // Fields
        public Beyond.Rendering.EntityVFXPriorityType priorityType;  // 0x10
        public Beyond.Rendering.EntityVFXRendererMask rendererMask;  // 0x14
        public System.Boolean useScaledTime;  // 0x18
        public System.Boolean enableVertColorIfMeshHasVFXVertColor;  // 0x19
        public UnityEngine.Material material;  // 0x20
        public System.Boolean loop;  // 0x28
        public System.Boolean useDissolveCurve;  // 0x29
        public System.Boolean useScanCurve;  // 0x2a
        public System.Boolean useCutoffPosY;  // 0x2b
        public System.Boolean useCutoffPosYAutoBounds;  // 0x2c
        public System.Single cutoffPosYAbsoluteBounds;  // 0x30
        public System.Boolean useCutoffLocalDirection;  // 0x34
        public UnityEngine.Vector3 cutoffDirection;  // 0x38
        public System.Single duration;  // 0x44
        public System.Boolean useStartCurve;  // 0x48
        public UnityEngine.AnimationCurve opacityCurve;  // 0x50
        public UnityEngine.AnimationCurve dissolveCurve;  // 0x58
        public UnityEngine.AnimationCurve scanCurve;  // 0x60
        public UnityEngine.AnimationCurve cutoffPosYCurve;  // 0x68
        public System.Collections.Generic.List<Beyond.Rendering.EntityVFXCurveData> customCurves;  // 0x70
        public System.Boolean useLoopCurve;  // 0x78
        public System.Single loopDuration;  // 0x7c
        public UnityEngine.AnimationCurve loopCurve;  // 0x80
        public UnityEngine.AnimationCurve dissolveLoopCurve;  // 0x88
        public System.Collections.Generic.List<Beyond.Rendering.EntityVFXCurveData> customLoopCurves;  // 0x90
        public System.Boolean useEndCurve;  // 0x98
        public System.Single endDuration;  // 0x9c
        public UnityEngine.AnimationCurve endCurve;  // 0xa0
        public UnityEngine.AnimationCurve dissolveEndCurve;  // 0xa8
        public System.Collections.Generic.List<Beyond.Rendering.EntityVFXCurveData> customEndCurves;  // 0xb0
        public System.Boolean useAddictiveProperties;  // 0xb8
        public System.Boolean useAddictivePropertiesUpdate;  // 0xb9
        public System.Collections.Generic.List<System.String> addictiveVectorProperties;  // 0xc0
        public System.Boolean useColorTexture;  // 0xc8
        public System.String colorTextureName;  // 0xd0

        // Methods
        // RVA: 0x045DF500  token: 0x6000166
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000042  // size: 0x38
    public class EntityVFXAddictiveMaterialAsset : Beyond.Rendering.EntityVFXAssetBase
    {
        // Fields
        public Beyond.Rendering.EntityVFXAddictiveMaterialData data;  // 0x30

        // Methods
        // RVA: 0x046EA7B0  token: 0x6000167
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000043  // size: 0x78
    public class EntityVFXAfterImageData
    {
        // Fields
        public Beyond.Rendering.EntityVFXRendererMask rendererMask;  // 0x10
        public System.Boolean useScaledTime;  // 0x14
        public Beyond.Rendering.EntityVFXAfterImageData.TimeMode timeMode;  // 0x18
        public System.Boolean loop;  // 0x1c
        public System.Single duration;  // 0x20
        public System.Single timeInterval;  // 0x24
        public System.Collections.Generic.List<System.Single> timeBurst;  // 0x28
        public UnityEngine.Material depthOnlyMaterial;  // 0x30
        public UnityEngine.Material material;  // 0x38
        public UnityEngine.Material materialOutline;  // 0x40
        public System.Single lifeTime;  // 0x48
        public UnityEngine.AnimationCurve opacityCurve;  // 0x50
        public UnityEngine.AnimationCurve dissolveCurve;  // 0x58
        public System.Collections.Generic.List<Beyond.Rendering.EntityVFXCurveData> customCurves;  // 0x60
        public UnityEngine.AnimationCurve opacityCurveOutline;  // 0x68
        public System.Collections.Generic.List<Beyond.Rendering.EntityVFXCurveData> customCurvesOutline;  // 0x70

        // Methods
        // RVA: 0x04D39C30  token: 0x6000168
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000045  // size: 0x38
    public class EntityVFXAfterImageAsset : Beyond.Rendering.EntityVFXAssetBase
    {
        // Fields
        public Beyond.Rendering.EntityVFXAfterImageData data;  // 0x30

        // Methods
        // RVA: 0x046EA7B0  token: 0x6000169
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000046  // size: 0x28
    public sealed struct EntityCustomizeRendererType
    {
        // Fields
        public System.Boolean useContainStr;  // 0x10
        public System.String rendererNameRegex;  // 0x18
        public Beyond.Rendering.EntityVFXRendererType type;  // 0x20

    }

    // TypeToken: 0x2000047  // size: 0x40
    public class EntityVFXAsset : UnityEngine.ScriptableObject
    {
        // Fields
        public Beyond.Rendering.RenderHelperType renderHelperType;  // 0x18
        public System.Boolean useCustomRenderer;  // 0x1c
        public System.Collections.Generic.List<Beyond.Rendering.EntityCustomizeRendererType> customRenderers;  // 0x20
        public System.Boolean meshHasVFXVertColor;  // 0x28
        public System.Boolean useCustomRendererProperty;  // 0x29
        public Beyond.Rendering.EntityCustomizeRendererPropertyConfig customRendererPropertyConfig;  // 0x30
        private System.Collections.Generic.List<Beyond.Rendering.EntityRendererTypeConfig> m_rendererTypeConfigs;  // 0x38

        // Methods
        // RVA: 0x0432BAC0  token: 0x600016A
        public System.Collections.Generic.List<Beyond.Rendering.EntityRendererTypeConfig> GetRendererTypeConfigs(System.Boolean forceUpdate) { }
        // RVA: 0x0495D100  token: 0x600016B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000048  // size: 0x14
    public sealed struct EntityVFXRendererType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.EntityVFXRendererType Normal;  // const
        public static Beyond.Rendering.EntityVFXRendererType Body;  // const
        public static Beyond.Rendering.EntityVFXRendererType Cloth;  // const
        public static Beyond.Rendering.EntityVFXRendererType Eyebrow;  // const
        public static Beyond.Rendering.EntityVFXRendererType EyeShadow;  // const
        public static Beyond.Rendering.EntityVFXRendererType Face;  // const
        public static Beyond.Rendering.EntityVFXRendererType Hair;  // const
        public static Beyond.Rendering.EntityVFXRendererType HairShadow;  // const
        public static Beyond.Rendering.EntityVFXRendererType Iris;  // const
        public static Beyond.Rendering.EntityVFXRendererType Weapon1;  // const
        public static Beyond.Rendering.EntityVFXRendererType Weapon2;  // const
        public static Beyond.Rendering.EntityVFXRendererType Part1;  // const
        public static Beyond.Rendering.EntityVFXRendererType Part2;  // const
        public static Beyond.Rendering.EntityVFXRendererType Part3;  // const
        public static Beyond.Rendering.EntityVFXRendererType Part4;  // const
        public static Beyond.Rendering.EntityVFXRendererType Part5;  // const
        public static Beyond.Rendering.EntityVFXRendererType Part6;  // const
        public static Beyond.Rendering.EntityVFXRendererType Part7;  // const
        public static Beyond.Rendering.EntityVFXRendererType Part8;  // const
        public static Beyond.Rendering.EntityVFXRendererType Part9;  // const
        public static Beyond.Rendering.EntityVFXRendererType Part10;  // const
        public static Beyond.Rendering.EntityVFXRendererType Part11;  // const
        public static Beyond.Rendering.EntityVFXRendererType Part12;  // const
        public static Beyond.Rendering.EntityVFXRendererType Part13;  // const
        public static Beyond.Rendering.EntityVFXRendererType Part14;  // const
        public static Beyond.Rendering.EntityVFXRendererType Part15;  // const
        public static Beyond.Rendering.EntityVFXRendererType AlphaQuadAvoidAddMaterial;  // const
        public static Beyond.Rendering.EntityVFXRendererType Count;  // const

    }

    // TypeToken: 0x2000049  // size: 0x14
    public sealed struct EntityVFXRendererMask
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.EntityVFXRendererMask Normal;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Body;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Cloth;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Eyebrow;  // const
        public static Beyond.Rendering.EntityVFXRendererMask EyeShadow;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Face;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Hair;  // const
        public static Beyond.Rendering.EntityVFXRendererMask HairShadow;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Iris;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Weapon1;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Weapon2;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Part1;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Part2;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Part3;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Part4;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Part5;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Part6;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Part7;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Part8;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Part9;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Part10;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Part11;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Part12;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Part13;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Part14;  // const
        public static Beyond.Rendering.EntityVFXRendererMask Part15;  // const
        public static Beyond.Rendering.EntityVFXRendererMask AlphaQuadAvoidAddMaterial;  // const
        public static Beyond.Rendering.EntityVFXRendererMask All;  // const
        public static Beyond.Rendering.EntityVFXRendererMask AllWithoutAlphaQuad;  // const

    }

    // TypeToken: 0x200004A  // size: 0x14
    public sealed struct EntityVFXPriorityType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.EntityVFXPriorityType Normal;  // const
        public static Beyond.Rendering.EntityVFXPriorityType UltiSkillTimeline;  // const

    }

    // TypeToken: 0x200004B  // size: 0x14
    public sealed struct SettingLodLevel
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.SettingLodLevel None;  // const
        public static Beyond.Rendering.SettingLodLevel Low;  // const
        public static Beyond.Rendering.SettingLodLevel Mid;  // const
        public static Beyond.Rendering.SettingLodLevel High;  // const
        public static Beyond.Rendering.SettingLodLevel Highest;  // const

    }

    // TypeToken: 0x200004C  // size: 0x14
    public sealed struct AssetPlatformLayers
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.AssetPlatformLayers PC;  // const
        public static Beyond.Rendering.AssetPlatformLayers Mobile;  // const
        public static Beyond.Rendering.AssetPlatformLayers Other;  // const

    }

    // TypeToken: 0x200004D  // size: 0x14
    public sealed struct EffectTargetLayers
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.EffectTargetLayers Main;  // const
        public static Beyond.Rendering.EffectTargetLayers Guard;  // const

    }

    // TypeToken: 0x200004E  // size: 0x30
    public class EntityVFXAssetBase : UnityEngine.ScriptableObject
    {
        // Fields
        public System.Boolean useECSRenderer;  // 0x18
        public Beyond.Rendering.SettingLodLevel settingLodLevel;  // 0x1c
        public Beyond.Rendering.AssetPlatformLayers platformLayer;  // 0x20
        public Beyond.Rendering.EffectTargetLayers targetLayer;  // 0x24
        private System.String m_assetName;  // 0x28

        // Properties
        System.String assetName { get; /* RVA: 0x0A9B4418 */ }

        // Methods
        // RVA: 0x05397B78  token: 0x600016D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004F  // size: 0x14
    public sealed struct VFXType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.VFXType Addictive;  // const
        public static Beyond.Rendering.VFXType Dissolve;  // const
        public static Beyond.Rendering.VFXType Max;  // const

    }

    // TypeToken: 0x2000050  // size: 0x48
    public class EntityVFXControllerBase
    {
        // Fields
        public Beyond.Rendering.SettingLodLevel settingLodLevel;  // 0x10
        public Beyond.Rendering.AssetPlatformLayers platformLayer;  // 0x14
        public Beyond.Rendering.EffectTargetLayers targetLayer;  // 0x18
        protected Beyond.Rendering.IRendererHelper m_owner;  // 0x20
        protected System.Boolean m_isPlaying;  // 0x28
        private System.Boolean m_isHiding;  // 0x29
        protected System.Boolean m_isKeyword;  // 0x2a
        private System.String m_vfxName;  // 0x30
        private System.Single m_destroyTime;  // 0x38
        private System.Single m_lastTickTime;  // 0x3c
        public System.Boolean preload;  // 0x40
        public System.Boolean fromTimeline;  // 0x41

        // Properties
        System.Boolean isPlaying { get; /* RVA: 0x0402AB10 */ }
        System.Boolean isStopping { get; /* RVA: 0x0A9B49B0 */ }
        System.Boolean isHiding { get; /* RVA: 0x03099050 */ }
        System.Boolean isKeyword { get; /* RVA: 0x03098E80 */ }
        System.String vfxName { get; /* RVA: 0x031DA370 */ }
        System.Single destroyTime { get; /* RVA: 0x0302E1E0 */ set; /* RVA: 0x031DA450 */ }
        System.Single lastTickTime { get; /* RVA: 0x03577E70 */ set; /* RVA: 0x03577EA0 */ }

        // Methods
        // RVA: 0x031DA620  token: 0x6000177
        public System.Void SetOwner(System.String name, Beyond.Rendering.IRendererHelper owner) { }
        // RVA: 0x031D9B60  token: 0x6000178
        public System.Void InitLOD(Beyond.Rendering.EntityVFXAssetBase vfxAssetBase) { }
        // RVA: 0x031DA3A0  token: 0x6000179
        public System.Void SetName(System.String name) { }
        // RVA: 0x0A9B47FC  token: 0x600017A
        public System.Boolean PoolTick() { }
        // RVA: 0x0A9B4484  token: 0x600017B
        public System.Void Destroy() { }
        // RVA: 0x0A9B4684  token: 0x600017C
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x0A9B47B8  token: 0x600017D
        public virtual System.Void Play() { }
        // RVA: 0x0A9B4908  token: 0x600017E
        public virtual System.Void Stop(System.Boolean forceStop) { }
        // RVA: 0x0A9B4848  token: 0x600017F
        public virtual System.Void Replay() { }
        // RVA: 0x0A9B495C  token: 0x6000180
        public virtual System.Void Tick(System.Single logicDeltaTime) { }
        // RVA: 0x03577B00  token: 0x6000181
        protected System.Void RefreshLastTickTime() { }
        // RVA: 0x03577DE0  token: 0x6000182
        protected System.Single GetDeltaTimeFromLastTick(System.Boolean useScaledTime) { }
        // RVA: 0x0A9B45EC  token: 0x6000183
        public virtual Beyond.Rendering.EntityVFXPriorityType GetPriorityType() { }
        // RVA: 0x0A9B4638  token: 0x6000184
        public virtual Beyond.Rendering.EntityVFXRendererMask GetRendererMask() { }
        // RVA: 0x0A9B4554  token: 0x6000185
        public virtual System.Boolean GetAllowSimultaneousKeyword() { }
        // RVA: 0x0A9B45A0  token: 0x6000186
        public virtual System.Boolean GetNeedTick() { }
        // RVA: 0x0485AEF0  token: 0x6000187
        public System.Void Hide() { }
        // RVA: 0x0A9B46C8  token: 0x6000188
        protected virtual System.Void OnHide() { }
        // RVA: 0x049AFA80  token: 0x6000189
        public System.Void Show() { }
        // RVA: 0x0A9B4774  token: 0x600018A
        protected virtual System.Void OnShow() { }
        // RVA: 0x0A9B488C  token: 0x600018B
        public System.Void Sample(System.Single time, System.Boolean ending) { }
        // RVA: 0x0A9B470C  token: 0x600018C
        protected virtual System.Void OnSample(System.Single time, System.Boolean ending) { }
        // RVA: 0x04D8E730  token: 0x600018D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000051  // size: 0x58
    public abstract class EntityVFXControllerStateBase : Beyond.Rendering.EntityVFXControllerBase
    {
        // Fields
        private System.Single <m_passTime>k__BackingField;  // 0x48
        private System.Single <m_loopPassTime>k__BackingField;  // 0x4c
        private System.Single <m_endPassTime>k__BackingField;  // 0x50
        private Beyond.Rendering.EntityVFXControllerStateBase.State m_curState;  // 0x54

        // Properties
        System.Single m_passTime { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Single m_loopPassTime { get; /* RVA: 0x04D87860 */ set; /* RVA: 0x04D91480 */ }
        System.Single m_endPassTime { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }
        System.Boolean isStopping { get; /* RVA: 0x0A9B4C04 */ }
        System.Boolean useScaledTime { get; /* RVA: 0x0A9B4D8C */ }
        System.Boolean useStartCurve { get; /* RVA: 0x0A9B4DD8 */ }
        System.Boolean useLoopCurve { get; /* RVA: 0x0A9B4D40 */ }
        System.Boolean useEndCurve { get; /* RVA: 0x0A9B4CF4 */ }
        System.Boolean loop { get; /* RVA: 0x0A9B4CA8 */ }
        System.Single duration { get; /* RVA: 0x0A9B4B64 */ }
        System.Single loopDuration { get; /* RVA: 0x0A9B4C54 */ }
        System.Single endDuration { get; /* RVA: 0x0A9B4BB8 */ }

        // Methods
        // RVA: 0x0455A2A0  token: 0x6000195
        public virtual System.Void Play() { }
        // RVA: 0x0A9B4B00  token: 0x6000196
        public virtual System.Void Replay() { }
        // RVA: 0x045DFBB0  token: 0x6000197
        public virtual System.Void Stop(System.Boolean forceStop) { }
        // RVA: 0x0402AA60  token: 0x6000198
        public virtual System.Boolean GetNeedTick() { }
        // RVA: 0x03577B40  token: 0x6000199
        public virtual System.Void Tick(System.Single logicDeltaTime) { }
        // RVA: 0x0A9B49FC  token: 0x600019A
        protected virtual System.Void OnSample(System.Single time, System.Boolean ending) { }
        // RVA: -1  // abstract  token: 0x60001A3
        protected virtual System.Void UpdateStart() { }
        // RVA: -1  // abstract  token: 0x60001A4
        protected virtual System.Void UpdateLoop() { }
        // RVA: -1  // abstract  token: 0x60001A5
        protected virtual System.Void UpdateStop() { }
        // RVA: 0x045DFC10  token: 0x60001A6
        private System.Void _FinalStop() { }
        // RVA: -1  // abstract  token: 0x60001A7
        protected virtual System.Void OnFinalStop() { }
        // RVA: 0x04D8E730  token: 0x60001A8
        protected System.Void .ctor() { }
        // RVA: 0x06C1D558  token: 0x60001A9
        public System.Boolean <>iFixBaseProxy_get_isStopping() { }
        // RVA: 0x06C1D538  token: 0x60001AA
        public System.Void <>iFixBaseProxy_Play() { }
        // RVA: 0x06C1D540  token: 0x60001AB
        public System.Void <>iFixBaseProxy_Replay() { }
        // RVA: 0x06C1D548  token: 0x60001AC
        public System.Void <>iFixBaseProxy_Stop(System.Boolean P0) { }
        // RVA: 0x06C1E0F8  token: 0x60001AD
        public System.Boolean <>iFixBaseProxy_GetNeedTick() { }
        // RVA: 0x06C1D550  token: 0x60001AE
        public System.Void <>iFixBaseProxy_Tick(System.Single P0) { }
        // RVA: 0x06C1D528  token: 0x60001AF
        public System.Void <>iFixBaseProxy_OnSample(System.Single P0, System.Boolean P1) { }

    }

    // TypeToken: 0x2000053  // size: 0x14
    public sealed struct EntityCustomizePropertyName
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.EntityCustomizePropertyName None;  // const
        public static Beyond.Rendering.EntityCustomizePropertyName AlphaTex;  // const
        public static Beyond.Rendering.EntityCustomizePropertyName NormalTex;  // const

    }

    // TypeToken: 0x2000054  // size: 0x14
    public sealed struct EntityCustomizeMatchMethod
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.EntityCustomizeMatchMethod None;  // const
        public static Beyond.Rendering.EntityCustomizeMatchMethod All;  // const
        public static Beyond.Rendering.EntityCustomizeMatchMethod RendererName;  // const
        public static Beyond.Rendering.EntityCustomizeMatchMethod SourceMaterial0;  // const
        public static Beyond.Rendering.EntityCustomizeMatchMethod SourceMesh;  // const

    }

    // TypeToken: 0x2000055  // size: 0x28
    public class EntityCustomizeRendererPropertyConfig
    {
        // Fields
        public System.Boolean useColorTexture;  // 0x10
        public System.Boolean useAutoFindColorTexture;  // 0x11
        public System.String autoFindColorTextureName;  // 0x18
        public Beyond.SerializeFieldDictionary<UnityEngine.Material,UnityEngine.Texture> preConfigColorTextureDict;  // 0x20

        // Methods
        // RVA: 0x0A9B2D2C  token: 0x60001B0
        public UnityEngine.Texture GetColorTexture(UnityEngine.Material material) { }
        // RVA: 0x041E1670  token: 0x60001B1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000056  // size: 0x14
    public sealed struct EntityVFXKeywordEnum
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.EntityVFXKeywordEnum None;  // const
        public static Beyond.Rendering.EntityVFXKeywordEnum Dissolve;  // const

    }

    // TypeToken: 0x2000057  // size: 0xC8
    public class EntityVFXDissolveData
    {
        // Fields
        public Beyond.Rendering.EntityVFXPriorityType priorityType;  // 0x10
        public Beyond.Rendering.EntityVFXRendererMask rendererMask;  // 0x14
        public System.Boolean useScaledTime;  // 0x18
        public System.Boolean allowSimultaneous;  // 0x19
        public System.Boolean loop;  // 0x1a
        public System.Single duration;  // 0x1c
        public Beyond.Rendering.EntityVFXDissolveData.UVSet uvSet;  // 0x20
        public System.Boolean useCutoffPosY;  // 0x24
        public System.Boolean useCutoffPosYAutoBounds;  // 0x25
        public System.Single cutoffPosYAbsoluteBounds;  // 0x28
        public System.Boolean useCutoffLocalDirection;  // 0x2c
        public UnityEngine.Vector3 cutoffDirection;  // 0x30
        public System.Single cutoffUseDissolve;  // 0x3c
        public System.Boolean stopShadowCasting;  // 0x40
        public System.Boolean stopRayTracingMeanTime;  // 0x41
        public System.Single stopShadowCastingDelay;  // 0x44
        public System.Single revertShadowCastingDelay;  // 0x48
        public UnityEngine.Texture2D dissolveTexture;  // 0x50
        public UnityEngine.Vector4 dissolveTextureTillingOffset;  // 0x58
        public System.Single dissolveEdgeSharp;  // 0x68
        public UnityEngine.Color dissolveEmissiveColor;  // 0x6c
        public System.Single dissolveEmissiveEdge;  // 0x7c
        public System.Boolean useLocalScreenUV;  // 0x80
        public UnityEngine.AnimationCurve dissolveValueCurve;  // 0x88
        public UnityEngine.AnimationCurve cutoffPosYCurve;  // 0x90
        public System.Boolean useLoopCurve;  // 0x98
        public System.Single loopDuration;  // 0x9c
        public UnityEngine.AnimationCurve dissolveValueLoopCurve;  // 0xa0
        public UnityEngine.AnimationCurve cutoffPosYLoopCurve;  // 0xa8
        public System.Boolean useEndCurve;  // 0xb0
        public System.Single endDuration;  // 0xb4
        public UnityEngine.AnimationCurve dissolveValueEndCurve;  // 0xb8
        public UnityEngine.AnimationCurve cutoffPosYEndCurve;  // 0xc0

        // Methods
        // RVA: 0x047A0220  token: 0x60001B2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000059  // size: 0x38
    public class EntityVFXDissolveAsset : Beyond.Rendering.EntityVFXAssetBase
    {
        // Fields
        public Beyond.Rendering.EntityVFXDissolveData data;  // 0x30

        // Methods
        // RVA: 0x046EA7B0  token: 0x60001B3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005A  // size: 0x20
    public class EntityFactoryRendererInfo
    {
        // Fields
        public System.Int32 subMeshCount;  // 0x10
        public System.Boolean enableVAT;  // 0x14
        public System.Int32 vatIndex;  // 0x18
        public System.Int32 initMaterialsCount;  // 0x1c

        // Methods
        // RVA: 0x041E1670  token: 0x60001B4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005B  // size: 0x30
    public class EntityVFXFactoryAddedMaterialManager
    {
        // Fields
        private static readonly System.Int32 S_ENABLE_FACTORY_VAT;  // static @ 0x0
        private static readonly System.Int32 S_FACTORY_VAT_MAP;  // static @ 0x4
        private static readonly System.Int32 S_FACTORY_VAT_MAP_PARAMS;  // static @ 0x8
        private System.Collections.Generic.List<Beyond.Rendering.EntityFactoryRendererInfo> m_factoryRendererInfos;  // 0x10
        private System.Collections.Generic.List<Beyond.Rendering.EntityVFXFactoryAddedMaterialManager.VATData> m_vatDatas;  // 0x18
        private System.Collections.Generic.List<Beyond.Rendering.EntityVFXFactoryAddedMaterialManager.AddedMaterialData> m_addedMaterialDatas;  // 0x20
        private System.Collections.Generic.Dictionary<UnityEngine.Material,Beyond.Rendering.EntityVFXFactoryAddedMaterialManager.AddedMaterialData> m_addedMaterialDataDict;  // 0x28

        // Methods
        // RVA: 0x0A9B4EA8  token: 0x60001B5
        public System.Void Init(System.Collections.Generic.List<Beyond.Rendering.EntityFactoryRendererInfo> factoryRendererInfos) { }
        // RVA: 0x0A9B52F8  token: 0x60001B6
        private System.Void _AddVATMaterial(UnityEngine.Material src) { }
        // RVA: 0x0A9B56A4  token: 0x60001B7
        private UnityEngine.Material _GetVATMaterial(Beyond.Rendering.EntityFactoryRendererInfo rendererInfo, UnityEngine.Material src) { }
        // RVA: 0x0A9B55C0  token: 0x60001B8
        private System.Int32 _GetAddedMaterialIndex(Beyond.Rendering.EntityFactoryRendererInfo rendererInfo, UnityEngine.Material src) { }
        // RVA: 0x0A9B5758  token: 0x60001B9
        private System.Void _RemoveVATMaterial(UnityEngine.Material src) { }
        // RVA: 0x0A9B4E24  token: 0x60001BA
        public System.Void AddMaterialToAllRenderer(UnityEngine.Material material) { }
        // RVA: 0x0A9B4FBC  token: 0x60001BB
        public System.Void RemoveMaterialFromAllRenderer(UnityEngine.Material material) { }
        // RVA: 0x0A9B50EC  token: 0x60001BC
        public System.Void SetFloatToFactoryMaterial(UnityEngine.Material material, System.Int32 nameId, System.Single value) { }
        // RVA: 0x0A9B51E0  token: 0x60001BD
        public System.Void SetVectorToFactoryMaterial(UnityEngine.Material material, System.Int32 nameId, UnityEngine.Vector4 value) { }
        // RVA: 0x0A9B58C8  token: 0x60001BE
        public System.Void .ctor() { }
        // RVA: 0x0A9B5860  token: 0x60001BF
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000060  // size: 0x10
    public static class EntityVFXFactoryMaterialCache
    {
        // Fields
        private static readonly System.Int32 S_FACTORY_VAT_MAP;  // static @ 0x0
        private static readonly System.Int32 S_FACTORY_VAT_MAP_PARAMS;  // static @ 0x4
        private static System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.Dictionary<System.Int32,Beyond.Rendering.EntityVFXFactoryMaterialCache.VATMaterialData>> s_cachedMaterials;  // static @ 0x8

        // Methods
        // RVA: 0x0A9B59B0  token: 0x60001C6
        public static UnityEngine.Material GetVATMaterial(UnityEngine.Material srcMaterial, UnityEngine.Texture factoryVATMap, UnityEngine.Vector4 factoryVATMapParams) { }
        // RVA: 0x0A9B5C2C  token: 0x60001C7
        public static System.Void ReleaseVATMaterial(UnityEngine.Material srcMaterial, UnityEngine.Texture factoryVATMap) { }
        // RVA: 0x0A9B5DE4  token: 0x60001C8
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000062  // size: 0x50
    public class EntityVFXSetParamsData
    {
        // Fields
        public Beyond.Rendering.EntityVFXPriorityType priorityType;  // 0x10
        public Beyond.Rendering.EntityVFXRendererMask rendererMask;  // 0x14
        public System.Boolean useScaledTime;  // 0x18
        public System.Boolean loop;  // 0x19
        public Beyond.Rendering.EntityVFXSetParamsData.Type type;  // 0x1c
        public System.Single duration;  // 0x20
        public System.Collections.Generic.List<Beyond.Rendering.EntityVFXCurveData> customCurves;  // 0x28
        public System.Boolean useLoopCurve;  // 0x30
        public System.Single loopDuration;  // 0x34
        public System.Collections.Generic.List<Beyond.Rendering.EntityVFXCurveData> customLoopCurves;  // 0x38
        public System.Single endDuration;  // 0x40
        public System.Collections.Generic.List<Beyond.Rendering.EntityVFXCurveData> customEndCurves;  // 0x48

        // Methods
        // RVA: 0x04DA6BF0  token: 0x60001CA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000064  // size: 0x38
    public class EntityVFXSetParamsAsset : Beyond.Rendering.EntityVFXAssetBase
    {
        // Fields
        public Beyond.Rendering.EntityVFXSetParamsData data;  // 0x30

        // Methods
        // RVA: 0x046EA7B0  token: 0x60001CB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000065  // size: 0x10
    public static class EntityVFXUtils
    {
        // Methods
        // RVA: 0x0302DC00  token: 0x60001CC
        public static Beyond.Rendering.EntityVFXRendererMask ToMask(Beyond.Rendering.EntityVFXRendererType type) { }
        // RVA: 0x032C5080  token: 0x60001CD
        public static System.Void CalculateCutOffBounds(Beyond.Rendering.IRendererHelper owner, System.Boolean useCutoffLocalDirection, UnityEngine.Vector3 cutoffDirectionData, System.Boolean useCutoffPosYAutoBounds, System.Single cutoffPosYAbsoluteBounds, System.Single& cutoffMin, System.Single& cutoffMax, UnityEngine.Vector3& cutoffDirection) { }
        // RVA: 0x032C4EA0  token: 0x60001CE
        public static System.ValueTuple<System.Single,System.Single> GetBoundsMinMaxOfDirection(UnityEngine.Bounds bounds, UnityEngine.Vector3 direction) { }
        // RVA: 0x03578D40  token: 0x60001CF
        public static System.Void ApplyEntityVFXCurves(UnityEngine.Material material, System.Collections.Generic.List<Beyond.Rendering.EntityVFXCurveData> curveDataList, System.Single schedule) { }
        // RVA: 0x03579520  token: 0x60001D0
        public static Beyond.Rendering.EntityVFXCurveEvaluateResult Evaluate(Beyond.Rendering.EntityVFXCurveData curveData, System.Single schedule) { }
        // RVA: 0x03579450  token: 0x60001D1
        private static System.Void _UpdateCurve(UnityEngine.Material material, Beyond.Rendering.EntityVFXCurveData curveData, System.Single schedule) { }
        // RVA: 0x0A9B5E80  token: 0x60001D2
        public static System.Boolean IsCharacterShader(System.String name) { }
        // RVA: 0x0A9A7704  token: 0x60001D3
        public static System.Boolean IsLitShader(System.String name) { }

    }

    // TypeToken: 0x2000066
    public interface ICaptureMesh
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001D4
        public virtual Beyond.Rendering.EntityRenderCapturedEntity Capture(UnityEngine.GameObject placeRoot, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: -1  // abstract  token: 0x60001D5
        public virtual System.Void Release(Beyond.Rendering.EntityRenderCapturedEntity mesh) { }

    }

    // TypeToken: 0x2000067
    public interface IEntityVFXTimelineHost
    {
        // Properties
        UnityEngine.GameObject gameObject { get; /* RVA: -1  // abstract */ }
        System.Boolean inited { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60001D8
        public virtual System.Void InitAll() { }
        // RVA: -1  // abstract  token: 0x60001D9
        public virtual System.Void ResetAll() { }
        // RVA: -1  // abstract  token: 0x60001DA
        public virtual System.Void SampleVFX(System.String assetName, System.Boolean playing, System.Single time, System.Boolean ending) { }
        // RVA: -1  // abstract  token: 0x60001DB
        public virtual System.Void AddTimelineEffect(Beyond.Rendering.EntityVFXAssetBase asset) { }

    }

    // TypeToken: 0x2000068  // size: 0x14
    public sealed struct RenderHelperType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.RenderHelperType Unknown;  // const
        public static Beyond.Rendering.RenderHelperType Character;  // const
        public static Beyond.Rendering.RenderHelperType Lit;  // const
        public static Beyond.Rendering.RenderHelperType Mix;  // const

    }

    // TypeToken: 0x2000069
    public interface IRendererHelper
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001DC
        public virtual System.Void Reset() { }
        // RVA: -1  // abstract  token: 0x60001DD
        public virtual System.Void Play(System.String vfxName) { }
        // RVA: -1  // abstract  token: 0x60001DE
        public virtual System.Void Stop(System.String vfxName) { }
        // RVA: -1  // abstract  token: 0x60001DF
        public virtual System.Void ShowAll() { }
        // RVA: -1  // abstract  token: 0x60001E0
        public virtual System.Void HideAll() { }
        // RVA: -1  // abstract  token: 0x60001E1
        public virtual System.Void SetDitherAlpha(System.Single ditherAlpha, System.Int32 handle) { }
        // RVA: -1  // abstract  token: 0x60001E2
        public virtual System.Single GetDitherAlpha() { }
        // RVA: -1  // abstract  token: 0x60001E3
        public virtual System.Void ResetAllDitherAlpha() { }
        // RVA: -1  // abstract  token: 0x60001E4
        public virtual System.Void ClearEffectCache(System.String name) { }
        // RVA: -1  // abstract  token: 0x60001E5
        public virtual System.Void OnEntityVFXControllerDestroying(Beyond.Rendering.EntityVFXControllerBase controller) { }
        // RVA: -1  // abstract  token: 0x60001E6
        public virtual System.Void SetVisibleByNameContainsStr(System.String name, System.Boolean visible) { }
        // RVA: -1  // abstract  token: 0x60001E7
        public virtual System.Void SetVisibleByRenderer(UnityEngine.Renderer renderer, System.Boolean visible) { }
        // RVA: -1  // abstract  token: 0x60001E8
        public virtual System.Int32 AddMaterialToAllRenderer(UnityEngine.Material material, Beyond.Rendering.EntityVFXRendererMask rendererMask, Beyond.Rendering.AddMaterialConfig config) { }
        // RVA: -1  // abstract  token: 0x60001E9
        public virtual System.Void RemoveMaterialFromAllRenderer(System.Int32 handle) { }
        // RVA: -1  // abstract  token: 0x60001EA
        public virtual System.Boolean GetAddedMaterialsOfCustomizeProperties(System.Int32 handle, System.Collections.Generic.List<UnityEngine.Material> result) { }
        // RVA: -1  // abstract  token: 0x60001EB
        public virtual System.Void SetKeywordEnableToAllMaterial(System.String keyword, System.Boolean enable, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: -1  // abstract  token: 0x60001EC
        public virtual System.Void SetFloatToAllMaterial(System.Int32 nameId, System.Single value, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: -1  // abstract  token: 0x60001ED
        public virtual System.Void SetFloatToCustomPerDraw(Beyond.Rendering.EntityRenderHelperCustomPerDrawType type, System.Single value, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: -1  // abstract  token: 0x60001EE
        public virtual System.Void SetTextureToAllMaterial(System.Int32 nameId, UnityEngine.Texture value, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: -1  // abstract  token: 0x60001EF
        public virtual System.Void SetVectorToAllMaterial(System.Int32 nameId, UnityEngine.Vector4 value, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: -1  // abstract  token: 0x60001F0
        public virtual System.Void SetFloatToFactoryMaterial(UnityEngine.Material material, System.Int32 nameId, System.Single value) { }
        // RVA: -1  // abstract  token: 0x60001F1
        public virtual System.Void SetVectorToFactoryMaterial(UnityEngine.Material material, System.Int32 nameId, UnityEngine.Vector4 value) { }
        // RVA: -1  // abstract  token: 0x60001F2
        public virtual System.Void SetAddictiveFloatProperty(System.Int32 propertyId, System.Single value) { }
        // RVA: -1  // abstract  token: 0x60001F3
        public virtual System.Single GetAddictiveFloatProperty(System.Int32 propertyId, System.Single defaultValue) { }
        // RVA: -1  // abstract  token: 0x60001F4
        public virtual System.Void SetAddictiveVectorProperty(System.Int32 propertyId, UnityEngine.Vector4 value) { }
        // RVA: -1  // abstract  token: 0x60001F5
        public virtual UnityEngine.Vector4 GetAddictiveVectorProperty(System.Int32 propertyId, UnityEngine.Vector4 defaultValue) { }
        // RVA: -1  // abstract  token: 0x60001F6
        public virtual System.Void SetAddictiveTextureProperty(System.Int32 propertyId, UnityEngine.Texture value) { }
        // RVA: -1  // abstract  token: 0x60001F7
        public virtual UnityEngine.Texture GetAddictiveTextureProperty(System.Int32 propertyId, UnityEngine.Texture defaultValue) { }
        // RVA: -1  // abstract  token: 0x60001F8
        public virtual System.Void StopShadowCasting(System.Boolean stopRayTracing) { }
        // RVA: -1  // abstract  token: 0x60001F9
        public virtual System.Void RevertShadowCastingMode() { }
        // RVA: -1  // abstract  token: 0x60001FA
        public virtual System.Void SetDepthOnlyEnabledToAllMaterial(System.Boolean enable, Beyond.Rendering.EntityVFXRendererMask rendererMask) { }
        // RVA: -1  // abstract  token: 0x60001FB
        public virtual System.Boolean SetManualDitherEnable(System.Boolean enable) { }
        // RVA: -1  // abstract  token: 0x60001FC
        public virtual System.Void SetManualDitherAlphaValue(System.Single value) { }
        // RVA: -1  // abstract  token: 0x60001FD
        public virtual UnityEngine.Bounds GetRendererBounds() { }
        // RVA: -1  // abstract  token: 0x60001FE
        public virtual UnityEngine.Matrix4x4 GetTransform() { }
        // RVA: -1  // abstract  token: 0x60001FF
        public virtual System.Boolean IsFactory() { }
        // RVA: -1  // abstract  token: 0x6000200
        public virtual Beyond.Rendering.RenderHelperType GetRenderHelperType(System.Boolean forceCalculate) { }
        // RVA: -1  // abstract  token: 0x6000201
        public virtual System.Void SetTimeScale(System.Single timeScale) { }
        // RVA: -1  // abstract  token: 0x6000202
        public virtual System.Single GetTimeScale() { }
        // RVA: -1  // abstract  token: 0x6000203
        public virtual System.Boolean GetMeshHasVFXVertColor() { }
        // RVA: -1  // abstract  token: 0x6000204
        public virtual System.Boolean CanPlay(Beyond.Rendering.SettingLodLevel settingLodLevel, Beyond.Rendering.AssetPlatformLayers platformLayer, Beyond.Rendering.EffectTargetLayers targetLayer) { }

    }

    // TypeToken: 0x200006A  // size: 0x50
    public class CommonVAT : UnityEngine.MonoBehaviour
    {
        // Fields
        private static readonly System.Int32 S_COMMON_VAT_PLAY_PARAMS;  // static @ 0x0
        public Beyond.Rendering.RuntimeVATData data;  // 0x18
        private System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.List<Beyond.Rendering.RuntimeVATAnimationClip>> m_animDic;  // 0x20
        private System.Collections.Generic.List<Beyond.Rendering.RuntimeVATAnimationClip> m_curClips;  // 0x28
        private Beyond.Rendering.VATPropertySetter m_propertySetter;  // 0x30
        private System.Int32 m_curIndex;  // 0x38
        private System.Int32 m_nextPlayTimerId;  // 0x3c
        private System.Boolean m_hasNextAnimToPlay;  // 0x40
        private System.String m_nextAnimName;  // 0x48

        // Properties
        System.String PropertySetterType { get; /* RVA: 0x0A9B2B1C */ }

        // Methods
        // RVA: 0x0A9B2330  token: 0x6000206
        private System.Void Start() { }
        // RVA: 0x0A9B2298  token: 0x6000207
        public System.Void Play(System.String name, System.Boolean playWhenComplete) { }
        // RVA: 0x0A9B2674  token: 0x6000208
        private System.Void _DoPlay(System.String name) { }
        // RVA: 0x0A9B27F4  token: 0x6000209
        private System.Void _NextPlay() { }
        // RVA: 0x0A9B25A8  token: 0x600020A
        private System.Void _CreateTimer(System.Single time, System.Action func, System.String funcName) { }
        // RVA: 0x0A9B2534  token: 0x600020B
        private System.Void _ClearTimer() { }
        // RVA: 0x0A9B2964  token: 0x600020C
        private System.Void _UpdateProperty(Beyond.Rendering.RuntimeVATAnimationClip runtimeVatAnimationClip) { }
        // RVA: 0x0A9B2A88  token: 0x600020D
        public System.Void .ctor() { }
        // RVA: 0x0A9B2A60  token: 0x600020E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200006B  // size: 0x90
    public class FactoryVAT : UnityEngine.MonoBehaviour
    {
        // Fields
        public Beyond.Rendering.RuntimeVATData data;  // 0x18
        public System.Single speed;  // 0x20
        public System.Single passTime;  // 0x24
        public System.Int32 lastFrame;  // 0x28
        public System.Int32 curFrame;  // 0x2c
        public System.Boolean preview;  // 0x30
        private UnityEngine.MaterialPropertyBlock m_materialPropertyBlock;  // 0x38
        private System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.List<Beyond.Rendering.RuntimeVATAnimationClip>> m_animDic;  // 0x40
        private System.Collections.Generic.List<Beyond.Rendering.RuntimeVATAnimationClip> m_curClips;  // 0x48
        private System.Int32 m_curIndex;  // 0x50
        private System.Action m_callBack;  // 0x58
        private System.Boolean m_hasNextAnimToPlay;  // 0x60
        private System.String m_nextAnimName;  // 0x68
        private System.Action m_nextAnimCallBack;  // 0x70
        private static readonly System.Int32 S_FACTORY_VAT_FRAME;  // static @ 0x0
        private static readonly System.Int32 S_FACTORY_VAT_LAST_FRAME;  // static @ 0x4
        private System.Collections.Generic.List<UnityEngine.Renderer> m_allRenderers;  // 0x78
        private System.Collections.Generic.List<UnityEngine.Renderer> m_dynamicRenders;  // 0x80
        private System.Collections.Generic.List<UnityEngine.Renderer> m_staticRenders;  // 0x88

        // Methods
        // RVA: 0x0A9B6058  token: 0x600020F
        private System.Void Start() { }
        // RVA: 0x0A9B6008  token: 0x6000210
        public System.Void Reset() { }
        // RVA: 0x0A9B691C  token: 0x6000211
        private System.Void _UpdateAnim() { }
        // RVA: 0x0A9B5EFC  token: 0x6000212
        public System.Void OnUpdate() { }
        // RVA: 0x0A9B5F4C  token: 0x6000213
        public System.Void Play(System.String name, System.Action callBack, System.Boolean whenComplete) { }
        // RVA: 0x0A9B61DC  token: 0x6000214
        private System.Void _DoPlay(System.String name, System.Action callBack) { }
        // RVA: 0x0A9B64D4  token: 0x6000215
        private System.Void _InitRenderer() { }
        // RVA: 0x0A9B62C8  token: 0x6000216
        private System.Void _InitRenderer_MeshRender(UnityEngine.LODGroup lodGroup) { }
        // RVA: 0x0A9B68CC  token: 0x6000217
        private System.Void _SetParamsToRenderer() { }
        // RVA: 0x0A9B67D8  token: 0x6000218
        private System.Void _SetParamsToRenderer_MeshRender() { }
        // RVA: 0x0A9B6788  token: 0x6000219
        private System.Void _ReSetParamsToRenderer() { }
        // RVA: 0x0A9B6590  token: 0x600021A
        private System.Void _ReSetParamsToRenderer_MeshRender() { }
        // RVA: 0x0A9B6B20  token: 0x600021B
        public System.Void .ctor() { }
        // RVA: 0x0A9B6AD8  token: 0x600021C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200006C  // size: 0x30
    public class RuntimeVATAnimationClip
    {
        // Fields
        public System.String stateName;  // 0x10
        public System.String name;  // 0x18
        public System.Boolean loop;  // 0x20
        public System.Int32 startFrame;  // 0x24
        public System.Int32 endFrame;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x600021D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200006D  // size: 0x38
    public class RuntimeVATData
    {
        // Fields
        public UnityEngine.Texture vatMap;  // 0x10
        public System.Boolean fullRigidBody;  // 0x18
        public System.Collections.Generic.List<UnityEngine.Matrix4x4> bonePoses;  // 0x20
        public System.Single frameRate;  // 0x28
        public System.Collections.Generic.List<Beyond.Rendering.RuntimeVATAnimationClip> animationClips;  // 0x30

        // Methods
        // RVA: 0x0A9B8640  token: 0x600021E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200006E  // size: 0x10
    public abstract class VATPropertySetter
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600021F
        public virtual System.Void Init(UnityEngine.GameObject gameObject) { }
        // RVA: -1  // abstract  token: 0x6000220
        public virtual System.Void SetFloat(System.Int32 id, System.Single value) { }
        // RVA: -1  // abstract  token: 0x6000221
        public virtual System.Void SetVector(System.Int32 id, UnityEngine.Vector4 value) { }
        // RVA: 0x041E1670  token: 0x6000222
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200006F  // size: 0x20
    public class VATRendererPropertySetter : Beyond.Rendering.VATPropertySetter
    {
        // Fields
        private System.Collections.Generic.List<UnityEngine.Renderer> m_allRenderers;  // 0x10
        private System.Collections.Generic.List<UnityEngine.Material> m_allMaterials;  // 0x18

        // Methods
        // RVA: 0x0A9B87F8  token: 0x6000223
        public virtual System.Void Init(UnityEngine.GameObject gameObject) { }
        // RVA: 0x0A9B89F0  token: 0x6000224
        public virtual System.Void SetFloat(System.Int32 id, System.Single value) { }
        // RVA: 0x0A9B8B38  token: 0x6000225
        public virtual System.Void SetVector(System.Int32 id, UnityEngine.Vector4 value) { }
        // RVA: 0x041E1670  token: 0x6000226
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000070  // size: 0x18
    public class VATEntityRendererHelperPropertySetter : Beyond.Rendering.VATPropertySetter
    {
        // Fields
        private Beyond.Rendering.IRendererHelper m_rendererHelper;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000227
        public System.Void .ctor(Beyond.Rendering.IRendererHelper rendererHelper) { }
        // RVA: 0x0A9B8690  token: 0x6000228
        public virtual System.Void Init(UnityEngine.GameObject gameObject) { }
        // RVA: 0x0A9B86E4  token: 0x6000229
        public virtual System.Void SetFloat(System.Int32 id, System.Single value) { }
        // RVA: 0x0A9B8764  token: 0x600022A
        public virtual System.Void SetVector(System.Int32 id, UnityEngine.Vector4 value) { }

    }

    // TypeToken: 0x2000071  // size: 0x18
    public class PsoCreateUploadHelper
    {
        // Fields
        protected static System.Int32 PSO_UPLOAD_INFO_VERSION;  // const
        protected static System.Int32 MAX_UPLOAD_COUNT_PER_LOOP;  // const
        protected static System.Int32 MAX_UPLOAD_COUNT_PER_LOOP_LARGE;  // const
        protected static System.Int32 MAX_UPLOAD_SIZE_PER_LOOP;  // const
        protected static System.Int32 MAX_UPLOAD_SIZE_PER_LOOP_LARGE;  // const
        protected static System.Single TICK_INTERVAL;  // const
        protected static System.Int32 TICK_UPLOAD_INTERVAL;  // const
        protected static System.Single CREATE_TIME_SMALL_DROP;  // const
        protected static Beyond.Rendering.PsoCreateUploadHelper s_instance;  // static @ 0x0
        public static System.Int32 s_enabled;  // static @ 0x8
        protected static System.Int32 s_tickCount;  // static @ 0xc
        public static System.Boolean s_skipUploadFile;  // static @ 0x10
        public static System.Boolean s_isShaderWarming;  // static @ 0x11
        public static System.Boolean s_enableTimeLimit;  // static @ 0x12
        private static readonly System.Action<System.Boolean,System.String,System.String> s_uploadFileCallback;  // static @ 0x18
        private static System.String s_persistentDataPath;  // static @ 0x20
        private static System.Collections.Concurrent.ConcurrentQueue<System.ValueTuple<System.String,System.Single,System.Int32,System.Boolean,System.String>> s_concurrentQueue;  // static @ 0x28
        private Beyond.TickFunction m_timerId;  // 0x10
        private static System.String URL;  // const
        private static System.String TOKEN;  // const
        private static System.Threading.SynchronizationContext s_mainSyncContext;  // static @ 0x30
        private static System.Int32 s_mainThreadId;  // static @ 0x38

        // Properties
        Beyond.Rendering.PsoCreateUploadHelper instance { get; /* RVA: 0x04CEC1A0 */ }
        System.String persistentDataPath { get; /* RVA: 0x03828780 */ }

        // Methods
        // RVA: 0x04B83810  token: 0x600022C
        private static System.Void .cctor() { }
        // RVA: 0x04848BE0  token: 0x600022E
        public static System.Void Initialize() { }
        // RVA: 0x03829F90  token: 0x600022F
        public System.Void Init() { }
        // RVA: 0x03F84BC0  token: 0x6000230
        private System.Void _SceneLoadStartHandler(Beyond.EventData<System.String,System.String>& args) { }
        // RVA: 0x0A9B7E34  token: 0x6000231
        public System.Void Dispose() { }
        // RVA: 0x03F84D60  token: 0x6000232
        public static System.Boolean IsPsoRecordEnabled() { }
        // RVA: 0x0A9B7DC4  token: 0x6000233
        public static System.Void BeforeWarmUpBegin() { }
        // RVA: 0x0A9B7D48  token: 0x6000234
        public static System.Void AfterWarmUpFinish() { }
        // RVA: 0x03F84C20  token: 0x6000235
        private static System.Void _Tick(System.Single deltaTime) { }
        // RVA: 0x03EBB900  token: 0x6000236
        private static System.Void _PsoUploadProcessOneCallBack(System.String infoJson, UnityEngine.HGPsoExtInfo info) { }
        // RVA: 0x0A9B8350  token: 0x6000237
        private static System.String _PsoUpdateGameInfoCallBack() { }
        // RVA: 0x03F84D20  token: 0x6000238
        private static System.Void _UploadPipelineCreatedInfo(System.Boolean timeLimit) { }
        // RVA: 0x03F851E0  token: 0x6000239
        private static System.Void _CreateAndUploadFinalJson(System.Boolean timeLimit) { }
        // RVA: 0x0A9B7F90  token: 0x600023A
        private static System.Void _CreateAndUploadFileToServer(System.String path, System.String serverFileName, Beyond.Rendering.PsoCreateUploadHelper.PsoInfoWrapper psoInfoWrapper) { }
        // RVA: 0x0A9B84C8  token: 0x600023B
        private static System.Void _UploadFile(System.String urlPath, System.String rawFilePath, System.String serverFileNameOverride, System.Action<System.Boolean,System.String,System.String> callback) { }
        // RVA: 0x0A9B83BC  token: 0x600023C
        private static System.Void _UploadFileToServer(System.String urlPath, System.String rawFilePath, System.String serverFileNameOverride, System.Action<System.Boolean,System.String,System.String> callback) { }
        // RVA: 0x0A9B7F28  token: 0x600023D
        private static System.Void _Callback(System.Boolean success, System.String msg, System.String url) { }
        // RVA: 0x041E1670  token: 0x600023E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000079  // size: 0xB8
    public class VFXSludgeBaker
    {
        // Fields
        public UnityEngine.Splines.SplineContainer splineContainer;  // 0x10
        public UnityEngine.Transform rootTransform;  // 0x18
        public System.Single thickness;  // 0x20
        public System.Single density;  // 0x24
        public System.Single maxHeight;  // 0x28
        public System.Single blurIntensity;  // 0x2c
        public System.Single gridHeightOffset;  // 0x30
        public System.Single topLimit;  // 0x34
        public System.Single bottomLimit;  // 0x38
        public UnityEngine.Texture2D bakedTexture1;  // 0x40
        public UnityEngine.Bounds localBounds;  // 0x48
        public System.Single maxHighPowerZoneSignedDistance;  // 0x60
        public System.Single maxEdgeToHighPowerZoneSignedDistance;  // 0x64
        private System.Collections.Generic.List<UnityEngine.Vector3> m_localPolygon;  // 0x68
        private System.Int32 m_xCount;  // 0x70
        private System.Int32 m_zCount;  // 0x74
        private System.Single[,] m_raycastHeights;  // 0x78
        private System.Single[,] m_topHeights;  // 0x80
        private UnityEngine.Vector3[,] m_normals;  // 0x88
        private System.Single[,] m_edgeSignedDistance;  // 0x90
        private System.Single[,] m_highPowerZoneSignedDistance;  // 0x98
        private System.Boolean[,] m_inPolygon;  // 0xa0
        private System.Boolean[,] m_inHighPowerZone;  // 0xa8
        private static System.Single EXTEND_SIZE;  // const
        public System.Single rayCastPosY;  // 0xb0

        // Methods
        // RVA: 0x0A9BBB74  token: 0x600024C
        public System.Void .ctor(UnityEngine.Splines.SplineContainer splineContainer, UnityEngine.Transform target, System.Single density, System.Single thickness, System.Single maxHeight, System.Single blurIntensity, System.Single gridHeightOffset) { }
        // RVA: 0x0A9B8CA0  token: 0x600024D
        public System.Void Bake() { }
        // RVA: 0x0A9BA178  token: 0x600024E
        private System.Void _CalculatePolygonAndBounds() { }
        // RVA: 0x0A9B8D38  token: 0x600024F
        public UnityEngine.Vector2Int GetLocalIndex(UnityEngine.Vector3 localPosition) { }
        // RVA: 0x0A9B8E1C  token: 0x6000250
        public UnityEngine.Vector3 GetLocalPosition(UnityEngine.Vector2Int index) { }
        // RVA: 0x0A9BB350  token: 0x6000251
        private System.Void _RaycastSceneObjectsHeight() { }
        // RVA: 0x0A9B9C8C  token: 0x6000252
        private System.Void _CalculateInPolygon() { }
        // RVA: 0x0A9B98DC  token: 0x6000253
        private System.Void _CalculateInHighPowerZone() { }
        // RVA: 0x0A9BAB84  token: 0x6000254
        private static System.Single[] _CreateGaussianKernel(System.Int32 size, System.Single sigma) { }
        // RVA: 0x0A9BA978  token: 0x6000255
        private static System.Single[,] _Convolution(System.Single[,] src, System.Single[] kernel, UnityEngine.Vector2Int offsetDir) { }
        // RVA: 0x0A9B8F40  token: 0x6000256
        private System.Void _BlurTopHeight() { }
        // RVA: 0x0A9BA49C  token: 0x6000257
        private System.Void _CalculateThicknessAndTopHeight() { }
        // RVA: 0x0A9B9E70  token: 0x6000258
        private System.Void _CalculateNormals() { }
        // RVA: 0x0A9BAE28  token: 0x6000259
        private static System.Void _ForAround(System.Int32 ix, System.Int32 iz, System.Int32 xCount, System.Int32 zCount, System.Action<System.Int32,System.Int32> callback) { }
        // RVA: 0x0A9BAF90  token: 0x600025A
        private System.Int32 _GetHash(System.Int32 ix, System.Int32 iz) { }
        // RVA: 0x0A9BAF24  token: 0x600025B
        private static System.Int32 _GetHash(System.Int32 ix, System.Int32 iz, System.Int32 xCount) { }
        // RVA: 0x0A9B9104  token: 0x600025C
        private static System.Void _CalculateEdgeSignedDistance(System.Single[,] sdf) { }
        // RVA: 0x0A9B9418  token: 0x600025D
        private System.Void _CalculateEdgeSignedDistance() { }
        // RVA: 0x0A9BB000  token: 0x600025E
        private UnityEngine.Color _GetTextureColor1(System.Int32 ix, System.Int32 iz) { }
        // RVA: 0x0A9BB200  token: 0x600025F
        private UnityEngine.Color _GetTextureColor2(System.Int32 ix, System.Int32 iz) { }
        // RVA: 0x0A9BACBC  token: 0x6000260
        private System.Void _CreateTexture() { }

    }

    // TypeToken: 0x200007C  // size: 0x10
    public class VFXSludgeNavUtils
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000265
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000086  // size: 0x10
    public static class VFXSludgeUtils
    {
        // Methods
        // RVA: 0x0A9C689C  token: 0x600027C
        public static System.Collections.Generic.List<UnityEngine.Vector3> GetPolygonOffSpline(UnityEngine.Splines.SplineContainer splineContainer, System.Single splitDistance, System.Int32 splineIndex) { }
        // RVA: 0x0A9C6350  token: 0x600027D
        public static System.Void GetPolygonOffSplineWithSimplify(UnityEngine.Splines.SplineContainer container, System.Collections.Generic.List<UnityEngine.Vector2>& points, System.Int32 splineIndex, System.Boolean closed, System.Boolean isDebug, System.Boolean isConvexHull) { }
        // RVA: 0x0A9C6010  token: 0x600027E
        public static System.Collections.Generic.List<UnityEngine.Vector3> GetPolygonOffByPolyLine(System.Collections.Generic.List<UnityEngine.Vector2> polylinePoints, UnityEngine.Transform transform, System.Single splitDistance) { }
        // RVA: 0x0A9C5E9C  token: 0x600027F
        public static System.Collections.Generic.List<UnityEngine.Vector3> CalculatePointPolygonHorizonRayCross(System.Collections.Generic.List<UnityEngine.Vector3> polygon, UnityEngine.Vector3 point) { }

    }

    // TypeToken: 0x2000087  // size: 0x18
    public class HGTerrain : UnityEngine.MonoBehaviour
    {
        // Methods
        // RVA: 0x05393520  token: 0x6000280
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000088  // size: 0x80
    public sealed class ToggleFunc : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068FE0AC  token: 0x6000281
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x6000282
        public virtual System.Void Invoke(Beyond.Rendering.HGTerrainHelper terrainHelper) { }
        // RVA: 0x047FDD70  token: 0x6000283
        public virtual System.IAsyncResult BeginInvoke(Beyond.Rendering.HGTerrainHelper terrainHelper, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000284
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000089  // size: 0x18
    public class HGTerrainHelper : UnityEngine.MonoBehaviour
    {
        // Methods
        // RVA: 0x05393520  token: 0x6000285
        public System.Void .ctor() { }

    }

}

namespace Beyond.Rendering.ECS
{

    // TypeToken: 0x200008E  // size: 0x70
    public class HGAddedMaterialController
    {
        // Fields
        public static System.Int32 MAX_ACTIVE_ADDED_MATERIAL;  // const
        private UnityEngine.HGMeshRenderer m_sourceRenderer;  // 0x10
        private UnityEngine.HGMeshRendererData m_sourceData;  // 0x18
        private UnityEngine.Transform m_overlayParent;  // 0x20
        private System.Int32 m_sourceMaterialSlotCount;  // 0x28
        private readonly System.Collections.Generic.Dictionary<System.Int32,Beyond.Rendering.ECS.HGAddedMaterialController.OverlayLayer> m_activeLayers;  // 0x30
        private readonly System.Collections.Generic.Stack<Beyond.Rendering.ECS.HGAddedMaterialController.OverlayLayer> m_pool;  // 0x38
        private System.Int32 m_nextHandle;  // 0x40
        private System.Int32 m_layerCreateCounter;  // 0x44
        private readonly System.Collections.Generic.List<System.Int32> m_handlesScratch;  // 0x48
        public System.Action<UnityEngine.HGMeshRenderer> onLayerActivated;  // 0x50
        private System.Collections.Generic.Dictionary<System.Int32,System.Single> m_addictiveFloatProperties;  // 0x58
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Vector4> m_addictiveVectorProperties;  // 0x60
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Texture> m_addictiveTextureProperties;  // 0x68

        // Properties
        System.Int32 activeLayerCount { get; /* RVA: 0x0A9BD844 */ }

        // Methods
        // RVA: 0x0A9BC61C  token: 0x6000299
        public System.Void Init(UnityEngine.HGMeshRenderer sourceRenderer, UnityEngine.HGMeshRendererData sourceData, UnityEngine.Transform overlayParent, System.Int32 sourceMaterialSlotCount) { }
        // RVA: 0x0A9BCABC  token: 0x600029A
        public System.Int32 RequireAddMaterial(UnityEngine.Material material) { }
        // RVA: 0x0A9BCD88  token: 0x600029B
        public System.Void RequireRemoveMaterial(System.Int32 handle) { }
        // RVA: 0x0A9BC144  token: 0x600029C
        public System.Int32 GetActiveOverlayRenderers(System.Collections.Generic.List<UnityEngine.HGMeshRenderer> result) { }
        // RVA: 0x0A9BC324  token: 0x600029E
        public System.Boolean GetAddedMaterialsOfCustomizeProperties(System.Int32 handle, System.Collections.Generic.List<UnityEngine.Material> result) { }
        // RVA: 0x0A9BD174  token: 0x600029F
        public System.Void SetAddictiveFloatProperty(System.Int32 propertyId, System.Single value) { }
        // RVA: 0x0A9BC41C  token: 0x60002A0
        public System.Single GetAddictiveFloatProperty(System.Int32 propertyId, System.Single defaultValue) { }
        // RVA: 0x0A9BD2FC  token: 0x60002A1
        public System.Void SetAddictiveVectorProperty(System.Int32 propertyId, UnityEngine.Vector4 value) { }
        // RVA: 0x0A9BC554  token: 0x60002A2
        public UnityEngine.Vector4 GetAddictiveVectorProperty(System.Int32 propertyId, UnityEngine.Vector4 defaultValue) { }
        // RVA: 0x0A9BD238  token: 0x60002A3
        public System.Void SetAddictiveTextureProperty(System.Int32 propertyId, UnityEngine.Texture value) { }
        // RVA: 0x0A9BC4B8  token: 0x60002A4
        public UnityEngine.Texture GetAddictiveTextureProperty(System.Int32 propertyId, UnityEngine.Texture defaultValue) { }
        // RVA: 0x0A9BD3D4  token: 0x60002A5
        private Beyond.Rendering.ECS.HGAddedMaterialController.OverlayLayer _CreateNewLayer() { }
        // RVA: 0x0A9BCF44  token: 0x60002A6
        public System.Void Reset() { }
        // RVA: 0x0A9BC6D0  token: 0x60002A7
        public System.Void Release() { }
        // RVA: 0x0A9BD634  token: 0x60002A8
        private static System.Void _DestroyLayer(Beyond.Rendering.ECS.HGAddedMaterialController.OverlayLayer layer) { }
        // RVA: 0x0A9BD784  token: 0x60002A9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000090  // size: 0x48
    public class HGMaterialCommitter
    {
        // Fields
        private UnityEngine.HGMeshRenderer m_hgRenderer;  // 0x10
        private UnityEngine.HGMeshRendererData m_sourceData;  // 0x18
        private UnityEngine.HGMeshRendererData m_clonedData;  // 0x20
        private Beyond.Rendering.ECS.HGMaterialCommitter.MaterialsProvider m_materialsProvider;  // 0x28
        private System.Action m_onDirty;  // 0x30
        private System.Action m_onCommitted;  // 0x38
        private System.Boolean m_dirtyMaterialsArray;  // 0x40

        // Properties
        UnityEngine.HGMeshRendererData clonedData { get; /* RVA: 0x0A9BDEB0 */ }

        // Methods
        // RVA: 0x0A9BD8EC  token: 0x60002AC
        public System.Void Init(UnityEngine.HGMeshRenderer renderer, UnityEngine.HGMeshRendererData sourceData, Beyond.Rendering.ECS.HGMaterialCommitter.MaterialsProvider materialsProvider, System.Action onDirty, System.Action onCommitted) { }
        // RVA: 0x0A9BDA24  token: 0x60002AD
        public System.Void MarkMaterialsArrayDirty() { }
        // RVA: 0x0A9BD9D8  token: 0x60002AE
        public System.Boolean IsDirty() { }
        // RVA: 0x0A9BDC8C  token: 0x60002AF
        public System.Void TryCommit() { }
        // RVA: 0x0A9BD89C  token: 0x60002B0
        public System.Void ForceCommit() { }
        // RVA: 0x0A9BDCE4  token: 0x60002B1
        private System.Void _Commit() { }
        // RVA: 0x0A9BDBE0  token: 0x60002B2
        public System.Void Reset() { }
        // RVA: 0x0A9BDA84  token: 0x60002B3
        public System.Void Release() { }
        // RVA: 0x041E1670  token: 0x60002B4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000092  // size: 0x70
    public class HGMaterialController
    {
        // Fields
        private static System.String VFX_MATERIAL_POSTFIX;  // const
        private static System.String ALPHA_BLEND_ON_KEYWORD;  // const
        private static System.String DEPTH_ONLY_PASS;  // const
        private static System.String DITHER_KEYWORD;  // const
        private static System.Int32 s_zTestGBuffer;  // static @ 0x0
        private static System.Int32 s_outlineZTest;  // static @ 0x4
        private static System.Int32 s_ditherTransparentAlpha;  // static @ 0x8
        private UnityEngine.HGMeshRenderer m_hgRenderer;  // 0x10
        private UnityEngine.HGMeshRendererData m_sourceData;  // 0x18
        private Beyond.Rendering.ECS.HGMaterialCommitter m_committer;  // 0x20
        private UnityEngine.Material[] m_sourceMaterials;  // 0x28
        private UnityEngine.Material[] m_currentMaterials;  // 0x30
        private System.Collections.Generic.List<UnityEngine.Material> m_instancedMaterials;  // 0x38
        private System.Collections.Generic.Dictionary<UnityEngine.Material,UnityEngine.Material> m_src2dstMaterialDict;  // 0x40
        private System.Collections.Generic.Dictionary<System.Int32,System.Single> m_floatProperties;  // 0x48
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Vector4> m_vectorProperties;  // 0x50
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Texture> m_textureProperties;  // 0x58
        private System.Collections.Generic.Dictionary<System.String,System.Boolean> m_keywordStates;  // 0x60
        private System.Boolean m_depthOnlyPassEnabled;  // 0x68
        private System.Boolean m_depthOnlyPassControlling;  // 0x69

        // Properties
        UnityEngine.Material[] currentMeshMaterials { get; /* RVA: 0x0A9BF764 */ }

        // Methods
        // RVA: 0x0A9BDF00  token: 0x60002BA
        public System.Void Init(UnityEngine.HGMeshRenderer renderer, UnityEngine.HGMeshRendererData sourceData, UnityEngine.Material[] sourceMaterials, Beyond.Rendering.ECS.HGMaterialCommitter committer) { }
        // RVA: 0x0A9BF454  token: 0x60002BB
        private UnityEngine.Material _GetOrCreateInstancedMaterial(System.Int32 slot) { }
        // RVA: 0x0A9BEDCC  token: 0x60002BC
        private System.Void _ApplyAllPropertiesToOneMaterial(UnityEngine.Material mat) { }
        // RVA: 0x0A9BF2B4  token: 0x60002BD
        private System.Void _ApplyDepthOnlyPassToMaterial(UnityEngine.Material mat, System.Boolean enable) { }
        // RVA: 0x0A9BE684  token: 0x60002BE
        public System.Void SetFloatToAllMaterial(System.Int32 nameId, System.Single value) { }
        // RVA: 0x0A9BEBD0  token: 0x60002BF
        public System.Void SetVectorToAllMaterial(System.Int32 nameId, UnityEngine.Vector4 value) { }
        // RVA: 0x0A9BEA8C  token: 0x60002C0
        public System.Void SetTextureToAllMaterial(System.Int32 nameId, UnityEngine.Texture value) { }
        // RVA: 0x0A9BE7FC  token: 0x60002C1
        public System.Void SetKeywordEnableToAllMaterial(System.String keyword, System.Boolean enable) { }
        // RVA: 0x0A9BE5D4  token: 0x60002C2
        public System.Void SetDepthOnlyPassEnableToAllMaterial(System.Boolean enable) { }
        // RVA: 0x0A9BEA14  token: 0x60002C3
        public System.Boolean SetManualDitherEnable(System.Boolean enable) { }
        // RVA: 0x0A9BE990  token: 0x60002C4
        public System.Void SetManualDitherAlphaValue(System.Single value) { }
        // RVA: 0x0A9BE254  token: 0x60002C5
        public System.Void ReplaceAllMaterials(UnityEngine.Material replacement) { }
        // RVA: 0x0A9BE32C  token: 0x60002C6
        public System.Void ReplaceMaterialBySource(UnityEngine.Material srcMat, UnityEngine.Material replacement) { }
        // RVA: 0x0A9BE4C4  token: 0x60002C7
        public System.Void Reset() { }
        // RVA: 0x0A9BE060  token: 0x60002C8
        public System.Void Release() { }
        // RVA: 0x041E1670  token: 0x60002C9
        public System.Void .ctor() { }
        // RVA: 0x0A9BF6FC  token: 0x60002CA
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000093  // size: 0x88
    public class HGRendererStateController
    {
        // Fields
        private UnityEngine.HGMeshRenderer m_hgRenderer;  // 0x10
        private System.Boolean m_oriEnabled;  // 0x18
        private System.Boolean m_warnedNoMeshes;  // 0x19
        private UnityEngine.Bounds[] m_meshLocalBounds;  // 0x20
        private UnityEngine.Matrix4x4 m_cachedL2W;  // 0x28
        private UnityEngine.Bounds m_cachedWorldBounds;  // 0x68
        private System.Boolean m_hasCachedWorldBounds;  // 0x80

        // Properties
        UnityEngine.HGMeshRenderer hgRenderer { get; /* RVA: 0x0A9C0780 */ }

        // Methods
        // RVA: 0x0A9BFB54  token: 0x60002CB
        public System.Void Init(UnityEngine.HGMeshRenderer renderer) { }
        // RVA: 0x0A9BFC60  token: 0x60002CC
        public System.Boolean IsValid() { }
        // RVA: 0x0A9BFE38  token: 0x60002CE
        public System.Void SetVisible(System.Boolean visible) { }
        // RVA: 0x0A9BFD70  token: 0x60002CF
        public System.Void Reset() { }
        // RVA: 0x0A9BF7B4  token: 0x60002D0
        public UnityEngine.Bounds GetBounds() { }
        // RVA: 0x0A9BFC00  token: 0x60002D1
        public System.Void InvalidateMeshCache() { }
        // RVA: 0x0A9BFF20  token: 0x60002D2
        private System.Void _EnsureMeshLocalBoundsCached() { }
        // RVA: 0x0A9C0118  token: 0x60002D3
        private static UnityEngine.Bounds _LocalBoundsToWorld(UnityEngine.Bounds local, UnityEngine.Matrix4x4 l2w) { }
        // RVA: 0x0A9C06B4  token: 0x60002D4
        private System.Void _WarnNoMeshes() { }
        // RVA: 0x0A9BFECC  token: 0x60002D5
        public System.Void StopShadowCasting(System.Boolean stopRayTracing) { }
        // RVA: 0x0A9BFDF4  token: 0x60002D6
        public System.Void RevertShadowCastingMode() { }
        // RVA: 0x0A9BFCC8  token: 0x60002D7
        public System.Void Release() { }
        // RVA: 0x041E1670  token: 0x60002D8
        public System.Void .ctor() { }

    }

}

namespace Beyond.Rendering.LODLightmapping
{

    // TypeToken: 0x200008B  // size: 0x28
    public class LightmapInfoTransfer : UnityEngine.MonoBehaviour
    {
        // Fields
        private UnityEngine.MeshRenderer _lightmappedRenderer;  // 0x18
        private UnityEngine.MeshRenderer m_currentRenderer;  // 0x20

        // Methods
        // RVA: 0x0A9C2CE4  token: 0x6000293
        private System.Void OnEnable() { }
        // RVA: 0x0A9C2C68  token: 0x6000294
        private System.Void Awake() { }
        // RVA: 0x0A9C2D34  token: 0x6000295
        private System.Void _RendererInfoTransfer() { }
        // RVA: 0x05393520  token: 0x6000296
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008C  // size: 0x14
    public sealed struct LodLightmappingMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.LODLightmapping.LodLightmappingMode AllFromLightProbe;  // const
        public static Beyond.Rendering.LODLightmapping.LodLightmappingMode AllFromLOD0;  // const
        public static Beyond.Rendering.LODLightmapping.LodLightmappingMode UseDifferentLightmapAreaForLODs;  // const

    }

    // TypeToken: 0x200008D  // size: 0x30
    public class LODLightmapping : UnityEngine.MonoBehaviour
    {
        // Fields
        public Beyond.Rendering.LODLightmapping.LodLightmappingMode mode;  // 0x18
        public UnityEngine.Vector4 lightmapScaleOffset;  // 0x1c
        public System.Int32 lightmapIndex;  // 0x2c

        // Methods
        // RVA: 0x0A9C28EC  token: 0x6000297
        public System.Void RendererInfoTransfer() { }
        // RVA: 0x04A39E70  token: 0x6000298
        public System.Void .ctor() { }

    }

}

namespace Beyond.Rendering.SplineMovingObjects
{

    // TypeToken: 0x200008A  // size: 0x278
    public class HGSplineMovingObjects : UnityEngine.MonoBehaviour, HG.Rendering.Runtime.IVFXPlayable
    {
        // Fields
        private UnityEngine.HyperGryph.SplineObjectsDesc desc;  // 0x18
        private UnityEngine.HyperGryph.SplineObjectsKnotData[] knotData;  // 0x258
        private System.Boolean fixedPostion;  // 0x260
        private System.Boolean optimizeCurve;  // 0x261
        private System.UInt32 m_instanceHandle;  // 0x264
        private Unity.Collections.NativeArray<UnityEngine.HyperGryph.SplineObjectsKnotData> m_stonesKnotDataNativeArray;  // 0x268

        // Methods
        // RVA: 0x0A9C0C00  token: 0x6000286
        private System.Void _CreateInstance() { }
        // RVA: 0x0A9C0E9C  token: 0x6000287
        private System.Void _ExtractSplineData() { }
        // RVA: 0x0A9C0E34  token: 0x6000288
        private System.Void _DestroyInstance() { }
        // RVA: 0x0A9C10F8  token: 0x6000289
        private System.Void _Release() { }
        // RVA: 0x0A9C0B48  token: 0x600028A
        private System.Void Update() { }
        // RVA: 0x0A9C0A04  token: 0x600028B
        private System.Void OnEnable() { }
        // RVA: 0x0A9C0820  token: 0x600028C
        private System.Void OnDisable() { }
        // RVA: 0x0A9C07D0  token: 0x600028D
        private System.Void OnDestroy() { }
        // RVA: 0x0A9C0870  token: 0x600028E
        private System.Void OnDrawGizmosSelected() { }
        // RVA: 0x0A9C0A54  token: 0x600028F
        public System.Void Pause() { }
        // RVA: 0x0A9C0AA8  token: 0x6000290
        public virtual System.Void Play() { }
        // RVA: 0x0A9C0AF8  token: 0x6000291
        public virtual System.Void Stop() { }
        // RVA: 0x0A9C1174  token: 0x6000292
        public System.Void .ctor() { }

    }

}

namespace Beyond.SourceGenerator
{

    // TypeToken: 0x2000006  // size: 0x10
    public static class AnimatorBlackboardUtility
    {
        // Fields
        private static System.Boolean s_enableFastPathBuffer;  // static @ 0x0

        // Methods
        // RVA: -1  // generic def  token: 0x6000016
        private static System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity) { }

    }

    // TypeToken: 0x2000007  // size: 0x10
    public class AnimatorBlackboardAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000017
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000008  // size: 0x18
    public class ECSComponentAttribute : System.Attribute
    {
        // Fields
        public System.Runtime.InteropServices.LayoutKind layoutKind;  // 0x10
        public System.Boolean isTag;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x6000018
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
        // RVA: 0x041E1670  token: 0x6000019
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x10
    public class CameraControlConfigAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600001A
        public System.Void .ctor() { }

    }

}

namespace HG.Rendering.Runtime
{

    // TypeToken: 0x2000003  // size: 0x170
    public class ContainerWaterManager : UnityEngine.MonoBehaviour
    {
        // Fields
        private static readonly System.Int32 DISPLACEMENT_TEX;  // static @ 0x0
        private static readonly System.Int32 DISPLACEMENT_TEX_ST;  // static @ 0x4
        private static readonly System.Int32 WATER_MENISCUS_WIDTH;  // static @ 0x8
        private static readonly System.Int32 ICE_POSITION;  // static @ 0xc
        public UnityEngine.Material waveMaterial;  // 0x18
        public UnityEngine.Texture2D waveTexture;  // 0x20
        public UnityEngine.Transform iceBall;  // 0x28
        public UnityEngine.Renderer liquidRenderer;  // 0x30
        private System.Single[][] m_waveN;  // 0x38
        private System.Single[][] m_waveNm1;  // 0x40
        private System.Single[][] m_waveNp1;  // 0x48
        private readonly System.Single m_cylinderRadius;  // 0x50
        private System.Single _dx;  // 0x54
        private System.Int32 m_nx;  // 0x58
        private System.Int32 m_ny;  // 0x5c
        public System.Single CFL;  // 0x60
        public System.Single c;  // 0x64
        private System.Single m_dt;  // 0x68
        private System.Single m_t;  // 0x6c
        public System.Boolean reflectiveBoundary;  // 0x70
        private System.Single _waveMultiplier;  // 0x74
        private System.Single _pulseFrequency;  // 0x78
        private System.Single _pulseMagnitude;  // 0x7c
        private System.Single _elasticity;  // 0x80
        private System.Single _viscosity;  // 0x84
        private System.Single _surfaceTension;  // 0x88
        private System.Boolean _adaptiveQuality;  // 0x8c
        private System.Single _targetFrameTime;  // 0x90
        private System.Single m_lastFrameTime;  // 0x94
        private UnityEngine.UI.RawImage _debugDisplay;  // 0x98
        private System.Int32 _updateFrequency;  // 0xa0
        private System.Int32 m_frameCounter;  // 0xa4
        private System.Single m_currentYRotationSpeed;  // 0xa8
        private System.Boolean m_needsTextureUpdate;  // 0xac
        private UnityEngine.Vector3 m_lastIceBallPosition;  // 0xb0
        private UnityEngine.Quaternion m_lastIceBallRotation;  // 0xbc
        private readonly System.Single m_iceBallRadius;  // 0xcc
        private System.Single _rippleUpdateInterval;  // 0xd0
        private System.Single _rippleBaseStrength;  // 0xd4
        private System.Single _rippleRadius;  // 0xd8
        private System.Single _rippleWaveLength;  // 0xdc
        private System.Single _rippleSpeed;  // 0xe0
        private System.Single _distanceDecayPower;  // 0xe4
        private System.Single _meltingUniformity;  // 0xe8
        private System.Single _meltingVariation;  // 0xec
        private System.Single m_lastRippleUpdateTime;  // 0xf0
        private System.Single m_ripplePhase;  // 0xf4
        private System.Single _rotationWaveMultiplier;  // 0xf8
        private System.Single _rotationSensitivity;  // 0xfc
        private System.Boolean[,] m_cylinderMask;  // 0x100
        private System.Collections.Generic.List<UnityEngine.Vector2Int> m_activePoints;  // 0x108
        private UnityEngine.Color32[] m_cachedPixels;  // 0x110
        private System.Single _normalMeniscusWidth;  // 0x118
        private System.Single _vortexDetectionThreshold;  // 0x11c
        private System.Single _vortexTransitionTime;  // 0x120
        private System.Boolean m_isVortexActive;  // 0x124
        private System.Single m_vortexTransitionProgress;  // 0x128
        private System.Single m_lastVortexCheckTime;  // 0x12c
        private System.Single m_lastYAxisStopTime;  // 0x130
        private System.Boolean m_wasYAxisRotating;  // 0x134
        private readonly System.Single m_yAxisStopDelay;  // 0x138
        private static System.Single VORTEX_CHECK_INTERVAL;  // const
        private System.Single _movementDistanceThreshold;  // 0x13c
        private System.Single _movementRecoveryDelay;  // 0x140
        private System.Boolean m_isMovementActive;  // 0x144
        private System.Single m_lastMovementStopTime;  // 0x148
        private System.Boolean m_wasMoving;  // 0x14c
        private System.Single m_currentMeniscusWidth;  // 0x150
        private System.Boolean m_isMeniscusTransitioning;  // 0x154
        private System.Single m_meniscusTransitionStartTime;  // 0x158
        private System.Single m_meniscusTransitionStartValue;  // 0x15c
        private UnityEngine.Vector2Int m_tempTexturePos;  // 0x160
        private UnityEngine.Vector2Int m_tempNearestPoint;  // 0x168

        // Properties
        System.Single dy { get; /* RVA: 0x0A9AB5A4 */ }

        // Methods
        // RVA: 0x0A9A84C0  token: 0x6000004
        private System.Void Start() { }
        // RVA: 0x0A9A8FE8  token: 0x6000005
        private System.Void _InitializeWaveSystem() { }
        // RVA: 0x0A9A956C  token: 0x6000006
        private System.Void _PrecomputeCylinderMask() { }
        // RVA: 0x0A9A975C  token: 0x6000007
        private System.Void _UnifiedWaveStep(System.Boolean isEditorUpdate) { }
        // RVA: 0x0A9AB32C  token: 0x6000008
        private System.Boolean _WorldToTextureCoords(UnityEngine.Vector3 worldPos, UnityEngine.Vector2Int& texturePos) { }
        // RVA: 0x0A9A945C  token: 0x6000009
        private System.Boolean _IsInsideCylinder(System.Int32 i, System.Int32 j) { }
        // RVA: 0x0A9A94F0  token: 0x600000A
        private System.Boolean _IsValidPosition(System.Int32 x, System.Int32 y) { }
        // RVA: 0x0A9A894C  token: 0x600000B
        private System.Void _ApplyMatrixToTexture(System.Single[][] state, UnityEngine.Texture2D& tex, System.Single waveMultiplier) { }
        // RVA: 0x0A9A8DCC  token: 0x600000C
        private System.Void _ApplyReflectiveBoundary() { }
        // RVA: 0x0A9A87F8  token: 0x600000D
        private System.Void _ApplyAbsorptiveBoundary() { }
        // RVA: 0x0A9A8E80  token: 0x600000E
        private UnityEngine.Vector2Int _FindNearestInsidePoint(System.Int32 x, System.Int32 y) { }
        // RVA: 0x0A9AAFB4  token: 0x600000F
        private System.Void _UpdateVortexState(System.Boolean isEditorUpdate) { }
        // RVA: 0x0A9A8334  token: 0x6000010
        public System.Void ResetWaveTexture() { }
        // RVA: 0x0A9A8544  token: 0x6000011
        private System.Void Update() { }
        // RVA: 0x0A9AB4A0  token: 0x6000012
        public System.Void .ctor() { }
        // RVA: 0x0A9AB41C  token: 0x6000013
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000004  // size: 0x18
    public class VFXLockTransform : UnityEngine.MonoBehaviour
    {
        // Methods
        // RVA: 0x05393520  token: 0x6000014
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000005  // size: 0x10
    public class HGConstantBufferLayoutAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000015
        public System.Void .ctor() { }

    }

}

namespace IFix
{

    // TypeToken: 0x2000096  // size: 0x28
    public class ILFixDynamicMethodWrapper
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10
        private System.Int32 methodId;  // 0x18
        private System.Object anonObj;  // 0x20
        public static IFix.ILFixDynamicMethodWrapper[] wrapperArray;  // static @ 0x0

        // Methods
        // RVA: 0x032B2C30  token: 0x60002D9
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj) { }
        // RVA: 0x066C0730  token: 0x60002DA
        public System.Single __Gen_Wrap_0(System.Object P0) { }
        // RVA: 0x063633B4  token: 0x60002DB
        public System.Void __Gen_Wrap_1(System.Object P0) { }
        // RVA: 0x09CA9454  token: 0x60002DC
        public System.Boolean __Gen_Wrap_2(System.Object P0, System.Int32 P1, System.Int32 P2) { }
        // RVA: 0x0A9C2474  token: 0x60002DD
        public UnityEngine.Vector2Int __Gen_Wrap_3(System.Object P0, System.Int32 P1, System.Int32 P2) { }
        // RVA: 0x0A9C2568  token: 0x60002DE
        public System.Boolean __Gen_Wrap_4(System.Object P0, UnityEngine.Vector3 P1, UnityEngine.Vector2Int& P2) { }
        // RVA: 0x066C2B40  token: 0x60002DF
        public System.Void __Gen_Wrap_5(System.Object P0, System.Boolean P1) { }
        // RVA: 0x0A9C26B0  token: 0x60002E0
        public System.Void __Gen_Wrap_6(System.Object P0, System.Object P1, UnityEngine.Texture2D& P2, System.Single P3) { }
        // RVA: 0x066C288C  token: 0x60002E1
        public System.Void __Gen_Wrap_7(System.Object P0, System.Single P1) { }
        // RVA: 0x066F3838  token: 0x60002E2
        public System.Void __Gen_Wrap_8(System.Object P0, System.Int32 P1) { }
        // RVA: 0x0A9C27D4  token: 0x60002E3
        public System.Void __Gen_Wrap_9(System.Object P0, UnityEngine.Vector4 P1, UnityEngine.Vector4 P2) { }
        // RVA: 0x0A9C1378  token: 0x60002E4
        public System.Void __Gen_Wrap_10(System.Object P0, UnityEngine.Vector4 P1) { }
        // RVA: 0x0A9C1470  token: 0x60002E5
        public System.Void __Gen_Wrap_11(System.Object P0, System.Single P1, System.Single P2, System.Single P3) { }
        // RVA: 0x0A9C157C  token: 0x60002E6
        public System.Void __Gen_Wrap_12(System.Object P0, System.Single P1, System.Single P2) { }
        // RVA: 0x09CD8CC0  token: 0x60002E7
        public System.Void __Gen_Wrap_13(System.Object P0, System.Single P1, System.Single P2, System.Single P3, System.Single P4) { }
        // RVA: 0x06366CB0  token: 0x60002E8
        public System.String __Gen_Wrap_14(System.Object P0) { }
        // RVA: 0x063625A4  token: 0x60002E9
        public System.Void __Gen_Wrap_15() { }
        // RVA: 0x0A9C1664  token: 0x60002EA
        public System.String __Gen_Wrap_16(Beyond.Resource.RootCategory P0) { }
        // RVA: 0x0A9C1724  token: 0x60002EB
        public System.String __Gen_Wrap_17(Beyond.Resource.RootCategory P0, System.Boolean P1) { }
        // RVA: 0x0A9C1800  token: 0x60002EC
        public System.String __Gen_Wrap_18() { }
        // RVA: 0x063623A8  token: 0x60002ED
        public System.Boolean __Gen_Wrap_19() { }
        // RVA: 0x0A9C18B0  token: 0x60002EE
        public System.String __Gen_Wrap_20(UnityEngine.RuntimePlatform P0, UnityEngine.Rendering.GraphicsDeviceType P1, System.Int32 P2, System.Object P3) { }
        // RVA: 0x0A9C19B8  token: 0x60002EF
        public System.String __Gen_Wrap_21(UnityEngine.RuntimePlatform P0, UnityEngine.Rendering.GraphicsDeviceType P1, System.Int32 P2, System.Object P3, System.Object P4) { }
        // RVA: 0x0635FE78  token: 0x60002F0
        public System.Int64 __Gen_Wrap_22() { }
        // RVA: 0x0A9C1AD0  token: 0x60002F1
        public System.Boolean __Gen_Wrap_23(System.Object P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4, System.Object P5) { }
        // RVA: 0x0A9C1C08  token: 0x60002F2
        public System.Boolean __Gen_Wrap_24(System.Object P0, System.String& P1) { }
        // RVA: 0x063632E4  token: 0x60002F3
        public System.Boolean __Gen_Wrap_25(System.Object P0) { }
        // RVA: 0x06301ED4  token: 0x60002F4
        public System.Void __Gen_Wrap_26(Beyond.Rendering.ShaderWarmupManager.WarmUpReason P0) { }
        // RVA: 0x0A9C1D24  token: 0x60002F5
        public System.String __Gen_Wrap_27(System.IntPtr P0, Beyond.Rendering.WindowsGpuDriverVersion.SP_DEVINFO_DATA& P1, System.UInt32 P2) { }
        // RVA: 0x06366BC4  token: 0x60002F6
        public System.Boolean __Gen_Wrap_28(System.Object P0, System.Object P1) { }
        // RVA: 0x0A9C1E84  token: 0x60002F7
        public System.Int32 __Gen_Wrap_29(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x06366900  token: 0x60002F8
        public System.String __Gen_Wrap_30(System.Object P0, System.Object P1) { }
        // RVA: 0x071AD678  token: 0x60002F9
        public System.Int32 __Gen_Wrap_31(Beyond.Rendering.WindowsGpuDriverVersion.Result P0, Beyond.Rendering.WindowsGpuDriverVersion.Result P1) { }
        // RVA: 0x0A9C1F8C  token: 0x60002FA
        public System.Collections.Generic.List<Beyond.Rendering.WindowsGpuDriverVersion.Result> __Gen_Wrap_32() { }
        // RVA: 0x0635FF24  token: 0x60002FB
        public System.Void __Gen_Wrap_33(System.Int32 P0) { }
        // RVA: 0x0A9C203C  token: 0x60002FC
        public Beyond.Rendering.WindowsGpuDriverVersion.Result __Gen_Wrap_34() { }
        // RVA: 0x0A9C20EC  token: 0x60002FD
        public System.Boolean __Gen_Wrap_35(System.Object P0, System.Object P1, System.String& P2) { }
        // RVA: 0x06363D30  token: 0x60002FE
        public System.Boolean __Gen_Wrap_36(System.Boolean P0) { }
        // RVA: 0x0A9C2220  token: 0x60002FF
        public System.Collections.IEnumerator __Gen_Wrap_37() { }
        // RVA: 0x0A9C22BC  token: 0x6000300
        public System.Collections.Generic.List<Beyond.Resource.StringPathHash> __Gen_Wrap_38(System.Boolean P0) { }
        // RVA: 0x0A9C237C  token: 0x6000301
        public System.Collections.IEnumerator __Gen_Wrap_39(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x071AFE44  token: 0x6000302
        public System.Int32 __Gen_Wrap_40(System.Object P0, System.Int32 P1, System.Int32 P2) { }
        // RVA: 0x01282570  token: 0x6000303
        public System.Void __Gen_Wrap_41(System.Object P0, System.Boolean P1, System.Boolean P2) { }
        // RVA: 0x012B9870  token: 0x6000304
        public System.Int32 __Gen_Wrap_42() { }
        // RVA: 0x02E762E0  token: 0x6000305
        public System.Void __Gen_Wrap_43(System.Int32 P0, System.Int32& P1) { }
        // RVA: 0x02E76510  token: 0x6000306
        public System.Collections.IEnumerator __Gen_Wrap_44(System.Object P0, System.Int32 P1, System.Int32 P2, System.Object P3) { }
        // RVA: 0x02E76C70  token: 0x6000307
        public System.Collections.IEnumerator __Gen_Wrap_45(System.Boolean P0) { }
        // RVA: 0x02E77220  token: 0x6000308
        public System.Collections.IEnumerator __Gen_Wrap_46(System.Object P0, System.Int32 P1, System.Int32 P2) { }
        // RVA: 0x02E778C0  token: 0x6000309
        public System.Collections.IEnumerator __Gen_Wrap_47(System.Object P0) { }
        // RVA: 0x02E77F20  token: 0x600030A
        public System.Boolean __Gen_Wrap_48(Beyond.Rendering.SceneStateAreaKey& P0, Beyond.Rendering.SceneStateAreaKey P1) { }
        // RVA: 0x02E78660  token: 0x600030B
        public System.Boolean __Gen_Wrap_49(Beyond.Rendering.SceneStateAreaKey& P0, System.Object P1) { }
        // RVA: 0x02E78D80  token: 0x600030C
        public System.Int32 __Gen_Wrap_50(Beyond.Rendering.SceneStateAreaKey& P0) { }
        // RVA: 0x012B6D00  token: 0x600030D
        public System.Boolean __Gen_Wrap_51(System.Object P0, System.Boolean P1) { }
        // RVA: 0x02E793C0  token: 0x600030E
        public System.Boolean __Gen_Wrap_52(System.Object P0, System.Single P1, System.Int32 P2) { }
        // RVA: 0x02E796E0  token: 0x600030F
        public System.Collections.Generic.Dictionary<System.Int32,System.Single> __Gen_Wrap_53(System.Object P0) { }
        // RVA: 0x02E79D50  token: 0x6000310
        public System.Void __Gen_Wrap_54(System.Object P0, System.Object P1) { }
        // RVA: 0x02E7A100  token: 0x6000311
        public Beyond.Rendering.EntityRenderCapturedEntity __Gen_Wrap_55(System.Object P0, System.Object P1, Beyond.Rendering.EntityVFXRendererMask P2) { }
        // RVA: 0x0125D2B0  token: 0x6000312
        public System.Int32 __Gen_Wrap_56(System.Object P0) { }
        // RVA: 0x0127B070  token: 0x6000313
        public Beyond.Rendering.EntityVFXRendererMask __Gen_Wrap_57(Beyond.Rendering.EntityVFXRendererType P0) { }
        // RVA: 0x02E7A840  token: 0x6000314
        public System.Void __Gen_Wrap_58(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x01416100  token: 0x6000315
        public System.Boolean __Gen_Wrap_59(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x01CB9BE0  token: 0x6000316
        public System.Void __Gen_Wrap_60(System.Object P0, System.Object P1, System.Object P2, System.Object P3, System.Boolean P4) { }
        // RVA: 0x02E7ACD0  token: 0x6000317
        public UnityEngine.Material[] __Gen_Wrap_61(System.Object P0) { }
        // RVA: 0x02E7B340  token: 0x6000318
        public System.Boolean __Gen_Wrap_62(Beyond.Rendering.AddMaterialConfig& P0) { }
        // RVA: 0x02E7BA00  token: 0x6000319
        public UnityEngine.Material __Gen_Wrap_63(System.Object P0, Beyond.Rendering.EntityRenderHelperMaterialController.AddedMaterialInfo P1, System.Object P2) { }
        // RVA: 0x02E7C270  token: 0x600031A
        public System.Boolean __Gen_Wrap_64(System.Object P0, UnityEngine.Vector4 P1) { }
        // RVA: 0x02E7C640  token: 0x600031B
        public UnityEngine.Texture __Gen_Wrap_65(System.Object P0, System.Object P1) { }
        // RVA: 0x02E7CDA0  token: 0x600031C
        public System.Int32 __Gen_Wrap_66(System.Object P0, System.Object P1, Beyond.Rendering.EntityVFXRendererMask P2, Beyond.Rendering.AddMaterialConfig P3) { }
        // RVA: 0x02E7D280  token: 0x600031D
        public System.Collections.Generic.List<Beyond.Rendering.EntityRenderHelperMaterialController.RendererInfo> __Gen_Wrap_67(System.Object P0, Beyond.Rendering.EntityVFXRendererMask P1, System.Boolean P2) { }
        // RVA: 0x019531C0  token: 0x600031E
        public System.Void __Gen_Wrap_68(System.Object P0, System.Int32 P1, System.Single P2) { }
        // RVA: 0x02474C00  token: 0x600031F
        public System.Single __Gen_Wrap_69(System.Object P0, System.Int32 P1, System.Single P2) { }
        // RVA: 0x02E7D940  token: 0x6000320
        public System.Void __Gen_Wrap_70(System.Object P0, System.Int32 P1, UnityEngine.Vector4 P2) { }
        // RVA: 0x02E7DD20  token: 0x6000321
        public UnityEngine.Vector4 __Gen_Wrap_71(System.Object P0, System.Int32 P1, UnityEngine.Vector4 P2) { }
        // RVA: 0x011C8780  token: 0x6000322
        public System.Void __Gen_Wrap_72(System.Object P0, System.Int32 P1, System.Object P2) { }
        // RVA: 0x02E7E470  token: 0x6000323
        public UnityEngine.Texture __Gen_Wrap_73(System.Object P0, System.Int32 P1, System.Object P2) { }
        // RVA: 0x012F6B10  token: 0x6000324
        public Beyond.Rendering.EntityVFXRendererMask __Gen_Wrap_74(System.Object P0, System.Int32 P1) { }
        // RVA: 0x019DF960  token: 0x6000325
        public System.Boolean __Gen_Wrap_75(System.Object P0, System.Int32 P1, System.Object P2) { }
        // RVA: 0x0128FA90  token: 0x6000326
        public System.Void __Gen_Wrap_76(System.Object P0, System.Int32 P1, Beyond.Rendering.EntityVFXRendererMask P2) { }
        // RVA: 0x011C8780  token: 0x6000327
        public System.Void __Gen_Wrap_77(System.Object P0, Beyond.Rendering.EntityVFXRendererMask P1, System.Object P2) { }
        // RVA: 0x01847510  token: 0x6000328
        public System.Void __Gen_Wrap_78(System.Object P0, System.Object P1, System.Boolean P2, Beyond.Rendering.EntityVFXRendererMask P3) { }
        // RVA: 0x01921E20  token: 0x6000329
        public System.Void __Gen_Wrap_79(System.Object P0, System.Boolean P1, Beyond.Rendering.EntityVFXRendererMask P2) { }
        // RVA: 0x02E7EBB0  token: 0x600032A
        public System.Single __Gen_Wrap_80(System.Object P0, System.Single P1, System.Single P2, System.Int32 P3) { }
        // RVA: 0x01753E10  token: 0x600032B
        public System.Void __Gen_Wrap_81(System.Object P0, Beyond.Rendering.EntityRenderHelperCustomPerDrawType P1, System.Single P2, Beyond.Rendering.EntityVFXRendererMask P3) { }
        // RVA: 0x02E7EF00  token: 0x600032C
        public System.Void __Gen_Wrap_82(System.Object P0, Beyond.Rendering.EntityRenderHelperVectorCustomPerDrawType P1, UnityEngine.Vector4 P2, Beyond.Rendering.EntityVFXRendererMask P3) { }
        // RVA: 0x01753E10  token: 0x600032D
        public System.Void __Gen_Wrap_83(System.Object P0, System.Int32 P1, System.Single P2, Beyond.Rendering.EntityVFXRendererMask P3) { }
        // RVA: 0x02E7F2E0  token: 0x600032E
        public System.Void __Gen_Wrap_84(System.Object P0, System.Int32 P1, UnityEngine.Vector4 P2, Beyond.Rendering.EntityVFXRendererMask P3) { }
        // RVA: 0x01E4DC00  token: 0x600032F
        public System.Void __Gen_Wrap_85(System.Object P0, System.Int32 P1, System.Object P2, Beyond.Rendering.EntityVFXRendererMask P3) { }
        // RVA: 0x017F4E90  token: 0x6000330
        public System.Boolean __Gen_Wrap_86(System.Object P0, System.Single P1) { }
        // RVA: 0x02E7F6E0  token: 0x6000331
        public System.Boolean __Gen_Wrap_87(System.Object P0, System.Boolean P1, System.Boolean& P2) { }
        // RVA: 0x02E7FA30  token: 0x6000332
        public System.Boolean __Gen_Wrap_88(System.Object P0, UnityEngine.Vector3& P1) { }
        // RVA: 0x02E80100  token: 0x6000333
        public System.Boolean __Gen_Wrap_89(System.Object P0, UnityEngine.Bounds& P1) { }
        // RVA: 0x02E807D0  token: 0x6000334
        public System.Boolean __Gen_Wrap_90(System.Object P0, UnityEngine.Bounds& P1) { }
        // RVA: 0x02E80F40  token: 0x6000335
        public UnityEngine.Bounds __Gen_Wrap_91(System.Object P0) { }
        // RVA: 0x012C0160  token: 0x6000336
        public System.Void __Gen_Wrap_92(System.Object P0, System.Object P1, System.Boolean P2) { }
        // RVA: 0x0125D2B0  token: 0x6000337
        public Beyond.Rendering.RenderHelperType __Gen_Wrap_93(System.Object P0) { }
        // RVA: 0x02E81570  token: 0x6000338
        public System.Boolean __Gen_Wrap_94(System.Object P0, System.Object P1, System.Boolean P2, Beyond.Rendering.EntityRenderHelperVisibleController.RendererInfo& P3) { }
        // RVA: 0x02E81D70  token: 0x6000339
        public System.Collections.Generic.List<Beyond.Rendering.EntityRendererTypeConfig> __Gen_Wrap_95(System.Object P0, System.Boolean P1) { }
        // RVA: 0x017AD6A0  token: 0x600033A
        public System.Single __Gen_Wrap_96(System.Object P0, System.Boolean P1) { }
        // RVA: 0x01298AE0  token: 0x600033B
        public Beyond.Rendering.EntityVFXPriorityType __Gen_Wrap_97(System.Object P0) { }
        // RVA: 0x01298AE0  token: 0x600033C
        public Beyond.Rendering.EntityVFXRendererMask __Gen_Wrap_98(System.Object P0) { }
        // RVA: 0x02E82400  token: 0x600033D
        public System.Void __Gen_Wrap_99(System.Object P0, System.Single P1, System.Boolean P2) { }
        // RVA: 0x02E65CB0  token: 0x600033E
        public UnityEngine.Material __Gen_Wrap_100(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x01738AF0  token: 0x600033F
        public System.Boolean __Gen_Wrap_101(Beyond.Rendering.EntityVFXFactoryAddedMaterialManager.AddedMaterialData P0) { }
        // RVA: 0x02214660  token: 0x6000340
        public System.Void __Gen_Wrap_102(System.Object P0, System.Object P1, System.Int32 P2, System.Single P3) { }
        // RVA: 0x02E664B0  token: 0x6000341
        public System.Void __Gen_Wrap_103(System.Object P0, System.Object P1, System.Int32 P2, UnityEngine.Vector4 P3) { }
        // RVA: 0x02E66940  token: 0x6000342
        public UnityEngine.Material __Gen_Wrap_104(System.Object P0, System.Object P1, UnityEngine.Vector4 P2) { }
        // RVA: 0x02E67150  token: 0x6000343
        public System.ValueTuple<System.Single,System.Single> __Gen_Wrap_105(UnityEngine.Bounds P0, UnityEngine.Vector3 P1) { }
        // RVA: 0x02E67880  token: 0x6000344
        public System.Void __Gen_Wrap_106(System.Object P0, System.Boolean P1, UnityEngine.Vector3 P2, System.Boolean P3, System.Single P4, System.Single& P5, System.Single& P6, UnityEngine.Vector3& P7) { }
        // RVA: 0x02E681B0  token: 0x6000345
        public Beyond.Rendering.EntityVFXCurveEvaluateResult __Gen_Wrap_107(System.Object P0, System.Single P1) { }
        // RVA: 0x02E68830  token: 0x6000346
        public System.Void __Gen_Wrap_108(System.Object P0, System.Object P1, System.Single P2) { }
        // RVA: 0x0259F9D0  token: 0x6000347
        public System.Void __Gen_Wrap_109(System.Object P0, System.Single P1, System.Object P2, System.Object P3) { }
        // RVA: 0x01527A20  token: 0x6000348
        public System.Void __Gen_Wrap_110(System.Object P0, System.Object P1, System.Object P2, System.Boolean P3) { }
        // RVA: 0x02E68C00  token: 0x6000349
        public Beyond.Rendering.PsoCreateUploadHelper __Gen_Wrap_111() { }
        // RVA: 0x02E69160  token: 0x600034A
        public System.Void __Gen_Wrap_112(System.Object P0, UnityEngine.HGPsoExtInfo P1) { }
        // RVA: 0x011C7C80  token: 0x600034B
        public System.Void __Gen_Wrap_113(System.Boolean P0) { }
        // RVA: 0x02CBB8F0  token: 0x600034C
        public System.Void __Gen_Wrap_114(System.Object P0, System.Object P1, System.Object P2, System.Object P3) { }
        // RVA: 0x0181A080  token: 0x600034D
        public System.Void __Gen_Wrap_115(System.Single P0) { }
        // RVA: 0x02E69500  token: 0x600034E
        public System.Void __Gen_Wrap_116(System.Object P0, Beyond.EventData<System.String,System.String>& P1) { }
        // RVA: 0x02E698D0  token: 0x600034F
        public System.Void __Gen_Wrap_117(System.Boolean P0, System.Object P1, System.Object P2) { }
        // RVA: 0x02E69C70  token: 0x6000350
        public System.Collections.Generic.List<UnityEngine.Vector3> __Gen_Wrap_118(System.Object P0, System.Single P1, System.Int32 P2) { }
        // RVA: 0x02E6A320  token: 0x6000351
        public UnityEngine.Vector3 __Gen_Wrap_119(System.Object P0, UnityEngine.Vector2Int P1) { }
        // RVA: 0x02E6AA30  token: 0x6000352
        public System.Collections.Generic.List<UnityEngine.Vector3> __Gen_Wrap_120(System.Object P0, UnityEngine.Vector3 P1) { }
        // RVA: 0x02E6B180  token: 0x6000353
        public UnityEngine.Vector2Int __Gen_Wrap_121(System.Object P0, UnityEngine.Vector3 P1) { }
        // RVA: 0x01F50410  token: 0x6000354
        public System.Int32 __Gen_Wrap_122(System.Int32 P0, System.Int32 P1, System.Int32 P2) { }
        // RVA: 0x017F2A10  token: 0x6000355
        public System.Void __Gen_Wrap_123(System.Int32 P0, System.Int32 P1) { }
        // RVA: 0x02E6B890  token: 0x6000356
        public System.Void __Gen_Wrap_124(System.Int32 P0, System.Int32 P1, System.Int32 P2, System.Int32 P3, System.Object P4) { }
        // RVA: 0x02E6BBB0  token: 0x6000357
        public UnityEngine.Color __Gen_Wrap_125(System.Object P0, System.Int32 P1, System.Int32 P2) { }
        // RVA: 0x02E6C200  token: 0x6000358
        public System.Single[] __Gen_Wrap_126(System.Int32 P0, System.Single P1) { }
        // RVA: 0x02E6C7E0  token: 0x6000359
        public System.Single[,] __Gen_Wrap_127(System.Object P0, System.Object P1, UnityEngine.Vector2Int P2) { }
        // RVA: 0x02E6CFF0  token: 0x600035A
        public System.Void __Gen_Wrap_128(UnityEngine.Bounds P0, System.Object P1, UnityEngine.Vector3& P2, UnityEngine.Vector3& P3, System.Nullable<UnityEngine.Vector3> P4) { }
        // RVA: 0x02E6DB70  token: 0x600035B
        public Beyond.Rendering.VFXSludgeNavUtils.Spline2DProjectAndSimplify.Frame __Gen_Wrap_129(UnityEngine.Vector3 P0, UnityEngine.Vector3 P1) { }
        // RVA: 0x02E6E2B0  token: 0x600035C
        public Beyond.Rendering.VFXSludgeNavUtils.Spline2DProjectAndSimplify.Frame __Gen_Wrap_130(UnityEngine.Vector3 P0, System.Single P1) { }
        // RVA: 0x02E6E930  token: 0x600035D
        public System.Collections.Generic.List<UnityEngine.Vector3> __Gen_Wrap_131(System.Object P0, System.Single P1, System.Int32 P2, System.Boolean P3, System.Single P4) { }
        // RVA: 0x02E6F010  token: 0x600035E
        public UnityEngine.Vector3 __Gen_Wrap_132(Beyond.Rendering.VFXSludgeNavUtils.Spline2DProjectAndSimplify.Frame& P0, UnityEngine.Vector3 P1) { }
        // RVA: 0x02E6FAE0  token: 0x600035F
        public System.Void __Gen_Wrap_133(System.Object P0, Beyond.Rendering.VFXSludgeNavUtils.Spline2DProjectAndSimplify.Frame P1, System.Collections.Generic.List<UnityEngine.Vector2>& P2, System.Collections.Generic.List<System.Single>& P3, System.Single& P4, System.Single& P5, System.Single& P6, System.Single& P7) { }
        // RVA: 0x01D41D40  token: 0x6000360
        public System.Int32 __Gen_Wrap_134(System.Int32 P0, System.Int32 P1) { }
        // RVA: 0x02E70730  token: 0x6000361
        public System.Collections.Generic.List<System.Int32> __Gen_Wrap_135(System.Object P0) { }
        // RVA: 0x02E70D90  token: 0x6000362
        public System.Single __Gen_Wrap_136(System.Object P0, System.Int32 P1, System.Boolean P2) { }
        // RVA: 0x02E71090  token: 0x6000363
        public System.Single __Gen_Wrap_137(UnityEngine.Vector2 P0, UnityEngine.Vector2 P1, UnityEngine.Vector2 P2) { }
        // RVA: 0x02E71560  token: 0x6000364
        public System.Collections.Generic.List<System.Int32> __Gen_Wrap_138(System.Object P0, System.Boolean P1, System.Single P2, System.Single P3, System.Single P4) { }
        // RVA: 0x02E71C50  token: 0x6000365
        public Beyond.Rendering.VFXSludgeNavUtils.Spline2DProjectAndSimplify.ProcessResult __Gen_Wrap_139(System.Object P0, Beyond.Rendering.VFXSludgeNavUtils.Spline2DProjectAndSimplify.Frame P1, System.Single P2, System.Int32 P3, System.Boolean P4, Beyond.Rendering.VFXSludgeNavUtils.Spline2DProjectAndSimplify.EdgeMode P5, System.Single P6, System.Single P7, System.Single P8) { }
        // RVA: 0x02E72480  token: 0x6000366
        public System.Collections.Generic.List<UnityEngine.Vector2> __Gen_Wrap_140(System.Object P0, System.Single P1, System.Int32 P2, System.Int32 P3, System.Boolean P4, System.Boolean P5) { }
        // RVA: 0x02E72B80  token: 0x6000367
        public System.Collections.Generic.List<UnityEngine.Vector3> __Gen_Wrap_141(System.Object P0, System.Single P1, System.Single P2, System.Boolean P3) { }
        // RVA: 0x02E73260  token: 0x6000368
        public System.Void __Gen_Wrap_142(System.Object P0, System.Collections.Generic.List<UnityEngine.Vector2>& P1, System.Int32 P2, System.Boolean P3, System.Boolean P4, System.Boolean P5) { }
        // RVA: 0x02E73A70  token: 0x6000369
        public System.Collections.Generic.List<UnityEngine.Vector3> __Gen_Wrap_143(System.Object P0, System.Object P1, System.Single P2) { }
        // RVA: 0x01294170  token: 0x600036A
        public System.Void __Gen_Wrap_144(System.Object P0, System.Object P1, System.Object P2, System.Object P3, System.Int32 P4) { }
        // RVA: 0x02E741D0  token: 0x600036B
        public Beyond.Rendering.ECS.HGAddedMaterialController.OverlayLayer __Gen_Wrap_145(System.Object P0) { }
        // RVA: 0x0137AF60  token: 0x600036C
        public System.Int32 __Gen_Wrap_146(System.Object P0, System.Object P1) { }
        // RVA: 0x02E74840  token: 0x600036D
        public UnityEngine.HGMeshRendererData __Gen_Wrap_147(System.Object P0) { }
        // RVA: 0x01293280  token: 0x600036E
        public System.Void __Gen_Wrap_148(System.Object P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4, System.Object P5) { }
        // RVA: 0x01363040  token: 0x600036F
        public System.Void __Gen_Wrap_149(System.Object P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4) { }
        // RVA: 0x02E74EA0  token: 0x6000370
        public UnityEngine.Material __Gen_Wrap_150(System.Object P0, System.Int32 P1) { }
        // RVA: 0x02E75530  token: 0x6000371
        public UnityEngine.HGMeshRenderer __Gen_Wrap_151(System.Object P0) { }
        // RVA: 0x02E75B90  token: 0x6000372
        public UnityEngine.Bounds __Gen_Wrap_152(UnityEngine.Bounds P0, UnityEngine.Matrix4x4 P1) { }
        // RVA: 0x04D7A3D0  token: 0x6000373
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000097  // size: 0x60
    public class ILFixInterfaceBridge : IFix.Core.AnonymousStorey, System.IDisposable, System.Collections.IEnumerator, System.Collections.Generic.IEnumerator`1, System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        private System.Int32 methodId_0;  // 0x40
        private System.Int32 methodId_1;  // 0x44
        private System.Int32 methodId_2;  // 0x48
        private System.Int32 methodId_3;  // 0x4c
        private System.Int32 methodId_4;  // 0x50
        private System.Int32 methodId_5;  // 0x54
        private System.Int32 methodId_6;  // 0x58

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0A9C6B24 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0A9C6BB4 */ }

        // Methods
        // RVA: 0x06365FC0  token: 0x6000374
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x06365D60  token: 0x6000375
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x06365E98  token: 0x6000377
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x0A8A5D4C  token: 0x6000379
        private virtual System.Void MoveNext() { }
        // RVA: 0x0A8A5FAC  token: 0x600037A
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine P0) { }
        // RVA: 0x0A9C6C44  token: 0x600037B
        public System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x0A9C6AC0  token: 0x600037C
        public System.Void RefAwaitUnsafeOnCompleteMethod() { }
        // RVA: 0x0A9C6A98  token: 0x600037D
        public System.Void RefAsyncBuilderStartMethod() { }

    }

    // TypeToken: 0x2000098  // size: 0x18
    public class WrappersManagerImpl : IFix.Core.WrappersManager
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x600037E
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x0A9C6EB0  token: 0x600037F
        public static IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id) { }
        // RVA: 0x0302E240  token: 0x6000380
        public static System.Boolean IsPatched(System.Int32 id) { }
        // RVA: 0x0A9C6DC0  token: 0x6000381
        public virtual System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon) { }
        // RVA: 0x0A9C6E54  token: 0x6000382
        public virtual System.Object CreateWrapper(System.Int32 id) { }
        // RVA: 0x0A9C6F04  token: 0x6000383
        public virtual System.Object InitWrapperArray(System.Int32 len) { }
        // RVA: 0x0A9C6D38  token: 0x6000384
        public virtual IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine) { }

    }

    // TypeToken: 0x2000099  // size: 0x14
    public sealed struct IDMAP0
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static IFix.IDMAP0 HG-Rendering-Runtime-ContainerWaterManager-get_dy0;  // const
        public static IFix.IDMAP0 HG-Rendering-Runtime-ContainerWaterManager-_PrecomputeCylinderMask0;  // const
        public static IFix.IDMAP0 HG-Rendering-Runtime-ContainerWaterManager-_InitializeWaveSystem0;  // const
        public static IFix.IDMAP0 HG-Rendering-Runtime-ContainerWaterManager-Start0;  // const
        public static IFix.IDMAP0 HG-Rendering-Runtime-ContainerWaterManager-_IsInsideCylinder0;  // const
        public static IFix.IDMAP0 HG-Rendering-Runtime-ContainerWaterManager-_ApplyReflectiveBoundary0;  // const
        public static IFix.IDMAP0 HG-Rendering-Runtime-ContainerWaterManager-_FindNearestInsidePoint0;  // const
        public static IFix.IDMAP0 HG-Rendering-Runtime-ContainerWaterManager-_ApplyAbsorptiveBoundary0;  // const
        public static IFix.IDMAP0 HG-Rendering-Runtime-ContainerWaterManager-_WorldToTextureCoords0;  // const
        public static IFix.IDMAP0 HG-Rendering-Runtime-ContainerWaterManager-_IsValidPosition0;  // const
        public static IFix.IDMAP0 HG-Rendering-Runtime-ContainerWaterManager-_UnifiedWaveStep0;  // const
        public static IFix.IDMAP0 HG-Rendering-Runtime-ContainerWaterManager-_ApplyMatrixToTexture0;  // const
        public static IFix.IDMAP0 HG-Rendering-Runtime-ContainerWaterManager-_UpdateVortexState0;  // const
        public static IFix.IDMAP0 HG-Rendering-Runtime-ContainerWaterManager-ResetWaveTexture0;  // const
        public static IFix.IDMAP0 HG-Rendering-Runtime-ContainerWaterManager-Update0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CustomPerDrawDataChannelUtils-SetPerDrawData_LitDissolve0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CustomPerDrawDataChannelUtils-SetPerDrawData_HoudiniVAT0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CustomPerDrawDataChannelUtils-SetPerDrawData_MovingBamboo0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CustomPerDrawDataChannelUtils-SetPerDrawData_EmissiveColor0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CustomPerDrawDataChannelUtils-SetPerDrawData_EmissiveAlbedoColor0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CustomPerDrawDataChannelUtils-SetPerDrawData_SweepLightIntensity0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CustomPerDrawDataChannelUtils-SetPerDrawData_SweepBreathing0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CustomPerDrawDataChannelUtils-SetPerDrawData_UVAnimation0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CustomPerDrawDataChannelUtils-SetPerDrawData_CharacterParams0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CustomPerDrawDataChannelUtils-SetPerDrawData_VFXAlpha00;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CustomPerDrawDataChannelUtils-SetPerDrawData_TrailDisplayArea0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CustomPerDrawDataChannelUtils-SetPerDrawData_TrailScanCircle0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PipelineSettingHelper-_GetFullSettingPath0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PipelineSettingHelper-_LoadSetting0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PipelineSettingHelper-LoadPipelineSettings0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ResourceRouter-_GetCategoryFolder0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ResourceRouter-GetEnvironmentsFullPath0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ResourceRouter-GetDecalResourcesPath0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ResourceRouter-GetDecalResourcesFullPath0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-get_isWarming0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-GetPsoPathName0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-GetPsoPathName1;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_GetCacheFileSize0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-GetLoginWarmupDetail0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManagerRemoteCfg-get_cpuNameForWarmUp0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManagerRemoteCfg-_CheckDeviceMatch0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManagerRemoteCfg-IsDeviceInBlackList0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManagerRemoteCfg-IsEnableDx11DriverVersionDetect0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-ProcessRemoteConfig0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-MarkNeedShaderWarmUp0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_MarkWarmupFinishAPI0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_MarkWarmUpLoginFinish0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-WindowsGpuDriverVersion-_GetDeviceRegistryPropertyString0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-WindowsGpuDriverVersion-_ContainsIgnoreCase0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-WindowsGpuDriverVersion-_ScoreAgainstUnityCurrentGpu0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-WindowsGpuDriverVersion-_GetDriverKeySuffix0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-WindowsGpuDriverVersion-_ReadRegistrySz0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-WindowsGpuDriverVersion-GetAllDisplayDriverVersions0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-WindowsGpuDriverVersion-SerializeAllResults0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_MarkWarmupDx11Driver0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-MarkWarmUpLoginFinish0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-CleanLoadedPso0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_TryDeleteVulkanPsoCacheFile0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-TryRepairPsoCacheAfterCorruptCheck0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_ResetWarmUpStatsIfNeed0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_IsDeviceSupportWarmUp0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_CheckNeedWarmUpByFirstEntry0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_IsNeedWarmupByGraphicsChange0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_SetWarmupRetryCount0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_SetWarmupUnfinishedCount0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_CheckWarmUpUnfinishedOften0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-WindowsGpuDriverVersion-GetBestMatch0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-WindowsGpuDriverVersion-_TryGetDriverVersionByKeyId0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-WindowsGpuDriverVersion-IsBestMatchDriverVersionChanged0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_IsNeedWarmupDx11Driver0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_IsNeedWarmupByCacheOutdated0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_CheckNeedWarmUpByDeviceState0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-CheckNeedWarmUpLogin0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_DisposeWarmupAssetHandles0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-StopWarmupShadersDownloading0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_StopDownloadingWarmupAndWait0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_BuildLoginWarmupPaths0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_LoadPsoAssets0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_GetBackgroundWarmupStartIndex0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_GetCompileCountForRange0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_InitWarmupStats0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_GetLoginTimeLimit0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_GetWarmupEndIndexExclusive0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PsoCreateUploadHelper-IsPsoRecordEnabled0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PsoCreateUploadHelper-BeforeWarmUpBegin0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-TimeCostCounter-Tick0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_StepProgress0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-TimeCostCounter-get_isTimeOut0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PsoCreateUploadHelper-AfterWarmUpFinish0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_WarmupOnePsoTimeLimited0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_SetDownloadingWarmupUnfinishedCount0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-WarmupShadersLogin0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_UpdateDownloadWarmupPercent0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_IsDeviceSupportDownloadWarmUp0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_GetDownloadingWarmupUnfinishedCount0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-_WarmupOnePsoBackgroundOnly0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-WarmupShadersDownloading0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-WarmupOnePso0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-WarmupShadersScene0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ShaderWarmupManager-TimeCostCounter-Begin0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-WindowsGpuDriverVersion-GetCurrentDriverVersion0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-WindowsGpuDriverVersion-SerializeAllCurrentResults0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-WindowsGpuDriverVersion-Result-ToString0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-WindowsGpuDriverVersion-SafeRegistryHandle-ReleaseHandle0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-SceneStateAreaKey-Equals0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-SceneStateAreaKey-Equals1;  // const
        public static IFix.IDMAP0 Beyond-Rendering-SceneStateAreaKey-GetHashCode0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderAlphaDitherController-_GetCurMinAlpha0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderAlphaDitherController-_SetMaterialDitherEnabled0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderAlphaDitherController-_SetMaterialDitherAlpha0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderAlphaDitherController-_ApplyNewAlpha0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderAlphaDitherController-ResetDitherAlpha0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderAlphaDitherController-SetDitherAlpha0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderAlphaDitherController-GetDitherAlpha0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderAlphaDitherController-Reset0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderAlphaDitherController-GetAlphaDictionary0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderCaptureMeshController-Release0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderCaptureMeshController-Reset0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderCaptureMeshController-_SyncTransformInRenderPipeline0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderCaptureMeshController-LateTick0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderCaptureMeshController-InitRendererTypeMasks0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderCaptureMeshController-Capture0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-_GetSubMeshCount0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXUtils-ToMask0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-_Init0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-TryInit0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialController-Init0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-AddedMaterialController-Init0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-Init0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-OnRelease0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-AddedMaterialController-Reset0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-get_rendererValid0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-TryBeginMaterialUpdate0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-get_currentMeshMaterials0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-TrySetSharedMaterials0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-AddMaterialConfig-get_useCustomizeProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-AddedMaterialController-_GetCustomizePropertyMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-AddedMaterialController-ApplySharedMaterialArray0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-TryReplaceSharedMaterials0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialController-_ReplaceSharedMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialController-_ReleaseAllReplaceMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialGroupData-Clear0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialController-Reset0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-TrySetCharacterPerDrawData0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-CustomPerDrawDataController-Reset0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-Reset0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-IsRendererValid0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-IsAllRendererValid0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-TryStopShadowCasting0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-StopShadowCasting0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-TryRevertShadowCastingMode0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RevertShadowCastingMode0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityCustomizeRendererPropertyConfig-GetColorTexture0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-AddedMaterialController-_InitCustomizeRendererProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-AddedMaterialController-RequireAddMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-_GetRenderersOfRendererMask0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-AddMaterialToAllRenderer0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-AddedMaterialController-SetAddictiveFloatProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-SetAddictiveFloatProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-AddedMaterialController-GetAddictiveFloatProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-GetAddictiveFloatProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-AddedMaterialController-SetAddictiveVectorProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-SetAddictiveVectorProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-AddedMaterialController-GetAddictiveVectorProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-GetAddictiveVectorProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-AddedMaterialController-SetAddictiveTextureProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-SetAddictiveTextureProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-AddedMaterialController-GetAddictiveTextureProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-GetAddictiveTextureProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-AddedMaterialController-RequireRemoveMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RemoveMaterialFromAllRenderer0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-AddedMaterialController-GetAddedMaterialsOfCustomizeProperties0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-GetAddedMaterialsOfCustomizeProperties0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialGroupData-CopyFrom0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialController-_CopyReplaceMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialController-_SplitRenderers0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialController-_GetAffectGroupData0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-get_rendererName0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-TrySetSharedMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialController-_CreateReplaceMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialController-_TryEnsureReplaceMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialController-SetKeywordEnableToAllMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-SetKeywordEnableToAllMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialController-SetDepthOnlyPassEnableToAllMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-SetDepthOnlyPassEnableToAllMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-CustomPerDrawDataController-Init0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-CustomPerDrawDataController-_PackUNorm8ToFloat0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-CustomPerDrawDataController-SetFloatToCustomPerDraw0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-SetFloatToCustomPerDraw0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-TrySetEmissiveColor0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-CustomPerDrawDataController-SetVectorToCustomPerDraw0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-SetVectorToCustomPerDraw0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialController-SetFloatToAllMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-SetFloatToAllMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialController-SetVectorToAllMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-SetVectorToAllMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialController-SetTextureToAllMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-SetTextureToAllMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-TryFillRendererTypeMask0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-FillRendererTypeMasks0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-TrySetManualDitherAlphaValue0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-SetManualDitherAlphaValue0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-TrySetManualDitherEnable0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-SetManualDitherEnable0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-TryGetRendererPosition0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-TryGetRendererBounds0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-RendererInfo-TryEncapsulateBounds0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-GetRendererBounds0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialController-SetZTestInMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-SetZTestInMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-GetRenderHelperType0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialController-ReleaseFloatToAllMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialController-ReleaseVectorToAllMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperMaterialController-MeshMaterialController-ReleaseTextureToAllMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperVisibleController-RendererInfo-_Init0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperVisibleController-RendererInfo-TryInit0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperVisibleController-Init0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperVisibleController-RendererInfo-get_rendererValid0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperVisibleController-RendererInfo-TryAddToDictionary0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperVisibleController-_InitDictionary0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperVisibleController-_TryGetRendererInfo0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperVisibleController-Release0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperVisibleController-RendererInfo-TrySetVisible0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperVisibleController-SetVisibleByNameContainsStr0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperVisibleController-RendererInfo-TrySetVisibleByRenderer0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperVisibleController-SetVisibleByRenderer0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperVisibleController-RendererInfo-TryResetVisible0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperVisibleController-ResetVisibleByNameContainsStr0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperVisibleController-ResetVisibleByRenderer0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderHelperVisibleController-Reset0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderMaterialManager-SetFloat0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderMaterialManager-SetTexture0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderMaterialManager-SetVector0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderMaterialManager-AddMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderMaterialManager-_RevertMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderMaterialManager-RemoveMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderMaterialManager-ClearMaterials0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderMaterialManager-SetKeywordEnable0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderMaterialManager-SetDepthOnlyEnabled0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityRenderMaterialManager-SetZTestInMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXAsset-GetRendererTypeConfigs0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXAssetBase-get_assetName0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-get_isPlaying0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-get_isStopping0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-get_isHiding0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-get_isKeyword0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-get_vfxName0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-get_destroyTime0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-set_destroyTime0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-get_lastTickTime0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-set_lastTickTime0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-SetOwner0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-InitLOD0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-SetName0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-Stop0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-OnDestroy0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-Destroy0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-Play0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-Replay0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-Tick0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-RefreshLastTickTime0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-GetDeltaTimeFromLastTick0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-GetPriorityType0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-GetRendererMask0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-GetAllowSimultaneousKeyword0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-GetNeedTick0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-OnHide0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-Hide0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-OnShow0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-Show0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-OnSample0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerBase-Sample0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerStateBase-get_isStopping0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerStateBase-Play0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerStateBase-Replay0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerStateBase-_FinalStop0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerStateBase-get_useEndCurve0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerStateBase-get_endDuration0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerStateBase-Stop0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerStateBase-get_useLoopCurve0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerStateBase-GetNeedTick0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerStateBase-get_useScaledTime0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerStateBase-get_duration0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerStateBase-get_loop0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerStateBase-get_useStartCurve0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerStateBase-get_loopDuration0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerStateBase-Tick0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXControllerStateBase-OnSample0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXFactoryAddedMaterialManager-Init0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXFactoryAddedMaterialManager-_AddVATMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXFactoryAddedMaterialManager-_GetVATMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXFactoryAddedMaterialManager-_GetAddedMaterialIndex0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXFactoryAddedMaterialManager-_RemoveVATMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXFactoryAddedMaterialManager-AddMaterialToAllRenderer0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXFactoryAddedMaterialManager-RemoveMaterialFromAllRenderer0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXFactoryAddedMaterialManager-SetFloatToFactoryMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXFactoryAddedMaterialManager-SetVectorToFactoryMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXFactoryMaterialCache-GetVATMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXFactoryMaterialCache-ReleaseVATMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXUtils-GetBoundsMinMaxOfDirection0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXUtils-CalculateCutOffBounds0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXUtils-Evaluate0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXUtils-_UpdateCurve0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-EntityVFXUtils-ApplyEntityVFXCurves0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CommonVAT-get_PropertySetterType0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CommonVAT-Start0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CommonVAT-_ClearTimer0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CommonVAT-_UpdateProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CommonVAT-_CreateTimer0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CommonVAT-_NextPlay0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CommonVAT-_DoPlay0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-CommonVAT-Play0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-FactoryVAT-_InitRenderer_MeshRender0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-FactoryVAT-_InitRenderer0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-FactoryVAT-Start0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-FactoryVAT-_ReSetParamsToRenderer_MeshRender0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-FactoryVAT-_ReSetParamsToRenderer0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-FactoryVAT-Reset0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-FactoryVAT-_SetParamsToRenderer_MeshRender0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-FactoryVAT-_SetParamsToRenderer0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-FactoryVAT-_DoPlay0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-FactoryVAT-_UpdateAnim0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-FactoryVAT-OnUpdate0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-FactoryVAT-Play0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VATRendererPropertySetter-Init0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VATRendererPropertySetter-SetFloat0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VATRendererPropertySetter-SetVector0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VATEntityRendererHelperPropertySetter-Init0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VATEntityRendererHelperPropertySetter-SetFloat0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VATEntityRendererHelperPropertySetter-SetVector0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PsoCreateUploadHelper-get_instance0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PsoCreateUploadHelper-get_persistentDataPath0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PsoCreateUploadHelper-_PsoUploadProcessOneCallBack0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PsoCreateUploadHelper-_PsoUpdateGameInfoCallBack0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PsoCreateUploadHelper-Initialize0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PsoCreateUploadHelper-_UploadPipelineCreatedInfo0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PsoCreateUploadHelper-_UploadFileToServer0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PsoCreateUploadHelper-_CreateAndUploadFileToServer0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PsoCreateUploadHelper-_CreateAndUploadFinalJson0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PsoCreateUploadHelper-_Tick0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PsoCreateUploadHelper-_SceneLoadStartHandler0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PsoCreateUploadHelper-Init0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PsoCreateUploadHelper-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PsoCreateUploadHelper-_UploadFile0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-PsoCreateUploadHelper-_Callback0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeUtils-GetPolygonOffSpline0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-_CalculatePolygonAndBounds0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-GetLocalPosition0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-_RaycastSceneObjectsHeight0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeUtils-CalculatePointPolygonHorizonRayCross0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-GetLocalIndex0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-_CalculateInPolygon0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-_CalculateInHighPowerZone0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-_GetHash0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-_ForAround0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-_CalculateEdgeSignedDistance0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-_CalculateEdgeSignedDistance1;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-_BlurTopHeight0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-_CalculateThicknessAndTopHeight0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-_CalculateNormals0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-_GetTextureColor10;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-_CreateTexture0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-Bake0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-_CreateGaussianKernel0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-_Convolution0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-_GetHash1;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeBaker-_GetTextureColor20;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeNavUtils-BoundsUtil-GetTopBottomPoints0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeNavUtils-Spline2DProjectAndSimplify-MakeFrameFromPlaneNormal0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeNavUtils-Spline2DProjectAndSimplify-MakeFrameByYaw0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeNavUtils-Spline2DProjectAndSimplify-SampleUniformByArcLength0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeNavUtils-Spline2DProjectAndSimplify-Frame-WorldToFrame0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeNavUtils-Spline2DProjectAndSimplify-ProjectWorldToFrameXZ0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeNavUtils-Spline2DProjectAndSimplify-ConvexHullIndices0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeNavUtils-Spline2DProjectAndSimplify-_AngleDegAt0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeNavUtils-Spline2DProjectAndSimplify-_CurvatureAt0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeNavUtils-Spline2DProjectAndSimplify-_PointToSegmentDistance2D0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeNavUtils-Spline2DProjectAndSimplify-Simplify2D_ByCurvatureAngleDeviation0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeNavUtils-Spline2DProjectAndSimplify-Process0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeNavUtils-Spline2DProjectAndSimplify-Frame-FrameToWorld0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeNavUtils-SplineSamplingUtils-SampleByArcLength0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeNavUtils-PolySimplify-SimplifyByAngle0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeUtils-GetPolygonOffSplineWithSimplify0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-VFXSludgeUtils-GetPolygonOffByPolyLine0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-SplineMovingObjects-HGSplineMovingObjects-_CreateInstance0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-SplineMovingObjects-HGSplineMovingObjects-_ExtractSplineData0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-SplineMovingObjects-HGSplineMovingObjects-_DestroyInstance0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-SplineMovingObjects-HGSplineMovingObjects-_Release0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-SplineMovingObjects-HGSplineMovingObjects-Update0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-SplineMovingObjects-HGSplineMovingObjects-OnEnable0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-SplineMovingObjects-HGSplineMovingObjects-OnDisable0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-SplineMovingObjects-HGSplineMovingObjects-OnDestroy0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-SplineMovingObjects-HGSplineMovingObjects-OnDrawGizmosSelected0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-SplineMovingObjects-HGSplineMovingObjects-Pause0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-SplineMovingObjects-HGSplineMovingObjects-Play0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-SplineMovingObjects-HGSplineMovingObjects-Stop0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-LODLightmapping-LightmapInfoTransfer-_RendererInfoTransfer0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-LODLightmapping-LightmapInfoTransfer-Awake0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-LODLightmapping-LightmapInfoTransfer-OnEnable0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-LODLightmapping-LODLightmapping-RendererInfoTransfer0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGAddedMaterialController-Init0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGAddedMaterialController-_CreateNewLayer0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGAddedMaterialController-RequireAddMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGAddedMaterialController-RequireRemoveMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGAddedMaterialController-GetActiveOverlayRenderers0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGAddedMaterialController-get_activeLayerCount0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGAddedMaterialController-GetAddedMaterialsOfCustomizeProperties0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGAddedMaterialController-SetAddictiveFloatProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGAddedMaterialController-GetAddictiveFloatProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGAddedMaterialController-SetAddictiveVectorProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGAddedMaterialController-GetAddictiveVectorProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGAddedMaterialController-SetAddictiveTextureProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGAddedMaterialController-GetAddictiveTextureProperty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGAddedMaterialController-Reset0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGAddedMaterialController-_DestroyLayer0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGAddedMaterialController-Release0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialCommitter-get_clonedData0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialCommitter-Init0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialCommitter-MarkMaterialsArrayDirty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialCommitter-IsDirty0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialCommitter-_Commit0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialCommitter-TryCommit0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialCommitter-ForceCommit0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialCommitter-Reset0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialCommitter-Release0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialController-get_currentMeshMaterials0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialController-Init0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialController-_ApplyDepthOnlyPassToMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialController-_ApplyAllPropertiesToOneMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialController-_GetOrCreateInstancedMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialController-SetFloatToAllMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialController-SetVectorToAllMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialController-SetTextureToAllMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialController-SetKeywordEnableToAllMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialController-SetDepthOnlyPassEnableToAllMaterial0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialController-SetManualDitherEnable0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialController-SetManualDitherAlphaValue0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialController-ReplaceAllMaterials0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialController-ReplaceMaterialBySource0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialController-Reset0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGMaterialController-Release0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGRendererStateController-Init0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGRendererStateController-IsValid0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGRendererStateController-get_hgRenderer0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGRendererStateController-SetVisible0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGRendererStateController-Reset0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGRendererStateController-_WarnNoMeshes0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGRendererStateController-_EnsureMeshLocalBoundsCached0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGRendererStateController-_LocalBoundsToWorld0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGRendererStateController-GetBounds0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGRendererStateController-InvalidateMeshCache0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGRendererStateController-StopShadowCasting0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGRendererStateController-RevertShadowCastingMode0;  // const
        public static IFix.IDMAP0 Beyond-Rendering-ECS-HGRendererStateController-Release0;  // const

    }

}

