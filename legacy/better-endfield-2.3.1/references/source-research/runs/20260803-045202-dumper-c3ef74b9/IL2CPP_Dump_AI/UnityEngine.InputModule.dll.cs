// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.InputModule.dll
// Classes:  7
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

CLASS: UnityEngineInternal.Input.NativeUpdateCallback
TYPE:  sealed class
TOKEN: 0x2000002
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x03EB3930  token=0x6000001  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x6000002  System.Void Invoke(UnityEngineInternal.Input.NativeInputUpdateType updateType, UnityEngineInternal.Input.NativeInputEventBuffer* buffer)
END_CLASS

CLASS: UnityEngineInternal.Input.NativeInputEventType
TYPE:  sealed struct
TOKEN: 0x2000003
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngineInternal.Input.NativeInputEventTypeDeviceRemoved  // const
  public    static  UnityEngineInternal.Input.NativeInputEventTypeDeviceConfigChanged  // const
  public    static  UnityEngineInternal.Input.NativeInputEventTypeText  // const
  public    static  UnityEngineInternal.Input.NativeInputEventTypeState  // const
  public    static  UnityEngineInternal.Input.NativeInputEventTypeDelta  // const
METHODS:
END_CLASS

CLASS: UnityEngineInternal.Input.NativeInputEventBuffer
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x24
FIELDS:
  public            System.Void*                    eventBuffer  // 0x10
  public            System.Int32                    eventCount  // 0x18
  public            System.Int32                    sizeInBytes  // 0x1c
  public            System.Int32                    capacityInBytes  // 0x20
METHODS:
END_CLASS

CLASS: UnityEngineInternal.Input.NativeInputEvent
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x24
FIELDS:
  public    static  System.Int32                    structSize  // const
  public            UnityEngineInternal.Input.NativeInputEventTypetype  // 0x10
  public            System.UInt16                   sizeInBytes  // 0x14
  public            System.UInt16                   deviceId  // 0x16
  public            System.Double                   time  // 0x18
  public            System.Int32                    eventId  // 0x20
METHODS:
END_CLASS

CLASS: UnityEngineInternal.Input.NativeInputUpdateType
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngineInternal.Input.NativeInputUpdateTypeDynamic  // const
  public    static  UnityEngineInternal.Input.NativeInputUpdateTypeFixed  // const
  public    static  UnityEngineInternal.Input.NativeInputUpdateTypeBeforeRender  // const
  public    static  UnityEngineInternal.Input.NativeInputUpdateTypeEditor  // const
  public    static  UnityEngineInternal.Input.NativeInputUpdateTypeIgnoreFocus  // const
METHODS:
END_CLASS

CLASS: UnityEngineInternal.Input.NativeInputSystem
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x10
FIELDS:
  public    static  UnityEngineInternal.Input.NativeUpdateCallbackonUpdate  // static @ 0x0
  public    static  System.Action<UnityEngineInternal.Input.NativeInputUpdateType>onBeforeUpdate  // static @ 0x8
  public    static  System.Func<UnityEngineInternal.Input.NativeInputUpdateType,System.Boolean>onShouldRunUpdate  // static @ 0x10
  private   static  System.Action<System.Int32,System.String>s_OnDeviceDiscoveredCallback  // static @ 0x18
PROPERTIES:
  onDeviceDiscovered  get=0x0B418224  set=0x03EB39A0
  hasDeviceDiscoveredCallback  set=0x03EB3A40
  currentTime  get=0x030CFBD0
  currentTimeOffsetToRealtimeSinceStartup  get=0x030CF7B0
METHODS:
  RVA=0x03EB3A30  token=0x6000005  System.Void .cctor()
  RVA=0x03DC8450  token=0x6000006  System.Void NotifyBeforeUpdate(UnityEngineInternal.Input.NativeInputUpdateType updateType)
  RVA=0x030CE8C0  token=0x6000007  System.Void NotifyUpdate(UnityEngineInternal.Input.NativeInputUpdateType updateType, System.IntPtr eventBuffer)
  RVA=0x0484DC50  token=0x6000008  System.Void NotifyDeviceDiscovered(System.Int32 deviceId, System.String deviceDescriptor)
  RVA=0x03BBC9E0  token=0x6000009  System.Void ShouldRunUpdate(UnityEngineInternal.Input.NativeInputUpdateType updateType, System.Boolean& retval)
  RVA=0x0B4181C4  token=0x600000D  System.Int32 AllocateDeviceId()
  RVA=0x0B4181F0  token=0x600000E  System.Void QueueInputEvent(System.IntPtr inputEvent)
  RVA=0x043207A0  token=0x600000F  System.Int64 IOCTL(System.Int32 deviceId, System.Int32 code, System.IntPtr data, System.Int32 sizeInBytes)
  RVA=0x03EB38E0  token=0x6000010  System.Void SetPollingFrequency(System.Single hertz)
  RVA=0x04B1ED30  token=0x6000011  System.Void Update(UnityEngineInternal.Input.NativeInputUpdateType updateType)
END_CLASS

