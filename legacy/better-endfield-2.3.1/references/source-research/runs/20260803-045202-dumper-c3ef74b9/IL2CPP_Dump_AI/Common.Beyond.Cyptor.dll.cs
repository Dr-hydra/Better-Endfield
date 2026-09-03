// ========================================================
// Dumped by @desirepro
// Assembly: Common.Beyond.Cyptor.dll
// Classes:  10
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

CLASS: __StaticArrayInitTypeSize=15
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x1F
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=1024
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x410
FIELDS:
METHODS:
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=102412F3E0576D447EB37B36D82BA0C1C5481B8F0D12FDC70347CE4A076B229D4C86  // static @ 0x0
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=1595524C911AFEB934ED10A8D7FA3546CD5EBA4BFD0D0E8A1ABB244985A66F2D3F  // static @ 0x400
METHODS:
END_CLASS

CLASS: Beyond.Cryptor.Crc32Utils
TYPE:  static class
TOKEN: 0x2000002
SIZE:  0x10
FIELDS:
  private   static readonly System.String                   UNITY_CRC_SALT  // static @ 0x0
  private   static readonly System.Int32                    MAX_CACHE_CHAR_NUM  // static @ 0x8
  private   static readonly System.Int32                    MAX_CACHE_CHAR_BYTE_NUM  // static @ 0xc
  private   static readonly System.UInt32[]                 crctable  // static @ 0x10
METHODS:
  RVA=0x067CD600  token=0x6000001  System.UInt32 CalculateCrc32(System.Byte[] data, System.Int32 offset, System.Int32 length, System.UInt32 startCRC)
  RVA=0x067CD6D0  token=0x6000002  System.UInt32 CalculateCrc32(System.ReadOnlySpan<System.Byte> data, System.Int32 offset, System.Int32 length, System.UInt32 startCRC)
  RVA=0x067CD898  token=0x6000003  System.Int32 UnityCRC(System.String s)
  RVA=0x067CD7D4  token=0x6000004  System.Int64 UnityCRC64(System.String s)
  RVA=0x02F83150  token=0x6000005  System.Int64 XXHash64(System.String input)
  RVA=0x067CD8E8  token=0x6000006  System.UInt64 XXHash64(System.Byte[] bytes)
  RVA=0x02F84A90  token=0x6000007  System.Int32 XXHash32(System.String input)
  RVA=0x067CD5A0  token=0x6000008  System.Int32 CRC32(System.Byte[] input, System.Int32 offset, System.Int32 length)
  RVA=0x0307DF20  token=0x6000009  System.Int32 CRC32(System.String s)
  RVA=0x04CA9700  token=0x600000A  System.Void .cctor()
END_CLASS

CLASS: Beyond.Cryptor.RSACryptoUtils
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x10
FIELDS:
  public    static  System.Int32                    SignLen  // const
METHODS:
  RVA=0x03FE3E50  token=0x600000B  System.Security.Cryptography.RSACryptoServiceProvider CreateRsaProviderFromPrivateKey(System.String privateKey)
  RVA=0x03FE4C70  token=0x600000C  System.Int32 _GetIntegerSize(System.IO.BinaryReader binr)
  RVA=0x067CE644  token=0x600000D  System.Security.Cryptography.RSACryptoServiceProvider CreateRsaProviderFromPublicKey(System.String publicKeyString)
  RVA=0x067CE6C4  token=0x600000E  System.Boolean VerfyStream(System.IO.Stream stream, System.String publicKey)
  RVA=0x067CDDD0  token=0x600000F  System.Security.Cryptography.RSACryptoServiceProvider CreateRsaProviderFromPublicKey(System.Byte[] x509key)
  RVA=0x067CE8DC  token=0x6000010  System.Boolean _CompareBytearrays(System.Byte[] a, System.Byte[] b)
  RVA=0x041E1670  token=0x6000011  System.Void .ctor()
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x6000012  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x067CD978  token=0x6000013  System.UInt32 __Gen_Wrap_0(System.ReadOnlySpan<System.Byte> P0, System.Int32 P1, System.Int32 P2, System.UInt32 P3)
  RVA=0x06360E84  token=0x6000014  System.Int32 __Gen_Wrap_1(System.Object P0)
  RVA=0x067CDAC0  token=0x6000015  System.Int64 __Gen_Wrap_2(System.Object P0)
  RVA=0x067CDB90  token=0x6000016  System.UInt64 __Gen_Wrap_3(System.Object P0)
  RVA=0x067CDC4C  token=0x6000017  System.Security.Cryptography.RSACryptoServiceProvider __Gen_Wrap_4(System.Object P0)
  RVA=0x06366BC4  token=0x6000018  System.Boolean __Gen_Wrap_5(System.Object P0, System.Object P1)
  RVA=0x04D37250  token=0x6000019  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x40
EXTENDS: IFix.Core.AnonymousStorey
FIELDS:
METHODS:
  RVA=0x067CDD3C  token=0x600001A  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x600001B  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x600001C  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x067CEB60  token=0x600001D  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x02F83580  token=0x600001E  System.Boolean IsPatched(System.Int32 id)
  RVA=0x067CEA1C  token=0x600001F  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x067CEAE8  token=0x6000020  System.Object CreateWrapper(System.Int32 id)
  RVA=0x067CEBC4  token=0x6000021  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x067CE978  token=0x6000022  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: IFix.IDMAP0
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  IFix.IDMAP0                     Beyond-Cryptor-Crc32Utils-CalculateCrc320  // const
  public    static  IFix.IDMAP0                     Beyond-Cryptor-Crc32Utils-UnityCRC0  // const
  public    static  IFix.IDMAP0                     Beyond-Cryptor-Crc32Utils-UnityCRC640  // const
  public    static  IFix.IDMAP0                     Beyond-Cryptor-Crc32Utils-XXHash640  // const
  public    static  IFix.IDMAP0                     Beyond-Cryptor-Crc32Utils-XXHash641  // const
  public    static  IFix.IDMAP0                     Beyond-Cryptor-Crc32Utils-XXHash320  // const
  public    static  IFix.IDMAP0                     Beyond-Cryptor-Crc32Utils-CRC320  // const
  public    static  IFix.IDMAP0                     Beyond-Cryptor-RSACryptoUtils-_GetIntegerSize0  // const
  public    static  IFix.IDMAP0                     Beyond-Cryptor-RSACryptoUtils-CreateRsaProviderFromPrivateKey0  // const
  public    static  IFix.IDMAP0                     Beyond-Cryptor-RSACryptoUtils-CreateRsaProviderFromPublicKey0  // const
  public    static  IFix.IDMAP0                     Beyond-Cryptor-RSACryptoUtils-CreateRsaProviderFromPublicKey1  // const
  public    static  IFix.IDMAP0                     Beyond-Cryptor-RSACryptoUtils-VerfyStream0  // const
  public    static  IFix.IDMAP0                     Beyond-Cryptor-RSACryptoUtils-_CompareBytearrays0  // const
METHODS:
END_CLASS

