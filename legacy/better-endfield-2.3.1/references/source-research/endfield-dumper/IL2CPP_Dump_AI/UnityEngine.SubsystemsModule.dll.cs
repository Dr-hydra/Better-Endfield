// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.SubsystemsModule.dll
// Classes:  14
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

CLASS: UnityEngine.IntegratedSubsystem
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x20
FIELDS:
  private           System.IntPtr                   m_Ptr  // 0x10
  private           UnityEngine.ISubsystemDescriptorm_SubsystemDescriptor  // 0x18
METHODS:
  RVA=0x0B42B6FC  token=0x6000001  System.Void SetHandle(UnityEngine.IntegratedSubsystem subsystem)
  RVA=0x041E1670  token=0x6000002  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.IntegratedSubsystem`1
TYPE:  class
TOKEN: 0x2000003
EXTENDS: UnityEngine.IntegratedSubsystem
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000003  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.IntegratedSubsystemDescriptor
TYPE:  abstract class
TOKEN: 0x2000004
SIZE:  0x18
IMPLEMENTS: UnityEngine.ISubsystemDescriptor
FIELDS:
  private           System.IntPtr                   m_Ptr  // 0x10
PROPERTIES:
  id  get=0x0B42B6F0
METHODS:
  RVA=0x041E1670  token=0x6000005  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.IntegratedSubsystemDescriptor`1
TYPE:  class
TOKEN: 0x2000005
EXTENDS: UnityEngine.IntegratedSubsystemDescriptor
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000006  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.SubsystemDescriptorBindings
TYPE:  static class
TOKEN: 0x2000006
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B42B78C  token=0x6000007  System.String GetId(System.IntPtr descriptorPtr)
END_CLASS

CLASS: UnityEngine.ISubsystemDescriptor
TYPE:  interface
TOKEN: 0x2000007
FIELDS:
PROPERTIES:
  id  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Subsystem
TYPE:  abstract class
TOKEN: 0x2000008
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000009  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.SubsystemDescriptor
TYPE:  abstract class
TOKEN: 0x2000009
SIZE:  0x18
IMPLEMENTS: UnityEngine.ISubsystemDescriptor
FIELDS:
  private           System.String                   <id>k__BackingField  // 0x10
PROPERTIES:
  id  get=0x02B2ECC0
METHODS:
  RVA=0x041E1670  token=0x600000B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Internal_SubsystemDescriptors
TYPE:  static class
TOKEN: 0x200000A
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B42B740  token=0x600000C  System.Void Internal_AddDescriptor(UnityEngine.SubsystemDescriptor descriptor)
END_CLASS

CLASS: UnityEngine.SubsystemManager
TYPE:  static class
TOKEN: 0x200000B
SIZE:  0x10
FIELDS:
  private   static  System.Action                   beforeReloadSubsystems  // static @ 0x0
  private   static  System.Action                   afterReloadSubsystems  // static @ 0x8
  private   static  System.Collections.Generic.List<UnityEngine.IntegratedSubsystem>s_IntegratedSubsystems  // static @ 0x10
  private   static  System.Collections.Generic.List<UnityEngine.SubsystemsImplementation.SubsystemWithProvider>s_StandaloneSubsystems  // static @ 0x18
  private   static  System.Collections.Generic.List<UnityEngine.Subsystem>s_DeprecatedSubsystems  // static @ 0x20
  private   static  System.Action                   reloadSubsytemsStarted  // static @ 0x28
  private   static  System.Action                   reloadSubsytemsCompleted  // static @ 0x30
METHODS:
  RVA=0x0B42C00C  token=0x600000D  System.Void ReloadSubsystemsStarted()
  RVA=0x0B42BF5C  token=0x600000E  System.Void ReloadSubsystemsCompleted()
  RVA=0x0B42BEC8  token=0x600000F  System.Void InitializeIntegratedSubsystem(System.IntPtr ptr, UnityEngine.IntegratedSubsystem subsystem)
  RVA=0x0B42BBAC  token=0x6000010  System.Void ClearSubsystems()
  RVA=0x0B42C0BC  token=0x6000011  System.Void StaticConstructScriptingClassMap()
  RVA=0x0B42C0E8  token=0x6000012  System.Void .cctor()
  RVA=0x0B42BD88  token=0x6000013  UnityEngine.IntegratedSubsystem GetIntegratedSubsystemByPtr(System.IntPtr ptr)
END_CLASS

CLASS: UnityEngine.SubsystemsImplementation.SubsystemDescriptorStore
TYPE:  static class
TOKEN: 0x200000C
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.List<UnityEngine.IntegratedSubsystemDescriptor>s_IntegratedDescriptors  // static @ 0x0
  private   static  System.Collections.Generic.List<UnityEngine.SubsystemsImplementation.SubsystemDescriptorWithProvider>s_StandaloneDescriptors  // static @ 0x8
  private   static  System.Collections.Generic.List<UnityEngine.SubsystemDescriptor>s_DeprecatedDescriptors  // static @ 0x10
METHODS:
  RVA=0x0B42B934  token=0x6000014  System.Void InitializeManagedDescriptor(System.IntPtr ptr, UnityEngine.IntegratedSubsystemDescriptor desc)
  RVA=0x0B42B7C0  token=0x6000015  System.Void ClearManagedDescriptors()
  RVA=0x0B42BA1C  token=0x6000016  System.Void ReportSingleSubsystemAnalytics(System.String id)
  RVA=-1  // generic def  token=0x6000017  System.Void RegisterDescriptor(TDescriptor descriptor, System.Collections.Generic.List<TBaseTypeInList> storeInList)
  RVA=0x0B42B9B8  token=0x6000018  System.Void RegisterDeprecatedDescriptor(UnityEngine.SubsystemDescriptor descriptor)
  RVA=0x0B42BA50  token=0x6000019  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.SubsystemsImplementation.SubsystemDescriptorWithProvider
TYPE:  abstract class
TOKEN: 0x200000D
SIZE:  0x18
IMPLEMENTS: UnityEngine.ISubsystemDescriptor
FIELDS:
  private           System.String                   <id>k__BackingField  // 0x10
PROPERTIES:
  id  get=0x02B2ECC0
METHODS:
  RVA=0x041E1670  token=0x600001B  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.SubsystemsImplementation.SubsystemWithProvider
TYPE:  abstract class
TOKEN: 0x200000E
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600001C  System.Void .ctor()
END_CLASS

