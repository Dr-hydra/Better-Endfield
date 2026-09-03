// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.VRModule.dll
// Classes:  4
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

CLASS: StereoRenderingMode
TYPE:  sealed struct
TOKEN: 0x2000003
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.XR.XRSettings.StereoRenderingModeMultiPass  // const
  public    static  UnityEngine.XR.XRSettings.StereoRenderingModeSinglePass  // const
  public    static  UnityEngine.XR.XRSettings.StereoRenderingModeSinglePassInstanced  // const
  public    static  UnityEngine.XR.XRSettings.StereoRenderingModeSinglePassMultiview  // const
METHODS:
END_CLASS

CLASS: UnityEngine.XR.XRSettings
TYPE:  static class
TOKEN: 0x2000002
SIZE:  0x10
FIELDS:
PROPERTIES:
  enabled  get=0x0B5904D0
  isDeviceActive  get=0x0B5905E0
  eyeTextureResolutionScale  get=0x0B590588  set=0x0B5906C8
  eyeTextureWidth  get=0x0B5905B4
  eyeTextureHeight  get=0x0B59055C
  eyeTextureDesc  get=0x0B590530
  renderViewportScale  get=0x0B590664
  renderViewportScaleInternal  get=0x0B590638
  loadedDeviceName  get=0x0B59060C
  supportedDevices  get=0x0B59069C
  stereoRenderingMode  get=0x0B590670
METHODS:
  RVA=0x0B5904FC  token=0x600000D  System.Void get_eyeTextureDesc_Injected(UnityEngine.RenderTextureDescriptor& ret)
END_CLASS

CLASS: UnityEngine.XR.XRDevice
TYPE:  static class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
  private   static  System.Action<System.String>    deviceLoaded  // static @ 0x0
METHODS:
  RVA=0x0B59043C  token=0x600000E  System.Void DisableAutoXRCameraTracking(UnityEngine.Camera camera, System.Boolean disabled)
  RVA=0x0B590480  token=0x600000F  System.Void InvokeDeviceLoaded(System.String loadedDeviceName)
END_CLASS

