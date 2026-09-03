// ========================================================
// Dumped by @desirepro
// Assembly: VolumetricLightBeam.dll
// Classes:  68
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000002  // size: 0x10
    public static class EditorPrefsStrings
    {
    }

    // TypeToken: 0x200000C  // size: 0x10
    public static class Help
    {
        // Fields
        private static System.String UrlBase;  // const
        private static System.String UrlSuffix;  // const
        public static System.String UrlBeam;  // const
        public static System.String UrlDustParticles;  // const
        public static System.String UrlDynamicOcclusionRaycasting;  // const
        public static System.String UrlDynamicOcclusionDepthBuffer;  // const
        public static System.String UrlTriggerZone;  // const
        public static System.String UrlSkewingHandle;  // const
        public static System.String UrlEffectFlicker;  // const
        public static System.String UrlEffectPulse;  // const
        public static System.String UrlConfig;  // const

    }

    // TypeToken: 0x200000D  // size: 0x10
    public static class Internal
    {
        // Properties
        UnityEngine.HideFlags ProceduralObjectsHideFlags { get; /* RVA: 0x011EC580 */ }

    }

    // TypeToken: 0x200000E  // size: 0x10
    public static class Beam
    {
        // Fields
        public static VLB.ColorMode ColorModeDefault;  // const
        public static System.Single MultiplierDefault;  // const
        public static System.Single MultiplierMin;  // const
        public static System.Single IntensityDefault;  // const
        public static System.Single IntensityMin;  // const
        public static System.Single HDRPExposureWeightDefault;  // const
        public static System.Single HDRPExposureWeightMin;  // const
        public static System.Single HDRPExposureWeightMax;  // const
        public static System.Single SpotAngleDefault;  // const
        public static System.Single SpotAngleMin;  // const
        public static System.Single SpotAngleMax;  // const
        public static System.Single ConeRadiusStart;  // const
        public static VLB.MeshType GeomMeshType;  // const
        public static System.Int32 GeomSidesDefault;  // const
        public static System.Int32 GeomSidesMin;  // const
        public static System.Int32 GeomSidesMax;  // const
        public static System.Int32 GeomSegmentsDefault;  // const
        public static System.Int32 GeomSegmentsMin;  // const
        public static System.Int32 GeomSegmentsMax;  // const
        public static System.Boolean GeomCap;  // const
        public static VLB.AttenuationEquation AttenuationEquationDefault;  // const
        public static System.Single AttenuationCustomBlendingDefault;  // const
        public static System.Single AttenuationCustomBlendingMin;  // const
        public static System.Single AttenuationCustomBlendingMax;  // const
        public static System.Single FallOffStart;  // const
        public static System.Single FallOffEnd;  // const
        public static System.Single FallOffDistancesMinThreshold;  // const
        public static System.Single DepthBlendDistance;  // const
        public static System.Single CameraClippingDistance;  // const
        public static System.Single FresnelPowMaxValue;  // const
        public static System.Single FresnelPow;  // const
        public static System.Single GlareFrontalDefault;  // const
        public static System.Single GlareBehindDefault;  // const
        public static System.Single GlareMin;  // const
        public static System.Single GlareMax;  // const
        public static VLB.NoiseMode NoiseModeDefault;  // const
        public static System.Single NoiseIntensityMin;  // const
        public static System.Single NoiseIntensityMax;  // const
        public static System.Single NoiseIntensityDefault;  // const
        public static System.Single NoiseScaleMin;  // const
        public static System.Single NoiseScaleMax;  // const
        public static readonly UnityEngine.Vector3 NoiseScalesDefault;  // static @ 0x0
        public static System.Single NoiseContrastDefault;  // const
        public static System.Single NoiseContrastMin;  // const
        public static System.Single NoiseContrastMax;  // const
        public static System.Single NoiseScaleDefault;  // const
        public static VLB.BlendingMode BlendingModeDefault;  // const
        public static VLB.ShaderAccuracy ShaderAccuracyDefault;  // const
        public static System.Single FadeOutBeginDefault;  // const
        public static System.Single FadeOutEndDefault;  // const
        public static System.Single FadeOutBeginSecondDefault;  // const
        public static System.Single FadeOutEndSecondDefault;  // const
        public static VLB.Dimensions DimensionsDefault;  // const
        public static UnityEngine.Transform ClippingPlaneTransformDefault;  // const
        public static readonly UnityEngine.Color FlatColor;  // static @ 0xc
        public static readonly UnityEngine.Vector3 NoiseVelocityDefault;  // static @ 0x1c
        public static readonly UnityEngine.Vector2 TiltDefault;  // static @ 0x28
        public static readonly UnityEngine.Vector3 SkewingLocalForwardDirectionDefault;  // static @ 0x30

        // Methods
        // RVA: 0x04B39200  token: 0x6000033
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000F  // size: 0x10
    public static class DustParticles
    {
        // Fields
        public static System.Single AlphaDefault;  // const
        public static System.Single SizeDefault;  // const
        public static VLB.ParticlesDirection DirectionDefault;  // const
        public static System.Single DensityDefault;  // const
        public static System.Single DensityMin;  // const
        public static System.Single DensityMax;  // const
        public static System.Boolean CullingEnabledDefault;  // const
        public static System.Single CullingMaxDistanceDefault;  // const
        public static System.Single CullingMaxDistanceMin;  // const
        public static readonly UnityEngine.Vector3 VelocityDefault;  // static @ 0x0
        public static readonly VLB.MinMaxRangeFloat SpawnDistanceRangeDefault;  // static @ 0xc

        // Methods
        // RVA: 0x0B897458  token: 0x6000034
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000010  // size: 0x10
    public static class DynOcclusion
    {
        // Fields
        public static System.Single FadeDistanceToSurfaceDefault;  // const
        public static VLB.DynamicOcclusionUpdateRate UpdateRateDefault;  // const
        public static System.Int32 WaitFramesCountDefault;  // const
        public static VLB.Dimensions RaycastingDimensionsDefault;  // const
        public static System.Boolean RaycastingConsiderTriggersDefault;  // const
        public static System.Single RaycastingMinOccluderAreaDefault;  // const
        public static System.Single RaycastingMinSurfaceRatioDefault;  // const
        public static System.Single RaycastingMinSurfaceRatioMin;  // const
        public static System.Single RaycastingMinSurfaceRatioMax;  // const
        public static System.Single RaycastingMaxSurfaceDotDefault;  // const
        public static System.Single RaycastingMaxSurfaceAngleMin;  // const
        public static System.Single RaycastingMaxSurfaceAngleMax;  // const
        public static VLB.PlaneAlignment RaycastingPlaneAlignmentDefault;  // const
        public static System.Single RaycastingPlaneOffsetDefault;  // const
        public static System.Int32 DepthBufferDepthMapResolutionDefault;  // const
        public static System.Boolean DepthBufferOcclusionCullingDefault;  // const
        public static readonly UnityEngine.LayerMask LayerMaskDefault;  // static @ 0x0

        // Methods
        // RVA: 0x0B8974C0  token: 0x6000035
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000011  // size: 0x10
    public static class Config
    {
        // Fields
        public static System.Boolean GeometryOverrideLayerDefault;  // const
        public static System.Int32 GeometryLayerIDDefault;  // const
        public static System.String GeometryTagDefault;  // const
        public static System.String FadeOutCameraTagDefault;  // const
        public static VLB.RenderQueue GeometryRenderQueueDefault;  // const
        public static VLB.RenderPipeline GeometryRenderPipelineDefault;  // const
        public static VLB.RenderingMode GeometryRenderingModeDefault;  // const
        public static System.Int32 Noise3DSizeDefault;  // const
        public static System.Int32 SharedMeshSides;  // const
        public static System.Int32 SharedMeshSegments;  // const
        public static System.Single DitheringFactor;  // const
        public static System.Boolean UseLightColorTemperatureDefault;  // const
        public static System.Boolean FeatureEnabledDefault;  // const
        public static VLB.FeatureEnabledColorGradient FeatureEnabledColorGradientDefault;  // const

    }

    // TypeToken: 0x2000023  // size: 0x14
    public sealed struct BlendingMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.MaterialManager.BlendingMode Additive;  // const
        public static VLB.MaterialManager.BlendingMode SoftAdditive;  // const
        public static VLB.MaterialManager.BlendingMode TraditionalTransparency;  // const
        public static VLB.MaterialManager.BlendingMode Count;  // const

    }

    // TypeToken: 0x2000024  // size: 0x14
    public sealed struct ColorGradient
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.MaterialManager.ColorGradient Off;  // const
        public static VLB.MaterialManager.ColorGradient MatrixLow;  // const
        public static VLB.MaterialManager.ColorGradient MatrixHigh;  // const
        public static VLB.MaterialManager.ColorGradient Count;  // const

    }

    // TypeToken: 0x2000025  // size: 0x14
    public sealed struct DepthBlend
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.MaterialManager.DepthBlend Off;  // const
        public static VLB.MaterialManager.DepthBlend On;  // const
        public static VLB.MaterialManager.DepthBlend Count;  // const

    }

    // TypeToken: 0x2000026  // size: 0x14
    public sealed struct DynamicOcclusion
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.MaterialManager.DynamicOcclusion Off;  // const
        public static VLB.MaterialManager.DynamicOcclusion ClippingPlane;  // const
        public static VLB.MaterialManager.DynamicOcclusion DepthTexture;  // const
        public static VLB.MaterialManager.DynamicOcclusion Count;  // const

    }

    // TypeToken: 0x2000027  // size: 0x14
    public sealed struct MeshSkewing
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.MaterialManager.MeshSkewing Off;  // const
        public static VLB.MaterialManager.MeshSkewing On;  // const
        public static VLB.MaterialManager.MeshSkewing Count;  // const

    }

    // TypeToken: 0x2000028  // size: 0x14
    public sealed struct Noise3D
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.MaterialManager.Noise3D Off;  // const
        public static VLB.MaterialManager.Noise3D On;  // const
        public static VLB.MaterialManager.Noise3D Count;  // const

    }

    // TypeToken: 0x2000029  // size: 0x14
    public sealed struct ShaderAccuracy
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.MaterialManager.ShaderAccuracy Fast;  // const
        public static VLB.MaterialManager.ShaderAccuracy High;  // const
        public static VLB.MaterialManager.ShaderAccuracy Count;  // const

    }

    // TypeToken: 0x200002A  // size: 0x30
    public sealed struct StaticProperties
    {
        // Fields
        public System.Boolean useFog;  // 0x10
        public VLB.MaterialManager.BlendingMode blendingMode;  // 0x14
        public VLB.MaterialManager.Noise3D noise3D;  // 0x18
        public VLB.MaterialManager.DepthBlend depthBlend;  // 0x1c
        public VLB.MaterialManager.ColorGradient colorGradient;  // 0x20
        public VLB.MaterialManager.DynamicOcclusion dynamicOcclusion;  // 0x24
        public VLB.MaterialManager.MeshSkewing meshSkewing;  // 0x28
        public VLB.MaterialManager.ShaderAccuracy shaderAccuracy;  // 0x2c

        // Properties
        System.Int32 blendingModeID { get; /* RVA: 0x04D88B20 */ }
        System.Int32 noise3DID { get; /* RVA: 0x0B8988F4 */ }
        System.Int32 depthBlendID { get; /* RVA: 0x0B8987B8 */ }
        System.Int32 colorGradientID { get; /* RVA: 0x0B898784 */ }
        System.Int32 dynamicOcclusionID { get; /* RVA: 0x0B8987EC */ }
        System.Int32 meshSkewingID { get; /* RVA: 0x0B8988C0 */ }
        System.Int32 shaderAccuracyID { get; /* RVA: 0x0B898928 */ }
        System.Int32 materialID { get; /* RVA: 0x0B898820 */ }

        // Methods
        // RVA: 0x0B898460  token: 0x6000043
        public System.Void ApplyToMaterial(UnityEngine.Material mat) { }

    }

    // TypeToken: 0x200002B  // size: 0x18
    public class MaterialsGroup
    {
        // Fields
        public readonly UnityEngine.Material[] materials;  // 0x10

        // Methods
        // RVA: 0x0B897B74  token: 0x6000044
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002D  // size: 0x80
    public sealed class Callback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x074FF394  token: 0x6000045
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x6000046
        public virtual System.Void Invoke(VLB.MaterialModifier.Interface owner) { }
        // RVA: 0x047FDD70  token: 0x6000047
        public virtual System.IAsyncResult BeginInvoke(VLB.MaterialModifier.Interface owner, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000048
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200002E
    public interface Interface
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000049
        public virtual System.Void SetMaterialProp(System.Int32 nameID, System.Single value) { }
        // RVA: -1  // abstract  token: 0x600004A
        public virtual System.Void SetMaterialProp(System.Int32 nameID, UnityEngine.Vector4 value) { }
        // RVA: -1  // abstract  token: 0x600004B
        public virtual System.Void SetMaterialProp(System.Int32 nameID, UnityEngine.Color value) { }
        // RVA: -1  // abstract  token: 0x600004C
        public virtual System.Void SetMaterialProp(System.Int32 nameID, UnityEngine.Matrix4x4 value) { }
        // RVA: -1  // abstract  token: 0x600004D
        public virtual System.Void SetMaterialProp(System.Int32 nameID, UnityEngine.Texture value) { }

    }

    // TypeToken: 0x2000033  // size: 0x1C
    public sealed struct Plane2D
    {
        // Fields
        public UnityEngine.Vector2 normal;  // 0x10
        public System.Single distance;  // 0x18

        // Methods
        // RVA: 0x04DBF500  token: 0x600005B
        public System.Single Distance(UnityEngine.Vector2 point) { }
        // RVA: 0x0B897D14  token: 0x600005C
        public UnityEngine.Vector2 ClosestPoint(UnityEngine.Vector2 pt) { }
        // RVA: 0x0B89807C  token: 0x600005D
        public UnityEngine.Vector2 Intersect(UnityEngine.Vector2 p1, UnityEngine.Vector2 p2) { }
        // RVA: 0x0B898060  token: 0x600005E
        public System.Boolean GetSide(UnityEngine.Vector2 point) { }
        // RVA: 0x0B897FA0  token: 0x600005F
        public static VLB.PolygonHelper.Plane2D FromPoints(UnityEngine.Vector3 p1, UnityEngine.Vector3 p2) { }
        // RVA: 0x0B897F2C  token: 0x6000060
        public static VLB.PolygonHelper.Plane2D FromNormalAndPoint(UnityEngine.Vector3 normalizedNormal, UnityEngine.Vector3 p1) { }
        // RVA: 0x0B897F04  token: 0x6000061
        public System.Void Flip() { }
        // RVA: 0x0B897D58  token: 0x6000062
        public UnityEngine.Vector2[] CutConvex(UnityEngine.Vector2[] poly) { }
        // RVA: 0x0B898148  token: 0x6000063
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000038  // size: 0x14
    public sealed struct RenderPipeline
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.SRPHelper.RenderPipeline HGRP;  // const
        public static VLB.SRPHelper.RenderPipeline Undefined;  // const
        public static VLB.SRPHelper.RenderPipeline BuiltIn;  // const
        public static VLB.SRPHelper.RenderPipeline URP;  // const
        public static VLB.SRPHelper.RenderPipeline LWRP;  // const
        public static VLB.SRPHelper.RenderPipeline HDRP;  // const

    }

    // TypeToken: 0x200003A  // size: 0x38
    public sealed struct Packed
    {
        // Fields
        public UnityEngine.Vector3 position;  // 0x10
        public UnityEngine.Quaternion rotation;  // 0x1c
        public UnityEngine.Vector3 lossyScale;  // 0x2c

        // Methods
        // RVA: 0x0B897BE4  token: 0x6000070
        public System.Boolean IsSame(UnityEngine.Transform transf) { }

    }

    // TypeToken: 0x200003C  // size: 0x14
    public sealed struct FloatPackingPrecision
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.Utils.FloatPackingPrecision High;  // const
        public static VLB.Utils.FloatPackingPrecision Low;  // const
        public static VLB.Utils.FloatPackingPrecision Undef;  // const

    }

    // TypeToken: 0x200003F  // size: 0x14
    public sealed struct AttachedLightType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.VolumetricLightBeam.AttachedLightType NoLight;  // const
        public static VLB.VolumetricLightBeam.AttachedLightType OtherLight;  // const
        public static VLB.VolumetricLightBeam.AttachedLightType SpotLight;  // const

    }

    // TypeToken: 0x2000041  // size: 0x1C
    public sealed struct __StaticArrayInitTypeSize=12
    {
    }

    // TypeToken: 0x2000040  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=12 0A0EC6D4742068B4D88C6145B8224EF1DC240C8A305CDFC50C3AAF9121E6875D;  // static @ 0x0
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=12 F186F2262AE48F2AA4F90C9A6B35913B0F6B0B895423B6267252259BFD357D3B;  // static @ 0xc

    }

namespace Beyond.SourceGenerator
{

    // TypeToken: 0x2000004  // size: 0x20
    public class DataNAttribute : System.Attribute
    {
        // Fields
        public System.Int32 capacity;  // 0x10
        public System.Int32 tSize;  // 0x14
        public System.Boolean ring;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000002
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000005  // size: 0x10
    public static class AnimatorBlackboardUtility
    {
        // Fields
        private static System.Boolean s_enableFastPathBuffer;  // static @ 0x0

        // Methods
        // RVA: -1  // generic def  token: 0x6000003
        private static System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity) { }

    }

    // TypeToken: 0x2000006  // size: 0x10
    public class AnimatorBlackboardAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000004
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x18
    public class ECSComponentAttribute : System.Attribute
    {
        // Fields
        public System.Runtime.InteropServices.LayoutKind layoutKind;  // 0x10
        public System.Boolean isTag;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x6000005
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000008  // size: 0x10
    public class CameraControlConfigAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000006
        public System.Void .ctor() { }

    }

}

namespace HG.Rendering.Runtime
{

    // TypeToken: 0x2000003  // size: 0x10
    public class HGConstantBufferLayoutAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000001
        public System.Void .ctor() { }

    }

}

namespace IFix
{

    // TypeToken: 0x2000042  // size: 0x28
    public class ILFixDynamicMethodWrapper
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10
        private System.Int32 methodId;  // 0x18
        private System.Object anonObj;  // 0x20
        public static IFix.ILFixDynamicMethodWrapper[] wrapperArray;  // static @ 0x0

        // Methods
        // RVA: 0x032B2C30  token: 0x60000E3
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj) { }
        // RVA: 0x0B8974FC  token: 0x60000E4
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000043  // size: 0x40
    public class ILFixInterfaceBridge : IFix.Core.AnonymousStorey
    {
        // Methods
        // RVA: 0x0B897560  token: 0x60000E5
        public System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x041E1670  token: 0x60000E6
        public System.Void RefAsyncBuilderStartMethod() { }

    }

    // TypeToken: 0x2000044  // size: 0x18
    public class WrappersManagerImpl : IFix.Core.WrappersManager
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x60000E7
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x0B89AC90  token: 0x60000E8
        public static IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id) { }
        // RVA: 0x0B89AD80  token: 0x60000E9
        public static System.Boolean IsPatched(System.Int32 id) { }
        // RVA: 0x0B89AB4C  token: 0x60000EA
        public virtual System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon) { }
        // RVA: 0x0B89AC18  token: 0x60000EB
        public virtual System.Object CreateWrapper(System.Int32 id) { }
        // RVA: 0x0B89ACF4  token: 0x60000EC
        public virtual System.Object InitWrapperArray(System.Int32 len) { }
        // RVA: 0x0B89AAA8  token: 0x60000ED
        public virtual IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine) { }

    }

}

namespace VLB
{

    // TypeToken: 0x2000009  // size: 0x98
    public class BeamGeometry : UnityEngine.MonoBehaviour, Interface
    {
        // Fields
        private VLB.VolumetricLightBeam m_Master;  // 0x18
        private UnityEngine.Matrix4x4 m_ColorGradientMatrix;  // 0x20
        private VLB.MeshType m_CurrentMeshType;  // 0x60
        private VLB.MaterialModifier.Callback m_MaterialModifierCallback;  // 0x68
        private UnityEngine.Coroutine m_CoFadeOut;  // 0x70
        private UnityEngine.MeshRenderer <meshRenderer>k__BackingField;  // 0x78
        private UnityEngine.MeshFilter <meshFilter>k__BackingField;  // 0x80
        private UnityEngine.Mesh <coneMesh>k__BackingField;  // 0x88
        public UnityEngine.Material m_CustomMaterial;  // 0x90

        // Properties
        UnityEngine.MeshRenderer meshRenderer { get; /* RVA: 0x04D862A0 */ set; /* RVA: 0x06402320 */ }
        UnityEngine.MeshFilter meshFilter { get; /* RVA: 0x04D86260 */ set; /* RVA: 0x04CD9970 */ }
        UnityEngine.Mesh coneMesh { get; /* RVA: 0x04D861F0 */ set; /* RVA: 0x05395D30 */ }
        System.Boolean visible { get; /* RVA: 0x0B896EA0 */ set; /* RVA: 0x0B896F00 */ }
        System.Int32 sortingLayerID { get; /* RVA: 0x0B896E60 */ set; /* RVA: 0x0B896EC0 */ }
        System.Int32 sortingOrder { get; /* RVA: 0x0B896E80 */ set; /* RVA: 0x0B896EE0 */ }
        System.Boolean _INTERNAL_IsFadeOutCoroutineRunning { get; /* RVA: 0x09B02DAC */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000014
        public System.Void RestartFadeOutCoroutine() { }
        // RVA: 0x041E1670  token: 0x6000015
        public System.Void StopFadeOutCoroutine() { }
        // RVA: 0x0B896D94  token: 0x6000016
        public virtual System.Void SetMaterialProp(System.Int32 nameID, System.Single value) { }
        // RVA: 0x0B896B10  token: 0x6000017
        public virtual System.Void SetMaterialProp(System.Int32 nameID, UnityEngine.Vector4 value) { }
        // RVA: 0x0B896BF0  token: 0x6000018
        public virtual System.Void SetMaterialProp(System.Int32 nameID, UnityEngine.Color value) { }
        // RVA: 0x0B8969F8  token: 0x6000019
        public virtual System.Void SetMaterialProp(System.Int32 nameID, UnityEngine.Matrix4x4 value) { }
        // RVA: 0x0B896CD0  token: 0x600001A
        public virtual System.Void SetMaterialProp(System.Int32 nameID, UnityEngine.Texture value) { }
        // RVA: 0x05393520  token: 0x600001B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000A  // size: 0xB8
    public class Config : UnityEngine.ScriptableObject
    {
        // Fields
        public static System.String ClassName;  // const
        public static System.String kAssetName;  // const
        public static System.String kAssetNameExt;  // const
        private static VLB.Config ms_Instance;  // static @ 0x0
        public UnityEngine.Mesh sharedMesh;  // 0x18
        public System.Boolean featureEnabledDustParticles;  // 0x20
        public System.Boolean featureEnabledEffect;  // 0x21
        public System.Boolean featureEnabledTriggerZone;  // 0x22
        public System.Boolean geometryOverrideLayer;  // 0x23
        public System.Int32 geometryLayerID;  // 0x24
        public System.String geometryTag;  // 0x28
        public System.Int32 geometryRenderQueue;  // 0x30
        private VLB.RenderPipeline _RenderPipeline;  // 0x34
        private VLB.RenderingMode _RenderingMode;  // 0x38
        public System.Single ditheringFactor;  // 0x3c
        public System.Boolean useLightColorTemperature;  // 0x40
        public System.Int32 sharedMeshSides;  // 0x44
        public System.Int32 sharedMeshSegments;  // 0x48
        public UnityEngine.Vector3 globalNoisesScale;  // 0x4c
        public UnityEngine.Vector3 globalNoiseVelocity;  // 0x58
        public System.String fadeOutCameraTag;  // 0x68
        public UnityEngine.Texture3D noiseTexture3D;  // 0x70
        public UnityEngine.ParticleSystem dustParticlesPrefab;  // 0x78
        public UnityEngine.Texture2D ditheringNoiseTexture;  // 0x80
        public VLB.FeatureEnabledColorGradient featureEnabledColorGradient;  // 0x88
        public System.Boolean featureEnabledDepthBlend;  // 0x8c
        public System.Boolean featureEnabledNoise3D;  // 0x8d
        public System.Boolean featureEnabledDynamicOcclusion;  // 0x8e
        public System.Boolean featureEnabledMeshSkewing;  // 0x8f
        public System.Boolean featureEnabledShaderAccuracyHigh;  // 0x90
        private UnityEngine.Shader _BeamShader;  // 0x98
        private UnityEngine.Transform m_CachedFadeOutCamera;  // 0xa0
        private System.Int32 pluginVersion;  // 0xa8
        private UnityEngine.Material _DummyMaterial;  // 0xb0

        // Properties
        VLB.RenderPipeline renderPipeline { get; /* RVA: 0x04D86730 */ }
        VLB.RenderingMode renderingMode { get; /* RVA: 0x04D863B0 */ }
        VLB.RenderingMode actualRenderingMode { get; /* RVA: 0x0B8973B0 */ }
        System.Boolean useSinglePassShader { get; /* RVA: 0x0B897440 */ }
        System.Boolean requiresDoubleSidedMesh { get; /* RVA: 0x0B897440 */ }
        UnityEngine.Shader beamShader { get; /* RVA: 0x04D85EE0 */ }
        UnityEngine.Transform fadeOutCameraTransform { get; /* RVA: 0x0B8973CC */ }
        System.Boolean isHDRPExposureWeightSupported { get; /* RVA: 0x0B897438 */ }
        System.Boolean hasRenderPipelineMismatch { get; /* RVA: 0x037E2870 */ }
        VLB.Config Instance { get; /* RVA: 0x05393938 */ }

        // Methods
        // RVA: 0x0B8972AC  token: 0x6000026
        public System.Void Reset() { }
        // RVA: 0x04D83220  token: 0x6000027
        private System.Void OnEnable() { }
        // RVA: 0x012081B0  token: 0x6000028
        public System.Boolean IsSRPBatcherSupported() { }
        // RVA: 0x0B896F7C  token: 0x6000029
        public System.Void ForceUpdateFadeOutCamera() { }
        // RVA: 0x037E27D0  token: 0x600002A
        private static System.Void OnStartup() { }
        // RVA: 0x037E2DC0  token: 0x600002B
        private System.Void RefreshGlobalShaderProperties() { }
        // RVA: 0x0B897138  token: 0x600002C
        public System.Void ResetInternalData() { }
        // RVA: 0x0B897000  token: 0x600002D
        public UnityEngine.ParticleSystem NewVolumetricDustParticles() { }
        // RVA: 0x04D83230  token: 0x600002E
        private System.Void HandleBackwardCompatibility(System.Int32 serializedVersion, System.Int32 newVersion) { }
        // RVA: 0x0496FF50  token: 0x600002F
        private static VLB.Config LoadAssetInternal(System.String assetName) { }
        // RVA: 0x0496FD70  token: 0x6000030
        private static VLB.Config GetInstance(System.Boolean assertIfNotFound) { }
        // RVA: 0x04B698D0  token: 0x6000031
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000B  // size: 0x10
    public static class Consts
    {
        // Fields
        public static System.String PluginFolder;  // const

    }

    // TypeToken: 0x2000012  // size: 0x10
    public static class DummyMaterial
    {
    }

    // TypeToken: 0x2000013  // size: 0x14
    public sealed struct FeatureEnabledColorGradient
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.FeatureEnabledColorGradient Off;  // const
        public static VLB.FeatureEnabledColorGradient HighOnly;  // const
        public static VLB.FeatureEnabledColorGradient HighAndLow;  // const

    }

    // TypeToken: 0x2000014  // size: 0x14
    public sealed struct ColorMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.ColorMode Flat;  // const
        public static VLB.ColorMode Gradient;  // const

    }

    // TypeToken: 0x2000015  // size: 0x14
    public sealed struct AttenuationEquation
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.AttenuationEquation Linear;  // const
        public static VLB.AttenuationEquation Quadratic;  // const
        public static VLB.AttenuationEquation Blend;  // const

    }

    // TypeToken: 0x2000016  // size: 0x14
    public sealed struct BlendingMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.BlendingMode Additive;  // const
        public static VLB.BlendingMode SoftAdditive;  // const
        public static VLB.BlendingMode TraditionalTransparency;  // const

    }

    // TypeToken: 0x2000017  // size: 0x14
    public sealed struct ShaderAccuracy
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.ShaderAccuracy Fast;  // const
        public static VLB.ShaderAccuracy High;  // const

    }

    // TypeToken: 0x2000018  // size: 0x14
    public sealed struct NoiseMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.NoiseMode Disabled;  // const
        public static VLB.NoiseMode WorldSpace;  // const
        public static VLB.NoiseMode LocalSpace;  // const

    }

    // TypeToken: 0x2000019  // size: 0x14
    public sealed struct MeshType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.MeshType Shared;  // const
        public static VLB.MeshType Custom;  // const

    }

    // TypeToken: 0x200001A  // size: 0x14
    public sealed struct RenderPipeline
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.RenderPipeline HGRP;  // const
        public static VLB.RenderPipeline BuiltIn;  // const
        public static VLB.RenderPipeline URP;  // const
        public static VLB.RenderPipeline HDRP;  // const

    }

    // TypeToken: 0x200001B  // size: 0x14
    public sealed struct RenderingMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.RenderingMode MultiPass;  // const
        public static VLB.RenderingMode SinglePass;  // const
        public static VLB.RenderingMode GPUInstancing;  // const
        public static VLB.RenderingMode SRPBatcher;  // const

    }

    // TypeToken: 0x200001C  // size: 0x14
    public sealed struct RenderQueue
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.RenderQueue Custom;  // const
        public static VLB.RenderQueue Background;  // const
        public static VLB.RenderQueue Geometry;  // const
        public static VLB.RenderQueue AlphaTest;  // const
        public static VLB.RenderQueue GeometryLast;  // const
        public static VLB.RenderQueue Transparent;  // const
        public static VLB.RenderQueue Overlay;  // const

    }

    // TypeToken: 0x200001D  // size: 0x14
    public sealed struct Dimensions
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.Dimensions Dim3D;  // const
        public static VLB.Dimensions Dim2D;  // const

    }

    // TypeToken: 0x200001E  // size: 0x14
    public sealed struct PlaneAlignment
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.PlaneAlignment Surface;  // const
        public static VLB.PlaneAlignment Beam;  // const

    }

    // TypeToken: 0x200001F  // size: 0x14
    public sealed struct DynamicOcclusionUpdateRate
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.DynamicOcclusionUpdateRate Never;  // const
        public static VLB.DynamicOcclusionUpdateRate OnEnable;  // const
        public static VLB.DynamicOcclusionUpdateRate OnBeamMove;  // const
        public static VLB.DynamicOcclusionUpdateRate EveryXFrames;  // const
        public static VLB.DynamicOcclusionUpdateRate OnBeamMoveAndEveryXFrames;  // const

    }

    // TypeToken: 0x2000020  // size: 0x14
    public sealed struct ParticlesDirection
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static VLB.ParticlesDirection Random;  // const
        public static VLB.ParticlesDirection LocalSpace;  // const
        public static VLB.ParticlesDirection WorldSpace;  // const

    }

    // TypeToken: 0x2000021  // size: 0x10
    public sealed class HighlightNullAttribute : UnityEngine.PropertyAttribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000036
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000022  // size: 0x10
    public static class MaterialManager
    {
        // Fields
        public static System.Int32 staticPropertiesCount;  // const
        public static UnityEngine.MaterialPropertyBlock materialPropertyBlock;  // static @ 0x0
        private static readonly UnityEngine.Rendering.BlendMode[] BlendingMode_SrcFactor;  // static @ 0x8
        private static readonly UnityEngine.Rendering.BlendMode[] BlendingMode_DstFactor;  // static @ 0x10
        private static readonly System.Boolean[] BlendingMode_AlphaAsBlack;  // static @ 0x18
        private static readonly System.Collections.Hashtable ms_MaterialsGroup;  // static @ 0x20

        // Methods
        // RVA: 0x0B8978A4  token: 0x6000037
        public static UnityEngine.Material NewMaterialTransient(System.Boolean gpuInstanced) { }
        // RVA: 0x0B8977E8  token: 0x6000038
        public static UnityEngine.Material NewMaterialPersistent(UnityEngine.Shader shader, System.Boolean gpuInstanced) { }
        // RVA: 0x0B8975F4  token: 0x6000039
        public static UnityEngine.Material GetInstancedMaterial(System.UInt32 groupID, VLB.MaterialManager.StaticProperties& staticProps) { }
        // RVA: 0x0B897964  token: 0x600003A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200002C  // size: 0x10
    public static class MaterialModifier
    {
    }

    // TypeToken: 0x200002F  // size: 0x18
    public sealed struct MinMaxRangeFloat
    {
        // Fields
        private System.Single m_MinValue;  // 0x10
        private System.Single m_MaxValue;  // 0x14

        // Properties
        System.Single minValue { get; /* RVA: 0x04D871D0 */ }
        System.Single maxValue { get; /* RVA: 0x04D9C560 */ }
        System.Single randomValue { get; /* RVA: 0x0B897BD0 */ }
        UnityEngine.Vector2 asVector2 { get; /* RVA: 0x04D889F0 */ }

        // Methods
        // RVA: 0x04D87A10  token: 0x600004E
        public System.Void .ctor(System.Single min, System.Single max) { }
        // RVA: 0x0B897BBC  token: 0x6000053
        public System.Single GetLerpedValue(System.Single lerp01) { }

    }

    // TypeToken: 0x2000030  // size: 0x18
    public class MinMaxRangeAttribute : System.Attribute
    {
        // Fields
        private readonly System.Single <minValue>k__BackingField;  // 0x10
        private readonly System.Single <maxValue>k__BackingField;  // 0x14

        // Properties
        System.Single minValue { get; /* RVA: 0x04D85F70 */ }
        System.Single maxValue { get; /* RVA: 0x04D88D40 */ }

        // Methods
        // RVA: 0x04D8D120  token: 0x6000054
        public System.Void .ctor(System.Single min, System.Single max) { }

    }

    // TypeToken: 0x2000031  // size: 0x10
    public class PlatformHelper
    {
        // Methods
        // RVA: 0x0496FF90  token: 0x6000057
        public static System.String GetCurrentPlatformSuffix() { }
        // RVA: 0x0496FFB0  token: 0x6000058
        private static System.String GetPlatformSuffix(UnityEngine.RuntimePlatform platform) { }
        // RVA: 0x041E1670  token: 0x6000059
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000032  // size: 0x18
    public class PolygonHelper : UnityEngine.MonoBehaviour
    {
        // Methods
        // RVA: 0x05393520  token: 0x600005A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000034  // size: 0x10
    public static class ShaderKeywords
    {
        // Fields
        public static System.String UseFog;  // const
        public static System.String AlphaAsBlack;  // const
        public static System.String ColorGradientMatrixLow;  // const
        public static System.String ColorGradientMatrixHigh;  // const
        public static System.String DepthBlend;  // const
        public static System.String Noise3D;  // const
        public static System.String OcclusionClippingPlane;  // const
        public static System.String OcclusionDepthTexture;  // const
        public static System.String MeshSkewing;  // const
        public static System.String ShaderAccuracyHigh;  // const

    }

    // TypeToken: 0x2000035  // size: 0x10
    public static class ShaderProperties
    {
        // Fields
        public static readonly System.Int32 ConeParams0;  // static @ 0x0
        public static readonly System.Int32 ConeParams1;  // static @ 0x4
        public static readonly System.Int32 TiltVectorX;  // static @ 0x8
        public static readonly System.Int32 TiltVectorY;  // static @ 0xc
        public static readonly System.Int32 DistanceFadeStart;  // static @ 0x10
        public static readonly System.Int32 DistanceFadeEnd;  // static @ 0x14
        public static readonly System.Int32 DistanceFadeStartSecond;  // static @ 0x18
        public static readonly System.Int32 DistanceFadeEndSecond;  // static @ 0x1c
        public static readonly System.Int32 NoiseContrast;  // static @ 0x20
        public static readonly System.Int32 NoiseVelocity;  // static @ 0x24
        public static readonly System.Int32 NoiseScale;  // static @ 0x28
        public static readonly System.Int32 DepthBlendCapOff;  // static @ 0x2c
        public static readonly System.Int32 BlendMode;  // static @ 0x30
        public static readonly System.Int32 FogIntensity;  // static @ 0x34
        public static readonly System.Int32 ClippingPlaneTransition;  // static @ 0x38
        public static readonly System.Int32 UseClippingPlane;  // static @ 0x3c
        public static readonly System.Int32 FadeOutFactor;  // static @ 0x40
        public static readonly System.Int32 ConeSlopeCosSin;  // static @ 0x44
        public static readonly System.Int32 ConeRadius;  // static @ 0x48
        public static readonly System.Int32 ConeGeomProps;  // static @ 0x4c
        public static readonly System.Int32 ColorFlat;  // static @ 0x50
        public static readonly System.Int32 AlphaInside;  // static @ 0x54
        public static readonly System.Int32 AlphaOutside;  // static @ 0x58
        public static readonly System.Int32 HDRPExposureWeight;  // static @ 0x5c
        public static readonly System.Int32 AttenuationLerpLinearQuad;  // static @ 0x60
        public static readonly System.Int32 DistanceFallOff;  // static @ 0x64
        public static readonly System.Int32 DistanceCamClipping;  // static @ 0x68
        public static readonly System.Int32 FresnelPow;  // static @ 0x6c
        public static readonly System.Int32 GlareBehind;  // static @ 0x70
        public static readonly System.Int32 GlareFrontal;  // static @ 0x74
        public static readonly System.Int32 DrawCap;  // static @ 0x78
        public static readonly System.Int32 DepthBlendDistance;  // static @ 0x7c
        public static readonly System.Int32 NoiseVelocityAndScale;  // static @ 0x80
        public static readonly System.Int32 NoiseParam;  // static @ 0x84
        public static readonly System.Int32 CameraParams;  // static @ 0x88
        public static readonly System.Int32 ColorGradientMatrix;  // static @ 0x8c
        public static readonly System.Int32 LocalToWorldMatrix;  // static @ 0x90
        public static readonly System.Int32 WorldToLocalMatrix;  // static @ 0x94
        public static readonly System.Int32 BlendSrcFactor;  // static @ 0x98
        public static readonly System.Int32 BlendDstFactor;  // static @ 0x9c
        public static readonly System.Int32 DynamicOcclusionClippingPlaneWS;  // static @ 0xa0
        public static readonly System.Int32 DynamicOcclusionClippingPlaneProps;  // static @ 0xa4
        public static readonly System.Int32 DynamicOcclusionDepthTexture;  // static @ 0xa8
        public static readonly System.Int32 DynamicOcclusionDepthProps;  // static @ 0xac
        public static readonly System.Int32 LocalForwardDirection;  // static @ 0xb0
        public static readonly System.Int32 TiltVector;  // static @ 0xb4
        public static readonly System.Int32 AdditionalClippingPlaneWS;  // static @ 0xb8
        public static readonly System.Int32 ParticlesTintColor;  // static @ 0xbc
        public static readonly System.Int32 GlobalUsesReversedZBuffer;  // static @ 0xc0
        public static readonly System.Int32 GlobalNoiseTex3D;  // static @ 0xc4
        public static readonly System.Int32 GlobalNoiseCustomTime;  // static @ 0xc8
        public static readonly System.Int32 GlobalDitheringFactor;  // static @ 0xcc
        public static readonly System.Int32 GlobalDitheringNoiseTex;  // static @ 0xd0

        // Methods
        // RVA: 0x04654190  token: 0x6000064
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000036  // size: 0x10
    public static class SpotLightHelper
    {
        // Methods
        // RVA: 0x0B8982A0  token: 0x6000065
        public static System.Void GetCullingDistance(UnityEngine.Light light, System.Single& cullingDistance, System.Single& falloffDistance) { }
        // RVA: 0x0B898390  token: 0x6000066
        public static System.Single GetIntensity(UnityEngine.Light light) { }
        // RVA: 0x0B8983F8  token: 0x6000067
        public static System.Single GetSpotAngle(UnityEngine.Light light) { }
        // RVA: 0x0B898328  token: 0x6000068
        public static System.Single GetFallOffEnd(UnityEngine.Light light) { }

    }

    // TypeToken: 0x2000037  // size: 0x10
    public static class SRPHelper
    {
        // Fields
        private static VLB.SRPHelper.RenderPipeline m_RenderPipelineCached;  // static @ 0x0

        // Properties
        VLB.SRPHelper.RenderPipeline renderPipelineType { get; /* RVA: 0x037E28E0 */ }

        // Methods
        // RVA: 0x011EC580  token: 0x600006A
        private static VLB.SRPHelper.RenderPipeline ComputeRenderPipeline() { }
        // RVA: 0x02FFF600  token: 0x600006B
        public static System.Boolean IsUsingCustomRenderPipeline() { }
        // RVA: 0x0B898208  token: 0x600006C
        public static System.Void RegisterOnBeginCameraRendering(System.Action<UnityEngine.Rendering.ScriptableRenderContext,UnityEngine.Camera> cb) { }
        // RVA: 0x0B898254  token: 0x600006D
        public static System.Void UnregisterOnBeginCameraRendering(System.Action<UnityEngine.Rendering.ScriptableRenderContext,UnityEngine.Camera> cb) { }
        // RVA: 0x04D79C10  token: 0x600006E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000039  // size: 0x10
    public static class TransformUtils
    {
        // Methods
        // RVA: 0x0B89895C  token: 0x600006F
        public static VLB.TransformUtils.Packed GetWorldPacked(UnityEngine.Transform self) { }

    }

    // TypeToken: 0x200003B  // size: 0x10
    public static class Utils
    {
        // Fields
        private static System.Single kEpsilon;  // const
        private static System.Int32 kFloatPackingHighMinShaderLevel;  // const
        private static VLB.Utils.FloatPackingPrecision ms_FloatPackingPrecision;  // static @ 0x0

        // Methods
        // RVA: 0x04DBF550  token: 0x6000071
        public static System.Single ComputeConeRadiusEnd(System.Single fallOffEnd, System.Single spotAngle) { }
        // RVA: 0x04DBF590  token: 0x6000072
        public static System.Single ComputeSpotAngle(System.Single fallOffEnd, System.Single coneRadiusEnd) { }
        // RVA: -1  // generic def  token: 0x6000073
        public static System.Void Swap(T& a, T& b) { }
        // RVA: 0x0B898D3C  token: 0x6000074
        public static System.String GetPath(UnityEngine.Transform current) { }
        // RVA: -1  // generic def  token: 0x6000075
        public static T NewWithComponent(System.String name) { }
        // RVA: -1  // generic def  token: 0x6000076
        public static T GetOrAddComponent(UnityEngine.GameObject self) { }
        // RVA: -1  // generic def  token: 0x6000077
        public static T GetOrAddComponent(UnityEngine.MonoBehaviour self) { }
        // RVA: -1  // generic def  token: 0x6000078
        public static System.Void ForeachComponentsOnlyInChildren(UnityEngine.GameObject self, System.Action<T> lambda, System.Boolean includeInactive) { }
        // RVA: 0x0B898E5C  token: 0x6000079
        public static System.Boolean HasFlag(System.Enum mask, System.Enum flags) { }
        // RVA: 0x04D889F0  token: 0x600007A
        public static UnityEngine.Vector2 xy(UnityEngine.Vector3 aVector) { }
        // RVA: 0x04D88A40  token: 0x600007B
        public static UnityEngine.Vector2 xz(UnityEngine.Vector3 aVector) { }
        // RVA: 0x04D8B880  token: 0x600007C
        public static UnityEngine.Vector2 yz(UnityEngine.Vector3 aVector) { }
        // RVA: 0x04DB4770  token: 0x600007D
        public static UnityEngine.Vector2 yx(UnityEngine.Vector3 aVector) { }
        // RVA: 0x04DB5A80  token: 0x600007E
        public static UnityEngine.Vector2 zx(UnityEngine.Vector3 aVector) { }
        // RVA: 0x04DB5BE0  token: 0x600007F
        public static UnityEngine.Vector2 zy(UnityEngine.Vector3 aVector) { }
        // RVA: 0x06935188  token: 0x6000080
        public static System.Boolean Approximately(System.Single a, System.Single b, System.Single epsilon) { }
        // RVA: 0x0B8989E8  token: 0x6000081
        public static System.Boolean Approximately(UnityEngine.Vector2 a, UnityEngine.Vector2 b, System.Single epsilon) { }
        // RVA: 0x0B898A1C  token: 0x6000082
        public static System.Boolean Approximately(UnityEngine.Vector3 a, UnityEngine.Vector3 b, System.Single epsilon) { }
        // RVA: 0x0B898A80  token: 0x6000083
        public static System.Boolean Approximately(UnityEngine.Vector4 a, UnityEngine.Vector4 b, System.Single epsilon) { }
        // RVA: 0x04DBF530  token: 0x6000084
        public static UnityEngine.Vector4 AsVector4(UnityEngine.Vector3 vec3, System.Single w) { }
        // RVA: 0x0B898FF4  token: 0x6000085
        public static UnityEngine.Vector4 PlaneEquation(UnityEngine.Vector3 normalizedNormal, UnityEngine.Vector3 pt) { }
        // RVA: 0x0B898E00  token: 0x6000086
        public static System.Single GetVolumeCubic(UnityEngine.Bounds self) { }
        // RVA: 0x0B898C90  token: 0x6000087
        public static System.Single GetMaxArea2D(UnityEngine.Bounds self) { }
        // RVA: 0x04DBF5C0  token: 0x6000088
        public static UnityEngine.Color Opaque(UnityEngine.Color self) { }
        // RVA: 0x0B898B7C  token: 0x6000089
        public static UnityEngine.Color ComputeComplementaryColor(UnityEngine.Color self, System.Boolean blackAndWhite) { }
        // RVA: 0x0B8991F4  token: 0x600008A
        public static UnityEngine.Plane TranslateCustom(UnityEngine.Plane plane, UnityEngine.Vector3 translation) { }
        // RVA: 0x0B898AD4  token: 0x600008B
        public static UnityEngine.Vector3 ClosestPointOnPlaneCustom(UnityEngine.Plane plane, UnityEngine.Vector3 point) { }
        // RVA: 0x0B898ED8  token: 0x600008C
        public static System.Boolean IsAlmostZero(System.Single f) { }
        // RVA: 0x0B898EF0  token: 0x600008D
        public static System.Boolean IsValid(UnityEngine.Plane plane) { }
        // RVA: 0x0690914C  token: 0x600008E
        public static System.Void SetKeywordEnabled(UnityEngine.Material mat, System.String name, System.Boolean enabled) { }
        // RVA: 0x0B8991D0  token: 0x600008F
        public static System.Void SetShaderKeywordEnabled(System.String name, System.Boolean enabled) { }
        // RVA: 0x0B89912C  token: 0x6000090
        public static UnityEngine.Matrix4x4 SampleInMatrix(UnityEngine.Gradient self, System.Int32 floatPackingPrecision) { }
        // RVA: 0x0B899068  token: 0x6000091
        public static UnityEngine.Color[] SampleInArray(UnityEngine.Gradient self, System.Int32 samplesCount) { }
        // RVA: 0x04DAFA70  token: 0x6000092
        private static UnityEngine.Vector4 Vector4_Floor(UnityEngine.Vector4 vec) { }
        // RVA: 0x0B898F20  token: 0x6000093
        public static System.Single PackToFloat(UnityEngine.Color color, System.Int32 floatPackingPrecision) { }
        // RVA: 0x0B898C20  token: 0x6000094
        public static VLB.Utils.FloatPackingPrecision GetFloatPackingPrecision() { }
        // RVA: 0x041E1670  token: 0x6000095
        public static System.Void MarkCurrentSceneDirty() { }
        // RVA: 0x041E1670  token: 0x6000096
        public static System.Void MarkObjectDirty(UnityEngine.Object obj) { }

    }

    // TypeToken: 0x200003D  // size: 0x10
    public static class Version
    {
        // Fields
        public static System.Int32 Current;  // const

    }

    // TypeToken: 0x200003E  // size: 0x198
    public class VolumetricLightBeam : UnityEngine.MonoBehaviour
    {
        // Fields
        public UnityEngine.PlatformLayers platformLayers;  // 0x18
        public static System.String ClassName;  // const
        public System.Boolean colorFromLight;  // 0x1c
        public VLB.ColorMode colorMode;  // 0x20
        public UnityEngine.Color color;  // 0x24
        public UnityEngine.Gradient colorGradient;  // 0x38
        public System.Boolean intensityFromLight;  // 0x40
        public System.Boolean intensityModeAdvanced;  // 0x41
        public System.Single intensityInside;  // 0x44
        public System.Single intensityOutside;  // 0x48
        public System.Single intensityMultiplier;  // 0x4c
        public System.Single hdrpExposureWeight;  // 0x50
        public VLB.BlendingMode blendingMode;  // 0x54
        public System.Boolean spotAngleFromLight;  // 0x58
        public System.Single spotAngle;  // 0x5c
        public System.Single spotAngleMultiplier;  // 0x60
        public System.Single coneRadiusStart;  // 0x64
        public VLB.ShaderAccuracy shaderAccuracy;  // 0x68
        public VLB.MeshType geomMeshType;  // 0x6c
        public System.Int32 geomCustomSides;  // 0x70
        public System.Int32 geomCustomSegments;  // 0x74
        public UnityEngine.Vector3 skewingLocalForwardDirection;  // 0x78
        public System.Boolean useAdditionalClippingPlane;  // 0x84
        public UnityEngine.Transform clippingPlaneTransform;  // 0x88
        public UnityEngine.Vector3 clippingPlanePosition;  // 0x90
        public UnityEngine.Quaternion clippingPlaneRotation;  // 0x9c
        public System.Single clippingPlaneTransition;  // 0xac
        public System.Boolean geomCap;  // 0xb0
        public VLB.AttenuationEquation attenuationEquation;  // 0xb4
        public System.Single attenuationCustomBlending;  // 0xb8
        public System.Single fallOffStart;  // 0xbc
        public System.Single fallOffEnd;  // 0xc0
        public System.Boolean fallOffEndFromLight;  // 0xc4
        private UnityEngine.Light m_CachedLightSpot;  // 0xc8
        public System.Single fallOffCap;  // 0xd0
        public System.Single fallOffEndMultiplier;  // 0xd4
        public System.Boolean depthBlendCapOff;  // 0xd8
        public System.Single depthBlendDistance;  // 0xdc
        public System.Single cameraClippingDistance;  // 0xe0
        public System.Single glareFrontal;  // 0xe4
        public System.Single glareBehind;  // 0xe8
        public System.Single fresnelPow;  // 0xec
        public VLB.NoiseMode noiseMode;  // 0xf0
        public System.Single noiseIntensity;  // 0xf4
        public System.Boolean noiseScaleUseGlobal;  // 0xf8
        public System.Single noiseContrast;  // 0xfc
        public System.Boolean noiseScaleUseSeparateAxis;  // 0x100
        public UnityEngine.Vector3 noiseScalesLocal;  // 0x104
        public System.Single noiseScaleLocal;  // 0x110
        public System.Boolean noiseVelocityUseGlobal;  // 0x114
        public UnityEngine.Vector3 noiseVelocityLocal;  // 0x118
        public System.Boolean fadeOutFromLight;  // 0x124
        public System.Single fadeOutBeginSecond;  // 0x128
        public System.Single fadeOutEndSecond;  // 0x12c
        public System.Boolean useFog;  // 0x130
        public System.Single fogIntensity;  // 0x134
        public VLB.Dimensions dimensions;  // 0x138
        public UnityEngine.Vector2 tiltFactor;  // 0x13c
        private System.Int32 pluginVersion;  // 0x144
        private System.Boolean _TrackChangesDuringPlaytime;  // 0x148
        private System.Int32 _SortingLayerID;  // 0x14c
        private System.Int32 _SortingOrder;  // 0x150
        private System.Single _FadeOutBegin;  // 0x154
        private System.Single _FadeOutEnd;  // 0x158
        private System.UInt32 <_INTERNAL_InstancedMaterialGroupID>k__BackingField;  // 0x15c
        public VLB.BeamGeometry m_BeamGeom;  // 0x160
        public System.Boolean matIsPersistent;  // 0x168
        private UnityEngine.Coroutine m_CoPlaytimeUpdate;  // 0x170
        private UnityEngine.Vector3 m_CachedClippingPlanePosition;  // 0x178
        private UnityEngine.Quaternion m_CachedClippingPlaneRotation;  // 0x184
        private System.Boolean m_HasCachedClippingPlanePose;  // 0x194

        // Properties
        VLB.ColorMode usedColorMode { get; /* RVA: 0x0B89A810 */ }
        System.Boolean useColorFromAttachedLightSpot { get; /* RVA: 0x0B89A660 */ }
        System.Boolean useColorTemperatureFromAttachedLightSpot { get; /* RVA: 0x0B89A6B8 */ }
        System.Single alphaInside { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        System.Single alphaOutside { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.Single intensityGlobal { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04DBF620 */ }
        System.Boolean useIntensityFromAttachedLightSpot { get; /* RVA: 0x0B89A760 */ }
        System.Boolean useSpotAngleFromAttachedLightSpot { get; /* RVA: 0x0B89A7B8 */ }
        System.Single coneAngle { get; /* RVA: 0x0B899A90 */ }
        System.Single coneRadiusEnd { get; /* RVA: 0x0B899C50 */ set; /* RVA: 0x0B89A844 */ }
        System.Single coneVolume { get; /* RVA: 0x0B899CB0 */ }
        System.Single coneApexOffsetZ { get; /* RVA: 0x0B899AE8 */ }
        UnityEngine.Vector3 coneApexPositionLocal { get; /* RVA: 0x0B899C20 */ }
        UnityEngine.Vector3 coneApexPositionGlobal { get; /* RVA: 0x0B899B58 */ }
        System.Int32 geomSides { get; /* RVA: 0x0B899D4C */ set; /* RVA: 0x0B89A8F8 */ }
        System.Int32 geomSegments { get; /* RVA: 0x0B899D20 */ set; /* RVA: 0x0B89A898 */ }
        UnityEngine.Vector3 skewingLocalForwardDirectionNormalized { get; /* RVA: 0x0B89A518 */ }
        System.Boolean canHaveMeshSkewing { get; /* RVA: 0x02FFF600 */ }
        System.Boolean hasMeshSkewing { get; /* RVA: 0x0B899DCC */ }
        UnityEngine.Vector4 additionalClippingPlane { get; /* RVA: 0x0B899588 */ }
        System.Single attenuationLerpLinearQuad { get; /* RVA: 0x0B899748 */ }
        System.Single fadeStart { get; /* RVA: 0x04DA62F0 */ set; /* RVA: 0x04DA6320 */ }
        System.Single fadeEnd { get; /* RVA: 0x04D9DF30 */ set; /* RVA: 0x04D9DFF0 */ }
        System.Boolean fadeEndFromLight { get; /* RVA: 0x04DA32F0 */ set; /* RVA: 0x04DBF600 */ }
        UnityEngine.Light lightSpotAttached { get; /* RVA: 0x04D8D1D0 */ }
        System.Boolean useFallOffEndFromAttachedLightSpot { get; /* RVA: 0x0B89A704 */ }
        System.Single maxGeometryDistance { get; /* RVA: 0x0B89A004 */ }
        System.Boolean isNoiseEnabled { get; /* RVA: 0x0B899EA8 */ }
        System.Boolean noiseEnabled { get; /* RVA: 0x0B899EA8 */ set; /* RVA: 0x0B89A958 */ }
        System.Single cullingDistance { get; /* RVA: 0x0B899D00 */ }
        System.Single fadeOutBegin { get; /* RVA: 0x04DBF5F0 */ set; /* RVA: 0x04DBF610 */ }
        System.Single fadeOutEnd { get; /* RVA: 0x04D90480 */ set; /* RVA: 0x04D904C0 */ }
        System.Boolean isFadeOutEnabled { get; /* RVA: 0x0B899E84 */ }
        System.Boolean isTilted { get; /* RVA: 0x0B899EB4 */ }
        System.Int32 sortingLayerID { get; /* RVA: 0x04DA7C70 */ set; /* RVA: 0x0B89A978 */ }
        System.String sortingLayerName { get; /* RVA: 0x0B89A650 */ set; /* RVA: 0x0B89A9FC */ }
        System.Int32 sortingOrder { get; /* RVA: 0x04DA24D0 */ set; /* RVA: 0x0B89AA24 */ }
        System.Boolean trackChangesDuringPlaytime { get; /* RVA: 0x04D8FE60 */ set; /* RVA: 0x04DBF630 */ }
        System.Boolean isCurrentlyTrackingChanges { get; /* RVA: 0x0B899E78 */ }
        System.Boolean hasGeometry { get; /* RVA: 0x0B899D78 */ }
        UnityEngine.Bounds bounds { get; /* RVA: 0x0B899994 */ }
        System.Int32 blendingModeAsInt { get; /* RVA: 0x0B8998E8 */ }
        UnityEngine.Quaternion beamInternalLocalRotation { get; /* RVA: 0x0B899828 */ }
        UnityEngine.Vector3 beamLocalForward { get; /* RVA: 0x0B8998AC */ }
        UnityEngine.Vector3 beamGlobalForward { get; /* RVA: 0x0B899770 */ }
        UnityEngine.Vector3 lossyScale { get; /* RVA: 0x0B899F14 */ }
        System.Single raycastDistance { get; /* RVA: 0x0B89A318 */ }
        UnityEngine.Vector3 raycastGlobalForward { get; /* RVA: 0x0B89A3F4 */ }
        UnityEngine.Vector3 raycastGlobalUp { get; /* RVA: 0x0B89A4C8 */ }
        UnityEngine.Vector3 raycastGlobalRight { get; /* RVA: 0x0B89A478 */ }
        System.Int32 _INTERNAL_pluginVersion { get; /* RVA: 0x04DBD7D0 */ }
        System.UInt32 _INTERNAL_InstancedMaterialGroupID { get; /* RVA: 0x04D904B0 */ set; /* RVA: 0x04D904E0 */ }
        System.String meshStats { get; /* RVA: 0x0B89A034 */ }
        System.Int32 meshVerticesCount { get; /* RVA: 0x0B89A26C */ }
        System.Int32 meshTrianglesCount { get; /* RVA: 0x0B89A1A8 */ }

        // Methods
        // RVA: 0x0B899358  token: 0x60000A1
        public System.Void GetInsideAndOutsideIntensity(System.Single& inside, System.Single& outside) { }
        // RVA: 0x0B899288  token: 0x60000D7
        private UnityEngine.Vector3 ComputeRaycastGlobalVector(UnityEngine.Vector3 localVec) { }
        // RVA: 0x04DBF5E0  token: 0x60000DC
        private System.Void SetFadeOutValue(System.Single& propToChange, System.Single value) { }
        // RVA: 0x0B89937C  token: 0x60000E2
        public System.Void .ctor() { }

    }

}

