// ========================================================
// Dumped by @desirepro
// Assembly: SRSA.dll
// Classes:  6
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

CLASS: Beyond.SRSA
TYPE:  static class
TOKEN: 0x2000002
SIZE:  0x10
FIELDS:
  private   static  System.String                   EncryptionLib  // const
  public    static  System.Int32                    s_keyLength  // const
METHODS:
  RVA=0x05397C3C  token=0x6000001  System.IntPtr mono_method_h_get_code(System.IntPtr message, System.Int32 message_size)
  RVA=0x05397C34  token=0x6000002  System.IntPtr mono_method_h_set_code(System.IntPtr message)
  RVA=0x05397C44  token=0x6000003  System.Void mono_method_h_remove_code(System.IntPtr message)
  RVA=0x047C96C0  token=0x6000004  System.Void LoadFromFile(System.Span<System.Byte> message, System.Span<System.Byte>& buffer)
  RVA=0x047C9530  token=0x6000005  System.Void LoadFromFile2(System.Span<System.Byte> inputData, System.Span<System.Byte>& outputBytes)
  RVA=0x0AC87AB4  token=0x6000006  System.Void test()
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x6000007  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x0AC878B8  token=0x6000008  System.Void __Gen_Wrap_0(System.Span<System.Byte> P0, System.Span<System.Byte>& P1)
  RVA=0x063761FC  token=0x6000009  System.Void __Gen_Wrap_1()
  RVA=0x04D37720  token=0x600000A  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x40
EXTENDS: IFix.Core.AnonymousStorey
FIELDS:
METHODS:
  RVA=0x0AC87A20  token=0x600000B  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x600000C  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x600000D  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x0AC87CDC  token=0x600000E  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x047C98C0  token=0x600000F  System.Boolean IsPatched(System.Int32 id)
  RVA=0x0AC87B98  token=0x6000010  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x0AC87C64  token=0x6000011  System.Object CreateWrapper(System.Int32 id)
  RVA=0x0AC87D40  token=0x6000012  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x0AC87AF4  token=0x6000013  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: IFix.IDMAP0
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  IFix.IDMAP0                     Beyond-SRSA-LoadFromFile0  // const
  public    static  IFix.IDMAP0                     Beyond-SRSA-LoadFromFile20  // const
  public    static  IFix.IDMAP0                     Beyond-SRSA-test0  // const
METHODS:
END_CLASS

