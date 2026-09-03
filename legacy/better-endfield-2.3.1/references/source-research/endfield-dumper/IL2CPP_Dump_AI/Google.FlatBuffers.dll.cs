// ========================================================
// Dumped by @desirepro
// Assembly: Google.FlatBuffers.dll
// Classes:  17
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

CLASS: UnionForShort
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x12
FIELDS:
  public            System.Int16                    shortVal  // 0x10
  public            System.UInt16                   ushortVal  // 0x10
METHODS:
END_CLASS

CLASS: UnionForLong
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x18
FIELDS:
  public            System.Int64                    longVal  // 0x10
  public            System.Single                   floatValLow  // 0x10
  public            System.Single                   floatValHigh  // 0x14
METHODS:
END_CLASS

CLASS: Google.FlatBuffers.ByteData
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x20
FIELDS:
  private           System.Byte*                    _buffer  // 0x10
  private           System.Int32                    _length  // 0x18
PROPERTIES:
  Buffer  get=0x02B76770
  Length  get=0x04D88B10
  Span  get=0x09AC920C
  ReadOnlySpan  get=0x05390CF0
METHODS:
  RVA=0x030CFC40  token=0x6000005  System.Boolean IsCreated()
  RVA=0x03585F10  token=0x6000006  System.Void .ctor(Unity.Collections.NativeArray<System.Byte> buffer)
  RVA=-1  // generic def  token=0x6000009  System.Span<T> ToSpan(T* ptr, System.Int32 length)
  RVA=-1  // generic def  token=0x600000A  System.ReadOnlySpan<T> ToReadOnlySpan(T* ptr, System.Int32 length)
END_CLASS

CLASS: Google.FlatBuffers.ByteBuffer
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x28
FIELDS:
  private           Google.FlatBuffers.ByteData     _buffer  // 0x10
  private           System.Int32                    _pos  // 0x20
  private   static  System.Collections.Generic.Dictionary<System.Type,System.Int32>genericSizes  // static @ 0x0
PROPERTIES:
  Position  get=0x02B2E2D0  set=0x014F51F0
  Length  get=0x04D88B10
METHODS:
  RVA=0x02B775A0  token=0x600000B  System.Void .ctor(Google.FlatBuffers.ByteData allocator, System.Int32 position)
  RVA=0x03585E20  token=0x600000C  System.Void .ctor(Unity.Collections.NativeArray<System.Byte> buffer)
  RVA=0x03585E90  token=0x600000D  System.Void .ctor(Unity.Collections.NativeArray<System.Byte> buffer, System.Int32 pos)
  RVA=0x030CFC40  token=0x600000E  System.Boolean IsCreated()
  RVA=0x04D91EC0  token=0x6000012  System.Void Reset()
  RVA=0x09AC8190  token=0x6000013  Google.FlatBuffers.ByteBuffer Duplicate()
  RVA=0x09AC8FB4  token=0x6000014  System.Byte[] ToArray(System.Int32 pos, System.Int32 len)
  RVA=-1  // generic def  token=0x6000015  System.Int32 SizeOf()
  RVA=-1  // generic def  token=0x6000016  System.Boolean IsSupportedType()
  RVA=-1  // generic def  token=0x6000017  System.Int32 ArraySize(T[] x)
  RVA=-1  // generic def  token=0x6000018  System.Int32 ArraySize(System.ArraySegment<T> x)
  RVA=-1  // generic def  token=0x6000019  System.Int32 ArraySize(System.Span<T> x)
  RVA=-1  // generic def  token=0x600001A  System.ReadOnlySpan<T> ToSpanSlice(System.Int32 pos, System.Int32 count)
  RVA=-1  // generic def  token=0x600001B  T[] ToArray(System.Int32 pos, System.Int32 count)
  RVA=-1  // generic def  token=0x600001C  Unity.Collections.NativeArray<T> ToNativeArray(System.Int32 pos, System.Int32 count)
  RVA=0x09AC907C  token=0x600001D  System.Byte[] ToSizedArray()
  RVA=0x09AC9024  token=0x600001E  System.Byte[] ToFullArray()
  RVA=0x09AC90D8  token=0x600001F  System.Span<System.Byte> ToSpan(System.Int32 pos, System.Int32 len)
  RVA=0x02B76770  token=0x6000020  System.Byte* GetUnsafePtr()
  RVA=0x09AC9170  token=0x6000021  System.Void WriteLittleEndian(System.Int32 offset, System.Int32 count, System.UInt64 data)
  RVA=0x09AC8EF4  token=0x6000022  System.UInt64 ReadLittleEndian(System.Int32 offset, System.Int32 count)
  RVA=0x041E1670  token=0x6000023  System.Void AssertOffsetAndLength(System.Int32 offset, System.Int32 length)
  RVA=0x09AC8A84  token=0x6000024  System.Void PutSbyte(System.Int32 offset, System.SByte value)
  RVA=0x09AC8754  token=0x6000025  System.Void PutByte(System.Int32 offset, System.Byte value)
  RVA=0x09AC87CC  token=0x6000026  System.Void PutByte(System.Int32 offset, System.Byte value, System.Int32 count)
  RVA=0x09AC8E94  token=0x6000027  System.Void Put(System.Int32 offset, System.Byte value)
  RVA=0x09AC8B5C  token=0x6000028  System.Void PutStringUTF8(System.Int32 offset, System.String value)
  RVA=0x09AC8AFC  token=0x6000029  System.Void PutShort(System.Int32 offset, System.Int16 value)
  RVA=0x09AC8D94  token=0x600002A  System.Void PutUshort(System.Int32 offset, System.UInt16 value)
  RVA=0x09AC89C4  token=0x600002B  System.Void PutInt(System.Int32 offset, System.Int32 value)
  RVA=0x09AC8C48  token=0x600002C  System.Void PutUint(System.Int32 offset, System.UInt32 value)
  RVA=0x09AC8A24  token=0x600002D  System.Void PutLong(System.Int32 offset, System.Int64 value)
  RVA=0x09AC8CEC  token=0x600002E  System.Void PutUlong(System.Int32 offset, System.UInt64 value)
  RVA=0x09AC8938  token=0x600002F  System.Void PutFloat(System.Int32 offset, System.Single value)
  RVA=0x09AC88AC  token=0x6000030  System.Void PutDouble(System.Int32 offset, System.Double value)
  RVA=0x09AC839C  token=0x6000031  System.SByte GetSbyte(System.Int32 index)
  RVA=0x09AC86E8  token=0x6000032  System.Byte Get(System.Int32 index)
  RVA=0x09AC845C  token=0x6000033  System.String GetStringUTF8(System.Int32 startPos, System.Int32 len)
  RVA=0x09AC8408  token=0x6000034  System.Int16 GetShort(System.Int32 offset)
  RVA=0x09AC8650  token=0x6000035  System.UInt16 GetUshort(System.Int32 offset)
  RVA=0x09AC82F4  token=0x6000036  System.Int32 GetInt(System.Int32 offset)
  RVA=0x09AC8520  token=0x6000037  System.UInt32 GetUint(System.Int32 offset)
  RVA=0x09AC8348  token=0x6000038  System.Int64 GetLong(System.Int32 offset)
  RVA=0x09AC85B8  token=0x6000039  System.UInt64 GetUlong(System.Int32 offset)
  RVA=0x09AC8274  token=0x600003A  System.Single GetFloat(System.Int32 offset)
  RVA=0x09AC81F4  token=0x600003B  System.Double GetDouble(System.Int32 offset)
  RVA=-1  // generic def  token=0x600003C  System.Int32 Put(System.Int32 offset, T[] x)
  RVA=-1  // generic def  token=0x600003D  System.Int32 Put(System.Int32 offset, System.ArraySegment<T> x)
  RVA=-1  // generic def  token=0x600003E  System.Int32 Put(System.Int32 offset, System.IntPtr ptr, System.Int32 sizeInBytes)
  RVA=-1  // generic def  token=0x600003F  System.Int32 Put(System.Int32 offset, System.Span<T> x)
  RVA=0x0404E940  token=0x6000040  System.Void .cctor()
END_CLASS

CLASS: Google.FlatBuffers.ByteBufferUtil
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09AC80CC  token=0x6000041  System.Int32 GetSizePrefix(Google.FlatBuffers.ByteBuffer bb)
  RVA=0x09AC8118  token=0x6000042  Google.FlatBuffers.ByteBuffer RemoveSizePrefix(Google.FlatBuffers.ByteBuffer bb)
  RVA=0x041E1670  token=0x6000043  System.Void .ctor()
END_CLASS

CLASS: Google.FlatBuffers.FlatBufferBuilder
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x78
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.Int32                    _space  // 0x10
  private           Unity.Collections.NativeArray<System.Byte>_data  // 0x18
  private           Google.FlatBuffers.ByteBuffer   _bb  // 0x28
  private           System.Int32                    _minAlign  // 0x40
  private           System.Int32[]                  _vtable  // 0x48
  private           System.Int32                    _vtableSize  // 0x50
  private           System.Int32                    _objectStart  // 0x54
  private           System.Int32[]                  _vtables  // 0x58
  private           System.Int32                    _numVtables  // 0x60
  private           System.Int32                    _vectorNumElems  // 0x64
  private           System.Collections.Generic.Dictionary<System.String,Google.FlatBuffers.StringOffset>_sharedStringMap  // 0x68
  private           System.Boolean                  <ForceDefaults>k__BackingField  // 0x70
PROPERTIES:
  ForceDefaults  get=0x04D86A50  set=0x04D86A80
  Offset  get=0x09ACB42C
  DataBuffer  get=0x04D9CD10
METHODS:
  RVA=0x09ACB298  token=0x6000044  System.Void .ctor(System.Int32 initialSize)
  RVA=0x09ACA1E8  token=0x6000045  System.Void Dispose()
  RVA=0x09AC9E6C  token=0x6000046  System.Void Clear()
  RVA=0x09ACA9E4  token=0x600004A  System.Void Pad(System.Int32 size)
  RVA=0x09ACA77C  token=0x600004B  System.Void GrowBuffer()
  RVA=0x09ACAA50  token=0x600004C  System.Void Prep(System.Int32 size, System.Int32 additionalBytes)
  RVA=0x09ACAAFC  token=0x600004D  System.Void PutBool(System.Boolean x)
  RVA=0x09ACAD6C  token=0x600004E  System.Void PutSbyte(System.SByte x)
  RVA=0x09ACAB64  token=0x600004F  System.Void PutByte(System.Byte x)
  RVA=0x09ACADD4  token=0x6000050  System.Void PutShort(System.Int16 x)
  RVA=0x09ACAF0C  token=0x6000051  System.Void PutUshort(System.UInt16 x)
  RVA=0x09ACAC9C  token=0x6000052  System.Void PutInt(System.Int32 x)
  RVA=0x09ACAE3C  token=0x6000053  System.Void PutUint(System.UInt32 x)
  RVA=0x09ACAD04  token=0x6000054  System.Void PutLong(System.Int64 x)
  RVA=0x09ACAEA4  token=0x6000055  System.Void PutUlong(System.UInt64 x)
  RVA=0x09ACAC34  token=0x6000056  System.Void PutFloat(System.Single x)
  RVA=-1  // generic def  token=0x6000057  System.Void Put(T[] x)
  RVA=-1  // generic def  token=0x6000058  System.Void Put(System.ArraySegment<T> x)
  RVA=-1  // generic def  token=0x6000059  System.Void Put(System.IntPtr ptr, System.Int32 sizeInBytes)
  RVA=-1  // generic def  token=0x600005A  System.Void Put(System.Span<T> x)
  RVA=0x09ACABCC  token=0x600005B  System.Void PutDouble(System.Double x)
  RVA=0x09AC92F0  token=0x600005C  System.Void AddBool(System.Boolean x)
  RVA=0x09AC9938  token=0x600005D  System.Void AddSbyte(System.SByte x)
  RVA=0x09AC936C  token=0x600005E  System.Void AddByte(System.Byte x)
  RVA=0x09AC9B04  token=0x600005F  System.Void AddShort(System.Int16 x)
  RVA=0x09AC9E34  token=0x6000060  System.Void AddUshort(System.UInt16 x)
  RVA=0x09AC9674  token=0x6000061  System.Void AddInt(System.Int32 x)
  RVA=0x09AC9BB8  token=0x6000062  System.Void AddUint(System.UInt32 x)
  RVA=0x09AC9770  token=0x6000063  System.Void AddLong(System.Int64 x)
  RVA=0x09AC9D34  token=0x6000064  System.Void AddUlong(System.UInt64 x)
  RVA=0x09AC95B4  token=0x6000065  System.Void AddFloat(System.Single x)
  RVA=-1  // generic def  token=0x6000066  System.Void Add(T[] x)
  RVA=-1  // generic def  token=0x6000067  System.Void Add(System.ArraySegment<T> x)
  RVA=-1  // generic def  token=0x6000068  System.Void Add(System.IntPtr ptr, System.Int32 sizeInBytes)
  RVA=-1  // generic def  token=0x6000069  System.Void Add(System.Span<T> x)
  RVA=0x09AC9530  token=0x600006A  System.Void AddDouble(System.Double x)
  RVA=0x09AC9868  token=0x600006B  System.Void AddOffset(System.Int32 off)
  RVA=0x09ACB230  token=0x600006C  System.Void StartVector(System.Int32 elemSize, System.Int32 count, System.Int32 alignment)
  RVA=0x09ACA558  token=0x600006D  Google.FlatBuffers.VectorOffset EndVector()
  RVA=-1  // generic def  token=0x600006E  Google.FlatBuffers.VectorOffset CreateVectorOfTables(Google.FlatBuffers.Offset<T>[] offsets)
  RVA=0x09ACA90C  token=0x600006F  System.Void Nested(System.Int32 obj)
  RVA=0x09ACA97C  token=0x6000070  System.Void NotNested()
  RVA=0x09ACB15C  token=0x6000071  System.Void StartTable(System.Int32 numfields)
  RVA=0x09ACB0CC  token=0x6000072  System.Void Slot(System.Int32 voffset)
  RVA=0x09AC9328  token=0x6000073  System.Void AddBool(System.Int32 o, System.Boolean x, System.Boolean d)
  RVA=0x09AC9268  token=0x6000074  System.Void AddBool(System.Int32 o, System.Nullable<System.Boolean> x)
  RVA=0x09AC99F8  token=0x6000075  System.Void AddSbyte(System.Int32 o, System.SByte x, System.SByte d)
  RVA=0x09AC9970  token=0x6000076  System.Void AddSbyte(System.Int32 o, System.Nullable<System.SByte> x)
  RVA=0x09AC942C  token=0x6000077  System.Void AddByte(System.Int32 o, System.Byte x, System.Byte d)
  RVA=0x09AC93A4  token=0x6000078  System.Void AddByte(System.Int32 o, System.Nullable<System.Byte> x)
  RVA=0x09AC9A3C  token=0x6000079  System.Void AddShort(System.Int32 o, System.Int16 x, System.Int32 d)
  RVA=0x09AC9A80  token=0x600007A  System.Void AddShort(System.Int32 o, System.Nullable<System.Int16> x)
  RVA=0x09AC9DF0  token=0x600007B  System.Void AddUshort(System.Int32 o, System.UInt16 x, System.UInt16 d)
  RVA=0x09AC9D6C  token=0x600007C  System.Void AddUshort(System.Int32 o, System.Nullable<System.UInt16> x)
  RVA=0x09AC96AC  token=0x600007D  System.Void AddInt(System.Int32 o, System.Int32 x, System.Int32 d)
  RVA=0x09AC96EC  token=0x600007E  System.Void AddInt(System.Int32 o, System.Nullable<System.Int32> x)
  RVA=0x09AC9B78  token=0x600007F  System.Void AddUint(System.Int32 o, System.UInt32 x, System.UInt32 d)
  RVA=0x09AC9BF0  token=0x6000080  System.Void AddUint(System.Int32 o, System.Nullable<System.UInt32> x)
  RVA=0x09AC9828  token=0x6000081  System.Void AddLong(System.Int32 o, System.Int64 x, System.Int64 d)
  RVA=0x09AC97A8  token=0x6000082  System.Void AddLong(System.Int32 o, System.Nullable<System.Int64> x)
  RVA=0x09AC9CF4  token=0x6000083  System.Void AddUlong(System.Int32 o, System.UInt64 x, System.UInt64 d)
  RVA=0x09AC9C74  token=0x6000084  System.Void AddUlong(System.Int32 o, System.Nullable<System.UInt64> x)
  RVA=0x09AC956C  token=0x6000085  System.Void AddFloat(System.Int32 o, System.Single x, System.Double d)
  RVA=0x09AC95F0  token=0x6000086  System.Void AddFloat(System.Int32 o, System.Nullable<System.Single> x)
  RVA=0x09AC94F0  token=0x6000087  System.Void AddDouble(System.Int32 o, System.Double x, System.Double d)
  RVA=0x09AC9470  token=0x6000088  System.Void AddDouble(System.Int32 o, System.Nullable<System.Double> x)
  RVA=0x09AC98FC  token=0x6000089  System.Void AddOffset(System.Int32 o, System.Int32 x, System.Int32 d)
  RVA=0x09ACA040  token=0x600008A  Google.FlatBuffers.StringOffset CreateString(System.String s)
  RVA=0x09ACA120  token=0x600008B  Google.FlatBuffers.StringOffset CreateUTF8String(System.Span<System.Byte> chars)
  RVA=0x09AC9F1C  token=0x600008C  Google.FlatBuffers.StringOffset CreateSharedString(System.String s)
  RVA=0x09AC9B3C  token=0x600008D  System.Void AddStruct(System.Int32 voffset, System.Int32 x, System.Int32 d)
  RVA=0x09ACA224  token=0x600008E  System.Int32 EndTable()
  RVA=0x09ACAF74  token=0x600008F  System.Void Required(System.Int32 table, System.Int32 field)
  RVA=0x09ACA5A4  token=0x6000090  System.Void Finish(System.Int32 rootTable, System.Boolean sizePrefix)
  RVA=0x09ACA754  token=0x6000091  System.Void Finish(System.Int32 rootTable)
  RVA=0x09ACA598  token=0x6000092  System.Void FinishSizePrefixed(System.Int32 rootTable)
  RVA=0x09ACB080  token=0x6000094  System.Byte[] SizedByteArray()
  RVA=0x09ACA640  token=0x6000095  System.Void Finish(System.Int32 rootTable, System.String fileIdentifier, System.Boolean sizePrefix)
  RVA=0x09ACA760  token=0x6000096  System.Void Finish(System.Int32 rootTable, System.String fileIdentifier)
  RVA=0x09ACA57C  token=0x6000097  System.Void FinishSizePrefixed(System.Int32 rootTable, System.String fileIdentifier)
END_CLASS

CLASS: Google.FlatBuffers.FlatBufferConstants
TYPE:  static class
TOKEN: 0x2000008
SIZE:  0x10
FIELDS:
  public    static  System.Int32                    FileIdentifierLength  // const
  public    static  System.Int32                    SizePrefixLength  // const
METHODS:
  RVA=0x041E1670  token=0x6000098  System.Void FLATBUFFERS_23_1_21()
END_CLASS

CLASS: Google.FlatBuffers.FlatBufferUtils
TYPE:  static class
TOKEN: 0x2000009
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04D875C0  token=0x6000099  System.Int16 ReinterpretAsShort(System.UInt16 val)
  RVA=0x04D875C0  token=0x600009A  System.UInt16 ReinterpretAsUShort(System.Int16 val)
  RVA=0x04DA0C70  token=0x600009B  System.Void ReinterpretAsFloat(System.Int64 val, System.Single& floatValLow, System.Single& floatValHigh)
  RVA=-1  // generic def  token=0x600009C  System.Span<T> AsSpan(Unity.Collections.NativeArray<T> nativeArray)
  RVA=-1  // generic def  token=0x600009D  System.ReadOnlySpan<T> AsReadOnlySpan(Unity.Collections.NativeArray<T> nativeArray)
END_CLASS

CLASS: Google.FlatBuffers.IFlatbufferObject
TYPE:  interface
TOKEN: 0x200000C
FIELDS:
PROPERTIES:
  ByteBuffer  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x600009E  System.Void __init(System.Int32 _i, Google.FlatBuffers.ByteBuffer _bb)
END_CLASS

CLASS: Google.FlatBuffers.Offset`1
TYPE:  sealed struct
TOKEN: 0x200000D
FIELDS:
  public            System.Int32                    Value  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60000A0  System.Void .ctor(System.Int32 value)
END_CLASS

CLASS: Google.FlatBuffers.StringOffset
TYPE:  sealed struct
TOKEN: 0x200000E
SIZE:  0x14
FIELDS:
  public            System.Int32                    Value  // 0x10
METHODS:
  RVA=0x04D86140  token=0x60000A1  System.Void .ctor(System.Int32 value)
END_CLASS

CLASS: Google.FlatBuffers.VectorOffset
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x14
FIELDS:
  public            System.Int32                    Value  // 0x10
METHODS:
  RVA=0x04D86140  token=0x60000A2  System.Void .ctor(System.Int32 value)
END_CLASS

CLASS: Google.FlatBuffers.Struct
TYPE:  sealed struct
TOKEN: 0x2000010
SIZE:  0x30
FIELDS:
  private           System.Int32                    <bb_pos>k__BackingField  // 0x10
  private           Google.FlatBuffers.ByteBuffer   <bb>k__BackingField  // 0x18
PROPERTIES:
  bb_pos  get=0x02B77620  set=0x04D86140
  bb  get=0x02A8F1E0  set=0x04DA0CB0
METHODS:
  RVA=0x04DA0C90  token=0x60000A7  System.Void .ctor(System.Int32 _i, Google.FlatBuffers.ByteBuffer _bb)
END_CLASS

CLASS: Google.FlatBuffers.Table
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x30
FIELDS:
  private           System.Int32                    <bb_pos>k__BackingField  // 0x10
  private           Google.FlatBuffers.ByteBuffer   <bb>k__BackingField  // 0x18
PROPERTIES:
  bb_pos  get=0x02B77620  set=0x04D86140
  bb  get=0x02A8F1E0  set=0x04DA0CB0
  ByteBuffer  get=0x02A8F1E0
METHODS:
  RVA=0x04DA0C90  token=0x60000AD  System.Void .ctor(System.Int32 _i, Google.FlatBuffers.ByteBuffer _bb)
  RVA=0x09ACB8F0  token=0x60000AE  System.Int32 __offset(System.Int32 vtableOffset)
  RVA=0x09ACB878  token=0x60000AF  System.Int32 __offset(System.Int32 vtableOffset, System.Int32 offset, Google.FlatBuffers.ByteBuffer bb)
  RVA=0x09ACB814  token=0x60000B0  System.Int32 __indirect(System.Int32 offset)
  RVA=0x09ACB7BC  token=0x60000B1  System.Int32 __indirect(System.Int32 offset, Google.FlatBuffers.ByteBuffer bb)
  RVA=0x0336F6C0  token=0x60000B2  System.String __string(System.Int32 offset)
  RVA=0x09ACB9CC  token=0x60000B3  System.Int32 __vector_len(System.Int32 offset)
  RVA=0x09ACBA54  token=0x60000B4  System.Int32 __vector(System.Int32 offset)
  RVA=-1  // generic def  token=0x60000B5  System.Span<T> __vector_as_span(System.Int32 offset, System.Int32 elementSize)
  RVA=-1  // generic def  token=0x60000B6  T[] __vector_as_array(System.Int32 offset)
  RVA=-1  // generic def  token=0x60000B7  T __union(System.Int32 offset)
  RVA=0x09ACB694  token=0x60000B8  System.Boolean __has_identifier(Google.FlatBuffers.ByteBuffer bb, System.String ident)
  RVA=0x09ACB57C  token=0x60000B9  System.Int32 CompareStrings(System.Int32 offset_1, System.Int32 offset_2, Google.FlatBuffers.ByteBuffer bb)
  RVA=0x09ACB474  token=0x60000BA  System.Int32 CompareStrings(System.Int32 offset_1, System.Byte[] key, Google.FlatBuffers.ByteBuffer bb)
END_CLASS

CLASS: Microsoft.CodeAnalysis.EmbeddedAttribute
TYPE:  sealed class
TOKEN: 0x2000002
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
END_CLASS

CLASS: System.Runtime.CompilerServices.IsUnmanagedAttribute
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000002  System.Void .ctor()
END_CLASS

