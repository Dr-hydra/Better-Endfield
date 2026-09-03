// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.VRModule.dll
// Classes:  4
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000003  // size: 0x14
    public sealed struct StereoRenderingMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.XR.XRSettings.StereoRenderingMode MultiPass;  // const
        public static UnityEngine.XR.XRSettings.StereoRenderingMode SinglePass;  // const
        public static UnityEngine.XR.XRSettings.StereoRenderingMode SinglePassInstanced;  // const
        public static UnityEngine.XR.XRSettings.StereoRenderingMode SinglePassMultiview;  // const

    }

namespace UnityEngine.XR
{

    // TypeToken: 0x2000002  // size: 0x10
    public static class XRSettings
    {
        // Properties
        System.Boolean enabled { get; /* RVA: 0x0B5904D0 */ }
        System.Boolean isDeviceActive { get; /* RVA: 0x0B5905E0 */ }
        System.Single eyeTextureResolutionScale { get; /* RVA: 0x0B590588 */ set; /* RVA: 0x0B5906C8 */ }
        System.Int32 eyeTextureWidth { get; /* RVA: 0x0B5905B4 */ }
        System.Int32 eyeTextureHeight { get; /* RVA: 0x0B59055C */ }
        UnityEngine.RenderTextureDescriptor eyeTextureDesc { get; /* RVA: 0x0B590530 */ }
        System.Single renderViewportScale { get; /* RVA: 0x0B590664 */ }
        System.Single renderViewportScaleInternal { get; /* RVA: 0x0B590638 */ }
        System.String loadedDeviceName { get; /* RVA: 0x0B59060C */ }
        System.String[] supportedDevices { get; /* RVA: 0x0B59069C */ }
        UnityEngine.XR.XRSettings.StereoRenderingMode stereoRenderingMode { get; /* RVA: 0x0B590670 */ }

        // Methods
        // RVA: 0x0B5904FC  token: 0x600000D
        private static System.Void get_eyeTextureDesc_Injected(UnityEngine.RenderTextureDescriptor& ret) { }

    }

    // TypeToken: 0x2000004  // size: 0x10
    public static class XRDevice
    {
        // Fields
        private static System.Action<System.String> deviceLoaded;  // static @ 0x0

        // Methods
        // RVA: 0x0B59043C  token: 0x600000E
        public static System.Void DisableAutoXRCameraTracking(UnityEngine.Camera camera, System.Boolean disabled) { }
        // RVA: 0x0B590480  token: 0x600000F
        private static System.Void InvokeDeviceLoaded(System.String loadedDeviceName) { }

    }

}

