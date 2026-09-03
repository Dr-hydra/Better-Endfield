// ========================================================
// Dumped by @desirepro
// Assembly: Unsafe.VFS.dll
// Classes:  8
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

CLASS: Beyond.VFS.FVFSUntrackedLowIOReadHandle
TYPE:  sealed struct
TOKEN: 0x2000002
SIZE:  0x30
IMPLEMENTS: System.IDisposable
FIELDS:
  public            Unity.IO.LowLevel.Unsafe.ReadCommand*readCommand  // 0x10
  public            Unity.IO.LowLevel.Unsafe.ReadHandlereadHandle  // 0x18
  public            System.Boolean                  isEnc  // 0x28
METHODS:
  RVA=0x03352250  token=0x6000001  System.Void .ctor(System.String path, System.Int64 offset, System.Int64 len, System.Boolean async, System.Boolean isEnc)
  RVA=0x0B5AB7EC  token=0x6000002  System.Void .ctor(Beyond.UnSafeString path, System.Int64 offset, System.Int64 len, System.Boolean async, System.Boolean isEnc)
  RVA=0x033529E0  token=0x6000003  System.Void Dispose()
  RVA=0x033521D0  token=0x6000004  System.Boolean IsValid()
  RVA=0x03350090  token=0x6000005  System.Boolean IsComplete()
  RVA=0x03352DE0  token=0x6000006  Unity.IO.LowLevel.Unsafe.ReadStatus GetStatus()
  RVA=0x03585A20  token=0x6000007  Unity.Collections.NativeArray<System.Byte> GetData()
  RVA=0x03352870  token=0x6000008  Unity.Collections.NativeArray<System.Byte> GetDataThread()
END_CLASS

CLASS: Beyond.VFS.FVFSPathInfo
TYPE:  sealed struct
TOKEN: 0x2000003
SIZE:  0x30
FIELDS:
  public            System.String                   fullPathFormat  // 0x10
  public            System.String                   param0  // 0x18
  public            System.String                   param1  // 0x20
  public            System.String                   param2  // 0x28
METHODS:
  RVA=0x02F766E0  token=0x6000009  System.String GetResultString()
  RVA=0x02F732A0  token=0x600000A  Beyond.UnSafeString GetUnsafeString()
END_CLASS

CLASS: Beyond.VFS.ThreadUnsafeStringUtils
TYPE:  static class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Concurrent.ConcurrentStack<Beyond.UnSafeString>s_stack  // static @ 0x0
METHODS:
  RVA=0x02F24C50  token=0x600000B  Beyond.UnSafeString ThreadStringAlloc()
  RVA=0x02F23CC0  token=0x600000C  System.Void ThreadStringRelease(Beyond.UnSafeString value)
  RVA=0x02F24DF0  token=0x600000D  Beyond.UnSafeString AppendPathInfo(Beyond.UnSafeString str, Beyond.VFS.FVFSPathInfo& info)
  RVA=0x04D26E80  token=0x600000E  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x600000F  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x0B5AB93C  token=0x6000010  System.Void __Gen_Wrap_0(Beyond.VFS.FVFSUntrackedLowIOReadHandle& P0)
  RVA=0x0B5ABA7C  token=0x6000011  System.Boolean __Gen_Wrap_1(Beyond.VFS.FVFSUntrackedLowIOReadHandle& P0)
  RVA=0x0B5ABBCC  token=0x6000012  Unity.IO.LowLevel.Unsafe.ReadStatus __Gen_Wrap_2(Beyond.VFS.FVFSUntrackedLowIOReadHandle& P0)
  RVA=0x0B5ABD1C  token=0x6000013  Unity.Collections.NativeArray<System.Byte> __Gen_Wrap_3(Beyond.VFS.FVFSUntrackedLowIOReadHandle& P0)
  RVA=0x0B5ABE98  token=0x6000014  System.String __Gen_Wrap_4(Beyond.VFS.FVFSPathInfo& P0)
  RVA=0x0B5AC000  token=0x6000015  Beyond.UnSafeString __Gen_Wrap_5(Beyond.VFS.FVFSPathInfo& P0)
  RVA=0x0B5AC168  token=0x6000016  Beyond.UnSafeString __Gen_Wrap_6()
  RVA=0x063633B4  token=0x6000017  System.Void __Gen_Wrap_7(System.Object P0)
  RVA=0x0B5AC234  token=0x6000018  Beyond.UnSafeString __Gen_Wrap_8(System.Object P0, Beyond.VFS.FVFSPathInfo& P1)
  RVA=0x04D37B10  token=0x6000019  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x40
EXTENDS: IFix.Core.AnonymousStorey
FIELDS:
METHODS:
  RVA=0x0B5AC3B8  token=0x600001A  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x600001B  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x600001C  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x0B5AC634  token=0x600001D  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x02F733D0  token=0x600001E  System.Boolean IsPatched(System.Int32 id)
  RVA=0x0B5AC4F0  token=0x600001F  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x0B5AC5BC  token=0x6000020  System.Object CreateWrapper(System.Int32 id)
  RVA=0x0B5AC698  token=0x6000021  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x0B5AC44C  token=0x6000022  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: IFix.IDMAP0
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  IFix.IDMAP0                     Beyond-VFS-FVFSUntrackedLowIOReadHandle-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-VFS-FVFSUntrackedLowIOReadHandle-IsValid0  // const
  public    static  IFix.IDMAP0                     Beyond-VFS-FVFSUntrackedLowIOReadHandle-IsComplete0  // const
  public    static  IFix.IDMAP0                     Beyond-VFS-FVFSUntrackedLowIOReadHandle-GetStatus0  // const
  public    static  IFix.IDMAP0                     Beyond-VFS-FVFSUntrackedLowIOReadHandle-GetData0  // const
  public    static  IFix.IDMAP0                     Beyond-VFS-FVFSUntrackedLowIOReadHandle-GetDataThread0  // const
  public    static  IFix.IDMAP0                     Beyond-VFS-FVFSPathInfo-GetResultString0  // const
  public    static  IFix.IDMAP0                     Beyond-VFS-FVFSPathInfo-GetUnsafeString0  // const
  public    static  IFix.IDMAP0                     Beyond-VFS-ThreadUnsafeStringUtils-ThreadStringAlloc0  // const
  public    static  IFix.IDMAP0                     Beyond-VFS-ThreadUnsafeStringUtils-ThreadStringRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-VFS-ThreadUnsafeStringUtils-AppendPathInfo0  // const
METHODS:
END_CLASS

