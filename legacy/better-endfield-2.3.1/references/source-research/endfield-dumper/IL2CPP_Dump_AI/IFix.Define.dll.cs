// ========================================================
// Dumped by @desirepro
// Assembly: IFix.Define.dll
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

CLASS: Beyond.HotFix.HotfixDownloadPayload
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x30
FIELDS:
  public            System.Int32                    effectiveVersion  // 0x10
  public            System.String                   luahotfix  // 0x18
  public            System.String                   i18nhotfix  // 0x20
  public            System.String                   ifixhotfix  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
END_CLASS

CLASS: Beyond.HotFix.PatchClipboardData
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x30
FIELDS:
  public            System.Int32                    ver  // 0x10
  public            System.Int32                    overrideVFSIFixVersionFrom  // 0x14
  public            System.Int32                    overrideVFSIFixVersionTo  // 0x18
  public            System.String                   platform  // 0x20
  public            System.Collections.Generic.List<Beyond.HotFix.PatchItem>patches  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000002  System.Void .ctor()
END_CLASS

CLASS: Beyond.HotFix.LuaPatchClipboardData
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x30
FIELDS:
  public            System.Int32                    ver  // 0x10
  public            System.Int32                    overrideVFSIFixVersionFrom  // 0x14
  public            System.Int32                    overrideVFSIFixVersionTo  // 0x18
  public            System.String                   platform  // 0x20
  public            System.Collections.Generic.List<Beyond.HotFix.PatchItem>luaPatches  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000003  System.Void .ctor()
END_CLASS

CLASS: Beyond.HotFix.PatchItem
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x30
FIELDS:
  public            System.String                   name  // 0x10
  public            System.String                   md5  // 0x18
  public            System.Int32                    size  // 0x20
  public            System.String                   data  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000004  System.Void .ctor()
END_CLASS

CLASS: Beyond.HotFix.HotfixDownloadPackage
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x28
FIELDS:
  public            System.String                   alg  // 0x10
  public            System.String                   data  // 0x18
  public            System.String                   sign  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000005  System.Void .ctor()
END_CLASS

CLASS: Beyond.HotFix.HotFixPatchDefine
TYPE:  static class
TOKEN: 0x2000007
SIZE:  0x10
FIELDS:
  public    static readonly System.String                   hotFixPatchFileExtension  // static @ 0x0
  public    static  System.String                   HOTFIX_DOWNLOAD_ALGORITHM  // const
  public    static readonly System.String                   CLIENT_NET_RSA_PUBLIC_KEY  // static @ 0x8
  private   static readonly System.Byte[]                   s_luaPatchAesKey  // static @ 0x10
  private   static readonly System.Byte[]                   s_luaPatchAesIV  // static @ 0x18
  private   static readonly System.Byte[]                   s_hotfixDownloadHmacKey  // static @ 0x20
  private   static readonly System.Byte[]                   s_hotfixDownloadEnvelopeAesKey  // static @ 0x28
  private   static readonly System.Byte[]                   s_hotfixDownloadEnvelopeAesIV  // static @ 0x30
METHODS:
  RVA=0x09E13E40  token=0x6000006  System.Byte[] EncryptLuaPatchPayload(System.Byte[] luaBytes)
  RVA=0x03C0D460  token=0x6000007  System.Byte[] DecryptLuaPatchPayload(System.Byte[] encryptedBytes, System.Int32 offset, System.Int32 count)
  RVA=0x09E140F4  token=0x6000008  System.String PackHotfixDownloadText(System.String rawJson)
  RVA=0x03C0B760  token=0x6000009  System.Boolean TryDecodeHotfixDownloadText(System.String downloadText, System.String& json, System.String& error)
  RVA=0x09E142AC  token=0x600000A  System.Boolean TryExtractLuaHotfix(System.String json, System.String& luaHotfix, System.String& error)
  RVA=0x09E1446C  token=0x600000B  System.Boolean VerifyMD5(System.Byte[] bytes, System.String expectMd5)
  RVA=0x09E13DB0  token=0x600000C  System.Byte[] EncryptHotfixDownloadEnvelope(System.Byte[] bytes)
  RVA=0x03C0A7A0  token=0x600000D  System.Byte[] DecryptHotfixDownloadEnvelope(System.Byte[] encryptedBytes, System.Int32 offset, System.Int32 count)
  RVA=0x03C0B3B0  token=0x600000E  System.Boolean _VerifyHotfixDownloadHmac(System.String content, System.String signatureBase64)
  RVA=0x03C0A530  token=0x600000F  System.Byte[] _ComputeHotfixDownloadHmac(System.String content)
  RVA=0x04593100  token=0x6000010  System.Boolean _FixedTimeEquals(System.Byte[] left, System.Byte[] right)
  RVA=0x09E14650  token=0x6000011  System.Byte[] _EncryptAes(System.Byte[] bytes, System.Byte[] key, System.Byte[] iv)
  RVA=0x03C0B460  token=0x6000012  System.Byte[] _DecryptAes(System.Byte[] encryptedBytes, System.Int32 offset, System.Int32 count, System.Byte[] key, System.Byte[] iv)
  RVA=0x04994BE0  token=0x6000013  System.Void .cctor()
END_CLASS

CLASS: IFix.HotfixPatchAttribute
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000014  System.Void .ctor()
END_CLASS

CLASS: IFix.HotfixInterpretAttribute
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000015  System.Void .ctor()
END_CLASS

CLASS: IFix.IFixIgnoreAttribute
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000016  System.Void .ctor()
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x6000017  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x09E148CC  token=0x6000018  System.Byte[] __Gen_Wrap_0(System.Object P0)
  RVA=0x09E149BC  token=0x6000019  System.Byte[] __Gen_Wrap_1(System.Object P0, System.Int32 P1, System.Int32 P2)
  RVA=0x09E14AE0  token=0x600001A  System.Byte[] __Gen_Wrap_2(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x09E14BF4  token=0x600001B  System.String __Gen_Wrap_3(System.Object P0)
  RVA=0x09E14CD0  token=0x600001C  System.Byte[] __Gen_Wrap_4(System.Object P0, System.Int32 P1, System.Int32 P2, System.Object P3, System.Object P4)
  RVA=0x06366BC4  token=0x600001D  System.Boolean __Gen_Wrap_5(System.Object P0, System.Object P1)
  RVA=0x09E14E14  token=0x600001E  System.Boolean __Gen_Wrap_6(System.Object P0, System.String& P1, System.String& P2)
  RVA=0x04D37870  token=0x600001F  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x40
EXTENDS: IFix.Core.AnonymousStorey
FIELDS:
METHODS:
  RVA=0x09E14F90  token=0x6000020  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x6000021  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x200000D
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000022  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x09E1520C  token=0x6000023  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x04593190  token=0x6000024  System.Boolean IsPatched(System.Int32 id)
  RVA=0x09E150C8  token=0x6000025  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x09E15194  token=0x6000026  System.Object CreateWrapper(System.Int32 id)
  RVA=0x09E15270  token=0x6000027  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x09E15024  token=0x6000028  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: IFix.IDMAP0
TYPE:  sealed struct
TOKEN: 0x200000E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchDefine-EncryptLuaPatchPayload0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchDefine-DecryptLuaPatchPayload0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchDefine-_ComputeHotfixDownloadHmac0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchDefine-_EncryptAes0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchDefine-EncryptHotfixDownloadEnvelope0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchDefine-PackHotfixDownloadText0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchDefine-_DecryptAes0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchDefine-DecryptHotfixDownloadEnvelope0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchDefine-_FixedTimeEquals0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchDefine-_VerifyHotfixDownloadHmac0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchDefine-TryDecodeHotfixDownloadText0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchDefine-TryExtractLuaHotfix0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchDefine-VerifyMD50  // const
METHODS:
END_CLASS

