// ========================================================
// Dumped by @desirepro
// Assembly: Unity.RenderPipelines.Core.Runtime.dll
// Classes:  411
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

CLASS: SceneRenderPipeline
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.Rendering.RenderPipelineAssetrenderPipelineAsset  // 0x18
METHODS:
  RVA=0x0B2B5E40  token=0x6000003  System.Void OnEnable()
  RVA=0x0B2B5E40  token=0x6000004  System.Void OnValidate()
  RVA=0x05393520  token=0x6000005  System.Void .ctor()
END_CLASS

CLASS: ShaderQuality
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Rendering.HGShaderQualitySettings.ShaderQualityLow  // const
  public    static  Beyond.Rendering.HGShaderQualitySettings.ShaderQualityHigh  // const
METHODS:
END_CLASS

CLASS: UpDirection
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.LightAnchor.UpDirectionWorld  // const
  public    static  UnityEngine.LightAnchor.UpDirectionLocal  // const
METHODS:
END_CLASS

CLASS: Axes
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x34
FIELDS:
  public            UnityEngine.Vector3             up  // 0x10
  public            UnityEngine.Vector3             right  // 0x1c
  public            UnityEngine.Vector3             forward  // 0x28
METHODS:
END_CLASS

CLASS: Brick
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            UnityEngine.Vector3Int          position  // 0x10
  public            System.Int32                    subdivisionLevel  // 0x1c
METHODS:
  RVA=0x04D92BB0  token=0x6000031  System.Void .ctor(UnityEngine.Vector3Int position, System.Int32 subdivisionLevel)
  RVA=0x0B29F5C8  token=0x6000032  System.Boolean Equals(UnityEngine.Experimental.Rendering.ProbeBrickIndex.Brick other)
END_CLASS

CLASS: ReservedBrick
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x24
FIELDS:
  public            UnityEngine.Experimental.Rendering.ProbeBrickIndex.Brickbrick  // 0x10
  public            System.Int32                    flattenedIdx  // 0x20
METHODS:
END_CLASS

CLASS: VoxelMeta
TYPE:  sealed struct
TOKEN: 0x200000D
SIZE:  0x20
FIELDS:
  public            UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegIdid  // 0x10
  public            System.Collections.Generic.List<System.UInt16>brickIndices  // 0x18
METHODS:
END_CLASS

CLASS: BrickMeta
TYPE:  sealed struct
TOKEN: 0x200000E
SIZE:  0x20
FIELDS:
  public            System.Collections.Generic.HashSet<UnityEngine.Vector3Int>voxels  // 0x10
  public            System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickIndex.ReservedBrick>bricks  // 0x18
METHODS:
END_CLASS

CLASS: CellIndexUpdateInfo
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x40
FIELDS:
  public            System.Int32                    firstChunkIndex  // 0x10
  public            System.Int32                    numberOfChunks  // 0x14
  public            System.Int32                    minSubdivInCell  // 0x18
  public            UnityEngine.Vector3Int          minValidBrickIndexForCellAtMaxRes  // 0x1c
  public            UnityEngine.Vector3Int          maxValidBrickIndexForCellAtMaxResPlusOne  // 0x28
  public            UnityEngine.Vector3Int          cellPositionInBricksAtMaxRes  // 0x34
METHODS:
END_CLASS

CLASS: <>c__DisplayClass31_0
TYPE:  sealed class
TOKEN: 0x2000010
SIZE:  0x20
FIELDS:
  public            UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegIdid  // 0x10
  public            System.Predicate<UnityEngine.Experimental.Rendering.ProbeBrickIndex.VoxelMeta><>9__0  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000033  System.Void .ctor()
  RVA=0x0702DACC  token=0x6000034  System.Boolean <AddBricks>b__0(UnityEngine.Experimental.Rendering.ProbeBrickIndex.VoxelMeta lhs)
END_CLASS

CLASS: <>c__DisplayClass32_0
TYPE:  sealed class
TOKEN: 0x2000011
SIZE:  0x20
FIELDS:
  public            UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegIdid  // 0x10
  public            System.Predicate<UnityEngine.Experimental.Rendering.ProbeBrickIndex.VoxelMeta><>9__0  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000035  System.Void .ctor()
  RVA=0x0702DACC  token=0x6000036  System.Boolean <RemoveBricks>b__0(UnityEngine.Experimental.Rendering.ProbeBrickIndex.VoxelMeta lhs)
END_CLASS

CLASS: BrickChunkAlloc
TYPE:  sealed struct
TOKEN: 0x2000013
SIZE:  0x1C
FIELDS:
  public            System.Int32                    x  // 0x10
  public            System.Int32                    y  // 0x14
  public            System.Int32                    z  // 0x18
METHODS:
  RVA=0x04DB8A00  token=0x600004B  System.Int32 flattenIndex(System.Int32 sx, System.Int32 sy)
END_CLASS

CLASS: DataLocation
TYPE:  sealed struct
TOKEN: 0x2000014
SIZE:  0x58
FIELDS:
  private           UnityEngine.Texture3D           TexL0_L1rx  // 0x10
  private           UnityEngine.Texture3D           TexL1_G_ry  // 0x18
  private           UnityEngine.Texture3D           TexL1_B_rz  // 0x20
  private           UnityEngine.Texture3D           TexL2_0  // 0x28
  private           UnityEngine.Texture3D           TexL2_1  // 0x30
  private           UnityEngine.Texture3D           TexL2_2  // 0x38
  private           UnityEngine.Texture3D           TexL2_3  // 0x40
  private           System.Int32                    width  // 0x48
  private           System.Int32                    height  // 0x4c
  private           System.Int32                    depth  // 0x50
METHODS:
  RVA=0x0B29F69C  token=0x600004C  System.Void Cleanup()
END_CLASS

CLASS: IndexMetaData
TYPE:  sealed struct
TOKEN: 0x2000016
SIZE:  0x30
FIELDS:
  private           UnityEngine.Vector3Int          minLocalIdx  // 0x10
  private           UnityEngine.Vector3Int          maxLocalIdx  // 0x1c
  private           System.Int32                    firstChunkIndex  // 0x28
  private           System.Int32                    minSubdiv  // 0x2c
METHODS:
  RVA=0x0B29F7C8  token=0x6000059  System.Void Pack(System.UInt32[]& vals)
END_CLASS

CLASS: Cell
TYPE:  class
TOKEN: 0x200001C
SIZE:  0x50
FIELDS:
  public            System.Int32                    index  // 0x10
  public            UnityEngine.Vector3Int          position  // 0x14
  public            System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickIndex.Brick>bricks  // 0x20
  public            UnityEngine.Vector3[]           probePositions  // 0x28
  public            UnityEngine.Rendering.SphericalHarmonicsL2[]sh  // 0x30
  public            System.Single[]                 validity  // 0x38
  public            System.Int32                    minSubdiv  // 0x40
  public            System.Int32                    flatIdxInCellIndices  // 0x44
  public            System.Boolean                  loaded  // 0x48
METHODS:
  RVA=0x04D8EC90  token=0x60000AE  System.Void .ctor()
END_CLASS

CLASS: CellChunkInfo
TYPE:  class
TOKEN: 0x200001D
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc>chunks  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60000AF  System.Void .ctor()
END_CLASS

CLASS: CellSortInfo
TYPE:  class
TOKEN: 0x200001E
SIZE:  0x30
IMPLEMENTS: System.IComparable
FIELDS:
  private           System.String                   sourceAsset  // 0x10
  private           UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Cellcell  // 0x18
  private           System.Single                   distanceToCamera  // 0x20
  private           UnityEngine.Vector3             position  // 0x24
METHODS:
  RVA=0x0B29F61C  token=0x60000B0  System.Int32 CompareTo(System.Object obj)
  RVA=0x041E1670  token=0x60000B1  System.Void .ctor()
END_CLASS

CLASS: Volume
TYPE:  sealed struct
TOKEN: 0x200001F
SIZE:  0x48
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private           UnityEngine.Vector3             corner  // 0x10
  private           UnityEngine.Vector3             X  // 0x1c
  private           UnityEngine.Vector3             Y  // 0x28
  private           UnityEngine.Vector3             Z  // 0x34
  private           System.Single                   maxSubdivisionMultiplier  // 0x40
  private           System.Single                   minSubdivisionMultiplier  // 0x44
METHODS:
  RVA=0x0B2B6B2C  token=0x60000B2  System.Void .ctor(UnityEngine.Matrix4x4 trs, System.Single maxSubdivision, System.Single minSubdivision)
  RVA=0x04DB8C10  token=0x60000B3  System.Void .ctor(UnityEngine.Vector3 corner, UnityEngine.Vector3 X, UnityEngine.Vector3 Y, UnityEngine.Vector3 Z, System.Single maxSubdivision, System.Single minSubdivision)
  RVA=0x04DB8BC0  token=0x60000B4  System.Void .ctor(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Volume copy)
  RVA=0x0B2B6240  token=0x60000B5  UnityEngine.Bounds CalculateAABB()
  RVA=0x0B2B6550  token=0x60000B6  System.Void CalculateCenterAndSize(UnityEngine.Vector3& center, UnityEngine.Vector3& size)
  RVA=0x0B2B6A40  token=0x60000B7  System.Void Transform(UnityEngine.Matrix4x4 trs)
  RVA=0x0B2B6874  token=0x60000B8  System.String ToString()
  RVA=0x0B2B66D0  token=0x60000B9  System.Boolean Equals(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Volume other)
END_CLASS

CLASS: RefVolTransform
TYPE:  sealed struct
TOKEN: 0x2000020
SIZE:  0x70
FIELDS:
  public            UnityEngine.Matrix4x4           refSpaceToWS  // 0x10
  public            UnityEngine.Vector3             posWS  // 0x50
  public            UnityEngine.Quaternion          rot  // 0x5c
  public            System.Single                   scale  // 0x6c
METHODS:
END_CLASS

CLASS: RuntimeResources
TYPE:  sealed struct
TOKEN: 0x2000021
SIZE:  0x58
FIELDS:
  public            UnityEngine.ComputeBuffer       index  // 0x10
  public            UnityEngine.ComputeBuffer       cellIndices  // 0x18
  public            UnityEngine.Texture3D           L0_L1rx  // 0x20
  public            UnityEngine.Texture3D           L1_G_ry  // 0x28
  public            UnityEngine.Texture3D           L1_B_rz  // 0x30
  public            UnityEngine.Texture3D           L2_0  // 0x38
  public            UnityEngine.Texture3D           L2_1  // 0x40
  public            UnityEngine.Texture3D           L2_2  // 0x48
  public            UnityEngine.Texture3D           L2_3  // 0x50
METHODS:
END_CLASS

CLASS: RegId
TYPE:  sealed struct
TOKEN: 0x2000022
SIZE:  0x14
FIELDS:
  private           System.Int32                    id  // 0x10
METHODS:
  RVA=0x068B8E74  token=0x60000BA  System.Boolean IsValid()
  RVA=0x04D910C0  token=0x60000BB  System.Void Invalidate()
  RVA=0x07872540  token=0x60000BC  System.Boolean op_Equality(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId lhs, UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId rhs)
  RVA=0x0AAED910  token=0x60000BD  System.Boolean op_Inequality(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId lhs, UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId rhs)
  RVA=0x0B2AD4E4  token=0x60000BE  System.Boolean Equals(System.Object obj)
  RVA=0x02B77620  token=0x60000BF  System.Int32 GetHashCode()
END_CLASS

CLASS: ExtraDataActionInput
TYPE:  sealed struct
TOKEN: 0x2000023
SIZE:  0x11
FIELDS:
METHODS:
END_CLASS

CLASS: InitInfo
TYPE:  sealed struct
TOKEN: 0x2000024
SIZE:  0x28
FIELDS:
  public            UnityEngine.Vector3Int          pendingMinCellPosition  // 0x10
  public            UnityEngine.Vector3Int          pendingMaxCellPosition  // 0x1c
METHODS:
END_CLASS

CLASS: CellInstancedDebugProbes
TYPE:  class
TOKEN: 0x2000025
SIZE:  0x40
FIELDS:
  public            System.Collections.Generic.List<UnityEngine.Matrix4x4[]>probeBuffers  // 0x10
  public            System.Collections.Generic.List<UnityEngine.MaterialPropertyBlock>props  // 0x18
  public            UnityEngine.Hash128             cellHash  // 0x20
  public            UnityEngine.Vector3             cellPosition  // 0x30
METHODS:
  RVA=0x041E1670  token=0x60000C0  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000026
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Experimental.Rendering.ProbeReferenceVolume.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Single>      <>9__119_6  // static @ 0x8
  public    static  System.Func<System.Single>      <>9__119_15  // static @ 0x10
  public    static  System.Func<System.Single>      <>9__119_16  // static @ 0x18
  public    static  System.Func<System.Single>      <>9__119_21  // static @ 0x20
  public    static  System.Func<System.Int32>       <>9__119_24  // static @ 0x28
  public    static  System.Func<System.Int32>       <>9__119_25  // static @ 0x30
METHODS:
  RVA=0x0B2B61DC  token=0x60000C1  System.Void .cctor()
  RVA=0x041E1670  token=0x60000C2  System.Void .ctor()
  RVA=0x04D879D0  token=0x60000C3  System.Single <RegisterDebug>b__119_6()
  RVA=0x04DB8BB0  token=0x60000C4  System.Single <RegisterDebug>b__119_15()
  RVA=0x04DA9C10  token=0x60000C5  System.Single <RegisterDebug>b__119_16()
  RVA=0x04D879D0  token=0x60000C6  System.Single <RegisterDebug>b__119_21()
  RVA=0x011EC580  token=0x60000C7  System.Int32 <RegisterDebug>b__119_24()
  RVA=0x0B2B6128  token=0x60000C8  System.Int32 <RegisterDebug>b__119_25()
END_CLASS

CLASS: Version
TYPE:  sealed struct
TOKEN: 0x200002A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Experimental.Rendering.ProbeReferenceVolumeProfile.VersionInitial  // const
METHODS:
END_CLASS

CLASS: AssetVersion
TYPE:  sealed struct
TOKEN: 0x200002D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Experimental.Rendering.ProbeVolumeAsset.AssetVersionFirst  // const
  public    static  UnityEngine.Experimental.Rendering.ProbeVolumeAsset.AssetVersionAddProbeVolumesAtlasEncodingModes  // const
  public    static  UnityEngine.Experimental.Rendering.ProbeVolumeAsset.AssetVersionPV2  // const
  public    static  UnityEngine.Experimental.Rendering.ProbeVolumeAsset.AssetVersionChunkBasedIndex  // const
  public    static  UnityEngine.Experimental.Rendering.ProbeVolumeAsset.AssetVersionMax  // const
  public    static  UnityEngine.Experimental.Rendering.ProbeVolumeAsset.AssetVersionCurrent  // const
METHODS:
END_CLASS

CLASS: SerializableAssetItem
TYPE:  sealed struct
TOKEN: 0x2000033
SIZE:  0x20
FIELDS:
  public            UnityEngine.Experimental.Rendering.ProbeVolumeStatestate  // 0x10
  public            UnityEngine.Experimental.Rendering.ProbeVolumeAssetasset  // 0x18
METHODS:
END_CLASS

CLASS: SerializableBoundItem
TYPE:  sealed struct
TOKEN: 0x2000035
SIZE:  0x30
FIELDS:
  public            System.String                   sceneGUID  // 0x10
  public            UnityEngine.Bounds              bounds  // 0x18
METHODS:
END_CLASS

CLASS: SerializableHasPVItem
TYPE:  sealed struct
TOKEN: 0x2000036
SIZE:  0x20
FIELDS:
  public            System.String                   sceneGUID  // 0x10
  public            System.Boolean                  hasProbeVolumes  // 0x18
METHODS:
END_CLASS

CLASS: SerializablePVProfile
TYPE:  sealed struct
TOKEN: 0x2000037
SIZE:  0x20
FIELDS:
  public            System.String                   sceneGUID  // 0x10
  public            UnityEngine.Experimental.Rendering.ProbeReferenceVolumeProfileprofile  // 0x18
METHODS:
END_CLASS

CLASS: SerializablePVBakeSettings
TYPE:  sealed struct
TOKEN: 0x2000038
SIZE:  0x38
FIELDS:
  public            System.String                   sceneGUID  // 0x10
  public            UnityEngine.Experimental.Rendering.ProbeVolumeBakingProcessSettingssettings  // 0x18
METHODS:
END_CLASS

CLASS: BakingSet
TYPE:  class
TOKEN: 0x2000039
SIZE:  0x48
FIELDS:
  public            System.String                   name  // 0x10
  public            System.Collections.Generic.List<System.String>sceneGUIDs  // 0x18
  public            UnityEngine.Experimental.Rendering.ProbeVolumeBakingProcessSettingssettings  // 0x20
  public            UnityEngine.Experimental.Rendering.ProbeReferenceVolumeProfileprofile  // 0x40
METHODS:
  RVA=0x0B29F550  token=0x60000EE  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200003A
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.SerializablePVProfile,System.String><>9__22_0  // static @ 0x8
METHODS:
  RVA=0x0B2B6178  token=0x60000EF  System.Void .cctor()
  RVA=0x041E1670  token=0x60000F0  System.Void .ctor()
  RVA=0x02A55C30  token=0x60000F1  System.String <UpdateBakingSets>b__22_0(UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.SerializablePVProfile s)
END_CLASS

CLASS: Strings
TYPE:  static class
TOKEN: 0x2000042
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Rendering.DebugUI.Widget.NameAndTooltipClearRenderTargetsAtCreation  // static @ 0x0
  public    static readonly UnityEngine.Rendering.DebugUI.Widget.NameAndTooltipDisablePassCulling  // static @ 0x10
  public    static readonly UnityEngine.Rendering.DebugUI.Widget.NameAndTooltipImmediateMode  // static @ 0x20
  public    static readonly UnityEngine.Rendering.DebugUI.Widget.NameAndTooltipEnableLogging  // static @ 0x30
  public    static readonly UnityEngine.Rendering.DebugUI.Widget.NameAndTooltipLogFrameInformation  // static @ 0x40
  public    static readonly UnityEngine.Rendering.DebugUI.Widget.NameAndTooltipLogResources  // static @ 0x50
METHODS:
  RVA=0x0B2B5E4C  token=0x6000111  System.Void .cctor()
END_CLASS

CLASS: PassDebugData
TYPE:  sealed struct
TOKEN: 0x2000045
SIZE:  0x30
FIELDS:
  public            System.String                   name  // 0x10
  public            System.Collections.Generic.List<System.Int32>[]resourceReadLists  // 0x18
  public            System.Collections.Generic.List<System.Int32>[]resourceWriteLists  // 0x20
  public            System.Boolean                  culled  // 0x28
  public            System.Boolean                  generateDebugData  // 0x29
METHODS:
END_CLASS

CLASS: ResourceDebugData
TYPE:  sealed struct
TOKEN: 0x2000046
SIZE:  0x38
FIELDS:
  public            System.String                   name  // 0x10
  public            System.Boolean                  imported  // 0x18
  public            System.Int32                    creationPassIndex  // 0x1c
  public            System.Int32                    releasePassIndex  // 0x20
  public            System.Collections.Generic.List<System.Int32>consumerList  // 0x28
  public            System.Collections.Generic.List<System.Int32>producerList  // 0x30
METHODS:
END_CLASS

CLASS: CompiledResourceInfo
TYPE:  sealed struct
TOKEN: 0x2000048
SIZE:  0x28
FIELDS:
  public            System.Collections.Generic.List<System.Int32>producers  // 0x10
  public            System.Collections.Generic.List<System.Int32>consumers  // 0x18
  public            System.Int32                    refCount  // 0x20
  public            System.Boolean                  imported  // 0x24
METHODS:
  RVA=0x0B2B7BB4  token=0x6000165  System.Void Reset()
END_CLASS

CLASS: CompiledPassInfo
TYPE:  sealed struct
TOKEN: 0x2000049
SIZE:  0x58
FIELDS:
  public            UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPasspass  // 0x10
  public            System.Collections.Generic.List<System.Int32>[]resourceCreateList  // 0x18
  public            System.Collections.Generic.List<System.Int32>[]resourceReleaseList  // 0x20
  public            System.Int32                    refCount  // 0x28
  public            System.Boolean                  culled  // 0x2c
  public            System.Boolean                  hasSideEffect  // 0x2d
  public            System.Int32                    syncToPassIndex  // 0x30
  public            System.Int32                    syncFromPassIndex  // 0x34
  public            System.Boolean                  needGraphicsFence  // 0x38
  public            UnityEngine.Rendering.GraphicsFencefence  // 0x40
  public            System.Boolean                  enableAsyncCompute  // 0x50
PROPERTIES:
  allowPassCulling  get=0x0B2B7B98
METHODS:
  RVA=0x0B2B79B0  token=0x6000167  System.Void Reset(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPass pass)
END_CLASS

CLASS: ProfilingScopePassData
TYPE:  class
TOKEN: 0x200004A
SIZE:  0x18
FIELDS:
  public            UnityEngine.Rendering.ProfilingSamplersampler  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000168  System.Void .ctor()
END_CLASS

CLASS: OnGraphRegisteredDelegate
TYPE:  sealed class
TOKEN: 0x200004B
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x068FE0AC  token=0x6000169  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x600016A  System.Void Invoke(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph graph)
  RVA=0x047FDD70  token=0x600016B  System.IAsyncResult BeginInvoke(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph graph, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x600016C  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: OnExecutionRegisteredDelegate
TYPE:  sealed class
TOKEN: 0x200004C
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x09B3F6AC  token=0x600016D  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x600016E  System.Void Invoke(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph graph, System.String executionName)
  RVA=0x05E43E3C  token=0x600016F  System.IAsyncResult BeginInvoke(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph graph, System.String executionName, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000170  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200004D
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.<>c<>9  // static @ 0x0
  public    static  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderFunc<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.ProfilingScopePassData><>9__61_0  // static @ 0x8
  public    static  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderFunc<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.ProfilingScopePassData><>9__62_0  // static @ 0x10
METHODS:
  RVA=0x0B2C7CD8  token=0x6000171  System.Void .cctor()
  RVA=0x041E1670  token=0x6000172  System.Void .ctor()
  RVA=0x09B428A8  token=0x6000173  System.Void <BeginProfilingSampler>b__61_0(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.ProfilingScopePassData data, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext ctx)
  RVA=0x09B428D8  token=0x6000174  System.Void <EndProfilingSampler>b__62_0(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.ProfilingScopePassData data, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext ctx)
END_CLASS

CLASS: SharedObjectPoolBase
TYPE:  abstract class
TOKEN: 0x2000054
SIZE:  0x10
FIELDS:
  protected static  System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphObjectPool.SharedObjectPoolBase>s_AllocatedPools  // static @ 0x0
METHODS:
  RVA=-1  // abstract  token=0x60001B6  System.Void Clear()
  RVA=0x0B2C6560  token=0x60001B7  System.Void ClearAll()
  RVA=0x041E1670  token=0x60001B8  System.Void .ctor()
  RVA=0x0B2C6694  token=0x60001B9  System.Void .cctor()
END_CLASS

CLASS: SharedObjectPool`1
TYPE:  class
TOKEN: 0x2000055
EXTENDS: SharedObjectPoolBase
FIELDS:
  private           System.Collections.Generic.Stack<T>m_Pool  // 0x0
  private   static readonly System.Lazy<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphObjectPool.SharedObjectPool<T>>s_Instance  // static @ 0x0
PROPERTIES:
  sharedPool  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60001BA  T Get()
  RVA=-1  // not resolved  token=0x60001BB  System.Void Release(T value)
  RVA=-1  // not resolved  token=0x60001BC  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphObjectPool.SharedObjectPool<T> AllocatePool()
  RVA=-1  // not resolved  token=0x60001BD  System.Void Clear()
  RVA=-1  // not resolved  token=0x60001BF  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60001C0  System.Void .cctor()
END_CLASS

CLASS: ResourceLogInfo
TYPE:  sealed struct
TOKEN: 0x200005F
FIELDS:
  public            System.String                   name  // 0x0
  public            System.Int64                    size  // 0x0
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000060
FIELDS:
  public    static readonly UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourcePool.<>c<Type><>9  // static @ 0x0
  public    static  System.Comparison<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourcePool.ResourceLogInfo<Type>><>9__17_0  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000217  System.Void .cctor()
  RVA=-1  // not resolved  token=0x6000218  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000219  System.Int32 <LogResources>b__17_0(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourcePool.ResourceLogInfo<Type> a, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourcePool.ResourceLogInfo<Type> b)
END_CLASS

CLASS: ResourceCallback
TYPE:  sealed class
TOKEN: 0x2000062
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x086FC23C  token=0x600024E  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x600024F  System.Void Invoke(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext rgContext, UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResource res)
  RVA=0x05E43E3C  token=0x6000250  System.IAsyncResult BeginInvoke(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext rgContext, UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResource res, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000251  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: RenderGraphResourcesData
TYPE:  class
TOKEN: 0x2000063
SIZE:  0x38
FIELDS:
  public            UnityEngine.Rendering.DynamicArray<UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResource>resourceArray  // 0x10
  public            System.Int32                    sharedResourcesCount  // 0x18
  public            UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResourcePoolpool  // 0x20
  public            UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceRegistry.ResourceCallbackcreateResourceCallback  // 0x28
  public            UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceRegistry.ResourceCallbackreleaseResourceCallback  // 0x30
METHODS:
  RVA=0x0B2C5F6C  token=0x6000252  System.Void Clear(System.Boolean onException, System.Int32 frameIndex)
  RVA=0x0B2C5EE8  token=0x6000253  System.Void Cleanup()
  RVA=0x0B2C5FEC  token=0x6000254  System.Void PurgeUnusedGraphicsResources(System.Int32 frameIndex)
  RVA=-1  // generic def  token=0x6000255  System.Int32 AddNewRenderGraphResource(ResType& outRes, System.Boolean pooledResource)
  RVA=0x0B2C6010  token=0x6000256  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000073
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Rendering.CommandBufferPool.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x04D43C90  token=0x60002A7  System.Void .cctor()
  RVA=0x041E1670  token=0x60002A8  System.Void .ctor()
  RVA=0x03127EF0  token=0x60002A9  System.Void <.cctor>b__4_0(UnityEngine.Rendering.CommandBuffer x)
  RVA=0x03128110  token=0x60002AA  System.Void <.cctor>b__4_1(UnityEngine.Rendering.CommandBuffer x)
END_CLASS

CLASS: FixedBufferStringQueue
TYPE:  sealed struct
TOKEN: 0x200007E
SIZE:  0x38
FIELDS:
  private           System.Byte*                    m_ReadCursor  // 0x10
  private           System.Byte*                    m_WriteCursor  // 0x18
  private   readonly System.Byte*                    m_BufferEnd  // 0x20
  private   readonly System.Byte*                    m_BufferStart  // 0x28
  private   readonly System.Int32                    m_BufferLength  // 0x30
  private           System.Int32                    <Count>k__BackingField  // 0x34
PROPERTIES:
  Count  get=0x011F0020  set=0x011F0030
METHODS:
  RVA=0x0B2C014C  token=0x60002EC  System.Void .ctor(System.Byte* ptr, System.Int32 length)
  RVA=0x0B2C00AC  token=0x60002ED  System.Boolean TryPush(System.String v)
  RVA=0x0B2C003C  token=0x60002EE  System.Boolean TryPop(System.String& v)
  RVA=0x0B2C0018  token=0x60002EF  System.Void Clear()
END_CLASS

CLASS: IKeyGetter`2
TYPE:  interface
TOKEN: 0x200007F
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60002F0  TKey Get(TValue& v)
END_CLASS

CLASS: DefaultKeyGetter`1
TYPE:  sealed struct
TOKEN: 0x2000080
SIZE:  0x11
IMPLEMENTS: IKeyGetter`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60002F1  T Get(T& v)
END_CLASS

CLASS: UintKeyGetter
TYPE:  sealed struct
TOKEN: 0x2000081
SIZE:  0x11
IMPLEMENTS: IKeyGetter`2
FIELDS:
METHODS:
  RVA=0x04D94D20  token=0x60002F2  System.UInt32 Get(System.UInt32& v)
END_CLASS

CLASS: ScalerContainer
TYPE:  sealed struct
TOKEN: 0x2000088
SIZE:  0x20
FIELDS:
  public            UnityEngine.Rendering.DynamicResScalePolicyTypetype  // 0x10
  public            UnityEngine.Rendering.PerformDynamicResmethod  // 0x18
METHODS:
END_CLASS

CLASS: UpsamplerScheduleType
TYPE:  sealed struct
TOKEN: 0x2000089
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.DynamicResolutionHandler.UpsamplerScheduleTypeBeforePost  // const
  public    static  UnityEngine.Rendering.DynamicResolutionHandler.UpsamplerScheduleTypeAfterPost  // const
METHODS:
END_CLASS

CLASS: PooledObject
TYPE:  sealed struct
TOKEN: 0x2000092
IMPLEMENTS: System.IDisposable
FIELDS:
  private   readonly T                               m_ToReturn  // 0x0
  private   readonly UnityEngine.Rendering.ObjectPool<T>m_Pool  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000348  System.Void .ctor(T value, UnityEngine.Rendering.ObjectPool<T> pool)
  RVA=-1  // not resolved  token=0x6000349  System.Void System.IDisposable.Dispose()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000096
FIELDS:
  public    static readonly UnityEngine.Rendering.ListPool.<>c<T><>9  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000356  System.Void .cctor()
  RVA=-1  // not resolved  token=0x6000357  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000358  System.Void <.cctor>b__4_0(System.Collections.Generic.List<T> l)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000098
FIELDS:
  public    static readonly UnityEngine.Rendering.HashSetPool.<>c<T><>9  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600035D  System.Void .cctor()
  RVA=-1  // not resolved  token=0x600035E  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600035F  System.Void <.cctor>b__4_0(System.Collections.Generic.HashSet<T> l)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200009A
FIELDS:
  public    static readonly UnityEngine.Rendering.DictionaryPool.<>c<TKey,TValue><>9  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000364  System.Void .cctor()
  RVA=-1  // not resolved  token=0x6000365  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000366  System.Void <.cctor>b__4_0(System.Collections.Generic.Dictionary<TKey,TValue> l)
END_CLASS

CLASS: StereoRenderingMode
TYPE:  sealed struct
TOKEN: 0x20000A2
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.XRGraphics.StereoRenderingModeMultiPass  // const
  public    static  UnityEngine.Rendering.XRGraphics.StereoRenderingModeSinglePass  // const
  public    static  UnityEngine.Rendering.XRGraphics.StereoRenderingModeSinglePassInstanced  // const
  public    static  UnityEngine.Rendering.XRGraphics.StereoRenderingModeSinglePassMultiView  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000A6
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Rendering.DebugManager.<>c<>9  // static @ 0x0
  public    static  System.Action<System.Boolean>   <>9__61_0  // static @ 0x8
  public    static  System.Action                   <>9__61_1  // static @ 0x10
METHODS:
  RVA=0x04D41B90  token=0x60003CF  System.Void .cctor()
  RVA=0x041E1670  token=0x60003D0  System.Void .ctor()
  RVA=0x041E1670  token=0x60003D1  System.Void <.ctor>b__61_0(System.Boolean <p0>)
  RVA=0x041E1670  token=0x60003D2  System.Void <.ctor>b__61_1()
  RVA=0x0484F270  token=0x60003D3  UnityEngine.Rendering.DebugManager <.cctor>b__81_0()
END_CLASS

CLASS: DebugActionKeyType
TYPE:  sealed struct
TOKEN: 0x20000A9
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.DebugActionState.DebugActionKeyTypeButton  // const
  public    static  UnityEngine.Rendering.DebugActionState.DebugActionKeyTypeAxis  // const
  public    static  UnityEngine.Rendering.DebugActionState.DebugActionKeyTypeKey  // const
METHODS:
END_CLASS

CLASS: Container
TYPE:  class
TOKEN: 0x20000AD
SIZE:  0x50
EXTENDS: Widget
IMPLEMENTS: IContainer
FIELDS:
  private           UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget><children>k__BackingField  // 0x48
PROPERTIES:
  children  get=0x04D86200  set=0x035A41A0
  panel  get=0x02B2ECC0  set=0x0B2B8EBC
METHODS:
  RVA=0x0B2B8C48  token=0x60003F0  System.Void .ctor()
  RVA=0x0B2B8DB0  token=0x60003F1  System.Void .ctor(System.String displayName, UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> children)
  RVA=0x0B2B88F4  token=0x60003F2  System.Void GenerateQueryPath()
  RVA=0x0B2B8B80  token=0x60003F3  System.Void OnItemAdded(UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> sender, UnityEngine.Rendering.ListChangedEventArgs<UnityEngine.Rendering.DebugUI.Widget> e)
  RVA=0x0B2B8BE4  token=0x60003F4  System.Void OnItemRemoved(UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> sender, UnityEngine.Rendering.ListChangedEventArgs<UnityEngine.Rendering.DebugUI.Widget> e)
  RVA=0x0B2B8A1C  token=0x60003F5  System.Int32 GetHashCode()
END_CLASS

CLASS: ContextMenuItem
TYPE:  sealed struct
TOKEN: 0x20000AF
SIZE:  0x20
FIELDS:
  public            System.String                   displayName  // 0x10
  public            System.Action                   action  // 0x18
METHODS:
END_CLASS

CLASS: Foldout
TYPE:  class
TOKEN: 0x20000AE
SIZE:  0x70
EXTENDS: Container
IMPLEMENTS: IValueField
FIELDS:
  public            System.Boolean                  opened  // 0x50
  public            System.Boolean                  isHeader  // 0x51
  public            System.Collections.Generic.List<UnityEngine.Rendering.DebugUI.Foldout.ContextMenuItem>contextMenuItems  // 0x58
  private           System.String[]                 <columnLabels>k__BackingField  // 0x60
  private           System.String[]                 <columnTooltips>k__BackingField  // 0x68
PROPERTIES:
  isReadOnly  get=0x012081B0
  columnLabels  get=0x04D86280  set=0x063CC164
  columnTooltips  get=0x04D86230  set=0x0485F170
METHODS:
  RVA=0x0B2C0290  token=0x60003FB  System.Void .ctor()
  RVA=0x0B2C0298  token=0x60003FC  System.Void .ctor(System.String displayName, UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> children, System.String[] columnLabels, System.String[] columnTooltips)
  RVA=0x04D86800  token=0x60003FD  System.Boolean GetValue()
  RVA=0x0B2C024C  token=0x60003FE  System.Object UnityEngine.Rendering.DebugUI.IValueField.GetValue()
  RVA=0x0B2C0200  token=0x60003FF  System.Void SetValue(System.Object value)
  RVA=0x04D88C30  token=0x6000400  System.Object ValidateValue(System.Object value)
  RVA=0x04D86840  token=0x6000401  System.Void SetValue(System.Boolean value)
END_CLASS

CLASS: HBox
TYPE:  class
TOKEN: 0x20000B0
SIZE:  0x50
EXTENDS: Container
FIELDS:
METHODS:
  RVA=0x0B2C118C  token=0x6000402  System.Void .ctor()
END_CLASS

CLASS: VBox
TYPE:  class
TOKEN: 0x20000B1
SIZE:  0x50
EXTENDS: Container
FIELDS:
METHODS:
  RVA=0x0B2C7EA0  token=0x6000403  System.Void .ctor()
END_CLASS

CLASS: Row
TYPE:  class
TOKEN: 0x20000B3
SIZE:  0x70
EXTENDS: Foldout
FIELDS:
METHODS:
  RVA=0x0B2C63B8  token=0x600040A  System.Void .ctor()
END_CLASS

CLASS: Table
TYPE:  class
TOKEN: 0x20000B2
SIZE:  0x60
EXTENDS: Container
FIELDS:
  public            System.Boolean                  isReadOnly  // 0x50
  private           System.Boolean[]                m_Header  // 0x58
PROPERTIES:
  VisibleColumns  get=0x0B2C683C
METHODS:
  RVA=0x0B2C67F4  token=0x6000404  System.Void .ctor()
  RVA=0x0B2C67B0  token=0x6000405  System.Void SetColumnVisibility(System.Int32 index, System.Boolean visible)
  RVA=0x0B2C6720  token=0x6000406  System.Boolean GetColumnVisibility(System.Int32 index)
  RVA=0x0B2C6760  token=0x6000408  System.Void OnItemAdded(UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> sender, UnityEngine.Rendering.ListChangedEventArgs<UnityEngine.Rendering.DebugUI.Widget> e)
  RVA=0x0B2C6788  token=0x6000409  System.Void OnItemRemoved(UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> sender, UnityEngine.Rendering.ListChangedEventArgs<UnityEngine.Rendering.DebugUI.Widget> e)
END_CLASS

CLASS: Flags
TYPE:  sealed struct
TOKEN: 0x20000B4
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.DebugUI.FlagsNone  // const
  public    static  UnityEngine.Rendering.DebugUI.FlagsEditorOnly  // const
  public    static  UnityEngine.Rendering.DebugUI.FlagsRuntimeOnly  // const
  public    static  UnityEngine.Rendering.DebugUI.FlagsEditorForceUpdate  // const
METHODS:
END_CLASS

CLASS: NameAndTooltip
TYPE:  sealed struct
TOKEN: 0x20000B6
SIZE:  0x20
FIELDS:
  public            System.String                   name  // 0x10
  public            System.String                   tooltip  // 0x18
METHODS:
END_CLASS

CLASS: Widget
TYPE:  abstract class
TOKEN: 0x20000B5
SIZE:  0x48
FIELDS:
  protected         UnityEngine.Rendering.DebugUI.Panelm_Panel  // 0x10
  protected         UnityEngine.Rendering.DebugUI.IContainerm_Parent  // 0x18
  private           UnityEngine.Rendering.DebugUI.Flags<flags>k__BackingField  // 0x20
  private           System.String                   <displayName>k__BackingField  // 0x28
  private           System.String                   <tooltip>k__BackingField  // 0x30
  private           System.String                   <queryPath>k__BackingField  // 0x38
  public            System.Func<System.Boolean>     isHiddenCallback  // 0x40
PROPERTIES:
  panel  get=0x02B2ECC0  set=0x053908C0
  parent  get=0x0385B100  set=0x05392C40
  flags  get=0x02B70F90  set=0x04D86330
  displayName  get=0x04D86240  set=0x02FDB880
  tooltip  get=0x011F36E0  set=0x05396200
  queryPath  get=0x04D85A50  set=0x046A91B0
  isEditorOnly  get=0x0B2C802C
  isRuntimeOnly  get=0x0B2C8070
  isInactiveInEditor  get=0x0B2C8054
  isHidden  get=0x0B2C8034
  nameAndTooltip  set=0x0B2C807C
METHODS:
  RVA=0x0B2C7F50  token=0x600041B  System.Void GenerateQueryPath()
  RVA=0x0B2C8008  token=0x600041C  System.Int32 GetHashCode()
  RVA=0x041E1670  token=0x600041E  System.Void .ctor()
END_CLASS

CLASS: IContainer
TYPE:  interface
TOKEN: 0x20000B7
FIELDS:
PROPERTIES:
  children  get=-1  // abstract
  displayName  get=-1  // abstract  set=-1  // abstract
  queryPath  get=-1  // abstract
METHODS:
END_CLASS

CLASS: IValueField
TYPE:  interface
TOKEN: 0x20000B8
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000423  System.Object GetValue()
  RVA=-1  // abstract  token=0x6000424  System.Void SetValue(System.Object value)
  RVA=-1  // abstract  token=0x6000425  System.Object ValidateValue(System.Object value)
END_CLASS

CLASS: Button
TYPE:  class
TOKEN: 0x20000B9
SIZE:  0x50
EXTENDS: Widget
FIELDS:
  private           System.Action                   <action>k__BackingField  // 0x48
PROPERTIES:
  action  get=0x04D86200  set=0x035A41A0
METHODS:
  RVA=0x041E1670  token=0x6000428  System.Void .ctor()
END_CLASS

CLASS: Value
TYPE:  class
TOKEN: 0x20000BA
SIZE:  0x58
EXTENDS: Widget
FIELDS:
  private           System.Func<System.Object>      <getter>k__BackingField  // 0x48
  public            System.Single                   refreshRate  // 0x50
PROPERTIES:
  getter  get=0x04D86200  set=0x035A41A0
METHODS:
  RVA=0x0B2C7F08  token=0x600042B  System.Void .ctor()
  RVA=0x0B2C7EE8  token=0x600042C  System.Object GetValue()
END_CLASS

CLASS: Field`1
TYPE:  abstract class
TOKEN: 0x20000BB
EXTENDS: Widget
IMPLEMENTS: IValueField
FIELDS:
  private           System.Func<T>                  <getter>k__BackingField  // 0x0
  private           System.Action<T>                <setter>k__BackingField  // 0x0
  public            System.Action<UnityEngine.Rendering.DebugUI.Field<T>,T>onValueChanged  // 0x0
PROPERTIES:
  getter  get=-1  // not resolved  set=-1  // not resolved
  setter  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000431  System.Object UnityEngine.Rendering.DebugUI.IValueField.ValidateValue(System.Object value)
  RVA=-1  // not resolved  token=0x6000432  T ValidateValue(T value)
  RVA=-1  // not resolved  token=0x6000433  System.Object UnityEngine.Rendering.DebugUI.IValueField.GetValue()
  RVA=-1  // not resolved  token=0x6000434  T GetValue()
  RVA=-1  // not resolved  token=0x6000435  System.Void SetValue(System.Object value)
  RVA=-1  // not resolved  token=0x6000436  System.Void SetValue(T value)
  RVA=-1  // not resolved  token=0x6000437  System.Void .ctor()
END_CLASS

CLASS: BoolField
TYPE:  class
TOKEN: 0x20000BC
SIZE:  0x60
EXTENDS: Field`1
FIELDS:
METHODS:
  RVA=0x0B2B6F64  token=0x6000438  System.Void .ctor()
END_CLASS

CLASS: HistoryBoolField
TYPE:  class
TOKEN: 0x20000BD
SIZE:  0x68
EXTENDS: BoolField
FIELDS:
  private           System.Func<System.Boolean>[]   <historyGetter>k__BackingField  // 0x60
PROPERTIES:
  historyGetter  get=0x04D86280  set=0x063CC164
  historyDepth  get=0x0B2C1214
METHODS:
  RVA=0x0B2C11D4  token=0x600043C  System.Boolean GetHistoryValue(System.Int32 historyIndex)
  RVA=0x0B2C120C  token=0x600043D  System.Void .ctor()
END_CLASS

CLASS: IntField
TYPE:  class
TOKEN: 0x20000BE
SIZE:  0x78
EXTENDS: Field`1
FIELDS:
  public            System.Func<System.Int32>       min  // 0x60
  public            System.Func<System.Int32>       max  // 0x68
  public            System.Int32                    incStep  // 0x70
  public            System.Int32                    intStepMult  // 0x74
METHODS:
  RVA=0x0B2C12E0  token=0x600043E  System.Int32 ValidateValue(System.Int32 value)
  RVA=0x0B2C1330  token=0x600043F  System.Void .ctor()
END_CLASS

CLASS: UIntField
TYPE:  class
TOKEN: 0x20000BF
SIZE:  0x78
EXTENDS: Field`1
FIELDS:
  public            System.Func<System.UInt32>      min  // 0x60
  public            System.Func<System.UInt32>      max  // 0x68
  public            System.UInt32                   incStep  // 0x70
  public            System.UInt32                   intStepMult  // 0x74
METHODS:
  RVA=0x0B2C7E04  token=0x6000440  System.UInt32 ValidateValue(System.UInt32 value)
  RVA=0x0B2C7E54  token=0x6000441  System.Void .ctor()
END_CLASS

CLASS: FloatField
TYPE:  class
TOKEN: 0x20000C0
SIZE:  0x80
EXTENDS: Field`1
FIELDS:
  public            System.Func<System.Single>      min  // 0x60
  public            System.Func<System.Single>      max  // 0x68
  public            System.Single                   incStep  // 0x70
  public            System.Single                   incStepMult  // 0x74
  public            System.Int32                    decimals  // 0x78
METHODS:
  RVA=0x0B2C0174  token=0x6000442  System.Single ValidateValue(System.Single value)
  RVA=0x0B2C01C0  token=0x6000443  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000C2
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Rendering.DebugUI.EnumUtility.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Reflection.FieldInfo,UnityEngine.GUIContent><>9__0_0  // static @ 0x8
METHODS:
  RVA=0x0B2C7D3C  token=0x6000446  System.Void .cctor()
  RVA=0x041E1670  token=0x6000447  System.Void .ctor()
  RVA=0x0B2C7B68  token=0x6000448  UnityEngine.GUIContent <MakeEnumNames>b__0_0(System.Reflection.FieldInfo fieldInfo)
END_CLASS

CLASS: EnumUtility
TYPE:  static class
TOKEN: 0x20000C1
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B2BFDF8  token=0x6000444  UnityEngine.GUIContent[] MakeEnumNames(System.Type enumType)
  RVA=0x0B2BFF34  token=0x6000445  System.Int32[] MakeEnumValues(System.Type enumType)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000C4
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Rendering.DebugUI.EnumField.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.GUIContent,System.String><>9__17_0  // static @ 0x8
METHODS:
  RVA=0x0B2C7DA0  token=0x6000453  System.Void .cctor()
  RVA=0x041E1670  token=0x6000454  System.Void .ctor()
  RVA=0x0B2C7AF4  token=0x6000455  System.String <InitQuickSeparators>b__17_0(UnityEngine.GUIContent x)
END_CLASS

CLASS: EnumField
TYPE:  class
TOKEN: 0x20000C3
SIZE:  0x90
EXTENDS: Field`1
FIELDS:
  public            UnityEngine.GUIContent[]        enumNames  // 0x60
  public            System.Int32[]                  enumValues  // 0x68
  private           System.Int32[]                  quickSeparators  // 0x70
  private           System.Int32[]                  indexes  // 0x78
  private           System.Func<System.Int32>       <getIndex>k__BackingField  // 0x80
  private           System.Action<System.Int32>     <setIndex>k__BackingField  // 0x88
PROPERTIES:
  getIndex  get=0x04D86260  set=0x04CD9970
  setIndex  get=0x04D861F0  set=0x05395D30
  currentIndex  get=0x0B2BFD54  set=0x0B2BFDD4
  autoEnum  set=0x0B2BFD78
METHODS:
  RVA=0x0B2BFB18  token=0x6000450  System.Void InitQuickSeparators()
  RVA=0x0B2BFA60  token=0x6000451  System.Void InitIndexes()
  RVA=0x0B2BFD2C  token=0x6000452  System.Void .ctor()
END_CLASS

CLASS: HistoryEnumField
TYPE:  class
TOKEN: 0x20000C5
SIZE:  0x98
EXTENDS: EnumField
FIELDS:
  private           System.Func<System.Int32>[]     <historyIndexGetter>k__BackingField  // 0x90
PROPERTIES:
  historyIndexGetter  get=0x04D86220  set=0x06402330
  historyDepth  get=0x0B2C1268
METHODS:
  RVA=0x0B2C1224  token=0x6000459  System.Int32 GetHistoryValue(System.Int32 historyIndex)
  RVA=0x0B2C1260  token=0x600045A  System.Void .ctor()
END_CLASS

CLASS: BitField
TYPE:  class
TOKEN: 0x20000C6
SIZE:  0x78
EXTENDS: Field`1
FIELDS:
  private           UnityEngine.GUIContent[]        <enumNames>k__BackingField  // 0x60
  private           System.Int32[]                  <enumValues>k__BackingField  // 0x68
  private           System.Type                     m_EnumType  // 0x70
PROPERTIES:
  enumNames  get=0x04D86280  set=0x063CC164
  enumValues  get=0x04D86230  set=0x0485F170
  enumType  get=0x04D862B0  set=0x0B2B6F0C
METHODS:
  RVA=0x0B2B6EE4  token=0x6000461  System.Void .ctor()
END_CLASS

CLASS: ColorField
TYPE:  class
TOKEN: 0x20000C7
SIZE:  0x70
EXTENDS: Field`1
FIELDS:
  public            System.Boolean                  hdr  // 0x60
  public            System.Boolean                  showAlpha  // 0x61
  public            System.Boolean                  showPicker  // 0x62
  public            System.Single                   incStep  // 0x64
  public            System.Single                   incStepMult  // 0x68
  public            System.Int32                    decimals  // 0x6c
METHODS:
  RVA=0x0B2B7870  token=0x6000462  UnityEngine.Color ValidateValue(UnityEngine.Color value)
  RVA=0x0B2B78CC  token=0x6000463  System.Void .ctor()
END_CLASS

CLASS: Vector2Field
TYPE:  class
TOKEN: 0x20000C8
SIZE:  0x70
EXTENDS: Field`1
FIELDS:
  public            System.Single                   incStep  // 0x60
  public            System.Single                   incStepMult  // 0x64
  public            System.Int32                    decimals  // 0x68
METHODS:
  RVA=0x0B2D8D40  token=0x6000464  System.Void .ctor()
END_CLASS

CLASS: Vector3Field
TYPE:  class
TOKEN: 0x20000C9
SIZE:  0x70
EXTENDS: Field`1
FIELDS:
  public            System.Single                   incStep  // 0x60
  public            System.Single                   incStepMult  // 0x64
  public            System.Int32                    decimals  // 0x68
METHODS:
  RVA=0x0B2D8D80  token=0x6000465  System.Void .ctor()
END_CLASS

CLASS: Vector4Field
TYPE:  class
TOKEN: 0x20000CA
SIZE:  0x70
EXTENDS: Field`1
FIELDS:
  public            System.Single                   incStep  // 0x60
  public            System.Single                   incStepMult  // 0x64
  public            System.Int32                    decimals  // 0x68
METHODS:
  RVA=0x0B2D8DC0  token=0x6000466  System.Void .ctor()
END_CLASS

CLASS: Style
TYPE:  sealed struct
TOKEN: 0x20000CC
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.DebugUI.MessageBox.StyleInfo  // const
  public    static  UnityEngine.Rendering.DebugUI.MessageBox.StyleWarning  // const
  public    static  UnityEngine.Rendering.DebugUI.MessageBox.StyleError  // const
METHODS:
END_CLASS

CLASS: MessageBox
TYPE:  class
TOKEN: 0x20000CB
SIZE:  0x50
EXTENDS: Widget
FIELDS:
  public            UnityEngine.Rendering.DebugUI.MessageBox.Stylestyle  // 0x48
METHODS:
  RVA=0x041E1670  token=0x6000467  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000CE
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Rendering.DebugUI.Panel.<>c<>9  // static @ 0x0
  public    static  System.Action<UnityEngine.Rendering.DebugUI.Panel><>9__29_0  // static @ 0x8
METHODS:
  RVA=0x0B2D8C78  token=0x600047D  System.Void .cctor()
  RVA=0x041E1670  token=0x600047E  System.Void .ctor()
  RVA=0x041E1670  token=0x600047F  System.Void <.ctor>b__29_0(UnityEngine.Rendering.DebugUI.Panel <p0>)
END_CLASS

CLASS: Panel
TYPE:  class
TOKEN: 0x20000CD
SIZE:  0x38
IMPLEMENTS: IContainer System.IComparable`1
FIELDS:
  private           UnityEngine.Rendering.DebugUI.Flags<flags>k__BackingField  // 0x10
  private           System.String                   <displayName>k__BackingField  // 0x18
  private           System.Int32                    <groupIndex>k__BackingField  // 0x20
  private           UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget><children>k__BackingField  // 0x28
  private           System.Action<UnityEngine.Rendering.DebugUI.Panel>onSetDirty  // 0x30
PROPERTIES:
  flags  get=0x02B2E2D0  set=0x014F51F0
  displayName  get=0x0385B100  set=0x05392C40
  groupIndex  get=0x02B70F90  set=0x04D86330
  queryPath  get=0x0385B100
  isEditorOnly  get=0x02F2E070
  isRuntimeOnly  get=0x0B2D1C94
  isInactiveInEditor  get=0x0B2D1C78
  editorForceUpdate  get=0x0B2D1C70
  children  get=0x04D86240  set=0x02FDB880
EVENTS:
  onSetDirty  add=add_onSetDirty  remove=remove_onSetDirty
METHODS:
  RVA=0x0B2D19D4  token=0x6000477  System.Void .ctor()
  RVA=0x0B2D17A0  token=0x6000478  System.Void OnItemAdded(UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> sender, UnityEngine.Rendering.ListChangedEventArgs<UnityEngine.Rendering.DebugUI.Widget> e)
  RVA=0x0B2D17F8  token=0x6000479  System.Void OnItemRemoved(UnityEngine.Rendering.ObservableList<UnityEngine.Rendering.DebugUI.Widget> sender, UnityEngine.Rendering.ListChangedEventArgs<UnityEngine.Rendering.DebugUI.Widget> e)
  RVA=0x0B2D1854  token=0x600047A  System.Void SetDirty()
  RVA=0x0B2D163C  token=0x600047B  System.Int32 GetHashCode()
  RVA=0x0B2D19A8  token=0x600047C  System.Int32 System.IComparable<UnityEngine.Rendering.DebugUI.Panel>.CompareTo(UnityEngine.Rendering.DebugUI.Panel other)
END_CLASS

CLASS: <DoAfterInputModuleUpdated>d__9
TYPE:  sealed class
TOKEN: 0x20000D0
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.Action                   action  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x600048E  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600048F  System.Void System.IDisposable.Dispose()
  RVA=0x0B2D8A10  token=0x6000490  System.Boolean MoveNext()
  RVA=0x0B2D8AC0  token=0x6000492  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <RefreshRuntimeUINextFrame>d__15
TYPE:  sealed class
TOKEN: 0x20000D1
SIZE:  0x20
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000494  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000495  System.Void System.IDisposable.Dispose()
  RVA=0x0B2D8B0C  token=0x6000496  System.Boolean MoveNext()
  RVA=0x0B2D8B90  token=0x6000498  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: SunSourceDirLightOverrideLensFlareData
TYPE:  sealed struct
TOKEN: 0x20000DF
SIZE:  0x40
FIELDS:
  public            System.Boolean                  enable  // 0x10
  public            UnityEngine.Rendering.LensFlareDataSRPlensFlareData  // 0x18
  public            System.Single                   intensity  // 0x20
  public            System.Single                   scale  // 0x24
  public            System.Boolean                  useOcclusion  // 0x28
  public            System.Single                   occlusionRadius  // 0x2c
  public            System.UInt32                   sampleCount  // 0x30
  public            System.Single                   occlusionOffset  // 0x34
  public            System.Boolean                  allowOffScreen  // 0x38
METHODS:
END_CLASS

CLASS: SunSourceDirLightOverrideLightData
TYPE:  sealed struct
TOKEN: 0x20000E0
SIZE:  0x3C
FIELDS:
  public            UnityEngine.Quaternion          rotationLensFlare  // 0x10
  public            UnityEngine.Vector3             dirLightFoward  // 0x20
  public            UnityEngine.Color               color  // 0x2c
METHODS:
END_CLASS

CLASS: SunSourceDirLightOverrideInfo
TYPE:  sealed struct
TOKEN: 0x20000E1
SIZE:  0x78
FIELDS:
  public            UnityEngine.GameObject          dirLightGameObject  // 0x10
  public            UnityEngine.Rendering.LensFlareCommonSRP.SunSourceDirLightOverrideLensFlareDataflareData  // 0x18
  public            UnityEngine.Rendering.LensFlareCommonSRP.SunSourceDirLightOverrideLightDatalightData  // 0x48
METHODS:
END_CLASS

CLASS: <>c__DisplayClass35_0
TYPE:  sealed struct
TOKEN: 0x20000E2
SIZE:  0x18
FIELDS:
  public            UnityEngine.Rendering.LensFlareDataElementSRPelement  // 0x10
METHODS:
END_CLASS

CLASS: <>c__DisplayClass35_1
TYPE:  sealed struct
TOKEN: 0x20000E3
SIZE:  0x2C
FIELDS:
  public            UnityEngine.Vector2             screenPos  // 0x10
  public            System.Single                   position  // 0x18
  public            System.Single                   globalCos0  // 0x1c
  public            System.Single                   globalSin0  // 0x20
  public            System.Single                   combinedScale  // 0x24
  public            System.Single                   usedAspectRatio  // 0x28
METHODS:
END_CLASS

CLASS: <hammersley2dSeq16>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x20000F5
SIZE:  0x110
FIELDS:
  public            System.Single                   FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: Hammersley2dSeq16
TYPE:  sealed struct
TOKEN: 0x20000F4
SIZE:  0x110
FIELDS:
  public            UnityEngine.Rendering.Hammersley.Hammersley2dSeq16.<hammersley2dSeq16>e__FixedBufferhammersley2dSeq16  // 0x10
METHODS:
END_CLASS

CLASS: <hammersley2dSeq32>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x20000F7
SIZE:  0x210
FIELDS:
  public            System.Single                   FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: Hammersley2dSeq32
TYPE:  sealed struct
TOKEN: 0x20000F6
SIZE:  0x210
FIELDS:
  public            UnityEngine.Rendering.Hammersley.Hammersley2dSeq32.<hammersley2dSeq32>e__FixedBufferhammersley2dSeq32  // 0x10
METHODS:
END_CLASS

CLASS: <hammersley2dSeq64>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x20000F9
SIZE:  0x410
FIELDS:
  public            System.Single                   FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: Hammersley2dSeq64
TYPE:  sealed struct
TOKEN: 0x20000F8
SIZE:  0x410
FIELDS:
  public            UnityEngine.Rendering.Hammersley.Hammersley2dSeq64.<hammersley2dSeq64>e__FixedBufferhammersley2dSeq64  // 0x10
METHODS:
END_CLASS

CLASS: <hammersley2dSeq256>e__FixedBuffer
TYPE:  sealed struct
TOKEN: 0x20000FB
SIZE:  0x1010
FIELDS:
  public            System.Single                   FixedElementField  // 0x10
METHODS:
END_CLASS

CLASS: Hammersley2dSeq256
TYPE:  sealed struct
TOKEN: 0x20000FA
SIZE:  0x1010
FIELDS:
  public            UnityEngine.Rendering.Hammersley.Hammersley2dSeq256.<hammersley2dSeq256>e__FixedBufferhammersley2dSeq256  // 0x10
METHODS:
END_CLASS

CLASS: BlitType
TYPE:  sealed struct
TOKEN: 0x2000100
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.PowerOfTwoTextureAtlas.BlitTypePadding  // const
  public    static  UnityEngine.Rendering.PowerOfTwoTextureAtlas.BlitTypePaddingMultiply  // const
  public    static  UnityEngine.Rendering.PowerOfTwoTextureAtlas.BlitTypeOctahedralPadding  // const
  public    static  UnityEngine.Rendering.PowerOfTwoTextureAtlas.BlitTypeOctahedralPaddingMultiply  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000101
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Rendering.PowerOfTwoTextureAtlas.<>c<>9  // static @ 0x0
  public    static  System.Comparison<System.ValueTuple<System.Int32,UnityEngine.Vector2Int>><>9__23_0  // static @ 0x8
METHODS:
  RVA=0x0B2D8CDC  token=0x6000539  System.Void .cctor()
  RVA=0x041E1670  token=0x600053A  System.Void .ctor()
  RVA=0x0B2D8BDC  token=0x600053B  System.Int32 <RelayoutEntries>b__23_0(System.ValueTuple<System.Int32,UnityEngine.Vector2Int> c1, System.ValueTuple<System.Int32,UnityEngine.Vector2Int> c2)
END_CLASS

CLASS: AtlasNode
TYPE:  class
TOKEN: 0x2000106
SIZE:  0x30
FIELDS:
  public            UnityEngine.Rendering.AtlasAllocator.AtlasNodem_RightChild  // 0x10
  public            UnityEngine.Rendering.AtlasAllocator.AtlasNodem_BottomChild  // 0x18
  public            UnityEngine.Vector4             m_Rect  // 0x20
METHODS:
  RVA=0x0B2C9070  token=0x6000562  UnityEngine.Rendering.AtlasAllocator.AtlasNode Allocate(UnityEngine.Rendering.ObjectPool<UnityEngine.Rendering.AtlasAllocator.AtlasNode>& pool, System.Int32 width, System.Int32 height, System.Boolean powerOfTwoPadding)
  RVA=0x0B2C99C8  token=0x6000563  System.Void Release(UnityEngine.Rendering.ObjectPool<UnityEngine.Rendering.AtlasAllocator.AtlasNode>& pool)
  RVA=0x04DB8D50  token=0x6000564  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000107
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Rendering.AtlasAllocator.<>c<>9  // static @ 0x0
  public    static  UnityEngine.Events.UnityAction<UnityEngine.Rendering.AtlasAllocator.AtlasNode><>9__6_0  // static @ 0x8
  public    static  UnityEngine.Events.UnityAction<UnityEngine.Rendering.AtlasAllocator.AtlasNode><>9__6_1  // static @ 0x10
METHODS:
  RVA=0x0B2D8C14  token=0x6000565  System.Void .cctor()
  RVA=0x041E1670  token=0x6000566  System.Void .ctor()
  RVA=0x041E1670  token=0x6000567  System.Void <.ctor>b__6_0(UnityEngine.Rendering.AtlasAllocator.AtlasNode _)
  RVA=0x041E1670  token=0x6000568  System.Void <.ctor>b__6_1(UnityEngine.Rendering.AtlasAllocator.AtlasNode _)
END_CLASS

CLASS: BlitType
TYPE:  sealed struct
TOKEN: 0x2000109
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.Texture2DAtlas.BlitTypeDefault  // const
  public    static  UnityEngine.Rendering.Texture2DAtlas.BlitTypeCubeTo2DOctahedral  // const
  public    static  UnityEngine.Rendering.Texture2DAtlas.BlitTypeSingleChannel  // const
  public    static  UnityEngine.Rendering.Texture2DAtlas.BlitTypeCubeTo2DOctahedralSingleChannel  // const
METHODS:
END_CLASS

CLASS: AtlasNodePool
TYPE:  class
TOKEN: 0x200010B
SIZE:  0x20
FIELDS:
  private           UnityEngine.Rendering.AtlasAllocatorDynamic.AtlasNode[]m_Nodes  // 0x10
  private           System.Int16                    m_Next  // 0x18
  private           System.Int16                    m_FreelistHead  // 0x1a
METHODS:
  RVA=0x0B2C9008  token=0x600058F  System.Void .ctor(System.Int16 capacity)
  RVA=0x0B2C8FF0  token=0x6000590  System.Void Dispose()
  RVA=0x04DB8D20  token=0x6000591  System.Void Clear()
  RVA=0x0B2C8EA4  token=0x6000592  System.Int16 AtlasNodeCreate(System.Int16 parent)
  RVA=0x0B2C8FA4  token=0x6000593  System.Void AtlasNodeFree(System.Int16 index)
END_CLASS

CLASS: AtlasNodeFlags
TYPE:  sealed struct
TOKEN: 0x200010D
SIZE:  0x14
FIELDS:
  public            System.UInt32                   value__  // 0x10
  public    static  UnityEngine.Rendering.AtlasAllocatorDynamic.AtlasNode.AtlasNodeFlagsIsOccupied  // const
METHODS:
END_CLASS

CLASS: AtlasNode
TYPE:  sealed struct
TOKEN: 0x200010C
SIZE:  0x30
FIELDS:
  public            System.Int16                    m_Self  // 0x10
  public            System.Int16                    m_Parent  // 0x12
  public            System.Int16                    m_LeftChild  // 0x14
  public            System.Int16                    m_RightChild  // 0x16
  public            System.Int16                    m_FreelistNext  // 0x18
  public            System.UInt16                   m_Flags  // 0x1a
  public            UnityEngine.Vector4             m_Rect  // 0x20
METHODS:
  RVA=0x0B2C9A90  token=0x6000594  System.Void .ctor(System.Int16 self, System.Int16 parent)
  RVA=0x0B2C9868  token=0x6000595  System.Boolean IsOccupied()
  RVA=0x04DB8D40  token=0x6000596  System.Void SetIsOccupied()
  RVA=0x04DB8D30  token=0x6000597  System.Void ClearIsOccupied()
  RVA=0x0B2C97C8  token=0x6000598  System.Boolean IsLeafNode()
  RVA=0x0B2C93D0  token=0x6000599  System.Int16 Allocate(UnityEngine.Rendering.AtlasAllocatorDynamic.AtlasNodePool pool, System.Int32 width, System.Int32 height)
  RVA=0x0B2C9924  token=0x600059A  System.Void ReleaseChildren(UnityEngine.Rendering.AtlasAllocatorDynamic.AtlasNodePool pool)
  RVA=0x0B2C9870  token=0x600059B  System.Void ReleaseAndMerge(UnityEngine.Rendering.AtlasAllocatorDynamic.AtlasNodePool pool)
  RVA=0x0B2C97D4  token=0x600059C  System.Boolean IsMergeNeeded(UnityEngine.Rendering.AtlasAllocatorDynamic.AtlasNodePool pool)
END_CLASS

CLASS: BlitShaderIDs
TYPE:  static class
TOKEN: 0x200011A
SIZE:  0x10
FIELDS:
  public    static readonly System.Int32                    _BlitTexture  // static @ 0x0
  public    static readonly System.Int32                    _BlitCubeTexture  // static @ 0x4
  public    static readonly System.Int32                    _BlitScaleBias  // static @ 0x8
  public    static readonly System.Int32                    _BlitScaleBiasRt  // static @ 0xc
  public    static readonly System.Int32                    _BlitMipLevel  // static @ 0x10
  public    static readonly System.Int32                    _BlitTextureSize  // static @ 0x14
  public    static readonly System.Int32                    _BlitPaddingSize  // static @ 0x18
  public    static readonly System.Int32                    _InputDepth  // static @ 0x1c
METHODS:
  RVA=0x0B2DA180  token=0x600065E  System.Void .cctor()
END_CLASS

CLASS: Sections
TYPE:  static class
TOKEN: 0x200011F
SIZE:  0x10
FIELDS:
  public    static  System.Int32                    section1  // const
  public    static  System.Int32                    section2  // const
  public    static  System.Int32                    section3  // const
  public    static  System.Int32                    section4  // const
  public    static  System.Int32                    section5  // const
  public    static  System.Int32                    section6  // const
  public    static  System.Int32                    section7  // const
  public    static  System.Int32                    section8  // const
METHODS:
END_CLASS

CLASS: Priorities
TYPE:  static class
TOKEN: 0x2000120
SIZE:  0x10
FIELDS:
  public    static  System.Int32                    assetsCreateShaderMenuPriority  // const
  public    static  System.Int32                    assetsCreateRenderingMenuPriority  // const
  public    static  System.Int32                    editMenuPriority  // const
  public    static  System.Int32                    gameObjectMenuPriority  // const
  public    static  System.Int32                    srpLensFlareMenuPriority  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000121
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Rendering.CoreUtils.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Reflection.Assembly,System.Collections.Generic.IEnumerable<System.Type>><>9__81_0  // static @ 0x8
METHODS:
  RVA=0x04D44A70  token=0x60006C5  System.Void .cctor()
  RVA=0x041E1670  token=0x60006C6  System.Void .ctor()
  RVA=0x03B0F0B0  token=0x60006C7  System.Collections.Generic.IEnumerable<System.Type> <GetAllAssemblyTypes>b__81_0(System.Reflection.Assembly t)
END_CLASS

CLASS: <>c__82`1
TYPE:  sealed class
TOKEN: 0x2000122
FIELDS:
  public    static readonly UnityEngine.Rendering.CoreUtils.<>c__82<T><>9  // static @ 0x0
  public    static  System.Func<System.Type,System.Boolean><>9__82_0  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60006C8  System.Void .cctor()
  RVA=-1  // not resolved  token=0x60006C9  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60006CA  System.Boolean <GetAllTypesDerivedFrom>b__82_0(System.Type t)
END_CLASS

CLASS: ShaderConstants
TYPE:  static class
TOKEN: 0x2000125
SIZE:  0x10
FIELDS:
  public    static readonly System.Int32                    _FsrEasuConstants0  // static @ 0x0
  public    static readonly System.Int32                    _FsrEasuConstants1  // static @ 0x4
  public    static readonly System.Int32                    _FsrEasuConstants2  // static @ 0x8
  public    static readonly System.Int32                    _FsrEasuConstants3  // static @ 0xc
  public    static readonly System.Int32                    _FsrRcasConstants  // static @ 0x10
METHODS:
  RVA=0x0B2E6168  token=0x60006D0  System.Void .cctor()
END_CLASS

CLASS: Segment
TYPE:  class
TOKEN: 0x2000127
SIZE:  0x28
FIELDS:
  public            System.Single                   offsetX  // 0x10
  public            System.Single                   offsetY  // 0x14
  public            System.Single                   scaleX  // 0x18
  public            System.Single                   scaleY  // 0x1c
  public            System.Single                   lnA  // 0x20
  public            System.Single                   B  // 0x24
METHODS:
  RVA=0x0B2E6124  token=0x60006E0  System.Single Eval(System.Single x)
  RVA=0x041E1670  token=0x60006E1  System.Void .ctor()
END_CLASS

CLASS: DirectParams
TYPE:  sealed struct
TOKEN: 0x2000128
SIZE:  0x30
FIELDS:
  private           System.Single                   x0  // 0x10
  private           System.Single                   y0  // 0x14
  private           System.Single                   x1  // 0x18
  private           System.Single                   y1  // 0x1c
  private           System.Single                   W  // 0x20
  private           System.Single                   overshootX  // 0x24
  private           System.Single                   overshootY  // 0x28
  private           System.Single                   gamma  // 0x2c
METHODS:
END_CLASS

CLASS: Uniforms
TYPE:  class
TOKEN: 0x2000129
SIZE:  0x18
FIELDS:
  private           UnityEngine.Rendering.HableCurveparent  // 0x10
PROPERTIES:
  curve  get=0x0B2E68B8
  toeSegmentA  get=0x0B2E6AD8
  toeSegmentB  get=0x0B2E6B64
  midSegmentA  get=0x0B2E68F0
  midSegmentB  get=0x0B2E697C
  shoSegmentA  get=0x0B2E69E4
  shoSegmentB  get=0x0B2E6A70
METHODS:
  RVA=0x053908C0  token=0x60006E2  System.Void .ctor(UnityEngine.Rendering.HableCurve parent)
END_CLASS

CLASS: <>c__DisplayClass10_0
TYPE:  sealed struct
TOKEN: 0x200012E
SIZE:  0x28
FIELDS:
  public            UnityEngine.Rendering.MeshGizmo <>4__this  // 0x10
  public            UnityEngine.Color               color  // 0x18
METHODS:
END_CLASS

CLASS: Package
TYPE:  sealed struct
TOKEN: 0x2000130
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.ReloadAttribute.PackageBuiltin  // const
  public    static  UnityEngine.Rendering.ReloadAttribute.PackageRoot  // const
METHODS:
END_CLASS

CLASS: Indent
TYPE:  sealed class
TOKEN: 0x200013D
SIZE:  0x18
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
  public    readonly System.Int32                    relativeAmount  // 0x10
METHODS:
  RVA=0x014F51F0  token=0x600073B  System.Void .ctor(System.Int32 relativeAmount)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200013E
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Rendering.VolumeComponent.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Reflection.FieldInfo,System.Int32><>9__10_0  // static @ 0x8
METHODS:
  RVA=0x04D4A770  token=0x600073C  System.Void .cctor()
  RVA=0x041E1670  token=0x600073D  System.Void .ctor()
  RVA=0x03E64790  token=0x600073E  System.Int32 <FindParameters>b__10_0(System.Reflection.FieldInfo t)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000140
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Rendering.VolumeManager.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Type,System.Boolean><>9__25_0  // static @ 0x8
  public    static  System.Predicate<UnityEngine.Rendering.Volume><>9__38_0  // static @ 0x10
METHODS:
  RVA=0x04D4C330  token=0x600075C  System.Void .cctor()
  RVA=0x041E1670  token=0x600075D  System.Void .ctor()
  RVA=0x047039E0  token=0x600075E  System.Boolean <ReloadBaseTypes>b__25_0(System.Type t)
  RVA=0x0B2E686C  token=0x600075F  System.Boolean <GetVolumes>b__38_0(UnityEngine.Rendering.Volume v)
  RVA=0x046FE1E0  token=0x6000760  UnityEngine.Rendering.VolumeManager <.cctor>b__42_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200016C
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Rendering.VolumeProfile.<>c<>9  // static @ 0x0
  public    static  System.Predicate<UnityEngine.Rendering.VolumeComponent><>9__2_0  // static @ 0x8
METHODS:
  RVA=0x04D47890  token=0x60007E8  System.Void .cctor()
  RVA=0x041E1670  token=0x60007E9  System.Void .ctor()
  RVA=0x0463BDE0  token=0x60007EA  System.Boolean <OnEnable>b__2_0(UnityEngine.Rendering.VolumeComponent x)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000176
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Rendering.UI.DebugUIHandlerCanvas.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.Rendering.DebugUI.Widget,System.Boolean><>9__12_0  // static @ 0x8
  public    static  System.Action<UnityEngine.Rendering.UI.DebugUIHandlerPanel><>9__15_0  // static @ 0x10
METHODS:
  RVA=0x0B2EE3DC  token=0x6000821  System.Void .cctor()
  RVA=0x041E1670  token=0x6000822  System.Void .ctor()
  RVA=0x0B2EE308  token=0x6000823  System.Boolean <Rebuild>b__12_0(UnityEngine.Rendering.DebugUI.Widget x)
  RVA=0x0B2EE2D4  token=0x6000824  System.Void <ActivatePanel>b__15_0(UnityEngine.Rendering.UI.DebugUIHandlerPanel p)
END_CLASS

CLASS: <>c__DisplayClass14_0
TYPE:  sealed class
TOKEN: 0x2000177
SIZE:  0x18
FIELDS:
  public            System.String                   queryPath  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000825  System.Void .ctor()
  RVA=0x0B2EE338  token=0x6000826  System.Boolean <GetWidgetFromPath>b__0(UnityEngine.Rendering.UI.DebugUIHandlerWidget w)
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x200017A
SIZE:  0x18
FIELDS:
  public            UnityEngine.Rendering.UI.DebugUIHandlerWidgetwidget  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000842  System.Void .ctor()
  RVA=0x0B2EE368  token=0x6000843  System.Boolean <IsDirectChild>b__0(UnityEngine.Rendering.UI.DebugUIHandlerWidget x)
END_CLASS

CLASS: <RefreshAfterSanitization>d__4
TYPE:  sealed class
TOKEN: 0x200017D
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            UnityEngine.Rendering.UI.DebugUIHandlerEnumHistory<>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000850  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000851  System.Void System.IDisposable.Dispose()
  RVA=0x0B2EE158  token=0x6000852  System.Boolean MoveNext()
  RVA=0x0B2EE288  token=0x6000854  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x2000188
SIZE:  0x18
FIELDS:
  public            UnityEngine.Rendering.DebugUI.Valuewidget  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000899  System.Void .ctor()
  RVA=0x0B2EE3BC  token=0x600089A  System.Boolean <Toggle>b__0(UnityEngine.Rendering.UI.DebugUIHandlerValue x)
END_CLASS

CLASS: <RefreshAfterSanitization>d__4
TYPE:  sealed class
TOKEN: 0x200018C
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            UnityEngine.Rendering.UI.DebugUIHandlerToggleHistory<>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60008A9  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60008AA  System.Void System.IDisposable.Dispose()
  RVA=0x0B2EE0BC  token=0x60008AB  System.Boolean MoveNext()
  RVA=0x0B2EE23C  token=0x60008AD  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: __StaticArrayInitTypeSize=24
TYPE:  sealed struct
TOKEN: 0x2000196
SIZE:  0x28
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=144
TYPE:  sealed struct
TOKEN: 0x2000197
SIZE:  0xA0
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=256
TYPE:  sealed struct
TOKEN: 0x2000198
SIZE:  0x110
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=512
TYPE:  sealed struct
TOKEN: 0x2000199
SIZE:  0x210
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=1024
TYPE:  sealed struct
TOKEN: 0x200019A
SIZE:  0x410
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=4096
TYPE:  sealed struct
TOKEN: 0x200019B
SIZE:  0x1010
FIELDS:
METHODS:
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x2000195
SIZE:  0x10
FIELDS:
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=25645AC882BD91FEBD5A58E67471A0EF32BFC485447A8A18D61DBA335FDA5A2C097  // static @ 0x0
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=5124A1D5C231BB4DDB64A57CFF5FC45592F845DB22D8DE0DCCA7BB59D822908C39A  // static @ 0x100
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=102490C2D148CDB931199C77ADD692B92324CDFC328EDD1D0AB7FA24830C177A4FF4  // static @ 0x300
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=144BDFF4BF48DBC7214A392CF884F7F2A6073CCDB77CA3EB11BEBB93DA8ED8B2158  // static @ 0x700
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=4096C606E03B5FE8EAD2ECA6BCB45AE684039D928B4EE7C4A03C63D0DF9F94F81DAF  // static @ 0x790
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=24FE78C65211DD0B56A97024FB61111E686EF1FE054AA132BA58E2891AC496F1EE  // static @ 0x1790
METHODS:
END_CLASS

CLASS: Beyond.Rendering.HGShaderQualitySettings
TYPE:  static class
TOKEN: 0x2000005
SIZE:  0x10
FIELDS:
  public    static  Beyond.Rendering.HGShaderQualitySettings.ShaderQualityHgShaderLod  // static @ 0x0
METHODS:
  RVA=0x0B29F78C  token=0x6000006  System.Void .cctor()
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

CLASS: UnityEngine.LightAnchor
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x40
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  System.Single                   k_ArcRadius  // const
  private   static  System.Single                   k_AxisLength  // const
  private   static  System.Single                   k_MaxDistance  // const
  private           System.Single                   m_Distance  // 0x18
  private           UnityEngine.LightAnchor.UpDirectionm_FrameSpace  // 0x1c
  private           UnityEngine.Transform           m_AnchorPositionOverride  // 0x20
  private           UnityEngine.Vector3             m_AnchorPositionOffset  // 0x28
  private           System.Single                   m_Yaw  // 0x34
  private           System.Single                   m_Pitch  // 0x38
  private           System.Single                   m_Roll  // 0x3c
PROPERTIES:
  yaw  get=0x04D8E050  set=0x0B2A0C38
  pitch  get=0x04D88360  set=0x0B2A0B98
  roll  get=0x04D863E0  set=0x0B2A0BE8
  distance  get=0x04D86380  set=0x0B2A0B74
  frameSpace  get=0x04D86310  set=0x04D86320
  anchorPosition  get=0x0B2A09F8
  anchorPositionOverride  get=0x04D862C0  set=0x05390F40
  anchorPositionOffset  get=0x04D90C00  set=0x04D90C20
METHODS:
  RVA=0x0B2A0090  token=0x6000016  System.Single NormalizeAngleDegree(System.Single angle)
  RVA=0x0B2A01A0  token=0x6000017  System.Void SynchronizeOnTransform(UnityEngine.Camera camera)
  RVA=0x0B2A059C  token=0x6000018  System.Void UpdateTransform(UnityEngine.Camera camera, UnityEngine.Vector3 anchor)
  RVA=0x0B29F954  token=0x6000019  UnityEngine.LightAnchor.Axes GetWorldSpaceAxes(UnityEngine.Camera camera, UnityEngine.Vector3 anchor)
  RVA=0x0B2A08FC  token=0x600001A  System.Void Update()
  RVA=0x0B2A00D8  token=0x600001B  System.Void OnDrawGizmosSelected()
  RVA=0x0B2A0678  token=0x600001C  System.Void UpdateTransform(UnityEngine.Vector3 up, UnityEngine.Vector3 right, UnityEngine.Vector3 forward, UnityEngine.Vector3 anchor)
  RVA=0x05393520  token=0x600001D  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.ProbeBrickIndex
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x58
FIELDS:
  private   static  System.Int32                    kMaxSubdivisionLevels  // const
  private   static  System.Int32                    kIndexChunkSize  // const
  private           System.Collections.BitArray     m_IndexChunks  // 0x10
  private           System.Int32                    m_IndexInChunks  // 0x18
  private           System.Int32                    m_NextFreeChunk  // 0x1c
  private           UnityEngine.ComputeBuffer       m_PhysicalIndexBuffer  // 0x20
  private           System.Int32[]                  m_PhysicalIndexBufferData  // 0x28
  private           System.Int32                    <estimatedVMemCost>k__BackingField  // 0x30
  private           UnityEngine.Vector3Int          m_CenterRS  // 0x34
  private           System.Collections.Generic.Dictionary<UnityEngine.Vector3Int,System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickIndex.VoxelMeta>>m_VoxelToBricks  // 0x40
  private           System.Collections.Generic.Dictionary<UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId,UnityEngine.Experimental.Rendering.ProbeBrickIndex.BrickMeta>m_BricksToVoxels  // 0x48
  private           System.Boolean                  m_NeedUpdateIndexComputeBuffer  // 0x50
PROPERTIES:
  estimatedVMemCost  get=0x04D864F0  set=0x04D86740
METHODS:
  RVA=0x0B2A1B84  token=0x6000020  System.Int32 GetVoxelSubdivLevel()
  RVA=0x0B2A2248  token=0x6000021  System.Int32 SizeOfPhysicalIndexFromBudget(UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudget memoryBudget)
  RVA=0x0B2A2C84  token=0x6000022  System.Void .ctor(UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudget memoryBudget)
  RVA=0x0B2A2C54  token=0x6000023  System.Void UploadIndexData()
  RVA=0x0B2A18B4  token=0x6000024  System.Void Clear()
  RVA=0x0B2A1BD4  token=0x6000025  System.Void MapBrickToVoxels(UnityEngine.Experimental.Rendering.ProbeBrickIndex.Brick brick, System.Collections.Generic.HashSet<UnityEngine.Vector3Int> voxels)
  RVA=0x0B2A17B4  token=0x6000026  System.Void ClearVoxel(UnityEngine.Vector3Int pos, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo cellInfo)
  RVA=0x0B2A1B4C  token=0x6000027  System.Void GetRuntimeResources(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RuntimeResources& rr)
  RVA=0x0B2A1754  token=0x6000028  System.Void Cleanup()
  RVA=0x04DB8A20  token=0x6000029  System.Int32 MergeIndex(System.Int32 index, System.Int32 size)
  RVA=0x0B2A1638  token=0x600002A  System.Boolean AssignIndexChunksToCell(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Cell cell, System.Int32 bricksCount, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo& cellUpdateInfo)
  RVA=0x0B2A0C88  token=0x600002B  System.Void AddBricks(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId id, System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickIndex.Brick> bricks, System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc> allocations, System.Int32 allocationSize, System.Int32 poolWidth, System.Int32 poolHeight, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo cellInfo)
  RVA=0x0B2A1DC4  token=0x600002C  System.Void RemoveBricks(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId id, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo cellInfo)
  RVA=0x0B2A2274  token=0x600002D  System.Void UpdateIndexForVoxel(UnityEngine.Vector3Int voxel, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo cellInfo)
  RVA=0x0B2A2858  token=0x600002E  System.Void UpdatePhysicalIndex(UnityEngine.Vector3Int brickMin, UnityEngine.Vector3Int brickMax, System.Int32 value, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo cellInfo)
  RVA=0x0B2A195C  token=0x600002F  System.Void ClipToIndexSpace(UnityEngine.Vector3Int pos, System.Int32 subdiv, UnityEngine.Vector3Int& outMinpos, UnityEngine.Vector3Int& outMaxpos, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo cellInfo)
  RVA=0x0B2A24D4  token=0x6000030  System.Void UpdateIndexForVoxel(UnityEngine.Vector3Int voxel, System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickIndex.ReservedBrick> bricks, System.Collections.Generic.List<System.UInt16> indices, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo cellInfo)
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.ProbeBrickPool
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x88
FIELDS:
  private   static  System.Int32                    kBrickCellCount  // const
  private   static  System.Int32                    kBrickProbeCountPerDim  // const
  private   static  System.Int32                    kBrickProbeCountTotal  // const
  private           System.Int32                    <estimatedVMemCost>k__BackingField  // 0x10
  private   static  System.Int32                    kMaxPoolWidth  // const
  private           System.Int32                    m_AllocationSize  // 0x14
  private           UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudgetm_MemoryBudget  // 0x18
  private           UnityEngine.Experimental.Rendering.ProbeBrickPool.DataLocationm_Pool  // 0x20
  private           UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAllocm_NextFreeChunk  // 0x68
  private           System.Collections.Generic.Stack<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc>m_FreeList  // 0x78
  private           UnityEngine.Experimental.Rendering.ProbeVolumeSHBandsm_SHBands  // 0x80
PROPERTIES:
  estimatedVMemCost  get=0x02B2E2D0  set=0x014F51F0
METHODS:
  RVA=0x0B2A43D0  token=0x6000039  System.Void .ctor(System.Int32 allocationSize, UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudget memoryBudget, UnityEngine.Experimental.Rendering.ProbeVolumeSHBands shBands)
  RVA=0x0B2A3550  token=0x600003A  System.Void EnsureTextureValidity()
  RVA=0x04D882A0  token=0x600003B  System.Int32 GetChunkSize()
  RVA=0x04DB8A60  token=0x600003C  System.Int32 GetChunkSizeInProbeCount()
  RVA=0x04D86570  token=0x600003D  System.Int32 GetPoolWidth()
  RVA=0x04D86560  token=0x600003E  System.Int32 GetPoolHeight()
  RVA=0x04DB8A70  token=0x600003F  UnityEngine.Vector3Int GetPoolDimensions()
  RVA=0x0B2A3F0C  token=0x6000040  System.Void GetRuntimeResources(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RuntimeResources& rr)
  RVA=0x0B2A2FC8  token=0x6000041  System.Void Clear()
  RVA=0x0B2A2E80  token=0x6000042  System.Void Allocate(System.Int32 numberOfBrickChunks, System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc> outAllocations)
  RVA=0x0B2A3404  token=0x6000043  System.Void Deallocate(System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc> allocations)
  RVA=0x0B2A4038  token=0x6000044  System.Void Update(UnityEngine.Experimental.Rendering.ProbeBrickPool.DataLocation source, System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc> srcLocations, System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc> dstLocations, UnityEngine.Experimental.Rendering.ProbeVolumeSHBands bands)
  RVA=0x0B2A3F94  token=0x6000045  UnityEngine.Vector3Int ProbeCountToDataLocSize(System.Int32 numProbes)
  RVA=0x0B2A3018  token=0x6000046  UnityEngine.Experimental.Rendering.ProbeBrickPool.DataLocation CreateDataLocation(System.Int32 numProbes, System.Boolean compressed, UnityEngine.Experimental.Rendering.ProbeVolumeSHBands bands, System.Int32& allocatedBytes)
  RVA=0x0B2A3FF4  token=0x6000047  System.Void SetPixel(UnityEngine.Color[]& data, System.Int32 x, System.Int32 y, System.Int32 z, System.Int32 dataLocWidth, System.Int32 dataLocHeight, UnityEngine.Color value)
  RVA=0x0B2A3638  token=0x6000048  System.Void FillDataLocation(UnityEngine.Experimental.Rendering.ProbeBrickPool.DataLocation& loc, UnityEngine.Rendering.SphericalHarmonicsL2[] shl2, UnityEngine.Experimental.Rendering.ProbeVolumeSHBands bands)
  RVA=0x04DB8A40  token=0x6000049  System.Void DerivePoolSizeFromBudget(System.Int32 allocationSize, UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudget memoryBudget, System.Int32& width, System.Int32& height, System.Int32& depth)
  RVA=0x0B2A2FBC  token=0x600004A  System.Void Cleanup()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.ProbeCellIndices
TYPE:  class
TOKEN: 0x2000015
SIZE:  0x48
FIELDS:
  private   static  System.Int32                    kUintPerEntry  // const
  private           System.Int32                    <estimatedVMemCost>k__BackingField  // 0x10
  private           UnityEngine.ComputeBuffer       m_IndexOfIndicesBuffer  // 0x18
  private           System.UInt32[]                 m_IndexOfIndicesData  // 0x20
  private           UnityEngine.Vector3Int          m_CellCount  // 0x28
  private           UnityEngine.Vector3Int          m_CellMin  // 0x34
  private           System.Int32                    m_CellSizeInMinBricks  // 0x40
  private           System.Boolean                  m_NeedUpdateComputeBuffer  // 0x44
PROPERTIES:
  estimatedVMemCost  get=0x02B2E2D0  set=0x014F51F0
METHODS:
  RVA=0x04D90C00  token=0x600004F  UnityEngine.Vector3Int GetCellIndexDimension()
  RVA=0x04D89640  token=0x6000050  UnityEngine.Vector3Int GetCellMinPosition()
  RVA=0x04DB8A90  token=0x6000051  System.Int32 GetFlatIndex(UnityEngine.Vector3Int normalizedPos)
  RVA=0x0B2A47DC  token=0x6000052  System.Void .ctor(UnityEngine.Vector3Int cellMin, UnityEngine.Vector3Int cellMax, System.Int32 cellSizeInMinBricks)
  RVA=0x0B2A46C8  token=0x6000053  System.Int32 GetFlatIdxForCell(UnityEngine.Vector3Int cellPosition)
  RVA=0x0B2A44F8  token=0x6000054  System.Void AddCell(System.Int32 cellFlatIdx, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo cellUpdateInfo)
  RVA=0x0B2A4764  token=0x6000055  System.Void MarkCellAsUnloaded(System.Int32 cellFlatIdx)
  RVA=0x0B2A47AC  token=0x6000056  System.Void PushComputeData()
  RVA=0x0B2A472C  token=0x6000057  System.Void GetRuntimeResources(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RuntimeResources& rr)
  RVA=0x0B2A4668  token=0x6000058  System.Void Cleanup()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.ProbeVolumeSystemParameters
TYPE:  sealed struct
TOKEN: 0x2000017
SIZE:  0x38
FIELDS:
  public            UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudgetmemoryBudget  // 0x10
  public            UnityEngine.Mesh                probeDebugMesh  // 0x18
  public            UnityEngine.Shader              probeDebugShader  // 0x20
  public            UnityEngine.Experimental.Rendering.ProbeVolumeSceneDatasceneData  // 0x28
  public            UnityEngine.Experimental.Rendering.ProbeVolumeSHBandsshBands  // 0x30
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.ProbeVolumeShadingParameters
TYPE:  sealed struct
TOKEN: 0x2000018
SIZE:  0x20
FIELDS:
  public            System.Single                   normalBias  // 0x10
  public            System.Single                   viewBias  // 0x14
  public            System.Boolean                  scaleBiasByMinDistanceBetweenProbes  // 0x18
  public            System.Single                   samplingNoise  // 0x1c
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudget
TYPE:  sealed struct
TOKEN: 0x2000019
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudgetMemoryBudgetLow  // const
  public    static  UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudgetMemoryBudgetMedium  // const
  public    static  UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudgetMemoryBudgetHigh  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.ProbeVolumeSHBands
TYPE:  sealed struct
TOKEN: 0x200001A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Experimental.Rendering.ProbeVolumeSHBandsSphericalHarmonicsL1  // const
  public    static  UnityEngine.Experimental.Rendering.ProbeVolumeSHBandsSphericalHarmonicsL2  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.ProbeReferenceVolume
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x1A8
FIELDS:
  private   static  System.Int32                    kProbeIndexPoolAllocationSize  // const
  private           System.Boolean                  m_IsInitialized  // 0x10
  private           System.Int32                    m_ID  // 0x14
  private           UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RefVolTransformm_Transform  // 0x18
  private           System.Int32                    m_MaxSubdivision  // 0x78
  private           UnityEngine.Experimental.Rendering.ProbeBrickPoolm_Pool  // 0x80
  private           UnityEngine.Experimental.Rendering.ProbeBrickIndexm_Index  // 0x88
  private           UnityEngine.Experimental.Rendering.ProbeCellIndicesm_CellIndices  // 0x90
  private           System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc>m_TmpSrcChunks  // 0x98
  private           System.Single[]                 m_PositionOffsets  // 0xa0
  private           System.Collections.Generic.Dictionary<UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId,System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc>>m_Registry  // 0xa8
  private           UnityEngine.Bounds              m_CurrGlobalBounds  // 0xb0
  private           System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Cell>cells  // 0xc8
  private           System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Experimental.Rendering.ProbeReferenceVolume.CellChunkInfo>m_ChunkInfo  // 0xd0
  private           UnityEngine.Experimental.Rendering.ProbeVolumeSceneDatasceneData  // 0xd8
  public            System.Action<UnityEngine.Experimental.Rendering.ProbeReferenceVolume.ExtraDataActionInput>retrieveExtraDataAction  // 0xe0
  private           System.Boolean                  m_BricksLoaded  // 0xe8
  private           System.Collections.Generic.Dictionary<UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Cell,UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId>m_CellToBricks  // 0xf0
  private           System.Collections.Generic.Dictionary<UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId,UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo>m_BricksToCellUpdateInfo  // 0xf8
  private           System.Collections.Generic.Dictionary<System.String,UnityEngine.Experimental.Rendering.ProbeVolumeAsset>m_PendingAssetsToBeLoaded  // 0x100
  private           System.Collections.Generic.Dictionary<System.String,UnityEngine.Experimental.Rendering.ProbeVolumeAsset>m_PendingAssetsToBeUnloaded  // 0x108
  private           System.Collections.Generic.Dictionary<System.String,UnityEngine.Experimental.Rendering.ProbeVolumeAsset>m_ActiveAssets  // 0x110
  private           System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeReferenceVolume.CellSortInfo>m_CellsToBeLoaded  // 0x118
  private           System.Collections.Generic.Dictionary<System.Int32,System.Int32>m_CellRefCounting  // 0x120
  private           System.Boolean                  m_NeedLoadAsset  // 0x128
  private           System.Boolean                  m_ProbeReferenceVolumeInit  // 0x129
  private           System.Boolean                  m_EnabledBySRP  // 0x12a
  private           UnityEngine.Experimental.Rendering.ProbeReferenceVolume.InitInfom_PendingInitInfo  // 0x12c
  private           System.Boolean                  m_NeedsIndexRebuild  // 0x144
  private           System.Boolean                  m_HasChangedIndex  // 0x145
  private           System.Int32                    m_CBShaderID  // 0x148
  private           System.Int32                    m_NumberOfCellsLoadedPerFrame  // 0x14c
  private           UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudgetm_MemoryBudget  // 0x150
  private           UnityEngine.Experimental.Rendering.ProbeVolumeSHBandsm_SHBands  // 0x154
  private           System.Boolean                  clearAssetsOnVolumeClear  // 0x158
  private   static  UnityEngine.Experimental.Rendering.ProbeReferenceVolume_instance  // static @ 0x0
  private   static  System.Int32                    kProbesPerBatch  // const
  private   readonly UnityEngine.Experimental.Rendering.ProbeVolumeDebug<debugDisplay>k__BackingField  // 0x160
  private   readonly UnityEngine.Color[]             <subdivisionDebugColors>k__BackingField  // 0x168
  private           UnityEngine.Rendering.DebugUI.Widget[]m_DebugItems  // 0x170
  private           UnityEngine.Mesh                m_DebugMesh  // 0x178
  private           UnityEngine.Material            m_DebugMaterial  // 0x180
  private           System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeReferenceVolume.CellInstancedDebugProbes>m_CellDebugData  // 0x188
  private           UnityEngine.Plane[]             m_DebugFrustumPlanes  // 0x190
  private           System.Single                   dilationValidtyThreshold  // 0x198
  private           System.Collections.Generic.Dictionary<UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Volume,System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickIndex.Brick>>realtimeSubdivisionInfo  // 0x1a0
PROPERTIES:
  isInitialized  get=0x04DB8B40
  enabledBySRP  get=0x04DB8B30
  shBands  get=0x04DB8B50
  memoryBudget  get=0x04DA24D0
  instance  get=0x0B2AA918
  debugDisplay  get=0x04D8D8F0
  subdivisionDebugColors  get=0x04D8DF00
METHODS:
  RVA=0x0B2A79F8  token=0x600005A  System.Void InvalidateAllCellRefs()
  RVA=0x0B2A9F68  token=0x6000060  System.Void SetNumberOfCellsLoadedPerFrame(System.Int32 numberOfCells)
  RVA=0x0B2A7924  token=0x6000061  System.Void Initialize(UnityEngine.Experimental.Rendering.ProbeVolumeSystemParameters& parameters)
  RVA=0x04DB8B20  token=0x6000062  System.Void SetEnableStateFromSRP(System.Boolean srpEnablesPV)
  RVA=0x0B2A6D94  token=0x6000063  System.Void ForceSHBand(UnityEngine.Experimental.Rendering.ProbeVolumeSHBands shBands)
  RVA=0x0B2A5AF8  token=0x6000064  System.Void Cleanup()
  RVA=0x0B2A74B8  token=0x6000065  System.Int32 GetVideoMemoryCost()
  RVA=0x0B2A99CC  token=0x6000066  System.Void RemoveCell(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Cell cell)
  RVA=0x0B2A4E2C  token=0x6000067  System.Void AddCell(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Cell cell, System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc> chunks)
  RVA=0x0B2A5878  token=0x6000068  System.Boolean CheckCompatibilityWithCollection(UnityEngine.Experimental.Rendering.ProbeVolumeAsset asset, System.Collections.Generic.Dictionary<System.String,UnityEngine.Experimental.Rendering.ProbeVolumeAsset> collection)
  RVA=0x0B2A4FBC  token=0x6000069  System.Void AddPendingAssetLoading(UnityEngine.Experimental.Rendering.ProbeVolumeAsset asset)
  RVA=0x0B2A5744  token=0x600006A  System.Void AddPendingAssetRemoval(UnityEngine.Experimental.Rendering.ProbeVolumeAsset asset)
  RVA=0x0B2A9C34  token=0x600006B  System.Void RemovePendingAsset(UnityEngine.Experimental.Rendering.ProbeVolumeAsset asset)
  RVA=0x0B2A82B4  token=0x600006C  System.Void PerformPendingIndexChangeAndInit()
  RVA=0x0B2A9EF8  token=0x600006D  System.Void SetMinBrickAndMaxSubdiv(System.Single minBrickSize, System.Int32 maxSubdiv)
  RVA=0x0B2A7A40  token=0x600006E  System.Void LoadAsset(UnityEngine.Experimental.Rendering.ProbeVolumeAsset asset)
  RVA=0x0B2A8310  token=0x600006F  System.Void PerformPendingLoading()
  RVA=0x0B2A810C  token=0x6000070  System.Void PerformPendingDeletion()
  RVA=0x0B2A6FA0  token=0x6000071  System.Int32 GetNumberOfBricksAtSubdiv(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Cell cell, UnityEngine.Vector3Int& minValidLocalIdxAtMaxRes, UnityEngine.Vector3Int& sizeOfValidIndicesAtMaxRes)
  RVA=0x0B2A6DEC  token=0x6000072  System.Boolean GetCellIndexUpdate(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Cell cell, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo& cellUpdateInfo)
  RVA=0x0B2A7CA8  token=0x6000073  System.Void LoadPendingCells(System.Boolean loadAll)
  RVA=0x0B2A867C  token=0x6000074  System.Void PerformPendingOperations(System.Boolean loadAllCells)
  RVA=0x0B2A7508  token=0x6000075  System.Void InitProbeReferenceVolume(System.Int32 allocationSize, UnityEngine.Experimental.Rendering.ProbeVolumeTextureMemoryBudget memoryBudget, UnityEngine.Experimental.Rendering.ProbeVolumeSHBands shBands)
  RVA=0x0B2AA298  token=0x6000076  System.Void SortPendingCells(UnityEngine.Vector3 cameraPosition)
  RVA=0x04735900  token=0x6000077  System.Void .ctor()
  RVA=0x0B2A73F0  token=0x6000078  UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RuntimeResources GetRuntimeResources()
  RVA=0x0B2A9F7C  token=0x6000079  System.Void SetTRS(UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, System.Single minBrickSize)
  RVA=0x0B2A9EA8  token=0x600007A  System.Void SetMaxSubdivision(System.Int32 maxSubdivision)
  RVA=0x04DB8AF0  token=0x600007B  System.Int32 CellSize(System.Int32 subdivisionLevel)
  RVA=0x0B2A5804  token=0x600007C  System.Single BrickSize(System.Int32 subdivisionLevel)
  RVA=0x04DA60B0  token=0x600007D  System.Single MinBrickSize()
  RVA=0x0B2A80F0  token=0x600007E  System.Single MaxBrickSize()
  RVA=0x04D978B0  token=0x600007F  UnityEngine.Matrix4x4 GetRefSpaceToWS()
  RVA=0x04D96DC0  token=0x6000080  UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RefVolTransform GetTransform()
  RVA=0x04D863C0  token=0x6000081  System.Int32 GetMaxSubdivision()
  RVA=0x0B2A6F8C  token=0x6000082  System.Int32 GetMaxSubdivision(System.Single multiplier)
  RVA=0x0B2A6F70  token=0x6000083  System.Single GetDistanceBetweenProbes(System.Int32 subdivisionLevel)
  RVA=0x0B2A8100  token=0x6000084  System.Single MinDistanceBetweenProbes()
  RVA=0x04D8C8C0  token=0x6000085  System.Boolean DataHasBeenLoaded()
  RVA=0x0B2A5BE4  token=0x6000086  System.Void Clear()
  RVA=0x0B2A4AAC  token=0x6000087  UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId AddBricks(System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickIndex.Brick> bricks, UnityEngine.Experimental.Rendering.ProbeBrickPool.DataLocation dataloc, UnityEngine.Experimental.Rendering.ProbeBrickIndex.CellIndexUpdateInfo cellUpdateInfo, System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeBrickPool.BrickChunkAlloc>& ch_list)
  RVA=0x0B2A9818  token=0x6000088  System.Void ReleaseBricks(UnityEngine.Experimental.Rendering.ProbeReferenceVolume.RegId id)
  RVA=0x0B2AA714  token=0x6000089  System.Void UpdateConstantBuffer(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Experimental.Rendering.ProbeVolumeShadingParameters parameters)
  RVA=0x0B2A5A84  token=0x600008A  System.Void CleanupLoadedData()
  RVA=0x03451490  token=0x600008D  System.Void RenderDebug(UnityEngine.Camera camera)
  RVA=0x0B2A7754  token=0x600008E  System.Void InitializeDebug(UnityEngine.Mesh debugProbeMesh, UnityEngine.Shader debugProbeShader)
  RVA=0x0B2A5A30  token=0x600008F  System.Void CleanupDebug()
  RVA=-1  // generic def  token=0x6000090  System.Void RefreshDebug(UnityEngine.Rendering.DebugUI.Field<T> field, T value)
  RVA=-1  // generic def  token=0x6000091  System.Void DebugCellIndexChanged(UnityEngine.Rendering.DebugUI.Field<T> field, T value)
  RVA=0x0B2A86C0  token=0x6000092  System.Void RegisterDebug()
  RVA=0x0B2AA648  token=0x6000093  System.Void UnregisterDebug(System.Boolean destroyPanel)
  RVA=0x0B2AA04C  token=0x6000094  System.Boolean ShouldCullCell(UnityEngine.Vector3 cellPosition, UnityEngine.Transform cameraTransform, UnityEngine.Plane[] frustumPlanes)
  RVA=0x0B2A685C  token=0x6000095  System.Void DrawProbeDebug(UnityEngine.Camera camera)
  RVA=0x0B2A5B78  token=0x6000096  System.Void ClearDebugData()
  RVA=0x0B2A5CCC  token=0x6000097  System.Void CreateInstancedProbes()
  RVA=0x077CA5E4  token=0x6000098  System.Void OnClearLightingdata()
  RVA=0x04735890  token=0x6000099  System.Void .cctor()
  RVA=0x0B2AA3BC  token=0x600009A  System.Boolean <RegisterDebug>b__119_0()
  RVA=0x0B2AA4BC  token=0x600009B  System.Void <RegisterDebug>b__119_1(System.Boolean value)
  RVA=0x0B2AA588  token=0x600009C  System.Boolean <RegisterDebug>b__119_2()
  RVA=0x0B2AA5A8  token=0x600009D  System.Void <RegisterDebug>b__119_3(System.Boolean value)
  RVA=0x0B2AA5C8  token=0x600009E  System.Single <RegisterDebug>b__119_4()
  RVA=0x0B2AA5E8  token=0x600009F  System.Void <RegisterDebug>b__119_5(System.Single value)
  RVA=0x0B2AA608  token=0x60000A0  System.Boolean <RegisterDebug>b__119_7()
  RVA=0x0B2AA628  token=0x60000A1  System.Void <RegisterDebug>b__119_8(System.Boolean value)
  RVA=0x0B2AA3FC  token=0x60000A2  System.Int32 <RegisterDebug>b__119_9()
  RVA=0x0B2AA3DC  token=0x60000A3  System.Void <RegisterDebug>b__119_10(System.Int32 value)
  RVA=0x0B2AA3FC  token=0x60000A4  System.Int32 <RegisterDebug>b__119_11()
  RVA=0x0B2AA3DC  token=0x60000A5  System.Void <RegisterDebug>b__119_12(System.Int32 value)
  RVA=0x0B2AA41C  token=0x60000A6  System.Single <RegisterDebug>b__119_13()
  RVA=0x0B2AA43C  token=0x60000A7  System.Void <RegisterDebug>b__119_14(System.Single value)
  RVA=0x0B2AA45C  token=0x60000A8  System.Single <RegisterDebug>b__119_17()
  RVA=0x0B2AA47C  token=0x60000A9  System.Void <RegisterDebug>b__119_18(System.Single value)
  RVA=0x0B2AA49C  token=0x60000AA  System.Single <RegisterDebug>b__119_19()
  RVA=0x0B2AA4DC  token=0x60000AB  System.Void <RegisterDebug>b__119_20(System.Single value)
  RVA=0x0B2AA4FC  token=0x60000AC  System.Int32 <RegisterDebug>b__119_22()
  RVA=0x0B2AA51C  token=0x60000AD  System.Void <RegisterDebug>b__119_23(System.Int32 v)
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.DebugProbeShadingMode
TYPE:  sealed struct
TOKEN: 0x2000027
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Experimental.Rendering.DebugProbeShadingModeSH  // const
  public    static  UnityEngine.Experimental.Rendering.DebugProbeShadingModeValidity  // const
  public    static  UnityEngine.Experimental.Rendering.DebugProbeShadingModeValidityOverDilationThreshold  // const
  public    static  UnityEngine.Experimental.Rendering.DebugProbeShadingModeSize  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.ProbeVolumeDebug
TYPE:  class
TOKEN: 0x2000028
SIZE:  0x38
FIELDS:
  public            System.Boolean                  drawProbes  // 0x10
  public            System.Boolean                  drawBricks  // 0x11
  public            System.Boolean                  drawCells  // 0x12
  public            System.Boolean                  realtimeSubdivision  // 0x13
  public            System.Int32                    subdivisionCellUpdatePerFrame  // 0x14
  public            System.Single                   subdivisionDelayInSeconds  // 0x18
  public            UnityEngine.Experimental.Rendering.DebugProbeShadingModeprobeShading  // 0x1c
  public            System.Single                   probeSize  // 0x20
  public            System.Single                   subdivisionViewCullingDistance  // 0x24
  public            System.Single                   probeCullingDistance  // 0x28
  public            System.Int32                    maxSubdivToVisualize  // 0x2c
  public            System.Single                   exposureCompensation  // 0x30
METHODS:
  RVA=0x04DB8B70  token=0x60000C9  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.ProbeReferenceVolumeProfile
TYPE:  sealed class
TOKEN: 0x2000029
SIZE:  0x28
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  private           UnityEngine.Experimental.Rendering.ProbeReferenceVolumeProfile.Versionversion  // 0x18
  public            System.Int32                    simplificationLevels  // 0x1c
  public            System.Single                   minDistanceBetweenProbes  // 0x20
PROPERTIES:
  cellSizeInBricks  get=0x04DB8AB0
  maxSubdivision  get=0x04DB8AE0
  minBrickSize  get=0x0B2A4A90
  cellSizeInMeters  get=0x0B2A4A44
METHODS:
  RVA=0x0B2A4984  token=0x60000CE  System.Void OnEnable()
  RVA=0x0B2A491C  token=0x60000CF  System.Boolean IsEquivalent(UnityEngine.Experimental.Rendering.ProbeReferenceVolumeProfile otherProfile)
  RVA=0x0B2A49D8  token=0x60000D0  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.ProbeVolume
TYPE:  class
TOKEN: 0x200002B
SIZE:  0x80
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Boolean                  globalVolume  // 0x18
  public            UnityEngine.Vector3             size  // 0x1c
  public            System.Single                   geometryDistanceOffset  // 0x28
  public            UnityEngine.LayerMask           objectLayerMask  // 0x2c
  public            System.Int32                    lowestSubdivLevelOverride  // 0x30
  public            System.Int32                    highestSubdivLevelOverride  // 0x34
  public            System.Boolean                  overridesSubdivLevels  // 0x38
  private           System.Boolean                  mightNeedRebaking  // 0x39
  private           UnityEngine.Matrix4x4           cachedTransform  // 0x3c
  private           System.Int32                    cachedHashCode  // 0x7c
METHODS:
  RVA=0x0B2AD4A0  token=0x60000D1  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.ProbeVolumeAsset
TYPE:  class
TOKEN: 0x200002C
SIZE:  0x78
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  protected internal        System.Int32                    m_Version  // 0x18
  private           System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeReferenceVolume.Cell>cells  // 0x20
  private           UnityEngine.Vector3Int          maxCellPosition  // 0x28
  private           UnityEngine.Vector3Int          minCellPosition  // 0x34
  private           UnityEngine.Bounds              globalBounds  // 0x40
  private           UnityEngine.Experimental.Rendering.ProbeVolumeSHBandsbands  // 0x58
  private           System.String                   m_AssetFullPath  // 0x60
  private           System.Int32                    cellSizeInBricks  // 0x68
  private           System.Single                   minDistanceBetweenProbes  // 0x6c
  private           System.Int32                    simplificationLevels  // 0x70
PROPERTIES:
  Version  get=0x011EF5B0
  maxSubdivision  get=0x04DB8B60
  minBrickSize  get=0x0B2AAA54
METHODS:
  RVA=0x0B2AA94C  token=0x60000D5  System.Boolean CompatibleWith(UnityEngine.Experimental.Rendering.ProbeVolumeAsset otherAsset)
  RVA=0x04D86280  token=0x60000D6  System.String GetSerializedFullPath()
  RVA=0x0B2AA9AC  token=0x60000D7  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.ProbeDilationSettings
TYPE:  sealed struct
TOKEN: 0x200002E
SIZE:  0x24
FIELDS:
  public            System.Boolean                  enableDilation  // 0x10
  public            System.Single                   dilationDistance  // 0x14
  public            System.Single                   dilationValidityThreshold  // 0x18
  public            System.Int32                    dilationIterations  // 0x1c
  public            System.Boolean                  squaredDistWeighting  // 0x20
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.VirtualOffsetSettings
TYPE:  sealed struct
TOKEN: 0x200002F
SIZE:  0x1C
FIELDS:
  public            System.Boolean                  useVirtualOffset  // 0x10
  public            System.Single                   outOfGeoOffset  // 0x14
  public            System.Single                   searchMultiplier  // 0x18
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.ProbeVolumeBakingProcessSettings
TYPE:  sealed struct
TOKEN: 0x2000030
SIZE:  0x30
FIELDS:
  public            UnityEngine.Experimental.Rendering.ProbeDilationSettingsdilationSettings  // 0x10
  public            UnityEngine.Experimental.Rendering.VirtualOffsetSettingsvirtualOffsetSettings  // 0x24
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.ProbeVolumeState
TYPE:  sealed struct
TOKEN: 0x2000031
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Experimental.Rendering.ProbeVolumeStateDefault  // const
  public    static  UnityEngine.Experimental.Rendering.ProbeVolumeStateInvalid  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.ProbeVolumePerSceneData
TYPE:  class
TOKEN: 0x2000032
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           System.Collections.Generic.Dictionary<UnityEngine.Experimental.Rendering.ProbeVolumeState,UnityEngine.Experimental.Rendering.ProbeVolumeAsset>assets  // 0x18
  private           System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeVolumePerSceneData.SerializableAssetItem>serializedAssets  // 0x20
  private           UnityEngine.Experimental.Rendering.ProbeVolumeStatem_CurrentState  // 0x28
  private           UnityEngine.Experimental.Rendering.ProbeVolumeStatem_PreviousState  // 0x2c
METHODS:
  RVA=0x0B2AACAC  token=0x60000D8  System.Void OnAfterDeserialize()
  RVA=0x0B2AAE40  token=0x60000D9  System.Void OnBeforeSerialize()
  RVA=0x0B2AB280  token=0x60000DA  System.Void StoreAssetForState(UnityEngine.Experimental.Rendering.ProbeVolumeState state, UnityEngine.Experimental.Rendering.ProbeVolumeAsset asset)
  RVA=0x0B2AAAE8  token=0x60000DB  System.Void InvalidateAllAssets()
  RVA=0x0B2AAA70  token=0x60000DC  UnityEngine.Experimental.Rendering.ProbeVolumeAsset GetCurrentStateAsset()
  RVA=0x0B2AB080  token=0x60000DD  System.Void QueueAssetLoading()
  RVA=0x0B2AB188  token=0x60000DE  System.Void QueueAssetRemoval()
  RVA=0x0B2AB078  token=0x60000DF  System.Void OnEnable()
  RVA=0x0B2AB070  token=0x60000E0  System.Void OnDisable()
  RVA=0x0B2AB070  token=0x60000E1  System.Void OnDestroy()
  RVA=0x0B2AB2E4  token=0x60000E2  System.Void Update()
  RVA=0x0B2AB3F8  token=0x60000E3  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.ProbeVolumeSceneData
TYPE:  class
TOKEN: 0x2000034
SIZE:  0x70
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private   static  System.Reflection.PropertyInfo  s_SceneGUID  // static @ 0x0
  private           System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.SerializableBoundItem>serializedBounds  // 0x10
  private           System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.SerializableHasPVItem>serializedHasVolumes  // 0x18
  private           System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.SerializablePVProfile>serializedProfiles  // 0x20
  private           System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.SerializablePVBakeSettings>serializedBakeSettings  // 0x28
  private           System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.BakingSet>serializedBakingSets  // 0x30
  private           UnityEngine.Object              parentAsset  // 0x38
  private           System.String                   parentSceneDataPropertyName  // 0x40
  public            System.Collections.Generic.Dictionary<System.String,UnityEngine.Bounds>sceneBounds  // 0x48
  private           System.Collections.Generic.Dictionary<System.String,System.Boolean>hasProbeVolumes  // 0x50
  private           System.Collections.Generic.Dictionary<System.String,UnityEngine.Experimental.Rendering.ProbeReferenceVolumeProfile>sceneProfiles  // 0x58
  private           System.Collections.Generic.Dictionary<System.String,UnityEngine.Experimental.Rendering.ProbeVolumeBakingProcessSettings>sceneBakingSettings  // 0x60
  private           System.Collections.Generic.List<UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.BakingSet>bakingSets  // 0x68
METHODS:
  RVA=0x0B2AB520  token=0x60000E4  System.String GetSceneGUID(UnityEngine.SceneManagement.Scene scene)
  RVA=0x0B2AD194  token=0x60000E5  System.Void .ctor(UnityEngine.Object parentAsset, System.String parentSceneDataPropertyName)
  RVA=0x0B2ACA78  token=0x60000E6  System.Void SetParentObject(UnityEngine.Object parent, System.String parentSceneDataPropertyName)
  RVA=0x0B2AB6C0  token=0x60000E7  System.Void OnAfterDeserialize()
  RVA=0x0B2ACD68  token=0x60000E8  System.Void UpdateBakingSets()
  RVA=0x0B2ABF1C  token=0x60000E9  System.Void OnBeforeSerialize()
  RVA=0x0B2AB480  token=0x60000EA  UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.BakingSet CreateNewBakingSet(System.String name)
  RVA=0x0B2AB5E4  token=0x60000EB  System.Void InitializeBakingSet(UnityEngine.Experimental.Rendering.ProbeVolumeSceneData.BakingSet set, System.String name)
  RVA=0x0B2ACAAC  token=0x60000EC  System.Void SyncBakingSetSettings()
  RVA=0x0B2AD0E4  token=0x60000ED  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RendererList
TYPE:  sealed struct
TOKEN: 0x200003B
SIZE:  0x208
FIELDS:
  private   static readonly UnityEngine.Rendering.ShaderTagIds_EmptyName  // static @ 0x0
  public    static readonly UnityEngine.Experimental.Rendering.RendererListnullRendererList  // static @ 0x8
  private           System.Boolean                  <isValid>k__BackingField  // 0x10
  public            UnityEngine.Rendering.CullingResultscullingResult  // 0x18
  public            UnityEngine.Rendering.DrawingSettingsdrawSettings  // 0x28
  public            UnityEngine.Rendering.FilteringSettingsfilteringSettings  // 0x174
  public            System.Nullable<UnityEngine.Rendering.RenderStateBlock>stateBlock  // 0x194
PROPERTIES:
  isValid  get=0x04D883E0  set=0x04D87180
METHODS:
  RVA=0x0B2B5828  token=0x60000F4  UnityEngine.Experimental.Rendering.RendererList Create(UnityEngine.Experimental.Rendering.RendererListDesc& desc)
  RVA=0x0B2B5DC4  token=0x60000F5  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RendererListDesc
TYPE:  sealed struct
TOKEN: 0x200003C
SIZE:  0xD0
FIELDS:
  public            UnityEngine.Rendering.SortingCriteriasortingCriteria  // 0x10
  public            UnityEngine.Rendering.PerObjectDatarendererConfiguration  // 0x14
  public            UnityEngine.Rendering.RenderQueueRangerenderQueueRange  // 0x18
  public            System.Nullable<UnityEngine.Rendering.RenderStateBlock>stateBlock  // 0x20
  public            UnityEngine.Material            overrideMaterial  // 0x90
  public            System.Boolean                  excludeObjectMotionVectors  // 0x98
  public            System.Int32                    layerMask  // 0x9c
  public            System.Int32                    overrideMaterialPassIndex  // 0xa0
  private           UnityEngine.Rendering.CullingResults<cullingResult>k__BackingField  // 0xa8
  private           UnityEngine.Camera              <camera>k__BackingField  // 0xb8
  private           UnityEngine.Rendering.ShaderTagId<passName>k__BackingField  // 0xc0
  private           UnityEngine.Rendering.ShaderTagId[]<passNames>k__BackingField  // 0xc8
PROPERTIES:
  cullingResult  get=0x04D8E900  set=0x04DB8BA0
  camera  get=0x04D86290  set=0x04D84CC0
  passName  get=0x04D8F8E0  set=0x04D8F8F0
  passNames  get=0x04D85EF0  set=0x06AC5AF4
METHODS:
  RVA=0x0B2B57A8  token=0x60000FE  System.Void .ctor(UnityEngine.Rendering.ShaderTagId passName, UnityEngine.Rendering.CullingResults cullingResult, UnityEngine.Camera camera)
  RVA=0x0B2B56F0  token=0x60000FF  System.Void .ctor(UnityEngine.Rendering.ShaderTagId[] passNames, UnityEngine.Rendering.CullingResults cullingResult, UnityEngine.Camera camera)
  RVA=0x0B2B5650  token=0x6000100  System.Boolean IsValid()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.DepthAccess
TYPE:  sealed struct
TOKEN: 0x200003D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Experimental.Rendering.RenderGraphModule.DepthAccessRead  // const
  public    static  UnityEngine.Experimental.Rendering.RenderGraphModule.DepthAccessWrite  // const
  public    static  UnityEngine.Experimental.Rendering.RenderGraphModule.DepthAccessReadWrite  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext
TYPE:  class
TOKEN: 0x200003E
SIZE:  0x30
FIELDS:
  public            UnityEngine.Rendering.ScriptableRenderContextrenderContext  // 0x10
  public            UnityEngine.Rendering.CommandBuffercmd  // 0x18
  public            UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphObjectPoolrenderGraphPool  // 0x20
  public            UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDefaultResourcesdefaultResources  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000101  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphParameters
TYPE:  sealed struct
TOKEN: 0x200003F
SIZE:  0x30
FIELDS:
  public            System.String                   executionName  // 0x10
  public            System.Int32                    currentFrameIndex  // 0x18
  public            System.Boolean                  rendererListCulling  // 0x1c
  public            UnityEngine.Rendering.ScriptableRenderContextscriptableRenderContext  // 0x20
  public            UnityEngine.Rendering.CommandBuffercommandBuffer  // 0x28
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphExecution
TYPE:  sealed struct
TOKEN: 0x2000040
SIZE:  0x18
IMPLEMENTS: System.IDisposable
FIELDS:
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphrenderGraph  // 0x10
METHODS:
  RVA=0x05392320  token=0x6000102  System.Void .ctor(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph renderGraph)
  RVA=0x0B2AE004  token=0x6000103  System.Void Dispose()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDebugParams
TYPE:  class
TOKEN: 0x2000041
SIZE:  0x28
FIELDS:
  private           UnityEngine.Rendering.DebugUI.Widget[]m_DebugItems  // 0x10
  private           UnityEngine.Rendering.DebugUI.Panelm_DebugPanel  // 0x18
  public            System.Boolean                  clearRenderTargetsAtCreation  // 0x20
  public            System.Boolean                  clearRenderTargetsAtRelease  // 0x21
  public            System.Boolean                  disablePassCulling  // 0x22
  public            System.Boolean                  immediateMode  // 0x23
  public            System.Boolean                  enableLogging  // 0x24
  public            System.Boolean                  logFrameInformation  // 0x25
  public            System.Boolean                  logResources  // 0x26
METHODS:
  RVA=0x0B2AD770  token=0x6000104  System.Void RegisterDebug(System.String name, UnityEngine.Rendering.DebugUI.Panel debugPanel)
  RVA=0x0B2ADF94  token=0x6000105  System.Void UnRegisterDebug(System.String name)
  RVA=0x041E1670  token=0x6000106  System.Void .ctor()
  RVA=0x04D866B0  token=0x6000107  System.Boolean <RegisterDebug>b__10_0()
  RVA=0x04D866D0  token=0x6000108  System.Void <RegisterDebug>b__10_1(System.Boolean value)
  RVA=0x04D86AD0  token=0x6000109  System.Boolean <RegisterDebug>b__10_2()
  RVA=0x04D86AE0  token=0x600010A  System.Void <RegisterDebug>b__10_3(System.Boolean value)
  RVA=0x04D8E070  token=0x600010B  System.Boolean <RegisterDebug>b__10_4()
  RVA=0x04D8E080  token=0x600010C  System.Void <RegisterDebug>b__10_5(System.Boolean value)
  RVA=0x04D86870  token=0x600010D  System.Boolean <RegisterDebug>b__10_6()
  RVA=0x04D86880  token=0x600010E  System.Void <RegisterDebug>b__10_7(System.Boolean value)
  RVA=0x0B2ADEDC  token=0x600010F  System.Void <RegisterDebug>b__10_8()
  RVA=0x0B2ADF38  token=0x6000110  System.Void <RegisterDebug>b__10_9()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderFunc`1
TYPE:  sealed class
TOKEN: 0x2000043
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000112  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000113  System.Void Invoke(PassData data, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext renderGraphContext)
  RVA=-1  // runtime  token=0x6000114  System.IAsyncResult BeginInvoke(PassData data, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext renderGraphContext, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6000115  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDebugData
TYPE:  class
TOKEN: 0x2000044
SIZE:  0x20
FIELDS:
  public            System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDebugData.PassDebugData>passList  // 0x10
  public            System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDebugData.ResourceDebugData>[]resourceLists  // 0x18
METHODS:
  RVA=0x0B2AD590  token=0x6000116  System.Void Clear()
  RVA=0x0B2AD6D0  token=0x6000117  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph
TYPE:  class
TOKEN: 0x2000047
SIZE:  0xC0
FIELDS:
  public    static readonly System.Int32                    kMaxMRTCount  // static @ 0x0
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceRegistrym_Resources  // 0x10
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphObjectPoolm_RenderGraphPool  // 0x18
  private           System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPass>m_RenderPasses  // 0x20
  private           System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle>m_RendererLists  // 0x28
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDebugParamsm_DebugParameters  // 0x30
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLoggerm_FrameInformationLogger  // 0x38
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDefaultResourcesm_DefaultResources  // 0x40
  private           System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Rendering.ProfilingSampler>m_DefaultProfilingSamplers  // 0x48
  private           System.Boolean                  m_ExecutionExceptionWasRaised  // 0x50
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContextm_RenderGraphContext  // 0x58
  private           UnityEngine.Rendering.CommandBufferm_PreviousCommandBuffer  // 0x60
  private           System.Int32                    m_CurrentImmediatePassIndex  // 0x68
  private           System.Collections.Generic.List<System.Int32>[]m_ImmediateModeResourceList  // 0x70
  private           UnityEngine.Rendering.DynamicArray<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledResourceInfo>[]m_CompiledResourcesInfos  // 0x78
  private           UnityEngine.Rendering.DynamicArray<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo>m_CompiledPassInfos  // 0x80
  private           System.Collections.Generic.Stack<System.Int32>m_CullingStack  // 0x88
  private           System.Int32                    m_ExecutionCount  // 0x90
  private           System.Int32                    m_CurrentFrameIndex  // 0x94
  private           System.Boolean                  m_HasRenderGraphBegun  // 0x98
  private           System.String                   m_CurrentExecutionName  // 0xa0
  private           System.Boolean                  m_RendererListCulling  // 0xa8
  private           System.Collections.Generic.Dictionary<System.String,UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDebugData>m_DebugData  // 0xb0
  private   static  System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph>s_RegisteredGraphs  // static @ 0x8
  private           System.String                   <name>k__BackingField  // 0xb8
  private   static  System.Boolean                  <requireDebugData>k__BackingField  // static @ 0x10
  private   static  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.OnGraphRegisteredDelegateonGraphRegistered  // static @ 0x18
  private   static  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.OnGraphRegisteredDelegateonGraphUnregistered  // static @ 0x20
  private   static  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.OnExecutionRegisteredDelegateonExecutionRegistered  // static @ 0x28
  private   static  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.OnExecutionRegisteredDelegateonExecutionUnregistered  // static @ 0x30
PROPERTIES:
  name  get=0x04D85EF0  set=0x06AC5AF4
  requireDebugData  get=0x0B2B525C  set=0x0B2B55FC
  defaultResources  get=0x04D85A60
EVENTS:
  onGraphRegistered  add=add_onGraphRegistered  remove=remove_onGraphRegistered
  onGraphUnregistered  add=add_onGraphUnregistered  remove=remove_onGraphUnregistered
  onExecutionRegistered  add=add_onExecutionRegistered  remove=remove_onExecutionRegistered
  onExecutionUnregistered  add=add_onExecutionUnregistered  remove=remove_onExecutionUnregistered
METHODS:
  RVA=0x0B2B4A08  token=0x600011D  System.Void .ctor(System.String name)
  RVA=0x0B2AE638  token=0x600011E  System.Void Cleanup()
  RVA=0x0B2B3A38  token=0x600011F  System.Void RegisterDebug(UnityEngine.Rendering.DebugUI.Panel panel)
  RVA=0x0B2B3D90  token=0x6000120  System.Void UnRegisterDebug()
  RVA=0x0B2B21B4  token=0x6000121  System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph> GetRegisteredRenderGraphs()
  RVA=0x0B2B1CB0  token=0x6000122  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDebugData GetDebugData(System.String executionName)
  RVA=0x0B2B028C  token=0x6000123  System.Void EndFrame()
  RVA=0x0B2B22E4  token=0x6000124  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle ImportTexture(UnityEngine.Rendering.RTHandle rt)
  RVA=0x0B2B2268  token=0x6000125  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle ImportBackbuffer(UnityEngine.Rendering.RenderTargetIdentifier rt)
  RVA=0x0B2AFB10  token=0x6000126  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle CreateTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc& desc)
  RVA=0x0B2AF9F4  token=0x6000127  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle CreateSharedTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc& desc, System.Boolean explicitRelease)
  RVA=0x0B2B3C04  token=0x6000128  System.Void ReleaseSharedTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle texture)
  RVA=0x0B2AFB50  token=0x6000129  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle CreateTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle texture)
  RVA=0x0B2AFA84  token=0x600012A  System.Void CreateTextureIfInvalid(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc& desc, UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle& texture)
  RVA=0x0B2B2204  token=0x600012B  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc GetTextureDesc(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle texture)
  RVA=0x0B2AF904  token=0x600012C  UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle CreateRendererList(UnityEngine.Rendering.RendererUtils.RendererListDesc& desc)
  RVA=0x0B2B22C4  token=0x600012D  UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle ImportComputeBuffer(UnityEngine.ComputeBuffer computeBuffer)
  RVA=0x0B2AF8DC  token=0x600012E  UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle CreateComputeBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferDesc& desc)
  RVA=0x0B2AF884  token=0x600012F  UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle CreateComputeBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle& computeBuffer)
  RVA=0x0B2B1C70  token=0x6000130  UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferDesc GetComputeBufferDesc(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle& computeBuffer)
  RVA=-1  // generic def  token=0x6000131  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphBuilder AddRenderPass(System.String passName, PassData& passData, UnityEngine.Rendering.ProfilingSampler sampler)
  RVA=-1  // generic def  token=0x6000132  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphBuilder AddRenderPass(System.String passName, PassData& passData)
  RVA=0x0B2B377C  token=0x6000133  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphExecution RecordAndExecute(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphParameters& parameters)
  RVA=0x0B2B0A88  token=0x6000134  System.Void Execute()
  RVA=0x0B2AE1F0  token=0x6000135  System.Void BeginProfilingSampler(UnityEngine.Rendering.ProfilingSampler sampler)
  RVA=0x0B2B034C  token=0x6000136  System.Void EndProfilingSampler(UnityEngine.Rendering.ProfilingSampler sampler)
  RVA=0x04D86260  token=0x6000137  UnityEngine.Rendering.DynamicArray<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo> GetCompiledPassInfos()
  RVA=0x0B2AE700  token=0x6000138  System.Void ClearCompiledGraph()
  RVA=0x0B2B252C  token=0x6000139  System.Void InvalidateContext()
  RVA=0x0B2B2D68  token=0x600013A  System.Void OnPassAdded(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPass pass)
  RVA=0x0B2B2318  token=0x6000143  System.Void InitResourceInfosData(UnityEngine.Rendering.DynamicArray<UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledResourceInfo> resourceInfos, System.Int32 count)
  RVA=0x0B2B23B8  token=0x6000144  System.Void InitializeCompilationData()
  RVA=0x0B2AF230  token=0x6000145  System.Void CountReferences()
  RVA=0x0B2AFCEC  token=0x6000146  System.Void CullUnusedPasses()
  RVA=0x0B2B3F1C  token=0x6000147  System.Void UpdatePassSynchronization(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo& currentPassInfo, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo& producerPassInfo, System.Int32 currentPassIndex, System.Int32 lastProducer, System.Int32& intLastSyncIndex)
  RVA=0x0B2B4830  token=0x6000148  System.Void UpdateResourceSynchronization(System.Int32& lastGraphicsPipeSync, System.Int32& lastComputePipeSync, System.Int32 currentPassIndex, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledResourceInfo& resource)
  RVA=0x0B2B1EE0  token=0x6000149  System.Int32 GetLatestProducerIndex(System.Int32 passIndex, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledResourceInfo& info)
  RVA=0x0B2B2014  token=0x600014A  System.Int32 GetLatestValidReadIndex(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledResourceInfo& info)
  RVA=0x0B2B1E10  token=0x600014B  System.Int32 GetFirstValidWriteIndex(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledResourceInfo& info)
  RVA=0x0B2B20E4  token=0x600014C  System.Int32 GetLatestValidWriteIndex(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledResourceInfo& info)
  RVA=0x0B2AF924  token=0x600014D  System.Void CreateRendererLists()
  RVA=0x0B2B3F3C  token=0x600014E  System.Void UpdateResourceAllocationAndSynchronization()
  RVA=0x0B2AE024  token=0x600014F  System.Boolean AreRendererListsEmpty(System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle> rendererLists)
  RVA=0x0B2B3C8C  token=0x6000150  System.Void TryCullPassAtIndex(System.Int32 passIndex)
  RVA=0x0B2AFBF4  token=0x6000151  System.Void CullRendererLists()
  RVA=0x0B2AF138  token=0x6000152  System.Void CompileRenderGraph()
  RVA=0x0B2AE948  token=0x6000153  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo& CompilePassImmediatly(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPass pass)
  RVA=0x0B2B0954  token=0x6000154  System.Void ExecutePassImmediatly(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPass pass)
  RVA=0x0B2B05E0  token=0x6000155  System.Void ExecuteCompiledPass(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo& passInfo, System.Int32 passIndex)
  RVA=0x0B2B0980  token=0x6000156  System.Void ExecuteRenderGraph()
  RVA=0x0B2B3378  token=0x6000157  System.Void PreRenderPassSetRenderTargets(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo& passInfo, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext rgContext)
  RVA=0x0B2B3038  token=0x6000158  System.Void PreRenderPassExecute(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo& passInfo, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext rgContext)
  RVA=0x0B2B2D94  token=0x6000159  System.Void PostRenderPassExecute(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo& passInfo, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext rgContext)
  RVA=0x0B2AE7D0  token=0x600015A  System.Void ClearRenderPasses()
  RVA=0x0B2B3A68  token=0x600015B  System.Void ReleaseImmediateModeResources()
  RVA=0x0B2B2848  token=0x600015C  System.Void LogFrameInformation()
  RVA=0x0B2B2C78  token=0x600015D  System.Void LogRendererListsCreation()
  RVA=0x0B2B29BC  token=0x600015E  System.Void LogRenderPassBegin(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph.CompiledPassInfo& passInfo)
  RVA=0x0B2B2584  token=0x600015F  System.Void LogCulledPasses()
  RVA=0x0B2B1D20  token=0x6000160  UnityEngine.Rendering.ProfilingSampler GetDefaultProfilingSampler(System.String name)
  RVA=0x0B2B3DBC  token=0x6000161  System.Void UpdateImportedResourceLifeTime(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDebugData.ResourceDebugData& data, System.Collections.Generic.List<System.Int32> passList)
  RVA=0x0B2B0CB0  token=0x6000162  System.Void GenerateDebugData()
  RVA=0x0B2AE484  token=0x6000163  System.Void CleanupDebugData()
  RVA=0x0B2B4950  token=0x6000164  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphProfilingScope
TYPE:  sealed struct
TOKEN: 0x200004E
SIZE:  0x28
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.Boolean                  m_Disposed  // 0x10
  private           UnityEngine.Rendering.ProfilingSamplerm_Sampler  // 0x18
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphm_RenderGraph  // 0x20
METHODS:
  RVA=0x0B2C314C  token=0x6000175  System.Void .ctor(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph renderGraph, UnityEngine.Rendering.ProfilingSampler sampler)
  RVA=0x0B2C3140  token=0x6000176  System.Void Dispose()
  RVA=0x0B2C3108  token=0x6000177  System.Void Dispose(System.Boolean disposing)
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphBuilder
TYPE:  sealed struct
TOKEN: 0x200004F
SIZE:  0x30
IMPLEMENTS: System.IDisposable
FIELDS:
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPassm_RenderPass  // 0x10
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceRegistrym_Resources  // 0x18
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphm_RenderGraph  // 0x20
  private           System.Boolean                  m_Disposed  // 0x28
METHODS:
  RVA=0x0B2C18D4  token=0x6000178  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle UseColorBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle& input, System.Int32 index)
  RVA=0x0B2C1950  token=0x6000179  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle UseDepthBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle& input, UnityEngine.Experimental.Rendering.RenderGraphModule.DepthAccess flags)
  RVA=0x0B2C1694  token=0x600017A  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle ReadTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle& input)
  RVA=0x0B2C1A64  token=0x600017B  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle WriteTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle& input)
  RVA=0x0B2C1860  token=0x600017C  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle ReadWriteTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle& input)
  RVA=0x0B2C148C  token=0x600017D  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle CreateTransientTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc& desc)
  RVA=0x0B2C14F8  token=0x600017E  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle CreateTransientTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle& texture)
  RVA=0x0B2C15C8  token=0x600017F  UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle UseRendererList(UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle& input)
  RVA=0x0B2C166C  token=0x6000180  UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle ReadComputeBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle& input)
  RVA=0x0B2C1A1C  token=0x6000181  UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle WriteComputeBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle& input)
  RVA=0x0B2C1438  token=0x6000182  UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle CreateTransientComputeBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferDesc& desc)
  RVA=0x0B2C13B4  token=0x6000183  UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle CreateTransientComputeBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle& computebuffer)
  RVA=-1  // generic def  token=0x6000184  System.Void SetRenderFunc(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderFunc<PassData> renderFunc)
  RVA=0x0B2C1634  token=0x6000185  System.Void EnableAsyncCompute(System.Boolean value)
  RVA=0x0B2C137C  token=0x6000186  System.Void AllowPassCulling(System.Boolean value)
  RVA=0x0B2C1628  token=0x6000187  System.Void Dispose()
  RVA=0x0B2C1398  token=0x6000188  System.Void AllowRendererListCulling(System.Boolean value)
  RVA=0x0B2C15C8  token=0x6000189  UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle DependsOn(UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle& input)
  RVA=0x09B369D8  token=0x600018A  System.Void .ctor(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPass renderPass, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceRegistry resources, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph renderGraph)
  RVA=0x0B2C15F4  token=0x600018B  System.Void Dispose(System.Boolean disposing)
  RVA=0x041E1670  token=0x600018C  System.Void CheckResource(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res, System.Boolean dontCheckTransientReadWrite)
  RVA=0x0B2C1650  token=0x600018D  System.Void GenerateDebugData(System.Boolean value)
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDefaultResources
TYPE:  class
TOKEN: 0x2000050
SIZE:  0xD0
FIELDS:
  private           System.Boolean                  m_IsValid  // 0x10
  private           UnityEngine.Rendering.RTHandle  m_BlackTexture2D  // 0x18
  private           UnityEngine.Rendering.RTHandle  m_WhiteTexture2D  // 0x20
  private           UnityEngine.Rendering.RTHandle  m_ShadowTexture2D  // 0x28
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle<blackTexture>k__BackingField  // 0x30
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle<whiteTexture>k__BackingField  // 0x40
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle<clearTextureXR>k__BackingField  // 0x50
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle<magentaTextureXR>k__BackingField  // 0x60
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle<blackTextureXR>k__BackingField  // 0x70
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle<blackTextureArrayXR>k__BackingField  // 0x80
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle<blackUIntTextureXR>k__BackingField  // 0x90
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle<blackTexture3DXR>k__BackingField  // 0xa0
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle<whiteTextureXR>k__BackingField  // 0xb0
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle<defaultShadowTexture>k__BackingField  // 0xc0
PROPERTIES:
  blackTexture  get=0x04D87480  set=0x04D87490
  whiteTexture  get=0x04D905C0  set=0x04D926F0
  clearTextureXR  get=0x04D8CE10  set=0x04D906B0
  magentaTextureXR  get=0x04DA1280  set=0x04DA1330
  blackTextureXR  get=0x04D8FD40  set=0x04DA1310
  blackTextureArrayXR  get=0x04D90620  set=0x04D9DEE0
  blackUIntTextureXR  get=0x04D9DC90  set=0x04DA12D0
  blackTexture3DXR  get=0x04DA1260  set=0x04DA1300
  whiteTextureXR  get=0x04DA1230  set=0x04DA12B0
  defaultShadowTexture  get=0x04DA1220  set=0x04DA12A0
METHODS:
  RVA=0x0B2C1CA4  token=0x60001A2  System.Void .ctor()
  RVA=0x0B2C1AC4  token=0x60001A3  System.Void Cleanup()
  RVA=0x0B2C1B08  token=0x60001A4  System.Void InitializeForRendering(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraph renderGraph)
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogIndent
TYPE:  sealed struct
TOKEN: 0x2000051
SIZE:  0x28
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.Int32                    m_Indentation  // 0x10
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLoggerm_Logger  // 0x18
  private           System.Boolean                  m_Disposed  // 0x20
METHODS:
  RVA=0x0B2C1E08  token=0x60001A5  System.Void .ctor(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger logger, System.Int32 indentation)
  RVA=0x0B2C1DC8  token=0x60001A6  System.Void Dispose()
  RVA=0x0B2C1DD4  token=0x60001A7  System.Void Dispose(System.Boolean disposing)
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger
TYPE:  class
TOKEN: 0x2000052
SIZE:  0x28
FIELDS:
  private           System.Collections.Generic.Dictionary<System.String,System.Text.StringBuilder>m_LogMap  // 0x10
  private           System.Text.StringBuilder       m_CurrentBuilder  // 0x18
  private           System.Int32                    m_CurrentIndentation  // 0x20
METHODS:
  RVA=0x0B2C2134  token=0x60001A8  System.Void Initialize(System.String logName)
  RVA=0x0B2C20D8  token=0x60001A9  System.Void IncrementIndentation(System.Int32 value)
  RVA=0x0B2C1E44  token=0x60001AA  System.Void DecrementIndentation(System.Int32 value)
  RVA=0x0B2C2224  token=0x60001AB  System.Void LogLine(System.String format, System.Object[] args)
  RVA=0x0B2C2048  token=0x60001AC  System.String GetLog(System.String logName)
  RVA=0x0B2C1EB0  token=0x60001AD  System.String GetAllLogs()
  RVA=0x0B2C22A8  token=0x60001AE  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphObjectPool
TYPE:  sealed class
TOKEN: 0x2000053
SIZE:  0x28
FIELDS:
  private           System.Collections.Generic.Dictionary<System.ValueTuple<System.Type,System.Int32>,System.Collections.Generic.Stack<System.Object>>m_ArrayPool  // 0x10
  private           System.Collections.Generic.List<System.ValueTuple<System.Object,System.ValueTuple<System.Type,System.Int32>>>m_AllocatedArrays  // 0x18
  private           System.Collections.Generic.List<UnityEngine.MaterialPropertyBlock>m_AllocatedMaterialPropertyBlocks  // 0x20
METHODS:
  RVA=0x0B2C2800  token=0x60001AF  System.Void .ctor()
  RVA=-1  // generic def  token=0x60001B0  T[] GetTempArray(System.Int32 size)
  RVA=0x0B2C23C8  token=0x60001B1  UnityEngine.MaterialPropertyBlock GetTempMaterialPropertyBlock()
  RVA=0x0B2C2484  token=0x60001B2  System.Void ReleaseAllTempAlloc()
  RVA=-1  // generic def  token=0x60001B3  T Get()
  RVA=-1  // generic def  token=0x60001B4  System.Void Release(T value)
  RVA=0x0B2C2320  token=0x60001B5  System.Void Cleanup()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPass
TYPE:  abstract class
TOKEN: 0x2000056
SIZE:  0x80
FIELDS:
  private           System.String                   <name>k__BackingField  // 0x10
  private           System.Int32                    <index>k__BackingField  // 0x18
  private           UnityEngine.Rendering.ProfilingSampler<customSampler>k__BackingField  // 0x20
  private           System.Boolean                  <enableAsyncCompute>k__BackingField  // 0x28
  private           System.Boolean                  <allowPassCulling>k__BackingField  // 0x29
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle<depthBuffer>k__BackingField  // 0x2c
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle[]<colorBuffers>k__BackingField  // 0x40
  private           System.Int32                    <colorBufferMaxIndex>k__BackingField  // 0x48
  private           System.Int32                    <refCount>k__BackingField  // 0x4c
  private           System.Boolean                  <generateDebugData>k__BackingField  // 0x50
  private           System.Boolean                  <allowRendererListCulling>k__BackingField  // 0x51
  public            System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle>[]resourceReadLists  // 0x58
  public            System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle>[]resourceWriteLists  // 0x60
  public            System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle>[]transientResourceList  // 0x68
  public            System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle>usedRendererListList  // 0x70
  public            System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle>dependsOnRendererListList  // 0x78
PROPERTIES:
  name  get=0x02B2ECC0  set=0x053908C0
  index  get=0x011EF5B0  set=0x011EF9B0
  customSampler  get=0x04D862C0  set=0x05390F40
  enableAsyncCompute  get=0x011F33C0  set=0x011F33D0
  allowPassCulling  get=0x04D867B0  set=0x04D867C0
  depthBuffer  get=0x04D8D370  set=0x04D8D3A0
  colorBuffers  get=0x04D85A60  set=0x042B4AE0
  colorBufferMaxIndex  get=0x04D863D0  set=0x04D86450
  refCount  get=0x04D86600  set=0x04D86630
  generateDebugData  get=0x04D86800  set=0x04D86840
  allowRendererListCulling  get=0x04D867F0  set=0x04D86830
METHODS:
  RVA=-1  // generic def  token=0x60001C1  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderFunc<PassData> GetExecuteDelegate()
  RVA=-1  // abstract  token=0x60001C2  System.Void Execute(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext renderGraphContext)
  RVA=-1  // abstract  token=0x60001C3  System.Void Release(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphObjectPool pool)
  RVA=-1  // abstract  token=0x60001C4  System.Boolean HasRenderFunc()
  RVA=0x0B2C2E80  token=0x60001DB  System.Void .ctor()
  RVA=0x0B2C2AD4  token=0x60001DC  System.Void Clear()
  RVA=0x0B2C29A4  token=0x60001DD  System.Void AddResourceWrite(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res)
  RVA=0x0B2C290C  token=0x60001DE  System.Void AddResourceRead(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res)
  RVA=0x0B2C2A3C  token=0x60001DF  System.Void AddTransientResource(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res)
  RVA=0x0B2C2E2C  token=0x60001E0  System.Void UseRendererList(UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle rendererList)
  RVA=0x0B2C2CE8  token=0x60001E1  System.Void DependsOnRendererList(UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle rendererList)
  RVA=0x011F33D0  token=0x60001E2  System.Void EnableAsyncCompute(System.Boolean value)
  RVA=0x04D867C0  token=0x60001E3  System.Void AllowPassCulling(System.Boolean value)
  RVA=0x04D86830  token=0x60001E4  System.Void AllowRendererListCulling(System.Boolean value)
  RVA=0x04D86840  token=0x60001E5  System.Void GenerateDebugData(System.Boolean value)
  RVA=0x0B2C2D3C  token=0x60001E6  System.Void SetColorBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle resource, System.Int32 index)
  RVA=0x0B2C2DD8  token=0x60001E7  System.Void SetDepthBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle resource, UnityEngine.Experimental.Rendering.RenderGraphModule.DepthAccess flags)
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPass`1
TYPE:  sealed class
TOKEN: 0x2000057
EXTENDS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPass
FIELDS:
  private           PassData                        data  // 0x0
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.RenderFunc<PassData>renderFunc  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001E8  System.Void Execute(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext renderGraphContext)
  RVA=-1  // not resolved  token=0x60001E9  System.Void Initialize(System.Int32 passIndex, PassData passData, System.String passName, UnityEngine.Rendering.ProfilingSampler sampler)
  RVA=-1  // not resolved  token=0x60001EA  System.Void Release(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphObjectPool pool)
  RVA=-1  // not resolved  token=0x60001EB  System.Boolean HasRenderFunc()
  RVA=-1  // not resolved  token=0x60001EC  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphProfileId
TYPE:  sealed struct
TOKEN: 0x2000058
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphProfileIdCompileRenderGraph  // const
  public    static  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphProfileIdExecuteRenderGraph  // const
  public    static  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphProfileIdRenderGraphClear  // const
  public    static  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphProfileIdRenderGraphClearDebug  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle
TYPE:  sealed struct
TOKEN: 0x2000059
SIZE:  0x18
FIELDS:
  private   static  UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandles_NullHandle  // static @ 0x0
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandlehandle  // 0x10
PROPERTIES:
  nullHandle  get=0x0B2B7D20
METHODS:
  RVA=0x0B2B7CE8  token=0x60001EE  System.Void .ctor(System.Int32 handle, System.Boolean shared)
  RVA=0x0B2B7D70  token=0x60001EF  UnityEngine.ComputeBuffer op_Implicit(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle buffer)
  RVA=0x0B2B7C9C  token=0x60001F0  System.Boolean IsValid()
  RVA=0x041E1670  token=0x60001F1  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferDesc
TYPE:  sealed struct
TOKEN: 0x200005A
SIZE:  0x28
FIELDS:
  public            System.Int32                    count  // 0x10
  public            System.Int32                    stride  // 0x14
  public            UnityEngine.ComputeBufferType   type  // 0x18
  public            System.String                   name  // 0x20
METHODS:
  RVA=0x04DA11E0  token=0x60001F2  System.Void .ctor(System.Int32 count, System.Int32 stride)
  RVA=0x04DA11C0  token=0x60001F3  System.Void .ctor(System.Int32 count, System.Int32 stride, UnityEngine.ComputeBufferType type)
  RVA=0x04DB8C70  token=0x60001F4  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferResource
TYPE:  class
TOKEN: 0x200005B
SIZE:  0x50
EXTENDS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResource`2
FIELDS:
METHODS:
  RVA=0x0B2B859C  token=0x60001F5  System.String GetName()
  RVA=0x0B2B8448  token=0x60001F6  System.Void CreatePooledGraphicsResource()
  RVA=0x0B2B8720  token=0x60001F7  System.Void ReleasePooledGraphicsResource(System.Int32 frameIndex)
  RVA=0x0B2B82E0  token=0x60001F8  System.Void CreateGraphicsResource(System.String name)
  RVA=0x0B2B86DC  token=0x60001F9  System.Void ReleaseGraphicsResource()
  RVA=0x0B2B85DC  token=0x60001FA  System.Void LogCreation(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger logger)
  RVA=0x0B2B865C  token=0x60001FB  System.Void LogRelease(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger logger)
  RVA=0x0B2B885C  token=0x60001FC  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferPool
TYPE:  class
TOKEN: 0x200005C
SIZE:  0x28
EXTENDS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourcePool`1
FIELDS:
METHODS:
  RVA=0x0B2B8284  token=0x60001FD  System.Void ReleaseInternalResource(UnityEngine.ComputeBuffer res)
  RVA=0x0B2B7DDC  token=0x60001FE  System.String GetResourceName(UnityEngine.ComputeBuffer res)
  RVA=0x0B2B7E08  token=0x60001FF  System.Int64 GetResourceSize(UnityEngine.ComputeBuffer res)
  RVA=0x0B2B7E48  token=0x6000200  System.String GetResourceTypeName()
  RVA=0x09878C7C  token=0x6000201  System.Int32 GetSortIndex(UnityEngine.ComputeBuffer res)
  RVA=0x0B2B7E74  token=0x6000202  System.Void PurgeUnusedResources(System.Int32 currentFrameIndex)
  RVA=0x0B2B82A4  token=0x6000203  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResourcePool
TYPE:  abstract class
TOKEN: 0x200005D
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000204  System.Void PurgeUnusedResources(System.Int32 currentFrameIndex)
  RVA=-1  // abstract  token=0x6000205  System.Void Cleanup()
  RVA=-1  // abstract  token=0x6000206  System.Void CheckFrameAllocation(System.Boolean onException, System.Int32 frameIndex)
  RVA=-1  // abstract  token=0x6000207  System.Void LogResources(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger logger)
  RVA=0x041E1670  token=0x6000208  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourcePool`1
TYPE:  abstract class
TOKEN: 0x200005E
EXTENDS: UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResourcePool
FIELDS:
  protected         System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.SortedList<System.Int32,System.ValueTuple<Type,System.Int32>>>m_ResourcePool  // 0x0
  protected         System.Collections.Generic.List<System.Int32>m_RemoveList  // 0x0
  private           System.Collections.Generic.List<System.ValueTuple<System.Int32,Type>>m_FrameAllocatedResources  // 0x0
  protected static  System.Int32                    s_CurrentFrameIndex  // static @ 0x0
  private   static  System.Int32                    kStaleResourceLifetime  // const
METHODS:
  RVA=-1  // abstract  token=0x6000209  System.Void ReleaseInternalResource(Type res)
  RVA=-1  // abstract  token=0x600020A  System.String GetResourceName(Type res)
  RVA=-1  // abstract  token=0x600020B  System.Int64 GetResourceSize(Type res)
  RVA=-1  // abstract  token=0x600020C  System.String GetResourceTypeName()
  RVA=-1  // abstract  token=0x600020D  System.Int32 GetSortIndex(Type res)
  RVA=-1  // not resolved  token=0x600020E  System.Void ReleaseResource(System.Int32 hash, Type resource, System.Int32 currentFrameIndex)
  RVA=-1  // not resolved  token=0x600020F  System.Boolean TryGetResource(System.Int32 hashCode, Type& resource)
  RVA=-1  // not resolved  token=0x6000210  System.Void Cleanup()
  RVA=-1  // not resolved  token=0x6000211  System.Void RegisterFrameAllocation(System.Int32 hash, Type value)
  RVA=-1  // not resolved  token=0x6000212  System.Void UnregisterFrameAllocation(System.Int32 hash, Type value)
  RVA=-1  // not resolved  token=0x6000213  System.Void CheckFrameAllocation(System.Boolean onException, System.Int32 frameIndex)
  RVA=-1  // not resolved  token=0x6000214  System.Void LogResources(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger logger)
  RVA=-1  // not resolved  token=0x6000215  System.Boolean ShouldReleaseResource(System.Int32 lastUsedFrameIndex, System.Int32 currentFrameIndex)
  RVA=-1  // not resolved  token=0x6000216  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceRegistry
TYPE:  class
TOKEN: 0x2000061
SIZE:  0x50
FIELDS:
  private   static  System.Int32                    kSharedResourceLifetime  // const
  private   static  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceRegistrym_CurrentRegistry  // static @ 0x0
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceRegistry.RenderGraphResourcesData[]m_RenderGraphResources  // 0x10
  private           UnityEngine.Rendering.DynamicArray<UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListResource>m_RendererListResources  // 0x18
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDebugParamsm_RenderGraphDebug  // 0x20
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLoggerm_ResourceLogger  // 0x28
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLoggerm_FrameInformationLogger  // 0x30
  private           System.Int32                    m_CurrentFrameIndex  // 0x38
  private           System.Int32                    m_ExecutionCount  // 0x3c
  private           UnityEngine.Rendering.RTHandle  m_CurrentBackbuffer  // 0x40
  private   static  System.Int32                    kInitialRendererListCount  // const
  private           System.Collections.Generic.List<UnityEngine.Rendering.RendererUtils.RendererList>m_ActiveRendererLists  // 0x48
PROPERTIES:
  current  get=0x0B2C5E60  set=0x0B2C5E98
METHODS:
  RVA=0x0B2C49D0  token=0x600021C  UnityEngine.Rendering.RTHandle GetTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle& handle)
  RVA=0x0B2C58C0  token=0x600021D  System.Boolean TextureNeedsFallback(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle& handle)
  RVA=0x0B2C46C0  token=0x600021E  UnityEngine.Rendering.RendererUtils.RendererList GetRendererList(UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle& handle)
  RVA=0x0B2C43D4  token=0x600021F  UnityEngine.ComputeBuffer GetComputeBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle& handle)
  RVA=0x0B2C5D38  token=0x6000220  System.Void .ctor()
  RVA=0x0B2C59D4  token=0x6000221  System.Void .ctor(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphDebugParams renderGraphDebug, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger frameInformationLogger)
  RVA=0x0B2C31B4  token=0x6000222  System.Void BeginRenderGraph(System.Int32 executionCount)
  RVA=0x0B2C3194  token=0x6000223  System.Void BeginExecute(System.Int32 currentFrameIndex)
  RVA=0x0B2C40F0  token=0x6000224  System.Void EndExecute()
  RVA=0x0B2C323C  token=0x6000225  System.Void CheckHandleValidity(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res)
  RVA=0x0B2C3290  token=0x6000226  System.Void CheckHandleValidity(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceType type, System.Int32 index)
  RVA=0x0B2C4E7C  token=0x6000227  System.Void IncrementWriteCount(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res)
  RVA=0x0B2C44A4  token=0x6000228  System.String GetRenderGraphResourceName(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res)
  RVA=0x0B2C4564  token=0x6000229  System.String GetRenderGraphResourceName(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceType type, System.Int32 index)
  RVA=0x0B2C5094  token=0x600022A  System.Boolean IsRenderGraphResourceImported(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res)
  RVA=0x0B2C5150  token=0x600022B  System.Boolean IsRenderGraphResourceShared(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceType type, System.Int32 index)
  RVA=0x0B2C4F38  token=0x600022C  System.Boolean IsGraphicsResourceCreated(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res)
  RVA=0x0B2C51AC  token=0x600022D  System.Boolean IsRendererListCreated(UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle& res)
  RVA=0x0B2C4FF8  token=0x600022E  System.Boolean IsRenderGraphResourceImported(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceType type, System.Int32 index)
  RVA=0x0B2C4604  token=0x600022F  System.Int32 GetRenderGraphResourceTransientIndex(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& res)
  RVA=0x0B2C4DAC  token=0x6000230  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle ImportTexture(UnityEngine.Rendering.RTHandle rt)
  RVA=0x0B2C3B2C  token=0x6000231  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle CreateSharedTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc& desc, System.Boolean explicitRelease)
  RVA=0x0B2C55F4  token=0x6000232  System.Void ReleaseSharedTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle texture)
  RVA=0x0B2C4B7C  token=0x6000233  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle ImportBackbuffer(UnityEngine.Rendering.RenderTargetIdentifier rt)
  RVA=0x0B2C3FD8  token=0x6000234  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle CreateTexture(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc& desc, System.Int32 transientPassIndex)
  RVA=0x0B2C478C  token=0x6000235  System.Int32 GetTextureResourceCount()
  RVA=0x0B2C4910  token=0x6000236  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureResource GetTextureResource(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& handle)
  RVA=0x0B2C47E8  token=0x6000237  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc GetTextureResourceDesc(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& handle)
  RVA=0x0B2C4154  token=0x6000238  System.Void ForceTextureClear(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& handle, UnityEngine.Color clearColor)
  RVA=0x0B2C36C0  token=0x6000239  UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle CreateRendererList(UnityEngine.Rendering.RendererUtils.RendererListDesc& desc)
  RVA=0x0B2C4CF0  token=0x600023A  UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle ImportComputeBuffer(UnityEngine.ComputeBuffer computeBuffer)
  RVA=0x0B2C34D4  token=0x600023B  UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferHandle CreateComputeBuffer(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferDesc& desc, System.Int32 transientPassIndex)
  RVA=0x0B2C4210  token=0x600023C  UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferDesc GetComputeBufferResourceDesc(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& handle)
  RVA=0x0B2C41B4  token=0x600023D  System.Int32 GetComputeBufferResourceCount()
  RVA=0x0B2C4314  token=0x600023E  UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferResource GetComputeBufferResource(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle& handle)
  RVA=0x0B2C5944  token=0x600023F  System.Void UpdateSharedResourceLastFrameIndex(System.Int32 type, System.Int32 index)
  RVA=0x0B2C5340  token=0x6000240  System.Void ManageSharedRenderGraphResources()
  RVA=0x0B2C35B0  token=0x6000241  System.Void CreatePooledResource(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext rgContext, System.Int32 type, System.Int32 index)
  RVA=0x0B2C3D34  token=0x6000242  System.Void CreateTextureCallback(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext rgContext, UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResource res)
  RVA=0x0B2C54E0  token=0x6000243  System.Void ReleasePooledResource(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext rgContext, System.Int32 type, System.Int32 index)
  RVA=0x0B2C571C  token=0x6000244  System.Void ReleaseTextureCallback(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext rgContext, UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResource res)
  RVA=0x041E1670  token=0x6000245  System.Void ValidateTextureDesc(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc& desc)
  RVA=0x041E1670  token=0x6000246  System.Void ValidateRendererListDesc(UnityEngine.Rendering.RendererUtils.RendererListDesc& desc)
  RVA=0x041E1670  token=0x6000247  System.Void ValidateComputeBufferDesc(UnityEngine.Experimental.Rendering.RenderGraphModule.ComputeBufferDesc& desc)
  RVA=0x0B2C37F4  token=0x6000248  System.Void CreateRendererLists(System.Collections.Generic.List<UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle> rendererLists, UnityEngine.Rendering.ScriptableRenderContext context, System.Boolean manualDispatch)
  RVA=0x0B2C3418  token=0x6000249  System.Void Clear(System.Boolean onException)
  RVA=0x0B2C548C  token=0x600024A  System.Void PurgeUnusedGraphicsResources()
  RVA=0x0B2C33B8  token=0x600024B  System.Void Cleanup()
  RVA=0x0B2C40F8  token=0x600024C  System.Void FlushLogs()
  RVA=0x0B2C5234  token=0x600024D  System.Void LogResources()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle
TYPE:  sealed struct
TOKEN: 0x2000064
SIZE:  0x18
FIELDS:
  private           System.Boolean                  m_IsValid  // 0x10
  private           System.Int32                    <handle>k__BackingField  // 0x14
PROPERTIES:
  handle  get=0x04D88B20  set=0x04D88B50
METHODS:
  RVA=0x04D9D420  token=0x6000259  System.Void .ctor(System.Int32 handle)
  RVA=0x04D9D8A0  token=0x600025A  System.Int32 op_Implicit(UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle handle)
  RVA=0x0B2C6088  token=0x600025B  UnityEngine.Rendering.RendererUtils.RendererList op_Implicit(UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListHandle rendererList)
  RVA=0x04D883E0  token=0x600025C  System.Boolean IsValid()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RendererListResource
TYPE:  sealed struct
TOKEN: 0x2000065
SIZE:  0x100
FIELDS:
  public            UnityEngine.Rendering.RendererUtils.RendererListDescdesc  // 0x10
  public            UnityEngine.Rendering.RendererUtils.RendererListrendererList  // 0xf0
METHODS:
  RVA=0x09B3F960  token=0x600025D  System.Void .ctor(UnityEngine.Rendering.RendererUtils.RendererListDesc& desc)
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceType
TYPE:  sealed struct
TOKEN: 0x2000066
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceTypeTexture  // const
  public    static  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceTypeComputeBuffer  // const
  public    static  UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceTypeCount  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle
TYPE:  sealed struct
TOKEN: 0x2000067
SIZE:  0x18
FIELDS:
  private   static  System.UInt32                   kValidityMask  // const
  private   static  System.UInt32                   kIndexMask  // const
  private           System.UInt32                   m_Value  // 0x10
  private   static  System.UInt32                   s_CurrentValidBit  // static @ 0x0
  private   static  System.UInt32                   s_SharedResourceValidBit  // static @ 0x4
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceType<type>k__BackingField  // 0x14
PROPERTIES:
  index  get=0x04D87110
  type  get=0x04D88B20  set=0x04D88B50
  iType  get=0x0B2C6330
METHODS:
  RVA=0x0B2C62A4  token=0x6000262  System.Void .ctor(System.Int32 value, UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourceType type, System.Boolean shared)
  RVA=0x0B2C6374  token=0x6000263  System.Int32 op_Implicit(UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandle handle)
  RVA=0x0B2C6118  token=0x6000264  System.Boolean IsValid()
  RVA=0x0B2C6190  token=0x6000265  System.Void NewFrame(System.Int32 executionIndex)
  RVA=0x0B2C6254  token=0x6000266  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResource
TYPE:  class
TOKEN: 0x2000068
SIZE:  0x30
FIELDS:
  public            System.Boolean                  imported  // 0x10
  public            System.Boolean                  shared  // 0x11
  public            System.Boolean                  sharedExplicitRelease  // 0x12
  public            System.Boolean                  requestFallBack  // 0x13
  public            System.UInt32                   writeCount  // 0x14
  public            System.Int32                    cachedHash  // 0x18
  public            System.Int32                    transientPassIndex  // 0x1c
  public            System.Int32                    sharedResourceLastFrameUsed  // 0x20
  protected         UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResourcePoolm_Pool  // 0x28
METHODS:
  RVA=0x0B2C12BC  token=0x6000267  System.Void Reset(UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResourcePool pool)
  RVA=0x0B2C127C  token=0x6000268  System.String GetName()
  RVA=0x012081B0  token=0x6000269  System.Boolean IsCreated()
  RVA=0x04DB8D00  token=0x600026A  System.Void IncrementWriteCount()
  RVA=0x0B2C12A8  token=0x600026B  System.Boolean NeedsFallBack()
  RVA=0x041E1670  token=0x600026C  System.Void CreatePooledGraphicsResource()
  RVA=0x041E1670  token=0x600026D  System.Void CreateGraphicsResource(System.String name)
  RVA=0x041E1670  token=0x600026E  System.Void ReleasePooledGraphicsResource(System.Int32 frameIndex)
  RVA=0x041E1670  token=0x600026F  System.Void ReleaseGraphicsResource()
  RVA=0x041E1670  token=0x6000270  System.Void LogCreation(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger logger)
  RVA=0x041E1670  token=0x6000271  System.Void LogRelease(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger logger)
  RVA=0x011EC580  token=0x6000272  System.Int32 GetSortIndex()
  RVA=0x041E1670  token=0x6000273  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResource`2
TYPE:  abstract class
TOKEN: 0x2000069
EXTENDS: UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResource
FIELDS:
  public            DescType                        desc  // 0x0
  public            ResType                         graphicsResource  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000274  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000275  System.Void Reset(UnityEngine.Experimental.Rendering.RenderGraphModule.IRenderGraphResourcePool pool)
  RVA=-1  // not resolved  token=0x6000276  System.Boolean IsCreated()
  RVA=-1  // not resolved  token=0x6000277  System.Void ReleaseGraphicsResource()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle
TYPE:  sealed struct
TOKEN: 0x200006A
SIZE:  0x20
FIELDS:
  private   static  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandles_NullHandle  // static @ 0x0
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandlehandle  // 0x10
  private           UnityEngine.Experimental.Rendering.RenderGraphModule.ResourceHandlefallBackResource  // 0x18
PROPERTIES:
  nullHandle  get=0x0B2C6CBC
METHODS:
  RVA=0x0B2C6C30  token=0x6000279  System.Void .ctor(System.Int32 handle, System.Boolean shared)
  RVA=0x0B2C6E78  token=0x600027A  UnityEngine.Rendering.RenderTargetIdentifier op_Implicit(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle texture)
  RVA=0x0B2C6D18  token=0x600027B  UnityEngine.Texture op_Implicit(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle texture)
  RVA=0x0B2C6DFC  token=0x600027C  UnityEngine.RenderTexture op_Implicit(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle texture)
  RVA=0x0B2C6D94  token=0x600027D  UnityEngine.Rendering.RTHandle op_Implicit(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle texture)
  RVA=0x0B2C6BE4  token=0x600027E  System.Boolean IsValid()
  RVA=0x04DB8D10  token=0x600027F  System.Void SetFallBackResource(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureHandle texture)
  RVA=0x041E1670  token=0x6000280  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.TextureSizeMode
TYPE:  sealed struct
TOKEN: 0x200006B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureSizeModeExplicit  // const
  public    static  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureSizeModeScale  // const
  public    static  UnityEngine.Experimental.Rendering.RenderGraphModule.TextureSizeModeFunctor  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.FastMemoryDesc
TYPE:  sealed struct
TOKEN: 0x200006C
SIZE:  0x1C
FIELDS:
  public            System.Boolean                  inFastMemory  // 0x10
  public            UnityEngine.Rendering.FastMemoryFlagsflags  // 0x14
  public            System.Single                   residencyFraction  // 0x18
METHODS:
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc
TYPE:  sealed struct
TOKEN: 0x200006D
SIZE:  0x70
FIELDS:
  public            System.Int32                    width  // 0x10
  public            System.Int32                    height  // 0x14
  public            System.Int32                    slices  // 0x18
  public            UnityEngine.Rendering.DepthBits depthBufferBits  // 0x1c
  public            UnityEngine.Experimental.Rendering.GraphicsFormatcolorFormat  // 0x20
  public            UnityEngine.FilterMode          filterMode  // 0x24
  public            UnityEngine.TextureWrapMode     wrapMode  // 0x28
  public            UnityEngine.Rendering.TextureDimensiondimension  // 0x2c
  public            System.Boolean                  enableRandomWrite  // 0x30
  public            System.Boolean                  useMipMap  // 0x31
  public            System.Boolean                  autoGenerateMips  // 0x32
  public            System.Boolean                  isShadowMap  // 0x33
  public            System.Int32                    anisoLevel  // 0x34
  public            System.Single                   mipMapBias  // 0x38
  public            UnityEngine.Rendering.MSAASamplesmsaaSamples  // 0x3c
  public            System.Boolean                  bindTextureMS  // 0x40
  public            UnityEngine.RenderTextureMemorylessmemoryless  // 0x44
  public            System.String                   name  // 0x48
  public            UnityEngine.Experimental.Rendering.RenderGraphModule.FastMemoryDescfastMemoryDesc  // 0x50
  public            System.Boolean                  fallBackToBlackTexture  // 0x5c
  public            System.Boolean                  clearBuffer  // 0x5d
  public            UnityEngine.Color               clearColor  // 0x60
METHODS:
  RVA=0x0B2C6B48  token=0x6000281  System.Void InitDefaultValues(System.Boolean xrReady)
  RVA=0x0B2C6BB0  token=0x6000282  System.Void .ctor(System.Int32 width, System.Int32 height, System.Boolean xrReady)
  RVA=0x09B419A0  token=0x6000283  System.Void .ctor(UnityEngine.Experimental.Rendering.RenderGraphModule.TextureDesc input)
  RVA=0x0B2C6A4C  token=0x6000284  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.TextureResource
TYPE:  class
TOKEN: 0x200006E
SIZE:  0x98
EXTENDS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResource`2
FIELDS:
  private   static  System.Int32                    m_TextureCreationIndex  // static @ 0x0
METHODS:
  RVA=0x0B2C77A8  token=0x6000285  System.String GetName()
  RVA=0x0B2C7654  token=0x6000286  System.Void CreatePooledGraphicsResource()
  RVA=0x0B2C7984  token=0x6000287  System.Void ReleasePooledGraphicsResource(System.Int32 frameIndex)
  RVA=0x0B2C742C  token=0x6000288  System.Void CreateGraphicsResource(System.String name)
  RVA=0x0B2C7938  token=0x6000289  System.Void ReleaseGraphicsResource()
  RVA=0x0B2C7804  token=0x600028A  System.Void LogCreation(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger logger)
  RVA=0x0B2C78B8  token=0x600028B  System.Void LogRelease(UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphLogger logger)
  RVA=0x0B2C7ACC  token=0x600028C  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Experimental.Rendering.RenderGraphModule.TexturePool
TYPE:  class
TOKEN: 0x200006F
SIZE:  0x28
EXTENDS: UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphResourcePool`1
FIELDS:
METHODS:
  RVA=0x0B2C73D0  token=0x600028D  System.Void ReleaseInternalResource(UnityEngine.Rendering.RTHandle res)
  RVA=0x0B2C6F2C  token=0x600028E  System.String GetResourceName(UnityEngine.Rendering.RTHandle res)
  RVA=0x0B2C6F50  token=0x600028F  System.Int64 GetResourceSize(UnityEngine.Rendering.RTHandle res)
  RVA=0x0B2C6F74  token=0x6000290  System.String GetResourceTypeName()
  RVA=0x0B2C6FA0  token=0x6000291  System.Int32 GetSortIndex(UnityEngine.Rendering.RTHandle res)
  RVA=0x0B2C6FC0  token=0x6000292  System.Void PurgeUnusedResources(System.Int32 currentFrameIndex)
  RVA=0x0B2C73F0  token=0x6000293  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.CameraSwitcher
TYPE:  class
TOKEN: 0x2000070
SIZE:  0x78
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.Camera[]            m_Cameras  // 0x18
  private           System.Int32                    m_CurrentCameraIndex  // 0x20
  private           UnityEngine.Camera              m_OriginalCamera  // 0x28
  private           UnityEngine.Vector3             m_OriginalCameraPosition  // 0x30
  private           UnityEngine.Quaternion          m_OriginalCameraRotation  // 0x3c
  private           UnityEngine.Camera              m_CurrentCamera  // 0x50
  private           UnityEngine.GUIContent[]        m_CameraNames  // 0x58
  private           System.Int32[]                  m_CameraIndices  // 0x60
  private           UnityEngine.Rendering.DebugUI.EnumFieldm_DebugEntry  // 0x68
  private           System.Int32                    m_DebugEntryEnumIndex  // 0x70
METHODS:
  RVA=0x0B2B7060  token=0x6000294  System.Void OnEnable()
  RVA=0x0B2B6FEC  token=0x6000295  System.Void OnDisable()
  RVA=0x0B2B6F8C  token=0x6000296  System.Int32 GetCameraCount()
  RVA=0x0B2B6FAC  token=0x6000297  UnityEngine.Camera GetNextCamera()
  RVA=0x0B2B75B4  token=0x6000298  System.Void SetCameraIndex(System.Int32 index)
  RVA=0x0B2B7854  token=0x6000299  System.Void .ctor()
  RVA=0x02B70F90  token=0x600029A  System.Int32 <OnEnable>b__10_0()
  RVA=0x0B2B784C  token=0x600029B  System.Void <OnEnable>b__10_1(System.Int32 value)
  RVA=0x04D868A0  token=0x600029C  System.Int32 <OnEnable>b__10_2()
  RVA=0x04D868D0  token=0x600029D  System.Void <OnEnable>b__10_3(System.Int32 value)
END_CLASS

CLASS: UnityEngine.Rendering.FreeCamera
TYPE:  class
TOKEN: 0x2000071
SIZE:  0x70
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  System.Single                   k_MouseSensitivityMultiplier  // const
  public            System.Single                   m_LookSpeedController  // 0x18
  public            System.Single                   m_LookSpeedMouse  // 0x1c
  public            System.Single                   m_MoveSpeed  // 0x20
  public            System.Single                   m_MoveSpeedIncrement  // 0x24
  public            System.Single                   m_Turbo  // 0x28
  private           UnityEngine.InputSystem.InputActionlookAction  // 0x30
  private           UnityEngine.InputSystem.InputActionmoveAction  // 0x38
  private           UnityEngine.InputSystem.InputActionspeedAction  // 0x40
  private           UnityEngine.InputSystem.InputActionyMoveAction  // 0x48
  private           System.Single                   inputRotateAxisX  // 0x50
  private           System.Single                   inputRotateAxisY  // 0x54
  private           System.Single                   inputChangeSpeed  // 0x58
  private           System.Single                   inputVertical  // 0x5c
  private           System.Single                   inputHorizontal  // 0x60
  private           System.Single                   inputYAxis  // 0x64
  private           System.Boolean                  leftShiftBoost  // 0x68
  private           System.Boolean                  leftShift  // 0x69
  private           System.Boolean                  fire1  // 0x6a
METHODS:
  RVA=0x0B2C02DC  token=0x600029E  System.Void OnEnable()
  RVA=0x0B2C02E4  token=0x600029F  System.Void RegisterInputs()
  RVA=0x0B2C0A78  token=0x60002A0  System.Void UpdateInputs()
  RVA=0x0B2C0C90  token=0x60002A1  System.Void Update()
  RVA=0x0B2C1154  token=0x60002A2  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.CommandBufferPool
TYPE:  static class
TOKEN: 0x2000072
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.Rendering.ObjectPool<UnityEngine.Rendering.CommandBuffer>s_BufferPool  // static @ 0x0
METHODS:
  RVA=0x0B2B7914  token=0x60002A3  UnityEngine.Rendering.CommandBuffer Get()
  RVA=0x03339460  token=0x60002A4  UnityEngine.Rendering.CommandBuffer Get(System.String name)
  RVA=0x03339790  token=0x60002A5  System.Void Release(UnityEngine.Rendering.CommandBuffer buffer)
  RVA=0x04B083D0  token=0x60002A6  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.ClearFlag
TYPE:  sealed struct
TOKEN: 0x2000074
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.ClearFlag None  // const
  public    static  UnityEngine.Rendering.ClearFlag Color  // const
  public    static  UnityEngine.Rendering.ClearFlag Depth  // const
  public    static  UnityEngine.Rendering.ClearFlag Stencil  // const
  public    static  UnityEngine.Rendering.ClearFlag DepthStencil  // const
  public    static  UnityEngine.Rendering.ClearFlag ColorStencil  // const
  public    static  UnityEngine.Rendering.ClearFlag All  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.ComponentSingleton`1
TYPE:  static class
TOKEN: 0x2000075
FIELDS:
  private   static  TType                           s_Instance  // static @ 0x0
PROPERTIES:
  instance  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60002AC  System.Void Release()
END_CLASS

CLASS: UnityEngine.Rendering.ConstantBuffer
TYPE:  class
TOKEN: 0x2000076
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.List<UnityEngine.Rendering.ConstantBufferBase>m_RegisteredConstantBuffers  // static @ 0x0
METHODS:
  RVA=-1  // generic def  token=0x60002AD  System.Void PushGlobal(UnityEngine.Rendering.CommandBuffer cmd, CBType& data, System.Int32 shaderId)
  RVA=-1  // generic def  token=0x60002AE  System.Void PushGlobal(CBType& data, System.Int32 shaderId)
  RVA=-1  // generic def  token=0x60002AF  System.Void Push(UnityEngine.Rendering.CommandBuffer cmd, CBType& data, UnityEngine.ComputeShader cs, System.Int32 shaderId)
  RVA=-1  // generic def  token=0x60002B0  System.Void Push(CBType& data, UnityEngine.ComputeShader cs, System.Int32 shaderId)
  RVA=-1  // generic def  token=0x60002B1  System.Void Push(UnityEngine.Rendering.CommandBuffer cmd, CBType& data, UnityEngine.Material mat, System.Int32 shaderId)
  RVA=-1  // generic def  token=0x60002B2  System.Void Push(CBType& data, UnityEngine.Material mat, System.Int32 shaderId)
  RVA=-1  // generic def  token=0x60002B3  System.Void UpdateData(UnityEngine.Rendering.CommandBuffer cmd, CBType& data)
  RVA=-1  // generic def  token=0x60002B4  System.Void UpdateData(CBType& data)
  RVA=-1  // generic def  token=0x60002B5  System.Void SetGlobal(UnityEngine.Rendering.CommandBuffer cmd, System.Int32 shaderId)
  RVA=-1  // generic def  token=0x60002B6  System.Void SetGlobal(System.Int32 shaderId)
  RVA=-1  // generic def  token=0x60002B7  System.Void Set(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.ComputeShader cs, System.Int32 shaderId)
  RVA=-1  // generic def  token=0x60002B8  System.Void Set(UnityEngine.ComputeShader cs, System.Int32 shaderId)
  RVA=-1  // generic def  token=0x60002B9  System.Void Set(UnityEngine.Material mat, System.Int32 shaderId)
  RVA=0x04A41F00  token=0x60002BA  System.Void ReleaseAll()
  RVA=0x0B2B8884  token=0x60002BB  System.Void Register(UnityEngine.Rendering.ConstantBufferBase cb)
  RVA=0x041E1670  token=0x60002BC  System.Void .ctor()
  RVA=0x04D187B0  token=0x60002BD  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.ConstantBufferBase
TYPE:  abstract class
TOKEN: 0x2000077
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60002BE  System.Void Release()
  RVA=0x041E1670  token=0x60002BF  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.ConstantBuffer`1
TYPE:  class
TOKEN: 0x2000078
EXTENDS: UnityEngine.Rendering.ConstantBufferBase
FIELDS:
  private           System.Collections.Generic.HashSet<System.Int32>m_GlobalBindings  // 0x0
  private           CBType[]                        m_Data  // 0x0
  private           UnityEngine.ComputeBuffer       m_GPUConstantBuffer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60002C0  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60002C1  System.Void UpdateData(UnityEngine.Rendering.CommandBuffer cmd, CBType& data)
  RVA=-1  // not resolved  token=0x60002C2  System.Void UpdateData(CBType& data)
  RVA=-1  // not resolved  token=0x60002C3  System.Void SetGlobal(UnityEngine.Rendering.CommandBuffer cmd, System.Int32 shaderId)
  RVA=-1  // not resolved  token=0x60002C4  System.Void SetGlobal(System.Int32 shaderId)
  RVA=-1  // not resolved  token=0x60002C5  System.Void Set(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.ComputeShader cs, System.Int32 shaderId)
  RVA=-1  // not resolved  token=0x60002C6  System.Void Set(UnityEngine.ComputeShader cs, System.Int32 shaderId)
  RVA=-1  // not resolved  token=0x60002C7  System.Void Set(UnityEngine.Material mat, System.Int32 shaderId)
  RVA=-1  // not resolved  token=0x60002C8  System.Void PushGlobal(UnityEngine.Rendering.CommandBuffer cmd, CBType& data, System.Int32 shaderId)
  RVA=-1  // not resolved  token=0x60002C9  System.Void PushGlobal(CBType& data, System.Int32 shaderId)
  RVA=-1  // not resolved  token=0x60002CA  System.Void Release()
END_CLASS

CLASS: UnityEngine.Rendering.ConstantBufferSingleton`1
TYPE:  class
TOKEN: 0x2000079
EXTENDS: UnityEngine.Rendering.ConstantBuffer`1
FIELDS:
  private   static  UnityEngine.Rendering.ConstantBufferSingleton<CBType>s_Instance  // static @ 0x0
PROPERTIES:
  instance  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60002CD  System.Void Release()
  RVA=-1  // not resolved  token=0x60002CE  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.DisplayInfoAttribute
TYPE:  class
TOKEN: 0x200007A
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public            System.String                   name  // 0x10
  public            System.Int32                    order  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60002CF  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.AdditionalPropertyAttribute
TYPE:  class
TOKEN: 0x200007B
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002D0  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.CoreProfileId
TYPE:  sealed struct
TOKEN: 0x200007C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.CoreProfileIdBlitTextureInPotAtlas  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.CoreUnsafeUtils
TYPE:  static class
TOKEN: 0x200007D
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60002D1  System.Void CopyTo(System.Collections.Generic.List<T> list, System.Void* dest, System.Int32 count)
  RVA=-1  // generic def  token=0x60002D2  System.Void CopyTo(T[] list, System.Void* dest, System.Int32 count)
  RVA=0x0B2B8FF8  token=0x60002D3  System.Void CalculateRadixParams(System.Int32 radixBits, System.Int32& bitStates)
  RVA=0x04DB8CD0  token=0x60002D4  System.Int32 CalculateRadixSupportSize(System.Int32 bitStates, System.Int32 arrayLength)
  RVA=0x04DB8C90  token=0x60002D5  System.Void CalculateRadixSortSupportArrays(System.Int32 bitStates, System.Int32 arrayLength, System.UInt32* supportArray, System.UInt32*& bucketIndices, System.UInt32*& bucketSizes, System.UInt32*& bucketPrefix, System.UInt32*& arrayOutput)
  RVA=0x0B2B9398  token=0x60002D6  System.Void MergeSort(System.UInt32* array, System.UInt32* support, System.Int32 length)
  RVA=0x0B2B9544  token=0x60002D7  System.Void MergeSort(System.UInt32[] arr, System.Int32 sortSize, System.UInt32[]& supportArray)
  RVA=0x0B2B9638  token=0x60002D8  System.Void MergeSort(Unity.Collections.NativeArray<System.UInt32> arr, Unity.Collections.Allocator arrAllocator, System.Int32 sortSize, Unity.Collections.NativeArray<System.UInt32>& supportArray)
  RVA=0x0B2B92D4  token=0x60002D9  System.Void InsertionSort(System.UInt32* arr, System.Int32 length)
  RVA=0x0B2B9314  token=0x60002DA  System.Void InsertionSort(System.UInt32[] arr, System.Int32 sortSize)
  RVA=0x0B2B925C  token=0x60002DB  System.Void InsertionSort(Unity.Collections.NativeArray<System.UInt32> arr, System.Int32 sortSize)
  RVA=0x0B2B9780  token=0x60002DC  System.Void RadixSort(System.UInt32* array, System.UInt32* support, System.Int32 radixBits, System.Int32 bitStates, System.Int32 length)
  RVA=0x0B2B9A30  token=0x60002DD  System.Void RadixSort(System.UInt32[] arr, System.Int32 sortSize, System.UInt32[]& supportArray, System.Int32 radixBits)
  RVA=0x0B2B9944  token=0x60002DE  System.Void RadixSort(Unity.Collections.NativeArray<System.UInt32> array, System.Int32 sortSize, Unity.Collections.NativeArray<System.UInt32>& supportArray, Unity.Collections.Allocator supportArrayAllocator, System.Int32 radixBits)
  RVA=0x0B2B9708  token=0x60002DF  System.Void QuickSort(System.UInt32[] arr, System.Int32 left, System.Int32 right)
  RVA=-1  // generic def  token=0x60002E0  System.Void QuickSort(System.Int32 count, System.Void* data)
  RVA=-1  // generic def  token=0x60002E1  System.Void QuickSort(System.Int32 count, System.Void* data)
  RVA=-1  // generic def  token=0x60002E2  System.Void QuickSort(System.Void* data, System.Int32 left, System.Int32 right)
  RVA=-1  // generic def  token=0x60002E3  System.Int32 IndexOf(System.Void* data, System.Int32 count, T v)
  RVA=-1  // generic def  token=0x60002E4  System.Int32 CompareHashes(System.Int32 oldHashCount, System.Void* oldHashes, System.Int32 newHashCount, System.Void* newHashes, System.Int32* addIndices, System.Int32* removeIndices, System.Int32& addCount, System.Int32& remCount)
  RVA=0x0B2B90CC  token=0x60002E5  System.Int32 CompareHashes(System.Int32 oldHashCount, UnityEngine.Hash128* oldHashes, System.Int32 newHashCount, UnityEngine.Hash128* newHashes, System.Int32* addIndices, System.Int32* removeIndices, System.Int32& addCount, System.Int32& remCount)
  RVA=-1  // generic def  token=0x60002E6  System.Void CombineHashes(System.Int32 count, System.Void* hashes, UnityEngine.Hash128* outHash)
  RVA=0x0B2B9074  token=0x60002E7  System.Void CombineHashes(System.Int32 count, UnityEngine.Hash128* hashes, UnityEngine.Hash128* outHash)
  RVA=-1  // generic def  token=0x60002E8  System.Int32 Partition(System.Void* data, System.Int32 left, System.Int32 right)
  RVA=0x0B2B913C  token=0x60002E9  System.Boolean HaveDuplicates(System.Int32[] arr)
END_CLASS

CLASS: UnityEngine.Rendering.DynamicArray`1
TYPE:  class
TOKEN: 0x2000082
FIELDS:
  private           T[]                             m_Array  // 0x0
  private           System.Int32                    <size>k__BackingField  // 0x0
PROPERTIES:
  size  get=-1  // not resolved  set=-1  // not resolved
  capacity  get=-1  // not resolved
  Item  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60002F6  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60002F7  System.Void .ctor(System.Int32 size)
  RVA=-1  // not resolved  token=0x60002F8  System.Void Clear()
  RVA=-1  // not resolved  token=0x60002F9  System.Boolean Contains(T item)
  RVA=-1  // not resolved  token=0x60002FA  System.Int32 Add(T& value)
  RVA=-1  // not resolved  token=0x60002FB  System.Void AddRange(UnityEngine.Rendering.DynamicArray<T> array)
  RVA=-1  // not resolved  token=0x60002FC  System.Boolean Remove(T item)
  RVA=-1  // not resolved  token=0x60002FD  System.Void RemoveAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x60002FE  System.Void RemoveRange(System.Int32 index, System.Int32 count)
  RVA=-1  // not resolved  token=0x60002FF  System.Int32 FindIndex(System.Int32 startIndex, System.Int32 count, System.Predicate<T> match)
  RVA=-1  // not resolved  token=0x6000300  System.Int32 IndexOf(T item, System.Int32 index, System.Int32 count)
  RVA=-1  // not resolved  token=0x6000301  System.Int32 IndexOf(T item, System.Int32 index)
  RVA=-1  // not resolved  token=0x6000302  System.Int32 IndexOf(T item)
  RVA=-1  // not resolved  token=0x6000303  System.Void Resize(System.Int32 newSize, System.Boolean keepContent)
  RVA=-1  // not resolved  token=0x6000304  System.Void Reserve(System.Int32 newCapacity, System.Boolean keepContent)
  RVA=-1  // not resolved  token=0x6000306  T[] op_Implicit(UnityEngine.Rendering.DynamicArray<T> array)
END_CLASS

CLASS: UnityEngine.Rendering.DynamicArrayExtensions
TYPE:  static class
TOKEN: 0x2000083
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000307  System.Int32 Partition(T[] data, System.Int32 left, System.Int32 right)
  RVA=-1  // generic def  token=0x6000308  System.Void QuickSort(T[] data, System.Int32 left, System.Int32 right)
  RVA=-1  // generic def  token=0x6000309  System.Void QuickSort(UnityEngine.Rendering.DynamicArray<T> array)
END_CLASS

CLASS: UnityEngine.Rendering.PerformDynamicRes
TYPE:  sealed class
TOKEN: 0x2000084
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x046716C0  token=0x600030A  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x07378308  token=0x600030B  System.Single Invoke()
  RVA=0x053DED94  token=0x600030C  System.IAsyncResult BeginInvoke(System.AsyncCallback callback, System.Object object)
  RVA=0x05FD0828  token=0x600030D  System.Single EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: UnityEngine.Rendering.DynamicResScalePolicyType
TYPE:  sealed struct
TOKEN: 0x2000085
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.DynamicResScalePolicyTypeReturnsPercentage  // const
  public    static  UnityEngine.Rendering.DynamicResScalePolicyTypeReturnsMinMaxLerpFactor  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.DynamicResScalerSlot
TYPE:  sealed struct
TOKEN: 0x2000086
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.DynamicResScalerSlotUser  // const
  public    static  UnityEngine.Rendering.DynamicResScalerSlotSystem  // const
  public    static  UnityEngine.Rendering.DynamicResScalerSlotCount  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.DynamicResolutionHandler
TYPE:  class
TOKEN: 0x2000087
SIZE:  0x90
FIELDS:
  private           System.Boolean                  m_Enabled  // 0x10
  private           System.Boolean                  m_UseMipBias  // 0x11
  private           System.Single                   m_MinScreenFraction  // 0x14
  private           System.Single                   m_MaxScreenFraction  // 0x18
  private           System.Single                   m_CurrentFraction  // 0x1c
  private           System.Boolean                  m_ForcingRes  // 0x20
  private           System.Boolean                  m_CurrentCameraRequest  // 0x21
  private           System.Single                   m_PrevFraction  // 0x24
  private           System.Boolean                  m_ForceSoftwareFallback  // 0x28
  private           System.Boolean                  m_RunUpscalerFilterOnFullResolution  // 0x29
  private           System.Single                   m_PrevHWScaleWidth  // 0x2c
  private           System.Single                   m_PrevHWScaleHeight  // 0x30
  private           UnityEngine.Vector2Int          m_LastScaledSize  // 0x34
  private   static  UnityEngine.Rendering.DynamicResScalerSlots_ActiveScalerSlot  // static @ 0x0
  private   static  UnityEngine.Rendering.DynamicResolutionHandler.ScalerContainer[]s_ScalerContainers  // static @ 0x8
  private           UnityEngine.Vector2Int          cachedOriginalSize  // 0x3c
  private           UnityEngine.Rendering.DynamicResUpscaleFilter<filter>k__BackingField  // 0x44
  private   static  System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Rendering.DynamicResUpscaleFilter>s_CameraUpscaleFilters  // static @ 0x10
  private           UnityEngine.Vector2Int          <finalViewport>k__BackingField  // 0x48
  private           UnityEngine.Rendering.DynamicResolutionTypetype  // 0x50
  private           UnityEngine.Rendering.GlobalDynamicResolutionSettingsm_CachedSettings  // 0x54
  private   static  System.Int32                    CameraDictionaryMaxcCapacity  // const
  private           System.WeakReference            m_OwnerCameraWeakRef  // 0x80
  private   static  System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Rendering.DynamicResolutionHandler>s_CameraInstances  // static @ 0x18
  private   static  UnityEngine.Rendering.DynamicResolutionHandlers_DefaultInstance  // static @ 0x20
  private   static  System.Int32                    s_ActiveCameraId  // static @ 0x28
  private   static  UnityEngine.Rendering.DynamicResolutionHandlers_ActiveInstance  // static @ 0x30
  private   static  System.Boolean                  s_ActiveInstanceDirty  // static @ 0x38
  private   static  System.Single                   s_GlobalHwFraction  // static @ 0x3c
  private   static  System.Boolean                  s_GlobalHwUpresActive  // static @ 0x40
  private           UnityEngine.Rendering.DynamicResolutionHandler.UpsamplerScheduleTypem_UpsamplerSchedule  // 0x88
PROPERTIES:
  filter  get=0x04D86A30  set=0x04D86A40
  finalViewport  get=0x04D86200  set=0x04D866F0
  runUpscalerFilterOnFullResolution  get=0x0B2BFA4C  set=0x04D867C0
  upsamplerSchedule  get=0x04D86890  set=0x04D868C0
  instance  get=0x0B2BFA18
METHODS:
  RVA=0x04671970  token=0x600030E  System.Void Reset()
  RVA=0x0B2BEAB0  token=0x6000315  System.Boolean FlushScalableBufferManagerState()
  RVA=0x0B2BEBD8  token=0x6000316  UnityEngine.Rendering.DynamicResolutionHandler GetOrCreateDrsInstanceHandler(UnityEngine.Camera camera)
  RVA=0x04671750  token=0x600031A  System.Void .ctor()
  RVA=0x04D88DD0  token=0x600031B  System.Single DefaultDynamicResMethod()
  RVA=0x0B2BF104  token=0x600031C  System.Void ProcessSettings(UnityEngine.Rendering.GlobalDynamicResolutionSettings settings)
  RVA=0x0B2BF0A4  token=0x600031D  UnityEngine.Vector2 GetResolvedScale()
  RVA=0x03EC96B0  token=0x600031E  System.Single CalculateMipBias(UnityEngine.Vector2Int inputResolution, UnityEngine.Vector2Int outputResolution, System.Boolean forceApply)
  RVA=0x0B2BF334  token=0x600031F  System.Void SetDynamicResScaler(UnityEngine.Rendering.PerformDynamicRes scaler, UnityEngine.Rendering.DynamicResScalePolicyType scalerType)
  RVA=0x0B2BF3C8  token=0x6000320  System.Void SetSystemDynamicResScaler(UnityEngine.Rendering.PerformDynamicRes scaler, UnityEngine.Rendering.DynamicResScalePolicyType scalerType)
  RVA=0x0B2BF2E0  token=0x6000321  System.Void SetActiveDynamicScalerSlot(UnityEngine.Rendering.DynamicResScalerSlot slot)
  RVA=0x03DF2EA0  token=0x6000322  System.Void ClearSelectedCamera()
  RVA=0x0B2BF45C  token=0x6000323  System.Void SetUpscaleFilter(UnityEngine.Camera camera, UnityEngine.Rendering.DynamicResUpscaleFilter filter)
  RVA=0x04D86860  token=0x6000324  System.Void SetCurrentCameraRequest(System.Boolean cameraRequest)
  RVA=0x0B2BF5BC  token=0x6000325  System.Void UpdateAndUseCamera(UnityEngine.Camera camera, System.Nullable<UnityEngine.Rendering.GlobalDynamicResolutionSettings> settings, System.Action OnResolutionChange)
  RVA=0x0B2BF794  token=0x6000326  System.Void Update(UnityEngine.Rendering.GlobalDynamicResolutionSettings settings, System.Action OnResolutionChange)
  RVA=0x0B2BF570  token=0x6000327  System.Boolean SoftwareDynamicResIsEnabled()
  RVA=0x04D30F30  token=0x6000328  System.Boolean HardwareDynamicResIsEnabled()
  RVA=0x0B2BF2CC  token=0x6000329  System.Boolean RequestsHardwareDynamicResolution()
  RVA=0x0B2BEA7C  token=0x600032A  System.Boolean DynamicResolutionEnabled()
  RVA=0x04D8E3B0  token=0x600032B  System.Void ForceSoftwareFallback()
  RVA=0x03F65000  token=0x600032C  UnityEngine.Vector2Int GetScaledSize(UnityEngine.Vector2Int size)
  RVA=0x0B2BE994  token=0x600032D  UnityEngine.Vector2Int ApplyScalesOnSize(UnityEngine.Vector2Int size)
  RVA=0x0B2BE9C8  token=0x600032E  UnityEngine.Vector2Int ApplyScalesOnSize(UnityEngine.Vector2Int size, UnityEngine.Vector2 scales)
  RVA=0x0B2BEBBC  token=0x600032F  System.Single GetCurrentScale()
  RVA=0x04DB8CF0  token=0x6000330  UnityEngine.Vector2Int GetLastScaledSize()
  RVA=0x03F64FD0  token=0x6000331  System.Single GetLowResMultiplier(System.Single targetLowRes)
  RVA=0x046713A0  token=0x6000332  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.DynamicResolutionType
TYPE:  sealed struct
TOKEN: 0x200008A
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  UnityEngine.Rendering.DynamicResolutionTypeSoftware  // const
  public    static  UnityEngine.Rendering.DynamicResolutionTypeHardware  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.DynamicResUpscaleFilter
TYPE:  sealed struct
TOKEN: 0x200008B
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  UnityEngine.Rendering.DynamicResUpscaleFilterBilinear  // const
  public    static  UnityEngine.Rendering.DynamicResUpscaleFilterCatmullRom  // const
  public    static  UnityEngine.Rendering.DynamicResUpscaleFilterLanczos  // const
  public    static  UnityEngine.Rendering.DynamicResUpscaleFilterContrastAdaptiveSharpen  // const
  public    static  UnityEngine.Rendering.DynamicResUpscaleFilterEdgeAdaptiveScalingUpres  // const
  public    static  UnityEngine.Rendering.DynamicResUpscaleFilterTAAU  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.GlobalDynamicResolutionSettings
TYPE:  sealed struct
TOKEN: 0x200008C
SIZE:  0x38
FIELDS:
  public            System.Boolean                  enabled  // 0x10
  public            System.Boolean                  useMipBias  // 0x11
  public            System.Boolean                  enableDLSS  // 0x12
  public            System.UInt32                   DLSSPerfQualitySetting  // 0x14
  public            System.Boolean                  DLSSUseOptimalSettings  // 0x18
  public            System.Single                   DLSSSharpness  // 0x1c
  public            System.Single                   maxPercentage  // 0x20
  public            System.Single                   minPercentage  // 0x24
  public            UnityEngine.Rendering.DynamicResolutionTypedynResType  // 0x28
  public            UnityEngine.Rendering.DynamicResUpscaleFilterupsampleFilter  // 0x29
  public            System.Boolean                  forceResolution  // 0x2a
  public            System.Single                   forcedPercentage  // 0x2c
  public            System.Single                   lowResTransparencyMinimumThreshold  // 0x30
  public            System.Single                   rayTracingHalfResThreshold  // 0x34
METHODS:
  RVA=0x04671920  token=0x6000333  UnityEngine.Rendering.GlobalDynamicResolutionSettings NewDefault()
END_CLASS

CLASS: UnityEngine.Rendering.IAdditionalData
TYPE:  interface
TOKEN: 0x200008D
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.IVirtualTexturingEnabledRenderPipeline
TYPE:  interface
TOKEN: 0x200008E
FIELDS:
PROPERTIES:
  virtualTexturingEnabled  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.ListBuffer`1
TYPE:  sealed struct
TOKEN: 0x200008F
FIELDS:
  private           T*                              m_BufferPtr  // 0x0
  private           System.Int32                    m_Capacity  // 0x0
  private           System.Int32*                   m_CountPtr  // 0x0
PROPERTIES:
  BufferPtr  get=-1  // not resolved
  Count  get=-1  // not resolved
  Capacity  get=-1  // not resolved
  Item  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000338  System.Void .ctor(T* bufferPtr, System.Int32* countPtr, System.Int32 capacity)
  RVA=-1  // not resolved  token=0x600033A  T& GetUnchecked(System.Int32& index)
  RVA=-1  // not resolved  token=0x600033B  System.Boolean TryAdd(T& value)
  RVA=-1  // not resolved  token=0x600033C  System.Void CopyTo(T* dstBuffer, System.Int32 startDstIndex, System.Int32 copyCount)
  RVA=-1  // not resolved  token=0x600033D  System.Boolean TryCopyTo(UnityEngine.Rendering.ListBuffer<T> other)
  RVA=-1  // not resolved  token=0x600033E  System.Boolean TryCopyFrom(T* srcPtr, System.Int32 count)
END_CLASS

CLASS: UnityEngine.Rendering.ListBufferExtensions
TYPE:  static class
TOKEN: 0x2000090
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x600033F  System.Void QuickSort(UnityEngine.Rendering.ListBuffer<T> self)
END_CLASS

CLASS: UnityEngine.Rendering.ObjectPool`1
TYPE:  class
TOKEN: 0x2000091
FIELDS:
  private   readonly System.Collections.Generic.Stack<T>m_Stack  // 0x0
  private   readonly UnityEngine.Events.UnityAction<T>m_ActionOnGet  // 0x0
  private   readonly UnityEngine.Events.UnityAction<T>m_ActionOnRelease  // 0x0
  private   readonly System.Boolean                  m_CollectionCheck  // 0x0
  private           System.Int32                    <countAll>k__BackingField  // 0x0
PROPERTIES:
  countAll  get=-1  // not resolved  set=-1  // not resolved
  countActive  get=-1  // not resolved
  countInactive  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000344  System.Void .ctor(UnityEngine.Events.UnityAction<T> actionOnGet, UnityEngine.Events.UnityAction<T> actionOnRelease, System.Boolean collectionCheck)
  RVA=-1  // not resolved  token=0x6000345  T Get()
  RVA=-1  // not resolved  token=0x6000346  UnityEngine.Rendering.ObjectPool.PooledObject<T> Get(T& v)
  RVA=-1  // not resolved  token=0x6000347  System.Void Release(T element)
END_CLASS

CLASS: UnityEngine.Rendering.GenericPool`1
TYPE:  static class
TOKEN: 0x2000093
FIELDS:
  private   static readonly UnityEngine.Rendering.ObjectPool<T>s_Pool  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600034A  T Get()
  RVA=-1  // not resolved  token=0x600034B  UnityEngine.Rendering.ObjectPool.PooledObject<T> Get(T& value)
  RVA=-1  // not resolved  token=0x600034C  System.Void Release(T toRelease)
  RVA=-1  // not resolved  token=0x600034D  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.UnsafeGenericPool`1
TYPE:  static class
TOKEN: 0x2000094
FIELDS:
  private   static readonly UnityEngine.Rendering.ObjectPool<T>s_Pool  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600034E  T Get()
  RVA=-1  // not resolved  token=0x600034F  UnityEngine.Rendering.ObjectPool.PooledObject<T> Get(T& value)
  RVA=-1  // not resolved  token=0x6000350  System.Void Release(T toRelease)
  RVA=-1  // not resolved  token=0x6000351  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.ListPool`1
TYPE:  static class
TOKEN: 0x2000095
FIELDS:
  private   static readonly UnityEngine.Rendering.ObjectPool<System.Collections.Generic.List<T>>s_Pool  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000352  System.Collections.Generic.List<T> Get()
  RVA=-1  // not resolved  token=0x6000353  UnityEngine.Rendering.ObjectPool.PooledObject<System.Collections.Generic.List<T>> Get(System.Collections.Generic.List<T>& value)
  RVA=-1  // not resolved  token=0x6000354  System.Void Release(System.Collections.Generic.List<T> toRelease)
  RVA=-1  // not resolved  token=0x6000355  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.HashSetPool`1
TYPE:  static class
TOKEN: 0x2000097
FIELDS:
  private   static readonly UnityEngine.Rendering.ObjectPool<System.Collections.Generic.HashSet<T>>s_Pool  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000359  System.Collections.Generic.HashSet<T> Get()
  RVA=-1  // not resolved  token=0x600035A  UnityEngine.Rendering.ObjectPool.PooledObject<System.Collections.Generic.HashSet<T>> Get(System.Collections.Generic.HashSet<T>& value)
  RVA=-1  // not resolved  token=0x600035B  System.Void Release(System.Collections.Generic.HashSet<T> toRelease)
  RVA=-1  // not resolved  token=0x600035C  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.DictionaryPool`2
TYPE:  static class
TOKEN: 0x2000099
FIELDS:
  private   static readonly UnityEngine.Rendering.ObjectPool<System.Collections.Generic.Dictionary<TKey,TValue>>s_Pool  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000360  System.Collections.Generic.Dictionary<TKey,TValue> Get()
  RVA=-1  // not resolved  token=0x6000361  UnityEngine.Rendering.ObjectPool.PooledObject<System.Collections.Generic.Dictionary<TKey,TValue>> Get(System.Collections.Generic.Dictionary<TKey,TValue>& value)
  RVA=-1  // not resolved  token=0x6000362  System.Void Release(System.Collections.Generic.Dictionary<TKey,TValue> toRelease)
  RVA=-1  // not resolved  token=0x6000363  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.ListChangedEventArgs`1
TYPE:  sealed class
TOKEN: 0x200009B
EXTENDS: System.EventArgs
FIELDS:
  public    readonly System.Int32                    index  // 0x0
  public    readonly T                               item  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000367  System.Void .ctor(System.Int32 index, T item)
END_CLASS

CLASS: UnityEngine.Rendering.ListChangedEventHandler`1
TYPE:  sealed class
TOKEN: 0x200009C
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000368  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000369  System.Void Invoke(UnityEngine.Rendering.ObservableList<T> sender, UnityEngine.Rendering.ListChangedEventArgs<T> e)
  RVA=-1  // runtime  token=0x600036A  System.IAsyncResult BeginInvoke(UnityEngine.Rendering.ObservableList<T> sender, UnityEngine.Rendering.ListChangedEventArgs<T> e, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x600036B  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: UnityEngine.Rendering.ObservableList`1
TYPE:  class
TOKEN: 0x200009D
IMPLEMENTS: System.Collections.Generic.IList`1 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           System.Collections.Generic.IList<T>m_List  // 0x0
  private           UnityEngine.Rendering.ListChangedEventHandler<T>ItemAdded  // 0x0
  private           UnityEngine.Rendering.ListChangedEventHandler<T>ItemRemoved  // 0x0
PROPERTIES:
  Item  get=-1  // not resolved  set=-1  // not resolved
  Count  get=-1  // not resolved
  IsReadOnly  get=-1  // not resolved
EVENTS:
  ItemAdded  add=add_ItemAdded  remove=remove_ItemAdded
  ItemRemoved  add=add_ItemRemoved  remove=remove_ItemRemoved
METHODS:
  RVA=-1  // not resolved  token=0x6000374  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000375  System.Void .ctor(System.Int32 capacity)
  RVA=-1  // not resolved  token=0x6000376  System.Void .ctor(System.Collections.Generic.IEnumerable<T> collection)
  RVA=-1  // not resolved  token=0x6000377  System.Void OnEvent(UnityEngine.Rendering.ListChangedEventHandler<T> e, System.Int32 index, T item)
  RVA=-1  // not resolved  token=0x6000378  System.Boolean Contains(T item)
  RVA=-1  // not resolved  token=0x6000379  System.Int32 IndexOf(T item)
  RVA=-1  // not resolved  token=0x600037A  System.Void Add(T item)
  RVA=-1  // not resolved  token=0x600037B  System.Void Add(T[] items)
  RVA=-1  // not resolved  token=0x600037C  System.Void Insert(System.Int32 index, T item)
  RVA=-1  // not resolved  token=0x600037D  System.Boolean Remove(T item)
  RVA=-1  // not resolved  token=0x600037E  System.Int32 Remove(T[] items)
  RVA=-1  // not resolved  token=0x600037F  System.Void RemoveAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000380  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000381  System.Void CopyTo(T[] array, System.Int32 arrayIndex)
  RVA=-1  // not resolved  token=0x6000382  System.Collections.Generic.IEnumerator<T> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000383  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: UnityEngine.Rendering.SerializableEnum
TYPE:  class
TOKEN: 0x200009E
SIZE:  0x20
FIELDS:
  private           System.String                   m_EnumValueAsString  // 0x10
  private           System.Type                     m_EnumType  // 0x18
PROPERTIES:
  value  get=0x0B2C6484  set=0x0B2C6530
METHODS:
  RVA=0x0B2C6400  token=0x6000386  System.Void .ctor(System.Type enumType)
END_CLASS

CLASS: UnityEngine.Rendering.SerializedDictionary`2
TYPE:  class
TOKEN: 0x200009F
EXTENDS: UnityEngine.Rendering.SerializedDictionary`4
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000387  K SerializeKey(K key)
  RVA=-1  // not resolved  token=0x6000388  V SerializeValue(V val)
  RVA=-1  // not resolved  token=0x6000389  K DeserializeKey(K key)
  RVA=-1  // not resolved  token=0x600038A  V DeserializeValue(V val)
  RVA=-1  // not resolved  token=0x600038B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.SerializedDictionary`4
TYPE:  abstract class
TOKEN: 0x20000A0
EXTENDS: System.Collections.Generic.Dictionary`2
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           System.Collections.Generic.List<SK>m_Keys  // 0x0
  private           System.Collections.Generic.List<SV>m_Values  // 0x0
METHODS:
  RVA=-1  // abstract  token=0x600038C  SK SerializeKey(K key)
  RVA=-1  // abstract  token=0x600038D  SV SerializeValue(V value)
  RVA=-1  // abstract  token=0x600038E  K DeserializeKey(SK serializedKey)
  RVA=-1  // abstract  token=0x600038F  V DeserializeValue(SV serializedValue)
  RVA=-1  // not resolved  token=0x6000390  System.Void OnBeforeSerialize()
  RVA=-1  // not resolved  token=0x6000391  System.Void OnAfterDeserialize()
  RVA=-1  // not resolved  token=0x6000392  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.XRGraphics
TYPE:  class
TOKEN: 0x20000A1
SIZE:  0x10
FIELDS:
PROPERTIES:
  eyeTextureResolutionScale  get=0x0B2C8140  set=0x0B2C8298
  renderViewportScale  get=0x0B2C81F8
  enabled  get=0x0B2C80A8
  isDeviceActive  get=0x0B2C8190
  loadedDeviceName  get=0x0B2C81B4
  supportedDevices  get=0x0B2C824C
  stereoRenderingMode  get=0x0B2C8224
  eyeTextureDesc  get=0x0B2C80B0
  eyeTextureWidth  get=0x0B2C816C
  eyeTextureHeight  get=0x0B2C811C
METHODS:
  RVA=0x041E1670  token=0x600039E  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.DebugAction
TYPE:  sealed struct
TOKEN: 0x20000A3
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.DebugActionEnableDebugMenu  // const
  public    static  UnityEngine.Rendering.DebugActionPreviousDebugPanel  // const
  public    static  UnityEngine.Rendering.DebugActionNextDebugPanel  // const
  public    static  UnityEngine.Rendering.DebugActionAction  // const
  public    static  UnityEngine.Rendering.DebugActionMakePersistent  // const
  public    static  UnityEngine.Rendering.DebugActionMoveVertical  // const
  public    static  UnityEngine.Rendering.DebugActionMoveHorizontal  // const
  public    static  UnityEngine.Rendering.DebugActionMultiplier  // const
  public    static  UnityEngine.Rendering.DebugActionResetAll  // const
  public    static  UnityEngine.Rendering.DebugActionDebugActionCount  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.DebugActionRepeatMode
TYPE:  sealed struct
TOKEN: 0x20000A4
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.DebugActionRepeatModeNever  // const
  public    static  UnityEngine.Rendering.DebugActionRepeatModeDelay  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.DebugManager
TYPE:  sealed class
TOKEN: 0x20000A5
SIZE:  0x88
FIELDS:
  private   static  System.String                   kEnableDebugBtn1  // const
  private   static  System.String                   kEnableDebugBtn2  // const
  private   static  System.String                   kDebugPreviousBtn  // const
  private   static  System.String                   kDebugNextBtn  // const
  private   static  System.String                   kValidateBtn  // const
  private   static  System.String                   kPersistentBtn  // const
  private   static  System.String                   kDPadVertical  // const
  private   static  System.String                   kDPadHorizontal  // const
  private   static  System.String                   kMultiplierBtn  // const
  private   static  System.String                   kResetBtn  // const
  private   static  System.String                   kEnableDebug  // const
  private           UnityEngine.Rendering.DebugActionDesc[]m_DebugActions  // 0x10
  private           UnityEngine.Rendering.DebugActionState[]m_DebugActionStates  // 0x18
  private           UnityEngine.InputSystem.InputActionMapdebugActionMap  // 0x20
  private   static readonly System.Lazy<UnityEngine.Rendering.DebugManager>s_Instance  // static @ 0x0
  private           System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.Rendering.DebugUI.Panel>m_ReadOnlyPanels  // 0x28
  private   readonly System.Collections.Generic.List<UnityEngine.Rendering.DebugUI.Panel>m_Panels  // 0x30
  private           System.Action<System.Boolean>   onDisplayRuntimeUIChanged  // 0x38
  private           System.Action                   onSetDirty  // 0x40
  private           System.Action                   resetData  // 0x48
  public            System.Boolean                  refreshEditorRequested  // 0x50
  private           System.Nullable<System.Int32>   m_RequestedPanelIndex  // 0x54
  private           UnityEngine.GameObject          m_Root  // 0x60
  private           UnityEngine.Rendering.UI.DebugUIHandlerCanvasm_RootUICanvas  // 0x68
  private           UnityEngine.GameObject          m_PersistentRoot  // 0x70
  private           UnityEngine.Rendering.UI.DebugUIHandlerPersistentCanvasm_RootUIPersistentCanvas  // 0x78
  private           System.Boolean                  m_EditorOpen  // 0x80
  private           System.Boolean                  m_EnableRuntimeUI  // 0x81
PROPERTIES:
  instance  get=0x03948C00
  panels  get=0x0B2BC27C
  displayEditorUI  get=0x04D8D1F0
  enableRuntimeUI  get=0x04D8FEE0  set=0x03948CD0
  displayRuntimeUI  get=0x03948C50  set=0x04A438B0
  displayPersistentRuntimeUI  get=0x0B2BC20C  set=0x04D230A0
EVENTS:
  onDisplayRuntimeUIChanged  add=add_onDisplayRuntimeUIChanged  remove=remove_onDisplayRuntimeUIChanged
  onSetDirty  add=add_onSetDirty  remove=remove_onSetDirty
  resetData  add=add_resetData  remove=remove_resetData
METHODS:
  RVA=0x0B2BABE0  token=0x600039F  System.Void RegisterActions()
  RVA=0x0B2B9EE8  token=0x60003A0  System.Void EnableInputActions()
  RVA=0x0B2B9DF8  token=0x60003A1  System.Void AddAction(UnityEngine.Rendering.DebugAction action, UnityEngine.Rendering.DebugActionDesc desc)
  RVA=0x0B2BBCD0  token=0x60003A2  System.Void SampleAction(System.Int32 actionIndex)
  RVA=0x0B2BBF40  token=0x60003A3  System.Void UpdateAction(System.Int32 actionIndex)
  RVA=0x0B2BBF98  token=0x60003A4  System.Void UpdateActions()
  RVA=0x0B2BA454  token=0x60003A5  System.Single GetAction(UnityEngine.Rendering.DebugAction action)
  RVA=0x0B2BA290  token=0x60003A6  System.Boolean GetActionToggleDebugMenuWithTouch()
  RVA=0x0B2BA1C4  token=0x60003A7  System.Boolean GetActionReleaseScrollTarget()
  RVA=0x0B2BB050  token=0x60003A8  System.Void RegisterInputs()
  RVA=0x0B2BBFE4  token=0x60003AA  System.Void UpdateReadOnlyCollection()
  RVA=0x04D8D200  token=0x60003B3  System.Void ToggleEditorUI(System.Boolean open)
  RVA=0x0484F2C0  token=0x60003BA  System.Void .ctor()
  RVA=0x04DA9DE0  token=0x60003BB  System.Void RefreshEditor()
  RVA=0x0B2BBCA8  token=0x60003BC  System.Void Reset()
  RVA=0x0B2BABB4  token=0x60003BD  System.Void ReDrawOnScreenDebug()
  RVA=0x0B2BB020  token=0x60003BE  System.Void RegisterData(UnityEngine.Rendering.IDebugData data)
  RVA=0x0B2BBF10  token=0x60003BF  System.Void UnregisterData(UnityEngine.Rendering.IDebugData data)
  RVA=0x0B2BAA60  token=0x60003C0  System.Int32 GetState()
  RVA=0x0B2BB9D4  token=0x60003C1  System.Void RegisterRootCanvas(UnityEngine.Rendering.UI.DebugUIHandlerCanvas root)
  RVA=0x0B2B9EC8  token=0x60003C2  System.Void ChangeSelection(UnityEngine.Rendering.UI.DebugUIHandlerWidget widget, System.Boolean fromNext)
  RVA=0x0B2BBDE8  token=0x60003C3  System.Void SetScrollTarget(UnityEngine.Rendering.UI.DebugUIHandlerWidget widget)
  RVA=0x0B2B9FF4  token=0x60003C4  System.Void EnsurePersistentCanvas()
  RVA=0x0B2BBE64  token=0x60003C5  System.Void TogglePersistent(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x0B2BAB98  token=0x60003C6  System.Void OnPanelDirty(UnityEngine.Rendering.DebugUI.Panel panel)
  RVA=0x0B2BBC54  token=0x60003C7  System.Void RequestEditorWindowPanelIndex(System.Int32 index)
  RVA=0x04DB8CE0  token=0x60003C8  System.Nullable<System.Int32> GetRequestedEditorWindowPanelIndex()
  RVA=0x0B2BA770  token=0x60003C9  UnityEngine.Rendering.DebugUI.Panel GetPanel(System.String displayName, System.Boolean createIfNull, System.Int32 groupIndex, System.Boolean overrideIfExist)
  RVA=0x0B2BBA20  token=0x60003CA  System.Void RemovePanel(System.String displayName)
  RVA=0x0B2BBBF0  token=0x60003CB  System.Void RemovePanel(UnityEngine.Rendering.DebugUI.Panel panel)
  RVA=0x0B2BA644  token=0x60003CC  UnityEngine.Rendering.DebugUI.Widget GetItem(System.String queryPath)
  RVA=0x0B2BA48C  token=0x60003CD  UnityEngine.Rendering.DebugUI.Widget GetItem(System.String queryPath, UnityEngine.Rendering.DebugUI.IContainer container)
  RVA=0x04B7D6F0  token=0x60003CE  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.DebugActionDesc
TYPE:  class
TOKEN: 0x20000A7
SIZE:  0x20
FIELDS:
  public            UnityEngine.InputSystem.InputActionbuttonAction  // 0x10
  public            UnityEngine.Rendering.DebugActionRepeatModerepeatMode  // 0x18
  public            System.Single                   repeatDelay  // 0x1c
METHODS:
  RVA=0x041E1670  token=0x60003D4  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.DebugActionState
TYPE:  class
TOKEN: 0x20000A8
SIZE:  0x38
FIELDS:
  private           UnityEngine.Rendering.DebugActionState.DebugActionKeyTypem_Type  // 0x10
  private           UnityEngine.InputSystem.InputActioninputAction  // 0x18
  private           System.Boolean[]                m_TriggerPressedUp  // 0x20
  private           System.Single                   m_Timer  // 0x28
  private           System.Boolean                  <runningAction>k__BackingField  // 0x2c
  private           System.Single                   <actionState>k__BackingField  // 0x30
PROPERTIES:
  runningAction  get=0x04D869F0  set=0x04D86A00
  actionState  get=0x04D86E20  set=0x04D86E30
METHODS:
  RVA=0x0B2B9BDC  token=0x60003D9  System.Void Trigger(System.Int32 triggerCount, System.Single state)
  RVA=0x0B2B9B58  token=0x60003DA  System.Void TriggerWithButton(UnityEngine.InputSystem.InputAction action, System.Single state)
  RVA=0x0B2B9B40  token=0x60003DB  System.Void Reset()
  RVA=0x0B2B9C78  token=0x60003DC  System.Void Update(UnityEngine.Rendering.DebugActionDesc desc)
  RVA=0x041E1670  token=0x60003DD  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.IDebugData
TYPE:  interface
TOKEN: 0x20000AA
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60003DE  System.Action GetReset()
END_CLASS

CLASS: UnityEngine.Rendering.DebugShapes
TYPE:  class
TOKEN: 0x20000AB
SIZE:  0x30
FIELDS:
  private   static  UnityEngine.Rendering.DebugShapess_Instance  // static @ 0x0
  private           UnityEngine.Mesh                m_sphereMesh  // 0x10
  private           UnityEngine.Mesh                m_boxMesh  // 0x18
  private           UnityEngine.Mesh                m_coneMesh  // 0x20
  private           UnityEngine.Mesh                m_pyramidMesh  // 0x28
PROPERTIES:
  instance  get=0x0B2BE910
METHODS:
  RVA=0x0B2BE17C  token=0x60003E0  System.Void BuildSphere(UnityEngine.Mesh& outputMesh, System.Single radius, System.UInt32 longSubdiv, System.UInt32 latSubdiv)
  RVA=0x0B2BC458  token=0x60003E1  System.Void BuildBox(UnityEngine.Mesh& outputMesh, System.Single length, System.Single width, System.Single height)
  RVA=0x0B2BD098  token=0x60003E2  System.Void BuildCone(UnityEngine.Mesh& outputMesh, System.Single height, System.Single topRadius, System.Single bottomRadius, System.Int32 nbSides)
  RVA=0x0B2BDAE0  token=0x60003E3  System.Void BuildPyramid(UnityEngine.Mesh& outputMesh, System.Single width, System.Single height, System.Single depth)
  RVA=0x0B2BDFF0  token=0x60003E4  System.Void BuildShapes()
  RVA=0x0B2BE7DC  token=0x60003E5  System.Void RebuildResources()
  RVA=0x0B2BE8F4  token=0x60003E6  UnityEngine.Mesh RequestSphereMesh()
  RVA=0x0B2BE8A0  token=0x60003E7  UnityEngine.Mesh RequestBoxMesh()
  RVA=0x0B2BE8BC  token=0x60003E8  UnityEngine.Mesh RequestConeMesh()
  RVA=0x0B2BE8D8  token=0x60003E9  UnityEngine.Mesh RequestPyramidMesh()
  RVA=0x041E1670  token=0x60003EA  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.DebugUI
TYPE:  class
TOKEN: 0x20000AC
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60003EB  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.DebugUpdater
TYPE:  class
TOKEN: 0x20000CF
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  UnityEngine.Rendering.DebugUpdaters_Instance  // static @ 0x0
  private           UnityEngine.ScreenOrientation   m_Orientation  // 0x18
  private           System.Boolean                  m_RuntimeUiWasVisibleLastFrame  // 0x1c
METHODS:
  RVA=0x041E1670  token=0x6000480  System.Void RuntimeInit()
  RVA=0x03949B80  token=0x6000481  System.Void SetEnabled(System.Boolean enabled)
  RVA=0x0B2CAAB0  token=0x6000482  System.Void EnableRuntime()
  RVA=0x039499F0  token=0x6000483  System.Void DisableRuntime()
  RVA=0x04A439B0  token=0x6000484  System.Void HandleInternalEventSystemComponents(System.Boolean uiEnabled)
  RVA=0x0B2CAC1C  token=0x6000485  System.Void EnsureExactlyOneEventSystem()
  RVA=0x0B2CAA54  token=0x6000486  System.Collections.IEnumerator DoAfterInputModuleUpdated(System.Action action)
  RVA=0x0B2CA76C  token=0x6000487  System.Void CheckInputModuleExists()
  RVA=0x0B2CA64C  token=0x6000488  System.Void AssignDefaultActions()
  RVA=0x0B2CA844  token=0x6000489  System.Void CreateDebugEventSystem()
  RVA=0x0B2CA924  token=0x600048A  System.Void DestroyDebugEventSystem()
  RVA=0x0B2CADEC  token=0x600048B  System.Void Update()
  RVA=0x0B2CADA8  token=0x600048C  System.Collections.IEnumerator RefreshRuntimeUINextFrame()
  RVA=0x05393520  token=0x600048D  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.MousePositionDebug
TYPE:  class
TOKEN: 0x20000D2
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.Rendering.MousePositionDebugs_Instance  // static @ 0x0
PROPERTIES:
  instance  get=0x03945840
METHODS:
  RVA=0x041E1670  token=0x600049B  System.Void Build()
  RVA=0x041E1670  token=0x600049C  System.Void Cleanup()
  RVA=0x0B2D1430  token=0x600049D  UnityEngine.Vector2 GetMousePosition(System.Single ScreenHeight, System.Boolean sceneView)
  RVA=0x0B2D13C0  token=0x600049E  UnityEngine.Vector2 GetInputMousePosition()
  RVA=0x04DBB8D0  token=0x600049F  UnityEngine.Vector2 GetMouseClickPosition(System.Single ScreenHeight)
  RVA=0x041E1670  token=0x60004A0  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.TProfilingSampler`1
TYPE:  class
TOKEN: 0x20000D3
EXTENDS: UnityEngine.Rendering.ProfilingSampler
FIELDS:
  private   static  System.Collections.Generic.Dictionary<TEnum,UnityEngine.Rendering.TProfilingSampler<TEnum>>samples  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60004A1  System.Void .cctor()
  RVA=-1  // not resolved  token=0x60004A2  System.Void .ctor(System.String name)
END_CLASS

CLASS: UnityEngine.Rendering.ProfilingSampler
TYPE:  class
TOKEN: 0x20000D4
SIZE:  0x38
FIELDS:
  private           UnityEngine.Profiling.CustomSampler<sampler>k__BackingField  // 0x10
  private           UnityEngine.Profiling.CustomSampler<inlineSampler>k__BackingField  // 0x18
  private           System.String                   <name>k__BackingField  // 0x20
  private           UnityEngine.Profiling.Recorder  m_Recorder  // 0x28
  private           UnityEngine.Profiling.Recorder  m_InlineRecorder  // 0x30
PROPERTIES:
  sampler  get=0x02B2ECC0  set=0x053908C0
  inlineSampler  get=0x0385B100  set=0x05392C40
  name  get=0x04D862C0  set=0x05390F40
  enableRecording  set=0x0B2D355C
  gpuElapsedTime  get=0x0B2D343C
  gpuSampleCount  get=0x0B2D348C
  cpuElapsedTime  get=0x0B2D33AC
  cpuSampleCount  get=0x0B2D33FC
  inlineCpuElapsedTime  get=0x0B2D34CC
  inlineCpuSampleCount  get=0x0B2D351C
METHODS:
  RVA=-1  // generic def  token=0x60004A3  UnityEngine.Rendering.ProfilingSampler Get(TEnum marker)
  RVA=0x03B945E0  token=0x60004A4  System.Void .ctor(System.String name)
  RVA=0x0B2D3324  token=0x60004A5  System.Void Begin(UnityEngine.Rendering.CommandBuffer cmd)
  RVA=0x0B2D3368  token=0x60004A6  System.Void End(UnityEngine.Rendering.CommandBuffer cmd)
  RVA=0x0AB8BD08  token=0x60004A7  System.Boolean IsValid()
  RVA=0x041E1670  token=0x60004B5  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.ProfilingScope
TYPE:  sealed struct
TOKEN: 0x20000D5
SIZE:  0x11
IMPLEMENTS: System.IDisposable
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60004B6  System.Void .ctor(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.ProfilingSampler sampler)
  RVA=0x041E1670  token=0x60004B7  System.Void Dispose()
END_CLASS

CLASS: UnityEngine.Rendering.ProfilingSample
TYPE:  sealed struct
TOKEN: 0x20000D6
SIZE:  0x30
IMPLEMENTS: System.IDisposable
FIELDS:
  private   readonly UnityEngine.Rendering.CommandBufferm_Cmd  // 0x10
  private   readonly System.String                   m_Name  // 0x18
  private           System.Boolean                  m_Disposed  // 0x20
  private           UnityEngine.Profiling.CustomSamplerm_Sampler  // 0x28
METHODS:
  RVA=0x0B2D31F0  token=0x60004B8  System.Void .ctor(UnityEngine.Rendering.CommandBuffer cmd, System.String name, UnityEngine.Profiling.CustomSampler sampler)
  RVA=0x0B2D3294  token=0x60004B9  System.Void .ctor(UnityEngine.Rendering.CommandBuffer cmd, System.String format, System.Object arg)
  RVA=0x0B2D32DC  token=0x60004BA  System.Void .ctor(UnityEngine.Rendering.CommandBuffer cmd, System.String format, System.Object[] args)
  RVA=0x0B2D31E4  token=0x60004BB  System.Void Dispose()
  RVA=0x0B2D3164  token=0x60004BC  System.Void Dispose(System.Boolean disposing)
END_CLASS

CLASS: UnityEngine.Rendering.CoreRPHelpURLAttribute
TYPE:  class
TOKEN: 0x20000D7
SIZE:  0x28
EXTENDS: UnityEngine.HelpURLAttribute
FIELDS:
METHODS:
  RVA=0x0B2CA620  token=0x60004BD  System.Void .ctor(System.String pageName, System.String packageName)
END_CLASS

CLASS: UnityEngine.Rendering.DocumentationInfo
TYPE:  class
TOKEN: 0x20000D8
SIZE:  0x10
FIELDS:
  private   static  System.String                   fallbackVersion  // const
  private   static  System.String                   url  // const
PROPERTIES:
  version  get=0x0B2CAF9C
METHODS:
  RVA=0x0B2CAF40  token=0x60004BF  System.String GetPageLink(System.String packageName, System.String pageName)
  RVA=0x041E1670  token=0x60004C0  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.DocumentationUtils
TYPE:  static class
TOKEN: 0x20000D9
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60004C1  System.String GetHelpURL(TEnum mask)
END_CLASS

CLASS: UnityEngine.Rendering.APVConstantBufferRegister
TYPE:  sealed struct
TOKEN: 0x20000DA
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.APVConstantBufferRegisterGlobalRegister  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.ShaderVariablesProbeVolumes
TYPE:  sealed struct
TOKEN: 0x20000DB
SIZE:  0x50
FIELDS:
  public            UnityEngine.Vector3             _PoolDim  // 0x10
  public            System.Single                   _ViewBias  // 0x1c
  public            UnityEngine.Vector3             _MinCellPosition  // 0x20
  public            System.Single                   _PVSamplingNoise  // 0x2c
  public            UnityEngine.Vector3             _CellIndicesDim  // 0x30
  public            System.Single                   _CellInMeters  // 0x3c
  public            System.Single                   _CellInMinBricks  // 0x40
  public            System.Single                   _MinBrickSize  // 0x44
  public            System.Int32                    _IndexChunkSize  // 0x48
  public            System.Single                   _NormalBias  // 0x4c
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.SphericalHarmonicsL1
TYPE:  sealed struct
TOKEN: 0x20000DC
SIZE:  0x40
FIELDS:
  public            UnityEngine.Vector4             shAr  // 0x10
  public            UnityEngine.Vector4             shAg  // 0x20
  public            UnityEngine.Vector4             shAb  // 0x30
  public    static readonly UnityEngine.Rendering.SphericalHarmonicsL1zero  // static @ 0x0
METHODS:
  RVA=0x0B2D42A8  token=0x60004C2  UnityEngine.Rendering.SphericalHarmonicsL1 op_Addition(UnityEngine.Rendering.SphericalHarmonicsL1 lhs, UnityEngine.Rendering.SphericalHarmonicsL1 rhs)
  RVA=0x0B2D46AC  token=0x60004C3  UnityEngine.Rendering.SphericalHarmonicsL1 op_Subtraction(UnityEngine.Rendering.SphericalHarmonicsL1 lhs, UnityEngine.Rendering.SphericalHarmonicsL1 rhs)
  RVA=0x0B2D4620  token=0x60004C4  UnityEngine.Rendering.SphericalHarmonicsL1 op_Multiply(UnityEngine.Rendering.SphericalHarmonicsL1 lhs, System.Single rhs)
  RVA=0x0B2D4360  token=0x60004C5  UnityEngine.Rendering.SphericalHarmonicsL1 op_Division(UnityEngine.Rendering.SphericalHarmonicsL1 lhs, System.Single rhs)
  RVA=0x0B2D4448  token=0x60004C6  System.Boolean op_Equality(UnityEngine.Rendering.SphericalHarmonicsL1 lhs, UnityEngine.Rendering.SphericalHarmonicsL1 rhs)
  RVA=0x0B2D458C  token=0x60004C7  System.Boolean op_Inequality(UnityEngine.Rendering.SphericalHarmonicsL1 lhs, UnityEngine.Rendering.SphericalHarmonicsL1 rhs)
  RVA=0x0B2D410C  token=0x60004C8  System.Boolean Equals(System.Object other)
  RVA=0x0B2D41F0  token=0x60004C9  System.Int32 GetHashCode()
  RVA=0x0B2D4240  token=0x60004CA  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.SphericalHarmonicsL2Utils
TYPE:  class
TOKEN: 0x20000DD
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B2D47E8  token=0x60004CB  System.Void GetL1(UnityEngine.Rendering.SphericalHarmonicsL2 sh, UnityEngine.Vector3& L1_R, UnityEngine.Vector3& L1_G, UnityEngine.Vector3& L1_B)
  RVA=0x0B2D490C  token=0x60004CC  System.Void GetL2(UnityEngine.Rendering.SphericalHarmonicsL2 sh, UnityEngine.Vector3& L2_0, UnityEngine.Vector3& L2_1, UnityEngine.Vector3& L2_2, UnityEngine.Vector3& L2_3, UnityEngine.Vector3& L2_4)
  RVA=0x0B2D4B4C  token=0x60004CD  System.Void SetL0(UnityEngine.Rendering.SphericalHarmonicsL2& sh, UnityEngine.Vector3 L0)
  RVA=0x0B2D4C88  token=0x60004CE  System.Void SetL1R(UnityEngine.Rendering.SphericalHarmonicsL2& sh, UnityEngine.Vector3 L1_R)
  RVA=0x0B2D4C1C  token=0x60004CF  System.Void SetL1G(UnityEngine.Rendering.SphericalHarmonicsL2& sh, UnityEngine.Vector3 L1_G)
  RVA=0x0B2D4BB0  token=0x60004D0  System.Void SetL1B(UnityEngine.Rendering.SphericalHarmonicsL2& sh, UnityEngine.Vector3 L1_B)
  RVA=0x0B2D4CEC  token=0x60004D1  System.Void SetL1(UnityEngine.Rendering.SphericalHarmonicsL2& sh, UnityEngine.Vector3 L1_R, UnityEngine.Vector3 L1_G, UnityEngine.Vector3 L1_B)
  RVA=0x0B2D4AD8  token=0x60004D2  System.Void SetCoefficient(UnityEngine.Rendering.SphericalHarmonicsL2& sh, System.Int32 index, UnityEngine.Vector3 coefficient)
  RVA=0x0B2D4764  token=0x60004D3  UnityEngine.Vector3 GetCoefficient(UnityEngine.Rendering.SphericalHarmonicsL2 sh, System.Int32 index)
  RVA=0x041E1670  token=0x60004D4  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.LensFlareCommonSRP
TYPE:  sealed class
TOKEN: 0x20000DE
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.Rendering.LensFlareCommonSRPm_Instance  // static @ 0x0
  private   static readonly System.Object                   m_Padlock  // static @ 0x8
  private   static  System.Collections.Generic.List<UnityEngine.Rendering.LensFlareComponentSRP>m_Data  // static @ 0x10
  public    static  System.Int32                    maxLensFlareWithOcclusion  // static @ 0x18
  public    static  System.Int32                    maxLensFlareWithOcclusionTemporalSample  // static @ 0x1c
  public    static  System.Int32                    mergeNeeded  // static @ 0x20
  public    static  UnityEngine.Rendering.RTHandle  occlusionRT  // static @ 0x28
  private   static  System.Int32                    frameIdx  // static @ 0x30
PROPERTIES:
  Instance  get=0x03AAE370
  Data  get=0x0B2D1184
METHODS:
  RVA=0x041E1670  token=0x60004D5  System.Void .ctor()
  RVA=0x0B2CFDF8  token=0x60004D6  System.Void Initialize()
  RVA=0x0B2CC848  token=0x60004D7  System.Void Dispose()
  RVA=0x05394AF0  token=0x60004DA  System.Collections.Generic.List<UnityEngine.Rendering.LensFlareComponentSRP> GetData()
  RVA=0x03AAE0F0  token=0x60004DB  System.Boolean IsEmpty()
  RVA=0x0B2CB67C  token=0x60004DC  System.Void AddData(UnityEngine.Rendering.LensFlareComponentSRP newData)
  RVA=0x04D88DD0  token=0x60004DD  System.Single ShapeAttenuationPointLight()
  RVA=0x0B2D0518  token=0x60004DE  System.Single ShapeAttenuationDirLight(UnityEngine.Vector3 forward, UnityEngine.Vector3 wo)
  RVA=0x0B2D05A4  token=0x60004DF  System.Single ShapeAttenuationSpotConeLight(UnityEngine.Vector3 forward, UnityEngine.Vector3 wo, System.Single spotAngle, System.Single innerSpotPercent01)
  RVA=0x0B2D055C  token=0x60004E0  System.Single ShapeAttenuationSpotBoxLight(UnityEngine.Vector3 forward, UnityEngine.Vector3 wo)
  RVA=0x0B2D0698  token=0x60004E1  System.Single ShapeAttenuationSpotPyramidLight(UnityEngine.Vector3 forward, UnityEngine.Vector3 wo)
  RVA=0x0B2D0100  token=0x60004E2  System.Single ShapeAttenuationAreaTubeLight(UnityEngine.Vector3 lightPositionWS, UnityEngine.Vector3 lightSide, System.Single lightWidth, UnityEngine.Camera cam)
  RVA=0x0B2D0084  token=0x60004E3  System.Single ShapeAttenuationAreaRectangleLight(UnityEngine.Vector3 forward, UnityEngine.Vector3 wo)
  RVA=0x0B2D0008  token=0x60004E4  System.Single ShapeAttenuationAreaDiscLight(UnityEngine.Vector3 forward, UnityEngine.Vector3 wo)
  RVA=0x0B2CFB30  token=0x60004E5  UnityEngine.Vector4 GetFlareData0(UnityEngine.Vector2 screenPos, UnityEngine.Vector2 translationScale, UnityEngine.Vector2 rayOff0, UnityEngine.Vector2 vLocalScreenRatio, System.Single angleDeg, System.Single position, System.Single angularOffset, UnityEngine.Vector2 positionOffset, System.Boolean autoRotate)
  RVA=0x0B2CFD70  token=0x60004E6  UnityEngine.Vector2 GetLensFlareRayOffset(UnityEngine.Vector2 screenPos, System.Single position, System.Single globalCos0, System.Single globalSin0)
  RVA=0x0B2D1048  token=0x60004E7  UnityEngine.Vector3 WorldToViewport(UnityEngine.Camera camera, System.Boolean isLocalLight, System.Boolean isCameraRelative, UnityEngine.Matrix4x4 viewProjMatrix, UnityEngine.Vector3 positionWS)
  RVA=0x0B2D0EEC  token=0x60004E8  UnityEngine.Vector3 WorldToViewportLocal(System.Boolean isCameraRelative, UnityEngine.Matrix4x4 viewProjMatrix, UnityEngine.Vector3 cameraPosWS, UnityEngine.Vector3 positionWS)
  RVA=0x0B2D0D60  token=0x60004E9  UnityEngine.Vector3 WorldToViewportDistance(UnityEngine.Camera cam, UnityEngine.Vector3 positionWS)
  RVA=0x0B2CB830  token=0x60004EA  System.Void ComputeOcclusion(UnityEngine.Material lensFlareShader, UnityEngine.Rendering.LensFlareCommonSRP lensFlares, UnityEngine.Camera cam, System.Single actualWidth, System.Single actualHeight, System.Boolean usePanini, System.Single paniniDistance, System.Single paniniCropToFit, System.Boolean isCameraRelative, UnityEngine.Vector3 cameraPositionWS, UnityEngine.Matrix4x4 viewProjMatrix, UnityEngine.Rendering.CommandBuffer cmd, System.Boolean taaEnabled, System.Int32 _FlareOcclusionTex, System.Int32 _FlareOcclusionIndex, System.Int32 _FlareTex, System.Int32 _FlareColorValue, System.Int32 _FlareData0, System.Int32 _FlareData1, System.Int32 _FlareData2, System.Int32 _FlareData3, System.Int32 _FlareData4)
  RVA=0x0B2CC8E4  token=0x60004EB  System.Void DoLensFlareDataDrivenCommon(UnityEngine.Material lensFlareShader, UnityEngine.Rendering.LensFlareCommonSRP lensFlares, UnityEngine.Camera cam, System.Single actualWidth, System.Single actualHeight, System.Boolean usePanini, System.Single paniniDistance, System.Single paniniCropToFit, System.Boolean isCameraRelative, UnityEngine.Vector3 cameraPositionWS, UnityEngine.Matrix4x4 viewProjMatrix, UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, System.Func<UnityEngine.Light,UnityEngine.Camera,UnityEngine.Vector3,System.Single> GetLensFlareLightAttenuation, System.Int32 _FlareOcclusionTex, System.Int32 _FlareOcclusionIndex, System.Int32 _FlareTex, System.Int32 _FlareColorValue, System.Int32 _FlareData0, System.Int32 _FlareData1, System.Int32 _FlareData2, System.Int32 _FlareData3, System.Int32 _FlareData4, System.Boolean debugFullScreen, UnityEngine.ComputeBuffer debugFullscreenBuffer, UnityEngine.Rendering.LensFlareCommonSRP.SunSourceDirLightOverrideInfo dirLightOverrideInfo)
  RVA=0x03AADF50  token=0x60004EC  System.Void RemoveData(UnityEngine.Rendering.LensFlareComponentSRP data)
  RVA=0x0B2CF974  token=0x60004ED  UnityEngine.Vector2 DoPaniniProjection(UnityEngine.Vector2 screenPos, System.Single actualWidth, System.Single actualHeight, System.Single fieldOfView, System.Single paniniProjectionCropToFit, System.Single paniniProjectionDistance)
  RVA=0x0B2CB7E4  token=0x60004EE  UnityEngine.Vector2 CalcViewExtents(System.Single actualWidth, System.Single actualHeight, System.Single fieldOfView)
  RVA=0x0B2CB6FC  token=0x60004EF  UnityEngine.Vector2 CalcCropExtents(System.Single actualWidth, System.Single actualHeight, System.Single fieldOfView, System.Single d)
  RVA=0x0B2CFF8C  token=0x60004F0  UnityEngine.Vector2 Panini_Generic_Inv(UnityEngine.Vector2 projPos, System.Single d)
  RVA=0x04B1D060  token=0x60004F1  System.Void .cctor()
  RVA=0x0B2D0CEC  token=0x60004F2  System.Single <ShapeAttenuationAreaTubeLight>g__Fpo|26_0(System.Single d, System.Single l)
  RVA=0x0B2D0D44  token=0x60004F3  System.Single <ShapeAttenuationAreaTubeLight>g__Fwt|26_1(System.Single d, System.Single l)
  RVA=0x0B2D0910  token=0x60004F4  System.Single <ShapeAttenuationAreaTubeLight>g__DiffLineIntegral|26_2(UnityEngine.Vector3 p1, UnityEngine.Vector3 p2)
  RVA=0x0B2D071C  token=0x60004F5  UnityEngine.Vector2 <DoLensFlareDataDrivenCommon>g__ComputeLocalSize|35_0(UnityEngine.Vector2 rayOff, UnityEngine.Vector2 rayOff0, UnityEngine.Vector2 curSize, UnityEngine.AnimationCurve distortionCurve, UnityEngine.Rendering.LensFlareCommonSRP.<>c__DisplayClass35_0& , UnityEngine.Rendering.LensFlareCommonSRP.<>c__DisplayClass35_1& )
  RVA=0x09BBB1DC  token=0x60004F6  System.Single <DoLensFlareDataDrivenCommon>g__RandomRange|35_1(System.Single min, System.Single max)
END_CLASS

CLASS: UnityEngine.Rendering.LensFlareComponentSRP
TYPE:  sealed class
TOKEN: 0x20000E4
SIZE:  0x78
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.Rendering.LensFlareDataSRPm_LensFlareData  // 0x18
  public            System.Single                   intensity  // 0x20
  public            System.Single                   maxAttenuationDistance  // 0x24
  public            System.Single                   maxAttenuationScale  // 0x28
  public            UnityEngine.AnimationCurve      distanceAttenuationCurve  // 0x30
  public            UnityEngine.AnimationCurve      scaleByDistanceCurve  // 0x38
  public            System.Boolean                  attenuationByLightShape  // 0x40
  public            UnityEngine.AnimationCurve      radialScreenAttenuationCurve  // 0x48
  public            System.Boolean                  useOcclusion  // 0x50
  public            System.Single                   occlusionRadius  // 0x54
  public            System.UInt32                   sampleCount  // 0x58
  public            System.Single                   occlusionOffset  // 0x5c
  public            System.Single                   scale  // 0x60
  public            System.Boolean                  allowOffScreen  // 0x64
  public            System.Boolean                  useCustomForward  // 0x65
  public            UnityEngine.Vector3             customForward  // 0x68
  private   static  System.Single                   sCelestialAngularRadius  // static @ 0x0
PROPERTIES:
  lensFlareData  get=0x0385B100  set=0x0B2D1300
METHODS:
  RVA=0x0B2D1250  token=0x60004F9  System.Single celestialProjectedOcclusionRadius(UnityEngine.Camera mainCam)
  RVA=0x03AADFF0  token=0x60004FA  System.Void OnEnable()
  RVA=0x03AAE090  token=0x60004FB  System.Void OnDisable()
  RVA=0x0B2D11B8  token=0x60004FC  System.Void OnValidate()
  RVA=0x0402B7F0  token=0x60004FD  System.Void .ctor()
  RVA=0x04D7A250  token=0x60004FE  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.SRPLensFlareBlendMode
TYPE:  sealed struct
TOKEN: 0x20000E5
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.SRPLensFlareBlendModeAdditive  // const
  public    static  UnityEngine.Rendering.SRPLensFlareBlendModeScreen  // const
  public    static  UnityEngine.Rendering.SRPLensFlareBlendModePremultiply  // const
  public    static  UnityEngine.Rendering.SRPLensFlareBlendModeLerp  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.SRPLensFlareDistribution
TYPE:  sealed struct
TOKEN: 0x20000E6
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.SRPLensFlareDistributionUniform  // const
  public    static  UnityEngine.Rendering.SRPLensFlareDistributionCurve  // const
  public    static  UnityEngine.Rendering.SRPLensFlareDistributionRandom  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.SRPLensFlareType
TYPE:  sealed struct
TOKEN: 0x20000E7
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.SRPLensFlareTypeImage  // const
  public    static  UnityEngine.Rendering.SRPLensFlareTypeCircle  // const
  public    static  UnityEngine.Rendering.SRPLensFlareTypePolygon  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.LensFlareDataElementSRP
TYPE:  sealed class
TOKEN: 0x20000E8
SIZE:  0xF0
FIELDS:
  public            System.Boolean                  visible  // 0x10
  public            System.Single                   position  // 0x14
  public            UnityEngine.Vector2             positionOffset  // 0x18
  public            System.Single                   angularOffset  // 0x20
  public            UnityEngine.Vector2             translationScale  // 0x24
  private           System.Single                   m_LocalIntensity  // 0x2c
  public            UnityEngine.Texture             lensFlareTexture  // 0x30
  public            System.Single                   uniformScale  // 0x38
  public            UnityEngine.Vector2             sizeXY  // 0x3c
  public            System.Boolean                  allowMultipleElement  // 0x44
  private           System.Int32                    m_Count  // 0x48
  public            System.Boolean                  preserveAspectRatio  // 0x4c
  public            System.Single                   rotation  // 0x50
  public            UnityEngine.Color               tint  // 0x54
  public            UnityEngine.Rendering.SRPLensFlareBlendModeblendMode  // 0x64
  public            System.Boolean                  autoRotate  // 0x68
  public            UnityEngine.Rendering.SRPLensFlareTypeflareType  // 0x6c
  public            System.Boolean                  modulateByLightColor  // 0x70
  private           System.Boolean                  isFoldOpened  // 0x71
  public            UnityEngine.Rendering.SRPLensFlareDistributiondistribution  // 0x74
  public            System.Single                   lengthSpread  // 0x78
  public            UnityEngine.AnimationCurve      positionCurve  // 0x80
  public            UnityEngine.AnimationCurve      scaleCurve  // 0x88
  public            System.Int32                    seed  // 0x90
  public            UnityEngine.Gradient            colorGradient  // 0x98
  private           System.Single                   m_IntensityVariation  // 0xa0
  public            UnityEngine.Vector2             positionVariation  // 0xa4
  public            System.Single                   scaleVariation  // 0xac
  public            System.Single                   rotationVariation  // 0xb0
  public            System.Boolean                  enableRadialDistortion  // 0xb4
  public            UnityEngine.Vector2             targetSizeDistortion  // 0xb8
  public            UnityEngine.AnimationCurve      distortionCurve  // 0xc0
  public            System.Boolean                  distortionRelativeToCenter  // 0xc8
  private           System.Single                   m_FallOff  // 0xcc
  private           System.Single                   m_EdgeOffset  // 0xd0
  private           System.Int32                    m_SideCount  // 0xd4
  private           System.Single                   m_SdfRoundness  // 0xd8
  public            System.Boolean                  inverseSDF  // 0xdc
  public            System.Single                   uniformAngle  // 0xe0
  public            UnityEngine.AnimationCurve      uniformAngleCurve  // 0xe8
PROPERTIES:
  localIntensity  get=0x04D86AF0  set=0x0B2D1380
  count  get=0x04D863D0  set=0x0B2D1324
  intensityVariation  get=0x04D91E60  set=0x0B2D136C
  fallOff  get=0x04D9DFA0  set=0x0B2D1350
  edgeOffset  get=0x04D8E2E0  set=0x0B2D1334
  sideCount  get=0x04DB8DE0  set=0x0B2D13AC
  sdfRoundness  get=0x04D9DFB0  set=0x0B2D1390
METHODS:
  RVA=0x039C4B00  token=0x60004FF  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.LensFlareDataSRP
TYPE:  sealed class
TOKEN: 0x20000E9
SIZE:  0x20
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  public            UnityEngine.Rendering.LensFlareDataElementSRP[]elements  // 0x18
METHODS:
  RVA=0x04D5ECE0  token=0x600050E  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.RenderPipelineResources
TYPE:  abstract class
TOKEN: 0x20000EA
SIZE:  0x18
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
PROPERTIES:
  packagePath  get=0x011EC580
  packagePath_Internal  get=0x05395664
METHODS:
  RVA=0x03573370  token=0x6000511  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.PackingRules
TYPE:  sealed struct
TOKEN: 0x20000EB
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.PackingRulesExact  // const
  public    static  UnityEngine.Rendering.PackingRulesAggressive  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.FieldPacking
TYPE:  sealed struct
TOKEN: 0x20000EC
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.FieldPackingNoPacking  // const
  public    static  UnityEngine.Rendering.FieldPackingR11G11B10  // const
  public    static  UnityEngine.Rendering.FieldPackingPackedFloat  // const
  public    static  UnityEngine.Rendering.FieldPackingPackedUint  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.FieldPrecision
TYPE:  sealed struct
TOKEN: 0x20000ED
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.FieldPrecisionHalf  // const
  public    static  UnityEngine.Rendering.FieldPrecisionReal  // const
  public    static  UnityEngine.Rendering.FieldPrecisionDefault  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.GenerateHLSL
TYPE:  class
TOKEN: 0x20000EE
SIZE:  0x30
EXTENDS: System.Attribute
FIELDS:
  public            UnityEngine.Rendering.PackingRulespackingRules  // 0x10
  public            System.Boolean                  containsPackedFields  // 0x14
  public            System.Boolean                  needAccessors  // 0x15
  public            System.Boolean                  needSetters  // 0x16
  public            System.Boolean                  needParamDebug  // 0x17
  public            System.Int32                    paramDefinesStart  // 0x18
  public            System.Boolean                  omitStructDeclaration  // 0x1c
  public            System.Boolean                  generateCBuffer  // 0x1d
  public            System.Int32                    constantRegister  // 0x20
  public            System.String                   sourcePath  // 0x28
METHODS:
  RVA=0x0B2CAFC8  token=0x6000512  System.Void .ctor(UnityEngine.Rendering.PackingRules rules, System.Boolean needAccessors, System.Boolean needSetters, System.Boolean needParamDebug, System.Int32 paramDefinesStart, System.Boolean omitStructDeclaration, System.Boolean containsPackedFields, System.Boolean generateCBuffer, System.Int32 constantRegister, System.String sourcePath)
END_CLASS

CLASS: UnityEngine.Rendering.SurfaceDataAttributes
TYPE:  class
TOKEN: 0x20000EF
SIZE:  0x30
EXTENDS: System.Attribute
FIELDS:
  public            System.String[]                 displayNames  // 0x10
  public            System.Boolean                  isDirection  // 0x18
  public            System.Boolean                  sRGBDisplay  // 0x19
  public            UnityEngine.Rendering.FieldPrecisionprecision  // 0x1c
  public            System.Boolean                  checkIsNormalized  // 0x20
  public            System.String                   preprocessor  // 0x28
METHODS:
  RVA=0x0B2D4D74  token=0x6000513  System.Void .ctor(System.String displayName, System.Boolean isDirection, System.Boolean sRGBDisplay, UnityEngine.Rendering.FieldPrecision precision, System.Boolean checkIsNormalized, System.String preprocessor)
  RVA=0x0B2D4E24  token=0x6000514  System.Void .ctor(System.String[] displayNames, System.Boolean isDirection, System.Boolean sRGBDisplay, UnityEngine.Rendering.FieldPrecision precision, System.Boolean checkIsNormalized, System.String preprocessor)
END_CLASS

CLASS: UnityEngine.Rendering.HLSLArray
TYPE:  class
TOKEN: 0x20000F0
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public            System.Int32                    arraySize  // 0x10
  public            System.Type                     elementType  // 0x18
METHODS:
  RVA=0x05391D70  token=0x6000515  System.Void .ctor(System.Int32 arraySize, System.Type elementType)
END_CLASS

CLASS: UnityEngine.Rendering.PackingAttribute
TYPE:  class
TOKEN: 0x20000F1
SIZE:  0x38
EXTENDS: System.Attribute
FIELDS:
  public            System.String[]                 displayNames  // 0x10
  public            System.Single[]                 range  // 0x18
  public            UnityEngine.Rendering.FieldPackingpackingScheme  // 0x20
  public            System.Int32                    offsetInSource  // 0x24
  public            System.Int32                    sizeInBits  // 0x28
  public            System.Boolean                  isDirection  // 0x2c
  public            System.Boolean                  sRGBDisplay  // 0x2d
  public            System.Boolean                  checkIsNormalized  // 0x2e
  public            System.String                   preprocessor  // 0x30
METHODS:
  RVA=0x0B2D1438  token=0x6000516  System.Void .ctor(System.String[] displayNames, UnityEngine.Rendering.FieldPacking packingScheme, System.Int32 bitSize, System.Int32 offsetInSource, System.Single minValue, System.Single maxValue, System.Boolean isDirection, System.Boolean sRGBDisplay, System.Boolean checkIsNormalized, System.String preprocessor)
  RVA=0x0B2D1520  token=0x6000517  System.Void .ctor(System.String displayName, UnityEngine.Rendering.FieldPacking packingScheme, System.Int32 bitSize, System.Int32 offsetInSource, System.Single minValue, System.Single maxValue, System.Boolean isDirection, System.Boolean sRGBDisplay, System.Boolean checkIsNormalized, System.String preprocessor)
END_CLASS

CLASS: UnityEngine.Rendering.ShaderGenUInt4
TYPE:  sealed struct
TOKEN: 0x20000F2
SIZE:  0x11
FIELDS:
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.Hammersley
TYPE:  static class
TOKEN: 0x20000F3
SIZE:  0x10
FIELDS:
  private   static  System.Single[]                 k_Hammersley2dSeq16  // static @ 0x0
  private   static  System.Single[]                 k_Hammersley2dSeq32  // static @ 0x8
  private   static  System.Single[]                 k_Hammersley2dSeq64  // static @ 0x10
  private   static  System.Single[]                 k_Hammersley2dSeq256  // static @ 0x18
  private   static readonly System.Int32                    s_hammersley2DSeq16Id  // static @ 0x20
  private   static readonly System.Int32                    s_hammersley2DSeq32Id  // static @ 0x24
  private   static readonly System.Int32                    s_hammersley2DSeq64Id  // static @ 0x28
  private   static readonly System.Int32                    s_hammersley2DSeq256Id  // static @ 0x2c
METHODS:
  RVA=0x0B2CB150  token=0x6000518  System.Void Initialize()
  RVA=0x0B2CB028  token=0x6000519  System.Void BindConstants(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.ComputeShader cs)
  RVA=0x0B2CB434  token=0x600051A  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.BufferedRTHandleSystem
TYPE:  class
TOKEN: 0x20000FC
SIZE:  0x20
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Rendering.RTHandle[]>m_RTHandles  // 0x10
  private           System.Boolean                  m_DisposedValue  // 0x18
METHODS:
  RVA=0x0B2CA220  token=0x600051B  UnityEngine.Rendering.RTHandle GetFrameRT(System.Int32 bufferId, System.Int32 frameIndex)
  RVA=0x0B2CA0F8  token=0x600051C  System.Void AllocBuffer(System.Int32 bufferId, System.Func<System.Int32,UnityEngine.Rendering.RTHandle> allocator, System.Int32 bufferCount)
  RVA=0x0B2CA348  token=0x600051D  System.Void ReleaseBuffer(System.Int32 bufferId)
  RVA=0x0B2CA2C0  token=0x600051E  System.Int32 GetNumFramesAllocated(System.Int32 bufferId)
  RVA=0x0B2CA41C  token=0x600051F  System.Void Swap()
  RVA=0x047006E0  token=0x6000520  System.Void Dispose(System.Boolean disposing)
  RVA=0x0B2CA214  token=0x6000521  System.Void Dispose()
  RVA=0x04700710  token=0x6000522  System.Void ReleaseAll()
  RVA=0x04A3B350  token=0x6000523  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.DepthBits
TYPE:  sealed struct
TOKEN: 0x20000FD
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.DepthBits None  // const
  public    static  UnityEngine.Rendering.DepthBits Depth8  // const
  public    static  UnityEngine.Rendering.DepthBits Depth16  // const
  public    static  UnityEngine.Rendering.DepthBits Depth24  // const
  public    static  UnityEngine.Rendering.DepthBits Depth32  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.MSAASamples
TYPE:  sealed struct
TOKEN: 0x20000FE
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.MSAASamplesNone  // const
  public    static  UnityEngine.Rendering.MSAASamplesMSAA2x  // const
  public    static  UnityEngine.Rendering.MSAASamplesMSAA4x  // const
  public    static  UnityEngine.Rendering.MSAASamplesMSAA8x  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.PowerOfTwoTextureAtlas
TYPE:  class
TOKEN: 0x20000FF
SIZE:  0x58
EXTENDS: UnityEngine.Rendering.Texture2DAtlas
FIELDS:
  private   readonly System.Int32                    m_MipPadding  // 0x48
  private   static  System.Single                   k_MipmapFactorApprox  // const
  private           System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Vector2Int>m_RequestedTextures  // 0x50
PROPERTIES:
  mipPadding  get=0x04D863D0
METHODS:
  RVA=0x0B2D306C  token=0x6000524  System.Void .ctor(System.Int32 size, System.Int32 mipPadding, UnityEngine.Experimental.Rendering.GraphicsFormat format, UnityEngine.FilterMode filterMode, System.String name, System.Boolean useMipMap)
  RVA=0x04DB8DF0  token=0x6000526  System.Int32 GetTexturePadding()
  RVA=0x0B2D2870  token=0x6000527  UnityEngine.Vector4 GetPayloadScaleOffset(UnityEngine.Texture texture, UnityEngine.Vector4& scaleOffset)
  RVA=0x0B2D2758  token=0x6000528  UnityEngine.Vector4 GetPayloadScaleOffset(UnityEngine.Vector2& textureSize, UnityEngine.Vector2& paddingSize, UnityEngine.Vector4& scaleOffset)
  RVA=0x0B2D1EC0  token=0x6000529  System.Void Blit2DTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4 scaleOffset, UnityEngine.Texture texture, UnityEngine.Vector4 sourceScaleOffset, System.Boolean blitMips, UnityEngine.Rendering.PowerOfTwoTextureAtlas.BlitType blitType)
  RVA=0x0B2D2514  token=0x600052A  System.Void BlitTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4 scaleOffset, UnityEngine.Texture texture, UnityEngine.Vector4 sourceScaleOffset, System.Boolean blitMips, System.Int32 overrideInstanceID)
  RVA=0x0B2D2440  token=0x600052B  System.Void BlitTextureMultiply(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4 scaleOffset, UnityEngine.Texture texture, UnityEngine.Vector4 sourceScaleOffset, System.Boolean blitMips, System.Int32 overrideInstanceID)
  RVA=0x0B2D236C  token=0x600052C  System.Void BlitOctahedralTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4 scaleOffset, UnityEngine.Texture texture, UnityEngine.Vector4 sourceScaleOffset, System.Boolean blitMips, System.Int32 overrideInstanceID)
  RVA=0x0B2D2298  token=0x600052D  System.Void BlitOctahedralTextureMultiply(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4 scaleOffset, UnityEngine.Texture texture, UnityEngine.Vector4 sourceScaleOffset, System.Boolean blitMips, System.Int32 overrideInstanceID)
  RVA=0x0B2D3038  token=0x600052E  System.Void TextureSizeToPowerOfTwo(UnityEngine.Texture texture, System.Int32& width, System.Int32& height)
  RVA=0x0B2D291C  token=0x600052F  UnityEngine.Vector2 GetPowerOfTwoTextureSize(UnityEngine.Texture texture)
  RVA=0x0B2D1D28  token=0x6000530  System.Boolean AllocateTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4& scaleOffset, UnityEngine.Texture texture, System.Int32 width, System.Int32 height, System.Int32 overrideInstanceID)
  RVA=0x0B2D2FF0  token=0x6000531  System.Void ResetRequestedTexture()
  RVA=0x0B2D2E14  token=0x6000532  System.Boolean ReserveSpace(UnityEngine.Texture texture)
  RVA=0x0B2D2FA8  token=0x6000533  System.Boolean ReserveSpace(UnityEngine.Texture texture, System.Int32 width, System.Int32 height)
  RVA=0x0B2D2F68  token=0x6000534  System.Boolean ReserveSpace(UnityEngine.Texture textureA, UnityEngine.Texture textureB, System.Int32 width, System.Int32 height)
  RVA=0x0B2D2E74  token=0x6000535  System.Boolean ReserveSpace(System.Int32 id, System.Int32 width, System.Int32 height)
  RVA=0x0B2D2998  token=0x6000536  System.Boolean RelayoutEntries()
  RVA=0x0B2D25E0  token=0x6000537  System.Int64 GetApproxCacheSizeInByte(System.Int32 nbElement, System.Int32 resolution, System.Boolean hasMipmap, UnityEngine.Experimental.Rendering.GraphicsFormat format)
  RVA=0x0B2D2690  token=0x6000538  System.Int32 GetMaxCacheSizeForWeightInByte(System.Int32 weight, System.Boolean hasMipmap, UnityEngine.Experimental.Rendering.GraphicsFormat format)
END_CLASS

CLASS: UnityEngine.Rendering.RTHandle
TYPE:  class
TOKEN: 0x2000102
SIZE:  0x58
FIELDS:
  private           UnityEngine.RenderTexture       m_RT  // 0x10
  private           UnityEngine.Texture             m_ExternalTexture  // 0x18
  private           UnityEngine.Rendering.RenderTargetIdentifierm_NameID  // 0x20
  private           System.Boolean                  m_EnableMSAA  // 0x48
  private           System.Boolean                  m_EnableRandomWrite  // 0x49
  private           System.String                   m_Name  // 0x50
PROPERTIES:
  rt  get=0x02B2ECC0
  nameID  get=0x04D8E8C0
  name  get=0x04D86270  set=0x0B2D3EC0
  isMSAAEnabled  get=0x04D867D0
METHODS:
  RVA=0x0B2D3E14  token=0x6000541  UnityEngine.Rendering.RenderTargetIdentifier op_Implicit(UnityEngine.Rendering.RTHandle handle)
  RVA=0x0B2D3E58  token=0x6000542  UnityEngine.Texture op_Implicit(UnityEngine.Rendering.RTHandle handle)
  RVA=0x033021A0  token=0x6000543  UnityEngine.RenderTexture op_Implicit(UnityEngine.Rendering.RTHandle handle)
  RVA=0x037DF870  token=0x6000544  System.Void SetRenderTexture(UnityEngine.RenderTexture rt)
  RVA=0x037DF980  token=0x6000545  System.Void SetTexture(UnityEngine.Texture tex)
  RVA=0x0B2D3CD8  token=0x6000546  System.Void SetTexture(UnityEngine.Rendering.RenderTargetIdentifier tex)
  RVA=0x0B2D3C34  token=0x6000547  System.Int32 GetInstanceID()
  RVA=0x037DC790  token=0x6000548  System.Void Release()
  RVA=0x0B2D3D88  token=0x6000549  System.Void SwitchToFastMemory(UnityEngine.Rendering.CommandBuffer cmd, System.Single residencyFraction, UnityEngine.Rendering.FastMemoryFlags flags, System.Boolean copyContents)
  RVA=0x0B2D3C18  token=0x600054A  System.Void CopyToFastMemory(UnityEngine.Rendering.CommandBuffer cmd, System.Single residencyFraction, UnityEngine.Rendering.FastMemoryFlags flags)
  RVA=0x0B2D3D24  token=0x600054B  System.Void SwitchOutFastMemory(UnityEngine.Rendering.CommandBuffer cmd, System.Boolean copyContents)
  RVA=0x041E1670  token=0x600054C  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.RTHandles
TYPE:  static class
TOKEN: 0x2000103
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.Rendering.RTHandleSystems_DefaultInstance  // static @ 0x0
METHODS:
  RVA=0x037DED20  token=0x600054D  UnityEngine.Rendering.RTHandle Alloc(System.Int32 width, System.Int32 height, System.Int32 slices, UnityEngine.Rendering.DepthBits depthBufferBits, UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat, UnityEngine.FilterMode filterMode, UnityEngine.TextureWrapMode wrapMode, UnityEngine.Rendering.TextureDimension dimension, System.Boolean enableRandomWrite, System.Boolean useMipMap, System.Boolean autoGenerateMips, System.Boolean isShadowMap, System.Int32 anisoLevel, System.Single mipMapBias, UnityEngine.Rendering.MSAASamples msaaSamples, System.Boolean bindTextureMS, UnityEngine.RenderTextureMemoryless memoryless, System.String name)
  RVA=0x0B2D3F78  token=0x600054E  UnityEngine.Rendering.RTHandle Alloc(System.Int32 width, System.Int32 height, UnityEngine.TextureWrapMode wrapModeU, UnityEngine.TextureWrapMode wrapModeV, UnityEngine.TextureWrapMode wrapModeW, System.Int32 slices, UnityEngine.Rendering.DepthBits depthBufferBits, UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat, UnityEngine.FilterMode filterMode, UnityEngine.Rendering.TextureDimension dimension, System.Boolean enableRandomWrite, System.Boolean useMipMap, System.Boolean autoGenerateMips, System.Boolean isShadowMap, System.Int32 anisoLevel, System.Single mipMapBias, UnityEngine.Rendering.MSAASamples msaaSamples, System.Boolean bindTextureMS, System.Boolean useDynamicScale, UnityEngine.RenderTextureMemoryless memoryless, System.String name)
  RVA=0x05390780  token=0x600054F  UnityEngine.Rendering.RTHandle Alloc(UnityEngine.Texture tex)
  RVA=0x0B2D4104  token=0x6000550  UnityEngine.Rendering.RTHandle Alloc(UnityEngine.RenderTexture tex)
  RVA=0x0B2D404C  token=0x6000551  UnityEngine.Rendering.RTHandle Alloc(UnityEngine.Rendering.RenderTargetIdentifier tex)
  RVA=0x0B2D40D0  token=0x6000552  UnityEngine.Rendering.RTHandle Alloc(UnityEngine.Rendering.RenderTargetIdentifier tex, System.String name)
  RVA=0x0B2D4080  token=0x6000553  UnityEngine.Rendering.RTHandle Alloc(UnityEngine.Rendering.RTHandle tex)
  RVA=0x04D31EF0  token=0x6000554  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.RTHandleSystem
TYPE:  class
TOKEN: 0x2000104
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000555  System.Void Initialize()
  RVA=0x037DEDF0  token=0x6000556  UnityEngine.Rendering.RTHandle Alloc(System.Int32 width, System.Int32 height, System.Int32 slices, UnityEngine.Rendering.DepthBits depthBufferBits, UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat, UnityEngine.FilterMode filterMode, UnityEngine.TextureWrapMode wrapMode, UnityEngine.Rendering.TextureDimension dimension, System.Boolean enableRandomWrite, System.Boolean useMipMap, System.Boolean autoGenerateMips, System.Boolean isShadowMap, System.Int32 anisoLevel, System.Single mipMapBias, UnityEngine.Rendering.MSAASamples msaaSamples, System.Boolean bindTextureMS, UnityEngine.RenderTextureMemoryless memoryless, System.String name)
  RVA=0x037DEED0  token=0x6000557  UnityEngine.Rendering.RTHandle Alloc(System.Int32 width, System.Int32 height, UnityEngine.TextureWrapMode wrapModeU, UnityEngine.TextureWrapMode wrapModeV, UnityEngine.TextureWrapMode wrapModeW, System.Int32 slices, UnityEngine.Rendering.DepthBits depthBufferBits, UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat, UnityEngine.FilterMode filterMode, UnityEngine.Rendering.TextureDimension dimension, System.Boolean enableRandomWrite, System.Boolean useMipMap, System.Boolean autoGenerateMips, System.Boolean isShadowMap, System.Int32 anisoLevel, System.Single mipMapBias, UnityEngine.Rendering.MSAASamples msaaSamples, System.Boolean bindTextureMS, UnityEngine.RenderTextureMemoryless memoryless, System.String name)
  RVA=0x0B2D35A0  token=0x6000558  UnityEngine.Rendering.RTHandle AllocAutoSizedRenderTexture(System.Int32 width, System.Int32 height, System.Int32 slices, UnityEngine.Rendering.DepthBits depthBufferBits, UnityEngine.Experimental.Rendering.GraphicsFormat colorFormat, UnityEngine.FilterMode filterMode, UnityEngine.TextureWrapMode wrapMode, UnityEngine.Rendering.TextureDimension dimension, System.Boolean enableRandomWrite, System.Boolean useMipMap, System.Boolean autoGenerateMips, System.Boolean isShadowMap, System.Int32 anisoLevel, System.Single mipMapBias, UnityEngine.Rendering.MSAASamples msaaSamples, System.Boolean bindTextureMS, UnityEngine.RenderTextureMemoryless memoryless, System.String name)
  RVA=0x0B2D3AAC  token=0x6000559  UnityEngine.Rendering.RTHandle Alloc(UnityEngine.RenderTexture texture)
  RVA=0x037DF8F0  token=0x600055A  UnityEngine.Rendering.RTHandle Alloc(UnityEngine.Texture texture)
  RVA=0x0B2D3A50  token=0x600055B  UnityEngine.Rendering.RTHandle Alloc(UnityEngine.Rendering.RenderTargetIdentifier texture)
  RVA=0x0B2D3B2C  token=0x600055C  UnityEngine.Rendering.RTHandle Alloc(UnityEngine.Rendering.RenderTargetIdentifier texture, System.String name)
  RVA=0x0B2D3BC8  token=0x600055D  UnityEngine.Rendering.RTHandle Alloc(UnityEngine.Rendering.RTHandle tex)
  RVA=0x041E1670  token=0x600055E  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.AtlasAllocator
TYPE:  class
TOKEN: 0x2000105
SIZE:  0x30
FIELDS:
  private           UnityEngine.Rendering.AtlasAllocator.AtlasNodem_Root  // 0x10
  private           System.Int32                    m_Width  // 0x18
  private           System.Int32                    m_Height  // 0x1c
  private           System.Boolean                  powerOfTwoPadding  // 0x20
  private           UnityEngine.Rendering.ObjectPool<UnityEngine.Rendering.AtlasAllocator.AtlasNode>m_NodePool  // 0x28
METHODS:
  RVA=0x0B2C8C34  token=0x600055F  System.Void .ctor(System.Int32 width, System.Int32 height, System.Boolean potPadding)
  RVA=0x0B2C8B80  token=0x6000560  System.Boolean Allocate(UnityEngine.Vector4& result, System.Int32 width, System.Int32 height)
  RVA=0x0B2C8BDC  token=0x6000561  System.Void Reset()
END_CLASS

CLASS: UnityEngine.Rendering.Texture2DAtlas
TYPE:  class
TOKEN: 0x2000108
SIZE:  0x48
FIELDS:
  private   static  System.Int32                    kGPUTexInvalid  // const
  private   static  System.Int32                    kGPUTexValidMip0  // const
  private   static  System.Int32                    kGPUTexValidMipAll  // const
  private           UnityEngine.Rendering.RTHandle  m_AtlasTexture  // 0x10
  private           System.Int32                    m_Width  // 0x18
  private           System.Int32                    m_Height  // 0x1c
  private           UnityEngine.Experimental.Rendering.GraphicsFormatm_Format  // 0x20
  private           System.Boolean                  m_UseMipMaps  // 0x24
  private           System.Boolean                  m_IsAtlasTextureOwner  // 0x25
  private           UnityEngine.Rendering.AtlasAllocatorm_AtlasAllocator  // 0x28
  private           System.Collections.Generic.Dictionary<System.Int32,System.ValueTuple<UnityEngine.Vector4,UnityEngine.Vector2Int>>m_AllocationCache  // 0x30
  private           System.Collections.Generic.Dictionary<System.Int32,System.Int32>m_IsGPUTextureUpToDate  // 0x38
  private           System.Collections.Generic.Dictionary<System.Int32,System.Int32>m_TextureHashes  // 0x40
  private   static readonly UnityEngine.Vector4             fullScaleOffset  // static @ 0x0
  private   static readonly System.Int32                    s_MaxMipLevelPadding  // static @ 0x10
PROPERTIES:
  maxMipLevelPadding  get=0x0B2D7398
  AtlasTexture  get=0x02B2ECC0
METHODS:
  RVA=0x0B2D70D0  token=0x600056B  System.Void .ctor(System.Int32 width, System.Int32 height, UnityEngine.Experimental.Rendering.GraphicsFormat format, UnityEngine.FilterMode filterMode, System.Boolean powerOfTwoPadding, System.String name, System.Boolean useMipMap)
  RVA=0x0B2D6DAC  token=0x600056C  System.Void Release()
  RVA=0x0B2D6DE4  token=0x600056D  System.Void ResetAllocator()
  RVA=0x0B2D5FD0  token=0x600056E  System.Void ClearTarget(UnityEngine.Rendering.CommandBuffer cmd)
  RVA=0x0B2D6428  token=0x600056F  System.Int32 GetTextureMipmapCount(System.Int32 width, System.Int32 height)
  RVA=0x0B2D6460  token=0x6000570  System.Boolean Is2D(UnityEngine.Texture texture)
  RVA=0x0B2D65D0  token=0x6000571  System.Boolean IsSingleChannelBlit(UnityEngine.Texture source, UnityEngine.Texture destination)
  RVA=0x0B2D5AAC  token=0x6000572  System.Void Blit2DTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4 scaleOffset, UnityEngine.Texture texture, UnityEngine.Vector4 sourceScaleOffset, System.Boolean blitMips, UnityEngine.Rendering.Texture2DAtlas.BlitType blitType)
  RVA=0x0B2D67FC  token=0x6000573  System.Void MarkGPUTextureValid(System.Int32 instanceId, System.Boolean mipAreValid)
  RVA=0x0B2D67A4  token=0x6000574  System.Void MarkGPUTextureInvalid(System.Int32 instanceId)
  RVA=0x0B2D5E78  token=0x6000575  System.Void BlitTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4 scaleOffset, UnityEngine.Texture texture, UnityEngine.Vector4 sourceScaleOffset, System.Boolean blitMips, System.Int32 overrideInstanceID)
  RVA=0x0B2D5E20  token=0x6000576  System.Void BlitOctahedralTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4 scaleOffset, UnityEngine.Texture texture, UnityEngine.Vector4 sourceScaleOffset, System.Boolean blitMips, System.Int32 overrideInstanceID)
  RVA=0x0B2D5CC0  token=0x6000577  System.Void BlitCubeTexture2D(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4 scaleOffset, UnityEngine.Texture texture, System.Boolean blitMips, System.Int32 overrideInstanceID)
  RVA=0x0B2D58F4  token=0x6000578  System.Boolean AllocateTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4& scaleOffset, UnityEngine.Texture texture, System.Int32 width, System.Int32 height, System.Int32 overrideInstanceID)
  RVA=0x0B2D5894  token=0x6000579  System.Boolean AllocateTextureWithoutBlit(UnityEngine.Texture texture, System.Int32 width, System.Int32 height, UnityEngine.Vector4& scaleOffset)
  RVA=0x0B2D5708  token=0x600057A  System.Boolean AllocateTextureWithoutBlit(System.Int32 instanceId, System.Int32 width, System.Int32 height, UnityEngine.Vector4& scaleOffset)
  RVA=0x0B2D6364  token=0x600057B  System.Int32 GetTextureHash(UnityEngine.Texture textureA, UnityEngine.Texture textureB)
  RVA=0x0B2D6408  token=0x600057C  System.Int32 GetTextureID(UnityEngine.Texture texture)
  RVA=0x0B2D63C4  token=0x600057D  System.Int32 GetTextureID(UnityEngine.Texture textureA, UnityEngine.Texture textureB)
  RVA=0x0B2D64D4  token=0x600057E  System.Boolean IsCached(UnityEngine.Vector4& scaleOffset, UnityEngine.Texture textureA, UnityEngine.Texture textureB)
  RVA=0x0B2D6594  token=0x600057F  System.Boolean IsCached(UnityEngine.Vector4& scaleOffset, UnityEngine.Texture texture)
  RVA=0x0B2D6514  token=0x6000580  System.Boolean IsCached(UnityEngine.Vector4& scaleOffset, System.Int32 id)
  RVA=0x0B2D62F8  token=0x6000581  UnityEngine.Vector2Int GetCachedTextureSize(System.Int32 id)
  RVA=0x0B2D6BB4  token=0x6000582  System.Boolean NeedsUpdate(UnityEngine.Texture texture, System.Boolean needMips)
  RVA=0x0B2D6860  token=0x6000583  System.Boolean NeedsUpdate(UnityEngine.Texture textureA, UnityEngine.Texture textureB, System.Boolean needMips)
  RVA=0x0B2D5670  token=0x6000584  System.Boolean AddTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4& scaleOffset, UnityEngine.Texture texture)
  RVA=0x0B2D6E5C  token=0x6000585  System.Boolean UpdateTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture oldTexture, UnityEngine.Texture newTexture, UnityEngine.Vector4& scaleOffset, UnityEngine.Vector4 sourceScaleOffset, System.Boolean updateIfNeeded, System.Boolean blitMips)
  RVA=0x0B2D6FC8  token=0x6000586  System.Boolean UpdateTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture texture, UnityEngine.Vector4& scaleOffset, System.Boolean updateIfNeeded, System.Boolean blitMips)
  RVA=0x0B2D615C  token=0x6000587  System.Boolean EnsureTextureSlot(System.Boolean& isUploadNeeded, UnityEngine.Vector4& scaleBias, System.Int32 key, System.Int32 width, System.Int32 height)
  RVA=0x0B2D707C  token=0x6000588  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.AtlasAllocatorDynamic
TYPE:  class
TOKEN: 0x200010A
SIZE:  0x30
FIELDS:
  private           System.Int32                    m_Width  // 0x10
  private           System.Int32                    m_Height  // 0x14
  private           UnityEngine.Rendering.AtlasAllocatorDynamic.AtlasNodePoolm_Pool  // 0x18
  private           System.Int16                    m_Root  // 0x20
  private           System.Collections.Generic.Dictionary<System.Int32,System.Int16>m_NodeFromID  // 0x28
METHODS:
  RVA=0x0B2C8A3C  token=0x6000589  System.Void .ctor(System.Int32 width, System.Int32 height, System.Int32 capacityAllocations)
  RVA=0x0B2C8354  token=0x600058A  System.Boolean Allocate(UnityEngine.Vector4& result, System.Int32 key, System.Int32 width, System.Int32 height)
  RVA=0x0B2C88C0  token=0x600058B  System.Void Release(System.Int32 key)
  RVA=0x0B2C8988  token=0x600058C  System.Void Release()
  RVA=0x0B2C8858  token=0x600058D  System.String DebugStringFromRoot(System.Int32 depthMax)
  RVA=0x0B2C8458  token=0x600058E  System.Void DebugStringFromNode(System.String& res, System.Int16 n, System.Int32 depthCurrent, System.Int32 depthMax)
END_CLASS

CLASS: UnityEngine.Rendering.Texture2DAtlasDynamic
TYPE:  class
TOKEN: 0x200010E
SIZE:  0x38
FIELDS:
  private           UnityEngine.Rendering.RTHandle  m_AtlasTexture  // 0x10
  private           System.Boolean                  isAtlasTextureOwner  // 0x18
  private           System.Int32                    m_Width  // 0x1c
  private           System.Int32                    m_Height  // 0x20
  private           UnityEngine.Experimental.Rendering.GraphicsFormatm_Format  // 0x24
  private           UnityEngine.Rendering.AtlasAllocatorDynamicm_AtlasAllocator  // 0x28
  private           System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Vector4>m_AllocationCache  // 0x30
PROPERTIES:
  AtlasTexture  get=0x02B2ECC0
METHODS:
  RVA=0x0B2D54C4  token=0x600059E  System.Void .ctor(System.Int32 width, System.Int32 height, System.Int32 capacity, UnityEngine.Experimental.Rendering.GraphicsFormat format)
  RVA=0x0B2D53A4  token=0x600059F  System.Void .ctor(System.Int32 width, System.Int32 height, System.Int32 capacity, UnityEngine.Rendering.RTHandle atlasTexture)
  RVA=0x0B2D5314  token=0x60005A0  System.Void Release()
  RVA=0x0B2D534C  token=0x60005A1  System.Void ResetAllocator()
  RVA=0x0B2D4E6C  token=0x60005A2  System.Boolean AddTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector4& scaleOffset, UnityEngine.Texture texture)
  RVA=0x0B2D5248  token=0x60005A3  System.Boolean IsCached(UnityEngine.Vector4& scaleOffset, System.Int32 key)
  RVA=0x0B2D50E4  token=0x60005A4  System.Boolean EnsureTextureSlot(System.Boolean& isUploadNeeded, UnityEngine.Vector4& scaleOffset, System.Int32 key, System.Int32 width, System.Int32 height)
  RVA=0x0B2D52AC  token=0x60005A5  System.Void ReleaseTextureSlot(System.Int32 key)
END_CLASS

CLASS: UnityEngine.Rendering.TextureXR
TYPE:  static class
TOKEN: 0x200010F
SIZE:  0x10
FIELDS:
  private   static  System.Int32                    m_MaxViews  // static @ 0x0
  private   static  UnityEngine.Texture             m_BlackUIntTexture2DArray  // static @ 0x8
  private   static  UnityEngine.Texture             m_BlackUIntTexture  // static @ 0x10
  private   static  UnityEngine.Rendering.RTHandle  m_BlackUIntTexture2DArrayRTH  // static @ 0x18
  private   static  UnityEngine.Rendering.RTHandle  m_BlackUIntTextureRTH  // static @ 0x20
  private   static  UnityEngine.Texture2DArray      m_ClearTexture2DArray  // static @ 0x28
  private   static  UnityEngine.Texture2D           m_ClearTexture  // static @ 0x30
  private   static  UnityEngine.Rendering.RTHandle  m_ClearTexture2DArrayRTH  // static @ 0x38
  private   static  UnityEngine.Rendering.RTHandle  m_ClearTextureRTH  // static @ 0x40
  private   static  UnityEngine.Texture2DArray      m_MagentaTexture2DArray  // static @ 0x48
  private   static  UnityEngine.Texture2D           m_MagentaTexture  // static @ 0x50
  private   static  UnityEngine.Rendering.RTHandle  m_MagentaTexture2DArrayRTH  // static @ 0x58
  private   static  UnityEngine.Rendering.RTHandle  m_MagentaTextureRTH  // static @ 0x60
  private   static  UnityEngine.Texture2D           m_BlackTexture  // static @ 0x68
  private   static  UnityEngine.Texture3D           m_BlackTexture3D  // static @ 0x70
  private   static  UnityEngine.Texture2DArray      m_BlackTexture2DArray  // static @ 0x78
  private   static  UnityEngine.Rendering.RTHandle  m_BlackTexture2DArrayRTH  // static @ 0x80
  private   static  UnityEngine.Rendering.RTHandle  m_BlackTextureRTH  // static @ 0x88
  private   static  UnityEngine.Rendering.RTHandle  m_BlackTexture3DRTH  // static @ 0x90
  private   static  UnityEngine.Texture2DArray      m_WhiteTexture2DArray  // static @ 0x98
  private   static  UnityEngine.Rendering.RTHandle  m_WhiteTexture2DArrayRTH  // static @ 0xa0
  private   static  UnityEngine.Rendering.RTHandle  m_WhiteTextureRTH  // static @ 0xa8
PROPERTIES:
  maxViews  set=0x0B2D89BC
  slices  get=0x0B2D896C
  useTexArray  get=0x04775CF0
  dimension  get=0x0B2D8918
METHODS:
  RVA=0x0B2D7AFC  token=0x60005AA  UnityEngine.Rendering.RTHandle GetBlackUIntTexture()
  RVA=0x0B2D7B70  token=0x60005AB  UnityEngine.Rendering.RTHandle GetClearTexture()
  RVA=0x0B2D7BE4  token=0x60005AC  UnityEngine.Rendering.RTHandle GetMagentaTexture()
  RVA=0x0B2D7A80  token=0x60005AD  UnityEngine.Rendering.RTHandle GetBlackTexture()
  RVA=0x0B2D7A2C  token=0x60005AE  UnityEngine.Rendering.RTHandle GetBlackTextureArray()
  RVA=0x0B2D79D8  token=0x60005AF  UnityEngine.Rendering.RTHandle GetBlackTexture3D()
  RVA=0x0B2D7C58  token=0x60005B0  UnityEngine.Rendering.RTHandle GetWhiteTexture()
  RVA=0x0B2D7CD4  token=0x60005B1  System.Void Initialize(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.ComputeShader clearR32_UIntShader)
  RVA=0x0B2D7870  token=0x60005B2  UnityEngine.Texture2DArray CreateTexture2DArrayFromTexture2D(UnityEngine.Texture2D source, System.String name)
  RVA=0x0B2D74B8  token=0x60005B3  UnityEngine.Texture CreateBlackUIntTextureArray(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.ComputeShader clearR32_UIntShader)
  RVA=0x0B2D7694  token=0x60005B4  UnityEngine.Texture CreateBlackUintTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.ComputeShader clearR32_UIntShader)
  RVA=0x0B2D73E8  token=0x60005B5  UnityEngine.Texture3D CreateBlackTexture3D(System.String name)
  RVA=0x04D79990  token=0x60005B6  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.ArrayExtensions
TYPE:  static class
TOKEN: 0x2000110
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60005B7  System.Void ResizeArray(Unity.Collections.NativeArray<T>& array, System.Int32 capacity, Unity.Collections.Allocator allocator)
  RVA=0x0B2C82A0  token=0x60005B8  System.Void ResizeArray(UnityEngine.Jobs.TransformAccessArray& array, System.Int32 capacity)
  RVA=-1  // generic def  token=0x60005B9  System.Void ResizeArray(T[]& array, System.Int32 capacity)
  RVA=-1  // generic def  token=0x60005BA  System.Void Grow(T[]& array, System.Int32 targetSize)
  RVA=-1  // generic def  token=0x60005BB  System.Void GrowToNearestPowerOf2(T[]& array, System.Int32 targetSize)
  RVA=0x0B2C832C  token=0x60005BC  System.UInt32 RoundUpToPowerOf2(System.UInt32 n)
END_CLASS

CLASS: UnityEngine.Rendering.IBitArray
TYPE:  interface
TOKEN: 0x2000111
FIELDS:
PROPERTIES:
  capacity  get=-1  // abstract
  allFalse  get=-1  // abstract
  allTrue  get=-1  // abstract
  Item  get=-1  // abstract  set=-1  // abstract
  humanizedData  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60005C3  UnityEngine.Rendering.IBitArray BitAnd(UnityEngine.Rendering.IBitArray other)
  RVA=-1  // abstract  token=0x60005C4  UnityEngine.Rendering.IBitArray BitOr(UnityEngine.Rendering.IBitArray other)
  RVA=-1  // abstract  token=0x60005C5  UnityEngine.Rendering.IBitArray BitNot()
END_CLASS

CLASS: UnityEngine.Rendering.BitArray8
TYPE:  sealed struct
TOKEN: 0x2000112
SIZE:  0x11
IMPLEMENTS: UnityEngine.Rendering.IBitArray
FIELDS:
  private           System.Byte                     data  // 0x10
PROPERTIES:
  capacity  get=0x04D8D060
  allFalse  get=0x0961F480
  allTrue  get=0x0B2CA01C
  humanizedData  get=0x0B2CA024
  Item  get=0x0B2CA000  set=0x0B2CA0E8
METHODS:
  RVA=0x04D87180  token=0x60005CC  System.Void .ctor(System.Byte initValue)
  RVA=0x0B2C9F70  token=0x60005CD  System.Void .ctor(System.Collections.Generic.IEnumerable<System.UInt32> bitIndexTrue)
  RVA=0x04DB8DD0  token=0x60005CE  UnityEngine.Rendering.BitArray8 op_OnesComplement(UnityEngine.Rendering.BitArray8 a)
  RVA=0x04DB8DC0  token=0x60005CF  UnityEngine.Rendering.BitArray8 op_BitwiseOr(UnityEngine.Rendering.BitArray8 a, UnityEngine.Rendering.BitArray8 b)
  RVA=0x04DB8DB0  token=0x60005D0  UnityEngine.Rendering.BitArray8 op_BitwiseAnd(UnityEngine.Rendering.BitArray8 a, UnityEngine.Rendering.BitArray8 b)
  RVA=0x0B2C9E08  token=0x60005D1  UnityEngine.Rendering.IBitArray BitAnd(UnityEngine.Rendering.IBitArray other)
  RVA=0x0B2C9EB4  token=0x60005D2  UnityEngine.Rendering.IBitArray BitOr(UnityEngine.Rendering.IBitArray other)
  RVA=0x0B2C9E6C  token=0x60005D3  UnityEngine.Rendering.IBitArray BitNot()
  RVA=0x066FF158  token=0x60005D4  System.Boolean op_Equality(UnityEngine.Rendering.BitArray8 a, UnityEngine.Rendering.BitArray8 b)
  RVA=0x066FF160  token=0x60005D5  System.Boolean op_Inequality(UnityEngine.Rendering.BitArray8 a, UnityEngine.Rendering.BitArray8 b)
  RVA=0x0B2C9F14  token=0x60005D6  System.Boolean Equals(System.Object obj)
  RVA=0x04DB8DA0  token=0x60005D7  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.Rendering.BitArray16
TYPE:  sealed struct
TOKEN: 0x2000113
SIZE:  0x12
IMPLEMENTS: UnityEngine.Rendering.IBitArray
FIELDS:
  private           System.UInt16                   data  // 0x10
PROPERTIES:
  capacity  get=0x04DA43A0
  allFalse  get=0x0B160F5C
  allTrue  get=0x0B2C9CCC
  humanizedData  get=0x0B2C9CD8
  Item  get=0x05396E28  set=0x0539559C
METHODS:
  RVA=0x04D87160  token=0x60005DE  System.Void .ctor(System.UInt16 initValue)
  RVA=0x0B2C9C38  token=0x60005DF  System.Void .ctor(System.Collections.Generic.IEnumerable<System.UInt32> bitIndexTrue)
  RVA=0x04DB8D90  token=0x60005E0  UnityEngine.Rendering.BitArray16 op_OnesComplement(UnityEngine.Rendering.BitArray16 a)
  RVA=0x04DB8D80  token=0x60005E1  UnityEngine.Rendering.BitArray16 op_BitwiseOr(UnityEngine.Rendering.BitArray16 a, UnityEngine.Rendering.BitArray16 b)
  RVA=0x04DB8D70  token=0x60005E2  UnityEngine.Rendering.BitArray16 op_BitwiseAnd(UnityEngine.Rendering.BitArray16 a, UnityEngine.Rendering.BitArray16 b)
  RVA=0x0B2C9AC4  token=0x60005E3  UnityEngine.Rendering.IBitArray BitAnd(UnityEngine.Rendering.IBitArray other)
  RVA=0x0B2C9B74  token=0x60005E4  UnityEngine.Rendering.IBitArray BitOr(UnityEngine.Rendering.IBitArray other)
  RVA=0x0B2C9B2C  token=0x60005E5  UnityEngine.Rendering.IBitArray BitNot()
  RVA=0x0B290008  token=0x60005E6  System.Boolean op_Equality(UnityEngine.Rendering.BitArray16 a, UnityEngine.Rendering.BitArray16 b)
  RVA=0x0B2C9E00  token=0x60005E7  System.Boolean op_Inequality(UnityEngine.Rendering.BitArray16 a, UnityEngine.Rendering.BitArray16 b)
  RVA=0x0B2C9BD8  token=0x60005E8  System.Boolean Equals(System.Object obj)
  RVA=0x04DB8D60  token=0x60005E9  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.Rendering.BitArray32
TYPE:  sealed struct
TOKEN: 0x2000114
SIZE:  0x14
IMPLEMENTS: UnityEngine.Rendering.IBitArray
FIELDS:
  private           System.UInt32                   data  // 0x10
PROPERTIES:
  capacity  get=0x04DA43E0
  allFalse  get=0x0426F6A0
  allTrue  get=0x0B2D9B6C
  humanizedVersion  get=0x0B2D9C9C
  humanizedData  get=0x0B2D9B74
  Item  get=0x0B2D9B54  set=0x0B2D9CF0
METHODS:
  RVA=0x04D86140  token=0x60005F1  System.Void .ctor(System.UInt32 initValue)
  RVA=0x0B2D9AC4  token=0x60005F2  System.Void .ctor(System.Collections.Generic.IEnumerable<System.UInt32> bitIndexTrue)
  RVA=0x0B2D9960  token=0x60005F3  UnityEngine.Rendering.IBitArray BitAnd(UnityEngine.Rendering.IBitArray other)
  RVA=0x0B2D9A08  token=0x60005F4  UnityEngine.Rendering.IBitArray BitOr(UnityEngine.Rendering.IBitArray other)
  RVA=0x0B2D99C4  token=0x60005F5  UnityEngine.Rendering.IBitArray BitNot()
  RVA=0x04DB8FF0  token=0x60005F6  UnityEngine.Rendering.BitArray32 op_OnesComplement(UnityEngine.Rendering.BitArray32 a)
  RVA=0x04DB8FE0  token=0x60005F7  UnityEngine.Rendering.BitArray32 op_BitwiseOr(UnityEngine.Rendering.BitArray32 a, UnityEngine.Rendering.BitArray32 b)
  RVA=0x04DA5940  token=0x60005F8  UnityEngine.Rendering.BitArray32 op_BitwiseAnd(UnityEngine.Rendering.BitArray32 a, UnityEngine.Rendering.BitArray32 b)
  RVA=0x07872540  token=0x60005F9  System.Boolean op_Equality(UnityEngine.Rendering.BitArray32 a, UnityEngine.Rendering.BitArray32 b)
  RVA=0x0AAED910  token=0x60005FA  System.Boolean op_Inequality(UnityEngine.Rendering.BitArray32 a, UnityEngine.Rendering.BitArray32 b)
  RVA=0x0B2D9A68  token=0x60005FB  System.Boolean Equals(System.Object obj)
  RVA=0x04DB8FD0  token=0x60005FC  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.Rendering.BitArray64
TYPE:  sealed struct
TOKEN: 0x2000115
SIZE:  0x18
IMPLEMENTS: UnityEngine.Rendering.IBitArray
FIELDS:
  private           System.UInt64                   data  // 0x10
PROPERTIES:
  capacity  get=0x04DB9010
  allFalse  get=0x0314CD60
  allTrue  get=0x0B2D9F20
  humanizedData  get=0x0B2D9F28
  Item  get=0x0B2D9F08  set=0x0B2DA050
METHODS:
  RVA=0x04D88B60  token=0x6000603  System.Void .ctor(System.UInt64 initValue)
  RVA=0x0B2D9E74  token=0x6000604  System.Void .ctor(System.Collections.Generic.IEnumerable<System.UInt32> bitIndexTrue)
  RVA=0x04DB9040  token=0x6000605  UnityEngine.Rendering.BitArray64 op_OnesComplement(UnityEngine.Rendering.BitArray64 a)
  RVA=0x04DB9030  token=0x6000606  UnityEngine.Rendering.BitArray64 op_BitwiseOr(UnityEngine.Rendering.BitArray64 a, UnityEngine.Rendering.BitArray64 b)
  RVA=0x04DB9020  token=0x6000607  UnityEngine.Rendering.BitArray64 op_BitwiseAnd(UnityEngine.Rendering.BitArray64 a, UnityEngine.Rendering.BitArray64 b)
  RVA=0x0B2D9D00  token=0x6000608  UnityEngine.Rendering.IBitArray BitAnd(UnityEngine.Rendering.IBitArray other)
  RVA=0x0B2D9DB0  token=0x6000609  UnityEngine.Rendering.IBitArray BitOr(UnityEngine.Rendering.IBitArray other)
  RVA=0x0B2D9D68  token=0x600060A  UnityEngine.Rendering.IBitArray BitNot()
  RVA=0x02FB1840  token=0x600060B  System.Boolean op_Equality(UnityEngine.Rendering.BitArray64 a, UnityEngine.Rendering.BitArray64 b)
  RVA=0x02F019D0  token=0x600060C  System.Boolean op_Inequality(UnityEngine.Rendering.BitArray64 a, UnityEngine.Rendering.BitArray64 b)
  RVA=0x0B2D9E14  token=0x600060D  System.Boolean Equals(System.Object obj)
  RVA=0x04DB9000  token=0x600060E  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.Rendering.BitArray128
TYPE:  sealed struct
TOKEN: 0x2000116
SIZE:  0x20
IMPLEMENTS: UnityEngine.Rendering.IBitArray
FIELDS:
  private           System.UInt64                   data1  // 0x10
  private           System.UInt64                   data2  // 0x18
PROPERTIES:
  Data1  get=0x02B76770
  Data2  get=0x04D88320
  capacity  get=0x04DB8E50
  allFalse  get=0x0B2D9000
  allTrue  get=0x0B2D9014
  humanizedData  get=0x0B2D9028
  Item  get=0x05392684  set=0x05392698
METHODS:
  RVA=0x04D861B0  token=0x6000617  System.Void .ctor(System.UInt64 initValue1, System.UInt64 initValue2)
  RVA=0x037DB200  token=0x6000618  System.Void .ctor(System.Collections.Generic.IEnumerable<System.UInt32> bitIndexTrue)
  RVA=0x04DB8EA0  token=0x6000619  UnityEngine.Rendering.BitArray128 op_OnesComplement(UnityEngine.Rendering.BitArray128 a)
  RVA=0x04DB8E80  token=0x600061A  UnityEngine.Rendering.BitArray128 op_BitwiseOr(UnityEngine.Rendering.BitArray128 a, UnityEngine.Rendering.BitArray128 b)
  RVA=0x04DB8E60  token=0x600061B  UnityEngine.Rendering.BitArray128 op_BitwiseAnd(UnityEngine.Rendering.BitArray128 a, UnityEngine.Rendering.BitArray128 b)
  RVA=0x0B2D8E00  token=0x600061C  UnityEngine.Rendering.IBitArray BitAnd(UnityEngine.Rendering.IBitArray other)
  RVA=0x0B2D8EF4  token=0x600061D  UnityEngine.Rendering.IBitArray BitOr(UnityEngine.Rendering.IBitArray other)
  RVA=0x0B2D8E94  token=0x600061E  UnityEngine.Rendering.IBitArray BitNot()
  RVA=0x067C4D3C  token=0x600061F  System.Boolean op_Equality(UnityEngine.Rendering.BitArray128 a, UnityEngine.Rendering.BitArray128 b)
  RVA=0x067C4D54  token=0x6000620  System.Boolean op_Inequality(UnityEngine.Rendering.BitArray128 a, UnityEngine.Rendering.BitArray128 b)
  RVA=0x0B2D8F88  token=0x6000621  System.Boolean Equals(System.Object obj)
  RVA=0x04DB8E20  token=0x6000622  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.Rendering.BitArray256
TYPE:  sealed struct
TOKEN: 0x2000117
SIZE:  0x30
IMPLEMENTS: UnityEngine.Rendering.IBitArray
FIELDS:
  private           System.UInt64                   data1  // 0x10
  private           System.UInt64                   data2  // 0x18
  private           System.UInt64                   data3  // 0x20
  private           System.UInt64                   data4  // 0x28
PROPERTIES:
  capacity  get=0x04DB8F10
  allFalse  get=0x0B2D95A8
  allTrue  get=0x0B2D95C8
  humanizedData  get=0x0B2D95E8
  Item  get=0x0B2D9574  set=0x0B2D9924
METHODS:
  RVA=0x04D861C0  token=0x6000629  System.Void .ctor(System.UInt64 initValue1, System.UInt64 initValue2, System.UInt64 initValue3, System.UInt64 initValue4)
  RVA=0x0B2D9480  token=0x600062A  System.Void .ctor(System.Collections.Generic.IEnumerable<System.UInt32> bitIndexTrue)
  RVA=0x04DB8FA0  token=0x600062B  UnityEngine.Rendering.BitArray256 op_OnesComplement(UnityEngine.Rendering.BitArray256 a)
  RVA=0x04DB8F60  token=0x600062C  UnityEngine.Rendering.BitArray256 op_BitwiseOr(UnityEngine.Rendering.BitArray256 a, UnityEngine.Rendering.BitArray256 b)
  RVA=0x04DB8F20  token=0x600062D  UnityEngine.Rendering.BitArray256 op_BitwiseAnd(UnityEngine.Rendering.BitArray256 a, UnityEngine.Rendering.BitArray256 b)
  RVA=0x0B2D91E8  token=0x600062E  UnityEngine.Rendering.IBitArray BitAnd(UnityEngine.Rendering.IBitArray other)
  RVA=0x0B2D9318  token=0x600062F  UnityEngine.Rendering.IBitArray BitOr(UnityEngine.Rendering.IBitArray other)
  RVA=0x0B2D92A8  token=0x6000630  UnityEngine.Rendering.IBitArray BitNot()
  RVA=0x0B2D98B4  token=0x6000631  System.Boolean op_Equality(UnityEngine.Rendering.BitArray256 a, UnityEngine.Rendering.BitArray256 b)
  RVA=0x0B2D98EC  token=0x6000632  System.Boolean op_Inequality(UnityEngine.Rendering.BitArray256 a, UnityEngine.Rendering.BitArray256 b)
  RVA=0x0B2D93D8  token=0x6000633  System.Boolean Equals(System.Object obj)
  RVA=0x04DB8EC0  token=0x6000634  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.Rendering.BitArrayUtilities
TYPE:  static class
TOKEN: 0x2000118
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B2DA0B4  token=0x6000635  System.Boolean Get8(System.UInt32 index, System.Byte data)
  RVA=0x0489D0A0  token=0x6000636  System.Boolean Get16(System.UInt32 index, System.UInt16 data)
  RVA=0x0690DAE4  token=0x6000637  System.Boolean Get32(System.UInt32 index, System.UInt32 data)
  RVA=0x0B2DA0A0  token=0x6000638  System.Boolean Get64(System.UInt32 index, System.UInt64 data)
  RVA=0x03A3D9D0  token=0x6000639  System.Boolean Get128(System.UInt32 index, System.UInt64 data1, System.UInt64 data2)
  RVA=0x0B2DA060  token=0x600063A  System.Boolean Get256(System.UInt32 index, System.UInt64 data1, System.UInt64 data2, System.UInt64 data3, System.UInt64 data4)
  RVA=0x0B2DA168  token=0x600063B  System.Void Set8(System.UInt32 index, System.Byte& data, System.Boolean value)
  RVA=0x04489110  token=0x600063C  System.Void Set16(System.UInt32 index, System.UInt16& data, System.Boolean value)
  RVA=0x0B2DA134  token=0x600063D  System.Void Set32(System.UInt32 index, System.UInt32& data, System.Boolean value)
  RVA=0x0B2DA14C  token=0x600063E  System.Void Set64(System.UInt32 index, System.UInt64& data, System.Boolean value)
  RVA=0x03B201E0  token=0x600063F  System.Void Set128(System.UInt32 index, System.UInt64& data1, System.UInt64& data2, System.Boolean value)
  RVA=0x0B2DA0CC  token=0x6000640  System.Void Set256(System.UInt32 index, System.UInt64& data1, System.UInt64& data2, System.UInt64& data3, System.UInt64& data4, System.Boolean value)
END_CLASS

CLASS: UnityEngine.Rendering.Blitter
TYPE:  static class
TOKEN: 0x2000119
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.Material            s_Blit  // static @ 0x0
  private   static  UnityEngine.Material            s_BlitTexArray  // static @ 0x8
  private   static  UnityEngine.Material            s_BlitTexArraySingleSlice  // static @ 0x10
  private   static  UnityEngine.Material            s_BlitMotionVector  // static @ 0x18
  private   static  UnityEngine.Material            s_BlitColorAndDepth  // static @ 0x20
  private   static  UnityEngine.Material            s_LowResBlit  // static @ 0x28
  private   static  UnityEngine.MaterialPropertyBlocks_PropertyBlock  // static @ 0x30
  private   static  UnityEngine.Mesh                s_TriangleMesh  // static @ 0x38
  private   static  UnityEngine.Mesh                s_QuadMesh  // static @ 0x40
METHODS:
  RVA=0x04775980  token=0x6000641  System.Void Initialize(UnityEngine.Shader blitPS, UnityEngine.Shader blitColorAndDepthPS, UnityEngine.Shader blitMotionVectorPS, UnityEngine.Shader lowResBlitPS)
  RVA=0x037DC4E0  token=0x6000642  System.Void Cleanup()
  RVA=0x03D24420  token=0x6000643  UnityEngine.Material GetBlitMaterial(System.Boolean isFP32Output, UnityEngine.Rendering.TextureDimension dimension, System.Boolean singleSlice)
  RVA=0x0B2DC374  token=0x6000644  UnityEngine.Material GetBlitColorAndDepth(System.Boolean isFP32Output)
  RVA=0x0B2DC3C4  token=0x6000645  UnityEngine.Material GetBlitMotionVectorMaterial()
  RVA=0x0B2DC414  token=0x6000646  UnityEngine.Material GetLowResBlitMaterial()
  RVA=0x0B2DC1AC  token=0x6000647  System.Void DrawTriangle(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Material material, System.Int32 shaderPass)
  RVA=0x0B2DBFE0  token=0x6000648  System.Void DrawQuad(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Material material, System.Int32 shaderPass)
  RVA=0x0B2DBEB8  token=0x6000649  System.Void BlitTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle source, UnityEngine.Vector4 scaleBias, System.Single mipLevel, System.Boolean bilinear)
  RVA=0x0B2DBC84  token=0x600064A  System.Void BlitTexture2D(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle source, UnityEngine.Vector4 scaleBias, System.Single mipLevel, System.Boolean bilinear)
  RVA=0x0B2DBD98  token=0x600064B  System.Void BlitTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle source, UnityEngine.Vector4 scaleBias, UnityEngine.Material material, System.Int32 pass)
  RVA=0x0B2DA4A0  token=0x600064C  System.Void BlitCameraTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle source, UnityEngine.Rendering.RTHandle destination, System.Single mipLevel, System.Boolean bilinear)
  RVA=0x0B2DA2FC  token=0x600064D  System.Void BlitCameraTexture2D(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle source, UnityEngine.Rendering.RTHandle destination, System.Single mipLevel, System.Boolean bilinear)
  RVA=0x0B2DA8C8  token=0x600064E  System.Void BlitCameraTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle source, UnityEngine.Rendering.RTHandle destination, UnityEngine.Material material, System.Int32 pass)
  RVA=0x0B2DA7FC  token=0x600064F  System.Void BlitCameraTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle source, UnityEngine.Rendering.RTHandle destination, UnityEngine.Vector4 scaleBias, System.Single mipLevel, System.Boolean bilinear)
  RVA=0x0B2DA644  token=0x6000650  System.Void BlitCameraTexture(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle source, UnityEngine.Rendering.RTHandle destination, UnityEngine.Rect destViewport, System.Single mipLevel, System.Boolean bilinear)
  RVA=0x0B2DBAC0  token=0x6000651  System.Void BlitQuad(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture source, UnityEngine.Vector4 scaleBiasTex, UnityEngine.Vector4 scaleBiasRT, System.Int32 mipLevelTex, System.Boolean bilinear)
  RVA=0x0B2DB7F8  token=0x6000652  System.Void BlitQuadWithPadding(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture source, UnityEngine.Vector2 textureSize, UnityEngine.Vector4 scaleBiasTex, UnityEngine.Vector4 scaleBiasRT, System.Int32 mipLevelTex, System.Boolean bilinear, System.Int32 paddingInPixels)
  RVA=0x0B2DB530  token=0x6000653  System.Void BlitQuadWithPaddingMultiply(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture source, UnityEngine.Vector2 textureSize, UnityEngine.Vector4 scaleBiasTex, UnityEngine.Vector4 scaleBiasRT, System.Int32 mipLevelTex, System.Boolean bilinear, System.Int32 paddingInPixels)
  RVA=0x0B2DB0A0  token=0x6000654  System.Void BlitOctahedralWithPadding(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture source, UnityEngine.Vector2 textureSize, UnityEngine.Vector4 scaleBiasTex, UnityEngine.Vector4 scaleBiasRT, System.Int32 mipLevelTex, System.Boolean bilinear, System.Int32 paddingInPixels)
  RVA=0x0B2DAE54  token=0x6000655  System.Void BlitOctahedralWithPaddingMultiply(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture source, UnityEngine.Vector2 textureSize, UnityEngine.Vector4 scaleBiasTex, UnityEngine.Vector4 scaleBiasRT, System.Int32 mipLevelTex, System.Boolean bilinear, System.Int32 paddingInPixels)
  RVA=0x0B2DACA0  token=0x6000656  System.Void BlitCubeToOctahedral2DQuad(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture source, UnityEngine.Vector4 scaleBiasRT, System.Int32 mipLevelTex)
  RVA=0x0B2DAA58  token=0x6000657  System.Void BlitCubeToOctahedral2DQuadSingleChannel(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture source, UnityEngine.Vector4 scaleBiasRT, System.Int32 mipLevelTex)
  RVA=0x0B2DB2EC  token=0x6000658  System.Void BlitQuadSingleChannel(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Texture source, UnityEngine.Vector4 scaleBiasTex, UnityEngine.Vector4 scaleBiasRT, System.Int32 mipLevelTex)
  RVA=0x04D22230  token=0x6000659  System.Void .cctor()
  RVA=0x0B2DC55C  token=0x600065A  UnityEngine.Vector3[] <Initialize>g__GetFullScreenTriangleVertexPosition|10_0(System.Single z)
  RVA=0x0B2DC464  token=0x600065B  UnityEngine.Vector2[] <Initialize>g__GetFullScreenTriangleTexCoord|10_1()
  RVA=0x0B2DC6F0  token=0x600065C  UnityEngine.Vector3[] <Initialize>g__GetQuadVertexPosition|10_2(System.Single z)
  RVA=0x0B2DC628  token=0x600065D  UnityEngine.Vector2[] <Initialize>g__GetQuadTexCoord|10_3()
END_CLASS

CLASS: UnityEngine.Rendering.CameraCaptureBridge
TYPE:  static class
TOKEN: 0x200011B
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.Dictionary<UnityEngine.Camera,System.Collections.Generic.HashSet<System.Action<UnityEngine.Rendering.RenderTargetIdentifier,UnityEngine.Rendering.CommandBuffer>>>actionDict  // static @ 0x0
  private   static  System.Boolean                  _enabled  // static @ 0x8
PROPERTIES:
  enabled  get=0x0B2DC9CC  set=0x0B2DCA1C
METHODS:
  RVA=0x0324C8D0  token=0x6000661  System.Collections.Generic.IEnumerator<System.Action<UnityEngine.Rendering.RenderTargetIdentifier,UnityEngine.Rendering.CommandBuffer>> GetCaptureActions(UnityEngine.Camera camera)
  RVA=0x0B2DC7B0  token=0x6000662  System.Void AddCaptureAction(UnityEngine.Camera camera, System.Action<UnityEngine.Rendering.RenderTargetIdentifier,UnityEngine.Rendering.CommandBuffer> action)
  RVA=0x0B2DC8F4  token=0x6000663  System.Void RemoveCaptureAction(UnityEngine.Camera camera, System.Action<UnityEngine.Rendering.RenderTargetIdentifier,UnityEngine.Rendering.CommandBuffer> action)
  RVA=0x04D18A80  token=0x6000664  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.ColorUtils
TYPE:  static class
TOKEN: 0x200011C
SIZE:  0x10
FIELDS:
  public    static  System.Single                   s_LightMeterCalibrationConstant  // static @ 0x0
  public    static  System.Single                   s_LensAttenuation  // static @ 0x4
PROPERTIES:
  lensImperfectionExposureScale  get=0x0B2DD54C
METHODS:
  RVA=0x04DB9180  token=0x6000666  System.Single StandardIlluminantY(System.Single x)
  RVA=0x0B2DCA90  token=0x6000667  UnityEngine.Vector3 CIExyToLMS(System.Single x, System.Single y)
  RVA=0x0B2DCB20  token=0x6000668  UnityEngine.Vector3 ColorBalanceToLMSCoeffs(System.Single temperature, System.Single tint)
  RVA=0x0B2DD168  token=0x6000669  System.ValueTuple<UnityEngine.Vector4,UnityEngine.Vector4,UnityEngine.Vector4> PrepareShadowsMidtonesHighlights(UnityEngine.Vector4& inShadows, UnityEngine.Vector4& inMidtones, UnityEngine.Vector4& inHighlights)
  RVA=0x0B2DCDE4  token=0x600066A  System.ValueTuple<UnityEngine.Vector4,UnityEngine.Vector4,UnityEngine.Vector4> PrepareLiftGammaGain(UnityEngine.Vector4& inLift, UnityEngine.Vector4& inGamma, UnityEngine.Vector4& inGain)
  RVA=0x0B2DD3C8  token=0x600066B  System.ValueTuple<UnityEngine.Vector4,UnityEngine.Vector4> PrepareSplitToning(UnityEngine.Vector4& inShadows, UnityEngine.Vector4& inHighlights, System.Single balance)
  RVA=0x04DB9150  token=0x600066C  System.Single Luminance(UnityEngine.Color& color)
  RVA=0x0B2DCC9C  token=0x600066D  System.Single ComputeEV100(System.Single aperture, System.Single shutterSpeed, System.Single ISO)
  RVA=0x0B2DCD0C  token=0x600066E  System.Single ConvertEV100ToExposure(System.Single EV100)
  RVA=0x0B2DCD80  token=0x600066F  System.Single ConvertExposureToEV100(System.Single exposure)
  RVA=0x0B2DCC34  token=0x6000670  System.Single ComputeEV100FromAvgLuminance(System.Single avgLuminance)
  RVA=0x0B2DCCC0  token=0x6000671  System.Single ComputeISO(System.Single aperture, System.Single shutterSpeed, System.Single targetEV100)
  RVA=0x0B2DD454  token=0x6000672  System.UInt32 ToHex(UnityEngine.Color c)
  RVA=0x0B2DD4D4  token=0x6000673  UnityEngine.Color ToRGBA(System.UInt32 hex)
  RVA=0x04D5E270  token=0x6000674  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.CoreMatrixUtils
TYPE:  static class
TOKEN: 0x200011D
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04DB91B0  token=0x6000675  System.Void MatrixTimesTranslation(UnityEngine.Matrix4x4& inOutMatrix, UnityEngine.Vector3 translation)
  RVA=0x04DB9680  token=0x6000676  System.Void TranslationTimesMatrix(UnityEngine.Matrix4x4& inOutMatrix, UnityEngine.Vector3 translation)
  RVA=0x04DB9530  token=0x6000677  UnityEngine.Matrix4x4 MultiplyPerspectiveMatrix(UnityEngine.Matrix4x4 perspective, UnityEngine.Matrix4x4 rhs)
  RVA=0x04DB93F0  token=0x6000678  UnityEngine.Matrix4x4 MultiplyOrthoMatrixCentered(UnityEngine.Matrix4x4 ortho, UnityEngine.Matrix4x4 rhs)
  RVA=0x04DB9240  token=0x6000679  UnityEngine.Matrix4x4 MultiplyGenericOrthoMatrix(UnityEngine.Matrix4x4 ortho, UnityEngine.Matrix4x4 rhs)
  RVA=0x0B2DD5B0  token=0x600067A  UnityEngine.Matrix4x4 MultiplyOrthoMatrix(UnityEngine.Matrix4x4 ortho, UnityEngine.Matrix4x4 rhs, System.Boolean centered)
  RVA=0x0B2DD6A0  token=0x600067B  UnityEngine.Matrix4x4 MultiplyProjectionMatrix(UnityEngine.Matrix4x4 projMatrix, UnityEngine.Matrix4x4 rhs, System.Boolean orthoCentered)
END_CLASS

CLASS: UnityEngine.Rendering.CoreUtils
TYPE:  static class
TOKEN: 0x200011E
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Vector3[]           lookAtList  // static @ 0x0
  public    static readonly UnityEngine.Vector3[]           upVectorList  // static @ 0x8
  private   static  System.String                   obsoletePriorityMessage  // const
  public    static  System.Int32                    editMenuPriority1  // const
  public    static  System.Int32                    editMenuPriority2  // const
  public    static  System.Int32                    editMenuPriority3  // const
  public    static  System.Int32                    editMenuPriority4  // const
  public    static  System.Int32                    assetCreateMenuPriority1  // const
  public    static  System.Int32                    assetCreateMenuPriority2  // const
  public    static  System.Int32                    assetCreateMenuPriority3  // const
  public    static  System.Int32                    gameObjectMenuPriority  // const
  private   static  UnityEngine.Cubemap             m_BlackCubeTexture  // static @ 0x10
  private   static  UnityEngine.Cubemap             m_MagentaCubeTexture  // static @ 0x18
  private   static  UnityEngine.CubemapArray        m_MagentaCubeTextureArray  // static @ 0x20
  private   static  UnityEngine.Cubemap             m_WhiteCubeTexture  // static @ 0x28
  private   static  UnityEngine.RenderTexture       m_EmptyUAV  // static @ 0x30
  private   static  UnityEngine.Texture3D           m_BlackVolumeTexture  // static @ 0x38
  private   static  System.Collections.Generic.IEnumerable<System.Type>m_AssemblyTypes  // static @ 0x40
PROPERTIES:
  blackCubeTexture  get=0x0B2E03DC
  magentaCubeTexture  get=0x0B2E0A38
  magentaCubeTextureArray  get=0x0B2E0868
  whiteCubeTexture  get=0x0B2E0BCC
  emptyUAV  get=0x0B2E0720
  blackVolumeTexture  get=0x0B2E0570
METHODS:
  RVA=0x0B2DD8E0  token=0x6000682  System.Void ClearRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor)
  RVA=0x0B2DE678  token=0x6000683  System.Int32 FixupDepthSlice(System.Int32 depthSlice, UnityEngine.Rendering.RTHandle buffer)
  RVA=0x0B2DE668  token=0x6000684  System.Int32 FixupDepthSlice(System.Int32 depthSlice, UnityEngine.CubemapFace cubemapFace)
  RVA=0x0B2DF178  token=0x6000685  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier buffer, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice)
  RVA=0x0B2DF368  token=0x6000686  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier buffer, UnityEngine.Rendering.ClearFlag clearFlag, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice)
  RVA=0x0B2DFFF8  token=0x6000687  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice)
  RVA=0x0B2DF258  token=0x6000688  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, UnityEngine.Rendering.ClearFlag clearFlag, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice)
  RVA=0x0B2DFA34  token=0x6000689  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice)
  RVA=0x0B2DF0C0  token=0x600068A  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, UnityEngine.Rendering.RenderTargetIdentifier depthBuffer)
  RVA=0x0B2DF000  token=0x600068B  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, UnityEngine.Rendering.ClearFlag clearFlag)
  RVA=0x0B2DFB4C  token=0x600068C  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor)
  RVA=0x0B2E0100  token=0x600068D  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier buffer, UnityEngine.Rendering.RenderBufferLoadAction loadAction, UnityEngine.Rendering.RenderBufferStoreAction storeAction, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor)
  RVA=0x0B2DFD24  token=0x600068E  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier buffer, UnityEngine.Rendering.RenderBufferLoadAction loadAction, UnityEngine.Rendering.RenderBufferStoreAction storeAction, UnityEngine.Rendering.ClearFlag clearFlag)
  RVA=0x0B2DFEDC  token=0x600068F  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, UnityEngine.Rendering.RenderBufferLoadAction colorLoadAction, UnityEngine.Rendering.RenderBufferStoreAction colorStoreAction, UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, UnityEngine.Rendering.RenderBufferLoadAction depthLoadAction, UnityEngine.Rendering.RenderBufferStoreAction depthStoreAction, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor)
  RVA=0x0B2DF43C  token=0x6000690  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier buffer, UnityEngine.Rendering.RenderBufferLoadAction colorLoadAction, UnityEngine.Rendering.RenderBufferStoreAction colorStoreAction, UnityEngine.Rendering.RenderBufferLoadAction depthLoadAction, UnityEngine.Rendering.RenderBufferStoreAction depthStoreAction, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor)
  RVA=0x0B2DF748  token=0x6000691  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, UnityEngine.Rendering.RenderBufferLoadAction colorLoadAction, UnityEngine.Rendering.RenderBufferStoreAction colorStoreAction, UnityEngine.Rendering.RenderTargetIdentifier depthBuffer, UnityEngine.Rendering.RenderBufferLoadAction depthLoadAction, UnityEngine.Rendering.RenderBufferStoreAction depthStoreAction, UnityEngine.Rendering.ClearFlag clearFlag)
  RVA=0x0B2E0278  token=0x6000692  System.Void SetViewportAndClear(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle buffer, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor)
  RVA=0x0B2DFC20  token=0x6000693  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle buffer, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice)
  RVA=0x0B2E01C8  token=0x6000694  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle buffer, UnityEngine.Rendering.ClearFlag clearFlag, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice)
  RVA=0x0B2DF61C  token=0x6000695  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle colorBuffer, UnityEngine.Rendering.RTHandle depthBuffer, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice)
  RVA=0x0B2DEDCC  token=0x6000696  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle colorBuffer, UnityEngine.Rendering.RTHandle depthBuffer, UnityEngine.Rendering.ClearFlag clearFlag, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice)
  RVA=0x0B2DF868  token=0x6000697  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle colorBuffer, UnityEngine.Rendering.RTHandle depthBuffer, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor, System.Int32 miplevel, UnityEngine.CubemapFace cubemapFace, System.Int32 depthSlice)
  RVA=0x0B2DEF00  token=0x6000698  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, UnityEngine.Rendering.RTHandle depthBuffer)
  RVA=0x0B2DF520  token=0x6000699  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, UnityEngine.Rendering.RTHandle depthBuffer, UnityEngine.Rendering.ClearFlag clearFlag)
  RVA=0x0B2DFDF0  token=0x600069A  System.Void SetRenderTarget(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, UnityEngine.Rendering.RTHandle depthBuffer, UnityEngine.Rendering.ClearFlag clearFlag, UnityEngine.Color clearColor)
  RVA=0x0B2E0300  token=0x600069B  System.Void SetViewport(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RTHandle target)
  RVA=0x037E0EB0  token=0x600069C  System.String GetRenderTargetAutoName(System.Int32 width, System.Int32 height, System.Int32 depth, UnityEngine.RenderTextureFormat format, System.String name, System.Boolean mips, System.Boolean enableMSAA, UnityEngine.Rendering.MSAASamples msaaSamples)
  RVA=0x0B2DE6E8  token=0x600069D  System.String GetRenderTargetAutoName(System.Int32 width, System.Int32 height, System.Int32 depth, UnityEngine.Experimental.Rendering.GraphicsFormat format, System.String name, System.Boolean mips, System.Boolean enableMSAA, UnityEngine.Rendering.MSAASamples msaaSamples)
  RVA=0x037DFB30  token=0x600069E  System.String GetRenderTargetAutoName(System.Int32 width, System.Int32 height, System.Int32 depth, UnityEngine.Experimental.Rendering.GraphicsFormat format, UnityEngine.Rendering.TextureDimension dim, System.String name, System.Boolean mips, System.Boolean enableMSAA, UnityEngine.Rendering.MSAASamples msaaSamples, System.Boolean dynamicRes)
  RVA=0x037DFC40  token=0x600069F  System.String GetRenderTargetAutoName(System.Int32 width, System.Int32 height, System.Int32 depth, System.String format, UnityEngine.Rendering.TextureDimension dim, System.String name, System.Boolean mips, System.Boolean enableMSAA, UnityEngine.Rendering.MSAASamples msaaSamples, System.Boolean dynamicRes)
  RVA=0x0B2DE890  token=0x60006A0  System.String GetTextureAutoName(System.Int32 width, System.Int32 height, UnityEngine.TextureFormat format, UnityEngine.Rendering.TextureDimension dim, System.String name, System.Boolean mips, System.Int32 depth)
  RVA=0x0B2DE7C8  token=0x60006A1  System.String GetTextureAutoName(System.Int32 width, System.Int32 height, UnityEngine.Experimental.Rendering.GraphicsFormat format, UnityEngine.Rendering.TextureDimension dim, System.String name, System.Boolean mips, System.Int32 depth)
  RVA=0x0B2DE958  token=0x60006A2  System.String GetTextureAutoName(System.Int32 width, System.Int32 height, System.String format, UnityEngine.Rendering.TextureDimension dim, System.String name, System.Boolean mips, System.Int32 depth)
  RVA=0x0B2DD790  token=0x60006A3  System.Void ClearCubemap(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.RenderTexture renderTexture, UnityEngine.Color clearColor, System.Boolean clearMips)
  RVA=0x0B2DE0CC  token=0x60006A4  System.Void DrawFullScreen(UnityEngine.Rendering.CommandBuffer commandBuffer, UnityEngine.Material material, UnityEngine.MaterialPropertyBlock properties, System.Int32 shaderPassId)
  RVA=0x0B2DE14C  token=0x60006A5  System.Void DrawFullScreen(UnityEngine.Rendering.CommandBuffer commandBuffer, UnityEngine.Material material, UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, UnityEngine.MaterialPropertyBlock properties, System.Int32 shaderPassId)
  RVA=0x0B2DE230  token=0x60006A6  System.Void DrawFullScreen(UnityEngine.Rendering.CommandBuffer commandBuffer, UnityEngine.Material material, UnityEngine.Rendering.RenderTargetIdentifier colorBuffer, UnityEngine.Rendering.RenderTargetIdentifier depthStencilBuffer, UnityEngine.MaterialPropertyBlock properties, System.Int32 shaderPassId)
  RVA=0x0B2DDF14  token=0x60006A7  System.Void DrawFullScreen(UnityEngine.Rendering.CommandBuffer commandBuffer, UnityEngine.Material material, UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, UnityEngine.Rendering.RenderTargetIdentifier depthStencilBuffer, UnityEngine.MaterialPropertyBlock properties, System.Int32 shaderPassId)
  RVA=0x0B2DE000  token=0x60006A8  System.Void DrawFullScreen(UnityEngine.Rendering.CommandBuffer commandBuffer, UnityEngine.Material material, UnityEngine.Rendering.RenderTargetIdentifier[] colorBuffers, UnityEngine.MaterialPropertyBlock properties, System.Int32 shaderPassId)
  RVA=0x0B2DD964  token=0x60006A9  UnityEngine.Color ConvertSRGBToActiveColorSpace(UnityEngine.Color color)
  RVA=0x0B2DD920  token=0x60006AA  UnityEngine.Color ConvertLinearToActiveColorSpace(UnityEngine.Color color)
  RVA=0x0B2DDE14  token=0x60006AB  UnityEngine.Material CreateEngineMaterial(System.String shaderPath)
  RVA=0x0351F8A0  token=0x60006AC  UnityEngine.Material CreateEngineMaterial(UnityEngine.Shader shader, System.Boolean enableInstancing)
  RVA=-1  // generic def  token=0x60006AD  System.Boolean HasFlag(T mask, T flag)
  RVA=-1  // generic def  token=0x60006AE  System.Void Swap(T& a, T& b)
  RVA=0x0B2DED98  token=0x60006AF  System.Void SetKeyword(UnityEngine.Rendering.CommandBuffer cmd, System.String keyword, System.Boolean state)
  RVA=0x0690914C  token=0x60006B0  System.Void SetKeyword(UnityEngine.Material material, System.String keyword, System.Boolean state)
  RVA=0x0B2DED64  token=0x60006B1  System.Void SetKeyword(UnityEngine.ComputeShader cs, System.String keyword, System.Boolean state)
  RVA=0x037DD090  token=0x60006B2  System.Void Destroy(UnityEngine.Object obj)
  RVA=0x03B0CFF0  token=0x60006B3  System.Collections.Generic.IEnumerable<System.Type> GetAllAssemblyTypes()
  RVA=-1  // generic def  token=0x60006B4  System.Collections.Generic.IEnumerable<System.Type> GetAllTypesDerivedFrom()
  RVA=0x0B2DED54  token=0x60006B5  System.Void SafeRelease(UnityEngine.ComputeBuffer buffer)
  RVA=0x0B2DD9A8  token=0x60006B6  UnityEngine.Mesh CreateCubeMesh(UnityEngine.Vector3 min, UnityEngine.Vector3 max)
  RVA=0x02FFF600  token=0x60006B7  System.Boolean ArePostProcessesEnabled(UnityEngine.Camera camera)
  RVA=0x02FFF600  token=0x60006B8  System.Boolean AreAnimatedMaterialsEnabled(UnityEngine.Camera camera)
  RVA=0x012081B0  token=0x60006B9  System.Boolean IsSceneLightingDisabled(UnityEngine.Camera camera)
  RVA=0x012081B0  token=0x60006BA  System.Boolean IsLightOverlapDebugEnabled(UnityEngine.Camera camera)
  RVA=0x02FFF600  token=0x60006BB  System.Boolean IsSceneViewFogEnabled(UnityEngine.Camera camera)
  RVA=0x012081B0  token=0x60006BC  System.Boolean IsSceneFilteringEnabled()
  RVA=0x012081B0  token=0x60006BD  System.Boolean IsSceneViewPrefabStageContextHidden()
  RVA=0x0B2DE32C  token=0x60006BE  System.Void DrawRendererList(UnityEngine.Rendering.ScriptableRenderContext renderContext, UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Experimental.Rendering.RendererList rendererList)
  RVA=0x0B2DE5F0  token=0x60006BF  System.Void DrawRendererList(UnityEngine.Rendering.ScriptableRenderContext renderContext, UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Rendering.RendererUtils.RendererList rendererList)
  RVA=0x0B2DEC54  token=0x60006C0  System.Int32 GetTextureHash(UnityEngine.Texture texture)
  RVA=0x0B2DED20  token=0x60006C1  System.Int32 PreviousPowerOfTwo(System.Int32 size)
  RVA=-1  // generic def  token=0x60006C2  T GetLastEnumValue()
  RVA=0x0B2DE6BC  token=0x60006C3  System.String GetCorePath()
  RVA=0x048B65E0  token=0x60006C4  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.DelegateUtility
TYPE:  static class
TOKEN: 0x2000123
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B2E321C  token=0x60006CB  System.Delegate Cast(System.Delegate source, System.Type type)
END_CLASS

CLASS: UnityEngine.Rendering.FSRUtils
TYPE:  static class
TOKEN: 0x2000124
SIZE:  0x10
FIELDS:
  private   static  System.Single                   kMaxSharpnessStops  // const
  public    static  System.Single                   kDefaultSharpnessStops  // const
  public    static  System.Single                   kDefaultSharpnessLinear  // const
METHODS:
  RVA=0x0B2E3374  token=0x60006CC  System.Void SetEasuConstants(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Vector2 inputViewportSizeInPixels, UnityEngine.Vector2 inputImageSizeInPixels, UnityEngine.Vector2 outputImageSizeInPixels)
  RVA=0x0B2E3580  token=0x60006CD  System.Void SetRcasConstants(UnityEngine.Rendering.CommandBuffer cmd, System.Single sharpnessStops)
  RVA=0x0B2E3560  token=0x60006CE  System.Void SetRcasConstantsLinear(UnityEngine.Rendering.CommandBuffer cmd, System.Single sharpnessLinear)
  RVA=0x0B2E335C  token=0x60006CF  System.Boolean IsSupported()
END_CLASS

CLASS: UnityEngine.Rendering.HableCurve
TYPE:  class
TOKEN: 0x2000126
SIZE:  0x30
FIELDS:
  private           System.Single                   <whitePoint>k__BackingField  // 0x10
  private           System.Single                   <inverseWhitePoint>k__BackingField  // 0x14
  private           System.Single                   <x0>k__BackingField  // 0x18
  private           System.Single                   <x1>k__BackingField  // 0x1c
  public    readonly UnityEngine.Rendering.HableCurve.Segment[]segments  // 0x20
  public    readonly UnityEngine.Rendering.HableCurve.Uniformsuniforms  // 0x28
PROPERTIES:
  whitePoint  get=0x04D85F70  set=0x04D88280
  inverseWhitePoint  get=0x04D88D40  set=0x04D88D50
  x0  get=0x04D86380  set=0x04D86390
  x1  get=0x04D868F0  set=0x04D86900
METHODS:
  RVA=0x0B2E3E30  token=0x60006D9  System.Void .ctor()
  RVA=0x0B2E36CC  token=0x60006DA  System.Single Eval(System.Single x)
  RVA=0x0B2E3C1C  token=0x60006DB  System.Void Init(System.Single toeStrength, System.Single toeLength, System.Single shoulderStrength, System.Single shoulderLength, System.Single shoulderAngle, System.Single gamma)
  RVA=0x0B2E3734  token=0x60006DC  System.Void InitSegments(UnityEngine.Rendering.HableCurve.DirectParams srcParams)
  RVA=0x0B2E3DC0  token=0x60006DD  System.Void SolveAB(System.Single& lnA, System.Single& B, System.Single x0, System.Single y0, System.Single m)
  RVA=0x0B2E3684  token=0x60006DE  System.Void AsSlopeIntercept(System.Single& m, System.Single& b, System.Single x0, System.Single x1, System.Single y0, System.Single y1)
  RVA=0x04DB97E0  token=0x60006DF  System.Single EvalDerivativeLinearGamma(System.Single m, System.Single b, System.Single g, System.Single x)
END_CLASS

CLASS: UnityEngine.Rendering.HaltonSequence
TYPE:  static class
TOKEN: 0x200012A
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0326BFC0  token=0x60006EA  System.Single Get(System.Int32 index, System.Int32 radix)
END_CLASS

CLASS: UnityEngine.Rendering.MaterialQuality
TYPE:  sealed struct
TOKEN: 0x200012B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Rendering.MaterialQualityLow  // const
  public    static  UnityEngine.Rendering.MaterialQualityMedium  // const
  public    static  UnityEngine.Rendering.MaterialQualityHigh  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.MaterialQualityUtilities
TYPE:  static class
TOKEN: 0x200012C
SIZE:  0x10
FIELDS:
  public    static  System.String[]                 KeywordNames  // static @ 0x0
  public    static  System.String[]                 EnumNames  // static @ 0x8
  public    static  UnityEngine.Rendering.ShaderKeyword[]Keywords  // static @ 0x10
METHODS:
  RVA=0x0B2E4090  token=0x60006EB  UnityEngine.Rendering.MaterialQuality GetHighestQuality(UnityEngine.Rendering.MaterialQuality levels)
  RVA=0x0B2E3F88  token=0x60006EC  UnityEngine.Rendering.MaterialQuality GetClosestQuality(UnityEngine.Rendering.MaterialQuality availableLevels, UnityEngine.Rendering.MaterialQuality requestedLevel)
  RVA=0x0B2E4104  token=0x60006ED  System.Void SetGlobalShaderKeywords(UnityEngine.Rendering.MaterialQuality level)
  RVA=0x0B2E41E0  token=0x60006EE  System.Void SetGlobalShaderKeywords(UnityEngine.Rendering.MaterialQuality level, UnityEngine.Rendering.CommandBuffer cmd)
  RVA=0x0B2E42E0  token=0x60006EF  System.Int32 ToFirstIndex(UnityEngine.Rendering.MaterialQuality level)
  RVA=0x04D88AF0  token=0x60006F0  UnityEngine.Rendering.MaterialQuality FromIndex(System.Int32 index)
  RVA=0x0B2E435C  token=0x60006F1  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.MeshGizmo
TYPE:  class
TOKEN: 0x200012D
SIZE:  0x48
IMPLEMENTS: System.IDisposable
FIELDS:
  public    static readonly System.Int32                    vertexCountPerCube  // static @ 0x0
  public            UnityEngine.Mesh                mesh  // 0x10
  private           System.Collections.Generic.List<UnityEngine.Vector3>vertices  // 0x18
  private           System.Collections.Generic.List<System.Int32>indices  // 0x20
  private           System.Collections.Generic.List<UnityEngine.Color>colors  // 0x28
  private           UnityEngine.Material            wireMaterial  // 0x30
  private           UnityEngine.Material            dottedWireMaterial  // 0x38
  private           UnityEngine.Material            solidMaterial  // 0x40
METHODS:
  RVA=0x0B2E541C  token=0x60006F2  System.Void .ctor(System.Int32 capacity)
  RVA=0x0B2E4FA0  token=0x60006F3  System.Void Clear()
  RVA=0x0B2E465C  token=0x60006F4  System.Void AddWireCube(UnityEngine.Vector3 center, UnityEngine.Vector3 size, UnityEngine.Color color)
  RVA=0x0B2E506C  token=0x60006F5  System.Void DrawMesh(UnityEngine.Matrix4x4 trs, UnityEngine.Material mat, UnityEngine.MeshTopology topology, UnityEngine.Rendering.CompareFunction depthTest, System.String gizmoName)
  RVA=0x0B2E522C  token=0x60006F6  System.Void RenderWireframe(UnityEngine.Matrix4x4 trs, UnityEngine.Rendering.CompareFunction depthTest, System.String gizmoName)
  RVA=0x0B2E501C  token=0x60006F7  System.Void Dispose()
  RVA=0x0B2E53E0  token=0x60006F8  System.Void .cctor()
  RVA=0x0B2E5280  token=0x60006F9  System.Void <AddWireCube>g__AddEdge|10_0(UnityEngine.Vector3 p1, UnityEngine.Vector3 p2, UnityEngine.Rendering.MeshGizmo.<>c__DisplayClass10_0& )
END_CLASS

CLASS: UnityEngine.Rendering.ReloadAttribute
TYPE:  sealed class
TOKEN: 0x200012F
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60006FA  System.Void .ctor(System.String[] paths, UnityEngine.Rendering.ReloadAttribute.Package package)
  RVA=0x0B2E5FC0  token=0x60006FB  System.Void .ctor(System.String path, UnityEngine.Rendering.ReloadAttribute.Package package)
  RVA=0x041E1670  token=0x60006FC  System.Void .ctor(System.String pathFormat, System.Int32 rangeMin, System.Int32 rangeMax, UnityEngine.Rendering.ReloadAttribute.Package package)
END_CLASS

CLASS: UnityEngine.Rendering.ReloadGroupAttribute
TYPE:  sealed class
TOKEN: 0x2000131
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60006FD  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.TextureCurve
TYPE:  class
TOKEN: 0x2000132
SIZE:  0x40
IMPLEMENTS: System.IDisposable
FIELDS:
  private   static  System.Int32                    k_Precision  // const
  private   static  System.Single                   k_Step  // const
  private           System.Int32                    <length>k__BackingField  // 0x10
  private           System.Boolean                  m_Loop  // 0x14
  private           System.Single                   m_ZeroValue  // 0x18
  private           System.Single                   m_Range  // 0x1c
  private           UnityEngine.AnimationCurve      m_Curve  // 0x20
  private           UnityEngine.AnimationCurve      m_LoopingCurve  // 0x28
  private           UnityEngine.Texture2D           m_Texture  // 0x30
  private           System.Boolean                  m_IsCurveDirty  // 0x38
  private           System.Boolean                  m_IsTextureDirty  // 0x39
  private           System.Int32                    m_LastUpdatedFrame  // 0x3c
PROPERTIES:
  length  get=0x02B2E2D0  set=0x014F51F0
  Item  get=0x0B2E6588
METHODS:
  RVA=0x03C06DA0  token=0x6000700  System.Boolean IsTextureCurveDirty()
  RVA=0x0B2E6524  token=0x6000702  System.Void .ctor(UnityEngine.AnimationCurve baseCurve, System.Single zeroValue, System.Boolean loop, UnityEngine.Vector2& bounds)
  RVA=0x039C5760  token=0x6000703  System.Void .ctor(UnityEngine.Keyframe[] keys, System.Single zeroValue, System.Boolean loop, UnityEngine.Vector2& bounds)
  RVA=0x0421C210  token=0x6000704  System.Void Finalize()
  RVA=0x041E1670  token=0x6000705  System.Void Dispose()
  RVA=0x037DC8B0  token=0x6000706  System.Void Release()
  RVA=0x04DB9870  token=0x6000707  System.Void SetDirty()
  RVA=0x037E0A50  token=0x6000708  UnityEngine.Experimental.Rendering.GraphicsFormat GetTextureFormat()
  RVA=0x037E07A0  token=0x6000709  UnityEngine.Texture2D GetTexture()
  RVA=0x037E09B0  token=0x600070A  System.Single Evaluate(System.Single time)
  RVA=0x0B2E6448  token=0x600070B  System.Int32 AddKey(System.Single time, System.Single value)
  RVA=0x0B2E647C  token=0x600070C  System.Int32 MoveKey(System.Int32 index, UnityEngine.Keyframe& key)
  RVA=0x0B2E64CC  token=0x600070D  System.Void RemoveKey(System.Int32 index)
  RVA=0x0B2E64F8  token=0x600070E  System.Void SmoothTangents(System.Int32 index, System.Single weight)
END_CLASS

CLASS: UnityEngine.Rendering.TextureCurveParameter
TYPE:  class
TOKEN: 0x2000133
SIZE:  0x20
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x039C5700  token=0x600070F  System.Void .ctor(UnityEngine.Rendering.TextureCurve value, System.Boolean overrideState)
  RVA=0x037DC890  token=0x6000710  System.Void Release()
END_CLASS

CLASS: UnityEngine.Rendering.TileLayoutUtils
TYPE:  static class
TOKEN: 0x2000134
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B2E6770  token=0x6000711  System.Boolean TryLayoutByTiles(UnityEngine.RectInt src, System.UInt32 tileSize, UnityEngine.RectInt& main, UnityEngine.RectInt& topRow, UnityEngine.RectInt& rightCol, UnityEngine.RectInt& topRight)
  RVA=0x0B2E66FC  token=0x6000712  System.Boolean TryLayoutByRow(UnityEngine.RectInt src, System.UInt32 tileSize, UnityEngine.RectInt& main, UnityEngine.RectInt& other)
  RVA=0x0B2E668C  token=0x6000713  System.Boolean TryLayoutByCol(UnityEngine.RectInt src, System.UInt32 tileSize, UnityEngine.RectInt& main, UnityEngine.RectInt& other)
END_CLASS

CLASS: UnityEngine.Rendering.XRUtils
TYPE:  static class
TOKEN: 0x2000135
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B2E7AA4  token=0x6000714  System.Void DrawOcclusionMesh(UnityEngine.Rendering.CommandBuffer cmd, UnityEngine.Camera camera, System.Boolean stereoEnabled)
END_CLASS

CLASS: UnityEngine.Rendering.IVolume
TYPE:  interface
TOKEN: 0x2000136
FIELDS:
PROPERTIES:
  isGlobal  get=-1  // abstract  set=-1  // abstract
  colliders  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000718  System.Void Play()
  RVA=-1  // abstract  token=0x6000719  System.Void Stop()
END_CLASS

CLASS: UnityEngine.Rendering.Volume
TYPE:  class
TOKEN: 0x2000137
SIZE:  0x60
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.Rendering.IVolume
FIELDS:
  private           System.Boolean                  m_IsGlobal  // 0x18
  public            System.Boolean                  isLunaPlatform  // 0x19
  public            System.Action<UnityEngine.Rendering.VolumeProfile>LunaCreateVolumeProfile  // 0x20
  public            System.Single                   priority  // 0x28
  public            System.Single                   blendDistance  // 0x2c
  public            System.Single                   weight  // 0x30
  public            UnityEngine.Rendering.VolumeProfilesharedProfile  // 0x38
  private           System.Collections.Generic.List<UnityEngine.Collider>m_Colliders  // 0x40
  private           System.Int32                    m_PreviousLayer  // 0x48
  private           System.Single                   m_PreviousPriority  // 0x4c
  private           UnityEngine.Rendering.VolumeProfilem_InternalProfile  // 0x50
  public            System.Boolean                  vfxEnableState  // 0x58
PROPERTIES:
  isGlobal  get=0x04D86340  set=0x04D86350
  colliders  get=0x04D85A60
  profileRef  get=0x02FC7110
METHODS:
  RVA=0x0B2E7768  token=0x600071C  UnityEngine.Rendering.VolumeProfile GetInstantiatedProfile()
  RVA=0x02FDAEA0  token=0x600071D  System.Void SetInstantiatedProfile(UnityEngine.Rendering.VolumeProfile profile)
  RVA=0x0B2E79BC  token=0x6000720  System.Boolean HasInstantiatedProfile()
  RVA=0x04255790  token=0x6000721  System.Void OnEnable()
  RVA=0x04255700  token=0x6000722  System.Void OnDisable()
  RVA=0x031C4890  token=0x6000723  System.Void Update()
  RVA=0x031C4980  token=0x6000724  System.Void UpdateLayer()
  RVA=0x04DA7440  token=0x6000725  System.Void Play()
  RVA=0x04DB98D0  token=0x6000726  System.Void Stop()
  RVA=0x0395B350  token=0x6000727  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.VolumeComponentMenu
TYPE:  class
TOKEN: 0x2000138
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.String                   menu  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000728  System.Void .ctor(System.String menu)
END_CLASS

CLASS: UnityEngine.Rendering.VolumeComponentMenuForRenderPipeline
TYPE:  class
TOKEN: 0x2000139
SIZE:  0x20
EXTENDS: UnityEngine.Rendering.VolumeComponentMenu
FIELDS:
  private   readonly System.Type[]                   <pipelineTypes>k__BackingField  // 0x18
PROPERTIES:
  pipelineTypes  get=0x0385B100
METHODS:
  RVA=0x0B2E6BCC  token=0x600072A  System.Void .ctor(System.String menu, System.Type[] pipelineTypes)
END_CLASS

CLASS: UnityEngine.Rendering.VolumeComponentDeprecated
TYPE:  sealed class
TOKEN: 0x200013A
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600072B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.MigratingVolumeComponent
TYPE:  sealed class
TOKEN: 0x200013B
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600072C  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.VolumeComponent
TYPE:  class
TOKEN: 0x200013C
SIZE:  0x30
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  public            System.Boolean                  active  // 0x18
  private           System.String                   <displayName>k__BackingField  // 0x20
  private           UnityEngine.Rendering.VolumeParameter[]<parameters>k__BackingField  // 0x28
PROPERTIES:
  displayName  get=0x04D862C0  set=0x05390F40
  parameters  get=0x04D86240  set=0x02FDB880
METHODS:
  RVA=0x035938A0  token=0x6000731  System.Void FindParameters(System.Object o, System.Collections.Generic.List<UnityEngine.Rendering.VolumeParameter> parameters, System.Func<System.Reflection.FieldInfo,System.Boolean> filter)
  RVA=0x02FC49F0  token=0x6000732  System.Void OnEnable()
  RVA=0x02FC4950  token=0x6000733  System.Void OnDisable()
  RVA=0x02FC5B80  token=0x6000734  System.Void Override(UnityEngine.Rendering.VolumeComponent state, System.Single interpFactor)
  RVA=0x02FC4340  token=0x6000735  System.Void SetAllOverridesTo(System.Boolean state)
  RVA=0x02FC4110  token=0x6000736  System.Void SetOverridesTo(System.Collections.Generic.IEnumerable<UnityEngine.Rendering.VolumeParameter> enumerable, System.Boolean state)
  RVA=0x0B2E6D98  token=0x6000737  System.Int32 GetHashCode()
  RVA=0x02FC4830  token=0x6000738  System.Void OnDestroy()
  RVA=0x02FC4980  token=0x6000739  System.Void Release()
  RVA=0x03573680  token=0x600073A  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.VolumeManager
TYPE:  sealed class
TOKEN: 0x200013F
SIZE:  0x50
FIELDS:
  private   static readonly System.Lazy<UnityEngine.Rendering.VolumeManager>s_Instance  // static @ 0x0
  private           UnityEngine.Rendering.VolumeStack<stack>k__BackingField  // 0x10
  private           System.Type[]                   <baseComponentTypeArray>k__BackingField  // 0x18
  private   static  System.Int32                    k_MaxLayerCount  // const
  private   readonly System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<UnityEngine.Rendering.Volume>>m_SortedVolumes  // 0x20
  private   readonly System.Collections.Generic.List<UnityEngine.Rendering.Volume>m_Volumes  // 0x28
  private   readonly System.Collections.Generic.Dictionary<System.Int32,System.Boolean>m_SortNeeded  // 0x30
  private   readonly System.Collections.Generic.List<UnityEngine.Rendering.VolumeComponent>m_ComponentsDefaultState  // 0x38
  private   readonly System.Collections.Generic.List<UnityEngine.Collider>m_TempColliders  // 0x40
  private           UnityEngine.Rendering.VolumeStackm_DefaultStack  // 0x48
PROPERTIES:
  instance  get=0x03450900
  stack  get=0x02B2ECC0  set=0x053908C0
  baseComponentTypes  get=0x0385B100  set=0x0B2E7218
  baseComponentTypeArray  get=0x0385B100  set=0x05392C40
METHODS:
  RVA=0x046FE230  token=0x6000746  System.Void .ctor()
  RVA=0x02FC44D0  token=0x6000747  UnityEngine.Rendering.VolumeStack CreateStack()
  RVA=0x0B2E71A0  token=0x6000748  System.Void ResetMainStack()
  RVA=0x04D39BD0  token=0x6000749  System.Void DestroyStack(UnityEngine.Rendering.VolumeStack stack)
  RVA=0x02FC45C0  token=0x600074A  System.Void ReloadBaseTypes()
  RVA=0x042559C0  token=0x600074B  System.Void Register(UnityEngine.Rendering.Volume volume, System.Int32 layer)
  RVA=0x04255840  token=0x600074C  System.Void Unregister(UnityEngine.Rendering.Volume volume, System.Int32 layer)
  RVA=-1  // generic def  token=0x600074D  System.Boolean IsComponentActiveInMask(UnityEngine.LayerMask layerMask)
  RVA=0x04255BB0  token=0x600074E  System.Void SetLayerDirty(System.Int32 layer)
  RVA=0x0B2E71B4  token=0x600074F  System.Void UpdateVolumeLayer(UnityEngine.Rendering.Volume volume, System.Int32 prevLayer, System.Int32 newLayer)
  RVA=0x02FC58F0  token=0x6000750  System.Void OverrideData(UnityEngine.Rendering.VolumeStack stack, System.Collections.Generic.List<UnityEngine.Rendering.VolumeComponent> components, System.Single interpFactor)
  RVA=0x031B7B60  token=0x6000751  System.Void ReplaceData(UnityEngine.Rendering.VolumeStack stack)
  RVA=0x0B2E6E04  token=0x6000752  System.Void CheckBaseTypes()
  RVA=0x0B2E6E9C  token=0x6000753  System.Void CheckStack(UnityEngine.Rendering.VolumeStack stack)
  RVA=0x03D52330  token=0x6000754  System.Boolean CheckUpdateRequired(UnityEngine.Rendering.VolumeStack stack)
  RVA=0x0B2E71F4  token=0x6000755  System.Void Update(UnityEngine.Transform trigger, UnityEngine.LayerMask layerMask)
  RVA=0x02FC67F0  token=0x6000756  System.Void Update(UnityEngine.Rendering.VolumeStack stack, UnityEngine.Transform trigger, UnityEngine.LayerMask layerMask)
  RVA=0x0B2E7064  token=0x6000757  UnityEngine.Rendering.Volume[] GetVolumes(UnityEngine.LayerMask layerMask)
  RVA=0x036DB830  token=0x6000758  System.Collections.Generic.List<UnityEngine.Rendering.Volume> GrabVolumes(UnityEngine.LayerMask mask)
  RVA=0x04331D70  token=0x6000759  System.Void SortByPriority(System.Collections.Generic.List<UnityEngine.Rendering.Volume> volumes)
  RVA=0x02FFF600  token=0x600075A  System.Boolean IsVolumeRenderedByCamera(UnityEngine.Rendering.Volume volume, UnityEngine.Camera camera)
  RVA=0x04B705A0  token=0x600075B  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.VolumeIsolationScope
TYPE:  sealed struct
TOKEN: 0x2000141
SIZE:  0x11
IMPLEMENTS: System.IDisposable
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000761  System.Void .ctor(System.Boolean unused)
  RVA=0x041E1670  token=0x6000762  System.Void System.IDisposable.Dispose()
END_CLASS

CLASS: UnityEngine.Rendering.VolumeParameter
TYPE:  abstract class
TOKEN: 0x2000142
SIZE:  0x18
FIELDS:
  public    static  System.String                   k_DebuggerDisplay  // const
  public            System.Boolean                  overrideState  // 0x10
METHODS:
  RVA=-1  // abstract  token=0x6000763  System.Void Interp(UnityEngine.Rendering.VolumeParameter from, UnityEngine.Rendering.VolumeParameter to, System.Single t)
  RVA=-1  // generic def  token=0x6000764  T GetValue()
  RVA=-1  // abstract  token=0x6000765  System.Void SetValue(UnityEngine.Rendering.VolumeParameter parameter)
  RVA=0x041E1670  token=0x6000766  System.Void OnEnable()
  RVA=0x041E1670  token=0x6000767  System.Void OnDisable()
  RVA=0x041E1670  token=0x6000768  System.Void Release()
  RVA=0x041E1670  token=0x6000769  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.VolumeParameter`1
TYPE:  class
TOKEN: 0x2000143
EXTENDS: UnityEngine.Rendering.VolumeParameter
IMPLEMENTS: System.IEquatable`1
FIELDS:
  protected         T                               m_Value  // 0x0
PROPERTIES:
  value  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600076C  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600076D  System.Void .ctor(T value, System.Boolean overrideState)
  RVA=-1  // not resolved  token=0x600076E  System.Void Interp(UnityEngine.Rendering.VolumeParameter from, UnityEngine.Rendering.VolumeParameter to, System.Single t)
  RVA=-1  // not resolved  token=0x600076F  System.Void Interp(T from, T to, System.Single t)
  RVA=-1  // not resolved  token=0x6000770  System.Void Override(T x)
  RVA=-1  // not resolved  token=0x6000771  System.Void SetValue(UnityEngine.Rendering.VolumeParameter parameter)
  RVA=-1  // not resolved  token=0x6000772  System.Int32 GetHashCode()
  RVA=-1  // not resolved  token=0x6000773  System.String ToString()
  RVA=-1  // not resolved  token=0x6000774  System.Boolean op_Equality(UnityEngine.Rendering.VolumeParameter<T> lhs, T rhs)
  RVA=-1  // not resolved  token=0x6000775  System.Boolean op_Inequality(UnityEngine.Rendering.VolumeParameter<T> lhs, T rhs)
  RVA=-1  // not resolved  token=0x6000776  System.Boolean Equals(UnityEngine.Rendering.VolumeParameter<T> other)
  RVA=-1  // not resolved  token=0x6000777  System.Boolean Equals(System.Object obj)
  RVA=-1  // not resolved  token=0x6000778  T op_Explicit(UnityEngine.Rendering.VolumeParameter<T> prop)
END_CLASS

CLASS: UnityEngine.Rendering.BoolParameter
TYPE:  class
TOKEN: 0x2000144
SIZE:  0x20
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x04DA0980  token=0x6000779  System.Void .ctor(System.Boolean value, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.LayerMaskParameter
TYPE:  class
TOKEN: 0x2000145
SIZE:  0x20
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x0B2E3F34  token=0x600077A  System.Void .ctor(UnityEngine.LayerMask value, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.IntParameter
TYPE:  class
TOKEN: 0x2000146
SIZE:  0x20
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x047A60C0  token=0x600077B  System.Void .ctor(System.Int32 value, System.Boolean overrideState)
  RVA=0x04DB9830  token=0x600077C  System.Void Interp(System.Int32 from, System.Int32 to, System.Single t)
END_CLASS

CLASS: UnityEngine.Rendering.NoInterpIntParameter
TYPE:  class
TOKEN: 0x2000147
SIZE:  0x20
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x0B2E59D0  token=0x600077D  System.Void .ctor(System.Int32 value, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.MinIntParameter
TYPE:  class
TOKEN: 0x2000148
SIZE:  0x28
EXTENDS: UnityEngine.Rendering.IntParameter
FIELDS:
  public            System.Int32                    min  // 0x20
PROPERTIES:
  value  get=0x011EF5B0  set=0x0B2E55A0
METHODS:
  RVA=0x0B2E4620  token=0x6000780  System.Void .ctor(System.Int32 value, System.Int32 min, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.NoInterpMinIntParameter
TYPE:  class
TOKEN: 0x2000149
SIZE:  0x28
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
  public            System.Int32                    min  // 0x20
PROPERTIES:
  value  get=0x011EF5B0  set=0x0B2E55A0
METHODS:
  RVA=0x0B2E5B40  token=0x6000783  System.Void .ctor(System.Int32 value, System.Int32 min, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.MaxIntParameter
TYPE:  class
TOKEN: 0x200014A
SIZE:  0x28
EXTENDS: UnityEngine.Rendering.IntParameter
FIELDS:
  public            System.Int32                    max  // 0x20
PROPERTIES:
  value  get=0x011EF5B0  set=0x0B2E4650
METHODS:
  RVA=0x0B2E4620  token=0x6000786  System.Void .ctor(System.Int32 value, System.Int32 max, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.NoInterpMaxIntParameter
TYPE:  class
TOKEN: 0x200014B
SIZE:  0x28
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
  public            System.Int32                    max  // 0x20
PROPERTIES:
  value  get=0x011EF5B0  set=0x0B2E4650
METHODS:
  RVA=0x0B2E5A84  token=0x6000789  System.Void .ctor(System.Int32 value, System.Int32 max, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.ClampedIntParameter
TYPE:  class
TOKEN: 0x200014C
SIZE:  0x28
EXTENDS: UnityEngine.Rendering.IntParameter
FIELDS:
  public            System.Int32                    min  // 0x20
  public            System.Int32                    max  // 0x24
PROPERTIES:
  value  get=0x011EF5B0  set=0x0B2DCA70
METHODS:
  RVA=0x05397614  token=0x600078C  System.Void .ctor(System.Int32 value, System.Int32 min, System.Int32 max, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.NoInterpClampedIntParameter
TYPE:  class
TOKEN: 0x200014D
SIZE:  0x28
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
  public            System.Int32                    min  // 0x20
  public            System.Int32                    max  // 0x24
PROPERTIES:
  value  get=0x011EF5B0  set=0x0B2DCA70
METHODS:
  RVA=0x0B2E56CC  token=0x600078F  System.Void .ctor(System.Int32 value, System.Int32 min, System.Int32 max, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.FloatParameter
TYPE:  class
TOKEN: 0x200014E
SIZE:  0x20
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x04DA09C0  token=0x6000790  System.Void .ctor(System.Single value, System.Boolean overrideState)
  RVA=0x04DB9770  token=0x6000791  System.Void Interp(System.Single from, System.Single to, System.Single t)
END_CLASS

CLASS: UnityEngine.Rendering.NoInterpFloatParameter
TYPE:  class
TOKEN: 0x200014F
SIZE:  0x20
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x0B2E5904  token=0x6000792  System.Void .ctor(System.Single value, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.MinFloatParameter
TYPE:  class
TOKEN: 0x2000150
SIZE:  0x28
EXTENDS: UnityEngine.Rendering.FloatParameter
FIELDS:
  public            System.Single                   min  // 0x20
PROPERTIES:
  value  get=0x04D86380  set=0x0B2E5590
METHODS:
  RVA=0x04DB9860  token=0x6000795  System.Void .ctor(System.Single value, System.Single min, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.NoInterpMinFloatParameter
TYPE:  class
TOKEN: 0x2000151
SIZE:  0x28
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
  public            System.Single                   min  // 0x20
PROPERTIES:
  value  get=0x04D86380  set=0x0B2E5590
METHODS:
  RVA=0x0B2E5AE0  token=0x6000798  System.Void .ctor(System.Single value, System.Single min, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.MaxFloatParameter
TYPE:  class
TOKEN: 0x2000152
SIZE:  0x28
EXTENDS: UnityEngine.Rendering.FloatParameter
FIELDS:
  public            System.Single                   max  // 0x20
PROPERTIES:
  value  get=0x04D86380  set=0x0B2E4610
METHODS:
  RVA=0x04DB9860  token=0x600079B  System.Void .ctor(System.Single value, System.Single max, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.NoInterpMaxFloatParameter
TYPE:  class
TOKEN: 0x2000153
SIZE:  0x28
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
  public            System.Single                   max  // 0x20
PROPERTIES:
  value  get=0x04D86380  set=0x0B2E4610
METHODS:
  RVA=0x0B2E5A24  token=0x600079E  System.Void .ctor(System.Single value, System.Single max, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.ClampedFloatParameter
TYPE:  class
TOKEN: 0x2000154
SIZE:  0x30
EXTENDS: UnityEngine.Rendering.FloatParameter
FIELDS:
  public            System.Single                   min  // 0x20
  public            System.Single                   max  // 0x24
  public            System.Single                   sliderExponent  // 0x28
PROPERTIES:
  value  get=0x04D86380  set=0x03EE0E50
METHODS:
  RVA=0x04DB9050  token=0x60007A1  System.Void .ctor(System.Single value, System.Single min, System.Single max, System.Single sliderExponent, System.Boolean overrideState)
  RVA=0x04DB9080  token=0x60007A2  System.Void .ctor(System.Single value, System.Single min, System.Single max, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.NoInterpClampedFloatParameter
TYPE:  class
TOKEN: 0x2000155
SIZE:  0x30
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
  public            System.Single                   min  // 0x20
  public            System.Single                   max  // 0x24
  public            System.Single                   sliderExponent  // 0x28
PROPERTIES:
  value  get=0x04D86380  set=0x0B2E56A4
METHODS:
  RVA=0x0B2E5624  token=0x60007A5  System.Void .ctor(System.Single value, System.Single min, System.Single max, System.Single sliderExponent, System.Boolean overrideState)
  RVA=0x0B2E55AC  token=0x60007A6  System.Void .ctor(System.Single value, System.Single min, System.Single max, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.UnclampedFloatParameter
TYPE:  class
TOKEN: 0x2000156
SIZE:  0x30
EXTENDS: UnityEngine.Rendering.FloatParameter
FIELDS:
  public            System.Single                   min  // 0x20
  public            System.Single                   max  // 0x24
  public            System.Single                   sliderExponent  // 0x28
PROPERTIES:
  value  get=0x04D86380  set=0x04D86390
METHODS:
  RVA=0x04DB9050  token=0x60007A9  System.Void .ctor(System.Single value, System.Single min, System.Single max, System.Single sliderExponent, System.Boolean overrideState)
  RVA=0x04DB9080  token=0x60007AA  System.Void .ctor(System.Single value, System.Single min, System.Single max, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.NoInterpUnclampedFloatParameter
TYPE:  class
TOKEN: 0x2000157
SIZE:  0x30
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
  public            System.Single                   min  // 0x20
  public            System.Single                   max  // 0x24
  public            System.Single                   sliderExponent  // 0x28
PROPERTIES:
  value  get=0x04D86380  set=0x04D86390
METHODS:
  RVA=0x0B2E5DC4  token=0x60007AD  System.Void .ctor(System.Single value, System.Single min, System.Single max, System.Single sliderExponent, System.Boolean overrideState)
  RVA=0x0B2E5E44  token=0x60007AE  System.Void .ctor(System.Single value, System.Single min, System.Single max, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.FloatRangeParameter
TYPE:  class
TOKEN: 0x2000158
SIZE:  0x28
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
  public            System.Single                   min  // 0x20
  public            System.Single                   max  // 0x24
PROPERTIES:
  value  get=0x04D92340  set=0x0B2E365C
METHODS:
  RVA=0x043D42E0  token=0x60007B1  System.Void .ctor(UnityEngine.Vector2 value, System.Single min, System.Single max, System.Boolean overrideState)
  RVA=0x04DB9790  token=0x60007B2  System.Void Interp(UnityEngine.Vector2 from, UnityEngine.Vector2 to, System.Single t)
END_CLASS

CLASS: UnityEngine.Rendering.NoInterpFloatRangeParameter
TYPE:  class
TOKEN: 0x2000159
SIZE:  0x28
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
  public            System.Single                   min  // 0x20
  public            System.Single                   max  // 0x24
PROPERTIES:
  value  get=0x04D92340  set=0x0B2E365C
METHODS:
  RVA=0x0B2E5960  token=0x60007B5  System.Void .ctor(UnityEngine.Vector2 value, System.Single min, System.Single max, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.ColorParameter
TYPE:  class
TOKEN: 0x200015A
SIZE:  0x30
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
  public            System.Boolean                  hdr  // 0x28
  public            System.Boolean                  showAlpha  // 0x29
  public            System.Boolean                  showEyeDropper  // 0x2a
METHODS:
  RVA=0x04DB9130  token=0x60007B6  System.Void .ctor(UnityEngine.Color value, System.Boolean overrideState)
  RVA=0x04DB9110  token=0x60007B7  System.Void .ctor(UnityEngine.Color value, System.Boolean hdr, System.Boolean showAlpha, System.Boolean showEyeDropper, System.Boolean overrideState)
  RVA=0x04DB90A0  token=0x60007B8  System.Void Interp(UnityEngine.Color from, UnityEngine.Color to, System.Single t)
END_CLASS

CLASS: UnityEngine.Rendering.NoInterpColorParameter
TYPE:  class
TOKEN: 0x200015B
SIZE:  0x30
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
  public            System.Boolean                  hdr  // 0x28
  public            System.Boolean                  showAlpha  // 0x29
  public            System.Boolean                  showEyeDropper  // 0x2a
METHODS:
  RVA=0x0B2E572C  token=0x60007B9  System.Void .ctor(UnityEngine.Color value, System.Boolean overrideState)
  RVA=0x0B2E5784  token=0x60007BA  System.Void .ctor(UnityEngine.Color value, System.Boolean hdr, System.Boolean showAlpha, System.Boolean showEyeDropper, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.Vector2Parameter
TYPE:  class
TOKEN: 0x200015C
SIZE:  0x20
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x04DA09D0  token=0x60007BB  System.Void .ctor(UnityEngine.Vector2 value, System.Boolean overrideState)
  RVA=0x04DB9790  token=0x60007BC  System.Void Interp(UnityEngine.Vector2 from, UnityEngine.Vector2 to, System.Single t)
END_CLASS

CLASS: UnityEngine.Rendering.NoInterpVector2Parameter
TYPE:  class
TOKEN: 0x200015D
SIZE:  0x20
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x0B2E5EBC  token=0x60007BD  System.Void .ctor(UnityEngine.Vector2 value, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.Vector3Parameter
TYPE:  class
TOKEN: 0x200015E
SIZE:  0x28
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x04416260  token=0x60007BE  System.Void .ctor(UnityEngine.Vector3 value, System.Boolean overrideState)
  RVA=0x04DB9880  token=0x60007BF  System.Void Interp(UnityEngine.Vector3 from, UnityEngine.Vector3 to, System.Single t)
END_CLASS

CLASS: UnityEngine.Rendering.NoInterpVector3Parameter
TYPE:  class
TOKEN: 0x200015F
SIZE:  0x28
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x0B2E5F18  token=0x60007C0  System.Void .ctor(UnityEngine.Vector3 value, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.Vector4Parameter
TYPE:  class
TOKEN: 0x2000160
SIZE:  0x28
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x04DA09E0  token=0x60007C1  System.Void .ctor(UnityEngine.Vector4 value, System.Boolean overrideState)
  RVA=0x04DB90A0  token=0x60007C2  System.Void Interp(UnityEngine.Vector4 from, UnityEngine.Vector4 to, System.Single t)
END_CLASS

CLASS: UnityEngine.Rendering.NoInterpVector4Parameter
TYPE:  class
TOKEN: 0x2000161
SIZE:  0x28
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x0B2E5F70  token=0x60007C3  System.Void .ctor(UnityEngine.Vector4 value, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.TextureParameter
TYPE:  class
TOKEN: 0x2000162
SIZE:  0x28
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
  public            UnityEngine.Rendering.TextureDimensiondimension  // 0x20
METHODS:
  RVA=0x05397BEC  token=0x60007C4  System.Void .ctor(UnityEngine.Texture value, System.Boolean overrideState)
  RVA=0x0402BF60  token=0x60007C5  System.Void .ctor(UnityEngine.Texture value, UnityEngine.Rendering.TextureDimension dimension, System.Boolean overrideState)
  RVA=0x0B2E65CC  token=0x60007C6  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.Rendering.NoInterpTextureParameter
TYPE:  class
TOKEN: 0x2000163
SIZE:  0x20
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x0B2E5D6C  token=0x60007C7  System.Void .ctor(UnityEngine.Texture value, System.Boolean overrideState)
  RVA=0x0B2E5CAC  token=0x60007C8  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.Rendering.Texture2DParameter
TYPE:  class
TOKEN: 0x2000164
SIZE:  0x20
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x0402D9F0  token=0x60007C9  System.Void .ctor(UnityEngine.Texture value, System.Boolean overrideState)
  RVA=0x0B2E62C8  token=0x60007CA  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.Rendering.Texture3DParameter
TYPE:  class
TOKEN: 0x2000165
SIZE:  0x20
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x0402C1F0  token=0x60007CB  System.Void .ctor(UnityEngine.Texture value, System.Boolean overrideState)
  RVA=0x0B2E6388  token=0x60007CC  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.Rendering.RenderTextureParameter
TYPE:  class
TOKEN: 0x2000166
SIZE:  0x20
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x0B2E60CC  token=0x60007CD  System.Void .ctor(UnityEngine.RenderTexture value, System.Boolean overrideState)
  RVA=0x0B2E6014  token=0x60007CE  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.Rendering.NoInterpRenderTextureParameter
TYPE:  class
TOKEN: 0x2000167
SIZE:  0x20
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x0B2E5C54  token=0x60007CF  System.Void .ctor(UnityEngine.RenderTexture value, System.Boolean overrideState)
  RVA=0x0B2E5B9C  token=0x60007D0  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.Rendering.CubemapParameter
TYPE:  class
TOKEN: 0x2000168
SIZE:  0x20
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x0402D990  token=0x60007D1  System.Void .ctor(UnityEngine.Texture value, System.Boolean overrideState)
  RVA=0x0B2E0D60  token=0x60007D2  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.Rendering.NoInterpCubemapParameter
TYPE:  class
TOKEN: 0x2000169
SIZE:  0x20
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x0B2E58AC  token=0x60007D3  System.Void .ctor(UnityEngine.Cubemap value, System.Boolean overrideState)
  RVA=0x0B2E57F4  token=0x60007D4  System.Int32 GetHashCode()
END_CLASS

CLASS: UnityEngine.Rendering.AnimationCurveParameter
TYPE:  class
TOKEN: 0x200016A
SIZE:  0x20
EXTENDS: UnityEngine.Rendering.VolumeParameter`1
FIELDS:
METHODS:
  RVA=0x043D4360  token=0x60007D5  System.Void .ctor(UnityEngine.AnimationCurve value, System.Boolean overrideState)
END_CLASS

CLASS: UnityEngine.Rendering.VolumeProfile
TYPE:  sealed class
TOKEN: 0x200016B
SIZE:  0x28
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  public            System.Collections.Generic.List<UnityEngine.Rendering.VolumeComponent>components  // 0x18
  public            System.Boolean                  isDirty  // 0x20
METHODS:
  RVA=0x047C4430  token=0x60007D6  System.Void OnEnable()
  RVA=0x0B2E74F0  token=0x60007D7  System.Void OnDisable()
  RVA=0x04D8F6A0  token=0x60007D8  System.Void Reset()
  RVA=-1  // generic def  token=0x60007D9  T Add(System.Boolean overrides)
  RVA=0x02FC4400  token=0x60007DA  UnityEngine.Rendering.VolumeComponent Add(System.Type type, System.Boolean overrides)
  RVA=-1  // generic def  token=0x60007DB  System.Void Remove()
  RVA=0x0B2E75C0  token=0x60007DC  System.Void Remove(System.Type type)
  RVA=-1  // generic def  token=0x60007DD  System.Boolean Has()
  RVA=0x02FC5040  token=0x60007DE  System.Boolean Has(System.Type type)
  RVA=0x0B2E73A4  token=0x60007DF  System.Boolean HasSubclassOf(System.Type type)
  RVA=-1  // generic def  token=0x60007E0  System.Boolean TryGet(T& component)
  RVA=-1  // generic def  token=0x60007E1  System.Boolean TryGet(System.Type type, T& component)
  RVA=-1  // generic def  token=0x60007E2  System.Boolean TryGetSubclassOf(System.Type type, T& component)
  RVA=-1  // generic def  token=0x60007E3  System.Boolean TryGetAllSubclassOf(System.Type type, System.Collections.Generic.List<T> result)
  RVA=0x0B2E7310  token=0x60007E4  System.Int32 GetHashCode()
  RVA=0x0B2E726C  token=0x60007E5  System.Int32 GetComponentListHashCode()
  RVA=0x0B2E769C  token=0x60007E6  System.Void Sanitize()
  RVA=0x046FE410  token=0x60007E7  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.VolumeStack
TYPE:  sealed class
TOKEN: 0x200016D
SIZE:  0x28
IMPLEMENTS: System.IDisposable
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<System.Type,UnityEngine.Rendering.VolumeComponent>components  // 0x10
  public            System.ValueTuple<UnityEngine.Rendering.VolumeParameter,UnityEngine.Rendering.VolumeParameter>[]defaultParameters  // 0x18
  private           System.Boolean                  requiresReset  // 0x20
METHODS:
  RVA=0x02FC4540  token=0x60007EB  System.Void .ctor()
  RVA=0x02FC4AB0  token=0x60007EC  System.Void Reload(System.Collections.Generic.List<UnityEngine.Rendering.VolumeComponent> componentDefaultStates)
  RVA=-1  // generic def  token=0x60007ED  T GetComponent()
  RVA=0x02FC5A60  token=0x60007EE  UnityEngine.Rendering.VolumeComponent GetComponent(System.Type type)
  RVA=0x037DCDB0  token=0x60007EF  System.Void Dispose()
END_CLASS

CLASS: UnityEngine.Rendering.XRGraphicsAutomatedTests
TYPE:  static class
TOKEN: 0x200016E
SIZE:  0x10
FIELDS:
  private   static readonly System.Boolean                  <enabled>k__BackingField  // static @ 0x0
  public    static  System.Boolean                  running  // static @ 0x1
PROPERTIES:
  activatedFromCommandLine  get=0x012081B0
  enabled  get=0x0B2E7A54
METHODS:
  RVA=0x0B2E7A0C  token=0x60007F2  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.LookDev.IDataProvider
TYPE:  interface
TOKEN: 0x200016F
FIELDS:
PROPERTIES:
  supportedDebugModes  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60007F3  System.Void FirstInitScene(UnityEngine.Rendering.LookDev.StageRuntimeInterface stage)
  RVA=-1  // abstract  token=0x60007F4  System.Void UpdateSky(UnityEngine.Camera camera, UnityEngine.Rendering.LookDev.Sky sky, UnityEngine.Rendering.LookDev.StageRuntimeInterface stage)
  RVA=-1  // abstract  token=0x60007F6  System.Void UpdateDebugMode(System.Int32 debugIndex)
  RVA=-1  // abstract  token=0x60007F7  System.Void GetShadowMask(UnityEngine.RenderTexture& output, UnityEngine.Rendering.LookDev.StageRuntimeInterface stage)
  RVA=-1  // abstract  token=0x60007F8  System.Void OnBeginRendering(UnityEngine.Rendering.LookDev.StageRuntimeInterface stage)
  RVA=-1  // abstract  token=0x60007F9  System.Void OnEndRendering(UnityEngine.Rendering.LookDev.StageRuntimeInterface stage)
  RVA=-1  // abstract  token=0x60007FA  System.Void Cleanup(UnityEngine.Rendering.LookDev.StageRuntimeInterface SRI)
END_CLASS

CLASS: UnityEngine.Rendering.LookDev.Sky
TYPE:  sealed struct
TOKEN: 0x2000170
SIZE:  0x20
FIELDS:
  public            UnityEngine.Cubemap             cubemap  // 0x10
  public            System.Single                   longitudeOffset  // 0x18
  public            System.Single                   exposure  // 0x1c
METHODS:
END_CLASS

CLASS: UnityEngine.Rendering.LookDev.StageRuntimeInterface
TYPE:  class
TOKEN: 0x2000171
SIZE:  0x30
FIELDS:
  private           System.Func<System.Boolean,UnityEngine.GameObject>m_AddGameObject  // 0x10
  private           System.Func<UnityEngine.Camera> m_GetCamera  // 0x18
  private           System.Func<UnityEngine.Light>  m_GetSunLight  // 0x20
  public            System.Object                   SRPData  // 0x28
PROPERTIES:
  camera  get=0x0B2E6288
  sunLight  get=0x0B2E62A8
METHODS:
  RVA=0x031D14F0  token=0x60007FB  System.Void .ctor(System.Func<System.Boolean,UnityEngine.GameObject> AddGameObject, System.Func<UnityEngine.Camera> GetCamera, System.Func<UnityEngine.Light> GetSunLight)
  RVA=0x0B2E6264  token=0x60007FC  UnityEngine.GameObject AddGameObject(System.Boolean persistent)
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerBitField
TYPE:  class
TOKEN: 0x2000172
SIZE:  0x80
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
FIELDS:
  public            UnityEngine.UI.Text             nameLabel  // 0x58
  public            UnityEngine.Rendering.UI.UIFoldoutvalueToggle  // 0x60
  public            System.Collections.Generic.List<UnityEngine.Rendering.UI.DebugUIHandlerIndirectToggle>toggles  // 0x68
  private           UnityEngine.Rendering.DebugUI.BitFieldm_Field  // 0x70
  private           UnityEngine.Rendering.UI.DebugUIHandlerContainerm_Container  // 0x78
METHODS:
  RVA=0x0B2E13C0  token=0x60007FF  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x0B2E0E20  token=0x6000800  System.Boolean GetValue(System.Int32 index)
  RVA=0x0B2E10F0  token=0x6000801  System.Void SetValue(System.Int32 index, System.Boolean value)
  RVA=0x0B2E1014  token=0x6000802  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x0B2E0FBC  token=0x6000803  System.Void OnDeselection()
  RVA=0x0B2E0FEC  token=0x6000804  System.Void OnIncrement(System.Boolean fast)
  RVA=0x0B2E0F98  token=0x6000805  System.Void OnDecrement(System.Boolean fast)
  RVA=0x0B2E0F68  token=0x6000806  System.Void OnAction()
  RVA=0x0B2E0EBC  token=0x6000807  UnityEngine.Rendering.UI.DebugUIHandlerWidget Next()
  RVA=0x0B2E170C  token=0x6000808  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerButton
TYPE:  class
TOKEN: 0x2000173
SIZE:  0x68
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
FIELDS:
  public            UnityEngine.UI.Text             nameLabel  // 0x58
  private           UnityEngine.Rendering.DebugUI.Buttonm_Field  // 0x60
METHODS:
  RVA=0x0B2E1774  token=0x6000809  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x0B2E1740  token=0x600080A  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x0B2E0FBC  token=0x600080B  System.Void OnDeselection()
  RVA=0x0B2E1714  token=0x600080C  System.Void OnAction()
  RVA=0x0B2E170C  token=0x600080D  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIPrefabBundle
TYPE:  class
TOKEN: 0x2000174
SIZE:  0x20
FIELDS:
  public            System.String                   type  // 0x10
  public            UnityEngine.RectTransform       prefab  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600080E  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerCanvas
TYPE:  class
TOKEN: 0x2000175
SIZE:  0x58
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           System.Int32                    m_DebugTreeState  // 0x18
  private           System.Collections.Generic.Dictionary<System.Type,UnityEngine.Transform>m_PrefabsMap  // 0x20
  public            UnityEngine.Transform           panelPrefab  // 0x28
  public            System.Collections.Generic.List<UnityEngine.Rendering.UI.DebugUIPrefabBundle>prefabs  // 0x30
  private           System.Collections.Generic.List<UnityEngine.Rendering.UI.DebugUIHandlerPanel>m_UIPanels  // 0x38
  private           System.Int32                    m_SelectedPanel  // 0x40
  private           UnityEngine.Rendering.UI.DebugUIHandlerWidgetm_SelectedWidget  // 0x48
  private           System.String                   m_CurrentQueryPath  // 0x50
METHODS:
  RVA=0x0B2E2020  token=0x600080F  System.Void OnEnable()
  RVA=0x0B2E3114  token=0x6000810  System.Void Update()
  RVA=0x04D87910  token=0x6000811  System.Void RequestHierarchyReset()
  RVA=0x0B2E288C  token=0x6000812  System.Void ResetAllHierarchy()
  RVA=0x0B2E2170  token=0x6000813  System.Void Rebuild()
  RVA=0x0B2E2D40  token=0x6000814  System.Void Traverse(UnityEngine.Rendering.DebugUI.IContainer container, UnityEngine.Transform parentTransform, UnityEngine.Rendering.UI.DebugUIHandlerWidget parentUIHandler, UnityEngine.Rendering.UI.DebugUIHandlerWidget& selectedHandler)
  RVA=0x0B2E1CE0  token=0x6000815  UnityEngine.Rendering.UI.DebugUIHandlerWidget GetWidgetFromPath(System.String queryPath)
  RVA=0x0B2E17F8  token=0x6000816  System.Void ActivatePanel(System.Int32 index, UnityEngine.Rendering.UI.DebugUIHandlerWidget selectedWidget)
  RVA=0x0B2E1B54  token=0x6000817  System.Void ChangeSelection(UnityEngine.Rendering.UI.DebugUIHandlerWidget widget, System.Boolean fromNext)
  RVA=0x0B2E2B64  token=0x6000818  System.Void SelectPreviousItem()
  RVA=0x0B2E2AF8  token=0x6000819  System.Void SelectNextPanel()
  RVA=0x0B2E2C00  token=0x600081A  System.Void SelectPreviousPanel()
  RVA=0x0B2E2A44  token=0x600081B  System.Void SelectNextItem()
  RVA=0x0B2E1A70  token=0x600081C  System.Void ChangeSelectionValue(System.Single multiplier)
  RVA=0x0B2E1A00  token=0x600081D  System.Void ActivateSelection()
  RVA=0x0B2E1E0C  token=0x600081E  System.Void HandleInput()
  RVA=0x0B2E2C70  token=0x600081F  System.Void SetScrollTarget(UnityEngine.Rendering.UI.DebugUIHandlerWidget widget)
  RVA=0x05393520  token=0x6000820  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerColor
TYPE:  class
TOKEN: 0x2000178
SIZE:  0xA0
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
FIELDS:
  public            UnityEngine.UI.Text             nameLabel  // 0x58
  public            UnityEngine.Rendering.UI.UIFoldoutvalueToggle  // 0x60
  public            UnityEngine.UI.Image            colorImage  // 0x68
  public            UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatFieldfieldR  // 0x70
  public            UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatFieldfieldG  // 0x78
  public            UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatFieldfieldB  // 0x80
  public            UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatFieldfieldA  // 0x88
  private           UnityEngine.Rendering.DebugUI.ColorFieldm_Field  // 0x90
  private           UnityEngine.Rendering.UI.DebugUIHandlerContainerm_Container  // 0x98
METHODS:
  RVA=0x0B2E7DE8  token=0x6000827  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x0B2E7CD4  token=0x6000828  System.Void SetValue(System.Single x, System.Boolean r, System.Boolean g, System.Boolean b, System.Boolean a)
  RVA=0x0B2E82D4  token=0x6000829  System.Void SetupSettings(UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField field)
  RVA=0x0B2E7BF0  token=0x600082A  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x0B2E0FBC  token=0x600082B  System.Void OnDeselection()
  RVA=0x0B2E0FEC  token=0x600082C  System.Void OnIncrement(System.Boolean fast)
  RVA=0x0B2E0F98  token=0x600082D  System.Void OnDecrement(System.Boolean fast)
  RVA=0x0B2E0F68  token=0x600082E  System.Void OnAction()
  RVA=0x0B2E8664  token=0x600082F  System.Void UpdateColor()
  RVA=0x0B2E7B3C  token=0x6000830  UnityEngine.Rendering.UI.DebugUIHandlerWidget Next()
  RVA=0x0B2E170C  token=0x6000831  System.Void .ctor()
  RVA=0x0B2E8410  token=0x6000832  System.Single <SetWidget>b__9_0()
  RVA=0x0B2E8464  token=0x6000833  System.Void <SetWidget>b__9_1(System.Single x)
  RVA=0x0B2E848C  token=0x6000834  System.Single <SetWidget>b__9_2()
  RVA=0x0B2E84E0  token=0x6000835  System.Void <SetWidget>b__9_3(System.Single x)
  RVA=0x0B2E8508  token=0x6000836  System.Single <SetWidget>b__9_4()
  RVA=0x0B2E855C  token=0x6000837  System.Void <SetWidget>b__9_5(System.Single x)
  RVA=0x0B2E8584  token=0x6000838  System.Single <SetWidget>b__9_6()
  RVA=0x0B2E85D8  token=0x6000839  System.Void <SetWidget>b__9_7(System.Single x)
  RVA=0x0B2E8600  token=0x600083A  System.Single <SetupSettings>b__11_0()
  RVA=0x0B2E8620  token=0x600083B  System.Single <SetupSettings>b__11_1()
  RVA=0x0B2E8640  token=0x600083C  System.Single <SetupSettings>b__11_2()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerContainer
TYPE:  class
TOKEN: 0x2000179
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.RectTransform       contentHolder  // 0x18
METHODS:
  RVA=0x0B2E8964  token=0x600083D  UnityEngine.Rendering.UI.DebugUIHandlerWidget GetFirstItem()
  RVA=0x0B2E89E4  token=0x600083E  UnityEngine.Rendering.UI.DebugUIHandlerWidget GetLastItem()
  RVA=0x0B2E8A68  token=0x600083F  System.Boolean IsDirectChild(UnityEngine.Rendering.UI.DebugUIHandlerWidget widget)
  RVA=0x0B2E8710  token=0x6000840  System.Collections.Generic.List<UnityEngine.Rendering.UI.DebugUIHandlerWidget> GetActiveChildren()
  RVA=0x05393520  token=0x6000841  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerEnumField
TYPE:  class
TOKEN: 0x200017B
SIZE:  0x80
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
FIELDS:
  public            UnityEngine.UI.Text             nextButtonText  // 0x58
  public            UnityEngine.UI.Text             previousButtonText  // 0x60
  public            UnityEngine.UI.Text             nameLabel  // 0x68
  public            UnityEngine.UI.Text             valueLabel  // 0x70
  protected internal        UnityEngine.Rendering.DebugUI.EnumFieldm_Field  // 0x78
METHODS:
  RVA=0x0B2E906C  token=0x6000844  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x0B2E8F60  token=0x6000845  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x0B2E8D0C  token=0x6000846  System.Void OnDeselection()
  RVA=0x0B2E8B60  token=0x6000847  System.Void OnAction()
  RVA=0x0B2E8E14  token=0x6000848  System.Void OnIncrement(System.Boolean fast)
  RVA=0x0B2E8B70  token=0x6000849  System.Void OnDecrement(System.Boolean fast)
  RVA=0x0B2E90FC  token=0x600084A  System.Void UpdateValueLabel()
  RVA=0x0B2E170C  token=0x600084B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerEnumHistory
TYPE:  class
TOKEN: 0x200017C
SIZE:  0x88
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerEnumField
FIELDS:
  private           UnityEngine.UI.Text[]           historyValues  // 0x80
  private   static  System.Single                   xDecal  // const
METHODS:
  RVA=0x0B2E9214  token=0x600084C  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x0B2E9448  token=0x600084D  System.Void UpdateValueLabel()
  RVA=0x0B2E91B8  token=0x600084E  System.Collections.IEnumerator RefreshAfterSanitization()
  RVA=0x0B2E170C  token=0x600084F  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerFloatField
TYPE:  class
TOKEN: 0x200017E
SIZE:  0x70
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
FIELDS:
  public            UnityEngine.UI.Text             nameLabel  // 0x58
  public            UnityEngine.UI.Text             valueLabel  // 0x60
  private           UnityEngine.Rendering.DebugUI.FloatFieldm_Field  // 0x68
METHODS:
  RVA=0x0B2E97AC  token=0x6000856  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x0B2E9750  token=0x6000857  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x0B2E96E4  token=0x6000858  System.Void OnDeselection()
  RVA=0x0B2E9740  token=0x6000859  System.Void OnIncrement(System.Boolean fast)
  RVA=0x0B2E96D4  token=0x600085A  System.Void OnDecrement(System.Boolean fast)
  RVA=0x0B2E9620  token=0x600085B  System.Void ChangeValue(System.Boolean fast, System.Single multiplier)
  RVA=0x0B2E983C  token=0x600085C  System.Void UpdateValueLabel()
  RVA=0x0B2E170C  token=0x600085D  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerFoldout
TYPE:  class
TOKEN: 0x200017F
SIZE:  0x78
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
FIELDS:
  public            UnityEngine.UI.Text             nameLabel  // 0x58
  public            UnityEngine.Rendering.UI.UIFoldoutvalueToggle  // 0x60
  private           UnityEngine.Rendering.DebugUI.Foldoutm_Field  // 0x68
  private           UnityEngine.Rendering.UI.DebugUIHandlerContainerm_Container  // 0x70
  private   static  System.Single                   xDecal  // const
  private   static  System.Single                   xDecalInit  // const
METHODS:
  RVA=0x0B2E9AE8  token=0x600085E  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x0B2E9A0C  token=0x600085F  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x0B2E0FBC  token=0x6000860  System.Void OnDeselection()
  RVA=0x0B2E99E8  token=0x6000861  System.Void OnIncrement(System.Boolean fast)
  RVA=0x0B2E99C4  token=0x6000862  System.Void OnDecrement(System.Boolean fast)
  RVA=0x0B2E9998  token=0x6000863  System.Void OnAction()
  RVA=0x0B2E9EC4  token=0x6000864  System.Void UpdateValue()
  RVA=0x0B2E98EC  token=0x6000865  UnityEngine.Rendering.UI.DebugUIHandlerWidget Next()
  RVA=0x0B2E170C  token=0x6000866  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerGroup
TYPE:  class
TOKEN: 0x2000180
SIZE:  0x78
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
FIELDS:
  public            UnityEngine.UI.Text             nameLabel  // 0x58
  public            UnityEngine.Transform           header  // 0x60
  private           UnityEngine.Rendering.DebugUI.Containerm_Field  // 0x68
  private           UnityEngine.Rendering.UI.DebugUIHandlerContainerm_Container  // 0x70
METHODS:
  RVA=0x0B2EA040  token=0x6000867  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x0B2E9F94  token=0x6000868  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x0B2E9EF8  token=0x6000869  UnityEngine.Rendering.UI.DebugUIHandlerWidget Next()
  RVA=0x0B2E170C  token=0x600086A  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerHBox
TYPE:  class
TOKEN: 0x2000181
SIZE:  0x60
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
FIELDS:
  private           UnityEngine.Rendering.UI.DebugUIHandlerContainerm_Container  // 0x58
METHODS:
  RVA=0x0B2EA26C  token=0x600086B  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x0B2EA1C0  token=0x600086C  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x0B2EA124  token=0x600086D  UnityEngine.Rendering.UI.DebugUIHandlerWidget Next()
  RVA=0x0B2E170C  token=0x600086E  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField
TYPE:  class
TOKEN: 0x2000182
SIZE:  0x90
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
FIELDS:
  public            UnityEngine.UI.Text             nameLabel  // 0x58
  public            UnityEngine.UI.Text             valueLabel  // 0x60
  public            System.Func<System.Single>      getter  // 0x68
  public            System.Action<System.Single>    setter  // 0x70
  public            System.Func<System.Single>      incStepGetter  // 0x78
  public            System.Func<System.Single>      incStepMultGetter  // 0x80
  public            System.Func<System.Single>      decimalsGetter  // 0x88
METHODS:
  RVA=0x0B2EA388  token=0x600086F  System.Void Init()
  RVA=0x0B2E9750  token=0x6000870  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x0B2E96E4  token=0x6000871  System.Void OnDeselection()
  RVA=0x0B2EA3A0  token=0x6000872  System.Void OnIncrement(System.Boolean fast)
  RVA=0x0B2EA390  token=0x6000873  System.Void OnDecrement(System.Boolean fast)
  RVA=0x0B2EA2CC  token=0x6000874  System.Void ChangeValue(System.Boolean fast, System.Single multiplier)
  RVA=0x0B2EA3B0  token=0x6000875  System.Void UpdateValueLabel()
  RVA=0x0B2E170C  token=0x6000876  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerIndirectToggle
TYPE:  class
TOKEN: 0x2000183
SIZE:  0x88
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
FIELDS:
  public            UnityEngine.UI.Text             nameLabel  // 0x58
  public            UnityEngine.UI.Toggle           valueToggle  // 0x60
  public            UnityEngine.UI.Image            checkmarkImage  // 0x68
  public            System.Func<System.Int32,System.Boolean>getter  // 0x70
  public            System.Action<System.Int32,System.Boolean>setter  // 0x78
  private           System.Int32                    index  // 0x80
METHODS:
  RVA=0x0B2EA48C  token=0x6000877  System.Void Init()
  RVA=0x0B2EA650  token=0x6000878  System.Void OnToggleValueChanged(System.Boolean value)
  RVA=0x0B2EA5F4  token=0x6000879  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x0B2EA598  token=0x600087A  System.Void OnDeselection()
  RVA=0x0B2EA544  token=0x600087B  System.Void OnAction()
  RVA=0x0B2EA680  token=0x600087C  System.Void UpdateValueLabel()
  RVA=0x0B2E170C  token=0x600087D  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerIntField
TYPE:  class
TOKEN: 0x2000184
SIZE:  0x70
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
FIELDS:
  public            UnityEngine.UI.Text             nameLabel  // 0x58
  public            UnityEngine.UI.Text             valueLabel  // 0x60
  private           UnityEngine.Rendering.DebugUI.IntFieldm_Field  // 0x68
METHODS:
  RVA=0x0B2EA7E0  token=0x600087E  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x0B2E9750  token=0x600087F  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x0B2E96E4  token=0x6000880  System.Void OnDeselection()
  RVA=0x0B2EA7D0  token=0x6000881  System.Void OnIncrement(System.Boolean fast)
  RVA=0x0B2EA7C0  token=0x6000882  System.Void OnDecrement(System.Boolean fast)
  RVA=0x0B2EA70C  token=0x6000883  System.Void ChangeValue(System.Boolean fast, System.Int32 multiplier)
  RVA=0x0B2EA870  token=0x6000884  System.Void UpdateValueLabel()
  RVA=0x0B2E170C  token=0x6000885  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerMessageBox
TYPE:  class
TOKEN: 0x2000185
SIZE:  0x68
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
FIELDS:
  public            UnityEngine.UI.Text             nameLabel  // 0x58
  private           UnityEngine.Rendering.DebugUI.MessageBoxm_Field  // 0x60
  private   static  UnityEngine.Color32             k_WarningBackgroundColor  // static @ 0x0
  private   static  UnityEngine.Color32             k_WarningTextColor  // static @ 0x4
  private   static  UnityEngine.Color32             k_ErrorBackgroundColor  // static @ 0x8
  private   static  UnityEngine.Color32             k_ErrorTextColor  // static @ 0xc
METHODS:
  RVA=0x0B2EA928  token=0x6000886  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x012081B0  token=0x6000887  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x0B2E170C  token=0x6000888  System.Void .ctor()
  RVA=0x0B2EAA58  token=0x6000889  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerPanel
TYPE:  class
TOKEN: 0x2000186
SIZE:  0x60
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.UI.Text             nameLabel  // 0x18
  public            UnityEngine.UI.ScrollRect       scrollRect  // 0x20
  public            UnityEngine.RectTransform       viewport  // 0x28
  public            UnityEngine.Rendering.UI.DebugUIHandlerCanvasCanvas  // 0x30
  private           UnityEngine.RectTransform       m_ScrollTransform  // 0x38
  private           UnityEngine.RectTransform       m_ContentTransform  // 0x40
  private           UnityEngine.RectTransform       m_MaskTransform  // 0x48
  private           UnityEngine.Rendering.UI.DebugUIHandlerWidgetm_ScrollTarget  // 0x50
  protected internal        UnityEngine.Rendering.DebugUI.Panelm_Panel  // 0x58
METHODS:
  RVA=0x0B2EACA4  token=0x600088A  System.Void OnEnable()
  RVA=0x0B2EAE3C  token=0x600088B  System.Void SetPanel(UnityEngine.Rendering.DebugUI.Panel panel)
  RVA=0x02E56440  token=0x600088C  UnityEngine.Rendering.DebugUI.Panel GetPanel()
  RVA=0x0B2EADFC  token=0x600088D  System.Void SelectNextItem()
  RVA=0x0B2EAE1C  token=0x600088E  System.Void SelectPreviousItem()
  RVA=0x0B2EAD60  token=0x600088F  System.Void OnScrollbarClicked()
  RVA=0x02FDAEA0  token=0x6000890  System.Void SetScrollTarget(UnityEngine.Rendering.UI.DebugUIHandlerWidget target)
  RVA=0x0B2EAE78  token=0x6000891  System.Void UpdateScroll()
  RVA=0x0B2EAB40  token=0x6000892  System.Single GetYPosInScroll(UnityEngine.RectTransform target)
  RVA=0x0B2EAAF0  token=0x6000893  UnityEngine.Rendering.UI.DebugUIHandlerWidget GetFirstItem()
  RVA=0x0B2EADB0  token=0x6000894  System.Void ResetDebugManager()
  RVA=0x05393520  token=0x6000895  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerPersistentCanvas
TYPE:  class
TOKEN: 0x2000187
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.RectTransform       panel  // 0x18
  public            UnityEngine.RectTransform       valuePrefab  // 0x20
  private           System.Collections.Generic.List<UnityEngine.Rendering.UI.DebugUIHandlerValue>m_Items  // 0x28
METHODS:
  RVA=0x0B2EB194  token=0x6000896  System.Void Toggle(UnityEngine.Rendering.DebugUI.Value widget)
  RVA=0x0B2EB028  token=0x6000897  System.Void Clear()
  RVA=0x0B2EB3D8  token=0x6000898  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerRow
TYPE:  class
TOKEN: 0x2000189
SIZE:  0x80
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerFoldout
FIELDS:
  private           System.Single                   m_Timer  // 0x78
METHODS:
  RVA=0x04DB98E0  token=0x600089B  System.Void OnEnable()
  RVA=0x0B2EB458  token=0x600089C  System.Void Update()
  RVA=0x0B2E170C  token=0x600089D  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerToggle
TYPE:  class
TOKEN: 0x200018A
SIZE:  0x78
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
FIELDS:
  public            UnityEngine.UI.Text             nameLabel  // 0x58
  public            UnityEngine.UI.Toggle           valueToggle  // 0x60
  public            UnityEngine.UI.Image            checkmarkImage  // 0x68
  protected internal        UnityEngine.Rendering.DebugUI.BoolFieldm_Field  // 0x70
METHODS:
  RVA=0x0B2EBDC0  token=0x600089E  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x0B2EBD6C  token=0x600089F  System.Void OnToggleValueChanged(System.Boolean value)
  RVA=0x0B2EA5F4  token=0x60008A0  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x0B2EA598  token=0x60008A1  System.Void OnDeselection()
  RVA=0x0B2EBCF4  token=0x60008A2  System.Void OnAction()
  RVA=0x0B2EBED0  token=0x60008A3  System.Void UpdateValueLabel()
  RVA=0x0B2E170C  token=0x60008A4  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerToggleHistory
TYPE:  class
TOKEN: 0x200018B
SIZE:  0x80
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerToggle
FIELDS:
  private           UnityEngine.UI.Toggle[]         historyToggles  // 0x78
  private   static  System.Single                   xDecal  // const
METHODS:
  RVA=0x0B2EB874  token=0x60008A5  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x0B2EBBAC  token=0x60008A6  System.Void UpdateValueLabel()
  RVA=0x0B2EB818  token=0x60008A7  System.Collections.IEnumerator RefreshAfterSanitization()
  RVA=0x0B2E170C  token=0x60008A8  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerUIntField
TYPE:  class
TOKEN: 0x200018D
SIZE:  0x70
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
FIELDS:
  public            UnityEngine.UI.Text             nameLabel  // 0x58
  public            UnityEngine.UI.Text             valueLabel  // 0x60
  private           UnityEngine.Rendering.DebugUI.UIntFieldm_Field  // 0x68
METHODS:
  RVA=0x0B2EC048  token=0x60008AF  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x0B2E9750  token=0x60008B0  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x0B2E96E4  token=0x60008B1  System.Void OnDeselection()
  RVA=0x0B2EC038  token=0x60008B2  System.Void OnIncrement(System.Boolean fast)
  RVA=0x0B2EC028  token=0x60008B3  System.Void OnDecrement(System.Boolean fast)
  RVA=0x0B2EBF68  token=0x60008B4  System.Void ChangeValue(System.Boolean fast, System.Int32 multiplier)
  RVA=0x0B2EC0D8  token=0x60008B5  System.Void UpdateValueLabel()
  RVA=0x0B2E170C  token=0x60008B6  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerValue
TYPE:  class
TOKEN: 0x200018E
SIZE:  0x78
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
FIELDS:
  public            UnityEngine.UI.Text             nameLabel  // 0x58
  public            UnityEngine.UI.Text             valueLabel  // 0x60
  private           UnityEngine.Rendering.DebugUI.Valuem_Field  // 0x68
  private           System.Single                   m_Timer  // 0x70
METHODS:
  RVA=0x04DB98F0  token=0x60008B7  System.Void OnEnable()
  RVA=0x0B2EC338  token=0x60008B8  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x0B2E9750  token=0x60008B9  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x0B2E96E4  token=0x60008BA  System.Void OnDeselection()
  RVA=0x0B2EC3BC  token=0x60008BB  System.Void Update()
  RVA=0x0B2E170C  token=0x60008BC  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerVBox
TYPE:  class
TOKEN: 0x200018F
SIZE:  0x60
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
FIELDS:
  private           UnityEngine.Rendering.UI.DebugUIHandlerContainerm_Container  // 0x58
METHODS:
  RVA=0x0B2EC2D8  token=0x60008BD  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x0B2EC22C  token=0x60008BE  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x0B2EC190  token=0x60008BF  UnityEngine.Rendering.UI.DebugUIHandlerWidget Next()
  RVA=0x0B2E170C  token=0x60008C0  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerVector2
TYPE:  class
TOKEN: 0x2000190
SIZE:  0x88
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
FIELDS:
  public            UnityEngine.UI.Text             nameLabel  // 0x58
  public            UnityEngine.Rendering.UI.UIFoldoutvalueToggle  // 0x60
  public            UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatFieldfieldX  // 0x68
  public            UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatFieldfieldY  // 0x70
  private           UnityEngine.Rendering.DebugUI.Vector2Fieldm_Field  // 0x78
  private           UnityEngine.Rendering.UI.DebugUIHandlerContainerm_Container  // 0x80
METHODS:
  RVA=0x0B2EC6A8  token=0x60008C1  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x0B2EC5F0  token=0x60008C2  System.Void SetValue(System.Single v, System.Boolean x, System.Boolean y)
  RVA=0x0B2EC920  token=0x60008C3  System.Void SetupSettings(UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField field)
  RVA=0x0B2EC50C  token=0x60008C4  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x0B2E0FBC  token=0x60008C5  System.Void OnDeselection()
  RVA=0x0B2E0FEC  token=0x60008C6  System.Void OnIncrement(System.Boolean fast)
  RVA=0x0B2E0F98  token=0x60008C7  System.Void OnDecrement(System.Boolean fast)
  RVA=0x0B2E0F68  token=0x60008C8  System.Void OnAction()
  RVA=0x0B2EC458  token=0x60008C9  UnityEngine.Rendering.UI.DebugUIHandlerWidget Next()
  RVA=0x0B2E170C  token=0x60008CA  System.Void .ctor()
  RVA=0x0B2ECA5C  token=0x60008CB  System.Single <SetWidget>b__6_0()
  RVA=0x0B2ECAB0  token=0x60008CC  System.Void <SetWidget>b__6_1(System.Single x)
  RVA=0x0B2ECAD0  token=0x60008CD  System.Single <SetWidget>b__6_2()
  RVA=0x0B2ECB24  token=0x60008CE  System.Void <SetWidget>b__6_3(System.Single x)
  RVA=0x0B2ECB44  token=0x60008CF  System.Single <SetupSettings>b__8_0()
  RVA=0x0B2ECB64  token=0x60008D0  System.Single <SetupSettings>b__8_1()
  RVA=0x0B2ECB84  token=0x60008D1  System.Single <SetupSettings>b__8_2()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerVector3
TYPE:  class
TOKEN: 0x2000191
SIZE:  0x90
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
FIELDS:
  public            UnityEngine.UI.Text             nameLabel  // 0x58
  public            UnityEngine.Rendering.UI.UIFoldoutvalueToggle  // 0x60
  public            UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatFieldfieldX  // 0x68
  public            UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatFieldfieldY  // 0x70
  public            UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatFieldfieldZ  // 0x78
  private           UnityEngine.Rendering.DebugUI.Vector3Fieldm_Field  // 0x80
  private           UnityEngine.Rendering.UI.DebugUIHandlerContainerm_Container  // 0x88
METHODS:
  RVA=0x0B2ECE14  token=0x60008D2  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x0B2ECD3C  token=0x60008D3  System.Void SetValue(System.Single v, System.Boolean x, System.Boolean y, System.Boolean z)
  RVA=0x0B2ED184  token=0x60008D4  System.Void SetupSettings(UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField field)
  RVA=0x0B2ECC58  token=0x60008D5  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x0B2E0FBC  token=0x60008D6  System.Void OnDeselection()
  RVA=0x0B2E0FEC  token=0x60008D7  System.Void OnIncrement(System.Boolean fast)
  RVA=0x0B2E0F98  token=0x60008D8  System.Void OnDecrement(System.Boolean fast)
  RVA=0x0B2E0F68  token=0x60008D9  System.Void OnAction()
  RVA=0x0B2ECBA4  token=0x60008DA  UnityEngine.Rendering.UI.DebugUIHandlerWidget Next()
  RVA=0x0B2E170C  token=0x60008DB  System.Void .ctor()
  RVA=0x0B2ED2C0  token=0x60008DC  System.Single <SetWidget>b__7_0()
  RVA=0x0B2ED314  token=0x60008DD  System.Void <SetWidget>b__7_1(System.Single v)
  RVA=0x0B2ED338  token=0x60008DE  System.Single <SetWidget>b__7_2()
  RVA=0x0B2ED38C  token=0x60008DF  System.Void <SetWidget>b__7_3(System.Single v)
  RVA=0x0B2ED3B0  token=0x60008E0  System.Single <SetWidget>b__7_4()
  RVA=0x0B2ED404  token=0x60008E1  System.Void <SetWidget>b__7_5(System.Single v)
  RVA=0x0B2ED428  token=0x60008E2  System.Single <SetupSettings>b__9_0()
  RVA=0x0B2ED448  token=0x60008E3  System.Single <SetupSettings>b__9_1()
  RVA=0x0B2ED468  token=0x60008E4  System.Single <SetupSettings>b__9_2()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerVector4
TYPE:  class
TOKEN: 0x2000192
SIZE:  0x98
EXTENDS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
FIELDS:
  public            UnityEngine.UI.Text             nameLabel  // 0x58
  public            UnityEngine.Rendering.UI.UIFoldoutvalueToggle  // 0x60
  public            UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatFieldfieldX  // 0x68
  public            UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatFieldfieldY  // 0x70
  public            UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatFieldfieldZ  // 0x78
  public            UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatFieldfieldW  // 0x80
  private           UnityEngine.Rendering.DebugUI.Vector4Fieldm_Field  // 0x88
  private           UnityEngine.Rendering.UI.DebugUIHandlerContainerm_Container  // 0x90
METHODS:
  RVA=0x0B2ED72C  token=0x60008E5  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x0B2ED624  token=0x60008E6  System.Void SetValue(System.Single v, System.Boolean x, System.Boolean y, System.Boolean z, System.Boolean w)
  RVA=0x0B2EDBA0  token=0x60008E7  System.Void SetupSettings(UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField field)
  RVA=0x0B2ED540  token=0x60008E8  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x0B2E0FBC  token=0x60008E9  System.Void OnDeselection()
  RVA=0x0B2E0FEC  token=0x60008EA  System.Void OnIncrement(System.Boolean fast)
  RVA=0x0B2E0F98  token=0x60008EB  System.Void OnDecrement(System.Boolean fast)
  RVA=0x0B2E0F68  token=0x60008EC  System.Void OnAction()
  RVA=0x0B2ED48C  token=0x60008ED  UnityEngine.Rendering.UI.DebugUIHandlerWidget Next()
  RVA=0x0B2E170C  token=0x60008EE  System.Void .ctor()
  RVA=0x0B2EDCDC  token=0x60008EF  System.Single <SetWidget>b__8_0()
  RVA=0x0B2EDD30  token=0x60008F0  System.Void <SetWidget>b__8_1(System.Single x)
  RVA=0x0B2EDD58  token=0x60008F1  System.Single <SetWidget>b__8_2()
  RVA=0x0B2EDDAC  token=0x60008F2  System.Void <SetWidget>b__8_3(System.Single x)
  RVA=0x0B2EDDD4  token=0x60008F3  System.Single <SetWidget>b__8_4()
  RVA=0x0B2EDE28  token=0x60008F4  System.Void <SetWidget>b__8_5(System.Single x)
  RVA=0x0B2EDE50  token=0x60008F5  System.Single <SetWidget>b__8_6()
  RVA=0x0B2EDEA4  token=0x60008F6  System.Void <SetWidget>b__8_7(System.Single x)
  RVA=0x0B2EDECC  token=0x60008F7  System.Single <SetupSettings>b__10_0()
  RVA=0x0B2EDEEC  token=0x60008F8  System.Single <SetupSettings>b__10_1()
  RVA=0x0B2EDF0C  token=0x60008F9  System.Single <SetupSettings>b__10_2()
END_CLASS

CLASS: UnityEngine.Rendering.UI.DebugUIHandlerWidget
TYPE:  class
TOKEN: 0x2000193
SIZE:  0x58
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.Color               colorDefault  // 0x18
  public            UnityEngine.Color               colorSelected  // 0x28
  private           UnityEngine.Rendering.UI.DebugUIHandlerWidget<parentUIHandler>k__BackingField  // 0x38
  private           UnityEngine.Rendering.UI.DebugUIHandlerWidget<previousUIHandler>k__BackingField  // 0x40
  private           UnityEngine.Rendering.UI.DebugUIHandlerWidget<nextUIHandler>k__BackingField  // 0x48
  protected         UnityEngine.Rendering.DebugUI.Widgetm_Widget  // 0x50
PROPERTIES:
  parentUIHandler  get=0x04D85A50  set=0x046A91B0
  previousUIHandler  get=0x04D85A60  set=0x042B4AE0
  nextUIHandler  get=0x04D86200  set=0x035A41A0
METHODS:
  RVA=0x041E1670  token=0x6000900  System.Void OnEnable()
  RVA=0x02FDAEA0  token=0x6000901  System.Void SetWidget(UnityEngine.Rendering.DebugUI.Widget widget)
  RVA=0x04D86270  token=0x6000902  UnityEngine.Rendering.DebugUI.Widget GetWidget()
  RVA=-1  // generic def  token=0x6000903  T CastWidget()
  RVA=0x02FFF600  token=0x6000904  System.Boolean OnSelection(System.Boolean fromNext, UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
  RVA=0x041E1670  token=0x6000905  System.Void OnDeselection()
  RVA=0x041E1670  token=0x6000906  System.Void OnAction()
  RVA=0x041E1670  token=0x6000907  System.Void OnIncrement(System.Boolean fast)
  RVA=0x041E1670  token=0x6000908  System.Void OnDecrement(System.Boolean fast)
  RVA=0x0B2EE008  token=0x6000909  UnityEngine.Rendering.UI.DebugUIHandlerWidget Previous()
  RVA=0x0B2EDF30  token=0x600090A  UnityEngine.Rendering.UI.DebugUIHandlerWidget Next()
  RVA=0x0B2EE08C  token=0x600090B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Rendering.UI.UIFoldout
TYPE:  class
TOKEN: 0x2000194
SIZE:  0x1C8
EXTENDS: UnityEngine.UI.Toggle
FIELDS:
  public            UnityEngine.GameObject          content  // 0x1b0
  public            UnityEngine.GameObject          arrowOpened  // 0x1b8
  public            UnityEngine.GameObject          arrowClosed  // 0x1c0
METHODS:
  RVA=0x0B2EE640  token=0x600090C  System.Void Start()
  RVA=0x0B2EE440  token=0x600090D  System.Void OnValidate()
  RVA=0x0B2EE454  token=0x600090E  System.Void SetState(System.Boolean state)
  RVA=0x0B2EE460  token=0x600090F  System.Void SetState(System.Boolean state, System.Boolean rebuildLayout)
  RVA=0x0B2EE704  token=0x6000910  System.Void .ctor()
END_CLASS

