// ========================================================
// Dumped by @desirepro
// Assembly: HG.RenderPipelines.ScriptBridge.dll
// Classes:  5
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

namespace HG.Rendering.ScriptBridge
{

    // TypeToken: 0x2000002  // size: 0x10
    public static class HGRenderBridgeStatics
    {
        // Properties
        System.Int32 UIPrefabMaxNodeCount { get; /* RVA: 0x09D27130 */ }

        // Methods
        // RVA: 0x09D269E4  token: 0x6000001
        public static UnityEngine.Camera AttachHGCamera(UnityEngine.GameObject camObject) { }
        // RVA: 0x09D26B58  token: 0x6000002
        public static UnityEngine.Material GetHGDecalProjectorMaterialInChildren(UnityEngine.GameObject go) { }
        // RVA: 0x09D26AB0  token: 0x6000003
        public static UnityEngine.HGDecalProjector GetHGDecalProjectorInChildren(UnityEngine.GameObject go) { }
        // RVA: 0x09D26F84  token: 0x6000004
        public static System.Void SetHGDecalProjectorProgress(UnityEngine.Object component, System.Single progress) { }
        // RVA: 0x09D26EDC  token: 0x6000005
        public static System.Void SetHGDecalProjectorHollow(UnityEngine.Object component, System.Single hollow) { }
        // RVA: 0x04422250  token: 0x6000006
        public static System.Void HGCharacterHelperFindRenderers(UnityEngine.GameObject go) { }
        // RVA: 0x09D26DFC  token: 0x6000007
        public static System.Void SetHGDecalProjectorAngle(UnityEngine.GameObject go, System.Single angle) { }
        // RVA: 0x031CBBC0  token: 0x6000008
        public static System.Void UpdateCurrentPlayerCenter(UnityEngine.Transform centerTransform) { }
        // RVA: 0x031CC5E0  token: 0x6000009
        public static System.Void UpdateCurrentEnvCenter(UnityEngine.Transform centerTransform) { }
        // RVA: 0x09D27070  token: 0x600000A
        public static System.Void UpdateAnchorBrightParams(UnityEngine.Vector2 anchorPosition, System.Single anchorRadius, System.Single anchorBrightIntensity, System.Boolean anchorBrightFlag) { }
        // RVA: 0x044D5AE0  token: 0x600000B
        public static System.Void SetVFXPPPriorityFilterCinematic() { }
        // RVA: 0x0394F090  token: 0x600000C
        public static System.Void SetVFXPPPriorityFilterNormal() { }
        // RVA: 0x0394EFF0  token: 0x600000D
        public static System.Void SetVFXPPPriorityFilterUltiSkillCam() { }
        // RVA: 0x09D27014  token: 0x600000E
        public static System.Void SetVFXPPActive(System.Boolean isActive) { }
        // RVA: 0x0494F880  token: 0x600000F
        public static System.Void SetSceneDarkEnabled(System.Boolean enabled) { }
        // RVA: 0x09D26C90  token: 0x6000010
        public static System.String GetWorldUIKeyword() { }
        // RVA: 0x0323BB30  token: 0x6000011
        public static System.Void SetCharacterPositionsHeights(System.Collections.Generic.List<UnityEngine.Vector3> characterPositions, System.Collections.Generic.List<System.Single> characterRadiuses, System.Collections.Generic.List<System.Single> characterHeights) { }
        // RVA: 0x03B0C640  token: 0x6000012
        public static System.Void SetGameplayTime(System.Double gameplayTime) { }
        // RVA: 0x03B0C690  token: 0x6000013
        public static System.Void SetLastGameplayTime(System.Double lastGameplayTime) { }
        // RVA: 0x0340BC50  token: 0x6000014
        public static System.Void RequestDisableFrameGenTemporarily(UnityEngine.Camera camera, System.Boolean disable) { }
        // RVA: 0x09D26D08  token: 0x6000015
        public static System.Void PauseMobileFrameGenTemporarily(System.Boolean pause) { }

    }

    // TypeToken: 0x2000003  // size: 0x10
    public class TAAUControlBridge
    {
        // Properties
        System.Boolean taauFastConverge { set; /* RVA: 0x09D28854 */ }

        // Methods
        // RVA: 0x09D28394  token: 0x6000017
        public static System.Void SetRenderingScale(UnityEngine.Camera camera, System.Single scale) { }
        // RVA: 0x09D286D8  token: 0x6000018
        public static System.Void ToggleTAAU(UnityEngine.Camera camera) { }
        // RVA: 0x09D284B8  token: 0x6000019
        public static System.Void ToggleMetalFXSpatial(UnityEngine.Camera camera) { }
        // RVA: 0x09D285C8  token: 0x600001A
        public static System.Void ToggleMetalFXTemporal(UnityEngine.Camera camera) { }
        // RVA: 0x09D28650  token: 0x600001B
        public static System.Void ToggleTAAUWithMetalFXSpatial(UnityEngine.Camera camera) { }
        // RVA: 0x09D28540  token: 0x600001C
        public static System.Void ToggleMetalFXTemporalWithMetalFXSpatial(UnityEngine.Camera camera) { }
        // RVA: 0x09D278E8  token: 0x600001E
        public static System.Void ChangeHistoryWeight(System.Single val) { }
        // RVA: 0x09D27B04  token: 0x600001F
        public static System.Single GetHistoryWeight() { }
        // RVA: 0x09D27834  token: 0x6000020
        public static System.Void ChangeHistoryWeightInMotion(System.Single val) { }
        // RVA: 0x09D27780  token: 0x6000021
        public static System.Void ChangeDepthDiff(System.Single val) { }
        // RVA: 0x09D27D04  token: 0x6000022
        public static System.Void MinMVConsideredDynamic(System.Single val) { }
        // RVA: 0x09D27C50  token: 0x6000023
        public static System.Void MaxMVConsideredDynamic(System.Single val) { }
        // RVA: 0x09D2799C  token: 0x6000024
        public static System.Void CharacterMotionSensitivity(System.Single val) { }
        // RVA: 0x09D27A50  token: 0x6000025
        public static System.Void FastConvergeHistoryWeight(System.Single val) { }
        // RVA: 0x09D27B9C  token: 0x6000026
        public static System.Void InputSampleLumaWeight(System.Single val) { }
        // RVA: 0x09D28760  token: 0x6000027
        public static System.Void ToggleTargetFrameRate(System.Int32 val) { }
        // RVA: 0x030418C0  token: 0x6000028
        public static System.Void NotifyTimeScale(System.Single timeScale) { }
        // RVA: 0x09D27FEC  token: 0x6000029
        public static System.Void SetDLSSQuality(UnityEngine.Camera camera, System.Int32 quality) { }
        // RVA: 0x09D28430  token: 0x600002A
        public static System.Void ToggleDLSS(UnityEngine.Camera camera) { }
        // RVA: 0x09D27E60  token: 0x600002B
        public static System.Void SetDLSSFG(UnityEngine.Camera camera, System.Int32 mode) { }
        // RVA: 0x09D27DB8  token: 0x600002C
        public static System.Void SetDLSSFGModeGenFrames(System.Int32 numFramesToGenerate) { }
        // RVA: 0x09D280C4  token: 0x600002D
        public static System.Void SetDLSSReflex(System.Int32 mode) { }
        // RVA: 0x09D27F44  token: 0x600002E
        public static System.Void SetDLSSPCLEnable(System.Boolean enable) { }
        // RVA: 0x09D2816C  token: 0x600002F
        public static System.Void SetDLSSSharpenStrength(System.Single sharpenStrength) { }
        // RVA: 0x09D282FC  token: 0x6000030
        public static System.Void SetForceJitterPhaseIdx(UnityEngine.Camera camera, System.Int32 idx) { }
        // RVA: 0x09D287AC  token: 0x6000031
        public static System.Void UseDLSSExposure(System.Boolean use) { }
        // RVA: 0x09D28224  token: 0x6000032
        public static System.Void SetFSR3Quality(UnityEngine.Camera camera, System.Int32 quality) { }
        // RVA: 0x041E1670  token: 0x6000033
        public System.Void .ctor() { }

    }

}

namespace IFix
{

    // TypeToken: 0x2000004  // size: 0x28
    public class ILFixDynamicMethodWrapper
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10
        private System.Int32 methodId;  // 0x18
        private System.Object anonObj;  // 0x20
        public static IFix.ILFixDynamicMethodWrapper[] wrapperArray;  // static @ 0x0

        // Methods
        // RVA: 0x06375088  token: 0x6000034
        public System.Void __Gen_Wrap_0(System.Boolean P0) { }
        // RVA: 0x06A5FB54  token: 0x6000035
        public System.Void __Gen_Wrap_2(System.Object P0, System.Int32 P1) { }
        // RVA: 0x09D272F0  token: 0x6000036
        public UnityEngine.Camera __Gen_Wrap_3(System.Object P0) { }
        // RVA: 0x09D273CC  token: 0x6000037
        public UnityEngine.Material __Gen_Wrap_4(System.Object P0) { }
        // RVA: 0x09D274A8  token: 0x6000038
        public UnityEngine.HGDecalProjector __Gen_Wrap_5(System.Object P0) { }
        // RVA: 0x09D27584  token: 0x6000039
        public System.Void __Gen_Wrap_6(System.Object P0, System.Single P1) { }
        // RVA: 0x063633B4  token: 0x600003A
        public System.Void __Gen_Wrap_7(System.Object P0) { }
        // RVA: 0x09D27650  token: 0x600003B
        public System.Void __Gen_Wrap_8(UnityEngine.Vector2 P0, System.Single P1, System.Single P2, System.Boolean P3) { }
        // RVA: 0x063625A4  token: 0x600003C
        public System.Void __Gen_Wrap_9() { }
        // RVA: 0x09D27170  token: 0x600003D
        public System.String __Gen_Wrap_10() { }
        // RVA: 0x06364290  token: 0x600003E
        public System.Void __Gen_Wrap_11(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x09CD5364  token: 0x600003F
        public System.Void __Gen_Wrap_12(System.Double P0) { }
        // RVA: 0x09D27228  token: 0x6000040
        public System.Void __Gen_Wrap_13(System.Object P0, System.Boolean P1) { }
        // RVA: 0x06377214  token: 0x6000041
        public System.Int32 __Gen_Wrap_14() { }
        // RVA: 0x06301ED4  token: 0x6000042
        public System.Void __Gen_Wrap_15(System.Int32 P0) { }
        // RVA: 0x069ABD7C  token: 0x6000043
        public System.Void __Gen_Wrap_16(System.Single P0) { }
        // RVA: 0x066D3790  token: 0x6000044
        public System.Single __Gen_Wrap_17() { }
        // RVA: 0x04D36FB0  token: 0x6000045
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000005  // size: 0x10
    public class WrappersManagerImpl
    {
        // Methods
        // RVA: 0x09D288EC  token: 0x6000046
        public static IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id) { }
        // RVA: 0x03041920  token: 0x6000047
        public static System.Boolean IsPatched(System.Int32 id) { }

    }

}

