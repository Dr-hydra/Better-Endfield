// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.InputModule.dll
// Classes:  7
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

namespace UnityEngineInternal.Input
{

    // TypeToken: 0x2000002  // size: 0x80
    public sealed class NativeUpdateCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x03EB3930  token: 0x6000001
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x6000002
        public virtual System.Void Invoke(UnityEngineInternal.Input.NativeInputUpdateType updateType, UnityEngineInternal.Input.NativeInputEventBuffer* buffer) { }

    }

    // TypeToken: 0x2000003  // size: 0x14
    public sealed struct NativeInputEventType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngineInternal.Input.NativeInputEventType DeviceRemoved;  // const
        public static UnityEngineInternal.Input.NativeInputEventType DeviceConfigChanged;  // const
        public static UnityEngineInternal.Input.NativeInputEventType Text;  // const
        public static UnityEngineInternal.Input.NativeInputEventType State;  // const
        public static UnityEngineInternal.Input.NativeInputEventType Delta;  // const

    }

    // TypeToken: 0x2000004  // size: 0x24
    public sealed struct NativeInputEventBuffer
    {
        // Fields
        public System.Void* eventBuffer;  // 0x10
        public System.Int32 eventCount;  // 0x18
        public System.Int32 sizeInBytes;  // 0x1c
        public System.Int32 capacityInBytes;  // 0x20

    }

    // TypeToken: 0x2000005  // size: 0x24
    public sealed struct NativeInputEvent
    {
        // Fields
        public static System.Int32 structSize;  // const
        public UnityEngineInternal.Input.NativeInputEventType type;  // 0x10
        public System.UInt16 sizeInBytes;  // 0x14
        public System.UInt16 deviceId;  // 0x16
        public System.Double time;  // 0x18
        public System.Int32 eventId;  // 0x20

    }

    // TypeToken: 0x2000006  // size: 0x14
    public sealed struct NativeInputUpdateType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngineInternal.Input.NativeInputUpdateType Dynamic;  // const
        public static UnityEngineInternal.Input.NativeInputUpdateType Fixed;  // const
        public static UnityEngineInternal.Input.NativeInputUpdateType BeforeRender;  // const
        public static UnityEngineInternal.Input.NativeInputUpdateType Editor;  // const
        public static UnityEngineInternal.Input.NativeInputUpdateType IgnoreFocus;  // const

    }

    // TypeToken: 0x2000007  // size: 0x10
    public class NativeInputSystem
    {
        // Fields
        public static UnityEngineInternal.Input.NativeUpdateCallback onUpdate;  // static @ 0x0
        public static System.Action<UnityEngineInternal.Input.NativeInputUpdateType> onBeforeUpdate;  // static @ 0x8
        public static System.Func<UnityEngineInternal.Input.NativeInputUpdateType,System.Boolean> onShouldRunUpdate;  // static @ 0x10
        private static System.Action<System.Int32,System.String> s_OnDeviceDiscoveredCallback;  // static @ 0x18

        // Properties
        System.Action<System.Int32,System.String> onDeviceDiscovered { get; /* RVA: 0x0B418224 */ set; /* RVA: 0x03EB39A0 */ }
        System.Boolean hasDeviceDiscoveredCallback { set; /* RVA: 0x03EB3A40 */ }
        System.Double currentTime { get; /* RVA: 0x030CFBD0 */ }
        System.Double currentTimeOffsetToRealtimeSinceStartup { get; /* RVA: 0x030CF7B0 */ }

        // Methods
        // RVA: 0x03EB3A30  token: 0x6000005
        private static System.Void .cctor() { }
        // RVA: 0x03DC8450  token: 0x6000006
        private static System.Void NotifyBeforeUpdate(UnityEngineInternal.Input.NativeInputUpdateType updateType) { }
        // RVA: 0x030CE8C0  token: 0x6000007
        private static System.Void NotifyUpdate(UnityEngineInternal.Input.NativeInputUpdateType updateType, System.IntPtr eventBuffer) { }
        // RVA: 0x0484DC50  token: 0x6000008
        private static System.Void NotifyDeviceDiscovered(System.Int32 deviceId, System.String deviceDescriptor) { }
        // RVA: 0x03BBC9E0  token: 0x6000009
        private static System.Void ShouldRunUpdate(UnityEngineInternal.Input.NativeInputUpdateType updateType, System.Boolean& retval) { }
        // RVA: 0x0B4181C4  token: 0x600000D
        public static System.Int32 AllocateDeviceId() { }
        // RVA: 0x0B4181F0  token: 0x600000E
        public static System.Void QueueInputEvent(System.IntPtr inputEvent) { }
        // RVA: 0x043207A0  token: 0x600000F
        public static System.Int64 IOCTL(System.Int32 deviceId, System.Int32 code, System.IntPtr data, System.Int32 sizeInBytes) { }
        // RVA: 0x03EB38E0  token: 0x6000010
        public static System.Void SetPollingFrequency(System.Single hertz) { }
        // RVA: 0x04B1ED30  token: 0x6000011
        public static System.Void Update(UnityEngineInternal.Input.NativeInputUpdateType updateType) { }

    }

}

