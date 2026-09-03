// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.VFXModule.dll
// Classes:  11
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

namespace UnityEngine.VFX
{

    // TypeToken: 0x2000002  // size: 0x28
    public sealed class VFXEventAttribute : System.IDisposable
    {
        // Fields
        private System.IntPtr m_Ptr;  // 0x10
        private System.Boolean m_Owner;  // 0x18
        private UnityEngine.VFX.VisualEffectAsset m_VfxAsset;  // 0x20

        // Methods
        // RVA: 0x0B58DAF8  token: 0x6000001
        private System.Void .ctor(System.IntPtr ptr, System.Boolean owner, UnityEngine.VFX.VisualEffectAsset vfxAsset) { }
        // RVA: 0x0B58D808  token: 0x6000002
        private static UnityEngine.VFX.VFXEventAttribute CreateEventAttributeWrapper() { }
        // RVA: 0x0B58DA8C  token: 0x6000003
        private System.Void SetWrapValue(System.IntPtr ptrToEventAttribute) { }
        // RVA: 0x0B58D920  token: 0x6000004
        private static System.IntPtr Internal_Create() { }
        // RVA: 0x0B58D9C4  token: 0x6000005
        private static UnityEngine.VFX.VFXEventAttribute Internal_InstanciateVFXEventAttribute(UnityEngine.VFX.VisualEffectAsset vfxAsset) { }
        // RVA: 0x0B58D980  token: 0x6000006
        private System.Void Internal_InitFromAsset(UnityEngine.VFX.VisualEffectAsset vfxAsset) { }
        // RVA: 0x0B58DA50  token: 0x6000007
        private System.Void Release() { }
        // RVA: 0x0B58D8B0  token: 0x6000008
        protected virtual System.Void Finalize() { }
        // RVA: 0x0B58D864  token: 0x6000009
        public virtual System.Void Dispose() { }
        // RVA: 0x0B58D94C  token: 0x600000A
        private static System.Void Internal_Destroy(System.IntPtr ptr) { }

    }

    // TypeToken: 0x2000003  // size: 0x18
    public class VFXExpressionValues
    {
        // Fields
        private System.IntPtr m_Ptr;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600000B
        private System.Void .ctor() { }
        // RVA: 0x0B58DB10  token: 0x600000C
        private static UnityEngine.VFX.VFXExpressionValues CreateExpressionValuesWrapper(System.IntPtr ptr) { }

    }

    // TypeToken: 0x2000004  // size: 0x1C
    public sealed struct VFXCameraXRSettings
    {
        // Fields
        public System.UInt32 viewTotal;  // 0x10
        public System.UInt32 viewCount;  // 0x14
        public System.UInt32 viewOffset;  // 0x18

    }

    // TypeToken: 0x2000005  // size: 0x10
    public static class VFXManager
    {
        // Fields
        private static readonly UnityEngine.VFX.VFXCameraXRSettings kDefaultCameraXRSettings;  // static @ 0x0

        // Methods
        // RVA: 0x0B58DB58  token: 0x600000D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x18
    public abstract class VFXSpawnerCallbacks : UnityEngine.ScriptableObject
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600000E
        public virtual System.Void OnPlay(UnityEngine.VFX.VFXSpawnerState state, UnityEngine.VFX.VFXExpressionValues vfxValues, UnityEngine.VFX.VisualEffect vfxComponent) { }
        // RVA: -1  // abstract  token: 0x600000F
        public virtual System.Void OnUpdate(UnityEngine.VFX.VFXSpawnerState state, UnityEngine.VFX.VFXExpressionValues vfxValues, UnityEngine.VFX.VisualEffect vfxComponent) { }
        // RVA: -1  // abstract  token: 0x6000010
        public virtual System.Void OnStop(UnityEngine.VFX.VFXSpawnerState state, UnityEngine.VFX.VFXExpressionValues vfxValues, UnityEngine.VFX.VisualEffect vfxComponent) { }
        // RVA: 0x03573370  token: 0x6000011
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x28
    public sealed class VFXSpawnerState : System.IDisposable
    {
        // Fields
        private System.IntPtr m_Ptr;  // 0x10
        private System.Boolean m_Owner;  // 0x18
        private UnityEngine.VFX.VFXEventAttribute m_WrapEventAttribute;  // 0x20

        // Methods
        // RVA: 0x04DAB020  token: 0x6000012
        private System.Void .ctor(System.IntPtr ptr, System.Boolean owner) { }
        // RVA: 0x0B58DBAC  token: 0x6000013
        private static UnityEngine.VFX.VFXSpawnerState CreateSpawnerStateWrapper() { }
        // RVA: 0x0B58DCF4  token: 0x6000014
        private System.Void PrepareWrapper() { }
        // RVA: 0x0B58DE04  token: 0x6000015
        private System.Void SetWrapValue(System.IntPtr ptrToSpawnerState, System.IntPtr ptrToEventAttribute) { }
        // RVA: 0x0B58DDC8  token: 0x6000016
        private System.Void Release() { }
        // RVA: 0x0B58DC50  token: 0x6000017
        protected virtual System.Void Finalize() { }
        // RVA: 0x0B58DC04  token: 0x6000018
        public virtual System.Void Dispose() { }
        // RVA: 0x0B58DCC0  token: 0x6000019
        private static System.Void Internal_Destroy(System.IntPtr ptr) { }

    }

    // TypeToken: 0x2000008  // size: 0x18
    public abstract class VisualEffectObject : UnityEngine.Object
    {
        // Methods
        // RVA: 0x0B58DF60  token: 0x600001A
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x18
    public class VisualEffectAsset : UnityEngine.VFX.VisualEffectObject
    {
        // Fields
        public static readonly System.Int32 PlayEventID;  // static @ 0x0
        public static readonly System.Int32 StopEventID;  // static @ 0x4

        // Methods
        // RVA: 0x0B58DF58  token: 0x600001B
        public System.Void .ctor() { }
        // RVA: 0x0B58DEDC  token: 0x600001C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x20
    public sealed struct VFXOutputEventArgs
    {
        // Fields
        private readonly System.Int32 <nameId>k__BackingField;  // 0x10
        private readonly UnityEngine.VFX.VFXEventAttribute <eventAttribute>k__BackingField;  // 0x18

        // Methods
        // RVA: 0x06B17430  token: 0x600001D
        public System.Void .ctor(System.Int32 nameId, UnityEngine.VFX.VFXEventAttribute eventAttribute) { }

    }

    // TypeToken: 0x200000B  // size: 0x28
    public class VisualEffect : UnityEngine.Behaviour
    {
        // Fields
        private UnityEngine.VFX.VFXEventAttribute m_cachedEventAttribute;  // 0x18
        public System.Action<UnityEngine.VFX.VFXOutputEventArgs> outputEventReceived;  // 0x20

        // Properties
        UnityEngine.VFX.VisualEffectAsset visualEffectAsset { get; /* RVA: 0x0B58E0B8 */ }

        // Methods
        // RVA: 0x0B58DFA0  token: 0x600001F
        public UnityEngine.VFX.VFXEventAttribute CreateVFXEventAttribute() { }
        // RVA: 0x0B58E024  token: 0x6000020
        private static UnityEngine.VFX.VFXEventAttribute InvokeGetCachedEventAttributeForOutputEvent_Internal(UnityEngine.VFX.VisualEffect source) { }
        // RVA: 0x0B58E064  token: 0x6000021
        private static System.Void InvokeOutputEventReceived_Internal(UnityEngine.VFX.VisualEffect source, System.Int32 eventNameId) { }

    }

}

