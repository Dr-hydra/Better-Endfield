// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.VFXModule.dll
// Classes:  11
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

CLASS: UnityEngine.VFX.VFXEventAttribute
TYPE:  sealed class
TOKEN: 0x2000002
SIZE:  0x28
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.IntPtr                   m_Ptr  // 0x10
  private           System.Boolean                  m_Owner  // 0x18
  private           UnityEngine.VFX.VisualEffectAssetm_VfxAsset  // 0x20
METHODS:
  RVA=0x0B58DAF8  token=0x6000001  System.Void .ctor(System.IntPtr ptr, System.Boolean owner, UnityEngine.VFX.VisualEffectAsset vfxAsset)
  RVA=0x0B58D808  token=0x6000002  UnityEngine.VFX.VFXEventAttribute CreateEventAttributeWrapper()
  RVA=0x0B58DA8C  token=0x6000003  System.Void SetWrapValue(System.IntPtr ptrToEventAttribute)
  RVA=0x0B58D920  token=0x6000004  System.IntPtr Internal_Create()
  RVA=0x0B58D9C4  token=0x6000005  UnityEngine.VFX.VFXEventAttribute Internal_InstanciateVFXEventAttribute(UnityEngine.VFX.VisualEffectAsset vfxAsset)
  RVA=0x0B58D980  token=0x6000006  System.Void Internal_InitFromAsset(UnityEngine.VFX.VisualEffectAsset vfxAsset)
  RVA=0x0B58DA50  token=0x6000007  System.Void Release()
  RVA=0x0B58D8B0  token=0x6000008  System.Void Finalize()
  RVA=0x0B58D864  token=0x6000009  System.Void Dispose()
  RVA=0x0B58D94C  token=0x600000A  System.Void Internal_Destroy(System.IntPtr ptr)
END_CLASS

CLASS: UnityEngine.VFX.VFXExpressionValues
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x18
FIELDS:
  private           System.IntPtr                   m_Ptr  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600000B  System.Void .ctor()
  RVA=0x0B58DB10  token=0x600000C  UnityEngine.VFX.VFXExpressionValues CreateExpressionValuesWrapper(System.IntPtr ptr)
END_CLASS

CLASS: UnityEngine.VFX.VFXCameraXRSettings
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x1C
FIELDS:
  public            System.UInt32                   viewTotal  // 0x10
  public            System.UInt32                   viewCount  // 0x14
  public            System.UInt32                   viewOffset  // 0x18
METHODS:
END_CLASS

CLASS: UnityEngine.VFX.VFXManager
TYPE:  static class
TOKEN: 0x2000005
SIZE:  0x10
FIELDS:
  private   static readonly UnityEngine.VFX.VFXCameraXRSettingskDefaultCameraXRSettings  // static @ 0x0
METHODS:
  RVA=0x0B58DB58  token=0x600000D  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.VFX.VFXSpawnerCallbacks
TYPE:  abstract class
TOKEN: 0x2000006
SIZE:  0x18
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600000E  System.Void OnPlay(UnityEngine.VFX.VFXSpawnerState state, UnityEngine.VFX.VFXExpressionValues vfxValues, UnityEngine.VFX.VisualEffect vfxComponent)
  RVA=-1  // abstract  token=0x600000F  System.Void OnUpdate(UnityEngine.VFX.VFXSpawnerState state, UnityEngine.VFX.VFXExpressionValues vfxValues, UnityEngine.VFX.VisualEffect vfxComponent)
  RVA=-1  // abstract  token=0x6000010  System.Void OnStop(UnityEngine.VFX.VFXSpawnerState state, UnityEngine.VFX.VFXExpressionValues vfxValues, UnityEngine.VFX.VisualEffect vfxComponent)
  RVA=0x03573370  token=0x6000011  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.VFX.VFXSpawnerState
TYPE:  sealed class
TOKEN: 0x2000007
SIZE:  0x28
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.IntPtr                   m_Ptr  // 0x10
  private           System.Boolean                  m_Owner  // 0x18
  private           UnityEngine.VFX.VFXEventAttributem_WrapEventAttribute  // 0x20
METHODS:
  RVA=0x04DAB020  token=0x6000012  System.Void .ctor(System.IntPtr ptr, System.Boolean owner)
  RVA=0x0B58DBAC  token=0x6000013  UnityEngine.VFX.VFXSpawnerState CreateSpawnerStateWrapper()
  RVA=0x0B58DCF4  token=0x6000014  System.Void PrepareWrapper()
  RVA=0x0B58DE04  token=0x6000015  System.Void SetWrapValue(System.IntPtr ptrToSpawnerState, System.IntPtr ptrToEventAttribute)
  RVA=0x0B58DDC8  token=0x6000016  System.Void Release()
  RVA=0x0B58DC50  token=0x6000017  System.Void Finalize()
  RVA=0x0B58DC04  token=0x6000018  System.Void Dispose()
  RVA=0x0B58DCC0  token=0x6000019  System.Void Internal_Destroy(System.IntPtr ptr)
END_CLASS

CLASS: UnityEngine.VFX.VisualEffectObject
TYPE:  abstract class
TOKEN: 0x2000008
SIZE:  0x18
EXTENDS: UnityEngine.Object
FIELDS:
METHODS:
  RVA=0x0B58DF60  token=0x600001A  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.VFX.VisualEffectAsset
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x18
EXTENDS: UnityEngine.VFX.VisualEffectObject
FIELDS:
  public    static readonly System.Int32                    PlayEventID  // static @ 0x0
  public    static readonly System.Int32                    StopEventID  // static @ 0x4
METHODS:
  RVA=0x0B58DF58  token=0x600001B  System.Void .ctor()
  RVA=0x0B58DEDC  token=0x600001C  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.VFX.VFXOutputEventArgs
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x20
FIELDS:
  private   readonly System.Int32                    <nameId>k__BackingField  // 0x10
  private   readonly UnityEngine.VFX.VFXEventAttribute<eventAttribute>k__BackingField  // 0x18
METHODS:
  RVA=0x06B17430  token=0x600001D  System.Void .ctor(System.Int32 nameId, UnityEngine.VFX.VFXEventAttribute eventAttribute)
END_CLASS

CLASS: UnityEngine.VFX.VisualEffect
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x28
EXTENDS: UnityEngine.Behaviour
FIELDS:
  private           UnityEngine.VFX.VFXEventAttributem_cachedEventAttribute  // 0x18
  public            System.Action<UnityEngine.VFX.VFXOutputEventArgs>outputEventReceived  // 0x20
PROPERTIES:
  visualEffectAsset  get=0x0B58E0B8
METHODS:
  RVA=0x0B58DFA0  token=0x600001F  UnityEngine.VFX.VFXEventAttribute CreateVFXEventAttribute()
  RVA=0x0B58E024  token=0x6000020  UnityEngine.VFX.VFXEventAttribute InvokeGetCachedEventAttributeForOutputEvent_Internal(UnityEngine.VFX.VisualEffect source)
  RVA=0x0B58E064  token=0x6000021  System.Void InvokeOutputEventReceived_Internal(UnityEngine.VFX.VisualEffect source, System.Int32 eventNameId)
END_CLASS

