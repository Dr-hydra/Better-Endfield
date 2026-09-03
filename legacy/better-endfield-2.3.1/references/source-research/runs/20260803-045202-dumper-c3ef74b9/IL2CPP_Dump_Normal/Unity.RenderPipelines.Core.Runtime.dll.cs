// ========================================================
// Dumped by @desirepro
// Assembly: Unity.RenderPipelines.Core.Runtime.dll
// Classes:  411
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000004  // size: 0x20
    public class SceneRenderPipeline : UnityEngine.MonoBehaviour
    {
        // Fields
        public UnityEngine.Rendering.RenderPipelineAsset renderPipelineAsset;  // 0x18

        // Methods
        // RVA: 0x0B2B5E40  token: 0x6000003
        private System.Void OnEnable() { }
        // RVA: 0x0B2B5E40  token: 0x6000004
        private System.Void OnValidate() { }
        // RVA: 0x05393520  token: 0x6000005
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x14
    public sealed struct ShaderQuality
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Rendering.HGShaderQualitySettings.ShaderQuality Low;  // const
        public static Beyond.Rendering.HGShaderQualitySettings.ShaderQuality High;  // const

    }

    // TypeToken: 0x2000008  // size: 0x14
    public sealed struct UpDirection
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.LightAnchor.UpDirection World;  // const
        public static UnityEngine.LightAnchor.UpDirection Local;  // const

    }

    // TypeToken: 0x2000009  // size: 0x34
    public sealed struct Axes
    {
        // Fields
        public UnityEngine.Vector3 up;  // 0x10
        public UnityEngine.Vector3 right;  // 0x1c
        public UnityEngine.Vector3 forward;  // 0x28

    }

    // TypeToken: 0x200000B  // size: 0x20
    public sealed struct Brick : System.IEquatable`1
    {
        // Fields
        public UnityEngine.Vector3Int position;  // 0x10
        public System.Int32 subdivisionLevel;  // 0x1c

        // Methods
        // RVA: 0x04D92BB0  token: 0x6000031
        private System.Void .ctor(UnityEngine.Vector3Int position, System.Int32 subdivisionLevel) { }
        // RVA: 0x0B29F5C8  token: 0x6000032
        public virtual System.Boolean Equals(UnityEngine.Experimental.Rendering.ProbeBrickIndex.Brick other) { }

    }

    // TypeToken: 0x200000C  // size: 0x24
    public sealed struct ReservedBrick
    {
        // Fields
        public UnityEngine.Experimental.Rendering.ProbeBrickIndex.Brick brick;  // 0x10
        public System.Int32 flattenedIdx;  // 0x20

    }

    // TypeToken: 0x200000D  // size: 0x20
    public sealed struct VoxelMeta
    {
        // Fields
        public UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId id;  // 0x10
        public System.Collections.Generic.List<System.UInt16> brickIndices;  // 0x18

    }

    // TypeToken: 0x200000E  // size: 0x20
    public sealed struct BrickMeta
    {
        // Fields
        public System.Collections.Generic.HashSet<UnityEngine.Vector3Int> voxels;  // 0x10
        public System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickIndex.ReservedBrick> bricks;  // 0x18

    }

    // TypeToken: 0x200000F  // size: 0x40
    public sealed struct CellIndexUpdateInfo
    {
        // Fields
        public System.Int32 firstChunkIndex;  // 0x10
        public System.Int32 numberOfChunks;  // 0x14
        public System.Int32 minSubdivInCell;  // 0x18
        public UnityEngine.Vector3Int minValidBrickIndexForCellAtMaxRes;  // 0x1c
        public UnityEngine.Vector3Int maxValidBrickIndexForCellAtMaxResPlusOne;  // 0x28
        public UnityEngine.Vector3Int cellPositionInBricksAtMaxRes;  // 0x34

    }

    // TypeToken: 0x2000010  // size: 0x20
    public sealed class <>c__DisplayClass31_0
    {
        // Fields
        public UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId id;  // 0x10
        public System.Predicate<UnityEngine.Experimental.Rendering.ProbeBrickIndex.VoxelMeta> <>9__0;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000033
        public System.Void .ctor() { }
        // RVA: 0x0702DACC  token: 0x6000034
        private System.Boolean <AddBricks>b__0(UnityEngine.Experimental.Rendering.ProbeBrickIndex.VoxelMeta lhs) { }

    }

    // TypeToken: 0x2000011  // size: 0x20
    public sealed class <>c__DisplayClass32_0
    {
        // Fields
        public UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId id;  // 0x10
        public System.Predicate<UnityEngine.Experimental.Rendering.ProbeBrickIndex.VoxelMeta> <>9__0;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000035
        public System.Void .ctor() { }
        // RVA: 0x0702DACC  token: 0x6000036
        private System.Boolean <RemoveBricks>b__0(UnityEngine.Experimental.Rendering.ProbeBrickIndex.VoxelMeta lhs) { }

    }

    // TypeToken: 0x2000013  // size: 0x1C
    public sealed struct BrickChunkAlloc
    {
        // Fields
        public System.Int32 x;  // 0x10
        public System.Int32 y;  // 0x14
        public System.Int32 z;  // 0x18

        // Methods
        // RVA: 0x04DB8A00  token: 0x600004B
        private System.Int32 flattenIndex(System.Int32 sx, System.Int32 sy) { }

    }

    // TypeToken: 0x2000014  // size: 0x58
    public sealed struct DataLocation
    {
        // Fields
        private UnityEngine.Texture3D TexL0_L1rx;  // 0x10
        private UnityEngine.Texture3D TexL1_G_ry;  // 0x18
        private UnityEngine.Texture3D TexL1_B_rz;  // 0x20
        private UnityEngine.Texture3D TexL2_0;  // 0x28
        private UnityEngine.Texture3D TexL2_1;  // 0x30
        private UnityEngine.Texture3D TexL2_2;  // 0x38
        private UnityEngine.Texture3D TexL2_3;  // 0x40
        private System.Int32 width;  // 0x48
        private System.Int32 height;  // 0x4c
        private System.Int32 depth;  // 0x50

        // Methods
        // RVA: 0x0B29F69C  token: 0x600004C
        private System.Void Cleanup() { }

    }

    // TypeToken: 0x2000016  // size: 0x30
    public sealed struct IndexMetaData
    {
        // Fields
        private UnityEngine.Vector3Int minLocalIdx;  // 0x10
        private UnityEngine.Vector3Int maxLocalIdx;  // 0x1c
        private System.Int32 firstChunkIndex;  // 0x28
        private System.Int32 minSubdiv;  // 0x2c

        // Methods
        // RVA: 0x0B29F7C8  token: 0x6000059
        private System.Void Pack(System.UInt32[]& vals) { }

    }

    // TypeToken: 0x200001C  // size: 0x50
    public class Cell
    {
        // Fields
        public System.Int32 index;  // 0x10
        public UnityEngine.Vector3Int position;  // 0x14
        public System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickIndex.Brick> bricks;  // 0x20
        public UnityEngine.Vector3[] probePositions;  // 0x28
        public UnityEngine.Rendering.SphericalHarmonicsL2[] sh;  // 0x30
        public System.Single[] validity;  // 0x38
        public System.Int32 minSubdiv;  // 0x40
        public System.Int32 flatIdxInCellIndices;  // 0x44
        public System.Boolean loaded;  // 0x48

        // Methods
        // RVA: 0x04D8EC90  token: 0x60000AE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001D  // size: 0x18
    public class CellChunkInfo
    {
        // Fields
        public System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc> chunks;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60000AF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001E  // size: 0x30
    public class CellSortInfo : System.IComparable
    {
        // Fields
        private System.String sourceAsset;  // 0x10
        private UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Cell cell;  // 0x18
        private System.Single distanceToCamera;  // 0x20
        private UnityEngine.Vector3 position;  // 0x24

        // Methods
        // RVA: 0x0B29F61C  token: 0x60000B0
        public virtual System.Int32 CompareTo(System.Object obj) { }
        // RVA: 0x041E1670  token: 0x60000B1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001F  // size: 0x48
    public sealed struct Volume : System.IEquatable`1
    {
        // Fields
        private UnityEngine.Vector3 corner;  // 0x10
        private UnityEngine.Vector3 X;  // 0x1c
        private UnityEngine.Vector3 Y;  // 0x28
        private UnityEngine.Vector3 Z;  // 0x34
        private System.Single maxSubdivisionMultiplier;  // 0x40
        private System.Single minSubdivisionMultiplier;  // 0x44

        // Methods
        // RVA: 0x0B2B6B2C  token: 0x60000B2
        public System.Void .ctor(UnityEngine.Matrix4x4 trs, System.Single maxSubdivision, System.Single minSubdivision) { }
        // RVA: 0x04DB8C10  token: 0x60000B3
        public System.Void .ctor(UnityEngine.Vector3 corner, UnityEngine.Vector3 X, UnityEngine.Vector3 Y, UnityEngine.Vector3 Z, System.Single maxSubdivision, System.Single minSubdivision) { }
        // RVA: 0x04DB8BC0  token: 0x60000B4
        public System.Void .ctor(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Volume copy) { }
        // RVA: 0x0B2B6240  token: 0x60000B5
        public UnityEngine.Bounds CalculateAABB() { }
        // RVA: 0x0B2B6550  token: 0x60000B6
        public System.Void CalculateCenterAndSize(UnityEngine.Vector3& center, UnityEngine.Vector3& size) { }
        // RVA: 0x0B2B6A40  token: 0x60000B7
        public System.Void Transform(UnityEngine.Matrix4x4 trs) { }
        // RVA: 0x0B2B6874  token: 0x60000B8
        public virtual System.String ToString() { }
        // RVA: 0x0B2B66D0  token: 0x60000B9
        public virtual System.Boolean Equals(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Volume other) { }

    }

    // TypeToken: 0x2000020  // size: 0x70
    public sealed struct RefVolTransform
    {
        // Fields
        public UnityEngine.Matrix4x4 refSpaceToWS;  // 0x10
        public UnityEngine.Vector3 posWS;  // 0x50
        public UnityEngine.Quaternion rot;  // 0x5c
        public System.Single scale;  // 0x6c

    }

    // TypeToken: 0x2000021  // size: 0x58
    public sealed struct RuntimeResources
    {
        // Fields
        public UnityEngine.ComputeBuffer index;  // 0x10
        public UnityEngine.ComputeBuffer cellIndices;  // 0x18
        public UnityEngine.Texture3D L0_L1rx;  // 0x20
        public UnityEngine.Texture3D L1_G_ry;  // 0x28
        public UnityEngine.Texture3D L1_B_rz;  // 0x30
        public UnityEngine.Texture3D L2_0;  // 0x38
        public UnityEngine.Texture3D L2_1;  // 0x40
        public UnityEngine.Texture3D L2_2;  // 0x48
        public UnityEngine.Texture3D L2_3;  // 0x50

    }

    // TypeToken: 0x2000022  // size: 0x14
    public sealed struct RegId
    {
        // Fields
        private System.Int32 id;  // 0x10

        // Methods
        // RVA: 0x068B8E74  token: 0x60000BA
        public System.Boolean IsValid() { }
        // RVA: 0x04D910C0  token: 0x60000BB
        public System.Void Invalidate() { }
        // RVA: 0x07872540  token: 0x60000BC
        public static System.Boolean op_Equality(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId lhs, UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId rhs) { }
        // RVA: 0x0AAED910  token: 0x60000BD
        public static System.Boolean op_Inequality(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId lhs, UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId rhs) { }
        // RVA: 0x0B2AD4E4  token: 0x60000BE
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x02B77620  token: 0x60000BF
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000023  // size: 0x11
    public sealed struct ExtraDataActionInput
    {
    }

    // TypeToken: 0x2000024  // size: 0x28
    public sealed struct InitInfo
    {
        // Fields
        public UnityEngine.Vector3Int pendingMinCellPosition;  // 0x10
        public UnityEngine.Vector3Int pendingMaxCellPosition;  // 0x1c

    }

    // TypeToken: 0x2000025  // size: 0x40
    public class CellInstancedDebugProbes
    {
        // Fields
        public System.Collections.Generic.List<UnityEngine.Matrix4x4[]> probeBuffers;  // 0x10
        public System.Collections.Generic.List<UnityEngine.MaterialPropertyBlock> props;  // 0x18
        public UnityEngine.Hash128 cellHash;  // 0x20
        public UnityEngine.Vector3 cellPosition;  // 0x30

        // Methods
        // RVA: 0x041E1670  token: 0x60000C0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000026  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Experimental.Rendering.ProbeReferenceVolume.<>c <>9;  // static @ 0x0
        public static System.Func<System.Single> <>9__119_6;  // static @ 0x8
        public static System.Func<System.Single> <>9__119_15;  // static @ 0x10
        public static System.Func<System.Single> <>9__119_16;  // static @ 0x18
        public static System.Func<System.Single> <>9__119_21;  // static @ 0x20
        public static System.Func<System.Int32> <>9__119_24;  // static @ 0x28
        public static System.Func<System.Int32> <>9__119_25;  // static @ 0x30

        // Methods
        // RVA: 0x0B2B61DC  token: 0x60000C1
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60000C2
        public System.Void .ctor() { }
        // RVA: 0x04D879D0  token: 0x60000C3
        private System.Single <RegisterDebug>b__119_6() { }
        // RVA: 0x04DB8BB0  token: 0x60000C4
        private System.Single <RegisterDebug>b__119_15() { }
        // RVA: 0x04DA9C10  token: 0x60000C5
        private System.Single <RegisterDebug>b__119_16() { }
        // RVA: 0x04D879D0  token: 0x60000C6
        private System.Single <RegisterDebug>b__119_21() { }
        // RVA: 0x011EC580  token: 0x60000C7
        private System.Int32 <RegisterDebug>b__119_24() { }
        // RVA: 0x0B2B6128  token: 0x60000C8
        private System.Int32 <RegisterDebug>b__119_25() { }

    }

    // TypeToken: 0x200002A  // size: 0x14
    public sealed struct Version
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Experimental.Rendering.ProbeReferenceVolumeProfile.Version Initial;  // const

    }

    // TypeToken: 0x200002D  // size: 0x14
    public sealed struct AssetVersion
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Experimental.Rendering.ProbeVolumeAsset.AssetVersion First;  // const
        public static UnityEngine.Experimental.Rendering.ProbeVolumeAsset.AssetVersion AddProbeVolumesAtlasEncodingModes;  // const
        public static UnityEngine.Experimental.Rendering.ProbeVolumeAsset.AssetVersion PV2;  // const
        public static UnityEngine.Experimental.Rendering.ProbeVolumeAsset.AssetVersion ChunkBasedIndex;  // const
        public static UnityEngine.Experimental.Rendering.ProbeVolumeAsset.AssetVersion Max;  // const
        public static UnityEngine.Experimental.Rendering.ProbeVolumeAsset.AssetVersion Current;  // const

    }

    // TypeToken: 0x2000033  // size: 0x20
    public sealed struct SerializableAssetItem
    {
        // Fields
        public UnityEngine.Experimental.Rendering.ProbeVolumeState state;  // 0x10
        public UnityEngine.Experimental.Rendering.ProbeVolumeAsset asset;  // 0x18

    }

    // TypeToken: 0x2000035  // size: 0x30
    public sealed struct SerializableBoundItem
    {
        // Fields
        public System.String sceneGUID;  // 0x10
        public UnityEngine.Bounds bounds;  // 0x18

    }

    // TypeToken: 0x2000036  // size: 0x20
    public sealed struct SerializableHasPVItem
    {
        // Fields
        public System.String sceneGUID;  // 0x10
        public System.Boolean hasProbeVolumes;  // 0x18

    }

    // TypeToken: 0x2000037  // size: 0x20
    public sealed struct SerializablePVProfile
    {
        // Fields
        public System.String sceneGUID;  // 0x10
        public UnityEngine.Experimental.Rendering.ProbeReferenceVolumeProfile profile;  // 0x18

    }

    // TypeToken: 0x2000038  // size: 0x38
    public sealed struct SerializablePVBakeSettings
    {
        // Fields
        public System.String sceneGUID;  // 0x10
        public UnityEngine.Experimental.Rendering.ProbeVolumeBakingProcessSettings settings;  // 0x18

    }

    // TypeToken: 0x2000039  // size: 0x48
    public class BakingSet
    {
        // Fields
        public System.String name;  // 0x10
        public System.Collections.Generic.List<System.String> sceneGUIDs;  // 0x18
        public UnityEngine.Experimental.Rendering.ProbeVolumeBakingProcessSettings settings;  // 0x20
        public UnityEngine.Experimental.Rendering.ProbeReferenceVolumeProfile profile;  // 0x40

        // Methods
        // RVA: 0x0B29F550  token: 0x60000EE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003A  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.<>c <>9;  // static @ 0x0
        public static System.Func<UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.SerializablePVProfile,System.String> <>9__22_0;  // static @ 0x8

        // Methods
        // RVA: 0x0B2B6178  token: 0x60000EF
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60000F0
        public System.Void .ctor() { }
        // RVA: 0x02A55C30  token: 0x60000F1
        private System.String <UpdateBakingSets>b__22_0(UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.SerializablePVProfile s) { }

    }

    // TypeToken: 0x2000042  // size: 0x10
    public static class Strings
    {
        // Fields
        public static readonly UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip ClearRenderTargetsAtCreation;  // static @ 0x0
        public static readonly UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip DisablePassCulling;  // static @ 0x10
        public static readonly UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip ImmediateMode;  // static @ 0x20
        public static readonly UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip EnableLogging;  // static @ 0x30
        public static readonly UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip LogFrameInformation;  // static @ 0x40
        public static readonly UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip LogResources;  // static @ 0x50

        // Methods
        // RVA: 0x0B2B5E4C  token: 0x6000111
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000045  // size: 0x30
    public sealed struct PassDebugData
    {
        // Fields
        public System.String name;  // 0x10
        public System.Collections.Generic.List<System.Int32>[] resourceReadLists;  // 0x18
        public System.Collections.Generic.List<System.Int32>[] resourceWriteLists;  // 0x20
        public System.Boolean culled;  // 0x28
        public System.Boolean generateDebugData;  // 0x29

    }

    // TypeToken: 0x2000046  // size: 0x38
    public sealed struct ResourceDebugData
    {
        // Fields
        public System.String name;  // 0x10
        public System.Boolean imported;  // 0x18
        public System.Int32 creationPassIndex;  // 0x1c
        public System.Int32 releasePassIndex;  // 0x20
        public System.Collections.Generic.List<System.Int32> consumerList;  // 0x28
        public System.Collections.Generic.List<System.Int32> producerList;  // 0x30

    }

    // TypeToken: 0x2000048  // size: 0x28
    public sealed struct CompiledResourceInfo
    {
        // Fields
        public System.Collections.Generic.List<System.Int32> producers;  // 0x10
        public System.Collections.Generic.List<System.Int32> consumers;  // 0x18
        public System.Int32 refCount;  // 0x20
        public System.Boolean imported;  // 0x24

        // Methods
        // RVA: 0x0B2B7BB4  token: 0x6000165
        public System.Void Reset() { }

    }

    // TypeToken: 0x2000049  // size: 0x58
    public sealed struct CompiledPassInfo
    {
        // Fields
        public UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPass pass;  // 0x10
        public System.Collections.Generic.List<System.Int32>[] resourceCreateList;  // 0x18
        public System.Collections.Generic.List<System.Int32>[] resourceReleaseList;  // 0x20
        public System.Int32 refCount;  // 0x28
        public System.Boolean culled;  // 0x2c
        public System.Boolean hasSideEffect;  // 0x2d
        public System.Int32 syncToPassIndex;  // 0x30
        public System.Int32 syncFromPassIndex;  // 0x34
        public System.Boolean needGraphicsFence;  // 0x38
        public UnityEngine.Rendering.GraphicsFence fence;  // 0x40
        public System.Boolean enableAsyncCompute;  // 0x50

        // Properties
        System.Boolean allowPassCulling { get; /* RVA: 0x0B2B7B98 */ }

        // Methods
        // RVA: 0x0B2B79B0  token: 0x6000167
        public System.Void Reset(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPass pass) { }

    }

    // TypeToken: 0x200004A  // size: 0x18
    public class ProfilingScopePassData
    {
        // Fields
        public UnityEngine.Rendering.ProfilingSampler sampler;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000168
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004B  // size: 0x80
    public sealed class OnGraphRegisteredDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x068FE0AC  token: 0x6000169
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x600016A
        public virtual System.Void Invoke(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph graph) { }
        // RVA: 0x047FDD70  token: 0x600016B
        public virtual System.IAsyncResult BeginInvoke(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph graph, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x600016C
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200004C  // size: 0x80
    public sealed class OnExecutionRegisteredDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x09B3F6AC  token: 0x600016D
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x600016E
        public virtual System.Void Invoke(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph graph, System.String executionName) { }
        // RVA: 0x05E43E3C  token: 0x600016F
        public virtual System.IAsyncResult BeginInvoke(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph graph, System.String executionName, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000170
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200004D  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.<>c <>9;  // static @ 0x0
        public static UnityEngine.Experimental.Rendering.RenderGraphModule.RenderFunc<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.ProfilingScopePassData> <>9__61_0;  // static @ 0x8
        public static UnityEngine.Experimental.Rendering.RenderGraphModule.RenderFunc<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.ProfilingScopePassData> <>9__62_0;  // static @ 0x10

        // Methods
        // RVA: 0x0B2C7CD8  token: 0x6000171
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000172
        public System.Void .ctor() { }
        // RVA: 0x09B428A8  token: 0x6000173
        private System.Void <BeginProfilingSampler>b__61_0(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.ProfilingScopePassData data, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext ctx) { }
        // RVA: 0x09B428D8  token: 0x6000174
        private System.Void <EndProfilingSampler>b__62_0(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.ProfilingScopePassData data, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext ctx) { }

    }

    // TypeToken: 0x2000054  // size: 0x10
    public abstract class SharedObjectPoolBase
    {
        // Fields
        protected static System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphObjectPool.SharedObjectPoolBase> s_AllocatedPools;  // static @ 0x0

        // Methods
        // RVA: -1  // abstract  token: 0x60001B6
        protected virtual System.Void Clear() { }
        // RVA: 0x0B2C6560  token: 0x60001B7
        public static System.Void ClearAll() { }
        // RVA: 0x041E1670  token: 0x60001B8
        protected System.Void .ctor() { }
        // RVA: 0x0B2C6694  token: 0x60001B9
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000055
    public class SharedObjectPool`1 : SharedObjectPoolBase
    {
        // Fields
        private System.Collections.Generic.Stack<T> m_Pool;  // 0x0
        private static readonly System.Lazy<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphObjectPool.SharedObjectPool<T>> s_Instance;  // static @ 0x0

        // Properties
        UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphObjectPool.SharedObjectPool<T> sharedPool { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60001BA
        public T Get() { }
        // RVA: -1  // not resolved  token: 0x60001BB
        public System.Void Release(T value) { }
        // RVA: -1  // not resolved  token: 0x60001BC
        private static UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphObjectPool.SharedObjectPool<T> AllocatePool() { }
        // RVA: -1  // not resolved  token: 0x60001BD
        protected virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x60001BF
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60001C0
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200005F
    public sealed struct ResourceLogInfo
    {
        // Fields
        public System.String name;  // 0x0
        public System.Int64 size;  // 0x0

    }

    // TypeToken: 0x2000060
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourcePool.<>c<Type> <>9;  // static @ 0x0
        public static System.Comparison<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourcePool.ResourceLogInfo<Type>> <>9__17_0;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000217
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x6000218
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000219
        private System.Int32 <LogResources>b__17_0(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourcePool.ResourceLogInfo<Type> a, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourcePool.ResourceLogInfo<Type> b) { }

    }

    // TypeToken: 0x2000062  // size: 0x80
    public sealed class ResourceCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x086FC23C  token: 0x600024E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x600024F
        public virtual System.Void Invoke(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext rgContext, UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResource res) { }
        // RVA: 0x05E43E3C  token: 0x6000250
        public virtual System.IAsyncResult BeginInvoke(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext rgContext, UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResource res, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000251
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000063  // size: 0x38
    public class RenderGraphResourcesData
    {
        // Fields
        public UnityEngine.Rendering.DynamicArray<UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResource> resourceArray;  // 0x10
        public System.Int32 sharedResourcesCount;  // 0x18
        public UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResourcePool pool;  // 0x20
        public UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceRegistry.ResourceCallback createResourceCallback;  // 0x28
        public UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceRegistry.ResourceCallback releaseResourceCallback;  // 0x30

        // Methods
        // RVA: 0x0B2C5F6C  token: 0x6000252
        public System.Void Clear(System.Boolean onException, System.Int32 frameIndex) { }
        // RVA: 0x0B2C5EE8  token: 0x6000253
        public System.Void Cleanup() { }
        // RVA: 0x0B2C5FEC  token: 0x6000254
        public System.Void PurgeUnusedGraphicsResources(System.Int32 frameIndex) { }
        // RVA: -1  // generic def  token: 0x6000255
        public System.Int32 AddNewRenderGraphResource(ResType& outRes, System.Boolean pooledResource) { }
        // RVA: 0x0B2C6010  token: 0x6000256
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000073  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Rendering.CommandBufferPool.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x04D43C90  token: 0x60002A7
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60002A8
        public System.Void .ctor() { }
        // RVA: 0x03127EF0  token: 0x60002A9
        private System.Void <.cctor>b__4_0(UnityEngine.Rendering.CommandBuffer x) { }
        // RVA: 0x03128110  token: 0x60002AA
        private System.Void <.cctor>b__4_1(UnityEngine.Rendering.CommandBuffer x) { }

    }

    // TypeToken: 0x200007E  // size: 0x38
    public sealed struct FixedBufferStringQueue
    {
        // Fields
        private System.Byte* m_ReadCursor;  // 0x10
        private System.Byte* m_WriteCursor;  // 0x18
        private readonly System.Byte* m_BufferEnd;  // 0x20
        private readonly System.Byte* m_BufferStart;  // 0x28
        private readonly System.Int32 m_BufferLength;  // 0x30
        private System.Int32 <Count>k__BackingField;  // 0x34

        // Properties
        System.Int32 Count { get; /* RVA: 0x011F0020 */ set; /* RVA: 0x011F0030 */ }

        // Methods
        // RVA: 0x0B2C014C  token: 0x60002EC
        public System.Void .ctor(System.Byte* ptr, System.Int32 length) { }
        // RVA: 0x0B2C00AC  token: 0x60002ED
        public System.Boolean TryPush(System.String v) { }
        // RVA: 0x0B2C003C  token: 0x60002EE
        public System.Boolean TryPop(System.String& v) { }
        // RVA: 0x0B2C0018  token: 0x60002EF
        public System.Void Clear() { }

    }

    // TypeToken: 0x200007F
    public interface IKeyGetter`2
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60002F0
        public virtual TKey Get(TValue& v) { }

    }

    // TypeToken: 0x2000080  // size: 0x11
    public sealed struct DefaultKeyGetter`1 : IKeyGetter`2
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60002F1
        public virtual T Get(T& v) { }

    }

    // TypeToken: 0x2000081  // size: 0x11
    public sealed struct UintKeyGetter : IKeyGetter`2
    {
        // Methods
        // RVA: 0x04D94D20  token: 0x60002F2
        public virtual System.UInt32 Get(System.UInt32& v) { }

    }

    // TypeToken: 0x2000088  // size: 0x20
    public sealed struct ScalerContainer
    {
        // Fields
        public UnityEngine.Rendering.DynamicResScalePolicyType type;  // 0x10
        public UnityEngine.Rendering.PerformDynamicRes method;  // 0x18

    }

    // TypeToken: 0x2000089  // size: 0x14
    public sealed struct UpsamplerScheduleType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.DynamicResolutionHandler.UpsamplerScheduleType BeforePost;  // const
        public static UnityEngine.Rendering.DynamicResolutionHandler.UpsamplerScheduleType AfterPost;  // const

    }

    // TypeToken: 0x2000092
    public sealed struct PooledObject : System.IDisposable
    {
        // Fields
        private readonly T m_ToReturn;  // 0x0
        private readonly UnityEngine.Rendering.ObjectPool<T> m_Pool;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000348
        private System.Void .ctor(T value, UnityEngine.Rendering.ObjectPool<T> pool) { }
        // RVA: -1  // not resolved  token: 0x6000349
        private virtual System.Void System.IDisposable.Dispose() { }

    }

    // TypeToken: 0x2000096
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Rendering.ListPool.<>c<T> <>9;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000356
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x6000357
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000358
        private System.Void <.cctor>b__4_0(System.Collections.Generic.List<T> l) { }

    }

    // TypeToken: 0x2000098
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Rendering.HashSetPool.<>c<T> <>9;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600035D
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x600035E
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600035F
        private System.Void <.cctor>b__4_0(System.Collections.Generic.HashSet<T> l) { }

    }

    // TypeToken: 0x200009A
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Rendering.DictionaryPool.<>c<TKey,TValue> <>9;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000364
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x6000365
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000366
        private System.Void <.cctor>b__4_0(System.Collections.Generic.Dictionary<TKey,TValue> l) { }

    }

    // TypeToken: 0x20000A2  // size: 0x14
    public sealed struct StereoRenderingMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.XRGraphics.StereoRenderingMode MultiPass;  // const
        public static UnityEngine.Rendering.XRGraphics.StereoRenderingMode SinglePass;  // const
        public static UnityEngine.Rendering.XRGraphics.StereoRenderingMode SinglePassInstanced;  // const
        public static UnityEngine.Rendering.XRGraphics.StereoRenderingMode SinglePassMultiView;  // const

    }

    // TypeToken: 0x20000A6  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Rendering.DebugManager.<>c <>9;  // static @ 0x0
        public static System.Action<System.Boolean> <>9__61_0;  // static @ 0x8
        public static System.Action <>9__61_1;  // static @ 0x10

        // Methods
        // RVA: 0x04D41B90  token: 0x60003CF
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60003D0
        public System.Void .ctor() { }
        // RVA: 0x041E1670  token: 0x60003D1
        private System.Void <.ctor>b__61_0(System.Boolean <p0>) { }
        // RVA: 0x041E1670  token: 0x60003D2
        private System.Void <.ctor>b__61_1() { }
        // RVA: 0x0484F270  token: 0x60003D3
        private UnityEngine.Rendering.DebugManager <.cctor>b__81_0() { }

    }

    // TypeToken: 0x20000A9  // size: 0x14
    public sealed struct DebugActionKeyType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.DebugActionState.DebugActionKeyType Button;  // const
        public static UnityEngine.Rendering.DebugActionState.DebugActionKeyType Axis;  // const
        public static UnityEngine.Rendering.DebugActionState.DebugActionKeyType Key;  // const

    }

    // TypeToken: 0x20000AD  // size: 0x50
    public class Container : Widget, IContainer
    {
        // Fields
        private UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> <children>k__BackingField;  // 0x48

        // Properties
        UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> children { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }
        UnityEngine.Rendering.DebugUI.Panel panel { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x0B2B8EBC */ }

        // Methods
        // RVA: 0x0B2B8C48  token: 0x60003F0
        public System.Void .ctor() { }
        // RVA: 0x0B2B8DB0  token: 0x60003F1
        public System.Void .ctor(System.String displayName, UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> children) { }
        // RVA: 0x0B2B88F4  token: 0x60003F2
        private virtual System.Void GenerateQueryPath() { }
        // RVA: 0x0B2B8B80  token: 0x60003F3
        protected virtual System.Void OnItemAdded(UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> sender, UnityEngine.Rendering.ListChangedEventArgs<UnityEngine.Rendering.DebugUI.Widget> e) { }
        // RVA: 0x0B2B8BE4  token: 0x60003F4
        protected virtual System.Void OnItemRemoved(UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> sender, UnityEngine.Rendering.ListChangedEventArgs<UnityEngine.Rendering.DebugUI.Widget> e) { }
        // RVA: 0x0B2B8A1C  token: 0x60003F5
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x20000AF  // size: 0x20
    public sealed struct ContextMenuItem
    {
        // Fields
        public System.String displayName;  // 0x10
        public System.Action action;  // 0x18

    }

    // TypeToken: 0x20000AE  // size: 0x70
    public class Foldout : Container, IValueField
    {
        // Fields
        public System.Boolean opened;  // 0x50
        public System.Boolean isHeader;  // 0x51
        public System.Collections.Generic.List<UnityEngine.Rendering.DebugUI.Foldout.ContextMenuItem> contextMenuItems;  // 0x58
        private System.String[] <columnLabels>k__BackingField;  // 0x60
        private System.String[] <columnTooltips>k__BackingField;  // 0x68

        // Properties
        System.Boolean isReadOnly { get; /* RVA: 0x012081B0 */ }
        System.String[] columnLabels { get; /* RVA: 0x04D86280 */ set; /* RVA: 0x063CC164 */ }
        System.String[] columnTooltips { get; /* RVA: 0x04D86230 */ set; /* RVA: 0x0485F170 */ }

        // Methods
        // RVA: 0x0B2C0290  token: 0x60003FB
        public System.Void .ctor() { }
        // RVA: 0x0B2C0298  token: 0x60003FC
        public System.Void .ctor(System.String displayName, UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> children, System.String[] columnLabels, System.String[] columnTooltips) { }
        // RVA: 0x04D86800  token: 0x60003FD
        public System.Boolean GetValue() { }
        // RVA: 0x0B2C024C  token: 0x60003FE
        private virtual System.Object UnityEngine.Rendering.DebugUI.IValueField.GetValue() { }
        // RVA: 0x0B2C0200  token: 0x60003FF
        public virtual System.Void SetValue(System.Object value) { }
        // RVA: 0x04D88C30  token: 0x6000400
        public virtual System.Object ValidateValue(System.Object value) { }
        // RVA: 0x04D86840  token: 0x6000401
        public System.Void SetValue(System.Boolean value) { }

    }

    // TypeToken: 0x20000B0  // size: 0x50
    public class HBox : Container
    {
        // Methods
        // RVA: 0x0B2C118C  token: 0x6000402
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000B1  // size: 0x50
    public class VBox : Container
    {
        // Methods
        // RVA: 0x0B2C7EA0  token: 0x6000403
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000B3  // size: 0x70
    public class Row : Foldout
    {
        // Methods
        // RVA: 0x0B2C63B8  token: 0x600040A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000B2  // size: 0x60
    public class Table : Container
    {
        // Fields
        public System.Boolean isReadOnly;  // 0x50
        private System.Boolean[] m_Header;  // 0x58

        // Properties
        System.Boolean[] VisibleColumns { get; /* RVA: 0x0B2C683C */ }

        // Methods
        // RVA: 0x0B2C67F4  token: 0x6000404
        public System.Void .ctor() { }
        // RVA: 0x0B2C67B0  token: 0x6000405
        public System.Void SetColumnVisibility(System.Int32 index, System.Boolean visible) { }
        // RVA: 0x0B2C6720  token: 0x6000406
        public System.Boolean GetColumnVisibility(System.Int32 index) { }
        // RVA: 0x0B2C6760  token: 0x6000408
        protected virtual System.Void OnItemAdded(UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> sender, UnityEngine.Rendering.ListChangedEventArgs<UnityEngine.Rendering.DebugUI.Widget> e) { }
        // RVA: 0x0B2C6788  token: 0x6000409
        protected virtual System.Void OnItemRemoved(UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> sender, UnityEngine.Rendering.ListChangedEventArgs<UnityEngine.Rendering.DebugUI.Widget> e) { }

    }

    // TypeToken: 0x20000B4  // size: 0x14
    public sealed struct Flags
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.DebugUI.Flags None;  // const
        public static UnityEngine.Rendering.DebugUI.Flags EditorOnly;  // const
        public static UnityEngine.Rendering.DebugUI.Flags RuntimeOnly;  // const
        public static UnityEngine.Rendering.DebugUI.Flags EditorForceUpdate;  // const

    }

    // TypeToken: 0x20000B6  // size: 0x20
    public sealed struct NameAndTooltip
    {
        // Fields
        public System.String name;  // 0x10
        public System.String tooltip;  // 0x18

    }

    // TypeToken: 0x20000B5  // size: 0x48
    public abstract class Widget
    {
        // Fields
        protected UnityEngine.Rendering.DebugUI.Panel m_Panel;  // 0x10
        protected UnityEngine.Rendering.DebugUI.IContainer m_Parent;  // 0x18
        private UnityEngine.Rendering.DebugUI.Flags <flags>k__BackingField;  // 0x20
        private System.String <displayName>k__BackingField;  // 0x28
        private System.String <tooltip>k__BackingField;  // 0x30
        private System.String <queryPath>k__BackingField;  // 0x38
        public System.Func<System.Boolean> isHiddenCallback;  // 0x40

        // Properties
        UnityEngine.Rendering.DebugUI.Panel panel { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        UnityEngine.Rendering.DebugUI.IContainer parent { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        UnityEngine.Rendering.DebugUI.Flags flags { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }
        System.String displayName { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        System.String tooltip { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        System.String queryPath { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }
        System.Boolean isEditorOnly { get; /* RVA: 0x0B2C802C */ }
        System.Boolean isRuntimeOnly { get; /* RVA: 0x0B2C8070 */ }
        System.Boolean isInactiveInEditor { get; /* RVA: 0x0B2C8054 */ }
        System.Boolean isHidden { get; /* RVA: 0x0B2C8034 */ }
        UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip nameAndTooltip { set; /* RVA: 0x0B2C807C */ }

        // Methods
        // RVA: 0x0B2C7F50  token: 0x600041B
        private virtual System.Void GenerateQueryPath() { }
        // RVA: 0x0B2C8008  token: 0x600041C
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x041E1670  token: 0x600041E
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000B7
    public interface IContainer
    {
        // Properties
        UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> children { get; /* RVA: -1  // abstract */ }
        System.String displayName { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.String queryPath { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x20000B8
    public interface IValueField
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000423
        public virtual System.Object GetValue() { }
        // RVA: -1  // abstract  token: 0x6000424
        public virtual System.Void SetValue(System.Object value) { }
        // RVA: -1  // abstract  token: 0x6000425
        public virtual System.Object ValidateValue(System.Object value) { }

    }

    // TypeToken: 0x20000B9  // size: 0x50
    public class Button : Widget
    {
        // Fields
        private System.Action <action>k__BackingField;  // 0x48

        // Properties
        System.Action action { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000428
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BA  // size: 0x58
    public class Value : Widget
    {
        // Fields
        private System.Func<System.Object> <getter>k__BackingField;  // 0x48
        public System.Single refreshRate;  // 0x50

        // Properties
        System.Func<System.Object> getter { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }

        // Methods
        // RVA: 0x0B2C7F08  token: 0x600042B
        public System.Void .ctor() { }
        // RVA: 0x0B2C7EE8  token: 0x600042C
        public System.Object GetValue() { }

    }

    // TypeToken: 0x20000BB
    public abstract class Field`1 : Widget, IValueField
    {
        // Fields
        private System.Func<T> <getter>k__BackingField;  // 0x0
        private System.Action<T> <setter>k__BackingField;  // 0x0
        public System.Action<UnityEngine.Rendering.DebugUI.Field<T>,T> onValueChanged;  // 0x0

        // Properties
        System.Func<T> getter { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Action<T> setter { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000431
        private virtual System.Object UnityEngine.Rendering.DebugUI.IValueField.ValidateValue(System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000432
        public virtual T ValidateValue(T value) { }
        // RVA: -1  // not resolved  token: 0x6000433
        private virtual System.Object UnityEngine.Rendering.DebugUI.IValueField.GetValue() { }
        // RVA: -1  // not resolved  token: 0x6000434
        public T GetValue() { }
        // RVA: -1  // not resolved  token: 0x6000435
        public virtual System.Void SetValue(System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000436
        public System.Void SetValue(T value) { }
        // RVA: -1  // not resolved  token: 0x6000437
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000BC  // size: 0x60
    public class BoolField : Field`1
    {
        // Methods
        // RVA: 0x0B2B6F64  token: 0x6000438
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BD  // size: 0x68
    public class HistoryBoolField : BoolField
    {
        // Fields
        private System.Func<System.Boolean>[] <historyGetter>k__BackingField;  // 0x60

        // Properties
        System.Func<System.Boolean>[] historyGetter { get; /* RVA: 0x04D86280 */ set; /* RVA: 0x063CC164 */ }
        System.Int32 historyDepth { get; /* RVA: 0x0B2C1214 */ }

        // Methods
        // RVA: 0x0B2C11D4  token: 0x600043C
        public System.Boolean GetHistoryValue(System.Int32 historyIndex) { }
        // RVA: 0x0B2C120C  token: 0x600043D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BE  // size: 0x78
    public class IntField : Field`1
    {
        // Fields
        public System.Func<System.Int32> min;  // 0x60
        public System.Func<System.Int32> max;  // 0x68
        public System.Int32 incStep;  // 0x70
        public System.Int32 intStepMult;  // 0x74

        // Methods
        // RVA: 0x0B2C12E0  token: 0x600043E
        public virtual System.Int32 ValidateValue(System.Int32 value) { }
        // RVA: 0x0B2C1330  token: 0x600043F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BF  // size: 0x78
    public class UIntField : Field`1
    {
        // Fields
        public System.Func<System.UInt32> min;  // 0x60
        public System.Func<System.UInt32> max;  // 0x68
        public System.UInt32 incStep;  // 0x70
        public System.UInt32 intStepMult;  // 0x74

        // Methods
        // RVA: 0x0B2C7E04  token: 0x6000440
        public virtual System.UInt32 ValidateValue(System.UInt32 value) { }
        // RVA: 0x0B2C7E54  token: 0x6000441
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C0  // size: 0x80
    public class FloatField : Field`1
    {
        // Fields
        public System.Func<System.Single> min;  // 0x60
        public System.Func<System.Single> max;  // 0x68
        public System.Single incStep;  // 0x70
        public System.Single incStepMult;  // 0x74
        public System.Int32 decimals;  // 0x78

        // Methods
        // RVA: 0x0B2C0174  token: 0x6000442
        public virtual System.Single ValidateValue(System.Single value) { }
        // RVA: 0x0B2C01C0  token: 0x6000443
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C2  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Rendering.DebugUI.EnumUtility.<>c <>9;  // static @ 0x0
        public static System.Func<System.Reflection.FieldInfo,UnityEngine.GUIContent> <>9__0_0;  // static @ 0x8

        // Methods
        // RVA: 0x0B2C7D3C  token: 0x6000446
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000447
        public System.Void .ctor() { }
        // RVA: 0x0B2C7B68  token: 0x6000448
        private UnityEngine.GUIContent <MakeEnumNames>b__0_0(System.Reflection.FieldInfo fieldInfo) { }

    }

    // TypeToken: 0x20000C1  // size: 0x10
    public static class EnumUtility
    {
        // Methods
        // RVA: 0x0B2BFDF8  token: 0x6000444
        private static UnityEngine.GUIContent[] MakeEnumNames(System.Type enumType) { }
        // RVA: 0x0B2BFF34  token: 0x6000445
        private static System.Int32[] MakeEnumValues(System.Type enumType) { }

    }

    // TypeToken: 0x20000C4  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Rendering.DebugUI.EnumField.<>c <>9;  // static @ 0x0
        public static System.Func<UnityEngine.GUIContent,System.String> <>9__17_0;  // static @ 0x8

        // Methods
        // RVA: 0x0B2C7DA0  token: 0x6000453
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000454
        public System.Void .ctor() { }
        // RVA: 0x0B2C7AF4  token: 0x6000455
        private System.String <InitQuickSeparators>b__17_0(UnityEngine.GUIContent x) { }

    }

    // TypeToken: 0x20000C3  // size: 0x90
    public class EnumField : Field`1
    {
        // Fields
        public UnityEngine.GUIContent[] enumNames;  // 0x60
        public System.Int32[] enumValues;  // 0x68
        private System.Int32[] quickSeparators;  // 0x70
        private System.Int32[] indexes;  // 0x78
        private System.Func<System.Int32> <getIndex>k__BackingField;  // 0x80
        private System.Action<System.Int32> <setIndex>k__BackingField;  // 0x88

        // Properties
        System.Func<System.Int32> getIndex { get; /* RVA: 0x04D86260 */ set; /* RVA: 0x04CD9970 */ }
        System.Action<System.Int32> setIndex { get; /* RVA: 0x04D861F0 */ set; /* RVA: 0x05395D30 */ }
        System.Int32 currentIndex { get; /* RVA: 0x0B2BFD54 */ set; /* RVA: 0x0B2BFDD4 */ }
        System.Type autoEnum { set; /* RVA: 0x0B2BFD78 */ }

        // Methods
        // RVA: 0x0B2BFB18  token: 0x6000450
        private System.Void InitQuickSeparators() { }
        // RVA: 0x0B2BFA60  token: 0x6000451
        private System.Void InitIndexes() { }
        // RVA: 0x0B2BFD2C  token: 0x6000452
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C5  // size: 0x98
    public class HistoryEnumField : EnumField
    {
        // Fields
        private System.Func<System.Int32>[] <historyIndexGetter>k__BackingField;  // 0x90

        // Properties
        System.Func<System.Int32>[] historyIndexGetter { get; /* RVA: 0x04D86220 */ set; /* RVA: 0x06402330 */ }
        System.Int32 historyDepth { get; /* RVA: 0x0B2C1268 */ }

        // Methods
        // RVA: 0x0B2C1224  token: 0x6000459
        public System.Int32 GetHistoryValue(System.Int32 historyIndex) { }
        // RVA: 0x0B2C1260  token: 0x600045A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C6  // size: 0x78
    public class BitField : Field`1
    {
        // Fields
        private UnityEngine.GUIContent[] <enumNames>k__BackingField;  // 0x60
        private System.Int32[] <enumValues>k__BackingField;  // 0x68
        private System.Type m_EnumType;  // 0x70

        // Properties
        UnityEngine.GUIContent[] enumNames { get; /* RVA: 0x04D86280 */ set; /* RVA: 0x063CC164 */ }
        System.Int32[] enumValues { get; /* RVA: 0x04D86230 */ set; /* RVA: 0x0485F170 */ }
        System.Type enumType { get; /* RVA: 0x04D862B0 */ set; /* RVA: 0x0B2B6F0C */ }

        // Methods
        // RVA: 0x0B2B6EE4  token: 0x6000461
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C7  // size: 0x70
    public class ColorField : Field`1
    {
        // Fields
        public System.Boolean hdr;  // 0x60
        public System.Boolean showAlpha;  // 0x61
        public System.Boolean showPicker;  // 0x62
        public System.Single incStep;  // 0x64
        public System.Single incStepMult;  // 0x68
        public System.Int32 decimals;  // 0x6c

        // Methods
        // RVA: 0x0B2B7870  token: 0x6000462
        public virtual UnityEngine.Color ValidateValue(UnityEngine.Color value) { }
        // RVA: 0x0B2B78CC  token: 0x6000463
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C8  // size: 0x70
    public class Vector2Field : Field`1
    {
        // Fields
        public System.Single incStep;  // 0x60
        public System.Single incStepMult;  // 0x64
        public System.Int32 decimals;  // 0x68

        // Methods
        // RVA: 0x0B2D8D40  token: 0x6000464
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C9  // size: 0x70
    public class Vector3Field : Field`1
    {
        // Fields
        public System.Single incStep;  // 0x60
        public System.Single incStepMult;  // 0x64
        public System.Int32 decimals;  // 0x68

        // Methods
        // RVA: 0x0B2D8D80  token: 0x6000465
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CA  // size: 0x70
    public class Vector4Field : Field`1
    {
        // Fields
        public System.Single incStep;  // 0x60
        public System.Single incStepMult;  // 0x64
        public System.Int32 decimals;  // 0x68

        // Methods
        // RVA: 0x0B2D8DC0  token: 0x6000466
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CC  // size: 0x14
    public sealed struct Style
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.DebugUI.MessageBox.Style Info;  // const
        public static UnityEngine.Rendering.DebugUI.MessageBox.Style Warning;  // const
        public static UnityEngine.Rendering.DebugUI.MessageBox.Style Error;  // const

    }

    // TypeToken: 0x20000CB  // size: 0x50
    public class MessageBox : Widget
    {
        // Fields
        public UnityEngine.Rendering.DebugUI.MessageBox.Style style;  // 0x48

        // Methods
        // RVA: 0x041E1670  token: 0x6000467
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CE  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Rendering.DebugUI.Panel.<>c <>9;  // static @ 0x0
        public static System.Action<UnityEngine.Rendering.DebugUI.Panel> <>9__29_0;  // static @ 0x8

        // Methods
        // RVA: 0x0B2D8C78  token: 0x600047D
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600047E
        public System.Void .ctor() { }
        // RVA: 0x041E1670  token: 0x600047F
        private System.Void <.ctor>b__29_0(UnityEngine.Rendering.DebugUI.Panel <p0>) { }

    }

    // TypeToken: 0x20000CD  // size: 0x38
    public class Panel : IContainer, System.IComparable`1
    {
        // Fields
        private UnityEngine.Rendering.DebugUI.Flags <flags>k__BackingField;  // 0x10
        private System.String <displayName>k__BackingField;  // 0x18
        private System.Int32 <groupIndex>k__BackingField;  // 0x20
        private UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> <children>k__BackingField;  // 0x28
        private System.Action<UnityEngine.Rendering.DebugUI.Panel> onSetDirty;  // 0x30

        // Properties
        UnityEngine.Rendering.DebugUI.Flags flags { get; /* RVA: 0x02B2E2D0 */ set; /* RVA: 0x014F51F0 */ }
        System.String displayName { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Int32 groupIndex { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }
        System.String queryPath { get; /* RVA: 0x0385B100 */ }
        System.Boolean isEditorOnly { get; /* RVA: 0x02F2E070 */ }
        System.Boolean isRuntimeOnly { get; /* RVA: 0x0B2D1C94 */ }
        System.Boolean isInactiveInEditor { get; /* RVA: 0x0B2D1C78 */ }
        System.Boolean editorForceUpdate { get; /* RVA: 0x0B2D1C70 */ }
        UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> children { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }

        // Events
        event System.Action<UnityEngine.Rendering.DebugUI.Panel> onSetDirty;

        // Methods
        // RVA: 0x0B2D19D4  token: 0x6000477
        public System.Void .ctor() { }
        // RVA: 0x0B2D17A0  token: 0x6000478
        protected virtual System.Void OnItemAdded(UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> sender, UnityEngine.Rendering.ListChangedEventArgs<UnityEngine.Rendering.DebugUI.Widget> e) { }
        // RVA: 0x0B2D17F8  token: 0x6000479
        protected virtual System.Void OnItemRemoved(UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> sender, UnityEngine.Rendering.ListChangedEventArgs<UnityEngine.Rendering.DebugUI.Widget> e) { }
        // RVA: 0x0B2D1854  token: 0x600047A
        public System.Void SetDirty() { }
        // RVA: 0x0B2D163C  token: 0x600047B
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B2D19A8  token: 0x600047C
        private virtual System.Int32 System.IComparable<UnityEngine.Rendering.DebugUI.Panel>.CompareTo(UnityEngine.Rendering.DebugUI.Panel other) { }

    }

    // TypeToken: 0x20000D0  // size: 0x28
    public sealed class <DoAfterInputModuleUpdated>d__9 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.Action action;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600048E
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600048F
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B2D8A10  token: 0x6000490
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B2D8AC0  token: 0x6000492
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000D1  // size: 0x20
    public sealed class <RefreshRuntimeUINextFrame>d__15 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000494
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000495
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B2D8B0C  token: 0x6000496
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B2D8B90  token: 0x6000498
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000DF  // size: 0x40
    public sealed struct SunSourceDirLightOverrideLensFlareData
    {
        // Fields
        public System.Boolean enable;  // 0x10
        public UnityEngine.Rendering.LensFlareDataSRP lensFlareData;  // 0x18
        public System.Single intensity;  // 0x20
        public System.Single scale;  // 0x24
        public System.Boolean useOcclusion;  // 0x28
        public System.Single occlusionRadius;  // 0x2c
        public System.UInt32 sampleCount;  // 0x30
        public System.Single occlusionOffset;  // 0x34
        public System.Boolean allowOffScreen;  // 0x38

    }

    // TypeToken: 0x20000E0  // size: 0x3C
    public sealed struct SunSourceDirLightOverrideLightData
    {
        // Fields
        public UnityEngine.Quaternion rotationLensFlare;  // 0x10
        public UnityEngine.Vector3 dirLightFoward;  // 0x20
        public UnityEngine.Color color;  // 0x2c

    }

    // TypeToken: 0x20000E1  // size: 0x78
    public sealed struct SunSourceDirLightOverrideInfo
    {
        // Fields
        public UnityEngine.GameObject dirLightGameObject;  // 0x10
        public UnityEngine.Rendering.LensFlareCommonSRP.SunSourceDirLightOverrideLensFlareData flareData;  // 0x18
        public UnityEngine.Rendering.LensFlareCommonSRP.SunSourceDirLightOverrideLightData lightData;  // 0x48

    }

    // TypeToken: 0x20000E2  // size: 0x18
    public sealed struct <>c__DisplayClass35_0
    {
        // Fields
        public UnityEngine.Rendering.LensFlareDataElementSRP element;  // 0x10

    }

    // TypeToken: 0x20000E3  // size: 0x2C
    public sealed struct <>c__DisplayClass35_1
    {
        // Fields
        public UnityEngine.Vector2 screenPos;  // 0x10
        public System.Single position;  // 0x18
        public System.Single globalCos0;  // 0x1c
        public System.Single globalSin0;  // 0x20
        public System.Single combinedScale;  // 0x24
        public System.Single usedAspectRatio;  // 0x28

    }

    // TypeToken: 0x20000F5  // size: 0x110
    public sealed struct <hammersley2dSeq16>e__FixedBuffer
    {
        // Fields
        public System.Single FixedElementField;  // 0x10

    }

    // TypeToken: 0x20000F4  // size: 0x110
    public sealed struct Hammersley2dSeq16
    {
        // Fields
        public UnityEngine.Rendering.Hammersley.Hammersley2dSeq16.<hammersley2dSeq16>e__FixedBuffer hammersley2dSeq16;  // 0x10

    }

    // TypeToken: 0x20000F7  // size: 0x210
    public sealed struct <hammersley2dSeq32>e__FixedBuffer
    {
        // Fields
        public System.Single FixedElementField;  // 0x10

    }

    // TypeToken: 0x20000F6  // size: 0x210
    public sealed struct Hammersley2dSeq32
    {
        // Fields
        public UnityEngine.Rendering.Hammersley.Hammersley2dSeq32.<hammersley2dSeq32>e__FixedBuffer hammersley2dSeq32;  // 0x10

    }

    // TypeToken: 0x20000F9  // size: 0x410
    public sealed struct <hammersley2dSeq64>e__FixedBuffer
    {
        // Fields
        public System.Single FixedElementField;  // 0x10

    }

    // TypeToken: 0x20000F8  // size: 0x410
    public sealed struct Hammersley2dSeq64
    {
        // Fields
        public UnityEngine.Rendering.Hammersley.Hammersley2dSeq64.<hammersley2dSeq64>e__FixedBuffer hammersley2dSeq64;  // 0x10

    }

    // TypeToken: 0x20000FB  // size: 0x1010
    public sealed struct <hammersley2dSeq256>e__FixedBuffer
    {
        // Fields
        public System.Single FixedElementField;  // 0x10

    }

    // TypeToken: 0x20000FA  // size: 0x1010
    public sealed struct Hammersley2dSeq256
    {
        // Fields
        public UnityEngine.Rendering.Hammersley.Hammersley2dSeq256.<hammersley2dSeq256>e__FixedBuffer hammersley2dSeq256;  // 0x10

    }

    // TypeToken: 0x2000100  // size: 0x14
    public sealed struct BlitType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.PowerOfTwoTextureAtlas.BlitType Padding;  // const
        public static UnityEngine.Rendering.PowerOfTwoTextureAtlas.BlitType PaddingMultiply;  // const
        public static UnityEngine.Rendering.PowerOfTwoTextureAtlas.BlitType OctahedralPadding;  // const
        public static UnityEngine.Rendering.PowerOfTwoTextureAtlas.BlitType OctahedralPaddingMultiply;  // const

    }

    // TypeToken: 0x2000101  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Rendering.PowerOfTwoTextureAtlas.<>c <>9;  // static @ 0x0
        public static System.Comparison<System.ValueTuple<System.Int32,UnityEngine.Vector2Int>> <>9__23_0;  // static @ 0x8

        // Methods
        // RVA: 0x0B2D8CDC  token: 0x6000539
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600053A
        public System.Void .ctor() { }
        // RVA: 0x0B2D8BDC  token: 0x600053B
        private System.Int32 <RelayoutEntries>b__23_0(System.ValueTuple<System.Int32,UnityEngine.Vector2Int> c1, System.ValueTuple<System.Int32,UnityEngine.Vector2Int> c2) { }

    }

    // TypeToken: 0x2000106  // size: 0x30
    public class AtlasNode
    {
        // Fields
        public UnityEngine.Rendering.AtlasAllocator.AtlasNode m_RightChild;  // 0x10
        public UnityEngine.Rendering.AtlasAllocator.AtlasNode m_BottomChild;  // 0x18
        public UnityEngine.Vector4 m_Rect;  // 0x20

        // Methods
        // RVA: 0x0B2C9070  token: 0x6000562
        public UnityEngine.Rendering.AtlasAllocator.AtlasNode Allocate(UnityEngine.Rendering.ObjectPool<UnityEngine.Rendering.AtlasAllocator.AtlasNode>& pool, System.Int32 width, System.Int32 height, System.Boolean powerOfTwoPadding) { }
        // RVA: 0x0B2C99C8  token: 0x6000563
        public System.Void Release(UnityEngine.Rendering.ObjectPool<UnityEngine.Rendering.AtlasAllocator.AtlasNode>& pool) { }
        // RVA: 0x04DB8D50  token: 0x6000564
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000107  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Rendering.AtlasAllocator.<>c <>9;  // static @ 0x0
        public static UnityEngine.Events.UnityAction<UnityEngine.Rendering.AtlasAllocator.AtlasNode> <>9__6_0;  // static @ 0x8
        public static UnityEngine.Events.UnityAction<UnityEngine.Rendering.AtlasAllocator.AtlasNode> <>9__6_1;  // static @ 0x10

        // Methods
        // RVA: 0x0B2D8C14  token: 0x6000565
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000566
        public System.Void .ctor() { }
        // RVA: 0x041E1670  token: 0x6000567
        private System.Void <.ctor>b__6_0(UnityEngine.Rendering.AtlasAllocator.AtlasNode _) { }
        // RVA: 0x041E1670  token: 0x6000568
        private System.Void <.ctor>b__6_1(UnityEngine.Rendering.AtlasAllocator.AtlasNode _) { }

    }

    // TypeToken: 0x2000109  // size: 0x14
    public sealed struct BlitType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.Texture2DAtlas.BlitType Default;  // const
        public static UnityEngine.Rendering.Texture2DAtlas.BlitType CubeTo2DOctahedral;  // const
        public static UnityEngine.Rendering.Texture2DAtlas.BlitType SingleChannel;  // const
        public static UnityEngine.Rendering.Texture2DAtlas.BlitType CubeTo2DOctahedralSingleChannel;  // const

    }

    // TypeToken: 0x200010B  // size: 0x20
    public class AtlasNodePool
    {
        // Fields
        private UnityEngine.Rendering.AtlasAllocatorDynamic.AtlasNode[] m_Nodes;  // 0x10
        private System.Int16 m_Next;  // 0x18
        private System.Int16 m_FreelistHead;  // 0x1a

        // Methods
        // RVA: 0x0B2C9008  token: 0x600058F
        public System.Void .ctor(System.Int16 capacity) { }
        // RVA: 0x0B2C8FF0  token: 0x6000590
        public System.Void Dispose() { }
        // RVA: 0x04DB8D20  token: 0x6000591
        public System.Void Clear() { }
        // RVA: 0x0B2C8EA4  token: 0x6000592
        public System.Int16 AtlasNodeCreate(System.Int16 parent) { }
        // RVA: 0x0B2C8FA4  token: 0x6000593
        public System.Void AtlasNodeFree(System.Int16 index) { }

    }

    // TypeToken: 0x200010D  // size: 0x14
    public sealed struct AtlasNodeFlags
    {
        // Fields
        public System.UInt32 value__;  // 0x10
        public static UnityEngine.Rendering.AtlasAllocatorDynamic.AtlasNode.AtlasNodeFlags IsOccupied;  // const

    }

    // TypeToken: 0x200010C  // size: 0x30
    public sealed struct AtlasNode
    {
        // Fields
        public System.Int16 m_Self;  // 0x10
        public System.Int16 m_Parent;  // 0x12
        public System.Int16 m_LeftChild;  // 0x14
        public System.Int16 m_RightChild;  // 0x16
        public System.Int16 m_FreelistNext;  // 0x18
        public System.UInt16 m_Flags;  // 0x1a
        public UnityEngine.Vector4 m_Rect;  // 0x20

        // Methods
        // RVA: 0x0B2C9A90  token: 0x6000594
        public System.Void .ctor(System.Int16 self, System.Int16 parent) { }
        // RVA: 0x0B2C9868  token: 0x6000595
        public System.Boolean IsOccupied() { }
        // RVA: 0x04DB8D40  token: 0x6000596
        public System.Void SetIsOccupied() { }
        // RVA: 0x04DB8D30  token: 0x6000597
        public System.Void ClearIsOccupied() { }
        // RVA: 0x0B2C97C8  token: 0x6000598
        public System.Boolean IsLeafNode() { }
        // RVA: 0x0B2C93D0  token: 0x6000599
        public System.Int16 Allocate(UnityEngine.Rendering.AtlasAllocatorDynamic.AtlasNodePool pool, System.Int32 width, System.Int32 height) { }
        // RVA: 0x0B2C9924  token: 0x600059A
        public System.Void ReleaseChildren(UnityEngine.Rendering.AtlasAllocatorDynamic.AtlasNodePool pool) { }
        // RVA: 0x0B2C9870  token: 0x600059B
        public System.Void ReleaseAndMerge(UnityEngine.Rendering.AtlasAllocatorDynamic.AtlasNodePool pool) { }
        // RVA: 0x0B2C97D4  token: 0x600059C
        public System.Boolean IsMergeNeeded(UnityEngine.Rendering.AtlasAllocatorDynamic.AtlasNodePool pool) { }

    }

    // TypeToken: 0x200011A  // size: 0x10
    public static class BlitShaderIDs
    {
        // Fields
        public static readonly System.Int32 _BlitTexture;  // static @ 0x0
        public static readonly System.Int32 _BlitCubeTexture;  // static @ 0x4
        public static readonly System.Int32 _BlitScaleBias;  // static @ 0x8
        public static readonly System.Int32 _BlitScaleBiasRt;  // static @ 0xc
        public static readonly System.Int32 _BlitMipLevel;  // static @ 0x10
        public static readonly System.Int32 _BlitTextureSize;  // static @ 0x14
        public static readonly System.Int32 _BlitPaddingSize;  // static @ 0x18
        public static readonly System.Int32 _InputDepth;  // static @ 0x1c

        // Methods
        // RVA: 0x0B2DA180  token: 0x600065E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200011F  // size: 0x10
    public static class Sections
    {
        // Fields
        public static System.Int32 section1;  // const
        public static System.Int32 section2;  // const
        public static System.Int32 section3;  // const
        public static System.Int32 section4;  // const
        public static System.Int32 section5;  // const
        public static System.Int32 section6;  // const
        public static System.Int32 section7;  // const
        public static System.Int32 section8;  // const

    }

    // TypeToken: 0x2000120  // size: 0x10
    public static class Priorities
    {
        // Fields
        public static System.Int32 assetsCreateShaderMenuPriority;  // const
        public static System.Int32 assetsCreateRenderingMenuPriority;  // const
        public static System.Int32 editMenuPriority;  // const
        public static System.Int32 gameObjectMenuPriority;  // const
        public static System.Int32 srpLensFlareMenuPriority;  // const

    }

    // TypeToken: 0x2000121  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Rendering.CoreUtils.<>c <>9;  // static @ 0x0
        public static System.Func<System.Reflection.Assembly,System.Collections.Generic.IEnumerable<System.Type>> <>9__81_0;  // static @ 0x8

        // Methods
        // RVA: 0x04D44A70  token: 0x60006C5
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60006C6
        public System.Void .ctor() { }
        // RVA: 0x03B0F0B0  token: 0x60006C7
        private System.Collections.Generic.IEnumerable<System.Type> <GetAllAssemblyTypes>b__81_0(System.Reflection.Assembly t) { }

    }

    // TypeToken: 0x2000122
    public sealed class <>c__82`1
    {
        // Fields
        public static readonly UnityEngine.Rendering.CoreUtils.<>c__82<T> <>9;  // static @ 0x0
        public static System.Func<System.Type,System.Boolean> <>9__82_0;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60006C8
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x60006C9
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60006CA
        private System.Boolean <GetAllTypesDerivedFrom>b__82_0(System.Type t) { }

    }

    // TypeToken: 0x2000125  // size: 0x10
    public static class ShaderConstants
    {
        // Fields
        public static readonly System.Int32 _FsrEasuConstants0;  // static @ 0x0
        public static readonly System.Int32 _FsrEasuConstants1;  // static @ 0x4
        public static readonly System.Int32 _FsrEasuConstants2;  // static @ 0x8
        public static readonly System.Int32 _FsrEasuConstants3;  // static @ 0xc
        public static readonly System.Int32 _FsrRcasConstants;  // static @ 0x10

        // Methods
        // RVA: 0x0B2E6168  token: 0x60006D0
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000127  // size: 0x28
    public class Segment
    {
        // Fields
        public System.Single offsetX;  // 0x10
        public System.Single offsetY;  // 0x14
        public System.Single scaleX;  // 0x18
        public System.Single scaleY;  // 0x1c
        public System.Single lnA;  // 0x20
        public System.Single B;  // 0x24

        // Methods
        // RVA: 0x0B2E6124  token: 0x60006E0
        public System.Single Eval(System.Single x) { }
        // RVA: 0x041E1670  token: 0x60006E1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000128  // size: 0x30
    public sealed struct DirectParams
    {
        // Fields
        private System.Single x0;  // 0x10
        private System.Single y0;  // 0x14
        private System.Single x1;  // 0x18
        private System.Single y1;  // 0x1c
        private System.Single W;  // 0x20
        private System.Single overshootX;  // 0x24
        private System.Single overshootY;  // 0x28
        private System.Single gamma;  // 0x2c

    }

    // TypeToken: 0x2000129  // size: 0x18
    public class Uniforms
    {
        // Fields
        private UnityEngine.Rendering.HableCurve parent;  // 0x10

        // Properties
        UnityEngine.Vector4 curve { get; /* RVA: 0x0B2E68B8 */ }
        UnityEngine.Vector4 toeSegmentA { get; /* RVA: 0x0B2E6AD8 */ }
        UnityEngine.Vector4 toeSegmentB { get; /* RVA: 0x0B2E6B64 */ }
        UnityEngine.Vector4 midSegmentA { get; /* RVA: 0x0B2E68F0 */ }
        UnityEngine.Vector4 midSegmentB { get; /* RVA: 0x0B2E697C */ }
        UnityEngine.Vector4 shoSegmentA { get; /* RVA: 0x0B2E69E4 */ }
        UnityEngine.Vector4 shoSegmentB { get; /* RVA: 0x0B2E6A70 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x60006E2
        private System.Void .ctor(UnityEngine.Rendering.HableCurve parent) { }

    }

    // TypeToken: 0x200012E  // size: 0x28
    public sealed struct <>c__DisplayClass10_0
    {
        // Fields
        public UnityEngine.Rendering.MeshGizmo <>4__this;  // 0x10
        public UnityEngine.Color color;  // 0x18

    }

    // TypeToken: 0x2000130  // size: 0x14
    public sealed struct Package
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.ReloadAttribute.Package Builtin;  // const
        public static UnityEngine.Rendering.ReloadAttribute.Package Root;  // const

    }

    // TypeToken: 0x200013D  // size: 0x18
    public sealed class Indent : UnityEngine.PropertyAttribute
    {
        // Fields
        public readonly System.Int32 relativeAmount;  // 0x10

        // Methods
        // RVA: 0x014F51F0  token: 0x600073B
        public System.Void .ctor(System.Int32 relativeAmount) { }

    }

    // TypeToken: 0x200013E  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Rendering.VolumeComponent.<>c <>9;  // static @ 0x0
        public static System.Func<System.Reflection.FieldInfo,System.Int32> <>9__10_0;  // static @ 0x8

        // Methods
        // RVA: 0x04D4A770  token: 0x600073C
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600073D
        public System.Void .ctor() { }
        // RVA: 0x03E64790  token: 0x600073E
        private System.Int32 <FindParameters>b__10_0(System.Reflection.FieldInfo t) { }

    }

    // TypeToken: 0x2000140  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Rendering.VolumeManager.<>c <>9;  // static @ 0x0
        public static System.Func<System.Type,System.Boolean> <>9__25_0;  // static @ 0x8
        public static System.Predicate<UnityEngine.Rendering.Volume> <>9__38_0;  // static @ 0x10

        // Methods
        // RVA: 0x04D4C330  token: 0x600075C
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600075D
        public System.Void .ctor() { }
        // RVA: 0x047039E0  token: 0x600075E
        private System.Boolean <ReloadBaseTypes>b__25_0(System.Type t) { }
        // RVA: 0x0B2E686C  token: 0x600075F
        private System.Boolean <GetVolumes>b__38_0(UnityEngine.Rendering.Volume v) { }
        // RVA: 0x046FE1E0  token: 0x6000760
        private UnityEngine.Rendering.VolumeManager <.cctor>b__42_0() { }

    }

    // TypeToken: 0x200016C  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Rendering.VolumeProfile.<>c <>9;  // static @ 0x0
        public static System.Predicate<UnityEngine.Rendering.VolumeComponent> <>9__2_0;  // static @ 0x8

        // Methods
        // RVA: 0x04D47890  token: 0x60007E8
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60007E9
        public System.Void .ctor() { }
        // RVA: 0x0463BDE0  token: 0x60007EA
        private System.Boolean <OnEnable>b__2_0(UnityEngine.Rendering.VolumeComponent x) { }

    }

    // TypeToken: 0x2000176  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.Rendering.UI.DebugUIHandlerCanvas.<>c <>9;  // static @ 0x0
        public static System.Func<UnityEngine.Rendering.DebugUI.Widget,System.Boolean> <>9__12_0;  // static @ 0x8
        public static System.Action<UnityEngine.Rendering.UI.DebugUIHandlerPanel> <>9__15_0;  // static @ 0x10

        // Methods
        // RVA: 0x0B2EE3DC  token: 0x6000821
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000822
        public System.Void .ctor() { }
        // RVA: 0x0B2EE308  token: 0x6000823
        private System.Boolean <Rebuild>b__12_0(UnityEngine.Rendering.DebugUI.Widget x) { }
        // RVA: 0x0B2EE2D4  token: 0x6000824
        private System.Void <ActivatePanel>b__15_0(UnityEngine.Rendering.UI.DebugUIHandlerPanel p) { }

    }

    // TypeToken: 0x2000177  // size: 0x18
    public sealed class <>c__DisplayClass14_0
    {
        // Fields
        public System.String queryPath;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000825
        public System.Void .ctor() { }
        // RVA: 0x0B2EE338  token: 0x6000826
        private System.Boolean <GetWidgetFromPath>b__0(UnityEngine.Rendering.UI.DebugUIHandlerWidget w) { }

    }

    // TypeToken: 0x200017A  // size: 0x18
    public sealed class <>c__DisplayClass3_0
    {
        // Fields
        public UnityEngine.Rendering.UI.DebugUIHandlerWidget widget;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000842
        public System.Void .ctor() { }
        // RVA: 0x0B2EE368  token: 0x6000843
        private System.Boolean <IsDirectChild>b__0(UnityEngine.Rendering.UI.DebugUIHandlerWidget x) { }

    }

    // TypeToken: 0x200017D  // size: 0x28
    public sealed class <RefreshAfterSanitization>d__4 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public UnityEngine.Rendering.UI.DebugUIHandlerEnumHistory <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000850
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000851
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B2EE158  token: 0x6000852
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B2EE288  token: 0x6000854
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000188  // size: 0x18
    public sealed class <>c__DisplayClass3_0
    {
        // Fields
        public UnityEngine.Rendering.DebugUI.Value widget;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000899
        public System.Void .ctor() { }
        // RVA: 0x0B2EE3BC  token: 0x600089A
        private System.Boolean <Toggle>b__0(UnityEngine.Rendering.UI.DebugUIHandlerValue x) { }

    }

    // TypeToken: 0x200018C  // size: 0x28
    public sealed class <RefreshAfterSanitization>d__4 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public UnityEngine.Rendering.UI.DebugUIHandlerToggleHistory <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60008A9
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60008AA
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B2EE0BC  token: 0x60008AB
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B2EE23C  token: 0x60008AD
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000196  // size: 0x28
    public sealed struct __StaticArrayInitTypeSize=24
    {
    }

    // TypeToken: 0x2000197  // size: 0xA0
    public sealed struct __StaticArrayInitTypeSize=144
    {
    }

    // TypeToken: 0x2000198  // size: 0x110
    public sealed struct __StaticArrayInitTypeSize=256
    {
    }

    // TypeToken: 0x2000199  // size: 0x210
    public sealed struct __StaticArrayInitTypeSize=512
    {
    }

    // TypeToken: 0x200019A  // size: 0x410
    public sealed struct __StaticArrayInitTypeSize=1024
    {
    }

    // TypeToken: 0x200019B  // size: 0x1010
    public sealed struct __StaticArrayInitTypeSize=4096
    {
    }

    // TypeToken: 0x2000195  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=256 45AC882BD91FEBD5A58E67471A0EF32BFC485447A8A18D61DBA335FDA5A2C097;  // static @ 0x0
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=512 4A1D5C231BB4DDB64A57CFF5FC45592F845DB22D8DE0DCCA7BB59D822908C39A;  // static @ 0x100
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=1024 90C2D148CDB931199C77ADD692B92324CDFC328EDD1D0AB7FA24830C177A4FF4;  // static @ 0x300
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=144 BDFF4BF48DBC7214A392CF884F7F2A6073CCDB77CA3EB11BEBB93DA8ED8B2158;  // static @ 0x700
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=4096 C606E03B5FE8EAD2ECA6BCB45AE684039D928B4EE7C4A03C63D0DF9F94F81DAF;  // static @ 0x790
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=24 FE78C65211DD0B56A97024FB61111E686EF1FE054AA132BA58E2891AC496F1EE;  // static @ 0x1790

    }

namespace Beyond.Rendering
{

    // TypeToken: 0x2000005  // size: 0x10
    public static class HGShaderQualitySettings
    {
        // Fields
        public static Beyond.Rendering.HGShaderQualitySettings.ShaderQuality HgShaderLod;  // static @ 0x0

        // Methods
        // RVA: 0x0B29F78C  token: 0x6000006
        private static System.Void .cctor() { }

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

namespace UnityEngine
{

    // TypeToken: 0x2000007  // size: 0x40
    public class LightAnchor : UnityEngine.MonoBehaviour
    {
        // Fields
        private static System.Single k_ArcRadius;  // const
        private static System.Single k_AxisLength;  // const
        private static System.Single k_MaxDistance;  // const
        private System.Single m_Distance;  // 0x18
        private UnityEngine.LightAnchor.UpDirection m_FrameSpace;  // 0x1c
        private UnityEngine.Transform m_AnchorPositionOverride;  // 0x20
        private UnityEngine.Vector3 m_AnchorPositionOffset;  // 0x28
        private System.Single m_Yaw;  // 0x34
        private System.Single m_Pitch;  // 0x38
        private System.Single m_Roll;  // 0x3c

        // Properties
        System.Single yaw { get; /* RVA: 0x04D8E050 */ set; /* RVA: 0x0B2A0C38 */ }
        System.Single pitch { get; /* RVA: 0x04D88360 */ set; /* RVA: 0x0B2A0B98 */ }
        System.Single roll { get; /* RVA: 0x04D863E0 */ set; /* RVA: 0x0B2A0BE8 */ }
        System.Single distance { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x0B2A0B74 */ }
        UnityEngine.LightAnchor.UpDirection frameSpace { get; /* RVA: 0x04D86310 */ set; /* RVA: 0x04D86320 */ }
        UnityEngine.Vector3 anchorPosition { get; /* RVA: 0x0B2A09F8 */ }
        UnityEngine.Transform anchorPositionOverride { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        UnityEngine.Vector3 anchorPositionOffset { get; /* RVA: 0x04D90C00 */ set; /* RVA: 0x04D90C20 */ }

        // Methods
        // RVA: 0x0B2A0090  token: 0x6000016
        public static System.Single NormalizeAngleDegree(System.Single angle) { }
        // RVA: 0x0B2A01A0  token: 0x6000017
        public System.Void SynchronizeOnTransform(UnityEngine.Camera camera) { }
        // RVA: 0x0B2A059C  token: 0x6000018
        public System.Void UpdateTransform(UnityEngine.Camera camera, UnityEngine.Vector3 anchor) { }
        // RVA: 0x0B29F954  token: 0x6000019
        private UnityEngine.LightAnchor.Axes GetWorldSpaceAxes(UnityEngine.Camera camera, UnityEngine.Vector3 anchor) { }
        // RVA: 0x0B2A08FC  token: 0x600001A
        private System.Void Update() { }
        // RVA: 0x0B2A00D8  token: 0x600001B
        private System.Void OnDrawGizmosSelected() { }
        // RVA: 0x0B2A0678  token: 0x600001C
        private System.Void UpdateTransform(UnityEngine.Vector3 up, UnityEngine.Vector3 right, UnityEngine.Vector3 forward, UnityEngine.Vector3 anchor) { }
        // RVA: 0x05393520  token: 0x600001D
        public System.Void .ctor() { }

    }

}

namespace UnityEngine.Experimental.Rendering
{

    // TypeToken: 0x200000A  // size: 0x58
    public class ProbeBrickIndex
    {
        // Fields
        private static System.Int32 kMaxSubdivisionLevels;  // const
        private static System.Int32 kIndexChunkSize;  // const
        private System.Collections.BitArray m_IndexChunks;  // 0x10
        private System.Int32 m_IndexInChunks;  // 0x18
        private System.Int32 m_NextFreeChunk;  // 0x1c
        private UnityEngine.ComputeBuffer m_PhysicalIndexBuffer;  // 0x20
        private System.Int32[] m_PhysicalIndexBufferData;  // 0x28
        private System.Int32 <estimatedVMemCost>k__BackingField;  // 0x30
        private UnityEngine.Vector3Int m_CenterRS;  // 0x34
        private System.Collections.Generic.Dictionary<UnityEngine.Vector3Int,System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickIndex.VoxelMeta>> m_VoxelToBricks;  // 0x40
        private System.Collections.Generic.Dictionary<UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId,UnityEngine.Experimental.Rendering.ProbeBrickIndex.BrickMeta> m_BricksToVoxels;  // 0x48
        private System.Boolean m_NeedUpdateIndexComputeBuffer;  // 0x50

        // Properties
        System.Int32 estimatedVMemCost { get; /* RVA: 0x04D864F0 */ set; /* RVA: 0x04D86740 */ }

        // Methods
        // RVA: 0x0B2A1B84  token: 0x6000020
        private System.Int32 GetVoxelSubdivLevel() { }
        // RVA: 0x0B2A2248  token: 0x6000021
        private System.Int32 SizeOfPhysicalIndexFromBudget(UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudget memoryBudget) { }
        // RVA: 0x0B2A2C84  token: 0x6000022
        private System.Void .ctor(UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudget memoryBudget) { }
        // RVA: 0x0B2A2C54  token: 0x6000023
        private System.Void UploadIndexData() { }
        // RVA: 0x0B2A18B4  token: 0x6000024
        private System.Void Clear() { }
        // RVA: 0x0B2A1BD4  token: 0x6000025
        private System.Void MapBrickToVoxels(UnityEngine.Experimental.Rendering.ProbeBrickIndex.Brick brick, System.Collections.Generic.HashSet<UnityEngine.Vector3Int> voxels) { }
        // RVA: 0x0B2A17B4  token: 0x6000026
        private System.Void ClearVoxel(UnityEngine.Vector3Int pos, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo cellInfo) { }
        // RVA: 0x0B2A1B4C  token: 0x6000027
        private System.Void GetRuntimeResources(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RuntimeResources& rr) { }
        // RVA: 0x0B2A1754  token: 0x6000028
        private System.Void Cleanup() { }
        // RVA: 0x04DB8A20  token: 0x6000029
        private System.Int32 MergeIndex(System.Int32 index, System.Int32 size) { }
        // RVA: 0x0B2A1638  token: 0x600002A
        private System.Boolean AssignIndexChunksToCell(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Cell cell, System.Int32 bricksCount, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo& cellUpdateInfo) { }
        // RVA: 0x0B2A0C88  token: 0x600002B
        public System.Void AddBricks(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId id, System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickIndex.Brick> bricks, System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc> allocations, System.Int32 allocationSize, System.Int32 poolWidth, System.Int32 poolHeight, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo cellInfo) { }
        // RVA: 0x0B2A1DC4  token: 0x600002C
        public System.Void RemoveBricks(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId id, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo cellInfo) { }
        // RVA: 0x0B2A2274  token: 0x600002D
        private System.Void UpdateIndexForVoxel(UnityEngine.Vector3Int voxel, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo cellInfo) { }
        // RVA: 0x0B2A2858  token: 0x600002E
        private System.Void UpdatePhysicalIndex(UnityEngine.Vector3Int brickMin, UnityEngine.Vector3Int brickMax, System.Int32 value, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo cellInfo) { }
        // RVA: 0x0B2A195C  token: 0x600002F
        private System.Void ClipToIndexSpace(UnityEngine.Vector3Int pos, System.Int32 subdiv, UnityEngine.Vector3Int& outMinpos, UnityEngine.Vector3Int& outMaxpos, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo cellInfo) { }
        // RVA: 0x0B2A24D4  token: 0x6000030
        private System.Void UpdateIndexForVoxel(UnityEngine.Vector3Int voxel, System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickIndex.ReservedBrick> bricks, System.Collections.Generic.List<System.UInt16> indices, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo cellInfo) { }

    }

    // TypeToken: 0x2000012  // size: 0x88
    public class ProbeBrickPool
    {
        // Fields
        private static System.Int32 kBrickCellCount;  // const
        private static System.Int32 kBrickProbeCountPerDim;  // const
        private static System.Int32 kBrickProbeCountTotal;  // const
        private System.Int32 <estimatedVMemCost>k__BackingField;  // 0x10
        private static System.Int32 kMaxPoolWidth;  // const
        private System.Int32 m_AllocationSize;  // 0x14
        private UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudget m_MemoryBudget;  // 0x18
        private UnityEngine.Experimental.Rendering.ProbeBrickPool.DataLocation m_Pool;  // 0x20
        private UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc m_NextFreeChunk;  // 0x68
        private System.Collections.Generic.Stack<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc> m_FreeList;  // 0x78
        private UnityEngine.Experimental.Rendering.ProbeVolumeSHBands m_SHBands;  // 0x80

        // Properties
        System.Int32 estimatedVMemCost { get; /* RVA: 0x02B2E2D0 */ set; /* RVA: 0x014F51F0 */ }

        // Methods
        // RVA: 0x0B2A43D0  token: 0x6000039
        private System.Void .ctor(System.Int32 allocationSize, UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudget memoryBudget, UnityEngine.Experimental.Rendering.ProbeVolumeSHBands shBands) { }
        // RVA: 0x0B2A3550  token: 0x600003A
        private System.Void EnsureTextureValidity() { }
        // RVA: 0x04D882A0  token: 0x600003B
        private System.Int32 GetChunkSize() { }
        // RVA: 0x04DB8A60  token: 0x600003C
        private System.Int32 GetChunkSizeInProbeCount() { }
        // RVA: 0x04D86570  token: 0x600003D
        private System.Int32 GetPoolWidth() { }
        // RVA: 0x04D86560  token: 0x600003E
        private System.Int32 GetPoolHeight() { }
        // RVA: 0x04DB8A70  token: 0x600003F
        private UnityEngine.Vector3Int GetPoolDimensions() { }
        // RVA: 0x0B2A3F0C  token: 0x6000040
        private System.Void GetRuntimeResources(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RuntimeResources& rr) { }
        // RVA: 0x0B2A2FC8  token: 0x6000041
        private System.Void Clear() { }
        // RVA: 0x0B2A2E80  token: 0x6000042
        private System.Void Allocate(System.Int32 numberOfBrickChunks, System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc> outAllocations) { }
        // RVA: 0x0B2A3404  token: 0x6000043
        private System.Void Deallocate(System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc> allocations) { }
        // RVA: 0x0B2A4038  token: 0x6000044
        private System.Void Update(UnityEngine.Experimental.Rendering.ProbeBrickPool.DataLocation source, System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc> srcLocations, System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc> dstLocations, UnityEngine.Experimental.Rendering.ProbeVolumeSHBands bands) { }
        // RVA: 0x0B2A3F94  token: 0x6000045
        private static UnityEngine.Vector3Int ProbeCountToDataLocSize(System.Int32 numProbes) { }
        // RVA: 0x0B2A3018  token: 0x6000046
        public static UnityEngine.Experimental.Rendering.ProbeBrickPool.DataLocation CreateDataLocation(System.Int32 numProbes, System.Boolean compressed, UnityEngine.Experimental.Rendering.ProbeVolumeSHBands bands, System.Int32& allocatedBytes) { }
        // RVA: 0x0B2A3FF4  token: 0x6000047
        private static System.Void SetPixel(UnityEngine.Color[]& data, System.Int32 x, System.Int32 y, System.Int32 z, System.Int32 dataLocWidth, System.Int32 dataLocHeight, UnityEngine.Color value) { }
        // RVA: 0x0B2A3638  token: 0x6000048
        public static System.Void FillDataLocation(UnityEngine.Experimental.Rendering.ProbeBrickPool.DataLocation& loc, UnityEngine.Rendering.SphericalHarmonicsL2[] shl2, UnityEngine.Experimental.Rendering.ProbeVolumeSHBands bands) { }
        // RVA: 0x04DB8A40  token: 0x6000049
        private System.Void DerivePoolSizeFromBudget(System.Int32 allocationSize, UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudget memoryBudget, System.Int32& width, System.Int32& height, System.Int32& depth) { }
        // RVA: 0x0B2A2FBC  token: 0x600004A
        private System.Void Cleanup() { }

    }

    // TypeToken: 0x2000015  // size: 0x48
    public class ProbeCellIndices
    {
        // Fields
        private static System.Int32 kUintPerEntry;  // const
        private System.Int32 <estimatedVMemCost>k__BackingField;  // 0x10
        private UnityEngine.ComputeBuffer m_IndexOfIndicesBuffer;  // 0x18
        private System.UInt32[] m_IndexOfIndicesData;  // 0x20
        private UnityEngine.Vector3Int m_CellCount;  // 0x28
        private UnityEngine.Vector3Int m_CellMin;  // 0x34
        private System.Int32 m_CellSizeInMinBricks;  // 0x40
        private System.Boolean m_NeedUpdateComputeBuffer;  // 0x44

        // Properties
        System.Int32 estimatedVMemCost { get; /* RVA: 0x02B2E2D0 */ set; /* RVA: 0x014F51F0 */ }

        // Methods
        // RVA: 0x04D90C00  token: 0x600004F
        private UnityEngine.Vector3Int GetCellIndexDimension() { }
        // RVA: 0x04D89640  token: 0x6000050
        private UnityEngine.Vector3Int GetCellMinPosition() { }
        // RVA: 0x04DB8A90  token: 0x6000051
        private System.Int32 GetFlatIndex(UnityEngine.Vector3Int normalizedPos) { }
        // RVA: 0x0B2A47DC  token: 0x6000052
        private System.Void .ctor(UnityEngine.Vector3Int cellMin, UnityEngine.Vector3Int cellMax, System.Int32 cellSizeInMinBricks) { }
        // RVA: 0x0B2A46C8  token: 0x6000053
        private System.Int32 GetFlatIdxForCell(UnityEngine.Vector3Int cellPosition) { }
        // RVA: 0x0B2A44F8  token: 0x6000054
        private System.Void AddCell(System.Int32 cellFlatIdx, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo cellUpdateInfo) { }
        // RVA: 0x0B2A4764  token: 0x6000055
        private System.Void MarkCellAsUnloaded(System.Int32 cellFlatIdx) { }
        // RVA: 0x0B2A47AC  token: 0x6000056
        private System.Void PushComputeData() { }
        // RVA: 0x0B2A472C  token: 0x6000057
        private System.Void GetRuntimeResources(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RuntimeResources& rr) { }
        // RVA: 0x0B2A4668  token: 0x6000058
        private System.Void Cleanup() { }

    }

    // TypeToken: 0x2000017  // size: 0x38
    public sealed struct ProbeVolumeSystemParameters
    {
        // Fields
        public UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudget memoryBudget;  // 0x10
        public UnityEngine.Mesh probeDebugMesh;  // 0x18
        public UnityEngine.Shader probeDebugShader;  // 0x20
        public UnityEngine.Experimental.Rendering.ProbeVolumeSceneData sceneData;  // 0x28
        public UnityEngine.Experimental.Rendering.ProbeVolumeSHBands shBands;  // 0x30

    }

    // TypeToken: 0x2000018  // size: 0x20
    public sealed struct ProbeVolumeShadingParameters
    {
        // Fields
        public System.Single normalBias;  // 0x10
        public System.Single viewBias;  // 0x14
        public System.Boolean scaleBiasByMinDistanceBetweenProbes;  // 0x18
        public System.Single samplingNoise;  // 0x1c

    }

    // TypeToken: 0x2000019  // size: 0x14
    public sealed struct ProbeVolumeTextureMemoryBudget
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudget MemoryBudgetLow;  // const
        public static UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudget MemoryBudgetMedium;  // const
        public static UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudget MemoryBudgetHigh;  // const

    }

    // TypeToken: 0x200001A  // size: 0x14
    public sealed struct ProbeVolumeSHBands
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Experimental.Rendering.ProbeVolumeSHBands SphericalHarmonicsL1;  // const
        public static UnityEngine.Experimental.Rendering.ProbeVolumeSHBands SphericalHarmonicsL2;  // const

    }

    // TypeToken: 0x200001B  // size: 0x1A8
    public class ProbeReferenceVolume
    {
        // Fields
        private static System.Int32 kProbeIndexPoolAllocationSize;  // const
        private System.Boolean m_IsInitialized;  // 0x10
        private System.Int32 m_ID;  // 0x14
        private UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RefVolTransform m_Transform;  // 0x18
        private System.Int32 m_MaxSubdivision;  // 0x78
        private UnityEngine.Experimental.Rendering.ProbeBrickPool m_Pool;  // 0x80
        private UnityEngine.Experimental.Rendering.ProbeBrickIndex m_Index;  // 0x88
        private UnityEngine.Experimental.Rendering.ProbeCellIndices m_CellIndices;  // 0x90
        private System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc> m_TmpSrcChunks;  // 0x98
        private System.Single[] m_PositionOffsets;  // 0xa0
        private System.Collections.Generic.Dictionary<UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId,System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc>> m_Registry;  // 0xa8
        private UnityEngine.Bounds m_CurrGlobalBounds;  // 0xb0
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Cell> cells;  // 0xc8
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Experimental.Rendering.ProbeReferenceVolume.CellChunkInfo> m_ChunkInfo;  // 0xd0
        private UnityEngine.Experimental.Rendering.ProbeVolumeSceneData sceneData;  // 0xd8
        public System.Action<UnityEngine.Experimental.Rendering.ProbeReferenceVolume.ExtraDataActionInput> retrieveExtraDataAction;  // 0xe0
        private System.Boolean m_BricksLoaded;  // 0xe8
        private System.Collections.Generic.Dictionary<UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Cell,UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId> m_CellToBricks;  // 0xf0
        private System.Collections.Generic.Dictionary<UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId,UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo> m_BricksToCellUpdateInfo;  // 0xf8
        private System.Collections.Generic.Dictionary<System.String,UnityEngine.Experimental.Rendering.ProbeVolumeAsset> m_PendingAssetsToBeLoaded;  // 0x100
        private System.Collections.Generic.Dictionary<System.String,UnityEngine.Experimental.Rendering.ProbeVolumeAsset> m_PendingAssetsToBeUnloaded;  // 0x108
        private System.Collections.Generic.Dictionary<System.String,UnityEngine.Experimental.Rendering.ProbeVolumeAsset> m_ActiveAssets;  // 0x110
        private System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeReferenceVolume.CellSortInfo> m_CellsToBeLoaded;  // 0x118
        private System.Collections.Generic.Dictionary<System.Int32,System.Int32> m_CellRefCounting;  // 0x120
        private System.Boolean m_NeedLoadAsset;  // 0x128
        private System.Boolean m_ProbeReferenceVolumeInit;  // 0x129
        private System.Boolean m_EnabledBySRP;  // 0x12a
        private UnityEngine.Experimental.Rendering.ProbeReferenceVolume.InitInfo m_PendingInitInfo;  // 0x12c
        private System.Boolean m_NeedsIndexRebuild;  // 0x144
        private System.Boolean m_HasChangedIndex;  // 0x145
        private System.Int32 m_CBShaderID;  // 0x148
        private System.Int32 m_NumberOfCellsLoadedPerFrame;  // 0x14c
        private UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudget m_MemoryBudget;  // 0x150
        private UnityEngine.Experimental.Rendering.ProbeVolumeSHBands m_SHBands;  // 0x154
        private System.Boolean clearAssetsOnVolumeClear;  // 0x158
        private static UnityEngine.Experimental.Rendering.ProbeReferenceVolume _instance;  // static @ 0x0
        private static System.Int32 kProbesPerBatch;  // const
        private readonly UnityEngine.Experimental.Rendering.ProbeVolumeDebug <debugDisplay>k__BackingField;  // 0x160
        private readonly UnityEngine.Color[] <subdivisionDebugColors>k__BackingField;  // 0x168
        private UnityEngine.Rendering.DebugUI.Widget[] m_DebugItems;  // 0x170
        private UnityEngine.Mesh m_DebugMesh;  // 0x178
        private UnityEngine.Material m_DebugMaterial;  // 0x180
        private System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeReferenceVolume.CellInstancedDebugProbes> m_CellDebugData;  // 0x188
        private UnityEngine.Plane[] m_DebugFrustumPlanes;  // 0x190
        private System.Single dilationValidtyThreshold;  // 0x198
        private System.Collections.Generic.Dictionary<UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Volume,System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickIndex.Brick>> realtimeSubdivisionInfo;  // 0x1a0

        // Properties
        System.Boolean isInitialized { get; /* RVA: 0x04DB8B40 */ }
        System.Boolean enabledBySRP { get; /* RVA: 0x04DB8B30 */ }
        UnityEngine.Experimental.Rendering.ProbeVolumeSHBands shBands { get; /* RVA: 0x04DB8B50 */ }
        UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudget memoryBudget { get; /* RVA: 0x04DA24D0 */ }
        UnityEngine.Experimental.Rendering.ProbeReferenceVolume instance { get; /* RVA: 0x0B2AA918 */ }
        UnityEngine.Experimental.Rendering.ProbeVolumeDebug debugDisplay { get; /* RVA: 0x04D8D8F0 */ }
        UnityEngine.Color[] subdivisionDebugColors { get; /* RVA: 0x04D8DF00 */ }

        // Methods
        // RVA: 0x0B2A79F8  token: 0x600005A
        private System.Void InvalidateAllCellRefs() { }
        // RVA: 0x0B2A9F68  token: 0x6000060
        public System.Void SetNumberOfCellsLoadedPerFrame(System.Int32 numberOfCells) { }
        // RVA: 0x0B2A7924  token: 0x6000061
        public System.Void Initialize(UnityEngine.Experimental.Rendering.ProbeVolumeSystemParameters& parameters) { }
        // RVA: 0x04DB8B20  token: 0x6000062
        public System.Void SetEnableStateFromSRP(System.Boolean srpEnablesPV) { }
        // RVA: 0x0B2A6D94  token: 0x6000063
        private System.Void ForceSHBand(UnityEngine.Experimental.Rendering.ProbeVolumeSHBands shBands) { }
        // RVA: 0x0B2A5AF8  token: 0x6000064
        public System.Void Cleanup() { }
        // RVA: 0x0B2A74B8  token: 0x6000065
        public System.Int32 GetVideoMemoryCost() { }
        // RVA: 0x0B2A99CC  token: 0x6000066
        private System.Void RemoveCell(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Cell cell) { }
        // RVA: 0x0B2A4E2C  token: 0x6000067
        private System.Void AddCell(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Cell cell, System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc> chunks) { }
        // RVA: 0x0B2A5878  token: 0x6000068
        private System.Boolean CheckCompatibilityWithCollection(UnityEngine.Experimental.Rendering.ProbeVolumeAsset asset, System.Collections.Generic.Dictionary<System.String,UnityEngine.Experimental.Rendering.ProbeVolumeAsset> collection) { }
        // RVA: 0x0B2A4FBC  token: 0x6000069
        private System.Void AddPendingAssetLoading(UnityEngine.Experimental.Rendering.ProbeVolumeAsset asset) { }
        // RVA: 0x0B2A5744  token: 0x600006A
        private System.Void AddPendingAssetRemoval(UnityEngine.Experimental.Rendering.ProbeVolumeAsset asset) { }
        // RVA: 0x0B2A9C34  token: 0x600006B
        private System.Void RemovePendingAsset(UnityEngine.Experimental.Rendering.ProbeVolumeAsset asset) { }
        // RVA: 0x0B2A82B4  token: 0x600006C
        private System.Void PerformPendingIndexChangeAndInit() { }
        // RVA: 0x0B2A9EF8  token: 0x600006D
        private System.Void SetMinBrickAndMaxSubdiv(System.Single minBrickSize, System.Int32 maxSubdiv) { }
        // RVA: 0x0B2A7A40  token: 0x600006E
        private System.Void LoadAsset(UnityEngine.Experimental.Rendering.ProbeVolumeAsset asset) { }
        // RVA: 0x0B2A8310  token: 0x600006F
        private System.Void PerformPendingLoading() { }
        // RVA: 0x0B2A810C  token: 0x6000070
        private System.Void PerformPendingDeletion() { }
        // RVA: 0x0B2A6FA0  token: 0x6000071
        private System.Int32 GetNumberOfBricksAtSubdiv(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Cell cell, UnityEngine.Vector3Int& minValidLocalIdxAtMaxRes, UnityEngine.Vector3Int& sizeOfValidIndicesAtMaxRes) { }
        // RVA: 0x0B2A6DEC  token: 0x6000072
        private System.Boolean GetCellIndexUpdate(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Cell cell, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo& cellUpdateInfo) { }
        // RVA: 0x0B2A7CA8  token: 0x6000073
        private System.Void LoadPendingCells(System.Boolean loadAll) { }
        // RVA: 0x0B2A867C  token: 0x6000074
        public System.Void PerformPendingOperations(System.Boolean loadAllCells) { }
        // RVA: 0x0B2A7508  token: 0x6000075
        private System.Void InitProbeReferenceVolume(System.Int32 allocationSize, UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudget memoryBudget, UnityEngine.Experimental.Rendering.ProbeVolumeSHBands shBands) { }
        // RVA: 0x0B2AA298  token: 0x6000076
        public System.Void SortPendingCells(UnityEngine.Vector3 cameraPosition) { }
        // RVA: 0x04735900  token: 0x6000077
        private System.Void .ctor() { }
        // RVA: 0x0B2A73F0  token: 0x6000078
        public UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RuntimeResources GetRuntimeResources() { }
        // RVA: 0x0B2A9F7C  token: 0x6000079
        private System.Void SetTRS(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, System.Single minBrickSize) { }
        // RVA: 0x0B2A9EA8  token: 0x600007A
        private System.Void SetMaxSubdivision(System.Int32 maxSubdivision) { }
        // RVA: 0x04DB8AF0  token: 0x600007B
        private static System.Int32 CellSize(System.Int32 subdivisionLevel) { }
        // RVA: 0x0B2A5804  token: 0x600007C
        private System.Single BrickSize(System.Int32 subdivisionLevel) { }
        // RVA: 0x04DA60B0  token: 0x600007D
        private System.Single MinBrickSize() { }
        // RVA: 0x0B2A80F0  token: 0x600007E
        private System.Single MaxBrickSize() { }
        // RVA: 0x04D978B0  token: 0x600007F
        private UnityEngine.Matrix4x4 GetRefSpaceToWS() { }
        // RVA: 0x04D96DC0  token: 0x6000080
        private UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RefVolTransform GetTransform() { }
        // RVA: 0x04D863C0  token: 0x6000081
        private System.Int32 GetMaxSubdivision() { }
        // RVA: 0x0B2A6F8C  token: 0x6000082
        private System.Int32 GetMaxSubdivision(System.Single multiplier) { }
        // RVA: 0x0B2A6F70  token: 0x6000083
        private System.Single GetDistanceBetweenProbes(System.Int32 subdivisionLevel) { }
        // RVA: 0x0B2A8100  token: 0x6000084
        private System.Single MinDistanceBetweenProbes() { }
        // RVA: 0x04D8C8C0  token: 0x6000085
        public System.Boolean DataHasBeenLoaded() { }
        // RVA: 0x0B2A5BE4  token: 0x6000086
        private System.Void Clear() { }
        // RVA: 0x0B2A4AAC  token: 0x6000087
        private UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId AddBricks(System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickIndex.Brick> bricks, UnityEngine.Experimental.Rendering.ProbeBrickPool.DataLocation dataloc, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo cellUpdateInfo, System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc>& ch_list) { }
        // RVA: 0x0B2A9818  token: 0x6000088
        private System.Void ReleaseBricks(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId id) { }
        // RVA: 0x0B2AA714  token: 0x6000089
        public System.Void UpdateConstantBuffer(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Experimental.Rendering.ProbeVolumeShadingParameters parameters) { }
        // RVA: 0x0B2A5A84  token: 0x600008A
        private System.Void CleanupLoadedData() { }
        // RVA: 0x03451490  token: 0x600008D
        public System.Void RenderDebug(UnityEngine.Camera camera) { }
        // RVA: 0x0B2A7754  token: 0x600008E
        private System.Void InitializeDebug(UnityEngine.Mesh debugProbeMesh, UnityEngine.Shader debugProbeShader) { }
        // RVA: 0x0B2A5A30  token: 0x600008F
        private System.Void CleanupDebug() { }
        // RVA: -1  // generic def  token: 0x6000090
        private System.Void RefreshDebug(UnityEngine.Rendering.DebugUI.Field<T> field, T value) { }
        // RVA: -1  // generic def  token: 0x6000091
        private System.Void DebugCellIndexChanged(UnityEngine.Rendering.DebugUI.Field<T> field, T value) { }
        // RVA: 0x0B2A86C0  token: 0x6000092
        private System.Void RegisterDebug() { }
        // RVA: 0x0B2AA648  token: 0x6000093
        private System.Void UnregisterDebug(System.Boolean destroyPanel) { }
        // RVA: 0x0B2AA04C  token: 0x6000094
        private System.Boolean ShouldCullCell(UnityEngine.Vector3 cellPosition, UnityEngine.Transform cameraTransform, UnityEngine.Plane[] frustumPlanes) { }
        // RVA: 0x0B2A685C  token: 0x6000095
        private System.Void DrawProbeDebug(UnityEngine.Camera camera) { }
        // RVA: 0x0B2A5B78  token: 0x6000096
        private System.Void ClearDebugData() { }
        // RVA: 0x0B2A5CCC  token: 0x6000097
        private System.Void CreateInstancedProbes() { }
        // RVA: 0x077CA5E4  token: 0x6000098
        private System.Void OnClearLightingdata() { }
        // RVA: 0x04735890  token: 0x6000099
        private static System.Void .cctor() { }
        // RVA: 0x0B2AA3BC  token: 0x600009A
        private System.Boolean <RegisterDebug>b__119_0() { }
        // RVA: 0x0B2AA4BC  token: 0x600009B
        private System.Void <RegisterDebug>b__119_1(System.Boolean value) { }
        // RVA: 0x0B2AA588  token: 0x600009C
        private System.Boolean <RegisterDebug>b__119_2() { }
        // RVA: 0x0B2AA5A8  token: 0x600009D
        private System.Void <RegisterDebug>b__119_3(System.Boolean value) { }
        // RVA: 0x0B2AA5C8  token: 0x600009E
        private System.Single <RegisterDebug>b__119_4() { }
        // RVA: 0x0B2AA5E8  token: 0x600009F
        private System.Void <RegisterDebug>b__119_5(System.Single value) { }
        // RVA: 0x0B2AA608  token: 0x60000A0
        private System.Boolean <RegisterDebug>b__119_7() { }
        // RVA: 0x0B2AA628  token: 0x60000A1
        private System.Void <RegisterDebug>b__119_8(System.Boolean value) { }
        // RVA: 0x0B2AA3FC  token: 0x60000A2
        private System.Int32 <RegisterDebug>b__119_9() { }
        // RVA: 0x0B2AA3DC  token: 0x60000A3
        private System.Void <RegisterDebug>b__119_10(System.Int32 value) { }
        // RVA: 0x0B2AA3FC  token: 0x60000A4
        private System.Int32 <RegisterDebug>b__119_11() { }
        // RVA: 0x0B2AA3DC  token: 0x60000A5
        private System.Void <RegisterDebug>b__119_12(System.Int32 value) { }
        // RVA: 0x0B2AA41C  token: 0x60000A6
        private System.Single <RegisterDebug>b__119_13() { }
        // RVA: 0x0B2AA43C  token: 0x60000A7
        private System.Void <RegisterDebug>b__119_14(System.Single value) { }
        // RVA: 0x0B2AA45C  token: 0x60000A8
        private System.Single <RegisterDebug>b__119_17() { }
        // RVA: 0x0B2AA47C  token: 0x60000A9
        private System.Void <RegisterDebug>b__119_18(System.Single value) { }
        // RVA: 0x0B2AA49C  token: 0x60000AA
        private System.Single <RegisterDebug>b__119_19() { }
        // RVA: 0x0B2AA4DC  token: 0x60000AB
        private System.Void <RegisterDebug>b__119_20(System.Single value) { }
        // RVA: 0x0B2AA4FC  token: 0x60000AC
        private System.Int32 <RegisterDebug>b__119_22() { }
        // RVA: 0x0B2AA51C  token: 0x60000AD
        private System.Void <RegisterDebug>b__119_23(System.Int32 v) { }

    }

    // TypeToken: 0x2000027  // size: 0x14
    public sealed struct DebugProbeShadingMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Experimental.Rendering.DebugProbeShadingMode SH;  // const
        public static UnityEngine.Experimental.Rendering.DebugProbeShadingMode Validity;  // const
        public static UnityEngine.Experimental.Rendering.DebugProbeShadingMode ValidityOverDilationThreshold;  // const
        public static UnityEngine.Experimental.Rendering.DebugProbeShadingMode Size;  // const

    }

    // TypeToken: 0x2000028  // size: 0x38
    public class ProbeVolumeDebug
    {
        // Fields
        public System.Boolean drawProbes;  // 0x10
        public System.Boolean drawBricks;  // 0x11
        public System.Boolean drawCells;  // 0x12
        public System.Boolean realtimeSubdivision;  // 0x13
        public System.Int32 subdivisionCellUpdatePerFrame;  // 0x14
        public System.Single subdivisionDelayInSeconds;  // 0x18
        public UnityEngine.Experimental.Rendering.DebugProbeShadingMode probeShading;  // 0x1c
        public System.Single probeSize;  // 0x20
        public System.Single subdivisionViewCullingDistance;  // 0x24
        public System.Single probeCullingDistance;  // 0x28
        public System.Int32 maxSubdivToVisualize;  // 0x2c
        public System.Single exposureCompensation;  // 0x30

        // Methods
        // RVA: 0x04DB8B70  token: 0x60000C9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000029  // size: 0x28
    public sealed class ProbeReferenceVolumeProfile : UnityEngine.ScriptableObject
    {
        // Fields
        private UnityEngine.Experimental.Rendering.ProbeReferenceVolumeProfile.Version version;  // 0x18
        public System.Int32 simplificationLevels;  // 0x1c
        public System.Single minDistanceBetweenProbes;  // 0x20

        // Properties
        System.Int32 cellSizeInBricks { get; /* RVA: 0x04DB8AB0 */ }
        System.Int32 maxSubdivision { get; /* RVA: 0x04DB8AE0 */ }
        System.Single minBrickSize { get; /* RVA: 0x0B2A4A90 */ }
        System.Single cellSizeInMeters { get; /* RVA: 0x0B2A4A44 */ }

        // Methods
        // RVA: 0x0B2A4984  token: 0x60000CE
        private System.Void OnEnable() { }
        // RVA: 0x0B2A491C  token: 0x60000CF
        public System.Boolean IsEquivalent(UnityEngine.Experimental.Rendering.ProbeReferenceVolumeProfile otherProfile) { }
        // RVA: 0x0B2A49D8  token: 0x60000D0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002B  // size: 0x80
    public class ProbeVolume : UnityEngine.MonoBehaviour
    {
        // Fields
        public System.Boolean globalVolume;  // 0x18
        public UnityEngine.Vector3 size;  // 0x1c
        public System.Single geometryDistanceOffset;  // 0x28
        public UnityEngine.LayerMask objectLayerMask;  // 0x2c
        public System.Int32 lowestSubdivLevelOverride;  // 0x30
        public System.Int32 highestSubdivLevelOverride;  // 0x34
        public System.Boolean overridesSubdivLevels;  // 0x38
        private System.Boolean mightNeedRebaking;  // 0x39
        private UnityEngine.Matrix4x4 cachedTransform;  // 0x3c
        private System.Int32 cachedHashCode;  // 0x7c

        // Methods
        // RVA: 0x0B2AD4A0  token: 0x60000D1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002C  // size: 0x78
    public class ProbeVolumeAsset : UnityEngine.ScriptableObject
    {
        // Fields
        protected internal System.Int32 m_Version;  // 0x18
        private System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Cell> cells;  // 0x20
        private UnityEngine.Vector3Int maxCellPosition;  // 0x28
        private UnityEngine.Vector3Int minCellPosition;  // 0x34
        private UnityEngine.Bounds globalBounds;  // 0x40
        private UnityEngine.Experimental.Rendering.ProbeVolumeSHBands bands;  // 0x58
        private System.String m_AssetFullPath;  // 0x60
        private System.Int32 cellSizeInBricks;  // 0x68
        private System.Single minDistanceBetweenProbes;  // 0x6c
        private System.Int32 simplificationLevels;  // 0x70

        // Properties
        System.Int32 Version { get; /* RVA: 0x011EF5B0 */ }
        System.Int32 maxSubdivision { get; /* RVA: 0x04DB8B60 */ }
        System.Single minBrickSize { get; /* RVA: 0x0B2AAA54 */ }

        // Methods
        // RVA: 0x0B2AA94C  token: 0x60000D5
        private System.Boolean CompatibleWith(UnityEngine.Experimental.Rendering.ProbeVolumeAsset otherAsset) { }
        // RVA: 0x04D86280  token: 0x60000D6
        public System.String GetSerializedFullPath() { }
        // RVA: 0x0B2AA9AC  token: 0x60000D7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002E  // size: 0x24
    public sealed struct ProbeDilationSettings
    {
        // Fields
        public System.Boolean enableDilation;  // 0x10
        public System.Single dilationDistance;  // 0x14
        public System.Single dilationValidityThreshold;  // 0x18
        public System.Int32 dilationIterations;  // 0x1c
        public System.Boolean squaredDistWeighting;  // 0x20

    }

    // TypeToken: 0x200002F  // size: 0x1C
    public sealed struct VirtualOffsetSettings
    {
        // Fields
        public System.Boolean useVirtualOffset;  // 0x10
        public System.Single outOfGeoOffset;  // 0x14
        public System.Single searchMultiplier;  // 0x18

    }

    // TypeToken: 0x2000030  // size: 0x30
    public sealed struct ProbeVolumeBakingProcessSettings
    {
        // Fields
        public UnityEngine.Experimental.Rendering.ProbeDilationSettings dilationSettings;  // 0x10
        public UnityEngine.Experimental.Rendering.VirtualOffsetSettings virtualOffsetSettings;  // 0x24

    }

    // TypeToken: 0x2000031  // size: 0x14
    public sealed struct ProbeVolumeState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Experimental.Rendering.ProbeVolumeState Default;  // const
        public static UnityEngine.Experimental.Rendering.ProbeVolumeState Invalid;  // const

    }

    // TypeToken: 0x2000032  // size: 0x30
    public class ProbeVolumePerSceneData : UnityEngine.MonoBehaviour, UnityEngine.ISerializationCallbackReceiver
    {
        // Fields
        private System.Collections.Generic.Dictionary<UnityEngine.Experimental.Rendering.ProbeVolumeState,UnityEngine.Experimental.Rendering.ProbeVolumeAsset> assets;  // 0x18
        private System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeVolumePerSceneData.SerializableAssetItem> serializedAssets;  // 0x20
        private UnityEngine.Experimental.Rendering.ProbeVolumeState m_CurrentState;  // 0x28
        private UnityEngine.Experimental.Rendering.ProbeVolumeState m_PreviousState;  // 0x2c

        // Methods
        // RVA: 0x0B2AACAC  token: 0x60000D8
        public virtual System.Void OnAfterDeserialize() { }
        // RVA: 0x0B2AAE40  token: 0x60000D9
        public virtual System.Void OnBeforeSerialize() { }
        // RVA: 0x0B2AB280  token: 0x60000DA
        private System.Void StoreAssetForState(UnityEngine.Experimental.Rendering.ProbeVolumeState state, UnityEngine.Experimental.Rendering.ProbeVolumeAsset asset) { }
        // RVA: 0x0B2AAAE8  token: 0x60000DB
        private System.Void InvalidateAllAssets() { }
        // RVA: 0x0B2AAA70  token: 0x60000DC
        private UnityEngine.Experimental.Rendering.ProbeVolumeAsset GetCurrentStateAsset() { }
        // RVA: 0x0B2AB080  token: 0x60000DD
        private System.Void QueueAssetLoading() { }
        // RVA: 0x0B2AB188  token: 0x60000DE
        private System.Void QueueAssetRemoval() { }
        // RVA: 0x0B2AB078  token: 0x60000DF
        private System.Void OnEnable() { }
        // RVA: 0x0B2AB070  token: 0x60000E0
        private System.Void OnDisable() { }
        // RVA: 0x0B2AB070  token: 0x60000E1
        private System.Void OnDestroy() { }
        // RVA: 0x0B2AB2E4  token: 0x60000E2
        private System.Void Update() { }
        // RVA: 0x0B2AB3F8  token: 0x60000E3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000034  // size: 0x70
    public class ProbeVolumeSceneData : UnityEngine.ISerializationCallbackReceiver
    {
        // Fields
        private static System.Reflection.PropertyInfo s_SceneGUID;  // static @ 0x0
        private System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.SerializableBoundItem> serializedBounds;  // 0x10
        private System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.SerializableHasPVItem> serializedHasVolumes;  // 0x18
        private System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.SerializablePVProfile> serializedProfiles;  // 0x20
        private System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.SerializablePVBakeSettings> serializedBakeSettings;  // 0x28
        private System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.BakingSet> serializedBakingSets;  // 0x30
        private UnityEngine.Object parentAsset;  // 0x38
        private System.String parentSceneDataPropertyName;  // 0x40
        public System.Collections.Generic.Dictionary<System.String,UnityEngine.Bounds> sceneBounds;  // 0x48
        private System.Collections.Generic.Dictionary<System.String,System.Boolean> hasProbeVolumes;  // 0x50
        private System.Collections.Generic.Dictionary<System.String,UnityEngine.Experimental.Rendering.ProbeReferenceVolumeProfile> sceneProfiles;  // 0x58
        private System.Collections.Generic.Dictionary<System.String,UnityEngine.Experimental.Rendering.ProbeVolumeBakingProcessSettings> sceneBakingSettings;  // 0x60
        private System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.BakingSet> bakingSets;  // 0x68

        // Methods
        // RVA: 0x0B2AB520  token: 0x60000E4
        private System.String GetSceneGUID(UnityEngine.SceneManagement.Scene scene) { }
        // RVA: 0x0B2AD194  token: 0x60000E5
        public System.Void .ctor(UnityEngine.Object parentAsset, System.String parentSceneDataPropertyName) { }
        // RVA: 0x0B2ACA78  token: 0x60000E6
        public System.Void SetParentObject(UnityEngine.Object parent, System.String parentSceneDataPropertyName) { }
        // RVA: 0x0B2AB6C0  token: 0x60000E7
        public virtual System.Void OnAfterDeserialize() { }
        // RVA: 0x0B2ACD68  token: 0x60000E8
        private System.Void UpdateBakingSets() { }
        // RVA: 0x0B2ABF1C  token: 0x60000E9
        public virtual System.Void OnBeforeSerialize() { }
        // RVA: 0x0B2AB480  token: 0x60000EA
        private UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.BakingSet CreateNewBakingSet(System.String name) { }
        // RVA: 0x0B2AB5E4  token: 0x60000EB
        private System.Void InitializeBakingSet(UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.BakingSet set, System.String name) { }
        // RVA: 0x0B2ACAAC  token: 0x60000EC
        private System.Void SyncBakingSetSettings() { }
        // RVA: 0x0B2AD0E4  token: 0x60000ED
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200003B  // size: 0x208
    public sealed struct RendererList
    {
        // Fields
        private static readonly UnityEngine.Rendering.ShaderTagId s_EmptyName;  // static @ 0x0
        public static readonly UnityEngine.Experimental.Rendering.RendererList nullRendererList;  // static @ 0x8
        private System.Boolean <isValid>k__BackingField;  // 0x10
        public UnityEngine.Rendering.CullingResults cullingResult;  // 0x18
        public UnityEngine.Rendering.DrawingSettings drawSettings;  // 0x28
        public UnityEngine.Rendering.FilteringSettings filteringSettings;  // 0x174
        public System.Nullable<UnityEngine.Rendering.RenderStateBlock> stateBlock;  // 0x194

        // Properties
        System.Boolean isValid { get; /* RVA: 0x04D883E0 */ set; /* RVA: 0x04D87180 */ }

        // Methods
        // RVA: 0x0B2B5828  token: 0x60000F4
        public static UnityEngine.Experimental.Rendering.RendererList Create(UnityEngine.Experimental.Rendering.RendererListDesc& desc) { }
        // RVA: 0x0B2B5DC4  token: 0x60000F5
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200003C  // size: 0xD0
    public sealed struct RendererListDesc
    {
        // Fields
        public UnityEngine.Rendering.SortingCriteria sortingCriteria;  // 0x10
        public UnityEngine.Rendering.PerObjectData rendererConfiguration;  // 0x14
        public UnityEngine.Rendering.RenderQueueRange renderQueueRange;  // 0x18
        public System.Nullable<UnityEngine.Rendering.RenderStateBlock> stateBlock;  // 0x20
        public UnityEngine.Material overrideMaterial;  // 0x90
        public System.Boolean excludeObjectMotionVectors;  // 0x98
        public System.Int32 layerMask;  // 0x9c
        public System.Int32 overrideMaterialPassIndex;  // 0xa0
        private UnityEngine.Rendering.CullingResults <cullingResult>k__BackingField;  // 0xa8
        private UnityEngine.Camera <camera>k__BackingField;  // 0xb8
        private UnityEngine.Rendering.ShaderTagId <passName>k__BackingField;  // 0xc0
        private UnityEngine.Rendering.ShaderTagId[] <passNames>k__BackingField;  // 0xc8

        // Properties
        UnityEngine.Rendering.CullingResults cullingResult { get; /* RVA: 0x04D8E900 */ set; /* RVA: 0x04DB8BA0 */ }
        UnityEngine.Camera camera { get; /* RVA: 0x04D86290 */ set; /* RVA: 0x04D84CC0 */ }
        UnityEngine.Rendering.ShaderTagId passName { get; /* RVA: 0x04D8F8E0 */ set; /* RVA: 0x04D8F8F0 */ }
        UnityEngine.Rendering.ShaderTagId[] passNames { get; /* RVA: 0x04D85EF0 */ set; /* RVA: 0x06AC5AF4 */ }

        // Methods
        // RVA: 0x0B2B57A8  token: 0x60000FE
        public System.Void .ctor(UnityEngine.Rendering.ShaderTagId passName, UnityEngine.Rendering.CullingResults cullingResult, UnityEngine.Camera camera) { }
        // RVA: 0x0B2B56F0  token: 0x60000FF
        public System.Void .ctor(UnityEngine.Rendering.ShaderTagId[] passNames, UnityEngine.Rendering.CullingResults cullingResult, UnityEngine.Camera camera) { }
        // RVA: 0x0B2B5650  token: 0x6000100
        public System.Boolean IsValid() { }

    }

}

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{

    // TypeToken: 0x200003D  // size: 0x14
    public sealed struct DepthAccess
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Experimental.Rendering.RenderGraphModule.DepthAccess Read;  // const
        public static UnityEngine.Experimental.Rendering.RenderGraphModule.DepthAccess Write;  // const
        public static UnityEngine.Experimental.Rendering.RenderGraphModule.DepthAccess ReadWrite;  // const

    }

    // TypeToken: 0x200003E  // size: 0x30
    public class RenderGraphContext
    {
        // Fields
        public UnityEngine.Rendering.ScriptableRenderContext renderContext;  // 0x10
        public UnityEngine.Rendering.CommandBuffer cmd;  // 0x18
        public UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphObjectPool renderGraphPool;  // 0x20
        public UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDefaultResources defaultResources;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000101
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003F  // size: 0x30
    public sealed struct RenderGraphParameters
    {
        // Fields
        public System.String executionName;  // 0x10
        public System.Int32 currentFrameIndex;  // 0x18
        public System.Boolean rendererListCulling;  // 0x1c
        public UnityEngine.Rendering.ScriptableRenderContext scriptableRenderContext;  // 0x20
        public UnityEngine.Rendering.CommandBuffer commandBuffer;  // 0x28

    }

    // TypeToken: 0x2000040  // size: 0x18
    public sealed struct RenderGraphExecution : System.IDisposable
    {
        // Fields
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph renderGraph;  // 0x10

        // Methods
        // RVA: 0x05392320  token: 0x6000102
        private System.Void .ctor(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph renderGraph) { }
        // RVA: 0x0B2AE004  token: 0x6000103
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x2000041  // size: 0x28
    public class RenderGraphDebugParams
    {
        // Fields
        private UnityEngine.Rendering.DebugUI.Widget[] m_DebugItems;  // 0x10
        private UnityEngine.Rendering.DebugUI.Panel m_DebugPanel;  // 0x18
        public System.Boolean clearRenderTargetsAtCreation;  // 0x20
        public System.Boolean clearRenderTargetsAtRelease;  // 0x21
        public System.Boolean disablePassCulling;  // 0x22
        public System.Boolean immediateMode;  // 0x23
        public System.Boolean enableLogging;  // 0x24
        public System.Boolean logFrameInformation;  // 0x25
        public System.Boolean logResources;  // 0x26

        // Methods
        // RVA: 0x0B2AD770  token: 0x6000104
        public System.Void RegisterDebug(System.String name, UnityEngine.Rendering.DebugUI.Panel debugPanel) { }
        // RVA: 0x0B2ADF94  token: 0x6000105
        public System.Void UnRegisterDebug(System.String name) { }
        // RVA: 0x041E1670  token: 0x6000106
        public System.Void .ctor() { }
        // RVA: 0x04D866B0  token: 0x6000107
        private System.Boolean <RegisterDebug>b__10_0() { }
        // RVA: 0x04D866D0  token: 0x6000108
        private System.Void <RegisterDebug>b__10_1(System.Boolean value) { }
        // RVA: 0x04D86AD0  token: 0x6000109
        private System.Boolean <RegisterDebug>b__10_2() { }
        // RVA: 0x04D86AE0  token: 0x600010A
        private System.Void <RegisterDebug>b__10_3(System.Boolean value) { }
        // RVA: 0x04D8E070  token: 0x600010B
        private System.Boolean <RegisterDebug>b__10_4() { }
        // RVA: 0x04D8E080  token: 0x600010C
        private System.Void <RegisterDebug>b__10_5(System.Boolean value) { }
        // RVA: 0x04D86870  token: 0x600010D
        private System.Boolean <RegisterDebug>b__10_6() { }
        // RVA: 0x04D86880  token: 0x600010E
        private System.Void <RegisterDebug>b__10_7(System.Boolean value) { }
        // RVA: 0x0B2ADEDC  token: 0x600010F
        private System.Void <RegisterDebug>b__10_8() { }
        // RVA: 0x0B2ADF38  token: 0x6000110
        private System.Void <RegisterDebug>b__10_9() { }

    }

    // TypeToken: 0x2000043
    public sealed class RenderFunc`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000112
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000113
        public virtual System.Void Invoke(PassData data, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext renderGraphContext) { }
        // RVA: -1  // runtime  token: 0x6000114
        public virtual System.IAsyncResult BeginInvoke(PassData data, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext renderGraphContext, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x6000115
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000044  // size: 0x20
    public class RenderGraphDebugData
    {
        // Fields
        public System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDebugData.PassDebugData> passList;  // 0x10
        public System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDebugData.ResourceDebugData>[] resourceLists;  // 0x18

        // Methods
        // RVA: 0x0B2AD590  token: 0x6000116
        public System.Void Clear() { }
        // RVA: 0x0B2AD6D0  token: 0x6000117
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000047  // size: 0xC0
    public class RenderGraph
    {
        // Fields
        public static readonly System.Int32 kMaxMRTCount;  // static @ 0x0
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceRegistry m_Resources;  // 0x10
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphObjectPool m_RenderGraphPool;  // 0x18
        private System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPass> m_RenderPasses;  // 0x20
        private System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle> m_RendererLists;  // 0x28
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDebugParams m_DebugParameters;  // 0x30
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger m_FrameInformationLogger;  // 0x38
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDefaultResources m_DefaultResources;  // 0x40
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Rendering.ProfilingSampler> m_DefaultProfilingSamplers;  // 0x48
        private System.Boolean m_ExecutionExceptionWasRaised;  // 0x50
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext m_RenderGraphContext;  // 0x58
        private UnityEngine.Rendering.CommandBuffer m_PreviousCommandBuffer;  // 0x60
        private System.Int32 m_CurrentImmediatePassIndex;  // 0x68
        private System.Collections.Generic.List<System.Int32>[] m_ImmediateModeResourceList;  // 0x70
        private UnityEngine.Rendering.DynamicArray<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledResourceInfo>[] m_CompiledResourcesInfos;  // 0x78
        private UnityEngine.Rendering.DynamicArray<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo> m_CompiledPassInfos;  // 0x80
        private System.Collections.Generic.Stack<System.Int32> m_CullingStack;  // 0x88
        private System.Int32 m_ExecutionCount;  // 0x90
        private System.Int32 m_CurrentFrameIndex;  // 0x94
        private System.Boolean m_HasRenderGraphBegun;  // 0x98
        private System.String m_CurrentExecutionName;  // 0xa0
        private System.Boolean m_RendererListCulling;  // 0xa8
        private System.Collections.Generic.Dictionary<System.String,UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDebugData> m_DebugData;  // 0xb0
        private static System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph> s_RegisteredGraphs;  // static @ 0x8
        private System.String <name>k__BackingField;  // 0xb8
        private static System.Boolean <requireDebugData>k__BackingField;  // static @ 0x10
        private static UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.OnGraphRegisteredDelegate onGraphRegistered;  // static @ 0x18
        private static UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.OnGraphRegisteredDelegate onGraphUnregistered;  // static @ 0x20
        private static UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.OnExecutionRegisteredDelegate onExecutionRegistered;  // static @ 0x28
        private static UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.OnExecutionRegisteredDelegate onExecutionUnregistered;  // static @ 0x30

        // Properties
        System.String name { get; /* RVA: 0x04D85EF0 */ set; /* RVA: 0x06AC5AF4 */ }
        System.Boolean requireDebugData { get; /* RVA: 0x0B2B525C */ set; /* RVA: 0x0B2B55FC */ }
        UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDefaultResources defaultResources { get; /* RVA: 0x04D85A60 */ }

        // Events
        event UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.OnGraphRegisteredDelegate onGraphRegistered;
        event UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.OnGraphRegisteredDelegate onGraphUnregistered;
        event UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.OnExecutionRegisteredDelegate onExecutionRegistered;
        event UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.OnExecutionRegisteredDelegate onExecutionUnregistered;

        // Methods
        // RVA: 0x0B2B4A08  token: 0x600011D
        public System.Void .ctor(System.String name) { }
        // RVA: 0x0B2AE638  token: 0x600011E
        public System.Void Cleanup() { }
        // RVA: 0x0B2B3A38  token: 0x600011F
        public System.Void RegisterDebug(UnityEngine.Rendering.DebugUI.Panel panel) { }
        // RVA: 0x0B2B3D90  token: 0x6000120
        public System.Void UnRegisterDebug() { }
        // RVA: 0x0B2B21B4  token: 0x6000121
        public static System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph> GetRegisteredRenderGraphs() { }
        // RVA: 0x0B2B1CB0  token: 0x6000122
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDebugData GetDebugData(System.String executionName) { }
        // RVA: 0x0B2B028C  token: 0x6000123
        public System.Void EndFrame() { }
        // RVA: 0x0B2B22E4  token: 0x6000124
        public UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle ImportTexture(UnityEngine.Rendering.RTHandle rt) { }
        // RVA: 0x0B2B2268  token: 0x6000125
        public UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle ImportBackbuffer(UnityEngine.Rendering.RenderTargetIdentifier rt) { }
        // RVA: 0x0B2AFB10  token: 0x6000126
        public UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle CreateTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc& desc) { }
        // RVA: 0x0B2AF9F4  token: 0x6000127
        public UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle CreateSharedTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc& desc, System.Boolean explicitRelease) { }
        // RVA: 0x0B2B3C04  token: 0x6000128
        public System.Void ReleaseSharedTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle texture) { }
        // RVA: 0x0B2AFB50  token: 0x6000129
        public UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle CreateTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle texture) { }
        // RVA: 0x0B2AFA84  token: 0x600012A
        public System.Void CreateTextureIfInvalid(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc& desc, UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle& texture) { }
        // RVA: 0x0B2B2204  token: 0x600012B
        public UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc GetTextureDesc(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle texture) { }
        // RVA: 0x0B2AF904  token: 0x600012C
        public UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle CreateRendererList(UnityEngine.Rendering.RendererUtils.RendererListDesc& desc) { }
        // RVA: 0x0B2B22C4  token: 0x600012D
        public UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle ImportComputeBuffer(UnityEngine.ComputeBuffer computeBuffer) { }
        // RVA: 0x0B2AF8DC  token: 0x600012E
        public UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle CreateComputeBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferDesc& desc) { }
        // RVA: 0x0B2AF884  token: 0x600012F
        public UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle CreateComputeBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle& computeBuffer) { }
        // RVA: 0x0B2B1C70  token: 0x6000130
        public UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferDesc GetComputeBufferDesc(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle& computeBuffer) { }
        // RVA: -1  // generic def  token: 0x6000131
        public UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphBuilder AddRenderPass(System.String passName, PassData& passData, UnityEngine.Rendering.ProfilingSampler sampler) { }
        // RVA: -1  // generic def  token: 0x6000132
        public UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphBuilder AddRenderPass(System.String passName, PassData& passData) { }
        // RVA: 0x0B2B377C  token: 0x6000133
        public UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphExecution RecordAndExecute(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphParameters& parameters) { }
        // RVA: 0x0B2B0A88  token: 0x6000134
        private System.Void Execute() { }
        // RVA: 0x0B2AE1F0  token: 0x6000135
        public System.Void BeginProfilingSampler(UnityEngine.Rendering.ProfilingSampler sampler) { }
        // RVA: 0x0B2B034C  token: 0x6000136
        public System.Void EndProfilingSampler(UnityEngine.Rendering.ProfilingSampler sampler) { }
        // RVA: 0x04D86260  token: 0x6000137
        private UnityEngine.Rendering.DynamicArray<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo> GetCompiledPassInfos() { }
        // RVA: 0x0B2AE700  token: 0x6000138
        private System.Void ClearCompiledGraph() { }
        // RVA: 0x0B2B252C  token: 0x6000139
        private System.Void InvalidateContext() { }
        // RVA: 0x0B2B2D68  token: 0x600013A
        private System.Void OnPassAdded(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPass pass) { }
        // RVA: 0x0B2B2318  token: 0x6000143
        private System.Void InitResourceInfosData(UnityEngine.Rendering.DynamicArray<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledResourceInfo> resourceInfos, System.Int32 count) { }
        // RVA: 0x0B2B23B8  token: 0x6000144
        private System.Void InitializeCompilationData() { }
        // RVA: 0x0B2AF230  token: 0x6000145
        private System.Void CountReferences() { }
        // RVA: 0x0B2AFCEC  token: 0x6000146
        private System.Void CullUnusedPasses() { }
        // RVA: 0x0B2B3F1C  token: 0x6000147
        private System.Void UpdatePassSynchronization(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo& currentPassInfo, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo& producerPassInfo, System.Int32 currentPassIndex, System.Int32 lastProducer, System.Int32& intLastSyncIndex) { }
        // RVA: 0x0B2B4830  token: 0x6000148
        private System.Void UpdateResourceSynchronization(System.Int32& lastGraphicsPipeSync, System.Int32& lastComputePipeSync, System.Int32 currentPassIndex, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledResourceInfo& resource) { }
        // RVA: 0x0B2B1EE0  token: 0x6000149
        private System.Int32 GetLatestProducerIndex(System.Int32 passIndex, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledResourceInfo& info) { }
        // RVA: 0x0B2B2014  token: 0x600014A
        private System.Int32 GetLatestValidReadIndex(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledResourceInfo& info) { }
        // RVA: 0x0B2B1E10  token: 0x600014B
        private System.Int32 GetFirstValidWriteIndex(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledResourceInfo& info) { }
        // RVA: 0x0B2B20E4  token: 0x600014C
        private System.Int32 GetLatestValidWriteIndex(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledResourceInfo& info) { }
        // RVA: 0x0B2AF924  token: 0x600014D
        private System.Void CreateRendererLists() { }
        // RVA: 0x0B2B3F3C  token: 0x600014E
        private System.Void UpdateResourceAllocationAndSynchronization() { }
        // RVA: 0x0B2AE024  token: 0x600014F
        private System.Boolean AreRendererListsEmpty(System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle> rendererLists) { }
        // RVA: 0x0B2B3C8C  token: 0x6000150
        private System.Void TryCullPassAtIndex(System.Int32 passIndex) { }
        // RVA: 0x0B2AFBF4  token: 0x6000151
        private System.Void CullRendererLists() { }
        // RVA: 0x0B2AF138  token: 0x6000152
        private System.Void CompileRenderGraph() { }
        // RVA: 0x0B2AE948  token: 0x6000153
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo& CompilePassImmediatly(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPass pass) { }
        // RVA: 0x0B2B0954  token: 0x6000154
        private System.Void ExecutePassImmediatly(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPass pass) { }
        // RVA: 0x0B2B05E0  token: 0x6000155
        private System.Void ExecuteCompiledPass(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo& passInfo, System.Int32 passIndex) { }
        // RVA: 0x0B2B0980  token: 0x6000156
        private System.Void ExecuteRenderGraph() { }
        // RVA: 0x0B2B3378  token: 0x6000157
        private System.Void PreRenderPassSetRenderTargets(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo& passInfo, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext rgContext) { }
        // RVA: 0x0B2B3038  token: 0x6000158
        private System.Void PreRenderPassExecute(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo& passInfo, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext rgContext) { }
        // RVA: 0x0B2B2D94  token: 0x6000159
        private System.Void PostRenderPassExecute(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo& passInfo, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext rgContext) { }
        // RVA: 0x0B2AE7D0  token: 0x600015A
        private System.Void ClearRenderPasses() { }
        // RVA: 0x0B2B3A68  token: 0x600015B
        private System.Void ReleaseImmediateModeResources() { }
        // RVA: 0x0B2B2848  token: 0x600015C
        private System.Void LogFrameInformation() { }
        // RVA: 0x0B2B2C78  token: 0x600015D
        private System.Void LogRendererListsCreation() { }
        // RVA: 0x0B2B29BC  token: 0x600015E
        private System.Void LogRenderPassBegin(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo& passInfo) { }
        // RVA: 0x0B2B2584  token: 0x600015F
        private System.Void LogCulledPasses() { }
        // RVA: 0x0B2B1D20  token: 0x6000160
        private UnityEngine.Rendering.ProfilingSampler GetDefaultProfilingSampler(System.String name) { }
        // RVA: 0x0B2B3DBC  token: 0x6000161
        private System.Void UpdateImportedResourceLifeTime(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDebugData.ResourceDebugData& data, System.Collections.Generic.List<System.Int32> passList) { }
        // RVA: 0x0B2B0CB0  token: 0x6000162
        private System.Void GenerateDebugData() { }
        // RVA: 0x0B2AE484  token: 0x6000163
        private System.Void CleanupDebugData() { }
        // RVA: 0x0B2B4950  token: 0x6000164
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200004E  // size: 0x28
    public sealed struct RenderGraphProfilingScope : System.IDisposable
    {
        // Fields
        private System.Boolean m_Disposed;  // 0x10
        private UnityEngine.Rendering.ProfilingSampler m_Sampler;  // 0x18
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph m_RenderGraph;  // 0x20

        // Methods
        // RVA: 0x0B2C314C  token: 0x6000175
        public System.Void .ctor(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph renderGraph, UnityEngine.Rendering.ProfilingSampler sampler) { }
        // RVA: 0x0B2C3140  token: 0x6000176
        public virtual System.Void Dispose() { }
        // RVA: 0x0B2C3108  token: 0x6000177
        private System.Void Dispose(System.Boolean disposing) { }

    }

    // TypeToken: 0x200004F  // size: 0x30
    public sealed struct RenderGraphBuilder : System.IDisposable
    {
        // Fields
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPass m_RenderPass;  // 0x10
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceRegistry m_Resources;  // 0x18
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph m_RenderGraph;  // 0x20
        private System.Boolean m_Disposed;  // 0x28

        // Methods
        // RVA: 0x0B2C18D4  token: 0x6000178
        public UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle UseColorBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle& input, System.Int32 index) { }
        // RVA: 0x0B2C1950  token: 0x6000179
        public UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle UseDepthBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle& input, UnityEngine.Experimental.Rendering.RenderGraphModule.DepthAccess flags) { }
        // RVA: 0x0B2C1694  token: 0x600017A
        public UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle ReadTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle& input) { }
        // RVA: 0x0B2C1A64  token: 0x600017B
        public UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle WriteTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle& input) { }
        // RVA: 0x0B2C1860  token: 0x600017C
        public UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle ReadWriteTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle& input) { }
        // RVA: 0x0B2C148C  token: 0x600017D
        public UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle CreateTransientTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc& desc) { }
        // RVA: 0x0B2C14F8  token: 0x600017E
        public UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle CreateTransientTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle& texture) { }
        // RVA: 0x0B2C15C8  token: 0x600017F
        public UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle UseRendererList(UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle& input) { }
        // RVA: 0x0B2C166C  token: 0x6000180
        public UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle ReadComputeBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle& input) { }
        // RVA: 0x0B2C1A1C  token: 0x6000181
        public UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle WriteComputeBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle& input) { }
        // RVA: 0x0B2C1438  token: 0x6000182
        public UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle CreateTransientComputeBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferDesc& desc) { }
        // RVA: 0x0B2C13B4  token: 0x6000183
        public UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle CreateTransientComputeBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle& computebuffer) { }
        // RVA: -1  // generic def  token: 0x6000184
        public System.Void SetRenderFunc(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderFunc<PassData> renderFunc) { }
        // RVA: 0x0B2C1634  token: 0x6000185
        public System.Void EnableAsyncCompute(System.Boolean value) { }
        // RVA: 0x0B2C137C  token: 0x6000186
        public System.Void AllowPassCulling(System.Boolean value) { }
        // RVA: 0x0B2C1628  token: 0x6000187
        public virtual System.Void Dispose() { }
        // RVA: 0x0B2C1398  token: 0x6000188
        public System.Void AllowRendererListCulling(System.Boolean value) { }
        // RVA: 0x0B2C15C8  token: 0x6000189
        public UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle DependsOn(UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle& input) { }
        // RVA: 0x09B369D8  token: 0x600018A
        private System.Void .ctor(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPass renderPass, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceRegistry resources, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph renderGraph) { }
        // RVA: 0x0B2C15F4  token: 0x600018B
        private System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x041E1670  token: 0x600018C
        private System.Void CheckResource(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res, System.Boolean dontCheckTransientReadWrite) { }
        // RVA: 0x0B2C1650  token: 0x600018D
        private System.Void GenerateDebugData(System.Boolean value) { }

    }

    // TypeToken: 0x2000050  // size: 0xD0
    public class RenderGraphDefaultResources
    {
        // Fields
        private System.Boolean m_IsValid;  // 0x10
        private UnityEngine.Rendering.RTHandle m_BlackTexture2D;  // 0x18
        private UnityEngine.Rendering.RTHandle m_WhiteTexture2D;  // 0x20
        private UnityEngine.Rendering.RTHandle m_ShadowTexture2D;  // 0x28
        private UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle <blackTexture>k__BackingField;  // 0x30
        private UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle <whiteTexture>k__BackingField;  // 0x40
        private UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle <clearTextureXR>k__BackingField;  // 0x50
        private UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle <magentaTextureXR>k__BackingField;  // 0x60
        private UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle <blackTextureXR>k__BackingField;  // 0x70
        private UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle <blackTextureArrayXR>k__BackingField;  // 0x80
        private UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle <blackUIntTextureXR>k__BackingField;  // 0x90
        private UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle <blackTexture3DXR>k__BackingField;  // 0xa0
        private UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle <whiteTextureXR>k__BackingField;  // 0xb0
        private UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle <defaultShadowTexture>k__BackingField;  // 0xc0

        // Properties
        UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle blackTexture { get; /* RVA: 0x04D87480 */ set; /* RVA: 0x04D87490 */ }
        UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle whiteTexture { get; /* RVA: 0x04D905C0 */ set; /* RVA: 0x04D926F0 */ }
        UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle clearTextureXR { get; /* RVA: 0x04D8CE10 */ set; /* RVA: 0x04D906B0 */ }
        UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle magentaTextureXR { get; /* RVA: 0x04DA1280 */ set; /* RVA: 0x04DA1330 */ }
        UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle blackTextureXR { get; /* RVA: 0x04D8FD40 */ set; /* RVA: 0x04DA1310 */ }
        UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle blackTextureArrayXR { get; /* RVA: 0x04D90620 */ set; /* RVA: 0x04D9DEE0 */ }
        UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle blackUIntTextureXR { get; /* RVA: 0x04D9DC90 */ set; /* RVA: 0x04DA12D0 */ }
        UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle blackTexture3DXR { get; /* RVA: 0x04DA1260 */ set; /* RVA: 0x04DA1300 */ }
        UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle whiteTextureXR { get; /* RVA: 0x04DA1230 */ set; /* RVA: 0x04DA12B0 */ }
        UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle defaultShadowTexture { get; /* RVA: 0x04DA1220 */ set; /* RVA: 0x04DA12A0 */ }

        // Methods
        // RVA: 0x0B2C1CA4  token: 0x60001A2
        private System.Void .ctor() { }
        // RVA: 0x0B2C1AC4  token: 0x60001A3
        private System.Void Cleanup() { }
        // RVA: 0x0B2C1B08  token: 0x60001A4
        private System.Void InitializeForRendering(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph renderGraph) { }

    }

    // TypeToken: 0x2000051  // size: 0x28
    public sealed struct RenderGraphLogIndent : System.IDisposable
    {
        // Fields
        private System.Int32 m_Indentation;  // 0x10
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger m_Logger;  // 0x18
        private System.Boolean m_Disposed;  // 0x20

        // Methods
        // RVA: 0x0B2C1E08  token: 0x60001A5
        public System.Void .ctor(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger logger, System.Int32 indentation) { }
        // RVA: 0x0B2C1DC8  token: 0x60001A6
        public virtual System.Void Dispose() { }
        // RVA: 0x0B2C1DD4  token: 0x60001A7
        private System.Void Dispose(System.Boolean disposing) { }

    }

    // TypeToken: 0x2000052  // size: 0x28
    public class RenderGraphLogger
    {
        // Fields
        private System.Collections.Generic.Dictionary<System.String,System.Text.StringBuilder> m_LogMap;  // 0x10
        private System.Text.StringBuilder m_CurrentBuilder;  // 0x18
        private System.Int32 m_CurrentIndentation;  // 0x20

        // Methods
        // RVA: 0x0B2C2134  token: 0x60001A8
        public System.Void Initialize(System.String logName) { }
        // RVA: 0x0B2C20D8  token: 0x60001A9
        public System.Void IncrementIndentation(System.Int32 value) { }
        // RVA: 0x0B2C1E44  token: 0x60001AA
        public System.Void DecrementIndentation(System.Int32 value) { }
        // RVA: 0x0B2C2224  token: 0x60001AB
        public System.Void LogLine(System.String format, System.Object[] args) { }
        // RVA: 0x0B2C2048  token: 0x60001AC
        public System.String GetLog(System.String logName) { }
        // RVA: 0x0B2C1EB0  token: 0x60001AD
        public System.String GetAllLogs() { }
        // RVA: 0x0B2C22A8  token: 0x60001AE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000053  // size: 0x28
    public sealed class RenderGraphObjectPool
    {
        // Fields
        private System.Collections.Generic.Dictionary<System.ValueTuple<System.Type,System.Int32>,System.Collections.Generic.Stack<System.Object>> m_ArrayPool;  // 0x10
        private System.Collections.Generic.List<System.ValueTuple<System.Object,System.ValueTuple<System.Type,System.Int32>>> m_AllocatedArrays;  // 0x18
        private System.Collections.Generic.List<UnityEngine.MaterialPropertyBlock> m_AllocatedMaterialPropertyBlocks;  // 0x20

        // Methods
        // RVA: 0x0B2C2800  token: 0x60001AF
        private System.Void .ctor() { }
        // RVA: -1  // generic def  token: 0x60001B0
        public T[] GetTempArray(System.Int32 size) { }
        // RVA: 0x0B2C23C8  token: 0x60001B1
        public UnityEngine.MaterialPropertyBlock GetTempMaterialPropertyBlock() { }
        // RVA: 0x0B2C2484  token: 0x60001B2
        private System.Void ReleaseAllTempAlloc() { }
        // RVA: -1  // generic def  token: 0x60001B3
        private T Get() { }
        // RVA: -1  // generic def  token: 0x60001B4
        private System.Void Release(T value) { }
        // RVA: 0x0B2C2320  token: 0x60001B5
        private System.Void Cleanup() { }

    }

    // TypeToken: 0x2000056  // size: 0x80
    public abstract class RenderGraphPass
    {
        // Fields
        private System.String <name>k__BackingField;  // 0x10
        private System.Int32 <index>k__BackingField;  // 0x18
        private UnityEngine.Rendering.ProfilingSampler <customSampler>k__BackingField;  // 0x20
        private System.Boolean <enableAsyncCompute>k__BackingField;  // 0x28
        private System.Boolean <allowPassCulling>k__BackingField;  // 0x29
        private UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle <depthBuffer>k__BackingField;  // 0x2c
        private UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle[] <colorBuffers>k__BackingField;  // 0x40
        private System.Int32 <colorBufferMaxIndex>k__BackingField;  // 0x48
        private System.Int32 <refCount>k__BackingField;  // 0x4c
        private System.Boolean <generateDebugData>k__BackingField;  // 0x50
        private System.Boolean <allowRendererListCulling>k__BackingField;  // 0x51
        public System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle>[] resourceReadLists;  // 0x58
        public System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle>[] resourceWriteLists;  // 0x60
        public System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle>[] transientResourceList;  // 0x68
        public System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle> usedRendererListList;  // 0x70
        public System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle> dependsOnRendererListList;  // 0x78

        // Properties
        System.String name { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.Int32 index { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        UnityEngine.Rendering.ProfilingSampler customSampler { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.Boolean enableAsyncCompute { get; /* RVA: 0x011F33C0 */ set; /* RVA: 0x011F33D0 */ }
        System.Boolean allowPassCulling { get; /* RVA: 0x04D867B0 */ set; /* RVA: 0x04D867C0 */ }
        UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle depthBuffer { get; /* RVA: 0x04D8D370 */ set; /* RVA: 0x04D8D3A0 */ }
        UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle[] colorBuffers { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        System.Int32 colorBufferMaxIndex { get; /* RVA: 0x04D863D0 */ set; /* RVA: 0x04D86450 */ }
        System.Int32 refCount { get; /* RVA: 0x04D86600 */ set; /* RVA: 0x04D86630 */ }
        System.Boolean generateDebugData { get; /* RVA: 0x04D86800 */ set; /* RVA: 0x04D86840 */ }
        System.Boolean allowRendererListCulling { get; /* RVA: 0x04D867F0 */ set; /* RVA: 0x04D86830 */ }

        // Methods
        // RVA: -1  // generic def  token: 0x60001C1
        public UnityEngine.Experimental.Rendering.RenderGraphModule.RenderFunc<PassData> GetExecuteDelegate() { }
        // RVA: -1  // abstract  token: 0x60001C2
        public virtual System.Void Execute(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext renderGraphContext) { }
        // RVA: -1  // abstract  token: 0x60001C3
        public virtual System.Void Release(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphObjectPool pool) { }
        // RVA: -1  // abstract  token: 0x60001C4
        public virtual System.Boolean HasRenderFunc() { }
        // RVA: 0x0B2C2E80  token: 0x60001DB
        public System.Void .ctor() { }
        // RVA: 0x0B2C2AD4  token: 0x60001DC
        public System.Void Clear() { }
        // RVA: 0x0B2C29A4  token: 0x60001DD
        public System.Void AddResourceWrite(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res) { }
        // RVA: 0x0B2C290C  token: 0x60001DE
        public System.Void AddResourceRead(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res) { }
        // RVA: 0x0B2C2A3C  token: 0x60001DF
        public System.Void AddTransientResource(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res) { }
        // RVA: 0x0B2C2E2C  token: 0x60001E0
        public System.Void UseRendererList(UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle rendererList) { }
        // RVA: 0x0B2C2CE8  token: 0x60001E1
        public System.Void DependsOnRendererList(UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle rendererList) { }
        // RVA: 0x011F33D0  token: 0x60001E2
        public System.Void EnableAsyncCompute(System.Boolean value) { }
        // RVA: 0x04D867C0  token: 0x60001E3
        public System.Void AllowPassCulling(System.Boolean value) { }
        // RVA: 0x04D86830  token: 0x60001E4
        public System.Void AllowRendererListCulling(System.Boolean value) { }
        // RVA: 0x04D86840  token: 0x60001E5
        public System.Void GenerateDebugData(System.Boolean value) { }
        // RVA: 0x0B2C2D3C  token: 0x60001E6
        public System.Void SetColorBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle resource, System.Int32 index) { }
        // RVA: 0x0B2C2DD8  token: 0x60001E7
        public System.Void SetDepthBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle resource, UnityEngine.Experimental.Rendering.RenderGraphModule.DepthAccess flags) { }

    }

    // TypeToken: 0x2000057
    public sealed class RenderGraphPass`1 : UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPass
    {
        // Fields
        private PassData data;  // 0x0
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderFunc<PassData> renderFunc;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001E8
        public virtual System.Void Execute(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext renderGraphContext) { }
        // RVA: -1  // not resolved  token: 0x60001E9
        public System.Void Initialize(System.Int32 passIndex, PassData passData, System.String passName, UnityEngine.Rendering.ProfilingSampler sampler) { }
        // RVA: -1  // not resolved  token: 0x60001EA
        public virtual System.Void Release(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphObjectPool pool) { }
        // RVA: -1  // not resolved  token: 0x60001EB
        public virtual System.Boolean HasRenderFunc() { }
        // RVA: -1  // not resolved  token: 0x60001EC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000058  // size: 0x14
    public sealed struct RenderGraphProfileId
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphProfileId CompileRenderGraph;  // const
        public static UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphProfileId ExecuteRenderGraph;  // const
        public static UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphProfileId RenderGraphClear;  // const
        public static UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphProfileId RenderGraphClearDebug;  // const

    }

    // TypeToken: 0x2000059  // size: 0x18
    public sealed struct ComputeBufferHandle
    {
        // Fields
        private static UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle s_NullHandle;  // static @ 0x0
        private UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle handle;  // 0x10

        // Properties
        UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle nullHandle { get; /* RVA: 0x0B2B7D20 */ }

        // Methods
        // RVA: 0x0B2B7CE8  token: 0x60001EE
        private System.Void .ctor(System.Int32 handle, System.Boolean shared) { }
        // RVA: 0x0B2B7D70  token: 0x60001EF
        public static UnityEngine.ComputeBuffer op_Implicit(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle buffer) { }
        // RVA: 0x0B2B7C9C  token: 0x60001F0
        public System.Boolean IsValid() { }
        // RVA: 0x041E1670  token: 0x60001F1
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200005A  // size: 0x28
    public sealed struct ComputeBufferDesc
    {
        // Fields
        public System.Int32 count;  // 0x10
        public System.Int32 stride;  // 0x14
        public UnityEngine.ComputeBufferType type;  // 0x18
        public System.String name;  // 0x20

        // Methods
        // RVA: 0x04DA11E0  token: 0x60001F2
        public System.Void .ctor(System.Int32 count, System.Int32 stride) { }
        // RVA: 0x04DA11C0  token: 0x60001F3
        public System.Void .ctor(System.Int32 count, System.Int32 stride, UnityEngine.ComputeBufferType type) { }
        // RVA: 0x04DB8C70  token: 0x60001F4
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x200005B  // size: 0x50
    public class ComputeBufferResource : UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResource`2
    {
        // Methods
        // RVA: 0x0B2B859C  token: 0x60001F5
        public virtual System.String GetName() { }
        // RVA: 0x0B2B8448  token: 0x60001F6
        public virtual System.Void CreatePooledGraphicsResource() { }
        // RVA: 0x0B2B8720  token: 0x60001F7
        public virtual System.Void ReleasePooledGraphicsResource(System.Int32 frameIndex) { }
        // RVA: 0x0B2B82E0  token: 0x60001F8
        public virtual System.Void CreateGraphicsResource(System.String name) { }
        // RVA: 0x0B2B86DC  token: 0x60001F9
        public virtual System.Void ReleaseGraphicsResource() { }
        // RVA: 0x0B2B85DC  token: 0x60001FA
        public virtual System.Void LogCreation(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger logger) { }
        // RVA: 0x0B2B865C  token: 0x60001FB
        public virtual System.Void LogRelease(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger logger) { }
        // RVA: 0x0B2B885C  token: 0x60001FC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005C  // size: 0x28
    public class ComputeBufferPool : UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourcePool`1
    {
        // Methods
        // RVA: 0x0B2B8284  token: 0x60001FD
        protected virtual System.Void ReleaseInternalResource(UnityEngine.ComputeBuffer res) { }
        // RVA: 0x0B2B7DDC  token: 0x60001FE
        protected virtual System.String GetResourceName(UnityEngine.ComputeBuffer res) { }
        // RVA: 0x0B2B7E08  token: 0x60001FF
        protected virtual System.Int64 GetResourceSize(UnityEngine.ComputeBuffer res) { }
        // RVA: 0x0B2B7E48  token: 0x6000200
        protected virtual System.String GetResourceTypeName() { }
        // RVA: 0x09878C7C  token: 0x6000201
        protected virtual System.Int32 GetSortIndex(UnityEngine.ComputeBuffer res) { }
        // RVA: 0x0B2B7E74  token: 0x6000202
        public virtual System.Void PurgeUnusedResources(System.Int32 currentFrameIndex) { }
        // RVA: 0x0B2B82A4  token: 0x6000203
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005D  // size: 0x10
    public abstract class IRenderGraphResourcePool
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000204
        public virtual System.Void PurgeUnusedResources(System.Int32 currentFrameIndex) { }
        // RVA: -1  // abstract  token: 0x6000205
        public virtual System.Void Cleanup() { }
        // RVA: -1  // abstract  token: 0x6000206
        public virtual System.Void CheckFrameAllocation(System.Boolean onException, System.Int32 frameIndex) { }
        // RVA: -1  // abstract  token: 0x6000207
        public virtual System.Void LogResources(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger logger) { }
        // RVA: 0x041E1670  token: 0x6000208
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200005E
    public abstract class RenderGraphResourcePool`1 : UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResourcePool
    {
        // Fields
        protected System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.SortedList<System.Int32,System.ValueTuple<Type,System.Int32>>> m_ResourcePool;  // 0x0
        protected System.Collections.Generic.List<System.Int32> m_RemoveList;  // 0x0
        private System.Collections.Generic.List<System.ValueTuple<System.Int32,Type>> m_FrameAllocatedResources;  // 0x0
        protected static System.Int32 s_CurrentFrameIndex;  // static @ 0x0
        private static System.Int32 kStaleResourceLifetime;  // const

        // Methods
        // RVA: -1  // abstract  token: 0x6000209
        protected virtual System.Void ReleaseInternalResource(Type res) { }
        // RVA: -1  // abstract  token: 0x600020A
        protected virtual System.String GetResourceName(Type res) { }
        // RVA: -1  // abstract  token: 0x600020B
        protected virtual System.Int64 GetResourceSize(Type res) { }
        // RVA: -1  // abstract  token: 0x600020C
        protected virtual System.String GetResourceTypeName() { }
        // RVA: -1  // abstract  token: 0x600020D
        protected virtual System.Int32 GetSortIndex(Type res) { }
        // RVA: -1  // not resolved  token: 0x600020E
        public System.Void ReleaseResource(System.Int32 hash, Type resource, System.Int32 currentFrameIndex) { }
        // RVA: -1  // not resolved  token: 0x600020F
        public System.Boolean TryGetResource(System.Int32 hashCode, Type& resource) { }
        // RVA: -1  // not resolved  token: 0x6000210
        public virtual System.Void Cleanup() { }
        // RVA: -1  // not resolved  token: 0x6000211
        public System.Void RegisterFrameAllocation(System.Int32 hash, Type value) { }
        // RVA: -1  // not resolved  token: 0x6000212
        public System.Void UnregisterFrameAllocation(System.Int32 hash, Type value) { }
        // RVA: -1  // not resolved  token: 0x6000213
        public virtual System.Void CheckFrameAllocation(System.Boolean onException, System.Int32 frameIndex) { }
        // RVA: -1  // not resolved  token: 0x6000214
        public virtual System.Void LogResources(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger logger) { }
        // RVA: -1  // not resolved  token: 0x6000215
        protected static System.Boolean ShouldReleaseResource(System.Int32 lastUsedFrameIndex, System.Int32 currentFrameIndex) { }
        // RVA: -1  // not resolved  token: 0x6000216
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000061  // size: 0x50
    public class RenderGraphResourceRegistry
    {
        // Fields
        private static System.Int32 kSharedResourceLifetime;  // const
        private static UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceRegistry m_CurrentRegistry;  // static @ 0x0
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceRegistry.RenderGraphResourcesData[] m_RenderGraphResources;  // 0x10
        private UnityEngine.Rendering.DynamicArray<UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListResource> m_RendererListResources;  // 0x18
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDebugParams m_RenderGraphDebug;  // 0x20
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger m_ResourceLogger;  // 0x28
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger m_FrameInformationLogger;  // 0x30
        private System.Int32 m_CurrentFrameIndex;  // 0x38
        private System.Int32 m_ExecutionCount;  // 0x3c
        private UnityEngine.Rendering.RTHandle m_CurrentBackbuffer;  // 0x40
        private static System.Int32 kInitialRendererListCount;  // const
        private System.Collections.Generic.List<UnityEngine.Rendering.RendererUtils.RendererList> m_ActiveRendererLists;  // 0x48

        // Properties
        UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceRegistry current { get; /* RVA: 0x0B2C5E60 */ set; /* RVA: 0x0B2C5E98 */ }

        // Methods
        // RVA: 0x0B2C49D0  token: 0x600021C
        private UnityEngine.Rendering.RTHandle GetTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle& handle) { }
        // RVA: 0x0B2C58C0  token: 0x600021D
        private System.Boolean TextureNeedsFallback(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle& handle) { }
        // RVA: 0x0B2C46C0  token: 0x600021E
        private UnityEngine.Rendering.RendererUtils.RendererList GetRendererList(UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle& handle) { }
        // RVA: 0x0B2C43D4  token: 0x600021F
        private UnityEngine.ComputeBuffer GetComputeBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle& handle) { }
        // RVA: 0x0B2C5D38  token: 0x6000220
        private System.Void .ctor() { }
        // RVA: 0x0B2C59D4  token: 0x6000221
        private System.Void .ctor(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDebugParams renderGraphDebug, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger frameInformationLogger) { }
        // RVA: 0x0B2C31B4  token: 0x6000222
        private System.Void BeginRenderGraph(System.Int32 executionCount) { }
        // RVA: 0x0B2C3194  token: 0x6000223
        private System.Void BeginExecute(System.Int32 currentFrameIndex) { }
        // RVA: 0x0B2C40F0  token: 0x6000224
        private System.Void EndExecute() { }
        // RVA: 0x0B2C323C  token: 0x6000225
        private System.Void CheckHandleValidity(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res) { }
        // RVA: 0x0B2C3290  token: 0x6000226
        private System.Void CheckHandleValidity(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceType type, System.Int32 index) { }
        // RVA: 0x0B2C4E7C  token: 0x6000227
        private System.Void IncrementWriteCount(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res) { }
        // RVA: 0x0B2C44A4  token: 0x6000228
        private System.String GetRenderGraphResourceName(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res) { }
        // RVA: 0x0B2C4564  token: 0x6000229
        private System.String GetRenderGraphResourceName(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceType type, System.Int32 index) { }
        // RVA: 0x0B2C5094  token: 0x600022A
        private System.Boolean IsRenderGraphResourceImported(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res) { }
        // RVA: 0x0B2C5150  token: 0x600022B
        private System.Boolean IsRenderGraphResourceShared(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceType type, System.Int32 index) { }
        // RVA: 0x0B2C4F38  token: 0x600022C
        private System.Boolean IsGraphicsResourceCreated(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res) { }
        // RVA: 0x0B2C51AC  token: 0x600022D
        private System.Boolean IsRendererListCreated(UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle& res) { }
        // RVA: 0x0B2C4FF8  token: 0x600022E
        private System.Boolean IsRenderGraphResourceImported(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceType type, System.Int32 index) { }
        // RVA: 0x0B2C4604  token: 0x600022F
        private System.Int32 GetRenderGraphResourceTransientIndex(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res) { }
        // RVA: 0x0B2C4DAC  token: 0x6000230
        private UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle ImportTexture(UnityEngine.Rendering.RTHandle rt) { }
        // RVA: 0x0B2C3B2C  token: 0x6000231
        private UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle CreateSharedTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc& desc, System.Boolean explicitRelease) { }
        // RVA: 0x0B2C55F4  token: 0x6000232
        private System.Void ReleaseSharedTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle texture) { }
        // RVA: 0x0B2C4B7C  token: 0x6000233
        private UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle ImportBackbuffer(UnityEngine.Rendering.RenderTargetIdentifier rt) { }
        // RVA: 0x0B2C3FD8  token: 0x6000234
        private UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle CreateTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc& desc, System.Int32 transientPassIndex) { }
        // RVA: 0x0B2C478C  token: 0x6000235
        private System.Int32 GetTextureResourceCount() { }
        // RVA: 0x0B2C4910  token: 0x6000236
        private UnityEngine.Experimental.Rendering.RenderGraphModule.TextureResource GetTextureResource(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& handle) { }
        // RVA: 0x0B2C47E8  token: 0x6000237
        private UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc GetTextureResourceDesc(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& handle) { }
        // RVA: 0x0B2C4154  token: 0x6000238
        private System.Void ForceTextureClear(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& handle, UnityEngine.Color clearColor) { }
        // RVA: 0x0B2C36C0  token: 0x6000239
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle CreateRendererList(UnityEngine.Rendering.RendererUtils.RendererListDesc& desc) { }
        // RVA: 0x0B2C4CF0  token: 0x600023A
        private UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle ImportComputeBuffer(UnityEngine.ComputeBuffer computeBuffer) { }
        // RVA: 0x0B2C34D4  token: 0x600023B
        private UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle CreateComputeBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferDesc& desc, System.Int32 transientPassIndex) { }
        // RVA: 0x0B2C4210  token: 0x600023C
        private UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferDesc GetComputeBufferResourceDesc(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& handle) { }
        // RVA: 0x0B2C41B4  token: 0x600023D
        private System.Int32 GetComputeBufferResourceCount() { }
        // RVA: 0x0B2C4314  token: 0x600023E
        private UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferResource GetComputeBufferResource(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& handle) { }
        // RVA: 0x0B2C5944  token: 0x600023F
        private System.Void UpdateSharedResourceLastFrameIndex(System.Int32 type, System.Int32 index) { }
        // RVA: 0x0B2C5340  token: 0x6000240
        private System.Void ManageSharedRenderGraphResources() { }
        // RVA: 0x0B2C35B0  token: 0x6000241
        private System.Void CreatePooledResource(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext rgContext, System.Int32 type, System.Int32 index) { }
        // RVA: 0x0B2C3D34  token: 0x6000242
        private System.Void CreateTextureCallback(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext rgContext, UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResource res) { }
        // RVA: 0x0B2C54E0  token: 0x6000243
        private System.Void ReleasePooledResource(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext rgContext, System.Int32 type, System.Int32 index) { }
        // RVA: 0x0B2C571C  token: 0x6000244
        private System.Void ReleaseTextureCallback(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext rgContext, UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResource res) { }
        // RVA: 0x041E1670  token: 0x6000245
        private System.Void ValidateTextureDesc(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc& desc) { }
        // RVA: 0x041E1670  token: 0x6000246
        private System.Void ValidateRendererListDesc(UnityEngine.Rendering.RendererUtils.RendererListDesc& desc) { }
        // RVA: 0x041E1670  token: 0x6000247
        private System.Void ValidateComputeBufferDesc(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferDesc& desc) { }
        // RVA: 0x0B2C37F4  token: 0x6000248
        private System.Void CreateRendererLists(System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle> rendererLists, UnityEngine.Rendering.ScriptableRenderContext context, System.Boolean manualDispatch) { }
        // RVA: 0x0B2C3418  token: 0x6000249
        private System.Void Clear(System.Boolean onException) { }
        // RVA: 0x0B2C548C  token: 0x600024A
        private System.Void PurgeUnusedGraphicsResources() { }
        // RVA: 0x0B2C33B8  token: 0x600024B
        private System.Void Cleanup() { }
        // RVA: 0x0B2C40F8  token: 0x600024C
        private System.Void FlushLogs() { }
        // RVA: 0x0B2C5234  token: 0x600024D
        private System.Void LogResources() { }

    }

    // TypeToken: 0x2000064  // size: 0x18
    public sealed struct RendererListHandle
    {
        // Fields
        private System.Boolean m_IsValid;  // 0x10
        private System.Int32 <handle>k__BackingField;  // 0x14

        // Properties
        System.Int32 handle { get; /* RVA: 0x04D88B20 */ set; /* RVA: 0x04D88B50 */ }

        // Methods
        // RVA: 0x04D9D420  token: 0x6000259
        private System.Void .ctor(System.Int32 handle) { }
        // RVA: 0x04D9D8A0  token: 0x600025A
        public static System.Int32 op_Implicit(UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle handle) { }
        // RVA: 0x0B2C6088  token: 0x600025B
        public static UnityEngine.Rendering.RendererUtils.RendererList op_Implicit(UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle rendererList) { }
        // RVA: 0x04D883E0  token: 0x600025C
        public System.Boolean IsValid() { }

    }

    // TypeToken: 0x2000065  // size: 0x100
    public sealed struct RendererListResource
    {
        // Fields
        public UnityEngine.Rendering.RendererUtils.RendererListDesc desc;  // 0x10
        public UnityEngine.Rendering.RendererUtils.RendererList rendererList;  // 0xf0

        // Methods
        // RVA: 0x09B3F960  token: 0x600025D
        private System.Void .ctor(UnityEngine.Rendering.RendererUtils.RendererListDesc& desc) { }

    }

    // TypeToken: 0x2000066  // size: 0x14
    public sealed struct RenderGraphResourceType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceType Texture;  // const
        public static UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceType ComputeBuffer;  // const
        public static UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceType Count;  // const

    }

    // TypeToken: 0x2000067  // size: 0x18
    public sealed struct ResourceHandle
    {
        // Fields
        private static System.UInt32 kValidityMask;  // const
        private static System.UInt32 kIndexMask;  // const
        private System.UInt32 m_Value;  // 0x10
        private static System.UInt32 s_CurrentValidBit;  // static @ 0x0
        private static System.UInt32 s_SharedResourceValidBit;  // static @ 0x4
        private UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceType <type>k__BackingField;  // 0x14

        // Properties
        System.Int32 index { get; /* RVA: 0x04D87110 */ }
        UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceType type { get; /* RVA: 0x04D88B20 */ set; /* RVA: 0x04D88B50 */ }
        System.Int32 iType { get; /* RVA: 0x0B2C6330 */ }

        // Methods
        // RVA: 0x0B2C62A4  token: 0x6000262
        private System.Void .ctor(System.Int32 value, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceType type, System.Boolean shared) { }
        // RVA: 0x0B2C6374  token: 0x6000263
        public static System.Int32 op_Implicit(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle handle) { }
        // RVA: 0x0B2C6118  token: 0x6000264
        public System.Boolean IsValid() { }
        // RVA: 0x0B2C6190  token: 0x6000265
        public static System.Void NewFrame(System.Int32 executionIndex) { }
        // RVA: 0x0B2C6254  token: 0x6000266
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000068  // size: 0x30
    public class IRenderGraphResource
    {
        // Fields
        public System.Boolean imported;  // 0x10
        public System.Boolean shared;  // 0x11
        public System.Boolean sharedExplicitRelease;  // 0x12
        public System.Boolean requestFallBack;  // 0x13
        public System.UInt32 writeCount;  // 0x14
        public System.Int32 cachedHash;  // 0x18
        public System.Int32 transientPassIndex;  // 0x1c
        public System.Int32 sharedResourceLastFrameUsed;  // 0x20
        protected UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResourcePool m_Pool;  // 0x28

        // Methods
        // RVA: 0x0B2C12BC  token: 0x6000267
        public virtual System.Void Reset(UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResourcePool pool) { }
        // RVA: 0x0B2C127C  token: 0x6000268
        public virtual System.String GetName() { }
        // RVA: 0x012081B0  token: 0x6000269
        public virtual System.Boolean IsCreated() { }
        // RVA: 0x04DB8D00  token: 0x600026A
        public virtual System.Void IncrementWriteCount() { }
        // RVA: 0x0B2C12A8  token: 0x600026B
        public virtual System.Boolean NeedsFallBack() { }
        // RVA: 0x041E1670  token: 0x600026C
        public virtual System.Void CreatePooledGraphicsResource() { }
        // RVA: 0x041E1670  token: 0x600026D
        public virtual System.Void CreateGraphicsResource(System.String name) { }
        // RVA: 0x041E1670  token: 0x600026E
        public virtual System.Void ReleasePooledGraphicsResource(System.Int32 frameIndex) { }
        // RVA: 0x041E1670  token: 0x600026F
        public virtual System.Void ReleaseGraphicsResource() { }
        // RVA: 0x041E1670  token: 0x6000270
        public virtual System.Void LogCreation(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger logger) { }
        // RVA: 0x041E1670  token: 0x6000271
        public virtual System.Void LogRelease(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger logger) { }
        // RVA: 0x011EC580  token: 0x6000272
        public virtual System.Int32 GetSortIndex() { }
        // RVA: 0x041E1670  token: 0x6000273
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000069
    public abstract class RenderGraphResource`2 : UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResource
    {
        // Fields
        public DescType desc;  // 0x0
        public ResType graphicsResource;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000274
        protected System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000275
        public virtual System.Void Reset(UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResourcePool pool) { }
        // RVA: -1  // not resolved  token: 0x6000276
        public virtual System.Boolean IsCreated() { }
        // RVA: -1  // not resolved  token: 0x6000277
        public virtual System.Void ReleaseGraphicsResource() { }

    }

    // TypeToken: 0x200006A  // size: 0x20
    public sealed struct TextureHandle
    {
        // Fields
        private static UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle s_NullHandle;  // static @ 0x0
        private UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle handle;  // 0x10
        private UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle fallBackResource;  // 0x18

        // Properties
        UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle nullHandle { get; /* RVA: 0x0B2C6CBC */ }

        // Methods
        // RVA: 0x0B2C6C30  token: 0x6000279
        private System.Void .ctor(System.Int32 handle, System.Boolean shared) { }
        // RVA: 0x0B2C6E78  token: 0x600027A
        public static UnityEngine.Rendering.RenderTargetIdentifier op_Implicit(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle texture) { }
        // RVA: 0x0B2C6D18  token: 0x600027B
        public static UnityEngine.Texture op_Implicit(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle texture) { }
        // RVA: 0x0B2C6DFC  token: 0x600027C
        public static UnityEngine.RenderTexture op_Implicit(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle texture) { }
        // RVA: 0x0B2C6D94  token: 0x600027D
        public static UnityEngine.Rendering.RTHandle op_Implicit(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle texture) { }
        // RVA: 0x0B2C6BE4  token: 0x600027E
        public System.Boolean IsValid() { }
        // RVA: 0x04DB8D10  token: 0x600027F
        public System.Void SetFallBackResource(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle texture) { }
        // RVA: 0x041E1670  token: 0x6000280
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200006B  // size: 0x14
    public sealed struct TextureSizeMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Experimental.Rendering.RenderGraphModule.TextureSizeMode Explicit;  // const
        public static UnityEngine.Experimental.Rendering.RenderGraphModule.TextureSizeMode Scale;  // const
        public static UnityEngine.Experimental.Rendering.RenderGraphModule.TextureSizeMode Functor;  // const

    }

    // TypeToken: 0x200006C  // size: 0x1C
    public sealed struct FastMemoryDesc
    {
        // Fields
        public System.Boolean inFastMemory;  // 0x10
        public UnityEngine.Rendering.FastMemoryFlags flags;  // 0x14
        public System.Single residencyFraction;  // 0x18

    }

    // TypeToken: 0x200006D  // size: 0x70
    public sealed struct TextureDesc
    {
        // Fields
        public System.Int32 width;  // 0x10
        public System.Int32 height;  // 0x14
        public System.Int32 slices;  // 0x18
        public UnityEngine.Rendering.DepthBits depthBufferBits;  // 0x1c
        public UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat;  // 0x20
        public UnityEngine.FilterMode filterMode;  // 0x24
        public UnityEngine.TextureWrapMode wrapMode;  // 0x28
        public UnityEngine.Rendering.TextureDimension dimension;  // 0x2c
        public System.Boolean enableRandomWrite;  // 0x30
        public System.Boolean useMipMap;  // 0x31
        public System.Boolean autoGenerateMips;  // 0x32
        public System.Boolean isShadowMap;  // 0x33
        public System.Int32 anisoLevel;  // 0x34
        public System.Single mipMapBias;  // 0x38
        public UnityEngine.Rendering.MSAASamples msaaSamples;  // 0x3c
        public System.Boolean bindTextureMS;  // 0x40
        public UnityEngine.RenderTextureMemoryless memoryless;  // 0x44
        public System.String name;  // 0x48
        public UnityEngine.Experimental.Rendering.RenderGraphModule.FastMemoryDesc fastMemoryDesc;  // 0x50
        public System.Boolean fallBackToBlackTexture;  // 0x5c
        public System.Boolean clearBuffer;  // 0x5d
        public UnityEngine.Color clearColor;  // 0x60

        // Methods
        // RVA: 0x0B2C6B48  token: 0x6000281
        private System.Void InitDefaultValues(System.Boolean xrReady) { }
        // RVA: 0x0B2C6BB0  token: 0x6000282
        public System.Void .ctor(System.Int32 width, System.Int32 height, System.Boolean xrReady) { }
        // RVA: 0x09B419A0  token: 0x6000283
        public System.Void .ctor(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc input) { }
        // RVA: 0x0B2C6A4C  token: 0x6000284
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x200006E  // size: 0x98
    public class TextureResource : UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResource`2
    {
        // Fields
        private static System.Int32 m_TextureCreationIndex;  // static @ 0x0

        // Methods
        // RVA: 0x0B2C77A8  token: 0x6000285
        public virtual System.String GetName() { }
        // RVA: 0x0B2C7654  token: 0x6000286
        public virtual System.Void CreatePooledGraphicsResource() { }
        // RVA: 0x0B2C7984  token: 0x6000287
        public virtual System.Void ReleasePooledGraphicsResource(System.Int32 frameIndex) { }
        // RVA: 0x0B2C742C  token: 0x6000288
        public virtual System.Void CreateGraphicsResource(System.String name) { }
        // RVA: 0x0B2C7938  token: 0x6000289
        public virtual System.Void ReleaseGraphicsResource() { }
        // RVA: 0x0B2C7804  token: 0x600028A
        public virtual System.Void LogCreation(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger logger) { }
        // RVA: 0x0B2C78B8  token: 0x600028B
        public virtual System.Void LogRelease(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger logger) { }
        // RVA: 0x0B2C7ACC  token: 0x600028C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200006F  // size: 0x28
    public class TexturePool : UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourcePool`1
    {
        // Methods
        // RVA: 0x0B2C73D0  token: 0x600028D
        protected virtual System.Void ReleaseInternalResource(UnityEngine.Rendering.RTHandle res) { }
        // RVA: 0x0B2C6F2C  token: 0x600028E
        protected virtual System.String GetResourceName(UnityEngine.Rendering.RTHandle res) { }
        // RVA: 0x0B2C6F50  token: 0x600028F
        protected virtual System.Int64 GetResourceSize(UnityEngine.Rendering.RTHandle res) { }
        // RVA: 0x0B2C6F74  token: 0x6000290
        protected virtual System.String GetResourceTypeName() { }
        // RVA: 0x0B2C6FA0  token: 0x6000291
        protected virtual System.Int32 GetSortIndex(UnityEngine.Rendering.RTHandle res) { }
        // RVA: 0x0B2C6FC0  token: 0x6000292
        public virtual System.Void PurgeUnusedResources(System.Int32 currentFrameIndex) { }
        // RVA: 0x0B2C73F0  token: 0x6000293
        public System.Void .ctor() { }

    }

}

namespace UnityEngine.Rendering
{

    // TypeToken: 0x2000070  // size: 0x78
    public class CameraSwitcher : UnityEngine.MonoBehaviour
    {
        // Fields
        public UnityEngine.Camera[] m_Cameras;  // 0x18
        private System.Int32 m_CurrentCameraIndex;  // 0x20
        private UnityEngine.Camera m_OriginalCamera;  // 0x28
        private UnityEngine.Vector3 m_OriginalCameraPosition;  // 0x30
        private UnityEngine.Quaternion m_OriginalCameraRotation;  // 0x3c
        private UnityEngine.Camera m_CurrentCamera;  // 0x50
        private UnityEngine.GUIContent[] m_CameraNames;  // 0x58
        private System.Int32[] m_CameraIndices;  // 0x60
        private UnityEngine.Rendering.DebugUI.EnumField m_DebugEntry;  // 0x68
        private System.Int32 m_DebugEntryEnumIndex;  // 0x70

        // Methods
        // RVA: 0x0B2B7060  token: 0x6000294
        private System.Void OnEnable() { }
        // RVA: 0x0B2B6FEC  token: 0x6000295
        private System.Void OnDisable() { }
        // RVA: 0x0B2B6F8C  token: 0x6000296
        private System.Int32 GetCameraCount() { }
        // RVA: 0x0B2B6FAC  token: 0x6000297
        private UnityEngine.Camera GetNextCamera() { }
        // RVA: 0x0B2B75B4  token: 0x6000298
        private System.Void SetCameraIndex(System.Int32 index) { }
        // RVA: 0x0B2B7854  token: 0x6000299
        public System.Void .ctor() { }
        // RVA: 0x02B70F90  token: 0x600029A
        private System.Int32 <OnEnable>b__10_0() { }
        // RVA: 0x0B2B784C  token: 0x600029B
        private System.Void <OnEnable>b__10_1(System.Int32 value) { }
        // RVA: 0x04D868A0  token: 0x600029C
        private System.Int32 <OnEnable>b__10_2() { }
        // RVA: 0x04D868D0  token: 0x600029D
        private System.Void <OnEnable>b__10_3(System.Int32 value) { }

    }

    // TypeToken: 0x2000071  // size: 0x70
    public class FreeCamera : UnityEngine.MonoBehaviour
    {
        // Fields
        private static System.Single k_MouseSensitivityMultiplier;  // const
        public System.Single m_LookSpeedController;  // 0x18
        public System.Single m_LookSpeedMouse;  // 0x1c
        public System.Single m_MoveSpeed;  // 0x20
        public System.Single m_MoveSpeedIncrement;  // 0x24
        public System.Single m_Turbo;  // 0x28
        private UnityEngine.InputSystem.InputAction lookAction;  // 0x30
        private UnityEngine.InputSystem.InputAction moveAction;  // 0x38
        private UnityEngine.InputSystem.InputAction speedAction;  // 0x40
        private UnityEngine.InputSystem.InputAction yMoveAction;  // 0x48
        private System.Single inputRotateAxisX;  // 0x50
        private System.Single inputRotateAxisY;  // 0x54
        private System.Single inputChangeSpeed;  // 0x58
        private System.Single inputVertical;  // 0x5c
        private System.Single inputHorizontal;  // 0x60
        private System.Single inputYAxis;  // 0x64
        private System.Boolean leftShiftBoost;  // 0x68
        private System.Boolean leftShift;  // 0x69
        private System.Boolean fire1;  // 0x6a

        // Methods
        // RVA: 0x0B2C02DC  token: 0x600029E
        private System.Void OnEnable() { }
        // RVA: 0x0B2C02E4  token: 0x600029F
        private System.Void RegisterInputs() { }
        // RVA: 0x0B2C0A78  token: 0x60002A0
        private System.Void UpdateInputs() { }
        // RVA: 0x0B2C0C90  token: 0x60002A1
        private System.Void Update() { }
        // RVA: 0x0B2C1154  token: 0x60002A2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000072  // size: 0x10
    public static class CommandBufferPool
    {
        // Fields
        private static UnityEngine.Rendering.ObjectPool<UnityEngine.Rendering.CommandBuffer> s_BufferPool;  // static @ 0x0

        // Methods
        // RVA: 0x0B2B7914  token: 0x60002A3
        public static UnityEngine.Rendering.CommandBuffer Get() { }
        // RVA: 0x03339460  token: 0x60002A4
        public static UnityEngine.Rendering.CommandBuffer Get(System.String name) { }
        // RVA: 0x03339790  token: 0x60002A5
        public static System.Void Release(UnityEngine.Rendering.CommandBuffer buffer) { }
        // RVA: 0x04B083D0  token: 0x60002A6
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000074  // size: 0x14
    public sealed struct ClearFlag
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.ClearFlag None;  // const
        public static UnityEngine.Rendering.ClearFlag Color;  // const
        public static UnityEngine.Rendering.ClearFlag Depth;  // const
        public static UnityEngine.Rendering.ClearFlag Stencil;  // const
        public static UnityEngine.Rendering.ClearFlag DepthStencil;  // const
        public static UnityEngine.Rendering.ClearFlag ColorStencil;  // const
        public static UnityEngine.Rendering.ClearFlag All;  // const

    }

    // TypeToken: 0x2000075
    public static class ComponentSingleton`1
    {
        // Fields
        private static TType s_Instance;  // static @ 0x0

        // Properties
        TType instance { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60002AC
        public static System.Void Release() { }

    }

    // TypeToken: 0x2000076  // size: 0x10
    public class ConstantBuffer
    {
        // Fields
        private static System.Collections.Generic.List<UnityEngine.Rendering.ConstantBufferBase> m_RegisteredConstantBuffers;  // static @ 0x0

        // Methods
        // RVA: -1  // generic def  token: 0x60002AD
        public static System.Void PushGlobal(UnityEngine.Rendering.CommandBuffer cmd, CBType& data, System.Int32 shaderId) { }
        // RVA: -1  // generic def  token: 0x60002AE
        public static System.Void PushGlobal(CBType& data, System.Int32 shaderId) { }
        // RVA: -1  // generic def  token: 0x60002AF
        public static System.Void Push(UnityEngine.Rendering.CommandBuffer cmd, CBType& data, UnityEngine.ComputeShader cs, System.Int32 shaderId) { }
        // RVA: -1  // generic def  token: 0x60002B0
        public static System.Void Push(CBType& data, UnityEngine.ComputeShader cs, System.Int32 shaderId) { }
        // RVA: -1  // generic def  token: 0x60002B1
        public static System.Void Push(UnityEngine.Rendering.CommandBuffer cmd, CBType& data, UnityEngine.Material mat, System.Int32 shaderId) { }
        // RVA: -1  // generic def  token: 0x60002B2
        public static System.Void Push(CBType& data, UnityEngine.Material mat, System.Int32 shaderId) { }
        // RVA: -1  // generic def  token: 0x60002B3
        public static System.Void UpdateData(UnityEngine.Rendering.CommandBuffer cmd, CBType& data) { }
        // RVA: -1  // generic def  token: 0x60002B4
        public static System.Void UpdateData(CBType& data) { }
        // RVA: -1  // generic def  token: 0x60002B5
        public static System.Void SetGlobal(UnityEngine.Rendering.CommandBuffer cmd, System.Int32 shaderId) { }
        // RVA: -1  // generic def  token: 0x60002B6
        public static System.Void SetGlobal(System.Int32 shaderId) { }
        // RVA: -1  // generic def  token: 0x60002B7
        public static System.Void Set(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.ComputeShader cs, System.Int32 shaderId) { }
        // RVA: -1  // generic def  token: 0x60002B8
        public static System.Void Set(UnityEngine.ComputeShader cs, System.Int32 shaderId) { }
        // RVA: -1  // generic def  token: 0x60002B9
        public static System.Void Set(UnityEngine.Material mat, System.Int32 shaderId) { }
        // RVA: 0x04A41F00  token: 0x60002BA
        public static System.Void ReleaseAll() { }
        // RVA: 0x0B2B8884  token: 0x60002BB
        private static System.Void Register(UnityEngine.Rendering.ConstantBufferBase cb) { }
        // RVA: 0x041E1670  token: 0x60002BC
        public System.Void .ctor() { }
        // RVA: 0x04D187B0  token: 0x60002BD
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000077  // size: 0x10
    public abstract class ConstantBufferBase
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60002BE
        public virtual System.Void Release() { }
        // RVA: 0x041E1670  token: 0x60002BF
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000078
    public class ConstantBuffer`1 : UnityEngine.Rendering.ConstantBufferBase
    {
        // Fields
        private System.Collections.Generic.HashSet<System.Int32> m_GlobalBindings;  // 0x0
        private CBType[] m_Data;  // 0x0
        private UnityEngine.ComputeBuffer m_GPUConstantBuffer;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60002C0
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60002C1
        public System.Void UpdateData(UnityEngine.Rendering.CommandBuffer cmd, CBType& data) { }
        // RVA: -1  // not resolved  token: 0x60002C2
        public System.Void UpdateData(CBType& data) { }
        // RVA: -1  // not resolved  token: 0x60002C3
        public System.Void SetGlobal(UnityEngine.Rendering.CommandBuffer cmd, System.Int32 shaderId) { }
        // RVA: -1  // not resolved  token: 0x60002C4
        public System.Void SetGlobal(System.Int32 shaderId) { }
        // RVA: -1  // not resolved  token: 0x60002C5
        public System.Void Set(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.ComputeShader cs, System.Int32 shaderId) { }
        // RVA: -1  // not resolved  token: 0x60002C6
        public System.Void Set(UnityEngine.ComputeShader cs, System.Int32 shaderId) { }
        // RVA: -1  // not resolved  token: 0x60002C7
        public System.Void Set(UnityEngine.Material mat, System.Int32 shaderId) { }
        // RVA: -1  // not resolved  token: 0x60002C8
        public System.Void PushGlobal(UnityEngine.Rendering.CommandBuffer cmd, CBType& data, System.Int32 shaderId) { }
        // RVA: -1  // not resolved  token: 0x60002C9
        public System.Void PushGlobal(CBType& data, System.Int32 shaderId) { }
        // RVA: -1  // not resolved  token: 0x60002CA
        public virtual System.Void Release() { }

    }

    // TypeToken: 0x2000079
    public class ConstantBufferSingleton`1 : UnityEngine.Rendering.ConstantBuffer`1
    {
        // Fields
        private static UnityEngine.Rendering.ConstantBufferSingleton<CBType> s_Instance;  // static @ 0x0

        // Properties
        UnityEngine.Rendering.ConstantBufferSingleton<CBType> instance { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60002CD
        public virtual System.Void Release() { }
        // RVA: -1  // not resolved  token: 0x60002CE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007A  // size: 0x20
    public class DisplayInfoAttribute : System.Attribute
    {
        // Fields
        public System.String name;  // 0x10
        public System.Int32 order;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60002CF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007B  // size: 0x10
    public class AdditionalPropertyAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60002D0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007C  // size: 0x14
    public sealed struct CoreProfileId
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.CoreProfileId BlitTextureInPotAtlas;  // const

    }

    // TypeToken: 0x200007D  // size: 0x10
    public static class CoreUnsafeUtils
    {
        // Methods
        // RVA: -1  // generic def  token: 0x60002D1
        public static System.Void CopyTo(System.Collections.Generic.List<T> list, System.Void* dest, System.Int32 count) { }
        // RVA: -1  // generic def  token: 0x60002D2
        public static System.Void CopyTo(T[] list, System.Void* dest, System.Int32 count) { }
        // RVA: 0x0B2B8FF8  token: 0x60002D3
        private static System.Void CalculateRadixParams(System.Int32 radixBits, System.Int32& bitStates) { }
        // RVA: 0x04DB8CD0  token: 0x60002D4
        private static System.Int32 CalculateRadixSupportSize(System.Int32 bitStates, System.Int32 arrayLength) { }
        // RVA: 0x04DB8C90  token: 0x60002D5
        private static System.Void CalculateRadixSortSupportArrays(System.Int32 bitStates, System.Int32 arrayLength, System.UInt32* supportArray, System.UInt32*& bucketIndices, System.UInt32*& bucketSizes, System.UInt32*& bucketPrefix, System.UInt32*& arrayOutput) { }
        // RVA: 0x0B2B9398  token: 0x60002D6
        private static System.Void MergeSort(System.UInt32* array, System.UInt32* support, System.Int32 length) { }
        // RVA: 0x0B2B9544  token: 0x60002D7
        public static System.Void MergeSort(System.UInt32[] arr, System.Int32 sortSize, System.UInt32[]& supportArray) { }
        // RVA: 0x0B2B9638  token: 0x60002D8
        public static System.Void MergeSort(Unity.Collections.NativeArray<System.UInt32> arr, Unity.Collections.Allocator arrAllocator, System.Int32 sortSize, Unity.Collections.NativeArray<System.UInt32>& supportArray) { }
        // RVA: 0x0B2B92D4  token: 0x60002D9
        private static System.Void InsertionSort(System.UInt32* arr, System.Int32 length) { }
        // RVA: 0x0B2B9314  token: 0x60002DA
        public static System.Void InsertionSort(System.UInt32[] arr, System.Int32 sortSize) { }
        // RVA: 0x0B2B925C  token: 0x60002DB
        public static System.Void InsertionSort(Unity.Collections.NativeArray<System.UInt32> arr, System.Int32 sortSize) { }
        // RVA: 0x0B2B9780  token: 0x60002DC
        private static System.Void RadixSort(System.UInt32* array, System.UInt32* support, System.Int32 radixBits, System.Int32 bitStates, System.Int32 length) { }
        // RVA: 0x0B2B9A30  token: 0x60002DD
        public static System.Void RadixSort(System.UInt32[] arr, System.Int32 sortSize, System.UInt32[]& supportArray, System.Int32 radixBits) { }
        // RVA: 0x0B2B9944  token: 0x60002DE
        public static System.Void RadixSort(Unity.Collections.NativeArray<System.UInt32> array, System.Int32 sortSize, Unity.Collections.NativeArray<System.UInt32>& supportArray, Unity.Collections.Allocator supportArrayAllocator, System.Int32 radixBits) { }
        // RVA: 0x0B2B9708  token: 0x60002DF
        public static System.Void QuickSort(System.UInt32[] arr, System.Int32 left, System.Int32 right) { }
        // RVA: -1  // generic def  token: 0x60002E0
        public static System.Void QuickSort(System.Int32 count, System.Void* data) { }
        // RVA: -1  // generic def  token: 0x60002E1
        public static System.Void QuickSort(System.Int32 count, System.Void* data) { }
        // RVA: -1  // generic def  token: 0x60002E2
        public static System.Void QuickSort(System.Void* data, System.Int32 left, System.Int32 right) { }
        // RVA: -1  // generic def  token: 0x60002E3
        public static System.Int32 IndexOf(System.Void* data, System.Int32 count, T v) { }
        // RVA: -1  // generic def  token: 0x60002E4
        public static System.Int32 CompareHashes(System.Int32 oldHashCount, System.Void* oldHashes, System.Int32 newHashCount, System.Void* newHashes, System.Int32* addIndices, System.Int32* removeIndices, System.Int32& addCount, System.Int32& remCount) { }
        // RVA: 0x0B2B90CC  token: 0x60002E5
        public static System.Int32 CompareHashes(System.Int32 oldHashCount, UnityEngine.Hash128* oldHashes, System.Int32 newHashCount, UnityEngine.Hash128* newHashes, System.Int32* addIndices, System.Int32* removeIndices, System.Int32& addCount, System.Int32& remCount) { }
        // RVA: -1  // generic def  token: 0x60002E6
        public static System.Void CombineHashes(System.Int32 count, System.Void* hashes, UnityEngine.Hash128* outHash) { }
        // RVA: 0x0B2B9074  token: 0x60002E7
        public static System.Void CombineHashes(System.Int32 count, UnityEngine.Hash128* hashes, UnityEngine.Hash128* outHash) { }
        // RVA: -1  // generic def  token: 0x60002E8
        private static System.Int32 Partition(System.Void* data, System.Int32 left, System.Int32 right) { }
        // RVA: 0x0B2B913C  token: 0x60002E9
        public static System.Boolean HaveDuplicates(System.Int32[] arr) { }

    }

    // TypeToken: 0x2000082
    public class DynamicArray`1
    {
        // Fields
        private T[] m_Array;  // 0x0
        private System.Int32 <size>k__BackingField;  // 0x0

        // Properties
        System.Int32 size { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Int32 capacity { get; /* RVA: -1  // not resolved */ }
        T& Item { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60002F6
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60002F7
        public System.Void .ctor(System.Int32 size) { }
        // RVA: -1  // not resolved  token: 0x60002F8
        public System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x60002F9
        public System.Boolean Contains(T item) { }
        // RVA: -1  // not resolved  token: 0x60002FA
        public System.Int32 Add(T& value) { }
        // RVA: -1  // not resolved  token: 0x60002FB
        public System.Void AddRange(UnityEngine.Rendering.DynamicArray<T> array) { }
        // RVA: -1  // not resolved  token: 0x60002FC
        public System.Boolean Remove(T item) { }
        // RVA: -1  // not resolved  token: 0x60002FD
        public System.Void RemoveAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60002FE
        public System.Void RemoveRange(System.Int32 index, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x60002FF
        public System.Int32 FindIndex(System.Int32 startIndex, System.Int32 count, System.Predicate<T> match) { }
        // RVA: -1  // not resolved  token: 0x6000300
        public System.Int32 IndexOf(T item, System.Int32 index, System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x6000301
        public System.Int32 IndexOf(T item, System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000302
        public System.Int32 IndexOf(T item) { }
        // RVA: -1  // not resolved  token: 0x6000303
        public System.Void Resize(System.Int32 newSize, System.Boolean keepContent) { }
        // RVA: -1  // not resolved  token: 0x6000304
        public System.Void Reserve(System.Int32 newCapacity, System.Boolean keepContent) { }
        // RVA: -1  // not resolved  token: 0x6000306
        public static T[] op_Implicit(UnityEngine.Rendering.DynamicArray<T> array) { }

    }

    // TypeToken: 0x2000083  // size: 0x10
    public static class DynamicArrayExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000307
        private static System.Int32 Partition(T[] data, System.Int32 left, System.Int32 right) { }
        // RVA: -1  // generic def  token: 0x6000308
        private static System.Void QuickSort(T[] data, System.Int32 left, System.Int32 right) { }
        // RVA: -1  // generic def  token: 0x6000309
        public static System.Void QuickSort(UnityEngine.Rendering.DynamicArray<T> array) { }

    }

    // TypeToken: 0x2000084  // size: 0x80
    public sealed class PerformDynamicRes : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x046716C0  token: 0x600030A
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x07378308  token: 0x600030B
        public virtual System.Single Invoke() { }
        // RVA: 0x053DED94  token: 0x600030C
        public virtual System.IAsyncResult BeginInvoke(System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x05FD0828  token: 0x600030D
        public virtual System.Single EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000085  // size: 0x14
    public sealed struct DynamicResScalePolicyType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.DynamicResScalePolicyType ReturnsPercentage;  // const
        public static UnityEngine.Rendering.DynamicResScalePolicyType ReturnsMinMaxLerpFactor;  // const

    }

    // TypeToken: 0x2000086  // size: 0x14
    public sealed struct DynamicResScalerSlot
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.DynamicResScalerSlot User;  // const
        public static UnityEngine.Rendering.DynamicResScalerSlot System;  // const
        public static UnityEngine.Rendering.DynamicResScalerSlot Count;  // const

    }

    // TypeToken: 0x2000087  // size: 0x90
    public class DynamicResolutionHandler
    {
        // Fields
        private System.Boolean m_Enabled;  // 0x10
        private System.Boolean m_UseMipBias;  // 0x11
        private System.Single m_MinScreenFraction;  // 0x14
        private System.Single m_MaxScreenFraction;  // 0x18
        private System.Single m_CurrentFraction;  // 0x1c
        private System.Boolean m_ForcingRes;  // 0x20
        private System.Boolean m_CurrentCameraRequest;  // 0x21
        private System.Single m_PrevFraction;  // 0x24
        private System.Boolean m_ForceSoftwareFallback;  // 0x28
        private System.Boolean m_RunUpscalerFilterOnFullResolution;  // 0x29
        private System.Single m_PrevHWScaleWidth;  // 0x2c
        private System.Single m_PrevHWScaleHeight;  // 0x30
        private UnityEngine.Vector2Int m_LastScaledSize;  // 0x34
        private static UnityEngine.Rendering.DynamicResScalerSlot s_ActiveScalerSlot;  // static @ 0x0
        private static UnityEngine.Rendering.DynamicResolutionHandler.ScalerContainer[] s_ScalerContainers;  // static @ 0x8
        private UnityEngine.Vector2Int cachedOriginalSize;  // 0x3c
        private UnityEngine.Rendering.DynamicResUpscaleFilter <filter>k__BackingField;  // 0x44
        private static System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Rendering.DynamicResUpscaleFilter> s_CameraUpscaleFilters;  // static @ 0x10
        private UnityEngine.Vector2Int <finalViewport>k__BackingField;  // 0x48
        private UnityEngine.Rendering.DynamicResolutionType type;  // 0x50
        private UnityEngine.Rendering.GlobalDynamicResolutionSettings m_CachedSettings;  // 0x54
        private static System.Int32 CameraDictionaryMaxcCapacity;  // const
        private System.WeakReference m_OwnerCameraWeakRef;  // 0x80
        private static System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Rendering.DynamicResolutionHandler> s_CameraInstances;  // static @ 0x18
        private static UnityEngine.Rendering.DynamicResolutionHandler s_DefaultInstance;  // static @ 0x20
        private static System.Int32 s_ActiveCameraId;  // static @ 0x28
        private static UnityEngine.Rendering.DynamicResolutionHandler s_ActiveInstance;  // static @ 0x30
        private static System.Boolean s_ActiveInstanceDirty;  // static @ 0x38
        private static System.Single s_GlobalHwFraction;  // static @ 0x3c
        private static System.Boolean s_GlobalHwUpresActive;  // static @ 0x40
        private UnityEngine.Rendering.DynamicResolutionHandler.UpsamplerScheduleType m_UpsamplerSchedule;  // 0x88

        // Properties
        UnityEngine.Rendering.DynamicResUpscaleFilter filter { get; /* RVA: 0x04D86A30 */ set; /* RVA: 0x04D86A40 */ }
        UnityEngine.Vector2Int finalViewport { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x04D866F0 */ }
        System.Boolean runUpscalerFilterOnFullResolution { get; /* RVA: 0x0B2BFA4C */ set; /* RVA: 0x04D867C0 */ }
        UnityEngine.Rendering.DynamicResolutionHandler.UpsamplerScheduleType upsamplerSchedule { get; /* RVA: 0x04D86890 */ set; /* RVA: 0x04D868C0 */ }
        UnityEngine.Rendering.DynamicResolutionHandler instance { get; /* RVA: 0x0B2BFA18 */ }

        // Methods
        // RVA: 0x04671970  token: 0x600030E
        private System.Void Reset() { }
        // RVA: 0x0B2BEAB0  token: 0x6000315
        private System.Boolean FlushScalableBufferManagerState() { }
        // RVA: 0x0B2BEBD8  token: 0x6000316
        private static UnityEngine.Rendering.DynamicResolutionHandler GetOrCreateDrsInstanceHandler(UnityEngine.Camera camera) { }
        // RVA: 0x04671750  token: 0x600031A
        private System.Void .ctor() { }
        // RVA: 0x04D88DD0  token: 0x600031B
        private static System.Single DefaultDynamicResMethod() { }
        // RVA: 0x0B2BF104  token: 0x600031C
        private System.Void ProcessSettings(UnityEngine.Rendering.GlobalDynamicResolutionSettings settings) { }
        // RVA: 0x0B2BF0A4  token: 0x600031D
        public UnityEngine.Vector2 GetResolvedScale() { }
        // RVA: 0x03EC96B0  token: 0x600031E
        public System.Single CalculateMipBias(UnityEngine.Vector2Int inputResolution, UnityEngine.Vector2Int outputResolution, System.Boolean forceApply) { }
        // RVA: 0x0B2BF334  token: 0x600031F
        public static System.Void SetDynamicResScaler(UnityEngine.Rendering.PerformDynamicRes scaler, UnityEngine.Rendering.DynamicResScalePolicyType scalerType) { }
        // RVA: 0x0B2BF3C8  token: 0x6000320
        public static System.Void SetSystemDynamicResScaler(UnityEngine.Rendering.PerformDynamicRes scaler, UnityEngine.Rendering.DynamicResScalePolicyType scalerType) { }
        // RVA: 0x0B2BF2E0  token: 0x6000321
        public static System.Void SetActiveDynamicScalerSlot(UnityEngine.Rendering.DynamicResScalerSlot slot) { }
        // RVA: 0x03DF2EA0  token: 0x6000322
        public static System.Void ClearSelectedCamera() { }
        // RVA: 0x0B2BF45C  token: 0x6000323
        public static System.Void SetUpscaleFilter(UnityEngine.Camera camera, UnityEngine.Rendering.DynamicResUpscaleFilter filter) { }
        // RVA: 0x04D86860  token: 0x6000324
        public System.Void SetCurrentCameraRequest(System.Boolean cameraRequest) { }
        // RVA: 0x0B2BF5BC  token: 0x6000325
        public static System.Void UpdateAndUseCamera(UnityEngine.Camera camera, System.Nullable<UnityEngine.Rendering.GlobalDynamicResolutionSettings> settings, System.Action OnResolutionChange) { }
        // RVA: 0x0B2BF794  token: 0x6000326
        public System.Void Update(UnityEngine.Rendering.GlobalDynamicResolutionSettings settings, System.Action OnResolutionChange) { }
        // RVA: 0x0B2BF570  token: 0x6000327
        public System.Boolean SoftwareDynamicResIsEnabled() { }
        // RVA: 0x04D30F30  token: 0x6000328
        public System.Boolean HardwareDynamicResIsEnabled() { }
        // RVA: 0x0B2BF2CC  token: 0x6000329
        public System.Boolean RequestsHardwareDynamicResolution() { }
        // RVA: 0x0B2BEA7C  token: 0x600032A
        public System.Boolean DynamicResolutionEnabled() { }
        // RVA: 0x04D8E3B0  token: 0x600032B
        public System.Void ForceSoftwareFallback() { }
        // RVA: 0x03F65000  token: 0x600032C
        public UnityEngine.Vector2Int GetScaledSize(UnityEngine.Vector2Int size) { }
        // RVA: 0x0B2BE994  token: 0x600032D
        public UnityEngine.Vector2Int ApplyScalesOnSize(UnityEngine.Vector2Int size) { }
        // RVA: 0x0B2BE9C8  token: 0x600032E
        private UnityEngine.Vector2Int ApplyScalesOnSize(UnityEngine.Vector2Int size, UnityEngine.Vector2 scales) { }
        // RVA: 0x0B2BEBBC  token: 0x600032F
        public System.Single GetCurrentScale() { }
        // RVA: 0x04DB8CF0  token: 0x6000330
        public UnityEngine.Vector2Int GetLastScaledSize() { }
        // RVA: 0x03F64FD0  token: 0x6000331
        public System.Single GetLowResMultiplier(System.Single targetLowRes) { }
        // RVA: 0x046713A0  token: 0x6000332
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200008A  // size: 0x11
    public sealed struct DynamicResolutionType
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static UnityEngine.Rendering.DynamicResolutionType Software;  // const
        public static UnityEngine.Rendering.DynamicResolutionType Hardware;  // const

    }

    // TypeToken: 0x200008B  // size: 0x11
    public sealed struct DynamicResUpscaleFilter
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static UnityEngine.Rendering.DynamicResUpscaleFilter Bilinear;  // const
        public static UnityEngine.Rendering.DynamicResUpscaleFilter CatmullRom;  // const
        public static UnityEngine.Rendering.DynamicResUpscaleFilter Lanczos;  // const
        public static UnityEngine.Rendering.DynamicResUpscaleFilter ContrastAdaptiveSharpen;  // const
        public static UnityEngine.Rendering.DynamicResUpscaleFilter EdgeAdaptiveScalingUpres;  // const
        public static UnityEngine.Rendering.DynamicResUpscaleFilter TAAU;  // const

    }

    // TypeToken: 0x200008C  // size: 0x38
    public sealed struct GlobalDynamicResolutionSettings
    {
        // Fields
        public System.Boolean enabled;  // 0x10
        public System.Boolean useMipBias;  // 0x11
        public System.Boolean enableDLSS;  // 0x12
        public System.UInt32 DLSSPerfQualitySetting;  // 0x14
        public System.Boolean DLSSUseOptimalSettings;  // 0x18
        public System.Single DLSSSharpness;  // 0x1c
        public System.Single maxPercentage;  // 0x20
        public System.Single minPercentage;  // 0x24
        public UnityEngine.Rendering.DynamicResolutionType dynResType;  // 0x28
        public UnityEngine.Rendering.DynamicResUpscaleFilter upsampleFilter;  // 0x29
        public System.Boolean forceResolution;  // 0x2a
        public System.Single forcedPercentage;  // 0x2c
        public System.Single lowResTransparencyMinimumThreshold;  // 0x30
        public System.Single rayTracingHalfResThreshold;  // 0x34

        // Methods
        // RVA: 0x04671920  token: 0x6000333
        public static UnityEngine.Rendering.GlobalDynamicResolutionSettings NewDefault() { }

    }

    // TypeToken: 0x200008D
    public interface IAdditionalData
    {
    }

    // TypeToken: 0x200008E
    public interface IVirtualTexturingEnabledRenderPipeline
    {
        // Properties
        System.Boolean virtualTexturingEnabled { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x200008F
    public sealed struct ListBuffer`1
    {
        // Fields
        private T* m_BufferPtr;  // 0x0
        private System.Int32 m_Capacity;  // 0x0
        private System.Int32* m_CountPtr;  // 0x0

        // Properties
        T* BufferPtr { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Int32 Capacity { get; /* RVA: -1  // not resolved */ }
        T& Item { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000338
        public System.Void .ctor(T* bufferPtr, System.Int32* countPtr, System.Int32 capacity) { }
        // RVA: -1  // not resolved  token: 0x600033A
        public T& GetUnchecked(System.Int32& index) { }
        // RVA: -1  // not resolved  token: 0x600033B
        public System.Boolean TryAdd(T& value) { }
        // RVA: -1  // not resolved  token: 0x600033C
        public System.Void CopyTo(T* dstBuffer, System.Int32 startDstIndex, System.Int32 copyCount) { }
        // RVA: -1  // not resolved  token: 0x600033D
        public System.Boolean TryCopyTo(UnityEngine.Rendering.ListBuffer<T> other) { }
        // RVA: -1  // not resolved  token: 0x600033E
        public System.Boolean TryCopyFrom(T* srcPtr, System.Int32 count) { }

    }

    // TypeToken: 0x2000090  // size: 0x10
    public static class ListBufferExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x600033F
        public static System.Void QuickSort(UnityEngine.Rendering.ListBuffer<T> self) { }

    }

    // TypeToken: 0x2000091
    public class ObjectPool`1
    {
        // Fields
        private readonly System.Collections.Generic.Stack<T> m_Stack;  // 0x0
        private readonly UnityEngine.Events.UnityAction<T> m_ActionOnGet;  // 0x0
        private readonly UnityEngine.Events.UnityAction<T> m_ActionOnRelease;  // 0x0
        private readonly System.Boolean m_CollectionCheck;  // 0x0
        private System.Int32 <countAll>k__BackingField;  // 0x0

        // Properties
        System.Int32 countAll { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Int32 countActive { get; /* RVA: -1  // not resolved */ }
        System.Int32 countInactive { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000344
        public System.Void .ctor(UnityEngine.Events.UnityAction<T> actionOnGet, UnityEngine.Events.UnityAction<T> actionOnRelease, System.Boolean collectionCheck) { }
        // RVA: -1  // not resolved  token: 0x6000345
        public T Get() { }
        // RVA: -1  // not resolved  token: 0x6000346
        public UnityEngine.Rendering.ObjectPool.PooledObject<T> Get(T& v) { }
        // RVA: -1  // not resolved  token: 0x6000347
        public System.Void Release(T element) { }

    }

    // TypeToken: 0x2000093
    public static class GenericPool`1
    {
        // Fields
        private static readonly UnityEngine.Rendering.ObjectPool<T> s_Pool;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600034A
        public static T Get() { }
        // RVA: -1  // not resolved  token: 0x600034B
        public static UnityEngine.Rendering.ObjectPool.PooledObject<T> Get(T& value) { }
        // RVA: -1  // not resolved  token: 0x600034C
        public static System.Void Release(T toRelease) { }
        // RVA: -1  // not resolved  token: 0x600034D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000094
    public static class UnsafeGenericPool`1
    {
        // Fields
        private static readonly UnityEngine.Rendering.ObjectPool<T> s_Pool;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600034E
        public static T Get() { }
        // RVA: -1  // not resolved  token: 0x600034F
        public static UnityEngine.Rendering.ObjectPool.PooledObject<T> Get(T& value) { }
        // RVA: -1  // not resolved  token: 0x6000350
        public static System.Void Release(T toRelease) { }
        // RVA: -1  // not resolved  token: 0x6000351
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000095
    public static class ListPool`1
    {
        // Fields
        private static readonly UnityEngine.Rendering.ObjectPool<System.Collections.Generic.List<T>> s_Pool;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000352
        public static System.Collections.Generic.List<T> Get() { }
        // RVA: -1  // not resolved  token: 0x6000353
        public static UnityEngine.Rendering.ObjectPool.PooledObject<System.Collections.Generic.List<T>> Get(System.Collections.Generic.List<T>& value) { }
        // RVA: -1  // not resolved  token: 0x6000354
        public static System.Void Release(System.Collections.Generic.List<T> toRelease) { }
        // RVA: -1  // not resolved  token: 0x6000355
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000097
    public static class HashSetPool`1
    {
        // Fields
        private static readonly UnityEngine.Rendering.ObjectPool<System.Collections.Generic.HashSet<T>> s_Pool;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000359
        public static System.Collections.Generic.HashSet<T> Get() { }
        // RVA: -1  // not resolved  token: 0x600035A
        public static UnityEngine.Rendering.ObjectPool.PooledObject<System.Collections.Generic.HashSet<T>> Get(System.Collections.Generic.HashSet<T>& value) { }
        // RVA: -1  // not resolved  token: 0x600035B
        public static System.Void Release(System.Collections.Generic.HashSet<T> toRelease) { }
        // RVA: -1  // not resolved  token: 0x600035C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000099
    public static class DictionaryPool`2
    {
        // Fields
        private static readonly UnityEngine.Rendering.ObjectPool<System.Collections.Generic.Dictionary<TKey,TValue>> s_Pool;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000360
        public static System.Collections.Generic.Dictionary<TKey,TValue> Get() { }
        // RVA: -1  // not resolved  token: 0x6000361
        public static UnityEngine.Rendering.ObjectPool.PooledObject<System.Collections.Generic.Dictionary<TKey,TValue>> Get(System.Collections.Generic.Dictionary<TKey,TValue>& value) { }
        // RVA: -1  // not resolved  token: 0x6000362
        public static System.Void Release(System.Collections.Generic.Dictionary<TKey,TValue> toRelease) { }
        // RVA: -1  // not resolved  token: 0x6000363
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200009B
    public sealed class ListChangedEventArgs`1 : System.EventArgs
    {
        // Fields
        public readonly System.Int32 index;  // 0x0
        public readonly T item;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000367
        public System.Void .ctor(System.Int32 index, T item) { }

    }

    // TypeToken: 0x200009C
    public sealed class ListChangedEventHandler`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000368
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000369
        public virtual System.Void Invoke(UnityEngine.Rendering.ObservableList<T> sender, UnityEngine.Rendering.ListChangedEventArgs<T> e) { }
        // RVA: -1  // runtime  token: 0x600036A
        public virtual System.IAsyncResult BeginInvoke(UnityEngine.Rendering.ObservableList<T> sender, UnityEngine.Rendering.ListChangedEventArgs<T> e, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x600036B
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200009D
    public class ObservableList`1 : System.Collections.Generic.IList`1, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private System.Collections.Generic.IList<T> m_List;  // 0x0
        private UnityEngine.Rendering.ListChangedEventHandler<T> ItemAdded;  // 0x0
        private UnityEngine.Rendering.ListChangedEventHandler<T> ItemRemoved;  // 0x0

        // Properties
        T Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsReadOnly { get; /* RVA: -1  // not resolved */ }

        // Events
        event UnityEngine.Rendering.ListChangedEventHandler<T> ItemAdded;
        event UnityEngine.Rendering.ListChangedEventHandler<T> ItemRemoved;

        // Methods
        // RVA: -1  // not resolved  token: 0x6000374
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000375
        public System.Void .ctor(System.Int32 capacity) { }
        // RVA: -1  // not resolved  token: 0x6000376
        public System.Void .ctor(System.Collections.Generic.IEnumerable<T> collection) { }
        // RVA: -1  // not resolved  token: 0x6000377
        private System.Void OnEvent(UnityEngine.Rendering.ListChangedEventHandler<T> e, System.Int32 index, T item) { }
        // RVA: -1  // not resolved  token: 0x6000378
        public virtual System.Boolean Contains(T item) { }
        // RVA: -1  // not resolved  token: 0x6000379
        public virtual System.Int32 IndexOf(T item) { }
        // RVA: -1  // not resolved  token: 0x600037A
        public virtual System.Void Add(T item) { }
        // RVA: -1  // not resolved  token: 0x600037B
        public System.Void Add(T[] items) { }
        // RVA: -1  // not resolved  token: 0x600037C
        public virtual System.Void Insert(System.Int32 index, T item) { }
        // RVA: -1  // not resolved  token: 0x600037D
        public virtual System.Boolean Remove(T item) { }
        // RVA: -1  // not resolved  token: 0x600037E
        public System.Int32 Remove(T[] items) { }
        // RVA: -1  // not resolved  token: 0x600037F
        public virtual System.Void RemoveAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000380
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000381
        public virtual System.Void CopyTo(T[] array, System.Int32 arrayIndex) { }
        // RVA: -1  // not resolved  token: 0x6000382
        public virtual System.Collections.Generic.IEnumerator<T> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000383
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200009E  // size: 0x20
    public class SerializableEnum
    {
        // Fields
        private System.String m_EnumValueAsString;  // 0x10
        private System.Type m_EnumType;  // 0x18

        // Properties
        System.Enum value { get; /* RVA: 0x0B2C6484 */ set; /* RVA: 0x0B2C6530 */ }

        // Methods
        // RVA: 0x0B2C6400  token: 0x6000386
        public System.Void .ctor(System.Type enumType) { }

    }

    // TypeToken: 0x200009F
    public class SerializedDictionary`2 : UnityEngine.Rendering.SerializedDictionary`4
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000387
        public virtual K SerializeKey(K key) { }
        // RVA: -1  // not resolved  token: 0x6000388
        public virtual V SerializeValue(V val) { }
        // RVA: -1  // not resolved  token: 0x6000389
        public virtual K DeserializeKey(K key) { }
        // RVA: -1  // not resolved  token: 0x600038A
        public virtual V DeserializeValue(V val) { }
        // RVA: -1  // not resolved  token: 0x600038B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A0
    public abstract class SerializedDictionary`4 : System.Collections.Generic.Dictionary`2, UnityEngine.ISerializationCallbackReceiver
    {
        // Fields
        private System.Collections.Generic.List<SK> m_Keys;  // 0x0
        private System.Collections.Generic.List<SV> m_Values;  // 0x0

        // Methods
        // RVA: -1  // abstract  token: 0x600038C
        public virtual SK SerializeKey(K key) { }
        // RVA: -1  // abstract  token: 0x600038D
        public virtual SV SerializeValue(V value) { }
        // RVA: -1  // abstract  token: 0x600038E
        public virtual K DeserializeKey(SK serializedKey) { }
        // RVA: -1  // abstract  token: 0x600038F
        public virtual V DeserializeValue(SV serializedValue) { }
        // RVA: -1  // not resolved  token: 0x6000390
        public virtual System.Void OnBeforeSerialize() { }
        // RVA: -1  // not resolved  token: 0x6000391
        public virtual System.Void OnAfterDeserialize() { }
        // RVA: -1  // not resolved  token: 0x6000392
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000A1  // size: 0x10
    public class XRGraphics
    {
        // Properties
        System.Single eyeTextureResolutionScale { get; /* RVA: 0x0B2C8140 */ set; /* RVA: 0x0B2C8298 */ }
        System.Single renderViewportScale { get; /* RVA: 0x0B2C81F8 */ }
        System.Boolean enabled { get; /* RVA: 0x0B2C80A8 */ }
        System.Boolean isDeviceActive { get; /* RVA: 0x0B2C8190 */ }
        System.String loadedDeviceName { get; /* RVA: 0x0B2C81B4 */ }
        System.String[] supportedDevices { get; /* RVA: 0x0B2C824C */ }
        UnityEngine.Rendering.XRGraphics.StereoRenderingMode stereoRenderingMode { get; /* RVA: 0x0B2C8224 */ }
        UnityEngine.RenderTextureDescriptor eyeTextureDesc { get; /* RVA: 0x0B2C80B0 */ }
        System.Int32 eyeTextureWidth { get; /* RVA: 0x0B2C816C */ }
        System.Int32 eyeTextureHeight { get; /* RVA: 0x0B2C811C */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600039E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A3  // size: 0x14
    public sealed struct DebugAction
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.DebugAction EnableDebugMenu;  // const
        public static UnityEngine.Rendering.DebugAction PreviousDebugPanel;  // const
        public static UnityEngine.Rendering.DebugAction NextDebugPanel;  // const
        public static UnityEngine.Rendering.DebugAction Action;  // const
        public static UnityEngine.Rendering.DebugAction MakePersistent;  // const
        public static UnityEngine.Rendering.DebugAction MoveVertical;  // const
        public static UnityEngine.Rendering.DebugAction MoveHorizontal;  // const
        public static UnityEngine.Rendering.DebugAction Multiplier;  // const
        public static UnityEngine.Rendering.DebugAction ResetAll;  // const
        public static UnityEngine.Rendering.DebugAction DebugActionCount;  // const

    }

    // TypeToken: 0x20000A4  // size: 0x14
    public sealed struct DebugActionRepeatMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.DebugActionRepeatMode Never;  // const
        public static UnityEngine.Rendering.DebugActionRepeatMode Delay;  // const

    }

    // TypeToken: 0x20000A5  // size: 0x88
    public sealed class DebugManager
    {
        // Fields
        private static System.String kEnableDebugBtn1;  // const
        private static System.String kEnableDebugBtn2;  // const
        private static System.String kDebugPreviousBtn;  // const
        private static System.String kDebugNextBtn;  // const
        private static System.String kValidateBtn;  // const
        private static System.String kPersistentBtn;  // const
        private static System.String kDPadVertical;  // const
        private static System.String kDPadHorizontal;  // const
        private static System.String kMultiplierBtn;  // const
        private static System.String kResetBtn;  // const
        private static System.String kEnableDebug;  // const
        private UnityEngine.Rendering.DebugActionDesc[] m_DebugActions;  // 0x10
        private UnityEngine.Rendering.DebugActionState[] m_DebugActionStates;  // 0x18
        private UnityEngine.InputSystem.InputActionMap debugActionMap;  // 0x20
        private static readonly System.Lazy<UnityEngine.Rendering.DebugManager> s_Instance;  // static @ 0x0
        private System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.Rendering.DebugUI.Panel> m_ReadOnlyPanels;  // 0x28
        private readonly System.Collections.Generic.List<UnityEngine.Rendering.DebugUI.Panel> m_Panels;  // 0x30
        private System.Action<System.Boolean> onDisplayRuntimeUIChanged;  // 0x38
        private System.Action onSetDirty;  // 0x40
        private System.Action resetData;  // 0x48
        public System.Boolean refreshEditorRequested;  // 0x50
        private System.Nullable<System.Int32> m_RequestedPanelIndex;  // 0x54
        private UnityEngine.GameObject m_Root;  // 0x60
        private UnityEngine.Rendering.UI.DebugUIHandlerCanvas m_RootUICanvas;  // 0x68
        private UnityEngine.GameObject m_PersistentRoot;  // 0x70
        private UnityEngine.Rendering.UI.DebugUIHandlerPersistentCanvas m_RootUIPersistentCanvas;  // 0x78
        private System.Boolean m_EditorOpen;  // 0x80
        private System.Boolean m_EnableRuntimeUI;  // 0x81

        // Properties
        UnityEngine.Rendering.DebugManager instance { get; /* RVA: 0x03948C00 */ }
        System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.Rendering.DebugUI.Panel> panels { get; /* RVA: 0x0B2BC27C */ }
        System.Boolean displayEditorUI { get; /* RVA: 0x04D8D1F0 */ }
        System.Boolean enableRuntimeUI { get; /* RVA: 0x04D8FEE0 */ set; /* RVA: 0x03948CD0 */ }
        System.Boolean displayRuntimeUI { get; /* RVA: 0x03948C50 */ set; /* RVA: 0x04A438B0 */ }
        System.Boolean displayPersistentRuntimeUI { get; /* RVA: 0x0B2BC20C */ set; /* RVA: 0x04D230A0 */ }

        // Events
        event System.Action<System.Boolean> onDisplayRuntimeUIChanged;
        event System.Action onSetDirty;
        event System.Action resetData;

        // Methods
        // RVA: 0x0B2BABE0  token: 0x600039F
        private System.Void RegisterActions() { }
        // RVA: 0x0B2B9EE8  token: 0x60003A0
        private System.Void EnableInputActions() { }
        // RVA: 0x0B2B9DF8  token: 0x60003A1
        private System.Void AddAction(UnityEngine.Rendering.DebugAction action, UnityEngine.Rendering.DebugActionDesc desc) { }
        // RVA: 0x0B2BBCD0  token: 0x60003A2
        private System.Void SampleAction(System.Int32 actionIndex) { }
        // RVA: 0x0B2BBF40  token: 0x60003A3
        private System.Void UpdateAction(System.Int32 actionIndex) { }
        // RVA: 0x0B2BBF98  token: 0x60003A4
        private System.Void UpdateActions() { }
        // RVA: 0x0B2BA454  token: 0x60003A5
        private System.Single GetAction(UnityEngine.Rendering.DebugAction action) { }
        // RVA: 0x0B2BA290  token: 0x60003A6
        private System.Boolean GetActionToggleDebugMenuWithTouch() { }
        // RVA: 0x0B2BA1C4  token: 0x60003A7
        private System.Boolean GetActionReleaseScrollTarget() { }
        // RVA: 0x0B2BB050  token: 0x60003A8
        private System.Void RegisterInputs() { }
        // RVA: 0x0B2BBFE4  token: 0x60003AA
        private System.Void UpdateReadOnlyCollection() { }
        // RVA: 0x04D8D200  token: 0x60003B3
        public System.Void ToggleEditorUI(System.Boolean open) { }
        // RVA: 0x0484F2C0  token: 0x60003BA
        private System.Void .ctor() { }
        // RVA: 0x04DA9DE0  token: 0x60003BB
        public System.Void RefreshEditor() { }
        // RVA: 0x0B2BBCA8  token: 0x60003BC
        public System.Void Reset() { }
        // RVA: 0x0B2BABB4  token: 0x60003BD
        public System.Void ReDrawOnScreenDebug() { }
        // RVA: 0x0B2BB020  token: 0x60003BE
        public System.Void RegisterData(UnityEngine.Rendering.IDebugData data) { }
        // RVA: 0x0B2BBF10  token: 0x60003BF
        public System.Void UnregisterData(UnityEngine.Rendering.IDebugData data) { }
        // RVA: 0x0B2BAA60  token: 0x60003C0
        public System.Int32 GetState() { }
        // RVA: 0x0B2BB9D4  token: 0x60003C1
        private System.Void RegisterRootCanvas(UnityEngine.Rendering.UI.DebugUIHandlerCanvas root) { }
        // RVA: 0x0B2B9EC8  token: 0x60003C2
        private System.Void ChangeSelection(UnityEngine.Rendering.UI.DebugUIHandlerWidget widget, System.Boolean fromNext) { }
        // RVA: 0x0B2BBDE8  token: 0x60003C3
        private System.Void SetScrollTarget(UnityEngine.Rendering.UI.DebugUIHandlerWidget widget) { }
        // RVA: 0x0B2B9FF4  token: 0x60003C4
        private System.Void EnsurePersistentCanvas() { }
        // RVA: 0x0B2BBE64  token: 0x60003C5
        private System.Void TogglePersistent(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x0B2BAB98  token: 0x60003C6
        private System.Void OnPanelDirty(UnityEngine.Rendering.DebugUI.Panel panel) { }
        // RVA: 0x0B2BBC54  token: 0x60003C7
        public System.Void RequestEditorWindowPanelIndex(System.Int32 index) { }
        // RVA: 0x04DB8CE0  token: 0x60003C8
        private System.Nullable<System.Int32> GetRequestedEditorWindowPanelIndex() { }
        // RVA: 0x0B2BA770  token: 0x60003C9
        public UnityEngine.Rendering.DebugUI.Panel GetPanel(System.String displayName, System.Boolean createIfNull, System.Int32 groupIndex, System.Boolean overrideIfExist) { }
        // RVA: 0x0B2BBA20  token: 0x60003CA
        public System.Void RemovePanel(System.String displayName) { }
        // RVA: 0x0B2BBBF0  token: 0x60003CB
        public System.Void RemovePanel(UnityEngine.Rendering.DebugUI.Panel panel) { }
        // RVA: 0x0B2BA644  token: 0x60003CC
        public UnityEngine.Rendering.DebugUI.Widget GetItem(System.String queryPath) { }
        // RVA: 0x0B2BA48C  token: 0x60003CD
        private UnityEngine.Rendering.DebugUI.Widget GetItem(System.String queryPath, UnityEngine.Rendering.DebugUI.IContainer container) { }
        // RVA: 0x04B7D6F0  token: 0x60003CE
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000A7  // size: 0x20
    public class DebugActionDesc
    {
        // Fields
        public UnityEngine.InputSystem.InputAction buttonAction;  // 0x10
        public UnityEngine.Rendering.DebugActionRepeatMode repeatMode;  // 0x18
        public System.Single repeatDelay;  // 0x1c

        // Methods
        // RVA: 0x041E1670  token: 0x60003D4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A8  // size: 0x38
    public class DebugActionState
    {
        // Fields
        private UnityEngine.Rendering.DebugActionState.DebugActionKeyType m_Type;  // 0x10
        private UnityEngine.InputSystem.InputAction inputAction;  // 0x18
        private System.Boolean[] m_TriggerPressedUp;  // 0x20
        private System.Single m_Timer;  // 0x28
        private System.Boolean <runningAction>k__BackingField;  // 0x2c
        private System.Single <actionState>k__BackingField;  // 0x30

        // Properties
        System.Boolean runningAction { get; /* RVA: 0x04D869F0 */ set; /* RVA: 0x04D86A00 */ }
        System.Single actionState { get; /* RVA: 0x04D86E20 */ set; /* RVA: 0x04D86E30 */ }

        // Methods
        // RVA: 0x0B2B9BDC  token: 0x60003D9
        private System.Void Trigger(System.Int32 triggerCount, System.Single state) { }
        // RVA: 0x0B2B9B58  token: 0x60003DA
        public System.Void TriggerWithButton(UnityEngine.InputSystem.InputAction action, System.Single state) { }
        // RVA: 0x0B2B9B40  token: 0x60003DB
        private System.Void Reset() { }
        // RVA: 0x0B2B9C78  token: 0x60003DC
        public System.Void Update(UnityEngine.Rendering.DebugActionDesc desc) { }
        // RVA: 0x041E1670  token: 0x60003DD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000AA
    public interface IDebugData
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60003DE
        public virtual System.Action GetReset() { }

    }

    // TypeToken: 0x20000AB  // size: 0x30
    public class DebugShapes
    {
        // Fields
        private static UnityEngine.Rendering.DebugShapes s_Instance;  // static @ 0x0
        private UnityEngine.Mesh m_sphereMesh;  // 0x10
        private UnityEngine.Mesh m_boxMesh;  // 0x18
        private UnityEngine.Mesh m_coneMesh;  // 0x20
        private UnityEngine.Mesh m_pyramidMesh;  // 0x28

        // Properties
        UnityEngine.Rendering.DebugShapes instance { get; /* RVA: 0x0B2BE910 */ }

        // Methods
        // RVA: 0x0B2BE17C  token: 0x60003E0
        private System.Void BuildSphere(UnityEngine.Mesh& outputMesh, System.Single radius, System.UInt32 longSubdiv, System.UInt32 latSubdiv) { }
        // RVA: 0x0B2BC458  token: 0x60003E1
        private System.Void BuildBox(UnityEngine.Mesh& outputMesh, System.Single length, System.Single width, System.Single height) { }
        // RVA: 0x0B2BD098  token: 0x60003E2
        private System.Void BuildCone(UnityEngine.Mesh& outputMesh, System.Single height, System.Single topRadius, System.Single bottomRadius, System.Int32 nbSides) { }
        // RVA: 0x0B2BDAE0  token: 0x60003E3
        private System.Void BuildPyramid(UnityEngine.Mesh& outputMesh, System.Single width, System.Single height, System.Single depth) { }
        // RVA: 0x0B2BDFF0  token: 0x60003E4
        private System.Void BuildShapes() { }
        // RVA: 0x0B2BE7DC  token: 0x60003E5
        private System.Void RebuildResources() { }
        // RVA: 0x0B2BE8F4  token: 0x60003E6
        public UnityEngine.Mesh RequestSphereMesh() { }
        // RVA: 0x0B2BE8A0  token: 0x60003E7
        public UnityEngine.Mesh RequestBoxMesh() { }
        // RVA: 0x0B2BE8BC  token: 0x60003E8
        public UnityEngine.Mesh RequestConeMesh() { }
        // RVA: 0x0B2BE8D8  token: 0x60003E9
        public UnityEngine.Mesh RequestPyramidMesh() { }
        // RVA: 0x041E1670  token: 0x60003EA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000AC  // size: 0x10
    public class DebugUI
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60003EB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CF  // size: 0x20
    public class DebugUpdater : UnityEngine.MonoBehaviour
    {
        // Fields
        private static UnityEngine.Rendering.DebugUpdater s_Instance;  // static @ 0x0
        private UnityEngine.ScreenOrientation m_Orientation;  // 0x18
        private System.Boolean m_RuntimeUiWasVisibleLastFrame;  // 0x1c

        // Methods
        // RVA: 0x041E1670  token: 0x6000480
        private static System.Void RuntimeInit() { }
        // RVA: 0x03949B80  token: 0x6000481
        private static System.Void SetEnabled(System.Boolean enabled) { }
        // RVA: 0x0B2CAAB0  token: 0x6000482
        private static System.Void EnableRuntime() { }
        // RVA: 0x039499F0  token: 0x6000483
        private static System.Void DisableRuntime() { }
        // RVA: 0x04A439B0  token: 0x6000484
        private static System.Void HandleInternalEventSystemComponents(System.Boolean uiEnabled) { }
        // RVA: 0x0B2CAC1C  token: 0x6000485
        private System.Void EnsureExactlyOneEventSystem() { }
        // RVA: 0x0B2CAA54  token: 0x6000486
        private System.Collections.IEnumerator DoAfterInputModuleUpdated(System.Action action) { }
        // RVA: 0x0B2CA76C  token: 0x6000487
        private System.Void CheckInputModuleExists() { }
        // RVA: 0x0B2CA64C  token: 0x6000488
        private System.Void AssignDefaultActions() { }
        // RVA: 0x0B2CA844  token: 0x6000489
        private System.Void CreateDebugEventSystem() { }
        // RVA: 0x0B2CA924  token: 0x600048A
        private System.Void DestroyDebugEventSystem() { }
        // RVA: 0x0B2CADEC  token: 0x600048B
        private System.Void Update() { }
        // RVA: 0x0B2CADA8  token: 0x600048C
        private static System.Collections.IEnumerator RefreshRuntimeUINextFrame() { }
        // RVA: 0x05393520  token: 0x600048D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D2  // size: 0x10
    public class MousePositionDebug
    {
        // Fields
        private static UnityEngine.Rendering.MousePositionDebug s_Instance;  // static @ 0x0

        // Properties
        UnityEngine.Rendering.MousePositionDebug instance { get; /* RVA: 0x03945840 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600049B
        public System.Void Build() { }
        // RVA: 0x041E1670  token: 0x600049C
        public System.Void Cleanup() { }
        // RVA: 0x0B2D1430  token: 0x600049D
        public UnityEngine.Vector2 GetMousePosition(System.Single ScreenHeight, System.Boolean sceneView) { }
        // RVA: 0x0B2D13C0  token: 0x600049E
        private UnityEngine.Vector2 GetInputMousePosition() { }
        // RVA: 0x04DBB8D0  token: 0x600049F
        public UnityEngine.Vector2 GetMouseClickPosition(System.Single ScreenHeight) { }
        // RVA: 0x041E1670  token: 0x60004A0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D3
    public class TProfilingSampler`1 : UnityEngine.Rendering.ProfilingSampler
    {
        // Fields
        private static System.Collections.Generic.Dictionary<TEnum,UnityEngine.Rendering.TProfilingSampler<TEnum>> samples;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60004A1
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x60004A2
        public System.Void .ctor(System.String name) { }

    }

    // TypeToken: 0x20000D4  // size: 0x38
    public class ProfilingSampler
    {
        // Fields
        private UnityEngine.Profiling.CustomSampler <sampler>k__BackingField;  // 0x10
        private UnityEngine.Profiling.CustomSampler <inlineSampler>k__BackingField;  // 0x18
        private System.String <name>k__BackingField;  // 0x20
        private UnityEngine.Profiling.Recorder m_Recorder;  // 0x28
        private UnityEngine.Profiling.Recorder m_InlineRecorder;  // 0x30

        // Properties
        UnityEngine.Profiling.CustomSampler sampler { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        UnityEngine.Profiling.CustomSampler inlineSampler { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.String name { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.Boolean enableRecording { set; /* RVA: 0x0B2D355C */ }
        System.Single gpuElapsedTime { get; /* RVA: 0x0B2D343C */ }
        System.Int32 gpuSampleCount { get; /* RVA: 0x0B2D348C */ }
        System.Single cpuElapsedTime { get; /* RVA: 0x0B2D33AC */ }
        System.Int32 cpuSampleCount { get; /* RVA: 0x0B2D33FC */ }
        System.Single inlineCpuElapsedTime { get; /* RVA: 0x0B2D34CC */ }
        System.Int32 inlineCpuSampleCount { get; /* RVA: 0x0B2D351C */ }

        // Methods
        // RVA: -1  // generic def  token: 0x60004A3
        public static UnityEngine.Rendering.ProfilingSampler Get(TEnum marker) { }
        // RVA: 0x03B945E0  token: 0x60004A4
        public System.Void .ctor(System.String name) { }
        // RVA: 0x0B2D3324  token: 0x60004A5
        public System.Void Begin(UnityEngine.Rendering.CommandBuffer cmd) { }
        // RVA: 0x0B2D3368  token: 0x60004A6
        public System.Void End(UnityEngine.Rendering.CommandBuffer cmd) { }
        // RVA: 0x0AB8BD08  token: 0x60004A7
        private System.Boolean IsValid() { }
        // RVA: 0x041E1670  token: 0x60004B5
        private System.Void .ctor() { }

    }

    // TypeToken: 0x20000D5  // size: 0x11
    public sealed struct ProfilingScope : System.IDisposable
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60004B6
        public System.Void .ctor(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.ProfilingSampler sampler) { }
        // RVA: 0x041E1670  token: 0x60004B7
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x20000D6  // size: 0x30
    public sealed struct ProfilingSample : System.IDisposable
    {
        // Fields
        private readonly UnityEngine.Rendering.CommandBuffer m_Cmd;  // 0x10
        private readonly System.String m_Name;  // 0x18
        private System.Boolean m_Disposed;  // 0x20
        private UnityEngine.Profiling.CustomSampler m_Sampler;  // 0x28

        // Methods
        // RVA: 0x0B2D31F0  token: 0x60004B8
        public System.Void .ctor(UnityEngine.Rendering.CommandBuffer cmd, System.String name, UnityEngine.Profiling.CustomSampler sampler) { }
        // RVA: 0x0B2D3294  token: 0x60004B9
        public System.Void .ctor(UnityEngine.Rendering.CommandBuffer cmd, System.String format, System.Object arg) { }
        // RVA: 0x0B2D32DC  token: 0x60004BA
        public System.Void .ctor(UnityEngine.Rendering.CommandBuffer cmd, System.String format, System.Object[] args) { }
        // RVA: 0x0B2D31E4  token: 0x60004BB
        public virtual System.Void Dispose() { }
        // RVA: 0x0B2D3164  token: 0x60004BC
        private System.Void Dispose(System.Boolean disposing) { }

    }

    // TypeToken: 0x20000D7  // size: 0x28
    public class CoreRPHelpURLAttribute : UnityEngine.HelpURLAttribute
    {
        // Methods
        // RVA: 0x0B2CA620  token: 0x60004BD
        public System.Void .ctor(System.String pageName, System.String packageName) { }

    }

    // TypeToken: 0x20000D8  // size: 0x10
    public class DocumentationInfo
    {
        // Fields
        private static System.String fallbackVersion;  // const
        private static System.String url;  // const

        // Properties
        System.String version { get; /* RVA: 0x0B2CAF9C */ }

        // Methods
        // RVA: 0x0B2CAF40  token: 0x60004BF
        public static System.String GetPageLink(System.String packageName, System.String pageName) { }
        // RVA: 0x041E1670  token: 0x60004C0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D9  // size: 0x10
    public static class DocumentationUtils
    {
        // Methods
        // RVA: -1  // generic def  token: 0x60004C1
        public static System.String GetHelpURL(TEnum mask) { }

    }

    // TypeToken: 0x20000DA  // size: 0x14
    public sealed struct APVConstantBufferRegister
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.APVConstantBufferRegister GlobalRegister;  // const

    }

    // TypeToken: 0x20000DB  // size: 0x50
    public sealed struct ShaderVariablesProbeVolumes
    {
        // Fields
        public UnityEngine.Vector3 _PoolDim;  // 0x10
        public System.Single _ViewBias;  // 0x1c
        public UnityEngine.Vector3 _MinCellPosition;  // 0x20
        public System.Single _PVSamplingNoise;  // 0x2c
        public UnityEngine.Vector3 _CellIndicesDim;  // 0x30
        public System.Single _CellInMeters;  // 0x3c
        public System.Single _CellInMinBricks;  // 0x40
        public System.Single _MinBrickSize;  // 0x44
        public System.Int32 _IndexChunkSize;  // 0x48
        public System.Single _NormalBias;  // 0x4c

    }

    // TypeToken: 0x20000DC  // size: 0x40
    public sealed struct SphericalHarmonicsL1
    {
        // Fields
        public UnityEngine.Vector4 shAr;  // 0x10
        public UnityEngine.Vector4 shAg;  // 0x20
        public UnityEngine.Vector4 shAb;  // 0x30
        public static readonly UnityEngine.Rendering.SphericalHarmonicsL1 zero;  // static @ 0x0

        // Methods
        // RVA: 0x0B2D42A8  token: 0x60004C2
        public static UnityEngine.Rendering.SphericalHarmonicsL1 op_Addition(UnityEngine.Rendering.SphericalHarmonicsL1 lhs, UnityEngine.Rendering.SphericalHarmonicsL1 rhs) { }
        // RVA: 0x0B2D46AC  token: 0x60004C3
        public static UnityEngine.Rendering.SphericalHarmonicsL1 op_Subtraction(UnityEngine.Rendering.SphericalHarmonicsL1 lhs, UnityEngine.Rendering.SphericalHarmonicsL1 rhs) { }
        // RVA: 0x0B2D4620  token: 0x60004C4
        public static UnityEngine.Rendering.SphericalHarmonicsL1 op_Multiply(UnityEngine.Rendering.SphericalHarmonicsL1 lhs, System.Single rhs) { }
        // RVA: 0x0B2D4360  token: 0x60004C5
        public static UnityEngine.Rendering.SphericalHarmonicsL1 op_Division(UnityEngine.Rendering.SphericalHarmonicsL1 lhs, System.Single rhs) { }
        // RVA: 0x0B2D4448  token: 0x60004C6
        public static System.Boolean op_Equality(UnityEngine.Rendering.SphericalHarmonicsL1 lhs, UnityEngine.Rendering.SphericalHarmonicsL1 rhs) { }
        // RVA: 0x0B2D458C  token: 0x60004C7
        public static System.Boolean op_Inequality(UnityEngine.Rendering.SphericalHarmonicsL1 lhs, UnityEngine.Rendering.SphericalHarmonicsL1 rhs) { }
        // RVA: 0x0B2D410C  token: 0x60004C8
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x0B2D41F0  token: 0x60004C9
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B2D4240  token: 0x60004CA
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000DD  // size: 0x10
    public class SphericalHarmonicsL2Utils
    {
        // Methods
        // RVA: 0x0B2D47E8  token: 0x60004CB
        public static System.Void GetL1(UnityEngine.Rendering.SphericalHarmonicsL2 sh, UnityEngine.Vector3& L1_R, UnityEngine.Vector3& L1_G, UnityEngine.Vector3& L1_B) { }
        // RVA: 0x0B2D490C  token: 0x60004CC
        public static System.Void GetL2(UnityEngine.Rendering.SphericalHarmonicsL2 sh, UnityEngine.Vector3& L2_0, UnityEngine.Vector3& L2_1, UnityEngine.Vector3& L2_2, UnityEngine.Vector3& L2_3, UnityEngine.Vector3& L2_4) { }
        // RVA: 0x0B2D4B4C  token: 0x60004CD
        public static System.Void SetL0(UnityEngine.Rendering.SphericalHarmonicsL2& sh, UnityEngine.Vector3 L0) { }
        // RVA: 0x0B2D4C88  token: 0x60004CE
        public static System.Void SetL1R(UnityEngine.Rendering.SphericalHarmonicsL2& sh, UnityEngine.Vector3 L1_R) { }
        // RVA: 0x0B2D4C1C  token: 0x60004CF
        public static System.Void SetL1G(UnityEngine.Rendering.SphericalHarmonicsL2& sh, UnityEngine.Vector3 L1_G) { }
        // RVA: 0x0B2D4BB0  token: 0x60004D0
        public static System.Void SetL1B(UnityEngine.Rendering.SphericalHarmonicsL2& sh, UnityEngine.Vector3 L1_B) { }
        // RVA: 0x0B2D4CEC  token: 0x60004D1
        public static System.Void SetL1(UnityEngine.Rendering.SphericalHarmonicsL2& sh, UnityEngine.Vector3 L1_R, UnityEngine.Vector3 L1_G, UnityEngine.Vector3 L1_B) { }
        // RVA: 0x0B2D4AD8  token: 0x60004D2
        public static System.Void SetCoefficient(UnityEngine.Rendering.SphericalHarmonicsL2& sh, System.Int32 index, UnityEngine.Vector3 coefficient) { }
        // RVA: 0x0B2D4764  token: 0x60004D3
        public static UnityEngine.Vector3 GetCoefficient(UnityEngine.Rendering.SphericalHarmonicsL2 sh, System.Int32 index) { }
        // RVA: 0x041E1670  token: 0x60004D4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000DE  // size: 0x10
    public sealed class LensFlareCommonSRP
    {
        // Fields
        private static UnityEngine.Rendering.LensFlareCommonSRP m_Instance;  // static @ 0x0
        private static readonly System.Object m_Padlock;  // static @ 0x8
        private static System.Collections.Generic.List<UnityEngine.Rendering.LensFlareComponentSRP> m_Data;  // static @ 0x10
        public static System.Int32 maxLensFlareWithOcclusion;  // static @ 0x18
        public static System.Int32 maxLensFlareWithOcclusionTemporalSample;  // static @ 0x1c
        public static System.Int32 mergeNeeded;  // static @ 0x20
        public static UnityEngine.Rendering.RTHandle occlusionRT;  // static @ 0x28
        private static System.Int32 frameIdx;  // static @ 0x30

        // Properties
        UnityEngine.Rendering.LensFlareCommonSRP Instance { get; /* RVA: 0x03AAE370 */ }
        System.Collections.Generic.List<UnityEngine.Rendering.LensFlareComponentSRP> Data { get; /* RVA: 0x0B2D1184 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60004D5
        private System.Void .ctor() { }
        // RVA: 0x0B2CFDF8  token: 0x60004D6
        public static System.Void Initialize() { }
        // RVA: 0x0B2CC848  token: 0x60004D7
        public static System.Void Dispose() { }
        // RVA: 0x05394AF0  token: 0x60004DA
        public System.Collections.Generic.List<UnityEngine.Rendering.LensFlareComponentSRP> GetData() { }
        // RVA: 0x03AAE0F0  token: 0x60004DB
        public System.Boolean IsEmpty() { }
        // RVA: 0x0B2CB67C  token: 0x60004DC
        public System.Void AddData(UnityEngine.Rendering.LensFlareComponentSRP newData) { }
        // RVA: 0x04D88DD0  token: 0x60004DD
        public static System.Single ShapeAttenuationPointLight() { }
        // RVA: 0x0B2D0518  token: 0x60004DE
        public static System.Single ShapeAttenuationDirLight(UnityEngine.Vector3 forward, UnityEngine.Vector3 wo) { }
        // RVA: 0x0B2D05A4  token: 0x60004DF
        public static System.Single ShapeAttenuationSpotConeLight(UnityEngine.Vector3 forward, UnityEngine.Vector3 wo, System.Single spotAngle, System.Single innerSpotPercent01) { }
        // RVA: 0x0B2D055C  token: 0x60004E0
        public static System.Single ShapeAttenuationSpotBoxLight(UnityEngine.Vector3 forward, UnityEngine.Vector3 wo) { }
        // RVA: 0x0B2D0698  token: 0x60004E1
        public static System.Single ShapeAttenuationSpotPyramidLight(UnityEngine.Vector3 forward, UnityEngine.Vector3 wo) { }
        // RVA: 0x0B2D0100  token: 0x60004E2
        public static System.Single ShapeAttenuationAreaTubeLight(UnityEngine.Vector3 lightPositionWS, UnityEngine.Vector3 lightSide, System.Single lightWidth, UnityEngine.Camera cam) { }
        // RVA: 0x0B2D0084  token: 0x60004E3
        public static System.Single ShapeAttenuationAreaRectangleLight(UnityEngine.Vector3 forward, UnityEngine.Vector3 wo) { }
        // RVA: 0x0B2D0008  token: 0x60004E4
        public static System.Single ShapeAttenuationAreaDiscLight(UnityEngine.Vector3 forward, UnityEngine.Vector3 wo) { }
        // RVA: 0x0B2CFB30  token: 0x60004E5
        public static UnityEngine.Vector4 GetFlareData0(UnityEngine.Vector2 screenPos, UnityEngine.Vector2 translationScale, UnityEngine.Vector2 rayOff0, UnityEngine.Vector2 vLocalScreenRatio, System.Single angleDeg, System.Single position, System.Single angularOffset, UnityEngine.Vector2 positionOffset, System.Boolean autoRotate) { }
        // RVA: 0x0B2CFD70  token: 0x60004E6
        public static UnityEngine.Vector2 GetLensFlareRayOffset(UnityEngine.Vector2 screenPos, System.Single position, System.Single globalCos0, System.Single globalSin0) { }
        // RVA: 0x0B2D1048  token: 0x60004E7
        public static UnityEngine.Vector3 WorldToViewport(UnityEngine.Camera camera, System.Boolean isLocalLight, System.Boolean isCameraRelative, UnityEngine.Matrix4x4 viewProjMatrix, UnityEngine.Vector3 positionWS) { }
        // RVA: 0x0B2D0EEC  token: 0x60004E8
        private static UnityEngine.Vector3 WorldToViewportLocal(System.Boolean isCameraRelative, UnityEngine.Matrix4x4 viewProjMatrix, UnityEngine.Vector3 cameraPosWS, UnityEngine.Vector3 positionWS) { }
        // RVA: 0x0B2D0D60  token: 0x60004E9
        private static UnityEngine.Vector3 WorldToViewportDistance(UnityEngine.Camera cam, UnityEngine.Vector3 positionWS) { }
        // RVA: 0x0B2CB830  token: 0x60004EA
        public static System.Void ComputeOcclusion(UnityEngine.Material lensFlareShader, UnityEngine.Rendering.LensFlareCommonSRP lensFlares, UnityEngine.Camera cam, System.Single actualWidth, System.Single actualHeight, System.Boolean usePanini, System.Single paniniDistance, System.Single paniniCropToFit, System.Boolean isCameraRelative, UnityEngine.Vector3 cameraPositionWS, UnityEngine.Matrix4x4 viewProjMatrix, UnityEngine.Rendering.CommandBuffer cmd, System.Boolean taaEnabled, System.Int32 _FlareOcclusionTex, System.Int32 _FlareOcclusionIndex, System.Int32 _FlareTex, System.Int32 _FlareColorValue, System.Int32 _FlareData0, System.Int32 _FlareData1, System.Int32 _FlareData2, System.Int32 _FlareData3, System.Int32 _FlareData4) { }
        // RVA: 0x0B2CC8E4  token: 0x60004EB
        public static System.Void DoLensFlareDataDrivenCommon(UnityEngine.Material lensFlareShader, UnityEngine.Rendering.LensFlareCommonSRP lensFlares, UnityEngine.Camera cam, System.Single actualWidth, System.Single actualHeight, System.Boolean usePanini, System.Single paniniDistance, System.Single paniniCropToFit, System.Boolean isCameraRelative, UnityEngine.Vector3 cameraPositionWS, UnityEngine.Matrix4x4 viewProjMatrix, UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, System.Func<UnityEngine.Light,UnityEngine.Camera,UnityEngine.Vector3,System.Single> GetLensFlareLightAttenuation, System.Int32 _FlareOcclusionTex, System.Int32 _FlareOcclusionIndex, System.Int32 _FlareTex, System.Int32 _FlareColorValue, System.Int32 _FlareData0, System.Int32 _FlareData1, System.Int32 _FlareData2, System.Int32 _FlareData3, System.Int32 _FlareData4, System.Boolean debugFullScreen, UnityEngine.ComputeBuffer debugFullscreenBuffer, UnityEngine.Rendering.LensFlareCommonSRP.SunSourceDirLightOverrideInfo dirLightOverrideInfo) { }
        // RVA: 0x03AADF50  token: 0x60004EC
        public System.Void RemoveData(UnityEngine.Rendering.LensFlareComponentSRP data) { }
        // RVA: 0x0B2CF974  token: 0x60004ED
        private static UnityEngine.Vector2 DoPaniniProjection(UnityEngine.Vector2 screenPos, System.Single actualWidth, System.Single actualHeight, System.Single fieldOfView, System.Single paniniProjectionCropToFit, System.Single paniniProjectionDistance) { }
        // RVA: 0x0B2CB7E4  token: 0x60004EE
        private static UnityEngine.Vector2 CalcViewExtents(System.Single actualWidth, System.Single actualHeight, System.Single fieldOfView) { }
        // RVA: 0x0B2CB6FC  token: 0x60004EF
        private static UnityEngine.Vector2 CalcCropExtents(System.Single actualWidth, System.Single actualHeight, System.Single fieldOfView, System.Single d) { }
        // RVA: 0x0B2CFF8C  token: 0x60004F0
        private static UnityEngine.Vector2 Panini_Generic_Inv(UnityEngine.Vector2 projPos, System.Single d) { }
        // RVA: 0x04B1D060  token: 0x60004F1
        private static System.Void .cctor() { }
        // RVA: 0x0B2D0CEC  token: 0x60004F2
        private static System.Single <ShapeAttenuationAreaTubeLight>g__Fpo|26_0(System.Single d, System.Single l) { }
        // RVA: 0x0B2D0D44  token: 0x60004F3
        private static System.Single <ShapeAttenuationAreaTubeLight>g__Fwt|26_1(System.Single d, System.Single l) { }
        // RVA: 0x0B2D0910  token: 0x60004F4
        private static System.Single <ShapeAttenuationAreaTubeLight>g__DiffLineIntegral|26_2(UnityEngine.Vector3 p1, UnityEngine.Vector3 p2) { }
        // RVA: 0x0B2D071C  token: 0x60004F5
        private static UnityEngine.Vector2 <DoLensFlareDataDrivenCommon>g__ComputeLocalSize|35_0(UnityEngine.Vector2 rayOff, UnityEngine.Vector2 rayOff0, UnityEngine.Vector2 curSize, UnityEngine.AnimationCurve distortionCurve, UnityEngine.Rendering.LensFlareCommonSRP.<>c__DisplayClass35_0& , UnityEngine.Rendering.LensFlareCommonSRP.<>c__DisplayClass35_1& ) { }
        // RVA: 0x09BBB1DC  token: 0x60004F6
        private static System.Single <DoLensFlareDataDrivenCommon>g__RandomRange|35_1(System.Single min, System.Single max) { }

    }

    // TypeToken: 0x20000E4  // size: 0x78
    public sealed class LensFlareComponentSRP : UnityEngine.MonoBehaviour
    {
        // Fields
        private UnityEngine.Rendering.LensFlareDataSRP m_LensFlareData;  // 0x18
        public System.Single intensity;  // 0x20
        public System.Single maxAttenuationDistance;  // 0x24
        public System.Single maxAttenuationScale;  // 0x28
        public UnityEngine.AnimationCurve distanceAttenuationCurve;  // 0x30
        public UnityEngine.AnimationCurve scaleByDistanceCurve;  // 0x38
        public System.Boolean attenuationByLightShape;  // 0x40
        public UnityEngine.AnimationCurve radialScreenAttenuationCurve;  // 0x48
        public System.Boolean useOcclusion;  // 0x50
        public System.Single occlusionRadius;  // 0x54
        public System.UInt32 sampleCount;  // 0x58
        public System.Single occlusionOffset;  // 0x5c
        public System.Single scale;  // 0x60
        public System.Boolean allowOffScreen;  // 0x64
        public System.Boolean useCustomForward;  // 0x65
        public UnityEngine.Vector3 customForward;  // 0x68
        private static System.Single sCelestialAngularRadius;  // static @ 0x0

        // Properties
        UnityEngine.Rendering.LensFlareDataSRP lensFlareData { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x0B2D1300 */ }

        // Methods
        // RVA: 0x0B2D1250  token: 0x60004F9
        public System.Single celestialProjectedOcclusionRadius(UnityEngine.Camera mainCam) { }
        // RVA: 0x03AADFF0  token: 0x60004FA
        private System.Void OnEnable() { }
        // RVA: 0x03AAE090  token: 0x60004FB
        private System.Void OnDisable() { }
        // RVA: 0x0B2D11B8  token: 0x60004FC
        private System.Void OnValidate() { }
        // RVA: 0x0402B7F0  token: 0x60004FD
        public System.Void .ctor() { }
        // RVA: 0x04D7A250  token: 0x60004FE
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000E5  // size: 0x14
    public sealed struct SRPLensFlareBlendMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.SRPLensFlareBlendMode Additive;  // const
        public static UnityEngine.Rendering.SRPLensFlareBlendMode Screen;  // const
        public static UnityEngine.Rendering.SRPLensFlareBlendMode Premultiply;  // const
        public static UnityEngine.Rendering.SRPLensFlareBlendMode Lerp;  // const

    }

    // TypeToken: 0x20000E6  // size: 0x14
    public sealed struct SRPLensFlareDistribution
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.SRPLensFlareDistribution Uniform;  // const
        public static UnityEngine.Rendering.SRPLensFlareDistribution Curve;  // const
        public static UnityEngine.Rendering.SRPLensFlareDistribution Random;  // const

    }

    // TypeToken: 0x20000E7  // size: 0x14
    public sealed struct SRPLensFlareType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.SRPLensFlareType Image;  // const
        public static UnityEngine.Rendering.SRPLensFlareType Circle;  // const
        public static UnityEngine.Rendering.SRPLensFlareType Polygon;  // const

    }

    // TypeToken: 0x20000E8  // size: 0xF0
    public sealed class LensFlareDataElementSRP
    {
        // Fields
        public System.Boolean visible;  // 0x10
        public System.Single position;  // 0x14
        public UnityEngine.Vector2 positionOffset;  // 0x18
        public System.Single angularOffset;  // 0x20
        public UnityEngine.Vector2 translationScale;  // 0x24
        private System.Single m_LocalIntensity;  // 0x2c
        public UnityEngine.Texture lensFlareTexture;  // 0x30
        public System.Single uniformScale;  // 0x38
        public UnityEngine.Vector2 sizeXY;  // 0x3c
        public System.Boolean allowMultipleElement;  // 0x44
        private System.Int32 m_Count;  // 0x48
        public System.Boolean preserveAspectRatio;  // 0x4c
        public System.Single rotation;  // 0x50
        public UnityEngine.Color tint;  // 0x54
        public UnityEngine.Rendering.SRPLensFlareBlendMode blendMode;  // 0x64
        public System.Boolean autoRotate;  // 0x68
        public UnityEngine.Rendering.SRPLensFlareType flareType;  // 0x6c
        public System.Boolean modulateByLightColor;  // 0x70
        private System.Boolean isFoldOpened;  // 0x71
        public UnityEngine.Rendering.SRPLensFlareDistribution distribution;  // 0x74
        public System.Single lengthSpread;  // 0x78
        public UnityEngine.AnimationCurve positionCurve;  // 0x80
        public UnityEngine.AnimationCurve scaleCurve;  // 0x88
        public System.Int32 seed;  // 0x90
        public UnityEngine.Gradient colorGradient;  // 0x98
        private System.Single m_IntensityVariation;  // 0xa0
        public UnityEngine.Vector2 positionVariation;  // 0xa4
        public System.Single scaleVariation;  // 0xac
        public System.Single rotationVariation;  // 0xb0
        public System.Boolean enableRadialDistortion;  // 0xb4
        public UnityEngine.Vector2 targetSizeDistortion;  // 0xb8
        public UnityEngine.AnimationCurve distortionCurve;  // 0xc0
        public System.Boolean distortionRelativeToCenter;  // 0xc8
        private System.Single m_FallOff;  // 0xcc
        private System.Single m_EdgeOffset;  // 0xd0
        private System.Int32 m_SideCount;  // 0xd4
        private System.Single m_SdfRoundness;  // 0xd8
        public System.Boolean inverseSDF;  // 0xdc
        public System.Single uniformAngle;  // 0xe0
        public UnityEngine.AnimationCurve uniformAngleCurve;  // 0xe8

        // Properties
        System.Single localIntensity { get; /* RVA: 0x04D86AF0 */ set; /* RVA: 0x0B2D1380 */ }
        System.Int32 count { get; /* RVA: 0x04D863D0 */ set; /* RVA: 0x0B2D1324 */ }
        System.Single intensityVariation { get; /* RVA: 0x04D91E60 */ set; /* RVA: 0x0B2D136C */ }
        System.Single fallOff { get; /* RVA: 0x04D9DFA0 */ set; /* RVA: 0x0B2D1350 */ }
        System.Single edgeOffset { get; /* RVA: 0x04D8E2E0 */ set; /* RVA: 0x0B2D1334 */ }
        System.Int32 sideCount { get; /* RVA: 0x04DB8DE0 */ set; /* RVA: 0x0B2D13AC */ }
        System.Single sdfRoundness { get; /* RVA: 0x04D9DFB0 */ set; /* RVA: 0x0B2D1390 */ }

        // Methods
        // RVA: 0x039C4B00  token: 0x60004FF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E9  // size: 0x20
    public sealed class LensFlareDataSRP : UnityEngine.ScriptableObject
    {
        // Fields
        public UnityEngine.Rendering.LensFlareDataElementSRP[] elements;  // 0x18

        // Methods
        // RVA: 0x04D5ECE0  token: 0x600050E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000EA  // size: 0x18
    public abstract class RenderPipelineResources : UnityEngine.ScriptableObject
    {
        // Properties
        System.String packagePath { get; /* RVA: 0x011EC580 */ }
        System.String packagePath_Internal { get; /* RVA: 0x05395664 */ }

        // Methods
        // RVA: 0x03573370  token: 0x6000511
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000EB  // size: 0x14
    public sealed struct PackingRules
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.PackingRules Exact;  // const
        public static UnityEngine.Rendering.PackingRules Aggressive;  // const

    }

    // TypeToken: 0x20000EC  // size: 0x14
    public sealed struct FieldPacking
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.FieldPacking NoPacking;  // const
        public static UnityEngine.Rendering.FieldPacking R11G11B10;  // const
        public static UnityEngine.Rendering.FieldPacking PackedFloat;  // const
        public static UnityEngine.Rendering.FieldPacking PackedUint;  // const

    }

    // TypeToken: 0x20000ED  // size: 0x14
    public sealed struct FieldPrecision
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.FieldPrecision Half;  // const
        public static UnityEngine.Rendering.FieldPrecision Real;  // const
        public static UnityEngine.Rendering.FieldPrecision Default;  // const

    }

    // TypeToken: 0x20000EE  // size: 0x30
    public class GenerateHLSL : System.Attribute
    {
        // Fields
        public UnityEngine.Rendering.PackingRules packingRules;  // 0x10
        public System.Boolean containsPackedFields;  // 0x14
        public System.Boolean needAccessors;  // 0x15
        public System.Boolean needSetters;  // 0x16
        public System.Boolean needParamDebug;  // 0x17
        public System.Int32 paramDefinesStart;  // 0x18
        public System.Boolean omitStructDeclaration;  // 0x1c
        public System.Boolean generateCBuffer;  // 0x1d
        public System.Int32 constantRegister;  // 0x20
        public System.String sourcePath;  // 0x28

        // Methods
        // RVA: 0x0B2CAFC8  token: 0x6000512
        public System.Void .ctor(UnityEngine.Rendering.PackingRules rules, System.Boolean needAccessors, System.Boolean needSetters, System.Boolean needParamDebug, System.Int32 paramDefinesStart, System.Boolean omitStructDeclaration, System.Boolean containsPackedFields, System.Boolean generateCBuffer, System.Int32 constantRegister, System.String sourcePath) { }

    }

    // TypeToken: 0x20000EF  // size: 0x30
    public class SurfaceDataAttributes : System.Attribute
    {
        // Fields
        public System.String[] displayNames;  // 0x10
        public System.Boolean isDirection;  // 0x18
        public System.Boolean sRGBDisplay;  // 0x19
        public UnityEngine.Rendering.FieldPrecision precision;  // 0x1c
        public System.Boolean checkIsNormalized;  // 0x20
        public System.String preprocessor;  // 0x28

        // Methods
        // RVA: 0x0B2D4D74  token: 0x6000513
        public System.Void .ctor(System.String displayName, System.Boolean isDirection, System.Boolean sRGBDisplay, UnityEngine.Rendering.FieldPrecision precision, System.Boolean checkIsNormalized, System.String preprocessor) { }
        // RVA: 0x0B2D4E24  token: 0x6000514
        public System.Void .ctor(System.String[] displayNames, System.Boolean isDirection, System.Boolean sRGBDisplay, UnityEngine.Rendering.FieldPrecision precision, System.Boolean checkIsNormalized, System.String preprocessor) { }

    }

    // TypeToken: 0x20000F0  // size: 0x20
    public class HLSLArray : System.Attribute
    {
        // Fields
        public System.Int32 arraySize;  // 0x10
        public System.Type elementType;  // 0x18

        // Methods
        // RVA: 0x05391D70  token: 0x6000515
        public System.Void .ctor(System.Int32 arraySize, System.Type elementType) { }

    }

    // TypeToken: 0x20000F1  // size: 0x38
    public class PackingAttribute : System.Attribute
    {
        // Fields
        public System.String[] displayNames;  // 0x10
        public System.Single[] range;  // 0x18
        public UnityEngine.Rendering.FieldPacking packingScheme;  // 0x20
        public System.Int32 offsetInSource;  // 0x24
        public System.Int32 sizeInBits;  // 0x28
        public System.Boolean isDirection;  // 0x2c
        public System.Boolean sRGBDisplay;  // 0x2d
        public System.Boolean checkIsNormalized;  // 0x2e
        public System.String preprocessor;  // 0x30

        // Methods
        // RVA: 0x0B2D1438  token: 0x6000516
        public System.Void .ctor(System.String[] displayNames, UnityEngine.Rendering.FieldPacking packingScheme, System.Int32 bitSize, System.Int32 offsetInSource, System.Single minValue, System.Single maxValue, System.Boolean isDirection, System.Boolean sRGBDisplay, System.Boolean checkIsNormalized, System.String preprocessor) { }
        // RVA: 0x0B2D1520  token: 0x6000517
        public System.Void .ctor(System.String displayName, UnityEngine.Rendering.FieldPacking packingScheme, System.Int32 bitSize, System.Int32 offsetInSource, System.Single minValue, System.Single maxValue, System.Boolean isDirection, System.Boolean sRGBDisplay, System.Boolean checkIsNormalized, System.String preprocessor) { }

    }

    // TypeToken: 0x20000F2  // size: 0x11
    public sealed struct ShaderGenUInt4
    {
    }

    // TypeToken: 0x20000F3  // size: 0x10
    public static class Hammersley
    {
        // Fields
        private static System.Single[] k_Hammersley2dSeq16;  // static @ 0x0
        private static System.Single[] k_Hammersley2dSeq32;  // static @ 0x8
        private static System.Single[] k_Hammersley2dSeq64;  // static @ 0x10
        private static System.Single[] k_Hammersley2dSeq256;  // static @ 0x18
        private static readonly System.Int32 s_hammersley2DSeq16Id;  // static @ 0x20
        private static readonly System.Int32 s_hammersley2DSeq32Id;  // static @ 0x24
        private static readonly System.Int32 s_hammersley2DSeq64Id;  // static @ 0x28
        private static readonly System.Int32 s_hammersley2DSeq256Id;  // static @ 0x2c

        // Methods
        // RVA: 0x0B2CB150  token: 0x6000518
        public static System.Void Initialize() { }
        // RVA: 0x0B2CB028  token: 0x6000519
        public static System.Void BindConstants(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.ComputeShader cs) { }
        // RVA: 0x0B2CB434  token: 0x600051A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000FC  // size: 0x20
    public class BufferedRTHandleSystem : System.IDisposable
    {
        // Fields
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Rendering.RTHandle[]> m_RTHandles;  // 0x10
        private System.Boolean m_DisposedValue;  // 0x18

        // Methods
        // RVA: 0x0B2CA220  token: 0x600051B
        public UnityEngine.Rendering.RTHandle GetFrameRT(System.Int32 bufferId, System.Int32 frameIndex) { }
        // RVA: 0x0B2CA0F8  token: 0x600051C
        public System.Void AllocBuffer(System.Int32 bufferId, System.Func<System.Int32,UnityEngine.Rendering.RTHandle> allocator, System.Int32 bufferCount) { }
        // RVA: 0x0B2CA348  token: 0x600051D
        public System.Void ReleaseBuffer(System.Int32 bufferId) { }
        // RVA: 0x0B2CA2C0  token: 0x600051E
        public System.Int32 GetNumFramesAllocated(System.Int32 bufferId) { }
        // RVA: 0x0B2CA41C  token: 0x600051F
        private System.Void Swap() { }
        // RVA: 0x047006E0  token: 0x6000520
        private System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x0B2CA214  token: 0x6000521
        public virtual System.Void Dispose() { }
        // RVA: 0x04700710  token: 0x6000522
        public System.Void ReleaseAll() { }
        // RVA: 0x04A3B350  token: 0x6000523
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000FD  // size: 0x14
    public sealed struct DepthBits
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.DepthBits None;  // const
        public static UnityEngine.Rendering.DepthBits Depth8;  // const
        public static UnityEngine.Rendering.DepthBits Depth16;  // const
        public static UnityEngine.Rendering.DepthBits Depth24;  // const
        public static UnityEngine.Rendering.DepthBits Depth32;  // const

    }

    // TypeToken: 0x20000FE  // size: 0x14
    public sealed struct MSAASamples
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.MSAASamples None;  // const
        public static UnityEngine.Rendering.MSAASamples MSAA2x;  // const
        public static UnityEngine.Rendering.MSAASamples MSAA4x;  // const
        public static UnityEngine.Rendering.MSAASamples MSAA8x;  // const

    }

    // TypeToken: 0x20000FF  // size: 0x58
    public class PowerOfTwoTextureAtlas : UnityEngine.Rendering.Texture2DAtlas
    {
        // Fields
        private readonly System.Int32 m_MipPadding;  // 0x48
        private static System.Single k_MipmapFactorApprox;  // const
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Vector2Int> m_RequestedTextures;  // 0x50

        // Properties
        System.Int32 mipPadding { get; /* RVA: 0x04D863D0 */ }

        // Methods
        // RVA: 0x0B2D306C  token: 0x6000524
        public System.Void .ctor(System.Int32 size, System.Int32 mipPadding, UnityEngine.Experimental.Rendering.GraphicsFormat format, UnityEngine.FilterMode filterMode, System.String name, System.Boolean useMipMap) { }
        // RVA: 0x04DB8DF0  token: 0x6000526
        private System.Int32 GetTexturePadding() { }
        // RVA: 0x0B2D2870  token: 0x6000527
        public UnityEngine.Vector4 GetPayloadScaleOffset(UnityEngine.Texture texture, UnityEngine.Vector4& scaleOffset) { }
        // RVA: 0x0B2D2758  token: 0x6000528
        public static UnityEngine.Vector4 GetPayloadScaleOffset(UnityEngine.Vector2& textureSize, UnityEngine.Vector2& paddingSize, UnityEngine.Vector4& scaleOffset) { }
        // RVA: 0x0B2D1EC0  token: 0x6000529
        private System.Void Blit2DTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4 scaleOffset, UnityEngine.Texture texture, UnityEngine.Vector4 sourceScaleOffset, System.Boolean blitMips, UnityEngine.Rendering.PowerOfTwoTextureAtlas.BlitType blitType) { }
        // RVA: 0x0B2D2514  token: 0x600052A
        public virtual System.Void BlitTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4 scaleOffset, UnityEngine.Texture texture, UnityEngine.Vector4 sourceScaleOffset, System.Boolean blitMips, System.Int32 overrideInstanceID) { }
        // RVA: 0x0B2D2440  token: 0x600052B
        public System.Void BlitTextureMultiply(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4 scaleOffset, UnityEngine.Texture texture, UnityEngine.Vector4 sourceScaleOffset, System.Boolean blitMips, System.Int32 overrideInstanceID) { }
        // RVA: 0x0B2D236C  token: 0x600052C
        public virtual System.Void BlitOctahedralTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4 scaleOffset, UnityEngine.Texture texture, UnityEngine.Vector4 sourceScaleOffset, System.Boolean blitMips, System.Int32 overrideInstanceID) { }
        // RVA: 0x0B2D2298  token: 0x600052D
        public System.Void BlitOctahedralTextureMultiply(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4 scaleOffset, UnityEngine.Texture texture, UnityEngine.Vector4 sourceScaleOffset, System.Boolean blitMips, System.Int32 overrideInstanceID) { }
        // RVA: 0x0B2D3038  token: 0x600052E
        private System.Void TextureSizeToPowerOfTwo(UnityEngine.Texture texture, System.Int32& width, System.Int32& height) { }
        // RVA: 0x0B2D291C  token: 0x600052F
        private UnityEngine.Vector2 GetPowerOfTwoTextureSize(UnityEngine.Texture texture) { }
        // RVA: 0x0B2D1D28  token: 0x6000530
        public virtual System.Boolean AllocateTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4& scaleOffset, UnityEngine.Texture texture, System.Int32 width, System.Int32 height, System.Int32 overrideInstanceID) { }
        // RVA: 0x0B2D2FF0  token: 0x6000531
        public System.Void ResetRequestedTexture() { }
        // RVA: 0x0B2D2E14  token: 0x6000532
        public System.Boolean ReserveSpace(UnityEngine.Texture texture) { }
        // RVA: 0x0B2D2FA8  token: 0x6000533
        public System.Boolean ReserveSpace(UnityEngine.Texture texture, System.Int32 width, System.Int32 height) { }
        // RVA: 0x0B2D2F68  token: 0x6000534
        public System.Boolean ReserveSpace(UnityEngine.Texture textureA, UnityEngine.Texture textureB, System.Int32 width, System.Int32 height) { }
        // RVA: 0x0B2D2E74  token: 0x6000535
        private System.Boolean ReserveSpace(System.Int32 id, System.Int32 width, System.Int32 height) { }
        // RVA: 0x0B2D2998  token: 0x6000536
        public System.Boolean RelayoutEntries() { }
        // RVA: 0x0B2D25E0  token: 0x6000537
        public static System.Int64 GetApproxCacheSizeInByte(System.Int32 nbElement, System.Int32 resolution, System.Boolean hasMipmap, UnityEngine.Experimental.Rendering.GraphicsFormat format) { }
        // RVA: 0x0B2D2690  token: 0x6000538
        public static System.Int32 GetMaxCacheSizeForWeightInByte(System.Int32 weight, System.Boolean hasMipmap, UnityEngine.Experimental.Rendering.GraphicsFormat format) { }

    }

    // TypeToken: 0x2000102  // size: 0x58
    public class RTHandle
    {
        // Fields
        private UnityEngine.RenderTexture m_RT;  // 0x10
        private UnityEngine.Texture m_ExternalTexture;  // 0x18
        private UnityEngine.Rendering.RenderTargetIdentifier m_NameID;  // 0x20
        private System.Boolean m_EnableMSAA;  // 0x48
        private System.Boolean m_EnableRandomWrite;  // 0x49
        private System.String m_Name;  // 0x50

        // Properties
        UnityEngine.RenderTexture rt { get; /* RVA: 0x02B2ECC0 */ }
        UnityEngine.Rendering.RenderTargetIdentifier nameID { get; /* RVA: 0x04D8E8C0 */ }
        System.String name { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x0B2D3EC0 */ }
        System.Boolean isMSAAEnabled { get; /* RVA: 0x04D867D0 */ }

        // Methods
        // RVA: 0x0B2D3E14  token: 0x6000541
        public static UnityEngine.Rendering.RenderTargetIdentifier op_Implicit(UnityEngine.Rendering.RTHandle handle) { }
        // RVA: 0x0B2D3E58  token: 0x6000542
        public static UnityEngine.Texture op_Implicit(UnityEngine.Rendering.RTHandle handle) { }
        // RVA: 0x033021A0  token: 0x6000543
        public static UnityEngine.RenderTexture op_Implicit(UnityEngine.Rendering.RTHandle handle) { }
        // RVA: 0x037DF870  token: 0x6000544
        public System.Void SetRenderTexture(UnityEngine.RenderTexture rt) { }
        // RVA: 0x037DF980  token: 0x6000545
        public System.Void SetTexture(UnityEngine.Texture tex) { }
        // RVA: 0x0B2D3CD8  token: 0x6000546
        public System.Void SetTexture(UnityEngine.Rendering.RenderTargetIdentifier tex) { }
        // RVA: 0x0B2D3C34  token: 0x6000547
        public System.Int32 GetInstanceID() { }
        // RVA: 0x037DC790  token: 0x6000548
        public System.Void Release() { }
        // RVA: 0x0B2D3D88  token: 0x6000549
        public System.Void SwitchToFastMemory(UnityEngine.Rendering.CommandBuffer cmd, System.Single residencyFraction, UnityEngine.Rendering.FastMemoryFlags flags, System.Boolean copyContents) { }
        // RVA: 0x0B2D3C18  token: 0x600054A
        public System.Void CopyToFastMemory(UnityEngine.Rendering.CommandBuffer cmd, System.Single residencyFraction, UnityEngine.Rendering.FastMemoryFlags flags) { }
        // RVA: 0x0B2D3D24  token: 0x600054B
        public System.Void SwitchOutFastMemory(UnityEngine.Rendering.CommandBuffer cmd, System.Boolean copyContents) { }
        // RVA: 0x041E1670  token: 0x600054C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000103  // size: 0x10
    public static class RTHandles
    {
        // Fields
        private static UnityEngine.Rendering.RTHandleSystem s_DefaultInstance;  // static @ 0x0

        // Methods
        // RVA: 0x037DED20  token: 0x600054D
        public static UnityEngine.Rendering.RTHandle Alloc(System.Int32 width, System.Int32 height, System.Int32 slices, UnityEngine.Rendering.DepthBits depthBufferBits, UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat, UnityEngine.FilterMode filterMode, UnityEngine.TextureWrapMode wrapMode, UnityEngine.Rendering.TextureDimension dimension, System.Boolean enableRandomWrite, System.Boolean useMipMap, System.Boolean autoGenerateMips, System.Boolean isShadowMap, System.Int32 anisoLevel, System.Single mipMapBias, UnityEngine.Rendering.MSAASamples msaaSamples, System.Boolean bindTextureMS, UnityEngine.RenderTextureMemoryless memoryless, System.String name) { }
        // RVA: 0x0B2D3F78  token: 0x600054E
        public static UnityEngine.Rendering.RTHandle Alloc(System.Int32 width, System.Int32 height, UnityEngine.TextureWrapMode wrapModeU, UnityEngine.TextureWrapMode wrapModeV, UnityEngine.TextureWrapMode wrapModeW, System.Int32 slices, UnityEngine.Rendering.DepthBits depthBufferBits, UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat, UnityEngine.FilterMode filterMode, UnityEngine.Rendering.TextureDimension dimension, System.Boolean enableRandomWrite, System.Boolean useMipMap, System.Boolean autoGenerateMips, System.Boolean isShadowMap, System.Int32 anisoLevel, System.Single mipMapBias, UnityEngine.Rendering.MSAASamples msaaSamples, System.Boolean bindTextureMS, System.Boolean useDynamicScale, UnityEngine.RenderTextureMemoryless memoryless, System.String name) { }
        // RVA: 0x05390780  token: 0x600054F
        public static UnityEngine.Rendering.RTHandle Alloc(UnityEngine.Texture tex) { }
        // RVA: 0x0B2D4104  token: 0x6000550
        public static UnityEngine.Rendering.RTHandle Alloc(UnityEngine.RenderTexture tex) { }
        // RVA: 0x0B2D404C  token: 0x6000551
        public static UnityEngine.Rendering.RTHandle Alloc(UnityEngine.Rendering.RenderTargetIdentifier tex) { }
        // RVA: 0x0B2D40D0  token: 0x6000552
        public static UnityEngine.Rendering.RTHandle Alloc(UnityEngine.Rendering.RenderTargetIdentifier tex, System.String name) { }
        // RVA: 0x0B2D4080  token: 0x6000553
        private static UnityEngine.Rendering.RTHandle Alloc(UnityEngine.Rendering.RTHandle tex) { }
        // RVA: 0x04D31EF0  token: 0x6000554
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000104  // size: 0x10
    public class RTHandleSystem
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000555
        public System.Void Initialize() { }
        // RVA: 0x037DEDF0  token: 0x6000556
        public static UnityEngine.Rendering.RTHandle Alloc(System.Int32 width, System.Int32 height, System.Int32 slices, UnityEngine.Rendering.DepthBits depthBufferBits, UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat, UnityEngine.FilterMode filterMode, UnityEngine.TextureWrapMode wrapMode, UnityEngine.Rendering.TextureDimension dimension, System.Boolean enableRandomWrite, System.Boolean useMipMap, System.Boolean autoGenerateMips, System.Boolean isShadowMap, System.Int32 anisoLevel, System.Single mipMapBias, UnityEngine.Rendering.MSAASamples msaaSamples, System.Boolean bindTextureMS, UnityEngine.RenderTextureMemoryless memoryless, System.String name) { }
        // RVA: 0x037DEED0  token: 0x6000557
        public static UnityEngine.Rendering.RTHandle Alloc(System.Int32 width, System.Int32 height, UnityEngine.TextureWrapMode wrapModeU, UnityEngine.TextureWrapMode wrapModeV, UnityEngine.TextureWrapMode wrapModeW, System.Int32 slices, UnityEngine.Rendering.DepthBits depthBufferBits, UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat, UnityEngine.FilterMode filterMode, UnityEngine.Rendering.TextureDimension dimension, System.Boolean enableRandomWrite, System.Boolean useMipMap, System.Boolean autoGenerateMips, System.Boolean isShadowMap, System.Int32 anisoLevel, System.Single mipMapBias, UnityEngine.Rendering.MSAASamples msaaSamples, System.Boolean bindTextureMS, UnityEngine.RenderTextureMemoryless memoryless, System.String name) { }
        // RVA: 0x0B2D35A0  token: 0x6000558
        private static UnityEngine.Rendering.RTHandle AllocAutoSizedRenderTexture(System.Int32 width, System.Int32 height, System.Int32 slices, UnityEngine.Rendering.DepthBits depthBufferBits, UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat, UnityEngine.FilterMode filterMode, UnityEngine.TextureWrapMode wrapMode, UnityEngine.Rendering.TextureDimension dimension, System.Boolean enableRandomWrite, System.Boolean useMipMap, System.Boolean autoGenerateMips, System.Boolean isShadowMap, System.Int32 anisoLevel, System.Single mipMapBias, UnityEngine.Rendering.MSAASamples msaaSamples, System.Boolean bindTextureMS, UnityEngine.RenderTextureMemoryless memoryless, System.String name) { }
        // RVA: 0x0B2D3AAC  token: 0x6000559
        public static UnityEngine.Rendering.RTHandle Alloc(UnityEngine.RenderTexture texture) { }
        // RVA: 0x037DF8F0  token: 0x600055A
        public static UnityEngine.Rendering.RTHandle Alloc(UnityEngine.Texture texture) { }
        // RVA: 0x0B2D3A50  token: 0x600055B
        public static UnityEngine.Rendering.RTHandle Alloc(UnityEngine.Rendering.RenderTargetIdentifier texture) { }
        // RVA: 0x0B2D3B2C  token: 0x600055C
        public static UnityEngine.Rendering.RTHandle Alloc(UnityEngine.Rendering.RenderTargetIdentifier texture, System.String name) { }
        // RVA: 0x0B2D3BC8  token: 0x600055D
        private static UnityEngine.Rendering.RTHandle Alloc(UnityEngine.Rendering.RTHandle tex) { }
        // RVA: 0x041E1670  token: 0x600055E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000105  // size: 0x30
    public class AtlasAllocator
    {
        // Fields
        private UnityEngine.Rendering.AtlasAllocator.AtlasNode m_Root;  // 0x10
        private System.Int32 m_Width;  // 0x18
        private System.Int32 m_Height;  // 0x1c
        private System.Boolean powerOfTwoPadding;  // 0x20
        private UnityEngine.Rendering.ObjectPool<UnityEngine.Rendering.AtlasAllocator.AtlasNode> m_NodePool;  // 0x28

        // Methods
        // RVA: 0x0B2C8C34  token: 0x600055F
        public System.Void .ctor(System.Int32 width, System.Int32 height, System.Boolean potPadding) { }
        // RVA: 0x0B2C8B80  token: 0x6000560
        public System.Boolean Allocate(UnityEngine.Vector4& result, System.Int32 width, System.Int32 height) { }
        // RVA: 0x0B2C8BDC  token: 0x6000561
        public System.Void Reset() { }

    }

    // TypeToken: 0x2000108  // size: 0x48
    public class Texture2DAtlas
    {
        // Fields
        private static System.Int32 kGPUTexInvalid;  // const
        private static System.Int32 kGPUTexValidMip0;  // const
        private static System.Int32 kGPUTexValidMipAll;  // const
        private UnityEngine.Rendering.RTHandle m_AtlasTexture;  // 0x10
        private System.Int32 m_Width;  // 0x18
        private System.Int32 m_Height;  // 0x1c
        private UnityEngine.Experimental.Rendering.GraphicsFormat m_Format;  // 0x20
        private System.Boolean m_UseMipMaps;  // 0x24
        private System.Boolean m_IsAtlasTextureOwner;  // 0x25
        private UnityEngine.Rendering.AtlasAllocator m_AtlasAllocator;  // 0x28
        private System.Collections.Generic.Dictionary<System.Int32,System.ValueTuple<UnityEngine.Vector4,UnityEngine.Vector2Int>> m_AllocationCache;  // 0x30
        private System.Collections.Generic.Dictionary<System.Int32,System.Int32> m_IsGPUTextureUpToDate;  // 0x38
        private System.Collections.Generic.Dictionary<System.Int32,System.Int32> m_TextureHashes;  // 0x40
        private static readonly UnityEngine.Vector4 fullScaleOffset;  // static @ 0x0
        private static readonly System.Int32 s_MaxMipLevelPadding;  // static @ 0x10

        // Properties
        System.Int32 maxMipLevelPadding { get; /* RVA: 0x0B2D7398 */ }
        UnityEngine.Rendering.RTHandle AtlasTexture { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x0B2D70D0  token: 0x600056B
        public System.Void .ctor(System.Int32 width, System.Int32 height, UnityEngine.Experimental.Rendering.GraphicsFormat format, UnityEngine.FilterMode filterMode, System.Boolean powerOfTwoPadding, System.String name, System.Boolean useMipMap) { }
        // RVA: 0x0B2D6DAC  token: 0x600056C
        public System.Void Release() { }
        // RVA: 0x0B2D6DE4  token: 0x600056D
        public System.Void ResetAllocator() { }
        // RVA: 0x0B2D5FD0  token: 0x600056E
        public System.Void ClearTarget(UnityEngine.Rendering.CommandBuffer cmd) { }
        // RVA: 0x0B2D6428  token: 0x600056F
        private System.Int32 GetTextureMipmapCount(System.Int32 width, System.Int32 height) { }
        // RVA: 0x0B2D6460  token: 0x6000570
        private System.Boolean Is2D(UnityEngine.Texture texture) { }
        // RVA: 0x0B2D65D0  token: 0x6000571
        private System.Boolean IsSingleChannelBlit(UnityEngine.Texture source, UnityEngine.Texture destination) { }
        // RVA: 0x0B2D5AAC  token: 0x6000572
        private System.Void Blit2DTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4 scaleOffset, UnityEngine.Texture texture, UnityEngine.Vector4 sourceScaleOffset, System.Boolean blitMips, UnityEngine.Rendering.Texture2DAtlas.BlitType blitType) { }
        // RVA: 0x0B2D67FC  token: 0x6000573
        private System.Void MarkGPUTextureValid(System.Int32 instanceId, System.Boolean mipAreValid) { }
        // RVA: 0x0B2D67A4  token: 0x6000574
        private System.Void MarkGPUTextureInvalid(System.Int32 instanceId) { }
        // RVA: 0x0B2D5E78  token: 0x6000575
        public virtual System.Void BlitTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4 scaleOffset, UnityEngine.Texture texture, UnityEngine.Vector4 sourceScaleOffset, System.Boolean blitMips, System.Int32 overrideInstanceID) { }
        // RVA: 0x0B2D5E20  token: 0x6000576
        public virtual System.Void BlitOctahedralTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4 scaleOffset, UnityEngine.Texture texture, UnityEngine.Vector4 sourceScaleOffset, System.Boolean blitMips, System.Int32 overrideInstanceID) { }
        // RVA: 0x0B2D5CC0  token: 0x6000577
        public virtual System.Void BlitCubeTexture2D(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4 scaleOffset, UnityEngine.Texture texture, System.Boolean blitMips, System.Int32 overrideInstanceID) { }
        // RVA: 0x0B2D58F4  token: 0x6000578
        public virtual System.Boolean AllocateTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4& scaleOffset, UnityEngine.Texture texture, System.Int32 width, System.Int32 height, System.Int32 overrideInstanceID) { }
        // RVA: 0x0B2D5894  token: 0x6000579
        public System.Boolean AllocateTextureWithoutBlit(UnityEngine.Texture texture, System.Int32 width, System.Int32 height, UnityEngine.Vector4& scaleOffset) { }
        // RVA: 0x0B2D5708  token: 0x600057A
        public virtual System.Boolean AllocateTextureWithoutBlit(System.Int32 instanceId, System.Int32 width, System.Int32 height, UnityEngine.Vector4& scaleOffset) { }
        // RVA: 0x0B2D6364  token: 0x600057B
        private System.Int32 GetTextureHash(UnityEngine.Texture textureA, UnityEngine.Texture textureB) { }
        // RVA: 0x0B2D6408  token: 0x600057C
        public System.Int32 GetTextureID(UnityEngine.Texture texture) { }
        // RVA: 0x0B2D63C4  token: 0x600057D
        public System.Int32 GetTextureID(UnityEngine.Texture textureA, UnityEngine.Texture textureB) { }
        // RVA: 0x0B2D64D4  token: 0x600057E
        public System.Boolean IsCached(UnityEngine.Vector4& scaleOffset, UnityEngine.Texture textureA, UnityEngine.Texture textureB) { }
        // RVA: 0x0B2D6594  token: 0x600057F
        public System.Boolean IsCached(UnityEngine.Vector4& scaleOffset, UnityEngine.Texture texture) { }
        // RVA: 0x0B2D6514  token: 0x6000580
        public System.Boolean IsCached(UnityEngine.Vector4& scaleOffset, System.Int32 id) { }
        // RVA: 0x0B2D62F8  token: 0x6000581
        private UnityEngine.Vector2Int GetCachedTextureSize(System.Int32 id) { }
        // RVA: 0x0B2D6BB4  token: 0x6000582
        public virtual System.Boolean NeedsUpdate(UnityEngine.Texture texture, System.Boolean needMips) { }
        // RVA: 0x0B2D6860  token: 0x6000583
        public virtual System.Boolean NeedsUpdate(UnityEngine.Texture textureA, UnityEngine.Texture textureB, System.Boolean needMips) { }
        // RVA: 0x0B2D5670  token: 0x6000584
        public virtual System.Boolean AddTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4& scaleOffset, UnityEngine.Texture texture) { }
        // RVA: 0x0B2D6E5C  token: 0x6000585
        public virtual System.Boolean UpdateTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture oldTexture, UnityEngine.Texture newTexture, UnityEngine.Vector4& scaleOffset, UnityEngine.Vector4 sourceScaleOffset, System.Boolean updateIfNeeded, System.Boolean blitMips) { }
        // RVA: 0x0B2D6FC8  token: 0x6000586
        public virtual System.Boolean UpdateTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture texture, UnityEngine.Vector4& scaleOffset, System.Boolean updateIfNeeded, System.Boolean blitMips) { }
        // RVA: 0x0B2D615C  token: 0x6000587
        private System.Boolean EnsureTextureSlot(System.Boolean& isUploadNeeded, UnityEngine.Vector4& scaleBias, System.Int32 key, System.Int32 width, System.Int32 height) { }
        // RVA: 0x0B2D707C  token: 0x6000588
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200010A  // size: 0x30
    public class AtlasAllocatorDynamic
    {
        // Fields
        private System.Int32 m_Width;  // 0x10
        private System.Int32 m_Height;  // 0x14
        private UnityEngine.Rendering.AtlasAllocatorDynamic.AtlasNodePool m_Pool;  // 0x18
        private System.Int16 m_Root;  // 0x20
        private System.Collections.Generic.Dictionary<System.Int32,System.Int16> m_NodeFromID;  // 0x28

        // Methods
        // RVA: 0x0B2C8A3C  token: 0x6000589
        public System.Void .ctor(System.Int32 width, System.Int32 height, System.Int32 capacityAllocations) { }
        // RVA: 0x0B2C8354  token: 0x600058A
        public System.Boolean Allocate(UnityEngine.Vector4& result, System.Int32 key, System.Int32 width, System.Int32 height) { }
        // RVA: 0x0B2C88C0  token: 0x600058B
        public System.Void Release(System.Int32 key) { }
        // RVA: 0x0B2C8988  token: 0x600058C
        public System.Void Release() { }
        // RVA: 0x0B2C8858  token: 0x600058D
        public System.String DebugStringFromRoot(System.Int32 depthMax) { }
        // RVA: 0x0B2C8458  token: 0x600058E
        private System.Void DebugStringFromNode(System.String& res, System.Int16 n, System.Int32 depthCurrent, System.Int32 depthMax) { }

    }

    // TypeToken: 0x200010E  // size: 0x38
    public class Texture2DAtlasDynamic
    {
        // Fields
        private UnityEngine.Rendering.RTHandle m_AtlasTexture;  // 0x10
        private System.Boolean isAtlasTextureOwner;  // 0x18
        private System.Int32 m_Width;  // 0x1c
        private System.Int32 m_Height;  // 0x20
        private UnityEngine.Experimental.Rendering.GraphicsFormat m_Format;  // 0x24
        private UnityEngine.Rendering.AtlasAllocatorDynamic m_AtlasAllocator;  // 0x28
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Vector4> m_AllocationCache;  // 0x30

        // Properties
        UnityEngine.Rendering.RTHandle AtlasTexture { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x0B2D54C4  token: 0x600059E
        public System.Void .ctor(System.Int32 width, System.Int32 height, System.Int32 capacity, UnityEngine.Experimental.Rendering.GraphicsFormat format) { }
        // RVA: 0x0B2D53A4  token: 0x600059F
        public System.Void .ctor(System.Int32 width, System.Int32 height, System.Int32 capacity, UnityEngine.Rendering.RTHandle atlasTexture) { }
        // RVA: 0x0B2D5314  token: 0x60005A0
        public System.Void Release() { }
        // RVA: 0x0B2D534C  token: 0x60005A1
        public System.Void ResetAllocator() { }
        // RVA: 0x0B2D4E6C  token: 0x60005A2
        public System.Boolean AddTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4& scaleOffset, UnityEngine.Texture texture) { }
        // RVA: 0x0B2D5248  token: 0x60005A3
        public System.Boolean IsCached(UnityEngine.Vector4& scaleOffset, System.Int32 key) { }
        // RVA: 0x0B2D50E4  token: 0x60005A4
        public System.Boolean EnsureTextureSlot(System.Boolean& isUploadNeeded, UnityEngine.Vector4& scaleOffset, System.Int32 key, System.Int32 width, System.Int32 height) { }
        // RVA: 0x0B2D52AC  token: 0x60005A5
        public System.Void ReleaseTextureSlot(System.Int32 key) { }

    }

    // TypeToken: 0x200010F  // size: 0x10
    public static class TextureXR
    {
        // Fields
        private static System.Int32 m_MaxViews;  // static @ 0x0
        private static UnityEngine.Texture m_BlackUIntTexture2DArray;  // static @ 0x8
        private static UnityEngine.Texture m_BlackUIntTexture;  // static @ 0x10
        private static UnityEngine.Rendering.RTHandle m_BlackUIntTexture2DArrayRTH;  // static @ 0x18
        private static UnityEngine.Rendering.RTHandle m_BlackUIntTextureRTH;  // static @ 0x20
        private static UnityEngine.Texture2DArray m_ClearTexture2DArray;  // static @ 0x28
        private static UnityEngine.Texture2D m_ClearTexture;  // static @ 0x30
        private static UnityEngine.Rendering.RTHandle m_ClearTexture2DArrayRTH;  // static @ 0x38
        private static UnityEngine.Rendering.RTHandle m_ClearTextureRTH;  // static @ 0x40
        private static UnityEngine.Texture2DArray m_MagentaTexture2DArray;  // static @ 0x48
        private static UnityEngine.Texture2D m_MagentaTexture;  // static @ 0x50
        private static UnityEngine.Rendering.RTHandle m_MagentaTexture2DArrayRTH;  // static @ 0x58
        private static UnityEngine.Rendering.RTHandle m_MagentaTextureRTH;  // static @ 0x60
        private static UnityEngine.Texture2D m_BlackTexture;  // static @ 0x68
        private static UnityEngine.Texture3D m_BlackTexture3D;  // static @ 0x70
        private static UnityEngine.Texture2DArray m_BlackTexture2DArray;  // static @ 0x78
        private static UnityEngine.Rendering.RTHandle m_BlackTexture2DArrayRTH;  // static @ 0x80
        private static UnityEngine.Rendering.RTHandle m_BlackTextureRTH;  // static @ 0x88
        private static UnityEngine.Rendering.RTHandle m_BlackTexture3DRTH;  // static @ 0x90
        private static UnityEngine.Texture2DArray m_WhiteTexture2DArray;  // static @ 0x98
        private static UnityEngine.Rendering.RTHandle m_WhiteTexture2DArrayRTH;  // static @ 0xa0
        private static UnityEngine.Rendering.RTHandle m_WhiteTextureRTH;  // static @ 0xa8

        // Properties
        System.Int32 maxViews { set; /* RVA: 0x0B2D89BC */ }
        System.Int32 slices { get; /* RVA: 0x0B2D896C */ }
        System.Boolean useTexArray { get; /* RVA: 0x04775CF0 */ }
        UnityEngine.Rendering.TextureDimension dimension { get; /* RVA: 0x0B2D8918 */ }

        // Methods
        // RVA: 0x0B2D7AFC  token: 0x60005AA
        public static UnityEngine.Rendering.RTHandle GetBlackUIntTexture() { }
        // RVA: 0x0B2D7B70  token: 0x60005AB
        public static UnityEngine.Rendering.RTHandle GetClearTexture() { }
        // RVA: 0x0B2D7BE4  token: 0x60005AC
        public static UnityEngine.Rendering.RTHandle GetMagentaTexture() { }
        // RVA: 0x0B2D7A80  token: 0x60005AD
        public static UnityEngine.Rendering.RTHandle GetBlackTexture() { }
        // RVA: 0x0B2D7A2C  token: 0x60005AE
        public static UnityEngine.Rendering.RTHandle GetBlackTextureArray() { }
        // RVA: 0x0B2D79D8  token: 0x60005AF
        public static UnityEngine.Rendering.RTHandle GetBlackTexture3D() { }
        // RVA: 0x0B2D7C58  token: 0x60005B0
        public static UnityEngine.Rendering.RTHandle GetWhiteTexture() { }
        // RVA: 0x0B2D7CD4  token: 0x60005B1
        public static System.Void Initialize(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.ComputeShader clearR32_UIntShader) { }
        // RVA: 0x0B2D7870  token: 0x60005B2
        private static UnityEngine.Texture2DArray CreateTexture2DArrayFromTexture2D(UnityEngine.Texture2D source, System.String name) { }
        // RVA: 0x0B2D74B8  token: 0x60005B3
        private static UnityEngine.Texture CreateBlackUIntTextureArray(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.ComputeShader clearR32_UIntShader) { }
        // RVA: 0x0B2D7694  token: 0x60005B4
        private static UnityEngine.Texture CreateBlackUintTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.ComputeShader clearR32_UIntShader) { }
        // RVA: 0x0B2D73E8  token: 0x60005B5
        private static UnityEngine.Texture3D CreateBlackTexture3D(System.String name) { }
        // RVA: 0x04D79990  token: 0x60005B6
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000110  // size: 0x10
    public static class ArrayExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x60005B7
        public static System.Void ResizeArray(Unity.Collections.NativeArray<T>& array, System.Int32 capacity, Unity.Collections.Allocator allocator) { }
        // RVA: 0x0B2C82A0  token: 0x60005B8
        public static System.Void ResizeArray(UnityEngine.Jobs.TransformAccessArray& array, System.Int32 capacity) { }
        // RVA: -1  // generic def  token: 0x60005B9
        public static System.Void ResizeArray(T[]& array, System.Int32 capacity) { }
        // RVA: -1  // generic def  token: 0x60005BA
        public static System.Void Grow(T[]& array, System.Int32 targetSize) { }
        // RVA: -1  // generic def  token: 0x60005BB
        public static System.Void GrowToNearestPowerOf2(T[]& array, System.Int32 targetSize) { }
        // RVA: 0x0B2C832C  token: 0x60005BC
        public static System.UInt32 RoundUpToPowerOf2(System.UInt32 n) { }

    }

    // TypeToken: 0x2000111
    public interface IBitArray
    {
        // Properties
        System.UInt32 capacity { get; /* RVA: -1  // abstract */ }
        System.Boolean allFalse { get; /* RVA: -1  // abstract */ }
        System.Boolean allTrue { get; /* RVA: -1  // abstract */ }
        System.Boolean Item { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.String humanizedData { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60005C3
        public virtual UnityEngine.Rendering.IBitArray BitAnd(UnityEngine.Rendering.IBitArray other) { }
        // RVA: -1  // abstract  token: 0x60005C4
        public virtual UnityEngine.Rendering.IBitArray BitOr(UnityEngine.Rendering.IBitArray other) { }
        // RVA: -1  // abstract  token: 0x60005C5
        public virtual UnityEngine.Rendering.IBitArray BitNot() { }

    }

    // TypeToken: 0x2000112  // size: 0x11
    public sealed struct BitArray8 : UnityEngine.Rendering.IBitArray
    {
        // Fields
        private System.Byte data;  // 0x10

        // Properties
        System.UInt32 capacity { get; /* RVA: 0x04D8D060 */ }
        System.Boolean allFalse { get; /* RVA: 0x0961F480 */ }
        System.Boolean allTrue { get; /* RVA: 0x0B2CA01C */ }
        System.String humanizedData { get; /* RVA: 0x0B2CA024 */ }
        System.Boolean Item { get; /* RVA: 0x0B2CA000 */ set; /* RVA: 0x0B2CA0E8 */ }

        // Methods
        // RVA: 0x04D87180  token: 0x60005CC
        public System.Void .ctor(System.Byte initValue) { }
        // RVA: 0x0B2C9F70  token: 0x60005CD
        public System.Void .ctor(System.Collections.Generic.IEnumerable<System.UInt32> bitIndexTrue) { }
        // RVA: 0x04DB8DD0  token: 0x60005CE
        public static UnityEngine.Rendering.BitArray8 op_OnesComplement(UnityEngine.Rendering.BitArray8 a) { }
        // RVA: 0x04DB8DC0  token: 0x60005CF
        public static UnityEngine.Rendering.BitArray8 op_BitwiseOr(UnityEngine.Rendering.BitArray8 a, UnityEngine.Rendering.BitArray8 b) { }
        // RVA: 0x04DB8DB0  token: 0x60005D0
        public static UnityEngine.Rendering.BitArray8 op_BitwiseAnd(UnityEngine.Rendering.BitArray8 a, UnityEngine.Rendering.BitArray8 b) { }
        // RVA: 0x0B2C9E08  token: 0x60005D1
        public virtual UnityEngine.Rendering.IBitArray BitAnd(UnityEngine.Rendering.IBitArray other) { }
        // RVA: 0x0B2C9EB4  token: 0x60005D2
        public virtual UnityEngine.Rendering.IBitArray BitOr(UnityEngine.Rendering.IBitArray other) { }
        // RVA: 0x0B2C9E6C  token: 0x60005D3
        public virtual UnityEngine.Rendering.IBitArray BitNot() { }
        // RVA: 0x066FF158  token: 0x60005D4
        public static System.Boolean op_Equality(UnityEngine.Rendering.BitArray8 a, UnityEngine.Rendering.BitArray8 b) { }
        // RVA: 0x066FF160  token: 0x60005D5
        public static System.Boolean op_Inequality(UnityEngine.Rendering.BitArray8 a, UnityEngine.Rendering.BitArray8 b) { }
        // RVA: 0x0B2C9F14  token: 0x60005D6
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x04DB8DA0  token: 0x60005D7
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000113  // size: 0x12
    public sealed struct BitArray16 : UnityEngine.Rendering.IBitArray
    {
        // Fields
        private System.UInt16 data;  // 0x10

        // Properties
        System.UInt32 capacity { get; /* RVA: 0x04DA43A0 */ }
        System.Boolean allFalse { get; /* RVA: 0x0B160F5C */ }
        System.Boolean allTrue { get; /* RVA: 0x0B2C9CCC */ }
        System.String humanizedData { get; /* RVA: 0x0B2C9CD8 */ }
        System.Boolean Item { get; /* RVA: 0x05396E28 */ set; /* RVA: 0x0539559C */ }

        // Methods
        // RVA: 0x04D87160  token: 0x60005DE
        public System.Void .ctor(System.UInt16 initValue) { }
        // RVA: 0x0B2C9C38  token: 0x60005DF
        public System.Void .ctor(System.Collections.Generic.IEnumerable<System.UInt32> bitIndexTrue) { }
        // RVA: 0x04DB8D90  token: 0x60005E0
        public static UnityEngine.Rendering.BitArray16 op_OnesComplement(UnityEngine.Rendering.BitArray16 a) { }
        // RVA: 0x04DB8D80  token: 0x60005E1
        public static UnityEngine.Rendering.BitArray16 op_BitwiseOr(UnityEngine.Rendering.BitArray16 a, UnityEngine.Rendering.BitArray16 b) { }
        // RVA: 0x04DB8D70  token: 0x60005E2
        public static UnityEngine.Rendering.BitArray16 op_BitwiseAnd(UnityEngine.Rendering.BitArray16 a, UnityEngine.Rendering.BitArray16 b) { }
        // RVA: 0x0B2C9AC4  token: 0x60005E3
        public virtual UnityEngine.Rendering.IBitArray BitAnd(UnityEngine.Rendering.IBitArray other) { }
        // RVA: 0x0B2C9B74  token: 0x60005E4
        public virtual UnityEngine.Rendering.IBitArray BitOr(UnityEngine.Rendering.IBitArray other) { }
        // RVA: 0x0B2C9B2C  token: 0x60005E5
        public virtual UnityEngine.Rendering.IBitArray BitNot() { }
        // RVA: 0x0B290008  token: 0x60005E6
        public static System.Boolean op_Equality(UnityEngine.Rendering.BitArray16 a, UnityEngine.Rendering.BitArray16 b) { }
        // RVA: 0x0B2C9E00  token: 0x60005E7
        public static System.Boolean op_Inequality(UnityEngine.Rendering.BitArray16 a, UnityEngine.Rendering.BitArray16 b) { }
        // RVA: 0x0B2C9BD8  token: 0x60005E8
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x04DB8D60  token: 0x60005E9
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000114  // size: 0x14
    public sealed struct BitArray32 : UnityEngine.Rendering.IBitArray
    {
        // Fields
        private System.UInt32 data;  // 0x10

        // Properties
        System.UInt32 capacity { get; /* RVA: 0x04DA43E0 */ }
        System.Boolean allFalse { get; /* RVA: 0x0426F6A0 */ }
        System.Boolean allTrue { get; /* RVA: 0x0B2D9B6C */ }
        System.String humanizedVersion { get; /* RVA: 0x0B2D9C9C */ }
        System.String humanizedData { get; /* RVA: 0x0B2D9B74 */ }
        System.Boolean Item { get; /* RVA: 0x0B2D9B54 */ set; /* RVA: 0x0B2D9CF0 */ }

        // Methods
        // RVA: 0x04D86140  token: 0x60005F1
        public System.Void .ctor(System.UInt32 initValue) { }
        // RVA: 0x0B2D9AC4  token: 0x60005F2
        public System.Void .ctor(System.Collections.Generic.IEnumerable<System.UInt32> bitIndexTrue) { }
        // RVA: 0x0B2D9960  token: 0x60005F3
        public virtual UnityEngine.Rendering.IBitArray BitAnd(UnityEngine.Rendering.IBitArray other) { }
        // RVA: 0x0B2D9A08  token: 0x60005F4
        public virtual UnityEngine.Rendering.IBitArray BitOr(UnityEngine.Rendering.IBitArray other) { }
        // RVA: 0x0B2D99C4  token: 0x60005F5
        public virtual UnityEngine.Rendering.IBitArray BitNot() { }
        // RVA: 0x04DB8FF0  token: 0x60005F6
        public static UnityEngine.Rendering.BitArray32 op_OnesComplement(UnityEngine.Rendering.BitArray32 a) { }
        // RVA: 0x04DB8FE0  token: 0x60005F7
        public static UnityEngine.Rendering.BitArray32 op_BitwiseOr(UnityEngine.Rendering.BitArray32 a, UnityEngine.Rendering.BitArray32 b) { }
        // RVA: 0x04DA5940  token: 0x60005F8
        public static UnityEngine.Rendering.BitArray32 op_BitwiseAnd(UnityEngine.Rendering.BitArray32 a, UnityEngine.Rendering.BitArray32 b) { }
        // RVA: 0x07872540  token: 0x60005F9
        public static System.Boolean op_Equality(UnityEngine.Rendering.BitArray32 a, UnityEngine.Rendering.BitArray32 b) { }
        // RVA: 0x0AAED910  token: 0x60005FA
        public static System.Boolean op_Inequality(UnityEngine.Rendering.BitArray32 a, UnityEngine.Rendering.BitArray32 b) { }
        // RVA: 0x0B2D9A68  token: 0x60005FB
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x04DB8FD0  token: 0x60005FC
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000115  // size: 0x18
    public sealed struct BitArray64 : UnityEngine.Rendering.IBitArray
    {
        // Fields
        private System.UInt64 data;  // 0x10

        // Properties
        System.UInt32 capacity { get; /* RVA: 0x04DB9010 */ }
        System.Boolean allFalse { get; /* RVA: 0x0314CD60 */ }
        System.Boolean allTrue { get; /* RVA: 0x0B2D9F20 */ }
        System.String humanizedData { get; /* RVA: 0x0B2D9F28 */ }
        System.Boolean Item { get; /* RVA: 0x0B2D9F08 */ set; /* RVA: 0x0B2DA050 */ }

        // Methods
        // RVA: 0x04D88B60  token: 0x6000603
        public System.Void .ctor(System.UInt64 initValue) { }
        // RVA: 0x0B2D9E74  token: 0x6000604
        public System.Void .ctor(System.Collections.Generic.IEnumerable<System.UInt32> bitIndexTrue) { }
        // RVA: 0x04DB9040  token: 0x6000605
        public static UnityEngine.Rendering.BitArray64 op_OnesComplement(UnityEngine.Rendering.BitArray64 a) { }
        // RVA: 0x04DB9030  token: 0x6000606
        public static UnityEngine.Rendering.BitArray64 op_BitwiseOr(UnityEngine.Rendering.BitArray64 a, UnityEngine.Rendering.BitArray64 b) { }
        // RVA: 0x04DB9020  token: 0x6000607
        public static UnityEngine.Rendering.BitArray64 op_BitwiseAnd(UnityEngine.Rendering.BitArray64 a, UnityEngine.Rendering.BitArray64 b) { }
        // RVA: 0x0B2D9D00  token: 0x6000608
        public virtual UnityEngine.Rendering.IBitArray BitAnd(UnityEngine.Rendering.IBitArray other) { }
        // RVA: 0x0B2D9DB0  token: 0x6000609
        public virtual UnityEngine.Rendering.IBitArray BitOr(UnityEngine.Rendering.IBitArray other) { }
        // RVA: 0x0B2D9D68  token: 0x600060A
        public virtual UnityEngine.Rendering.IBitArray BitNot() { }
        // RVA: 0x02FB1840  token: 0x600060B
        public static System.Boolean op_Equality(UnityEngine.Rendering.BitArray64 a, UnityEngine.Rendering.BitArray64 b) { }
        // RVA: 0x02F019D0  token: 0x600060C
        public static System.Boolean op_Inequality(UnityEngine.Rendering.BitArray64 a, UnityEngine.Rendering.BitArray64 b) { }
        // RVA: 0x0B2D9E14  token: 0x600060D
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x04DB9000  token: 0x600060E
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000116  // size: 0x20
    public sealed struct BitArray128 : UnityEngine.Rendering.IBitArray
    {
        // Fields
        private System.UInt64 data1;  // 0x10
        private System.UInt64 data2;  // 0x18

        // Properties
        System.UInt64 Data1 { get; /* RVA: 0x02B76770 */ }
        System.UInt64 Data2 { get; /* RVA: 0x04D88320 */ }
        System.UInt32 capacity { get; /* RVA: 0x04DB8E50 */ }
        System.Boolean allFalse { get; /* RVA: 0x0B2D9000 */ }
        System.Boolean allTrue { get; /* RVA: 0x0B2D9014 */ }
        System.String humanizedData { get; /* RVA: 0x0B2D9028 */ }
        System.Boolean Item { get; /* RVA: 0x05392684 */ set; /* RVA: 0x05392698 */ }

        // Methods
        // RVA: 0x04D861B0  token: 0x6000617
        public System.Void .ctor(System.UInt64 initValue1, System.UInt64 initValue2) { }
        // RVA: 0x037DB200  token: 0x6000618
        public System.Void .ctor(System.Collections.Generic.IEnumerable<System.UInt32> bitIndexTrue) { }
        // RVA: 0x04DB8EA0  token: 0x6000619
        public static UnityEngine.Rendering.BitArray128 op_OnesComplement(UnityEngine.Rendering.BitArray128 a) { }
        // RVA: 0x04DB8E80  token: 0x600061A
        public static UnityEngine.Rendering.BitArray128 op_BitwiseOr(UnityEngine.Rendering.BitArray128 a, UnityEngine.Rendering.BitArray128 b) { }
        // RVA: 0x04DB8E60  token: 0x600061B
        public static UnityEngine.Rendering.BitArray128 op_BitwiseAnd(UnityEngine.Rendering.BitArray128 a, UnityEngine.Rendering.BitArray128 b) { }
        // RVA: 0x0B2D8E00  token: 0x600061C
        public virtual UnityEngine.Rendering.IBitArray BitAnd(UnityEngine.Rendering.IBitArray other) { }
        // RVA: 0x0B2D8EF4  token: 0x600061D
        public virtual UnityEngine.Rendering.IBitArray BitOr(UnityEngine.Rendering.IBitArray other) { }
        // RVA: 0x0B2D8E94  token: 0x600061E
        public virtual UnityEngine.Rendering.IBitArray BitNot() { }
        // RVA: 0x067C4D3C  token: 0x600061F
        public static System.Boolean op_Equality(UnityEngine.Rendering.BitArray128 a, UnityEngine.Rendering.BitArray128 b) { }
        // RVA: 0x067C4D54  token: 0x6000620
        public static System.Boolean op_Inequality(UnityEngine.Rendering.BitArray128 a, UnityEngine.Rendering.BitArray128 b) { }
        // RVA: 0x0B2D8F88  token: 0x6000621
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x04DB8E20  token: 0x6000622
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000117  // size: 0x30
    public sealed struct BitArray256 : UnityEngine.Rendering.IBitArray
    {
        // Fields
        private System.UInt64 data1;  // 0x10
        private System.UInt64 data2;  // 0x18
        private System.UInt64 data3;  // 0x20
        private System.UInt64 data4;  // 0x28

        // Properties
        System.UInt32 capacity { get; /* RVA: 0x04DB8F10 */ }
        System.Boolean allFalse { get; /* RVA: 0x0B2D95A8 */ }
        System.Boolean allTrue { get; /* RVA: 0x0B2D95C8 */ }
        System.String humanizedData { get; /* RVA: 0x0B2D95E8 */ }
        System.Boolean Item { get; /* RVA: 0x0B2D9574 */ set; /* RVA: 0x0B2D9924 */ }

        // Methods
        // RVA: 0x04D861C0  token: 0x6000629
        public System.Void .ctor(System.UInt64 initValue1, System.UInt64 initValue2, System.UInt64 initValue3, System.UInt64 initValue4) { }
        // RVA: 0x0B2D9480  token: 0x600062A
        public System.Void .ctor(System.Collections.Generic.IEnumerable<System.UInt32> bitIndexTrue) { }
        // RVA: 0x04DB8FA0  token: 0x600062B
        public static UnityEngine.Rendering.BitArray256 op_OnesComplement(UnityEngine.Rendering.BitArray256 a) { }
        // RVA: 0x04DB8F60  token: 0x600062C
        public static UnityEngine.Rendering.BitArray256 op_BitwiseOr(UnityEngine.Rendering.BitArray256 a, UnityEngine.Rendering.BitArray256 b) { }
        // RVA: 0x04DB8F20  token: 0x600062D
        public static UnityEngine.Rendering.BitArray256 op_BitwiseAnd(UnityEngine.Rendering.BitArray256 a, UnityEngine.Rendering.BitArray256 b) { }
        // RVA: 0x0B2D91E8  token: 0x600062E
        public virtual UnityEngine.Rendering.IBitArray BitAnd(UnityEngine.Rendering.IBitArray other) { }
        // RVA: 0x0B2D9318  token: 0x600062F
        public virtual UnityEngine.Rendering.IBitArray BitOr(UnityEngine.Rendering.IBitArray other) { }
        // RVA: 0x0B2D92A8  token: 0x6000630
        public virtual UnityEngine.Rendering.IBitArray BitNot() { }
        // RVA: 0x0B2D98B4  token: 0x6000631
        public static System.Boolean op_Equality(UnityEngine.Rendering.BitArray256 a, UnityEngine.Rendering.BitArray256 b) { }
        // RVA: 0x0B2D98EC  token: 0x6000632
        public static System.Boolean op_Inequality(UnityEngine.Rendering.BitArray256 a, UnityEngine.Rendering.BitArray256 b) { }
        // RVA: 0x0B2D93D8  token: 0x6000633
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x04DB8EC0  token: 0x6000634
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000118  // size: 0x10
    public static class BitArrayUtilities
    {
        // Methods
        // RVA: 0x0B2DA0B4  token: 0x6000635
        public static System.Boolean Get8(System.UInt32 index, System.Byte data) { }
        // RVA: 0x0489D0A0  token: 0x6000636
        public static System.Boolean Get16(System.UInt32 index, System.UInt16 data) { }
        // RVA: 0x0690DAE4  token: 0x6000637
        public static System.Boolean Get32(System.UInt32 index, System.UInt32 data) { }
        // RVA: 0x0B2DA0A0  token: 0x6000638
        public static System.Boolean Get64(System.UInt32 index, System.UInt64 data) { }
        // RVA: 0x03A3D9D0  token: 0x6000639
        public static System.Boolean Get128(System.UInt32 index, System.UInt64 data1, System.UInt64 data2) { }
        // RVA: 0x0B2DA060  token: 0x600063A
        public static System.Boolean Get256(System.UInt32 index, System.UInt64 data1, System.UInt64 data2, System.UInt64 data3, System.UInt64 data4) { }
        // RVA: 0x0B2DA168  token: 0x600063B
        public static System.Void Set8(System.UInt32 index, System.Byte& data, System.Boolean value) { }
        // RVA: 0x04489110  token: 0x600063C
        public static System.Void Set16(System.UInt32 index, System.UInt16& data, System.Boolean value) { }
        // RVA: 0x0B2DA134  token: 0x600063D
        public static System.Void Set32(System.UInt32 index, System.UInt32& data, System.Boolean value) { }
        // RVA: 0x0B2DA14C  token: 0x600063E
        public static System.Void Set64(System.UInt32 index, System.UInt64& data, System.Boolean value) { }
        // RVA: 0x03B201E0  token: 0x600063F
        public static System.Void Set128(System.UInt32 index, System.UInt64& data1, System.UInt64& data2, System.Boolean value) { }
        // RVA: 0x0B2DA0CC  token: 0x6000640
        public static System.Void Set256(System.UInt32 index, System.UInt64& data1, System.UInt64& data2, System.UInt64& data3, System.UInt64& data4, System.Boolean value) { }

    }

    // TypeToken: 0x2000119  // size: 0x10
    public static class Blitter
    {
        // Fields
        private static UnityEngine.Material s_Blit;  // static @ 0x0
        private static UnityEngine.Material s_BlitTexArray;  // static @ 0x8
        private static UnityEngine.Material s_BlitTexArraySingleSlice;  // static @ 0x10
        private static UnityEngine.Material s_BlitMotionVector;  // static @ 0x18
        private static UnityEngine.Material s_BlitColorAndDepth;  // static @ 0x20
        private static UnityEngine.Material s_LowResBlit;  // static @ 0x28
        private static UnityEngine.MaterialPropertyBlock s_PropertyBlock;  // static @ 0x30
        private static UnityEngine.Mesh s_TriangleMesh;  // static @ 0x38
        private static UnityEngine.Mesh s_QuadMesh;  // static @ 0x40

        // Methods
        // RVA: 0x04775980  token: 0x6000641
        public static System.Void Initialize(UnityEngine.Shader blitPS, UnityEngine.Shader blitColorAndDepthPS, UnityEngine.Shader blitMotionVectorPS, UnityEngine.Shader lowResBlitPS) { }
        // RVA: 0x037DC4E0  token: 0x6000642
        public static System.Void Cleanup() { }
        // RVA: 0x03D24420  token: 0x6000643
        public static UnityEngine.Material GetBlitMaterial(System.Boolean isFP32Output, UnityEngine.Rendering.TextureDimension dimension, System.Boolean singleSlice) { }
        // RVA: 0x0B2DC374  token: 0x6000644
        public static UnityEngine.Material GetBlitColorAndDepth(System.Boolean isFP32Output) { }
        // RVA: 0x0B2DC3C4  token: 0x6000645
        public static UnityEngine.Material GetBlitMotionVectorMaterial() { }
        // RVA: 0x0B2DC414  token: 0x6000646
        public static UnityEngine.Material GetLowResBlitMaterial() { }
        // RVA: 0x0B2DC1AC  token: 0x6000647
        private static System.Void DrawTriangle(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Material material, System.Int32 shaderPass) { }
        // RVA: 0x0B2DBFE0  token: 0x6000648
        private static System.Void DrawQuad(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Material material, System.Int32 shaderPass) { }
        // RVA: 0x0B2DBEB8  token: 0x6000649
        public static System.Void BlitTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle source, UnityEngine.Vector4 scaleBias, System.Single mipLevel, System.Boolean bilinear) { }
        // RVA: 0x0B2DBC84  token: 0x600064A
        public static System.Void BlitTexture2D(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle source, UnityEngine.Vector4 scaleBias, System.Single mipLevel, System.Boolean bilinear) { }
        // RVA: 0x0B2DBD98  token: 0x600064B
        public static System.Void BlitTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle source, UnityEngine.Vector4 scaleBias, UnityEngine.Material material, System.Int32 pass) { }
        // RVA: 0x0B2DA4A0  token: 0x600064C
        public static System.Void BlitCameraTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle source, UnityEngine.Rendering.RTHandle destination, System.Single mipLevel, System.Boolean bilinear) { }
        // RVA: 0x0B2DA2FC  token: 0x600064D
        public static System.Void BlitCameraTexture2D(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle source, UnityEngine.Rendering.RTHandle destination, System.Single mipLevel, System.Boolean bilinear) { }
        // RVA: 0x0B2DA8C8  token: 0x600064E
        public static System.Void BlitCameraTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle source, UnityEngine.Rendering.RTHandle destination, UnityEngine.Material material, System.Int32 pass) { }
        // RVA: 0x0B2DA7FC  token: 0x600064F
        public static System.Void BlitCameraTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle source, UnityEngine.Rendering.RTHandle destination, UnityEngine.Vector4 scaleBias, System.Single mipLevel, System.Boolean bilinear) { }
        // RVA: 0x0B2DA644  token: 0x6000650
        public static System.Void BlitCameraTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle source, UnityEngine.Rendering.RTHandle destination, UnityEngine.Rect destViewport, System.Single mipLevel, System.Boolean bilinear) { }
        // RVA: 0x0B2DBAC0  token: 0x6000651
        public static System.Void BlitQuad(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture source, UnityEngine.Vector4 scaleBiasTex, UnityEngine.Vector4 scaleBiasRT, System.Int32 mipLevelTex, System.Boolean bilinear) { }
        // RVA: 0x0B2DB7F8  token: 0x6000652
        public static System.Void BlitQuadWithPadding(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture source, UnityEngine.Vector2 textureSize, UnityEngine.Vector4 scaleBiasTex, UnityEngine.Vector4 scaleBiasRT, System.Int32 mipLevelTex, System.Boolean bilinear, System.Int32 paddingInPixels) { }
        // RVA: 0x0B2DB530  token: 0x6000653
        public static System.Void BlitQuadWithPaddingMultiply(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture source, UnityEngine.Vector2 textureSize, UnityEngine.Vector4 scaleBiasTex, UnityEngine.Vector4 scaleBiasRT, System.Int32 mipLevelTex, System.Boolean bilinear, System.Int32 paddingInPixels) { }
        // RVA: 0x0B2DB0A0  token: 0x6000654
        public static System.Void BlitOctahedralWithPadding(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture source, UnityEngine.Vector2 textureSize, UnityEngine.Vector4 scaleBiasTex, UnityEngine.Vector4 scaleBiasRT, System.Int32 mipLevelTex, System.Boolean bilinear, System.Int32 paddingInPixels) { }
        // RVA: 0x0B2DAE54  token: 0x6000655
        public static System.Void BlitOctahedralWithPaddingMultiply(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture source, UnityEngine.Vector2 textureSize, UnityEngine.Vector4 scaleBiasTex, UnityEngine.Vector4 scaleBiasRT, System.Int32 mipLevelTex, System.Boolean bilinear, System.Int32 paddingInPixels) { }
        // RVA: 0x0B2DACA0  token: 0x6000656
        public static System.Void BlitCubeToOctahedral2DQuad(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture source, UnityEngine.Vector4 scaleBiasRT, System.Int32 mipLevelTex) { }
        // RVA: 0x0B2DAA58  token: 0x6000657
        public static System.Void BlitCubeToOctahedral2DQuadSingleChannel(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture source, UnityEngine.Vector4 scaleBiasRT, System.Int32 mipLevelTex) { }
        // RVA: 0x0B2DB2EC  token: 0x6000658
        public static System.Void BlitQuadSingleChannel(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture source, UnityEngine.Vector4 scaleBiasTex, UnityEngine.Vector4 scaleBiasRT, System.Int32 mipLevelTex) { }
        // RVA: 0x04D22230  token: 0x6000659
        private static System.Void .cctor() { }
        // RVA: 0x0B2DC55C  token: 0x600065A
        private static UnityEngine.Vector3[] <Initialize>g__GetFullScreenTriangleVertexPosition|10_0(System.Single z) { }
        // RVA: 0x0B2DC464  token: 0x600065B
        private static UnityEngine.Vector2[] <Initialize>g__GetFullScreenTriangleTexCoord|10_1() { }
        // RVA: 0x0B2DC6F0  token: 0x600065C
        private static UnityEngine.Vector3[] <Initialize>g__GetQuadVertexPosition|10_2(System.Single z) { }
        // RVA: 0x0B2DC628  token: 0x600065D
        private static UnityEngine.Vector2[] <Initialize>g__GetQuadTexCoord|10_3() { }

    }

    // TypeToken: 0x200011B  // size: 0x10
    public static class CameraCaptureBridge
    {
        // Fields
        private static System.Collections.Generic.Dictionary<UnityEngine.Camera,System.Collections.Generic.HashSet<System.Action<UnityEngine.Rendering.RenderTargetIdentifier,UnityEngine.Rendering.CommandBuffer>>> actionDict;  // static @ 0x0
        private static System.Boolean _enabled;  // static @ 0x8

        // Properties
        System.Boolean enabled { get; /* RVA: 0x0B2DC9CC */ set; /* RVA: 0x0B2DCA1C */ }

        // Methods
        // RVA: 0x0324C8D0  token: 0x6000661
        public static System.Collections.Generic.IEnumerator<System.Action<UnityEngine.Rendering.RenderTargetIdentifier,UnityEngine.Rendering.CommandBuffer>> GetCaptureActions(UnityEngine.Camera camera) { }
        // RVA: 0x0B2DC7B0  token: 0x6000662
        public static System.Void AddCaptureAction(UnityEngine.Camera camera, System.Action<UnityEngine.Rendering.RenderTargetIdentifier,UnityEngine.Rendering.CommandBuffer> action) { }
        // RVA: 0x0B2DC8F4  token: 0x6000663
        public static System.Void RemoveCaptureAction(UnityEngine.Camera camera, System.Action<UnityEngine.Rendering.RenderTargetIdentifier,UnityEngine.Rendering.CommandBuffer> action) { }
        // RVA: 0x04D18A80  token: 0x6000664
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200011C  // size: 0x10
    public static class ColorUtils
    {
        // Fields
        public static System.Single s_LightMeterCalibrationConstant;  // static @ 0x0
        public static System.Single s_LensAttenuation;  // static @ 0x4

        // Properties
        System.Single lensImperfectionExposureScale { get; /* RVA: 0x0B2DD54C */ }

        // Methods
        // RVA: 0x04DB9180  token: 0x6000666
        public static System.Single StandardIlluminantY(System.Single x) { }
        // RVA: 0x0B2DCA90  token: 0x6000667
        public static UnityEngine.Vector3 CIExyToLMS(System.Single x, System.Single y) { }
        // RVA: 0x0B2DCB20  token: 0x6000668
        public static UnityEngine.Vector3 ColorBalanceToLMSCoeffs(System.Single temperature, System.Single tint) { }
        // RVA: 0x0B2DD168  token: 0x6000669
        public static System.ValueTuple<UnityEngine.Vector4,UnityEngine.Vector4,UnityEngine.Vector4> PrepareShadowsMidtonesHighlights(UnityEngine.Vector4& inShadows, UnityEngine.Vector4& inMidtones, UnityEngine.Vector4& inHighlights) { }
        // RVA: 0x0B2DCDE4  token: 0x600066A
        public static System.ValueTuple<UnityEngine.Vector4,UnityEngine.Vector4,UnityEngine.Vector4> PrepareLiftGammaGain(UnityEngine.Vector4& inLift, UnityEngine.Vector4& inGamma, UnityEngine.Vector4& inGain) { }
        // RVA: 0x0B2DD3C8  token: 0x600066B
        public static System.ValueTuple<UnityEngine.Vector4,UnityEngine.Vector4> PrepareSplitToning(UnityEngine.Vector4& inShadows, UnityEngine.Vector4& inHighlights, System.Single balance) { }
        // RVA: 0x04DB9150  token: 0x600066C
        public static System.Single Luminance(UnityEngine.Color& color) { }
        // RVA: 0x0B2DCC9C  token: 0x600066D
        public static System.Single ComputeEV100(System.Single aperture, System.Single shutterSpeed, System.Single ISO) { }
        // RVA: 0x0B2DCD0C  token: 0x600066E
        public static System.Single ConvertEV100ToExposure(System.Single EV100) { }
        // RVA: 0x0B2DCD80  token: 0x600066F
        public static System.Single ConvertExposureToEV100(System.Single exposure) { }
        // RVA: 0x0B2DCC34  token: 0x6000670
        public static System.Single ComputeEV100FromAvgLuminance(System.Single avgLuminance) { }
        // RVA: 0x0B2DCCC0  token: 0x6000671
        public static System.Single ComputeISO(System.Single aperture, System.Single shutterSpeed, System.Single targetEV100) { }
        // RVA: 0x0B2DD454  token: 0x6000672
        public static System.UInt32 ToHex(UnityEngine.Color c) { }
        // RVA: 0x0B2DD4D4  token: 0x6000673
        public static UnityEngine.Color ToRGBA(System.UInt32 hex) { }
        // RVA: 0x04D5E270  token: 0x6000674
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200011D  // size: 0x10
    public static class CoreMatrixUtils
    {
        // Methods
        // RVA: 0x04DB91B0  token: 0x6000675
        public static System.Void MatrixTimesTranslation(UnityEngine.Matrix4x4& inOutMatrix, UnityEngine.Vector3 translation) { }
        // RVA: 0x04DB9680  token: 0x6000676
        public static System.Void TranslationTimesMatrix(UnityEngine.Matrix4x4& inOutMatrix, UnityEngine.Vector3 translation) { }
        // RVA: 0x04DB9530  token: 0x6000677
        public static UnityEngine.Matrix4x4 MultiplyPerspectiveMatrix(UnityEngine.Matrix4x4 perspective, UnityEngine.Matrix4x4 rhs) { }
        // RVA: 0x04DB93F0  token: 0x6000678
        private static UnityEngine.Matrix4x4 MultiplyOrthoMatrixCentered(UnityEngine.Matrix4x4 ortho, UnityEngine.Matrix4x4 rhs) { }
        // RVA: 0x04DB9240  token: 0x6000679
        private static UnityEngine.Matrix4x4 MultiplyGenericOrthoMatrix(UnityEngine.Matrix4x4 ortho, UnityEngine.Matrix4x4 rhs) { }
        // RVA: 0x0B2DD5B0  token: 0x600067A
        public static UnityEngine.Matrix4x4 MultiplyOrthoMatrix(UnityEngine.Matrix4x4 ortho, UnityEngine.Matrix4x4 rhs, System.Boolean centered) { }
        // RVA: 0x0B2DD6A0  token: 0x600067B
        public static UnityEngine.Matrix4x4 MultiplyProjectionMatrix(UnityEngine.Matrix4x4 projMatrix, UnityEngine.Matrix4x4 rhs, System.Boolean orthoCentered) { }

    }

    // TypeToken: 0x200011E  // size: 0x10
    public static class CoreUtils
    {
        // Fields
        public static readonly UnityEngine.Vector3[] lookAtList;  // static @ 0x0
        public static readonly UnityEngine.Vector3[] upVectorList;  // static @ 0x8
        private static System.String obsoletePriorityMessage;  // const
        public static System.Int32 editMenuPriority1;  // const
        public static System.Int32 editMenuPriority2;  // const
        public static System.Int32 editMenuPriority3;  // const
        public static System.Int32 editMenuPriority4;  // const
        public static System.Int32 assetCreateMenuPriority1;  // const
        public static System.Int32 assetCreateMenuPriority2;  // const
        public static System.Int32 assetCreateMenuPriority3;  // const
        public static System.Int32 gameObjectMenuPriority;  // const
        private static UnityEngine.Cubemap m_BlackCubeTexture;  // static @ 0x10
        private static UnityEngine.Cubemap m_MagentaCubeTexture;  // static @ 0x18
        private static UnityEngine.CubemapArray m_MagentaCubeTextureArray;  // static @ 0x20
        private static UnityEngine.Cubemap m_WhiteCubeTexture;  // static @ 0x28
        private static UnityEngine.RenderTexture m_EmptyUAV;  // static @ 0x30
        private static UnityEngine.Texture3D m_BlackVolumeTexture;  // static @ 0x38
        private static System.Collections.Generic.IEnumerable<System.Type> m_AssemblyTypes;  // static @ 0x40

        // Properties
        UnityEngine.Cubemap blackCubeTexture { get; /* RVA: 0x0B2E03DC */ }
        UnityEngine.Cubemap magentaCubeTexture { get; /* RVA: 0x0B2E0A38 */ }
        UnityEngine.CubemapArray magentaCubeTextureArray { get; /* RVA: 0x0B2E0868 */ }
        UnityEngine.Cubemap whiteCubeTexture { get; /* RVA: 0x0B2E0BCC */ }
        UnityEngine.RenderTexture emptyUAV { get; /* RVA: 0x0B2E0720 */ }
        UnityEngine.Texture3D blackVolumeTexture { get; /* RVA: 0x0B2E0570 */ }

        // Methods
        // RVA: 0x0B2DD8E0  token: 0x6000682
        public static System.Void ClearRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor) { }
        // RVA: 0x0B2DE678  token: 0x6000683
        private static System.Int32 FixupDepthSlice(System.Int32 depthSlice, UnityEngine.Rendering.RTHandle buffer) { }
        // RVA: 0x0B2DE668  token: 0x6000684
        private static System.Int32 FixupDepthSlice(System.Int32 depthSlice, UnityEngine.CubemapFace cubemapFace) { }
        // RVA: 0x0B2DF178  token: 0x6000685
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier buffer, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice) { }
        // RVA: 0x0B2DF368  token: 0x6000686
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier buffer, UnityEngine.Rendering.ClearFlag clearFlag, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice) { }
        // RVA: 0x0B2DFFF8  token: 0x6000687
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice) { }
        // RVA: 0x0B2DF258  token: 0x6000688
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, UnityEngine.Rendering.ClearFlag clearFlag, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice) { }
        // RVA: 0x0B2DFA34  token: 0x6000689
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice) { }
        // RVA: 0x0B2DF0C0  token: 0x600068A
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, UnityEngine.Rendering.RenderTargetIdentifier depthBuffer) { }
        // RVA: 0x0B2DF000  token: 0x600068B
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, UnityEngine.Rendering.ClearFlag clearFlag) { }
        // RVA: 0x0B2DFB4C  token: 0x600068C
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor) { }
        // RVA: 0x0B2E0100  token: 0x600068D
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier buffer, UnityEngine.Rendering.RenderBufferLoadAction loadAction, UnityEngine.Rendering.RenderBufferStoreAction storeAction, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor) { }
        // RVA: 0x0B2DFD24  token: 0x600068E
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier buffer, UnityEngine.Rendering.RenderBufferLoadAction loadAction, UnityEngine.Rendering.RenderBufferStoreAction storeAction, UnityEngine.Rendering.ClearFlag clearFlag) { }
        // RVA: 0x0B2DFEDC  token: 0x600068F
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, UnityEngine.Rendering.RenderBufferLoadAction colorLoadAction, UnityEngine.Rendering.RenderBufferStoreAction colorStoreAction, UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, UnityEngine.Rendering.RenderBufferLoadAction depthLoadAction, UnityEngine.Rendering.RenderBufferStoreAction depthStoreAction, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor) { }
        // RVA: 0x0B2DF43C  token: 0x6000690
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier buffer, UnityEngine.Rendering.RenderBufferLoadAction colorLoadAction, UnityEngine.Rendering.RenderBufferStoreAction colorStoreAction, UnityEngine.Rendering.RenderBufferLoadAction depthLoadAction, UnityEngine.Rendering.RenderBufferStoreAction depthStoreAction, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor) { }
        // RVA: 0x0B2DF748  token: 0x6000691
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, UnityEngine.Rendering.RenderBufferLoadAction colorLoadAction, UnityEngine.Rendering.RenderBufferStoreAction colorStoreAction, UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, UnityEngine.Rendering.RenderBufferLoadAction depthLoadAction, UnityEngine.Rendering.RenderBufferStoreAction depthStoreAction, UnityEngine.Rendering.ClearFlag clearFlag) { }
        // RVA: 0x0B2E0278  token: 0x6000692
        private static System.Void SetViewportAndClear(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle buffer, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor) { }
        // RVA: 0x0B2DFC20  token: 0x6000693
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle buffer, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice) { }
        // RVA: 0x0B2E01C8  token: 0x6000694
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle buffer, UnityEngine.Rendering.ClearFlag clearFlag, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice) { }
        // RVA: 0x0B2DF61C  token: 0x6000695
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle colorBuffer, UnityEngine.Rendering.RTHandle depthBuffer, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice) { }
        // RVA: 0x0B2DEDCC  token: 0x6000696
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle colorBuffer, UnityEngine.Rendering.RTHandle depthBuffer, UnityEngine.Rendering.ClearFlag clearFlag, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice) { }
        // RVA: 0x0B2DF868  token: 0x6000697
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle colorBuffer, UnityEngine.Rendering.RTHandle depthBuffer, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice) { }
        // RVA: 0x0B2DEF00  token: 0x6000698
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, UnityEngine.Rendering.RTHandle depthBuffer) { }
        // RVA: 0x0B2DF520  token: 0x6000699
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, UnityEngine.Rendering.RTHandle depthBuffer, UnityEngine.Rendering.ClearFlag clearFlag) { }
        // RVA: 0x0B2DFDF0  token: 0x600069A
        public static System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, UnityEngine.Rendering.RTHandle depthBuffer, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor) { }
        // RVA: 0x0B2E0300  token: 0x600069B
        public static System.Void SetViewport(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle target) { }
        // RVA: 0x037E0EB0  token: 0x600069C
        public static System.String GetRenderTargetAutoName(System.Int32 width, System.Int32 height, System.Int32 depth, UnityEngine.RenderTextureFormat format, System.String name, System.Boolean mips, System.Boolean enableMSAA, UnityEngine.Rendering.MSAASamples msaaSamples) { }
        // RVA: 0x0B2DE6E8  token: 0x600069D
        public static System.String GetRenderTargetAutoName(System.Int32 width, System.Int32 height, System.Int32 depth, UnityEngine.Experimental.Rendering.GraphicsFormat format, System.String name, System.Boolean mips, System.Boolean enableMSAA, UnityEngine.Rendering.MSAASamples msaaSamples) { }
        // RVA: 0x037DFB30  token: 0x600069E
        public static System.String GetRenderTargetAutoName(System.Int32 width, System.Int32 height, System.Int32 depth, UnityEngine.Experimental.Rendering.GraphicsFormat format, UnityEngine.Rendering.TextureDimension dim, System.String name, System.Boolean mips, System.Boolean enableMSAA, UnityEngine.Rendering.MSAASamples msaaSamples, System.Boolean dynamicRes) { }
        // RVA: 0x037DFC40  token: 0x600069F
        private static System.String GetRenderTargetAutoName(System.Int32 width, System.Int32 height, System.Int32 depth, System.String format, UnityEngine.Rendering.TextureDimension dim, System.String name, System.Boolean mips, System.Boolean enableMSAA, UnityEngine.Rendering.MSAASamples msaaSamples, System.Boolean dynamicRes) { }
        // RVA: 0x0B2DE890  token: 0x60006A0
        public static System.String GetTextureAutoName(System.Int32 width, System.Int32 height, UnityEngine.TextureFormat format, UnityEngine.Rendering.TextureDimension dim, System.String name, System.Boolean mips, System.Int32 depth) { }
        // RVA: 0x0B2DE7C8  token: 0x60006A1
        public static System.String GetTextureAutoName(System.Int32 width, System.Int32 height, UnityEngine.Experimental.Rendering.GraphicsFormat format, UnityEngine.Rendering.TextureDimension dim, System.String name, System.Boolean mips, System.Int32 depth) { }
        // RVA: 0x0B2DE958  token: 0x60006A2
        private static System.String GetTextureAutoName(System.Int32 width, System.Int32 height, System.String format, UnityEngine.Rendering.TextureDimension dim, System.String name, System.Boolean mips, System.Int32 depth) { }
        // RVA: 0x0B2DD790  token: 0x60006A3
        public static System.Void ClearCubemap(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.RenderTexture renderTexture, UnityEngine.Color clearColor, System.Boolean clearMips) { }
        // RVA: 0x0B2DE0CC  token: 0x60006A4
        public static System.Void DrawFullScreen(UnityEngine.Rendering.CommandBuffer commandBuffer, UnityEngine.Material material, UnityEngine.MaterialPropertyBlock properties, System.Int32 shaderPassId) { }
        // RVA: 0x0B2DE14C  token: 0x60006A5
        public static System.Void DrawFullScreen(UnityEngine.Rendering.CommandBuffer commandBuffer, UnityEngine.Material material, UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, UnityEngine.MaterialPropertyBlock properties, System.Int32 shaderPassId) { }
        // RVA: 0x0B2DE230  token: 0x60006A6
        public static System.Void DrawFullScreen(UnityEngine.Rendering.CommandBuffer commandBuffer, UnityEngine.Material material, UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, UnityEngine.Rendering.RenderTargetIdentifier depthStencilBuffer, UnityEngine.MaterialPropertyBlock properties, System.Int32 shaderPassId) { }
        // RVA: 0x0B2DDF14  token: 0x60006A7
        public static System.Void DrawFullScreen(UnityEngine.Rendering.CommandBuffer commandBuffer, UnityEngine.Material material, UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, UnityEngine.Rendering.RenderTargetIdentifier depthStencilBuffer, UnityEngine.MaterialPropertyBlock properties, System.Int32 shaderPassId) { }
        // RVA: 0x0B2DE000  token: 0x60006A8
        public static System.Void DrawFullScreen(UnityEngine.Rendering.CommandBuffer commandBuffer, UnityEngine.Material material, UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, UnityEngine.MaterialPropertyBlock properties, System.Int32 shaderPassId) { }
        // RVA: 0x0B2DD964  token: 0x60006A9
        public static UnityEngine.Color ConvertSRGBToActiveColorSpace(UnityEngine.Color color) { }
        // RVA: 0x0B2DD920  token: 0x60006AA
        public static UnityEngine.Color ConvertLinearToActiveColorSpace(UnityEngine.Color color) { }
        // RVA: 0x0B2DDE14  token: 0x60006AB
        public static UnityEngine.Material CreateEngineMaterial(System.String shaderPath) { }
        // RVA: 0x0351F8A0  token: 0x60006AC
        public static UnityEngine.Material CreateEngineMaterial(UnityEngine.Shader shader, System.Boolean enableInstancing) { }
        // RVA: -1  // generic def  token: 0x60006AD
        public static System.Boolean HasFlag(T mask, T flag) { }
        // RVA: -1  // generic def  token: 0x60006AE
        public static System.Void Swap(T& a, T& b) { }
        // RVA: 0x0B2DED98  token: 0x60006AF
        public static System.Void SetKeyword(UnityEngine.Rendering.CommandBuffer cmd, System.String keyword, System.Boolean state) { }
        // RVA: 0x0690914C  token: 0x60006B0
        public static System.Void SetKeyword(UnityEngine.Material material, System.String keyword, System.Boolean state) { }
        // RVA: 0x0B2DED64  token: 0x60006B1
        public static System.Void SetKeyword(UnityEngine.ComputeShader cs, System.String keyword, System.Boolean state) { }
        // RVA: 0x037DD090  token: 0x60006B2
        public static System.Void Destroy(UnityEngine.Object obj) { }
        // RVA: 0x03B0CFF0  token: 0x60006B3
        public static System.Collections.Generic.IEnumerable<System.Type> GetAllAssemblyTypes() { }
        // RVA: -1  // generic def  token: 0x60006B4
        public static System.Collections.Generic.IEnumerable<System.Type> GetAllTypesDerivedFrom() { }
        // RVA: 0x0B2DED54  token: 0x60006B5
        public static System.Void SafeRelease(UnityEngine.ComputeBuffer buffer) { }
        // RVA: 0x0B2DD9A8  token: 0x60006B6
        public static UnityEngine.Mesh CreateCubeMesh(UnityEngine.Vector3 min, UnityEngine.Vector3 max) { }
        // RVA: 0x02FFF600  token: 0x60006B7
        public static System.Boolean ArePostProcessesEnabled(UnityEngine.Camera camera) { }
        // RVA: 0x02FFF600  token: 0x60006B8
        public static System.Boolean AreAnimatedMaterialsEnabled(UnityEngine.Camera camera) { }
        // RVA: 0x012081B0  token: 0x60006B9
        public static System.Boolean IsSceneLightingDisabled(UnityEngine.Camera camera) { }
        // RVA: 0x012081B0  token: 0x60006BA
        public static System.Boolean IsLightOverlapDebugEnabled(UnityEngine.Camera camera) { }
        // RVA: 0x02FFF600  token: 0x60006BB
        public static System.Boolean IsSceneViewFogEnabled(UnityEngine.Camera camera) { }
        // RVA: 0x012081B0  token: 0x60006BC
        public static System.Boolean IsSceneFilteringEnabled() { }
        // RVA: 0x012081B0  token: 0x60006BD
        public static System.Boolean IsSceneViewPrefabStageContextHidden() { }
        // RVA: 0x0B2DE32C  token: 0x60006BE
        public static System.Void DrawRendererList(UnityEngine.Rendering.ScriptableRenderContext renderContext, UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Experimental.Rendering.RendererList rendererList) { }
        // RVA: 0x0B2DE5F0  token: 0x60006BF
        public static System.Void DrawRendererList(UnityEngine.Rendering.ScriptableRenderContext renderContext, UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RendererUtils.RendererList rendererList) { }
        // RVA: 0x0B2DEC54  token: 0x60006C0
        public static System.Int32 GetTextureHash(UnityEngine.Texture texture) { }
        // RVA: 0x0B2DED20  token: 0x60006C1
        public static System.Int32 PreviousPowerOfTwo(System.Int32 size) { }
        // RVA: -1  // generic def  token: 0x60006C2
        public static T GetLastEnumValue() { }
        // RVA: 0x0B2DE6BC  token: 0x60006C3
        private static System.String GetCorePath() { }
        // RVA: 0x048B65E0  token: 0x60006C4
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000123  // size: 0x10
    public static class DelegateUtility
    {
        // Methods
        // RVA: 0x0B2E321C  token: 0x60006CB
        public static System.Delegate Cast(System.Delegate source, System.Type type) { }

    }

    // TypeToken: 0x2000124  // size: 0x10
    public static class FSRUtils
    {
        // Fields
        private static System.Single kMaxSharpnessStops;  // const
        public static System.Single kDefaultSharpnessStops;  // const
        public static System.Single kDefaultSharpnessLinear;  // const

        // Methods
        // RVA: 0x0B2E3374  token: 0x60006CC
        public static System.Void SetEasuConstants(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector2 inputViewportSizeInPixels, UnityEngine.Vector2 inputImageSizeInPixels, UnityEngine.Vector2 outputImageSizeInPixels) { }
        // RVA: 0x0B2E3580  token: 0x60006CD
        public static System.Void SetRcasConstants(UnityEngine.Rendering.CommandBuffer cmd, System.Single sharpnessStops) { }
        // RVA: 0x0B2E3560  token: 0x60006CE
        public static System.Void SetRcasConstantsLinear(UnityEngine.Rendering.CommandBuffer cmd, System.Single sharpnessLinear) { }
        // RVA: 0x0B2E335C  token: 0x60006CF
        public static System.Boolean IsSupported() { }

    }

    // TypeToken: 0x2000126  // size: 0x30
    public class HableCurve
    {
        // Fields
        private System.Single <whitePoint>k__BackingField;  // 0x10
        private System.Single <inverseWhitePoint>k__BackingField;  // 0x14
        private System.Single <x0>k__BackingField;  // 0x18
        private System.Single <x1>k__BackingField;  // 0x1c
        public readonly UnityEngine.Rendering.HableCurve.Segment[] segments;  // 0x20
        public readonly UnityEngine.Rendering.HableCurve.Uniforms uniforms;  // 0x28

        // Properties
        System.Single whitePoint { get; /* RVA: 0x04D85F70 */ set; /* RVA: 0x04D88280 */ }
        System.Single inverseWhitePoint { get; /* RVA: 0x04D88D40 */ set; /* RVA: 0x04D88D50 */ }
        System.Single x0 { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x04D86390 */ }
        System.Single x1 { get; /* RVA: 0x04D868F0 */ set; /* RVA: 0x04D86900 */ }

        // Methods
        // RVA: 0x0B2E3E30  token: 0x60006D9
        public System.Void .ctor() { }
        // RVA: 0x0B2E36CC  token: 0x60006DA
        public System.Single Eval(System.Single x) { }
        // RVA: 0x0B2E3C1C  token: 0x60006DB
        public System.Void Init(System.Single toeStrength, System.Single toeLength, System.Single shoulderStrength, System.Single shoulderLength, System.Single shoulderAngle, System.Single gamma) { }
        // RVA: 0x0B2E3734  token: 0x60006DC
        private System.Void InitSegments(UnityEngine.Rendering.HableCurve.DirectParams srcParams) { }
        // RVA: 0x0B2E3DC0  token: 0x60006DD
        private System.Void SolveAB(System.Single& lnA, System.Single& B, System.Single x0, System.Single y0, System.Single m) { }
        // RVA: 0x0B2E3684  token: 0x60006DE
        private System.Void AsSlopeIntercept(System.Single& m, System.Single& b, System.Single x0, System.Single x1, System.Single y0, System.Single y1) { }
        // RVA: 0x04DB97E0  token: 0x60006DF
        private System.Single EvalDerivativeLinearGamma(System.Single m, System.Single b, System.Single g, System.Single x) { }

    }

    // TypeToken: 0x200012A  // size: 0x10
    public static class HaltonSequence
    {
        // Methods
        // RVA: 0x0326BFC0  token: 0x60006EA
        public static System.Single Get(System.Int32 index, System.Int32 radix) { }

    }

    // TypeToken: 0x200012B  // size: 0x14
    public sealed struct MaterialQuality
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Rendering.MaterialQuality Low;  // const
        public static UnityEngine.Rendering.MaterialQuality Medium;  // const
        public static UnityEngine.Rendering.MaterialQuality High;  // const

    }

    // TypeToken: 0x200012C  // size: 0x10
    public static class MaterialQualityUtilities
    {
        // Fields
        public static System.String[] KeywordNames;  // static @ 0x0
        public static System.String[] EnumNames;  // static @ 0x8
        public static UnityEngine.Rendering.ShaderKeyword[] Keywords;  // static @ 0x10

        // Methods
        // RVA: 0x0B2E4090  token: 0x60006EB
        public static UnityEngine.Rendering.MaterialQuality GetHighestQuality(UnityEngine.Rendering.MaterialQuality levels) { }
        // RVA: 0x0B2E3F88  token: 0x60006EC
        public static UnityEngine.Rendering.MaterialQuality GetClosestQuality(UnityEngine.Rendering.MaterialQuality availableLevels, UnityEngine.Rendering.MaterialQuality requestedLevel) { }
        // RVA: 0x0B2E4104  token: 0x60006ED
        public static System.Void SetGlobalShaderKeywords(UnityEngine.Rendering.MaterialQuality level) { }
        // RVA: 0x0B2E41E0  token: 0x60006EE
        public static System.Void SetGlobalShaderKeywords(UnityEngine.Rendering.MaterialQuality level, UnityEngine.Rendering.CommandBuffer cmd) { }
        // RVA: 0x0B2E42E0  token: 0x60006EF
        public static System.Int32 ToFirstIndex(UnityEngine.Rendering.MaterialQuality level) { }
        // RVA: 0x04D88AF0  token: 0x60006F0
        public static UnityEngine.Rendering.MaterialQuality FromIndex(System.Int32 index) { }
        // RVA: 0x0B2E435C  token: 0x60006F1
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200012D  // size: 0x48
    public class MeshGizmo : System.IDisposable
    {
        // Fields
        public static readonly System.Int32 vertexCountPerCube;  // static @ 0x0
        public UnityEngine.Mesh mesh;  // 0x10
        private System.Collections.Generic.List<UnityEngine.Vector3> vertices;  // 0x18
        private System.Collections.Generic.List<System.Int32> indices;  // 0x20
        private System.Collections.Generic.List<UnityEngine.Color> colors;  // 0x28
        private UnityEngine.Material wireMaterial;  // 0x30
        private UnityEngine.Material dottedWireMaterial;  // 0x38
        private UnityEngine.Material solidMaterial;  // 0x40

        // Methods
        // RVA: 0x0B2E541C  token: 0x60006F2
        public System.Void .ctor(System.Int32 capacity) { }
        // RVA: 0x0B2E4FA0  token: 0x60006F3
        public System.Void Clear() { }
        // RVA: 0x0B2E465C  token: 0x60006F4
        public System.Void AddWireCube(UnityEngine.Vector3 center, UnityEngine.Vector3 size, UnityEngine.Color color) { }
        // RVA: 0x0B2E506C  token: 0x60006F5
        private System.Void DrawMesh(UnityEngine.Matrix4x4 trs, UnityEngine.Material mat, UnityEngine.MeshTopology topology, UnityEngine.Rendering.CompareFunction depthTest, System.String gizmoName) { }
        // RVA: 0x0B2E522C  token: 0x60006F6
        public System.Void RenderWireframe(UnityEngine.Matrix4x4 trs, UnityEngine.Rendering.CompareFunction depthTest, System.String gizmoName) { }
        // RVA: 0x0B2E501C  token: 0x60006F7
        public virtual System.Void Dispose() { }
        // RVA: 0x0B2E53E0  token: 0x60006F8
        private static System.Void .cctor() { }
        // RVA: 0x0B2E5280  token: 0x60006F9
        private System.Void <AddWireCube>g__AddEdge|10_0(UnityEngine.Vector3 p1, UnityEngine.Vector3 p2, UnityEngine.Rendering.MeshGizmo.<>c__DisplayClass10_0& ) { }

    }

    // TypeToken: 0x200012F  // size: 0x10
    public sealed class ReloadAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60006FA
        public System.Void .ctor(System.String[] paths, UnityEngine.Rendering.ReloadAttribute.Package package) { }
        // RVA: 0x0B2E5FC0  token: 0x60006FB
        public System.Void .ctor(System.String path, UnityEngine.Rendering.ReloadAttribute.Package package) { }
        // RVA: 0x041E1670  token: 0x60006FC
        public System.Void .ctor(System.String pathFormat, System.Int32 rangeMin, System.Int32 rangeMax, UnityEngine.Rendering.ReloadAttribute.Package package) { }

    }

    // TypeToken: 0x2000131  // size: 0x10
    public sealed class ReloadGroupAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60006FD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000132  // size: 0x40
    public class TextureCurve : System.IDisposable
    {
        // Fields
        private static System.Int32 k_Precision;  // const
        private static System.Single k_Step;  // const
        private System.Int32 <length>k__BackingField;  // 0x10
        private System.Boolean m_Loop;  // 0x14
        private System.Single m_ZeroValue;  // 0x18
        private System.Single m_Range;  // 0x1c
        private UnityEngine.AnimationCurve m_Curve;  // 0x20
        private UnityEngine.AnimationCurve m_LoopingCurve;  // 0x28
        private UnityEngine.Texture2D m_Texture;  // 0x30
        private System.Boolean m_IsCurveDirty;  // 0x38
        private System.Boolean m_IsTextureDirty;  // 0x39
        private System.Int32 m_LastUpdatedFrame;  // 0x3c

        // Properties
        System.Int32 length { get; /* RVA: 0x02B2E2D0 */ set; /* RVA: 0x014F51F0 */ }
        UnityEngine.Keyframe Item { get; /* RVA: 0x0B2E6588 */ }

        // Methods
        // RVA: 0x03C06DA0  token: 0x6000700
        public System.Boolean IsTextureCurveDirty() { }
        // RVA: 0x0B2E6524  token: 0x6000702
        public System.Void .ctor(UnityEngine.AnimationCurve baseCurve, System.Single zeroValue, System.Boolean loop, UnityEngine.Vector2& bounds) { }
        // RVA: 0x039C5760  token: 0x6000703
        public System.Void .ctor(UnityEngine.Keyframe[] keys, System.Single zeroValue, System.Boolean loop, UnityEngine.Vector2& bounds) { }
        // RVA: 0x0421C210  token: 0x6000704
        protected virtual System.Void Finalize() { }
        // RVA: 0x041E1670  token: 0x6000705
        public virtual System.Void Dispose() { }
        // RVA: 0x037DC8B0  token: 0x6000706
        public System.Void Release() { }
        // RVA: 0x04DB9870  token: 0x6000707
        public System.Void SetDirty() { }
        // RVA: 0x037E0A50  token: 0x6000708
        private static UnityEngine.Experimental.Rendering.GraphicsFormat GetTextureFormat() { }
        // RVA: 0x037E07A0  token: 0x6000709
        public UnityEngine.Texture2D GetTexture() { }
        // RVA: 0x037E09B0  token: 0x600070A
        public System.Single Evaluate(System.Single time) { }
        // RVA: 0x0B2E6448  token: 0x600070B
        public System.Int32 AddKey(System.Single time, System.Single value) { }
        // RVA: 0x0B2E647C  token: 0x600070C
        public System.Int32 MoveKey(System.Int32 index, UnityEngine.Keyframe& key) { }
        // RVA: 0x0B2E64CC  token: 0x600070D
        public System.Void RemoveKey(System.Int32 index) { }
        // RVA: 0x0B2E64F8  token: 0x600070E
        public System.Void SmoothTangents(System.Int32 index, System.Single weight) { }

    }

    // TypeToken: 0x2000133  // size: 0x20
    public class TextureCurveParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x039C5700  token: 0x600070F
        public System.Void .ctor(UnityEngine.Rendering.TextureCurve value, System.Boolean overrideState) { }
        // RVA: 0x037DC890  token: 0x6000710
        public virtual System.Void Release() { }

    }

    // TypeToken: 0x2000134  // size: 0x10
    public static class TileLayoutUtils
    {
        // Methods
        // RVA: 0x0B2E6770  token: 0x6000711
        public static System.Boolean TryLayoutByTiles(UnityEngine.RectInt src, System.UInt32 tileSize, UnityEngine.RectInt& main, UnityEngine.RectInt& topRow, UnityEngine.RectInt& rightCol, UnityEngine.RectInt& topRight) { }
        // RVA: 0x0B2E66FC  token: 0x6000712
        public static System.Boolean TryLayoutByRow(UnityEngine.RectInt src, System.UInt32 tileSize, UnityEngine.RectInt& main, UnityEngine.RectInt& other) { }
        // RVA: 0x0B2E668C  token: 0x6000713
        public static System.Boolean TryLayoutByCol(UnityEngine.RectInt src, System.UInt32 tileSize, UnityEngine.RectInt& main, UnityEngine.RectInt& other) { }

    }

    // TypeToken: 0x2000135  // size: 0x10
    public static class XRUtils
    {
        // Methods
        // RVA: 0x0B2E7AA4  token: 0x6000714
        public static System.Void DrawOcclusionMesh(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Camera camera, System.Boolean stereoEnabled) { }

    }

    // TypeToken: 0x2000136
    public interface IVolume
    {
        // Properties
        System.Boolean isGlobal { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Collections.Generic.List<UnityEngine.Collider> colliders { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000718
        public virtual System.Void Play() { }
        // RVA: -1  // abstract  token: 0x6000719
        public virtual System.Void Stop() { }

    }

    // TypeToken: 0x2000137  // size: 0x60
    public class Volume : UnityEngine.MonoBehaviour, UnityEngine.Rendering.IVolume
    {
        // Fields
        private System.Boolean m_IsGlobal;  // 0x18
        public System.Boolean isLunaPlatform;  // 0x19
        public System.Action<UnityEngine.Rendering.VolumeProfile> LunaCreateVolumeProfile;  // 0x20
        public System.Single priority;  // 0x28
        public System.Single blendDistance;  // 0x2c
        public System.Single weight;  // 0x30
        public UnityEngine.Rendering.VolumeProfile sharedProfile;  // 0x38
        private System.Collections.Generic.List<UnityEngine.Collider> m_Colliders;  // 0x40
        private System.Int32 m_PreviousLayer;  // 0x48
        private System.Single m_PreviousPriority;  // 0x4c
        private UnityEngine.Rendering.VolumeProfile m_InternalProfile;  // 0x50
        public System.Boolean vfxEnableState;  // 0x58

        // Properties
        System.Boolean isGlobal { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x04D86350 */ }
        System.Collections.Generic.List<UnityEngine.Collider> colliders { get; /* RVA: 0x04D85A60 */ }
        UnityEngine.Rendering.VolumeProfile profileRef { get; /* RVA: 0x02FC7110 */ }

        // Methods
        // RVA: 0x0B2E7768  token: 0x600071C
        public UnityEngine.Rendering.VolumeProfile GetInstantiatedProfile() { }
        // RVA: 0x02FDAEA0  token: 0x600071D
        public System.Void SetInstantiatedProfile(UnityEngine.Rendering.VolumeProfile profile) { }
        // RVA: 0x0B2E79BC  token: 0x6000720
        public System.Boolean HasInstantiatedProfile() { }
        // RVA: 0x04255790  token: 0x6000721
        private System.Void OnEnable() { }
        // RVA: 0x04255700  token: 0x6000722
        private System.Void OnDisable() { }
        // RVA: 0x031C4890  token: 0x6000723
        private System.Void Update() { }
        // RVA: 0x031C4980  token: 0x6000724
        private System.Void UpdateLayer() { }
        // RVA: 0x04DA7440  token: 0x6000725
        public virtual System.Void Play() { }
        // RVA: 0x04DB98D0  token: 0x6000726
        public virtual System.Void Stop() { }
        // RVA: 0x0395B350  token: 0x6000727
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000138  // size: 0x18
    public class VolumeComponentMenu : System.Attribute
    {
        // Fields
        public readonly System.String menu;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000728
        public System.Void .ctor(System.String menu) { }

    }

    // TypeToken: 0x2000139  // size: 0x20
    public class VolumeComponentMenuForRenderPipeline : UnityEngine.Rendering.VolumeComponentMenu
    {
        // Fields
        private readonly System.Type[] <pipelineTypes>k__BackingField;  // 0x18

        // Properties
        System.Type[] pipelineTypes { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x0B2E6BCC  token: 0x600072A
        public System.Void .ctor(System.String menu, System.Type[] pipelineTypes) { }

    }

    // TypeToken: 0x200013A  // size: 0x10
    public sealed class VolumeComponentDeprecated : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600072B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200013B  // size: 0x10
    public sealed class MigratingVolumeComponent : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600072C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200013C  // size: 0x30
    public class VolumeComponent : UnityEngine.ScriptableObject
    {
        // Fields
        public System.Boolean active;  // 0x18
        private System.String <displayName>k__BackingField;  // 0x20
        private UnityEngine.Rendering.VolumeParameter[] <parameters>k__BackingField;  // 0x28

        // Properties
        System.String displayName { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        UnityEngine.Rendering.VolumeParameter[] parameters { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }

        // Methods
        // RVA: 0x035938A0  token: 0x6000731
        private static System.Void FindParameters(System.Object o, System.Collections.Generic.List<UnityEngine.Rendering.VolumeParameter> parameters, System.Func<System.Reflection.FieldInfo,System.Boolean> filter) { }
        // RVA: 0x02FC49F0  token: 0x6000732
        protected virtual System.Void OnEnable() { }
        // RVA: 0x02FC4950  token: 0x6000733
        protected virtual System.Void OnDisable() { }
        // RVA: 0x02FC5B80  token: 0x6000734
        public virtual System.Void Override(UnityEngine.Rendering.VolumeComponent state, System.Single interpFactor) { }
        // RVA: 0x02FC4340  token: 0x6000735
        public System.Void SetAllOverridesTo(System.Boolean state) { }
        // RVA: 0x02FC4110  token: 0x6000736
        private System.Void SetOverridesTo(System.Collections.Generic.IEnumerable<UnityEngine.Rendering.VolumeParameter> enumerable, System.Boolean state) { }
        // RVA: 0x0B2E6D98  token: 0x6000737
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x02FC4830  token: 0x6000738
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x02FC4980  token: 0x6000739
        public System.Void Release() { }
        // RVA: 0x03573680  token: 0x600073A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200013F  // size: 0x50
    public sealed class VolumeManager
    {
        // Fields
        private static readonly System.Lazy<UnityEngine.Rendering.VolumeManager> s_Instance;  // static @ 0x0
        private UnityEngine.Rendering.VolumeStack <stack>k__BackingField;  // 0x10
        private System.Type[] <baseComponentTypeArray>k__BackingField;  // 0x18
        private static System.Int32 k_MaxLayerCount;  // const
        private readonly System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<UnityEngine.Rendering.Volume>> m_SortedVolumes;  // 0x20
        private readonly System.Collections.Generic.List<UnityEngine.Rendering.Volume> m_Volumes;  // 0x28
        private readonly System.Collections.Generic.Dictionary<System.Int32,System.Boolean> m_SortNeeded;  // 0x30
        private readonly System.Collections.Generic.List<UnityEngine.Rendering.VolumeComponent> m_ComponentsDefaultState;  // 0x38
        private readonly System.Collections.Generic.List<UnityEngine.Collider> m_TempColliders;  // 0x40
        private UnityEngine.Rendering.VolumeStack m_DefaultStack;  // 0x48

        // Properties
        UnityEngine.Rendering.VolumeManager instance { get; /* RVA: 0x03450900 */ }
        UnityEngine.Rendering.VolumeStack stack { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.Collections.Generic.IEnumerable<System.Type> baseComponentTypes { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x0B2E7218 */ }
        System.Type[] baseComponentTypeArray { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x046FE230  token: 0x6000746
        private System.Void .ctor() { }
        // RVA: 0x02FC44D0  token: 0x6000747
        public UnityEngine.Rendering.VolumeStack CreateStack() { }
        // RVA: 0x0B2E71A0  token: 0x6000748
        public System.Void ResetMainStack() { }
        // RVA: 0x04D39BD0  token: 0x6000749
        public System.Void DestroyStack(UnityEngine.Rendering.VolumeStack stack) { }
        // RVA: 0x02FC45C0  token: 0x600074A
        private System.Void ReloadBaseTypes() { }
        // RVA: 0x042559C0  token: 0x600074B
        public System.Void Register(UnityEngine.Rendering.Volume volume, System.Int32 layer) { }
        // RVA: 0x04255840  token: 0x600074C
        public System.Void Unregister(UnityEngine.Rendering.Volume volume, System.Int32 layer) { }
        // RVA: -1  // generic def  token: 0x600074D
        public System.Boolean IsComponentActiveInMask(UnityEngine.LayerMask layerMask) { }
        // RVA: 0x04255BB0  token: 0x600074E
        private System.Void SetLayerDirty(System.Int32 layer) { }
        // RVA: 0x0B2E71B4  token: 0x600074F
        private System.Void UpdateVolumeLayer(UnityEngine.Rendering.Volume volume, System.Int32 prevLayer, System.Int32 newLayer) { }
        // RVA: 0x02FC58F0  token: 0x6000750
        private System.Void OverrideData(UnityEngine.Rendering.VolumeStack stack, System.Collections.Generic.List<UnityEngine.Rendering.VolumeComponent> components, System.Single interpFactor) { }
        // RVA: 0x031B7B60  token: 0x6000751
        private System.Void ReplaceData(UnityEngine.Rendering.VolumeStack stack) { }
        // RVA: 0x0B2E6E04  token: 0x6000752
        public System.Void CheckBaseTypes() { }
        // RVA: 0x0B2E6E9C  token: 0x6000753
        public System.Void CheckStack(UnityEngine.Rendering.VolumeStack stack) { }
        // RVA: 0x03D52330  token: 0x6000754
        private System.Boolean CheckUpdateRequired(UnityEngine.Rendering.VolumeStack stack) { }
        // RVA: 0x0B2E71F4  token: 0x6000755
        public System.Void Update(UnityEngine.Transform trigger, UnityEngine.LayerMask layerMask) { }
        // RVA: 0x02FC67F0  token: 0x6000756
        public System.Void Update(UnityEngine.Rendering.VolumeStack stack, UnityEngine.Transform trigger, UnityEngine.LayerMask layerMask) { }
        // RVA: 0x0B2E7064  token: 0x6000757
        public UnityEngine.Rendering.Volume[] GetVolumes(UnityEngine.LayerMask layerMask) { }
        // RVA: 0x036DB830  token: 0x6000758
        private System.Collections.Generic.List<UnityEngine.Rendering.Volume> GrabVolumes(UnityEngine.LayerMask mask) { }
        // RVA: 0x04331D70  token: 0x6000759
        private static System.Void SortByPriority(System.Collections.Generic.List<UnityEngine.Rendering.Volume> volumes) { }
        // RVA: 0x02FFF600  token: 0x600075A
        private static System.Boolean IsVolumeRenderedByCamera(UnityEngine.Rendering.Volume volume, UnityEngine.Camera camera) { }
        // RVA: 0x04B705A0  token: 0x600075B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000141  // size: 0x11
    public sealed struct VolumeIsolationScope : System.IDisposable
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000761
        public System.Void .ctor(System.Boolean unused) { }
        // RVA: 0x041E1670  token: 0x6000762
        private virtual System.Void System.IDisposable.Dispose() { }

    }

    // TypeToken: 0x2000142  // size: 0x18
    public abstract class VolumeParameter
    {
        // Fields
        public static System.String k_DebuggerDisplay;  // const
        public System.Boolean overrideState;  // 0x10

        // Methods
        // RVA: -1  // abstract  token: 0x6000763
        private virtual System.Void Interp(UnityEngine.Rendering.VolumeParameter from, UnityEngine.Rendering.VolumeParameter to, System.Single t) { }
        // RVA: -1  // generic def  token: 0x6000764
        public T GetValue() { }
        // RVA: -1  // abstract  token: 0x6000765
        public virtual System.Void SetValue(UnityEngine.Rendering.VolumeParameter parameter) { }
        // RVA: 0x041E1670  token: 0x6000766
        protected internal virtual System.Void OnEnable() { }
        // RVA: 0x041E1670  token: 0x6000767
        protected internal virtual System.Void OnDisable() { }
        // RVA: 0x041E1670  token: 0x6000768
        public virtual System.Void Release() { }
        // RVA: 0x041E1670  token: 0x6000769
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000143
    public class VolumeParameter`1 : UnityEngine.Rendering.VolumeParameter, System.IEquatable`1
    {
        // Fields
        protected T m_Value;  // 0x0

        // Properties
        T value { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600076C
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600076D
        protected System.Void .ctor(T value, System.Boolean overrideState) { }
        // RVA: -1  // not resolved  token: 0x600076E
        private virtual System.Void Interp(UnityEngine.Rendering.VolumeParameter from, UnityEngine.Rendering.VolumeParameter to, System.Single t) { }
        // RVA: -1  // not resolved  token: 0x600076F
        public virtual System.Void Interp(T from, T to, System.Single t) { }
        // RVA: -1  // not resolved  token: 0x6000770
        public System.Void Override(T x) { }
        // RVA: -1  // not resolved  token: 0x6000771
        public virtual System.Void SetValue(UnityEngine.Rendering.VolumeParameter parameter) { }
        // RVA: -1  // not resolved  token: 0x6000772
        public virtual System.Int32 GetHashCode() { }
        // RVA: -1  // not resolved  token: 0x6000773
        public virtual System.String ToString() { }
        // RVA: -1  // not resolved  token: 0x6000774
        public static System.Boolean op_Equality(UnityEngine.Rendering.VolumeParameter<T> lhs, T rhs) { }
        // RVA: -1  // not resolved  token: 0x6000775
        public static System.Boolean op_Inequality(UnityEngine.Rendering.VolumeParameter<T> lhs, T rhs) { }
        // RVA: -1  // not resolved  token: 0x6000776
        public virtual System.Boolean Equals(UnityEngine.Rendering.VolumeParameter<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000777
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: -1  // not resolved  token: 0x6000778
        public static T op_Explicit(UnityEngine.Rendering.VolumeParameter<T> prop) { }

    }

    // TypeToken: 0x2000144  // size: 0x20
    public class BoolParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x04DA0980  token: 0x6000779
        public System.Void .ctor(System.Boolean value, System.Boolean overrideState) { }

    }

    // TypeToken: 0x2000145  // size: 0x20
    public class LayerMaskParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x0B2E3F34  token: 0x600077A
        public System.Void .ctor(UnityEngine.LayerMask value, System.Boolean overrideState) { }

    }

    // TypeToken: 0x2000146  // size: 0x20
    public class IntParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x047A60C0  token: 0x600077B
        public System.Void .ctor(System.Int32 value, System.Boolean overrideState) { }
        // RVA: 0x04DB9830  token: 0x600077C
        public virtual System.Void Interp(System.Int32 from, System.Int32 to, System.Single t) { }

    }

    // TypeToken: 0x2000147  // size: 0x20
    public class NoInterpIntParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x0B2E59D0  token: 0x600077D
        public System.Void .ctor(System.Int32 value, System.Boolean overrideState) { }

    }

    // TypeToken: 0x2000148  // size: 0x28
    public class MinIntParameter : UnityEngine.Rendering.IntParameter
    {
        // Fields
        public System.Int32 min;  // 0x20

        // Properties
        System.Int32 value { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x0B2E55A0 */ }

        // Methods
        // RVA: 0x0B2E4620  token: 0x6000780
        public System.Void .ctor(System.Int32 value, System.Int32 min, System.Boolean overrideState) { }

    }

    // TypeToken: 0x2000149  // size: 0x28
    public class NoInterpMinIntParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Fields
        public System.Int32 min;  // 0x20

        // Properties
        System.Int32 value { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x0B2E55A0 */ }

        // Methods
        // RVA: 0x0B2E5B40  token: 0x6000783
        public System.Void .ctor(System.Int32 value, System.Int32 min, System.Boolean overrideState) { }

    }

    // TypeToken: 0x200014A  // size: 0x28
    public class MaxIntParameter : UnityEngine.Rendering.IntParameter
    {
        // Fields
        public System.Int32 max;  // 0x20

        // Properties
        System.Int32 value { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x0B2E4650 */ }

        // Methods
        // RVA: 0x0B2E4620  token: 0x6000786
        public System.Void .ctor(System.Int32 value, System.Int32 max, System.Boolean overrideState) { }

    }

    // TypeToken: 0x200014B  // size: 0x28
    public class NoInterpMaxIntParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Fields
        public System.Int32 max;  // 0x20

        // Properties
        System.Int32 value { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x0B2E4650 */ }

        // Methods
        // RVA: 0x0B2E5A84  token: 0x6000789
        public System.Void .ctor(System.Int32 value, System.Int32 max, System.Boolean overrideState) { }

    }

    // TypeToken: 0x200014C  // size: 0x28
    public class ClampedIntParameter : UnityEngine.Rendering.IntParameter
    {
        // Fields
        public System.Int32 min;  // 0x20
        public System.Int32 max;  // 0x24

        // Properties
        System.Int32 value { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x0B2DCA70 */ }

        // Methods
        // RVA: 0x05397614  token: 0x600078C
        public System.Void .ctor(System.Int32 value, System.Int32 min, System.Int32 max, System.Boolean overrideState) { }

    }

    // TypeToken: 0x200014D  // size: 0x28
    public class NoInterpClampedIntParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Fields
        public System.Int32 min;  // 0x20
        public System.Int32 max;  // 0x24

        // Properties
        System.Int32 value { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x0B2DCA70 */ }

        // Methods
        // RVA: 0x0B2E56CC  token: 0x600078F
        public System.Void .ctor(System.Int32 value, System.Int32 min, System.Int32 max, System.Boolean overrideState) { }

    }

    // TypeToken: 0x200014E  // size: 0x20
    public class FloatParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x04DA09C0  token: 0x6000790
        public System.Void .ctor(System.Single value, System.Boolean overrideState) { }
        // RVA: 0x04DB9770  token: 0x6000791
        public virtual System.Void Interp(System.Single from, System.Single to, System.Single t) { }

    }

    // TypeToken: 0x200014F  // size: 0x20
    public class NoInterpFloatParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x0B2E5904  token: 0x6000792
        public System.Void .ctor(System.Single value, System.Boolean overrideState) { }

    }

    // TypeToken: 0x2000150  // size: 0x28
    public class MinFloatParameter : UnityEngine.Rendering.FloatParameter
    {
        // Fields
        public System.Single min;  // 0x20

        // Properties
        System.Single value { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x0B2E5590 */ }

        // Methods
        // RVA: 0x04DB9860  token: 0x6000795
        public System.Void .ctor(System.Single value, System.Single min, System.Boolean overrideState) { }

    }

    // TypeToken: 0x2000151  // size: 0x28
    public class NoInterpMinFloatParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Fields
        public System.Single min;  // 0x20

        // Properties
        System.Single value { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x0B2E5590 */ }

        // Methods
        // RVA: 0x0B2E5AE0  token: 0x6000798
        public System.Void .ctor(System.Single value, System.Single min, System.Boolean overrideState) { }

    }

    // TypeToken: 0x2000152  // size: 0x28
    public class MaxFloatParameter : UnityEngine.Rendering.FloatParameter
    {
        // Fields
        public System.Single max;  // 0x20

        // Properties
        System.Single value { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x0B2E4610 */ }

        // Methods
        // RVA: 0x04DB9860  token: 0x600079B
        public System.Void .ctor(System.Single value, System.Single max, System.Boolean overrideState) { }

    }

    // TypeToken: 0x2000153  // size: 0x28
    public class NoInterpMaxFloatParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Fields
        public System.Single max;  // 0x20

        // Properties
        System.Single value { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x0B2E4610 */ }

        // Methods
        // RVA: 0x0B2E5A24  token: 0x600079E
        public System.Void .ctor(System.Single value, System.Single max, System.Boolean overrideState) { }

    }

    // TypeToken: 0x2000154  // size: 0x30
    public class ClampedFloatParameter : UnityEngine.Rendering.FloatParameter
    {
        // Fields
        public System.Single min;  // 0x20
        public System.Single max;  // 0x24
        public System.Single sliderExponent;  // 0x28

        // Properties
        System.Single value { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x03EE0E50 */ }

        // Methods
        // RVA: 0x04DB9050  token: 0x60007A1
        public System.Void .ctor(System.Single value, System.Single min, System.Single max, System.Single sliderExponent, System.Boolean overrideState) { }
        // RVA: 0x04DB9080  token: 0x60007A2
        public System.Void .ctor(System.Single value, System.Single min, System.Single max, System.Boolean overrideState) { }

    }

    // TypeToken: 0x2000155  // size: 0x30
    public class NoInterpClampedFloatParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Fields
        public System.Single min;  // 0x20
        public System.Single max;  // 0x24
        public System.Single sliderExponent;  // 0x28

        // Properties
        System.Single value { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x0B2E56A4 */ }

        // Methods
        // RVA: 0x0B2E5624  token: 0x60007A5
        public System.Void .ctor(System.Single value, System.Single min, System.Single max, System.Single sliderExponent, System.Boolean overrideState) { }
        // RVA: 0x0B2E55AC  token: 0x60007A6
        public System.Void .ctor(System.Single value, System.Single min, System.Single max, System.Boolean overrideState) { }

    }

    // TypeToken: 0x2000156  // size: 0x30
    public class UnclampedFloatParameter : UnityEngine.Rendering.FloatParameter
    {
        // Fields
        public System.Single min;  // 0x20
        public System.Single max;  // 0x24
        public System.Single sliderExponent;  // 0x28

        // Properties
        System.Single value { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x04D86390 */ }

        // Methods
        // RVA: 0x04DB9050  token: 0x60007A9
        public System.Void .ctor(System.Single value, System.Single min, System.Single max, System.Single sliderExponent, System.Boolean overrideState) { }
        // RVA: 0x04DB9080  token: 0x60007AA
        public System.Void .ctor(System.Single value, System.Single min, System.Single max, System.Boolean overrideState) { }

    }

    // TypeToken: 0x2000157  // size: 0x30
    public class NoInterpUnclampedFloatParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Fields
        public System.Single min;  // 0x20
        public System.Single max;  // 0x24
        public System.Single sliderExponent;  // 0x28

        // Properties
        System.Single value { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x04D86390 */ }

        // Methods
        // RVA: 0x0B2E5DC4  token: 0x60007AD
        public System.Void .ctor(System.Single value, System.Single min, System.Single max, System.Single sliderExponent, System.Boolean overrideState) { }
        // RVA: 0x0B2E5E44  token: 0x60007AE
        public System.Void .ctor(System.Single value, System.Single min, System.Single max, System.Boolean overrideState) { }

    }

    // TypeToken: 0x2000158  // size: 0x28
    public class FloatRangeParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Fields
        public System.Single min;  // 0x20
        public System.Single max;  // 0x24

        // Properties
        UnityEngine.Vector2 value { get; /* RVA: 0x04D92340 */ set; /* RVA: 0x0B2E365C */ }

        // Methods
        // RVA: 0x043D42E0  token: 0x60007B1
        public System.Void .ctor(UnityEngine.Vector2 value, System.Single min, System.Single max, System.Boolean overrideState) { }
        // RVA: 0x04DB9790  token: 0x60007B2
        public virtual System.Void Interp(UnityEngine.Vector2 from, UnityEngine.Vector2 to, System.Single t) { }

    }

    // TypeToken: 0x2000159  // size: 0x28
    public class NoInterpFloatRangeParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Fields
        public System.Single min;  // 0x20
        public System.Single max;  // 0x24

        // Properties
        UnityEngine.Vector2 value { get; /* RVA: 0x04D92340 */ set; /* RVA: 0x0B2E365C */ }

        // Methods
        // RVA: 0x0B2E5960  token: 0x60007B5
        public System.Void .ctor(UnityEngine.Vector2 value, System.Single min, System.Single max, System.Boolean overrideState) { }

    }

    // TypeToken: 0x200015A  // size: 0x30
    public class ColorParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Fields
        public System.Boolean hdr;  // 0x28
        public System.Boolean showAlpha;  // 0x29
        public System.Boolean showEyeDropper;  // 0x2a

        // Methods
        // RVA: 0x04DB9130  token: 0x60007B6
        public System.Void .ctor(UnityEngine.Color value, System.Boolean overrideState) { }
        // RVA: 0x04DB9110  token: 0x60007B7
        public System.Void .ctor(UnityEngine.Color value, System.Boolean hdr, System.Boolean showAlpha, System.Boolean showEyeDropper, System.Boolean overrideState) { }
        // RVA: 0x04DB90A0  token: 0x60007B8
        public virtual System.Void Interp(UnityEngine.Color from, UnityEngine.Color to, System.Single t) { }

    }

    // TypeToken: 0x200015B  // size: 0x30
    public class NoInterpColorParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Fields
        public System.Boolean hdr;  // 0x28
        public System.Boolean showAlpha;  // 0x29
        public System.Boolean showEyeDropper;  // 0x2a

        // Methods
        // RVA: 0x0B2E572C  token: 0x60007B9
        public System.Void .ctor(UnityEngine.Color value, System.Boolean overrideState) { }
        // RVA: 0x0B2E5784  token: 0x60007BA
        public System.Void .ctor(UnityEngine.Color value, System.Boolean hdr, System.Boolean showAlpha, System.Boolean showEyeDropper, System.Boolean overrideState) { }

    }

    // TypeToken: 0x200015C  // size: 0x20
    public class Vector2Parameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x04DA09D0  token: 0x60007BB
        public System.Void .ctor(UnityEngine.Vector2 value, System.Boolean overrideState) { }
        // RVA: 0x04DB9790  token: 0x60007BC
        public virtual System.Void Interp(UnityEngine.Vector2 from, UnityEngine.Vector2 to, System.Single t) { }

    }

    // TypeToken: 0x200015D  // size: 0x20
    public class NoInterpVector2Parameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x0B2E5EBC  token: 0x60007BD
        public System.Void .ctor(UnityEngine.Vector2 value, System.Boolean overrideState) { }

    }

    // TypeToken: 0x200015E  // size: 0x28
    public class Vector3Parameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x04416260  token: 0x60007BE
        public System.Void .ctor(UnityEngine.Vector3 value, System.Boolean overrideState) { }
        // RVA: 0x04DB9880  token: 0x60007BF
        public virtual System.Void Interp(UnityEngine.Vector3 from, UnityEngine.Vector3 to, System.Single t) { }

    }

    // TypeToken: 0x200015F  // size: 0x28
    public class NoInterpVector3Parameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x0B2E5F18  token: 0x60007C0
        public System.Void .ctor(UnityEngine.Vector3 value, System.Boolean overrideState) { }

    }

    // TypeToken: 0x2000160  // size: 0x28
    public class Vector4Parameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x04DA09E0  token: 0x60007C1
        public System.Void .ctor(UnityEngine.Vector4 value, System.Boolean overrideState) { }
        // RVA: 0x04DB90A0  token: 0x60007C2
        public virtual System.Void Interp(UnityEngine.Vector4 from, UnityEngine.Vector4 to, System.Single t) { }

    }

    // TypeToken: 0x2000161  // size: 0x28
    public class NoInterpVector4Parameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x0B2E5F70  token: 0x60007C3
        public System.Void .ctor(UnityEngine.Vector4 value, System.Boolean overrideState) { }

    }

    // TypeToken: 0x2000162  // size: 0x28
    public class TextureParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Fields
        public UnityEngine.Rendering.TextureDimension dimension;  // 0x20

        // Methods
        // RVA: 0x05397BEC  token: 0x60007C4
        public System.Void .ctor(UnityEngine.Texture value, System.Boolean overrideState) { }
        // RVA: 0x0402BF60  token: 0x60007C5
        public System.Void .ctor(UnityEngine.Texture value, UnityEngine.Rendering.TextureDimension dimension, System.Boolean overrideState) { }
        // RVA: 0x0B2E65CC  token: 0x60007C6
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000163  // size: 0x20
    public class NoInterpTextureParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x0B2E5D6C  token: 0x60007C7
        public System.Void .ctor(UnityEngine.Texture value, System.Boolean overrideState) { }
        // RVA: 0x0B2E5CAC  token: 0x60007C8
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000164  // size: 0x20
    public class Texture2DParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x0402D9F0  token: 0x60007C9
        public System.Void .ctor(UnityEngine.Texture value, System.Boolean overrideState) { }
        // RVA: 0x0B2E62C8  token: 0x60007CA
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000165  // size: 0x20
    public class Texture3DParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x0402C1F0  token: 0x60007CB
        public System.Void .ctor(UnityEngine.Texture value, System.Boolean overrideState) { }
        // RVA: 0x0B2E6388  token: 0x60007CC
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000166  // size: 0x20
    public class RenderTextureParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x0B2E60CC  token: 0x60007CD
        public System.Void .ctor(UnityEngine.RenderTexture value, System.Boolean overrideState) { }
        // RVA: 0x0B2E6014  token: 0x60007CE
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000167  // size: 0x20
    public class NoInterpRenderTextureParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x0B2E5C54  token: 0x60007CF
        public System.Void .ctor(UnityEngine.RenderTexture value, System.Boolean overrideState) { }
        // RVA: 0x0B2E5B9C  token: 0x60007D0
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000168  // size: 0x20
    public class CubemapParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x0402D990  token: 0x60007D1
        public System.Void .ctor(UnityEngine.Texture value, System.Boolean overrideState) { }
        // RVA: 0x0B2E0D60  token: 0x60007D2
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000169  // size: 0x20
    public class NoInterpCubemapParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x0B2E58AC  token: 0x60007D3
        public System.Void .ctor(UnityEngine.Cubemap value, System.Boolean overrideState) { }
        // RVA: 0x0B2E57F4  token: 0x60007D4
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x200016A  // size: 0x20
    public class AnimationCurveParameter : UnityEngine.Rendering.VolumeParameter`1
    {
        // Methods
        // RVA: 0x043D4360  token: 0x60007D5
        public System.Void .ctor(UnityEngine.AnimationCurve value, System.Boolean overrideState) { }

    }

    // TypeToken: 0x200016B  // size: 0x28
    public sealed class VolumeProfile : UnityEngine.ScriptableObject
    {
        // Fields
        public System.Collections.Generic.List<UnityEngine.Rendering.VolumeComponent> components;  // 0x18
        public System.Boolean isDirty;  // 0x20

        // Methods
        // RVA: 0x047C4430  token: 0x60007D6
        private System.Void OnEnable() { }
        // RVA: 0x0B2E74F0  token: 0x60007D7
        private System.Void OnDisable() { }
        // RVA: 0x04D8F6A0  token: 0x60007D8
        public System.Void Reset() { }
        // RVA: -1  // generic def  token: 0x60007D9
        public T Add(System.Boolean overrides) { }
        // RVA: 0x02FC4400  token: 0x60007DA
        public UnityEngine.Rendering.VolumeComponent Add(System.Type type, System.Boolean overrides) { }
        // RVA: -1  // generic def  token: 0x60007DB
        public System.Void Remove() { }
        // RVA: 0x0B2E75C0  token: 0x60007DC
        public System.Void Remove(System.Type type) { }
        // RVA: -1  // generic def  token: 0x60007DD
        public System.Boolean Has() { }
        // RVA: 0x02FC5040  token: 0x60007DE
        public System.Boolean Has(System.Type type) { }
        // RVA: 0x0B2E73A4  token: 0x60007DF
        public System.Boolean HasSubclassOf(System.Type type) { }
        // RVA: -1  // generic def  token: 0x60007E0
        public System.Boolean TryGet(T& component) { }
        // RVA: -1  // generic def  token: 0x60007E1
        public System.Boolean TryGet(System.Type type, T& component) { }
        // RVA: -1  // generic def  token: 0x60007E2
        public System.Boolean TryGetSubclassOf(System.Type type, T& component) { }
        // RVA: -1  // generic def  token: 0x60007E3
        public System.Boolean TryGetAllSubclassOf(System.Type type, System.Collections.Generic.List<T> result) { }
        // RVA: 0x0B2E7310  token: 0x60007E4
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B2E726C  token: 0x60007E5
        private System.Int32 GetComponentListHashCode() { }
        // RVA: 0x0B2E769C  token: 0x60007E6
        private System.Void Sanitize() { }
        // RVA: 0x046FE410  token: 0x60007E7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200016D  // size: 0x28
    public sealed class VolumeStack : System.IDisposable
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<System.Type,UnityEngine.Rendering.VolumeComponent> components;  // 0x10
        public System.ValueTuple<UnityEngine.Rendering.VolumeParameter,UnityEngine.Rendering.VolumeParameter>[] defaultParameters;  // 0x18
        private System.Boolean requiresReset;  // 0x20

        // Methods
        // RVA: 0x02FC4540  token: 0x60007EB
        private System.Void .ctor() { }
        // RVA: 0x02FC4AB0  token: 0x60007EC
        private System.Void Reload(System.Collections.Generic.List<UnityEngine.Rendering.VolumeComponent> componentDefaultStates) { }
        // RVA: -1  // generic def  token: 0x60007ED
        public T GetComponent() { }
        // RVA: 0x02FC5A60  token: 0x60007EE
        public UnityEngine.Rendering.VolumeComponent GetComponent(System.Type type) { }
        // RVA: 0x037DCDB0  token: 0x60007EF
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x200016E  // size: 0x10
    public static class XRGraphicsAutomatedTests
    {
        // Fields
        private static readonly System.Boolean <enabled>k__BackingField;  // static @ 0x0
        public static System.Boolean running;  // static @ 0x1

        // Properties
        System.Boolean activatedFromCommandLine { get; /* RVA: 0x012081B0 */ }
        System.Boolean enabled { get; /* RVA: 0x0B2E7A54 */ }

        // Methods
        // RVA: 0x0B2E7A0C  token: 0x60007F2
        private static System.Void .cctor() { }

    }

}

namespace UnityEngine.Rendering.LookDev
{

    // TypeToken: 0x200016F
    public interface IDataProvider
    {
        // Properties
        System.Collections.Generic.IEnumerable<System.String> supportedDebugModes { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60007F3
        public virtual System.Void FirstInitScene(UnityEngine.Rendering.LookDev.StageRuntimeInterface stage) { }
        // RVA: -1  // abstract  token: 0x60007F4
        public virtual System.Void UpdateSky(UnityEngine.Camera camera, UnityEngine.Rendering.LookDev.Sky sky, UnityEngine.Rendering.LookDev.StageRuntimeInterface stage) { }
        // RVA: -1  // abstract  token: 0x60007F6
        public virtual System.Void UpdateDebugMode(System.Int32 debugIndex) { }
        // RVA: -1  // abstract  token: 0x60007F7
        public virtual System.Void GetShadowMask(UnityEngine.RenderTexture& output, UnityEngine.Rendering.LookDev.StageRuntimeInterface stage) { }
        // RVA: -1  // abstract  token: 0x60007F8
        public virtual System.Void OnBeginRendering(UnityEngine.Rendering.LookDev.StageRuntimeInterface stage) { }
        // RVA: -1  // abstract  token: 0x60007F9
        public virtual System.Void OnEndRendering(UnityEngine.Rendering.LookDev.StageRuntimeInterface stage) { }
        // RVA: -1  // abstract  token: 0x60007FA
        public virtual System.Void Cleanup(UnityEngine.Rendering.LookDev.StageRuntimeInterface SRI) { }

    }

    // TypeToken: 0x2000170  // size: 0x20
    public sealed struct Sky
    {
        // Fields
        public UnityEngine.Cubemap cubemap;  // 0x10
        public System.Single longitudeOffset;  // 0x18
        public System.Single exposure;  // 0x1c

    }

    // TypeToken: 0x2000171  // size: 0x30
    public class StageRuntimeInterface
    {
        // Fields
        private System.Func<System.Boolean,UnityEngine.GameObject> m_AddGameObject;  // 0x10
        private System.Func<UnityEngine.Camera> m_GetCamera;  // 0x18
        private System.Func<UnityEngine.Light> m_GetSunLight;  // 0x20
        public System.Object SRPData;  // 0x28

        // Properties
        UnityEngine.Camera camera { get; /* RVA: 0x0B2E6288 */ }
        UnityEngine.Light sunLight { get; /* RVA: 0x0B2E62A8 */ }

        // Methods
        // RVA: 0x031D14F0  token: 0x60007FB
        public System.Void .ctor(System.Func<System.Boolean,UnityEngine.GameObject> AddGameObject, System.Func<UnityEngine.Camera> GetCamera, System.Func<UnityEngine.Light> GetSunLight) { }
        // RVA: 0x0B2E6264  token: 0x60007FC
        public UnityEngine.GameObject AddGameObject(System.Boolean persistent) { }

    }

}

namespace UnityEngine.Rendering.UI
{

    // TypeToken: 0x2000172  // size: 0x80
    public class DebugUIHandlerBitField : UnityEngine.Rendering.UI.DebugUIHandlerWidget
    {
        // Fields
        public UnityEngine.UI.Text nameLabel;  // 0x58
        public UnityEngine.Rendering.UI.UIFoldout valueToggle;  // 0x60
        public System.Collections.Generic.List<UnityEngine.Rendering.UI.DebugUIHandlerIndirectToggle> toggles;  // 0x68
        private UnityEngine.Rendering.DebugUI.BitField m_Field;  // 0x70
        private UnityEngine.Rendering.UI.DebugUIHandlerContainer m_Container;  // 0x78

        // Methods
        // RVA: 0x0B2E13C0  token: 0x60007FF
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x0B2E0E20  token: 0x6000800
        private System.Boolean GetValue(System.Int32 index) { }
        // RVA: 0x0B2E10F0  token: 0x6000801
        private System.Void SetValue(System.Int32 index, System.Boolean value) { }
        // RVA: 0x0B2E1014  token: 0x6000802
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x0B2E0FBC  token: 0x6000803
        public virtual System.Void OnDeselection() { }
        // RVA: 0x0B2E0FEC  token: 0x6000804
        public virtual System.Void OnIncrement(System.Boolean fast) { }
        // RVA: 0x0B2E0F98  token: 0x6000805
        public virtual System.Void OnDecrement(System.Boolean fast) { }
        // RVA: 0x0B2E0F68  token: 0x6000806
        public virtual System.Void OnAction() { }
        // RVA: 0x0B2E0EBC  token: 0x6000807
        public virtual UnityEngine.Rendering.UI.DebugUIHandlerWidget Next() { }
        // RVA: 0x0B2E170C  token: 0x6000808
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000173  // size: 0x68
    public class DebugUIHandlerButton : UnityEngine.Rendering.UI.DebugUIHandlerWidget
    {
        // Fields
        public UnityEngine.UI.Text nameLabel;  // 0x58
        private UnityEngine.Rendering.DebugUI.Button m_Field;  // 0x60

        // Methods
        // RVA: 0x0B2E1774  token: 0x6000809
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x0B2E1740  token: 0x600080A
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x0B2E0FBC  token: 0x600080B
        public virtual System.Void OnDeselection() { }
        // RVA: 0x0B2E1714  token: 0x600080C
        public virtual System.Void OnAction() { }
        // RVA: 0x0B2E170C  token: 0x600080D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000174  // size: 0x20
    public class DebugUIPrefabBundle
    {
        // Fields
        public System.String type;  // 0x10
        public UnityEngine.RectTransform prefab;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600080E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000175  // size: 0x58
    public class DebugUIHandlerCanvas : UnityEngine.MonoBehaviour
    {
        // Fields
        private System.Int32 m_DebugTreeState;  // 0x18
        private System.Collections.Generic.Dictionary<System.Type,UnityEngine.Transform> m_PrefabsMap;  // 0x20
        public UnityEngine.Transform panelPrefab;  // 0x28
        public System.Collections.Generic.List<UnityEngine.Rendering.UI.DebugUIPrefabBundle> prefabs;  // 0x30
        private System.Collections.Generic.List<UnityEngine.Rendering.UI.DebugUIHandlerPanel> m_UIPanels;  // 0x38
        private System.Int32 m_SelectedPanel;  // 0x40
        private UnityEngine.Rendering.UI.DebugUIHandlerWidget m_SelectedWidget;  // 0x48
        private System.String m_CurrentQueryPath;  // 0x50

        // Methods
        // RVA: 0x0B2E2020  token: 0x600080F
        private System.Void OnEnable() { }
        // RVA: 0x0B2E3114  token: 0x6000810
        private System.Void Update() { }
        // RVA: 0x04D87910  token: 0x6000811
        private System.Void RequestHierarchyReset() { }
        // RVA: 0x0B2E288C  token: 0x6000812
        private System.Void ResetAllHierarchy() { }
        // RVA: 0x0B2E2170  token: 0x6000813
        private System.Void Rebuild() { }
        // RVA: 0x0B2E2D40  token: 0x6000814
        private System.Void Traverse(UnityEngine.Rendering.DebugUI.IContainer container, UnityEngine.Transform parentTransform, UnityEngine.Rendering.UI.DebugUIHandlerWidget parentUIHandler, UnityEngine.Rendering.UI.DebugUIHandlerWidget& selectedHandler) { }
        // RVA: 0x0B2E1CE0  token: 0x6000815
        private UnityEngine.Rendering.UI.DebugUIHandlerWidget GetWidgetFromPath(System.String queryPath) { }
        // RVA: 0x0B2E17F8  token: 0x6000816
        private System.Void ActivatePanel(System.Int32 index, UnityEngine.Rendering.UI.DebugUIHandlerWidget selectedWidget) { }
        // RVA: 0x0B2E1B54  token: 0x6000817
        private System.Void ChangeSelection(UnityEngine.Rendering.UI.DebugUIHandlerWidget widget, System.Boolean fromNext) { }
        // RVA: 0x0B2E2B64  token: 0x6000818
        private System.Void SelectPreviousItem() { }
        // RVA: 0x0B2E2AF8  token: 0x6000819
        private System.Void SelectNextPanel() { }
        // RVA: 0x0B2E2C00  token: 0x600081A
        private System.Void SelectPreviousPanel() { }
        // RVA: 0x0B2E2A44  token: 0x600081B
        private System.Void SelectNextItem() { }
        // RVA: 0x0B2E1A70  token: 0x600081C
        private System.Void ChangeSelectionValue(System.Single multiplier) { }
        // RVA: 0x0B2E1A00  token: 0x600081D
        private System.Void ActivateSelection() { }
        // RVA: 0x0B2E1E0C  token: 0x600081E
        private System.Void HandleInput() { }
        // RVA: 0x0B2E2C70  token: 0x600081F
        private System.Void SetScrollTarget(UnityEngine.Rendering.UI.DebugUIHandlerWidget widget) { }
        // RVA: 0x05393520  token: 0x6000820
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000178  // size: 0xA0
    public class DebugUIHandlerColor : UnityEngine.Rendering.UI.DebugUIHandlerWidget
    {
        // Fields
        public UnityEngine.UI.Text nameLabel;  // 0x58
        public UnityEngine.Rendering.UI.UIFoldout valueToggle;  // 0x60
        public UnityEngine.UI.Image colorImage;  // 0x68
        public UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField fieldR;  // 0x70
        public UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField fieldG;  // 0x78
        public UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField fieldB;  // 0x80
        public UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField fieldA;  // 0x88
        private UnityEngine.Rendering.DebugUI.ColorField m_Field;  // 0x90
        private UnityEngine.Rendering.UI.DebugUIHandlerContainer m_Container;  // 0x98

        // Methods
        // RVA: 0x0B2E7DE8  token: 0x6000827
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x0B2E7CD4  token: 0x6000828
        private System.Void SetValue(System.Single x, System.Boolean r, System.Boolean g, System.Boolean b, System.Boolean a) { }
        // RVA: 0x0B2E82D4  token: 0x6000829
        private System.Void SetupSettings(UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField field) { }
        // RVA: 0x0B2E7BF0  token: 0x600082A
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x0B2E0FBC  token: 0x600082B
        public virtual System.Void OnDeselection() { }
        // RVA: 0x0B2E0FEC  token: 0x600082C
        public virtual System.Void OnIncrement(System.Boolean fast) { }
        // RVA: 0x0B2E0F98  token: 0x600082D
        public virtual System.Void OnDecrement(System.Boolean fast) { }
        // RVA: 0x0B2E0F68  token: 0x600082E
        public virtual System.Void OnAction() { }
        // RVA: 0x0B2E8664  token: 0x600082F
        private System.Void UpdateColor() { }
        // RVA: 0x0B2E7B3C  token: 0x6000830
        public virtual UnityEngine.Rendering.UI.DebugUIHandlerWidget Next() { }
        // RVA: 0x0B2E170C  token: 0x6000831
        public System.Void .ctor() { }
        // RVA: 0x0B2E8410  token: 0x6000832
        private System.Single <SetWidget>b__9_0() { }
        // RVA: 0x0B2E8464  token: 0x6000833
        private System.Void <SetWidget>b__9_1(System.Single x) { }
        // RVA: 0x0B2E848C  token: 0x6000834
        private System.Single <SetWidget>b__9_2() { }
        // RVA: 0x0B2E84E0  token: 0x6000835
        private System.Void <SetWidget>b__9_3(System.Single x) { }
        // RVA: 0x0B2E8508  token: 0x6000836
        private System.Single <SetWidget>b__9_4() { }
        // RVA: 0x0B2E855C  token: 0x6000837
        private System.Void <SetWidget>b__9_5(System.Single x) { }
        // RVA: 0x0B2E8584  token: 0x6000838
        private System.Single <SetWidget>b__9_6() { }
        // RVA: 0x0B2E85D8  token: 0x6000839
        private System.Void <SetWidget>b__9_7(System.Single x) { }
        // RVA: 0x0B2E8600  token: 0x600083A
        private System.Single <SetupSettings>b__11_0() { }
        // RVA: 0x0B2E8620  token: 0x600083B
        private System.Single <SetupSettings>b__11_1() { }
        // RVA: 0x0B2E8640  token: 0x600083C
        private System.Single <SetupSettings>b__11_2() { }

    }

    // TypeToken: 0x2000179  // size: 0x20
    public class DebugUIHandlerContainer : UnityEngine.MonoBehaviour
    {
        // Fields
        public UnityEngine.RectTransform contentHolder;  // 0x18

        // Methods
        // RVA: 0x0B2E8964  token: 0x600083D
        private UnityEngine.Rendering.UI.DebugUIHandlerWidget GetFirstItem() { }
        // RVA: 0x0B2E89E4  token: 0x600083E
        private UnityEngine.Rendering.UI.DebugUIHandlerWidget GetLastItem() { }
        // RVA: 0x0B2E8A68  token: 0x600083F
        private System.Boolean IsDirectChild(UnityEngine.Rendering.UI.DebugUIHandlerWidget widget) { }
        // RVA: 0x0B2E8710  token: 0x6000840
        private System.Collections.Generic.List<UnityEngine.Rendering.UI.DebugUIHandlerWidget> GetActiveChildren() { }
        // RVA: 0x05393520  token: 0x6000841
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200017B  // size: 0x80
    public class DebugUIHandlerEnumField : UnityEngine.Rendering.UI.DebugUIHandlerWidget
    {
        // Fields
        public UnityEngine.UI.Text nextButtonText;  // 0x58
        public UnityEngine.UI.Text previousButtonText;  // 0x60
        public UnityEngine.UI.Text nameLabel;  // 0x68
        public UnityEngine.UI.Text valueLabel;  // 0x70
        protected internal UnityEngine.Rendering.DebugUI.EnumField m_Field;  // 0x78

        // Methods
        // RVA: 0x0B2E906C  token: 0x6000844
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x0B2E8F60  token: 0x6000845
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x0B2E8D0C  token: 0x6000846
        public virtual System.Void OnDeselection() { }
        // RVA: 0x0B2E8B60  token: 0x6000847
        public virtual System.Void OnAction() { }
        // RVA: 0x0B2E8E14  token: 0x6000848
        public virtual System.Void OnIncrement(System.Boolean fast) { }
        // RVA: 0x0B2E8B70  token: 0x6000849
        public virtual System.Void OnDecrement(System.Boolean fast) { }
        // RVA: 0x0B2E90FC  token: 0x600084A
        protected virtual System.Void UpdateValueLabel() { }
        // RVA: 0x0B2E170C  token: 0x600084B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200017C  // size: 0x88
    public class DebugUIHandlerEnumHistory : UnityEngine.Rendering.UI.DebugUIHandlerEnumField
    {
        // Fields
        private UnityEngine.UI.Text[] historyValues;  // 0x80
        private static System.Single xDecal;  // const

        // Methods
        // RVA: 0x0B2E9214  token: 0x600084C
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x0B2E9448  token: 0x600084D
        protected virtual System.Void UpdateValueLabel() { }
        // RVA: 0x0B2E91B8  token: 0x600084E
        private System.Collections.IEnumerator RefreshAfterSanitization() { }
        // RVA: 0x0B2E170C  token: 0x600084F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200017E  // size: 0x70
    public class DebugUIHandlerFloatField : UnityEngine.Rendering.UI.DebugUIHandlerWidget
    {
        // Fields
        public UnityEngine.UI.Text nameLabel;  // 0x58
        public UnityEngine.UI.Text valueLabel;  // 0x60
        private UnityEngine.Rendering.DebugUI.FloatField m_Field;  // 0x68

        // Methods
        // RVA: 0x0B2E97AC  token: 0x6000856
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x0B2E9750  token: 0x6000857
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x0B2E96E4  token: 0x6000858
        public virtual System.Void OnDeselection() { }
        // RVA: 0x0B2E9740  token: 0x6000859
        public virtual System.Void OnIncrement(System.Boolean fast) { }
        // RVA: 0x0B2E96D4  token: 0x600085A
        public virtual System.Void OnDecrement(System.Boolean fast) { }
        // RVA: 0x0B2E9620  token: 0x600085B
        private System.Void ChangeValue(System.Boolean fast, System.Single multiplier) { }
        // RVA: 0x0B2E983C  token: 0x600085C
        private System.Void UpdateValueLabel() { }
        // RVA: 0x0B2E170C  token: 0x600085D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200017F  // size: 0x78
    public class DebugUIHandlerFoldout : UnityEngine.Rendering.UI.DebugUIHandlerWidget
    {
        // Fields
        public UnityEngine.UI.Text nameLabel;  // 0x58
        public UnityEngine.Rendering.UI.UIFoldout valueToggle;  // 0x60
        private UnityEngine.Rendering.DebugUI.Foldout m_Field;  // 0x68
        private UnityEngine.Rendering.UI.DebugUIHandlerContainer m_Container;  // 0x70
        private static System.Single xDecal;  // const
        private static System.Single xDecalInit;  // const

        // Methods
        // RVA: 0x0B2E9AE8  token: 0x600085E
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x0B2E9A0C  token: 0x600085F
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x0B2E0FBC  token: 0x6000860
        public virtual System.Void OnDeselection() { }
        // RVA: 0x0B2E99E8  token: 0x6000861
        public virtual System.Void OnIncrement(System.Boolean fast) { }
        // RVA: 0x0B2E99C4  token: 0x6000862
        public virtual System.Void OnDecrement(System.Boolean fast) { }
        // RVA: 0x0B2E9998  token: 0x6000863
        public virtual System.Void OnAction() { }
        // RVA: 0x0B2E9EC4  token: 0x6000864
        private System.Void UpdateValue() { }
        // RVA: 0x0B2E98EC  token: 0x6000865
        public virtual UnityEngine.Rendering.UI.DebugUIHandlerWidget Next() { }
        // RVA: 0x0B2E170C  token: 0x6000866
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000180  // size: 0x78
    public class DebugUIHandlerGroup : UnityEngine.Rendering.UI.DebugUIHandlerWidget
    {
        // Fields
        public UnityEngine.UI.Text nameLabel;  // 0x58
        public UnityEngine.Transform header;  // 0x60
        private UnityEngine.Rendering.DebugUI.Container m_Field;  // 0x68
        private UnityEngine.Rendering.UI.DebugUIHandlerContainer m_Container;  // 0x70

        // Methods
        // RVA: 0x0B2EA040  token: 0x6000867
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x0B2E9F94  token: 0x6000868
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x0B2E9EF8  token: 0x6000869
        public virtual UnityEngine.Rendering.UI.DebugUIHandlerWidget Next() { }
        // RVA: 0x0B2E170C  token: 0x600086A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000181  // size: 0x60
    public class DebugUIHandlerHBox : UnityEngine.Rendering.UI.DebugUIHandlerWidget
    {
        // Fields
        private UnityEngine.Rendering.UI.DebugUIHandlerContainer m_Container;  // 0x58

        // Methods
        // RVA: 0x0B2EA26C  token: 0x600086B
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x0B2EA1C0  token: 0x600086C
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x0B2EA124  token: 0x600086D
        public virtual UnityEngine.Rendering.UI.DebugUIHandlerWidget Next() { }
        // RVA: 0x0B2E170C  token: 0x600086E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000182  // size: 0x90
    public class DebugUIHandlerIndirectFloatField : UnityEngine.Rendering.UI.DebugUIHandlerWidget
    {
        // Fields
        public UnityEngine.UI.Text nameLabel;  // 0x58
        public UnityEngine.UI.Text valueLabel;  // 0x60
        public System.Func<System.Single> getter;  // 0x68
        public System.Action<System.Single> setter;  // 0x70
        public System.Func<System.Single> incStepGetter;  // 0x78
        public System.Func<System.Single> incStepMultGetter;  // 0x80
        public System.Func<System.Single> decimalsGetter;  // 0x88

        // Methods
        // RVA: 0x0B2EA388  token: 0x600086F
        public System.Void Init() { }
        // RVA: 0x0B2E9750  token: 0x6000870
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x0B2E96E4  token: 0x6000871
        public virtual System.Void OnDeselection() { }
        // RVA: 0x0B2EA3A0  token: 0x6000872
        public virtual System.Void OnIncrement(System.Boolean fast) { }
        // RVA: 0x0B2EA390  token: 0x6000873
        public virtual System.Void OnDecrement(System.Boolean fast) { }
        // RVA: 0x0B2EA2CC  token: 0x6000874
        private System.Void ChangeValue(System.Boolean fast, System.Single multiplier) { }
        // RVA: 0x0B2EA3B0  token: 0x6000875
        private System.Void UpdateValueLabel() { }
        // RVA: 0x0B2E170C  token: 0x6000876
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000183  // size: 0x88
    public class DebugUIHandlerIndirectToggle : UnityEngine.Rendering.UI.DebugUIHandlerWidget
    {
        // Fields
        public UnityEngine.UI.Text nameLabel;  // 0x58
        public UnityEngine.UI.Toggle valueToggle;  // 0x60
        public UnityEngine.UI.Image checkmarkImage;  // 0x68
        public System.Func<System.Int32,System.Boolean> getter;  // 0x70
        public System.Action<System.Int32,System.Boolean> setter;  // 0x78
        private System.Int32 index;  // 0x80

        // Methods
        // RVA: 0x0B2EA48C  token: 0x6000877
        public System.Void Init() { }
        // RVA: 0x0B2EA650  token: 0x6000878
        private System.Void OnToggleValueChanged(System.Boolean value) { }
        // RVA: 0x0B2EA5F4  token: 0x6000879
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x0B2EA598  token: 0x600087A
        public virtual System.Void OnDeselection() { }
        // RVA: 0x0B2EA544  token: 0x600087B
        public virtual System.Void OnAction() { }
        // RVA: 0x0B2EA680  token: 0x600087C
        private System.Void UpdateValueLabel() { }
        // RVA: 0x0B2E170C  token: 0x600087D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000184  // size: 0x70
    public class DebugUIHandlerIntField : UnityEngine.Rendering.UI.DebugUIHandlerWidget
    {
        // Fields
        public UnityEngine.UI.Text nameLabel;  // 0x58
        public UnityEngine.UI.Text valueLabel;  // 0x60
        private UnityEngine.Rendering.DebugUI.IntField m_Field;  // 0x68

        // Methods
        // RVA: 0x0B2EA7E0  token: 0x600087E
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x0B2E9750  token: 0x600087F
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x0B2E96E4  token: 0x6000880
        public virtual System.Void OnDeselection() { }
        // RVA: 0x0B2EA7D0  token: 0x6000881
        public virtual System.Void OnIncrement(System.Boolean fast) { }
        // RVA: 0x0B2EA7C0  token: 0x6000882
        public virtual System.Void OnDecrement(System.Boolean fast) { }
        // RVA: 0x0B2EA70C  token: 0x6000883
        private System.Void ChangeValue(System.Boolean fast, System.Int32 multiplier) { }
        // RVA: 0x0B2EA870  token: 0x6000884
        private System.Void UpdateValueLabel() { }
        // RVA: 0x0B2E170C  token: 0x6000885
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000185  // size: 0x68
    public class DebugUIHandlerMessageBox : UnityEngine.Rendering.UI.DebugUIHandlerWidget
    {
        // Fields
        public UnityEngine.UI.Text nameLabel;  // 0x58
        private UnityEngine.Rendering.DebugUI.MessageBox m_Field;  // 0x60
        private static UnityEngine.Color32 k_WarningBackgroundColor;  // static @ 0x0
        private static UnityEngine.Color32 k_WarningTextColor;  // static @ 0x4
        private static UnityEngine.Color32 k_ErrorBackgroundColor;  // static @ 0x8
        private static UnityEngine.Color32 k_ErrorTextColor;  // static @ 0xc

        // Methods
        // RVA: 0x0B2EA928  token: 0x6000886
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x012081B0  token: 0x6000887
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x0B2E170C  token: 0x6000888
        public System.Void .ctor() { }
        // RVA: 0x0B2EAA58  token: 0x6000889
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000186  // size: 0x60
    public class DebugUIHandlerPanel : UnityEngine.MonoBehaviour
    {
        // Fields
        public UnityEngine.UI.Text nameLabel;  // 0x18
        public UnityEngine.UI.ScrollRect scrollRect;  // 0x20
        public UnityEngine.RectTransform viewport;  // 0x28
        public UnityEngine.Rendering.UI.DebugUIHandlerCanvas Canvas;  // 0x30
        private UnityEngine.RectTransform m_ScrollTransform;  // 0x38
        private UnityEngine.RectTransform m_ContentTransform;  // 0x40
        private UnityEngine.RectTransform m_MaskTransform;  // 0x48
        private UnityEngine.Rendering.UI.DebugUIHandlerWidget m_ScrollTarget;  // 0x50
        protected internal UnityEngine.Rendering.DebugUI.Panel m_Panel;  // 0x58

        // Methods
        // RVA: 0x0B2EACA4  token: 0x600088A
        private System.Void OnEnable() { }
        // RVA: 0x0B2EAE3C  token: 0x600088B
        private System.Void SetPanel(UnityEngine.Rendering.DebugUI.Panel panel) { }
        // RVA: 0x02E56440  token: 0x600088C
        private UnityEngine.Rendering.DebugUI.Panel GetPanel() { }
        // RVA: 0x0B2EADFC  token: 0x600088D
        public System.Void SelectNextItem() { }
        // RVA: 0x0B2EAE1C  token: 0x600088E
        public System.Void SelectPreviousItem() { }
        // RVA: 0x0B2EAD60  token: 0x600088F
        public System.Void OnScrollbarClicked() { }
        // RVA: 0x02FDAEA0  token: 0x6000890
        private System.Void SetScrollTarget(UnityEngine.Rendering.UI.DebugUIHandlerWidget target) { }
        // RVA: 0x0B2EAE78  token: 0x6000891
        private System.Void UpdateScroll() { }
        // RVA: 0x0B2EAB40  token: 0x6000892
        private System.Single GetYPosInScroll(UnityEngine.RectTransform target) { }
        // RVA: 0x0B2EAAF0  token: 0x6000893
        private UnityEngine.Rendering.UI.DebugUIHandlerWidget GetFirstItem() { }
        // RVA: 0x0B2EADB0  token: 0x6000894
        public System.Void ResetDebugManager() { }
        // RVA: 0x05393520  token: 0x6000895
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000187  // size: 0x30
    public class DebugUIHandlerPersistentCanvas : UnityEngine.MonoBehaviour
    {
        // Fields
        public UnityEngine.RectTransform panel;  // 0x18
        public UnityEngine.RectTransform valuePrefab;  // 0x20
        private System.Collections.Generic.List<UnityEngine.Rendering.UI.DebugUIHandlerValue> m_Items;  // 0x28

        // Methods
        // RVA: 0x0B2EB194  token: 0x6000896
        private System.Void Toggle(UnityEngine.Rendering.DebugUI.Value widget) { }
        // RVA: 0x0B2EB028  token: 0x6000897
        private System.Void Clear() { }
        // RVA: 0x0B2EB3D8  token: 0x6000898
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000189  // size: 0x80
    public class DebugUIHandlerRow : UnityEngine.Rendering.UI.DebugUIHandlerFoldout
    {
        // Fields
        private System.Single m_Timer;  // 0x78

        // Methods
        // RVA: 0x04DB98E0  token: 0x600089B
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0B2EB458  token: 0x600089C
        protected System.Void Update() { }
        // RVA: 0x0B2E170C  token: 0x600089D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200018A  // size: 0x78
    public class DebugUIHandlerToggle : UnityEngine.Rendering.UI.DebugUIHandlerWidget
    {
        // Fields
        public UnityEngine.UI.Text nameLabel;  // 0x58
        public UnityEngine.UI.Toggle valueToggle;  // 0x60
        public UnityEngine.UI.Image checkmarkImage;  // 0x68
        protected internal UnityEngine.Rendering.DebugUI.BoolField m_Field;  // 0x70

        // Methods
        // RVA: 0x0B2EBDC0  token: 0x600089E
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x0B2EBD6C  token: 0x600089F
        private System.Void OnToggleValueChanged(System.Boolean value) { }
        // RVA: 0x0B2EA5F4  token: 0x60008A0
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x0B2EA598  token: 0x60008A1
        public virtual System.Void OnDeselection() { }
        // RVA: 0x0B2EBCF4  token: 0x60008A2
        public virtual System.Void OnAction() { }
        // RVA: 0x0B2EBED0  token: 0x60008A3
        protected internal virtual System.Void UpdateValueLabel() { }
        // RVA: 0x0B2E170C  token: 0x60008A4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200018B  // size: 0x80
    public class DebugUIHandlerToggleHistory : UnityEngine.Rendering.UI.DebugUIHandlerToggle
    {
        // Fields
        private UnityEngine.UI.Toggle[] historyToggles;  // 0x78
        private static System.Single xDecal;  // const

        // Methods
        // RVA: 0x0B2EB874  token: 0x60008A5
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x0B2EBBAC  token: 0x60008A6
        protected internal virtual System.Void UpdateValueLabel() { }
        // RVA: 0x0B2EB818  token: 0x60008A7
        private System.Collections.IEnumerator RefreshAfterSanitization() { }
        // RVA: 0x0B2E170C  token: 0x60008A8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200018D  // size: 0x70
    public class DebugUIHandlerUIntField : UnityEngine.Rendering.UI.DebugUIHandlerWidget
    {
        // Fields
        public UnityEngine.UI.Text nameLabel;  // 0x58
        public UnityEngine.UI.Text valueLabel;  // 0x60
        private UnityEngine.Rendering.DebugUI.UIntField m_Field;  // 0x68

        // Methods
        // RVA: 0x0B2EC048  token: 0x60008AF
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x0B2E9750  token: 0x60008B0
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x0B2E96E4  token: 0x60008B1
        public virtual System.Void OnDeselection() { }
        // RVA: 0x0B2EC038  token: 0x60008B2
        public virtual System.Void OnIncrement(System.Boolean fast) { }
        // RVA: 0x0B2EC028  token: 0x60008B3
        public virtual System.Void OnDecrement(System.Boolean fast) { }
        // RVA: 0x0B2EBF68  token: 0x60008B4
        private System.Void ChangeValue(System.Boolean fast, System.Int32 multiplier) { }
        // RVA: 0x0B2EC0D8  token: 0x60008B5
        private System.Void UpdateValueLabel() { }
        // RVA: 0x0B2E170C  token: 0x60008B6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200018E  // size: 0x78
    public class DebugUIHandlerValue : UnityEngine.Rendering.UI.DebugUIHandlerWidget
    {
        // Fields
        public UnityEngine.UI.Text nameLabel;  // 0x58
        public UnityEngine.UI.Text valueLabel;  // 0x60
        private UnityEngine.Rendering.DebugUI.Value m_Field;  // 0x68
        private System.Single m_Timer;  // 0x70

        // Methods
        // RVA: 0x04DB98F0  token: 0x60008B7
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0B2EC338  token: 0x60008B8
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x0B2E9750  token: 0x60008B9
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x0B2E96E4  token: 0x60008BA
        public virtual System.Void OnDeselection() { }
        // RVA: 0x0B2EC3BC  token: 0x60008BB
        private System.Void Update() { }
        // RVA: 0x0B2E170C  token: 0x60008BC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200018F  // size: 0x60
    public class DebugUIHandlerVBox : UnityEngine.Rendering.UI.DebugUIHandlerWidget
    {
        // Fields
        private UnityEngine.Rendering.UI.DebugUIHandlerContainer m_Container;  // 0x58

        // Methods
        // RVA: 0x0B2EC2D8  token: 0x60008BD
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x0B2EC22C  token: 0x60008BE
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x0B2EC190  token: 0x60008BF
        public virtual UnityEngine.Rendering.UI.DebugUIHandlerWidget Next() { }
        // RVA: 0x0B2E170C  token: 0x60008C0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000190  // size: 0x88
    public class DebugUIHandlerVector2 : UnityEngine.Rendering.UI.DebugUIHandlerWidget
    {
        // Fields
        public UnityEngine.UI.Text nameLabel;  // 0x58
        public UnityEngine.Rendering.UI.UIFoldout valueToggle;  // 0x60
        public UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField fieldX;  // 0x68
        public UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField fieldY;  // 0x70
        private UnityEngine.Rendering.DebugUI.Vector2Field m_Field;  // 0x78
        private UnityEngine.Rendering.UI.DebugUIHandlerContainer m_Container;  // 0x80

        // Methods
        // RVA: 0x0B2EC6A8  token: 0x60008C1
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x0B2EC5F0  token: 0x60008C2
        private System.Void SetValue(System.Single v, System.Boolean x, System.Boolean y) { }
        // RVA: 0x0B2EC920  token: 0x60008C3
        private System.Void SetupSettings(UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField field) { }
        // RVA: 0x0B2EC50C  token: 0x60008C4
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x0B2E0FBC  token: 0x60008C5
        public virtual System.Void OnDeselection() { }
        // RVA: 0x0B2E0FEC  token: 0x60008C6
        public virtual System.Void OnIncrement(System.Boolean fast) { }
        // RVA: 0x0B2E0F98  token: 0x60008C7
        public virtual System.Void OnDecrement(System.Boolean fast) { }
        // RVA: 0x0B2E0F68  token: 0x60008C8
        public virtual System.Void OnAction() { }
        // RVA: 0x0B2EC458  token: 0x60008C9
        public virtual UnityEngine.Rendering.UI.DebugUIHandlerWidget Next() { }
        // RVA: 0x0B2E170C  token: 0x60008CA
        public System.Void .ctor() { }
        // RVA: 0x0B2ECA5C  token: 0x60008CB
        private System.Single <SetWidget>b__6_0() { }
        // RVA: 0x0B2ECAB0  token: 0x60008CC
        private System.Void <SetWidget>b__6_1(System.Single x) { }
        // RVA: 0x0B2ECAD0  token: 0x60008CD
        private System.Single <SetWidget>b__6_2() { }
        // RVA: 0x0B2ECB24  token: 0x60008CE
        private System.Void <SetWidget>b__6_3(System.Single x) { }
        // RVA: 0x0B2ECB44  token: 0x60008CF
        private System.Single <SetupSettings>b__8_0() { }
        // RVA: 0x0B2ECB64  token: 0x60008D0
        private System.Single <SetupSettings>b__8_1() { }
        // RVA: 0x0B2ECB84  token: 0x60008D1
        private System.Single <SetupSettings>b__8_2() { }

    }

    // TypeToken: 0x2000191  // size: 0x90
    public class DebugUIHandlerVector3 : UnityEngine.Rendering.UI.DebugUIHandlerWidget
    {
        // Fields
        public UnityEngine.UI.Text nameLabel;  // 0x58
        public UnityEngine.Rendering.UI.UIFoldout valueToggle;  // 0x60
        public UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField fieldX;  // 0x68
        public UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField fieldY;  // 0x70
        public UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField fieldZ;  // 0x78
        private UnityEngine.Rendering.DebugUI.Vector3Field m_Field;  // 0x80
        private UnityEngine.Rendering.UI.DebugUIHandlerContainer m_Container;  // 0x88

        // Methods
        // RVA: 0x0B2ECE14  token: 0x60008D2
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x0B2ECD3C  token: 0x60008D3
        private System.Void SetValue(System.Single v, System.Boolean x, System.Boolean y, System.Boolean z) { }
        // RVA: 0x0B2ED184  token: 0x60008D4
        private System.Void SetupSettings(UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField field) { }
        // RVA: 0x0B2ECC58  token: 0x60008D5
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x0B2E0FBC  token: 0x60008D6
        public virtual System.Void OnDeselection() { }
        // RVA: 0x0B2E0FEC  token: 0x60008D7
        public virtual System.Void OnIncrement(System.Boolean fast) { }
        // RVA: 0x0B2E0F98  token: 0x60008D8
        public virtual System.Void OnDecrement(System.Boolean fast) { }
        // RVA: 0x0B2E0F68  token: 0x60008D9
        public virtual System.Void OnAction() { }
        // RVA: 0x0B2ECBA4  token: 0x60008DA
        public virtual UnityEngine.Rendering.UI.DebugUIHandlerWidget Next() { }
        // RVA: 0x0B2E170C  token: 0x60008DB
        public System.Void .ctor() { }
        // RVA: 0x0B2ED2C0  token: 0x60008DC
        private System.Single <SetWidget>b__7_0() { }
        // RVA: 0x0B2ED314  token: 0x60008DD
        private System.Void <SetWidget>b__7_1(System.Single v) { }
        // RVA: 0x0B2ED338  token: 0x60008DE
        private System.Single <SetWidget>b__7_2() { }
        // RVA: 0x0B2ED38C  token: 0x60008DF
        private System.Void <SetWidget>b__7_3(System.Single v) { }
        // RVA: 0x0B2ED3B0  token: 0x60008E0
        private System.Single <SetWidget>b__7_4() { }
        // RVA: 0x0B2ED404  token: 0x60008E1
        private System.Void <SetWidget>b__7_5(System.Single v) { }
        // RVA: 0x0B2ED428  token: 0x60008E2
        private System.Single <SetupSettings>b__9_0() { }
        // RVA: 0x0B2ED448  token: 0x60008E3
        private System.Single <SetupSettings>b__9_1() { }
        // RVA: 0x0B2ED468  token: 0x60008E4
        private System.Single <SetupSettings>b__9_2() { }

    }

    // TypeToken: 0x2000192  // size: 0x98
    public class DebugUIHandlerVector4 : UnityEngine.Rendering.UI.DebugUIHandlerWidget
    {
        // Fields
        public UnityEngine.UI.Text nameLabel;  // 0x58
        public UnityEngine.Rendering.UI.UIFoldout valueToggle;  // 0x60
        public UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField fieldX;  // 0x68
        public UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField fieldY;  // 0x70
        public UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField fieldZ;  // 0x78
        public UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField fieldW;  // 0x80
        private UnityEngine.Rendering.DebugUI.Vector4Field m_Field;  // 0x88
        private UnityEngine.Rendering.UI.DebugUIHandlerContainer m_Container;  // 0x90

        // Methods
        // RVA: 0x0B2ED72C  token: 0x60008E5
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x0B2ED624  token: 0x60008E6
        private System.Void SetValue(System.Single v, System.Boolean x, System.Boolean y, System.Boolean z, System.Boolean w) { }
        // RVA: 0x0B2EDBA0  token: 0x60008E7
        private System.Void SetupSettings(UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField field) { }
        // RVA: 0x0B2ED540  token: 0x60008E8
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x0B2E0FBC  token: 0x60008E9
        public virtual System.Void OnDeselection() { }
        // RVA: 0x0B2E0FEC  token: 0x60008EA
        public virtual System.Void OnIncrement(System.Boolean fast) { }
        // RVA: 0x0B2E0F98  token: 0x60008EB
        public virtual System.Void OnDecrement(System.Boolean fast) { }
        // RVA: 0x0B2E0F68  token: 0x60008EC
        public virtual System.Void OnAction() { }
        // RVA: 0x0B2ED48C  token: 0x60008ED
        public virtual UnityEngine.Rendering.UI.DebugUIHandlerWidget Next() { }
        // RVA: 0x0B2E170C  token: 0x60008EE
        public System.Void .ctor() { }
        // RVA: 0x0B2EDCDC  token: 0x60008EF
        private System.Single <SetWidget>b__8_0() { }
        // RVA: 0x0B2EDD30  token: 0x60008F0
        private System.Void <SetWidget>b__8_1(System.Single x) { }
        // RVA: 0x0B2EDD58  token: 0x60008F1
        private System.Single <SetWidget>b__8_2() { }
        // RVA: 0x0B2EDDAC  token: 0x60008F2
        private System.Void <SetWidget>b__8_3(System.Single x) { }
        // RVA: 0x0B2EDDD4  token: 0x60008F3
        private System.Single <SetWidget>b__8_4() { }
        // RVA: 0x0B2EDE28  token: 0x60008F4
        private System.Void <SetWidget>b__8_5(System.Single x) { }
        // RVA: 0x0B2EDE50  token: 0x60008F5
        private System.Single <SetWidget>b__8_6() { }
        // RVA: 0x0B2EDEA4  token: 0x60008F6
        private System.Void <SetWidget>b__8_7(System.Single x) { }
        // RVA: 0x0B2EDECC  token: 0x60008F7
        private System.Single <SetupSettings>b__10_0() { }
        // RVA: 0x0B2EDEEC  token: 0x60008F8
        private System.Single <SetupSettings>b__10_1() { }
        // RVA: 0x0B2EDF0C  token: 0x60008F9
        private System.Single <SetupSettings>b__10_2() { }

    }

    // TypeToken: 0x2000193  // size: 0x58
    public class DebugUIHandlerWidget : UnityEngine.MonoBehaviour
    {
        // Fields
        public UnityEngine.Color colorDefault;  // 0x18
        public UnityEngine.Color colorSelected;  // 0x28
        private UnityEngine.Rendering.UI.DebugUIHandlerWidget <parentUIHandler>k__BackingField;  // 0x38
        private UnityEngine.Rendering.UI.DebugUIHandlerWidget <previousUIHandler>k__BackingField;  // 0x40
        private UnityEngine.Rendering.UI.DebugUIHandlerWidget <nextUIHandler>k__BackingField;  // 0x48
        protected UnityEngine.Rendering.DebugUI.Widget m_Widget;  // 0x50

        // Properties
        UnityEngine.Rendering.UI.DebugUIHandlerWidget parentUIHandler { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }
        UnityEngine.Rendering.UI.DebugUIHandlerWidget previousUIHandler { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        UnityEngine.Rendering.UI.DebugUIHandlerWidget nextUIHandler { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000900
        protected virtual System.Void OnEnable() { }
        // RVA: 0x02FDAEA0  token: 0x6000901
        private virtual System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget) { }
        // RVA: 0x04D86270  token: 0x6000902
        private UnityEngine.Rendering.DebugUI.Widget GetWidget() { }
        // RVA: -1  // generic def  token: 0x6000903
        protected T CastWidget() { }
        // RVA: 0x02FFF600  token: 0x6000904
        public virtual System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous) { }
        // RVA: 0x041E1670  token: 0x6000905
        public virtual System.Void OnDeselection() { }
        // RVA: 0x041E1670  token: 0x6000906
        public virtual System.Void OnAction() { }
        // RVA: 0x041E1670  token: 0x6000907
        public virtual System.Void OnIncrement(System.Boolean fast) { }
        // RVA: 0x041E1670  token: 0x6000908
        public virtual System.Void OnDecrement(System.Boolean fast) { }
        // RVA: 0x0B2EE008  token: 0x6000909
        public virtual UnityEngine.Rendering.UI.DebugUIHandlerWidget Previous() { }
        // RVA: 0x0B2EDF30  token: 0x600090A
        public virtual UnityEngine.Rendering.UI.DebugUIHandlerWidget Next() { }
        // RVA: 0x0B2EE08C  token: 0x600090B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000194  // size: 0x1C8
    public class UIFoldout : UnityEngine.UI.Toggle
    {
        // Fields
        public UnityEngine.GameObject content;  // 0x1b0
        public UnityEngine.GameObject arrowOpened;  // 0x1b8
        public UnityEngine.GameObject arrowClosed;  // 0x1c0

        // Methods
        // RVA: 0x0B2EE640  token: 0x600090C
        protected virtual System.Void Start() { }
        // RVA: 0x0B2EE440  token: 0x600090D
        private System.Void OnValidate() { }
        // RVA: 0x0B2EE454  token: 0x600090E
        public System.Void SetState(System.Boolean state) { }
        // RVA: 0x0B2EE460  token: 0x600090F
        public System.Void SetState(System.Boolean state, System.Boolean rebuildLayout) { }
        // RVA: 0x0B2EE704  token: 0x6000910
        public System.Void .ctor() { }

    }

}

