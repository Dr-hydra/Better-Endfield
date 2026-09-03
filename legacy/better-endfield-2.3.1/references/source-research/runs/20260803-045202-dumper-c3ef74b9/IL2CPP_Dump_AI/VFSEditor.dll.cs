// ========================================================
// Dumped by @desirepro
// Assembly: VFSEditor.dll
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

CLASS: Beyond.SourceGenerator.CameraControlConfigAttribute
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000002  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.ECSComponentAttribute
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public            System.Runtime.InteropServices.LayoutKindlayoutKind  // 0x10
  public            System.Boolean                  isTag  // 0x14
METHODS:
  RVA=0x041E1670  token=0x6000003  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardUtility
TYPE:  static class
TOKEN: 0x2000005
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_enableFastPathBuffer  // static @ 0x0
METHODS:
  RVA=-1  // generic def  token=0x6000004  System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity)
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardAttribute
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000005  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.DataNAttribute
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public            System.Int32                    capacity  // 0x10
  public            System.Int32                    tSize  // 0x14
  public            System.Boolean                  ring  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000006  System.Void .ctor()
END_CLASS

CLASS: Beyond.VFS.Editor.VFSEditorUnsafeUtils
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B8961FC  token=0x6000007  System.Void TryGetBundleDeps(Beyond.ManifestBinary.ManifestDataBinary manifestDataBinary, Beyond.ManifestBinary.ManifestDataBinary.Bundle bundle, System.Collections.Generic.List<Beyond.ManifestBinary.ManifestDataBinary.Bundle>& bundles)
  RVA=0x0B89622C  token=0x6000008  System.Void TryGetBundleDirectDeps(Beyond.ManifestBinary.ManifestDataBinary manifestDataBinary, Beyond.ManifestBinary.ManifestDataBinary.Bundle bundle, System.Collections.Generic.List<Beyond.ManifestBinary.ManifestDataBinary.Bundle>& bundles)
  RVA=0x0B8963A0  token=0x6000009  System.Void TryGetBundleDirectReverseDeps(Beyond.ManifestBinary.ManifestDataBinary manifestDataBinary, Beyond.ManifestBinary.ManifestDataBinary.Bundle bundle, System.Collections.Generic.List<Beyond.ManifestBinary.ManifestDataBinary.Bundle>& bundles)
  RVA=0x0B896514  token=0x600000A  System.Void _TryGetBundleDeps(Beyond.ManifestBinary.ManifestDataBinary manifestDataBinary, Beyond.ManifestBinary.ManifestDataBinary.Bundle bundle, System.Collections.Generic.List<Beyond.ManifestBinary.ManifestDataBinary.Bundle>& bundles)
  RVA=0x0B896178  token=0x600000B  System.String GetLogBundleName(Beyond.ManifestBinary.RuntimeManifestBinary manifestBinary, Beyond.ManifestBinary.ManifestDataBinary.Bundle bundle)
  RVA=0x0B8960F4  token=0x600000C  System.String GetLogAssetPath(Beyond.ManifestBinary.RuntimeManifestBinary manifestBinary, Beyond.ManifestBinary.ManifestDataBinary.AssetInfo asset)
  RVA=0x041E1670  token=0x600000D  System.Void .ctor()
END_CLASS

CLASS: HG.Rendering.Runtime.HGConstantBufferLayoutAttribute
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x600000E  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x0B895FFC  token=0x600000F  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x40
EXTENDS: IFix.Core.AnonymousStorey
FIELDS:
METHODS:
  RVA=0x0B896060  token=0x6000010  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x6000011  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000012  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x0B896870  token=0x6000013  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x0B896960  token=0x6000014  System.Boolean IsPatched(System.Int32 id)
  RVA=0x0B89672C  token=0x6000015  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x0B8967F8  token=0x6000016  System.Object CreateWrapper(System.Int32 id)
  RVA=0x0B8968D4  token=0x6000017  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x0B896688  token=0x6000018  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

