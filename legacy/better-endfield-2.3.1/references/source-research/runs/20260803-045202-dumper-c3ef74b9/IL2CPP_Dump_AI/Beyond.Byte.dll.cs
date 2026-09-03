// ========================================================
// Dumped by @desirepro
// Assembly: Beyond.Byte.dll
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

CLASS: Beyond.Byte.UInt128
TYPE:  sealed struct
TOKEN: 0x2000002
SIZE:  0x20
IMPLEMENTS: System.IComparable`1 System.IEquatable`1
FIELDS:
  public            System.UInt64                   high  // 0x10
  public            System.UInt64                   low  // 0x18
METHODS:
  RVA=0x0636D0AC  token=0x6000001  System.Boolean Equals(System.Object obj)
  RVA=0x02F7AAE0  token=0x6000002  System.Int32 GetHashCode()
  RVA=0x04D861B0  token=0x6000003  System.Void .ctor(System.UInt64 high, System.UInt64 low)
  RVA=0x0636D300  token=0x6000004  System.Void .ctor(System.Byte[] bytes)
  RVA=0x0636D3BC  token=0x6000005  Beyond.Byte.UInt128 op_Implicit(System.Byte[] bytes)
  RVA=0x0636D150  token=0x6000006  System.Guid ToGuid()
  RVA=0x0636D02C  token=0x6000007  System.Int32 CompareTo(Beyond.Byte.UInt128 other)
  RVA=0x02F7BC80  token=0x6000008  System.Boolean Equals(Beyond.Byte.UInt128 other)
  RVA=0x0636D33C  token=0x6000009  System.Boolean op_Equality(Beyond.Byte.UInt128 a, Beyond.Byte.UInt128 b)
  RVA=0x0636D430  token=0x600000A  System.Boolean op_Inequality(Beyond.Byte.UInt128 a, Beyond.Byte.UInt128 B)
  RVA=0x0636D250  token=0x600000B  System.Boolean <>iFixBaseProxy_Equals(System.Object P0)
  RVA=0x0636D2B0  token=0x600000C  System.Int32 <>iFixBaseProxy_GetHashCode()
END_CLASS

CLASS: Beyond.Byte.ByteBufStream
TYPE:  sealed struct
TOKEN: 0x2000003
SIZE:  0x20
FIELDS:
  public            System.Int32                    currentIdx  // 0x10
  public            System.Byte[]                   datas  // 0x18
METHODS:
  RVA=0x02F7B350  token=0x600000D  System.Int32 GetCurrentIdx()
  RVA=0x06367FE8  token=0x600000E  System.Byte[] GetRaw()
  RVA=0x06367F58  token=0x600000F  System.Byte[] GetDataCopy()
  RVA=0x06368034  token=0x6000010  System.UInt32 ReadUInt()
  RVA=0x02F78F00  token=0x6000011  System.Int32 ReadInt()
  RVA=0x02F793A0  token=0x6000012  System.Int32 ReadByte()
  RVA=0x06368090  token=0x6000013  System.UInt64 ReadULong()
  RVA=0x02F7A6D0  token=0x6000014  System.Int64 ReadLong()
  RVA=0x02F78DE0  token=0x6000015  Beyond.Byte.UInt128 ReadUInt128()
  RVA=0x03117190  token=0x6000016  System.String ReadUTF8()
  RVA=0x02F7B530  token=0x6000017  System.Void SkipReadUTF8()
  RVA=0x063680EC  token=0x6000018  System.Void WriteByte(System.Byte val)
  RVA=0x06368294  token=0x6000019  System.Void WriteUInt(System.UInt32 val)
  RVA=0x06368150  token=0x600001A  System.Void WriteInt(System.Int32 val)
  RVA=0x06368300  token=0x600001B  System.Void WriteULong(System.UInt64 val)
  RVA=0x063681B0  token=0x600001C  System.Void WriteLong(System.Int64 val)
  RVA=0x06368210  token=0x600001D  System.Void WriteUInt128(Beyond.Byte.UInt128 val)
  RVA=0x0636836C  token=0x600001E  System.Void WriteUTF8(System.String val)
  RVA=0x02F7B4A0  token=0x600001F  System.Void SkipBytes(System.Int32 offset)
  RVA=0x031170E0  token=0x6000020  System.Int32 RemainBytes()
  RVA=0x03117120  token=0x6000021  Beyond.Byte.ByteBufStream CreateFromByte(System.Byte[] bs, System.Int32 startIdx)
  RVA=0x06367EB0  token=0x6000022  Beyond.Byte.ByteBufStream Create(System.Int32 cap)
END_CLASS

CLASS: Beyond.Byte.ByteHelper
TYPE:  static class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x02F79510  token=0x6000023  Beyond.Byte.UInt128 ReadUInt128(System.Byte[] bs, System.Int32 startIndex, System.Boolean bIsLittle)
  RVA=0x02F7A7F0  token=0x6000024  System.Int64 ReadLong(System.Byte[] bs, System.Int32 startIndex, System.Boolean bIsLittle)
  RVA=0x02F7A8A0  token=0x6000025  System.UInt64 ReadULong(System.Byte[] bs, System.Int32 startIndex, System.Boolean bIsLittle)
  RVA=0x02F791F0  token=0x6000026  System.UInt32 ReadUInt(System.Byte[] bs, System.Int32 startIndex, System.Boolean bIsLittle)
  RVA=0x02F790A0  token=0x6000027  System.Int32 ReadInt(System.Byte[] bs, System.Int32 startIndex, System.Boolean bIsLittle)
  RVA=0x02F792E0  token=0x6000028  System.UInt16 ReadUShort(System.Byte[] bs, System.Int32 startIndex, System.Boolean bIsLittle)
  RVA=0x02F78CC0  token=0x6000029  System.Int16 ReadShort(System.Byte[] bs, System.Int32 startIndex, System.Boolean bIsLittle)
  RVA=0x02F79480  token=0x600002A  System.Byte ReadByte(System.Byte[] bs, System.Int32 startIndex, System.Boolean bIsLittle)
  RVA=0x063687D4  token=0x600002B  System.Single ReadFloat(System.Byte[] bs, System.Int32 startIndex, System.Boolean bIsLittle)
  RVA=0x031171E0  token=0x600002C  System.String ReadUTF8(System.Byte[] bs, System.Int32 startIndex, System.Int16& len)
  RVA=0x02F78960  token=0x600002D  System.Void SkipReadUTF8(System.Byte[] bs, System.Int32 startIndex, System.Int16& len)
  RVA=0x06368928  token=0x600002E  System.Byte[] WriteByte(System.Byte[] data, System.Int32 writerIndex, System.Byte value)
  RVA=0x031439B0  token=0x600002F  System.Byte[] WriteUShort(System.Byte[] data, System.Int32 writerIndex, System.UInt16 value, System.Boolean bIsLittle)
  RVA=0x06368ABC  token=0x6000030  System.Byte[] WriteShort(System.Byte[] data, System.Int32 writerIndex, System.Int16 value, System.Boolean bIsLittle)
  RVA=0x03143A40  token=0x6000031  System.Byte[] WriteUInt(System.Byte[] data, System.Int32 writerIndex, System.UInt32 value, System.Boolean bIsLittle)
  RVA=0x02F7E7D0  token=0x6000032  System.Span<System.Byte> WriteUInt(System.Span<System.Byte> data, System.Int32 writerIndex, System.UInt32 value, System.Boolean bIsLittle)
  RVA=0x03143940  token=0x6000033  System.Byte[] WriteInt(System.Byte[] data, System.Int32 writerIndex, System.Int32 value, System.Boolean bIsLittle)
  RVA=0x06368C00  token=0x6000034  System.Byte[] WriteULong(System.Byte[] data, System.Int32 writerIndex, System.UInt64 value, System.Boolean bIsLittle)
  RVA=0x02F7FF20  token=0x6000035  System.Span<System.Byte> WriteULong(System.Span<System.Byte> data, System.Int32 writerIndex, System.UInt64 value, System.Boolean bIsLittle)
  RVA=0x06368A30  token=0x6000036  System.Byte[] WriteLong(System.Byte[] data, System.Int32 writerIndex, System.Int64 value, System.Boolean bIsLittle)
  RVA=0x06368B4C  token=0x6000037  System.Byte[] WriteUInt128(System.Byte[] data, System.Int32 writerIndex, Beyond.Byte.UInt128 value, System.Boolean bIsLittle)
  RVA=0x02F7BD00  token=0x6000038  System.Span<System.Byte> WriteUInt128(System.Span<System.Byte> data, System.Int32 writerIndex, Beyond.Byte.UInt128 value, System.Boolean bIsLittle)
  RVA=0x063689A4  token=0x6000039  System.Byte[] WriteFloat(System.Byte[] data, System.Int32 writerIndex, System.Single value)
  RVA=0x06368DF8  token=0x600003A  System.Byte[] WriteUTF8(System.Byte[] data, System.Int32 writerIndex, System.String value, System.Int32& len)
  RVA=0x063688B8  token=0x600003B  System.Void SplitLong(System.Int64 longValue, System.Int32& highInt, System.Int32& lowInt)
  RVA=0x06368778  token=0x600003C  System.Int64 MergeLong(System.Int32 highInt, System.Int32 lowInt)
  RVA=0x06368864  token=0x600003D  System.Int32 SingleToInt32Bits(System.Single value)
  RVA=0x06368730  token=0x600003E  System.Single Int32BitsToSingle(System.Int32 value)
  RVA=0x06368564  token=0x600003F  System.String GetHexString(System.Byte[] byteArray)
  RVA=0x063685C0  token=0x6000040  System.String GetHexString(System.Byte[] byteArray, System.Int32 startIdx, System.Int32 length)
  RVA=0x02F76D20  token=0x6000041  System.String GetHexString(System.Span<System.Byte> byteArray)
  RVA=0x02F7EE90  token=0x6000042  System.String GetHexString(System.Span<System.Byte> byteArray, System.Int32 startIdx, System.Int32 length)
  RVA=0x03C6AF10  token=0x6000043  System.Void AppendHexString(Beyond.UnSafeString str, System.Span<System.Byte> byteArray)
  RVA=0x02F7F400  token=0x6000044  System.Void AppendHexString(Beyond.UnSafeString str, System.Span<System.Byte> byteArray, System.Int32 startIdx, System.Int32 length)
  RVA=0x03351C90  token=0x6000045  System.String GetHexStringUnsafeString(System.Span<System.Byte> byteArray)
  RVA=0x02F7E220  token=0x6000046  System.String GetHexStringUnsafeString(System.Span<System.Byte> byteArray, System.Int32 startIdx, System.Int32 length)
  RVA=0x063683E4  token=0x6000047  Beyond.Byte.UInt128 ComputeMD5Bytes(System.Byte[] bs, System.Int32 offset, System.Int32 count)
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
  RVA=0x032B2C30  token=0x6000048  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x06368EE0  token=0x6000049  System.Boolean __Gen_Wrap_0(Beyond.Byte.UInt128& P0, Beyond.Byte.UInt128 P1)
  RVA=0x063698D4  token=0x600004A  System.Boolean __Gen_Wrap_1(Beyond.Byte.UInt128& P0, System.Object P1)
  RVA=0x0636A550  token=0x600004B  System.Int32 __Gen_Wrap_2(Beyond.Byte.UInt128& P0)
  RVA=0x0636B318  token=0x600004C  Beyond.Byte.UInt128 __Gen_Wrap_3(System.Object P0)
  RVA=0x0636BF4C  token=0x600004D  System.Guid __Gen_Wrap_4(Beyond.Byte.UInt128& P0)
  RVA=0x0636CA70  token=0x600004E  System.Int32 __Gen_Wrap_5(Beyond.Byte.UInt128& P0, Beyond.Byte.UInt128 P1)
  RVA=0x0636CBD8  token=0x600004F  System.Boolean __Gen_Wrap_6(Beyond.Byte.UInt128 P0, Beyond.Byte.UInt128 P1)
  RVA=0x0636CD00  token=0x6000050  System.Int32 __Gen_Wrap_7(Beyond.Byte.ByteBufStream& P0)
  RVA=0x0636CE4C  token=0x6000051  System.Byte[] __Gen_Wrap_8(Beyond.Byte.ByteBufStream& P0)
  RVA=0x06369194  token=0x6000052  System.UInt32 __Gen_Wrap_9(System.Object P0, System.Int32 P1, System.Boolean P2)
  RVA=0x06369058  token=0x6000053  System.UInt32 __Gen_Wrap_10(Beyond.Byte.ByteBufStream& P0)
  RVA=0x06369194  token=0x6000054  System.Int32 __Gen_Wrap_11(System.Object P0, System.Int32 P1, System.Boolean P2)
  RVA=0x06369194  token=0x6000055  System.Byte __Gen_Wrap_12(System.Object P0, System.Int32 P1, System.Boolean P2)
  RVA=0x06369298  token=0x6000056  System.UInt64 __Gen_Wrap_13(System.Object P0, System.Int32 P1, System.Boolean P2)
  RVA=0x0636939C  token=0x6000057  System.UInt64 __Gen_Wrap_14(Beyond.Byte.ByteBufStream& P0)
  RVA=0x06369298  token=0x6000058  System.Int64 __Gen_Wrap_15(System.Object P0, System.Int32 P1, System.Boolean P2)
  RVA=0x063694D8  token=0x6000059  System.Int64 __Gen_Wrap_16(Beyond.Byte.ByteBufStream& P0)
  RVA=0x06369624  token=0x600005A  Beyond.Byte.UInt128 __Gen_Wrap_17(System.Object P0, System.Int32 P1, System.Boolean P2)
  RVA=0x0636975C  token=0x600005B  Beyond.Byte.UInt128 __Gen_Wrap_18(Beyond.Byte.ByteBufStream& P0)
  RVA=0x06369194  token=0x600005C  System.UInt16 __Gen_Wrap_19(System.Object P0, System.Int32 P1, System.Boolean P2)
  RVA=0x06369194  token=0x600005D  System.Int16 __Gen_Wrap_20(System.Object P0, System.Int32 P1, System.Boolean P2)
  RVA=0x06369A24  token=0x600005E  System.String __Gen_Wrap_21(System.Object P0, System.Int32 P1, System.Int16& P2)
  RVA=0x06369B70  token=0x600005F  System.String __Gen_Wrap_22(Beyond.Byte.ByteBufStream& P0)
  RVA=0x06369CD0  token=0x6000060  System.Void __Gen_Wrap_23(System.Object P0, System.Int32 P1, System.Int16& P2)
  RVA=0x06369DE8  token=0x6000061  System.Void __Gen_Wrap_24(Beyond.Byte.ByteBufStream& P0)
  RVA=0x06369F24  token=0x6000062  System.Byte[] __Gen_Wrap_25(System.Object P0, System.Int32 P1, System.Byte P2)
  RVA=0x0636A038  token=0x6000063  System.Void __Gen_Wrap_26(Beyond.Byte.ByteBufStream& P0, System.Byte P1)
  RVA=0x0636A180  token=0x6000064  System.Byte[] __Gen_Wrap_27(System.Object P0, System.Int32 P1, System.UInt32 P2, System.Boolean P3)
  RVA=0x0636A2B4  token=0x6000065  System.Void __Gen_Wrap_28(Beyond.Byte.ByteBufStream& P0, System.UInt32 P1)
  RVA=0x0636A3F8  token=0x6000066  System.Void __Gen_Wrap_29(Beyond.Byte.ByteBufStream& P0, System.Int32 P1)
  RVA=0x0636A694  token=0x6000067  System.Byte[] __Gen_Wrap_30(System.Object P0, System.Int32 P1, System.UInt64 P2, System.Boolean P3)
  RVA=0x0636A7BC  token=0x6000068  System.Void __Gen_Wrap_31(Beyond.Byte.ByteBufStream& P0, System.UInt64 P1)
  RVA=0x0636A904  token=0x6000069  System.Void __Gen_Wrap_32(Beyond.Byte.ByteBufStream& P0, System.Int64 P1)
  RVA=0x0636AA4C  token=0x600006A  System.Byte[] __Gen_Wrap_33(System.Object P0, System.Int32 P1, Beyond.Byte.UInt128 P2, System.Boolean P3)
  RVA=0x0636AB94  token=0x600006B  System.Void __Gen_Wrap_34(Beyond.Byte.ByteBufStream& P0, Beyond.Byte.UInt128 P1)
  RVA=0x0636AD00  token=0x600006C  System.Byte[] __Gen_Wrap_35(System.Object P0, System.Int32 P1, System.UInt16 P2, System.Boolean P3)
  RVA=0x0636AE34  token=0x600006D  System.Byte[] __Gen_Wrap_36(System.Object P0, System.Int32 P1, System.Int16 P2, System.Boolean P3)
  RVA=0x0636AF5C  token=0x600006E  System.Byte[] __Gen_Wrap_37(System.Object P0, System.Int32 P1, System.Object P2, System.Int32& P3)
  RVA=0x0636B0A8  token=0x600006F  System.Void __Gen_Wrap_38(Beyond.Byte.ByteBufStream& P0, System.Object P1)
  RVA=0x0636B1F0  token=0x6000070  Beyond.Byte.ByteBufStream __Gen_Wrap_39(System.Object P0, System.Int32 P1)
  RVA=0x0636B410  token=0x6000071  Beyond.Byte.ByteBufStream __Gen_Wrap_40(System.Int32 P0)
  RVA=0x0636B508  token=0x6000072  System.Single __Gen_Wrap_41(System.Int32 P0)
  RVA=0x0636B5C0  token=0x6000073  System.Single __Gen_Wrap_42(System.Object P0, System.Int32 P1, System.Boolean P2)
  RVA=0x0636B6B4  token=0x6000074  System.Span<System.Byte> __Gen_Wrap_43(System.Span<System.Byte> P0, System.Int32 P1, System.UInt32 P2, System.Boolean P3)
  RVA=0x0636B820  token=0x6000075  System.Byte[] __Gen_Wrap_44(System.Object P0, System.Int32 P1, System.Int32 P2, System.Boolean P3)
  RVA=0x0636B954  token=0x6000076  System.Span<System.Byte> __Gen_Wrap_45(System.Span<System.Byte> P0, System.Int32 P1, System.UInt64 P2, System.Boolean P3)
  RVA=0x0636BAC0  token=0x6000077  System.Byte[] __Gen_Wrap_46(System.Object P0, System.Int32 P1, System.Int64 P2, System.Boolean P3)
  RVA=0x0636BBE8  token=0x6000078  System.Span<System.Byte> __Gen_Wrap_47(System.Span<System.Byte> P0, System.Int32 P1, Beyond.Byte.UInt128 P2, System.Boolean P3)
  RVA=0x0636BD7C  token=0x6000079  System.Int32 __Gen_Wrap_48(System.Single P0)
  RVA=0x0636BE38  token=0x600007A  System.Byte[] __Gen_Wrap_49(System.Object P0, System.Int32 P1, System.Single P2)
  RVA=0x0636C0AC  token=0x600007B  System.Void __Gen_Wrap_50(System.Int64 P0, System.Int32& P1, System.Int32& P2)
  RVA=0x0636C1D4  token=0x600007C  System.Int64 __Gen_Wrap_51(System.Int32 P0, System.Int32 P1)
  RVA=0x0636C2A8  token=0x600007D  System.String __Gen_Wrap_52(System.Object P0, System.Int32 P1, System.Int32 P2)
  RVA=0x0636C3BC  token=0x600007E  System.String __Gen_Wrap_53(System.Object P0)
  RVA=0x0636C498  token=0x600007F  System.String __Gen_Wrap_54(System.Span<System.Byte> P0, System.Int32 P1, System.Int32 P2)
  RVA=0x0636C5E4  token=0x6000080  System.String __Gen_Wrap_55(System.Span<System.Byte> P0)
  RVA=0x0636C6FC  token=0x6000081  System.Void __Gen_Wrap_56(System.Object P0, System.Span<System.Byte> P1, System.Int32 P2, System.Int32 P3)
  RVA=0x0636C838  token=0x6000082  System.Void __Gen_Wrap_57(System.Object P0, System.Span<System.Byte> P1)
  RVA=0x0636C94C  token=0x6000083  Beyond.Byte.UInt128 __Gen_Wrap_58(System.Object P0, System.Int32 P1, System.Int32 P2)
  RVA=0x04D37A30  token=0x6000084  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x40
EXTENDS: IFix.Core.AnonymousStorey
FIELDS:
METHODS:
  RVA=0x0636CF98  token=0x6000085  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x6000086  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000087  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x0636D6A8  token=0x6000088  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x02F7AA50  token=0x6000089  System.Boolean IsPatched(System.Int32 id)
  RVA=0x0636D564  token=0x600008A  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x0636D630  token=0x600008B  System.Object CreateWrapper(System.Int32 id)
  RVA=0x0636D70C  token=0x600008C  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x0636D4C0  token=0x600008D  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: IFix.IDMAP0
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  IFix.IDMAP0                     Beyond-Byte-UInt128-Equals0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-UInt128-Equals1  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-UInt128-GetHashCode0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-UInt128-op_Implicit0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-UInt128-ToGuid0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-UInt128-CompareTo0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-UInt128-op_Equality0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-UInt128-op_Inequality0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-GetCurrentIdx0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-GetRaw0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-GetDataCopy0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-ReadUInt0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-ReadUInt0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-ReadInt0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-ReadInt0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-ReadByte0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-ReadByte0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-ReadULong0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-ReadULong0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-ReadLong0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-ReadLong0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-ReadUInt1280  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-ReadUInt1280  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-ReadUShort0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-ReadShort0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-ReadUTF80  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-ReadUTF80  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-SkipReadUTF80  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-SkipReadUTF80  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-WriteByte0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-WriteByte0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-WriteUInt0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-WriteUInt0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-WriteInt0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-WriteULong0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-WriteULong0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-WriteLong0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-WriteUInt1280  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-WriteUInt1280  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-WriteUShort0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-WriteShort0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-WriteUTF80  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-WriteUTF80  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-SkipBytes0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-RemainBytes0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-CreateFromByte0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteBufStream-Create0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-Int32BitsToSingle0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-ReadFloat0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-WriteUInt1  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-WriteInt0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-WriteULong1  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-WriteLong0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-WriteUInt1281  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-SingleToInt32Bits0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-WriteFloat0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-SplitLong0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-MergeLong0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-GetHexString0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-GetHexString1  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-GetHexString2  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-GetHexString3  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-AppendHexString0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-AppendHexString1  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-GetHexStringUnsafeString0  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-GetHexStringUnsafeString1  // const
  public    static  IFix.IDMAP0                     Beyond-Byte-ByteHelper-ComputeMD5Bytes0  // const
METHODS:
END_CLASS

