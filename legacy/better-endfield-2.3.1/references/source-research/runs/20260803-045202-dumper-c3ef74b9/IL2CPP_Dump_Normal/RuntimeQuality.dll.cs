// ========================================================
// Dumped by @desirepro
// Assembly: RuntimeQuality.dll
// Classes:  93
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x200000D  // size: 0x14
    public sealed struct MatchType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Scripts.Quality.DeviceInfo.MatchType WhiteList;  // const
        public static Beyond.Scripts.Quality.DeviceInfo.MatchType SystemVersion;  // const
        public static Beyond.Scripts.Quality.DeviceInfo.MatchType GPU;  // const
        public static Beyond.Scripts.Quality.DeviceInfo.MatchType CPU;  // const
        public static Beyond.Scripts.Quality.DeviceInfo.MatchType DefaultScore;  // const
        public static Beyond.Scripts.Quality.DeviceInfo.MatchType MemoryCorrection;  // const
        public static Beyond.Scripts.Quality.DeviceInfo.MatchType HardCode;  // const

    }

    // TypeToken: 0x2000017  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Beyond.Scripts.Quality.QualityManager.<>c <>9;  // static @ 0x0
        public static System.Func<Beyond.Scripts.Quality.Components.QualityTierLevel,System.Int32> <>9__29_0;  // static @ 0x8
        public static System.Func<Beyond.Scripts.Quality.Components.QualityTierComponentMatch,System.Int32> <>9__30_0;  // static @ 0x10
        public static System.Func<Beyond.Scripts.Quality.DeviceQualityMapItem,System.Int32> <>9__31_0;  // static @ 0x18
        public static System.Func<Beyond.Scripts.Quality.DeviceQualityMapItem,System.Int32> <>9__32_0;  // static @ 0x20
        public static System.Comparison<System.Int32> <>9__66_0;  // static @ 0x28
        public static System.Comparison<System.Int32> <>9__67_0;  // static @ 0x30
        public static System.Comparison<System.Int32> <>9__68_0;  // static @ 0x38
        public static System.Comparison<Beyond.Scripts.Quality.Components.QualityTierComponentMatch> <>9__70_0;  // static @ 0x40

        // Methods
        // RVA: 0x04D4AD70  token: 0x600007D
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600007E
        public System.Void .ctor() { }
        // RVA: 0x03E647B0  token: 0x600007F
        private System.Int32 <LoadQualityTier>b__29_0(Beyond.Scripts.Quality.Components.QualityTierLevel s) { }
        // RVA: 0x048467E0  token: 0x6000080
        private System.Int32 <setQualityTier>b__30_0(Beyond.Scripts.Quality.Components.QualityTierComponentMatch x) { }
        // RVA: 0x04D94D70  token: 0x6000081
        private System.Int32 <ApplyQualityScore>b__31_0(Beyond.Scripts.Quality.DeviceQualityMapItem s) { }
        // RVA: 0x04D94D70  token: 0x6000082
        private System.Int32 <ApplyShaderTier>b__32_0(Beyond.Scripts.Quality.DeviceQualityMapItem s) { }
        // RVA: 0x0AC4664C  token: 0x6000083
        private System.Int32 <GetQualityComponentDefaultTier>b__66_0(System.Int32 a, System.Int32 b) { }
        // RVA: 0x0AC4664C  token: 0x6000084
        private System.Int32 <GetQualityComponentTier>b__67_0(System.Int32 a, System.Int32 b) { }
        // RVA: 0x0AC4664C  token: 0x6000085
        private System.Int32 <SetQualityComponentTier>b__68_0(System.Int32 a, System.Int32 b) { }
        // RVA: 0x0AC46668  token: 0x6000086
        private System.Int32 <_GetCurrentComponentTierIndex>b__70_0(Beyond.Scripts.Quality.Components.QualityTierComponentMatch a, Beyond.Scripts.Quality.Components.QualityTierComponentMatch b) { }

    }

    // TypeToken: 0x200001A  // size: 0x14
    public sealed struct AntiAliasing
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Scripts.Quality.Components.AntiAliasingQuality.AntiAliasing None;  // const
        public static Beyond.Scripts.Quality.Components.AntiAliasingQuality.AntiAliasing CSAA;  // const
        public static Beyond.Scripts.Quality.Components.AntiAliasingQuality.AntiAliasing FXAA;  // const
        public static Beyond.Scripts.Quality.Components.AntiAliasingQuality.AntiAliasing MSAA;  // const

    }

    // TypeToken: 0x2000021  // size: 0x14
    public sealed struct MatchSource
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Scripts.Quality.Components.QualityTierComponentMatch.MatchSource UpClosest;  // const
        public static Beyond.Scripts.Quality.Components.QualityTierComponentMatch.MatchSource DownClosest;  // const
        public static Beyond.Scripts.Quality.Components.QualityTierComponentMatch.MatchSource Matched;  // const
        public static Beyond.Scripts.Quality.Components.QualityTierComponentMatch.MatchSource Override;  // const

    }

    // TypeToken: 0x2000028  // size: 0x14
    public sealed struct DLSSFrameGenQualityEnum
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Scripts.Quality.Components.DLSSFrameGenQuality.DLSSFrameGenQualityEnum Auto;  // const
        public static Beyond.Scripts.Quality.Components.DLSSFrameGenQuality.DLSSFrameGenQualityEnum X1;  // const
        public static Beyond.Scripts.Quality.Components.DLSSFrameGenQuality.DLSSFrameGenQualityEnum X2;  // const
        public static Beyond.Scripts.Quality.Components.DLSSFrameGenQuality.DLSSFrameGenQualityEnum X3;  // const

    }

    // TypeToken: 0x200002D  // size: 0x14
    public sealed struct FrameGenMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Scripts.Quality.Components.FrameGenQuality.FrameGenMode None;  // const
        public static Beyond.Scripts.Quality.Components.FrameGenQuality.FrameGenMode DLSS;  // const
        public static Beyond.Scripts.Quality.Components.FrameGenQuality.FrameGenMode FSR3;  // const

    }

    // TypeToken: 0x2000030  // size: 0x11
    public sealed struct FrameRateQualityEnum
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Beyond.Scripts.Quality.Components.FrameRateQuality.FrameRateQualityEnum FPS120;  // const
        public static Beyond.Scripts.Quality.Components.FrameRateQuality.FrameRateQualityEnum FPS60;  // const
        public static Beyond.Scripts.Quality.Components.FrameRateQuality.FrameRateQualityEnum FPS30;  // const

    }

    // TypeToken: 0x2000032  // size: 0x14
    public sealed struct ReasonEnum
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Scripts.Quality.Components.HgFrameRateControl.ReasonEnum Default;  // const
        public static Beyond.Scripts.Quality.Components.HgFrameRateControl.ReasonEnum Init;  // const
        public static Beyond.Scripts.Quality.Components.HgFrameRateControl.ReasonEnum Quality;  // const
        public static Beyond.Scripts.Quality.Components.HgFrameRateControl.ReasonEnum Dialog;  // const
        public static Beyond.Scripts.Quality.Components.HgFrameRateControl.ReasonEnum Cutscene;  // const
        public static Beyond.Scripts.Quality.Components.HgFrameRateControl.ReasonEnum GMCommand;  // const

    }

    // TypeToken: 0x2000034  // size: 0x11
    public sealed struct FrameRateQualityMobileEnum
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Beyond.Scripts.Quality.Components.FrameRateQualityMobile.FrameRateQualityMobileEnum FPS60;  // const
        public static Beyond.Scripts.Quality.Components.FrameRateQualityMobile.FrameRateQualityMobileEnum FPS45;  // const
        public static Beyond.Scripts.Quality.Components.FrameRateQualityMobile.FrameRateQualityMobileEnum FPS30;  // const

    }

    // TypeToken: 0x2000039  // size: 0x14
    public sealed struct HGAnisoLevel
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Scripts.Quality.Components.HGAnisoLevelQuality.HGAnisoLevel X1;  // const
        public static Beyond.Scripts.Quality.Components.HGAnisoLevelQuality.HGAnisoLevel X2;  // const
        public static Beyond.Scripts.Quality.Components.HGAnisoLevelQuality.HGAnisoLevel X4;  // const
        public static Beyond.Scripts.Quality.Components.HGAnisoLevelQuality.HGAnisoLevel X8;  // const

    }

    // TypeToken: 0x2000042  // size: 0x11
    public sealed struct TextureQualityEnum
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Beyond.Scripts.Quality.Components.HGTextureQuality.TextureQualityEnum High;  // const
        public static Beyond.Scripts.Quality.Components.HGTextureQuality.TextureQualityEnum Medium;  // const
        public static Beyond.Scripts.Quality.Components.HGTextureQuality.TextureQualityEnum Low;  // const

    }

    // TypeToken: 0x200004C  // size: 0x14
    public sealed struct RenderingScaleMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Scripts.Quality.Components.RenderingScaleQualityMobile.RenderingScaleMode ExHigh;  // const
        public static Beyond.Scripts.Quality.Components.RenderingScaleQualityMobile.RenderingScaleMode High;  // const
        public static Beyond.Scripts.Quality.Components.RenderingScaleQualityMobile.RenderingScaleMode Medium;  // const
        public static Beyond.Scripts.Quality.Components.RenderingScaleQualityMobile.RenderingScaleMode Low;  // const
        public static Beyond.Scripts.Quality.Components.RenderingScaleQualityMobile.RenderingScaleMode ExLow;  // const

    }

    // TypeToken: 0x200004E  // size: 0x14
    public sealed struct RenderingScaleMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Scripts.Quality.Components.RenderingScaleQualityPC.RenderingScaleMode Percent100;  // const
        public static Beyond.Scripts.Quality.Components.RenderingScaleQualityPC.RenderingScaleMode Percent90;  // const
        public static Beyond.Scripts.Quality.Components.RenderingScaleQualityPC.RenderingScaleMode Percent80;  // const
        public static Beyond.Scripts.Quality.Components.RenderingScaleQualityPC.RenderingScaleMode Percent70;  // const
        public static Beyond.Scripts.Quality.Components.RenderingScaleQualityPC.RenderingScaleMode Percent60;  // const

    }

    // TypeToken: 0x2000052  // size: 0x14
    public sealed struct ShaderLodEnum
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Scripts.Quality.Components.ShaderLodQuality.ShaderLodEnum PC600;  // const
        public static Beyond.Scripts.Quality.Components.ShaderLodQuality.ShaderLodEnum Mobile500;  // const
        public static Beyond.Scripts.Quality.Components.ShaderLodQuality.ShaderLodEnum Mobile400;  // const
        public static Beyond.Scripts.Quality.Components.ShaderLodQuality.ShaderLodEnum Mobile300;  // const
        public static Beyond.Scripts.Quality.Components.ShaderLodQuality.ShaderLodEnum Mobile200;  // const
        public static Beyond.Scripts.Quality.Components.ShaderLodQuality.ShaderLodEnum Mobile100;  // const

    }

    // TypeToken: 0x2000057  // size: 0x14
    public sealed struct UpscalerMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Scripts.Quality.Components.UpscalerQuality.UpscalerMode DLSS;  // const
        public static Beyond.Scripts.Quality.Components.UpscalerQuality.UpscalerMode TAAU;  // const
        public static Beyond.Scripts.Quality.Components.UpscalerQuality.UpscalerMode FSR3;  // const

    }

namespace Beyond.Gameplay.BeyondPerformance
{

    // TypeToken: 0x2000009  // size: 0x11
    public sealed struct LogicQualityLevel
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Beyond.Gameplay.BeyondPerformance.LogicQualityLevel Economy;  // const
        public static Beyond.Gameplay.BeyondPerformance.LogicQualityLevel Balanced;  // const
        public static Beyond.Gameplay.BeyondPerformance.LogicQualityLevel Performance;  // const
        public static Beyond.Gameplay.BeyondPerformance.LogicQualityLevel Max;  // const

    }

    // TypeToken: 0x200000A  // size: 0x11
    public sealed struct ThermalState
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Beyond.Gameplay.BeyondPerformance.ThermalState GameNormal;  // const
        public static Beyond.Gameplay.BeyondPerformance.ThermalState GameFair;  // const
        public static Beyond.Gameplay.BeyondPerformance.ThermalState GameSerious;  // const

    }

    // TypeToken: 0x200000B  // size: 0x10
    public static class LogicQualityConst
    {
        // Fields
        public static System.Single s_reduceQualityLevelFPSThresholdRatio;  // static @ 0x0
        public static System.Single s_recoverQualityLevelFPSThresholdRatio;  // static @ 0x4

        // Methods
        // RVA: 0x04D5DAF0  token: 0x6000009
        private static System.Void .cctor() { }

    }

}

namespace Beyond.PoolCore
{

    // TypeToken: 0x2000003  // size: 0x10
    public static class ListPoolInitializer_RuntimeQuality
    {
        // Fields
        private static System.Boolean s_initialized;  // static @ 0x0

        // Methods
        // RVA: 0x04CE21B0  token: 0x6000002
        private static System.Void InitializeAtRuntime() { }
        // RVA: 0x04CE21E0  token: 0x6000003
        private static System.Void Initialize() { }

    }

}

namespace Beyond.Scripts.Quality
{

    // TypeToken: 0x200000C  // size: 0x88
    public class DeviceInfo
    {
        // Fields
        public UnityEngine.RuntimePlatform m_platform;  // 0x10
        private System.Int32 <DevicesScore>k__BackingField;  // 0x14
        public readonly System.String m_deviceModel;  // 0x18
        public readonly System.String m_deviceName;  // 0x20
        public readonly System.String m_graphicsDeviceName;  // 0x28
        public readonly System.String m_graphicsDeviceVersion;  // 0x30
        public readonly System.String m_cpuDeviceName;  // 0x38
        public readonly System.Int32 m_systemMemorySize;  // 0x40
        public readonly System.Int32 m_GPUMemorySize;  // 0x44
        public readonly System.Int32 m_processorCount;  // 0x48
        public readonly UnityEngine.Rendering.GraphicsDeviceType m_graphicsDeviceType;  // 0x4c
        public Beyond.Scripts.Quality.DeviceMatchRules m_matchRules;  // 0x50
        public Beyond.Scripts.Quality.DeviceMatchResult m_matchResult;  // 0x58
        public System.Boolean bEditorSimulator;  // 0x78
        public readonly System.String m_SystemInfo;  // 0x80
        private static System.String ADRENO_KEY;  // const
        private static System.String ADRENO_PATTERN;  // const
        private static System.String ADRENO_STANDARD_FORMAT;  // const
        private static System.String MALI_KEY;  // const
        private static System.String MALI_PATTERN;  // const
        private static System.String MALI_STANDARD_FORMAT;  // const
        private static System.String POWER_VR_KEY;  // const
        private static System.String POWER_VR_PATTERN;  // const
        private static System.String POWER_VR_STANDARD_FORMAT;  // const
        private static System.String NVIDIA_KEY;  // const
        private static System.String NVIDIA_PATTERN;  // const
        private static System.String NVIDIA_STANDARD_FORMAT;  // const
        private static System.String TITAN_PATTERN;  // const
        private static System.String NVIDIA_FALLBACK;  // const
        private static System.String AMD_KEY;  // const
        private static System.String AMD_PATTERN;  // const
        private static System.String AMD_STANDARD_FORMAT;  // const
        private static System.String AMD_FALLBACK;  // const
        private static System.String INTEL_KEY;  // const
        private static System.String INTEL_PATTERN;  // const
        private static System.String INTEL_STANDARD_FORMAT;  // const
        private static System.String INTEL_PATTERN_2;  // const
        private static System.String INTEL_FALLBACK;  // const

        // Properties
        System.Int32 DevicesScore { get; /* RVA: 0x04D882A0 */ set; /* RVA: 0x04D8F540 */ }
        System.String cpuName { get; /* RVA: 0x0350CDE0 */ }

        // Methods
        // RVA: 0x0350CC90  token: 0x600000C
        public System.Void .ctor(System.String remoteJson) { }
        // RVA: 0x0AC3DCF4  token: 0x600000D
        public System.Void .ctor(UnityEngine.RuntimePlatform platform, System.String deviceName, System.String deviceModel, System.String graphicsDeviceName, System.String graphicsDeviceVersion, System.String systemMemorySize, System.String processorName, System.String processorCount, System.String processorFrequency) { }
        // RVA: 0x0350AE10  token: 0x600000E
        public System.Void SupplementFromRemote(System.String json) { }
        // RVA: 0x0454FCA0  token: 0x600000F
        public virtual System.String ToString() { }
        // RVA: 0x0AC3D450  token: 0x6000010
        public System.Boolean IsNoMatchDevice() { }
        // RVA: 0x04854560  token: 0x6000011
        public System.Boolean IsZeroDevices() { }
        // RVA: 0x0AC3D394  token: 0x6000013
        public System.Boolean IsHUAWEIDevice() { }
        // RVA: 0x0AC3D4AC  token: 0x6000014
        public System.Boolean IsQualcommDevice() { }
        // RVA: 0x0350C760  token: 0x6000015
        public Beyond.Scripts.Quality.DeviceMatchRules LoadingSettingRules() { }
        // RVA: 0x0350AEC0  token: 0x6000016
        public System.Int32 GetDeviceScore(System.String remoteJson) { }
        // RVA: 0x0AC3C560  token: 0x6000017
        public System.Int32 EditorSimulateGetMatchedRule(Beyond.Scripts.Quality.DeviceInfo deviceInfo) { }
        // RVA: 0x0AC3D054  token: 0x6000018
        public System.Int32 IOSGetMatchedRule(Beyond.Scripts.Quality.DeviceInfo deviceInfo) { }
        // RVA: 0x0AC3C2F8  token: 0x6000019
        public System.Int32 AndroidGetMatchedRule(Beyond.Scripts.Quality.DeviceInfo deviceInfo) { }
        // RVA: 0x0350C7F0  token: 0x600001A
        public System.Int32 StandaloneGetMatchedRule(Beyond.Scripts.Quality.DeviceInfo deviceInfo) { }
        // RVA: 0x0AC3DA0C  token: 0x600001B
        public System.Int32 StandaloneOSXGetMatchedRule(Beyond.Scripts.Quality.DeviceInfo deviceInfo) { }
        // RVA: 0x0AC3C4B4  token: 0x600001C
        public System.Int32 CloudGameGetMatchedRule(Beyond.Scripts.Quality.DeviceInfo deviceInfo) { }
        // RVA: 0x0AC3DA60  token: 0x600001D
        public System.Int32 StandalonePS5GetMatchedRule(Beyond.Scripts.Quality.DeviceInfo deviceInfo) { }
        // RVA: 0x0AC3C50C  token: 0x600001E
        public System.Int32 DefaultGetMatchedRule(Beyond.Scripts.Quality.DeviceInfo deviceInfo) { }
        // RVA: 0x041297C0  token: 0x600001F
        public System.Boolean RuleSuffixMatch(System.Collections.Generic.List<Beyond.Scripts.Quality.RankingRule> list, System.String name, Beyond.Scripts.Quality.RankingRule& matchedRule) { }
        // RVA: 0x0AC3D82C  token: 0x6000020
        public static System.Boolean RuleFullNameMatch(System.Collections.Generic.List<Beyond.Scripts.Quality.RankingRule> list, System.String name, Beyond.Scripts.Quality.RankingRule& matchedRule) { }
        // RVA: 0x0AC3D530  token: 0x6000021
        public System.Boolean MatchDeviceModeRule(System.Collections.Generic.List<Beyond.Scripts.Quality.RankingRule> list, System.String deviceModel, Beyond.Scripts.Quality.RankingRule& rule) { }
        // RVA: 0x0AC3D5D8  token: 0x6000022
        public System.Boolean MatchGPURule(Beyond.Scripts.Quality.DeviceMatchRules rules, System.String gpu, Beyond.Scripts.Quality.RankingRule& rule, System.String& standardName) { }
        // RVA: 0x041291C0  token: 0x6000023
        private System.Boolean StandaloneMatchGPURule(Beyond.Scripts.Quality.DeviceMatchRules rules, System.String gpu, Beyond.Scripts.Quality.RankingRule& rule, System.String& standardName) { }
        // RVA: 0x0AC3CCD0  token: 0x6000024
        private System.String GetNvidiaStandardGPUName(System.String gpu) { }
        // RVA: 0x04129440  token: 0x6000025
        private System.String GetAmdStandardGPUName(System.String gpu) { }
        // RVA: 0x0AC3C788  token: 0x6000026
        private System.String GetIntelStandardGPUName(System.String gpu) { }
        // RVA: 0x0AC3C64C  token: 0x6000027
        private System.String GetAdrenoStandardGPUName(System.String gpu) { }
        // RVA: 0x0AC3C9E8  token: 0x6000028
        private System.String GetMaliStandardGPUName(System.String gpu) { }
        // RVA: 0x0AC3CF18  token: 0x6000029
        private System.String GetPowerVRStandardGPUName(System.String gpu) { }
        // RVA: 0x0AC3DB10  token: 0x600002A
        private static System.Void _ReportMatchRuleFailed(Beyond.Scripts.Quality.DeviceInfo deviceModel) { }
        // RVA: 0x0669AD6C  token: 0x600002B
        public System.String <>iFixBaseProxy_ToString() { }

    }

    // TypeToken: 0x200000E  // size: 0x20
    public sealed struct RankingRule : System.IEquatable`1
    {
        // Fields
        public System.String regexPattern;  // 0x10
        public System.Int32 targetScore;  // 0x18

        // Methods
        // RVA: 0x0AC45920  token: 0x600002C
        public virtual System.Boolean Equals(Beyond.Scripts.Quality.RankingRule other) { }
        // RVA: 0x045501D0  token: 0x600002D
        public virtual System.String ToString() { }
        // RVA: 0x0AC45990  token: 0x600002E
        public System.String <>iFixBaseProxy_ToString() { }

    }

    // TypeToken: 0x200000F  // size: 0x1C
    public sealed struct MemoryRule : System.IEquatable`1
    {
        // Fields
        public UnityEngine.Vector2 range;  // 0x10
        public System.Int32 targetScore;  // 0x18

        // Methods
        // RVA: 0x0AC425D0  token: 0x600002F
        public virtual System.String ToString() { }
        // RVA: 0x0AC42560  token: 0x6000030
        public virtual System.Boolean Equals(Beyond.Scripts.Quality.MemoryRule other) { }
        // RVA: 0x0AC4269C  token: 0x6000031
        public System.String <>iFixBaseProxy_ToString() { }

    }

    // TypeToken: 0x2000010  // size: 0x10
    public static class ListUtils
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000032
        public static System.Void AddOrReplace(System.Collections.Generic.List<TItem> source, System.Collections.Generic.List<TItem> remote) { }

    }

    // TypeToken: 0x2000011  // size: 0x98
    public class DeviceMatchRules
    {
        // Fields
        public System.Int32 DefaultIOSRankingRule;  // 0x10
        public System.Int32 DefaultAndroidScore;  // 0x14
        public System.Int32 DefaultStandAloneScore;  // 0x18
        public System.Int32 DefaultPowerVRScore;  // 0x1c
        public System.Collections.Generic.List<Beyond.Scripts.Quality.RankingRule> appleWhiteList;  // 0x20
        public System.Collections.Generic.List<Beyond.Scripts.Quality.RankingRule> appleGPUList;  // 0x28
        public System.Collections.Generic.List<Beyond.Scripts.Quality.RankingRule> androidWhiteList;  // 0x30
        public System.Collections.Generic.List<Beyond.Scripts.Quality.RankingRule> adrenoRulesList;  // 0x38
        public System.Collections.Generic.List<Beyond.Scripts.Quality.RankingRule> maliRulesList;  // 0x40
        public System.Collections.Generic.List<Beyond.Scripts.Quality.RankingRule> powerVRRulesList;  // 0x48
        public System.Collections.Generic.List<Beyond.Scripts.Quality.RankingRule> androidGPUWhiteList;  // 0x50
        public System.Collections.Generic.List<Beyond.Scripts.Quality.RankingRule> androidCPUWhiteList;  // 0x58
        public System.Collections.Generic.List<Beyond.Scripts.Quality.MemoryRule> iOSMemoryRules;  // 0x60
        public System.Collections.Generic.List<Beyond.Scripts.Quality.MemoryRule> androidMemoryRules;  // 0x68
        public System.Collections.Generic.List<Beyond.Scripts.Quality.RankingRule> nvidiaRulesList;  // 0x70
        public System.Collections.Generic.List<Beyond.Scripts.Quality.RankingRule> amdRulesList;  // 0x78
        public System.Collections.Generic.List<Beyond.Scripts.Quality.RankingRule> intelRulesList;  // 0x80
        public System.Collections.Generic.List<Beyond.Scripts.Quality.MemoryRule> standaloneMemoryRules;  // 0x88
        public System.Collections.Generic.List<Beyond.Scripts.Quality.RankingRule> macSocRulesList;  // 0x90

        // Methods
        // RVA: 0x0AC3DE30  token: 0x6000033
        public System.Void Combine(Beyond.Scripts.Quality.DeviceMatchRules other) { }
        // RVA: 0x04D7B090  token: 0x6000034
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000012  // size: 0x30
    public sealed struct DeviceMatchResult
    {
        // Fields
        private Beyond.Scripts.Quality.RankingRule m_rule;  // 0x10
        private Beyond.Scripts.Quality.DeviceInfo.MatchType m_matchType;  // 0x20
        private System.String gpuStandardName;  // 0x28

        // Methods
        // RVA: 0x0350AE90  token: 0x6000035
        public System.Void .ctor(Beyond.Scripts.Quality.RankingRule r, Beyond.Scripts.Quality.DeviceInfo.MatchType type, System.String gpuStandardName) { }
        // RVA: 0x04550040  token: 0x6000036
        public virtual System.String ToString() { }
        // RVA: 0x0AC3DD8C  token: 0x6000037
        public System.Boolean IsNoMatch() { }
        // RVA: 0x0AC3DDD8  token: 0x6000038
        public System.String <>iFixBaseProxy_ToString() { }

    }

    // TypeToken: 0x2000013  // size: 0x38
    public sealed struct DeviceQualityMapItem
    {
        // Fields
        public System.String name;  // 0x10
        public System.Int32 deviceScore;  // 0x18
        public System.Int32 defaultLevel;  // 0x1c
        public System.Int32 veryLowLevel;  // 0x20
        public System.Int32 lowLevel;  // 0x24
        public System.Int32 middleLevel;  // 0x28
        public System.Int32 highLevel;  // 0x2c
        public System.Int32 veryHighLevel;  // 0x30

        // Methods
        // RVA: 0x0AC3E00C  token: 0x6000039
        public virtual System.String ToString() { }
        // RVA: 0x0AC3E2A4  token: 0x600003A
        private UnityEngine.Color getColor(System.Int32 level) { }
        // RVA: 0x0AC3E50C  token: 0x600003B
        private UnityEngine.Color getVeryLowColor() { }
        // RVA: 0x0AC3E3B0  token: 0x600003C
        private UnityEngine.Color getLowColor() { }
        // RVA: 0x0AC3E424  token: 0x600003D
        private UnityEngine.Color getMiddleColor() { }
        // RVA: 0x0AC3E33C  token: 0x600003E
        private UnityEngine.Color getHighColor() { }
        // RVA: 0x0AC3E498  token: 0x600003F
        private UnityEngine.Color getVeryHighColor() { }
        // RVA: 0x0AC3E240  token: 0x6000040
        public System.String <>iFixBaseProxy_ToString() { }

    }

    // TypeToken: 0x2000014  // size: 0x20
    public class DeviceQualityMap
    {
        // Fields
        public UnityEngine.RuntimePlatform m_platform;  // 0x10
        public System.Collections.Generic.List<Beyond.Scripts.Quality.DeviceQualityMapItem> items;  // 0x18

        // Methods
        // RVA: 0x04CDB860  token: 0x6000041
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000015  // size: 0x10
    public static class QualityConst
    {
        // Fields
        public static System.String SIMULATE_DEVICE_LIST;  // const
        public static System.String SETTING_RULES;  // const
        public static System.String QUALITY_MAP;  // const
        public static System.String QUALITY_TIER_COMPONENTS;  // const

    }

    // TypeToken: 0x2000016  // size: 0x98
    public class QualityManager : Beyond.Singleton`1, System.IDisposable
    {
        // Fields
        private Beyond.Scripts.Quality.DeviceInfo m_deviceInfo;  // 0x10
        private Beyond.Scripts.Quality.DeviceQualityMap m_qualityMap;  // 0x18
        private Beyond.Scripts.Quality.DeviceQualityMapItem m_matchedConfig;  // 0x20
        private System.Collections.Generic.Dictionary<Beyond.GameSetting.GameSettingVideoQuality,System.Int32> m_qualityScoreMap;  // 0x48
        private System.Collections.Generic.List<Beyond.Scripts.Quality.Components.QualityTierLevel> m_qualityTiers;  // 0x50
        private System.Collections.Generic.HashSet<System.Type> m_qualityTierTypes;  // 0x58
        private System.Collections.Generic.Dictionary<System.Type,Beyond.Scripts.Quality.Components.QualityTierComponentMatch> m_currentQualityTierComponent;  // 0x60
        private System.Collections.Generic.Dictionary<System.Type,Beyond.Scripts.Quality.Components.QualityTierComponentMatch> m_overrideQualityTierComponent;  // 0x68
        private System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.Dictionary<System.Type,System.Action<Beyond.Scripts.Quality.Components.QualityTierComponent>>> m_qualityTierComponentChangeCallBack;  // 0x70
        private System.Collections.Generic.Queue<System.ValueTuple<System.Type,System.Action<Beyond.Scripts.Quality.Components.QualityTierComponent>>> m_queue;  // 0x78
        private System.Int32 m_defaultTier;  // 0x80
        private System.Int32 m_currentTier;  // 0x84
        private System.Boolean m_overrideFinish;  // 0x88
        private System.Func<System.Type,System.Boolean> resetSaveQualityTier;  // 0x90
        private static System.Collections.Generic.HashSet<System.Int32> s_matchCount;  // static @ 0x0
        private static System.Collections.Generic.HashSet<Beyond.Scripts.Quality.Components.QualityTierComponentMatch> s_matchQuality;  // static @ 0x8

        // Properties
        Beyond.Scripts.Quality.DeviceQualityMapItem Config { get; /* RVA: 0x0AC45394 */ }
        System.Int32 currentTier { get; /* RVA: 0x04B4B340 */ }
        System.Int32 defaultTier { get; /* RVA: 0x04B4B310 */ }
        Beyond.Scripts.Quality.DeviceInfo device { get; /* RVA: 0x03690520 */ }
        System.Boolean isInitializing { get; /* RVA: 0x036904F0 */ }

        // Methods
        // RVA: 0x04A233A0  token: 0x6000046
        private System.Void .ctor() { }
        // RVA: 0x041E3220  token: 0x6000047
        public System.Void Init(System.String remoteJson) { }
        // RVA: 0x04B40C00  token: 0x6000048
        public System.Void InitOnlyDevice(System.String remoteJson) { }
        // RVA: 0x041E3500  token: 0x6000049
        public System.Void InitShaderLod() { }
        // RVA: 0x04151520  token: 0x600004A
        public System.Void FinishInit() { }
        // RVA: 0x041E47F0  token: 0x600004C
        public virtual System.Void Dispose() { }
        // RVA: 0x03899090  token: 0x600004D
        public System.Void LoadQualityTier(System.String overridePlatform) { }
        // RVA: 0x0368D6B0  token: 0x600004E
        private System.Void setQualityTier(System.Int32 qualityTier) { }
        // RVA: 0x0366FDA0  token: 0x600004F
        public System.Void ApplyQualityScore() { }
        // RVA: 0x036705F0  token: 0x6000050
        public System.Void ApplyShaderTier() { }
        // RVA: 0x036921B0  token: 0x6000051
        private System.Type _GetQualityComponentByName(System.String typeName) { }
        // RVA: 0x04B89CD0  token: 0x6000052
        public System.Void SetQualityTierByVideoQuality(Beyond.GameSetting.GameSettingVideoQuality VideoQuality) { }
        // RVA: 0x04991510  token: 0x6000053
        public Beyond.GameSetting.GameSettingVideoQuality GetDefaultVideoQuality() { }
        // RVA: 0x04854520  token: 0x6000054
        public System.Boolean NoMatchDeviceOrZeroScore() { }
        // RVA: 0x0AC44430  token: 0x6000055
        public System.Boolean IsQualcommDevice() { }
        // RVA: 0x0AC44368  token: 0x6000056
        public System.Boolean IsHUAWEIDevice() { }
        // RVA: 0x0AC443BC  token: 0x6000057
        public static System.Boolean IsIpadDevice() { }
        // RVA: 0x0AC44594  token: 0x6000058
        public System.Boolean IsVideoQualityEnable(Beyond.GameSetting.GameSettingVideoQuality VideoQuality) { }
        // RVA: 0x04150EE0  token: 0x6000059
        public System.Void InitResetQualityTierComponentFunction(System.Func<System.Type,System.Boolean> func) { }
        // RVA: 0x04485530  token: 0x600005A
        public System.Boolean ResetQualityTierComponentValue(System.Type type) { }
        // RVA: 0x03899A80  token: 0x600005B
        private Beyond.Scripts.Quality.Components.QualityTierComponentMatch _SetQualityTierComponentLevel(System.Type type, System.Int32 level) { }
        // RVA: 0x04A3C4A0  token: 0x600005C
        public System.Void ForceSetQualityTierComponentLevel(System.String type, System.Int32 level) { }
        // RVA: 0x0AC447E8  token: 0x600005D
        public System.Void RevertSetQualityTierComponentLevel(System.String type) { }
        // RVA: 0x0AC445F0  token: 0x600005E
        public System.Boolean NeedRestart() { }
        // RVA: 0x04151560  token: 0x600005F
        public System.Void ApplyCallBack() { }
        // RVA: 0x03690550  token: 0x6000060
        private Beyond.Scripts.Quality.Components.QualityTierComponentMatch _GetOverrideQualityTierComponentLevel(System.Type type) { }
        // RVA: 0x04151260  token: 0x6000061
        public Beyond.Scripts.Quality.Components.QualityTierType GetQualityTierComponentTierType(System.String type) { }
        // RVA: 0x041519F0  token: 0x6000062
        public System.Int32 GetQualityTierComponentTierVersion(System.String type) { }
        // RVA: 0x0368E580  token: 0x6000063
        private System.Boolean _IsToggleQualityTierComponent(System.String type) { }
        // RVA: 0x0AC45234  token: 0x6000064
        private System.Void _ToggleQualityTierComponentLevel(System.String type, System.Int32 index) { }
        // RVA: 0x0368D5D0  token: 0x6000065
        public System.Boolean IsEnumQualityTierComponent(System.String type) { }
        // RVA: 0x0AC42F4C  token: 0x6000066
        public System.Int32 GetEnumQualityTierComponentCount(System.String type) { }
        // RVA: 0x04A2FD80  token: 0x6000067
        public System.Void SetEnumQualityTierComponentCount(System.String type, System.Int32 index) { }
        // RVA: 0x0368D4F0  token: 0x6000068
        public System.Boolean IsSliderQualityTierComponent(System.String type) { }
        // RVA: 0x0AC44974  token: 0x6000069
        public System.Void SetSliderQualityTierComponentCount(System.String type, System.Single value) { }
        // RVA: 0x0AC440A4  token: 0x600006A
        public UnityEngine.Vector3 GetSliderQualityTierComponent(System.String type) { }
        // RVA: 0x0AC441D8  token: 0x600006B
        public System.Boolean InvokeQualityTierComponent(System.String type, System.String functionName, System.Object[] parameters) { }
        // RVA: -1  // generic def  token: 0x600006C
        public System.Void RegisterQualityTierComponentChange(System.Type tierType, System.Action<Beyond.Scripts.Quality.Components.QualityTierComponent> cb) { }
        // RVA: 0x0368E660  token: 0x600006D
        private System.Void InvokeChangeCallBack(System.Type type, Beyond.Scripts.Quality.Components.QualityTierComponent qualityTierComponent) { }
        // RVA: 0x0AC43D34  token: 0x600006E
        public System.ValueTuple<System.Single,System.Single> GetQualityTierComponentTierCount(System.String type) { }
        // RVA: 0x0AC43020  token: 0x600006F
        public System.Single GetQualityComponentDefaultTier(System.String type) { }
        // RVA: 0x0AC43638  token: 0x6000070
        public System.Single GetQualityComponentTier(System.String type) { }
        // RVA: 0x0368C030  token: 0x6000071
        public System.Void SetQualityComponentTier(System.String type, System.Single index) { }
        // RVA: 0x0AC44484  token: 0x6000072
        public System.Boolean IsQualitySubSettingTierValid(System.String type, System.Int32 tier) { }
        // RVA: 0x0AC44D24  token: 0x6000073
        private System.Void _GetCurrentComponentTierIndex(Beyond.Scripts.Quality.Components.QualityTierComponent qualityTierComponent, System.Int32& index, System.Int32& defaultTierIndex) { }
        // RVA: 0x0AC44AC4  token: 0x6000074
        public virtual System.String ToString() { }
        // RVA: 0x0AC42920  token: 0x6000075
        public System.String DebugQualityTierComponentInfo() { }
        // RVA: 0x0368EC90  token: 0x6000076
        private static System.Void _ReportQuality() { }
        // RVA: 0x041E3CE0  token: 0x6000077
        private System.Void _DynamicInitQualitySetting() { }
        // RVA: 0x041E3B70  token: 0x6000078
        private System.Void _DynamicReleaseQualitySetting() { }
        // RVA: 0x03690650  token: 0x6000079
        private System.Void DynamicQualityChange(Beyond.EventData<Beyond.Gameplay.BeyondPerformance.LogicQualityLevel,Beyond.Gameplay.BeyondPerformance.LogicQualityLevel>& data) { }
        // RVA: 0x0AC42D4C  token: 0x600007A
        private System.Void DynamicThermalChange(Beyond.EventData<Beyond.Gameplay.BeyondPerformance.ThermalState,Beyond.Gameplay.BeyondPerformance.ThermalState>& data) { }
        // RVA: 0x04CA3B10  token: 0x600007B
        private static System.Void .cctor() { }
        // RVA: 0x0669AD6C  token: 0x600007C
        public System.String <>iFixBaseProxy_ToString() { }

    }

    // TypeToken: 0x2000018  // size: 0x10
    public static class QualityUtility
    {
        // Fields
        private static Newtonsoft.Json.JsonSerializerSettings s_settings;  // static @ 0x0

        // Properties
        Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings { get; /* RVA: 0x035831A0 */ }

        // Methods
        // RVA: -1  // generic def  token: 0x6000087
        public static T LoadFromCompressedJson() { }
        // RVA: -1  // generic def  token: 0x6000088
        public static T LoadingQualityJson(System.String path) { }
        // RVA: -1  // generic def  token: 0x6000089
        public static T LoadingQualityJsonFromString(System.String value) { }
        // RVA: 0x0AC45660  token: 0x600008A
        private static System.Byte[] Decompress(System.Byte[] data) { }

    }

}

namespace Beyond.Scripts.Quality.Components
{

    // TypeToken: 0x2000019  // size: 0x30
    public class AntiAliasingQuality : Beyond.Scripts.Quality.Components.EnumQualityComponent`1
    {
        // Methods
        // RVA: 0x0AC3BF88  token: 0x600008C
        protected virtual System.Void Apply() { }
        // RVA: 0x0AC3BFCC  token: 0x600008D
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x600008E
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x200001B
    public interface IEnumQualityComponent
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600008F
        public virtual System.Int32 GetEnumIndex(System.Enum enumValue) { }
        // RVA: -1  // abstract  token: 0x6000090
        public virtual System.Int32 GetEnumIndex() { }
        // RVA: -1  // abstract  token: 0x6000091
        public virtual System.Int32 GetDefaultEnumIndex() { }
        // RVA: -1  // abstract  token: 0x6000092
        public virtual System.Int32 GetEnumCount() { }
        // RVA: -1  // abstract  token: 0x6000093
        public virtual System.Void SetEnumIndex(System.Int32 value) { }
        // RVA: -1  // abstract  token: 0x6000094
        public virtual System.String GetEnumName(System.Int32 index) { }

    }

    // TypeToken: 0x200001C
    public class EnumQualityComponent`1 : Beyond.Scripts.Quality.Components.QualityTierComponent, Beyond.Scripts.Quality.Components.IEnumQualityComponent, Beyond.Scripts.Quality.Components.IQualityComponentExtension`1
    {
        // Fields
        private T enumValue;  // 0x0
        private System.Nullable<System.Int32> m_currentValue;  // 0x0
        private System.String[] m_strings;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000095
        public virtual System.Int32 GetEnumIndex(System.Enum enumValue) { }
        // RVA: -1  // not resolved  token: 0x6000096
        public virtual System.Int32 GetEnumIndex() { }
        // RVA: -1  // not resolved  token: 0x6000097
        public virtual System.Int32 GetDefaultEnumIndex() { }
        // RVA: -1  // not resolved  token: 0x6000098
        public virtual System.Int32 GetEnumCount() { }
        // RVA: -1  // not resolved  token: 0x6000099
        public virtual System.Void SetEnumIndex(System.Int32 value) { }
        // RVA: -1  // not resolved  token: 0x600009A
        public virtual System.String GetEnumName(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x600009B
        public System.Void SetEnum(T value) { }
        // RVA: -1  // not resolved  token: 0x600009C
        public virtual T GetValue() { }
        // RVA: -1  // not resolved  token: 0x600009D
        public virtual T GetDefaultValue() { }
        // RVA: -1  // not resolved  token: 0x600009E
        public System.Void SetEnumWithOutApply(T value) { }
        // RVA: -1  // not resolved  token: 0x600009F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001D  // size: 0x14
    public sealed struct QualityTierType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Scripts.Quality.Components.QualityTierType Quality;  // const
        public static Beyond.Scripts.Quality.Components.QualityTierType ToggleQuality;  // const
        public static Beyond.Scripts.Quality.Components.QualityTierType EnumQuality;  // const
        public static Beyond.Scripts.Quality.Components.QualityTierType SliderQuality;  // const

    }

    // TypeToken: 0x200001E
    public interface IQualityComponentExtension`1
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60000A0
        public virtual T GetValue() { }
        // RVA: -1  // abstract  token: 0x60000A1
        public virtual T GetDefaultValue() { }

    }

    // TypeToken: 0x200001F  // size: 0x18
    public abstract class QualityTierComponent
    {
        // Fields
        public static System.Action<System.String,System.Int32> s_removeRegisterKeyAction;  // static @ 0x0
        private System.Boolean isNeedStartDirty;  // 0x10

        // Methods
        // RVA: 0x04B33480  token: 0x60000A2
        protected virtual System.Void Apply() { }
        // RVA: 0x046DEB70  token: 0x60000A3
        protected virtual System.Void InitApply() { }
        // RVA: 0x0AC454F0  token: 0x60000A4
        protected virtual System.Void LowMemory() { }
        // RVA: 0x03690610  token: 0x60000A5
        protected virtual System.Void DynamicQualityChange(Beyond.Gameplay.BeyondPerformance.LogicQualityLevel current, Beyond.Gameplay.BeyondPerformance.LogicQualityLevel last) { }
        // RVA: 0x0AC4548C  token: 0x60000A6
        protected virtual System.Void DynamicThermalChange(Beyond.Gameplay.BeyondPerformance.ThermalState current, Beyond.Gameplay.BeyondPerformance.ThermalState last) { }
        // RVA: 0x0480ED30  token: 0x60000A7
        public virtual System.Int32 Version() { }
        // RVA: 0x0AC45618  token: 0x60000A8
        public virtual System.Boolean TierLimit() { }
        // RVA: 0x0473B2C0  token: 0x60000A9
        private virtual System.Int32 ApplyOrder() { }
        // RVA: 0x0AC45530  token: 0x60000AA
        private System.Void RemoveQualitySettingSaveValue(System.String settingId) { }
        // RVA: 0x0368EB70  token: 0x60000AB
        private System.Void SafeApply() { }
        // RVA: 0x04A1EC10  token: 0x60000AC
        private System.Void RemoveRegisterKey() { }
        // RVA: 0x0368E500  token: 0x60000AD
        private System.Void SafeLowMemory(System.Boolean init) { }
        // RVA: 0x03692360  token: 0x60000AE
        private System.Void CallDynamicQualityChange(Beyond.Gameplay.BeyondPerformance.LogicQualityLevel current, Beyond.Gameplay.BeyondPerformance.LogicQualityLevel last) { }
        // RVA: 0x0AC45414  token: 0x60000AF
        private virtual System.Void CallDynamicThermalChange(Beyond.Gameplay.BeyondPerformance.ThermalState current, Beyond.Gameplay.BeyondPerformance.ThermalState last) { }
        // RVA: 0x041E1670  token: 0x60000B0
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000020  // size: 0x20
    public sealed struct QualityTierComponentMatch
    {
        // Fields
        public Beyond.Scripts.Quality.Components.QualityTierComponent component;  // 0x10
        public Beyond.Scripts.Quality.Components.QualityTierComponentMatch.MatchSource matchSource;  // 0x18
        public System.Int32 matchQualityTier;  // 0x1c

    }

    // TypeToken: 0x2000022  // size: 0x20
    public class QualityTierLevel
    {
        // Fields
        public System.Int32 qualityTier;  // 0x10
        public System.Collections.Generic.List<Beyond.Scripts.Quality.Components.QualityTierComponent> qualityTierComponents;  // 0x18

        // Methods
        // RVA: 0x048E2CA0  token: 0x60000B1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000023  // size: 0x30
    public class SliderQualityComponent : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        private System.Single sliderValue;  // 0x18
        private System.Single MaxSliderValue;  // 0x1c
        private System.Single MinSliderValue;  // 0x20
        private System.Nullable<System.Single> m_currentSliderValue;  // 0x24

        // Methods
        // RVA: 0x04485340  token: 0x60000B2
        public System.Single GetValue() { }
        // RVA: 0x04D7CA10  token: 0x60000B3
        public System.Single GetDefaultValue() { }
        // RVA: 0x0AC462B0  token: 0x60000B4
        public UnityEngine.Vector2 GetRange() { }
        // RVA: 0x0AC46308  token: 0x60000B5
        public System.Void SetValue(System.Single value) { }
        // RVA: 0x041E1670  token: 0x60000B6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000024  // size: 0x20
    public class ToggleQualityComponent : Beyond.Scripts.Quality.Components.QualityTierComponent, Beyond.Scripts.Quality.Components.IQualityComponentExtension`1
    {
        // Fields
        private System.Boolean toggleVal;  // 0x18
        private System.Nullable<System.Boolean> m_currentValue;  // 0x19

        // Methods
        // RVA: 0x0368D100  token: 0x60000B7
        protected virtual System.Void Apply() { }
        // RVA: 0x0AC465F8  token: 0x60000B8
        protected virtual System.Void Toggle(System.Boolean value) { }
        // RVA: 0x0AC4646C  token: 0x60000B9
        public System.Void ChangeToggle(System.Int32 index) { }
        // RVA: 0x0368D150  token: 0x60000BA
        public virtual System.Boolean GetValue() { }
        // RVA: 0x0368D1B0  token: 0x60000BB
        public virtual System.Boolean GetDefaultValue() { }
        // RVA: 0x041E1670  token: 0x60000BC
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x60000BD
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000025  // size: 0x28
    public class CharacterRenderFeatureQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        public System.Int32 characterSelfShadowOffLodQuality;  // 0x18
        public System.Int32 characterShadowTierLevel;  // 0x1c
        public System.Int32 characterOutlineTierLevel;  // 0x20

        // Methods
        // RVA: 0x04D13B70  token: 0x60000BE
        protected virtual System.Void Apply() { }
        // RVA: 0x04D83190  token: 0x60000BF
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x60000C0
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000026  // size: 0x20
    public class ChromaticAberrationQuality : Beyond.Scripts.Quality.Components.ToggleQualityComponent
    {
        // Fields
        private static System.Int32 ENABLE_TIER;  // const
        private static System.Int32 DISABLE_TIER;  // const

        // Methods
        // RVA: 0x03C8F990  token: 0x60000C1
        protected virtual System.Void Toggle(System.Boolean value) { }
        // RVA: 0x041E1670  token: 0x60000C2
        public System.Void .ctor() { }
        // RVA: 0x0AC3BFF4  token: 0x60000C3
        public System.Void <>iFixBaseProxy_Toggle(System.Boolean P0) { }

    }

    // TypeToken: 0x2000027  // size: 0x38
    public class DLSSFrameGenQuality : Beyond.Scripts.Quality.Components.EnumQualityComponent`1
    {
        // Fields
        private System.Action<Beyond.Scripts.Quality.Components.QualityTierComponent> callback;  // 0x30

        // Methods
        // RVA: 0x04B08A20  token: 0x60000C4
        protected virtual System.Void Apply() { }
        // RVA: 0x03E514D0  token: 0x60000C5
        protected System.Void OnDeserialized(System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x044846F0  token: 0x60000C6
        private System.Void DlSSChange(Beyond.Scripts.Quality.Components.QualityTierComponent qualityTierComponent) { }
        // RVA: 0x0AC3BFFC  token: 0x60000C7
        public System.Boolean ShouldShowAuto() { }
        // RVA: 0x0AC3C048  token: 0x60000C8
        public System.Boolean ShouldShowX1() { }
        // RVA: 0x0AC3C094  token: 0x60000C9
        public System.Boolean ShouldShowX2() { }
        // RVA: 0x0AC3C0F0  token: 0x60000CA
        public System.Boolean ShouldShowX3() { }
        // RVA: 0x0AC3C14C  token: 0x60000CB
        public System.Boolean ShouldShow() { }
        // RVA: 0x04D7EB70  token: 0x60000CC
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x60000CD
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000029  // size: 0x38
    public class DLSSUpscalerModel : Beyond.Scripts.Quality.Components.EnumQualityComponent`1
    {
        // Fields
        private System.Action<Beyond.Scripts.Quality.Components.QualityTierComponent> callback;  // 0x30

        // Methods
        // RVA: 0x04484330  token: 0x60000CE
        protected virtual System.Void Apply() { }
        // RVA: 0x0AC3C1F4  token: 0x60000CF
        public System.Boolean ShouldShow() { }
        // RVA: 0x04D7EB40  token: 0x60000D0
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x60000D1
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x200002A  // size: 0x38
    public class DLSSUpscalerQuality : Beyond.Scripts.Quality.Components.EnumQualityComponent`1
    {
        // Fields
        private System.Action<Beyond.Scripts.Quality.Components.QualityTierComponent> callback;  // 0x30

        // Methods
        // RVA: 0x04483DB0  token: 0x60000D2
        protected virtual System.Void Apply() { }
        // RVA: 0x0AC3C240  token: 0x60000D3
        public System.Boolean ShouldShow() { }
        // RVA: 0x03E51570  token: 0x60000D4
        protected System.Void OnDeserialized(System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x04484790  token: 0x60000D5
        private System.Void DlSSChange(Beyond.Scripts.Quality.Components.QualityTierComponent qualityTierComponent) { }
        // RVA: 0x04D7EB10  token: 0x60000D6
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x60000D7
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x200002B  // size: 0x20
    public class EnvironmentRenderingFeatureQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        public System.Int32 environmentRenderingFeatureQualityTier;  // 0x18

        // Methods
        // RVA: 0x03C90950  token: 0x60000D8
        protected virtual System.Void Apply() { }
        // RVA: 0x041E1670  token: 0x60000D9
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x60000DA
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x200002C  // size: 0x30
    public class FrameGenQuality : Beyond.Scripts.Quality.Components.EnumQualityComponent`1
    {
        // Methods
        // RVA: 0x04484D60  token: 0x60000DB
        protected virtual System.Void Apply() { }
        // RVA: 0x0AC3E8B8  token: 0x60000DC
        public System.Boolean ShouldShow() { }
        // RVA: 0x0AC3E818  token: 0x60000DD
        public System.Boolean ShouldShowDLSS() { }
        // RVA: 0x0AC3E868  token: 0x60000DE
        public System.Boolean ShouldShowFSR3() { }
        // RVA: 0x04D7DC10  token: 0x60000DF
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x60000E0
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x200002E  // size: 0x20
    public class FrameGenQualityMobile : Beyond.Scripts.Quality.Components.ToggleQualityComponent
    {
        // Methods
        // RVA: 0x0AC3E638  token: 0x60000E1
        protected virtual System.Void Apply() { }
        // RVA: 0x0AC3E744  token: 0x60000E2
        public System.Boolean ShouldShow() { }
        // RVA: 0x041E1670  token: 0x60000E3
        public System.Void .ctor() { }
        // RVA: 0x0AC3E810  token: 0x60000E4
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x200002F  // size: 0x30
    public class FrameRateQuality : Beyond.Scripts.Quality.Components.EnumQualityComponent`1
    {
        // Properties
        System.Int32 frameRate { get; /* RVA: 0x048223B0 */ }

        // Methods
        // RVA: 0x04822410  token: 0x60000E6
        public static System.Int32 GetFrameRateFromEnum(Beyond.Scripts.Quality.Components.FrameRateQuality.FrameRateQualityEnum enumValue) { }
        // RVA: 0x04D7DBE0  token: 0x60000E7
        public virtual System.Int32 Version() { }
        // RVA: 0x0AC3EE64  token: 0x60000E8
        public System.Boolean ShouldShow() { }
        // RVA: 0x04822330  token: 0x60000E9
        protected virtual System.Void Apply() { }
        // RVA: 0x0AC3ECF4  token: 0x60000EA
        private System.Void DlSSChange(Beyond.Scripts.Quality.Components.QualityTierComponent qualityTierComponent) { }
        // RVA: 0x039BA9A0  token: 0x60000EB
        protected System.Void OnDeserialized(System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AC3EF0C  token: 0x60000EC
        private System.Boolean _FPS30Enabled() { }
        // RVA: 0x0AC3EF58  token: 0x60000ED
        private System.Boolean _FPS60Enabled() { }
        // RVA: 0x0AC3EFA4  token: 0x60000EE
        private System.Boolean _FPSBNoLimitEnabled() { }
        // RVA: 0x0AC3EFF0  token: 0x60000EF
        private System.Boolean _IsGDLSSGDisable() { }
        // RVA: 0x04D7DBB0  token: 0x60000F0
        public System.Void .ctor() { }
        // RVA: 0x074559DC  token: 0x60000F1
        public System.Int32 <>iFixBaseProxy_Version() { }
        // RVA: 0x06D6601C  token: 0x60000F2
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000031  // size: 0x10
    public class HgFrameRateControl
    {
        // Fields
        public static System.Int32 s_cinematicFrameRate;  // static @ 0x0
        public static System.Collections.Generic.Stack<System.ValueTuple<System.Int32,System.Int32,Beyond.Scripts.Quality.Components.HgFrameRateControl.ReasonEnum>> s_rateStack;  // static @ 0x8
        public static System.Action<System.Int32,System.Int32> s_OnFrameRateChanged;  // static @ 0x10

        // Methods
        // RVA: 0x04822570  token: 0x60000F3
        public static System.Int32 GetDefaultFrameRate() { }
        // RVA: 0x04822450  token: 0x60000F4
        public static System.Void Init() { }
        // RVA: 0x0AC3F864  token: 0x60000F5
        public static System.Void SetFrameRateOverrider(System.Int32 frameRate, Beyond.Scripts.Quality.Components.HgFrameRateControl.ReasonEnum reason) { }
        // RVA: 0x0AC3F41C  token: 0x60000F6
        public static System.Int32 GetFrameRate() { }
        // RVA: 0x0AC3F594  token: 0x60000F7
        public static System.Boolean IsHighFrameRate() { }
        // RVA: 0x0AC3F5B8  token: 0x60000F8
        public static System.Int32 RecommendedDefaultFrameRate() { }
        // RVA: 0x0AC3F460  token: 0x60000F9
        public static System.Int32 GetUserSettingFrameRate() { }
        // RVA: 0x048225A0  token: 0x60000FA
        public static System.Void __ForceSetFrameRate(System.Int32 frameRate, Beyond.Scripts.Quality.Components.HgFrameRateControl.ReasonEnum reason) { }
        // RVA: 0x0AC3F6EC  token: 0x60000FB
        public static System.Void ResetFrameRate() { }
        // RVA: 0x041E1670  token: 0x60000FC
        public System.Void .ctor() { }
        // RVA: 0x048224C0  token: 0x60000FD
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000033  // size: 0x38
    public class FrameRateQualityMobile : Beyond.Scripts.Quality.Components.EnumQualityComponent`1
    {
        // Fields
        private System.Action<Beyond.Scripts.Quality.Components.QualityTierComponent> callback;  // 0x30

        // Properties
        System.Int32 frameRate { get; /* RVA: 0x0AC3EC78 */ }

        // Methods
        // RVA: 0x0AC3E9A8  token: 0x60000FF
        public static System.Int32 GetFrameRateFromEnum(Beyond.Scripts.Quality.Components.FrameRateQualityMobile.FrameRateQualityMobileEnum enumValue) { }
        // RVA: 0x0AC3EC00  token: 0x6000100
        public virtual System.Int32 Version() { }
        // RVA: 0x0AC3E920  token: 0x6000101
        protected virtual System.Void Apply() { }
        // RVA: 0x0AC3EA1C  token: 0x6000102
        protected System.Void OnDeserialized(System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AC3EC50  token: 0x6000103
        public System.Void .ctor() { }
        // RVA: 0x074559DC  token: 0x6000104
        public System.Int32 <>iFixBaseProxy_Version() { }
        // RVA: 0x06D6601C  token: 0x6000105
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000035  // size: 0x38
    public class FSR3UpscalerQuality : Beyond.Scripts.Quality.Components.EnumQualityComponent`1
    {
        // Fields
        private System.Action<Beyond.Scripts.Quality.Components.QualityTierComponent> callback;  // 0x30

        // Methods
        // RVA: 0x04483F70  token: 0x6000106
        protected virtual System.Void Apply() { }
        // RVA: 0x0AC3E580  token: 0x6000107
        public System.Boolean ShouldShow() { }
        // RVA: 0x03E51610  token: 0x6000108
        protected System.Void OnDeserialized(System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x04484860  token: 0x6000109
        private System.Void FSR3Change(Beyond.Scripts.Quality.Components.QualityTierComponent qualityTierComponent) { }
        // RVA: 0x04D7DD90  token: 0x600010A
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x600010B
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000036  // size: 0x20
    public class GrassSparsityQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        public System.Int32 grassSparsityQualityTier;  // 0x18

        // Methods
        // RVA: 0x03C8FA20  token: 0x600010C
        protected virtual System.Void Apply() { }
        // RVA: 0x041E1670  token: 0x600010D
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x600010E
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000037  // size: 0x20
    public class HGAmbientOcclusionQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        public System.Int32 aoQualityTier;  // 0x18

        // Methods
        // RVA: 0x03C909F0  token: 0x600010F
        protected virtual System.Void Apply() { }
        // RVA: 0x041E1670  token: 0x6000110
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x6000111
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000038  // size: 0x30
    public class HGAnisoLevelQuality : Beyond.Scripts.Quality.Components.EnumQualityComponent`1
    {
        // Methods
        // RVA: 0x04B87D30  token: 0x6000112
        protected virtual System.Void InitApply() { }
        // RVA: 0x0AC3F098  token: 0x6000113
        protected virtual System.Void Apply() { }
        // RVA: 0x04D7D9A0  token: 0x6000114
        public System.Void .ctor() { }
        // RVA: 0x0AC3F19C  token: 0x6000115
        public System.Void <>iFixBaseProxy_InitApply() { }
        // RVA: 0x06D6601C  token: 0x6000116
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x200003A  // size: 0x20
    public class HGContactShadowQuality : Beyond.Scripts.Quality.Components.ToggleQualityComponent
    {
        // Fields
        private static System.Int32 ENABLE_TIER;  // const
        private static System.Int32 DISABLE_TIER;  // const

        // Methods
        // RVA: 0x03C8FA90  token: 0x6000117
        protected virtual System.Void Toggle(System.Boolean value) { }
        // RVA: 0x041E1670  token: 0x6000118
        public System.Void .ctor() { }
        // RVA: 0x0AC3BFF4  token: 0x6000119
        public System.Void <>iFixBaseProxy_Toggle(System.Boolean P0) { }

    }

    // TypeToken: 0x200003B  // size: 0x18
    public class HGIrradianceVolumeQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Methods
        // RVA: 0x0AC3F1A4  token: 0x600011A
        protected virtual System.Void LowMemory() { }
        // RVA: 0x041E1670  token: 0x600011B
        public System.Void .ctor() { }
        // RVA: 0x06D66024  token: 0x600011C
        public System.Void <>iFixBaseProxy_LowMemory() { }

    }

    // TypeToken: 0x200003C  // size: 0x20
    public class HGLODStreamingComponent : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        public System.Boolean enableLODStreaming;  // 0x18

        // Methods
        // RVA: 0x04D33920  token: 0x600011D
        protected virtual System.Void Apply() { }
        // RVA: 0x0AC3F1E8  token: 0x600011E
        protected virtual System.Void LowMemory() { }
        // RVA: 0x041E1670  token: 0x600011F
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x6000120
        public System.Void <>iFixBaseProxy_Apply() { }
        // RVA: 0x06D66024  token: 0x6000121
        public System.Void <>iFixBaseProxy_LowMemory() { }

    }

    // TypeToken: 0x200003D  // size: 0x18
    public class HGReflectionProbeBinningQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Methods
        // RVA: 0x0AC3F2E4  token: 0x6000122
        protected virtual System.Void LowMemory() { }
        // RVA: 0x041E1670  token: 0x6000123
        public System.Void .ctor() { }
        // RVA: 0x06D66024  token: 0x6000124
        public System.Void <>iFixBaseProxy_LowMemory() { }

    }

    // TypeToken: 0x200003E  // size: 0x20
    public class HGRPTierQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        public System.Int32 HGRP_QualityTier;  // 0x18

        // Methods
        // RVA: 0x03C90860  token: 0x6000125
        protected virtual System.Void Apply() { }
        // RVA: 0x04D77930  token: 0x6000126
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x6000127
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x200003F  // size: 0x20
    public class HGScreenSpaceReflectionQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        public System.Int32 ssrQualityTier;  // 0x18

        // Methods
        // RVA: 0x03C8FB20  token: 0x6000128
        protected virtual System.Void Apply() { }
        // RVA: 0x041E1670  token: 0x6000129
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x600012A
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000040  // size: 0x20
    public class HGShadowQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        public System.Int32 shadowQualityTier;  // 0x18

        // Methods
        // RVA: 0x03C8FB90  token: 0x600012B
        protected virtual System.Void Apply() { }
        // RVA: 0x041E1670  token: 0x600012C
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x600012D
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000041  // size: 0x30
    public class HGTextureQuality : Beyond.Scripts.Quality.Components.EnumQualityComponent`1
    {
        // Properties
        System.Int32 textureQualityTier { get; /* RVA: 0x03C8FCB0 */ }

        // Methods
        // RVA: 0x03C8FC00  token: 0x600012F
        protected virtual System.Void Apply() { }
        // RVA: 0x04D7D8E0  token: 0x6000130
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x6000131
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000043  // size: 0x20
    public class HGVolumetricCloudQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        public System.Int32 volumetricCloudQualityTier;  // 0x18

        // Methods
        // RVA: 0x03C90780  token: 0x6000132
        private System.Int32 GetShaderLod() { }
        // RVA: 0x03C906F0  token: 0x6000133
        protected virtual System.Void Apply() { }
        // RVA: 0x0AC3F3C4  token: 0x6000134
        public System.Boolean ShouldShowNoExLow() { }
        // RVA: 0x0AC3F36C  token: 0x6000135
        public System.Boolean ShouldShowExLow() { }
        // RVA: 0x041E1670  token: 0x6000136
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x6000137
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000044  // size: 0x20
    public class HGVolumetricFogQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        public System.Int32 volumetricFogQualityTier;  // 0x18

        // Methods
        // RVA: 0x03C907F0  token: 0x6000138
        protected virtual System.Void Apply() { }
        // RVA: 0x041E1670  token: 0x6000139
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x600013A
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000045  // size: 0x20
    public class LODGroupQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        public System.Single lodBias;  // 0x18
        public System.Int32 lodOffset;  // 0x1c

        // Methods
        // RVA: 0x0AC42504  token: 0x600013B
        protected virtual System.Void Apply() { }
        // RVA: 0x041E1670  token: 0x600013C
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x600013D
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000046  // size: 0x20
    public class PhysicsClothQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        public System.Boolean UsePhysicCloth;  // 0x18
        public System.Boolean UseCrossFrameJob;  // 0x19

        // Methods
        // RVA: 0x0AC426F4  token: 0x600013E
        protected virtual System.Void Apply() { }
        // RVA: 0x04DA9A50  token: 0x600013F
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x6000140
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000047  // size: 0x38
    public class PrefabInstantiateQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        public System.Single normalMaxUpdateTimeInMsThreshold;  // 0x18
        public System.Single busyMaxUpdateTimeInMsThreshold;  // 0x1c
        public System.Single burstMaxUpdateTimeInMsThreshold;  // 0x20
        public System.Single normalUnloadMaxUpdateTimeInMsThreshold;  // 0x24
        public System.Single burstUnloadMaxUpdateTimeInMsThreshold;  // 0x28
        public System.Single normalCallbackMaxUpdateTimeInMsThreshold;  // 0x2c
        public System.Single burstCallbackMaxUpdateTimeInMsThreshold;  // 0x30
        public System.Int32 normalMaxDestroyCount;  // 0x34

        // Methods
        // RVA: 0x0AC427A4  token: 0x6000141
        protected virtual System.Void Apply() { }
        // RVA: 0x04DA9A60  token: 0x6000142
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x6000143
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000048  // size: 0x38
    public class PreloadLoadSettingQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        public System.Single normalMaxUpdateTimeInMsThreshold;  // 0x18
        public System.Single busyMaxUpdateTimeInMsThreshold;  // 0x1c
        public System.Single burstMaxUpdateTimeInMsThreshold;  // 0x20
        public System.Single normalUnloadMaxUpdateTimeInMsThreshold;  // 0x24
        public System.Single burstUnloadMaxUpdateTimeInMsThreshold;  // 0x28
        public System.Single normalCallbackMaxUpdateTimeInMsThreshold;  // 0x2c
        public System.Single burstCallbackMaxUpdateTimeInMsThreshold;  // 0x30
        public System.Single preloadManagerUnloadKeepTime;  // 0x34

        // Methods
        // RVA: 0x04B0D360  token: 0x6000144
        protected virtual System.Void Apply() { }
        // RVA: 0x04D51130  token: 0x6000145
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x6000146
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000049  // size: 0x20
    public class RayTracingQuality : Beyond.Scripts.Quality.Components.ToggleQualityComponent
    {
        // Properties
        System.Boolean supportsRayTracing { get; /* RVA: 0x04A1EB60 */ }

        // Methods
        // RVA: 0x04A1EB00  token: 0x6000148
        protected virtual System.Void InitApply() { }
        // RVA: 0x0AC459E0  token: 0x6000149
        protected virtual System.Void Apply() { }
        // RVA: 0x0AC45AD4  token: 0x600014A
        public System.Boolean ShouldShow() { }
        // RVA: 0x04D7CBF0  token: 0x600014B
        private virtual System.Int32 ApplyOrder() { }
        // RVA: 0x041E1670  token: 0x600014C
        public System.Void .ctor() { }
        // RVA: 0x0AC3F19C  token: 0x600014D
        public System.Void <>iFixBaseProxy_InitApply() { }
        // RVA: 0x0AC3E810  token: 0x600014E
        public System.Void <>iFixBaseProxy_Apply() { }
        // RVA: 0x0AC45B24  token: 0x600014F
        public System.Int32 <>iFixBaseProxy_ApplyOrder() { }

    }

    // TypeToken: 0x200004A  // size: 0x38
    public class ReflexQuality : Beyond.Scripts.Quality.Components.EnumQualityComponent`1
    {
        // Fields
        private System.Action<Beyond.Scripts.Quality.Components.QualityTierComponent> callback;  // 0x30

        // Methods
        // RVA: 0x044853B0  token: 0x6000150
        protected virtual System.Void Apply() { }
        // RVA: 0x0AC45C8C  token: 0x6000151
        public System.Boolean ShouldShow() { }
        // RVA: 0x03E516B0  token: 0x6000152
        protected System.Void OnDeserialized(System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AC45B2C  token: 0x6000153
        private System.Void DlSSChange(Beyond.Scripts.Quality.Components.QualityTierComponent qualityTierComponent) { }
        // RVA: 0x04D7CBC0  token: 0x6000154
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x6000155
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x200004B  // size: 0x30
    public class RenderingScaleQualityMobile : Beyond.Scripts.Quality.Components.EnumQualityComponent`1
    {
        // Properties
        System.Single renderingScaleFactor { get; /* RVA: 0x0AC45E80 */ }

        // Methods
        // RVA: 0x0AC45D4C  token: 0x6000157
        protected virtual System.Void Apply() { }
        // RVA: 0x0AC45E58  token: 0x6000158
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x6000159
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x200004D  // size: 0x38
    public class RenderingScaleQualityPC : Beyond.Scripts.Quality.Components.EnumQualityComponent`1
    {
        // Fields
        private System.Action<Beyond.Scripts.Quality.Components.QualityTierComponent> callback;  // 0x30

        // Properties
        System.Single renderingScale { get; /* RVA: 0x04484BB0 */ }

        // Methods
        // RVA: 0x04484A60  token: 0x600015B
        protected virtual System.Void Apply() { }
        // RVA: 0x0AC45F4C  token: 0x600015C
        public System.Boolean ShouldShow() { }
        // RVA: 0x03E51750  token: 0x600015D
        protected System.Void OnDeserialized(System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x04484930  token: 0x600015E
        private System.Void DlSSChange(Beyond.Scripts.Quality.Components.QualityTierComponent qualityTierComponent) { }
        // RVA: 0x04D7CB30  token: 0x600015F
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x6000160
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x200004F  // size: 0x50
    public class ResLoadSettingQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        public System.Boolean debugCloseBundleUnload;  // 0x18
        public System.Boolean debugCloseBundleUnloadUntilBurstMode;  // 0x19
        public System.Boolean enableForceSyncUnload;  // 0x1a
        public System.Boolean enableUnloadLocker;  // 0x1b
        public System.Int32 enableUnloadLockerBundleNum;  // 0x1c
        public System.Int32 maxUnloadOpNum_Normal;  // 0x20
        public System.Int32 maxUnloadOpNum_BusyMode;  // 0x24
        public System.Int32 bundleFrameDelayToUnload_NormalMode;  // 0x28
        public System.Int32 bundleFrameDelayToUnload_BusyMode;  // 0x2c
        public System.Int32 bundleFrameDelayToUnload_BurstMode;  // 0x30
        public System.Int32 bundleFrameDelayToUnload_SwitchScene;  // 0x34
        public System.Int32 bundleFrameDelayToUnload_Fast;  // 0x38
        public System.Int32 enterBusyOperationNum;  // 0x3c
        public System.Int32 exitBusyOperationNum;  // 0x40
        public System.Single unloadBudgetFloatingCoefficient;  // 0x44
        public System.Single unloadMaxFloatingBudget;  // 0x48

        // Methods
        // RVA: 0x04A7D020  token: 0x6000161
        protected virtual System.Void Apply() { }
        // RVA: 0x0AC46034  token: 0x6000162
        protected virtual System.Void LowMemory() { }
        // RVA: 0x04D10F30  token: 0x6000163
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x6000164
        public System.Void <>iFixBaseProxy_Apply() { }
        // RVA: 0x06D66024  token: 0x6000165
        public System.Void <>iFixBaseProxy_LowMemory() { }

    }

    // TypeToken: 0x2000050  // size: 0x20
    public class SceneDetailQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        public System.Int32 sceneDetailQualityTier;  // 0x18

        // Methods
        // RVA: 0x03C90A90  token: 0x6000166
        protected virtual System.Void Apply() { }
        // RVA: 0x041E1670  token: 0x6000167
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x6000168
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000051  // size: 0x20
    public class ShaderLodQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        public Beyond.Scripts.Quality.Components.ShaderLodQuality.ShaderLodEnum ShaderLod;  // 0x18

        // Methods
        // RVA: 0x04CD9040  token: 0x6000169
        protected virtual System.Void InitApply() { }
        // RVA: 0x0AC460FC  token: 0x600016A
        protected virtual System.Void Apply() { }
        // RVA: 0x041E1670  token: 0x600016B
        public System.Void .ctor() { }
        // RVA: 0x0AC3F19C  token: 0x600016C
        public System.Void <>iFixBaseProxy_InitApply() { }
        // RVA: 0x06D6601C  token: 0x600016D
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000053  // size: 0x30
    public class SharpnessQuality : Beyond.Scripts.Quality.Components.SliderQualityComponent
    {
        // Methods
        // RVA: 0x04484C10  token: 0x600016E
        protected virtual System.Void Apply() { }
        // RVA: 0x0AC461CC  token: 0x600016F
        public System.Boolean ShouldShow() { }
        // RVA: 0x041E1670  token: 0x6000170
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x6000171
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000054  // size: 0x30
    public class SliderTestComponent : Beyond.Scripts.Quality.Components.SliderQualityComponent
    {
        // Methods
        // RVA: 0x0AC463B4  token: 0x6000172
        protected virtual System.Void Apply() { }
        // RVA: 0x041E1670  token: 0x6000173
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x6000174
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000055  // size: 0x20
    public class TickExclusiveQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        public Beyond.TickExclusiveMode quality;  // 0x18
        private Beyond.Gameplay.BeyondPerformance.LogicQualityLevel m_currentQuality;  // 0x1c
        private Beyond.Gameplay.BeyondPerformance.ThermalState m_currentThermalState;  // 0x1d

        // Methods
        // RVA: 0x043ECE90  token: 0x6000175
        protected virtual System.Void Apply() { }
        // RVA: 0x043ECEF0  token: 0x6000176
        protected virtual System.Void DynamicQualityChange(Beyond.Gameplay.BeyondPerformance.LogicQualityLevel current, Beyond.Gameplay.BeyondPerformance.LogicQualityLevel last) { }
        // RVA: 0x0AC463F8  token: 0x6000177
        protected virtual System.Void DynamicThermalChange(Beyond.Gameplay.BeyondPerformance.ThermalState current, Beyond.Gameplay.BeyondPerformance.ThermalState last) { }
        // RVA: 0x043ECF40  token: 0x6000178
        private System.Void _OnQualityOrThermalChanged() { }
        // RVA: 0x04D84AB0  token: 0x6000179
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x600017A
        public System.Void <>iFixBaseProxy_Apply() { }
        // RVA: 0x070A9134  token: 0x600017B
        public System.Void <>iFixBaseProxy_DynamicQualityChange(Beyond.Gameplay.BeyondPerformance.LogicQualityLevel P0, Beyond.Gameplay.BeyondPerformance.LogicQualityLevel P1) { }
        // RVA: 0x070A913C  token: 0x600017C
        public System.Void <>iFixBaseProxy_DynamicThermalChange(Beyond.Gameplay.BeyondPerformance.ThermalState P0, Beyond.Gameplay.BeyondPerformance.ThermalState P1) { }

    }

    // TypeToken: 0x2000056  // size: 0x30
    public class UpscalerQuality : Beyond.Scripts.Quality.Components.EnumQualityComponent`1
    {
        // Methods
        // RVA: 0x04484FB0  token: 0x600017D
        protected virtual System.Void Apply() { }
        // RVA: 0x0AC46720  token: 0x600017E
        public System.Boolean ShouldShow() { }
        // RVA: 0x0AC46680  token: 0x600017F
        public System.Boolean ShouldShowDLSS() { }
        // RVA: 0x0AC466D0  token: 0x6000180
        public System.Boolean ShouldShowFSR3() { }
        // RVA: 0x04D7C650  token: 0x6000181
        public virtual System.Int32 Version() { }
        // RVA: 0x04D7C620  token: 0x6000182
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x6000183
        public System.Void <>iFixBaseProxy_Apply() { }
        // RVA: 0x074559DC  token: 0x6000184
        public System.Int32 <>iFixBaseProxy_Version() { }

    }

    // TypeToken: 0x2000058  // size: 0x20
    public class VSyncQuality : Beyond.Scripts.Quality.Components.QualityTierComponent
    {
        // Fields
        public System.Boolean bVSync;  // 0x18

        // Methods
        // RVA: 0x0AC46864  token: 0x6000185
        protected virtual System.Void Apply() { }
        // RVA: 0x0AC468BC  token: 0x6000186
        protected System.Void Remove() { }
        // RVA: 0x04CDE570  token: 0x6000187
        public static System.Void Init() { }
        // RVA: 0x04D8EB60  token: 0x6000188
        public System.Void .ctor() { }
        // RVA: 0x06D6601C  token: 0x6000189
        public System.Void <>iFixBaseProxy_Apply() { }

    }

    // TypeToken: 0x2000059  // size: 0x28
    public class VSyncQualityV2 : Beyond.Scripts.Quality.Components.ToggleQualityComponent
    {
        // Fields
        private System.Action<Beyond.Scripts.Quality.Components.QualityTierComponent> callback;  // 0x20

        // Methods
        // RVA: 0x04B4A190  token: 0x600018A
        protected virtual System.Void Toggle(System.Boolean value) { }
        // RVA: 0x04CDE5A0  token: 0x600018B
        public static System.Void Init() { }
        // RVA: 0x04D7C5C0  token: 0x600018C
        public virtual System.Int32 Version() { }
        // RVA: 0x03E517F0  token: 0x600018D
        protected System.Void OnDeserialized(System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x02E85650  token: 0x600018E
        private System.Void FrameGenChange(Beyond.Scripts.Quality.Components.QualityTierComponent qualityTierComponent) { }
        // RVA: 0x0AC46770  token: 0x600018F
        public System.Boolean ShouldShow() { }
        // RVA: 0x041E1670  token: 0x6000190
        public System.Void .ctor() { }
        // RVA: 0x0AC3BFF4  token: 0x6000191
        public System.Void <>iFixBaseProxy_Toggle(System.Boolean P0) { }
        // RVA: 0x074559DC  token: 0x6000192
        public System.Int32 <>iFixBaseProxy_Version() { }

    }

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
        // RVA: 0x041E1670  token: 0x6000004
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000005  // size: 0x10
    public static class AnimatorBlackboardUtility
    {
        // Fields
        private static System.Boolean s_enableFastPathBuffer;  // static @ 0x0

        // Methods
        // RVA: -1  // generic def  token: 0x6000005
        private static System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity) { }

    }

    // TypeToken: 0x2000006  // size: 0x10
    public class AnimatorBlackboardAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000006
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x10
    public class CameraControlConfigAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000007
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000008  // size: 0x18
    public class ECSComponentAttribute : System.Attribute
    {
        // Fields
        public System.Runtime.InteropServices.LayoutKind layoutKind;  // 0x10
        public System.Boolean isTag;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x6000008
        public System.Void .ctor() { }

    }

}

namespace HG.Rendering.Runtime
{

    // TypeToken: 0x2000002  // size: 0x10
    public class HGConstantBufferLayoutAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000001
        public System.Void .ctor() { }

    }

}

namespace IFix
{

    // TypeToken: 0x200005A  // size: 0x28
    public class ILFixDynamicMethodWrapper
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10
        private System.Int32 methodId;  // 0x18
        private System.Object anonObj;  // 0x20
        public static IFix.ILFixDynamicMethodWrapper[] wrapperArray;  // static @ 0x0

        // Methods
        // RVA: 0x032B2C30  token: 0x6000193
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj) { }
        // RVA: 0x063625A4  token: 0x6000194
        public System.Void __Gen_Wrap_0() { }
        // RVA: 0x06377B18  token: 0x6000195
        public System.Void __Gen_Wrap_1(System.Object P0, System.Object P1) { }
        // RVA: 0x0AC40D48  token: 0x6000196
        public System.String __Gen_Wrap_2(Beyond.Scripts.Quality.RankingRule& P0) { }
        // RVA: 0x0AC414F0  token: 0x6000197
        public System.String __Gen_Wrap_3(Beyond.Scripts.Quality.DeviceMatchResult& P0) { }
        // RVA: 0x0AC41A54  token: 0x6000198
        public System.String __Gen_Wrap_4(System.Object P0) { }
        // RVA: 0x0AC42144  token: 0x6000199
        public System.Boolean __Gen_Wrap_5(Beyond.Scripts.Quality.DeviceMatchResult& P0) { }
        // RVA: 0x063632E4  token: 0x600019A
        public System.Boolean __Gen_Wrap_6(System.Object P0) { }
        // RVA: 0x0AC42348  token: 0x600019B
        public System.String __Gen_Wrap_7() { }
        // RVA: 0x0AC42414  token: 0x600019C
        public Beyond.Scripts.Quality.DeviceMatchRules __Gen_Wrap_8(System.Object P0) { }
        // RVA: 0x06360610  token: 0x600019D
        public System.Int32 __Gen_Wrap_9(System.Object P0, System.Object P1) { }
        // RVA: 0x0AC3F954  token: 0x600019E
        public System.String __Gen_Wrap_10(System.Object P0, System.Object P1) { }
        // RVA: 0x0AC3FA60  token: 0x600019F
        public System.Boolean __Gen_Wrap_11(System.Object P0, System.Object P1, System.Object P2, Beyond.Scripts.Quality.RankingRule& P3) { }
        // RVA: 0x0AC3FBF8  token: 0x60001A0
        public System.Boolean __Gen_Wrap_12(System.Object P0, System.Object P1, System.Object P2, Beyond.Scripts.Quality.RankingRule& P3, System.String& P4) { }
        // RVA: 0x063633B4  token: 0x60001A1
        public System.Void __Gen_Wrap_13(System.Object P0) { }
        // RVA: 0x0AC3FDD8  token: 0x60001A2
        public System.Boolean __Gen_Wrap_14(System.Object P0, System.Object P1, Beyond.Scripts.Quality.RankingRule& P2) { }
        // RVA: 0x0AC3FF48  token: 0x60001A3
        public System.Boolean __Gen_Wrap_15(Beyond.Scripts.Quality.RankingRule& P0, Beyond.Scripts.Quality.RankingRule P1) { }
        // RVA: 0x0AC400B8  token: 0x60001A4
        public System.String __Gen_Wrap_16(Beyond.Scripts.Quality.MemoryRule& P0) { }
        // RVA: 0x0AC4020C  token: 0x60001A5
        public System.Boolean __Gen_Wrap_17(Beyond.Scripts.Quality.MemoryRule& P0, Beyond.Scripts.Quality.MemoryRule P1) { }
        // RVA: 0x0AC40388  token: 0x60001A6
        public System.String __Gen_Wrap_18(Beyond.Scripts.Quality.DeviceQualityMapItem& P0) { }
        // RVA: 0x0AC404F4  token: 0x60001A7
        public UnityEngine.Color __Gen_Wrap_19(Beyond.Scripts.Quality.DeviceQualityMapItem& P0, System.Int32 P1) { }
        // RVA: 0x0AC40698  token: 0x60001A8
        public UnityEngine.Color __Gen_Wrap_20(Beyond.Scripts.Quality.DeviceQualityMapItem& P0) { }
        // RVA: 0x0AC40820  token: 0x60001A9
        public Beyond.Scripts.Quality.DeviceQualityMapItem __Gen_Wrap_21(System.Object P0) { }
        // RVA: 0x06360E84  token: 0x60001AA
        public System.Int32 __Gen_Wrap_22(System.Object P0) { }
        // RVA: 0x0AC4092C  token: 0x60001AB
        public Beyond.Scripts.Quality.DeviceInfo __Gen_Wrap_23(System.Object P0) { }
        // RVA: 0x06366E40  token: 0x60001AC
        public System.Int32 __Gen_Wrap_24(Beyond.Scripts.Quality.Components.QualityTierLevel P0) { }
        // RVA: 0x0AC40A1C  token: 0x60001AD
        public System.Int32 __Gen_Wrap_25(Beyond.Scripts.Quality.DeviceQualityMapItem P0) { }
        // RVA: 0x0AC40B20  token: 0x60001AE
        public Beyond.Scripts.Quality.Components.QualityTierComponentMatch __Gen_Wrap_26(System.Object P0, System.Object P1, System.Int32 P2) { }
        // RVA: 0x0AC40C58  token: 0x60001AF
        public System.Int32 __Gen_Wrap_27(Beyond.Scripts.Quality.Components.QualityTierComponentMatch P0) { }
        // RVA: 0x06364290  token: 0x60001B0
        public System.Void __Gen_Wrap_28(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x066C2B40  token: 0x60001B1
        public System.Void __Gen_Wrap_29(System.Object P0, System.Boolean P1) { }
        // RVA: 0x0AC40EA4  token: 0x60001B2
        public Beyond.Scripts.Quality.Components.QualityTierComponentMatch __Gen_Wrap_30(System.Object P0, System.Object P1) { }
        // RVA: 0x066F3838  token: 0x60001B3
        public System.Void __Gen_Wrap_31(System.Object P0, System.Int32 P1) { }
        // RVA: 0x0AC40FCC  token: 0x60001B4
        public System.Void __Gen_Wrap_32(System.Object P0, Beyond.Gameplay.BeyondPerformance.LogicQualityLevel P1, Beyond.Gameplay.BeyondPerformance.LogicQualityLevel P2) { }
        // RVA: 0x0AC410C4  token: 0x60001B5
        public System.Void __Gen_Wrap_33(System.Object P0, Beyond.EventData<Beyond.Gameplay.BeyondPerformance.LogicQualityLevel,Beyond.Gameplay.BeyondPerformance.LogicQualityLevel>& P1) { }
        // RVA: 0x0AC411E8  token: 0x60001B6
        public System.Void __Gen_Wrap_34(System.Object P0, Beyond.Gameplay.BeyondPerformance.ThermalState P1, Beyond.Gameplay.BeyondPerformance.ThermalState P2) { }
        // RVA: 0x0AC412CC  token: 0x60001B7
        public System.Void __Gen_Wrap_35(System.Object P0, Beyond.EventData<Beyond.Gameplay.BeyondPerformance.ThermalState,Beyond.Gameplay.BeyondPerformance.ThermalState>& P1) { }
        // RVA: 0x0AC413E4  token: 0x60001B8
        public System.Type __Gen_Wrap_36(System.Object P0, System.Object P1) { }
        // RVA: 0x066F4838  token: 0x60001B9
        public System.Void __Gen_Wrap_37(System.Object P0, System.Object P1, System.Int32 P2) { }
        // RVA: 0x066F3838  token: 0x60001BA
        public System.Void __Gen_Wrap_38(System.Object P0, Beyond.GameSetting.GameSettingVideoQuality P1) { }
        // RVA: 0x06360E84  token: 0x60001BB
        public Beyond.GameSetting.GameSettingVideoQuality __Gen_Wrap_39(System.Object P0) { }
        // RVA: 0x063623A8  token: 0x60001BC
        public System.Boolean __Gen_Wrap_40() { }
        // RVA: 0x06366BC4  token: 0x60001BD
        public System.Boolean __Gen_Wrap_41(System.Object P0, System.Object P1) { }
        // RVA: 0x06360610  token: 0x60001BE
        public Beyond.Scripts.Quality.Components.QualityTierType __Gen_Wrap_42(System.Object P0, System.Object P1) { }
        // RVA: 0x066C288C  token: 0x60001BF
        public System.Void __Gen_Wrap_43(System.Object P0, System.Single P1) { }
        // RVA: 0x071C15C8  token: 0x60001C0
        public System.Void __Gen_Wrap_44(System.Object P0, System.Object P1, System.Single P2) { }
        // RVA: 0x0AC41658  token: 0x60001C1
        public UnityEngine.Vector2 __Gen_Wrap_45(System.Object P0) { }
        // RVA: 0x066C0730  token: 0x60001C2
        public System.Single __Gen_Wrap_46(System.Object P0) { }
        // RVA: 0x0AC41734  token: 0x60001C3
        public UnityEngine.Vector3 __Gen_Wrap_47(System.Object P0, System.Object P1) { }
        // RVA: 0x0AC41854  token: 0x60001C4
        public System.Boolean __Gen_Wrap_48(System.Object P0, System.Object P1, System.Object P2, System.Object P3) { }
        // RVA: 0x0AC4195C  token: 0x60001C5
        public System.ValueTuple<System.Single,System.Single> __Gen_Wrap_49(System.Object P0, System.Object P1) { }
        // RVA: 0x09C88C8C  token: 0x60001C6
        public System.Int32 __Gen_Wrap_50(System.Int32 P0, System.Int32 P1) { }
        // RVA: 0x066C0800  token: 0x60001C7
        public System.Single __Gen_Wrap_51(System.Object P0, System.Object P1) { }
        // RVA: 0x0AC41B44  token: 0x60001C8
        public System.Int32 __Gen_Wrap_52(Beyond.Scripts.Quality.Components.QualityTierComponentMatch P0, Beyond.Scripts.Quality.Components.QualityTierComponentMatch P1) { }
        // RVA: 0x0AC41C6C  token: 0x60001C9
        public System.Void __Gen_Wrap_53(System.Object P0, System.Object P1, System.Int32& P2, System.Int32& P3) { }
        // RVA: 0x0AC41DA8  token: 0x60001CA
        public System.Boolean __Gen_Wrap_54(System.Object P0, System.Object P1, System.Int32 P2) { }
        // RVA: 0x0AC41E9C  token: 0x60001CB
        public System.Byte[] __Gen_Wrap_55(System.Object P0) { }
        // RVA: 0x0AC41F78  token: 0x60001CC
        public Newtonsoft.Json.JsonSerializerSettings __Gen_Wrap_56() { }
        // RVA: 0x0AC42044  token: 0x60001CD
        public System.Void __Gen_Wrap_57(System.Object P0, System.Runtime.Serialization.StreamingContext P1) { }
        // RVA: 0x0A8A0364  token: 0x60001CE
        public System.Int32 __Gen_Wrap_58(Beyond.Scripts.Quality.Components.FrameRateQuality.FrameRateQualityEnum P0) { }
        // RVA: 0x07199E0C  token: 0x60001CF
        public System.Void __Gen_Wrap_59(System.Int32 P0, Beyond.Scripts.Quality.Components.HgFrameRateControl.ReasonEnum P1) { }
        // RVA: 0x06377214  token: 0x60001D0
        public System.Int32 __Gen_Wrap_60() { }
        // RVA: 0x0AC4228C  token: 0x60001D1
        public System.Int32 __Gen_Wrap_61(Beyond.Scripts.Quality.Components.FrameRateQualityMobile.FrameRateQualityMobileEnum P0) { }
        // RVA: 0x04D37020  token: 0x60001D2
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200005B  // size: 0x40
    public class ILFixInterfaceBridge : IFix.Core.AnonymousStorey
    {
        // Methods
        // RVA: 0x0AC4690C  token: 0x60001D3
        public System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x041E1670  token: 0x60001D4
        public System.Void RefAsyncBuilderStartMethod() { }

    }

    // TypeToken: 0x200005C  // size: 0x18
    public class WrappersManagerImpl : IFix.Core.WrappersManager
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x60001D5
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x0AC46B88  token: 0x60001D6
        public static IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id) { }
        // RVA: 0x036923C0  token: 0x60001D7
        public static System.Boolean IsPatched(System.Int32 id) { }
        // RVA: 0x0AC46A44  token: 0x60001D8
        public virtual System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon) { }
        // RVA: 0x0AC46B10  token: 0x60001D9
        public virtual System.Object CreateWrapper(System.Int32 id) { }
        // RVA: 0x0AC46BEC  token: 0x60001DA
        public virtual System.Object InitWrapperArray(System.Int32 len) { }
        // RVA: 0x0AC469A0  token: 0x60001DB
        public virtual IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine) { }

    }

    // TypeToken: 0x200005D  // size: 0x14
    public sealed struct IDMAP0
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static IFix.IDMAP0 Beyond-PoolCore-ListPoolInitializer_RuntimeQuality-Initialize0;  // const
        public static IFix.IDMAP0 Beyond-PoolCore-ListPoolInitializer_RuntimeQuality-InitializeAtRuntime0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceMatchRules-Combine0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-SupplementFromRemote0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-RankingRule-ToString0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceMatchResult-ToString0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-ToString0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceMatchResult-IsNoMatch0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-IsNoMatchDevice0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-IsZeroDevices0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-get_cpuName0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-IsHUAWEIDevice0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-IsQualcommDevice0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-LoadingSettingRules0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-CloudGameGetMatchedRule0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-GetNvidiaStandardGPUName0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-RuleSuffixMatch0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-GetAmdStandardGPUName0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-GetIntelStandardGPUName0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-StandaloneMatchGPURule0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-_ReportMatchRuleFailed0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-StandaloneGetMatchedRule0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-GetDeviceScore0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-RuleFullNameMatch0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-MatchDeviceModeRule0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-GetAdrenoStandardGPUName0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-GetMaliStandardGPUName0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-MatchGPURule0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-AndroidGetMatchedRule0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-IOSGetMatchedRule0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-DefaultGetMatchedRule0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-EditorSimulateGetMatchedRule0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-StandaloneOSXGetMatchedRule0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-StandalonePS5GetMatchedRule0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceInfo-GetPowerVRStandardGPUName0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-RankingRule-Equals0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-MemoryRule-ToString0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-MemoryRule-Equals0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceQualityMapItem-ToString0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceQualityMapItem-getColor0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceQualityMapItem-getVeryLowColor0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceQualityMapItem-getLowColor0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceQualityMapItem-getMiddleColor0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceQualityMapItem-getHighColor0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-DeviceQualityMapItem-getVeryHighColor0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-get_Config0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-get_currentTier0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-get_defaultTier0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-get_device0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-QualityTierComponent-ApplyOrder0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-LoadQualityTier0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-_SetQualityTierComponentLevel0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-get_isInitializing0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-QualityTierComponent-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-QualityTierComponent-InitApply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-QualityTierComponent-LowMemory0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-InvokeChangeCallBack0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-QualityTierComponent-SafeApply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-QualityTierComponent-SafeLowMemory0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-_GetOverrideQualityTierComponentLevel0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-ApplyCallBack0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-setQualityTier0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-ApplyQualityScore0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-QualityTierComponent-DynamicQualityChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-QualityTierComponent-CallDynamicQualityChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-DynamicQualityChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-QualityTierComponent-DynamicThermalChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-QualityTierComponent-CallDynamicThermalChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-DynamicThermalChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-_DynamicInitQualitySetting0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-Init0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-InitOnlyDevice0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-_GetQualityComponentByName0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-ForceSetQualityTierComponentLevel0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-FinishInit0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-ApplyShaderTier0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-InitShaderLod0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-_DynamicReleaseQualitySetting0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-SetQualityTierByVideoQuality0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-GetDefaultVideoQuality0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-NoMatchDeviceOrZeroScore0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-IsQualcommDevice0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-IsHUAWEIDevice0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-IsIpadDevice0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-InitResetQualityTierComponentFunction0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-ResetQualityTierComponentValue0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-RevertSetQualityTierComponentLevel0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-NeedRestart0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-GetQualityTierComponentTierType0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-QualityTierComponent-Version0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-GetQualityTierComponentTierVersion0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-_IsToggleQualityTierComponent0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-ToggleQualityComponent-ChangeToggle0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-ToggleQualityComponent-GetValue0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-_ToggleQualityTierComponentLevel0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-IsEnumQualityTierComponent0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-GetEnumQualityTierComponentCount0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-SetEnumQualityTierComponentCount0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-IsSliderQualityTierComponent0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-SliderQualityComponent-SetValue0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-SetSliderQualityTierComponentCount0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-SliderQualityComponent-GetRange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-SliderQualityComponent-GetValue0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-GetSliderQualityTierComponent0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-InvokeQualityTierComponent0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-GetQualityTierComponentTierCount0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-ToggleQualityComponent-GetDefaultValue0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-SliderQualityComponent-GetDefaultValue0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-GetQualityComponentDefaultTier0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-GetQualityComponentTier0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-_ReportQuality0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-SetQualityComponentTier0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-QualityTierComponent-TierLimit0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-_GetCurrentComponentTierIndex0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-IsQualitySubSettingTierValid0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-ToString0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityManager-DebugQualityTierComponentInfo0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityUtility-Decompress0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-QualityUtility-get_JsonSerializerSettings0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-AntiAliasingQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-QualityTierComponent-RemoveQualitySettingSaveValue0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-QualityTierComponent-RemoveRegisterKey0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-ToggleQualityComponent-Toggle0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-ToggleQualityComponent-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-CharacterRenderFeatureQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-ChromaticAberrationQuality-Toggle0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-DLSSFrameGenQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-DLSSFrameGenQuality-DlSSChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-DLSSFrameGenQuality-OnDeserialized0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-DLSSFrameGenQuality-ShouldShowAuto0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-DLSSFrameGenQuality-ShouldShowX10;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-DLSSFrameGenQuality-ShouldShowX20;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-DLSSFrameGenQuality-ShouldShowX30;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-DLSSFrameGenQuality-ShouldShow0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-DLSSUpscalerModel-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-DLSSUpscalerModel-ShouldShow0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-DLSSUpscalerQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-DLSSUpscalerQuality-ShouldShow0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-DLSSUpscalerQuality-DlSSChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-DLSSUpscalerQuality-OnDeserialized0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-EnvironmentRenderingFeatureQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameGenQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameGenQuality-ShouldShowDLSS0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameGenQuality-ShouldShowFSR30;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameGenQuality-ShouldShow0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameGenQualityMobile-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameGenQualityMobile-ShouldShow0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameRateQuality-GetFrameRateFromEnum0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameRateQuality-get_frameRate0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameRateQuality-Version0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameRateQuality-ShouldShow0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HgFrameRateControl-__ForceSetFrameRate0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameRateQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameRateQuality-DlSSChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameRateQuality-OnDeserialized0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameRateQuality-_FPS30Enabled0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameRateQuality-_FPS60Enabled0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameRateQuality-_FPSBNoLimitEnabled0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameRateQuality-_IsGDLSSGDisable0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HgFrameRateControl-GetDefaultFrameRate0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HgFrameRateControl-Init0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HgFrameRateControl-SetFrameRateOverrider0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HgFrameRateControl-GetFrameRate0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HgFrameRateControl-RecommendedDefaultFrameRate0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HgFrameRateControl-GetUserSettingFrameRate0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HgFrameRateControl-ResetFrameRate0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameRateQualityMobile-GetFrameRateFromEnum0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameRateQualityMobile-get_frameRate0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameRateQualityMobile-Version0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameRateQualityMobile-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FrameRateQualityMobile-OnDeserialized0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FSR3UpscalerQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FSR3UpscalerQuality-ShouldShow0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FSR3UpscalerQuality-FSR3Change0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-FSR3UpscalerQuality-OnDeserialized0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-GrassSparsityQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HGAmbientOcclusionQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HGAnisoLevelQuality-InitApply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HGAnisoLevelQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HGContactShadowQuality-Toggle0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HGIrradianceVolumeQuality-LowMemory0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HGLODStreamingComponent-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HGLODStreamingComponent-LowMemory0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HGReflectionProbeBinningQuality-LowMemory0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HGRPTierQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HGScreenSpaceReflectionQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HGShadowQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HGTextureQuality-get_textureQualityTier0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HGTextureQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HGVolumetricCloudQuality-GetShaderLod0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HGVolumetricCloudQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HGVolumetricCloudQuality-ShouldShowNoExLow0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HGVolumetricCloudQuality-ShouldShowExLow0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-HGVolumetricFogQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-LODGroupQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-PhysicsClothQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-PrefabInstantiateQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-PreloadLoadSettingQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-RayTracingQuality-get_supportsRayTracing0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-RayTracingQuality-InitApply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-RayTracingQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-RayTracingQuality-ShouldShow0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-RayTracingQuality-ApplyOrder0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-ReflexQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-ReflexQuality-ShouldShow0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-ReflexQuality-DlSSChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-ReflexQuality-OnDeserialized0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-RenderingScaleQualityMobile-get_renderingScaleFactor0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-RenderingScaleQualityMobile-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-RenderingScaleQualityPC-get_renderingScale0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-RenderingScaleQualityPC-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-RenderingScaleQualityPC-ShouldShow0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-RenderingScaleQualityPC-DlSSChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-RenderingScaleQualityPC-OnDeserialized0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-ResLoadSettingQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-ResLoadSettingQuality-LowMemory0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-SceneDetailQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-ShaderLodQuality-InitApply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-ShaderLodQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-SharpnessQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-SharpnessQuality-ShouldShow0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-SliderTestComponent-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-TickExclusiveQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-TickExclusiveQuality-_OnQualityOrThermalChanged0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-TickExclusiveQuality-DynamicQualityChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-TickExclusiveQuality-DynamicThermalChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-UpscalerQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-UpscalerQuality-ShouldShowDLSS0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-UpscalerQuality-ShouldShow0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-UpscalerQuality-ShouldShowFSR30;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-UpscalerQuality-Version0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-VSyncQuality-Apply0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-VSyncQuality-Remove0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-VSyncQuality-Init0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-VSyncQualityV2-Toggle0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-VSyncQualityV2-Init0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-VSyncQualityV2-Version0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-VSyncQualityV2-FrameGenChange0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-VSyncQualityV2-OnDeserialized0;  // const
        public static IFix.IDMAP0 Beyond-Scripts-Quality-Components-VSyncQualityV2-ShouldShow0;  // const

    }

}

