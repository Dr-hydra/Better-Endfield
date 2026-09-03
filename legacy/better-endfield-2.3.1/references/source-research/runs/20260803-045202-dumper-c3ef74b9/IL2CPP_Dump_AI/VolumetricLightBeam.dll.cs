// ========================================================
// Dumped by @desirepro
// Assembly: VolumetricLightBeam.dll
// Classes:  68
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

CLASS: EditorPrefsStrings
TYPE:  static class
TOKEN: 0x2000002
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: Help
TYPE:  static class
TOKEN: 0x200000C
SIZE:  0x10
FIELDS:
  private   static  System.String                   UrlBase  // const
  private   static  System.String                   UrlSuffix  // const
  public    static  System.String                   UrlBeam  // const
  public    static  System.String                   UrlDustParticles  // const
  public    static  System.String                   UrlDynamicOcclusionRaycasting  // const
  public    static  System.String                   UrlDynamicOcclusionDepthBuffer  // const
  public    static  System.String                   UrlTriggerZone  // const
  public    static  System.String                   UrlSkewingHandle  // const
  public    static  System.String                   UrlEffectFlicker  // const
  public    static  System.String                   UrlEffectPulse  // const
  public    static  System.String                   UrlConfig  // const
METHODS:
END_CLASS

CLASS: Internal
TYPE:  static class
TOKEN: 0x200000D
SIZE:  0x10
FIELDS:
PROPERTIES:
  ProceduralObjectsHideFlags  get=0x011EC580
METHODS:
END_CLASS

CLASS: Beam
TYPE:  static class
TOKEN: 0x200000E
SIZE:  0x10
FIELDS:
  public    static  VLB.ColorMode                   ColorModeDefault  // const
  public    static  System.Single                   MultiplierDefault  // const
  public    static  System.Single                   MultiplierMin  // const
  public    static  System.Single                   IntensityDefault  // const
  public    static  System.Single                   IntensityMin  // const
  public    static  System.Single                   HDRPExposureWeightDefault  // const
  public    static  System.Single                   HDRPExposureWeightMin  // const
  public    static  System.Single                   HDRPExposureWeightMax  // const
  public    static  System.Single                   SpotAngleDefault  // const
  public    static  System.Single                   SpotAngleMin  // const
  public    static  System.Single                   SpotAngleMax  // const
  public    static  System.Single                   ConeRadiusStart  // const
  public    static  VLB.MeshType                    GeomMeshType  // const
  public    static  System.Int32                    GeomSidesDefault  // const
  public    static  System.Int32                    GeomSidesMin  // const
  public    static  System.Int32                    GeomSidesMax  // const
  public    static  System.Int32                    GeomSegmentsDefault  // const
  public    static  System.Int32                    GeomSegmentsMin  // const
  public    static  System.Int32                    GeomSegmentsMax  // const
  public    static  System.Boolean                  GeomCap  // const
  public    static  VLB.AttenuationEquation         AttenuationEquationDefault  // const
  public    static  System.Single                   AttenuationCustomBlendingDefault  // const
  public    static  System.Single                   AttenuationCustomBlendingMin  // const
  public    static  System.Single                   AttenuationCustomBlendingMax  // const
  public    static  System.Single                   FallOffStart  // const
  public    static  System.Single                   FallOffEnd  // const
  public    static  System.Single                   FallOffDistancesMinThreshold  // const
  public    static  System.Single                   DepthBlendDistance  // const
  public    static  System.Single                   CameraClippingDistance  // const
  public    static  System.Single                   FresnelPowMaxValue  // const
  public    static  System.Single                   FresnelPow  // const
  public    static  System.Single                   GlareFrontalDefault  // const
  public    static  System.Single                   GlareBehindDefault  // const
  public    static  System.Single                   GlareMin  // const
  public    static  System.Single                   GlareMax  // const
  public    static  VLB.NoiseMode                   NoiseModeDefault  // const
  public    static  System.Single                   NoiseIntensityMin  // const
  public    static  System.Single                   NoiseIntensityMax  // const
  public    static  System.Single                   NoiseIntensityDefault  // const
  public    static  System.Single                   NoiseScaleMin  // const
  public    static  System.Single                   NoiseScaleMax  // const
  public    static readonly UnityEngine.Vector3             NoiseScalesDefault  // static @ 0x0
  public    static  System.Single                   NoiseContrastDefault  // const
  public    static  System.Single                   NoiseContrastMin  // const
  public    static  System.Single                   NoiseContrastMax  // const
  public    static  System.Single                   NoiseScaleDefault  // const
  public    static  VLB.BlendingMode                BlendingModeDefault  // const
  public    static  VLB.ShaderAccuracy              ShaderAccuracyDefault  // const
  public    static  System.Single                   FadeOutBeginDefault  // const
  public    static  System.Single                   FadeOutEndDefault  // const
  public    static  System.Single                   FadeOutBeginSecondDefault  // const
  public    static  System.Single                   FadeOutEndSecondDefault  // const
  public    static  VLB.Dimensions                  DimensionsDefault  // const
  public    static  UnityEngine.Transform           ClippingPlaneTransformDefault  // const
  public    static readonly UnityEngine.Color               FlatColor  // static @ 0xc
  public    static readonly UnityEngine.Vector3             NoiseVelocityDefault  // static @ 0x1c
  public    static readonly UnityEngine.Vector2             TiltDefault  // static @ 0x28
  public    static readonly UnityEngine.Vector3             SkewingLocalForwardDirectionDefault  // static @ 0x30
METHODS:
  RVA=0x04B39200  token=0x6000033  System.Void .cctor()
END_CLASS

CLASS: DustParticles
TYPE:  static class
TOKEN: 0x200000F
SIZE:  0x10
FIELDS:
  public    static  System.Single                   AlphaDefault  // const
  public    static  System.Single                   SizeDefault  // const
  public    static  VLB.ParticlesDirection          DirectionDefault  // const
  public    static  System.Single                   DensityDefault  // const
  public    static  System.Single                   DensityMin  // const
  public    static  System.Single                   DensityMax  // const
  public    static  System.Boolean                  CullingEnabledDefault  // const
  public    static  System.Single                   CullingMaxDistanceDefault  // const
  public    static  System.Single                   CullingMaxDistanceMin  // const
  public    static readonly UnityEngine.Vector3             VelocityDefault  // static @ 0x0
  public    static readonly VLB.MinMaxRangeFloat            SpawnDistanceRangeDefault  // static @ 0xc
METHODS:
  RVA=0x0B897458  token=0x6000034  System.Void .cctor()
END_CLASS

CLASS: DynOcclusion
TYPE:  static class
TOKEN: 0x2000010
SIZE:  0x10
FIELDS:
  public    static  System.Single                   FadeDistanceToSurfaceDefault  // const
  public    static  VLB.DynamicOcclusionUpdateRate  UpdateRateDefault  // const
  public    static  System.Int32                    WaitFramesCountDefault  // const
  public    static  VLB.Dimensions                  RaycastingDimensionsDefault  // const
  public    static  System.Boolean                  RaycastingConsiderTriggersDefault  // const
  public    static  System.Single                   RaycastingMinOccluderAreaDefault  // const
  public    static  System.Single                   RaycastingMinSurfaceRatioDefault  // const
  public    static  System.Single                   RaycastingMinSurfaceRatioMin  // const
  public    static  System.Single                   RaycastingMinSurfaceRatioMax  // const
  public    static  System.Single                   RaycastingMaxSurfaceDotDefault  // const
  public    static  System.Single                   RaycastingMaxSurfaceAngleMin  // const
  public    static  System.Single                   RaycastingMaxSurfaceAngleMax  // const
  public    static  VLB.PlaneAlignment              RaycastingPlaneAlignmentDefault  // const
  public    static  System.Single                   RaycastingPlaneOffsetDefault  // const
  public    static  System.Int32                    DepthBufferDepthMapResolutionDefault  // const
  public    static  System.Boolean                  DepthBufferOcclusionCullingDefault  // const
  public    static readonly UnityEngine.LayerMask           LayerMaskDefault  // static @ 0x0
METHODS:
  RVA=0x0B8974C0  token=0x6000035  System.Void .cctor()
END_CLASS

CLASS: Config
TYPE:  static class
TOKEN: 0x2000011
SIZE:  0x10
FIELDS:
  public    static  System.Boolean                  GeometryOverrideLayerDefault  // const
  public    static  System.Int32                    GeometryLayerIDDefault  // const
  public    static  System.String                   GeometryTagDefault  // const
  public    static  System.String                   FadeOutCameraTagDefault  // const
  public    static  VLB.RenderQueue                 GeometryRenderQueueDefault  // const
  public    static  VLB.RenderPipeline              GeometryRenderPipelineDefault  // const
  public    static  VLB.RenderingMode               GeometryRenderingModeDefault  // const
  public    static  System.Int32                    Noise3DSizeDefault  // const
  public    static  System.Int32                    SharedMeshSides  // const
  public    static  System.Int32                    SharedMeshSegments  // const
  public    static  System.Single                   DitheringFactor  // const
  public    static  System.Boolean                  UseLightColorTemperatureDefault  // const
  public    static  System.Boolean                  FeatureEnabledDefault  // const
  public    static  VLB.FeatureEnabledColorGradient FeatureEnabledColorGradientDefault  // const
METHODS:
END_CLASS

CLASS: BlendingMode
TYPE:  sealed struct
TOKEN: 0x2000023
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.MaterialManager.BlendingModeAdditive  // const
  public    static  VLB.MaterialManager.BlendingModeSoftAdditive  // const
  public    static  VLB.MaterialManager.BlendingModeTraditionalTransparency  // const
  public    static  VLB.MaterialManager.BlendingModeCount  // const
METHODS:
END_CLASS

CLASS: ColorGradient
TYPE:  sealed struct
TOKEN: 0x2000024
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.MaterialManager.ColorGradientOff  // const
  public    static  VLB.MaterialManager.ColorGradientMatrixLow  // const
  public    static  VLB.MaterialManager.ColorGradientMatrixHigh  // const
  public    static  VLB.MaterialManager.ColorGradientCount  // const
METHODS:
END_CLASS

CLASS: DepthBlend
TYPE:  sealed struct
TOKEN: 0x2000025
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.MaterialManager.DepthBlend  Off  // const
  public    static  VLB.MaterialManager.DepthBlend  On  // const
  public    static  VLB.MaterialManager.DepthBlend  Count  // const
METHODS:
END_CLASS

CLASS: DynamicOcclusion
TYPE:  sealed struct
TOKEN: 0x2000026
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.MaterialManager.DynamicOcclusionOff  // const
  public    static  VLB.MaterialManager.DynamicOcclusionClippingPlane  // const
  public    static  VLB.MaterialManager.DynamicOcclusionDepthTexture  // const
  public    static  VLB.MaterialManager.DynamicOcclusionCount  // const
METHODS:
END_CLASS

CLASS: MeshSkewing
TYPE:  sealed struct
TOKEN: 0x2000027
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.MaterialManager.MeshSkewing Off  // const
  public    static  VLB.MaterialManager.MeshSkewing On  // const
  public    static  VLB.MaterialManager.MeshSkewing Count  // const
METHODS:
END_CLASS

CLASS: Noise3D
TYPE:  sealed struct
TOKEN: 0x2000028
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.MaterialManager.Noise3D     Off  // const
  public    static  VLB.MaterialManager.Noise3D     On  // const
  public    static  VLB.MaterialManager.Noise3D     Count  // const
METHODS:
END_CLASS

CLASS: ShaderAccuracy
TYPE:  sealed struct
TOKEN: 0x2000029
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.MaterialManager.ShaderAccuracyFast  // const
  public    static  VLB.MaterialManager.ShaderAccuracyHigh  // const
  public    static  VLB.MaterialManager.ShaderAccuracyCount  // const
METHODS:
END_CLASS

CLASS: StaticProperties
TYPE:  sealed struct
TOKEN: 0x200002A
SIZE:  0x30
FIELDS:
  public            System.Boolean                  useFog  // 0x10
  public            VLB.MaterialManager.BlendingModeblendingMode  // 0x14
  public            VLB.MaterialManager.Noise3D     noise3D  // 0x18
  public            VLB.MaterialManager.DepthBlend  depthBlend  // 0x1c
  public            VLB.MaterialManager.ColorGradientcolorGradient  // 0x20
  public            VLB.MaterialManager.DynamicOcclusiondynamicOcclusion  // 0x24
  public            VLB.MaterialManager.MeshSkewing meshSkewing  // 0x28
  public            VLB.MaterialManager.ShaderAccuracyshaderAccuracy  // 0x2c
PROPERTIES:
  blendingModeID  get=0x04D88B20
  noise3DID  get=0x0B8988F4
  depthBlendID  get=0x0B8987B8
  colorGradientID  get=0x0B898784
  dynamicOcclusionID  get=0x0B8987EC
  meshSkewingID  get=0x0B8988C0
  shaderAccuracyID  get=0x0B898928
  materialID  get=0x0B898820
METHODS:
  RVA=0x0B898460  token=0x6000043  System.Void ApplyToMaterial(UnityEngine.Material mat)
END_CLASS

CLASS: MaterialsGroup
TYPE:  class
TOKEN: 0x200002B
SIZE:  0x18
FIELDS:
  public    readonly UnityEngine.Material[]          materials  // 0x10
METHODS:
  RVA=0x0B897B74  token=0x6000044  System.Void .ctor()
END_CLASS

CLASS: Callback
TYPE:  sealed class
TOKEN: 0x200002D
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x074FF394  token=0x6000045  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000046  System.Void Invoke(VLB.MaterialModifier.Interface owner)
  RVA=0x047FDD70  token=0x6000047  System.IAsyncResult BeginInvoke(VLB.MaterialModifier.Interface owner, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000048  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: Interface
TYPE:  interface
TOKEN: 0x200002E
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000049  System.Void SetMaterialProp(System.Int32 nameID, System.Single value)
  RVA=-1  // abstract  token=0x600004A  System.Void SetMaterialProp(System.Int32 nameID, UnityEngine.Vector4 value)
  RVA=-1  // abstract  token=0x600004B  System.Void SetMaterialProp(System.Int32 nameID, UnityEngine.Color value)
  RVA=-1  // abstract  token=0x600004C  System.Void SetMaterialProp(System.Int32 nameID, UnityEngine.Matrix4x4 value)
  RVA=-1  // abstract  token=0x600004D  System.Void SetMaterialProp(System.Int32 nameID, UnityEngine.Texture value)
END_CLASS

CLASS: Plane2D
TYPE:  sealed struct
TOKEN: 0x2000033
SIZE:  0x1C
FIELDS:
  public            UnityEngine.Vector2             normal  // 0x10
  public            System.Single                   distance  // 0x18
METHODS:
  RVA=0x04DBF500  token=0x600005B  System.Single Distance(UnityEngine.Vector2 point)
  RVA=0x0B897D14  token=0x600005C  UnityEngine.Vector2 ClosestPoint(UnityEngine.Vector2 pt)
  RVA=0x0B89807C  token=0x600005D  UnityEngine.Vector2 Intersect(UnityEngine.Vector2 p1, UnityEngine.Vector2 p2)
  RVA=0x0B898060  token=0x600005E  System.Boolean GetSide(UnityEngine.Vector2 point)
  RVA=0x0B897FA0  token=0x600005F  VLB.PolygonHelper.Plane2D FromPoints(UnityEngine.Vector3 p1, UnityEngine.Vector3 p2)
  RVA=0x0B897F2C  token=0x6000060  VLB.PolygonHelper.Plane2D FromNormalAndPoint(UnityEngine.Vector3 normalizedNormal, UnityEngine.Vector3 p1)
  RVA=0x0B897F04  token=0x6000061  System.Void Flip()
  RVA=0x0B897D58  token=0x6000062  UnityEngine.Vector2[] CutConvex(UnityEngine.Vector2[] poly)
  RVA=0x0B898148  token=0x6000063  System.String ToString()
END_CLASS

CLASS: RenderPipeline
TYPE:  sealed struct
TOKEN: 0x2000038
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.SRPHelper.RenderPipeline    HGRP  // const
  public    static  VLB.SRPHelper.RenderPipeline    Undefined  // const
  public    static  VLB.SRPHelper.RenderPipeline    BuiltIn  // const
  public    static  VLB.SRPHelper.RenderPipeline    URP  // const
  public    static  VLB.SRPHelper.RenderPipeline    LWRP  // const
  public    static  VLB.SRPHelper.RenderPipeline    HDRP  // const
METHODS:
END_CLASS

CLASS: Packed
TYPE:  sealed struct
TOKEN: 0x200003A
SIZE:  0x38
FIELDS:
  public            UnityEngine.Vector3             position  // 0x10
  public            UnityEngine.Quaternion          rotation  // 0x1c
  public            UnityEngine.Vector3             lossyScale  // 0x2c
METHODS:
  RVA=0x0B897BE4  token=0x6000070  System.Boolean IsSame(UnityEngine.Transform transf)
END_CLASS

CLASS: FloatPackingPrecision
TYPE:  sealed struct
TOKEN: 0x200003C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.Utils.FloatPackingPrecision High  // const
  public    static  VLB.Utils.FloatPackingPrecision Low  // const
  public    static  VLB.Utils.FloatPackingPrecision Undef  // const
METHODS:
END_CLASS

CLASS: AttachedLightType
TYPE:  sealed struct
TOKEN: 0x200003F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.VolumetricLightBeam.AttachedLightTypeNoLight  // const
  public    static  VLB.VolumetricLightBeam.AttachedLightTypeOtherLight  // const
  public    static  VLB.VolumetricLightBeam.AttachedLightTypeSpotLight  // const
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=12
TYPE:  sealed struct
TOKEN: 0x2000041
SIZE:  0x1C
FIELDS:
METHODS:
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x2000040
SIZE:  0x10
FIELDS:
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=120A0EC6D4742068B4D88C6145B8224EF1DC240C8A305CDFC50C3AAF9121E6875D  // static @ 0x0
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=12F186F2262AE48F2AA4F90C9A6B35913B0F6B0B895423B6267252259BFD357D3B  // static @ 0xc
METHODS:
END_CLASS

CLASS: Beyond.SourceGenerator.DataNAttribute
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public            System.Int32                    capacity  // 0x10
  public            System.Int32                    tSize  // 0x14
  public            System.Boolean                  ring  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000002  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardUtility
TYPE:  static class
TOKEN: 0x2000005
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_enableFastPathBuffer  // static @ 0x0
METHODS:
  RVA=-1  // generic def  token=0x6000003  System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity)
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardAttribute
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000004  System.Void .ctor()
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
  RVA=0x041E1670  token=0x6000005  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.CameraControlConfigAttribute
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000006  System.Void .ctor()
END_CLASS

CLASS: HG.Rendering.Runtime.HGConstantBufferLayoutAttribute
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x2000042
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x60000E3  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x0B8974FC  token=0x60000E4  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x2000043
SIZE:  0x40
EXTENDS: IFix.Core.AnonymousStorey
FIELDS:
METHODS:
  RVA=0x0B897560  token=0x60000E5  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x60000E6  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x2000044
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x60000E7  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x0B89AC90  token=0x60000E8  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x0B89AD80  token=0x60000E9  System.Boolean IsPatched(System.Int32 id)
  RVA=0x0B89AB4C  token=0x60000EA  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x0B89AC18  token=0x60000EB  System.Object CreateWrapper(System.Int32 id)
  RVA=0x0B89ACF4  token=0x60000EC  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x0B89AAA8  token=0x60000ED  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: VLB.BeamGeometry
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x98
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Interface
FIELDS:
  private           VLB.VolumetricLightBeam         m_Master  // 0x18
  private           UnityEngine.Matrix4x4           m_ColorGradientMatrix  // 0x20
  private           VLB.MeshType                    m_CurrentMeshType  // 0x60
  private           VLB.MaterialModifier.Callback   m_MaterialModifierCallback  // 0x68
  private           UnityEngine.Coroutine           m_CoFadeOut  // 0x70
  private           UnityEngine.MeshRenderer        <meshRenderer>k__BackingField  // 0x78
  private           UnityEngine.MeshFilter          <meshFilter>k__BackingField  // 0x80
  private           UnityEngine.Mesh                <coneMesh>k__BackingField  // 0x88
  public            UnityEngine.Material            m_CustomMaterial  // 0x90
PROPERTIES:
  meshRenderer  get=0x04D862A0  set=0x06402320
  meshFilter  get=0x04D86260  set=0x04CD9970
  coneMesh  get=0x04D861F0  set=0x05395D30
  visible  get=0x0B896EA0  set=0x0B896F00
  sortingLayerID  get=0x0B896E60  set=0x0B896EC0
  sortingOrder  get=0x0B896E80  set=0x0B896EE0
  _INTERNAL_IsFadeOutCoroutineRunning  get=0x09B02DAC
METHODS:
  RVA=0x041E1670  token=0x6000014  System.Void RestartFadeOutCoroutine()
  RVA=0x041E1670  token=0x6000015  System.Void StopFadeOutCoroutine()
  RVA=0x0B896D94  token=0x6000016  System.Void SetMaterialProp(System.Int32 nameID, System.Single value)
  RVA=0x0B896B10  token=0x6000017  System.Void SetMaterialProp(System.Int32 nameID, UnityEngine.Vector4 value)
  RVA=0x0B896BF0  token=0x6000018  System.Void SetMaterialProp(System.Int32 nameID, UnityEngine.Color value)
  RVA=0x0B8969F8  token=0x6000019  System.Void SetMaterialProp(System.Int32 nameID, UnityEngine.Matrix4x4 value)
  RVA=0x0B896CD0  token=0x600001A  System.Void SetMaterialProp(System.Int32 nameID, UnityEngine.Texture value)
  RVA=0x05393520  token=0x600001B  System.Void .ctor()
END_CLASS

CLASS: VLB.Config
TYPE:  class
TOKEN: 0x200000A
SIZE:  0xB8
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  public    static  System.String                   ClassName  // const
  public    static  System.String                   kAssetName  // const
  public    static  System.String                   kAssetNameExt  // const
  private   static  VLB.Config                      ms_Instance  // static @ 0x0
  public            UnityEngine.Mesh                sharedMesh  // 0x18
  public            System.Boolean                  featureEnabledDustParticles  // 0x20
  public            System.Boolean                  featureEnabledEffect  // 0x21
  public            System.Boolean                  featureEnabledTriggerZone  // 0x22
  public            System.Boolean                  geometryOverrideLayer  // 0x23
  public            System.Int32                    geometryLayerID  // 0x24
  public            System.String                   geometryTag  // 0x28
  public            System.Int32                    geometryRenderQueue  // 0x30
  private           VLB.RenderPipeline              _RenderPipeline  // 0x34
  private           VLB.RenderingMode               _RenderingMode  // 0x38
  public            System.Single                   ditheringFactor  // 0x3c
  public            System.Boolean                  useLightColorTemperature  // 0x40
  public            System.Int32                    sharedMeshSides  // 0x44
  public            System.Int32                    sharedMeshSegments  // 0x48
  public            UnityEngine.Vector3             globalNoisesScale  // 0x4c
  public            UnityEngine.Vector3             globalNoiseVelocity  // 0x58
  public            System.String                   fadeOutCameraTag  // 0x68
  public            UnityEngine.Texture3D           noiseTexture3D  // 0x70
  public            UnityEngine.ParticleSystem      dustParticlesPrefab  // 0x78
  public            UnityEngine.Texture2D           ditheringNoiseTexture  // 0x80
  public            VLB.FeatureEnabledColorGradient featureEnabledColorGradient  // 0x88
  public            System.Boolean                  featureEnabledDepthBlend  // 0x8c
  public            System.Boolean                  featureEnabledNoise3D  // 0x8d
  public            System.Boolean                  featureEnabledDynamicOcclusion  // 0x8e
  public            System.Boolean                  featureEnabledMeshSkewing  // 0x8f
  public            System.Boolean                  featureEnabledShaderAccuracyHigh  // 0x90
  private           UnityEngine.Shader              _BeamShader  // 0x98
  private           UnityEngine.Transform           m_CachedFadeOutCamera  // 0xa0
  private           System.Int32                    pluginVersion  // 0xa8
  private           UnityEngine.Material            _DummyMaterial  // 0xb0
PROPERTIES:
  renderPipeline  get=0x04D86730
  renderingMode  get=0x04D863B0
  actualRenderingMode  get=0x0B8973B0
  useSinglePassShader  get=0x0B897440
  requiresDoubleSidedMesh  get=0x0B897440
  beamShader  get=0x04D85EE0
  fadeOutCameraTransform  get=0x0B8973CC
  isHDRPExposureWeightSupported  get=0x0B897438
  hasRenderPipelineMismatch  get=0x037E2870
  Instance  get=0x05393938
METHODS:
  RVA=0x0B8972AC  token=0x6000026  System.Void Reset()
  RVA=0x04D83220  token=0x6000027  System.Void OnEnable()
  RVA=0x012081B0  token=0x6000028  System.Boolean IsSRPBatcherSupported()
  RVA=0x0B896F7C  token=0x6000029  System.Void ForceUpdateFadeOutCamera()
  RVA=0x037E27D0  token=0x600002A  System.Void OnStartup()
  RVA=0x037E2DC0  token=0x600002B  System.Void RefreshGlobalShaderProperties()
  RVA=0x0B897138  token=0x600002C  System.Void ResetInternalData()
  RVA=0x0B897000  token=0x600002D  UnityEngine.ParticleSystem NewVolumetricDustParticles()
  RVA=0x04D83230  token=0x600002E  System.Void HandleBackwardCompatibility(System.Int32 serializedVersion, System.Int32 newVersion)
  RVA=0x0496FF50  token=0x600002F  VLB.Config LoadAssetInternal(System.String assetName)
  RVA=0x0496FD70  token=0x6000030  VLB.Config GetInstance(System.Boolean assertIfNotFound)
  RVA=0x04B698D0  token=0x6000031  System.Void .ctor()
END_CLASS

CLASS: VLB.Consts
TYPE:  static class
TOKEN: 0x200000B
SIZE:  0x10
FIELDS:
  public    static  System.String                   PluginFolder  // const
METHODS:
END_CLASS

CLASS: VLB.DummyMaterial
TYPE:  static class
TOKEN: 0x2000012
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: VLB.FeatureEnabledColorGradient
TYPE:  sealed struct
TOKEN: 0x2000013
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.FeatureEnabledColorGradient Off  // const
  public    static  VLB.FeatureEnabledColorGradient HighOnly  // const
  public    static  VLB.FeatureEnabledColorGradient HighAndLow  // const
METHODS:
END_CLASS

CLASS: VLB.ColorMode
TYPE:  sealed struct
TOKEN: 0x2000014
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.ColorMode                   Flat  // const
  public    static  VLB.ColorMode                   Gradient  // const
METHODS:
END_CLASS

CLASS: VLB.AttenuationEquation
TYPE:  sealed struct
TOKEN: 0x2000015
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.AttenuationEquation         Linear  // const
  public    static  VLB.AttenuationEquation         Quadratic  // const
  public    static  VLB.AttenuationEquation         Blend  // const
METHODS:
END_CLASS

CLASS: VLB.BlendingMode
TYPE:  sealed struct
TOKEN: 0x2000016
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.BlendingMode                Additive  // const
  public    static  VLB.BlendingMode                SoftAdditive  // const
  public    static  VLB.BlendingMode                TraditionalTransparency  // const
METHODS:
END_CLASS

CLASS: VLB.ShaderAccuracy
TYPE:  sealed struct
TOKEN: 0x2000017
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.ShaderAccuracy              Fast  // const
  public    static  VLB.ShaderAccuracy              High  // const
METHODS:
END_CLASS

CLASS: VLB.NoiseMode
TYPE:  sealed struct
TOKEN: 0x2000018
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.NoiseMode                   Disabled  // const
  public    static  VLB.NoiseMode                   WorldSpace  // const
  public    static  VLB.NoiseMode                   LocalSpace  // const
METHODS:
END_CLASS

CLASS: VLB.MeshType
TYPE:  sealed struct
TOKEN: 0x2000019
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.MeshType                    Shared  // const
  public    static  VLB.MeshType                    Custom  // const
METHODS:
END_CLASS

CLASS: VLB.RenderPipeline
TYPE:  sealed struct
TOKEN: 0x200001A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.RenderPipeline              HGRP  // const
  public    static  VLB.RenderPipeline              BuiltIn  // const
  public    static  VLB.RenderPipeline              URP  // const
  public    static  VLB.RenderPipeline              HDRP  // const
METHODS:
END_CLASS

CLASS: VLB.RenderingMode
TYPE:  sealed struct
TOKEN: 0x200001B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.RenderingMode               MultiPass  // const
  public    static  VLB.RenderingMode               SinglePass  // const
  public    static  VLB.RenderingMode               GPUInstancing  // const
  public    static  VLB.RenderingMode               SRPBatcher  // const
METHODS:
END_CLASS

CLASS: VLB.RenderQueue
TYPE:  sealed struct
TOKEN: 0x200001C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.RenderQueue                 Custom  // const
  public    static  VLB.RenderQueue                 Background  // const
  public    static  VLB.RenderQueue                 Geometry  // const
  public    static  VLB.RenderQueue                 AlphaTest  // const
  public    static  VLB.RenderQueue                 GeometryLast  // const
  public    static  VLB.RenderQueue                 Transparent  // const
  public    static  VLB.RenderQueue                 Overlay  // const
METHODS:
END_CLASS

CLASS: VLB.Dimensions
TYPE:  sealed struct
TOKEN: 0x200001D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.Dimensions                  Dim3D  // const
  public    static  VLB.Dimensions                  Dim2D  // const
METHODS:
END_CLASS

CLASS: VLB.PlaneAlignment
TYPE:  sealed struct
TOKEN: 0x200001E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.PlaneAlignment              Surface  // const
  public    static  VLB.PlaneAlignment              Beam  // const
METHODS:
END_CLASS

CLASS: VLB.DynamicOcclusionUpdateRate
TYPE:  sealed struct
TOKEN: 0x200001F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.DynamicOcclusionUpdateRate  Never  // const
  public    static  VLB.DynamicOcclusionUpdateRate  OnEnable  // const
  public    static  VLB.DynamicOcclusionUpdateRate  OnBeamMove  // const
  public    static  VLB.DynamicOcclusionUpdateRate  EveryXFrames  // const
  public    static  VLB.DynamicOcclusionUpdateRate  OnBeamMoveAndEveryXFrames  // const
METHODS:
END_CLASS

CLASS: VLB.ParticlesDirection
TYPE:  sealed struct
TOKEN: 0x2000020
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  VLB.ParticlesDirection          Random  // const
  public    static  VLB.ParticlesDirection          LocalSpace  // const
  public    static  VLB.ParticlesDirection          WorldSpace  // const
METHODS:
END_CLASS

CLASS: VLB.HighlightNullAttribute
TYPE:  sealed class
TOKEN: 0x2000021
SIZE:  0x10
EXTENDS: UnityEngine.PropertyAttribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000036  System.Void .ctor()
END_CLASS

CLASS: VLB.MaterialManager
TYPE:  static class
TOKEN: 0x2000022
SIZE:  0x10
FIELDS:
  public    static  System.Int32                    staticPropertiesCount  // const
  public    static  UnityEngine.MaterialPropertyBlockmaterialPropertyBlock  // static @ 0x0
  private   static readonly UnityEngine.Rendering.BlendMode[]BlendingMode_SrcFactor  // static @ 0x8
  private   static readonly UnityEngine.Rendering.BlendMode[]BlendingMode_DstFactor  // static @ 0x10
  private   static readonly System.Boolean[]                BlendingMode_AlphaAsBlack  // static @ 0x18
  private   static readonly System.Collections.Hashtable    ms_MaterialsGroup  // static @ 0x20
METHODS:
  RVA=0x0B8978A4  token=0x6000037  UnityEngine.Material NewMaterialTransient(System.Boolean gpuInstanced)
  RVA=0x0B8977E8  token=0x6000038  UnityEngine.Material NewMaterialPersistent(UnityEngine.Shader shader, System.Boolean gpuInstanced)
  RVA=0x0B8975F4  token=0x6000039  UnityEngine.Material GetInstancedMaterial(System.UInt32 groupID, VLB.MaterialManager.StaticProperties& staticProps)
  RVA=0x0B897964  token=0x600003A  System.Void .cctor()
END_CLASS

CLASS: VLB.MaterialModifier
TYPE:  static class
TOKEN: 0x200002C
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: VLB.MinMaxRangeFloat
TYPE:  sealed struct
TOKEN: 0x200002F
SIZE:  0x18
FIELDS:
  private           System.Single                   m_MinValue  // 0x10
  private           System.Single                   m_MaxValue  // 0x14
PROPERTIES:
  minValue  get=0x04D871D0
  maxValue  get=0x04D9C560
  randomValue  get=0x0B897BD0
  asVector2  get=0x04D889F0
METHODS:
  RVA=0x04D87A10  token=0x600004E  System.Void .ctor(System.Single min, System.Single max)
  RVA=0x0B897BBC  token=0x6000053  System.Single GetLerpedValue(System.Single lerp01)
END_CLASS

CLASS: VLB.MinMaxRangeAttribute
TYPE:  class
TOKEN: 0x2000030
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private   readonly System.Single                   <minValue>k__BackingField  // 0x10
  private   readonly System.Single                   <maxValue>k__BackingField  // 0x14
PROPERTIES:
  minValue  get=0x04D85F70
  maxValue  get=0x04D88D40
METHODS:
  RVA=0x04D8D120  token=0x6000054  System.Void .ctor(System.Single min, System.Single max)
END_CLASS

CLASS: VLB.PlatformHelper
TYPE:  class
TOKEN: 0x2000031
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0496FF90  token=0x6000057  System.String GetCurrentPlatformSuffix()
  RVA=0x0496FFB0  token=0x6000058  System.String GetPlatformSuffix(UnityEngine.RuntimePlatform platform)
  RVA=0x041E1670  token=0x6000059  System.Void .ctor()
END_CLASS

CLASS: VLB.PolygonHelper
TYPE:  class
TOKEN: 0x2000032
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
METHODS:
  RVA=0x05393520  token=0x600005A  System.Void .ctor()
END_CLASS

CLASS: VLB.ShaderKeywords
TYPE:  static class
TOKEN: 0x2000034
SIZE:  0x10
FIELDS:
  public    static  System.String                   UseFog  // const
  public    static  System.String                   AlphaAsBlack  // const
  public    static  System.String                   ColorGradientMatrixLow  // const
  public    static  System.String                   ColorGradientMatrixHigh  // const
  public    static  System.String                   DepthBlend  // const
  public    static  System.String                   Noise3D  // const
  public    static  System.String                   OcclusionClippingPlane  // const
  public    static  System.String                   OcclusionDepthTexture  // const
  public    static  System.String                   MeshSkewing  // const
  public    static  System.String                   ShaderAccuracyHigh  // const
METHODS:
END_CLASS

CLASS: VLB.ShaderProperties
TYPE:  static class
TOKEN: 0x2000035
SIZE:  0x10
FIELDS:
  public    static readonly System.Int32                    ConeParams0  // static @ 0x0
  public    static readonly System.Int32                    ConeParams1  // static @ 0x4
  public    static readonly System.Int32                    TiltVectorX  // static @ 0x8
  public    static readonly System.Int32                    TiltVectorY  // static @ 0xc
  public    static readonly System.Int32                    DistanceFadeStart  // static @ 0x10
  public    static readonly System.Int32                    DistanceFadeEnd  // static @ 0x14
  public    static readonly System.Int32                    DistanceFadeStartSecond  // static @ 0x18
  public    static readonly System.Int32                    DistanceFadeEndSecond  // static @ 0x1c
  public    static readonly System.Int32                    NoiseContrast  // static @ 0x20
  public    static readonly System.Int32                    NoiseVelocity  // static @ 0x24
  public    static readonly System.Int32                    NoiseScale  // static @ 0x28
  public    static readonly System.Int32                    DepthBlendCapOff  // static @ 0x2c
  public    static readonly System.Int32                    BlendMode  // static @ 0x30
  public    static readonly System.Int32                    FogIntensity  // static @ 0x34
  public    static readonly System.Int32                    ClippingPlaneTransition  // static @ 0x38
  public    static readonly System.Int32                    UseClippingPlane  // static @ 0x3c
  public    static readonly System.Int32                    FadeOutFactor  // static @ 0x40
  public    static readonly System.Int32                    ConeSlopeCosSin  // static @ 0x44
  public    static readonly System.Int32                    ConeRadius  // static @ 0x48
  public    static readonly System.Int32                    ConeGeomProps  // static @ 0x4c
  public    static readonly System.Int32                    ColorFlat  // static @ 0x50
  public    static readonly System.Int32                    AlphaInside  // static @ 0x54
  public    static readonly System.Int32                    AlphaOutside  // static @ 0x58
  public    static readonly System.Int32                    HDRPExposureWeight  // static @ 0x5c
  public    static readonly System.Int32                    AttenuationLerpLinearQuad  // static @ 0x60
  public    static readonly System.Int32                    DistanceFallOff  // static @ 0x64
  public    static readonly System.Int32                    DistanceCamClipping  // static @ 0x68
  public    static readonly System.Int32                    FresnelPow  // static @ 0x6c
  public    static readonly System.Int32                    GlareBehind  // static @ 0x70
  public    static readonly System.Int32                    GlareFrontal  // static @ 0x74
  public    static readonly System.Int32                    DrawCap  // static @ 0x78
  public    static readonly System.Int32                    DepthBlendDistance  // static @ 0x7c
  public    static readonly System.Int32                    NoiseVelocityAndScale  // static @ 0x80
  public    static readonly System.Int32                    NoiseParam  // static @ 0x84
  public    static readonly System.Int32                    CameraParams  // static @ 0x88
  public    static readonly System.Int32                    ColorGradientMatrix  // static @ 0x8c
  public    static readonly System.Int32                    LocalToWorldMatrix  // static @ 0x90
  public    static readonly System.Int32                    WorldToLocalMatrix  // static @ 0x94
  public    static readonly System.Int32                    BlendSrcFactor  // static @ 0x98
  public    static readonly System.Int32                    BlendDstFactor  // static @ 0x9c
  public    static readonly System.Int32                    DynamicOcclusionClippingPlaneWS  // static @ 0xa0
  public    static readonly System.Int32                    DynamicOcclusionClippingPlaneProps  // static @ 0xa4
  public    static readonly System.Int32                    DynamicOcclusionDepthTexture  // static @ 0xa8
  public    static readonly System.Int32                    DynamicOcclusionDepthProps  // static @ 0xac
  public    static readonly System.Int32                    LocalForwardDirection  // static @ 0xb0
  public    static readonly System.Int32                    TiltVector  // static @ 0xb4
  public    static readonly System.Int32                    AdditionalClippingPlaneWS  // static @ 0xb8
  public    static readonly System.Int32                    ParticlesTintColor  // static @ 0xbc
  public    static readonly System.Int32                    GlobalUsesReversedZBuffer  // static @ 0xc0
  public    static readonly System.Int32                    GlobalNoiseTex3D  // static @ 0xc4
  public    static readonly System.Int32                    GlobalNoiseCustomTime  // static @ 0xc8
  public    static readonly System.Int32                    GlobalDitheringFactor  // static @ 0xcc
  public    static readonly System.Int32                    GlobalDitheringNoiseTex  // static @ 0xd0
METHODS:
  RVA=0x04654190  token=0x6000064  System.Void .cctor()
END_CLASS

CLASS: VLB.SpotLightHelper
TYPE:  static class
TOKEN: 0x2000036
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B8982A0  token=0x6000065  System.Void GetCullingDistance(UnityEngine.Light light, System.Single& cullingDistance, System.Single& falloffDistance)
  RVA=0x0B898390  token=0x6000066  System.Single GetIntensity(UnityEngine.Light light)
  RVA=0x0B8983F8  token=0x6000067  System.Single GetSpotAngle(UnityEngine.Light light)
  RVA=0x0B898328  token=0x6000068  System.Single GetFallOffEnd(UnityEngine.Light light)
END_CLASS

CLASS: VLB.SRPHelper
TYPE:  static class
TOKEN: 0x2000037
SIZE:  0x10
FIELDS:
  private   static  VLB.SRPHelper.RenderPipeline    m_RenderPipelineCached  // static @ 0x0
PROPERTIES:
  renderPipelineType  get=0x037E28E0
METHODS:
  RVA=0x011EC580  token=0x600006A  VLB.SRPHelper.RenderPipeline ComputeRenderPipeline()
  RVA=0x02FFF600  token=0x600006B  System.Boolean IsUsingCustomRenderPipeline()
  RVA=0x0B898208  token=0x600006C  System.Void RegisterOnBeginCameraRendering(System.Action<UnityEngine.Rendering.ScriptableRenderContext,UnityEngine.Camera> cb)
  RVA=0x0B898254  token=0x600006D  System.Void UnregisterOnBeginCameraRendering(System.Action<UnityEngine.Rendering.ScriptableRenderContext,UnityEngine.Camera> cb)
  RVA=0x04D79C10  token=0x600006E  System.Void .cctor()
END_CLASS

CLASS: VLB.TransformUtils
TYPE:  static class
TOKEN: 0x2000039
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B89895C  token=0x600006F  VLB.TransformUtils.Packed GetWorldPacked(UnityEngine.Transform self)
END_CLASS

CLASS: VLB.Utils
TYPE:  static class
TOKEN: 0x200003B
SIZE:  0x10
FIELDS:
  private   static  System.Single                   kEpsilon  // const
  private   static  System.Int32                    kFloatPackingHighMinShaderLevel  // const
  private   static  VLB.Utils.FloatPackingPrecision ms_FloatPackingPrecision  // static @ 0x0
METHODS:
  RVA=0x04DBF550  token=0x6000071  System.Single ComputeConeRadiusEnd(System.Single fallOffEnd, System.Single spotAngle)
  RVA=0x04DBF590  token=0x6000072  System.Single ComputeSpotAngle(System.Single fallOffEnd, System.Single coneRadiusEnd)
  RVA=-1  // generic def  token=0x6000073  System.Void Swap(T& a, T& b)
  RVA=0x0B898D3C  token=0x6000074  System.String GetPath(UnityEngine.Transform current)
  RVA=-1  // generic def  token=0x6000075  T NewWithComponent(System.String name)
  RVA=-1  // generic def  token=0x6000076  T GetOrAddComponent(UnityEngine.GameObject self)
  RVA=-1  // generic def  token=0x6000077  T GetOrAddComponent(UnityEngine.MonoBehaviour self)
  RVA=-1  // generic def  token=0x6000078  System.Void ForeachComponentsOnlyInChildren(UnityEngine.GameObject self, System.Action<T> lambda, System.Boolean includeInactive)
  RVA=0x0B898E5C  token=0x6000079  System.Boolean HasFlag(System.Enum mask, System.Enum flags)
  RVA=0x04D889F0  token=0x600007A  UnityEngine.Vector2 xy(UnityEngine.Vector3 aVector)
  RVA=0x04D88A40  token=0x600007B  UnityEngine.Vector2 xz(UnityEngine.Vector3 aVector)
  RVA=0x04D8B880  token=0x600007C  UnityEngine.Vector2 yz(UnityEngine.Vector3 aVector)
  RVA=0x04DB4770  token=0x600007D  UnityEngine.Vector2 yx(UnityEngine.Vector3 aVector)
  RVA=0x04DB5A80  token=0x600007E  UnityEngine.Vector2 zx(UnityEngine.Vector3 aVector)
  RVA=0x04DB5BE0  token=0x600007F  UnityEngine.Vector2 zy(UnityEngine.Vector3 aVector)
  RVA=0x06935188  token=0x6000080  System.Boolean Approximately(System.Single a, System.Single b, System.Single epsilon)
  RVA=0x0B8989E8  token=0x6000081  System.Boolean Approximately(UnityEngine.Vector2 a, UnityEngine.Vector2 b, System.Single epsilon)
  RVA=0x0B898A1C  token=0x6000082  System.Boolean Approximately(UnityEngine.Vector3 a, UnityEngine.Vector3 b, System.Single epsilon)
  RVA=0x0B898A80  token=0x6000083  System.Boolean Approximately(UnityEngine.Vector4 a, UnityEngine.Vector4 b, System.Single epsilon)
  RVA=0x04DBF530  token=0x6000084  UnityEngine.Vector4 AsVector4(UnityEngine.Vector3 vec3, System.Single w)
  RVA=0x0B898FF4  token=0x6000085  UnityEngine.Vector4 PlaneEquation(UnityEngine.Vector3 normalizedNormal, UnityEngine.Vector3 pt)
  RVA=0x0B898E00  token=0x6000086  System.Single GetVolumeCubic(UnityEngine.Bounds self)
  RVA=0x0B898C90  token=0x6000087  System.Single GetMaxArea2D(UnityEngine.Bounds self)
  RVA=0x04DBF5C0  token=0x6000088  UnityEngine.Color Opaque(UnityEngine.Color self)
  RVA=0x0B898B7C  token=0x6000089  UnityEngine.Color ComputeComplementaryColor(UnityEngine.Color self, System.Boolean blackAndWhite)
  RVA=0x0B8991F4  token=0x600008A  UnityEngine.Plane TranslateCustom(UnityEngine.Plane plane, UnityEngine.Vector3 translation)
  RVA=0x0B898AD4  token=0x600008B  UnityEngine.Vector3 ClosestPointOnPlaneCustom(UnityEngine.Plane plane, UnityEngine.Vector3 point)
  RVA=0x0B898ED8  token=0x600008C  System.Boolean IsAlmostZero(System.Single f)
  RVA=0x0B898EF0  token=0x600008D  System.Boolean IsValid(UnityEngine.Plane plane)
  RVA=0x0690914C  token=0x600008E  System.Void SetKeywordEnabled(UnityEngine.Material mat, System.String name, System.Boolean enabled)
  RVA=0x0B8991D0  token=0x600008F  System.Void SetShaderKeywordEnabled(System.String name, System.Boolean enabled)
  RVA=0x0B89912C  token=0x6000090  UnityEngine.Matrix4x4 SampleInMatrix(UnityEngine.Gradient self, System.Int32 floatPackingPrecision)
  RVA=0x0B899068  token=0x6000091  UnityEngine.Color[] SampleInArray(UnityEngine.Gradient self, System.Int32 samplesCount)
  RVA=0x04DAFA70  token=0x6000092  UnityEngine.Vector4 Vector4_Floor(UnityEngine.Vector4 vec)
  RVA=0x0B898F20  token=0x6000093  System.Single PackToFloat(UnityEngine.Color color, System.Int32 floatPackingPrecision)
  RVA=0x0B898C20  token=0x6000094  VLB.Utils.FloatPackingPrecision GetFloatPackingPrecision()
  RVA=0x041E1670  token=0x6000095  System.Void MarkCurrentSceneDirty()
  RVA=0x041E1670  token=0x6000096  System.Void MarkObjectDirty(UnityEngine.Object obj)
END_CLASS

CLASS: VLB.Version
TYPE:  static class
TOKEN: 0x200003D
SIZE:  0x10
FIELDS:
  public    static  System.Int32                    Current  // const
METHODS:
END_CLASS

CLASS: VLB.VolumetricLightBeam
TYPE:  class
TOKEN: 0x200003E
SIZE:  0x198
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            UnityEngine.PlatformLayers      platformLayers  // 0x18
  public    static  System.String                   ClassName  // const
  public            System.Boolean                  colorFromLight  // 0x1c
  public            VLB.ColorMode                   colorMode  // 0x20
  public            UnityEngine.Color               color  // 0x24
  public            UnityEngine.Gradient            colorGradient  // 0x38
  public            System.Boolean                  intensityFromLight  // 0x40
  public            System.Boolean                  intensityModeAdvanced  // 0x41
  public            System.Single                   intensityInside  // 0x44
  public            System.Single                   intensityOutside  // 0x48
  public            System.Single                   intensityMultiplier  // 0x4c
  public            System.Single                   hdrpExposureWeight  // 0x50
  public            VLB.BlendingMode                blendingMode  // 0x54
  public            System.Boolean                  spotAngleFromLight  // 0x58
  public            System.Single                   spotAngle  // 0x5c
  public            System.Single                   spotAngleMultiplier  // 0x60
  public            System.Single                   coneRadiusStart  // 0x64
  public            VLB.ShaderAccuracy              shaderAccuracy  // 0x68
  public            VLB.MeshType                    geomMeshType  // 0x6c
  public            System.Int32                    geomCustomSides  // 0x70
  public            System.Int32                    geomCustomSegments  // 0x74
  public            UnityEngine.Vector3             skewingLocalForwardDirection  // 0x78
  public            System.Boolean                  useAdditionalClippingPlane  // 0x84
  public            UnityEngine.Transform           clippingPlaneTransform  // 0x88
  public            UnityEngine.Vector3             clippingPlanePosition  // 0x90
  public            UnityEngine.Quaternion          clippingPlaneRotation  // 0x9c
  public            System.Single                   clippingPlaneTransition  // 0xac
  public            System.Boolean                  geomCap  // 0xb0
  public            VLB.AttenuationEquation         attenuationEquation  // 0xb4
  public            System.Single                   attenuationCustomBlending  // 0xb8
  public            System.Single                   fallOffStart  // 0xbc
  public            System.Single                   fallOffEnd  // 0xc0
  public            System.Boolean                  fallOffEndFromLight  // 0xc4
  private           UnityEngine.Light               m_CachedLightSpot  // 0xc8
  public            System.Single                   fallOffCap  // 0xd0
  public            System.Single                   fallOffEndMultiplier  // 0xd4
  public            System.Boolean                  depthBlendCapOff  // 0xd8
  public            System.Single                   depthBlendDistance  // 0xdc
  public            System.Single                   cameraClippingDistance  // 0xe0
  public            System.Single                   glareFrontal  // 0xe4
  public            System.Single                   glareBehind  // 0xe8
  public            System.Single                   fresnelPow  // 0xec
  public            VLB.NoiseMode                   noiseMode  // 0xf0
  public            System.Single                   noiseIntensity  // 0xf4
  public            System.Boolean                  noiseScaleUseGlobal  // 0xf8
  public            System.Single                   noiseContrast  // 0xfc
  public            System.Boolean                  noiseScaleUseSeparateAxis  // 0x100
  public            UnityEngine.Vector3             noiseScalesLocal  // 0x104
  public            System.Single                   noiseScaleLocal  // 0x110
  public            System.Boolean                  noiseVelocityUseGlobal  // 0x114
  public            UnityEngine.Vector3             noiseVelocityLocal  // 0x118
  public            System.Boolean                  fadeOutFromLight  // 0x124
  public            System.Single                   fadeOutBeginSecond  // 0x128
  public            System.Single                   fadeOutEndSecond  // 0x12c
  public            System.Boolean                  useFog  // 0x130
  public            System.Single                   fogIntensity  // 0x134
  public            VLB.Dimensions                  dimensions  // 0x138
  public            UnityEngine.Vector2             tiltFactor  // 0x13c
  private           System.Int32                    pluginVersion  // 0x144
  private           System.Boolean                  _TrackChangesDuringPlaytime  // 0x148
  private           System.Int32                    _SortingLayerID  // 0x14c
  private           System.Int32                    _SortingOrder  // 0x150
  private           System.Single                   _FadeOutBegin  // 0x154
  private           System.Single                   _FadeOutEnd  // 0x158
  private           System.UInt32                   <_INTERNAL_InstancedMaterialGroupID>k__BackingField  // 0x15c
  public            VLB.BeamGeometry                m_BeamGeom  // 0x160
  public            System.Boolean                  matIsPersistent  // 0x168
  private           UnityEngine.Coroutine           m_CoPlaytimeUpdate  // 0x170
  private           UnityEngine.Vector3             m_CachedClippingPlanePosition  // 0x178
  private           UnityEngine.Quaternion          m_CachedClippingPlaneRotation  // 0x184
  private           System.Boolean                  m_HasCachedClippingPlanePose  // 0x194
PROPERTIES:
  usedColorMode  get=0x0B89A810
  useColorFromAttachedLightSpot  get=0x0B89A660
  useColorTemperatureFromAttachedLightSpot  get=0x0B89A6B8
  alphaInside  get=0x04D86400  set=0x04D86480
  alphaOutside  get=0x04D8D350  set=0x04D8D380
  intensityGlobal  get=0x04D8D350  set=0x04DBF620
  useIntensityFromAttachedLightSpot  get=0x0B89A760
  useSpotAngleFromAttachedLightSpot  get=0x0B89A7B8
  coneAngle  get=0x0B899A90
  coneRadiusEnd  get=0x0B899C50  set=0x0B89A844
  coneVolume  get=0x0B899CB0
  coneApexOffsetZ  get=0x0B899AE8
  coneApexPositionLocal  get=0x0B899C20
  coneApexPositionGlobal  get=0x0B899B58
  geomSides  get=0x0B899D4C  set=0x0B89A8F8
  geomSegments  get=0x0B899D20  set=0x0B89A898
  skewingLocalForwardDirectionNormalized  get=0x0B89A518
  canHaveMeshSkewing  get=0x02FFF600
  hasMeshSkewing  get=0x0B899DCC
  additionalClippingPlane  get=0x0B899588
  attenuationLerpLinearQuad  get=0x0B899748
  fadeStart  get=0x04DA62F0  set=0x04DA6320
  fadeEnd  get=0x04D9DF30  set=0x04D9DFF0
  fadeEndFromLight  get=0x04DA32F0  set=0x04DBF600
  lightSpotAttached  get=0x04D8D1D0
  useFallOffEndFromAttachedLightSpot  get=0x0B89A704
  maxGeometryDistance  get=0x0B89A004
  isNoiseEnabled  get=0x0B899EA8
  noiseEnabled  get=0x0B899EA8  set=0x0B89A958
  cullingDistance  get=0x0B899D00
  fadeOutBegin  get=0x04DBF5F0  set=0x04DBF610
  fadeOutEnd  get=0x04D90480  set=0x04D904C0
  isFadeOutEnabled  get=0x0B899E84
  isTilted  get=0x0B899EB4
  sortingLayerID  get=0x04DA7C70  set=0x0B89A978
  sortingLayerName  get=0x0B89A650  set=0x0B89A9FC
  sortingOrder  get=0x04DA24D0  set=0x0B89AA24
  trackChangesDuringPlaytime  get=0x04D8FE60  set=0x04DBF630
  isCurrentlyTrackingChanges  get=0x0B899E78
  hasGeometry  get=0x0B899D78
  bounds  get=0x0B899994
  blendingModeAsInt  get=0x0B8998E8
  beamInternalLocalRotation  get=0x0B899828
  beamLocalForward  get=0x0B8998AC
  beamGlobalForward  get=0x0B899770
  lossyScale  get=0x0B899F14
  raycastDistance  get=0x0B89A318
  raycastGlobalForward  get=0x0B89A3F4
  raycastGlobalUp  get=0x0B89A4C8
  raycastGlobalRight  get=0x0B89A478
  _INTERNAL_pluginVersion  get=0x04DBD7D0
  _INTERNAL_InstancedMaterialGroupID  get=0x04D904B0  set=0x04D904E0
  meshStats  get=0x0B89A034
  meshVerticesCount  get=0x0B89A26C
  meshTrianglesCount  get=0x0B89A1A8
METHODS:
  RVA=0x0B899358  token=0x60000A1  System.Void GetInsideAndOutsideIntensity(System.Single& inside, System.Single& outside)
  RVA=0x0B899288  token=0x60000D7  UnityEngine.Vector3 ComputeRaycastGlobalVector(UnityEngine.Vector3 localVec)
  RVA=0x04DBF5E0  token=0x60000DC  System.Void SetFadeOutValue(System.Single& propToChange, System.Single value)
  RVA=0x0B89937C  token=0x60000E2  System.Void .ctor()
END_CLASS

